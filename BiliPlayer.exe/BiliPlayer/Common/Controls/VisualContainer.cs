using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace BiliPlayer.Common.Controls
{
	// Token: 0x02000036 RID: 54
	internal class VisualContainer<T> : FrameworkElement where T : Visual
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00004C57 File Offset: 0x00002E57
		public VisualContainer()
		{
			this._children = new VisualCollection(this);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004C6B File Offset: 0x00002E6B
		public virtual void Add(T visual)
		{
			this._children.Add(visual);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004C7F File Offset: 0x00002E7F
		public virtual void Remove(T visual)
		{
			this._children.Remove(visual);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004C92 File Offset: 0x00002E92
		public virtual void Clear()
		{
			this._children.Clear();
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004C9F File Offset: 0x00002E9F
		public IEnumerable<T> Items
		{
			get
			{
				return this._children.Cast<T>();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004CAC File Offset: 0x00002EAC
		protected override Visual GetVisualChild(int index)
		{
			return this._children[index];
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004CBA File Offset: 0x00002EBA
		protected override int VisualChildrenCount
		{
			get
			{
				return this._children.Count;
			}
		}

		// Token: 0x0400006D RID: 109
		private VisualCollection _children;
	}
}
