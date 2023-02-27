namespace FAStudio.RES
{
	public class ReadTool
	{
		public static void ReadString()
		{
			return;
		}
	}

	public struct Rectangle
	{
		public int x;
		public int y;
		public int width;
		public int height;
	}
	public struct Vector3
	{
		public Single x;
		public Single y;
		public Single z;
	}

	#region ndsfile
	public static class NDSFILE
	{
		//模块结构
		public struct NUR
		{
			//Universal Read
			//通用读取
			public char[] ID;//4
			public UInt32 Constant;//4
			public UInt32 Size;//4
			public UInt16 Header_Size;//2
			public UInt16 Sections;//2
		}
		public struct BTAF
		{
			public char[] BTAF_ID;
			public UInt32 Section_Size;
			public UInt32 Num_File;
			public UInt32[] Data;//Start,End Offset
		}
		public struct BTNF
		{
			public char[] BTNF_ID;
			public UInt32 Section_Size2;
		}
		public struct ARCV_Data
		{
			public UInt32 Offset;
			public UInt32 Size;
			public UInt32 CRC;
		}
		public struct RAHC
		{
			public char[] RAHC_ID;//4
			public UInt32 Section_Size;//4
			public UInt16 Height;//2 0xFFFF?
			public UInt16 Width;//2 0xFFFF?
			public UInt32 Bit;//4
			public UInt16 U_Height;//2
			public UInt16 U_Width;//2
			public uint T_Flag;//1
			public uint P_Flag;//1
			public UInt16 UWI;//2//unknown
			public UInt32 data_size;//4
			public UInt32 data_offest;//4
			public byte[] data;
		}
		public struct SOCP
		{
			public char[] SOCP_ID;//4
			public UInt32 Section_Size2;//4
			public UInt32 Constant2;//4
			public UInt16 Char_Size;//2
			public UInt16 Char_Count;//2
		}
		public struct PLTT
		{
			public char[] PLTT_ID;//4
			public UInt32 Section_Size;
			public UInt16 Bit;//2
			public UInt16 UNKn;//2
			public UInt32 UnK;//4
			public UInt32 Memory_Size;//4
			public UInt32 Data_Offest;//4
		}
		public struct PCMP
		{
			public char[] PCMP_ID;//4
			public UInt32 Section_Size2;//4
			public UInt16 Palettes;//2
			public UInt32 Constant2;//6
			public UInt16[] Palette_Index;//Every 2//Simple ID, two characters each
		}
		public struct CEBK
		{
			public char[] CEBK_ID;//4
			public UInt32 Section_Size;//4
			public UInt16 Bank;//2//Number of frames
			public UInt16 E_Flag;//2
			public UInt32 Data_Offest;//4
									  //public UInt32 Flag;//4
									  //public UInt32 P_Data_Offest;//4
									  //public UInt64 Padding;//8
			public UInt32[] zero;//16(4+4+8)
			public UInt32[] Length;
			public UInt32[] Offset;
		}
		public struct LABL
		{
			public char[] LABL_ID;//4
			public UInt32 Section_Size2;//4
			public UInt32 UnKnown;//4
			public UInt32[] Name_Offest;//4
			public string[] Name;//offest
		}
		public struct UEXT
		{
			public char[] UEXT_ID;//4
			public UInt32 Section_Size3;//4
			public UInt32 UnKniwn_2;//4
		}
		public struct SCRN
		{
			public char[] SCRN_ID;//4
			public UInt32 Section_Size;//4
			public UInt16 Width;//2
			public UInt16 Heigth;//2
			public UInt16 Internal_Screen_Size;//2
			public UInt16 BG_Type;//2
			public UInt32 Data_Size;//4
			public UInt32[] Data;
		}
		public struct KNBA
		{
			public Char[] KNBA_ID;//4
			public UInt32 Section_Size;//4
			public UInt16 Bank;//2
			public UInt16 Total_Frames;//2
			public UInt32 Bank_Block_Offset;//4
			public UInt32 Header_Block_Offset;//4
			public UInt32 Data_Block_Offset;//4
			public UInt64 Padding;//8
		}
		public struct MCBK
		{
			public char[] MCBK_ID;//4
			public UInt32 Section_Size;//4
			public UInt16 Map_Banks;//2
			public UInt16 Constant2;//2
			public UInt32 Header_Offset;//4
			public UInt32 Parts_Offset;//4
			public UInt64 Padding;//8
		}
		//全局文件结构
		public struct NARC
		{

			public NUR Hander;
			//BTAF
			public BTAF btaf;
			//BTNF
			public BTNF btnf;
			//Root Folder
			public UInt32[] Folder;
		}
		public struct ARCV
		{
			public char[] ID;//4
			public UInt32 Files;//4
			public UInt32 Size;//4
			public ARCV_Data[] data;//offset,size,crc
		}


		public struct NCGR
		{
			//ncgr
			public NUR Hander;
			//RAHC
			public RAHC rahc;
			//SOCP
			public SOCP socp;
		}

		public struct NCLR
		{
			//nclr
			public NUR Hander;
			//PLTT
			public PLTT ptllltt;
			//PCMP
			public PCMP pcmp;
		}

		public struct NCER
		{
			//ncer
			public NUR Hander;
			//CEBK
			public CEBK cebk;
			//LABL
			public LABL labl;
			//UEXT
			public UEXT uext;
		}
		public struct NSCR
		{
			//nscr
			public NUR Hander;
			//SCRN
			public SCRN scrn;
		}
		public struct NMCR
		{
			//nmcr
			public NUR Hander;
			//MCBK
			public MCBK mcbk;
		}
		public struct NANR
		{
			//nanr
			public NUR Hander;
			//KNBA
			public KNBA knba;
			//LABL
			public LABL labl;
			//UEXT
			public UEXT uext;
		}
		public struct NMAR
		{
			//nmar
			public NUR Hander;
			//KNBA
			public KNBA knba;
			//LABL
			public LABL labl;
		}
	}
	#endregion
	#region xna(xnb)file
	public static class XNBFILE
	{
		public struct Model<T>
		{
			public byte Count;
			public UInt32 Size;
			public T[] data;
		}
		public struct Sound
		{
			public string name;
			//PCM_16 or MS-ADPCM
			public char[] ID;//3//XNA
			public byte Platform;//1//Plantform
								 //m-WindowsPhone
								 //w-Windows
								 //x-Xbox360
			public byte Bit_Verson;//1//05
			public byte Flag_bit;//1//0
			public UInt32 Size;//4
			public byte count;//1
			public byte string_length;//1
			public char[] Type_count;
			public Int32 Verson;//4
			public byte Unknown;//1//value:0
			public byte UnKnown;//1//1
			public UInt32 Format_Size;//4
			public byte[] Format;
			public UInt32 Data_Size;//4
			public byte[] data;
			public Int32 Start;//4
			public Int32 Start_Length;//4//It may be the total number of audio frames
			public Int32 MS_Time;//4//Unit: ms
		}
		public struct Image
		{
			public string name;
			public char[] ID;//3//XNA
			public byte Platform;//1
			public byte Bit_Verson;//1
			public byte Flag_bit;//1
			public UInt32 Size;//4
			public byte count;//1
			public byte string_length;//1
			public byte UNKnown;//1//1
			public char[] Type_count;
			public Int32 Verson;//4
			public byte Unknown;//1//value:0
			public byte UnKnown;//1//1
			public Int32 Surface_format;//4
			public UInt32 Width;//4
			public UInt32 Height;//4
			public UInt32 Mip_count;//4
			public UInt32 Data_Size;//4
			public byte[] Data;
		}
		public struct Music
		{
			public string _name;
			public char[] ID;//3//XNA
			public byte Platform;//1
			public byte Bit_Verson;//1
			public byte Flag_bit;//1
			public UInt32 Size;//4
			public byte count;//1
			public byte[] string_length;//1
			public char[] Type_count;
			public Int32[] Verson;//4
			public byte Unknown;//1//value:0
			public byte Sign;//1//1
			public byte name_length;//1
			public char[] name;
			public byte Sign2;//1
			public Int32 MS_Time;//2
		}
		public struct Font
		{
			public string name;
			public char[] ID;//3//XNA
			public char Platform;//1
			public byte Bit_Verson;//1
			public byte Flag_bit;//1
			public UInt32 Size;//4
			public byte count;//1
			public byte[] string_length;//1
			public byte[] Constant;//1//value:01
			public string[] Type_count;
			public Int32[] Verson;//4
			public byte[] constant;//3
			public UInt32 Format;//4
			public UInt32 Width;//4
			public UInt32 Height;//4
			public UInt32 Mip_count;//4
			public UInt32 Data_Size;//4
			public byte[] Data;
			//Glyphs
			public Model<Rectangle> Glyphs;
			//Cropping
			public Model<Rectangle> Cropping;
			//Character_map
			public Model<char> Character_map;
			//
			public Int32 Vertical_line_spacing;
			public Single Horizontal_spacing;
			//Kerning
			public Model<Vector3> Kerning;
			//
			public byte Default_character;
		}
	}
	#endregion
	public static class Audio
	{
		public struct WMA
		{
			public char[] name;
			public Int32 Time;
		}
		#region WAV<->
		public struct Chunk
		{
			struct Chunker
			{
				public UInt32 id;   // 块标志//4
				public UInt32 size; // 块大小//4
				public UInt16[] data; // 块数据//UInt8
			}
		}
		public struct RIFF
		{
			public char[] ID;//4
			public UInt32 Size;//4
			public char[] WAVE;//4

		}
		public struct Q_FMT
		{
			public ushort Code;//2
			public ushort Channels;//2
			public uint Sample;//4
			public uint Rate;//4
			public ushort Block;//2
			public ushort Sample_bit;//2
			public byte[] Other;
		}
		public struct FMT
		{
			public char[] ID;//4
			public UInt32 Size;//4
			public Q_FMT fmt;
		}
		public struct DATA
		{
			public char[] ID;//4
			public UInt32 Size;//4
			public byte[] data;
		}
		public struct INAM
		{
			public char[] ID;//4
			public UInt32 Size;//4
			public char[] ListData;
			public char padding;//uchar->byte//1
		}
		public struct LIST
		{
			public char[] ID;//4
			public UInt32 Size;//4
			public char[] Chunk_Type;//4
			public INAM inam;
		}
		public struct WAVE
		{
			public RIFF riff;
			public FMT fmt;
			public DATA data;
			public LIST list;
		}
		#endregion
	}
}
