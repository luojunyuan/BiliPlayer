using System;
using System.Windows.Media;
using BiliPlayer.Common.Data;

namespace BiliPlayer.View.Subtitles
{
	// Token: 0x02000014 RID: 20
	internal class TitleInfo
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002FB0 File Offset: 0x000011B0
		public string Text { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002FB9 File Offset: 0x000011B9
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002FC1 File Offset: 0x000011C1
		public TimeSpan Start { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002FCA File Offset: 0x000011CA
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002FD2 File Offset: 0x000011D2
		public TimeSpan End { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002FDB File Offset: 0x000011DB
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002FE3 File Offset: 0x000011E3
		public Brush Stroke { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002FEC File Offset: 0x000011EC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002FF4 File Offset: 0x000011F4
		public Brush Fill { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FFD File Offset: 0x000011FD
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003005 File Offset: 0x00001205
		public bool IsDarkColor { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003016 File Offset: 0x00001216
		public int ChannelNo { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000301F File Offset: 0x0000121F
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003027 File Offset: 0x00001227
		public ScrollMode ScrollMode { get; set; }
	}
}
