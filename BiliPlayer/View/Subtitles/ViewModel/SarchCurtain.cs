using System;
using BiliPlayer.Common.MVVM;
using BiliPlayer.DataSource.Bili;

namespace BiliPlayer.View.Subtitles.ViewModel
{
	internal class SarchCurtain : NotifyItem
	{
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				base.updateProper<string>(ref this._description, value, "Description");
			}
		}

		public string Input
		{
			get
			{
				return this._input;
			}
			set
			{
				base.updateProper<string>(ref this._input, value, "Input");
				this.process();
			}
		}

		public bool? Result { get; private set; }

		public string Content { get; private set; }

		public DelegateCommand ApplyCommand { get; }

		public SarchCurtain()
		{
			this.ApplyCommand = new DelegateCommand(new Action<object>(this.onApply), new Func<object, bool>(this.canApply));
		}

		private async void process()
		{
			this.Description = "可以只输入编号（支持拖拽），目前只支持bilibili";
			this.Content = null;
			DelegateCommand.UpdateCommandState();
			if (!string.IsNullOrWhiteSpace(this.Input))
			{
				try
				{
					this.Description = "处理中，请稍后...";
					string input = this.Input;
					string content = await BiliTitleDownloader.DownlaodFile(input);
					this.Content = content;
					if (input != this.Input)
					{
						return;
					}
					this.Description = "加载完成，点击确定打开字幕";
					input = null;
				}
				catch (Exception ex)
				{
					this.Description = ex.Message;
				}
				DelegateCommand.UpdateCommandState();
			}
		}

		private void onApply(object para)
		{
			this.Result = new bool?(true);
		}

		private bool canApply(object para)
		{
			return !string.IsNullOrEmpty(this.Content);
		}

		private const string DefaultDescription = "可以只输入编号（支持拖拽），目前只支持bilibili";

		private string _description = "可以只输入编号（支持拖拽），目前只支持bilibili";

		private string _input;
	}
}
