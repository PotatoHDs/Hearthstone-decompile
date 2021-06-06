using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.DataModels;

// Token: 0x0200067C RID: 1660
public static class RewardExtensions
{
	// Token: 0x06005CEB RID: 23787 RVA: 0x001E1770 File Offset: 0x001DF970
	public static RewardItemType ToRewardItemType(this RewardItem.RewardType source)
	{
		switch (source)
		{
		case RewardItem.RewardType.NONE:
			return RewardItemType.UNDEFINED;
		case RewardItem.RewardType.GOLD:
			return RewardItemType.GOLD;
		case RewardItem.RewardType.DUST:
			return RewardItemType.DUST;
		case RewardItem.RewardType.ARCANE_ORBS:
			return RewardItemType.ARCANE_ORBS;
		case RewardItem.RewardType.BOOSTER:
			return RewardItemType.BOOSTER;
		case RewardItem.RewardType.CARD:
			return RewardItemType.CARD;
		case RewardItem.RewardType.RANDOM_CARD:
			return RewardItemType.RANDOM_CARD;
		case RewardItem.RewardType.TAVERN_TICKET:
			return RewardItemType.ARENA_TICKET;
		case RewardItem.RewardType.CARD_BACK:
			return RewardItemType.CARD_BACK;
		case RewardItem.RewardType.HERO_SKIN:
			return RewardItemType.HERO_SKIN;
		case RewardItem.RewardType.CUSTOM_COIN:
			return RewardItemType.CUSTOM_COIN;
		case RewardItem.RewardType.REWARD_TRACK_XP_BOOST:
			return RewardItemType.REWARD_TRACK_XP_BOOST;
		case RewardItem.RewardType.CARD_SUBSET:
			return RewardItemType.CARD_SUBSET;
		}
		Log.All.PrintWarning(string.Format("RewardItem has unsupported item type [{0}]", source), Array.Empty<object>());
		return RewardItemType.UNDEFINED;
	}

	// Token: 0x06005CEC RID: 23788 RVA: 0x001E17FD File Offset: 0x001DF9FD
	public static TAG_PREMIUM GetPremium(this RewardItem.CardPremiumLevel source)
	{
		if (source == RewardItem.CardPremiumLevel.NORMAL)
		{
			return TAG_PREMIUM.NORMAL;
		}
		if (source == RewardItem.CardPremiumLevel.GOLDEN)
		{
			return TAG_PREMIUM.GOLDEN;
		}
		if (source == RewardItem.CardPremiumLevel.DIAMOND)
		{
			return TAG_PREMIUM.DIAMOND;
		}
		return TAG_PREMIUM.NORMAL;
	}

	// Token: 0x06005CED RID: 23789 RVA: 0x001E1811 File Offset: 0x001DFA11
	public static IEnumerable<RewardItemDataModel> Consolidate(this IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemType).SelectMany(new Func<IGrouping<RewardItemType, RewardItemDataModel>, IEnumerable<RewardItemDataModel>>(RewardFactory.ConsolidateGroup));
	}
}
