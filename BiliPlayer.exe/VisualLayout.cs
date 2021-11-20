using System;
using System.Windows.Input;
using System.Windows.Media;

namespace System.Windows
{
	// Token: 0x02000008 RID: 8
	public static class VisualLayout
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000276C File Offset: 0x0000096C
		public static T FindLogicalParent<T>(this DependencyObject child) where T : class
		{
			DependencyObject parent = LogicalTreeHelper.GetParent(child);
			if (parent == null)
			{
				return default(T);
			}
			if (parent is T)
			{
				return parent as T;
			}
			return parent.FindLogicalParent<T>();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027A8 File Offset: 0x000009A8
		public static T FindVisualParent<T>(this DependencyObject child) where T : class
		{
			if (!(child is Visual))
			{
				return default(T);
			}
			DependencyObject parent = VisualTreeHelper.GetParent(child);
			if (parent == null)
			{
				return default(T);
			}
			if (parent is T)
			{
				return parent as T;
			}
			return parent.FindVisualParent<T>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027F8 File Offset: 0x000009F8
		public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is T)
				{
					return (T)((object)child);
				}
				T t = child.FindVisualChild<T>();
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000284C File Offset: 0x00000A4C
		public static void AddCommandBinding(this UIElement c, ICommand cmd, ExecutedRoutedEventHandler executed, CanExecuteRoutedEventHandler canExecuted = null)
		{
			CanExecuteRoutedEventHandler canExecuteRoutedEventHandler;
			if ((canExecuteRoutedEventHandler = canExecuted) == null && (canExecuteRoutedEventHandler = VisualLayout.<>c.<>9__3_0) == null)
			{
				canExecuteRoutedEventHandler = (VisualLayout.<>c.<>9__3_0 = delegate(object _1, CanExecuteRoutedEventArgs e)
				{
					e.CanExecute = true;
				});
			}
			canExecuted = canExecuteRoutedEventHandler;
			CommandBinding commandBinding = new CommandBinding(cmd, executed, canExecuted);
			c.CommandBindings.Add(commandBinding);
		}
	}
}
