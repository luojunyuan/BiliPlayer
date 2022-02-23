using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation;

internal abstract class RangeTimeLine<T> : AnimationTimeline
{
	public override Type TargetPropertyType => typeof(T);

	public RangeTimeLine(TimeSpan start, TimeSpan end)
	{
		base.BeginTime = start;
		base.Duration = end - start;
	}

	protected override Freezable CreateInstanceCore()
	{
		return this;
	}

	public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
	{
		return GetCurrentValue(animationClock);
	}

	protected abstract T GetCurrentValue(AnimationClock animationClock);
}
