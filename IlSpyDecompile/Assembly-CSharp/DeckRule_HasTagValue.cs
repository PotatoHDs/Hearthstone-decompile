using System.Collections.Generic;

public class DeckRule_HasTagValue : DeckRule
{
	public DeckRule_HasTagValue(GAME_TAG tag, int minValue, int maxValue)
	{
		m_ruleType = RuleType.HAS_TAG_VALUE;
		m_tag = (int)tag;
		m_tagMinValue = minValue;
		m_tagMaxValue = maxValue;
	}

	public DeckRule_HasTagValue(DeckRulesetRuleDbfRecord record)
		: base(RuleType.HAS_TAG_VALUE, record)
	{
	}

	public override bool Filter(EntityDef def, CollectionDeck deck)
	{
		if (!AppliesTo(def.GetCardId()))
		{
			return true;
		}
		int tag = def.GetTag(m_tag);
		return GetResult(CardHasTagValue(tag, m_tagMaxValue, m_tagMinValue));
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		bool flag = true;
		List<CollectionDeckSlot> slots = deck.GetSlots();
		int num = 0;
		foreach (CollectionDeckSlot item in slots)
		{
			string cardID = item.CardID;
			if (AppliesTo(cardID))
			{
				bool val = CardHasTagValue(DefLoader.Get().GetEntityDef(cardID).GetTag(m_tag), m_tagMaxValue, m_tagMinValue);
				if (!GetResult(val))
				{
					num += item.Count;
					flag = false;
				}
			}
		}
		if (!flag)
		{
			reason = new RuleInvalidReason(m_errorString, num);
		}
		return flag;
	}

	private static bool CardHasTagValue(int tagValue, int max, int min)
	{
		if (tagValue >= max)
		{
			return tagValue <= min;
		}
		return false;
	}
}
