using BiliPlayer.Common.MVVM;

namespace BiliPlayer.View.Setting;

internal class SettingData : NotifyItem
{
	private double _fontScale = 1.0;

	private double _opacity = 1.0;

	private string _FontFamily = FontFamilies.GetDefaultFont("微软雅黑");

	private bool _hasStroke = true;

	private bool _hasShadow = true;

	private bool _isBold = true;

	public static SettingData Instance { get; } = new SettingData();


	public double FontScale
	{
		get
		{
			return _fontScale;
		}
		set
		{
			_fontScale = value;
			notifySettingChanged();
		}
	}

	public double Opacity
	{
		get
		{
			return _opacity;
		}
		set
		{
			_opacity = value;
			onPropertyChanged("Opacity");
		}
	}

	public string FontFamily
	{
		get
		{
			return _FontFamily;
		}
		set
		{
			_FontFamily = value;
			notifySettingChanged();
		}
	}

	public bool HasStroke
	{
		get
		{
			return _hasStroke;
		}
		set
		{
			_hasStroke = value;
			notifySettingChanged();
		}
	}

	public bool HasShadow
	{
		get
		{
			return _hasShadow;
		}
		set
		{
			_hasShadow = value;
			notifySettingChanged();
		}
	}

	public bool IsBold
	{
		get
		{
			return _isBold;
		}
		set
		{
			_isBold = value;
			notifySettingChanged();
		}
	}

	public object SettingId { get; private set; }

	private SettingData()
	{
	}

	private void notifySettingChanged()
	{
		SettingId = new object();
		onPropertyChanged("SettingId");
	}
}
