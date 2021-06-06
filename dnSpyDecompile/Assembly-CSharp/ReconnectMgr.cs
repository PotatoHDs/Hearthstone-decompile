using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000906 RID: 2310
public class ReconnectMgr : IService, IHasUpdate
{
	// Token: 0x14000084 RID: 132
	// (add) Token: 0x06008080 RID: 32896 RVA: 0x0029C4F0 File Offset: 0x0029A6F0
	// (remove) Token: 0x06008081 RID: 32897 RVA: 0x0029C528 File Offset: 0x0029A728
	public event Action OnReconnectComplete;

	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x06008082 RID: 32898 RVA: 0x0029C55D File Offset: 0x0029A75D
	// (set) Token: 0x06008083 RID: 32899 RVA: 0x0029C565 File Offset: 0x0029A765
	public bool FullResetRequired { get; set; }

	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x06008084 RID: 32900 RVA: 0x0029C56E File Offset: 0x0029A76E
	// (set) Token: 0x06008085 RID: 32901 RVA: 0x0029C576 File Offset: 0x0029A776
	public bool UpdateRequired { get; set; }

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x06008086 RID: 32902 RVA: 0x0029C57F File Offset: 0x0029A77F
	// (set) Token: 0x06008087 RID: 32903 RVA: 0x0029C587 File Offset: 0x0029A787
	public bool ReconnectBlockedByInactivity { get; set; }

	// Token: 0x06008088 RID: 32904 RVA: 0x0029C590 File Offset: 0x0029A790
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		Network network = Network.Get();
		network.AddBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		network.OnDisconnectedFromBattleNet += this.OnDisconnectedFromBattleNet;
		serviceLocator.Get<LoginManager>().OnAchievesLoaded += this.OnAchievesLoaded;
		ReconnectMgr.m_authTokenCache = new OfflineAuthTokenCache(global::Log.Offline);
		HearthstoneApplication.Get().WillReset += this.WillReset;
		yield break;
	}

	// Token: 0x06008089 RID: 32905 RVA: 0x0029C5A8 File Offset: 0x0029A7A8
	public void Update()
	{
		this.CheckGameplayReconnectTimeout();
		this.CheckGameplayReconnectRetry();
		ReconnectMgr.m_authTokenCache.RefreshTokenCacheIfNeeded();
		if (!Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora())
		{
			this.UpdateWhileDisconnectedFromBattleNet();
			return;
		}
		if (Network.IsLoggedIn() && this.m_initializedForOfflineAccess)
		{
			this.OnBoxReconnectComplete();
			return;
		}
	}

	// Token: 0x0600808A RID: 32906 RVA: 0x0029C5F6 File Offset: 0x0029A7F6
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(LoginManager),
			typeof(GameMgr),
			typeof(NetworkReachabilityManager)
		};
	}

	// Token: 0x0600808B RID: 32907 RVA: 0x0029C628 File Offset: 0x0029A828
	public void Shutdown()
	{
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.RemoveBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
			network.OnDisconnectedFromBattleNet -= this.OnDisconnectedFromBattleNet;
		}
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr))
		{
			gameMgr.UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
		LoginManager loginManager;
		if (HearthstoneServices.TryGet<LoginManager>(out loginManager))
		{
			loginManager.OnAchievesLoaded -= this.OnAchievesLoaded;
		}
		HearthstoneApplication.Get().WillReset -= this.WillReset;
	}

	// Token: 0x0600808C RID: 32908 RVA: 0x0029C6B0 File Offset: 0x0029A8B0
	public static ReconnectMgr Get()
	{
		return HearthstoneServices.Get<ReconnectMgr>();
	}

	// Token: 0x0600808D RID: 32909 RVA: 0x0029C6B7 File Offset: 0x0029A8B7
	public bool IsReconnecting()
	{
		return this.m_reconnectType > ReconnectType.INVALID;
	}

	// Token: 0x0600808E RID: 32910 RVA: 0x0029C6C2 File Offset: 0x0029A8C2
	public bool IsRestoringGameStateFromDatabase()
	{
		return this.m_savedStartGameParams != null && this.m_savedStartGameParams.LoadGame;
	}

	// Token: 0x0600808F RID: 32911 RVA: 0x0029C6D9 File Offset: 0x0029A8D9
	public bool IsStartingReconnectGame()
	{
		if (GameMgr.Get().IsReconnect())
		{
			if (SceneMgr.Get().GetNextMode() == SceneMgr.Mode.GAMEPLAY)
			{
				return true;
			}
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY && !SceneMgr.Get().IsSceneLoaded())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008090 RID: 32912 RVA: 0x0029C714 File Offset: 0x0029A914
	public static bool IsReconnectAllowed(FatalErrorMessage fatalErrorMessage)
	{
		if (fatalErrorMessage != null)
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Checking Fatal Error Reason: {0}", new object[]
			{
				fatalErrorMessage.m_reason
			});
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Unable to retrieve guardian vars.", Array.Empty<object>());
			return false;
		}
		if (!netObject.AllowOfflineClientActivity)
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect disabled by guardian var.", Array.Empty<object>());
			return false;
		}
		if (fatalErrorMessage != null && !FatalErrorMgr.IsReconnectAllowedBasedOnFatalErrorReason(fatalErrorMessage.m_reason))
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect not allowed because of Fatal Error Reason. Reason={0}", new object[]
			{
				fatalErrorMessage.m_reason
			});
			return false;
		}
		if (ReconnectMgr.m_authTokenCache != null && ReconnectMgr.m_authTokenCache.FailedToGenerateLoginWebToken)
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect not allowed because you failed to generate a valid login token.", Array.Empty<object>());
			return false;
		}
		if (!GameUtils.AreAllTutorialsComplete())
		{
			global::Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect disabled for players in tutorial.", Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x06008091 RID: 32913 RVA: 0x0029C805 File Offset: 0x0029AA05
	public float GetTimeout()
	{
		if (HearthstoneApplication.IsInternal())
		{
			return Options.Get().GetFloat(Option.RECONNECT_TIMEOUT);
		}
		return (float)OptionDataTables.s_defaultsMap[Option.RECONNECT_TIMEOUT];
	}

	// Token: 0x06008092 RID: 32914 RVA: 0x0029C82C File Offset: 0x0029AA2C
	public float GetRetryTime()
	{
		if (HearthstoneApplication.IsInternal())
		{
			return Options.Get().GetFloat(Option.RECONNECT_RETRY_TIME);
		}
		return (float)OptionDataTables.s_defaultsMap[Option.RECONNECT_RETRY_TIME];
	}

	// Token: 0x06008093 RID: 32915 RVA: 0x0029C853 File Offset: 0x0029AA53
	public bool AddTimeoutListener(ReconnectMgr.TimeoutCallback callback)
	{
		return this.AddTimeoutListener(callback, null);
	}

	// Token: 0x06008094 RID: 32916 RVA: 0x0029C860 File Offset: 0x0029AA60
	public bool AddTimeoutListener(ReconnectMgr.TimeoutCallback callback, object userData)
	{
		ReconnectMgr.TimeoutListener timeoutListener = new ReconnectMgr.TimeoutListener();
		timeoutListener.SetCallback(callback);
		timeoutListener.SetUserData(userData);
		if (this.m_timeoutListeners.Contains(timeoutListener))
		{
			return false;
		}
		this.m_timeoutListeners.Add(timeoutListener);
		return true;
	}

	// Token: 0x06008095 RID: 32917 RVA: 0x0029C89E File Offset: 0x0029AA9E
	public bool RemoveTimeoutListener(ReconnectMgr.TimeoutCallback callback)
	{
		return this.RemoveTimeoutListener(callback, null);
	}

	// Token: 0x06008096 RID: 32918 RVA: 0x0029C8A8 File Offset: 0x0029AAA8
	public bool RemoveTimeoutListener(ReconnectMgr.TimeoutCallback callback, object userData)
	{
		ReconnectMgr.TimeoutListener timeoutListener = new ReconnectMgr.TimeoutListener();
		timeoutListener.SetCallback(callback);
		timeoutListener.SetUserData(userData);
		return this.m_timeoutListeners.Remove(timeoutListener);
	}

	// Token: 0x06008097 RID: 32919 RVA: 0x0029C8D8 File Offset: 0x0029AAD8
	private void HandleDisconnectedGameResult()
	{
		NetCache.ProfileNoticeDisconnectedGame dcgameNotice = this.GetDCGameNotice();
		if (dcgameNotice != null && dcgameNotice.GameResult != ProfileNoticeDisconnectedGameResult.GameResult.GR_PLAYING)
		{
			this.OnGameResult(dcgameNotice);
		}
	}

	// Token: 0x06008098 RID: 32920 RVA: 0x0029C900 File Offset: 0x0029AB00
	public bool ReconnectToGameFromLogin()
	{
		this.HandleDisconnectedGameResult();
		NetCache.NetCacheDisconnectedGame netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisconnectedGame>();
		if (netObject == null || netObject.ServerInfo == null || (HearthstoneApplication.IsInternal() && !Vars.Key("Developer.ReconnectToGameFromLogin").GetBool(true)))
		{
			return false;
		}
		this.StartReconnecting(ReconnectType.LOGIN);
		this.ReconnectToGameFromLogin_RequestRequiredData(netObject);
		return true;
	}

	// Token: 0x06008099 RID: 32921 RVA: 0x0029C954 File Offset: 0x0029AB54
	private void ReconnectToGameFromLogin_RequestRequiredData(NetCache.NetCacheDisconnectedGame dcGame)
	{
		GameType gameType = dcGame.GameType;
		if (gameType == GameType.GT_BATTLEGROUNDS)
		{
			Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.ReconnectToGameFromLogin_OnBaconRatingInfo), null);
			Network.Get().RequestBaconRatingInfo();
			return;
		}
		this.ReconnectToGameFromLogin_StartGame(dcGame);
	}

	// Token: 0x0600809A RID: 32922 RVA: 0x0029C9A4 File Offset: 0x0029ABA4
	private void ReconnectToGameFromLogin_OnBaconRatingInfo()
	{
		Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.ReconnectToGameFromLogin_OnBaconRatingInfo));
		NetCache.NetCacheDisconnectedGame netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisconnectedGame>();
		this.ReconnectToGameFromLogin_StartGame(netObject);
	}

	// Token: 0x0600809B RID: 32923 RVA: 0x0029C9E4 File Offset: 0x0029ABE4
	private void ReconnectToGameFromLogin_StartGame(NetCache.NetCacheDisconnectedGame dcGame)
	{
		this.StartGame(dcGame.GameType, dcGame.FormatType, ReconnectType.LOGIN, dcGame.ServerInfo, dcGame.ServerInfo.Mission, dcGame.LoadGameState);
	}

	// Token: 0x0600809C RID: 32924 RVA: 0x0029CA10 File Offset: 0x0029AC10
	public bool ReconnectToGameFromGameplay()
	{
		GameServerInfo lastGameServerJoined = Network.Get().GetLastGameServerJoined();
		if (lastGameServerJoined == null)
		{
			Debug.LogError("serverInfo in ReconnectMgr.ReconnectFromGameplay is null and should not be!");
			return false;
		}
		if (!lastGameServerJoined.Resumable)
		{
			return false;
		}
		this.HideGameplayReconnectDialog();
		GameType gameType = GameMgr.Get().GetGameType();
		FormatType formatType = GameMgr.Get().GetFormatType();
		ReconnectType reconnectType = ReconnectType.GAMEPLAY;
		this.StartReconnecting(reconnectType);
		this.m_reconnectCoroutine = this.WaitForInternetAndReconnect(gameType, formatType, reconnectType, lastGameServerJoined);
		HearthstoneApplication.Get().StartCoroutine(this.m_reconnectCoroutine);
		return true;
	}

	// Token: 0x0600809D RID: 32925 RVA: 0x0029CA88 File Offset: 0x0029AC88
	public void StopReconnectCoroutine()
	{
		if (this.m_reconnectCoroutine != null)
		{
			HearthstoneApplication.Get().StopCoroutine(this.m_reconnectCoroutine);
			this.m_reconnectCoroutine = null;
		}
	}

	// Token: 0x0600809E RID: 32926 RVA: 0x0029CAA9 File Offset: 0x0029ACA9
	private IEnumerator WaitForInternetAndReconnect(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo)
	{
		while (!this.m_networkReachabilityManager.InternetAvailable_Cached)
		{
			yield return new WaitForSeconds(1f);
		}
		this.StartGame(gameType, formatType, reconnectType, serverInfo, 0, false);
		this.m_reconnectCoroutine = null;
		yield break;
	}

	// Token: 0x0600809F RID: 32927 RVA: 0x0029CAD8 File Offset: 0x0029ACD8
	public bool ShowDisconnectedGameResult(NetCache.ProfileNoticeDisconnectedGame dcGame)
	{
		if (!GameUtils.IsMatchmadeGameType(dcGame.GameType, null))
		{
			return false;
		}
		TimeSpan timeSpan = DateTime.UtcNow - DateTime.FromFileTimeUtc(dcGame.Date);
		global::Log.ReturningPlayer.Print("This user disconnected from his or her last game {0} minutes ago.", new object[]
		{
			timeSpan.TotalMinutes
		});
		if (timeSpan.TotalHours > 24.0)
		{
			global::Log.All.Print("Not showing the Disconnected Game Result because the game was disconnected from {0} hours ago.", new object[]
			{
				timeSpan.TotalHours
			});
			return false;
		}
		ProfileNoticeDisconnectedGameResult.GameResult gameResult = dcGame.GameResult;
		if (gameResult - ProfileNoticeDisconnectedGameResult.GameResult.GR_WINNER > 1)
		{
			Debug.LogError(string.Format("ReconnectMgr.ShowDisconnectedGameResult() - unhandled game result {0}", dcGame.GameResult));
			return false;
		}
		if (dcGame.GameType == GameType.GT_UNKNOWN)
		{
			return false;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_RECONNECT_RESULT_HEADER");
		string key;
		if (dcGame.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_TIE)
		{
			key = "GLUE_RECONNECT_RESULT_TIE";
		}
		else
		{
			switch (dcGame.YourResult)
			{
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_WON:
				key = "GLUE_RECONNECT_RESULT_WIN";
				break;
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_LOST:
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_QUIT:
				key = "GLUE_RECONNECT_RESULT_LOSE";
				break;
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_DISCONNECTED:
				key = "GLUE_RECONNECT_RESULT_DISCONNECT";
				break;
			default:
				Debug.LogError(string.Format("ReconnectMgr.ShowDisconnectedGameResult() - unhandled player result {0}", dcGame.YourResult));
				return false;
			}
		}
		popupInfo.m_text = GameStrings.Format(key, new object[]
		{
			this.GetGameTypeName(dcGame.GameType, dcGame.MissionId)
		});
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_showAlertIcon = true;
		DialogManager.Get().ShowPopup(popupInfo);
		return true;
	}

	// Token: 0x060080A0 RID: 32928 RVA: 0x0029CC68 File Offset: 0x0029AE68
	private string GetGameTypeName(GameType gameType, int missionId)
	{
		AdventureDbfRecord adventureRecordFromMissionId = GameUtils.GetAdventureRecordFromMissionId(missionId);
		if (adventureRecordFromMissionId != null)
		{
			switch (adventureRecordFromMissionId.ID)
			{
			case 1:
				return GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_TUTORIAL");
			case 2:
				return GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_PRACTICE");
			case 3:
				return GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_NAXXRAMAS");
			case 4:
				return GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_BRM");
			case 7:
				return GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL");
			}
			return adventureRecordFromMissionId.Name;
		}
		string key;
		if (this.m_gameTypeNameKeys.TryGetValue(gameType, out key))
		{
			return GameStrings.Get(key);
		}
		global::Error.AddDevFatal("ReconnectMgr.GetGameTypeName() - no name for mission {0} gameType {1}", new object[]
		{
			missionId,
			gameType
		});
		return string.Empty;
	}

	// Token: 0x060080A1 RID: 32929 RVA: 0x0029CD2D File Offset: 0x0029AF2D
	public void SetNextReLoginCallback(Action nextCallback)
	{
		this.m_nextReLoginCallback = nextCallback;
	}

	// Token: 0x060080A2 RID: 32930 RVA: 0x0029CD36 File Offset: 0x0029AF36
	private void WillReset()
	{
		this.m_gameplayReconnectDialog = null;
		this.FullResetRequired = false;
		this.UpdateRequired = false;
		this.m_initializedForOfflineAccess = false;
		this.ClearReconnectData();
		this.m_timeoutListeners.Clear();
	}

	// Token: 0x060080A3 RID: 32931 RVA: 0x0029CD68 File Offset: 0x0029AF68
	private void StartReconnecting(ReconnectType reconnectType)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.m_reconnectType = reconnectType;
		this.m_reconnectStartTimestamp = realtimeSinceStartup;
		this.m_retryStartTimestamp = realtimeSinceStartup;
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.ReconnectStart(reconnectType.ToString());
		}
		this.ShowGameplayReconnectingDialog();
	}

	// Token: 0x060080A4 RID: 32932 RVA: 0x0029CDB4 File Offset: 0x0029AFB4
	private void CheckGameplayReconnectTimeout()
	{
		if (!this.IsReconnecting())
		{
			return;
		}
		float num = Time.realtimeSinceStartup - this.m_reconnectStartTimestamp;
		float timeout = this.GetTimeout();
		if (num >= timeout && !Network.Get().IsConnectedToGameServer())
		{
			this.OnReconnectTimeout();
		}
	}

	// Token: 0x060080A5 RID: 32933 RVA: 0x0029CDF4 File Offset: 0x0029AFF4
	private void CheckGameplayReconnectRetry()
	{
		if (!this.m_networkReachabilityManager.InternetAvailable_Cached)
		{
			return;
		}
		if (!this.IsReconnecting())
		{
			return;
		}
		if (Network.Get().IsConnectedToGameServer() || Network.Get().GameServerHasEvents())
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_retryStartTimestamp;
		float retryTime = this.GetRetryTime();
		if (num < retryTime)
		{
			return;
		}
		if (this.m_savedStartGameParams.ServerInfo == null)
		{
			Debug.LogError(string.Format("m_savedStartGameParams.ServerInfo in CheckGameplayReconnectRetry is null and should not be! {0}", this.m_savedStartGameParams.ToString()));
			return;
		}
		this.m_retryStartTimestamp = realtimeSinceStartup;
		this.StartGame_Internal();
	}

	// Token: 0x060080A6 RID: 32934 RVA: 0x0029CE80 File Offset: 0x0029B080
	private void OnReconnectTimeout()
	{
		this.SetBypassReconnect(true);
		this.ClearReconnectData();
		this.FireTimeoutEvent();
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			bool flag = GameMgr.Get().IsAI() && !GameMgr.Get().IsTavernBrawl();
			global::Error.AddFatal(FatalErrorReason.RECONNECT_TIME_OUT, flag ? "GLOBAL_ERROR_NETWORK_ADVENTURE_RECONNECT_TIMEOUT" : "GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION", Array.Empty<object>());
			return;
		}
		this.AttemptToRestoreGameState();
	}

	// Token: 0x060080A7 RID: 32935 RVA: 0x0029CEEC File Offset: 0x0029B0EC
	private void AttemptToRestoreGameState()
	{
		if (this.m_savedStartGameParams.LoadGame)
		{
			GameMgr.Get().SetPendingAutoConcede(false);
			GameMgr.Get().FindGame(this.m_savedStartGameParams.GameType, this.m_savedStartGameParams.FormatType, this.m_savedStartGameParams.ScenarioId, 0, 0L, null, null, this.m_savedStartGameParams.LoadGame, null, GameType.GT_UNKNOWN);
			return;
		}
		this.ClearReconnectData();
		this.ChangeGameplayDialogToTimeout();
	}

	// Token: 0x060080A8 RID: 32936 RVA: 0x0029CF63 File Offset: 0x0029B163
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (!this.IsReconnecting() && !this.IsRestoringGameStateFromDatabase())
		{
			return false;
		}
		this.ChangeGameplayDialogToTimeout();
		if (this.m_savedStartGameParams != null)
		{
			this.m_savedStartGameParams.LoadGame = false;
		}
		return true;
	}

	// Token: 0x060080A9 RID: 32937 RVA: 0x0029CF92 File Offset: 0x0029B192
	private void OnDisconnectedFromBattleNet(BattleNetErrors error)
	{
		this.m_initializedForOfflineAccess = false;
	}

	// Token: 0x060080AA RID: 32938 RVA: 0x0029CF9B File Offset: 0x0029B19B
	private void OnGameResult(NetCache.ProfileNoticeDisconnectedGame dcGameNotice)
	{
		this.ShowDisconnectedGameResult(dcGameNotice);
		this.AckNotice(dcGameNotice);
	}

	// Token: 0x060080AB RID: 32939 RVA: 0x0029CFAC File Offset: 0x0029B1AC
	public void SetBypassReconnect(bool shouldBypass)
	{
		this.m_bypassReconnect = shouldBypass;
	}

	// Token: 0x060080AC RID: 32940 RVA: 0x0029CFB5 File Offset: 0x0029B1B5
	public bool GetBypassReconnect()
	{
		return this.m_bypassReconnect;
	}

	// Token: 0x060080AD RID: 32941 RVA: 0x0029CFBD File Offset: 0x0029B1BD
	private void ClearReconnectData()
	{
		this.m_reconnectType = ReconnectType.INVALID;
		this.m_reconnectStartTimestamp = 0f;
		this.m_retryStartTimestamp = 0f;
	}

	// Token: 0x060080AE RID: 32942 RVA: 0x0029CFDC File Offset: 0x0029B1DC
	private void InitializeForOfflineAccess(FatalErrorMessage fatalErrorMessage)
	{
		if (!this.m_initializedForOfflineAccess && ReconnectMgr.IsReconnectAllowed(fatalErrorMessage))
		{
			global::Log.Offline.PrintDebug("ReconnectMgr: Initializing for offline box access.", Array.Empty<object>());
			this.m_initializedForOfflineAccess = true;
			this.m_reconnectNumAttempts = 0;
			this.m_reconnectTimer = 0f;
			Network.Get().ResetForNewAuroraConnection();
		}
	}

	// Token: 0x060080AF RID: 32943 RVA: 0x0029D034 File Offset: 0x0029B234
	private void UpdateWhileDisconnectedFromBattleNet()
	{
		if (!this.m_allowOfflineActivity)
		{
			return;
		}
		if (!this.m_initializedForOfflineAccess)
		{
			return;
		}
		if (this.FullResetRequired)
		{
			return;
		}
		if (this.ReconnectBlockedByInactivity)
		{
			return;
		}
		if (!this.m_networkReachabilityManager.InternetAvailable_Cached)
		{
			return;
		}
		if (!BattleNet.HasExternalChallenge())
		{
			return;
		}
		if (this.m_reconnectNumAttempts >= 4)
		{
			this.FullResetRequired = true;
			return;
		}
		this.m_reconnectTimer -= Time.deltaTime;
		if (this.m_reconnectTimer <= 0f)
		{
			float reconnectTimer = this.RECONNECT_RATE_SECONDS[Mathf.Min(this.m_reconnectNumAttempts, this.RECONNECT_RATE_SECONDS.Length - 1)];
			this.m_reconnectTimer = reconnectTimer;
			this.m_reconnectNumAttempts++;
			global::Log.Offline.PrintDebug("UpdateWhileDisconnectedFromBattleNet: Attempt to relogin using LoginManager::LoginUsingStoredWebToken() | BattleNetStatus={0} | NumAttempts={1}", new object[]
			{
				((Network.BnetLoginState)BattleNet.BattleNetStatus()).ToString(),
				this.m_reconnectNumAttempts
			});
			if (ReconnectMgr.m_authTokenCache.LoginUsingStoredWebToken())
			{
				global::Log.Offline.PrintDebug("UpdateWhileDisconnectedFromBattleNet: Stored web token provided to BattleNet successfully.", Array.Empty<object>());
				return;
			}
			global::Log.Offline.PrintWarning("UpdateWhileDisconnectedFromBattleNet: Failed to reconnect using stored web token.", Array.Empty<object>());
		}
	}

	// Token: 0x060080B0 RID: 32944 RVA: 0x0029D150 File Offset: 0x0029B350
	private void OnBoxReconnectComplete()
	{
		global::Log.Offline.PrintDebug("ReconnectMgr: Reconnect Successful!", Array.Empty<object>());
		this.m_initializedForOfflineAccess = false;
		LoginManager.Get().BeginLoginProcess();
		LoginManager.Get().OnFullLoginFlowComplete += this.OnReconnectLoginComplete;
		LoginManager.Get().OnAchievesLoaded += this.OnReconnectAchievesLoaded;
	}

	// Token: 0x060080B1 RID: 32945 RVA: 0x0029D1B0 File Offset: 0x0029B3B0
	private void OnReconnectAchievesLoaded()
	{
		LoginManager.Get().OnAchievesLoaded -= this.OnReconnectAchievesLoaded;
		if (this.OnReconnectComplete != null)
		{
			this.OnReconnectComplete();
		}
		if (!LoginManager.Get().AttemptToReconnectToGame(new ReconnectMgr.TimeoutCallback(this.OnLoginReconnectToGameTimeout)))
		{
			this.ShowIntroPopups();
		}
	}

	// Token: 0x060080B2 RID: 32946 RVA: 0x0029D204 File Offset: 0x0029B404
	private bool OnLoginReconnectToGameTimeout(object userData)
	{
		this.ShowIntroPopups();
		return true;
	}

	// Token: 0x060080B3 RID: 32947 RVA: 0x0029D20D File Offset: 0x0029B40D
	private void ShowIntroPopups()
	{
		Processor.RunCoroutine(this.OnReconnectAchievesLoaded_ShowIntroPopupsCoroutine(), null);
	}

	// Token: 0x060080B4 RID: 32948 RVA: 0x0029D21C File Offset: 0x0029B41C
	private IEnumerator OnReconnectAchievesLoaded_ShowIntroPopupsCoroutine()
	{
		while (DialogManager.Get().ShowingDialog())
		{
			yield return new WaitForEndOfFrame();
		}
		while (CollectionManager.Get() == null || CollectionManager.Get().IsFullyLoaded())
		{
			yield return new WaitForEndOfFrame();
		}
		JobDefinition jobDefinition = Processor.QueueJob("LoginManager.ShowIntroPopups", LoginManager.Get().ShowIntroPopups(), Array.Empty<IJobDependency>());
		Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow(), new IJobDependency[]
		{
			jobDefinition.CreateDependency()
		});
		yield break;
	}

	// Token: 0x060080B5 RID: 32949 RVA: 0x0029D224 File Offset: 0x0029B424
	private void OnReconnectLoginComplete()
	{
		LoginManager.Get().OnFullLoginFlowComplete -= this.OnReconnectLoginComplete;
		PopupDisplayManager.Get().ShowAnyOutstandingPopups();
		if (this.m_nextReLoginCallback != null)
		{
			this.m_nextReLoginCallback();
			this.m_nextReLoginCallback = null;
		}
	}

	// Token: 0x060080B6 RID: 32950 RVA: 0x0029D260 File Offset: 0x0029B460
	private void ShowGameplayReconnectingDialog()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING_HEADER");
		ReconnectType reconnectType = this.m_reconnectType;
		if (reconnectType == ReconnectType.LOGIN)
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING_LOGIN");
		}
		else
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING");
		}
		if (HearthstoneApplication.CanQuitGame)
		{
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_RECONNECT_EXIT_BUTTON");
		}
		else
		{
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		}
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnGameplayReconnectingDialogResponse);
		DialogManager.Get().ShowPopup(popupInfo, new DialogManager.DialogProcessCallback(this.OnGameplayReconnectingDialogProcessed));
	}

	// Token: 0x060080B7 RID: 32951 RVA: 0x0029D30C File Offset: 0x0029B50C
	private bool OnGameplayReconnectingDialogProcessed(DialogBase dialog, object userData)
	{
		if (!this.IsReconnecting())
		{
			return false;
		}
		this.m_gameplayReconnectDialog = (AlertPopup)dialog;
		if (this.IsStartingReconnectGame())
		{
			this.ChangeGameplayDialogToReconnected();
		}
		return true;
	}

	// Token: 0x060080B8 RID: 32952 RVA: 0x0029D333 File Offset: 0x0029B533
	private void OnGameplayReconnectingDialogResponse(AlertPopup.Response response, object userData)
	{
		this.m_gameplayReconnectDialog = null;
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x060080B9 RID: 32953 RVA: 0x0029D348 File Offset: 0x0029B548
	private void ChangeGameplayDialogToReconnected()
	{
		if (this.m_gameplayReconnectDialog == null)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTED_HEADER");
		ReconnectType reconnectType = this.m_reconnectType;
		if (reconnectType == ReconnectType.LOGIN)
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTED_LOGIN");
		}
		else
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTED");
		}
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		popupInfo.m_showAlertIcon = true;
		this.m_gameplayReconnectDialog.UpdateInfo(popupInfo);
		LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnPreviousSceneDestroyed));
	}

	// Token: 0x060080BA RID: 32954 RVA: 0x0029D3D8 File Offset: 0x0029B5D8
	private void OnPreviousSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(new LoadingScreen.PreviousSceneDestroyedCallback(this.OnPreviousSceneDestroyed));
		this.HideGameplayReconnectDialog();
	}

	// Token: 0x060080BB RID: 32955 RVA: 0x0029D3F8 File Offset: 0x0029B5F8
	private void ChangeGameplayDialogToTimeout()
	{
		if (this.m_gameplayReconnectDialog == null)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_TIMEOUT_HEADER");
		popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_TIMEOUT");
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnTimeoutGameplayDialogResponse);
		this.m_gameplayReconnectDialog.UpdateInfo(popupInfo);
	}

	// Token: 0x060080BC RID: 32956 RVA: 0x0029D466 File Offset: 0x0029B666
	private void OnTimeoutGameplayDialogResponse(AlertPopup.Response response, object userData)
	{
		this.m_gameplayReconnectDialog = null;
		if (!Network.IsLoggedIn())
		{
			if (HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
				return;
			}
			HearthstoneApplication.Get().Exit();
		}
	}

	// Token: 0x060080BD RID: 32957 RVA: 0x0029D497 File Offset: 0x0029B697
	public void HideGameplayReconnectDialog()
	{
		if (this.m_gameplayReconnectDialog == null)
		{
			return;
		}
		this.m_gameplayReconnectDialog.Hide();
		this.m_gameplayReconnectDialog = null;
	}

	// Token: 0x060080BE RID: 32958 RVA: 0x0029D4BC File Offset: 0x0029B6BC
	private NetCache.ProfileNoticeDisconnectedGame GetDCGameNotice()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject == null || netObject.Notices == null || netObject.Notices.Count == 0)
		{
			return null;
		}
		NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame = null;
		List<NetCache.ProfileNoticeDisconnectedGame> list = new List<NetCache.ProfileNoticeDisconnectedGame>();
		foreach (NetCache.ProfileNotice profileNotice in netObject.Notices)
		{
			if (profileNotice is NetCache.ProfileNoticeDisconnectedGame)
			{
				NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame2 = profileNotice as NetCache.ProfileNoticeDisconnectedGame;
				list.Add(profileNoticeDisconnectedGame2);
				if (profileNoticeDisconnectedGame == null)
				{
					profileNoticeDisconnectedGame = profileNoticeDisconnectedGame2;
				}
				else if (profileNoticeDisconnectedGame2.NoticeID > profileNoticeDisconnectedGame.NoticeID)
				{
					profileNoticeDisconnectedGame = profileNoticeDisconnectedGame2;
				}
			}
		}
		if (profileNoticeDisconnectedGame == null)
		{
			return null;
		}
		foreach (NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame3 in list)
		{
			if (profileNoticeDisconnectedGame3.NoticeID != profileNoticeDisconnectedGame.NoticeID)
			{
				this.AckNotice(profileNoticeDisconnectedGame3);
			}
		}
		return profileNoticeDisconnectedGame;
	}

	// Token: 0x060080BF RID: 32959 RVA: 0x0029D5C0 File Offset: 0x0029B7C0
	private void AckNotice(NetCache.ProfileNoticeDisconnectedGame notice)
	{
		Network.Get().AckNotice(notice.NoticeID);
	}

	// Token: 0x060080C0 RID: 32960 RVA: 0x0029D5D4 File Offset: 0x0029B7D4
	private void StartGame(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo, int scenarioId = 0, bool loadGameState = false)
	{
		this.m_savedStartGameParams.GameType = gameType;
		this.m_savedStartGameParams.FormatType = formatType;
		this.m_savedStartGameParams.ReconnectType = reconnectType;
		this.m_savedStartGameParams.ServerInfo = serverInfo;
		this.m_savedStartGameParams.ScenarioId = scenarioId;
		this.m_savedStartGameParams.LoadGame = loadGameState;
		this.StartGame_Internal();
	}

	// Token: 0x060080C1 RID: 32961 RVA: 0x0029D632 File Offset: 0x0029B832
	private void StartGame_Internal()
	{
		this.StopReconnectCoroutine();
		GameMgr.Get().ReconnectGame(this.m_savedStartGameParams.GameType, this.m_savedStartGameParams.FormatType, this.m_savedStartGameParams.ReconnectType, this.m_savedStartGameParams.ServerInfo);
	}

	// Token: 0x060080C2 RID: 32962 RVA: 0x0029D670 File Offset: 0x0029B870
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state != FindGameState.SERVER_GAME_STARTED)
		{
			if (state == FindGameState.SERVER_GAME_CANCELED)
			{
				if (this.IsReconnecting() || this.IsRestoringGameStateFromDatabase())
				{
					this.OnReconnectTimeout();
					return true;
				}
			}
		}
		else if (this.IsReconnecting() || this.IsRestoringGameStateFromDatabase())
		{
			this.m_timeoutListeners.Clear();
			this.ChangeGameplayDialogToReconnected();
			this.ClearReconnectData();
		}
		return false;
	}

	// Token: 0x060080C3 RID: 32963 RVA: 0x0029D6D4 File Offset: 0x0029B8D4
	private void FireTimeoutEvent()
	{
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.ReconnectEnd(false);
		}
		ReconnectMgr.TimeoutListener[] array = this.m_timeoutListeners.ToArray();
		this.m_timeoutListeners.Clear();
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			flag = (array[i].Fire() || flag);
		}
		if (!flag && Network.IsLoggedIn())
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
	}

	// Token: 0x060080C4 RID: 32964 RVA: 0x0029D73C File Offset: 0x0029B93C
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.ReconnectEnd(false);
		}
		this.ClearReconnectData();
		this.InitializeForOfflineAccess(message);
	}

	// Token: 0x060080C5 RID: 32965 RVA: 0x0029D768 File Offset: 0x0029B968
	private void OnAchievesLoaded()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		this.m_allowOfflineActivity = netObject.AllowOfflineClientActivity;
	}

	// Token: 0x0400697C RID: 27004
	private global::Map<GameType, string> m_gameTypeNameKeys = new global::Map<GameType, string>
	{
		{
			GameType.GT_VS_FRIEND,
			"GLUE_RECONNECT_GAME_TYPE_FRIENDLY"
		},
		{
			GameType.GT_ARENA,
			"GLUE_RECONNECT_GAME_TYPE_ARENA"
		},
		{
			GameType.GT_CASUAL,
			"GLUE_RECONNECT_GAME_TYPE_UNRANKED"
		},
		{
			GameType.GT_RANKED,
			"GLUE_RECONNECT_GAME_TYPE_RANKED"
		},
		{
			GameType.GT_TAVERNBRAWL,
			"GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL"
		},
		{
			GameType.GT_FSG_BRAWL_VS_FRIEND,
			"GLUE_RECONNECT_GAME_TYPE_FRIENDLY"
		},
		{
			GameType.GT_FSG_BRAWL,
			"GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL"
		},
		{
			GameType.GT_FSG_BRAWL_1P_VS_AI,
			"GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL"
		},
		{
			GameType.GT_FSG_BRAWL_2P_COOP,
			"GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL"
		}
	};

	// Token: 0x0400697D RID: 27005
	private float[] RECONNECT_RATE_SECONDS = new float[]
	{
		1f,
		2f,
		3f,
		5f
	};

	// Token: 0x0400697E RID: 27006
	private const int RECONNECT_RETRY_ATTEMPTS_ALLOWED = 4;

	// Token: 0x0400697F RID: 27007
	private AlertPopup m_gameplayReconnectDialog;

	// Token: 0x04006980 RID: 27008
	private ReconnectType m_reconnectType;

	// Token: 0x04006981 RID: 27009
	private float m_reconnectStartTimestamp;

	// Token: 0x04006982 RID: 27010
	private float m_retryStartTimestamp;

	// Token: 0x04006983 RID: 27011
	private float m_reconnectTimer;

	// Token: 0x04006984 RID: 27012
	private int m_reconnectNumAttempts;

	// Token: 0x04006985 RID: 27013
	private bool m_bypassReconnect;

	// Token: 0x04006986 RID: 27014
	private ReconnectMgr.SavedStartGameParameters m_savedStartGameParams = new ReconnectMgr.SavedStartGameParameters();

	// Token: 0x04006987 RID: 27015
	private List<ReconnectMgr.TimeoutListener> m_timeoutListeners = new List<ReconnectMgr.TimeoutListener>();

	// Token: 0x04006988 RID: 27016
	private bool m_allowOfflineActivity;

	// Token: 0x04006989 RID: 27017
	private bool m_initializedForOfflineAccess;

	// Token: 0x0400698A RID: 27018
	private Action m_nextReLoginCallback;

	// Token: 0x0400698B RID: 27019
	private IEnumerator m_reconnectCoroutine;

	// Token: 0x0400698C RID: 27020
	private static OfflineAuthTokenCache m_authTokenCache;

	// Token: 0x0400698D RID: 27021
	private NetworkReachabilityManager m_networkReachabilityManager;

	// Token: 0x020025D1 RID: 9681
	// (Invoke) Token: 0x060134B4 RID: 79028
	public delegate bool TimeoutCallback(object userData);

	// Token: 0x020025D2 RID: 9682
	private class TimeoutListener : global::EventListener<ReconnectMgr.TimeoutCallback>
	{
		// Token: 0x060134B7 RID: 79031 RVA: 0x00530543 File Offset: 0x0052E743
		public bool Fire()
		{
			return this.m_callback(this.m_userData);
		}
	}

	// Token: 0x020025D3 RID: 9683
	private class SavedStartGameParameters
	{
		// Token: 0x060134B9 RID: 79033 RVA: 0x00530560 File Offset: 0x0052E760
		public override string ToString()
		{
			return string.Format("GameType: {0}, FormatType: {1}, ReconnectType: {2}, ScenarioId: {3}, LoadGame: {4}", new object[]
			{
				this.GameType,
				this.FormatType,
				this.ReconnectType,
				this.ScenarioId,
				this.LoadGame
			});
		}

		// Token: 0x0400EEC9 RID: 61129
		public GameType GameType;

		// Token: 0x0400EECA RID: 61130
		public FormatType FormatType;

		// Token: 0x0400EECB RID: 61131
		public ReconnectType ReconnectType;

		// Token: 0x0400EECC RID: 61132
		public GameServerInfo ServerInfo;

		// Token: 0x0400EECD RID: 61133
		public int ScenarioId;

		// Token: 0x0400EECE RID: 61134
		public bool LoadGame;
	}
}
