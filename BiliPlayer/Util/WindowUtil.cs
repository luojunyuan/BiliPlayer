using System;
using System.ComponentModel;
using System.Windows;

namespace BiliPlayer.Util
{
	// Token: 0x0200002E RID: 46
	internal class WindowUtil
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004384 File Offset: 0x00002584
		public static bool IsInDesignMode()
		{
			return WindowUtil._isInDesignMode.Value;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004390 File Offset: 0x00002590
		private static bool isInDesignMode()
		{
			return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
		}

		// Token: 0x0400005F RID: 95
		private static Lazy<bool> _isInDesignMode = new Lazy<bool>(new Func<bool>(WindowUtil.isInDesignMode));
	}
}
