using System;
using System.Diagnostics;
using System.Windows;
using BiliPlayer.Common.Media;
using BiliPlayer.Common.MVVM;
using BiliPlayer.View.Setting;
using BiliPlayer.View.Subtitles;
using Microsoft.Win32;

namespace BiliPlayer.View
{
	// Token: 0x0200000F RID: 15
	internal class BulletCurtainCommands : MediaCommands
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A86 File Offset: 0x00000C86
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A8E File Offset: 0x00000C8E
		public DelegateCommand HideTitle { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A97 File Offset: 0x00000C97
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A9F File Offset: 0x00000C9F
		public DelegateCommand OpenOnlineTitle { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public DelegateCommand Setting { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002AB9 File Offset: 0x00000CB9
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002AC1 File Offset: 0x00000CC1
		public DelegateCommand Help { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002ACC File Offset: 0x00000CCC
		public BulletCurtainCommands(BulletCurtainController controller) : base(controller)
		{
			this._controller = controller;
			base.Open = new DelegateCommand(new Action<object>(this.onOpen), null);
			this.HideTitle = new DelegateCommand(new Action<object>(this.onHideTitle), null);
			this.Setting = new DelegateCommand(new Action<object>(this.onSetting), null);
			this.Help = new DelegateCommand(new Action<object>(this.onHelp), null);
			this.OpenOnlineTitle = new DelegateCommand(new Action<object>(this.onOpenOnlineTitle), null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B60 File Offset: 0x00000D60
		private void onOpen(object para)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = true
			};
			if (openFileDialog.ShowDialog() != true)
			{
				return;
			}
			this._controller.Open(openFileDialog.FileNames);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BAE File Offset: 0x00000DAE
		private void onHideTitle(object para)
		{
			this._controller.ShowTitle = !this._controller.ShowTitle;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BC9 File Offset: 0x00000DC9
		private void onHelp(object para)
		{
			Process.Start("http://www.cnblogs.com/TianFang/p/4115773.html");
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BD6 File Offset: 0x00000DD6
		private void onSetting(object para)
		{
			new SettingWindow
			{
				Owner = Application.Current.MainWindow
			}.Show();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BF2 File Offset: 0x00000DF2
		private void onOpenOnlineTitle(object para)
		{
			new SarchCurtain
			{
				Owner = Application.Current.MainWindow,
				DataContext = this._controller
			}.ShowDialog();
		}

		// Token: 0x04000019 RID: 25
		private BulletCurtainController _controller;
	}
}
