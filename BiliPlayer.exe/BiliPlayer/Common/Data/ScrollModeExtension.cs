using System;

namespace BiliPlayer.Common.Data
{
	// Token: 0x02000038 RID: 56
	internal static class ScrollModeExtension
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00004CC7 File Offset: 0x00002EC7
		public static bool IsFixedTitle(this ScrollMode mode)
		{
			return mode == ScrollMode.Bottom || mode == ScrollMode.Top;
		}
	}
}
