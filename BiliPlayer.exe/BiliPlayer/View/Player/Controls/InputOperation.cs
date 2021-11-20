using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BiliPlayer.DataSource;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000027 RID: 39
	internal class InputOperation
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003E80 File Offset: 0x00002080
		public InputOperation(FrameworkElement element, BulletCurtainController mediaController)
		{
			this._element = element;
			this._controller = mediaController;
			this.addBinding(Key.Right, "FastForward");
			this.addBinding(Key.Left, "Rewind");
			this.addBinding(Key.Space, "TogglePlayPause");
			this.addBinding(MouseButton.XButton2, "FastForward");
			this.addBinding(MouseButton.XButton1, "Rewind");
			this._element.AllowDrop = true;
			this._element.DragEnter += this.onDragEnter;
			this._element.Drop += this.onDrop;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003F1A File Offset: 0x0000211A
		private void addBinding(Key key, string command)
		{
			this.addBinding(new KeyGesture(key), command);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003F29 File Offset: 0x00002129
		private void addBinding(MouseButton mouseButton, string command)
		{
			this.addBinding(new InputOperation.MouseClickGesture(mouseButton), command);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003F38 File Offset: 0x00002138
		private void addBinding(InputGesture gesture, string command)
		{
			InputOperation.MediaInputBinding mediaInputBinding = new InputOperation.MediaInputBinding
			{
				Gesture = gesture
			};
			Binding binding = new Binding(command)
			{
				Source = this._controller.Commands
			};
			BindingOperations.SetBinding(mediaInputBinding, InputBinding.CommandProperty, binding);
			this._element.InputBindings.Add(mediaInputBinding);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003F8C File Offset: 0x0000218C
		private void onDragEnter(object sender, DragEventArgs e)
		{
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			e.Effects = ((((array != null) ? array.FirstOrDefault<string>() : null) != null) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003FC8 File Offset: 0x000021C8
		private async void onDrop(object sender, DragEventArgs e)
		{
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (array != null)
			{
				this._controller.Open(array);
			}
			else
			{
				string text = e.Data.GetData(DataFormats.Text) as string;
				string text2 = text;
				if (((text2 != null) ? new bool?(text2.StartsWith("http")) : null) == true)
				{
					try
					{
						this._controller.Message = "加载在线字幕...";
						string text3 = await TitleDownloader.DownlaodFile(text);
						this._controller.Open(new string[]
						{
							text3
						});
					}
					catch (Exception)
					{
					}
					this._controller.Message = null;
				}
			}
		}

		// Token: 0x04000058 RID: 88
		private BulletCurtainController _controller;

		// Token: 0x04000059 RID: 89
		private FrameworkElement _element;

		// Token: 0x02000053 RID: 83
		private class MouseClickGesture : InputGesture
		{
			// Token: 0x060001B4 RID: 436 RVA: 0x00006074 File Offset: 0x00004274
			public MouseClickGesture(MouseButton button)
			{
				this._button = button;
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00006084 File Offset: 0x00004284
			public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
			{
				MouseButtonEventArgs mouseButtonEventArgs = inputEventArgs as MouseButtonEventArgs;
				MouseButton button = this._button;
				return button == ((mouseButtonEventArgs != null) ? new MouseButton?(mouseButtonEventArgs.ChangedButton) : null);
			}

			// Token: 0x040000C1 RID: 193
			private MouseButton _button;
		}

		// Token: 0x02000054 RID: 84
		private class MediaInputBinding : InputBinding
		{
		}
	}
}
