using System.Windows;
using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.Common.Controls;

internal abstract class ItemVisual<T> : DrawingVisual
{
	public static readonly DependencyProperty IsVisibleProperty = Dependency.RegistProperty("IsVisible", delegate(ItemVisual<T> i)
	{
		i.render();
	});

	public T DataContext { get; private set; }

	public bool IsVisible
	{
		get
		{
			return (bool)GetValue(IsVisibleProperty);
		}
		set
		{
			SetValue(IsVisibleProperty, value);
		}
	}

	public ItemVisual(T dataContext)
	{
		DataContext = dataContext;
	}

	protected virtual void onRender(DrawingContext context)
	{
	}

	protected virtual void render()
	{
		using DrawingContext context = RenderOpen();
		if (IsVisible)
		{
			onRender(context);
		}
	}

	protected void setBinding(DependencyProperty property, string bindingPath)
	{
		setBinding(property, bindingPath, DataContext);
	}

	protected void setBinding(DependencyProperty property, string bindingPath, object datacontext)
	{
		this.SetBinding(property, bindingPath, datacontext);
	}
}
