using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using BiliPlayer.Common.Data;
using BiliPlayer.View.Subtitles;

namespace BiliPlayer.DataSource.Bili
{
	// Token: 0x02000034 RID: 52
	internal class TitleParser
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004823 File Offset: 0x00002A23
		public static IEnumerable<TitleInfo> Parse(string file)
		{
			return TitleParser.parse(file).Select(new Func<TitleData, TitleInfo>(TitleParser.convert));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000483C File Offset: 0x00002A3C
		private static TitleData[] parse(string file)
		{
			TitleData[] array = (from i in XElement.Load(file).Elements("d")
			let content = i.Value
			let para = i.Attribute("p").Value
			let data = TitleParser.parse(content, para)
			where data != null
			orderby data.Start
			select data).ToArray<TitleData>();
			ChannelAllocator.assignChannelNo(array);
			return array;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004944 File Offset: 0x00002B44
		private static TitleInfo convert(TitleData title)
		{
			return new TitleInfo
			{
				Text = title.Content,
				Fill = title.Foreground,
				Stroke = Brushes.Black,
				Start = title.Start,
				End = title.End,
				IsDarkColor = title.IsDarkColor,
				ChannelNo = title.ChannelNo,
				ScrollMode = title.ScrollMode
			};
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000049B8 File Offset: 0x00002BB8
		private static TitleData parse(string content, string paraData)
		{
			string[] array = paraData.Split(new char[]
			{
				','
			});
			TimeSpan timeSpan = TimeSpan.FromSeconds(double.Parse(array[0]));
			ScrollMode? scrollMode = TitleParser.parseMode(int.Parse(array[1]));
			if (scrollMode == null)
			{
				return null;
			}
			SolidColorBrush colorBrush = TitleParser.getColorBrush(int.Parse(array[3]));
			int num = (66 * colorBrush.Color.R + 129 * colorBrush.Color.G + 25 * colorBrush.Color.B + 128 >> 8) + 16;
			TimeSpan t = TimeSpan.FromSeconds((double)(scrollMode.Value.IsFixedTitle() ? 8 : TitleParser._rnd.Next(10, 20)));
			return new TitleData
			{
				Content = content,
				Foreground = colorBrush,
				ScrollMode = scrollMode.Value,
				Start = timeSpan,
				End = timeSpan + t,
				IsDarkColor = (num < 60)
			};
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004ABC File Offset: 0x00002CBC
		private static ScrollMode? parseMode(int mode)
		{
			switch (mode)
			{
			case 1:
			case 2:
			case 3:
				return new ScrollMode?(ScrollMode.Horizontal);
			case 4:
				return new ScrollMode?(ScrollMode.Bottom);
			case 5:
				return new ScrollMode?(ScrollMode.Top);
			case 6:
				return new ScrollMode?(ScrollMode.Reverse);
			default:
				return null;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004B10 File Offset: 0x00002D10
		private static SolidColorBrush getColorBrush(int color)
		{
			SolidColorBrush result;
			if (TitleParser._colorDictionary.TryGetValue(color, out result))
			{
				return result;
			}
			byte[] array = BitConverter.GetBytes(color).Reverse<byte>().ToArray<byte>();
			SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(byte.MaxValue, array[1], array[2], array[3]));
			solidColorBrush.Freeze();
			TitleParser._colorDictionary[color] = solidColorBrush;
			return solidColorBrush;
		}

		// Token: 0x04000069 RID: 105
		private static Random _rnd = new Random();

		// Token: 0x0400006A RID: 106
		private static Dictionary<int, SolidColorBrush> _colorDictionary = new Dictionary<int, SolidColorBrush>();
	}
}
