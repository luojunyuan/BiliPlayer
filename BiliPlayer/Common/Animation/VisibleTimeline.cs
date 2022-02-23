using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation;

internal class VisibleTimeline : RangeTimeLine<bool>
{
	private TimeSpan _start;

	private TimeSpan _end;

	private static readonly TimeSpan ExtraTime = TimeSpan.FromSeconds(0.1);

	public VisibleTimeline(TimeSpan start, TimeSpan end)
		: base(start, end + ExtraTime)
	{
		_start = start;
		_end = end;
	}

	protected override bool GetCurrentValue(AnimationClock animationClock)
	{
		Duration duration = base.Duration;
		Duration? duration2 = animationClock.CurrentTime;
		return duration - duration2 > ExtraTime;
	}
}
