using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BiliPlayer.DataSource.Bili;

internal class BiliTitleDownloader
{
	public static async Task<string> DownloadContent(string code)
	{
		string requestUri = $"http://comment.bilibili.com/{await getCid(code)}.xml";
		return await new HttpClient(new HttpClientHandler
		{
			Proxy = null,
			AutomaticDecompression = DecompressionMethods.Deflate
		}).GetStringAsync(requestUri);
	}

	private static async Task<string> getCid(string code)
	{
		string requestUri = $"http://www.bilibili.com/video/{code}";
		string input = await new HttpClient(new HttpClientHandler
		{
			Proxy = null,
			AutomaticDecompression = DecompressionMethods.GZip,
			AllowAutoRedirect = true
		}).GetStringAsync(requestUri);
		Match match = Regex.Match(input, "cid=(\\d+)");
		if (match.Success)
		{
			return match.Groups[1].Value;
		}
		MatchCollection matchCollection = Regex.Matches(input, "option value='/video/(.+?)'>(.+?)<");
		if (matchCollection.Count > 1)
		{
			code = matchCollection[1].Groups[1].Value;
			return await getCid(code);
		}
		throw new InvalidOperationException("解析失败");
	}

	public static async Task<string> DownlaodFile(string address)
	{
		Match match = Regex.Match(address, "av\\d+", RegexOptions.IgnoreCase);
		if (!match.Success)
		{
			throw new ArgumentException("地址格式不合法");
		}
		string text = await DownloadContent(match.Value);
		if (text == null)
		{
			throw new InvalidOperationException("下载失败");
		}
		string text2 = Path.GetTempFileName() + ".xml";
		File.WriteAllText(text2, text, Encoding.UTF8);
		return text2;
	}
}
