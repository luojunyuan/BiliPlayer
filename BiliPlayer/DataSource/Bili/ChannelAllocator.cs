using System;
using System.Collections.Generic;
using System.Linq;
using BiliPlayer.Common.Data;

namespace BiliPlayer.DataSource.Bili;

internal class ChannelAllocator
{
	private static TitleData placeholder;

	private static Random _rnd;

	static ChannelAllocator()
	{
		_rnd = new Random();
		placeholder = new TitleData();
	}

	public static void assignChannelNo(TitleData[] titles)
	{
		int num = 20;
		List<TitleData> list = new List<TitleData>();
		for (int j = 0; j < num; j++)
		{
			list.Add(placeholder);
		}
		foreach (TitleData item in titles.Where((TitleData i) => !i.ScrollMode.IsFixedTitle()))
		{
			int num2 = 0;
			num2 = (from i in list.Select((TitleData data, int index) => new
				{
					Index = index,
					Data = data,
					TimeSpan = item.Start - data.End
				})
				orderby i.TimeSpan descending
				select i).Take(num / 2).ToArray()[_rnd.Next(num / 2)].Index;
			item.ChannelNo = num2;
			list[num2] = item;
		}
		assignFixedChannels(titles.Where((TitleData i) => i.ScrollMode == ScrollMode.Top), num);
		assignFixedChannels(titles.Where((TitleData i) => i.ScrollMode == ScrollMode.Bottom), num);
	}

	private static void assignFixedChannels(IEnumerable<TitleData> titles, int channelCount)
	{
		List<TitleData> list = new List<TitleData>();
		for (int j = 0; j < channelCount; j++)
		{
			list.Add(placeholder);
		}
		foreach (TitleData item in titles)
		{
			var source = list.Select((TitleData data, int index) => new
			{
				Index = index,
				Data = data,
				TimeSpan = item.Start - data.End
			}).ToArray();
			int? num = source.Where(i => i.TimeSpan > TimeSpan.Zero).FirstOrDefault()?.Index;
			if (!num.HasValue)
			{
				num = source.OrderBy(i => i.TimeSpan).First().Index;
			}
			item.ChannelNo = num.Value;
			list[num.Value] = item;
		}
	}
}
