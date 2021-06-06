using System;
using System.Collections.Generic;
using Assets;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusClient;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;

// Token: 0x020008C5 RID: 2245
public class GameMgr : IService
{
	// Token: 0x1400007E RID: 126
	// (add) Token: 0x06007B80 RID: 31616 RVA: 0x00280CA8 File Offset: 0x0027EEA8
	// (remove) Token: 0x06007B81 RID: 31617 RVA: 0x00280CE0 File Offset: 0x0027EEE0
	public event Action OnTransitionPopupShown;

	// Token: 0x06007B82 RID: 31618 RVA: 0x00280D15 File Offset: 0x0027EF15
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		this.MATCHING_POPUP_NAME = new PlatformDependentValue<string>(PlatformCategory.Screen)
		{
			PC = "MatchingPopup3D.prefab:4f4a40d14d907e94da1b81d97c18a44f",
			Phone = "MatchingPopup3D_phone.prefab:a7a5cea6306a1fa4680a9782fd25be14"
		};
		Network network = serviceLocator.Get<Network>();
		network.RegisterGameQueueHandler(new Network.GameQueueHandler(this.OnGameQueueEvent));
		network.RegisterNetHandler(GameToConnectNotification.PacketID.ID, new Network.NetHandler(this.OnGameToJoinNotification), null);
		network.RegisterNetHandler(GameSetup.PacketID.ID, new Network.NetHandler(this.OnGameSetup), null);
		network.RegisterNetHandler(GameCanceled.PacketID.ID, new Network.NetHandler(this.OnGameCanceled), null);
		network.RegisterNetHandler(ServerResult.PacketID.ID, new Network.NetHandler(this.OnServerResult), null);
		network.AddBnetErrorListener(BnetFeature.Games, new Network.BnetErrorCallback(this.OnBnetError));
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		yield break;
	}

	// Token: 0x06007B83 RID: 31619 RVA: 0x001B7846 File Offset: 0x001B5A46
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network)
		};
	}

	// Token: 0x06007B84 RID: 31620 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06007B85 RID: 31621 RVA: 0x00280D2C File Offset: 0x0027EF2C
	private void WillReset()
	{
		this.m_gameType = GameType.GT_UNKNOWN;
		this.m_prevGameType = GameType.GT_UNKNOWN;
		this.m_nextGameType = GameType.GT_UNKNOWN;
		this.m_formatType = FormatType.FT_UNKNOWN;
		this.m_prevFormatType = FormatType.FT_UNKNOWN;
		this.m_nextFormatType = FormatType.FT_UNKNOWN;
		this.m_missionId = 0;
		this.m_prevMissionId = 0;
		this.m_nextMissionId = 0;
		this.m_brawlLibraryItemId = 0;
		this.m_nextBrawlLibraryItemId = 0;
		this.m_reconnectType = ReconnectType.INVALID;
		this.m_prevReconnectType = ReconnectType.INVALID;
		this.m_nextReconnectType = ReconnectType.INVALID;
		this.m_readyToProcessGameConnections = false;
		this.m_deferredGameConnectionInfo = null;
		this.m_spectator = false;
		this.m_prevSpectator = false;
		this.m_nextSpectator = false;
		this.m_lastEnterGameError = 0U;
		this.m_findGameState = FindGameState.INVALID;
		this.m_gameSetup = null;
		this.m_lastDisplayedPlayerNames.Clear();
		this.m_connectionInfoForGameConnectingTo = null;
		this.m_lastGameData.Clear();
	}

	// Token: 0x06007B86 RID: 31622 RVA: 0x00280DF0 File Offset: 0x0027EFF0
	public static GameMgr Get()
	{
		return HearthstoneServices.Get<GameMgr>();
	}

	// Token: 0x06007B87 RID: 31623 RVA: 0x00280DF8 File Offset: 0x0027EFF8
	public void OnLoggedIn()
	{
		SceneMgr.Get().RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		ReconnectMgr.Get().AddTimeoutListener(new ReconnectMgr.TimeoutCallback(this.OnReconnectTimeout));
	}

	// Token: 0x06007B88 RID: 31624 RVA: 0x00280E48 File Offset: 0x0027F048
	public GameType GetGameType()
	{
		return this.m_gameType;
	}

	// Token: 0x06007B89 RID: 31625 RVA: 0x00280E50 File Offset: 0x0027F050
	public GameType GetPreviousGameType()
	{
		return this.m_prevGameType;
	}

	// Token: 0x06007B8A RID: 31626 RVA: 0x00280E58 File Offset: 0x0027F058
	public GameType GetNextGameType()
	{
		return this.m_nextGameType;
	}

	// Token: 0x06007B8B RID: 31627 RVA: 0x00280E60 File Offset: 0x0027F060
	public FormatType GetFormatType()
	{
		return this.m_formatType;
	}

	// Token: 0x06007B8C RID: 31628 RVA: 0x00280E68 File Offset: 0x0027F068
	public FormatType GetPreviousFormatType()
	{
		return this.m_prevFormatType;
	}

	// Token: 0x06007B8D RID: 31629 RVA: 0x00280E70 File Offset: 0x0027F070
	public FormatType GetNextFormatType()
	{
		return this.m_nextFormatType;
	}

	// Token: 0x06007B8E RID: 31630 RVA: 0x00280E78 File Offset: 0x0027F078
	public int GetMissionId()
	{
		return this.m_missionId;
	}

	// Token: 0x06007B8F RID: 31631 RVA: 0x00280E80 File Offset: 0x0027F080
	public int GetPreviousMissionId()
	{
		return this.m_prevMissionId;
	}

	// Token: 0x06007B90 RID: 31632 RVA: 0x00280E88 File Offset: 0x0027F088
	public int GetNextMissionId()
	{
		return this.m_nextMissionId;
	}

	// Token: 0x06007B91 RID: 31633 RVA: 0x00280E90 File Offset: 0x0027F090
	public ReconnectType GetReconnectType()
	{
		return this.m_reconnectType;
	}

	// Token: 0x06007B92 RID: 31634 RVA: 0x00280E98 File Offset: 0x0027F098
	public ReconnectType GetPreviousReconnectType()
	{
		return this.m_prevReconnectType;
	}

	// Token: 0x06007B93 RID: 31635 RVA: 0x00280EA0 File Offset: 0x0027F0A0
	public ReconnectType GetNextReconnectType()
	{
		return this.m_nextReconnectType;
	}

	// Token: 0x06007B94 RID: 31636 RVA: 0x00280EA8 File Offset: 0x0027F0A8
	public bool IsReconnect()
	{
		return this.m_reconnectType > ReconnectType.INVALID;
	}

	// Token: 0x06007B95 RID: 31637 RVA: 0x00280EB3 File Offset: 0x0027F0B3
	public bool IsPreviousReconnect()
	{
		return this.m_prevReconnectType > ReconnectType.INVALID;
	}

	// Token: 0x06007B96 RID: 31638 RVA: 0x00280EBE File Offset: 0x0027F0BE
	public bool IsNextReconnect()
	{
		return this.m_nextReconnectType > ReconnectType.INVALID;
	}

	// Token: 0x06007B97 RID: 31639 RVA: 0x00280EC9 File Offset: 0x0027F0C9
	public bool IsSpectator()
	{
		return this.m_spectator;
	}

	// Token: 0x06007B98 RID: 31640 RVA: 0x00280ED1 File Offset: 0x0027F0D1
	public bool WasSpectator()
	{
		return this.m_prevSpectator;
	}

	// Token: 0x06007B99 RID: 31641 RVA: 0x00280ED9 File Offset: 0x0027F0D9
	public bool IsNextSpectator()
	{
		return this.m_nextSpectator;
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x06007B9A RID: 31642 RVA: 0x00280EE1 File Offset: 0x0027F0E1
	public long? LastDeckId
	{
		get
		{
			return this.m_lastDeckId;
		}
	}

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x06007B9B RID: 31643 RVA: 0x00280EE9 File Offset: 0x0027F0E9
	public int? LastHeroCardDbId
	{
		get
		{
			return this.m_lastHeroCardDbId;
		}
	}

	// Token: 0x06007B9C RID: 31644 RVA: 0x00280EF1 File Offset: 0x0027F0F1
	public uint GetLastEnterGameError()
	{
		return this.m_lastEnterGameError;
	}

	// Token: 0x06007B9D RID: 31645 RVA: 0x00280EF9 File Offset: 0x0027F0F9
	public bool IsPendingAutoConcede()
	{
		return this.m_pendingAutoConcede;
	}

	// Token: 0x06007B9E RID: 31646 RVA: 0x00280F01 File Offset: 0x0027F101
	public void SetPendingAutoConcede(bool pendingAutoConcede)
	{
		if (!Network.Get().IsConnectedToGameServer())
		{
			return;
		}
		this.m_pendingAutoConcede = pendingAutoConcede;
	}

	// Token: 0x06007B9F RID: 31647 RVA: 0x00280F17 File Offset: 0x0027F117
	public Network.GameSetup GetGameSetup()
	{
		return this.m_gameSetup;
	}

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x06007BA0 RID: 31648 RVA: 0x00280F1F File Offset: 0x0027F11F
	public LastGameData LastGameData
	{
		get
		{
			return this.m_lastGameData;
		}
	}

	// Token: 0x06007BA1 RID: 31649 RVA: 0x00280F28 File Offset: 0x0027F128
	public bool ConnectToGame(GameConnectionInfo info)
	{
		if (info == null)
		{
			global::Log.GameMgr.PrintWarning("ConnectToGame() called with no GameConnectionInfo passed in!", Array.Empty<object>());
			return false;
		}
		if (!this.m_readyToProcessGameConnections)
		{
			global::Log.GameMgr.Print("Received a GameConnectionInfo packet before the game is finished initializing; deferring it until later.", Array.Empty<object>());
			if (this.m_deferredGameConnectionInfo != null)
			{
				global::Log.GameMgr.PrintWarning("Another deferredGameConnectionInfo packet already exists.  Older packet GameType: {0}  Newer packet GameType: {1}", new object[]
				{
					this.m_deferredGameConnectionInfo.GameType,
					info.GameType
				});
				global::Log.GameMgr.PrintWarning("Stomping over another deferred GameConnectionInfo packet.", Array.Empty<object>());
			}
			this.m_deferredGameConnectionInfo = info;
			return false;
		}
		FindGameState? findGameState = GameMgr.s_bnetToFindGameResultMap[QueueEvent.Type.QUEUE_GAME_STARTED];
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = info.Address;
		gameServerInfo.Port = (uint)info.Port;
		gameServerInfo.GameHandle = (uint)info.GameHandle;
		gameServerInfo.ClientHandle = info.ClientHandle;
		gameServerInfo.AuroraPassword = info.AuroraPassword;
		gameServerInfo.Mission = info.Scenario;
		this.m_nextGameType = info.GameType;
		this.m_nextFormatType = info.FormatType;
		this.m_nextMissionId = info.Scenario;
		this.m_connectionInfoForGameConnectingTo = info;
		gameServerInfo.Version = BattleNet.GetVersion();
		gameServerInfo.Resumable = true;
		QueueEvent queueEvent = new QueueEvent(QueueEvent.Type.QUEUE_GAME_STARTED, 0, 0, 0, gameServerInfo);
		this.ChangeFindGameState(findGameState.Value, queueEvent, queueEvent.GameServer, null);
		return true;
	}

	// Token: 0x06007BA2 RID: 31650 RVA: 0x00281080 File Offset: 0x0027F280
	public bool ConnectToGameIfHaveDeferredConnectionPacket()
	{
		this.m_readyToProcessGameConnections = true;
		if (this.m_deferredGameConnectionInfo != null)
		{
			bool result = this.ConnectToGame(this.m_deferredGameConnectionInfo);
			this.m_deferredGameConnectionInfo = null;
			return result;
		}
		return false;
	}

	// Token: 0x06007BA3 RID: 31651 RVA: 0x002810A6 File Offset: 0x0027F2A6
	public FindGameState GetFindGameState()
	{
		return this.m_findGameState;
	}

	// Token: 0x06007BA4 RID: 31652 RVA: 0x002810AE File Offset: 0x0027F2AE
	public bool IsFindingGame()
	{
		return this.m_findGameState > FindGameState.INVALID;
	}

	// Token: 0x06007BA5 RID: 31653 RVA: 0x002810BC File Offset: 0x0027F2BC
	public bool IsAboutToStopFindingGame()
	{
		switch (this.m_findGameState)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			return true;
		}
		return false;
	}

	// Token: 0x06007BA6 RID: 31654 RVA: 0x00281105 File Offset: 0x0027F305
	public void RegisterFindGameEvent(GameMgr.FindGameCallback callback)
	{
		this.RegisterFindGameEvent(callback, null);
	}

	// Token: 0x06007BA7 RID: 31655 RVA: 0x00281110 File Offset: 0x0027F310
	public void RegisterFindGameEvent(GameMgr.FindGameCallback callback, object userData)
	{
		GameMgr.FindGameListener findGameListener = new GameMgr.FindGameListener();
		findGameListener.SetCallback(callback);
		findGameListener.SetUserData(userData);
		if (this.m_findGameListeners.Contains(findGameListener))
		{
			return;
		}
		this.m_findGameListeners.Add(findGameListener);
	}

	// Token: 0x06007BA8 RID: 31656 RVA: 0x0028114C File Offset: 0x0027F34C
	public bool UnregisterFindGameEvent(GameMgr.FindGameCallback callback)
	{
		return this.UnregisterFindGameEvent(callback, null);
	}

	// Token: 0x06007BA9 RID: 31657 RVA: 0x00281158 File Offset: 0x0027F358
	public bool UnregisterFindGameEvent(GameMgr.FindGameCallback callback, object userData)
	{
		GameMgr.FindGameListener findGameListener = new GameMgr.FindGameListener();
		findGameListener.SetCallback(callback);
		findGameListener.SetUserData(userData);
		return this.m_findGameListeners.Remove(findGameListener);
	}

	// Token: 0x06007BAA RID: 31658 RVA: 0x00281188 File Offset: 0x0027F388
	private void FindGameInternal(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId, long deckId, string aiDeck, int heroCardDbId, int? seasonId, bool restoreSavedGameState, byte[] snapshot, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		this.m_lastEnterGameError = 0U;
		this.m_nextGameType = gameType;
		this.m_nextFormatType = formatType;
		this.m_nextMissionId = missionId;
		this.m_nextBrawlLibraryItemId = brawlLibraryItemId;
		this.m_lastDeckId = new long?(deckId);
		this.m_lastAIDeck = aiDeck;
		this.m_lastHeroCardDbId = new int?(heroCardDbId);
		this.m_lastSeasonId = seasonId;
		this.ChangeFindGameState(FindGameState.CLIENT_STARTED);
		Network.Get().FindGame(gameType, formatType, missionId, brawlLibraryItemId, deckId, aiDeck, heroCardDbId, seasonId, restoreSavedGameState, snapshot, progFilterOverride);
		this.UpdateSessionPresence(gameType);
	}

	// Token: 0x06007BAB RID: 31659 RVA: 0x00281210 File Offset: 0x0027F410
	public void FindGame(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId = 0, long deckId = 0L, string aiDeck = null, int? seasonId = null, bool restoreSavedGameState = false, byte[] snapshot = null, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		this.FindGameInternal(gameType, formatType, missionId, brawlLibraryItemId, deckId, aiDeck, 0, seasonId, restoreSavedGameState, snapshot, progFilterOverride);
		if (!restoreSavedGameState)
		{
			string text = this.DetermineTransitionPopupForFindGame(gameType, missionId);
			if (text != null)
			{
				this.ShowTransitionPopup(text, missionId);
			}
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			CollectionDeck deck = collectionManager.GetDeck(deckId);
			if (deck != null)
			{
				global::Log.Decks.PrintInfo("Finding Game With Deck:", Array.Empty<object>());
				deck.LogDeckStringInformation();
			}
		}
	}

	// Token: 0x06007BAC RID: 31660 RVA: 0x0028127C File Offset: 0x0027F47C
	public void FindGameWithHero(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId, int heroCardDbId, long deckid = 0L)
	{
		this.FindGameInternal(gameType, formatType, missionId, brawlLibraryItemId, deckid, null, heroCardDbId, null, false, null, GameType.GT_UNKNOWN);
		string text = this.DetermineTransitionPopupForFindGame(gameType, missionId);
		if (text != null)
		{
			this.ShowTransitionPopup(text, missionId);
		}
		global::Log.Decks.PrintInfo("Finding Game With Hero: {0}", new object[]
		{
			heroCardDbId
		});
	}

	// Token: 0x06007BAD RID: 31661 RVA: 0x002812D8 File Offset: 0x0027F4D8
	public void Cheat_ShowTransitionPopup(GameType gameType, FormatType formatType, int missionId)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return;
		}
		this.m_nextMissionId = missionId;
		this.m_nextFormatType = formatType;
		string text = this.DetermineTransitionPopupForFindGame(gameType, missionId);
		if (text != null)
		{
			this.ShowTransitionPopup(text, missionId);
		}
	}

	// Token: 0x06007BAE RID: 31662 RVA: 0x00281310 File Offset: 0x0027F510
	public void RestartGame()
	{
		this.FindGameInternal(this.m_gameType, this.m_formatType, this.m_missionId, this.m_brawlLibraryItemId, this.m_lastDeckId ?? 0L, this.m_lastAIDeck, this.m_lastHeroCardDbId ?? 0, this.m_lastSeasonId, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06007BAF RID: 31663 RVA: 0x0028137F File Offset: 0x0027F57F
	public bool HasLastPlayedDeckId()
	{
		return this.m_lastDeckId != null;
	}

	// Token: 0x06007BB0 RID: 31664 RVA: 0x0028138C File Offset: 0x0027F58C
	public void EnterFriendlyChallengeGameWithDecks(FormatType formatType, BrawlType brawlType, int missionId, int seasonId, int brawlLibraryItemId, DeckShareState player1DeckShareState, long player1DeckId, DeckShareState player2DeckShareState, long player2DeckId, BnetGameAccountId player2GameAccountId)
	{
		Network.Get().EnterFriendlyChallengeGame(formatType, brawlType, missionId, seasonId, brawlLibraryItemId, player1DeckShareState, player1DeckId, player2DeckShareState, player2DeckId, null, null, player2GameAccountId);
	}

	// Token: 0x06007BB1 RID: 31665 RVA: 0x002813C8 File Offset: 0x0027F5C8
	public void EnterFriendlyChallengeGameWithHeroes(FormatType formatType, BrawlType brawlType, int missionId, int seasonId, int brawlLibraryItemId, long player1HeroCardDbId, long player2HeroCardDbId, BnetGameAccountId player2GameAccountId)
	{
		Network.Get().EnterFriendlyChallengeGame(formatType, brawlType, missionId, seasonId, brawlLibraryItemId, DeckShareState.NO_DECK_SHARE, 0L, DeckShareState.NO_DECK_SHARE, 0L, new long?(player1HeroCardDbId), new long?(player2HeroCardDbId), player2GameAccountId);
	}

	// Token: 0x06007BB2 RID: 31666 RVA: 0x002813FC File Offset: 0x0027F5FC
	public void WaitForFriendChallengeToStart(FormatType formatType, BrawlType brawlType, int missionId, int brawlLibraryItemId, bool isBaconGame)
	{
		this.m_nextFormatType = formatType;
		this.m_nextMissionId = missionId;
		this.m_nextBrawlLibraryItemId = brawlLibraryItemId;
		this.m_lastEnterGameError = 0U;
		bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode > FiresideGatheringManager.FiresideGatheringMode.NONE;
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || (brawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL && flag))
		{
			this.m_nextGameType = GameType.GT_FSG_BRAWL_VS_FRIEND;
			this.ChangeFindGameState(FindGameState.CLIENT_STARTED);
		}
		else if (isBaconGame)
		{
			if (PartyManager.Get().GetCurrentPartySize() <= PartyManager.Get().GetBattlegroundsMaxRankedPartySize())
			{
				this.m_nextGameType = GameType.GT_BATTLEGROUNDS;
			}
			else
			{
				this.m_nextGameType = GameType.GT_BATTLEGROUNDS_FRIENDLY;
			}
			this.ChangeFindGameState(FindGameState.BNET_QUEUE_ENTERED);
		}
		else
		{
			this.m_nextGameType = GameType.GT_VS_FRIEND;
			this.ChangeFindGameState(FindGameState.CLIENT_STARTED);
		}
		string text = this.DetermineTransitionPopupForFindGame(this.m_nextGameType, missionId);
		if (text != null)
		{
			this.ShowTransitionPopup(text, missionId);
			return;
		}
		Debug.LogError("WaitForFriendChallengeToStart - No valid transition popup.");
	}

	// Token: 0x06007BB3 RID: 31667 RVA: 0x002814BC File Offset: 0x0027F6BC
	public void SpectateGame(JoinInfo joinInfo)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = joinInfo.ServerIpAddress;
		gameServerInfo.Port = joinInfo.ServerPort;
		gameServerInfo.GameHandle = (uint)joinInfo.GameHandle;
		gameServerInfo.SpectatorPassword = joinInfo.SecretKey;
		gameServerInfo.SpectatorMode = true;
		this.m_nextGameType = joinInfo.GameType;
		this.m_nextFormatType = joinInfo.FormatType;
		this.m_nextMissionId = joinInfo.MissionId;
		this.m_brawlLibraryItemId = joinInfo.BrawlLibraryItemId;
		this.m_nextSpectator = true;
		this.m_lastEnterGameError = 0U;
		this.ChangeFindGameState(FindGameState.CLIENT_STARTED);
		this.ShowTransitionPopup("LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168", joinInfo.MissionId);
		this.ChangeFindGameState(FindGameState.SERVER_GAME_CONNECTING, gameServerInfo);
		if (Gameplay.Get() == null)
		{
			Network.Get().SetGameServerDisconnectEventListener(new Network.GameServerDisconnectEvent(this.OnGameServerDisconnect));
		}
	}

	// Token: 0x06007BB4 RID: 31668 RVA: 0x0028158A File Offset: 0x0027F78A
	private void OnGameServerDisconnect(BattleNetErrors error)
	{
		this.OnGameCanceled();
	}

	// Token: 0x06007BB5 RID: 31669 RVA: 0x00281594 File Offset: 0x0027F794
	public void ReconnectGame(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo)
	{
		this.m_nextGameType = gameType;
		this.m_nextFormatType = formatType;
		this.m_nextMissionId = serverInfo.Mission;
		this.m_nextBrawlLibraryItemId = serverInfo.BrawlLibraryItemId;
		this.m_nextReconnectType = reconnectType;
		this.m_nextSpectator = serverInfo.SpectatorMode;
		this.m_lastEnterGameError = 0U;
		this.ChangeFindGameState(FindGameState.CLIENT_STARTED);
		this.ChangeFindGameState(FindGameState.SERVER_GAME_CONNECTING, serverInfo);
	}

	// Token: 0x06007BB6 RID: 31670 RVA: 0x002815F8 File Offset: 0x0027F7F8
	public bool CancelFindGame()
	{
		if (!GameUtils.IsMatchmadeGameType(this.m_nextGameType, null))
		{
			return false;
		}
		if (!Network.Get().IsFindingGame())
		{
			return false;
		}
		Network.Get().CancelFindGame();
		if (this.IsFindingGame())
		{
			this.ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		}
		return true;
	}

	// Token: 0x06007BB7 RID: 31671 RVA: 0x00281646 File Offset: 0x0027F846
	public void HideTransitionPopup()
	{
		if (this.m_transitionPopup)
		{
			this.m_transitionPopup.Hide();
		}
	}

	// Token: 0x06007BB8 RID: 31672 RVA: 0x00281660 File Offset: 0x0027F860
	public GameEntity CreateGameEntity(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		FlowPerformanceGame flowPerformanceGame = (hearthstonePerformance != null) ? hearthstonePerformance.GetCurrentPerformanceFlow<FlowPerformanceGame>() : null;
		if (flowPerformanceGame != null)
		{
			flowPerformanceGame.GameUuid = createGame.Uuid;
		}
		ScenarioDbId missionId = (ScenarioDbId)this.m_missionId;
		GameEntity gameEntity;
		if (missionId > ScenarioDbId.TB_HEADLESSHORSEMAN)
		{
			if (missionId <= ScenarioDbId.FB_EXPANSIONDRAFT)
			{
				if (missionId <= ScenarioDbId.TB_FOXBLESSING)
				{
					if (missionId > ScenarioDbId.TB_MARIN)
					{
						if (missionId <= ScenarioDbId.FB_BUILDABRAWL_1P)
						{
							if (missionId == ScenarioDbId.FB_CHAMPS || missionId == ScenarioDbId.FB_CHAMPS_1P)
							{
								goto IL_17F2;
							}
							if (missionId != ScenarioDbId.FB_BUILDABRAWL_1P)
							{
								goto IL_1D18;
							}
						}
						else if (missionId != ScenarioDbId.FB_BUILDABRAWL)
						{
							if (missionId == ScenarioDbId.TB_LETHALPUZZLES_RESTART)
							{
								gameEntity = new TB13_LethalPuzzles_Restart();
								goto IL_1D1E;
							}
							if (missionId != ScenarioDbId.TB_FOXBLESSING)
							{
								goto IL_1D18;
							}
							goto IL_1808;
						}
						gameEntity = new FB_BuildABrawl();
						goto IL_1D1E;
					}
					if (missionId <= ScenarioDbId.FB_ELOBRAWL)
					{
						if (missionId == ScenarioDbId.FB_DUELERSBRAWL_1P)
						{
							goto IL_181E;
						}
						if (missionId == ScenarioDbId.TB_HEADLESSREDUX)
						{
							gameEntity = new TB_HeadlessRedux();
							goto IL_1D1E;
						}
						if (missionId != ScenarioDbId.FB_ELOBRAWL)
						{
							goto IL_1D18;
						}
						gameEntity = new FB_ELObrawl();
						goto IL_1D1E;
					}
					else
					{
						if (missionId == ScenarioDbId.GIL_DUNGEON)
						{
							gameEntity = GIL_Dungeon.InstantiateGilDungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						}
						if (missionId == ScenarioDbId.TB_KOBOLDGIFTS)
						{
							gameEntity = new TB_KoboldGifts();
							goto IL_1D1E;
						}
						if (missionId != ScenarioDbId.TB_MARIN)
						{
							goto IL_1D18;
						}
						gameEntity = new TB_Marin();
						goto IL_1D1E;
					}
				}
				else if (missionId <= ScenarioDbId.TB_FIREFEST2)
				{
					if (missionId <= ScenarioDbId.FB_TOKICOOP)
					{
						if (missionId == ScenarioDbId.GIL_BONUS_CHALLENGE)
						{
							gameEntity = GIL_Dungeon.InstantiateGilDungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						}
						if (missionId != ScenarioDbId.TB_FOXBLESSING_1P)
						{
							if (missionId != ScenarioDbId.FB_TOKICOOP)
							{
								goto IL_1D18;
							}
							goto IL_1829;
						}
					}
					else
					{
						if (missionId == ScenarioDbId.TRL_DUNGEON)
						{
							gameEntity = TRL_Dungeon.InstantiateTRLDungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						}
						if (missionId != ScenarioDbId.TB_FIREFEST2_1P && missionId != ScenarioDbId.TB_FIREFEST2)
						{
							goto IL_1D18;
						}
						gameEntity = new TB_Firefest2();
						goto IL_1D1E;
					}
				}
				else if (missionId <= ScenarioDbId.BOTA_LETHAL_PUZZLE_4)
				{
					if (missionId == ScenarioDbId.BOTA_MIRROR_PUZZLE_1)
					{
						gameEntity = new BOTA_Mirror_Puzzle_1();
						goto IL_1D1E;
					}
					switch (missionId)
					{
					case ScenarioDbId.BOTA_SURVIVAL_PUZZLE_1:
						gameEntity = new BOTA_Survival_Puzzle_1();
						goto IL_1D1E;
					case (ScenarioDbId)2910:
					case (ScenarioDbId)2912:
					case (ScenarioDbId)2915:
					case (ScenarioDbId)2916:
						goto IL_1D18;
					case ScenarioDbId.BOTA_MIRROR_PUZZLE_2:
						gameEntity = new BOTA_Mirror_Puzzle_2();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_MIRROR_PUZZLE_3:
						gameEntity = new BOTA_Mirror_Puzzle_3();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_MIRROR_PUZZLE_4:
						gameEntity = new BOTA_Mirror_Puzzle_4();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_SURVIVAL_PUZZLE_2:
						gameEntity = new BOTA_Survival_Puzzle_2();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_SURVIVAL_PUZZLE_3:
						gameEntity = new BOTA_Survival_Puzzle_3();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_SURVIVAL_PUZZLE_4:
						gameEntity = new BOTA_Survival_Puzzle_4();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_MIRROR_BOOM:
						gameEntity = new BOTA_Mirror_Boom();
						goto IL_1D1E;
					default:
						switch (missionId)
						{
						case ScenarioDbId.FB_DUELERSBRAWL:
							goto IL_181E;
						case ScenarioDbId.BOTA_LETHAL_PUZZLE_1:
							gameEntity = new BOTA_Lethal_Puzzle_1();
							goto IL_1D1E;
						case ScenarioDbId.BOTA_LETHAL_PUZZLE_2:
							gameEntity = new BOTA_Lethal_Puzzle_2();
							goto IL_1D1E;
						case ScenarioDbId.BOTA_LETHAL_PUZZLE_3:
							gameEntity = new BOTA_Lethal_Puzzle_3();
							goto IL_1D1E;
						case ScenarioDbId.BOTA_LETHAL_PUZZLE_4:
							gameEntity = new BOTA_Lethal_Puzzle_4();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
						break;
					}
				}
				else if (missionId <= ScenarioDbId.BOTA_CLEAR_BOOM)
				{
					switch (missionId)
					{
					case ScenarioDbId.BOTA_CLEAR_PUZZLE_1:
						gameEntity = new BOTA_Clear_Puzzle_1();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_CLEAR_PUZZLE_2:
						gameEntity = new BOTA_Clear_Puzzle_2();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_CLEAR_PUZZLE_3:
						gameEntity = new BOTA_Clear_Puzzle_3();
						goto IL_1D1E;
					case ScenarioDbId.BOTA_CLEAR_PUZZLE_4:
						gameEntity = new BOTA_Clear_Puzzle_4();
						goto IL_1D1E;
					case ScenarioDbId.TB_MAMMOTHPARTY_ANYTIME:
						goto IL_1599;
					default:
						switch (missionId)
						{
						case ScenarioDbId.BOTA_LETHAL_BOOM:
							gameEntity = new BOTA_Lethal_Boom();
							goto IL_1D1E;
						case ScenarioDbId.BOTA_SURVIVAL_BOOM:
							gameEntity = new BOTA_Survival_Boom();
							goto IL_1D1E;
						case ScenarioDbId.BOTA_CLEAR_BOOM:
							gameEntity = new BOTA_Clear_Boom();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
						break;
					}
				}
				else
				{
					if (missionId == ScenarioDbId.DALA_01_BANK)
					{
						goto IL_177A;
					}
					if (missionId != ScenarioDbId.FB_EXPANSIONDRAFT)
					{
						goto IL_1D18;
					}
					goto IL_181E;
				}
				IL_1808:
				gameEntity = new TB_NewYearRaven();
				goto IL_1D1E;
				IL_181E:
				gameEntity = new FB_DuelersBrawl();
				goto IL_1D1E;
			}
			if (missionId <= ScenarioDbId.TB_AUTOBRAWL)
			{
				if (missionId <= ScenarioDbId.TB_ARCHIVIST)
				{
					if (missionId <= ScenarioDbId.TB_TROLLSWEEK1_1P)
					{
						if (missionId == ScenarioDbId.FB_TOKICOOP_1P)
						{
							goto IL_1829;
						}
						if (missionId - ScenarioDbId.DALA_02_VIOLET_HOLD <= 3)
						{
							goto IL_177A;
						}
						if (missionId != ScenarioDbId.TB_TROLLSWEEK1_1P)
						{
							goto IL_1D18;
						}
					}
					else if (missionId != ScenarioDbId.TB_TROLLSWEEK1)
					{
						if (missionId == ScenarioDbId.DALA_TAVERN)
						{
							goto IL_1787;
						}
						if (missionId - ScenarioDbId.TB_ARCHIVIST_1P > 1)
						{
							goto IL_1D18;
						}
						gameEntity = new TB_NoMulligan();
						goto IL_1D1E;
					}
					gameEntity = new TB_TrollsWeek1();
					goto IL_1D1E;
				}
				if (missionId <= ScenarioDbId.TB_IGNOBLEGARDEN)
				{
					if (missionId == ScenarioDbId.TB_HENCHMANIA_1P || missionId == ScenarioDbId.TB_HENCHMANIA)
					{
						gameEntity = new TB_Henchmania();
						goto IL_1D1E;
					}
					if (missionId != ScenarioDbId.TB_IGNOBLEGARDEN)
					{
						goto IL_1D18;
					}
				}
				else
				{
					switch (missionId)
					{
					case ScenarioDbId.TB_IGNOBLEGARDEN_1P:
						break;
					case (ScenarioDbId)3326:
					case (ScenarioDbId)3333:
					case (ScenarioDbId)3335:
					case (ScenarioDbId)3336:
					case (ScenarioDbId)3337:
					case (ScenarioDbId)3338:
					case (ScenarioDbId)3339:
					case (ScenarioDbId)3340:
						goto IL_1D18;
					case ScenarioDbId.TB_207TH:
					case ScenarioDbId.TB_207TH_1P:
						gameEntity = new TB_207();
						goto IL_1D1E;
					case ScenarioDbId.DALA_01_BANK_HEROIC:
					case ScenarioDbId.DALA_02_VIOLET_HOLD_HEROIC:
					case ScenarioDbId.DALA_03_STREETS_HEROIC:
					case ScenarioDbId.DALA_04_UNDERBELLY_HEROIC:
					case ScenarioDbId.DALA_05_CITADEL_HEROIC:
						goto IL_177A;
					case ScenarioDbId.TB_RANDOM_DECK_KEEP_WINNER:
						goto IL_1876;
					default:
						if (missionId == ScenarioDbId.TB_DARWIN_CHAMPS)
						{
							goto IL_17F2;
						}
						if (missionId - ScenarioDbId.TB_AUTOBRAWL_1P > 1)
						{
							goto IL_1D18;
						}
						gameEntity = new TB_AutoBrawl();
						goto IL_1D1E;
					}
				}
				gameEntity = new TB_Ignoblegarden();
				goto IL_1D1E;
			}
			else
			{
				if (missionId <= ScenarioDbId.BTP_04_CENARIUS)
				{
					if (missionId <= ScenarioDbId.TB_DRAWNDISOVERY)
					{
						if (missionId == ScenarioDbId.DALA_TAVERN_HEROIC)
						{
							goto IL_1787;
						}
						if (missionId != ScenarioDbId.TB_CAROUSEL_1P)
						{
							if (missionId != ScenarioDbId.TB_DRAWNDISOVERY)
							{
								goto IL_1D18;
							}
							gameEntity = new TB_DrawnDiscovery();
							goto IL_1D1E;
						}
					}
					else
					{
						if (missionId == ScenarioDbId.TB_FIREFEST3_1P || missionId == ScenarioDbId.TB_FIREFEST3)
						{
							gameEntity = new TB_Firefest3();
							goto IL_1D1E;
						}
						switch (missionId)
						{
						case ScenarioDbId.TB_BACON_1P:
						case ScenarioDbId.TB_BACONSHOP_8P:
							gameEntity = new TB_BaconShop();
							goto IL_1D1E;
						case (ScenarioDbId)3415:
						case (ScenarioDbId)3416:
						case (ScenarioDbId)3417:
						case (ScenarioDbId)3418:
						case (ScenarioDbId)3419:
						case (ScenarioDbId)3420:
						case (ScenarioDbId)3421:
						case (ScenarioDbId)3423:
						case (ScenarioDbId)3424:
						case (ScenarioDbId)3425:
						case (ScenarioDbId)3440:
						case (ScenarioDbId)3441:
						case (ScenarioDbId)3442:
						case (ScenarioDbId)3443:
						case (ScenarioDbId)3444:
						case (ScenarioDbId)3445:
						case (ScenarioDbId)3446:
						case (ScenarioDbId)3448:
						case (ScenarioDbId)3450:
						case (ScenarioDbId)3452:
						case (ScenarioDbId)3453:
						case (ScenarioDbId)3454:
						case (ScenarioDbId)3455:
						case (ScenarioDbId)3456:
						case (ScenarioDbId)3457:
						case (ScenarioDbId)3460:
						case (ScenarioDbId)3461:
						case (ScenarioDbId)3462:
						case (ScenarioDbId)3463:
						case (ScenarioDbId)3464:
						case (ScenarioDbId)3465:
						case (ScenarioDbId)3467:
						case (ScenarioDbId)3468:
						case (ScenarioDbId)3474:
						case (ScenarioDbId)3476:
						case (ScenarioDbId)3482:
						case (ScenarioDbId)3485:
						case (ScenarioDbId)3486:
						case (ScenarioDbId)3487:
						case (ScenarioDbId)3492:
						case (ScenarioDbId)3496:
						case (ScenarioDbId)3502:
						case (ScenarioDbId)3503:
						case (ScenarioDbId)3504:
						case (ScenarioDbId)3505:
						case (ScenarioDbId)3506:
						case (ScenarioDbId)3507:
						case (ScenarioDbId)3508:
						case (ScenarioDbId)3509:
						case (ScenarioDbId)3510:
						case (ScenarioDbId)3511:
						case (ScenarioDbId)3512:
						case (ScenarioDbId)3513:
						case (ScenarioDbId)3514:
						case (ScenarioDbId)3515:
						case (ScenarioDbId)3516:
						case (ScenarioDbId)3517:
						case (ScenarioDbId)3518:
						case (ScenarioDbId)3519:
						case (ScenarioDbId)3520:
						case (ScenarioDbId)3521:
						case (ScenarioDbId)3522:
						case (ScenarioDbId)3523:
						case (ScenarioDbId)3524:
						case (ScenarioDbId)3526:
						case (ScenarioDbId)3527:
						case (ScenarioDbId)3528:
						case (ScenarioDbId)3531:
						case (ScenarioDbId)3532:
						case (ScenarioDbId)3533:
						case (ScenarioDbId)3534:
						case (ScenarioDbId)3535:
						case (ScenarioDbId)3536:
						case (ScenarioDbId)3537:
						case (ScenarioDbId)3538:
						case (ScenarioDbId)3542:
						case (ScenarioDbId)3544:
						case (ScenarioDbId)3545:
						case (ScenarioDbId)3546:
						case (ScenarioDbId)3547:
						case (ScenarioDbId)3548:
						case (ScenarioDbId)3549:
						case (ScenarioDbId)3550:
						case (ScenarioDbId)3551:
						case (ScenarioDbId)3554:
						case (ScenarioDbId)3606:
						case (ScenarioDbId)3607:
						case (ScenarioDbId)3608:
						case ScenarioDbId.PRACTICE_EXPERT_DEMONHUNTER:
						case (ScenarioDbId)3610:
						case (ScenarioDbId)3612:
						case (ScenarioDbId)3613:
						case (ScenarioDbId)3621:
						case (ScenarioDbId)3622:
						case (ScenarioDbId)3623:
						case (ScenarioDbId)3625:
						case (ScenarioDbId)3626:
						case (ScenarioDbId)3627:
						case (ScenarioDbId)3628:
						case (ScenarioDbId)3629:
						case (ScenarioDbId)3630:
						case (ScenarioDbId)3631:
						case (ScenarioDbId)3632:
						case (ScenarioDbId)3633:
						case (ScenarioDbId)3634:
						case (ScenarioDbId)3635:
						case (ScenarioDbId)3636:
						case (ScenarioDbId)3637:
						case (ScenarioDbId)3638:
						case (ScenarioDbId)3639:
						case (ScenarioDbId)3640:
						case (ScenarioDbId)3641:
						case (ScenarioDbId)3642:
						case (ScenarioDbId)3643:
						case (ScenarioDbId)3644:
						case (ScenarioDbId)3645:
						case (ScenarioDbId)3646:
						case (ScenarioDbId)3647:
							goto IL_1D18;
						case ScenarioDbId.FB_RAGRAID:
							gameEntity = new FB_RagRaidScript();
							goto IL_1D1E;
						case ScenarioDbId.TB_MARTINAUTOBRAWL:
							gameEntity = new TB_MartinAutoBrawl();
							goto IL_1D1E;
						case ScenarioDbId.TB_BACONHAND_1P:
							gameEntity = new TB_BaconHand();
							goto IL_1D1E;
						case ScenarioDbId.ULDA_CITY:
						case ScenarioDbId.ULDA_DESERT:
						case ScenarioDbId.ULDA_TOMB:
						case ScenarioDbId.ULDA_HALLS:
						case ScenarioDbId.ULDA_SANCTUM:
						case ScenarioDbId.ULDA_CITY_HEROIC:
						case ScenarioDbId.ULDA_DESERT_HEROIC:
						case ScenarioDbId.ULDA_TOMB_HEROIC:
						case ScenarioDbId.ULDA_HALLS_HEROIC:
						case ScenarioDbId.ULDA_SANCTUM_HEROIC:
							gameEntity = ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						case ScenarioDbId.ULDA_TAVERN:
						case ScenarioDbId.ULDA_TAVERN_HEROIC:
							gameEntity = new ULDA_Tavern();
							goto IL_1D1E;
						case ScenarioDbId.TB_EVILBRM_1:
						case ScenarioDbId.TB_EVILBRM_2:
						case ScenarioDbId.TB_EVILBRM_DEBUG:
							gameEntity = new TB_EVILBRM();
							goto IL_1D1E;
						case ScenarioDbId.TB_LEAGUE_REVIVAL:
							gameEntity = new TB_LEAGUE_REVIVAL();
							goto IL_1D1E;
						case ScenarioDbId.PVPDR_Season_1:
							gameEntity = new WizardDuels();
							goto IL_1D1E;
						case ScenarioDbId.DRGA_Good_01:
						case ScenarioDbId.DRGA_Good_02:
						case ScenarioDbId.DRGA_Good_03:
						case ScenarioDbId.DRGA_Good_04:
						case ScenarioDbId.DRGA_Good_05:
						case ScenarioDbId.DRGA_Good_06:
						case ScenarioDbId.DRGA_Good_07:
						case ScenarioDbId.DRGA_Good_08:
						case ScenarioDbId.DRGA_Good_09:
						case ScenarioDbId.DRGA_Good_10:
						case ScenarioDbId.DRGA_Good_11:
						case ScenarioDbId.DRGA_Good_12:
						case ScenarioDbId.DRGA_Evil_01:
						case ScenarioDbId.DRGA_Evil_02:
						case ScenarioDbId.DRGA_Evil_03:
						case ScenarioDbId.DRGA_Evil_04:
						case ScenarioDbId.DRGA_Evil_05:
						case ScenarioDbId.DRGA_Evil_06:
						case ScenarioDbId.DRGA_Evil_07:
						case ScenarioDbId.DRGA_Evil_08:
						case ScenarioDbId.DRGA_Evil_09:
						case ScenarioDbId.DRGA_Evil_10:
						case ScenarioDbId.DRGA_Evil_11:
						case ScenarioDbId.DRGA_Evil_12:
						case ScenarioDbId.DRGA_Good_01_Heroic:
						case ScenarioDbId.DRGA_Good_02_Heroic:
						case ScenarioDbId.DRGA_Good_03_Heroic:
						case ScenarioDbId.DRGA_Good_04_Heroic:
						case ScenarioDbId.DRGA_Good_05_Heroic:
						case ScenarioDbId.DRGA_Good_06_Heroic:
						case ScenarioDbId.DRGA_Good_07_Heroic:
						case ScenarioDbId.DRGA_Good_08_Heroic:
						case ScenarioDbId.DRGA_Good_09_Heroic:
						case ScenarioDbId.DRGA_Good_10_Heroic:
						case ScenarioDbId.DRGA_Good_11_Heroic:
						case ScenarioDbId.DRGA_Good_12_Heroic:
						case ScenarioDbId.DRGA_Evil_01_Heroic:
						case ScenarioDbId.DRGA_Evil_02_Heroic:
						case ScenarioDbId.DRGA_Evil_03_Heroic:
						case ScenarioDbId.DRGA_Evil_04_Heroic:
						case ScenarioDbId.DRGA_Evil_05_Heroic:
						case ScenarioDbId.DRGA_Evil_06_Heroic:
						case ScenarioDbId.DRGA_Evil_07_Heroic:
						case ScenarioDbId.DRGA_Evil_08_Heroic:
						case ScenarioDbId.DRGA_Evil_09_Heroic:
						case ScenarioDbId.DRGA_Evil_10_Heroic:
						case ScenarioDbId.DRGA_Evil_11_Heroic:
						case ScenarioDbId.DRGA_Evil_12_Heroic:
							gameEntity = DRGA_Dungeon.InstantiateDRGADungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						case ScenarioDbId.TB_SEEDED_BRAWL:
							goto IL_1876;
						case ScenarioDbId.TB_CAROUSEL:
							break;
						case ScenarioDbId.TB_TEMPLEOUTRUN_1:
						case ScenarioDbId.TB_TEMPLEOUTRUN_2:
							gameEntity = ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						case ScenarioDbId.TB_BACONSHOP_Tutorial:
							gameEntity = new TB_BaconShop_Tutorial();
							goto IL_1D1E;
						case ScenarioDbId.ReturningPlayer_Challenge_1:
							gameEntity = new RP_Fight_01();
							goto IL_1D1E;
						case ScenarioDbId.ReturningPlayer_Challenge_2:
							gameEntity = new RP_Fight_02();
							goto IL_1D1E;
						case ScenarioDbId.ReturningPlayer_Challenge_3:
							gameEntity = new RP_Fight_03();
							goto IL_1D1E;
						case ScenarioDbId.TB_ROAD_TO_NR1:
						case ScenarioDbId.TB_ROAD_TO_NR2:
						case ScenarioDbId.TB_ROAD_TO_NR3:
						case ScenarioDbId.TB_ROAD_TO_NR4:
						case ScenarioDbId.TB_ROAD_TO_NR5:
						case ScenarioDbId.TB_ROAD_TO_NR6:
						case ScenarioDbId.TB_ROAD_TO_NR7:
						case ScenarioDbId.TB_ROAD_TO_NR8:
							gameEntity = new TB_RoadToNR();
							goto IL_1D1E;
						case ScenarioDbId.TB_ROAD_TO_NR_TAVERN:
							gameEntity = new TB_RoadToNR_Tavern();
							goto IL_1D1E;
						case ScenarioDbId.BTA_01_INQUISITOR_DAKREL:
						case ScenarioDbId.BTA_02_XUR_GOTH:
						case ScenarioDbId.BTA_03_ZIXOR:
						case ScenarioDbId.BTA_04_BALTHARAK:
						case ScenarioDbId.BTA_05_KANRETHAD_PRIME:
						case ScenarioDbId.BTA_06_BURGRAK_CRUELCHAIN:
						case ScenarioDbId.BTA_07_FELSTORM_RUN:
						case ScenarioDbId.BTA_08_MOTHER_SHAHRAZ:
						case ScenarioDbId.BTA_09_SHAL_JA_OUTCAST:
						case ScenarioDbId.BTA_10_KARNUK_OUTCAST:
						case ScenarioDbId.BTA_11_JEK_HAZ:
						case ScenarioDbId.BTA_12_MAGTHERIDON_PRIME:
						case ScenarioDbId.BTA_13_GOK_AMOK:
						case ScenarioDbId.BTA_14_FLIKK:
						case ScenarioDbId.BTA_15_BADUU_CORRUPTED:
						case ScenarioDbId.BTA_16_MECHA_JARAXXUS:
						case ScenarioDbId.BTA_17_ILLIDAN_STORMRAGE:
							gameEntity = BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						case ScenarioDbId.BTA_Heroic_KAZZAK:
						case ScenarioDbId.BTA_Heroic_GRUUL:
						case ScenarioDbId.BTA_Heroic_MAGTHERIDON:
						case ScenarioDbId.BTA_Heroic_SUPREMUS:
						case ScenarioDbId.BTA_Heroic_TERON_GOREFIEND:
						case ScenarioDbId.BTA_Heroic_MOTHER_SHARAZ:
						case ScenarioDbId.BTA_Heroic_LADY_VASHJ:
						case ScenarioDbId.BTA_Heroic_KAELTHAS:
						case ScenarioDbId.BTA_Heroic_ILLIDAN:
							gameEntity = BTA_Dungeon_Heroic.InstantiateBTADungeonMissionEntityForBoss(powerList, createGame);
							goto IL_1D1E;
						case ScenarioDbId.TB_SPT_DALA_1P:
						case ScenarioDbId.TB_SPT_DALA:
							gameEntity = new TB_SPT_DALA();
							goto IL_1D1E;
						case ScenarioDbId.BTP_01_AZZINOTH:
							gameEntity = new BTA_Prologue_Fight_01();
							goto IL_1D1E;
						case ScenarioDbId.BTP_02_XAVIUS:
							gameEntity = new BTA_Prologue_Fight_02();
							goto IL_1D1E;
						case ScenarioDbId.BTP_03_MANNOROTH:
							gameEntity = new BTA_Prologue_Fight_03();
							goto IL_1D1E;
						case ScenarioDbId.BTP_04_CENARIUS:
							gameEntity = new BTA_Prologue_Fight_04();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
					}
					gameEntity = new TB_Carousel();
					goto IL_1D1E;
				}
				if (missionId <= ScenarioDbId.BOH_VALEERA_08)
				{
					switch (missionId)
					{
					case ScenarioDbId.TB_RumbleDome:
					case ScenarioDbId.TB_Rumbledome_1p:
						gameEntity = new TB_RumbleDome();
						goto IL_1D1E;
					case (ScenarioDbId)3711:
					case (ScenarioDbId)3712:
						goto IL_1D18;
					case ScenarioDbId.TB_CRAZY_DECK_KEEP_WINNER:
						goto IL_1876;
					default:
						switch (missionId)
						{
						case ScenarioDbId.BOH_JAINA_01:
							gameEntity = new BoH_Jaina_01();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_02:
							gameEntity = new BoH_Jaina_02();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_03:
							gameEntity = new BoH_Jaina_03();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_04:
							gameEntity = new BoH_Jaina_04();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_05:
							gameEntity = new BoH_Jaina_05();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_06:
							gameEntity = new BoH_Jaina_06();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_07:
							gameEntity = new BoH_Jaina_07();
							goto IL_1D1E;
						case ScenarioDbId.BOH_JAINA_08:
							gameEntity = new BoH_Jaina_08();
							goto IL_1D1E;
						default:
							switch (missionId)
							{
							case ScenarioDbId.BOH_REXXAR_01:
								gameEntity = new BoH_Rexxar_01();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_02:
								gameEntity = new BoH_Rexxar_02();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_03:
								gameEntity = new BoH_Rexxar_03();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_04:
								gameEntity = new BoH_Rexxar_04();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_05:
								gameEntity = new BoH_Rexxar_05();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_06:
								gameEntity = new BoH_Rexxar_06();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_07:
								gameEntity = new BoH_Rexxar_07();
								goto IL_1D1E;
							case ScenarioDbId.BOH_REXXAR_08:
								gameEntity = new BoH_Rexxar_08();
								goto IL_1D1E;
							case (ScenarioDbId)3774:
							case (ScenarioDbId)3775:
							case (ScenarioDbId)3776:
							case (ScenarioDbId)3777:
							case (ScenarioDbId)3778:
							case (ScenarioDbId)3779:
							case (ScenarioDbId)3780:
							case (ScenarioDbId)3781:
							case (ScenarioDbId)3782:
							case (ScenarioDbId)3783:
							case (ScenarioDbId)3784:
							case (ScenarioDbId)3785:
							case (ScenarioDbId)3786:
							case (ScenarioDbId)3787:
							case (ScenarioDbId)3788:
							case (ScenarioDbId)3789:
							case (ScenarioDbId)3790:
							case (ScenarioDbId)3791:
							case (ScenarioDbId)3792:
							case (ScenarioDbId)3801:
							case (ScenarioDbId)3802:
							case (ScenarioDbId)3803:
							case (ScenarioDbId)3804:
							case (ScenarioDbId)3805:
							case (ScenarioDbId)3806:
							case (ScenarioDbId)3807:
							case (ScenarioDbId)3808:
							case (ScenarioDbId)3809:
							case (ScenarioDbId)3818:
							case (ScenarioDbId)3819:
							case (ScenarioDbId)3820:
							case (ScenarioDbId)3821:
							case (ScenarioDbId)3822:
							case (ScenarioDbId)3823:
							case (ScenarioDbId)3824:
							case (ScenarioDbId)3833:
							case (ScenarioDbId)3834:
							case (ScenarioDbId)3835:
							case (ScenarioDbId)3836:
							case (ScenarioDbId)3837:
							case (ScenarioDbId)3838:
							case (ScenarioDbId)3847:
							case (ScenarioDbId)3848:
							case (ScenarioDbId)3849:
							case (ScenarioDbId)3850:
								goto IL_1D18;
							case ScenarioDbId.BOH_GARROSH_01:
								gameEntity = new BoH_Garrosh_01();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_02:
								gameEntity = new BoH_Garrosh_02();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_03:
								gameEntity = new BoH_Garrosh_03();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_04:
								gameEntity = new BoH_Garrosh_04();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_05:
								gameEntity = new BoH_Garrosh_05();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_06:
								gameEntity = new BoH_Garrosh_06();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_07:
								gameEntity = new BoH_Garrosh_07();
								goto IL_1D1E;
							case ScenarioDbId.BOH_GARROSH_08:
								gameEntity = new BoH_Garrosh_08();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_01:
								gameEntity = new BoH_Uther_01();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_02:
								gameEntity = new BoH_Uther_02();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_03:
								gameEntity = new BoH_Uther_03();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_04:
								gameEntity = new BoH_Uther_04();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_05:
								gameEntity = new BoH_Uther_05();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_06:
								gameEntity = new BoH_Uther_06();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_07:
								gameEntity = new BoH_Uther_07();
								goto IL_1D1E;
							case ScenarioDbId.BOH_UTHER_08:
								gameEntity = new BoH_Uther_08();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_01:
								gameEntity = new BoH_Anduin_01();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_02:
								gameEntity = new BoH_Anduin_02();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_03:
								gameEntity = new BoH_Anduin_03();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_04:
								gameEntity = new BoH_Anduin_04();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_05:
								gameEntity = new BoH_Anduin_05();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_06:
								gameEntity = new BoH_Anduin_06();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_07:
								gameEntity = new BoH_Anduin_07();
								goto IL_1D1E;
							case ScenarioDbId.BOH_ANDUIN_08:
								gameEntity = new BoH_Anduin_08();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_01:
								gameEntity = new BOM_01_Rokara_01();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_02:
								gameEntity = new BOM_01_Rokara_02();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_03:
								gameEntity = new BOM_01_Rokara_03();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_04:
								gameEntity = new BOM_01_Rokara_04();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_05:
								gameEntity = new BOM_01_Rokara_05();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_06:
								gameEntity = new BOM_01_Rokara_06();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_07:
								gameEntity = new BOM_01_Rokara_07();
								goto IL_1D1E;
							case ScenarioDbId.BOM_01_Rokara_08:
								gameEntity = new BOM_01_Rokara_08();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_01:
								gameEntity = new BoH_Valeera_01();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_02:
								gameEntity = new BoH_Valeera_02();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_03:
								gameEntity = new BoH_Valeera_03();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_04:
								gameEntity = new BoH_Valeera_04();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_05:
								gameEntity = new BoH_Valeera_05();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_06:
								gameEntity = new BoH_Valeera_06();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_07:
								gameEntity = new BoH_Valeera_07();
								goto IL_1D1E;
							case ScenarioDbId.BOH_VALEERA_08:
								gameEntity = new BoH_Valeera_08();
								goto IL_1D1E;
							default:
								goto IL_1D18;
							}
							break;
						}
						break;
					}
				}
				else if (missionId <= ScenarioDbId.BOH_MALFURION_08)
				{
					switch (missionId)
					{
					case ScenarioDbId.BOH_THRALL_01:
						gameEntity = new BoH_Thrall_01();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_02:
						gameEntity = new BoH_Thrall_02();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_03:
						gameEntity = new BoH_Thrall_03();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_04:
						gameEntity = new BoH_Thrall_04();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_05:
						gameEntity = new BoH_Thrall_05();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_06:
						gameEntity = new BoH_Thrall_06();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_07:
						gameEntity = new BoH_Thrall_07();
						goto IL_1D1E;
					case ScenarioDbId.BOH_THRALL_08:
						gameEntity = new BoH_Thrall_08();
						goto IL_1D1E;
					default:
						switch (missionId)
						{
						case ScenarioDbId.BOH_MALFURION_01:
							gameEntity = new BoH_Malfurion_01();
							goto IL_1D1E;
						case (ScenarioDbId)3924:
						case (ScenarioDbId)3930:
						case (ScenarioDbId)3931:
							goto IL_1D18;
						case ScenarioDbId.BOH_MALFURION_02:
							gameEntity = new BoH_Malfurion_02();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_03:
							gameEntity = new BoH_Malfurion_03();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_04:
							gameEntity = new BoH_Malfurion_04();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_05:
							gameEntity = new BoH_Malfurion_05();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_06:
							gameEntity = new BoH_Malfurion_06();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_07:
							gameEntity = new BoH_Malfurion_07();
							goto IL_1D1E;
						case ScenarioDbId.BOH_MALFURION_08:
							gameEntity = new BoH_Malfurion_08();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
						break;
					}
				}
				else
				{
					switch (missionId)
					{
					case ScenarioDbId.BOM_02_Xyrella_01:
						gameEntity = new BOM_02_Xyrella_Fight_01();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_02:
						gameEntity = new BOM_02_Xyrella_Fight_02();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_03:
						gameEntity = new BOM_02_Xyrella_Fight_03();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_04:
						gameEntity = new BOM_02_Xyrella_Fight_04();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_05:
						gameEntity = new BOM_02_Xyrella_Fight_05();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_06:
						gameEntity = new BOM_02_Xyrella_Fight_06();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_07:
						gameEntity = new BOM_02_Xyrella_Fight_07();
						goto IL_1D1E;
					case ScenarioDbId.BOM_02_Xyrella_08:
						gameEntity = new BOM_02_Xyrella_Fight_08();
						goto IL_1D1E;
					default:
						switch (missionId)
						{
						case ScenarioDbId.BOM_03_Guff_01:
							gameEntity = new BOM_03_Guff_Fight_01();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_02:
							gameEntity = new BOM_03_Guff_Fight_02();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_03:
							gameEntity = new BOM_03_Guff_Fight_03();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_04:
							gameEntity = new BOM_03_Guff_Fight_04();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_05:
							gameEntity = new BOM_03_Guff_Fight_05();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_06:
							gameEntity = new BOM_03_Guff_Fight_06();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_07:
							gameEntity = new BOM_03_Guff_Fight_07();
							goto IL_1D1E;
						case ScenarioDbId.BOM_03_Guff_08:
							gameEntity = new BOM_03_Guff_Fight_08();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
						break;
					}
				}
			}
			IL_1787:
			gameEntity = new DALA_Tavern();
			goto IL_1D1E;
			IL_1876:
			gameEntity = new TB_RandomDeckKeepWinnerDeck();
			goto IL_1D1E;
			IL_177A:
			gameEntity = DALA_Dungeon.InstantiateDALADungeonMissionEntityForBoss(powerList, createGame);
			goto IL_1D1E;
			IL_17F2:
			gameEntity = new FB_Champs();
			goto IL_1D1E;
			IL_1829:
			gameEntity = new FB_TokiCoop();
			goto IL_1D1E;
		}
		if (missionId <= ScenarioDbId.TB_KELTHUZADRAFAAM_1P)
		{
			if (missionId > ScenarioDbId.LOE_ZINAAR)
			{
				if (missionId > ScenarioDbId.LOE_STEEL_SENTINEL)
				{
					if (missionId <= ScenarioDbId.LOE_CHALLENGE_WARLOCK_V_SUN_RAIDER)
					{
						if (missionId != ScenarioDbId.LOE_SLITHERSPEAR)
						{
							if (missionId == ScenarioDbId.LOE_CHALLENGE_PALADIN_V_ARCHAEDUS)
							{
								goto IL_149C;
							}
							if (missionId != ScenarioDbId.LOE_CHALLENGE_WARLOCK_V_SUN_RAIDER)
							{
								goto IL_1D18;
							}
							goto IL_1486;
						}
					}
					else
					{
						switch (missionId)
						{
						case ScenarioDbId.LOE_CHALLENGE_DRUID_V_SCARVASH:
						case ScenarioDbId.LOE_HEROIC_SCARVASH:
							goto IL_1491;
						case ScenarioDbId.LOE_CHALLENGE_MAGE_V_SENTINEL:
						case ScenarioDbId.LOE_HEROIC_STEEL_SENTINEL:
							goto IL_14A7;
						case (ScenarioDbId)1626:
						case (ScenarioDbId)1627:
						case (ScenarioDbId)1629:
						case (ScenarioDbId)1630:
						case (ScenarioDbId)1631:
						case (ScenarioDbId)1632:
						case (ScenarioDbId)1634:
						case (ScenarioDbId)1635:
						case (ScenarioDbId)1636:
						case (ScenarioDbId)1639:
						case (ScenarioDbId)1640:
						case (ScenarioDbId)1641:
						case (ScenarioDbId)1642:
						case (ScenarioDbId)1643:
						case (ScenarioDbId)1644:
						case (ScenarioDbId)1647:
						case (ScenarioDbId)1648:
						case (ScenarioDbId)1649:
						case (ScenarioDbId)1652:
						case (ScenarioDbId)1655:
						case (ScenarioDbId)1662:
						case (ScenarioDbId)1663:
						case (ScenarioDbId)1664:
						case (ScenarioDbId)1665:
						case (ScenarioDbId)1667:
						case (ScenarioDbId)1677:
						case (ScenarioDbId)1678:
							goto IL_1D18;
						case ScenarioDbId.LOE_CHALLENGE_ROGUE_V_SKELESAURUS:
						case ScenarioDbId.LOE_HEROIC_SKELESAURUS:
							goto IL_14E9;
						case ScenarioDbId.TB_CO_OP_1P_TEST:
						case ScenarioDbId.TB_CO_OP_V2:
							goto IL_13CB;
						case ScenarioDbId.LOE_CHALLENGE_SHAMAN_V_GIANTFIN:
						case ScenarioDbId.LOE_HEROIC_GIANTFIN:
							goto IL_1465;
						case ScenarioDbId.LOE_CHALLENGE_WARRIOR_V_ZINAAR:
						case ScenarioDbId.LOE_HEROIC_ZINAAR:
							goto IL_147B;
						case ScenarioDbId.LOE_CHALLENGE_HUNTER_V_SLITHERSPEAR:
						case ScenarioDbId.LOE_HEROIC_SLITHERSPEAR:
							break;
						case ScenarioDbId.LOE_CHALLENGE_PRIEST_V_NAZJAR:
						case ScenarioDbId.LOE_HEROIC_LADY_NAZJAR:
							goto IL_14C8;
						case ScenarioDbId.LOE_HEROIC_SUN_RAIDER_PHAERIX:
							goto IL_1486;
						case ScenarioDbId.LOE_HEROIC_TEMPLE_ESCAPE:
							goto IL_14B2;
						case ScenarioDbId.TB_DECKBUILDING_1P_TEST:
							goto IL_13E1;
						case ScenarioDbId.TB_GIFTEXCHANGE_1P_TEST:
						case ScenarioDbId.TB_GIFTEXCHANGE:
							gameEntity = new TB05_GiftExchange();
							goto IL_1D1E;
						case ScenarioDbId.LOE_HEROIC_ARCHAEDAS:
							goto IL_149C;
						case ScenarioDbId.TB_CHOOSEFATEBUILD_1P_TEST:
						case ScenarioDbId.TB_CHOOSEFATEBUILD:
							gameEntity = new TB_ChooseYourFateBuildaround();
							goto IL_1D1E;
						case ScenarioDbId.LOE_HEROIC_MINE_CART:
							goto IL_14BD;
						case ScenarioDbId.LOE_HEROIC_RAFAAM_1:
							goto IL_14D3;
						case ScenarioDbId.LOE_HEROIC_RAFAAM_2:
							goto IL_14DE;
						case ScenarioDbId.TB_CHOOSEFATERANDOM_1P_TEST:
						case ScenarioDbId.TB_CHOOSEFATERANDOM:
							gameEntity = new TB_ChooseYourFateRandom();
							goto IL_1D1E;
						default:
							if (missionId != ScenarioDbId.TB_KELTHUZADRAFAAM && missionId != ScenarioDbId.TB_KELTHUZADRAFAAM_1P)
							{
								goto IL_1D18;
							}
							gameEntity = new TB_KelthuzadRafaam();
							goto IL_1D1E;
						}
					}
					gameEntity = new LOE09_LordSlitherspear();
					goto IL_1D1E;
				}
				if (missionId <= ScenarioDbId.LOE_LADY_NAZJAR)
				{
					switch (missionId)
					{
					case ScenarioDbId.LOE_SCARVASH:
						break;
					case ScenarioDbId.LOE_TEMPLE_ESCAPE:
						goto IL_14B2;
					case ScenarioDbId.LOE_MINE_CART:
						goto IL_14BD;
					default:
						if (missionId == ScenarioDbId.LOE_ARCHAEDAS)
						{
							goto IL_149C;
						}
						if (missionId != ScenarioDbId.LOE_LADY_NAZJAR)
						{
							goto IL_1D18;
						}
						goto IL_14C8;
					}
				}
				else
				{
					switch (missionId)
					{
					case ScenarioDbId.LOE_SKELESAURUS:
						goto IL_14E9;
					case (ScenarioDbId)1144:
					case (ScenarioDbId)1145:
						goto IL_1D18;
					case ScenarioDbId.LOE_RAFAAM_1:
						goto IL_14D3;
					case ScenarioDbId.TB_CO_OP_PRECON:
						goto IL_13CB;
					default:
						if (missionId == ScenarioDbId.LOE_RAFAAM_2)
						{
							goto IL_14DE;
						}
						if (missionId != ScenarioDbId.LOE_STEEL_SENTINEL)
						{
							goto IL_1D18;
						}
						goto IL_14A7;
					}
				}
				IL_1491:
				gameEntity = new LOE04_Scarvash();
				goto IL_1D1E;
				IL_149C:
				gameEntity = new LOE08_Archaedas();
				goto IL_1D1E;
				IL_14A7:
				gameEntity = new LOE14_Steel_Sentinel();
				goto IL_1D1E;
				IL_14B2:
				gameEntity = new LOE03_AncientTemple();
				goto IL_1D1E;
				IL_14BD:
				gameEntity = new LOE07_MineCart();
				goto IL_1D1E;
				IL_14C8:
				gameEntity = new LOE12_Naga();
				goto IL_1D1E;
				IL_14D3:
				gameEntity = new LOE15_Boss1();
				goto IL_1D1E;
				IL_14DE:
				gameEntity = new LOE16_Boss2();
				goto IL_1D1E;
				IL_14E9:
				gameEntity = new LOE13_Skelesaurus();
				goto IL_1D1E;
			}
			if (missionId <= ScenarioDbId.TB_DECKBUILDING)
			{
				if (missionId <= ScenarioDbId.TUTORIAL_MUKLA)
				{
					if (missionId == ScenarioDbId.TUTORIAL_HOGGER)
					{
						gameEntity = new Tutorial_01();
						goto IL_1D1E;
					}
					if (missionId == ScenarioDbId.TUTORIAL_MILLHOUSE)
					{
						gameEntity = new Tutorial_02();
						goto IL_1D1E;
					}
					if (missionId != ScenarioDbId.TUTORIAL_MUKLA)
					{
						goto IL_1D18;
					}
					gameEntity = new Tutorial_03();
					goto IL_1D1E;
				}
				else
				{
					switch (missionId)
					{
					case ScenarioDbId.TUTORIAL_NESINGWARY:
						gameEntity = new Tutorial_04();
						goto IL_1D1E;
					case (ScenarioDbId)202:
					case (ScenarioDbId)203:
					case (ScenarioDbId)204:
					case (ScenarioDbId)205:
					case (ScenarioDbId)206:
					case (ScenarioDbId)207:
					case (ScenarioDbId)208:
					case (ScenarioDbId)209:
					case (ScenarioDbId)210:
					case (ScenarioDbId)211:
					case (ScenarioDbId)212:
					case (ScenarioDbId)213:
					case (ScenarioDbId)214:
					case (ScenarioDbId)215:
					case (ScenarioDbId)216:
					case (ScenarioDbId)217:
					case (ScenarioDbId)218:
					case (ScenarioDbId)219:
					case (ScenarioDbId)220:
					case (ScenarioDbId)221:
					case (ScenarioDbId)222:
					case (ScenarioDbId)223:
					case (ScenarioDbId)224:
					case (ScenarioDbId)225:
					case (ScenarioDbId)226:
					case (ScenarioDbId)227:
					case (ScenarioDbId)228:
					case (ScenarioDbId)229:
					case (ScenarioDbId)230:
					case (ScenarioDbId)231:
					case (ScenarioDbId)232:
					case (ScenarioDbId)233:
					case (ScenarioDbId)234:
					case (ScenarioDbId)235:
					case (ScenarioDbId)236:
					case (ScenarioDbId)237:
					case (ScenarioDbId)238:
					case (ScenarioDbId)239:
					case (ScenarioDbId)240:
					case (ScenarioDbId)241:
					case (ScenarioDbId)242:
					case (ScenarioDbId)243:
					case (ScenarioDbId)244:
					case (ScenarioDbId)245:
					case (ScenarioDbId)246:
					case (ScenarioDbId)247:
					case (ScenarioDbId)250:
					case (ScenarioDbId)251:
					case (ScenarioDbId)252:
					case (ScenarioDbId)253:
					case (ScenarioDbId)254:
					case (ScenarioDbId)255:
					case (ScenarioDbId)256:
					case (ScenarioDbId)257:
					case (ScenarioDbId)258:
					case (ScenarioDbId)259:
					case ScenarioDbId.PRACTICE_EXPERT_MAGE:
					case (ScenarioDbId)261:
					case (ScenarioDbId)262:
					case (ScenarioDbId)263:
					case ScenarioDbId.PRACTICE_EXPERT_WARRIOR:
					case ScenarioDbId.PRACTICE_EXPERT_PRIEST:
					case ScenarioDbId.PRACTICE_EXPERT_WARLOCK:
					case ScenarioDbId.PRACTICE_EXPERT_DRUID:
					case ScenarioDbId.PRACTICE_EXPERT_ROGUE:
					case ScenarioDbId.PRACTICE_EXPERT_HUNTER:
					case ScenarioDbId.PRACTICE_EXPERT_PALADIN:
					case ScenarioDbId.PRACTICE_EXPERT_SHAMAN:
					case (ScenarioDbId)272:
					case (ScenarioDbId)289:
					case (ScenarioDbId)290:
					case (ScenarioDbId)291:
					case (ScenarioDbId)315:
					case (ScenarioDbId)316:
					case (ScenarioDbId)317:
					case (ScenarioDbId)318:
					case (ScenarioDbId)342:
						goto IL_1D18;
					case ScenarioDbId.TUTORIAL_ILLIDAN:
						gameEntity = new Tutorial_05();
						goto IL_1D1E;
					case ScenarioDbId.TUTORIAL_CHO:
						gameEntity = new Tutorial_06();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_ANUBREKHAN:
					case ScenarioDbId.NAXX_HEROIC_ANUBREKHAN:
						gameEntity = new NAX01_AnubRekhan();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_FAERLINA:
					case ScenarioDbId.NAXX_CHALLENGE_DRUID_V_FAERLINA:
					case ScenarioDbId.NAXX_HEROIC_FAERLINA:
						gameEntity = new NAX02_Faerlina();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_NOTH:
					case ScenarioDbId.NAXX_HEROIC_NOTH:
						gameEntity = new NAX04_Noth();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_HEIGAN:
					case ScenarioDbId.NAXX_CHALLENGE_MAGE_V_HEIGAN:
					case ScenarioDbId.NAXX_HEROIC_HEIGAN:
						gameEntity = new NAX05_Heigan();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_LOATHEB:
					case ScenarioDbId.NAXX_CHALLENGE_HUNTER_V_LOATHEB:
					case ScenarioDbId.NAXX_HEROIC_LOATHEB:
						gameEntity = new NAX06_Loatheb();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_MAEXXNA:
					case ScenarioDbId.NAXX_CHALLENGE_ROGUE_V_MAEXXNA:
					case ScenarioDbId.NAXX_HEROIC_MAEXXNA:
						gameEntity = new NAX03_Maexxna();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_RAZUVIOUS:
					case ScenarioDbId.NAXX_HEROIC_RAZUVIOUS:
						gameEntity = new NAX07_Razuvious();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_GOTHIK:
					case ScenarioDbId.NAXX_CHALLENGE_SHAMAN_V_GOTHIK:
					case ScenarioDbId.NAXX_HEROIC_GOTHIK:
						gameEntity = new NAX08_Gothik();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_HORSEMEN:
					case ScenarioDbId.NAXX_CHALLENGE_WARLOCK_V_HORSEMEN:
					case ScenarioDbId.NAXX_HEROIC_HORSEMEN:
						gameEntity = new NAX09_Horsemen();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_PATCHWERK:
					case ScenarioDbId.NAXX_HEROIC_PATCHWERK:
						gameEntity = new NAX10_Patchwerk();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_GROBBULUS:
					case ScenarioDbId.NAXX_CHALLENGE_WARRIOR_V_GROBBULUS:
					case ScenarioDbId.NAXX_HEROIC_GROBBULUS:
						gameEntity = new NAX11_Grobbulus();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_GLUTH:
					case ScenarioDbId.NAXX_HEROIC_GLUTH:
						gameEntity = new NAX12_Gluth();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_THADDIUS:
					case ScenarioDbId.NAXX_CHALLENGE_PRIEST_V_THADDIUS:
					case ScenarioDbId.NAXX_HEROIC_THADDIUS:
						gameEntity = new NAX13_Thaddius();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_KELTHUZAD:
					case ScenarioDbId.NAXX_CHALLENGE_PALADIN_V_KELTHUZAD:
					case ScenarioDbId.NAXX_HEROIC_KELTHUZAD:
						gameEntity = new NAX15_KelThuzad();
						goto IL_1D1E;
					case ScenarioDbId.NAXX_SAPPHIRON:
					case ScenarioDbId.NAXX_HEROIC_SAPPHIRON:
						gameEntity = new NAX14_Sapphiron();
						goto IL_1D1E;
					case ScenarioDbId.BRM_GRIM_GUZZLER:
					case ScenarioDbId.BRM_HEROIC_GRIM_GUZZLER:
					case ScenarioDbId.BRM_CHALLENGE_HUNTER_V_GUZZLER:
						gameEntity = new BRM01_GrimGuzzler();
						goto IL_1D1E;
					case ScenarioDbId.BRM_DARK_IRON_ARENA:
					case ScenarioDbId.BRM_HEROIC_DARK_IRON_ARENA:
					case ScenarioDbId.BRM_CHALLENGE_MAGE_V_DARK_IRON_ARENA:
						gameEntity = new BRM02_DarkIronArena();
						goto IL_1D1E;
					case ScenarioDbId.BRM_THAURISSAN:
					case ScenarioDbId.BRM_HEROIC_THAURISSAN:
						gameEntity = new BRM03_Thaurissan();
						goto IL_1D1E;
					case ScenarioDbId.BRM_GARR:
					case ScenarioDbId.BRM_HEROIC_GARR:
					case ScenarioDbId.BRM_CHALLENGE_WARRIOR_V_GARR:
						gameEntity = new BRM04_Garr();
						goto IL_1D1E;
					case ScenarioDbId.BRM_BARON_GEDDON:
					case ScenarioDbId.BRM_HEROIC_BARON_GEDDON:
					case ScenarioDbId.BRM_CHALLENGE_SHAMAN_V_GEDDON:
						gameEntity = new BRM05_BaronGeddon();
						goto IL_1D1E;
					case ScenarioDbId.BRM_MAJORDOMO:
					case ScenarioDbId.BRM_HEROIC_MAJORDOMO:
						gameEntity = new BRM06_Majordomo();
						goto IL_1D1E;
					case ScenarioDbId.BRM_OMOKK:
					case ScenarioDbId.BRM_HEROIC_OMOKK:
						gameEntity = new BRM07_Omokk();
						goto IL_1D1E;
					case ScenarioDbId.BRM_DRAKKISATH:
					case ScenarioDbId.BRM_CHALLENGE_PRIEST_V_DRAKKISATH:
					case ScenarioDbId.BRM_HEROIC_DRAKKISATH:
						gameEntity = new BRM08_Drakkisath();
						goto IL_1D1E;
					case ScenarioDbId.BRM_REND_BLACKHAND:
					case ScenarioDbId.BRM_CHALLENGE_DRUID_V_BLACKHAND:
					case ScenarioDbId.BRM_HEROIC_REND_BLACKHAND:
						gameEntity = new BRM09_RendBlackhand();
						goto IL_1D1E;
					case ScenarioDbId.BRM_RAZORGORE:
					case ScenarioDbId.BRM_HEROIC_RAZORGORE:
					case ScenarioDbId.BRM_CHALLENGE_WARLOCK_V_RAZORGORE:
						gameEntity = new BRM10_Razorgore();
						goto IL_1D1E;
					case ScenarioDbId.BRM_VAELASTRASZ:
					case ScenarioDbId.BRM_HEROIC_VAELASTRASZ:
					case ScenarioDbId.BRM_CHALLENGE_ROGUE_V_VAELASTRASZ:
						gameEntity = new BRM11_Vaelastrasz();
						goto IL_1D1E;
					case ScenarioDbId.BRM_CHROMAGGUS:
					case ScenarioDbId.BRM_HEROIC_CHROMAGGUS:
						gameEntity = new BRM12_Chromaggus();
						goto IL_1D1E;
					case ScenarioDbId.BRM_NEFARIAN:
					case ScenarioDbId.BRM_HEROIC_NEFARIAN:
						gameEntity = new BRM13_Nefarian();
						goto IL_1D1E;
					case ScenarioDbId.BRM_OMNOTRON:
					case ScenarioDbId.BRM_HEROIC_OMNOTRON:
					case ScenarioDbId.BRM_CHALLENGE_PALADIN_V_OMNOTRON:
						gameEntity = new BRM14_Omnotron();
						goto IL_1D1E;
					case ScenarioDbId.BRM_MALORIAK:
					case ScenarioDbId.BRM_HEROIC_MALORIAK:
						gameEntity = new BRM15_Maloriak();
						goto IL_1D1E;
					case ScenarioDbId.BRM_ATRAMEDES:
					case ScenarioDbId.BRM_HEROIC_ATRAMEDES:
						gameEntity = new BRM16_Atramedes();
						goto IL_1D1E;
					case ScenarioDbId.BRM_ZOMBIE_NEF:
					case ScenarioDbId.BRM_HEROIC_ZOMBIE_NEF:
						gameEntity = new BRM17_ZombieNef();
						goto IL_1D1E;
					default:
						if (missionId == ScenarioDbId.TB_RAG_V_NEF)
						{
							gameEntity = new TB01_RagVsNef();
							goto IL_1D1E;
						}
						if (missionId != ScenarioDbId.TB_DECKBUILDING)
						{
							goto IL_1D18;
						}
						goto IL_13E1;
					}
				}
			}
			else if (missionId <= ScenarioDbId.TB_CO_OP_TEST2)
			{
				if (missionId != ScenarioDbId.TB_CO_OP_TEST && missionId != ScenarioDbId.TB_CO_OP && missionId != ScenarioDbId.TB_CO_OP_TEST2)
				{
					goto IL_1D18;
				}
			}
			else
			{
				if (missionId == ScenarioDbId.LOE_GIANTFIN)
				{
					goto IL_1465;
				}
				if (missionId == ScenarioDbId.LOE_SUN_RAIDER_PHAERIX)
				{
					goto IL_1486;
				}
				if (missionId != ScenarioDbId.LOE_ZINAAR)
				{
					goto IL_1D18;
				}
				goto IL_147B;
			}
			IL_13CB:
			gameEntity = new TB02_CoOp();
			goto IL_1D1E;
			IL_13E1:
			gameEntity = new TB04_DeckBuilding();
			goto IL_1D1E;
			IL_1465:
			gameEntity = new LOE10_Giantfin();
			goto IL_1D1E;
			IL_147B:
			gameEntity = new LOE01_Zinaar();
			goto IL_1D1E;
			IL_1486:
			gameEntity = new LOE02_Sun_Raider_Phaerix();
			goto IL_1D1E;
		}
		if (missionId <= ScenarioDbId.TB_BLIZZCON_2016)
		{
			if (missionId <= ScenarioDbId.TB_COOPV3)
			{
				if (missionId > ScenarioDbId.KAR_PORTALS)
				{
					if (missionId != ScenarioDbId.TB_SHADOWTOWERS_1P_TEST && missionId != ScenarioDbId.TB_SHADOWTOWERS)
					{
						switch (missionId)
						{
						case ScenarioDbId.TB_SHADOWTOWERS_TEST:
							break;
						case (ScenarioDbId)1814:
						case (ScenarioDbId)1821:
						case (ScenarioDbId)1822:
						case (ScenarioDbId)1823:
						case (ScenarioDbId)1824:
						case (ScenarioDbId)1825:
						case (ScenarioDbId)1826:
						case (ScenarioDbId)1827:
						case (ScenarioDbId)1828:
						case (ScenarioDbId)1829:
						case (ScenarioDbId)1830:
						case (ScenarioDbId)1838:
						case (ScenarioDbId)1839:
						case (ScenarioDbId)1840:
						case (ScenarioDbId)1841:
						case (ScenarioDbId)1842:
						case (ScenarioDbId)1843:
						case (ScenarioDbId)1845:
						case (ScenarioDbId)1846:
						case (ScenarioDbId)1847:
						case (ScenarioDbId)1848:
						case (ScenarioDbId)1849:
						case (ScenarioDbId)1851:
						case (ScenarioDbId)1858:
						case (ScenarioDbId)1859:
						case (ScenarioDbId)1864:
							goto IL_1D18;
						case ScenarioDbId.KAR_HEROIC_NETHERSPITE:
						case ScenarioDbId.KAR_CHALLENGE_DRUID_V_NETHERSPITE:
							goto IL_1578;
						case ScenarioDbId.KAR_HEROIC_ARAN:
						case ScenarioDbId.KAR_CHALLENGE_ROGUE_V_ARAN:
							goto IL_156D;
						case ScenarioDbId.KAR_HEROIC_PROLOGUE:
							goto IL_14FF;
						case ScenarioDbId.KAR_HEROIC_PANTRY:
						case ScenarioDbId.KAR_CHALLENGE_PRIEST_V_PANTRY:
							goto IL_150A;
						case ScenarioDbId.KAR_HEROIC_MIRROR:
						case ScenarioDbId.KAR_CHALLENGE_SHAMAN_V_MIRROR:
							goto IL_1515;
						case ScenarioDbId.KAR_HEROIC_CHESS:
							goto IL_1520;
						case ScenarioDbId.KAR_HEROIC_JULIANNE:
						case ScenarioDbId.KAR_CHALLENGE_WARLOCK_V_JULIANNE:
							goto IL_152B;
						case ScenarioDbId.KAR_HEROIC_WOLF:
						case ScenarioDbId.KAR_CHALLENGE_PALADIN_V_WOLF:
							goto IL_1536;
						case ScenarioDbId.KAR_HEROIC_CRONE:
							goto IL_1541;
						case ScenarioDbId.KAR_HEROIC_CURATOR:
						case ScenarioDbId.KAR_CHALLENGE_HUNTER_V_CURATOR:
							goto IL_154C;
						case ScenarioDbId.KAR_HEROIC_NIGHTBANE:
						case ScenarioDbId.KAR_CHALLENGE_MAGE_V_NIGHTBANE:
							goto IL_1557;
						case ScenarioDbId.KAR_HEROIC_ILLHOOF:
						case ScenarioDbId.KAR_CHALLENGE_WARRIOR_V_ILLHOOF:
							goto IL_1562;
						case ScenarioDbId.KAR_HEROIC_PORTALS:
							goto IL_1583;
						case ScenarioDbId.TB_COOPV3_1P_TEST:
						case ScenarioDbId.TB_COOPV3:
							goto IL_13D6;
						case ScenarioDbId.TB_DECKRECIPE_1P_TEST:
						case ScenarioDbId.TB_DECKRECIPE:
							gameEntity = new TB10_DeckRecipe();
							goto IL_1D1E;
						default:
							goto IL_1D18;
						}
					}
					gameEntity = new TB09_ShadowTowers();
					goto IL_1D1E;
				}
				if (missionId == ScenarioDbId.KAR_CRONE)
				{
					goto IL_1541;
				}
				if (missionId == ScenarioDbId.KAR_WOLF)
				{
					goto IL_1536;
				}
				switch (missionId)
				{
				case ScenarioDbId.KAR_CHESS:
					goto IL_1520;
				case (ScenarioDbId)1750:
				case (ScenarioDbId)1751:
				case ScenarioDbId.KAR_CHESS_2P:
				case (ScenarioDbId)1754:
				case (ScenarioDbId)1757:
				case (ScenarioDbId)1762:
				case (ScenarioDbId)1763:
				case (ScenarioDbId)1764:
					goto IL_1D18;
				case ScenarioDbId.KAR_JULIANNE:
					goto IL_152B;
				case ScenarioDbId.KAR_MIRROR:
					goto IL_1515;
				case ScenarioDbId.KAR_CURATOR:
					goto IL_154C;
				case ScenarioDbId.KAR_ILLHOOF:
					goto IL_1562;
				case ScenarioDbId.KAR_NIGHTBANE:
					goto IL_1557;
				case ScenarioDbId.KAR_ARAN:
					goto IL_156D;
				case ScenarioDbId.KAR_NETHERSPITE:
					goto IL_1578;
				case ScenarioDbId.KAR_PANTRY:
					goto IL_150A;
				case ScenarioDbId.KAR_PROLOGUE:
					break;
				case ScenarioDbId.KAR_PORTALS:
					goto IL_1583;
				default:
					goto IL_1D18;
				}
				IL_14FF:
				gameEntity = new KAR00_Prologue();
				goto IL_1D1E;
				IL_150A:
				gameEntity = new KAR01_Pantry();
				goto IL_1D1E;
				IL_1515:
				gameEntity = new KAR02_Mirror();
				goto IL_1D1E;
				IL_1520:
				gameEntity = new KAR03_Chess();
				goto IL_1D1E;
				IL_152B:
				gameEntity = new KAR04_Julianne();
				goto IL_1D1E;
				IL_1536:
				gameEntity = new KAR05_Wolf();
				goto IL_1D1E;
				IL_1541:
				gameEntity = new KAR06_Crone();
				goto IL_1D1E;
				IL_154C:
				gameEntity = new KAR07_Curator();
				goto IL_1D1E;
				IL_1557:
				gameEntity = new KAR08_Nightbane();
				goto IL_1D1E;
				IL_1562:
				gameEntity = new KAR09_Illhoof();
				goto IL_1D1E;
				IL_156D:
				gameEntity = new KAR10_Aran();
				goto IL_1D1E;
				IL_1578:
				gameEntity = new KAR11_Netherspite();
				goto IL_1D1E;
				IL_1583:
				gameEntity = new KAR12_Portals();
				goto IL_1D1E;
			}
			if (missionId <= ScenarioDbId.TB_COOPV3_Score)
			{
				if (missionId - ScenarioDbId.TB_KARAPORTALS_1P_TEST <= 1)
				{
					gameEntity = new TB12_PartyPortals();
					goto IL_1D1E;
				}
				if (missionId != ScenarioDbId.TB_COOPV3_Score_1P_TEST && missionId != ScenarioDbId.TB_COOPV3_Score)
				{
					goto IL_1D18;
				}
			}
			else
			{
				if (missionId == ScenarioDbId.TB_JUGGERNAUT)
				{
					gameEntity = new TB_Juggernaut();
					goto IL_1D1E;
				}
				if (missionId == ScenarioDbId.TB_BATTLEROYALE_1P_TEST)
				{
					goto IL_145A;
				}
				if (missionId - ScenarioDbId.TB_BLIZZCON_2016_1P > 1)
				{
					goto IL_1D18;
				}
				gameEntity = new TB_Blizzcon_2016();
				goto IL_1D1E;
			}
			IL_13D6:
			gameEntity = new TB11_CoOpv3();
			goto IL_1D1E;
		}
		if (missionId > ScenarioDbId.TB_100TH)
		{
			if (missionId <= ScenarioDbId.TB_FIREFEST_1P)
			{
				if (missionId == ScenarioDbId.ICC_10_DEATHWHISPER)
				{
					gameEntity = new ICC_10_Deathwhisper();
					goto IL_1D1E;
				}
				if (missionId == ScenarioDbId.TB_100TH_1P)
				{
					goto IL_15BA;
				}
				if (missionId != ScenarioDbId.TB_FIREFEST_1P)
				{
					goto IL_1D18;
				}
			}
			else if (missionId <= ScenarioDbId.TB_LK_RAID)
			{
				switch (missionId)
				{
				case ScenarioDbId.TB_FIREFEST:
					break;
				case ScenarioDbId.TB_FROSTFEST_1P:
				case ScenarioDbId.TB_FROSTFEST:
					gameEntity = new TB_FrostFestival();
					goto IL_1D1E;
				case (ScenarioDbId)2578:
				case (ScenarioDbId)2579:
					goto IL_1D18;
				default:
					if (missionId != ScenarioDbId.TB_LK_RAID)
					{
						goto IL_1D18;
					}
					gameEntity = new TB_LichKingRaid();
					goto IL_1D1E;
				}
			}
			else
			{
				if (missionId == ScenarioDbId.LOOT_DUNGEON)
				{
					gameEntity = LOOT_Dungeon.InstantiateLootDungeonMissionEntityForBoss(powerList, createGame);
					goto IL_1D1E;
				}
				if (missionId != ScenarioDbId.TB_HEADLESSHORSEMAN)
				{
					goto IL_1D18;
				}
				gameEntity = new TB_HeadlessHorseman();
				goto IL_1D1E;
			}
			gameEntity = new TB_FireFest();
			goto IL_1D1E;
		}
		if (missionId <= ScenarioDbId.ICC_01_LICHKING)
		{
			if (missionId == ScenarioDbId.TB_LETHALPUZZLES)
			{
				gameEntity = new TB13_LethalPuzzles();
				goto IL_1D1E;
			}
			switch (missionId)
			{
			case ScenarioDbId.TB_DECKRECIPE_MSG_1P_TEST:
			case ScenarioDbId.TB_DECKRECIPE_MSG:
				gameEntity = new TB10_DeckRecipe();
				goto IL_1D1E;
			case ScenarioDbId.TB_BATTLEROYALE:
				goto IL_145A;
			case (ScenarioDbId)2125:
			case (ScenarioDbId)2126:
				goto IL_1D18;
			case ScenarioDbId.TB_DPROMO:
				gameEntity = new TB14_DPromo();
				goto IL_1D1E;
			default:
				if (missionId != ScenarioDbId.ICC_01_LICHKING)
				{
					goto IL_1D18;
				}
				gameEntity = new ICC_01_LICHKING();
				goto IL_1D1E;
			}
		}
		else
		{
			if (missionId == ScenarioDbId.ICC_03_SECRETS)
			{
				gameEntity = new ICC_03_SECRETS();
				goto IL_1D1E;
			}
			if (missionId == ScenarioDbId.ICC_04_SINDRAGOSA)
			{
				gameEntity = new ICC_04_Sindragosa();
				goto IL_1D1E;
			}
			switch (missionId)
			{
			case ScenarioDbId.TB_MAMMOTHPARTY_1P:
			case ScenarioDbId.TB_MAMMOTHPARTY:
				goto IL_1599;
			case (ScenarioDbId)2258:
			case (ScenarioDbId)2259:
			case (ScenarioDbId)2260:
			case (ScenarioDbId)2266:
			case (ScenarioDbId)2267:
			case (ScenarioDbId)2272:
			case (ScenarioDbId)2273:
			case (ScenarioDbId)2274:
			case (ScenarioDbId)2275:
			case (ScenarioDbId)2276:
				goto IL_1D18;
			case ScenarioDbId.ICC_05_LANATHEL:
				gameEntity = new ICC_05_Lanathel();
				goto IL_1D1E;
			case ScenarioDbId.ICC_06_MARROWGAR:
				gameEntity = new ICC_06_Marrowgar();
				goto IL_1D1E;
			case ScenarioDbId.ICC_07_PUTRICIDE:
				gameEntity = new ICC_07_Putricide();
				goto IL_1D1E;
			case ScenarioDbId.ICC_08_FINALE:
				gameEntity = new ICC_08_Finale();
				goto IL_1D1E;
			case ScenarioDbId.TB_MP_CROSSROADS_1P:
			case ScenarioDbId.TB_MP_CROSSROADS:
				gameEntity = new TB_MP_Crossroads();
				goto IL_1D1E;
			case ScenarioDbId.TB_MAMMOTHPARTY_STORMWIND:
				gameEntity = new TB16_MP_Stormwind();
				goto IL_1D1E;
			case ScenarioDbId.ICC_09_SAURFANG:
				gameEntity = new ICC_09_Saurfang();
				goto IL_1D1E;
			case ScenarioDbId.TB_100TH:
				break;
			default:
				goto IL_1D18;
			}
		}
		IL_15BA:
		gameEntity = new TB_100th();
		goto IL_1D1E;
		IL_145A:
		gameEntity = new TB15_BossBattleRoyale();
		goto IL_1D1E;
		IL_1599:
		gameEntity = new TB_MammothParty();
		goto IL_1D1E;
		IL_1D18:
		gameEntity = new StandardGameEntity();
		IL_1D1E:
		gameEntity.OnCreateGame();
		return gameEntity;
	}

	// Token: 0x06007BB9 RID: 31673 RVA: 0x00283392 File Offset: 0x00281592
	public bool IsAI()
	{
		return GameUtils.IsAIMission(this.m_missionId);
	}

	// Token: 0x06007BBA RID: 31674 RVA: 0x0028339F File Offset: 0x0028159F
	public bool WasAI()
	{
		return GameUtils.IsAIMission(this.m_prevMissionId);
	}

	// Token: 0x06007BBB RID: 31675 RVA: 0x002833AC File Offset: 0x002815AC
	public bool IsNextAI()
	{
		return GameUtils.IsAIMission(this.m_nextMissionId);
	}

	// Token: 0x06007BBC RID: 31676 RVA: 0x002833B9 File Offset: 0x002815B9
	public bool IsTutorial()
	{
		return GameUtils.IsTutorialMission(this.m_missionId);
	}

	// Token: 0x06007BBD RID: 31677 RVA: 0x002833C6 File Offset: 0x002815C6
	public bool WasTutorial()
	{
		return GameUtils.IsTutorialMission(this.m_prevMissionId);
	}

	// Token: 0x06007BBE RID: 31678 RVA: 0x002833D3 File Offset: 0x002815D3
	public bool IsNextTutorial()
	{
		return GameUtils.IsTutorialMission(this.m_nextMissionId);
	}

	// Token: 0x06007BBF RID: 31679 RVA: 0x002833E0 File Offset: 0x002815E0
	public bool IsPractice()
	{
		return GameUtils.IsPracticeMission(this.m_missionId);
	}

	// Token: 0x06007BC0 RID: 31680 RVA: 0x002833ED File Offset: 0x002815ED
	public bool WasPractice()
	{
		return GameUtils.IsPracticeMission(this.m_prevMissionId);
	}

	// Token: 0x06007BC1 RID: 31681 RVA: 0x002833FA File Offset: 0x002815FA
	public bool IsNextPractice()
	{
		return GameUtils.IsPracticeMission(this.m_nextMissionId);
	}

	// Token: 0x06007BC2 RID: 31682 RVA: 0x00283407 File Offset: 0x00281607
	public bool IsClassChallengeMission()
	{
		return GameUtils.IsClassChallengeMission(this.m_missionId);
	}

	// Token: 0x06007BC3 RID: 31683 RVA: 0x00283414 File Offset: 0x00281614
	public bool IsHeroicMission()
	{
		return GameUtils.IsHeroicAdventureMission(this.m_missionId);
	}

	// Token: 0x06007BC4 RID: 31684 RVA: 0x00283421 File Offset: 0x00281621
	public bool IsExpansionMission()
	{
		return GameUtils.IsExpansionMission(this.m_missionId);
	}

	// Token: 0x06007BC5 RID: 31685 RVA: 0x0028342E File Offset: 0x0028162E
	public bool WasExpansionMission()
	{
		return GameUtils.IsExpansionMission(this.m_prevMissionId);
	}

	// Token: 0x06007BC6 RID: 31686 RVA: 0x0028343B File Offset: 0x0028163B
	public bool IsNextExpansionMission()
	{
		return GameUtils.IsExpansionMission(this.m_nextMissionId);
	}

	// Token: 0x06007BC7 RID: 31687 RVA: 0x00283448 File Offset: 0x00281648
	public bool IsDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(this.m_missionId);
	}

	// Token: 0x06007BC8 RID: 31688 RVA: 0x00283455 File Offset: 0x00281655
	public bool WasDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(this.m_prevMissionId);
	}

	// Token: 0x06007BC9 RID: 31689 RVA: 0x00283462 File Offset: 0x00281662
	public bool IsNextDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(this.m_nextMissionId);
	}

	// Token: 0x06007BCA RID: 31690 RVA: 0x0028346F File Offset: 0x0028166F
	public bool IsPlay()
	{
		return this.IsRankedPlay() || this.IsUnrankedPlay();
	}

	// Token: 0x06007BCB RID: 31691 RVA: 0x00283481 File Offset: 0x00281681
	public bool WasPlay()
	{
		return this.WasRankedPlay() || this.WasUnrankedPlay();
	}

	// Token: 0x06007BCC RID: 31692 RVA: 0x00283493 File Offset: 0x00281693
	public bool IsNextPlay()
	{
		return this.IsNextRankedPlay() || this.IsNextUnrankedPlay();
	}

	// Token: 0x06007BCD RID: 31693 RVA: 0x002834A5 File Offset: 0x002816A5
	public bool IsRankedPlay()
	{
		return this.m_gameType == GameType.GT_RANKED;
	}

	// Token: 0x06007BCE RID: 31694 RVA: 0x002834B0 File Offset: 0x002816B0
	public bool WasRankedPlay()
	{
		return this.m_prevGameType == GameType.GT_RANKED;
	}

	// Token: 0x06007BCF RID: 31695 RVA: 0x002834BB File Offset: 0x002816BB
	public bool IsNextRankedPlay()
	{
		return this.m_nextGameType == GameType.GT_RANKED;
	}

	// Token: 0x06007BD0 RID: 31696 RVA: 0x002834C6 File Offset: 0x002816C6
	public bool IsUnrankedPlay()
	{
		return this.m_gameType == GameType.GT_CASUAL;
	}

	// Token: 0x06007BD1 RID: 31697 RVA: 0x002834D1 File Offset: 0x002816D1
	public bool WasUnrankedPlay()
	{
		return this.m_prevGameType == GameType.GT_CASUAL;
	}

	// Token: 0x06007BD2 RID: 31698 RVA: 0x002834DC File Offset: 0x002816DC
	public bool IsNextUnrankedPlay()
	{
		return this.m_nextGameType == GameType.GT_CASUAL;
	}

	// Token: 0x06007BD3 RID: 31699 RVA: 0x002834E7 File Offset: 0x002816E7
	public bool IsArena()
	{
		return this.m_gameType == GameType.GT_ARENA;
	}

	// Token: 0x06007BD4 RID: 31700 RVA: 0x002834F2 File Offset: 0x002816F2
	public bool WasArena()
	{
		return this.m_prevGameType == GameType.GT_ARENA;
	}

	// Token: 0x06007BD5 RID: 31701 RVA: 0x002834FD File Offset: 0x002816FD
	public bool IsNextArena()
	{
		return this.m_nextGameType == GameType.GT_ARENA;
	}

	// Token: 0x06007BD6 RID: 31702 RVA: 0x00283508 File Offset: 0x00281708
	public bool IsFriendly()
	{
		return this.m_gameType == GameType.GT_VS_FRIEND || this.m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
	}

	// Token: 0x06007BD7 RID: 31703 RVA: 0x0028351F File Offset: 0x0028171F
	public bool WasFriendly()
	{
		return this.m_prevGameType == GameType.GT_VS_FRIEND || this.m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
	}

	// Token: 0x06007BD8 RID: 31704 RVA: 0x00283536 File Offset: 0x00281736
	public bool IsNextFriendly()
	{
		return this.m_nextGameType == GameType.GT_VS_FRIEND || this.m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
	}

	// Token: 0x06007BD9 RID: 31705 RVA: 0x0028354D File Offset: 0x0028174D
	public bool WasTavernBrawl()
	{
		return GameUtils.IsTavernBrawlGameType(this.m_prevGameType) && !this.WasFriendly();
	}

	// Token: 0x06007BDA RID: 31706 RVA: 0x00283567 File Offset: 0x00281767
	public bool IsTavernBrawl()
	{
		return GameUtils.IsTavernBrawlGameType(this.m_gameType) && !this.IsFriendly();
	}

	// Token: 0x06007BDB RID: 31707 RVA: 0x00283581 File Offset: 0x00281781
	public bool IsNextTavernBrawl()
	{
		return GameUtils.IsTavernBrawlGameType(this.m_nextGameType) && !this.IsNextFriendly();
	}

	// Token: 0x06007BDC RID: 31708 RVA: 0x0028359B File Offset: 0x0028179B
	public bool IsBattlegrounds()
	{
		return this.m_gameType == GameType.GT_BATTLEGROUNDS || this.m_gameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
	}

	// Token: 0x06007BDD RID: 31709 RVA: 0x002835B3 File Offset: 0x002817B3
	public bool WasBattlegrounds()
	{
		return this.m_prevGameType == GameType.GT_BATTLEGROUNDS || this.m_prevGameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
	}

	// Token: 0x06007BDE RID: 31710 RVA: 0x002835CB File Offset: 0x002817CB
	public bool IsFriendlyBattlegrounds()
	{
		return this.m_gameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
	}

	// Token: 0x06007BDF RID: 31711 RVA: 0x002835D7 File Offset: 0x002817D7
	public bool IsStandardFormatType()
	{
		return this.m_formatType == FormatType.FT_STANDARD;
	}

	// Token: 0x06007BE0 RID: 31712 RVA: 0x002835E2 File Offset: 0x002817E2
	public bool IsWildFormatType()
	{
		return this.m_formatType == FormatType.FT_WILD;
	}

	// Token: 0x06007BE1 RID: 31713 RVA: 0x002835ED File Offset: 0x002817ED
	public bool IsClassicFormatType()
	{
		return this.m_formatType == FormatType.FT_CLASSIC;
	}

	// Token: 0x06007BE2 RID: 31714 RVA: 0x002835F8 File Offset: 0x002817F8
	public bool IsNextWildFormatType()
	{
		return this.m_nextFormatType == FormatType.FT_WILD;
	}

	// Token: 0x06007BE3 RID: 31715 RVA: 0x00283603 File Offset: 0x00281803
	public bool IsDuels()
	{
		return this.m_gameType == GameType.GT_PVPDR || this.m_gameType == GameType.GT_PVPDR_PAID;
	}

	// Token: 0x06007BE4 RID: 31716 RVA: 0x0028361B File Offset: 0x0028181B
	public bool WasDuels()
	{
		return this.m_prevGameType == GameType.GT_PVPDR || this.m_prevGameType == GameType.GT_PVPDR_PAID;
	}

	// Token: 0x06007BE5 RID: 31717 RVA: 0x00283633 File Offset: 0x00281833
	private SceneMgr.Mode GetSpectatorPostGameSceneMode()
	{
		if (PartyManager.Get().IsInBattlegroundsParty())
		{
			return SceneMgr.Mode.BACON;
		}
		if (GameUtils.AreAllTutorialsComplete())
		{
			return SceneMgr.Mode.HUB;
		}
		if (Network.ShouldBeConnectedToAurora())
		{
			return SceneMgr.Mode.INVALID;
		}
		return SceneMgr.Mode.HUB;
	}

	// Token: 0x06007BE6 RID: 31718 RVA: 0x00283658 File Offset: 0x00281858
	public SceneMgr.Mode GetPostGameSceneMode()
	{
		if (this.IsSpectator())
		{
			return this.GetSpectatorPostGameSceneMode();
		}
		SceneMgr.Mode result = SceneMgr.Mode.HUB;
		bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode > FiresideGatheringManager.FiresideGatheringMode.NONE;
		GameType gameType = this.m_gameType;
		switch (gameType)
		{
		case GameType.GT_VS_AI:
		{
			if (this.m_missionId == 3539)
			{
				return SceneMgr.Mode.BACON;
			}
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			if (tavernBrawlMission != null && tavernBrawlMission.missionId == this.m_missionId)
			{
				return SceneMgr.Mode.TAVERN_BRAWL;
			}
			return SceneMgr.Mode.ADVENTURE;
		}
		case GameType.GT_VS_FRIEND:
			break;
		case (GameType)3:
		case GameType.GT_TUTORIAL:
		case GameType.GT_TEST_AI_VS_AI:
			return result;
		case GameType.GT_ARENA:
			return SceneMgr.Mode.DRAFT;
		case GameType.GT_RANKED:
		case GameType.GT_CASUAL:
			return SceneMgr.Mode.TOURNAMENT;
		default:
			switch (gameType)
			{
			case GameType.GT_TAVERNBRAWL:
			case GameType.GT_FSG_BRAWL:
			case GameType.GT_FSG_BRAWL_2P_COOP:
				result = (flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.TAVERN_BRAWL);
				if (TavernBrawlManager.Get().CurrentTavernBrawlSeasonEndInSeconds < 10L && !flag)
				{
					return SceneMgr.Mode.HUB;
				}
				return result;
			case GameType.GT_TB_1P_VS_AI:
			case GameType.GT_TB_2P_COOP:
			case (GameType)25:
			case GameType.GT_RESERVED_18_22:
			case GameType.GT_RESERVED_18_23:
				return result;
			case GameType.GT_FSG_BRAWL_VS_FRIEND:
				break;
			case GameType.GT_FSG_BRAWL_1P_VS_AI:
				return flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.HUB;
			case GameType.GT_BATTLEGROUNDS:
			case GameType.GT_BATTLEGROUNDS_FRIENDLY:
				return SceneMgr.Mode.BACON;
			case GameType.GT_PVPDR_PAID:
			case GameType.GT_PVPDR:
				return SceneMgr.Mode.PVP_DUNGEON_RUN;
			default:
				return result;
			}
			break;
		}
		if (GameUtils.IsFiresideGatheringGameType(this.m_gameType) && GameUtils.IsTavernBrawlGameType(this.m_gameType))
		{
			flag = true;
		}
		if (!FriendChallengeMgr.Get().HasChallenge())
		{
			result = (flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.HUB);
		}
		else if (flag && GameUtils.IsFiresideGatheringGameType(this.m_gameType))
		{
			result = SceneMgr.Mode.FIRESIDE_GATHERING;
		}
		else if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			if (FriendChallengeMgr.Get().IsChallengeFiresideBrawl())
			{
				result = SceneMgr.Mode.HUB;
			}
			else
			{
				result = SceneMgr.Mode.TAVERN_BRAWL;
			}
		}
		else
		{
			result = SceneMgr.Mode.FRIENDLY;
		}
		return result;
	}

	// Token: 0x06007BE7 RID: 31719 RVA: 0x002837EA File Offset: 0x002819EA
	public SceneMgr.Mode GetPostDisconnectSceneMode()
	{
		if (this.IsSpectator())
		{
			return this.GetSpectatorPostGameSceneMode();
		}
		if (this.IsTutorial())
		{
			return SceneMgr.Mode.INVALID;
		}
		return this.GetPostGameSceneMode();
	}

	// Token: 0x06007BE8 RID: 31720 RVA: 0x0028380C File Offset: 0x00281A0C
	public void PreparePostGameSceneMode(SceneMgr.Mode mode)
	{
		if (mode == SceneMgr.Mode.ADVENTURE && AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.CHOOSER)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_missionId);
			if (record == null)
			{
				return;
			}
			int adventureId = record.AdventureId;
			if (adventureId == 0)
			{
				return;
			}
			int modeId = record.ModeId;
			if (modeId == 0)
			{
				return;
			}
			AdventureConfig.Get().SetSelectedAdventureMode((AdventureDbId)adventureId, (AdventureModeDbId)modeId);
			AdventureConfig.Get().ChangeSubSceneToSelectedAdventure();
			AdventureConfig.Get().SetMission((ScenarioDbId)this.m_missionId, false);
		}
	}

	// Token: 0x06007BE9 RID: 31721 RVA: 0x0028387C File Offset: 0x00281A7C
	public bool IsTransitionPopupShown()
	{
		return !(this.m_transitionPopup == null) && this.m_transitionPopup.IsShown();
	}

	// Token: 0x06007BEA RID: 31722 RVA: 0x00283899 File Offset: 0x00281A99
	public TransitionPopup GetTransitionPopup()
	{
		return this.m_transitionPopup;
	}

	// Token: 0x06007BEB RID: 31723 RVA: 0x002838A4 File Offset: 0x00281AA4
	public void UpdatePresence()
	{
		if (!Network.ShouldBeConnectedToAurora() || !Network.IsLoggedIn())
		{
			return;
		}
		if (this.IsSpectator())
		{
			PresenceMgr presenceMgr = PresenceMgr.Get();
			if (this.IsTutorial())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_TUTORIAL
				});
			}
			else if (this.IsBattlegrounds() || this.m_missionId == 3539)
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_BATTLEGROUNDS
				});
			}
			else if (this.IsPractice())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_PRACTICE
				});
			}
			else if (this.IsPlay())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_PLAY
				});
			}
			else if (this.IsArena())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_ARENA
				});
			}
			else if (this.IsFriendly())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_FRIENDLY
				});
			}
			else if (this.IsTavernBrawl())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_TAVERN_BRAWL
				});
			}
			else if (this.IsDuels())
			{
				presenceMgr.SetStatus(new Enum[]
				{
					Global.PresenceStatus.SPECTATING_GAME_DUELS
				});
			}
			else if (this.IsExpansionMission())
			{
				ScenarioDbId missionId = (ScenarioDbId)this.m_missionId;
				presenceMgr.SetStatus_SpectatingMission(missionId);
			}
			SpectatorManager.Get().UpdateMySpectatorInfo();
			return;
		}
		if (this.IsTutorial())
		{
			ScenarioDbId missionId2 = (ScenarioDbId)this.m_missionId;
			if (missionId2 <= ScenarioDbId.TUTORIAL_MUKLA)
			{
				if (missionId2 != ScenarioDbId.TUTORIAL_HOGGER)
				{
					if (missionId2 != ScenarioDbId.TUTORIAL_MILLHOUSE)
					{
						if (missionId2 == ScenarioDbId.TUTORIAL_MUKLA)
						{
							PresenceMgr.Get().SetStatus(new Enum[]
							{
								Global.PresenceStatus.TUTORIAL_GAME,
								PresenceTutorial.MUKLA
							});
						}
					}
					else
					{
						PresenceMgr.Get().SetStatus(new Enum[]
						{
							Global.PresenceStatus.TUTORIAL_GAME,
							PresenceTutorial.MILLHOUSE
						});
					}
				}
				else
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.TUTORIAL_GAME,
						PresenceTutorial.HOGGER
					});
				}
			}
			else if (missionId2 != ScenarioDbId.TUTORIAL_NESINGWARY)
			{
				if (missionId2 != ScenarioDbId.TUTORIAL_ILLIDAN)
				{
					if (missionId2 == ScenarioDbId.TUTORIAL_CHO)
					{
						PresenceMgr.Get().SetStatus(new Enum[]
						{
							Global.PresenceStatus.TUTORIAL_GAME,
							PresenceTutorial.CHO
						});
					}
				}
				else
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.TUTORIAL_GAME,
						PresenceTutorial.ILLIDAN
					});
				}
			}
			else
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.TUTORIAL_GAME,
					PresenceTutorial.HEMET
				});
			}
		}
		else if (this.IsBattlegrounds() || this.m_missionId == 3539)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.BATTLEGROUNDS_GAME
			});
		}
		else if (this.IsDuels())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.DUELS_GAME
			});
		}
		else if (this.IsPractice())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.PRACTICE_GAME
			});
		}
		else if (this.IsPlay())
		{
			if (this.IsRankedPlay())
			{
				if (this.IsStandardFormatType())
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.PLAY_RANKED_STANDARD
					});
				}
				else if (this.IsWildFormatType())
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.PLAY_RANKED_WILD
					});
				}
				else if (this.IsClassicFormatType())
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.PLAY_RANKED_CLASSIC
					});
				}
				else
				{
					PresenceMgr.Get().SetStatus(new Enum[]
					{
						Global.PresenceStatus.PLAY_GAME
					});
				}
			}
			else if (this.IsStandardFormatType())
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.PLAY_CASUAL_STANDARD
				});
			}
			else if (this.IsWildFormatType())
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.PLAY_CASUAL_WILD
				});
			}
			else if (this.IsClassicFormatType())
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.PLAY_CASUAL_CLASSIC
				});
			}
			else
			{
				PresenceMgr.Get().SetStatus(new Enum[]
				{
					Global.PresenceStatus.PLAY_GAME
				});
			}
		}
		else if (this.IsArena())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.ARENA_GAME
			});
		}
		else if (this.IsFriendly())
		{
			Global.PresenceStatus presenceStatus = Global.PresenceStatus.FRIENDLY_GAME;
			if (GameUtils.IsWaitingForOpponentReconnect())
			{
				presenceStatus = Global.PresenceStatus.WAIT_FOR_OPPONENT_RECONNECT;
			}
			else if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				presenceStatus = Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_GAME;
			}
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				presenceStatus
			});
		}
		else if (this.IsTavernBrawl())
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.TAVERN_BRAWL_GAME
			});
		}
		else if (this.IsExpansionMission())
		{
			ScenarioDbId missionId3 = (ScenarioDbId)this.m_missionId;
			PresenceMgr.Get().SetStatus_PlayingMission(missionId3);
		}
		SpectatorManager.Get().UpdateMySpectatorInfo();
	}

	// Token: 0x06007BEC RID: 31724 RVA: 0x00283DE0 File Offset: 0x00281FE0
	public void UpdateSessionPresence(GameType gameType)
	{
		if (gameType == GameType.GT_ARENA)
		{
			int wins = DraftManager.Get().GetWins();
			int losses = DraftManager.Get().GetLosses();
			SessionRecord sessionRecord = new SessionRecord();
			sessionRecord.Wins = (uint)wins;
			sessionRecord.Losses = (uint)losses;
			sessionRecord.RunFinished = false;
			sessionRecord.SessionRecordType = SessionRecordType.ARENA;
			BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord);
			return;
		}
		if (GameUtils.IsTavernBrawlGameType(gameType) && TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
		{
			int gamesWon = TavernBrawlManager.Get().GamesWon;
			int gamesLost = TavernBrawlManager.Get().GamesLost;
			SessionRecord sessionRecord2 = new SessionRecord();
			sessionRecord2.Wins = (uint)gamesWon;
			sessionRecord2.Losses = (uint)gamesLost;
			sessionRecord2.RunFinished = false;
			sessionRecord2.SessionRecordType = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_NORMAL) ? SessionRecordType.TAVERN_BRAWL : SessionRecordType.HEROIC_BRAWL);
			BnetPresenceMgr.Get().SetGameFieldBlob(22U, sessionRecord2);
		}
	}

	// Token: 0x06007BED RID: 31725 RVA: 0x00283EA9 File Offset: 0x002820A9
	public void SetLastDisplayedPlayerName(int playerId, string name)
	{
		this.m_lastDisplayedPlayerNames[playerId] = name;
	}

	// Token: 0x06007BEE RID: 31726 RVA: 0x00283EB8 File Offset: 0x002820B8
	public string GetLastDisplayedPlayerName(int playerId)
	{
		string result;
		this.m_lastDisplayedPlayerNames.TryGetValue(playerId, out result);
		return result;
	}

	// Token: 0x06007BEF RID: 31727 RVA: 0x00283ED5 File Offset: 0x002820D5
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode == SceneMgr.Mode.GAMEPLAY && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			this.OnGameEnded();
		}
	}

	// Token: 0x06007BF0 RID: 31728 RVA: 0x00283EF0 File Offset: 0x002820F0
	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		this.PreloadTransitionPopup();
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
		{
			this.DestroyTransitionPopup();
		}
		if (mode == SceneMgr.Mode.GAMEPLAY && prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			Screen.sleepTimeout = -1;
			return;
		}
		if (prevMode == SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.GAMEPLAY && !SpectatorManager.Get().IsInSpectatorMode())
		{
			Screen.sleepTimeout = -2;
		}
	}

	// Token: 0x06007BF1 RID: 31729 RVA: 0x00283F40 File Offset: 0x00282140
	private void OnServerResult()
	{
		if (!this.IsFindingGame())
		{
			return;
		}
		ServerResult serverResult = Network.Get().GetServerResult();
		if (serverResult.ResultCode == 1)
		{
			float secondsToWait = Mathf.Max(serverResult.HasRetryDelaySeconds ? serverResult.RetryDelaySeconds : 2f, 0.5f);
			Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.OnServerResult_Retry), null);
			Processor.ScheduleCallback(secondsToWait, true, new Processor.ScheduledCallback(this.OnServerResult_Retry), null);
			return;
		}
		if (serverResult.ResultCode == 2)
		{
			this.OnGameCanceled();
		}
	}

	// Token: 0x06007BF2 RID: 31730 RVA: 0x00283FC0 File Offset: 0x002821C0
	private void OnServerResult_Retry(object userData)
	{
		Network.Get().RetryGotoGameServer();
	}

	// Token: 0x06007BF3 RID: 31731 RVA: 0x00283FD0 File Offset: 0x002821D0
	private void ChangeBoardIfNecessary()
	{
		int board = this.m_gameSetup.Board;
		if (DemoMgr.Get().IsExpoDemo())
		{
			string str = Vars.Key("Demo.ForceBoard").GetStr(null);
			if (str != null)
			{
				board = GameUtils.GetBoardIdFromAssetName(str);
			}
		}
		this.m_gameSetup.Board = board;
	}

	// Token: 0x06007BF4 RID: 31732 RVA: 0x0028401C File Offset: 0x0028221C
	private void PreloadTransitionPopup()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.TAVERN_BRAWL:
			this.LoadTransitionPopup(this.MATCHING_POPUP_NAME);
			return;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.ADVENTURE:
			this.LoadTransitionPopup("LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168");
			break;
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.CREDITS:
		case SceneMgr.Mode.RESET:
			break;
		default:
			return;
		}
	}

	// Token: 0x06007BF5 RID: 31733 RVA: 0x0028407A File Offset: 0x0028227A
	private string DetermineTransitionPopupForFindGame(GameType gameType, int missionId)
	{
		if (gameType == GameType.GT_TUTORIAL)
		{
			return null;
		}
		if (GameUtils.IsMatchmadeGameType(gameType, new int?(missionId)))
		{
			return this.MATCHING_POPUP_NAME;
		}
		return "LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168";
	}

	// Token: 0x06007BF6 RID: 31734 RVA: 0x002840A4 File Offset: 0x002822A4
	private void LoadTransitionPopup(string prefabPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			global::Error.AddDevFatal("GameMgr.LoadTransitionPopup() - Failed to load {0}", new object[]
			{
				prefabPath
			});
			return;
		}
		if (this.m_transitionPopup != null)
		{
			UnityEngine.Object.Destroy(this.m_transitionPopup.gameObject);
		}
		this.m_transitionPopup = gameObject.GetComponent<TransitionPopup>();
		this.m_initialTransitionPopupPos = this.m_transitionPopup.transform.position;
		this.m_transitionPopup.RegisterMatchCanceledEvent(new TransitionPopup.MatchCanceledEvent(this.OnTransitionPopupCanceled));
		SceneUtils.SetLayer(this.m_transitionPopup, GameLayer.IgnoreFullScreenEffects);
	}

	// Token: 0x06007BF7 RID: 31735 RVA: 0x00284148 File Offset: 0x00282348
	private void ShowTransitionPopup(string popupName, int scenarioId)
	{
		Type right = this.s_transitionPopupNameToType[popupName];
		if (!this.m_transitionPopup || this.m_transitionPopup.GetType() != right)
		{
			this.DestroyTransitionPopup();
			this.LoadTransitionPopup(popupName);
		}
		if (this.m_transitionPopup.IsShown())
		{
			return;
		}
		if (Box.Get() != null && Box.Get().GetState() != Box.State.OPEN)
		{
			Vector3 b = Box.Get().m_Camera.GetCameraPosition(BoxCamera.State.OPENED) - this.m_initialTransitionPopupPos;
			Vector3 position = Box.Get().GetBoxCamera().m_IgnoreFullscreenEffectsCamera.transform.position - b;
			this.m_transitionPopup.transform.position = position;
		}
		AdventureDbId adventureId = GameUtils.GetAdventureId(this.m_nextMissionId);
		this.m_transitionPopup.SetAdventureId(adventureId);
		this.m_transitionPopup.SetFormatType(this.m_nextFormatType);
		this.m_transitionPopup.SetGameType(this.m_nextGameType);
		this.m_transitionPopup.SetDeckId(this.m_lastDeckId);
		this.m_transitionPopup.SetScenarioId(scenarioId);
		this.m_transitionPopup.Show();
		if (this.OnTransitionPopupShown != null)
		{
			this.OnTransitionPopupShown();
		}
	}

	// Token: 0x06007BF8 RID: 31736 RVA: 0x00284279 File Offset: 0x00282479
	private void OnTransitionPopupCanceled()
	{
		bool flag = Network.Get().IsFindingGame();
		if (flag)
		{
			Network.Get().CancelFindGame();
		}
		this.ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		if (!flag)
		{
			this.ChangeFindGameState(FindGameState.INVALID);
		}
	}

	// Token: 0x06007BF9 RID: 31737 RVA: 0x002842A4 File Offset: 0x002824A4
	private void DestroyTransitionPopup()
	{
		if (this.m_transitionPopup)
		{
			UnityEngine.Object.Destroy(this.m_transitionPopup.gameObject);
		}
	}

	// Token: 0x06007BFA RID: 31738 RVA: 0x002842C4 File Offset: 0x002824C4
	private bool GetFriendlyErrorMessage(int errorCode, ref string headerKey, ref string messageKey, ref object[] messageParams)
	{
		if (errorCode <= 1002002)
		{
			if (errorCode <= 1001000)
			{
				switch (errorCode)
				{
				case 1000500:
					headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
					messageKey = "GLOBAL_ERROR_FIND_GAME_SCENARIO_INCORRECT_NUM_PLAYERS";
					return true;
				case 1000501:
					headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
					messageKey = "GLOBAL_ERROR_FIND_GAME_SCENARIO_NO_DECK_SPECIFIED";
					return true;
				case 1000502:
					break;
				default:
					if (errorCode != 1001000)
					{
						return false;
					}
					headerKey = "GLOBAL_TAVERN_BRAWL";
					messageKey = "GLOBAL_TAVERN_BRAWL_ERROR_SEASON_INCREMENTED";
					TavernBrawlManager.Get().RefreshServerData(BrawlType.BRAWL_TYPE_UNKNOWN);
					return true;
				}
			}
			else
			{
				if (errorCode == 1001001)
				{
					headerKey = "GLOBAL_TAVERN_BRAWL";
					messageKey = "GLOBAL_TAVERN_BRAWL_ERROR_NOT_ACTIVE";
					TavernBrawlManager.Get().RefreshServerData(BrawlType.BRAWL_TYPE_UNKNOWN);
					return true;
				}
				if (errorCode != 1002002)
				{
					return false;
				}
				GameType gameType = this.GetGameType();
				if (gameType == GameType.GT_UNKNOWN)
				{
					gameType = this.GetNextGameType();
				}
				if (!GameUtils.IsMatchmadeGameType(gameType, null))
				{
					headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
					messageKey = "GLUE_ERROR_DECK_RULESET_RULE_VIOLATION";
					return true;
				}
				return false;
			}
		}
		else if (errorCode <= 1002008)
		{
			if (errorCode == 1002007)
			{
				headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
				messageKey = "GLUE_ERROR_DECK_VALIDATION_WRONG_FORMAT";
				return true;
			}
			if (errorCode != 1002008)
			{
				return false;
			}
		}
		else if (errorCode != 1003005)
		{
			if (errorCode != 1003015)
			{
				return false;
			}
			headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
			messageKey = "GLUE_ERROR_PLAY_GAME_PARTY_NOT_ALLOWED";
			return true;
		}
		else
		{
			if (this.m_nextGameType == GameType.GT_ARENA)
			{
				headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
				messageKey = "GLOBAL_ARENA_SEASON_ERROR_NOT_ACTIVE";
				DraftManager.Get().RefreshCurrentSeasonFromServer();
				if (SceneMgr.Get().GetMode() == SceneMgr.Mode.DRAFT)
				{
					Processor.ScheduleCallback(0f, false, delegate(object userData)
					{
						Navigation.GoBack();
					}, null);
				}
				return true;
			}
			if (this.m_nextGameType == GameType.GT_PVPDR || this.m_nextGameType == GameType.GT_PVPDR_PAID)
			{
				headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
				messageKey = "GLOBAL_PVPDR_SEASON_ERROR_NOT_ACTIVE";
				if (SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN)
				{
					Processor.ScheduleCallback(0f, false, delegate(object userData)
					{
						Navigation.GoBack();
					}, null);
				}
				return true;
			}
			return false;
		}
		headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
		messageKey = "GLOBAL_ERROR_FIND_GAME_SCENARIO_MISCONFIGURED";
		return true;
	}

	// Token: 0x06007BFB RID: 31739 RVA: 0x002844D0 File Offset: 0x002826D0
	private void OnGameQueueEvent(QueueEvent queueEvent)
	{
		FindGameState? findGameState = null;
		GameMgr.s_bnetToFindGameResultMap.TryGetValue(queueEvent.EventType, out findGameState);
		if (queueEvent.BnetError != 0)
		{
			this.m_lastEnterGameError = (uint)queueEvent.BnetError;
		}
		if (findGameState == null)
		{
			return;
		}
		if (queueEvent.EventType == QueueEvent.Type.QUEUE_DELAY_ERROR)
		{
			int bnetError = queueEvent.BnetError;
			if (bnetError == 25017)
			{
				return;
			}
			string headerKey = "";
			string messageKey = null;
			object[] messageArgs = new object[0];
			if (this.GetFriendlyErrorMessage(queueEvent.BnetError, ref headerKey, ref messageKey, ref messageArgs))
			{
				global::Error.AddWarningLoc(headerKey, messageKey, messageArgs);
				findGameState = new FindGameState?(FindGameState.BNET_QUEUE_CANCELED);
				this.HandleGameCanceled();
			}
		}
		if (queueEvent.BnetError != 0)
		{
			string arg = string.Empty;
			if (Enum.IsDefined(typeof(BattleNetErrors), (BattleNetErrors)queueEvent.BnetError))
			{
				arg = ((BattleNetErrors)queueEvent.BnetError).ToString();
			}
			else if (Enum.IsDefined(typeof(ErrorCode), (ErrorCode)queueEvent.BnetError))
			{
				arg = ((ErrorCode)queueEvent.BnetError).ToString();
			}
			string text = string.Format("OnGameQueueEvent error={0} {1}", queueEvent.BnetError, arg);
			if (HearthstoneApplication.IsInternal())
			{
				global::Error.AddDevWarning("OnGameQueueEvent", text, Array.Empty<object>());
			}
			else
			{
				global::Log.BattleNet.PrintWarning(text, Array.Empty<object>());
			}
		}
		if (queueEvent.EventType == QueueEvent.Type.QUEUE_GAME_STARTED)
		{
			queueEvent.GameServer.Mission = this.m_nextMissionId;
			this.ChangeFindGameState(findGameState.Value, queueEvent, queueEvent.GameServer, null);
			return;
		}
		this.ChangeFindGameState(findGameState.Value, queueEvent);
	}

	// Token: 0x06007BFC RID: 31740 RVA: 0x0028466C File Offset: 0x0028286C
	private void OnGameToJoinNotification()
	{
		GameToConnectNotification gameToConnectNotification = Network.Get().GetGameToConnectNotification();
		this.ConnectToGame(gameToConnectNotification.Info);
	}

	// Token: 0x06007BFD RID: 31741 RVA: 0x00284694 File Offset: 0x00282894
	private void OnGameSetup()
	{
		if (SpectatorManager.Get().IsSpectatingOpposingSide() && this.m_gameSetup != null)
		{
			return;
		}
		this.m_gameSetup = Network.Get().GetGameSetupInfo();
		this.ChangeBoardIfNecessary();
		if (this.m_findGameState == FindGameState.INVALID && this.m_gameType == GameType.GT_UNKNOWN)
		{
			Debug.LogError(string.Format("GameMgr.OnGameStarting() - Received {0} packet even though we're not looking for a game.", GameSetup.PacketID.ID));
			return;
		}
		this.m_lastGameData.Clear();
		this.m_lastGameData.GameConnectionInfo = this.m_connectionInfoForGameConnectingTo;
		this.m_connectionInfoForGameConnectingTo = null;
		this.m_prevGameType = this.m_gameType;
		this.m_gameType = this.m_nextGameType;
		this.m_nextGameType = GameType.GT_UNKNOWN;
		this.m_prevFormatType = this.m_formatType;
		this.m_formatType = this.m_nextFormatType;
		this.m_nextFormatType = FormatType.FT_UNKNOWN;
		this.m_prevMissionId = this.m_missionId;
		this.m_missionId = this.m_nextMissionId;
		this.m_nextMissionId = 0;
		this.m_brawlLibraryItemId = this.m_nextBrawlLibraryItemId;
		this.m_nextBrawlLibraryItemId = 0;
		this.m_prevReconnectType = this.m_reconnectType;
		this.m_reconnectType = this.m_nextReconnectType;
		this.m_nextReconnectType = ReconnectType.INVALID;
		this.m_prevSpectator = this.m_spectator;
		this.m_spectator = this.m_nextSpectator;
		this.m_nextSpectator = false;
		if (!this.m_spectator)
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.StartPerformanceFlow(new FlowPerformanceGame.GameSetupConfig
				{
					GameType = this.m_gameType,
					BoardId = this.m_gameSetup.Board,
					ScenarioId = this.m_missionId,
					FormatType = this.m_formatType
				});
			}
		}
		this.ChangeFindGameState(FindGameState.SERVER_GAME_STARTED);
	}

	// Token: 0x06007BFE RID: 31742 RVA: 0x00284820 File Offset: 0x00282A20
	private void OnGameCanceled()
	{
		this.HandleGameCanceled();
		Network network = Network.Get();
		Network.GameCancelInfo gameCancelInfo = network.GetGameCancelInfo();
		network.DisconnectFromGameServer();
		this.ChangeFindGameState(FindGameState.SERVER_GAME_CANCELED, gameCancelInfo);
	}

	// Token: 0x06007BFF RID: 31743 RVA: 0x00284850 File Offset: 0x00282A50
	public bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (info.GetFeature() == BnetFeature.Games)
		{
			BattleNetErrors error = info.GetError();
			this.m_lastEnterGameError = (uint)error;
			string arg = null;
			bool flag = false;
			FindGameState state = FindGameState.BNET_ERROR;
			if (error == BattleNetErrors.ERROR_GAME_MASTER_INVALID_FACTORY || error == BattleNetErrors.ERROR_GAME_MASTER_NO_GAME_SERVER || error == BattleNetErrors.ERROR_GAME_MASTER_NO_FACTORY)
			{
				arg = error.ToString();
				flag = true;
			}
			if (!flag)
			{
				string headerKey = "";
				string messageKey = null;
				object[] messageArgs = new object[0];
				ReconnectMgr reconnectMgr = ReconnectMgr.Get();
				if (this.GetFriendlyErrorMessage((int)this.m_lastEnterGameError, ref headerKey, ref messageKey, ref messageArgs) && !reconnectMgr.IsReconnecting() && !reconnectMgr.IsRestoringGameStateFromDatabase())
				{
					global::Error.AddWarningLoc(headerKey, messageKey, messageArgs);
					ErrorCode lastEnterGameError = (ErrorCode)this.m_lastEnterGameError;
					arg = lastEnterGameError.ToString();
					state = FindGameState.BNET_QUEUE_CANCELED;
					flag = true;
				}
			}
			if (!flag && info.GetFeatureEvent() == BnetFeatureEvent.Games_OnFindGame)
			{
				flag = true;
			}
			if (flag)
			{
				string text = string.Format("GameMgr.OnBnetError() - received error {0} {1}", this.m_lastEnterGameError, arg);
				global::Log.BattleNet.PrintError(text, Array.Empty<object>());
				if (!global::Log.BattleNet.CanPrint(LogTarget.CONSOLE, global::Log.LogLevel.Error, false))
				{
					Debug.LogError(string.Format("[{0}] {1}", "BattleNet", text));
				}
				this.HandleGameCanceled();
				this.ChangeFindGameState(state);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007C00 RID: 31744 RVA: 0x0028497D File Offset: 0x00282B7D
	private void HandleGameCanceled()
	{
		this.m_nextGameType = GameType.GT_UNKNOWN;
		this.m_nextFormatType = FormatType.FT_UNKNOWN;
		this.m_nextMissionId = 0;
		this.m_nextBrawlLibraryItemId = 0;
		this.m_nextReconnectType = ReconnectType.INVALID;
		this.m_nextSpectator = false;
		Network.Get().ClearLastGameServerJoined();
	}

	// Token: 0x06007C01 RID: 31745 RVA: 0x002849B3 File Offset: 0x00282BB3
	private bool OnReconnectTimeout(object userData)
	{
		this.HandleGameCanceled();
		this.ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		this.ChangeFindGameState(FindGameState.INVALID);
		return false;
	}

	// Token: 0x06007C02 RID: 31746 RVA: 0x002849CC File Offset: 0x00282BCC
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (this.IsFindingGame())
		{
			this.ChangeFindGameState(FindGameState.CLIENT_CANCELED);
			this.ChangeFindGameState(FindGameState.INVALID);
			DialogManager.Get().ShowReconnectHelperDialog(null, null);
		}
	}

	// Token: 0x06007C03 RID: 31747 RVA: 0x002849F2 File Offset: 0x00282BF2
	private bool ChangeFindGameState(FindGameState state)
	{
		return this.ChangeFindGameState(state, null, null, null);
	}

	// Token: 0x06007C04 RID: 31748 RVA: 0x002849FE File Offset: 0x00282BFE
	private bool ChangeFindGameState(FindGameState state, QueueEvent queueEvent)
	{
		return this.ChangeFindGameState(state, queueEvent, null, null);
	}

	// Token: 0x06007C05 RID: 31749 RVA: 0x00284A0A File Offset: 0x00282C0A
	private bool ChangeFindGameState(FindGameState state, GameServerInfo serverInfo)
	{
		return this.ChangeFindGameState(state, null, serverInfo, null);
	}

	// Token: 0x06007C06 RID: 31750 RVA: 0x00284A16 File Offset: 0x00282C16
	private bool ChangeFindGameState(FindGameState state, Network.GameCancelInfo cancelInfo)
	{
		return this.ChangeFindGameState(state, null, null, cancelInfo);
	}

	// Token: 0x06007C07 RID: 31751 RVA: 0x00284A24 File Offset: 0x00282C24
	private bool ChangeFindGameState(FindGameState state, QueueEvent queueEvent, GameServerInfo serverInfo, Network.GameCancelInfo cancelInfo)
	{
		FindGameState findGameState = this.m_findGameState;
		uint lastEnterGameError = this.m_lastEnterGameError;
		this.m_findGameState = state;
		FindGameEventData findGameEventData = new FindGameEventData();
		findGameEventData.m_state = state;
		findGameEventData.m_gameServer = serverInfo;
		findGameEventData.m_cancelInfo = cancelInfo;
		if (queueEvent != null)
		{
			findGameEventData.m_queueMinSeconds = queueEvent.MinSeconds;
			findGameEventData.m_queueMaxSeconds = queueEvent.MaxSeconds;
		}
		switch (state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			Network.Get().RemoveGameServerDisconnectEventListener(new Network.GameServerDisconnectEvent(this.OnGameServerDisconnect));
			break;
		}
		bool flag = this.FireFindGameEvent(findGameEventData);
		if (!flag)
		{
			this.DoDefaultFindGameEventBehavior(findGameEventData);
		}
		this.FinalizeState(findGameEventData);
		if (findGameState != state)
		{
			Network.Get().OnFindGameStateChanged(findGameState, state, lastEnterGameError);
		}
		return flag;
	}

	// Token: 0x06007C08 RID: 31752 RVA: 0x00284AF0 File Offset: 0x00282CF0
	private bool FireFindGameEvent(FindGameEventData eventData)
	{
		bool flag = false;
		GameMgr.FindGameListener[] array = this.m_findGameListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			flag = (array[i].Fire(eventData) || flag);
		}
		return flag;
	}

	// Token: 0x06007C09 RID: 31753 RVA: 0x00284B28 File Offset: 0x00282D28
	private void DoDefaultFindGameEventBehavior(FindGameEventData eventData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
			this.HideTransitionPopup();
			break;
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_ERROR:
		{
			ReconnectMgr reconnectMgr = ReconnectMgr.Get();
			if (!reconnectMgr.IsReconnecting() && !reconnectMgr.IsRestoringGameStateFromDatabase())
			{
				global::Error.AddWarningLoc("GLOBAL_ERROR_GENERIC_HEADER", "GLOBAL_ERROR_GAME_DENIED", Array.Empty<object>());
			}
			this.HideTransitionPopup();
			return;
		}
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
			break;
		case FindGameState.BNET_QUEUE_CANCELED:
			this.HideTransitionPopup();
			return;
		case FindGameState.SERVER_GAME_CONNECTING:
			Network.Get().GotoGameServer(eventData.m_gameServer, this.IsNextReconnect());
			return;
		case FindGameState.SERVER_GAME_STARTED:
			if (Box.Get() != null)
			{
				LoadingScreen.Get().SetFreezeFrameCamera(Box.Get().GetCamera());
				LoadingScreen.Get().SetTransitionAudioListener(Box.Get().GetAudioListener());
			}
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAMEPLAY, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				return;
			}
			if (!SpectatorManager.Get().IsSpectatingOpposingSide())
			{
				SceneMgr.Get().ReloadMode();
				return;
			}
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			if (eventData.m_cancelInfo != null)
			{
				Network.GameCancelInfo.Reason cancelReason = eventData.m_cancelInfo.CancelReason;
				if (cancelReason - Network.GameCancelInfo.Reason.OPPONENT_TIMEOUT <= 2)
				{
					global::Error.AddWarningLoc("GLOBAL_ERROR_GENERIC_HEADER", "GLOBAL_ERROR_GAME_OPPONENT_TIMEOUT", Array.Empty<object>());
				}
				else
				{
					global::Error.AddDevWarning("GAME ERROR", "The Game Server canceled the game. Error: {0}", new object[]
					{
						eventData.m_cancelInfo.CancelReason
					});
				}
			}
			this.HideTransitionPopup();
			return;
		default:
			return;
		}
	}

	// Token: 0x06007C0A RID: 31754 RVA: 0x00284C88 File Offset: 0x00282E88
	private void FinalizeState(FindGameEventData eventData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			this.ChangeFindGameState(FindGameState.INVALID);
			break;
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
		case FindGameState.SERVER_GAME_CONNECTING:
			break;
		default:
			return;
		}
	}

	// Token: 0x06007C0B RID: 31755 RVA: 0x00284CD4 File Offset: 0x00282ED4
	private void OnGameEnded()
	{
		if (!this.m_spectator)
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.StopCurrentFlow();
			}
		}
		this.m_prevGameType = this.m_gameType;
		this.m_gameType = GameType.GT_UNKNOWN;
		this.m_prevFormatType = this.m_formatType;
		this.m_formatType = FormatType.FT_UNKNOWN;
		this.m_prevMissionId = this.m_missionId;
		this.m_missionId = 0;
		this.m_brawlLibraryItemId = 0;
		this.m_prevReconnectType = this.m_reconnectType;
		this.m_reconnectType = ReconnectType.INVALID;
		this.m_prevSpectator = this.m_spectator;
		this.m_spectator = false;
		this.m_lastEnterGameError = 0U;
		this.m_pendingAutoConcede = false;
		this.m_gameSetup = null;
		this.m_lastDisplayedPlayerNames.Clear();
	}

	// Token: 0x04006504 RID: 25860
	private const string MATCHING_POPUP_PC_NAME = "MatchingPopup3D.prefab:4f4a40d14d907e94da1b81d97c18a44f";

	// Token: 0x04006505 RID: 25861
	private const string MATCHING_POPUP_PHONE_NAME = "MatchingPopup3D_phone.prefab:a7a5cea6306a1fa4680a9782fd25be14";

	// Token: 0x04006506 RID: 25862
	private const string LOADING_POPUP_NAME = "LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168";

	// Token: 0x04006507 RID: 25863
	private const int MINIMUM_SECONDS_TIL_TB_END_TO_RETURN_TO_TB_SCENE = 10;

	// Token: 0x04006508 RID: 25864
	private PlatformDependentValue<string> MATCHING_POPUP_NAME;

	// Token: 0x04006509 RID: 25865
	private readonly global::Map<string, Type> s_transitionPopupNameToType = new global::Map<string, Type>
	{
		{
			"MatchingPopup3D.prefab:4f4a40d14d907e94da1b81d97c18a44f",
			typeof(MatchingPopupDisplay)
		},
		{
			"MatchingPopup3D_phone.prefab:a7a5cea6306a1fa4680a9782fd25be14",
			typeof(MatchingPopupDisplay)
		},
		{
			"LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168",
			typeof(LoadingPopupDisplay)
		}
	};

	// Token: 0x0400650A RID: 25866
	private LastGameData m_lastGameData = new LastGameData();

	// Token: 0x0400650B RID: 25867
	private GameConnectionInfo m_connectionInfoForGameConnectingTo;

	// Token: 0x0400650C RID: 25868
	private GameType m_gameType;

	// Token: 0x0400650D RID: 25869
	private GameType m_prevGameType;

	// Token: 0x0400650E RID: 25870
	private GameType m_nextGameType;

	// Token: 0x0400650F RID: 25871
	private FormatType m_formatType;

	// Token: 0x04006510 RID: 25872
	private FormatType m_prevFormatType;

	// Token: 0x04006511 RID: 25873
	private FormatType m_nextFormatType;

	// Token: 0x04006512 RID: 25874
	private int m_missionId;

	// Token: 0x04006513 RID: 25875
	private int m_prevMissionId;

	// Token: 0x04006514 RID: 25876
	private int m_nextMissionId;

	// Token: 0x04006515 RID: 25877
	private int m_brawlLibraryItemId;

	// Token: 0x04006516 RID: 25878
	private int m_nextBrawlLibraryItemId;

	// Token: 0x04006517 RID: 25879
	private ReconnectType m_reconnectType;

	// Token: 0x04006518 RID: 25880
	private ReconnectType m_prevReconnectType;

	// Token: 0x04006519 RID: 25881
	private ReconnectType m_nextReconnectType;

	// Token: 0x0400651A RID: 25882
	private bool m_readyToProcessGameConnections;

	// Token: 0x0400651B RID: 25883
	private GameConnectionInfo m_deferredGameConnectionInfo;

	// Token: 0x0400651C RID: 25884
	private bool m_spectator;

	// Token: 0x0400651D RID: 25885
	private bool m_prevSpectator;

	// Token: 0x0400651E RID: 25886
	private bool m_nextSpectator;

	// Token: 0x0400651F RID: 25887
	private long? m_lastDeckId;

	// Token: 0x04006520 RID: 25888
	private string m_lastAIDeck;

	// Token: 0x04006521 RID: 25889
	private int? m_lastHeroCardDbId;

	// Token: 0x04006522 RID: 25890
	private int? m_lastSeasonId;

	// Token: 0x04006523 RID: 25891
	private uint m_lastEnterGameError;

	// Token: 0x04006524 RID: 25892
	private bool m_pendingAutoConcede;

	// Token: 0x04006525 RID: 25893
	private FindGameState m_findGameState;

	// Token: 0x04006526 RID: 25894
	private List<GameMgr.FindGameListener> m_findGameListeners = new List<GameMgr.FindGameListener>();

	// Token: 0x04006527 RID: 25895
	private TransitionPopup m_transitionPopup;

	// Token: 0x04006528 RID: 25896
	private Vector3 m_initialTransitionPopupPos;

	// Token: 0x04006529 RID: 25897
	private Network.GameSetup m_gameSetup;

	// Token: 0x0400652A RID: 25898
	private global::Map<int, string> m_lastDisplayedPlayerNames = new global::Map<int, string>();

	// Token: 0x0400652B RID: 25899
	private static global::Map<QueueEvent.Type, FindGameState?> s_bnetToFindGameResultMap = new global::Map<QueueEvent.Type, FindGameState?>
	{
		{
			QueueEvent.Type.UNKNOWN,
			null
		},
		{
			QueueEvent.Type.QUEUE_ENTER,
			new FindGameState?(FindGameState.BNET_QUEUE_ENTERED)
		},
		{
			QueueEvent.Type.QUEUE_LEAVE,
			null
		},
		{
			QueueEvent.Type.QUEUE_DELAY,
			new FindGameState?(FindGameState.BNET_QUEUE_DELAYED)
		},
		{
			QueueEvent.Type.QUEUE_UPDATE,
			new FindGameState?(FindGameState.BNET_QUEUE_UPDATED)
		},
		{
			QueueEvent.Type.QUEUE_DELAY_ERROR,
			new FindGameState?(FindGameState.BNET_ERROR)
		},
		{
			QueueEvent.Type.QUEUE_AMM_ERROR,
			new FindGameState?(FindGameState.BNET_ERROR)
		},
		{
			QueueEvent.Type.QUEUE_WAIT_END,
			null
		},
		{
			QueueEvent.Type.QUEUE_CANCEL,
			new FindGameState?(FindGameState.BNET_QUEUE_CANCELED)
		},
		{
			QueueEvent.Type.QUEUE_GAME_STARTED,
			new FindGameState?(FindGameState.SERVER_GAME_CONNECTING)
		},
		{
			QueueEvent.Type.ABORT_CLIENT_DROPPED,
			new FindGameState?(FindGameState.BNET_ERROR)
		}
	};

	// Token: 0x0400652C RID: 25900
	public const int NO_BRAWL_LIBRARY_ITEM_ID = 0;

	// Token: 0x0200253A RID: 9530
	// (Invoke) Token: 0x0601325F RID: 78431
	public delegate bool FindGameCallback(FindGameEventData eventData, object userData);

	// Token: 0x0200253B RID: 9531
	private class FindGameListener : global::EventListener<GameMgr.FindGameCallback>
	{
		// Token: 0x06013262 RID: 78434 RVA: 0x0052AE6F File Offset: 0x0052906F
		public bool Fire(FindGameEventData eventData)
		{
			return this.m_callback(eventData, this.m_userData);
		}
	}
}
