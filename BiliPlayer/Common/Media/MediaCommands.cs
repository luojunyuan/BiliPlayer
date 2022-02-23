using System;
using BiliPlayer.Common.MVVM;
using Microsoft.Win32;

namespace BiliPlayer.Common.Media;

internal class MediaCommands : NotifyItem
{
	protected MediaController controller;

	public DelegateCommand TogglePlayPause { get; private set; }

	public DelegateCommand Stop { get; private set; }

	public DelegateCommand FastForward { get; private set; }

	public DelegateCommand Rewind { get; private set; }

	public DelegateCommand Seek { get; private set; }

	public DelegateCommand Open { get; protected set; }

	public MediaCommands(MediaController mediaController)
	{
		controller = mediaController;
		TogglePlayPause = new DelegateCommand(onPlayOrPause);
		Stop = new DelegateCommand(onStop);
		Seek = new DelegateCommand(onSeek);
		FastForward = new DelegateCommand(delegate
		{
			goForward(TimeSpan.FromSeconds(30.0));
		});
		Rewind = new DelegateCommand(delegate
		{
			goForward(TimeSpan.FromSeconds(-30.0));
		});
		Open = new DelegateCommand(onOpen);
	}

	private void onOpen(object para)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog
		{
			Multiselect = false
		};
		if (openFileDialog.ShowDialog() == true)
		{
			controller.Source = new Uri(openFileDialog.FileName);
			controller.Begin();
		}
	}

	private void onPlayOrPause(object para)
	{
		switch (controller.State)
		{
		case MediaState.Stopped:
			controller.Begin();
			break;
		case MediaState.Playing:
			controller.Pause();
			break;
		case MediaState.Paused:
			controller.Resume();
			break;
		}
	}

	private void onStop(object para)
	{
		controller.Stop();
	}

	private void onSeek(object para)
	{
		TimeSpan offset = (TimeSpan)para;
		controller.Seek(offset);
	}

	private void goForward(TimeSpan timeSpan)
	{
		TimeSpan? timeSpan2 = controller.CurrentTime + timeSpan;
		if (timeSpan2 > controller.Duration)
		{
			timeSpan2 = controller.Duration;
		}
		if (timeSpan2 < TimeSpan.Zero)
		{
			timeSpan2 = TimeSpan.Zero;
		}
		if (timeSpan2.HasValue)
		{
			onSeek(timeSpan2);
		}
	}
}
