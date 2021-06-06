using System;
using System.Collections;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class PvPDungeonRunScene : PegasusScene
{
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_screenPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_CollectionManagerPrefab;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_PopupManagerPrefab;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionStartingOffset = 100f;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionTime = 1f;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_rootDropHeight = 10f;

	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideOutSound;

	private bool m_unloading;

	private bool m_screenPrefabLoaded;

	private bool m_gameSaveDataReceived;

	private bool m_collectionManagerPrefabLoaded;

	private bool m_isEditingDeck;

	private bool m_isTransitioningToCollection;

	private GameObject m_displayRoot;

	private GameObject m_collectionManager;

	private PvPDungeonRunDisplay m_display;

	private GameObject m_popupManagerRoot;

	private DuelsPopupManager m_PopupManager;

	private DungeonCrawlServices m_services;

	private AdventureDbId m_currentAdventure;

	private AdventureDungeonCrawlDisplay m_dungeonCrawlDisplay;

	private GuestHeroPickerTrayDisplay m_guestHeroPickerTrayDisplay;

	private AssetLoadingHelper m_assetLoadingHelper;

	private bool m_hasSession;

	private bool m_hasLatestSessionData;

	private bool m_hasStatsInfo;

	private int m_seasonId;

	private AdventureDefCache m_adventureDefCache;

	private AdventureWingDefCache m_adventureWingDefCache;

	private Vector3 CM_POS = new Vector3(55.5f, -15.5f, -80.9f);

	private Vector3 CM_SCALE = new Vector3(1.05f, 1.05f, 1.05f);

	private static PvPDungeonRunScene m_instance;

	public static PvPDungeonRunScene Get()
	{
		return m_instance;
	}

	public static bool IsEditingDeck()
	{
		if (m_instance != null && m_instance.m_isEditingDeck)
		{
			return !m_instance.m_isTransitioningToCollection;
		}
		return false;
	}

	public void Start()
	{
		m_instance = this;
		AssetLoader.Get().InstantiatePrefab((string)m_CollectionManagerPrefab, OnCollectionManagerLoaded);
		AssetLoader.Get().InstantiatePrefab((string)m_PopupManagerPrefab, OnPopupManagerLoaded);
		Network.Get().RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, OnPVPDRStatsResponse);
		Network.Get().RequestPVPDRStatsInfo();
		Network.Get().RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
		Network.Get().SendPVPDRSessionInfoRequest();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Duels);
		m_adventureDefCache = new AdventureDefCache(preloadRecords: false);
		m_adventureWingDefCache = new AdventureWingDefCache(preloadRecords: false);
		StartCoroutine(NotifySceneLoadedWhenReady());
	}

	public void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public void OnDestroy()
	{
		m_instance = null;
	}

	public GameSaveKeyId GetGSDKeyForAdventure()
	{
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(m_currentAdventure, AdventureModeDbId.DUNGEON_CRAWL);
		if (adventureDataRecord != null)
		{
			return (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		}
		Debug.LogError("PvPDungeonRunScene.GetGSDKeyForAdventure called but could not find record for adventureId = " + m_currentAdventure);
		return (GameSaveKeyId)0;
	}

	public int GetSeasonID()
	{
		return m_seasonId;
	}

	private void OnCollectionManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_collectionManagerPrefabLoaded = true;
		if (go == null)
		{
			Debug.LogError($"PvPDungeonRunScene.OnCollectionManagerLoaded() - failed to load screen {assetRef}");
			return;
		}
		m_collectionManager = go;
		m_collectionManager.SetActive(value: true);
		m_collectionManager.transform.SetParent(base.transform, worldPositionStays: false);
		m_collectionManager.transform.localPosition = CM_POS;
		m_collectionManager.transform.localScale = CM_SCALE;
	}

	public void OnGuestHeroSelected(TAG_CLASS classId, GuestHeroDbfRecord record)
	{
		m_services.DungeonCrawlData.SelectedHeroClass = classId;
		TransitionToDungeonCrawlPlayMat();
	}

	public bool TransitionToGuestHeroPicker()
	{
		bool num = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", OnGuestHeroPickerLoaded);
		if (!num)
		{
			Debug.LogError("PvPDungeonRunDisplay could not load the GuestHeroPicker prefab.");
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DUELS_HEADLINE"),
				m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE"),
				m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK
			};
			DialogManager.Get().ShowPopup(info);
		}
		return num;
	}

	public void TransitionToDungeonCrawlPlayMat()
	{
		if (!(m_dungeonCrawlDisplay == null))
		{
			return;
		}
		DungeonCrawlUtil.LoadDungeonRunPrefab(delegate(GameObject go)
		{
			AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
			if ((bool)component)
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_IDLE);
				m_dungeonCrawlDisplay = component;
				GameUtils.SetParent(go, base.transform);
				component.StartRun(m_services);
			}
		});
	}

	public bool TransitionFromDungeonCrawlPlayMat()
	{
		m_displayRoot.SetActive(value: true);
		Vector3 up = Vector3.up;
		up.x -= m_transitionStartingOffset;
		m_displayRoot.transform.localPosition = up;
		Hashtable args = iTween.Hash("islocal", true, "position", Vector3.zero, "time", 1, "easeType", "easeOutBounce", "oncomplete", (Action<object>)delegate
		{
			m_display.EnableButtons();
			if (m_dungeonCrawlDisplay != null)
			{
				if (DuelsConfig.Get().RunRecentlyEnded())
				{
					m_display.CheckForStatsChanged();
				}
				UnityEngine.Object.Destroy(m_dungeonCrawlDisplay.gameObject);
				m_dungeonCrawlDisplay = null;
				Network.Get().SendPVPDRSessionInfoRequest();
			}
		}, "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_displayRoot.gameObject, args);
		if (!string.IsNullOrEmpty(m_SlideInSound))
		{
			SoundManager.Get().LoadAndPlay(m_SlideInSound);
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

	public bool NavigateBackFromPlaymat()
	{
		PVPDRLobbyDataModel pVPDRLobbyDataModel = m_display.GetPVPDRLobbyDataModel();
		bool flag = pVPDRLobbyDataModel.HasSession && !pVPDRLobbyDataModel.IsSessionActive;
		if (!pVPDRLobbyDataModel.HasSession || flag || DuelsConfig.IsInitialLoadoutComplete())
		{
			if (!TransitionFromDungeonCrawlPlayMat())
			{
				Navigation.Push(NavigateBackFromPlaymat);
			}
		}
		else if (!TransitionToGuestHeroPicker())
		{
			Navigation.Push(NavigateBackFromPlaymat);
		}
		return true;
	}

	public void TransitionBackFromGuestHeroPicker()
	{
		GuestHeroPickerDisplay.Get().HideTray();
		m_displayRoot.SetActive(value: true);
		m_displayRoot.transform.localPosition = Vector3.up;
		m_display.EnableButtons(enabled: false);
	}

	public void SetAdventureData()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		adventureConfig.SetSelectedAdventureMode(m_services.DungeonCrawlData.GetSelectedAdventure(), AdventureModeDbId.DUNGEON_CRAWL);
		adventureConfig.SetMission(m_services.DungeonCrawlData.GetMission());
	}

	public void ShowDungeonCrawlDisplay(Action<object> action)
	{
		int num = ((!UniversalInputManager.UsePhoneUI) ? 3 : 0);
		Hashtable args = iTween.Hash("islocal", true, "position", new Vector3(0f, num, 0f), "time", 1, "easeType", "easeOutBounce", "oncomplete", action, "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_dungeonCrawlDisplay.gameObject, args);
		m_isEditingDeck = false;
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_IDLE);
	}

	public void HideDungeonCrawlDisplay()
	{
		m_isTransitioningToCollection = true;
		int num = (UniversalInputManager.UsePhoneUI ? (-180) : (-110));
		Hashtable args = iTween.Hash("islocal", true, "position", new Vector3(num, 0f, 0f), "time", 1, "easeType", "easeOutBounce", "oncomplete", (Action<object>)delegate
		{
			m_isTransitioningToCollection = false;
		}, "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_dungeonCrawlDisplay.gameObject, args);
		m_isEditingDeck = true;
	}

	public void OnHeroPickerShown()
	{
		m_display.OnHeroPickerShown();
		m_display.EnableButtons(enabled: false);
		if (m_displayRoot != null)
		{
			m_displayRoot.SetActive(value: false);
		}
		if (m_dungeonCrawlDisplay != null)
		{
			UnityEngine.Object.Destroy(m_dungeonCrawlDisplay.gameObject);
			m_dungeonCrawlDisplay = null;
		}
	}

	public void OnHeroPickerHidden()
	{
		m_display.EnableButtons();
	}

	public AdventureDef GetAdventureDef(AdventureDbId advId)
	{
		return m_adventureDefCache.GetDef(advId);
	}

	public AdventureWingDef GetWingDef(WingDbId wingId)
	{
		return m_adventureWingDefCache.GetDef(wingId);
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		m_unloading = true;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			BnetBar.Get().ToggleActive(active: true);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.Unload();
		}
		if (m_displayRoot != null)
		{
			UnityEngine.Object.Destroy(m_displayRoot);
		}
		if (m_dungeonCrawlDisplay != null)
		{
			UnityEngine.Object.Destroy(m_dungeonCrawlDisplay.gameObject);
		}
		if (m_collectionManager != null)
		{
			UnityEngine.Object.Destroy(m_collectionManager.gameObject);
		}
		if (m_guestHeroPickerTrayDisplay != null)
		{
			UnityEngine.Object.Destroy(m_guestHeroPickerTrayDisplay.gameObject);
		}
		if (m_adventureDefCache != null)
		{
			m_adventureDefCache.Unload();
		}
		if (m_adventureWingDefCache != null)
		{
			m_adventureWingDefCache.Unload();
		}
		if (m_popupManagerRoot != null)
		{
			UnityEngine.Object.Destroy(m_popupManagerRoot.gameObject);
		}
		Network.Get().RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
		m_unloading = false;
	}

	private void DoDungeonRunTransition()
	{
		if ((bool)GuestHeroPickerDisplay.Get())
		{
			m_services.SubsceneController.OnTransitionComplete();
			GuestHeroPickerDisplay.Get().HideTray();
		}
		else if (m_displayRoot != null && m_displayRoot.gameObject.activeInHierarchy)
		{
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x -= m_transitionStartingOffset;
			Hashtable args = iTween.Hash("islocal", true, "position", localPosition, "time", m_transitionTime, "easeType", "easeOutBounce", "oncomplete", (Action<object>)delegate
			{
				m_displayRoot.SetActive(value: false);
				m_services.SubsceneController.OnTransitionComplete();
				if (m_dungeonCrawlDisplay != null)
				{
					m_dungeonCrawlDisplay.EnableBackButton(enabled: true);
				}
			}, "oncompletetarget", base.gameObject);
			if (m_dungeonCrawlDisplay != null)
			{
				m_dungeonCrawlDisplay.EnableBackButton(enabled: false);
			}
			iTween.MoveTo(m_displayRoot.gameObject, args);
			if (!string.IsNullOrEmpty(m_SlideOutSound))
			{
				SoundManager.Get().LoadAndPlay(m_SlideOutSound);
			}
		}
		else
		{
			m_services.SubsceneController.OnTransitionComplete();
		}
	}

	private void CreateServices(AdventureDbId adventureId)
	{
		m_assetLoadingHelper = new AssetLoadingHelper();
		m_assetLoadingHelper.AssetLoadingComplete += OnAssetLoadingComplete;
		m_services = DungeonCrawlUtil.CreatePvPDungeonCrawlServices(adventureId, m_assetLoadingHelper);
	}

	private void OnAssetLoadingComplete(object sender, EventArgs args)
	{
		if (m_services != null && m_dungeonCrawlDisplay != null)
		{
			DoDungeonRunTransition();
		}
	}

	private void OnGameSaveDataReceived(bool success)
	{
		m_gameSaveDataReceived = true;
	}

	private void OnGuestHeroPickerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.SetParent(base.transform);
		m_guestHeroPickerTrayDisplay = go.GetComponentInChildren<GuestHeroPickerTrayDisplay>();
	}

	private void OnPVPDRStatsResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRStatsInfoResponse.PacketID.ID, OnPVPDRStatsResponse);
		m_hasStatsInfo = true;
	}

	private void OnPVPDRSessionInfoResponse()
	{
		PVPDRSessionInfoResponse pVPDRSessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		m_hasSession = pVPDRSessionInfoResponse.HasSession;
		m_hasLatestSessionData = true;
		if (m_services != null)
		{
			return;
		}
		m_currentAdventure = AdventureDbId.INVALID;
		if (pVPDRSessionInfoResponse.HasCurrentSeason)
		{
			m_seasonId = pVPDRSessionInfoResponse.CurrentSeason.Season.GameContentSeason.SeasonId;
			PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(m_seasonId);
			if (record != null)
			{
				m_currentAdventure = (AdventureDbId)record.AdventureId;
				AdventureDbfRecord record2 = GameDbf.Adventure.GetRecord((int)m_currentAdventure);
				m_adventureDefCache.LoadDefForId(m_currentAdventure);
				m_adventureWingDefCache.LoadDefForId((WingDbId)record2.Wings[0].ID);
			}
		}
		CreateServices(m_currentAdventure);
		SetAdventureData();
	}

	private void OnScreenPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"PvPDungeonRunScene.OnScreenLoaded() - failed to load screen {assetRef}");
			return;
		}
		m_displayRoot = go;
		m_displayRoot.transform.SetParent(base.transform);
		m_screenPrefabLoaded = true;
	}

	private IEnumerator NotifySceneLoadedWhenReady()
	{
		while (!m_hasStatsInfo)
		{
			yield return null;
		}
		while (!m_hasLatestSessionData)
		{
			yield return null;
		}
		GameSaveDataManager.Get().Request(GetGSDKeyForAdventure(), OnGameSaveDataReceived);
		while (!m_gameSaveDataReceived)
		{
			yield return null;
		}
		AssetLoader.Get().InstantiatePrefab((string)m_screenPrefab, OnScreenPrefabLoaded);
		while (!m_screenPrefabLoaded)
		{
			yield return null;
		}
		while (m_display == null)
		{
			m_display = m_displayRoot.GetComponentInChildren<PvPDungeonRunDisplay>();
			yield return null;
		}
		while (!m_display.IsFinishedLoading)
		{
			yield return null;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_IDLE);
		bool recentLoss = DuelsConfig.Get().HasRecentLoss();
		bool recentWin = DuelsConfig.Get().HasRecentWin();
		bool flag = SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY;
		m_displayRoot.SetActive(!flag);
		if (flag)
		{
			m_display.GetPVPDRLobbyDataModel().RecentLoss = recentLoss;
			m_display.GetPVPDRLobbyDataModel().RecentWin = recentWin;
			TransitionToDungeonCrawlPlayMat();
			while (m_dungeonCrawlDisplay == null)
			{
				yield return null;
			}
		}
		while (!m_collectionManagerPrefabLoaded)
		{
			yield return null;
		}
		CollectionManagerDisplay obj = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		obj.EnableInput(enable: false);
		obj.PopulateSetFilters(shouldReset: true);
		SceneMgr.Get().NotifySceneLoaded();
	}

	public DuelsPopupManager GetPopupManager()
	{
		return m_PopupManager;
	}

	private void OnPopupManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"PvPDungeonRunScene.OnPopupManagerLoaded() - failed to load screen {assetRef}");
			return;
		}
		m_popupManagerRoot = go;
		m_popupManagerRoot.transform.SetParent(base.gameObject.transform);
		m_PopupManager = go.GetComponentInChildren<DuelsPopupManager>();
	}

	public static void ShowDuelsMessagePopup(string header, string message, string rating, Action callback)
	{
		DuelsPopupManager popupManager = m_instance.GetPopupManager();
		if (popupManager != null)
		{
			popupManager.ShowNotice(header, message, rating, callback);
		}
	}
}
