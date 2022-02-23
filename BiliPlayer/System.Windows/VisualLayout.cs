using System.Windows.Input;
using System.Windows.Media;

namespace System.Windows;

public static class VisualLayout
{
	public static T FindLogicalParent<T>(this DependencyObject child) where T : class
	{
		DependencyObject parent = LogicalTreeHelper.GetParent(child);
		if (parent == null)
		{
			return null;
		}
		if (parent is T)
		{
			return parent as T;
		}
		return parent.FindLogicalParent<T>();
	}

	public static T FindVisualParent<T>(this DependencyObject child) where T : class
	{
		if (!(child is Visual))
		{
			return null;
		}
		DependencyObject parent = VisualTreeHelper.GetParent(child);
		if (parent == null)
		{
			return null;
		}
		if (parent is T)
		{
			return parent as T;
		}
		return parent.FindVisualParent<T>();
	}

	public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
	{
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(obj, i);
			if (child != null && child is T)
			{
				return (T)child;
			}
			T val = child.FindVisualChild<T>();
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	public static void AddCommandBinding(this UIElement c, ICommand cmd, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecuted = null)
	{
		canExecuted = canExecuted ?? ((CanExecuteRoutedEventHandler)delegate(object _1, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
		});
		CommandBinding commandBinding = new CommandBinding(cmd, executed, canExecuted);
		c.CommandBindings.Add(commandBinding);
	}
}
