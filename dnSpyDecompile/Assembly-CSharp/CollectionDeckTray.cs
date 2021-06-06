using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

// Token: 0x0200010A RID: 266
[CustomEditClass]
public class CollectionDeckTray : EditableDeckTray
{
	// Token: 0x06000FE5 RID: 4069 RVA: 0x00058BA0 File Offset: 0x00056DA0
	private void Awake()
	{
		CollectionDeckTray.s_instance = this;
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		if (this.m_scrollbar != null)
		{
			if (SceneMgr.Get().IsInTavernBrawlMode() && !UniversalInputManager.UsePhoneUI)
			{
				Vector3 center = this.m_scrollbar.m_ScrollBounds.center;
				center.z = 3f;
				this.m_scrollbar.m_ScrollBounds.center = center;
				Vector3 size = this.m_scrollbar.m_ScrollBounds.size;
				size.z = 47.67f;
				this.m_scrollbar.m_ScrollBounds.size = size;
				if (this.m_cardsContent != null && this.m_cardsContent.m_deckCompleteHighlight != null)
				{
					Vector3 localPosition = this.m_cardsContent.m_deckCompleteHighlight.transform.localPosition;
					localPosition.z = -34.15f;
					this.m_cardsContent.m_deckCompleteHighlight.transform.localPosition = localPosition;
				}
			}
			this.m_scrollbar.Enable(false);
			this.m_scrollbar.AddTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(base.OnTouchScrollStarted));
			this.m_scrollbar.AddTouchScrollEndedListener(new UIBScrollable.OnTouchScrollEnded(base.OnTouchScrollEnded));
		}
		this.m_contents[DeckTray.DeckContentTypes.Decks] = this.m_decksContent;
		this.m_contents[DeckTray.DeckContentTypes.Cards] = this.m_cardsContent;
		if (this.m_heroSkinContent != null)
		{
			this.m_contents[DeckTray.DeckContentTypes.HeroSkin] = this.m_heroSkinContent;
			this.m_heroSkinContent.RegisterHeroAssignedListener(new DeckTrayHeroSkinContent.HeroAssigned(this.OnHeroAssigned));
		}
		if (this.m_cardBackContent != null)
		{
			this.m_contents[DeckTray.DeckContentTypes.CardBack] = this.m_cardBackContent;
		}
		this.m_cardsContent.RegisterCardTileHeldListener(new DeckTrayCardListContent.CardTileHeld(this.OnCardTileHeld));
		this.m_cardsContent.RegisterCardTilePressListener(new DeckTrayCardListContent.CardTilePress(this.OnCardTilePress));
		this.m_cardsContent.RegisterCardTileTapListener(new DeckTrayCardListContent.CardTileTap(this.OnCardTileTap));
		this.m_cardsContent.RegisterCardTileOverListener(new DeckTrayCardListContent.CardTileOver(this.OnCardTileOver));
		this.m_cardsContent.RegisterCardTileOutListener(new DeckTrayCardListContent.CardTileOut(this.OnCardTileOut));
		this.m_cardsContent.RegisterCardTileReleaseListener(new DeckTrayCardListContent.CardTileRelease(this.OnCardTileRelease));
		this.m_cardsContent.RegisterCardCountUpdated(new DeckTrayCardListContent.CardCountChanged(this.OnCardCountUpdated));
		this.m_decksContent.RegisterBusyWithDeck(new DeckTrayDeckListContent.BusyWithDeck(base.OnBusyWithDeck));
		string key = "GLUE_COLLECTION_MY_DECKS";
		if (SceneMgr.Get().IsInDuelsMode())
		{
			key = "GLUE_PVPDR_DECK_TRAY_HEADER";
		}
		else if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			key = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? "GLUE_HEROIC_BRAWL_DECK" : "GLUE_COLLECTION_DECK");
		}
		else
		{
			this.m_decksContent.RegisterDeckCountUpdated(new DeckTrayDeckListContent.DeckCountChanged(this.OnDeckCountUpdated));
		}
		this.SetMyDecksLabelText(GameStrings.Get(key));
		this.m_doneButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		this.m_doneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.DoneButtonPress));
		CollectionManager.Get().RegisterEditedDeckChanged(new CollectionManager.OnEditedDeckChanged(base.OnEditedDeckChanged));
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		CollectionInputMgr.Get().SetScrollbar(this.m_scrollbar);
		foreach (DeckTray.DeckContentScroll deckContentScroll in this.m_scrollables)
		{
			deckContentScroll.SaveStartPosition();
		}
		this.m_defaultCardEventHandler = base.gameObject.AddComponent<CollectionCardEventHandler>();
		if (SceneMgr.Get().IsInDuelsMode() && this.TrayContentsContainer != null && this.TrayContentsDuelsBone != null)
		{
			this.TrayContentsContainer.transform.localPosition = this.TrayContentsDuelsBone.transform.localPosition;
		}
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x00058F94 File Offset: 0x00057194
	protected override void Start()
	{
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(true);
		CollectionManager.Get().GetCollectibleDisplay().RegisterSwitchViewModeListener(new CollectibleDisplay.OnSwitchViewMode(this.OnCMViewModeChanged));
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING && !SceneMgr.Get().IsInDuelsMode())
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnBackOutOfCollectionScreen));
		}
		CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreated));
		base.Start();
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00059014 File Offset: 0x00057214
	private void OnDestroy()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.RemoveEditedDeckChanged(new CollectionManager.OnEditedDeckChanged(base.OnEditedDeckChanged));
			collectionManager.DoneEditing();
		}
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		}
		CollectionDeckTray.s_instance = null;
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00059068 File Offset: 0x00057268
	public bool CanPickupCard()
	{
		DeckTray.DeckContentTypes currentContentType = base.GetCurrentContentType();
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		return (currentContentType == DeckTray.DeckContentTypes.Cards && viewMode == CollectionUtils.ViewMode.CARDS) || (currentContentType == DeckTray.DeckContentTypes.CardBack && viewMode == CollectionUtils.ViewMode.CARD_BACKS) || (currentContentType == DeckTray.DeckContentTypes.HeroSkin && viewMode == CollectionUtils.ViewMode.HERO_SKINS) || (currentContentType == DeckTray.DeckContentTypes.Coin && viewMode == CollectionUtils.ViewMode.COINS);
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x000590AF File Offset: 0x000572AF
	public static CollectionDeckTray Get()
	{
		return CollectionDeckTray.s_instance;
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x000590B6 File Offset: 0x000572B6
	public void Unload()
	{
		CollectionManager.Get().RemoveDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreated));
		CollectionInputMgr.Get().SetScrollbar(null);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000590DC File Offset: 0x000572DC
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (UniversalInputManager.Get() != null && UniversalInputManager.Get().IsTextInputActive())
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, true);
		}
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				editedDeck.SendChanges();
			}
		}
		this.Exit();
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00059134 File Offset: 0x00057334
	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, Actor animateActor = null, bool allowInvalid = false)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		CollectionCardEventHandler cardEventHandler = this.GetCardEventHandler(cardEntityDef.GetCardId());
		bool updateVisuals = cardEventHandler.ShouldUpdateVisuals();
		EntityDef entityDef = null;
		bool flag = base.GetCardsContent().AddCard(cardEntityDef, premium, deckTileToRemove, playSound, ref entityDef, animateActor, allowInvalid, updateVisuals);
		if (flag)
		{
			cardEventHandler.OnCardAdded(this, editedDeck, cardEntityDef, premium, animateActor);
		}
		if (entityDef != null)
		{
			this.GetCardEventHandler(entityDef.GetCardId()).OnCardRemoved(this, editedDeck);
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			AdventureDungeonCrawlDisplay.Get().SyncDeckList();
			this.UpdateDoneButtonText();
		}
		return flag;
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x000591BC File Offset: 0x000573BC
	private bool AddCardWithPreferredPremium(EntityDef cardEntityDef, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, Actor animateActor = null, bool allowInvalid = false)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		TAG_PREMIUM preferredPremiumThatCanBeAddedToDeck = this.GetPreferredPremiumThatCanBeAddedToDeck(editedDeck, cardEntityDef.GetCardId());
		return this.AddCard(cardEntityDef, preferredPremiumThatCanBeAddedToDeck, deckTileToRemove, playSound, animateActor, allowInvalid);
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x000591F0 File Offset: 0x000573F0
	public TAG_PREMIUM GetPreferredPremiumThatCanBeAddedToDeck(CollectionDeck deck, string cardId)
	{
		TAG_PREMIUM tag_PREMIUM = TAG_PREMIUM.DIAMOND;
		if (!deck.CanAddOwnedCard(cardId, tag_PREMIUM))
		{
			tag_PREMIUM = TAG_PREMIUM.GOLDEN;
			if (!deck.CanAddOwnedCard(cardId, tag_PREMIUM))
			{
				tag_PREMIUM = TAG_PREMIUM.NORMAL;
			}
		}
		return tag_PREMIUM;
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00059218 File Offset: 0x00057418
	public void OnCardManuallyAddedByUser_CheckSuggestions(EntityDef cardEntityDef)
	{
		this.OnCardManuallyAddedByUser_CheckSuggestions(new EntityDef[]
		{
			cardEntityDef
		});
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0005922C File Offset: 0x0005742C
	public void OnCardManuallyAddedByUser_CheckSuggestions(IEnumerable<EntityDef> cardEntityDefs)
	{
		EntityDef entityDef = cardEntityDefs.FirstOrDefault((EntityDef def) => def.IsCollectionManagerFilterManaCostByEven || def.IsCollectionManagerFilterManaCostByOdd);
		if (entityDef == null)
		{
			return;
		}
		CollectionManagerDisplay cmDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		bool flag = entityDef.IsCollectionManagerFilterManaCostByEven && cmDisplay != null && !cmDisplay.IsManaFilterEvenValues;
		bool flag2 = entityDef.IsCollectionManagerFilterManaCostByOdd && cmDisplay != null && !cmDisplay.IsManaFilterOddValues;
		if (flag || flag2)
		{
			string key = flag ? "GLUE_COLLECTION_MANAGER_MANA_FILTER_PROMPT_BODY_EVEN_CARDS" : "GLUE_COLLECTION_MANAGER_MANA_FILTER_PROMPT_BODY_ODD_CARDS";
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_MANAGER_MANA_FILTER_PROMPT_HEADER"),
				m_text = GameStrings.Get(key),
				m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
				m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO"),
				m_showAlertIcon = false,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
				m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					if (response != AlertPopup.Response.CONFIRM || cmDisplay == null)
					{
						return;
					}
					string newSearchText = CollectibleCardFilter.CreateSearchTerm_Mana_OddEven((bool)userData);
					cmDisplay.FilterBySearchText(newSearchText);
				},
				m_responseUserData = flag2
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x00059369 File Offset: 0x00057569
	public int RemoveClosestInvalidCard(EntityDef entityDef, CollectionDeck deck, int sameRemoveCount)
	{
		this.GetCardEventHandler(entityDef.GetCardId()).OnCardRemoved(this, deck);
		return base.GetCardsContent().RemoveClosestInvalidCard(entityDef, sameRemoveCount);
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x0005938C File Offset: 0x0005758C
	public bool SetCardBack(Actor actor)
	{
		CollectionCardBack component = actor.gameObject.GetComponent<CollectionCardBack>();
		return !(component == null) && this.GetCardBackContent().SetNewCardBack(component.GetCardBackId(), actor.gameObject);
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x000593C7 File Offset: 0x000575C7
	public void FlashDeckTemplateHighlight()
	{
		if (this.m_deckTemplateChosenGlow != null)
		{
			this.m_deckTemplateChosenGlow.SendEvent("Flash");
		}
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000593E7 File Offset: 0x000575E7
	public void SetHeroSkin(Actor actor)
	{
		this.GetHeroSkinContent().SetNewHeroSkin(actor);
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000593F8 File Offset: 0x000575F8
	public void HandleAddedCardDeckUpdate(EntityDef entityDef, TAG_PREMIUM premium, int newCount)
	{
		if (!base.IsShowingDeckContents())
		{
			return;
		}
		CollectionDeck editingDeck = base.GetCardsContent().GetEditingDeck();
		if (editingDeck == null)
		{
			Debug.LogWarning("null editing deck returned during HandleAddedCardDeckUpdate");
			return;
		}
		CollectionDeckSlot collectionDeckSlot = editingDeck.FindFirstOwnedSlotByCardId(entityDef.GetCardId(), false);
		int num = 0;
		while (collectionDeckSlot != null && num < newCount)
		{
			this.AddCard(entityDef, premium, base.GetCardsContent().GetCardTileVisual(collectionDeckSlot.Index), true, null, false);
			collectionDeckSlot = editingDeck.FindFirstOwnedSlotByCardId(entityDef.GetCardId(), false);
			num++;
		}
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x00059470 File Offset: 0x00057670
	public bool HandleDeletedCardDeckUpdate(string cardID, TAG_PREMIUM premium)
	{
		if (!base.IsShowingDeckContents())
		{
			return false;
		}
		CollectionDeck editingDeck = base.GetCardsContent().GetEditingDeck();
		this.GetCardEventHandler(cardID).OnCardRemoved(this, editingDeck);
		base.GetCardsContent().UpdateCardList(cardID, true, null, null);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(true);
		return true;
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000594C4 File Offset: 0x000576C4
	public bool RemoveCard(string cardID, TAG_PREMIUM premium, bool valid)
	{
		bool flag = false;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null)
		{
			CollectionCardEventHandler cardEventHandler = this.GetCardEventHandler(cardID);
			flag = editedDeck.RemoveCard(cardID, premium, valid);
			this.HandleDeletedCardDeckUpdate(cardID, premium);
			if (flag)
			{
				cardEventHandler.OnCardRemoved(this, editedDeck);
			}
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			AdventureDungeonCrawlDisplay.Get().SyncDeckList();
			this.UpdateDoneButtonText();
		}
		return flag;
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x00059524 File Offset: 0x00057724
	public bool RemoveAllCopiesOfCard(string cardID)
	{
		bool flag = false;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		for (int i = editedDeck.GetSlots().Count - 1; i >= 0; i--)
		{
			CollectionDeckSlot collectionDeckSlot = editedDeck.GetSlots()[i];
			if (!(collectionDeckSlot.CardID != cardID))
			{
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.NORMAL) > 0)
				{
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.NORMAL, true);
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.NORMAL, false);
				}
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.GOLDEN) > 0)
				{
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.GOLDEN, true);
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.GOLDEN, false);
				}
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.DIAMOND) > 0)
				{
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.DIAMOND, true);
					flag |= this.RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.DIAMOND, false);
				}
			}
		}
		return flag;
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00059608 File Offset: 0x00057808
	public void ShowDeck(CollectionUtils.ViewMode viewMode)
	{
		Log.CollectionManager.Print("mode={0}", new object[]
		{
			viewMode
		});
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && ((viewMode == CollectionUtils.ViewMode.HERO_SKINS && !collectionManagerDisplay.CanViewHeroSkins()) || (viewMode == CollectionUtils.ViewMode.CARD_BACKS && !collectionManagerDisplay.CanViewCardBacks()) || (viewMode == CollectionUtils.ViewMode.COINS && !collectionManagerDisplay.CanViewCoins())))
		{
			viewMode = CollectionUtils.ViewMode.CARDS;
			collectionManagerDisplay.SetViewMode(CollectionUtils.ViewMode.CARDS, null);
		}
		DeckTray.DeckContentTypes contentTypeFromViewMode = this.GetContentTypeFromViewMode(viewMode);
		base.SetTrayMode(contentTypeFromViewMode);
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		editedDeck.ReconcileUnownedCards();
		if (!CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			Navigation.PushUnique(new Navigation.NavigateBackHandler(this.OnBackOutOfDeckContents));
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial(false) && editedDeck.FormatType == FormatType.FT_WILD && collectionManagerDisplay != null && collectionManagerDisplay.ViewModeHasVisibleDeckList())
		{
			collectionManagerDisplay.ShowConvertTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x000596E2 File Offset: 0x000578E2
	public void EnterEditDeckModeForTavernBrawl(CollectionDeck deck)
	{
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnBackOutOfDeckContents));
		this.UpdateDoneButtonText();
		this.m_cardsContent.UpdateCardList();
		this.CheckNumCardsNeededToBuildDeck(deck);
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0005970E File Offset: 0x0005790E
	public void ExitEditDeckModeForTavernBrawl()
	{
		this.UpdateDoneButtonText();
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x00059716 File Offset: 0x00057916
	public void EnterDeckEditForPVPDR(CollectionDeck deck)
	{
		CollectionManager.Get().SetEditedDeck(deck, null);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		collectionManagerDisplay.ShowDuelsDeckHeader();
		collectionManagerDisplay.ShowCurrentEditedDeck();
		this.UpdateDoneButtonText();
	}

	// Token: 0x06000FFD RID: 4093 RVA: 0x00059744 File Offset: 0x00057944
	private void CheckNumCardsNeededToBuildDeck(CollectionDeck deck)
	{
		int num = this.CalculateNumCardsNeededToCraftToReachMinimumDeckSize(deck);
		if (num > 0)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER"),
				m_text = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_NOT_ENOUGH_CARDS", new object[]
				{
					num
				}),
				m_okText = GameStrings.Get("GLOBAL_OKAY"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x000597BD File Offset: 0x000579BD
	public bool IsWaitingToDeleteDeck()
	{
		return this.m_decksContent.IsWaitingToDeleteDeck();
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x000597CA File Offset: 0x000579CA
	public void DeleteEditingDeck(bool popNavigation = true)
	{
		if (popNavigation)
		{
			Navigation.Pop();
		}
		this.m_decksContent.DeleteEditingDeck();
		base.SetTrayMode(DeckTray.DeckContentTypes.Decks);
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000597E6 File Offset: 0x000579E6
	public void CancelRenamingDeck()
	{
		this.m_decksContent.CancelRenameEditingDeck();
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000597F3 File Offset: 0x000579F3
	public void ClearCountLabels()
	{
		this.m_countLabelText.Text = "";
		this.m_countText.Text = "";
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x00059815 File Offset: 0x00057A15
	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID)
	{
		return this.m_cardsContent.GetCardTileVisual(cardID);
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x00059823 File Offset: 0x00057A23
	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID, TAG_PREMIUM premType)
	{
		return this.m_cardsContent.GetCardTileVisual(cardID, premType);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x00059832 File Offset: 0x00057A32
	public DeckTrayDeckTileVisual GetCardTileVisual(int index)
	{
		return this.m_cardsContent.GetCardTileVisual(index);
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x00059840 File Offset: 0x00057A40
	public DeckTrayDeckTileVisual GetOrAddCardTileVisual(int index, bool affectedByScrollbar = true)
	{
		DeckTrayDeckTileVisual orAddCardTileVisual = this.m_cardsContent.GetOrAddCardTileVisual(index);
		if (orAddCardTileVisual == null)
		{
			orAddCardTileVisual = this.m_cardsContent.GetOrAddCardTileVisual(index);
			if (affectedByScrollbar)
			{
				this.m_scrollbar.AddVisibleAffectedObject(orAddCardTileVisual.gameObject, this.m_cardsContent.GetCardVisualExtents(), true, new UIBScrollable.VisibleAffected(DeckTray.OnDeckTrayTileScrollVisibleAffected));
			}
		}
		return orAddCardTileVisual;
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0005989D File Offset: 0x00057A9D
	public void SetMyDecksLabelText(string text)
	{
		this.m_myDecksLabel.Text = text;
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x000598AB File Offset: 0x00057AAB
	public TooltipZone GetTooltipZone()
	{
		return this.m_deckHeaderTooltip;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x000598B3 File Offset: 0x00057AB3
	public CollectionDeckTrayDeckListContent GetDecksContent()
	{
		return this.m_decksContent;
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000598BB File Offset: 0x00057ABB
	public DeckTrayCardBackContent GetCardBackContent()
	{
		return this.m_cardBackContent;
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x000598C3 File Offset: 0x00057AC3
	public DeckTrayHeroSkinContent GetHeroSkinContent()
	{
		return this.m_heroSkinContent;
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x000598CB File Offset: 0x00057ACB
	public void Exit()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.HideUnseenDeckTrays();
		}
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x000598E0 File Offset: 0x00057AE0
	public CollectionDeckBoxVisual GetEditingDeckBox()
	{
		TraySection editingTraySection = this.GetDecksContent().GetEditingTraySection();
		if (editingTraySection == null)
		{
			return null;
		}
		return editingTraySection.m_deckBox;
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x0005990A File Offset: 0x00057B0A
	private void DoneButtonPress(UIEvent e)
	{
		if (this.m_cardBackContent.WaitingForCardbackAnimation)
		{
			base.StartCoroutine(this.CompleteDoneButtonPressAfterAnimations(e));
			return;
		}
		Navigation.GoBack();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0005992E File Offset: 0x00057B2E
	private IEnumerator CompleteDoneButtonPressAfterAnimations(UIEvent e)
	{
		while (this.m_cardBackContent.WaitingForCardbackAnimation)
		{
			yield return null;
		}
		this.DoneButtonPress(e);
		yield break;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00059944 File Offset: 0x00057B44
	public override bool OnBackOutOfDeckContents()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			return this.OnBackOutOfDeckContentsDuel();
		}
		return this.OnBackOutOfDeckContentsImpl(false);
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00059960 File Offset: 0x00057B60
	public bool OnBackOutOfDeckContentsImpl(bool deleteDeck)
	{
		if (base.GetCurrentContentType() != DeckTray.DeckContentTypes.INVALID && base.GetCurrentContent() != null && !base.GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!base.IsShowingDeckContents())
		{
			return false;
		}
		Log.DeckTray.Print("backing out of deck contents " + deleteDeck.ToString(), Array.Empty<object>());
		DeckHelper.Get().Hide(true);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (deleteDeck)
		{
			this.m_decksContent.DeleteDeck(editedDeck.ID);
		}
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.HideNonDeckTemplateTabs(false, false);
		bool flag = true;
		IList<DeckRuleViolation> violations;
		if (deckRuleset != null)
		{
			flag = deckRuleset.IsDeckValid(editedDeck, out violations, Array.Empty<DeckRule.RuleType>());
		}
		else
		{
			violations = new List<DeckRuleViolation>();
		}
		if (!flag && !deleteDeck)
		{
			this.PopupInvalidDeckConfirmation(violations);
		}
		else
		{
			if (editedDeck.FormatType == FormatType.FT_STANDARD && flag && CollectionManager.Get().ShouldShowWildToStandardTutorial(false) && UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, "CollectionDeckTray.OnBackOutOfDeckContentsImpl:ShowSetRotationTutorial"))
			{
				Options.Get().SetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK, false);
				Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, editedDeck.ID);
				Vector3 vector = OverlayUI.Get().GetRelativePosition(this.m_doneButton.transform.position, null, null, 0f);
				vector += (UniversalInputManager.UsePhoneUI ? new Vector3(-56.5f, 0f, 35f) : new Vector3(-30.8f, 0f, 17.8f));
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, vector, NotificationManager.NOTIFICATITON_WORLD_SCALE, GameStrings.Get("GLUE_COLLECTION_TUTORIAL16"), false, NotificationManager.PopupTextType.BASIC);
				notification.ShowPopUpArrow(Notification.PopUpArrowDirection.RightDown);
				notification.PulseReminderEveryXSeconds(3f);
				UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
				this.m_doneButton.GetComponentInChildren<HighlightState>().ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			this.SaveCurrentDeckAndEnterDeckListMode();
		}
		return true;
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x00059B5D File Offset: 0x00057D5D
	public bool OnBackOutOfDeckContentsDuel()
	{
		return AdventureDungeonCrawlDisplay.Get().BackFromDeckEdit(this.m_cardsContent.GetEditingDeck()) && this.OnConfirmBackOutOfDeckContentsDuel();
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x00059B80 File Offset: 0x00057D80
	public bool OnConfirmBackOutOfDeckContentsDuel()
	{
		if (base.GetCurrentContentType() != DeckTray.DeckContentTypes.INVALID && base.GetCurrentContent() != null && !base.GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!base.IsShowingDeckContents())
		{
			return false;
		}
		DeckHelper.Get().Hide(true);
		CollectionManager.Get().DoneEditing();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
			collectionManagerDisplay.OnDoneEditingDeck();
		}
		return true;
	}

	// Token: 0x06001013 RID: 4115 RVA: 0x00059BF8 File Offset: 0x00057DF8
	private void PopupInvalidDeckConfirmation(IList<DeckRuleViolation> violations)
	{
		HashSet<DeckRule.RuleType> hashSet = new HashSet<DeckRule.RuleType>
		{
			DeckRule.RuleType.IS_NOT_ROTATED,
			DeckRule.RuleType.DECK_SIZE,
			DeckRule.RuleType.PLAYER_OWNS_EACH_COPY,
			DeckRule.RuleType.IS_CARD_PLAYABLE
		};
		bool flag = false;
		string text = "";
		for (int i = 0; i < violations.Count; i++)
		{
			DeckRuleViolation deckRuleViolation = violations[i];
			if (hashSet.Contains(deckRuleViolation.Rule.Type))
			{
				flag = true;
				text = text + deckRuleViolation.DisplayError + "\n";
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = GameStrings.Get("GLUE_COLLECTION_DECK_GENERIC_INVALID_MESSAGE");
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS, null);
		if (!Network.IsLoggedIn())
		{
			this.SaveCurrentDeckAndEnterDeckListMode();
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_OFFLINE_SAVE_DECK_HEADER"),
				m_text = GameStrings.Get("GLUE_OFFLINE_SAVE_DECK_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_showAlertIcon = false
			};
			DialogManager.Get().ShowPopup(info);
			return;
		}
		AlertPopup.PopupInfo popupInfo = null;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (this.CalculateNumCardsNeededToCraftToReachMinimumDeckSize(editedDeck) > 0)
		{
			this.SaveCurrentDeckAndEnterDeckListMode();
		}
		else if (flag)
		{
			popupInfo = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER"),
				m_text = text + "\n" + GameStrings.Get("GLUE_COLLECTION_DECK_RULE_FINISH_AUTOMATICALLY"),
				m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_SAVE_ANYWAY"),
				m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_FINISH_FOR_ME"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
				m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					if (response == AlertPopup.Response.CANCEL)
					{
						this.SaveCurrentDeckAndEnterDeckListMode();
						return;
					}
					this.FinishMyDeck(true);
				}
			};
		}
		else
		{
			popupInfo = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER"),
				m_text = text,
				m_okText = GameStrings.Get("GLOBAL_OKAY"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					this.SaveCurrentDeckAndEnterDeckListMode();
				}
			};
		}
		if (popupInfo != null)
		{
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00059DE4 File Offset: 0x00057FE4
	private bool OnBackOutOfCollectionScreen()
	{
		if (this == null || base.gameObject == null)
		{
			return true;
		}
		if (NotificationManager.Get() != null)
		{
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL16"), 0f);
		}
		if (this.m_doneButton != null)
		{
			this.m_doneButton.GetComponentInChildren<HighlightState>().ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (base.GetCurrentContentType() != DeckTray.DeckContentTypes.INVALID && base.GetCurrentContent() != null && !base.GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (base.IsShowingDeckContents() && SceneMgr.Get() != null && !SceneMgr.Get().IsInTavernBrawlMode())
		{
			return false;
		}
		AnimationUtil.DelayedActivate(base.gameObject, 0.25f, false);
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().Exit();
		}
		return true;
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x00059EC8 File Offset: 0x000580C8
	public static void SaveCurrentDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null)
		{
			editedDeck.SendChanges();
			if (Network.IsLoggedIn())
			{
				CollectionManager.Get().SetTimeOfLastPlayerDeckSave(new DateTime?(DateTime.Now));
			}
			Log.Decks.PrintInfo("Finished Editing Deck:", Array.Empty<object>());
			editedDeck.LogDeckStringInformation();
			FiresideGatheringManager.Get().UpdateDeckValidity();
		}
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00059F28 File Offset: 0x00058128
	private void SaveCurrentDeckAndEnterDeckListMode()
	{
		CollectionDeckTray.SaveCurrentDeck();
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			if (TavernBrawlDisplay.Get() != null)
			{
				TavernBrawlDisplay.Get().BackFromDeckEdit(true);
			}
			this.m_cardsContent.UpdateCardList();
			return;
		}
		base.SetTrayMode(DeckTray.DeckContentTypes.Decks);
		CollectionManager.Get().DoneEditing();
		this.UpdateDoneButtonText();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.OnDoneEditingDeck();
		}
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00059FA4 File Offset: 0x000581A4
	public void CompleteMyDeckButtonPress()
	{
		if (!Network.IsLoggedIn())
		{
			CollectionManager.ShowFeatureDisabledWhileOfflinePopup();
			return;
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_HEADER"),
			m_text = GameStrings.Get("GLUE_COLLECTION_DECK_RULE_FINISH_AUTOMATICALLY"),
			m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CONFIRM"),
			m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CANCEL"),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					this.FinishMyDeck(false);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x0005A030 File Offset: 0x00058230
	public void FinishMyDeck(bool backOutWhenComplete)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool allowSmartDeckCompletion = SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER;
		CollectionManager.Get().AutoFillDeck(editedDeck, allowSmartDeckCompletion, delegate(CollectionDeck filledDeck, IEnumerable<DeckMaker.DeckFill> fillCards)
		{
			this.AutoAddCardsAndTryToBackOut(fillCards, filledDeck.GetRuleset(), backOutWhenComplete);
		});
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0005A084 File Offset: 0x00058284
	private void AutoAddCardsAndTryToBackOut(IEnumerable<DeckMaker.DeckFill> fillCards, DeckRuleset deckRuleset, bool backOutWhenComplete)
	{
		CollectionDeckTray.PopuplateDeckCompleteCallback completedCallback = null;
		if (backOutWhenComplete)
		{
			completedCallback = delegate(List<EntityDef> addedCards, List<EntityDef> removedCards)
			{
				this.OnBackOutOfDeckContents();
			};
		}
		base.StartCoroutine(this.AutoAddCardsWithTiming(fillCards, deckRuleset, false, completedCallback));
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x0005A0B4 File Offset: 0x000582B4
	public void PopulateDeck(IEnumerable<DeckMaker.DeckFill> fillCards, CollectionDeckTray.PopuplateDeckCompleteCallback completedCallback)
	{
		CollectionManager.Get().GetEditedDeck().ClearSlotContents();
		base.GetCardsContent().UpdateCardList();
		base.StartCoroutine(this.AutoAddCardsWithTiming(fillCards, null, true, completedCallback));
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0005A0E1 File Offset: 0x000582E1
	private IEnumerator AutoAddCardsWithTiming(IEnumerable<DeckMaker.DeckFill> fillCards, DeckRuleset deckRuleset, bool allowInvalid, CollectionDeckTray.PopuplateDeckCompleteCallback completedCallback)
	{
		base.AllowInput(false);
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(false);
		List<EntityDef> addedCards = null;
		List<EntityDef> removedCards = null;
		if (completedCallback != null)
		{
			addedCards = new List<EntityDef>();
			removedCards = new List<EntityDef>();
		}
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionDeck deck = CollectionManager.Get().GetEditedDeck();
			int maxDeckSize = (deckRuleset == null) ? int.MaxValue : deckRuleset.GetDeckSize(deck);
			foreach (DeckMaker.DeckFill deckFill in fillCards)
			{
				if (!deck.HasReplaceableSlot() && deck.GetTotalCardCount() >= maxDeckSize)
				{
					break;
				}
				EntityDef addCard = deckFill.m_addCard;
				EntityDef removeTemplate = deckFill.m_removeTemplate;
				if (removeTemplate != null)
				{
					bool flag = this.RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.NORMAL, false);
					if (!flag)
					{
						flag = this.RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.GOLDEN, false);
					}
					if (!flag)
					{
						flag = this.RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.DIAMOND, false);
					}
					if (flag && removedCards != null)
					{
						removedCards.Add(removeTemplate);
					}
				}
				if (addCard != null && (false | this.AddCardWithPreferredPremium(addCard, null, true, null, allowInvalid)))
				{
					if (addedCards != null)
					{
						addedCards.Add(addCard);
					}
					yield return new WaitForSeconds(0.2f);
				}
			}
			IEnumerator<DeckMaker.DeckFill> enumerator = null;
			deck = null;
		}
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(true);
		base.AllowInput(true);
		if (completedCallback != null)
		{
			completedCallback(addedCards, removedCards);
		}
		yield break;
		yield break;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x0005A110 File Offset: 0x00058310
	public override void UpdateDoneButtonText()
	{
		bool flag = !CollectionManager.Get().IsInEditMode() || CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			flag = (tavernBrawlDisplay != null && !tavernBrawlDisplay.IsInDeckEditMode() && !UniversalInputManager.UsePhoneUI);
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			flag = (!(AdventureDungeonCrawlDisplay.Get() != null) || !AdventureDungeonCrawlDisplay.Get().GetDuelsDeckIsValid());
		}
		bool flag2 = this.m_backArrow != null;
		if (flag)
		{
			this.m_doneButton.SetText(flag2 ? "" : GameStrings.Get("GLOBAL_BACK"));
			if (flag2)
			{
				this.m_backArrow.gameObject.SetActive(true);
				return;
			}
		}
		else
		{
			this.m_doneButton.SetText(GameStrings.Get("GLOBAL_DONE"));
			if (flag2)
			{
				this.m_backArrow.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0005A20B File Offset: 0x0005840B
	protected override void HideUnseenDeckTrays()
	{
		base.HideUnseenDeckTrays();
		this.m_decksContent.HideTraySectionsNotInBounds(this.m_scrollbar.m_ScrollBounds.bounds);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0005A230 File Offset: 0x00058430
	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.ShowDeckBigCard(cardTile, 0.2f);
			return;
		}
		if (CollectionInputMgr.Get() != null)
		{
			if (SceneMgr.Get().IsInDuelsMode() && DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID()))
			{
				return;
			}
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0005A288 File Offset: 0x00058488
	private void OnCardTileTap(DeckTrayDeckTileVisual cardTile)
	{
		if (cardTile == null || this.m_cardsContent == null)
		{
			return;
		}
		UniversalInputManager universalInputManager = UniversalInputManager.Get();
		CollectionManager collectionManager = CollectionManager.Get();
		CollectionDeck collectionDeck = null;
		CollectibleDisplay collectibleDisplay = null;
		if (collectionManager != null)
		{
			collectionDeck = collectionManager.GetEditedDeck();
			collectibleDisplay = collectionManager.GetCollectibleDisplay();
		}
		if (universalInputManager != null && collectibleDisplay != null && collectionDeck != null && universalInputManager.IsTouchMode() && collectibleDisplay.GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE && !collectionDeck.IsValidSlot(cardTile.GetSlot(), false, false, false))
		{
			this.m_cardsContent.ShowDeckHelper(cardTile, false, true);
		}
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0005A30D File Offset: 0x0005850D
	protected override void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		if (CollectionInputMgr.Get() == null || !CollectionInputMgr.Get().HasHeldCard())
		{
			this.ShowDeckBigCard(cardTile, 0f);
		}
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0005A344 File Offset: 0x00058544
	private void OnCardTileHeld(DeckTrayDeckTileVisual cardTile)
	{
		if (CollectionInputMgr.Get() != null && !TavernBrawlDisplay.IsTavernBrawlViewing() && CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE && CollectionInputMgr.Get().GrabCard(cardTile, true) && this.m_deckBigCard != null)
		{
			this.HideDeckBigCard(cardTile, true);
		}
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0005A39B File Offset: 0x0005859B
	protected override void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID()))
		{
			return;
		}
		this.RemoveCardTile(cardTile, false);
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0005A3B4 File Offset: 0x000585B4
	public void RemoveCardTile(DeckTrayDeckTileVisual cardTile, bool removeAllCopies = false)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			return;
		}
		if (CollectionInputMgr.Get().HasHeldCard())
		{
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.HideDeckBigCard(cardTile, false);
			return;
		}
		if (!editedDeck.IsValidSlot(cardTile.GetSlot(), false, false, false))
		{
			this.m_cardsContent.ShowDeckHelper(cardTile, false, true);
			return;
		}
		if (CollectionInputMgr.Get() == null || TavernBrawlDisplay.IsTavernBrawlViewing())
		{
			return;
		}
		CollectionDeckTileActor actor = cardTile.GetActor();
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(actor.GetSpell(SpellType.SUMMON_IN).gameObject);
		gameObject.transform.position = actor.transform.position + new Vector3(-2f, 0f, 0f);
		gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		gameObject.GetComponent<Spell>().ActivateState(SpellStateType.BIRTH);
		base.StartCoroutine(this.DestroyAfterSeconds(gameObject));
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().RemoveCard(cardTile.GetCardID(), cardTile.GetSlot().UnPreferredPremium, editedDeck.IsValidSlot(cardTile.GetSlot(), false, false, false));
		}
		iTween.MoveTo(gameObject, new Vector3(gameObject.transform.position.x - 10f, gameObject.transform.position.y + 10f, gameObject.transform.position.z), 4f);
		SoundManager.Get().LoadAndPlay("collection_manager_card_remove_from_deck_instant.prefab:bcee588ddfc73844ea3a24beb63bc53f", base.gameObject);
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0005A555 File Offset: 0x00058755
	private IEnumerator DestroyAfterSeconds(GameObject go)
	{
		yield return new WaitForSeconds(5f);
		UnityEngine.Object.Destroy(go);
		yield break;
	}

	// Token: 0x06001025 RID: 4133 RVA: 0x0005A564 File Offset: 0x00058764
	protected override void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard == null)
		{
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		EntityDef entityDef = actor.GetEntityDef();
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), null))
		{
			GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(editedDeck, cardTile.GetSlot());
			this.m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, ghostTypeFromSlot, delay);
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(true);
			}
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null && collectionManagerDisplay.m_deckTemplateCardReplacePopup != null)
			{
				collectionManagerDisplay.m_deckTemplateCardReplacePopup.Shrink(0.1f);
			}
		}
	}

	// Token: 0x06001026 RID: 4134 RVA: 0x0005A64C File Offset: 0x0005884C
	protected override void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard != null)
		{
			if (force)
			{
				this.m_deckBigCard.ForceHide();
			}
			else
			{
				this.m_deckBigCard.Hide(actor.GetEntityDef(), actor.GetPremium());
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(false);
			}
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null && collectionManagerDisplay.m_deckTemplateCardReplacePopup != null)
			{
				collectionManagerDisplay.m_deckTemplateCardReplacePopup.Unshrink(0.1f);
			}
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0005A6E0 File Offset: 0x000588E0
	private void OnCardCountUpdated(int cardCount)
	{
		string text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
		string text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", new object[]
		{
			cardCount,
			CollectionManager.Get().GetDeckSize()
		});
		this.m_countLabelText.Text = text;
		this.m_countText.Text = text2;
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x0005A73C File Offset: 0x0005893C
	private void OnDeckCountUpdated(int deckCount)
	{
		string text = GameStrings.Get("GLUE_DECK_TRAY_DECK_COUNT_LABEL");
		string text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", new object[]
		{
			deckCount,
			27
		});
		this.m_countLabelText.Text = text;
		this.m_countText.Text = text2;
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0005A790 File Offset: 0x00058990
	private void OnDeckCreated(long deckID)
	{
		base.ResetDeckTrayScroll();
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x0005A798 File Offset: 0x00058998
	private void OnCMViewModeChanged(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse)
	{
		DeckTray.DeckContentTypes contentTypeFromViewMode = this.GetContentTypeFromViewMode(mode);
		this.m_cardsContent.ShowFakeDeck(mode == CollectionUtils.ViewMode.DECK_TEMPLATE);
		if (!triggerResponse)
		{
			return;
		}
		this.m_decksContent.UpdateDeckName(null);
		if (this.m_currentContent == DeckTray.DeckContentTypes.Decks)
		{
			return;
		}
		base.SetTrayMode(contentTypeFromViewMode);
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x0005A7DD File Offset: 0x000589DD
	private DeckTray.DeckContentTypes GetContentTypeFromViewMode(CollectionUtils.ViewMode viewMode)
	{
		switch (viewMode)
		{
		case CollectionUtils.ViewMode.HERO_SKINS:
			return DeckTray.DeckContentTypes.HeroSkin;
		case CollectionUtils.ViewMode.CARD_BACKS:
			return DeckTray.DeckContentTypes.CardBack;
		case CollectionUtils.ViewMode.COINS:
			return DeckTray.DeckContentTypes.Coin;
		}
		return DeckTray.DeckContentTypes.Cards;
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x0005A804 File Offset: 0x00058A04
	private void OnHeroAssigned(string cardID)
	{
		this.m_decksContent.UpdateEditingDeckBoxVisual(cardID, null);
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x0005A828 File Offset: 0x00058A28
	private CollectionCardEventHandler GetCardEventHandler(string cardID)
	{
		CollectionDeckTray.CollectionCardEventHandlerData collectionCardEventHandlerData = this.m_cardEventHandlers.Find((CollectionDeckTray.CollectionCardEventHandlerData data) => data.CardID == cardID);
		if (collectionCardEventHandlerData != null)
		{
			if (collectionCardEventHandlerData.GetInstance() == null)
			{
				CollectionCardEventHandler collectionCardEventHandler = UnityEngine.Object.Instantiate<CollectionCardEventHandler>(collectionCardEventHandlerData.CardHandlerPrefab);
				collectionCardEventHandler.transform.parent = base.transform;
				TransformUtil.Identity(collectionCardEventHandler);
				collectionCardEventHandlerData.SetInstance(collectionCardEventHandler);
			}
			return collectionCardEventHandlerData.GetInstance();
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardID, false);
		for (int i = 0; i < this.m_tagCardEventHandlers.Count; i++)
		{
			CollectionDeckTray.CollectionTagEventHandlerData collectionTagEventHandlerData = this.m_tagCardEventHandlers[i];
			if (GameUtils.GetCardTagValue(cardDbId, collectionTagEventHandlerData.Tag) != 0)
			{
				return collectionTagEventHandlerData.cardHandlerInstance;
			}
		}
		return this.m_defaultCardEventHandler;
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x0005A8F0 File Offset: 0x00058AF0
	private int CalculateNumCardsNeededToCraftToReachMinimumDeckSize(CollectionDeck deck)
	{
		if (deck == null)
		{
			Log.CollectionManager.PrintWarning("GetNumCardsNeededToCraftToReachMinimumDeckSize - No deck to check ruleset against.", Array.Empty<object>());
			return 0;
		}
		CollectionDeck collectionDeck = new CollectionDeck();
		collectionDeck.CopyFrom(deck);
		collectionDeck.ClearSlotContents();
		int minimumAllowedDeckSize = collectionDeck.GetRuleset().GetMinimumAllowedDeckSize(collectionDeck);
		IEnumerable<DeckMaker.DeckFill> fillCards = DeckMaker.GetFillCards(collectionDeck, collectionDeck.GetRuleset());
		int num = 0;
		foreach (DeckMaker.DeckFill deckFill in fillCards)
		{
			TAG_PREMIUM preferredPremiumThatCanBeAddedToDeck = this.GetPreferredPremiumThatCanBeAddedToDeck(collectionDeck, deckFill.m_addCard.GetCardId());
			collectionDeck.AddCard(deckFill.m_addCard, preferredPremiumThatCanBeAddedToDeck, false);
			num++;
			if (num >= minimumAllowedDeckSize)
			{
				return 0;
			}
		}
		return minimumAllowedDeckSize - num;
	}

	// Token: 0x04000AC9 RID: 2761
	public CollectionDeckTrayDeckListContent m_decksContent;

	// Token: 0x04000ACA RID: 2762
	public DeckTrayCardBackContent m_cardBackContent;

	// Token: 0x04000ACB RID: 2763
	public DeckTrayHeroSkinContent m_heroSkinContent;

	// Token: 0x04000ACC RID: 2764
	public GameObject m_coinContent;

	// Token: 0x04000ACD RID: 2765
	public GameObject TrayContentsContainer;

	// Token: 0x04000ACE RID: 2766
	public GameObject TrayContentsDuelsBone;

	// Token: 0x04000ACF RID: 2767
	public Transform m_removeCardTutorialBone;

	// Token: 0x04000AD0 RID: 2768
	public PlayMakerFSM m_deckTemplateChosenGlow;

	// Token: 0x04000AD1 RID: 2769
	public List<CollectionDeckTray.CollectionCardEventHandlerData> m_cardEventHandlers;

	// Token: 0x04000AD2 RID: 2770
	public List<CollectionDeckTray.CollectionTagEventHandlerData> m_tagCardEventHandlers;

	// Token: 0x04000AD3 RID: 2771
	private CollectionCardEventHandler m_defaultCardEventHandler;

	// Token: 0x04000AD4 RID: 2772
	private static CollectionDeckTray s_instance;

	// Token: 0x02001434 RID: 5172
	[Serializable]
	public class CollectionCardEventHandlerData
	{
		// Token: 0x0600D9FD RID: 55805 RVA: 0x003F1001 File Offset: 0x003EF201
		public CollectionCardEventHandler GetInstance()
		{
			return this.cardHandlerInstance;
		}

		// Token: 0x0600D9FE RID: 55806 RVA: 0x003F1009 File Offset: 0x003EF209
		public void SetInstance(CollectionCardEventHandler instance)
		{
			this.cardHandlerInstance = instance;
		}

		// Token: 0x0400A948 RID: 43336
		public string CardID;

		// Token: 0x0400A949 RID: 43337
		public CollectionCardEventHandler CardHandlerPrefab;

		// Token: 0x0400A94A RID: 43338
		private CollectionCardEventHandler cardHandlerInstance;
	}

	// Token: 0x02001435 RID: 5173
	[Serializable]
	public class CollectionTagEventHandlerData
	{
		// Token: 0x0400A94B RID: 43339
		public GAME_TAG Tag;

		// Token: 0x0400A94C RID: 43340
		public CollectionCardEventHandler cardHandlerInstance;
	}

	// Token: 0x02001436 RID: 5174
	// (Invoke) Token: 0x0600DA02 RID: 55810
	public delegate void PopuplateDeckCompleteCallback(List<EntityDef> addedCards, List<EntityDef> removedCards);
}
