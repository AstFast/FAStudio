namespace FAStudio.RES
{
	/*
	public class coder
	{
		private static uint Swap(uint le)
		{
			uint num2 = (le >> 8) & 0xff;
			uint num3 = (le >> 0x10) & 0xff;
			uint num4 = (le >> 0x18) & 0xff;
			return (((((le & 0xff) * 0x100_0000) + (num2 * 0x1_0000)) + (num3 * 0x100)) + num4);
		}
		public static void Coder(string[] args)
		{
			byte[] buffer = new byte[0x4c];
			short[] numArray = new short[0x400];
			int[] numArray2 = new int[0x20];
			int[] numArray3 = new int[] { 0, 240, 460, 0x188 };
			int[] numArray5 = new int[4];
			numArray5[2] = -208;
			numArray5[3] = -220;
			if (args.Length > 1)
			{
				if (!File.Exists(args[0]))
				{
					return;
				}
				var wave = new WAVERead(args[0]);
				wave.Read();
				var waver = wave.Return();
				var Data_R = new BinaryReader(wave.BytesToStream(waver.data.data));
				int num = ((int)waver.data.Size / (int)waver.fmt.fmt.Channels) / 2;
				BinaryWriter bw;
				if (args.Length <= 1)
				{
					bw = new BinaryWriter(new FileStream(Path.GetFileNameWithoutExtension(args[0]) + ".sps", FileMode.Create));
				}
				else
				{
					try
					{
						bw = new(new FileStream(args[1], FileMode.Create));
					}
					catch
					{
						bw = new(new FileStream(args[1], FileMode.Open));
					}
				}
				bw.Write((byte)0x48);
				bw.Write((byte)0x00);
				bw.Write((byte)0x00);
				bw.Write((byte)0x0c);
				bw.Write((byte)0x14);
				if (waver.fmt.fmt.Channels == 1)
				{
					bw.Write((byte)0);
				}
				else
				{
					bw.Write((byte)4);
				}
				bw.Write((ushort)Swap(waver.fmt.fmt.Rate));
				byte[] temp = BitConverter.GetBytes((int)waver.data.Size / ((int)waver.fmt.fmt.Channels * 2));
				byte[] temp1 = new byte[3];
				for (int i=1; i<4; i++){temp1[i-1] = temp[i];}
				bw.Write((byte)0x40);
				bw.Write(temp1);
			}
		}
	}
	
	*/
}