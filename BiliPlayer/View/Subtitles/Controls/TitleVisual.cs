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

namespace BiliPlayer.View.Subtitles.Controls;

internal abstract class TitleVisual : ItemVisual<TitleInfo>
{
	protected static Random _rnd;

	protected double scaleRate = 1.0;

	private static Lazy<DropShadowEffect> _effect;

	private int _fontSize;

	private Rect? _bounds;

	public static DependencyProperty ProgressProperty;

	public AnimationContext Animation { get; private set; }

	protected double fontHeight => 30.0 * scaleRate + 2.0;

	static TitleVisual()
	{
		_rnd = new Random();
		ProgressProperty = Dependency.RegistProperty<TitleVisual, double>("Progress", delegate(TitleVisual i)
		{
			i.onProgressChanged();
		});
		_effect = new Lazy<DropShadowEffect>(() => new DropShadowEffect
		{
			Color = (Color)ColorConverter.ConvertFromString("#4F000000"),
			BlurRadius = 5.0,
			RenderingBias = RenderingBias.Quality,
			Opacity = 0.85,
			ShadowDepth = 5.0
		});
	}

	public TitleVisual(TitleInfo title)
		: base(title)
	{
		if (string.IsNullOrEmpty(title.Text))
		{
			throw new ArgumentException();
		}
		Animation = new AnimationContext(this);
		setBinding(ItemVisual<TitleInfo>.IsVisibleProperty, "IsVisible", Animation);
		setBinding(ProgressProperty, ProgressProperty.Name, Animation);
		_fontSize = _rnd.Next(20, 26);
	}

	public void ReRender()
	{
		if (base.IsVisible)
		{
			onReRender();
		}
	}

	protected virtual void onReRender()
	{
		render();
	}

	private RenderTargetBitmap getImage()
	{
		SettingData instance = SettingData.Instance;
		scaleRate = instance.FontScale;
		FormattedText formattedText = new FormattedText(base.DataContext.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(instance.FontFamily), (double)_fontSize * scaleRate, Brushes.Black);
		if (instance.IsBold)
		{
			formattedText.SetFontWeight(FontWeights.DemiBold);
		}
		Geometry geometry = formattedText.BuildGeometry(default(Point));
		_bounds = new Rect(geometry.Bounds.Size);
		_bounds = new Rect(default(Point), new Point(geometry.Bounds.Size.Width + 20.0, geometry.Bounds.Size.Height + 20.0));
		Path path = new Path
		{
			Data = geometry,
			Fill = base.DataContext.Fill
		};
		if (instance.HasStroke)
		{
			path.Stroke = Brushes.Black;
			path.StrokeThickness = 0.75 * scaleRate;
		}
		if (instance.HasShadow)
		{
			path.Effect = _effect.Value;
		}
		if (base.DataContext.IsDarkColor)
		{
			path.Stroke = Brushes.White;
		}
		path.Arrange(_bounds.Value);
		RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)_bounds.Value.Width, (int)_bounds.Value.Height, 96.0, 96.0, PixelFormats.Default);
		renderTargetBitmap.Render(path);
		renderTargetBitmap.Freeze();
		return renderTargetBitmap;
	}

	protected override async void render()
	{
		using DrawingContext context = RenderOpen();
		if (!IsVisible)
		{
			return;
		}
		RenderTargetBitmap imageSource = await TitleRender.Dispatcher.InvokeAsync(() => getImage());
		if (!IsVisible)
		{
			return;
		}
		context.DrawImage(imageSource, _bounds.Value);
		onProgressChanged(0.0, _bounds.Value);
	}

	private void onProgressChanged()
	{
		if (base.IsVisible && _bounds.HasValue)
		{
			double progress = (double)GetValue(ProgressProperty);
			onProgressChanged(progress, _bounds.Value);
		}
	}

	protected abstract void onProgressChanged(double progress, Rect bounds);

	public static TitleVisual Create(TitleInfo title)
	{
		if (title.ScrollMode.IsFixedTitle())
		{
			return new FixedTitle(title);
		}
		return new ScrollTitle(title);
	}
}
