using System;
using System.Windows;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x0200001A RID: 26
	internal class ScrollTitle : TitleVisual
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003797 File Offset: 0x00001997
		public ScrollTitle(TitleInfo title) : base(title)
		{
			this._offset = TitleVisual._rnd.Next(0, 8) * 4;
			this._yOffset = new Lazy<double>(new Func<double>(this.getYOffset));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000037CB File Offset: 0x000019CB
		protected override void onProgressChanged(double progress, Rect bounds)
		{
			base.Offset = new Vector(-bounds.Width + (1.0 - progress) * 2500.0, this._yOffset.Value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003801 File Offset: 0x00001A01
		protected override void onReRender()
		{
			this._yOffset = new Lazy<double>(new Func<double>(this.getYOffset));
			this.render();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003820 File Offset: 0x00001A20
		private double getYOffset()
		{
			return (double)base.DataContext.ChannelNo * base.fontHeight + (double)this._offset;
		}

		// Token: 0x04000040 RID: 64
		private Lazy<double> _yOffset;

		// Token: 0x04000041 RID: 65
		private int _offset;
	}
}
