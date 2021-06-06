using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bgs;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusClient;
using PegasusFSG;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020002EA RID: 746
[CustomEditClass]
public class FiresideGatheringManager : IService, IHasUpdate, IHasFixedUpdate
{
	// Token: 0x14000020 RID: 32
	// (add) Token: 0x06002716 RID: 10006 RVA: 0x000C3960 File Offset: 0x000C1B60
	// (remove) Token: 0x06002717 RID: 10007 RVA: 0x000C3994 File Offset: 0x000C1B94
	public static event FiresideGatheringManager.OnPatronListUpdatedCallback OnPatronListUpdated;

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x06002718 RID: 10008 RVA: 0x000C39C7 File Offset: 0x000C1BC7
	// (set) Token: 0x06002719 RID: 10009 RVA: 0x000C39CF File Offset: 0x000C1BCF
	public FiresideGatheringManager.FiresideGatheringMode CurrentFiresideGatheringMode { get; set; }

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x0600271A RID: 10010 RVA: 0x000C39D8 File Offset: 0x000C1BD8
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("FiresideGatheringManagerSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x0600271B RID: 10011 RVA: 0x000C3A11 File Offset: 0x000C1C11
	// (set) Token: 0x0600271C RID: 10012 RVA: 0x000C3A19 File Offset: 0x000C1C19
	private FiresideGatheringManagerData Data { get; set; }

	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x0600271D RID: 10013 RVA: 0x000C3A22 File Offset: 0x000C1C22
	// (set) Token: 0x0600271E RID: 10014 RVA: 0x000C3A2F File Offset: 0x000C1C2F
	public bool HasSeenReturnToFSGSceneTooltip
	{
		get
		{
			return this.Data.m_hasSeenReturnToFSGSceneTooltip;
		}
		set
		{
			this.Data.m_hasSeenReturnToFSGSceneTooltip = value;
		}
	}

	// Token: 0x0600271F RID: 10015 RVA: 0x000C3A3D File Offset: 0x000C1C3D
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadResource loadData = new LoadResource("ServiceData/FiresideGatheringManagerData", LoadResourceFlags.FailOnError);
		yield return loadData;
		this.Data = (loadData.LoadedAsset as FiresideGatheringManagerData);
		BnetPresenceMgr.Get();
		HearthstoneApplication.Get().WillReset += this.WillReset;
		serviceLocator.Get<SceneMgr>().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.SceneMgr_OnScenePreUnload));
		Action action = delegate()
		{
			ChatMgr.Get().OnFriendListToggled += this.OnFriendListClosed_CloseTooltip;
		};
		if (ChatMgr.Get() == null)
		{
			ChatMgr.OnStarted += action;
		}
		else
		{
			action();
		}
		Action action2 = delegate()
		{
			DialogManager.Get().OnDialogShown += this.CloseTooltip;
		};
		if (DialogManager.Get() == null)
		{
			DialogManager.OnStarted += action2;
		}
		else
		{
			action2();
		}
		Network network = serviceLocator.Get<Network>();
		network.RegisterNetHandler(RequestNearbyFSGsResponse.PacketID.ID, new Network.NetHandler(this.OnRequestNearbyFSGsResponse), null);
		network.RegisterNetHandler(CheckInToFSGResponse.PacketID.ID, new Network.NetHandler(this.OnCheckInToFSGResponse), null);
		network.RegisterNetHandler(CheckOutOfFSGResponse.PacketID.ID, new Network.NetHandler(this.OnCheckOutOfFSGResponse), null);
		network.RegisterNetHandler(InnkeeperSetupGatheringResponse.PacketID.ID, new Network.NetHandler(this.OnInnkeeperSetupGatheringResponse), null);
		network.RegisterNetHandler(FSGPatronListUpdate.PacketID.ID, new Network.NetHandler(this.OnPatronListUpdateReceivedFromServer), null);
		FiresideGatheringManager.s_guardianVars.Init();
		FiresideGatheringManager.s_clientOptions.Init();
		FiresideGatheringManager.s_profileProgress.Init();
		FiresideGatheringManager.s_fsgFeaturesConfig.Init();
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), new Action(this.OnNetCache_GuardianVars));
		netCache.RegisterUpdatedListener(typeof(FSGFeatureConfig), new Action(this.OnNetCache_FSGFeatureConfig));
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheProfileProgress), new Action(this.CheckCanBeginLocationDataGatheringForLogin));
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheClientOptions), new Action(this.CheckCanBeginLocationDataGatheringForLogin));
		BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnPlayersPresenceChanged));
		Action action3 = delegate()
		{
			CollectionManager.Get().RegisterDeckDeletedListener(new CollectionManager.DelOnDeckDeleted(this.CollectionManager_DeckDeleted));
		};
		if (CollectionManager.Get() == null)
		{
			CollectionManager.OnCollectionManagerReady += action3.Invoke;
		}
		else
		{
			action3();
		}
		this.CheckCanBeginLocationDataGatheringForLogin();
		yield break;
	}

	// Token: 0x06002720 RID: 10016 RVA: 0x000C3A53 File Offset: 0x000C1C53
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(FullScreenFXMgr),
			typeof(SceneMgr)
		};
	}

	// Token: 0x06002721 RID: 10017 RVA: 0x000C3A8F File Offset: 0x000C1C8F
	public void Shutdown()
	{
		HearthstoneApplication.Get().WillReset -= this.WillReset;
		if (this.IsCheckedIn)
		{
			global::Log.FiresideGatherings.Print("OnApplicationQuit: calling check out.", Array.Empty<object>());
			this.CheckOutOfFSG(false);
		}
	}

	// Token: 0x06002722 RID: 10018 RVA: 0x000C3ACC File Offset: 0x000C1CCC
	public void Update()
	{
		if (this.m_waitingForCheckIn)
		{
			if (this.IsCheckedIn)
			{
				this.TransitionToFSGSceneIfSafe();
				if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING || SceneMgr.Get().GetNextMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
				{
					this.m_waitingForCheckIn = false;
					return;
				}
			}
			else if (!this.CanAutoCheckInEventually || this.m_nearbyFSGs.Count < 1)
			{
				this.m_waitingForCheckIn = false;
				DialogManager.Get().ShowFiresideGatheringCheckInFailedDialog();
			}
		}
	}

	// Token: 0x06002723 RID: 10019 RVA: 0x000C3B3A File Offset: 0x000C1D3A
	public void FixedUpdate()
	{
		if (!this.m_haltFSGNotificationsAndCheckins && GameUtils.AreAllTutorialsComplete())
		{
			this.AutoInnkeeperSetup();
			this.AutoCheckIn();
			this.NotifyFSGNearbyIfNeeded();
		}
		this.DoStartAndEndTimingEvents();
		this.m_haltFSGNotificationsAndCheckins = false;
	}

	// Token: 0x06002724 RID: 10020 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	public float GetSecondsBetweenUpdates()
	{
		return 1f;
	}

	// Token: 0x06002725 RID: 10021 RVA: 0x000C3B74 File Offset: 0x000C1D74
	private void WillReset()
	{
		if (this.IsCheckedIn)
		{
			this.CheckOutOfFSG(false);
			this.LeaveFSG();
		}
		this.m_nearbyFSGs.Clear();
		this.m_hasBegunLocationDataGatheringForLogin = false;
		this.m_fsgSignShown = false;
		this.m_tooltipShowing = null;
		this.HasSeenReturnToFSGSceneTooltip = false;
		this.m_waitingForCheckIn = false;
		this.m_errorOccuredOnCheckin = false;
		ChatMgr.Get().OnFriendListToggled -= this.ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
	}

	// Token: 0x06002726 RID: 10022 RVA: 0x000C3BE1 File Offset: 0x000C1DE1
	public static FiresideGatheringManager Get()
	{
		return HearthstoneServices.Get<FiresideGatheringManager>();
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x06002727 RID: 10023 RVA: 0x000C3BE8 File Offset: 0x000C1DE8
	public static bool IsFSGFeatureEnabled
	{
		get
		{
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				return false;
			}
			NetCache.NetCacheFeatures value = FiresideGatheringManager.s_guardianVars.Value;
			bool flag = value != null && value.FSGEnabled;
			return GameUtils.AreAllTutorialsComplete() && flag;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x06002728 RID: 10024 RVA: 0x000C3C20 File Offset: 0x000C1E20
	public static bool IsGpsFeatureEnabled
	{
		get
		{
			if (!FiresideGatheringManager.IsFSGFeatureEnabled)
			{
				return false;
			}
			FSGFeatureConfig value = FiresideGatheringManager.s_fsgFeaturesConfig.Value;
			return value == null || value.Gps;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x06002729 RID: 10025 RVA: 0x000C3C4C File Offset: 0x000C1E4C
	public static bool IsWifiFeatureEnabled
	{
		get
		{
			if (!FiresideGatheringManager.IsFSGFeatureEnabled)
			{
				return false;
			}
			FSGFeatureConfig value = FiresideGatheringManager.s_fsgFeaturesConfig.Value;
			return value == null || value.Wifi;
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x0600272A RID: 10026 RVA: 0x000C3C78 File Offset: 0x000C1E78
	public static bool CanRequestNearbyFSG
	{
		get
		{
			return FiresideGatheringManager.IsFSGFeatureEnabled && (FiresideGatheringManager.IsGpsFeatureEnabled || FiresideGatheringManager.IsWifiFeatureEnabled);
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x0600272B RID: 10027 RVA: 0x000C3C94 File Offset: 0x000C1E94
	public bool IsRequestNearbyFSGsPending
	{
		get
		{
			return this.m_isRequestNearbyFSGsPending;
		}
	}

	// Token: 0x0600272C RID: 10028 RVA: 0x000C3C9C File Offset: 0x000C1E9C
	private string GetTavernName_TavernSign(FSGConfig fsg)
	{
		if (string.IsNullOrEmpty(fsg.TavernName))
		{
			return GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_DEFAULT_TAVERN_NAME");
		}
		return fsg.TavernName;
	}

	// Token: 0x0600272D RID: 10029 RVA: 0x000C3CBC File Offset: 0x000C1EBC
	public string GetTavernName_FriendsList(FSGConfig fsg)
	{
		if (!string.IsNullOrEmpty(fsg.TavernName))
		{
			return fsg.TavernName;
		}
		BnetPlayer bnetPlayer;
		if (this.m_innkeepers.TryGetValue(fsg.FsgId, out bnetPlayer) && bnetPlayer.GetBattleTag() != null)
		{
			return GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_FIRST_TIME_TAVERN_NAME", new object[]
			{
				bnetPlayer.GetBattleTag().ToString()
			});
		}
		return GameStrings.Get("GLOBAL_FIRESIDE_GATHERING_DEFAULT_TAVERN_NAME");
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x0600272E RID: 10030 RVA: 0x000C3D29 File Offset: 0x000C1F29
	public bool HasFSGToInnkeeperSetup
	{
		get
		{
			return this.m_innkeeperFSG != null;
		}
	}

	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x0600272F RID: 10031 RVA: 0x000C3D34 File Offset: 0x000C1F34
	public FSGConfig FSGToInnkeeperSetup
	{
		get
		{
			return this.m_innkeeperFSG;
		}
	}

	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x06002730 RID: 10032 RVA: 0x000C3D3C File Offset: 0x000C1F3C
	// (set) Token: 0x06002731 RID: 10033 RVA: 0x000C3D44 File Offset: 0x000C1F44
	public TavernSignData LastSign { get; private set; }

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06002732 RID: 10034 RVA: 0x000C3D50 File Offset: 0x000C1F50
	// (remove) Token: 0x06002733 RID: 10035 RVA: 0x000C3D88 File Offset: 0x000C1F88
	public event FiresideGatheringManager.CheckedInToFSGCallback OnJoinFSG;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06002734 RID: 10036 RVA: 0x000C3DC0 File Offset: 0x000C1FC0
	// (remove) Token: 0x06002735 RID: 10037 RVA: 0x000C3DF8 File Offset: 0x000C1FF8
	public event FiresideGatheringManager.CheckedOutOfFSGCallback OnLeaveFSG;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06002736 RID: 10038 RVA: 0x000C3E30 File Offset: 0x000C2030
	// (remove) Token: 0x06002737 RID: 10039 RVA: 0x000C3E68 File Offset: 0x000C2068
	public event FiresideGatheringManager.RequestNearbyFSGsCallback OnNearbyFSGs;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06002738 RID: 10040 RVA: 0x000C3EA0 File Offset: 0x000C20A0
	// (remove) Token: 0x06002739 RID: 10041 RVA: 0x000C3ED8 File Offset: 0x000C20D8
	public event FiresideGatheringManager.NearbyFSGsChangedCallback OnNearbyFSGsChanged;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x0600273A RID: 10042 RVA: 0x000C3F10 File Offset: 0x000C2110
	// (remove) Token: 0x0600273B RID: 10043 RVA: 0x000C3F48 File Offset: 0x000C2148
	public event FiresideGatheringManager.OnInnkeeperSetupFinishedCallback OnInnkeeperSetupFinished;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x0600273C RID: 10044 RVA: 0x000C3F80 File Offset: 0x000C2180
	// (remove) Token: 0x0600273D RID: 10045 RVA: 0x000C3FB8 File Offset: 0x000C21B8
	public event FiresideGatheringManager.FSGSignClosedCallback OnSignClosed;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x0600273E RID: 10046 RVA: 0x000C3FF0 File Offset: 0x000C21F0
	// (remove) Token: 0x0600273F RID: 10047 RVA: 0x000C4028 File Offset: 0x000C2228
	public event FiresideGatheringManager.FSGSignShownCallback OnSignShown;

	// Token: 0x06002740 RID: 10048 RVA: 0x000C4060 File Offset: 0x000C2260
	public void CheckInToFSG(long fsgId)
	{
		this.m_checkInRequestPending = true;
		this.m_nearbyFSGsFoundEventSent = true;
		if (BnetPresenceMgr.Get().GetMyPlayer().IsAppearingOffline())
		{
			this.PromptPlayerToAppearOnline(fsgId);
			return;
		}
		FSGConfig fsgconfig = this.m_nearbyFSGs.FirstOrDefault((FSGConfig f) => f.FsgId == fsgId);
		string text = (fsgconfig == null) ? "<notfound>" : fsgconfig.TavernName;
		global::Log.FiresideGatherings.Print("CheckInToFSG: sending check in to server for {0}-{1}", new object[]
		{
			fsgId,
			string.IsNullOrEmpty(text) ? "<no name>" : text
		});
		if (this.m_gpsCheatingLocation)
		{
			Network.Get().CheckInToFSG(fsgId, this.m_gpsCheatLatitude, this.m_gpsCheatLongitude, 0.0, FiresideGatheringManager.IsWifiFeatureEnabled ? this.BSSIDS : null);
			return;
		}
		if (this.IsGpsLocationValid)
		{
			Network.Get().CheckInToFSG(fsgId, this.Latitude, this.Longitude, this.GpsAccuracy, FiresideGatheringManager.IsWifiFeatureEnabled ? this.BSSIDS : null);
			return;
		}
		if (FiresideGatheringManager.IsWifiFeatureEnabled)
		{
			Network.Get().CheckInToFSG(fsgId, this.BSSIDS);
			return;
		}
		if (this.m_waitingForCheckIn)
		{
			this.m_waitingForCheckIn = false;
			this.ShowNoGPSOrWifiAlertPopup();
		}
	}

	// Token: 0x06002741 RID: 10049 RVA: 0x000C41B1 File Offset: 0x000C23B1
	public void SetWaitingForCheckIn()
	{
		this.m_waitingForCheckIn = true;
	}

	// Token: 0x06002742 RID: 10050 RVA: 0x000C41BA File Offset: 0x000C23BA
	public void ClearErrorOccuredOnCheckIn()
	{
		this.m_errorOccuredOnCheckin = false;
	}

	// Token: 0x06002743 RID: 10051 RVA: 0x000C41C4 File Offset: 0x000C23C4
	public void BeginLocationDataGatheringForLogin()
	{
		if (this.m_hasBegunLocationDataGatheringForLogin)
		{
			return;
		}
		global::Log.FiresideGatherings.Print("FiresideGatheringManager.BeginLocationDataGathering", Array.Empty<object>());
		if (!FiresideGatheringManager.IsFSGFeatureEnabled)
		{
			global::Log.FiresideGatherings.Print("FiresideGatheringManager.BeginLocationDataGathering FEATURE DISABLED", Array.Empty<object>());
			return;
		}
		if (!this.HasManuallyInitiatedFSGScanBefore.Value)
		{
			return;
		}
		if (!ClientLocationManager.Get().GPSServicesReady)
		{
			Processor.RunCoroutine(this.WaitThenBeginLocationDataGatheringForLogin(), null);
			return;
		}
		bool hasValue = Vars.Key("Location.Latitude").HasValue;
		bool hasValue2 = Vars.Key("Location.Longitude").HasValue;
		bool flag = hasValue && hasValue2;
		string[] array = Vars.Key("Location.BSSID").GetStr(string.Empty).Split(new char[]
		{
			' ',
			',',
			';'
		}, StringSplitOptions.RemoveEmptyEntries);
		bool flag2 = array != null && array.Length != 0;
		bool flag3 = FiresideGatheringManager.IsGpsFeatureEnabled && (flag || ClientLocationManager.Get().GPSAvailable);
		this.m_hasBegunLocationDataGatheringForLogin = true;
		if (flag || flag2)
		{
			ClientLocationData clientLocationData = null;
			ClientLocationData clientLocationData2 = null;
			if (flag3 && flag)
			{
				double @double = Vars.Key("Location.Latitude").GetDouble(0.0);
				double double2 = Vars.Key("Location.Longitude").GetDouble(0.0);
				clientLocationData = new ClientLocationData();
				clientLocationData.location = new GpsCoordinate(@double, double2, 0.0, global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds);
				this.OnLocationDataGPSUpdate(clientLocationData);
			}
			if (FiresideGatheringManager.IsWifiFeatureEnabled && flag2)
			{
				clientLocationData2 = new ClientLocationData();
				clientLocationData2.accessPointSamples = (from bssid in array
				select new AccessPointInfo
				{
					bssid = bssid
				}).ToList<AccessPointInfo>();
				this.OnLocationDataWIFIUpdate(clientLocationData2);
			}
			if (clientLocationData != null || clientLocationData2 != null)
			{
				this.OnLocationDataComplete();
				return;
			}
		}
		if (flag3)
		{
			if (FiresideGatheringManager.IsWifiFeatureEnabled)
			{
				ClientLocationManager.Get().RequestGPSAndWifiData(new Action<ClientLocationData>(this.OnLocationDataGPSUpdate), new Action<ClientLocationData>(this.OnLocationDataWIFIUpdate), new Action(this.OnLocationDataComplete));
				return;
			}
			ClientLocationManager.Get().RequestGPSData(new Action<ClientLocationData>(this.OnLocationDataGPSUpdate), new Action(this.OnLocationDataComplete));
			return;
		}
		else
		{
			if (FiresideGatheringManager.IsWifiFeatureEnabled)
			{
				ClientLocationManager.Get().RequestWifiData(new Action<ClientLocationData>(this.OnLocationDataWIFIUpdate), new Action(this.OnLocationDataComplete));
				return;
			}
			this.RequestNearbyFSGs(true);
			return;
		}
	}

	// Token: 0x06002744 RID: 10052 RVA: 0x000C441D File Offset: 0x000C261D
	private IEnumerator WaitThenBeginLocationDataGatheringForLogin()
	{
		global::Log.FiresideGatherings.Print("FiresideGatheringManager.WaitThenBeginLocationDataGatheringForLogin", Array.Empty<object>());
		yield return new WaitForSeconds(1f);
		this.BeginLocationDataGatheringForLogin();
		yield break;
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x06002745 RID: 10053 RVA: 0x000C442C File Offset: 0x000C262C
	public FSGConfig CurrentFSG
	{
		get
		{
			return this.m_currentFSG;
		}
	}

	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x06002746 RID: 10054 RVA: 0x000C4434 File Offset: 0x000C2634
	public long CurrentFsgId
	{
		get
		{
			if (this.m_currentFSG != null)
			{
				return this.m_currentFSG.FsgId;
			}
			return 0L;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x06002747 RID: 10055 RVA: 0x000C444C File Offset: 0x000C264C
	public bool CurrentFsgIsLargeScale
	{
		get
		{
			return this.m_currentFSG != null && this.m_currentFSG.HasIsLargeScaleFsg && this.m_currentFSG.IsLargeScaleFsg;
		}
	}

	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x06002748 RID: 10056 RVA: 0x000C4470 File Offset: 0x000C2670
	public byte[] CurrentFsgSharedSecretKey
	{
		get
		{
			return this.m_currentFSGSharedSecretKey;
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x06002749 RID: 10057 RVA: 0x000C4478 File Offset: 0x000C2678
	public List<GameContentScenario> CurrentFsgBrawls
	{
		get
		{
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
			if (mission == null)
			{
				return new List<GameContentScenario>();
			}
			bool useFallbackBrawls = this.m_innkeeperSelectedBrawlLibraryItemIds.Count == 0;
			return mission.BrawlList.Where(delegate(GameContentScenario scen)
			{
				if (scen.IsRequired)
				{
					return true;
				}
				if (useFallbackBrawls)
				{
					if (scen.IsFallback)
					{
						return true;
					}
				}
				else if (this.m_innkeeperSelectedBrawlLibraryItemIds.Contains(scen.LibraryItemId))
				{
					return true;
				}
				return false;
			}).ToList<GameContentScenario>();
		}
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x000C44D8 File Offset: 0x000C26D8
	public void CheckOutOfFSG(bool optOut = false)
	{
		if (!this.IsCheckedIn)
		{
			return;
		}
		if (optOut)
		{
			this.PlayerAccountShouldAutoCheckin.Set(false);
		}
		FSGConfig currentFSG = this.m_currentFSG;
		this.BackOutOfFSGScene();
		Network.Get().CheckOutOfFSG(currentFSG.FsgId);
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x000C451C File Offset: 0x000C271C
	private void BackOutOfFSGScene()
	{
		if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode != FiresideGatheringManager.FiresideGatheringMode.NONE && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			Navigation.Clear();
			if (!HearthstoneApplication.Get().IsResetting())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			}
		}
		this.CurrentFiresideGatheringMode = FiresideGatheringManager.FiresideGatheringMode.NONE;
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x000C4574 File Offset: 0x000C2774
	private void CollectionManager_DeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck.Type == DeckType.FSG_BRAWL_DECK)
		{
			this.UpdateDeckValidity();
		}
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x000C4588 File Offset: 0x000C2788
	public void UpdateDeckValidity()
	{
		if (!this.IsCheckedIn)
		{
			BnetPresenceMgr.Get().SetDeckValidity(null);
			return;
		}
		DeckValidity deckValidity = BnetPresenceMgr.Get().GetMyPlayer().GetHearthstoneGameAccount().GetDeckValidity();
		if (deckValidity == null)
		{
			deckValidity = new DeckValidity();
		}
		foreach (object obj in Enum.GetValues(typeof(FormatType)))
		{
			FormatType formatType = (FormatType)obj;
			if (formatType != FormatType.FT_UNKNOWN && formatType != FormatType.FT_WILD)
			{
				deckValidity.ValidFormatDecks.Add(new FormatDeckValidity
				{
					FormatType = formatType,
					ValidDeck = CollectionManager.Get().AccountHasValidDeck(formatType)
				});
			}
		}
		deckValidity.ValidFormatDecks.Add(new FormatDeckValidity
		{
			FormatType = FormatType.FT_WILD,
			ValidDeck = (CollectionManager.Get().AccountHasValidDeck(FormatType.FT_STANDARD) || CollectionManager.Get().AccountHasValidDeck(FormatType.FT_WILD))
		});
		deckValidity.ValidTavernBrawlDeck = this.GenerateBrawlDeckValidity(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		deckValidity.ValidFiresideBrawlDeck = this.GenerateBrawlDeckValidity(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
		BnetPresenceMgr.Get().SetDeckValidity(deckValidity);
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x000C46A4 File Offset: 0x000C28A4
	private List<BrawlDeckValidity> GenerateBrawlDeckValidity(BrawlType brawlType)
	{
		List<BrawlDeckValidity> list = new List<BrawlDeckValidity>();
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(brawlType))
		{
			return list;
		}
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
		if (mission == null)
		{
			return list;
		}
		int seasonId = mission.seasonId;
		foreach (GameContentScenario gameContentScenario in mission.BrawlList)
		{
			bool validDeck = !mission.CanCreateDeck(gameContentScenario.LibraryItemId) || TavernBrawlManager.Get().HasValidDeck(brawlType, gameContentScenario.LibraryItemId);
			list.Add(new BrawlDeckValidity
			{
				SeasonId = seasonId,
				BrawlLibraryItemId = gameContentScenario.LibraryItemId,
				ValidDeck = validDeck
			});
		}
		return list;
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x000C4768 File Offset: 0x000C2968
	public bool OpponentHasValidDeckForSelectedPlaymode(BnetPlayer opponent)
	{
		DeckValidity deckValidity = opponent.GetHearthstoneGameAccount().GetDeckValidity();
		switch (this.CurrentFiresideGatheringMode)
		{
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE:
			return deckValidity.ValidFormatDecks.Exists((FormatDeckValidity x) => x.ValidDeck && x.FormatType == this.m_FormatType.Value);
		case FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL:
		{
			List<BrawlDeckValidity> brawlDeckValidity = (deckValidity == null) ? null : deckValidity.ValidTavernBrawlDeck;
			return this.OpponentHasValidTavernBrawlDeck(BrawlType.BRAWL_TYPE_TAVERN_BRAWL, brawlDeckValidity);
		}
		case FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL:
		{
			List<BrawlDeckValidity> brawlDeckValidity2 = (deckValidity == null) ? null : deckValidity.ValidFiresideBrawlDeck;
			return this.OpponentHasValidTavernBrawlDeck(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING, brawlDeckValidity2);
		}
		default:
			return false;
		}
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x000C47E4 File Offset: 0x000C29E4
	private bool OpponentHasValidTavernBrawlDeck(BrawlType brawlType, List<BrawlDeckValidity> brawlDeckValidity)
	{
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(brawlType);
		if (mission == null)
		{
			return false;
		}
		if (!mission.CanCreateDeck(mission.SelectedBrawlLibraryItemId))
		{
			return true;
		}
		if (brawlDeckValidity == null)
		{
			return false;
		}
		BrawlDeckValidity brawlDeckValidity2 = brawlDeckValidity.FirstOrDefault((BrawlDeckValidity brawlInfo) => brawlInfo.SeasonId == mission.seasonId && brawlInfo.BrawlLibraryItemId == mission.SelectedBrawlLibraryItemId);
		return brawlDeckValidity2 != null && brawlDeckValidity2.ValidDeck;
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x000C4850 File Offset: 0x000C2A50
	private void JoinFSG(long fsgID, List<FSGPatron> patrons, byte[] sharedSecretKey, List<int> innkeeperSelectedBrawlLibraryItemIds)
	{
		this.m_currentFSG = null;
		this.m_currentFSGSharedSecretKey = null;
		this.m_innkeeperSelectedBrawlLibraryItemIds.Clear();
		foreach (FSGConfig fsgconfig in this.m_nearbyFSGs)
		{
			if (fsgconfig.FsgId == fsgID)
			{
				this.m_currentFSG = fsgconfig;
				this.m_currentFSGSharedSecretKey = sharedSecretKey;
				this.m_innkeeperSelectedBrawlLibraryItemIds = new HashSet<int>(innkeeperSelectedBrawlLibraryItemIds);
				break;
			}
		}
		if (this.m_currentFSG == null)
		{
			global::Log.FiresideGatherings.PrintError("FiresideGatheringManager.OnCheckInToGatheringResponse: Error: Didn't have a corresponding FSG for checkin", Array.Empty<object>());
			this.m_errorOccuredOnCheckin = true;
			return;
		}
		this.LastTavernID.Set(this.m_currentFSG.FsgId);
		this.m_pendingPatrons.Clear();
		this.m_displayablePatrons.Clear();
		this.m_knownPatronsFromServer.Clear();
		this.m_isAppendingPatronList = true;
		this.RebuildKnownPatronsFromPresence();
		if (!this.CurrentFsgIsLargeScale && patrons != null)
		{
			foreach (FSGPatron patron in patrons)
			{
				this.AddKnownPatron(patron, false);
			}
			FiresideGatheringPresenceManager.Get().AddRemovePatronSubscriptions(patrons, null);
		}
		this.PlayerAccountShouldAutoCheckin.Set(true);
		this.m_isAppendingPatronList = false;
		this.UpdateMyPresence();
		Processor.ScheduleCallback((float)FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS, true, new Processor.ScheduledCallback(this.PeriodicCheckForMoreSubscribeOpportunities), null);
		this.TransitionToFSGSceneIfSafe();
		if (this.OnJoinFSG != null)
		{
			this.OnJoinFSG(this.m_currentFSG);
		}
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x000C49EC File Offset: 0x000C2BEC
	private void OnSceneLoadedDuringAutoCheckin(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoadedDuringAutoCheckin));
		this.TransitionToFSGSceneIfSafe();
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x000C4A0C File Offset: 0x000C2C0C
	private void PromptPlayerToAppearOnline(long fsgId)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
		popupInfo.m_text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_APPEAR_ONLINE_PROMPT");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
		popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnAppearOnlineResponse);
		popupInfo.m_responseUserData = fsgId;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x000C4A98 File Offset: 0x000C2C98
	private void OnAppearOnlineResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			BnetPresenceMgr.Get().SetAccountField(12U, false);
			long fsgId = (long)userData;
			this.CheckInToFSG(fsgId);
			return;
		}
		this.m_waitingForCheckIn = false;
		this.BackOutOfFSGScene();
		this.LeaveFSG();
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x000C4ADC File Offset: 0x000C2CDC
	private void LeaveFSG()
	{
		FSGConfig currentFSG = this.m_currentFSG;
		this.m_currentFSG = null;
		this.m_currentFSGSharedSecretKey = null;
		this.m_innkeeperSelectedBrawlLibraryItemIds.Clear();
		this.m_nearbyFSGsFoundEventSent = true;
		this.HideFSGSign(this.m_fsgSignShown);
		this.m_fsgSignShown = false;
		this.HasSeenReturnToFSGSceneTooltip = false;
		this.m_pendingPatrons.Clear();
		this.m_displayablePatrons.Clear();
		this.m_knownPatronsFromServer.Clear();
		this.m_knownPatronsFromPresence.Clear();
		FiresideGatheringPresenceManager.Get().ClearSubscribedPatrons();
		this.PlayerAccountShouldAutoCheckin.Set(false);
		this.UpdateMyPresence();
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.PeriodicCheckForMoreSubscribeOpportunities), null);
		if (this.OnLeaveFSG != null)
		{
			this.OnLeaveFSG(currentFSG);
		}
	}

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x06002756 RID: 10070 RVA: 0x000C4B99 File Offset: 0x000C2D99
	[CustomEditField(Hide = true)]
	public bool IsCheckedIn
	{
		get
		{
			return this.m_currentFSG != null;
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x06002757 RID: 10071 RVA: 0x000C4BA4 File Offset: 0x000C2DA4
	public bool IsPrerelease
	{
		get
		{
			if (this.m_currentFSG == null)
			{
				return false;
			}
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
			return mission != null && mission.IsPrerelease;
		}
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x000C4BD2 File Offset: 0x000C2DD2
	public bool IsCheckedInToFSG(long gatheringID)
	{
		return this.m_currentFSG != null && this.m_currentFSG.FsgId == gatheringID;
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x000C4BEC File Offset: 0x000C2DEC
	public bool IsPlayerInMyFSG(BnetPlayer player)
	{
		if (player == null)
		{
			return false;
		}
		if (this.IsPlayerInMyFSGAndDisplayable(player))
		{
			return true;
		}
		foreach (BnetPlayer bnetPlayer in this.m_pendingPatrons)
		{
			BnetAccountId accountId = bnetPlayer.GetAccountId();
			BnetAccountId accountId2 = player.GetAccountId();
			if (accountId != null && accountId2 != null && accountId.GetLo() == accountId2.GetLo())
			{
				return true;
			}
			BnetGameAccountId bestGameAccountId = bnetPlayer.GetBestGameAccountId();
			BnetGameAccountId bestGameAccountId2 = player.GetBestGameAccountId();
			if (bestGameAccountId != null && bestGameAccountId2 != null && bestGameAccountId.GetLo() == bestGameAccountId2.GetLo())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x000C4CB8 File Offset: 0x000C2EB8
	public bool IsPlayerInMyFSGAndDisplayable(BnetPlayer player)
	{
		if (player == null || this.CurrentFsgIsLargeScale)
		{
			return false;
		}
		foreach (BnetPlayer bnetPlayer in this.m_displayablePatrons)
		{
			BnetAccountId accountId = bnetPlayer.GetAccountId();
			BnetAccountId accountId2 = player.GetAccountId();
			if (accountId != null && accountId2 != null && accountId.GetLo() == accountId2.GetLo())
			{
				return true;
			}
			BnetGameAccountId bestGameAccountId = bnetPlayer.GetBestGameAccountId();
			BnetGameAccountId bestGameAccountId2 = player.GetBestGameAccountId();
			if (bestGameAccountId != null && bestGameAccountId2 != null && bestGameAccountId.GetLo() == bestGameAccountId2.GetLo())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x000C4D84 File Offset: 0x000C2F84
	public List<FSGConfig> GetFSGs()
	{
		return this.m_nearbyFSGs;
	}

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x0600275C RID: 10076 RVA: 0x000C4D8C File Offset: 0x000C2F8C
	public List<BnetPlayer> DisplayablePatronList
	{
		get
		{
			if (!this.IsCheckedIn || this.CurrentFsgIsLargeScale)
			{
				return new List<BnetPlayer>();
			}
			return this.m_displayablePatrons.ToList<BnetPlayer>();
		}
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x0600275D RID: 10077 RVA: 0x000C4DAF File Offset: 0x000C2FAF
	public int DisplayablePatronCount
	{
		get
		{
			if (!this.IsCheckedIn)
			{
				return 0;
			}
			return this.DisplayablePatronList.Count;
		}
	}

	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x0600275E RID: 10078 RVA: 0x000C4DC8 File Offset: 0x000C2FC8
	public List<BnetPlayer> FullPatronList
	{
		get
		{
			List<BnetPlayer> list = new List<BnetPlayer>();
			if (this.IsCheckedIn && !this.CurrentFsgIsLargeScale)
			{
				list.AddRange(this.m_displayablePatrons);
				list.AddRange(this.m_pendingPatrons);
			}
			return list;
		}
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x000C4E04 File Offset: 0x000C3004
	public int FiresideGatheringSort(FSGConfig fsg1, FSGConfig fsg2)
	{
		if (this.IsCheckedInToFSG(fsg1.FsgId))
		{
			return 1;
		}
		if (this.IsCheckedInToFSG(fsg2.FsgId))
		{
			return -1;
		}
		int num = string.Compare(fsg1.TavernName, fsg2.TavernName);
		if (num != 0)
		{
			return num;
		}
		return (int)(fsg1.FsgId - fsg2.FsgId);
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x000C4E58 File Offset: 0x000C3058
	public int FiresideGatheringPlayerSort(BnetPlayer patron1, BnetPlayer patron2)
	{
		int result = 0;
		bool lhsflag = BnetFriendMgr.Get().IsFriend(patron1);
		bool rhsflag = BnetFriendMgr.Get().IsFriend(patron2);
		if (FriendUtils.FriendFlagSort(patron1, patron2, lhsflag, rhsflag, out result))
		{
			return result;
		}
		return FriendUtils.FriendNameSort(patron1, patron2);
	}

	// Token: 0x06002761 RID: 10081 RVA: 0x000C4E98 File Offset: 0x000C3098
	public bool ShowSignIfNeeded(FiresideGatheringManager.OnCloseSign callback = null)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (!this.IsCheckedIn || this.m_fsgSignShown || SceneMgr.Get().IsTransitioning() || mode != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return false;
		}
		this.m_fsgSignShown = true;
		this.ShowSign(this.m_currentFSG.SignData, this.GetTavernName_TavernSign(this.m_currentFSG), callback);
		return true;
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x000C4EFC File Offset: 0x000C30FC
	public bool ShowSmallSignIfNeeded(Transform smallSignContainer)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (!this.IsCheckedIn || !this.m_fsgSignShown || mode != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return false;
		}
		this.m_fsgSignShown = true;
		this.m_smallSignContainer = smallSignContainer;
		this.ShowSmallSign(this.m_currentFSG.SignData, this.GetTavernName_TavernSign(this.m_currentFSG));
		return true;
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x000C4F57 File Offset: 0x000C3157
	public void GotoFSGLink()
	{
		Application.OpenURL(ExternalUrlService.Get().GetFSGLink());
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x000C4F68 File Offset: 0x000C3168
	public void ShowFindFSGDialog()
	{
		if (this.HasManuallyInitiatedFSGScanBefore.Value)
		{
			ClientLocationManager.Get().RequestGPSData(new Action<ClientLocationData>(this.OnLocationDataGPSUpdate), null);
		}
		DialogManager.Get().ShowFiresideGatheringFindEventDialog(new FiresideGatheringFindEventDialog.ResponseCallback(this.OnFindEventDialogCallBack));
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x000C4FA4 File Offset: 0x000C31A4
	public void OnLocationDataGPSUpdate(ClientLocationData locationData)
	{
		if (this.m_locationData == null)
		{
			this.m_locationData = locationData;
		}
		this.m_locationData.location = locationData.location;
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x000C4FC8 File Offset: 0x000C31C8
	public void OnLocationDataWIFIUpdate(ClientLocationData locationData)
	{
		if (this.m_locationData == null)
		{
			this.m_locationData = locationData;
		}
		this.m_locationData.accessPointSamples = locationData.accessPointSamples;
		this.m_accumulatedAccessPoints.Clear();
		foreach (AccessPointInfo accessPointInfo in this.m_locationData.accessPointSamples)
		{
			if (FiresideGatheringManager.IsValidBSSID(accessPointInfo.bssid))
			{
				this.m_accumulatedAccessPoints[accessPointInfo.bssid] = accessPointInfo;
			}
		}
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000C5064 File Offset: 0x000C3264
	public void AddWIFIAccessPoints(ClientLocationData locationData)
	{
		if (this.m_locationData == null)
		{
			this.m_locationData = locationData;
		}
		if (locationData == null)
		{
			return;
		}
		foreach (AccessPointInfo accessPointInfo in locationData.accessPointSamples)
		{
			if (FiresideGatheringManager.IsValidBSSID(accessPointInfo.bssid))
			{
				this.m_accumulatedAccessPoints[accessPointInfo.bssid] = accessPointInfo;
			}
		}
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x000C50E4 File Offset: 0x000C32E4
	public void RequestNearbyFSGs(bool isStateCheck = false)
	{
		if (!FiresideGatheringManager.IsFSGFeatureEnabled)
		{
			global::Log.FiresideGatherings.Print("Not requesting Nearby FSGs because feature is disabled for me.", Array.Empty<object>());
			return;
		}
		global::Log.FiresideGatherings.Print("Requesting Nearby FSGS: gps={0} wifi={1} accuracy={2}", new object[]
		{
			FiresideGatheringManager.IsGpsFeatureEnabled,
			FiresideGatheringManager.IsWifiFeatureEnabled,
			this.GpsAccuracy
		});
		this.m_isRequestNearbyFSGsPending = true;
		if (this.m_gpsCheatingLocation)
		{
			Network.Get().RequestNearbyFSGs(this.m_gpsCheatLatitude, this.m_gpsCheatLongitude, 0.0, FiresideGatheringManager.IsWifiFeatureEnabled ? this.BSSIDS : null);
			return;
		}
		if (isStateCheck)
		{
			Network.Get().RequestNearbyFSGs(null);
			return;
		}
		if (this.IsGpsLocationValid)
		{
			Network.Get().RequestNearbyFSGs(this.Latitude, this.Longitude, this.GpsAccuracy, FiresideGatheringManager.IsWifiFeatureEnabled ? this.BSSIDS : null);
			return;
		}
		if (FiresideGatheringManager.IsWifiFeatureEnabled)
		{
			Network.Get().RequestNearbyFSGs(this.BSSIDS);
		}
	}

	// Token: 0x06002769 RID: 10089 RVA: 0x000C51E4 File Offset: 0x000C33E4
	public void InnkeeperSetupFSG(bool provideWifiForTavern)
	{
		global::Log.FiresideGatherings.Print("Doing Innkeeper FSG Setup", Array.Empty<object>());
		if (this.m_innkeeperFSG == null)
		{
			global::Log.FiresideGatherings.PrintError("FiresideGatheringManager.InnkeeperSetupFSG tried to setup an FSG but no valid FSG exists", Array.Empty<object>());
			return;
		}
		long fsgId = this.m_innkeeperFSG.FsgId;
		if (this.m_gpsCheatingLocation)
		{
			Network.Get().InnkeeperSetupFSG(this.m_gpsCheatLatitude, this.m_gpsCheatLongitude, 0.0, (FiresideGatheringManager.IsWifiFeatureEnabled && provideWifiForTavern) ? this.BSSIDS : null, fsgId);
			return;
		}
		if (this.IsGpsLocationValid)
		{
			Network.Get().InnkeeperSetupFSG(this.Latitude, this.Longitude, this.GpsAccuracy, (FiresideGatheringManager.IsWifiFeatureEnabled && provideWifiForTavern) ? this.BSSIDS : null, fsgId);
			return;
		}
		if (FiresideGatheringManager.IsWifiFeatureEnabled)
		{
			Network.Get().InnkeeperSetupFSG(provideWifiForTavern ? this.BSSIDS : null, fsgId);
		}
	}

	// Token: 0x0600276A RID: 10090 RVA: 0x000C52BF File Offset: 0x000C34BF
	public void RequestFSGNotificationAndCheckinsHalt()
	{
		this.m_haltFSGNotificationsAndCheckins = true;
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x000C52C8 File Offset: 0x000C34C8
	public void ShowFiresideGatheringInnkeeperSetupDialog()
	{
		ChatMgr.Get().CloseChatUI(true);
		string tavernName_TavernSign = this.GetTavernName_TavernSign(this.m_innkeeperFSG);
		DialogManager.Get().ShowFiresideGatheringInnkeeperSetupDialog(new FiresideGatheringInnkeeperSetupDialog.ResponseCallback(this.ShowFiresideGatheringInnkeeperSetup_OnResponse), tavernName_TavernSign);
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x000C5304 File Offset: 0x000C3504
	public void ShowInnkeeperSetupTooltip()
	{
		this.ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_INNKEEPER_TOOLTIP"), new float?(6f));
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x000C5320 File Offset: 0x000C3520
	public bool InBrawlMode()
	{
		return this.CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL || this.CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL;
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x000C5338 File Offset: 0x000C3538
	public static bool IsValidBSSID(string bssid)
	{
		bool flag = false;
		foreach (char c in bssid)
		{
			bool flag2 = c == ':';
			bool flag3 = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
			if (!flag2 && !flag3)
			{
				return false;
			}
			flag = (flag || (!flag2 && c != '0'));
		}
		return flag;
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x000C53B2 File Offset: 0x000C35B2
	public void EnableTransitionInputBlocker(bool enabled)
	{
		if (this.m_transitionInputBlocker == null)
		{
			this.InitializeTransitionInputBlocker(enabled);
			return;
		}
		this.m_transitionInputBlocker.gameObject.SetActive(enabled);
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x000C53DC File Offset: 0x000C35DC
	public void TransitionToFSGSceneIfSafe()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.HUB || mode == SceneMgr.Mode.ADVENTURE || mode == SceneMgr.Mode.DRAFT || mode == SceneMgr.Mode.TAVERN_BRAWL)
		{
			if (!PopupDisplayManager.Get().IsShowing && !StoreManager.Get().IsShownOrWaitingToShow())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				this.EnableTransitionInputBlocker(true);
				return;
			}
		}
		else if (mode == SceneMgr.Mode.LOGIN)
		{
			SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoadedDuringAutoCheckin));
		}
	}

	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x06002771 RID: 10097 RVA: 0x000C5450 File Offset: 0x000C3650
	private double Latitude
	{
		get
		{
			if (this.m_locationData == null || this.m_locationData.location == null)
			{
				return 0.0;
			}
			double num = this.m_locationData.location.Latitude;
			if (this.m_gpsCheatOffset != 0.0)
			{
				num += 57.2957763671875 * (this.m_gpsCheatOffset / 6378137.0);
			}
			return num;
		}
	}

	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x06002772 RID: 10098 RVA: 0x000C54BC File Offset: 0x000C36BC
	private double Longitude
	{
		get
		{
			if (this.m_locationData == null || this.m_locationData.location == null)
			{
				return 0.0;
			}
			return this.m_locationData.location.Longitude;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x06002773 RID: 10099 RVA: 0x000C54ED File Offset: 0x000C36ED
	private double GpsAccuracy
	{
		get
		{
			if (this.m_locationData == null || this.m_locationData.location == null)
			{
				return -1.0;
			}
			return this.m_locationData.location.Accuracy;
		}
	}

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x06002774 RID: 10100 RVA: 0x000C5520 File Offset: 0x000C3720
	public bool IsGpsLocationValid
	{
		get
		{
			if (!FiresideGatheringManager.IsGpsFeatureEnabled)
			{
				return false;
			}
			if (this.m_locationData == null || this.m_locationData.location == null)
			{
				return false;
			}
			FSGFeatureConfig value = FiresideGatheringManager.s_fsgFeaturesConfig.Value;
			return value != null && this.m_locationData.location.Accuracy <= (double)value.MaxAccuracy;
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x06002775 RID: 10101 RVA: 0x000C5577 File Offset: 0x000C3777
	private List<string> BSSIDS
	{
		get
		{
			return (from kv in this.m_accumulatedAccessPoints
			select kv.Key).ToList<string>();
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x06002776 RID: 10102 RVA: 0x000C55A8 File Offset: 0x000C37A8
	[CustomEditField(Hide = true)]
	public bool AutoCheckInEnabled
	{
		get
		{
			NetCache.NetCacheFeatures value = FiresideGatheringManager.s_guardianVars.Value;
			if (value == null || !value.FSGAutoCheckinEnabled)
			{
				return false;
			}
			FSGFeatureConfig value2 = FiresideGatheringManager.s_fsgFeaturesConfig.Value;
			return value2 != null && value2.AutoCheckin;
		}
	}

	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x06002777 RID: 10103 RVA: 0x000C55EC File Offset: 0x000C37EC
	[CustomEditField(Hide = true)]
	public int FriendListPatronCountLimit
	{
		get
		{
			NetCache.NetCacheFeatures value = FiresideGatheringManager.s_guardianVars.Value;
			if (value == null || value.FSGFriendListPatronCountLimit < 0)
			{
				return 30;
			}
			return value.FSGFriendListPatronCountLimit;
		}
	}

	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x06002778 RID: 10104 RVA: 0x000C5619 File Offset: 0x000C3819
	// (set) Token: 0x06002779 RID: 10105 RVA: 0x000C5621 File Offset: 0x000C3821
	public ReactiveBoolOption PlayerAccountShouldAutoCheckin { get; set; } = ReactiveBoolOption.CreateInstance(Option.SHOULD_AUTO_CHECK_IN_TO_FIRESIDE_GATHERINGS);

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x0600277A RID: 10106 RVA: 0x000C562A File Offset: 0x000C382A
	// (set) Token: 0x0600277B RID: 10107 RVA: 0x000C5632 File Offset: 0x000C3832
	public ReactiveBoolOption HasManuallyInitiatedFSGScanBefore { get; set; } = ReactiveBoolOption.CreateInstance(Option.HAS_INITIATED_FIRESIDE_GATHERING_SCAN);

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x0600277C RID: 10108 RVA: 0x000C563B File Offset: 0x000C383B
	// (set) Token: 0x0600277D RID: 10109 RVA: 0x000C5643 File Offset: 0x000C3843
	public ReactiveLongOption LastTavernID { get; set; } = new ReactiveLongOption(Option.LAST_TAVERN_JOINED);

	// Token: 0x0600277E RID: 10110 RVA: 0x000C564C File Offset: 0x000C384C
	private void OnLocationDataComplete()
	{
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			return;
		}
		NetCache.NetCacheFeatures netObject = netCache.GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null && netObject.FSGLoginScanEnabled)
		{
			this.RequestNearbyFSGs(false);
		}
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x000C567C File Offset: 0x000C387C
	private void AutoInnkeeperSetup()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (!this.m_doAutoInnkeeperSetup || this.IsCheckedIn || this.m_innkeeperFSG == null || this.m_innkeeperFSG.IsSetupComplete || mode != SceneMgr.Mode.HUB || SceneMgr.Get().IsTransitioning() || this.m_checkInDialogShown || PopupDisplayManager.Get().IsShowing)
		{
			return;
		}
		this.m_doAutoInnkeeperSetup = false;
		this.m_haltAutoCheckinWhileInnkeeperSetup = true;
		this.ShowFiresideGatheringInnkeeperSetupDialog();
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x06002780 RID: 10112 RVA: 0x000C56F2 File Offset: 0x000C38F2
	private bool CanAutoCheckInEventually
	{
		get
		{
			return this.AutoCheckInEnabled && this.PlayerAccountShouldAutoCheckin.Value && !this.m_errorOccuredOnCheckin && GameUtils.AreAllTutorialsComplete();
		}
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x000C5718 File Offset: 0x000C3918
	private void AutoCheckIn()
	{
		if (this.IsCheckedIn)
		{
			return;
		}
		if (!this.CanAutoCheckInEventually)
		{
			return;
		}
		FSGConfig preferredFSG = this.GetPreferredFSG();
		if (preferredFSG == null)
		{
			return;
		}
		if (this.m_checkInRequestPending || this.m_checkInDialogShown)
		{
			return;
		}
		if (this.m_haltAutoCheckinWhileInnkeeperSetup || (preferredFSG.IsInnkeeper && !preferredFSG.IsSetupComplete))
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.COLLECTIONMANAGER || StoreManager.Get().IsShownOrWaitingToShow() || PopupDisplayManager.Get().IsShowing)
		{
			return;
		}
		if (mode == SceneMgr.Mode.LOGIN || mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.HUB)
		{
			this.CheckInToFSG(preferredFSG.FsgId);
			return;
		}
		DialogManager.Get().ShowFiresideGatheringNearbyDialog(new FiresideGatheringJoinDialog.ResponseCallback(this.OnJoinFSGDialogResponse));
		this.m_checkInDialogShown = true;
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x000C57D0 File Offset: 0x000C39D0
	private FSGConfig GetPreferredFSG()
	{
		if (this.m_nearbyFSGs.Count < 1)
		{
			return null;
		}
		FSGConfig fsgconfig = null;
		for (int i = 1; i < this.m_nearbyFSGs.Count; i++)
		{
			FSGConfig fsgconfig2 = this.m_nearbyFSGs[0];
			if (fsgconfig2.IsInnkeeper && fsgconfig2.IsSetupComplete)
			{
				return fsgconfig2;
			}
			if (fsgconfig2.FsgId == this.LastTavernID.Value)
			{
				fsgconfig = fsgconfig2;
			}
		}
		if (fsgconfig == null)
		{
			return this.m_nearbyFSGs[0];
		}
		return fsgconfig;
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x000C584C File Offset: 0x000C3A4C
	private void NotifyFSGNearbyIfNeeded()
	{
		if (this.IsCheckedIn || this.m_checkInRequestPending || this.AutoCheckInEnabled || this.PlayerAccountShouldAutoCheckin.Value || this.m_nearbyFSGsFoundEventSent || this.m_haltAutoCheckinWhileInnkeeperSetup || !this.m_fsgAvailableToCheckin || this.m_tooltipShowing != null)
		{
			return;
		}
		if (this.m_nearbyFSGs.Count > 0)
		{
			this.NotifyFSGNearby();
		}
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x000C58B9 File Offset: 0x000C3AB9
	private void NotifyFSGNearby()
	{
		this.m_nearbyFSGsFoundEventSent = true;
		this.ShowNearbyFSGsTooltip();
		if (this.OnNearbyFSGs != null)
		{
			this.OnNearbyFSGs();
		}
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x000C58DB File Offset: 0x000C3ADB
	private void ShowNearbyFSGsTooltip()
	{
		this.ShowTooltip(GameStrings.Get("GLUE_FSG_NEARBY_TOOLTIP"), new float?(6f));
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x000C58F8 File Offset: 0x000C3AF8
	private void ShowTooltip(string text, float? durationSeconds = 6f)
	{
		Vector3 vector = BaseUI.Get().m_BnetBar.m_friendButton.transform.position;
		vector += this.Data.m_nearbyFiresidePopupOffset;
		Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, Vector3.zero, this.Data.m_nearbyFiresidePopupScale, text, true, NotificationManager.PopupTextType.BASIC);
		Notification.PopUpArrowDirection direction = UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.LeftUp : Notification.PopUpArrowDirection.LeftDown;
		notification.ShowPopUpArrow(direction);
		notification.PulseReminderEveryXSeconds(2f);
		notification.transform.position = vector;
		notification.transform.localEulerAngles = this.Data.m_nearbyFiresidePopupRotation;
		SceneUtils.SetLayer(notification.gameObject, GameLayer.BattleNet);
		this.m_tooltipShowing = notification;
		if (durationSeconds != null)
		{
			Processor.RunCoroutine(this.Tooltip_End(durationSeconds.Value, notification), null);
		}
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x000C59D6 File Offset: 0x000C3BD6
	private IEnumerator Tooltip_End(float secondsTillDeath, Notification notice)
	{
		if (notice == null)
		{
			yield break;
		}
		if (secondsTillDeath > 0f)
		{
			yield return new WaitForSeconds(secondsTillDeath);
		}
		PegUI.OnReleasePreTrigger -= this.PegUI_OnReleasePreTrigger;
		if (notice != null)
		{
			notice.PlayDeath();
			if (notice == this.m_tooltipShowing)
			{
				this.m_tooltipShowing = null;
			}
		}
		yield break;
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x000C59F3 File Offset: 0x000C3BF3
	public void ShowReturnToFSGSceneTooltip()
	{
		if (Box.Get().IsTransitioningToSceneMode())
		{
			Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished));
			return;
		}
		this.ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished(null);
	}

	// Token: 0x06002789 RID: 10121 RVA: 0x000C5A20 File Offset: 0x000C3C20
	private void ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished(object data)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.ShowReturnToFSGSceneTooltipOnTransitionToBoxFinished));
		if (this.HasSeenReturnToFSGSceneTooltip)
		{
			SocialToastMgr.Get().AddToast(UserAttentionBlocker.NONE, string.Empty, SocialToastMgr.TOAST_TYPE.FIRESIDE_GATHERING_IS_HERE_REMINDER, true);
			return;
		}
		this.HasSeenReturnToFSGSceneTooltip = true;
		this.ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_RETURN_TO_SCENE_HERE"), null);
		ChatMgr.Get().OnFriendListToggled += this.ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
		PegUI.OnReleasePreTrigger += this.PegUI_OnReleasePreTrigger;
	}

	// Token: 0x0600278A RID: 10122 RVA: 0x000C5AA8 File Offset: 0x000C3CA8
	private void ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip(bool open)
	{
		if (!open)
		{
			return;
		}
		ChatMgr.Get().OnFriendListToggled -= this.ShowReturnToFSGSceneTooltip_OnFriendListToggled_ShowNextTooltip;
		this.CloseTooltip();
		if (!this.IsCheckedIn)
		{
			return;
		}
		Action action = delegate()
		{
			FriendListFSGFrame friendListFSGFrame = ChatMgr.Get().FriendListFrame.FindFirstRenderedItem<FriendListFSGFrame>(null);
			if (friendListFSGFrame == null)
			{
				return;
			}
			PegUI.OnReleasePreTrigger += this.PegUI_OnReleasePreTrigger;
			ChatMgr.Get().FriendListFrame.items.Scrolled += this.CloseTooltip;
			this.m_tooltipShowing = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, Vector3.zero, this.Data.m_nearbyFiresidePopupScale, GameStrings.Get("GLUE_FIRESIDE_GATHERING_RETURN_TO_SCENE_HERE"), true, NotificationManager.PopupTextType.BASIC);
			Notification.PopUpArrowDirection direction = Notification.PopUpArrowDirection.Left;
			this.m_tooltipShowing.ShowPopUpArrow(direction);
			this.m_tooltipShowing.PulseReminderEveryXSeconds(2f);
			this.m_tooltipShowing.transform.position = friendListFSGFrame.transform.position + this.Data.m_returnToFsgFriendListPopupOffset;
			this.m_tooltipShowing.transform.localEulerAngles = this.Data.m_nearbyFiresidePopupRotation;
			SceneUtils.SetLayer(this.m_tooltipShowing.gameObject, GameLayer.BattleNet);
			Processor.RunCoroutine(this.Tooltip_End(6f, this.m_tooltipShowing), null);
		};
		if (ChatMgr.Get().FriendListFrame.IsStarted)
		{
			action();
			return;
		}
		ChatMgr.Get().FriendListFrame.OnStarted += action;
	}

	// Token: 0x0600278B RID: 10123 RVA: 0x000C5B14 File Offset: 0x000C3D14
	private void CloseTooltip()
	{
		PegUI.OnReleasePreTrigger -= this.PegUI_OnReleasePreTrigger;
		if (ChatMgr.Get().FriendListFrame != null)
		{
			ChatMgr.Get().FriendListFrame.items.Scrolled -= this.CloseTooltip;
		}
		if (this.m_tooltipShowing != null)
		{
			this.m_tooltipShowing.CloseWithoutAnimation();
		}
	}

	// Token: 0x0600278C RID: 10124 RVA: 0x000C5B7D File Offset: 0x000C3D7D
	private void PegUI_OnReleasePreTrigger(PegUIElement elem)
	{
		this.CloseTooltip();
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x000C5B85 File Offset: 0x000C3D85
	private void OnFriendListClosed_CloseTooltip(bool opened)
	{
		if (opened)
		{
			return;
		}
		this.CloseTooltip();
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x000C5B91 File Offset: 0x000C3D91
	private void SceneMgr_OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return;
		}
		this.CloseTooltip();
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x000C5BA0 File Offset: 0x000C3DA0
	private void DoStartAndEndTimingEvents()
	{
		if (this.m_nearbyFSGs.Count == 0)
		{
			return;
		}
		long unixTimestampSeconds = global::TimeUtils.UnixTimestampSeconds;
		for (int i = this.m_nearbyFSGs.Count - 1; i >= 0; i--)
		{
			FSGConfig fsgconfig = this.m_nearbyFSGs[i];
			if (fsgconfig.UnixEndTimeWithSlush < unixTimestampSeconds)
			{
				this.m_nearbyFSGs.RemoveAt(i);
				if (fsgconfig == this.m_currentFSG)
				{
					this.CheckOutOfFSG(false);
				}
			}
		}
	}

	// Token: 0x06002790 RID: 10128 RVA: 0x000C5C0C File Offset: 0x000C3E0C
	private FiresideGatheringSign GenerateCustomTavernSign(int sign, int background, int major, int minor, string tavernName)
	{
		FiresideGatheringSign signObject = this.GetSignObject(sign);
		Material material = signObject.GetShieldMeshRenderer().GetMaterial();
		signObject.GetComponentInChildren<UberText>().Text = tavernName;
		AssetHandle<Texture> assetHandle = AssetLoader.Get().LoadAsset<Texture>(FiresideGatheringManager.m_backgroundTextures[background - 1], AssetLoadingOptions.None);
		AssetHandle<Texture> assetHandle2 = AssetLoader.Get().LoadAsset<Texture>(FiresideGatheringManager.m_majorTextures[major - 1], AssetLoadingOptions.None);
		AssetHandle<Texture> assetHandle3 = AssetLoader.Get().LoadAsset<Texture>(FiresideGatheringManager.m_minorTextures[minor - 1], AssetLoadingOptions.None);
		material.SetTexture("_BackgroundTex", assetHandle);
		material.SetTexture("_MajorTex", assetHandle2);
		material.SetTexture("_MinorTex", assetHandle3);
		DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(signObject, assetHandle);
		}
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(signObject, assetHandle2);
		}
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(signObject, assetHandle3);
		}
		return signObject;
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x000C5CDA File Offset: 0x000C3EDA
	private void ShowSign(TavernSignData signData, string tavernName, FiresideGatheringManager.OnCloseSign callback)
	{
		if (this.m_currentSign != null)
		{
			this.HideFSGSign(false);
		}
		this.ShowSign(signData, tavernName, callback, new PrefabCallback<GameObject>(this.OnSignAssetLoaded));
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x000C5D06 File Offset: 0x000C3F06
	private void ShowSmallSign(TavernSignData signData, string tavernName)
	{
		this.ShowSign(signData, tavernName, null, new PrefabCallback<GameObject>(this.OnSmallSignAssetLoaded));
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x000C5D20 File Offset: 0x000C3F20
	private void ShowSign(TavernSignData signData, string tavernName, FiresideGatheringManager.OnCloseSign callback, PrefabCallback<GameObject> onSignAssetLoadedCallback)
	{
		this.m_currentSignCallback = callback;
		this.LastSign = signData;
		if (signData.SignType == TavernSignType.TAVERN_SIGN_TYPE_CUSTOM)
		{
			FiresideGatheringSign firesideGatheringSign = this.GenerateCustomTavernSign(signData.Sign, signData.Background, signData.Major, signData.Minor, tavernName);
			onSignAssetLoadedCallback("", firesideGatheringSign.gameObject, null);
			return;
		}
		FiresideGatheringManagerData.SignTypeMapping signTypeMapping = this.Data.m_signTypeMapping.Find((FiresideGatheringManagerData.SignTypeMapping x) => x.m_type == signData.SignType);
		if (signTypeMapping == null || signTypeMapping.m_prefabName == null)
		{
			global::Error.AddDevFatal("FiresideGatheringManager.ShowSign() - unhandled sign type {0}", new object[]
			{
				signData.SignType
			});
			return;
		}
		AssetLoader.Get().InstantiatePrefab(signTypeMapping.m_prefabName, onSignAssetLoadedCallback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x000C5E10 File Offset: 0x000C4010
	private void OnSignAssetLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		FiresideGatheringSign component = go.GetComponent<FiresideGatheringSign>();
		if (component == null)
		{
			return;
		}
		this.m_currentSign = component;
		component.OnDestroyEvent += this.OnSignHidden;
		go.transform.localPosition = this.Data.m_signPosition;
		go.transform.localScale = this.Data.m_signScale;
		SceneUtils.SetLayer(go, GameLayer.IgnoreFullScreenEffects);
		SoundManager.Get().LoadAndPlay("GVG_sign_enter.prefab:68c9d25c4da293b4dba44c37615c0ae0");
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurBrightness(1f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette();
		fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
		this.PlaySignTween(go);
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x000C5ED4 File Offset: 0x000C40D4
	private void OnSmallSignAssetLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		FiresideGatheringSign component = go.GetComponent<FiresideGatheringSign>();
		if (component == null)
		{
			return;
		}
		this.m_currentSign = component;
		if (this.m_smallSignContainer == null)
		{
			UnityEngine.Object.Destroy(component);
		}
		go.transform.localScale = this.Data.m_signScale;
		go.transform.SetParent(this.m_smallSignContainer, false);
		go.transform.localPosition = Vector3.zero;
		SceneUtils.SetLayer(go, GameLayer.Default);
		component.SetSignShadowEnabled(true);
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x000C5F58 File Offset: 0x000C4158
	private FiresideGatheringSign GetSignObject(int signIndex)
	{
		if (signIndex < 1 || signIndex > 8)
		{
			global::Log.FiresideGatherings.PrintError("FiresideGatheringManager.GetSignObject passed an invalid sign index: {0}. Using default of 1", new object[]
			{
				signIndex
			});
			signIndex = 1;
		}
		FiresideGatheringSign component = ((GameObject)GameUtils.InstantiateGameObject(FiresideGatheringManager.m_tavernSignAsset.ToString(), null, false)).GetComponent<FiresideGatheringSign>();
		GameObject gameObject = (GameObject)GameUtils.InstantiateGameObject(FiresideGatheringManager.m_fsgShields[signIndex - 1].ToString(), null, false);
		GameUtils.SetParent(gameObject, component.m_shieldContainer, false);
		component.SetSignShield(gameObject.GetComponentInChildren<FiresideGatheringSignShield>());
		return component;
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x000C5FE0 File Offset: 0x000C41E0
	private void PlaySignTween(GameObject signObject)
	{
		Hashtable hashtable = iTween.Hash(new object[]
		{
			"sign",
			signObject
		});
		Action<object> action = delegate(object e)
		{
			this.PlaySignAnimation(e);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			0.25f,
			"oncomplete",
			action,
			"oncompleteparams",
			hashtable
		});
		iTween.ScaleFrom(signObject, args);
		Processor.RunCoroutine(this.CreateSignInputBlocker(signObject), null);
	}

	// Token: 0x06002798 RID: 10136 RVA: 0x000C6084 File Offset: 0x000C4284
	private void PlaySignAnimation(object args)
	{
		Animator componentInChildren = ((GameObject)((Hashtable)args)["sign"]).GetComponentInChildren<Animator>();
		componentInChildren.enabled = true;
		componentInChildren.Play("FSG_SignSwing");
		if (this.OnSignShown != null)
		{
			this.OnSignShown();
		}
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x000C60C4 File Offset: 0x000C42C4
	private IEnumerator CreateSignInputBlocker(GameObject signObject)
	{
		Camera camera = CameraUtils.FindFirstByLayer(GameLayer.UI);
		GameObject inputBlockerObject = CameraUtils.CreateInputBlocker(camera, "FSGSign");
		inputBlockerObject.transform.parent = signObject.transform;
		inputBlockerObject.transform.localPosition = new Vector3(0f, 1f, 0f);
		PegUIElement fsgSignBlocker = inputBlockerObject.AddComponent<PegUIElement>();
		yield return new WaitForSeconds(2f);
		fsgSignBlocker.AddEventListener(UIEventType.RELEASE, delegate(UIEvent _)
		{
			this.HideFSGSign(false);
			UnityEngine.Object.Destroy(inputBlockerObject);
		});
		yield break;
	}

	// Token: 0x0600279A RID: 10138 RVA: 0x000C60DC File Offset: 0x000C42DC
	private void HideFSGSign(bool hideImmediately = false)
	{
		if (this.m_currentSign == null)
		{
			this.OnSignHidden();
			return;
		}
		this.m_currentSign.gameObject.SetActive(!hideImmediately);
		this.m_currentSign.m_fxMotes.gameObject.SetActive(false);
		if (!hideImmediately)
		{
			SoundManager.Get().LoadAndPlay("GVG_sign_exit.prefab:697b23cceecfd154dacf14bc58b75af2");
		}
		this.HideSignAnim(this.m_currentSign.gameObject);
	}

	// Token: 0x0600279B RID: 10139 RVA: 0x000C6150 File Offset: 0x000C4350
	public void OnTavernSignAnimationComplete()
	{
		if (this.m_currentSign != null)
		{
			this.m_currentSign.UnregisterSignSocketAnimationCompleteListener(new Action(this.OnTavernSignAnimationComplete));
		}
		if (this.OnSignClosed != null)
		{
			this.OnSignClosed();
		}
	}

	// Token: 0x0600279C RID: 10140 RVA: 0x000C618C File Offset: 0x000C438C
	private void HideSignAnim(GameObject sign)
	{
		Animator componentInChildren = sign.GetComponentInChildren<Animator>();
		componentInChildren.enabled = true;
		componentInChildren.Play(UniversalInputManager.UsePhoneUI ? "FSG_SignSocketIn_phone" : "FSG_SignSocketIn");
		SceneUtils.SetLayer(sign, GameLayer.Default);
		this.OnSignHidden();
		sign.GetComponent<FiresideGatheringSign>().RegisterSignSocketAnimationCompleteListener(new Action(this.OnTavernSignAnimationComplete));
	}

	// Token: 0x0600279D RID: 10141 RVA: 0x000C61E7 File Offset: 0x000C43E7
	private void OnSignHidden()
	{
		this.m_currentSign = null;
		if (this.m_currentSignCallback != null)
		{
			this.m_currentSignCallback();
			this.m_currentSignCallback = null;
		}
		this.HideBlur();
	}

	// Token: 0x0600279E RID: 10142 RVA: 0x000C6210 File Offset: 0x000C4410
	private void HideBlur()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
	}

	// Token: 0x0600279F RID: 10143 RVA: 0x000C6234 File Offset: 0x000C4434
	private void OnJoinFSGDialogResponse(bool joinFSG)
	{
		if (!joinFSG)
		{
			this.PlayerAccountShouldAutoCheckin.Set(false);
			return;
		}
		FSGConfig preferredFSG = this.GetPreferredFSG();
		if (preferredFSG == null)
		{
			return;
		}
		this.CheckInToFSG(preferredFSG.FsgId);
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x060027A0 RID: 10144 RVA: 0x000C6282 File Offset: 0x000C4482
	private void OnFindEventDialogCallBack(bool searchForGatherings)
	{
		if (!searchForGatherings)
		{
			this.GotoFSGLink();
			return;
		}
		this.HasManuallyInitiatedFSGScanBefore.Set(true);
		if (!ClientLocationManager.Get().GPSOrWifiServicesAvailable)
		{
			this.ShowNoGPSOrWifiAlertPopup();
			return;
		}
		DialogManager.Get().ShowFiresideGatheringLocationHelperDialog(null);
	}

	// Token: 0x060027A1 RID: 10145 RVA: 0x000C62B8 File Offset: 0x000C44B8
	private void ShowNoGPSOrWifiAlertPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_FIRESIDE_GATHERING");
		popupInfo.m_text = GameStrings.Get("GLUE_FIRESIDE_GATHERING_SCAN_NO_GPS_OR_WIFI");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060027A2 RID: 10146 RVA: 0x000C6304 File Offset: 0x000C4504
	private void OnFailedToFindFSGDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM)
		{
			this.GotoFSGLink();
		}
	}

	// Token: 0x060027A3 RID: 10147 RVA: 0x000C6310 File Offset: 0x000C4510
	private void ShowFiresideGatheringInnkeeperSetup_OnResponse(bool doSetup)
	{
		this.m_haltAutoCheckinWhileInnkeeperSetup = false;
		if (doSetup)
		{
			DialogManager.Get().ShowFiresideGatheringInnkeeperSetupHelperDialog(null);
			return;
		}
		this.PlayerAccountShouldAutoCheckin.Set(false);
	}

	// Token: 0x060027A4 RID: 10148 RVA: 0x000C6334 File Offset: 0x000C4534
	private void OnRequestNearbyFSGsResponse()
	{
		this.m_isRequestNearbyFSGsPending = false;
		RequestNearbyFSGsResponse response = Network.Get().GetRequestNearbyFSGsResponse();
		if (response.ErrorCode != ErrorCode.ERROR_OK)
		{
			global::Log.FiresideGatherings.PrintError("NearbyFSGsResponse: code={0} {1} fsgCount={2}", new object[]
			{
				(int)response.ErrorCode,
				response.ErrorCode,
				response.FSGs.Count
			});
			if (this.OnNearbyFSGsChanged != null)
			{
				this.OnNearbyFSGsChanged();
			}
			return;
		}
		global::Log.FiresideGatherings.Print("NearbyFSGsResponse: code={0} {1} fsgCount={2}", new object[]
		{
			(int)response.ErrorCode,
			response.ErrorCode,
			response.FSGs.Count
		});
		this.m_nearbyFSGs.Clear();
		this.m_innkeeperFSG = null;
		this.m_fsgAvailableToCheckin = false;
		for (int i = 0; i < response.FSGs.Count; i++)
		{
			FSGConfig fsgconfig = response.FSGs[i];
			this.m_nearbyFSGs.Add(fsgconfig);
			if (fsgconfig.IsInnkeeper)
			{
				this.m_innkeeperFSG = fsgconfig;
			}
			else
			{
				this.m_fsgAvailableToCheckin = true;
			}
			this.AddKnownInnkeeper(fsgconfig.FsgId, fsgconfig.FsgInnkeeperAccountId);
		}
		if (response.HasCheckedInFsgId)
		{
			FSGConfig fsgconfig2 = this.m_nearbyFSGs.FirstOrDefault((FSGConfig fsg) => fsg.FsgId == response.CheckedInFsgId);
			if (fsgconfig2 == null)
			{
				global::Log.FiresideGatherings.PrintError("NearbyFSGsResponse: Error: already checked into FSG (id={0}) but no corresponding FSGConfig found in nearby list - ignoring. patronCount={1}", new object[]
				{
					response.CheckedInFsgId,
					response.FsgAttendees.Count
				});
			}
			else
			{
				global::Log.FiresideGatherings.Print("NearbyFSGsResponse: already checked into {0}-{1}, showing FSG UI. patronCount={2}", new object[]
				{
					response.CheckedInFsgId,
					string.IsNullOrEmpty(fsgconfig2.TavernName) ? "<no name>" : fsgconfig2.TavernName,
					response.FsgAttendees.Count
				});
				this.JoinFSG(response.CheckedInFsgId, response.FsgAttendees, response.FsgSharedSecretKey, response.InnkeeperSelectedBrawlLibraryItemId);
			}
		}
		if (this.OnNearbyFSGsChanged != null)
		{
			this.OnNearbyFSGsChanged();
		}
	}

	// Token: 0x060027A5 RID: 10149 RVA: 0x000C65B4 File Offset: 0x000C47B4
	private void OnCheckInToFSGResponse()
	{
		this.m_checkInRequestPending = false;
		CheckInToFSGResponse checkInToFSGResponse = Network.Get().GetCheckInToFSGResponse();
		if (checkInToFSGResponse.ErrorCode != ErrorCode.ERROR_OK && checkInToFSGResponse.ErrorCode != ErrorCode.ERROR_FSG_ALREADY_CHECKED_IN_FETCH_FSG_INFO)
		{
			global::Log.FiresideGatherings.PrintError("CheckInResponse: code={0} {1} fsgId={2} patronCount={3}", new object[]
			{
				(int)checkInToFSGResponse.ErrorCode,
				checkInToFSGResponse.ErrorCode,
				checkInToFSGResponse.FsgId,
				checkInToFSGResponse.FsgAttendees.Count
			});
			this.m_errorOccuredOnCheckin = true;
			return;
		}
		global::Log.FiresideGatherings.Print("CheckInResponse: code={0} {1} fsgId={2} patronCount={3}", new object[]
		{
			(int)checkInToFSGResponse.ErrorCode,
			checkInToFSGResponse.ErrorCode,
			checkInToFSGResponse.FsgId,
			(checkInToFSGResponse.FsgAttendees == null) ? "null" : checkInToFSGResponse.FsgAttendees.Count.ToString()
		});
		FriendChallengeMgr.Get().UpdateMyFsgSharedSecret(checkInToFSGResponse.FsgSharedSecretKey);
		this.JoinFSG(checkInToFSGResponse.FsgId, checkInToFSGResponse.FsgAttendees, checkInToFSGResponse.FsgSharedSecretKey, checkInToFSGResponse.InnkeeperSelectedBrawlLibraryItemId);
	}

	// Token: 0x060027A6 RID: 10150 RVA: 0x000C66D4 File Offset: 0x000C48D4
	private void OnCheckOutOfFSGResponse()
	{
		CheckOutOfFSGResponse checkOutOfFSGResponse = Network.Get().GetCheckOutOfFSGResponse();
		if (checkOutOfFSGResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			global::Log.FiresideGatherings.PrintError("CheckOutResponse: code={0} {1} fsgId={2}", new object[]
			{
				(int)checkOutOfFSGResponse.ErrorCode,
				checkOutOfFSGResponse.ErrorCode,
				checkOutOfFSGResponse.FsgId
			});
			return;
		}
		global::Log.FiresideGatherings.Print("CheckOutResponse: code={0} {1} fsgId={2}", new object[]
		{
			(int)checkOutOfFSGResponse.ErrorCode,
			checkOutOfFSGResponse.ErrorCode,
			checkOutOfFSGResponse.FsgId
		});
		FriendChallengeMgr.Get().UpdateMyFsgSharedSecret(null);
		this.LeaveFSG();
	}

	// Token: 0x060027A7 RID: 10151 RVA: 0x000C6784 File Offset: 0x000C4984
	private void CheckCanBeginLocationDataGatheringForLogin()
	{
		if (this.m_hasBegunLocationDataGatheringForLogin)
		{
			return;
		}
		global::Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering", Array.Empty<object>());
		if (FiresideGatheringManager.s_guardianVars.Value == null)
		{
			global::Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO GUARDIAN", Array.Empty<object>());
			return;
		}
		if (FiresideGatheringManager.s_fsgFeaturesConfig.Value == null)
		{
			global::Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO FEATURE CONFIG", Array.Empty<object>());
			return;
		}
		if (FiresideGatheringManager.s_profileProgress.Value == null)
		{
			global::Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO PROFILE PROGRESS", Array.Empty<object>());
			return;
		}
		if (FiresideGatheringManager.s_clientOptions.Value == null)
		{
			global::Log.FiresideGatherings.PrintDebug("FiresideGatheringManager.CheckCanBeginLocationGathering NO CLIENT OPTIONS", Array.Empty<object>());
			return;
		}
		this.BeginLocationDataGatheringForLogin();
	}

	// Token: 0x060027A8 RID: 10152 RVA: 0x000C6838 File Offset: 0x000C4A38
	private void OnNetCache_GuardianVars()
	{
		NetCache.NetCacheFeatures value = FiresideGatheringManager.s_guardianVars.Value;
		if (value.FSGEnabled == FiresideGatheringManager.s_cacheFSGEnabled)
		{
			return;
		}
		if (!value.FSGEnabled && this.IsCheckedIn)
		{
			this.CheckOutOfFSG(false);
			this.LeaveFSG();
			this.m_nearbyFSGs.Clear();
		}
		FiresideGatheringManager.s_cacheFSGEnabled = value.FSGEnabled;
		this.CheckCanBeginLocationDataGatheringForLogin();
	}

	// Token: 0x060027A9 RID: 10153 RVA: 0x000C6898 File Offset: 0x000C4A98
	private void OnNetCache_FSGFeatureConfig()
	{
		FSGFeatureConfig value = FiresideGatheringManager.s_fsgFeaturesConfig.Value;
		if (value.Gps == FiresideGatheringManager.s_cacheGPSEnabled && value.Wifi == FiresideGatheringManager.s_cacheWifiEnabled)
		{
			return;
		}
		FiresideGatheringManager.s_cacheGPSEnabled = value.Gps;
		FiresideGatheringManager.s_cacheWifiEnabled = value.Wifi;
		this.CheckCanBeginLocationDataGatheringForLogin();
	}

	// Token: 0x060027AA RID: 10154 RVA: 0x000C68E8 File Offset: 0x000C4AE8
	private void RebuildKnownPatronsFromPresence()
	{
		this.m_knownPatronsFromPresence.Clear();
		if (!this.IsCheckedIn)
		{
			return;
		}
		long num = (this.m_currentFSG == null) ? 0L : this.m_currentFSG.FsgId;
		foreach (BnetPlayer player in BnetFriendMgr.Get().GetFriends())
		{
			FiresideGatheringInfo playerFSGInfo = FiresideGatheringManager.GetPlayerFSGInfo(player);
			if (playerFSGInfo != null && playerFSGInfo.FsgId == num)
			{
				this.AddKnownPatronFromPresence(player);
			}
		}
	}

	// Token: 0x060027AB RID: 10155 RVA: 0x000C6980 File Offset: 0x000C4B80
	private void AddKnownPatronFromPresence(BnetPlayer player)
	{
		if (this.m_knownPatronsFromPresence.Contains(player))
		{
			return;
		}
		bool flag;
		this.AddKnownPatron(BnetUtils.CreatePegasusBnetId(player.GetAccountId()), BnetUtils.CreatePegasusBnetId(player.GetHearthstoneGameAccountId()), true, out flag);
	}

	// Token: 0x060027AC RID: 10156 RVA: 0x000C69BC File Offset: 0x000C4BBC
	private void PlayersPresenceChanged(BnetPlayerChangelist changelist, out List<BnetPlayer> addedToDisplayablePatronList, out List<BnetPlayer> removedFromDisplayablePatronList)
	{
		addedToDisplayablePatronList = null;
		removedFromDisplayablePatronList = null;
		List<BnetPlayer> list = new List<BnetPlayer>();
		BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
		foreach (BnetPlayer bnetPlayer in this.m_pendingPatrons)
		{
			if (bnetPlayer.GetAccountId() != accountId && FiresideGatheringPresenceManager.IsDisplayable(bnetPlayer) && !this.IsPlayerInMyFSGAndDisplayable(bnetPlayer))
			{
				bool flag = this.m_displayablePatrons.Add(bnetPlayer);
				list.Add(bnetPlayer);
				if (flag)
				{
					if (addedToDisplayablePatronList == null)
					{
						addedToDisplayablePatronList = new List<BnetPlayer>();
					}
					addedToDisplayablePatronList.Add(bnetPlayer);
				}
			}
		}
		foreach (BnetPlayer item in list)
		{
			this.m_pendingPatrons.Remove(item);
		}
		list.Clear();
		long num = (this.m_currentFSG == null) ? 0L : this.m_currentFSG.FsgId;
		List<BnetPlayerChange> list2 = (changelist == null) ? null : changelist.GetChanges();
		if (list2 != null)
		{
			for (int i = 0; i < list2.Count; i++)
			{
				BnetPlayerChange bnetPlayerChange = list2[i];
				BnetPlayer newPlayer = bnetPlayerChange.GetNewPlayer();
				bool flag2 = newPlayer.GetAccountId() == accountId;
				if (!flag2)
				{
					bool flag3 = false;
					FiresideGatheringInfo playerFSGInfo = FiresideGatheringManager.GetPlayerFSGInfo(newPlayer);
					if (playerFSGInfo != null && playerFSGInfo.FsgId == num)
					{
						this.AddKnownPatronFromPresence(newPlayer);
					}
					else if (this.m_knownPatronsFromPresence.Contains(newPlayer))
					{
						if (!this.m_knownPatronsFromServer.Contains(newPlayer))
						{
							flag3 = (this.m_displayablePatrons.Remove(newPlayer) || flag3);
							this.m_pendingPatrons.Remove(newPlayer);
						}
						this.m_knownPatronsFromPresence.Remove(newPlayer);
					}
					if (this.IsPlayerInMyFSGAndDisplayable(newPlayer) && !FiresideGatheringPresenceManager.IsDisplayable(newPlayer))
					{
						flag3 = (this.m_displayablePatrons.Remove(newPlayer) || flag3);
						this.m_pendingPatrons.Add(newPlayer);
					}
					if (flag3)
					{
						if (removedFromDisplayablePatronList == null)
						{
							removedFromDisplayablePatronList = new List<BnetPlayer>();
						}
						removedFromDisplayablePatronList.Add(newPlayer);
					}
				}
				if (flag2 && !bnetPlayerChange.GetOldPlayer().IsAppearingOffline() && bnetPlayerChange.GetNewPlayer().IsAppearingOffline() && this.IsCheckedIn)
				{
					this.PromptPlayerToAppearOnline(this.CurrentFsgId);
				}
			}
		}
	}

	// Token: 0x060027AD RID: 10157 RVA: 0x000C6C18 File Offset: 0x000C4E18
	private void OnInnkeeperSetupGatheringResponse()
	{
		InnkeeperSetupGatheringResponse innkeeperSetupGatheringResponse = Network.Get().GetInnkeeperSetupGatheringResponse();
		bool flag = true;
		if (innkeeperSetupGatheringResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			global::Log.FiresideGatherings.PrintError("InnkeeperSetupResponse: code={0} {1} fsgId={2}", new object[]
			{
				(int)innkeeperSetupGatheringResponse.ErrorCode,
				innkeeperSetupGatheringResponse.ErrorCode,
				innkeeperSetupGatheringResponse.FsgId
			});
			flag = false;
		}
		global::Log.FiresideGatherings.Print("InnkeeperSetupResponse: code={0} {1} fsgId={2}", new object[]
		{
			(int)innkeeperSetupGatheringResponse.ErrorCode,
			innkeeperSetupGatheringResponse.ErrorCode,
			innkeeperSetupGatheringResponse.FsgId
		});
		if (flag)
		{
			this.m_innkeeperFSG.IsSetupComplete = true;
		}
		if (this.OnInnkeeperSetupFinished != null)
		{
			this.OnInnkeeperSetupFinished(flag);
		}
		if (flag)
		{
			this.CheckInToFSG(this.m_innkeeperFSG.FsgId);
		}
	}

	// Token: 0x060027AE RID: 10158 RVA: 0x000C6CF4 File Offset: 0x000C4EF4
	private BnetPlayer AddKnownPatron(FSGPatron patron, bool isKnownFromPresence)
	{
		bool flag;
		return this.AddKnownPatron(patron.BnetAccount, patron.GameAccount, isKnownFromPresence, out flag);
	}

	// Token: 0x060027AF RID: 10159 RVA: 0x000C6D18 File Offset: 0x000C4F18
	private BnetPlayer AddKnownPatron(BnetId bnetAccountId, BnetId gameAccountId, bool isKnownFromPresence, out bool isNewDisplayablePatron)
	{
		isNewDisplayablePatron = false;
		BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
		if (bnetAccountId.Lo == accountId.GetLo())
		{
			return null;
		}
		BnetAccountId accountId2 = BnetAccountId.CreateFromNet(bnetAccountId);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FSG_PATRON, accountId2, BnetGameAccountId.CreateFromNet(gameAccountId), BnetProgramId.HEARTHSTONE);
		if (bnetPlayer == null)
		{
			return null;
		}
		if (this.m_displayablePatrons.Contains(bnetPlayer))
		{
			isNewDisplayablePatron = false;
		}
		else if (FiresideGatheringPresenceManager.IsDisplayable(bnetPlayer))
		{
			this.m_displayablePatrons.Add(bnetPlayer);
			isNewDisplayablePatron = true;
			this.m_pendingPatrons.Remove(bnetPlayer);
		}
		else
		{
			this.m_pendingPatrons.Add(bnetPlayer);
			isNewDisplayablePatron = false;
		}
		if (isKnownFromPresence)
		{
			this.m_knownPatronsFromPresence.Add(bnetPlayer);
		}
		else
		{
			this.m_knownPatronsFromServer.Add(bnetPlayer);
		}
		return bnetPlayer;
	}

	// Token: 0x060027B0 RID: 10160 RVA: 0x000C6DDC File Offset: 0x000C4FDC
	private void AddKnownInnkeeper(long fsgId, BnetId bnetAccountId)
	{
		if (bnetAccountId == null)
		{
			return;
		}
		BnetAccountId bnetAccountId2 = BnetAccountId.CreateFromNet(bnetAccountId);
		BnetPlayer bnetPlayer = BnetPresenceMgr.Get().RegisterPlayer(BnetPlayerSource.FSG_PATRON, bnetAccountId2, null, BnetProgramId.HEARTHSTONE);
		if (bnetPlayer == null)
		{
			return;
		}
		if (!this.m_innkeepers.ContainsKey(fsgId))
		{
			this.m_innkeepers.Add(fsgId, bnetPlayer);
		}
		BnetAccountId accountId = BnetPresenceMgr.Get().GetMyPlayer().GetAccountId();
		if (bnetAccountId.Lo == accountId.GetLo())
		{
			return;
		}
		BnetPresenceMgr.RequestPlayerBattleTag(bnetAccountId2);
	}

	// Token: 0x060027B1 RID: 10161 RVA: 0x000C6E4C File Offset: 0x000C504C
	private void OnPatronListUpdateReceivedFromServer()
	{
		if (this.m_currentFSG == null || this.CurrentFsgIsLargeScale)
		{
			return;
		}
		bool isAppendingPatronList = this.m_isAppendingPatronList;
		this.m_isAppendingPatronList = true;
		FSGPatronListUpdate fsgpatronListUpdate = Network.Get().GetFSGPatronListUpdate();
		ulong myselfGameAccountLo = BnetPresenceMgr.Get().GetMyPlayer().GetBestGameAccountId().GetLo();
		fsgpatronListUpdate.AddedPatrons.RemoveAll((FSGPatron patron) => myselfGameAccountLo == patron.GameAccount.Lo);
		List<BnetPlayer> list = null;
		List<BnetPlayer> list2 = null;
		foreach (FSGPatron fsgpatron in fsgpatronListUpdate.AddedPatrons)
		{
			bool flag;
			BnetPlayer item = this.AddKnownPatron(fsgpatron.BnetAccount, fsgpatron.GameAccount, false, out flag);
			if (flag)
			{
				if (list == null)
				{
					list = new List<BnetPlayer>();
				}
				list.Add(item);
			}
		}
		foreach (FSGPatron fsgpatron2 in fsgpatronListUpdate.RemovedPatrons)
		{
			BnetGameAccountId id = BnetGameAccountId.CreateFromNet(fsgpatron2.GameAccount);
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(id);
			this.m_knownPatronsFromServer.Remove(player);
			bool flag2 = false;
			if (!this.m_knownPatronsFromPresence.Contains(player))
			{
				flag2 = this.m_displayablePatrons.Remove(player);
				this.m_pendingPatrons.Remove(player);
			}
			if (flag2)
			{
				if (list2 == null)
				{
					list2 = new List<BnetPlayer>();
				}
				list2.Add(player);
			}
		}
		FiresideGatheringPresenceManager.Get().AddRemovePatronSubscriptions(fsgpatronListUpdate.AddedPatrons, fsgpatronListUpdate.RemovedPatrons);
		this.m_isAppendingPatronList = isAppendingPatronList;
		if (FiresideGatheringManager.OnPatronListUpdated != null)
		{
			FiresideGatheringManager.OnPatronListUpdated(list, list2);
		}
	}

	// Token: 0x060027B2 RID: 10162 RVA: 0x000C700C File Offset: 0x000C520C
	private void OnPlayersPresenceChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (!this.IsCheckedIn || this.m_isAppendingPatronList)
		{
			return;
		}
		List<BnetPlayer> addedToDisplayablePatronList;
		List<BnetPlayer> list;
		this.PlayersPresenceChanged(changelist, out addedToDisplayablePatronList, out list);
		FiresideGatheringPresenceManager.Get().CheckForMoreSubscribeOpportunities(list, this.m_pendingPatrons);
		if (FiresideGatheringManager.OnPatronListUpdated != null)
		{
			FiresideGatheringManager.OnPatronListUpdated(addedToDisplayablePatronList, list);
		}
	}

	// Token: 0x060027B3 RID: 10163 RVA: 0x000C705C File Offset: 0x000C525C
	private void PeriodicCheckForMoreSubscribeOpportunities(object userData)
	{
		if (!this.IsCheckedIn || HearthstoneApplication.Get().IsResetting() || HearthstoneApplication.Get().IsExiting())
		{
			return;
		}
		FiresideGatheringPresenceManager.Get().CheckForMoreSubscribeOpportunities(null, this.m_pendingPatrons);
		Processor.ScheduleCallback((float)FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS, true, new Processor.ScheduledCallback(this.PeriodicCheckForMoreSubscribeOpportunities), null);
	}

	// Token: 0x060027B4 RID: 10164 RVA: 0x000C70B8 File Offset: 0x000C52B8
	private void InitializeTransitionInputBlocker(bool enabled)
	{
		if (this.m_transitionInputBlocker == null)
		{
			Camera camera = CameraUtils.FindFirstByLayer(GameLayer.BattleNetDialog);
			this.m_transitionInputBlocker = CameraUtils.CreateInputBlocker(camera, "FSGTransitionInputBlocker");
			this.m_transitionInputBlocker.transform.SetParent(this.SceneObject.transform);
			TransformUtil.SetPosZ(this.m_transitionInputBlocker, 1f);
			this.m_transitionInputBlocker.gameObject.SetActive(enabled);
		}
	}

	// Token: 0x060027B5 RID: 10165 RVA: 0x000C7128 File Offset: 0x000C5328
	private static FiresideGatheringInfo GetPlayerFSGInfo(BnetPlayer player)
	{
		BnetGameAccount bnetGameAccount = (player == null) ? null : player.GetHearthstoneGameAccount();
		if (bnetGameAccount == null)
		{
			return null;
		}
		byte[] gameFieldBytes = bnetGameAccount.GetGameFieldBytes(25U);
		if (gameFieldBytes != null && gameFieldBytes.Length != 0)
		{
			return ProtobufUtil.ParseFrom<FiresideGatheringInfo>(gameFieldBytes, 0, -1);
		}
		return null;
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x000C7167 File Offset: 0x000C5367
	private FiresideGatheringInfo GetMyFSGInfoForPresence()
	{
		if (this.m_currentFSG == null)
		{
			return null;
		}
		return new FiresideGatheringInfo
		{
			FsgId = this.m_currentFSG.FsgId
		};
	}

	// Token: 0x060027B7 RID: 10167 RVA: 0x000C718C File Offset: 0x000C538C
	private void UpdateMyPresence()
	{
		FiresideGatheringInfo myFSGInfoForPresence = this.GetMyFSGInfoForPresence();
		BnetPresenceMgr.Get().SetGameFieldBlob(25U, myFSGInfoForPresence);
	}

	// Token: 0x060027B8 RID: 10168 RVA: 0x000C71AE File Offset: 0x000C53AE
	public void Cheat_CheckInToFakeFSG(FSGConfig fsg)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.m_cachedFakeCheatFsg = fsg;
		Network.Get().RegisterNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.Cheat_OnTavernBrawlInfoCheckInToFakeFSG), null);
		Network.Get().RequestTavernBrawlInfo(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x000C71EC File Offset: 0x000C53EC
	private void Cheat_OnTavernBrawlInfoCheckInToFakeFSG()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.Cheat_OnTavernBrawlInfoCheckInToFakeFSG));
		PegasusPacket packet = new PegasusPacket(505, 0, new CheckInToFSGResponse
		{
			ErrorCode = ErrorCode.ERROR_OK,
			PlayerRecord = new TavernBrawlPlayerRecord(),
			PlayerRecord = 
			{
				SessionStatus = TavernBrawlStatus.TB_STATUS_ACTIVE
			},
			FsgId = this.m_cachedFakeCheatFsg.FsgId
		});
		Network.Get().SimulateReceivedPacketFromServer(packet);
		this.m_cachedFakeCheatFsg = null;
	}

	// Token: 0x060027BA RID: 10170 RVA: 0x000C7278 File Offset: 0x000C5478
	public void Cheat_CheckInToFakeFSG()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		FSGConfig fsgconfig = new FSGConfig();
		fsgconfig.FsgId = -1L;
		fsgconfig.TavernName = "Fake Gathering";
		fsgconfig.UnixOfficialStartTime = global::TimeUtils.UnixTimestampSeconds - 7200L;
		fsgconfig.UnixOfficialEndTime = global::TimeUtils.UnixTimestampSeconds + 14400L;
		fsgconfig.UnixStartTimeWithSlush = fsgconfig.UnixOfficialStartTime - 28800L;
		fsgconfig.UnixEndTimeWithSlush = fsgconfig.UnixOfficialEndTime + 28800L;
		this.Cheat_CheckInToFakeFSG(fsgconfig);
	}

	// Token: 0x060027BB RID: 10171 RVA: 0x000C72F8 File Offset: 0x000C54F8
	public void Cheat_CheckOutOfFakeFSG()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		PegasusPacket packet = new PegasusPacket(506, 0, new CheckOutOfFSGResponse
		{
			ErrorCode = ErrorCode.ERROR_OK
		});
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	// Token: 0x060027BC RID: 10172 RVA: 0x000C7332 File Offset: 0x000C5532
	public void Cheat_NearbyFSGNotice()
	{
		this.NotifyFSGNearby();
	}

	// Token: 0x060027BD RID: 10173 RVA: 0x000C733C File Offset: 0x000C553C
	public void Cheat_CreateFakeGatherings(int numGatherings, bool innkeeper = false)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
		requestNearbyFSGsResponse.ErrorCode = ErrorCode.ERROR_OK;
		requestNearbyFSGsResponse.FSGs = new List<FSGConfig>();
		for (int i = 0; i < numGatherings; i++)
		{
			FSGConfig fsgconfig = new FSGConfig();
			fsgconfig.FsgId = (long)(-i - 2);
			fsgconfig.TavernName = "Fake Gathering " + i;
			fsgconfig.UnixOfficialStartTime = global::TimeUtils.UnixTimestampSeconds - 7200L;
			fsgconfig.UnixOfficialEndTime = global::TimeUtils.UnixTimestampSeconds + 14400L;
			fsgconfig.UnixStartTimeWithSlush = fsgconfig.UnixOfficialStartTime - 28800L;
			fsgconfig.UnixEndTimeWithSlush = fsgconfig.UnixOfficialEndTime + 28800L;
			fsgconfig.SignData = new TavernSignData
			{
				Sign = UnityEngine.Random.Range(1, 8),
				Background = UnityEngine.Random.Range(1, 15),
				Major = UnityEngine.Random.Range(1, 85),
				Minor = UnityEngine.Random.Range(1, 43),
				SignType = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM
			};
			if (innkeeper && i == 0)
			{
				fsgconfig.IsInnkeeper = true;
				fsgconfig.IsSetupComplete = false;
			}
			requestNearbyFSGsResponse.FSGs.Add(fsgconfig);
		}
		PegasusPacket packet = new PegasusPacket(504, 0, requestNearbyFSGsResponse);
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x000C7478 File Offset: 0x000C5678
	public void Cheat_RemoveFakeGatherings()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		RequestNearbyFSGsResponse requestNearbyFSGsResponse = new RequestNearbyFSGsResponse();
		requestNearbyFSGsResponse.ErrorCode = ErrorCode.ERROR_OK;
		requestNearbyFSGsResponse.FSGs = new List<FSGConfig>();
		foreach (FSGConfig fsgconfig in this.m_nearbyFSGs)
		{
			if (fsgconfig.FsgId >= 0L)
			{
				requestNearbyFSGsResponse.FSGs.Add(fsgconfig);
			}
		}
		PegasusPacket packet = new PegasusPacket(504, 0, requestNearbyFSGsResponse);
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	// Token: 0x060027BF RID: 10175 RVA: 0x000C7514 File Offset: 0x000C5714
	public void Cheat_MockInnkeeperSetup()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		PegasusPacket packet = new PegasusPacket(508, 0, new InnkeeperSetupGatheringResponse
		{
			ErrorCode = ErrorCode.ERROR_OK,
			FsgId = this.m_innkeeperFSG.FsgId
		});
		Network.Get().SimulateReceivedPacketFromServer(packet);
	}

	// Token: 0x060027C0 RID: 10176 RVA: 0x000C7560 File Offset: 0x000C5760
	public void Cheat_ShowSign(TavernSignType type, int sign, int background, int major, int minor, string tavernName)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.ShowSign(new TavernSignData
		{
			SignType = type,
			Sign = sign,
			Background = background,
			Major = major,
			Minor = minor
		}, tavernName, null);
	}

	// Token: 0x060027C1 RID: 10177 RVA: 0x000C75AA File Offset: 0x000C57AA
	public void Cheat_GPSOffset(double offset)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.m_gpsCheatOffset = offset;
	}

	// Token: 0x060027C2 RID: 10178 RVA: 0x000C75BB File Offset: 0x000C57BB
	public void Cheat_GPSSet(double latitude, double longitude)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.m_gpsCheatingLocation = true;
		this.m_gpsCheatLatitude = latitude;
		this.m_gpsCheatLongitude = longitude;
	}

	// Token: 0x060027C3 RID: 10179 RVA: 0x000C75DA File Offset: 0x000C57DA
	public void Cheat_ResetGPSCheating()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		this.m_gpsCheatingLocation = false;
		this.m_gpsCheatLatitude = 0.0;
		this.m_gpsCheatLongitude = 0.0;
		this.m_gpsCheatOffset = 0.0;
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x000C7618 File Offset: 0x000C5818
	public void Cheat_GetGPSCheats(out bool isCheatingGPS, out double latitude, out double longitude, out double offset)
	{
		isCheatingGPS = this.m_gpsCheatingLocation;
		latitude = this.m_gpsCheatLatitude;
		longitude = this.m_gpsCheatLongitude;
		offset = this.m_gpsCheatOffset;
	}

	// Token: 0x060027C5 RID: 10181 RVA: 0x000C763B File Offset: 0x000C583B
	public void Cheat_ToggleLargeScaleFSG()
	{
		if (this.m_currentFSG == null)
		{
			return;
		}
		this.m_currentFSG.IsLargeScaleFsg = !this.m_currentFSG.IsLargeScaleFsg;
		if (this.OnJoinFSG != null)
		{
			this.OnJoinFSG(this.m_currentFSG);
		}
	}

	// Token: 0x060027C6 RID: 10182 RVA: 0x000C7678 File Offset: 0x000C5878
	public BnetPlayer Cheat_CreateFSGPatron(string fullName, int leagueId, int starLevel, BnetProgramId programId, bool isFriend, bool isOnline)
	{
		BnetPlayer bnetPlayer = BnetFriendMgr.Get().Cheat_CreatePlayer(fullName, leagueId, starLevel, programId, isFriend, isOnline);
		this.m_displayablePatrons.Add(bnetPlayer);
		return bnetPlayer;
	}

	// Token: 0x060027C7 RID: 10183 RVA: 0x000C76A7 File Offset: 0x000C58A7
	public int Cheat_RemoveCheatFriends()
	{
		return this.m_displayablePatrons.RemoveWhere((BnetPlayer player) => player.IsCheatPlayer);
	}

	// Token: 0x04001646 RID: 5702
	public const long INVALID_FSG_ID = 0L;

	// Token: 0x04001649 RID: 5705
	private FSGConfig m_currentFSG;

	// Token: 0x0400164A RID: 5706
	private byte[] m_currentFSGSharedSecretKey;

	// Token: 0x0400164B RID: 5707
	private HashSet<int> m_innkeeperSelectedBrawlLibraryItemIds = new HashSet<int>();

	// Token: 0x0400164C RID: 5708
	private List<FSGConfig> m_nearbyFSGs = new List<FSGConfig>();

	// Token: 0x0400164D RID: 5709
	private HashSet<BnetPlayer> m_knownPatronsFromServer = new HashSet<BnetPlayer>();

	// Token: 0x0400164E RID: 5710
	private HashSet<BnetPlayer> m_knownPatronsFromPresence = new HashSet<BnetPlayer>();

	// Token: 0x0400164F RID: 5711
	private HashSet<BnetPlayer> m_displayablePatrons = new HashSet<BnetPlayer>();

	// Token: 0x04001650 RID: 5712
	private HashSet<BnetPlayer> m_pendingPatrons = new HashSet<BnetPlayer>();

	// Token: 0x04001651 RID: 5713
	private bool m_isAppendingPatronList;

	// Token: 0x04001652 RID: 5714
	private global::Map<long, BnetPlayer> m_innkeepers = new global::Map<long, BnetPlayer>();

	// Token: 0x04001653 RID: 5715
	private bool m_checkInRequestPending;

	// Token: 0x04001654 RID: 5716
	private bool m_checkInDialogShown;

	// Token: 0x04001655 RID: 5717
	private bool m_nearbyFSGsFoundEventSent;

	// Token: 0x04001656 RID: 5718
	private Notification m_tooltipShowing;

	// Token: 0x04001657 RID: 5719
	private ClientLocationData m_locationData;

	// Token: 0x04001658 RID: 5720
	private global::Map<string, AccessPointInfo> m_accumulatedAccessPoints = new global::Map<string, AccessPointInfo>();

	// Token: 0x04001659 RID: 5721
	private bool m_hasBegunLocationDataGatheringForLogin;

	// Token: 0x0400165A RID: 5722
	private FiresideGatheringSign m_currentSign;

	// Token: 0x0400165B RID: 5723
	private Transform m_smallSignContainer;

	// Token: 0x0400165C RID: 5724
	private FiresideGatheringManager.OnCloseSign m_currentSignCallback;

	// Token: 0x0400165D RID: 5725
	private bool m_haltFSGNotificationsAndCheckins;

	// Token: 0x0400165E RID: 5726
	private bool m_fsgSignShown;

	// Token: 0x0400165F RID: 5727
	private bool m_doAutoInnkeeperSetup = true;

	// Token: 0x04001660 RID: 5728
	private bool m_haltAutoCheckinWhileInnkeeperSetup;

	// Token: 0x04001661 RID: 5729
	private bool m_errorOccuredOnCheckin;

	// Token: 0x04001662 RID: 5730
	private bool m_waitingForCheckIn;

	// Token: 0x04001663 RID: 5731
	private FSGConfig m_innkeeperFSG;

	// Token: 0x04001664 RID: 5732
	private bool m_fsgAvailableToCheckin;

	// Token: 0x04001665 RID: 5733
	private bool m_isRequestNearbyFSGsPending;

	// Token: 0x04001666 RID: 5734
	private double m_gpsCheatOffset;

	// Token: 0x04001667 RID: 5735
	private static bool s_cacheFSGEnabled = false;

	// Token: 0x04001668 RID: 5736
	private static bool s_cacheGPSEnabled = false;

	// Token: 0x04001669 RID: 5737
	private static bool s_cacheWifiEnabled = false;

	// Token: 0x0400166A RID: 5738
	private bool m_gpsCheatingLocation;

	// Token: 0x0400166B RID: 5739
	private double m_gpsCheatLatitude;

	// Token: 0x0400166C RID: 5740
	private double m_gpsCheatLongitude;

	// Token: 0x0400166D RID: 5741
	private FSGConfig m_cachedFakeCheatFsg;

	// Token: 0x0400166E RID: 5742
	private const string BACKGROUND_TEXTURE_SHADER_VAL = "_BackgroundTex";

	// Token: 0x0400166F RID: 5743
	private const string MAJOR_TEXTURE_SHADER_VAL = "_MajorTex";

	// Token: 0x04001670 RID: 5744
	private const string MINOR_TEXTURE_SHADER_VAL = "_MinorTex";

	// Token: 0x04001671 RID: 5745
	private static readonly AssetReference m_tavernSignAsset = new AssetReference("FSG_TavernSign.prefab:8ce9cae2230ceda45a5f20996b704a9b");

	// Token: 0x04001672 RID: 5746
	private GameObject m_sceneObject;

	// Token: 0x04001673 RID: 5747
	private static readonly AssetReference[] m_fsgShields = new AssetReference[]
	{
		new AssetReference("shield_01.prefab:78363d95f6d2de34fbc560266fea640d"),
		new AssetReference("shield_02.prefab:c377b1e43c7e56940b5976606e4c204d"),
		new AssetReference("shield_03.prefab:1aff0388b4ec9a541914c6001d89a1a4"),
		new AssetReference("shield_04.prefab:b8c72df88e9b2a346be4921349e95d69"),
		new AssetReference("shield_05.prefab:8b4ff22e9b7e20a44afdce7d38c71179"),
		new AssetReference("shield_06.prefab:e0adca09a959f1c4ea1921958d4b7b88"),
		new AssetReference("shield_07.prefab:62ae59fe1ee4edb41ab4e2f56f2c4c9d"),
		new AssetReference("shield_08.prefab:2ac01bcf753502d4391495e1ba01297f")
	};

	// Token: 0x04001674 RID: 5748
	private static readonly AssetReference[] m_backgroundTextures = new AssetReference[]
	{
		new AssetReference("FSG_BG_01.psd:e688e3dbcd82aa540bd5a237b8046087"),
		new AssetReference("FSG_BG_02.psd:ae5f9d676c6184d41b976845d4131392"),
		new AssetReference("FSG_BG_03.psd:2bfa2796138e44b4db4ffd7d7c35048c"),
		new AssetReference("FSG_BG_04.psd:4968e80d9d5570a49bf34a477953c463"),
		new AssetReference("FSG_BG_05.psd:8b2ce0acdd997df4d9a235d10b0b0245"),
		new AssetReference("FSG_BG_06.psd:50c2055eec0ae094e8c44d98a86ec997"),
		new AssetReference("FSG_BG_07.psd:2e9be5a80e6fb8c4ab5e8f30ecb529b5"),
		new AssetReference("FSG_BG_08.psd:3413b3c98ad07944b923e5b475c5cb71"),
		new AssetReference("FSG_BG_09.psd:495ad5978abcac5428426c9422b34f54"),
		new AssetReference("FSG_BG_10.psd:6840b0525caaafc46ada6c96aec606c7"),
		new AssetReference("FSG_BG_11.psd:183ea8ead0f840b458c3b6b6feaecd9e"),
		new AssetReference("FSG_BG_12.psd:36143d8a02d74644a95f4f875b084687"),
		new AssetReference("FSG_BG_13.psd:e86b53637b4c7f940af65cf93f139ad7"),
		new AssetReference("FSG_BG_14.psd:ca315657cd75a3d4183070b7620eea46"),
		new AssetReference("FSG_BG_15.psd:ba08c85d3825071429b4452a05e1f869")
	};

	// Token: 0x04001675 RID: 5749
	private static readonly AssetReference[] m_majorTextures = new AssetReference[]
	{
		new AssetReference("FSG_major_icon_01.psd:07f39638ef5fac0409bceafcfe91a017"),
		new AssetReference("FSG_major_icon_02.psd:ba033ce365731044dbd0a6447b927516"),
		new AssetReference("FSG_major_icon_03.psd:4e63ef4c0d31305449bc692cd8ed4296"),
		new AssetReference("FSG_major_icon_04.psd:1600613c07c2b894db3985ae0d058df6"),
		new AssetReference("FSG_major_icon_05.psd:2b3cec372d669a14bac09798de77f4aa"),
		new AssetReference("FSG_major_icon_06.psd:7f3790b88769cd745bae9b0bca991a42"),
		new AssetReference("FSG_major_icon_07.psd:d6de41cea2ada024e8e4f561f7604691"),
		new AssetReference("FSG_major_icon_08.psd:7511ad8cbc11b8f4abc097895bf36f72"),
		new AssetReference("FSG_major_icon_09.psd:52a8139050006ee4f8a713228ec0680e"),
		new AssetReference("FSG_major_icon_10.psd:783574c20786759499bd49291d09dd0b"),
		new AssetReference("FSG_major_icon_11.psd:abec391ccf583f2409e4274963e11fb7"),
		new AssetReference("FSG_major_icon_12.psd:ef67cc5c43169bd4a9fbf9e429fff2c1"),
		new AssetReference("FSG_major_icon_13.psd:bec11e064c7f3fd408c59174e001a566"),
		new AssetReference("FSG_major_icon_14.psd:db89aa7c56a75e542b41b4b68934150b"),
		new AssetReference("FSG_major_icon_15.psd:7227d20dda7e8b743b5b3429065b94cb"),
		new AssetReference("FSG_major_icon_16.psd:9a95f2321a81b034bb21bdd8af813dd7"),
		new AssetReference("FSG_major_icon_17.psd:7e3e3309328a8d24ca4d73a819455026"),
		new AssetReference("FSG_major_icon_18.psd:664f85016c825ae4d87b32f1e2aee030"),
		new AssetReference("FSG_major_icon_19.psd:585e794cb0197db47bf865aa7342bfce"),
		new AssetReference("FSG_major_icon_20.psd:fed552b35702f944eb3d71eaaebd811b"),
		new AssetReference("FSG_major_icon_21.psd:53a8ce78f94668b419be52058eb62744"),
		new AssetReference("FSG_major_icon_22.psd:e3f2458e7131899489ca0cd3e96202ce"),
		new AssetReference("FSG_major_icon_23.psd:061bd991c06d66b45a3e6e85ee917085"),
		new AssetReference("FSG_major_icon_24.psd:968f4450552f1234786ebc685dcfab37"),
		new AssetReference("FSG_major_icon_25.psd:0e7774e4335227148b30c32a319dcd91"),
		new AssetReference("FSG_major_icon_26.psd:9d7beaf2c0b180b4b836ce5d3ba213f0"),
		new AssetReference("FSG_major_icon_27.psd:7055b2b8640998f478e59eb17063d434"),
		new AssetReference("FSG_major_icon_28.psd:3b0eae9bf1035f943a83b5da9ea06265"),
		new AssetReference("FSG_major_icon_29.psd:733546c7388d3db4da3875a12d2dbb04"),
		new AssetReference("FSG_major_icon_30.psd:b3189578cae8a2a418edd28b18871e5a"),
		new AssetReference("FSG_major_icon_31.psd:440512e280677784e9d203d183bd4b3b"),
		new AssetReference("FSG_major_icon_32.psd:d6ba8937a47ae3443bf494d7081df118"),
		new AssetReference("FSG_major_icon_33.psd:8817a57cf03ca3b459f8179201e9ef61"),
		new AssetReference("FSG_major_icon_34.psd:11244accdd35fcf4eadff77a526e674c"),
		new AssetReference("FSG_major_icon_35.psd:9b5ab5a32b35f9744869f20eda9a5a3f"),
		new AssetReference("FSG_major_icon_36.psd:b63250e48235c3446aaed3d3eda8a039"),
		new AssetReference("FSG_major_icon_37.psd:06d29c693b13e4341bdccc98879318f3"),
		new AssetReference("FSG_major_icon_38.psd:b595fe851a209284090902687ef4719a"),
		new AssetReference("FSG_major_icon_39.psd:b1a27e92316e6154695947477b692f31"),
		new AssetReference("FSG_major_icon_40.psd:aed8fc8c49d256b4a9bdbb8d1f2653b4"),
		new AssetReference("FSG_major_icon_41.psd:d0c01bd273040e54fa1e37b4b968e21d"),
		new AssetReference("FSG_major_icon_42.psd:cad47cc1621a9a74aacb7538e62ed968"),
		new AssetReference("FSG_major_icon_43.psd:6a62aa135e1d7f24e9d16f232c819771"),
		new AssetReference("FSG_major_icon_44.psd:e1fa19d8f78ed604986f3e3b08da86e3"),
		new AssetReference("FSG_major_icon_45.psd:94a2d34daafb0db48aeee1bb29f30d94"),
		new AssetReference("FSG_major_icon_46.psd:eae1bb9b57e5d2d46b76c2da550d9361"),
		new AssetReference("FSG_major_icon_47.psd:f6d114a4c539da7409ed64c04a6b4d1c"),
		new AssetReference("FSG_major_icon_48.psd:13482644107b8124baaa27c4b69f7f40"),
		new AssetReference("FSG_major_icon_49.psd:60f5e08764889bd4891230677667c06a"),
		new AssetReference("FSG_major_icon_50.psd:ae3b236c6985ed149a8e11cae889891d"),
		new AssetReference("FSG_major_icon_51.psd:be89f941faf7e884f9517b3cc758cc95"),
		new AssetReference("FSG_major_icon_52.psd:d64ccacd8e3575f4f8934868aeeebac0"),
		new AssetReference("FSG_major_icon_53.psd:85e1dfeb648dd25478c278b10452ea04"),
		new AssetReference("FSG_major_icon_54.psd:3639a3461412bcd468f36bd0d8808194"),
		new AssetReference("FSG_major_icon_55.psd:6dfe240913765e7439864ae59e906021"),
		new AssetReference("FSG_major_icon_56.psd:50542c563f4226746948db1227e041a8"),
		new AssetReference("FSG_major_icon_57.psd:c35838a239a1a9d4fbe40886c1836151"),
		new AssetReference("FSG_major_icon_58.psd:0153d881bf26d904eafe57c0f4069b68"),
		new AssetReference("FSG_major_icon_59.psd:7ea63d9dc56429843ba7ad8e662d6be4"),
		new AssetReference("FSG_major_icon_60.psd:2da17d5f4dd8218458a8a47cc5a5315b"),
		new AssetReference("FSG_major_icon_61.psd:7138870a6a857f5439fda6dbf723ee8a"),
		new AssetReference("FSG_major_icon_62.psd:56c1d251d05be7849b21a92236292231"),
		new AssetReference("FSG_major_icon_63.psd:956d1ae251106c043aeea63cc82a8dc0"),
		new AssetReference("FSG_major_icon_64.psd:8507174dedd9fbf46ad2a3eb3608d3d0"),
		new AssetReference("FSG_major_icon_65.psd:f1c75986e3593584ab58710c5b9afc16"),
		new AssetReference("FSG_major_icon_66.psd:547d68ca4a4d9a847ba684b31d672754"),
		new AssetReference("FSG_major_icon_67.psd:c302ac3c5c6208b4d82db58ec2534840"),
		new AssetReference("FSG_major_icon_68.psd:999a9c18e17735246b975f538f5f39e9"),
		new AssetReference("FSG_major_icon_69.psd:d3bd17b65f76e734bb02b6201576613f"),
		new AssetReference("FSG_major_icon_70.psd:df022a496a17dd64c95a5f4f6d9e1dbf"),
		new AssetReference("FSG_major_icon_71.psd:390ccf576a2fc464ab1c3cdf9a01c05f"),
		new AssetReference("FSG_major_icon_72.psd:13c3d60992523af418244d648bec2927"),
		new AssetReference("FSG_major_icon_73.psd:bc28bce291ff2284395f1504ebd4c352"),
		new AssetReference("FSG_major_icon_74.psd:a7d9b83cf0f7ebf45abac1b19bd64f25"),
		new AssetReference("FSG_major_icon_75.psd:74d90f06be30baa4aba2c7d629d56edd"),
		new AssetReference("FSG_major_icon_76.psd:bf7de29836133584cb1529079c315956"),
		new AssetReference("FSG_major_icon_77.psd:1126fe28c57f50c42af8de4e64016dbe"),
		new AssetReference("FSG_major_icon_78.psd:f3d8a417df5cce244802d22ee22fb8f3"),
		new AssetReference("FSG_major_icon_79.psd:705c5ab1b294713469d61cb9b719c21f"),
		new AssetReference("FSG_major_icon_80.psd:7e4b7b718714ecc43b757f31b6f35b5a"),
		new AssetReference("FSG_major_icon_81.psd:16c7b39fdde347e4a80f6912a6a9c20e"),
		new AssetReference("FSG_major_icon_82.psd:5cd2aa06a7003ae449270d3f57698eaf"),
		new AssetReference("FSG_major_icon_83.psd:4e038124f13272e4197dca5ded30e3ec"),
		new AssetReference("FSG_major_icon_84.psd:66c8e00ffd94eaa4bb91ef35c8bfab63"),
		new AssetReference("FSG_major_icon_85.psd:79cc45177c1bbe3438a8404770221fbd")
	};

	// Token: 0x04001676 RID: 5750
	private static readonly AssetReference[] m_minorTextures = new AssetReference[]
	{
		new AssetReference("FSG_minor_icon_01.psd:76f1d2c44969469479ca1d22ed4bb2c5"),
		new AssetReference("FSG_minor_icon_02.psd:5720e69f56a33f343893cbe0bdb83328"),
		new AssetReference("FSG_minor_icon_03.psd:581cd97bcda285d469ea061c2a1b65e0"),
		new AssetReference("FSG_minor_icon_04.psd:eb9a4735aaee11047b850c1639180cc0"),
		new AssetReference("FSG_minor_icon_05.psd:73ffd45b96f36984683d94002aeb687f"),
		new AssetReference("FSG_minor_icon_06.psd:fb79ddcfa27651141a69fe57d707fd31"),
		new AssetReference("FSG_minor_icon_07.psd:2dc56df138ffca54c99c2823d8b8c230"),
		new AssetReference("FSG_minor_icon_08.psd:aa6b9c693dba947459171534c501561c"),
		new AssetReference("FSG_minor_icon_09.psd:1223eaead151e0442b39d44b79d2fe99"),
		new AssetReference("FSG_minor_icon_10.psd:e35d16e74556b824a82a24fdef18ab6e"),
		new AssetReference("FSG_minor_icon_11.psd:686a7f1c092367a4ebefcf0be60a5025"),
		new AssetReference("FSG_minor_icon_12.psd:f6951bde51d82f94b95dd49a17a9acde"),
		new AssetReference("FSG_minor_icon_13.psd:c22f4029bc9a09c47b05ce79910a85c9"),
		new AssetReference("FSG_minor_icon_14.psd:a19fd3d9b22bd5f439df4ceb1bf65b3d"),
		new AssetReference("FSG_minor_icon_15.psd:ef282300e35d6114682429509c1ec6be"),
		new AssetReference("FSG_minor_icon_16.psd:5a72a33d18d433442a381db9aa9c5eae"),
		new AssetReference("FSG_minor_icon_17.psd:b13ef82f1b931c741bbffc35b55ce244"),
		new AssetReference("FSG_minor_icon_18.psd:ba5c373f08f049848b950f3c547edc00"),
		new AssetReference("FSG_minor_icon_19.psd:fd556fad5a1adc0448aa85e14ae0b33b"),
		new AssetReference("FSG_minor_icon_20.psd:ec061cb081c39a749a3d7cc07aa4c5af"),
		new AssetReference("FSG_minor_icon_21.psd:6d47b3264df5bad40847b6a0b2c763ff"),
		new AssetReference("FSG_minor_icon_22.psd:4b21f492cc667104b9b4fc87dabca71f"),
		new AssetReference("FSG_minor_icon_23.psd:4cd0570a850886d41a962273726efc86"),
		new AssetReference("FSG_minor_icon_24.psd:cc11c8161f16f1a4298cc85629fd8f24"),
		new AssetReference("FSG_minor_icon_25.psd:f86b6ce99de7dba48a6a3f9617d9e37a"),
		new AssetReference("FSG_minor_icon_26.psd:b85753f42615a3442a328f41fb214a8d"),
		new AssetReference("FSG_minor_icon_27.psd:76ed51a0cbdac4a4885b6a9a6f35db34"),
		new AssetReference("FSG_minor_icon_28.psd:4e7c0cbabe0df1a4aa75ffb7ad4ecf3d"),
		new AssetReference("FSG_minor_icon_29.psd:45af214ee19ff79408336758bfbbd400"),
		new AssetReference("FSG_minor_icon_30.psd:c3670d1a631e2054984bd6ade942600b"),
		new AssetReference("FSG_minor_icon_31.psd:c7f21d37679fc6d4b980876ece61e1f4"),
		new AssetReference("FSG_minor_icon_32.psd:10746cf72967e2541b7978ff5e23ef79"),
		new AssetReference("FSG_minor_icon_33.psd:af1b2ce747ad74143aff763617ba9691"),
		new AssetReference("FSG_minor_icon_34.psd:c79f56adcf6621b4f9574c1e18adb146"),
		new AssetReference("FSG_minor_icon_35.psd:aed7c2408cd63c94991f2f5dc91a046b"),
		new AssetReference("FSG_minor_icon_36.psd:6409f5c8977ba1b4bbf65d919b35f860"),
		new AssetReference("FSG_minor_icon_37.psd:b4654929917a9b340ac6da0e51c2093b"),
		new AssetReference("FSG_minor_icon_38.psd:2285c681967265847a6b583271ffc132"),
		new AssetReference("FSG_minor_icon_39.psd:a0dc328dcc11e3049905c29c85ffa1fe"),
		new AssetReference("FSG_minor_icon_40.psd:8a69a6cc8757a0643a06083a1c7b4b3d"),
		new AssetReference("FSG_minor_icon_41.psd:4c48d1ce608b0b24684545ce45a08db8"),
		new AssetReference("FSG_minor_icon_42.psd:61224edd16eba5e47bdde26683514baa"),
		new AssetReference("FSG_minor_icon_43.psd:785e9e8832f639647a0ca0a3da6ca9f2")
	};

	// Token: 0x04001677 RID: 5751
	private const int MAX_SIGN_INDEX = 8;

	// Token: 0x04001678 RID: 5752
	private const int MAX_BACKGROUND_INDEX = 15;

	// Token: 0x04001679 RID: 5753
	private const int MAX_MAJOR_INDEX = 85;

	// Token: 0x0400167A RID: 5754
	private const int MAX_MINOR_INDEX = 43;

	// Token: 0x0400167B RID: 5755
	private GameObject m_transitionInputBlocker;

	// Token: 0x0400167D RID: 5757
	private static ReactiveObject<NetCache.NetCacheFeatures> s_guardianVars = ReactiveNetCacheObject<NetCache.NetCacheFeatures>.CreateInstance();

	// Token: 0x0400167E RID: 5758
	private static ReactiveObject<FSGFeatureConfig> s_fsgFeaturesConfig = ReactiveNetCacheObject<FSGFeatureConfig>.CreateInstance();

	// Token: 0x0400167F RID: 5759
	private static ReactiveObject<NetCache.NetCacheProfileProgress> s_profileProgress = ReactiveNetCacheObject<NetCache.NetCacheProfileProgress>.CreateInstance();

	// Token: 0x04001680 RID: 5760
	private static ReactiveObject<NetCache.NetCacheClientOptions> s_clientOptions = ReactiveNetCacheObject<NetCache.NetCacheClientOptions>.CreateInstance();

	// Token: 0x04001681 RID: 5761
	private ReactiveEnumOption<FormatType> m_FormatType = ReactiveEnumOption<FormatType>.CreateInstance(Option.FORMAT_TYPE);

	// Token: 0x04001682 RID: 5762
	public long m_activeFSGMenu = -1L;

	// Token: 0x02001602 RID: 5634
	public enum FiresideGatheringMode
	{
		// Token: 0x0400AFA7 RID: 44967
		NONE,
		// Token: 0x0400AFA8 RID: 44968
		MAIN_SCREEN,
		// Token: 0x0400AFA9 RID: 44969
		FRIENDLY_CHALLENGE,
		// Token: 0x0400AFAA RID: 44970
		FRIENDLY_CHALLENGE_BRAWL,
		// Token: 0x0400AFAB RID: 44971
		FIRESIDE_BRAWL
	}

	// Token: 0x02001603 RID: 5635
	// (Invoke) Token: 0x0600E283 RID: 57987
	public delegate void CheckedInToFSGCallback(FSGConfig gathering);

	// Token: 0x02001604 RID: 5636
	// (Invoke) Token: 0x0600E287 RID: 57991
	public delegate void CheckedOutOfFSGCallback(FSGConfig gathering);

	// Token: 0x02001605 RID: 5637
	// (Invoke) Token: 0x0600E28B RID: 57995
	public delegate void RequestNearbyFSGsCallback();

	// Token: 0x02001606 RID: 5638
	// (Invoke) Token: 0x0600E28F RID: 57999
	public delegate void NearbyFSGsChangedCallback();

	// Token: 0x02001607 RID: 5639
	// (Invoke) Token: 0x0600E293 RID: 58003
	public delegate void OnCloseSign();

	// Token: 0x02001608 RID: 5640
	// (Invoke) Token: 0x0600E297 RID: 58007
	public delegate void OnInnkeeperSetupFinishedCallback(bool success);

	// Token: 0x02001609 RID: 5641
	// (Invoke) Token: 0x0600E29B RID: 58011
	public delegate void FSGSignClosedCallback();

	// Token: 0x0200160A RID: 5642
	// (Invoke) Token: 0x0600E29F RID: 58015
	public delegate void FSGSignShownCallback();

	// Token: 0x0200160B RID: 5643
	// (Invoke) Token: 0x0600E2A3 RID: 58019
	public delegate void OnPatronListUpdatedCallback(List<BnetPlayer> addedToDisplayablePatronList, List<BnetPlayer> removedFromDisplayablePatronList);
}
