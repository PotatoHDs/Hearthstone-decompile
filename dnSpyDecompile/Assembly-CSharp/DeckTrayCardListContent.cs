using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

// Token: 0x0200088E RID: 2190
[CustomEditClass]
public class DeckTrayCardListContent : DeckTrayContent
{
	// Token: 0x170006FA RID: 1786
	// (get) Token: 0x060077B0 RID: 30640 RVA: 0x00271339 File Offset: 0x0026F539
	private float TemplateDeckHelpButtonHeight
	{
		get
		{
			return this.m_deckTemplateHelpButton.GetComponent<UIBScrollableItem>().m_size.z * this.m_cardTileSlotLocalScaleVec3.z;
		}
	}

	// Token: 0x060077B1 RID: 30641 RVA: 0x0027135C File Offset: 0x0026F55C
	protected override void Awake()
	{
		base.Awake();
		if (this.m_smartDeckCompleteButton != null)
		{
			this.m_smartDeckCompleteButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeckCompleteButtonPress));
			this.m_smartDeckCompleteButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnDeckCompleteButtonOver));
			this.m_smartDeckCompleteButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnDeckCompleteButtonOut));
		}
		if (this.m_deckTemplateHelpButton != null)
		{
			this.m_deckTemplateHelpButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeckTemplateHelpButtonPress));
			this.m_deckTemplateHelpButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnDeckTemplateHelpButtonOver));
			this.m_deckTemplateHelpButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnDeckTemplateHelpButtonOut));
		}
		this.m_originalLocalPosition = base.transform.localPosition;
		this.m_hasFinishedEntering = false;
	}

	// Token: 0x060077B2 RID: 30642 RVA: 0x00271439 File Offset: 0x0026F639
	protected override void OnDestroy()
	{
		this.m_cardDefs.DisposeValuesAndClear<DefLoader.DisposableCardDef>();
		base.OnDestroy();
	}

	// Token: 0x060077B3 RID: 30643 RVA: 0x0027144C File Offset: 0x0026F64C
	public override bool AnimateContentEntranceStart()
	{
		if (this.m_loading)
		{
			return false;
		}
		this.m_animating = true;
		this.m_hasFinishedEntering = false;
		Action<object> action = delegate(object _1)
		{
			this.UpdateDeckCompleteHighlight();
			this.ShowDeckEditingTipsIfNeeded();
			this.m_animating = false;
		};
		CollectionDeck editingDeck = this.GetEditingDeck();
		if (editingDeck != null)
		{
			base.transform.localPosition = this.GetOffscreenLocalPosition();
			iTween.StopByName(base.gameObject, "position");
			iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
			{
				"position",
				this.m_originalLocalPosition,
				"isLocal",
				true,
				"time",
				0.3f,
				"easeType",
				iTween.EaseType.easeOutQuad,
				"oncomplete",
				action,
				"name",
				"position"
			}));
			if (editingDeck.GetTotalCardCount() > 0)
			{
				SoundManager.Get().LoadAndPlay("collection_manager_new_deck_moves_up_tray.prefab:13650cd587089e14d9a297c8de6057f1", base.gameObject);
			}
			this.UpdateCardList(false, null, null);
		}
		else
		{
			action(null);
		}
		return true;
	}

	// Token: 0x060077B4 RID: 30644 RVA: 0x00271565 File Offset: 0x0026F765
	public override bool AnimateContentEntranceEnd()
	{
		if (this.m_animating)
		{
			return false;
		}
		this.m_hasFinishedEntering = true;
		this.FireCardCountChangedEvent();
		return true;
	}

	// Token: 0x060077B5 RID: 30645 RVA: 0x00271580 File Offset: 0x0026F780
	public override bool AnimateContentExitStart()
	{
		if (this.m_animating)
		{
			return false;
		}
		this.m_animating = true;
		this.m_hasFinishedExiting = false;
		if (this.m_deckCompleteHighlight != null)
		{
			this.m_deckCompleteHighlight.SetActive(false);
		}
		iTween.StopByName(base.gameObject, "position");
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			this.GetOffscreenLocalPosition(),
			"isLocal",
			true,
			"time",
			0.3f,
			"easeType",
			iTween.EaseType.easeInQuad,
			"name",
			"position"
		}));
		if (HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown())
		{
			SoundManager.Get().LoadAndPlay("panel_slide_off_deck_creation_screen.prefab:b0d25fc984ec05d4fbea7480b611e5ad", base.gameObject);
		}
		Processor.ScheduleCallback(0.5f, false, delegate(object o)
		{
			this.m_animating = false;
		}, null);
		return true;
	}

	// Token: 0x060077B6 RID: 30646 RVA: 0x00271691 File Offset: 0x0026F891
	public override bool AnimateContentExitEnd()
	{
		this.m_hasFinishedExiting = true;
		return !this.m_animating;
	}

	// Token: 0x060077B7 RID: 30647 RVA: 0x002716A3 File Offset: 0x0026F8A3
	public bool HasFinishedEntering()
	{
		return this.m_hasFinishedEntering;
	}

	// Token: 0x060077B8 RID: 30648 RVA: 0x002716AB File Offset: 0x0026F8AB
	public bool HasFinishedExiting()
	{
		return this.m_hasFinishedExiting;
	}

	// Token: 0x060077B9 RID: 30649 RVA: 0x002716B4 File Offset: 0x0026F8B4
	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		if (newDeck == null)
		{
			return;
		}
		List<CollectionDeckSlot> slots = newDeck.GetSlots();
		this.LoadCardPrefabs(slots);
		if (base.IsModeActive())
		{
			this.ShowDeckHelpButtonIfNeeded();
		}
	}

	// Token: 0x060077BA RID: 30650 RVA: 0x002716E4 File Offset: 0x0026F8E4
	public void ShowDeckHelper(DeckTrayDeckTileVisual tileToRemove, bool continueAfterReplace, bool replacingCard = false)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			return;
		}
		if (!DeckHelper.Get())
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

	// Token: 0x060077BB RID: 30651 RVA: 0x00271746 File Offset: 0x0026F946
	public bool MouseIsOverDeckHelperButton()
	{
		return this.m_smartDeckCompleteButton != null && this.m_smartDeckCompleteButton.gameObject.activeInHierarchy && UniversalInputManager.Get().InputIsOver(this.m_smartDeckCompleteButton.gameObject);
	}

	// Token: 0x060077BC RID: 30652 RVA: 0x00271780 File Offset: 0x0026F980
	public bool MouseIsOverDeckCardTile()
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (UniversalInputManager.Get().InputIsOver(deckTrayDeckTileVisual.gameObject))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060077BD RID: 30653 RVA: 0x002717E8 File Offset: 0x0026F9E8
	public Vector3 GetCardVisualExtents()
	{
		return new Vector3(this.m_cardTileHeight, this.m_cardTileHeight, this.m_cardTileHeight);
	}

	// Token: 0x060077BE RID: 30654 RVA: 0x00271801 File Offset: 0x0026FA01
	public List<DeckTrayDeckTileVisual> GetCardTiles()
	{
		return this.m_cardTiles;
	}

	// Token: 0x060077BF RID: 30655 RVA: 0x0027180C File Offset: 0x0026FA0C
	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID)
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (!(deckTrayDeckTileVisual == null) && !(deckTrayDeckTileVisual.GetActor() == null) && deckTrayDeckTileVisual.GetActor().GetEntityDef() != null && deckTrayDeckTileVisual.GetActor().GetEntityDef().GetCardId() == cardID)
			{
				return deckTrayDeckTileVisual;
			}
		}
		return null;
	}

	// Token: 0x060077C0 RID: 30656 RVA: 0x0027189C File Offset: 0x0026FA9C
	public DeckTrayDeckTileVisual GetCardTileVisual(string cardID, TAG_PREMIUM premType)
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (!(deckTrayDeckTileVisual == null) && !(deckTrayDeckTileVisual.GetActor() == null) && deckTrayDeckTileVisual.GetActor().GetEntityDef() != null && deckTrayDeckTileVisual.GetActor().GetEntityDef().GetCardId() == cardID && deckTrayDeckTileVisual.GetActor().GetPremium() == premType)
			{
				return deckTrayDeckTileVisual;
			}
		}
		return null;
	}

	// Token: 0x060077C1 RID: 30657 RVA: 0x0027193C File Offset: 0x0026FB3C
	public DeckTrayDeckTileVisual GetCardTileVisual(int index)
	{
		if (index < this.m_cardTiles.Count)
		{
			return this.m_cardTiles[index];
		}
		return null;
	}

	// Token: 0x060077C2 RID: 30658 RVA: 0x0027195C File Offset: 0x0026FB5C
	public DeckTrayDeckTileVisual GetCardTileVisualOrLastVisible(string cardID)
	{
		int num = 0;
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			num++;
			if (!(deckTrayDeckTileVisual == null) && !(deckTrayDeckTileVisual.GetActor() == null) && deckTrayDeckTileVisual.GetActor().GetEntityDef() != null)
			{
				if (num > 20)
				{
					return deckTrayDeckTileVisual;
				}
				if (deckTrayDeckTileVisual.GetActor().GetEntityDef().GetCardId() == cardID)
				{
					return deckTrayDeckTileVisual;
				}
			}
		}
		return null;
	}

	// Token: 0x060077C3 RID: 30659 RVA: 0x002719FC File Offset: 0x0026FBFC
	public DeckTrayDeckTileVisual GetOrAddCardTileVisual(int index)
	{
		DeckTrayDeckTileVisual newTileVisual = this.GetCardTileVisual(index);
		if (newTileVisual != null)
		{
			return newTileVisual;
		}
		GameObject gameObject = new GameObject("DeckTileVisual" + index);
		GameUtils.SetParent(gameObject, this, false);
		gameObject.transform.localScale = this.m_cardTileSlotLocalScaleVec3;
		newTileVisual = gameObject.AddComponent<DeckTrayDeckTileVisual>();
		bool useFullScaleDeckTileActor = !UniversalInputManager.UsePhoneUI || this.m_forceUseFullScaleDeckTileActors;
		newTileVisual.Initialize(useFullScaleDeckTileActor);
		newTileVisual.AddEventListener(UIEventType.DRAG, delegate(UIEvent e)
		{
			this.FireCardTileDragEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
		{
			this.FireCardTilePressEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.TAP, delegate(UIEvent e)
		{
			this.FireCardTileTapEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
		{
			this.FireCardTileOverEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
		{
			this.FireCardTileOutEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.FireCardTileReleaseEvent(newTileVisual);
		});
		newTileVisual.AddEventListener(UIEventType.RIGHTCLICK, delegate(UIEvent e)
		{
			this.FireCardTileRightClickedEvent(newTileVisual);
		});
		this.m_cardTiles.Insert(index, newTileVisual);
		Vector3 extents = new Vector3(this.m_cardTileHeight, this.m_cardTileHeight, this.m_cardTileHeight);
		if (this.m_scrollbar != null)
		{
			this.m_scrollbar.AddVisibleAffectedObject(gameObject, extents, true, new UIBScrollable.VisibleAffected(this.IsCardTileVisible));
		}
		return newTileVisual;
	}

	// Token: 0x060077C4 RID: 30660 RVA: 0x00271BA0 File Offset: 0x0026FDA0
	public void UpdateTileVisuals()
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			deckTrayDeckTileVisual.UpdateGhostedState();
		}
	}

	// Token: 0x060077C5 RID: 30661 RVA: 0x00271BF0 File Offset: 0x0026FDF0
	public override void Show(bool showAll = false)
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (showAll || deckTrayDeckTileVisual.IsInUse())
			{
				deckTrayDeckTileVisual.Show();
			}
		}
	}

	// Token: 0x060077C6 RID: 30662 RVA: 0x00271C50 File Offset: 0x0026FE50
	public override void Hide(bool hideAll = false)
	{
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (hideAll || !deckTrayDeckTileVisual.IsInUse())
			{
				deckTrayDeckTileVisual.Hide();
			}
		}
	}

	// Token: 0x060077C7 RID: 30663 RVA: 0x00271CB0 File Offset: 0x0026FEB0
	public void CommitFakeDeckChanges()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		editedDeck.CopyContents(this.m_templateFakeDeck);
		editedDeck.Name = this.m_templateFakeDeck.Name;
	}

	// Token: 0x060077C8 RID: 30664 RVA: 0x00271CD8 File Offset: 0x0026FED8
	public CollectionDeck GetEditingDeck()
	{
		if (this.m_isShowingFakeDeck)
		{
			if (CollectionManager.Get() != null)
			{
				CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
				if (editedDeck != null)
				{
					this.m_templateFakeDeck.FormatType = editedDeck.FormatType;
				}
			}
			if (this.m_templateFakeDeck.FormatType == FormatType.FT_UNKNOWN)
			{
				Debug.LogError("CollectionDeck.GetEditingDeck could not determine the format type for the fake deck " + this.m_templateFakeDeck.ToString());
			}
		}
		if (!this.m_isShowingFakeDeck)
		{
			return CollectionManager.Get().GetEditedDeck();
		}
		return this.m_templateFakeDeck;
	}

	// Token: 0x060077C9 RID: 30665 RVA: 0x00271D53 File Offset: 0x0026FF53
	public void ShowFakeDeck(bool show)
	{
		if (this.m_isShowingFakeDeck == show)
		{
			return;
		}
		this.m_isShowingFakeDeck = show;
		this.UpdateCardList();
	}

	// Token: 0x060077CA RID: 30666 RVA: 0x00271D6C File Offset: 0x0026FF6C
	public void ResetFakeDeck()
	{
		if (this.m_templateFakeDeck == null)
		{
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			return;
		}
		this.m_templateFakeDeck.CopyContents(editedDeck);
		this.m_templateFakeDeck.Name = editedDeck.Name;
	}

	// Token: 0x060077CB RID: 30667 RVA: 0x00271DAE File Offset: 0x0026FFAE
	public void ShowDeckCompleteEffects()
	{
		base.StartCoroutine(this.ShowDeckCompleteEffectsWithInterval(this.m_deckCardBarFlareUpInterval));
	}

	// Token: 0x060077CC RID: 30668 RVA: 0x00271DC3 File Offset: 0x0026FFC3
	public void SetInArena(bool inArena)
	{
		this.m_inArena = inArena;
	}

	// Token: 0x060077CD RID: 30669 RVA: 0x00271DCC File Offset: 0x0026FFCC
	public bool AddCard(EntityDef cardEntityDef, TAG_PREMIUM premium, DeckTrayDeckTileVisual deckTileToRemove, bool playSound, ref EntityDef removedCard, Actor animateFromActor = null, bool allowInvalid = false, bool updateVisuals = true)
	{
		if (!base.IsModeActive())
		{
			return false;
		}
		if (cardEntityDef == null)
		{
			Debug.LogError("Trying to add card EntityDef that is null.");
			return false;
		}
		string cardId = cardEntityDef.GetCardId();
		CollectionDeck editingDeck = this.GetEditingDeck();
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
		bool flag = editingDeck.GetTotalCardCount() == CollectionManager.Get().GetDeckSize();
		DeckTrayDeckTileVisual firstInvalidCard = this.GetFirstInvalidCard();
		if (flag && (deckTileToRemove == null || editingDeck.IsValidSlot(deckTileToRemove.GetSlot(), false, false, false)) && firstInvalidCard != null)
		{
			deckTileToRemove = firstInvalidCard;
		}
		if (flag || (deckTileToRemove != null && !editingDeck.IsValidSlot(deckTileToRemove.GetSlot(), false, false, false)))
		{
			if (deckTileToRemove == null)
			{
				GameplayErrorManager.Get().DisplayMessage(GameStrings.Get("GLUE_COLLECTION_MANAGER_ON_ADD_FULL_DECK_ERROR_TEXT"));
				Debug.LogWarning(string.Format("DeckTrayCardListContent.AddCard(): Cannot add card {0} (premium {1}) without removing one first.", cardEntityDef.GetCardId(), premium));
				return false;
			}
			deckTileToRemove.SetHighlight(false);
			string cardID = deckTileToRemove.GetCardID();
			TAG_PREMIUM premium2 = deckTileToRemove.GetPremium();
			if (!editingDeck.RemoveCard(cardID, premium2, editingDeck.IsValidSlot(deckTileToRemove.GetSlot(), false, false, false)))
			{
				Debug.LogWarning(string.Format("DeckTrayCardListContent.AddCard({0},{1}): Tried to remove card {2} with premium {3}, but it failed!", new object[]
				{
					cardId,
					premium,
					cardID,
					premium2
				}));
				return false;
			}
			removedCard = deckTileToRemove.GetActor().GetEntityDef();
		}
		if (!editingDeck.AddCard(cardEntityDef, premium, false))
		{
			Debug.LogWarning(string.Format("DeckTrayCardListContent.AddCard({0},{1}): deck.AddCard failed!", cardId, premium));
			return false;
		}
		if (updateVisuals)
		{
			this.UpdateCardList(cardEntityDef, true, animateFromActor, null);
			CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(true);
		}
		DeckHelper.Get().OnCardAdded(editingDeck);
		if (!Options.Get().GetBool(Option.HAS_ADDED_CARDS_TO_DECK, false) && editingDeck.GetTotalCardCount() >= 2 && !DeckHelper.Get().IsActive() && editingDeck.GetTotalCardCount() < 15 && UserAttentionManager.CanShowAttentionGrabber("DeckTrayCardListContent.AddCard:" + Option.HAS_ADDED_CARDS_TO_DECK))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_PAGEFLIP_28"), "VO_INNKEEPER_CM_PAGEFLIP_28.prefab:47bb7bdb89ad93443ab7d031bbe666fb", 0f, null, false);
			Options.Get().SetBool(Option.HAS_ADDED_CARDS_TO_DECK, true);
		}
		return true;
	}

	// Token: 0x060077CE RID: 30670 RVA: 0x00272010 File Offset: 0x00270210
	public int RemoveClosestInvalidCard(EntityDef entityDef, int sameRemoveCount)
	{
		CollectionDeck editingDeck = this.GetEditingDeck();
		int cost = entityDef.GetCost();
		int num = int.MaxValue;
		string text = string.Empty;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		foreach (CollectionDeckSlot collectionDeckSlot in editingDeck.GetSlots())
		{
			if (!editingDeck.IsValidSlot(collectionDeckSlot, false, false, false))
			{
				EntityDef entityDef2 = DefLoader.Get().GetEntityDef(collectionDeckSlot.CardID);
				if (entityDef2 == entityDef)
				{
					text = entityDef.GetCardId();
					premium = collectionDeckSlot.UnPreferredPremium;
					break;
				}
				int num2 = Mathf.Abs(cost - entityDef2.GetCost());
				if (num2 < num)
				{
					num = num2;
					text = collectionDeckSlot.CardID;
				}
			}
		}
		int num3 = 0;
		if (!string.IsNullOrEmpty(text))
		{
			for (int i = 0; i < sameRemoveCount; i++)
			{
				if (editingDeck.RemoveCard(text, premium, false))
				{
					num3++;
				}
			}
		}
		this.UpdateCardList();
		return num3;
	}

	// Token: 0x060077CF RID: 30671 RVA: 0x00272108 File Offset: 0x00270308
	[ContextMenu("Update Card List")]
	public void UpdateCardList()
	{
		this.UpdateCardList(true, null, null);
	}

	// Token: 0x060077D0 RID: 30672 RVA: 0x00272113 File Offset: 0x00270313
	public void UpdateCardList(bool updateHighlight, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		this.UpdateCardList(string.Empty, updateHighlight, animateFromActor, onCompleteCallback);
	}

	// Token: 0x060077D1 RID: 30673 RVA: 0x00272123 File Offset: 0x00270323
	public void UpdateCardList(EntityDef justChangedCardEntityDef, bool updateHighlight = true, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		this.UpdateCardList((justChangedCardEntityDef != null) ? justChangedCardEntityDef.GetCardId() : string.Empty, updateHighlight, animateFromActor, onCompleteCallback);
	}

	// Token: 0x060077D2 RID: 30674 RVA: 0x00272140 File Offset: 0x00270340
	public void UpdateCardList(string justChangedCardID, bool updateHighlight = true, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		CollectionDeck editingDeck = this.GetEditingDeck();
		if (editingDeck == null)
		{
			return;
		}
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			deckTrayDeckTileVisual.MarkAsUnused();
		}
		List<CollectionDeckSlot> slots = editingDeck.GetSlots();
		int num = 0;
		Vector3 cardTileOffset = this.GetCardTileOffset(editingDeck);
		for (int i = 0; i < slots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = slots[i];
			if (collectionDeckSlot.Count == 0)
			{
				Log.DeckTray.Print(string.Format("DeckTrayCardListContent.UpdateCardList(): Slot {0} of deck is empty! Skipping...", i), Array.Empty<object>());
			}
			else
			{
				num += collectionDeckSlot.Count;
				DeckTrayDeckTileVisual orAddCardTileVisual = this.GetOrAddCardTileVisual(i);
				orAddCardTileVisual.SetInArena(this.m_inArena);
				orAddCardTileVisual.gameObject.transform.localPosition = cardTileOffset + Vector3.down * (this.m_cardTileSlotLocalHeight * (float)i);
				orAddCardTileVisual.MarkAsUsed();
				orAddCardTileVisual.Show();
				orAddCardTileVisual.SetSlot(editingDeck, collectionDeckSlot, justChangedCardID.Equals(collectionDeckSlot.CardID));
			}
		}
		this.Hide(false);
		this.ShowDeckHelpButtonIfNeeded();
		this.FireCardCountChangedEvent();
		if (this.m_scrollbar != null)
		{
			this.m_scrollbar.UpdateScroll();
		}
		if (updateHighlight)
		{
			this.UpdateDeckCompleteHighlight();
		}
		if (animateFromActor != null && base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.ShowAddCardAnimationAfterTrayLoads(animateFromActor, onCompleteCallback));
			return;
		}
		if (onCompleteCallback != null)
		{
			onCompleteCallback();
		}
	}

	// Token: 0x060077D3 RID: 30675 RVA: 0x002722D0 File Offset: 0x002704D0
	private Vector3 GetCardTileOffset(CollectionDeck currentDeck)
	{
		Vector3 a = Vector3.zero;
		if (!this.m_isShowingFakeDeck && currentDeck != null && this.m_deckTemplateHelpButton != null)
		{
			bool flag = true;
			if (SceneMgr.Get().IsInTavernBrawlMode())
			{
				flag = TavernBrawlDisplay.IsTavernBrawlEditing();
			}
			if (flag && currentDeck.GetTotalInvalidCardCount() > 0)
			{
				a = Vector3.down * this.TemplateDeckHelpButtonHeight;
			}
		}
		return a + this.m_cardTileOffset;
	}

	// Token: 0x060077D4 RID: 30676 RVA: 0x00272339 File Offset: 0x00270539
	public void TriggerCardCountUpdate()
	{
		this.FireCardCountChangedEvent();
	}

	// Token: 0x060077D5 RID: 30677 RVA: 0x00272341 File Offset: 0x00270541
	public void HideDeckHelpPopup()
	{
		if (this.m_deckHelpPopup != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_deckHelpPopup, 0f);
		}
	}

	// Token: 0x060077D6 RID: 30678 RVA: 0x00272368 File Offset: 0x00270568
	public DeckTrayDeckTileVisual GetFirstInvalidCard()
	{
		CollectionDeck editingDeck = this.GetEditingDeck();
		if (editingDeck != null)
		{
			foreach (CollectionDeckSlot collectionDeckSlot in editingDeck.GetSlots())
			{
				if (!editingDeck.IsValidSlot(collectionDeckSlot, false, false, false))
				{
					return this.GetCardTileVisual(collectionDeckSlot.Index);
				}
			}
		}
		return null;
	}

	// Token: 0x060077D7 RID: 30679 RVA: 0x002723DC File Offset: 0x002705DC
	private static bool ShouldShowDeckCompleteHighlight(CollectionDeck deck)
	{
		DeckType type = deck.Type;
		return type != DeckType.CLIENT_ONLY_DECK && type != DeckType.DRAFT_DECK;
	}

	// Token: 0x060077D8 RID: 30680 RVA: 0x002723FC File Offset: 0x002705FC
	public void UpdateDeckCompleteHighlight()
	{
		CollectionDeck editingDeck = this.GetEditingDeck();
		if (editingDeck == null)
		{
			return;
		}
		if (!DeckTrayCardListContent.ShouldShowDeckCompleteHighlight(editingDeck))
		{
			return;
		}
		bool flag = editingDeck != null && editingDeck.GetTotalValidCardCount() == CollectionManager.Get().GetDeckSize();
		bool flag2 = editingDeck != null && editingDeck.Locked;
		if (this.m_scrollbar != null && this.m_LockedScrollBounds != null && flag2)
		{
			this.m_scrollbar.m_ScrollBounds.center = this.m_LockedScrollBounds.center;
			this.m_scrollbar.m_ScrollBounds.size = this.m_LockedScrollBounds.size;
		}
		if (this.m_deckCompleteHighlight != null)
		{
			if (flag2)
			{
				this.m_deckCompleteHighlight.SetActive(false);
			}
			else
			{
				this.m_deckCompleteHighlight.SetActive(flag);
			}
		}
		if (flag && !Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, false))
		{
			Options.Get().SetBool(Option.HAS_FINISHED_A_DECK, true);
		}
	}

	// Token: 0x060077D9 RID: 30681 RVA: 0x002724EA File Offset: 0x002706EA
	public Notification GetDeckHelpPopup()
	{
		return this.m_deckHelpPopup;
	}

	// Token: 0x060077DA RID: 30682 RVA: 0x002724F2 File Offset: 0x002706F2
	private IEnumerator ShowAddCardAnimationAfterTrayLoads(Actor cardToAnimate, Action onCompleteCallback)
	{
		string cardID = cardToAnimate.GetEntityDef().GetCardId();
		DeckTrayDeckTileVisual tile = this.GetCardTileVisual(cardID);
		Vector3 cardPos = cardToAnimate.transform.position;
		while (tile == null)
		{
			yield return null;
			tile = this.GetCardTileVisual(cardID);
		}
		GameObject cardTileObject = UnityEngine.Object.Instantiate<GameObject>(tile.GetActor().gameObject);
		Actor movingCardTile = cardTileObject.GetComponent<Actor>();
		if (this.GetEditingDeck().GetCardCountAllMatchingSlots(cardID) == 1)
		{
			tile.Hide();
		}
		else
		{
			tile.Show();
		}
		movingCardTile.transform.position = new Vector3(cardPos.x, cardPos.y + 2.5f, cardPos.z);
		if (UniversalInputManager.UsePhoneUI)
		{
			movingCardTile.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
		}
		else
		{
			movingCardTile.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		movingCardTile.ActivateAllSpellsDeathStates();
		movingCardTile.ActivateSpellBirthState(SpellType.SUMMON_IN_LARGE);
		if (UniversalInputManager.UsePhoneUI && this.m_phoneDeckTileBone != null)
		{
			iTween.MoveTo(cardTileObject, iTween.Hash(new object[]
			{
				"position",
				this.m_phoneDeckTileBone.transform.position,
				"time",
				0.5f,
				"easetype",
				iTween.EaseType.easeInCubic,
				"oncomplete",
				new Action<object>(delegate(object v)
				{
					tile.ShowAndSetupActor();
					tile.GetActor().GetSpell(SpellType.SUMMON_IN).ActivateState(SpellStateType.BIRTH);
					this.StartCoroutine(this.FinishPhoneMovingCardTile(cardTileObject, movingCardTile, 1f));
					if (onCompleteCallback != null)
					{
						onCompleteCallback();
					}
				})
			}));
			iTween.ScaleTo(cardTileObject, iTween.Hash(new object[]
			{
				"scale",
				new Vector3(0.5f, 1.1f, 1.1f),
				"time",
				0.5f,
				"easetype",
				iTween.EaseType.easeInCubic
			}));
		}
		else
		{
			Vector3[] newPath = new Vector3[3];
			Vector3 startSpot = movingCardTile.transform.position;
			newPath[0] = startSpot;
			iTween.ValueTo(cardTileObject, iTween.Hash(new object[]
			{
				"from",
				0f,
				"to",
				1f,
				"time",
				0.75f,
				"easetype",
				iTween.EaseType.easeOutCirc,
				"onupdate",
				new Action<object>(delegate(object val)
				{
					Vector3 position = tile.transform.position;
					newPath[1] = new Vector3((startSpot.x + position.x) * 0.5f, (startSpot.y + position.y) * 0.5f + 60f, (startSpot.z + position.z) * 0.5f);
					newPath[2] = position;
					iTween.PutOnPath(cardTileObject, newPath, (float)val);
				}),
				"oncomplete",
				new Action<object>(delegate(object v)
				{
					tile.ShowAndSetupActor();
					tile.GetActor().GetSpell(SpellType.SUMMON_IN).ActivateState(SpellStateType.BIRTH);
					movingCardTile.Hide();
					UnityEngine.Object.Destroy(cardTileObject);
					if (onCompleteCallback != null)
					{
						onCompleteCallback();
					}
				})
			}));
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef", base.gameObject);
		yield break;
	}

	// Token: 0x060077DB RID: 30683 RVA: 0x0027250F File Offset: 0x0027070F
	private IEnumerator FinishPhoneMovingCardTile(GameObject obj, Actor movingCardTile, float delay)
	{
		yield return new WaitForSeconds(delay);
		movingCardTile.Hide();
		UnityEngine.Object.Destroy(obj);
		yield break;
	}

	// Token: 0x060077DC RID: 30684 RVA: 0x0027252C File Offset: 0x0027072C
	private IEnumerator ShowDeckCompleteEffectsWithInterval(float interval)
	{
		if (this.m_scrollbar == null)
		{
			yield break;
		}
		bool needScroll = this.m_scrollbar.IsScrollNeeded();
		if (needScroll)
		{
			this.m_scrollbar.Enable(false);
			this.m_scrollbar.ForceVisibleAffectedObjectsShow(true);
			this.m_scrollbar.SetScroll(0f, iTween.EaseType.easeOutSine, 0.25f, true, true);
			yield return new WaitForSeconds(0.3f);
			this.m_scrollbar.SetScroll(1f, iTween.EaseType.easeInOutQuart, interval * (float)this.m_cardTiles.Count, true, true);
		}
		foreach (DeckTrayDeckTileVisual deckTrayDeckTileVisual in this.m_cardTiles)
		{
			if (!(deckTrayDeckTileVisual == null) && deckTrayDeckTileVisual.IsInUse())
			{
				deckTrayDeckTileVisual.GetActor().ActivateSpellBirthState(SpellType.SUMMON_IN_FORGE);
				yield return new WaitForSeconds(interval);
			}
		}
		List<DeckTrayDeckTileVisual>.Enumerator enumerator = default(List<DeckTrayDeckTileVisual>.Enumerator);
		foreach (DeckTrayDeckTileVisual tile in this.m_cardTiles)
		{
			if (!(tile == null) && tile.IsInUse())
			{
				yield return new WaitForSeconds(interval);
				tile.GetActor().DeactivateAllSpells();
				tile = null;
			}
		}
		enumerator = default(List<DeckTrayDeckTileVisual>.Enumerator);
		if (needScroll)
		{
			this.m_scrollbar.ForceVisibleAffectedObjectsShow(false);
			this.m_scrollbar.EnableIfNeeded();
		}
		yield break;
		yield break;
	}

	// Token: 0x060077DD RID: 30685 RVA: 0x00272544 File Offset: 0x00270744
	private void IsCardTileVisible(GameObject obj, bool visible)
	{
		if (obj.activeSelf != visible)
		{
			DeckTrayDeckTileVisual component = obj.GetComponent<DeckTrayDeckTileVisual>();
			if (component == null)
			{
				return;
			}
			if (visible && obj.GetComponent<DeckTrayDeckTileVisual>().IsInUse())
			{
				component.ShowAndSetupActor();
				return;
			}
			component.Hide();
		}
	}

	// Token: 0x060077DE RID: 30686 RVA: 0x00272588 File Offset: 0x00270788
	private void ShowDeckEditingTipsIfNeeded()
	{
		if (Options.Get().GetBool(Option.HAS_REMOVED_CARD_FROM_DECK, false))
		{
			return;
		}
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			return;
		}
		if (CollectionManager.Get().GetCollectibleDisplay() == null || CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.CARDS)
		{
			return;
		}
		if (this.m_cardTiles.Count <= 0)
		{
			return;
		}
		Transform removeCardTutorialBone = CollectionDeckTray.Get().m_removeCardTutorialBone;
		if (this.m_deckHelpPopup == null)
		{
			this.m_deckHelpPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, removeCardTutorialBone.position, removeCardTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL08"), true, NotificationManager.PopupTextType.BASIC);
			if (this.m_deckHelpPopup != null)
			{
				this.m_deckHelpPopup.PulseReminderEveryXSeconds(3f);
			}
		}
	}

	// Token: 0x060077DF RID: 30687 RVA: 0x00272648 File Offset: 0x00270848
	private void ShowDeckHelpButtonIfNeeded()
	{
		bool flag = false;
		if (CollectionManager.Get().GetCollectibleDisplay() == null)
		{
			return;
		}
		CollectionDeck editingDeck = this.GetEditingDeck();
		if (editingDeck != null && DeckHelper.Get() != null && editingDeck.GetTotalValidCardCount() < CollectionManager.Get().GetDeckSize())
		{
			flag = true;
		}
		bool active;
		if (editingDeck.GetTotalInvalidCardCount() > 0)
		{
			flag = false;
			active = true;
		}
		else
		{
			active = false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			active = false;
			flag = false;
		}
		if (TavernBrawlDisplay.IsTavernBrawlViewing() || SceneMgr.Get().IsInDuelsMode())
		{
			active = false;
			flag = false;
		}
		if (!DeckHelper.HasChoicesToOffer(editingDeck))
		{
			flag = false;
		}
		if (this.m_smartDeckCompleteButton != null)
		{
			this.m_smartDeckCompleteButton.gameObject.SetActive(flag);
			if (flag)
			{
				Vector3 cardTileOffset = this.GetCardTileOffset(editingDeck);
				cardTileOffset.y -= this.m_cardTileSlotLocalHeight * (float)editingDeck.GetSlots().Count;
				this.m_smartDeckCompleteButton.transform.localPosition = cardTileOffset;
			}
		}
		if (this.m_deckTemplateHelpButton != null)
		{
			this.m_deckTemplateHelpButton.gameObject.SetActive(active);
		}
		if (!Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, false))
		{
			if (this.m_smartDeckCompleteButton != null)
			{
				HighlightState componentInChildren = this.m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
				if (componentInChildren != null)
				{
					componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				}
			}
			if (this.m_deckTemplateHelpButton != null)
			{
				HighlightState componentInChildren2 = this.m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
				if (componentInChildren2 != null)
				{
					componentInChildren2.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				}
			}
		}
	}

	// Token: 0x060077E0 RID: 30688 RVA: 0x002727C8 File Offset: 0x002709C8
	private void OnDeckTemplateHelpButtonPress(UIEvent e)
	{
		Options.Get().SetBool(Option.HAS_CLICKED_DECK_TEMPLATE_REPLACE, true);
		DeckTrayDeckTileVisual firstInvalidCard = this.GetFirstInvalidCard();
		this.ShowDeckHelper(firstInvalidCard, true, firstInvalidCard != null);
	}

	// Token: 0x060077E1 RID: 30689 RVA: 0x002727FC File Offset: 0x002709FC
	private void OnDeckTemplateHelpButtonOver(UIEvent e)
	{
		HighlightState componentInChildren = this.m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			if (!Options.Get().GetBool(Option.HAS_FINISHED_A_DECK, false))
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

	// Token: 0x060077E2 RID: 30690 RVA: 0x00272860 File Offset: 0x00270A60
	private void OnDeckTemplateHelpButtonOut(UIEvent e)
	{
		HighlightState componentInChildren = this.m_deckTemplateHelpButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			if (!Options.Get().GetBool(Option.HAS_CLICKED_DECK_TEMPLATE_REPLACE, false))
			{
				componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				return;
			}
			componentInChildren.ChangeState(ActorStateType.NONE);
		}
	}

	// Token: 0x060077E3 RID: 30691 RVA: 0x002728A6 File Offset: 0x00270AA6
	private void OnDeckCompleteButtonPress(UIEvent e)
	{
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().CompleteMyDeckButtonPress();
		}
	}

	// Token: 0x060077E4 RID: 30692 RVA: 0x002728C0 File Offset: 0x00270AC0
	private void OnDeckCompleteButtonOver(UIEvent e)
	{
		if (CollectionInputMgr.Get().HasHeldCard())
		{
			return;
		}
		HighlightState componentInChildren = this.m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
		}
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9", base.gameObject);
	}

	// Token: 0x060077E5 RID: 30693 RVA: 0x00272914 File Offset: 0x00270B14
	private void OnDeckCompleteButtonOut(UIEvent e)
	{
		HighlightState componentInChildren = this.m_smartDeckCompleteButton.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.ChangeState(ActorStateType.NONE);
		}
	}

	// Token: 0x060077E6 RID: 30694 RVA: 0x00272940 File Offset: 0x00270B40
	private void LoadCardPrefabs(List<CollectionDeckSlot> deckSlots)
	{
		if (deckSlots.Count == 0)
		{
			return;
		}
		int prefabsToLoad = deckSlots.Count;
		this.m_loading = true;
		this.m_cardDefs.DisposeValuesAndClear<DefLoader.DisposableCardDef>();
		DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> <>9__0;
		for (int i = 0; i < deckSlots.Count; i++)
		{
			CollectionDeckSlot collectionDeckSlot = deckSlots[i];
			if (collectionDeckSlot.Count == 0)
			{
				Log.DeckTray.Print(string.Format("DeckTrayCardListContent.LoadCardPrefabs(): Slot {0} of deck is empty! Skipping...", i), Array.Empty<object>());
			}
			else
			{
				DefLoader defLoader = DefLoader.Get();
				string cardID = collectionDeckSlot.CardID;
				DefLoader.LoadDefCallback<DefLoader.DisposableCardDef> callback;
				if ((callback = <>9__0) == null)
				{
					callback = (<>9__0 = delegate(string cardId, DefLoader.DisposableCardDef def, object userData)
					{
						this.m_cardDefs.Add(def);
						int prefabsToLoad;
						prefabsToLoad--;
						prefabsToLoad = prefabsToLoad;
						if (prefabsToLoad == 0)
						{
							this.m_loading = false;
						}
					});
				}
				defLoader.LoadCardDef(cardID, callback, null, new CardPortraitQuality(1, false));
			}
		}
	}

	// Token: 0x060077E7 RID: 30695 RVA: 0x002729FC File Offset: 0x00270BFC
	private Vector3 GetOffscreenLocalPosition()
	{
		Vector3 originalLocalPosition = this.m_originalLocalPosition;
		CollectionDeck editingDeck = this.GetEditingDeck();
		int num = (editingDeck != null) ? (editingDeck.GetSlotCount() + 2) : 0;
		originalLocalPosition.z -= this.m_cardTileHeight * (float)num - this.GetCardTileOffset(editingDeck).y / this.m_cardTileSlotLocalScaleVec3.y;
		return originalLocalPosition;
	}

	// Token: 0x060077E8 RID: 30696 RVA: 0x00272A54 File Offset: 0x00270C54
	public void RegisterCardTileHeldListener(DeckTrayCardListContent.CardTileHeld dlg)
	{
		this.m_cardTileHeldListeners.Add(dlg);
	}

	// Token: 0x060077E9 RID: 30697 RVA: 0x00272A62 File Offset: 0x00270C62
	public void RegisterCardTilePressListener(DeckTrayCardListContent.CardTilePress dlg)
	{
		this.m_cardTilePressListeners.Add(dlg);
	}

	// Token: 0x060077EA RID: 30698 RVA: 0x00272A70 File Offset: 0x00270C70
	public void RegisterCardTileTapListener(DeckTrayCardListContent.CardTileTap dlg)
	{
		this.m_cardTileTapListeners.Add(dlg);
	}

	// Token: 0x060077EB RID: 30699 RVA: 0x00272A7E File Offset: 0x00270C7E
	public void RegisterCardTileOverListener(DeckTrayCardListContent.CardTileOver dlg)
	{
		this.m_cardTileOverListeners.Add(dlg);
	}

	// Token: 0x060077EC RID: 30700 RVA: 0x00272A8C File Offset: 0x00270C8C
	public void RegisterCardTileOutListener(DeckTrayCardListContent.CardTileOut dlg)
	{
		this.m_cardTileOutListeners.Add(dlg);
	}

	// Token: 0x060077ED RID: 30701 RVA: 0x00272A9A File Offset: 0x00270C9A
	public void RegisterCardTileReleaseListener(DeckTrayCardListContent.CardTileRelease dlg)
	{
		this.m_cardTileReleaseListeners.Add(dlg);
	}

	// Token: 0x060077EE RID: 30702 RVA: 0x00272AA8 File Offset: 0x00270CA8
	public void RegisterCardTileRightClickedListener(DeckTrayCardListContent.CardTileRightClicked dlg)
	{
		this.m_cardTileRightClickedListeners.Add(dlg);
	}

	// Token: 0x060077EF RID: 30703 RVA: 0x00272AB6 File Offset: 0x00270CB6
	public void RegisterCardCountUpdated(DeckTrayCardListContent.CardCountChanged dlg)
	{
		this.m_cardCountChangedListeners.Add(dlg);
	}

	// Token: 0x060077F0 RID: 30704 RVA: 0x00272AC4 File Offset: 0x00270CC4
	public void UnregisterCardTileHeldListener(DeckTrayCardListContent.CardTileHeld dlg)
	{
		this.m_cardTileHeldListeners.Remove(dlg);
	}

	// Token: 0x060077F1 RID: 30705 RVA: 0x00272AD3 File Offset: 0x00270CD3
	public void UnregisterCardTileTapListener(DeckTrayCardListContent.CardTileTap dlg)
	{
		this.m_cardTileTapListeners.Remove(dlg);
	}

	// Token: 0x060077F2 RID: 30706 RVA: 0x00272AE2 File Offset: 0x00270CE2
	public void UnregisterCardTilePressListener(DeckTrayCardListContent.CardTilePress dlg)
	{
		this.m_cardTilePressListeners.Remove(dlg);
	}

	// Token: 0x060077F3 RID: 30707 RVA: 0x00272AF1 File Offset: 0x00270CF1
	public void UnregisterCardTileOverListener(DeckTrayCardListContent.CardTileOver dlg)
	{
		this.m_cardTileOverListeners.Remove(dlg);
	}

	// Token: 0x060077F4 RID: 30708 RVA: 0x00272B00 File Offset: 0x00270D00
	public void UnregisterCardTileOutListener(DeckTrayCardListContent.CardTileOut dlg)
	{
		this.m_cardTileOutListeners.Remove(dlg);
	}

	// Token: 0x060077F5 RID: 30709 RVA: 0x00272B0F File Offset: 0x00270D0F
	public void UnregisterCardTileReleaseListener(DeckTrayCardListContent.CardTileRelease dlg)
	{
		this.m_cardTileReleaseListeners.Remove(dlg);
	}

	// Token: 0x060077F6 RID: 30710 RVA: 0x00272B1E File Offset: 0x00270D1E
	public void UnregisterCardTileRightClickedListener(DeckTrayCardListContent.CardTileRightClicked dlg)
	{
		this.m_cardTileRightClickedListeners.Remove(dlg);
	}

	// Token: 0x060077F7 RID: 30711 RVA: 0x00272B2D File Offset: 0x00270D2D
	public void UnregisterCardCountUpdated(DeckTrayCardListContent.CardCountChanged dlg)
	{
		this.m_cardCountChangedListeners.Remove(dlg);
	}

	// Token: 0x060077F8 RID: 30712 RVA: 0x00272B3C File Offset: 0x00270D3C
	private void FireCardTileDragEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileHeld[] array = this.m_cardTileHeldListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077F9 RID: 30713 RVA: 0x00272B6C File Offset: 0x00270D6C
	private void FireCardTilePressEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTilePress[] array = this.m_cardTilePressListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FA RID: 30714 RVA: 0x00272B9C File Offset: 0x00270D9C
	private void FireCardTileTapEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileTap[] array = this.m_cardTileTapListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FB RID: 30715 RVA: 0x00272BCC File Offset: 0x00270DCC
	private void FireCardTileOverEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileOver[] array = this.m_cardTileOverListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FC RID: 30716 RVA: 0x00272BFC File Offset: 0x00270DFC
	private void FireCardTileOutEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileOut[] array = this.m_cardTileOutListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FD RID: 30717 RVA: 0x00272C2C File Offset: 0x00270E2C
	private void FireCardTileReleaseEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileRelease[] array = this.m_cardTileReleaseListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FE RID: 30718 RVA: 0x00272C5C File Offset: 0x00270E5C
	private void FireCardTileRightClickedEvent(DeckTrayDeckTileVisual cardTile)
	{
		DeckTrayCardListContent.CardTileRightClicked[] array = this.m_cardTileRightClickedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](cardTile);
		}
	}

	// Token: 0x060077FF RID: 30719 RVA: 0x00272C8C File Offset: 0x00270E8C
	private void FireCardCountChangedEvent()
	{
		DeckTrayCardListContent.CardCountChanged[] array = this.m_cardCountChangedListeners.ToArray();
		CollectionDeck editingDeck = this.GetEditingDeck();
		int cardCount = 0;
		if (editingDeck != null)
		{
			cardCount = (editingDeck.ShouldSplitSlotsByOwnershipOrFormatValidity() ? editingDeck.GetTotalValidCardCount() : editingDeck.GetTotalCardCount());
		}
		DeckTrayCardListContent.CardCountChanged[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](cardCount);
		}
	}

	// Token: 0x04005DB1 RID: 23985
	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardTileHeight = 2.45f;

	// Token: 0x04005DB2 RID: 23986
	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardHelpButtonHeight = 3f;

	// Token: 0x04005DB3 RID: 23987
	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_deckCardBarFlareUpInterval = 0.075f;

	// Token: 0x04005DB4 RID: 23988
	[CustomEditField(Sections = "Card Tile Settings")]
	public GameObject m_phoneDeckTileBone;

	// Token: 0x04005DB5 RID: 23989
	[CustomEditField(Sections = "Card Tile Settings")]
	public Vector3 m_cardTileOffset = Vector3.zero;

	// Token: 0x04005DB6 RID: 23990
	[CustomEditField(Sections = "Card Tile Settings")]
	public float m_cardTileSlotLocalHeight;

	// Token: 0x04005DB7 RID: 23991
	[CustomEditField(Sections = "Card Tile Settings")]
	public Vector3 m_cardTileSlotLocalScaleVec3 = new Vector3(0.01f, 0.02f, 0.01f);

	// Token: 0x04005DB8 RID: 23992
	[CustomEditField(Sections = "Card Tile Settings")]
	public bool m_forceUseFullScaleDeckTileActors;

	// Token: 0x04005DB9 RID: 23993
	[CustomEditField(Sections = "Deck Help")]
	public UIBButton m_smartDeckCompleteButton;

	// Token: 0x04005DBA RID: 23994
	[CustomEditField(Sections = "Deck Help")]
	public UIBButton m_deckTemplateHelpButton;

	// Token: 0x04005DBB RID: 23995
	[CustomEditField(Sections = "Other Objects")]
	public GameObject m_deckCompleteHighlight;

	// Token: 0x04005DBC RID: 23996
	[CustomEditField(Sections = "Scroll Settings")]
	public UIBScrollable m_scrollbar;

	// Token: 0x04005DBD RID: 23997
	[CustomEditField(Sections = "Scroll Settings")]
	public BoxCollider m_LockedScrollBounds;

	// Token: 0x04005DBE RID: 23998
	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	// Token: 0x04005DBF RID: 23999
	private const float CARD_MOVEMENT_TIME = 0.3f;

	// Token: 0x04005DC0 RID: 24000
	private Vector3 m_originalLocalPosition;

	// Token: 0x04005DC1 RID: 24001
	private List<DeckTrayDeckTileVisual> m_cardTiles = new List<DeckTrayDeckTileVisual>();

	// Token: 0x04005DC2 RID: 24002
	private List<DeckTrayCardListContent.CardTileHeld> m_cardTileHeldListeners = new List<DeckTrayCardListContent.CardTileHeld>();

	// Token: 0x04005DC3 RID: 24003
	private List<DeckTrayCardListContent.CardTilePress> m_cardTilePressListeners = new List<DeckTrayCardListContent.CardTilePress>();

	// Token: 0x04005DC4 RID: 24004
	private List<DeckTrayCardListContent.CardTileTap> m_cardTileTapListeners = new List<DeckTrayCardListContent.CardTileTap>();

	// Token: 0x04005DC5 RID: 24005
	private List<DeckTrayCardListContent.CardTileOver> m_cardTileOverListeners = new List<DeckTrayCardListContent.CardTileOver>();

	// Token: 0x04005DC6 RID: 24006
	private List<DeckTrayCardListContent.CardTileOut> m_cardTileOutListeners = new List<DeckTrayCardListContent.CardTileOut>();

	// Token: 0x04005DC7 RID: 24007
	private List<DeckTrayCardListContent.CardTileRelease> m_cardTileReleaseListeners = new List<DeckTrayCardListContent.CardTileRelease>();

	// Token: 0x04005DC8 RID: 24008
	private List<DeckTrayCardListContent.CardTileRightClicked> m_cardTileRightClickedListeners = new List<DeckTrayCardListContent.CardTileRightClicked>();

	// Token: 0x04005DC9 RID: 24009
	private List<DeckTrayCardListContent.CardCountChanged> m_cardCountChangedListeners = new List<DeckTrayCardListContent.CardCountChanged>();

	// Token: 0x04005DCA RID: 24010
	private List<DefLoader.DisposableCardDef> m_cardDefs = new List<DefLoader.DisposableCardDef>();

	// Token: 0x04005DCB RID: 24011
	private bool m_animating;

	// Token: 0x04005DCC RID: 24012
	private bool m_loading;

	// Token: 0x04005DCD RID: 24013
	private const float DECK_HELP_BUTTON_EMPTY_DECK_Y_LOCAL_POS = -0.01194457f;

	// Token: 0x04005DCE RID: 24014
	private const float DECK_HELP_BUTTON_Y_TILE_OFFSET = -0.04915909f;

	// Token: 0x04005DCF RID: 24015
	private bool m_inArena;

	// Token: 0x04005DD0 RID: 24016
	private CollectionDeck m_templateFakeDeck = new CollectionDeck();

	// Token: 0x04005DD1 RID: 24017
	private bool m_isShowingFakeDeck;

	// Token: 0x04005DD2 RID: 24018
	private bool m_hasFinishedEntering;

	// Token: 0x04005DD3 RID: 24019
	private bool m_hasFinishedExiting = true;

	// Token: 0x04005DD4 RID: 24020
	private Notification m_deckHelpPopup;

	// Token: 0x020024D5 RID: 9429
	// (Invoke) Token: 0x0601310C RID: 78092
	public delegate void CardTileHeld(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024D6 RID: 9430
	// (Invoke) Token: 0x06013110 RID: 78096
	public delegate void CardTilePress(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024D7 RID: 9431
	// (Invoke) Token: 0x06013114 RID: 78100
	public delegate void CardTileTap(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024D8 RID: 9432
	// (Invoke) Token: 0x06013118 RID: 78104
	public delegate void CardTileOver(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024D9 RID: 9433
	// (Invoke) Token: 0x0601311C RID: 78108
	public delegate void CardTileOut(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024DA RID: 9434
	// (Invoke) Token: 0x06013120 RID: 78112
	public delegate void CardTileRelease(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024DB RID: 9435
	// (Invoke) Token: 0x06013124 RID: 78116
	public delegate void CardTileRightClicked(DeckTrayDeckTileVisual cardTile);

	// Token: 0x020024DC RID: 9436
	// (Invoke) Token: 0x06013128 RID: 78120
	public delegate void CardCountChanged(int cardCount);
}
