using NAudio.Wave;
using Newtonsoft.Json;
using SkiaSharp;
namespace FAStudio.RES
{
	public class UnpackXNB
	{
		string input;
		string filepath;
		XNBFILE.Sound sound = new();
		XNBFILE.Image image = new();
		XNBFILE.Music music = new();
		XNBFILE.Font font = new();
		public UnpackXNB(string input)
		{
			this.input = input;
		}
		public UnpackXNB(string input, string filepath)
		{
			this.input = input;
			this.filepath = filepath;
		}
		public XNBFILE.Sound ReadSound()
		{
			BinaryReader br = new(new FileStream(input, FileMode.Open));
			sound.ID = br.ReadChars(3);
			sound.Platform = br.ReadByte();
			sound.Bit_Verson = br.ReadByte();
			sound.Flag_bit = br.ReadByte();
			sound.Size = br.ReadUInt32();
			sound.count = br.ReadByte();
			sound.string_length = br.ReadByte();
			sound.Type_count = br.ReadChars(sound.string_length);
			sound.Verson = br.ReadInt32();
			sound.Unknown = br.ReadByte();
			sound.UnKnown = br.ReadByte();
			sound.Format_Size = br.ReadUInt32();
			sound.Format = br.ReadBytes((int)sound.Format_Size);
			sound.Data_Size = br.ReadUInt32();
			sound.data = br.ReadBytes((int)sound.Data_Size);
			sound.Start = br.ReadInt32();
			sound.Start_Length = br.ReadInt32();
			sound.MS_Time = br.ReadInt32();
			return sound;
		}
		public XNBFILE.Image ReadImage()
		{
			BinaryReader br = new(File.OpenRead(input));
			image.ID = br.ReadChars(3);
			image.Platform = br.ReadByte();
			image.Bit_Verson = br.ReadByte();
			image.Flag_bit = br.ReadByte();
			image.Size = br.ReadUInt32();
			image.count = br.ReadByte();
			image.string_length = br.ReadByte();
			br.ReadByte();
			image.Type_count = br.ReadChars(image.string_length);
			image.Verson = br.ReadInt32();
			image.Unknown = br.ReadByte();
			image.UnKnown = br.ReadByte();
			image.Surface_format = br.ReadInt32();
			image.Width = br.ReadUInt32();
			image.Height = br.ReadUInt32();
			image.Mip_count = br.ReadUInt32();
			image.Data_Size = br.ReadUInt32();
			image.Data = br.ReadBytes((int)image.Data_Size);
			return image;
		}
		public XNBFILE.Music ReadMusic()
		{
			BinaryReader br = new(File.OpenRead(input));
			music.ID = br.ReadChars(3);
			music.Platform = br.ReadByte();
			music.Bit_Verson = br.ReadByte();
			music.Flag_bit = br.ReadByte();
			music.Size = br.ReadUInt32();
			music.count = br.ReadByte();
			music.string_length = new byte[music.count];
			music.Type_count = new char[music.count];
			music.Verson = new int[music.count];
			for (int i = 0; i < music.count; i++)
			{
				music.string_length[i] = br.ReadByte();
				music.Type_count = br.ReadChars(music.string_length[i]);
				music.Verson[i] = br.ReadInt32();
			}
			music.Unknown = br.ReadByte();
			music.Sign = br.ReadByte();
			music.name_length = br.ReadByte();
			music.name = br.ReadChars((int)music.name_length);
			music.Sign2 = br.ReadByte();
			music.MS_Time = br.ReadInt32();
			return music;
		}
		public XNBFILE.Font ReadFont()
		{
			BinaryReader br = new(new FileStream(input, FileMode.Open));
			font.ID = br.ReadChars(3);
			font.Platform = br.ReadChar();
			font.Bit_Verson = br.ReadByte();
			font.Flag_bit = br.ReadByte();
			font.Size = br.ReadUInt32();
			font.count = br.ReadByte();
			font.string_length = new byte[font.count];
			font.Verson = new int[font.count];
			font.Type_count = new string[font.count];
			for (int a = 0; a < font.count; a++)
			{
				font.string_length[a] = br.ReadByte();
				var i = br.ReadByte();
				if (i != 1)
				{
					br.BaseStream.Seek(-1, SeekOrigin.Current);
				}
				font.Type_count[a] = new string(br.ReadChars(font.string_length[a]));
				font.Verson[a] = br.ReadInt32();
			}
			font.constant = br.ReadBytes(3);
			font.Format = br.ReadUInt32();
			font.Width = br.ReadUInt32();
			font.Height = br.ReadUInt32();
			font.Mip_count = br.ReadUInt32();
			font.Data_Size = br.ReadUInt32();
			font.Data = br.ReadBytes((int)font.Data_Size);
			//
			font.Glyphs.Count = br.ReadByte();
			font.Glyphs.Size = br.ReadUInt32();
			font.Glyphs.data = new Rectangle[font.Glyphs.Size];
			for (int a = 0; a < (int)font.Glyphs.Size; a++)
			{
				font.Glyphs.data[a].x = br.ReadInt32();
				font.Glyphs.data[a].y = br.ReadInt32();
				font.Glyphs.data[a].width = br.ReadInt32();
				font.Glyphs.data[a].height = br.ReadInt32();
			}
			//
			font.Cropping.Count = br.ReadByte();
			font.Cropping.Size = br.ReadUInt32();
			font.Cropping.data = new Rectangle[font.Cropping.Size];
			for (int a = 0; a < (int)font.Cropping.Size; a++)
			{
				font.Cropping.data[a].x = br.ReadInt32();
				font.Cropping.data[a].y = br.ReadInt32();
				font.Cropping.data[a].width = br.ReadInt32();
				font.Cropping.data[a].height = br.ReadInt32();
			}
			//
			font.Character_map.Size = br.ReadByte();
			font.Character_map.Size = br.ReadUInt32();
			font.Character_map.data = new char[font.Character_map.Size];
			font.Character_map.data = br.ReadChars((int)font.Character_map.Size);
			//
			font.Vertical_line_spacing = br.ReadInt32();
			font.Horizontal_spacing = br.ReadSingle();
			//
			font.Kerning.Count = br.ReadByte();
			font.Kerning.Size = br.ReadUInt32();
			font.Kerning.data = new Vector3[font.Kerning.Size];
			for (int a = 0; a < font.Kerning.Size; a++)
			{
				font.Kerning.data[a].x = br.ReadSingle();
				font.Kerning.data[a].y = br.ReadSingle();
				font.Kerning.data[a].z = br.ReadSingle();
			}
			//
			font.Default_character = br.ReadByte();
			return font;
		}
		public XNBFILE.Music Return_Music() { return music; }
		public XNBFILE.Sound Return_Sound() { return sound; }
		public XNBFILE.Image Return_Image() { return image; }
		public XNBFILE.Font Return_Font() { return font; }
	}
	public class Wma
	{
		public string filepath;
		public Wma(string filepath)
		{
			this.filepath = filepath;
		}
		private char[] StringToCharArry(string str)
		{
			char[] temp = str.ToCharArray(0, str.Length);
			return temp;
		}
		public void WmaWrite()
		{
			string filepath_wma = Path.GetDirectoryName(filepath) + @"\" + Path.GetFileNameWithoutExtension(filepath) + @".xnb";
			var reader = new MediaFoundationReader(filepath);
			var milliseconds = reader.TotalTime.TotalMilliseconds;
			//var milliseconds = reader.TotalTime;
			BinaryWriter bw = new BinaryWriter(new FileStream(filepath_wma, FileMode.OpenOrCreate));
			bw.Write(StringToCharArry("XNB"));
			bw.Write('m');
			bw.Write((byte)5);
			bw.Write((byte)0);
			bw.Write(0);
			//string Str = "Microsoft.Xna.Framework.Content.SongReader";
			//string str = "Microsoft.Xna.Framework.Content.Int32Reader";
			bw.Write((byte)2);
			bw.Write("Microsoft.Xna.Framework.Content.SongReader");
			bw.Write((int)0);
			bw.Write("Microsoft.Xna.Framework.Content.Int32Reader");
			bw.Write((int)0);
			bw.Write((byte)0);
			bw.Write((byte)1);
			bw.Write(Path.GetFileName(filepath));
			bw.Write((byte)2);
			bw.Write((UInt32)Math.Ceiling(milliseconds));
			var FileSize = bw.BaseStream.Position;
			bw.Seek(6, SeekOrigin.Begin);
			bw.Write((UInt32)FileSize);
			bw.Flush();
			bw.Close();
		}
	}
	public class Wav
	{
		string output;
		XNBFILE.Sound sound;
		public Wav(string output, XNBFILE.Sound sound)
		{
			this.output = output;
			this.sound = sound;
		}
		private Char[] StringToCharArry(string str)
		{
			char[] temp = str.ToCharArray(0, str.Length);
			return temp;
		}
		public void WriteWav()
		{
			BinaryWriter bw = new BinaryWriter(new FileStream(output, FileMode.Create));
			//XNBFILE.Sound sound = new();
			bw.Write(StringToCharArry("RIFF"));
			bw.Write((int)(sound.Format_Size + 24 + sound.Data_Size));
			bw.Write(StringToCharArry("WAVE"));
			bw.Write(StringToCharArry("fmt "));
			//bw.Write(20);
			bw.Write(sound.Format_Size);
			bw.Write(sound.Format);
			bw.Write(StringToCharArry("data"));
			bw.Write(sound.Data_Size);
			bw.Write(sound.data);
			bw.Flush();
			bw.Close();
			/*
			bw.Write("LIST");
			bw.Write((int)(name.Lengt + 13));
			bw.Write("INFO");
			bw.Write("INAM")
			bw.Write((int)(name.Length + 1));
			bw.Write(name);
			bw.Write((byte)0);
			bw.Write((byte)0);
			*/
		}
		string input;
		public Wav(string input)
		{
			this.input = input;
		}
	}
	public class packXNB
	{
		public string input;
		public string output;
		XNBFILE.Music wma;
		XNBFILE.Sound wav;
		XNBFILE.Font font;
		XNBFILE.Image image;
		Audio.WAVE wave;
		private char[] StringToCharArry(string str)
		{
			char[] temp = str.ToCharArray(0, str.Length);
			return temp;
		}
		public packXNB(string input)
		{
			this.input = input;
		}
		public packXNB(string output, XNBFILE.Image image)
		{
			this.output = output;
			this.image = image;
		}
		public packXNB(string output, XNBFILE.Font font)
		{
			this.output = output;
			this.font = font;
		}
		public packXNB(string output, XNBFILE.Sound wav)
		{
			this.output = output;
			this.wav = wav;
		}
		public packXNB(string output, XNBFILE.Music wma)
		{
			this.output = output;
			this.wma = wma;
		}
		public packXNB(string output, Audio.WAVE wave)
		{
			this.output = output;
			this.wave = wave;
		}
		public void WAV_Write()
		{
			try
			{
				BinaryWriter bw = new BinaryWriter(new FileStream(output, FileMode.Create));
				bw.Write(StringToCharArry("XNB"));
				bw.Write('m');
				bw.Write((byte)5);
				bw.Write((byte)0);
				bw.Write((UInt32)0);
				bw.Write((byte)1);
				bw.Write("Microsoft.Xna.Framework.Content.SoundEffectReader");
				bw.Write((int)0);
				bw.Write((byte)0);
				bw.Write((byte)1);
				bw.Write(wave.fmt.Size);
				bw.Write(wave.fmt.fmt.Code);
				bw.Write(wave.fmt.fmt.Channels);
				bw.Write(wave.fmt.fmt.Sample);
				bw.Write(wave.fmt.fmt.Rate);
				bw.Write(wave.fmt.fmt.Block);
				bw.Write(wave.fmt.fmt.Sample_bit);
				if (wave.fmt.fmt.Other != null) { bw.Write(wave.fmt.fmt.Other); }
				bw.Write(wave.data.Size);
				bw.Write(wave.data.data);
				bw.Write((int)0);
				bw.Write((int)wave.data.Size / ((int)wave.fmt.fmt.Channels * 2));
				bw.Write((int)wave.data.Size / (int)wave.fmt.fmt.Rate);
				var FileSize = bw.BaseStream.Position;
				bw.Seek(6, SeekOrigin.Begin);
				bw.Write((UInt32)FileSize);
				bw.Flush();
				bw.Close();
			}
			catch
			{ }
		}
		public void Wav_Write()
		{
			File.Create(output);
			BinaryWriter bw = new BinaryWriter(File.OpenWrite(output));
			bw.Write(StringToCharArry("XNB"));
			bw.Write('m');
			bw.Write((byte)5);
			bw.Write((byte)0);
			bw.Write((UInt32)(137 + wav.Data_Size));
			bw.Write((byte)1);
			bw.Write("Microsoft.Xna.Framework.Content.SoundEffectReader");
			bw.Write(wav.Verson);
			bw.Write((byte)0);
			bw.Write((byte)1);
			bw.Write(wav.Format_Size);
			bw.Write(wav.Format);
			bw.Write(wav.Data_Size);
			bw.Write(wav.data);
			bw.Write(wav.Start);
			bw.Write(wav.Start_Length);
			bw.Write(wav.MS_Time);
			bw.Flush();
			bw.Close();
		}
		public void WMA_Write()
		{
			var i = new Wma(input);
			i.WmaWrite();
		}
		public void PNG_Write(byte[] data, UInt32 width, UInt32 heigth)
		{
			BinaryWriter bw = new BinaryWriter(new FileStream(input, FileMode.Create));
			bw.Write(StringToCharArry("XNB"));
			bw.Write('m');
			bw.Write((byte)5);
			bw.Write((byte)0);
			bw.Write(0);
			bw.Write((byte)1);
			string i = "Microsoft.Xna.Framework.Content.Texture2DReader, Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553";
			bw.Write((byte)i.Length);
			bw.Write((byte)1);
			bw.Write(StringToCharArry(i));
			bw.Write((int)0);
			bw.Write((byte)0);
			bw.Write((byte)1);
			bw.Write((UInt32)0);//Color
			bw.Write((UInt32)width);
			bw.Write((UInt32)heigth);
			bw.Write((UInt32)1);
			bw.Write(data);
			var FileSize = bw.BaseStream.Position;
			bw.Seek(6, SeekOrigin.Begin);
			bw.Write((UInt32)FileSize);
			bw.Flush();
			bw.Close();
		}
		private static int TypeReview(string type)
		{
			if (type == @"Microsoft.Xna.Framework.Content.SpriteFontReader, Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.Texture2DReader, Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Rectangle, Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553]]")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553]]")
			{
				return 1;
			}
			else if (type == @"Microsoft.Xna.Framework.Content.ListReader`1[[System.Char, mscorlib, Version=3.7.0.0, Culture=neutral, PublicKeyToken=969db8053d3322ac]]")
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
		private static byte[] CharToByteArray(char[] chars)
		{
			byte[] bytes = new byte[chars.Length];
			for (int i = 0; i < chars.Length; i++)
			{
				bytes[i] = (byte)chars[i];
			}
			return bytes;
		}
		public void Font_Write(byte[] data, UInt32 width, UInt32 higth)
		{
			BinaryWriter bw = new(new FileStream(output, FileMode.OpenOrCreate));
			bw.Write(StringToCharArry("XNB"));
			bw.Write(font.Platform);
			bw.Write(font.Bit_Verson);
			bw.Write(font.Flag_bit);
			/*
			uint FileSize = 11;//header
			for (int a = 0; a < font.count; a++)
			{
				FileSize++;
				if (TypeReview(font.Type_count[a]) == 1)
				{
					FileSize++;
				}
				FileSize += font.string_length[a];
				FileSize += 4;
			}
			FileSize += 23;
			FileSize += (uint)data.Length;
			FileSize++;
			FileSize += 4;
			FileSize += (font.Glyphs.Size*4*4);
			FileSize++;
			FileSize += 4;
			FileSize += (font.Cropping.Size * 4*4);
			FileSize += (uint)CharToByteArray(font.Character_map.data).Length;
			FileSize += 8;
			FileSize++;
			FileSize += 4;
			FileSize += (font.Kerning.Size * 4*3);
			FileSize++;
			*/
			bw.Write(0);//size
			bw.Write(font.count);
			for (int a = 0; a < font.count; a++)
			{
				bw.Write(font.string_length[a]);
				if (TypeReview(font.Type_count[a]) == 1)
				{
					bw.Write((byte)1);
				}
				bw.Write(StringToCharArry(font.Type_count[a]));
				bw.Write(font.Verson[a]);
			}
			bw.Write((byte)0);
			bw.Write((byte)1);
			bw.Write((byte)2);
			bw.Write(font.Format);
			bw.Write(width);
			bw.Write(higth);
			bw.Write(1);
			bw.Write(data.Length);
			bw.Write(data);
			bw.Write(font.Glyphs.Count);
			bw.Write(font.Glyphs.Size);
			foreach (var i in font.Glyphs.data)
			{
				bw.Write(i.x);
				bw.Write(i.y);
				bw.Write(i.width);
				bw.Write(i.height);
			}
			bw.Write(font.Cropping.Count);
			bw.Write(font.Cropping.Size);
			foreach (var i in font.Cropping.data)
			{
				bw.Write(i.x);
				bw.Write(i.y);
				bw.Write(i.width);
				bw.Write(i.height);
			}
			bw.Write(font.Character_map.Count);
			bw.Write(font.Character_map.Size);
			foreach (var i in font.Character_map.data)
			{
				bw.Write(i);
			}
			bw.Write(font.Vertical_line_spacing);
			bw.Write(font.Horizontal_spacing);
			bw.Write(font.Kerning.Count);
			bw.Write(font.Kerning.Size);
			foreach (var i in font.Kerning.data)
			{
				bw.Write(i.x);
				bw.Write(i.y);
				bw.Write(i.z);
			}
			bw.Write(font.Default_character);
			var FileSize = bw.BaseStream.Position;
			bw.Seek(6, SeekOrigin.Begin);
			bw.Write((UInt32)FileSize);
			bw.Flush();
			bw.Close();
		}
	}
	public class Png
	{
		string filepath;
		string[] filepaths;
		public Png(string filepath)
		{
			this.filepath = filepath;
		}
		public Png(string[] filepaths)
		{
			this.filepaths = filepaths;
		}
		public byte[] ConvertRGBAtoBGRA(byte[] rgbaData)
		{
			var bgraData = new byte[rgbaData.Length];
			for (int i = 0; i < rgbaData.Length; i += 4)
			{
				bgraData[i] = rgbaData[i + 2];
				bgraData[i + 1] = rgbaData[i + 1];
				bgraData[i + 2] = rgbaData[i];
				bgraData[i + 3] = rgbaData[i + 3];
			}
			return bgraData;
		}
		public byte[] ConvertRGBAtoARGB(byte[] rgbaData)
		{
			var argbData = new byte[rgbaData.Length];
			for (int i = 0; i < rgbaData.Length; i += 4)
			{
				argbData[i] = rgbaData[i + 3];
				argbData[i + 1] = rgbaData[i];
				argbData[i + 2] = rgbaData[i + 1];
				argbData[i + 3] = rgbaData[i + 2];
			}
			return argbData;
		}
		public void CreatePng(int width, int height, byte[] rgbaData)
		{
			using (var image = SKImage.FromPixels(new SKImageInfo(width, height), SKData.CreateCopy(rgbaData)))
			{
				using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
				{
					using (var stream = new FileStream(filepath, FileMode.OpenOrCreate))
					{
						data.SaveTo(stream);
					}
				}
			}
		}
		private byte[] ConvertARGBToRGBA(byte[] argbData)
		{
			var rgbaData = new byte[argbData.Length];
			for (int i = 0; i < argbData.Length; i += 4)
			{
				rgbaData[i] = argbData[i + 1];
				rgbaData[i + 1] = argbData[i + 2];
				rgbaData[i + 2] = argbData[i + 3];
				rgbaData[i + 3] = argbData[i];
			}
			return rgbaData;
		}
		public byte[] ConvertBGRAtoRGBA(byte[] bgraData)
		{
			var rgbaData = new byte[bgraData.Length];
			for (int i = 0; i < bgraData.Length; i += 4)
			{
				rgbaData[i] = bgraData[i + 2];
				rgbaData[i + 1] = bgraData[i + 1];
				rgbaData[i + 2] = bgraData[i];
				rgbaData[i + 3] = bgraData[i + 3];
			}
			return rgbaData;
		}
		public static byte[] ConvertBGRAToRGBA(byte[] bgra)
		{
			byte[] rgba = new byte[bgra.Length];
			for (int i = 0; i < bgra.Length; i += 4)
			{
				rgba[i] = bgra[i + 2];
				rgba[i + 1] = bgra[i + 1];
				rgba[i + 2] = bgra[i];
				rgba[i + 3] = bgra[i + 3];
			}
			return rgba;
		}
		public (int width, int height, byte[] rgba) GetPngInfo()
		{
			using (var stream = File.OpenRead(filepath))
			{
				using (var bitmap = SKBitmap.Decode(stream))
				{
					int width = bitmap.Width;
					int height = bitmap.Height;
					var data = bitmap.Bytes;
					byte[] rgba = new byte[data.Length];
					Array.Copy(data, rgba, data.Length);
					return (width, height, ConvertBGRAtoRGBA(rgba));
				}
			}
		}
	}
	public class Font
	{
		public string input, output, JSONpath;
		public Font(string input)
		{
			this.input = input;
		}
		public Font(string input, string output)
		{
			this.input = input;
			this.output = output;
		}
		public Font(string output, string JSONpath, string input)
		{
			this.output = output;
			this.JSONpath = JSONpath;
			this.input = input;
		}

		public void XNBConversFont()
		{
			var WR = new UnpackXNB(input).ReadFont();
			var FC = new Font_C();
			var font = new Font_JSON();
			string filepath_png = Path.GetDirectoryName(input) + @"\" + Path.GetFileNameWithoutExtension(input) + @".png";
			string filepath_json = Path.GetDirectoryName(input) + @"\" + Path.GetFileNameWithoutExtension(input) + @".json";
			//hander
			FC.hander.target = WR.Platform;
			FC.hander.formatVersion = WR.Bit_Verson;
			FC.hander.hidef = false;
			FC.hander.compressed = false;
			//readers
			FC.readers = new Readers[WR.Verson.Length];
			for (int s = 0; s < WR.Verson.Length; s++)
			{
				FC.readers[s].type = new string(WR.Type_count[s]);
				FC.readers[s].verson = WR.Verson[s];
			}
			//content
			FC.content.texture.export = (Path.GetFileNameWithoutExtension(input) + ".png");
			FC.content.texture.format = (int)WR.Format;
			FC.content.glyphs = new Glyphs[WR.Glyphs.Size];
			for (int a = 0; a < WR.Glyphs.Size; a++)
			{
				FC.content.glyphs[a].x = WR.Glyphs.data[a].x;
				FC.content.glyphs[a].y = WR.Glyphs.data[a].y;
				FC.content.glyphs[a].width = WR.Glyphs.data[a].width;
				FC.content.glyphs[a].height = WR.Glyphs.data[a].height;
			}
			FC.content.cropping = new Cropping[WR.Cropping.Size];
			for (int a = 0; a < WR.Cropping.Size; a++)
			{
				FC.content.cropping[a].x = WR.Cropping.data[a].x;
				FC.content.cropping[a].y = WR.Cropping.data[a].y;
				FC.content.cropping[a].width = WR.Cropping.data[a].width;
				FC.content.cropping[a].height = WR.Cropping.data[a].height;
			}
			FC.content.characterMap = new char[WR.Character_map.Size];
			for (int a = 0; a < WR.Character_map.Size; a++)
			{
				FC.content.characterMap[a] = WR.Character_map.data[a];
			}
			FC.content.verticalLineSpacing = WR.Vertical_line_spacing;
			FC.content.horizontalSpacing = WR.Horizontal_spacing;
			FC.content.kerning = new Kerning[WR.Kerning.Size];
			for (int a = 0; a < WR.Kerning.Size; a++)
			{
				FC.content.kerning[a].x = WR.Kerning.data[a].x;
				FC.content.kerning[a].y = WR.Kerning.data[a].y;
				FC.content.kerning[a].z = WR.Kerning.data[a].z;
			}
			font.hander = FC.hander;
			font.readers = new Readers[FC.readers.Length];
			font.readers = FC.readers;
			font.content = FC.content;
			File.Create(filepath_json);
			var i = JsonConvert.SerializeObject(font);
			File.WriteAllText(filepath_json, i);
			new Png(filepath_png).CreatePng((int)WR.Width, (int)WR.Height, WR.Data);
		}
		public void FontConverXNB()
		{
			if (!File.Exists(JSONpath))
			{
				return;
			}
			Font_JSON json = JsonConvert.DeserializeObject<Font_JSON>(File.ReadAllText(JSONpath));
			var FC = new Font_C();
			FC.hander = json.hander;
			FC.readers = json.readers;
			FC.content = json.content;
			var SDF = new XNBFILE.Font();
			SDF.Platform = FC.hander.target;
			SDF.Bit_Verson = (byte)FC.hander.formatVersion;
			SDF.Flag_bit = (FC.hander.hidef) ? (byte)0 : (byte)Convert.ToInt32(FC.hander.hidef);
			//FC.hander.compressed = false;//我不会，别问(I don't know/////////)
			SDF.count = (byte)FC.readers.Length;
			SDF.string_length = new byte[SDF.count];
			SDF.Constant = new byte[SDF.count];
			SDF.Type_count = new string[SDF.count];
			SDF.Verson = new int[SDF.count];
			for (int a = 0; a < SDF.count; a++)
			{
				SDF.string_length[a] = (byte)FC.readers[a].type.Length;
				SDF.Constant[a] = (byte)1;
				SDF.Type_count[a] = FC.readers[a].type;
				SDF.Verson[a] = (byte)FC.readers[a].verson;
			}
			SDF.Format = (uint)FC.content.texture.format;
			SDF.Glyphs.Count = (byte)3;
			SDF.Glyphs.Size = (uint)FC.content.glyphs.Length;
			SDF.Glyphs.data = new Rectangle[FC.content.glyphs.Length];
			for (int a = 0; a < FC.content.glyphs.Length; a++)
			{
				SDF.Glyphs.data[a].x = FC.content.glyphs[a].x;
				SDF.Glyphs.data[a].y = FC.content.glyphs[a].y;
				SDF.Glyphs.data[a].width = FC.content.glyphs[a].width;
				SDF.Glyphs.data[a].height = FC.content.glyphs[a].height;
			}
			SDF.Cropping.Count = (byte)3;
			SDF.Cropping.Size = (uint)FC.content.cropping.Length;
			SDF.Cropping.data = new Rectangle[FC.content.cropping.Length];
			for (int a = 0; a < FC.content.cropping.Length; a++)
			{
				SDF.Cropping.data[a].x = FC.content.cropping[a].x;
				SDF.Cropping.data[a].y = FC.content.cropping[a].y;
				SDF.Cropping.data[a].width = FC.content.cropping[a].width;
				SDF.Cropping.data[a].height = FC.content.cropping[a].height;
			}
			SDF.Character_map.Count = (byte)5;
			SDF.Character_map.Size = (uint)FC.content.characterMap.Length;
			SDF.Character_map.data = new char[FC.content.characterMap.Length];
			for (int a = 0; a < FC.content.characterMap.Length; a++)
			{
				SDF.Character_map.data[a] = FC.content.characterMap[a];
			}
			SDF.Vertical_line_spacing = FC.content.verticalLineSpacing;
			SDF.Horizontal_spacing = FC.content.horizontalSpacing;
			SDF.Kerning.Count = (byte)7;
			SDF.Kerning.Size = (uint)FC.content.kerning.Length;
			SDF.Kerning.data = new Vector3[FC.content.kerning.Length];
			for (int a = 0; a < FC.content.kerning.Length; a++)
			{
				SDF.Kerning.data[a].x = FC.content.kerning[a].x;
				SDF.Kerning.data[a].y = FC.content.kerning[a].y;
				SDF.Kerning.data[a].z = FC.content.kerning[a].z;
			}
			SDF.Default_character = (byte)0;
			var img = new Png(input).GetPngInfo();
			var xfont = new packXNB(output, SDF);
			xfont.Font_Write(img.rgba, (uint)img.width, (uint)img.height);
		}
	}
}

