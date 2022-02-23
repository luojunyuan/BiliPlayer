using System.Diagnostics;
using System.Windows;
using BiliPlayer.Common.Media;
using BiliPlayer.Common.MVVM;
using BiliPlayer.View.Setting;
using BiliPlayer.View.Subtitles;
using Microsoft.Win32;

namespace BiliPlayer.View;

internal class BulletCurtainCommands : MediaCommands
{
	private BulletCurtainController _controller;

	public DelegateCommand HideTitle { get; private set; }

	public DelegateCommand OpenOnlineTitle { get; private set; }

	public DelegateCommand Setting { get; private set; }

	public DelegateCommand Help { get; private set; }

	public BulletCurtainCommands(BulletCurtainController controller)
		: base(controller)
	{
		_controller = controller;
		base.Open = new DelegateCommand(onOpen);
		HideTitle = new DelegateCommand(onHideTitle);
		Setting = new DelegateCommand(onSetting);
		Help = new DelegateCommand(onHelp);
		OpenOnlineTitle = new DelegateCommand(onOpenOnlineTitle);
	}

	private void onOpen(object para)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Multiselect = true
		};
		if (openFileDialog.ShowDialog() == true)
		{
			_controller.Open(openFileDialog.FileNames);
		}
	}

	private void onHideTitle(object para)
	{
		_controller.ShowTitle = !_controller.ShowTitle;
	}

	private void onHelp(object para)
	{
		Process.Start("http://www.cnblogs.com/TianFang/p/4115773.html");
	}

	private void onSetting(object para)
	{
		SettingWindow settingWindow = new SettingWindow();
		settingWindow.Owner = Application.Current.MainWindow;
		settingWindow.Show();
	}

	private void onOpenOnlineTitle(object para)
	{
		SarchCurtain sarchCurtain = new SarchCurtain();
		sarchCurtain.Owner = Application.Current.MainWindow;
		sarchCurtain.DataContext = _controller;
		sarchCurtain.ShowDialog();
	}
}
