using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BiliPlayer.Common.MVVM
{
	// Token: 0x0200003B RID: 59
	public static class NotifyobjectExtension
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00004DDB File Offset: 0x00002FDB
		public static void UpdateProperty<T>(this INotifyPropertyChanged obj, ref T properValue, T newValue, PropertyChangedEventHandler notifyEvent, [CallerMemberName] string propertyName = "")
		{
			if (object.Equals(properValue, newValue))
			{
				return;
			}
			properValue = newValue;
			obj.NotifyPropertyChanged(notifyEvent, propertyName);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004E06 File Offset: 0x00003006
		public static void NotifyPropertyChanged(this INotifyPropertyChanged obj, PropertyChangedEventHandler notifyEvent, [CallerMemberName] string propertyName = "")
		{
			if (notifyEvent != null)
			{
				notifyEvent(obj, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
