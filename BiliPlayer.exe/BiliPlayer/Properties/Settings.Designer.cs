﻿using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace BiliPlayer.Properties
{
	// Token: 0x0200000D RID: 13
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A59 File Offset: 0x00000C59
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000014 RID: 20
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
