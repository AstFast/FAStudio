using FA_Audio;
using FAStudio.RES;
using xas_decode;

namespace FAStudio;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		/*
				if (count == 1)
					CounterBtn.Text = $"Clicked {count} time";
				else
					CounterBtn.Text = $"Clicked {count} times";

				SemanticScreenReader.Announce(CounterBtn.Text);
		*/
	}
	private bool _asdated = false;
	private async void LIST_Clicked(object sender, EventArgs e)
	{
		await ((Button)sender).RotateTo(_asdated ? 0 : -90);

		//LIST.Margin = new Thickness(0, 0, _asdated ? 0 : -100, 50);
		LIST_1.Animate<Thickness>("LIST",
			value =>
			{
				int faster = Convert.ToInt32(value * 10);
				var RigthMargin =
				!_asdated
				? (faster * 20) - 200
				: (faster * 20) * -2;

				return new Thickness(0, 0, RigthMargin, 50);
			},
			newThickness => LIST_1.Margin = newThickness,
			length: 250,
			finished: (_, __) => _asdated = !_asdated);
	}

	private async void WMAConverXNB(object sender, EventArgs e)
	{

		//string action = await Page.DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
		try
		{
			var wma = new packXNB(EnterInput.Text);
			wma.WMA_Write();
			Tips_ALL.Text = "写入完成";
		}
		catch
		{
			Tips_ALL.Text = "写入失败";
		}
	}

	private void XNBConverWAV(object sender, EventArgs e)
	{
		try
		{
			var sound = new UnpackXNB(EnterInput.Text);
			sound.ReadSound();
			var wav = new Wav(EnterOutput.Text, sound.Return_Sound());
			wav.WriteWav();
			Tips_ALL.Text = "写入完成，部分音频需要转码才能让大部分音频播放器播放";
		}
		catch
		{
			Tips_ALL.Text = "写入失败";
		}
	}
	private void WAVConverXNB(object sender, EventArgs e)
	{
		try
		{
			var i = new WAVERead(EnterInput.Text);
			i.Read();
			var a = i.Return();
			var b = new packXNB(EnterOutput.Text, a);
			b.WAV_Write();
			Tips_ALL.Text = "执行完成";
		}
		finally
		{
			Tips_ALL.Text = "执行错误";
		}
	}
	private void EAConverWAV(object sender, EventArgs e)
	{
		try
		{
			EA_M.decoder(new string[] { EnterInput.Text, EnterOutput.Text });
			Tips_ALL.Text = "执行完成";
		}
		catch
		{
			Tips_ALL.Text = "执行错误";
		}
	}

	private void XNBConverPNG(object sender, EventArgs e)
	{
		try
		{
			var img = new UnpackXNB(EnterInput.Text).ReadImage();
			var i = new Png(EnterOutput.Text);
			i.CreatePng((int)img.Width, (int)img.Height, i.ConvertRGBAtoBGRA(img.Data));
			Tips_ALL.Text = "执行成功";
		}
		catch
		{
			Tips_ALL.Text = "执行错误";
		}
	}

	private void PNGConverXNB(object sender, EventArgs e)
	{
		try
		{
			var pngr = new Png(EnterInput.Text);
			var cd = pngr.GetPngInfo();
			var png = new packXNB(EnterOutput.Text);
			png.PNG_Write(cd.rgba, (UInt32)cd.width, (UInt32)cd.height);
			Tips_ALL.Text = "执行完成";
		}
		catch
		{
			Tips_ALL.Text = "执行失败";
		}
	}

	private void XNBConverFont(object sender, EventArgs e)
	{

		try
		{
			var font = new RES.Font(EnterInput.Text);
			font.XNBConversFont();
			Tips_ALL.Text = "执行成功";
		}
		catch
		{
			Tips_ALL.Text = "执行失败";
		}

	}

	private void FontConverXNB(object sender, EventArgs e)
	{
		try
		{
			var i = new RES.Font(EnterOutput.Text, (Path.GetDirectoryName(EnterInput.Text) + @"\" + Path.GetFileNameWithoutExtension(EnterInput.Text) + @".json"), (Path.GetDirectoryName(EnterInput.Text) + @"\" + Path.GetFileNameWithoutExtension(EnterInput.Text) + @".png"));
			i.FontConverXNB();
			Tips_ALL.Text = "执行成功";
		}
		catch
		{
			Tips_ALL.Text = "执行失败";
		}
	}
	private void WAVConverEA(object sender, EventArgs e)
	{
		try
		{
		}
		catch
		{
			Tips_ALL.Text = "执行失败";
		}

	}
}

