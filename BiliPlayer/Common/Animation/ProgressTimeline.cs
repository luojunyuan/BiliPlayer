using System;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation;

internal class ProgressTimeline : RangeTimeLine<double>
{
	public ProgressTimeline(TimeSpan start, TimeSpan end)
		: base(start, end)
	{
	}

	protected override double GetCurrentValue(AnimationClock animationClock)
	{
		return animationClock.CurrentProgress.Value;
	}
}
