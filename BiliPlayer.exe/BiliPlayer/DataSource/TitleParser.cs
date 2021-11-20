using System;
using System.Collections.Generic;
using BiliPlayer.DataSource.Bili;
using BiliPlayer.View.Subtitles;

namespace BiliPlayer.DataSource
{
	// Token: 0x02000030 RID: 48
	internal class TitleParser
	{
		// Token: 0x06000103 RID: 259 RVA: 0x000043E0 File Offset: 0x000025E0
		public static IEnumerable<TitleInfo> Parse()
		{
			return TitleParser.Parse("F:\\共享\\乐园追放\\subtitle - 副本.xml");
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000043EC File Offset: 0x000025EC
		public static IEnumerable<TitleInfo> Parse(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				return new TitleInfo[0];
			}
			return TitleParser.Parse(file);
		}
	}
}
