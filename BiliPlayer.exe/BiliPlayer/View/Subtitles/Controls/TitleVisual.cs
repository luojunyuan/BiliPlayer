using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BiliPlayer.Common.Controls;
using BiliPlayer.Common.Data;
using BiliPlayer.Util;
using BiliPlayer.View.Setting;

namespace BiliPlayer.View.Subtitles.Controls
{
	// Token: 0x02000019 RID: 25
	internal abstract class TitleVisual : ItemVisual<TitleInfo>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033FA File Offset: 0x000015FA
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003402 File Offset: 0x00001602
		public AnimationContext Animation { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000340B File Offset: 0x0000160B
		protected double fontHeight
		{
			get
			{
				return 30.0 * this.scaleRate + 2.0;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003428 File Offset: 0x00001628
		static TitleVisual()
		{
			TitleVisual._effect = new Lazy<DropShadowEffect>(() => new DropShadowEffect
			{
				Color = (Color)ColorConverter.ConvertFromString("#4F000000"),
				BlurRadius = 5.0,
				RenderingBias = RenderingBias.Quality,
				Opacity = 0.85,
				ShadowDepth = 5.0
			});
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003478 File Offset: 0x00001678
		public TitleVisual(TitleInfo title) : base(title)
		{
			if (string.IsNullOrEmpty(title.Text))
			{
				throw new ArgumentException();
			}
			this.Animation = new AnimationContext(this);
			base.setBinding(ItemVisual<TitleInfo>.IsVisibleProperty, "IsVisible", this.Animation);
			base.setBinding(TitleVisual.ProgressProperty, TitleVisual.ProgressProperty.Name, this.Animation);
			this._fontSize = TitleVisual._rnd.Next(20, 26);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034FF File Offset: 0x000016FF
		public void ReRender()
		{
			if (!base.IsVisible)
			{
				return;
			}
			this.onReRender();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003510 File Offset: 0x00001710
		protected virtual void onReRender()
		{
			this.render();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003518 File Offset: 0x00001718
		private RenderTargetBitmap getImage()
		{
			SettingData instance = SettingData.Instance;
			this.scaleRate = instance.FontScale;
			FormattedText formattedText = new FormattedText(base.DataContext.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(instance.FontFamily), (double)this._fontSize * this.scaleRate, Brushes.Black);
			if (instance.IsBold)
			{
				formattedText.SetFontWeight(FontWeights.DemiBold);
			}
			Geometry geometry = formattedText.BuildGeometry(default(Point));
			this._bounds = new Rect?(new Rect(geometry.Bounds.Size));
			this._bounds = new Rect?(new Rect(default(Point), new Point(geometry.Bounds.Size.Width + 20.0, geometry.Bounds.Size.Height + 20.0)));
			Path path = new Path
			{
				Data = geometry,
				Fill = base.DataContext.Fill
			};
			if (instance.HasStroke)
			{
				path.Stroke = Brushes.Black;
				path.StrokeThickness = 0.75 * this.scaleRate;
			}
			if (instance.HasShadow)
			{
				path.Effect = TitleVisual._effect.Value;
			}
			if (base.DataContext.IsDarkColor)
			{
				path.Stroke = Brushes.White;
			}
			path.Arrange(this._bounds.Value);
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)this._bounds.Value.Width, (int)this._bounds.Value.Height, 96.0, 96.0, PixelFormats.Default);
			renderTargetBitmap.Render(path);
			renderTargetBitmap.Freeze();
			return renderTargetBitmap;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000036F0 File Offset: 0x000018F0
		protected override async void render()
		{
			using (DrawingContext context = this.RenderOpen())
			{
				if (!this.IsVisible)
				{
					return;
				}
				RenderTargetBitmap imageSource = await TitleRender.Dispatcher.InvokeAsync<RenderTargetBitmap>(() => this.getImage());
				if (!this.IsVisible)
				{
					return;
				}
				context.DrawImage(imageSource, this._bounds.Value);
				this.onProgressChanged(0.0, this._bounds.Value);
			}
			DrawingContext context = null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000372C File Offset: 0x0000192C
		private void onProgressChanged()
		{
			if (!base.IsVisible)
			{
				return;
			}
			if (this._bounds == null)
			{
				return;
			}
			double progress = (double)base.GetValue(TitleVisual.ProgressProperty);
			this.onProgressChanged(progress, this._bounds.Value);
		}

		// Token: 0x0600009A RID: 154
		protected abstract void onProgressChanged(double progress, Rect bounds);

		// Token: 0x0600009B RID: 155 RVA: 0x00003773 File Offset: 0x00001973
		public static TitleVisual Create(TitleInfo title)
		{
			if (title.ScrollMode.IsFixedTitle())
			{
				return new FixedTitle(title);
			}
			return new ScrollTitle(title);
		}

		// Token: 0x0400003A RID: 58
		protected static Random _rnd = new Random();

		// Token: 0x0400003B RID: 59
		protected double scaleRate = 1.0;

		// Token: 0x0400003C RID: 60
		private static Lazy<DropShadowEffect> _effect;

		// Token: 0x0400003D RID: 61
		private int _fontSize;

		// Token: 0x0400003E RID: 62
		private Rect? _bounds;

		// Token: 0x0400003F RID: 63
		public static DependencyProperty ProgressProperty = Dependency.RegistProperty<TitleVisual, double>("Progress", delegate(TitleVisual i)
		{
			i.onProgressChanged();
		});
	}
}
