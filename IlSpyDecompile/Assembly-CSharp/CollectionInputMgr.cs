using System.Collections.Generic;
using Hearthstone;
using PegasusShared;
using UnityEngine;

public class CollectionInputMgr : MonoBehaviour
{
	public delegate void OnCardDroppedCallback();

	public CollectionDraggableCardVisual m_heldCardVisual;

	public Collider TooltipPlane;

	public static readonly PlatformDependentValue<float> PHONE_HEIGHT_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		Phone = 10f
	};

	private static CollectionInputMgr s_instance;

	private bool m_heldCardOffscreen;

	private bool m_mouseIsOverDeck;

	private UIBScrollable m_scrollBar;

	public OnCardDroppedCallback m_cardDroppedCallback;

	private void Awake()
	{
		s_instance = this;
		UniversalInputManager.Get().RegisterMouseOnOrOffScreenListener(OnMouseOnOrOffScreen);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	private void Start()
	{
	}

	private void Update()
	{
		UpdateHeldCard();
	}

	public static CollectionInputMgr Get()
	{
		return s_instance;
	}

	public void Unload()
	{
		UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(OnMouseOnOrOffScreen);
	}

	public bool HandleKeyboardInput()
	{
		if (CollectionDeckTray.Get() != null && (InputCollection.GetKey(KeyCode.LeftCommand) || InputCollection.GetKey(KeyCode.LeftControl) || InputCollection.GetKey(KeyCode.RightCommand) || InputCollection.GetKey(KeyCode.RightControl)))
		{
			bool flag = CollectionDeckTray.Get().IsShowingDeckContents();
			if (InputCollection.GetKeyDown(KeyCode.C) && flag)
			{
				CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
				if (editedDeck.CanCopyAsShareableDeck(out var topViolation))
				{
					ClipboardUtils.CopyToClipboard(editedDeck.GetShareableDeck().Serialize());
					UIStatus.Get().AddInfo(GameStrings.Get("GLUE_COLLECTION_DECK_COPIED_TOAST"));
				}
				else
				{
					string userFriendlyCopyErrorMessageFromDeckRuleViolation = CollectionDeck.GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(topViolation);
					if (!string.IsNullOrEmpty(userFriendlyCopyErrorMessageFromDeckRuleViolation))
					{
						UIStatus.Get().AddInfo(userFriendlyCopyErrorMessageFromDeckRuleViolation);
					}
				}
			}
			if (InputCollection.GetKeyDown(KeyCode.V))
			{
				bool flag2 = DialogManager.Get().ShowingDialog();
				CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
				if (collectionManagerDisplay != null)
				{
					bool flag3 = collectionManagerDisplay.IsSelectingNewDeckHero();
					if (!flag2 && !flag && !flag3)
					{
						collectionManagerDisplay.PasteFromClipboardIfValidOrShowStatusMessage();
					}
				}
			}
		}
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			if (CardBackInfoManager.IsLoadedAndShowingPreview())
			{
				CardBackInfoManager.Get().CancelPreview();
				return true;
			}
			if (CraftingManager.Get() != null && CraftingManager.Get().IsCardShowing() && !CraftingManager.Get().IsCancelling())
			{
				Navigation.GoBack();
				return true;
			}
		}
		if (HearthstoneApplication.GetMode() == ApplicationMode.INTERNAL && InputCollection.GetKeyUp(KeyCode.P))
		{
			TAG_PREMIUM tAG_PREMIUM = TAG_PREMIUM.GOLDEN;
			if (CollectionManager.Get().GetPreferredPremium() == TAG_PREMIUM.GOLDEN)
			{
				tAG_PREMIUM = TAG_PREMIUM.NORMAL;
			}
			Debug.Log("setting premium preference " + tAG_PREMIUM);
			CollectionManager.Get().SetPremiumPreference(tAG_PREMIUM);
			return true;
		}
		return false;
	}

	public static void PasteDeckFromClipboard()
	{
		ShareableDeck shareableDeck = ShareableDeck.DeserializeFromClipboard();
		if (shareableDeck != null)
		{
			PasteDeckInEditModeFromShareableDeck(shareableDeck);
		}
	}

	public static void PasteDeckInEditModeFromShareableDeck(ShareableDeck shareableDeck)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			Debug.LogError("Error trying to paste deck. Collection Manager is not in edit mode.");
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (!string.IsNullOrEmpty(shareableDeck.DeckName))
		{
			editedDeck.Name = shareableDeck.DeckName;
			CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
			if (collectionDeckTray != null)
			{
				collectionDeckTray.GetDecksContent().UpdateDeckName(shareableDeck.DeckName);
			}
		}
		editedDeck.SetShareableDeckCreatedFrom(shareableDeck);
		DefLoader defLoader = DefLoader.Get();
		List<DeckMaker.DeckFill> list = new List<DeckMaker.DeckFill>();
		if (SceneMgr.Get().IsInDuelsMode())
		{
			int num = editedDeck.GetMaxCardCount() - 1;
			for (int i = 0; i < shareableDeck.DeckContents.Cards.Count; i++)
			{
				DeckCardData deckCardData = shareableDeck.DeckContents.Cards[i];
				EntityDef entityDef = defLoader.GetEntityDef(deckCardData.Def.Asset);
				CollectionDeckSlot collectionDeckSlot = editedDeck.FindFirstSlotByCardId(entityDef.GetCardId());
				if (editedDeck.CanInsertCard(entityDef.GetCardId(), TAG_PREMIUM.NORMAL) || editedDeck.CanInsertCard(entityDef.GetCardId(), TAG_PREMIUM.GOLDEN) || collectionDeckSlot != null)
				{
					list.Add(new DeckMaker.DeckFill
					{
						m_addCard = entityDef
					});
				}
				if (list.Count >= num)
				{
					break;
				}
			}
			List<string> cardsWithCardID = editedDeck.GetCardsWithCardID();
			if (cardsWithCardID != null)
			{
				for (int j = 0; j < cardsWithCardID.Count; j++)
				{
					if (DuelsConfig.IsCardLoadoutTreasure(cardsWithCardID[j]))
					{
						int dbId = GameUtils.TranslateCardIdToDbId(cardsWithCardID[j]);
						list.Add(new DeckMaker.DeckFill
						{
							m_addCard = defLoader.GetEntityDef(dbId)
						});
						break;
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < shareableDeck.DeckContents.Cards.Count; k++)
			{
				DeckCardData deckCardData2 = shareableDeck.DeckContents.Cards[k];
				EntityDef entityDef2 = defLoader.GetEntityDef(deckCardData2.Def.Asset);
				if (CollectionManager.Get().GetTotalOwnedCount(entityDef2.GetCardId()) < deckCardData2.Qty)
				{
					string ownedCounterpartCardIDForFormat = GameUtils.GetOwnedCounterpartCardIDForFormat(entityDef2, shareableDeck.FormatType, deckCardData2.Qty);
					if (ownedCounterpartCardIDForFormat != null && editedDeck.CanInsertCard(ownedCounterpartCardIDForFormat, (TAG_PREMIUM)deckCardData2.Def.Premium))
					{
						EntityDef entityDef3 = defLoader.GetEntityDef(ownedCounterpartCardIDForFormat);
						if (entityDef3 != null)
						{
							entityDef2 = entityDef3;
						}
					}
				}
				for (int l = 0; l < deckCardData2.Qty; l++)
				{
					list.Add(new DeckMaker.DeckFill
					{
						m_addCard = entityDef2
					});
				}
			}
		}
		CollectionDeckTray.PopuplateDeckCompleteCallback completedCallback = delegate(List<EntityDef> addedCards, List<EntityDef> removedCards)
		{
			CollectionDeck editedDeck2 = CollectionManager.Get().GetEditedDeck();
			int num2 = CollectionManager.Get().GetDeckRuleset()?.GetDeckSize(editedDeck2) ?? int.MinValue;
			if (editedDeck2 != null && (editedDeck2.HasReplaceableSlot() || editedDeck2.GetTotalCardCount() < num2))
			{
				CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(addedCards);
			}
		};
		CollectionDeckTray.Get().PopulateDeck(list, completedCallback);
	}

	public static void AlertPlayerOnInvalidDeckPaste(string errorReason)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INVALID_POPUP_HEADER"),
			m_text = errorReason,
			m_okText = GameStrings.Get("GLOBAL_OKAY"),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	public bool GrabCard(CollectionCardVisual cardVisual)
	{
		Actor preferredActor = cardVisual.GetCollectionCardActors().GetPreferredActor();
		if (!CanGrabItem(preferredActor))
		{
			return false;
		}
		m_heldCardVisual.SetSlot(null);
		if (!m_heldCardVisual.ChangeActor(preferredActor, cardVisual.GetVisualType(), preferredActor.GetPremium()))
		{
			return false;
		}
		if (m_scrollBar != null)
		{
			m_scrollBar.Pause(pause: true);
		}
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		CollectionCardBack component = preferredActor.GetComponent<CollectionCardBack>();
		if (component != null)
		{
			m_heldCardVisual.SetCardBackId(component.GetCardBackId());
		}
		m_heldCardVisual.transform.position = preferredActor.transform.position;
		m_heldCardVisual.Show(m_mouseIsOverDeck);
		SoundManager.Get().LoadAndPlay("collection_manager_pick_up_card.prefab:f7fb595cdc26f2f4997b4a10eaf1d0e1", m_heldCardVisual.gameObject);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideFilterTrayOnStartDragCard();
		}
		return true;
	}

	public bool GrabCard(DeckTrayDeckTileVisual deckTileVisual, OnCardDroppedCallback callback, bool removeCard = true)
	{
		m_cardDroppedCallback = callback;
		return GrabCard(deckTileVisual, removeCard);
	}

	public bool GrabCard(DeckTrayDeckTileVisual deckTileVisual, bool removeCard = true)
	{
		Actor actor = deckTileVisual.GetActor();
		if (!CanGrabItem(actor))
		{
			return false;
		}
		m_heldCardVisual.SetSlot(deckTileVisual.GetSlot());
		if (!m_heldCardVisual.ChangeActor(actor, CollectionUtils.ViewMode.CARDS, deckTileVisual.GetSlot().UnPreferredPremium))
		{
			return false;
		}
		if (m_scrollBar != null)
		{
			m_scrollBar.Pause(pause: true);
		}
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		m_heldCardVisual.transform.position = actor.transform.position;
		m_heldCardVisual.Show(m_mouseIsOverDeck);
		SoundManager.Get().LoadAndPlay("collection_manager_pick_up_card.prefab:f7fb595cdc26f2f4997b4a10eaf1d0e1", m_heldCardVisual.gameObject);
		if (DuelsConfig.IsCardLoadoutTreasure(m_heldCardVisual.GetCardID()))
		{
			removeCard = false;
		}
		if (removeCard)
		{
			CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
			CollectionDeckTray.Get().RemoveCard(m_heldCardVisual.GetCardID(), deckTileVisual.GetSlot().UnPreferredPremium, editingDeck.IsValidSlot(deckTileVisual.GetSlot()));
			if (!Options.Get().GetBool(Option.HAS_REMOVED_CARD_FROM_DECK, defaultVal: false))
			{
				CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
				Options.Get().SetBool(Option.HAS_REMOVED_CARD_FROM_DECK, val: true);
			}
		}
		return true;
	}

	public void DropCard(DeckTrayDeckTileVisual deckTileToRemove)
	{
		DropCard(dragCanceled: false, deckTileToRemove);
	}

	public void SetScrollbar(UIBScrollable scrollbar)
	{
		m_scrollBar = scrollbar;
	}

	public bool IsDraggingScrollbar()
	{
		if (m_scrollBar != null)
		{
			return m_scrollBar.IsDragging();
		}
		return false;
	}

	public bool HasHeldCard()
	{
		return m_heldCardVisual.IsShown();
	}

	private bool CanGrabItem(Actor actor)
	{
		if (IsDraggingScrollbar())
		{
			return false;
		}
		if (m_heldCardVisual.IsShown())
		{
			return false;
		}
		if (actor == null)
		{
			return false;
		}
		return true;
	}

	private void UpdateHeldCard()
	{
		if (!m_heldCardVisual.IsShown() || !UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out var hitInfo))
		{
			return;
		}
		if (m_heldCardVisual != null && (bool)UniversalInputManager.UsePhoneUI)
		{
			Transform[] componentsInChildren = m_heldCardVisual.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 19;
			}
		}
		Vector3 point = hitInfo.point;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			point.y += PHONE_HEIGHT_OFFSET;
		}
		m_heldCardVisual.transform.position = point;
		if (CollectionDeckTray.Get() != null)
		{
			m_mouseIsOverDeck = CollectionDeckTray.Get().MouseIsOver();
			m_heldCardVisual.UpdateVisual(m_mouseIsOverDeck);
		}
		if (DraftPhoneDeckTray.Get() != null)
		{
			m_mouseIsOverDeck = DraftPhoneDeckTray.Get().MouseIsOver();
			m_heldCardVisual.UpdateVisual(m_mouseIsOverDeck);
		}
		if (InputCollection.GetMouseButtonUp(0))
		{
			DropCard(null);
		}
	}

	private void DropCard(bool dragCanceled, DeckTrayDeckTileVisual deckTileToRemove)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		if (!dragCanceled)
		{
			if (m_mouseIsOverDeck)
			{
				switch (m_heldCardVisual.GetVisualType())
				{
				case CollectionUtils.ViewMode.CARDS:
					if (CollectionDeckTray.Get() != null && !DuelsConfig.IsCardLoadoutTreasure(m_heldCardVisual.GetCardID()) && CollectionDeckTray.Get().AddCard(m_heldCardVisual.GetEntityDef(), m_heldCardVisual.GetPremium(), deckTileToRemove, playSound: true, null, allowInvalid: true))
					{
						CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(m_heldCardVisual.GetEntityDef());
					}
					break;
				case CollectionUtils.ViewMode.CARD_BACKS:
				{
					object obj = m_heldCardVisual.GetCardBackId();
					if (obj == null)
					{
						Debug.LogWarning("Cardback ID not set for dragging card back.");
					}
					else if (CollectionDeckTray.Get() != null)
					{
						CollectionDeckTray.Get().GetCardBackContent().UpdateCardBack((int)obj, assigning: true);
					}
					break;
				}
				case CollectionUtils.ViewMode.HERO_SKINS:
					if (CollectionDeckTray.Get() != null)
					{
						CollectionDeckTray.Get().GetHeroSkinContent().UpdateHeroSkin(m_heldCardVisual.GetEntityDef(), m_heldCardVisual.GetPremium(), assigning: true);
					}
					break;
				}
			}
			else
			{
				SoundManager.Get().LoadAndPlay("collection_manager_drop_card.prefab:8275e45efb8280347b35c2548e706d84", m_heldCardVisual.gameObject);
				if (m_cardDroppedCallback != null)
				{
					m_cardDroppedCallback();
					m_cardDroppedCallback = null;
				}
			}
		}
		m_heldCardVisual.Hide();
		if (m_scrollBar != null)
		{
			m_scrollBar.Pause(pause: false);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			if (!dragCanceled && m_mouseIsOverDeck)
			{
				collectionManagerDisplay.WaitThenUnhideFilterTrayOnStopDragCard();
			}
			else
			{
				collectionManagerDisplay.UnhideFilterTrayOnStopDragCard();
			}
		}
	}

	private void OnMouseOnOrOffScreen(bool onScreen)
	{
		if (m_heldCardVisual == null || m_heldCardVisual.gameObject == null)
		{
			return;
		}
		if (onScreen)
		{
			if (m_heldCardOffscreen)
			{
				m_heldCardOffscreen = false;
				if (UniversalInputManager.Get().GetMouseButton(0))
				{
					m_heldCardVisual.Show(m_mouseIsOverDeck);
				}
				else
				{
					DropCard(dragCanceled: true, null);
				}
			}
		}
		else if (m_heldCardVisual.IsShown())
		{
			m_heldCardVisual.Hide();
			m_heldCardOffscreen = true;
		}
	}
}
