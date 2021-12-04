using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BiliPlayer.View.Subtitles.ViewModel;

namespace BiliPlayer.View.Subtitles
{
	public partial class SarchCurtain : IComponentConnector
	{
		public SarchCurtain()
		{
			this.InitializeComponent();
		}

		private void onClose(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		protected override async void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			await Task.Yield();
			BulletCurtainController bulletCurtainController = this.DataContext as BulletCurtainController;
			SarchCurtain sarchCurtain = this.root.DataContext as SarchCurtain;
			if (sarchCurtain.Result == true)
			{
				bulletCurtainController.Open(new string[]
				{
					null,
					sarchCurtain.Content
				});
			}
		}
	}
}
