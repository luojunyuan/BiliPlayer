using System;
using System.Windows;
using System.Windows.Data;

namespace BiliPlayer.Util;

internal static class Dependency
{
	public static DependencyProperty RegistProperty<TClass>(string name, Action<TClass> action) where TClass : class
	{
		return RegistProperty<TClass, object>(name, action);
	}

	public static DependencyProperty RegistProperty<TClass, TValue>(string name, Action<TClass> action) where TClass : class
	{
		return RegistProperty(name, default(TValue), action);
	}

	public static DependencyProperty RegistProperty<TClass, TValue>(string name, TValue defaultValue, Action<TClass> action) where TClass : class
	{
		return DependencyProperty.Register(name, typeof(TValue), typeof(TClass), new PropertyMetadata(defaultValue, delegate(DependencyObject s, DependencyPropertyChangedEventArgs e)
		{
			action(s as TClass);
		}));
	}

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
