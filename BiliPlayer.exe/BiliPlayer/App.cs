using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace BiliPlayer
{
	// Token: 0x0200000A RID: 10
	public class App : Application
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000028A0 File Offset: 0x00000AA0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			base.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
			Uri resourceLocator = new Uri("/BiliPlayer;component/app.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028E1 File Offset: 0x00000AE1
		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}

		// Token: 0x0400000C RID: 12
		private bool _contentLoaded;
	}
}
