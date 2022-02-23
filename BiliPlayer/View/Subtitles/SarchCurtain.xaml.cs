using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BiliPlayer.View.Subtitles.ViewModel;

namespace BiliPlayer.View.Subtitles;

public partial class SarchCurtain : Window
{
	public SarchCurtain()
	{
		InitializeComponent();
	}

	private void onClose(object sender, RoutedEventArgs e)
	{
		Close();
	}

	protected override async void OnClosed(EventArgs e)
	{
		base.OnClosed(e);
		await Task.Yield();
		BulletCurtainController bulletCurtainController = DataContext as BulletCurtainController;
		BiliPlayer.View.Subtitles.ViewModel.SarchCurtain sarchCurtain = root.DataContext as BiliPlayer.View.Subtitles.ViewModel.SarchCurtain;
		if (sarchCurtain.Result == true)
		{
			bulletCurtainController.Open(null, sarchCurtain.Content);
		}
	}
}
