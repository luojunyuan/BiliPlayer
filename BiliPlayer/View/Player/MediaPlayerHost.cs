using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace BiliPlayer.View.Player;

internal class MediaPlayerHost : ContentControl, IViewPart
{
	[Import]
	private MediaElement _mediaElement;

	public void Init(Panel container)
	{
		base.Content = _mediaElement;
	}
}
