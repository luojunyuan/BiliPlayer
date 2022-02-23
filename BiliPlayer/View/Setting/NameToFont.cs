using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Setting;

internal class NameToFont : CommonConverter<string, FontFamily>
{
	protected override object convert(string value, object parameter)
	{
		return FontFamilies.GetFont(value);
	}

	protected override object convertBack(FontFamily value, object parameter)
	{
		return FontFamilies.GetFontName(value);
	}
}
