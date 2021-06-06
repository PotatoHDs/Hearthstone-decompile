using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class Gameplay : PegasusScene
{
	// Token: 0x06002A71 RID: 10865 RVA: 0x000D58F8 File Offset: 0x000D3AF8
	protected override void Awake()
	{
		global::Log.LoadingScreen.Print("Gameplay.Awake()", Array.Empty<object>());
		base.Awake();
		Gameplay.s_instance = this;
		GameState gameState = GameState.Initialize();
		if (this.ShouldHandleDisconnect(false))
		{
			global::Log.LoadingScreen.PrintWarning("Gameplay.Awake() - DISCONNECTED", Array.Empty<object>());
			this.HandleDisconnect();
			return;
		}
		Network.Get().SetGameServerDisconnectEventListener(new Network.GameServerDisconnectEvent(this.OnDisconnect));
		CheatMgr.Get().RegisterCategory("gameplay:more");
		CheatMgr.Get().RegisterCheatHandler("saveme", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_saveme), null, null, null);
		if (!HearthstoneApplication.IsPublic())
		{
			CheatMgr.Get().RegisterCheatHandler("entitycount", new CheatMgr.ProcessCheatCallback(GameDebugDisplay.Get().ToggleEntityCount), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("showtag", new CheatMgr.ProcessCheatCallback(GameDebugDisplay.Get().AddTagToDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("hidetag", new CheatMgr.ProcessCheatCallback(GameDebugDisplay.Get().RemoveTagToDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("hidetags", new CheatMgr.ProcessCheatCallback(GameDebugDisplay.Get().RemoveAllTags), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("hidezerotags", new CheatMgr.ProcessCheatCallback(GameDebugDisplay.Get().ToggleHideZeroTags), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("aidebug", new CheatMgr.ProcessCheatCallback(AIDebugDisplay.Get().ToggleDebugDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("ropedebug", new CheatMgr.ProcessCheatCallback(RopeTimerDebugDisplay.Get().EnableDebugDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatAlias("ropedebug", new string[]
			{
				"ropetimerdebug",
				"timerdebug",
				"debugrope",
				"debugropetimer"
			});
			CheatMgr.Get().RegisterCheatHandler("disableropedebug", new CheatMgr.ProcessCheatCallback(RopeTimerDebugDisplay.Get().DisableDebugDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatAlias("disableropedebug", new string[]
			{
				"disableropetimerdebug",
				"disabletimerdebug",
				"disabledebugrope",
				"disabledebugropetimer"
			});
			CheatMgr.Get().RegisterCheatHandler("showbugs", new CheatMgr.ProcessCheatCallback(JiraBugDebugDisplay.Get().EnableDebugDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("hidebugs", new CheatMgr.ProcessCheatCallback(JiraBugDebugDisplay.Get().DisableDebugDisplay), null, null, null);
			CheatMgr.Get().RegisterCheatHandler("concede", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_concede), "This is what happens when you Prep > Coin.", null, null);
			ZombeastDebugManager.Get();
			DrustvarHorrorDebugManager.Get();
			SmartDiscoverDebugManager.Get();
		}
		CheatMgr.Get().DefaultCategory();
		gameState.RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		AssetLoader.Get().InstantiatePrefab("InputManager.prefab:909a8d3bcaaf7ea48a770ff400f4db32", new PrefabCallback<GameObject>(this.OnInputManagerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("MulliganManager.prefab:511d1cd9bce694c0a93778f083b47044", new PrefabCallback<GameObject>(this.OnMulliganManagerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("ThinkEmoteController.prefab:2163c9dc60486d74f8249ccf878b1742", AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", new PrefabCallback<GameObject>(this.OnCardDrawStandinLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("TurnStartManager.prefab:077d03854627944a695a7e86d67153ca", new PrefabCallback<GameObject>(this.OnTurnStartManagerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("TargetReticleManager.prefab:fcbd8bbbf8c5f4c0589fa9c1927bd018", new PrefabCallback<GameObject>(this.OnTargetReticleManagerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("RemoteActionHandler.prefab:69f5fe6e6c4af9e4aa51f7ffc10fb9b3", new PrefabCallback<GameObject>(this.OnRemoteActionHandlerLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("ChoiceCardMgr.prefab:c78e5c81bb7cbaa4ca3f09e6dd732675", new PrefabCallback<GameObject>(this.OnChoiceCardMgrLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("Actor_Tag_Visual_Table.prefab:7cbaaffc9f20b1e49a08e703944b1e04", new PrefabCallback<GameObject>(this.OnTagVisualConfigurationLoaded), null, AssetLoadingOptions.None);
		LoadingScreen.Get().RegisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFinished));
		this.m_boardProgress = -1f;
		this.ProcessGameSetupPacket();
	}

	// Token: 0x06002A72 RID: 10866 RVA: 0x000D5D04 File Offset: 0x000D3F04
	private void OnDestroy()
	{
		global::Log.LoadingScreen.Print("Gameplay.OnDestroy()", Array.Empty<object>());
		if (this.m_inputCamera != null)
		{
			if (PegUI.Get() != null)
			{
				PegUI.Get().RemoveInputCamera(this.m_inputCamera);
			}
			this.m_inputCamera = null;
		}
		this.RestoreOriginalTimeScale();
		TimeScaleMgr.Get().PopTemporarySpeedIncrease();
		Gameplay.s_instance = null;
	}

	// Token: 0x06002A73 RID: 10867 RVA: 0x000D5D70 File Offset: 0x000D3F70
	private void Start()
	{
		global::Log.LoadingScreen.Print("Gameplay.Start()", Array.Empty<object>());
		this.CheckBattleNetConnection();
		Network network = Network.Get();
		network.AddBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		network.RegisterNetHandler(PowerHistory.PacketID.ID, new Network.NetHandler(this.OnPowerHistory), null);
		network.RegisterNetHandler(AllOptions.PacketID.ID, new Network.NetHandler(this.OnAllOptions), null);
		network.RegisterNetHandler(EntityChoices.PacketID.ID, new Network.NetHandler(this.OnEntityChoices), null);
		network.RegisterNetHandler(EntitiesChosen.PacketID.ID, new Network.NetHandler(this.OnEntitiesChosen), null);
		network.RegisterNetHandler(UserUI.PacketID.ID, new Network.NetHandler(this.OnUserUI), null);
		network.RegisterNetHandler(NAckOption.PacketID.ID, new Network.NetHandler(this.OnOptionRejected), null);
		network.RegisterNetHandler(PegasusGame.TurnTimer.PacketID.ID, new Network.NetHandler(this.OnTurnTimerUpdate), null);
		network.RegisterNetHandler(SpectatorNotify.PacketID.ID, new Network.NetHandler(this.OnSpectatorNotify), null);
		network.RegisterNetHandler(AIDebugInformation.PacketID.ID, new Network.NetHandler(this.OnAIDebugInformation), null);
		network.RegisterNetHandler(RopeTimerDebugInformation.PacketID.ID, new Network.NetHandler(this.OnRopeTimerDebugInformation), null);
		network.RegisterNetHandler(DebugMessage.PacketID.ID, new Network.NetHandler(this.OnDebugMessage), null);
		network.RegisterNetHandler(ScriptDebugInformation.PacketID.ID, new Network.NetHandler(this.OnScriptDebugInformation), null);
		network.RegisterNetHandler(GameRoundHistory.PacketID.ID, new Network.NetHandler(this.OnGameRoundHistory), null);
		network.RegisterNetHandler(GameRealTimeBattlefieldRaces.PacketID.ID, new Network.NetHandler(this.OnGameRealTimeBattlefieldRaces), null);
		network.RegisterNetHandler(GameGuardianVars.PacketID.ID, new Network.NetHandler(this.OnGameGuardianVars), null);
		network.RegisterNetHandler(ScriptLogMessage.PacketID.ID, new Network.NetHandler(this.OnScriptLogMessage), null);
		network.RegisterNetHandler(UpdateBattlegroundInfo.PacketID.ID, new Network.NetHandler(this.OnBattlegroundInfo), null);
		if (!HearthstoneApplication.IsPublic() && Cheats.Get().ShouldSkipSendingGetGameState())
		{
			return;
		}
		network.GetGameState();
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x000D5F90 File Offset: 0x000D4190
	private void CheckBattleNetConnection()
	{
		if (!Network.IsLoggedIn() && Network.ShouldBeConnectedToAurora())
		{
			this.OnBnetError(new BnetErrorInfo(BnetFeature.Bnet, BnetFeatureEvent.Bnet_OnDisconnected, BattleNetErrors.ERROR_RPC_DISCONNECT), null);
		}
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x000D5FB8 File Offset: 0x000D41B8
	private void Update()
	{
		this.CheckCriticalAssetLoads();
		Network.Get().ProcessNetwork();
		if (this.IsDoneUpdatingGame())
		{
			EndGameScreen endGameScreen = EndGameScreen.Get();
			if (endGameScreen != null && (endGameScreen.IsPlayingBlockingAnim() || endGameScreen.IsScoreScreenShown()))
			{
				return;
			}
			this.HandleLastFatalBnetError();
			PlayerMigrationManager playerMigrationManager = PlayerMigrationManager.Get();
			if (playerMigrationManager != null && playerMigrationManager.RestartRequired && !playerMigrationManager.IsShowingPlayerMigrationRelogPopup)
			{
				playerMigrationManager.ShowRestartAlert();
			}
			return;
		}
		else
		{
			if (GameMgr.Get().IsFindingGame())
			{
				return;
			}
			if (this.m_unloading)
			{
				return;
			}
			if (SceneMgr.Get().WillTransition())
			{
				return;
			}
			if (!this.AreCriticalAssetsLoaded())
			{
				return;
			}
			if (GameState.Get() == null)
			{
				return;
			}
			GameState.Get().Update();
			return;
		}
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x000D6062 File Offset: 0x000D4262
	private void OnGUI()
	{
		this.LayoutProgressGUI();
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x000D606C File Offset: 0x000D426C
	private void LayoutProgressGUI()
	{
		if (this.m_boardProgress < 0f)
		{
			return;
		}
		Vector2 vector = new Vector2(150f, 30f);
		Vector2 vector2 = new Vector2((float)Screen.width * 0.5f - vector.x * 0.5f, (float)Screen.height * 0.5f - vector.y * 0.5f);
		GUI.Box(new Rect(vector2.x, vector2.y, vector.x, vector.y), "");
		GUI.Box(new Rect(vector2.x, vector2.y, this.m_boardProgress * vector.x, vector.y), "");
		GUI.TextField(new Rect(vector2.x, vector2.y, vector.x, vector.y), string.Format("{0:0}%", this.m_boardProgress * 100f));
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x000D6164 File Offset: 0x000D4364
	public static Gameplay Get()
	{
		return Gameplay.s_instance;
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x000D616C File Offset: 0x000D436C
	public override void PreUnload()
	{
		this.m_unloading = true;
		if (Board.Get() != null && BoardCameras.Get() != null)
		{
			LoadingScreen.Get().SetFreezeFrameCamera(Camera.main);
			LoadingScreen.Get().SetTransitionAudioListener(BoardCameras.Get().GetAudioListener());
		}
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x000D61BD File Offset: 0x000D43BD
	public override bool IsUnloading()
	{
		return this.m_unloading;
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x000D61C8 File Offset: 0x000D43C8
	public override void Unload()
	{
		global::Log.LoadingScreen.Print("Gameplay.Unload()", Array.Empty<object>());
		bool flag = this.IsLeavingGameUnfinished();
		GameState.Shutdown();
		Network network = Network.Get();
		network.RemoveGameServerDisconnectEventListener(new Network.GameServerDisconnectEvent(this.OnDisconnect));
		network.RemoveBnetErrorListener(new Network.BnetErrorCallback(this.OnBnetError));
		network.RemoveNetHandler(PowerHistory.PacketID.ID, new Network.NetHandler(this.OnPowerHistory));
		network.RemoveNetHandler(AllOptions.PacketID.ID, new Network.NetHandler(this.OnAllOptions));
		network.RemoveNetHandler(EntityChoices.PacketID.ID, new Network.NetHandler(this.OnEntityChoices));
		network.RemoveNetHandler(EntitiesChosen.PacketID.ID, new Network.NetHandler(this.OnEntitiesChosen));
		network.RemoveNetHandler(UserUI.PacketID.ID, new Network.NetHandler(this.OnUserUI));
		network.RemoveNetHandler(NAckOption.PacketID.ID, new Network.NetHandler(this.OnOptionRejected));
		network.RemoveNetHandler(PegasusGame.TurnTimer.PacketID.ID, new Network.NetHandler(this.OnTurnTimerUpdate));
		network.RemoveNetHandler(SpectatorNotify.PacketID.ID, new Network.NetHandler(this.OnSpectatorNotify));
		network.RemoveNetHandler(AIDebugInformation.PacketID.ID, new Network.NetHandler(this.OnAIDebugInformation));
		network.RemoveNetHandler(RopeTimerDebugInformation.PacketID.ID, new Network.NetHandler(this.OnRopeTimerDebugInformation));
		network.RemoveNetHandler(DebugMessage.PacketID.ID, new Network.NetHandler(this.OnDebugMessage));
		network.RemoveNetHandler(ScriptDebugInformation.PacketID.ID, new Network.NetHandler(this.OnScriptDebugInformation));
		network.RemoveNetHandler(GameRoundHistory.PacketID.ID, new Network.NetHandler(this.OnGameRoundHistory));
		network.RemoveNetHandler(GameRealTimeBattlefieldRaces.PacketID.ID, new Network.NetHandler(this.OnGameRealTimeBattlefieldRaces));
		network.RemoveNetHandler(GameGuardianVars.PacketID.ID, new Network.NetHandler(this.OnGameGuardianVars));
		network.RemoveNetHandler(ScriptLogMessage.PacketID.ID, new Network.NetHandler(this.OnScriptLogMessage));
		network.RemoveNetHandler(UpdateBattlegroundInfo.PacketID.ID, new Network.NetHandler(this.OnBattlegroundInfo));
		CheatMgr.Get().UnregisterCheatHandler("saveme", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_saveme));
		if (flag)
		{
			if (GameMgr.Get().IsPendingAutoConcede())
			{
				Network.Get().AutoConcede();
				GameMgr.Get().SetPendingAutoConcede(false);
			}
			Network.Get().DisconnectFromGameServer();
		}
		foreach (NameBanner nameBanner in this.m_nameBanners)
		{
			nameBanner.Unload();
		}
		if (this.m_nameBannerGamePlayPhone != null)
		{
			this.m_nameBannerGamePlayPhone.Unload();
		}
		this.m_unloading = false;
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x000D6480 File Offset: 0x000D4680
	public void RemoveClassNames()
	{
		foreach (NameBanner nameBanner in this.m_nameBanners)
		{
			nameBanner.FadeOutSubtext();
			nameBanner.PositionNameText(true);
		}
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x000D64D8 File Offset: 0x000D46D8
	public void RemoveNameBanners()
	{
		foreach (NameBanner nameBanner in this.m_nameBanners)
		{
			UnityEngine.Object.Destroy(nameBanner.gameObject);
		}
		this.m_nameBanners.Clear();
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x000D6538 File Offset: 0x000D4738
	public void AddGamePlayNameBannerPhone()
	{
		if (this.m_nameBannerGamePlayPhone == null)
		{
			AssetLoader.Get().InstantiatePrefab("NameBannerGamePlay_phone.prefab:947928a8ac849b2408a621c97d3b9fa6", new PrefabCallback<GameObject>(this.OnPlayerBannerLoaded), global::Player.Side.OPPOSING, AssetLoadingOptions.None);
			this.m_numBannersRequested++;
		}
	}

	// Token: 0x06002A7F RID: 10879 RVA: 0x000D6589 File Offset: 0x000D4789
	public void RemoveGamePlayNameBannerPhone()
	{
		if (this.m_nameBannerGamePlayPhone != null)
		{
			this.m_nameBannerGamePlayPhone.Unload();
		}
	}

	// Token: 0x06002A80 RID: 10880 RVA: 0x000D65A4 File Offset: 0x000D47A4
	public void UpdateFriendlySideMedalChange(MedalInfoTranslator medalInfo)
	{
		foreach (NameBanner nameBanner in this.m_nameBanners)
		{
			if (nameBanner.GetPlayerSide() == global::Player.Side.FRIENDLY)
			{
				nameBanner.UpdateMedalChange(medalInfo);
			}
		}
	}

	// Token: 0x06002A81 RID: 10881 RVA: 0x000D6600 File Offset: 0x000D4800
	public void UpdateEnemySideNameBannerName(string newName)
	{
		foreach (NameBanner nameBanner in this.m_nameBanners)
		{
			if (nameBanner.GetPlayerSide() == global::Player.Side.OPPOSING)
			{
				nameBanner.SetName(newName);
			}
		}
	}

	// Token: 0x06002A82 RID: 10882 RVA: 0x000D665C File Offset: 0x000D485C
	public Actor GetCardDrawStandIn()
	{
		return this.m_cardDrawStandIn;
	}

	// Token: 0x06002A83 RID: 10883 RVA: 0x000D6664 File Offset: 0x000D4864
	public NameBanner GetNameBannerForSide(global::Player.Side wantedSide)
	{
		if (this.m_nameBannerGamePlayPhone != null && this.m_nameBannerGamePlayPhone.GetPlayerSide() == wantedSide)
		{
			return this.m_nameBannerGamePlayPhone;
		}
		return this.m_nameBanners.Find((NameBanner x) => x.GetPlayerSide() == wantedSide);
	}

	// Token: 0x06002A84 RID: 10884 RVA: 0x000D66BD File Offset: 0x000D48BD
	public void SetGameStateBusy(bool busy, float delay)
	{
		if (delay <= Mathf.Epsilon)
		{
			GameState.Get().SetBusy(busy);
			return;
		}
		base.StartCoroutine(this.SetGameStateBusyWithDelay(busy, delay));
	}

	// Token: 0x06002A85 RID: 10885 RVA: 0x000D66E4 File Offset: 0x000D48E4
	public void SwapCardBacks()
	{
		int cardBackId = GameState.Get().GetOpposingSidePlayer().GetCardBackId();
		int cardBackId2 = GameState.Get().GetFriendlySidePlayer().GetCardBackId();
		GameState.Get().GetOpposingSidePlayer().SetCardBackId(cardBackId2);
		GameState.Get().GetFriendlySidePlayer().SetCardBackId(cardBackId);
		CardBackManager.Get().SetGameCardBackIDs(cardBackId, cardBackId2);
	}

	// Token: 0x06002A86 RID: 10886 RVA: 0x000D673D File Offset: 0x000D493D
	public bool HasBattleNetFatalError()
	{
		return this.m_lastFatalBnetErrorInfo != null;
	}

	// Token: 0x06002A87 RID: 10887 RVA: 0x000D6748 File Offset: 0x000D4948
	public BoardLayout GetBoardLayout()
	{
		return this.m_boardLayout;
	}

	// Token: 0x06002A88 RID: 10888 RVA: 0x000D6750 File Offset: 0x000D4950
	private void ProcessGameSetupPacket()
	{
		Network.GameSetup gameSetup = GameMgr.Get().GetGameSetup();
		this.LoadBoard(gameSetup);
		GameState.Get().OnGameSetup(gameSetup);
	}

	// Token: 0x06002A89 RID: 10889 RVA: 0x000D677A File Offset: 0x000D497A
	private bool IsHandlingNetworkProblem()
	{
		return this.ShouldHandleDisconnect(false) || this.m_handleLastFatalBnetErrorNow;
	}

	// Token: 0x06002A8A RID: 10890 RVA: 0x000D6794 File Offset: 0x000D4994
	private bool ShouldHandleDisconnect(bool onDisconnect = false)
	{
		return (!Network.Get().IsConnectedToGameServer() || onDisconnect) && !Network.Get().WasGameConceded() && ((Network.Get().WasDisconnectRequested() && GameMgr.Get() != null && GameMgr.Get().IsSpectator() && !GameState.Get().IsGameOverNowOrPending()) || GameState.Get() == null || !GameState.Get().IsGameOverNowOrPending());
	}

	// Token: 0x06002A8B RID: 10891 RVA: 0x000D6804 File Offset: 0x000D4A04
	private void OnDisconnect(BattleNetErrors error)
	{
		if (!this.ShouldHandleDisconnect(true))
		{
			return;
		}
		Network.Get().RemoveGameServerDisconnectEventListener(new Network.GameServerDisconnectEvent(this.OnDisconnect));
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.DisconnectEvent(SceneMgr.Get().GetMode().ToString());
		}
		this.HandleDisconnect();
	}

	// Token: 0x06002A8C RID: 10892 RVA: 0x000D6860 File Offset: 0x000D4A60
	private void HandleDisconnect()
	{
		global::Log.GameMgr.PrintWarning("Gameplay is handling a game disconnect.", Array.Empty<object>());
		if (Network.Get().GetLastGameServerJoined() != null && ReconnectMgr.Get().ReconnectToGameFromGameplay())
		{
			return;
		}
		if (SpectatorManager.Get().HandleDisconnectFromGameplay())
		{
			return;
		}
		DisconnectMgr.Get().DisconnectFromGameplay();
	}

	// Token: 0x06002A8D RID: 10893 RVA: 0x000D68B1 File Offset: 0x000D4AB1
	private bool IsDoneUpdatingGame()
	{
		if (this.m_handleLastFatalBnetErrorNow)
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

	// Token: 0x06002A8E RID: 10894 RVA: 0x000D68F0 File Offset: 0x000D4AF0
	private bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (Network.Get().OnIgnorableBnetError(info))
		{
			return true;
		}
		if (this.m_handleLastFatalBnetErrorNow)
		{
			return true;
		}
		this.m_lastFatalBnetErrorInfo = info;
		BattleNetErrors error = info.GetError();
		if (error == BattleNetErrors.ERROR_PARENTAL_CONTROL_RESTRICTION || error == BattleNetErrors.ERROR_SESSION_DUPLICATE)
		{
			this.m_handleLastFatalBnetErrorNow = true;
		}
		return true;
	}

	// Token: 0x06002A8F RID: 10895 RVA: 0x000D6936 File Offset: 0x000D4B36
	private void OnBnetErrorResponse(AlertPopup.Response response, object userData)
	{
		if (HearthstoneApplication.AllowResetFromFatalError)
		{
			HearthstoneApplication.Get().Reset();
			return;
		}
		HearthstoneApplication.Get().Exit();
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x000D695C File Offset: 0x000D4B5C
	private void HandleLastFatalBnetError()
	{
		if (this.m_lastFatalBnetErrorInfo == null)
		{
			return;
		}
		if (this.m_handleLastFatalBnetErrorNow)
		{
			Network.Get().OnFatalBnetError(this.m_lastFatalBnetErrorInfo);
			this.m_handleLastFatalBnetErrorNow = false;
		}
		else
		{
			string key = HearthstoneApplication.AllowResetFromFatalError ? "GAMEPLAY_DISCONNECT_BODY_RESET" : "GAMEPLAY_DISCONNECT_BODY";
			if (GameMgr.Get().IsSpectator())
			{
				key = (HearthstoneApplication.AllowResetFromFatalError ? "GAMEPLAY_SPECTATOR_DISCONNECT_BODY_RESET" : "GAMEPLAY_SPECTATOR_DISCONNECT_BODY");
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GAMEPLAY_DISCONNECT_HEADER");
			popupInfo.m_text = GameStrings.Get(key);
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnBnetErrorResponse);
			DialogManager.Get().ShowPopup(popupInfo);
		}
		this.m_lastFatalBnetErrorInfo = null;
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x000D6A28 File Offset: 0x000D4C28
	private void OnPowerHistory()
	{
		List<Network.PowerHistory> powerHistory = Network.Get().GetPowerHistory();
		global::Log.LoadingScreen.Print("Gameplay.OnPowerHistory() - powerList={0}", new object[]
		{
			powerHistory.Count
		});
		if (this.AreCriticalAssetsLoaded())
		{
			GameState.Get().OnPowerHistory(powerHistory);
			return;
		}
		this.m_queuedPowerHistory.Enqueue(powerHistory);
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x000D6A84 File Offset: 0x000D4C84
	private void OnAllOptions()
	{
		Network.Options options = Network.Get().GetOptions();
		global::Log.LoadingScreen.Print("Gameplay.OnAllOptions() - id={0}", new object[]
		{
			options.ID
		});
		GameState.Get().OnAllOptions(options);
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x000D6ACC File Offset: 0x000D4CCC
	private void OnEntityChoices()
	{
		Network.EntityChoices entityChoices = Network.Get().GetEntityChoices();
		global::Log.LoadingScreen.Print("Gameplay.OnEntityChoices() - id={0}", new object[]
		{
			entityChoices.ID
		});
		GameState.Get().OnEntityChoices(entityChoices);
	}

	// Token: 0x06002A94 RID: 10900 RVA: 0x000D6B14 File Offset: 0x000D4D14
	private void OnEntitiesChosen()
	{
		Network.EntitiesChosen entitiesChosen = Network.Get().GetEntitiesChosen();
		GameState.Get().OnEntitiesChosen(entitiesChosen);
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x000D6B37 File Offset: 0x000D4D37
	private void OnUserUI()
	{
		if (RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().HandleAction(Network.Get().GetUserUI());
		}
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x000D6B5C File Offset: 0x000D4D5C
	private void OnOptionRejected()
	{
		int nackOption = Network.Get().GetNAckOption();
		GameState.Get().OnOptionRejected(nackOption);
	}

	// Token: 0x06002A97 RID: 10903 RVA: 0x000D6B80 File Offset: 0x000D4D80
	private void OnTurnTimerUpdate()
	{
		Network.TurnTimerInfo turnTimerInfo = Network.Get().GetTurnTimerInfo();
		GameState.Get().OnTurnTimerUpdate(turnTimerInfo);
	}

	// Token: 0x06002A98 RID: 10904 RVA: 0x000D6BA4 File Offset: 0x000D4DA4
	private void OnSpectatorNotify()
	{
		SpectatorNotify spectatorNotify = Network.Get().GetSpectatorNotify();
		GameState.Get().OnSpectatorNotifyEvent(spectatorNotify);
	}

	// Token: 0x06002A99 RID: 10905 RVA: 0x000D6BC8 File Offset: 0x000D4DC8
	private void OnAIDebugInformation()
	{
		AIDebugInformation aidebugInformation = Network.Get().GetAIDebugInformation();
		AIDebugDisplay.Get().OnAIDebugInformation(aidebugInformation);
	}

	// Token: 0x06002A9A RID: 10906 RVA: 0x000D6BEC File Offset: 0x000D4DEC
	private void OnRopeTimerDebugInformation()
	{
		RopeTimerDebugInformation ropeTimerDebugInformation = Network.Get().GetRopeTimerDebugInformation();
		RopeTimerDebugDisplay.Get().OnRopeTimerDebugInformation(ropeTimerDebugInformation);
	}

	// Token: 0x06002A9B RID: 10907 RVA: 0x000D6C10 File Offset: 0x000D4E10
	private void OnDebugMessage()
	{
		DebugMessage debugMessage = Network.Get().GetDebugMessage();
		DebugMessageManager.Get().OnDebugMessage(debugMessage);
	}

	// Token: 0x06002A9C RID: 10908 RVA: 0x000D6C34 File Offset: 0x000D4E34
	private void OnScriptDebugInformation()
	{
		ScriptDebugInformation scriptDebugInformation = Network.Get().GetScriptDebugInformation();
		ScriptDebugDisplay.Get().OnScriptDebugInfo(scriptDebugInformation);
	}

	// Token: 0x06002A9D RID: 10909 RVA: 0x000D6C58 File Offset: 0x000D4E58
	private void OnGameRoundHistory()
	{
		GameRoundHistory gameRoundHistory = Network.Get().GetGameRoundHistory();
		if (PlayerLeaderboardManager.Get() != null)
		{
			PlayerLeaderboardManager.Get().UpdateRoundHistory(gameRoundHistory);
		}
	}

	// Token: 0x06002A9E RID: 10910 RVA: 0x000D6C88 File Offset: 0x000D4E88
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
		if (!Array.Exists<TAG_RACE>(availableRacesInBattlegroundsExcludingAmalgam, (TAG_RACE race) => race == TAG_RACE.INVALID))
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

	// Token: 0x06002A9F RID: 10911 RVA: 0x000D6D54 File Offset: 0x000D4F54
	private void OnGameGuardianVars()
	{
		GameGuardianVars gameGuardianVars = Network.Get().GetGameGuardianVars();
		if (GameState.Get() != null)
		{
			GameState.Get().UpdateGameGuardianVars(gameGuardianVars);
		}
	}

	// Token: 0x06002AA0 RID: 10912 RVA: 0x000D6D80 File Offset: 0x000D4F80
	private void OnBattlegroundInfo()
	{
		UpdateBattlegroundInfo battlegroundInfo = Network.Get().GetBattlegroundInfo();
		if (GameState.Get() != null)
		{
			GameState.Get().UpdateBattlegroundInfo(battlegroundInfo);
		}
	}

	// Token: 0x06002AA1 RID: 10913 RVA: 0x000D6DAC File Offset: 0x000D4FAC
	private void OnScriptLogMessage()
	{
		ScriptLogMessage scriptLogMessage = Network.Get().GetScriptLogMessage();
		if (SceneDebugger.Get() != null)
		{
			SceneDebugger.Get().AddServerScriptLogMessage(scriptLogMessage);
		}
	}

	// Token: 0x06002AA2 RID: 10914 RVA: 0x000D6DD6 File Offset: 0x000D4FD6
	private bool AreCriticalAssetsLoaded()
	{
		return this.m_criticalAssetsLoaded;
	}

	// Token: 0x06002AA3 RID: 10915 RVA: 0x000D6DE0 File Offset: 0x000D4FE0
	private bool CheckCriticalAssetLoads()
	{
		if (this.m_criticalAssetsLoaded)
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
		if (this.GetBoardLayout() == null)
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
		if (global::TurnTimer.Get() == null)
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
		this.m_criticalAssetsLoaded = true;
		this.ProcessQueuedPowerHistory();
		return true;
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x000D6EFC File Offset: 0x000D50FC
	private void InitCardBacks()
	{
		int cardBackId = GameState.Get().GetFriendlySidePlayer().GetCardBackId();
		int cardBackId2 = GameState.Get().GetOpposingSidePlayer().GetCardBackId();
		CardBackManager.Get().SetGameCardBackIDs(cardBackId, cardBackId2);
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x000D6F38 File Offset: 0x000D5138
	private void LoadBoard(Network.GameSetup setup)
	{
		BoardDbfRecord record = GameDbf.Board.GetRecord(setup.Board);
		if (record == null)
		{
			Debug.LogError(string.Format("Gameplay.LoadBoard() - FAILED to load board id: \"{0}\"", setup.Board));
			UIStatus.Get().AddInfo(string.Format("Failed to Load board ID: {0}, defaulting back to 1.", setup.Board));
			record = GameDbf.Board.GetRecord(1);
		}
		AssetReference assetRef = new AssetReference(record.Prefab);
		AssetLoader.Get().InstantiatePrefab(assetRef, new PrefabCallback<GameObject>(this.OnBoardLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x000D6FC6 File Offset: 0x000D51C6
	private IEnumerator NotifyPlayersOfBoardLoad()
	{
		while (this.GetBoardLayout() == null)
		{
			yield return null;
		}
		using (global::Map<int, global::Player>.ValueCollection.Enumerator enumerator = GameState.Get().GetPlayerMap().Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				global::Player player = enumerator.Current;
				player.OnBoardLoaded();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06002AA7 RID: 10919 RVA: 0x000D6FD8 File Offset: 0x000D51D8
	private void OnBoardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_boardProgress = -1f;
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnBoardLoaded() - FAILED to load board \"{0}\"", go));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.GetComponent<Board>().SetBoardDbId(GameMgr.Get().GetGameSetup().Board);
		string input = UniversalInputManager.UsePhoneUI ? "BoardCameras_phone.prefab:1e862adebb4fd4fca8b24249d32f8d86" : "BoardCameras.prefab:b4f3a6717904ff34985655c86149f06c";
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnBoardCamerasLoaded), null, AssetLoadingOptions.None);
		AssetLoader.Get().InstantiatePrefab("BoardStandardGame.prefab:b87d693f752160b43a25b7cec3787122", new PrefabCallback<GameObject>(this.OnBoardStandardGameLoaded), null, AssetLoadingOptions.None);
		if (GameMgr.Get().IsTutorial())
		{
			AssetLoader.Get().InstantiatePrefab("BoardTutorial.prefab:08bd830fc30e15e48a4b56bfc3abee15", new PrefabCallback<GameObject>(this.OnBoardTutorialLoaded), null, AssetLoadingOptions.None);
		}
	}

	// Token: 0x06002AA8 RID: 10920 RVA: 0x000D70BD File Offset: 0x000D52BD
	private void OnBoardProgressUpdate(string name, float progress, object callbackData)
	{
		this.m_boardProgress = progress;
	}

	// Token: 0x06002AA9 RID: 10921 RVA: 0x000D70C8 File Offset: 0x000D52C8
	private void OnBoardCamerasLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnBoardCamerasLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = Board.Get().transform;
		this.m_inputCamera = Camera.main;
		PegUI.Get().AddInputCamera(this.m_inputCamera);
		AssetLoader.Get().InstantiatePrefab("CardTypeBanner.prefab:3b446c3c5a48357438d8aa969b5c377d", AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("BigCard.prefab:c938058e4609a1146b7ce8a115cc82df", AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002AAA RID: 10922 RVA: 0x000D715C File Offset: 0x000D535C
	private void OnBoardStandardGameLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnBoardStandardGameLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.m_boardLayout = go.GetComponent<BoardLayout>();
		go.transform.parent = Board.Get().transform;
		AssetLoader.Get().InstantiatePrefab("EndTurnButton.prefab:313ebd8bcb770a944be3633ad928096b", new PrefabCallback<GameObject>(this.OnEndTurnButtonLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		AssetLoader.Get().InstantiatePrefab("TurnTimer.prefab:aa1be1e4f5b36ca4aa6a38ac7d0538ce", new PrefabCallback<GameObject>(this.OnTurnTimerLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002AAB RID: 10923 RVA: 0x000D71F9 File Offset: 0x000D53F9
	private void OnBoardTutorialLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnBoardTutorialLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = Board.Get().transform;
	}

	// Token: 0x06002AAC RID: 10924 RVA: 0x000D723C File Offset: 0x000D543C
	private void OnEndTurnButtonLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnEndTurnButtonLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		EndTurnButton component = go.GetComponent<EndTurnButton>();
		if (component == null)
		{
			Debug.LogError(string.Format("Gameplay.OnEndTurnButtonLoaded() - ERROR \"{0}\" has no {1} component", base.name, typeof(EndTurnButton)));
			return;
		}
		component.transform.position = Board.Get().FindBone("EndTurnButton").position;
		foreach (Renderer renderer in go.GetComponentsInChildren<Renderer>())
		{
			if (!renderer.gameObject.GetComponent<TextMesh>())
			{
				renderer.GetMaterial().color = Board.Get().m_EndTurnButtonColor;
			}
		}
	}

	// Token: 0x06002AAD RID: 10925 RVA: 0x000D7304 File Offset: 0x000D5504
	private void OnTurnTimerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnTurnTimerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		global::TurnTimer component = go.GetComponent<global::TurnTimer>();
		if (component == null)
		{
			Debug.LogError(string.Format("Gameplay.OnTurnTimerLoaded() - ERROR \"{0}\" has no {1} component", base.name, typeof(global::TurnTimer)));
			return;
		}
		component.transform.position = Board.Get().FindBone("TurnTimerBone").position;
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x000D7389 File Offset: 0x000D5589
	private void OnRemoteActionHandlerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnRemoteActionHandlerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AAF RID: 10927 RVA: 0x000D73C5 File Offset: 0x000D55C5
	private void OnTagVisualConfigurationLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnTagVisualConfigurationLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AB0 RID: 10928 RVA: 0x000D7401 File Offset: 0x000D5601
	private void OnChoiceCardMgrLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnChoiceCardMgrLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AB1 RID: 10929 RVA: 0x000D743D File Offset: 0x000D563D
	private void OnInputManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnInputManagerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AB2 RID: 10930 RVA: 0x000D7479 File Offset: 0x000D5679
	private void OnMulliganManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnMulliganManagerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AB3 RID: 10931 RVA: 0x000D74B5 File Offset: 0x000D56B5
	private void OnTurnStartManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnTurnStartManagerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
	}

	// Token: 0x06002AB4 RID: 10932 RVA: 0x000D74F4 File Offset: 0x000D56F4
	private void OnTargetReticleManagerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnTargetReticleManagerLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.transform.parent = base.transform;
		TargetReticleManager.Get().PreloadTargetArrows();
	}

	// Token: 0x06002AB5 RID: 10933 RVA: 0x000D7548 File Offset: 0x000D5748
	private void OnPlayerBannerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		global::Player.Side side = (global::Player.Side)callbackData;
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnPlayerBannerLoaded() - FAILED to load \"{0}\" side={1}", assetRef, side.ToString()));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		NameBanner component = go.GetComponent<NameBanner>();
		if (component == null)
		{
			Debug.LogError(string.Format("Gameplay.OnPlayerBannerLoaded() - FAILED to to find NameBanner component on \"{0}\" side={1}", assetRef, side.ToString()));
			return;
		}
		this.m_nameBanners.Add(component);
		this.m_numBannersRequested--;
		if (UniversalInputManager.UsePhoneUI)
		{
			if (base.name == "NameBannerGamePlay_phone")
			{
				this.m_nameBannerGamePlayPhone = component;
				this.m_nameBannerGamePlayPhone.Initialize(side);
			}
			else
			{
				component.Initialize(side);
			}
		}
		else
		{
			component.Initialize(side);
			if (!string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAlternatePlayerName()) && component.GetPlayerSide() == global::Player.Side.FRIENDLY)
			{
				component.UseLongName();
			}
		}
		base.StartCoroutine(this.ShowBannersWhenReady());
	}

	// Token: 0x06002AB6 RID: 10934 RVA: 0x000D764F File Offset: 0x000D584F
	private IEnumerator ShowBannersWhenReady()
	{
		if (this.m_numBannersRequested > 0)
		{
			yield break;
		}
		foreach (NameBanner banner in this.m_nameBanners)
		{
			while (banner.IsWaitingForMedal)
			{
				yield return null;
			}
			banner = null;
		}
		List<NameBanner>.Enumerator enumerator = default(List<NameBanner>.Enumerator);
		using (List<NameBanner>.Enumerator enumerator2 = this.m_nameBanners.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				NameBanner nameBanner = enumerator2.Current;
				nameBanner.Show();
			}
			yield break;
		}
		yield break;
		yield break;
	}

	// Token: 0x06002AB7 RID: 10935 RVA: 0x000D7660 File Offset: 0x000D5860
	private void OnCardDrawStandinLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("Gameplay.OnCardDrawStandinLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		this.m_cardDrawStandIn = go.GetComponent<Actor>();
		go.GetComponentInChildren<CardBackDisplay>().SetCardBack(CardBackManager.CardBackSlot.FRIENDLY);
		this.m_cardDrawStandIn.Hide();
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x000D76BC File Offset: 0x000D58BC
	private void OnTransitionFinished(bool cutoff, object userData)
	{
		LoadingScreen.Get().UnregisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFinished));
		if (cutoff)
		{
			return;
		}
		if (this.IsHandlingNetworkProblem())
		{
			return;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			if (GameState.Get() != null && GameState.Get().IsMulliganPhase())
			{
				AssetLoader.Get().InstantiatePrefab("NameBannerRight_phone.prefab:8712bbdedd6fa4a45b18dc88226d67b3", new PrefabCallback<GameObject>(this.OnPlayerBannerLoaded), global::Player.Side.FRIENDLY, AssetLoadingOptions.None);
				AssetLoader.Get().InstantiatePrefab("NameBanner_phone.prefab:c919b2370a8d748d38e2cb4708e15398", new PrefabCallback<GameObject>(this.OnPlayerBannerLoaded), global::Player.Side.OPPOSING, AssetLoadingOptions.None);
				this.m_numBannersRequested += 2;
				return;
			}
			if (GameMgr.Get() != null && !GameMgr.Get().IsTutorial())
			{
				this.AddGamePlayNameBannerPhone();
				return;
			}
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab("NameBanner.prefab:f579c831653574d4da0437a5fcf0d58f", new PrefabCallback<GameObject>(this.OnPlayerBannerLoaded), global::Player.Side.FRIENDLY, AssetLoadingOptions.None);
			AssetLoader.Get().InstantiatePrefab("NameBanner.prefab:f579c831653574d4da0437a5fcf0d58f", new PrefabCallback<GameObject>(this.OnPlayerBannerLoaded), global::Player.Side.OPPOSING, AssetLoadingOptions.None);
			this.m_numBannersRequested += 2;
		}
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x000D77E8 File Offset: 0x000D59E8
	private void ProcessQueuedPowerHistory()
	{
		while (this.m_queuedPowerHistory.Count > 0)
		{
			List<Network.PowerHistory> powerList = this.m_queuedPowerHistory.Dequeue();
			GameState.Get().OnPowerHistory(powerList);
		}
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x000D781C File Offset: 0x000D5A1C
	private bool IsLeavingGameUnfinished()
	{
		return (GameState.Get() == null || !GameState.Get().IsGameOver()) && !GameMgr.Get().IsReconnect() && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR);
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x000D7852 File Offset: 0x000D5A52
	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		if (phase == GameState.CreateGamePhase.CREATING)
		{
			this.InitCardBacks();
			base.StartCoroutine(this.NotifyPlayersOfBoardLoad());
			return;
		}
		if (phase == GameState.CreateGamePhase.CREATED)
		{
			CardBackManager.Get().UpdateAllCardBacksInSceneWhenReady();
		}
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x000D787A File Offset: 0x000D5A7A
	private IEnumerator SetGameStateBusyWithDelay(bool busy, float delay)
	{
		yield return new WaitForSeconds(delay);
		GameState.Get().SetBusy(busy);
		yield break;
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x000D7890 File Offset: 0x000D5A90
	public void SaveOriginalTimeScale()
	{
		this.m_originalTimeScale = new float?(TimeScaleMgr.Get().GetGameTimeScale());
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x000D78A7 File Offset: 0x000D5AA7
	public void RestoreOriginalTimeScale()
	{
		if (this.m_originalTimeScale != null)
		{
			TimeScaleMgr.Get().SetGameTimeScale(this.m_originalTimeScale.Value);
			this.m_originalTimeScale = null;
		}
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x000D78D8 File Offset: 0x000D5AD8
	public Coroutine RegisterCoroutine(IEnumerator routine)
	{
		return base.StartCoroutine(routine);
	}

	// Token: 0x06002AC0 RID: 10944 RVA: 0x000D78E1 File Offset: 0x000D5AE1
	public void UnregisterCoroutine(Coroutine routine)
	{
		base.StopCoroutine(routine);
	}

	// Token: 0x06002AC1 RID: 10945 RVA: 0x000D78EA File Offset: 0x000D5AEA
	private bool OnProcessCheat_saveme(string func, string[] args, string rawArgs)
	{
		GameState.Get().DebugNukeServerBlocks();
		return true;
	}

	// Token: 0x06002AC2 RID: 10946 RVA: 0x000D78F8 File Offset: 0x000D5AF8
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

	// Token: 0x040017E3 RID: 6115
	private static Gameplay s_instance;

	// Token: 0x040017E4 RID: 6116
	private bool m_unloading;

	// Token: 0x040017E5 RID: 6117
	private BnetErrorInfo m_lastFatalBnetErrorInfo;

	// Token: 0x040017E6 RID: 6118
	private bool m_handleLastFatalBnetErrorNow;

	// Token: 0x040017E7 RID: 6119
	private float m_boardProgress;

	// Token: 0x040017E8 RID: 6120
	private List<NameBanner> m_nameBanners = new List<NameBanner>();

	// Token: 0x040017E9 RID: 6121
	private NameBanner m_nameBannerGamePlayPhone;

	// Token: 0x040017EA RID: 6122
	private int m_numBannersRequested;

	// Token: 0x040017EB RID: 6123
	private Actor m_cardDrawStandIn;

	// Token: 0x040017EC RID: 6124
	private BoardLayout m_boardLayout;

	// Token: 0x040017ED RID: 6125
	private bool m_criticalAssetsLoaded;

	// Token: 0x040017EE RID: 6126
	private Queue<List<Network.PowerHistory>> m_queuedPowerHistory = new Queue<List<Network.PowerHistory>>();

	// Token: 0x040017EF RID: 6127
	private float? m_originalTimeScale;

	// Token: 0x040017F0 RID: 6128
	private Camera m_inputCamera;
}
