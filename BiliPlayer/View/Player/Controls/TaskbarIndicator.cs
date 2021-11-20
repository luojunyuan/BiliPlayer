using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000029 RID: 41
	[Export]
	internal class TaskbarIndicator
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000405C File Offset: 0x0000225C
		public void Regist(Window window)
		{
			TaskbarItemInfo taskbarItemInfo = new TaskbarItemInfo();
			taskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
			this.setBinding(taskbarItemInfo, TaskbarItemInfo.ProgressValueProperty, "CurrentTime", new Func<object>(this.getProgress));
			this.setBinding(taskbarItemInfo, TaskbarItemInfo.ProgressStateProperty, "State", new Func<object>(this.getState));
			window.TaskbarItemInfo = taskbarItemInfo;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000040B7 File Offset: 0x000022B7
		private void setBinding(TaskbarItemInfo taskBarItemInfo, DependencyProperty proeprty, string property, Func<object> convertHandler)
		{
			BindingOperations.SetBinding(taskBarItemInfo, proeprty, new Binding(property)
			{
				Source = this._media,
				Converter = new TaskbarIndicator.TaskInfoConverter(convertHandler)
			});
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000040E0 File Offset: 0x000022E0
		private object getProgress()
		{
			TimeSpan? timeSpan = this._media.CurrentTime;
			double? num = (timeSpan != null) ? new double?(timeSpan.GetValueOrDefault().TotalSeconds) : null;
			timeSpan = this._media.Duration;
			return (num / ((timeSpan != null) ? new double?(timeSpan.GetValueOrDefault().TotalSeconds) : null)) ?? 0.0;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000041AC File Offset: 0x000023AC
		private object getState()
		{
			switch (this._media.State)
			{
			case MediaState.Stopped:
				return TaskbarItemProgressState.None;
			case MediaState.Playing:
				return TaskbarItemProgressState.Normal;
			case MediaState.Paused:
				return TaskbarItemProgressState.Paused;
			default:
				return TaskbarItemProgressState.None;
			}
		}

		// Token: 0x0400005B RID: 91
		[Import]
		private BulletCurtainController _media;

		// Token: 0x02000056 RID: 86
		private class TaskInfoConverter : CommonConverter<object, object>
		{
			// Token: 0x060001B9 RID: 441 RVA: 0x000062A2 File Offset: 0x000044A2
			public TaskInfoConverter(Func<object> convertHandler)
			{
				this._convertHandler = convertHandler;
			}

			// Token: 0x060001BA RID: 442 RVA: 0x000062B1 File Offset: 0x000044B1
			protected override object convert(object value, object parameter)
			{
				return this._convertHandler();
			}

			// Token: 0x040000C7 RID: 199
			private Func<object> _convertHandler;
		}
	}
}
