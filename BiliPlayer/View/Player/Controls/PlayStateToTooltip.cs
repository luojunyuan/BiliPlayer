using System;
using BiliPlayer.Common.Media;
using BiliPlayer.Util;

namespace BiliPlayer.View.Player.Controls
{
	// Token: 0x02000026 RID: 38
	internal class PlayStateToTooltip : CommonConverter<MediaState, string>
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003E65 File Offset: 0x00002065
		protected override object convert(MediaState value, object parameter)
		{
			if (value != MediaState.Playing)
			{
				return "播放";
			}
			return "暂停";
		}
	}
}
