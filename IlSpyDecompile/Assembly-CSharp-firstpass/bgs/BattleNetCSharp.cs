using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using bnet.protocol.authentication.v1;
using bnet.protocol.connection.v1;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.notification.v1;
using bnet.protocol.v2;

namespace bgs
{
	public class BattleNetCSharp : IBattleNet
	{
		public enum ConnectionState
		{
			Disconnected,
			Connect,
			InitRPC,
			WaitForInitRPC,
			Logon,
			WaitForLogon,
			WaitForGameAccountSelect,
			WaitForAPIToInitialize,
			Ready,
			Error
		}

		public delegate void AuroraStateHandler();

		private delegate void NotificationHandler(Notification notification);

		private delegate void OnConnectHandler(BattleNetErrors error);

		private delegate void OnDisconectHandler(BattleNetErrors error);

		private class BnetErrorComparer : IComparer<BnetErrorInfo>
		{
			public int Compare(BnetErrorInfo x, BnetErrorInfo y)
			{
				if (x == null || y == null)
				{
					return 0;
				}
				if (x.GetError() == BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED && y.GetError() != BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED)
				{
					return 1;
				}
				if (x.GetError() != BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED && y.GetError() == BattleNetErrors.ERROR_RPC_PEER_DISCONNECTED)
				{
					return -1;
				}
				return 0;
			}
		}

		private BattleNetLogSource m_logSource = new BattleNetLogSource("Main");

		private FriendsAPI m_friendAPI;

		private PresenceAPI m_presenceAPI;

		private ChannelAPI m_channelAPI;

		private GamesAPI m_gamesAPI;

		private PartyAPI m_partyAPI;

		private ChallengeAPI m_challengeAPI;

		private WhisperAPI m_whisperAPI;

		private NotificationAPI m_notificationAPI;

		private BroadcastAPI m_broadcastAPI;

		private AccountAPI m_accountAPI;

		private AuthenticationAPI m_authenticationAPI;

		private LocalStorageAPI m_localStorageAPI;

		private ResourcesAPI m_resourcesAPI;

		private ProfanityAPI m_profanityAPI;

		private SessionAPI m_sessionAPI;

		private Dictionary<string, NotificationHandler> m_notificationHandlers;

		private Dictionary<ConnectionState, AuroraStateHandler> m_stateHandlers;

		private List<ServiceDescriptor> m_importedServices;

		private List<ServiceDescriptor> m_exportedServices;

		private List<BattleNet.BnetEvent> m_bnetEvents;

		private readonly long m_keepAliveIntervalMilliseconds = 20000L;

		private readonly DateTime m_unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private long m_serverTimeUTCAtConnectMicroseconds;

		private long m_serverTimeDeltaUTCSeconds;

		private List<BattleNetAPI> m_apiList;

		private uint m_connectedRegion = uint.MaxValue;

		protected ClientInterface m_clientInterface;

		private Stopwatch m_stopwatch = new Stopwatch();

		private int m_shutdownInMinutes;

		private string m_auroraEnvironment;

		private uint m_auroraPort;

		private string m_userEmailAddress;

		private readonly ICompressionProvider m_compressionProvider;

		private readonly IFileUtil m_fileUtil;

		private readonly IJsonSerializer m_jsonSerializer;

		private readonly IRpcConnectionFactory m_rpcConnectionFactory;

		private readonly int[] ConnectAuroraAttemptRateByAttemptCount = new int[5] { 1, 1, 2, 3, 5 };

		private DateTime m_attemptTime;

		private bool m_hasSuccessfullyConnected;

		private int m_retryAttemptCount;

		private string m_connectAuroraAddress;

		private uint m_connectAuroraPort;

		private SslParameters m_connectAuororaSslParams;

		private ISocketEventListener m_socketEventListener;

		private bool m_isSubscribedToMembershipEvents;

		private bool m_initialized;

		private IRpcConnection m_rpcConnection;

		private ConnectionState m_connectionState;

		private List<BnetErrorInfo> m_errorEvents = new List<BnetErrorInfo>();

		private const uint DEFAULT_PORT = 1119u;

		private const string s_programName = "WTCG";

		private ServiceDescriptor m_connectionService = new ConnectionService();

		private ServiceDescriptor m_notificationService = new NotificationService();

		private ServiceDescriptor m_notificationListenerService = new NotificationListenerService();

		public FriendsAPI Friends => m_friendAPI;

		public PresenceAPI Presence => m_presenceAPI;

		public ChannelAPI Channel => m_channelAPI;

		public GamesAPI Games => m_gamesAPI;

		public PartyAPI Party => m_partyAPI;

		public ChallengeAPI Challenge => m_challengeAPI;

		public WhisperAPI Whisper => m_whisperAPI;

		public NotificationAPI Notification => m_notificationAPI;

		public BroadcastAPI Broadcast => m_broadcastAPI;

		public AccountAPI Account => m_accountAPI;

		public SessionAPI Session => m_sessionAPI;

		public ResourcesAPI Resources => m_resourcesAPI;

		public LocalStorageAPI LocalStorage => m_localStorageAPI;

		public long ServerTimeUTCAtConnectMicroseconds => m_serverTimeUTCAtConnectMicroseconds;

		public long CurrentUTCServerTimeSeconds => GetCurrentTimeSecondsSinceUnixEpoch() + m_serverTimeDeltaUTCSeconds;

		public ICompressionProvider CompressionProvider => m_compressionProvider;

		public bnet.protocol.EntityId GameAccountId => m_authenticationAPI.GameAccountId;

		public bnet.protocol.EntityId AccountId => m_authenticationAPI.AccountId;

		public ServiceDescriptor NotificationService => m_notificationService;

		public event Action<BattleNetErrors> OnConnected;

		public event Action<BattleNetErrors> OnDisconnected;

		public long GetCurrentTimeSecondsSinceUnixEpoch()
		{
			return (long)(DateTime.UtcNow - m_unixEpoch).TotalSeconds;
		}

		public double GetRealTimeSinceStartup()
		{
			return m_stopwatch.Elapsed.TotalSeconds;
		}

		public BattleNetCSharp(ClientInterface clientInterface, IRpcConnectionFactory rpcConnectionFactory, ICompressionProvider compressionProvider, IFileUtil fileUtil, IJsonSerializer jsonSerializer, LoggerInterface loggerInterface, ISocketEventListener socketEventListener)
		{
			m_clientInterface = clientInterface;
			m_rpcConnectionFactory = rpcConnectionFactory;
			m_compressionProvider = compressionProvider;
			m_fileUtil = fileUtil;
			m_jsonSerializer = jsonSerializer;
			m_socketEventListener = socketEventListener;
			LogAdapter.SetLogger(loggerInterface);
			m_notificationHandlers = new Dictionary<string, NotificationHandler>();
			m_stateHandlers = new Dictionary<ConnectionState, AuroraStateHandler>();
			m_importedServices = new List<ServiceDescriptor>();
			m_exportedServices = new List<ServiceDescriptor>();
			m_apiList = new List<BattleNetAPI>();
			m_bnetEvents = new List<BattleNet.BnetEvent>();
			m_friendAPI = new FriendsAPI(this);
			m_presenceAPI = new PresenceAPI(this);
			m_channelAPI = new ChannelAPI(this);
			m_gamesAPI = new GamesAPI(this);
			m_partyAPI = new PartyAPI(this);
			m_challengeAPI = new ChallengeAPI(this);
			m_whisperAPI = new WhisperAPI(this);
			m_notificationAPI = new NotificationAPI(this);
			m_broadcastAPI = new BroadcastAPI(this);
			m_accountAPI = new AccountAPI(this);
			m_authenticationAPI = new AuthenticationAPI(this);
			m_localStorageAPI = new LocalStorageAPI(this);
			m_resourcesAPI = new ResourcesAPI(this);
			m_profanityAPI = new ProfanityAPI(this);
			m_sessionAPI = new SessionAPI(this);
			m_notificationHandlers.Add("WHISPER", m_whisperAPI.OnWhisper);
			m_notificationHandlers.Add("WHISPER_ECHO", m_whisperAPI.OnWhisperEcho);
			m_notificationHandlers.Add("BROADCAST", m_broadcastAPI.OnBroadcast);
			m_notificationHandlers.Add("WTCG.UtilNotificationMessage", delegate(Notification n)
			{
				m_notificationAPI.OnNotification("WTCG.UtilNotificationMessage", n);
			});
			m_broadcastAPI.RegisterListener(OnBroadcastReceived);
			m_stateHandlers.Add(ConnectionState.Connect, AuroraStateHandler_Connect);
			m_stateHandlers.Add(ConnectionState.InitRPC, AuroraStateHandler_InitRPC);
			m_stateHandlers.Add(ConnectionState.WaitForInitRPC, AuroraStateHandler_WaitForInitRPC);
			m_stateHandlers.Add(ConnectionState.Logon, AuroraStateHandler_Logon);
			m_stateHandlers.Add(ConnectionState.WaitForLogon, AuroraStateHandler_WaitForLogon);
			m_stateHandlers.Add(ConnectionState.WaitForGameAccountSelect, AuroraStateHandler_WaitForGameAccountSelect);
			m_stateHandlers.Add(ConnectionState.WaitForAPIToInitialize, AuroraStateHandler_WaitForAPIToInitialize);
			m_stateHandlers.Add(ConnectionState.Ready, AuroraStateHandler_Ready);
			m_stateHandlers.Add(ConnectionState.Disconnected, AuroraStateHandler_Disconnected);
			m_stateHandlers.Add(ConnectionState.Error, AuroraStateHandler_Unhandled);
			m_apiList.Add(m_friendAPI);
			m_apiList.Add(m_presenceAPI);
			m_apiList.Add(m_channelAPI);
			m_apiList.Add(m_gamesAPI);
			m_apiList.Add(m_partyAPI);
			m_apiList.Add(m_challengeAPI);
			m_apiList.Add(m_whisperAPI);
			m_apiList.Add(m_notificationAPI);
			m_apiList.Add(m_broadcastAPI);
			m_apiList.Add(m_accountAPI);
			m_apiList.Add(m_authenticationAPI);
			m_apiList.Add(m_localStorageAPI);
			m_apiList.Add(m_resourcesAPI);
			m_apiList.Add(m_profanityAPI);
			m_apiList.Add(m_sessionAPI);
		}

		public bool IsInitialized()
		{
			return m_initialized;
		}

		public bool Init(bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			if (m_initialized)
			{
				return true;
			}
			m_stopwatch.Reset();
			m_stopwatch.Start();
			m_auroraEnvironment = targetServer;
			m_auroraPort = ((port == 0) ? 1119u : port);
			m_userEmailAddress = userEmailAddress;
			bool flag = false;
			try
			{
				flag = UriUtils.GetHostAddress(targetServer, out var _);
			}
			catch (Exception ex)
			{
				m_logSource.LogError($"Exception within GetHostAddress for target server {targetServer} : {ex.Message}");
			}
			if (flag)
			{
				m_initialized = true;
				ConnectAurora(targetServer, m_auroraPort, sslParams);
			}
			else
			{
				LogAdapter.Log(LogLevel.Fatal, "GLOBAL_ERROR_NETWORK_NO_CONNECTION");
			}
			return m_initialized;
		}

		public ClientInterface Client()
		{
			return m_clientInterface;
		}

		public void AppQuit()
		{
			RequestCloseAurora();
		}

		public bool IsConnected()
		{
			if (m_rpcConnection == null)
			{
				return false;
			}
			if (InState(ConnectionState.Ready))
			{
				return true;
			}
			return false;
		}

		public void ConnectAurora(string address, uint port, SslParameters sslParams)
		{
			m_logSource.LogInfo("Sending connection request to {0}:{1}", address, port);
			m_logSource.LogDebug("Aurora version is {0}", GetVersion());
			m_logSource.LogDebug("Connection Request to '{0}:{1}' with Version '{2}'", address, port, GetVersion());
			m_rpcConnection = m_rpcConnectionFactory.CreateRpcConnection(m_fileUtil, m_jsonSerializer, m_socketEventListener);
			m_connectionService.Id = 0u;
			m_rpcConnection.GetServiceHelper().AddImportedService(m_connectionService);
			m_rpcConnection.GetServiceHelper().AddExportedService(m_connectionService);
			m_rpcConnection.RegisterServiceMethodListener(m_connectionService.Id, 4u, HandleForceDisconnectRequest);
			m_rpcConnection.RegisterServiceMethodListener(m_connectionService.Id, 3u, HandleEchoRequest);
			m_rpcConnection.SetOnConnectHandler(OnConnectHandlerCallback);
			m_rpcConnection.SetOnDisconnectHandler(OnDisconectHandlerCallback);
			m_rpcConnection.Connect(address, port, sslParams, m_retryAttemptCount);
			SwitchToState(ConnectionState.InitRPC);
			m_attemptTime = DateTime.Now;
			m_connectAuroraAddress = address;
			m_connectAuroraPort = port;
			m_connectAuororaSslParams = sslParams;
			m_hasSuccessfullyConnected = false;
		}

		public void OnConnectHandlerCallback(BattleNetErrors error)
		{
			if (this.OnConnected != null)
			{
				this.OnConnected(error);
			}
			if (error != 0)
			{
				SwitchToState(ConnectionState.Error);
				EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnConnected, error);
			}
			else
			{
				m_logSource.LogDebug("ConnectAurora sucessful after {0}ms. Stopping any retry attempts.", DateTime.Now.Subtract(m_attemptTime).Milliseconds);
				m_hasSuccessfullyConnected = true;
				m_retryAttemptCount = 0;
			}
			foreach (BattleNetAPI api in m_apiList)
			{
				api.OnConnected(error);
			}
		}

		private void OnDisconectHandlerCallback(BattleNetErrors error)
		{
			if (this.OnDisconnected != null)
			{
				this.OnDisconnected(error);
			}
			BattleNet.Log.LogInfo("Disconnected: code={0} {1}", (int)error, error.ToString());
			SwitchToState(ConnectionState.Disconnected);
			if (error != 0)
			{
				EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, error);
			}
			m_bnetEvents.Add(BattleNet.BnetEvent.Disconnected);
			foreach (BattleNetAPI api in m_apiList)
			{
				api.OnDisconnected();
			}
			m_hasSuccessfullyConnected = false;
			m_initialized = false;
		}

		private void InitRPCListeners()
		{
			m_rpcConnection.RegisterServiceMethodListener(m_notificationListenerService.Id, 1u, HandleNotificationReceived);
			foreach (BattleNetAPI api in m_apiList)
			{
				api.InitRPCListeners(m_rpcConnection);
			}
		}

		public void CloseAurora()
		{
			BattleNet.Log.LogError("CloseAurora() is deprecated in BattleNetCSharp. Use RequestCloseAurora() instead.");
		}

		public void RequestCloseAurora()
		{
			if (m_rpcConnection != null)
			{
				m_rpcConnection.Disconnect();
			}
			SwitchToState(ConnectionState.Disconnected);
			m_initialized = false;
		}

		public void QueryAurora()
		{
		}

		public void ProcessAurora()
		{
			if (!m_hasSuccessfullyConnected)
			{
				int seconds = ConnectAuroraAttemptRateByAttemptCount[Math.Min(m_retryAttemptCount, ConnectAuroraAttemptRateByAttemptCount.Length - 1)];
				TimeSpan timeSpan = DateTime.Now.Subtract(m_attemptTime);
				if (timeSpan > new TimeSpan(0, 0, seconds))
				{
					m_attemptTime = DateTime.Now;
					m_retryAttemptCount++;
					m_logSource.LogDebug("ConnectAurora failed, attempting to retry: address={0} port={1} attempt={2} waitTime={3}ms", m_connectAuroraAddress, m_connectAuroraPort, m_retryAttemptCount, timeSpan.Milliseconds);
					ConnectAurora(m_connectAuroraAddress, m_connectAuroraPort, m_connectAuororaSslParams);
					return;
				}
			}
			if (InState(ConnectionState.Disconnected) || InState(ConnectionState.Error))
			{
				return;
			}
			if (m_rpcConnection != null)
			{
				m_rpcConnection.Update();
				if (m_rpcConnection.GetMillisecondsSinceLastPacketSent() > m_keepAliveIntervalMilliseconds)
				{
					m_rpcConnection.QueueRequest(m_connectionService, 5u, new NoData());
				}
			}
			if (m_stateHandlers.TryGetValue(m_connectionState, out var value))
			{
				value();
			}
			else
			{
				m_logSource.LogError("Missing state handler");
			}
			for (int i = 0; i < m_apiList.Count; i++)
			{
				m_apiList[i].Process();
			}
		}

		public string GetLaunchOption(string key, bool encrypted)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted, LaunchOptionHelper.ProgramCodeWtcg);
		}

		private string GetLaunchOption(string key, bool encrypted, string programCode)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted, programCode);
		}

		public string GetConnectionString()
		{
			string launchOption = GetLaunchOption("CONNECTION_STRING", encrypted: false, LaunchOptionHelper.ProgramCodeWtcg);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			string launchOption2 = GetLaunchOption("REGION", encrypted: false, LaunchOptionHelper.ProgramCodeWtcg);
			if (string.IsNullOrEmpty(launchOption2))
			{
				return null;
			}
			string key = $"CONNECTION_STRING_{launchOption2}";
			launchOption = GetLaunchOption(key, encrypted: false, LaunchOptionHelper.ProgramCodeWtcg);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			launchOption = GetLaunchOption(key, encrypted: false, LaunchOptionHelper.ProgramCodeBna);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			return null;
		}

		public constants.BnetRegion GetAccountRegion()
		{
			if (Account.GetPreferredRegion() == uint.MaxValue)
			{
				return constants.BnetRegion.REGION_UNINITIALIZED;
			}
			return (constants.BnetRegion)Account.GetPreferredRegion();
		}

		public string GetAccountCountry()
		{
			return Account.GetAccountCountry();
		}

		public bool IsHeadlessAccount()
		{
			return Account.IsHeadlessAccount();
		}

		public constants.BnetRegion GetCurrentRegion()
		{
			if (m_connectedRegion == uint.MaxValue)
			{
				return constants.BnetRegion.REGION_UNINITIALIZED;
			}
			return (constants.BnetRegion)m_connectedRegion;
		}

		public void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			Account.GetPlayRestrictions(ref restrictions, reload);
		}

		public void GetAccountState(BnetAccountId bnetAccount)
		{
			Account.GetAccountState(bnetAccount);
		}

		public int GetShutdownMinutes()
		{
			return m_shutdownInMinutes;
		}

		public int BattleNetStatus()
		{
			switch (m_connectionState)
			{
			case ConnectionState.Disconnected:
				return 5;
			case ConnectionState.Connect:
			case ConnectionState.InitRPC:
			case ConnectionState.WaitForInitRPC:
			case ConnectionState.Logon:
			case ConnectionState.WaitForLogon:
			case ConnectionState.WaitForGameAccountSelect:
			case ConnectionState.WaitForAPIToInitialize:
				return 1;
			case ConnectionState.Error:
				return 3;
			case ConnectionState.Ready:
				return 4;
			default:
				m_logSource.LogError("Unknown Battle.Net Status");
				return 0;
			}
		}

		public void GetQueueInfo(ref QueueInfo queueInfo)
		{
			m_authenticationAPI.GetQueueInfo(ref queueInfo);
		}

		public AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest()
		{
			return m_authenticationAPI.NextMemModuleRequest();
		}

		public void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			m_authenticationAPI.SendMemModuleResponse(request, memModuleResponseBytes);
		}

		public void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			m_gamesAPI.SendUtilPacket(packetId, systemId, bytes, size, subID, context, route);
		}

		public GamesAPI.UtilResponse NextUtilPacket()
		{
			return m_gamesAPI.NextUtilPacket();
		}

		public int GetErrorsCount()
		{
			return m_errorEvents.Count;
		}

		public void GetErrors([Out] BnetErrorInfo[] errors)
		{
			m_errorEvents.Sort(new BnetErrorComparer());
			m_errorEvents.CopyTo(errors);
		}

		public void ClearErrors()
		{
			m_errorEvents.Clear();
		}

		public bgs.types.EntityId GetMyGameAccountId()
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			if (GameAccountId != null)
			{
				result.hi = GameAccountId.High;
				result.lo = GameAccountId.Low;
			}
			return result;
		}

		public GameAccountHandle GetMyGameAccountHandle()
		{
			return BnetEntityId.CreateFromEntityId(GetMyGameAccountId()).GetGameAccountHandle();
		}

		public bgs.types.EntityId GetMyAccountId()
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			if (AccountId != null)
			{
				result.hi = AccountId.High;
				result.lo = AccountId.Low;
			}
			return result;
		}

		public string GetVersion()
		{
			return m_clientInterface.GetVersion();
		}

		public int GetDataVersion()
		{
			return m_clientInterface.GetDataVersion();
		}

		public string GetUserEmailAddress()
		{
			return m_userEmailAddress;
		}

		public string GetEnvironment()
		{
			return m_auroraEnvironment;
		}

		public uint GetPort()
		{
			return m_auroraPort;
		}

		public int GetBnetEventsSize()
		{
			return m_bnetEvents.Count;
		}

		public void ClearBnetEvents()
		{
			m_bnetEvents.Clear();
		}

		public void GetBnetEvents([Out] BattleNet.BnetEvent[] bnetEvents)
		{
			m_bnetEvents.CopyTo(bnetEvents);
		}

		public void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			m_gamesAPI.QueueMatchmaking(matchmakerAttributesFilter, gameAttributes, players);
		}

		public void CancelMatchmaking()
		{
			m_gamesAPI.CancelMatchmaking();
		}

		public QueueEvent GetQueueEvent()
		{
			return m_gamesAPI.GetQueueEvent();
		}

		public int PresenceSize()
		{
			return m_presenceAPI.PresenceSize();
		}

		public void ClearPresence()
		{
			m_presenceAPI.ClearPresence();
		}

		public void GetPresence([Out] PresenceUpdate[] updates)
		{
			m_presenceAPI.GetPresence(updates);
		}

		public void SetPresenceBool(uint field, bool val)
		{
			m_presenceAPI.SetPresenceBool(field, val);
		}

		public void SetAccountLevelPresenceBool(uint field, bool val)
		{
			m_presenceAPI.SetAccountLevelPresenceBool(field, val);
		}

		public void SetPresenceInt(uint field, long val)
		{
			m_presenceAPI.SetPresenceInt(field, val);
		}

		public void SetPresenceString(uint field, string val)
		{
			m_presenceAPI.SetPresenceString(field, val);
		}

		public void SetPresenceBlob(uint field, byte[] val)
		{
			m_presenceAPI.SetPresenceBlob(field, val);
		}

		public void SetPresenceEntityId(uint field, bgs.types.EntityId val)
		{
			m_presenceAPI.SetPresenceEntityId(field, val);
		}

		public void SetRichPresence([In] RichPresenceUpdate[] updates)
		{
			m_presenceAPI.PublishRichPresence(updates);
		}

		public void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			m_presenceAPI.RequestPresenceFields(isGameAccountEntityId, entityId, fieldList);
		}

		public void PresenceSubscribe(bgs.types.EntityId entityId)
		{
			m_presenceAPI.PresenceSubscribe(entityId.ToProtocol());
		}

		public void PresenceUnsubscribe(bgs.types.EntityId entityId)
		{
			m_presenceAPI.PresenceUnsubscribe(entityId.ToProtocol());
		}

		public void ChannelMembershipSubscribe()
		{
			if (!m_isSubscribedToMembershipEvents)
			{
				m_channelAPI.SubscribeMembership();
				m_isSubscribedToMembershipEvents = true;
			}
		}

		public bool IsSubscribedToEntity(bgs.types.EntityId entityId)
		{
			return m_presenceAPI.IsSubscribedToEntity(entityId.ToProtocol());
		}

		public void CreateParty(string szPartyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			m_partyAPI.CreateParty(szPartyType, privacyLevel, partyAttributes);
		}

		public void JoinParty(bgs.types.EntityId partyId, string szPartyType)
		{
			m_partyAPI.JoinParty(PartyId.FromEntityId(partyId).ToChannelId(), szPartyType);
		}

		public void LeaveParty(bgs.types.EntityId partyId)
		{
			m_partyAPI.LeaveParty(PartyId.FromEntityId(partyId).ToChannelId());
		}

		public void DissolveParty(bgs.types.EntityId partyId)
		{
			m_partyAPI.DissolveParty(PartyId.FromEntityId(partyId).ToChannelId());
		}

		public void SetPartyPrivacy(bgs.types.EntityId partyId, int privacyLevel)
		{
			m_partyAPI.SetPartyPrivacy(PartyId.FromEntityId(partyId).ToChannelId(), privacyLevel);
		}

		public void AssignPartyRole(bgs.types.EntityId partyId, bgs.types.EntityId memberId, uint roleId)
		{
			m_partyAPI.AssignPartyRole(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(memberId).GetGameAccountHandle(), roleId);
		}

		public void SendPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId inviteeId, bool isReservation)
		{
			m_partyAPI.SendPartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(inviteeId).GetGameAccountHandle(), isReservation);
		}

		public void AcceptPartyInvite(ulong invitationId)
		{
			m_partyAPI.AcceptPartyInvite(invitationId);
		}

		public void DeclinePartyInvite(ulong invitationId)
		{
			m_partyAPI.DeclinePartyInvite(invitationId);
		}

		public void RevokePartyInvite(bgs.types.EntityId partyId, ulong invitationId)
		{
			m_partyAPI.RevokePartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), invitationId);
		}

		public void RequestPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId whomToAskForApproval, bgs.types.EntityId whomToInvite, string szPartyType)
		{
			m_partyAPI.RequestPartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(whomToAskForApproval).GetGameAccountHandle(), BnetGameAccountId.CreateFromEntityId(whomToInvite).GetGameAccountHandle());
		}

		public void IgnoreInviteRequest(bgs.types.EntityId partyId, bgs.types.EntityId requestedTargetId)
		{
			m_partyAPI.IgnoreInviteRequest(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(requestedTargetId).GetGameAccountHandle());
		}

		public void KickPartyMember(bgs.types.EntityId partyId, bgs.types.EntityId memberId)
		{
			m_partyAPI.KickPartyMember(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(memberId).GetGameAccountHandle());
		}

		public void SendPartyChatMessage(bgs.types.EntityId partyId, string message)
		{
			m_partyAPI.SendPartyChatMessage(PartyId.FromEntityId(partyId).ToChannelId(), message);
		}

		public void ClearPartyAttribute(bgs.types.EntityId partyId, string attributeKey)
		{
			m_partyAPI.ClearPartyAttribute(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey);
		}

		public void SetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, [In] long value)
		{
			m_partyAPI.SetPartyAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		public void SetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, [In] string value)
		{
			m_partyAPI.SetPartyAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		public void SetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, [In] byte[] value)
		{
			m_partyAPI.SetPartyAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		public void SetPartyAttributes(bgs.types.EntityId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			m_partyAPI.SetPartyAttributes(PartyId.FromEntityId(partyId).ToChannelId(), attrs);
		}

		public void ClearMemberAttribute(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			m_partyAPI.ClearMemberAttribute(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey);
		}

		public void SetMemberAttributeLong(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value)
		{
			m_partyAPI.SetMemberAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		public void SetMemberAttributeString(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value)
		{
			m_partyAPI.SetMemberAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		public void SetMemberAttributeBlob(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value)
		{
			m_partyAPI.SetMemberAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		public void SetMemberAttributes(bgs.types.EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			m_partyAPI.SetMemberAttributes(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attrs);
		}

		public int GetPartyPrivacy(bgs.types.EntityId partyId)
		{
			return m_partyAPI.GetPartyPrivacy(PartyId.FromEntityId(partyId).ToChannelId());
		}

		public int GetCountPartyMembers(bgs.types.EntityId partyId)
		{
			return (int)m_partyAPI.GetCountPartyMembers(PartyId.FromEntityId(partyId).ToChannelId());
		}

		public int GetMaxPartyMembers(bgs.types.EntityId partyId)
		{
			return (int)m_partyAPI.GetMaxPartyMembers(PartyId.FromEntityId(partyId).ToChannelId());
		}

		public void GetPartyMembers(bgs.types.EntityId partyId, out bgs.types.PartyMember[] members)
		{
			m_partyAPI.GetPartyMembers(PartyId.FromEntityId(partyId).ToChannelId(), out members);
		}

		public void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			m_partyAPI.GetReceivedPartyInvites(out invites);
		}

		public void GetPartySentInvites(bgs.types.EntityId partyId, out PartyInvite[] invites)
		{
			m_partyAPI.GetPartySentInvites(PartyId.FromEntityId(partyId).ToChannelId(), out invites);
		}

		public void GetPartyInviteRequests(bgs.types.EntityId partyId, out InviteRequest[] requests)
		{
			m_partyAPI.GetPartyInviteRequests(PartyId.FromEntityId(partyId).ToChannelId(), out requests);
		}

		public void GetAllPartyAttributes(bgs.types.EntityId partyId, out string[] allKeys)
		{
			m_partyAPI.GetAllPartyAttributes(PartyId.FromEntityId(partyId).ToChannelId(), out allKeys);
		}

		public bool GetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, out long value)
		{
			return m_partyAPI.GetPartyAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		public void GetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, out string value)
		{
			m_partyAPI.GetPartyAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		public void GetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, out byte[] value)
		{
			m_partyAPI.GetPartyAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		public void GetMemberAttributeString(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			m_partyAPI.GetMemberAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, out value);
		}

		public void GetMemberAttributeBlob(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			m_partyAPI.GetMemberAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, out value);
		}

		public void GetPartyListenerEvents(out PartyListenerEvent[] updates)
		{
			m_partyAPI.GetPartyListenerEvents(out updates);
		}

		public void ClearPartyListenerEvents()
		{
			m_partyAPI.ClearPartyListenerEvents();
		}

		public void GetFriendsInfo(ref FriendsInfo info)
		{
			m_friendAPI.GetFriendsInfo(ref info);
		}

		public void ClearFriendsUpdates()
		{
			m_friendAPI.ClearFriendsUpdates();
		}

		public void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			m_friendAPI.GetFriendsUpdates(updates);
		}

		public void SendFriendInvite(string sender, string target, bool byEmail)
		{
			m_friendAPI.SendFriendInvite(sender, target, byEmail);
		}

		public void ManageFriendInvite(int action, ulong inviteId)
		{
			m_friendAPI.ManageFriendInvite(action, inviteId);
		}

		public void RemoveFriend(BnetAccountId account)
		{
			m_friendAPI.RemoveFriend(account);
		}

		public void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			m_whisperAPI.SendWhisper(gameAccount, message);
		}

		public void GetWhisperInfo(ref WhisperInfo info)
		{
			m_whisperAPI.GetWhisperInfo(ref info);
		}

		public void GetWhispers([Out] BnetWhisper[] whispers)
		{
			m_whisperAPI.GetWhispers(whispers);
		}

		public void ClearWhispers()
		{
			m_whisperAPI.ClearWhispers();
		}

		public int GetNotificationCount()
		{
			return m_notificationAPI.GetNotificationCount();
		}

		public void GetNotifications([Out] BnetNotification[] notifications)
		{
			m_notificationAPI.GetNotifications(notifications);
		}

		public void ClearNotifications()
		{
			m_notificationAPI.ClearNotifications();
		}

		public void ApplicationWasPaused()
		{
			m_logSource.LogWarning("Application was paused.");
			if (m_rpcConnection != null)
			{
				m_rpcConnection.Update();
			}
		}

		public void ApplicationWasUnpaused()
		{
			m_logSource.LogWarning("Application was unpaused.");
		}

		public bool CheckWebAuth(out string url)
		{
			url = null;
			if (m_challengeAPI != null && InState(ConnectionState.WaitForLogon))
			{
				ExternalChallenge nextExternalChallenge = m_challengeAPI.GetNextExternalChallenge();
				if (nextExternalChallenge != null)
				{
					url = nextExternalChallenge.URL;
					m_logSource.LogDebug("Delivering a challenge url={0}", url);
					return true;
				}
			}
			return false;
		}

		public bool HasExternalChallenge()
		{
			if (m_challengeAPI != null && InState(ConnectionState.WaitForLogon))
			{
				return m_challengeAPI.HasExternalChallenge();
			}
			return false;
		}

		public void ProvideWebAuthToken(string token, RPCContextDelegate callback = null)
		{
			m_logSource.LogDebug("ProvideWebAuthToken token={0}", token);
			if (m_authenticationAPI != null && InState(ConnectionState.WaitForLogon))
			{
				m_authenticationAPI.VerifyWebCredentials(token, callback);
			}
		}

		public void GenerateSSOToken(Action<bool, string> callback)
		{
			if (m_authenticationAPI != null)
			{
				m_authenticationAPI.GenerateSSOToken(callback);
			}
		}

		public void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			if (m_authenticationAPI != null)
			{
				m_authenticationAPI.GenerateAppWebCredentials(callback);
			}
		}

		public void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			if (m_authenticationAPI != null)
			{
				m_authenticationAPI.GenerateWtcgWebCredentials(callback);
			}
		}

		public string FilterProfanity(string unfiltered)
		{
			return m_profanityAPI.FilterProfanity(unfiltered);
		}

		public void SetConnectedRegion(uint region)
		{
			m_connectedRegion = region;
		}

		public void EnqueueErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error, RPCContext context = null)
		{
			if (error != 0)
			{
				LogLevel level = LogLevel.Warning;
				string format = "Enqueuing BattleNetError {0} {1} code={2} packetId={3} system={4} context={5}";
				if (context == null)
				{
					m_logSource.Log(level, format, feature.ToString(), featureEvent.ToString(), new Error(error), "", "", "");
				}
				else
				{
					if (error == BattleNetErrors.ERROR_INTERNAL && context.SystemId == UtilSystemId.BATTLEPAY)
					{
						level = LogLevel.Info;
					}
					m_logSource.Log(level, format, feature.ToString(), featureEvent.ToString(), new Error(error), context.PacketId, (int)context.SystemId, context.Context);
				}
			}
			m_errorEvents.Add(new BnetErrorInfo(feature, featureEvent, error, context?.Context ?? 0));
		}

		private void HandleForceDisconnectRequest(RPCContext context)
		{
			DisconnectNotification disconnectNotification = DisconnectNotification.ParseFrom(context.Payload);
			m_logSource.LogDebug("RPC Called: ForceDisconnect : " + disconnectNotification.ErrorCode);
			EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, (BattleNetErrors)disconnectNotification.ErrorCode, context);
		}

		public bnet.protocol.EntityId GetAccountEntity()
		{
			return m_authenticationAPI.AccountId;
		}

		private void HandleEchoRequest(RPCContext context)
		{
			if (m_rpcConnection == null)
			{
				LogAdapter.Log(LogLevel.Error, "HandleEchoRequest with null RPC Connection");
				return;
			}
			EchoRequest echoRequest = EchoRequest.ParseFrom(context.Payload);
			EchoResponse echoResponse = new EchoResponse();
			if (echoRequest.HasTime)
			{
				echoResponse.SetTime(echoRequest.Time);
			}
			if (echoRequest.HasPayload)
			{
				echoResponse.SetPayload(echoRequest.Payload);
			}
			EchoResponse message = echoResponse;
			m_rpcConnection.QueueResponse(context, message);
			Console.WriteLine("");
			Console.WriteLine("[*]send echo response");
		}

		private void HandleNotificationReceived(RPCContext context)
		{
			Notification notification = bnet.protocol.notification.v1.Notification.ParseFrom(context.Payload);
			m_logSource.LogDebug("Notification: " + notification);
			if (m_notificationHandlers.TryGetValue(notification.Type, out var value))
			{
				value(notification);
			}
			else
			{
				m_logSource.LogWarning("unhandled battle net notification: " + notification.Type);
			}
		}

		public void AuroraStateHandler_Unhandled()
		{
			m_logSource.LogError("Unhandled Aurora State");
		}

		public void AuroraStateHandler_Connect()
		{
		}

		public void AuroraStateHandler_InitRPC()
		{
			m_importedServices.Clear();
			m_exportedServices.Clear();
			ConnectRequest connectRequest = new ConnectRequest();
			m_importedServices.Add(m_authenticationAPI.AuthServerService);
			m_importedServices.Add(m_gamesAPI.GameUtilityService);
			m_importedServices.Add(m_gamesAPI.GameRequestService);
			m_importedServices.Add(m_notificationService);
			m_importedServices.Add(m_presenceAPI.PresenceService);
			m_importedServices.Add(m_channelAPI.ChannelService);
			m_importedServices.Add(m_channelAPI.ChannelMembershipService);
			m_importedServices.Add(m_friendAPI.FriendsService);
			m_importedServices.Add(m_accountAPI.AccountService);
			m_importedServices.Add(m_resourcesAPI.ResourcesService);
			m_exportedServices.Add(m_authenticationAPI.AuthClientService);
			m_exportedServices.Add(m_gamesAPI.GameRequestListener);
			m_exportedServices.Add(m_notificationListenerService);
			m_exportedServices.Add(m_channelAPI.ChannelListener);
			m_exportedServices.Add(m_channelAPI.ChannelMembershipListener);
			m_exportedServices.Add(m_presenceAPI.ChannelSubscriberService);
			m_exportedServices.Add(m_friendAPI.FriendsNotifyService);
			m_exportedServices.Add(m_challengeAPI.ChallengeNotifyService);
			m_exportedServices.Add(m_accountAPI.AccountNotifyService);
			m_exportedServices.Add(m_sessionAPI.SessionNotifyService);
			connectRequest.SetBindRequest(CreateBindRequest(m_importedServices, m_exportedServices));
			connectRequest.SetUseBindlessRpc(val: true);
			m_rpcConnection.QueueRequest(m_connectionService, 1u, connectRequest, OnConnectCallback);
			SwitchToState(ConnectionState.WaitForInitRPC);
		}

		private void OnConnectCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			m_logSource.LogDebug("RPC Connected, error : " + status);
			if (status != 0)
			{
				SwitchToState(ConnectionState.Error);
				EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnConnected, status, context);
				return;
			}
			ConnectResponse connectResponse = ConnectResponse.ParseFrom(context.Payload);
			if (connectResponse.HasServerTime)
			{
				m_serverTimeUTCAtConnectMicroseconds = (long)connectResponse.ServerTime;
				m_serverTimeDeltaUTCSeconds = m_serverTimeUTCAtConnectMicroseconds / 1000000 - GetCurrentTimeSecondsSinceUnixEpoch();
			}
			if (connectResponse.HasBindResult && connectResponse.HasBindResponse && connectResponse.BindResult == 0)
			{
				m_logSource.LogDebug("RPC Connected, Bind Result : {0} Bind Response {1}", connectResponse.BindResult, connectResponse.BindResponse.ImportedServiceIdCount);
				int num = 0;
				foreach (uint importedServiceId in connectResponse.BindResponse.ImportedServiceIdList)
				{
					ServiceDescriptor serviceDescriptor = m_importedServices[num++];
					m_logSource.LogDebug("Importing service oldId={0} newId={1} name={2}", serviceDescriptor.Id, importedServiceId, serviceDescriptor.Name);
					serviceDescriptor.Id = importedServiceId;
					m_rpcConnection.GetServiceHelper().AddImportedService(serviceDescriptor);
				}
				if (connectResponse.HasContentHandleArray)
				{
					if (!m_clientInterface.GetDisableConnectionMetering())
					{
						m_rpcConnection.SetConnectionMeteringContentHandles(connectResponse.ContentHandleArray, m_localStorageAPI);
					}
					else
					{
						m_logSource.LogWarning("Connection metering disabled by configuration.");
					}
				}
				else
				{
					m_logSource.LogDebug("Connection response had not connection metering request");
				}
				m_logSource.LogDebug("FRONT ServerId={0:x2}", connectResponse.ServerId.Label);
				InitRPCListeners();
				PrintBindServiceResponse(connectResponse.BindResponse);
				SwitchToState(ConnectionState.Logon);
			}
			else
			{
				m_logSource.LogWarning("BindRequest failed with error={0}", connectResponse.BindResult);
				SwitchToState(ConnectionState.Error);
			}
		}

		public void AuroraStateHandler_WaitForInitRPC()
		{
		}

		public void AuroraStateHandler_Logon()
		{
			m_logSource.LogDebug("Sending Logon request");
			LogonRequest message = CreateLogonRequest();
			m_rpcConnection.QueueRequest(m_authenticationAPI.AuthServerService, 1u, message, OnLogonCallback);
			SwitchToState(ConnectionState.WaitForLogon);
		}

		private void OnLogonCallback(RPCContext context)
		{
			m_logSource.LogDebug("Logon Complete. Context = {0}", context.Request.ToString());
		}

		public void AuroraStateHandler_WaitForLogon()
		{
			if (m_authenticationAPI.AuthenticationFailure())
			{
				EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, BattleNetErrors.ERROR_NO_AUTH);
			}
		}

		public void AuroraStateHandler_WaitForGameAccountSelect()
		{
		}

		public void IssueSelectGameAccountRequest()
		{
			m_rpcConnection.QueueRequest(m_authenticationAPI.AuthServerService, 4u, GameAccountId, OnSelectGameAccountCallback);
			SwitchToState(ConnectionState.WaitForGameAccountSelect);
		}

		private void OnSelectGameAccountCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status == BattleNetErrors.ERROR_OK)
			{
				SwitchToState(ConnectionState.WaitForAPIToInitialize);
				foreach (BattleNetAPI api in m_apiList)
				{
					api.Initialize();
					api.OnGameAccountSelected();
				}
			}
			else
			{
				SwitchToState(ConnectionState.Error);
				EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_GameAccountSelected, status, context);
				m_logSource.LogError("Failed to select a game account status = {0}", status.ToString());
			}
		}

		public void AuroraStateHandler_WaitForAPIToInitialize()
		{
			if (m_friendAPI.IsInitialized && m_sessionAPI.IsInitialized)
			{
				SwitchToState(ConnectionState.Ready);
			}
		}

		public void AuroraStateHandler_Ready()
		{
			ChannelMembershipSubscribe();
		}

		public void AuroraStateHandler_Disconnected()
		{
			m_logSource.LogWarning("Client disconnected from Aurora");
		}

		public BattleNetLogSource GetLogSource()
		{
			return m_logSource;
		}

		private bool InState(ConnectionState state)
		{
			return m_connectionState == state;
		}

		private bool SwitchToState(ConnectionState state)
		{
			if (state == m_connectionState)
			{
				return false;
			}
			bool flag = true;
			if (state != 0 || m_connectionState != ConnectionState.Ready)
			{
				flag = state > m_connectionState;
			}
			if (flag)
			{
				m_logSource.LogDebug("Expected state change {0} -> {1}", m_connectionState.ToString(), state.ToString());
			}
			else
			{
				m_logSource.LogWarning("Unexpected state changes {0} -> {1}", m_connectionState.ToString(), state.ToString());
				m_logSource.LogDebugStackTrace("SwitchToState", 5);
			}
			m_connectionState = state;
			return true;
		}

		private BindRequest CreateBindRequest(List<ServiceDescriptor> imports, List<ServiceDescriptor> exports)
		{
			BindRequest bindRequest = new BindRequest();
			uint num = 0u;
			foreach (ServiceDescriptor import in imports)
			{
				num = (import.Id = num + 1);
				BoundService boundService = new BoundService();
				boundService.SetId(import.Id);
				boundService.SetHash(import.Hash);
				bindRequest.AddImportedService(boundService);
				m_rpcConnection.GetServiceHelper().AddImportedService(import);
				m_logSource.LogDebug("Importing service id={0} name={1} hash={2}", import.Id, import.Name, import.Hash);
			}
			foreach (ServiceDescriptor export in exports)
			{
				num = (export.Id = num + 1);
				BoundService boundService2 = new BoundService();
				boundService2.SetId(export.Id);
				boundService2.SetHash(export.Hash);
				bindRequest.AddExportedService(boundService2);
				m_rpcConnection.GetServiceHelper().AddExportedService(export);
				m_logSource.LogDebug("Exporting service id={0} name={1} hash={2}", export.Id, export.Name, export.Hash);
			}
			return bindRequest;
		}

		private LogonRequest CreateLogonRequest()
		{
			LogonRequest logonRequest = new LogonRequest();
			logonRequest.SetProgram("WTCG");
			logonRequest.SetLocale(Client().GetLocaleName());
			logonRequest.SetPlatform(Client().GetPlatformName());
			logonRequest.SetVersion(Client().GetAuroraVersionName());
			logonRequest.SetApplicationVersion(Client().GetApplicationVersion());
			logonRequest.SetPublicComputer(val: false);
			logonRequest.SetAllowLogonQueueNotifications(val: true);
			string userAgent = Client().GetUserAgent();
			if (!string.IsNullOrEmpty(userAgent))
			{
				logonRequest.SetUserAgent(userAgent);
			}
			bool flag = false;
			flag = true;
			m_logSource.LogDebug("CreateLogonRequest SSL={0}", flag);
			if (!string.IsNullOrEmpty(m_userEmailAddress))
			{
				m_logSource.LogDebug("Email = {0}", m_userEmailAddress);
			}
			return logonRequest;
		}

		public void OnBroadcastReceived(IList<bnet.protocol.Attribute> AttributeList)
		{
			foreach (bnet.protocol.Attribute Attribute in AttributeList)
			{
				if (Attribute.Name == "shutdown")
				{
					m_shutdownInMinutes = (int)Attribute.Value.IntValue;
				}
			}
		}

		private void PrintBindServiceResponse(BindResponse response)
		{
			string text = "BindResponse: { ";
			int importedServiceIdCount = response.ImportedServiceIdCount;
			text = text + "Num Imported Services: " + importedServiceIdCount;
			text += " [";
			for (int i = 0; i < importedServiceIdCount; i++)
			{
				text = text + " Id:" + response.ImportedServiceId[i];
			}
			text += " ]";
			text += " }";
			m_logSource.LogDebug(text);
		}
	}
}
