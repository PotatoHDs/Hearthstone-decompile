using Assets;
using UnityEngine;

namespace FixedReward
{
	public static class FixedRewardExtensions
	{
		public static Achieve.RewardTiming GetRewardTiming(this FixedRewardMapDbfRecord record)
		{
			if (EnumUtils.TryGetEnum<Achieve.RewardTiming>(record.RewardTiming, out var outVal))
			{
				return outVal;
			}
			Debug.LogWarning($"QueueRewardVisual rewardMapID={record.ID} no enum value for reward visual timing {record.RewardTiming}, check fixed rewards map");
			return Achieve.RewardTiming.IMMEDIATE;
		}

		public static Reward GetFixedReward(this FixedRewardMapDbfRecord record)
		{
			Reward reward = new Reward();
			FixedRewardDbfRecord record2 = GameDbf.FixedReward.GetRecord(record.RewardId);
			if (record2 == null)
			{
				return reward;
			}
			reward.Type = record2.Type;
			switch (reward.Type)
			{
			case Assets.FixedReward.Type.VIRTUAL_CARD:
			{
				NetCache.CardDefinition cardDefinition2 = record2.GetCardDefinition();
				if (cardDefinition2 != null)
				{
					reward.FixedCardRewardData = new CardRewardData(cardDefinition2.Name, cardDefinition2.Premium, record.RewardCount)
					{
						FixedReward = record
					};
					if (GameUtils.IsClassicCard(record2.CardId))
					{
						reward.FixedCraftableCardRewardData = cardDefinition2;
					}
				}
				break;
			}
			case Assets.FixedReward.Type.REAL_CARD:
			{
				NetCache.CardDefinition cardDefinition3 = record2.GetCardDefinition();
				if (cardDefinition3 != null)
				{
					reward.FixedCardRewardData = new CardRewardData(cardDefinition3.Name, cardDefinition3.Premium, record.RewardCount)
					{
						FixedReward = record
					};
				}
				break;
			}
			case Assets.FixedReward.Type.CARDBACK:
			{
				int cardBackId = record2.CardBackId;
				reward.FixedCardBackRewardData = new CardBackRewardData(cardBackId);
				break;
			}
			case Assets.FixedReward.Type.CRAFTABLE_CARD:
			{
				NetCache.CardDefinition cardDefinition = record2.GetCardDefinition();
				if (cardDefinition != null)
				{
					reward.FixedCraftableCardRewardData = cardDefinition;
				}
				break;
			}
			case Assets.FixedReward.Type.META_ACTION_FLAGS:
			{
				int metaActionId = record2.MetaActionId;
				ulong metaActionFlags = record2.MetaActionFlags;
				reward.MetaActionData = new MetaAction(metaActionId);
				reward.MetaActionData.UpdateFlags(metaActionFlags, 0uL);
				break;
			}
			}
			return reward;
		}

		private static NetCache.CardDefinition GetCardDefinition(this FixedRewardDbfRecord record)
		{
			string text = GameUtils.TranslateDbIdToCardId(record.CardId);
			if (text == null)
			{
				return null;
			}
			if (!EnumUtils.TryCast<TAG_PREMIUM>(record.CardPremium, out var outVal))
			{
				return null;
			}
			return new NetCache.CardDefinition
			{
				Name = text,
				Premium = outVal
			};
		}
	}
}
