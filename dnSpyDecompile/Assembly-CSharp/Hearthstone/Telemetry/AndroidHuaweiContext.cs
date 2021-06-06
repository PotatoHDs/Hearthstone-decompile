using System;

namespace Hearthstone.Telemetry
{
	// Token: 0x02001064 RID: 4196
	internal class AndroidHuaweiContext : AndroidContext
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600B556 RID: 46422 RVA: 0x0037C0F7 File Offset: 0x0037A2F7
		public override string ApplicationID
		{
			get
			{
				return "com.blizzard.wtcg.hearthstone.cn.huawei";
			}
		}

		// Token: 0x0400973D RID: 38717
		private const string s_applicationId = "com.blizzard.wtcg.hearthstone.cn.huawei";
	}
}
