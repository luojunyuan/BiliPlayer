using System;
using BiliPlayer.Common.MVVM;

namespace BiliPlayer.View.Setting
{
	// Token: 0x0200001C RID: 28
	internal class SettingData : NotifyItem
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003916 File Offset: 0x00001B16
		public static SettingData Instance { get; } = new SettingData();

		// Token: 0x060000A6 RID: 166 RVA: 0x00003920 File Offset: 0x00001B20
		private SettingData()
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003976 File Offset: 0x00001B76
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000397E File Offset: 0x00001B7E
		public double FontScale
		{
			get
			{
				return this._fontScale;
			}
			set
			{
				this._fontScale = value;
				this.notifySettingChanged();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000398D File Offset: 0x00001B8D
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003995 File Offset: 0x00001B95
		public double Opacity
		{
			get
			{
				return this._opacity;
			}
			set
			{
				this._opacity = value;
				base.onPropertyChanged("Opacity");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000039A9 File Offset: 0x00001BA9
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000039B1 File Offset: 0x00001BB1
		public string FontFamily
		{
			get
			{
				return this._FontFamily;
			}
			set
			{
				this._FontFamily = value;
				this.notifySettingChanged();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000039C0 File Offset: 0x00001BC0
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000039C8 File Offset: 0x00001BC8
		public bool HasStroke
		{
			get
			{
				return this._hasStroke;
			}
			set
			{
				this._hasStroke = value;
				this.notifySettingChanged();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000039D7 File Offset: 0x00001BD7
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000039DF File Offset: 0x00001BDF
		public bool HasShadow
		{
			get
			{
				return this._hasShadow;
			}
			set
			{
				this._hasShadow = value;
				this.notifySettingChanged();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000039EE File Offset: 0x00001BEE
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000039F6 File Offset: 0x00001BF6
		public bool IsBold
		{
			get
			{
				return this._isBold;
			}
			set
			{
				this._isBold = value;
				this.notifySettingChanged();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003A05 File Offset: 0x00001C05
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003A0D File Offset: 0x00001C0D
		public object SettingId { get; private set; }

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A16 File Offset: 0x00001C16
		private void notifySettingChanged()
		{
			this.SettingId = new object();
			base.onPropertyChanged("SettingId");
		}

		// Token: 0x04000044 RID: 68
		private double _fontScale = 1.0;

		// Token: 0x04000045 RID: 69
		private double _opacity = 1.0;

		// Token: 0x04000046 RID: 70
		private string _FontFamily = FontFamilies.GetDefaultFont("微软雅黑");

		// Token: 0x04000047 RID: 71
		private bool _hasStroke = true;

		// Token: 0x04000048 RID: 72
		private bool _hasShadow = true;

		// Token: 0x04000049 RID: 73
		private bool _isBold = true;
	}
}
