using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BiliPlayer.Common.MVVM;

public class NotifyItem : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged;

	protected void onPropertyChanged([CallerMemberName] string name = "")
	{
		this.NotifyPropertyChanged(this.PropertyChanged, name);
	}

	protected void updateProper<T>(ref T propertyValue, T newValue, [CallerMemberName] string properName = "")
	{
		this.UpdateProperty(ref propertyValue, newValue, this.PropertyChanged, properName);
	}
}
