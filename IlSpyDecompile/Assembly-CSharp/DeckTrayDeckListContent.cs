using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public abstract class DeckTrayDeckListContent : DeckTrayContent
{
	public delegate void BusyWithDeck(bool busy);

	public delegate void DeckCountChanged(int deckCount);

	[CustomEditField(Sections = "Deck Tray Settings")]
	public Transform m_deckEditTopPos;

	[CustomEditField(Sections = "Deck Tray Settings")]
	public Transform m_traySectionStartPos;

	[CustomEditField(Sections = "Deck Tray Settings")]
	public GameObject m_deckInfoTooltipBone;

	[CustomEditField(Sections = "Deck Tray Settings")]
	public GameObject m_deckOptionsBone;

	[CustomEditField(Sections = "Prefabs")]
	public TraySection m_traySectionPrefab;

	[CustomEditField(Sections = "Prefabs")]
	public DeckTray m_deckTray;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_deckInfoActorPrefab;

	[CustomEditField(Sections = "Prefabs", T = EditType.GAME_OBJECT)]
	public string m_deckOptionsPrefab;

	[CustomEditField(Sections = "Scroll Settings")]
	public UIBScrollable m_scrollbar;

	protected const float TIME_BETWEEN_TRAY_DOOR_ANIMS = 0.015f;

	protected const int MAX_NUM_DECKBOXES_AVAILABLE = 27;

	protected const int NUM_DECKBOXES_TO_DISPLAY = 29;

	protected CollectionDeckInfo m_deckInfoTooltip;

	protected List<TraySection> m_traySections = new List<TraySection>();

	protected TraySection m_editingTraySection;

	protected int m_centeringDeckList = -1;

	protected DeckOptionsMenu m_deckOptionsMenu;

	protected bool m_initialized;

	protected bool m_animatingExit;

	protected bool m_doneEntering;

	private bool m_wasTouchModeEnabled;

	private List<DeckCountChanged> m_deckCountChangedListeners = new List<DeckCountChanged>();

	private List<BusyWithDeck> m_busyWithDeckListeners = new List<BusyWithDeck>();

	private CollectionDeckBoxVisual m_draggingDeckBox;

	private const float TRAY_MATERIAL_Y_OFFSET = -0.0825f;

	public CollectionDeckBoxVisual DraggingDeckBox => m_draggingDeckBox;

	public bool IsTouchDragging
	{
		get
		{
			if (m_scrollbar != null)
			{
				return m_scrollbar.IsTouchDragging();
			}
			return false;
		}
	}

	public bool IsShowingDeckOptions
	{
		get
		{
			if (m_deckOptionsMenu != null)
			{
				return m_deckOptionsMenu.IsShown;
			}
			return false;
		}
	}

	public bool CanDragToReorderDecks
	{
		get
		{
			NetCache.NetCacheFeatures netCacheFeatures = NetCache.Get()?.GetNetObject<NetCache.NetCacheFeatures>();
			if (netCacheFeatures != null && !netCacheFeatures.Collection.DeckReordering)
			{
				return false;
			}
			if (!CollectionManagerDisplay.IsSpecialOneDeckMode())
			{
				return !m_animatingExit;
			}
			return false;
		}
	}

	protected void Update()
	{
		UpdateDragToReorder();
		if (m_wasTouchModeEnabled != UniversalInputManager.Get().IsTouchMode())
		{
			m_wasTouchModeEnabled = UniversalInputManager.Get().IsTouchMode();
			if (UniversalInputManager.Get().IsTouchMode() && m_deckInfoTooltip != null)
			{
				HideDeckInfo();
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnDestroy()
	{
		if (Box.Get() != null)
		{
			Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		}
		base.OnDestroy();
	}

	public bool IsDoneEntering()
	{
		return m_doneEntering;
	}

	public IEnumerator ShowTrayDoors(bool show)
	{
		foreach (TraySection traySection in m_traySections)
		{
			traySection.EnableDoors(show);
			traySection.ShowDoor(show);
		}
		yield return null;
	}

	public override bool AnimateContentExitEnd()
	{
		return !m_animatingExit;
	}

	public override bool PreAnimateContentExit()
	{
		if (m_scrollbar == null)
		{
			return true;
		}
		if (m_centeringDeckList != -1 && m_editingTraySection != null)
		{
			BoxCollider component = m_editingTraySection.m_deckBox.GetComponent<BoxCollider>();
			if (m_scrollbar.ScrollObjectIntoView(m_editingTraySection.m_deckBox.gameObject, component.center.y, component.size.y / 2f, delegate
			{
				m_animatingExit = false;
			}, iTween.EaseType.linear, m_scrollbar.m_ScrollTweenTime, blockInputWhileScrolling: true))
			{
				m_animatingExit = true;
				m_centeringDeckList = -1;
			}
		}
		StartCoroutine(ShowTrayDoors(show: false));
		return !m_animatingExit;
	}

	public override bool PreAnimateContentEntrance()
	{
		m_doneEntering = false;
		StartCoroutine(ShowTrayDoors(show: true));
		return true;
	}

	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		if (newDeck != null && m_deckInfoTooltip != null)
		{
			m_deckInfoTooltip.SetDeck(newDeck);
			if (m_deckOptionsMenu != null)
			{
				m_deckOptionsMenu.SetDeck(newDeck);
			}
		}
		if (IsModeActive())
		{
			InitializeTraysFromDecks();
		}
	}

	public void UpdateEditingDeckBoxVisual(string heroCardId, TAG_PREMIUM? premiumOverride = null)
	{
		if (!(m_editingTraySection == null))
		{
			m_editingTraySection.m_deckBox.SetHeroCardPremiumOverride(premiumOverride);
			m_editingTraySection.m_deckBox.SetHeroCardID(heroCardId);
		}
	}

	private void OnDrawGizmos()
	{
		if (!(m_editingTraySection == null))
		{
			Bounds bounds = m_editingTraySection.m_deckBox.GetDeckNameText().GetBounds();
			Gizmos.DrawWireSphere(bounds.min, 0.1f);
			Gizmos.DrawWireSphere(bounds.max, 0.1f);
		}
	}

	public void RegisterDeckCountUpdated(DeckCountChanged dlg)
	{
		m_deckCountChangedListeners.Add(dlg);
	}

	public void UnregisterDeckCountUpdated(DeckCountChanged dlg)
	{
		m_deckCountChangedListeners.Remove(dlg);
	}

	public void RegisterBusyWithDeck(BusyWithDeck dlg)
	{
		m_busyWithDeckListeners.Add(dlg);
	}

	public void UnregisterBusyWithDeck(BusyWithDeck dlg)
	{
		m_busyWithDeckListeners.Remove(dlg);
	}

	public virtual void HideTraySectionsNotInBounds(Bounds bounds)
	{
		int num = 0;
		foreach (TraySection traySection in m_traySections)
		{
			if (traySection.HideIfNotInBounds(bounds))
			{
				num++;
			}
		}
		Log.DeckTray.Print("Hid {0} tray sections that were not visible.", num);
	}

	protected abstract void Initialize();

	protected abstract void HideDeckInfoListener();

	protected virtual void ShowDeckInfo()
	{
		SceneUtils.SetLayer(m_editingTraySection.m_deckBox.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(m_deckInfoTooltip.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(m_deckOptionsMenu.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.25f, iTween.EaseType.easeInOutQuad);
		m_deckInfoTooltip.UpdateManaCurve();
		if (CollectionManagerDisplay.ShouldShowDeckHeaderInfo())
		{
			m_deckInfoTooltip.Show();
		}
		if (CollectionManagerDisplay.ShouldShowDeckOptionsMenu())
		{
			m_deckOptionsMenu.Show();
		}
	}

	protected void HideDeckInfo()
	{
		m_deckInfoTooltip.Hide();
	}

	protected void CreateTraySections()
	{
		Vector3 localScale = m_traySectionStartPos.localScale;
		Vector3 localEulerAngles = m_traySectionStartPos.localEulerAngles;
		for (int i = 0; i < 29; i++)
		{
			TraySection traySection = (TraySection)GameUtils.Instantiate(m_traySectionPrefab, base.gameObject);
			traySection.m_deckBox.SetPositionIndex(i);
			traySection.transform.localScale = localScale;
			traySection.transform.localEulerAngles = localEulerAngles;
			traySection.EnableDoors(i < 27);
			CollectionDeckBoxVisual deckBox = traySection.m_deckBox;
			deckBox.AddEventListener(UIEventType.ROLLOVER, delegate
			{
				OnDeckBoxVisualOver(deckBox);
			});
			deckBox.AddEventListener(UIEventType.ROLLOUT, delegate
			{
				OnDeckBoxVisualOut(deckBox);
			});
			deckBox.AddEventListener(UIEventType.TAP, delegate
			{
				OnDeckBoxVisualRelease(traySection);
			});
			deckBox.StoreOriginalButtonPositionAndRotation();
			deckBox.HideBanner();
			m_traySections.Add(traySection);
		}
		RefreshTraySectionPositions(animateToNewPositions: false);
		if (!UniversalInputManager.UsePhoneUI)
		{
			HideTraySectionsNotInBounds(m_deckTray.m_scrollbar.m_ScrollBounds.bounds);
			Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		}
	}

	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		foreach (TraySection traySection in m_traySections)
		{
			traySection.gameObject.SetActive(value: true);
		}
	}

	protected TraySection GetExistingTrayFromDeck(CollectionDeck deck)
	{
		return GetExistingTrayFromDeck(deck.ID);
	}

	private TraySection GetExistingTrayFromDeck(long deckID)
	{
		foreach (TraySection traySection in m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == deckID)
			{
				return traySection;
			}
		}
		return null;
	}

	public TraySection GetEditingTraySection()
	{
		return m_editingTraySection;
	}

	protected void InitializeTraysFromDecks()
	{
		UpdateDeckTrayVisuals();
	}

	protected void UpdateAllTrays(bool immediate = false, bool initializeTrays = true)
	{
		if (initializeTrays)
		{
			InitializeTraysFromDecks();
		}
		List<TraySection> list = new List<TraySection>();
		foreach (TraySection traySection in m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == -1 && !traySection.m_deckBox.IsLocked())
			{
				traySection.HideDeckBox(immediate);
			}
			else if (m_editingTraySection != traySection && !traySection.IsOpen())
			{
				list.Add(traySection);
			}
		}
		StartCoroutine(UpdateAllTraysAnimation(list, immediate));
	}

	protected virtual IEnumerator UpdateAllTraysAnimation(List<TraySection> showTraySections, bool immediate)
	{
		foreach (TraySection showTraySection in showTraySections)
		{
			showTraySection.ShowDeckBox(immediate);
			if (!immediate)
			{
				yield return new WaitForSeconds(0.015f);
			}
		}
		m_doneEntering = true;
	}

	public TraySection GetLastUnusedTraySection()
	{
		int num = 0;
		foreach (TraySection traySection in m_traySections)
		{
			if (num >= 27)
			{
				break;
			}
			if (traySection.m_deckBox.GetDeckID() == -1)
			{
				return traySection;
			}
			num++;
		}
		return null;
	}

	public TraySection GetLastUsedTraySection()
	{
		int num = 0;
		TraySection result = null;
		foreach (TraySection traySection in m_traySections)
		{
			if (num < 27)
			{
				if (traySection.m_deckBox.GetDeckID() == -1)
				{
					return result;
				}
				result = traySection;
				num++;
				continue;
			}
			return result;
		}
		return result;
	}

	public TraySection GetTraySection(int index)
	{
		if (index >= 0 && index < m_traySections.Count)
		{
			return m_traySections[index];
		}
		return null;
	}

	public bool CanShowNewDeckButton()
	{
		if (CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count < 27)
		{
			return !SceneMgr.Get().IsInDuelsMode();
		}
		return false;
	}

	public void SetEditingTraySection(int index)
	{
		m_editingTraySection = m_traySections[index];
		m_centeringDeckList = m_editingTraySection.m_deckBox.GetPositionIndex();
	}

	protected bool IsEditingCards()
	{
		return CollectionManager.Get().GetEditedDeck() != null;
	}

	protected virtual void OnDeckBoxVisualOver(CollectionDeckBoxVisual deckBox)
	{
		if (!deckBox.IsLocked() && !UniversalInputManager.Get().IsTouchMode() && IsEditingCards() && m_deckInfoTooltip != null)
		{
			ShowDeckInfo();
		}
	}

	private void OnDeckBoxVisualOut(CollectionDeckBoxVisual deckBox)
	{
		if (deckBox.IsLocked())
		{
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (m_deckInfoTooltip != null && m_deckInfoTooltip.IsShown())
			{
				deckBox.SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			}
		}
		else if (!UniversalInputManager.Get().InputIsOver(deckBox.m_deleteButton.gameObject))
		{
			deckBox.ShowDeleteButton(show: false);
		}
	}

	protected abstract void OnDeckBoxVisualRelease(TraySection traySection);

	protected void FireDeckCountChangedEvent()
	{
		DeckCountChanged[] array = m_deckCountChangedListeners.ToArray();
		int count = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
		DeckCountChanged[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](count);
		}
	}

	protected void FireBusyWithDeckEvent(bool busy)
	{
		BusyWithDeck[] array = m_busyWithDeckListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](busy);
		}
	}

	private int GetTotalDeckBoxesInUse()
	{
		int num = 0;
		foreach (TraySection traySection in m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() > -1)
			{
				num++;
			}
		}
		return num;
	}

	protected int UpdateDeckTrayVisuals()
	{
		List<CollectionDeck> list = null;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			list = CollectionManager.Get().GetDecks(TavernBrawlManager.Get().DeckTypeForCurrentBrawlType);
			int brawlLibraryItemId = TavernBrawlManager.Get().CurrentMission()?.SelectedBrawlLibraryItemId ?? 0;
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
		for (int i = 0; i < count && i < m_traySections.Count; i++)
		{
			if (i < list.Count)
			{
				CollectionDeck deck2 = list[i];
				m_traySections[i].m_deckBox.AssignFromCollectionDeck(deck2);
			}
		}
		return list.Count;
	}

	public void OnDeckContentsUpdated(long deckID)
	{
		foreach (TraySection traySection in m_traySections)
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

	protected void SwapEditTrayIfNeeded(long editDeckID)
	{
		if (editDeckID < 0)
		{
			return;
		}
		TraySection traySection = null;
		foreach (TraySection traySection2 in m_traySections)
		{
			if (traySection2.m_deckBox.GetDeckID() == editDeckID)
			{
				traySection = traySection2;
				break;
			}
		}
		if (!(traySection == m_editingTraySection))
		{
			m_deckTray.TryEnableScrollbar();
			float scrollImmediate = (float)traySection.m_deckBox.GetPositionIndex() / (float)(GetTotalDeckBoxesInUse() - 1);
			m_scrollbar.SetScrollImmediate(scrollImmediate);
			m_deckTray.SaveScrollbarPosition(DeckTray.DeckContentTypes.Decks);
			m_editingTraySection.m_deckBox.transform.localScale = CollectionDeckBoxVisual.SCALED_DOWN_LOCAL_SCALE;
			Vector3 zero = Vector3.zero;
			zero.y = 1.273138f;
			m_editingTraySection.m_deckBox.transform.localPosition = zero;
			m_editingTraySection.m_deckBox.Hide();
			m_editingTraySection.m_deckBox.EnableButtonAnimation();
			traySection.m_deckBox.transform.localScale = CollectionDeckBoxVisual.SCALED_UP_LOCAL_SCALE;
			traySection.m_deckBox.transform.parent = null;
			traySection.m_deckBox.transform.position = m_deckEditTopPos.position;
			traySection.ShowDeckBoxNoAnim();
			traySection.m_deckBox.SetEnabled(enabled: true);
			m_editingTraySection = traySection;
		}
	}

	public void StartDragToReorder(CollectionDeckBoxVisual draggingDeckBox)
	{
		if (!(m_draggingDeckBox == draggingDeckBox))
		{
			if (m_draggingDeckBox != null)
			{
				StopDragToReorder();
			}
			if (CanDragToReorderDecks)
			{
				m_draggingDeckBox = draggingDeckBox;
				m_scrollbar.Pause(pause: true);
				m_scrollbar.PauseUpdateScrollHeight(pause: true);
			}
		}
	}

	public void StopDragToReorder()
	{
		if (m_draggingDeckBox != null)
		{
			foreach (CollectionDeck deck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
			{
				deck.SendChanges();
			}
			if (!UniversalInputManager.Get().IsTouchMode())
			{
				m_draggingDeckBox.ShowDeleteButton(show: true);
			}
			m_draggingDeckBox.OnStopDragToReorder();
		}
		m_draggingDeckBox = null;
		m_scrollbar.Pause(pause: false);
		m_scrollbar.PauseUpdateScrollHeight(pause: false);
	}

	private void UpdateDragToReorder()
	{
		if (m_draggingDeckBox == null)
		{
			return;
		}
		if (!UniversalInputManager.Get().GetMouseButton(0) || !CanDragToReorderDecks)
		{
			StopDragToReorder();
			return;
		}
		int num = m_traySections.FindIndex((TraySection section) => section.m_deckBox == m_draggingDeckBox);
		if (num < 0)
		{
			return;
		}
		TraySection traySection = m_traySections[num];
		if (traySection == null)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		Vector3 inNormal = -Camera.main.transform.forward;
		if (!new Plane(inNormal, m_traySectionStartPos.position).Raycast(ray, out var enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 size = TransformUtil.ComputeSetPointBounds(m_traySections[0], includeInactive: false).size;
		float z = m_traySectionStartPos.position.z;
		int value = Mathf.FloorToInt((0f - (point.z - z)) / size.z);
		int count = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
		if (count < 1)
		{
			return;
		}
		float num2 = m_scrollbar.m_ScrollBounds.bounds.min.z - z;
		int value2 = Mathf.FloorToInt((0f - (m_scrollbar.m_ScrollBounds.bounds.max.z - z)) / size.z) - 1;
		int value3 = Mathf.FloorToInt((0f - num2) / size.z) + 1;
		value2 = Mathf.Clamp(value2, 0, count - 1);
		value3 = Mathf.Clamp(value3, 0, count - 1);
		value = Mathf.Clamp(value, value2, value3);
		if (value >= m_traySections.Count || value == num)
		{
			return;
		}
		float tweenTime = 1f;
		TraySection traySection2 = m_traySections[value];
		Bounds bounds = TransformUtil.ComputeSetPointBounds(traySection2.gameObject, includeInactive: false);
		if (!m_scrollbar.ScrollObjectIntoView(traySection2.gameObject, bounds.center.z - traySection2.gameObject.transform.position.z, bounds.extents.z * 1.25f, null, iTween.EaseType.linear, tweenTime, blockInputWhileScrolling: true))
		{
			m_scrollbar.StopScroll();
		}
		m_traySections.RemoveAt(num);
		m_traySections.Insert(value, traySection);
		for (int i = 0; i < count; i++)
		{
			TraySection traySection3 = m_traySections[i];
			traySection3.m_deckBox.SetPositionIndex(i);
			CollectionDeck collectionDeck = traySection3.m_deckBox.GetCollectionDeck();
			if (collectionDeck != null)
			{
				collectionDeck.SortOrder = i + -100;
			}
		}
		RefreshTraySectionPositions(animateToNewPositions: true);
	}

	private void RefreshTraySectionPositions(bool animateToNewPositions)
	{
		Vector3 localPosition = m_traySectionStartPos.localPosition;
		Vector3 vector = Vector3.zero;
		Transform parent = m_traySectionStartPos.parent;
		for (int i = 0; i < 29; i++)
		{
			TraySection traySection = m_traySections[i];
			Bounds bounds = TransformUtil.ComputeSetPointBounds(traySection.gameObject, includeInactive: false);
			Vector3 position = traySection.transform.position;
			if (i > 0)
			{
				Vector3 vector2 = position - TransformUtil.ComputeWorldPoint(bounds, TransformUtil.GetUnitAnchor(Anchor.FRONT));
				Vector3 vector3 = vector + vector2;
				Vector3 vector4 = ((parent != null) ? parent.InverseTransformVector(vector3) : vector3);
				localPosition += vector4;
			}
			if (animateToNewPositions)
			{
				Hashtable args = iTween.Hash("position", localPosition, "isLocal", true, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
				iTween.MoveTo(traySection.gameObject, args);
			}
			else
			{
				traySection.gameObject.transform.localPosition = localPosition;
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
			vector = TransformUtil.ComputeWorldPoint(bounds, TransformUtil.GetUnitAnchor(Anchor.BACK)) - position;
		}
	}

	public bool UpdateDeckBoxWithNewId(long oldId, long newId)
	{
		foreach (TraySection traySection in m_traySections)
		{
			if (traySection.m_deckBox.GetDeckID() == oldId)
			{
				traySection.m_deckBox.SetDeckID(newId);
				return true;
			}
		}
		return false;
	}

	public void RefreshMissingCardIndicators()
	{
		foreach (TraySection traySection in m_traySections)
		{
			traySection.m_deckBox.UpdateMissingCardsIndicator();
		}
	}
}
