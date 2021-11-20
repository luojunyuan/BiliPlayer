using System;
using System.Windows;
using BiliPlayer.Common.Data;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x0200001B RID: 27
	internal class FixedTitle : TitleVisual
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000383D File Offset: 0x00001A3D
		public FixedTitle(TitleInfo title) : base(title)
		{
			this._yOffset = new Lazy<double>(new Func<double>(this.getYOffset));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003860 File Offset: 0x00001A60
		protected override void onProgressChanged(double progress, Rect bounds)
		{
			if (this._yOffset.IsValueCreated)
			{
				return;
			}
			TitleContainer titleContainer = this.FindVisualParent<TitleContainer>();
			double num = this._yOffset.Value;
			if (base.DataContext.ScrollMode == ScrollMode.Bottom)
			{
				num = -num + titleContainer.RenderSize.Height - bounds.Height;
			}
			base.Offset = new Vector((titleContainer.RenderSize.Width - bounds.Width) / 2.0, num);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000038E2 File Offset: 0x00001AE2
		protected override void onReRender()
		{
			this._yOffset = new Lazy<double>(new Func<double>(this.getYOffset));
			this.render();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003901 File Offset: 0x00001B01
		private double getYOffset()
		{
			return (double)base.DataContext.ChannelNo * base.fontHeight;
		}

		// Token: 0x04000042 RID: 66
		private Lazy<double> _yOffset;
	}
}
