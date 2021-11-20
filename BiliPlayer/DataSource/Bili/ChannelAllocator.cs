using System;
using System.Collections.Generic;
using System.Linq;
using BiliPlayer.Common.Data;

namespace BiliPlayer.DataSource.Bili
{
	// Token: 0x02000032 RID: 50
	internal class ChannelAllocator
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000044E9 File Offset: 0x000026E9
		static ChannelAllocator()
		{
			ChannelAllocator.placeholder = new TitleData();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004500 File Offset: 0x00002700
		public static void assignChannelNo(TitleData[] titles)
		{
			int num = 20;
			List<TitleData> list = new List<TitleData>();
			for (int j = 0; j < num; j++)
			{
				list.Add(ChannelAllocator.placeholder);
			}
			using (IEnumerator<TitleData> enumerator = (from i in titles
			where !i.ScrollMode.IsFixedTitle()
			select i).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TitleData item = enumerator.Current;
					int index2 = (from i in list.Select((TitleData data, int index) => new
					{
						Index = index,
						Data = data,
						TimeSpan = item.Start - data.End
					})
					orderby i.TimeSpan descending
					select i).Take(num / 2).ToArray()[ChannelAllocator._rnd.Next(num / 2)].Index;
					item.ChannelNo = index2;
					list[index2] = item;
				}
			}
			ChannelAllocator.assignFixedChannels(from i in titles
			where i.ScrollMode == ScrollMode.Top
			select i, num);
			ChannelAllocator.assignFixedChannels(from i in titles
			where i.ScrollMode == ScrollMode.Bottom
			select i, num);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004668 File Offset: 0x00002868
		private static void assignFixedChannels(IEnumerable<TitleData> titles, int channelCount)
		{
			List<TitleData> list = new List<TitleData>();
			for (int j = 0; j < channelCount; j++)
			{
				list.Add(ChannelAllocator.placeholder);
			}
			using (IEnumerator<TitleData> enumerator = titles.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TitleData item = enumerator.Current;
					var source = list.Select((TitleData data, int index) => new
					{
						Index = index,
						Data = data,
						TimeSpan = item.Start - data.End
					}).ToArray();
					var f__AnonymousType = (from i in source
					where i.TimeSpan > TimeSpan.Zero
					select i).FirstOrDefault();
					int? num = (f__AnonymousType != null) ? new int?(f__AnonymousType.Index) : null;
					if (num == null)
					{
						num = new int?((from i in source
						orderby i.TimeSpan
						select i).First().Index);
					}
					item.ChannelNo = num.Value;
					list[num.Value] = item;
				}
			}
		}

		// Token: 0x04000060 RID: 96
		private static TitleData placeholder;

		// Token: 0x04000061 RID: 97
		private static Random _rnd = new Random();
	}
}
