using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace BiliPlayer.View.Setting;

internal class FontFamilies
{
	private static Dictionary<string, FontFamily> _chineseFonts;

	public static FontFamily[] ChineseFonts { get; }

	static FontFamilies()
	{
		_chineseFonts = getChineseFonts().ToDictionary((KeyValuePair<string, FontFamily> i) => i.Key, (KeyValuePair<string, FontFamily> i) => i.Value);
		ChineseFonts = (from i in _chineseFonts
			orderby i.Key
			select i.Value).ToArray();
	}

	private static IEnumerable<KeyValuePair<string, FontFamily>> getChineseFonts()
	{
		XmlLanguage zhcn = XmlLanguage.GetLanguage("zh-cn");
		foreach (FontFamily systemFontFamily in Fonts.SystemFontFamilies)
		{
			if (systemFontFamily.FamilyNames.ContainsKey(zhcn))
			{
				string value = "";
				systemFontFamily.FamilyNames.TryGetValue(zhcn, out value);
				yield return new KeyValuePair<string, FontFamily>(value, systemFontFamily);
			}
		}
	}

	public static string GetDefaultFont(string desireFont)
	{
		if (_chineseFonts.ContainsKey(desireFont))
		{
			return desireFont;
		}
		return _chineseFonts.First().Key;
	}

	public static string GetFontName(FontFamily font)
	{
		return _chineseFonts.First((KeyValuePair<string, FontFamily> i) => i.Value == font).Key;
	}

	public static FontFamily GetFont(string name)
	{
		return _chineseFonts[name];
	}
}
