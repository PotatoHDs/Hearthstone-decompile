using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.DataModels;

public static class RewardExtensions
{
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
		default:
			Log.All.PrintWarning($"RewardItem has unsupported item type [{source}]");
			return RewardItemType.UNDEFINED;
		}
	}

	public static TAG_PREMIUM GetPremium(this RewardItem.CardPremiumLevel source)
	{
		return source switch
		{
			RewardItem.CardPremiumLevel.NORMAL => TAG_PREMIUM.NORMAL, 
			RewardItem.CardPremiumLevel.GOLDEN => TAG_PREMIUM.GOLDEN, 
			RewardItem.CardPremiumLevel.DIAMOND => TAG_PREMIUM.DIAMOND, 
			_ => TAG_PREMIUM.NORMAL, 
		};
	}

	public static IEnumerable<RewardItemDataModel> Consolidate(this IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
			group element by element.ItemType).SelectMany(RewardFactory.ConsolidateGroup);
	}
}
