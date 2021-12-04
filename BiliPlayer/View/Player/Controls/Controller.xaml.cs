using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BiliPlayer.View.Player.Controls
{
	public partial class Controller : UserControl
	{
		public Controller()
		{
			this.InitializeComponent();
		}

		private void onProgressChanged(object sender, MouseButtonEventArgs e)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((sender as Slider).Value);
			(base.DataContext as BulletCurtainController).Commands.Seek.Execute(timeSpan);
		}
	}
}
