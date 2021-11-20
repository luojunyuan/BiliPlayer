using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BiliPlayer.Common.MVVM
{
	// Token: 0x02000039 RID: 57
	public class NotifyItem : INotifyPropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000137 RID: 311 RVA: 0x00004CD4 File Offset: 0x00002ED4
		// (remove) Token: 0x06000138 RID: 312 RVA: 0x00004D0C File Offset: 0x00002F0C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000139 RID: 313 RVA: 0x00004D41 File Offset: 0x00002F41
		protected void onPropertyChanged([CallerMemberName] string name = "")
		{
			this.NotifyPropertyChanged(this.PropertyChanged, name);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004D50 File Offset: 0x00002F50
		protected void updateProper<T>(ref T propertyValue, T newValue, [CallerMemberName] string properName = "")
		{
			this.UpdateProperty(ref propertyValue, newValue, this.PropertyChanged, properName);
		}
	}
}
