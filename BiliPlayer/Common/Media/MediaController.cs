using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BiliPlayer.Common.MVVM;

namespace BiliPlayer.Common.Media
{
	// Token: 0x02000040 RID: 64
	internal class MediaController : NotifyItem
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000522E File Offset: 0x0000342E
		public TimelineGroup TimelineGroup
		{
			get
			{
				return this._storyboard;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005238 File Offset: 0x00003438
		public MediaController(MediaElement mediaElement)
		{
			MediaController __this = this;
			this._storyboard = new MediaController.MediaStoryboard
			{
				SlipBehavior = SlipBehavior.Slip
			};
			mediaElement.Volume = 0.8;
			this._mediaElement = mediaElement;
			this._mediaTimeline = new MediaTimeline();
			Storyboard.SetTarget(this._mediaTimeline, mediaElement);
			this._storyboard.Children.Add(this._mediaTimeline);
			mediaElement.MediaOpened += delegate(object s, RoutedEventArgs e)
			{
				Duration naturalDuration = mediaElement.NaturalDuration;
				if (naturalDuration.HasTimeSpan)
				{
					__this.Duration = new TimeSpan?(naturalDuration.TimeSpan);
				}
			};
			this._updateTimer = new Timer(delegate(object _)
			{
				this.updateCurrentTime();
			}, null, 1000, 1000);
			this._mediaTimeline.CurrentStateInvalidated += delegate(object s, EventArgs e)
			{
				this.notifyStateChanged();
			};
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005313 File Offset: 0x00003513
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00005320 File Offset: 0x00003520
		public Uri Source
		{
			get
			{
				return this._mediaTimeline.Source;
			}
			set
			{
				this._mediaTimeline.Source = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000532E File Offset: 0x0000352E
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000533B File Offset: 0x0000353B
		public double SpeedRatio
		{
			get
			{
				return this._storyboard.SpeedRatio;
			}
			set
			{
				this._storyboard.SpeedRatio = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005349 File Offset: 0x00003549
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00005356 File Offset: 0x00003556
		public double Volume
		{
			get
			{
				return this._mediaElement.Volume;
			}
			set
			{
				this._mediaElement.Volume = value;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005364 File Offset: 0x00003564
		public void Begin()
		{
			if (this.Source == null)
			{
				return;
			}
			this._storyboard.Begin();
			this.notifyStateChanged();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005386 File Offset: 0x00003586
		public void Stop()
		{
			this._storyboard.Stop();
			this.notifyStateChanged();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005399 File Offset: 0x00003599
		public void Seek(TimeSpan offset)
		{
			this._storyboard.Seek(offset);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000053A7 File Offset: 0x000035A7
		public void Pause()
		{
			this._storyboard.Pause();
			this.notifyStateChanged();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000053BA File Offset: 0x000035BA
		public void Resume()
		{
			this._storyboard.Resume();
			this.notifyStateChanged();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000053D0 File Offset: 0x000035D0
		private async void notifyStateChanged()
		{
			await Task.Delay(100);
			this.onPropertyChanged("State");
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005409 File Offset: 0x00003609
		public MediaState State
		{
			get
			{
				return this._storyboard.State;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005416 File Offset: 0x00003616
		private void updateCurrentTime()
		{
			if (this.State == MediaState.Playing)
			{
				this.CurrentTime = new TimeSpan?(this._storyboard.GetCurrentTime());
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005437 File Offset: 0x00003637
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000543F File Offset: 0x0000363F
		public TimeSpan? Duration
		{
			get
			{
				return this.duration;
			}
			private set
			{
				this.duration = value;
				base.onPropertyChanged("Duration");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005453 File Offset: 0x00003653
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000545B File Offset: 0x0000365B
		public TimeSpan? CurrentTime
		{
			get
			{
				return this.currentTime;
			}
			private set
			{
				this.currentTime = value;
				base.onPropertyChanged("CurrentTime");
			}
		}

		// Token: 0x04000081 RID: 129
		private MediaController.MediaStoryboard _storyboard;

		// Token: 0x04000082 RID: 130
		protected MediaTimeline _mediaTimeline;

		// Token: 0x04000083 RID: 131
		protected MediaElement _mediaElement;

		// Token: 0x04000084 RID: 132
		private Timer _updateTimer;

		// Token: 0x04000085 RID: 133
		private TimeSpan? duration;

		// Token: 0x04000086 RID: 134
		private TimeSpan? currentTime;

		// Token: 0x02000061 RID: 97
		private class MediaStoryboard : Storyboard
		{
			// Token: 0x060001DD RID: 477 RVA: 0x000068B5 File Offset: 0x00004AB5
			protected override Clock AllocateClock()
			{
				this._clock = base.AllocateClock();
				return this._clock;
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060001DE RID: 478 RVA: 0x000068C9 File Offset: 0x00004AC9
			public MediaState State
			{
				get
				{
					if (this._clock == null)
					{
						return MediaState.Stopped;
					}
					if (this._clock.IsPaused)
					{
						return MediaState.Paused;
					}
					if (this._clock.CurrentState == ClockState.Stopped)
					{
						return MediaState.Stopped;
					}
					return MediaState.Playing;
				}
			}

			// Token: 0x040000E7 RID: 231
			private Clock _clock;
		}
	}
}
