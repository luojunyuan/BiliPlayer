using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace BiliPlayer.View.Player
{
	// Token: 0x02000023 RID: 35
	internal class MediaPlayerHost : ContentControl, IViewPart
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00003D69 File Offset: 0x00001F69
		public void Init(Panel container)
		{
			base.Content = this._mediaElement;
		}

		// Token: 0x04000055 RID: 85
		[Import]
		private MediaElement _mediaElement;
	}
}
