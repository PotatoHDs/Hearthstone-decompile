using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

// Token: 0x020008BF RID: 2239
public class GameDbfIndex
{
	// Token: 0x06007B38 RID: 31544 RVA: 0x0027F679 File Offset: 0x0027D879
	public GameDbfIndex()
	{
		this.Initialize();
	}

	// Token: 0x06007B39 RID: 31545 RVA: 0x0027F688 File Offset: 0x0027D888
	public void Initialize()
	{
		this.m_cardsByCardId = new Map<string, CardDbfRecord>();
		this.m_cardTagsByCardDbId = new Map<int, List<CardTagDbfRecord>>();
		this.m_cardDiscoverStringsByCardId = new Map<string, CardDiscoverStringDbfRecord>();
		this.m_allCardIds = new List<string>();
		this.m_allCardDbIds = new List<int>();
		this.m_collectibleCardIds = new List<string>();
		this.m_collectibleCardDbIds = new List<int>();
		this.m_collectibleCardCount = 0;
		this.m_featuredCardEventCardIds = new List<string>();
		this.m_fixedRewardsByAction = new Map<int, List<FixedRewardMapDbfRecord>>();
		this.m_fixedActionRecordsByType = new Map<FixedRewardAction.Type, List<FixedRewardActionDbfRecord>>();
		this.m_subsetsReferencedByRuleId = new Map<int, List<int>>();
		this.m_subsetCards = new Map<int, HashSet<string>>();
		this.m_rulesByDeckRulesetId = new Map<int, HashSet<int>>();
		this.m_cardChangesByCardId = new Map<int, List<CardChangeDbfRecord>>();
		this.m_cardsWithPlayerDeckOverrides = new Map<int, int>();
		this.m_spellOverridesByCardSetId = new Map<int, Map<SpellType, string>>();
	}

	// Token: 0x06007B3A RID: 31546 RVA: 0x0027F74C File Offset: 0x0027D94C
	public void PostProcessDbfLoad_CardTag(Dbf<CardTagDbfRecord> dbf)
	{
		this.m_cardTagsByCardDbId.Clear();
		foreach (CardTagDbfRecord cardTagRecord in dbf.GetRecords())
		{
			this.OnCardTagAdded(cardTagRecord);
		}
	}

	// Token: 0x06007B3B RID: 31547 RVA: 0x0027F7AC File Offset: 0x0027D9AC
	public void OnCardTagAdded(CardTagDbfRecord cardTagRecord)
	{
		int cardId = cardTagRecord.CardId;
		List<CardTagDbfRecord> list = null;
		if (!this.m_cardTagsByCardDbId.TryGetValue(cardId, out list))
		{
			list = new List<CardTagDbfRecord>();
			this.m_cardTagsByCardDbId[cardId] = list;
		}
		list.Add(cardTagRecord);
	}

	// Token: 0x06007B3C RID: 31548 RVA: 0x0027F7EC File Offset: 0x0027D9EC
	public void OnCardTagRemoved(List<CardTagDbfRecord> removedRecords)
	{
		foreach (CardTagDbfRecord item in removedRecords)
		{
			using (Map<int, List<CardTagDbfRecord>>.ValueCollection.Enumerator enumerator2 = this.m_cardTagsByCardDbId.Values.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.Remove(item))
					{
						break;
					}
				}
			}
		}
	}

	// Token: 0x06007B3D RID: 31549 RVA: 0x0027F87C File Offset: 0x0027DA7C
	public void PostProcessDbfLoad_CardDiscoverString(Dbf<CardDiscoverStringDbfRecord> dbf)
	{
		this.m_cardDiscoverStringsByCardId.Clear();
		foreach (CardDiscoverStringDbfRecord cardDiscoverStringRecord in dbf.GetRecords())
		{
			this.OnCardDiscoverStringAdded(cardDiscoverStringRecord);
		}
	}

	// Token: 0x06007B3E RID: 31550 RVA: 0x0027F8DC File Offset: 0x0027DADC
	public void OnCardDiscoverStringAdded(CardDiscoverStringDbfRecord cardDiscoverStringRecord)
	{
		this.m_cardDiscoverStringsByCardId[cardDiscoverStringRecord.NoteMiniGuid] = cardDiscoverStringRecord;
	}

	// Token: 0x06007B3F RID: 31551 RVA: 0x0027F8F0 File Offset: 0x0027DAF0
	public void OnCardDiscoverStringRemoved(List<CardDiscoverStringDbfRecord> removedRecords)
	{
		foreach (CardDiscoverStringDbfRecord cardDiscoverStringDbfRecord in removedRecords)
		{
			this.m_cardDiscoverStringsByCardId.Remove(cardDiscoverStringDbfRecord.NoteMiniGuid);
		}
	}

	// Token: 0x06007B40 RID: 31552 RVA: 0x0027F94C File Offset: 0x0027DB4C
	public int GetCardTagValue(int cardDbId, GAME_TAG tagId)
	{
		List<CardTagDbfRecord> list = null;
		if (!this.m_cardTagsByCardDbId.TryGetValue(cardDbId, out list))
		{
			return 0;
		}
		CardTagDbfRecord cardTagDbfRecord = list.Find((CardTagDbfRecord item) => item.TagId == (int)tagId);
		if (cardTagDbfRecord == null)
		{
			return 0;
		}
		return cardTagDbfRecord.TagValue;
	}

	// Token: 0x06007B41 RID: 31553 RVA: 0x0027F998 File Offset: 0x0027DB98
	public IEnumerable<CardTagDbfRecord> GetCardTagRecords(int cardDbId)
	{
		List<CardTagDbfRecord> result = null;
		if (!this.m_cardTagsByCardDbId.TryGetValue(cardDbId, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007B42 RID: 31554 RVA: 0x0027F9BC File Offset: 0x0027DBBC
	public void PostProcessDbfLoad_Card(Dbf<CardDbfRecord> dbf)
	{
		this.m_cardsByCardId.Clear();
		this.m_allCardDbIds.Clear();
		this.m_allCardIds.Clear();
		this.m_collectibleCardCount = 0;
		this.m_collectibleCardIds.Clear();
		this.m_collectibleCardDbIds.Clear();
		this.m_featuredCardEventCardIds.Clear();
		foreach (CardDbfRecord cardRecord in dbf.GetRecords())
		{
			this.OnCardAdded(cardRecord);
		}
	}

	// Token: 0x06007B43 RID: 31555 RVA: 0x0027FA58 File Offset: 0x0027DC58
	public void OnCardAdded(CardDbfRecord cardRecord)
	{
		int id = cardRecord.ID;
		bool flag = this.GetCardTagValue(id, GAME_TAG.COLLECTIBLE) == 1;
		string noteMiniGuid = cardRecord.NoteMiniGuid;
		this.m_cardsByCardId[noteMiniGuid] = cardRecord;
		this.m_allCardDbIds.Add(id);
		this.m_allCardIds.Add(noteMiniGuid);
		if (flag)
		{
			this.m_collectibleCardCount++;
			this.m_collectibleCardIds.Add(noteMiniGuid);
			this.m_collectibleCardDbIds.Add(id);
		}
		if (!string.IsNullOrEmpty(cardRecord.FeaturedCardsEvent))
		{
			this.m_featuredCardEventCardIds.Add(noteMiniGuid);
		}
	}

	// Token: 0x06007B44 RID: 31556 RVA: 0x0027FAE8 File Offset: 0x0027DCE8
	public void OnCardRemoved(List<CardDbfRecord> removedRecords)
	{
		HashSet<int> removedCardDbIds = new HashSet<int>();
		HashSet<string> removedCardIds = new HashSet<string>();
		foreach (CardDbfRecord cardDbfRecord in removedRecords)
		{
			removedCardDbIds.Add(cardDbfRecord.ID);
			if (cardDbfRecord.NoteMiniGuid != null)
			{
				removedCardIds.Add(cardDbfRecord.NoteMiniGuid);
				this.m_cardsByCardId.Remove(cardDbfRecord.NoteMiniGuid);
			}
		}
		if (removedCardDbIds.Count > 0)
		{
			this.m_allCardDbIds.RemoveAll((int cardDbId) => removedCardDbIds.Contains(cardDbId));
			this.m_collectibleCardDbIds.RemoveAll((int cardDbId) => this.m_collectibleCardDbIds.Contains(cardDbId));
		}
		if (removedCardIds.Count > 0)
		{
			this.m_allCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
			this.m_collectibleCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
			this.m_featuredCardEventCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
		}
	}

	// Token: 0x06007B45 RID: 31557 RVA: 0x0027FC24 File Offset: 0x0027DE24
	public void PostProcessDbfLoad_FixedRewardMap(Dbf<FixedRewardMapDbfRecord> dbf)
	{
		this.m_fixedRewardsByAction.Clear();
		foreach (FixedRewardMapDbfRecord record in dbf.GetRecords())
		{
			this.OnFixedRewardMapAdded(record);
		}
	}

	// Token: 0x06007B46 RID: 31558 RVA: 0x0027FC84 File Offset: 0x0027DE84
	public void OnFixedRewardMapAdded(FixedRewardMapDbfRecord record)
	{
		int actionId = record.ActionId;
		List<FixedRewardMapDbfRecord> list;
		if (!this.m_fixedRewardsByAction.TryGetValue(actionId, out list))
		{
			list = new List<FixedRewardMapDbfRecord>();
			this.m_fixedRewardsByAction.Add(actionId, list);
		}
		list.Add(record);
	}

	// Token: 0x06007B47 RID: 31559 RVA: 0x0027FCC4 File Offset: 0x0027DEC4
	public void OnFixedRewardMapRemoved(List<FixedRewardMapDbfRecord> removedRecords)
	{
		HashSet<int> removedIds = new HashSet<int>(from r in removedRecords
		select r.ID);
		Predicate<FixedRewardMapDbfRecord> <>9__2;
		foreach (int key in new HashSet<int>(from r in removedRecords
		select r.ActionId))
		{
			List<FixedRewardMapDbfRecord> list;
			if (this.m_fixedRewardsByAction.TryGetValue(key, out list))
			{
				List<FixedRewardMapDbfRecord> list2 = list;
				Predicate<FixedRewardMapDbfRecord> match;
				if ((match = <>9__2) == null)
				{
					match = (<>9__2 = ((FixedRewardMapDbfRecord r) => removedIds.Contains(r.ID)));
				}
				list2.RemoveAll(match);
			}
		}
	}

	// Token: 0x06007B48 RID: 31560 RVA: 0x0027FDA4 File Offset: 0x0027DFA4
	public void PostProcessDbfLoad_FixedRewardAction(Dbf<FixedRewardActionDbfRecord> dbf)
	{
		this.m_fixedActionRecordsByType.Clear();
		foreach (FixedRewardActionDbfRecord record in dbf.GetRecords())
		{
			this.OnFixedRewardActionAdded(record);
		}
	}

	// Token: 0x06007B49 RID: 31561 RVA: 0x0027FE04 File Offset: 0x0027E004
	public void OnFixedRewardActionAdded(FixedRewardActionDbfRecord record)
	{
		string text = record.Type.ToString();
		FixedRewardAction.Type @enum;
		try
		{
			@enum = EnumUtils.GetEnum<FixedRewardAction.Type>(text);
		}
		catch
		{
			Debug.LogErrorFormat("Error parsing FixedRewardAction.Type, type did not match a FixedRewardType: {0}", new object[]
			{
				text
			});
			return;
		}
		List<FixedRewardActionDbfRecord> list;
		if (!this.m_fixedActionRecordsByType.TryGetValue(@enum, out list))
		{
			list = new List<FixedRewardActionDbfRecord>();
			this.m_fixedActionRecordsByType.Add(@enum, list);
		}
		list.Add(record);
	}

	// Token: 0x06007B4A RID: 31562 RVA: 0x0027FE80 File Offset: 0x0027E080
	public void OnFixedRewardActionRemoved(List<FixedRewardActionDbfRecord> removedRecords)
	{
		HashSet<int> removedIds = new HashSet<int>(from r in removedRecords
		select r.ID);
		HashSet<FixedRewardAction.Type> hashSet = null;
		try
		{
			hashSet = new HashSet<FixedRewardAction.Type>(from r in removedRecords
			select EnumUtils.GetEnum<FixedRewardAction.Type>(r.Type.ToString()));
		}
		catch
		{
			string format = "Error parsing FixedRewardAction.Type, type did not match a FixedRewardType: {0}";
			object[] array = new object[1];
			array[0] = string.Join(", ", (from r in removedRecords
			select r.Type.ToString()).ToArray<string>());
			Debug.LogErrorFormat(format, array);
			hashSet = new HashSet<FixedRewardAction.Type>();
		}
		Predicate<FixedRewardActionDbfRecord> <>9__3;
		foreach (FixedRewardAction.Type key in hashSet)
		{
			List<FixedRewardActionDbfRecord> list;
			if (this.m_fixedActionRecordsByType.TryGetValue(key, out list))
			{
				List<FixedRewardActionDbfRecord> list2 = list;
				Predicate<FixedRewardActionDbfRecord> match;
				if ((match = <>9__3) == null)
				{
					match = (<>9__3 = ((FixedRewardActionDbfRecord r) => removedIds.Contains(r.ID)));
				}
				list2.RemoveAll(match);
			}
		}
	}

	// Token: 0x06007B4B RID: 31563 RVA: 0x0027FFC4 File Offset: 0x0027E1C4
	public void PostProcessDbfLoad_DeckRulesetRuleSubset(Dbf<DeckRulesetRuleSubsetDbfRecord> dbf)
	{
		this.m_subsetsReferencedByRuleId.Clear();
		foreach (DeckRulesetRuleSubsetDbfRecord record in dbf.GetRecords())
		{
			this.OnDeckRulesetRuleSubsetAdded(record);
		}
	}

	// Token: 0x06007B4C RID: 31564 RVA: 0x00280024 File Offset: 0x0027E224
	public void OnDeckRulesetRuleSubsetAdded(DeckRulesetRuleSubsetDbfRecord record)
	{
		int deckRulesetRuleId = record.DeckRulesetRuleId;
		int subsetId = record.SubsetId;
		List<int> list;
		if (!this.m_subsetsReferencedByRuleId.TryGetValue(deckRulesetRuleId, out list))
		{
			list = new List<int>();
			this.m_subsetsReferencedByRuleId[deckRulesetRuleId] = list;
		}
		list.Add(subsetId);
	}

	// Token: 0x06007B4D RID: 31565 RVA: 0x0028006C File Offset: 0x0027E26C
	public void OnDeckRulesetRuleSubsetRemoved(List<DeckRulesetRuleSubsetDbfRecord> removedRecords)
	{
		using (List<DeckRulesetRuleSubsetDbfRecord>.Enumerator enumerator = removedRecords.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DeckRulesetRuleSubsetDbfRecord rec = enumerator.Current;
				List<int> list;
				if (this.m_subsetsReferencedByRuleId.TryGetValue(rec.DeckRulesetRuleId, out list))
				{
					list.RemoveAll((int subsetId) => subsetId == rec.SubsetId);
				}
			}
		}
	}

	// Token: 0x06007B4E RID: 31566 RVA: 0x002800EC File Offset: 0x0027E2EC
	public void PostProcessDbfLoad_SubsetCard(Dbf<SubsetCardDbfRecord> dbf)
	{
		this.m_subsetCards.Clear();
		foreach (SubsetCardDbfRecord record in dbf.GetRecords())
		{
			this.OnSubsetCardAdded(record);
		}
	}

	// Token: 0x06007B4F RID: 31567 RVA: 0x0028014C File Offset: 0x0027E34C
	public void OnSubsetCardAdded(SubsetCardDbfRecord record)
	{
		int subsetId = record.SubsetId;
		int cardId = record.CardId;
		CardDbfRecord record2 = GameDbf.Card.GetRecord(cardId);
		if (record2 == null)
		{
			return;
		}
		HashSet<string> hashSet;
		if (!this.m_subsetCards.TryGetValue(subsetId, out hashSet))
		{
			hashSet = new HashSet<string>();
			this.m_subsetCards[subsetId] = hashSet;
		}
		hashSet.Add(record2.NoteMiniGuid);
	}

	// Token: 0x06007B50 RID: 31568 RVA: 0x002801A8 File Offset: 0x0027E3A8
	public void OnSubsetCardRemoved(List<SubsetCardDbfRecord> removedRecords)
	{
		foreach (SubsetCardDbfRecord subsetCardDbfRecord in removedRecords)
		{
			HashSet<string> hashSet;
			if (this.m_subsetCards.TryGetValue(subsetCardDbfRecord.SubsetId, out hashSet) && hashSet != null)
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(subsetCardDbfRecord.CardId);
				if (record != null && record.NoteMiniGuid != null)
				{
					hashSet.Remove(record.NoteMiniGuid);
				}
			}
		}
	}

	// Token: 0x06007B51 RID: 31569 RVA: 0x00280230 File Offset: 0x0027E430
	public void PostProcessDbfLoad_DeckRulesetRule(Dbf<DeckRulesetRuleDbfRecord> dbf)
	{
		this.m_rulesByDeckRulesetId.Clear();
		foreach (DeckRulesetRuleDbfRecord record in dbf.GetRecords())
		{
			this.OnDeckRulesetRuleAdded(record);
		}
	}

	// Token: 0x06007B52 RID: 31570 RVA: 0x00280290 File Offset: 0x0027E490
	public void OnDeckRulesetRuleAdded(DeckRulesetRuleDbfRecord record)
	{
		HashSet<int> hashSet;
		if (!this.m_rulesByDeckRulesetId.TryGetValue(record.DeckRulesetId, out hashSet))
		{
			hashSet = new HashSet<int>();
			this.m_rulesByDeckRulesetId[record.DeckRulesetId] = hashSet;
		}
		hashSet.Add(record.ID);
	}

	// Token: 0x06007B53 RID: 31571 RVA: 0x002802D8 File Offset: 0x0027E4D8
	public void OnDeckRulesetRuleRemoved(List<DeckRulesetRuleDbfRecord> removedRecords)
	{
		foreach (DeckRulesetRuleDbfRecord deckRulesetRuleDbfRecord in removedRecords)
		{
			HashSet<int> hashSet;
			if (this.m_rulesByDeckRulesetId.TryGetValue(deckRulesetRuleDbfRecord.DeckRulesetId, out hashSet))
			{
				hashSet.Remove(deckRulesetRuleDbfRecord.ID);
			}
		}
	}

	// Token: 0x06007B54 RID: 31572 RVA: 0x00280344 File Offset: 0x0027E544
	public void PostProcessDbfLoad_CardChange(Dbf<CardChangeDbfRecord> dbf)
	{
		this.m_cardChangesByCardId.Clear();
		foreach (CardChangeDbfRecord record in dbf.GetRecords())
		{
			this.OnCardChangeAdded(record);
		}
	}

	// Token: 0x06007B55 RID: 31573 RVA: 0x002803A4 File Offset: 0x0027E5A4
	public void OnCardChangeAdded(CardChangeDbfRecord record)
	{
		List<CardChangeDbfRecord> list;
		if (!this.m_cardChangesByCardId.TryGetValue(record.CardId, out list))
		{
			list = new List<CardChangeDbfRecord>();
			this.m_cardChangesByCardId[record.CardId] = list;
		}
		list.Add(record);
	}

	// Token: 0x06007B56 RID: 31574 RVA: 0x002803E8 File Offset: 0x0027E5E8
	public void OnCardChangeRemoved(List<CardChangeDbfRecord> removedRecords)
	{
		foreach (CardChangeDbfRecord cardChangeDbfRecord in removedRecords)
		{
			List<CardChangeDbfRecord> list;
			if (this.m_cardChangesByCardId.TryGetValue(cardChangeDbfRecord.CardId, out list))
			{
				list.Remove(cardChangeDbfRecord);
			}
		}
	}

	// Token: 0x06007B57 RID: 31575 RVA: 0x0028044C File Offset: 0x0027E64C
	public void PostProcessDbfLoad_CardPlayerDeckOverride(Dbf<CardPlayerDeckOverrideDbfRecord> dbf)
	{
		this.m_cardsWithPlayerDeckOverrides.Clear();
		foreach (CardPlayerDeckOverrideDbfRecord record in dbf.GetRecords())
		{
			this.OnCardPlayerDeckOverrideAdded(record);
		}
	}

	// Token: 0x06007B58 RID: 31576 RVA: 0x002804AC File Offset: 0x0027E6AC
	public void OnCardPlayerDeckOverrideAdded(CardPlayerDeckOverrideDbfRecord record)
	{
		this.m_cardsWithPlayerDeckOverrides[record.CardId] = record.ID;
	}

	// Token: 0x06007B59 RID: 31577 RVA: 0x002804C8 File Offset: 0x0027E6C8
	public void OnCardPlayerDeckOverrideRemoved(List<CardPlayerDeckOverrideDbfRecord> removedRecords)
	{
		foreach (CardPlayerDeckOverrideDbfRecord cardPlayerDeckOverrideDbfRecord in removedRecords)
		{
			this.m_cardsWithPlayerDeckOverrides.Remove(cardPlayerDeckOverrideDbfRecord.CardId);
		}
	}

	// Token: 0x06007B5A RID: 31578 RVA: 0x00280524 File Offset: 0x0027E724
	public CardDbfRecord GetCardRecord(string cardId)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		if (cardId == "PlaceholderCard")
		{
			this.CachePlaceholderRecord();
		}
		CardDbfRecord result = null;
		this.m_cardsByCardId.TryGetValue(cardId, out result);
		return result;
	}

	// Token: 0x06007B5B RID: 31579 RVA: 0x00280560 File Offset: 0x0027E760
	public List<CardChangeDbfRecord> GetCardChangeRecords(int cardId)
	{
		List<CardChangeDbfRecord> result = null;
		this.m_cardChangesByCardId.TryGetValue(cardId, out result);
		return result;
	}

	// Token: 0x06007B5C RID: 31580 RVA: 0x0028057F File Offset: 0x0027E77F
	public CardSetDbfRecord GetCardSet(TAG_CARD_SET cardSetId)
	{
		return GameDbf.CardSet.GetRecord((int)cardSetId);
	}

	// Token: 0x06007B5D RID: 31581 RVA: 0x0028058C File Offset: 0x0027E78C
	public string GetCardSetSpellOverride(TAG_CARD_SET cardSetId, SpellType spellType)
	{
		Map<SpellType, string> map = null;
		if (this.m_spellOverridesByCardSetId.TryGetValue((int)cardSetId, out map))
		{
			string result = null;
			if (map.TryGetValue(spellType, out result))
			{
				return result;
			}
		}
		return null;
	}

	// Token: 0x06007B5E RID: 31582 RVA: 0x002805BC File Offset: 0x0027E7BC
	public void PostProcessDbfLoad_CardSetSpellOverride(Dbf<CardSetSpellOverrideDbfRecord> dbf)
	{
		this.m_spellOverridesByCardSetId.Clear();
		foreach (CardSetSpellOverrideDbfRecord record in dbf.GetRecords())
		{
			this.OnCardSetSpellOverrideAdded(record);
		}
	}

	// Token: 0x06007B5F RID: 31583 RVA: 0x0028061C File Offset: 0x0027E81C
	public void OnCardSetSpellOverrideAdded(CardSetSpellOverrideDbfRecord record)
	{
		Map<SpellType, string> map;
		if (!this.m_spellOverridesByCardSetId.TryGetValue(record.CardSetId, out map))
		{
			map = new Map<SpellType, string>();
			this.m_spellOverridesByCardSetId[record.CardSetId] = map;
		}
		SpellType spellType = (SpellType)Enum.Parse(typeof(SpellType), record.SpellType);
		if (Enum.IsDefined(typeof(SpellType), spellType))
		{
			map.Add(spellType, record.OverridePrefab);
		}
	}

	// Token: 0x06007B60 RID: 31584 RVA: 0x00280698 File Offset: 0x0027E898
	public void OnCardSetSpellOverrideRemoved(List<CardSetSpellOverrideDbfRecord> removedRecords)
	{
		foreach (CardSetSpellOverrideDbfRecord cardSetSpellOverrideDbfRecord in removedRecords)
		{
			Map<SpellType, string> map;
			if (this.m_spellOverridesByCardSetId.TryGetValue(cardSetSpellOverrideDbfRecord.CardSetId, out map))
			{
				SpellType spellType = (SpellType)Enum.Parse(typeof(SpellType), cardSetSpellOverrideDbfRecord.SpellType);
				if (Enum.IsDefined(typeof(SpellType), spellType))
				{
					map.Remove(spellType);
					if (map.Count == 0)
					{
						this.m_spellOverridesByCardSetId.Remove(cardSetSpellOverrideDbfRecord.CardSetId);
					}
				}
			}
		}
	}

	// Token: 0x06007B61 RID: 31585 RVA: 0x00280748 File Offset: 0x0027E948
	public string GetCardDiscoverString(string cardId)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		CardDiscoverStringDbfRecord cardDiscoverStringDbfRecord = null;
		if (this.m_cardDiscoverStringsByCardId.TryGetValue(cardId, out cardDiscoverStringDbfRecord))
		{
			return cardDiscoverStringDbfRecord.StringId;
		}
		return null;
	}

	// Token: 0x06007B62 RID: 31586 RVA: 0x0028077C File Offset: 0x0027E97C
	public string GetClientString(int recordId)
	{
		ClientStringDbfRecord record = GameDbf.ClientString.GetRecord(recordId);
		return (record == null) ? null : record.Text;
	}

	// Token: 0x06007B63 RID: 31587 RVA: 0x002807A8 File Offset: 0x0027E9A8
	private void CachePlaceholderRecord()
	{
		if (this.m_cardsByCardId.ContainsKey("PlaceholderCard"))
		{
			return;
		}
		CardDbfRecord cardDbfRecord = new CardDbfRecord();
		cardDbfRecord.SetID(-1);
		cardDbfRecord.SetNoteMiniGuid("PlaceholderCard");
		DbfLocValue dbfLocValue = new DbfLocValue();
		dbfLocValue.SetString(Locale.enUS, "Placeholder Card");
		cardDbfRecord.SetName(dbfLocValue);
		DbfLocValue dbfLocValue2 = new DbfLocValue();
		dbfLocValue2.SetString(Locale.enUS, "Battlecry: Someone remembers to publish this card.");
		cardDbfRecord.SetTextInHand(dbfLocValue2);
		Dictionary<GAME_TAG, int> dictionary = new Dictionary<GAME_TAG, int>();
		dictionary.Add(GAME_TAG.CARD_SET, 7);
		dictionary.Add(GAME_TAG.CARDTYPE, 4);
		dictionary.Add(GAME_TAG.CLASS, 4);
		dictionary.Add(GAME_TAG.RARITY, 4);
		dictionary.Add(GAME_TAG.FACTION, 3);
		dictionary.Add(GAME_TAG.COST, 9);
		dictionary.Add(GAME_TAG.HEALTH, 8);
		dictionary.Add(GAME_TAG.ATK, 6);
		List<CardTagDbfRecord> list = new List<CardTagDbfRecord>();
		foreach (KeyValuePair<GAME_TAG, int> keyValuePair in dictionary)
		{
			CardTagDbfRecord cardTagDbfRecord = new CardTagDbfRecord();
			cardTagDbfRecord.SetCardId(cardDbfRecord.ID);
			cardTagDbfRecord.SetTagId((int)keyValuePair.Key);
			cardTagDbfRecord.SetTagValue(keyValuePair.Value);
			list.Add(cardTagDbfRecord);
		}
		this.m_cardsByCardId.Add("PlaceholderCard", cardDbfRecord);
		this.m_cardTagsByCardDbId.Add(cardDbfRecord.ID, list);
	}

	// Token: 0x06007B64 RID: 31588 RVA: 0x0028090C File Offset: 0x0027EB0C
	public int GetCollectibleCardCount()
	{
		return this.m_collectibleCardCount;
	}

	// Token: 0x06007B65 RID: 31589 RVA: 0x00280914 File Offset: 0x0027EB14
	public List<string> GetAllCardIds()
	{
		return this.m_allCardIds;
	}

	// Token: 0x06007B66 RID: 31590 RVA: 0x0028091C File Offset: 0x0027EB1C
	public List<int> GetAllCardDbIds()
	{
		return this.m_allCardDbIds;
	}

	// Token: 0x06007B67 RID: 31591 RVA: 0x00280924 File Offset: 0x0027EB24
	public List<string> GetCollectibleCardIds()
	{
		return this.m_collectibleCardIds;
	}

	// Token: 0x06007B68 RID: 31592 RVA: 0x0028092C File Offset: 0x0027EB2C
	public List<int> GetCollectibleCardDbIds()
	{
		return this.m_collectibleCardDbIds;
	}

	// Token: 0x06007B69 RID: 31593 RVA: 0x00280934 File Offset: 0x0027EB34
	public List<string> GetCardsWithFeaturedCardsEvent()
	{
		return this.m_featuredCardEventCardIds;
	}

	// Token: 0x06007B6A RID: 31594 RVA: 0x0028093C File Offset: 0x0027EB3C
	public List<FixedRewardMapDbfRecord> GetFixedRewardMapRecordsForAction(int actionId)
	{
		List<FixedRewardMapDbfRecord> list = null;
		if (!this.m_fixedRewardsByAction.TryGetValue(actionId, out list))
		{
			list = new List<FixedRewardMapDbfRecord>();
			this.m_fixedRewardsByAction[actionId] = list;
		}
		return list;
	}

	// Token: 0x06007B6B RID: 31595 RVA: 0x00280970 File Offset: 0x0027EB70
	public List<FixedRewardActionDbfRecord> GetFixedActionRecordsForType(FixedRewardAction.Type type)
	{
		List<FixedRewardActionDbfRecord> list = null;
		if (!this.m_fixedActionRecordsByType.TryGetValue(type, out list))
		{
			list = new List<FixedRewardActionDbfRecord>();
			this.m_fixedActionRecordsByType[type] = list;
		}
		return list;
	}

	// Token: 0x06007B6C RID: 31596 RVA: 0x002809A4 File Offset: 0x0027EBA4
	public List<HashSet<string>> GetSubsetsForRule(int ruleId)
	{
		List<HashSet<string>> list = new List<HashSet<string>>();
		List<int> list2;
		if (this.m_subsetsReferencedByRuleId.TryGetValue(ruleId, out list2))
		{
			for (int i = 0; i < list2.Count; i++)
			{
				list.Add(this.GetSubsetById(list2[i]));
			}
		}
		return list;
	}

	// Token: 0x06007B6D RID: 31597 RVA: 0x002809EC File Offset: 0x0027EBEC
	public List<int> GetCardSetIdsForSubsetRule(int ruleId)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		if (this.m_subsetsReferencedByRuleId.TryGetValue(ruleId, out list))
		{
			foreach (int id in list)
			{
				SubsetDbfRecord record = GameDbf.Subset.GetRecord(id);
				if (record != null)
				{
					foreach (SubsetRuleDbfRecord subsetRuleDbfRecord in record.Rules)
					{
						if (subsetRuleDbfRecord.Tag == 183 && !subsetRuleDbfRecord.RuleIsNot && subsetRuleDbfRecord.MaxValue == subsetRuleDbfRecord.MinValue)
						{
							list2.Add(subsetRuleDbfRecord.MaxValue);
						}
					}
				}
			}
		}
		return list2;
	}

	// Token: 0x06007B6E RID: 31598 RVA: 0x00280ADC File Offset: 0x0027ECDC
	public DeckRulesetRuleDbfRecord[] GetRulesForDeckRuleset(int deckRulesetId)
	{
		HashSet<int> source;
		if (!this.m_rulesByDeckRulesetId.TryGetValue(deckRulesetId, out source))
		{
			source = new HashSet<int>();
		}
		return (from ruleId in source
		let ruleDbf = GameDbf.DeckRulesetRule.GetRecord(ruleId)
		where ruleDbf != null
		select ruleDbf).ToArray<DeckRulesetRuleDbfRecord>();
	}

	// Token: 0x06007B6F RID: 31599 RVA: 0x00280B74 File Offset: 0x0027ED74
	public HashSet<string> GetSubsetById(int id)
	{
		HashSet<string> hashSet = null;
		if (!this.m_subsetCards.TryGetValue(id, out hashSet))
		{
			hashSet = new HashSet<string>();
			this.m_subsetCards[id] = hashSet;
		}
		return hashSet;
	}

	// Token: 0x06007B70 RID: 31600 RVA: 0x00280BA7 File Offset: 0x0027EDA7
	public IEnumerable<CardPlayerDeckOverrideDbfRecord> GetAllCardPlayerDeckOverrides()
	{
		return this.m_cardsWithPlayerDeckOverrides.Select(delegate(KeyValuePair<int, int> kv)
		{
			Dbf<CardPlayerDeckOverrideDbfRecord> cardPlayerDeckOverride = GameDbf.CardPlayerDeckOverride;
			KeyValuePair<int, int> keyValuePair = kv;
			return cardPlayerDeckOverride.GetRecord(keyValuePair.Value);
		});
	}

	// Token: 0x06007B71 RID: 31601 RVA: 0x00280BD4 File Offset: 0x0027EDD4
	public bool HasCardPlayerDeckOverride(string cardId)
	{
		int key = GameUtils.TranslateCardIdToDbId(cardId, false);
		int num;
		return this.m_cardsWithPlayerDeckOverrides.TryGetValue(key, out num);
	}

	// Token: 0x06007B72 RID: 31602 RVA: 0x00280BF8 File Offset: 0x0027EDF8
	public CardPlayerDeckOverrideDbfRecord GetCardPlayerDeckOverride(string cardId)
	{
		int key = GameUtils.TranslateCardIdToDbId(cardId, false);
		int id;
		if (!this.m_cardsWithPlayerDeckOverrides.TryGetValue(key, out id))
		{
			return null;
		}
		return GameDbf.CardPlayerDeckOverride.GetRecord(id);
	}

	// Token: 0x040064BE RID: 25790
	private Map<string, CardDbfRecord> m_cardsByCardId;

	// Token: 0x040064BF RID: 25791
	private Map<int, List<CardTagDbfRecord>> m_cardTagsByCardDbId;

	// Token: 0x040064C0 RID: 25792
	private Map<string, CardDiscoverStringDbfRecord> m_cardDiscoverStringsByCardId;

	// Token: 0x040064C1 RID: 25793
	private List<string> m_allCardIds;

	// Token: 0x040064C2 RID: 25794
	private List<int> m_allCardDbIds;

	// Token: 0x040064C3 RID: 25795
	private List<string> m_collectibleCardIds;

	// Token: 0x040064C4 RID: 25796
	private List<int> m_collectibleCardDbIds;

	// Token: 0x040064C5 RID: 25797
	private int m_collectibleCardCount;

	// Token: 0x040064C6 RID: 25798
	private List<string> m_featuredCardEventCardIds;

	// Token: 0x040064C7 RID: 25799
	private Map<int, List<FixedRewardMapDbfRecord>> m_fixedRewardsByAction;

	// Token: 0x040064C8 RID: 25800
	private Map<FixedRewardAction.Type, List<FixedRewardActionDbfRecord>> m_fixedActionRecordsByType;

	// Token: 0x040064C9 RID: 25801
	private Map<int, List<int>> m_subsetsReferencedByRuleId;

	// Token: 0x040064CA RID: 25802
	private Map<int, HashSet<string>> m_subsetCards;

	// Token: 0x040064CB RID: 25803
	private Map<int, HashSet<int>> m_rulesByDeckRulesetId;

	// Token: 0x040064CC RID: 25804
	private Map<int, List<CardChangeDbfRecord>> m_cardChangesByCardId;

	// Token: 0x040064CD RID: 25805
	private Map<int, Map<SpellType, string>> m_spellOverridesByCardSetId;

	// Token: 0x040064CE RID: 25806
	private Map<int, int> m_cardsWithPlayerDeckOverrides;
}
