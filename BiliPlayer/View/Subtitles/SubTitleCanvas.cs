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

namespace BiliPlayer.View.Subtitles
{
	// Token: 0x02000013 RID: 19
	[Export]
	public class SubTitleCanvas : UserControl, IViewPart, IComponentConnector
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002E7C File Offset: 0x0000107C
		public SubTitleCanvas()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E8A File Offset: 0x0000108A
		public void Init(Panel container)
		{
			base.DataContext = this._controller;
			Panel.SetZIndex(this, 1);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EA0 File Offset: 0x000010A0
		internal Timeline Init(IEnumerable<TitleInfo> titles)
		{
			this.container.Clear();
			(from title in titles ?? new TitleInfo[0]
			where !string.IsNullOrEmpty(title.Text)
			let visual = TitleVisual.Create(title)
			select visual).ForEach(new Action<TitleVisual>(this.container.Add));
			return this.container.CreateTimelineGroup();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F54 File Offset: 0x00001154
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/BiliPlayer;component/view/subtitles/subtitlecanvas.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F84 File Offset: 0x00001184
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F8E File Offset: 0x0000118E
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.container = (TitleContainer)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000022 RID: 34
		[Import]
		private BulletCurtainController _controller;

		// Token: 0x04000023 RID: 35
		internal TitleContainer container;

		// Token: 0x04000024 RID: 36
		private bool _contentLoaded;
	}
}
