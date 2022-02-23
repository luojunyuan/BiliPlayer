using System.Collections.Generic;
using BiliPlayer.DataSource.Bili;
using BiliPlayer.View.Subtitles;

namespace BiliPlayer.DataSource;

internal class TitleParser
{
	public static IEnumerable<TitleInfo> Parse()
	{
		return Parse("F:\\共享\\乐园追放\\subtitle - 副本.xml");
	}

	public static IEnumerable<TitleInfo> Parse(string file)
	{
		if (string.IsNullOrEmpty(file))
		{
			return new TitleInfo[0];
		}
		return BiliPlayer.DataSource.Bili.TitleParser.Parse(file);
	}
}
