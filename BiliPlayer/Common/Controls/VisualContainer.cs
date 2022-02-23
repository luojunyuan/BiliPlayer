using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace BiliPlayer.Common.Controls;

internal class VisualContainer<T> : FrameworkElement where T : Visual
{
	private VisualCollection _children;

	public IEnumerable<T> Items => _children.Cast<T>();

	protected override int VisualChildrenCount => _children.Count;

	public VisualContainer()
	{
		_children = new VisualCollection(this);
	}

	public virtual void Add(T visual)
	{
		_children.Add(visual);
	}

	public virtual void Remove(T visual)
	{
		_children.Remove(visual);
	}

	public virtual void Clear()
	{
		_children.Clear();
	}

	protected override Visual GetVisualChild(int index)
	{
		return _children[index];
	}
}
