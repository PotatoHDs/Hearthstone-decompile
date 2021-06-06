using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class DeckTrayCardListContent : DeckTrayContent
{
	public delegate void CardTileHeld(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTilePress(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTileTap(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTileOver(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTileOut(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTileRelease(DeckTrayDeckTileVisual cardTile);

	public delegate void CardTileRightClicked(DeckTrayDeckTileVisual cardTile);

	public delegate void CardCountChanged(int cardCount);

	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardTileHeight = 2.45f;

	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardHelpButtonHeight = 3f;

	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_deckCardBarFlareUpInterval = 0.075f;

	[CustomEditField(Sections = "Card Tile Settings")]
	public GameObject m_phoneDeckTileBone;

	[CustomEditField(Sections = "Card Tile Settings")]
	public Vector3 m_cardTileOffset = Vector3.zero;

	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardTileSlotLocalHeight;

	[CustomEditField(Sections = "Card Tile Settings")]
	public Vector3 m_cardTileSlotLocalScaleVec3 = new Vector3(0.01f, 0.02f, 0.01f);

	[CustomEditField(Sections = "Card Tile Settings")]
	public bool m_forceUseFullScaleDeckTileActors;

	[CustomEditField(Sections = "Deck Help")]
	public UIBButton m_smartDeckCompleteButton;

	[CustomEditField(Sections = "Deck Help")]
	public UIBButton m_deckTemplateHelpButton;

	[CustomEditField(Sections = "Other Objects")]
	public GameObject m_deckCompleteHighlight;

	[CustomEditField(Sections = "Scroll Settings")]
	public UIBScrollable m_scrollbar;

	[CustomEditField(Sections = "Scroll Settings")]
	public BoxCollider m_LockedScrollBounds;

	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	private const float CARD_MOVEMENT_TIME = 0.3f;

	private Vector3 m_originalLocalPosition;

	private List<DeckTrayDeckTileVisual> m_cardTiles = new List<DeckTrayDeckTileVisual>();

	private List<CardTileHeld> m_cardTileHeldListeners = new List<CardTileHeld>();

	private List<CardTilePress> m_cardTilePressListeners = new List<CardTilePress>();

	private List<CardTileTap> m_cardTileTapListeners = new List<CardTileTap>();

	private List<CardTileOver> m_cardTileOverListeners = new List<CardTileOver>();

	private List<CardTileOut> m_cardTileOutListeners = new List<CardTileOut>();

	private List<CardTileRelease> m_cardTileReleaseListeners = new List<CardTileRelease>();

	private List<CardTileRightClicked> m_cardTileRightClickedListeners = new List<CardTileRightClicked>();

	private List<CardCountChanged> m_cardCountChangedListeners = new List<CardCountChanged>();

	private List<DefLoader.DisposableCardDef> m_cardDefs = new List<DefLoader.DisposableCardDef>();

	private bool m_animating;

	private bool m_loading;

	private const float DECK_HELP_BUTTON_EMPTY_DECK_Y_LOCAL_POS = -0.01194457f;

	private const float DECK_HELP_BUTTON_Y_TILE_OFFSET = -0.04915909f;

	private bool m_inArena;

	private CollectionDeck m_templateFakeDeck = new CollectionDeck();

	private bool m_isShowingFakeDeck;

	private bool m_hasFinishedEntering;

	private bool m_hasFinishedExiting = true;

	private Notification m_deckHelpPopup;

	private float TemplateDeckHelpButtonHeight => m_deckTemplateHelpButton.GetComponent<UIBScrollableItem>().m_size.z * m_cardTileSlotLocalScaleVec3.z;

	protected override void Awake()
	{
		base.Awake();
		if (m_smartDeckCompleteButton != null)
		{
			m_smartDeckCompleteButton.AddEventListener(UIEventType.RELEASE, OnDeckCompleteButtonPress);
			m_smartDeckCompleteButton.AddEventListener(UIEventType.ROLLOVER, OnDeckCompleteButtonOver);
			m_smartDeckCompleteButton.AddEventListener(UIEventType.ROLLOUT, OnDeckCompleteButtonOut);
		}
		if (m_deckTemplateHelpButton != null)
		{
			m_deckTemplateHelpButton.AddEventListener(UIEventType.RELEASE, OnDeckTemplateHelpButtonPress);
			m_deckTemplateHelpButton.AddEventListener(UIEventType.ROLLOVER, OnDeckTemplateHelpButtonOver);
			m_deckTemplateHelpButton.AddEventListener(UIEventType.ROLLOUT, OnDeckTemplateHelpButtonOut);
		}
		m_originalLocalPosition = base.transform.localPosition;
		m_hasFinishedEntering = false;
	}

	protected override void OnDestroy()
	{
		m_cardDefs.DisposeValuesAndClear();
		base.OnDestroy();
	}

	public override bool AnimateContentEntranceStart()
	{
		if (m_loading)
		{
			return false;
		}
		m_animating = true;
		m_hasFinishedEntering = false;
		Action<object> action = delegate
		{
			UpdateDeckCompleteHighlight();
			ShowDeckEditingTipsIfNeeded();
			m_animating = false;
		};
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck != null)
		{
			base.transform.localPosition = GetOffscreenLocalPosition();
			iTween.StopByName(base.gameObject, "position");
			iTween.MoveTo(base.gameObject, iTween.Hash("position", m_originalLocalPosition, "isLocal", true, "time", 0.3f, "easeType", iTween.EaseType.easeOutQuad, "oncomplete", action, "name", "position"));
			if (editingDeck.GetTotalCardCount() > 0)
			{
				SoundManager.Get().LoadAndPlay("collection_manager_new_deck_moves_up_tray.prefab:13650cd587089e14d9a297c8de6057f1", base.gameObject);
			}
			UpdateCardList(updateHighlight: false);
		}
		else
		{
			action(null);
		}
		return true;
	}

	public override bool AnimateContentEntranceEnd()
	{
		if (m_animating)
		{
			return false;
		}
		m_hasFinishedEntering = true;
		FireCardCountChangedEvent();
		return true;
	}

	public override bool AnimateContentExitStart()
	{
		if (m_animating)
		{
			return false;
		}
		m_animating = true;
		m_hasFinishedExiting = false;
		if (m_deckCompleteHighlight != null)
		{
			m_deckCompleteHighlight.SetActive(value: false);
		}
		iTween.StopByName(base.gameObject, "position");
		iTween.MoveTo(base.gameObject, iTween.Hash("position", GetOffscreenLocalPosition(), "isLocal", true, "time", 0.3f, "easeType", iTween.EaseType.easeInQuad, "name", "position"));
		if (HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown())
		{
			SoundManager.Get().LoadAndPlay("panel_slide_off_deck_creation_screen.prefab:b0d25fc984ec05d4fbea7480b611e5ad", base.gameObject);
		}
		Processor.ScheduleCallback(0.5f, realTime: false, delegate
		{
			m_animating = false;
		});
		return true;
	}

	public override bool AnimateContentExitEnd()
	{
		m_hasFinishedExiting = true;
		return !m_animating;
	}

	public bool HasFinishedEntering()
	{
		return m_hasFinishedEntering;
	}

	public bool HasFinishedExiting()
	{
		return m_hasFinishedExiting;
	}

	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		if (newDeck != null)
		{
			List<CollectionDeckSlot> slots = newDeck.GetSlots();
			LoadCardPrefabs(slots);
			if (IsModeActive())
			{
				ShowDeckHelpButtonIfNeeded();
			}
		}
	}

	public void ShowDeckHelper(DeckTrayDeckTileVisual tileToRemove, bool continueAfterReplace, bool replacingCard = false)
	{
		if (!CollectionManager.Get().IsInEditMode() || !DeckHelper.Get())
		{
			return;
		}
		if (!Network.IsLoggedIn())
		{
			CollectionManager.ShowFeatureDisabledWhileOfflinePopup();
			return;
		}
		DeckHelper.DelCompleteCallback onCompleteCallback = delegate(List<EntityDef> chosenCards)
		{
			if (CollectionDeckTray.Get() != null)
			{
				CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(chosenCards);
			}
		};
		DeckHelper.Get().Show(tileToRemove, continueAfterReplace, replacingCard, onCompleteCallback);
	}

	public bool MouseIsOverDeckHelperButton()
	{
		if (m_smartDeckCompleteButton != null && m_smartDeckCompleteButton.gameObject.activeInHierarchy)
		{
			return UniversalInputManager.Get().InputIsOver(m_smartDeckCompleteButton.gameObject);
		}
		return false;
	}

	public bool MouseIsOverDeckCardTile()
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (UniversalInputManager.Get().InputIsOver(cardTile.gameObject))
			{
				return true;
			}
		}
		return false;
	}

	public Vector3 GetCardVisualExtents()
	{
		return new Vector3(m_cardTileHeight, m_cardTileHeight, m_cardTileHeight);
	}

	public List<DeckTrayDeckTileVisual> GetCardTiles()
	{
		return m_cardTiles;
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID)
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (!(cardTile == null) && !(cardTile.GetActor() == null) && cardTile.GetActor().GetEntityDef() != null && cardTile.GetActor().GetEntityDef().GetCardId() == cardID)
			{
				return cardTile;
			}
		}
		return null;
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID, TAG_PREMIUM premType)
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (!(cardTile == null) && !(cardTile.GetActor() == null) && cardTile.GetActor().GetEntityDef() != null && cardTile.GetActor().GetEntityDef().GetCardId() == cardID && cardTile.GetActor().GetPremium() == premType)
			{
				return cardTile;
			}
		}
		return null;
	}

	public DeckTrayDeckTileVisual GetCardTileVisual(int index)
	{
		if (index < m_cardTiles.Count)
		{
			return m_cardTiles[index];
		}
		return null;
	}

	public DeckTrayDeckTileVisual GetCardTileVisualOrLastVisible(string cardID)
	{
		int num = 0;
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			num++;
			if (!(cardTile == null) && !(cardTile.GetActor() == null) && cardTile.GetActor().GetEntityDef() != null)
			{
				if (num > 20)
				{
					return cardTile;
				}
				if (cardTile.GetActor().GetEntityDef().GetCardId() == cardID)
				{
					return cardTile;
				}
			}
		}
		return null;
	}

	public DeckTrayDeckTileVisual GetOrAddCardTileVisual(int index)
	{
		DeckTrayDeckTileVisual newTileVisual = GetCardTileVisual(index);
		if (newTileVisual != null)
		{
			return newTileVisual;
		}
		GameObject gameObject = new GameObject("DeckTileVisual" + index);
		GameUtils.SetParent(gameObject, this);
		gameObject.transform.localScale = m_cardTileSlotLocalScaleVec3;
		newTileVisual = gameObject.AddComponent<DeckTrayDeckTileVisual>();
		bool useFullScaleDeckTileActor = !UniversalInputManager.UsePhoneUI || m_forceUseFullScaleDeckTileActors;
		newTileVisual.Initialize(useFullScaleDeckTileActor);
		newTileVisual.AddEventListener(UIEventType.DRAG, delegate
		{
			FireCardTileDragEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.PRESS, delegate
		{
			FireCardTilePressEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.TAP, delegate
		{
			FireCardTileTapEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.ROLLOVER, delegate
		{
			FireCardTileOverEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.ROLLOUT, delegate
		{
			FireCardTileOutEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.RELEASE, delegate
		{
			FireCardTileReleaseEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.RIGHTCLICK, delegate
		{
			FireCardTileRightClickedEvent(newTileVisual);
		});
		m_cardTiles.Insert(index, newTileVisual);
		Vector3 extents = new Vector3(m_cardTileHeight, m_cardTileHeight, m_cardTileHeight);
		if (m_scrollbar != null)
		{
			m_scrollbar.AddVisibleAffectedObject(gameObject, extents, visible: true, IsCardTileVisible);
		}
		return newTileVisual;
	}

	public void UpdateTileVisuals()
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			cardTile.UpdateGhostedState();
		}
	}

	public override void Show(bool showAll = false)
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (showAll || cardTile.IsInUse())
			{
				cardTile.Show();
			}
		}
	}

	public override void Hide(bool hideAll = false)
	{
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (hideAll || !cardTile.IsInUse())
			{
				cardTile.Hide();
			}
		}
	}

	public void CommitFakeDeckChanges()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		editedDeck.CopyContents(m_templateFakeDeck);
		editedDeck.Name = m_templateFakeDeck.Name;
	}

	public CollectionDeck GetEditingDeck()
	{
		if (m_isShowingFakeDeck)
		{
			if (CollectionManager.Get() != null)
			{
				CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
				if (editedDeck != null)
				{
					m_templateFakeDeck.FormatType = editedDeck.FormatType;
				}
			}
			if (m_templateFakeDeck.FormatType == FormatType.FT_UNKNOWN)
			{
				Debug.LogError("CollectionDeck.GetEditingDeck could not determine the format type for the fake deck " + m_templateFakeDeck.ToString());
			}
		}
		if (!m_isShowingFakeDeck)
		{
			return CollectionManager.Get().GetEditedDeck();
		}
		return m_templateFakeDeck;
	}

	public void ShowFakeDeck(bool show)
	{
		if (m_isShowingFakeDeck != show)
		{
			m_isShowingFakeDeck = show;
			UpdateCardList();
		}
	}

	public void ResetFakeDeck()
	{
		if (m_templateFakeDeck != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				m_templateFakeDeck.CopyContents(editedDeck);
				m_templateFakeDeck.Name = editedDeck.Name;
			}
		}
	}

	public void ShowDeckCompleteEffects()
	{
		StartCoroutine(ShowDeckCompleteEffectsWithInterval(m_deckCardBarFlareUpInterval));
	}

	public void SetInArena(bool inArena)
	{
		m_inArena = inArena;
	}

	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, ref EntityDef removedCard, Actor animateFromActor = null, bool allowInvalid = false, bool updateVisuals = true)
	{
		if (!IsModeActive())
		{
			return false;
		}
		if (cardEntityDef == null)
		{
			Debug.LogError("Trying to add card EntityDef that is null.");
			return false;
		}
		string cardId = cardEntityDef.GetCardId();
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck == null)
		{
			return false;
		}
		if (!allowInvalid && !editingDeck.CanAddOwnedCard(cardId, premium))
		{
			return false;
		}
		if (playSound)
		{
			SoundManager.Get().LoadAndPlay("collection_manager_place_card_in_deck.prefab:df069ffaea9dfb24b96accc95bc434a7", base.gameObject);
		}
		bool num = editingDeck.GetTotalCardCount() == CollectionManager.Get().GetDeckSize();
		DeckTrayDeckTileVisual firstInvalidCard = GetFirstInvalidCard();
		if (num && (deckTileToRemove == null || editingDeck.IsValidSlot(deckTileToRemove.GetSlot())) && firstInvalidCard != null)
		{
			deckTileToRemove = firstInvalidCard;
		}
		if (num || (deckTileToRemove != null && !editingDeck.IsValidSlot(deckTileToRemove.GetSlot())))
		{
			if (deckTileToRemove == null)
			{
				GameplayErrorManager.Get().DisplayMessage(GameStrings.Get("GLUE_COLLECTION_MANAGER_ON_ADD_FULL_DECK_ERROR_TEXT"));
				Debug.LogWarning($"DeckTrayCardListContent.AddCard(): Cannot add card {cardEntityDef.GetCardId()} (premium {premium}) without removing one first.");
				return false;
			}
			deckTileToRemove.SetHighlight(highlight: false);
			string cardID = deckTileToRemove.GetCardID();
			TAG_PREMIUM premium2 = deckTileToRemove.GetPremium();
			if (!editingDeck.RemoveCard(cardID, premium2, editingDeck.IsValidSlot(deckTileToRemove.GetSlot())))
			{
				Debug.LogWarning($"DeckTrayCardListContent.AddCard({cardId},{premium}): Tried to remove card {cardID} with premium {premium2}, but it failed!");
				return false;
			}
			removedCard = deckTileToRemove.GetActor().GetEntityDef();
		}
		if (!editingDeck.AddCard(cardEntityDef, premium))
		{
			Debug.LogWarning($"DeckTrayCardListContent.AddCard({cardId},{premium}): deck.AddCard failed!");
			return false;
		}
		if (updateVisuals)
		{
			UpdateCardList(cardEntityDef, updateHighlight: true, animateFromActor);
			CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(playSound: true);
		}
		DeckHelper.Get().OnCardAdded(editingDeck);
		if (!Options.Get().GetBool(Option.HAS_ADDED_CARDS_TO_DECK, defaultVal: false) && editingDeck.GetTotalCardCount() >= 2 && !DeckHelper.Get().IsActive() && editingDeck.GetTotalCardCount() < 15 && UserAttentionManager.CanShowAttentionGrabber("DeckTrayCardListContent.AddCard:" + Option.HAS_ADDED_CARDS_TO_DECK))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_PAGEFLIP_28"), "VO_INNKEEPER_CM_PAGEFLIP_28.prefab:47bb7bdb89ad93443ab7d031bbe666fb");
			Options.Get().SetBool(Option.HAS_ADDED_CARDS_TO_DECK, val: true);
		}
		return true;
	}

	public int RemoveClosestInvalidCard(EntityDef entityDef, int sameRemoveCount)
	{
		CollectionDeck editingDeck = GetEditingDeck();
		int cost = entityDef.GetCost();
		int num = int.MaxValue;
		string text = string.Empty;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		foreach (CollectionDeckSlot slot in editingDeck.GetSlots())
		{
			if (!editingDeck.IsValidSlot(slot))
			{
				EntityDef entityDef2 = DefLoader.Get().GetEntityDef(slot.CardID);
				if (entityDef2 == entityDef)
				{
					text = entityDef.GetCardId();
					premium = slot.UnPreferredPremium;
					break;
				}
				int num2 = Mathf.Abs(cost - entityDef2.GetCost());
				if (num2 < num)
				{
					num = num2;
					text = slot.CardID;
				}
			}
		}
		int num3 = 0;
		if (!string.IsNullOrEmpty(text))
		{
			for (int i = 0; i < sameRemoveCount; i++)
			{
				if (editingDeck.RemoveCard(text, premium, valid: false))
				{
					num3++;
				}
			}
		}
		UpdateCardList();
		return num3;
	}

	[ContextMenu("Update Card List")]
	public void UpdateCardList()
	{
		UpdateCardList(updateHighlight: true);
	}

	public void UpdateCardList(bool updateHighlight, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		UpdateCardList(string.Empty, updateHighlight, animateFromActor, onCompleteCallback);
	}

	public void UpdateCardList(EntityDef justChangedCardEntityDef, bool updateHighlight = true, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		UpdateCardList((justChangedCardEntityDef != null) ? justChangedCardEntityDef.GetCardId() : string.Empty, updateHighlight, animateFromActor, onCompleteCallback);
	}

	public void UpdateCardList(string justChangedCardID, bool updateHighlight = true, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck == null)
		{
			return;
		}
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			cardTile.MarkAsUnused();
		}
		List<CollectionDeckSlot> slots = editingDeck.GetSlots();
		int num = 0;
		Vector3 cardTileOffset = GetCardTileOffset(editingDeck);
		for (int i = 0; i < slots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = slots[i];
			if (collectionDeckSlot.Count == 0)
			{
				Log.DeckTray.Print($"DeckTrayCardListContent.UpdateCardList(): Slot {i} of deck is empty! Skipping...");
				continue;
			}
			num += collectionDeckSlot.Count;
			DeckTrayDeckTileVisual orAddCardTileVisual = GetOrAddCardTileVisual(i);
			orAddCardTileVisual.SetInArena(m_inArena);
			orAddCardTileVisual.gameObject.transform.localPosition = cardTileOffset + Vector3.down * (m_cardTileSlotLocalHeight * (float)i);
			orAddCardTileVisual.MarkAsUsed();
			orAddCardTileVisual.Show();
			orAddCardTileVisual.SetSlot(editingDeck, collectionDeckSlot, justChangedCardID.Equals(collectionDeckSlot.CardID));
		}
		Hide();
		ShowDeckHelpButtonIfNeeded();
		FireCardCountChangedEvent();
		if (m_scrollbar != null)
		{
			m_scrollbar.UpdateScroll();
		}
		if (updateHighlight)
		{
			UpdateDeckCompleteHighlight();
		}
		if (animateFromActor != null && base.gameObject.activeInHierarchy)
		{
			StartCoroutine(ShowAddCardAnimationAfterTrayLoads(animateFromActor, onCompleteCallback));
		}
		else
		{
			onCompleteCallback?.Invoke();
		}
	}

	private Vector3 GetCardTileOffset(CollectionDeck currentDeck)
	{
		Vector3 vector = Vector3.zero;
		if (!m_isShowingFakeDeck && currentDeck != null && m_deckTemplateHelpButton != null)
		{
			bool flag = true;
			if (SceneMgr.Get().IsInTavernBrawlMode())
			{
				flag = TavernBrawlDisplay.IsTavernBrawlEditing();
			}
			if (flag && currentDeck.GetTotalInvalidCardCount() > 0)
			{
				vector = Vector3.down * TemplateDeckHelpButtonHeight;
			}
		}
		return vector + m_cardTileOffset;
	}

	public void TriggerCardCountUpdate()
	{
		FireCardCountChangedEvent();
	}

	public void HideDeckHelpPopup()
	{
		if (m_deckHelpPopup != null)
		{
			NotificationManager.Get().DestroyNotification(m_deckHelpPopup, 0f);
		}
	}

	public DeckTrayDeckTileVisual GetFirstInvalidCard()
	{
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck != null)
		{
			foreach (CollectionDeckSlot slot in editingDeck.GetSlots())
			{
				if (!editingDeck.IsValidSlot(slot))
				{
					return GetCardTileVisual(slot.Index);
				}
			}
		}
		return null;
	}

	private static bool ShouldShowDeckCompleteHighlight(CollectionDeck deck)
	{
		DeckType type = deck.Type;
		if (type == DeckType.CLIENT_ONLY_DECK || type == DeckType.DRAFT_DECK)
		{
			return false;
		}
		return true;
	}

	public void UpdateDeckCompleteHighlight()
	{
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck == null || !ShouldShowDeckCompleteHighlight(editingDeck))
		{
			return;
		}
		bool flag = editingDeck != null && editingDeck.GetTotalValidCardCount() == CollectionManager.Get().GetDeckSize();
		bool flag2 = editingDeck?.Locked ?? false;
		if (m_scrollbar != null && m_LockedScrollBounds != null && flag2)
		{
			m_scrollbar.m_ScrollBounds.center = m_LockedScrollBounds.center;
			m_scrollbar.m_ScrollBounds.size = m_LockedScrollBounds.size;
		}
		if (m_deckCompleteHighlight != null)
		{
			if (flag2)
			{
				m_deckCompleteHighlight.SetActive(value: false);
			}
			else
			{
				m_deckCompleteHighlight.SetActive(flag);
			}
		}
		if (flag && !Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, defaultVal: false))
		{
			Options.Get().SetBool(Option.HAS_FINISHED_A_DECK, val: true);
		}
	}

	public Notification GetDeckHelpPopup()
	{
		return m_deckHelpPopup;
	}

	private IEnumerator ShowAddCardAnimationAfterTrayLoads(Actor cardToAnimate, Action onCompleteCallback)
	{
		string cardID = cardToAnimate.GetEntityDef().GetCardId();
		DeckTrayDeckTileVisual tile = GetCardTileVisual(cardID);
		Vector3 cardPos = cardToAnimate.transform.position;
		while (tile == null)
		{
			yield return null;
			tile = GetCardTileVisual(cardID);
		}
		GameObject cardTileObject = UnityEngine.Object.Instantiate(tile.GetActor().gameObject);
		Actor movingCardTile = cardTileObject.GetComponent<Actor>();
		if (GetEditingDeck().GetCardCountAllMatchingSlots(cardID) == 1)
		{
			tile.Hide();
		}
		else
		{
			tile.Show();
		}
		movingCardTile.transform.position = new Vector3(cardPos.x, cardPos.y + 2.5f, cardPos.z);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			movingCardTile.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
		}
		else
		{
			movingCardTile.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		movingCardTile.ActivateAllSpellsDeathStates();
		movingCardTile.ActivateSpellBirthState(SpellType.SUMMON_IN_LARGE);
		if ((bool)UniversalInputManager.UsePhoneUI && m_phoneDeckTileBone != null)
		{
			iTween.MoveTo(cardTileObject, iTween.Hash("position", m_phoneDeckTileBone.transform.position, "time", 0.5f, "easetype", iTween.EaseType.easeInCubic, "oncomplete", (Action<object>)delegate
			{
				tile.ShowAndSetupActor();
				tile.GetActor().GetSpell(SpellType.SUMMON_IN).ActivateState(SpellStateType.BIRTH);
				StartCoroutine(FinishPhoneMovingCardTile(cardTileObject, movingCardTile, 1f));
				if (onCompleteCallback != null)
				{
					onCompleteCallback();
				}
			}));
			iTween.ScaleTo(cardTileObject, iTween.Hash("scale", new Vector3(0.5f, 1.1f, 1.1f), "time", 0.5f, "easetype", iTween.EaseType.easeInCubic));
		}
		else
		{
			Vector3[] newPath = new Vector3[3];
			Vector3 startSpot = movingCardTile.transform.position;
			newPath[0] = startSpot;
			iTween.ValueTo(cardTileObject, iTween.Hash("from", 0f, "to", 1f, "time", 0.75f, "easetype", iTween.EaseType.easeOutCirc, "onupdate", (Action<object>)delegate(object val)
			{
				Vector3 position = tile.transform.position;
				newPath[1] = new Vector3((startSpot.x + position.x) * 0.5f, (startSpot.y + position.y) * 0.5f + 60f, (startSpot.z + position.z) * 0.5f);
				newPath[2] = position;
				iTween.PutOnPath(cardTileObject, newPath, (float)val);
			}, "oncomplete", (Action<object>)delegate
			{
				tile.ShowAndSetupActor();
				tile.GetActor().GetSpell(SpellType.SUMMON_IN).ActivateState(SpellStateType.BIRTH);
				movingCardTile.Hide();
				UnityEngine.Object.Destroy(cardTileObject);
				if (onCompleteCallback != null)
				{
					onCompleteCallback();
				}
			}));
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
	}

	private IEnumerator FinishPhoneMovingCardTile(GameObject obj, Actor movingCardTile, float delay)
	{
		yield return new WaitForSeconds(delay);
		movingCardTile.Hide();
		UnityEngine.Object.Destroy(obj);
	}

	private IEnumerator ShowDeckCompleteEffectsWithInterval(float interval)
	{
		if (m_scrollbar == null)
		{
			yield break;
		}
		bool needScroll = m_scrollbar.IsScrollNeeded();
		if (needScroll)
		{
			m_scrollbar.Enable(enable: false);
			m_scrollbar.ForceVisibleAffectedObjectsShow(show: true);
			m_scrollbar.SetScroll(0f, iTween.EaseType.easeOutSine, 0.25f, blockInputWhileScrolling: true);
			yield return new WaitForSeconds(0.3f);
			m_scrollbar.SetScroll(1f, iTween.EaseType.easeInOutQuart, interval * (float)m_cardTiles.Count, blockInputWhileScrolling: true);
		}
		foreach (DeckTrayDeckTileVisual cardTile in m_cardTiles)
		{
			if (!(cardTile == null) && cardTile.IsInUse())
			{
				cardTile.GetActor().ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
				yield return new WaitForSeconds(interval);
			}
		}
		foreach (DeckTrayDeckTileVisual tile in m_cardTiles)
		{
			if (!(tile == null) && tile.IsInUse())
			{
				yield return new WaitForSeconds(interval);
				tile.GetActor().DeactivateAllSpells();
			}
		}
		if (needScroll)
		{
			m_scrollbar.ForceVisibleAffectedObjectsShow(show: false);
			m_scrollbar.EnableIfNeeded();
		}
	}

	private void IsCardTileVisible(GameObject obj, bool visible)
	{
		if (obj.activeSelf == visible)
		{
			return;
		}
		DeckTrayDeckTileVisual component = obj.GetComponent<DeckTrayDeckTileVisual>();
		if (!(component == null))
		{
			if (visible && obj.GetComponent<DeckTrayDeckTileVisual>().IsInUse())
			{
				component.ShowAndSetupActor();
			}
			else
			{
				component.Hide();
			}
		}
	}

	private void ShowDeckEditingTipsIfNeeded()
	{
		if (Options.Get().GetBool(Option.HAS_REMOVED_CARD_FROM_DECK, defaultVal: false) || SceneMgr.Get().IsInTavernBrawlMode() || CollectionManager.Get().GetCollectibleDisplay() == null || CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != 0 || m_cardTiles.Count <= 0)
		{
			return;
		}
		Transform removeCardTutorialBone = CollectionDeckTray.Get().m_removeCardTutorialBone;
		if (m_deckHelpPopup == null)
		{
			m_deckHelpPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, removeCardTutorialBone.position, removeCardTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL08"));
			if (m_deckHelpPopup != null)
			{
				m_deckHelpPopup.PulseReminderEveryXSeconds(3f);
			}
		}
	}

	private void ShowDeckHelpButtonIfNeeded()
	{
		bool flag = false;
		bool flag2 = false;
		if (CollectionManager.Get().GetCollectibleDisplay() == null)
		{
			return;
		}
		CollectionDeck editingDeck = GetEditingDeck();
		if (editingDeck != null && DeckHelper.Get() != null && editingDeck.GetTotalValidCardCount() < CollectionManager.Get().GetDeckSize())
		{
			flag2 = true;
		}
		if (editingDeck.GetTotalInvalidCardCount() > 0)
		{
			flag2 = false;
			flag = true;
		}
		else
		{
			flag = false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			flag = false;
			flag2 = false;
		}
		if (TavernBrawlDisplay.IsTavernBrawlViewing() || SceneMgr.Get().IsInDuelsMode())
		{
			flag = false;
			flag2 = false;
		}
		if (!DeckHelper.HasChoicesToOffer(editingDeck))
		{
			flag2 = false;
		}
		if (m_smartDeckCompleteButton != null)
		{
			m_smartDeckCompleteButton.gameObject.SetActive(flag2);
			if (flag2)
			{
				Vector3 cardTileOffset = GetCardTileOffset(editingDeck);
				cardTileOffset.y -= m_cardTileSlotLocalHeight * (float)editingDeck.GetSlots().Count;
				m_smartDeckCompleteButton.transform.localPosition = cardTileOffset;
			}
		}
		if (m_deckTemplateHelpButton != null)
		{
			m_deckTemplateHelpButton.gameObject.SetActive(flag);
		}
		if (Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, defaultVal: false))
		{
			return;
		}
		if (m_smartDeckCompleteButton != null)
		{
			HighlightState componentInChildren = m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
		if (m_deckTemplateHelpButton != null)
		{
			HighlightState componentInChildren2 = m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
	}

	private void OnDeckTemplateHelpButtonPress(UIEvent e)
	{
		Options.Get().SetBool(Option.HAS_CLICKED_DECK_TEMPLATE_REPLACE, val: true);
		DeckTrayDeckTileVisual firstInvalidCard = GetFirstInvalidCard();
		ShowDeckHelper(firstInvalidCard, continueAfterReplace: true, firstInvalidCard != null);
	}

	private void OnDeckTemplateHelpButtonOver(UIEvent e)
	{
		HighlightState componentInChildren = m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			if (!Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, defaultVal: false))
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			else
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			}
		}
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9", base.gameObject);
	}

	private void OnDeckTemplateHelpButtonOut(UIEvent e)
	{
		HighlightState componentInChildren = m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			if (!Options.Get().GetBool(Option.HAS_CLICKED_DECK_TEMPLATE_REPLACE, defaultVal: false))
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			else
			{
				componentInChildren.ChangeState(ActorStateType.NONE);
			}
		}
	}

	private void OnDeckCompleteButtonPress(UIEvent e)
	{
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().CompleteMyDeckButtonPress();
		}
	}

	private void OnDeckCompleteButtonOver(UIEvent e)
	{
		if (!CollectionInputMgr.Get().HasHeldCard())
		{
			HighlightState componentInChildren = m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			}
			SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9", base.gameObject);
		}
	}

	private void OnDeckCompleteButtonOut(UIEvent e)
	{
		HighlightState componentInChildren = m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeState(ActorStateType.NONE);
		}
	}

	private void LoadCardPrefabs(List<CollectionDeckSlot> deckSlots)
	{
		if (deckSlots.Count == 0)
		{
			return;
		}
		int prefabsToLoad = deckSlots.Count;
		m_loading = true;
		m_cardDefs.DisposeValuesAndClear();
		for (int i = 0; i < deckSlots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = deckSlots[i];
			if (collectionDeckSlot.Count == 0)
			{
				Log.DeckTray.Print($"DeckTrayCardListContent.LoadCardPrefabs(): Slot {i} of deck is empty! Skipping...");
				continue;
			}
			DefLoader.Get().LoadCardDef(collectionDeckSlot.CardID, delegate(string cardId, DefLoader.DisposableCardDef def, object userData)
			{
				m_cardDefs.Add(def);
				prefabsToLoad--;
				if (prefabsToLoad == 0)
				{
					m_loading = false;
				}
			}, null, new CardPortraitQuality(1, loadPremium: false));
		}
	}

	private Vector3 GetOffscreenLocalPosition()
	{
		Vector3 originalLocalPosition = m_originalLocalPosition;
		CollectionDeck editingDeck = GetEditingDeck();
		int num = ((editingDeck != null) ? (editingDeck.GetSlotCount() + 2) : 0);
		originalLocalPosition.z -= m_cardTileHeight * (float)num - GetCardTileOffset(editingDeck).y / m_cardTileSlotLocalScaleVec3.y;
		return originalLocalPosition;
	}

	public void RegisterCardTileHeldListener(CardTileHeld dlg)
	{
		m_cardTileHeldListeners.Add(dlg);
	}

	public void RegisterCardTilePressListener(CardTilePress dlg)
	{
		m_cardTilePressListeners.Add(dlg);
	}

	public void RegisterCardTileTapListener(CardTileTap dlg)
	{
		m_cardTileTapListeners.Add(dlg);
	}

	public void RegisterCardTileOverListener(CardTileOver dlg)
	{
		m_cardTileOverListeners.Add(dlg);
	}

	public void RegisterCardTileOutListener(CardTileOut dlg)
	{
		m_cardTileOutListeners.Add(dlg);
	}

	public void RegisterCardTileReleaseListener(CardTileRelease dlg)
	{
		m_cardTileReleaseListeners.Add(dlg);
	}

	public void RegisterCardTileRightClickedListener(CardTileRightClicked dlg)
	{
		m_cardTileRightClickedListeners.Add(dlg);
	}

	public void RegisterCardCountUpdated(CardCountChanged dlg)
	{
		m_cardCountChangedListeners.Add(dlg);
	}

	public void UnregisterCardTileHeldListener(CardTileHeld dlg)
	{
		m_cardTileHeldListeners.Remove(dlg);
	}

	public void UnregisterCardTileTapListener(CardTileTap dlg)
	{
		m_cardTileTapListeners.Remove(dlg);
	}

	public void UnregisterCardTilePressListener(CardTilePress dlg)
	{
		m_cardTilePressListeners.Remove(dlg);
	}

	public void UnregisterCardTileOverListener(CardTileOver dlg)
	{
		m_cardTileOverListeners.Remove(dlg);
	}

	public void UnregisterCardTileOutListener(CardTileOut dlg)
	{
		m_cardTileOutListeners.Remove(dlg);
	}

	public void UnregisterCardTileReleaseListener(CardTileRelease dlg)
	{
		m_cardTileReleaseListeners.Remove(dlg);
	}

	public void UnregisterCardTileRightClickedListener(CardTileRightClicked dlg)
	{
		m_cardTileRightClickedListeners.Remove(dlg);
	}

	public void UnregisterCardCountUpdated(CardCountChanged dlg)
	{
		m_cardCountChangedListeners.Remove(dlg);
	}

	private void FireCardTileDragEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileHeld[] array = m_cardTileHeldListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTilePressEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTilePress[] array = m_cardTilePressListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTileTapEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileTap[] array = m_cardTileTapListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTileOverEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileOver[] array = m_cardTileOverListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTileOutEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileOut[] array = m_cardTileOutListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTileReleaseEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileRelease[] array = m_cardTileReleaseListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardTileRightClickedEvent(DeckTrayDeckTileVisual cardTile)
	{
		CardTileRightClicked[] array = m_cardTileRightClickedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	private void FireCardCountChangedEvent()
	{
		CardCountChanged[] array = m_cardCountChangedListeners.ToArray();
		CollectionDeck editingDeck = GetEditingDeck();
		int cardCount = 0;
		if (editingDeck != null)
		{
			cardCount = (editingDeck.ShouldSplitSlotsByOwnershipOrFormatValidity() ? editingDeck.GetTotalValidCardCount() : editingDeck.GetTotalCardCount());
		}
		CardCountChanged[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](cardCount);
		}
	}
}
