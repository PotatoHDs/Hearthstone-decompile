using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine;

public class GameDbfIndex
{
	private Map<string, CardDbfRecord> m_cardsByCardId;

	private Map<int, List<CardTagDbfRecord>> m_cardTagsByCardDbId;

	private Map<string, CardDiscoverStringDbfRecord> m_cardDiscoverStringsByCardId;

	private List<string> m_allCardIds;

	private List<int> m_allCardDbIds;

	private List<string> m_collectibleCardIds;

	private List<int> m_collectibleCardDbIds;

	private int m_collectibleCardCount;

	private List<string> m_featuredCardEventCardIds;

	private Map<int, List<FixedRewardMapDbfRecord>> m_fixedRewardsByAction;

	private Map<FixedRewardAction.Type, List<FixedRewardActionDbfRecord>> m_fixedActionRecordsByType;

	private Map<int, List<int>> m_subsetsReferencedByRuleId;

	private Map<int, HashSet<string>> m_subsetCards;

	private Map<int, HashSet<int>> m_rulesByDeckRulesetId;

	private Map<int, List<CardChangeDbfRecord>> m_cardChangesByCardId;

	private Map<int, Map<SpellType, string>> m_spellOverridesByCardSetId;

	private Map<int, int> m_cardsWithPlayerDeckOverrides;

	public GameDbfIndex()
	{
		Initialize();
	}

	public void Initialize()
	{
		m_cardsByCardId = new Map<string, CardDbfRecord>();
		m_cardTagsByCardDbId = new Map<int, List<CardTagDbfRecord>>();
		m_cardDiscoverStringsByCardId = new Map<string, CardDiscoverStringDbfRecord>();
		m_allCardIds = new List<string>();
		m_allCardDbIds = new List<int>();
		m_collectibleCardIds = new List<string>();
		m_collectibleCardDbIds = new List<int>();
		m_collectibleCardCount = 0;
		m_featuredCardEventCardIds = new List<string>();
		m_fixedRewardsByAction = new Map<int, List<FixedRewardMapDbfRecord>>();
		m_fixedActionRecordsByType = new Map<FixedRewardAction.Type, List<FixedRewardActionDbfRecord>>();
		m_subsetsReferencedByRuleId = new Map<int, List<int>>();
		m_subsetCards = new Map<int, HashSet<string>>();
		m_rulesByDeckRulesetId = new Map<int, HashSet<int>>();
		m_cardChangesByCardId = new Map<int, List<CardChangeDbfRecord>>();
		m_cardsWithPlayerDeckOverrides = new Map<int, int>();
		m_spellOverridesByCardSetId = new Map<int, Map<SpellType, string>>();
	}

	public void PostProcessDbfLoad_CardTag(Dbf<CardTagDbfRecord> dbf)
	{
		m_cardTagsByCardDbId.Clear();
		foreach (CardTagDbfRecord record in dbf.GetRecords())
		{
			OnCardTagAdded(record);
		}
	}

	public void OnCardTagAdded(CardTagDbfRecord cardTagRecord)
	{
		int cardId = cardTagRecord.CardId;
		List<CardTagDbfRecord> value = null;
		if (!m_cardTagsByCardDbId.TryGetValue(cardId, out value))
		{
			value = new List<CardTagDbfRecord>();
			m_cardTagsByCardDbId[cardId] = value;
		}
		value.Add(cardTagRecord);
	}

	public void OnCardTagRemoved(List<CardTagDbfRecord> removedRecords)
	{
		foreach (CardTagDbfRecord removedRecord in removedRecords)
		{
			using Map<int, List<CardTagDbfRecord>>.ValueCollection.Enumerator enumerator2 = m_cardTagsByCardDbId.Values.GetEnumerator();
			while (enumerator2.MoveNext() && !enumerator2.Current.Remove(removedRecord))
			{
			}
		}
	}

	public void PostProcessDbfLoad_CardDiscoverString(Dbf<CardDiscoverStringDbfRecord> dbf)
	{
		m_cardDiscoverStringsByCardId.Clear();
		foreach (CardDiscoverStringDbfRecord record in dbf.GetRecords())
		{
			OnCardDiscoverStringAdded(record);
		}
	}

	public void OnCardDiscoverStringAdded(CardDiscoverStringDbfRecord cardDiscoverStringRecord)
	{
		m_cardDiscoverStringsByCardId[cardDiscoverStringRecord.NoteMiniGuid] = cardDiscoverStringRecord;
	}

	public void OnCardDiscoverStringRemoved(List<CardDiscoverStringDbfRecord> removedRecords)
	{
		foreach (CardDiscoverStringDbfRecord removedRecord in removedRecords)
		{
			m_cardDiscoverStringsByCardId.Remove(removedRecord.NoteMiniGuid);
		}
	}

	public int GetCardTagValue(int cardDbId, GAME_TAG tagId)
	{
		List<CardTagDbfRecord> value = null;
		if (!m_cardTagsByCardDbId.TryGetValue(cardDbId, out value))
		{
			return 0;
		}
		return value.Find((CardTagDbfRecord item) => item.TagId == (int)tagId)?.TagValue ?? 0;
	}

	public IEnumerable<CardTagDbfRecord> GetCardTagRecords(int cardDbId)
	{
		List<CardTagDbfRecord> value = null;
		if (!m_cardTagsByCardDbId.TryGetValue(cardDbId, out value))
		{
			return null;
		}
		return value;
	}

	public void PostProcessDbfLoad_Card(Dbf<CardDbfRecord> dbf)
	{
		m_cardsByCardId.Clear();
		m_allCardDbIds.Clear();
		m_allCardIds.Clear();
		m_collectibleCardCount = 0;
		m_collectibleCardIds.Clear();
		m_collectibleCardDbIds.Clear();
		m_featuredCardEventCardIds.Clear();
		foreach (CardDbfRecord record in dbf.GetRecords())
		{
			OnCardAdded(record);
		}
	}

	public void OnCardAdded(CardDbfRecord cardRecord)
	{
		int iD = cardRecord.ID;
		bool num = GetCardTagValue(iD, GAME_TAG.COLLECTIBLE) == 1;
		string noteMiniGuid = cardRecord.NoteMiniGuid;
		m_cardsByCardId[noteMiniGuid] = cardRecord;
		m_allCardDbIds.Add(iD);
		m_allCardIds.Add(noteMiniGuid);
		if (num)
		{
			m_collectibleCardCount++;
			m_collectibleCardIds.Add(noteMiniGuid);
			m_collectibleCardDbIds.Add(iD);
		}
		if (!string.IsNullOrEmpty(cardRecord.FeaturedCardsEvent))
		{
			m_featuredCardEventCardIds.Add(noteMiniGuid);
		}
	}

	public void OnCardRemoved(List<CardDbfRecord> removedRecords)
	{
		HashSet<int> removedCardDbIds = new HashSet<int>();
		HashSet<string> removedCardIds = new HashSet<string>();
		foreach (CardDbfRecord removedRecord in removedRecords)
		{
			removedCardDbIds.Add(removedRecord.ID);
			if (removedRecord.NoteMiniGuid != null)
			{
				removedCardIds.Add(removedRecord.NoteMiniGuid);
				m_cardsByCardId.Remove(removedRecord.NoteMiniGuid);
			}
		}
		if (removedCardDbIds.Count > 0)
		{
			m_allCardDbIds.RemoveAll((int cardDbId) => removedCardDbIds.Contains(cardDbId));
			m_collectibleCardDbIds.RemoveAll((int cardDbId) => m_collectibleCardDbIds.Contains(cardDbId));
		}
		if (removedCardIds.Count > 0)
		{
			m_allCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
			m_collectibleCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
			m_featuredCardEventCardIds.RemoveAll((string cardId) => removedCardIds.Contains(cardId));
		}
	}

	public void PostProcessDbfLoad_FixedRewardMap(Dbf<FixedRewardMapDbfRecord> dbf)
	{
		m_fixedRewardsByAction.Clear();
		foreach (FixedRewardMapDbfRecord record in dbf.GetRecords())
		{
			OnFixedRewardMapAdded(record);
		}
	}

	public void OnFixedRewardMapAdded(FixedRewardMapDbfRecord record)
	{
		int actionId = record.ActionId;
		if (!m_fixedRewardsByAction.TryGetValue(actionId, out var value))
		{
			value = new List<FixedRewardMapDbfRecord>();
			m_fixedRewardsByAction.Add(actionId, value);
		}
		value.Add(record);
	}

	public void OnFixedRewardMapRemoved(List<FixedRewardMapDbfRecord> removedRecords)
	{
		HashSet<int> removedIds = new HashSet<int>(removedRecords.Select((FixedRewardMapDbfRecord r) => r.ID));
		foreach (int item in new HashSet<int>(removedRecords.Select((FixedRewardMapDbfRecord r) => r.ActionId)))
		{
			if (m_fixedRewardsByAction.TryGetValue(item, out var value))
			{
				value.RemoveAll((FixedRewardMapDbfRecord r) => removedIds.Contains(r.ID));
			}
		}
	}

	public void PostProcessDbfLoad_FixedRewardAction(Dbf<FixedRewardActionDbfRecord> dbf)
	{
		m_fixedActionRecordsByType.Clear();
		foreach (FixedRewardActionDbfRecord record in dbf.GetRecords())
		{
			OnFixedRewardActionAdded(record);
		}
	}

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
			Debug.LogErrorFormat("Error parsing FixedRewardAction.Type, type did not match a FixedRewardType: {0}", text);
			return;
		}
		if (!m_fixedActionRecordsByType.TryGetValue(@enum, out var value))
		{
			value = new List<FixedRewardActionDbfRecord>();
			m_fixedActionRecordsByType.Add(@enum, value);
		}
		value.Add(record);
	}

	public void OnFixedRewardActionRemoved(List<FixedRewardActionDbfRecord> removedRecords)
	{
		HashSet<int> removedIds = new HashSet<int>(removedRecords.Select((FixedRewardActionDbfRecord r) => r.ID));
		HashSet<FixedRewardAction.Type> hashSet = null;
		try
		{
			hashSet = new HashSet<FixedRewardAction.Type>(removedRecords.Select((FixedRewardActionDbfRecord r) => EnumUtils.GetEnum<FixedRewardAction.Type>(r.Type.ToString())));
		}
		catch
		{
			Debug.LogErrorFormat("Error parsing FixedRewardAction.Type, type did not match a FixedRewardType: {0}", string.Join(", ", removedRecords.Select((FixedRewardActionDbfRecord r) => r.Type.ToString()).ToArray()));
			hashSet = new HashSet<FixedRewardAction.Type>();
		}
		foreach (FixedRewardAction.Type item in hashSet)
		{
			if (m_fixedActionRecordsByType.TryGetValue(item, out var value))
			{
				value.RemoveAll((FixedRewardActionDbfRecord r) => removedIds.Contains(r.ID));
			}
		}
	}

	public void PostProcessDbfLoad_DeckRulesetRuleSubset(Dbf<DeckRulesetRuleSubsetDbfRecord> dbf)
	{
		m_subsetsReferencedByRuleId.Clear();
		foreach (DeckRulesetRuleSubsetDbfRecord record in dbf.GetRecords())
		{
			OnDeckRulesetRuleSubsetAdded(record);
		}
	}

	public void OnDeckRulesetRuleSubsetAdded(DeckRulesetRuleSubsetDbfRecord record)
	{
		int deckRulesetRuleId = record.DeckRulesetRuleId;
		int subsetId = record.SubsetId;
		if (!m_subsetsReferencedByRuleId.TryGetValue(deckRulesetRuleId, out var value))
		{
			value = new List<int>();
			m_subsetsReferencedByRuleId[deckRulesetRuleId] = value;
		}
		value.Add(subsetId);
	}

	public void OnDeckRulesetRuleSubsetRemoved(List<DeckRulesetRuleSubsetDbfRecord> removedRecords)
	{
		foreach (DeckRulesetRuleSubsetDbfRecord rec in removedRecords)
		{
			if (m_subsetsReferencedByRuleId.TryGetValue(rec.DeckRulesetRuleId, out var value))
			{
				value.RemoveAll((int subsetId) => subsetId == rec.SubsetId);
			}
		}
	}

	public void PostProcessDbfLoad_SubsetCard(Dbf<SubsetCardDbfRecord> dbf)
	{
		m_subsetCards.Clear();
		foreach (SubsetCardDbfRecord record in dbf.GetRecords())
		{
			OnSubsetCardAdded(record);
		}
	}

	public void OnSubsetCardAdded(SubsetCardDbfRecord record)
	{
		int subsetId = record.SubsetId;
		int cardId = record.CardId;
		CardDbfRecord record2 = GameDbf.Card.GetRecord(cardId);
		if (record2 != null)
		{
			if (!m_subsetCards.TryGetValue(subsetId, out var value))
			{
				value = new HashSet<string>();
				m_subsetCards[subsetId] = value;
			}
			value.Add(record2.NoteMiniGuid);
		}
	}

	public void OnSubsetCardRemoved(List<SubsetCardDbfRecord> removedRecords)
	{
		foreach (SubsetCardDbfRecord removedRecord in removedRecords)
		{
			if (m_subsetCards.TryGetValue(removedRecord.SubsetId, out var value) && value != null)
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(removedRecord.CardId);
				if (record != null && record.NoteMiniGuid != null)
				{
					value.Remove(record.NoteMiniGuid);
				}
			}
		}
	}

	public void PostProcessDbfLoad_DeckRulesetRule(Dbf<DeckRulesetRuleDbfRecord> dbf)
	{
		m_rulesByDeckRulesetId.Clear();
		foreach (DeckRulesetRuleDbfRecord record in dbf.GetRecords())
		{
			OnDeckRulesetRuleAdded(record);
		}
	}

	public void OnDeckRulesetRuleAdded(DeckRulesetRuleDbfRecord record)
	{
		if (!m_rulesByDeckRulesetId.TryGetValue(record.DeckRulesetId, out var value))
		{
			value = new HashSet<int>();
			m_rulesByDeckRulesetId[record.DeckRulesetId] = value;
		}
		value.Add(record.ID);
	}

	public void OnDeckRulesetRuleRemoved(List<DeckRulesetRuleDbfRecord> removedRecords)
	{
		foreach (DeckRulesetRuleDbfRecord removedRecord in removedRecords)
		{
			if (m_rulesByDeckRulesetId.TryGetValue(removedRecord.DeckRulesetId, out var value))
			{
				value.Remove(removedRecord.ID);
			}
		}
	}

	public void PostProcessDbfLoad_CardChange(Dbf<CardChangeDbfRecord> dbf)
	{
		m_cardChangesByCardId.Clear();
		foreach (CardChangeDbfRecord record in dbf.GetRecords())
		{
			OnCardChangeAdded(record);
		}
	}

	public void OnCardChangeAdded(CardChangeDbfRecord record)
	{
		if (!m_cardChangesByCardId.TryGetValue(record.CardId, out var value))
		{
			value = new List<CardChangeDbfRecord>();
			m_cardChangesByCardId[record.CardId] = value;
		}
		value.Add(record);
	}

	public void OnCardChangeRemoved(List<CardChangeDbfRecord> removedRecords)
	{
		foreach (CardChangeDbfRecord removedRecord in removedRecords)
		{
			if (m_cardChangesByCardId.TryGetValue(removedRecord.CardId, out var value))
			{
				value.Remove(removedRecord);
			}
		}
	}

	public void PostProcessDbfLoad_CardPlayerDeckOverride(Dbf<CardPlayerDeckOverrideDbfRecord> dbf)
	{
		m_cardsWithPlayerDeckOverrides.Clear();
		foreach (CardPlayerDeckOverrideDbfRecord record in dbf.GetRecords())
		{
			OnCardPlayerDeckOverrideAdded(record);
		}
	}

	public void OnCardPlayerDeckOverrideAdded(CardPlayerDeckOverrideDbfRecord record)
	{
		m_cardsWithPlayerDeckOverrides[record.CardId] = record.ID;
	}

	public void OnCardPlayerDeckOverrideRemoved(List<CardPlayerDeckOverrideDbfRecord> removedRecords)
	{
		foreach (CardPlayerDeckOverrideDbfRecord removedRecord in removedRecords)
		{
			m_cardsWithPlayerDeckOverrides.Remove(removedRecord.CardId);
		}
	}

	public CardDbfRecord GetCardRecord(string cardId)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		if (cardId == "PlaceholderCard")
		{
			CachePlaceholderRecord();
		}
		CardDbfRecord value = null;
		m_cardsByCardId.TryGetValue(cardId, out value);
		return value;
	}

	public List<CardChangeDbfRecord> GetCardChangeRecords(int cardId)
	{
		List<CardChangeDbfRecord> value = null;
		m_cardChangesByCardId.TryGetValue(cardId, out value);
		return value;
	}

	public CardSetDbfRecord GetCardSet(TAG_CARD_SET cardSetId)
	{
		return GameDbf.CardSet.GetRecord((int)cardSetId);
	}

	public string GetCardSetSpellOverride(TAG_CARD_SET cardSetId, SpellType spellType)
	{
		Map<SpellType, string> value = null;
		if (m_spellOverridesByCardSetId.TryGetValue((int)cardSetId, out value))
		{
			string value2 = null;
			if (value.TryGetValue(spellType, out value2))
			{
				return value2;
			}
		}
		return null;
	}

	public void PostProcessDbfLoad_CardSetSpellOverride(Dbf<CardSetSpellOverrideDbfRecord> dbf)
	{
		m_spellOverridesByCardSetId.Clear();
		foreach (CardSetSpellOverrideDbfRecord record in dbf.GetRecords())
		{
			OnCardSetSpellOverrideAdded(record);
		}
	}

	public void OnCardSetSpellOverrideAdded(CardSetSpellOverrideDbfRecord record)
	{
		if (!m_spellOverridesByCardSetId.TryGetValue(record.CardSetId, out var value))
		{
			value = new Map<SpellType, string>();
			m_spellOverridesByCardSetId[record.CardSetId] = value;
		}
		SpellType spellType = (SpellType)Enum.Parse(typeof(SpellType), record.SpellType);
		if (Enum.IsDefined(typeof(SpellType), spellType))
		{
			value.Add(spellType, record.OverridePrefab);
		}
	}

	public void OnCardSetSpellOverrideRemoved(List<CardSetSpellOverrideDbfRecord> removedRecords)
	{
		foreach (CardSetSpellOverrideDbfRecord removedRecord in removedRecords)
		{
			if (!m_spellOverridesByCardSetId.TryGetValue(removedRecord.CardSetId, out var value))
			{
				continue;
			}
			SpellType spellType = (SpellType)Enum.Parse(typeof(SpellType), removedRecord.SpellType);
			if (Enum.IsDefined(typeof(SpellType), spellType))
			{
				value.Remove(spellType);
				if (value.Count == 0)
				{
					m_spellOverridesByCardSetId.Remove(removedRecord.CardSetId);
				}
			}
		}
	}

	public string GetCardDiscoverString(string cardId)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		CardDiscoverStringDbfRecord value = null;
		if (m_cardDiscoverStringsByCardId.TryGetValue(cardId, out value))
		{
			return value.StringId;
		}
		return null;
	}

	public string GetClientString(int recordId)
	{
		return GameDbf.ClientString.GetRecord(recordId)?.Text;
	}

	private void CachePlaceholderRecord()
	{
		if (m_cardsByCardId.ContainsKey("PlaceholderCard"))
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
		Dictionary<GAME_TAG, int> obj = new Dictionary<GAME_TAG, int>
		{
			{
				GAME_TAG.CARD_SET,
				7
			},
			{
				GAME_TAG.CARDTYPE,
				4
			},
			{
				GAME_TAG.CLASS,
				4
			},
			{
				GAME_TAG.RARITY,
				4
			},
			{
				GAME_TAG.FACTION,
				3
			},
			{
				GAME_TAG.COST,
				9
			},
			{
				GAME_TAG.HEALTH,
				8
			},
			{
				GAME_TAG.ATK,
				6
			}
		};
		List<CardTagDbfRecord> list = new List<CardTagDbfRecord>();
		foreach (KeyValuePair<GAME_TAG, int> item in obj)
		{
			CardTagDbfRecord cardTagDbfRecord = new CardTagDbfRecord();
			cardTagDbfRecord.SetCardId(cardDbfRecord.ID);
			cardTagDbfRecord.SetTagId((int)item.Key);
			cardTagDbfRecord.SetTagValue(item.Value);
			list.Add(cardTagDbfRecord);
		}
		m_cardsByCardId.Add("PlaceholderCard", cardDbfRecord);
		m_cardTagsByCardDbId.Add(cardDbfRecord.ID, list);
	}

	public int GetCollectibleCardCount()
	{
		return m_collectibleCardCount;
	}

	public List<string> GetAllCardIds()
	{
		return m_allCardIds;
	}

	public List<int> GetAllCardDbIds()
	{
		return m_allCardDbIds;
	}

	public List<string> GetCollectibleCardIds()
	{
		return m_collectibleCardIds;
	}

	public List<int> GetCollectibleCardDbIds()
	{
		return m_collectibleCardDbIds;
	}

	public List<string> GetCardsWithFeaturedCardsEvent()
	{
		return m_featuredCardEventCardIds;
	}

	public List<FixedRewardMapDbfRecord> GetFixedRewardMapRecordsForAction(int actionId)
	{
		List<FixedRewardMapDbfRecord> value = null;
		if (!m_fixedRewardsByAction.TryGetValue(actionId, out value))
		{
			value = new List<FixedRewardMapDbfRecord>();
			m_fixedRewardsByAction[actionId] = value;
		}
		return value;
	}

	public List<FixedRewardActionDbfRecord> GetFixedActionRecordsForType(FixedRewardAction.Type type)
	{
		List<FixedRewardActionDbfRecord> value = null;
		if (!m_fixedActionRecordsByType.TryGetValue(type, out value))
		{
			value = new List<FixedRewardActionDbfRecord>();
			m_fixedActionRecordsByType[type] = value;
		}
		return value;
	}

	public List<HashSet<string>> GetSubsetsForRule(int ruleId)
	{
		List<HashSet<string>> list = new List<HashSet<string>>();
		if (m_subsetsReferencedByRuleId.TryGetValue(ruleId, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				list.Add(GetSubsetById(value[i]));
			}
		}
		return list;
	}

	public List<int> GetCardSetIdsForSubsetRule(int ruleId)
	{
		List<int> value = new List<int>();
		List<int> list = new List<int>();
		if (m_subsetsReferencedByRuleId.TryGetValue(ruleId, out value))
		{
			foreach (int item in value)
			{
				SubsetDbfRecord record = GameDbf.Subset.GetRecord(item);
				if (record != null)
				{
					foreach (SubsetRuleDbfRecord rule in record.Rules)
					{
						if (rule.Tag == 183 && !rule.RuleIsNot && rule.MaxValue == rule.MinValue)
						{
							list.Add(rule.MaxValue);
						}
					}
				}
			}
			return list;
		}
		return list;
	}

	public DeckRulesetRuleDbfRecord[] GetRulesForDeckRuleset(int deckRulesetId)
	{
		if (!m_rulesByDeckRulesetId.TryGetValue(deckRulesetId, out var value))
		{
			value = new HashSet<int>();
		}
		return (from ruleId in value
			let ruleDbf = GameDbf.DeckRulesetRule.GetRecord(ruleId)
			where ruleDbf != null
			select ruleDbf).ToArray();
	}

	public HashSet<string> GetSubsetById(int id)
	{
		HashSet<string> value = null;
		if (!m_subsetCards.TryGetValue(id, out value))
		{
			value = new HashSet<string>();
			m_subsetCards[id] = value;
		}
		return value;
	}

	public IEnumerable<CardPlayerDeckOverrideDbfRecord> GetAllCardPlayerDeckOverrides()
	{
		return m_cardsWithPlayerDeckOverrides.Select(delegate(KeyValuePair<int, int> kv)
		{
			Dbf<CardPlayerDeckOverrideDbfRecord> cardPlayerDeckOverride = GameDbf.CardPlayerDeckOverride;
			KeyValuePair<int, int> keyValuePair = kv;
			return cardPlayerDeckOverride.GetRecord(keyValuePair.Value);
		});
	}

	public bool HasCardPlayerDeckOverride(string cardId)
	{
		int key = GameUtils.TranslateCardIdToDbId(cardId);
		int value;
		return m_cardsWithPlayerDeckOverrides.TryGetValue(key, out value);
	}

	public CardPlayerDeckOverrideDbfRecord GetCardPlayerDeckOverride(string cardId)
	{
		int key = GameUtils.TranslateCardIdToDbId(cardId);
		if (!m_cardsWithPlayerDeckOverrides.TryGetValue(key, out var value))
		{
			return null;
		}
		return GameDbf.CardPlayerDeckOverride.GetRecord(value);
	}
}
