using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD0 RID: 4048
	public static class RewardBag
	{
		// Token: 0x0600B02B RID: 45099 RVA: 0x00366EC4 File Offset: 0x003650C4
		public static RewardBag.Reward ParseRewardValue(string value)
		{
			RewardBag.Reward result;
			EnumUtils.TryGetEnum<RewardBag.Reward>(value, out result);
			return result;
		}

		// Token: 0x020027F6 RID: 10230
		public enum Reward
		{
			// Token: 0x0400F7E2 RID: 63458
			[Description("unknown")]
			UNKNOWN = -1,
			// Token: 0x0400F7E3 RID: 63459
			[Description("none")]
			NONE,
			// Token: 0x0400F7E4 RID: 63460
			[Description("pack")]
			PACK,
			// Token: 0x0400F7E5 RID: 63461
			[Description("gold")]
			GOLD,
			// Token: 0x0400F7E6 RID: 63462
			[Description("dust")]
			DUST,
			// Token: 0x0400F7E7 RID: 63463
			[Description("com")]
			COM,
			// Token: 0x0400F7E8 RID: 63464
			[Description("rare")]
			RARE,
			// Token: 0x0400F7E9 RID: 63465
			[Description("epic")]
			EPIC,
			// Token: 0x0400F7EA RID: 63466
			[Description("leg")]
			LEG,
			// Token: 0x0400F7EB RID: 63467
			[Description("grare")]
			GRARE,
			// Token: 0x0400F7EC RID: 63468
			[Description("gcom")]
			GCOM,
			// Token: 0x0400F7ED RID: 63469
			[Description("gepic")]
			GEPIC,
			// Token: 0x0400F7EE RID: 63470
			[Description("gleg")]
			GLEG,
			// Token: 0x0400F7EF RID: 63471
			[Description("pack2_deprecated")]
			PACK2_DEPRECATED,
			// Token: 0x0400F7F0 RID: 63472
			[Description("seasonal_card_back")]
			SEASONAL_CARD_BACK,
			// Token: 0x0400F7F1 RID: 63473
			[Description("latest_pack")]
			LATEST_PACK,
			// Token: 0x0400F7F2 RID: 63474
			[Description("not_latest_pack")]
			NOT_LATEST_PACK,
			// Token: 0x0400F7F3 RID: 63475
			[Description("random_card")]
			RANDOM_CARD,
			// Token: 0x0400F7F4 RID: 63476
			[Description("golden_random_card")]
			GOLDEN_RANDOM_CARD,
			// Token: 0x0400F7F5 RID: 63477
			[Description("forge")]
			FORGE,
			// Token: 0x0400F7F6 RID: 63478
			[Description("specific_pack")]
			SPECIFIC_PACK,
			// Token: 0x0400F7F7 RID: 63479
			[Description("pack_offset_from_latest")]
			PACK_OFFSET_FROM_LATEST,
			// Token: 0x0400F7F8 RID: 63480
			[Description("reward_chest_contents")]
			REWARD_CHEST_CONTENTS,
			// Token: 0x0400F7F9 RID: 63481
			[Description("specific_card")]
			SPECIFIC_CARD,
			// Token: 0x0400F7FA RID: 63482
			[Description("specific_card_2x")]
			SPECIFIC_CARD_2X,
			// Token: 0x0400F7FB RID: 63483
			[Description("ranked_season_reward_pack")]
			RANKED_SEASON_REWARD_PACK
		}
	}
}
