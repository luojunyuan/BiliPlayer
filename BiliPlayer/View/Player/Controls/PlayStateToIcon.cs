using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls;

internal class PlayStateToIcon : CommonConverter<MediaState, string>
{
	protected override object convert(MediaState value, object parameter)
	{
		return ((value == MediaState.Playing) ? '\uf04c' : '\uf04b').ToString();
	}
}
