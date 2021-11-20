using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BiliPlayer.View.Subtitles.ViewModel;

namespace BiliPlayer.View.Subtitles
{
	// Token: 0x02000012 RID: 18
	public class SarchCurtain : Window, IComponentConnector
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002D81 File Offset: 0x00000F81
		public SarchCurtain()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D8F File Offset: 0x00000F8F
		private void onClose(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D98 File Offset: 0x00000F98
		protected override async void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			await Task.Yield();
			BulletCurtainController bulletCurtainController = this.DataContext as BulletCurtainController;
			SarchCurtain sarchCurtain = this.root.DataContext as SarchCurtain;
			if (sarchCurtain.Result == true)
			{
				bulletCurtainController.Open(new string[]
				{
					null,
					sarchCurtain.Content
				});
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DDC File Offset: 0x00000FDC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/subtitles/sarchcurtain.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E0C File Offset: 0x0000100C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.root = (StackPanel)target;
				return;
			case 2:
				((Button)target).Click += this.onClose;
				return;
			case 3:
				((Button)target).Click += this.onClose;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000020 RID: 32
		internal StackPanel root;

		// Token: 0x04000021 RID: 33
		private bool _contentLoaded;
	}
}
