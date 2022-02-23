using System;
using System.Windows;

namespace BiliPlayer.View.Subtitles.Controls;

internal class ScrollTitle : TitleVisual
{
	private Lazy<double> _yOffset;

	private int _offset;

	public ScrollTitle(TitleInfo title)
		: base(title)
	{
		_offset = TitleVisual._rnd.Next(0, 8) * 4;
		_yOffset = new Lazy<double>(getYOffset);
	}

	protected override void onProgressChanged(double progress, Rect bounds)
	{
		base.Offset = new Vector(0.0 - bounds.Width + (1.0 - progress) * 2500.0, _yOffset.Value);
	}

	protected override void onReRender()
	{
		_yOffset = new Lazy<double>(getYOffset);
		render();
	}

	private double getYOffset()
	{
		return (double)base.DataContext.ChannelNo * base.fontHeight + (double)_offset;
	}
}
