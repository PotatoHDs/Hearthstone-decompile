using System;
using System.Collections.Generic;

// Token: 0x020007BD RID: 1981
public class DeckRule_CountCopiesOfEachCard : DeckRule
{
	// Token: 0x06006D9C RID: 28060 RVA: 0x002355C4 File Offset: 0x002337C4
	public DeckRule_CountCopiesOfEachCard(int subset, int min, int max)
	{
		this.m_ruleType = DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD;
		this.m_appliesToSubsetId = subset;
		this.m_minValue = min;
		this.m_maxValue = max;
		if (this.m_appliesToSubsetId != 0)
		{
			this.m_appliesToSubset = GameDbf.GetIndex().GetSubsetById(this.m_appliesToSubsetId);
		}
	}

	// Token: 0x06006D9D RID: 28061 RVA: 0x00235611 File Offset: 0x00233811
	public DeckRule_CountCopiesOfEachCard(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.COUNT_COPIES_OF_EACH_CARD, record)
	{
	}

	// Token: 0x06006D9E RID: 28062 RVA: 0x0023561C File Offset: 0x0023381C
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		if (!base.AppliesTo(def.GetCardId()))
		{
			return true;
		}
		int cardIdCount = deck.GetCardIdCount(def.GetCardId(), true);
		int num = GameUtils.TranslateCardIdToDbId(def.GetCardId(), false);
		bool flag = cardIdCount + deck.GetCardCountMatchingTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID, num) + deck.GetCardIdCount(GameUtils.TranslateDbIdToCardId(GameUtils.GetCardTagValue(num, GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID), false), true) >= this.m_maxValue;
		if (flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_LOCK_MAX_DECK_COPIES", new object[]
			{
				this.m_maxValue
			}), this.m_maxValue, false);
		}
		return base.GetResult(!flag);
	}

	// Token: 0x06006D9F RID: 28063 RVA: 0x002356C3 File Offset: 0x002338C3
	public bool GetMaxCopies(EntityDef def, out int maxCopies)
	{
		maxCopies = int.MaxValue;
		if (!base.AppliesTo(def.GetCardId()))
		{
			return false;
		}
		maxCopies = this.m_maxValue;
		return true;
	}

	// Token: 0x06006DA0 RID: 28064 RVA: 0x002356E8 File Offset: 0x002338E8
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int countParam = 0;
		bool isMinimum = false;
		foreach (CollectionDeckSlot collectionDeckSlot in slots)
		{
			string cardID = collectionDeckSlot.CardID;
			if (base.AppliesTo(cardID))
			{
				int cardIdCount = deck.GetCardIdCount(cardID, true);
				if (cardIdCount < this.m_minValue)
				{
					flag = false;
					countParam = this.m_minValue - cardIdCount;
					isMinimum = true;
					break;
				}
				if (cardIdCount > this.m_maxValue)
				{
					flag = false;
					countParam = this.m_maxValue;
					break;
				}
			}
		}
		flag = base.GetResult(flag);
		if (!flag)
		{
			reason = new RuleInvalidReason(this.m_errorString, countParam, isMinimum);
		}
		return flag;
	}
}
