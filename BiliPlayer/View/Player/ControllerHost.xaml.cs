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

namespace BiliPlayer.View.Player
{
	public partial class ControllerHost : UserControl, IViewPart
	{
		public ControllerHost()
		{
			this.InitializeComponent();
		}

		public void Init(Panel container)
		{
			Panel.SetZIndex(this, 100);
			this._mediaElement.MouseDown += this.ChangeControllerVisibleState;
			base.DataContext = this._controller;
			container.ContextMenu = new MainMenu
			{
				DataContext = this._controller
			};
			this._taskbarIndicator.Regist(container.FindLogicalParent<Window>());
			this._input = new InputOperation(container.FindLogicalParent<Window>(), this._controller);
		}

		private void ChangeControllerVisibleState(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount != 2)
			{
				return;
			}
			double value = (this.controllerOffset.Y > 0.0) ? 0.0 : this.controller.RenderSize.Height;
			DoubleAnimation animation = new DoubleAnimation
			{
				To = new double?(value),
				Duration = TimeSpan.FromSeconds(0.6),
				EasingFunction = new CubicEase
				{
					EasingMode = EasingMode.EaseOut
				}
			};
			this.controllerOffset.BeginAnimation(TranslateTransform.YProperty, animation);
		}

		[Import]
		private TaskbarIndicator _taskbarIndicator;

		[Import]
		private MediaElement _mediaElement;

		[Import]
		private BulletCurtainController _controller;

		private InputOperation _input;

		internal Controller controller;
	}
}
