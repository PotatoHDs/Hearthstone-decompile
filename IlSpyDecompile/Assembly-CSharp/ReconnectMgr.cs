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

public class ReconnectMgr : IService, IHasUpdate
{
	public delegate bool TimeoutCallback(object userData);

	private class TimeoutListener : EventListener<TimeoutCallback>
	{
		public bool Fire()
		{
			return m_callback(m_userData);
		}
	}

	private class SavedStartGameParameters
	{
		public GameType GameType;

		public FormatType FormatType;

		public ReconnectType ReconnectType;

		public GameServerInfo ServerInfo;

		public int ScenarioId;

		public bool LoadGame;

		public override string ToString()
		{
			return $"GameType: {GameType}, FormatType: {FormatType}, ReconnectType: {ReconnectType}, ScenarioId: {ScenarioId}, LoadGame: {LoadGame}";
		}
	}

	private Map<GameType, string> m_gameTypeNameKeys = new Map<GameType, string>
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

	private float[] RECONNECT_RATE_SECONDS = new float[4] { 1f, 2f, 3f, 5f };

	private const int RECONNECT_RETRY_ATTEMPTS_ALLOWED = 4;

	private AlertPopup m_gameplayReconnectDialog;

	private ReconnectType m_reconnectType;

	private float m_reconnectStartTimestamp;

	private float m_retryStartTimestamp;

	private float m_reconnectTimer;

	private int m_reconnectNumAttempts;

	private bool m_bypassReconnect;

	private SavedStartGameParameters m_savedStartGameParams = new SavedStartGameParameters();

	private List<TimeoutListener> m_timeoutListeners = new List<TimeoutListener>();

	private bool m_allowOfflineActivity;

	private bool m_initializedForOfflineAccess;

	private Action m_nextReLoginCallback;

	private IEnumerator m_reconnectCoroutine;

	private static OfflineAuthTokenCache m_authTokenCache;

	private NetworkReachabilityManager m_networkReachabilityManager;

	public bool FullResetRequired { get; set; }

	public bool UpdateRequired { get; set; }

	public bool ReconnectBlockedByInactivity { get; set; }

	public event Action OnReconnectComplete;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		Network network = Network.Get();
		network.AddBnetErrorListener(OnBnetError);
		network.OnDisconnectedFromBattleNet += OnDisconnectedFromBattleNet;
		serviceLocator.Get<LoginManager>().OnAchievesLoaded += OnAchievesLoaded;
		m_authTokenCache = new OfflineAuthTokenCache(Log.Offline);
		HearthstoneApplication.Get().WillReset += WillReset;
		yield break;
	}

	public void Update()
	{
		CheckGameplayReconnectTimeout();
		CheckGameplayReconnectRetry();
		m_authTokenCache.RefreshTokenCacheIfNeeded();
		if (!Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora())
		{
			UpdateWhileDisconnectedFromBattleNet();
		}
		else if (Network.IsLoggedIn() && m_initializedForOfflineAccess)
		{
			OnBoxReconnectComplete();
		}
	}

	public Type[] GetDependencies()
	{
		return new Type[3]
		{
			typeof(LoginManager),
			typeof(GameMgr),
			typeof(NetworkReachabilityManager)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<Network>(out var service))
		{
			service.RemoveBnetErrorListener(OnBnetError);
			service.OnDisconnectedFromBattleNet -= OnDisconnectedFromBattleNet;
		}
		if (HearthstoneServices.TryGet<GameMgr>(out var service2))
		{
			service2.UnregisterFindGameEvent(OnFindGameEvent);
		}
		if (HearthstoneServices.TryGet<LoginManager>(out var service3))
		{
			service3.OnAchievesLoaded -= OnAchievesLoaded;
		}
		HearthstoneApplication.Get().WillReset -= WillReset;
	}

	public static ReconnectMgr Get()
	{
		return HearthstoneServices.Get<ReconnectMgr>();
	}

	public bool IsReconnecting()
	{
		return m_reconnectType != ReconnectType.INVALID;
	}

	public bool IsRestoringGameStateFromDatabase()
	{
		if (m_savedStartGameParams != null)
		{
			return m_savedStartGameParams.LoadGame;
		}
		return false;
	}

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

	public static bool IsReconnectAllowed(FatalErrorMessage fatalErrorMessage)
	{
		if (fatalErrorMessage != null)
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Checking Fatal Error Reason: {0}", fatalErrorMessage.m_reason);
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject == null)
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Unable to retrieve guardian vars.");
			return false;
		}
		if (!netObject.AllowOfflineClientActivity)
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect disabled by guardian var.");
			return false;
		}
		if (fatalErrorMessage != null && !FatalErrorMgr.IsReconnectAllowedBasedOnFatalErrorReason(fatalErrorMessage.m_reason))
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect not allowed because of Fatal Error Reason. Reason={0}", fatalErrorMessage.m_reason);
			return false;
		}
		if (m_authTokenCache != null && m_authTokenCache.FailedToGenerateLoginWebToken)
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect not allowed because you failed to generate a valid login token.");
			return false;
		}
		if (!GameUtils.AreAllTutorialsComplete())
		{
			Log.Offline.PrintDebug("ReconnectMgr.IsReconnectAllowed() - Reconnect disabled for players in tutorial.");
			return false;
		}
		return true;
	}

	public float GetTimeout()
	{
		if (HearthstoneApplication.IsInternal())
		{
			return Options.Get().GetFloat(Option.RECONNECT_TIMEOUT);
		}
		return (float)OptionDataTables.s_defaultsMap[Option.RECONNECT_TIMEOUT];
	}

	public float GetRetryTime()
	{
		if (HearthstoneApplication.IsInternal())
		{
			return Options.Get().GetFloat(Option.RECONNECT_RETRY_TIME);
		}
		return (float)OptionDataTables.s_defaultsMap[Option.RECONNECT_RETRY_TIME];
	}

	public bool AddTimeoutListener(TimeoutCallback callback)
	{
		return AddTimeoutListener(callback, null);
	}

	public bool AddTimeoutListener(TimeoutCallback callback, object userData)
	{
		TimeoutListener timeoutListener = new TimeoutListener();
		timeoutListener.SetCallback(callback);
		timeoutListener.SetUserData(userData);
		if (m_timeoutListeners.Contains(timeoutListener))
		{
			return false;
		}
		m_timeoutListeners.Add(timeoutListener);
		return true;
	}

	public bool RemoveTimeoutListener(TimeoutCallback callback)
	{
		return RemoveTimeoutListener(callback, null);
	}

	public bool RemoveTimeoutListener(TimeoutCallback callback, object userData)
	{
		TimeoutListener timeoutListener = new TimeoutListener();
		timeoutListener.SetCallback(callback);
		timeoutListener.SetUserData(userData);
		return m_timeoutListeners.Remove(timeoutListener);
	}

	private void HandleDisconnectedGameResult()
	{
		NetCache.ProfileNoticeDisconnectedGame dCGameNotice = GetDCGameNotice();
		if (dCGameNotice != null && dCGameNotice.GameResult != ProfileNoticeDisconnectedGameResult.GameResult.GR_PLAYING)
		{
			OnGameResult(dCGameNotice);
		}
	}

	public bool ReconnectToGameFromLogin()
	{
		HandleDisconnectedGameResult();
		NetCache.NetCacheDisconnectedGame netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisconnectedGame>();
		if (netObject == null || netObject.ServerInfo == null || (HearthstoneApplication.IsInternal() && !Vars.Key("Developer.ReconnectToGameFromLogin").GetBool(def: true)))
		{
			return false;
		}
		StartReconnecting(ReconnectType.LOGIN);
		ReconnectToGameFromLogin_RequestRequiredData(netObject);
		return true;
	}

	private void ReconnectToGameFromLogin_RequestRequiredData(NetCache.NetCacheDisconnectedGame dcGame)
	{
		GameType gameType = dcGame.GameType;
		if (gameType == GameType.GT_BATTLEGROUNDS)
		{
			Network.Get().RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, ReconnectToGameFromLogin_OnBaconRatingInfo);
			Network.Get().RequestBaconRatingInfo();
		}
		else
		{
			ReconnectToGameFromLogin_StartGame(dcGame);
		}
	}

	private void ReconnectToGameFromLogin_OnBaconRatingInfo()
	{
		Network.Get().RemoveNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, ReconnectToGameFromLogin_OnBaconRatingInfo);
		NetCache.NetCacheDisconnectedGame netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDisconnectedGame>();
		ReconnectToGameFromLogin_StartGame(netObject);
	}

	private void ReconnectToGameFromLogin_StartGame(NetCache.NetCacheDisconnectedGame dcGame)
	{
		StartGame(dcGame.GameType, dcGame.FormatType, ReconnectType.LOGIN, dcGame.ServerInfo, dcGame.ServerInfo.Mission, dcGame.LoadGameState);
	}

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
		HideGameplayReconnectDialog();
		GameType gameType = GameMgr.Get().GetGameType();
		FormatType formatType = GameMgr.Get().GetFormatType();
		ReconnectType reconnectType = ReconnectType.GAMEPLAY;
		StartReconnecting(reconnectType);
		m_reconnectCoroutine = WaitForInternetAndReconnect(gameType, formatType, reconnectType, lastGameServerJoined);
		HearthstoneApplication.Get().StartCoroutine(m_reconnectCoroutine);
		return true;
	}

	public void StopReconnectCoroutine()
	{
		if (m_reconnectCoroutine != null)
		{
			HearthstoneApplication.Get().StopCoroutine(m_reconnectCoroutine);
			m_reconnectCoroutine = null;
		}
	}

	private IEnumerator WaitForInternetAndReconnect(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo)
	{
		while (!m_networkReachabilityManager.InternetAvailable_Cached)
		{
			yield return new WaitForSeconds(1f);
		}
		StartGame(gameType, formatType, reconnectType, serverInfo);
		m_reconnectCoroutine = null;
	}

	public bool ShowDisconnectedGameResult(NetCache.ProfileNoticeDisconnectedGame dcGame)
	{
		if (!GameUtils.IsMatchmadeGameType(dcGame.GameType))
		{
			return false;
		}
		TimeSpan timeSpan = DateTime.UtcNow - DateTime.FromFileTimeUtc(dcGame.Date);
		Log.ReturningPlayer.Print("This user disconnected from his or her last game {0} minutes ago.", timeSpan.TotalMinutes);
		if (timeSpan.TotalHours > 24.0)
		{
			Log.All.Print("Not showing the Disconnected Game Result because the game was disconnected from {0} hours ago.", timeSpan.TotalHours);
			return false;
		}
		ProfileNoticeDisconnectedGameResult.GameResult gameResult = dcGame.GameResult;
		if ((uint)(gameResult - 2) > 1u)
		{
			Debug.LogError($"ReconnectMgr.ShowDisconnectedGameResult() - unhandled game result {dcGame.GameResult}");
			return false;
		}
		if (dcGame.GameType == GameType.GT_UNKNOWN)
		{
			return false;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_RECONNECT_RESULT_HEADER");
		string text = null;
		if (dcGame.GameResult == ProfileNoticeDisconnectedGameResult.GameResult.GR_TIE)
		{
			text = "GLUE_RECONNECT_RESULT_TIE";
		}
		else
		{
			switch (dcGame.YourResult)
			{
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_WON:
				text = "GLUE_RECONNECT_RESULT_WIN";
				break;
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_LOST:
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_QUIT:
				text = "GLUE_RECONNECT_RESULT_LOSE";
				break;
			case ProfileNoticeDisconnectedGameResult.PlayerResult.PR_DISCONNECTED:
				text = "GLUE_RECONNECT_RESULT_DISCONNECT";
				break;
			default:
				Debug.LogError($"ReconnectMgr.ShowDisconnectedGameResult() - unhandled player result {dcGame.YourResult}");
				return false;
			}
		}
		popupInfo.m_text = GameStrings.Format(text, GetGameTypeName(dcGame.GameType, dcGame.MissionId));
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_showAlertIcon = true;
		DialogManager.Get().ShowPopup(popupInfo);
		return true;
	}

	private string GetGameTypeName(GameType gameType, int missionId)
	{
		AdventureDbfRecord adventureRecordFromMissionId = GameUtils.GetAdventureRecordFromMissionId(missionId);
		if (adventureRecordFromMissionId != null)
		{
			return adventureRecordFromMissionId.ID switch
			{
				1 => GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_TUTORIAL"), 
				2 => GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_PRACTICE"), 
				3 => GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_NAXXRAMAS"), 
				4 => GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_BRM"), 
				7 => GameStrings.Get("GLUE_RECONNECT_GAME_TYPE_TAVERN_BRAWL"), 
				_ => adventureRecordFromMissionId.Name, 
			};
		}
		if (m_gameTypeNameKeys.TryGetValue(gameType, out var value))
		{
			return GameStrings.Get(value);
		}
		Error.AddDevFatal("ReconnectMgr.GetGameTypeName() - no name for mission {0} gameType {1}", missionId, gameType);
		return string.Empty;
	}

	public void SetNextReLoginCallback(Action nextCallback)
	{
		m_nextReLoginCallback = nextCallback;
	}

	private void WillReset()
	{
		m_gameplayReconnectDialog = null;
		FullResetRequired = false;
		UpdateRequired = false;
		m_initializedForOfflineAccess = false;
		ClearReconnectData();
		m_timeoutListeners.Clear();
	}

	private void StartReconnecting(ReconnectType reconnectType)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		m_reconnectType = reconnectType;
		m_reconnectStartTimestamp = realtimeSinceStartup;
		m_retryStartTimestamp = realtimeSinceStartup;
		PerformanceAnalytics.Get()?.ReconnectStart(reconnectType.ToString());
		ShowGameplayReconnectingDialog();
	}

	private void CheckGameplayReconnectTimeout()
	{
		if (IsReconnecting())
		{
			float num = Time.realtimeSinceStartup - m_reconnectStartTimestamp;
			float timeout = GetTimeout();
			if (num >= timeout && !Network.Get().IsConnectedToGameServer())
			{
				OnReconnectTimeout();
			}
		}
	}

	private void CheckGameplayReconnectRetry()
	{
		if (!m_networkReachabilityManager.InternetAvailable_Cached || !IsReconnecting() || Network.Get().IsConnectedToGameServer() || Network.Get().GameServerHasEvents())
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - m_retryStartTimestamp;
		float retryTime = GetRetryTime();
		if (!(num < retryTime))
		{
			if (m_savedStartGameParams.ServerInfo == null)
			{
				Debug.LogError($"m_savedStartGameParams.ServerInfo in CheckGameplayReconnectRetry is null and should not be! {m_savedStartGameParams.ToString()}");
				return;
			}
			m_retryStartTimestamp = realtimeSinceStartup;
			StartGame_Internal();
		}
	}

	private void OnReconnectTimeout()
	{
		SetBypassReconnect(shouldBypass: true);
		ClearReconnectData();
		FireTimeoutEvent();
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			bool flag = GameMgr.Get().IsAI() && !GameMgr.Get().IsTavernBrawl();
			Error.AddFatal(FatalErrorReason.RECONNECT_TIME_OUT, flag ? "GLOBAL_ERROR_NETWORK_ADVENTURE_RECONNECT_TIMEOUT" : "GLOBAL_ERROR_NETWORK_LOST_GAME_CONNECTION");
		}
		else
		{
			AttemptToRestoreGameState();
		}
	}

	private void AttemptToRestoreGameState()
	{
		if (m_savedStartGameParams.LoadGame)
		{
			GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: false);
			GameMgr.Get().FindGame(m_savedStartGameParams.GameType, m_savedStartGameParams.FormatType, m_savedStartGameParams.ScenarioId, 0, 0L, null, null, m_savedStartGameParams.LoadGame);
		}
		else
		{
			ClearReconnectData();
			ChangeGameplayDialogToTimeout();
		}
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (!IsReconnecting() && !IsRestoringGameStateFromDatabase())
		{
			return false;
		}
		ChangeGameplayDialogToTimeout();
		if (m_savedStartGameParams != null)
		{
			m_savedStartGameParams.LoadGame = false;
		}
		return true;
	}

	private void OnDisconnectedFromBattleNet(BattleNetErrors error)
	{
		m_initializedForOfflineAccess = false;
	}

	private void OnGameResult(NetCache.ProfileNoticeDisconnectedGame dcGameNotice)
	{
		ShowDisconnectedGameResult(dcGameNotice);
		AckNotice(dcGameNotice);
	}

	public void SetBypassReconnect(bool shouldBypass)
	{
		m_bypassReconnect = shouldBypass;
	}

	public bool GetBypassReconnect()
	{
		return m_bypassReconnect;
	}

	private void ClearReconnectData()
	{
		m_reconnectType = ReconnectType.INVALID;
		m_reconnectStartTimestamp = 0f;
		m_retryStartTimestamp = 0f;
	}

	private void InitializeForOfflineAccess(FatalErrorMessage fatalErrorMessage)
	{
		if (!m_initializedForOfflineAccess && IsReconnectAllowed(fatalErrorMessage))
		{
			Log.Offline.PrintDebug("ReconnectMgr: Initializing for offline box access.");
			m_initializedForOfflineAccess = true;
			m_reconnectNumAttempts = 0;
			m_reconnectTimer = 0f;
			Network.Get().ResetForNewAuroraConnection();
		}
	}

	private void UpdateWhileDisconnectedFromBattleNet()
	{
		if (!m_allowOfflineActivity || !m_initializedForOfflineAccess || FullResetRequired || ReconnectBlockedByInactivity || !m_networkReachabilityManager.InternetAvailable_Cached || !BattleNet.HasExternalChallenge())
		{
			return;
		}
		if (m_reconnectNumAttempts >= 4)
		{
			FullResetRequired = true;
			return;
		}
		m_reconnectTimer -= Time.deltaTime;
		if (m_reconnectTimer <= 0f)
		{
			float num = (m_reconnectTimer = RECONNECT_RATE_SECONDS[Mathf.Min(m_reconnectNumAttempts, RECONNECT_RATE_SECONDS.Length - 1)]);
			m_reconnectNumAttempts++;
			Log.Offline.PrintDebug("UpdateWhileDisconnectedFromBattleNet: Attempt to relogin using LoginManager::LoginUsingStoredWebToken() | BattleNetStatus={0} | NumAttempts={1}", ((Network.BnetLoginState)BattleNet.BattleNetStatus()).ToString(), m_reconnectNumAttempts);
			if (m_authTokenCache.LoginUsingStoredWebToken())
			{
				Log.Offline.PrintDebug("UpdateWhileDisconnectedFromBattleNet: Stored web token provided to BattleNet successfully.");
			}
			else
			{
				Log.Offline.PrintWarning("UpdateWhileDisconnectedFromBattleNet: Failed to reconnect using stored web token.");
			}
		}
	}

	private void OnBoxReconnectComplete()
	{
		Log.Offline.PrintDebug("ReconnectMgr: Reconnect Successful!");
		m_initializedForOfflineAccess = false;
		LoginManager.Get().BeginLoginProcess();
		LoginManager.Get().OnFullLoginFlowComplete += OnReconnectLoginComplete;
		LoginManager.Get().OnAchievesLoaded += OnReconnectAchievesLoaded;
	}

	private void OnReconnectAchievesLoaded()
	{
		LoginManager.Get().OnAchievesLoaded -= OnReconnectAchievesLoaded;
		if (this.OnReconnectComplete != null)
		{
			this.OnReconnectComplete();
		}
		if (!LoginManager.Get().AttemptToReconnectToGame(OnLoginReconnectToGameTimeout))
		{
			ShowIntroPopups();
		}
	}

	private bool OnLoginReconnectToGameTimeout(object userData)
	{
		ShowIntroPopups();
		return true;
	}

	private void ShowIntroPopups()
	{
		Processor.RunCoroutine(OnReconnectAchievesLoaded_ShowIntroPopupsCoroutine());
	}

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
		JobDefinition jobDefinition = Processor.QueueJob("LoginManager.ShowIntroPopups", LoginManager.Get().ShowIntroPopups());
		Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow(), jobDefinition.CreateDependency());
	}

	private void OnReconnectLoginComplete()
	{
		LoginManager.Get().OnFullLoginFlowComplete -= OnReconnectLoginComplete;
		PopupDisplayManager.Get().ShowAnyOutstandingPopups();
		if (m_nextReLoginCallback != null)
		{
			m_nextReLoginCallback();
			m_nextReLoginCallback = null;
		}
	}

	private void ShowGameplayReconnectingDialog()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING_HEADER");
		ReconnectType reconnectType = m_reconnectType;
		if (reconnectType == ReconnectType.LOGIN)
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING_LOGIN");
		}
		else
		{
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTING");
		}
		if ((bool)HearthstoneApplication.CanQuitGame)
		{
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_RECONNECT_EXIT_BUTTON");
		}
		else
		{
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		}
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseCallback = OnGameplayReconnectingDialogResponse;
		DialogManager.Get().ShowPopup(popupInfo, OnGameplayReconnectingDialogProcessed);
	}

	private bool OnGameplayReconnectingDialogProcessed(DialogBase dialog, object userData)
	{
		if (!IsReconnecting())
		{
			return false;
		}
		m_gameplayReconnectDialog = (AlertPopup)dialog;
		if (IsStartingReconnectGame())
		{
			ChangeGameplayDialogToReconnected();
		}
		return true;
	}

	private void OnGameplayReconnectingDialogResponse(AlertPopup.Response response, object userData)
	{
		m_gameplayReconnectDialog = null;
		HearthstoneApplication.Get().Exit();
	}

	private void ChangeGameplayDialogToReconnected()
	{
		if (!(m_gameplayReconnectDialog == null))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_RECONNECTED_HEADER");
			ReconnectType reconnectType = m_reconnectType;
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
			m_gameplayReconnectDialog.UpdateInfo(popupInfo);
			LoadingScreen.Get().RegisterPreviousSceneDestroyedListener(OnPreviousSceneDestroyed);
		}
	}

	private void OnPreviousSceneDestroyed(object userData)
	{
		LoadingScreen.Get().UnregisterPreviousSceneDestroyedListener(OnPreviousSceneDestroyed);
		HideGameplayReconnectDialog();
	}

	private void ChangeGameplayDialogToTimeout()
	{
		if (!(m_gameplayReconnectDialog == null))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_RECONNECT_TIMEOUT_HEADER");
			popupInfo.m_text = GameStrings.Get("GLOBAL_RECONNECT_TIMEOUT");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseCallback = OnTimeoutGameplayDialogResponse;
			m_gameplayReconnectDialog.UpdateInfo(popupInfo);
		}
	}

	private void OnTimeoutGameplayDialogResponse(AlertPopup.Response response, object userData)
	{
		m_gameplayReconnectDialog = null;
		if (!Network.IsLoggedIn())
		{
			if ((bool)HearthstoneApplication.AllowResetFromFatalError)
			{
				HearthstoneApplication.Get().Reset();
			}
			else
			{
				HearthstoneApplication.Get().Exit();
			}
		}
	}

	public void HideGameplayReconnectDialog()
	{
		if (!(m_gameplayReconnectDialog == null))
		{
			m_gameplayReconnectDialog.Hide();
			m_gameplayReconnectDialog = null;
		}
	}

	private NetCache.ProfileNoticeDisconnectedGame GetDCGameNotice()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject == null || netObject.Notices == null || netObject.Notices.Count == 0)
		{
			return null;
		}
		NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame = null;
		List<NetCache.ProfileNoticeDisconnectedGame> list = new List<NetCache.ProfileNoticeDisconnectedGame>();
		foreach (NetCache.ProfileNotice notice in netObject.Notices)
		{
			if (notice is NetCache.ProfileNoticeDisconnectedGame)
			{
				NetCache.ProfileNoticeDisconnectedGame profileNoticeDisconnectedGame2 = notice as NetCache.ProfileNoticeDisconnectedGame;
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
		foreach (NetCache.ProfileNoticeDisconnectedGame item in list)
		{
			if (item.NoticeID != profileNoticeDisconnectedGame.NoticeID)
			{
				AckNotice(item);
			}
		}
		return profileNoticeDisconnectedGame;
	}

	private void AckNotice(NetCache.ProfileNoticeDisconnectedGame notice)
	{
		Network.Get().AckNotice(notice.NoticeID);
	}

	private void StartGame(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo, int scenarioId = 0, bool loadGameState = false)
	{
		m_savedStartGameParams.GameType = gameType;
		m_savedStartGameParams.FormatType = formatType;
		m_savedStartGameParams.ReconnectType = reconnectType;
		m_savedStartGameParams.ServerInfo = serverInfo;
		m_savedStartGameParams.ScenarioId = scenarioId;
		m_savedStartGameParams.LoadGame = loadGameState;
		StartGame_Internal();
	}

	private void StartGame_Internal()
	{
		StopReconnectCoroutine();
		GameMgr.Get().ReconnectGame(m_savedStartGameParams.GameType, m_savedStartGameParams.FormatType, m_savedStartGameParams.ReconnectType, m_savedStartGameParams.ServerInfo);
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.SERVER_GAME_STARTED:
			if (IsReconnecting() || IsRestoringGameStateFromDatabase())
			{
				m_timeoutListeners.Clear();
				ChangeGameplayDialogToReconnected();
				ClearReconnectData();
			}
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			if (IsReconnecting() || IsRestoringGameStateFromDatabase())
			{
				OnReconnectTimeout();
				return true;
			}
			break;
		}
		return false;
	}

	private void FireTimeoutEvent()
	{
		PerformanceAnalytics.Get()?.ReconnectEnd(success: false);
		TimeoutListener[] array = m_timeoutListeners.ToArray();
		m_timeoutListeners.Clear();
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			flag = array[i].Fire() || flag;
		}
		if (!flag && Network.IsLoggedIn())
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		PerformanceAnalytics.Get()?.ReconnectEnd(success: false);
		ClearReconnectData();
		InitializeForOfflineAccess(message);
	}

	private void OnAchievesLoaded()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		m_allowOfflineActivity = netObject.AllowOfflineClientActivity;
	}
}
