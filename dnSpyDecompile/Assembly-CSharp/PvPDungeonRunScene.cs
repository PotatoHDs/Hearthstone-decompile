using System;
using System.Collections;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200062D RID: 1581
[CustomEditClass]
public class PvPDungeonRunScene : PegasusScene
{
	// Token: 0x060058C0 RID: 22720 RVA: 0x001CF54D File Offset: 0x001CD74D
	public static PvPDungeonRunScene Get()
	{
		return PvPDungeonRunScene.m_instance;
	}

	// Token: 0x060058C1 RID: 22721 RVA: 0x001CF554 File Offset: 0x001CD754
	public static bool IsEditingDeck()
	{
		return PvPDungeonRunScene.m_instance != null && PvPDungeonRunScene.m_instance.m_isEditingDeck && !PvPDungeonRunScene.m_instance.m_isTransitioningToCollection;
	}

	// Token: 0x060058C2 RID: 22722 RVA: 0x001CF580 File Offset: 0x001CD780
	public void Start()
	{
		PvPDungeonRunScene.m_instance = this;
		AssetLoader.Get().InstantiatePrefab(this.m_CollectionManagerPrefab, new PrefabCallback<GameObject>(this.OnCollectionManagerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab(this.m_PopupManagerPrefab, new PrefabCallback<GameObject>(this.OnPopupManagerLoaded), null, AssetLoadingOptions.None);
		Network.Get().RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRStatsResponse), null);
		Network.Get().RequestPVPDRStatsInfo();
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse), null);
		Network.Get().SendPVPDRSessionInfoRequest();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Duels);
		this.m_adventureDefCache = new AdventureDefCache(false);
		this.m_adventureWingDefCache = new AdventureWingDefCache(false);
		base.StartCoroutine(this.NotifySceneLoadedWhenReady());
	}

	// Token: 0x060058C3 RID: 22723 RVA: 0x00019DD3 File Offset: 0x00017FD3
	public void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x060058C4 RID: 22724 RVA: 0x001CF66F File Offset: 0x001CD86F
	public void OnDestroy()
	{
		PvPDungeonRunScene.m_instance = null;
	}

	// Token: 0x060058C5 RID: 22725 RVA: 0x001CF678 File Offset: 0x001CD878
	public GameSaveKeyId GetGSDKeyForAdventure()
	{
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(this.m_currentAdventure, AdventureModeDbId.DUNGEON_CRAWL);
		if (adventureDataRecord != null)
		{
			return (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		}
		Debug.LogError("PvPDungeonRunScene.GetGSDKeyForAdventure called but could not find record for adventureId = " + this.m_currentAdventure);
		return (GameSaveKeyId)0;
	}

	// Token: 0x060058C6 RID: 22726 RVA: 0x001CF6B7 File Offset: 0x001CD8B7
	public int GetSeasonID()
	{
		return this.m_seasonId;
	}

	// Token: 0x060058C7 RID: 22727 RVA: 0x001CF6C0 File Offset: 0x001CD8C0
	private void OnCollectionManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_collectionManagerPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError(string.Format("PvPDungeonRunScene.OnCollectionManagerLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		this.m_collectionManager = go;
		this.m_collectionManager.SetActive(true);
		this.m_collectionManager.transform.SetParent(base.transform, false);
		this.m_collectionManager.transform.localPosition = this.CM_POS;
		this.m_collectionManager.transform.localScale = this.CM_SCALE;
	}

	// Token: 0x060058C8 RID: 22728 RVA: 0x001CF744 File Offset: 0x001CD944
	public void OnGuestHeroSelected(TAG_CLASS classId, GuestHeroDbfRecord record)
	{
		this.m_services.DungeonCrawlData.SelectedHeroClass = classId;
		this.TransitionToDungeonCrawlPlayMat();
	}

	// Token: 0x060058C9 RID: 22729 RVA: 0x001CF760 File Offset: 0x001CD960
	public bool TransitionToGuestHeroPicker()
	{
		bool flag = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", new PrefabCallback<GameObject>(this.OnGuestHeroPickerLoaded), null, AssetLoadingOptions.None);
		if (!flag)
		{
			Debug.LogError("PvPDungeonRunDisplay could not load the GuestHeroPicker prefab.");
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DUELS_HEADLINE");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		return flag;
	}

	// Token: 0x060058CA RID: 22730 RVA: 0x001CF7DB File Offset: 0x001CD9DB
	public void TransitionToDungeonCrawlPlayMat()
	{
		if (this.m_dungeonCrawlDisplay == null)
		{
			DungeonCrawlUtil.LoadDungeonRunPrefab(delegate(GameObject go)
			{
				AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
				if (component)
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.DUELS_IDLE
					});
					this.m_dungeonCrawlDisplay = component;
					GameUtils.SetParent(go, base.transform, false);
					component.StartRun(this.m_services);
				}
			});
		}
	}

	// Token: 0x060058CB RID: 22731 RVA: 0x001CF7FC File Offset: 0x001CD9FC
	public bool TransitionFromDungeonCrawlPlayMat()
	{
		this.m_displayRoot.SetActive(true);
		Vector3 up = Vector3.up;
		up.x -= this.m_transitionStartingOffset;
		this.m_displayRoot.transform.localPosition = up;
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"position",
			Vector3.zero,
			"time",
			1,
			"easeType",
			"easeOutBounce",
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.m_display.EnableButtons(true);
				if (this.m_dungeonCrawlDisplay != null)
				{
					if (DuelsConfig.Get().RunRecentlyEnded())
					{
						this.m_display.CheckForStatsChanged();
					}
					UnityEngine.Object.Destroy(this.m_dungeonCrawlDisplay.gameObject);
					this.m_dungeonCrawlDisplay = null;
					Network.Get().SendPVPDRSessionInfoRequest();
				}
			}),
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_displayRoot.gameObject, args);
		if (!string.IsNullOrEmpty(this.m_SlideInSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_SlideInSound);
		}
		if (CollectionManager.Get().IsInEditMode())
		{
			CollectionManager.Get().DoneEditing();
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.OnDoneEditingDeck();
			}
		}
		return true;
	}

	// Token: 0x060058CC RID: 22732 RVA: 0x001CF928 File Offset: 0x001CDB28
	public bool NavigateBackFromPlaymat()
	{
		PVPDRLobbyDataModel pvpdrlobbyDataModel = this.m_display.GetPVPDRLobbyDataModel();
		bool flag = pvpdrlobbyDataModel.HasSession && !pvpdrlobbyDataModel.IsSessionActive;
		if (!pvpdrlobbyDataModel.HasSession || flag || DuelsConfig.IsInitialLoadoutComplete())
		{
			if (!this.TransitionFromDungeonCrawlPlayMat())
			{
				Navigation.Push(new Navigation.NavigateBackHandler(this.NavigateBackFromPlaymat));
			}
		}
		else if (!this.TransitionToGuestHeroPicker())
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.NavigateBackFromPlaymat));
		}
		return true;
	}

	// Token: 0x060058CD RID: 22733 RVA: 0x001CF99F File Offset: 0x001CDB9F
	public void TransitionBackFromGuestHeroPicker()
	{
		GuestHeroPickerDisplay.Get().HideTray(0f);
		this.m_displayRoot.SetActive(true);
		this.m_displayRoot.transform.localPosition = Vector3.up;
		this.m_display.EnableButtons(false);
	}

	// Token: 0x060058CE RID: 22734 RVA: 0x001CF9DD File Offset: 0x001CDBDD
	public void SetAdventureData()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.SetSelectedAdventureMode(this.m_services.DungeonCrawlData.GetSelectedAdventure(), AdventureModeDbId.DUNGEON_CRAWL);
		adventureConfig.SetMission(this.m_services.DungeonCrawlData.GetMission(), true);
	}

	// Token: 0x060058CF RID: 22735 RVA: 0x001CFA14 File Offset: 0x001CDC14
	public void ShowDungeonCrawlDisplay(Action<object> action)
	{
		int num = UniversalInputManager.UsePhoneUI ? 0 : 3;
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"position",
			new Vector3(0f, (float)num, 0f),
			"time",
			1,
			"easeType",
			"easeOutBounce",
			"oncomplete",
			action,
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_dungeonCrawlDisplay.gameObject, args);
		this.m_isEditingDeck = false;
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DUELS_IDLE
		});
	}

	// Token: 0x060058D0 RID: 22736 RVA: 0x001CFAE8 File Offset: 0x001CDCE8
	public void HideDungeonCrawlDisplay()
	{
		this.m_isTransitioningToCollection = true;
		int num = UniversalInputManager.UsePhoneUI ? -180 : -110;
		Hashtable args = iTween.Hash(new object[]
		{
			"islocal",
			true,
			"position",
			new Vector3((float)num, 0f, 0f),
			"time",
			1,
			"easeType",
			"easeOutBounce",
			"oncomplete",
			new Action<object>(delegate(object e)
			{
				this.m_isTransitioningToCollection = false;
			}),
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_dungeonCrawlDisplay.gameObject, args);
		this.m_isEditingDeck = true;
	}

	// Token: 0x060058D1 RID: 22737 RVA: 0x001CFBB8 File Offset: 0x001CDDB8
	public void OnHeroPickerShown()
	{
		this.m_display.OnHeroPickerShown();
		this.m_display.EnableButtons(false);
		if (this.m_displayRoot != null)
		{
			this.m_displayRoot.SetActive(false);
		}
		if (this.m_dungeonCrawlDisplay != null)
		{
			UnityEngine.Object.Destroy(this.m_dungeonCrawlDisplay.gameObject);
			this.m_dungeonCrawlDisplay = null;
		}
	}

	// Token: 0x060058D2 RID: 22738 RVA: 0x001CFC1B File Offset: 0x001CDE1B
	public void OnHeroPickerHidden()
	{
		this.m_display.EnableButtons(true);
	}

	// Token: 0x060058D3 RID: 22739 RVA: 0x001CFC29 File Offset: 0x001CDE29
	public AdventureDef GetAdventureDef(AdventureDbId advId)
	{
		return this.m_adventureDefCache.GetDef(advId);
	}

	// Token: 0x060058D4 RID: 22740 RVA: 0x001CFC37 File Offset: 0x001CDE37
	public AdventureWingDef GetWingDef(WingDbId wingId)
	{
		return this.m_adventureWingDefCache.GetDef(wingId);
	}

	// Token: 0x060058D5 RID: 22741 RVA: 0x001CFC45 File Offset: 0x001CDE45
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x060058D6 RID: 22742 RVA: 0x001CFC50 File Offset: 0x001CDE50
	public override void Unload()
	{
		this.m_unloading = true;
		if (UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(true);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.Unload();
		}
		if (this.m_displayRoot != null)
		{
			UnityEngine.Object.Destroy(this.m_displayRoot);
		}
		if (this.m_dungeonCrawlDisplay != null)
		{
			UnityEngine.Object.Destroy(this.m_dungeonCrawlDisplay.gameObject);
		}
		if (this.m_collectionManager != null)
		{
			UnityEngine.Object.Destroy(this.m_collectionManager.gameObject);
		}
		if (this.m_guestHeroPickerTrayDisplay != null)
		{
			UnityEngine.Object.Destroy(this.m_guestHeroPickerTrayDisplay.gameObject);
		}
		if (this.m_adventureDefCache != null)
		{
			this.m_adventureDefCache.Unload();
		}
		if (this.m_adventureWingDefCache != null)
		{
			this.m_adventureWingDefCache.Unload();
		}
		if (this.m_popupManagerRoot != null)
		{
			UnityEngine.Object.Destroy(this.m_popupManagerRoot.gameObject);
		}
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse));
		this.m_unloading = false;
	}

	// Token: 0x060058D7 RID: 22743 RVA: 0x001CFD7C File Offset: 0x001CDF7C
	private void DoDungeonRunTransition()
	{
		if (GuestHeroPickerDisplay.Get())
		{
			this.m_services.SubsceneController.OnTransitionComplete();
			GuestHeroPickerDisplay.Get().HideTray(0f);
			return;
		}
		if (this.m_displayRoot != null && this.m_displayRoot.gameObject.activeInHierarchy)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x -= this.m_transitionStartingOffset;
			Hashtable args = iTween.Hash(new object[]
			{
				"islocal",
				true,
				"position",
				localPosition,
				"time",
				this.m_transitionTime,
				"easeType",
				"easeOutBounce",
				"oncomplete",
				new Action<object>(delegate(object e)
				{
					this.m_displayRoot.SetActive(false);
					this.m_services.SubsceneController.OnTransitionComplete();
					if (this.m_dungeonCrawlDisplay != null)
					{
						this.m_dungeonCrawlDisplay.EnableBackButton(true);
					}
				}),
				"oncompletetarget",
				base.gameObject
			});
			if (this.m_dungeonCrawlDisplay != null)
			{
				this.m_dungeonCrawlDisplay.EnableBackButton(false);
			}
			iTween.MoveTo(this.m_displayRoot.gameObject, args);
			if (!string.IsNullOrEmpty(this.m_SlideOutSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_SlideOutSound);
				return;
			}
		}
		else
		{
			this.m_services.SubsceneController.OnTransitionComplete();
		}
	}

	// Token: 0x060058D8 RID: 22744 RVA: 0x001CFED6 File Offset: 0x001CE0D6
	private void CreateServices(AdventureDbId adventureId)
	{
		this.m_assetLoadingHelper = new AssetLoadingHelper();
		this.m_assetLoadingHelper.AssetLoadingComplete += this.OnAssetLoadingComplete;
		this.m_services = DungeonCrawlUtil.CreatePvPDungeonCrawlServices(adventureId, this.m_assetLoadingHelper);
	}

	// Token: 0x060058D9 RID: 22745 RVA: 0x001CFF0C File Offset: 0x001CE10C
	private void OnAssetLoadingComplete(object sender, EventArgs args)
	{
		if (this.m_services != null && this.m_dungeonCrawlDisplay != null)
		{
			this.DoDungeonRunTransition();
		}
	}

	// Token: 0x060058DA RID: 22746 RVA: 0x001CFF2A File Offset: 0x001CE12A
	private void OnGameSaveDataReceived(bool success)
	{
		this.m_gameSaveDataReceived = true;
	}

	// Token: 0x060058DB RID: 22747 RVA: 0x001CFF33 File Offset: 0x001CE133
	private void OnGuestHeroPickerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.SetParent(base.transform);
		this.m_guestHeroPickerTrayDisplay = go.GetComponentInChildren<GuestHeroPickerTrayDisplay>();
	}

	// Token: 0x060058DC RID: 22748 RVA: 0x001CFF52 File Offset: 0x001CE152
	private void OnPVPDRStatsResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRStatsInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRStatsResponse));
		this.m_hasStatsInfo = true;
	}

	// Token: 0x060058DD RID: 22749 RVA: 0x001CFF7C File Offset: 0x001CE17C
	private void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pvpdrsessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		this.m_hasSession = pvpdrsessionInfoResponse.HasSession;
		this.m_hasLatestSessionData = true;
		if (this.m_services == null)
		{
			this.m_currentAdventure = AdventureDbId.INVALID;
			if (pvpdrsessionInfoResponse.HasCurrentSeason)
			{
				this.m_seasonId = pvpdrsessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId;
				PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(this.m_seasonId);
				if (record != null)
				{
					this.m_currentAdventure = (AdventureDbId)record.AdventureId;
					AdventureDbfRecord record2 = GameDbf.Adventure.GetRecord((int)this.m_currentAdventure);
					this.m_adventureDefCache.LoadDefForId(this.m_currentAdventure);
					this.m_adventureWingDefCache.LoadDefForId((WingDbId)record2.Wings[0].ID);
				}
			}
			this.CreateServices(this.m_currentAdventure);
			this.SetAdventureData();
		}
	}

	// Token: 0x060058DE RID: 22750 RVA: 0x001D004E File Offset: 0x001CE24E
	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("PvPDungeonRunScene.OnScreenLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		this.m_displayRoot = go;
		this.m_displayRoot.transform.SetParent(base.transform);
		this.m_screenPrefabLoaded = true;
	}

	// Token: 0x060058DF RID: 22751 RVA: 0x001D008E File Offset: 0x001CE28E
	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!this.m_hasStatsInfo)
		{
			yield return null;
		}
		while (!this.m_hasLatestSessionData)
		{
			yield return null;
		}
		GameSaveDataManager.Get().Request(this.GetGSDKeyForAdventure(), new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnGameSaveDataReceived));
		while (!this.m_gameSaveDataReceived)
		{
			yield return null;
		}
		AssetLoader.Get().InstantiatePrefab(this.m_screenPrefab, new PrefabCallback<GameObject>(this.OnScreenPrefabLoaded), null, AssetLoadingOptions.None);
		while (!this.m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (this.m_display == null)
		{
			this.m_display = this.m_displayRoot.GetComponentInChildren<PvPDungeonRunDisplay>();
			yield return null;
		}
		while (!this.m_display.IsFinishedLoading)
		{
			yield return null;
		}
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DUELS_IDLE
		});
		bool recentLoss = DuelsConfig.Get().HasRecentLoss();
		bool recentWin = DuelsConfig.Get().HasRecentWin();
		bool flag = SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY;
		this.m_displayRoot.SetActive(!flag);
		if (flag)
		{
			this.m_display.GetPVPDRLobbyDataModel().RecentLoss = recentLoss;
			this.m_display.GetPVPDRLobbyDataModel().RecentWin = recentWin;
			this.TransitionToDungeonCrawlPlayMat();
			while (this.m_dungeonCrawlDisplay == null)
			{
				yield return null;
			}
		}
		while (!this.m_collectionManagerPrefabLoaded)
		{
			yield return null;
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		collectionManagerDisplay.EnableInput(false);
		collectionManagerDisplay.PopulateSetFilters(true);
		SceneMgr.Get().NotifySceneLoaded();
		yield break;
	}

	// Token: 0x060058E0 RID: 22752 RVA: 0x001D009D File Offset: 0x001CE29D
	public DuelsPopupManager GetPopupManager()
	{
		return this.m_PopupManager;
	}

	// Token: 0x060058E1 RID: 22753 RVA: 0x001D00A8 File Offset: 0x001CE2A8
	private void OnPopupManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("PvPDungeonRunScene.OnPopupManagerLoaded() - failed to load screen {0}", assetRef));
			return;
		}
		this.m_popupManagerRoot = go;
		this.m_popupManagerRoot.transform.SetParent(base.gameObject.transform);
		this.m_PopupManager = go.GetComponentInChildren<DuelsPopupManager>();
	}

	// Token: 0x060058E2 RID: 22754 RVA: 0x001D0100 File Offset: 0x001CE300
	public static void ShowDuelsMessagePopup(string header, string message, string rating, Action callback)
	{
		DuelsPopupManager popupManager = PvPDungeonRunScene.m_instance.GetPopupManager();
		if (popupManager != null)
		{
			popupManager.ShowNotice(header, message, rating, callback);
		}
	}

	// Token: 0x04004BFB RID: 19451
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	// Token: 0x04004BFC RID: 19452
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;

	// Token: 0x04004BFD RID: 19453
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_PopupManagerPrefab;

	// Token: 0x04004BFE RID: 19454
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionStartingOffset = 100f;

	// Token: 0x04004BFF RID: 19455
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionTime = 1f;

	// Token: 0x04004C00 RID: 19456
	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_rootDropHeight = 10f;

	// Token: 0x04004C01 RID: 19457
	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	// Token: 0x04004C02 RID: 19458
	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideOutSound;

	// Token: 0x04004C03 RID: 19459
	private bool m_unloading;

	// Token: 0x04004C04 RID: 19460
	private bool m_screenPrefabLoaded;

	// Token: 0x04004C05 RID: 19461
	private bool m_gameSaveDataReceived;

	// Token: 0x04004C06 RID: 19462
	private bool m_collectionManagerPrefabLoaded;

	// Token: 0x04004C07 RID: 19463
	private bool m_isEditingDeck;

	// Token: 0x04004C08 RID: 19464
	private bool m_isTransitioningToCollection;

	// Token: 0x04004C09 RID: 19465
	private GameObject m_displayRoot;

	// Token: 0x04004C0A RID: 19466
	private GameObject m_collectionManager;

	// Token: 0x04004C0B RID: 19467
	private PvPDungeonRunDisplay m_display;

	// Token: 0x04004C0C RID: 19468
	private GameObject m_popupManagerRoot;

	// Token: 0x04004C0D RID: 19469
	private DuelsPopupManager m_PopupManager;

	// Token: 0x04004C0E RID: 19470
	private DungeonCrawlServices m_services;

	// Token: 0x04004C0F RID: 19471
	private AdventureDbId m_currentAdventure;

	// Token: 0x04004C10 RID: 19472
	private AdventureDungeonCrawlDisplay m_dungeonCrawlDisplay;

	// Token: 0x04004C11 RID: 19473
	private GuestHeroPickerTrayDisplay m_guestHeroPickerTrayDisplay;

	// Token: 0x04004C12 RID: 19474
	private AssetLoadingHelper m_assetLoadingHelper;

	// Token: 0x04004C13 RID: 19475
	private bool m_hasSession;

	// Token: 0x04004C14 RID: 19476
	private bool m_hasLatestSessionData;

	// Token: 0x04004C15 RID: 19477
	private bool m_hasStatsInfo;

	// Token: 0x04004C16 RID: 19478
	private int m_seasonId;

	// Token: 0x04004C17 RID: 19479
	private AdventureDefCache m_adventureDefCache;

	// Token: 0x04004C18 RID: 19480
	private AdventureWingDefCache m_adventureWingDefCache;

	// Token: 0x04004C19 RID: 19481
	private Vector3 CM_POS = new Vector3(55.5f, -15.5f, -80.9f);

	// Token: 0x04004C1A RID: 19482
	private Vector3 CM_SCALE = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x04004C1B RID: 19483
	private static PvPDungeonRunScene m_instance;
}
