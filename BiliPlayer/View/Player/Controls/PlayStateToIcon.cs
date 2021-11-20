using System;
using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000025 RID: 37
	internal class PlayStateToIcon : CommonConverter<MediaState, string>
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00003E38 File Offset: 0x00002038
		protected override object convert(MediaState value, object parameter)
		{
			return ((value == MediaState.Playing) ? '' : '').ToString();
		}
	}
}
