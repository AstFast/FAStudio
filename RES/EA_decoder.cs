namespace xas_decode
{
	//反编译某未知大佬的作品
	//Decompile a person's work
	internal class EA_M
	{
		private static uint Swap(uint le)
		{
			uint num2 = (le >> 8) & 0xff;
			uint num3 = (le >> 0x10) & 0xff;
			uint num4 = (le >> 0x18) & 0xff;
			return (((((le & 0xff) * 0x100_0000) + (num2 * 0x1_0000)) + (num3 * 0x100)) + num4);
		}
		private static void Unsupported()
		{
			Environment.Exit(1);
		}
		private static void Writewavheader(BinaryWriter bw, int channels, int samplerate, int bytes)
		{
			bw.Write('R');
			bw.Write('I');
			bw.Write('F');
			bw.Write('F');
			bw.Write((int)(bytes + 0x24));
			bw.Write('W');
			bw.Write('A');
			bw.Write('V');
			bw.Write('E');
			bw.Write('f');
			bw.Write('m');
			bw.Write('t');
			bw.Write(' ');
			bw.Write(0x10);
			bw.Write((short)1);
			bw.Write((short)channels);
			bw.Write(samplerate);
			bw.Write((int)((samplerate * channels) * 2));
			bw.Write((short)(channels * 2));
			bw.Write((short)0x10);
			bw.Write('d');
			bw.Write('a');
			bw.Write('t');
			bw.Write('a');
			bw.Write(bytes);
		}
		public static void decoder(string[] args)
		{
			byte[] buffer = new byte[0x4c];
			short[] numArray = new short[0x400];
			int[] numArray2 = new int[0x20];
			int[] numArray3 = new int[] { 0, 240, 460, 0x188 };
			int[] numArray5 = new int[4];
			numArray5[2] = -208;
			numArray5[3] = -220;
			int[] numArray4 = numArray5;
			int index = 0;
			int channels = 0;
			int samplerate = 0;
			int num7 = 0;
			if (args.Length >= 1)
			{
				if (!File.Exists(args[0]))
				{
					return;
				}
				FileStream input = new FileStream(args[0], FileMode.Open);
				BinaryReader reader = new BinaryReader(input);
				if (args.Length > 2)
				{
					input.Seek(Convert.ToInt64(args[2]), SeekOrigin.Begin);
				}
				while (true)
				{
					uint num10;
					uint num11;
					FileStream stream2;
					BinaryWriter writer;
					while (true)
					{
						int num8 = reader.ReadByte();
						if (num8 == 4)
						{
							channels = (reader.ReadByte() >> 2) + 1;
							if ((channels > 8) || (channels < 1))
							{
								return;
							}
							samplerate = (reader.ReadByte() * 0x100) + reader.ReadByte();
						}
						else if (num8 == 0x48)
						{
							input.Seek(-1L, SeekOrigin.Current);
							Swap(reader.ReadUInt32());
							num8 = reader.ReadByte();//sps->20(0x14)
							if ((num8 != 20) && (num8 != 0x12))
							{
								Unsupported();
							}
							channels = (reader.ReadByte() >> 2) + 1;
							if ((channels > 8) || (channels < 1))
							{
								return;
							}
							samplerate = (reader.ReadByte() * 0x100) + reader.ReadByte();
						}
						else
						{
							if (num8 == 1)
							{
								input.Seek(-1L, SeekOrigin.Current);
								if (reader.ReadUInt32() != 0x1001)
								{
									Unsupported();
								}
								while (true)
								{
									uint num9 = reader.ReadUInt32();
									if (num9 == 0xc00_0048)
									{
										input.Seek((long)(-4), SeekOrigin.Current);
										break;
									}
								}
								continue;
							}
							Unsupported();
						}
						num10 = reader.ReadUInt32();
						num11 = 0;
						if ((num10 & 0x20) == 0x20)
						{
							reader.ReadInt32();
						}
						if ((num10 & 0x60) == 0x60)
						{
							reader.ReadInt32();
						}
						num10 = Swap(num10) & 0xFFFFFFF;//num10->采样点数
						string str = "";
						if (num7 > 0)
						{
							str = "_" + num7;
						}
						stream2 = (args.Length <= 1) ? new FileStream(Path.GetFileNameWithoutExtension(args[0]) + str + ".wav", FileMode.Create) : new FileStream(args[1], FileMode.Create);
						writer = new BinaryWriter(stream2);
						Writewavheader(writer, channels, samplerate, (int)((num10 * channels) * 2));
						while (input.Position < input.Length)
						{
							uint num12 = Swap(reader.ReadUInt32());
							if ((num8 != 20) && (num8 != 0x12))
							{
								if ((num12 & 0x80000000) > 0)
								{
									num12 &= 0xFFFFFF;
								}
							}
							else
							{
								if ((num12 & 0xFF000000) == 0x45000000)
								{
									break;
								}
								num12 &= 0xFFFFFF;
							}
							uint num13 = Swap(reader.ReadUInt32());
							if ((num12 == 0) || (num13 == 0))
							{
								break;
							}
							num12 -= 8;
							num11 += num13;
							int num15 = (int)((num12 / 0x4c) / channels);
							if ((num8 == 20) && (((num15 * 0x4c) * channels) != num12))
							{
								//!
							}
							if (num8 == 0x12)
							{
								for (int i = 0; i < (num13 * channels); i++)
								{
									int num17 = input.ReadByte();
									int num18 = input.ReadByte();
									writer.Write((short)((num17 * 0x100) + num18));
								}
							}
							else
							{
								int num19 = 0;
								while (num19 < num15)
								{
									int num20 = 0;
									while (true)
									{
										if (num20 >= channels)
										{
											int num14 = (num13 < 0x80) ? ((int)num13) : 0x80;
											if (num13 < 0)
											{
												//!
											}
											num13 -= 0x80;
											int num24 = 0;
											while (true)
											{
												if (num24 >= (num14 * channels))
												{
													num19++;
													break;
												}
												writer.Write(numArray[num24]);
												num24++;
											}
											break;
										}
										input.Read(buffer, 0, 0x4c);
										int num21 = 0;
										while (true)
										{
											if (num21 >= 4)
											{
												num20++;
												break;
											}
											numArray2[0] = (short)((buffer[num21 * 4] & 240) | (buffer[(num21 * 4) + 1] << 8));
											numArray2[1] = (short)((buffer[(num21 * 4) + 2] & 240) | (buffer[(num21 * 4) + 3] << 8));
											index = buffer[num21 * 4] & 15;
											int num2 = buffer[(num21 * 4) + 2] & 15;
											int num22 = 2;
											while (true)
											{
												if (num22 >= 0x20)
												{
													int num23 = 0;
													while (true)
													{
														if (num23 >= 0x20)
														{
															num21++;
															break;
														}
														numArray[(((num21 * 0x20) + num23) * channels) + num20] = (short)numArray2[num23];
														num23++;
													}
													break;
												}
												int num4 = (buffer[(12 + num21) + (num22 * 2)] & 240) >> 4;
												if (num4 > 7)
												{
													num4 -= 0x10;
												}
												numArray2[num22] = ((((numArray2[num22 - 1] * numArray3[index]) + (numArray2[num22 - 2] * numArray4[index])) + (num4 << ((20 - num2) & 0x1f))) + 0x80) >> 8;
												if (numArray2[num22] > 0x7fff)
												{
													numArray2[num22] = 0x7fff;
												}
												else if (numArray2[num22] < -32_768)
												{
													numArray2[num22] = -32_768;
												}
												num4 = buffer[(12 + num21) + (num22 * 2)] & 15;
												if (num4 > 7)
												{
													num4 -= 0x10;
												}
												int num3 = (numArray2[num22] * numArray3[index]) + (numArray2[num22 - 1] * numArray4[index]);
												numArray2[num22 + 1] = ((num3 + (num4 << ((20 - num2) & 0x1f))) + 0x80) >> 8;
												if (numArray2[num22 + 1] > 0x7fff)
												{
													numArray2[num22 + 1] = 0x7fff;
												}
												else if (numArray2[num22 + 1] < -32_768)
												{
													numArray2[num22 + 1] = -32_768;
												}
												num22 += 2;
											}
										}
									}
								}
							}
						}
						break;
					}
					if (num10 != num11)
					{
					}
					writer.Close();
					stream2.Close();
					if ((args.Length >= 3) || (input.Position >= input.Length))
					{
						reader.Close();
						input.Close();
						return;
					}
					num7++;
				}
			}
		}
	}
}
