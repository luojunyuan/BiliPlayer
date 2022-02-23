using System;
using System.Windows.Media;
using BiliPlayer.Common.Data;

namespace BiliPlayer.View.Subtitles;

internal class TitleInfo
{
	public string Text { get; set; }

	public TimeSpan Start { get; set; }

	public TimeSpan End { get; set; }

	public Brush Stroke { get; set; }

	public Brush Fill { get; set; }

	public bool IsDarkColor { get; set; }

	public int ChannelNo { get; set; }

	public ScrollMode ScrollMode { get; set; }
}
