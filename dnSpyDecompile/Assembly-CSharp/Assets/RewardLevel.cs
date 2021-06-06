using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD2 RID: 4050
	public static class RewardLevel
	{
		// Token: 0x0600B02F RID: 45103 RVA: 0x00366F24 File Offset: 0x00365124
		public static RewardLevel.SubscriptionRequirement ParseSubscriptionRequirementValue(string value)
		{
			RewardLevel.SubscriptionRequirement result;
			EnumUtils.TryGetEnum<RewardLevel.SubscriptionRequirement>(value, out result);
			return result;
		}

		// Token: 0x020027FA RID: 10234
		public enum SubscriptionRequirement
		{
			// Token: 0x0400F815 RID: 63509
			[Description("free")]
			FREE,
			// Token: 0x0400F816 RID: 63510
			[Description("paid")]
			PAID
		}
	}
}
