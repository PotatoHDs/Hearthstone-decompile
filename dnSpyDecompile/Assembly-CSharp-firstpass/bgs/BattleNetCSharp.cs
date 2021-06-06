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
	// Token: 0x02000214 RID: 532
	public class BattleNetCSharp : IBattleNet
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x00076AAE File Offset: 0x00074CAE
		public FriendsAPI Friends
		{
			get
			{
				return this.m_friendAPI;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x00076AB6 File Offset: 0x00074CB6
		public PresenceAPI Presence
		{
			get
			{
				return this.m_presenceAPI;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x00076ABE File Offset: 0x00074CBE
		public ChannelAPI Channel
		{
			get
			{
				return this.m_channelAPI;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00076AC6 File Offset: 0x00074CC6
		public GamesAPI Games
		{
			get
			{
				return this.m_gamesAPI;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x00076ACE File Offset: 0x00074CCE
		public PartyAPI Party
		{
			get
			{
				return this.m_partyAPI;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00076AD6 File Offset: 0x00074CD6
		public ChallengeAPI Challenge
		{
			get
			{
				return this.m_challengeAPI;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00076ADE File Offset: 0x00074CDE
		public WhisperAPI Whisper
		{
			get
			{
				return this.m_whisperAPI;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00076AE6 File Offset: 0x00074CE6
		public NotificationAPI Notification
		{
			get
			{
				return this.m_notificationAPI;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00076AEE File Offset: 0x00074CEE
		public BroadcastAPI Broadcast
		{
			get
			{
				return this.m_broadcastAPI;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00076AF6 File Offset: 0x00074CF6
		public AccountAPI Account
		{
			get
			{
				return this.m_accountAPI;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x00076AFE File Offset: 0x00074CFE
		public SessionAPI Session
		{
			get
			{
				return this.m_sessionAPI;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x00076B06 File Offset: 0x00074D06
		public ResourcesAPI Resources
		{
			get
			{
				return this.m_resourcesAPI;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x00076B0E File Offset: 0x00074D0E
		public LocalStorageAPI LocalStorage
		{
			get
			{
				return this.m_localStorageAPI;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x00076B16 File Offset: 0x00074D16
		public long ServerTimeUTCAtConnectMicroseconds
		{
			get
			{
				return this.m_serverTimeUTCAtConnectMicroseconds;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x00076B1E File Offset: 0x00074D1E
		public long CurrentUTCServerTimeSeconds
		{
			get
			{
				return this.GetCurrentTimeSecondsSinceUnixEpoch() + this.m_serverTimeDeltaUTCSeconds;
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x00076B30 File Offset: 0x00074D30
		public long GetCurrentTimeSecondsSinceUnixEpoch()
		{
			return (long)(DateTime.UtcNow - this.m_unixEpoch).TotalSeconds;
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00076B58 File Offset: 0x00074D58
		public double GetRealTimeSinceStartup()
		{
			return this.m_stopwatch.Elapsed.TotalSeconds;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00076B78 File Offset: 0x00074D78
		public ICompressionProvider CompressionProvider
		{
			get
			{
				return this.m_compressionProvider;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060021B6 RID: 8630 RVA: 0x00076B80 File Offset: 0x00074D80
		// (remove) Token: 0x060021B7 RID: 8631 RVA: 0x00076BB8 File Offset: 0x00074DB8
		public event Action<BattleNetErrors> OnConnected;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060021B8 RID: 8632 RVA: 0x00076BF0 File Offset: 0x00074DF0
		// (remove) Token: 0x060021B9 RID: 8633 RVA: 0x00076C28 File Offset: 0x00074E28
		public event Action<BattleNetErrors> OnDisconnected;

		// Token: 0x060021BA RID: 8634 RVA: 0x00076C60 File Offset: 0x00074E60
		public BattleNetCSharp(ClientInterface clientInterface, IRpcConnectionFactory rpcConnectionFactory, ICompressionProvider compressionProvider, IFileUtil fileUtil, IJsonSerializer jsonSerializer, LoggerInterface loggerInterface, ISocketEventListener socketEventListener)
		{
			this.m_clientInterface = clientInterface;
			this.m_rpcConnectionFactory = rpcConnectionFactory;
			this.m_compressionProvider = compressionProvider;
			this.m_fileUtil = fileUtil;
			this.m_jsonSerializer = jsonSerializer;
			this.m_socketEventListener = socketEventListener;
			LogAdapter.SetLogger(loggerInterface);
			this.m_notificationHandlers = new Dictionary<string, BattleNetCSharp.NotificationHandler>();
			this.m_stateHandlers = new Dictionary<BattleNetCSharp.ConnectionState, BattleNetCSharp.AuroraStateHandler>();
			this.m_importedServices = new List<ServiceDescriptor>();
			this.m_exportedServices = new List<ServiceDescriptor>();
			this.m_apiList = new List<BattleNetAPI>();
			this.m_bnetEvents = new List<BattleNet.BnetEvent>();
			this.m_friendAPI = new FriendsAPI(this);
			this.m_presenceAPI = new PresenceAPI(this);
			this.m_channelAPI = new ChannelAPI(this);
			this.m_gamesAPI = new GamesAPI(this);
			this.m_partyAPI = new PartyAPI(this);
			this.m_challengeAPI = new ChallengeAPI(this);
			this.m_whisperAPI = new WhisperAPI(this);
			this.m_notificationAPI = new NotificationAPI(this);
			this.m_broadcastAPI = new BroadcastAPI(this);
			this.m_accountAPI = new AccountAPI(this);
			this.m_authenticationAPI = new AuthenticationAPI(this);
			this.m_localStorageAPI = new LocalStorageAPI(this);
			this.m_resourcesAPI = new ResourcesAPI(this);
			this.m_profanityAPI = new ProfanityAPI(this);
			this.m_sessionAPI = new SessionAPI(this);
			this.m_notificationHandlers.Add("WHISPER", new BattleNetCSharp.NotificationHandler(this.m_whisperAPI.OnWhisper));
			this.m_notificationHandlers.Add("WHISPER_ECHO", new BattleNetCSharp.NotificationHandler(this.m_whisperAPI.OnWhisperEcho));
			this.m_notificationHandlers.Add("BROADCAST", new BattleNetCSharp.NotificationHandler(this.m_broadcastAPI.OnBroadcast));
			this.m_notificationHandlers.Add("WTCG.UtilNotificationMessage", delegate(Notification n)
			{
				this.m_notificationAPI.OnNotification("WTCG.UtilNotificationMessage", n);
			});
			this.m_broadcastAPI.RegisterListener(new BroadcastAPI.BroadcastCallback(this.OnBroadcastReceived));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.Connect, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_Connect));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.InitRPC, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_InitRPC));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.WaitForInitRPC, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_WaitForInitRPC));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.Logon, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_Logon));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.WaitForLogon, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_WaitForLogon));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.WaitForGameAccountSelect, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_WaitForGameAccountSelect));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.WaitForAPIToInitialize, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_WaitForAPIToInitialize));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.Ready, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_Ready));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.Disconnected, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_Disconnected));
			this.m_stateHandlers.Add(BattleNetCSharp.ConnectionState.Error, new BattleNetCSharp.AuroraStateHandler(this.AuroraStateHandler_Unhandled));
			this.m_apiList.Add(this.m_friendAPI);
			this.m_apiList.Add(this.m_presenceAPI);
			this.m_apiList.Add(this.m_channelAPI);
			this.m_apiList.Add(this.m_gamesAPI);
			this.m_apiList.Add(this.m_partyAPI);
			this.m_apiList.Add(this.m_challengeAPI);
			this.m_apiList.Add(this.m_whisperAPI);
			this.m_apiList.Add(this.m_notificationAPI);
			this.m_apiList.Add(this.m_broadcastAPI);
			this.m_apiList.Add(this.m_accountAPI);
			this.m_apiList.Add(this.m_authenticationAPI);
			this.m_apiList.Add(this.m_localStorageAPI);
			this.m_apiList.Add(this.m_resourcesAPI);
			this.m_apiList.Add(this.m_profanityAPI);
			this.m_apiList.Add(this.m_sessionAPI);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000770AA File Offset: 0x000752AA
		public bool IsInitialized()
		{
			return this.m_initialized;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000770B4 File Offset: 0x000752B4
		public bool Init(bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			if (this.m_initialized)
			{
				return true;
			}
			this.m_stopwatch.Reset();
			this.m_stopwatch.Start();
			this.m_auroraEnvironment = targetServer;
			this.m_auroraPort = ((port <= 0U) ? 1119U : port);
			this.m_userEmailAddress = userEmailAddress;
			bool flag = false;
			try
			{
				string text;
				flag = UriUtils.GetHostAddress(targetServer, out text);
			}
			catch (Exception ex)
			{
				this.m_logSource.LogError(string.Format("Exception within GetHostAddress for target server {0} : {1}", targetServer, ex.Message));
			}
			if (flag)
			{
				this.m_initialized = true;
				this.ConnectAurora(targetServer, this.m_auroraPort, sslParams);
			}
			else
			{
				LogAdapter.Log(LogLevel.Fatal, "GLOBAL_ERROR_NETWORK_NO_CONNECTION", "");
			}
			return this.m_initialized;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00077170 File Offset: 0x00075370
		public ClientInterface Client()
		{
			return this.m_clientInterface;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x00077178 File Offset: 0x00075378
		public void AppQuit()
		{
			this.RequestCloseAurora();
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00077180 File Offset: 0x00075380
		public bool IsConnected()
		{
			return this.m_rpcConnection != null && this.InState(BattleNetCSharp.ConnectionState.Ready);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x00077198 File Offset: 0x00075398
		public void ConnectAurora(string address, uint port, SslParameters sslParams)
		{
			this.m_logSource.LogInfo("Sending connection request to {0}:{1}", new object[]
			{
				address,
				port
			});
			this.m_logSource.LogDebug("Aurora version is {0}", new object[]
			{
				this.GetVersion()
			});
			this.m_logSource.LogDebug("Connection Request to '{0}:{1}' with Version '{2}'", new object[]
			{
				address,
				port,
				this.GetVersion()
			});
			this.m_rpcConnection = this.m_rpcConnectionFactory.CreateRpcConnection(this.m_fileUtil, this.m_jsonSerializer, this.m_socketEventListener);
			this.m_connectionService.Id = 0U;
			this.m_rpcConnection.GetServiceHelper().AddImportedService(this.m_connectionService);
			this.m_rpcConnection.GetServiceHelper().AddExportedService(this.m_connectionService);
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_connectionService.Id, 4U, new RPCContextDelegate(this.HandleForceDisconnectRequest));
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_connectionService.Id, 3U, new RPCContextDelegate(this.HandleEchoRequest));
			this.m_rpcConnection.SetOnConnectHandler(new bgs.OnConnectHandler(this.OnConnectHandlerCallback));
			this.m_rpcConnection.SetOnDisconnectHandler(new OnDisconnectHandler(this.OnDisconectHandlerCallback));
			this.m_rpcConnection.Connect(address, port, sslParams, this.m_retryAttemptCount);
			this.SwitchToState(BattleNetCSharp.ConnectionState.InitRPC);
			this.m_attemptTime = DateTime.Now;
			this.m_connectAuroraAddress = address;
			this.m_connectAuroraPort = port;
			this.m_connectAuororaSslParams = sslParams;
			this.m_hasSuccessfullyConnected = false;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00077328 File Offset: 0x00075528
		public void OnConnectHandlerCallback(BattleNetErrors error)
		{
			if (this.OnConnected != null)
			{
				this.OnConnected(error);
			}
			if (error != BattleNetErrors.ERROR_OK)
			{
				this.SwitchToState(BattleNetCSharp.ConnectionState.Error);
				this.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnConnected, error, null);
			}
			else
			{
				this.m_logSource.LogDebug("ConnectAurora sucessful after {0}ms. Stopping any retry attempts.", new object[]
				{
					DateTime.Now.Subtract(this.m_attemptTime).Milliseconds
				});
				this.m_hasSuccessfullyConnected = true;
				this.m_retryAttemptCount = 0;
			}
			foreach (BattleNetAPI battleNetAPI in this.m_apiList)
			{
				battleNetAPI.OnConnected(error);
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000773F0 File Offset: 0x000755F0
		private void OnDisconectHandlerCallback(BattleNetErrors error)
		{
			if (this.OnDisconnected != null)
			{
				this.OnDisconnected(error);
			}
			BattleNet.Log.LogInfo("Disconnected: code={0} {1}", new object[]
			{
				(int)error,
				error.ToString()
			});
			this.SwitchToState(BattleNetCSharp.ConnectionState.Disconnected);
			if (error != BattleNetErrors.ERROR_OK)
			{
				this.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, error, null);
			}
			this.m_bnetEvents.Add(BattleNet.BnetEvent.Disconnected);
			foreach (BattleNetAPI battleNetAPI in this.m_apiList)
			{
				battleNetAPI.OnDisconnected();
			}
			this.m_hasSuccessfullyConnected = false;
			this.m_initialized = false;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000774B4 File Offset: 0x000756B4
		private void InitRPCListeners()
		{
			this.m_rpcConnection.RegisterServiceMethodListener(this.m_notificationListenerService.Id, 1U, new RPCContextDelegate(this.HandleNotificationReceived));
			foreach (BattleNetAPI battleNetAPI in this.m_apiList)
			{
				battleNetAPI.InitRPCListeners(this.m_rpcConnection);
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x00077530 File Offset: 0x00075730
		public void CloseAurora()
		{
			BattleNet.Log.LogError("CloseAurora() is deprecated in BattleNetCSharp. Use RequestCloseAurora() instead.");
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x00077541 File Offset: 0x00075741
		public void RequestCloseAurora()
		{
			if (this.m_rpcConnection != null)
			{
				this.m_rpcConnection.Disconnect();
			}
			this.SwitchToState(BattleNetCSharp.ConnectionState.Disconnected);
			this.m_initialized = false;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void QueryAurora()
		{
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00077568 File Offset: 0x00075768
		public void ProcessAurora()
		{
			if (!this.m_hasSuccessfullyConnected)
			{
				int seconds = this.ConnectAuroraAttemptRateByAttemptCount[Math.Min(this.m_retryAttemptCount, this.ConnectAuroraAttemptRateByAttemptCount.Length - 1)];
				TimeSpan t = DateTime.Now.Subtract(this.m_attemptTime);
				if (t > new TimeSpan(0, 0, seconds))
				{
					this.m_attemptTime = DateTime.Now;
					this.m_retryAttemptCount++;
					this.m_logSource.LogDebug("ConnectAurora failed, attempting to retry: address={0} port={1} attempt={2} waitTime={3}ms", new object[]
					{
						this.m_connectAuroraAddress,
						this.m_connectAuroraPort,
						this.m_retryAttemptCount,
						t.Milliseconds
					});
					this.ConnectAurora(this.m_connectAuroraAddress, this.m_connectAuroraPort, this.m_connectAuororaSslParams);
					return;
				}
			}
			if (this.InState(BattleNetCSharp.ConnectionState.Disconnected) || this.InState(BattleNetCSharp.ConnectionState.Error))
			{
				return;
			}
			if (this.m_rpcConnection != null)
			{
				this.m_rpcConnection.Update();
				if (this.m_rpcConnection.GetMillisecondsSinceLastPacketSent() > this.m_keepAliveIntervalMilliseconds)
				{
					this.m_rpcConnection.QueueRequest(this.m_connectionService, 5U, new NoData(), null, 0U);
				}
			}
			BattleNetCSharp.AuroraStateHandler auroraStateHandler;
			if (this.m_stateHandlers.TryGetValue(this.m_connectionState, out auroraStateHandler))
			{
				auroraStateHandler();
			}
			else
			{
				this.m_logSource.LogError("Missing state handler");
			}
			for (int i = 0; i < this.m_apiList.Count; i++)
			{
				this.m_apiList[i].Process();
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000776EA File Offset: 0x000758EA
		public string GetLaunchOption(string key, bool encrypted)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted, LaunchOptionHelper.ProgramCodeWtcg);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x000776F8 File Offset: 0x000758F8
		private string GetLaunchOption(string key, bool encrypted, string programCode)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted, programCode);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x00077704 File Offset: 0x00075904
		public string GetConnectionString()
		{
			string launchOption = this.GetLaunchOption("CONNECTION_STRING", false, LaunchOptionHelper.ProgramCodeWtcg);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			string launchOption2 = this.GetLaunchOption("REGION", false, LaunchOptionHelper.ProgramCodeWtcg);
			if (string.IsNullOrEmpty(launchOption2))
			{
				return null;
			}
			string key = string.Format("CONNECTION_STRING_{0}", launchOption2);
			launchOption = this.GetLaunchOption(key, false, LaunchOptionHelper.ProgramCodeWtcg);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			launchOption = this.GetLaunchOption(key, false, LaunchOptionHelper.ProgramCodeBna);
			if (!string.IsNullOrEmpty(launchOption))
			{
				return launchOption;
			}
			return null;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00077786 File Offset: 0x00075986
		public constants.BnetRegion GetAccountRegion()
		{
			if (this.Account.GetPreferredRegion() == 4294967295U)
			{
				return constants.BnetRegion.REGION_UNINITIALIZED;
			}
			return (constants.BnetRegion)this.Account.GetPreferredRegion();
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x000777A3 File Offset: 0x000759A3
		public string GetAccountCountry()
		{
			return this.Account.GetAccountCountry();
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000777B0 File Offset: 0x000759B0
		public bool IsHeadlessAccount()
		{
			return this.Account.IsHeadlessAccount();
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000777BD File Offset: 0x000759BD
		public constants.BnetRegion GetCurrentRegion()
		{
			if (this.m_connectedRegion == 4294967295U)
			{
				return constants.BnetRegion.REGION_UNINITIALIZED;
			}
			return (constants.BnetRegion)this.m_connectedRegion;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000777D0 File Offset: 0x000759D0
		public void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			this.Account.GetPlayRestrictions(ref restrictions, reload);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x000777DF File Offset: 0x000759DF
		public void GetAccountState(BnetAccountId bnetAccount)
		{
			this.Account.GetAccountState(bnetAccount, null);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000777EE File Offset: 0x000759EE
		public int GetShutdownMinutes()
		{
			return this.m_shutdownInMinutes;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000777F8 File Offset: 0x000759F8
		public int BattleNetStatus()
		{
			switch (this.m_connectionState)
			{
			case BattleNetCSharp.ConnectionState.Disconnected:
				return 5;
			case BattleNetCSharp.ConnectionState.Connect:
			case BattleNetCSharp.ConnectionState.InitRPC:
			case BattleNetCSharp.ConnectionState.WaitForInitRPC:
			case BattleNetCSharp.ConnectionState.Logon:
			case BattleNetCSharp.ConnectionState.WaitForLogon:
			case BattleNetCSharp.ConnectionState.WaitForGameAccountSelect:
			case BattleNetCSharp.ConnectionState.WaitForAPIToInitialize:
				return 1;
			case BattleNetCSharp.ConnectionState.Ready:
				return 4;
			case BattleNetCSharp.ConnectionState.Error:
				return 3;
			default:
				this.m_logSource.LogError("Unknown Battle.Net Status");
				return 0;
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00077855 File Offset: 0x00075A55
		public void GetQueueInfo(ref QueueInfo queueInfo)
		{
			this.m_authenticationAPI.GetQueueInfo(ref queueInfo);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00077863 File Offset: 0x00075A63
		public AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest()
		{
			return this.m_authenticationAPI.NextMemModuleRequest();
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00077870 File Offset: 0x00075A70
		public void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			this.m_authenticationAPI.SendMemModuleResponse(request, memModuleResponseBytes);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0007787F File Offset: 0x00075A7F
		public void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			this.m_gamesAPI.SendUtilPacket(packetId, systemId, bytes, size, subID, context, route);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00077897 File Offset: 0x00075A97
		public GamesAPI.UtilResponse NextUtilPacket()
		{
			return this.m_gamesAPI.NextUtilPacket();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x000778A4 File Offset: 0x00075AA4
		public int GetErrorsCount()
		{
			return this.m_errorEvents.Count;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000778B1 File Offset: 0x00075AB1
		public void GetErrors([Out] BnetErrorInfo[] errors)
		{
			this.m_errorEvents.Sort(new BattleNetCSharp.BnetErrorComparer());
			this.m_errorEvents.CopyTo(errors);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000778CF File Offset: 0x00075ACF
		public void ClearErrors()
		{
			this.m_errorEvents.Clear();
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000778DC File Offset: 0x00075ADC
		public bgs.types.EntityId GetMyGameAccountId()
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			if (this.GameAccountId != null)
			{
				result.hi = this.GameAccountId.High;
				result.lo = this.GameAccountId.Low;
			}
			return result;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x0007791E File Offset: 0x00075B1E
		public GameAccountHandle GetMyGameAccountHandle()
		{
			return BnetEntityId.CreateFromEntityId(this.GetMyGameAccountId()).GetGameAccountHandle();
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00077930 File Offset: 0x00075B30
		public bgs.types.EntityId GetMyAccountId()
		{
			bgs.types.EntityId result = default(bgs.types.EntityId);
			if (this.AccountId != null)
			{
				result.hi = this.AccountId.High;
				result.lo = this.AccountId.Low;
			}
			return result;
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00077972 File Offset: 0x00075B72
		public string GetVersion()
		{
			return this.m_clientInterface.GetVersion();
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0007797F File Offset: 0x00075B7F
		public int GetDataVersion()
		{
			return this.m_clientInterface.GetDataVersion();
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0007798C File Offset: 0x00075B8C
		public string GetUserEmailAddress()
		{
			return this.m_userEmailAddress;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00077994 File Offset: 0x00075B94
		public string GetEnvironment()
		{
			return this.m_auroraEnvironment;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x0007799C File Offset: 0x00075B9C
		public uint GetPort()
		{
			return this.m_auroraPort;
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000779A4 File Offset: 0x00075BA4
		public int GetBnetEventsSize()
		{
			return this.m_bnetEvents.Count;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x000779B1 File Offset: 0x00075BB1
		public void ClearBnetEvents()
		{
			this.m_bnetEvents.Clear();
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x000779BE File Offset: 0x00075BBE
		public void GetBnetEvents([Out] BattleNet.BnetEvent[] bnetEvents)
		{
			this.m_bnetEvents.CopyTo(bnetEvents);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000779CC File Offset: 0x00075BCC
		public void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			this.m_gamesAPI.QueueMatchmaking(matchmakerAttributesFilter, gameAttributes, players);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000779DC File Offset: 0x00075BDC
		public void CancelMatchmaking()
		{
			this.m_gamesAPI.CancelMatchmaking();
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x000779E9 File Offset: 0x00075BE9
		public QueueEvent GetQueueEvent()
		{
			return this.m_gamesAPI.GetQueueEvent();
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000779F6 File Offset: 0x00075BF6
		public int PresenceSize()
		{
			return this.m_presenceAPI.PresenceSize();
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00077A03 File Offset: 0x00075C03
		public void ClearPresence()
		{
			this.m_presenceAPI.ClearPresence();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00077A10 File Offset: 0x00075C10
		public void GetPresence([Out] PresenceUpdate[] updates)
		{
			this.m_presenceAPI.GetPresence(updates);
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00077A1E File Offset: 0x00075C1E
		public void SetPresenceBool(uint field, bool val)
		{
			this.m_presenceAPI.SetPresenceBool(field, val);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00077A2D File Offset: 0x00075C2D
		public void SetAccountLevelPresenceBool(uint field, bool val)
		{
			this.m_presenceAPI.SetAccountLevelPresenceBool(field, val);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00077A3C File Offset: 0x00075C3C
		public void SetPresenceInt(uint field, long val)
		{
			this.m_presenceAPI.SetPresenceInt(field, val);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00077A4B File Offset: 0x00075C4B
		public void SetPresenceString(uint field, string val)
		{
			this.m_presenceAPI.SetPresenceString(field, val);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00077A5A File Offset: 0x00075C5A
		public void SetPresenceBlob(uint field, byte[] val)
		{
			this.m_presenceAPI.SetPresenceBlob(field, val);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00077A69 File Offset: 0x00075C69
		public void SetPresenceEntityId(uint field, bgs.types.EntityId val)
		{
			this.m_presenceAPI.SetPresenceEntityId(field, val);
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00077A78 File Offset: 0x00075C78
		public void SetRichPresence([In] RichPresenceUpdate[] updates)
		{
			this.m_presenceAPI.PublishRichPresence(updates);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00077A86 File Offset: 0x00075C86
		public void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			this.m_presenceAPI.RequestPresenceFields(isGameAccountEntityId, entityId, fieldList);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00077A96 File Offset: 0x00075C96
		public void PresenceSubscribe(bgs.types.EntityId entityId)
		{
			this.m_presenceAPI.PresenceSubscribe(entityId.ToProtocol());
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00077AAA File Offset: 0x00075CAA
		public void PresenceUnsubscribe(bgs.types.EntityId entityId)
		{
			this.m_presenceAPI.PresenceUnsubscribe(entityId.ToProtocol());
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00077ABE File Offset: 0x00075CBE
		public void ChannelMembershipSubscribe()
		{
			if (!this.m_isSubscribedToMembershipEvents)
			{
				this.m_channelAPI.SubscribeMembership();
				this.m_isSubscribedToMembershipEvents = true;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00077ADA File Offset: 0x00075CDA
		public bool IsSubscribedToEntity(bgs.types.EntityId entityId)
		{
			return this.m_presenceAPI.IsSubscribedToEntity(entityId.ToProtocol());
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00077AEE File Offset: 0x00075CEE
		public void CreateParty(string szPartyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			this.m_partyAPI.CreateParty(szPartyType, privacyLevel, partyAttributes);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00077AFE File Offset: 0x00075CFE
		public void JoinParty(bgs.types.EntityId partyId, string szPartyType)
		{
			this.m_partyAPI.JoinParty(PartyId.FromEntityId(partyId).ToChannelId(), szPartyType);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00077B17 File Offset: 0x00075D17
		public void LeaveParty(bgs.types.EntityId partyId)
		{
			this.m_partyAPI.LeaveParty(PartyId.FromEntityId(partyId).ToChannelId());
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00077B2F File Offset: 0x00075D2F
		public void DissolveParty(bgs.types.EntityId partyId)
		{
			this.m_partyAPI.DissolveParty(PartyId.FromEntityId(partyId).ToChannelId());
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00077B47 File Offset: 0x00075D47
		public void SetPartyPrivacy(bgs.types.EntityId partyId, int privacyLevel)
		{
			this.m_partyAPI.SetPartyPrivacy(PartyId.FromEntityId(partyId).ToChannelId(), privacyLevel);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00077B60 File Offset: 0x00075D60
		public void AssignPartyRole(bgs.types.EntityId partyId, bgs.types.EntityId memberId, uint roleId)
		{
			this.m_partyAPI.AssignPartyRole(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(memberId).GetGameAccountHandle(), roleId);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00077B84 File Offset: 0x00075D84
		public void SendPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId inviteeId, bool isReservation)
		{
			this.m_partyAPI.SendPartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(inviteeId).GetGameAccountHandle(), isReservation);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00077BA8 File Offset: 0x00075DA8
		public void AcceptPartyInvite(ulong invitationId)
		{
			this.m_partyAPI.AcceptPartyInvite(invitationId);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00077BB6 File Offset: 0x00075DB6
		public void DeclinePartyInvite(ulong invitationId)
		{
			this.m_partyAPI.DeclinePartyInvite(invitationId);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00077BC4 File Offset: 0x00075DC4
		public void RevokePartyInvite(bgs.types.EntityId partyId, ulong invitationId)
		{
			this.m_partyAPI.RevokePartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), invitationId);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00077BDD File Offset: 0x00075DDD
		public void RequestPartyInvite(bgs.types.EntityId partyId, bgs.types.EntityId whomToAskForApproval, bgs.types.EntityId whomToInvite, string szPartyType)
		{
			this.m_partyAPI.RequestPartyInvite(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(whomToAskForApproval).GetGameAccountHandle(), BnetGameAccountId.CreateFromEntityId(whomToInvite).GetGameAccountHandle());
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00077C0B File Offset: 0x00075E0B
		public void IgnoreInviteRequest(bgs.types.EntityId partyId, bgs.types.EntityId requestedTargetId)
		{
			this.m_partyAPI.IgnoreInviteRequest(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(requestedTargetId).GetGameAccountHandle());
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00077C2E File Offset: 0x00075E2E
		public void KickPartyMember(bgs.types.EntityId partyId, bgs.types.EntityId memberId)
		{
			this.m_partyAPI.KickPartyMember(PartyId.FromEntityId(partyId).ToChannelId(), BnetGameAccountId.CreateFromEntityId(memberId).GetGameAccountHandle());
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00077C51 File Offset: 0x00075E51
		public void SendPartyChatMessage(bgs.types.EntityId partyId, string message)
		{
			this.m_partyAPI.SendPartyChatMessage(PartyId.FromEntityId(partyId).ToChannelId(), message);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00077C6A File Offset: 0x00075E6A
		public void ClearPartyAttribute(bgs.types.EntityId partyId, string attributeKey)
		{
			this.m_partyAPI.ClearPartyAttribute(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00077C83 File Offset: 0x00075E83
		public void SetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, [In] long value)
		{
			this.m_partyAPI.SetPartyAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00077C9D File Offset: 0x00075E9D
		public void SetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, [In] string value)
		{
			this.m_partyAPI.SetPartyAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00077CB7 File Offset: 0x00075EB7
		public void SetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, [In] byte[] value)
		{
			this.m_partyAPI.SetPartyAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, value);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00077CD1 File Offset: 0x00075ED1
		public void SetPartyAttributes(bgs.types.EntityId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			this.m_partyAPI.SetPartyAttributes(PartyId.FromEntityId(partyId).ToChannelId(), attrs);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00077CEA File Offset: 0x00075EEA
		public void ClearMemberAttribute(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			this.m_partyAPI.ClearMemberAttribute(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00077D04 File Offset: 0x00075F04
		public void SetMemberAttributeLong(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value)
		{
			this.m_partyAPI.SetMemberAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00077D20 File Offset: 0x00075F20
		public void SetMemberAttributeString(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value)
		{
			this.m_partyAPI.SetMemberAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00077D3C File Offset: 0x00075F3C
		public void SetMemberAttributeBlob(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value)
		{
			this.m_partyAPI.SetMemberAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, value);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00077D58 File Offset: 0x00075F58
		public void SetMemberAttributes(bgs.types.EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			this.m_partyAPI.SetMemberAttributes(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attrs);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00077D72 File Offset: 0x00075F72
		public int GetPartyPrivacy(bgs.types.EntityId partyId)
		{
			return this.m_partyAPI.GetPartyPrivacy(PartyId.FromEntityId(partyId).ToChannelId());
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00077D8A File Offset: 0x00075F8A
		public int GetCountPartyMembers(bgs.types.EntityId partyId)
		{
			return (int)this.m_partyAPI.GetCountPartyMembers(PartyId.FromEntityId(partyId).ToChannelId());
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00077DA2 File Offset: 0x00075FA2
		public int GetMaxPartyMembers(bgs.types.EntityId partyId)
		{
			return (int)this.m_partyAPI.GetMaxPartyMembers(PartyId.FromEntityId(partyId).ToChannelId());
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00077DBA File Offset: 0x00075FBA
		public void GetPartyMembers(bgs.types.EntityId partyId, out PartyMember[] members)
		{
			this.m_partyAPI.GetPartyMembers(PartyId.FromEntityId(partyId).ToChannelId(), out members);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00077DD3 File Offset: 0x00075FD3
		public void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			this.m_partyAPI.GetReceivedPartyInvites(out invites);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00077DE1 File Offset: 0x00075FE1
		public void GetPartySentInvites(bgs.types.EntityId partyId, out PartyInvite[] invites)
		{
			this.m_partyAPI.GetPartySentInvites(PartyId.FromEntityId(partyId).ToChannelId(), out invites);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00077DFA File Offset: 0x00075FFA
		public void GetPartyInviteRequests(bgs.types.EntityId partyId, out InviteRequest[] requests)
		{
			this.m_partyAPI.GetPartyInviteRequests(PartyId.FromEntityId(partyId).ToChannelId(), out requests);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00077E13 File Offset: 0x00076013
		public void GetAllPartyAttributes(bgs.types.EntityId partyId, out string[] allKeys)
		{
			this.m_partyAPI.GetAllPartyAttributes(PartyId.FromEntityId(partyId).ToChannelId(), out allKeys);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00077E2C File Offset: 0x0007602C
		public bool GetPartyAttributeLong(bgs.types.EntityId partyId, string attributeKey, out long value)
		{
			return this.m_partyAPI.GetPartyAttributeLong(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00077E46 File Offset: 0x00076046
		public void GetPartyAttributeString(bgs.types.EntityId partyId, string attributeKey, out string value)
		{
			this.m_partyAPI.GetPartyAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00077E60 File Offset: 0x00076060
		public void GetPartyAttributeBlob(bgs.types.EntityId partyId, string attributeKey, out byte[] value)
		{
			this.m_partyAPI.GetPartyAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), attributeKey, out value);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00077E7A File Offset: 0x0007607A
		public void GetMemberAttributeString(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			this.m_partyAPI.GetMemberAttributeString(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, out value);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00077E96 File Offset: 0x00076096
		public void GetMemberAttributeBlob(bgs.types.EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			this.m_partyAPI.GetMemberAttributeBlob(PartyId.FromEntityId(partyId).ToChannelId(), partyMember, attributeKey, out value);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00077EB2 File Offset: 0x000760B2
		public void GetPartyListenerEvents(out PartyListenerEvent[] updates)
		{
			this.m_partyAPI.GetPartyListenerEvents(out updates);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00077EC0 File Offset: 0x000760C0
		public void ClearPartyListenerEvents()
		{
			this.m_partyAPI.ClearPartyListenerEvents();
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00077ECD File Offset: 0x000760CD
		public void GetFriendsInfo(ref FriendsInfo info)
		{
			this.m_friendAPI.GetFriendsInfo(ref info);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00077EDB File Offset: 0x000760DB
		public void ClearFriendsUpdates()
		{
			this.m_friendAPI.ClearFriendsUpdates();
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00077EE8 File Offset: 0x000760E8
		public void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			this.m_friendAPI.GetFriendsUpdates(updates);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00077EF6 File Offset: 0x000760F6
		public void SendFriendInvite(string sender, string target, bool byEmail)
		{
			this.m_friendAPI.SendFriendInvite(sender, target, byEmail);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00077F06 File Offset: 0x00076106
		public void ManageFriendInvite(int action, ulong inviteId)
		{
			this.m_friendAPI.ManageFriendInvite(action, inviteId);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00077F15 File Offset: 0x00076115
		public void RemoveFriend(BnetAccountId account)
		{
			this.m_friendAPI.RemoveFriend(account);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00077F23 File Offset: 0x00076123
		public void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			this.m_whisperAPI.SendWhisper(gameAccount, message);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x00077F32 File Offset: 0x00076132
		public void GetWhisperInfo(ref WhisperInfo info)
		{
			this.m_whisperAPI.GetWhisperInfo(ref info);
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00077F40 File Offset: 0x00076140
		public void GetWhispers([Out] BnetWhisper[] whispers)
		{
			this.m_whisperAPI.GetWhispers(whispers);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00077F4E File Offset: 0x0007614E
		public void ClearWhispers()
		{
			this.m_whisperAPI.ClearWhispers();
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00077F5B File Offset: 0x0007615B
		public int GetNotificationCount()
		{
			return this.m_notificationAPI.GetNotificationCount();
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00077F68 File Offset: 0x00076168
		public void GetNotifications([Out] BnetNotification[] notifications)
		{
			this.m_notificationAPI.GetNotifications(notifications);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00077F76 File Offset: 0x00076176
		public void ClearNotifications()
		{
			this.m_notificationAPI.ClearNotifications();
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00077F83 File Offset: 0x00076183
		public void ApplicationWasPaused()
		{
			this.m_logSource.LogWarning("Application was paused.");
			if (this.m_rpcConnection != null)
			{
				this.m_rpcConnection.Update();
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00077FA8 File Offset: 0x000761A8
		public void ApplicationWasUnpaused()
		{
			this.m_logSource.LogWarning("Application was unpaused.");
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00077FBC File Offset: 0x000761BC
		public bool CheckWebAuth(out string url)
		{
			url = null;
			if (this.m_challengeAPI != null && this.InState(BattleNetCSharp.ConnectionState.WaitForLogon))
			{
				ExternalChallenge nextExternalChallenge = this.m_challengeAPI.GetNextExternalChallenge();
				if (nextExternalChallenge != null)
				{
					url = nextExternalChallenge.URL;
					this.m_logSource.LogDebug("Delivering a challenge url={0}", new object[]
					{
						url
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00078012 File Offset: 0x00076212
		public bool HasExternalChallenge()
		{
			return this.m_challengeAPI != null && this.InState(BattleNetCSharp.ConnectionState.WaitForLogon) && this.m_challengeAPI.HasExternalChallenge();
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00078032 File Offset: 0x00076232
		public void ProvideWebAuthToken(string token, RPCContextDelegate callback = null)
		{
			this.m_logSource.LogDebug("ProvideWebAuthToken token={0}", new object[]
			{
				token
			});
			if (this.m_authenticationAPI != null && this.InState(BattleNetCSharp.ConnectionState.WaitForLogon))
			{
				this.m_authenticationAPI.VerifyWebCredentials(token, callback);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0007806C File Offset: 0x0007626C
		public void GenerateSSOToken(Action<bool, string> callback)
		{
			if (this.m_authenticationAPI != null)
			{
				this.m_authenticationAPI.GenerateSSOToken(callback);
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00078082 File Offset: 0x00076282
		public void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			if (this.m_authenticationAPI != null)
			{
				this.m_authenticationAPI.GenerateAppWebCredentials(callback);
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00078098 File Offset: 0x00076298
		public void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			if (this.m_authenticationAPI != null)
			{
				this.m_authenticationAPI.GenerateWtcgWebCredentials(callback);
			}
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000780AE File Offset: 0x000762AE
		public string FilterProfanity(string unfiltered)
		{
			return this.m_profanityAPI.FilterProfanity(unfiltered);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000780BC File Offset: 0x000762BC
		public void SetConnectedRegion(uint region)
		{
			this.m_connectedRegion = region;
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000780C8 File Offset: 0x000762C8
		public void EnqueueErrorInfo(BnetFeature feature, BnetFeatureEvent featureEvent, BattleNetErrors error, RPCContext context = null)
		{
			if (error != BattleNetErrors.ERROR_OK)
			{
				LogLevel level = LogLevel.Warning;
				string format = "Enqueuing BattleNetError {0} {1} code={2} packetId={3} system={4} context={5}";
				if (context == null)
				{
					this.m_logSource.Log(level, format, new object[]
					{
						feature.ToString(),
						featureEvent.ToString(),
						new Error(error),
						"",
						"",
						""
					});
				}
				else
				{
					if (error == BattleNetErrors.ERROR_INTERNAL && context.SystemId == UtilSystemId.BATTLEPAY)
					{
						level = LogLevel.Info;
					}
					this.m_logSource.Log(level, format, new object[]
					{
						feature.ToString(),
						featureEvent.ToString(),
						new Error(error),
						context.PacketId,
						(int)context.SystemId,
						context.Context
					});
				}
			}
			this.m_errorEvents.Add(new BnetErrorInfo(feature, featureEvent, error, (context == null) ? 0 : context.Context));
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000781E1 File Offset: 0x000763E1
		public bnet.protocol.EntityId GameAccountId
		{
			get
			{
				return this.m_authenticationAPI.GameAccountId;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000781EE File Offset: 0x000763EE
		public bnet.protocol.EntityId AccountId
		{
			get
			{
				return this.m_authenticationAPI.AccountId;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000781FB File Offset: 0x000763FB
		public ServiceDescriptor NotificationService
		{
			get
			{
				return this.m_notificationService;
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00078204 File Offset: 0x00076404
		private void HandleForceDisconnectRequest(RPCContext context)
		{
			DisconnectNotification disconnectNotification = DisconnectNotification.ParseFrom(context.Payload);
			this.m_logSource.LogDebug("RPC Called: ForceDisconnect : " + disconnectNotification.ErrorCode);
			this.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, (BattleNetErrors)disconnectNotification.ErrorCode, context);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000781EE File Offset: 0x000763EE
		public bnet.protocol.EntityId GetAccountEntity()
		{
			return this.m_authenticationAPI.AccountId;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00078250 File Offset: 0x00076450
		private void HandleEchoRequest(RPCContext context)
		{
			if (this.m_rpcConnection == null)
			{
				LogAdapter.Log(LogLevel.Error, "HandleEchoRequest with null RPC Connection", "");
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
			this.m_rpcConnection.QueueResponse(context, message);
			Console.WriteLine("");
			Console.WriteLine("[*]send echo response");
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000782D4 File Offset: 0x000764D4
		private void HandleNotificationReceived(RPCContext context)
		{
			Notification notification = bnet.protocol.notification.v1.Notification.ParseFrom(context.Payload);
			this.m_logSource.LogDebug("Notification: " + notification);
			BattleNetCSharp.NotificationHandler notificationHandler;
			if (this.m_notificationHandlers.TryGetValue(notification.Type, out notificationHandler))
			{
				notificationHandler(notification);
				return;
			}
			this.m_logSource.LogWarning("unhandled battle net notification: " + notification.Type);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0007833B File Offset: 0x0007653B
		public void AuroraStateHandler_Unhandled()
		{
			this.m_logSource.LogError("Unhandled Aurora State");
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void AuroraStateHandler_Connect()
		{
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00078350 File Offset: 0x00076550
		public void AuroraStateHandler_InitRPC()
		{
			this.m_importedServices.Clear();
			this.m_exportedServices.Clear();
			ConnectRequest connectRequest = new ConnectRequest();
			this.m_importedServices.Add(this.m_authenticationAPI.AuthServerService);
			this.m_importedServices.Add(this.m_gamesAPI.GameUtilityService);
			this.m_importedServices.Add(this.m_gamesAPI.GameRequestService);
			this.m_importedServices.Add(this.m_notificationService);
			this.m_importedServices.Add(this.m_presenceAPI.PresenceService);
			this.m_importedServices.Add(this.m_channelAPI.ChannelService);
			this.m_importedServices.Add(this.m_channelAPI.ChannelMembershipService);
			this.m_importedServices.Add(this.m_friendAPI.FriendsService);
			this.m_importedServices.Add(this.m_accountAPI.AccountService);
			this.m_importedServices.Add(this.m_resourcesAPI.ResourcesService);
			this.m_exportedServices.Add(this.m_authenticationAPI.AuthClientService);
			this.m_exportedServices.Add(this.m_gamesAPI.GameRequestListener);
			this.m_exportedServices.Add(this.m_notificationListenerService);
			this.m_exportedServices.Add(this.m_channelAPI.ChannelListener);
			this.m_exportedServices.Add(this.m_channelAPI.ChannelMembershipListener);
			this.m_exportedServices.Add(this.m_presenceAPI.ChannelSubscriberService);
			this.m_exportedServices.Add(this.m_friendAPI.FriendsNotifyService);
			this.m_exportedServices.Add(this.m_challengeAPI.ChallengeNotifyService);
			this.m_exportedServices.Add(this.m_accountAPI.AccountNotifyService);
			this.m_exportedServices.Add(this.m_sessionAPI.SessionNotifyService);
			connectRequest.SetBindRequest(this.CreateBindRequest(this.m_importedServices, this.m_exportedServices));
			connectRequest.SetUseBindlessRpc(true);
			this.m_rpcConnection.QueueRequest(this.m_connectionService, 1U, connectRequest, new RPCContextDelegate(this.OnConnectCallback), 0U);
			this.SwitchToState(BattleNetCSharp.ConnectionState.WaitForInitRPC);
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x00078570 File Offset: 0x00076770
		private void OnConnectCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			this.m_logSource.LogDebug("RPC Connected, error : " + status.ToString());
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.SwitchToState(BattleNetCSharp.ConnectionState.Error);
				this.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnConnected, status, context);
				return;
			}
			ConnectResponse connectResponse = ConnectResponse.ParseFrom(context.Payload);
			if (connectResponse.HasServerTime)
			{
				this.m_serverTimeUTCAtConnectMicroseconds = (long)connectResponse.ServerTime;
				this.m_serverTimeDeltaUTCSeconds = this.m_serverTimeUTCAtConnectMicroseconds / 1000000L - this.GetCurrentTimeSecondsSinceUnixEpoch();
			}
			if (connectResponse.HasBindResult && connectResponse.HasBindResponse && connectResponse.BindResult == 0U)
			{
				this.m_logSource.LogDebug("RPC Connected, Bind Result : {0} Bind Response {1}", new object[]
				{
					connectResponse.BindResult,
					connectResponse.BindResponse.ImportedServiceIdCount
				});
				int num = 0;
				foreach (uint num2 in connectResponse.BindResponse.ImportedServiceIdList)
				{
					ServiceDescriptor serviceDescriptor = this.m_importedServices[num++];
					this.m_logSource.LogDebug("Importing service oldId={0} newId={1} name={2}", new object[]
					{
						serviceDescriptor.Id,
						num2,
						serviceDescriptor.Name
					});
					serviceDescriptor.Id = num2;
					this.m_rpcConnection.GetServiceHelper().AddImportedService(serviceDescriptor);
				}
				if (connectResponse.HasContentHandleArray)
				{
					if (!this.m_clientInterface.GetDisableConnectionMetering())
					{
						this.m_rpcConnection.SetConnectionMeteringContentHandles(connectResponse.ContentHandleArray, this.m_localStorageAPI);
					}
					else
					{
						this.m_logSource.LogWarning("Connection metering disabled by configuration.");
					}
				}
				else
				{
					this.m_logSource.LogDebug("Connection response had not connection metering request");
				}
				this.m_logSource.LogDebug("FRONT ServerId={0:x2}", new object[]
				{
					connectResponse.ServerId.Label
				});
				this.InitRPCListeners();
				this.PrintBindServiceResponse(connectResponse.BindResponse);
				this.SwitchToState(BattleNetCSharp.ConnectionState.Logon);
				return;
			}
			this.m_logSource.LogWarning("BindRequest failed with error={0}", new object[]
			{
				connectResponse.BindResult
			});
			this.SwitchToState(BattleNetCSharp.ConnectionState.Error);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void AuroraStateHandler_WaitForInitRPC()
		{
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000787C4 File Offset: 0x000769C4
		public void AuroraStateHandler_Logon()
		{
			this.m_logSource.LogDebug("Sending Logon request");
			LogonRequest message = this.CreateLogonRequest();
			this.m_rpcConnection.QueueRequest(this.m_authenticationAPI.AuthServerService, 1U, message, new RPCContextDelegate(this.OnLogonCallback), 0U);
			this.SwitchToState(BattleNetCSharp.ConnectionState.WaitForLogon);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x00078816 File Offset: 0x00076A16
		private void OnLogonCallback(RPCContext context)
		{
			this.m_logSource.LogDebug("Logon Complete. Context = {0}", new object[]
			{
				context.Request.ToString()
			});
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0007883C File Offset: 0x00076A3C
		public void AuroraStateHandler_WaitForLogon()
		{
			if (this.m_authenticationAPI.AuthenticationFailure())
			{
				this.EnqueueErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, BattleNetErrors.ERROR_NO_AUTH, null);
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void AuroraStateHandler_WaitForGameAccountSelect()
		{
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00078857 File Offset: 0x00076A57
		public void IssueSelectGameAccountRequest()
		{
			this.m_rpcConnection.QueueRequest(this.m_authenticationAPI.AuthServerService, 4U, this.GameAccountId, new RPCContextDelegate(this.OnSelectGameAccountCallback), 0U);
			this.SwitchToState(BattleNetCSharp.ConnectionState.WaitForGameAccountSelect);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007888C File Offset: 0x00076A8C
		private void OnSelectGameAccountCallback(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status == BattleNetErrors.ERROR_OK)
			{
				this.SwitchToState(BattleNetCSharp.ConnectionState.WaitForAPIToInitialize);
				using (List<BattleNetAPI>.Enumerator enumerator = this.m_apiList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BattleNetAPI battleNetAPI = enumerator.Current;
						battleNetAPI.Initialize();
						battleNetAPI.OnGameAccountSelected();
					}
					return;
				}
			}
			this.SwitchToState(BattleNetCSharp.ConnectionState.Error);
			this.EnqueueErrorInfo(BnetFeature.Auth, BnetFeatureEvent.Auth_GameAccountSelected, status, context);
			this.m_logSource.LogError("Failed to select a game account status = {0}", new object[]
			{
				status.ToString()
			});
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x00078934 File Offset: 0x00076B34
		public void AuroraStateHandler_WaitForAPIToInitialize()
		{
			if (this.m_friendAPI.IsInitialized && this.m_sessionAPI.IsInitialized)
			{
				this.SwitchToState(BattleNetCSharp.ConnectionState.Ready);
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00078958 File Offset: 0x00076B58
		public void AuroraStateHandler_Ready()
		{
			this.ChannelMembershipSubscribe();
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x00078960 File Offset: 0x00076B60
		public void AuroraStateHandler_Disconnected()
		{
			this.m_logSource.LogWarning("Client disconnected from Aurora");
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x00078972 File Offset: 0x00076B72
		public BattleNetLogSource GetLogSource()
		{
			return this.m_logSource;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0007897A File Offset: 0x00076B7A
		private bool InState(BattleNetCSharp.ConnectionState state)
		{
			return this.m_connectionState == state;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00078988 File Offset: 0x00076B88
		private bool SwitchToState(BattleNetCSharp.ConnectionState state)
		{
			if (state == this.m_connectionState)
			{
				return false;
			}
			bool flag = true;
			if (state != BattleNetCSharp.ConnectionState.Disconnected || this.m_connectionState != BattleNetCSharp.ConnectionState.Ready)
			{
				flag = (state > this.m_connectionState);
			}
			if (flag)
			{
				this.m_logSource.LogDebug("Expected state change {0} -> {1}", new object[]
				{
					this.m_connectionState.ToString(),
					state.ToString()
				});
			}
			else
			{
				this.m_logSource.LogWarning("Unexpected state changes {0} -> {1}", new object[]
				{
					this.m_connectionState.ToString(),
					state.ToString()
				});
				this.m_logSource.LogDebugStackTrace("SwitchToState", 5, 0);
			}
			this.m_connectionState = state;
			return true;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x00078A4C File Offset: 0x00076C4C
		private BindRequest CreateBindRequest(List<ServiceDescriptor> imports, List<ServiceDescriptor> exports)
		{
			BindRequest bindRequest = new BindRequest();
			uint num = 0U;
			foreach (ServiceDescriptor serviceDescriptor in imports)
			{
				num = (serviceDescriptor.Id = num + 1U);
				BoundService boundService = new BoundService();
				boundService.SetId(serviceDescriptor.Id);
				boundService.SetHash(serviceDescriptor.Hash);
				bindRequest.AddImportedService(boundService);
				this.m_rpcConnection.GetServiceHelper().AddImportedService(serviceDescriptor);
				this.m_logSource.LogDebug("Importing service id={0} name={1} hash={2}", new object[]
				{
					serviceDescriptor.Id,
					serviceDescriptor.Name,
					serviceDescriptor.Hash
				});
			}
			foreach (ServiceDescriptor serviceDescriptor2 in exports)
			{
				num = (serviceDescriptor2.Id = num + 1U);
				BoundService boundService2 = new BoundService();
				boundService2.SetId(serviceDescriptor2.Id);
				boundService2.SetHash(serviceDescriptor2.Hash);
				bindRequest.AddExportedService(boundService2);
				this.m_rpcConnection.GetServiceHelper().AddExportedService(serviceDescriptor2);
				this.m_logSource.LogDebug("Exporting service id={0} name={1} hash={2}", new object[]
				{
					serviceDescriptor2.Id,
					serviceDescriptor2.Name,
					serviceDescriptor2.Hash
				});
			}
			return bindRequest;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00078BE8 File Offset: 0x00076DE8
		private LogonRequest CreateLogonRequest()
		{
			LogonRequest logonRequest = new LogonRequest();
			logonRequest.SetProgram("WTCG");
			logonRequest.SetLocale(this.Client().GetLocaleName());
			logonRequest.SetPlatform(this.Client().GetPlatformName());
			logonRequest.SetVersion(this.Client().GetAuroraVersionName());
			logonRequest.SetApplicationVersion(this.Client().GetApplicationVersion());
			logonRequest.SetPublicComputer(false);
			logonRequest.SetAllowLogonQueueNotifications(true);
			string userAgent = this.Client().GetUserAgent();
			if (!string.IsNullOrEmpty(userAgent))
			{
				logonRequest.SetUserAgent(userAgent);
			}
			bool flag = true;
			this.m_logSource.LogDebug("CreateLogonRequest SSL={0}", new object[]
			{
				flag
			});
			if (!string.IsNullOrEmpty(this.m_userEmailAddress))
			{
				this.m_logSource.LogDebug("Email = {0}", new object[]
				{
					this.m_userEmailAddress
				});
			}
			return logonRequest;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x00078CC4 File Offset: 0x00076EC4
		public void OnBroadcastReceived(IList<bnet.protocol.Attribute> AttributeList)
		{
			foreach (bnet.protocol.Attribute attribute in AttributeList)
			{
				if (attribute.Name == "shutdown")
				{
					this.m_shutdownInMinutes = (int)attribute.Value.IntValue;
				}
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00078D2C File Offset: 0x00076F2C
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
			this.m_logSource.LogDebug(text);
		}

		// Token: 0x04000C0B RID: 3083
		private BattleNetLogSource m_logSource = new BattleNetLogSource("Main");

		// Token: 0x04000C0C RID: 3084
		private FriendsAPI m_friendAPI;

		// Token: 0x04000C0D RID: 3085
		private PresenceAPI m_presenceAPI;

		// Token: 0x04000C0E RID: 3086
		private ChannelAPI m_channelAPI;

		// Token: 0x04000C0F RID: 3087
		private GamesAPI m_gamesAPI;

		// Token: 0x04000C10 RID: 3088
		private PartyAPI m_partyAPI;

		// Token: 0x04000C11 RID: 3089
		private ChallengeAPI m_challengeAPI;

		// Token: 0x04000C12 RID: 3090
		private WhisperAPI m_whisperAPI;

		// Token: 0x04000C13 RID: 3091
		private NotificationAPI m_notificationAPI;

		// Token: 0x04000C14 RID: 3092
		private BroadcastAPI m_broadcastAPI;

		// Token: 0x04000C15 RID: 3093
		private AccountAPI m_accountAPI;

		// Token: 0x04000C16 RID: 3094
		private AuthenticationAPI m_authenticationAPI;

		// Token: 0x04000C17 RID: 3095
		private LocalStorageAPI m_localStorageAPI;

		// Token: 0x04000C18 RID: 3096
		private ResourcesAPI m_resourcesAPI;

		// Token: 0x04000C19 RID: 3097
		private ProfanityAPI m_profanityAPI;

		// Token: 0x04000C1A RID: 3098
		private SessionAPI m_sessionAPI;

		// Token: 0x04000C1B RID: 3099
		private Dictionary<string, BattleNetCSharp.NotificationHandler> m_notificationHandlers;

		// Token: 0x04000C1C RID: 3100
		private Dictionary<BattleNetCSharp.ConnectionState, BattleNetCSharp.AuroraStateHandler> m_stateHandlers;

		// Token: 0x04000C1D RID: 3101
		private List<ServiceDescriptor> m_importedServices;

		// Token: 0x04000C1E RID: 3102
		private List<ServiceDescriptor> m_exportedServices;

		// Token: 0x04000C1F RID: 3103
		private List<BattleNet.BnetEvent> m_bnetEvents;

		// Token: 0x04000C20 RID: 3104
		private readonly long m_keepAliveIntervalMilliseconds = 20000L;

		// Token: 0x04000C21 RID: 3105
		private readonly DateTime m_unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000C22 RID: 3106
		private long m_serverTimeUTCAtConnectMicroseconds;

		// Token: 0x04000C23 RID: 3107
		private long m_serverTimeDeltaUTCSeconds;

		// Token: 0x04000C24 RID: 3108
		private List<BattleNetAPI> m_apiList;

		// Token: 0x04000C25 RID: 3109
		private uint m_connectedRegion = uint.MaxValue;

		// Token: 0x04000C28 RID: 3112
		protected ClientInterface m_clientInterface;

		// Token: 0x04000C29 RID: 3113
		private Stopwatch m_stopwatch = new Stopwatch();

		// Token: 0x04000C2A RID: 3114
		private int m_shutdownInMinutes;

		// Token: 0x04000C2B RID: 3115
		private string m_auroraEnvironment;

		// Token: 0x04000C2C RID: 3116
		private uint m_auroraPort;

		// Token: 0x04000C2D RID: 3117
		private string m_userEmailAddress;

		// Token: 0x04000C2E RID: 3118
		private readonly ICompressionProvider m_compressionProvider;

		// Token: 0x04000C2F RID: 3119
		private readonly IFileUtil m_fileUtil;

		// Token: 0x04000C30 RID: 3120
		private readonly IJsonSerializer m_jsonSerializer;

		// Token: 0x04000C31 RID: 3121
		private readonly IRpcConnectionFactory m_rpcConnectionFactory;

		// Token: 0x04000C32 RID: 3122
		private readonly int[] ConnectAuroraAttemptRateByAttemptCount = new int[]
		{
			1,
			1,
			2,
			3,
			5
		};

		// Token: 0x04000C33 RID: 3123
		private DateTime m_attemptTime;

		// Token: 0x04000C34 RID: 3124
		private bool m_hasSuccessfullyConnected;

		// Token: 0x04000C35 RID: 3125
		private int m_retryAttemptCount;

		// Token: 0x04000C36 RID: 3126
		private string m_connectAuroraAddress;

		// Token: 0x04000C37 RID: 3127
		private uint m_connectAuroraPort;

		// Token: 0x04000C38 RID: 3128
		private SslParameters m_connectAuororaSslParams;

		// Token: 0x04000C39 RID: 3129
		private ISocketEventListener m_socketEventListener;

		// Token: 0x04000C3A RID: 3130
		private bool m_isSubscribedToMembershipEvents;

		// Token: 0x04000C3B RID: 3131
		private bool m_initialized;

		// Token: 0x04000C3C RID: 3132
		private IRpcConnection m_rpcConnection;

		// Token: 0x04000C3D RID: 3133
		private BattleNetCSharp.ConnectionState m_connectionState;

		// Token: 0x04000C3E RID: 3134
		private List<BnetErrorInfo> m_errorEvents = new List<BnetErrorInfo>();

		// Token: 0x04000C3F RID: 3135
		private const uint DEFAULT_PORT = 1119U;

		// Token: 0x04000C40 RID: 3136
		private const string s_programName = "WTCG";

		// Token: 0x04000C41 RID: 3137
		private ServiceDescriptor m_connectionService = new ConnectionService();

		// Token: 0x04000C42 RID: 3138
		private ServiceDescriptor m_notificationService = new NotificationService();

		// Token: 0x04000C43 RID: 3139
		private ServiceDescriptor m_notificationListenerService = new NotificationListenerService();

		// Token: 0x020006BA RID: 1722
		public enum ConnectionState
		{
			// Token: 0x04002212 RID: 8722
			Disconnected,
			// Token: 0x04002213 RID: 8723
			Connect,
			// Token: 0x04002214 RID: 8724
			InitRPC,
			// Token: 0x04002215 RID: 8725
			WaitForInitRPC,
			// Token: 0x04002216 RID: 8726
			Logon,
			// Token: 0x04002217 RID: 8727
			WaitForLogon,
			// Token: 0x04002218 RID: 8728
			WaitForGameAccountSelect,
			// Token: 0x04002219 RID: 8729
			WaitForAPIToInitialize,
			// Token: 0x0400221A RID: 8730
			Ready,
			// Token: 0x0400221B RID: 8731
			Error
		}

		// Token: 0x020006BB RID: 1723
		// (Invoke) Token: 0x0600626A RID: 25194
		public delegate void AuroraStateHandler();

		// Token: 0x020006BC RID: 1724
		// (Invoke) Token: 0x0600626E RID: 25198
		private delegate void NotificationHandler(Notification notification);

		// Token: 0x020006BD RID: 1725
		// (Invoke) Token: 0x06006272 RID: 25202
		private delegate void OnConnectHandler(BattleNetErrors error);

		// Token: 0x020006BE RID: 1726
		// (Invoke) Token: 0x06006276 RID: 25206
		private delegate void OnDisconectHandler(BattleNetErrors error);

		// Token: 0x020006BF RID: 1727
		private class BnetErrorComparer : IComparer<BnetErrorInfo>
		{
			// Token: 0x06006279 RID: 25209 RVA: 0x00128858 File Offset: 0x00126A58
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
	}
}
