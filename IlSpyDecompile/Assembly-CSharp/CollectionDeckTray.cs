using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class CollectionDeckTray : EditableDeckTray
{
	[Serializable]
	public class CollectionCardEventHandlerData
	{
		public string CardID;

		public CollectionCardEventHandler CardHandlerPrefab;

		private CollectionCardEventHandler cardHandlerInstance;

		public CollectionCardEventHandler GetInstance()
		{
			return cardHandlerInstance;
		}

		public void SetInstance(CollectionCardEventHandler instance)
		{
			cardHandlerInstance = instance;
		}
	}

	[Serializable]
	public class CollectionTagEventHandlerData
	{
		public GAME_TAG Tag;

		public CollectionCardEventHandler cardHandlerInstance;
	}

	public delegate void PopuplateDeckCompleteCallback(List<EntityDef> addedCards, List<EntityDef> removedCards);

	public CollectionDeckTrayDeckListContent m_decksContent;

	public DeckTrayCardBackContent m_cardBackContent;

	public DeckTrayHeroSkinContent m_heroSkinContent;

	public GameObject m_coinContent;

	public GameObject TrayContentsContainer;

	public GameObject TrayContentsDuelsBone;

	public Transform m_removeCardTutorialBone;

	public PlayMakerFSM m_deckTemplateChosenGlow;

	public List<CollectionCardEventHandlerData> m_cardEventHandlers;

	public List<CollectionTagEventHandlerData> m_tagCardEventHandlers;

	private CollectionCardEventHandler m_defaultCardEventHandler;

	private static CollectionDeckTray s_instance;

	private void Awake()
	{
		s_instance = this;
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		if (m_scrollbar != null)
		{
			if (SceneMgr.Get().IsInTavernBrawlMode() && !UniversalInputManager.UsePhoneUI)
			{
				Vector3 center = m_scrollbar.m_ScrollBounds.center;
				center.z = 3f;
				m_scrollbar.m_ScrollBounds.center = center;
				Vector3 size = m_scrollbar.m_ScrollBounds.size;
				size.z = 47.67f;
				m_scrollbar.m_ScrollBounds.size = size;
				if (m_cardsContent != null && m_cardsContent.m_deckCompleteHighlight != null)
				{
					Vector3 localPosition = m_cardsContent.m_deckCompleteHighlight.transform.localPosition;
					localPosition.z = -34.15f;
					m_cardsContent.m_deckCompleteHighlight.transform.localPosition = localPosition;
				}
			}
			m_scrollbar.Enable(enable: false);
			m_scrollbar.AddTouchScrollStartedListener(base.OnTouchScrollStarted);
			m_scrollbar.AddTouchScrollEndedListener(base.OnTouchScrollEnded);
		}
		m_contents[DeckContentTypes.Decks] = m_decksContent;
		m_contents[DeckContentTypes.Cards] = m_cardsContent;
		if (m_heroSkinContent != null)
		{
			m_contents[DeckContentTypes.HeroSkin] = m_heroSkinContent;
			m_heroSkinContent.RegisterHeroAssignedListener(OnHeroAssigned);
		}
		if (m_cardBackContent != null)
		{
			m_contents[DeckContentTypes.CardBack] = m_cardBackContent;
		}
		m_cardsContent.RegisterCardTileHeldListener(OnCardTileHeld);
		m_cardsContent.RegisterCardTilePressListener(OnCardTilePress);
		m_cardsContent.RegisterCardTileTapListener(OnCardTileTap);
		m_cardsContent.RegisterCardTileOverListener(OnCardTileOver);
		m_cardsContent.RegisterCardTileOutListener(OnCardTileOut);
		m_cardsContent.RegisterCardTileReleaseListener(OnCardTileRelease);
		m_cardsContent.RegisterCardCountUpdated(OnCardCountUpdated);
		m_decksContent.RegisterBusyWithDeck(base.OnBusyWithDeck);
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
			m_decksContent.RegisterDeckCountUpdated(OnDeckCountUpdated);
		}
		SetMyDecksLabelText(GameStrings.Get(key));
		m_doneButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		m_doneButton.AddEventListener(UIEventType.RELEASE, DoneButtonPress);
		CollectionManager.Get().RegisterEditedDeckChanged(base.OnEditedDeckChanged);
		SceneMgr.Get().RegisterScenePreUnloadEvent(OnScenePreUnload);
		CollectionInputMgr.Get().SetScrollbar(m_scrollbar);
		foreach (DeckContentScroll scrollable in m_scrollables)
		{
			scrollable.SaveStartPosition();
		}
		m_defaultCardEventHandler = base.gameObject.AddComponent<CollectionCardEventHandler>();
		if (SceneMgr.Get().IsInDuelsMode() && TrayContentsContainer != null && TrayContentsDuelsBone != null)
		{
			TrayContentsContainer.transform.localPosition = TrayContentsDuelsBone.transform.localPosition;
		}
	}

	protected override void Start()
	{
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(playSound: true);
		CollectionManager.Get().GetCollectibleDisplay().RegisterSwitchViewModeListener(OnCMViewModeChanged);
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING && !SceneMgr.Get().IsInDuelsMode())
		{
			Navigation.Push(OnBackOutOfCollectionScreen);
		}
		CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreated);
		base.Start();
	}

	private void OnDestroy()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.RemoveEditedDeckChanged(base.OnEditedDeckChanged);
			collectionManager.DoneEditing();
		}
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(OnScenePreUnload);
		}
		s_instance = null;
	}

	public bool CanPickupCard()
	{
		DeckContentTypes currentContentType = GetCurrentContentType();
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if ((currentContentType != DeckContentTypes.Cards || viewMode != 0) && (currentContentType != DeckContentTypes.CardBack || viewMode != CollectionUtils.ViewMode.CARD_BACKS) && (currentContentType != DeckContentTypes.HeroSkin || viewMode != CollectionUtils.ViewMode.HERO_SKINS))
		{
			if (currentContentType == DeckContentTypes.Coin)
			{
				return viewMode == CollectionUtils.ViewMode.COINS;
			}
			return false;
		}
		return true;
	}

	public static CollectionDeckTray Get()
	{
		return s_instance;
	}

	public void Unload()
	{
		CollectionManager.Get().RemoveDeckCreatedListener(OnDeckCreated);
		CollectionInputMgr.Get().SetScrollbar(null);
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (UniversalInputManager.Get() != null && UniversalInputManager.Get().IsTextInputActive())
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, force: true);
		}
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionManager.Get().GetEditedDeck()?.SendChanges();
		}
		Exit();
	}

	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, Actor animateActor = null, bool allowInvalid = false)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		CollectionCardEventHandler cardEventHandler = GetCardEventHandler(cardEntityDef.GetCardId());
		bool updateVisuals = cardEventHandler.ShouldUpdateVisuals();
		EntityDef removedCard = null;
		bool num = GetCardsContent().AddCard(cardEntityDef, premium, deckTileToRemove, playSound, ref removedCard, animateActor, allowInvalid, updateVisuals);
		if (num)
		{
			cardEventHandler.OnCardAdded(this, editedDeck, cardEntityDef, premium, animateActor);
		}
		if (removedCard != null)
		{
			GetCardEventHandler(removedCard.GetCardId()).OnCardRemoved(this, editedDeck);
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			AdventureDungeonCrawlDisplay.Get().SyncDeckList();
			UpdateDoneButtonText();
		}
		return num;
	}

	private bool AddCardWithPreferredPremium(EntityDef cardEntityDef, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, Actor animateActor = null, bool allowInvalid = false)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		TAG_PREMIUM preferredPremiumThatCanBeAddedToDeck = GetPreferredPremiumThatCanBeAddedToDeck(editedDeck, cardEntityDef.GetCardId());
		return AddCard(cardEntityDef, preferredPremiumThatCanBeAddedToDeck, deckTileToRemove, playSound, animateActor, allowInvalid);
	}

	public TAG_PREMIUM GetPreferredPremiumThatCanBeAddedToDeck(CollectionDeck deck, string cardId)
	{
		TAG_PREMIUM tAG_PREMIUM = TAG_PREMIUM.DIAMOND;
		if (!deck.CanAddOwnedCard(cardId, tAG_PREMIUM))
		{
			tAG_PREMIUM = TAG_PREMIUM.GOLDEN;
			if (!deck.CanAddOwnedCard(cardId, tAG_PREMIUM))
			{
				tAG_PREMIUM = TAG_PREMIUM.NORMAL;
			}
		}
		return tAG_PREMIUM;
	}

	public void OnCardManuallyAddedByUser_CheckSuggestions(EntityDef cardEntityDef)
	{
		OnCardManuallyAddedByUser_CheckSuggestions(new EntityDef[1] { cardEntityDef });
	}

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
		if (!(flag || flag2))
		{
			return;
		}
		string key = (flag ? "GLUE_COLLECTION_MANAGER_MANA_FILTER_PROMPT_BODY_EVEN_CARDS" : "GLUE_COLLECTION_MANAGER_MANA_FILTER_PROMPT_BODY_ODD_CARDS");
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
				if (response == AlertPopup.Response.CONFIRM && !(cmDisplay == null))
				{
					string newSearchText = CollectibleCardFilter.CreateSearchTerm_Mana_OddEven((bool)userData);
					cmDisplay.FilterBySearchText(newSearchText);
				}
			},
			m_responseUserData = flag2
		};
		DialogManager.Get().ShowPopup(info);
	}

	public int RemoveClosestInvalidCard(EntityDef entityDef, CollectionDeck deck, int sameRemoveCount)
	{
		GetCardEventHandler(entityDef.GetCardId()).OnCardRemoved(this, deck);
		return GetCardsContent().RemoveClosestInvalidCard(entityDef, sameRemoveCount);
	}

	public bool SetCardBack(Actor actor)
	{
		CollectionCardBack component = actor.gameObject.GetComponent<CollectionCardBack>();
		if (component == null)
		{
			return false;
		}
		return GetCardBackContent().SetNewCardBack(component.GetCardBackId(), actor.gameObject);
	}

	public void FlashDeckTemplateHighlight()
	{
		if (m_deckTemplateChosenGlow != null)
		{
			m_deckTemplateChosenGlow.SendEvent("Flash");
		}
	}

	public void SetHeroSkin(Actor actor)
	{
		GetHeroSkinContent().SetNewHeroSkin(actor);
	}

	public void HandleAddedCardDeckUpdate(EntityDef entityDef, TAG_PREMIUM premium, int newCount)
	{
		if (!IsShowingDeckContents())
		{
			return;
		}
		CollectionDeck editingDeck = GetCardsContent().GetEditingDeck();
		if (editingDeck == null)
		{
			Debug.LogWarning("null editing deck returned during HandleAddedCardDeckUpdate");
			return;
		}
		CollectionDeckSlot collectionDeckSlot = editingDeck.FindFirstOwnedSlotByCardId(entityDef.GetCardId(), owned: false);
		int num = 0;
		while (collectionDeckSlot != null && num < newCount)
		{
			AddCard(entityDef, premium, GetCardsContent().GetCardTileVisual(collectionDeckSlot.Index), playSound: true);
			collectionDeckSlot = editingDeck.FindFirstOwnedSlotByCardId(entityDef.GetCardId(), owned: false);
			num++;
		}
	}

	public bool HandleDeletedCardDeckUpdate(string cardID, TAG_PREMIUM premium)
	{
		if (!IsShowingDeckContents())
		{
			return false;
		}
		CollectionDeck editingDeck = GetCardsContent().GetEditingDeck();
		GetCardEventHandler(cardID).OnCardRemoved(this, editingDeck);
		GetCardsContent().UpdateCardList(cardID);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(playSound: true);
		return true;
	}

	public bool RemoveCard(string cardID, TAG_PREMIUM premium, bool valid)
	{
		bool flag = false;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null)
		{
			CollectionCardEventHandler cardEventHandler = GetCardEventHandler(cardID);
			flag = editedDeck.RemoveCard(cardID, premium, valid);
			HandleDeletedCardDeckUpdate(cardID, premium);
			if (flag)
			{
				cardEventHandler.OnCardRemoved(this, editedDeck);
			}
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			AdventureDungeonCrawlDisplay.Get().SyncDeckList();
			UpdateDoneButtonText();
		}
		return flag;
	}

	public bool RemoveAllCopiesOfCard(string cardID)
	{
		bool flag = false;
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		for (int num = editedDeck.GetSlots().Count - 1; num >= 0; num--)
		{
			CollectionDeckSlot collectionDeckSlot = editedDeck.GetSlots()[num];
			if (!(collectionDeckSlot.CardID != cardID))
			{
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.NORMAL) > 0)
				{
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.NORMAL, valid: true);
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.NORMAL, valid: false);
				}
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.GOLDEN) > 0)
				{
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.GOLDEN, valid: true);
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.GOLDEN, valid: false);
				}
				while (collectionDeckSlot.GetCount(TAG_PREMIUM.DIAMOND) > 0)
				{
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.DIAMOND, valid: true);
					flag |= RemoveCard(collectionDeckSlot.CardID, TAG_PREMIUM.DIAMOND, valid: false);
				}
			}
		}
		return flag;
	}

	public void ShowDeck(CollectionUtils.ViewMode viewMode)
	{
		Log.CollectionManager.Print("mode={0}", viewMode);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && ((viewMode == CollectionUtils.ViewMode.HERO_SKINS && !collectionManagerDisplay.CanViewHeroSkins()) || (viewMode == CollectionUtils.ViewMode.CARD_BACKS && !collectionManagerDisplay.CanViewCardBacks()) || (viewMode == CollectionUtils.ViewMode.COINS && !collectionManagerDisplay.CanViewCoins())))
		{
			viewMode = CollectionUtils.ViewMode.CARDS;
			collectionManagerDisplay.SetViewMode(CollectionUtils.ViewMode.CARDS);
		}
		DeckContentTypes contentTypeFromViewMode = GetContentTypeFromViewMode(viewMode);
		SetTrayMode(contentTypeFromViewMode);
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		editedDeck.ReconcileUnownedCards();
		if (!CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			Navigation.PushUnique(OnBackOutOfDeckContents);
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial(checkPrevSceneIsPlayMode: false) && editedDeck.FormatType == FormatType.FT_WILD && collectionManagerDisplay != null && collectionManagerDisplay.ViewModeHasVisibleDeckList())
		{
			collectionManagerDisplay.ShowConvertTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
	}

	public void EnterEditDeckModeForTavernBrawl(CollectionDeck deck)
	{
		Navigation.Push(OnBackOutOfDeckContents);
		UpdateDoneButtonText();
		m_cardsContent.UpdateCardList();
		CheckNumCardsNeededToBuildDeck(deck);
	}

	public void ExitEditDeckModeForTavernBrawl()
	{
		UpdateDoneButtonText();
	}

	public void EnterDeckEditForPVPDR(CollectionDeck deck)
	{
		CollectionManager.Get().SetEditedDeck(deck);
		CollectionManagerDisplay obj = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		obj.ShowDuelsDeckHeader();
		obj.ShowCurrentEditedDeck();
		UpdateDoneButtonText();
	}

	private void CheckNumCardsNeededToBuildDeck(CollectionDeck deck)
	{
		int num = CalculateNumCardsNeededToCraftToReachMinimumDeckSize(deck);
		if (num > 0)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER");
			popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_DECK_RULE_NOT_ENOUGH_CARDS", num);
			popupInfo.m_okText = GameStrings.Get("GLOBAL_OKAY");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			AlertPopup.PopupInfo info = popupInfo;
			DialogManager.Get().ShowPopup(info);
		}
	}

	public bool IsWaitingToDeleteDeck()
	{
		return m_decksContent.IsWaitingToDeleteDeck();
	}

	public void DeleteEditingDeck(bool popNavigation = true)
	{
		if (popNavigation)
		{
			Navigation.Pop();
		}
		m_decksContent.DeleteEditingDeck();
		SetTrayMode(DeckContentTypes.Decks);
	}

	public void CancelRenamingDeck()
	{
		m_decksContent.CancelRenameEditingDeck();
	}

	public void ClearCountLabels()
	{
		m_countLabelText.Text = "";
		m_countText.Text = "";
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID)
	{
		return m_cardsContent.GetCardTileVisual(cardID);
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID, TAG_PREMIUM premType)
	{
		return m_cardsContent.GetCardTileVisual(cardID, premType);
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(int index)
	{
		return m_cardsContent.GetCardTileVisual(index);
	}

	public DeckTrayDeckTileVisual GetOrAddCardTileVisual(int index, bool affectedByScrollbar = true)
	{
		DeckTrayDeckTileVisual orAddCardTileVisual = m_cardsContent.GetOrAddCardTileVisual(index);
		if (orAddCardTileVisual == null)
		{
			orAddCardTileVisual = m_cardsContent.GetOrAddCardTileVisual(index);
			if (affectedByScrollbar)
			{
				m_scrollbar.AddVisibleAffectedObject(orAddCardTileVisual.gameObject, m_cardsContent.GetCardVisualExtents(), visible: true, DeckTray.OnDeckTrayTileScrollVisibleAffected);
			}
		}
		return orAddCardTileVisual;
	}

	public void SetMyDecksLabelText(string text)
	{
		m_myDecksLabel.Text = text;
	}

	public TooltipZone GetTooltipZone()
	{
		return m_deckHeaderTooltip;
	}

	public CollectionDeckTrayDeckListContent GetDecksContent()
	{
		return m_decksContent;
	}

	public DeckTrayCardBackContent GetCardBackContent()
	{
		return m_cardBackContent;
	}

	public DeckTrayHeroSkinContent GetHeroSkinContent()
	{
		return m_heroSkinContent;
	}

	public void Exit()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			HideUnseenDeckTrays();
		}
	}

	public CollectionDeckBoxVisual GetEditingDeckBox()
	{
		TraySection editingTraySection = GetDecksContent().GetEditingTraySection();
		if (editingTraySection == null)
		{
			return null;
		}
		return editingTraySection.m_deckBox;
	}

	private void DoneButtonPress(UIEvent e)
	{
		if (m_cardBackContent.WaitingForCardbackAnimation)
		{
			StartCoroutine(CompleteDoneButtonPressAfterAnimations(e));
		}
		else
		{
			Navigation.GoBack();
		}
	}

	private IEnumerator CompleteDoneButtonPressAfterAnimations(UIEvent e)
	{
		while (m_cardBackContent.WaitingForCardbackAnimation)
		{
			yield return null;
		}
		DoneButtonPress(e);
	}

	public override bool OnBackOutOfDeckContents()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			return OnBackOutOfDeckContentsDuel();
		}
		return OnBackOutOfDeckContentsImpl(deleteDeck: false);
	}

	public bool OnBackOutOfDeckContentsImpl(bool deleteDeck)
	{
		if (GetCurrentContentType() != DeckContentTypes.INVALID && GetCurrentContent() != null && !GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!IsShowingDeckContents())
		{
			return false;
		}
		Log.DeckTray.Print("backing out of deck contents " + deleteDeck);
		DeckHelper.Get().Hide();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (deleteDeck)
		{
			m_decksContent.DeleteDeck(editedDeck.ID);
		}
		DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.HideNonDeckTemplateTabs(hide: false);
		bool flag = true;
		IList<DeckRuleViolation> violations;
		if (deckRuleset != null)
		{
			flag = deckRuleset.IsDeckValid(editedDeck, out violations);
		}
		else
		{
			violations = new List<DeckRuleViolation>();
		}
		if (!flag && !deleteDeck)
		{
			PopupInvalidDeckConfirmation(violations);
		}
		else
		{
			if (editedDeck.FormatType == FormatType.FT_STANDARD && flag && CollectionManager.Get().ShouldShowWildToStandardTutorial(checkPrevSceneIsPlayMode: false) && UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, "CollectionDeckTray.OnBackOutOfDeckContentsImpl:ShowSetRotationTutorial"))
			{
				Options.Get().SetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK, val: false);
				Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, editedDeck.ID);
				Vector3 relativePosition = OverlayUI.Get().GetRelativePosition(m_doneButton.transform.position);
				relativePosition += (UniversalInputManager.UsePhoneUI ? new Vector3(-56.5f, 0f, 35f) : new Vector3(-30.8f, 0f, 17.8f));
				Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, relativePosition, NotificationManager.NOTIFICATITON_WORLD_SCALE, GameStrings.Get("GLUE_COLLECTION_TUTORIAL16"), convertLegacyPosition: false);
				notification.ShowPopUpArrow(Notification.PopUpArrowDirection.RightDown);
				notification.PulseReminderEveryXSeconds(3f);
				UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
				m_doneButton.GetComponentInChildren<HighlightState>().ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			SaveCurrentDeckAndEnterDeckListMode();
		}
		return true;
	}

	public bool OnBackOutOfDeckContentsDuel()
	{
		if (AdventureDungeonCrawlDisplay.Get().BackFromDeckEdit(m_cardsContent.GetEditingDeck()))
		{
			return OnConfirmBackOutOfDeckContentsDuel();
		}
		return false;
	}

	public bool OnConfirmBackOutOfDeckContentsDuel()
	{
		if (GetCurrentContentType() != DeckContentTypes.INVALID && GetCurrentContent() != null && !GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!IsShowingDeckContents())
		{
			return false;
		}
		DeckHelper.Get().Hide();
		CollectionManager.Get().DoneEditing();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideConvertTutorial();
			collectionManagerDisplay.OnDoneEditingDeck();
		}
		return true;
	}

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
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS);
		if (!Network.IsLoggedIn())
		{
			SaveCurrentDeckAndEnterDeckListMode();
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
		if (CalculateNumCardsNeededToCraftToReachMinimumDeckSize(editedDeck) <= 0)
		{
			popupInfo = ((!flag) ? new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER"),
				m_text = text,
				m_okText = GameStrings.Get("GLOBAL_OKAY"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = delegate
				{
					SaveCurrentDeckAndEnterDeckListMode();
				}
			} : new AlertPopup.PopupInfo
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
						SaveCurrentDeckAndEnterDeckListMode();
					}
					else
					{
						FinishMyDeck(backOutWhenComplete: true);
					}
				}
			});
		}
		else
		{
			SaveCurrentDeckAndEnterDeckListMode();
		}
		if (popupInfo != null)
		{
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private bool OnBackOutOfCollectionScreen()
	{
		if (this == null || base.gameObject == null)
		{
			return true;
		}
		if (NotificationManager.Get() != null)
		{
			NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL16"));
		}
		if (m_doneButton != null)
		{
			m_doneButton.GetComponentInChildren<HighlightState>().ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (GetCurrentContentType() != DeckContentTypes.INVALID && GetCurrentContent() != null && !GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (IsShowingDeckContents() && SceneMgr.Get() != null && !SceneMgr.Get().IsInTavernBrawlMode())
		{
			return false;
		}
		AnimationUtil.DelayedActivate(base.gameObject, 0.25f, activate: false);
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().Exit();
		}
		return true;
	}

	public static void SaveCurrentDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null)
		{
			editedDeck.SendChanges();
			if (Network.IsLoggedIn())
			{
				CollectionManager.Get().SetTimeOfLastPlayerDeckSave(DateTime.Now);
			}
			Log.Decks.PrintInfo("Finished Editing Deck:");
			editedDeck.LogDeckStringInformation();
			FiresideGatheringManager.Get().UpdateDeckValidity();
		}
	}

	private void SaveCurrentDeckAndEnterDeckListMode()
	{
		SaveCurrentDeck();
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			if (TavernBrawlDisplay.Get() != null)
			{
				TavernBrawlDisplay.Get().BackFromDeckEdit(animate: true);
			}
			m_cardsContent.UpdateCardList();
			return;
		}
		SetTrayMode(DeckContentTypes.Decks);
		CollectionManager.Get().DoneEditing();
		UpdateDoneButtonText();
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.OnDoneEditingDeck();
		}
	}

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
					FinishMyDeck(backOutWhenComplete: false);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	public void FinishMyDeck(bool backOutWhenComplete)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool allowSmartDeckCompletion = SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER;
		CollectionManager.Get().AutoFillDeck(editedDeck, allowSmartDeckCompletion, delegate(CollectionDeck filledDeck, IEnumerable<DeckMaker.DeckFill> fillCards)
		{
			AutoAddCardsAndTryToBackOut(fillCards, filledDeck.GetRuleset(), backOutWhenComplete);
		});
	}

	private void AutoAddCardsAndTryToBackOut(IEnumerable<DeckMaker.DeckFill> fillCards, DeckRuleset deckRuleset, bool backOutWhenComplete)
	{
		PopuplateDeckCompleteCallback completedCallback = null;
		if (backOutWhenComplete)
		{
			completedCallback = delegate
			{
				OnBackOutOfDeckContents();
			};
		}
		StartCoroutine(AutoAddCardsWithTiming(fillCards, deckRuleset, allowInvalid: false, completedCallback));
	}

	public void PopulateDeck(IEnumerable<DeckMaker.DeckFill> fillCards, PopuplateDeckCompleteCallback completedCallback)
	{
		CollectionManager.Get().GetEditedDeck().ClearSlotContents();
		GetCardsContent().UpdateCardList();
		StartCoroutine(AutoAddCardsWithTiming(fillCards, null, allowInvalid: true, completedCallback));
	}

	private IEnumerator AutoAddCardsWithTiming(IEnumerable<DeckMaker.DeckFill> fillCards, DeckRuleset deckRuleset, bool allowInvalid, PopuplateDeckCompleteCallback completedCallback)
	{
		AllowInput(allowed: false);
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: false);
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
			int maxDeckSize = deckRuleset?.GetDeckSize(deck) ?? int.MaxValue;
			foreach (DeckMaker.DeckFill fillCard in fillCards)
			{
				if (!deck.HasReplaceableSlot() && deck.GetTotalCardCount() >= maxDeckSize)
				{
					break;
				}
				EntityDef addCard = fillCard.m_addCard;
				EntityDef removeTemplate = fillCard.m_removeTemplate;
				if (removeTemplate != null)
				{
					bool flag = RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.NORMAL, valid: false);
					if (!flag)
					{
						flag = RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.GOLDEN, valid: false);
					}
					if (!flag)
					{
						flag = RemoveCard(removeTemplate.GetCardId(), TAG_PREMIUM.DIAMOND, valid: false);
					}
					if (flag)
					{
						removedCards?.Add(removeTemplate);
					}
				}
				if (addCard != null && (false | AddCardWithPreferredPremium(addCard, null, playSound: true, null, allowInvalid)))
				{
					addedCards?.Add(addCard);
					yield return new WaitForSeconds(0.2f);
				}
			}
		}
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: true);
		AllowInput(allowed: true);
		completedCallback?.Invoke(addedCards, removedCards);
	}

	public override void UpdateDoneButtonText()
	{
		bool flag = !CollectionManager.Get().IsInEditMode() || CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			TavernBrawlDisplay tavernBrawlDisplay = TavernBrawlDisplay.Get();
			flag = tavernBrawlDisplay != null && !tavernBrawlDisplay.IsInDeckEditMode() && !UniversalInputManager.UsePhoneUI;
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			flag = ((!(AdventureDungeonCrawlDisplay.Get() != null) || !AdventureDungeonCrawlDisplay.Get().GetDuelsDeckIsValid()) ? true : false);
		}
		bool flag2 = m_backArrow != null;
		if (flag)
		{
			m_doneButton.SetText(flag2 ? "" : GameStrings.Get("GLOBAL_BACK"));
			if (flag2)
			{
				m_backArrow.gameObject.SetActive(value: true);
			}
		}
		else
		{
			m_doneButton.SetText(GameStrings.Get("GLOBAL_DONE"));
			if (flag2)
			{
				m_backArrow.gameObject.SetActive(value: false);
			}
		}
	}

	protected override void HideUnseenDeckTrays()
	{
		base.HideUnseenDeckTrays();
		m_decksContent.HideTraySectionsNotInBounds(m_scrollbar.m_ScrollBounds.bounds);
	}

	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile, 0.2f);
		}
		else if (CollectionInputMgr.Get() != null && (!SceneMgr.Get().IsInDuelsMode() || !DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID())))
		{
			HideDeckBigCard(cardTile);
		}
	}

	private void OnCardTileTap(DeckTrayDeckTileVisual cardTile)
	{
		if (!(cardTile == null) && !(m_cardsContent == null))
		{
			UniversalInputManager universalInputManager = UniversalInputManager.Get();
			CollectionManager collectionManager = CollectionManager.Get();
			CollectionDeck collectionDeck = null;
			CollectibleDisplay collectibleDisplay = null;
			if (collectionManager != null)
			{
				collectionDeck = collectionManager.GetEditedDeck();
				collectibleDisplay = collectionManager.GetCollectibleDisplay();
			}
			if (universalInputManager != null && collectibleDisplay != null && collectionDeck != null && universalInputManager.IsTouchMode() && collectibleDisplay.GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE && !collectionDeck.IsValidSlot(cardTile.GetSlot()))
			{
				m_cardsContent.ShowDeckHelper(cardTile, continueAfterReplace: false, replacingCard: true);
			}
		}
	}

	protected override void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (!UniversalInputManager.Get().IsTouchMode() && (CollectionInputMgr.Get() == null || !CollectionInputMgr.Get().HasHeldCard()))
		{
			ShowDeckBigCard(cardTile);
		}
	}

	private void OnCardTileHeld(DeckTrayDeckTileVisual cardTile)
	{
		if (CollectionInputMgr.Get() != null && !TavernBrawlDisplay.IsTavernBrawlViewing() && CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE && CollectionInputMgr.Get().GrabCard(cardTile) && m_deckBigCard != null)
		{
			HideDeckBigCard(cardTile, force: true);
		}
	}

	protected override void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (!DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID()))
		{
			RemoveCardTile(cardTile);
		}
	}

	public void RemoveCardTile(DeckTrayDeckTileVisual cardTile, bool removeAllCopies = false)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE || CollectionInputMgr.Get().HasHeldCard())
		{
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (UniversalInputManager.Get().IsTouchMode())
		{
			HideDeckBigCard(cardTile);
		}
		else if (!editedDeck.IsValidSlot(cardTile.GetSlot()))
		{
			m_cardsContent.ShowDeckHelper(cardTile, continueAfterReplace: false, replacingCard: true);
		}
		else if (!(CollectionInputMgr.Get() == null) && !TavernBrawlDisplay.IsTavernBrawlViewing())
		{
			CollectionDeckTileActor actor = cardTile.GetActor();
			GameObject gameObject = UnityEngine.Object.Instantiate(actor.GetSpell(SpellType.SUMMON_IN).gameObject);
			gameObject.transform.position = actor.transform.position + new Vector3(-2f, 0f, 0f);
			gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
			gameObject.GetComponent<Spell>().ActivateState(SpellStateType.BIRTH);
			StartCoroutine(DestroyAfterSeconds(gameObject));
			if (Get() != null)
			{
				Get().RemoveCard(cardTile.GetCardID(), cardTile.GetSlot().UnPreferredPremium, editedDeck.IsValidSlot(cardTile.GetSlot()));
			}
			iTween.MoveTo(gameObject, new Vector3(gameObject.transform.position.x - 10f, gameObject.transform.position.y + 10f, gameObject.transform.position.z), 4f);
			SoundManager.Get().LoadAndPlay("collection_manager_card_remove_from_deck_instant.prefab:bcee588ddfc73844ea3a24beb63bc53f", base.gameObject);
		}
	}

	private IEnumerator DestroyAfterSeconds(GameObject go)
	{
		yield return new WaitForSeconds(5f);
		UnityEngine.Object.Destroy(go);
	}

	protected override void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard == null)
		{
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		EntityDef entityDef = actor.GetEntityDef();
		using DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId());
		GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(editedDeck, cardTile.GetSlot());
		m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, ghostTypeFromSlot, delay);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			cardTile.SetHighlight(highlight: true);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && collectionManagerDisplay.m_deckTemplateCardReplacePopup != null)
		{
			collectionManagerDisplay.m_deckTemplateCardReplacePopup.Shrink(0.1f);
		}
	}

	protected override void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard != null)
		{
			if (force)
			{
				m_deckBigCard.ForceHide();
			}
			else
			{
				m_deckBigCard.Hide(actor.GetEntityDef(), actor.GetPremium());
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(highlight: false);
			}
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null && collectionManagerDisplay.m_deckTemplateCardReplacePopup != null)
			{
				collectionManagerDisplay.m_deckTemplateCardReplacePopup.Unshrink(0.1f);
			}
		}
	}

	private void OnCardCountUpdated(int cardCount)
	{
		string text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
		string text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", cardCount, CollectionManager.Get().GetDeckSize());
		m_countLabelText.Text = text;
		m_countText.Text = text2;
	}

	private void OnDeckCountUpdated(int deckCount)
	{
		string text = GameStrings.Get("GLUE_DECK_TRAY_DECK_COUNT_LABEL");
		string text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", deckCount, 27);
		m_countLabelText.Text = text;
		m_countText.Text = text2;
	}

	private void OnDeckCreated(long deckID)
	{
		ResetDeckTrayScroll();
	}

	private void OnCMViewModeChanged(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse)
	{
		DeckContentTypes contentTypeFromViewMode = GetContentTypeFromViewMode(mode);
		m_cardsContent.ShowFakeDeck(mode == CollectionUtils.ViewMode.DECK_TEMPLATE);
		if (triggerResponse)
		{
			m_decksContent.UpdateDeckName();
			if (m_currentContent != 0)
			{
				SetTrayMode(contentTypeFromViewMode);
			}
		}
	}

	private DeckContentTypes GetContentTypeFromViewMode(CollectionUtils.ViewMode viewMode)
	{
		return viewMode switch
		{
			CollectionUtils.ViewMode.CARD_BACKS => DeckContentTypes.CardBack, 
			CollectionUtils.ViewMode.HERO_SKINS => DeckContentTypes.HeroSkin, 
			CollectionUtils.ViewMode.COINS => DeckContentTypes.Coin, 
			_ => DeckContentTypes.Cards, 
		};
	}

	private void OnHeroAssigned(string cardID)
	{
		m_decksContent.UpdateEditingDeckBoxVisual(cardID);
	}

	private CollectionCardEventHandler GetCardEventHandler(string cardID)
	{
		CollectionCardEventHandlerData collectionCardEventHandlerData = m_cardEventHandlers.Find((CollectionCardEventHandlerData data) => data.CardID == cardID);
		if (collectionCardEventHandlerData != null)
		{
			if (collectionCardEventHandlerData.GetInstance() == null)
			{
				CollectionCardEventHandler collectionCardEventHandler = UnityEngine.Object.Instantiate(collectionCardEventHandlerData.CardHandlerPrefab);
				collectionCardEventHandler.transform.parent = base.transform;
				TransformUtil.Identity(collectionCardEventHandler);
				collectionCardEventHandlerData.SetInstance(collectionCardEventHandler);
			}
			return collectionCardEventHandlerData.GetInstance();
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardID);
		for (int i = 0; i < m_tagCardEventHandlers.Count; i++)
		{
			CollectionTagEventHandlerData collectionTagEventHandlerData = m_tagCardEventHandlers[i];
			if (GameUtils.GetCardTagValue(cardDbId, collectionTagEventHandlerData.Tag) != 0)
			{
				return collectionTagEventHandlerData.cardHandlerInstance;
			}
		}
		return m_defaultCardEventHandler;
	}

	private int CalculateNumCardsNeededToCraftToReachMinimumDeckSize(CollectionDeck deck)
	{
		if (deck == null)
		{
			Log.CollectionManager.PrintWarning("GetNumCardsNeededToCraftToReachMinimumDeckSize - No deck to check ruleset against.");
			return 0;
		}
		CollectionDeck collectionDeck = new CollectionDeck();
		collectionDeck.CopyFrom(deck);
		collectionDeck.ClearSlotContents();
		int minimumAllowedDeckSize = collectionDeck.GetRuleset().GetMinimumAllowedDeckSize(collectionDeck);
		IEnumerable<DeckMaker.DeckFill> fillCards = DeckMaker.GetFillCards(collectionDeck, collectionDeck.GetRuleset());
		int num = 0;
		foreach (DeckMaker.DeckFill item in fillCards)
		{
			TAG_PREMIUM preferredPremiumThatCanBeAddedToDeck = GetPreferredPremiumThatCanBeAddedToDeck(collectionDeck, item.m_addCard.GetCardId());
			collectionDeck.AddCard(item.m_addCard, preferredPremiumThatCanBeAddedToDeck);
			num++;
			if (num >= minimumAllowedDeckSize)
			{
				return 0;
			}
		}
		return minimumAllowedDeckSize - num;
	}
}
