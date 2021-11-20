using System;
using System.Windows;
using System.Windows.Media.Animation;
using BiliPlayer.Common.Animation;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x02000016 RID: 22
	internal class AnimationContext : Animatable
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000312F File Offset: 0x0000132F
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003137 File Offset: 0x00001337
		public Timeline VisibleTimeLine { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003140 File Offset: 0x00001340
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003148 File Offset: 0x00001348
		public Timeline ProgressTimeLine { get; private set; }

		// Token: 0x06000081 RID: 129 RVA: 0x00003154 File Offset: 0x00001354
		public AnimationContext(TitleVisual data)
		{
			TitleInfo dataContext = data.DataContext;
			this.VisibleTimeLine = new VisibleTimeline(dataContext.Start, dataContext.End);
			this.bindingAnimation(this.VisibleTimeLine, AnimationContext.IsVisibleProperty.Name);
			this.ProgressTimeLine = new ProgressTimeline(dataContext.Start, dataContext.End);
			this.bindingAnimation(this.ProgressTimeLine, AnimationContext.ProgressProperty.Name);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031C8 File Offset: 0x000013C8
		protected override Freezable CreateInstanceCore()
		{
			return this;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000031CB File Offset: 0x000013CB
		private void bindingAnimation(Timeline timeline, string property)
		{
			Storyboard.SetTarget(timeline, this);
			Storyboard.SetTargetProperty(timeline, new PropertyPath(property, new object[0]));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031E6 File Offset: 0x000013E6
		protected static DependencyProperty registProperty<TClass>(string name, Action<TClass> action = null) where TClass : class
		{
			return AnimationContext.registProperty<TClass, object>(name, action);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000031F0 File Offset: 0x000013F0
		protected static DependencyProperty registProperty<TClass, TValue>(string name, Action<TClass> action = null) where TClass : class
		{
			AnimationContext.<>c__DisplayClass14_0<TClass, TValue> CS$<>8__locals1 = new AnimationContext.<>c__DisplayClass14_0<TClass, TValue>();
			CS$<>8__locals1.action = action;
			AnimationContext.<>c__DisplayClass14_0<TClass, TValue> CS$<>8__locals2 = CS$<>8__locals1;
			Action<TClass> action2;
			if ((action2 = CS$<>8__locals2.action) == null && (action2 = AnimationContext.<>c__14<TClass, TValue>.<>9__14_0) == null)
			{
				action2 = (AnimationContext.<>c__14<TClass, TValue>.<>9__14_0 = delegate(TClass _)
				{
				});
			}
			CS$<>8__locals2.action = action2;
			return DependencyProperty.Register(name, typeof(TValue), typeof(TClass), new PropertyMetadata(delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
			{
				CS$<>8__locals1.action(s as TClass);
			}));
		}

		// Token: 0x04000035 RID: 53
		public static DependencyProperty IsVisibleProperty = AnimationContext.registProperty<AnimationContext, bool>("IsVisible", null);

		// Token: 0x04000036 RID: 54
		public static DependencyProperty ProgressProperty = AnimationContext.registProperty<AnimationContext, double>("Progress", null);
	}
}
