using System;
using BiliPlayer.Util;

namespace BiliPlayer.View.Setting
{
	// Token: 0x0200001E RID: 30
	internal class OpacityConverter : CommonConverter<double, double>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003A8D File Offset: 0x00001C8D
		protected override object convertBack(double value, object parameter)
		{
			return value / 100.0;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003AA0 File Offset: 0x00001CA0
		protected override object convert(double value, object parameter)
		{
			return (int)(value * 100.0);
		}
	}
}
