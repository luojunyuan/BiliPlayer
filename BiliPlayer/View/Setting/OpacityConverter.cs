using BiliPlayer.Util;

namespace BiliPlayer.View.Setting;

internal class OpacityConverter : CommonConverter<double, double>
{
	protected override object convertBack(double value, object parameter)
	{
		return value / 100.0;
	}

	protected override object convert(double value, object parameter)
	{
		return (int)(value * 100.0);
	}
}
