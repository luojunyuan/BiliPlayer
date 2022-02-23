using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace BiliPlayer.View.Setting;

public partial class SettingWindow : Window
{
	public SettingWindow()
	{
		InitializeComponent();
		base.DataContext = SettingData.Instance;
	}
}
