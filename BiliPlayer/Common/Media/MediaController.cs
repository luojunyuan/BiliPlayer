using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BiliPlayer.Common.MVVM;

namespace BiliPlayer.Common.Media;

internal class MediaController : NotifyItem
{
	private class MediaStoryboard : Storyboard
	{
		private Clock _clock;

		public MediaState State
		{
			get
			{
				if (_clock == null)
				{
					return MediaState.Stopped;
				}
				if (_clock.IsPaused)
				{
					return MediaState.Paused;
				}
				if (_clock.CurrentState == ClockState.Stopped)
				{
					return MediaState.Stopped;
				}
				return MediaState.Playing;
			}
		}

		protected override Clock AllocateClock()
		{
			_clock = base.AllocateClock();
			return _clock;
		}
	}

	private MediaStoryboard _storyboard;

	protected MediaTimeline _mediaTimeline;

	protected MediaElement _mediaElement;

	private Timer _updateTimer;

	private TimeSpan? duration;

	private TimeSpan? currentTime;

	public TimelineGroup TimelineGroup => _storyboard;

	public Uri Source
	{
		get
		{
			return _mediaTimeline.Source;
		}
		set
		{
			_mediaTimeline.Source = value;
		}
	}

	public double SpeedRatio
	{
		get
		{
			return _storyboard.SpeedRatio;
		}
		set
		{
			_storyboard.SpeedRatio = value;
		}
	}

	public double Volume
	{
		get
		{
			return _mediaElement.Volume;
		}
		set
		{
			_mediaElement.Volume = value;
		}
	}

	public MediaState State => _storyboard.State;

	public TimeSpan? Duration
	{
		get
		{
			return duration;
		}
		private set
		{
			duration = value;
			onPropertyChanged("Duration");
		}
	}

	public TimeSpan? CurrentTime
	{
		get
		{
			return currentTime;
		}
		private set
		{
			currentTime = value;
			onPropertyChanged("CurrentTime");
		}
	}

	public MediaController(MediaElement mediaElement)
	{
		MediaController mediaController = this;
		_storyboard = new MediaStoryboard
		{
			SlipBehavior = SlipBehavior.Slip
		};
		mediaElement.Volume = 0.8;
		_mediaElement = mediaElement;
		_mediaTimeline = new MediaTimeline();
		Storyboard.SetTarget(_mediaTimeline, mediaElement);
		_storyboard.Children.Add(_mediaTimeline);
		mediaElement.MediaOpened += delegate
		{
			Duration naturalDuration = mediaElement.NaturalDuration;
			if (naturalDuration.HasTimeSpan)
			{
				mediaController.Duration = naturalDuration.TimeSpan;
			}
		};
		_updateTimer = new Timer(delegate
		{
			updateCurrentTime();
		}, null, 1000, 1000);
		_mediaTimeline.CurrentStateInvalidated += delegate
		{
			notifyStateChanged();
		};
	}

	public void Begin()
	{
		if (!(Source == null))
		{
			_storyboard.Begin();
			notifyStateChanged();
		}
	}

	public void Stop()
	{
		_storyboard.Stop();
		notifyStateChanged();
	}

	public void Seek(TimeSpan offset)
	{
		_storyboard.Seek(offset);
	}

	public void Pause()
	{
		_storyboard.Pause();
		notifyStateChanged();
	}

	public void Resume()
	{
		_storyboard.Resume();
		notifyStateChanged();
	}

	private async void notifyStateChanged()
	{
		await Task.Delay(100);
		onPropertyChanged("State");
	}

	private void updateCurrentTime()
	{
		if (State == MediaState.Playing)
		{
			CurrentTime = _storyboard.GetCurrentTime();
		}
	}
}
