using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000028 RID: 40
	public class MainMenu : ContextMenu, IComponentConnector
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00004009 File Offset: 0x00002209
		public MainMenu()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004018 File Offset: 0x00002218
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/player/controls/mainmenu.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004048 File Offset: 0x00002248
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0400005A RID: 90
		private bool _contentLoaded;
	}
}
