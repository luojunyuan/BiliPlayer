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

namespace BiliPlayer.View
{
	// Token: 0x02000011 RID: 17
	[Export]
	internal class BulletCurtainController : MediaController
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002C1B File Offset: 0x00000E1B
		[ImportingConstructor]
		public BulletCurtainController(MediaElement mediaElement) : base(mediaElement)
		{
			this.Commands = new BulletCurtainCommands(this);
			this.SettingData = SettingData.Instance;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002C42 File Offset: 0x00000E42
		public SettingData SettingData { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002C4A File Offset: 0x00000E4A
		public BulletCurtainCommands Commands { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002C52 File Offset: 0x00000E52
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002C5A File Offset: 0x00000E5A
		public bool ShowTitle
		{
			get
			{
				return this._showTitle;
			}
			set
			{
				this._showTitle = value;
				base.onPropertyChanged("ShowTitle");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002C6E File Offset: 0x00000E6E
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002C76 File Offset: 0x00000E76
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
				base.onPropertyChanged("Message");
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void Open(params string[] files)
		{
			string subtitle = (from i in files
			where i != null
			select i).FirstOrDefault((string i) => i.EndsWith(".xml"));
			string media = (from i in files
			where i != null
			select i).FirstOrDefault((string i) => !i.EndsWith(".xml"));
			this.open(media, subtitle);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D38 File Offset: 0x00000F38
		public async void open(string media, string subtitle)
		{
			subtitle = (subtitle ?? this._lastSubTitle);
			this._lastSubTitle = subtitle;
			TimeSpan currentPosition = this.CurrentTime ?? TimeSpan.Zero;
			this.Stop();
			IEnumerable<TitleInfo> titles = TitleParser.Parse(subtitle);
			this.TimelineGroup.Children.Clear();
			this.TimelineGroup.Children.Add(this._mediaTimeline);
			this.TimelineGroup.Children.Add(this._canvas.Init(titles));
			if (media != null)
			{
				bool flag = this.Source == null;
				this.Source = new Uri(media);
				if (flag)
				{
					this.Begin();
					await Task.Delay(500);
					this.Stop();
					await Task.Delay(500);
				}
			}
			this.Begin();
			if (media == null)
			{
				this.Seek(currentPosition);
			}
		}

		// Token: 0x0400001A RID: 26
		[Import]
		private SubTitleCanvas _canvas;

		// Token: 0x0400001D RID: 29
		private bool _showTitle = true;

		// Token: 0x0400001E RID: 30
		private string _message;

		// Token: 0x0400001F RID: 31
		private string _lastSubTitle;
	}
}
