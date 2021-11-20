using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiliPlayer.DataSource.Bili
{
	// Token: 0x02000031 RID: 49
	internal class BiliTitleDownloader
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000440C File Offset: 0x0000260C
		public static async Task<string> DownloadContent(string code)
		{
			string arg = await BiliTitleDownloader.getCid(code);
			string requestUri = string.Format("http://comment.bilibili.com/{0}.xml", arg);
			return await new HttpClient(new HttpClientHandler
			{
				Proxy = null,
				AutomaticDecompression = DecompressionMethods.Deflate
			}).GetStringAsync(requestUri);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004454 File Offset: 0x00002654
		private static async Task<string> getCid(string code)
		{
			string requestUri = string.Format("http://www.bilibili.com/video/{0}", code);
			string input = await new HttpClient(new HttpClientHandler
			{
				Proxy = null,
				AutomaticDecompression = DecompressionMethods.GZip,
				AllowAutoRedirect = true
			}).GetStringAsync(requestUri);
			Match match = Regex.Match(input, "cid=(\\d+)");
			string result;
			if (match.Success)
			{
				result = match.Groups[1].Value;
			}
			else
			{
				MatchCollection matchCollection = Regex.Matches(input, "option value='/video/(.+?)'>(.+?)<");
				if (matchCollection.Count <= 1)
				{
					throw new InvalidOperationException("解析失败");
				}
				code = matchCollection[1].Groups[1].Value;
				result = await BiliTitleDownloader.getCid(code);
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000449C File Offset: 0x0000269C
		public static async Task<string> DownlaodFile(string address)
		{
			Match match = Regex.Match(address, "av\\d+", RegexOptions.IgnoreCase);
			if (!match.Success)
			{
				throw new ArgumentException("地址格式不合法");
			}
			string text = await BiliTitleDownloader.DownloadContent(match.Value);
			if (text == null)
			{
				throw new InvalidOperationException("下载失败");
			}
			string text2 = Path.GetTempFileName() + ".xml";
			File.WriteAllText(text2, text, Encoding.UTF8);
			return text2;
		}
	}
}
