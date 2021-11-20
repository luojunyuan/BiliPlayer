using System;

namespace BiliPlayer.Util
{
	// Token: 0x0200002D RID: 45
	public sealed class BooleanToVisibility : VisibilityConverter<bool>
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004360 File Offset: 0x00002560
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004368 File Offset: 0x00002568
		public bool VisibleValue { get; set; }

		// Token: 0x060000FB RID: 251 RVA: 0x00004371 File Offset: 0x00002571
		protected override bool IsValaueVisible(bool value)
		{
			return this.VisibleValue == value;
		}
	}
}
