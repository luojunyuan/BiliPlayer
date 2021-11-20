using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BiliPlayer.Util
{
	// Token: 0x0200002B RID: 43
	[MarkupExtensionReturnType(typeof(IValueConverter))]
	public abstract class CommonConverter<TSource, TTarget> : MarkupExtension, IValueConverter
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004292 File Offset: 0x00002492
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000429A File Offset: 0x0000249A
		public bool IsNullValid { get; protected set; }

		// Token: 0x060000EC RID: 236
		protected abstract object convert(TSource value, object parameter);

		// Token: 0x060000ED RID: 237 RVA: 0x000042A3 File Offset: 0x000024A3
		protected virtual object convertBack(TTarget value, object parameter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000042AA File Offset: 0x000024AA
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null && this.IsNullValid)
			{
				return this.convert((TSource)((object)value), parameter);
			}
			if (!(value is TSource))
			{
				return DependencyProperty.UnsetValue;
			}
			return this.convert((TSource)((object)value), parameter);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000042E0 File Offset: 0x000024E0
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is TTarget))
			{
				return null;
			}
			return this.convertBack((TTarget)((object)value), parameter);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000031C8 File Offset: 0x000013C8
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
