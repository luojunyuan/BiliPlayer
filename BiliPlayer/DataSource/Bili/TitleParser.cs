using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;
using BiliPlayer.Common.Data;
using BiliPlayer.View.Subtitles;

namespace BiliPlayer.DataSource.Bili;

internal class TitleParser
{
	private static Random _rnd = new Random();

	private static Dictionary<int, SolidColorBrush> _colorDictionary = new Dictionary<int, SolidColorBrush>();

	public static IEnumerable<TitleInfo> Parse(string file)
	{
		return parse(file).Select(convert);
	}

	private static TitleData[] parse(string file)
	{
		TitleData[] array = (from i in XElement.Load(file).Elements("d")
			let content = i.Value
			let para = i.Attribute("p").Value
			let data = parse(content, para)
			where data != null
			orderby data.Start
			select data).ToArray();
		ChannelAllocator.assignChannelNo(array);
		return array;
	}

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

	private static TitleData parse(string content, string paraData)
	{
		string[] array = paraData.Split(',');
		TimeSpan timeSpan = TimeSpan.FromSeconds(double.Parse(array[0]));
		ScrollMode? scrollMode = parseMode(int.Parse(array[1]));
		if (!scrollMode.HasValue)
		{
			return null;
		}
		SolidColorBrush colorBrush = getColorBrush(int.Parse(array[3]));
		int num = (66 * colorBrush.Color.R + 129 * colorBrush.Color.G + 25 * colorBrush.Color.B + 128 >> 8) + 16;
		TimeSpan timeSpan2 = TimeSpan.FromSeconds(scrollMode.Value.IsFixedTitle() ? 8 : _rnd.Next(10, 20));
		return new TitleData
		{
			Content = content,
			Foreground = colorBrush,
			ScrollMode = scrollMode.Value,
			Start = timeSpan,
			End = timeSpan + timeSpan2,
			IsDarkColor = (num < 60)
		};
	}

	private static ScrollMode? parseMode(int mode)
	{
		switch (mode)
		{
		case 1:
		case 2:
		case 3:
			return ScrollMode.Horizontal;
		case 4:
			return ScrollMode.Bottom;
		case 5:
			return ScrollMode.Top;
		case 6:
			return ScrollMode.Reverse;
		default:
			return null;
		}
	}

	private static SolidColorBrush getColorBrush(int color)
	{
		if (_colorDictionary.TryGetValue(color, out var value))
		{
			return value;
		}
		byte[] array = BitConverter.GetBytes(color).Reverse().ToArray();
		SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromArgb(byte.MaxValue, array[1], array[2], array[3]));
		solidColorBrush.Freeze();
		_colorDictionary[color] = solidColorBrush;
		return solidColorBrush;
	}
}
