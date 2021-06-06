using System.ComponentModel;

namespace Assets
{
	public static class RewardBag
	{
		public enum Reward
		{
			[Description("unknown")]
			UNKNOWN = -1,
			[Description("none")]
			NONE,
			[Description("pack")]
			PACK,
			[Description("gold")]
			GOLD,
			[Description("dust")]
			DUST,
			[Description("com")]
			COM,
			[Description("rare")]
			RARE,
			[Description("epic")]
			EPIC,
			[Description("leg")]
			LEG,
			[Description("grare")]
			GRARE,
			[Description("gcom")]
			GCOM,
			[Description("gepic")]
			GEPIC,
			[Description("gleg")]
			GLEG,
			[Description("pack2_deprecated")]
			PACK2_DEPRECATED,
			[Description("seasonal_card_back")]
			SEASONAL_CARD_BACK,
			[Description("latest_pack")]
			LATEST_PACK,
			[Description("not_latest_pack")]
			NOT_LATEST_PACK,
			[Description("random_card")]
			RANDOM_CARD,
			[Description("golden_random_card")]
			GOLDEN_RANDOM_CARD,
			[Description("forge")]
			FORGE,
			[Description("specific_pack")]
			SPECIFIC_PACK,
			[Description("pack_offset_from_latest")]
			PACK_OFFSET_FROM_LATEST,
			[Description("reward_chest_contents")]
			REWARD_CHEST_CONTENTS,
			[Description("specific_card")]
			SPECIFIC_CARD,
			[Description("specific_card_2x")]
			SPECIFIC_CARD_2X,
			[Description("ranked_season_reward_pack")]
			RANKED_SEASON_REWARD_PACK
		}

		public static Reward ParseRewardValue(string value)
		{
			EnumUtils.TryGetEnum<Reward>(value, out var outVal);
			return outVal;
		}
	}
}
