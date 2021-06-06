using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

// Token: 0x0200089D RID: 2205
public abstract class EditableDeckTrayDeckListContent : DeckTrayDeckListContent
{
	// Token: 0x17000701 RID: 1793
	// (get) Token: 0x0600794E RID: 31054 RVA: 0x0027918D File Offset: 0x0027738D
	// (set) Token: 0x0600794D RID: 31053 RVA: 0x0027917D File Offset: 0x0027737D
	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 DeckButtonOffset
	{
		get
		{
			return this.m_deckButtonOffset;
		}
		set
		{
			this.m_deckButtonOffset = value;
			this.UpdateNewDeckButton(null);
		}
	}

	// Token: 0x0600794F RID: 31055 RVA: 0x00279198 File Offset: 0x00277398
	protected override void Awake()
	{
		base.Awake();
		CollectionManager collectionManager = CollectionManager.Get();
		collectionManager.RegisterFavoriteHeroChangedListener(new CollectionManager.FavoriteHeroChangedCallback(this.OnFavoriteHeroChanged));
		collectionManager.RegisterOnUIHeroOverrideCardRemovedListener(new CollectionManager.OnUIHeroOverrideCardRemovedCallback(this.OnUIHeroOverrideCardRemoved));
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += this.WillReset;
		}
	}

	// Token: 0x06007950 RID: 31056 RVA: 0x002791F8 File Offset: 0x002773F8
	protected override void OnDestroy()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		collectionManager.RemoveFavoriteHeroChangedListener(new CollectionManager.FavoriteHeroChangedCallback(this.OnFavoriteHeroChanged));
		collectionManager.RemoveDeckDeletedListener(new CollectionManager.DelOnDeckDeleted(this.OnDeckDeleted));
		collectionManager.RemoveOnUIHeroOverrideCardRemovedListener(new CollectionManager.OnUIHeroOverrideCardRemovedCallback(this.OnUIHeroOverrideCardRemoved));
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.WillReset;
		}
		base.OnDestroy();
	}

	// Token: 0x06007951 RID: 31057 RVA: 0x0027926C File Offset: 0x0027746C
	protected override void Initialize()
	{
		if (this.m_initialized)
		{
			return;
		}
		this.m_newDeckButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnNewDeckButtonPress();
		});
		CollectionManager.Get().RegisterDeckDeletedListener(new CollectionManager.DelOnDeckDeleted(this.OnDeckDeleted));
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_deckInfoActorPrefab, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError(string.Format("Unable to load actor {0}: null", this.m_deckInfoActorPrefab), base.gameObject);
			return;
		}
		this.m_deckInfoTooltip = gameObject.GetComponent<CollectionDeckInfo>();
		if (this.m_deckInfoTooltip == null)
		{
			Debug.LogError(string.Format("Actor {0} does not contain CollectionDeckInfo component.", this.m_deckInfoActorPrefab), base.gameObject);
			return;
		}
		GameUtils.SetParent(this.m_deckInfoTooltip, this.m_deckInfoTooltipBone, false);
		this.m_deckInfoTooltip.RegisterHideListener(new CollectionDeckInfo.HideListener(this.HideDeckInfoListener));
		gameObject = AssetLoader.Get().InstantiatePrefab(this.m_deckOptionsPrefab, AssetLoadingOptions.None);
		this.m_deckOptionsMenu = gameObject.GetComponent<DeckOptionsMenu>();
		GameUtils.SetParent(this.m_deckOptionsMenu.gameObject, this.m_deckOptionsBone, false);
		this.m_deckOptionsMenu.SetDeckInfo(this.m_deckInfoTooltip);
		base.HideDeckInfo();
		base.CreateTraySections();
		this.m_initialized = true;
	}

	// Token: 0x06007952 RID: 31058 RVA: 0x002793AB File Offset: 0x002775AB
	private void WillReset()
	{
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.BeginAnimation), null);
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.EndAnimation), null);
	}

	// Token: 0x06007953 RID: 31059 RVA: 0x002793D3 File Offset: 0x002775D3
	private void OnNewDeckButtonPress()
	{
		if (!base.IsModeActive())
		{
			return;
		}
		if (base.IsTouchDragging)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("Hub_Click.prefab:cc2cf2b5507827149b13d12210c0f323");
		this.StartCreateNewDeck();
	}

	// Token: 0x06007954 RID: 31060
	protected abstract void StartCreateNewDeck();

	// Token: 0x06007955 RID: 31061
	protected abstract void EndCreateNewDeck(bool newDeck);

	// Token: 0x06007956 RID: 31062 RVA: 0x00279404 File Offset: 0x00277604
	private void DeleteQueuedDecks(bool force = false)
	{
		if (this.m_decksToDelete.Count == 0 || (!base.IsModeActive() && !force))
		{
			return;
		}
		foreach (CollectionDeck collectionDeck in this.m_decksToDelete)
		{
			Network.Get().DeleteDeck(collectionDeck.ID, collectionDeck.Type);
			CollectionManager.Get().AddPendingDeckDelete(collectionDeck.ID);
			if (!Network.IsLoggedIn() || collectionDeck.ID <= 0L)
			{
				CollectionManager.Get().OnDeckDeletedWhileOffline(collectionDeck.ID);
			}
		}
		this.m_decksToDelete.Clear();
	}

	// Token: 0x06007957 RID: 31063 RVA: 0x002794BC File Offset: 0x002776BC
	private void OnDeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck == null)
		{
			return;
		}
		this.m_waitingToDeleteDeck = false;
		long id = removedDeck.ID;
		base.StartCoroutine(this.DeleteDeckAnimation(id, null));
	}

	// Token: 0x06007958 RID: 31064 RVA: 0x00273458 File Offset: 0x00271658
	private void OnFavoriteHeroChanged(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData)
	{
		base.UpdateDeckTrayVisuals();
	}

	// Token: 0x06007959 RID: 31065 RVA: 0x00273458 File Offset: 0x00271658
	private void OnUIHeroOverrideCardRemoved()
	{
		base.UpdateDeckTrayVisuals();
	}

	// Token: 0x0600795A RID: 31066 RVA: 0x002794EC File Offset: 0x002776EC
	public override void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, bool isNewDeck)
	{
		base.OnEditedDeckChanged(newDeck, oldDeck, isNewDeck);
		if (isNewDeck && newDeck != null)
		{
			this.m_newlyCreatedTraySection = base.GetExistingTrayFromDeck(newDeck);
			if (this.m_newlyCreatedTraySection != null)
			{
				this.m_centeringDeckList = this.m_newlyCreatedTraySection.m_deckBox.GetPositionIndex();
			}
		}
	}

	// Token: 0x0600795B RID: 31067 RVA: 0x00279539 File Offset: 0x00277739
	private IEnumerator DeleteDeckAnimation(long deckID, Action callback = null)
	{
		while (this.m_deletingDecks)
		{
			yield return null;
		}
		int num = 0;
		TraySection traySection = null;
		TraySection setNewDeckButtonPosition = this.m_traySections[0];
		for (int i = 0; i < this.m_traySections.Count; i++)
		{
			TraySection traySection2 = this.m_traySections[i];
			long deckID2 = traySection2.m_deckBox.GetDeckID();
			if (deckID2 == deckID)
			{
				num = i;
				traySection = traySection2;
			}
			else if (deckID2 == -1L)
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
		base.FireBusyWithDeckEvent(true);
		this.m_deletingDecks = true;
		base.FireDeckCountChangedEvent();
		this.m_traySections.RemoveAt(num);
		Vector3 vector;
		bool newDeckBtnActive;
		this.GetIdealNewDeckButtonLocalPosition(setNewDeckButtonPosition, out vector, out newDeckBtnActive);
		Vector3 vector2 = traySection.transform.localPosition;
		if (HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown())
		{
			SoundManager.Get().LoadAndPlay("collection_manager_delete_deck.prefab:5ca16bec63041b741a4fb33706ed9cb1", base.gameObject);
			this.m_deleteDeckPoof.transform.position = traySection.m_deckBox.transform.position + this.m_deleteDeckPoofVisualOffset;
			this.m_deleteDeckPoof.Play(true);
		}
		traySection.ClearDeckInfo();
		traySection.gameObject.SetActive(false);
		List<GameObject> animatingTraySections = new List<GameObject>();
		Action<object> action = delegate(object obj)
		{
			GameObject item = (GameObject)obj;
			animatingTraySections.Remove(item);
		};
		Vector3 localPosition = Vector3.zero;
		for (int j = num; j < this.m_traySections.Count; j++)
		{
			TraySection traySection3 = this.m_traySections[j];
			Vector3 localPosition2 = traySection3.transform.localPosition;
			iTween.MoveTo(traySection3.gameObject, iTween.Hash(new object[]
			{
				"position",
				vector2,
				"isLocal",
				true,
				"time",
				0.5f,
				"easeType",
				iTween.EaseType.easeOutBounce,
				"oncomplete",
				action,
				"oncompleteparams",
				traySection3.gameObject,
				"name",
				"position"
			}));
			animatingTraySections.Add(traySection3.gameObject);
			if (j <= 25)
			{
				localPosition = localPosition2;
			}
			vector2 = localPosition2;
		}
		if (num == 26)
		{
			localPosition = traySection.transform.localPosition;
		}
		this.m_traySections.Insert(26, traySection);
		this.m_newDeckButton.SetIsUsable(base.CanShowNewDeckButton());
		traySection.gameObject.SetActive(true);
		traySection.HideDeckBox(true, null);
		traySection.transform.localPosition = localPosition;
		if (this.m_newDeckButton.gameObject.activeSelf)
		{
			iTween.MoveTo(this.m_newDeckButtonContainer, iTween.Hash(new object[]
			{
				"position",
				vector,
				"isLocal",
				true,
				"time",
				0.5f,
				"easeType",
				iTween.EaseType.easeOutBounce,
				"oncomplete",
				action,
				"oncompleteparams",
				this.m_newDeckButtonContainer,
				"name",
				"position"
			}));
			animatingTraySections.Add(this.m_newDeckButtonContainer);
		}
		else
		{
			this.m_newDeckButtonContainer.transform.localPosition = vector;
		}
		while (animatingTraySections.Count > 0)
		{
			animatingTraySections.RemoveAll((GameObject obj) => obj == null || !obj.activeInHierarchy);
			yield return null;
		}
		if (!CollectionManager.Get().IsInEditMode())
		{
			this.ShowNewDeckButton(newDeckBtnActive, null);
		}
		base.FireBusyWithDeckEvent(false);
		if (callback != null)
		{
			callback();
		}
		this.m_deletingDecks = false;
		yield break;
	}

	// Token: 0x0600795C RID: 31068 RVA: 0x00279558 File Offset: 0x00277758
	private void UpdateNewDeckButton(TraySection setNewDeckButtonPosition = null)
	{
		bool flag = this.UpdateNewDeckButtonPosition(setNewDeckButtonPosition);
		this.ShowNewDeckButton(flag && base.CanShowNewDeckButton(), null);
	}

	// Token: 0x0600795D RID: 31069 RVA: 0x00279580 File Offset: 0x00277780
	private bool UpdateNewDeckButtonPosition(TraySection setNewDeckButtonPosition = null)
	{
		bool result = false;
		Vector3 localPosition;
		this.GetIdealNewDeckButtonLocalPosition(setNewDeckButtonPosition, out localPosition, out result);
		this.m_newDeckButtonContainer.transform.localPosition = localPosition;
		return result;
	}

	// Token: 0x0600795E RID: 31070 RVA: 0x002795AC File Offset: 0x002777AC
	private void GetIdealNewDeckButtonLocalPosition(TraySection setNewDeckButtonPosition, out Vector3 outPosition, out bool outActive)
	{
		TraySection lastUnusedTraySection = base.GetLastUnusedTraySection();
		TraySection traySection = (setNewDeckButtonPosition == null) ? lastUnusedTraySection : setNewDeckButtonPosition;
		outActive = (lastUnusedTraySection != null);
		outPosition = ((traySection != null) ? traySection.transform.localPosition : this.m_traySectionStartPos.localPosition) + this.m_deckButtonOffset;
	}

	// Token: 0x0600795F RID: 31071 RVA: 0x0027960C File Offset: 0x0027780C
	public void ShowNewDeckButton(bool newDeckButtonActive, CollectionDeckTrayButton.DelOnAnimationFinished callback = null)
	{
		this.ShowNewDeckButton(newDeckButtonActive, null, callback);
	}

	// Token: 0x06007960 RID: 31072 RVA: 0x0027962C File Offset: 0x0027782C
	public void ShowNewDeckButton(bool newDeckButtonActive, float? speed, CollectionDeckTrayButton.DelOnAnimationFinished callback = null)
	{
		if (this.m_newDeckButton.IsPoppedUp() == newDeckButtonActive)
		{
			if (callback != null)
			{
				callback(this);
			}
			return;
		}
		if (newDeckButtonActive)
		{
			this.m_newDeckButton.gameObject.SetActive(true);
			this.m_newDeckButton.PlayPopUpAnimation(delegate(object o)
			{
				if (callback != null)
				{
					callback(this);
				}
			}, null, speed);
			return;
		}
		this.m_newDeckButton.PlayPopDownAnimation(delegate(object o)
		{
			this.m_newDeckButton.gameObject.SetActive(false);
			if (callback != null)
			{
				callback(this);
			}
		}, null, speed);
	}

	// Token: 0x06007961 RID: 31073 RVA: 0x002796B8 File Offset: 0x002778B8
	public override bool AnimateContentEntranceStart()
	{
		this.Initialize();
		long editDeckID = -1L;
		if (this.m_editingTraySection != null)
		{
			editDeckID = this.m_editingTraySection.m_deckBox.GetDeckID();
		}
		base.InitializeTraysFromDecks();
		base.SwapEditTrayIfNeeded(editDeckID);
		base.UpdateAllTrays(CollectionManagerDisplay.IsSpecialOneDeckMode(), false);
		if (this.m_editingTraySection != null)
		{
			this.FinishRenamingEditingDeck(null);
			this.m_editingTraySection.MoveDeckBoxBackToOriginalPosition(0.25f, delegate(object o)
			{
				this.m_editingTraySection = null;
			});
		}
		this.m_newDeckButton.SetIsUsable(base.CanShowNewDeckButton());
		base.FireBusyWithDeckEvent(true);
		base.FireDeckCountChangedEvent();
		CollectionManager.Get().DoneEditing();
		return true;
	}

	// Token: 0x06007962 RID: 31074 RVA: 0x00279760 File Offset: 0x00277960
	public override bool AnimateContentEntranceEnd()
	{
		if (this.m_editingTraySection != null)
		{
			return false;
		}
		this.m_newDeckButton.SetEnabled(true, false);
		base.FireBusyWithDeckEvent(false);
		this.DeleteQueuedDecks(true);
		return true;
	}

	// Token: 0x06007963 RID: 31075 RVA: 0x00279790 File Offset: 0x00277990
	public override bool AnimateContentExitStart()
	{
		this.m_animatingExit = true;
		base.FireBusyWithDeckEvent(true);
		float? speed = null;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			speed = new float?(500f);
		}
		this.ShowNewDeckButton(false, speed, null);
		Processor.ScheduleCallback(0.5f, false, new Processor.ScheduledCallback(this.BeginAnimation), null);
		return true;
	}

	// Token: 0x06007964 RID: 31076 RVA: 0x002797F0 File Offset: 0x002779F0
	private void BeginAnimation(object userData)
	{
		float num = 0.5f;
		foreach (TraySection traySection in this.m_traySections)
		{
			if (this.m_editingTraySection != traySection)
			{
				traySection.HideDeckBox(false, null);
			}
		}
		if (this.m_newlyCreatedTraySection != null)
		{
			TraySection animateTraySection = this.m_newlyCreatedTraySection;
			this.UpdateNewDeckButtonPosition(animateTraySection);
			TraySection.DelOnDoorStateChangedCallback <>9__3;
			CollectionDeckTrayButton.DelOnAnimationFinished <>9__2;
			TraySection.DelOnDoorStateChangedCallback <>9__1;
			this.ShowNewDeckButton(true, delegate(object _1)
			{
				TraySection animateTraySection = animateTraySection;
				bool immediate = true;
				TraySection.DelOnDoorStateChangedCallback callback;
				if ((callback = <>9__1) == null)
				{
					callback = (<>9__1 = delegate(object _2)
					{
						animateTraySection.m_deckBox.gameObject.SetActive(false);
						CollectionDeckTrayButton newDeckButton = this.m_newDeckButton;
						float animTime = 0.1f;
						CollectionDeckTrayButton.DelOnAnimationFinished finished;
						if ((finished = <>9__2) == null)
						{
							finished = (<>9__2 = delegate(object _3)
							{
								TraySection animateTraySection2 = animateTraySection;
								float animTime2 = 0.1f;
								TraySection.DelOnDoorStateChangedCallback callback2;
								if ((callback2 = <>9__3) == null)
								{
									callback2 = (<>9__3 = delegate(object _4)
									{
										animateTraySection.MoveDeckBoxToEditPosition(this.m_deckEditTopPos.position, 0.25f, null);
									});
								}
								animateTraySection2.FlipDeckBoxHalfOverToShow(animTime2, callback2);
							});
						}
						newDeckButton.FlipHalfOverAndHide(animTime, finished);
					});
				}
				animateTraySection.ShowDeckBox(immediate, callback);
			});
			this.m_editingTraySection = this.m_newlyCreatedTraySection;
			this.m_newlyCreatedTraySection = null;
			num += 0.7f;
		}
		else if (this.m_editingTraySection != null)
		{
			this.m_editingTraySection.MoveDeckBoxToEditPosition(this.m_deckEditTopPos.position, 0.25f, null);
		}
		Processor.ScheduleCallback(num, false, new Processor.ScheduledCallback(this.EndAnimation), null);
	}

	// Token: 0x06007965 RID: 31077 RVA: 0x002798FC File Offset: 0x00277AFC
	private void EndAnimation(object userData)
	{
		this.m_animatingExit = false;
		base.FireBusyWithDeckEvent(false);
	}

	// Token: 0x06007966 RID: 31078 RVA: 0x0027990C File Offset: 0x00277B0C
	private CollectionDeck UpdateRenamingEditingDeck(string newDeckName)
	{
		CollectionDeck editingDeck = this.m_deckTray.GetCardsContent().GetEditingDeck();
		if (editingDeck != null && !string.IsNullOrEmpty(newDeckName))
		{
			editingDeck.Name = newDeckName;
		}
		return editingDeck;
	}

	// Token: 0x06007967 RID: 31079 RVA: 0x00279940 File Offset: 0x00277B40
	private void FinishRenamingEditingDeck(string newDeckName = null)
	{
		if (this.m_editingTraySection == null)
		{
			return;
		}
		CollectionDeckBoxVisual deckBox = this.m_editingTraySection.m_deckBox;
		CollectionDeck collectionDeck = this.UpdateRenamingEditingDeck(newDeckName);
		if (collectionDeck != null && this.m_editingTraySection != null)
		{
			deckBox.SetDeckName(collectionDeck.Name);
		}
		if (UniversalInputManager.Get() != null && UniversalInputManager.Get().IsTextInputActive())
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject, false);
		}
		deckBox.ShowDeckName();
	}

	// Token: 0x06007968 RID: 31080 RVA: 0x002799B8 File Offset: 0x00277BB8
	protected override void HideDeckInfoListener()
	{
		if (this.m_editingTraySection != null)
		{
			SceneUtils.SetLayer(this.m_editingTraySection.m_deckBox.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(this.m_deckOptionsMenu.gameObject, GameLayer.Default);
			this.m_editingTraySection.m_deckBox.HideRenameVisuals();
		}
		FullScreenFXMgr.Get().StopDesaturate(0.25f, iTween.EaseType.easeInOutQuad, null, null);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (this.m_editingTraySection != null)
			{
				this.m_editingTraySection.m_deckBox.SetHighlightState(ActorStateType.NONE);
				this.m_editingTraySection.m_deckBox.ShowDeckName();
			}
			this.FinishRenamingEditingDeck(null);
		}
		this.m_deckOptionsMenu.Hide(true);
	}

	// Token: 0x06007969 RID: 31081 RVA: 0x00279A6C File Offset: 0x00277C6C
	public override void HideTraySectionsNotInBounds(Bounds bounds)
	{
		base.HideTraySectionsNotInBounds(bounds);
		UIBScrollableItem component = this.m_newDeckButtonContainer.GetComponent<UIBScrollableItem>();
		if (component == null)
		{
			Debug.LogWarning("UIBScrollableItem not found on m_newDeckButtonContainer! This button may not be hidden properly while exiting Collection Manager!");
			return;
		}
		Bounds bounds2 = default(Bounds);
		Vector3 min;
		Vector3 max;
		component.GetWorldBounds(out min, out max);
		bounds2.SetMinMax(min, max);
		if (!bounds.Intersects(bounds2))
		{
			Log.DeckTray.Print("Hiding the New Deck button because it's out of the visible scroll area.", Array.Empty<object>());
			this.m_newDeckButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600796A RID: 31082 RVA: 0x00279AEC File Offset: 0x00277CEC
	public void CreateNewDeckFromUserSelection(TAG_CLASS heroClass, string heroCardID, string customDeckName = null, DeckSourceType deckSourceType = DeckSourceType.DECK_SOURCE_TYPE_NORMAL, string pastedDeckHashString = null)
	{
		bool flag = SceneMgr.Get().IsInTavernBrawlMode();
		bool flag2 = SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN;
		DeckType deckType = DeckType.NORMAL_DECK;
		string text = customDeckName;
		if (flag)
		{
			text = GameStrings.Get("GLUE_COLLECTION_TAVERN_BRAWL_DECKNAME");
			if (TavernBrawlManager.Get().CurrentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				deckType = DeckType.FSG_BRAWL_DECK;
				text = GameStrings.Get("GLUE_COLLECTION_FSG_BRAWL_DECKNAME");
			}
			else
			{
				deckType = DeckType.TAVERN_BRAWL_DECK;
			}
		}
		else if (flag2)
		{
			deckType = DeckType.PVPDR_DECK;
			text = GameStrings.Get("GLUE_COLLECTION_DUEL_DECKNAME");
		}
		else if (string.IsNullOrEmpty(text))
		{
			text = CollectionManager.Get().AutoGenerateDeckName(heroClass);
		}
		CollectionManager.Get().SendCreateDeck(deckType, text, heroCardID, deckSourceType, pastedDeckHashString);
		this.EndCreateNewDeck(true);
	}

	// Token: 0x0600796B RID: 31083 RVA: 0x00279B81 File Offset: 0x00277D81
	public void CreateNewDeckCancelled()
	{
		this.EndCreateNewDeck(false);
	}

	// Token: 0x0600796C RID: 31084 RVA: 0x00279B8A File Offset: 0x00277D8A
	public bool IsWaitingToDeleteDeck()
	{
		return this.m_waitingToDeleteDeck;
	}

	// Token: 0x0600796D RID: 31085 RVA: 0x00279B92 File Offset: 0x00277D92
	public int NumDecksToDelete()
	{
		return this.m_decksToDelete.Count;
	}

	// Token: 0x0600796E RID: 31086 RVA: 0x00279B9F File Offset: 0x00277D9F
	public bool IsDeletingDecks()
	{
		return this.m_deletingDecks;
	}

	// Token: 0x0600796F RID: 31087 RVA: 0x00279BA8 File Offset: 0x00277DA8
	public void DeleteDeck(long deckID)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck == null)
		{
			Log.All.PrintError("Unable to delete deck id={0} - not found in cache.", new object[]
			{
				deckID
			});
			return;
		}
		if (Network.IsLoggedIn() && deckID <= 0L)
		{
			Log.Offline.PrintDebug("DeleteDeck() - Attempting to delete fake deck while online.", Array.Empty<object>());
		}
		deck.MarkBeingDeleted();
		this.m_decksToDelete.Add(deck);
		this.DeleteQueuedDecks(false);
	}

	// Token: 0x06007970 RID: 31088 RVA: 0x00279C1C File Offset: 0x00277E1C
	public void DeleteEditingDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			Debug.LogWarning("No deck currently being edited!");
			return;
		}
		this.m_waitingToDeleteDeck = true;
		this.DeleteDeck(editedDeck.ID);
	}

	// Token: 0x06007971 RID: 31089 RVA: 0x00279C55 File Offset: 0x00277E55
	public void CancelRenameEditingDeck()
	{
		this.FinishRenamingEditingDeck(null);
	}

	// Token: 0x06007972 RID: 31090 RVA: 0x00279C5E File Offset: 0x00277E5E
	public Vector3 GetNewDeckButtonPosition()
	{
		return this.m_newDeckButton.transform.localPosition;
	}

	// Token: 0x06007973 RID: 31091 RVA: 0x00279C70 File Offset: 0x00277E70
	public void UpdateDeckName(string deckName = null)
	{
		if (deckName == null)
		{
			CollectionDeck editingDeck = this.m_deckTray.GetCardsContent().GetEditingDeck();
			if (editingDeck == null)
			{
				return;
			}
			deckName = editingDeck.Name;
		}
		this.FinishRenamingEditingDeck(deckName);
	}

	// Token: 0x06007974 RID: 31092 RVA: 0x00279CA8 File Offset: 0x00277EA8
	public void RenameCurrentlyEditingDeck()
	{
		if (this.m_editingTraySection == null)
		{
			Debug.LogWarning("Unable to rename deck. No deck currently being edited.", base.gameObject);
			return;
		}
		if (CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			return;
		}
		CollectionDeckBoxVisual deckBox = this.m_editingTraySection.m_deckBox;
		deckBox.HideDeckName();
		Camera camera = Box.Get().GetCamera();
		Bounds bounds = deckBox.GetDeckNameText().GetBounds();
		Rect rect = CameraUtils.CreateGUIViewportRect(camera, bounds.min, bounds.max);
		Font localizedFont = deckBox.GetDeckNameText().GetLocalizedFont();
		this.m_previousDeckName = deckBox.GetDeckNameText().Text;
		UniversalInputManager.TextInputParams parms = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_rect = rect,
			m_updatedCallback = delegate(string newName)
			{
				this.UpdateRenamingEditingDeck(newName);
			},
			m_completedCallback = delegate(string newName)
			{
				this.FinishRenamingEditingDeck(newName);
			},
			m_canceledCallback = delegate(bool a1, GameObject a2)
			{
				this.FinishRenamingEditingDeck(this.m_previousDeckName);
			},
			m_maxCharacters = CollectionDeck.DefaultMaxDeckNameCharacters,
			m_font = localizedFont,
			m_text = deckBox.GetDeckNameText().Text
		};
		UniversalInputManager.Get().UseTextInput(parms, false);
	}

	// Token: 0x06007975 RID: 31093 RVA: 0x00279DB5 File Offset: 0x00277FB5
	protected override IEnumerator UpdateAllTraysAnimation(List<TraySection> showTraySections, bool immediate)
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
		this.UpdateNewDeckButton(null);
		this.m_doneEntering = true;
		yield break;
		yield break;
	}

	// Token: 0x06007976 RID: 31094 RVA: 0x00279DD2 File Offset: 0x00277FD2
	protected override void ShowDeckInfo()
	{
		if (!UniversalInputManager.Get().IsTouchMode() && this.m_editingTraySection != null)
		{
			this.m_editingTraySection.m_deckBox.ShowRenameVisuals();
		}
		base.ShowDeckInfo();
	}

	// Token: 0x06007977 RID: 31095 RVA: 0x00279E04 File Offset: 0x00278004
	protected override void OnDeckBoxVisualOver(CollectionDeckBoxVisual deckBox)
	{
		base.OnDeckBoxVisualOver(deckBox);
		if (!UniversalInputManager.Get().IsTouchMode() && base.IsModeTryingOrActive() && base.DraggingDeckBox == null)
		{
			deckBox.ShowDeleteButton(true);
		}
	}

	// Token: 0x04005E45 RID: 24133
	[CustomEditField(Sections = "Deck Button Settings")]
	public ParticleSystem m_deleteDeckPoof;

	// Token: 0x04005E46 RID: 24134
	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 m_deleteDeckPoofVisualOffset;

	// Token: 0x04005E47 RID: 24135
	[CustomEditField(Sections = "Deck Button Settings")]
	public Vector3 m_rearrangeWiggleAxis = new Vector3(0f, 1f, 0f);

	// Token: 0x04005E48 RID: 24136
	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeWiggleAmplitude = 0.85f;

	// Token: 0x04005E49 RID: 24137
	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeWiggleFrequency = 15f;

	// Token: 0x04005E4A RID: 24138
	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeStartStopTweenDuration = 0.1f;

	// Token: 0x04005E4B RID: 24139
	[CustomEditField(Sections = "Deck Button Settings")]
	public float m_rearrangeEnlargeScale = 1.05f;

	// Token: 0x04005E4C RID: 24140
	[SerializeField]
	private Vector3 m_deckButtonOffset;

	// Token: 0x04005E4D RID: 24141
	[CustomEditField(Sections = "Deck Button Settings")]
	public GameObject m_newDeckButtonContainer;

	// Token: 0x04005E4E RID: 24142
	[CustomEditField(Sections = "Deck Button Settings")]
	public CollectionDeckTrayButton m_newDeckButton;

	// Token: 0x04005E4F RID: 24143
	protected string m_previousDeckName;

	// Token: 0x04005E50 RID: 24144
	protected const float DELETE_DECK_ANIM_TIME = 0.5f;

	// Token: 0x04005E51 RID: 24145
	protected bool m_deletingDecks;

	// Token: 0x04005E52 RID: 24146
	protected bool m_waitingToDeleteDeck;

	// Token: 0x04005E53 RID: 24147
	protected List<CollectionDeck> m_decksToDelete = new List<CollectionDeck>();

	// Token: 0x04005E54 RID: 24148
	protected TraySection m_newlyCreatedTraySection;
}
