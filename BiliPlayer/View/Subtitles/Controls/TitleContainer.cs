using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using BiliPlayer.Common.Controls;
using BiliPlayer.Util;
using BiliPlayer.View.Setting;

namespace BiliPlayer.View.Subtitles.Controls;

internal class TitleContainer : VisualContainer<TitleVisual>
{
	public DependencyProperty TitleStyleProperty = Dependency.RegistProperty("TitleStyle", delegate(TitleContainer i)
	{
		i.reRender();
	});

	public TitleContainer()
	{
		base.IsHitTestVisible = false;
		this.SetBinding(TitleStyleProperty, "SettingId", SettingData.Instance);
	}

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

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		base.Items.OfType<FixedTitle>().ToList().ForEach(delegate(FixedTitle i)
		{
			i.ReRender();
		});
	}

	private void reRender()
	{
		base.Items.ToList().ForEach(delegate(TitleVisual i)
		{
			i.ReRender();
		});
	}
}
