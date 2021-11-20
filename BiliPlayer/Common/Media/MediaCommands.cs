using System;
using BiliPlayer.Common.MVVM;
using Microsoft.Win32;

namespace BiliPlayer.Common.Media
{
	// Token: 0x0200003F RID: 63
	internal class MediaCommands : NotifyItem
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004F47 File Offset: 0x00003147
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004F4F File Offset: 0x0000314F
		public DelegateCommand TogglePlayPause { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004F58 File Offset: 0x00003158
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004F60 File Offset: 0x00003160
		public DelegateCommand Stop { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004F69 File Offset: 0x00003169
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00004F71 File Offset: 0x00003171
		public DelegateCommand FastForward { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004F7A File Offset: 0x0000317A
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00004F82 File Offset: 0x00003182
		public DelegateCommand Rewind { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004F8B File Offset: 0x0000318B
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00004F93 File Offset: 0x00003193
		public DelegateCommand Seek { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004F9C File Offset: 0x0000319C
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00004FA4 File Offset: 0x000031A4
		public DelegateCommand Open { get; protected set; }

		// Token: 0x0600015B RID: 347 RVA: 0x00004FB0 File Offset: 0x000031B0
		public MediaCommands(MediaController mediaController)
		{
			this.controller = mediaController;
			this.TogglePlayPause = new DelegateCommand(new Action<object>(this.onPlayOrPause), null);
			this.Stop = new DelegateCommand(new Action<object>(this.onStop), null);
			this.Seek = new DelegateCommand(new Action<object>(this.onSeek), null);
			this.FastForward = new DelegateCommand(delegate(object _)
			{
				this.goForward(TimeSpan.FromSeconds(30.0));
			}, null);
			this.Rewind = new DelegateCommand(delegate(object _)
			{
				this.goForward(TimeSpan.FromSeconds(-30.0));
			}, null);
			this.Open = new DelegateCommand(new Action<object>(this.onOpen), null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000505C File Offset: 0x0000325C
		private void onOpen(object para)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = false
			};
			if (openFileDialog.ShowDialog() != true)
			{
				return;
			}
			this.controller.Source = new Uri(openFileDialog.FileName);
			this.controller.Begin();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000050BC File Offset: 0x000032BC
		private void onPlayOrPause(object para)
		{
			switch (this.controller.State)
			{
			case MediaState.Stopped:
				this.controller.Begin();
				return;
			case MediaState.Playing:
				this.controller.Pause();
				return;
			case MediaState.Paused:
				this.controller.Resume();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000510B File Offset: 0x0000330B
		private void onStop(object para)
		{
			this.controller.Stop();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005118 File Offset: 0x00003318
		private void onSeek(object para)
		{
			TimeSpan offset = (TimeSpan)para;
			this.controller.Seek(offset);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005138 File Offset: 0x00003338
		private void goForward(TimeSpan timeSpan)
		{
			TimeSpan? timeSpan2 = this.controller.CurrentTime + timeSpan;
			if (timeSpan2 > this.controller.Duration)
			{
				timeSpan2 = this.controller.Duration;
			}
			if (timeSpan2 < TimeSpan.Zero)
			{
				timeSpan2 = new TimeSpan?(TimeSpan.Zero);
			}
			if (timeSpan2 == null)
			{
				return;
			}
			this.onSeek(timeSpan2);
		}

		// Token: 0x04000080 RID: 128
		protected MediaController controller;
	}
}
