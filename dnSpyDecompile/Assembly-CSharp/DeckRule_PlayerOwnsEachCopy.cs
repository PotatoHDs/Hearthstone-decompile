using System;
using System.Collections.Generic;

// Token: 0x020007C8 RID: 1992
public class DeckRule_PlayerOwnsEachCopy : DeckRule
{
	// Token: 0x06006DD5 RID: 28117 RVA: 0x0023653D File Offset: 0x0023473D
	public DeckRule_PlayerOwnsEachCopy()
	{
		this.m_ruleType = DeckRule.RuleType.PLAYER_OWNS_EACH_COPY;
	}

	// Token: 0x06006DD6 RID: 28118 RVA: 0x0023654C File Offset: 0x0023474C
	public DeckRule_PlayerOwnsEachCopy(DeckRulesetRuleDbfRecord record) : base(DeckRule.RuleType.PLAYER_OWNS_EACH_COPY, record)
	{
	}

	// Token: 0x06006DD7 RID: 28119 RVA: 0x00236558 File Offset: 0x00234758
	public override bool CanAddToDeck(EntityDef def, TAG_PREMIUM premium, CollectionDeck deck, out RuleInvalidReason reason)
	{
		reason = null;
		string cardId = def.GetCardId();
		if (!base.AppliesTo(cardId))
		{
			return true;
		}
		CollectibleCard card = CollectionManager.Get().GetCard(cardId, premium);
		if (card == null || deck.GetOwnedCardCountInDeck(cardId, premium, true) >= card.OwnedCount)
		{
			reason = new RuleInvalidReason(GameStrings.Get("GLUE_COLLECTION_LOCK_NO_MORE_INSTANCES"), 0, false);
			return base.GetResult(false);
		}
		return base.GetResult(true);
	}

	// Token: 0x06006DD8 RID: 28120 RVA: 0x002365C0 File Offset: 0x002347C0
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
			if (collectionDeckSlot.Count > 0 && base.AppliesTo(collectionDeckSlot.CardID))
			{
				foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
				{
					TAG_PREMIUM tag_PREMIUM = (TAG_PREMIUM)obj;
					KeyValuePair<string, TAG_PREMIUM> key = new KeyValuePair<string, TAG_PREMIUM>(collectionDeckSlot.CardID, tag_PREMIUM);
					int num = 0;
					map.TryGetValue(key, out num);
					map[key] = num + collectionDeckSlot.GetCount(tag_PREMIUM);
				}
			}
		}
		int num2 = 0;
		foreach (KeyValuePair<KeyValuePair<string, TAG_PREMIUM>, int> keyValuePair in map)
		{
			string key2 = keyValuePair.Key.Key;
			TAG_PREMIUM value = keyValuePair.Key.Value;
			int value2 = keyValuePair.Value;
			CollectibleCard card = collectionManager.GetCard(key2, value);
			int num3 = (card == null) ? 0 : card.OwnedCount;
			if (num3 < value2)
			{
				num2 += value2 - num3;
			}
		}
		bool result = base.GetResult(num2 == 0);
		if (!result)
		{
			reason = new RuleInvalidReason(GameStrings.Format("GLUE_COLLECTION_DECK_RULE_MISSING_CARDS", new object[]
			{
				num2
			}), num2, false);
		}
		return result;
	}
}
