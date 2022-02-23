using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BiliPlayer.Common.MVVM;

public static class NotifyobjectExtension
{
	public static void UpdateProperty<T>(this INotifyPropertyChanged obj, ref T properValue, T newValue, PropertyChangedEventHandler notifyEvent, [CallerMemberName] string propertyName = "")
	{
		if (!object.Equals(properValue, newValue))
		{
			properValue = newValue;
			obj.NotifyPropertyChanged(notifyEvent, propertyName);
		}
	}

	public static void NotifyPropertyChanged(this INotifyPropertyChanged obj, PropertyChangedEventHandler notifyEvent, [CallerMemberName] string propertyName = "")
	{
		notifyEvent?.Invoke(obj, new PropertyChangedEventArgs(propertyName));
	}
}
