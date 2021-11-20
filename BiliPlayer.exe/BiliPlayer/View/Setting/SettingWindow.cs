using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace BiliPlayer.View.Setting
{
	// Token: 0x0200001D RID: 29
	public class SettingWindow : Window, IComponentConnector
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00003A3A File Offset: 0x00001C3A
		public SettingWindow()
		{
			this.InitializeComponent();
			base.DataContext = SettingData.Instance;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A54 File Offset: 0x00001C54
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/setting/settingwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A84 File Offset: 0x00001C84
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0400004B RID: 75
		private bool _contentLoaded;
	}
}
