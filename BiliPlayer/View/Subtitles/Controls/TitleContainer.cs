using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;
using BiliPlayer.Common.Controls;
using BiliPlayer.Util;
using BiliPlayer.View.Setting;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x02000017 RID: 23
	internal class TitleContainer : VisualContainer<TitleVisual>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003288 File Offset: 0x00001488
		public TitleContainer()
		{
			base.IsHitTestVisible = false;
			this.SetBinding(this.TitleStyleProperty, "SettingId", SettingData.Instance, BindingMode.Default, 0);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000032EC File Offset: 0x000014EC
		public ParallelTimeline CreateTimelineGroup()
		{
			ParallelTimeline timelineGroup = new ParallelTimeline();
			base.Items.ToList().ForEach(delegate(TitleVisual i)
			{
				timelineGroup.Children.Add(i.Animation.VisibleTimeLine);
				timelineGroup.Children.Add(i.Animation.ProgressTimeLine);
			});
			return timelineGroup;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003327 File Offset: 0x00001527
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);
			base.Items.OfType<FixedTitle>().ToList().ForEach(delegate(FixedTitle i)
			{
				i.ReRender();
			});
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000335F File Offset: 0x0000155F
		private void reRender()
		{
			base.Items.ToList().ForEach(delegate(TitleVisual i)
			{
				i.ReRender();
			});
		}

		// Token: 0x04000037 RID: 55
		public DependencyProperty TitleStyleProperty = Dependency.RegistProperty<TitleContainer>("TitleStyle", delegate(TitleContainer i)
		{
			i.reRender();
		});
	}
}
