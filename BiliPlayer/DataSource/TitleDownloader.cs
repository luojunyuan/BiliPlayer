using System.Threading.Tasks;
using BiliPlayer.DataSource.Bili;

namespace BiliPlayer.DataSource;

internal class TitleDownloader
{
	public static Task<string> DownlaodFile(string key)
	{
		return BiliTitleDownloader.DownlaodFile(key);
	}
}
