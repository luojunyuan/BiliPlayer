using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BiliPlayer.DataSource;

namespace BiliPlayer.View.Player.Controls;

internal class InputOperation
{
	private class MouseClickGesture : InputGesture
	{
		private MouseButton _button;

		public MouseClickGesture(MouseButton button)
		{
			_button = button;
		}

		public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
		{
			return _button == (inputEventArgs as MouseButtonEventArgs)?.ChangedButton;
		}
	}

	private class MediaInputBinding : InputBinding
	{
	}

	private BulletCurtainController _controller;

	private FrameworkElement _element;

	public InputOperation(FrameworkElement element, BulletCurtainController mediaController)
	{
		_element = element;
		_controller = mediaController;
		addBinding(Key.Right, "FastForward");
		addBinding(Key.Left, "Rewind");
		addBinding(Key.Space, "TogglePlayPause");
		addBinding(MouseButton.XButton2, "FastForward");
		addBinding(MouseButton.XButton1, "Rewind");
		_element.AllowDrop = true;
		_element.DragEnter += onDragEnter;
		_element.Drop += onDrop;
	}

	private void addBinding(Key key, string command)
	{
		addBinding(new KeyGesture(key), command);
	}

	private void addBinding(MouseButton mouseButton, string command)
	{
		addBinding(new MouseClickGesture(mouseButton), command);
	}

	private void addBinding(InputGesture gesture, string command)
	{
		MediaInputBinding mediaInputBinding = new MediaInputBinding
		{
			Gesture = gesture
		};
		Binding binding = new Binding(command)
		{
			Source = _controller.Commands
		};
		BindingOperations.SetBinding(mediaInputBinding, InputBinding.CommandProperty, binding);
		_element.InputBindings.Add(mediaInputBinding);
	}

	private void onDragEnter(object sender, DragEventArgs e)
	{
		string text = (e.Data.GetData(DataFormats.FileDrop) as string[])?.FirstOrDefault();
		e.Effects = ((text != null) ? DragDropEffects.Copy : DragDropEffects.None);
	}

	private async void onDrop(object sender, DragEventArgs e)
	{
		if (e.Data.GetData(DataFormats.FileDrop) is string[] files)
		{
			_controller.Open(files);
			return;
		}
		string text = e.Data.GetData(DataFormats.Text) as string;
		if (text?.StartsWith("http") == true)
		{
			try
			{
				_controller.Message = "加载在线字幕...";
				string text2 = await TitleDownloader.DownlaodFile(text);
				_controller.Open(text2);
			}
			catch (Exception)
			{
			}
			_controller.Message = null;
		}
	}
}
