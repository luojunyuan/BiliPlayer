using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Setting;

internal class FontToName : CommonConverter<FontFamily, string>
{
	protected override object convert(FontFamily value, object parameter)
	{
		return FontFamilies.GetFontName(value);
	}
}
