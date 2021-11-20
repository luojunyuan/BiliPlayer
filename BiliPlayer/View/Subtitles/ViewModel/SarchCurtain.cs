using System;
using BiliPlayer.Common.MVVM;
using BiliPlayer.DataSource.Bili;

namespace BiliPlayer.View.Subtitles.ViewModel
{
	// Token: 0x02000015 RID: 21
	internal class SarchCurtain : NotifyItem
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003038 File Offset: 0x00001238
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003040 File Offset: 0x00001240
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003054 File Offset: 0x00001254
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000305C File Offset: 0x0000125C
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003076 File Offset: 0x00001276
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000307E File Offset: 0x0000127E
		public bool? Result { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003087 File Offset: 0x00001287
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000308F File Offset: 0x0000128F
		public string Content { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003098 File Offset: 0x00001298
		public DelegateCommand ApplyCommand { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x000030A0 File Offset: 0x000012A0
		public SarchCurtain()
		{
			this.ApplyCommand = new DelegateCommand(new Action<object>(this.onApply), new Func<object, bool>(this.canApply));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030D8 File Offset: 0x000012D8
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

		// Token: 0x0600007B RID: 123 RVA: 0x00003111 File Offset: 0x00001311
		private void onApply(object para)
		{
			this.Result = new bool?(true);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000311F File Offset: 0x0000131F
		private bool canApply(object para)
		{
			return !string.IsNullOrEmpty(this.Content);
		}

		// Token: 0x0400002D RID: 45
		private const string DefaultDescription = "可以只输入编号（支持拖拽），目前只支持bilibili";

		// Token: 0x0400002E RID: 46
		private string _description = "可以只输入编号（支持拖拽），目前只支持bilibili";

		// Token: 0x0400002F RID: 47
		private string _input;
	}
}
