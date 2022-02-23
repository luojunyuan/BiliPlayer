using System;
using System.ComponentModel;
using System.Windows;

namespace BiliPlayer.Util;

internal class WindowUtil
{
	private static Lazy<bool> _isInDesignMode = new Lazy<bool>(isInDesignMode);

	public static bool IsInDesignMode()
	{
		return _isInDesignMode.Value;
	}

	private static bool isInDesignMode()
	{
		return (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
	}
}
