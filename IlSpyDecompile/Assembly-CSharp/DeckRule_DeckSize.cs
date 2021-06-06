using UnityEngine;

public class DeckRule_DeckSize : DeckRule
{
	public DeckRule_DeckSize(int size)
	{
		m_ruleType = RuleType.DECK_SIZE;
		m_minValue = size;
		m_maxValue = size;
	}

	public DeckRule_DeckSize(DeckRulesetRuleDbfRecord record)
		: base(RuleType.DECK_SIZE, record)
	{
		if (m_ruleIsNot)
		{
			Debug.LogError("DECK_SIZE rules do not support \"is not\".");
		}
		if (m_appliesToSubset != null)
		{
			Debug.LogError("DECK_SIZE rules do not support \"applies to subset\".");
		}
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		int totalCardCount = deck.GetTotalCardCount();
		int num = 0;
		bool isMinimum = false;
		int num2 = m_minValue;
		int num3 = m_maxValue;
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(slot.CardID);
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
		bool result = GetResult(val);
		if (!result)
		{
			string error = ((totalCardCount >= num2) ? GameStrings.Format("GLUE_COLLECTION_DECK_RULE_TOO_MANY_CARDS", num) : GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", num));
			reason = new RuleInvalidReason(error, num, isMinimum);
		}
		return result;
	}

	public int GetMaximumDeckSize(CollectionDeck deck = null)
	{
		if (deck == null)
		{
			return GetDefaultDeckSize();
		}
		if (CardInDeckModifiesDeckSize(deck, out var modifiedDeckSize))
		{
			return modifiedDeckSize;
		}
		return m_maxValue;
	}

	public int GetMinimumDeckSize(CollectionDeck deck = null)
	{
		if (deck == null)
		{
			return GetDefaultDeckSize();
		}
		if (CardInDeckModifiesDeckSize(deck, out var modifiedDeckSize))
		{
			return modifiedDeckSize;
		}
		return m_minValue;
	}

	private bool CardInDeckModifiesDeckSize(CollectionDeck deck, out int modifiedDeckSize)
	{
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(slot.CardID);
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

	private int GetDefaultDeckSize()
	{
		return m_maxValue;
	}
}
