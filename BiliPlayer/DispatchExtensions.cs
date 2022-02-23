using System;
using System.Windows.Threading;

namespace BiliPlayer;

internal static class DispatchExtensions
{
	public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
	{
		return dispatcher.BeginInvoke(DispatcherPriority.Send, action);
	}
}
