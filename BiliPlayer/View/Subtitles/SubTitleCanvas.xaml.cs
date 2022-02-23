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

namespace BiliPlayer.View.Subtitles;

[Export]
public partial class SubTitleCanvas : UserControl, IViewPart
{
	[Import]
	private BulletCurtainController _controller;

	public SubTitleCanvas()
	{
		InitializeComponent();
	}

	public void Init(Panel container)
	{
		base.DataContext = _controller;
		Panel.SetZIndex(this, 1);
	}

	internal Timeline Init(IEnumerable<TitleInfo> titles)
	{
		container.Clear();
		(from title in titles ?? new TitleInfo[0]
			where !string.IsNullOrEmpty(title.Text)
			let visual = TitleVisual.Create(title)
			select visual).ToList().ForEach(container.Add);
		return container.CreateTimelineGroup();
	}
}
