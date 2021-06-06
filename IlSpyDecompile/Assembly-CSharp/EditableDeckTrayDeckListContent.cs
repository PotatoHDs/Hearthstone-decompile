using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

public abstract class EditableDeckTrayDeckListContent : DeckTrayDeckListContent
{
	[CustomEditField(Sections = "Deck Button Settings")]
	public ParticleSystem m_deleteDeckPoof;

	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 m_deleteDeckPoofVisualOffset;

	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 m_rearrangeWiggleAxis = new Vector3(0f, 1f, 0f);

	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeWiggleAmplitude = 0.85f;

	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeWiggleFrequency = 15f;

	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeStartStopTweenDuration = 0.1f;

	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeEnlargeScale = 1.05f;

	[SerializeField]
	private Vector3 m_deckButtonOffset;

	[CustomEditField(Sections = "Deck Button Settings")]
	public GameObject m_newDeckButtonContainer;

	[CustomEditField(Sections = "Deck Button Settings")]
	public CollectionDeckTrayButton m_newDeckButton;

	protected string m_previousDeckName;

	protected const float DELETE_DECK_ANIM_TIME = 0.5f;

	protected bool m_deletingDecks;

	protected bool m_waitingToDeleteDeck;

	protected List<CollectionDeck> m_decksToDelete = new List<CollectionDeck>();

	protected TraySection m_newlyCreatedTraySection;

	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 DeckButtonOffset
	{
		get
		{
			return m_deckButtonOffset;
		}
		set
		{
			m_deckButtonOffset = value;
			UpdateNewDeckButton();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		CollectionManager collectionManager = CollectionManager.Get();
		collectionManager.RegisterFavoriteHeroChangedListener(OnFavoriteHeroChanged);
		collectionManager.RegisterOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemoved);
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += WillReset;
		}
	}

	protected override void OnDestroy()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		collectionManager.RemoveFavoriteHeroChangedListener(OnFavoriteHeroChanged);
		collectionManager.RemoveDeckDeletedListener(OnDeckDeleted);
		collectionManager.RemoveOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemoved);
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= WillReset;
		}
		base.OnDestroy();
	}

	protected override void Initialize()
	{
		if (m_initialized)
		{
			return;
		}
		m_newDeckButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnNewDeckButtonPress();
		});
		CollectionManager.Get().RegisterDeckDeletedListener(OnDeckDeleted);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_deckInfoActorPrefab, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError($"Unable to load actor {m_deckInfoActorPrefab}: null", base.gameObject);
			return;
		}
		m_deckInfoTooltip = gameObject.GetComponent<CollectionDeckInfo>();
		if (m_deckInfoTooltip == null)
		{
			Debug.LogError($"Actor {m_deckInfoActorPrefab} does not contain CollectionDeckInfo component.", base.gameObject);
			return;
		}
		GameUtils.SetParent(m_deckInfoTooltip, m_deckInfoTooltipBone);
		m_deckInfoTooltip.RegisterHideListener(HideDeckInfoListener);
		gameObject = AssetLoader.Get().InstantiatePrefab(m_deckOptionsPrefab);
		m_deckOptionsMenu = gameObject.GetComponent<DeckOptionsMenu>();
		GameUtils.SetParent(m_deckOptionsMenu.gameObject, m_deckOptionsBone);
		m_deckOptionsMenu.SetDeckInfo(m_deckInfoTooltip);
		HideDeckInfo();
		CreateTraySections();
		m_initialized = true;
	}

	private void WillReset()
	{
		Processor.CancelScheduledCallback(BeginAnimation);
		Processor.CancelScheduledCallback(EndAnimation);
	}

	private void OnNewDeckButtonPress()
	{
		if (IsModeActive() && !base.IsTouchDragging)
		{
			SoundManager.Get().LoadAndPlay("Hub_Click.prefab:cc2cf2b5507827149b13d12210c0f323");
			StartCreateNewDeck();
		}
	}

	protected abstract void StartCreateNewDeck();

	protected abstract void EndCreateNewDeck(bool newDeck);

	private void DeleteQueuedDecks(bool force = false)
	{
		if (m_decksToDelete.Count == 0 || (!IsModeActive() && !force))
		{
			return;
		}
		foreach (CollectionDeck item in m_decksToDelete)
		{
			Network.Get().DeleteDeck(item.ID, item.Type);
			CollectionManager.Get().AddPendingDeckDelete(item.ID);
			if (!Network.IsLoggedIn() || item.ID <= 0)
			{
				CollectionManager.Get().OnDeckDeletedWhileOffline(item.ID);
			}
		}
		m_decksToDelete.Clear();
	}

	private void OnDeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck != null)
		{
			m_waitingToDeleteDeck = false;
			long iD = removedDeck.ID;
			StartCoroutine(DeleteDeckAnimation(iD));
		}
	}

	private void OnFavoriteHeroChanged(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData)
	{
		UpdateDeckTrayVisuals();
	}

	private void OnUIHeroOverrideCardRemoved()
	{
		UpdateDeckTrayVisuals();
	}

	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		base.OnEditedDeckChanged(newDeck, oldDeck, isNewDeck);
		if (isNewDeck && newDeck != null)
		{
			m_newlyCreatedTraySection = GetExistingTrayFromDeck(newDeck);
			if (m_newlyCreatedTraySection != null)
			{
				m_centeringDeckList = m_newlyCreatedTraySection.m_deckBox.GetPositionIndex();
			}
		}
	}

	private IEnumerator DeleteDeckAnimation(long deckID, Action callback = null)
	{
		while (m_deletingDecks)
		{
			yield return null;
		}
		int num = 0;
		TraySection traySection = null;
		TraySection setNewDeckButtonPosition = m_traySections[0];
		for (int i = 0; i < m_traySections.Count; i++)
		{
			TraySection traySection2 = m_traySections[i];
			long deckID2 = traySection2.m_deckBox.GetDeckID();
			if (deckID2 == deckID)
			{
				num = i;
				traySection = traySection2;
			}
			else if (deckID2 == -1)
			{
				break;
			}
			setNewDeckButtonPosition = traySection2;
		}
		if (traySection == null)
		{
			Debug.LogWarning("Unable to delete deck with ID {0}. Not found in tray sections.", base.gameObject);
			yield break;
		}
		FireBusyWithDeckEvent(busy: true);
		m_deletingDecks = true;
		FireDeckCountChangedEvent();
		m_traySections.RemoveAt(num);
		GetIdealNewDeckButtonLocalPosition(setNewDeckButtonPosition, out var outPosition, out var newDeckBtnActive);
		Vector3 vector = traySection.transform.localPosition;
		if (HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown())
		{
			SoundManager.Get().LoadAndPlay("collection_manager_delete_deck.prefab:5ca16bec63041b741a4fb33706ed9cb1", base.gameObject);
			m_deleteDeckPoof.transform.position = traySection.m_deckBox.transform.position + m_deleteDeckPoofVisualOffset;
			m_deleteDeckPoof.Play(withChildren: true);
		}
		traySection.ClearDeckInfo();
		traySection.gameObject.SetActive(value: false);
		List<GameObject> animatingTraySections = new List<GameObject>();
		Action<object> action = delegate(object obj)
		{
			GameObject item = (GameObject)obj;
			animatingTraySections.Remove(item);
		};
		Vector3 localPosition = Vector3.zero;
		for (int j = num; j < m_traySections.Count; j++)
		{
			TraySection traySection3 = m_traySections[j];
			Vector3 localPosition2 = traySection3.transform.localPosition;
			iTween.MoveTo(traySection3.gameObject, iTween.Hash("position", vector, "isLocal", true, "time", 0.5f, "easeType", iTween.EaseType.easeOutBounce, "oncomplete", action, "oncompleteparams", traySection3.gameObject, "name", "position"));
			animatingTraySections.Add(traySection3.gameObject);
			if (j <= 25)
			{
				localPosition = localPosition2;
			}
			vector = localPosition2;
		}
		if (num == 26)
		{
			localPosition = traySection.transform.localPosition;
		}
		m_traySections.Insert(26, traySection);
		m_newDeckButton.SetIsUsable(CanShowNewDeckButton());
		traySection.gameObject.SetActive(value: true);
		traySection.HideDeckBox(immediate: true);
		traySection.transform.localPosition = localPosition;
		if (m_newDeckButton.gameObject.activeSelf)
		{
			iTween.MoveTo(m_newDeckButtonContainer, iTween.Hash("position", outPosition, "isLocal", true, "time", 0.5f, "easeType", iTween.EaseType.easeOutBounce, "oncomplete", action, "oncompleteparams", m_newDeckButtonContainer, "name", "position"));
			animatingTraySections.Add(m_newDeckButtonContainer);
		}
		else
		{
			m_newDeckButtonContainer.transform.localPosition = outPosition;
		}
		while (animatingTraySections.Count > 0)
		{
			animatingTraySections.RemoveAll((GameObject obj) => obj == null || !obj.activeInHierarchy);
			yield return null;
		}
		if (!CollectionManager.Get().IsInEditMode())
		{
			ShowNewDeckButton(newDeckBtnActive);
		}
		FireBusyWithDeckEvent(busy: false);
		callback?.Invoke();
		m_deletingDecks = false;
	}

	private void UpdateNewDeckButton(TraySection setNewDeckButtonPosition = null)
	{
		bool flag = UpdateNewDeckButtonPosition(setNewDeckButtonPosition);
		ShowNewDeckButton(flag && CanShowNewDeckButton());
	}

	private bool UpdateNewDeckButtonPosition(TraySection setNewDeckButtonPosition = null)
	{
		bool outActive = false;
		GetIdealNewDeckButtonLocalPosition(setNewDeckButtonPosition, out var outPosition, out outActive);
		m_newDeckButtonContainer.transform.localPosition = outPosition;
		return outActive;
	}

	private void GetIdealNewDeckButtonLocalPosition(TraySection setNewDeckButtonPosition, out Vector3 outPosition, out bool outActive)
	{
		TraySection lastUnusedTraySection = GetLastUnusedTraySection();
		TraySection traySection = ((setNewDeckButtonPosition == null) ? lastUnusedTraySection : setNewDeckButtonPosition);
		outActive = lastUnusedTraySection != null;
		outPosition = ((traySection != null) ? traySection.transform.localPosition : m_traySectionStartPos.localPosition) + m_deckButtonOffset;
	}

	public void ShowNewDeckButton(bool newDeckButtonActive, CollectionDeckTrayButton.DelOnAnimationFinished callback = null)
	{
		ShowNewDeckButton(newDeckButtonActive, null, callback);
	}

	public void ShowNewDeckButton(bool newDeckButtonActive, float? speed, CollectionDeckTrayButton.DelOnAnimationFinished callback = null)
	{
		if (m_newDeckButton.IsPoppedUp() != newDeckButtonActive)
		{
			if (newDeckButtonActive)
			{
				m_newDeckButton.gameObject.SetActive(value: true);
				m_newDeckButton.PlayPopUpAnimation(delegate
				{
					if (callback != null)
					{
						callback(this);
					}
				}, null, speed);
				return;
			}
			m_newDeckButton.PlayPopDownAnimation(delegate
			{
				m_newDeckButton.gameObject.SetActive(value: false);
				if (callback != null)
				{
					callback(this);
				}
			}, null, speed);
		}
		else if (callback != null)
		{
			callback(this);
		}
	}

	public override bool AnimateContentEntranceStart()
	{
		Initialize();
		long editDeckID = -1L;
		if (m_editingTraySection != null)
		{
			editDeckID = m_editingTraySection.m_deckBox.GetDeckID();
		}
		InitializeTraysFromDecks();
		SwapEditTrayIfNeeded(editDeckID);
		UpdateAllTrays(CollectionManagerDisplay.IsSpecialOneDeckMode(), initializeTrays: false);
		if (m_editingTraySection != null)
		{
			FinishRenamingEditingDeck();
			m_editingTraySection.MoveDeckBoxBackToOriginalPosition(0.25f, delegate
			{
				m_editingTraySection = null;
			});
		}
		m_newDeckButton.SetIsUsable(CanShowNewDeckButton());
		FireBusyWithDeckEvent(busy: true);
		FireDeckCountChangedEvent();
		CollectionManager.Get().DoneEditing();
		return true;
	}

	public override bool AnimateContentEntranceEnd()
	{
		if (m_editingTraySection != null)
		{
			return false;
		}
		m_newDeckButton.SetEnabled(enabled: true);
		FireBusyWithDeckEvent(busy: false);
		DeleteQueuedDecks(force: true);
		return true;
	}

	public override bool AnimateContentExitStart()
	{
		m_animatingExit = true;
		FireBusyWithDeckEvent(busy: true);
		float? speed = null;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			speed = 500f;
		}
		ShowNewDeckButton(newDeckButtonActive: false, speed);
		Processor.ScheduleCallback(0.5f, realTime: false, BeginAnimation);
		return true;
	}

	private void BeginAnimation(object userData)
	{
		float num = 0.5f;
		foreach (TraySection traySection in m_traySections)
		{
			if (m_editingTraySection != traySection)
			{
				traySection.HideDeckBox();
			}
		}
		if (m_newlyCreatedTraySection != null)
		{
			TraySection animateTraySection = m_newlyCreatedTraySection;
			UpdateNewDeckButtonPosition(animateTraySection);
			ShowNewDeckButton(newDeckButtonActive: true, delegate
			{
				animateTraySection.ShowDeckBox(immediate: true, delegate
				{
					animateTraySection.m_deckBox.gameObject.SetActive(value: false);
					m_newDeckButton.FlipHalfOverAndHide(0.1f, delegate
					{
						animateTraySection.FlipDeckBoxHalfOverToShow(0.1f, delegate
						{
							animateTraySection.MoveDeckBoxToEditPosition(m_deckEditTopPos.position, 0.25f);
						});
					});
				});
			});
			m_editingTraySection = m_newlyCreatedTraySection;
			m_newlyCreatedTraySection = null;
			num += 0.7f;
		}
		else if (m_editingTraySection != null)
		{
			m_editingTraySection.MoveDeckBoxToEditPosition(m_deckEditTopPos.position, 0.25f);
		}
		Processor.ScheduleCallback(num, realTime: false, EndAnimation);
	}

	private void EndAnimation(object userData)
	{
		m_animatingExit = false;
		FireBusyWithDeckEvent(busy: false);
	}

	private CollectionDeck UpdateRenamingEditingDeck(string newDeckName)
	{
		CollectionDeck editingDeck = m_deckTray.GetCardsContent().GetEditingDeck();
		if (editingDeck != null && !string.IsNullOrEmpty(newDeckName))
		{
			editingDeck.Name = newDeckName;
		}
		return editingDeck;
	}

	private void FinishRenamingEditingDeck(string newDeckName = null)
	{
		if (!(m_editingTraySection == null))
		{
			CollectionDeckBoxVisual deckBox = m_editingTraySection.m_deckBox;
			CollectionDeck collectionDeck = UpdateRenamingEditingDeck(newDeckName);
			if (collectionDeck != null && m_editingTraySection != null)
			{
				deckBox.SetDeckName(collectionDeck.Name);
			}
			if (UniversalInputManager.Get() != null && UniversalInputManager.Get().IsTextInputActive())
			{
				UniversalInputManager.Get().CancelTextInput(base.gameObject);
			}
			deckBox.ShowDeckName();
		}
	}

	protected override void HideDeckInfoListener()
	{
		if (m_editingTraySection != null)
		{
			SceneUtils.SetLayer(m_editingTraySection.m_deckBox.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(m_deckOptionsMenu.gameObject, GameLayer.Default);
			m_editingTraySection.m_deckBox.HideRenameVisuals();
		}
		FullScreenFXMgr.Get().StopDesaturate(0.25f, iTween.EaseType.easeInOutQuad);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (m_editingTraySection != null)
			{
				m_editingTraySection.m_deckBox.SetHighlightState(ActorStateType.NONE);
				m_editingTraySection.m_deckBox.ShowDeckName();
			}
			FinishRenamingEditingDeck();
		}
		m_deckOptionsMenu.Hide();
	}

	public override void HideTraySectionsNotInBounds(Bounds bounds)
	{
		base.HideTraySectionsNotInBounds(bounds);
		UIBScrollableItem component = m_newDeckButtonContainer.GetComponent<UIBScrollableItem>();
		if (component == null)
		{
			Debug.LogWarning("UIBScrollableItem not found on m_newDeckButtonContainer! This button may not be hidden properly while exiting Collection Manager!");
			return;
		}
		Bounds bounds2 = default(Bounds);
		component.GetWorldBounds(out var min, out var max);
		bounds2.SetMinMax(min, max);
		if (!bounds.Intersects(bounds2))
		{
			Log.DeckTray.Print("Hiding the New Deck button because it's out of the visible scroll area.");
			m_newDeckButton.gameObject.SetActive(value: false);
		}
	}

	public void CreateNewDeckFromUserSelection(TAG_CLASS heroClass, string heroCardID, string customDeckName = null, DeckSourceType deckSourceType = DeckSourceType.DECK_SOURCE_TYPE_NORMAL, string pastedDeckHashString = null)
	{
		bool num = SceneMgr.Get().IsInTavernBrawlMode();
		bool flag = SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN;
		DeckType deckType = DeckType.NORMAL_DECK;
		string value = customDeckName;
		if (num)
		{
			value = GameStrings.Get("GLUE_COLLECTION_TAVERN_BRAWL_DECKNAME");
			if (TavernBrawlManager.Get().CurrentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				deckType = DeckType.FSG_BRAWL_DECK;
				value = GameStrings.Get("GLUE_COLLECTION_FSG_BRAWL_DECKNAME");
			}
			else
			{
				deckType = DeckType.TAVERN_BRAWL_DECK;
			}
		}
		else if (flag)
		{
			deckType = DeckType.PVPDR_DECK;
			value = GameStrings.Get("GLUE_COLLECTION_DUEL_DECKNAME");
		}
		else if (string.IsNullOrEmpty(value))
		{
			value = CollectionManager.Get().AutoGenerateDeckName(heroClass);
		}
		CollectionManager.Get().SendCreateDeck(deckType, value, heroCardID, deckSourceType, pastedDeckHashString);
		EndCreateNewDeck(newDeck: true);
	}

	public void CreateNewDeckCancelled()
	{
		EndCreateNewDeck(newDeck: false);
	}

	public bool IsWaitingToDeleteDeck()
	{
		return m_waitingToDeleteDeck;
	}

	public int NumDecksToDelete()
	{
		return m_decksToDelete.Count;
	}

	public bool IsDeletingDecks()
	{
		return m_deletingDecks;
	}

	public void DeleteDeck(long deckID)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck == null)
		{
			Log.All.PrintError("Unable to delete deck id={0} - not found in cache.", deckID);
			return;
		}
		if (Network.IsLoggedIn() && deckID <= 0)
		{
			Log.Offline.PrintDebug("DeleteDeck() - Attempting to delete fake deck while online.");
		}
		deck.MarkBeingDeleted();
		m_decksToDelete.Add(deck);
		DeleteQueuedDecks();
	}

	public void DeleteEditingDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			Debug.LogWarning("No deck currently being edited!");
			return;
		}
		m_waitingToDeleteDeck = true;
		DeleteDeck(editedDeck.ID);
	}

	public void CancelRenameEditingDeck()
	{
		FinishRenamingEditingDeck();
	}

	public Vector3 GetNewDeckButtonPosition()
	{
		return m_newDeckButton.transform.localPosition;
	}

	public void UpdateDeckName(string deckName = null)
	{
		if (deckName == null)
		{
			CollectionDeck editingDeck = m_deckTray.GetCardsContent().GetEditingDeck();
			if (editingDeck == null)
			{
				return;
			}
			deckName = editingDeck.Name;
		}
		FinishRenamingEditingDeck(deckName);
	}

	public void RenameCurrentlyEditingDeck()
	{
		if (m_editingTraySection == null)
		{
			Debug.LogWarning("Unable to rename deck. No deck currently being edited.", base.gameObject);
		}
		else if (!CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			CollectionDeckBoxVisual deckBox = m_editingTraySection.m_deckBox;
			deckBox.HideDeckName();
			Camera camera = Box.Get().GetCamera();
			Bounds bounds = deckBox.GetDeckNameText().GetBounds();
			Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
			Font localizedFont = deckBox.GetDeckNameText().GetLocalizedFont();
			m_previousDeckName = deckBox.GetDeckNameText().Text;
			UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
			{
				m_owner = base.gameObject,
				m_rect = rect,
				m_updatedCallback = delegate(string newName)
				{
					UpdateRenamingEditingDeck(newName);
				},
				m_completedCallback = delegate(string newName)
				{
					FinishRenamingEditingDeck(newName);
				},
				m_canceledCallback = delegate
				{
					FinishRenamingEditingDeck(m_previousDeckName);
				},
				m_maxCharacters = CollectionDeck.DefaultMaxDeckNameCharacters,
				m_font = localizedFont,
				m_text = deckBox.GetDeckNameText().Text
			};
			UniversalInputManager.Get().UseTextInput(parms);
		}
	}

	protected override IEnumerator UpdateAllTraysAnimation(List<TraySection> showTraySections, bool immediate)
	{
		foreach (TraySection showTraySection in showTraySections)
		{
			showTraySection.ShowDeckBox(immediate);
			if (!immediate)
			{
				yield return new WaitForSeconds(0.015f);
			}
		}
		UpdateNewDeckButton();
		m_doneEntering = true;
	}

	protected override void ShowDeckInfo()
	{
		if (!UniversalInputManager.Get().IsTouchMode() && m_editingTraySection != null)
		{
			m_editingTraySection.m_deckBox.ShowRenameVisuals();
		}
		base.ShowDeckInfo();
	}

	protected override void OnDeckBoxVisualOver(CollectionDeckBoxVisual deckBox)
	{
		base.OnDeckBoxVisualOver(deckBox);
		if (!UniversalInputManager.Get().IsTouchMode() && IsModeTryingOrActive() && base.DraggingDeckBox == null)
		{
			deckBox.ShowDeleteButton(show: true);
		}
	}
}
