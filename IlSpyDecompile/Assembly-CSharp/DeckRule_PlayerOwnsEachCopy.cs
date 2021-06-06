using System;
using System.Collections.Generic;

public class DeckRule_PlayerOwnsEachCopy : DeckRule
{
	public DeckRule_PlayerOwnsEachCopy()
	{
		m_ruleType = RuleType.PLAYER_OWNS_EACH_COPY;
	}

	public DeckRule_PlayerOwnsEachCopy(DeckRulesetRuleDbfRecord record)
		: base(RuleType.PLAYER_OWNS_EACH_COPY, record)
	{
	}

	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!AppliesTo(cardId))
		{
			return true;
		}
		CollectibleCard card = CollectionManager.Get().GetCard(cardId, premium);
		if (card == null || deck.GetOwnedCardCountInDeck(cardId, premium) >= card.OwnedCount)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_NO_MORE_INSTANCES"));
			return GetResult(val: false);
		}
		return GetResult(val: true);
	}

	public override bool IsDeckValid(CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		if (deck.Locked)
		{
			return true;
		}
		CollectionManager collectionManager = CollectionManager.Get();
		List<CollectionDeckSlot> slots = deck.GetSlots();
		Map<KeyValuePair<string, TAG_PREMIUM>, int> map = new Map<KeyValuePair<string, TAG_PREMIUM>, int>();
		for (int i = 0; i < slots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = slots[i];
			if (collectionDeckSlot.Count <= 0 || !AppliesTo(collectionDeckSlot.CardID))
			{
				continue;
			}
			foreach (TAG_PREMIUM value4 in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				KeyValuePair<string, TAG_PREMIUM> key = new KeyValuePair<string, TAG_PREMIUM>(collectionDeckSlot.CardID, value4);
				int value = 0;
				map.TryGetValue(key, out value);
				map[key] = value + collectionDeckSlot.GetCount(value4);
			}
		}
		int num = 0;
		foreach (KeyValuePair<KeyValuePair<string, TAG_PREMIUM>, int> item in map)
		{
			string key2 = item.Key.Key;
			TAG_PREMIUM value2 = item.Key.Value;
			int value3 = item.Value;
			int num2 = collectionManager.GetCard(key2, value2)?.OwnedCount ?? 0;
			if (num2 < value3)
			{
				num += value3 - num2;
			}
		}
		bool result = GetResult(num == 0);
		if (!result)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", num), num);
		}
		return result;
	}
}
