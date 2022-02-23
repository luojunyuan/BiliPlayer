using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BiliPlayer.Util;
using BiliPlayer.View;

namespace BiliPlayer;

public partial class MainWindow : Window
{
	// 名词解释
	// Subtitle 即弹幕
	// 注入 SubtitleCanvas ControllerHost MediaPlayerHost 三个组件
	[ImportMany]
	private IViewPart[] _views;

	[Export]
	private MediaElement _mediaElement;

	private CompositionContainer _container;

	public MainWindow()
	{
		InitializeComponent();
		base.Loaded += MainWindow_Loaded;
	}

	private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
	{
		if (!WindowUtil.IsInDesignMode())
		{
			await Task.Delay(100);
			compose();
			IViewPart[] views = _views;
			foreach (IViewPart viewPart in views)
			{
				root.Children.Add(viewPart as UIElement);
				viewPart.Init(root);
			}
		}
	}

	private void compose()
	{
		_mediaElement = new MediaElement
		{
			ScrubbingEnabled = true
		};
		using AssemblyCatalog catalog = new AssemblyCatalog(GetType().Assembly);
		_container = new CompositionContainer(catalog);
		_container.ComposeParts(this);
	}
}
