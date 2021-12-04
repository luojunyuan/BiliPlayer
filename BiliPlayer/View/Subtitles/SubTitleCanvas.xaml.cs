using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using BiliPlayer.View.Subtitles.Controls;

namespace BiliPlayer.View.Subtitles
{
	[Export]
	public partial class SubTitleCanvas : UserControl, IViewPart, IComponentConnector
	{
		public SubTitleCanvas()
		{
			this.InitializeComponent();
		}

		public void Init(Panel container)
		{
			base.DataContext = this._controller;
			Panel.SetZIndex(this, 1);
		}

		internal Timeline Init(IEnumerable<TitleInfo> titles)
		{
			this.container.Clear();
			(from title in titles ?? new TitleInfo[0]
				where !string.IsNullOrEmpty(title.Text)
				let visual = TitleVisual.Create(title)
				select visual).ForEach(new Action<TitleVisual>(this.container.Add));
			return this.container.CreateTimelineGroup();
		}

		// Token: 0x04000022 RID: 34
		[Import]
		private BulletCurtainController _controller;
	}
}
