using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls;

internal class PlayStateToTooltip : CommonConverter<MediaState, string>
{
	protected override object convert(MediaState value, object parameter)
	{
		if (value != MediaState.Playing)
		{
			return "播放";
		}
		return "暂停";
	}
}
