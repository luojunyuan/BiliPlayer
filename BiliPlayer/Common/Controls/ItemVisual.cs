using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.Common.Controls
{
	// Token: 0x02000035 RID: 53
	internal abstract class ItemVisual<T> : DrawingVisual
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004B89 File Offset: 0x00002D89
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004B91 File Offset: 0x00002D91
		public T DataContext { get; private set; }

		// Token: 0x06000127 RID: 295 RVA: 0x00004B9A File Offset: 0x00002D9A
		public ItemVisual(T dataContext)
		{
			this.DataContext = dataContext;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004BA9 File Offset: 0x00002DA9
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004BBB File Offset: 0x00002DBB
		public bool IsVisible
		{
			get
			{
				return (bool)base.GetValue(ItemVisual<T>.IsVisibleProperty);
			}
			set
			{
				base.SetValue(ItemVisual<T>.IsVisibleProperty, value);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004BCE File Offset: 0x00002DCE
		protected virtual void onRender(DrawingContext context)
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004BD0 File Offset: 0x00002DD0
		protected virtual void render()
		{
			using (DrawingContext drawingContext = base.RenderOpen())
			{
				if (this.IsVisible)
				{
					this.onRender(drawingContext);
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004C14 File Offset: 0x00002E14
		protected void setBinding(DependencyProperty property, string bindingPath)
		{
			this.setBinding(property, bindingPath, this.DataContext);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C29 File Offset: 0x00002E29
		protected void setBinding(DependencyProperty property, string bindingPath, object datacontext)
		{
			this.SetBinding(property, bindingPath, datacontext, BindingMode.Default, 0);
		}

		// Token: 0x0400006C RID: 108
		public static readonly DependencyProperty IsVisibleProperty = Dependency.RegistProperty<ItemVisual<T>>("IsVisible", delegate(ItemVisual<T> i)
		{
			i.render();
		});
	}
}
