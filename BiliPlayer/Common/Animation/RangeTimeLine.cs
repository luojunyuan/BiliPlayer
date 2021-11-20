using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace BiliPlayer.Common.Animation
{
	// Token: 0x0200003D RID: 61
	internal abstract class RangeTimeLine<T> : AnimationTimeline
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00004E3F File Offset: 0x0000303F
		public RangeTimeLine(TimeSpan start, TimeSpan end)
		{
			base.BeginTime = new TimeSpan?(start);
			base.Duration = end - start;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004E65 File Offset: 0x00003065
		public override Type TargetPropertyType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000031C8 File Offset: 0x000013C8
		protected override Freezable CreateInstanceCore()
		{
			return this;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004E74 File Offset: 0x00003074
		public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			return this.GetCurrentValue(animationClock);
		}

		// Token: 0x0600014B RID: 331
		protected abstract T GetCurrentValue(AnimationClock animationClock);
	}
}
