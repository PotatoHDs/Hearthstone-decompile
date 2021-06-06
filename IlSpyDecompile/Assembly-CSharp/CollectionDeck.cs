using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class CollectionDeck
{
	public enum SlotStatus
	{
		UNKNOWN,
		VALID,
		NOT_VALID,
		MISSING
	}

	public static int DefaultMaxDeckNameCharacters = 24;

	private string m_name;

	private List<CollectionDeckSlot> m_slots = new List<CollectionDeckSlot>();

	private bool m_forceDuplicatesIntoSeparateSlots;

	private bool m_netContentsLoaded;

	private bool m_isSavingContentChanges;

	private bool m_isSavingNameChanges;

	private bool m_isBeingDeleted;

	private ShareableDeck m_createdFromShareableDeck;

	public long ID;

	public DeckType Type = DeckType.NORMAL_DECK;

	public string HeroCardID = string.Empty;

	public TAG_PREMIUM HeroPremium;

	public bool HeroOverridden;

	public int CardBackID;

	public bool CardBackOverridden;

	public int SeasonId;

	public int BrawlLibraryItemId;

	public bool NeedsName;

	public long SortOrder;

	public ulong CreateDate;

	public bool Locked;

	public DeckSourceType SourceType;

	public string HeroPowerCardID = string.Empty;

	public string UIHeroOverrideCardID = string.Empty;

	public TAG_PREMIUM UIHeroOverridePremium;

	public string Name
	{
		get
		{
			return m_name;
		}
		set
		{
			if (value == null)
			{
				Debug.LogError($"CollectionDeck.SetName() - null name given for deck {this}");
			}
			else if (!value.Equals(m_name, StringComparison.InvariantCultureIgnoreCase))
			{
				m_name = value;
			}
		}
	}

	public FormatType FormatType { get; set; }

	public bool IsShared { get; set; }

	public bool IsCreatedWithDeckComplete { get; set; }

	public bool IsBrawlDeck => TavernBrawlManager.IsBrawlDeckType(Type);

	public bool IsDuelsDeck
	{
		get
		{
			if (Type != DeckType.PVPDR_DECK)
			{
				return Type == DeckType.PVPDR_DISPLAY_DECK;
			}
			return true;
		}
	}

	public bool IsValidForRuleset
	{
		get
		{
			if (IsShared)
			{
				return true;
			}
			if (!m_netContentsLoaded && Type != DeckType.CLIENT_ONLY_DECK && Type != DeckType.PVPDR_DISPLAY_DECK)
			{
				return false;
			}
			return GetRuleset()?.IsDeckValid(this) ?? false;
		}
	}

	public bool ForceDuplicatesIntoSeparateSlots
	{
		get
		{
			return m_forceDuplicatesIntoSeparateSlots;
		}
		set
		{
			m_forceDuplicatesIntoSeparateSlots = value;
		}
	}

	public ShareableDeck CreatedFromShareableDeck => m_createdFromShareableDeck;

	public override string ToString()
	{
		return $"Deck [id={ID} name=\"{Name}\" heroCardId={HeroCardID} heroCardFlair={HeroPremium} cardBackId={CardBackID} cardBackOverridden={CardBackOverridden} heroOverridden={HeroOverridden} slotCount={GetSlotCount()} needsName={NeedsName} sortOrder={SortOrder}]";
	}

	public bool HasUIHeroOverride()
	{
		return !string.IsNullOrEmpty(UIHeroOverrideCardID);
	}

	public string GetDisplayHeroCardID()
	{
		if (HasUIHeroOverride())
		{
			return UIHeroOverrideCardID;
		}
		return HeroCardID;
	}

	public TAG_PREMIUM? GetDisplayHeroPremiumOverride()
	{
		if (HasUIHeroOverride())
		{
			return UIHeroOverridePremium;
		}
		return null;
	}

	public List<long> GetCards()
	{
		List<long> list = new List<long>();
		for (int i = 0; i < m_slots.Count; i++)
		{
			long item = GameUtils.TranslateCardIdToDbId(m_slots[i].CardID);
			for (int j = 0; j < m_slots[i].Count; j++)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<string> GetCardsWithCardID()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < m_slots.Count; i++)
		{
			for (int j = 0; j < m_slots[i].Count; j++)
			{
				list.Add(m_slots[i].CardID);
			}
		}
		return list;
	}

	public List<CardWithPremiumStatus> GetCardsWithPremiumStatus()
	{
		List<CardWithPremiumStatus> list = new List<CardWithPremiumStatus>();
		for (int i = 0; i < m_slots.Count; i++)
		{
			long id = GameUtils.TranslateCardIdToDbId(m_slots[i].CardID);
			int count = m_slots[i].GetCount(TAG_PREMIUM.DIAMOND);
			int count2 = m_slots[i].GetCount(TAG_PREMIUM.GOLDEN);
			int count3 = m_slots[i].GetCount(TAG_PREMIUM.NORMAL);
			for (int j = 0; j < count; j++)
			{
				CardWithPremiumStatus item = new CardWithPremiumStatus(id, TAG_PREMIUM.DIAMOND);
				list.Add(item);
			}
			for (int k = 0; k < count2; k++)
			{
				CardWithPremiumStatus item2 = new CardWithPremiumStatus(id, TAG_PREMIUM.GOLDEN);
				list.Add(item2);
			}
			for (int l = 0; l < count3; l++)
			{
				CardWithPremiumStatus item3 = new CardWithPremiumStatus(id, TAG_PREMIUM.NORMAL);
				list.Add(item3);
			}
		}
		return list;
	}

	public void MarkNetworkContentsLoaded()
	{
		m_netContentsLoaded = true;
	}

	public bool NetworkContentsLoaded()
	{
		return m_netContentsLoaded;
	}

	public void MarkBeingDeleted()
	{
		m_isBeingDeleted = true;
	}

	public bool IsBeingDeleted()
	{
		return m_isBeingDeleted;
	}

	public bool IsSavingChanges()
	{
		if (!m_isSavingNameChanges)
		{
			return m_isSavingContentChanges;
		}
		return true;
	}

	public bool IsBeingEdited()
	{
		return this == CollectionManager.Get().GetEditedDeck();
	}

	public bool IsBasicDeck()
	{
		if (FormatType == FormatType.FT_WILD)
		{
			return false;
		}
		if (SourceType != DeckSourceType.DECK_SOURCE_TYPE_BASIC_DECK)
		{
			return false;
		}
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (DefLoader.Get().GetEntityDef(slot.CardID).IsCoreCard())
			{
				return false;
			}
		}
		return true;
	}

	public bool IsInnkeeperDeck()
	{
		return SourceType == DeckSourceType.DECK_SOURCE_TYPE_INNKEEPER_DECK;
	}

	public int GetMaxCardCount()
	{
		if (GetRuleset() != null)
		{
			return GetRuleset().GetDeckSize(this);
		}
		Debug.LogError("GetMaxCardCount() - unable to get correct count, ruleset was unavailable");
		return 0;
	}

	public int GetTotalCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			num += slot.Count;
		}
		return num;
	}

	public int GetTotalOwnedCardCount()
	{
		if (!ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return GetTotalCardCount();
		}
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.Owned)
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetTotalValidCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (IsValidSlot(slot))
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetTotalInvalidCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (!IsValidSlot(slot))
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetTotalUnownedCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (!slot.Owned)
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public List<CollectionDeckSlot> GetSlots()
	{
		return m_slots;
	}

	public int GetSlotCount()
	{
		return m_slots.Count;
	}

	public bool IsValidSlot(CollectionDeckSlot slot, bool ignoreOwnership = false, bool ignoreGameplayEvent = false, bool enforceRemainingDeckRuleset = false)
	{
		if (Locked)
		{
			return true;
		}
		if (FormatType != FormatType.FT_WILD && GameUtils.IsWildCard(slot.CardID))
		{
			return false;
		}
		if (GetRuleset() == null)
		{
			Debug.LogError("IsValidSlot() - Unable to find ruleset");
			return false;
		}
		if (GetRuleset().HasIsPlayableRule() && !ignoreGameplayEvent && !GameUtils.IsCardGameplayEventActive(slot.CardID))
		{
			return false;
		}
		if (DuelsConfig.IsCardLoadoutTreasure(slot.CardID))
		{
			return true;
		}
		if (!ignoreOwnership && !slot.Owned)
		{
			return false;
		}
		EntityDef entityDef = slot.GetEntityDef();
		if (enforceRemainingDeckRuleset && entityDef != null)
		{
			List<DeckRule.RuleType> list = new List<DeckRule.RuleType>();
			if (ignoreOwnership)
			{
				list.Add(DeckRule.RuleType.PLAYER_OWNS_EACH_COPY);
			}
			if (ignoreGameplayEvent)
			{
				list.Add(DeckRule.RuleType.IS_CARD_PLAYABLE);
			}
			if (!GetRuleset().Filter(entityDef, this, (list.Count == 0) ? null : list.ToArray()))
			{
				return false;
			}
		}
		return true;
	}

	public SlotStatus GetSlotStatus(CollectionDeckSlot slot)
	{
		if (slot == null)
		{
			return SlotStatus.UNKNOWN;
		}
		if (ShouldSplitSlotsByOwnershipOrFormatValidity() && !DuelsConfig.IsCardLoadoutTreasure(slot.CardID))
		{
			if (!GameUtils.IsCardCollectible(slot.CardID))
			{
				return SlotStatus.NOT_VALID;
			}
			if (!slot.Owned)
			{
				return SlotStatus.MISSING;
			}
			if (!IsValidSlot(slot, ignoreOwnership: true, ignoreGameplayEvent: false, enforceRemainingDeckRuleset: true))
			{
				return SlotStatus.NOT_VALID;
			}
		}
		return SlotStatus.VALID;
	}

	public bool HasReplaceableSlot()
	{
		for (int i = 0; i < m_slots.Count; i++)
		{
			if (!IsValidSlot(m_slots[i]))
			{
				return true;
			}
		}
		return false;
	}

	public CollectionDeckSlot GetSlotByIndex(int slotIndex)
	{
		if (slotIndex < 0 || slotIndex >= GetSlotCount())
		{
			return null;
		}
		return m_slots[slotIndex];
	}

	public CollectionDeckSlot GetExistingSlot(CollectionDeckSlot searchSlot)
	{
		if (ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return m_slots.Find((CollectionDeckSlot slot) => slot.CardID == searchSlot.CardID && slot.Owned == searchSlot.Owned);
		}
		return m_slots.Find((CollectionDeckSlot slot) => slot.CardID == searchSlot.CardID);
	}

	public DeckRuleset GetRuleset()
	{
		DeckRuleset deckRuleset = null;
		switch (Type)
		{
		case DeckType.PVPDR_DECK:
			deckRuleset = DeckRuleset.GetPVPDRRuleset();
			break;
		case DeckType.PVPDR_DISPLAY_DECK:
			deckRuleset = DeckRuleset.GetPVPDRDisplayRuleset();
			break;
		case DeckType.NORMAL_DECK:
		case DeckType.PRECON_DECK:
			deckRuleset = DeckRuleset.GetRuleset(FormatType);
			break;
		case DeckType.TAVERN_BRAWL_DECK:
		case DeckType.FSG_BRAWL_DECK:
			deckRuleset = TavernBrawlManager.Get().GetCurrentDeckRuleset();
			break;
		}
		if (deckRuleset == null)
		{
			deckRuleset = DeckRuleset.GetRuleset(FormatType.FT_WILD);
		}
		return deckRuleset;
	}

	public bool IsValidForFormat(FormatType formatType)
	{
		switch (formatType)
		{
		case FormatType.FT_CLASSIC:
			if (FormatType == formatType)
			{
				return GameUtils.CLASSIC_ORDERED_HERO_CLASSES.Contains(GetClass());
			}
			return false;
		case FormatType.FT_STANDARD:
			return FormatType == formatType;
		case FormatType.FT_WILD:
			if (FormatType != formatType)
			{
				return FormatType == FormatType.FT_STANDARD;
			}
			return true;
		default:
			return false;
		}
	}

	public bool InsertSlotByDefaultSort(CollectionDeckSlot slot, bool forceUniqueSlot = false)
	{
		return InsertSlot(GetInsertionIdxByDefaultSort(slot), slot, forceUniqueSlot);
	}

	public void CopyFrom(CollectionDeck otherDeck)
	{
		ID = otherDeck.ID;
		Type = otherDeck.Type;
		m_name = otherDeck.m_name;
		HeroCardID = otherDeck.HeroCardID;
		HeroPremium = otherDeck.HeroPremium;
		HeroOverridden = otherDeck.HeroOverridden;
		CardBackID = otherDeck.CardBackID;
		CardBackOverridden = otherDeck.CardBackOverridden;
		NeedsName = otherDeck.NeedsName;
		SeasonId = otherDeck.SeasonId;
		BrawlLibraryItemId = otherDeck.BrawlLibraryItemId;
		FormatType = otherDeck.FormatType;
		SortOrder = otherDeck.SortOrder;
		SourceType = otherDeck.SourceType;
		UIHeroOverrideCardID = otherDeck.UIHeroOverrideCardID;
		UIHeroOverridePremium = otherDeck.UIHeroOverridePremium;
		m_slots.Clear();
		for (int i = 0; i < otherDeck.GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = otherDeck.GetSlotByIndex(i);
			CollectionDeckSlot collectionDeckSlot = new CollectionDeckSlot();
			collectionDeckSlot.CopyFrom(slotByIndex);
			m_slots.Add(collectionDeckSlot);
		}
	}

	public void CopyContents(CollectionDeck otherDeck)
	{
		HeroCardID = otherDeck.HeroCardID;
		HeroPremium = otherDeck.HeroPremium;
		UIHeroOverrideCardID = otherDeck.UIHeroOverrideCardID;
		UIHeroOverridePremium = otherDeck.UIHeroOverridePremium;
		m_slots.Clear();
		for (int i = 0; i < otherDeck.GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = otherDeck.GetSlotByIndex(i);
			foreach (TAG_PREMIUM value in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				for (int j = 0; j < slotByIndex.GetCount(value); j++)
				{
					AddCard(slotByIndex.CardID, value);
				}
			}
		}
	}

	public void FillFromShareableDeck(ShareableDeck shareableDeck)
	{
		HeroCardID = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId);
		FormatType = shareableDeck.FormatType;
		m_slots.Clear();
		for (int i = 0; i < shareableDeck.DeckContents.Cards.Count; i++)
		{
			string cardID = GameUtils.TranslateDbIdToCardId(shareableDeck.DeckContents.Cards[i].Def.Asset);
			TAG_PREMIUM premium = (TAG_PREMIUM)shareableDeck.DeckContents.Cards[i].Def.Premium;
			int qty = shareableDeck.DeckContents.Cards[i].Qty;
			for (int j = 0; j < qty; j++)
			{
				AddCard(cardID, premium, exceedMax: true);
			}
		}
	}

	public void FillFromTemplateDeck(CollectionManager.TemplateDeck tplDeck)
	{
		ClearSlotContents();
		Name = tplDeck.m_title;
		foreach (KeyValuePair<string, int> cardId in tplDeck.m_cardIds)
		{
			CollectionManager.Get().GetOwnedCardCount(cardId.Key, out var _, out var golden, out var diamond);
			int num = cardId.Value;
			while (num > 0 && diamond > 0)
			{
				AddCard(cardId.Key, TAG_PREMIUM.DIAMOND);
				diamond--;
				num--;
			}
			while (num > 0 && golden > 0)
			{
				AddCard(cardId.Key, TAG_PREMIUM.GOLDEN);
				golden--;
				num--;
			}
			while (num > 0)
			{
				AddCard(cardId.Key, TAG_PREMIUM.NORMAL);
				num--;
			}
		}
	}

	public void FillFromCardList(IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		if (fillCards == null)
		{
			return;
		}
		foreach (DeckMaker.DeckFill fillCard in fillCards)
		{
			if (GetTotalCardCount() >= GetMaxCardCount())
			{
				break;
			}
			if (fillCard.m_addCard == null)
			{
				continue;
			}
			TAG_PREMIUM premium = TAG_PREMIUM.DIAMOND;
			if (!CanAddOwnedCard(fillCard.m_addCard.GetCardId(), premium))
			{
				premium = TAG_PREMIUM.GOLDEN;
				if (!CanAddOwnedCard(fillCard.m_addCard.GetCardId(), premium))
				{
					premium = TAG_PREMIUM.NORMAL;
					if (!CanAddOwnedCard(fillCard.m_addCard.GetCardId(), premium))
					{
						continue;
					}
				}
			}
			AddCard(fillCard.m_addCard.GetCardId(), premium);
		}
		SendChanges();
	}

	public void ReconcileUnownedCards()
	{
		List<CollectionDeckSlot> list = new List<CollectionDeckSlot>();
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (!slot.Owned)
			{
				list.Add(slot);
			}
		}
		foreach (CollectionDeckSlot item in list)
		{
			int num = item.Count;
			RemoveSlot(item);
			while (num > 0)
			{
				num--;
				if (CanAddOwnedCard(item.CardID, TAG_PREMIUM.DIAMOND))
				{
					AddCard(item.CardID, TAG_PREMIUM.DIAMOND);
				}
				else if (CanAddOwnedCard(item.CardID, TAG_PREMIUM.GOLDEN))
				{
					AddCard(item.CardID, TAG_PREMIUM.GOLDEN);
				}
				else
				{
					AddCard(item.CardID, TAG_PREMIUM.NORMAL);
				}
			}
		}
	}

	public void RemoveInvalidCards()
	{
		foreach (CollectionDeckSlot item in GetSlots().FindAll((CollectionDeckSlot slot) => !IsValidSlot(slot)))
		{
			RemoveSlot(item);
		}
	}

	public int GetUnownedCardIdCount(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID) && !slot.Owned)
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetInvalidCardIdCount(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID) && !IsValidSlot(slot))
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetCardIdCount(string cardID, bool includeUnowned = true)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID) && (includeUnowned || slot.Owned))
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetCardCountMatchingTag(GAME_TAG tagName, int tagValue)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (GameUtils.GetCardTagValue(slot.CardID, tagName) == tagValue)
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetCardCountFirstMatchingSlot(string cardID, TAG_PREMIUM premium)
	{
		return FindFirstSlotByCardId(cardID)?.GetCount(premium) ?? 0;
	}

	public int GetCardCountAllMatchingSlots(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID))
			{
				num += slot.Count;
			}
		}
		return num;
	}

	public int GetCardCountAllMatchingSlots(string cardID, TAG_PREMIUM premium)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID))
			{
				num += slot.GetCount(premium);
			}
		}
		return num;
	}

	public int GetValidCardCount(string cardID, TAG_PREMIUM premium, bool valid = true)
	{
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID) && IsValidSlot(slot) == valid)
			{
				num += slot.GetCount(premium);
			}
		}
		return num;
	}

	public int GetOwnedCardCountInDeck(string cardID, TAG_PREMIUM premium, bool owned = true)
	{
		if (!ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return GetCardCountAllMatchingSlots(cardID);
		}
		int num = 0;
		foreach (CollectionDeckSlot slot in m_slots)
		{
			if (slot.CardID.Equals(cardID) && slot.Owned == owned)
			{
				num += slot.GetCount(premium);
			}
		}
		return num;
	}

	public int GetCardCountInSet(HashSet<string> set, bool isNot)
	{
		int num = 0;
		for (int i = 0; i < m_slots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = m_slots[i];
			if (set.Contains(collectionDeckSlot.CardID) == !isNot)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	public void ClearSlotContents()
	{
		m_slots.Clear();
	}

	public bool CanAddOwnedCard(string cardID, TAG_PREMIUM premium)
	{
		int ownedCardCountInDeck = GetOwnedCardCountInDeck(cardID, premium);
		return CollectionManager.Get().GetOwnedCount(cardID, premium) > ownedCardCountInDeck;
	}

	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, bool exceedMax = false)
	{
		return AddCard(cardEntityDef.GetCardId(), premium, exceedMax);
	}

	public bool AddCard(string cardID, TAG_PREMIUM premium, bool exceedMax = false)
	{
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		if (deckRuleset != null)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
			if (deckRuleset.EntityIgnoresRuleset(entityDef) || deckRuleset.EntityInDeckIgnoresRuleset(this))
			{
				AddCard_IgnoreDeckRules(cardID, premium, 1);
				return true;
			}
		}
		if (!exceedMax && !CanInsertCard(cardID, premium))
		{
			return false;
		}
		bool flag = ShouldSplitSlotsByOwnershipOrFormatValidity() && CanAddOwnedCard(cardID, premium);
		bool forceUniqueSlot = false;
		bool flag2 = false;
		CollectionDeckSlot collectionDeckSlot = null;
		if (!m_forceDuplicatesIntoSeparateSlots)
		{
			collectionDeckSlot = (flag ? FindFirstValidSlotByCardId(cardID, valid: true, ignoreEvent: true) : FindFirstOwnedSlotByCardId(cardID, flag));
			forceUniqueSlot = collectionDeckSlot != null && collectionDeckSlot.m_entityDefOverride != null;
			if (collectionDeckSlot != null && collectionDeckSlot.m_entityDefOverride == null)
			{
				collectionDeckSlot.AddCard(1, premium);
				flag2 = true;
			}
		}
		if (!flag2)
		{
			collectionDeckSlot = new CollectionDeckSlot
			{
				CardID = cardID,
				Owned = flag
			};
			collectionDeckSlot.SetCount(1, premium);
			if (!InsertSlotByDefaultSort(collectionDeckSlot, forceUniqueSlot))
			{
				return false;
			}
		}
		return true;
	}

	public void OnCardInsertedToCollection(string cardID, TAG_PREMIUM premium)
	{
		if (Type == DeckType.NORMAL_DECK && CollectionManager.Get().GetEditedDeck() != this && GetCardIdCount(cardID) > 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
			entityDef.GetRarity();
			int num = ((entityDef.GetRarity() == TAG_RARITY.LEGENDARY) ? 1 : 2);
			int ownedCardCountInDeck = GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.NORMAL);
			int ownedCardCountInDeck2 = GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.GOLDEN);
			int ownedCardCountInDeck3 = GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.DIAMOND);
			int num2 = ownedCardCountInDeck + ownedCardCountInDeck2 + ownedCardCountInDeck3;
			if (num2 < GetCardIdCount(cardID) && num2 < num)
			{
				AddCard(cardID, premium);
			}
		}
	}

	public bool AddCard_DungeonCrawlBuff(string cardId, TAG_PREMIUM premium, List<int> enchantments)
	{
		CollectionDeckSlot collectionDeckSlot = new CollectionDeckSlot
		{
			CardID = cardId,
			Owned = true
		};
		collectionDeckSlot.CreateDynamicEntity();
		foreach (int enchantment in enchantments)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(enchantment);
			int tag = entityDef.GetTag(GAME_TAG.UI_BUFF_ATK_UP);
			if (tag != 0)
			{
				int tag2 = collectionDeckSlot.m_entityDefOverride.GetTag(GAME_TAG.ATK);
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.ATK, tag2 + tag);
			}
			int tag3 = entityDef.GetTag(GAME_TAG.UI_BUFF_HEALTH_UP);
			if (tag3 != 0)
			{
				int tag4 = collectionDeckSlot.m_entityDefOverride.GetTag(GAME_TAG.HEALTH);
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.HEALTH, tag4 + tag3);
			}
			int tag5 = entityDef.GetTag(GAME_TAG.UI_BUFF_DURABILITY_UP);
			if (tag5 != 0)
			{
				int tag6 = collectionDeckSlot.m_entityDefOverride.GetTag(GAME_TAG.DURABILITY);
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.DURABILITY, tag6 + tag5);
			}
			int tag7 = entityDef.GetTag(GAME_TAG.UI_BUFF_COST_UP);
			if (tag7 != 0)
			{
				int tag8 = collectionDeckSlot.m_entityDefOverride.GetTag(GAME_TAG.COST);
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.COST, tag8 + tag7);
			}
			int tag9 = entityDef.GetTag(GAME_TAG.UI_BUFF_COST_DOWN);
			if (tag9 != 0)
			{
				int tag10 = collectionDeckSlot.m_entityDefOverride.GetTag(GAME_TAG.COST);
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.COST, Math.Max(tag10 - tag9, 0));
			}
			if (entityDef.GetTag(GAME_TAG.UI_BUFF_SET_COST_ZERO) != 0)
			{
				collectionDeckSlot.m_entityDefOverride.SetTag(GAME_TAG.COST, 0);
			}
		}
		collectionDeckSlot.SetCount(1, premium);
		if (!InsertSlotByDefaultSort(collectionDeckSlot, forceUniqueSlot: true))
		{
			return false;
		}
		return true;
	}

	public void AddCard_IgnoreDeckRules(string cardID, TAG_PREMIUM premium, int countToAdd)
	{
		if (countToAdd <= 0)
		{
			return;
		}
		int num = countToAdd;
		if (ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			int ownedCount = CollectionManager.Get().GetOwnedCount(cardID, premium);
			if (ownedCount > 0)
			{
				CollectionDeckSlot collectionDeckSlot = FindFirstOwnedSlotByCardId(cardID, owned: true);
				if (collectionDeckSlot == null || m_forceDuplicatesIntoSeparateSlots)
				{
					collectionDeckSlot = new CollectionDeckSlot
					{
						CardID = cardID,
						Owned = true
					};
					collectionDeckSlot.SetCount(0, premium);
					InsertSlotByDefaultSort(collectionDeckSlot);
				}
				int count = collectionDeckSlot.GetCount(premium);
				int num2 = Mathf.Min(count + num, ownedCount);
				int num3 = num2 - count;
				collectionDeckSlot.SetCount(num2, premium);
				num -= num3;
			}
		}
		if (num > 0)
		{
			CollectionDeckSlot collectionDeckSlot2 = FindFirstOwnedSlotByCardId(cardID, owned: false);
			if (collectionDeckSlot2 == null || m_forceDuplicatesIntoSeparateSlots)
			{
				collectionDeckSlot2 = new CollectionDeckSlot
				{
					CardID = cardID,
					Owned = false
				};
				collectionDeckSlot2.SetCount(num, premium);
				InsertSlotByDefaultSort(collectionDeckSlot2);
			}
			else
			{
				collectionDeckSlot2.AddCard(num, premium);
			}
			num = 0;
		}
	}

	public bool RemoveCard(string cardID, TAG_PREMIUM premium, bool valid = true)
	{
		CollectionDeckSlot collectionDeckSlot = FindFirstValidSlotByCardId(cardID, valid);
		if (collectionDeckSlot == null)
		{
			return false;
		}
		collectionDeckSlot.RemoveCard(1, premium);
		UpdateUIHeroOverrideCardRemoval(cardID);
		return true;
	}

	public void RemoveAllCards()
	{
		m_slots = new List<CollectionDeckSlot>();
	}

	private void UpdateUIHeroOverrideCardRemoval(string cardID)
	{
		if (GameDbf.GetIndex().HasCardPlayerDeckOverride(cardID) && (CollectionDeckTray.Get() == null || !CollectionDeckTray.Get().IsShowingDeckContents()))
		{
			UIHeroOverrideCardID = string.Empty;
			UIHeroOverridePremium = TAG_PREMIUM.NORMAL;
			Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", GameStrings.GetClassName(GetClass()));
			CollectionManager.Get().OnUIHeroOverrideCardRemoved();
		}
	}

	public void OnContentChangesComplete()
	{
		m_isSavingContentChanges = false;
	}

	public void OnNameChangeComplete()
	{
		m_isSavingNameChanges = false;
	}

	public void SendChanges()
	{
		CollectionDeck baseDeck = CollectionManager.Get().GetBaseDeck(ID);
		if (this == baseDeck)
		{
			Debug.LogError($"CollectionDeck.Send() - {baseDeck} is a base deck. You cannot send a base deck to the network.");
			return;
		}
		if (baseDeck == null)
		{
			Log.CollectionManager.PrintError("CollectionDeck.SendChanges() - No base deck with id=" + ID);
			return;
		}
		GenerateNameDiff(baseDeck, out var deckName);
		List<Network.CardUserData> list = GenerateContentChanges(baseDeck);
		int heroAssetID;
		TAG_PREMIUM heroCardPremium;
		int overrideHeroAssetID;
		TAG_PREMIUM overrideHeroPremium;
		bool flag = GenerateHeroDiff(baseDeck, out heroAssetID, out heroCardPremium, out overrideHeroAssetID, out overrideHeroPremium);
		int cardBackID;
		bool flag2 = GenerateCardBackDiff(baseDeck, out cardBackID);
		bool flag3 = baseDeck.FormatType != FormatType;
		bool flag4 = baseDeck.SortOrder != SortOrder;
		Network network = Network.Get();
		if (deckName != null)
		{
			m_isSavingNameChanges = true;
			network.RenameDeck(ID, deckName);
		}
		string pastedDeckHash = null;
		if (m_createdFromShareableDeck != null)
		{
			pastedDeckHash = m_createdFromShareableDeck.Serialize(includeComments: false);
		}
		if (list.Count > 0 || flag || flag2 || flag3 || flag4)
		{
			m_isSavingContentChanges = true;
			Network.Get().SendDeckData(ID, list, heroAssetID, heroCardPremium, overrideHeroAssetID, overrideHeroPremium, cardBackID, FormatType, SortOrder, pastedDeckHash);
		}
		if (!Network.IsLoggedIn())
		{
			OnContentChangesComplete();
			OnNameChangeComplete();
		}
	}

	public static string GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(DeckRuleViolation violation)
	{
		if (violation == null || violation.Rule == null)
		{
			return string.Empty;
		}
		switch (violation.Rule.Type)
		{
		case DeckRule.RuleType.PLAYER_OWNS_EACH_COPY:
		case DeckRule.RuleType.DECK_SIZE:
			return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_INCOMPLETE");
		case DeckRule.RuleType.IS_NOT_ROTATED:
			return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_FORMAT");
		case DeckRule.RuleType.IS_CARD_PLAYABLE:
			return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_UNPLAYABLE");
		default:
			return violation.DisplayError;
		}
	}

	public void SetShareableDeckCreatedFrom(ShareableDeck shareableDeck)
	{
		m_createdFromShareableDeck = shareableDeck;
	}

	public bool CanInsertCard(string cardID, TAG_PREMIUM premium)
	{
		if (DeckType.DRAFT_DECK == Type || DeckType.CLIENT_ONLY_DECK == Type)
		{
			return true;
		}
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		if (deckRuleset == null)
		{
			return true;
		}
		if (Type == DeckType.PVPDR_DISPLAY_DECK && DuelsConfig.IsCardLoadoutTreasure(cardID))
		{
			return true;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		RuleInvalidReason reason;
		DeckRule brokenRule;
		return deckRuleset.CanAddToDeck(entityDef, premium, this, out reason, out brokenRule, DeckRule.RuleType.PLAYER_OWNS_EACH_COPY, DeckRule.RuleType.IS_CARD_PLAYABLE);
	}

	private bool InsertSlot(int slotIndex, CollectionDeckSlot slot, bool forceUniqueSlot = false)
	{
		if (slotIndex < 0 || slotIndex > GetSlotCount())
		{
			Log.Decks.Print($"CollectionDeck.InsertSlot(): inserting slot {slot} failed; invalid slot index {slotIndex}.");
			return false;
		}
		if (!m_forceDuplicatesIntoSeparateSlots && !forceUniqueSlot)
		{
			CollectionDeckSlot existingSlot = GetExistingSlot(slot);
			if (existingSlot != null)
			{
				Debug.LogWarningFormat("CollectionDeck.InsertSlot: slot with cardId={0} already exists in deckId={1} cardDbId={3} bestPremium={4} owned={5} existingCount={6} slotIndex={7}", slot.CardID, ID, slot.CardID, GameUtils.TranslateCardIdToDbId(slot.CardID), slot.PreferredPremium, slot.Owned, existingSlot.Count, slotIndex);
				return false;
			}
		}
		slot.OnSlotEmptied = (CollectionDeckSlot.DelOnSlotEmptied)Delegate.Combine(slot.OnSlotEmptied, new CollectionDeckSlot.DelOnSlotEmptied(OnSlotEmptied));
		slot.Index = slotIndex;
		m_slots.Insert(slotIndex, slot);
		UpdateSlotIndices(slotIndex, GetSlotCount() - 1);
		return true;
	}

	public void ForceRemoveSlot(CollectionDeckSlot slot)
	{
		RemoveSlot(slot);
	}

	private void RemoveSlot(CollectionDeckSlot slot)
	{
		slot.OnSlotEmptied = (CollectionDeckSlot.DelOnSlotEmptied)Delegate.Remove(slot.OnSlotEmptied, new CollectionDeckSlot.DelOnSlotEmptied(OnSlotEmptied));
		int index = slot.Index;
		m_slots.RemoveAt(index);
		slot.m_entityDefOverride = null;
		UpdateSlotIndices(index, GetSlotCount() - 1);
		UpdateUIHeroOverrideCardRemoval(slot.CardID);
	}

	private void OnSlotEmptied(CollectionDeckSlot slot)
	{
		if (GetExistingSlot(slot) == null)
		{
			Log.Decks.Print($"CollectionDeck.OnSlotCountUpdated(): Trying to remove slot {slot}, but it does not exist in deck {this}");
		}
		else
		{
			RemoveSlot(slot);
		}
	}

	private void UpdateSlotIndices(int indexA, int indexB)
	{
		if (GetSlotCount() != 0)
		{
			int val;
			int val2;
			if (indexA < indexB)
			{
				val = indexA;
				val2 = indexB;
			}
			else
			{
				val = indexB;
				val2 = indexA;
			}
			val = Math.Max(0, val);
			val2 = Math.Min(val2, GetSlotCount() - 1);
			for (int i = val; i <= val2; i++)
			{
				GetSlotByIndex(i).Index = i;
			}
		}
	}

	public CollectionDeckSlot FindFirstSlotByCardId(string cardID)
	{
		return m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID));
	}

	public CollectionDeckSlot FindFirstOwnedSlotByCardId(string cardID, bool owned)
	{
		if (!ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return FindFirstSlotByCardId(cardID);
		}
		return m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID) && slot.Owned == owned);
	}

	private CollectionDeckSlot FindFirstValidSlotByCardId(string cardID, bool valid, bool ignoreEvent = false)
	{
		if (!ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			Log.Decks.PrintWarning("Your deck doesn't care about Validity.  Why are you using 'FindFirstValidSlot' as opposed to 'FindFirstOwnedSlot'? This may be a bug!");
			return FindFirstSlotByCardId(cardID);
		}
		return m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID) && IsValidSlot(slot, ignoreOwnership: false, ignoreEvent) == valid);
	}

	private void GenerateNameDiff(CollectionDeck baseDeck, out string deckName)
	{
		deckName = null;
		if (!Name.Equals(baseDeck.Name))
		{
			deckName = Name;
		}
	}

	private bool GenerateHeroDiff(CollectionDeck baseDeck, out int heroAssetID, out TAG_PREMIUM heroCardPremium, out int overrideHeroAssetID, out TAG_PREMIUM overrideHeroPremium)
	{
		heroAssetID = -1;
		heroCardPremium = TAG_PREMIUM.NORMAL;
		overrideHeroAssetID = -1;
		overrideHeroPremium = TAG_PREMIUM.NORMAL;
		bool result = false;
		bool flag = HeroCardID == baseDeck.HeroCardID && HeroPremium == baseDeck.HeroPremium;
		if (HeroOverridden && (!baseDeck.HeroOverridden || !flag))
		{
			heroAssetID = GameUtils.TranslateCardIdToDbId(HeroCardID);
			heroCardPremium = HeroPremium;
			result = true;
		}
		if (!(UIHeroOverrideCardID == baseDeck.UIHeroOverrideCardID) || UIHeroOverridePremium != baseDeck.UIHeroOverridePremium)
		{
			overrideHeroAssetID = ((!string.IsNullOrEmpty(UIHeroOverrideCardID)) ? GameUtils.TranslateCardIdToDbId(UIHeroOverrideCardID) : 0);
			overrideHeroPremium = UIHeroOverridePremium;
			result = true;
		}
		return result;
	}

	private bool GenerateCardBackDiff(CollectionDeck baseDeck, out int cardBackID)
	{
		cardBackID = -1;
		if (!CardBackOverridden)
		{
			return false;
		}
		bool flag = CardBackID == baseDeck.CardBackID;
		if (baseDeck.CardBackOverridden && flag)
		{
			return false;
		}
		cardBackID = CardBackID;
		return true;
	}

	private List<Network.CardUserData> CardUserDataFromSlot(CollectionDeckSlot deckSlot, bool deleted)
	{
		List<Network.CardUserData> list = new List<Network.CardUserData>();
		Network.CardUserData cardUserData = new Network.CardUserData
		{
			DbId = GameUtils.TranslateCardIdToDbId(deckSlot.CardID),
			Count = ((!deleted) ? deckSlot.GetCount(TAG_PREMIUM.NORMAL) : 0),
			Premium = TAG_PREMIUM.NORMAL
		};
		Network.CardUserData item = new Network.CardUserData
		{
			DbId = cardUserData.DbId,
			Count = ((!deleted) ? deckSlot.GetCount(TAG_PREMIUM.GOLDEN) : 0),
			Premium = TAG_PREMIUM.GOLDEN
		};
		Network.CardUserData item2 = new Network.CardUserData
		{
			DbId = cardUserData.DbId,
			Count = ((!deleted) ? deckSlot.GetCount(TAG_PREMIUM.DIAMOND) : 0),
			Premium = TAG_PREMIUM.DIAMOND
		};
		list.Add(cardUserData);
		list.Add(item);
		list.Add(item2);
		return list;
	}

	private List<Network.CardUserData> GenerateContentChanges(CollectionDeck baseDeck)
	{
		SortedDictionary<string, CollectionDeckSlot> sortedDictionary = new SortedDictionary<string, CollectionDeckSlot>();
		foreach (CollectionDeckSlot slot in baseDeck.GetSlots())
		{
			CollectionDeckSlot value = null;
			if (sortedDictionary.TryGetValue(slot.CardID, out value))
			{
				foreach (TAG_PREMIUM value7 in Enum.GetValues(typeof(TAG_PREMIUM)))
				{
					value.AddCard(slot.GetCount(value7), value7);
				}
			}
			else
			{
				value = new CollectionDeckSlot();
				value.CopyFrom(slot);
				sortedDictionary.Add(value.CardID, value);
			}
		}
		SortedDictionary<string, CollectionDeckSlot> sortedDictionary2 = new SortedDictionary<string, CollectionDeckSlot>();
		foreach (CollectionDeckSlot slot2 in GetSlots())
		{
			CollectionDeckSlot value2 = null;
			if (sortedDictionary2.TryGetValue(slot2.CardID, out value2))
			{
				foreach (TAG_PREMIUM value8 in Enum.GetValues(typeof(TAG_PREMIUM)))
				{
					value2.AddCard(slot2.GetCount(value8), value8);
				}
			}
			else
			{
				value2 = new CollectionDeckSlot();
				value2.CopyFrom(slot2);
				sortedDictionary2.Add(value2.CardID, value2);
			}
		}
		SortedDictionary<string, CollectionDeckSlot>.Enumerator enumerator3 = sortedDictionary.GetEnumerator();
		SortedDictionary<string, CollectionDeckSlot>.Enumerator enumerator4 = sortedDictionary2.GetEnumerator();
		List<Network.CardUserData> list = new List<Network.CardUserData>();
		bool flag = enumerator3.MoveNext();
		bool flag2 = enumerator4.MoveNext();
		while (flag && flag2)
		{
			CollectionDeckSlot value3 = enumerator3.Current.Value;
			CollectionDeckSlot value4 = enumerator4.Current.Value;
			if (value3.CardID == value4.CardID)
			{
				if (value3.GetCount(TAG_PREMIUM.NORMAL) != value4.GetCount(TAG_PREMIUM.NORMAL) || value3.GetCount(TAG_PREMIUM.GOLDEN) != value4.GetCount(TAG_PREMIUM.GOLDEN) || value3.GetCount(TAG_PREMIUM.DIAMOND) != value4.GetCount(TAG_PREMIUM.DIAMOND))
				{
					list.AddRange(CardUserDataFromSlot(value4, value4.Count == 0));
				}
				flag = enumerator3.MoveNext();
				flag2 = enumerator4.MoveNext();
			}
			else if (value3.CardID.CompareTo(value4.CardID) < 0)
			{
				list.AddRange(CardUserDataFromSlot(value3, deleted: true));
				flag = enumerator3.MoveNext();
			}
			else
			{
				list.AddRange(CardUserDataFromSlot(value4, deleted: false));
				flag2 = enumerator4.MoveNext();
			}
		}
		while (flag)
		{
			CollectionDeckSlot value5 = enumerator3.Current.Value;
			list.AddRange(CardUserDataFromSlot(value5, deleted: true));
			flag = enumerator3.MoveNext();
		}
		while (flag2)
		{
			CollectionDeckSlot value6 = enumerator4.Current.Value;
			list.AddRange(CardUserDataFromSlot(value6, deleted: false));
			flag2 = enumerator4.MoveNext();
		}
		return list;
	}

	private int GetInsertionIdxByDefaultSort(CollectionDeckSlot slot)
	{
		EntityDef entityDef = slot.GetEntityDef();
		if (entityDef == null)
		{
			Log.Decks.Print($"CollectionDeck.GetInsertionIdxByDefaultSort(): could not get entity def for {slot.CardID}");
			return -1;
		}
		int i;
		for (i = 0; i < GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = GetSlotByIndex(i);
			EntityDef entityDef2 = slotByIndex.GetEntityDef();
			if (entityDef2 == null)
			{
				Log.Decks.Print($"CollectionDeck.GetInsertionIdxByDefaultSort(): entityDef is null at slot index {i}");
				break;
			}
			int num = CollectionManager.Get().EntityDefSortComparison(entityDef, entityDef2);
			if (num < 0 || (num <= 0 && (!ShouldSplitSlotsByOwnershipOrFormatValidity() || slot.Owned == slotByIndex.Owned)))
			{
				break;
			}
		}
		return i;
	}

	public TAG_CLASS GetClass()
	{
		return DefLoader.Get().GetEntityDef(HeroCardID)?.GetClass() ?? TAG_CLASS.INVALID;
	}

	public ShareableDeck GetShareableDeck()
	{
		DeckContents deckContents = GetDeckContents();
		int heroCardDbId = GameUtils.TranslateCardIdToDbId(HeroCardID);
		return new ShareableDeck(Name, heroCardDbId, deckContents, FormatType, Type == DeckType.DRAFT_DECK);
	}

	public bool CanCopyAsShareableDeck(out DeckRuleViolation topViolation)
	{
		topViolation = null;
		if (GetRuleset() != null)
		{
			if (!GetRuleset().IsDeckValid(this, out var violations) && violations != null && violations.Count > 0)
			{
				topViolation = violations[0];
				return false;
			}
			return true;
		}
		return false;
	}

	public void LogDeckStringInformation()
	{
		Log.Decks.PrintInfo(string.Format("{0} {1}", "###", Name));
		Log.Decks.PrintInfo(string.Format("{0}Deck ID: {1}", "# ", ID));
		Log.Decks.PrintInfo(GetShareableDeck().Serialize(includeComments: false));
	}

	public List<DeckMaker.DeckFill> GetDeckFillFromString(string deckString)
	{
		List<DeckMaker.DeckFill> list = new List<DeckMaker.DeckFill>();
		string[] array = deckString.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim();
			if (text.StartsWith("#"))
			{
				continue;
			}
			try
			{
				string[] array2 = text.Split(';');
				foreach (string text2 in array2)
				{
					try
					{
						string[] array3 = text2.Split(',');
						if (!int.TryParse(array3[0], out var result) || result < 0 || result > 10)
						{
							continue;
						}
						string cardId = array3[1];
						EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
						if (entityDef != null)
						{
							for (int k = 0; k < result; k++)
							{
								list.Add(new DeckMaker.DeckFill
								{
									m_addCard = entityDef
								});
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}
		return list;
	}

	public DeckContents GetDeckContents()
	{
		DeckContents deckContents = new DeckContents
		{
			DeckId = ID
		};
		foreach (CollectionDeckSlot slot in m_slots)
		{
			DeckCardData item = new DeckCardData
			{
				Def = new PegasusShared.CardDef
				{
					Asset = GameUtils.TranslateCardIdToDbId(slot.CardID),
					Premium = (int)slot.PreferredPremium
				},
				Qty = slot.Count
			};
			deckContents.Cards.Add(item);
		}
		return deckContents;
	}

	public bool ShouldSplitSlotsByOwnershipOrFormatValidity()
	{
		if (Locked)
		{
			return false;
		}
		switch (Type)
		{
		case DeckType.CLIENT_ONLY_DECK:
		case DeckType.DRAFT_DECK:
			return false;
		case DeckType.TAVERN_BRAWL_DECK:
		case DeckType.FSG_BRAWL_DECK:
			if (TavernBrawlManager.Get().IsCurrentBrawlTypeActive && TavernBrawlManager.Get().GetCurrentDeckRuleset() != null && TavernBrawlManager.Get().GetCurrentDeckRuleset().HasOwnershipOrRotatedRule())
			{
				return true;
			}
			return false;
		default:
			return true;
		}
	}
}
