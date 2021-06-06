using System;

namespace Hearthstone.Telemetry
{
	// Token: 0x02001065 RID: 4197
	internal class AndroidOneStoreContext : AndroidContext
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x0600B558 RID: 46424 RVA: 0x0037C106 File Offset: 0x0037A306
		public override string ApplicationID
		{
			get
			{
				return "com.blizzard.wtcg.hearthstone.kr.onestore";
			}
		}

		// Token: 0x0400973E RID: 38718
		private const string s_applicationId = "com.blizzard.wtcg.hearthstone.kr.onestore";
	}
}
