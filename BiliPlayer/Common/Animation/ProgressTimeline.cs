using System;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation
{
	// Token: 0x0200003C RID: 60
	internal class ProgressTimeline : RangeTimeLine<double>
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00004E18 File Offset: 0x00003018
		public ProgressTimeline(TimeSpan start, TimeSpan end) : base(start, end)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004E24 File Offset: 0x00003024
		protected override double GetCurrentValue(AnimationClock animationClock)
		{
			return animationClock.CurrentProgress.Value;
		}
	}
}
