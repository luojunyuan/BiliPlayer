using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BiliPlayer.Util;
using BiliPlayer.View;

namespace BiliPlayer
{
	// Token: 0x0200000B RID: 11
	public class MainWindow : Window, IComponentConnector
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000028FC File Offset: 0x00000AFC
		public MainWindow()
		{
			this.InitializeComponent();
			base.Loaded += this.MainWindow_Loaded;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000291C File Offset: 0x00000B1C
		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (!WindowUtil.IsInDesignMode())
			{
				await Task.Delay(100);
				this.compose();
				foreach (IViewPart viewPart in this._views)
				{
					this.root.Children.Add(viewPart as UIElement);
					viewPart.Init(this.root);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002958 File Offset: 0x00000B58
		private void compose()
		{
			this._mediaElement = new MediaElement
			{
				ScrubbingEnabled = true
			};
			using (AssemblyCatalog assemblyCatalog = new AssemblyCatalog(base.GetType().Assembly))
			{
				this._container = new CompositionContainer(assemblyCatalog, new ExportProvider[0]);
				this._container.ComposeParts(new object[]
				{
					this
				});
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029CC File Offset: 0x00000BCC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/mainwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029FC File Offset: 0x00000BFC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.root = (Grid)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400000D RID: 13
		[ImportMany]
		private IViewPart[] _views;

		// Token: 0x0400000E RID: 14
		[Export]
		private MediaElement _mediaElement;

		// Token: 0x0400000F RID: 15
		private CompositionContainer _container;

		// Token: 0x04000010 RID: 16
		internal Grid root;

		// Token: 0x04000011 RID: 17
		private bool _contentLoaded;
	}
}
