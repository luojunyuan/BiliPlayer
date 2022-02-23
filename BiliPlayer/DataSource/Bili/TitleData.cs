using System;
using System.Windows.Media;
using BiliPlayer.Common.Data;

namespace BiliPlayer.DataSource.Bili;

internal class TitleData
{
	public string Content { get; set; }

	public Brush Foreground { get; set; }

	public ScrollMode ScrollMode { get; set; }

	public TimeSpan Start { get; set; }

	public TimeSpan End { get; set; }

	public bool IsDarkColor { get; set; }

	public int ChannelNo { get; set; }
}
