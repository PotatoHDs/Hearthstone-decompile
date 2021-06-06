using System;

namespace Hearthstone.Telemetry
{
	// Token: 0x02001068 RID: 4200
	internal class IosContext : BaseContextData
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600B568 RID: 46440 RVA: 0x0037C2FB File Offset: 0x0037A4FB
		public override string ApplicationID
		{
			get
			{
				return "id625257520";
			}
		}

		// Token: 0x0400974E RID: 38734
		private const string s_applicationId = "id625257520";

		// Token: 0x0400974F RID: 38735
		protected const string s_testingApplicationId = "id432198765";
	}
}
