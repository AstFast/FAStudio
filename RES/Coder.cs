using NAudio.Wave;

namespace FAStudio.RES
{
	public class MSADPCMDecoder
	{
		private WaveFormat waveFormat;
		private int blockAlign;
		private int samplesPerBlock;
		private int coef1;
		private int coef2;
		private int[] predictor;
		private int[] delta;
		private int[] sample1;
		private int[] sample2;

		public MSADPCMDecoder(WaveFormat waveFormat)
		{
			this.waveFormat = waveFormat;
			this.blockAlign = waveFormat.BlockAlign;
			this.samplesPerBlock = (blockAlign - 7) * 2;
		}

		public byte[] Decode(byte[] audioData)
		{
			// Read the MSADPCM header
			ReadHeader(audioData);

			// Decode the audio data
			byte[] decodedData = new byte[audioData.Length * 2];
			int decodedIndex = 0;
			int audioIndex = 7;
			while (audioIndex < audioData.Length)
			{
				// Read the predictor and delta values
				predictor[0] = audioData[audioIndex++];
				delta[0] = BitConverter.ToInt16(audioData, audioIndex);
				audioIndex += 2;
				predictor[1] = audioData[audioIndex++];
				delta[1] = BitConverter.ToInt16(audioData, audioIndex);
				audioIndex += 2;

				// Decode the samples
				for (int i = 0; i < samplesPerBlock; i++)
				{
					int sample = audioData[audioIndex++];
					int nibble = sample & 0x0F;
					int decodedSample = DecodeNibble(nibble, 0);
					decodedData[decodedIndex++] = (byte)(decodedSample & 0xFF);
					decodedData[decodedIndex++] = (byte)((decodedSample >> 8) & 0xFF);

					nibble = (sample >> 4) & 0x0F;
					decodedSample = DecodeNibble(nibble, 1);
					decodedData[decodedIndex++] = (byte)(decodedSample & 0xFF);
					decodedData[decodedIndex++] = (byte)((decodedSample >> 8) & 0xFF);
				}
			}

			return decodedData;
		}

		private void ReadHeader(byte[] audioData)
		{
			// Read the MSADPCM header
			coef1 = BitConverter.ToInt16(audioData, 0);
			coef2 = BitConverter.ToInt16(audioData, 2);
			predictor = new int[2];
			delta = new int[2];
			sample1 = new int[2];
			sample2 = new int[2];
			sample1[0] = BitConverter.ToInt16(audioData, 4);
			sample2[0] = BitConverter.ToInt16(audioData, 6);
		}

		private int DecodeNibble(int nibble, int channel)
		{
			int predictor = this.predictor[channel];
			int delta = this.delta[channel];
			int sample1 = this.sample1[channel];
			int sample2 = this.sample2[channel];

			int decodedSample = (nibble * delta) + ((sample1 * coef1) + (sample2 * coef2)) / 256;
			decodedSample = Math.Max(Math.Min(decodedSample, 32767), -32768);

			this.predictor[channel] = decodedSample;
			this.delta[channel] = (delta * AdaptationTable[nibble]) / 256;
			this.sample2[channel] = sample1;
			this.sample1[channel] = decodedSample;

			return decodedSample;
		}

		private static readonly int[] AdaptationTable =
		{
		230, 230, 230, 230, 307, 409, 512, 614,
		768, 614, 512, 409, 307, 230, 230, 230
		};
	}
}
