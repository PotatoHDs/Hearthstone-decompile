using System;
using UnityEngine;

// Token: 0x020007BE RID: 1982
public class DeckRule_DeckSize : DeckRule
{
	// Token: 0x06006DA1 RID: 28065 RVA: 0x002357A8 File Offset: 0x002339A8
	public DeckRule_DeckSize(int size)
	{
		this.m_ruleType = DeckRule.RuleType.DECK_SIZE;
		this.m_minValue = size;
		this.m_maxValue = size;
	}

	// Token: 0x06006DA2 RID: 28066 RVA: 0x002357C5 File Offset: 0x002339C5
	public DeckRule_DeckSize(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.DECK_SIZE, record)
	{
		if (this.m_ruleIsNot)
		{
			Debug.LogError("DECK_SIZE rules do not support \"is not\".");
		}
		if (this.m_appliesToSubset != null)
		{
			Debug.LogError("DECK_SIZE rules do not support \"applies to subset\".");
		}
	}

	// Token: 0x06006DA3 RID: 28067 RVA: 0x002357F4 File Offset: 0x002339F4
	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int totalCardCount = deck.GetTotalCardCount();
		int num = 0;
		bool isMinimum = false;
		int num2 = this.m_minValue;
		int num3 = this.m_maxValue;
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			if (entityDef.HasTag(GAME_TAG.DECK_RULE_MOD_DECK_SIZE))
			{
				num3 = entityDef.GetTag(GAME_TAG.DECK_RULE_MOD_DECK_SIZE);
				num2 = num3;
				break;
			}
		}
		bool val = true;
		if (totalCardCount < num2)
		{
			val = false;
			num = num2 - totalCardCount;
			isMinimum = true;
		}
		else if (totalCardCount > num3)
		{
			val = false;
			num = totalCardCount - num3;
		}
		bool result = base.GetResult(val);
		if (!result)
		{
			string error;
			if (totalCardCount < num2)
			{
				error = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", new object[]
				{
					num
				});
			}
			else
			{
				error = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_TOO_MANY_CARDS", new object[]
				{
					num
				});
			}
			reason = new RuleInvalidReason(error, num, isMinimum);
		}
		return result;
	}

	// Token: 0x06006DA4 RID: 28068 RVA: 0x0023590C File Offset: 0x00233B0C
	public int GetMaximumDeckSize(CollectionDeck deck = null)
	{
		if (deck == null)
		{
			return this.GetDefaultDeckSize();
		}
		int result;
		if (this.CardInDeckModifiesDeckSize(deck, out result))
		{
			return result;
		}
		return this.m_maxValue;
	}

	// Token: 0x06006DA5 RID: 28069 RVA: 0x00235938 File Offset: 0x00233B38
	public int GetMinimumDeckSize(CollectionDeck deck = null)
	{
		if (deck == null)
		{
			return this.GetDefaultDeckSize();
		}
		int result;
		if (this.CardInDeckModifiesDeckSize(deck, out result))
		{
			return result;
		}
		return this.m_minValue;
	}

	// Token: 0x06006DA6 RID: 28070 RVA: 0x00235964 File Offset: 0x00233B64
	private bool CardInDeckModifiesDeckSize(CollectionDeck deck, out int modifiedDeckSize)
	{
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
			if (entityDef.HasTag(GAME_TAG.DECK_RULE_MOD_DECK_SIZE))
			{
				modifiedDeckSize = entityDef.GetTag(GAME_TAG.DECK_RULE_MOD_DECK_SIZE);
				return true;
			}
			if (entityDef.HasTag(GAME_TAG.IGNORE_DECK_RULESET))
			{
				modifiedDeckSize = int.MaxValue;
				return true;
			}
		}
		modifiedDeckSize = 0;
		return false;
	}

	// Token: 0x06006DA7 RID: 28071 RVA: 0x00235A00 File Offset: 0x00233C00
	private int GetDefaultDeckSize()
	{
		return this.m_maxValue;
	}
}
