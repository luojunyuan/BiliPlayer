using System;
using System.Windows;
using System.Windows.Data;

namespace BiliPlayer.Util
{
	// Token: 0x0200002A RID: 42
	internal static class Dependency
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000041F4 File Offset: 0x000023F4
		public static DependencyProperty RegistProperty<TClass>(string name, Action<TClass> action) where TClass : class
		{
			return Dependency.RegistProperty<TClass, object>(name, action);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004200 File Offset: 0x00002400
		public static DependencyProperty RegistProperty<TClass, TValue>(string name, Action<TClass> action) where TClass : class
		{
			return Dependency.RegistProperty<TClass, TValue>(name, default(TValue), action);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004220 File Offset: 0x00002420
		public static DependencyProperty RegistProperty<TClass, TValue>(string name, TValue defaultValue, Action<TClass> action) where TClass : class
		{
			return DependencyProperty.Register(name, typeof(TValue), typeof(TClass), new PropertyMetadata(defaultValue, delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
			{
				action(s as TClass);
			}));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000426B File Offset: 0x0000246B
		public static void SetBinding(this DependencyObject obj, DependencyProperty property, string bindingPath, object datacontext, BindingMode mode = BindingMode.Default, int delay = 0)
		{
			BindingOperations.SetBinding(obj, property, new Binding(bindingPath)
			{
				Source = datacontext,
				Mode = mode,
				Delay = delay
			});
		}
	}
}
