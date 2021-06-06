using System;
using System.Collections.Generic;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class CollectionInputMgr : MonoBehaviour
{
	// Token: 0x0600106E RID: 4206 RVA: 0x0005B9BF File Offset: 0x00059BBF
	private void Awake()
	{
		CollectionInputMgr.s_instance = this;
		UniversalInputManager.Get().RegisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0005B9DE File Offset: 0x00059BDE
	private void OnDestroy()
	{
		CollectionInputMgr.s_instance = null;
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0005B9E6 File Offset: 0x00059BE6
	private void Update()
	{
		this.UpdateHeldCard();
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0005B9EE File Offset: 0x00059BEE
	public static CollectionInputMgr Get()
	{
		return CollectionInputMgr.s_instance;
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0005B9F5 File Offset: 0x00059BF5
	public void Unload()
	{
		UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0005BA10 File Offset: 0x00059C10
	public bool HandleKeyboardInput()
	{
		if (CollectionDeckTray.Get() != null && (InputCollection.GetKey(KeyCode.LeftCommand) || InputCollection.GetKey(KeyCode.LeftControl) || InputCollection.GetKey(KeyCode.RightCommand) || InputCollection.GetKey(KeyCode.RightControl)))
		{
			bool flag = CollectionDeckTray.Get().IsShowingDeckContents();
			if (InputCollection.GetKeyDown(KeyCode.C) && flag)
			{
				CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
				DeckRuleViolation violation;
				if (editedDeck.CanCopyAsShareableDeck(out violation))
				{
					ClipboardUtils.CopyToClipboard(editedDeck.GetShareableDeck().Serialize(true));
					UIStatus.Get().AddInfo(GameStrings.Get("GLUE_COLLECTION_DECK_COPIED_TOAST"));
				}
				else
				{
					string userFriendlyCopyErrorMessageFromDeckRuleViolation = CollectionDeck.GetUserFriendlyCopyErrorMessageFromDeckRuleViolation(violation);
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
			TAG_PREMIUM tag_PREMIUM = TAG_PREMIUM.GOLDEN;
			if (CollectionManager.Get().GetPreferredPremium() == TAG_PREMIUM.GOLDEN)
			{
				tag_PREMIUM = TAG_PREMIUM.NORMAL;
			}
			Debug.Log("setting premium preference " + tag_PREMIUM);
			CollectionManager.Get().SetPremiumPreference(tag_PREMIUM);
			return true;
		}
		return false;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0005BBAC File Offset: 0x00059DAC
	public static void PasteDeckFromClipboard()
	{
		ShareableDeck shareableDeck = ShareableDeck.DeserializeFromClipboard();
		if (shareableDeck == null)
		{
			return;
		}
		CollectionInputMgr.PasteDeckInEditModeFromShareableDeck(shareableDeck);
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0005BBCC File Offset: 0x00059DCC
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
				EntityDef entityDef = defLoader.GetEntityDef(deckCardData.Def.Asset, true);
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
						int dbId = GameUtils.TranslateCardIdToDbId(cardsWithCardID[j], false);
						list.Add(new DeckMaker.DeckFill
						{
							m_addCard = defLoader.GetEntityDef(dbId, true)
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
				EntityDef entityDef2 = defLoader.GetEntityDef(deckCardData2.Def.Asset, true);
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
			DeckRuleset deckRuleset = CollectionManager.Get().GetDeckRuleset();
			int num2 = (deckRuleset == null) ? int.MinValue : deckRuleset.GetDeckSize(editedDeck2);
			if (editedDeck2 != null && (editedDeck2.HasReplaceableSlot() || editedDeck2.GetTotalCardCount() < num2))
			{
				CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(addedCards);
			}
		};
		CollectionDeckTray.Get().PopulateDeck(list, completedCallback);
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0005BE68 File Offset: 0x0005A068
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

	// Token: 0x06001078 RID: 4216 RVA: 0x0005BEBC File Offset: 0x0005A0BC
	public bool GrabCard(CollectionCardVisual cardVisual)
	{
		Actor preferredActor = cardVisual.GetCollectionCardActors().GetPreferredActor();
		if (!this.CanGrabItem(preferredActor))
		{
			return false;
		}
		this.m_heldCardVisual.SetSlot(null);
		if (!this.m_heldCardVisual.ChangeActor(preferredActor, cardVisual.GetVisualType(), preferredActor.GetPremium()))
		{
			return false;
		}
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.Pause(true);
		}
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		CollectionCardBack component = preferredActor.GetComponent<CollectionCardBack>();
		if (component != null)
		{
			this.m_heldCardVisual.SetCardBackId(component.GetCardBackId());
		}
		this.m_heldCardVisual.transform.position = preferredActor.transform.position;
		this.m_heldCardVisual.Show(this.m_mouseIsOverDeck);
		SoundManager.Get().LoadAndPlay("collection_manager_pick_up_card.prefab:f7fb595cdc26f2f4997b4a10eaf1d0e1", this.m_heldCardVisual.gameObject);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.HideFilterTrayOnStartDragCard();
		}
		return true;
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0005BFB9 File Offset: 0x0005A1B9
	public bool GrabCard(DeckTrayDeckTileVisual deckTileVisual, CollectionInputMgr.OnCardDroppedCallback callback, bool removeCard = true)
	{
		this.m_cardDroppedCallback = callback;
		return this.GrabCard(deckTileVisual, removeCard);
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0005BFCC File Offset: 0x0005A1CC
	public bool GrabCard(DeckTrayDeckTileVisual deckTileVisual, bool removeCard = true)
	{
		Actor actor = deckTileVisual.GetActor();
		if (!this.CanGrabItem(actor))
		{
			return false;
		}
		this.m_heldCardVisual.SetSlot(deckTileVisual.GetSlot());
		if (!this.m_heldCardVisual.ChangeActor(actor, CollectionUtils.ViewMode.CARDS, deckTileVisual.GetSlot().UnPreferredPremium))
		{
			return false;
		}
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.Pause(true);
		}
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		this.m_heldCardVisual.transform.position = actor.transform.position;
		this.m_heldCardVisual.Show(this.m_mouseIsOverDeck);
		SoundManager.Get().LoadAndPlay("collection_manager_pick_up_card.prefab:f7fb595cdc26f2f4997b4a10eaf1d0e1", this.m_heldCardVisual.gameObject);
		if (DuelsConfig.IsCardLoadoutTreasure(this.m_heldCardVisual.GetCardID()))
		{
			removeCard = false;
		}
		if (removeCard)
		{
			CollectionDeck editingDeck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
			CollectionDeckTray.Get().RemoveCard(this.m_heldCardVisual.GetCardID(), deckTileVisual.GetSlot().UnPreferredPremium, editingDeck.IsValidSlot(deckTileVisual.GetSlot(), false, false, false));
			if (!Options.Get().GetBool(Option.HAS_REMOVED_CARD_FROM_DECK, false))
			{
				CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
				Options.Get().SetBool(Option.HAS_REMOVED_CARD_FROM_DECK, true);
			}
		}
		return true;
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0005C112 File Offset: 0x0005A312
	public void DropCard(DeckTrayDeckTileVisual deckTileToRemove)
	{
		this.DropCard(false, deckTileToRemove);
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0005C11C File Offset: 0x0005A31C
	public void SetScrollbar(UIBScrollable scrollbar)
	{
		this.m_scrollBar = scrollbar;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0005C125 File Offset: 0x0005A325
	public bool IsDraggingScrollbar()
	{
		return this.m_scrollBar != null && this.m_scrollBar.IsDragging();
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0005C142 File Offset: 0x0005A342
	public bool HasHeldCard()
	{
		return this.m_heldCardVisual.IsShown();
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0005C14F File Offset: 0x0005A34F
	private bool CanGrabItem(Actor actor)
	{
		return !this.IsDraggingScrollbar() && !this.m_heldCardVisual.IsShown() && !(actor == null);
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0005C178 File Offset: 0x0005A378
	private void UpdateHeldCard()
	{
		if (!this.m_heldCardVisual.IsShown())
		{
			return;
		}
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.DragPlane.LayerBit(), out raycastHit))
		{
			return;
		}
		if (this.m_heldCardVisual != null && UniversalInputManager.UsePhoneUI)
		{
			Transform[] componentsInChildren = this.m_heldCardVisual.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = 19;
			}
		}
		Vector3 point = raycastHit.point;
		if (UniversalInputManager.UsePhoneUI)
		{
			point.y += CollectionInputMgr.PHONE_HEIGHT_OFFSET;
		}
		this.m_heldCardVisual.transform.position = point;
		if (CollectionDeckTray.Get() != null)
		{
			this.m_mouseIsOverDeck = CollectionDeckTray.Get().MouseIsOver();
			this.m_heldCardVisual.UpdateVisual(this.m_mouseIsOverDeck);
		}
		if (DraftPhoneDeckTray.Get() != null)
		{
			this.m_mouseIsOverDeck = DraftPhoneDeckTray.Get().MouseIsOver();
			this.m_heldCardVisual.UpdateVisual(this.m_mouseIsOverDeck);
		}
		if (InputCollection.GetMouseButtonUp(0))
		{
			this.DropCard(null);
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0005C298 File Offset: 0x0005A498
	private void DropCard(bool dragCanceled, DeckTrayDeckTileVisual deckTileToRemove)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		if (!dragCanceled)
		{
			if (this.m_mouseIsOverDeck)
			{
				switch (this.m_heldCardVisual.GetVisualType())
				{
				case CollectionUtils.ViewMode.CARDS:
					if (CollectionDeckTray.Get() != null && !DuelsConfig.IsCardLoadoutTreasure(this.m_heldCardVisual.GetCardID()) && CollectionDeckTray.Get().AddCard(this.m_heldCardVisual.GetEntityDef(), this.m_heldCardVisual.GetPremium(), deckTileToRemove, true, null, true))
					{
						CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(this.m_heldCardVisual.GetEntityDef());
					}
					break;
				case CollectionUtils.ViewMode.HERO_SKINS:
					if (CollectionDeckTray.Get() != null)
					{
						CollectionDeckTray.Get().GetHeroSkinContent().UpdateHeroSkin(this.m_heldCardVisual.GetEntityDef(), this.m_heldCardVisual.GetPremium(), true);
					}
					break;
				case CollectionUtils.ViewMode.CARD_BACKS:
				{
					object obj = this.m_heldCardVisual.GetCardBackId();
					if (obj == null)
					{
						Debug.LogWarning("Cardback ID not set for dragging card back.");
					}
					else if (CollectionDeckTray.Get() != null)
					{
						CollectionDeckTray.Get().GetCardBackContent().UpdateCardBack((int)obj, true, null);
					}
					break;
				}
				}
			}
			else
			{
				SoundManager.Get().LoadAndPlay("collection_manager_drop_card.prefab:8275e45efb8280347b35c2548e706d84", this.m_heldCardVisual.gameObject);
				if (this.m_cardDroppedCallback != null)
				{
					this.m_cardDroppedCallback();
					this.m_cardDroppedCallback = null;
				}
			}
		}
		this.m_heldCardVisual.Hide();
		if (this.m_scrollBar != null)
		{
			this.m_scrollBar.Pause(false);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			if (!dragCanceled && this.m_mouseIsOverDeck)
			{
				collectionManagerDisplay.WaitThenUnhideFilterTrayOnStopDragCard();
				return;
			}
			collectionManagerDisplay.UnhideFilterTrayOnStopDragCard();
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0005C45C File Offset: 0x0005A65C
	private void OnMouseOnOrOffScreen(bool onScreen)
	{
		if (this.m_heldCardVisual == null || this.m_heldCardVisual.gameObject == null)
		{
			return;
		}
		if (onScreen)
		{
			if (this.m_heldCardOffscreen)
			{
				this.m_heldCardOffscreen = false;
				if (UniversalInputManager.Get().GetMouseButton(0))
				{
					this.m_heldCardVisual.Show(this.m_mouseIsOverDeck);
					return;
				}
				this.DropCard(true, null);
				return;
			}
		}
		else if (this.m_heldCardVisual.IsShown())
		{
			this.m_heldCardVisual.Hide();
			this.m_heldCardOffscreen = true;
		}
	}

	// Token: 0x04000AFF RID: 2815
	public CollectionDraggableCardVisual m_heldCardVisual;

	// Token: 0x04000B00 RID: 2816
	public Collider TooltipPlane;

	// Token: 0x04000B01 RID: 2817
	public static readonly PlatformDependentValue<float> PHONE_HEIGHT_OFFSET = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		Phone = 10f
	};

	// Token: 0x04000B02 RID: 2818
	private static CollectionInputMgr s_instance;

	// Token: 0x04000B03 RID: 2819
	private bool m_heldCardOffscreen;

	// Token: 0x04000B04 RID: 2820
	private bool m_mouseIsOverDeck;

	// Token: 0x04000B05 RID: 2821
	private UIBScrollable m_scrollBar;

	// Token: 0x04000B06 RID: 2822
	public CollectionInputMgr.OnCardDroppedCallback m_cardDroppedCallback;

	// Token: 0x02001442 RID: 5186
	// (Invoke) Token: 0x0600DA2F RID: 55855
	public delegate void OnCardDroppedCallback();
}
