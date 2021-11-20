using System;
using System.Windows.Threading;

namespace BiliPlayer
{
	// Token: 0x02000009 RID: 9
	internal static class DispatchExtensions
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002895 File Offset: 0x00000A95
		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
		{
			return dispatcher.BeginInvoke(DispatcherPriority.Send, action);
		}
	}
}
