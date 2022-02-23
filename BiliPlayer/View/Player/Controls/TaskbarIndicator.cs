using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls;

[Export]
internal class TaskbarIndicator
{
	private class TaskInfoConverter : CommonConverter<object, object>
	{
		private Func<object> _convertHandler;

		public TaskInfoConverter(Func<object> convertHandler)
		{
			_convertHandler = convertHandler;
		}

		protected override object convert(object value, object parameter)
		{
			return _convertHandler();
		}
	}

	[Import]
	private BulletCurtainController _media;

	public void Regist(Window window)
	{
		TaskbarItemInfo taskbarItemInfo = new TaskbarItemInfo();
		taskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
		setBinding(taskbarItemInfo, TaskbarItemInfo.ProgressValueProperty, "CurrentTime", getProgress);
		setBinding(taskbarItemInfo, TaskbarItemInfo.ProgressStateProperty, "State", getState);
		window.TaskbarItemInfo = taskbarItemInfo;
	}

	private void setBinding(TaskbarItemInfo taskBarItemInfo, DependencyProperty proeprty, string property, Func<object> convertHandler)
	{
		BindingOperations.SetBinding(taskBarItemInfo, proeprty, new Binding(property)
		{
			Source = _media,
			Converter = new TaskInfoConverter(convertHandler)
		});
	}

	private object getProgress()
	{
		return (_media.CurrentTime?.TotalSeconds / _media.Duration?.TotalSeconds) ?? 0.0;
	}

	private object getState()
	{
		return _media.State switch
		{
			MediaState.Stopped => TaskbarItemProgressState.None, 
			MediaState.Playing => TaskbarItemProgressState.Normal, 
			MediaState.Paused => TaskbarItemProgressState.Paused, 
			_ => TaskbarItemProgressState.None, 
		};
	}
}
