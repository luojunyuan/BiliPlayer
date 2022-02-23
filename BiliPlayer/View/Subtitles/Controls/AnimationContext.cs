using System;
using System.Windows;
using System.Windows.Media.Animation;
using BiliPlayer.Common.Animation;

namespace BiliPlayer.View.Subtitles.Controls;

internal class AnimationContext : Animatable
{
	public static DependencyProperty IsVisibleProperty = registProperty<AnimationContext, bool>("IsVisible");

	public static DependencyProperty ProgressProperty = registProperty<AnimationContext, double>("Progress");

	public Timeline VisibleTimeLine { get; private set; }

	public Timeline ProgressTimeLine { get; private set; }

	public AnimationContext(TitleVisual data)
	{
		TitleInfo dataContext = data.DataContext;
		VisibleTimeLine = new VisibleTimeline(dataContext.Start, dataContext.End);
		bindingAnimation(VisibleTimeLine, IsVisibleProperty.Name);
		ProgressTimeLine = new ProgressTimeline(dataContext.Start, dataContext.End);
		bindingAnimation(ProgressTimeLine, ProgressProperty.Name);
	}

	protected override Freezable CreateInstanceCore()
	{
		return this;
	}

	private void bindingAnimation(Timeline timeline, string property)
	{
		Storyboard.SetTarget(timeline, this);
		Storyboard.SetTargetProperty(timeline, new PropertyPath(property));
	}

	protected static DependencyProperty registProperty<TClass>(string name, Action<TClass> action = null) where TClass : class
	{
		return registProperty<TClass, object>(name, action);
	}

	protected static DependencyProperty registProperty<TClass, TValue>(string name, Action<TClass> action = null) where TClass : class
	{
		action = action ?? ((Action<TClass>)delegate
		{
		});
		return DependencyProperty.Register(name, typeof(TValue), typeof(TClass), new PropertyMetadata(delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
		{
			action(s as TClass);
		}));
	}
}
