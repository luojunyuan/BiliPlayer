using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BiliPlayer.View.Player.Controls;

namespace BiliPlayer.View.Player;

public partial class ControllerHost : UserControl, IViewPart
{
	[Import]
	private TaskbarIndicator _taskbarIndicator;

	[Import]
	private MediaElement _mediaElement;

	[Import]
	private BulletCurtainController _controller;

	private InputOperation _input;

	public ControllerHost()
	{
		InitializeComponent();
	}

	public void Init(Panel container)
	{
		Panel.SetZIndex(this, 100);
		_mediaElement.MouseDown += ChangeControllerVisibleState;
		base.DataContext = _controller;
		container.ContextMenu = new MainMenu
		{
			DataContext = _controller
		};
		_taskbarIndicator.Regist(container.FindLogicalParent<Window>());
		_input = new InputOperation(container.FindLogicalParent<Window>(), _controller);
	}

	private void ChangeControllerVisibleState(object sender, MouseButtonEventArgs e)
	{
		if (e.ClickCount == 2)
		{
			double value = ((controllerOffset.Y > 0.0) ? 0.0 : controller.RenderSize.Height);
			DoubleAnimation animation = new DoubleAnimation
			{
				To = value,
				Duration = TimeSpan.FromSeconds(0.6),
				EasingFunction = new CubicEase
				{
					EasingMode = EasingMode.EaseOut
				}
			};
			controllerOffset.BeginAnimation(TranslateTransform.YProperty, animation);
		}
	}
}
