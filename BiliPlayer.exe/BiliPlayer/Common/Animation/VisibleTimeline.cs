using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation
{
	// Token: 0x0200003E RID: 62
	internal class VisibleTimeline : RangeTimeLine<bool>
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00004E82 File Offset: 0x00003082
		public VisibleTimeline(TimeSpan start, TimeSpan end) : base(start, end + VisibleTimeline.ExtraTime)
		{
			this._start = start;
			this._end = end;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004EA4 File Offset: 0x000030A4
		protected override bool GetCurrentValue(AnimationClock animationClock)
		{
			Duration duration = base.Duration;
			TimeSpan? currentTime = animationClock.CurrentTime;
			return duration - ((currentTime != null) ? new Duration?(currentTime.GetValueOrDefault()) : null) > VisibleTimeline.ExtraTime;
		}

		// Token: 0x04000077 RID: 119
		private TimeSpan _start;

		// Token: 0x04000078 RID: 120
		private TimeSpan _end;

		// Token: 0x04000079 RID: 121
		private static readonly TimeSpan ExtraTime = TimeSpan.FromSeconds(0.1);
	}
}
