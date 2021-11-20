using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BiliPlayer.Util
{
	// Token: 0x0200002C RID: 44
	public abstract class VisibilityConverter<T> : MarkupExtension, IValueConverter
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004304 File Offset: 0x00002504
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000430C File Offset: 0x0000250C
		public bool VisibleInDesignMode { get; set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00004315 File Offset: 0x00002515
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (this.VisibleInDesignMode && WindowUtil.IsInDesignMode())
			{
				return Visibility.Visible;
			}
			if (value == null)
			{
				return Visibility.Collapsed;
			}
			if (this.IsValaueVisible((T)((object)value)))
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004352 File Offset: 0x00002552
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		// Token: 0x060000F6 RID: 246
		protected abstract bool IsValaueVisible(T value);

		// Token: 0x060000F7 RID: 247 RVA: 0x000031C8 File Offset: 0x000013C8
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
