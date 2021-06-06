using System;
using Assets;
using UnityEngine;

namespace FixedReward
{
	// Token: 0x02000B4C RID: 2892
	public static class FixedRewardExtensions
	{
		// Token: 0x06009A59 RID: 39513 RVA: 0x0031AE78 File Offset: 0x00319078
		public static Achieve.RewardTiming GetRewardTiming(this FixedRewardMapDbfRecord record)
		{
			Achieve.RewardTiming result;
			if (EnumUtils.TryGetEnum<Achieve.RewardTiming>(record.RewardTiming, out result))
			{
				return result;
			}
			Debug.LogWarning(string.Format("QueueRewardVisual rewardMapID={0} no enum value for reward visual timing {1}, check fixed rewards map", record.ID, record.RewardTiming));
			return Achieve.RewardTiming.IMMEDIATE;
		}

		// Token: 0x06009A5A RID: 39514 RVA: 0x0031AEB8 File Offset: 0x003190B8
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
			case FixedReward.Type.VIRTUAL_CARD:
			{
				NetCache.CardDefinition cardDefinition = record2.GetCardDefinition();
				if (cardDefinition != null)
				{
					reward.FixedCardRewardData = new CardRewardData(cardDefinition.Name, cardDefinition.Premium, record.RewardCount)
					{
						FixedReward = record
					};
					if (GameUtils.IsClassicCard(record2.CardId))
					{
						reward.FixedCraftableCardRewardData = cardDefinition;
					}
				}
				break;
			}
			case FixedReward.Type.REAL_CARD:
			{
				NetCache.CardDefinition cardDefinition2 = record2.GetCardDefinition();
				if (cardDefinition2 != null)
				{
					reward.FixedCardRewardData = new CardRewardData(cardDefinition2.Name, cardDefinition2.Premium, record.RewardCount)
					{
						FixedReward = record
					};
				}
				break;
			}
			case FixedReward.Type.CARDBACK:
			{
				int cardBackId = record2.CardBackId;
				reward.FixedCardBackRewardData = new CardBackRewardData(cardBackId);
				break;
			}
			case FixedReward.Type.CRAFTABLE_CARD:
			{
				NetCache.CardDefinition cardDefinition3 = record2.GetCardDefinition();
				if (cardDefinition3 != null)
				{
					reward.FixedCraftableCardRewardData = cardDefinition3;
				}
				break;
			}
			case FixedReward.Type.META_ACTION_FLAGS:
			{
				int metaActionId = record2.MetaActionId;
				ulong metaActionFlags = record2.MetaActionFlags;
				reward.MetaActionData = new MetaAction(metaActionId);
				reward.MetaActionData.UpdateFlags(metaActionFlags, 0UL);
				break;
			}
			}
			return reward;
		}

		// Token: 0x06009A5B RID: 39515 RVA: 0x0031AFF4 File Offset: 0x003191F4
		private static NetCache.CardDefinition GetCardDefinition(this FixedRewardDbfRecord record)
		{
			string text = GameUtils.TranslateDbIdToCardId(record.CardId, false);
			if (text == null)
			{
				return null;
			}
			TAG_PREMIUM premium;
			if (!EnumUtils.TryCast<TAG_PREMIUM>(record.CardPremium, out premium))
			{
				return null;
			}
			return new NetCache.CardDefinition
			{
				Name = text,
				Premium = premium
			};
		}
	}
}
