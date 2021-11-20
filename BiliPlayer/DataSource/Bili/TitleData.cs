using System;
using System.Windows.Media;
using BiliPlayer.Common.Data;

namespace BiliPlayer.DataSource.Bili
{
	// Token: 0x02000033 RID: 51
	internal class TitleData
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000047A4 File Offset: 0x000029A4
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000047AC File Offset: 0x000029AC
		public string Content { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000047B5 File Offset: 0x000029B5
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000047BD File Offset: 0x000029BD
		public Brush Foreground { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000047C6 File Offset: 0x000029C6
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000047CE File Offset: 0x000029CE
		public ScrollMode ScrollMode { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000047D7 File Offset: 0x000029D7
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000047DF File Offset: 0x000029DF
		public TimeSpan Start { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000047E8 File Offset: 0x000029E8
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000047F0 File Offset: 0x000029F0
		public TimeSpan End { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000047F9 File Offset: 0x000029F9
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004801 File Offset: 0x00002A01
		public bool IsDarkColor { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000480A File Offset: 0x00002A0A
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004812 File Offset: 0x00002A12
		public int ChannelNo { get; set; }
	}
}
