using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using BiliPlayer.Common.Media;
using BiliPlayer.DataSource;
using BiliPlayer.View.Setting;
using BiliPlayer.View.Subtitles;

namespace BiliPlayer.View;

[Export]
internal class BulletCurtainController : MediaController
{
	[Import]
	private SubTitleCanvas _canvas;

	private bool _showTitle = true;

	private string _message;

	private string _lastSubTitle;

	public SettingData SettingData { get; }

	public BulletCurtainCommands Commands { get; }

	public bool ShowTitle
	{
		get
		{
			return _showTitle;
		}
		set
		{
			_showTitle = value;
			onPropertyChanged("ShowTitle");
		}
	}

	public string Message
	{
		get
		{
			return _message;
		}
		set
		{
			_message = value;
			onPropertyChanged("Message");
		}
	}

	[ImportingConstructor]
	public BulletCurtainController(MediaElement mediaElement)
		: base(mediaElement)
	{
		Commands = new BulletCurtainCommands(this);
		SettingData = SettingData.Instance;
	}

	public void Open(params string[] files)
	{
		string subtitle = files.Where((string i) => i != null).FirstOrDefault((string i) => i.EndsWith(".xml"));
		string media = files.Where((string i) => i != null).FirstOrDefault((string i) => !i.EndsWith(".xml"));
		open(media, subtitle);
	}

	public async void open(string media, string subtitle)
	{
		subtitle = subtitle ?? _lastSubTitle;
		_lastSubTitle = subtitle;
		TimeSpan currentPosition = CurrentTime ?? TimeSpan.Zero;
		Stop();
		IEnumerable<TitleInfo> titles = TitleParser.Parse(subtitle);
		TimelineGroup.Children.Clear();
		TimelineGroup.Children.Add(_mediaTimeline);
		TimelineGroup.Children.Add(_canvas.Init(titles));
		if (media != null)
		{
			bool num = Source == null;
			Source = new Uri(media);
			if (num)
			{
				Begin();
				await Task.Delay(500);
				Stop();
				await Task.Delay(500);
			}
		}
		Begin();
		if (media == null)
		{
			Seek(currentPosition);
		}
	}
}
