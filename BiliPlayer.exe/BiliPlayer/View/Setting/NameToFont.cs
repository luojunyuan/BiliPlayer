using System;
using System.Windows.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Setting
{
	// Token: 0x02000020 RID: 32
	internal class NameToFont : CommonConverter<string, FontFamily>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003ACB File Offset: 0x00001CCB
		protected override object convert(string value, object parameter)
		{
			return FontFamilies.GetFont(value);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003ABB File Offset: 0x00001CBB
		protected override object convertBack(FontFamily value, object parameter)
		{
			return FontFamilies.GetFontName(value);
		}
	}
}
