using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C1 RID: 1985
public class DeckRule_IsClassCardOrNeutral : DeckRule
{
	// Token: 0x06006DB3 RID: 28083 RVA: 0x00235C3B File Offset: 0x00233E3B
	public DeckRule_IsClassCardOrNeutral(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.IS_CLASS_CARD_OR_NEUTRAL, record)
	{
		if (this.m_ruleIsNot)
		{
			Debug.LogError("IS_CLASS_CARD_OR_NEUTRAL rules do not support \"is not\".");
		}
	}

	// Token: 0x06006DB4 RID: 28084 RVA: 0x00235C58 File Offset: 0x00233E58
	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		if (!base.AppliesTo(def.GetCardId()))
		{
			return true;
		}
		if (deck == null)
		{
			return true;
		}
		TAG_CLASS @class = deck.GetClass();
		return base.GetResult(DeckRule_IsClassCardOrNeutral.CardIsClassCardOrNeutral(def, @class));
	}

	// Token: 0x06006DB5 RID: 28085 RVA: 0x00235C90 File Offset: 0x00233E90
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		TAG_CLASS @class = deck.GetClass();
		foreach (CollectionDeckSlot collectionDeckSlot in slots)
		{
			string cardID = collectionDeckSlot.CardID;
			if (base.AppliesTo(cardID))
			{
				bool val = DeckRule_IsClassCardOrNeutral.CardIsClassCardOrNeutral(DefLoader.Get().GetEntityDef(cardID), @class);
				if (!base.GetResult(val))
				{
					num += collectionDeckSlot.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_INVALID_CLASS_CARD", new object[]
			{
				num
			}), num, false);
		}
		return flag;
	}

	// Token: 0x06006DB6 RID: 28086 RVA: 0x00235D54 File Offset: 0x00233F54
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		if (!base.AppliesTo(def.GetCardId()))
		{
			return true;
		}
		TAG_CLASS @class = deck.GetClass();
		bool flag = DeckRule_IsClassCardOrNeutral.CardIsClassCardOrNeutral(def, @class);
		flag = base.GetResult(flag);
		if (!flag)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_CARD_INVALID_CLASS"), 0, false);
		}
		return flag;
	}

	// Token: 0x06006DB7 RID: 28087 RVA: 0x00235DA4 File Offset: 0x00233FA4
	private static bool CardIsClassCardOrNeutral(EntityBase def, TAG_CLASS deckClass)
	{
		IEnumerable<TAG_CLASS> classes = def.GetClasses(null);
		bool result = false;
		foreach (TAG_CLASS tag_CLASS in classes)
		{
			if (tag_CLASS == TAG_CLASS.NEUTRAL || tag_CLASS == deckClass)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
