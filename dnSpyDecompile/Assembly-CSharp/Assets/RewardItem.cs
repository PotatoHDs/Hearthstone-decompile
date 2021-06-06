using System;

namespace Assets
{
	// Token: 0x02000FD1 RID: 4049
	public static class RewardItem
	{
		// Token: 0x0600B02C RID: 45100 RVA: 0x00366EDC File Offset: 0x003650DC
		public static RewardItem.RewardType ParseRewardTypeValue(string value)
		{
			RewardItem.RewardType result;
			EnumUtils.TryGetEnum<RewardItem.RewardType>(value, out result);
			return result;
		}

		// Token: 0x0600B02D RID: 45101 RVA: 0x00366EF4 File Offset: 0x003650F4
		public static RewardItem.CardPremiumLevel ParseCardPremiumLevelValue(string value)
		{
			RewardItem.CardPremiumLevel result;
			EnumUtils.TryGetEnum<RewardItem.CardPremiumLevel>(value, out result);
			return result;
		}

		// Token: 0x0600B02E RID: 45102 RVA: 0x00366F0C File Offset: 0x0036510C
		public static RewardItem.BoosterSelector ParseBoosterSelectorValue(string value)
		{
			RewardItem.BoosterSelector result;
			EnumUtils.TryGetEnum<RewardItem.BoosterSelector>(value, out result);
			return result;
		}

		// Token: 0x020027F7 RID: 10231
		public enum RewardType
		{
			// Token: 0x0400F7FD RID: 63485
			NONE,
			// Token: 0x0400F7FE RID: 63486
			GOLD,
			// Token: 0x0400F7FF RID: 63487
			DUST,
			// Token: 0x0400F800 RID: 63488
			ARCANE_ORBS,
			// Token: 0x0400F801 RID: 63489
			BOOSTER,
			// Token: 0x0400F802 RID: 63490
			CARD = 6,
			// Token: 0x0400F803 RID: 63491
			RANDOM_CARD,
			// Token: 0x0400F804 RID: 63492
			TAVERN_TICKET,
			// Token: 0x0400F805 RID: 63493
			CARD_BACK,
			// Token: 0x0400F806 RID: 63494
			HERO_SKIN,
			// Token: 0x0400F807 RID: 63495
			CUSTOM_COIN,
			// Token: 0x0400F808 RID: 63496
			REWARD_TRACK_XP_BOOST,
			// Token: 0x0400F809 RID: 63497
			CARD_SUBSET
		}

		// Token: 0x020027F8 RID: 10232
		public enum CardPremiumLevel
		{
			// Token: 0x0400F80B RID: 63499
			NORMAL,
			// Token: 0x0400F80C RID: 63500
			GOLDEN,
			// Token: 0x0400F80D RID: 63501
			DIAMOND
		}

		// Token: 0x020027F9 RID: 10233
		public enum BoosterSelector
		{
			// Token: 0x0400F80F RID: 63503
			NONE,
			// Token: 0x0400F810 RID: 63504
			LATEST,
			// Token: 0x0400F811 RID: 63505
			LATEST_OFFSET_BY_1,
			// Token: 0x0400F812 RID: 63506
			LATEST_OFFSET_BY_2,
			// Token: 0x0400F813 RID: 63507
			LATEST_OFFSET_BY_3
		}
	}
}
