namespace Assets
{
	public static class RewardItem
	{
		public enum RewardType
		{
			NONE = 0,
			GOLD = 1,
			DUST = 2,
			ARCANE_ORBS = 3,
			BOOSTER = 4,
			CARD = 6,
			RANDOM_CARD = 7,
			TAVERN_TICKET = 8,
			CARD_BACK = 9,
			HERO_SKIN = 10,
			CUSTOM_COIN = 11,
			REWARD_TRACK_XP_BOOST = 12,
			CARD_SUBSET = 13
		}

		public enum CardPremiumLevel
		{
			NORMAL,
			GOLDEN,
			DIAMOND
		}

		public enum BoosterSelector
		{
			NONE,
			LATEST,
			LATEST_OFFSET_BY_1,
			LATEST_OFFSET_BY_2,
			LATEST_OFFSET_BY_3
		}

		public static RewardType ParseRewardTypeValue(string value)
		{
			EnumUtils.TryGetEnum<RewardType>(value, out var outVal);
			return outVal;
		}

		public static CardPremiumLevel ParseCardPremiumLevelValue(string value)
		{
			EnumUtils.TryGetEnum<CardPremiumLevel>(value, out var outVal);
			return outVal;
		}

		public static BoosterSelector ParseBoosterSelectorValue(string value)
		{
			EnumUtils.TryGetEnum<BoosterSelector>(value, out var outVal);
			return outVal;
		}
	}
}
