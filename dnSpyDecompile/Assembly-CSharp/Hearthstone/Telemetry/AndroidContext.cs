using System;

namespace Hearthstone.Telemetry
{
	// Token: 0x02001063 RID: 4195
	internal class AndroidContext : BaseContextData
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600B554 RID: 46420 RVA: 0x0037C0E8 File Offset: 0x0037A2E8
		public override string ApplicationID
		{
			get
			{
				return "com.blizzard.wtcg.hearthstone";
			}
		}

		// Token: 0x0400973B RID: 38715
		private const string s_applicationId = "com.blizzard.wtcg.hearthstone";

		// Token: 0x0400973C RID: 38716
		protected const string s_testingApplicationId = "com.blizzard.telemetry.test";
	}
}
