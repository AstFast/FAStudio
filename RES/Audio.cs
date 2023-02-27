using FAStudio.RES;
using System.Text;

namespace FA_Audio
{
	public class WAVERead
	{
		BinaryReader br;
		Audio.WAVE wave = new();
		public WAVERead(string input)
		{
			this.br = new BinaryReader(new FileStream(input, FileMode.Open));
		}
		~WAVERead()
		{
			if (br != null)
			{
				br.Close();
			}
		}
		public void RIFF()
		{
			wave.riff.ID = br.ReadChars(4);
			wave.riff.Size = br.ReadUInt32();//Size_decoder
			wave.riff.WAVE = br.ReadChars(4);
		}
		public void FORMAT()
		{
			wave.fmt.ID = br.ReadChars(4);
			wave.fmt.Size = br.ReadUInt32();
			wave.fmt.fmt.Code = br.ReadUInt16();
			wave.fmt.fmt.Channels = br.ReadUInt16();
			wave.fmt.fmt.Sample = br.ReadUInt32();
			wave.fmt.fmt.Rate = br.ReadUInt32();
			wave.fmt.fmt.Block = br.ReadUInt16();
			wave.fmt.fmt.Sample_bit = br.ReadUInt16();
			if ((int)wave.fmt.Size != 16)
			{
				wave.fmt.fmt.Other = br.ReadBytes((int)wave.fmt.Size - 2 - 2 - 4 - 4 - 2 - 2);
			}
		}
		public void IDAT()
		{
			wave.data.ID = br.ReadChars(4);
			wave.data.Size = br.ReadUInt32();
			wave.data.data = br.ReadBytes((int)wave.data.Size);
		}
		public void INAM()
		{
			wave.list.inam.ID = br.ReadChars(4);
			wave.list.inam.Size = br.ReadUInt32();
			wave.list.inam.ListData = br.ReadChars((int)wave.list.inam.Size);
			wave.list.inam.padding = br.ReadChar();
		}
		public void LIST()
		{
			wave.list.ID = br.ReadChars(4);
			wave.list.Size = br.ReadUInt32();
		}
		public void ID3()
		{

		}
		private string OEX()
		{
			Char[] i = br.ReadChars(4);
			br.BaseStream.Seek(-4, SeekOrigin.Current);
			return i.ToString();
		}
		public void Read()
		{
			RIFF();
			FORMAT();
			IDAT();
			/*
			while (OEX() == null)
			{
				if (OEX() != null)
				{
					switch (OEX())
					{
						case "RIFF":
							RIFF();
							break;
						case "fmt ":
							FORMAT();
							break;
						case "data":
							IDAT();
							break;
						case "LIST":
							try
							{
								if ((wave.riff.Size - wave.fmt.Size - wave.data.Size) != 0)
								{
									LIST();
									if (OEX() == "INAM")
									{
										INAM();
									}
									else
									{
										return;
									}
								}
							}
							catch
							{
								return;
							}
							break;
						default:
							break;
					}
				}
			}
			*/
		}
		public byte[] CharsTobytes(char[] chars)
		{
			return Encoding.ASCII.GetBytes(chars);
		}
		public Stream BytesToStream(byte[] bytes)
		{
			Stream stream = new MemoryStream(bytes);
			return stream;
		}
		public byte[] StreamToBytes(Stream stream)
		{
			byte[] bytes = new byte[stream.Length];
			stream.Read(bytes, 0, bytes.Length);
			stream.Seek(0, SeekOrigin.Begin);
			return bytes;
		}
		public byte[] IntToBitConverter(int num)
		{
			byte[] bytes = BitConverter.GetBytes(num);
			return bytes;
		}
		/*
		private byte[] Cb(byte[] a, byte[] b)
		{
			return a.Concat(b).ToArray();
		}
		*/
		private void Cb(byte[] a, byte[] b)
		{

			Array.Copy(a, 0, b, 0, b.Length);
		}
		public XNBFILE.Sound WAVEConverSound()
		{
			XNBFILE.Sound sound = new();
			//byte[] format = new byte[wave.fmt.Size];
			//var bry = new BinaryReader(BytesToStream(sound.Format));
			//foreach (UInt32 i in (, , , , , , , ))
			//byte[] Ia = BitConverter.GetBytes((CharsTobytes(wave.fmt.ID);
			//format
			/*
			var bwy = new BinaryWriter(new MemoryStream());
			
			bwy.Write(wave.fmt.ID);
			bwy.Write(wave.fmt.Size);
			bwy.Write(wave.fmt.fmt.Code);
			bwy.Write(wave.fmt.fmt.Channels);
			bwy.Write(wave.fmt.fmt.Sample);
			bwy.Write(wave.fmt.fmt.Rate);
			bwy.Write(wave.fmt.fmt.Block);
			bwy.Write(wave.fmt.fmt.Sample_bit);
			bwy.Write(wave.fmt.fmt.Other);
			*/
			sound.Format_Size = wave.fmt.Size;
			byte[] i = CharsTobytes(wave.fmt.ID);
			//sound.Format = Cb(CharsTobytes(wave.fmt.ID), Cb(IntToBitConverter((int)wave.fmt.Size),Cb(BitConverter.GetBytes(wave.fmt.fmt.Code),Cb(BitConverter.GetBytes(wave.fmt.fmt.Channels),Cb(BitConverter.GetBytes(wave.fmt.fmt.Sample),Cb(BitConverter.GetBytes(wave.fmt.fmt.Rate),Cb(BitConverter.GetBytes(wave.fmt.fmt.Block), (wave.fmt.fmt.Other != null)?Cb(BitConverter.GetBytes(wave.fmt.fmt.Sample_bit), wave.fmt.fmt.Other): BitConverter.GetBytes(wave.fmt.fmt.Sample_bit))))))));
			Cb(i, IntToBitConverter((int)wave.fmt.Size));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Code));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Channels));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Sample));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Rate));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Block));
			Cb(i, BitConverter.GetBytes(wave.fmt.fmt.Sample_bit));
			if (wave.fmt.fmt.Other != null)
			{
				Cb(i, wave.fmt.fmt.Other);
			}
			sound.Format = i;
			sound.Data_Size = wave.data.Size;
			sound.data = wave.data.data;
			sound.Start = 0;
			sound.Start_Length = (int)wave.data.Size / ((int)wave.fmt.fmt.Channels * 2);//采样点数
																						//rate = ((int)wave.fmt.fmt.Channels * (int)wave.fmt.fmt.Sample_bit * (int)wave.fmt.fmt.Sample) / 8;
																						//sound.MS_Time = ((int)wave.data.Size * 8)/((int)wave.fmt.fmt.Channels * (int)wave.fmt.fmt.Sample_bit * (int)wave.fmt.fmt.Sample);
			sound.MS_Time = (int)wave.data.Size / (int)wave.fmt.fmt.Rate;//跟上一行结果一样
			return sound;
		}
		public Audio.WAVE Return() { return wave; }
	}
	public class Sound
	{

		/*
		public void Wave(string filepath)
		{

			//AdpcmWaveFormat
			//WaveFileChunkReader
			
			//var i = waveFileReader.Read();
		}

		public void WMA()
		{
			//var tag = File.OpenRead
			//var file = TagLib.Asf.File.Create(filepath);

		
		*/
	}



}
