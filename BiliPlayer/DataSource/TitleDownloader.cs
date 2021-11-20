using System;
using System.Threading.Tasks;
using BiliPlayer.DataSource.Bili;

namespace BiliPlayer.DataSource
{
	// Token: 0x0200002F RID: 47
	internal class TitleDownloader
	{
		// Token: 0x06000101 RID: 257 RVA: 0x000043D0 File Offset: 0x000025D0
		public static Task<string> DownlaodFile(string key)
		{
			return BiliTitleDownloader.DownlaodFile(key);
		}
	}
}
