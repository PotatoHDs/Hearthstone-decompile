using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using bnet.protocol;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;
using BobNetProto;
using Hearthstone;
using Hearthstone.Login;
using Hearthstone.Streaming;
using HearthstoneTelemetry;
using HSCachedDeckCompletion;
using Networking;
using PegasusFSG;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;

// Token: 0x02000605 RID: 1541
public class Network : IService, IHasUpdate
{
	// Token: 0x14000030 RID: 48
	// (add) Token: 0x06005423 RID: 21539 RVA: 0x001B7EF0 File Offset: 0x001B60F0
	// (remove) Token: 0x06005424 RID: 21540 RVA: 0x001B7F28 File Offset: 0x001B6128
	public event Action<BattleNetErrors> OnConnectedToBattleNet;

	// Token: 0x14000031 RID: 49
	// (add) Token: 0x06005425 RID: 21541 RVA: 0x001B7F60 File Offset: 0x001B6160
	// (remove) Token: 0x06005426 RID: 21542 RVA: 0x001B7F98 File Offset: 0x001B6198
	public event Action<BattleNetErrors> OnDisconnectedFromBattleNet;

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x06005427 RID: 21543 RVA: 0x001B7FCD File Offset: 0x001B61CD
	public static string BranchName
	{
		get
		{
			return string.Format("{0}.{1}{2}", "20.4", "0", "");
		}
	}

	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x06005428 RID: 21544 RVA: 0x001B7FE8 File Offset: 0x001B61E8
	// (set) Token: 0x06005429 RID: 21545 RVA: 0x001B7FEF File Offset: 0x001B61EF
	private static List<BattleNetErrors> GameServerDisconnectEvents { get; set; }

	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x0600542A RID: 21546 RVA: 0x001B7FF7 File Offset: 0x001B61F7
	// (set) Token: 0x0600542B RID: 21547 RVA: 0x001B7FFF File Offset: 0x001B61FF
	private long FakeIdWaitingForResponse { get; set; }

	// Token: 0x0600542C RID: 21548 RVA: 0x001B8008 File Offset: 0x001B6208
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		this.m_state.SetDefaults();
		if (PlatformSettings.s_isDeviceSupported)
		{
			HearthstoneApplication.Get().WillReset += this.WillReset;
			HearthstoneApplication.Get().Resetting += this.OnReset;
			Network.s_running = true;
			this.CreateNewDispatcher();
			this.InitBattleNet(this.m_dispatcherImpl);
			this.RegisterNetHandler(SubscribeResponse.PacketID.ID, new Network.NetHandler(this.OnSubscribeResponse), null);
			this.RegisterNetHandler(ClientStateNotification.PacketID.ID, new Network.NetHandler(this.OnClientStateNotification), null);
			this.RegisterNetHandler(PegasusUtil.GenericResponse.PacketID.ID, new Network.NetHandler(this.OnGenericResponse), null);
			this.RegisterNetHandler(PegasusUtil.GetDeckContentsResponse.PacketID.ID, new Network.NetHandler(this.OnDeckContentsResponse), null);
			if (Network.TUTORIALS_WITHOUT_ACCOUNT)
			{
				Network.SetShouldBeConnectedToAurora(global::Options.Get().GetBool(global::Option.CONNECT_TO_AURORA));
			}
		}
		yield break;
	}

	// Token: 0x0600542D RID: 21549 RVA: 0x001B8017 File Offset: 0x001B6217
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GameDbf),
			typeof(NetworkReachabilityManager)
		};
	}

	// Token: 0x0600542E RID: 21550 RVA: 0x001B803C File Offset: 0x001B623C
	public void Shutdown()
	{
		if (!Network.s_running)
		{
			return;
		}
		NetCache.Get().DispatchClientOptionsToServer();
		PresenceMgr.Get().OnShutdown();
		if (Network.IsLoggedIn())
		{
			this.CancelFindGame();
		}
		this.CloseAll();
		BattleNet.AppQuit();
		BnetNearbyPlayerMgr.Get().Shutdown();
		Network.s_running = false;
	}

	// Token: 0x0600542F RID: 21551 RVA: 0x001B808D File Offset: 0x001B628D
	private void WillReset()
	{
		NetCache.Get().DispatchClientOptionsToServer();
		NetCache.Get().Clear();
		this.m_state.DelayedError = null;
		this.m_state.TimeBeforeAllowReset = 0f;
		if (this.m_connectApi != null)
		{
			this.RemoveConnectApiConnectionListeners();
		}
	}

	// Token: 0x06005430 RID: 21552 RVA: 0x001B80CD File Offset: 0x001B62CD
	public void OnReset()
	{
		this.m_state = default(Network.NetworkState);
		this.m_state.SetDefaults();
		if (this.m_connectApi != null)
		{
			this.RegisterConnectApiConnectionListeners();
		}
		Network.s_running = true;
		this.ResetForNewAuroraConnection();
	}

	// Token: 0x06005431 RID: 21553 RVA: 0x001B8104 File Offset: 0x001B6304
	public bool ResetForNewAuroraConnection()
	{
		global::Log.Offline.PrintDebug("Resetting for new Aurora Connection", Array.Empty<object>());
		NetCache.Get().ClearForNewAuroraConnection();
		this.m_state.QueuedClientStateNotifications.Clear();
		this.CloseAll();
		this.m_dispatcherImpl.ResetForNewConnection();
		this.m_inTransitRequests.Clear();
		bool flag = false;
		if (Network.ShouldBeConnectedToAurora())
		{
			string username = Network.GetUsername();
			string targetServer = Network.GetTargetServer();
			uint port = Network.GetPort();
			SslParameters sslparams = Network.GetSSLParams();
			flag = BattleNet.Reset(Network.CreateBattleNetImplementation(), HearthstoneApplication.IsInternal(), username, targetServer, port, sslparams);
			global::Log.Offline.PrintDebug("ResetForNewAuroraConnection: ResetOk={0}", new object[]
			{
				flag
			});
		}
		if (flag || !Network.ShouldBeConnectedToAurora())
		{
			BnetParty.SetDisconnectedFromBattleNet();
			this.m_connectApi.SetDisconnectedFromBattleNet();
			this.InitializeConnectApi(this.m_dispatcherImpl);
		}
		return flag;
	}

	// Token: 0x06005432 RID: 21554 RVA: 0x001B81D7 File Offset: 0x001B63D7
	public static Network Get()
	{
		return HearthstoneServices.Get<Network>();
	}

	// Token: 0x06005433 RID: 21555 RVA: 0x001B81DE File Offset: 0x001B63DE
	public static float GetMaxDeferredWait()
	{
		return Network.m_maxDeferredWait;
	}

	// Token: 0x06005434 RID: 21556 RVA: 0x001B81E8 File Offset: 0x001B63E8
	public static string ProductVersion()
	{
		return string.Concat(new string[]
		{
			20.ToString(),
			".",
			4.ToString(),
			".",
			0.ToString(),
			".",
			0.ToString()
		});
	}

	// Token: 0x06005435 RID: 21557 RVA: 0x001B824C File Offset: 0x001B644C
	private void CreateNewDispatcher()
	{
		IDebugConnectionManager debugConnectionManager = new DebugConnectionManager();
		this.m_dispatcherImpl = new QueueDispatcher(debugConnectionManager, new ClientRequestManager(), new PacketDecoderManager(debugConnectionManager.AllowDebugConnections()), TelemetryManager.NetworkComponent);
	}

	// Token: 0x06005436 RID: 21558 RVA: 0x001B8280 File Offset: 0x001B6480
	private void ProcessRequestTimeouts()
	{
		float now = Time.realtimeSinceStartup;
		for (int i = 0; i < this.m_inTransitRequests.Count; i++)
		{
			Network.RequestContext requestContext = this.m_inTransitRequests[i];
			if (requestContext.m_timeoutHandler != null && requestContext.m_waitUntil < now)
			{
				Debug.LogWarning(string.Format("Encountered timeout waiting for {0} {1} {2}", requestContext.m_pendingResponseId, requestContext.m_requestId, requestContext.m_requestSubId));
				requestContext.m_timeoutHandler(requestContext.m_pendingResponseId, requestContext.m_requestId, requestContext.m_requestSubId);
			}
		}
		this.m_inTransitRequests.RemoveAll((Network.RequestContext rc) => rc.m_waitUntil < now);
	}

	// Token: 0x06005437 RID: 21559 RVA: 0x001B8340 File Offset: 0x001B6540
	public void AddPendingRequestTimeout(int requestId, int requestSubId)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return;
		}
		int num = 0;
		if ((201 != requestId || !Network.m_deferredGetAccountInfoMessageResponseMap.TryGetValue(requestSubId, out num)) && !Network.m_deferredMessageResponseMap.TryGetValue(requestId, out num))
		{
			return;
		}
		Network.TimeoutHandler timeoutHandler = null;
		if (this.m_state.NetTimeoutHandlers.TryGetValue(num, out timeoutHandler))
		{
			this.m_inTransitRequests.Add(new Network.RequestContext(num, requestId, requestSubId, timeoutHandler));
			return;
		}
		this.m_inTransitRequests.Add(new Network.RequestContext(num, requestId, requestSubId, new Network.TimeoutHandler(Network.OnRequestTimeout)));
	}

	// Token: 0x06005438 RID: 21560 RVA: 0x001B83CC File Offset: 0x001B65CC
	private void RemovePendingRequestTimeout(int pendingResponseId)
	{
		this.m_inTransitRequests.RemoveAll((Network.RequestContext pc) => pc.m_pendingResponseId == pendingResponseId);
	}

	// Token: 0x06005439 RID: 21561 RVA: 0x001B8400 File Offset: 0x001B6600
	private static void OnRequestTimeout(int pendingResponseId, int requestId, int requestSubId)
	{
		if (Network.m_deferredMessageResponseMap.ContainsValue(pendingResponseId) || Network.m_deferredGetAccountInfoMessageResponseMap.ContainsValue(pendingResponseId))
		{
			Debug.LogError(string.Format("OnRequestTimeout pending ID {0} {1} {2}", pendingResponseId, requestId, requestSubId));
			FatalErrorMgr.Get().SetErrorCode("HS", "NT" + pendingResponseId.ToString(), requestId.ToString(), requestSubId.ToString());
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.TIMEOUT_DEFERRED_RESPONSE, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
			Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN", 0f);
			return;
		}
		Debug.LogError(string.Format("Unhandled OnRequestTimeout pending ID {0} {1} {2}", pendingResponseId, requestId, requestSubId));
		FatalErrorMgr.Get().SetErrorCode("HS", "NU" + pendingResponseId.ToString(), requestId.ToString(), requestSubId.ToString());
		TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.TIMEOUT_NOT_DEFERRED_RESPONSE, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
		Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN", 0f);
	}

	// Token: 0x0600543A RID: 21562 RVA: 0x001B851C File Offset: 0x001B671C
	private void OnGenericResponse()
	{
		Network.GenericResponse genericResponse = this.GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError(string.Format("Login - GenericResponse parse error", Array.Empty<object>()));
			return;
		}
		bool flag = 201 == genericResponse.RequestId && Network.m_deferredGetAccountInfoMessageResponseMap.ContainsKey(genericResponse.RequestSubId);
		bool flag2 = Network.m_deferredMessageResponseMap.ContainsKey(genericResponse.RequestId);
		if (!flag && !flag2)
		{
			return;
		}
		if (Network.GenericResponse.Result.RESULT_REQUEST_IN_PROCESS == genericResponse.ResultCode)
		{
			return;
		}
		if (Network.GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED == genericResponse.ResultCode)
		{
			return;
		}
		Debug.LogError(string.Format("Unhandled resultCode {0} for requestId {1}:{2}", genericResponse.ResultCode, genericResponse.RequestId, genericResponse.RequestSubId));
		FatalErrorMgr.Get().SetErrorCode("HS", "NG" + genericResponse.ResultCode.ToString(), genericResponse.RequestId.ToString(), genericResponse.RequestSubId.ToString());
		TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.REQUEST_ERROR, FatalErrorMgr.Get().GetFormattedErrorCode(), 0);
		this.ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN", 0f);
	}

	// Token: 0x0600543B RID: 21563 RVA: 0x001B8633 File Offset: 0x001B6833
	public static bool IsRunning()
	{
		return Network.s_running;
	}

	// Token: 0x0600543C RID: 21564 RVA: 0x001B863C File Offset: 0x001B683C
	public double TimeSinceLastPong()
	{
		if (!this.IsConnectedToGameServer() || this.m_gameServerKeepAliveFrequencySeconds == 0U || this.m_connectApi.GetTimeLastPingSent() <= this.m_connectApi.GetTimeLastPingReceieved())
		{
			return 0.0;
		}
		return (double)Time.realtimeSinceStartup - this.m_connectApi.GetTimeLastPingReceieved();
	}

	// Token: 0x0600543D RID: 21565 RVA: 0x001B8690 File Offset: 0x001B6890
	private void OnSubscribeResponse()
	{
		SubscribeResponse subscribeResponse = this.m_connectApi.GetSubscribeResponse();
		if (subscribeResponse != null && subscribeResponse.HasRequestMaxWaitSecs && subscribeResponse.RequestMaxWaitSecs >= 30UL)
		{
			Network.m_maxDeferredWait = subscribeResponse.RequestMaxWaitSecs;
		}
	}

	// Token: 0x0600543E RID: 21566 RVA: 0x001B86CC File Offset: 0x001B68CC
	private void OnClientStateNotification()
	{
		ClientStateNotification clientStateNotification = this.m_connectApi.GetClientStateNotification();
		if (!NetCache.Get().HasReceivedInitialClientState)
		{
			this.m_state.QueuedClientStateNotifications.Add(clientStateNotification);
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			int countNotificationsAchieve = clientStateNotification.HasAchievementNotifications ? clientStateNotification.AchievementNotifications.AchievementNotifications_.Count : 0;
			int countNotificationsNotice = clientStateNotification.HasNoticeNotifications ? clientStateNotification.NoticeNotifications.NoticeNotifications_.Count : 0;
			int countNotificationsCollection;
			if (!clientStateNotification.HasCollectionModifications)
			{
				countNotificationsCollection = 0;
			}
			else
			{
				countNotificationsCollection = clientStateNotification.CollectionModifications.CardModifications.Sum((CardModification m) => m.Quantity);
			}
			int countNotificationsCurrency = clientStateNotification.HasCurrencyState ? 1 : 0;
			int countNotificationsBooster;
			if (!clientStateNotification.HasBoosterModifications)
			{
				countNotificationsBooster = 0;
			}
			else
			{
				countNotificationsBooster = clientStateNotification.BoosterModifications.Modifications.Sum((BoosterInfo m) => m.Count);
			}
			telemetryClient.SendInitialClientStateOutOfOrder(countNotificationsAchieve, countNotificationsNotice, countNotificationsCollection, countNotificationsCurrency, countNotificationsBooster, clientStateNotification.HasHeroXp ? clientStateNotification.HeroXp.XpInfos.Count : 0, clientStateNotification.HasPlayerRecords ? clientStateNotification.PlayerRecords.Records.Count : 0, clientStateNotification.HasArenaSessionResponse ? 1 : 0, clientStateNotification.HasCardBackModifications ? clientStateNotification.CardBackModifications.CardBackModifications_.Count : 0);
			return;
		}
		Network.ProcessClientStateNotification(clientStateNotification);
	}

	// Token: 0x0600543F RID: 21567 RVA: 0x001B882C File Offset: 0x001B6A2C
	public static void ProcessClientStateNotification(ClientStateNotification packet)
	{
		if (packet.HasCurrencyState)
		{
			NetCache.Get().OnCurrencyState(packet.CurrencyState);
		}
		if (packet.HasCollectionModifications)
		{
			NetCache.Get().OnCollectionModification(packet);
		}
		else
		{
			if (packet.HasAchievementNotifications)
			{
				AchieveManager.Get().OnAchievementNotifications(packet.AchievementNotifications.AchievementNotifications_);
			}
			if (packet.HasNoticeNotifications)
			{
				Network.Get().OnNoticeNotifications(packet.NoticeNotifications);
			}
			if (packet.HasBoosterModifications)
			{
				NetCache.Get().OnBoosterModifications(packet.BoosterModifications);
			}
		}
		if (packet.HasHeroXp)
		{
			NetCache.Get().OnHeroXP(packet.HeroXp);
		}
		if (packet.HasPlayerRecords)
		{
			NetCache.Get().OnPlayerRecordsPacket(packet.PlayerRecords);
		}
		if (packet.HasArenaSessionResponse)
		{
			DraftManager.Get().OnArenaSessionResponsePacket(packet.ArenaSessionResponse);
		}
		if (packet.HasCardBackModifications)
		{
			NetCache.Get().OnCardBackModifications(packet.CardBackModifications);
		}
		if (packet.HasPlayerDraftTickets)
		{
			NetCache.Get().OnPlayerDraftTickets(packet.PlayerDraftTickets);
		}
	}

	// Token: 0x06005440 RID: 21568 RVA: 0x001B892C File Offset: 0x001B6B2C
	public void OnInitialClientStateProcessed()
	{
		List<ClientStateNotification> list = new List<ClientStateNotification>(this.m_state.QueuedClientStateNotifications);
		this.m_state.QueuedClientStateNotifications.Clear();
		foreach (ClientStateNotification packet in list)
		{
			Network.ProcessClientStateNotification(packet);
		}
	}

	// Token: 0x06005441 RID: 21569 RVA: 0x001B8998 File Offset: 0x001B6B98
	public void OnNoticeNotifications(NoticeNotifications packet)
	{
		List<ProfileNotice> list = new List<ProfileNotice>();
		List<NetCache.ProfileNotice> receivedNotices = new List<NetCache.ProfileNotice>();
		for (int i = 0; i < packet.NoticeNotifications_.Count; i++)
		{
			NoticeNotification noticeNotification = packet.NoticeNotifications_[i];
			list.Add(noticeNotification.Notice);
		}
		this.HandleProfileNotices(list, ref receivedNotices);
		NetCache.Get().HandleIncomingProfileNotices(receivedNotices, false);
	}

	// Token: 0x06005442 RID: 21570 RVA: 0x001B89F5 File Offset: 0x001B6BF5
	private void RegisterConnectApiConnectionListeners()
	{
		this.m_connectApi.RegisterGameServerConnectEventListener(new Action<BattleNetErrors>(this.OnGameServerConnectEvent));
		this.m_connectApi.RegisterGameServerDisconnectEventListener(new Action<BattleNetErrors>(this.OnGameServerDisconnectEvent));
	}

	// Token: 0x06005443 RID: 21571 RVA: 0x001B8A25 File Offset: 0x001B6C25
	private void RemoveConnectApiConnectionListeners()
	{
		this.m_connectApi.RemoveGameServerConnectEventListener(new Action<BattleNetErrors>(this.OnGameServerConnectEvent));
		this.m_connectApi.RemoveGameServerDisconnectEventListener(new Action<BattleNetErrors>(this.OnGameServerDisconnectEvent));
	}

	// Token: 0x06005444 RID: 21572 RVA: 0x001B8A55 File Offset: 0x001B6C55
	public void UpdateCachedBnetValues()
	{
		this.m_state.CachedGameAccountId = BattleNet.GetMyGameAccountId();
		this.m_state.CachedRegion = BattleNet.GetCurrentRegion();
	}

	// Token: 0x06005445 RID: 21573 RVA: 0x001B8A77 File Offset: 0x001B6C77
	public void OverrideKeepAliveSeconds(uint value)
	{
		if (HearthstoneApplication.IsInternal())
		{
			this.m_gameServerKeepAliveFrequencySeconds = value;
		}
	}

	// Token: 0x06005446 RID: 21574 RVA: 0x001B8A88 File Offset: 0x001B6C88
	public bgs.types.EntityId GetMyGameAccountId()
	{
		bgs.types.EntityId myGameAccountId = BattleNet.GetMyGameAccountId();
		if (myGameAccountId.hi == 0UL && myGameAccountId.lo == 0UL)
		{
			return this.m_state.CachedGameAccountId;
		}
		return myGameAccountId;
	}

	// Token: 0x06005447 RID: 21575 RVA: 0x001B8AB8 File Offset: 0x001B6CB8
	public constants.BnetRegion GetCurrentRegion()
	{
		constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
		if (currentRegion == constants.BnetRegion.REGION_UNINITIALIZED)
		{
			return this.m_state.CachedRegion;
		}
		return currentRegion;
	}

	// Token: 0x06005448 RID: 21576 RVA: 0x001B8ADC File Offset: 0x001B6CDC
	private void InitializeConnectApi(IDispatcher dispatcher)
	{
		this.m_errorList.Clear();
		if (this.m_connectApi == null)
		{
			Network.GameServerDisconnectEvents = new List<BattleNetErrors>();
			this.m_connectApi = new ConnectAPI(dispatcher);
			this.RegisterConnectApiConnectionListeners();
		}
		this.m_connectApi.SetGameStartState(GameStartState.Invalid);
	}

	// Token: 0x06005449 RID: 21577 RVA: 0x001B8B1C File Offset: 0x001B6D1C
	public static void ApplicationPaused()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().DispatchClientOptionsToServer();
		}
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network) && network.m_connectApi != null)
		{
			network.m_connectApi.ProcessUtilPackets();
		}
		BattleNet.ApplicationWasPaused();
	}

	// Token: 0x0600544A RID: 21578 RVA: 0x001B8B5B File Offset: 0x001B6D5B
	public void CloseAll()
	{
		if (this.m_ackCardSeenPacket.CardDefs.Count != 0)
		{
			this.SendAckCardsSeen();
		}
		if (this.m_connectApi != null)
		{
			this.m_connectApi.Close();
		}
	}

	// Token: 0x0600544B RID: 21579 RVA: 0x001B8B88 File Offset: 0x001B6D88
	public static void ApplicationUnpaused()
	{
		BattleNet.ApplicationWasUnpaused();
	}

	// Token: 0x0600544C RID: 21580 RVA: 0x001B8B90 File Offset: 0x001B6D90
	public void Update()
	{
		if (!Network.s_running)
		{
			return;
		}
		this.ProcessRequestTimeouts();
		this.ProcessNetworkReachability();
		this.ProcessConnectApiHeartbeat();
		StoreManager.Get().Heartbeat();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_state.LastCall;
		if (num < Network.PROCESS_WARNING)
		{
			return;
		}
		if (realtimeSinceStartup - this.m_state.LastCallReport < Network.PROCESS_WARNING_REPORT_GAP)
		{
			return;
		}
		this.m_state.LastCallReport = realtimeSinceStartup;
		string devElapsedTimeString = global::TimeUtils.GetDevElapsedTimeString(num);
		Debug.LogWarning(string.Format("Network.ProcessNetwork not called for {0}", devElapsedTimeString));
	}

	// Token: 0x0600544D RID: 21581 RVA: 0x001B8C18 File Offset: 0x001B6E18
	private void ProcessConnectApiHeartbeat()
	{
		this.GetBattleNetPackets();
		int count = this.m_errorList.Count;
		for (int i = 0; i < count; i++)
		{
			Network.ConnectErrorParams connectErrorParams = this.m_errorList[i];
			if (connectErrorParams == null)
			{
				Debug.LogError("null error! " + this.m_errorList.Count);
			}
			else if (Time.realtimeSinceStartup >= connectErrorParams.m_creationTime + 0.4f)
			{
				this.m_errorList.RemoveAt(i);
				i--;
				count = this.m_errorList.Count;
				global::Error.AddFatal(connectErrorParams);
			}
		}
		if (this.m_connectApi == null)
		{
			return;
		}
		if (this.m_connectApi.HasGameServerConnection())
		{
			this.m_connectApi.UpdateGameServerConnection();
			this.UpdatePingPong();
		}
		this.m_connectApi.ProcessUtilPackets();
		if (this.m_connectApi.TryConnectDebugConsole())
		{
			this.m_connectApi.UpdateDebugConsole();
		}
	}

	// Token: 0x0600544E RID: 21582 RVA: 0x001B8CF4 File Offset: 0x001B6EF4
	private void ProcessNetworkReachability()
	{
		if (!Network.IsLoggedIn())
		{
			return;
		}
		if (!this.m_networkReachabilityManager.InternetAvailable_Cached)
		{
			if (this.IsInGame())
			{
				double totalSeconds = global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
				if (this.m_timeInternetUnreachable == 0.0)
				{
					this.m_timeInternetUnreachable = totalSeconds;
					return;
				}
				if (totalSeconds - this.m_timeInternetUnreachable < this.m_gameServerKeepAliveWaitForInternetSeconds)
				{
					return;
				}
			}
			global::Log.Offline.PrintError("Network.ProcessInternetReachability(): Access to the Internet has been lost.", Array.Empty<object>());
			global::Error.AddFatal(FatalErrorReason.NO_INTERNET_ACCESS, "GLOBAL_ERROR_NETWORK_DISCONNECT", Array.Empty<object>());
			return;
		}
		if (this.m_timeInternetUnreachable != 0.0)
		{
			double totalSeconds2 = global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
			TelemetryManager.Client().SendNetworkUnreachableRecovered((int)(totalSeconds2 - this.m_timeInternetUnreachable));
			if (this.IsInGame())
			{
				this.DisconnectFromGameServer();
			}
		}
		this.m_timeInternetUnreachable = 0.0;
	}

	// Token: 0x0600544F RID: 21583 RVA: 0x001B8DDF File Offset: 0x001B6FDF
	public void AddErrorToList(Network.ConnectErrorParams errorParams)
	{
		this.m_errorList.Add(errorParams);
	}

	// Token: 0x06005450 RID: 21584 RVA: 0x001B8DED File Offset: 0x001B6FED
	public void SetShouldIgnorePong(bool value)
	{
		this.m_connectApi.SetShouldIgnorePong(value);
	}

	// Token: 0x06005451 RID: 21585 RVA: 0x001B8DFB File Offset: 0x001B6FFB
	public void SetSpoofDisconnected(bool value)
	{
		this.m_connectApi.SetSpoofDisconnected(value);
	}

	// Token: 0x06005452 RID: 21586 RVA: 0x001B8E09 File Offset: 0x001B7009
	private bool IsInGame()
	{
		return GameState.Get() != null;
	}

	// Token: 0x06005453 RID: 21587 RVA: 0x001B8E14 File Offset: 0x001B7014
	private void UpdatePingPong()
	{
		if (this.m_gameServerKeepAliveFrequencySeconds > 0U)
		{
			double totalSeconds = global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
			if (this.m_connectApi.IsConnectedToGameServer() && totalSeconds - this.m_connectApi.GetTimeLastPingSent() > this.m_gameServerKeepAliveFrequencySeconds)
			{
				int pingsSinceLastPong = this.m_connectApi.GetPingsSinceLastPong();
				if (this.m_connectApi.GetTimeLastPingSent() <= this.m_connectApi.GetTimeLastPingReceieved())
				{
					this.m_connectApi.SetTimeLastPingReceived(totalSeconds - 0.001);
				}
				this.m_connectApi.SetTimeLastPingSent(totalSeconds);
				this.m_connectApi.SendPing();
				if ((long)pingsSinceLastPong >= (long)((ulong)this.m_gameServerKeepAliveRetry))
				{
					this.DisconnectFromGameServer();
					this.SetShouldIgnorePong(false);
				}
				this.m_connectApi.SetPingsSinceLastPong(pingsSinceLastPong + 1);
			}
		}
	}

	// Token: 0x06005454 RID: 21588 RVA: 0x001B8EE4 File Offset: 0x001B70E4
	private void GetBattleNetPackets()
	{
		GamesAPI.UtilResponse utilResponse;
		while ((utilResponse = BattleNet.NextUtilPacket()) != null)
		{
			bnet.protocol.Attribute attribute = utilResponse.m_response.AttributeList[0];
			bnet.protocol.Attribute attribute2 = utilResponse.m_response.AttributeList[1];
			int type = (int)attribute.Value.IntValue;
			byte[] blobValue = attribute2.Value.BlobValue;
			PegasusPacket pegasusPacket = new PegasusPacket(type, blobValue.Length, blobValue);
			pegasusPacket.Context = utilResponse.m_context;
			this.m_connectApi.DecodeAndProcessPacket(pegasusPacket);
		}
	}

	// Token: 0x06005455 RID: 21589 RVA: 0x001B8F58 File Offset: 0x001B7158
	public void AppAbort()
	{
		if (!Network.s_running)
		{
			return;
		}
		NetCache.Get().DispatchClientOptionsToServer();
		PresenceMgr.Get().OnShutdown();
		this.CancelFindGame();
		this.CloseAll();
		BattleNet.AppQuit();
		BnetNearbyPlayerMgr.Get().Shutdown();
		Network.s_running = false;
	}

	// Token: 0x06005456 RID: 21590 RVA: 0x001B8F97 File Offset: 0x001B7197
	public void ResetConnectionFailureCount()
	{
		this.m_numConnectionFailures = 0;
	}

	// Token: 0x06005457 RID: 21591 RVA: 0x001B8FA0 File Offset: 0x001B71A0
	public bool RegisterNetHandler(object enumId, Network.NetHandler handler, Network.TimeoutHandler timeoutHandler = null)
	{
		int key = (int)enumId;
		if (timeoutHandler != null)
		{
			if (this.m_state.NetTimeoutHandlers.ContainsKey(key))
			{
				return false;
			}
			this.m_state.NetTimeoutHandlers.Add(key, timeoutHandler);
		}
		List<Network.NetHandler> list;
		if (this.m_netHandlers.TryGetValue(key, out list))
		{
			if (list.Contains(handler))
			{
				return false;
			}
		}
		else
		{
			list = new List<Network.NetHandler>();
			this.m_netHandlers.Add(key, list);
		}
		list.Add(handler);
		return true;
	}

	// Token: 0x06005458 RID: 21592 RVA: 0x001B9014 File Offset: 0x001B7214
	public bool RemoveNetHandler(object enumId, Network.NetHandler handler)
	{
		int key = (int)enumId;
		List<Network.NetHandler> list;
		return this.m_netHandlers.TryGetValue(key, out list) && list.Remove(handler);
	}

	// Token: 0x06005459 RID: 21593 RVA: 0x001B9044 File Offset: 0x001B7244
	public void RegisterThrottledPacketListener(Network.ThrottledPacketListener listener)
	{
		if (this.m_throttledPacketListeners.Contains(listener))
		{
			return;
		}
		this.m_throttledPacketListeners.Add(listener);
	}

	// Token: 0x0600545A RID: 21594 RVA: 0x001B9061 File Offset: 0x001B7261
	public void RemoveThrottledPacketListener(Network.ThrottledPacketListener listener)
	{
		this.m_throttledPacketListeners.Remove(listener);
	}

	// Token: 0x0600545B RID: 21595 RVA: 0x001B9070 File Offset: 0x001B7270
	public void RegisterGameQueueHandler(Network.GameQueueHandler handler)
	{
		if (this.m_gameQueueHandler != null)
		{
			global::Log.Net.Print("handler {0} would bash game queue handler {1}", new object[]
			{
				handler,
				this.m_gameQueueHandler
			});
			return;
		}
		this.m_gameQueueHandler = handler;
	}

	// Token: 0x0600545C RID: 21596 RVA: 0x001B90A4 File Offset: 0x001B72A4
	public void RemoveGameQueueHandler(Network.GameQueueHandler handler)
	{
		if (this.m_gameQueueHandler != handler)
		{
			global::Log.Net.Print("Removing game queue handler that is not active {0}", new object[]
			{
				handler
			});
			return;
		}
		this.m_gameQueueHandler = null;
	}

	// Token: 0x0600545D RID: 21597 RVA: 0x001B90D5 File Offset: 0x001B72D5
	public void RegisterQueueInfoHandler(Network.QueueInfoHandler handler)
	{
		if (this.m_queueInfoHandler != null)
		{
			global::Log.Net.Print("handler {0} would bash queue info handler {1}", new object[]
			{
				handler,
				this.m_queueInfoHandler
			});
			return;
		}
		this.m_queueInfoHandler = handler;
	}

	// Token: 0x0600545E RID: 21598 RVA: 0x001B9109 File Offset: 0x001B7309
	public void RemoveQueueInfoHandler(Network.QueueInfoHandler handler)
	{
		if (this.m_queueInfoHandler != handler)
		{
			global::Log.Net.Print("Removing queue info handler that is not active {0}", new object[]
			{
				handler
			});
			return;
		}
		this.m_queueInfoHandler = null;
	}

	// Token: 0x0600545F RID: 21599 RVA: 0x001B913C File Offset: 0x001B733C
	public bool FakeHandleType(Enum enumId)
	{
		int id = Convert.ToInt32(enumId);
		return this.FakeHandleType(id);
	}

	// Token: 0x06005460 RID: 21600 RVA: 0x001B9157 File Offset: 0x001B7357
	public bool FakeHandleType(int id)
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			this.HandleType(id);
			return true;
		}
		return false;
	}

	// Token: 0x06005461 RID: 21601 RVA: 0x001B916C File Offset: 0x001B736C
	private bool HandleType(int id)
	{
		this.RemovePendingRequestTimeout(id);
		List<Network.NetHandler> list;
		if (!this.m_netHandlers.TryGetValue(id, out list) || list.Count == 0)
		{
			if (!this.CanIgnoreUnhandledPacket(id))
			{
				Debug.LogError(string.Format("Network.HandleType() - Received packet {0}, but there are no handlers for it.", id));
			}
			return false;
		}
		Network.NetHandler[] array = list.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
		return true;
	}

	// Token: 0x06005462 RID: 21602 RVA: 0x001B91D6 File Offset: 0x001B73D6
	private bool CanIgnoreUnhandledPacket(int id)
	{
		return id == 15 || id == 116 || id == 254;
	}

	// Token: 0x06005463 RID: 21603 RVA: 0x001B91F0 File Offset: 0x001B73F0
	private bool ProcessGameQueue()
	{
		QueueEvent queueEvent = BattleNet.GetQueueEvent();
		if (queueEvent == null)
		{
			return false;
		}
		switch (queueEvent.EventType)
		{
		case QueueEvent.Type.QUEUE_LEAVE:
		case QueueEvent.Type.QUEUE_DELAY_ERROR:
		case QueueEvent.Type.QUEUE_AMM_ERROR:
		case QueueEvent.Type.QUEUE_CANCEL:
		case QueueEvent.Type.QUEUE_GAME_STARTED:
		case QueueEvent.Type.ABORT_CLIENT_DROPPED:
			this.m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			break;
		}
		if (this.m_gameQueueHandler == null)
		{
			Debug.LogWarningFormat("m_gameQueueHandler is null in Network.ProcessGameQueue! event={0} server={1}:{2} gameHandle={3} clientHandle={4}", new object[]
			{
				queueEvent.EventType,
				(queueEvent.GameServer == null) ? "null" : queueEvent.GameServer.Address,
				(queueEvent.GameServer == null) ? 0U : queueEvent.GameServer.Port,
				(queueEvent.GameServer == null) ? 0U : queueEvent.GameServer.GameHandle,
				(queueEvent.GameServer == null) ? 0L : queueEvent.GameServer.ClientHandle
			});
		}
		else
		{
			this.m_gameQueueHandler(queueEvent);
		}
		return true;
	}

	// Token: 0x06005464 RID: 21604 RVA: 0x001B92FC File Offset: 0x001B74FC
	private bool ProcessGameServer()
	{
		int id = this.NextGamePacketType();
		bool result = this.HandleType(id);
		this.m_connectApi.DropGamePacket();
		return result;
	}

	// Token: 0x06005465 RID: 21605 RVA: 0x001B9324 File Offset: 0x001B7524
	private bool ProcessUtilServer()
	{
		int id = this.m_connectApi.NextUtilPacketType();
		bool result = this.HandleType(id);
		this.m_connectApi.DropUtilPacket();
		return result;
	}

	// Token: 0x06005466 RID: 21606 RVA: 0x001B9350 File Offset: 0x001B7550
	private bool ProcessConsole()
	{
		int id = this.m_connectApi.NextDebugPacketType();
		bool result = this.HandleType(id);
		this.m_connectApi.DropDebugPacket();
		return result;
	}

	// Token: 0x06005467 RID: 21607 RVA: 0x001B937C File Offset: 0x001B757C
	public Network.UnavailableReason GetHearthstoneUnavailable(bool gamePacket)
	{
		Network.UnavailableReason unavailableReason = new Network.UnavailableReason();
		if (gamePacket)
		{
			Deadend deadendGame = this.m_connectApi.GetDeadendGame();
			unavailableReason.mainReason = deadendGame.Reply1;
			unavailableReason.subReason = deadendGame.Reply2;
			unavailableReason.extraData = deadendGame.Reply3;
		}
		else
		{
			DeadendUtil deadendUtil = this.m_connectApi.GetDeadendUtil();
			unavailableReason.mainReason = deadendUtil.Reply1;
			unavailableReason.subReason = deadendUtil.Reply2;
			unavailableReason.extraData = deadendUtil.Reply3;
		}
		return unavailableReason;
	}

	// Token: 0x06005468 RID: 21608 RVA: 0x001B93F8 File Offset: 0x001B75F8
	public void BuyCard(int assetId, TAG_PREMIUM premium, int count, int unitBuyPrice, int currentCollectionCount)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != TAG_PREMIUM.NORMAL)
		{
			cardDef.Premium = (int)premium;
		}
		this.m_connectApi.BuyCard(cardDef, count, unitBuyPrice, currentCollectionCount);
	}

	// Token: 0x06005469 RID: 21609 RVA: 0x001B9430 File Offset: 0x001B7630
	public void SellCard(int assetId, TAG_PREMIUM premium, int count, int unitSellPrice, int currentCollectionCount)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != TAG_PREMIUM.NORMAL)
		{
			cardDef.Premium = (int)premium;
		}
		this.m_connectApi.SellCard(cardDef, count, unitSellPrice, currentCollectionCount);
	}

	// Token: 0x0600546A RID: 21610 RVA: 0x001B9465 File Offset: 0x001B7665
	public void GetAllClientOptions()
	{
		this.m_connectApi.GetAllClientOptions();
	}

	// Token: 0x0600546B RID: 21611 RVA: 0x001B9472 File Offset: 0x001B7672
	public void SetClientOptions(SetOptions packet)
	{
		this.m_connectApi.SetClientOptions(packet);
	}

	// Token: 0x0600546C RID: 21612 RVA: 0x001B9480 File Offset: 0x001B7680
	public static Network.BnetLoginState BattleNetStatus()
	{
		return (Network.BnetLoginState)BattleNet.BattleNetStatus();
	}

	// Token: 0x0600546D RID: 21613 RVA: 0x001B9487 File Offset: 0x001B7687
	public static bool IsLoggedIn()
	{
		return BattleNet.IsInitialized() && BattleNet.BattleNetStatus() == 4;
	}

	// Token: 0x0600546E RID: 21614 RVA: 0x001B949A File Offset: 0x001B769A
	public bool HaveUnhandledPackets()
	{
		return this.m_connectApi.HasUtilPackets() || this.m_connectApi.HasGamePackets() || this.m_connectApi.HasDebugPackets() || BattleNet.GetNotificationCount() > 0;
	}

	// Token: 0x0600546F RID: 21615 RVA: 0x001B94D4 File Offset: 0x001B76D4
	public int NextGamePacketType()
	{
		return this.m_connectApi.NextGamePacketType();
	}

	// Token: 0x06005470 RID: 21616 RVA: 0x001B94E1 File Offset: 0x001B76E1
	public PegasusPacket NextGamePacket()
	{
		return this.m_connectApi.NextGamePacket();
	}

	// Token: 0x06005471 RID: 21617 RVA: 0x001B94EE File Offset: 0x001B76EE
	public void PushReceivedGamePacket(PegasusPacket packet)
	{
		this.m_connectApi.PushReceivedGamePacket(packet);
	}

	// Token: 0x06005472 RID: 21618 RVA: 0x001B94FC File Offset: 0x001B76FC
	public void ProcessNetwork()
	{
		if (!Network.s_running)
		{
			return;
		}
		if (this.m_state.LastCallFrame == Time.frameCount)
		{
			return;
		}
		this.m_state.LastCallFrame = Time.frameCount;
		this.m_state.LastCall = Time.realtimeSinceStartup;
		if (!this.InitBattleNet(this.m_dispatcherImpl) && Network.ShouldBeConnectedToAurora())
		{
			return;
		}
		Network.s_urlDownloader.Process();
		if (Network.ShouldBeConnectedToAurora())
		{
			this.ProcessAurora();
		}
		else
		{
			this.ProcessDelayedError();
		}
		if (this.ProcessGameQueue())
		{
			return;
		}
		if (this.m_connectApi.HasGamePackets())
		{
			this.ProcessGameServer();
			return;
		}
		if (Network.GameServerDisconnectEvents != null && Network.GameServerDisconnectEvents.Count > 0)
		{
			BattleNetErrors[] array = Network.GameServerDisconnectEvents.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				Network.GameServerDisconnectEvent gameServerDisconnectEventListener = this.m_state.GameServerDisconnectEventListener;
				if (gameServerDisconnectEventListener != null)
				{
					gameServerDisconnectEventListener(array[i]);
				}
			}
			Network.GameServerDisconnectEvents.Clear();
		}
		if (this.m_connectApi.HasUtilPackets())
		{
			this.ProcessUtilServer();
			return;
		}
		if (this.m_connectApi.HasDebugPackets())
		{
			this.ProcessConsole();
			return;
		}
		this.ProcessQueuePosition();
	}

	// Token: 0x06005473 RID: 21619 RVA: 0x001B9618 File Offset: 0x001B7818
	private bool InitBattleNet(IDispatcher dispatcher)
	{
		bool flag = BattleNet.IsInitialized();
		if (!flag)
		{
			if (BattleNet.Get() == null)
			{
				BattleNet.SetImpl(Network.CreateBattleNetImplementation());
			}
			if (Network.ShouldBeConnectedToAurora())
			{
				string username = Network.GetUsername();
				string targetServer = Network.GetTargetServer();
				uint port = Network.GetPort();
				SslParameters sslparams = Network.GetSSLParams();
				flag = BattleNet.Init(BattleNet.Get(), HearthstoneApplication.IsInternal(), username, targetServer, port, sslparams);
			}
			if (flag || !Network.ShouldBeConnectedToAurora())
			{
				this.AddBnetErrorListener(BnetFeature.Auth, new Network.BnetErrorCallback(this.OnBnetAuthError));
				this.InitializeConnectApi(dispatcher);
			}
		}
		return flag;
	}

	// Token: 0x06005474 RID: 21620 RVA: 0x001B969C File Offset: 0x001B789C
	private static IBattleNet CreateBattleNetImplementation()
	{
		ClientInterface clientInterface = new Network.HSClientInterface();
		ICompressionProvider compressionProvider = new SharpZipCompressionProvider();
		IFileUtil fileUtil = new FileUtil(compressionProvider);
		IJsonSerializer jsonSerializer = new UnityJsonSerializer();
		LoggerInterface loggerInterface = new BattleNetLogger();
		IRpcConnectionFactory rpcConnectionFactory = new RpcConnectionFactory();
		BattleNetCSharp battleNetCSharp = new BattleNetCSharp(clientInterface, rpcConnectionFactory, compressionProvider, fileUtil, jsonSerializer, loggerInterface, TelemetryManager.NetworkComponent);
		battleNetCSharp.OnConnected += Network.OnConnectedToBattleNetCallback;
		battleNetCSharp.OnDisconnected += Network.OnDisconnectedFromBattleNetCallback;
		Debug.LogFormat("*** BattleNet version: Product = {0}, Data = {1}", new object[]
		{
			clientInterface.GetVersion(),
			clientInterface.GetDataVersion()
		});
		return battleNetCSharp;
	}

	// Token: 0x06005475 RID: 21621 RVA: 0x001B9733 File Offset: 0x001B7933
	private static void OnConnectedToBattleNetCallback(BattleNetErrors error)
	{
		if (Network.Get().OnConnectedToBattleNet != null)
		{
			Network.Get().OnConnectedToBattleNet(error);
		}
		TelemetryManager.OnBattleNetConnect(BattleNet.GetEnvironment(), (int)BattleNet.GetPort(), error);
	}

	// Token: 0x06005476 RID: 21622 RVA: 0x001B9761 File Offset: 0x001B7961
	private static void OnDisconnectedFromBattleNetCallback(BattleNetErrors error)
	{
		if (Network.Get().OnDisconnectedFromBattleNet != null)
		{
			Network.Get().OnDisconnectedFromBattleNet(error);
		}
		TelemetryManager.OnBattleNetDisconnect(BattleNet.GetEnvironment(), (int)BattleNet.GetPort(), error);
	}

	// Token: 0x06005477 RID: 21623 RVA: 0x001B978F File Offset: 0x001B798F
	public static bool ShouldBeConnectedToAurora()
	{
		return Network.s_shouldBeConnectedToAurora;
	}

	// Token: 0x06005478 RID: 21624 RVA: 0x001B9796 File Offset: 0x001B7996
	public static void SetShouldBeConnectedToAurora(bool shouldBeConnected)
	{
		Network.s_shouldBeConnectedToAurora = shouldBeConnected;
	}

	// Token: 0x06005479 RID: 21625 RVA: 0x001B978F File Offset: 0x001B798F
	public bool ShouldBeConnectedToAurora_NONSTATIC()
	{
		return Network.s_shouldBeConnectedToAurora;
	}

	// Token: 0x0600547A RID: 21626 RVA: 0x001B979E File Offset: 0x001B799E
	public void SetShouldBeConnectedToAurora_NONSTATIC(bool shouldBeConnected)
	{
		Network.s_shouldBeConnectedToAurora = shouldBeConnected;
	}

	// Token: 0x0600547B RID: 21627 RVA: 0x001B97A8 File Offset: 0x001B79A8
	public void ProcessQueuePosition()
	{
		bgs.types.QueueInfo queueInfo = default(bgs.types.QueueInfo);
		BattleNet.GetQueueInfo(ref queueInfo);
		if (!queueInfo.changed)
		{
			return;
		}
		if (this.m_queueInfoHandler == null)
		{
			return;
		}
		Network.QueueInfo queueInfo2 = new Network.QueueInfo();
		queueInfo2.position = queueInfo.position;
		queueInfo2.secondsTilEnd = queueInfo.end;
		queueInfo2.stdev = queueInfo.stdev;
		this.m_queueInfoHandler(queueInfo2);
	}

	// Token: 0x0600547C RID: 21628 RVA: 0x001B980C File Offset: 0x001B7A0C
	public Network.BnetEventHandler GetBnetEventHandler()
	{
		return this.m_state.CurrentBnetEventHandler;
	}

	// Token: 0x0600547D RID: 21629 RVA: 0x001B9819 File Offset: 0x001B7A19
	public void SetBnetStateHandler(Network.BnetEventHandler handler)
	{
		this.m_state.CurrentBnetEventHandler = handler;
	}

	// Token: 0x0600547E RID: 21630 RVA: 0x001B9827 File Offset: 0x001B7A27
	public Network.FriendsHandler GetFriendsHandler()
	{
		return this.m_state.CurrentFriendsHandler;
	}

	// Token: 0x0600547F RID: 21631 RVA: 0x001B9834 File Offset: 0x001B7A34
	public void SetFriendsHandler(Network.FriendsHandler handler)
	{
		this.m_state.CurrentFriendsHandler = handler;
	}

	// Token: 0x06005480 RID: 21632 RVA: 0x001B9842 File Offset: 0x001B7A42
	public Network.WhisperHandler GetWhisperHandler()
	{
		return this.m_state.CurrentWhisperHandler;
	}

	// Token: 0x06005481 RID: 21633 RVA: 0x001B984F File Offset: 0x001B7A4F
	public void SetWhisperHandler(Network.WhisperHandler handler)
	{
		this.m_state.CurrentWhisperHandler = handler;
	}

	// Token: 0x06005482 RID: 21634 RVA: 0x001B985D File Offset: 0x001B7A5D
	public Network.PresenceHandler GetPresenceHandler()
	{
		return this.m_state.CurrentPresenceHandler;
	}

	// Token: 0x06005483 RID: 21635 RVA: 0x001B986A File Offset: 0x001B7A6A
	public void SetPresenceHandler(Network.PresenceHandler handler)
	{
		this.m_state.CurrentPresenceHandler = handler;
	}

	// Token: 0x06005484 RID: 21636 RVA: 0x001B9878 File Offset: 0x001B7A78
	public Network.ShutdownHandler GetShutdownHandler()
	{
		return this.m_state.CurrentShutdownHandler;
	}

	// Token: 0x06005485 RID: 21637 RVA: 0x001B9885 File Offset: 0x001B7A85
	public void SetShutdownHandler(Network.ShutdownHandler handler)
	{
		this.m_state.CurrentShutdownHandler = handler;
	}

	// Token: 0x06005486 RID: 21638 RVA: 0x001B9893 File Offset: 0x001B7A93
	public Network.ChallengeHandler GetChallengeHandler()
	{
		return this.m_state.CurrentChallengeHandler;
	}

	// Token: 0x06005487 RID: 21639 RVA: 0x001B98A0 File Offset: 0x001B7AA0
	public void SetChallengeHandler(Network.ChallengeHandler handler)
	{
		this.m_state.CurrentChallengeHandler = handler;
	}

	// Token: 0x06005488 RID: 21640 RVA: 0x001B98AE File Offset: 0x001B7AAE
	public void SetGameServerDisconnectEventListener(Network.GameServerDisconnectEvent handler)
	{
		this.m_state.GameServerDisconnectEventListener = handler;
	}

	// Token: 0x06005489 RID: 21641 RVA: 0x001B98BC File Offset: 0x001B7ABC
	public void RemoveGameServerDisconnectEventListener(Network.GameServerDisconnectEvent handler)
	{
		if (this.m_state.GameServerDisconnectEventListener == handler)
		{
			this.m_state.GameServerDisconnectEventListener = null;
		}
	}

	// Token: 0x0600548A RID: 21642 RVA: 0x001B98DD File Offset: 0x001B7ADD
	public void AddBnetErrorListener(BnetFeature feature, Network.BnetErrorCallback callback)
	{
		this.AddBnetErrorListener(feature, callback, null);
	}

	// Token: 0x0600548B RID: 21643 RVA: 0x001B98E8 File Offset: 0x001B7AE8
	public void AddBnetErrorListener(BnetFeature feature, Network.BnetErrorCallback callback, object userData)
	{
		Network.BnetErrorListener bnetErrorListener = new Network.BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		List<Network.BnetErrorListener> list;
		if (!this.m_state.FeatureBnetErrorListeners.TryGetValue(feature, out list))
		{
			list = new List<Network.BnetErrorListener>();
			this.m_state.FeatureBnetErrorListeners.Add(feature, list);
		}
		else if (list.Contains(bnetErrorListener))
		{
			return;
		}
		list.Add(bnetErrorListener);
	}

	// Token: 0x0600548C RID: 21644 RVA: 0x001B9949 File Offset: 0x001B7B49
	public void AddBnetErrorListener(Network.BnetErrorCallback callback)
	{
		this.AddBnetErrorListener(callback, null);
	}

	// Token: 0x0600548D RID: 21645 RVA: 0x001B9954 File Offset: 0x001B7B54
	public void AddBnetErrorListener(Network.BnetErrorCallback callback, object userData)
	{
		Network.BnetErrorListener bnetErrorListener = new Network.BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		if (this.m_state.GlobalBnetErrorListeners.Contains(bnetErrorListener))
		{
			return;
		}
		this.m_state.GlobalBnetErrorListeners.Add(bnetErrorListener);
	}

	// Token: 0x0600548E RID: 21646 RVA: 0x001B999A File Offset: 0x001B7B9A
	public bool RemoveBnetErrorListener(BnetFeature feature, Network.BnetErrorCallback callback)
	{
		return this.RemoveBnetErrorListener(feature, callback, null);
	}

	// Token: 0x0600548F RID: 21647 RVA: 0x001B99A8 File Offset: 0x001B7BA8
	public bool RemoveBnetErrorListener(BnetFeature feature, Network.BnetErrorCallback callback, object userData)
	{
		List<Network.BnetErrorListener> list;
		if (!this.m_state.FeatureBnetErrorListeners.TryGetValue(feature, out list))
		{
			return false;
		}
		Network.BnetErrorListener bnetErrorListener = new Network.BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		return list.Remove(bnetErrorListener);
	}

	// Token: 0x06005490 RID: 21648 RVA: 0x001B99E7 File Offset: 0x001B7BE7
	public bool RemoveBnetErrorListener(Network.BnetErrorCallback callback)
	{
		return this.RemoveBnetErrorListener(callback, null);
	}

	// Token: 0x06005491 RID: 21649 RVA: 0x001B99F4 File Offset: 0x001B7BF4
	public bool RemoveBnetErrorListener(Network.BnetErrorCallback callback, object userData)
	{
		Network.BnetErrorListener bnetErrorListener = new Network.BnetErrorListener();
		bnetErrorListener.SetCallback(callback);
		bnetErrorListener.SetUserData(userData);
		return this.m_state.GlobalBnetErrorListeners.Remove(bnetErrorListener);
	}

	// Token: 0x06005492 RID: 21650 RVA: 0x001B9A26 File Offset: 0x001B7C26
	public void SendUnsubcribeRequest(Unsubscribe packet, UtilSystemId systemChannel)
	{
		this.m_connectApi.SendUnsubscribeRequest(packet, systemChannel);
	}

	// Token: 0x06005493 RID: 21651 RVA: 0x001B9A38 File Offset: 0x001B7C38
	public void ProcessAurora()
	{
		BattleNet.ProcessAurora();
		this.ProcessBnetEvents();
		if (Network.IsLoggedIn())
		{
			this.ProcessPresence();
			this.ProcessFriends();
			this.ProcessWhispers();
			this.ProcessParties();
			this.ProcessBroadcasts();
			this.ProcessNotifications();
			BnetNearbyPlayerMgr.Get().Update();
		}
		this.ProcessErrors();
	}

	// Token: 0x06005494 RID: 21652 RVA: 0x001B9A8C File Offset: 0x001B7C8C
	private void ProcessBnetEvents()
	{
		int bnetEventsSize = BattleNet.GetBnetEventsSize();
		if (bnetEventsSize <= 0)
		{
			return;
		}
		if (this.m_state.CurrentBnetEventHandler == null)
		{
			return;
		}
		BattleNet.BnetEvent[] array = new BattleNet.BnetEvent[bnetEventsSize];
		BattleNet.GetBnetEvents(array);
		this.m_state.CurrentBnetEventHandler(array);
		BattleNet.ClearBnetEvents();
	}

	// Token: 0x06005495 RID: 21653 RVA: 0x001B9AD8 File Offset: 0x001B7CD8
	private void ProcessWhispers()
	{
		WhisperInfo whisperInfo = default(WhisperInfo);
		BattleNet.GetWhisperInfo(ref whisperInfo);
		if (whisperInfo.whisperSize <= 0)
		{
			return;
		}
		if (this.m_state.CurrentWhisperHandler == null)
		{
			return;
		}
		BnetWhisper[] whispers = new BnetWhisper[whisperInfo.whisperSize];
		BattleNet.GetWhispers(whispers);
		this.m_state.CurrentWhisperHandler(whispers);
		BattleNet.ClearWhispers();
	}

	// Token: 0x06005496 RID: 21654 RVA: 0x001B9B34 File Offset: 0x001B7D34
	private void ProcessParties()
	{
		BnetParty.Process();
	}

	// Token: 0x06005497 RID: 21655 RVA: 0x001B9B3C File Offset: 0x001B7D3C
	private void ProcessBroadcasts()
	{
		int shutdownMinutes = BattleNet.GetShutdownMinutes();
		if (shutdownMinutes > 0)
		{
			if (this.m_state.CurrentShutdownHandler == null)
			{
				return;
			}
			this.m_state.CurrentShutdownHandler(shutdownMinutes);
		}
	}

	// Token: 0x06005498 RID: 21656 RVA: 0x001B9B74 File Offset: 0x001B7D74
	private void ProcessNotifications()
	{
		int notificationCount = BattleNet.GetNotificationCount();
		if (notificationCount <= 0)
		{
			return;
		}
		BnetNotification[] array = new BnetNotification[notificationCount];
		BattleNet.GetNotifications(array);
		BattleNet.ClearNotifications();
		foreach (BnetNotification bnetNotification in array)
		{
			string notificationType = bnetNotification.NotificationType;
			if (notificationType == "WTCG.UtilNotificationMessage")
			{
				PegasusPacket packet = new PegasusPacket(bnetNotification.MessageType, 0, bnetNotification.MessageSize, bnetNotification.BlobMessage);
				this.m_connectApi.DecodeAndProcessPacket(packet);
			}
		}
	}

	// Token: 0x06005499 RID: 21657 RVA: 0x001B9BF4 File Offset: 0x001B7DF4
	private void ProcessFriends()
	{
		FriendsInfo friendsInfo = default(FriendsInfo);
		BattleNet.GetFriendsInfo(ref friendsInfo);
		if (friendsInfo.updateSize == 0)
		{
			return;
		}
		if (this.m_state.CurrentFriendsHandler == null)
		{
			return;
		}
		FriendsUpdate[] updates = new FriendsUpdate[friendsInfo.updateSize];
		BattleNet.GetFriendsUpdates(updates);
		this.m_state.CurrentFriendsHandler(updates);
		BattleNet.ClearFriendsUpdates();
	}

	// Token: 0x0600549A RID: 21658 RVA: 0x001B9C50 File Offset: 0x001B7E50
	private void ProcessPresence()
	{
		int num = BattleNet.PresenceSize();
		if (num == 0)
		{
			return;
		}
		if (this.m_state.CurrentPresenceHandler == null)
		{
			return;
		}
		PresenceUpdate[] updates = new PresenceUpdate[num];
		BattleNet.GetPresence(updates);
		this.m_state.CurrentPresenceHandler(updates);
		BattleNet.ClearPresence();
	}

	// Token: 0x0600549B RID: 21659 RVA: 0x001B9C98 File Offset: 0x001B7E98
	private void ProcessErrors()
	{
		this.ProcessDelayedError();
		BnetErrorInfo[] array;
		if (this.m_connectApi.HasErrors())
		{
			BnetErrorInfo bnetErrorInfo = new BnetErrorInfo(BnetFeature.Games, BnetFeatureEvent.Games_OnClientRequest, BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER);
			array = new BnetErrorInfo[]
			{
				bnetErrorInfo
			};
		}
		else
		{
			int errorsCount = BattleNet.GetErrorsCount();
			if (errorsCount == 0)
			{
				return;
			}
			array = new BnetErrorInfo[errorsCount];
			BattleNet.GetErrors(array);
		}
		foreach (BnetErrorInfo bnetErrorInfo2 in array)
		{
			BattleNetErrors error = bnetErrorInfo2.GetError();
			if (error == (BattleNetErrors)1003013U)
			{
				BattleNet.ClearErrors();
				HearthstoneApplication.Get().Reset();
				return;
			}
			string text = HearthstoneApplication.IsPublic() ? "" : error.ToString();
			if (!this.m_connectApi.HasErrors() && this.m_connectApi.ShouldIgnoreError(bnetErrorInfo2))
			{
				if (!HearthstoneApplication.IsPublic())
				{
					global::Log.BattleNet.PrintDebug("BattleNet/ConnectDLL generated error={0} {1} (can ignore)", new object[]
					{
						(int)error,
						text
					});
				}
			}
			else if (!this.FireErrorListeners(bnetErrorInfo2) && (this.m_connectApi.HasErrors() || !this.OnIgnorableBnetError(bnetErrorInfo2)))
			{
				this.OnFatalBnetError(bnetErrorInfo2);
			}
		}
		BattleNet.ClearErrors();
	}

	// Token: 0x0600549C RID: 21660 RVA: 0x001B9DBC File Offset: 0x001B7FBC
	private bool FireErrorListeners(BnetErrorInfo info)
	{
		bool flag = false;
		List<Network.BnetErrorListener> list;
		if (this.m_state.FeatureBnetErrorListeners.TryGetValue(info.GetFeature(), out list) && list.Count > 0)
		{
			Network.BnetErrorListener[] array = list.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				flag = (array[i].Fire(info) || flag);
			}
		}
		Network.BnetErrorListener[] array2 = this.m_state.GlobalBnetErrorListeners.ToArray();
		for (int j = 0; j < array2.Length; j++)
		{
			flag = (array2[j].Fire(info) || flag);
		}
		return flag;
	}

	// Token: 0x0600549D RID: 21661 RVA: 0x001B9E48 File Offset: 0x001B8048
	public void ShowConnectionFailureError(string error)
	{
		int numConnectionFailures = this.m_numConnectionFailures;
		this.m_numConnectionFailures = numConnectionFailures + 1;
		this.ShowBreakingNewsOrError(error, this.DelayForConnectionFailures(numConnectionFailures));
	}

	// Token: 0x0600549E RID: 21662 RVA: 0x001B9E73 File Offset: 0x001B8073
	public void ShowBreakingNewsOrError(string error, float timeBeforeAllowReset = 0f)
	{
		this.m_state.DelayedError = error;
		this.m_state.TimeBeforeAllowReset = timeBeforeAllowReset;
		Debug.LogError(string.Format("Setting delayed error for Error Message: {0} and prevent reset for {1} seconds", error, timeBeforeAllowReset));
		this.ProcessDelayedError();
	}

	// Token: 0x0600549F RID: 21663 RVA: 0x001B9EAC File Offset: 0x001B80AC
	private bool ProcessDelayedError()
	{
		if (this.m_state.DelayedError == null)
		{
			return false;
		}
		bool result = false;
		if (BreakingNews.Get().GetStatus() > BreakingNews.Status.Fetching)
		{
			ErrorParams errorParams = new ErrorParams();
			errorParams.m_delayBeforeNextReset = this.m_state.TimeBeforeAllowReset;
			string text = BreakingNews.Get().GetText();
			if (string.IsNullOrEmpty(text))
			{
				if (BreakingNews.Get().GetError() != null && this.m_state.DelayedError == "GLOBAL_ERROR_NETWORK_NO_GAME_SERVER")
				{
					errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_NO_CONNECTION", Array.Empty<object>());
				}
				else if (HearthstoneApplication.IsInternal() && this.m_state.DelayedError == "GLOBAL_ERROR_UNKNOWN_ERROR")
				{
					errorParams.m_message = "Dev Message: Could not connect to Battle.net, and there was no breaking news to display. Maybe Battle.net is down?";
				}
				else
				{
					errorParams.m_message = GameStrings.Format(this.m_state.DelayedError, Array.Empty<object>());
				}
			}
			else
			{
				errorParams.m_message = GameStrings.Format("GLOBAL_MOBILE_ERROR_BREAKING_NEWS", new object[]
				{
					text
				});
				errorParams.m_reason = FatalErrorReason.BREAKING_NEWS;
			}
			global::Error.AddFatal(errorParams);
			this.m_state.DelayedError = null;
			this.m_state.TimeBeforeAllowReset = 0f;
			result = true;
		}
		return result;
	}

	// Token: 0x060054A0 RID: 21664 RVA: 0x001B9FD4 File Offset: 0x001B81D4
	public bool OnIgnorableBnetError(BnetErrorInfo info)
	{
		BattleNetErrors error = info.GetError();
		bool flag = false;
		if (error <= BattleNetErrors.ERROR_INCOMPLETE_PROFANITY_FILTERS)
		{
			if (error <= BattleNetErrors.ERROR_WAITING_FOR_DEPENDENCY)
			{
				if (error != BattleNetErrors.ERROR_OK)
				{
					if (error == BattleNetErrors.ERROR_WAITING_FOR_DEPENDENCY)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (error != BattleNetErrors.ERROR_INVALID_TARGET_ID)
			{
				if (error != BattleNetErrors.ERROR_API_NOT_READY)
				{
					if (error == BattleNetErrors.ERROR_INCOMPLETE_PROFANITY_FILTERS)
					{
						Locale locale = Localization.GetLocale();
						if (locale == Locale.zhCN)
						{
							this.m_state.LogSource.LogError("Network.IgnoreBnetError() - error={0} locale={1}", new object[]
							{
								info,
								locale
							});
						}
						flag = true;
					}
				}
				else
				{
					flag = (info.GetFeature() == BnetFeature.Presence);
				}
			}
			else
			{
				flag = (info.GetFeature() == BnetFeature.Friends && info.GetFeatureEvent() == BnetFeatureEvent.Friends_OnSendInvitation);
			}
		}
		else
		{
			if (error <= BattleNetErrors.ERROR_FRIENDS_FRIENDSHIP_ALREADY_EXISTS)
			{
				if (error == BattleNetErrors.ERROR_TARGET_OFFLINE)
				{
					flag = true;
					goto IL_121;
				}
				if (error == BattleNetErrors.ERROR_PRESENCE_TEMPORARY_OUTAGE)
				{
					flag = true;
					goto IL_121;
				}
				if (error != BattleNetErrors.ERROR_FRIENDS_FRIENDSHIP_ALREADY_EXISTS)
				{
					goto IL_121;
				}
			}
			else if (error != BattleNetErrors.ERROR_FRIENDS_INVITATION_ALREADY_EXISTS && error - BattleNetErrors.ERROR_FRIENDS_INVITEE_AT_MAX_FRIENDS > 2U)
			{
				if (error != BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER)
				{
					goto IL_121;
				}
				this.m_state.LogSource.LogError("Network.IgnoreBnetError() - error={0}", new object[]
				{
					info
				});
				flag = true;
				goto IL_121;
			}
			flag = true;
		}
		IL_121:
		if (error > BattleNetErrors.ERROR_OK && flag)
		{
			TelemetryManager.Client().SendIgnorableBattleNetError((int)error, error.ToString());
		}
		return flag;
	}

	// Token: 0x060054A1 RID: 21665 RVA: 0x001BA124 File Offset: 0x001B8324
	public void OnFatalBnetError(BnetErrorInfo info)
	{
		BattleNetErrors error = info.GetError();
		this.m_state.LogSource.LogError("Network.OnFatalBnetError() - error={0}", new object[]
		{
			info
		});
		TelemetryManager.Client().SendFatalBattleNetError((int)error, error.ToString());
		if (error > BattleNetErrors.ERROR_SESSION_DUPLICATE)
		{
			if (error <= BattleNetErrors.ERROR_LOGON_WEB_VERIFY_TIMEOUT)
			{
				if (error <= BattleNetErrors.ERROR_ADMIN_KICK)
				{
					if (error == BattleNetErrors.ERROR_SESSION_DISCONNECTED)
					{
						string text = "GLOBAL_ERROR_NETWORK_DISCONNECT";
						global::Error.AddFatal(FatalErrorReason.BNET_NETWORK_DISCONNECT, text, Array.Empty<object>());
						return;
					}
					if (error != BattleNetErrors.ERROR_ADMIN_KICK)
					{
						goto IL_3CE;
					}
				}
				else
				{
					if (error == BattleNetErrors.ERROR_BATTLENET_ACCOUNT_BANNED)
					{
						goto IL_1EA;
					}
					if (error != BattleNetErrors.ERROR_LOGON_WEB_VERIFY_TIMEOUT)
					{
						goto IL_3CE;
					}
					this.ShowConnectionFailureError("GLOBAL_MOBILE_ERROR_LOGON_WEB_TIMEOUT");
					return;
				}
			}
			else if (error <= BattleNetErrors.ERROR_RPC_QUOTA_EXCEEDED)
			{
				switch (error)
				{
				case BattleNetErrors.ERROR_RPC_PEER_UNKNOWN:
					goto IL_3CE;
				case BattleNetErrors.ERROR_RPC_PEER_UNAVAILABLE:
					TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.PEER_UNAVAILABLE, info.ToString(), 3004);
					this.ShowConnectionFailureError("GLOBAL_ERROR_UNKNOWN_ERROR");
					global::Log.Net.PrintWarning("ERROR_RPC_PEER_UNAVAILABLE - {0} connection failures.", new object[]
					{
						this.m_numConnectionFailures
					});
					return;
				case BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED:
				{
					string text = "GLOBAL_ERROR_NETWORK_DISCONNECT";
					this.ShowConnectionFailureError(text);
					return;
				}
				case BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT:
				{
					string text = "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT";
					this.ShowConnectionFailureError(text);
					return;
				}
				case BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT:
					this.ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_CONNECTION_TIMEOUT");
					return;
				default:
				{
					if (error != BattleNetErrors.ERROR_RPC_QUOTA_EXCEEDED)
					{
						goto IL_3CE;
					}
					string text = "GLOBAL_ERROR_NETWORK_SPAM";
					global::Error.AddFatal(FatalErrorReason.BNET_NETWORK_SPAM, text, Array.Empty<object>());
					return;
				}
				}
			}
			else if (error != BattleNetErrors.ERROR_SESSION_ADMIN_KICK)
			{
				switch (error)
				{
				case BattleNetErrors.ERROR_SESSION_CAIS_PLAYTIME_EXCEEDED:
				{
					ILoginService loginService = HearthstoneServices.Get<ILoginService>();
					if (loginService != null)
					{
						loginService.ClearAuthentication();
					}
					global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_PLAYTIME_EXCEEDED", Array.Empty<object>());
					return;
				}
				case BattleNetErrors.ERROR_SESSION_CAIS_CURFEW:
				{
					ILoginService loginService2 = HearthstoneServices.Get<ILoginService>();
					if (loginService2 != null)
					{
						loginService2.ClearAuthentication();
					}
					global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_CURFEW_REACHED", Array.Empty<object>());
					return;
				}
				case (BattleNetErrors)40013U:
					goto IL_3CE;
				case BattleNetErrors.ERROR_SESSION_INVALID_NID:
				{
					ILoginService loginService3 = HearthstoneServices.Get<ILoginService>();
					if (loginService3 != null)
					{
						loginService3.ClearAuthentication();
					}
					global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_INVALID_NID", Array.Empty<object>());
					return;
				}
				default:
				{
					if (error != BattleNetErrors.ERROR_RISK_ACCOUNT_LOCKED)
					{
						goto IL_3CE;
					}
					ILoginService loginService4 = HearthstoneServices.Get<ILoginService>();
					if (loginService4 != null)
					{
						loginService4.ClearAuthentication();
					}
					global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_RISK_ACCOUNT_LOCKED", Array.Empty<object>());
					return;
				}
				}
			}
			global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ADMIN_KICKED", Array.Empty<object>());
			return;
		}
		if (error <= BattleNetErrors.ERROR_SERVER_IS_PRIVATE)
		{
			if (error <= BattleNetErrors.ERROR_PARENTAL_CONTROL_RESTRICTION)
			{
				if (error == BattleNetErrors.ERROR_DENIED)
				{
					this.ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
					return;
				}
				if (error != BattleNetErrors.ERROR_PARENTAL_CONTROL_RESTRICTION)
				{
					goto IL_3CE;
				}
				ILoginService loginService5 = HearthstoneServices.Get<ILoginService>();
				if (loginService5 != null)
				{
					loginService5.ClearAuthentication();
				}
				global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_PARENTAL_CONTROLS", Array.Empty<object>());
				return;
			}
			else
			{
				if (error == BattleNetErrors.ERROR_BAD_VERSION)
				{
					if (PlatformSettings.IsMobile() && GameDownloadManagerProvider.Get() != null && !GameDownloadManagerProvider.Get().IsNewMobileVersionReleased)
					{
						global::Error.AddFatal(FatalErrorReason.UNAVAILABLE_NEW_VERSION, "GLOBAL_ERROR_NETWORK_UNAVAILABLE_NEW_VERSION", Array.Empty<object>());
					}
					else
					{
						global::Error.AddFatal(new ErrorParams
						{
							m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UPGRADE", Array.Empty<object>()),
							m_redirectToStore = global::Error.HAS_APP_STORE,
							m_reason = FatalErrorReason.UNAVAILABLE_UPGRADE
						});
					}
					ReconnectMgr.Get().FullResetRequired = true;
					ReconnectMgr.Get().UpdateRequired = true;
					return;
				}
				if (error != BattleNetErrors.ERROR_SERVER_IS_PRIVATE)
				{
					goto IL_3CE;
				}
				TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.PRIVATE_SERVER, info.ToString(), 33);
				this.ShowConnectionFailureError("GLOBAL_ERROR_UNKNOWN_ERROR");
				global::Log.Net.PrintWarning("ERROR_SERVER_IS_PRIVATE - {0} connection failures.", new object[]
				{
					this.m_numConnectionFailures
				});
				return;
			}
		}
		else if (error <= BattleNetErrors.ERROR_GAME_ACCOUNT_BANNED)
		{
			if (error == BattleNetErrors.ERROR_PHONE_LOCK)
			{
				global::Error.AddFatal(FatalErrorReason.BNET_PHONE_LOCK, "GLOBAL_ERROR_NETWORK_PHONE_LOCK", Array.Empty<object>());
				return;
			}
			if (error != BattleNetErrors.ERROR_GAME_ACCOUNT_BANNED)
			{
				goto IL_3CE;
			}
		}
		else
		{
			if (error == BattleNetErrors.ERROR_GAME_ACCOUNT_SUSPENDED)
			{
				ILoginService loginService6 = HearthstoneServices.Get<ILoginService>();
				if (loginService6 != null)
				{
					loginService6.ClearAuthentication();
				}
				global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_SUSPENDED", Array.Empty<object>());
				return;
			}
			if (error != BattleNetErrors.ERROR_SESSION_DUPLICATE)
			{
				goto IL_3CE;
			}
			global::Error.AddFatal(FatalErrorReason.LOGIN_FROM_ANOTHER_DEVICE, "GLOBAL_ERROR_NETWORK_DUPLICATE_LOGIN", Array.Empty<object>());
			return;
		}
		IL_1EA:
		ILoginService loginService7 = HearthstoneServices.Get<ILoginService>();
		if (loginService7 != null)
		{
			loginService7.ClearAuthentication();
		}
		global::Error.AddFatal(FatalErrorReason.ADMIN_KICK_OR_BAN, "GLOBAL_ERROR_NETWORK_ACCOUNT_BANNED", Array.Empty<object>());
		return;
		IL_3CE:
		string error2;
		if (HearthstoneApplication.IsInternal())
		{
			error2 = string.Format("Unhandled Bnet Error: {0}", info);
		}
		else
		{
			Debug.LogError(string.Format("Unhandled Bnet Error: {0}", info));
			error2 = GameStrings.Format("GLOBAL_ERROR_UNKNOWN_ERROR", Array.Empty<object>());
		}
		TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.OTHER_UNKNOWN, info.ToString(), (int)info.GetError());
		this.ShowConnectionFailureError(error2);
	}

	// Token: 0x060054A2 RID: 21666 RVA: 0x001BA554 File Offset: 0x001B8754
	private float DelayForConnectionFailures(int numFailures)
	{
		float num = (float)(new System.Random().NextDouble() * 3.0) + 3.5f;
		return (float)Math.Min(numFailures, 3) * num;
	}

	// Token: 0x060054A3 RID: 21667 RVA: 0x001BA587 File Offset: 0x001B8787
	public void EnsureSubscribedTo(UtilSystemId systemChannel)
	{
		this.m_connectApi.EnsureSubscribedTo(systemChannel);
	}

	// Token: 0x060054A4 RID: 21668 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private bool OnBnetAuthError(BnetErrorInfo info, object userData)
	{
		return false;
	}

	// Token: 0x060054A5 RID: 21669 RVA: 0x001BA595 File Offset: 0x001B8795
	public static void AcceptFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(1, inviteid.GetVal());
	}

	// Token: 0x060054A6 RID: 21670 RVA: 0x001BA5A3 File Offset: 0x001B87A3
	public static void RevokeFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(2, inviteid.GetVal());
	}

	// Token: 0x060054A7 RID: 21671 RVA: 0x001BA5B1 File Offset: 0x001B87B1
	public static void DeclineFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(3, inviteid.GetVal());
	}

	// Token: 0x060054A8 RID: 21672 RVA: 0x001BA5BF File Offset: 0x001B87BF
	public static void IgnoreFriendInvite(BnetInvitationId inviteid)
	{
		BattleNet.ManageFriendInvite(4, inviteid.GetVal());
	}

	// Token: 0x060054A9 RID: 21673 RVA: 0x001BA5CD File Offset: 0x001B87CD
	private static void SendFriendInvite(string sender, string target, bool byEmail)
	{
		BattleNet.SendFriendInvite(sender, target, byEmail);
	}

	// Token: 0x060054AA RID: 21674 RVA: 0x001BA5D7 File Offset: 0x001B87D7
	public static void SendFriendInviteByEmail(string sender, string target)
	{
		Network.SendFriendInvite(sender, target, true);
	}

	// Token: 0x060054AB RID: 21675 RVA: 0x001BA5E1 File Offset: 0x001B87E1
	public static void SendFriendInviteByBattleTag(string sender, string target)
	{
		Network.SendFriendInvite(sender, target, false);
	}

	// Token: 0x060054AC RID: 21676 RVA: 0x001BA5EB File Offset: 0x001B87EB
	public static void RemoveFriend(BnetAccountId id)
	{
		BattleNet.RemoveFriend(id);
	}

	// Token: 0x060054AD RID: 21677 RVA: 0x001BA5F3 File Offset: 0x001B87F3
	public static void GetAccountState(BnetAccountId bnetAccount)
	{
		BattleNet.GetAccountState(bnetAccount);
	}

	// Token: 0x060054AE RID: 21678 RVA: 0x001BA5FB File Offset: 0x001B87FB
	public static void SendWhisper(BnetGameAccountId gameAccount, string message)
	{
		BattleNet.SendWhisper(gameAccount, message);
	}

	// Token: 0x060054AF RID: 21679 RVA: 0x001BA604 File Offset: 0x001B8804
	public void GotoGameServer(GameServerInfo info, bool reconnecting)
	{
		this.m_state.LastGameServerInfo = info;
		if (this.m_connectApi.GetGameStartState() != GameStartState.Invalid && !ReconnectMgr.Get().IsRestoringGameStateFromDatabase())
		{
			global::Error.AddDevFatal("GotoGameServer() was called when we're already waiting for a game to start.", Array.Empty<object>());
			return;
		}
		string address = info.Address;
		uint @uint = Vars.Key("Application.GameServerPortOverride").GetUInt(info.Port);
		Debug.LogFormat(string.Concat(new object[]
		{
			"Network.GotoGameServer -- address= ",
			address,
			":",
			@uint,
			", game=",
			info.GameHandle,
			", client=",
			info.ClientHandle,
			", spectateKey=",
			info.SpectatorPassword,
			" reconnecting=",
			reconnecting.ToString()
		}), Array.Empty<object>());
		if (address == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(address) || @uint == 0U || (info.GameHandle == 0U && Network.ShouldBeConnectedToAurora()))
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"Network.GotoGameServer: ERROR in ServerInfo address= ",
				address,
				":",
				@uint,
				",    game=",
				info.GameHandle,
				", client=",
				info.ClientHandle,
				" reconnecting=",
				reconnecting.ToString()
			}));
		}
		this.m_gameServerKeepAliveFrequencySeconds = 0U;
		this.m_gameServerKeepAliveRetry = 3U;
		this.m_gameConceded = false;
		this.m_disconnectRequested = false;
		this.m_connectApi.SetTimeLastPingSent(0.0);
		this.m_connectApi.SetTimeLastPingReceived(0.0);
		this.m_connectApi.SetPingsSinceLastPong(0);
		if (Network.GameServerDisconnectEvents != null)
		{
			Network.GameServerDisconnectEvents.Clear();
		}
		this.m_state.LastConnectToGameServerInfo = new ConnectToGameServer();
		this.m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds = (long)global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds;
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo = new GameSessionInfo();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.GameServerIpAddress = info.Address;
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.GameServerPort = info.Port;
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.Version = info.Version;
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.GameHandle = info.GameHandle;
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.ScenarioId = GameMgr.Get().GetNextMissionId();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)GameMgr.Get().GetNextGameType();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)GameMgr.Get().GetNextFormatType();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.IsReconnect = GameMgr.Get().IsNextReconnect();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.IsSpectating = GameMgr.Get().IsNextSpectator();
		this.m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientHandle = info.ClientHandle;
		if (GameMgr.Get().LastDeckId != null)
		{
			this.m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientDeckId = GameMgr.Get().LastDeckId.Value;
		}
		if (GameMgr.Get().LastHeroCardDbId != null)
		{
			this.m_state.LastConnectToGameServerInfo.GameSessionInfo.ClientHeroCardId = (long)GameMgr.Get().LastHeroCardDbId.Value;
		}
		if (!this.m_connectApi.GotoGameServer(address, @uint))
		{
			return;
		}
		this.SendGameServerHandshake(info);
		this.m_connectApi.SetGameStartState(reconnecting ? GameStartState.Reconnecting : GameStartState.InitialStart);
	}

	// Token: 0x060054B0 RID: 21680 RVA: 0x001BA9E8 File Offset: 0x001B8BE8
	private void OnGameServerConnectEvent(BattleNetErrors error)
	{
		global::Log.GameMgr.Print("Connecting to game server with error code " + error, Array.Empty<object>());
		if (this.m_state.LastConnectToGameServerInfo != null)
		{
			long timeSpentMilliseconds = (long)(global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds - (double)this.m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds);
			this.m_state.LastConnectToGameServerInfo.ResultBnetCode = (uint)error;
			this.m_state.LastConnectToGameServerInfo.ResultBnetCodeString = error.ToString();
			this.m_state.LastConnectToGameServerInfo.TimeSpentMilliseconds = timeSpentMilliseconds;
			TelemetryManager.Client().SendConnectToGameServer(this.m_state.LastConnectToGameServerInfo);
			this.m_state.LastConnectToGameServerInfo = null;
		}
		GameServerInfo lastGameServerJoined = this.GetLastGameServerJoined();
		if (error == BattleNetErrors.ERROR_OK)
		{
			TelemetryManager.Client().SendConnectSuccess("GAME", (lastGameServerJoined == null) ? null : lastGameServerJoined.Address, (lastGameServerJoined == null) ? null : new uint?(lastGameServerJoined.Port));
			TelemetryManager.RegisterShutdownListener(new Action(this.SendDefaultDisconnectTelemetry));
			return;
		}
		TelemetryManager.Client().SendConnectFail("GAME", error.ToString(), (lastGameServerJoined == null) ? null : lastGameServerJoined.Address, (lastGameServerJoined == null) ? null : new uint?(lastGameServerJoined.Port));
		GameStartState gameStartState = this.m_connectApi.GetGameStartState();
		this.m_connectApi.SetGameStartState(GameStartState.Invalid);
		if (Network.ShouldBeConnectedToAurora())
		{
			if (gameStartState != GameStartState.Reconnecting)
			{
				this.ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER", 0f);
				Debug.LogError("Failed to connect to game server with error " + error);
				return;
			}
		}
		else
		{
			this.ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER", 0f);
			Debug.LogError("Failed to connect to game server with error " + error);
		}
	}

	// Token: 0x060054B1 RID: 21681 RVA: 0x001BABB4 File Offset: 0x001B8DB4
	private void OnGameServerDisconnectEvent(BattleNetErrors error)
	{
		global::Log.GameMgr.Print("Disconnected from game server with error {0} {1}", new object[]
		{
			(int)error,
			error.ToString()
		});
		TelemetryManager.UnregisterShutdownListener(new Action(this.SendDefaultDisconnectTelemetry));
		GameServerInfo lastGameServerJoined = this.GetLastGameServerJoined();
		TelemetryManager.Client().SendDisconnect("GAME", TelemetryUtil.GetReasonFromBnetError(error), (error == BattleNetErrors.ERROR_OK) ? null : error.ToString(), (lastGameServerJoined == null) ? null : lastGameServerJoined.Address, (lastGameServerJoined == null) ? null : new uint?(lastGameServerJoined.Port));
		this.m_state.LastConnectToGameServerInfo = null;
		bool flag = false;
		if (error != BattleNetErrors.ERROR_OK)
		{
			GameStartState gameStartState = this.m_connectApi.GetGameStartState();
			if (gameStartState == GameStartState.Reconnecting)
			{
				flag = true;
			}
			else if (gameStartState == GameStartState.InitialStart && (lastGameServerJoined == null || !lastGameServerJoined.SpectatorMode))
			{
				Debug.LogError("Disconnected from game server with error " + error);
				this.AddErrorToList(new Network.ConnectErrorParams
				{
					m_message = GameStrings.Format((error == BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT) ? "GLOBAL_ERROR_NETWORK_CONNECTION_TIMEOUT" : "GLOBAL_ERROR_NETWORK_DISCONNECT_GAME_SERVER", Array.Empty<object>())
				});
				flag = true;
			}
			this.m_connectApi.SetGameStartState(GameStartState.Invalid);
		}
		if (!flag)
		{
			this.AddGameServerDisconnectEvent(error);
		}
	}

	// Token: 0x060054B2 RID: 21682 RVA: 0x001BACEC File Offset: 0x001B8EEC
	private void SendDefaultDisconnectTelemetry()
	{
		GameServerInfo lastGameServerJoined = this.GetLastGameServerJoined();
		TelemetryManager.Client().SendDisconnect("GAME", TelemetryUtil.GetReasonFromBnetError(BattleNetErrors.ERROR_OK), null, (lastGameServerJoined == null) ? null : lastGameServerJoined.Address, (lastGameServerJoined == null) ? null : new uint?(lastGameServerJoined.Port));
	}

	// Token: 0x060054B3 RID: 21683 RVA: 0x001BAD3B File Offset: 0x001B8F3B
	private void AddGameServerDisconnectEvent(BattleNetErrors error)
	{
		if (Network.GameServerDisconnectEvents == null)
		{
			Network.GameServerDisconnectEvents = new List<BattleNetErrors>();
		}
		Network.GameServerDisconnectEvents.Add(error);
	}

	// Token: 0x060054B4 RID: 21684 RVA: 0x001BAD59 File Offset: 0x001B8F59
	public void SpectateSecondPlayer(GameServerInfo info)
	{
		info.SpectatorMode = true;
		if (!this.IsConnectedToGameServer())
		{
			this.GotoGameServer(info, false);
			return;
		}
		this.SendGameServerHandshake(info);
	}

	// Token: 0x060054B5 RID: 21685 RVA: 0x001BAD7A File Offset: 0x001B8F7A
	public bool RetryGotoGameServer()
	{
		if (this.m_connectApi.GetGameStartState() == GameStartState.Invalid)
		{
			return false;
		}
		this.SendGameServerHandshake(this.m_state.LastGameServerInfo);
		return true;
	}

	// Token: 0x060054B6 RID: 21686 RVA: 0x001BAD9D File Offset: 0x001B8F9D
	public GameServerInfo GetLastGameServerJoined()
	{
		return this.m_state.LastGameServerInfo;
	}

	// Token: 0x060054B7 RID: 21687 RVA: 0x001BADAA File Offset: 0x001B8FAA
	public void ClearLastGameServerJoined()
	{
		this.m_state.LastGameServerInfo = null;
	}

	// Token: 0x060054B8 RID: 21688 RVA: 0x001BADB8 File Offset: 0x001B8FB8
	public static string GetUsername()
	{
		string text = null;
		try
		{
			text = Network.GetStoredUserName();
		}
		catch (Exception ex)
		{
			Debug.LogError("Exception while loading settings: " + ex.Message);
		}
		if (text == null)
		{
			text = Vars.Key("Aurora.Username").GetStr("NOT_PROVIDED_PLEASE_PROVIDE_VIA_CONFIG");
		}
		if (text != null && text.IndexOf("@") == -1)
		{
			text += "@blizzard.com";
		}
		return text;
	}

	// Token: 0x060054B9 RID: 21689 RVA: 0x001BAE30 File Offset: 0x001B9030
	public static string GetTargetServer()
	{
		bool flag = Vars.Key("Aurora.Env.Override").GetInt(0) != 0;
		string text = "default";
		string text2 = null;
		if (flag)
		{
			text2 = Vars.Key("Aurora.Env").GetStr(text);
			if (string.IsNullOrEmpty(text2))
			{
				text2 = null;
			}
		}
		if (text2 == null)
		{
			text2 = BattleNet.GetConnectionString();
		}
		if (text2 == null)
		{
			string launchOption = BattleNet.GetLaunchOption("REGION", false);
			if (!string.IsNullOrEmpty(launchOption))
			{
				if (!(launchOption == "US"))
				{
					if (!(launchOption == "XX"))
					{
						if (!(launchOption == "EU"))
						{
							if (!(launchOption == "CN"))
							{
								if (!(launchOption == "KR"))
								{
									text2 = text;
								}
								else
								{
									text2 = "kr.actual.battle.net";
								}
							}
							else
							{
								text2 = "cn.actual.battle.net";
							}
						}
						else
						{
							text2 = "eu.actual.battle.net";
						}
					}
					else
					{
						text2 = "beta.actual.battle.net";
					}
				}
				else
				{
					text2 = "us.actual.battle.net";
				}
			}
		}
		if (text2.ToLower() == text)
		{
			text2 = "bn11-01.battle.net";
		}
		return text2;
	}

	// Token: 0x060054BA RID: 21690 RVA: 0x001BAF1C File Offset: 0x001B911C
	public static uint GetPort()
	{
		uint num = 0U;
		if (Vars.Key("Aurora.Env.Override").GetUInt(0U) > 0U)
		{
			num = Vars.Key("Aurora.Port").GetUInt(0U);
		}
		if (num == 0U)
		{
			num = 1119U;
		}
		return num;
	}

	// Token: 0x060054BB RID: 21691 RVA: 0x001BAF5C File Offset: 0x001B915C
	private static SslParameters GetSSLParams()
	{
		SslParameters sslParameters = new SslParameters();
		TextAsset textAsset = (TextAsset)Resources.Load("SSLCert/ssl_cert_bundle");
		if (textAsset != null)
		{
			sslParameters.bundleSettings.bundle = new SslCertBundle(textAsset.bytes);
		}
		sslParameters.bundleSettings.bundleDownloadConfig.numRetries = 3;
		sslParameters.bundleSettings.bundleDownloadConfig.timeoutMs = -1;
		return sslParameters;
	}

	// Token: 0x060054BC RID: 21692 RVA: 0x001BAFC1 File Offset: 0x001B91C1
	public static string GetVersion()
	{
		return Network.GetVersionFromConfig();
	}

	// Token: 0x060054BD RID: 21693 RVA: 0x001BAFC8 File Offset: 0x001B91C8
	private static string GetVersionFromConfig()
	{
		string text = Vars.Key("Aurora.Version.Source").GetStr("undefined");
		if (text == "undefined")
		{
			text = "product";
		}
		string text2;
		if (text == "product")
		{
			text2 = Network.ProductVersion();
		}
		else if (text == "string")
		{
			string text3 = "undefined";
			text2 = Vars.Key("Aurora.Version.String").GetStr(text3);
			if (text2 == text3)
			{
				Debug.LogError("Aurora.Version.String undefined");
			}
		}
		else
		{
			Debug.LogError("unknown version source: " + text);
			text2 = "0";
		}
		foreach (string text4 in HearthstoneApplication.CommandLineArgs)
		{
			if (text4.Equals("hsc") || text4.Equals("-hsc"))
			{
				text2 = "6969ef511a6cabbc24c5";
				break;
			}
			if (text4.Equals("hse") || text4.Equals("-hse"))
			{
				text2 = "2b4dbe94fa69d130c1a6";
				break;
			}
			if (text4.Equals("hsdev") || text4.Equals("-hsdev"))
			{
				text2 = "35d3a7a90e3bf4ad3b87";
				break;
			}
		}
		return text2;
	}

	// Token: 0x060054BE RID: 21694 RVA: 0x001BB0EE File Offset: 0x001B92EE
	public void OnLoginStarted()
	{
		this.m_connectApi.OnLoginStarted();
	}

	// Token: 0x060054BF RID: 21695 RVA: 0x001BB0FC File Offset: 0x001B92FC
	public void DoLoginUpdate()
	{
		string text = Vars.Key("Application.Referral").GetStr("none");
		if (text.Equals("none"))
		{
			if (PlatformSettings.OS == OSCategory.PC || PlatformSettings.OS == OSCategory.Mac)
			{
				text = "Battle.net";
			}
			else if (PlatformSettings.OS == OSCategory.iOS)
			{
				text = "AppleAppStore";
			}
			else if (PlatformSettings.OS == OSCategory.Android)
			{
				AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
				if (androidStore == AndroidStore.GOOGLE)
				{
					text = "GooglePlay";
				}
				else if (androidStore == AndroidStore.AMAZON)
				{
					text = "AmazonAppStore";
				}
				else if (androidStore == AndroidStore.HUAWEI)
				{
					text = "HuaweiAppStore";
				}
				else if (androidStore == AndroidStore.ONE_STORE)
				{
					text = "OneStore";
				}
				else if (androidStore == AndroidStore.BLIZZARD && PlatformSettings.LocaleVariant == LocaleVariant.China)
				{
					text = "JV-Android";
				}
			}
		}
		this.m_connectApi.DoLoginUpdate(text);
	}

	// Token: 0x060054C0 RID: 21696 RVA: 0x001BB1B4 File Offset: 0x001B93B4
	public void OnStartupPacketSequenceComplete()
	{
		this.m_connectApi.OnStartupPacketSequenceComplete();
	}

	// Token: 0x060054C1 RID: 21697 RVA: 0x001BB1C1 File Offset: 0x001B93C1
	public bool IsFindingGame()
	{
		return this.m_state.FindingBnetGameType > BnetGameType.BGT_UNKNOWN;
	}

	// Token: 0x060054C2 RID: 21698 RVA: 0x001BB1D1 File Offset: 0x001B93D1
	public BnetGameType GetFindingBnetGameType()
	{
		return this.m_state.FindingBnetGameType;
	}

	// Token: 0x060054C3 RID: 21699 RVA: 0x001BB1E0 File Offset: 0x001B93E0
	public static BnetGameType TranslateGameTypeToBnet(PegasusShared.GameType gameType, PegasusShared.FormatType formatType, int missionId)
	{
		switch (gameType)
		{
		case PegasusShared.GameType.GT_VS_AI:
			return BnetGameType.BGT_VS_AI;
		case PegasusShared.GameType.GT_VS_FRIEND:
			return BnetGameType.BGT_FRIENDS;
		case PegasusShared.GameType.GT_TUTORIAL:
			return BnetGameType.BGT_TUTORIAL;
		case PegasusShared.GameType.GT_ARENA:
			return BnetGameType.BGT_ARENA;
		case PegasusShared.GameType.GT_RANKED:
		case PegasusShared.GameType.GT_CASUAL:
			return RankMgr.Get().GetBnetGameTypeForLeague(gameType == PegasusShared.GameType.GT_RANKED, formatType);
		case PegasusShared.GameType.GT_TAVERNBRAWL:
			if (GameUtils.IsAIMission(missionId))
			{
				return BnetGameType.BGT_TAVERNBRAWL_1P_VERSUS_AI;
			}
			if (GameUtils.IsCoopMission(missionId))
			{
				return BnetGameType.BGT_TAVERNBRAWL_2P_COOP;
			}
			return BnetGameType.BGT_TAVERNBRAWL_PVP;
		case PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND:
			return BnetGameType.BGT_FSG_BRAWL_VS_FRIEND;
		case PegasusShared.GameType.GT_FSG_BRAWL:
			return BnetGameType.BGT_FSG_BRAWL_PVP;
		case PegasusShared.GameType.GT_FSG_BRAWL_1P_VS_AI:
			return BnetGameType.BGT_FSG_BRAWL_1P_VERSUS_AI;
		case PegasusShared.GameType.GT_FSG_BRAWL_2P_COOP:
			return BnetGameType.BGT_FSG_BRAWL_2P_COOP;
		case PegasusShared.GameType.GT_BATTLEGROUNDS:
			return BnetGameType.BGT_BATTLEGROUNDS;
		case PegasusShared.GameType.GT_BATTLEGROUNDS_FRIENDLY:
			return BnetGameType.BGT_BATTLEGROUNDS_FRIENDLY;
		case PegasusShared.GameType.GT_PVPDR_PAID:
			return BnetGameType.BGT_PVPDR_PAID;
		case PegasusShared.GameType.GT_PVPDR:
			return BnetGameType.BGT_PVPDR;
		}
		global::Error.AddDevFatal("Network.TranslateGameTypeToBnet() - do not know how to translate {0}", new object[]
		{
			gameType
		});
		return BnetGameType.BGT_UNKNOWN;
	}

	// Token: 0x060054C4 RID: 21700 RVA: 0x001BB2D0 File Offset: 0x001B94D0
	public void FindGame(PegasusShared.GameType gameType, PegasusShared.FormatType formatType, int scenarioId, int brawlLibraryItemId, long deckId, string aiDeck, int heroCardDbId, int? seasonId, bool restoredSavedGameState, byte[] snapshot, PegasusShared.GameType progFilterOverride = PegasusShared.GameType.GT_UNKNOWN)
	{
		if (gameType == PegasusShared.GameType.GT_VS_FRIEND || gameType == PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND)
		{
			global::Error.AddDevFatal("Network.FindGame - friendly challenges must call EnterFriendlyChallengeGame instead.", Array.Empty<object>());
			return;
		}
		BnetGameType bnetGameType = Network.TranslateGameTypeToBnet(gameType, formatType, scenarioId);
		if (bnetGameType == BnetGameType.BGT_UNKNOWN)
		{
			global::Error.AddDevFatal(string.Format("FindGame: no bnetGameType for {0} {1}", gameType, formatType), Array.Empty<object>());
			return;
		}
		this.m_state.FindingBnetGameType = bnetGameType;
		if (this.IsNoAccountTutorialGame(bnetGameType))
		{
			this.GoToNoAccountTutorialServer(scenarioId);
			return;
		}
		bool flag = Network.RequiresScenarioIdAttribute(gameType);
		byte[] array = Guid.NewGuid().ToByteArray();
		long currentFsgId = FiresideGatheringManager.Get().CurrentFsgId;
		global::Log.BattleNet.PrintInfo("FindGame type={0} scenario={1} deck={2} aideck={3} setScenId={4} request_guid={5}", new object[]
		{
			(int)bnetGameType,
			scenarioId,
			deckId,
			aiDeck,
			flag ? 1 : 0,
			(array == null) ? "null" : array.ToHexString()
		});
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", (long)bnetGameType));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("brawl_library_item_id", (long)brawlLibraryItemId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", deckId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("aideck", aiDeck ?? ""));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("request_guid", array));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		if (heroCardDbId != 0)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", (long)heroCardDbId));
		}
		if (seasonId != null)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", (long)seasonId.Value));
		}
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", (long)bnetGameType));
		if (flag)
		{
			list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", (long)scenarioId));
		}
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", (long)bnetGameType));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("brawl_library_item_id", (long)brawlLibraryItemId));
		list2.Add(ProtocolHelper.CreateAttributeV2("prog_filter_override", (long)progFilterOverride));
		if (Cheats.Get().GetBoardId() > 0)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("cheat_board_override", (long)Cheats.Get().GetBoardId()));
		}
		Cheats.Get().ClearBoardId();
		if (ReconnectMgr.Get().GetBypassReconnect())
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("bypass", true));
			ReconnectMgr.Get().SetBypassReconnect(false);
		}
		if (seasonId != null)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("season_id", (long)seasonId.Value));
		}
		if (snapshot != null)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("snapshot", snapshot));
		}
		if (restoredSavedGameState)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("load_game", true));
		}
		BattleNet.QueueMatchmaking(list, list2, new bnet.protocol.matchmaking.v1.Player[]
		{
			player
		});
		this.m_state.LastFindGameParameters = new FindGameResult();
		this.m_state.LastFindGameParameters.TimeSpentMilliseconds = (long)global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds;
		this.m_state.LastFindGameParameters.GameSessionInfo = new GameSessionInfo();
		this.m_state.LastFindGameParameters.GameSessionInfo.Version = Network.GetVersion();
		this.m_state.LastFindGameParameters.GameSessionInfo.ScenarioId = scenarioId;
		this.m_state.LastFindGameParameters.GameSessionInfo.BrawlLibraryItemId = brawlLibraryItemId;
		if (seasonId != null)
		{
			this.m_state.LastFindGameParameters.GameSessionInfo.SeasonId = seasonId.Value;
		}
		this.m_state.LastFindGameParameters.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)gameType;
		this.m_state.LastFindGameParameters.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType;
		this.m_state.LastFindGameParameters.GameSessionInfo.ClientDeckId = deckId;
		this.m_state.LastFindGameParameters.GameSessionInfo.ClientHeroCardId = (long)heroCardDbId;
	}

	// Token: 0x060054C5 RID: 21701 RVA: 0x001BB72C File Offset: 0x001B992C
	public void EnterFriendlyChallengeGame(PegasusShared.FormatType formatType, BrawlType brawlType, int scenarioId, int seasonId, int brawlLibraryItemId, DeckShareState player1DeckShareState, long player1DeckId, DeckShareState player2DeckShareState, long player2DeckId, long? player1HeroCardDbId, long? player2HeroCardDbId, BnetGameAccountId player2GameAccountId)
	{
		long val = 1L;
		PegasusShared.GameType gameType = PegasusShared.GameType.GT_VS_FRIEND;
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			val = 40L;
			gameType = PegasusShared.GameType.GT_FSG_BRAWL_VS_FRIEND;
		}
		long currentFsgId = FiresideGatheringManager.Get().CurrentFsgId;
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", val));
		list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", (long)scenarioId));
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", val));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("format", (long)formatType));
		list2.Add(ProtocolHelper.CreateAttributeV2("season_id", (long)seasonId));
		list2.Add(ProtocolHelper.CreateAttributeV2("brawl_library_item_id", (long)brawlLibraryItemId));
		if (Cheats.Get().GetBoardId() > 0)
		{
			list2.Add(ProtocolHelper.CreateAttributeV2("cheat_board_override", (long)Cheats.Get().GetBoardId()));
		}
		Cheats.Get().ClearBoardId();
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck_share_state", (long)player1DeckShareState));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", player1DeckId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 1L));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", (long)seasonId));
		if (player1HeroCardDbId != null)
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", player1HeroCardDbId.Value));
		}
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		bnet.protocol.matchmaking.v1.Player player2 = new bnet.protocol.matchmaking.v1.Player();
		player2.SetGameAccount(player2GameAccountId.GetGameAccountHandle());
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("deck_share_state", (long)player2DeckShareState));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("deck", player2DeckId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("fsg_id", currentFsgId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("season_id", (long)seasonId));
		if (player2HeroCardDbId != null)
		{
			player2.AddAttribute(ProtocolHelper.CreateAttributeV2("hero_card_id", player2HeroCardDbId.Value));
		}
		BattleNet.QueueMatchmaking(list, list2, new bnet.protocol.matchmaking.v1.Player[]
		{
			player,
			player2
		});
		this.m_state.LastFindGameParameters = new FindGameResult();
		this.m_state.LastFindGameParameters.TimeSpentMilliseconds = (long)global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds;
		this.m_state.LastFindGameParameters.GameSessionInfo = new GameSessionInfo();
		this.m_state.LastFindGameParameters.GameSessionInfo.Version = Network.GetVersion();
		this.m_state.LastFindGameParameters.GameSessionInfo.ScenarioId = scenarioId;
		this.m_state.LastFindGameParameters.GameSessionInfo.BrawlLibraryItemId = brawlLibraryItemId;
		this.m_state.LastFindGameParameters.GameSessionInfo.SeasonId = seasonId;
		this.m_state.LastFindGameParameters.GameSessionInfo.GameType = (Blizzard.Telemetry.WTCG.Client.GameType)gameType;
		this.m_state.LastFindGameParameters.GameSessionInfo.FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType;
		this.m_state.LastFindGameParameters.GameSessionInfo.ClientDeckId = player1DeckId;
		if (player1HeroCardDbId != null)
		{
			this.m_state.LastFindGameParameters.GameSessionInfo.ClientHeroCardId = player1HeroCardDbId.Value;
		}
	}

	// Token: 0x060054C6 RID: 21702 RVA: 0x001BBAF0 File Offset: 0x001B9CF0
	public void EnterBattlegroundsWithFriend(BnetGameAccountId player2GameAccountId, int scenarioId)
	{
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		long val = 50L;
		this.m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS;
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", val));
		list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", (long)scenarioId));
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", val));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("format", 2L));
		bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
		player.SetGameAccount(BnetPresenceMgr.Get().GetMyGameAccountId().GetGameAccountHandle());
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 1L));
		if (!string.IsNullOrEmpty(Cheats.Get().GetPlayerTags()))
		{
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("cheat_player_tags", Cheats.Get().GetPlayerTags()));
		}
		Cheats.Get().ClearAllPlayerTags();
		bnet.protocol.matchmaking.v1.Player player2 = new bnet.protocol.matchmaking.v1.Player();
		player2.SetGameAccount(player2GameAccountId.GetGameAccountHandle());
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		player2.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
		BattleNet.QueueMatchmaking(list, list2, new bnet.protocol.matchmaking.v1.Player[]
		{
			player,
			player2
		});
	}

	// Token: 0x060054C7 RID: 21703 RVA: 0x001BBC54 File Offset: 0x001B9E54
	public void EnterBattlegroundsWithParty(bgs.PartyMember[] members, int scenarioId)
	{
		List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
		long val;
		if (members.Length <= PartyManager.Get().GetBattlegroundsMaxRankedPartySize())
		{
			val = 50L;
			this.m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS;
		}
		else
		{
			val = 51L;
			this.m_state.FindingBnetGameType = BnetGameType.BGT_BATTLEGROUNDS_FRIENDLY;
		}
		int currentPartySize = PartyManager.Get().GetCurrentPartySize();
		BnetEntityId bnetEntityId = new BnetEntityId();
		if (PartyManager.Get().GetLeader() != null)
		{
			bnetEntityId = PartyManager.Get().GetLeader();
		}
		list.Add(ProtocolHelper.CreateAttributeV2("GameType", val));
		list.Add(ProtocolHelper.CreateAttributeV2("ScenarioId", (long)scenarioId));
		List<bnet.protocol.v2.Attribute> list2 = new List<bnet.protocol.v2.Attribute>();
		list2.Add(ProtocolHelper.CreateAttributeV2("type", val));
		list2.Add(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
		list2.Add(ProtocolHelper.CreateAttributeV2("format", 2L));
		List<bnet.protocol.matchmaking.v1.Player> list3 = new List<bnet.protocol.matchmaking.v1.Player>();
		foreach (bgs.PartyMember partyMember in members)
		{
			bnet.protocol.matchmaking.v1.Player player = new bnet.protocol.matchmaking.v1.Player();
			player.SetGameAccount(partyMember.GameAccountId.GetGameAccountHandle());
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("type", val));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("scenario", (long)scenarioId));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("player_type", 2L));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_size", (long)currentPartySize));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_leader_game_account_id_hi", bnetEntityId.GetHi()));
			player.AddAttribute(ProtocolHelper.CreateAttributeV2("party_leader_game_account_id_lo", bnetEntityId.GetLo()));
			list3.Add(player);
		}
		BattleNet.QueueMatchmaking(list, list2, list3.ToArray());
	}

	// Token: 0x060054C8 RID: 21704 RVA: 0x001BBDFF File Offset: 0x001B9FFF
	public void OnFindGameStateChanged(FindGameState prevState, FindGameState newState, uint errorCode)
	{
		switch (newState)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			break;
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CONNECTING:
			this.SendTelemetry_FindGameResult(errorCode);
			break;
		default:
			return;
		}
	}

	// Token: 0x060054C9 RID: 21705 RVA: 0x001BBE3C File Offset: 0x001BA03C
	private void SendTelemetry_FindGameResult(uint errorCode)
	{
		if (this.m_state.LastFindGameParameters == null)
		{
			return;
		}
		string resultCodeString;
		if (errorCode >= 1000000U)
		{
			ErrorCode errorCode2 = (ErrorCode)errorCode;
			resultCodeString = errorCode2.ToString();
		}
		else
		{
			BattleNetErrors battleNetErrors = (BattleNetErrors)errorCode;
			resultCodeString = battleNetErrors.ToString();
		}
		long timeSpentMilliseconds = (long)(global::TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds - (double)this.m_state.LastFindGameParameters.TimeSpentMilliseconds);
		this.m_state.LastFindGameParameters.ResultCode = errorCode;
		this.m_state.LastFindGameParameters.ResultCodeString = resultCodeString;
		this.m_state.LastFindGameParameters.TimeSpentMilliseconds = timeSpentMilliseconds;
		TelemetryManager.Client().SendFindGameResult(this.m_state.LastFindGameParameters);
		this.m_state.LastFindGameParameters = null;
	}

	// Token: 0x060054CA RID: 21706 RVA: 0x001BBF01 File Offset: 0x001BA101
	private static bool RequiresScenarioIdAttribute(PegasusShared.GameType gameType)
	{
		return gameType == PegasusShared.GameType.GT_VS_FRIEND || GameUtils.IsTavernBrawlGameType(gameType);
	}

	// Token: 0x060054CB RID: 21707 RVA: 0x001BBF14 File Offset: 0x001BA114
	public void CancelFindGame()
	{
		if (this.m_state.FindingBnetGameType == BnetGameType.BGT_UNKNOWN)
		{
			return;
		}
		if (!Network.IsLoggedIn())
		{
			this.m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			return;
		}
		BnetGameType findingBnetGameType = this.GetFindingBnetGameType();
		if (!this.IsNoAccountTutorialGame(findingBnetGameType))
		{
			BattleNet.CancelMatchmaking();
		}
		this.m_state.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
	}

	// Token: 0x060054CC RID: 21708 RVA: 0x001BBF64 File Offset: 0x001BA164
	private bool IsNoAccountTutorialGame(BnetGameType gameType)
	{
		return !Network.ShouldBeConnectedToAurora() && gameType == BnetGameType.BGT_TUTORIAL;
	}

	// Token: 0x060054CD RID: 21709 RVA: 0x001BBF78 File Offset: 0x001BA178
	private void SendGameServerHandshake(GameServerInfo gameInfo)
	{
		NetCache.Get().DispatchClientOptionsToServer();
		if (gameInfo.SpectatorMode)
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			this.m_connectApi.SendSpectatorGameHandshake(BattleNet.GetVersion(), this.GetPlatformBuilder(), gameInfo, new BnetId
			{
				Hi = myGameAccountId.GetHi(),
				Lo = myGameAccountId.GetLo()
			});
			return;
		}
		this.m_connectApi.SendGameHandshake(gameInfo, this.GetPlatformBuilder());
	}

	// Token: 0x060054CE RID: 21710 RVA: 0x001BBFEC File Offset: 0x001BA1EC
	private void GoToNoAccountTutorialServer(int scenario)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Version = BattleNet.GetVersion();
		if (Vars.Key("GameServerOverride.Active").GetBool(false))
		{
			gameServerInfo.Address = Vars.Key("GameServerOverride.Address").GetStr("");
			gameServerInfo.Port = Vars.Key("GameServerOverride.Port").GetUInt(0U);
			gameServerInfo.AuroraPassword = "";
		}
		else
		{
			constants.BnetRegion currentRegionId = MobileDeviceLocale.GetCurrentRegionId();
			if (HearthstoneApplication.GetMobileEnvironment() == MobileEnv.PRODUCTION)
			{
				string format;
				try
				{
					format = Network.RegionToTutorialName[currentRegionId];
				}
				catch (KeyNotFoundException)
				{
					Debug.LogWarning("No matching tutorial server name found for region " + currentRegionId);
					format = "us";
				}
				gameServerInfo.Address = string.Format(format, Network.TutorialServer);
				gameServerInfo.Port = 1119U;
			}
			else
			{
				MobileDeviceLocale.ConnectionData connectionDataFromRegionId = MobileDeviceLocale.GetConnectionDataFromRegionId(currentRegionId, true);
				gameServerInfo.Port = connectionDataFromRegionId.tutorialPort;
				gameServerInfo.Address = (string.IsNullOrEmpty(connectionDataFromRegionId.gameServerAddress) ? "10.130.126.28" : connectionDataFromRegionId.gameServerAddress);
				gameServerInfo.Version = connectionDataFromRegionId.version;
			}
			global::Log.Net.Print(string.Format("Connecting to account-free tutorial server for region {0}.  Address: {1}  Port: {2}  Version: {3}", new object[]
			{
				currentRegionId,
				gameServerInfo.Address,
				gameServerInfo.Port,
				gameServerInfo.Version
			}), Array.Empty<object>());
			gameServerInfo.AuroraPassword = "";
		}
		gameServerInfo.GameHandle = 0U;
		gameServerInfo.ClientHandle = 0L;
		gameServerInfo.Mission = scenario;
		gameServerInfo.BrawlLibraryItemId = 0;
		this.ResolveAddressAndGotoGameServer(gameServerInfo);
	}

	// Token: 0x060054CF RID: 21711 RVA: 0x001BC17C File Offset: 0x001BA37C
	private void ResolveAddressAndGotoGameServer(GameServerInfo gameServer)
	{
		IPAddress ipaddress;
		if (IPAddress.TryParse(gameServer.Address, out ipaddress))
		{
			gameServer.Address = ipaddress.ToString();
			Network.Get().GotoGameServer(gameServer, false);
			return;
		}
		try
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(gameServer.Address);
			if (hostEntry.AddressList.Length != 0)
			{
				IPAddress ipaddress2 = hostEntry.AddressList[0];
				gameServer.Address = ipaddress2.ToString();
				Network.Get().GotoGameServer(gameServer, false);
				return;
			}
		}
		catch (Exception ex)
		{
			this.m_state.LogSource.LogError("Exception within ResolveAddressAndGotoGameServer: " + ex.Message);
		}
		this.ThrowDnsResolveError(gameServer.Address);
	}

	// Token: 0x060054D0 RID: 21712 RVA: 0x001BC22C File Offset: 0x001BA42C
	private void ThrowDnsResolveError(string environment)
	{
		if (HearthstoneApplication.IsInternal())
		{
			global::Error.AddDevFatal("Environment " + environment + " could not be resolved! Please check your environment and Internet connection!", Array.Empty<object>());
			return;
		}
		global::Error.AddFatal(FatalErrorReason.DNS_RESOLVE, "GLOBAL_ERROR_NETWORK_NO_CONNECTION", Array.Empty<object>());
	}

	// Token: 0x060054D1 RID: 21713 RVA: 0x001BC264 File Offset: 0x001BA464
	public Network.GameCancelInfo GetGameCancelInfo()
	{
		GameCanceled gameCancelInfo = this.m_connectApi.GetGameCancelInfo();
		if (gameCancelInfo == null)
		{
			return null;
		}
		return new Network.GameCancelInfo
		{
			CancelReason = (Network.GameCancelInfo.Reason)gameCancelInfo.Reason_
		};
	}

	// Token: 0x060054D2 RID: 21714 RVA: 0x001BC293 File Offset: 0x001BA493
	public void GetGameState()
	{
		this.m_connectApi.GetGameState();
	}

	// Token: 0x060054D3 RID: 21715 RVA: 0x001BC2A0 File Offset: 0x001BA4A0
	public void UpdateBattlegroundInfo()
	{
		this.m_connectApi.UpdateBattlegroundInfo();
	}

	// Token: 0x060054D4 RID: 21716 RVA: 0x001BC2AD File Offset: 0x001BA4AD
	public void RequestGameRoundHistory()
	{
		this.m_connectApi.RequestGameRoundHistory();
	}

	// Token: 0x060054D5 RID: 21717 RVA: 0x001BC2BA File Offset: 0x001BA4BA
	public void RequestRealtimeBattlefieldRaces()
	{
		this.m_connectApi.RequestRealtimeBattlefieldRaces();
	}

	// Token: 0x060054D6 RID: 21718 RVA: 0x001BC2C8 File Offset: 0x001BA4C8
	public Network.TurnTimerInfo GetTurnTimerInfo()
	{
		PegasusGame.TurnTimer turnTimerInfo = this.m_connectApi.GetTurnTimerInfo();
		if (turnTimerInfo == null)
		{
			return null;
		}
		return new Network.TurnTimerInfo
		{
			Seconds = (float)turnTimerInfo.Seconds,
			Turn = turnTimerInfo.Turn,
			Show = turnTimerInfo.Show
		};
	}

	// Token: 0x060054D7 RID: 21719 RVA: 0x001BC310 File Offset: 0x001BA510
	public int GetNAckOption()
	{
		NAckOption nackOption = this.m_connectApi.GetNAckOption();
		if (nackOption == null)
		{
			return 0;
		}
		return nackOption.Id;
	}

	// Token: 0x060054D8 RID: 21720 RVA: 0x001BC334 File Offset: 0x001BA534
	public SpectatorNotify GetSpectatorNotify()
	{
		return this.m_connectApi.GetSpectatorNotify();
	}

	// Token: 0x060054D9 RID: 21721 RVA: 0x001BC341 File Offset: 0x001BA541
	public AIDebugInformation GetAIDebugInformation()
	{
		return this.m_connectApi.GetAIDebugInformation();
	}

	// Token: 0x060054DA RID: 21722 RVA: 0x001BC34E File Offset: 0x001BA54E
	public RopeTimerDebugInformation GetRopeTimerDebugInformation()
	{
		return this.m_connectApi.GetRopeTimerDebugInformation();
	}

	// Token: 0x060054DB RID: 21723 RVA: 0x001BC35B File Offset: 0x001BA55B
	public ScriptDebugInformation GetScriptDebugInformation()
	{
		return this.m_connectApi.GetScriptDebugInformation();
	}

	// Token: 0x060054DC RID: 21724 RVA: 0x001BC368 File Offset: 0x001BA568
	public GameRoundHistory GetGameRoundHistory()
	{
		return this.m_connectApi.GetGameRoundHistory();
	}

	// Token: 0x060054DD RID: 21725 RVA: 0x001BC375 File Offset: 0x001BA575
	public GameRealTimeBattlefieldRaces GetGameRealTimeBattlefieldRaces()
	{
		return this.m_connectApi.GetGameRealTimeBattlefieldRaces();
	}

	// Token: 0x060054DE RID: 21726 RVA: 0x001BC382 File Offset: 0x001BA582
	public BattlegroundsRatingChange GetBattlegroundsRatingChange()
	{
		return this.m_connectApi.GetBattlegroundsRatingChange();
	}

	// Token: 0x060054DF RID: 21727 RVA: 0x001BC38F File Offset: 0x001BA58F
	public GameGuardianVars GetGameGuardianVars()
	{
		return this.m_connectApi.GetGameGuardianVars();
	}

	// Token: 0x060054E0 RID: 21728 RVA: 0x001BC39C File Offset: 0x001BA59C
	public UpdateBattlegroundInfo GetBattlegroundInfo()
	{
		return this.m_connectApi.GetBattlegroundInfo();
	}

	// Token: 0x060054E1 RID: 21729 RVA: 0x001BC3A9 File Offset: 0x001BA5A9
	public DebugMessage GetDebugMessage()
	{
		return this.m_connectApi.GetDebugMessage();
	}

	// Token: 0x060054E2 RID: 21730 RVA: 0x001BC3B6 File Offset: 0x001BA5B6
	public ScriptLogMessage GetScriptLogMessage()
	{
		return this.m_connectApi.GetScriptLogMessage();
	}

	// Token: 0x060054E3 RID: 21731 RVA: 0x001BC3C3 File Offset: 0x001BA5C3
	public AchievementProgress GetAchievementInGameProgress()
	{
		return this.m_connectApi.GetAchievementInGameProgress();
	}

	// Token: 0x060054E4 RID: 21732 RVA: 0x001BC3D0 File Offset: 0x001BA5D0
	public AchievementComplete GetAchievementComplete()
	{
		return this.m_connectApi.GetAchievementComplete();
	}

	// Token: 0x060054E5 RID: 21733 RVA: 0x001BC3DD File Offset: 0x001BA5DD
	public void DisconnectFromGameServer()
	{
		if (!this.IsConnectedToGameServer())
		{
			return;
		}
		this.m_disconnectRequested = true;
		this.m_connectApi.DisconnectFromGameServer();
	}

	// Token: 0x060054E6 RID: 21734 RVA: 0x001BC3FA File Offset: 0x001BA5FA
	public bool WasDisconnectRequested()
	{
		return this.m_disconnectRequested;
	}

	// Token: 0x060054E7 RID: 21735 RVA: 0x001BC402 File Offset: 0x001BA602
	public bool IsConnectedToGameServer()
	{
		return this.m_connectApi.IsConnectedToGameServer();
	}

	// Token: 0x060054E8 RID: 21736 RVA: 0x001BC40F File Offset: 0x001BA60F
	public bool GameServerHasEvents()
	{
		return this.m_connectApi.GameServerHasEvents();
	}

	// Token: 0x060054E9 RID: 21737 RVA: 0x001BC41C File Offset: 0x001BA61C
	public bool WasGameConceded()
	{
		return this.m_gameConceded;
	}

	// Token: 0x060054EA RID: 21738 RVA: 0x001BC424 File Offset: 0x001BA624
	public void Concede()
	{
		this.m_gameConceded = true;
		this.m_connectApi.Concede();
	}

	// Token: 0x060054EB RID: 21739 RVA: 0x001BC438 File Offset: 0x001BA638
	public void AutoConcede()
	{
		if (!this.IsConnectedToGameServer())
		{
			return;
		}
		if (this.WasGameConceded())
		{
			return;
		}
		this.Concede();
	}

	// Token: 0x060054EC RID: 21740 RVA: 0x001BC454 File Offset: 0x001BA654
	public Network.EntityChoices GetEntityChoices()
	{
		PegasusGame.EntityChoices entityChoices = this.m_connectApi.GetEntityChoices();
		if (entityChoices == null)
		{
			return null;
		}
		return new Network.EntityChoices
		{
			ID = entityChoices.Id,
			ChoiceType = (CHOICE_TYPE)entityChoices.ChoiceType,
			CountMax = entityChoices.CountMax,
			CountMin = entityChoices.CountMin,
			Entities = this.CopyIntList(entityChoices.Entities),
			Source = entityChoices.Source,
			PlayerId = entityChoices.PlayerId,
			HideChosen = entityChoices.HideChosen
		};
	}

	// Token: 0x060054ED RID: 21741 RVA: 0x001BC4E0 File Offset: 0x001BA6E0
	public Network.EntitiesChosen GetEntitiesChosen()
	{
		PegasusGame.EntitiesChosen entitiesChosen = this.m_connectApi.GetEntitiesChosen();
		if (entitiesChosen == null)
		{
			return null;
		}
		return new Network.EntitiesChosen
		{
			ID = entitiesChosen.ChooseEntities.Id,
			Entities = this.CopyIntList(entitiesChosen.ChooseEntities.Entities),
			PlayerId = entitiesChosen.PlayerId,
			ChoiceType = (CHOICE_TYPE)entitiesChosen.ChoiceType
		};
	}

	// Token: 0x060054EE RID: 21742 RVA: 0x001BC543 File Offset: 0x001BA743
	public void SendChoices(int id, List<int> picks)
	{
		this.m_connectApi.SendChoices(id, picks);
	}

	// Token: 0x060054EF RID: 21743 RVA: 0x001BC552 File Offset: 0x001BA752
	public void SendOption(int id, int index, int target, int sub, int pos)
	{
		this.m_connectApi.SendOption(id, index, target, sub, pos);
	}

	// Token: 0x060054F0 RID: 21744 RVA: 0x001BC566 File Offset: 0x001BA766
	public void SendFreeDeckChoice(int classId, long noticeId)
	{
		this.m_connectApi.SendFreeDeckChoice(classId, noticeId);
	}

	// Token: 0x060054F1 RID: 21745 RVA: 0x001BC578 File Offset: 0x001BA778
	public Network.Options GetOptions()
	{
		AllOptions allOptions = this.m_connectApi.GetAllOptions();
		Network.Options options = new Network.Options
		{
			ID = allOptions.Id
		};
		for (int i = 0; i < allOptions.Options.Count; i++)
		{
			PegasusGame.Option option = allOptions.Options[i];
			Network.Options.Option option2 = new Network.Options.Option();
			option2.Type = (Network.Options.Option.OptionType)option.Type_;
			if (option.HasMainOption)
			{
				option2.Main.ID = option.MainOption.Id;
				option2.Main.PlayErrorInfo.PlayError = (PlayErrors.ErrorType)option.MainOption.PlayError;
				option2.Main.PlayErrorInfo.PlayErrorParam = (option.MainOption.HasPlayErrorParam ? new int?(option.MainOption.PlayErrorParam) : null);
				option2.Main.Targets = this.CopyTargetOptionList(option.MainOption.Targets);
			}
			for (int j = 0; j < option.SubOptions.Count; j++)
			{
				SubOption subOption = option.SubOptions[j];
				Network.Options.Option.SubOption subOption2 = new Network.Options.Option.SubOption();
				subOption2.ID = subOption.Id;
				subOption2.PlayErrorInfo.PlayError = (PlayErrors.ErrorType)subOption.PlayError;
				subOption2.PlayErrorInfo.PlayErrorParam = (subOption.HasPlayErrorParam ? new int?(subOption.PlayErrorParam) : null);
				subOption2.Targets = this.CopyTargetOptionList(subOption.Targets);
				option2.Subs.Add(subOption2);
			}
			options.List.Add(option2);
		}
		return options;
	}

	// Token: 0x060054F2 RID: 21746 RVA: 0x001BC72C File Offset: 0x001BA92C
	private List<Network.Options.Option.TargetOption> CopyTargetOptionList(IList<TargetOption> originalList)
	{
		List<Network.Options.Option.TargetOption> list = new List<Network.Options.Option.TargetOption>();
		for (int i = 0; i < originalList.Count; i++)
		{
			TargetOption targetOption = originalList[i];
			Network.Options.Option.TargetOption targetOption2 = new Network.Options.Option.TargetOption();
			targetOption2.CopyFrom(targetOption);
			list.Add(targetOption2);
		}
		return list;
	}

	// Token: 0x060054F3 RID: 21747 RVA: 0x001BC770 File Offset: 0x001BA970
	private List<int> CopyIntList(IList<int> intList)
	{
		int[] array = new int[intList.Count];
		intList.CopyTo(array, 0);
		return new List<int>(array);
	}

	// Token: 0x060054F4 RID: 21748 RVA: 0x001BC797 File Offset: 0x001BA997
	public void SendUserUI(int overCard, int heldCard, int arrowOrigin, int x, int y)
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.ShowUserUI != 0)
		{
			this.m_connectApi.SendUserUi(overCard, heldCard, arrowOrigin, x, y);
		}
	}

	// Token: 0x060054F5 RID: 21749 RVA: 0x001BC7C1 File Offset: 0x001BA9C1
	public void SendEmote(EmoteType emote)
	{
		this.m_connectApi.SendEmote((int)emote);
	}

	// Token: 0x060054F6 RID: 21750 RVA: 0x001BC7D0 File Offset: 0x001BA9D0
	public void SendSpectatorInvite(BnetAccountId bnetAccountId, BnetGameAccountId bnetGameAccountId)
	{
		BnetId targetBnetId = new BnetId
		{
			Hi = bnetAccountId.GetHi(),
			Lo = bnetAccountId.GetLo()
		};
		BnetId targetGameAccountId = new BnetId
		{
			Hi = bnetGameAccountId.GetHi(),
			Lo = bnetGameAccountId.GetLo()
		};
		this.m_connectApi.SendSpectatorInvite(targetBnetId, targetGameAccountId);
	}

	// Token: 0x060054F7 RID: 21751 RVA: 0x001BC828 File Offset: 0x001BAA28
	public void SendRemoveSpectators(bool regenerateSpectatorPassword, params BnetGameAccountId[] bnetGameAccountIds)
	{
		List<BnetId> list = new List<BnetId>();
		for (int i = 0; i < bnetGameAccountIds.Length; i++)
		{
			list.Add(new BnetId
			{
				Hi = bnetGameAccountIds[i].GetHi(),
				Lo = bnetGameAccountIds[i].GetLo()
			});
		}
		this.m_connectApi.SendRemoveSpectators(regenerateSpectatorPassword, list);
	}

	// Token: 0x060054F8 RID: 21752 RVA: 0x001BC87D File Offset: 0x001BAA7D
	public void SendRemoveAllSpectators(bool regenerateSpectatorPassword)
	{
		this.m_connectApi.SendRemoveAllSpectators(regenerateSpectatorPassword);
	}

	// Token: 0x060054F9 RID: 21753 RVA: 0x001BC88C File Offset: 0x001BAA8C
	public Network.UserUI GetUserUI()
	{
		PegasusGame.UserUI userUi = this.m_connectApi.GetUserUi();
		if (userUi == null)
		{
			return null;
		}
		Network.UserUI userUI = new Network.UserUI();
		if (userUi.HasPlayerId)
		{
			userUI.playerId = new int?(userUi.PlayerId);
		}
		if (userUi.HasMouseInfo)
		{
			MouseInfo mouseInfo = userUi.MouseInfo;
			userUI.mouseInfo = new Network.UserUI.MouseInfo();
			userUI.mouseInfo.ArrowOriginID = mouseInfo.ArrowOrigin;
			userUI.mouseInfo.HeldCardID = mouseInfo.HeldCard;
			userUI.mouseInfo.OverCardID = mouseInfo.OverCard;
			userUI.mouseInfo.X = mouseInfo.X;
			userUI.mouseInfo.Y = mouseInfo.Y;
		}
		else if (userUi.HasEmote)
		{
			userUI.emoteInfo = new Network.UserUI.EmoteInfo();
			userUI.emoteInfo.Emote = userUi.Emote;
		}
		return userUI;
	}

	// Token: 0x060054FA RID: 21754 RVA: 0x001BC960 File Offset: 0x001BAB60
	public Network.GameSetup GetGameSetupInfo()
	{
		PegasusGame.GameSetup gameSetup = this.m_connectApi.GetGameSetup();
		if (gameSetup == null)
		{
			return null;
		}
		Network.GameSetup gameSetup2 = new Network.GameSetup();
		gameSetup2.Board = gameSetup.Board;
		gameSetup2.MaxSecretZoneSizePerPlayer = gameSetup.MaxSecretZoneSizePerPlayer;
		gameSetup2.MaxSecretsPerPlayer = gameSetup.MaxSecretsPerPlayer;
		gameSetup2.MaxQuestsPerPlayer = gameSetup.MaxQuestsPerPlayer;
		gameSetup2.MaxFriendlyMinionsPerPlayer = gameSetup.MaxFriendlyMinionsPerPlayer;
		if (gameSetup.HasKeepAliveFrequencySeconds)
		{
			this.m_gameServerKeepAliveFrequencySeconds = gameSetup.KeepAliveFrequencySeconds;
		}
		else
		{
			this.m_gameServerKeepAliveFrequencySeconds = 0U;
		}
		if (gameSetup.HasKeepAliveRetry)
		{
			this.m_gameServerKeepAliveRetry = gameSetup.KeepAliveRetry;
		}
		else
		{
			this.m_gameServerKeepAliveRetry = 1U;
		}
		if (gameSetup.HasKeepAliveWaitForInternetSeconds)
		{
			this.m_gameServerKeepAliveWaitForInternetSeconds = gameSetup.KeepAliveWaitForInternetSeconds;
		}
		else
		{
			this.m_gameServerKeepAliveWaitForInternetSeconds = 20U;
		}
		if (gameSetup.HasDisconnectWhenStuckSeconds)
		{
			gameSetup2.DisconnectWhenStuckSeconds = gameSetup.DisconnectWhenStuckSeconds;
		}
		return gameSetup2;
	}

	// Token: 0x060054FB RID: 21755 RVA: 0x001BCA30 File Offset: 0x001BAC30
	public List<Network.PowerHistory> GetPowerHistory()
	{
		PegasusGame.PowerHistory powerHistory = this.m_connectApi.GetPowerHistory();
		if (powerHistory == null)
		{
			return null;
		}
		List<Network.PowerHistory> list = new List<Network.PowerHistory>();
		for (int i = 0; i < powerHistory.List.Count; i++)
		{
			PowerHistoryData powerHistoryData = powerHistory.List[i];
			Network.PowerHistory powerHistory2 = null;
			if (powerHistoryData.HasFullEntity)
			{
				powerHistory2 = Network.GetFullEntity(powerHistoryData.FullEntity);
			}
			else if (powerHistoryData.HasShowEntity)
			{
				powerHistory2 = Network.GetShowEntity(powerHistoryData.ShowEntity);
			}
			else if (powerHistoryData.HasHideEntity)
			{
				powerHistory2 = Network.GetHideEntity(powerHistoryData.HideEntity);
			}
			else if (powerHistoryData.HasChangeEntity)
			{
				powerHistory2 = Network.GetChangeEntity(powerHistoryData.ChangeEntity);
			}
			else if (powerHistoryData.HasTagChange)
			{
				powerHistory2 = Network.GetTagChange(powerHistoryData.TagChange);
			}
			else if (powerHistoryData.HasPowerStart)
			{
				powerHistory2 = Network.GetBlockStart(powerHistoryData.PowerStart);
			}
			else if (powerHistoryData.HasPowerEnd)
			{
				powerHistory2 = Network.GetBlockEnd(powerHistoryData.PowerEnd);
			}
			else if (powerHistoryData.HasCreateGame)
			{
				powerHistory2 = Network.GetCreateGame(powerHistoryData.CreateGame);
			}
			else if (powerHistoryData.HasResetGame)
			{
				powerHistory2 = Network.GetResetGame(powerHistoryData.ResetGame);
			}
			else if (powerHistoryData.HasMetaData)
			{
				powerHistory2 = Network.GetMetaData(powerHistoryData.MetaData);
			}
			else if (powerHistoryData.HasSubSpellStart)
			{
				powerHistory2 = Network.GetSubSpellStart(powerHistoryData.SubSpellStart);
			}
			else if (powerHistoryData.HasSubSpellEnd)
			{
				powerHistory2 = Network.GetSubSpellEnd(powerHistoryData.SubSpellEnd);
			}
			else if (powerHistoryData.HasVoSpell)
			{
				powerHistory2 = Network.GetVoSpell(powerHistoryData.VoSpell);
			}
			else if (powerHistoryData.HasCachedTagForDormantChange)
			{
				powerHistory2 = Network.GetCachedTagForDormantChange(powerHistoryData.CachedTagForDormantChange);
			}
			else if (powerHistoryData.HasShuffleDeck)
			{
				powerHistory2 = Network.GetShuffleDeck(powerHistoryData.ShuffleDeck);
			}
			else
			{
				Debug.LogError("Network.GetPowerHistory() - received invalid PowerHistoryData packet");
			}
			if (powerHistory2 != null)
			{
				list.Add(powerHistory2);
			}
		}
		return list;
	}

	// Token: 0x060054FC RID: 21756 RVA: 0x001BCC0B File Offset: 0x001BAE0B
	private static Network.HistFullEntity GetFullEntity(PowerHistoryEntity entity)
	{
		return new Network.HistFullEntity
		{
			Entity = Network.Entity.CreateFromProto(entity)
		};
	}

	// Token: 0x060054FD RID: 21757 RVA: 0x001BCC1E File Offset: 0x001BAE1E
	private static Network.HistShowEntity GetShowEntity(PowerHistoryEntity entity)
	{
		return new Network.HistShowEntity
		{
			Entity = Network.Entity.CreateFromProto(entity)
		};
	}

	// Token: 0x060054FE RID: 21758 RVA: 0x001BCC31 File Offset: 0x001BAE31
	private static Network.HistHideEntity GetHideEntity(PowerHistoryHide hide)
	{
		return new Network.HistHideEntity
		{
			Entity = hide.Entity,
			Zone = hide.Zone
		};
	}

	// Token: 0x060054FF RID: 21759 RVA: 0x001BCC50 File Offset: 0x001BAE50
	private static Network.HistChangeEntity GetChangeEntity(PowerHistoryEntity entity)
	{
		return new Network.HistChangeEntity
		{
			Entity = Network.Entity.CreateFromProto(entity)
		};
	}

	// Token: 0x06005500 RID: 21760 RVA: 0x001BCC63 File Offset: 0x001BAE63
	private static Network.HistTagChange GetTagChange(PowerHistoryTagChange tagChange)
	{
		return new Network.HistTagChange
		{
			Entity = tagChange.Entity,
			Tag = tagChange.Tag,
			Value = tagChange.Value,
			ChangeDef = tagChange.ChangeDef
		};
	}

	// Token: 0x06005501 RID: 21761 RVA: 0x001BCC9C File Offset: 0x001BAE9C
	private static Network.HistBlockStart GetBlockStart(PowerHistoryStart start)
	{
		return new Network.HistBlockStart(start.Type)
		{
			Entities = new List<int>
			{
				start.Source
			},
			Target = start.Target,
			SubOption = start.SubOption,
			EffectCardId = new List<string>
			{
				start.EffectCardId
			},
			IsEffectCardIdClientCached = new List<bool>
			{
				false
			},
			EffectIndex = start.EffectIndex,
			TriggerKeyword = start.TriggerKeyword,
			ShowInHistory = start.ShowInHistory,
			IsDeferrable = start.IsDeferrable,
			IsBatchable = start.IsBatchable,
			IsDeferBlocker = start.IsDeferBlocker,
			ForceShowBigCard = start.ForceShowBigCard
		};
	}

	// Token: 0x06005502 RID: 21762 RVA: 0x001BCD60 File Offset: 0x001BAF60
	private static Network.HistBlockEnd GetBlockEnd(PowerHistoryEnd end)
	{
		return new Network.HistBlockEnd();
	}

	// Token: 0x06005503 RID: 21763 RVA: 0x001BCD67 File Offset: 0x001BAF67
	private static Network.HistCreateGame GetCreateGame(PowerHistoryCreateGame createGame)
	{
		return Network.HistCreateGame.CreateFromProto(createGame);
	}

	// Token: 0x06005504 RID: 21764 RVA: 0x001BCD6F File Offset: 0x001BAF6F
	private static Network.HistResetGame GetResetGame(PowerHistoryResetGame resetGame)
	{
		return Network.HistResetGame.CreateFromProto(resetGame);
	}

	// Token: 0x06005505 RID: 21765 RVA: 0x001BCD78 File Offset: 0x001BAF78
	private static Network.HistMetaData GetMetaData(PowerHistoryMetaData metaData)
	{
		Network.HistMetaData histMetaData = new Network.HistMetaData();
		histMetaData.MetaType = (metaData.HasType ? metaData.Type : HistoryMeta.Type.TARGET);
		histMetaData.Data = (metaData.HasData ? metaData.Data : 0);
		for (int i = 0; i < metaData.Info.Count; i++)
		{
			int item = metaData.Info[i];
			histMetaData.Info.Add(item);
		}
		for (int j = 0; j < metaData.AdditionalData.Count; j++)
		{
			int item2 = metaData.AdditionalData[j];
			histMetaData.AdditionalData.Add(item2);
		}
		return histMetaData;
	}

	// Token: 0x06005506 RID: 21766 RVA: 0x001BCE1A File Offset: 0x001BB01A
	private static Network.HistSubSpellStart GetSubSpellStart(PowerHistorySubSpellStart subSpellStart)
	{
		return Network.HistSubSpellStart.CreateFromProto(subSpellStart);
	}

	// Token: 0x06005507 RID: 21767 RVA: 0x001BCE22 File Offset: 0x001BB022
	private static Network.HistSubSpellEnd GetSubSpellEnd(PowerHistorySubSpellEnd subSpellEnd)
	{
		return new Network.HistSubSpellEnd();
	}

	// Token: 0x06005508 RID: 21768 RVA: 0x001BCE29 File Offset: 0x001BB029
	private static Network.HistVoSpell GetVoSpell(PowerHistoryVoTask voSubspellTask)
	{
		return Network.HistVoSpell.CreateFromProto(voSubspellTask);
	}

	// Token: 0x06005509 RID: 21769 RVA: 0x001BCE31 File Offset: 0x001BB031
	private static Network.HistCachedTagForDormantChange GetCachedTagForDormantChange(PowerHistoryCachedTagForDormantChange tagChange)
	{
		return Network.HistCachedTagForDormantChange.CreateFromProto(tagChange);
	}

	// Token: 0x0600550A RID: 21770 RVA: 0x001BCE39 File Offset: 0x001BB039
	private static Network.HistShuffleDeck GetShuffleDeck(PowerHistoryShuffleDeck shuffleDeck)
	{
		return Network.HistShuffleDeck.CreateFromProto(shuffleDeck);
	}

	// Token: 0x0600550B RID: 21771 RVA: 0x001BCE44 File Offset: 0x001BB044
	private static List<int> MakeChoicesList(int choice1, int choice2, int choice3)
	{
		List<int> list = new List<int>();
		if (choice1 == 0)
		{
			return null;
		}
		list.Add(choice1);
		if (choice2 == 0)
		{
			return list;
		}
		list.Add(choice2);
		if (choice3 == 0)
		{
			return list;
		}
		list.Add(choice3);
		return list;
	}

	// Token: 0x0600550C RID: 21772 RVA: 0x001BCE7C File Offset: 0x001BB07C
	public void ValidateAchieve(int achieveID)
	{
		global::Log.Achievements.Print("Validating achieve: " + achieveID, Array.Empty<object>());
		this.m_connectApi.ValidateAchieve(achieveID);
	}

	// Token: 0x0600550D RID: 21773 RVA: 0x001BCEA9 File Offset: 0x001BB0A9
	public ValidateAchieveResponse GetValidatedAchieve()
	{
		return this.m_connectApi.GetValidateAchieveResponse();
	}

	// Token: 0x0600550E RID: 21774 RVA: 0x001BCEB6 File Offset: 0x001BB0B6
	public void RequestCancelQuest(int achieveID)
	{
		this.m_connectApi.RequestCancelQuest(achieveID);
	}

	// Token: 0x0600550F RID: 21775 RVA: 0x001BCEC4 File Offset: 0x001BB0C4
	public Network.CanceledQuest GetCanceledQuest()
	{
		CancelQuestResponse canceledQuestResponse = this.m_connectApi.GetCanceledQuestResponse();
		if (canceledQuestResponse == null)
		{
			return null;
		}
		return new Network.CanceledQuest
		{
			AchieveID = canceledQuestResponse.QuestId,
			Canceled = canceledQuestResponse.Success,
			NextQuestCancelDate = (canceledQuestResponse.HasNextQuestCancel ? global::TimeUtils.PegDateToFileTimeUtc(canceledQuestResponse.NextQuestCancel) : 0L)
		};
	}

	// Token: 0x06005510 RID: 21776 RVA: 0x001BCF1C File Offset: 0x001BB11C
	public Network.TriggeredEvent GetTriggerEventResponse()
	{
		TriggerEventResponse triggerEventResponse = this.m_connectApi.GetTriggerEventResponse();
		if (triggerEventResponse == null)
		{
			return null;
		}
		return new Network.TriggeredEvent
		{
			EventID = triggerEventResponse.EventId,
			Success = triggerEventResponse.Success
		};
	}

	// Token: 0x06005511 RID: 21777 RVA: 0x001BCF57 File Offset: 0x001BB157
	public void RequestAdventureProgress()
	{
		this.m_connectApi.RequestAdventureProgress();
	}

	// Token: 0x06005512 RID: 21778 RVA: 0x001BCF64 File Offset: 0x001BB164
	public List<Network.AdventureProgress> GetAdventureProgressResponse()
	{
		AdventureProgressResponse adventureProgressResponse = this.m_connectApi.GetAdventureProgressResponse();
		if (adventureProgressResponse == null)
		{
			return null;
		}
		List<Network.AdventureProgress> list = new List<Network.AdventureProgress>();
		for (int i = 0; i < adventureProgressResponse.List.Count; i++)
		{
			PegasusShared.AdventureProgress adventureProgress = adventureProgressResponse.List[i];
			list.Add(new Network.AdventureProgress
			{
				Wing = adventureProgress.WingId,
				Progress = adventureProgress.Progress,
				Ack = adventureProgress.Ack,
				Flags = adventureProgress.Flags_
			});
		}
		return list;
	}

	// Token: 0x06005513 RID: 21779 RVA: 0x001BCFF0 File Offset: 0x001BB1F0
	public Network.BeginDraft GetBeginDraft()
	{
		DraftBeginning draftBeginning = this.m_connectApi.GetDraftBeginning();
		if (draftBeginning == null)
		{
			return null;
		}
		Network.BeginDraft beginDraft = new Network.BeginDraft();
		beginDraft.DeckID = draftBeginning.DeckId;
		for (int i = 0; i < draftBeginning.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftBeginning.ChoiceList[i];
			NetCache.CardDefinition item = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset, false),
				Premium = (TAG_PREMIUM)cardDef.Premium
			};
			beginDraft.Heroes.Add(item);
		}
		beginDraft.Wins = (draftBeginning.HasCurrentSession ? draftBeginning.CurrentSession.Wins : 0);
		beginDraft.MaxSlot = draftBeginning.MaxSlot;
		if (draftBeginning.HasCurrentSession)
		{
			beginDraft.Session = draftBeginning.CurrentSession;
		}
		beginDraft.SlotType = draftBeginning.SlotType;
		beginDraft.UniqueSlotTypesForDraft = draftBeginning.UniqueSlotTypes;
		return beginDraft;
	}

	// Token: 0x06005514 RID: 21780 RVA: 0x001BD0CA File Offset: 0x001BB2CA
	public DraftError GetDraftError()
	{
		return this.m_connectApi.DraftGetError();
	}

	// Token: 0x06005515 RID: 21781 RVA: 0x001BD0D8 File Offset: 0x001BB2D8
	public Network.DraftChoicesAndContents GetDraftChoicesAndContents()
	{
		PegasusUtil.DraftChoicesAndContents draftChoicesAndContents = this.m_connectApi.GetDraftChoicesAndContents();
		if (draftChoicesAndContents == null)
		{
			return null;
		}
		Network.DraftChoicesAndContents draftChoicesAndContents2 = new Network.DraftChoicesAndContents();
		draftChoicesAndContents2.DeckInfo.Deck = draftChoicesAndContents.DeckId;
		draftChoicesAndContents2.Slot = draftChoicesAndContents.Slot;
		draftChoicesAndContents2.Hero.Name = ((draftChoicesAndContents.HeroDef.Asset == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(draftChoicesAndContents.HeroDef.Asset, false));
		draftChoicesAndContents2.Hero.Premium = (TAG_PREMIUM)draftChoicesAndContents.HeroDef.Premium;
		draftChoicesAndContents2.Wins = draftChoicesAndContents.CurrentSession.Wins;
		draftChoicesAndContents2.Losses = draftChoicesAndContents.CurrentSession.Losses;
		draftChoicesAndContents2.MaxWins = (draftChoicesAndContents.HasMaxWins ? draftChoicesAndContents.MaxWins : int.MaxValue);
		draftChoicesAndContents2.MaxSlot = draftChoicesAndContents.MaxSlot;
		if (draftChoicesAndContents.HasCurrentSession)
		{
			draftChoicesAndContents2.Session = draftChoicesAndContents.CurrentSession;
		}
		if (draftChoicesAndContents.HasHeroPowerDef)
		{
			draftChoicesAndContents2.HeroPower.Name = ((draftChoicesAndContents.HeroPowerDef.Asset == 0) ? string.Empty : GameUtils.TranslateDbIdToCardId(draftChoicesAndContents.HeroPowerDef.Asset, false));
		}
		for (int i = 0; i < draftChoicesAndContents.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftChoicesAndContents.ChoiceList[i];
			if (cardDef.Asset != 0)
			{
				NetCache.CardDefinition item = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset, false),
					Premium = (TAG_PREMIUM)cardDef.Premium
				};
				draftChoicesAndContents2.Choices.Add(item);
			}
		}
		for (int j = 0; j < draftChoicesAndContents.Cards.Count; j++)
		{
			DeckCardData deckCardData = draftChoicesAndContents.Cards[j];
			Network.CardUserData cardUserData = new Network.CardUserData();
			cardUserData.DbId = deckCardData.Def.Asset;
			cardUserData.Count = (deckCardData.HasQty ? deckCardData.Qty : 1);
			cardUserData.Premium = (TAG_PREMIUM)(deckCardData.Def.HasPremium ? deckCardData.Def.Premium : 0);
			draftChoicesAndContents2.DeckInfo.Cards.Add(cardUserData);
		}
		draftChoicesAndContents2.Chest = (draftChoicesAndContents.HasChest ? Network.ConvertRewardChest(draftChoicesAndContents.Chest) : null);
		draftChoicesAndContents2.SlotType = draftChoicesAndContents.SlotType;
		draftChoicesAndContents2.UniqueSlotTypesForDraft.AddRange(draftChoicesAndContents.UniqueSlotTypes);
		return draftChoicesAndContents2;
	}

	// Token: 0x06005516 RID: 21782 RVA: 0x001BD324 File Offset: 0x001BB524
	public Network.DraftChosen GetDraftChosen()
	{
		PegasusUtil.DraftChosen draftChosen = this.m_connectApi.GetDraftChosen();
		if (draftChosen == null)
		{
			return null;
		}
		NetCache.CardDefinition chosenCard = new NetCache.CardDefinition
		{
			Name = GameUtils.TranslateDbIdToCardId(draftChosen.Chosen.Asset, false),
			Premium = (TAG_PREMIUM)draftChosen.Chosen.Premium
		};
		List<NetCache.CardDefinition> list = new List<NetCache.CardDefinition>();
		for (int i = 0; i < draftChosen.NextChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftChosen.NextChoiceList[i];
			NetCache.CardDefinition item = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset, false),
				Premium = (TAG_PREMIUM)cardDef.Premium
			};
			list.Add(item);
		}
		return new Network.DraftChosen
		{
			ChosenCard = chosenCard,
			NextChoices = list,
			SlotType = draftChosen.SlotType
		};
	}

	// Token: 0x06005517 RID: 21783 RVA: 0x001BD3E8 File Offset: 0x001BB5E8
	public void MakeDraftChoice(long deckID, int slot, int index, int premium)
	{
		this.m_connectApi.DraftMakePick(deckID, slot, index, premium);
	}

	// Token: 0x06005518 RID: 21784 RVA: 0x001BD3FA File Offset: 0x001BB5FA
	public void RequestDraftChoicesAndContents()
	{
		this.m_connectApi.RequestDraftChoicesAndContents();
	}

	// Token: 0x06005519 RID: 21785 RVA: 0x001BD407 File Offset: 0x001BB607
	public void SendArenaSessionRequest()
	{
		this.m_connectApi.SendArenaSessionRequest();
	}

	// Token: 0x0600551A RID: 21786 RVA: 0x001BD414 File Offset: 0x001BB614
	public ArenaSessionResponse GetArenaSessionResponse()
	{
		return this.m_connectApi.GetArenaSessionResponse();
	}

	// Token: 0x0600551B RID: 21787 RVA: 0x001BD421 File Offset: 0x001BB621
	public void DraftBegin()
	{
		this.m_connectApi.DraftBegin();
	}

	// Token: 0x0600551C RID: 21788 RVA: 0x001BD42E File Offset: 0x001BB62E
	public void DraftRetire(long deckID, int slot, int seasonId)
	{
		this.m_connectApi.DraftRetire(deckID, slot, seasonId);
	}

	// Token: 0x0600551D RID: 21789 RVA: 0x001BD440 File Offset: 0x001BB640
	public Network.DraftRetired GetRetiredDraft()
	{
		PegasusUtil.DraftRetired draftRetired = this.m_connectApi.GetDraftRetired();
		if (draftRetired == null)
		{
			return null;
		}
		return new Network.DraftRetired
		{
			Deck = draftRetired.DeckId,
			Chest = Network.ConvertRewardChest(draftRetired.Chest)
		};
	}

	// Token: 0x0600551E RID: 21790 RVA: 0x001BD480 File Offset: 0x001BB680
	public void AckDraftRewards(long deckID, int slot)
	{
		this.m_connectApi.DraftAckRewards(deckID, slot);
	}

	// Token: 0x0600551F RID: 21791 RVA: 0x001BD490 File Offset: 0x001BB690
	public long GetRewardsAckDraftID()
	{
		DraftRewardsAcked draftRewardsAcked = this.m_connectApi.DraftRewardsAcked();
		if (draftRewardsAcked == null)
		{
			return 0L;
		}
		return draftRewardsAcked.DeckId;
	}

	// Token: 0x06005520 RID: 21792 RVA: 0x001BD4B5 File Offset: 0x001BB6B5
	public void DraftRequestDisablePremiums()
	{
		this.m_connectApi.DraftRequestDisablePremiums();
	}

	// Token: 0x06005521 RID: 21793 RVA: 0x001BD4C4 File Offset: 0x001BB6C4
	public Network.DraftChoicesAndContents GetDraftRemovePremiumsResponse()
	{
		DraftRemovePremiumsResponse draftDisablePremiumsResponse = this.m_connectApi.GetDraftDisablePremiumsResponse();
		Network.DraftChoicesAndContents draftChoicesAndContents = new Network.DraftChoicesAndContents();
		for (int i = 0; i < draftDisablePremiumsResponse.ChoiceList.Count; i++)
		{
			PegasusShared.CardDef cardDef = draftDisablePremiumsResponse.ChoiceList[i];
			if (cardDef.Asset != 0)
			{
				NetCache.CardDefinition item = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(cardDef.Asset, false),
					Premium = (TAG_PREMIUM)cardDef.Premium
				};
				draftChoicesAndContents.Choices.Add(item);
			}
		}
		for (int j = 0; j < draftDisablePremiumsResponse.Cards.Count; j++)
		{
			DeckCardData deckCardData = draftDisablePremiumsResponse.Cards[j];
			Network.CardUserData cardUserData = new Network.CardUserData();
			cardUserData.DbId = deckCardData.Def.Asset;
			cardUserData.Count = (deckCardData.HasQty ? deckCardData.Qty : 1);
			cardUserData.Premium = (TAG_PREMIUM)(deckCardData.Def.HasPremium ? deckCardData.Def.Premium : 0);
			draftChoicesAndContents.DeckInfo.Cards.Add(cardUserData);
		}
		return draftChoicesAndContents;
	}

	// Token: 0x06005522 RID: 21794 RVA: 0x001BD5D8 File Offset: 0x001BB7D8
	public static Network.RewardChest ConvertRewardChest(PegasusShared.RewardChest chest)
	{
		Network.RewardChest rewardChest = new Network.RewardChest();
		for (int i = 0; i < chest.Bag.Count; i++)
		{
			rewardChest.Rewards.Add(Network.ConvertRewardBag(chest.Bag[i]));
		}
		return rewardChest;
	}

	// Token: 0x06005523 RID: 21795 RVA: 0x001BD620 File Offset: 0x001BB820
	public static RewardData ConvertRewardBag(RewardBag bag)
	{
		if (bag.HasRewardBooster)
		{
			return new BoosterPackRewardData(bag.RewardBooster.BoosterType, bag.RewardBooster.BoosterCount);
		}
		if (bag.HasRewardCard)
		{
			return new CardRewardData(GameUtils.TranslateDbIdToCardId(bag.RewardCard.Card.Asset, false), (TAG_PREMIUM)bag.RewardCard.Card.Premium, bag.RewardCard.Quantity);
		}
		if (bag.HasRewardDust)
		{
			return new ArcaneDustRewardData(bag.RewardDust.Amount);
		}
		if (bag.HasRewardGold)
		{
			return new GoldRewardData((long)bag.RewardGold.Amount);
		}
		if (bag.HasRewardCardBack)
		{
			return new CardBackRewardData(bag.RewardCardBack.CardBack);
		}
		if (bag.HasRewardArenaTicket)
		{
			return new ForgeTicketRewardData(bag.RewardArenaTicket.Quantity);
		}
		Debug.LogError("Unrecognized reward bag reward");
		return null;
	}

	// Token: 0x06005524 RID: 21796 RVA: 0x001BD700 File Offset: 0x001BB900
	public void MassDisenchant()
	{
		this.m_connectApi.MassDisenchant();
	}

	// Token: 0x06005525 RID: 21797 RVA: 0x001BD710 File Offset: 0x001BB910
	public Network.MassDisenchantResponse GetMassDisenchantResponse()
	{
		PegasusUtil.MassDisenchantResponse massDisenchantResponse = this.m_connectApi.GetMassDisenchantResponse();
		if (massDisenchantResponse == null)
		{
			return null;
		}
		if (massDisenchantResponse.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(massDisenchantResponse.CollectionVersion);
		}
		return new Network.MassDisenchantResponse
		{
			Amount = massDisenchantResponse.Amount
		};
	}

	// Token: 0x06005526 RID: 21798 RVA: 0x001BD758 File Offset: 0x001BB958
	public void SetFavoriteHero(TAG_CLASS heroClass, NetCache.CardDefinition hero)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = GameUtils.TranslateCardIdToDbId(hero.Name, false),
			Premium = (int)hero.Premium
		};
		if (Network.IsLoggedIn())
		{
			this.m_connectApi.SetFavoriteHero((int)heroClass, cardDef);
			return;
		}
		OfflineDataCache.SetFavoriteHero((int)heroClass, cardDef, true);
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject != null)
		{
			netObject.FavoriteHeroes[heroClass] = hero;
		}
	}

	// Token: 0x06005527 RID: 21799 RVA: 0x001BD7C1 File Offset: 0x001BB9C1
	public void SetTag(int tagID, int entityID, int tagValue)
	{
		this.SendDebugConsoleCommand(string.Format("settag {0} {1} {2}", tagID, entityID, tagValue));
	}

	// Token: 0x06005528 RID: 21800 RVA: 0x001BD7E6 File Offset: 0x001BB9E6
	public void SetTag(int tagID, string entityIdentifier, int tagValue)
	{
		this.SendDebugConsoleCommand(string.Format("settag {0} {1} {2}", tagID, entityIdentifier, tagValue));
	}

	// Token: 0x06005529 RID: 21801 RVA: 0x001BD806 File Offset: 0x001BBA06
	public void PrintPersistentList(int entityID)
	{
		this.SendDebugConsoleCommand(string.Format("printpersistentlist {0}", entityID));
	}

	// Token: 0x0600552A RID: 21802 RVA: 0x001BD81F File Offset: 0x001BBA1F
	public void DebugScript(string powerGUID)
	{
		this.SendDebugConsoleCommand(string.Format("debugscript {0}", powerGUID));
	}

	// Token: 0x0600552B RID: 21803 RVA: 0x001BD833 File Offset: 0x001BBA33
	public void DisableScriptDebug()
	{
		this.SendDebugConsoleCommand("disablescriptdebug");
	}

	// Token: 0x0600552C RID: 21804 RVA: 0x001BD841 File Offset: 0x001BBA41
	public void DebugRopeTimer()
	{
		this.SendDebugConsoleCommand("debugropetimer");
	}

	// Token: 0x0600552D RID: 21805 RVA: 0x001BD84F File Offset: 0x001BBA4F
	public void DisableDebugRopeTimer()
	{
		this.SendDebugConsoleCommand("disabledebugropetimer");
	}

	// Token: 0x0600552E RID: 21806 RVA: 0x001BD860 File Offset: 0x001BBA60
	public Network.SetFavoriteHeroResponse GetSetFavoriteHeroResponse()
	{
		PegasusUtil.SetFavoriteHeroResponse setFavoriteHeroResponse = this.m_connectApi.GetSetFavoriteHeroResponse();
		if (setFavoriteHeroResponse == null)
		{
			return null;
		}
		Network.SetFavoriteHeroResponse setFavoriteHeroResponse2 = new Network.SetFavoriteHeroResponse();
		setFavoriteHeroResponse2.Success = setFavoriteHeroResponse.Success;
		if (setFavoriteHeroResponse.HasFavoriteHero)
		{
			if (!global::EnumUtils.TryCast<TAG_CLASS>(setFavoriteHeroResponse.FavoriteHero.ClassId, out setFavoriteHeroResponse2.HeroClass))
			{
				Debug.LogWarning(string.Format("Network.GetSetFavoriteHeroResponse() invalid class {0}", setFavoriteHeroResponse.FavoriteHero.ClassId));
			}
			TAG_PREMIUM premium;
			if (!global::EnumUtils.TryCast<TAG_PREMIUM>(setFavoriteHeroResponse.FavoriteHero.Hero.Premium, out premium))
			{
				Debug.LogWarning(string.Format("Network.GetSetFavoriteHeroResponse() invalid heroPremium {0}", setFavoriteHeroResponse.FavoriteHero.Hero.Premium));
			}
			setFavoriteHeroResponse2.Hero = new NetCache.CardDefinition
			{
				Name = GameUtils.TranslateDbIdToCardId(setFavoriteHeroResponse.FavoriteHero.Hero.Asset, false),
				Premium = premium
			};
		}
		return setFavoriteHeroResponse2;
	}

	// Token: 0x0600552F RID: 21807 RVA: 0x001BD948 File Offset: 0x001BBB48
	public void RequestRecruitAFriendUrl()
	{
		this.m_connectApi.RequestRecruitAFriendUrl(this.GetPlatformBuilder());
	}

	// Token: 0x06005530 RID: 21808 RVA: 0x001BD95B File Offset: 0x001BBB5B
	public RecruitAFriendURLResponse GetRecruitAFriendUrlResponse()
	{
		return this.m_connectApi.GetRecruitAFriendUrlResponse();
	}

	// Token: 0x06005531 RID: 21809 RVA: 0x001BD968 File Offset: 0x001BBB68
	public void RequestRecruitAFriendData()
	{
		this.m_connectApi.RequestRecruitAFriendData();
	}

	// Token: 0x06005532 RID: 21810 RVA: 0x001BD975 File Offset: 0x001BBB75
	public RecruitAFriendDataResponse GetRecruitAFriendDataResponse()
	{
		return this.m_connectApi.GetRecruitAFriendDataResponse();
	}

	// Token: 0x06005533 RID: 21811 RVA: 0x001BD982 File Offset: 0x001BBB82
	public void RequestProcessRecruitAFriend()
	{
		this.m_connectApi.RequestProcessRecruitAFriend();
	}

	// Token: 0x06005534 RID: 21812 RVA: 0x001BD98F File Offset: 0x001BBB8F
	public ProcessRecruitAFriendResponse GetProcessRecruitAFriendResponse()
	{
		return this.m_connectApi.GetProcessRecruitAFriendResponse();
	}

	// Token: 0x06005535 RID: 21813 RVA: 0x001BD99C File Offset: 0x001BBB9C
	public Network.PurchaseCanceledResponse GetPurchaseCanceledResponse()
	{
		CancelPurchaseResponse cancelPurchaseResponse = this.m_connectApi.GetCancelPurchaseResponse();
		if (cancelPurchaseResponse == null)
		{
			return null;
		}
		Network.PurchaseCanceledResponse purchaseCanceledResponse = new Network.PurchaseCanceledResponse
		{
			TransactionID = (cancelPurchaseResponse.HasTransactionId ? cancelPurchaseResponse.TransactionId : 0L),
			PMTProductID = (cancelPurchaseResponse.HasPmtProductId ? new long?(cancelPurchaseResponse.PmtProductId) : null),
			CurrencyCode = cancelPurchaseResponse.CurrencyCode
		};
		switch (cancelPurchaseResponse.Result)
		{
		case CancelPurchaseResponse.CancelResult.CR_SUCCESS:
			purchaseCanceledResponse.Result = Network.PurchaseCanceledResponse.CancelResult.SUCCESS;
			break;
		case CancelPurchaseResponse.CancelResult.CR_NOT_ALLOWED:
			purchaseCanceledResponse.Result = Network.PurchaseCanceledResponse.CancelResult.NOT_ALLOWED;
			break;
		case CancelPurchaseResponse.CancelResult.CR_NOTHING_TO_CANCEL:
			purchaseCanceledResponse.Result = Network.PurchaseCanceledResponse.CancelResult.NOTHING_TO_CANCEL;
			break;
		}
		return purchaseCanceledResponse;
	}

	// Token: 0x06005536 RID: 21814 RVA: 0x001BDA40 File Offset: 0x001BBC40
	public Network.BattlePayStatus GetBattlePayStatusResponse()
	{
		BattlePayStatusResponse battlePayStatusResponse = this.m_connectApi.GetBattlePayStatusResponse();
		if (battlePayStatusResponse == null)
		{
			return null;
		}
		Network.BattlePayStatus battlePayStatus = new Network.BattlePayStatus
		{
			State = (Network.BattlePayStatus.PurchaseState)battlePayStatusResponse.Status,
			BattlePayAvailable = battlePayStatusResponse.BattlePayAvailable,
			CurrencyCode = battlePayStatusResponse.CurrencyCode
		};
		if (battlePayStatusResponse.HasTransactionId)
		{
			battlePayStatus.TransactionID = battlePayStatusResponse.TransactionId;
		}
		if (battlePayStatusResponse.HasPmtProductId)
		{
			battlePayStatus.PMTProductID = new long?(battlePayStatusResponse.PmtProductId);
		}
		if (battlePayStatusResponse.HasPurchaseError)
		{
			battlePayStatus.PurchaseError = this.ConvertPurchaseError(battlePayStatusResponse.PurchaseError);
		}
		if (battlePayStatusResponse.HasThirdPartyId)
		{
			battlePayStatus.ThirdPartyID = battlePayStatusResponse.ThirdPartyId;
		}
		if (battlePayStatusResponse.HasProvider)
		{
			battlePayStatus.Provider = new BattlePayProvider?(battlePayStatusResponse.Provider);
		}
		return battlePayStatus;
	}

	// Token: 0x06005537 RID: 21815 RVA: 0x001BDB00 File Offset: 0x001BBD00
	private Network.PurchaseErrorInfo ConvertPurchaseError(PurchaseError purchaseError)
	{
		Network.PurchaseErrorInfo purchaseErrorInfo = new Network.PurchaseErrorInfo
		{
			Error = (Network.PurchaseErrorInfo.ErrorType)purchaseError.Error_
		};
		if (purchaseError.HasPurchaseInProgress)
		{
			purchaseErrorInfo.PurchaseInProgressProductID = purchaseError.PurchaseInProgress;
		}
		if (purchaseError.HasErrorCode)
		{
			purchaseErrorInfo.ErrorCode = purchaseError.ErrorCode;
		}
		return purchaseErrorInfo;
	}

	// Token: 0x06005538 RID: 21816 RVA: 0x001BDB48 File Offset: 0x001BBD48
	private static Dictionary<string, string> ConvertProductAttributesFromProtobuf(List<ProductAttribute> protoAttributes)
	{
		if (protoAttributes == null || protoAttributes.Count == 0)
		{
			return null;
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>(protoAttributes.Count);
		foreach (ProductAttribute productAttribute in protoAttributes)
		{
			if (productAttribute.HasName && productAttribute.HasValue && !string.IsNullOrEmpty(productAttribute.Name) && !string.IsNullOrEmpty(productAttribute.Value))
			{
				dictionary[productAttribute.Name.ToLowerInvariant()] = productAttribute.Value.ToLowerInvariant();
			}
		}
		return dictionary;
	}

	// Token: 0x06005539 RID: 21817 RVA: 0x001BDBF0 File Offset: 0x001BBDF0
	public Network.BattlePayConfig GetBattlePayConfigResponse()
	{
		BattlePayConfigResponse battlePayConfigResponse = this.m_connectApi.GetBattlePayConfigResponse();
		if (battlePayConfigResponse == null)
		{
			return null;
		}
		Network.BattlePayConfig battlePayConfig = new Network.BattlePayConfig
		{
			Available = (!battlePayConfigResponse.HasUnavailable || !battlePayConfigResponse.Unavailable),
			SecondsBeforeAutoCancel = (battlePayConfigResponse.HasSecsBeforeAutoCancel ? battlePayConfigResponse.SecsBeforeAutoCancel : StoreManager.DEFAULT_SECONDS_BEFORE_AUTO_CANCEL)
		};
		if (battlePayConfigResponse.HasCheckoutKrOnestoreKey)
		{
			battlePayConfig.CheckoutKrOnestoreKey = battlePayConfigResponse.CheckoutKrOnestoreKey;
		}
		for (int i = 0; i < battlePayConfigResponse.Currencies.Count; i++)
		{
			global::Currency currency = new global::Currency(battlePayConfigResponse.Currencies[i]);
			battlePayConfig.Currencies.Add(currency);
			if (currency.Code == battlePayConfigResponse.DefaultCurrencyCode)
			{
				battlePayConfig.Currency = currency;
			}
		}
		for (int j = 0; j < battlePayConfigResponse.Bundles.Count; j++)
		{
			PegasusUtil.Bundle bundle = battlePayConfigResponse.Bundles[j];
			Network.Bundle bundle2 = new Network.Bundle
			{
				AppleID = (bundle.HasAppleId ? bundle.AppleId : string.Empty),
				GooglePlayID = (bundle.HasGooglePlayId ? bundle.GooglePlayId : string.Empty),
				AmazonID = (bundle.HasAmazonId ? bundle.AmazonId : string.Empty),
				OneStoreID = (bundle.HasKronestoreId ? bundle.KronestoreId : string.Empty),
				ExclusiveProviders = bundle.ExclusiveProviders,
				IsPrePurchase = bundle.IsPrePurchase,
				PMTProductID = new long?(bundle.PmtProductId),
				DisplayName = (bundle.HasDisplayName ? DbfUtils.ConvertFromProtobuf(bundle.DisplayName) : null),
				DisplayDescription = (bundle.HasDisplayDesc ? DbfUtils.ConvertFromProtobuf(bundle.DisplayDesc) : null),
				Attributes = Network.ConvertProductAttributesFromProtobuf(bundle.Attributes),
				SaleIds = bundle.SaleIds.ToList<int>(),
				VisibleOnSalePeriodOnly = bundle.VisibleOnSalePeriodOnly
			};
			string tagsString;
			if (bundle2.Attributes != null && bundle2.Attributes.TryGetValue("tags", out tagsString))
			{
				IEnumerable<string> enumerable = CatalogUtils.ParseTagsString(tagsString);
				if (enumerable != null && enumerable.Contains("prepurchase"))
				{
					bundle2.IsPrePurchase = true;
				}
			}
			if (bundle.HasCost && bundle.Cost > 0UL)
			{
				bundle2.Cost = new ulong?(bundle.Cost);
				bundle2.CostDisplay = new double?(bundle.Cost / battlePayConfig.Currency.RoundingOffset());
			}
			if (bundle.HasGoldCost && bundle.GoldCost > 0L)
			{
				bundle2.GtappGoldCost = new long?(bundle.GoldCost);
			}
			if (bundle.VirtualCurrencyCost != null)
			{
				bundle2.VirtualCurrencyCost = new long?(bundle.VirtualCurrencyCost.Cost);
				bundle2.VirtualCurrencyCode = bundle.VirtualCurrencyCost.CurrencyCode;
			}
			if (bundle.HasProductEventName)
			{
				bundle2.ProductEvent = bundle.ProductEventName;
			}
			for (int k = 0; k < bundle.Items.Count; k++)
			{
				PegasusUtil.BundleItem bundleItem = bundle.Items[k];
				Network.BundleItem bundleItem2 = new Network.BundleItem
				{
					ItemType = bundleItem.ProductType,
					ProductData = bundleItem.Data,
					Quantity = bundleItem.Quantity,
					BaseQuantity = bundleItem.BaseQuantity
				};
				foreach (ProductAttribute productAttribute in bundleItem.Attributes)
				{
					bundleItem2.Attributes[productAttribute.Name] = productAttribute.Value;
				}
				bundle2.Items.Add(bundleItem2);
			}
			battlePayConfig.Bundles.Add(bundle2);
		}
		for (int l = 0; l < battlePayConfigResponse.GoldCostBoosters.Count; l++)
		{
			PegasusUtil.GoldCostBooster goldCostBooster = battlePayConfigResponse.GoldCostBoosters[l];
			Network.GoldCostBooster goldCostBooster2 = new Network.GoldCostBooster
			{
				ID = goldCostBooster.PackType
			};
			if (goldCostBooster.Cost > 0L)
			{
				goldCostBooster2.Cost = new long?(goldCostBooster.Cost);
			}
			else
			{
				goldCostBooster2.Cost = null;
			}
			if (goldCostBooster.HasBuyWithGoldEventName)
			{
				goldCostBooster2.BuyWithGoldEvent = SpecialEventManager.GetEventType(goldCostBooster.BuyWithGoldEventName);
			}
			battlePayConfig.GoldCostBoosters.Add(goldCostBooster2);
		}
		if (battlePayConfigResponse.HasGoldCostArena && battlePayConfigResponse.GoldCostArena > 0L)
		{
			battlePayConfig.GoldCostArena = new long?(battlePayConfigResponse.GoldCostArena);
		}
		else
		{
			battlePayConfig.GoldCostArena = null;
		}
		if (battlePayConfigResponse.HasCheckoutOauthClientId && !string.IsNullOrEmpty(battlePayConfigResponse.CheckoutOauthClientId))
		{
			battlePayConfig.CommerceClientID = battlePayConfigResponse.CheckoutOauthClientId;
		}
		if (battlePayConfigResponse.HasPersonalizedShopPageId && !string.IsNullOrEmpty(battlePayConfigResponse.PersonalizedShopPageId))
		{
			battlePayConfig.PersonalizedShopPageID = battlePayConfigResponse.PersonalizedShopPageId;
		}
		if (battlePayConfigResponse.LocaleMap != null)
		{
			foreach (LocaleMapEntry localeMapEntry in battlePayConfigResponse.LocaleMap)
			{
				battlePayConfig.CatalogLocaleToGameLocale.Add(localeMapEntry.CatalogLocaleId, (Locale)localeMapEntry.GameLocaleId);
			}
		}
		foreach (object obj in Enum.GetValues(typeof(Locale)))
		{
			Locale locale = (Locale)obj;
			if (locale != Locale.UNKNOWN && !battlePayConfig.CatalogLocaleToGameLocale.ContainsValue(locale))
			{
				global::Log.Store.PrintError("BattlePayConfig includes no catalog locale ID mapping for {0}", new object[]
				{
					locale.ToString()
				});
			}
		}
		battlePayConfig.SaleList = CatalogDeserializer.DeserializeShopSaleList(battlePayConfigResponse.SaleList);
		battlePayConfig.IgnoreProductTiming = battlePayConfigResponse.IgnoreProductTiming;
		return battlePayConfig;
	}

	// Token: 0x0600553A RID: 21818 RVA: 0x001BE1EC File Offset: 0x001BC3EC
	public void PurchaseViaGold(int quantity, ProductType productItemType, int data)
	{
		if (!Network.IsLoggedIn())
		{
			global::Log.All.PrintError("Client attempted to make a gold purchase while offline!", Array.Empty<object>());
			return;
		}
		this.m_connectApi.PurchaseViaGold(quantity, productItemType, data);
	}

	// Token: 0x0600553B RID: 21819 RVA: 0x001BE218 File Offset: 0x001BC418
	public void GetPurchaseMethod(long? pmtProductId, int quantity, global::Currency currency)
	{
		this.m_connectApi.RequestPurchaseMethod(pmtProductId, quantity, currency.toProto(), SystemInfo.deviceUniqueIdentifier, this.GetPlatformBuilder());
	}

	// Token: 0x0600553C RID: 21820 RVA: 0x001BE238 File Offset: 0x001BC438
	public void ConfirmPurchase()
	{
		this.m_connectApi.ConfirmPurchase();
	}

	// Token: 0x0600553D RID: 21821 RVA: 0x001BE245 File Offset: 0x001BC445
	public void BeginThirdPartyPurchase(BattlePayProvider provider, string pmtLegacyProductId, int quantity)
	{
		this.m_connectApi.BeginThirdPartyPurchase(SystemInfo.deviceUniqueIdentifier, provider, pmtLegacyProductId, quantity);
	}

	// Token: 0x0600553E RID: 21822 RVA: 0x001BE25A File Offset: 0x001BC45A
	public void BeginThirdPartyPurchaseWithReceipt(BattlePayProvider provider, string pmtLegacyProductId, int quantity, string thirdPartyId, string base64receipt, string thirdPartyUserId = "")
	{
		this.m_connectApi.BeginThirdPartyPurchaseWithReceipt(SystemInfo.deviceUniqueIdentifier, provider, pmtLegacyProductId, quantity, thirdPartyId, base64receipt, string.IsNullOrEmpty(thirdPartyUserId) ? null : thirdPartyUserId);
	}

	// Token: 0x0600553F RID: 21823 RVA: 0x001BE284 File Offset: 0x001BC484
	public void SubmitThirdPartyReceipt(long bpayId, BattlePayProvider provider, string pmtLegacyProductId, int quantity, string thirdPartyId, string base64receipt, string thirdPartyUserId = "")
	{
		this.m_connectApi.SubmitThirdPartyPurchaseReceipt(bpayId, provider, pmtLegacyProductId, SystemInfo.deviceUniqueIdentifier, quantity, thirdPartyId, base64receipt, thirdPartyUserId);
	}

	// Token: 0x06005540 RID: 21824 RVA: 0x001BE2AC File Offset: 0x001BC4AC
	public void GetThirdPartyPurchaseStatus(string transactionId)
	{
		this.m_connectApi.GetThirdPartyPurchaseStatus(transactionId);
	}

	// Token: 0x06005541 RID: 21825 RVA: 0x001BE2BA File Offset: 0x001BC4BA
	public void CancelBlizzardPurchase(bool isAutoCanceled, CancelPurchase.CancelReason? reason, string error)
	{
		this.m_connectApi.AbortBlizzardPurchase(SystemInfo.deviceUniqueIdentifier, isAutoCanceled, reason, error);
	}

	// Token: 0x06005542 RID: 21826 RVA: 0x001BE2CF File Offset: 0x001BC4CF
	public void CancelThirdPartyPurchase(CancelPurchase.CancelReason reason, string error)
	{
		this.m_connectApi.AbortThirdPartyPurchase(SystemInfo.deviceUniqueIdentifier, reason, error);
	}

	// Token: 0x06005543 RID: 21827 RVA: 0x001BE2E4 File Offset: 0x001BC4E4
	public Network.PurchaseMethod GetPurchaseMethodResponse()
	{
		PegasusUtil.PurchaseMethod purchaseMethodResponse = this.m_connectApi.GetPurchaseMethodResponse();
		if (purchaseMethodResponse == null)
		{
			return null;
		}
		Network.PurchaseMethod purchaseMethod = new Network.PurchaseMethod();
		if (purchaseMethodResponse.HasTransactionId)
		{
			purchaseMethod.TransactionID = purchaseMethodResponse.TransactionId;
		}
		if (purchaseMethodResponse.HasPmtProductId)
		{
			purchaseMethod.PMTProductID = new long?(purchaseMethodResponse.PmtProductId);
		}
		if (purchaseMethodResponse.HasQuantity)
		{
			purchaseMethod.Quantity = purchaseMethodResponse.Quantity;
		}
		purchaseMethod.CurrencyCode = purchaseMethodResponse.CurrencyCode;
		if (purchaseMethodResponse.HasWalletName)
		{
			purchaseMethod.WalletName = purchaseMethodResponse.WalletName;
		}
		if (purchaseMethodResponse.HasUseEbalance)
		{
			purchaseMethod.UseEBalance = purchaseMethodResponse.UseEbalance;
		}
		purchaseMethod.IsZeroCostLicense = (purchaseMethodResponse.HasIsZeroCostLicense && purchaseMethodResponse.IsZeroCostLicense);
		if (purchaseMethodResponse.HasChallengeId)
		{
			purchaseMethod.ChallengeID = purchaseMethodResponse.ChallengeId;
		}
		if (purchaseMethodResponse.HasChallengeUrl)
		{
			purchaseMethod.ChallengeURL = purchaseMethodResponse.ChallengeUrl;
		}
		if (purchaseMethodResponse.HasError)
		{
			purchaseMethod.PurchaseError = this.ConvertPurchaseError(purchaseMethodResponse.Error);
		}
		return purchaseMethod;
	}

	// Token: 0x06005544 RID: 21828 RVA: 0x001BE3D8 File Offset: 0x001BC5D8
	public Network.PurchaseResponse GetPurchaseResponse()
	{
		PegasusUtil.PurchaseResponse purchaseResponse = this.m_connectApi.GetPurchaseResponse();
		if (purchaseResponse == null)
		{
			return null;
		}
		return new Network.PurchaseResponse
		{
			PurchaseError = this.ConvertPurchaseError(purchaseResponse.Error),
			TransactionID = (purchaseResponse.HasTransactionId ? purchaseResponse.TransactionId : 0L),
			PMTProductID = (purchaseResponse.HasPmtProductId ? new long?(purchaseResponse.PmtProductId) : null),
			ThirdPartyID = (purchaseResponse.HasThirdPartyId ? purchaseResponse.ThirdPartyId : string.Empty),
			CurrencyCode = purchaseResponse.CurrencyCode
		};
	}

	// Token: 0x06005545 RID: 21829 RVA: 0x001BE470 File Offset: 0x001BC670
	public Network.PurchaseViaGoldResponse GetPurchaseWithGoldResponse()
	{
		PurchaseWithGoldResponse purchaseWithGoldResponse = this.m_connectApi.GetPurchaseWithGoldResponse();
		if (purchaseWithGoldResponse == null)
		{
			return null;
		}
		Network.PurchaseViaGoldResponse purchaseViaGoldResponse = new Network.PurchaseViaGoldResponse
		{
			Error = (Network.PurchaseViaGoldResponse.ErrorType)purchaseWithGoldResponse.Result
		};
		if (purchaseWithGoldResponse.HasGoldUsed)
		{
			purchaseViaGoldResponse.GoldUsed = purchaseWithGoldResponse.GoldUsed;
		}
		return purchaseViaGoldResponse;
	}

	// Token: 0x06005546 RID: 21830 RVA: 0x001BE4B8 File Offset: 0x001BC6B8
	public Network.ThirdPartyPurchaseStatusResponse GetThirdPartyPurchaseStatusResponse()
	{
		PegasusUtil.ThirdPartyPurchaseStatusResponse thirdPartyPurchaseStatusResponse = this.m_connectApi.GetThirdPartyPurchaseStatusResponse();
		if (thirdPartyPurchaseStatusResponse == null)
		{
			return null;
		}
		return new Network.ThirdPartyPurchaseStatusResponse
		{
			ThirdPartyID = thirdPartyPurchaseStatusResponse.ThirdPartyId,
			Status = (Network.ThirdPartyPurchaseStatusResponse.PurchaseStatus)thirdPartyPurchaseStatusResponse.Status_
		};
	}

	// Token: 0x06005547 RID: 21831 RVA: 0x001BE4F4 File Offset: 0x001BC6F4
	public Network.CardBackResponse GetCardBackResponse()
	{
		SetFavoriteCardBackResponse setFavoriteCardBackResponse = this.m_connectApi.GetSetFavoriteCardBackResponse();
		if (setFavoriteCardBackResponse == null)
		{
			return null;
		}
		return new Network.CardBackResponse
		{
			Success = setFavoriteCardBackResponse.Success,
			CardBack = setFavoriteCardBackResponse.CardBack
		};
	}

	// Token: 0x06005548 RID: 21832 RVA: 0x001BE530 File Offset: 0x001BC730
	public void SetFavoriteCardBack(int cardBack)
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject != null && cardBack != netObject.FavoriteCardBack)
		{
			this.m_connectApi.SetFavoriteCardBack(cardBack);
		}
		if (!Network.IsLoggedIn())
		{
			OfflineDataCache.SetFavoriteCardBack(cardBack);
			if (netObject != null)
			{
				NetCache.Get().ProcessNewFavoriteCardBack(cardBack);
			}
		}
	}

	// Token: 0x06005549 RID: 21833 RVA: 0x001BE57C File Offset: 0x001BC77C
	public NetCache.NetCacheCardBacks GetCardBacks()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheCardBacks();
		}
		CardBacks cardBacksPacket = this.GetCardBacksPacket();
		if (cardBacksPacket == null)
		{
			return null;
		}
		NetCache.NetCacheCardBacks netCacheCardBacks = new NetCache.NetCacheCardBacks();
		netCacheCardBacks.FavoriteCardBack = cardBacksPacket.FavoriteCardBack;
		for (int i = 0; i < cardBacksPacket.CardBacks_.Count; i++)
		{
			int item = cardBacksPacket.CardBacks_[i];
			netCacheCardBacks.CardBacks.Add(item);
		}
		return netCacheCardBacks;
	}

	// Token: 0x0600554A RID: 21834 RVA: 0x001BE5E5 File Offset: 0x001BC7E5
	public CardBacks GetCardBacksPacket()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return null;
		}
		return this.m_connectApi.GetCardBacks();
	}

	// Token: 0x0600554B RID: 21835 RVA: 0x001BE5FC File Offset: 0x001BC7FC
	public Network.CoinResponse GetCoinResponse()
	{
		SetFavoriteCoinResponse setFavoriteCoinResponse = this.m_connectApi.GetSetFavoriteCoinResponse();
		if (setFavoriteCoinResponse == null)
		{
			return null;
		}
		return new Network.CoinResponse
		{
			Success = setFavoriteCoinResponse.Success,
			Coin = setFavoriteCoinResponse.CoinId
		};
	}

	// Token: 0x0600554C RID: 21836 RVA: 0x001BE638 File Offset: 0x001BC838
	public void SetFavoriteCoin(int coin)
	{
		NetCache.NetCacheCoins netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCoins>();
		if (netObject != null && coin != netObject.FavoriteCoin)
		{
			this.m_connectApi.SetFavoriteCoin(coin);
		}
		if (!Network.IsLoggedIn())
		{
			OfflineDataCache.SetFavoriteCoin(coin);
			if (netObject != null)
			{
				NetCache.Get().ProcessNewFavoriteCoin(coin);
			}
		}
	}

	// Token: 0x0600554D RID: 21837 RVA: 0x001BE684 File Offset: 0x001BC884
	public NetCache.NetCacheCoins GetCoins()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheCoins();
		}
		Coins coinsPacket = this.GetCoinsPacket();
		if (coinsPacket == null)
		{
			return null;
		}
		NetCache.NetCacheCoins netCacheCoins = new NetCache.NetCacheCoins();
		netCacheCoins.FavoriteCoin = coinsPacket.FavoriteCoin;
		for (int i = 0; i < coinsPacket.Coins_.Count; i++)
		{
			int item = coinsPacket.Coins_[i];
			netCacheCoins.Coins.Add(item);
		}
		return netCacheCoins;
	}

	// Token: 0x0600554E RID: 21838 RVA: 0x001BE6ED File Offset: 0x001BC8ED
	public Coins GetCoinsPacket()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return null;
		}
		return this.m_connectApi.GetCoins();
	}

	// Token: 0x0600554F RID: 21839 RVA: 0x001BE703 File Offset: 0x001BC903
	public CoinUpdate GetCoinUpdate()
	{
		return this.m_connectApi.GetCoinUpdate();
	}

	// Token: 0x06005550 RID: 21840 RVA: 0x001BE710 File Offset: 0x001BC910
	public CardValues GetCardValues()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return null;
		}
		return this.m_connectApi.GetCardValues();
	}

	// Token: 0x06005551 RID: 21841 RVA: 0x001BE728 File Offset: 0x001BC928
	public InitialClientState GetInitialClientState()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			InitialClientState initialClientState = new InitialClientState();
			initialClientState.HasClientOptions = true;
			initialClientState.ClientOptions = new ClientOptions();
			initialClientState.HasCollection = true;
			initialClientState.Collection = new Collection();
			initialClientState.HasAchievements = true;
			initialClientState.Achievements = new Achieves();
			initialClientState.HasNotices = true;
			initialClientState.Notices = new PegasusUtil.ProfileNotices();
			initialClientState.HasGameCurrencyStates = true;
			initialClientState.GameCurrencyStates = new GameCurrencyStates();
			initialClientState.GameCurrencyStates.HasCurrencyVersion = true;
			initialClientState.GameCurrencyStates.CurrencyVersion = 0L;
			initialClientState.GameCurrencyStates.HasArcaneDustBalance = true;
			initialClientState.GameCurrencyStates.HasCappedGoldBalance = true;
			initialClientState.GameCurrencyStates.HasBonusGoldBalance = true;
			initialClientState.HasBoosters = true;
			initialClientState.Boosters = new Boosters();
			if (initialClientState.Decks == null)
			{
				initialClientState.Decks = new List<DeckInfo>();
			}
			return initialClientState;
		}
		return this.m_connectApi.GetInitialClientState();
	}

	// Token: 0x06005552 RID: 21842 RVA: 0x001BE810 File Offset: 0x001BCA10
	public void OpenBooster(int id)
	{
		global::Log.Net.Print("Network.OpenBooster", Array.Empty<object>());
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			NetCache.BoosterStack boosterStack = netObject.GetBoosterStack(id);
			int locallyPreConsumedCount = boosterStack.LocallyPreConsumedCount + 1;
			boosterStack.LocallyPreConsumedCount = locallyPreConsumedCount;
		}
		long fsgId = FiresideGatheringManager.Get().IsCheckedIn ? FiresideGatheringManager.Get().CurrentFsgId : 0L;
		this.m_connectApi.OpenBooster(id, fsgId);
	}

	// Token: 0x06005553 RID: 21843 RVA: 0x001BE880 File Offset: 0x001BCA80
	public void CreateDeck(DeckType deckType, string name, int heroDatabaseAssetID, TAG_PREMIUM heroPremium, PegasusShared.FormatType formatType, long sortOrder, DeckSourceType sourceType, out int? requestId, string pastedDeckHash = null, int brawlLibraryItemId = 0)
	{
		if (!Network.IsLoggedIn())
		{
			requestId = null;
			return;
		}
		requestId = new int?(this.GetNextCreateDeckRequestId());
		long? fsgId = FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null;
		global::Log.Net.Print(string.Format("Network.CreateDeck hero={0},premium={1}", heroDatabaseAssetID, heroPremium), Array.Empty<object>());
		this.m_connectApi.CreateDeck(deckType, name, heroDatabaseAssetID, heroPremium, formatType, sortOrder, sourceType, pastedDeckHash, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey, brawlLibraryItemId, requestId);
	}

	// Token: 0x06005554 RID: 21844 RVA: 0x001BE928 File Offset: 0x001BCB28
	private int GetNextCreateDeckRequestId()
	{
		int num = this.m_state.CurrentCreateDeckRequestId + 1;
		this.m_state.CurrentCreateDeckRequestId = num;
		return num;
	}

	// Token: 0x06005555 RID: 21845 RVA: 0x001BE94C File Offset: 0x001BCB4C
	public void RenameDeck(long deck, string name)
	{
		if (Network.IsLoggedIn())
		{
			global::Log.Net.Print(string.Format("Network.RenameDeck {0}", deck), Array.Empty<object>());
			CollectionManager.Get().AddPendingDeckRename(deck, name);
			this.m_connectApi.RenameDeck(deck, name);
			return;
		}
		OfflineDataCache.RenameDeck(deck, name);
	}

	// Token: 0x06005556 RID: 21846 RVA: 0x001BE9A0 File Offset: 0x001BCBA0
	public void SendDeckData(long deck, List<Network.CardUserData> cards, int newHeroAssetID, TAG_PREMIUM newHeroCardPremium, int heroOverrideAssetID, TAG_PREMIUM heroOverridePremium, int newCardBackID, PegasusShared.FormatType formatType, long sortOrder, string pastedDeckHash = null)
	{
		DeckSetData deckSetData = new DeckSetData
		{
			Deck = deck,
			FormatType = formatType,
			TaggedStandard = (formatType == PegasusShared.FormatType.FT_STANDARD),
			SortOrder = sortOrder
		};
		for (int i = 0; i < cards.Count; i++)
		{
			Network.CardUserData cardUserData = cards[i];
			DeckCardData deckCardData = new DeckCardData();
			PegasusShared.CardDef cardDef = new PegasusShared.CardDef();
			cardDef.Asset = cardUserData.DbId;
			if (cardUserData.Premium != TAG_PREMIUM.NORMAL)
			{
				cardDef.Premium = (int)cardUserData.Premium;
			}
			deckCardData.Def = cardDef;
			deckCardData.Qty = cardUserData.Count;
			deckSetData.Cards.Add(deckCardData);
		}
		if (-1 != newHeroAssetID)
		{
			deckSetData.Hero = new PegasusShared.CardDef
			{
				Asset = newHeroAssetID,
				Premium = (int)newHeroCardPremium
			};
		}
		if (-1 != heroOverrideAssetID)
		{
			deckSetData.UiHeroOverride = new PegasusShared.CardDef
			{
				Asset = heroOverrideAssetID,
				Premium = (int)heroOverridePremium
			};
		}
		if (-1 != newCardBackID)
		{
			deckSetData.CardBack = newCardBackID;
		}
		if (!string.IsNullOrEmpty(pastedDeckHash))
		{
			deckSetData.PastedDeckHash = pastedDeckHash;
		}
		long? num = FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null;
		if (num != null)
		{
			deckSetData.FsgId = num.Value;
		}
		deckSetData.FsgSharedSecretKey = FiresideGatheringManager.Get().CurrentFsgSharedSecretKey;
		if (Network.IsLoggedIn())
		{
			this.m_connectApi.SendDeckData(deckSetData);
			OfflineDataCache.ApplyDeckSetDataToOriginalDeck(deckSetData);
			CollectionManager.Get().AddPendingDeckEdit(deck);
		}
		OfflineDataCache.ApplyDeckSetDataLocally(deckSetData);
	}

	// Token: 0x06005557 RID: 21847 RVA: 0x001BEB24 File Offset: 0x001BCD24
	public void DeleteDeck(long deck, DeckType deckType)
	{
		OfflineDataCache.DeleteDeck(deck);
		if (!Network.IsLoggedIn())
		{
			return;
		}
		global::Log.Net.Print(string.Format("Network.DeleteDeck {0}", deck), Array.Empty<object>());
		if (deck <= 0L)
		{
			global::Log.Offline.PrintError("Network.DeleteDeck Error: Attempting to delete fake deck ID={0} on server.", new object[]
			{
				deck
			});
			return;
		}
		this.m_connectApi.DeleteDeck(deck, deckType);
	}

	// Token: 0x06005558 RID: 21848 RVA: 0x001BEB90 File Offset: 0x001BCD90
	public void RequestDeckContents(params long[] deckIds)
	{
		if (!Network.IsLoggedIn())
		{
			return;
		}
		global::Logger net = global::Log.Net;
		string format = "Network.GetDeckContents {0}";
		object[] array = new object[1];
		array[0] = string.Join(", ", (from id in deckIds
		select id.ToString()).ToArray<string>());
		net.Print(format, array);
		this.m_connectApi.RequestDeckContents(deckIds);
	}

	// Token: 0x06005559 RID: 21849 RVA: 0x001BEC00 File Offset: 0x001BCE00
	public void SetDeckTemplateSource(long deck, int templateID)
	{
		if (!Network.IsLoggedIn() || deck < 0L)
		{
			return;
		}
		global::Log.Net.Print(string.Format("Network.SendDeckTemplateSource {0}, {1}", deck, templateID), Array.Empty<object>());
		this.m_connectApi.SendDeckTemplateSource(deck, templateID);
	}

	// Token: 0x0600555A RID: 21850 RVA: 0x001BEC4C File Offset: 0x001BCE4C
	public GetDeckContentsResponse GetDeckContentsResponse()
	{
		GetDeckContentsResponse getDeckContentsResponse;
		if (Network.IsLoggedIn())
		{
			getDeckContentsResponse = this.m_connectApi.GetDeckContentsResponse();
		}
		else
		{
			getDeckContentsResponse = new GetDeckContentsResponse
			{
				Decks = new List<PegasusUtil.DeckContents>()
			};
			getDeckContentsResponse.Decks = OfflineDataCache.GetLocalDeckContentsFromCache();
		}
		return getDeckContentsResponse;
	}

	// Token: 0x0600555B RID: 21851 RVA: 0x001BEC8C File Offset: 0x001BCE8C
	public FreeDeckChoiceResponse GetFreeDeckChoiceResponse()
	{
		FreeDeckChoiceResponse result;
		if (Network.IsLoggedIn())
		{
			result = this.m_connectApi.GetFreeDeckChoiceResponse();
		}
		else
		{
			result = new FreeDeckChoiceResponse
			{
				Success = false
			};
		}
		return result;
	}

	// Token: 0x0600555C RID: 21852 RVA: 0x001BECBC File Offset: 0x001BCEBC
	public static SmartDeckRequest GenerateSmartDeckRequestMessage(CollectionDeck deck)
	{
		List<SmartDeckCardData> list = new List<SmartDeckCardData>();
		Dictionary<long, SmartDeckCardData> dictionary = new Dictionary<long, SmartDeckCardData>();
		foreach (CollectionDeckSlot collectionDeckSlot in deck.GetSlots())
		{
			if (collectionDeckSlot.Owned)
			{
				int num = GameUtils.TranslateCardIdToDbId(collectionDeckSlot.CardID, false);
				if (!dictionary.ContainsKey((long)num))
				{
					dictionary.Add((long)num, new SmartDeckCardData
					{
						Asset = num
					});
				}
				dictionary[(long)num].QtyGolden += collectionDeckSlot.GetCount(TAG_PREMIUM.GOLDEN);
				dictionary[(long)num].QtyNormal += collectionDeckSlot.GetCount(TAG_PREMIUM.NORMAL);
			}
		}
		foreach (long key in dictionary.Keys)
		{
			list.Add(dictionary[key]);
		}
		HSCachedDeckCompletionRequest requestMessage = new HSCachedDeckCompletionRequest
		{
			HeroClass = (int)deck.GetClass(),
			InsertedCard = list,
			DeckId = deck.ID,
			FormatType = deck.FormatType
		};
		return new SmartDeckRequest
		{
			RequestMessage = requestMessage
		};
	}

	// Token: 0x0600555D RID: 21853 RVA: 0x001BEE14 File Offset: 0x001BD014
	public void RequestSmartDeckCompletion(CollectionDeck deck)
	{
		SmartDeckRequest packet = Network.GenerateSmartDeckRequestMessage(deck);
		this.m_connectApi.SendSmartDeckRequest(packet);
	}

	// Token: 0x0600555E RID: 21854 RVA: 0x001BEE34 File Offset: 0x001BD034
	public void RequestOfflineDeckContents()
	{
		this.m_connectApi.SendOfflineDeckContentsRequest();
	}

	// Token: 0x0600555F RID: 21855 RVA: 0x001BEE41 File Offset: 0x001BD041
	public void RequestBaconRatingInfo()
	{
		this.m_connectApi.RequestBaconRatingInfo();
	}

	// Token: 0x06005560 RID: 21856 RVA: 0x001BEE4E File Offset: 0x001BD04E
	public ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> BattlegroundsRatingInfoResponse()
	{
		return this.m_connectApi.BattlegroundsRatingInfoResponse();
	}

	// Token: 0x06005561 RID: 21857 RVA: 0x001BEE5B File Offset: 0x001BD05B
	public void RequestBattlegroundsPremiumStatus()
	{
		this.m_connectApi.RequestBattlegroundsPremiumStatus();
	}

	// Token: 0x06005562 RID: 21858 RVA: 0x001BEE68 File Offset: 0x001BD068
	public ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> GetBattlegroundsPremiumStatus()
	{
		return this.m_connectApi.GetBattlegroundsPremiumStatus();
	}

	// Token: 0x06005563 RID: 21859 RVA: 0x001BEE75 File Offset: 0x001BD075
	public void SendPVPDRSessionStartRequest(bool paidEntry)
	{
		this.m_connectApi.SendPVPDRSessionStartRequest(paidEntry);
	}

	// Token: 0x06005564 RID: 21860 RVA: 0x001BEE83 File Offset: 0x001BD083
	public PVPDRSessionStartResponse GetPVPDRSessionStartResponse()
	{
		return this.m_connectApi.GetPVPDRSessionStartResponse();
	}

	// Token: 0x06005565 RID: 21861 RVA: 0x001BEE90 File Offset: 0x001BD090
	public void SendPVPDRSessionEndRequest()
	{
		this.m_connectApi.SendPVPDRSessionEndRequest();
	}

	// Token: 0x06005566 RID: 21862 RVA: 0x001BEE9D File Offset: 0x001BD09D
	public PVPDRSessionEndResponse GetPVPDRSessionEndResponse()
	{
		return this.m_connectApi.GetPVPDRSessionEndResponse();
	}

	// Token: 0x06005567 RID: 21863 RVA: 0x001BEEAA File Offset: 0x001BD0AA
	public void SendPVPDRSessionInfoRequest()
	{
		this.m_connectApi.SendPVPDRSessionInfoRequest();
	}

	// Token: 0x06005568 RID: 21864 RVA: 0x001BEEB7 File Offset: 0x001BD0B7
	public PVPDRSessionInfoResponse GetPVPDRSessionInfoResponse()
	{
		return this.m_connectApi.GetPVPDRSessionInfoResponse();
	}

	// Token: 0x06005569 RID: 21865 RVA: 0x001BEEC4 File Offset: 0x001BD0C4
	public void SendPVPDRRetireRequest()
	{
		this.m_connectApi.SendPVPDRRetireRequest();
	}

	// Token: 0x0600556A RID: 21866 RVA: 0x001BEED1 File Offset: 0x001BD0D1
	public PVPDRRetireResponse GetPVPDRRetireResponse()
	{
		return this.m_connectApi.GetPVPDRRetireResponse();
	}

	// Token: 0x0600556B RID: 21867 RVA: 0x001BEEDE File Offset: 0x001BD0DE
	public void RequestPVPDRStatsInfo()
	{
		this.m_connectApi.RequestPVPDRStatsInfo();
	}

	// Token: 0x0600556C RID: 21868 RVA: 0x001BEEEB File Offset: 0x001BD0EB
	public ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> PVPDRStatsInfoResponse()
	{
		return this.m_connectApi.PVPDRStatsInfoResponse();
	}

	// Token: 0x0600556D RID: 21869 RVA: 0x001BEEF8 File Offset: 0x001BD0F8
	public List<NetCache.BoosterCard> OpenedBooster()
	{
		BoosterContent openedBooster = this.m_connectApi.GetOpenedBooster();
		if (openedBooster == null)
		{
			return null;
		}
		List<NetCache.BoosterCard> list = new List<NetCache.BoosterCard>();
		for (int i = 0; i < openedBooster.List.Count; i++)
		{
			BoosterCard boosterCard = openedBooster.List[i];
			list.Add(new NetCache.BoosterCard
			{
				Def = 
				{
					Name = GameUtils.TranslateDbIdToCardId(boosterCard.CardDef.Asset, false),
					Premium = (TAG_PREMIUM)boosterCard.CardDef.Premium
				},
				Date = global::TimeUtils.PegDateToFileTimeUtc(boosterCard.InsertDate)
			});
		}
		if (openedBooster.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(openedBooster.CollectionVersion);
		}
		return list;
	}

	// Token: 0x0600556E RID: 21870 RVA: 0x001BEFAE File Offset: 0x001BD1AE
	public Network.DBAction GetDeckResponse()
	{
		return this.GetDbAction();
	}

	// Token: 0x0600556F RID: 21871 RVA: 0x001BEFB8 File Offset: 0x001BD1B8
	public Network.DBAction GetDbAction()
	{
		PegasusUtil.DBAction dbAction = this.m_connectApi.GetDbAction();
		if (dbAction == null)
		{
			return null;
		}
		return new Network.DBAction
		{
			Action = (Network.DBAction.ActionType)dbAction.Action,
			Result = (Network.DBAction.ResultType)dbAction.Result,
			MetaData = dbAction.MetaData
		};
	}

	// Token: 0x06005570 RID: 21872 RVA: 0x001BF000 File Offset: 0x001BD200
	public void ReconcileDeckContentsForChangedOfflineDecks(List<DeckInfo> remoteDecks, List<PegasusUtil.DeckContents> remoteContents, List<long> validDeckIds)
	{
		List<long> list = new List<long>();
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		using (List<DeckInfo>.Enumerator enumerator = remoteDecks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DeckInfo remoteDeckInfo = enumerator.Current;
				if (!validDeckIds.Exists((long item) => item == remoteDeckInfo.Id))
				{
					DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckInfo.Id, offlineData.OriginalDeckList);
					DeckInfo deckInfoFromDeckList2 = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckInfo.Id, offlineData.LocalDeckList);
					if (deckInfoFromDeckList2 == null && deckInfoFromDeckList != null)
					{
						NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
						if (netObject != null && netObject.AllowOfflineClientDeckDeletion)
						{
							Network.Get().DeleteDeck(remoteDeckInfo.Id, remoteDeckInfo.DeckType);
						}
					}
					else if (deckInfoFromDeckList2 == null && deckInfoFromDeckList == null)
					{
						list.Add(remoteDeckInfo.Id);
					}
					else if (deckInfoFromDeckList2 != null && deckInfoFromDeckList != null && remoteDeckInfo.LastModified != deckInfoFromDeckList2.LastModified)
					{
						if (remoteDeckInfo.LastModified != deckInfoFromDeckList.LastModified)
						{
							list.Add(remoteDeckInfo.Id);
						}
						else
						{
							list.Add(remoteDeckInfo.Id);
						}
					}
				}
			}
		}
		using (List<DeckInfo>.Enumerator enumerator = offlineData.LocalDeckList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DeckInfo localDeck = enumerator.Current;
				if (!remoteDecks.Any((DeckInfo d) => d.Id == localDeck.Id) && offlineData.OriginalDeckList.Any((DeckInfo d) => d.Id == localDeck.Id))
				{
					CollectionManager.Get().OnDeckDeletedWhileOffline(localDeck.Id);
				}
			}
		}
		if (list.Count > 0)
		{
			List<long> list2 = new List<long>();
			using (List<long>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					long deck = enumerator2.Current;
					this.m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Add(deck);
					DeckInfo deckInfo = remoteDecks.Find((DeckInfo item) => item.Id == deck);
					bool flag = deckInfo != null && deckInfo.DeckType == DeckType.PRECON_DECK;
					bool flag2 = remoteContents != null && remoteContents.Exists((PegasusUtil.DeckContents item) => item.DeckId == deck);
					if (!flag && !flag2)
					{
						list2.Add(deck);
					}
				}
			}
			if (list2.Count > 0)
			{
				this.RequestDeckContents(list2.ToArray());
			}
		}
		this.RegisterNetHandler(DeckCreated.PacketID.ID, new Network.NetHandler(this.OnDeckCreatedResponse_SendOfflineDeckSetData), null);
		this.CreateDeckFromOfflineDeckCache(offlineData);
		if (remoteContents != null)
		{
			this.UpdateDecksFromContent(remoteContents);
		}
	}

	// Token: 0x06005571 RID: 21873 RVA: 0x001BF324 File Offset: 0x001BD524
	public NetCache.NetCacheDecks GetDeckHeaders()
	{
		NetCache.NetCacheDecks result = new NetCache.NetCacheDecks();
		if (!Network.ShouldBeConnectedToAurora())
		{
			return result;
		}
		DeckList deckHeaders = this.m_connectApi.GetDeckHeaders();
		if (deckHeaders == null)
		{
			return null;
		}
		return Network.GetDeckHeaders(deckHeaders.Decks);
	}

	// Token: 0x06005572 RID: 21874 RVA: 0x001BF35C File Offset: 0x001BD55C
	public static NetCache.NetCacheDecks GetDeckHeaders(List<DeckInfo> deckHeaders)
	{
		NetCache.NetCacheDecks netCacheDecks = new NetCache.NetCacheDecks();
		if (deckHeaders == null)
		{
			return netCacheDecks;
		}
		for (int i = 0; i < deckHeaders.Count; i++)
		{
			netCacheDecks.Decks.Add(Network.GetDeckHeaderFromDeckInfo(deckHeaders[i]));
		}
		return netCacheDecks;
	}

	// Token: 0x06005573 RID: 21875 RVA: 0x001BF3A0 File Offset: 0x001BD5A0
	private void OnDeckContentsResponse()
	{
		GetDeckContentsResponse deckContentsResponse = this.GetDeckContentsResponse();
		this.UpdateDecksFromContent(deckContentsResponse.Decks);
	}

	// Token: 0x06005574 RID: 21876 RVA: 0x001BF3C0 File Offset: 0x001BD5C0
	private void UpdateDecksFromContent(List<PegasusUtil.DeckContents> decksContents)
	{
		List<DeckSetData> list = new List<DeckSetData>();
		List<RenameDeck> list2 = new List<RenameDeck>();
		List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
		foreach (PegasusUtil.DeckContents deckContents in decksContents)
		{
			if (this.m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Contains(deckContents.DeckId))
			{
				this.m_state.DeckIdsWaitingToDiffAgainstOfflineCache.Remove(deckContents.DeckId);
				this.DiffRemoteDeckContentsAgainstOfflineDataCache(deckContents, deckListFromNetCache, ref list, ref list2);
			}
			else
			{
				OfflineDataCache.CacheLocalAndOriginalDeckContents(deckContents, deckContents);
			}
		}
		List<long> list3 = new List<long>();
		foreach (DeckSetData deckSetData in list)
		{
			this.m_connectApi.SendDeckData(deckSetData);
			list3.Add(deckSetData.Deck);
		}
		CollectionManager.Get().RegisterDecksToRequestContentsAfterDeckSetDataResponse(list3);
		foreach (RenameDeck renameDeck in list2)
		{
			this.m_connectApi.RenameDeck(renameDeck.Deck, renameDeck.Name);
		}
		OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
	}

	// Token: 0x06005575 RID: 21877 RVA: 0x001BF524 File Offset: 0x001BD724
	private void DiffRemoteDeckContentsAgainstOfflineDataCache(PegasusUtil.DeckContents remoteDeckContents, List<DeckInfo> currentNetCacheDeckList, ref List<DeckSetData> deckSetDataToSend, ref List<RenameDeck> deckRenameToSend)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(remoteDeckContents.DeckId, offlineData.LocalDeckList);
		PegasusUtil.DeckContents deckContentsFromDeckContentsList = OfflineDataCache.GetDeckContentsFromDeckContentsList(remoteDeckContents.DeckId, offlineData.LocalDeckContents);
		DeckInfo deckInfo = null;
		foreach (DeckInfo deckInfo2 in currentNetCacheDeckList)
		{
			if (deckInfo2.Id == remoteDeckContents.DeckId)
			{
				deckInfo = deckInfo2;
				break;
			}
		}
		if (deckInfo == null)
		{
			return;
		}
		if (deckInfoFromDeckList != null && deckInfo.LastModified < deckInfoFromDeckList.LastModified)
		{
			DeckSetData item;
			if (OfflineDataCache.GenerateDeckSetDataFromDiff(remoteDeckContents.DeckId, deckInfoFromDeckList, deckInfo, deckContentsFromDeckContentsList, remoteDeckContents, out item))
			{
				deckSetDataToSend.Add(item);
			}
			RenameDeck renameDeck = OfflineDataCache.GenerateRenameDeckFromDiff(remoteDeckContents.DeckId, deckInfoFromDeckList, deckInfo);
			if (renameDeck != null && renameDeck.Name != null)
			{
				deckRenameToSend.Add(renameDeck);
				return;
			}
		}
		else
		{
			OfflineDataCache.CacheLocalAndOriginalDeckContents(remoteDeckContents, remoteDeckContents);
		}
	}

	// Token: 0x06005576 RID: 21878 RVA: 0x001BF60C File Offset: 0x001BD80C
	private void CreateDeckFromOfflineDeckCache(OfflineDataCache.OfflineData data)
	{
		int num = 0;
		List<long> fakeDeckIds = OfflineDataCache.GetFakeDeckIds(data);
		if (fakeDeckIds.Contains(this.FakeIdWaitingForResponse))
		{
			num = fakeDeckIds.IndexOf(Network.Get().FakeIdWaitingForResponse);
			num++;
		}
		DeckInfo deckInfo = null;
		int num2 = num;
		while (num2 < fakeDeckIds.Count && deckInfo == null)
		{
			this.FakeIdWaitingForResponse = fakeDeckIds[num2];
			deckInfo = OfflineDataCache.GetDeckInfoFromDeckList(this.FakeIdWaitingForResponse, data.LocalDeckList);
			num2++;
		}
		if (deckInfo == null)
		{
			this.RemoveNetHandler(DeckCreated.PacketID.ID, new Network.NetHandler(this.OnDeckCreatedResponse_SendOfflineDeckSetData));
			this.OnFinishedCreatingDecksFromOfflineDataCache();
			return;
		}
		int? num3;
		this.CreateDeck(deckInfo.DeckType, deckInfo.Name, deckInfo.Hero, (TAG_PREMIUM)deckInfo.HeroPremium, deckInfo.FormatType, deckInfo.SortOrder, deckInfo.SourceType, out num3, deckInfo.PastedDeckHash, 0);
		if (num3 != null)
		{
			this.m_state.InTransitOfflineCreateDeckRequestIds.Add(num3.Value);
		}
	}

	// Token: 0x06005577 RID: 21879 RVA: 0x001BF700 File Offset: 0x001BD900
	private void OnFinishedCreatingDecksFromOfflineDataCache()
	{
		OfflineDataCache.ClearFakeDeckIds();
		OfflineDataCache.RemoveAllOldDecksContents();
		this.FakeIdWaitingForResponse = 0L;
	}

	// Token: 0x06005578 RID: 21880 RVA: 0x001BF714 File Offset: 0x001BD914
	private void OnDeckCreatedResponse_SendOfflineDeckSetData()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		int? num;
		NetCache.DeckHeader createdDeck = this.GetCreatedDeck(out num);
		if (createdDeck != null)
		{
			if (num == null || !this.m_state.InTransitOfflineCreateDeckRequestIds.Contains(num.Value))
			{
				return;
			}
			this.m_state.InTransitOfflineCreateDeckRequestIds.Remove(num.Value);
			long fakeIdWaitingForResponse = Network.Get().FakeIdWaitingForResponse;
			DeckSetData deckSetData;
			if (OfflineDataCache.GenerateDeckSetDataFromDiff(fakeIdWaitingForResponse, offlineData.LocalDeckList, offlineData.OriginalDeckList, offlineData.LocalDeckContents, offlineData.OriginalDeckContents, out deckSetData))
			{
				deckSetData.Deck = createdDeck.ID;
				CollectionManager.Get().RegisterDecksToRequestContentsAfterDeckSetDataResponse(new List<long>
				{
					createdDeck.ID
				});
				this.m_connectApi.SendDeckData(deckSetData);
			}
			if (!OfflineDataCache.UpdateDeckWithNewId(fakeIdWaitingForResponse, createdDeck.ID))
			{
				global::Log.Offline.PrintDebug("OnDeckCreatedResponse_SendOfflineDeckSetData() - Deleting deck id={0} because it's fake id={1}  was not found in the offline cache.", new object[]
				{
					createdDeck.ID,
					fakeIdWaitingForResponse
				});
				this.DeleteDeck(createdDeck.ID, createdDeck.Type);
				return;
			}
			CollectionManager.Get().UpdateDeckWithNewId(fakeIdWaitingForResponse, createdDeck.ID);
			this.CreateDeckFromOfflineDeckCache(offlineData);
		}
	}

	// Token: 0x06005579 RID: 21881 RVA: 0x001BF839 File Offset: 0x001BDA39
	public static bool DeckNeedsName(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 512UL) > 0UL;
	}

	// Token: 0x0600557A RID: 21882 RVA: 0x001BF847 File Offset: 0x001BDA47
	public static bool AreDeckFlagsWild(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 128UL) == 0UL;
	}

	// Token: 0x0600557B RID: 21883 RVA: 0x001BF855 File Offset: 0x001BDA55
	public static bool AreDeckFlagsLocked(ulong deckValidityFlags)
	{
		return (deckValidityFlags & 1024UL) > 0UL;
	}

	// Token: 0x0600557C RID: 21884 RVA: 0x001BF864 File Offset: 0x001BDA64
	public NetCache.DeckHeader GetCreatedDeck(out int? requestId)
	{
		DeckCreated deckCreated = this.m_connectApi.DeckCreated();
		if (deckCreated == null)
		{
			requestId = null;
			return null;
		}
		NetCache.DeckHeader deckHeaderFromDeckInfo = Network.GetDeckHeaderFromDeckInfo(deckCreated.Info);
		requestId = new int?(deckCreated.RequestId);
		return deckHeaderFromDeckInfo;
	}

	// Token: 0x0600557D RID: 21885 RVA: 0x001BF8A8 File Offset: 0x001BDAA8
	public static NetCache.DeckHeader GetDeckHeaderFromDeckInfo(DeckInfo deck)
	{
		NetCache.DeckHeader deckHeader = new NetCache.DeckHeader
		{
			ID = deck.Id,
			Name = deck.Name,
			Hero = GameUtils.TranslateDbIdToCardId(deck.Hero, false),
			HeroPremium = (TAG_PREMIUM)deck.HeroPremium,
			HeroPower = GameUtils.GetHeroPowerCardIdFromHero(deck.Hero),
			Type = deck.DeckType,
			CardBack = deck.CardBack,
			CardBackOverridden = deck.CardBackOverride,
			HeroOverridden = deck.HeroOverride,
			SeasonId = deck.SeasonId,
			BrawlLibraryItemId = deck.BrawlLibraryItemId,
			NeedsName = Network.DeckNeedsName(deck.Validity),
			SortOrder = (deck.HasSortOrder ? deck.SortOrder : deck.Id),
			FormatType = deck.FormatType,
			Locked = Network.AreDeckFlagsLocked(deck.Validity),
			SourceType = (deck.HasSourceType ? deck.SourceType : DeckSourceType.DECK_SOURCE_TYPE_UNKNOWN),
			UIHeroOverride = ((deck.HasUiHeroOverride && deck.UiHeroOverride != 0) ? GameUtils.TranslateDbIdToCardId(deck.UiHeroOverride, false) : string.Empty),
			UIHeroOverridePremium = (TAG_PREMIUM)(deck.HasUiHeroOverridePremium ? deck.UiHeroOverridePremium : 0)
		};
		if (deck.HasCreateDate)
		{
			deckHeader.CreateDate = new DateTime?(global::TimeUtils.UnixTimeStampToDateTimeUtc(deck.CreateDate));
		}
		else
		{
			deckHeader.CreateDate = null;
		}
		if (deck.HasLastModified)
		{
			deckHeader.LastModified = new DateTime?(global::TimeUtils.UnixTimeStampToDateTimeUtc(deck.LastModified));
		}
		else
		{
			deckHeader.LastModified = null;
		}
		return deckHeader;
	}

	// Token: 0x0600557E RID: 21886 RVA: 0x001BFA4C File Offset: 0x001BDC4C
	public static DeckInfo GetDeckInfoFromDeckHeader(NetCache.DeckHeader deckHeader)
	{
		if (deckHeader == null)
		{
			return null;
		}
		DeckInfo deckInfo = new DeckInfo
		{
			Id = deckHeader.ID,
			Name = deckHeader.Name,
			Hero = GameUtils.TranslateCardIdToDbId(deckHeader.Hero, false),
			HeroPremium = (int)deckHeader.HeroPremium,
			DeckType = deckHeader.Type,
			CardBack = deckHeader.CardBack,
			CardBackOverride = deckHeader.CardBackOverridden,
			HeroOverride = deckHeader.HeroOverridden,
			BrawlLibraryItemId = deckHeader.BrawlLibraryItemId,
			SortOrder = deckHeader.SortOrder,
			SourceType = deckHeader.SourceType
		};
		if (deckHeader.SeasonId != 0)
		{
			deckInfo.SeasonId = deckHeader.SeasonId;
		}
		if (!string.IsNullOrEmpty(deckHeader.UIHeroOverride))
		{
			deckInfo.UiHeroOverride = GameUtils.TranslateCardIdToDbId(deckHeader.UIHeroOverride, false);
			deckInfo.UiHeroOverridePremium = (int)deckHeader.UIHeroOverridePremium;
		}
		if (deckHeader.CreateDate != null)
		{
			deckInfo.CreateDate = (long)global::TimeUtils.DateTimeToUnixTimeStamp(deckHeader.CreateDate.Value);
		}
		if (deckHeader.LastModified != null)
		{
			deckInfo.LastModified = (long)global::TimeUtils.DateTimeToUnixTimeStamp(deckHeader.LastModified.Value);
		}
		return deckInfo;
	}

	// Token: 0x0600557F RID: 21887 RVA: 0x001BFB80 File Offset: 0x001BDD80
	public int GetDeckLimit()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return 0;
		}
		ProfileDeckLimit deckLimit = this.m_connectApi.GetDeckLimit();
		if (deckLimit == null)
		{
			return 0;
		}
		return deckLimit.DeckLimit;
	}

	// Token: 0x06005580 RID: 21888 RVA: 0x001BFBB0 File Offset: 0x001BDDB0
	public long GetDeletedDeckID()
	{
		DeckDeleted deckDeleted = this.m_connectApi.DeckDeleted();
		if (deckDeleted == null)
		{
			return 0L;
		}
		return deckDeleted.Deck;
	}

	// Token: 0x06005581 RID: 21889 RVA: 0x001BFBD8 File Offset: 0x001BDDD8
	public Network.DeckName GetRenamedDeck()
	{
		DeckRenamed deckRenamed = this.m_connectApi.DeckRenamed();
		if (deckRenamed == null)
		{
			return null;
		}
		return new Network.DeckName
		{
			Deck = deckRenamed.Deck,
			Name = deckRenamed.Name
		};
	}

	// Token: 0x06005582 RID: 21890 RVA: 0x001BFC14 File Offset: 0x001BDE14
	public Network.GenericResponse GetGenericResponse()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new Network.GenericResponse
			{
				RequestId = 0,
				RequestSubId = 1,
				ResultCode = Network.GenericResponse.Result.RESULT_OK
			};
		}
		PegasusUtil.GenericResponse genericResponse = this.m_connectApi.GetGenericResponse();
		if (genericResponse == null)
		{
			return null;
		}
		return new Network.GenericResponse
		{
			ResultCode = (Network.GenericResponse.Result)genericResponse.ResultCode,
			RequestId = genericResponse.RequestId,
			RequestSubId = (genericResponse.HasRequestSubId ? genericResponse.RequestSubId : 0),
			GenericData = genericResponse.GenericData
		};
	}

	// Token: 0x06005583 RID: 21891 RVA: 0x001BFC94 File Offset: 0x001BDE94
	public void RequestNetCacheObject(GetAccountInfo.Request request)
	{
		this.m_connectApi.RequestAccountInfoNetCacheObject(request);
	}

	// Token: 0x06005584 RID: 21892 RVA: 0x001BFCA2 File Offset: 0x001BDEA2
	public void RequestNetCacheObjectList(List<GetAccountInfo.Request> requestList, List<GenericRequest> genericRequests)
	{
		this.m_connectApi.RequestNetCacheObjectList(requestList, genericRequests);
	}

	// Token: 0x06005585 RID: 21893 RVA: 0x001BFCB4 File Offset: 0x001BDEB4
	public NetCache.NetCacheProfileProgress GetProfileProgress()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheProfileProgress
			{
				CampaignProgress = global::Options.Get().GetEnum<TutorialProgress>(global::Option.LOCAL_TUTORIAL_PROGRESS)
			};
		}
		ProfileProgress profileProgress = this.m_connectApi.GetProfileProgress();
		if (profileProgress == null)
		{
			return null;
		}
		return new NetCache.NetCacheProfileProgress
		{
			CampaignProgress = (TutorialProgress)profileProgress.Progress,
			BestForgeWins = profileProgress.BestForge,
			LastForgeDate = (profileProgress.HasLastForge ? global::TimeUtils.PegDateToFileTimeUtc(profileProgress.LastForge) : 0L)
		};
	}

	// Token: 0x06005586 RID: 21894 RVA: 0x001BFD2C File Offset: 0x001BDF2C
	public void SetProgress(long value)
	{
		this.m_connectApi.SetProgress(value);
	}

	// Token: 0x06005587 RID: 21895 RVA: 0x001BFD3A File Offset: 0x001BDF3A
	public SetProgressResponse GetSetProgressResponse()
	{
		return this.m_connectApi.GetSetProgressResponse();
	}

	// Token: 0x06005588 RID: 21896 RVA: 0x001BFD48 File Offset: 0x001BDF48
	public void HandleProfileNotices(List<ProfileNotice> notices, ref List<NetCache.ProfileNotice> result)
	{
		int i = 0;
		while (i < notices.Count)
		{
			ProfileNotice profileNotice = notices[i];
			NetCache.ProfileNotice profileNotice2 = null;
			if (profileNotice.HasMedal)
			{
				global::Map<ProfileNoticeMedal.MedalType, PegasusShared.FormatType> map = new global::Map<ProfileNoticeMedal.MedalType, PegasusShared.FormatType>
				{
					{
						ProfileNoticeMedal.MedalType.UNKNOWN_MEDAL,
						PegasusShared.FormatType.FT_UNKNOWN
					},
					{
						ProfileNoticeMedal.MedalType.WILD_MEDAL,
						PegasusShared.FormatType.FT_WILD
					},
					{
						ProfileNoticeMedal.MedalType.STANDARD_MEDAL,
						PegasusShared.FormatType.FT_STANDARD
					},
					{
						ProfileNoticeMedal.MedalType.CLASSIC_MEDAL,
						PegasusShared.FormatType.FT_CLASSIC
					}
				};
				PegasusShared.FormatType formatType = PegasusShared.FormatType.FT_UNKNOWN;
				if (profileNotice.Medal.HasMedalType_)
				{
					map.TryGetValue(profileNotice.Medal.MedalType_, out formatType);
				}
				NetCache.ProfileNoticeMedal profileNoticeMedal = new NetCache.ProfileNoticeMedal
				{
					LeagueId = profileNotice.Medal.LeagueId,
					StarLevel = profileNotice.Medal.StarLevel,
					LegendRank = (profileNotice.Medal.HasLegendRank ? profileNotice.Medal.LegendRank : 0),
					BestStarLevel = (profileNotice.Medal.HasBestStarLevel ? profileNotice.Medal.BestStarLevel : 0),
					FormatType = formatType,
					WasLimitedByBestEverStarLevel = (profileNotice.Medal.HasWasLimitedByBestEverStarLevel && profileNotice.Medal.WasLimitedByBestEverStarLevel)
				};
				if (profileNotice.Medal.HasChest)
				{
					profileNoticeMedal.Chest = Network.ConvertRewardChest(profileNotice.Medal.Chest);
				}
				profileNotice2 = profileNoticeMedal;
				goto IL_89E;
			}
			if (profileNotice.HasRewardBooster)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardBooster
				{
					Id = profileNotice.RewardBooster.BoosterType,
					Count = profileNotice.RewardBooster.BoosterCount
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardCard)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCard
				{
					CardID = GameUtils.TranslateDbIdToCardId(profileNotice.RewardCard.Card.Asset, false),
					Premium = (TAG_PREMIUM)(profileNotice.RewardCard.Card.HasPremium ? profileNotice.RewardCard.Card.Premium : 0),
					Quantity = (profileNotice.RewardCard.HasQuantity ? profileNotice.RewardCard.Quantity : 1)
				};
				goto IL_89E;
			}
			if (profileNotice.HasPreconDeck)
			{
				profileNotice2 = new NetCache.ProfileNoticePreconDeck
				{
					DeckID = profileNotice.PreconDeck.Deck,
					HeroAsset = profileNotice.PreconDeck.Hero
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardDust)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardDust
				{
					Amount = profileNotice.RewardDust.Amount
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardMount)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardMount
				{
					MountID = profileNotice.RewardMount.MountId
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardForge)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardForge
				{
					Quantity = profileNotice.RewardForge.Quantity
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardCurrency)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCurrency
				{
					Amount = profileNotice.RewardCurrency.Amount,
					CurrencyType = (profileNotice.HasRewardCurrency ? profileNotice.RewardCurrency.CurrencyType : PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD)
				};
				goto IL_89E;
			}
			if (profileNotice.HasPurchase)
			{
				profileNotice2 = new NetCache.ProfileNoticePurchase
				{
					PMTProductID = (profileNotice.Purchase.HasPmtProductId ? new long?(profileNotice.Purchase.PmtProductId) : null),
					Data = (profileNotice.Purchase.HasData ? profileNotice.Purchase.Data : 0L),
					CurrencyCode = profileNotice.Purchase.CurrencyCode
				};
				goto IL_89E;
			}
			if (profileNotice.HasRewardCardBack)
			{
				profileNotice2 = new NetCache.ProfileNoticeRewardCardBack
				{
					CardBackID = profileNotice.RewardCardBack.CardBack
				};
				goto IL_89E;
			}
			if (profileNotice.HasBonusStars)
			{
				profileNotice2 = new NetCache.ProfileNoticeBonusStars
				{
					StarLevel = profileNotice.BonusStars.StarLevel,
					Stars = profileNotice.BonusStars.Stars
				};
				goto IL_89E;
			}
			if (profileNotice.HasDcGameResult)
			{
				if (!profileNotice.DcGameResult.HasGameType)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
				}
				else if (!profileNotice.DcGameResult.HasMissionId)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
				}
				else
				{
					if (profileNotice.DcGameResult.HasGameResult_)
					{
						NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame = new NetCache.ProfileNoticeDisconnectedGame
						{
							GameType = profileNotice.DcGameResult.GameType,
							FormatType = profileNotice.DcGameResult.FormatType,
							MissionId = profileNotice.DcGameResult.MissionId,
							GameResult = profileNotice.DcGameResult.GameResult_
						};
						if (profileNoticeDisconnectedGame.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_WINNER)
						{
							if (!profileNotice.DcGameResult.HasYourResult || !profileNotice.DcGameResult.HasOpponentResult)
							{
								Debug.LogError("Network.GetProfileNotices(): Missing PlayerResult");
								goto IL_8F6;
							}
							profileNoticeDisconnectedGame.YourResult = profileNotice.DcGameResult.YourResult;
							profileNoticeDisconnectedGame.OpponentResult = profileNotice.DcGameResult.OpponentResult;
						}
						profileNotice2 = profileNoticeDisconnectedGame;
						goto IL_89E;
					}
					Debug.LogError("Network.GetProfileNotices(): Missing GameResult");
				}
			}
			else if (profileNotice.HasDcGameResultNew)
			{
				if (!profileNotice.DcGameResultNew.HasGameType)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
				}
				else if (!profileNotice.DcGameResultNew.HasMissionId)
				{
					Debug.LogError("Network.GetProfileNotices(): Missing GameType");
				}
				else
				{
					if (profileNotice.DcGameResultNew.HasGameResult_)
					{
						NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame2 = new NetCache.ProfileNoticeDisconnectedGame
						{
							GameType = profileNotice.DcGameResultNew.GameType,
							FormatType = profileNotice.DcGameResultNew.FormatType,
							MissionId = profileNotice.DcGameResultNew.MissionId,
							GameResult = (ProfileNoticeDisconnectedGameResult.GameResult)profileNotice.DcGameResultNew.GameResult_
						};
						if (profileNoticeDisconnectedGame2.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_WINNER)
						{
							if (!profileNotice.DcGameResultNew.HasYourResult)
							{
								Debug.LogError("Network.GetProfileNotices(): Missing New PlayerResult");
							}
							profileNoticeDisconnectedGame2.YourResult = (ProfileNoticeDisconnectedGameResult.PlayerResult)profileNotice.DcGameResultNew.YourResult;
						}
						profileNotice2 = profileNoticeDisconnectedGame2;
						goto IL_89E;
					}
					Debug.LogError("Network.GetProfileNotices(): Missing GameResult");
				}
			}
			else
			{
				if (profileNotice.HasAdventureProgress)
				{
					NetCache.ProfileNoticeAdventureProgress profileNoticeAdventureProgress = new NetCache.ProfileNoticeAdventureProgress
					{
						Wing = profileNotice.AdventureProgress.WingId
					};
					NetCache.ProfileNotice.NoticeOrigin origin = (NetCache.ProfileNotice.NoticeOrigin)profileNotice.Origin;
					if (origin != NetCache.ProfileNotice.NoticeOrigin.ADVENTURE_PROGRESS)
					{
						if (origin == NetCache.ProfileNotice.NoticeOrigin.ADVENTURE_FLAGS)
						{
							profileNoticeAdventureProgress.Flags = new ulong?((ulong)(profileNotice.HasOriginData ? profileNotice.OriginData : 0L));
						}
					}
					else
					{
						profileNoticeAdventureProgress.Progress = new int?(profileNotice.HasOriginData ? ((int)profileNotice.OriginData) : 0);
					}
					profileNotice2 = profileNoticeAdventureProgress;
					goto IL_89E;
				}
				if (profileNotice.HasLevelUp)
				{
					profileNotice2 = new NetCache.ProfileNoticeLevelUp
					{
						HeroClass = profileNotice.LevelUp.HeroClass,
						NewLevel = profileNotice.LevelUp.NewLevel,
						TotalLevel = profileNotice.LevelUp.TotalLevel
					};
					goto IL_89E;
				}
				if (profileNotice.HasAccountLicense)
				{
					profileNotice2 = new NetCache.ProfileNoticeAcccountLicense
					{
						License = profileNotice.AccountLicense.License,
						CasID = profileNotice.AccountLicense.CasId
					};
					goto IL_89E;
				}
				if (profileNotice.HasTavernBrawlRewards)
				{
					profileNotice2 = new NetCache.ProfileNoticeTavernBrawlRewards
					{
						Chest = profileNotice.TavernBrawlRewards.RewardChest,
						Wins = profileNotice.TavernBrawlRewards.NumWins,
						Mode = (profileNotice.TavernBrawlRewards.HasBrawlMode ? profileNotice.TavernBrawlRewards.BrawlMode : TavernBrawlMode.TB_MODE_NORMAL)
					};
					goto IL_89E;
				}
				if (profileNotice.HasTavernBrawlTicket)
				{
					profileNotice2 = new NetCache.ProfileNoticeTavernBrawlTicket
					{
						TicketType = profileNotice.TavernBrawlTicket.TicketType,
						Quantity = profileNotice.TavernBrawlTicket.Quantity
					};
					goto IL_89E;
				}
				if (profileNotice.HasGenericRewardChest)
				{
					NetCache.ProfileNoticeGenericRewardChest profileNoticeGenericRewardChest = new NetCache.ProfileNoticeGenericRewardChest();
					profileNoticeGenericRewardChest.RewardChestAssetId = profileNotice.GenericRewardChest.RewardChestAssetId;
					profileNoticeGenericRewardChest.RewardChest = profileNotice.GenericRewardChest.RewardChest;
					profileNoticeGenericRewardChest.RewardChestByteSize = 0U;
					profileNoticeGenericRewardChest.RewardChestHash = null;
					if (profileNotice.GenericRewardChest.HasRewardChestByteSize)
					{
						profileNoticeGenericRewardChest.RewardChestByteSize = profileNotice.GenericRewardChest.RewardChestByteSize;
					}
					if (profileNotice.GenericRewardChest.HasRewardChestHash)
					{
						profileNoticeGenericRewardChest.RewardChestHash = profileNotice.GenericRewardChest.RewardChestHash;
					}
					profileNotice2 = profileNoticeGenericRewardChest;
					goto IL_89E;
				}
				if (profileNotice.HasLeaguePromotionRewards)
				{
					profileNotice2 = new NetCache.ProfileNoticeLeaguePromotionRewards
					{
						Chest = profileNotice.LeaguePromotionRewards.RewardChest,
						LeagueId = profileNotice.LeaguePromotionRewards.LeagueId
					};
					goto IL_89E;
				}
				if (profileNotice.HasDeckRemoved)
				{
					profileNotice2 = new NetCache.ProfileNoticeDeckRemoved
					{
						DeckID = profileNotice.DeckRemoved.DeckId
					};
					goto IL_89E;
				}
				if (profileNotice.HasFreeDeckChoice)
				{
					profileNotice2 = new NetCache.ProfileNoticeFreeDeckChoice();
					goto IL_89E;
				}
				if (profileNotice.HasDeckGranted)
				{
					profileNotice2 = new NetCache.ProfileNoticeDeckGranted
					{
						DeckDbiID = profileNotice.DeckGranted.DeckDbiId,
						ClassId = profileNotice.DeckGranted.ClassId,
						PlayerDeckID = profileNotice.DeckGranted.PlayerDeckId
					};
					goto IL_89E;
				}
				if (profileNotice.HasMiniSetGranted)
				{
					profileNotice2 = new NetCache.ProfileNoticeMiniSetGranted
					{
						MiniSetID = profileNotice.MiniSetGranted.MiniSetId
					};
					goto IL_89E;
				}
				if (profileNotice.HasSellableDeckGranted)
				{
					profileNotice2 = new NetCache.ProfileNoticeSellableDeckGranted
					{
						SellableDeckID = profileNotice.SellableDeckGranted.SellableDeckId,
						WasDeckGranted = profileNotice.SellableDeckGranted.WasDeckGranted,
						PlayerDeckID = profileNotice.SellableDeckGranted.PlayerDeckId
					};
					goto IL_89E;
				}
				Debug.LogError("Network.GetProfileNotices(): Unrecognized profile notice");
				goto IL_89E;
			}
			IL_8F6:
			i++;
			continue;
			IL_89E:
			if (profileNotice2 == null)
			{
				Debug.LogError("Network.GetProfileNotices(): Unhandled notice type! This notice will be lost!");
				goto IL_8F6;
			}
			profileNotice2.NoticeID = profileNotice.Entry;
			profileNotice2.Origin = (NetCache.ProfileNotice.NoticeOrigin)profileNotice.Origin;
			profileNotice2.OriginData = (profileNotice.HasOriginData ? profileNotice.OriginData : 0L);
			profileNotice2.Date = global::TimeUtils.PegDateToFileTimeUtc(profileNotice.When);
			result.Add(profileNotice2);
			goto IL_8F6;
		}
	}

	// Token: 0x06005589 RID: 21897 RVA: 0x001C065C File Offset: 0x001BE85C
	public NetCache.NetCacheMedalInfo GetMedalInfo()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			NetCache.NetCacheMedalInfo netCacheMedalInfo = new NetCache.NetCacheMedalInfo();
			foreach (object obj in Enum.GetValues(typeof(PegasusShared.FormatType)))
			{
				PegasusShared.FormatType formatType = (PegasusShared.FormatType)obj;
				if (formatType != PegasusShared.FormatType.FT_UNKNOWN)
				{
					MedalInfoData value = new MedalInfoData
					{
						FormatType = formatType
					};
					netCacheMedalInfo.MedalData.Add(formatType, value);
				}
			}
			return netCacheMedalInfo;
		}
		MedalInfo medalInfo = this.m_connectApi.GetMedalInfo();
		if (medalInfo == null)
		{
			return null;
		}
		return new NetCache.NetCacheMedalInfo(medalInfo);
	}

	// Token: 0x0600558A RID: 21898 RVA: 0x001C0700 File Offset: 0x001BE900
	public NetCache.NetCacheBaconRatingInfo GetBaconRatingInfo()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheBaconRatingInfo
			{
				Rating = 0
			};
		}
		ResponseWithRequest<BattlegroundsRatingInfoResponse, BattlegroundsRatingInfoRequest> responseWithRequest = this.m_connectApi.BattlegroundsRatingInfoResponse();
		if (responseWithRequest == null)
		{
			return null;
		}
		BattlegroundsRatingInfoResponse response = responseWithRequest.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCacheBaconRatingInfo
		{
			Rating = response.PlayerInfo.Rating
		};
	}

	// Token: 0x0600558B RID: 21899 RVA: 0x001C0754 File Offset: 0x001BE954
	public NetCache.NetCacheBaconPremiumStatus GetBaconPremiumStatus()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheBaconPremiumStatus
			{
				SeasonPremiumStatus = new List<BattlegroundSeasonPremiumStatus>()
			};
		}
		ResponseWithRequest<BattlegroundsPremiumStatusResponse, BattlegroundsPremiumStatusRequest> battlegroundsPremiumStatus = this.m_connectApi.GetBattlegroundsPremiumStatus();
		if (battlegroundsPremiumStatus == null)
		{
			return null;
		}
		BattlegroundsPremiumStatusResponse response = battlegroundsPremiumStatus.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCacheBaconPremiumStatus
		{
			SeasonPremiumStatus = response.SeasonPremiumStatus
		};
	}

	// Token: 0x0600558C RID: 21900 RVA: 0x001C07A8 File Offset: 0x001BE9A8
	public NetCache.NetCachePVPDRStatsInfo GetPVPDRStatsInfo()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCachePVPDRStatsInfo
			{
				Rating = 0,
				PaidRating = 0,
				HighWatermark = 0
			};
		}
		ResponseWithRequest<PVPDRStatsInfoResponse, PVPDRStatsInfoRequest> responseWithRequest = this.m_connectApi.PVPDRStatsInfoResponse();
		if (responseWithRequest == null)
		{
			return null;
		}
		PVPDRStatsInfoResponse response = responseWithRequest.Response;
		if (response == null)
		{
			return null;
		}
		return new NetCache.NetCachePVPDRStatsInfo
		{
			Rating = response.Rating,
			PaidRating = response.PaidRating,
			HighWatermark = response.HighWatermark
		};
	}

	// Token: 0x0600558D RID: 21901 RVA: 0x001C081D File Offset: 0x001BEA1D
	public GuardianVars GetGuardianVars()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new GuardianVars();
		}
		return this.m_connectApi.GetGuardianVars();
	}

	// Token: 0x0600558E RID: 21902 RVA: 0x001C0837 File Offset: 0x001BEA37
	public PlayerRecords GetPlayerRecordsPacket()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new PlayerRecords();
		}
		return this.m_connectApi.GetPlayerRecords();
	}

	// Token: 0x0600558F RID: 21903 RVA: 0x001C0854 File Offset: 0x001BEA54
	public static NetCache.NetCachePlayerRecords GetPlayerRecords(PlayerRecords packet)
	{
		if (packet == null)
		{
			return null;
		}
		NetCache.NetCachePlayerRecords netCachePlayerRecords = new NetCache.NetCachePlayerRecords();
		for (int i = 0; i < packet.Records.Count; i++)
		{
			PlayerRecord playerRecord = packet.Records[i];
			netCachePlayerRecords.Records.Add(new NetCache.PlayerRecord
			{
				RecordType = playerRecord.Type,
				Data = (playerRecord.HasData ? playerRecord.Data : 0),
				Wins = playerRecord.Wins,
				Losses = playerRecord.Losses,
				Ties = playerRecord.Ties
			});
		}
		return netCachePlayerRecords;
	}

	// Token: 0x06005590 RID: 21904 RVA: 0x001C08E8 File Offset: 0x001BEAE8
	public NetCache.NetCacheRewardProgress GetRewardProgress()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new NetCache.NetCacheRewardProgress();
		}
		RewardProgress rewardProgress = this.m_connectApi.GetRewardProgress();
		if (rewardProgress == null)
		{
			return null;
		}
		return new NetCache.NetCacheRewardProgress
		{
			Season = rewardProgress.SeasonNumber,
			SeasonEndDate = global::TimeUtils.PegDateToFileTimeUtc(rewardProgress.SeasonEnd),
			NextQuestCancelDate = global::TimeUtils.PegDateToFileTimeUtc(rewardProgress.NextQuestCancel)
		};
	}

	// Token: 0x06005591 RID: 21905 RVA: 0x001C0948 File Offset: 0x001BEB48
	public NetCache.NetCacheGamesPlayed GetGamesInfo()
	{
		GamesInfo gamesInfo = this.m_connectApi.GetGamesInfo();
		if (gamesInfo == null)
		{
			return null;
		}
		return new NetCache.NetCacheGamesPlayed
		{
			GamesStarted = gamesInfo.GamesStarted,
			GamesWon = gamesInfo.GamesWon,
			GamesLost = gamesInfo.GamesLost,
			FreeRewardProgress = gamesInfo.FreeRewardProgress
		};
	}

	// Token: 0x06005592 RID: 21906 RVA: 0x001C099B File Offset: 0x001BEB9B
	public ClientStaticAssetsResponse GetClientStaticAssetsResponse()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new ClientStaticAssetsResponse();
		}
		return this.m_connectApi.GetClientStaticAssetsResponse();
	}

	// Token: 0x06005593 RID: 21907 RVA: 0x001C09B8 File Offset: 0x001BEBB8
	public void RequestTavernBrawlInfo(BrawlType brawlType)
	{
		long? fsgId = FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null;
		this.m_connectApi.RequestTavernBrawlInfo(brawlType, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
	}

	// Token: 0x06005594 RID: 21908 RVA: 0x001C0A04 File Offset: 0x001BEC04
	public void RequestTavernBrawlPlayerRecord(BrawlType brawlType)
	{
		long? fsgId = FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null;
		this.m_connectApi.RequestTavernBrawlPlayerRecord(brawlType, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
	}

	// Token: 0x06005595 RID: 21909 RVA: 0x001C0A4F File Offset: 0x001BEC4F
	public TavernBrawlInfo GetTavernBrawlInfo()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new TavernBrawlInfo();
		}
		return this.m_connectApi.GetTavernBrawlInfo();
	}

	// Token: 0x06005596 RID: 21910 RVA: 0x001C0A69 File Offset: 0x001BEC69
	public TavernBrawlRequestSessionBeginResponse GetTavernBrawlSessionBegin()
	{
		return this.m_connectApi.GetTavernBrawlSessionBeginResponse();
	}

	// Token: 0x06005597 RID: 21911 RVA: 0x001C0A76 File Offset: 0x001BEC76
	public void TavernBrawlRetire()
	{
		this.m_connectApi.TavernBrawlRetire();
	}

	// Token: 0x06005598 RID: 21912 RVA: 0x001C0A83 File Offset: 0x001BEC83
	public TavernBrawlRequestSessionRetireResponse GetTavernBrawlSessionRetired()
	{
		return this.m_connectApi.GetTavernBrawlSessionRetired();
	}

	// Token: 0x06005599 RID: 21913 RVA: 0x001C0A90 File Offset: 0x001BEC90
	public void RequestTavernBrawlSessionBegin()
	{
		this.m_connectApi.RequestTavernBrawlSessionBegin();
	}

	// Token: 0x0600559A RID: 21914 RVA: 0x001C0A9D File Offset: 0x001BEC9D
	public void AckTavernBrawlSessionRewards()
	{
		this.m_connectApi.AckTavernBrawlSessionRewards();
	}

	// Token: 0x0600559B RID: 21915 RVA: 0x001C0AAC File Offset: 0x001BECAC
	public TavernBrawlPlayerRecord GetTavernBrawlRecord()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new TavernBrawlPlayerRecord();
		}
		TavernBrawlPlayerRecordResponse tavernBrawlPlayerRecordResponse = this.m_connectApi.GeTavernBrawlPlayerRecordResponse();
		if (tavernBrawlPlayerRecordResponse == null)
		{
			return null;
		}
		return tavernBrawlPlayerRecordResponse.Record;
	}

	// Token: 0x0600559C RID: 21916 RVA: 0x001C0ADD File Offset: 0x001BECDD
	public FavoriteHeroesResponse GetFavoriteHeroesResponse()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new FavoriteHeroesResponse();
		}
		return this.m_connectApi.GetFavoriteHeroesResponse();
	}

	// Token: 0x0600559D RID: 21917 RVA: 0x001C0AF7 File Offset: 0x001BECF7
	public AccountLicensesInfoResponse GetAccountLicensesInfoResponse()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new AccountLicensesInfoResponse();
		}
		return this.m_connectApi.GetAccountLicensesInfoResponse();
	}

	// Token: 0x0600559E RID: 21918 RVA: 0x001C0B11 File Offset: 0x001BED11
	public void RequestAccountLicensesUpdate()
	{
		this.m_connectApi.RequestAccountLicensesUpdate();
	}

	// Token: 0x0600559F RID: 21919 RVA: 0x001C0B1E File Offset: 0x001BED1E
	public UpdateAccountLicensesResponse GetUpdateAccountLicensesResponse()
	{
		return this.m_connectApi.GetUpdateAccountLicensesResponse();
	}

	// Token: 0x060055A0 RID: 21920 RVA: 0x001C0B2B File Offset: 0x001BED2B
	public UpdateLoginComplete GetUpdateLoginComplete()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new UpdateLoginComplete();
		}
		return this.m_connectApi.GetUpdateLoginComplete();
	}

	// Token: 0x060055A1 RID: 21921 RVA: 0x001C0B45 File Offset: 0x001BED45
	public HeroXP GetHeroXP()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return new HeroXP();
		}
		return this.m_connectApi.GetHeroXP();
	}

	// Token: 0x060055A2 RID: 21922 RVA: 0x001C0B5F File Offset: 0x001BED5F
	public void AckNotice(long id)
	{
		if (!NetCache.Get().RemoveNotice(id))
		{
			return;
		}
		global::Log.Achievements.Print("acking notice: {0}", new object[]
		{
			id
		});
		this.m_connectApi.AckNotice(id);
	}

	// Token: 0x060055A3 RID: 21923 RVA: 0x001C0B99 File Offset: 0x001BED99
	public void AckAchieveProgress(int id, int ackProgress)
	{
		global::Log.Achievements.Print("AckAchieveProgress: Achieve={0} Progress={1}", new object[]
		{
			id,
			ackProgress
		});
		this.m_connectApi.AckAchieveProgress(id, ackProgress);
	}

	// Token: 0x060055A4 RID: 21924 RVA: 0x001C0BCF File Offset: 0x001BEDCF
	public void AckQuest(int questId)
	{
		this.m_connectApi.AckQuest(questId);
	}

	// Token: 0x060055A5 RID: 21925 RVA: 0x001C0BDD File Offset: 0x001BEDDD
	public void CheckForNewQuests()
	{
		this.m_connectApi.CheckForNewQuests();
	}

	// Token: 0x060055A6 RID: 21926 RVA: 0x001C0BEA File Offset: 0x001BEDEA
	public void RerollQuest(int questId)
	{
		this.m_connectApi.RerollQuest(questId);
	}

	// Token: 0x060055A7 RID: 21927 RVA: 0x001C0BF8 File Offset: 0x001BEDF8
	public void AckAchievement(int achievementId)
	{
		this.m_connectApi.AckAchievement(achievementId);
	}

	// Token: 0x060055A8 RID: 21928 RVA: 0x001C0C06 File Offset: 0x001BEE06
	public void ClaimAchievementReward(int achievementId, int chooseOneRewardId = 0)
	{
		this.m_connectApi.ClaimAchievementReward(achievementId, chooseOneRewardId);
	}

	// Token: 0x060055A9 RID: 21929 RVA: 0x001C0C15 File Offset: 0x001BEE15
	public void AckRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack)
	{
		this.m_connectApi.AckRewardTrackReward(rewardTrackId, level, forPaidTrack);
	}

	// Token: 0x060055AA RID: 21930 RVA: 0x001C0C25 File Offset: 0x001BEE25
	public void ClaimRewardTrackReward(int rewardTrackId, int level, bool forPaidTrack, int chooseOneRewardItemId)
	{
		this.m_connectApi.ClaimRewardTrackReward(rewardTrackId, level, forPaidTrack, chooseOneRewardItemId);
	}

	// Token: 0x060055AB RID: 21931 RVA: 0x001C0C37 File Offset: 0x001BEE37
	public void CheckAccountLicenseAchieve(int achieveID)
	{
		this.m_connectApi.CheckAccountLicenseAchieve(achieveID);
	}

	// Token: 0x060055AC RID: 21932 RVA: 0x001C0C48 File Offset: 0x001BEE48
	public Network.AccountLicenseAchieveResponse GetAccountLicenseAchieveResponse()
	{
		PegasusUtil.AccountLicenseAchieveResponse accountLicenseAchieveResponse = this.m_connectApi.GetAccountLicenseAchieveResponse();
		if (accountLicenseAchieveResponse == null)
		{
			return null;
		}
		return new Network.AccountLicenseAchieveResponse
		{
			Achieve = accountLicenseAchieveResponse.Achieve,
			Result = (Network.AccountLicenseAchieveResponse.AchieveResult)accountLicenseAchieveResponse.Result_
		};
	}

	// Token: 0x060055AD RID: 21933 RVA: 0x001C0C84 File Offset: 0x001BEE84
	public void AckCardSeenBefore(int assetId, TAG_PREMIUM premium)
	{
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = assetId
		};
		if (premium != TAG_PREMIUM.NORMAL)
		{
			cardDef.Premium = (int)premium;
		}
		this.m_ackCardSeenPacket.CardDefs.Add(cardDef);
		if (this.m_ackCardSeenPacket.CardDefs.Count > 15)
		{
			this.SendAckCardsSeen();
		}
	}

	// Token: 0x060055AE RID: 21934 RVA: 0x001C0CD3 File Offset: 0x001BEED3
	public void AckWingProgress(int wingId, int ackId)
	{
		this.m_connectApi.AckWingProgress(wingId, ackId);
	}

	// Token: 0x060055AF RID: 21935 RVA: 0x001C0CE2 File Offset: 0x001BEEE2
	public void AcknowledgeBanner(int banner)
	{
		this.m_connectApi.AcknowledgeBanner(banner);
	}

	// Token: 0x060055B0 RID: 21936 RVA: 0x001C0CF0 File Offset: 0x001BEEF0
	public void SendAckCardsSeen()
	{
		this.m_connectApi.AckCardSeen(this.m_ackCardSeenPacket);
		this.m_ackCardSeenPacket.CardDefs.Clear();
	}

	// Token: 0x060055B1 RID: 21937 RVA: 0x001C0D14 File Offset: 0x001BEF14
	public void RequestNearbyFSGs(double latitude, double longitude, double accuracy, List<string> bssids)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.RequestNearbyFSGs(latitude, longitude, accuracy, bssids, platformBuilder);
	}

	// Token: 0x060055B2 RID: 21938 RVA: 0x001C0D3C File Offset: 0x001BEF3C
	public void RequestNearbyFSGs(List<string> bssids)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.RequestNearbyFSGs(bssids, platformBuilder);
	}

	// Token: 0x060055B3 RID: 21939 RVA: 0x001C0D60 File Offset: 0x001BEF60
	public void CheckInToFSG(long gatheringID, double latitude, double longitude, double accuracy, List<string> bssids)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.CheckInToFSG(gatheringID, latitude, longitude, accuracy, bssids, platformBuilder);
	}

	// Token: 0x060055B4 RID: 21940 RVA: 0x001C0D88 File Offset: 0x001BEF88
	public void CheckInToFSG(long gatheringID, List<string> bssids)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.CheckInToFSG(gatheringID, bssids, platformBuilder);
	}

	// Token: 0x060055B5 RID: 21941 RVA: 0x001C0DAC File Offset: 0x001BEFAC
	public void CheckOutOfFSG(long gatheringID)
	{
		global::Log.FiresideGatherings.Print("CheckOutOfFSG: sending check out to server for {0}", new object[]
		{
			gatheringID
		});
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.CheckOutOfFSG(gatheringID, platformBuilder);
	}

	// Token: 0x060055B6 RID: 21942 RVA: 0x001C0DEC File Offset: 0x001BEFEC
	public void InnkeeperSetupFSG(double latitude, double longitude, double accuracy, List<string> bssids, long fsgId)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.InnkeeperSetupFSG(bssids, fsgId, new GPSCoords
		{
			Latitude = latitude,
			Longitude = longitude,
			Accuracy = accuracy
		}, platformBuilder);
	}

	// Token: 0x060055B7 RID: 21943 RVA: 0x001C0E2C File Offset: 0x001BF02C
	public void InnkeeperSetupFSG(List<string> bssids, long fsgId)
	{
		Platform platformBuilder = this.GetPlatformBuilder();
		this.m_connectApi.InnkeeperSetupFSG(bssids, fsgId, platformBuilder);
	}

	// Token: 0x060055B8 RID: 21944 RVA: 0x001C0E4E File Offset: 0x001BF04E
	public void RequestFSGPatronListUpdate()
	{
		this.m_connectApi.RequestFSGPatronListUpdate();
	}

	// Token: 0x060055B9 RID: 21945 RVA: 0x001C0E5B File Offset: 0x001BF05B
	public void RequestLeaguePromoteSelf()
	{
		this.m_connectApi.RequestLeaguePromoteSelf();
	}

	// Token: 0x060055BA RID: 21946 RVA: 0x001C0E68 File Offset: 0x001BF068
	public RequestNearbyFSGsResponse GetRequestNearbyFSGsResponse()
	{
		return this.m_connectApi.GetRequestNearbyFSGsResponse();
	}

	// Token: 0x060055BB RID: 21947 RVA: 0x001C0E75 File Offset: 0x001BF075
	public CheckInToFSGResponse GetCheckInToFSGResponse()
	{
		return this.m_connectApi.GetCheckInToFSGResponse();
	}

	// Token: 0x060055BC RID: 21948 RVA: 0x001C0E82 File Offset: 0x001BF082
	public CheckOutOfFSGResponse GetCheckOutOfFSGResponse()
	{
		return this.m_connectApi.GetCheckOutOfFSGResponse();
	}

	// Token: 0x060055BD RID: 21949 RVA: 0x001C0E8F File Offset: 0x001BF08F
	public InnkeeperSetupGatheringResponse GetInnkeeperSetupGatheringResponse()
	{
		return this.m_connectApi.GetInnkeeperSetupGatheringResponse();
	}

	// Token: 0x060055BE RID: 21950 RVA: 0x001C0E9C File Offset: 0x001BF09C
	public PatronCheckedInToFSG GetPatronCheckedInToFSG()
	{
		return this.m_connectApi.GetPatronCheckedInToFSG();
	}

	// Token: 0x060055BF RID: 21951 RVA: 0x001C0EA9 File Offset: 0x001BF0A9
	public PatronCheckedOutOfFSG GetPatronCheckedOutOfFSG()
	{
		return this.m_connectApi.GetPatronCheckedOutOfFSG();
	}

	// Token: 0x060055C0 RID: 21952 RVA: 0x001C0EB6 File Offset: 0x001BF0B6
	public FSGPatronListUpdate GetFSGPatronListUpdate()
	{
		return this.m_connectApi.GetFSGPatronListUpdate();
	}

	// Token: 0x060055C1 RID: 21953 RVA: 0x001C0EC3 File Offset: 0x001BF0C3
	public FSGFeatureConfig GetFSGFeatureConfig()
	{
		return this.m_connectApi.GetFSGFeatureConfig();
	}

	// Token: 0x060055C2 RID: 21954 RVA: 0x001C0ED0 File Offset: 0x001BF0D0
	public LeaguePromoteSelfResponse GetLeaguePromoteSelfResponse()
	{
		return this.m_connectApi.GetLeaguePromoteSelfResponse();
	}

	// Token: 0x060055C3 RID: 21955 RVA: 0x001C0EDD File Offset: 0x001BF0DD
	public SmartDeckResponse GetSmartDeckResponse()
	{
		return this.m_connectApi.GetSmartDeckResponse();
	}

	// Token: 0x060055C4 RID: 21956 RVA: 0x001C0EEA File Offset: 0x001BF0EA
	public PlayerQuestStateUpdate GetPlayerQuestStateUpdate()
	{
		return this.m_connectApi.GetPlayerQuestStateUpdate();
	}

	// Token: 0x060055C5 RID: 21957 RVA: 0x001C0EF7 File Offset: 0x001BF0F7
	public PlayerQuestPoolStateUpdate GetPlayerQuestPoolStateUpdate()
	{
		return this.m_connectApi.GetPlayerQuestPoolStateUpdate();
	}

	// Token: 0x060055C6 RID: 21958 RVA: 0x001C0F04 File Offset: 0x001BF104
	public PlayerAchievementStateUpdate GetPlayerAchievementStateUpdate()
	{
		return this.m_connectApi.GetPlayerAchievementStateUpdate();
	}

	// Token: 0x060055C7 RID: 21959 RVA: 0x001C0F11 File Offset: 0x001BF111
	public PlayerRewardTrackStateUpdate GetPlayerRewardTrackStateUpdate()
	{
		return this.m_connectApi.GetPlayerRewardTrackStateUpdate();
	}

	// Token: 0x060055C8 RID: 21960 RVA: 0x001C0F1E File Offset: 0x001BF11E
	public RerollQuestResponse GetRerollQuestResponse()
	{
		return this.m_connectApi.GetRerollQuestResponse();
	}

	// Token: 0x060055C9 RID: 21961 RVA: 0x001C0F2B File Offset: 0x001BF12B
	public RewardTrackXpNotification GetRewardTrackXpNotification()
	{
		return this.m_connectApi.GetRewardTrackXpNotification();
	}

	// Token: 0x060055CA RID: 21962 RVA: 0x001C0F38 File Offset: 0x001BF138
	public RewardTrackUnclaimedNotification GetRewardTrackUnclaimedNotification()
	{
		return this.m_connectApi.GetRewardTrackUnclaimedNotification();
	}

	// Token: 0x060055CB RID: 21963 RVA: 0x001C0F45 File Offset: 0x001BF145
	public void RequestGameSaveData(List<long> keys, int clientToken)
	{
		this.m_connectApi.RequestGameSaveData(keys, clientToken);
	}

	// Token: 0x060055CC RID: 21964 RVA: 0x001C0F54 File Offset: 0x001BF154
	public GameSaveDataResponse GetGameSaveDataResponse()
	{
		return this.m_connectApi.GetGameSaveDataResponse();
	}

	// Token: 0x060055CD RID: 21965 RVA: 0x001C0F61 File Offset: 0x001BF161
	public void SetGameSaveData(List<GameSaveDataUpdate> dataUpdates, int clientToken)
	{
		this.m_connectApi.SetGameSaveData(dataUpdates, clientToken);
	}

	// Token: 0x060055CE RID: 21966 RVA: 0x001C0F70 File Offset: 0x001BF170
	public SetGameSaveDataResponse GetSetGameSaveDataResponse()
	{
		return this.m_connectApi.GetSetGameSaveDataResponse();
	}

	// Token: 0x060055CF RID: 21967 RVA: 0x001C0F80 File Offset: 0x001BF180
	public Network.CardSaleResult GetCardSaleResult()
	{
		BoughtSoldCard cardSaleResult = this.m_connectApi.GetCardSaleResult();
		if (cardSaleResult == null)
		{
			return null;
		}
		Network.CardSaleResult cardSaleResult2 = new Network.CardSaleResult
		{
			AssetID = cardSaleResult.Def.Asset,
			AssetName = GameUtils.TranslateDbIdToCardId(cardSaleResult.Def.Asset, false),
			Premium = (TAG_PREMIUM)(cardSaleResult.Def.HasPremium ? cardSaleResult.Def.Premium : 0),
			Action = (Network.CardSaleResult.SaleResult)cardSaleResult.Result_,
			Amount = cardSaleResult.Amount,
			Count = (cardSaleResult.HasCount ? cardSaleResult.Count : 1),
			Nerfed = (cardSaleResult.HasNerfed && cardSaleResult.Nerfed),
			UnitSellPrice = (cardSaleResult.HasUnitSellPrice ? cardSaleResult.UnitSellPrice : 0),
			UnitBuyPrice = (cardSaleResult.HasUnitBuyPrice ? cardSaleResult.UnitBuyPrice : 0)
		};
		if (cardSaleResult.HasCurrentCollectionCount)
		{
			cardSaleResult2.CurrentCollectionCount = new int?(cardSaleResult.CurrentCollectionCount);
		}
		else
		{
			cardSaleResult2.CurrentCollectionCount = null;
		}
		if (cardSaleResult.HasCollectionVersion)
		{
			NetCache.Get().AddExpectedCollectionModification(cardSaleResult.CollectionVersion);
		}
		return cardSaleResult2;
	}

	// Token: 0x060055D0 RID: 21968 RVA: 0x001C10A5 File Offset: 0x001BF2A5
	public void TriggerPlayedNearbyPlayerOnSubnet(BnetGameAccountId lastOpponentHSGameAccountID, ulong lastOpponentSessionStartTime, BnetGameAccountId otherPlayerHSGameAccountID, ulong otherPlayerSessionStartTime)
	{
		this.m_connectApi.TriggerPlayedNearbyPlayerOnSubnet(lastOpponentHSGameAccountID.GetHi(), lastOpponentHSGameAccountID.GetLo(), lastOpponentSessionStartTime, otherPlayerHSGameAccountID.GetHi(), otherPlayerHSGameAccountID.GetLo(), otherPlayerSessionStartTime);
	}

	// Token: 0x060055D1 RID: 21969 RVA: 0x001C10CD File Offset: 0x001BF2CD
	public void RequestAssetsVersion()
	{
		this.m_connectApi.RequestAssetsVersion(this.GetPlatformBuilder(), OfflineDataCache.GetCachedCollectionVersion(), OfflineDataCache.GetCachedDeckContentsTimes(), OfflineDataCache.GetCachedCollectionVersionLastModified());
	}

	// Token: 0x060055D2 RID: 21970 RVA: 0x001C10EF File Offset: 0x001BF2EF
	public void LoginOk()
	{
		this.m_connectApi.OnLoginComplete();
	}

	// Token: 0x060055D3 RID: 21971 RVA: 0x001C10FC File Offset: 0x001BF2FC
	public AssetsVersionResponse GetAssetsVersion()
	{
		return this.m_connectApi.GetAssetsVersionResponse();
	}

	// Token: 0x060055D4 RID: 21972 RVA: 0x001C1109 File Offset: 0x001BF309
	public GetAssetResponse GetAssetResponse()
	{
		return this.m_connectApi.GetAssetResponse();
	}

	// Token: 0x060055D5 RID: 21973 RVA: 0x001C1118 File Offset: 0x001BF318
	public void SendAssetRequest(int clientToken, List<AssetKey> requestKeys)
	{
		if (requestKeys == null || requestKeys.Count == 0)
		{
			return;
		}
		long? fsgId = FiresideGatheringManager.Get().IsCheckedIn ? new long?(FiresideGatheringManager.Get().CurrentFsgId) : null;
		this.m_connectApi.SendAssetRequest(clientToken, requestKeys, fsgId, FiresideGatheringManager.Get().CurrentFsgSharedSecretKey);
	}

	// Token: 0x060055D6 RID: 21974 RVA: 0x001C1170 File Offset: 0x001BF370
	public ServerResult GetServerResult()
	{
		return this.m_connectApi.GetServerResult();
	}

	// Token: 0x060055D7 RID: 21975 RVA: 0x001C1180 File Offset: 0x001BF380
	private Platform GetPlatformBuilder()
	{
		Platform platform = new Platform
		{
			Os = (int)PlatformSettings.OS,
			Screen = (int)PlatformSettings.Screen,
			Name = PlatformSettings.DeviceName,
			UniqueDeviceIdentifier = SystemInfo.deviceUniqueIdentifier
		};
		AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
		if (androidStore != AndroidStore.NONE)
		{
			platform.Store = (int)androidStore;
		}
		return platform;
	}

	// Token: 0x060055D8 RID: 21976 RVA: 0x001C11D8 File Offset: 0x001BF3D8
	public bool SendDebugConsoleCommand(string command)
	{
		if (!this.IsConnectedToGameServer())
		{
			global::Log.Net.Print(string.Format("Cannot send command '{0}' to server; no game server is active.", command), Array.Empty<object>());
			return false;
		}
		if (this.m_connectApi.AllowDebugConnections() && command != null)
		{
			this.m_connectApi.SendDebugConsoleCommand(command);
		}
		return true;
	}

	// Token: 0x060055D9 RID: 21977 RVA: 0x001C1226 File Offset: 0x001BF426
	public void SendDebugConsoleResponse(int responseType, string message)
	{
		this.m_connectApi.SendDebugConsoleResponse(responseType, message);
	}

	// Token: 0x060055DA RID: 21978 RVA: 0x001C1238 File Offset: 0x001BF438
	public string GetDebugConsoleCommand()
	{
		DebugConsoleCommand debugConsoleCommand = this.m_connectApi.GetDebugConsoleCommand();
		if (debugConsoleCommand == null)
		{
			return string.Empty;
		}
		return debugConsoleCommand.Command;
	}

	// Token: 0x060055DB RID: 21979 RVA: 0x001C1260 File Offset: 0x001BF460
	public Network.DebugConsoleResponse GetDebugConsoleResponse()
	{
		BobNetProto.DebugConsoleResponse debugConsoleResponse = this.m_connectApi.GetDebugConsoleResponse();
		if (debugConsoleResponse == null)
		{
			return null;
		}
		return new Network.DebugConsoleResponse
		{
			Type = (int)debugConsoleResponse.ResponseType_,
			Response = debugConsoleResponse.Response
		};
	}

	// Token: 0x060055DC RID: 21980 RVA: 0x001C129B File Offset: 0x001BF49B
	public void SendDebugCommandRequest(DebugCommandRequest packet)
	{
		this.m_connectApi.SendDebugCommandRequest(packet);
	}

	// Token: 0x060055DD RID: 21981 RVA: 0x001C12A9 File Offset: 0x001BF4A9
	public DebugCommandResponse GetDebugCommandResponse()
	{
		return this.m_connectApi.GetDebugCommandResponse();
	}

	// Token: 0x060055DE RID: 21982 RVA: 0x001C12B6 File Offset: 0x001BF4B6
	public void SendLocateCheatServerRequest()
	{
		this.m_connectApi.SendLocateCheatServerRequest();
	}

	// Token: 0x060055DF RID: 21983 RVA: 0x001C12C3 File Offset: 0x001BF4C3
	public LocateCheatServerResponse GetLocateCheatServerResponse()
	{
		return this.m_connectApi.GetLocateCheatServerResponse();
	}

	// Token: 0x060055E0 RID: 21984 RVA: 0x001C12D0 File Offset: 0x001BF4D0
	public GameToConnectNotification GetGameToConnectNotification()
	{
		return this.m_connectApi.GetGameToConnectNotification();
	}

	// Token: 0x060055E1 RID: 21985 RVA: 0x001C12DD File Offset: 0x001BF4DD
	public void GetServerTimeRequest()
	{
		this.m_connectApi.GetServerTimeRequest((long)global::TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now));
	}

	// Token: 0x060055E2 RID: 21986 RVA: 0x001C12F4 File Offset: 0x001BF4F4
	public void ReportBlizzardCheckoutStatus(BlizzardCheckoutStatus status, HearthstoneCheckoutTransactionData data = null)
	{
		this.m_connectApi.ReportBlizzardCheckoutStatus(status, data, (long)global::TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now));
	}

	// Token: 0x060055E3 RID: 21987 RVA: 0x001C130D File Offset: 0x001BF50D
	public ResponseWithRequest<GetServerTimeResponse, GetServerTimeRequest> GetServerTimeResponse()
	{
		return this.m_connectApi.GetServerTimeResponse();
	}

	// Token: 0x060055E4 RID: 21988 RVA: 0x001C131A File Offset: 0x001BF51A
	public void SimulateUncleanDisconnectFromGameServer()
	{
		if (this.m_connectApi.HasGameServerConnection())
		{
			this.m_connectApi.DisconnectFromGameServer();
		}
	}

	// Token: 0x060055E5 RID: 21989 RVA: 0x001C1334 File Offset: 0x001BF534
	public void SimulateReceivedPacketFromServer(PegasusPacket packet)
	{
		this.m_dispatcherImpl.NotifyUtilResponseReceived(packet);
	}

	// Token: 0x060055E6 RID: 21990 RVA: 0x00090064 File Offset: 0x0008E264
	private static string GetStoredUserName()
	{
		return null;
	}

	// Token: 0x060055E7 RID: 21991 RVA: 0x00090064 File Offset: 0x0008E264
	private static string GetStoredBNetIP()
	{
		return null;
	}

	// Token: 0x060055E8 RID: 21992 RVA: 0x00090064 File Offset: 0x0008E264
	private static string GetStoredVersion()
	{
		return null;
	}

	// Token: 0x060055E9 RID: 21993 RVA: 0x001C1342 File Offset: 0x001BF542
	public UtilLogRelay GetUtilLogRelay()
	{
		return this.m_connectApi.GetUtilLogRelay();
	}

	// Token: 0x060055EA RID: 21994 RVA: 0x001C134F File Offset: 0x001BF54F
	public GameLogRelay GetGameLogRelay()
	{
		return this.m_connectApi.GetGameLogRelay();
	}

	// Token: 0x04004A4E RID: 19022
	public const int NoSubOption = -1;

	// Token: 0x04004A4F RID: 19023
	public const int NoPosition = 0;

	// Token: 0x04004A50 RID: 19024
	public static string TutorialServer = "01";

	// Token: 0x04004A51 RID: 19025
	public const string CosmeticVersion = "20.4";

	// Token: 0x04004A52 RID: 19026
	public const string CosmeticRevision = "0";

	// Token: 0x04004A53 RID: 19027
	public const string VersionPostfix = "";

	// Token: 0x04004A54 RID: 19028
	public const string DEFAULT_INTERNAL_ENVIRONMENT = "bn12-01.battle.net";

	// Token: 0x04004A55 RID: 19029
	public const string DEFAULT_PUBLIC_ENVIRONMENT = "us.actual.battle.net";

	// Token: 0x04004A56 RID: 19030
	private static readonly float PROCESS_WARNING = 15f;

	// Token: 0x04004A57 RID: 19031
	private static readonly float PROCESS_WARNING_REPORT_GAP = 1f;

	// Token: 0x04004A58 RID: 19032
	private const int MIN_DEFERRED_WAIT = 30;

	// Token: 0x04004A59 RID: 19033
	public const int SEND_DECK_DATA_NO_HERO_ASSET_CHANGE = -1;

	// Token: 0x04004A5A RID: 19034
	public const int SEND_DECK_DATA_NO_CARD_BACK_CHANGE = -1;

	// Token: 0x04004A5B RID: 19035
	private const bool ReconnectAfterFailedPings = true;

	// Token: 0x04004A5C RID: 19036
	private const float ERROR_HANDLING_DELAY = 0.4f;

	// Token: 0x04004A5D RID: 19037
	public static readonly PlatformDependentValue<bool> LAUNCHES_WITH_BNET_APP = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = true,
		Mac = true,
		iOS = false,
		Android = false
	};

	// Token: 0x04004A5E RID: 19038
	public static readonly PlatformDependentValue<bool> TUTORIALS_WITHOUT_ACCOUNT = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	// Token: 0x04004A5F RID: 19039
	private static readonly global::Map<constants.BnetRegion, string> RegionToTutorialName = new global::Map<constants.BnetRegion, string>
	{
		{
			constants.BnetRegion.REGION_US,
			"us-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_EU,
			"eu-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_KR,
			"kr-tutorial{0}.actual.battle.net"
		},
		{
			constants.BnetRegion.REGION_CN,
			"cn-tutorial{0}.actual.battlenet.com.cn"
		}
	};

	// Token: 0x04004A60 RID: 19040
	private static readonly SortedDictionary<int, int> m_deferredMessageResponseMap = new SortedDictionary<int, int>
	{
		{
			305,
			306
		},
		{
			303,
			304
		},
		{
			205,
			307
		},
		{
			314,
			315
		}
	};

	// Token: 0x04004A61 RID: 19041
	private static readonly SortedDictionary<int, int> m_deferredGetAccountInfoMessageResponseMap = new SortedDictionary<int, int>
	{
		{
			11,
			233
		},
		{
			18,
			264
		},
		{
			4,
			232
		},
		{
			2,
			202
		},
		{
			10,
			231
		},
		{
			15,
			260
		},
		{
			19,
			271
		},
		{
			8,
			270
		},
		{
			21,
			283
		},
		{
			7,
			236
		},
		{
			27,
			318
		},
		{
			28,
			325
		},
		{
			29,
			608
		}
	};

	// Token: 0x04004A62 RID: 19042
	private IDispatcher m_dispatcherImpl;

	// Token: 0x04004A63 RID: 19043
	private global::Map<int, List<Network.NetHandler>> m_netHandlers = new global::Map<int, List<Network.NetHandler>>();

	// Token: 0x04004A64 RID: 19044
	private Network.QueueInfoHandler m_queueInfoHandler;

	// Token: 0x04004A65 RID: 19045
	private Network.GameQueueHandler m_gameQueueHandler;

	// Token: 0x04004A66 RID: 19046
	private int m_numConnectionFailures;

	// Token: 0x04004A67 RID: 19047
	private ConnectAPI m_connectApi;

	// Token: 0x04004A68 RID: 19048
	private uint m_gameServerKeepAliveFrequencySeconds;

	// Token: 0x04004A69 RID: 19049
	private uint m_gameServerKeepAliveRetry;

	// Token: 0x04004A6A RID: 19050
	private uint m_gameServerKeepAliveWaitForInternetSeconds;

	// Token: 0x04004A6B RID: 19051
	private bool m_gameConceded;

	// Token: 0x04004A6C RID: 19052
	private bool m_disconnectRequested;

	// Token: 0x04004A6D RID: 19053
	private double m_timeInternetUnreachable;

	// Token: 0x04004A6E RID: 19054
	private AckCardSeen m_ackCardSeenPacket = new AckCardSeen();

	// Token: 0x04004A6F RID: 19055
	private readonly List<Network.ConnectErrorParams> m_errorList = new List<Network.ConnectErrorParams>();

	// Token: 0x04004A72 RID: 19058
	private List<Network.ThrottledPacketListener> m_throttledPacketListeners = new List<Network.ThrottledPacketListener>();

	// Token: 0x04004A73 RID: 19059
	private List<Network.RequestContext> m_inTransitRequests = new List<Network.RequestContext>();

	// Token: 0x04004A74 RID: 19060
	private static float m_maxDeferredWait = 120f;

	// Token: 0x04004A75 RID: 19061
	private static bool s_shouldBeConnectedToAurora = !Network.TUTORIALS_WITHOUT_ACCOUNT;

	// Token: 0x04004A76 RID: 19062
	private static bool s_running;

	// Token: 0x04004A77 RID: 19063
	private static UnityUrlDownloader s_urlDownloader = new UnityUrlDownloader();

	// Token: 0x04004A78 RID: 19064
	private Network.NetworkState m_state;

	// Token: 0x04004A79 RID: 19065
	private NetworkReachabilityManager m_networkReachabilityManager;

	// Token: 0x02002059 RID: 8281
	private class HSClientInterface : ClientInterface
	{
		// Token: 0x06011CF4 RID: 72948 RVA: 0x004F9B38 File Offset: 0x004F7D38
		public string GetVersion()
		{
			return Network.GetVersion();
		}

		// Token: 0x06011CF5 RID: 72949 RVA: 0x004F9B40 File Offset: 0x004F7D40
		public string GetUserAgent()
		{
			string text = "Hearthstone/";
			text = string.Concat(new object[]
			{
				text,
				"20.4.",
				84593,
				" ("
			});
			if (PlatformSettings.OS == OSCategory.iOS)
			{
				text += "iOS;";
			}
			else if (PlatformSettings.OS == OSCategory.Android)
			{
				text += "Android;";
			}
			else if (PlatformSettings.OS == OSCategory.PC)
			{
				text += "PC;";
			}
			else if (PlatformSettings.OS == OSCategory.Mac)
			{
				text += "Mac;";
			}
			else
			{
				text += "UNKNOWN;";
			}
			text = string.Concat(new object[]
			{
				text,
				this.CleanUserAgentString(SystemInfo.deviceModel),
				";",
				SystemInfo.deviceType,
				";",
				this.CleanUserAgentString(SystemInfo.deviceUniqueIdentifier),
				";",
				SystemInfo.graphicsDeviceID,
				";",
				this.CleanUserAgentString(SystemInfo.graphicsDeviceName),
				";",
				this.CleanUserAgentString(SystemInfo.graphicsDeviceVendor),
				";",
				SystemInfo.graphicsDeviceVendorID,
				";",
				this.CleanUserAgentString(SystemInfo.graphicsDeviceVersion),
				";",
				SystemInfo.graphicsMemorySize,
				";",
				SystemInfo.graphicsShaderLevel,
				";",
				SystemInfo.npotSupport,
				";",
				this.CleanUserAgentString(SystemInfo.operatingSystem),
				";",
				SystemInfo.processorCount,
				";",
				this.CleanUserAgentString(SystemInfo.processorType),
				";",
				SystemInfo.supportedRenderTargetCount,
				";",
				SystemInfo.supports3DTextures.ToString(),
				";",
				SystemInfo.supportsAccelerometer.ToString(),
				";",
				SystemInfo.supportsComputeShaders.ToString(),
				";",
				SystemInfo.supportsGyroscope.ToString(),
				";",
				SystemInfo.supportsImageEffects.ToString(),
				";",
				SystemInfo.supportsInstancing.ToString(),
				";",
				SystemInfo.supportsLocationService.ToString(),
				";",
				SystemInfo.supportsRenderTextures.ToString(),
				";",
				SystemInfo.supportsRenderToCubemap.ToString(),
				";",
				SystemInfo.supportsShadows.ToString(),
				";",
				SystemInfo.supportsSparseTextures.ToString(),
				";",
				SystemInfo.supportsStencil,
				";",
				SystemInfo.supportsVibration.ToString(),
				";",
				SystemInfo.systemMemorySize,
				";",
				SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf).ToString(),
				";",
				SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444).ToString(),
				";",
				SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth).ToString(),
				";",
				SystemInfo.graphicsDeviceVersion.StartsWith("Metal").ToString(),
				";",
				Screen.currentResolution.width,
				";",
				Screen.currentResolution.height,
				";",
				Screen.dpi,
				";"
			});
			if (PlatformSettings.IsMobile())
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					text += "Phone;";
				}
				else
				{
					text += "Tablet;";
				}
			}
			else
			{
				text += "Desktop;";
			}
			text += Application.genuine.ToString();
			text += ") Battle.net/CSharp";
			global::Log.Net.Print(text, Array.Empty<object>());
			return text;
		}

		// Token: 0x06011CF6 RID: 72950 RVA: 0x004F9FE9 File Offset: 0x004F81E9
		public int GetApplicationVersion()
		{
			return 84593;
		}

		// Token: 0x06011CF7 RID: 72951 RVA: 0x004F9FF0 File Offset: 0x004F81F0
		private string CleanUserAgentString(string data)
		{
			return Regex.Replace(data, "[^a-zA-Z0-9_.]+", "_");
		}

		// Token: 0x06011CF8 RID: 72952 RVA: 0x004FA002 File Offset: 0x004F8202
		public string GetBasePersistentDataPath()
		{
			return FileUtils.PersistentDataPath;
		}

		// Token: 0x06011CF9 RID: 72953 RVA: 0x004FA009 File Offset: 0x004F8209
		public string GetTemporaryCachePath()
		{
			return this.s_tempCachePath;
		}

		// Token: 0x06011CFA RID: 72954 RVA: 0x004FA011 File Offset: 0x004F8211
		public bool GetDisableConnectionMetering()
		{
			return Vars.Key("Aurora.DisableConnectionMetering").GetBool(false);
		}

		// Token: 0x06011CFB RID: 72955 RVA: 0x004FA024 File Offset: 0x004F8224
		public constants.MobileEnv GetMobileEnvironment()
		{
			MobileEnv mobileEnvironment = HearthstoneApplication.GetMobileEnvironment();
			if (mobileEnvironment == MobileEnv.PRODUCTION)
			{
				return constants.MobileEnv.PRODUCTION;
			}
			return constants.MobileEnv.DEVELOPMENT;
		}

		// Token: 0x06011CFC RID: 72956 RVA: 0x004FA040 File Offset: 0x004F8240
		public string GetAuroraVersionName()
		{
			return 84593.ToString();
		}

		// Token: 0x06011CFD RID: 72957 RVA: 0x004FA05A File Offset: 0x004F825A
		public string GetLocaleName()
		{
			return Localization.GetLocaleName();
		}

		// Token: 0x06011CFE RID: 72958 RVA: 0x004FA061 File Offset: 0x004F8261
		public string GetPlatformName()
		{
			return "Win";
		}

		// Token: 0x06011CFF RID: 72959 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public constants.RuntimeEnvironment GetRuntimeEnvironment()
		{
			return constants.RuntimeEnvironment.Mono;
		}

		// Token: 0x06011D00 RID: 72960 RVA: 0x004FA068 File Offset: 0x004F8268
		public IUrlDownloader GetUrlDownloader()
		{
			return Network.s_urlDownloader;
		}

		// Token: 0x06011D01 RID: 72961 RVA: 0x004FA06F File Offset: 0x004F826F
		public int GetDataVersion()
		{
			return GameDbf.GetDataVersion();
		}

		// Token: 0x0400DCDC RID: 56540
		private string s_tempCachePath = Application.temporaryCachePath;
	}

	// Token: 0x0200205A RID: 8282
	public enum BnetLoginState
	{
		// Token: 0x0400DCDE RID: 56542
		BATTLE_NET_UNKNOWN,
		// Token: 0x0400DCDF RID: 56543
		BATTLE_NET_LOGGING_IN,
		// Token: 0x0400DCE0 RID: 56544
		BATTLE_NET_TIMEOUT,
		// Token: 0x0400DCE1 RID: 56545
		BATTLE_NET_LOGIN_FAILED,
		// Token: 0x0400DCE2 RID: 56546
		BATTLE_NET_LOGGED_IN
	}

	// Token: 0x0200205B RID: 8283
	public enum BoosterSource
	{
		// Token: 0x0400DCE4 RID: 56548
		UNKNOWN,
		// Token: 0x0400DCE5 RID: 56549
		ARENA_REWARD = 3,
		// Token: 0x0400DCE6 RID: 56550
		BOUGHT,
		// Token: 0x0400DCE7 RID: 56551
		LICENSED = 6,
		// Token: 0x0400DCE8 RID: 56552
		CS_GIFT = 8,
		// Token: 0x0400DCE9 RID: 56553
		QUEST_REWARD = 10,
		// Token: 0x0400DCEA RID: 56554
		BOUGHT_GOLD
	}

	// Token: 0x0200205C RID: 8284
	public enum Version
	{
		// Token: 0x0400DCEC RID: 56556
		Major = 20,
		// Token: 0x0400DCED RID: 56557
		Minor = 4,
		// Token: 0x0400DCEE RID: 56558
		Patch = 0,
		// Token: 0x0400DCEF RID: 56559
		Sku = 0
	}

	// Token: 0x0200205D RID: 8285
	public enum AuthResult
	{
		// Token: 0x0400DCF1 RID: 56561
		UNKNOWN,
		// Token: 0x0400DCF2 RID: 56562
		ALLOWED,
		// Token: 0x0400DCF3 RID: 56563
		INVALID,
		// Token: 0x0400DCF4 RID: 56564
		SECOND,
		// Token: 0x0400DCF5 RID: 56565
		OFFLINE
	}

	// Token: 0x0200205E RID: 8286
	public class ConnectErrorParams : ErrorParams
	{
		// Token: 0x06011D03 RID: 72963 RVA: 0x004FA089 File Offset: 0x004F8289
		public ConnectErrorParams()
		{
			this.m_creationTime = Time.realtimeSinceStartup;
		}

		// Token: 0x0400DCF6 RID: 56566
		public float m_creationTime;
	}

	// Token: 0x0200205F RID: 8287
	private class RequestContext
	{
		// Token: 0x06011D04 RID: 72964 RVA: 0x004FA09C File Offset: 0x004F829C
		public RequestContext(int pendingResponseId, int requestId, int requestSubId, Network.TimeoutHandler timeoutHandler)
		{
			this.m_waitUntil = Time.realtimeSinceStartup + Network.GetMaxDeferredWait();
			this.m_pendingResponseId = pendingResponseId;
			this.m_requestId = requestId;
			this.m_requestSubId = requestSubId;
			this.m_timeoutHandler = timeoutHandler;
		}

		// Token: 0x0400DCF7 RID: 56567
		public float m_waitUntil;

		// Token: 0x0400DCF8 RID: 56568
		public int m_pendingResponseId;

		// Token: 0x0400DCF9 RID: 56569
		public int m_requestId;

		// Token: 0x0400DCFA RID: 56570
		public int m_requestSubId;

		// Token: 0x0400DCFB RID: 56571
		public Network.TimeoutHandler m_timeoutHandler;
	}

	// Token: 0x02002060 RID: 8288
	public class UnavailableReason
	{
		// Token: 0x0400DCFC RID: 56572
		public string mainReason;

		// Token: 0x0400DCFD RID: 56573
		public string subReason;

		// Token: 0x0400DCFE RID: 56574
		public string extraData;
	}

	// Token: 0x02002061 RID: 8289
	private class BnetErrorListener : global::EventListener<Network.BnetErrorCallback>
	{
		// Token: 0x06011D06 RID: 72966 RVA: 0x004FA0D2 File Offset: 0x004F82D2
		public bool Fire(BnetErrorInfo info)
		{
			return this.m_callback(info, this.m_userData);
		}
	}

	// Token: 0x02002062 RID: 8290
	// (Invoke) Token: 0x06011D09 RID: 72969
	public delegate void NetHandler();

	// Token: 0x02002063 RID: 8291
	// (Invoke) Token: 0x06011D0D RID: 72973
	public delegate void ThrottledPacketListener(int packetID, long retryMillis);

	// Token: 0x02002064 RID: 8292
	// (Invoke) Token: 0x06011D11 RID: 72977
	public delegate void QueueInfoHandler(Network.QueueInfo queueInfo);

	// Token: 0x02002065 RID: 8293
	// (Invoke) Token: 0x06011D15 RID: 72981
	public delegate void GameQueueHandler(QueueEvent queueEvent);

	// Token: 0x02002066 RID: 8294
	// (Invoke) Token: 0x06011D19 RID: 72985
	public delegate void TimeoutHandler(int pendingResponseId, int requestId, int requestSubId);

	// Token: 0x02002067 RID: 8295
	// (Invoke) Token: 0x06011D1D RID: 72989
	public delegate void BnetEventHandler(BattleNet.BnetEvent[] updates);

	// Token: 0x02002068 RID: 8296
	// (Invoke) Token: 0x06011D21 RID: 72993
	public delegate void FriendsHandler(FriendsUpdate[] updates);

	// Token: 0x02002069 RID: 8297
	// (Invoke) Token: 0x06011D25 RID: 72997
	public delegate void WhisperHandler(BnetWhisper[] whispers);

	// Token: 0x0200206A RID: 8298
	// (Invoke) Token: 0x06011D29 RID: 73001
	public delegate void PartyHandler(PartyEvent[] updates);

	// Token: 0x0200206B RID: 8299
	// (Invoke) Token: 0x06011D2D RID: 73005
	public delegate void PresenceHandler(PresenceUpdate[] updates);

	// Token: 0x0200206C RID: 8300
	// (Invoke) Token: 0x06011D31 RID: 73009
	public delegate void ShutdownHandler(int minutes);

	// Token: 0x0200206D RID: 8301
	// (Invoke) Token: 0x06011D35 RID: 73013
	public delegate void ChallengeHandler(ChallengeInfo[] challenges);

	// Token: 0x0200206E RID: 8302
	// (Invoke) Token: 0x06011D39 RID: 73017
	public delegate void SpectatorInviteReceivedHandler(Invite invite);

	// Token: 0x0200206F RID: 8303
	// (Invoke) Token: 0x06011D3D RID: 73021
	public delegate bool BnetErrorCallback(BnetErrorInfo info, object userData);

	// Token: 0x02002070 RID: 8304
	// (Invoke) Token: 0x06011D41 RID: 73025
	public delegate void GameServerDisconnectEvent(BattleNetErrors errorCode);

	// Token: 0x02002071 RID: 8305
	private struct NetworkState
	{
		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06011D44 RID: 73028 RVA: 0x004FA0EE File Offset: 0x004F82EE
		// (set) Token: 0x06011D45 RID: 73029 RVA: 0x004FA0F6 File Offset: 0x004F82F6
		public BattleNetLogSource LogSource { get; set; }

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x06011D46 RID: 73030 RVA: 0x004FA0FF File Offset: 0x004F82FF
		// (set) Token: 0x06011D47 RID: 73031 RVA: 0x004FA107 File Offset: 0x004F8307
		public BnetGameType FindingBnetGameType { get; set; }

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x06011D48 RID: 73032 RVA: 0x004FA110 File Offset: 0x004F8310
		// (set) Token: 0x06011D49 RID: 73033 RVA: 0x004FA118 File Offset: 0x004F8318
		public float LastCall { get; set; }

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x06011D4A RID: 73034 RVA: 0x004FA121 File Offset: 0x004F8321
		// (set) Token: 0x06011D4B RID: 73035 RVA: 0x004FA129 File Offset: 0x004F8329
		public float LastCallReport { get; set; }

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x06011D4C RID: 73036 RVA: 0x004FA132 File Offset: 0x004F8332
		// (set) Token: 0x06011D4D RID: 73037 RVA: 0x004FA13A File Offset: 0x004F833A
		public int LastCallFrame { get; set; }

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x06011D4E RID: 73038 RVA: 0x004FA143 File Offset: 0x004F8343
		// (set) Token: 0x06011D4F RID: 73039 RVA: 0x004FA14B File Offset: 0x004F834B
		public Network.BnetEventHandler CurrentBnetEventHandler { get; set; }

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x06011D50 RID: 73040 RVA: 0x004FA154 File Offset: 0x004F8354
		// (set) Token: 0x06011D51 RID: 73041 RVA: 0x004FA15C File Offset: 0x004F835C
		public Network.FriendsHandler CurrentFriendsHandler { get; set; }

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x06011D52 RID: 73042 RVA: 0x004FA165 File Offset: 0x004F8365
		// (set) Token: 0x06011D53 RID: 73043 RVA: 0x004FA16D File Offset: 0x004F836D
		public Network.WhisperHandler CurrentWhisperHandler { get; set; }

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x06011D54 RID: 73044 RVA: 0x004FA176 File Offset: 0x004F8376
		// (set) Token: 0x06011D55 RID: 73045 RVA: 0x004FA17E File Offset: 0x004F837E
		public Network.PresenceHandler CurrentPresenceHandler { get; set; }

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x06011D56 RID: 73046 RVA: 0x004FA187 File Offset: 0x004F8387
		// (set) Token: 0x06011D57 RID: 73047 RVA: 0x004FA18F File Offset: 0x004F838F
		public Network.ShutdownHandler CurrentShutdownHandler { get; set; }

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x06011D58 RID: 73048 RVA: 0x004FA198 File Offset: 0x004F8398
		// (set) Token: 0x06011D59 RID: 73049 RVA: 0x004FA1A0 File Offset: 0x004F83A0
		public Network.ChallengeHandler CurrentChallengeHandler { get; set; }

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x06011D5A RID: 73050 RVA: 0x004FA1A9 File Offset: 0x004F83A9
		// (set) Token: 0x06011D5B RID: 73051 RVA: 0x004FA1B1 File Offset: 0x004F83B1
		public global::Map<BnetFeature, List<Network.BnetErrorListener>> FeatureBnetErrorListeners { get; set; }

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x06011D5C RID: 73052 RVA: 0x004FA1BA File Offset: 0x004F83BA
		// (set) Token: 0x06011D5D RID: 73053 RVA: 0x004FA1C2 File Offset: 0x004F83C2
		public List<Network.BnetErrorListener> GlobalBnetErrorListeners { get; set; }

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x06011D5E RID: 73054 RVA: 0x004FA1CB File Offset: 0x004F83CB
		// (set) Token: 0x06011D5F RID: 73055 RVA: 0x004FA1D3 File Offset: 0x004F83D3
		public Network.GameServerDisconnectEvent GameServerDisconnectEventListener { get; set; }

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x06011D60 RID: 73056 RVA: 0x004FA1DC File Offset: 0x004F83DC
		// (set) Token: 0x06011D61 RID: 73057 RVA: 0x004FA1E4 File Offset: 0x004F83E4
		public FindGameResult LastFindGameParameters { get; set; }

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x06011D62 RID: 73058 RVA: 0x004FA1ED File Offset: 0x004F83ED
		// (set) Token: 0x06011D63 RID: 73059 RVA: 0x004FA1F5 File Offset: 0x004F83F5
		public ConnectToGameServer LastConnectToGameServerInfo { get; set; }

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x06011D64 RID: 73060 RVA: 0x004FA1FE File Offset: 0x004F83FE
		// (set) Token: 0x06011D65 RID: 73061 RVA: 0x004FA206 File Offset: 0x004F8406
		public GameServerInfo LastGameServerInfo { get; set; }

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x06011D66 RID: 73062 RVA: 0x004FA20F File Offset: 0x004F840F
		// (set) Token: 0x06011D67 RID: 73063 RVA: 0x004FA217 File Offset: 0x004F8417
		public string DelayedError { get; set; }

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x06011D68 RID: 73064 RVA: 0x004FA220 File Offset: 0x004F8420
		// (set) Token: 0x06011D69 RID: 73065 RVA: 0x004FA228 File Offset: 0x004F8428
		public float TimeBeforeAllowReset { get; set; }

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x06011D6A RID: 73066 RVA: 0x004FA231 File Offset: 0x004F8431
		// (set) Token: 0x06011D6B RID: 73067 RVA: 0x004FA239 File Offset: 0x004F8439
		public List<ClientStateNotification> QueuedClientStateNotifications { get; set; }

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x06011D6C RID: 73068 RVA: 0x004FA242 File Offset: 0x004F8442
		// (set) Token: 0x06011D6D RID: 73069 RVA: 0x004FA24A File Offset: 0x004F844A
		public bgs.types.EntityId CachedGameAccountId { get; set; }

		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x06011D6E RID: 73070 RVA: 0x004FA253 File Offset: 0x004F8453
		// (set) Token: 0x06011D6F RID: 73071 RVA: 0x004FA25B File Offset: 0x004F845B
		public constants.BnetRegion CachedRegion { get; set; }

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x06011D70 RID: 73072 RVA: 0x004FA264 File Offset: 0x004F8464
		// (set) Token: 0x06011D71 RID: 73073 RVA: 0x004FA26C File Offset: 0x004F846C
		public int CurrentCreateDeckRequestId { get; set; }

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x06011D72 RID: 73074 RVA: 0x004FA275 File Offset: 0x004F8475
		// (set) Token: 0x06011D73 RID: 73075 RVA: 0x004FA27D File Offset: 0x004F847D
		public HashSet<int> InTransitOfflineCreateDeckRequestIds { get; set; }

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x06011D74 RID: 73076 RVA: 0x004FA286 File Offset: 0x004F8486
		// (set) Token: 0x06011D75 RID: 73077 RVA: 0x004FA28E File Offset: 0x004F848E
		public HashSet<long> DeckIdsWaitingToDiffAgainstOfflineCache { get; set; }

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x06011D76 RID: 73078 RVA: 0x004FA297 File Offset: 0x004F8497
		// (set) Token: 0x06011D77 RID: 73079 RVA: 0x004FA29F File Offset: 0x004F849F
		public global::Map<int, Network.TimeoutHandler> NetTimeoutHandlers { get; set; }

		// Token: 0x06011D78 RID: 73080 RVA: 0x004FA2A8 File Offset: 0x004F84A8
		public void SetDefaults()
		{
			this.LogSource = new BattleNetLogSource("Network");
			this.FindingBnetGameType = BnetGameType.BGT_UNKNOWN;
			this.LastCall = Time.realtimeSinceStartup;
			this.LastCallReport = Time.realtimeSinceStartup;
			this.LastCallFrame = 0;
			this.FeatureBnetErrorListeners = new global::Map<BnetFeature, List<Network.BnetErrorListener>>();
			this.GlobalBnetErrorListeners = new List<Network.BnetErrorListener>();
			this.QueuedClientStateNotifications = new List<ClientStateNotification>();
			this.InTransitOfflineCreateDeckRequestIds = new HashSet<int>();
			this.DeckIdsWaitingToDiffAgainstOfflineCache = new HashSet<long>();
			this.NetTimeoutHandlers = new global::Map<int, Network.TimeoutHandler>();
		}
	}

	// Token: 0x02002072 RID: 8306
	public class QueueInfo
	{
		// Token: 0x0400DD19 RID: 56601
		public int position;

		// Token: 0x0400DD1A RID: 56602
		public long secondsTilEnd;

		// Token: 0x0400DD1B RID: 56603
		public long stdev;
	}

	// Token: 0x02002073 RID: 8307
	public class CanceledQuest
	{
		// Token: 0x06011D7A RID: 73082 RVA: 0x004FA32B File Offset: 0x004F852B
		public CanceledQuest()
		{
			this.AchieveID = 0;
			this.Canceled = false;
			this.NextQuestCancelDate = 0L;
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x06011D7B RID: 73083 RVA: 0x004FA349 File Offset: 0x004F8549
		// (set) Token: 0x06011D7C RID: 73084 RVA: 0x004FA351 File Offset: 0x004F8551
		public int AchieveID { get; set; }

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x06011D7D RID: 73085 RVA: 0x004FA35A File Offset: 0x004F855A
		// (set) Token: 0x06011D7E RID: 73086 RVA: 0x004FA362 File Offset: 0x004F8562
		public bool Canceled { get; set; }

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x06011D7F RID: 73087 RVA: 0x004FA36B File Offset: 0x004F856B
		// (set) Token: 0x06011D80 RID: 73088 RVA: 0x004FA373 File Offset: 0x004F8573
		public long NextQuestCancelDate { get; set; }

		// Token: 0x06011D81 RID: 73089 RVA: 0x004FA37C File Offset: 0x004F857C
		public override string ToString()
		{
			return string.Format("[CanceledQuest AchieveID={0} Canceled={1} NextQuestCancelDate={2}]", this.AchieveID, this.Canceled, this.NextQuestCancelDate);
		}
	}

	// Token: 0x02002074 RID: 8308
	public class TriggeredEvent
	{
		// Token: 0x06011D82 RID: 73090 RVA: 0x004FA3A9 File Offset: 0x004F85A9
		public TriggeredEvent()
		{
			this.EventID = 0;
			this.Success = false;
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x06011D83 RID: 73091 RVA: 0x004FA3BF File Offset: 0x004F85BF
		// (set) Token: 0x06011D84 RID: 73092 RVA: 0x004FA3C7 File Offset: 0x004F85C7
		public int EventID { get; set; }

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x06011D85 RID: 73093 RVA: 0x004FA3D0 File Offset: 0x004F85D0
		// (set) Token: 0x06011D86 RID: 73094 RVA: 0x004FA3D8 File Offset: 0x004F85D8
		public bool Success { get; set; }
	}

	// Token: 0x02002075 RID: 8309
	public class AdventureProgress
	{
		// Token: 0x06011D87 RID: 73095 RVA: 0x004FA3E1 File Offset: 0x004F85E1
		public AdventureProgress()
		{
			this.Wing = 0;
			this.Progress = 0;
			this.Ack = 0;
			this.Flags = 0UL;
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x06011D88 RID: 73096 RVA: 0x004FA406 File Offset: 0x004F8606
		// (set) Token: 0x06011D89 RID: 73097 RVA: 0x004FA40E File Offset: 0x004F860E
		public int Wing { get; set; }

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x06011D8A RID: 73098 RVA: 0x004FA417 File Offset: 0x004F8617
		// (set) Token: 0x06011D8B RID: 73099 RVA: 0x004FA41F File Offset: 0x004F861F
		public int Progress { get; set; }

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x06011D8C RID: 73100 RVA: 0x004FA428 File Offset: 0x004F8628
		// (set) Token: 0x06011D8D RID: 73101 RVA: 0x004FA430 File Offset: 0x004F8630
		public int Ack { get; set; }

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x06011D8E RID: 73102 RVA: 0x004FA439 File Offset: 0x004F8639
		// (set) Token: 0x06011D8F RID: 73103 RVA: 0x004FA441 File Offset: 0x004F8641
		public ulong Flags { get; set; }
	}

	// Token: 0x02002076 RID: 8310
	public class CardSaleResult
	{
		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x06011D90 RID: 73104 RVA: 0x004FA44A File Offset: 0x004F864A
		// (set) Token: 0x06011D91 RID: 73105 RVA: 0x004FA452 File Offset: 0x004F8652
		public Network.CardSaleResult.SaleResult Action { get; set; }

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x06011D92 RID: 73106 RVA: 0x004FA45B File Offset: 0x004F865B
		// (set) Token: 0x06011D93 RID: 73107 RVA: 0x004FA463 File Offset: 0x004F8663
		public int AssetID { get; set; }

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x06011D94 RID: 73108 RVA: 0x004FA46C File Offset: 0x004F866C
		// (set) Token: 0x06011D95 RID: 73109 RVA: 0x004FA474 File Offset: 0x004F8674
		public string AssetName { get; set; }

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x06011D96 RID: 73110 RVA: 0x004FA47D File Offset: 0x004F867D
		// (set) Token: 0x06011D97 RID: 73111 RVA: 0x004FA485 File Offset: 0x004F8685
		public TAG_PREMIUM Premium { get; set; }

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x06011D98 RID: 73112 RVA: 0x004FA48E File Offset: 0x004F868E
		// (set) Token: 0x06011D99 RID: 73113 RVA: 0x004FA496 File Offset: 0x004F8696
		public int Amount { get; set; }

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06011D9A RID: 73114 RVA: 0x004FA49F File Offset: 0x004F869F
		// (set) Token: 0x06011D9B RID: 73115 RVA: 0x004FA4A7 File Offset: 0x004F86A7
		public int Count { get; set; }

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x06011D9C RID: 73116 RVA: 0x004FA4B0 File Offset: 0x004F86B0
		// (set) Token: 0x06011D9D RID: 73117 RVA: 0x004FA4B8 File Offset: 0x004F86B8
		public bool Nerfed { get; set; }

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x06011D9E RID: 73118 RVA: 0x004FA4C1 File Offset: 0x004F86C1
		// (set) Token: 0x06011D9F RID: 73119 RVA: 0x004FA4C9 File Offset: 0x004F86C9
		public int UnitSellPrice { get; set; }

		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x06011DA0 RID: 73120 RVA: 0x004FA4D2 File Offset: 0x004F86D2
		// (set) Token: 0x06011DA1 RID: 73121 RVA: 0x004FA4DA File Offset: 0x004F86DA
		public int UnitBuyPrice { get; set; }

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06011DA2 RID: 73122 RVA: 0x004FA4E3 File Offset: 0x004F86E3
		// (set) Token: 0x06011DA3 RID: 73123 RVA: 0x004FA4EB File Offset: 0x004F86EB
		public int? CurrentCollectionCount { get; set; }

		// Token: 0x06011DA4 RID: 73124 RVA: 0x004FA4F4 File Offset: 0x004F86F4
		public override string ToString()
		{
			return string.Format("[CardSaleResult Action={0} assetName={1} premium={2} amount={3} count={4}]", new object[]
			{
				this.Action,
				this.AssetName,
				this.Premium,
				this.Amount,
				this.Count
			});
		}

		// Token: 0x02002986 RID: 10630
		public enum SaleResult
		{
			// Token: 0x0400FCFC RID: 64764
			GENERIC_FAILURE = 1,
			// Token: 0x0400FCFD RID: 64765
			CARD_WAS_SOLD,
			// Token: 0x0400FCFE RID: 64766
			CARD_WAS_BOUGHT,
			// Token: 0x0400FCFF RID: 64767
			SOULBOUND,
			// Token: 0x0400FD00 RID: 64768
			FAILED_WRONG_SELL_PRICE,
			// Token: 0x0400FD01 RID: 64769
			FAILED_WRONG_BUY_PRICE,
			// Token: 0x0400FD02 RID: 64770
			FAILED_NO_PERMISSION,
			// Token: 0x0400FD03 RID: 64771
			FAILED_EVENT_NOT_ACTIVE,
			// Token: 0x0400FD04 RID: 64772
			COUNT_MISMATCH
		}
	}

	// Token: 0x02002077 RID: 8311
	public class BeginDraft
	{
		// Token: 0x06011DA6 RID: 73126 RVA: 0x004FA552 File Offset: 0x004F8752
		public BeginDraft()
		{
			this.Heroes = new List<NetCache.CardDefinition>();
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06011DA7 RID: 73127 RVA: 0x004FA565 File Offset: 0x004F8765
		// (set) Token: 0x06011DA8 RID: 73128 RVA: 0x004FA56D File Offset: 0x004F876D
		public long DeckID { get; set; }

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06011DA9 RID: 73129 RVA: 0x004FA576 File Offset: 0x004F8776
		// (set) Token: 0x06011DAA RID: 73130 RVA: 0x004FA57E File Offset: 0x004F877E
		public List<NetCache.CardDefinition> Heroes { get; set; }

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06011DAB RID: 73131 RVA: 0x004FA587 File Offset: 0x004F8787
		// (set) Token: 0x06011DAC RID: 73132 RVA: 0x004FA58F File Offset: 0x004F878F
		public int Wins { get; set; }

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06011DAD RID: 73133 RVA: 0x004FA598 File Offset: 0x004F8798
		// (set) Token: 0x06011DAE RID: 73134 RVA: 0x004FA5A0 File Offset: 0x004F87A0
		public int MaxSlot { get; set; }

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x06011DAF RID: 73135 RVA: 0x004FA5A9 File Offset: 0x004F87A9
		// (set) Token: 0x06011DB0 RID: 73136 RVA: 0x004FA5B1 File Offset: 0x004F87B1
		public ArenaSession Session { get; set; }

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x06011DB1 RID: 73137 RVA: 0x004FA5BA File Offset: 0x004F87BA
		// (set) Token: 0x06011DB2 RID: 73138 RVA: 0x004FA5C2 File Offset: 0x004F87C2
		public DraftSlotType SlotType { get; set; }

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x06011DB3 RID: 73139 RVA: 0x004FA5CB File Offset: 0x004F87CB
		// (set) Token: 0x06011DB4 RID: 73140 RVA: 0x004FA5D3 File Offset: 0x004F87D3
		public List<DraftSlotType> UniqueSlotTypesForDraft { get; set; }
	}

	// Token: 0x02002078 RID: 8312
	public class DraftChoicesAndContents
	{
		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x06011DB5 RID: 73141 RVA: 0x004FA5DC File Offset: 0x004F87DC
		// (set) Token: 0x06011DB6 RID: 73142 RVA: 0x004FA5E4 File Offset: 0x004F87E4
		public int Slot { get; set; }

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x06011DB7 RID: 73143 RVA: 0x004FA5ED File Offset: 0x004F87ED
		// (set) Token: 0x06011DB8 RID: 73144 RVA: 0x004FA5F5 File Offset: 0x004F87F5
		public List<NetCache.CardDefinition> Choices { get; set; }

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x06011DB9 RID: 73145 RVA: 0x004FA5FE File Offset: 0x004F87FE
		// (set) Token: 0x06011DBA RID: 73146 RVA: 0x004FA606 File Offset: 0x004F8806
		public NetCache.CardDefinition Hero { get; set; }

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x06011DBB RID: 73147 RVA: 0x004FA60F File Offset: 0x004F880F
		// (set) Token: 0x06011DBC RID: 73148 RVA: 0x004FA617 File Offset: 0x004F8817
		public NetCache.CardDefinition HeroPower { get; set; }

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06011DBD RID: 73149 RVA: 0x004FA620 File Offset: 0x004F8820
		// (set) Token: 0x06011DBE RID: 73150 RVA: 0x004FA628 File Offset: 0x004F8828
		public Network.DeckContents DeckInfo { get; set; }

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x06011DBF RID: 73151 RVA: 0x004FA631 File Offset: 0x004F8831
		// (set) Token: 0x06011DC0 RID: 73152 RVA: 0x004FA639 File Offset: 0x004F8839
		public int Wins { get; set; }

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x06011DC1 RID: 73153 RVA: 0x004FA642 File Offset: 0x004F8842
		// (set) Token: 0x06011DC2 RID: 73154 RVA: 0x004FA64A File Offset: 0x004F884A
		public int Losses { get; set; }

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x06011DC3 RID: 73155 RVA: 0x004FA653 File Offset: 0x004F8853
		// (set) Token: 0x06011DC4 RID: 73156 RVA: 0x004FA65B File Offset: 0x004F885B
		public Network.RewardChest Chest { get; set; }

		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06011DC5 RID: 73157 RVA: 0x004FA664 File Offset: 0x004F8864
		// (set) Token: 0x06011DC6 RID: 73158 RVA: 0x004FA66C File Offset: 0x004F886C
		public int MaxWins { get; set; }

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06011DC7 RID: 73159 RVA: 0x004FA675 File Offset: 0x004F8875
		// (set) Token: 0x06011DC8 RID: 73160 RVA: 0x004FA67D File Offset: 0x004F887D
		public int MaxSlot { get; set; }

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06011DC9 RID: 73161 RVA: 0x004FA686 File Offset: 0x004F8886
		// (set) Token: 0x06011DCA RID: 73162 RVA: 0x004FA68E File Offset: 0x004F888E
		public ArenaSession Session { get; set; }

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x06011DCB RID: 73163 RVA: 0x004FA697 File Offset: 0x004F8897
		// (set) Token: 0x06011DCC RID: 73164 RVA: 0x004FA69F File Offset: 0x004F889F
		public DraftSlotType SlotType { get; set; }

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x06011DCD RID: 73165 RVA: 0x004FA6A8 File Offset: 0x004F88A8
		// (set) Token: 0x06011DCE RID: 73166 RVA: 0x004FA6B0 File Offset: 0x004F88B0
		public List<DraftSlotType> UniqueSlotTypesForDraft { get; set; }

		// Token: 0x06011DCF RID: 73167 RVA: 0x004FA6BC File Offset: 0x004F88BC
		public DraftChoicesAndContents()
		{
			this.Choices = new List<NetCache.CardDefinition>();
			this.Hero = new NetCache.CardDefinition();
			this.HeroPower = new NetCache.CardDefinition();
			this.DeckInfo = new Network.DeckContents();
			this.Chest = null;
			this.UniqueSlotTypesForDraft = new List<DraftSlotType>();
		}
	}

	// Token: 0x02002079 RID: 8313
	public class DraftChosen
	{
		// Token: 0x06011DD0 RID: 73168 RVA: 0x004FA70D File Offset: 0x004F890D
		public DraftChosen()
		{
			this.ChosenCard = new NetCache.CardDefinition();
			this.NextChoices = new List<NetCache.CardDefinition>();
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x06011DD1 RID: 73169 RVA: 0x004FA72B File Offset: 0x004F892B
		// (set) Token: 0x06011DD2 RID: 73170 RVA: 0x004FA733 File Offset: 0x004F8933
		public NetCache.CardDefinition ChosenCard { get; set; }

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x06011DD3 RID: 73171 RVA: 0x004FA73C File Offset: 0x004F893C
		// (set) Token: 0x06011DD4 RID: 73172 RVA: 0x004FA744 File Offset: 0x004F8944
		public List<NetCache.CardDefinition> NextChoices { get; set; }

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x06011DD5 RID: 73173 RVA: 0x004FA74D File Offset: 0x004F894D
		// (set) Token: 0x06011DD6 RID: 73174 RVA: 0x004FA755 File Offset: 0x004F8955
		public DraftSlotType SlotType { get; set; }
	}

	// Token: 0x0200207A RID: 8314
	public class RewardChest
	{
		// Token: 0x06011DD7 RID: 73175 RVA: 0x004FA75E File Offset: 0x004F895E
		public RewardChest()
		{
			this.Rewards = new List<RewardData>();
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x06011DD8 RID: 73176 RVA: 0x004FA771 File Offset: 0x004F8971
		// (set) Token: 0x06011DD9 RID: 73177 RVA: 0x004FA779 File Offset: 0x004F8979
		public List<RewardData> Rewards { get; set; }
	}

	// Token: 0x0200207B RID: 8315
	public class DraftRetired
	{
		// Token: 0x06011DDA RID: 73178 RVA: 0x004FA782 File Offset: 0x004F8982
		public DraftRetired()
		{
			this.Deck = 0L;
			this.Chest = new Network.RewardChest();
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x06011DDB RID: 73179 RVA: 0x004FA79D File Offset: 0x004F899D
		// (set) Token: 0x06011DDC RID: 73180 RVA: 0x004FA7A5 File Offset: 0x004F89A5
		public long Deck { get; set; }

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06011DDD RID: 73181 RVA: 0x004FA7AE File Offset: 0x004F89AE
		// (set) Token: 0x06011DDE RID: 73182 RVA: 0x004FA7B6 File Offset: 0x004F89B6
		public Network.RewardChest Chest { get; set; }
	}

	// Token: 0x0200207C RID: 8316
	public class MassDisenchantResponse
	{
		// Token: 0x06011DDF RID: 73183 RVA: 0x004FA7BF File Offset: 0x004F89BF
		public MassDisenchantResponse()
		{
			this.Amount = 0;
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x06011DE0 RID: 73184 RVA: 0x004FA7CE File Offset: 0x004F89CE
		// (set) Token: 0x06011DE1 RID: 73185 RVA: 0x004FA7D6 File Offset: 0x004F89D6
		public int Amount { get; set; }
	}

	// Token: 0x0200207D RID: 8317
	public class SetFavoriteHeroResponse
	{
		// Token: 0x06011DE2 RID: 73186 RVA: 0x004FA7DF File Offset: 0x004F89DF
		public SetFavoriteHeroResponse()
		{
			this.Success = false;
			this.HeroClass = TAG_CLASS.INVALID;
			this.Hero = null;
		}

		// Token: 0x0400DD4A RID: 56650
		public bool Success;

		// Token: 0x0400DD4B RID: 56651
		public TAG_CLASS HeroClass;

		// Token: 0x0400DD4C RID: 56652
		public NetCache.CardDefinition Hero;
	}

	// Token: 0x0200207E RID: 8318
	public class PurchaseErrorInfo
	{
		// Token: 0x06011DE3 RID: 73187 RVA: 0x004FA7FC File Offset: 0x004F89FC
		public PurchaseErrorInfo()
		{
			this.Error = Network.PurchaseErrorInfo.ErrorType.UNKNOWN;
			this.PurchaseInProgressProductID = string.Empty;
			this.ErrorCode = string.Empty;
		}

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x06011DE4 RID: 73188 RVA: 0x004FA821 File Offset: 0x004F8A21
		// (set) Token: 0x06011DE5 RID: 73189 RVA: 0x004FA829 File Offset: 0x004F8A29
		public Network.PurchaseErrorInfo.ErrorType Error { get; set; }

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x06011DE6 RID: 73190 RVA: 0x004FA832 File Offset: 0x004F8A32
		// (set) Token: 0x06011DE7 RID: 73191 RVA: 0x004FA83A File Offset: 0x004F8A3A
		public string PurchaseInProgressProductID { get; set; }

		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x06011DE8 RID: 73192 RVA: 0x004FA843 File Offset: 0x004F8A43
		// (set) Token: 0x06011DE9 RID: 73193 RVA: 0x004FA84B File Offset: 0x004F8A4B
		public string ErrorCode { get; set; }

		// Token: 0x02002987 RID: 10631
		public enum ErrorType
		{
			// Token: 0x0400FD06 RID: 64774
			UNKNOWN = -1,
			// Token: 0x0400FD07 RID: 64775
			SUCCESS,
			// Token: 0x0400FD08 RID: 64776
			STILL_IN_PROGRESS,
			// Token: 0x0400FD09 RID: 64777
			INVALID_BNET,
			// Token: 0x0400FD0A RID: 64778
			SERVICE_NA,
			// Token: 0x0400FD0B RID: 64779
			PURCHASE_IN_PROGRESS,
			// Token: 0x0400FD0C RID: 64780
			DATABASE,
			// Token: 0x0400FD0D RID: 64781
			INVALID_QUANTITY,
			// Token: 0x0400FD0E RID: 64782
			DUPLICATE_LICENSE,
			// Token: 0x0400FD0F RID: 64783
			REQUEST_NOT_SENT,
			// Token: 0x0400FD10 RID: 64784
			NO_ACTIVE_BPAY,
			// Token: 0x0400FD11 RID: 64785
			FAILED_RISK,
			// Token: 0x0400FD12 RID: 64786
			CANCELED,
			// Token: 0x0400FD13 RID: 64787
			WAIT_MOP,
			// Token: 0x0400FD14 RID: 64788
			WAIT_CONFIRM,
			// Token: 0x0400FD15 RID: 64789
			WAIT_RISK,
			// Token: 0x0400FD16 RID: 64790
			PRODUCT_NA,
			// Token: 0x0400FD17 RID: 64791
			RISK_TIMEOUT,
			// Token: 0x0400FD18 RID: 64792
			PRODUCT_ALREADY_OWNED,
			// Token: 0x0400FD19 RID: 64793
			WAIT_THIRD_PARTY_RECEIPT,
			// Token: 0x0400FD1A RID: 64794
			PRODUCT_EVENT_HAS_ENDED,
			// Token: 0x0400FD1B RID: 64795
			BP_GENERIC_FAIL = 100,
			// Token: 0x0400FD1C RID: 64796
			BP_INVALID_CC_EXPIRY,
			// Token: 0x0400FD1D RID: 64797
			BP_RISK_ERROR,
			// Token: 0x0400FD1E RID: 64798
			BP_NO_VALID_PAYMENT,
			// Token: 0x0400FD1F RID: 64799
			BP_PAYMENT_AUTH,
			// Token: 0x0400FD20 RID: 64800
			BP_PROVIDER_DENIED,
			// Token: 0x0400FD21 RID: 64801
			BP_PURCHASE_BAN,
			// Token: 0x0400FD22 RID: 64802
			BP_SPENDING_LIMIT,
			// Token: 0x0400FD23 RID: 64803
			BP_PARENTAL_CONTROL,
			// Token: 0x0400FD24 RID: 64804
			BP_THROTTLED,
			// Token: 0x0400FD25 RID: 64805
			BP_THIRD_PARTY_BAD_RECEIPT,
			// Token: 0x0400FD26 RID: 64806
			BP_THIRD_PARTY_RECEIPT_USED,
			// Token: 0x0400FD27 RID: 64807
			BP_PRODUCT_UNIQUENESS_VIOLATED,
			// Token: 0x0400FD28 RID: 64808
			BP_REGION_IS_DOWN,
			// Token: 0x0400FD29 RID: 64809
			E_BP_GENERIC_FAIL_RETRY_CONTACT_CS_IF_PERSISTS = 115,
			// Token: 0x0400FD2A RID: 64810
			E_BP_CHALLENGE_ID_FAILED_VERIFICATION
		}
	}

	// Token: 0x0200207F RID: 8319
	public class PurchaseCanceledResponse
	{
		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x06011DEA RID: 73194 RVA: 0x004FA854 File Offset: 0x004F8A54
		// (set) Token: 0x06011DEB RID: 73195 RVA: 0x004FA85C File Offset: 0x004F8A5C
		public Network.PurchaseCanceledResponse.CancelResult Result { get; set; }

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x06011DEC RID: 73196 RVA: 0x004FA865 File Offset: 0x004F8A65
		// (set) Token: 0x06011DED RID: 73197 RVA: 0x004FA86D File Offset: 0x004F8A6D
		public long TransactionID { get; set; }

		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x06011DEE RID: 73198 RVA: 0x004FA876 File Offset: 0x004F8A76
		// (set) Token: 0x06011DEF RID: 73199 RVA: 0x004FA87E File Offset: 0x004F8A7E
		public long? PMTProductID { get; set; }

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x06011DF0 RID: 73200 RVA: 0x004FA887 File Offset: 0x004F8A87
		// (set) Token: 0x06011DF1 RID: 73201 RVA: 0x004FA88F File Offset: 0x004F8A8F
		public string CurrencyCode { get; set; }

		// Token: 0x02002988 RID: 10632
		public enum CancelResult
		{
			// Token: 0x0400FD2C RID: 64812
			SUCCESS,
			// Token: 0x0400FD2D RID: 64813
			NOT_ALLOWED,
			// Token: 0x0400FD2E RID: 64814
			NOTHING_TO_CANCEL
		}
	}

	// Token: 0x02002080 RID: 8320
	public class BattlePayStatus
	{
		// Token: 0x06011DF3 RID: 73203 RVA: 0x004FA898 File Offset: 0x004F8A98
		public BattlePayStatus()
		{
			this.State = Network.BattlePayStatus.PurchaseState.UNKNOWN;
			this.TransactionID = 0L;
			this.ThirdPartyID = string.Empty;
			this.PMTProductID = null;
			this.PurchaseError = new Network.PurchaseErrorInfo();
			this.BattlePayAvailable = false;
			this.Provider = MoneyOrGTAPPTransaction.UNKNOWN_PROVIDER;
		}

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x06011DF4 RID: 73204 RVA: 0x004FA8F1 File Offset: 0x004F8AF1
		// (set) Token: 0x06011DF5 RID: 73205 RVA: 0x004FA8F9 File Offset: 0x004F8AF9
		public Network.BattlePayStatus.PurchaseState State { get; set; }

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x06011DF6 RID: 73206 RVA: 0x004FA902 File Offset: 0x004F8B02
		// (set) Token: 0x06011DF7 RID: 73207 RVA: 0x004FA90A File Offset: 0x004F8B0A
		public long TransactionID { get; set; }

		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x06011DF8 RID: 73208 RVA: 0x004FA913 File Offset: 0x004F8B13
		// (set) Token: 0x06011DF9 RID: 73209 RVA: 0x004FA91B File Offset: 0x004F8B1B
		public string ThirdPartyID { get; set; }

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x06011DFA RID: 73210 RVA: 0x004FA924 File Offset: 0x004F8B24
		// (set) Token: 0x06011DFB RID: 73211 RVA: 0x004FA92C File Offset: 0x004F8B2C
		public long? PMTProductID { get; set; }

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x06011DFC RID: 73212 RVA: 0x004FA935 File Offset: 0x004F8B35
		// (set) Token: 0x06011DFD RID: 73213 RVA: 0x004FA93D File Offset: 0x004F8B3D
		public Network.PurchaseErrorInfo PurchaseError { get; set; }

		// Token: 0x170026E3 RID: 9955
		// (get) Token: 0x06011DFE RID: 73214 RVA: 0x004FA946 File Offset: 0x004F8B46
		// (set) Token: 0x06011DFF RID: 73215 RVA: 0x004FA94E File Offset: 0x004F8B4E
		public bool BattlePayAvailable { get; set; }

		// Token: 0x170026E4 RID: 9956
		// (get) Token: 0x06011E00 RID: 73216 RVA: 0x004FA957 File Offset: 0x004F8B57
		// (set) Token: 0x06011E01 RID: 73217 RVA: 0x004FA95F File Offset: 0x004F8B5F
		public string CurrencyCode { get; set; }

		// Token: 0x170026E5 RID: 9957
		// (get) Token: 0x06011E02 RID: 73218 RVA: 0x004FA968 File Offset: 0x004F8B68
		// (set) Token: 0x06011E03 RID: 73219 RVA: 0x004FA970 File Offset: 0x004F8B70
		public BattlePayProvider? Provider { get; set; }

		// Token: 0x02002989 RID: 10633
		public enum PurchaseState
		{
			// Token: 0x0400FD30 RID: 64816
			UNKNOWN = -1,
			// Token: 0x0400FD31 RID: 64817
			READY,
			// Token: 0x0400FD32 RID: 64818
			CHECK_RESULTS,
			// Token: 0x0400FD33 RID: 64819
			ERROR
		}
	}

	// Token: 0x02002081 RID: 8321
	public class BundleItem
	{
		// Token: 0x06011E04 RID: 73220 RVA: 0x004FA979 File Offset: 0x004F8B79
		public BundleItem()
		{
			this.ItemType = ProductType.PRODUCT_TYPE_UNKNOWN;
			this.ProductData = 0;
			this.Quantity = 0;
			this.BaseQuantity = 0;
			this.Attributes = new Dictionary<string, string>();
		}

		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x06011E05 RID: 73221 RVA: 0x004FA9A8 File Offset: 0x004F8BA8
		// (set) Token: 0x06011E06 RID: 73222 RVA: 0x004FA9B0 File Offset: 0x004F8BB0
		public ProductType ItemType { get; set; }

		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x06011E07 RID: 73223 RVA: 0x004FA9B9 File Offset: 0x004F8BB9
		// (set) Token: 0x06011E08 RID: 73224 RVA: 0x004FA9C1 File Offset: 0x004F8BC1
		public int ProductData { get; set; }

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x06011E09 RID: 73225 RVA: 0x004FA9CA File Offset: 0x004F8BCA
		// (set) Token: 0x06011E0A RID: 73226 RVA: 0x004FA9D2 File Offset: 0x004F8BD2
		public int Quantity { get; set; }

		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x06011E0B RID: 73227 RVA: 0x004FA9DB File Offset: 0x004F8BDB
		// (set) Token: 0x06011E0C RID: 73228 RVA: 0x004FA9E3 File Offset: 0x004F8BE3
		public int BaseQuantity { get; set; }

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x06011E0D RID: 73229 RVA: 0x004FA9EC File Offset: 0x004F8BEC
		// (set) Token: 0x06011E0E RID: 73230 RVA: 0x004FA9F4 File Offset: 0x004F8BF4
		public Dictionary<string, string> Attributes { get; set; }

		// Token: 0x06011E0F RID: 73231 RVA: 0x004FAA00 File Offset: 0x004F8C00
		public override bool Equals(object obj)
		{
			Network.BundleItem bundleItem = obj as Network.BundleItem;
			return bundleItem != null && bundleItem.ItemType == this.ItemType && bundleItem.ProductData == this.ProductData && bundleItem.BaseQuantity == this.BaseQuantity && bundleItem.Quantity == this.Quantity && bundleItem.Attributes == this.Attributes;
		}

		// Token: 0x06011E10 RID: 73232 RVA: 0x004FAA6C File Offset: 0x004F8C6C
		public override int GetHashCode()
		{
			return this.ItemType.GetHashCode() * this.ProductData.GetHashCode() * this.Quantity.GetHashCode();
		}
	}

	// Token: 0x02002082 RID: 8322
	public class Bundle
	{
		// Token: 0x06011E11 RID: 73233 RVA: 0x004FAAAB File Offset: 0x004F8CAB
		public Bundle()
		{
			this.AppleID = string.Empty;
			this.GooglePlayID = string.Empty;
			this.AmazonID = string.Empty;
			this.Items = new List<Network.BundleItem>();
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x06011E12 RID: 73234 RVA: 0x004FAADF File Offset: 0x004F8CDF
		// (set) Token: 0x06011E13 RID: 73235 RVA: 0x004FAAE7 File Offset: 0x004F8CE7
		public ulong? Cost { get; set; }

		// Token: 0x170026EC RID: 9964
		// (get) Token: 0x06011E14 RID: 73236 RVA: 0x004FAAF0 File Offset: 0x004F8CF0
		// (set) Token: 0x06011E15 RID: 73237 RVA: 0x004FAAF8 File Offset: 0x004F8CF8
		public double? CostDisplay { get; set; }

		// Token: 0x170026ED RID: 9965
		// (get) Token: 0x06011E16 RID: 73238 RVA: 0x004FAB01 File Offset: 0x004F8D01
		// (set) Token: 0x06011E17 RID: 73239 RVA: 0x004FAB09 File Offset: 0x004F8D09
		public long? GtappGoldCost { get; set; }

		// Token: 0x170026EE RID: 9966
		// (get) Token: 0x06011E18 RID: 73240 RVA: 0x004FAB12 File Offset: 0x004F8D12
		// (set) Token: 0x06011E19 RID: 73241 RVA: 0x004FAB1A File Offset: 0x004F8D1A
		public long? VirtualCurrencyCost { get; set; }

		// Token: 0x170026EF RID: 9967
		// (get) Token: 0x06011E1A RID: 73242 RVA: 0x004FAB23 File Offset: 0x004F8D23
		// (set) Token: 0x06011E1B RID: 73243 RVA: 0x004FAB2B File Offset: 0x004F8D2B
		public string VirtualCurrencyCode { get; set; }

		// Token: 0x170026F0 RID: 9968
		// (get) Token: 0x06011E1C RID: 73244 RVA: 0x004FAB34 File Offset: 0x004F8D34
		// (set) Token: 0x06011E1D RID: 73245 RVA: 0x004FAB3C File Offset: 0x004F8D3C
		public string AppleID { get; set; }

		// Token: 0x170026F1 RID: 9969
		// (get) Token: 0x06011E1E RID: 73246 RVA: 0x004FAB45 File Offset: 0x004F8D45
		// (set) Token: 0x06011E1F RID: 73247 RVA: 0x004FAB4D File Offset: 0x004F8D4D
		public string GooglePlayID { get; set; }

		// Token: 0x170026F2 RID: 9970
		// (get) Token: 0x06011E20 RID: 73248 RVA: 0x004FAB56 File Offset: 0x004F8D56
		// (set) Token: 0x06011E21 RID: 73249 RVA: 0x004FAB5E File Offset: 0x004F8D5E
		public string AmazonID { get; set; }

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x06011E22 RID: 73250 RVA: 0x004FAB67 File Offset: 0x004F8D67
		// (set) Token: 0x06011E23 RID: 73251 RVA: 0x004FAB6F File Offset: 0x004F8D6F
		public string OneStoreID { get; set; }

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x06011E24 RID: 73252 RVA: 0x004FAB78 File Offset: 0x004F8D78
		// (set) Token: 0x06011E25 RID: 73253 RVA: 0x004FAB80 File Offset: 0x004F8D80
		public List<Network.BundleItem> Items { get; set; }

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x06011E26 RID: 73254 RVA: 0x004FAB89 File Offset: 0x004F8D89
		// (set) Token: 0x06011E27 RID: 73255 RVA: 0x004FAB91 File Offset: 0x004F8D91
		public string ProductEvent { get; set; }

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x06011E28 RID: 73256 RVA: 0x004FAB9A File Offset: 0x004F8D9A
		// (set) Token: 0x06011E29 RID: 73257 RVA: 0x004FABA2 File Offset: 0x004F8DA2
		public List<BattlePayProvider> ExclusiveProviders { get; set; }

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x06011E2A RID: 73258 RVA: 0x004FABAB File Offset: 0x004F8DAB
		// (set) Token: 0x06011E2B RID: 73259 RVA: 0x004FABB3 File Offset: 0x004F8DB3
		public bool IsPrePurchase { get; set; }

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x06011E2C RID: 73260 RVA: 0x004FABBC File Offset: 0x004F8DBC
		// (set) Token: 0x06011E2D RID: 73261 RVA: 0x004FABC4 File Offset: 0x004F8DC4
		public long? PMTProductID { get; set; }

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x06011E2E RID: 73262 RVA: 0x004FABCD File Offset: 0x004F8DCD
		// (set) Token: 0x06011E2F RID: 73263 RVA: 0x004FABD5 File Offset: 0x004F8DD5
		public DbfLocValue DisplayName { get; set; }

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x06011E30 RID: 73264 RVA: 0x004FABDE File Offset: 0x004F8DDE
		// (set) Token: 0x06011E31 RID: 73265 RVA: 0x004FABE6 File Offset: 0x004F8DE6
		public DbfLocValue DisplayDescription { get; set; }

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x06011E32 RID: 73266 RVA: 0x004FABEF File Offset: 0x004F8DEF
		// (set) Token: 0x06011E33 RID: 73267 RVA: 0x004FABF7 File Offset: 0x004F8DF7
		public Dictionary<string, string> Attributes { get; set; }

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x06011E34 RID: 73268 RVA: 0x004FAC00 File Offset: 0x004F8E00
		// (set) Token: 0x06011E35 RID: 73269 RVA: 0x004FAC08 File Offset: 0x004F8E08
		public List<int> SaleIds { get; set; }

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x06011E36 RID: 73270 RVA: 0x004FAC11 File Offset: 0x004F8E11
		// (set) Token: 0x06011E37 RID: 73271 RVA: 0x004FAC19 File Offset: 0x004F8E19
		public bool VisibleOnSalePeriodOnly { get; set; }
	}

	// Token: 0x02002083 RID: 8323
	public class ShopSection
	{
		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x06011E38 RID: 73272 RVA: 0x004FAC22 File Offset: 0x004F8E22
		// (set) Token: 0x06011E39 RID: 73273 RVA: 0x004FAC2A File Offset: 0x004F8E2A
		public string InternalName { get; set; }

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x06011E3A RID: 73274 RVA: 0x004FAC33 File Offset: 0x004F8E33
		// (set) Token: 0x06011E3B RID: 73275 RVA: 0x004FAC3B File Offset: 0x004F8E3B
		public DbfLocValue Label { get; set; }

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x06011E3C RID: 73276 RVA: 0x004FAC44 File Offset: 0x004F8E44
		// (set) Token: 0x06011E3D RID: 73277 RVA: 0x004FAC4C File Offset: 0x004F8E4C
		public string Style { get; set; }

		// Token: 0x17002701 RID: 9985
		// (get) Token: 0x06011E3E RID: 73278 RVA: 0x004FAC55 File Offset: 0x004F8E55
		// (set) Token: 0x06011E3F RID: 73279 RVA: 0x004FAC5D File Offset: 0x004F8E5D
		public string FillerTags { get; set; }

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x06011E40 RID: 73280 RVA: 0x004FAC66 File Offset: 0x004F8E66
		// (set) Token: 0x06011E41 RID: 73281 RVA: 0x004FAC6E File Offset: 0x004F8E6E
		public int SortOrder { get; set; }

		// Token: 0x17002703 RID: 9987
		// (get) Token: 0x06011E42 RID: 73282 RVA: 0x004FAC77 File Offset: 0x004F8E77
		// (set) Token: 0x06011E43 RID: 73283 RVA: 0x004FAC7F File Offset: 0x004F8E7F
		public List<Network.ShopSection.ProductRef> Products { get; set; }

		// Token: 0x0200298A RID: 10634
		public class ProductRef
		{
			// Token: 0x17002D8A RID: 11658
			// (get) Token: 0x06013F07 RID: 81671 RVA: 0x00541703 File Offset: 0x0053F903
			// (set) Token: 0x06013F08 RID: 81672 RVA: 0x0054170B File Offset: 0x0053F90B
			public long PmtId { get; set; }

			// Token: 0x17002D8B RID: 11659
			// (get) Token: 0x06013F09 RID: 81673 RVA: 0x00541714 File Offset: 0x0053F914
			// (set) Token: 0x06013F0A RID: 81674 RVA: 0x0054171C File Offset: 0x0053F91C
			public int OrderId { get; set; }
		}
	}

	// Token: 0x02002084 RID: 8324
	public class ShopSale
	{
		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x06011E45 RID: 73285 RVA: 0x004FAC88 File Offset: 0x004F8E88
		// (set) Token: 0x06011E46 RID: 73286 RVA: 0x004FAC90 File Offset: 0x004F8E90
		public int SaleId { get; set; }

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x06011E47 RID: 73287 RVA: 0x004FAC99 File Offset: 0x004F8E99
		// (set) Token: 0x06011E48 RID: 73288 RVA: 0x004FACA1 File Offset: 0x004F8EA1
		public DateTime? StartUtc { get; set; }

		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x06011E49 RID: 73289 RVA: 0x004FACAA File Offset: 0x004F8EAA
		// (set) Token: 0x06011E4A RID: 73290 RVA: 0x004FACB2 File Offset: 0x004F8EB2
		public DateTime? SoftEndUtc { get; set; }

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x06011E4B RID: 73291 RVA: 0x004FACBB File Offset: 0x004F8EBB
		// (set) Token: 0x06011E4C RID: 73292 RVA: 0x004FACC3 File Offset: 0x004F8EC3
		public DateTime? HardEndUtc { get; set; }
	}

	// Token: 0x02002085 RID: 8325
	public class GoldCostBooster
	{
		// Token: 0x06011E4E RID: 73294 RVA: 0x004FACCC File Offset: 0x004F8ECC
		public GoldCostBooster()
		{
			this.Cost = null;
			this.ID = 0;
			this.BuyWithGoldEvent = SpecialEventType.UNKNOWN;
		}

		// Token: 0x17002708 RID: 9992
		// (get) Token: 0x06011E4F RID: 73295 RVA: 0x004FACFC File Offset: 0x004F8EFC
		// (set) Token: 0x06011E50 RID: 73296 RVA: 0x004FAD04 File Offset: 0x004F8F04
		public long? Cost { get; set; }

		// Token: 0x17002709 RID: 9993
		// (get) Token: 0x06011E51 RID: 73297 RVA: 0x004FAD0D File Offset: 0x004F8F0D
		// (set) Token: 0x06011E52 RID: 73298 RVA: 0x004FAD15 File Offset: 0x004F8F15
		public int ID { get; set; }

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x06011E53 RID: 73299 RVA: 0x004FAD1E File Offset: 0x004F8F1E
		// (set) Token: 0x06011E54 RID: 73300 RVA: 0x004FAD26 File Offset: 0x004F8F26
		public SpecialEventType BuyWithGoldEvent { get; set; }
	}

	// Token: 0x02002086 RID: 8326
	public class BattlePayConfig
	{
		// Token: 0x06011E55 RID: 73301 RVA: 0x004FAD30 File Offset: 0x004F8F30
		public BattlePayConfig()
		{
			this.Available = false;
			this.Currencies = new List<global::Currency>();
			this.Bundles = new List<Network.Bundle>();
			this.GoldCostBoosters = new List<Network.GoldCostBooster>();
			this.GoldCostArena = null;
			this.SecondsBeforeAutoCancel = StoreManager.DEFAULT_SECONDS_BEFORE_AUTO_CANCEL;
			this.CommerceClientID = "df5787f96b2b46c49c66dd45bcb05490";
			this.PersonalizedShopPageID = null;
			this.CatalogLocaleToGameLocale = new global::Map<long, Locale>();
			this.SaleList = new List<Network.ShopSale>();
			this.IgnoreProductTiming = false;
			this.CheckoutKrOnestoreKey = null;
		}

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06011E56 RID: 73302 RVA: 0x004FADBB File Offset: 0x004F8FBB
		// (set) Token: 0x06011E57 RID: 73303 RVA: 0x004FADC3 File Offset: 0x004F8FC3
		public bool Available { get; set; }

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06011E58 RID: 73304 RVA: 0x004FADCC File Offset: 0x004F8FCC
		// (set) Token: 0x06011E59 RID: 73305 RVA: 0x004FADD4 File Offset: 0x004F8FD4
		public global::Currency Currency { get; set; }

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x06011E5A RID: 73306 RVA: 0x004FADDD File Offset: 0x004F8FDD
		// (set) Token: 0x06011E5B RID: 73307 RVA: 0x004FADE5 File Offset: 0x004F8FE5
		public List<global::Currency> Currencies { get; set; }

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x06011E5C RID: 73308 RVA: 0x004FADEE File Offset: 0x004F8FEE
		// (set) Token: 0x06011E5D RID: 73309 RVA: 0x004FADF6 File Offset: 0x004F8FF6
		public List<Network.Bundle> Bundles { get; set; }

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x06011E5E RID: 73310 RVA: 0x004FADFF File Offset: 0x004F8FFF
		// (set) Token: 0x06011E5F RID: 73311 RVA: 0x004FAE07 File Offset: 0x004F9007
		public List<Network.GoldCostBooster> GoldCostBoosters { get; set; }

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x06011E60 RID: 73312 RVA: 0x004FAE10 File Offset: 0x004F9010
		// (set) Token: 0x06011E61 RID: 73313 RVA: 0x004FAE18 File Offset: 0x004F9018
		public long? GoldCostArena { get; set; }

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x06011E62 RID: 73314 RVA: 0x004FAE21 File Offset: 0x004F9021
		// (set) Token: 0x06011E63 RID: 73315 RVA: 0x004FAE29 File Offset: 0x004F9029
		public int SecondsBeforeAutoCancel { get; set; }

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06011E64 RID: 73316 RVA: 0x004FAE32 File Offset: 0x004F9032
		// (set) Token: 0x06011E65 RID: 73317 RVA: 0x004FAE3A File Offset: 0x004F903A
		public string CommerceClientID { get; set; }

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06011E66 RID: 73318 RVA: 0x004FAE43 File Offset: 0x004F9043
		// (set) Token: 0x06011E67 RID: 73319 RVA: 0x004FAE4B File Offset: 0x004F904B
		public string PersonalizedShopPageID { get; set; }

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06011E68 RID: 73320 RVA: 0x004FAE54 File Offset: 0x004F9054
		// (set) Token: 0x06011E69 RID: 73321 RVA: 0x004FAE5C File Offset: 0x004F905C
		public global::Map<long, Locale> CatalogLocaleToGameLocale { get; set; }

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06011E6A RID: 73322 RVA: 0x004FAE65 File Offset: 0x004F9065
		// (set) Token: 0x06011E6B RID: 73323 RVA: 0x004FAE6D File Offset: 0x004F906D
		public List<Network.ShopSale> SaleList { get; set; }

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x06011E6C RID: 73324 RVA: 0x004FAE76 File Offset: 0x004F9076
		// (set) Token: 0x06011E6D RID: 73325 RVA: 0x004FAE7E File Offset: 0x004F907E
		public bool IgnoreProductTiming { get; set; }

		// Token: 0x17002717 RID: 10007
		// (get) Token: 0x06011E6E RID: 73326 RVA: 0x004FAE87 File Offset: 0x004F9087
		// (set) Token: 0x06011E6F RID: 73327 RVA: 0x004FAE8F File Offset: 0x004F908F
		public string CheckoutKrOnestoreKey { get; set; }
	}

	// Token: 0x02002087 RID: 8327
	public class PurchaseViaGoldResponse
	{
		// Token: 0x06011E70 RID: 73328 RVA: 0x004FAE98 File Offset: 0x004F9098
		public PurchaseViaGoldResponse()
		{
			this.Error = Network.PurchaseViaGoldResponse.ErrorType.UNKNOWN;
			this.GoldUsed = 0L;
		}

		// Token: 0x17002718 RID: 10008
		// (get) Token: 0x06011E71 RID: 73329 RVA: 0x004FAEAF File Offset: 0x004F90AF
		// (set) Token: 0x06011E72 RID: 73330 RVA: 0x004FAEB7 File Offset: 0x004F90B7
		public Network.PurchaseViaGoldResponse.ErrorType Error { get; set; }

		// Token: 0x17002719 RID: 10009
		// (get) Token: 0x06011E73 RID: 73331 RVA: 0x004FAEC0 File Offset: 0x004F90C0
		// (set) Token: 0x06011E74 RID: 73332 RVA: 0x004FAEC8 File Offset: 0x004F90C8
		public long GoldUsed { get; set; }

		// Token: 0x0200298B RID: 10635
		public enum ErrorType
		{
			// Token: 0x0400FD37 RID: 64823
			UNKNOWN = -1,
			// Token: 0x0400FD38 RID: 64824
			SUCCESS = 1,
			// Token: 0x0400FD39 RID: 64825
			INSUFFICIENT_GOLD,
			// Token: 0x0400FD3A RID: 64826
			PRODUCT_NA,
			// Token: 0x0400FD3B RID: 64827
			FEATURE_NA,
			// Token: 0x0400FD3C RID: 64828
			INVALID_QUANTITY
		}
	}

	// Token: 0x02002088 RID: 8328
	public class PurchaseMethod
	{
		// Token: 0x06011E75 RID: 73333 RVA: 0x004FAED4 File Offset: 0x004F90D4
		public PurchaseMethod()
		{
			this.TransactionID = 0L;
			this.PMTProductID = null;
			this.Quantity = 0;
			this.CurrencyCode = string.Empty;
			this.WalletName = string.Empty;
			this.UseEBalance = false;
			this.IsZeroCostLicense = false;
			this.ChallengeID = string.Empty;
			this.ChallengeURL = string.Empty;
			this.PurchaseError = null;
		}

		// Token: 0x1700271A RID: 10010
		// (get) Token: 0x06011E76 RID: 73334 RVA: 0x004FAF46 File Offset: 0x004F9146
		// (set) Token: 0x06011E77 RID: 73335 RVA: 0x004FAF4E File Offset: 0x004F914E
		public long TransactionID { get; set; }

		// Token: 0x1700271B RID: 10011
		// (get) Token: 0x06011E78 RID: 73336 RVA: 0x004FAF57 File Offset: 0x004F9157
		// (set) Token: 0x06011E79 RID: 73337 RVA: 0x004FAF5F File Offset: 0x004F915F
		public long? PMTProductID { get; set; }

		// Token: 0x1700271C RID: 10012
		// (get) Token: 0x06011E7A RID: 73338 RVA: 0x004FAF68 File Offset: 0x004F9168
		// (set) Token: 0x06011E7B RID: 73339 RVA: 0x004FAF70 File Offset: 0x004F9170
		public int Quantity { get; set; }

		// Token: 0x1700271D RID: 10013
		// (get) Token: 0x06011E7C RID: 73340 RVA: 0x004FAF79 File Offset: 0x004F9179
		// (set) Token: 0x06011E7D RID: 73341 RVA: 0x004FAF81 File Offset: 0x004F9181
		public string CurrencyCode { get; set; }

		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x06011E7E RID: 73342 RVA: 0x004FAF8A File Offset: 0x004F918A
		// (set) Token: 0x06011E7F RID: 73343 RVA: 0x004FAF92 File Offset: 0x004F9192
		public string WalletName { get; set; }

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x06011E80 RID: 73344 RVA: 0x004FAF9B File Offset: 0x004F919B
		// (set) Token: 0x06011E81 RID: 73345 RVA: 0x004FAFA3 File Offset: 0x004F91A3
		public bool UseEBalance { get; set; }

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x06011E82 RID: 73346 RVA: 0x004FAFAC File Offset: 0x004F91AC
		// (set) Token: 0x06011E83 RID: 73347 RVA: 0x004FAFB4 File Offset: 0x004F91B4
		public bool IsZeroCostLicense { get; set; }

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x06011E84 RID: 73348 RVA: 0x004FAFBD File Offset: 0x004F91BD
		// (set) Token: 0x06011E85 RID: 73349 RVA: 0x004FAFC5 File Offset: 0x004F91C5
		public string ChallengeID { get; set; }

		// Token: 0x17002722 RID: 10018
		// (get) Token: 0x06011E86 RID: 73350 RVA: 0x004FAFCE File Offset: 0x004F91CE
		// (set) Token: 0x06011E87 RID: 73351 RVA: 0x004FAFD6 File Offset: 0x004F91D6
		public string ChallengeURL { get; set; }

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x06011E88 RID: 73352 RVA: 0x004FAFDF File Offset: 0x004F91DF
		// (set) Token: 0x06011E89 RID: 73353 RVA: 0x004FAFE7 File Offset: 0x004F91E7
		public Network.PurchaseErrorInfo PurchaseError { get; set; }
	}

	// Token: 0x02002089 RID: 8329
	public class PurchaseResponse
	{
		// Token: 0x06011E8A RID: 73354 RVA: 0x004FAFF0 File Offset: 0x004F91F0
		public PurchaseResponse()
		{
			this.PurchaseError = new Network.PurchaseErrorInfo();
			this.TransactionID = 0L;
			this.PMTProductID = null;
			this.ThirdPartyID = string.Empty;
		}

		// Token: 0x17002724 RID: 10020
		// (get) Token: 0x06011E8B RID: 73355 RVA: 0x004FB030 File Offset: 0x004F9230
		// (set) Token: 0x06011E8C RID: 73356 RVA: 0x004FB038 File Offset: 0x004F9238
		public Network.PurchaseErrorInfo PurchaseError { get; set; }

		// Token: 0x17002725 RID: 10021
		// (get) Token: 0x06011E8D RID: 73357 RVA: 0x004FB041 File Offset: 0x004F9241
		// (set) Token: 0x06011E8E RID: 73358 RVA: 0x004FB049 File Offset: 0x004F9249
		public long TransactionID { get; set; }

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x06011E8F RID: 73359 RVA: 0x004FB052 File Offset: 0x004F9252
		// (set) Token: 0x06011E90 RID: 73360 RVA: 0x004FB05A File Offset: 0x004F925A
		public long? PMTProductID { get; set; }

		// Token: 0x17002727 RID: 10023
		// (get) Token: 0x06011E91 RID: 73361 RVA: 0x004FB063 File Offset: 0x004F9263
		// (set) Token: 0x06011E92 RID: 73362 RVA: 0x004FB06B File Offset: 0x004F926B
		public string ThirdPartyID { get; set; }

		// Token: 0x17002728 RID: 10024
		// (get) Token: 0x06011E93 RID: 73363 RVA: 0x004FB074 File Offset: 0x004F9274
		// (set) Token: 0x06011E94 RID: 73364 RVA: 0x004FB07C File Offset: 0x004F927C
		public string CurrencyCode { get; set; }
	}

	// Token: 0x0200208A RID: 8330
	public class ThirdPartyPurchaseStatusResponse
	{
		// Token: 0x06011E95 RID: 73365 RVA: 0x004FB085 File Offset: 0x004F9285
		public ThirdPartyPurchaseStatusResponse()
		{
			this.ThirdPartyID = string.Empty;
			this.Status = Network.ThirdPartyPurchaseStatusResponse.PurchaseStatus.UNKNOWN;
		}

		// Token: 0x17002729 RID: 10025
		// (get) Token: 0x06011E96 RID: 73366 RVA: 0x004FB09F File Offset: 0x004F929F
		// (set) Token: 0x06011E97 RID: 73367 RVA: 0x004FB0A7 File Offset: 0x004F92A7
		public string ThirdPartyID { get; set; }

		// Token: 0x1700272A RID: 10026
		// (get) Token: 0x06011E98 RID: 73368 RVA: 0x004FB0B0 File Offset: 0x004F92B0
		// (set) Token: 0x06011E99 RID: 73369 RVA: 0x004FB0B8 File Offset: 0x004F92B8
		public Network.ThirdPartyPurchaseStatusResponse.PurchaseStatus Status { get; set; }

		// Token: 0x0200298C RID: 10636
		public enum PurchaseStatus
		{
			// Token: 0x0400FD3E RID: 64830
			UNKNOWN = -1,
			// Token: 0x0400FD3F RID: 64831
			NOT_FOUND = 1,
			// Token: 0x0400FD40 RID: 64832
			SUCCEEDED,
			// Token: 0x0400FD41 RID: 64833
			FAILED,
			// Token: 0x0400FD42 RID: 64834
			IN_PROGRESS
		}
	}

	// Token: 0x0200208B RID: 8331
	public class CardBackResponse
	{
		// Token: 0x06011E9A RID: 73370 RVA: 0x004FB0C1 File Offset: 0x004F92C1
		public CardBackResponse()
		{
			this.Success = false;
			this.CardBack = 0;
		}

		// Token: 0x1700272B RID: 10027
		// (get) Token: 0x06011E9B RID: 73371 RVA: 0x004FB0D7 File Offset: 0x004F92D7
		// (set) Token: 0x06011E9C RID: 73372 RVA: 0x004FB0DF File Offset: 0x004F92DF
		public bool Success { get; set; }

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x06011E9D RID: 73373 RVA: 0x004FB0E8 File Offset: 0x004F92E8
		// (set) Token: 0x06011E9E RID: 73374 RVA: 0x004FB0F0 File Offset: 0x004F92F0
		public int CardBack { get; set; }
	}

	// Token: 0x0200208C RID: 8332
	public class CoinResponse
	{
		// Token: 0x06011E9F RID: 73375 RVA: 0x004FB0F9 File Offset: 0x004F92F9
		public CoinResponse()
		{
			this.Success = false;
			this.Coin = 1;
		}

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x06011EA0 RID: 73376 RVA: 0x004FB10F File Offset: 0x004F930F
		// (set) Token: 0x06011EA1 RID: 73377 RVA: 0x004FB117 File Offset: 0x004F9317
		public bool Success { get; set; }

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x06011EA2 RID: 73378 RVA: 0x004FB120 File Offset: 0x004F9320
		// (set) Token: 0x06011EA3 RID: 73379 RVA: 0x004FB128 File Offset: 0x004F9328
		public int Coin { get; set; }
	}

	// Token: 0x0200208D RID: 8333
	public class GameCancelInfo
	{
		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x06011EA4 RID: 73380 RVA: 0x004FB131 File Offset: 0x004F9331
		// (set) Token: 0x06011EA5 RID: 73381 RVA: 0x004FB139 File Offset: 0x004F9339
		public Network.GameCancelInfo.Reason CancelReason { get; set; }

		// Token: 0x0200298D RID: 10637
		public enum Reason
		{
			// Token: 0x0400FD44 RID: 64836
			OPPONENT_TIMEOUT = 1,
			// Token: 0x0400FD45 RID: 64837
			PLAYER_LOADING_TIMEOUT,
			// Token: 0x0400FD46 RID: 64838
			PLAYER_LOADING_DISCONNECTED
		}
	}

	// Token: 0x0200208E RID: 8334
	public class Entity
	{
		// Token: 0x06011EA7 RID: 73383 RVA: 0x004FB142 File Offset: 0x004F9342
		public Entity()
		{
			this.Tags = new List<Network.Entity.Tag>();
			this.DefTags = new List<Network.Entity.Tag>();
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x06011EA8 RID: 73384 RVA: 0x004FB160 File Offset: 0x004F9360
		// (set) Token: 0x06011EA9 RID: 73385 RVA: 0x004FB168 File Offset: 0x004F9368
		public int ID { get; set; }

		// Token: 0x17002731 RID: 10033
		// (get) Token: 0x06011EAA RID: 73386 RVA: 0x004FB171 File Offset: 0x004F9371
		// (set) Token: 0x06011EAB RID: 73387 RVA: 0x004FB179 File Offset: 0x004F9379
		public List<Network.Entity.Tag> Tags { get; set; }

		// Token: 0x17002732 RID: 10034
		// (get) Token: 0x06011EAC RID: 73388 RVA: 0x004FB182 File Offset: 0x004F9382
		// (set) Token: 0x06011EAD RID: 73389 RVA: 0x004FB18A File Offset: 0x004F938A
		public List<Network.Entity.Tag> DefTags { get; set; }

		// Token: 0x17002733 RID: 10035
		// (get) Token: 0x06011EAE RID: 73390 RVA: 0x004FB193 File Offset: 0x004F9393
		// (set) Token: 0x06011EAF RID: 73391 RVA: 0x004FB19B File Offset: 0x004F939B
		public string CardID { get; set; }

		// Token: 0x06011EB0 RID: 73392 RVA: 0x004FB1A4 File Offset: 0x004F93A4
		public static Network.Entity CreateFromProto(PegasusGame.Entity src)
		{
			return new Network.Entity
			{
				ID = src.Id,
				CardID = string.Empty,
				Tags = Network.Entity.CreateTagsFromProto(src.Tags)
			};
		}

		// Token: 0x06011EB1 RID: 73393 RVA: 0x004FB1D4 File Offset: 0x004F93D4
		public static Network.Entity CreateFromProto(PowerHistoryEntity src)
		{
			return new Network.Entity
			{
				ID = src.Entity,
				CardID = src.Name,
				Tags = Network.Entity.CreateTagsFromProto(src.Tags),
				DefTags = Network.Entity.CreateTagsFromProto(src.DefTags)
			};
		}

		// Token: 0x06011EB2 RID: 73394 RVA: 0x004FB220 File Offset: 0x004F9420
		public static List<Network.Entity.Tag> CreateTagsFromProto(IList<PegasusGame.Tag> tagList)
		{
			List<Network.Entity.Tag> list = new List<Network.Entity.Tag>();
			for (int i = 0; i < tagList.Count; i++)
			{
				PegasusGame.Tag tag = tagList[i];
				list.Add(new Network.Entity.Tag
				{
					Name = tag.Name,
					Value = tag.Value
				});
			}
			return list;
		}

		// Token: 0x06011EB3 RID: 73395 RVA: 0x004FB270 File Offset: 0x004F9470
		public override string ToString()
		{
			return string.Format("id={0} cardId={1} tags={2}", this.ID, this.CardID, this.Tags.Count);
		}

		// Token: 0x0200298E RID: 10638
		public class Tag
		{
			// Token: 0x17002D8C RID: 11660
			// (get) Token: 0x06013F0C RID: 81676 RVA: 0x00541725 File Offset: 0x0053F925
			// (set) Token: 0x06013F0D RID: 81677 RVA: 0x0054172D File Offset: 0x0053F92D
			public int Name { get; set; }

			// Token: 0x17002D8D RID: 11661
			// (get) Token: 0x06013F0E RID: 81678 RVA: 0x00541736 File Offset: 0x0053F936
			// (set) Token: 0x06013F0F RID: 81679 RVA: 0x0054173E File Offset: 0x0053F93E
			public int Value { get; set; }
		}
	}

	// Token: 0x0200208F RID: 8335
	public class Options
	{
		// Token: 0x06011EB4 RID: 73396 RVA: 0x004FB29D File Offset: 0x004F949D
		public Options()
		{
			this.List = new List<Network.Options.Option>();
		}

		// Token: 0x17002734 RID: 10036
		// (get) Token: 0x06011EB5 RID: 73397 RVA: 0x004FB2B0 File Offset: 0x004F94B0
		// (set) Token: 0x06011EB6 RID: 73398 RVA: 0x004FB2B8 File Offset: 0x004F94B8
		public int ID { get; set; }

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x06011EB7 RID: 73399 RVA: 0x004FB2C1 File Offset: 0x004F94C1
		// (set) Token: 0x06011EB8 RID: 73400 RVA: 0x004FB2C9 File Offset: 0x004F94C9
		public List<Network.Options.Option> List { get; set; }

		// Token: 0x06011EB9 RID: 73401 RVA: 0x004FB2D4 File Offset: 0x004F94D4
		public bool HasValidOption()
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				if (this.List[i].Main.PlayErrorInfo.IsValid())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06011EBA RID: 73402 RVA: 0x004FB318 File Offset: 0x004F9518
		public Network.Options.Option GetOptionFromEntityID(int entityID)
		{
			for (int i = 0; i < this.List.Count; i++)
			{
				if (this.List[i].Main.ID == entityID)
				{
					return this.List[i];
				}
			}
			return null;
		}

		// Token: 0x06011EBB RID: 73403 RVA: 0x004FB364 File Offset: 0x004F9564
		public void CopyFrom(Network.Options options)
		{
			this.ID = options.ID;
			if (options.List == null)
			{
				this.List = null;
				return;
			}
			if (this.List != null)
			{
				this.List.Clear();
			}
			else
			{
				this.List = new List<Network.Options.Option>();
			}
			for (int i = 0; i < options.List.Count; i++)
			{
				Network.Options.Option option = new Network.Options.Option();
				option.CopyFrom(options.List[i]);
				this.List.Add(option);
			}
		}

		// Token: 0x0200298F RID: 10639
		public class Option
		{
			// Token: 0x06013F11 RID: 81681 RVA: 0x00541747 File Offset: 0x0053F947
			public Option()
			{
				this.Main = new Network.Options.Option.SubOption();
				this.Subs = new List<Network.Options.Option.SubOption>();
			}

			// Token: 0x17002D8E RID: 11662
			// (get) Token: 0x06013F12 RID: 81682 RVA: 0x00541765 File Offset: 0x0053F965
			// (set) Token: 0x06013F13 RID: 81683 RVA: 0x0054176D File Offset: 0x0053F96D
			public Network.Options.Option.OptionType Type { get; set; }

			// Token: 0x17002D8F RID: 11663
			// (get) Token: 0x06013F14 RID: 81684 RVA: 0x00541776 File Offset: 0x0053F976
			// (set) Token: 0x06013F15 RID: 81685 RVA: 0x0054177E File Offset: 0x0053F97E
			public Network.Options.Option.SubOption Main { get; set; }

			// Token: 0x17002D90 RID: 11664
			// (get) Token: 0x06013F16 RID: 81686 RVA: 0x00541787 File Offset: 0x0053F987
			// (set) Token: 0x06013F17 RID: 81687 RVA: 0x0054178F File Offset: 0x0053F98F
			public List<Network.Options.Option.SubOption> Subs { get; set; }

			// Token: 0x06013F18 RID: 81688 RVA: 0x00541798 File Offset: 0x0053F998
			public Network.Options.Option.SubOption GetSubOptionFromEntityID(int entityID)
			{
				for (int i = 0; i < this.Subs.Count; i++)
				{
					if (this.Subs[i].ID == entityID)
					{
						return this.Subs[i];
					}
				}
				return null;
			}

			// Token: 0x06013F19 RID: 81689 RVA: 0x005417E0 File Offset: 0x0053F9E0
			public bool HasValidSubOption()
			{
				for (int i = 0; i < this.Subs.Count; i++)
				{
					if (this.Subs[i].PlayErrorInfo.IsValid())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06013F1A RID: 81690 RVA: 0x00541820 File Offset: 0x0053FA20
			public void CopyFrom(Network.Options.Option option)
			{
				this.Type = option.Type;
				if (this.Main == null)
				{
					this.Main = new Network.Options.Option.SubOption();
				}
				this.Main.CopyFrom(option.Main);
				if (option.Subs == null)
				{
					this.Subs = null;
					return;
				}
				if (this.Subs == null)
				{
					this.Subs = new List<Network.Options.Option.SubOption>();
				}
				else
				{
					this.Subs.Clear();
				}
				for (int i = 0; i < option.Subs.Count; i++)
				{
					Network.Options.Option.SubOption subOption = new Network.Options.Option.SubOption();
					subOption.CopyFrom(option.Subs[i]);
					this.Subs.Add(subOption);
				}
			}

			// Token: 0x020029B8 RID: 10680
			public enum OptionType
			{
				// Token: 0x0400FE33 RID: 65075
				PASS = 1,
				// Token: 0x0400FE34 RID: 65076
				END_TURN,
				// Token: 0x0400FE35 RID: 65077
				POWER
			}

			// Token: 0x020029B9 RID: 10681
			public class PlayErrorInfo
			{
				// Token: 0x06013FD2 RID: 81874 RVA: 0x005422C8 File Offset: 0x005404C8
				public PlayErrorInfo()
				{
					this.PlayError = PlayErrors.ErrorType.INVALID;
					this.PlayErrorParam = null;
				}

				// Token: 0x17002DD0 RID: 11728
				// (get) Token: 0x06013FD3 RID: 81875 RVA: 0x005422F1 File Offset: 0x005404F1
				// (set) Token: 0x06013FD4 RID: 81876 RVA: 0x005422F9 File Offset: 0x005404F9
				public PlayErrors.ErrorType PlayError { get; set; }

				// Token: 0x17002DD1 RID: 11729
				// (get) Token: 0x06013FD5 RID: 81877 RVA: 0x00542302 File Offset: 0x00540502
				// (set) Token: 0x06013FD6 RID: 81878 RVA: 0x0054230A File Offset: 0x0054050A
				public int? PlayErrorParam { get; set; }

				// Token: 0x06013FD7 RID: 81879 RVA: 0x00542313 File Offset: 0x00540513
				public bool IsValid()
				{
					return this.PlayError == PlayErrors.ErrorType.NONE;
				}
			}

			// Token: 0x020029BA RID: 10682
			public class TargetOption
			{
				// Token: 0x06013FD8 RID: 81880 RVA: 0x0054231E File Offset: 0x0054051E
				public TargetOption()
				{
					this.ID = 0;
					this.PlayErrorInfo = new Network.Options.Option.PlayErrorInfo();
				}

				// Token: 0x17002DD2 RID: 11730
				// (get) Token: 0x06013FD9 RID: 81881 RVA: 0x00542338 File Offset: 0x00540538
				// (set) Token: 0x06013FDA RID: 81882 RVA: 0x00542340 File Offset: 0x00540540
				public int ID { get; set; }

				// Token: 0x17002DD3 RID: 11731
				// (get) Token: 0x06013FDB RID: 81883 RVA: 0x00542349 File Offset: 0x00540549
				// (set) Token: 0x06013FDC RID: 81884 RVA: 0x00542351 File Offset: 0x00540551
				public Network.Options.Option.PlayErrorInfo PlayErrorInfo { get; set; }

				// Token: 0x06013FDD RID: 81885 RVA: 0x0054235A File Offset: 0x0054055A
				public void CopyFrom(Network.Options.Option.TargetOption targetOption)
				{
					this.ID = targetOption.ID;
					this.PlayErrorInfo = targetOption.PlayErrorInfo;
				}

				// Token: 0x06013FDE RID: 81886 RVA: 0x00542374 File Offset: 0x00540574
				public void CopyFrom(PegasusGame.TargetOption targetOption)
				{
					this.ID = targetOption.Id;
					this.PlayErrorInfo.PlayError = (PlayErrors.ErrorType)targetOption.PlayError;
					this.PlayErrorInfo.PlayErrorParam = (targetOption.HasPlayErrorParam ? new int?(targetOption.PlayErrorParam) : null);
				}
			}

			// Token: 0x020029BB RID: 10683
			public class SubOption
			{
				// Token: 0x06013FDF RID: 81887 RVA: 0x005423C7 File Offset: 0x005405C7
				public SubOption()
				{
					this.ID = 0;
					this.PlayErrorInfo = new Network.Options.Option.PlayErrorInfo();
				}

				// Token: 0x17002DD4 RID: 11732
				// (get) Token: 0x06013FE0 RID: 81888 RVA: 0x005423E1 File Offset: 0x005405E1
				// (set) Token: 0x06013FE1 RID: 81889 RVA: 0x005423E9 File Offset: 0x005405E9
				public int ID { get; set; }

				// Token: 0x17002DD5 RID: 11733
				// (get) Token: 0x06013FE2 RID: 81890 RVA: 0x005423F2 File Offset: 0x005405F2
				// (set) Token: 0x06013FE3 RID: 81891 RVA: 0x005423FA File Offset: 0x005405FA
				public List<Network.Options.Option.TargetOption> Targets { get; set; }

				// Token: 0x17002DD6 RID: 11734
				// (get) Token: 0x06013FE4 RID: 81892 RVA: 0x00542403 File Offset: 0x00540603
				// (set) Token: 0x06013FE5 RID: 81893 RVA: 0x0054240B File Offset: 0x0054060B
				public Network.Options.Option.PlayErrorInfo PlayErrorInfo { get; set; }

				// Token: 0x06013FE6 RID: 81894 RVA: 0x00542414 File Offset: 0x00540614
				public bool IsValidTarget(int entityID)
				{
					if (this.Targets == null)
					{
						return false;
					}
					for (int i = 0; i < this.Targets.Count; i++)
					{
						if (this.Targets[i].ID == entityID && this.Targets[i].PlayErrorInfo.IsValid())
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06013FE7 RID: 81895 RVA: 0x00542470 File Offset: 0x00540670
				public PlayErrors.ErrorType GetErrorForTarget(int entityID)
				{
					if (this.Targets == null)
					{
						return PlayErrors.ErrorType.INVALID;
					}
					for (int i = 0; i < this.Targets.Count; i++)
					{
						if (this.Targets[i].ID == entityID)
						{
							return this.Targets[i].PlayErrorInfo.PlayError;
						}
					}
					return PlayErrors.ErrorType.INVALID;
				}

				// Token: 0x06013FE8 RID: 81896 RVA: 0x005424CC File Offset: 0x005406CC
				public int? GetErrorParamForTarget(int entityID)
				{
					if (this.Targets == null)
					{
						return null;
					}
					for (int i = 0; i < this.Targets.Count; i++)
					{
						if (this.Targets[i].ID == entityID)
						{
							return this.Targets[i].PlayErrorInfo.PlayErrorParam;
						}
					}
					return null;
				}

				// Token: 0x06013FE9 RID: 81897 RVA: 0x00542538 File Offset: 0x00540738
				public bool HasValidTarget()
				{
					if (this.Targets == null)
					{
						return false;
					}
					for (int i = 0; i < this.Targets.Count; i++)
					{
						if (this.Targets[i].PlayErrorInfo.IsValid())
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06013FEA RID: 81898 RVA: 0x00542580 File Offset: 0x00540780
				public void CopyFrom(Network.Options.Option.SubOption subOption)
				{
					this.ID = subOption.ID;
					this.PlayErrorInfo = subOption.PlayErrorInfo;
					if (subOption.Targets == null)
					{
						this.Targets = null;
						return;
					}
					if (this.Targets == null)
					{
						this.Targets = new List<Network.Options.Option.TargetOption>();
					}
					else
					{
						this.Targets.Clear();
					}
					for (int i = 0; i < subOption.Targets.Count; i++)
					{
						Network.Options.Option.TargetOption targetOption = new Network.Options.Option.TargetOption();
						targetOption.CopyFrom(subOption.Targets[i]);
						this.Targets.Add(targetOption);
					}
				}
			}
		}
	}

	// Token: 0x02002090 RID: 8336
	public class EntityChoices
	{
		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x06011EBC RID: 73404 RVA: 0x004FB3E7 File Offset: 0x004F95E7
		// (set) Token: 0x06011EBD RID: 73405 RVA: 0x004FB3EF File Offset: 0x004F95EF
		public int ID { get; set; }

		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x06011EBE RID: 73406 RVA: 0x004FB3F8 File Offset: 0x004F95F8
		// (set) Token: 0x06011EBF RID: 73407 RVA: 0x004FB400 File Offset: 0x004F9600
		public CHOICE_TYPE ChoiceType { get; set; }

		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x06011EC0 RID: 73408 RVA: 0x004FB409 File Offset: 0x004F9609
		// (set) Token: 0x06011EC1 RID: 73409 RVA: 0x004FB411 File Offset: 0x004F9611
		public int CountMin { get; set; }

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x06011EC2 RID: 73410 RVA: 0x004FB41A File Offset: 0x004F961A
		// (set) Token: 0x06011EC3 RID: 73411 RVA: 0x004FB422 File Offset: 0x004F9622
		public int CountMax { get; set; }

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x06011EC4 RID: 73412 RVA: 0x004FB42B File Offset: 0x004F962B
		// (set) Token: 0x06011EC5 RID: 73413 RVA: 0x004FB433 File Offset: 0x004F9633
		public List<int> Entities { get; set; }

		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x06011EC6 RID: 73414 RVA: 0x004FB43C File Offset: 0x004F963C
		// (set) Token: 0x06011EC7 RID: 73415 RVA: 0x004FB444 File Offset: 0x004F9644
		public int Source { get; set; }

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x06011EC8 RID: 73416 RVA: 0x004FB44D File Offset: 0x004F964D
		// (set) Token: 0x06011EC9 RID: 73417 RVA: 0x004FB455 File Offset: 0x004F9655
		public int PlayerId { get; set; }

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x06011ECA RID: 73418 RVA: 0x004FB45E File Offset: 0x004F965E
		// (set) Token: 0x06011ECB RID: 73419 RVA: 0x004FB466 File Offset: 0x004F9666
		public bool HideChosen { get; set; }

		// Token: 0x06011ECC RID: 73420 RVA: 0x004FB46F File Offset: 0x004F966F
		public bool IsSingleChoice()
		{
			return this.CountMax == 0 || (this.CountMin == 1 && this.CountMax == 1);
		}
	}

	// Token: 0x02002091 RID: 8337
	public class EntitiesChosen
	{
		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x06011ECE RID: 73422 RVA: 0x004FB48F File Offset: 0x004F968F
		// (set) Token: 0x06011ECF RID: 73423 RVA: 0x004FB497 File Offset: 0x004F9697
		public int ID { get; set; }

		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x06011ED0 RID: 73424 RVA: 0x004FB4A0 File Offset: 0x004F96A0
		// (set) Token: 0x06011ED1 RID: 73425 RVA: 0x004FB4A8 File Offset: 0x004F96A8
		public List<int> Entities { get; set; }

		// Token: 0x17002740 RID: 10048
		// (get) Token: 0x06011ED2 RID: 73426 RVA: 0x004FB4B1 File Offset: 0x004F96B1
		// (set) Token: 0x06011ED3 RID: 73427 RVA: 0x004FB4B9 File Offset: 0x004F96B9
		public int PlayerId { get; set; }

		// Token: 0x17002741 RID: 10049
		// (get) Token: 0x06011ED4 RID: 73428 RVA: 0x004FB4C2 File Offset: 0x004F96C2
		// (set) Token: 0x06011ED5 RID: 73429 RVA: 0x004FB4CA File Offset: 0x004F96CA
		public CHOICE_TYPE ChoiceType { get; set; }
	}

	// Token: 0x02002092 RID: 8338
	public class Notification
	{
		// Token: 0x17002742 RID: 10050
		// (get) Token: 0x06011ED7 RID: 73431 RVA: 0x004FB4D3 File Offset: 0x004F96D3
		// (set) Token: 0x06011ED8 RID: 73432 RVA: 0x004FB4DB File Offset: 0x004F96DB
		public Network.Notification.Type NotificationType { get; set; }

		// Token: 0x02002990 RID: 10640
		public enum Type
		{
			// Token: 0x0400FD4D RID: 64845
			IN_HAND_CARD_CAP = 1,
			// Token: 0x0400FD4E RID: 64846
			MANA_CAP
		}
	}

	// Token: 0x02002093 RID: 8339
	public class GameSetup
	{
		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x06011EDA RID: 73434 RVA: 0x004FB4E4 File Offset: 0x004F96E4
		// (set) Token: 0x06011EDB RID: 73435 RVA: 0x004FB4EC File Offset: 0x004F96EC
		public int Board { get; set; }

		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x06011EDC RID: 73436 RVA: 0x004FB4F5 File Offset: 0x004F96F5
		// (set) Token: 0x06011EDD RID: 73437 RVA: 0x004FB4FD File Offset: 0x004F96FD
		public int MaxSecretZoneSizePerPlayer { get; set; }

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x06011EDE RID: 73438 RVA: 0x004FB506 File Offset: 0x004F9706
		// (set) Token: 0x06011EDF RID: 73439 RVA: 0x004FB50E File Offset: 0x004F970E
		public int MaxSecretsPerPlayer { get; set; }

		// Token: 0x17002746 RID: 10054
		// (get) Token: 0x06011EE0 RID: 73440 RVA: 0x004FB517 File Offset: 0x004F9717
		// (set) Token: 0x06011EE1 RID: 73441 RVA: 0x004FB51F File Offset: 0x004F971F
		public int MaxQuestsPerPlayer { get; set; }

		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x06011EE2 RID: 73442 RVA: 0x004FB528 File Offset: 0x004F9728
		// (set) Token: 0x06011EE3 RID: 73443 RVA: 0x004FB530 File Offset: 0x004F9730
		public int MaxFriendlyMinionsPerPlayer { get; set; }

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x06011EE4 RID: 73444 RVA: 0x004FB539 File Offset: 0x004F9739
		// (set) Token: 0x06011EE5 RID: 73445 RVA: 0x004FB541 File Offset: 0x004F9741
		public uint DisconnectWhenStuckSeconds { get; set; }
	}

	// Token: 0x02002094 RID: 8340
	public class UserUI
	{
		// Token: 0x0400DDBF RID: 56767
		public Network.UserUI.MouseInfo mouseInfo;

		// Token: 0x0400DDC0 RID: 56768
		public Network.UserUI.EmoteInfo emoteInfo;

		// Token: 0x0400DDC1 RID: 56769
		public int? playerId;

		// Token: 0x02002991 RID: 10641
		public class MouseInfo
		{
			// Token: 0x17002D91 RID: 11665
			// (get) Token: 0x06013F1B RID: 81691 RVA: 0x005418C7 File Offset: 0x0053FAC7
			// (set) Token: 0x06013F1C RID: 81692 RVA: 0x005418CF File Offset: 0x0053FACF
			public int OverCardID { get; set; }

			// Token: 0x17002D92 RID: 11666
			// (get) Token: 0x06013F1D RID: 81693 RVA: 0x005418D8 File Offset: 0x0053FAD8
			// (set) Token: 0x06013F1E RID: 81694 RVA: 0x005418E0 File Offset: 0x0053FAE0
			public int HeldCardID { get; set; }

			// Token: 0x17002D93 RID: 11667
			// (get) Token: 0x06013F1F RID: 81695 RVA: 0x005418E9 File Offset: 0x0053FAE9
			// (set) Token: 0x06013F20 RID: 81696 RVA: 0x005418F1 File Offset: 0x0053FAF1
			public int ArrowOriginID { get; set; }

			// Token: 0x17002D94 RID: 11668
			// (get) Token: 0x06013F21 RID: 81697 RVA: 0x005418FA File Offset: 0x0053FAFA
			// (set) Token: 0x06013F22 RID: 81698 RVA: 0x00541902 File Offset: 0x0053FB02
			public int X { get; set; }

			// Token: 0x17002D95 RID: 11669
			// (get) Token: 0x06013F23 RID: 81699 RVA: 0x0054190B File Offset: 0x0053FB0B
			// (set) Token: 0x06013F24 RID: 81700 RVA: 0x00541913 File Offset: 0x0053FB13
			public int Y { get; set; }
		}

		// Token: 0x02002992 RID: 10642
		public class EmoteInfo
		{
			// Token: 0x17002D96 RID: 11670
			// (get) Token: 0x06013F26 RID: 81702 RVA: 0x0054191C File Offset: 0x0053FB1C
			// (set) Token: 0x06013F27 RID: 81703 RVA: 0x00541924 File Offset: 0x0053FB24
			public int Emote { get; set; }
		}
	}

	// Token: 0x02002095 RID: 8341
	public enum PowerType
	{
		// Token: 0x0400DDC3 RID: 56771
		FULL_ENTITY = 1,
		// Token: 0x0400DDC4 RID: 56772
		SHOW_ENTITY,
		// Token: 0x0400DDC5 RID: 56773
		HIDE_ENTITY,
		// Token: 0x0400DDC6 RID: 56774
		TAG_CHANGE,
		// Token: 0x0400DDC7 RID: 56775
		BLOCK_START,
		// Token: 0x0400DDC8 RID: 56776
		BLOCK_END,
		// Token: 0x0400DDC9 RID: 56777
		CREATE_GAME,
		// Token: 0x0400DDCA RID: 56778
		META_DATA,
		// Token: 0x0400DDCB RID: 56779
		CHANGE_ENTITY,
		// Token: 0x0400DDCC RID: 56780
		RESET_GAME,
		// Token: 0x0400DDCD RID: 56781
		SUB_SPELL_START,
		// Token: 0x0400DDCE RID: 56782
		SUB_SPELL_END,
		// Token: 0x0400DDCF RID: 56783
		VO_SPELL,
		// Token: 0x0400DDD0 RID: 56784
		CACHED_TAG_FOR_DORMANT_CHANGE,
		// Token: 0x0400DDD1 RID: 56785
		SHUFFLE_DECK
	}

	// Token: 0x02002096 RID: 8342
	public class PowerHistory
	{
		// Token: 0x06011EE8 RID: 73448 RVA: 0x004FB54A File Offset: 0x004F974A
		public PowerHistory(Network.PowerType init)
		{
			this.Type = init;
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x06011EE9 RID: 73449 RVA: 0x004FB559 File Offset: 0x004F9759
		// (set) Token: 0x06011EEA RID: 73450 RVA: 0x004FB561 File Offset: 0x004F9761
		public Network.PowerType Type { get; set; }

		// Token: 0x06011EEB RID: 73451 RVA: 0x004FB56A File Offset: 0x004F976A
		public override string ToString()
		{
			return string.Format("type={0}", this.Type);
		}
	}

	// Token: 0x02002097 RID: 8343
	public class HistBlockStart : Network.PowerHistory
	{
		// Token: 0x06011EEC RID: 73452 RVA: 0x004FB581 File Offset: 0x004F9781
		public HistBlockStart(HistoryBlock.Type type) : base(Network.PowerType.BLOCK_START)
		{
			this.BlockType = type;
		}

		// Token: 0x06011EED RID: 73453 RVA: 0x004FB594 File Offset: 0x004F9794
		public override string ToString()
		{
			return string.Format("type={0} blockType={1} entity={2} target={3} b={4} d={5} xd={6} bigCard={7}", new object[]
			{
				base.Type,
				this.BlockType,
				this.Entities,
				this.Target,
				this.IsBatchable,
				this.IsDeferrable,
				this.IsDeferBlocker,
				this.ForceShowBigCard
			});
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x06011EEE RID: 73454 RVA: 0x004FB61C File Offset: 0x004F981C
		// (set) Token: 0x06011EEF RID: 73455 RVA: 0x004FB624 File Offset: 0x004F9824
		public HistoryBlock.Type BlockType { get; set; }

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x06011EF0 RID: 73456 RVA: 0x004FB62D File Offset: 0x004F982D
		// (set) Token: 0x06011EF1 RID: 73457 RVA: 0x004FB635 File Offset: 0x004F9835
		public List<int> Entities { get; set; }

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x06011EF2 RID: 73458 RVA: 0x004FB63E File Offset: 0x004F983E
		// (set) Token: 0x06011EF3 RID: 73459 RVA: 0x004FB646 File Offset: 0x004F9846
		public int Target { get; set; }

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x06011EF4 RID: 73460 RVA: 0x004FB64F File Offset: 0x004F984F
		// (set) Token: 0x06011EF5 RID: 73461 RVA: 0x004FB657 File Offset: 0x004F9857
		public int SubOption { get; set; }

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x06011EF6 RID: 73462 RVA: 0x004FB660 File Offset: 0x004F9860
		// (set) Token: 0x06011EF7 RID: 73463 RVA: 0x004FB668 File Offset: 0x004F9868
		public List<string> EffectCardId { get; set; }

		// Token: 0x1700274F RID: 10063
		// (get) Token: 0x06011EF8 RID: 73464 RVA: 0x004FB671 File Offset: 0x004F9871
		// (set) Token: 0x06011EF9 RID: 73465 RVA: 0x004FB679 File Offset: 0x004F9879
		public List<bool> IsEffectCardIdClientCached { get; set; }

		// Token: 0x17002750 RID: 10064
		// (get) Token: 0x06011EFA RID: 73466 RVA: 0x004FB682 File Offset: 0x004F9882
		// (set) Token: 0x06011EFB RID: 73467 RVA: 0x004FB68A File Offset: 0x004F988A
		public int EffectIndex { get; set; }

		// Token: 0x17002751 RID: 10065
		// (get) Token: 0x06011EFC RID: 73468 RVA: 0x004FB693 File Offset: 0x004F9893
		// (set) Token: 0x06011EFD RID: 73469 RVA: 0x004FB69B File Offset: 0x004F989B
		public int TriggerKeyword { get; set; }

		// Token: 0x17002752 RID: 10066
		// (get) Token: 0x06011EFE RID: 73470 RVA: 0x004FB6A4 File Offset: 0x004F98A4
		// (set) Token: 0x06011EFF RID: 73471 RVA: 0x004FB6AC File Offset: 0x004F98AC
		public bool ShowInHistory { get; set; }

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x06011F00 RID: 73472 RVA: 0x004FB6B5 File Offset: 0x004F98B5
		// (set) Token: 0x06011F01 RID: 73473 RVA: 0x004FB6BD File Offset: 0x004F98BD
		public bool IsDeferrable { get; set; }

		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x06011F02 RID: 73474 RVA: 0x004FB6C6 File Offset: 0x004F98C6
		// (set) Token: 0x06011F03 RID: 73475 RVA: 0x004FB6CE File Offset: 0x004F98CE
		public bool IsBatchable { get; set; }

		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x06011F04 RID: 73476 RVA: 0x004FB6D7 File Offset: 0x004F98D7
		// (set) Token: 0x06011F05 RID: 73477 RVA: 0x004FB6DF File Offset: 0x004F98DF
		public bool IsDeferBlocker { get; set; }

		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x06011F06 RID: 73478 RVA: 0x004FB6E8 File Offset: 0x004F98E8
		// (set) Token: 0x06011F07 RID: 73479 RVA: 0x004FB6F0 File Offset: 0x004F98F0
		public bool ForceShowBigCard { get; set; }
	}

	// Token: 0x02002098 RID: 8344
	public class HistBlockEnd : Network.PowerHistory
	{
		// Token: 0x06011F08 RID: 73480 RVA: 0x004FB6F9 File Offset: 0x004F98F9
		public HistBlockEnd() : base(Network.PowerType.BLOCK_END)
		{
		}
	}

	// Token: 0x02002099 RID: 8345
	public class HistCreateGame : Network.PowerHistory
	{
		// Token: 0x06011F09 RID: 73481 RVA: 0x004FB704 File Offset: 0x004F9904
		public static Network.HistCreateGame CreateFromProto(PowerHistoryCreateGame src)
		{
			Network.HistCreateGame histCreateGame = new Network.HistCreateGame();
			histCreateGame.Uuid = src.GameUuid;
			histCreateGame.Game = Network.Entity.CreateFromProto(src.GameEntity);
			if (src.Players != null)
			{
				histCreateGame.Players = new List<Network.HistCreateGame.PlayerData>();
				for (int i = 0; i < src.Players.Count; i++)
				{
					Network.HistCreateGame.PlayerData item = Network.HistCreateGame.PlayerData.CreateFromProto(src.Players[i]);
					histCreateGame.Players.Add(item);
				}
			}
			if (src.PlayerInfos != null)
			{
				histCreateGame.PlayerInfos = new List<Network.HistCreateGame.SharedPlayerInfo>();
				for (int j = 0; j < src.PlayerInfos.Count; j++)
				{
					Network.HistCreateGame.SharedPlayerInfo item2 = Network.HistCreateGame.SharedPlayerInfo.CreateFromProto(src.PlayerInfos[j]);
					histCreateGame.PlayerInfos.Add(item2);
				}
			}
			return histCreateGame;
		}

		// Token: 0x06011F0A RID: 73482 RVA: 0x004FB7C5 File Offset: 0x004F99C5
		public HistCreateGame() : base(Network.PowerType.CREATE_GAME)
		{
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x06011F0B RID: 73483 RVA: 0x004FB7CE File Offset: 0x004F99CE
		// (set) Token: 0x06011F0C RID: 73484 RVA: 0x004FB7D6 File Offset: 0x004F99D6
		public Network.Entity Game { get; set; }

		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x06011F0D RID: 73485 RVA: 0x004FB7DF File Offset: 0x004F99DF
		// (set) Token: 0x06011F0E RID: 73486 RVA: 0x004FB7E7 File Offset: 0x004F99E7
		public string Uuid { get; set; }

		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x06011F0F RID: 73487 RVA: 0x004FB7F0 File Offset: 0x004F99F0
		// (set) Token: 0x06011F10 RID: 73488 RVA: 0x004FB7F8 File Offset: 0x004F99F8
		public List<Network.HistCreateGame.PlayerData> Players { get; set; }

		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x06011F11 RID: 73489 RVA: 0x004FB801 File Offset: 0x004F9A01
		// (set) Token: 0x06011F12 RID: 73490 RVA: 0x004FB809 File Offset: 0x004F9A09
		public List<Network.HistCreateGame.SharedPlayerInfo> PlayerInfos { get; set; }

		// Token: 0x06011F13 RID: 73491 RVA: 0x004FB814 File Offset: 0x004F9A14
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("game={0}", this.Game);
			if (this.Players == null)
			{
				stringBuilder.Append(" players=(null)");
			}
			else if (this.Players.Count == 0)
			{
				stringBuilder.Append(" players=0");
			}
			else
			{
				for (int i = 0; i < this.Players.Count; i++)
				{
					stringBuilder.AppendFormat(" players[{0}]=[{1}]", i, this.Players[i]);
				}
			}
			if (this.PlayerInfos == null)
			{
				stringBuilder.Append(" playerInfos=(null)");
			}
			else if (this.PlayerInfos.Count == 0)
			{
				stringBuilder.Append(" playerInfos=0");
			}
			else
			{
				for (int j = 0; j < this.PlayerInfos.Count; j++)
				{
					stringBuilder.AppendFormat(" playerInfos[{0}]=[{1}]", j, this.PlayerInfos[j]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x02002993 RID: 10643
		public class PlayerData
		{
			// Token: 0x17002D97 RID: 11671
			// (get) Token: 0x06013F29 RID: 81705 RVA: 0x0054192D File Offset: 0x0053FB2D
			// (set) Token: 0x06013F2A RID: 81706 RVA: 0x00541935 File Offset: 0x0053FB35
			public int ID { get; set; }

			// Token: 0x17002D98 RID: 11672
			// (get) Token: 0x06013F2B RID: 81707 RVA: 0x0054193E File Offset: 0x0053FB3E
			// (set) Token: 0x06013F2C RID: 81708 RVA: 0x00541946 File Offset: 0x0053FB46
			public BnetGameAccountId GameAccountId { get; set; }

			// Token: 0x17002D99 RID: 11673
			// (get) Token: 0x06013F2D RID: 81709 RVA: 0x0054194F File Offset: 0x0053FB4F
			// (set) Token: 0x06013F2E RID: 81710 RVA: 0x00541957 File Offset: 0x0053FB57
			public Network.Entity Player { get; set; }

			// Token: 0x17002D9A RID: 11674
			// (get) Token: 0x06013F2F RID: 81711 RVA: 0x00541960 File Offset: 0x0053FB60
			// (set) Token: 0x06013F30 RID: 81712 RVA: 0x00541968 File Offset: 0x0053FB68
			public int CardBackID { get; set; }

			// Token: 0x06013F31 RID: 81713 RVA: 0x00541974 File Offset: 0x0053FB74
			public static Network.HistCreateGame.PlayerData CreateFromProto(PegasusGame.Player src)
			{
				return new Network.HistCreateGame.PlayerData
				{
					ID = src.Id,
					GameAccountId = BnetGameAccountId.CreateFromNet(src.GameAccountId),
					Player = Network.Entity.CreateFromProto(src.Entity),
					CardBackID = src.CardBack
				};
			}

			// Token: 0x06013F32 RID: 81714 RVA: 0x005419C0 File Offset: 0x0053FBC0
			public override string ToString()
			{
				return string.Format("ID={0} GameAccountId={1} Player={2} CardBackID={3}", new object[]
				{
					this.ID,
					this.GameAccountId,
					this.Player,
					this.CardBackID
				});
			}
		}

		// Token: 0x02002994 RID: 10644
		public class SharedPlayerInfo
		{
			// Token: 0x17002D9B RID: 11675
			// (get) Token: 0x06013F34 RID: 81716 RVA: 0x00541A00 File Offset: 0x0053FC00
			// (set) Token: 0x06013F35 RID: 81717 RVA: 0x00541A08 File Offset: 0x0053FC08
			public int ID { get; set; }

			// Token: 0x17002D9C RID: 11676
			// (get) Token: 0x06013F36 RID: 81718 RVA: 0x00541A11 File Offset: 0x0053FC11
			// (set) Token: 0x06013F37 RID: 81719 RVA: 0x00541A19 File Offset: 0x0053FC19
			public BnetGameAccountId GameAccountId { get; set; }

			// Token: 0x06013F38 RID: 81720 RVA: 0x00541A22 File Offset: 0x0053FC22
			public static Network.HistCreateGame.SharedPlayerInfo CreateFromProto(PegasusGame.SharedPlayerInfo src)
			{
				return new Network.HistCreateGame.SharedPlayerInfo
				{
					ID = src.Id,
					GameAccountId = BnetGameAccountId.CreateFromNet(src.GameAccountId)
				};
			}

			// Token: 0x06013F39 RID: 81721 RVA: 0x00541A46 File Offset: 0x0053FC46
			public override string ToString()
			{
				return string.Format("ID={0} GameAccountId={1}", this.ID, this.GameAccountId);
			}
		}
	}

	// Token: 0x0200209A RID: 8346
	public class HistResetGame : Network.PowerHistory
	{
		// Token: 0x06011F14 RID: 73492 RVA: 0x004FB909 File Offset: 0x004F9B09
		public HistResetGame() : base(Network.PowerType.RESET_GAME)
		{
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x06011F15 RID: 73493 RVA: 0x004FB913 File Offset: 0x004F9B13
		// (set) Token: 0x06011F16 RID: 73494 RVA: 0x004FB91B File Offset: 0x004F9B1B
		public Network.HistCreateGame CreateGame { get; set; }

		// Token: 0x06011F17 RID: 73495 RVA: 0x004FB56A File Offset: 0x004F976A
		public override string ToString()
		{
			return string.Format("type={0}", base.Type);
		}

		// Token: 0x06011F18 RID: 73496 RVA: 0x004FB924 File Offset: 0x004F9B24
		public static Network.HistResetGame CreateFromProto(PowerHistoryResetGame proto)
		{
			return new Network.HistResetGame
			{
				CreateGame = Network.HistCreateGame.CreateFromProto(proto.CreateGame)
			};
		}
	}

	// Token: 0x0200209B RID: 8347
	public class HistFullEntity : Network.PowerHistory
	{
		// Token: 0x06011F19 RID: 73497 RVA: 0x004FB93C File Offset: 0x004F9B3C
		public HistFullEntity() : base(Network.PowerType.FULL_ENTITY)
		{
		}

		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x06011F1A RID: 73498 RVA: 0x004FB945 File Offset: 0x004F9B45
		// (set) Token: 0x06011F1B RID: 73499 RVA: 0x004FB94D File Offset: 0x004F9B4D
		public Network.Entity Entity { get; set; }

		// Token: 0x06011F1C RID: 73500 RVA: 0x004FB956 File Offset: 0x004F9B56
		public override string ToString()
		{
			return string.Format("type={0} entity=[{1}]", base.Type, this.Entity);
		}
	}

	// Token: 0x0200209C RID: 8348
	public class HistShowEntity : Network.PowerHistory
	{
		// Token: 0x06011F1D RID: 73501 RVA: 0x004FB973 File Offset: 0x004F9B73
		public HistShowEntity() : base(Network.PowerType.SHOW_ENTITY)
		{
		}

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x06011F1E RID: 73502 RVA: 0x004FB97C File Offset: 0x004F9B7C
		// (set) Token: 0x06011F1F RID: 73503 RVA: 0x004FB984 File Offset: 0x004F9B84
		public Network.Entity Entity { get; set; }

		// Token: 0x06011F20 RID: 73504 RVA: 0x004FB98D File Offset: 0x004F9B8D
		public override string ToString()
		{
			return string.Format("type={0} entity=[{1}]", base.Type, this.Entity);
		}
	}

	// Token: 0x0200209D RID: 8349
	public class HistHideEntity : Network.PowerHistory
	{
		// Token: 0x06011F21 RID: 73505 RVA: 0x004FB9AA File Offset: 0x004F9BAA
		public HistHideEntity() : base(Network.PowerType.HIDE_ENTITY)
		{
		}

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x06011F22 RID: 73506 RVA: 0x004FB9B3 File Offset: 0x004F9BB3
		// (set) Token: 0x06011F23 RID: 73507 RVA: 0x004FB9BB File Offset: 0x004F9BBB
		public int Entity { get; set; }

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x06011F24 RID: 73508 RVA: 0x004FB9C4 File Offset: 0x004F9BC4
		// (set) Token: 0x06011F25 RID: 73509 RVA: 0x004FB9CC File Offset: 0x004F9BCC
		public int Zone { get; set; }

		// Token: 0x06011F26 RID: 73510 RVA: 0x004FB9D5 File Offset: 0x004F9BD5
		public override string ToString()
		{
			return string.Format("type={0} entity={1} zone={2}", base.Type, this.Entity, this.Zone);
		}
	}

	// Token: 0x0200209E RID: 8350
	public class HistChangeEntity : Network.PowerHistory
	{
		// Token: 0x06011F27 RID: 73511 RVA: 0x004FBA02 File Offset: 0x004F9C02
		public HistChangeEntity() : base(Network.PowerType.CHANGE_ENTITY)
		{
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x06011F28 RID: 73512 RVA: 0x004FBA0C File Offset: 0x004F9C0C
		// (set) Token: 0x06011F29 RID: 73513 RVA: 0x004FBA14 File Offset: 0x004F9C14
		public Network.Entity Entity { get; set; }

		// Token: 0x06011F2A RID: 73514 RVA: 0x004FBA1D File Offset: 0x004F9C1D
		public override string ToString()
		{
			return string.Format("type={0} entity=[{1}]", base.Type, this.Entity);
		}
	}

	// Token: 0x0200209F RID: 8351
	public class HistTagChange : Network.PowerHistory
	{
		// Token: 0x06011F2B RID: 73515 RVA: 0x004FBA3A File Offset: 0x004F9C3A
		public HistTagChange() : base(Network.PowerType.TAG_CHANGE)
		{
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x06011F2C RID: 73516 RVA: 0x004FBA43 File Offset: 0x004F9C43
		// (set) Token: 0x06011F2D RID: 73517 RVA: 0x004FBA4B File Offset: 0x004F9C4B
		public int Entity { get; set; }

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x06011F2E RID: 73518 RVA: 0x004FBA54 File Offset: 0x004F9C54
		// (set) Token: 0x06011F2F RID: 73519 RVA: 0x004FBA5C File Offset: 0x004F9C5C
		public int Tag { get; set; }

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06011F30 RID: 73520 RVA: 0x004FBA65 File Offset: 0x004F9C65
		// (set) Token: 0x06011F31 RID: 73521 RVA: 0x004FBA6D File Offset: 0x004F9C6D
		public int Value { get; set; }

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06011F32 RID: 73522 RVA: 0x004FBA76 File Offset: 0x004F9C76
		// (set) Token: 0x06011F33 RID: 73523 RVA: 0x004FBA7E File Offset: 0x004F9C7E
		public bool ChangeDef { get; set; }

		// Token: 0x06011F34 RID: 73524 RVA: 0x004FBA88 File Offset: 0x004F9C88
		public override string ToString()
		{
			return string.Format("type={0} entity={1} tag={2} value={3}", new object[]
			{
				base.Type,
				this.Entity,
				this.Tag,
				this.Value
			});
		}
	}

	// Token: 0x020020A0 RID: 8352
	public class HistMetaData : Network.PowerHistory
	{
		// Token: 0x06011F35 RID: 73525 RVA: 0x004FBADD File Offset: 0x004F9CDD
		public HistMetaData() : base(Network.PowerType.META_DATA)
		{
			this.Info = new List<int>();
			this.AdditionalData = new List<int>();
		}

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x06011F36 RID: 73526 RVA: 0x004FBAFC File Offset: 0x004F9CFC
		// (set) Token: 0x06011F37 RID: 73527 RVA: 0x004FBB04 File Offset: 0x004F9D04
		public HistoryMeta.Type MetaType { get; set; }

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x06011F38 RID: 73528 RVA: 0x004FBB0D File Offset: 0x004F9D0D
		// (set) Token: 0x06011F39 RID: 73529 RVA: 0x004FBB15 File Offset: 0x004F9D15
		public List<int> Info { get; set; }

		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x06011F3A RID: 73530 RVA: 0x004FBB1E File Offset: 0x004F9D1E
		// (set) Token: 0x06011F3B RID: 73531 RVA: 0x004FBB26 File Offset: 0x004F9D26
		public int Data { get; set; }

		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06011F3C RID: 73532 RVA: 0x004FBB2F File Offset: 0x004F9D2F
		// (set) Token: 0x06011F3D RID: 73533 RVA: 0x004FBB37 File Offset: 0x004F9D37
		public List<int> AdditionalData { get; set; }

		// Token: 0x06011F3E RID: 73534 RVA: 0x004FBB40 File Offset: 0x004F9D40
		public override string ToString()
		{
			return string.Format("type={0} metaType={1} infoCount={2} data={3}", new object[]
			{
				base.Type,
				this.MetaType,
				this.Info.Count,
				this.Data
			});
		}
	}

	// Token: 0x020020A1 RID: 8353
	public class HistSubSpellStart : Network.PowerHistory
	{
		// Token: 0x06011F3F RID: 73535 RVA: 0x004FBB9A File Offset: 0x004F9D9A
		public HistSubSpellStart() : base(Network.PowerType.SUB_SPELL_START)
		{
		}

		// Token: 0x06011F40 RID: 73536 RVA: 0x004FBBA4 File Offset: 0x004F9DA4
		public static Network.HistSubSpellStart CreateFromProto(PowerHistorySubSpellStart proto)
		{
			return new Network.HistSubSpellStart
			{
				SpellPrefabGUID = proto.SpellPrefabGuid,
				SourceEntityID = (proto.HasSourceEntityId ? proto.SourceEntityId : 0),
				TargetEntityIDS = proto.TargetEntityIds
			};
		}

		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x06011F41 RID: 73537 RVA: 0x004FBBDA File Offset: 0x004F9DDA
		// (set) Token: 0x06011F42 RID: 73538 RVA: 0x004FBBE2 File Offset: 0x004F9DE2
		public string SpellPrefabGUID { get; set; }

		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x06011F43 RID: 73539 RVA: 0x004FBBEB File Offset: 0x004F9DEB
		// (set) Token: 0x06011F44 RID: 73540 RVA: 0x004FBBF3 File Offset: 0x004F9DF3
		public int SourceEntityID { get; set; }

		// Token: 0x1700276B RID: 10091
		// (get) Token: 0x06011F45 RID: 73541 RVA: 0x004FBBFC File Offset: 0x004F9DFC
		// (set) Token: 0x06011F46 RID: 73542 RVA: 0x004FBC04 File Offset: 0x004F9E04
		public List<int> TargetEntityIDS { get; set; }
	}

	// Token: 0x020020A2 RID: 8354
	public class HistSubSpellEnd : Network.PowerHistory
	{
		// Token: 0x06011F47 RID: 73543 RVA: 0x004FBC0D File Offset: 0x004F9E0D
		public HistSubSpellEnd() : base(Network.PowerType.SUB_SPELL_END)
		{
		}
	}

	// Token: 0x020020A3 RID: 8355
	public class HistVoSpell : Network.PowerHistory
	{
		// Token: 0x06011F48 RID: 73544 RVA: 0x004FBC17 File Offset: 0x004F9E17
		public HistVoSpell() : base(Network.PowerType.VO_SPELL)
		{
		}

		// Token: 0x06011F49 RID: 73545 RVA: 0x004FBC24 File Offset: 0x004F9E24
		public static Network.HistVoSpell CreateFromProto(PowerHistoryVoTask proto)
		{
			return new Network.HistVoSpell
			{
				SpellPrefabGUID = proto.SpellPrefabGuid,
				Speaker = proto.SpeakingEntity,
				Blocking = proto.Blocking,
				AdditionalDelayMs = proto.AdditionalDelayMs,
				BrassRingGUID = proto.BrassRingPrefabGuid
			};
		}

		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06011F4A RID: 73546 RVA: 0x004FBC72 File Offset: 0x004F9E72
		// (set) Token: 0x06011F4B RID: 73547 RVA: 0x004FBC7A File Offset: 0x004F9E7A
		public string SpellPrefabGUID { get; set; }

		// Token: 0x1700276D RID: 10093
		// (get) Token: 0x06011F4C RID: 73548 RVA: 0x004FBC83 File Offset: 0x004F9E83
		// (set) Token: 0x06011F4D RID: 73549 RVA: 0x004FBC8B File Offset: 0x004F9E8B
		public int Speaker { get; set; }

		// Token: 0x1700276E RID: 10094
		// (get) Token: 0x06011F4E RID: 73550 RVA: 0x004FBC94 File Offset: 0x004F9E94
		// (set) Token: 0x06011F4F RID: 73551 RVA: 0x004FBC9C File Offset: 0x004F9E9C
		public bool Blocking { get; set; }

		// Token: 0x1700276F RID: 10095
		// (get) Token: 0x06011F50 RID: 73552 RVA: 0x004FBCA5 File Offset: 0x004F9EA5
		// (set) Token: 0x06011F51 RID: 73553 RVA: 0x004FBCAD File Offset: 0x004F9EAD
		public int AdditionalDelayMs { get; set; }

		// Token: 0x17002770 RID: 10096
		// (get) Token: 0x06011F52 RID: 73554 RVA: 0x004FBCB6 File Offset: 0x004F9EB6
		// (set) Token: 0x06011F53 RID: 73555 RVA: 0x004FBCBE File Offset: 0x004F9EBE
		public string BrassRingGUID { get; set; }

		// Token: 0x17002771 RID: 10097
		// (get) Token: 0x06011F54 RID: 73556 RVA: 0x004FBCC7 File Offset: 0x004F9EC7
		// (set) Token: 0x06011F55 RID: 73557 RVA: 0x004FBCCF File Offset: 0x004F9ECF
		public AudioSource m_audioSource { get; set; }

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x06011F56 RID: 73558 RVA: 0x004FBCD8 File Offset: 0x004F9ED8
		// (set) Token: 0x06011F57 RID: 73559 RVA: 0x004FBCE0 File Offset: 0x004F9EE0
		public bool m_ableToLoad { get; set; }
	}

	// Token: 0x020020A4 RID: 8356
	public class HistCachedTagForDormantChange : Network.PowerHistory
	{
		// Token: 0x06011F58 RID: 73560 RVA: 0x004FBCE9 File Offset: 0x004F9EE9
		public HistCachedTagForDormantChange() : base(Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE)
		{
		}

		// Token: 0x06011F59 RID: 73561 RVA: 0x004FBCF3 File Offset: 0x004F9EF3
		public static Network.HistCachedTagForDormantChange CreateFromProto(PowerHistoryCachedTagForDormantChange proto)
		{
			return new Network.HistCachedTagForDormantChange
			{
				Entity = proto.Entity,
				Tag = proto.Tag,
				Value = proto.Value
			};
		}

		// Token: 0x17002773 RID: 10099
		// (get) Token: 0x06011F5A RID: 73562 RVA: 0x004FBD1E File Offset: 0x004F9F1E
		// (set) Token: 0x06011F5B RID: 73563 RVA: 0x004FBD26 File Offset: 0x004F9F26
		public int Entity { get; set; }

		// Token: 0x17002774 RID: 10100
		// (get) Token: 0x06011F5C RID: 73564 RVA: 0x004FBD2F File Offset: 0x004F9F2F
		// (set) Token: 0x06011F5D RID: 73565 RVA: 0x004FBD37 File Offset: 0x004F9F37
		public int Tag { get; set; }

		// Token: 0x17002775 RID: 10101
		// (get) Token: 0x06011F5E RID: 73566 RVA: 0x004FBD40 File Offset: 0x004F9F40
		// (set) Token: 0x06011F5F RID: 73567 RVA: 0x004FBD48 File Offset: 0x004F9F48
		public int Value { get; set; }

		// Token: 0x06011F60 RID: 73568 RVA: 0x004FBD54 File Offset: 0x004F9F54
		public override string ToString()
		{
			return string.Format("type={0} entity={1} tag={2} value={3}", new object[]
			{
				base.Type,
				this.Entity,
				this.Tag,
				this.Value
			});
		}
	}

	// Token: 0x020020A5 RID: 8357
	public class HistShuffleDeck : Network.PowerHistory
	{
		// Token: 0x06011F61 RID: 73569 RVA: 0x004FBDA9 File Offset: 0x004F9FA9
		public HistShuffleDeck() : base(Network.PowerType.SHUFFLE_DECK)
		{
		}

		// Token: 0x06011F62 RID: 73570 RVA: 0x004FBDB3 File Offset: 0x004F9FB3
		public static Network.HistShuffleDeck CreateFromProto(PowerHistoryShuffleDeck proto)
		{
			return new Network.HistShuffleDeck
			{
				PlayerID = proto.PlayerId
			};
		}

		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x06011F63 RID: 73571 RVA: 0x004FBDC6 File Offset: 0x004F9FC6
		// (set) Token: 0x06011F64 RID: 73572 RVA: 0x004FBDCE File Offset: 0x004F9FCE
		public int PlayerID { get; set; }

		// Token: 0x06011F65 RID: 73573 RVA: 0x004FBDD7 File Offset: 0x004F9FD7
		public override string ToString()
		{
			return string.Format("type={0} player_id={1}", base.Type, this.PlayerID);
		}
	}

	// Token: 0x020020A6 RID: 8358
	public class CardUserData
	{
		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x06011F66 RID: 73574 RVA: 0x004FBDF9 File Offset: 0x004F9FF9
		// (set) Token: 0x06011F67 RID: 73575 RVA: 0x004FBE01 File Offset: 0x004FA001
		public int DbId { get; set; }

		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x06011F68 RID: 73576 RVA: 0x004FBE0A File Offset: 0x004FA00A
		// (set) Token: 0x06011F69 RID: 73577 RVA: 0x004FBE12 File Offset: 0x004FA012
		public int Count { get; set; }

		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x06011F6A RID: 73578 RVA: 0x004FBE1B File Offset: 0x004FA01B
		// (set) Token: 0x06011F6B RID: 73579 RVA: 0x004FBE23 File Offset: 0x004FA023
		public TAG_PREMIUM Premium { get; set; }
	}

	// Token: 0x020020A7 RID: 8359
	public class DeckContents
	{
		// Token: 0x06011F6D RID: 73581 RVA: 0x004FBE2C File Offset: 0x004FA02C
		public DeckContents()
		{
			this.Cards = new List<Network.CardUserData>();
		}

		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x06011F6E RID: 73582 RVA: 0x004FBE3F File Offset: 0x004FA03F
		// (set) Token: 0x06011F6F RID: 73583 RVA: 0x004FBE47 File Offset: 0x004FA047
		public long Deck { get; set; }

		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x06011F70 RID: 73584 RVA: 0x004FBE50 File Offset: 0x004FA050
		// (set) Token: 0x06011F71 RID: 73585 RVA: 0x004FBE58 File Offset: 0x004FA058
		public List<Network.CardUserData> Cards { get; set; }

		// Token: 0x06011F72 RID: 73586 RVA: 0x004FBE64 File Offset: 0x004FA064
		public static Network.DeckContents FromPacket(PegasusUtil.DeckContents packet)
		{
			Network.DeckContents deckContents = new Network.DeckContents();
			deckContents.Deck = packet.DeckId;
			for (int i = 0; i < packet.Cards.Count; i++)
			{
				DeckCardData deckCardData = packet.Cards[i];
				Network.CardUserData cardUserData = new Network.CardUserData();
				cardUserData.DbId = deckCardData.Def.Asset;
				cardUserData.Count = (deckCardData.HasQty ? deckCardData.Qty : 1);
				cardUserData.Premium = (TAG_PREMIUM)(deckCardData.Def.HasPremium ? deckCardData.Def.Premium : 0);
				deckContents.Cards.Add(cardUserData);
			}
			return deckContents;
		}
	}

	// Token: 0x020020A8 RID: 8360
	public class DeckName
	{
		// Token: 0x1700277C RID: 10108
		// (get) Token: 0x06011F73 RID: 73587 RVA: 0x004FBF02 File Offset: 0x004FA102
		// (set) Token: 0x06011F74 RID: 73588 RVA: 0x004FBF0A File Offset: 0x004FA10A
		public long Deck { get; set; }

		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x06011F75 RID: 73589 RVA: 0x004FBF13 File Offset: 0x004FA113
		// (set) Token: 0x06011F76 RID: 73590 RVA: 0x004FBF1B File Offset: 0x004FA11B
		public string Name { get; set; }
	}

	// Token: 0x020020A9 RID: 8361
	public class DeckCard
	{
		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x06011F78 RID: 73592 RVA: 0x004FBF24 File Offset: 0x004FA124
		// (set) Token: 0x06011F79 RID: 73593 RVA: 0x004FBF2C File Offset: 0x004FA12C
		public long Deck { get; set; }

		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x06011F7A RID: 73594 RVA: 0x004FBF35 File Offset: 0x004FA135
		// (set) Token: 0x06011F7B RID: 73595 RVA: 0x004FBF3D File Offset: 0x004FA13D
		public long Card { get; set; }
	}

	// Token: 0x020020AA RID: 8362
	public class GenericResponse
	{
		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x06011F7D RID: 73597 RVA: 0x004FBF46 File Offset: 0x004FA146
		// (set) Token: 0x06011F7E RID: 73598 RVA: 0x004FBF4E File Offset: 0x004FA14E
		public int RequestId { get; set; }

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x06011F7F RID: 73599 RVA: 0x004FBF57 File Offset: 0x004FA157
		// (set) Token: 0x06011F80 RID: 73600 RVA: 0x004FBF5F File Offset: 0x004FA15F
		public int RequestSubId { get; set; }

		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x06011F81 RID: 73601 RVA: 0x004FBF68 File Offset: 0x004FA168
		// (set) Token: 0x06011F82 RID: 73602 RVA: 0x004FBF70 File Offset: 0x004FA170
		public Network.GenericResponse.Result ResultCode { get; set; }

		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x06011F83 RID: 73603 RVA: 0x004FBF79 File Offset: 0x004FA179
		// (set) Token: 0x06011F84 RID: 73604 RVA: 0x004FBF81 File Offset: 0x004FA181
		public object GenericData { get; set; }

		// Token: 0x02002995 RID: 10645
		public enum Result
		{
			// Token: 0x0400FD5C RID: 64860
			RESULT_OK,
			// Token: 0x0400FD5D RID: 64861
			RESULT_REQUEST_IN_PROCESS,
			// Token: 0x0400FD5E RID: 64862
			RESULT_REQUEST_COMPLETE,
			// Token: 0x0400FD5F RID: 64863
			RESULT_UNKNOWN_ERROR = 100,
			// Token: 0x0400FD60 RID: 64864
			RESULT_INTERNAL_ERROR,
			// Token: 0x0400FD61 RID: 64865
			RESULT_DB_ERROR,
			// Token: 0x0400FD62 RID: 64866
			RESULT_INVALID_REQUEST,
			// Token: 0x0400FD63 RID: 64867
			RESULT_LOGIN_LOAD,
			// Token: 0x0400FD64 RID: 64868
			RESULT_DATA_MIGRATION_OR_PLAYER_ID_ERROR,
			// Token: 0x0400FD65 RID: 64869
			RESULT_INTERNAL_RPC_ERROR,
			// Token: 0x0400FD66 RID: 64870
			RESULT_DATA_MIGRATION_REQUIRED
		}
	}

	// Token: 0x020020AB RID: 8363
	public class DBAction
	{
		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x06011F86 RID: 73606 RVA: 0x004FBF8A File Offset: 0x004FA18A
		// (set) Token: 0x06011F87 RID: 73607 RVA: 0x004FBF92 File Offset: 0x004FA192
		public Network.DBAction.ActionType Action { get; set; }

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x06011F88 RID: 73608 RVA: 0x004FBF9B File Offset: 0x004FA19B
		// (set) Token: 0x06011F89 RID: 73609 RVA: 0x004FBFA3 File Offset: 0x004FA1A3
		public Network.DBAction.ResultType Result { get; set; }

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x06011F8A RID: 73610 RVA: 0x004FBFAC File Offset: 0x004FA1AC
		// (set) Token: 0x06011F8B RID: 73611 RVA: 0x004FBFB4 File Offset: 0x004FA1B4
		public long MetaData { get; set; }

		// Token: 0x02002996 RID: 10646
		public enum ActionType
		{
			// Token: 0x0400FD68 RID: 64872
			UNKNOWN,
			// Token: 0x0400FD69 RID: 64873
			GET_DECK,
			// Token: 0x0400FD6A RID: 64874
			CREATE_DECK,
			// Token: 0x0400FD6B RID: 64875
			RENAME_DECK,
			// Token: 0x0400FD6C RID: 64876
			DELETE_DECK,
			// Token: 0x0400FD6D RID: 64877
			SET_DECK,
			// Token: 0x0400FD6E RID: 64878
			OPEN_BOOSTER,
			// Token: 0x0400FD6F RID: 64879
			GAMES_INFO
		}

		// Token: 0x02002997 RID: 10647
		public enum ResultType
		{
			// Token: 0x0400FD71 RID: 64881
			UNKNOWN,
			// Token: 0x0400FD72 RID: 64882
			SUCCESS,
			// Token: 0x0400FD73 RID: 64883
			NOT_OWNED,
			// Token: 0x0400FD74 RID: 64884
			CONSTRAINT
		}
	}

	// Token: 0x020020AC RID: 8364
	public class TurnTimerInfo
	{
		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x06011F8D RID: 73613 RVA: 0x004FBFBD File Offset: 0x004FA1BD
		// (set) Token: 0x06011F8E RID: 73614 RVA: 0x004FBFC5 File Offset: 0x004FA1C5
		public float Seconds { get; set; }

		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x06011F8F RID: 73615 RVA: 0x004FBFCE File Offset: 0x004FA1CE
		// (set) Token: 0x06011F90 RID: 73616 RVA: 0x004FBFD6 File Offset: 0x004FA1D6
		public int Turn { get; set; }

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x06011F91 RID: 73617 RVA: 0x004FBFDF File Offset: 0x004FA1DF
		// (set) Token: 0x06011F92 RID: 73618 RVA: 0x004FBFE7 File Offset: 0x004FA1E7
		public bool Show { get; set; }
	}

	// Token: 0x020020AD RID: 8365
	public class CardQuote
	{
		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x06011F94 RID: 73620 RVA: 0x004FBFF0 File Offset: 0x004FA1F0
		// (set) Token: 0x06011F95 RID: 73621 RVA: 0x004FBFF8 File Offset: 0x004FA1F8
		public int AssetID { get; set; }

		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x06011F96 RID: 73622 RVA: 0x004FC001 File Offset: 0x004FA201
		// (set) Token: 0x06011F97 RID: 73623 RVA: 0x004FC009 File Offset: 0x004FA209
		public int BuyPrice { get; set; }

		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x06011F98 RID: 73624 RVA: 0x004FC012 File Offset: 0x004FA212
		// (set) Token: 0x06011F99 RID: 73625 RVA: 0x004FC01A File Offset: 0x004FA21A
		public int SaleValue { get; set; }

		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x06011F9A RID: 73626 RVA: 0x004FC023 File Offset: 0x004FA223
		// (set) Token: 0x06011F9B RID: 73627 RVA: 0x004FC02B File Offset: 0x004FA22B
		public Network.CardQuote.QuoteState Status { get; set; }

		// Token: 0x02002998 RID: 10648
		public enum QuoteState
		{
			// Token: 0x0400FD76 RID: 64886
			SUCCESS,
			// Token: 0x0400FD77 RID: 64887
			UNKNOWN_ERROR
		}
	}

	// Token: 0x020020AE RID: 8366
	public class GameEnd
	{
		// Token: 0x06011F9D RID: 73629 RVA: 0x004FC034 File Offset: 0x004FA234
		public GameEnd()
		{
			this.Notices = new List<NetCache.ProfileNotice>();
		}

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x06011F9E RID: 73630 RVA: 0x004FC047 File Offset: 0x004FA247
		// (set) Token: 0x06011F9F RID: 73631 RVA: 0x004FC04F File Offset: 0x004FA24F
		public List<NetCache.ProfileNotice> Notices { get; set; }
	}

	// Token: 0x020020AF RID: 8367
	public class ProfileNotices
	{
		// Token: 0x06011FA0 RID: 73632 RVA: 0x004FC058 File Offset: 0x004FA258
		public ProfileNotices()
		{
			this.Notices = new List<NetCache.ProfileNotice>();
		}

		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x06011FA1 RID: 73633 RVA: 0x004FC06B File Offset: 0x004FA26B
		// (set) Token: 0x06011FA2 RID: 73634 RVA: 0x004FC073 File Offset: 0x004FA273
		public List<NetCache.ProfileNotice> Notices { get; set; }
	}

	// Token: 0x020020B0 RID: 8368
	public class AccountLicenseAchieveResponse
	{
		// Token: 0x17002790 RID: 10128
		// (get) Token: 0x06011FA3 RID: 73635 RVA: 0x004FC07C File Offset: 0x004FA27C
		// (set) Token: 0x06011FA4 RID: 73636 RVA: 0x004FC084 File Offset: 0x004FA284
		public int Achieve { get; set; }

		// Token: 0x17002791 RID: 10129
		// (get) Token: 0x06011FA5 RID: 73637 RVA: 0x004FC08D File Offset: 0x004FA28D
		// (set) Token: 0x06011FA6 RID: 73638 RVA: 0x004FC095 File Offset: 0x004FA295
		public Network.AccountLicenseAchieveResponse.AchieveResult Result { get; set; }

		// Token: 0x02002999 RID: 10649
		public enum AchieveResult
		{
			// Token: 0x0400FD79 RID: 64889
			INVALID_ACHIEVE = 1,
			// Token: 0x0400FD7A RID: 64890
			NOT_ACTIVE,
			// Token: 0x0400FD7B RID: 64891
			IN_PROGRESS,
			// Token: 0x0400FD7C RID: 64892
			COMPLETE,
			// Token: 0x0400FD7D RID: 64893
			STATUS_UNKNOWN
		}
	}

	// Token: 0x020020B1 RID: 8369
	public class DebugConsoleResponse
	{
		// Token: 0x06011FA8 RID: 73640 RVA: 0x004FC09E File Offset: 0x004FA29E
		public DebugConsoleResponse()
		{
			this.Response = "";
		}

		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x06011FA9 RID: 73641 RVA: 0x004FC0B1 File Offset: 0x004FA2B1
		// (set) Token: 0x06011FAA RID: 73642 RVA: 0x004FC0B9 File Offset: 0x004FA2B9
		public int Type { get; set; }

		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x06011FAB RID: 73643 RVA: 0x004FC0C2 File Offset: 0x004FA2C2
		// (set) Token: 0x06011FAC RID: 73644 RVA: 0x004FC0CA File Offset: 0x004FA2CA
		public string Response { get; set; }
	}

	// Token: 0x020020B2 RID: 8370
	public class RecruitInfo
	{
		// Token: 0x06011FAD RID: 73645 RVA: 0x004FC0D3 File Offset: 0x004FA2D3
		public RecruitInfo()
		{
			this.ID = 0UL;
			this.RecruitID = new BnetAccountId();
			this.Nickname = "";
			this.Status = 0;
			this.Level = 0;
		}

		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x06011FAE RID: 73646 RVA: 0x004FC107 File Offset: 0x004FA307
		// (set) Token: 0x06011FAF RID: 73647 RVA: 0x004FC10F File Offset: 0x004FA30F
		public ulong ID { get; set; }

		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x06011FB0 RID: 73648 RVA: 0x004FC118 File Offset: 0x004FA318
		// (set) Token: 0x06011FB1 RID: 73649 RVA: 0x004FC120 File Offset: 0x004FA320
		public BnetAccountId RecruitID { get; set; }

		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x06011FB2 RID: 73650 RVA: 0x004FC129 File Offset: 0x004FA329
		// (set) Token: 0x06011FB3 RID: 73651 RVA: 0x004FC131 File Offset: 0x004FA331
		public string Nickname { get; set; }

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x06011FB4 RID: 73652 RVA: 0x004FC13A File Offset: 0x004FA33A
		// (set) Token: 0x06011FB5 RID: 73653 RVA: 0x004FC142 File Offset: 0x004FA342
		public int Status { get; set; }

		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x06011FB6 RID: 73654 RVA: 0x004FC14B File Offset: 0x004FA34B
		// (set) Token: 0x06011FB7 RID: 73655 RVA: 0x004FC153 File Offset: 0x004FA353
		public int Level { get; set; }

		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x06011FB8 RID: 73656 RVA: 0x004FC15C File Offset: 0x004FA35C
		// (set) Token: 0x06011FB9 RID: 73657 RVA: 0x004FC164 File Offset: 0x004FA364
		public ulong CreationTimeMicrosec { get; set; }

		// Token: 0x06011FBA RID: 73658 RVA: 0x004FC170 File Offset: 0x004FA370
		public override string ToString()
		{
			return string.Format("[RecruitInfo: ID={0}, RecruitID={1}, Nickname={2}, Status={3}, Level={4}]", new object[]
			{
				this.ID,
				this.RecruitID,
				this.Nickname,
				this.Status,
				this.Level
			});
		}
	}
}
