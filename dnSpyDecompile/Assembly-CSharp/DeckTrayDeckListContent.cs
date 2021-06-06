using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x02000890 RID: 2192
[CustomEditClass]
public abstract class DeckTrayDeckListContent : DeckTrayContent
{
	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x06007817 RID: 30743 RVA: 0x00272E03 File Offset: 0x00271003
	public CollectionDeckBoxVisual DraggingDeckBox
	{
		get
		{
			return this.m_draggingDeckBox;
		}
	}

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x06007818 RID: 30744 RVA: 0x00272E0B File Offset: 0x0027100B
	public bool IsTouchDragging
	{
		get
		{
			return this.m_scrollbar != null && this.m_scrollbar.IsTouchDragging();
		}
	}

	// Token: 0x06007819 RID: 30745 RVA: 0x00272E28 File Offset: 0x00271028
	protected void Update()
	{
		this.UpdateDragToReorder();
		if (this.m_wasTouchModeEnabled == UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		this.m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
		if (UniversalInputManager.Get().IsTouchMode() && this.m_deckInfoTooltip != null)
		{
			this.HideDeckInfo();
		}
	}

	// Token: 0x0600781A RID: 30746 RVA: 0x00272E7E File Offset: 0x0027107E
	protected override void Awake()
	{
		base.Awake();
	}

	// Token: 0x0600781B RID: 30747 RVA: 0x00272E86 File Offset: 0x00271086
	protected override void OnDestroy()
	{
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		}
		base.OnDestroy();
	}

	// Token: 0x0600781C RID: 30748 RVA: 0x00272EB2 File Offset: 0x002710B2
	public bool IsDoneEntering()
	{
		return this.m_doneEntering;
	}

	// Token: 0x0600781D RID: 30749 RVA: 0x00272EBA File Offset: 0x002710BA
	public IEnumerator ShowTrayDoors(bool show)
	{
		foreach (TraySection traySection in this.m_traySections)
		{
			traySection.EnableDoors(show);
			traySection.ShowDoor(show);
		}
		yield return null;
		yield break;
	}

	// Token: 0x0600781E RID: 30750 RVA: 0x00272ED0 File Offset: 0x002710D0
	public override bool AnimateContentExitEnd()
	{
		return !this.m_animatingExit;
	}

	// Token: 0x0600781F RID: 30751 RVA: 0x00272EDC File Offset: 0x002710DC
	public override bool PreAnimateContentExit()
	{
		if (this.m_scrollbar == null)
		{
			return true;
		}
		if (this.m_centeringDeckList != -1 && this.m_editingTraySection != null)
		{
			BoxCollider component = this.m_editingTraySection.m_deckBox.GetComponent<BoxCollider>();
			if (this.m_scrollbar.ScrollObjectIntoView(this.m_editingTraySection.m_deckBox.gameObject, component.center.y, component.size.y / 2f, delegate(float f)
			{
				this.m_animatingExit = false;
			}, iTween.EaseType.linear, this.m_scrollbar.m_ScrollTweenTime, true))
			{
				this.m_animatingExit = true;
				this.m_centeringDeckList = -1;
			}
		}
		base.StartCoroutine(this.ShowTrayDoors(false));
		return !this.m_animatingExit;
	}

	// Token: 0x06007820 RID: 30752 RVA: 0x00272F9C File Offset: 0x0027119C
	public override bool PreAnimateContentEntrance()
	{
		this.m_doneEntering = false;
		base.StartCoroutine(this.ShowTrayDoors(true));
		return true;
	}

	// Token: 0x06007821 RID: 30753 RVA: 0x00272FB4 File Offset: 0x002711B4
	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		if (newDeck != null && this.m_deckInfoTooltip != null)
		{
			this.m_deckInfoTooltip.SetDeck(newDeck);
			if (this.m_deckOptionsMenu != null)
			{
				this.m_deckOptionsMenu.SetDeck(newDeck);
			}
		}
		if (base.IsModeActive())
		{
			this.InitializeTraysFromDecks();
		}
	}

	// Token: 0x06007822 RID: 30754 RVA: 0x00273006 File Offset: 0x00271206
	public void UpdateEditingDeckBoxVisual(string heroCardId, TAG_PREMIUM? premiumOverride = null)
	{
		if (this.m_editingTraySection == null)
		{
			return;
		}
		this.m_editingTraySection.m_deckBox.SetHeroCardPremiumOverride(premiumOverride);
		this.m_editingTraySection.m_deckBox.SetHeroCardID(heroCardId);
	}

	// Token: 0x06007823 RID: 30755 RVA: 0x0027303C File Offset: 0x0027123C
	private void OnDrawGizmos()
	{
		if (this.m_editingTraySection == null)
		{
			return;
		}
		Bounds bounds = this.m_editingTraySection.m_deckBox.GetDeckNameText().GetBounds();
		Gizmos.DrawWireSphere(bounds.min, 0.1f);
		Gizmos.DrawWireSphere(bounds.max, 0.1f);
	}

	// Token: 0x06007824 RID: 30756 RVA: 0x00273090 File Offset: 0x00271290
	public void RegisterDeckCountUpdated(DeckTrayDeckListContent.DeckCountChanged dlg)
	{
		this.m_deckCountChangedListeners.Add(dlg);
	}

	// Token: 0x06007825 RID: 30757 RVA: 0x0027309E File Offset: 0x0027129E
	public void UnregisterDeckCountUpdated(DeckTrayDeckListContent.DeckCountChanged dlg)
	{
		this.m_deckCountChangedListeners.Remove(dlg);
	}

	// Token: 0x06007826 RID: 30758 RVA: 0x002730AD File Offset: 0x002712AD
	public void RegisterBusyWithDeck(DeckTrayDeckListContent.BusyWithDeck dlg)
	{
		this.m_busyWithDeckListeners.Add(dlg);
	}

	// Token: 0x06007827 RID: 30759 RVA: 0x002730BB File Offset: 0x002712BB
	public void UnregisterBusyWithDeck(DeckTrayDeckListContent.BusyWithDeck dlg)
	{
		this.m_busyWithDeckListeners.Remove(dlg);
	}

	// Token: 0x06007828 RID: 30760 RVA: 0x002730CC File Offset: 0x002712CC
	public virtual void HideTraySectionsNotInBounds(Bounds bounds)
	{
		int num = 0;
		using (List<TraySection>.Enumerator enumerator = this.m_traySections.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HideIfNotInBounds(bounds))
				{
					num++;
				}
			}
		}
		Log.DeckTray.Print("Hid {0} tray sections that were not visible.", new object[]
		{
			num
		});
	}

	// Token: 0x06007829 RID: 30761
	protected abstract void Initialize();

	// Token: 0x0600782A RID: 30762
	protected abstract void HideDeckInfoListener();

	// Token: 0x0600782B RID: 30763 RVA: 0x00273144 File Offset: 0x00271344
	protected virtual void ShowDeckInfo()
	{
		SceneUtils.SetLayer(this.m_editingTraySection.m_deckBox.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(this.m_deckInfoTooltip.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(this.m_deckOptionsMenu.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.25f, iTween.EaseType.easeInOutQuad, null, null);
		this.m_deckInfoTooltip.UpdateManaCurve();
		if (CollectionManagerDisplay.ShouldShowDeckHeaderInfo())
		{
			this.m_deckInfoTooltip.Show();
		}
		if (CollectionManagerDisplay.ShouldShowDeckOptionsMenu())
		{
			this.m_deckOptionsMenu.Show();
		}
	}

	// Token: 0x0600782C RID: 30764 RVA: 0x002731D2 File Offset: 0x002713D2
	protected void HideDeckInfo()
	{
		this.m_deckInfoTooltip.Hide();
	}

	// Token: 0x170006FD RID: 1789
	// (get) Token: 0x0600782D RID: 30765 RVA: 0x002731DF File Offset: 0x002713DF
	public bool IsShowingDeckOptions
	{
		get
		{
			return this.m_deckOptionsMenu != null && this.m_deckOptionsMenu.IsShown;
		}
	}

	// Token: 0x0600782E RID: 30766 RVA: 0x002731FC File Offset: 0x002713FC
	protected void CreateTraySections()
	{
		Vector3 localScale = this.m_traySectionStartPos.localScale;
		Vector3 localEulerAngles = this.m_traySectionStartPos.localEulerAngles;
		for (int i = 0; i < 29; i++)
		{
			TraySection traySection = (TraySection)GameUtils.Instantiate(this.m_traySectionPrefab, base.gameObject, false);
			traySection.m_deckBox.SetPositionIndex(i);
			traySection.transform.localScale = localScale;
			traySection.transform.localEulerAngles = localEulerAngles;
			traySection.EnableDoors(i < 27);
			CollectionDeckBoxVisual deckBox = traySection.m_deckBox;
			deckBox.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
			{
				this.OnDeckBoxVisualOver(deckBox);
			});
			deckBox.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
			{
				this.OnDeckBoxVisualOut(deckBox);
			});
			deckBox.AddEventListener(UIEventType.TAP, delegate(UIEvent e)
			{
				this.OnDeckBoxVisualRelease(traySection);
			});
			deckBox.StoreOriginalButtonPositionAndRotation();
			deckBox.HideBanner();
			this.m_traySections.Add(traySection);
		}
		this.RefreshTraySectionPositions(false);
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.HideTraySectionsNotInBounds(this.m_deckTray.m_scrollbar.m_ScrollBounds.bounds);
			Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		}
	}

	// Token: 0x0600782F RID: 30767 RVA: 0x0027336C File Offset: 0x0027156C
	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		foreach (TraySection traySection in this.m_traySections)
		{
			traySection.gameObject.SetActive(true);
		}
	}

	// Token: 0x06007830 RID: 30768 RVA: 0x002733DC File Offset: 0x002715DC
	protected TraySection GetExistingTrayFromDeck(CollectionDeck deck)
	{
		return this.GetExistingTrayFromDeck(deck.ID);
	}

	// Token: 0x06007831 RID: 30769 RVA: 0x002733EC File Offset: 0x002715EC
	private TraySection GetExistingTrayFromDeck(long deckID)
	{
		foreach (TraySection traySection in this.m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == deckID)
			{
				return traySection;
			}
		}
		return null;
	}

	// Token: 0x06007832 RID: 30770 RVA: 0x00273450 File Offset: 0x00271650
	public TraySection GetEditingTraySection()
	{
		return this.m_editingTraySection;
	}

	// Token: 0x06007833 RID: 30771 RVA: 0x00273458 File Offset: 0x00271658
	protected void InitializeTraysFromDecks()
	{
		this.UpdateDeckTrayVisuals();
	}

	// Token: 0x06007834 RID: 30772 RVA: 0x00273464 File Offset: 0x00271664
	protected void UpdateAllTrays(bool immediate = false, bool initializeTrays = true)
	{
		if (initializeTrays)
		{
			this.InitializeTraysFromDecks();
		}
		List<TraySection> list = new List<TraySection>();
		foreach (TraySection traySection in this.m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == -1L && !traySection.m_deckBox.IsLocked())
			{
				traySection.HideDeckBox(immediate, null);
			}
			else if (this.m_editingTraySection != traySection && !traySection.IsOpen())
			{
				list.Add(traySection);
			}
		}
		base.StartCoroutine(this.UpdateAllTraysAnimation(list, immediate));
	}

	// Token: 0x06007835 RID: 30773 RVA: 0x00273514 File Offset: 0x00271714
	protected virtual IEnumerator UpdateAllTraysAnimation(List<TraySection> showTraySections, bool immediate)
	{
		foreach (TraySection traySection in showTraySections)
		{
			traySection.ShowDeckBox(immediate, null);
			if (!immediate)
			{
				yield return new WaitForSeconds(0.015f);
			}
		}
		List<TraySection>.Enumerator enumerator = default(List<TraySection>.Enumerator);
		this.m_doneEntering = true;
		yield break;
		yield break;
	}

	// Token: 0x06007836 RID: 30774 RVA: 0x00273534 File Offset: 0x00271734
	public TraySection GetLastUnusedTraySection()
	{
		int num = 0;
		foreach (TraySection traySection in this.m_traySections)
		{
			if (num >= 27)
			{
				break;
			}
			if (traySection.m_deckBox.GetDeckID() == -1L)
			{
				return traySection;
			}
			num++;
		}
		return null;
	}

	// Token: 0x06007837 RID: 30775 RVA: 0x002735A4 File Offset: 0x002717A4
	public TraySection GetLastUsedTraySection()
	{
		int num = 0;
		TraySection result = null;
		foreach (TraySection traySection in this.m_traySections)
		{
			if (num >= 27)
			{
				break;
			}
			if (traySection.m_deckBox.GetDeckID() == -1L)
			{
				return result;
			}
			result = traySection;
			num++;
		}
		return result;
	}

	// Token: 0x06007838 RID: 30776 RVA: 0x0027361C File Offset: 0x0027181C
	public TraySection GetTraySection(int index)
	{
		if (index >= 0 && index < this.m_traySections.Count)
		{
			return this.m_traySections[index];
		}
		return null;
	}

	// Token: 0x06007839 RID: 30777 RVA: 0x0027363E File Offset: 0x0027183E
	public bool CanShowNewDeckButton()
	{
		return CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count < 27 && !SceneMgr.Get().IsInDuelsMode();
	}

	// Token: 0x0600783A RID: 30778 RVA: 0x00273663 File Offset: 0x00271863
	public void SetEditingTraySection(int index)
	{
		this.m_editingTraySection = this.m_traySections[index];
		this.m_centeringDeckList = this.m_editingTraySection.m_deckBox.GetPositionIndex();
	}

	// Token: 0x0600783B RID: 30779 RVA: 0x0027368D File Offset: 0x0027188D
	protected bool IsEditingCards()
	{
		return CollectionManager.Get().GetEditedDeck() != null;
	}

	// Token: 0x0600783C RID: 30780 RVA: 0x0027369C File Offset: 0x0027189C
	protected virtual void OnDeckBoxVisualOver(CollectionDeckBoxVisual deckBox)
	{
		if (deckBox.IsLocked())
		{
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		if (this.IsEditingCards() && this.m_deckInfoTooltip != null)
		{
			this.ShowDeckInfo();
			return;
		}
	}

	// Token: 0x0600783D RID: 30781 RVA: 0x002736D4 File Offset: 0x002718D4
	private void OnDeckBoxVisualOut(CollectionDeckBoxVisual deckBox)
	{
		if (deckBox.IsLocked())
		{
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (this.m_deckInfoTooltip != null && this.m_deckInfoTooltip.IsShown())
			{
				deckBox.SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			}
			return;
		}
		if (!UniversalInputManager.Get().InputIsOver(deckBox.m_deleteButton.gameObject))
		{
			deckBox.ShowDeleteButton(false);
		}
	}

	// Token: 0x0600783E RID: 30782
	protected abstract void OnDeckBoxVisualRelease(TraySection traySection);

	// Token: 0x0600783F RID: 30783 RVA: 0x00273738 File Offset: 0x00271938
	protected void FireDeckCountChangedEvent()
	{
		DeckTrayDeckListContent.DeckCountChanged[] array = this.m_deckCountChangedListeners.ToArray();
		int count = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
		DeckTrayDeckListContent.DeckCountChanged[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](count);
		}
	}

	// Token: 0x06007840 RID: 30784 RVA: 0x0027377C File Offset: 0x0027197C
	protected void FireBusyWithDeckEvent(bool busy)
	{
		DeckTrayDeckListContent.BusyWithDeck[] array = this.m_busyWithDeckListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](busy);
		}
	}

	// Token: 0x06007841 RID: 30785 RVA: 0x002737AC File Offset: 0x002719AC
	private int GetTotalDeckBoxesInUse()
	{
		int num = 0;
		using (List<TraySection>.Enumerator enumerator = this.m_traySections.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_deckBox.GetDeckID() > -1L)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06007842 RID: 30786 RVA: 0x0027380C File Offset: 0x00271A0C
	protected int UpdateDeckTrayVisuals()
	{
		List<CollectionDeck> list;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			list = CollectionManager.Get().GetDecks(TavernBrawlManager.Get().DeckTypeForCurrentBrawlType);
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			int brawlLibraryItemId = (tavernBrawlMission == null) ? 0 : tavernBrawlMission.SelectedBrawlLibraryItemId;
			list.RemoveAll((CollectionDeck deck) => deck.BrawlLibraryItemId != brawlLibraryItemId);
		}
		else if (SceneMgr.Get().IsInDuelsMode())
		{
			list = new List<CollectionDeck>();
			CollectionDeck collectionDeck = CollectionManager.Get().GetEditedDeck();
			if (collectionDeck == null)
			{
				collectionDeck = CollectionManager.Get().GetDuelsDeck();
			}
			list.Add(collectionDeck);
		}
		else
		{
			list = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
		}
		int count = list.Count;
		int num = 0;
		while (num < count && num < this.m_traySections.Count)
		{
			if (num < list.Count)
			{
				CollectionDeck deck2 = list[num];
				this.m_traySections[num].m_deckBox.AssignFromCollectionDeck(deck2);
			}
			num++;
		}
		return list.Count;
	}

	// Token: 0x06007843 RID: 30787 RVA: 0x00273914 File Offset: 0x00271B14
	public void OnDeckContentsUpdated(long deckID)
	{
		foreach (TraySection traySection in this.m_traySections)
		{
			if (traySection.m_deckBox != null)
			{
				CollectionDeck collectionDeck = traySection.m_deckBox.GetCollectionDeck();
				if (collectionDeck != null)
				{
					traySection.m_deckBox.AssignFromCollectionDeck(collectionDeck);
				}
			}
		}
	}

	// Token: 0x06007844 RID: 30788 RVA: 0x0027398C File Offset: 0x00271B8C
	protected void SwapEditTrayIfNeeded(long editDeckID)
	{
		if (editDeckID < 0L)
		{
			return;
		}
		TraySection traySection = null;
		foreach (TraySection traySection2 in this.m_traySections)
		{
			if (traySection2.m_deckBox.GetDeckID() == editDeckID)
			{
				traySection = traySection2;
				break;
			}
		}
		if (traySection == this.m_editingTraySection)
		{
			return;
		}
		this.m_deckTray.TryEnableScrollbar();
		float scrollImmediate = (float)traySection.m_deckBox.GetPositionIndex() / (float)(this.GetTotalDeckBoxesInUse() - 1);
		this.m_scrollbar.SetScrollImmediate(scrollImmediate);
		this.m_deckTray.SaveScrollbarPosition(DeckTray.DeckContentTypes.Decks);
		this.m_editingTraySection.m_deckBox.transform.localScale = CollectionDeckBoxVisual.SCALED_DOWN_LOCAL_SCALE;
		Vector3 zero = Vector3.zero;
		zero.y = 1.273138f;
		this.m_editingTraySection.m_deckBox.transform.localPosition = zero;
		this.m_editingTraySection.m_deckBox.Hide();
		this.m_editingTraySection.m_deckBox.EnableButtonAnimation();
		traySection.m_deckBox.transform.localScale = CollectionDeckBoxVisual.SCALED_UP_LOCAL_SCALE;
		traySection.m_deckBox.transform.parent = null;
		traySection.m_deckBox.transform.position = this.m_deckEditTopPos.position;
		traySection.ShowDeckBoxNoAnim();
		traySection.m_deckBox.SetEnabled(true, false);
		this.m_editingTraySection = traySection;
	}

	// Token: 0x170006FE RID: 1790
	// (get) Token: 0x06007845 RID: 30789 RVA: 0x00273AFC File Offset: 0x00271CFC
	public bool CanDragToReorderDecks
	{
		get
		{
			NetCache netCache = NetCache.Get();
			NetCache.NetCacheFeatures netCacheFeatures = (netCache != null) ? netCache.GetNetObject<NetCache.NetCacheFeatures>() : null;
			return (netCacheFeatures == null || netCacheFeatures.Collection.DeckReordering) && !CollectionManagerDisplay.IsSpecialOneDeckMode() && !this.m_animatingExit;
		}
	}

	// Token: 0x06007846 RID: 30790 RVA: 0x00273B40 File Offset: 0x00271D40
	public void StartDragToReorder(CollectionDeckBoxVisual draggingDeckBox)
	{
		if (this.m_draggingDeckBox == draggingDeckBox)
		{
			return;
		}
		if (this.m_draggingDeckBox != null)
		{
			this.StopDragToReorder();
		}
		if (this.CanDragToReorderDecks)
		{
			this.m_draggingDeckBox = draggingDeckBox;
			this.m_scrollbar.Pause(true);
			this.m_scrollbar.PauseUpdateScrollHeight(true);
		}
	}

	// Token: 0x06007847 RID: 30791 RVA: 0x00273B98 File Offset: 0x00271D98
	public void StopDragToReorder()
	{
		if (this.m_draggingDeckBox != null)
		{
			foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
			{
				collectionDeck.SendChanges();
			}
			if (!UniversalInputManager.Get().IsTouchMode())
			{
				this.m_draggingDeckBox.ShowDeleteButton(true);
			}
			this.m_draggingDeckBox.OnStopDragToReorder();
		}
		this.m_draggingDeckBox = null;
		this.m_scrollbar.Pause(false);
		this.m_scrollbar.PauseUpdateScrollHeight(false);
	}

	// Token: 0x06007848 RID: 30792 RVA: 0x00273C40 File Offset: 0x00271E40
	private void UpdateDragToReorder()
	{
		if (this.m_draggingDeckBox == null)
		{
			return;
		}
		if (!UniversalInputManager.Get().GetMouseButton(0) || !this.CanDragToReorderDecks)
		{
			this.StopDragToReorder();
			return;
		}
		int num = this.m_traySections.FindIndex((TraySection section) => section.m_deckBox == this.m_draggingDeckBox);
		if (num < 0)
		{
			return;
		}
		TraySection traySection = this.m_traySections[num];
		if (traySection == null)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		Vector3 inNormal = -Camera.main.transform.forward;
		Plane plane = new Plane(inNormal, this.m_traySectionStartPos.position);
		float distance;
		if (!plane.Raycast(ray, out distance))
		{
			return;
		}
		ref Vector3 point = ray.GetPoint(distance);
		Vector3 size = TransformUtil.ComputeSetPointBounds(this.m_traySections[0], false).size;
		float z = this.m_traySectionStartPos.position.z;
		int num2 = Mathf.FloorToInt(-(point.z - z) / size.z);
		int count = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
		if (count < 1)
		{
			return;
		}
		float num3 = this.m_scrollbar.m_ScrollBounds.bounds.min.z - z;
		int num4 = Mathf.FloorToInt(-(this.m_scrollbar.m_ScrollBounds.bounds.max.z - z) / size.z) - 1;
		int num5 = Mathf.FloorToInt(-num3 / size.z) + 1;
		num4 = Mathf.Clamp(num4, 0, count - 1);
		num5 = Mathf.Clamp(num5, 0, count - 1);
		num2 = Mathf.Clamp(num2, num4, num5);
		if (num2 >= this.m_traySections.Count)
		{
			return;
		}
		if (num2 == num)
		{
			return;
		}
		float tweenTime = 1f;
		TraySection traySection2 = this.m_traySections[num2];
		Bounds bounds = TransformUtil.ComputeSetPointBounds(traySection2.gameObject, false);
		if (!this.m_scrollbar.ScrollObjectIntoView(traySection2.gameObject, bounds.center.z - traySection2.gameObject.transform.position.z, bounds.extents.z * 1.25f, null, iTween.EaseType.linear, tweenTime, true))
		{
			this.m_scrollbar.StopScroll();
		}
		this.m_traySections.RemoveAt(num);
		this.m_traySections.Insert(num2, traySection);
		for (int i = 0; i < count; i++)
		{
			TraySection traySection3 = this.m_traySections[i];
			traySection3.m_deckBox.SetPositionIndex(i);
			CollectionDeck collectionDeck = traySection3.m_deckBox.GetCollectionDeck();
			if (collectionDeck != null)
			{
				collectionDeck.SortOrder = (long)(i + -100);
			}
		}
		this.RefreshTraySectionPositions(true);
	}

	// Token: 0x06007849 RID: 30793 RVA: 0x00273EEC File Offset: 0x002720EC
	private void RefreshTraySectionPositions(bool animateToNewPositions)
	{
		Vector3 vector = this.m_traySectionStartPos.localPosition;
		Vector3 a = Vector3.zero;
		Transform parent = this.m_traySectionStartPos.parent;
		for (int i = 0; i < 29; i++)
		{
			TraySection traySection = this.m_traySections[i];
			Bounds bounds = TransformUtil.ComputeSetPointBounds(traySection.gameObject, false);
			Vector3 position = traySection.transform.position;
			if (i > 0)
			{
				Vector3 b = position - TransformUtil.ComputeWorldPoint(bounds, TransformUtil.GetUnitAnchor(Anchor.FRONT));
				Vector3 vector2 = a + b;
				Vector3 b2 = (parent != null) ? parent.InverseTransformVector(vector2) : vector2;
				vector += b2;
			}
			if (animateToNewPositions)
			{
				Hashtable args = iTween.Hash(new object[]
				{
					"position",
					vector,
					"isLocal",
					true,
					"time",
					0.25f,
					"easeType",
					iTween.EaseType.easeOutCubic
				});
				iTween.MoveTo(traySection.gameObject, args);
			}
			else
			{
				traySection.gameObject.transform.localPosition = vector;
			}
			Material material = null;
			foreach (Material material2 in traySection.m_door.GetComponent<Renderer>().GetMaterials())
			{
				if (material2.name.Equals("DeckTray", StringComparison.OrdinalIgnoreCase) || material2.name.Equals("DeckTray (Instance)", StringComparison.OrdinalIgnoreCase))
				{
					material = material2;
					break;
				}
			}
			UnityEngine.Vector2 mainTextureOffset = new UnityEngine.Vector2(0f, -0.0825f * (float)i);
			traySection.GetComponent<Renderer>().GetMaterial().mainTextureOffset = mainTextureOffset;
			if (material != null)
			{
				material.mainTextureOffset = mainTextureOffset;
			}
			a = TransformUtil.ComputeWorldPoint(bounds, TransformUtil.GetUnitAnchor(Anchor.BACK)) - position;
		}
	}

	// Token: 0x0600784A RID: 30794 RVA: 0x002740E4 File Offset: 0x002722E4
	public bool UpdateDeckBoxWithNewId(long oldId, long newId)
	{
		foreach (TraySection traySection in this.m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == oldId)
			{
				traySection.m_deckBox.SetDeckID(newId);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600784B RID: 30795 RVA: 0x00274154 File Offset: 0x00272354
	public void RefreshMissingCardIndicators()
	{
		foreach (TraySection traySection in this.m_traySections)
		{
			traySection.m_deckBox.UpdateMissingCardsIndicator();
		}
	}

	// Token: 0x04005DD7 RID: 24023
	[CustomEditField(Sections = "Deck Tray Settings")]
	public Transform m_deckEditTopPos;

	// Token: 0x04005DD8 RID: 24024
	[CustomEditField(Sections = "Deck Tray Settings")]
	public Transform m_traySectionStartPos;

	// Token: 0x04005DD9 RID: 24025
	[CustomEditField(Sections = "Deck Tray Settings")]
	public GameObject m_deckInfoTooltipBone;

	// Token: 0x04005DDA RID: 24026
	[CustomEditField(Sections = "Deck Tray Settings")]
	public GameObject m_deckOptionsBone;

	// Token: 0x04005DDB RID: 24027
	[CustomEditField(Sections = "Prefabs")]
	public TraySection m_traySectionPrefab;

	// Token: 0x04005DDC RID: 24028
	[CustomEditField(Sections = "Prefabs")]
	public DeckTray m_deckTray;

	// Token: 0x04005DDD RID: 24029
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_deckInfoActorPrefab;

	// Token: 0x04005DDE RID: 24030
	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_deckOptionsPrefab;

	// Token: 0x04005DDF RID: 24031
	[CustomEditField(Sections = "Scroll Settings")]
	public UIBScrollable m_scrollbar;

	// Token: 0x04005DE0 RID: 24032
	protected const float TIME_BETWEEN_TRAY_DOOR_ANIMS = 0.015f;

	// Token: 0x04005DE1 RID: 24033
	protected const int MAX_NUM_DECKBOXES_AVAILABLE = 27;

	// Token: 0x04005DE2 RID: 24034
	protected const int NUM_DECKBOXES_TO_DISPLAY = 29;

	// Token: 0x04005DE3 RID: 24035
	protected CollectionDeckInfo m_deckInfoTooltip;

	// Token: 0x04005DE4 RID: 24036
	protected List<TraySection> m_traySections = new List<TraySection>();

	// Token: 0x04005DE5 RID: 24037
	protected TraySection m_editingTraySection;

	// Token: 0x04005DE6 RID: 24038
	protected int m_centeringDeckList = -1;

	// Token: 0x04005DE7 RID: 24039
	protected DeckOptionsMenu m_deckOptionsMenu;

	// Token: 0x04005DE8 RID: 24040
	protected bool m_initialized;

	// Token: 0x04005DE9 RID: 24041
	protected bool m_animatingExit;

	// Token: 0x04005DEA RID: 24042
	protected bool m_doneEntering;

	// Token: 0x04005DEB RID: 24043
	private bool m_wasTouchModeEnabled;

	// Token: 0x04005DEC RID: 24044
	private List<DeckTrayDeckListContent.DeckCountChanged> m_deckCountChangedListeners = new List<DeckTrayDeckListContent.DeckCountChanged>();

	// Token: 0x04005DED RID: 24045
	private List<DeckTrayDeckListContent.BusyWithDeck> m_busyWithDeckListeners = new List<DeckTrayDeckListContent.BusyWithDeck>();

	// Token: 0x04005DEE RID: 24046
	private CollectionDeckBoxVisual m_draggingDeckBox;

	// Token: 0x04005DEF RID: 24047
	private const float TRAY_MATERIAL_Y_OFFSET = -0.0825f;

	// Token: 0x020024E5 RID: 9445
	// (Invoke) Token: 0x06013152 RID: 78162
	public delegate void BusyWithDeck(bool busy);

	// Token: 0x020024E6 RID: 9446
	// (Invoke) Token: 0x06013156 RID: 78166
	public delegate void DeckCountChanged(int deckCount);
}
