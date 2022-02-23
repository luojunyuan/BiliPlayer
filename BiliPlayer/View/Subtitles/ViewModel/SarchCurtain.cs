using System;
using BiliPlayer.Common.MVVM;
using BiliPlayer.DataSource.Bili;

namespace BiliPlayer.View.Subtitles.ViewModel;

internal class SarchCurtain : NotifyItem
{
	private const string DefaultDescription = "可以只输入编号（支持拖拽），目前只支持bilibili";

	private string _description = "可以只输入编号（支持拖拽），目前只支持bilibili";

	private string _input;

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			updateProper(ref _description, value, "Description");
		}
	}

	public string Input
	{
		get
		{
			return _input;
		}
		set
		{
			updateProper(ref _input, value, "Input");
			process();
		}
	}

	public bool? Result { get; private set; }

	public string Content { get; private set; }

	public DelegateCommand ApplyCommand { get; }

	public SarchCurtain()
	{
		ApplyCommand = new DelegateCommand(onApply, canApply);
	}

	private async void process()
	{
		Description = "可以只输入编号（支持拖拽），目前只支持bilibili";
		Content = null;
		DelegateCommand.UpdateCommandState();
		if (string.IsNullOrWhiteSpace(Input))
		{
			return;
		}
		try
		{
			Description = "处理中，请稍后...";
			string input = Input;
			string text2 = (Content = await BiliTitleDownloader.DownlaodFile(input));
			if (input != Input)
			{
				return;
			}
			Description = "加载完成，点击确定打开字幕";
		}
		catch (Exception ex)
		{
			Description = ex.Message;
		}
		DelegateCommand.UpdateCommandState();
	}

	private void onApply(object para)
	{
		Result = true;
	}

	private bool canApply(object para)
	{
		return !string.IsNullOrEmpty(Content);
	}
}
