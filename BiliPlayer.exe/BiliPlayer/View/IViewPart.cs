using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace BiliPlayer.View
{
	// Token: 0x02000010 RID: 16
	[InheritedExport]
	internal interface IViewPart
	{
		// Token: 0x06000049 RID: 73
		void Init(Panel container);
	}
}
