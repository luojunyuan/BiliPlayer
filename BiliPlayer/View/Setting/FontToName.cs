using System;
using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Setting
{
	// Token: 0x0200001F RID: 31
	internal class FontToName : CommonConverter<FontFamily, string>
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003ABB File Offset: 0x00001CBB
		protected override object convert(FontFamily value, object parameter)
		{
			return FontFamilies.GetFontName(value);
		}
	}
}
