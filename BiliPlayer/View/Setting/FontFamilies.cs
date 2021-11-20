using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace BiliPlayer.View.Setting
{
	// Token: 0x02000021 RID: 33
	internal class FontFamilies
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public static FontFamily[] ChineseFonts { get; } = (from i in FontFamilies._chineseFonts
		orderby i.Key
		select i.Value).ToArray<FontFamily>();

		// Token: 0x060000C4 RID: 196 RVA: 0x00003B61 File Offset: 0x00001D61
		private static IEnumerable<KeyValuePair<string, FontFamily>> getChineseFonts()
		{
			XmlLanguage zhcn = XmlLanguage.GetLanguage("zh-cn");
			foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
			{
				if (fontFamily.FamilyNames.ContainsKey(zhcn))
				{
					string key = "";
					fontFamily.FamilyNames.TryGetValue(zhcn, out key);
					yield return new KeyValuePair<string, FontFamily>(key, fontFamily);
				}
			}
			IEnumerator<FontFamily> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B6C File Offset: 0x00001D6C
		public static string GetDefaultFont(string desireFont)
		{
			if (FontFamilies._chineseFonts.ContainsKey(desireFont))
			{
				return desireFont;
			}
			return FontFamilies._chineseFonts.First<KeyValuePair<string, FontFamily>>().Key;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003B9C File Offset: 0x00001D9C
		public static string GetFontName(FontFamily font)
		{
			return FontFamilies._chineseFonts.First((KeyValuePair<string, FontFamily> i) => i.Value == font).Key;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public static FontFamily GetFont(string name)
		{
			return FontFamilies._chineseFonts[name];
		}

		// Token: 0x0400004C RID: 76
		private static Dictionary<string, FontFamily> _chineseFonts = FontFamilies.getChineseFonts().ToDictionary((KeyValuePair<string, FontFamily> i) => i.Key, (KeyValuePair<string, FontFamily> i) => i.Value);
	}
}
