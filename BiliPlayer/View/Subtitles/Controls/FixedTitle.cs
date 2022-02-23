using System;
using System.Windows;
using BiliPlayer.Common.Data;

namespace BiliPlayer.View.Subtitles.Controls;

internal class FixedTitle : TitleVisual
{
	private Lazy<double> _yOffset;

	public FixedTitle(TitleInfo title)
		: base(title)
	{
		_yOffset = new Lazy<double>(getYOffset);
	}

	protected override void onProgressChanged(double progress, Rect bounds)
	{
		if (!_yOffset.IsValueCreated)
		{
			TitleContainer titleContainer = this.FindVisualParent<TitleContainer>();
			double num = _yOffset.Value;
			if (base.DataContext.ScrollMode == ScrollMode.Bottom)
			{
				num = 0.0 - num + titleContainer.RenderSize.Height - bounds.Height;
			}
			base.Offset = new Vector((titleContainer.RenderSize.Width - bounds.Width) / 2.0, num);
		}
	}

	protected override void onReRender()
	{
		_yOffset = new Lazy<double>(getYOffset);
		render();
	}

	private double getYOffset()
	{
		return (double)base.DataContext.ChannelNo * base.fontHeight;
	}
}
