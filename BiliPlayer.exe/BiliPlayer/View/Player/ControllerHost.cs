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
	// Token: 0x02000022 RID: 34
	public class ControllerHost : UserControl, IViewPart, IComponentConnector
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00003BE9 File Offset: 0x00001DE9
		public ControllerHost()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003BF8 File Offset: 0x00001DF8
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

		// Token: 0x060000CB RID: 203 RVA: 0x00003C70 File Offset: 0x00001E70
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

		// Token: 0x060000CC RID: 204 RVA: 0x00003D0C File Offset: 0x00001F0C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/player/controllerhost.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003D3C File Offset: 0x00001F3C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.controller = (Controller)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			this.controllerOffset = (TranslateTransform)target;
		}

		// Token: 0x0400004E RID: 78
		[Import]
		private TaskbarIndicator _taskbarIndicator;

		// Token: 0x0400004F RID: 79
		[Import]
		private MediaElement _mediaElement;

		// Token: 0x04000050 RID: 80
		[Import]
		private BulletCurtainController _controller;

		// Token: 0x04000051 RID: 81
		private InputOperation _input;

		// Token: 0x04000052 RID: 82
		internal Controller controller;

		// Token: 0x04000053 RID: 83
		internal TranslateTransform controllerOffset;

		// Token: 0x04000054 RID: 84
		private bool _contentLoaded;
	}
}
