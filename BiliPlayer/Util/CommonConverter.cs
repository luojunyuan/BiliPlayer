using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BiliPlayer.Util;

[MarkupExtensionReturnType(typeof(IValueConverter))]
public abstract class CommonConverter<TSource, TTarget> : MarkupExtension, IValueConverter
{
	public bool IsNullValid { get; protected set; }

	protected abstract object convert(TSource value, object parameter);

	protected virtual object convertBack(TTarget value, object parameter)
	{
		throw new NotImplementedException();
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null && IsNullValid)
		{
			return convert((TSource)value, parameter);
		}
		if (!(value is TSource))
		{
			return DependencyProperty.UnsetValue;
		}
		return convert((TSource)value, parameter);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (!(value is TTarget))
		{
			return null;
		}
		return convertBack((TTarget)value, parameter);
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return this;
	}
}
