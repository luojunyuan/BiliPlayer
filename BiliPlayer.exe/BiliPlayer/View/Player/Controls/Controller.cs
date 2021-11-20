using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000024 RID: 36
	public class Controller : UserControl, IComponentConnector
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003D7F File Offset: 0x00001F7F
		public Controller()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003D90 File Offset: 0x00001F90
		private void onProgressChanged(object sender, MouseButtonEventArgs e)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((sender as Slider).Value);
			(base.DataContext as BulletCurtainController).Commands.Seek.Execute(timeSpan);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003DD0 File Offset: 0x00001FD0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/player/controls/controller.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003E00 File Offset: 0x00002000
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.slider = (Slider)target;
				return;
			}
			if (connectionId != 2)
			{
				this._contentLoaded = true;
				return;
			}
			((Slider)target).PreviewMouseLeftButtonUp += this.onProgressChanged;
		}

		// Token: 0x04000056 RID: 86
		internal Slider slider;

		// Token: 0x04000057 RID: 87
		private bool _contentLoaded;
	}
}
