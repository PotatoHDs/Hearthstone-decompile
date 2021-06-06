using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000102 RID: 258
public class CollectionDeck
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x00053090 File Offset: 0x00051290
	public override string ToString()
	{
		return string.Format("Deck [id={0} name=\"{1}\" heroCardId={2} heroCardFlair={3} cardBackId={4} cardBackOverridden={5} heroOverridden={6} slotCount={7} needsName={8} sortOrder={9}]", new object[]
		{
			this.ID,
			this.Name,
			this.HeroCardID,
			this.HeroPremium,
			this.CardBackID,
			this.CardBackOverridden,
			this.HeroOverridden,
			this.GetSlotCount(),
			this.NeedsName,
			this.SortOrder
		});
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00053131 File Offset: 0x00051331
	// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x00053139 File Offset: 0x00051339
	public string Name
	{
		get
		{
			return this.m_name;
		}
		set
		{
			if (value == null)
			{
				Debug.LogError(string.Format("CollectionDeck.SetName() - null name given for deck {0}", this));
				return;
			}
			if (value.Equals(this.m_name, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}
			this.m_name = value;
		}
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x00053166 File Offset: 0x00051366
	public bool HasUIHeroOverride()
	{
		return !string.IsNullOrEmpty(this.UIHeroOverrideCardID);
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x00053176 File Offset: 0x00051376
	public string GetDisplayHeroCardID()
	{
		if (this.HasUIHeroOverride())
		{
			return this.UIHeroOverrideCardID;
		}
		return this.HeroCardID;
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x00053190 File Offset: 0x00051390
	public TAG_PREMIUM? GetDisplayHeroPremiumOverride()
	{
		if (this.HasUIHeroOverride())
		{
			return new TAG_PREMIUM?(this.UIHeroOverridePremium);
		}
		return null;
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x000531BC File Offset: 0x000513BC
	public List<long> GetCards()
	{
		List<long> list = new List<long>();
		for (int i = 0; i < this.m_slots.Count; i++)
		{
			long item = (long)GameUtils.TranslateCardIdToDbId(this.m_slots[i].CardID, false);
			for (int j = 0; j < this.m_slots[i].Count; j++)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x00053224 File Offset: 0x00051424
	public List<string> GetCardsWithCardID()
	{
		List<string> list = new List<string>();
		for (int i = 0; i < this.m_slots.Count; i++)
		{
			for (int j = 0; j < this.m_slots[i].Count; j++)
			{
				list.Add(this.m_slots[i].CardID);
			}
		}
		return list;
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x00053284 File Offset: 0x00051484
	public List<CardWithPremiumStatus> GetCardsWithPremiumStatus()
	{
		List<CardWithPremiumStatus> list = new List<CardWithPremiumStatus>();
		for (int i = 0; i < this.m_slots.Count; i++)
		{
			long id = (long)GameUtils.TranslateCardIdToDbId(this.m_slots[i].CardID, false);
			int count = this.m_slots[i].GetCount(TAG_PREMIUM.DIAMOND);
			int count2 = this.m_slots[i].GetCount(TAG_PREMIUM.GOLDEN);
			int count3 = this.m_slots[i].GetCount(TAG_PREMIUM.NORMAL);
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

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0005336D File Offset: 0x0005156D
	// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00053375 File Offset: 0x00051575
	public FormatType FormatType { get; set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0005337E File Offset: 0x0005157E
	// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00053386 File Offset: 0x00051586
	public bool IsShared { get; set; }

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0005338F File Offset: 0x0005158F
	// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00053397 File Offset: 0x00051597
	public bool IsCreatedWithDeckComplete { get; set; }

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000533A0 File Offset: 0x000515A0
	public bool IsBrawlDeck
	{
		get
		{
			return TavernBrawlManager.IsBrawlDeckType(this.Type);
		}
	}

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x000533AD File Offset: 0x000515AD
	public bool IsDuelsDeck
	{
		get
		{
			return this.Type == DeckType.PVPDR_DECK || this.Type == DeckType.PVPDR_DISPLAY_DECK;
		}
	}

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000533C4 File Offset: 0x000515C4
	public bool IsValidForRuleset
	{
		get
		{
			if (this.IsShared)
			{
				return true;
			}
			if (!this.m_netContentsLoaded && this.Type != DeckType.CLIENT_ONLY_DECK && this.Type != DeckType.PVPDR_DISPLAY_DECK)
			{
				return false;
			}
			DeckRuleset ruleset = this.GetRuleset();
			return ruleset != null && ruleset.IsDeckValid(this);
		}
	}

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0005340B File Offset: 0x0005160B
	// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00053413 File Offset: 0x00051613
	public bool ForceDuplicatesIntoSeparateSlots
	{
		get
		{
			return this.m_forceDuplicatesIntoSeparateSlots;
		}
		set
		{
			this.m_forceDuplicatesIntoSeparateSlots = value;
		}
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0005341C File Offset: 0x0005161C
	public void MarkNetworkContentsLoaded()
	{
		this.m_netContentsLoaded = true;
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x00053425 File Offset: 0x00051625
	public bool NetworkContentsLoaded()
	{
		return this.m_netContentsLoaded;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0005342D File Offset: 0x0005162D
	public void MarkBeingDeleted()
	{
		this.m_isBeingDeleted = true;
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x00053436 File Offset: 0x00051636
	public bool IsBeingDeleted()
	{
		return this.m_isBeingDeleted;
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0005343E File Offset: 0x0005163E
	public bool IsSavingChanges()
	{
		return this.m_isSavingNameChanges || this.m_isSavingContentChanges;
	}

	// Token: 0x06000EE0 RID: 3808 RVA: 0x00053450 File Offset: 0x00051650
	public bool IsBeingEdited()
	{
		return this == CollectionManager.Get().GetEditedDeck();
	}

	// Token: 0x06000EE1 RID: 3809 RVA: 0x00053460 File Offset: 0x00051660
	public bool IsBasicDeck()
	{
		if (this.FormatType == FormatType.FT_WILD)
		{
			return false;
		}
		if (this.SourceType != DeckSourceType.DECK_SOURCE_TYPE_BASIC_DECK)
		{
			return false;
		}
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID).IsCoreCard())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x000534E0 File Offset: 0x000516E0
	public bool IsInnkeeperDeck()
	{
		return this.SourceType == DeckSourceType.DECK_SOURCE_TYPE_INNKEEPER_DECK;
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x000534EB File Offset: 0x000516EB
	public ShareableDeck CreatedFromShareableDeck
	{
		get
		{
			return this.m_createdFromShareableDeck;
		}
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x000534F3 File Offset: 0x000516F3
	public int GetMaxCardCount()
	{
		if (this.GetRuleset() != null)
		{
			return this.GetRuleset().GetDeckSize(this);
		}
		Debug.LogError("GetMaxCardCount() - unable to get correct count, ruleset was unavailable");
		return 0;
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x00053518 File Offset: 0x00051718
	public int GetTotalCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			num += collectionDeckSlot.Count;
		}
		return num;
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x00053570 File Offset: 0x00051770
	public int GetTotalOwnedCardCount()
	{
		if (!this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return this.GetTotalCardCount();
		}
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.Owned)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x000535E0 File Offset: 0x000517E0
	public int GetTotalValidCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (this.IsValidSlot(collectionDeckSlot, false, false, false))
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00053644 File Offset: 0x00051844
	public int GetTotalInvalidCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (!this.IsValidSlot(collectionDeckSlot, false, false, false))
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x000536A8 File Offset: 0x000518A8
	public int GetTotalUnownedCardCount()
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (!collectionDeckSlot.Owned)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00053708 File Offset: 0x00051908
	public List<CollectionDeckSlot> GetSlots()
	{
		return this.m_slots;
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x00053710 File Offset: 0x00051910
	public int GetSlotCount()
	{
		return this.m_slots.Count;
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x00053720 File Offset: 0x00051920
	public bool IsValidSlot(CollectionDeckSlot slot, bool ignoreOwnership = false, bool ignoreGameplayEvent = false, bool enforceRemainingDeckRuleset = false)
	{
		if (this.Locked)
		{
			return true;
		}
		if (this.FormatType != FormatType.FT_WILD && GameUtils.IsWildCard(slot.CardID))
		{
			return false;
		}
		if (this.GetRuleset() == null)
		{
			Debug.LogError("IsValidSlot() - Unable to find ruleset");
			return false;
		}
		if (this.GetRuleset().HasIsPlayableRule() && !ignoreGameplayEvent && !GameUtils.IsCardGameplayEventActive(slot.CardID))
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
			if (!this.GetRuleset().Filter(entityDef, this, (list.Count == 0) ? null : list.ToArray()))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x000537EC File Offset: 0x000519EC
	public CollectionDeck.SlotStatus GetSlotStatus(CollectionDeckSlot slot)
	{
		if (slot == null)
		{
			return CollectionDeck.SlotStatus.UNKNOWN;
		}
		if (this.ShouldSplitSlotsByOwnershipOrFormatValidity() && !DuelsConfig.IsCardLoadoutTreasure(slot.CardID))
		{
			if (!GameUtils.IsCardCollectible(slot.CardID))
			{
				return CollectionDeck.SlotStatus.NOT_VALID;
			}
			if (!slot.Owned)
			{
				return CollectionDeck.SlotStatus.MISSING;
			}
			if (!this.IsValidSlot(slot, true, false, true))
			{
				return CollectionDeck.SlotStatus.NOT_VALID;
			}
		}
		return CollectionDeck.SlotStatus.VALID;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0005383C File Offset: 0x00051A3C
	public bool HasReplaceableSlot()
	{
		for (int i = 0; i < this.m_slots.Count; i++)
		{
			if (!this.IsValidSlot(this.m_slots[i], false, false, false))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00053879 File Offset: 0x00051A79
	public CollectionDeckSlot GetSlotByIndex(int slotIndex)
	{
		if (slotIndex < 0 || slotIndex >= this.GetSlotCount())
		{
			return null;
		}
		return this.m_slots[slotIndex];
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x00053898 File Offset: 0x00051A98
	public CollectionDeckSlot GetExistingSlot(CollectionDeckSlot searchSlot)
	{
		if (this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return this.m_slots.Find((CollectionDeckSlot slot) => slot.CardID == searchSlot.CardID && slot.Owned == searchSlot.Owned);
		}
		return this.m_slots.Find((CollectionDeckSlot slot) => slot.CardID == searchSlot.CardID);
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x000538EC File Offset: 0x00051AEC
	public DeckRuleset GetRuleset()
	{
		DeckRuleset deckRuleset = null;
		switch (this.Type)
		{
		case DeckType.NORMAL_DECK:
		case DeckType.PRECON_DECK:
			deckRuleset = DeckRuleset.GetRuleset(this.FormatType);
			break;
		case DeckType.TAVERN_BRAWL_DECK:
		case DeckType.FSG_BRAWL_DECK:
			deckRuleset = TavernBrawlManager.Get().GetCurrentDeckRuleset();
			break;
		case DeckType.PVPDR_DECK:
			deckRuleset = DeckRuleset.GetPVPDRRuleset();
			break;
		case DeckType.PVPDR_DISPLAY_DECK:
			deckRuleset = DeckRuleset.GetPVPDRDisplayRuleset();
			break;
		}
		if (deckRuleset == null)
		{
			deckRuleset = DeckRuleset.GetRuleset(FormatType.FT_WILD);
		}
		return deckRuleset;
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x00053964 File Offset: 0x00051B64
	public bool IsValidForFormat(FormatType formatType)
	{
		switch (formatType)
		{
		case FormatType.FT_WILD:
			return this.FormatType == formatType || this.FormatType == FormatType.FT_STANDARD;
		case FormatType.FT_STANDARD:
			return this.FormatType == formatType;
		case FormatType.FT_CLASSIC:
			return this.FormatType == formatType && GameUtils.CLASSIC_ORDERED_HERO_CLASSES.Contains(this.GetClass());
		default:
			return false;
		}
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x000539C3 File Offset: 0x00051BC3
	public bool InsertSlotByDefaultSort(CollectionDeckSlot slot, bool forceUniqueSlot = false)
	{
		return this.InsertSlot(this.GetInsertionIdxByDefaultSort(slot), slot, forceUniqueSlot);
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x000539D4 File Offset: 0x00051BD4
	public void CopyFrom(CollectionDeck otherDeck)
	{
		this.ID = otherDeck.ID;
		this.Type = otherDeck.Type;
		this.m_name = otherDeck.m_name;
		this.HeroCardID = otherDeck.HeroCardID;
		this.HeroPremium = otherDeck.HeroPremium;
		this.HeroOverridden = otherDeck.HeroOverridden;
		this.CardBackID = otherDeck.CardBackID;
		this.CardBackOverridden = otherDeck.CardBackOverridden;
		this.NeedsName = otherDeck.NeedsName;
		this.SeasonId = otherDeck.SeasonId;
		this.BrawlLibraryItemId = otherDeck.BrawlLibraryItemId;
		this.FormatType = otherDeck.FormatType;
		this.SortOrder = otherDeck.SortOrder;
		this.SourceType = otherDeck.SourceType;
		this.UIHeroOverrideCardID = otherDeck.UIHeroOverrideCardID;
		this.UIHeroOverridePremium = otherDeck.UIHeroOverridePremium;
		this.m_slots.Clear();
		for (int i = 0; i < otherDeck.GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = otherDeck.GetSlotByIndex(i);
			CollectionDeckSlot collectionDeckSlot = new CollectionDeckSlot();
			collectionDeckSlot.CopyFrom(slotByIndex);
			this.m_slots.Add(collectionDeckSlot);
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x00053AE0 File Offset: 0x00051CE0
	public void CopyContents(CollectionDeck otherDeck)
	{
		this.HeroCardID = otherDeck.HeroCardID;
		this.HeroPremium = otherDeck.HeroPremium;
		this.UIHeroOverrideCardID = otherDeck.UIHeroOverrideCardID;
		this.UIHeroOverridePremium = otherDeck.UIHeroOverridePremium;
		this.m_slots.Clear();
		for (int i = 0; i < otherDeck.GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = otherDeck.GetSlotByIndex(i);
			foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				TAG_PREMIUM premium = (TAG_PREMIUM)obj;
				for (int j = 0; j < slotByIndex.GetCount(premium); j++)
				{
					this.AddCard(slotByIndex.CardID, premium, false);
				}
			}
		}
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x00053BB8 File Offset: 0x00051DB8
	public void FillFromShareableDeck(ShareableDeck shareableDeck)
	{
		this.HeroCardID = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId, false);
		this.FormatType = shareableDeck.FormatType;
		this.m_slots.Clear();
		for (int i = 0; i < shareableDeck.DeckContents.Cards.Count; i++)
		{
			string cardID = GameUtils.TranslateDbIdToCardId(shareableDeck.DeckContents.Cards[i].Def.Asset, false);
			TAG_PREMIUM premium = (TAG_PREMIUM)shareableDeck.DeckContents.Cards[i].Def.Premium;
			int qty = shareableDeck.DeckContents.Cards[i].Qty;
			for (int j = 0; j < qty; j++)
			{
				this.AddCard(cardID, premium, true);
			}
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x00053C7C File Offset: 0x00051E7C
	public void FillFromTemplateDeck(CollectionManager.TemplateDeck tplDeck)
	{
		this.ClearSlotContents();
		this.Name = tplDeck.m_title;
		foreach (KeyValuePair<string, int> keyValuePair in tplDeck.m_cardIds)
		{
			int num;
			int num2;
			int num3;
			CollectionManager.Get().GetOwnedCardCount(keyValuePair.Key, out num, out num2, out num3);
			int i;
			for (i = keyValuePair.Value; i > 0; i--)
			{
				if (num3 <= 0)
				{
					break;
				}
				this.AddCard(keyValuePair.Key, TAG_PREMIUM.DIAMOND, false);
				num3--;
			}
			while (i > 0)
			{
				if (num2 <= 0)
				{
					break;
				}
				this.AddCard(keyValuePair.Key, TAG_PREMIUM.GOLDEN, false);
				num2--;
				i--;
			}
			while (i > 0)
			{
				this.AddCard(keyValuePair.Key, TAG_PREMIUM.NORMAL, false);
				i--;
			}
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x00053D6C File Offset: 0x00051F6C
	public void FillFromCardList(IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		if (fillCards == null)
		{
			return;
		}
		foreach (DeckMaker.DeckFill deckFill in fillCards)
		{
			if (this.GetTotalCardCount() >= this.GetMaxCardCount())
			{
				break;
			}
			if (deckFill.m_addCard != null)
			{
				TAG_PREMIUM premium = TAG_PREMIUM.DIAMOND;
				if (!this.CanAddOwnedCard(deckFill.m_addCard.GetCardId(), premium))
				{
					premium = TAG_PREMIUM.GOLDEN;
					if (!this.CanAddOwnedCard(deckFill.m_addCard.GetCardId(), premium))
					{
						premium = TAG_PREMIUM.NORMAL;
						if (!this.CanAddOwnedCard(deckFill.m_addCard.GetCardId(), premium))
						{
							continue;
						}
					}
				}
				this.AddCard(deckFill.m_addCard.GetCardId(), premium, false);
			}
		}
		this.SendChanges();
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x00053E28 File Offset: 0x00052028
	public void ReconcileUnownedCards()
	{
		List<CollectionDeckSlot> list = new List<CollectionDeckSlot>();
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (!collectionDeckSlot.Owned)
			{
				list.Add(collectionDeckSlot);
			}
		}
		foreach (CollectionDeckSlot collectionDeckSlot2 in list)
		{
			int i = collectionDeckSlot2.Count;
			this.RemoveSlot(collectionDeckSlot2);
			while (i > 0)
			{
				i--;
				if (this.CanAddOwnedCard(collectionDeckSlot2.CardID, TAG_PREMIUM.DIAMOND))
				{
					this.AddCard(collectionDeckSlot2.CardID, TAG_PREMIUM.DIAMOND, false);
				}
				else if (this.CanAddOwnedCard(collectionDeckSlot2.CardID, TAG_PREMIUM.GOLDEN))
				{
					this.AddCard(collectionDeckSlot2.CardID, TAG_PREMIUM.GOLDEN, false);
				}
				else
				{
					this.AddCard(collectionDeckSlot2.CardID, TAG_PREMIUM.NORMAL, false);
				}
			}
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x00053F2C File Offset: 0x0005212C
	public void RemoveInvalidCards()
	{
		foreach (CollectionDeckSlot slot2 in this.GetSlots().FindAll((CollectionDeckSlot slot) => !this.IsValidSlot(slot, false, false, false)))
		{
			this.RemoveSlot(slot2);
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x00053F90 File Offset: 0x00052190
	public int GetUnownedCardIdCount(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID) && !collectionDeckSlot.Owned)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x00054000 File Offset: 0x00052200
	public int GetInvalidCardIdCount(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID) && !this.IsValidSlot(collectionDeckSlot, false, false, false))
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x00054074 File Offset: 0x00052274
	public int GetCardIdCount(string cardID, bool includeUnowned = true)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID) && (includeUnowned || collectionDeckSlot.Owned))
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x000540E8 File Offset: 0x000522E8
	public int GetCardCountMatchingTag(GAME_TAG tagName, int tagValue)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (GameUtils.GetCardTagValue(collectionDeckSlot.CardID, tagName) == tagValue)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00054150 File Offset: 0x00052350
	public int GetCardCountFirstMatchingSlot(string cardID, TAG_PREMIUM premium)
	{
		CollectionDeckSlot collectionDeckSlot = this.FindFirstSlotByCardId(cardID);
		if (collectionDeckSlot != null)
		{
			return collectionDeckSlot.GetCount(premium);
		}
		return 0;
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00054174 File Offset: 0x00052374
	public int GetCardCountAllMatchingSlots(string cardID)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID))
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x000541DC File Offset: 0x000523DC
	public int GetCardCountAllMatchingSlots(string cardID, TAG_PREMIUM premium)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID))
			{
				num += collectionDeckSlot.GetCount(premium);
			}
		}
		return num;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00054244 File Offset: 0x00052444
	public int GetValidCardCount(string cardID, TAG_PREMIUM premium, bool valid = true)
	{
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID) && this.IsValidSlot(collectionDeckSlot, false, false, false) == valid)
			{
				num += collectionDeckSlot.GetCount(premium);
			}
		}
		return num;
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x000542B8 File Offset: 0x000524B8
	public int GetOwnedCardCountInDeck(string cardID, TAG_PREMIUM premium, bool owned = true)
	{
		if (!this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return this.GetCardCountAllMatchingSlots(cardID);
		}
		int num = 0;
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			if (collectionDeckSlot.CardID.Equals(cardID) && collectionDeckSlot.Owned == owned)
			{
				num += collectionDeckSlot.GetCount(premium);
			}
		}
		return num;
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x00054338 File Offset: 0x00052538
	public int GetCardCountInSet(HashSet<string> set, bool isNot)
	{
		int num = 0;
		for (int i = 0; i < this.m_slots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = this.m_slots[i];
			if (set.Contains(collectionDeckSlot.CardID) == !isNot)
			{
				num += collectionDeckSlot.Count;
			}
		}
		return num;
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00054386 File Offset: 0x00052586
	public void ClearSlotContents()
	{
		this.m_slots.Clear();
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00054394 File Offset: 0x00052594
	public bool CanAddOwnedCard(string cardID, TAG_PREMIUM premium)
	{
		int ownedCardCountInDeck = this.GetOwnedCardCountInDeck(cardID, premium, true);
		return CollectionManager.Get().GetOwnedCount(cardID, premium) > ownedCardCountInDeck;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x000543BA File Offset: 0x000525BA
	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, bool exceedMax = false)
	{
		return this.AddCard(cardEntityDef.GetCardId(), premium, exceedMax);
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x000543CC File Offset: 0x000525CC
	public bool AddCard(string cardID, TAG_PREMIUM premium, bool exceedMax = false)
	{
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		if (deckRuleset != null)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
			if (deckRuleset.EntityIgnoresRuleset(entityDef) || deckRuleset.EntityInDeckIgnoresRuleset(this))
			{
				this.AddCard_IgnoreDeckRules(cardID, premium, 1);
				return true;
			}
		}
		if (!exceedMax && !this.CanInsertCard(cardID, premium))
		{
			return false;
		}
		bool flag = this.ShouldSplitSlotsByOwnershipOrFormatValidity() && this.CanAddOwnedCard(cardID, premium);
		bool forceUniqueSlot = false;
		bool flag2 = false;
		if (!this.m_forceDuplicatesIntoSeparateSlots)
		{
			CollectionDeckSlot collectionDeckSlot;
			if (!flag)
			{
				collectionDeckSlot = this.FindFirstOwnedSlotByCardId(cardID, flag);
			}
			else
			{
				collectionDeckSlot = this.FindFirstValidSlotByCardId(cardID, true, true);
			}
			forceUniqueSlot = (collectionDeckSlot != null && collectionDeckSlot.m_entityDefOverride != null);
			if (collectionDeckSlot != null && collectionDeckSlot.m_entityDefOverride == null)
			{
				collectionDeckSlot.AddCard(1, premium);
				flag2 = true;
			}
		}
		if (!flag2)
		{
			CollectionDeckSlot collectionDeckSlot = new CollectionDeckSlot
			{
				CardID = cardID,
				Owned = flag
			};
			collectionDeckSlot.SetCount(1, premium);
			if (!this.InsertSlotByDefaultSort(collectionDeckSlot, forceUniqueSlot))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000544B8 File Offset: 0x000526B8
	public void OnCardInsertedToCollection(string cardID, TAG_PREMIUM premium)
	{
		if (this.Type != DeckType.NORMAL_DECK)
		{
			return;
		}
		if (CollectionManager.Get().GetEditedDeck() == this)
		{
			return;
		}
		if (this.GetCardIdCount(cardID, true) <= 0)
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		entityDef.GetRarity();
		int num = (entityDef.GetRarity() == TAG_RARITY.LEGENDARY) ? 1 : 2;
		int ownedCardCountInDeck = this.GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.NORMAL, true);
		int ownedCardCountInDeck2 = this.GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.GOLDEN, true);
		int ownedCardCountInDeck3 = this.GetOwnedCardCountInDeck(cardID, TAG_PREMIUM.DIAMOND, true);
		int num2 = ownedCardCountInDeck + ownedCardCountInDeck2 + ownedCardCountInDeck3;
		if (num2 >= this.GetCardIdCount(cardID, true) || num2 >= num)
		{
			return;
		}
		this.AddCard(cardID, premium, false);
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x00054544 File Offset: 0x00052744
	public bool AddCard_DungeonCrawlBuff(string cardId, TAG_PREMIUM premium, List<int> enchantments)
	{
		CollectionDeckSlot collectionDeckSlot = new CollectionDeckSlot
		{
			CardID = cardId,
			Owned = true
		};
		collectionDeckSlot.CreateDynamicEntity();
		foreach (int dbId in enchantments)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(dbId, true);
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
		return this.InsertSlotByDefaultSort(collectionDeckSlot, true);
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000546F8 File Offset: 0x000528F8
	public void AddCard_IgnoreDeckRules(string cardID, TAG_PREMIUM premium, int countToAdd)
	{
		if (countToAdd <= 0)
		{
			return;
		}
		int num = countToAdd;
		if (this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			int ownedCount = CollectionManager.Get().GetOwnedCount(cardID, premium);
			if (ownedCount > 0)
			{
				CollectionDeckSlot collectionDeckSlot = this.FindFirstOwnedSlotByCardId(cardID, true);
				if (collectionDeckSlot == null || this.m_forceDuplicatesIntoSeparateSlots)
				{
					collectionDeckSlot = new CollectionDeckSlot
					{
						CardID = cardID,
						Owned = true
					};
					collectionDeckSlot.SetCount(0, premium);
					this.InsertSlotByDefaultSort(collectionDeckSlot, false);
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
			CollectionDeckSlot collectionDeckSlot2 = this.FindFirstOwnedSlotByCardId(cardID, false);
			if (collectionDeckSlot2 == null || this.m_forceDuplicatesIntoSeparateSlots)
			{
				collectionDeckSlot2 = new CollectionDeckSlot
				{
					CardID = cardID,
					Owned = false
				};
				collectionDeckSlot2.SetCount(num, premium);
				this.InsertSlotByDefaultSort(collectionDeckSlot2, false);
			}
			else
			{
				collectionDeckSlot2.AddCard(num, premium);
			}
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000547D4 File Offset: 0x000529D4
	public bool RemoveCard(string cardID, TAG_PREMIUM premium, bool valid = true)
	{
		CollectionDeckSlot collectionDeckSlot = this.FindFirstValidSlotByCardId(cardID, valid, false);
		if (collectionDeckSlot == null)
		{
			return false;
		}
		collectionDeckSlot.RemoveCard(1, premium);
		this.UpdateUIHeroOverrideCardRemoval(cardID);
		return true;
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x00054800 File Offset: 0x00052A00
	public void RemoveAllCards()
	{
		this.m_slots = new List<CollectionDeckSlot>();
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x00054810 File Offset: 0x00052A10
	private void UpdateUIHeroOverrideCardRemoval(string cardID)
	{
		if (GameDbf.GetIndex().HasCardPlayerDeckOverride(cardID) && (CollectionDeckTray.Get() == null || !CollectionDeckTray.Get().IsShowingDeckContents()))
		{
			this.UIHeroOverrideCardID = string.Empty;
			this.UIHeroOverridePremium = TAG_PREMIUM.NORMAL;
			this.Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", new object[]
			{
				GameStrings.GetClassName(this.GetClass())
			});
			CollectionManager.Get().OnUIHeroOverrideCardRemoved();
		}
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x00054883 File Offset: 0x00052A83
	public void OnContentChangesComplete()
	{
		this.m_isSavingContentChanges = false;
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0005488C File Offset: 0x00052A8C
	public void OnNameChangeComplete()
	{
		this.m_isSavingNameChanges = false;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x00054898 File Offset: 0x00052A98
	public void SendChanges()
	{
		CollectionDeck baseDeck = CollectionManager.Get().GetBaseDeck(this.ID);
		if (this == baseDeck)
		{
			Debug.LogError(string.Format("CollectionDeck.Send() - {0} is a base deck. You cannot send a base deck to the network.", baseDeck));
			return;
		}
		if (baseDeck == null)
		{
			Log.CollectionManager.PrintError("CollectionDeck.SendChanges() - No base deck with id=" + this.ID, Array.Empty<object>());
			return;
		}
		string text;
		this.GenerateNameDiff(baseDeck, out text);
		List<Network.CardUserData> list = this.GenerateContentChanges(baseDeck);
		int newHeroAssetID;
		TAG_PREMIUM newHeroCardPremium;
		int heroOverrideAssetID;
		TAG_PREMIUM heroOverridePremium;
		bool flag = this.GenerateHeroDiff(baseDeck, out newHeroAssetID, out newHeroCardPremium, out heroOverrideAssetID, out heroOverridePremium);
		int newCardBackID;
		bool flag2 = this.GenerateCardBackDiff(baseDeck, out newCardBackID);
		bool flag3 = baseDeck.FormatType != this.FormatType;
		bool flag4 = baseDeck.SortOrder != this.SortOrder;
		Network network = Network.Get();
		if (text != null)
		{
			this.m_isSavingNameChanges = true;
			network.RenameDeck(this.ID, text);
		}
		string pastedDeckHash = null;
		if (this.m_createdFromShareableDeck != null)
		{
			pastedDeckHash = this.m_createdFromShareableDeck.Serialize(false);
		}
		if (list.Count > 0 || flag || flag2 || flag3 || flag4)
		{
			this.m_isSavingContentChanges = true;
			Network.Get().SendDeckData(this.ID, list, newHeroAssetID, newHeroCardPremium, heroOverrideAssetID, heroOverridePremium, newCardBackID, this.FormatType, this.SortOrder, pastedDeckHash);
		}
		if (!Network.IsLoggedIn())
		{
			this.OnContentChangesComplete();
			this.OnNameChangeComplete();
		}
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x000549D8 File Offset: 0x00052BD8
	public static string GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(DeckRuleViolation violation)
	{
		if (violation == null || violation.Rule == null)
		{
			return string.Empty;
		}
		DeckRule.RuleType type = violation.Rule.Type;
		if (type <= DeckRule.RuleType.PLAYER_OWNS_EACH_COPY)
		{
			if (type == DeckRule.RuleType.IS_NOT_ROTATED)
			{
				return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_FORMAT");
			}
			if (type != DeckRule.RuleType.PLAYER_OWNS_EACH_COPY)
			{
				goto IL_57;
			}
		}
		else if (type != DeckRule.RuleType.DECK_SIZE)
		{
			if (type != DeckRule.RuleType.IS_CARD_PLAYABLE)
			{
				goto IL_57;
			}
			return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_UNPLAYABLE");
		}
		return GameStrings.Get("GLUE_COLLECTION_DECK_COPY_TOOLTIP_INCOMPLETE");
		IL_57:
		return violation.DisplayError;
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x00054A42 File Offset: 0x00052C42
	public void SetShareableDeckCreatedFrom(ShareableDeck shareableDeck)
	{
		this.m_createdFromShareableDeck = shareableDeck;
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x00054A4C File Offset: 0x00052C4C
	public bool CanInsertCard(string cardID, TAG_PREMIUM premium)
	{
		if (DeckType.DRAFT_DECK == this.Type || DeckType.CLIENT_ONLY_DECK == this.Type)
		{
			return true;
		}
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		if (deckRuleset == null)
		{
			return true;
		}
		if (this.Type == DeckType.PVPDR_DISPLAY_DECK && DuelsConfig.IsCardLoadoutTreasure(cardID))
		{
			return true;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		RuleInvalidReason ruleInvalidReason;
		DeckRule deckRule;
		return deckRuleset.CanAddToDeck(entityDef, premium, this, out ruleInvalidReason, out deckRule, new DeckRule.RuleType[]
		{
			DeckRule.RuleType.PLAYER_OWNS_EACH_COPY,
			DeckRule.RuleType.IS_CARD_PLAYABLE
		});
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00054ABC File Offset: 0x00052CBC
	private bool InsertSlot(int slotIndex, CollectionDeckSlot slot, bool forceUniqueSlot = false)
	{
		if (slotIndex < 0 || slotIndex > this.GetSlotCount())
		{
			Log.Decks.Print(string.Format("CollectionDeck.InsertSlot(): inserting slot {0} failed; invalid slot index {1}.", slot, slotIndex), Array.Empty<object>());
			return false;
		}
		if (!this.m_forceDuplicatesIntoSeparateSlots && !forceUniqueSlot)
		{
			CollectionDeckSlot existingSlot = this.GetExistingSlot(slot);
			if (existingSlot != null)
			{
				Debug.LogWarningFormat("CollectionDeck.InsertSlot: slot with cardId={0} already exists in deckId={1} cardDbId={3} bestPremium={4} owned={5} existingCount={6} slotIndex={7}", new object[]
				{
					slot.CardID,
					this.ID,
					slot.CardID,
					GameUtils.TranslateCardIdToDbId(slot.CardID, false),
					slot.PreferredPremium,
					slot.Owned,
					existingSlot.Count,
					slotIndex
				});
				return false;
			}
		}
		slot.OnSlotEmptied = (CollectionDeckSlot.DelOnSlotEmptied)Delegate.Combine(slot.OnSlotEmptied, new CollectionDeckSlot.DelOnSlotEmptied(this.OnSlotEmptied));
		slot.Index = slotIndex;
		this.m_slots.Insert(slotIndex, slot);
		this.UpdateSlotIndices(slotIndex, this.GetSlotCount() - 1);
		return true;
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x00054BD3 File Offset: 0x00052DD3
	public void ForceRemoveSlot(CollectionDeckSlot slot)
	{
		this.RemoveSlot(slot);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00054BDC File Offset: 0x00052DDC
	private void RemoveSlot(CollectionDeckSlot slot)
	{
		slot.OnSlotEmptied = (CollectionDeckSlot.DelOnSlotEmptied)Delegate.Remove(slot.OnSlotEmptied, new CollectionDeckSlot.DelOnSlotEmptied(this.OnSlotEmptied));
		int index = slot.Index;
		this.m_slots.RemoveAt(index);
		slot.m_entityDefOverride = null;
		this.UpdateSlotIndices(index, this.GetSlotCount() - 1);
		this.UpdateUIHeroOverrideCardRemoval(slot.CardID);
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x00054C40 File Offset: 0x00052E40
	private void OnSlotEmptied(CollectionDeckSlot slot)
	{
		if (this.GetExistingSlot(slot) == null)
		{
			Log.Decks.Print(string.Format("CollectionDeck.OnSlotCountUpdated(): Trying to remove slot {0}, but it does not exist in deck {1}", slot, this), Array.Empty<object>());
			return;
		}
		this.RemoveSlot(slot);
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x00054C70 File Offset: 0x00052E70
	private void UpdateSlotIndices(int indexA, int indexB)
	{
		if (this.GetSlotCount() == 0)
		{
			return;
		}
		int num;
		int num2;
		if (indexA < indexB)
		{
			num = indexA;
			num2 = indexB;
		}
		else
		{
			num = indexB;
			num2 = indexA;
		}
		num = Math.Max(0, num);
		num2 = Math.Min(num2, this.GetSlotCount() - 1);
		for (int i = num; i <= num2; i++)
		{
			this.GetSlotByIndex(i).Index = i;
		}
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x00054CC4 File Offset: 0x00052EC4
	public CollectionDeckSlot FindFirstSlotByCardId(string cardID)
	{
		return this.m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID));
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x00054CF8 File Offset: 0x00052EF8
	public CollectionDeckSlot FindFirstOwnedSlotByCardId(string cardID, bool owned)
	{
		if (!this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			return this.FindFirstSlotByCardId(cardID);
		}
		return this.m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID) && slot.Owned == owned);
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x00054D48 File Offset: 0x00052F48
	private CollectionDeckSlot FindFirstValidSlotByCardId(string cardID, bool valid, bool ignoreEvent = false)
	{
		if (!this.ShouldSplitSlotsByOwnershipOrFormatValidity())
		{
			Log.Decks.PrintWarning("Your deck doesn't care about Validity.  Why are you using 'FindFirstValidSlot' as opposed to 'FindFirstOwnedSlot'? This may be a bug!", Array.Empty<object>());
			return this.FindFirstSlotByCardId(cardID);
		}
		return this.m_slots.Find((CollectionDeckSlot slot) => slot.CardID.Equals(cardID) && this.IsValidSlot(slot, false, ignoreEvent, false) == valid);
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00054DB7 File Offset: 0x00052FB7
	private void GenerateNameDiff(CollectionDeck baseDeck, out string deckName)
	{
		deckName = null;
		if (!this.Name.Equals(baseDeck.Name))
		{
			deckName = this.Name;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x00054DD8 File Offset: 0x00052FD8
	private bool GenerateHeroDiff(CollectionDeck baseDeck, out int heroAssetID, out TAG_PREMIUM heroCardPremium, out int overrideHeroAssetID, out TAG_PREMIUM overrideHeroPremium)
	{
		heroAssetID = -1;
		heroCardPremium = TAG_PREMIUM.NORMAL;
		overrideHeroAssetID = -1;
		overrideHeroPremium = TAG_PREMIUM.NORMAL;
		bool result = false;
		bool flag = this.HeroCardID == baseDeck.HeroCardID && this.HeroPremium == baseDeck.HeroPremium;
		if (this.HeroOverridden && (!baseDeck.HeroOverridden || !flag))
		{
			heroAssetID = GameUtils.TranslateCardIdToDbId(this.HeroCardID, false);
			heroCardPremium = this.HeroPremium;
			result = true;
		}
		if (!(this.UIHeroOverrideCardID == baseDeck.UIHeroOverrideCardID) || this.UIHeroOverridePremium != baseDeck.UIHeroOverridePremium)
		{
			overrideHeroAssetID = (string.IsNullOrEmpty(this.UIHeroOverrideCardID) ? 0 : GameUtils.TranslateCardIdToDbId(this.UIHeroOverrideCardID, false));
			overrideHeroPremium = this.UIHeroOverridePremium;
			result = true;
		}
		return result;
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x00054E98 File Offset: 0x00053098
	private bool GenerateCardBackDiff(CollectionDeck baseDeck, out int cardBackID)
	{
		cardBackID = -1;
		if (!this.CardBackOverridden)
		{
			return false;
		}
		bool flag = this.CardBackID == baseDeck.CardBackID;
		if (baseDeck.CardBackOverridden && flag)
		{
			return false;
		}
		cardBackID = this.CardBackID;
		return true;
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x00054ED8 File Offset: 0x000530D8
	private List<Network.CardUserData> CardUserDataFromSlot(CollectionDeckSlot deckSlot, bool deleted)
	{
		List<Network.CardUserData> list = new List<Network.CardUserData>();
		Network.CardUserData cardUserData = new Network.CardUserData();
		cardUserData.DbId = GameUtils.TranslateCardIdToDbId(deckSlot.CardID, false);
		cardUserData.Count = (deleted ? 0 : deckSlot.GetCount(TAG_PREMIUM.NORMAL));
		cardUserData.Premium = TAG_PREMIUM.NORMAL;
		Network.CardUserData cardUserData2 = new Network.CardUserData();
		cardUserData2.DbId = cardUserData.DbId;
		cardUserData2.Count = (deleted ? 0 : deckSlot.GetCount(TAG_PREMIUM.GOLDEN));
		cardUserData2.Premium = TAG_PREMIUM.GOLDEN;
		Network.CardUserData cardUserData3 = new Network.CardUserData();
		cardUserData3.DbId = cardUserData.DbId;
		cardUserData3.Count = (deleted ? 0 : deckSlot.GetCount(TAG_PREMIUM.DIAMOND));
		cardUserData3.Premium = TAG_PREMIUM.DIAMOND;
		list.Add(cardUserData);
		list.Add(cardUserData2);
		list.Add(cardUserData3);
		return list;
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x00054F8C File Offset: 0x0005318C
	private List<Network.CardUserData> GenerateContentChanges(CollectionDeck baseDeck)
	{
		SortedDictionary<string, CollectionDeckSlot> sortedDictionary = new SortedDictionary<string, CollectionDeckSlot>();
		foreach (CollectionDeckSlot collectionDeckSlot in baseDeck.GetSlots())
		{
			CollectionDeckSlot collectionDeckSlot2 = null;
			if (sortedDictionary.TryGetValue(collectionDeckSlot.CardID, out collectionDeckSlot2))
			{
				using (IEnumerator enumerator2 = Enum.GetValues(typeof(TAG_PREMIUM)).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						TAG_PREMIUM premium = (TAG_PREMIUM)obj;
						collectionDeckSlot2.AddCard(collectionDeckSlot.GetCount(premium), premium);
					}
					continue;
				}
			}
			collectionDeckSlot2 = new CollectionDeckSlot();
			collectionDeckSlot2.CopyFrom(collectionDeckSlot);
			sortedDictionary.Add(collectionDeckSlot2.CardID, collectionDeckSlot2);
		}
		SortedDictionary<string, CollectionDeckSlot> sortedDictionary2 = new SortedDictionary<string, CollectionDeckSlot>();
		foreach (CollectionDeckSlot collectionDeckSlot3 in this.GetSlots())
		{
			CollectionDeckSlot collectionDeckSlot4 = null;
			if (sortedDictionary2.TryGetValue(collectionDeckSlot3.CardID, out collectionDeckSlot4))
			{
				using (IEnumerator enumerator2 = Enum.GetValues(typeof(TAG_PREMIUM)).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						TAG_PREMIUM premium2 = (TAG_PREMIUM)obj2;
						collectionDeckSlot4.AddCard(collectionDeckSlot3.GetCount(premium2), premium2);
					}
					continue;
				}
			}
			collectionDeckSlot4 = new CollectionDeckSlot();
			collectionDeckSlot4.CopyFrom(collectionDeckSlot3);
			sortedDictionary2.Add(collectionDeckSlot4.CardID, collectionDeckSlot4);
		}
		SortedDictionary<string, CollectionDeckSlot>.Enumerator enumerator3 = sortedDictionary.GetEnumerator();
		SortedDictionary<string, CollectionDeckSlot>.Enumerator enumerator4 = sortedDictionary2.GetEnumerator();
		List<Network.CardUserData> list = new List<Network.CardUserData>();
		bool flag = enumerator3.MoveNext();
		bool flag2 = enumerator4.MoveNext();
		while (flag && flag2)
		{
			KeyValuePair<string, CollectionDeckSlot> keyValuePair = enumerator3.Current;
			CollectionDeckSlot value = keyValuePair.Value;
			keyValuePair = enumerator4.Current;
			CollectionDeckSlot value2 = keyValuePair.Value;
			if (value.CardID == value2.CardID)
			{
				if (value.GetCount(TAG_PREMIUM.NORMAL) != value2.GetCount(TAG_PREMIUM.NORMAL) || value.GetCount(TAG_PREMIUM.GOLDEN) != value2.GetCount(TAG_PREMIUM.GOLDEN) || value.GetCount(TAG_PREMIUM.DIAMOND) != value2.GetCount(TAG_PREMIUM.DIAMOND))
				{
					list.AddRange(this.CardUserDataFromSlot(value2, value2.Count == 0));
				}
				flag = enumerator3.MoveNext();
				flag2 = enumerator4.MoveNext();
			}
			else if (value.CardID.CompareTo(value2.CardID) < 0)
			{
				list.AddRange(this.CardUserDataFromSlot(value, true));
				flag = enumerator3.MoveNext();
			}
			else
			{
				list.AddRange(this.CardUserDataFromSlot(value2, false));
				flag2 = enumerator4.MoveNext();
			}
		}
		while (flag)
		{
			KeyValuePair<string, CollectionDeckSlot> keyValuePair = enumerator3.Current;
			CollectionDeckSlot value3 = keyValuePair.Value;
			list.AddRange(this.CardUserDataFromSlot(value3, true));
			flag = enumerator3.MoveNext();
		}
		while (flag2)
		{
			KeyValuePair<string, CollectionDeckSlot> keyValuePair = enumerator4.Current;
			CollectionDeckSlot value4 = keyValuePair.Value;
			list.AddRange(this.CardUserDataFromSlot(value4, false));
			flag2 = enumerator4.MoveNext();
		}
		return list;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x000552E0 File Offset: 0x000534E0
	private int GetInsertionIdxByDefaultSort(CollectionDeckSlot slot)
	{
		EntityDef entityDef = slot.GetEntityDef();
		if (entityDef == null)
		{
			Log.Decks.Print(string.Format("CollectionDeck.GetInsertionIdxByDefaultSort(): could not get entity def for {0}", slot.CardID), Array.Empty<object>());
			return -1;
		}
		int i;
		for (i = 0; i < this.GetSlotCount(); i++)
		{
			CollectionDeckSlot slotByIndex = this.GetSlotByIndex(i);
			EntityDef entityDef2 = slotByIndex.GetEntityDef();
			if (entityDef2 == null)
			{
				Log.Decks.Print(string.Format("CollectionDeck.GetInsertionIdxByDefaultSort(): entityDef is null at slot index {0}", i), Array.Empty<object>());
				break;
			}
			int num = CollectionManager.Get().EntityDefSortComparison(entityDef, entityDef2);
			if (num < 0 || (num <= 0 && (!this.ShouldSplitSlotsByOwnershipOrFormatValidity() || slot.Owned == slotByIndex.Owned)))
			{
				break;
			}
		}
		return i;
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x0005538C File Offset: 0x0005358C
	public TAG_CLASS GetClass()
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(this.HeroCardID);
		if (entityDef != null)
		{
			return entityDef.GetClass();
		}
		return TAG_CLASS.INVALID;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x000553B8 File Offset: 0x000535B8
	public ShareableDeck GetShareableDeck()
	{
		DeckContents deckContents = this.GetDeckContents();
		int heroCardDbId = GameUtils.TranslateCardIdToDbId(this.HeroCardID, false);
		return new ShareableDeck(this.Name, heroCardDbId, deckContents, this.FormatType, this.Type == DeckType.DRAFT_DECK);
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x000553F8 File Offset: 0x000535F8
	public bool CanCopyAsShareableDeck(out DeckRuleViolation topViolation)
	{
		topViolation = null;
		if (this.GetRuleset() == null)
		{
			return false;
		}
		IList<DeckRuleViolation> list;
		if (!this.GetRuleset().IsDeckValid(this, out list, Array.Empty<DeckRule.RuleType>()) && list != null && list.Count > 0)
		{
			topViolation = list[0];
			return false;
		}
		return true;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x00055440 File Offset: 0x00053640
	public void LogDeckStringInformation()
	{
		Log.Decks.PrintInfo(string.Format("{0} {1}", "###", this.Name), Array.Empty<object>());
		Log.Decks.PrintInfo(string.Format("{0}Deck ID: {1}", "# ", this.ID), Array.Empty<object>());
		Log.Decks.PrintInfo(this.GetShareableDeck().Serialize(false), Array.Empty<object>());
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x000554B8 File Offset: 0x000536B8
	public List<DeckMaker.DeckFill> GetDeckFillFromString(string deckString)
	{
		List<DeckMaker.DeckFill> list = new List<DeckMaker.DeckFill>();
		string[] array = deckString.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim();
			if (!text.StartsWith("#"))
			{
				try
				{
					foreach (string text2 in text.Split(new char[]
					{
						';'
					}))
					{
						try
						{
							string[] array3 = text2.Split(new char[]
							{
								','
							});
							int num;
							if (int.TryParse(array3[0], out num) && num >= 0 && num <= 10)
							{
								string cardId = array3[1];
								EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
								if (entityDef != null)
								{
									for (int k = 0; k < num; k++)
									{
										list.Add(new DeckMaker.DeckFill
										{
											m_addCard = entityDef
										});
									}
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
		}
		return list;
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x000555D0 File Offset: 0x000537D0
	public DeckContents GetDeckContents()
	{
		DeckContents deckContents = new DeckContents
		{
			DeckId = this.ID
		};
		foreach (CollectionDeckSlot collectionDeckSlot in this.m_slots)
		{
			DeckCardData item = new DeckCardData
			{
				Def = new PegasusShared.CardDef
				{
					Asset = GameUtils.TranslateCardIdToDbId(collectionDeckSlot.CardID, false),
					Premium = (int)collectionDeckSlot.PreferredPremium
				},
				Qty = collectionDeckSlot.Count
			};
			deckContents.Cards.Add(item);
		}
		return deckContents;
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x00055678 File Offset: 0x00053878
	public bool ShouldSplitSlotsByOwnershipOrFormatValidity()
	{
		if (this.Locked)
		{
			return false;
		}
		DeckType type = this.Type;
		return type != DeckType.CLIENT_ONLY_DECK && type != DeckType.DRAFT_DECK && (type - DeckType.TAVERN_BRAWL_DECK > 1 || (TavernBrawlManager.Get().IsCurrentBrawlTypeActive && TavernBrawlManager.Get().GetCurrentDeckRuleset() != null && TavernBrawlManager.Get().GetCurrentDeckRuleset().HasOwnershipOrRotatedRule()));
	}

	// Token: 0x04000A35 RID: 2613
	public static int DefaultMaxDeckNameCharacters = 24;

	// Token: 0x04000A36 RID: 2614
	private string m_name;

	// Token: 0x04000A37 RID: 2615
	private List<CollectionDeckSlot> m_slots = new List<CollectionDeckSlot>();

	// Token: 0x04000A38 RID: 2616
	private bool m_forceDuplicatesIntoSeparateSlots;

	// Token: 0x04000A39 RID: 2617
	private bool m_netContentsLoaded;

	// Token: 0x04000A3A RID: 2618
	private bool m_isSavingContentChanges;

	// Token: 0x04000A3B RID: 2619
	private bool m_isSavingNameChanges;

	// Token: 0x04000A3C RID: 2620
	private bool m_isBeingDeleted;

	// Token: 0x04000A3D RID: 2621
	private ShareableDeck m_createdFromShareableDeck;

	// Token: 0x04000A3E RID: 2622
	public long ID;

	// Token: 0x04000A3F RID: 2623
	public DeckType Type = DeckType.NORMAL_DECK;

	// Token: 0x04000A40 RID: 2624
	public string HeroCardID = string.Empty;

	// Token: 0x04000A41 RID: 2625
	public TAG_PREMIUM HeroPremium;

	// Token: 0x04000A42 RID: 2626
	public bool HeroOverridden;

	// Token: 0x04000A43 RID: 2627
	public int CardBackID;

	// Token: 0x04000A44 RID: 2628
	public bool CardBackOverridden;

	// Token: 0x04000A45 RID: 2629
	public int SeasonId;

	// Token: 0x04000A46 RID: 2630
	public int BrawlLibraryItemId;

	// Token: 0x04000A47 RID: 2631
	public bool NeedsName;

	// Token: 0x04000A48 RID: 2632
	public long SortOrder;

	// Token: 0x04000A49 RID: 2633
	public ulong CreateDate;

	// Token: 0x04000A4A RID: 2634
	public bool Locked;

	// Token: 0x04000A4B RID: 2635
	public DeckSourceType SourceType;

	// Token: 0x04000A4C RID: 2636
	public string HeroPowerCardID = string.Empty;

	// Token: 0x04000A4D RID: 2637
	public string UIHeroOverrideCardID = string.Empty;

	// Token: 0x04000A4E RID: 2638
	public TAG_PREMIUM UIHeroOverridePremium;

	// Token: 0x0200141F RID: 5151
	public enum SlotStatus
	{
		// Token: 0x0400A907 RID: 43271
		UNKNOWN,
		// Token: 0x0400A908 RID: 43272
		VALID,
		// Token: 0x0400A909 RID: 43273
		NOT_VALID,
		// Token: 0x0400A90A RID: 43274
		MISSING
	}
}
