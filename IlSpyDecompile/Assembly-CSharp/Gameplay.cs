using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone;
using PegasusGame;
using UnityEngine;

public class Gameplay : PegasusScene
{
	private static Gameplay s_instance;

	private bool m_unloading;

	private BnetErrorInfo m_lastFatalBnetErrorInfo;

	private bool m_handleLastFatalBnetErrorNow;

	private float m_boardProgress;

	private List<NameBanner> m_nameBanners = new List<NameBanner>();

	private NameBanner m_nameBannerGamePlayPhone;

	private int m_numBannersRequested;

	private Actor m_cardDrawStandIn;

	private BoardLayout m_boardLayout;

	private bool m_criticalAssetsLoaded;

	private Queue<List<Network.PowerHistory>> m_queuedPowerHistory = new Queue<List<Network.PowerHistory>>();

	private float? m_originalTimeScale;

	private Camera m_inputCamera;

	protected override void Awake()
	{
		Log.LoadingScreen.Print("Gameplay.Awake()");
		base.Awake();
		s_instance = this;
		GameState gameState = GameState.Initialize();
		if (ShouldHandleDisconnect())
		{
			Log.LoadingScreen.PrintWarning("Gameplay.Awake() - DISCONNECTED");
			HandleDisconnect();
			return;
		}
		Network.Get().SetGameServerDisconnectEventListener(OnDisconnect);
		CheatMgr.Get().RegisterCategory("gameplay:more");
		CheatMgr.Get().RegisterCheatHandler("saveme", OnProcessCheat_saveme);
		if (!HearthstoneApplication.IsPublic())
		{
			CheatMgr.Get().RegisterCheatHandler("entitycount", GameDebugDisplay.Get().ToggleEntityCount);
			CheatMgr.Get().RegisterCheatHandler("showtag", GameDebugDisplay.Get().AddTagToDisplay);
			CheatMgr.Get().RegisterCheatHandler("hidetag", GameDebugDisplay.Get().RemoveTagToDisplay);
			CheatMgr.Get().RegisterCheatHandler("hidetags", GameDebugDisplay.Get().RemoveAllTags);
			CheatMgr.Get().RegisterCheatHandler("hidezerotags", GameDebugDisplay.Get().ToggleHideZeroTags);
			CheatMgr.Get().RegisterCheatHandler("aidebug", AIDebugDisplay.Get().ToggleDebugDisplay);
			CheatMgr.Get().RegisterCheatHandler("ropedebug", RopeTimerDebugDisplay.Get().EnableDebugDisplay);
			CheatMgr.Get().RegisterCheatAlias("ropedebug", "ropetimerdebug", "timerdebug", "debugrope", "debugropetimer");
			CheatMgr.Get().RegisterCheatHandler("disableropedebug", RopeTimerDebugDisplay.Get().DisableDebugDisplay);
			CheatMgr.Get().RegisterCheatAlias("disableropedebug", "disableropetimerdebug", "disabletimerdebug", "disabledebugrope", "disabledebugropetimer");
			CheatMgr.Get().RegisterCheatHandler("showbugs", JiraBugDebugDisplay.Get().EnableDebugDisplay);
			CheatMgr.Get().RegisterCheatHandler("hidebugs", JiraBugDebugDisplay.Get().DisableDebugDisplay);
			CheatMgr.Get().RegisterCheatHandler("concede", OnProcessCheat_concede, "This is what happens when you Prep > Coin.");
			ZombeastDebugManager.Get();
			DrustvarHorrorDebugManager.Get();
			SmartDiscoverDebugManager.Get();
		}
		CheatMgr.Get().DefaultCategory();
		gameState.RegisterCreateGameListener(OnCreateGame);
		AssetLoader.Get().InstantiatePrefab("InputManager.prefab:909a8d3bcaaf7ea48a770ff400f4db32", OnInputManagerLoaded);
		AssetLoader.Get().InstantiatePrefab("MulliganManager.prefab:511d1cd9bce694c0a93778f083b47044", OnMulliganManagerLoaded);
		AssetLoader.Get().InstantiatePrefab("ThinkEmoteController.prefab:2163c9dc60486d74f8249ccf878b1742");
		AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", OnCardDrawStandinLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("TurnStartManager.prefab:077d03854627944a695a7e86d67153ca", OnTurnStartManagerLoaded);
		AssetLoader.Get().InstantiatePrefab("TargetReticleManager.prefab:fcbd8bbbf8c5f4c0589fa9c1927bd018", OnTargetReticleManagerLoaded);
		AssetLoader.Get().InstantiatePrefab("RemoteActionHandler.prefab:69f5fe6e6c4af9e4aa51f7ffc10fb9b3", OnRemoteActionHandlerLoaded);
		AssetLoader.Get().InstantiatePrefab("ChoiceCardMgr.prefab:c78e5c81bb7cbaa4ca3f09e6dd732675", OnChoiceCardMgrLoaded);
		AssetLoader.Get().InstantiatePrefab("Actor_Tag_Visual_Table.prefab:7cbaaffc9f20b1e49a08e703944b1e04", OnTagVisualConfigurationLoaded);
		LoadingScreen.Get().RegisterFinishedTransitionListener(OnTransitionFinished);
		m_boardProgress = -1f;
		ProcessGameSetupPacket();
	}

	private void OnDestroy()
	{
		Log.LoadingScreen.Print("Gameplay.OnDestroy()");
		if (m_inputCamera != null)
		{
			if (PegUI.Get() != null)
			{
				PegUI.Get().RemoveInputCamera(m_inputCamera);
			}
			m_inputCamera = null;
		}
		RestoreOriginalTimeScale();
		TimeScaleMgr.Get().PopTemporarySpeedIncrease();
		s_instance = null;
	}

	private void Start()
	{
		Log.LoadingScreen.Print("Gameplay.Start()");
		CheckBattleNetConnection();
		Network network = Network.Get();
		network.AddBnetErrorListener(OnBnetError);
		network.RegisterNetHandler(PowerHistory.PacketID.ID, OnPowerHistory);
		network.RegisterNetHandler(AllOptions.PacketID.ID, OnAllOptions);
		network.RegisterNetHandler(EntityChoices.PacketID.ID, OnEntityChoices);
		network.RegisterNetHandler(EntitiesChosen.PacketID.ID, OnEntitiesChosen);
		network.RegisterNetHandler(UserUI.PacketID.ID, OnUserUI);
		network.RegisterNetHandler(NAckOption.PacketID.ID, OnOptionRejected);
		network.RegisterNetHandler(PegasusGame.TurnTimer.PacketID.ID, OnTurnTimerUpdate);
		network.RegisterNetHandler(SpectatorNotify.PacketID.ID, OnSpectatorNotify);
		network.RegisterNetHandler(AIDebugInformation.PacketID.ID, OnAIDebugInformation);
		network.RegisterNetHandler(RopeTimerDebugInformation.PacketID.ID, OnRopeTimerDebugInformation);
		network.RegisterNetHandler(DebugMessage.PacketID.ID, OnDebugMessage);
		network.RegisterNetHandler(ScriptDebugInformation.PacketID.ID, OnScriptDebugInformation);
		network.RegisterNetHandler(GameRoundHistory.PacketID.ID, OnGameRoundHistory);
		network.RegisterNetHandler(GameRealTimeBattlefieldRaces.PacketID.ID, OnGameRealTimeBattlefieldRaces);
		network.RegisterNetHandler(GameGuardianVars.PacketID.ID, OnGameGuardianVars);
		network.RegisterNetHandler(ScriptLogMessage.PacketID.ID, OnScriptLogMessage);
		network.RegisterNetHandler(UpdateBattlegroundInfo.PacketID.ID, OnBattlegroundInfo);
		if (HearthstoneApplication.IsPublic() || !Cheats.Get().ShouldSkipSendingGetGameState())
		{
			network.GetGameState();
		}
	}

	private void CheckBattleNetConnection()
	{
		if (!Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora())
		{
			OnBnetError(new BnetErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, BattleNetErrors.ERROR_RPC_DISCONNECT), null);
		}
	}

	private void Update()
	{
		CheckCriticalAssetLoads();
		Network.Get().ProcessNetwork();
		if (IsDoneUpdatingGame())
		{
			EndGameScreen endGameScreen = EndGameScreen.Get();
			if (!(endGameScreen != null) || (!endGameScreen.IsPlayingBlockingAnim() && !endGameScreen.IsScoreScreenShown()))
			{
				HandleLastFatalBnetError();
				PlayerMigrationManager playerMigrationManager = PlayerMigrationManager.Get();
				if (playerMigrationManager != null && playerMigrationManager.RestartRequired && !playerMigrationManager.IsShowingPlayerMigrationRelogPopup)
				{
					playerMigrationManager.ShowRestartAlert();
				}
			}
		}
		else if (!GameMgr.Get().IsFindingGame() && !m_unloading && !SceneMgr.Get().WillTransition() && AreCriticalAssetsLoaded() && GameState.Get() != null)
		{
			GameState.Get().Update();
		}
	}

	private void OnGUI()
	{
		LayoutProgressGUI();
	}

	private void LayoutProgressGUI()
	{
		if (!(m_boardProgress < 0f))
		{
			Vector2 vector = new Vector2(150f, 30f);
			Vector2 vector2 = new Vector2((float)Screen.width * 0.5f - vector.x * 0.5f, (float)Screen.height * 0.5f - vector.y * 0.5f);
			GUI.Box(new Rect(vector2.x, vector2.y, vector.x, vector.y), "");
			GUI.Box(new Rect(vector2.x, vector2.y, m_boardProgress * vector.x, vector.y), "");
			GUI.TextField(new Rect(vector2.x, vector2.y, vector.x, vector.y), $"{m_boardProgress * 100f:0}%");
		}
	}

	public static Gameplay Get()
	{
		return s_instance;
	}

	public override void PreUnload()
	{
		m_unloading = true;
		if (Board.Get() != null && BoardCameras.Get() != null)
		{
			LoadingScreen.Get().SetFreezeFrameCamera(Camera.main);
			LoadingScreen.Get().SetTransitionAudioListener(BoardCameras.Get().GetAudioListener());
		}
	}

	public override bool IsUnloading()
	{
		return m_unloading;
	}

	public override void Unload()
	{
		Log.LoadingScreen.Print("Gameplay.Unload()");
		bool num = IsLeavingGameUnfinished();
		GameState.Shutdown();
		Network network = Network.Get();
		network.RemoveGameServerDisconnectEventListener(OnDisconnect);
		network.RemoveBnetErrorListener(OnBnetError);
		network.RemoveNetHandler(PowerHistory.PacketID.ID, OnPowerHistory);
		network.RemoveNetHandler(AllOptions.PacketID.ID, OnAllOptions);
		network.RemoveNetHandler(EntityChoices.PacketID.ID, OnEntityChoices);
		network.RemoveNetHandler(EntitiesChosen.PacketID.ID, OnEntitiesChosen);
		network.RemoveNetHandler(UserUI.PacketID.ID, OnUserUI);
		network.RemoveNetHandler(NAckOption.PacketID.ID, OnOptionRejected);
		network.RemoveNetHandler(PegasusGame.TurnTimer.PacketID.ID, OnTurnTimerUpdate);
		network.RemoveNetHandler(SpectatorNotify.PacketID.ID, OnSpectatorNotify);
		network.RemoveNetHandler(AIDebugInformation.PacketID.ID, OnAIDebugInformation);
		network.RemoveNetHandler(RopeTimerDebugInformation.PacketID.ID, OnRopeTimerDebugInformation);
		network.RemoveNetHandler(DebugMessage.PacketID.ID, OnDebugMessage);
		network.RemoveNetHandler(ScriptDebugInformation.PacketID.ID, OnScriptDebugInformation);
		network.RemoveNetHandler(GameRoundHistory.PacketID.ID, OnGameRoundHistory);
		network.RemoveNetHandler(GameRealTimeBattlefieldRaces.PacketID.ID, OnGameRealTimeBattlefieldRaces);
		network.RemoveNetHandler(GameGuardianVars.PacketID.ID, OnGameGuardianVars);
		network.RemoveNetHandler(ScriptLogMessage.PacketID.ID, OnScriptLogMessage);
		network.RemoveNetHandler(UpdateBattlegroundInfo.PacketID.ID, OnBattlegroundInfo);
		CheatMgr.Get().UnregisterCheatHandler("saveme", OnProcessCheat_saveme);
		if (num)
		{
			if (GameMgr.Get().IsPendingAutoConcede())
			{
				Network.Get().AutoConcede();
				GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: false);
			}
			Network.Get().DisconnectFromGameServer();
		}
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			nameBanner.Unload();
		}
		if (m_nameBannerGamePlayPhone != null)
		{
			m_nameBannerGamePlayPhone.Unload();
		}
		m_unloading = false;
	}

	public void RemoveClassNames()
	{
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			nameBanner.FadeOutSubtext();
			nameBanner.PositionNameText(shouldTween: true);
		}
	}

	public void RemoveNameBanners()
	{
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			UnityEngine.Object.Destroy(nameBanner.gameObject);
		}
		m_nameBanners.Clear();
	}

	public void AddGamePlayNameBannerPhone()
	{
		if (m_nameBannerGamePlayPhone == null)
		{
			AssetLoader.Get().InstantiatePrefab("NameBannerGamePlay_phone.prefab:947928a8ac849b2408a621c97d3b9fa6", OnPlayerBannerLoaded, Player.Side.OPPOSING);
			m_numBannersRequested++;
		}
	}

	public void RemoveGamePlayNameBannerPhone()
	{
		if (m_nameBannerGamePlayPhone != null)
		{
			m_nameBannerGamePlayPhone.Unload();
		}
	}

	public void UpdateFriendlySideMedalChange(MedalInfoTranslator medalInfo)
	{
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			if (nameBanner.GetPlayerSide() == Player.Side.FRIENDLY)
			{
				nameBanner.UpdateMedalChange(medalInfo);
			}
		}
	}

	public void UpdateEnemySideNameBannerName(string newName)
	{
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			if (nameBanner.GetPlayerSide() == Player.Side.OPPOSING)
			{
				nameBanner.SetName(newName);
			}
		}
	}

	public Actor GetCardDrawStandIn()
	{
		return m_cardDrawStandIn;
	}

	public NameBanner GetNameBannerForSide(Player.Side wantedSide)
	{
		if (m_nameBannerGamePlayPhone != null && m_nameBannerGamePlayPhone.GetPlayerSide() == wantedSide)
		{
			return m_nameBannerGamePlayPhone;
		}
		return m_nameBanners.Find((NameBanner x) => x.GetPlayerSide() == wantedSide);
	}

	public void SetGameStateBusy(bool busy, float delay)
	{
		if (delay <= Mathf.Epsilon)
		{
			GameState.Get().SetBusy(busy);
		}
		else
		{
			StartCoroutine(SetGameStateBusyWithDelay(busy, delay));
		}
	}

	public void SwapCardBacks()
	{
		int cardBackId = GameState.Get().GetOpposingSidePlayer().GetCardBackId();
		int cardBackId2 = GameState.Get().GetFriendlySidePlayer().GetCardBackId();
		GameState.Get().GetOpposingSidePlayer().SetCardBackId(cardBackId2);
		GameState.Get().GetFriendlySidePlayer().SetCardBackId(cardBackId);
		CardBackManager.Get().SetGameCardBackIDs(cardBackId, cardBackId2);
	}

	public bool HasBattleNetFatalError()
	{
		return m_lastFatalBnetErrorInfo != null;
	}

	public BoardLayout GetBoardLayout()
	{
		return m_boardLayout;
	}

	private void ProcessGameSetupPacket()
	{
		Network.GameSetup gameSetup = GameMgr.Get().GetGameSetup();
		LoadBoard(gameSetup);
		GameState.Get().OnGameSetup(gameSetup);
	}

	private bool IsHandlingNetworkProblem()
	{
		if (ShouldHandleDisconnect())
		{
			return true;
		}
		if (m_handleLastFatalBnetErrorNow)
		{
			return true;
		}
		return false;
	}

	private bool ShouldHandleDisconnect(bool onDisconnect = false)
	{
		if (Network.Get().IsConnectedToGameServer() && !onDisconnect)
		{
			return false;
		}
		if (Network.Get().WasGameConceded())
		{
			return false;
		}
		if (Network.Get().WasDisconnectRequested() && GameMgr.Get() != null && GameMgr.Get().IsSpectator() && !GameState.Get().IsGameOverNowOrPending())
		{
			return true;
		}
		if (GameState.Get() != null && GameState.Get().IsGameOverNowOrPending())
		{
			return false;
		}
		return true;
	}

	private void OnDisconnect(BattleNetErrors error)
	{
		if (ShouldHandleDisconnect(onDisconnect: true))
		{
			Network.Get().RemoveGameServerDisconnectEventListener(OnDisconnect);
			PerformanceAnalytics.Get()?.DisconnectEvent(SceneMgr.Get().GetMode().ToString());
			HandleDisconnect();
		}
	}

	private void HandleDisconnect()
	{
		Log.GameMgr.PrintWarning("Gameplay is handling a game disconnect.");
		if ((Network.Get().GetLastGameServerJoined() == null || !ReconnectMgr.Get().ReconnectToGameFromGameplay()) && !SpectatorManager.Get().HandleDisconnectFromGameplay())
		{
			DisconnectMgr.Get().DisconnectFromGameplay();
		}
	}

	private bool IsDoneUpdatingGame()
	{
		if (m_handleLastFatalBnetErrorNow)
		{
			return true;
		}
		if (Network.Get().IsConnectedToGameServer())
		{
			return false;
		}
		if (GameState.Get() != null)
		{
			if (GameState.Get().HasPowersToProcess())
			{
				return false;
			}
			if (!GameState.Get().IsGameOver())
			{
				return false;
			}
		}
		return true;
	}

	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (Network.Get().OnIgnorableBnetError(info))
		{
			return true;
		}
		if (m_handleLastFatalBnetErrorNow)
		{
			return true;
		}
		m_lastFatalBnetErrorInfo = info;
		BattleNetErrors error = info.GetError();
		if (error == BattleNetErrors.ERROR_PARENTAL_CONTROL_RESTRICTION || error == BattleNetErrors.ERROR_SESSION_DUPLICATE)
		{
			m_handleLastFatalBnetErrorNow = true;
		}
		return true;
	}

	private void OnBnetErrorResponse(AlertPopup.Response response, object userData)
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

	private void HandleLastFatalBnetError()
	{
		if (m_lastFatalBnetErrorInfo == null)
		{
			return;
		}
		if (m_handleLastFatalBnetErrorNow)
		{
			Network.Get().OnFatalBnetError(m_lastFatalBnetErrorInfo);
			m_handleLastFatalBnetErrorNow = false;
		}
		else
		{
			string key = (HearthstoneApplication.AllowResetFromFatalError ? "GAMEPLAY_DISCONNECT_BODY_RESET" : "GAMEPLAY_DISCONNECT_BODY");
			if (GameMgr.Get().IsSpectator())
			{
				key = (HearthstoneApplication.AllowResetFromFatalError ? "GAMEPLAY_SPECTATOR_DISCONNECT_BODY_RESET" : "GAMEPLAY_SPECTATOR_DISCONNECT_BODY");
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GAMEPLAY_DISCONNECT_HEADER");
			popupInfo.m_text = GameStrings.Get(key);
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = OnBnetErrorResponse;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		m_lastFatalBnetErrorInfo = null;
	}

	private void OnPowerHistory()
	{
		List<Network.PowerHistory> powerHistory = Network.Get().GetPowerHistory();
		Log.LoadingScreen.Print("Gameplay.OnPowerHistory() - powerList={0}", powerHistory.Count);
		if (AreCriticalAssetsLoaded())
		{
			GameState.Get().OnPowerHistory(powerHistory);
		}
		else
		{
			m_queuedPowerHistory.Enqueue(powerHistory);
		}
	}

	private void OnAllOptions()
	{
		Network.Options options = Network.Get().GetOptions();
		Log.LoadingScreen.Print("Gameplay.OnAllOptions() - id={0}", options.ID);
		GameState.Get().OnAllOptions(options);
	}

	private void OnEntityChoices()
	{
		Network.EntityChoices entityChoices = Network.Get().GetEntityChoices();
		Log.LoadingScreen.Print("Gameplay.OnEntityChoices() - id={0}", entityChoices.ID);
		GameState.Get().OnEntityChoices(entityChoices);
	}

	private void OnEntitiesChosen()
	{
		Network.EntitiesChosen entitiesChosen = Network.Get().GetEntitiesChosen();
		GameState.Get().OnEntitiesChosen(entitiesChosen);
	}

	private void OnUserUI()
	{
		if ((bool)RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().HandleAction(Network.Get().GetUserUI());
		}
	}

	private void OnOptionRejected()
	{
		int nAckOption = Network.Get().GetNAckOption();
		GameState.Get().OnOptionRejected(nAckOption);
	}

	private void OnTurnTimerUpdate()
	{
		Network.TurnTimerInfo turnTimerInfo = Network.Get().GetTurnTimerInfo();
		GameState.Get().OnTurnTimerUpdate(turnTimerInfo);
	}

	private void OnSpectatorNotify()
	{
		SpectatorNotify spectatorNotify = Network.Get().GetSpectatorNotify();
		GameState.Get().OnSpectatorNotifyEvent(spectatorNotify);
	}

	private void OnAIDebugInformation()
	{
		AIDebugInformation aIDebugInformation = Network.Get().GetAIDebugInformation();
		AIDebugDisplay.Get().OnAIDebugInformation(aIDebugInformation);
	}

	private void OnRopeTimerDebugInformation()
	{
		RopeTimerDebugInformation ropeTimerDebugInformation = Network.Get().GetRopeTimerDebugInformation();
		RopeTimerDebugDisplay.Get().OnRopeTimerDebugInformation(ropeTimerDebugInformation);
	}

	private void OnDebugMessage()
	{
		DebugMessage debugMessage = Network.Get().GetDebugMessage();
		DebugMessageManager.Get().OnDebugMessage(debugMessage);
	}

	private void OnScriptDebugInformation()
	{
		ScriptDebugInformation scriptDebugInformation = Network.Get().GetScriptDebugInformation();
		ScriptDebugDisplay.Get().OnScriptDebugInfo(scriptDebugInformation);
	}

	private void OnGameRoundHistory()
	{
		GameRoundHistory gameRoundHistory = Network.Get().GetGameRoundHistory();
		if (PlayerLeaderboardManager.Get() != null)
		{
			PlayerLeaderboardManager.Get().UpdateRoundHistory(gameRoundHistory);
		}
	}

	private void OnGameRealTimeBattlefieldRaces()
	{
		GameRealTimeBattlefieldRaces gameRealTimeBattlefieldRaces = Network.Get().GetGameRealTimeBattlefieldRaces();
		if (PlayerLeaderboardManager.Get() != null)
		{
			PlayerLeaderboardManager.Get().UpdatePlayerRaces(gameRealTimeBattlefieldRaces);
		}
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		TAG_RACE[] availableRacesInBattlegroundsExcludingAmalgam = gameState.GetAvailableRacesInBattlegroundsExcludingAmalgam();
		if (!Array.Exists(availableRacesInBattlegroundsExcludingAmalgam, (TAG_RACE race) => race == TAG_RACE.INVALID))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		while (num < availableRacesInBattlegroundsExcludingAmalgam.Length && num2 < gameRealTimeBattlefieldRaces.Races.Count)
		{
			int race2 = gameRealTimeBattlefieldRaces.Races[num2].Race;
			if (race2 != 0 && race2 != 26 && Enum.IsDefined(typeof(TAG_RACE), race2))
			{
				availableRacesInBattlegroundsExcludingAmalgam[num] = (TAG_RACE)race2;
				num++;
			}
			num2++;
		}
	}

	private void OnGameGuardianVars()
	{
		GameGuardianVars gameGuardianVars = Network.Get().GetGameGuardianVars();
		if (GameState.Get() != null)
		{
			GameState.Get().UpdateGameGuardianVars(gameGuardianVars);
		}
	}

	private void OnBattlegroundInfo()
	{
		UpdateBattlegroundInfo battlegroundInfo = Network.Get().GetBattlegroundInfo();
		if (GameState.Get() != null)
		{
			GameState.Get().UpdateBattlegroundInfo(battlegroundInfo);
		}
	}

	private void OnScriptLogMessage()
	{
		ScriptLogMessage scriptLogMessage = Network.Get().GetScriptLogMessage();
		if (SceneDebugger.Get() != null)
		{
			SceneDebugger.Get().AddServerScriptLogMessage(scriptLogMessage);
		}
	}

	private bool AreCriticalAssetsLoaded()
	{
		return m_criticalAssetsLoaded;
	}

	private bool CheckCriticalAssetLoads()
	{
		if (m_criticalAssetsLoaded)
		{
			return true;
		}
		if (Board.Get() == null)
		{
			return false;
		}
		if (BoardCameras.Get() == null)
		{
			return false;
		}
		if (GetBoardLayout() == null)
		{
			return false;
		}
		if (GameMgr.Get().IsTutorial() && BoardTutorial.Get() == null)
		{
			return false;
		}
		if (MulliganManager.Get() == null)
		{
			return false;
		}
		if (TurnStartManager.Get() == null)
		{
			return false;
		}
		if (TargetReticleManager.Get() == null)
		{
			return false;
		}
		if (GameplayErrorManager.Get() == null)
		{
			return false;
		}
		if (EndTurnButton.Get() == null)
		{
			return false;
		}
		if (BigCard.Get() == null)
		{
			return false;
		}
		if (CardTypeBanner.Get() == null)
		{
			return false;
		}
		if (TurnTimer.Get() == null)
		{
			return false;
		}
		if (CardColorSwitcher.Get() == null)
		{
			return false;
		}
		if (RemoteActionHandler.Get() == null)
		{
			return false;
		}
		if (ChoiceCardMgr.Get() == null)
		{
			return false;
		}
		if (InputManager.Get() == null)
		{
			return false;
		}
		m_criticalAssetsLoaded = true;
		ProcessQueuedPowerHistory();
		return true;
	}

	private void InitCardBacks()
	{
		int cardBackId = GameState.Get().GetFriendlySidePlayer().GetCardBackId();
		int cardBackId2 = GameState.Get().GetOpposingSidePlayer().GetCardBackId();
		CardBackManager.Get().SetGameCardBackIDs(cardBackId, cardBackId2);
	}

	private void LoadBoard(Network.GameSetup setup)
	{
		AssetReference assetReference = null;
		BoardDbfRecord record = GameDbf.Board.GetRecord(setup.Board);
		if (record == null)
		{
			Debug.LogError($"Gameplay.LoadBoard() - FAILED to load board id: \"{setup.Board}\"");
			UIStatus.Get().AddInfo($"Failed to Load board ID: {setup.Board}, defaulting back to 1.");
			record = GameDbf.Board.GetRecord(1);
		}
		assetReference = new AssetReference(record.Prefab);
		AssetLoader.Get().InstantiatePrefab(assetReference, OnBoardLoaded);
	}

	private IEnumerator NotifyPlayersOfBoardLoad()
	{
		while (GetBoardLayout() == null)
		{
			yield return null;
		}
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			value.OnBoardLoaded();
		}
	}

	private void OnBoardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_boardProgress = -1f;
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnBoardLoaded() - FAILED to load board \"{go}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.GetComponent<Board>().SetBoardDbId(GameMgr.Get().GetGameSetup().Board);
		string text = (UniversalInputManager.UsePhoneUI ? "BoardCameras_phone.prefab:1e862adebb4fd4fca8b24249d32f8d86" : "BoardCameras.prefab:b4f3a6717904ff34985655c86149f06c");
		AssetLoader.Get().InstantiatePrefab(text, OnBoardCamerasLoaded);
		AssetLoader.Get().InstantiatePrefab("BoardStandardGame.prefab:b87d693f752160b43a25b7cec3787122", OnBoardStandardGameLoaded);
		if (GameMgr.Get().IsTutorial())
		{
			AssetLoader.Get().InstantiatePrefab("BoardTutorial.prefab:08bd830fc30e15e48a4b56bfc3abee15", OnBoardTutorialLoaded);
		}
	}

	private void OnBoardProgressUpdate(string name, float progress, object callbackData)
	{
		m_boardProgress = progress;
	}

	private void OnBoardCamerasLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnBoardCamerasLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = Board.Get().transform;
		m_inputCamera = Camera.main;
		PegUI.Get().AddInputCamera(m_inputCamera);
		AssetLoader.Get().InstantiatePrefab("CardTypeBanner.prefab:3b446c3c5a48357438d8aa969b5c377d", AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("BigCard.prefab:c938058e4609a1146b7ce8a115cc82df", AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnBoardStandardGameLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnBoardStandardGameLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		m_boardLayout = go.GetComponent<BoardLayout>();
		go.transform.parent = Board.Get().transform;
		AssetLoader.Get().InstantiatePrefab("EndTurnButton.prefab:313ebd8bcb770a944be3633ad928096b", OnEndTurnButtonLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("TurnTimer.prefab:aa1be1e4f5b36ca4aa6a38ac7d0538ce", OnTurnTimerLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnBoardTutorialLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnBoardTutorialLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = Board.Get().transform;
		}
	}

	private void OnEndTurnButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnEndTurnButtonLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		EndTurnButton component = go.GetComponent<EndTurnButton>();
		if (component == null)
		{
			Debug.LogError($"Gameplay.OnEndTurnButtonLoaded() - ERROR \"{base.name}\" has no {typeof(EndTurnButton)} component");
			return;
		}
		component.transform.position = Board.Get().FindBone("EndTurnButton").position;
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!renderer.gameObject.GetComponent<TextMesh>())
			{
				renderer.GetMaterial().color = Board.Get().m_EndTurnButtonColor;
			}
		}
	}

	private void OnTurnTimerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnTurnTimerLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		TurnTimer component = go.GetComponent<TurnTimer>();
		if (component == null)
		{
			Debug.LogError($"Gameplay.OnTurnTimerLoaded() - ERROR \"{base.name}\" has no {typeof(TurnTimer)} component");
		}
		else
		{
			component.transform.position = Board.Get().FindBone("TurnTimerBone").position;
		}
	}

	private void OnRemoteActionHandlerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnRemoteActionHandlerLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnTagVisualConfigurationLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnTagVisualConfigurationLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnChoiceCardMgrLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnChoiceCardMgrLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnInputManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnInputManagerLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnMulliganManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnMulliganManagerLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnTurnStartManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnTurnStartManagerLoaded() - FAILED to load \"{assetRef}\"");
		}
		else if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
		}
		else
		{
			go.transform.parent = base.transform;
		}
	}

	private void OnTargetReticleManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnTargetReticleManagerLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
		TargetReticleManager.Get().PreloadTargetArrows();
	}

	private void OnPlayerBannerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		Player.Side side = (Player.Side)callbackData;
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnPlayerBannerLoaded() - FAILED to load \"{assetRef}\" side={side.ToString()}");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		NameBanner component = go.GetComponent<NameBanner>();
		if (component == null)
		{
			Debug.LogError($"Gameplay.OnPlayerBannerLoaded() - FAILED to to find NameBanner component on \"{assetRef}\" side={side.ToString()}");
			return;
		}
		m_nameBanners.Add(component);
		m_numBannersRequested--;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (base.name == "NameBannerGamePlay_phone")
			{
				m_nameBannerGamePlayPhone = component;
				m_nameBannerGamePlayPhone.Initialize(side);
			}
			else
			{
				component.Initialize(side);
			}
		}
		else
		{
			component.Initialize(side);
			if (!string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAlternatePlayerName()) && component.GetPlayerSide() == Player.Side.FRIENDLY)
			{
				component.UseLongName();
			}
		}
		StartCoroutine(ShowBannersWhenReady());
	}

	private IEnumerator ShowBannersWhenReady()
	{
		if (m_numBannersRequested > 0)
		{
			yield break;
		}
		foreach (NameBanner banner in m_nameBanners)
		{
			while (banner.IsWaitingForMedal)
			{
				yield return null;
			}
		}
		foreach (NameBanner nameBanner in m_nameBanners)
		{
			nameBanner.Show();
		}
	}

	private void OnCardDrawStandinLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"Gameplay.OnCardDrawStandinLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		if (IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		m_cardDrawStandIn = go.GetComponent<Actor>();
		go.GetComponentInChildren<CardBackDisplay>().SetCardBack(CardBackManager.CardBackSlot.FRIENDLY);
		m_cardDrawStandIn.Hide();
	}

	private void OnTransitionFinished(bool cutoff, object userData)
	{
		LoadingScreen.Get().UnregisterFinishedTransitionListener(OnTransitionFinished);
		if (cutoff || IsHandlingNetworkProblem())
		{
			return;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (GameState.Get() != null && GameState.Get().IsMulliganPhase())
			{
				AssetLoader.Get().InstantiatePrefab("NameBannerRight_phone.prefab:8712bbdedd6fa4a45b18dc88226d67b3", OnPlayerBannerLoaded, Player.Side.FRIENDLY);
				AssetLoader.Get().InstantiatePrefab("NameBanner_phone.prefab:c919b2370a8d748d38e2cb4708e15398", OnPlayerBannerLoaded, Player.Side.OPPOSING);
				m_numBannersRequested += 2;
			}
			else if (GameMgr.Get() != null && !GameMgr.Get().IsTutorial())
			{
				AddGamePlayNameBannerPhone();
			}
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("NameBanner.prefab:f579c831653574d4da0437a5fcf0d58f", OnPlayerBannerLoaded, Player.Side.FRIENDLY);
			AssetLoader.Get().InstantiatePrefab("NameBanner.prefab:f579c831653574d4da0437a5fcf0d58f", OnPlayerBannerLoaded, Player.Side.OPPOSING);
			m_numBannersRequested += 2;
		}
	}

	private void ProcessQueuedPowerHistory()
	{
		while (m_queuedPowerHistory.Count > 0)
		{
			List<Network.PowerHistory> powerList = m_queuedPowerHistory.Dequeue();
			GameState.Get().OnPowerHistory(powerList);
		}
	}

	private bool IsLeavingGameUnfinished()
	{
		if (GameState.Get() != null && GameState.Get().IsGameOver())
		{
			return false;
		}
		if (GameMgr.Get().IsReconnect())
		{
			return false;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			return false;
		}
		return true;
	}

	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		switch (phase)
		{
		case GameState.CreateGamePhase.CREATING:
			InitCardBacks();
			StartCoroutine(NotifyPlayersOfBoardLoad());
			break;
		case GameState.CreateGamePhase.CREATED:
			CardBackManager.Get().UpdateAllCardBacksInSceneWhenReady();
			break;
		}
	}

	private IEnumerator SetGameStateBusyWithDelay(bool busy, float delay)
	{
		yield return new WaitForSeconds(delay);
		GameState.Get().SetBusy(busy);
	}

	public void SaveOriginalTimeScale()
	{
		m_originalTimeScale = TimeScaleMgr.Get().GetGameTimeScale();
	}

	public void RestoreOriginalTimeScale()
	{
		if (m_originalTimeScale.HasValue)
		{
			TimeScaleMgr.Get().SetGameTimeScale(m_originalTimeScale.Value);
			m_originalTimeScale = null;
		}
	}

	public Coroutine RegisterCoroutine(IEnumerator routine)
	{
		return StartCoroutine(routine);
	}

	public void UnregisterCoroutine(Coroutine routine)
	{
		StopCoroutine(routine);
	}

	private bool OnProcessCheat_saveme(string func, string[] args, string rawArgs)
	{
		GameState.Get().DebugNukeServerBlocks();
		return true;
	}

	private bool OnProcessCheat_concede(string func, string[] args, string rawArgs)
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			UIStatus.Get().AddInfo("No active game found!", 2f);
			return true;
		}
		gameState.Concede();
		return true;
	}
}
