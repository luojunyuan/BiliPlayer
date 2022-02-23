using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BiliPlayer.Util;

public abstract class VisibilityConverter<T> : MarkupExtension, IValueConverter
{
	public bool VisibleInDesignMode { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (VisibleInDesignMode && WindowUtil.IsInDesignMode())
		{
			return Visibility.Visible;
		}
		if (value == null)
		{
			return Visibility.Collapsed;
		}
		if (IsValaueVisible((T)value))
		{
			return Visibility.Visible;
		}
		return Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}

	protected abstract bool IsValaueVisible(T value);

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return this;
	}
}
