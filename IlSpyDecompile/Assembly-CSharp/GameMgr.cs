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

public class GameMgr : IService
{
	public delegate bool FindGameCallback(FindGameEventData eventData, object userData);

	private class FindGameListener : EventListener<FindGameCallback>
	{
		public bool Fire(FindGameEventData eventData)
		{
			return m_callback(eventData, m_userData);
		}
	}

	private const string MATCHING_POPUP_PC_NAME = "MatchingPopup3D.prefab:4f4a40d14d907e94da1b81d97c18a44f";

	private const string MATCHING_POPUP_PHONE_NAME = "MatchingPopup3D_phone.prefab:a7a5cea6306a1fa4680a9782fd25be14";

	private const string LOADING_POPUP_NAME = "LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168";

	private const int MINIMUM_SECONDS_TIL_TB_END_TO_RETURN_TO_TB_SCENE = 10;

	private PlatformDependentValue<string> MATCHING_POPUP_NAME;

	private readonly Map<string, Type> s_transitionPopupNameToType = new Map<string, Type>
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

	private LastGameData m_lastGameData = new LastGameData();

	private GameConnectionInfo m_connectionInfoForGameConnectingTo;

	private GameType m_gameType;

	private GameType m_prevGameType;

	private GameType m_nextGameType;

	private FormatType m_formatType;

	private FormatType m_prevFormatType;

	private FormatType m_nextFormatType;

	private int m_missionId;

	private int m_prevMissionId;

	private int m_nextMissionId;

	private int m_brawlLibraryItemId;

	private int m_nextBrawlLibraryItemId;

	private ReconnectType m_reconnectType;

	private ReconnectType m_prevReconnectType;

	private ReconnectType m_nextReconnectType;

	private bool m_readyToProcessGameConnections;

	private GameConnectionInfo m_deferredGameConnectionInfo;

	private bool m_spectator;

	private bool m_prevSpectator;

	private bool m_nextSpectator;

	private long? m_lastDeckId;

	private string m_lastAIDeck;

	private int? m_lastHeroCardDbId;

	private int? m_lastSeasonId;

	private uint m_lastEnterGameError;

	private bool m_pendingAutoConcede;

	private FindGameState m_findGameState;

	private List<FindGameListener> m_findGameListeners = new List<FindGameListener>();

	private TransitionPopup m_transitionPopup;

	private Vector3 m_initialTransitionPopupPos;

	private Network.GameSetup m_gameSetup;

	private Map<int, string> m_lastDisplayedPlayerNames = new Map<int, string>();

	private static Map<QueueEvent.Type, FindGameState?> s_bnetToFindGameResultMap = new Map<QueueEvent.Type, FindGameState?>
	{
		{
			QueueEvent.Type.UNKNOWN,
			null
		},
		{
			QueueEvent.Type.QUEUE_ENTER,
			FindGameState.BNET_QUEUE_ENTERED
		},
		{
			QueueEvent.Type.QUEUE_LEAVE,
			null
		},
		{
			QueueEvent.Type.QUEUE_DELAY,
			FindGameState.BNET_QUEUE_DELAYED
		},
		{
			QueueEvent.Type.QUEUE_UPDATE,
			FindGameState.BNET_QUEUE_UPDATED
		},
		{
			QueueEvent.Type.QUEUE_DELAY_ERROR,
			FindGameState.BNET_ERROR
		},
		{
			QueueEvent.Type.QUEUE_AMM_ERROR,
			FindGameState.BNET_ERROR
		},
		{
			QueueEvent.Type.QUEUE_WAIT_END,
			null
		},
		{
			QueueEvent.Type.QUEUE_CANCEL,
			FindGameState.BNET_QUEUE_CANCELED
		},
		{
			QueueEvent.Type.QUEUE_GAME_STARTED,
			FindGameState.SERVER_GAME_CONNECTING
		},
		{
			QueueEvent.Type.ABORT_CLIENT_DROPPED,
			FindGameState.BNET_ERROR
		}
	};

	public const int NO_BRAWL_LIBRARY_ITEM_ID = 0;

	public long? LastDeckId => m_lastDeckId;

	public int? LastHeroCardDbId => m_lastHeroCardDbId;

	public LastGameData LastGameData => m_lastGameData;

	public event Action OnTransitionPopupShown;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		MATCHING_POPUP_NAME = new PlatformDependentValue<string>(PlatformCategory.Screen)
		{
			PC = "MatchingPopup3D.prefab:4f4a40d14d907e94da1b81d97c18a44f",
			Phone = "MatchingPopup3D_phone.prefab:a7a5cea6306a1fa4680a9782fd25be14"
		};
		Network network = serviceLocator.Get<Network>();
		network.RegisterGameQueueHandler(OnGameQueueEvent);
		network.RegisterNetHandler(GameToConnectNotification.PacketID.ID, OnGameToJoinNotification);
		network.RegisterNetHandler(GameSetup.PacketID.ID, OnGameSetup);
		network.RegisterNetHandler(GameCanceled.PacketID.ID, OnGameCanceled);
		network.RegisterNetHandler(ServerResult.PacketID.ID, OnServerResult);
		network.AddBnetErrorListener(BnetFeature.Games, OnBnetError);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(Network) };
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		m_gameType = GameType.GT_UNKNOWN;
		m_prevGameType = GameType.GT_UNKNOWN;
		m_nextGameType = GameType.GT_UNKNOWN;
		m_formatType = FormatType.FT_UNKNOWN;
		m_prevFormatType = FormatType.FT_UNKNOWN;
		m_nextFormatType = FormatType.FT_UNKNOWN;
		m_missionId = 0;
		m_prevMissionId = 0;
		m_nextMissionId = 0;
		m_brawlLibraryItemId = 0;
		m_nextBrawlLibraryItemId = 0;
		m_reconnectType = ReconnectType.INVALID;
		m_prevReconnectType = ReconnectType.INVALID;
		m_nextReconnectType = ReconnectType.INVALID;
		m_readyToProcessGameConnections = false;
		m_deferredGameConnectionInfo = null;
		m_spectator = false;
		m_prevSpectator = false;
		m_nextSpectator = false;
		m_lastEnterGameError = 0u;
		m_findGameState = FindGameState.INVALID;
		m_gameSetup = null;
		m_lastDisplayedPlayerNames.Clear();
		m_connectionInfoForGameConnectingTo = null;
		m_lastGameData.Clear();
	}

	public static GameMgr Get()
	{
		return HearthstoneServices.Get<GameMgr>();
	}

	public void OnLoggedIn()
	{
		SceneMgr.Get().RegisterSceneUnloadedEvent(OnSceneUnloaded);
		SceneMgr.Get().RegisterScenePreLoadEvent(OnScenePreLoad);
		ReconnectMgr.Get().AddTimeoutListener(OnReconnectTimeout);
	}

	public GameType GetGameType()
	{
		return m_gameType;
	}

	public GameType GetPreviousGameType()
	{
		return m_prevGameType;
	}

	public GameType GetNextGameType()
	{
		return m_nextGameType;
	}

	public FormatType GetFormatType()
	{
		return m_formatType;
	}

	public FormatType GetPreviousFormatType()
	{
		return m_prevFormatType;
	}

	public FormatType GetNextFormatType()
	{
		return m_nextFormatType;
	}

	public int GetMissionId()
	{
		return m_missionId;
	}

	public int GetPreviousMissionId()
	{
		return m_prevMissionId;
	}

	public int GetNextMissionId()
	{
		return m_nextMissionId;
	}

	public ReconnectType GetReconnectType()
	{
		return m_reconnectType;
	}

	public ReconnectType GetPreviousReconnectType()
	{
		return m_prevReconnectType;
	}

	public ReconnectType GetNextReconnectType()
	{
		return m_nextReconnectType;
	}

	public bool IsReconnect()
	{
		return m_reconnectType != ReconnectType.INVALID;
	}

	public bool IsPreviousReconnect()
	{
		return m_prevReconnectType != ReconnectType.INVALID;
	}

	public bool IsNextReconnect()
	{
		return m_nextReconnectType != ReconnectType.INVALID;
	}

	public bool IsSpectator()
	{
		return m_spectator;
	}

	public bool WasSpectator()
	{
		return m_prevSpectator;
	}

	public bool IsNextSpectator()
	{
		return m_nextSpectator;
	}

	public uint GetLastEnterGameError()
	{
		return m_lastEnterGameError;
	}

	public bool IsPendingAutoConcede()
	{
		return m_pendingAutoConcede;
	}

	public void SetPendingAutoConcede(bool pendingAutoConcede)
	{
		if (Network.Get().IsConnectedToGameServer())
		{
			m_pendingAutoConcede = pendingAutoConcede;
		}
	}

	public Network.GameSetup GetGameSetup()
	{
		return m_gameSetup;
	}

	public bool ConnectToGame(GameConnectionInfo info)
	{
		if (info == null)
		{
			Log.GameMgr.PrintWarning("ConnectToGame() called with no GameConnectionInfo passed in!");
			return false;
		}
		if (!m_readyToProcessGameConnections)
		{
			Log.GameMgr.Print("Received a GameConnectionInfo packet before the game is finished initializing; deferring it until later.");
			if (m_deferredGameConnectionInfo != null)
			{
				Log.GameMgr.PrintWarning("Another deferredGameConnectionInfo packet already exists.  Older packet GameType: {0}  Newer packet GameType: {1}", m_deferredGameConnectionInfo.GameType, info.GameType);
				Log.GameMgr.PrintWarning("Stomping over another deferred GameConnectionInfo packet.");
			}
			m_deferredGameConnectionInfo = info;
			return false;
		}
		FindGameState? findGameState = s_bnetToFindGameResultMap[QueueEvent.Type.QUEUE_GAME_STARTED];
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = info.Address;
		gameServerInfo.Port = (uint)info.Port;
		gameServerInfo.GameHandle = (uint)info.GameHandle;
		gameServerInfo.ClientHandle = info.ClientHandle;
		gameServerInfo.AuroraPassword = info.AuroraPassword;
		gameServerInfo.Mission = info.Scenario;
		m_nextGameType = info.GameType;
		m_nextFormatType = info.FormatType;
		m_nextMissionId = info.Scenario;
		m_connectionInfoForGameConnectingTo = info;
		gameServerInfo.Version = BattleNet.GetVersion();
		gameServerInfo.Resumable = true;
		QueueEvent queueEvent = new QueueEvent(QueueEvent.Type.QUEUE_GAME_STARTED, 0, 0, 0, gameServerInfo);
		ChangeFindGameState(findGameState.Value, queueEvent, queueEvent.GameServer, null);
		return true;
	}

	public bool ConnectToGameIfHaveDeferredConnectionPacket()
	{
		m_readyToProcessGameConnections = true;
		if (m_deferredGameConnectionInfo != null)
		{
			bool result = ConnectToGame(m_deferredGameConnectionInfo);
			m_deferredGameConnectionInfo = null;
			return result;
		}
		return false;
	}

	public FindGameState GetFindGameState()
	{
		return m_findGameState;
	}

	public bool IsFindingGame()
	{
		return m_findGameState != FindGameState.INVALID;
	}

	public bool IsAboutToStopFindingGame()
	{
		switch (m_findGameState)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			return true;
		default:
			return false;
		}
	}

	public void RegisterFindGameEvent(FindGameCallback callback)
	{
		RegisterFindGameEvent(callback, null);
	}

	public void RegisterFindGameEvent(FindGameCallback callback, object userData)
	{
		FindGameListener findGameListener = new FindGameListener();
		findGameListener.SetCallback(callback);
		findGameListener.SetUserData(userData);
		if (!m_findGameListeners.Contains(findGameListener))
		{
			m_findGameListeners.Add(findGameListener);
		}
	}

	public bool UnregisterFindGameEvent(FindGameCallback callback)
	{
		return UnregisterFindGameEvent(callback, null);
	}

	public bool UnregisterFindGameEvent(FindGameCallback callback, object userData)
	{
		FindGameListener findGameListener = new FindGameListener();
		findGameListener.SetCallback(callback);
		findGameListener.SetUserData(userData);
		return m_findGameListeners.Remove(findGameListener);
	}

	private void FindGameInternal(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId, long deckId, string aiDeck, int heroCardDbId, int? seasonId, bool restoreSavedGameState, byte[] snapshot, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		m_lastEnterGameError = 0u;
		m_nextGameType = gameType;
		m_nextFormatType = formatType;
		m_nextMissionId = missionId;
		m_nextBrawlLibraryItemId = brawlLibraryItemId;
		m_lastDeckId = deckId;
		m_lastAIDeck = aiDeck;
		m_lastHeroCardDbId = heroCardDbId;
		m_lastSeasonId = seasonId;
		ChangeFindGameState(FindGameState.CLIENT_STARTED);
		Network.Get().FindGame(gameType, formatType, missionId, brawlLibraryItemId, deckId, aiDeck, heroCardDbId, seasonId, restoreSavedGameState, snapshot, progFilterOverride);
		UpdateSessionPresence(gameType);
	}

	public void FindGame(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId = 0, long deckId = 0L, string aiDeck = null, int? seasonId = null, bool restoreSavedGameState = false, byte[] snapshot = null, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		FindGameInternal(gameType, formatType, missionId, brawlLibraryItemId, deckId, aiDeck, 0, seasonId, restoreSavedGameState, snapshot, progFilterOverride);
		if (!restoreSavedGameState)
		{
			string text = DetermineTransitionPopupForFindGame(gameType, missionId);
			if (text != null)
			{
				ShowTransitionPopup(text, missionId);
			}
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			CollectionDeck deck = collectionManager.GetDeck(deckId);
			if (deck != null)
			{
				Log.Decks.PrintInfo("Finding Game With Deck:");
				deck.LogDeckStringInformation();
			}
		}
	}

	public void FindGameWithHero(GameType gameType, FormatType formatType, int missionId, int brawlLibraryItemId, int heroCardDbId, long deckid = 0L)
	{
		FindGameInternal(gameType, formatType, missionId, brawlLibraryItemId, deckid, null, heroCardDbId, null, restoreSavedGameState: false, null);
		string text = DetermineTransitionPopupForFindGame(gameType, missionId);
		if (text != null)
		{
			ShowTransitionPopup(text, missionId);
		}
		Log.Decks.PrintInfo("Finding Game With Hero: {0}", heroCardDbId);
	}

	public void Cheat_ShowTransitionPopup(GameType gameType, FormatType formatType, int missionId)
	{
		if (HearthstoneApplication.IsInternal())
		{
			m_nextMissionId = missionId;
			m_nextFormatType = formatType;
			string text = DetermineTransitionPopupForFindGame(gameType, missionId);
			if (text != null)
			{
				ShowTransitionPopup(text, missionId);
			}
		}
	}

	public void RestartGame()
	{
		FindGameInternal(m_gameType, m_formatType, m_missionId, m_brawlLibraryItemId, m_lastDeckId ?? 0, m_lastAIDeck, m_lastHeroCardDbId ?? 0, m_lastSeasonId, restoreSavedGameState: false, null);
	}

	public bool HasLastPlayedDeckId()
	{
		return m_lastDeckId.HasValue;
	}

	public void EnterFriendlyChallengeGameWithDecks(FormatType formatType, BrawlType brawlType, int missionId, int seasonId, int brawlLibraryItemId, DeckShareState player1DeckShareState, long player1DeckId, DeckShareState player2DeckShareState, long player2DeckId, BnetGameAccountId player2GameAccountId)
	{
		Network.Get().EnterFriendlyChallengeGame(formatType, brawlType, missionId, seasonId, brawlLibraryItemId, player1DeckShareState, player1DeckId, player2DeckShareState, player2DeckId, null, null, player2GameAccountId);
	}

	public void EnterFriendlyChallengeGameWithHeroes(FormatType formatType, BrawlType brawlType, int missionId, int seasonId, int brawlLibraryItemId, long player1HeroCardDbId, long player2HeroCardDbId, BnetGameAccountId player2GameAccountId)
	{
		Network.Get().EnterFriendlyChallengeGame(formatType, brawlType, missionId, seasonId, brawlLibraryItemId, DeckShareState.NO_DECK_SHARE, 0L, DeckShareState.NO_DECK_SHARE, 0L, player1HeroCardDbId, player2HeroCardDbId, player2GameAccountId);
	}

	public void WaitForFriendChallengeToStart(FormatType formatType, BrawlType brawlType, int missionId, int brawlLibraryItemId, bool isBaconGame)
	{
		m_nextFormatType = formatType;
		m_nextMissionId = missionId;
		m_nextBrawlLibraryItemId = brawlLibraryItemId;
		m_lastEnterGameError = 0u;
		bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode != FiresideGatheringManager.FiresideGatheringMode.NONE;
		if (brawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || (brawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL && flag))
		{
			m_nextGameType = GameType.GT_FSG_BRAWL_VS_FRIEND;
			ChangeFindGameState(FindGameState.CLIENT_STARTED);
		}
		else if (isBaconGame)
		{
			if (PartyManager.Get().GetCurrentPartySize() <= PartyManager.Get().GetBattlegroundsMaxRankedPartySize())
			{
				m_nextGameType = GameType.GT_BATTLEGROUNDS;
			}
			else
			{
				m_nextGameType = GameType.GT_BATTLEGROUNDS_FRIENDLY;
			}
			ChangeFindGameState(FindGameState.BNET_QUEUE_ENTERED);
		}
		else
		{
			m_nextGameType = GameType.GT_VS_FRIEND;
			ChangeFindGameState(FindGameState.CLIENT_STARTED);
		}
		string text = DetermineTransitionPopupForFindGame(m_nextGameType, missionId);
		if (text != null)
		{
			ShowTransitionPopup(text, missionId);
		}
		else
		{
			Debug.LogError("WaitForFriendChallengeToStart - No valid transition popup.");
		}
	}

	public void SpectateGame(JoinInfo joinInfo)
	{
		GameServerInfo gameServerInfo = new GameServerInfo();
		gameServerInfo.Address = joinInfo.ServerIpAddress;
		gameServerInfo.Port = joinInfo.ServerPort;
		gameServerInfo.GameHandle = (uint)joinInfo.GameHandle;
		gameServerInfo.SpectatorPassword = joinInfo.SecretKey;
		gameServerInfo.SpectatorMode = true;
		m_nextGameType = joinInfo.GameType;
		m_nextFormatType = joinInfo.FormatType;
		m_nextMissionId = joinInfo.MissionId;
		m_brawlLibraryItemId = joinInfo.BrawlLibraryItemId;
		m_nextSpectator = true;
		m_lastEnterGameError = 0u;
		ChangeFindGameState(FindGameState.CLIENT_STARTED);
		ShowTransitionPopup("LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168", joinInfo.MissionId);
		ChangeFindGameState(FindGameState.SERVER_GAME_CONNECTING, gameServerInfo);
		if (Gameplay.Get() == null)
		{
			Network.Get().SetGameServerDisconnectEventListener(OnGameServerDisconnect);
		}
	}

	private void OnGameServerDisconnect(BattleNetErrors error)
	{
		OnGameCanceled();
	}

	public void ReconnectGame(GameType gameType, FormatType formatType, ReconnectType reconnectType, GameServerInfo serverInfo)
	{
		m_nextGameType = gameType;
		m_nextFormatType = formatType;
		m_nextMissionId = serverInfo.Mission;
		m_nextBrawlLibraryItemId = serverInfo.BrawlLibraryItemId;
		m_nextReconnectType = reconnectType;
		m_nextSpectator = serverInfo.SpectatorMode;
		m_lastEnterGameError = 0u;
		ChangeFindGameState(FindGameState.CLIENT_STARTED);
		ChangeFindGameState(FindGameState.SERVER_GAME_CONNECTING, serverInfo);
	}

	public bool CancelFindGame()
	{
		if (!GameUtils.IsMatchmadeGameType(m_nextGameType))
		{
			return false;
		}
		if (!Network.Get().IsFindingGame())
		{
			return false;
		}
		Network.Get().CancelFindGame();
		if (IsFindingGame())
		{
			ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		}
		return true;
	}

	public void HideTransitionPopup()
	{
		if ((bool)m_transitionPopup)
		{
			m_transitionPopup.Hide();
		}
	}

	public GameEntity CreateGameEntity(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		FlowPerformanceGame flowPerformanceGame = HearthstonePerformance.Get()?.GetCurrentPerformanceFlow<FlowPerformanceGame>();
		if (flowPerformanceGame != null)
		{
			flowPerformanceGame.GameUuid = createGame.Uuid;
		}
		GameEntity gameEntity = null;
		switch (m_missionId)
		{
		case 3:
			gameEntity = new Tutorial_01();
			break;
		case 4:
			gameEntity = new Tutorial_02();
			break;
		case 181:
			gameEntity = new Tutorial_03();
			break;
		case 201:
			gameEntity = new Tutorial_04();
			break;
		case 248:
			gameEntity = new Tutorial_05();
			break;
		case 249:
			gameEntity = new Tutorial_06();
			break;
		case 273:
		case 300:
			gameEntity = new NAX01_AnubRekhan();
			break;
		case 274:
		case 294:
		case 301:
			gameEntity = new NAX02_Faerlina();
			break;
		case 278:
		case 293:
		case 302:
			gameEntity = new NAX03_Maexxna();
			break;
		case 275:
		case 303:
			gameEntity = new NAX04_Noth();
			break;
		case 276:
		case 297:
		case 304:
			gameEntity = new NAX05_Heigan();
			break;
		case 277:
		case 288:
		case 305:
			gameEntity = new NAX06_Loatheb();
			break;
		case 279:
		case 306:
			gameEntity = new NAX07_Razuvious();
			break;
		case 280:
		case 296:
		case 307:
			gameEntity = new NAX08_Gothik();
			break;
		case 281:
		case 299:
		case 308:
			gameEntity = new NAX09_Horsemen();
			break;
		case 282:
		case 309:
			gameEntity = new NAX10_Patchwerk();
			break;
		case 283:
		case 292:
		case 310:
			gameEntity = new NAX11_Grobbulus();
			break;
		case 284:
		case 311:
			gameEntity = new NAX12_Gluth();
			break;
		case 285:
		case 295:
		case 312:
			gameEntity = new NAX13_Thaddius();
			break;
		case 287:
		case 313:
			gameEntity = new NAX14_Sapphiron();
			break;
		case 286:
		case 298:
		case 314:
			gameEntity = new NAX15_KelThuzad();
			break;
		case 319:
		case 337:
		case 355:
			gameEntity = new BRM01_GrimGuzzler();
			break;
		case 320:
		case 338:
		case 360:
			gameEntity = new BRM02_DarkIronArena();
			break;
		case 321:
		case 339:
			gameEntity = new BRM03_Thaurissan();
			break;
		case 322:
		case 340:
		case 354:
			gameEntity = new BRM04_Garr();
			break;
		case 323:
		case 341:
		case 356:
			gameEntity = new BRM05_BaronGeddon();
			break;
		case 324:
		case 343:
			gameEntity = new BRM06_Majordomo();
			break;
		case 325:
		case 344:
			gameEntity = new BRM07_Omokk();
			break;
		case 326:
		case 336:
		case 345:
			gameEntity = new BRM08_Drakkisath();
			break;
		case 327:
		case 357:
		case 361:
			gameEntity = new BRM09_RendBlackhand();
			break;
		case 328:
		case 346:
		case 359:
			gameEntity = new BRM10_Razorgore();
			break;
		case 329:
		case 347:
		case 358:
			gameEntity = new BRM11_Vaelastrasz();
			break;
		case 330:
		case 348:
			gameEntity = new BRM12_Chromaggus();
			break;
		case 331:
		case 349:
			gameEntity = new BRM13_Nefarian();
			break;
		case 333:
		case 351:
			gameEntity = new BRM15_Maloriak();
			break;
		case 332:
		case 350:
		case 353:
			gameEntity = new BRM14_Omnotron();
			break;
		case 334:
		case 352:
			gameEntity = new BRM16_Atramedes();
			break;
		case 335:
		case 362:
			gameEntity = new BRM17_ZombieNef();
			break;
		case 383:
			gameEntity = new TB01_RagVsNef();
			break;
		case 600:
		case 660:
		case 840:
		case 1147:
		case 1633:
		case 1679:
			gameEntity = new TB02_CoOp();
			break;
		case 1844:
		case 1865:
		case 1937:
		case 1939:
			gameEntity = new TB11_CoOpv3();
			break;
		case 395:
		case 1654:
			gameEntity = new TB04_DeckBuilding();
			break;
		case 1668:
		case 1674:
			gameEntity = new TB_ChooseYourFateBuildaround();
			break;
		case 1675:
		case 1676:
			gameEntity = new TB_ChooseYourFateRandom();
			break;
		case 1656:
		case 1658:
			gameEntity = new TB05_GiftExchange();
			break;
		case 1779:
		case 1781:
		case 1813:
			gameEntity = new TB09_ShadowTowers();
			break;
		case 1852:
		case 1855:
			gameEntity = new TB10_DeckRecipe();
			break;
		case 2122:
		case 2124:
			gameEntity = new TB10_DeckRecipe();
			break;
		case 1920:
		case 1921:
			gameEntity = new TB12_PartyPortals();
			break;
		case 2114:
			gameEntity = new TB13_LethalPuzzles();
			break;
		case 2785:
			gameEntity = new TB13_LethalPuzzles_Restart();
			break;
		case 2127:
			gameEntity = new TB14_DPromo();
			break;
		case 2106:
		case 2123:
			gameEntity = new TB15_BossBattleRoyale();
			break;
		case 1000:
		case 1637:
		case 1661:
			gameEntity = new LOE10_Giantfin();
			break;
		case 1283:
		case 1645:
		case 1659:
			gameEntity = new LOE09_LordSlitherspear();
			break;
		case 1041:
		case 1638:
		case 1650:
			gameEntity = new LOE01_Zinaar();
			break;
		case 1040:
		case 1603:
		case 1651:
			gameEntity = new LOE02_Sun_Raider_Phaerix();
			break;
		case 1060:
		case 1624:
		case 1660:
			gameEntity = new LOE04_Scarvash();
			break;
		case 1084:
		case 1586:
		case 1657:
			gameEntity = new LOE08_Archaedas();
			break;
		case 1183:
		case 1625:
		case 1671:
			gameEntity = new LOE14_Steel_Sentinel();
			break;
		case 1061:
		case 1653:
			gameEntity = new LOE03_AncientTemple();
			break;
		case 1062:
		case 1669:
			gameEntity = new LOE07_MineCart();
			break;
		case 1086:
		case 1646:
		case 1666:
			gameEntity = new LOE12_Naga();
			break;
		case 1146:
		case 1672:
			gameEntity = new LOE15_Boss1();
			break;
		case 1173:
		case 1673:
			gameEntity = new LOE16_Boss2();
			break;
		case 1143:
		case 1628:
		case 1670:
			gameEntity = new LOE13_Skelesaurus();
			break;
		case 1703:
		case 1705:
			gameEntity = new TB_KelthuzadRafaam();
			break;
		case 1766:
		case 1817:
			gameEntity = new KAR00_Prologue();
			break;
		case 1765:
		case 1818:
		case 1861:
			gameEntity = new KAR01_Pantry();
			break;
		case 1755:
		case 1819:
		case 1860:
			gameEntity = new KAR02_Mirror();
			break;
		case 1749:
		case 1820:
			gameEntity = new KAR03_Chess();
			break;
		case 1753:
		case 1831:
		case 1857:
			gameEntity = new KAR04_Julianne();
			break;
		case 1724:
		case 1832:
		case 1853:
			gameEntity = new KAR05_Wolf();
			break;
		case 1723:
		case 1833:
			gameEntity = new KAR06_Crone();
			break;
		case 1756:
		case 1834:
		case 1850:
			gameEntity = new KAR07_Curator();
			break;
		case 1759:
		case 1835:
		case 1863:
			gameEntity = new KAR08_Nightbane();
			break;
		case 1758:
		case 1836:
		case 1856:
			gameEntity = new KAR09_Illhoof();
			break;
		case 1760:
		case 1816:
		case 1854:
			gameEntity = new KAR10_Aran();
			break;
		case 1761:
		case 1815:
		case 1862:
			gameEntity = new KAR11_Netherspite();
			break;
		case 1767:
		case 1837:
			gameEntity = new KAR12_Portals();
			break;
		case 2112:
		case 2113:
			gameEntity = new TB_Blizzcon_2016();
			break;
		case 2257:
		case 2261:
		case 2960:
			gameEntity = new TB_MammothParty();
			break;
		case 2268:
		case 2269:
			gameEntity = new TB_MP_Crossroads();
			break;
		case 2270:
			gameEntity = new TB16_MP_Stormwind();
			break;
		case 2277:
		case 2507:
			gameEntity = new TB_100th();
			break;
		case 2577:
		case 2580:
			gameEntity = new TB_FrostFestival();
			break;
		case 2569:
		case 2576:
			gameEntity = new TB_FireFest();
			break;
		case 2011:
			gameEntity = new TB_Juggernaut();
			break;
		case 2626:
			gameEntity = new TB_LichKingRaid();
			break;
		case 2199:
			gameEntity = new ICC_01_LICHKING();
			break;
		case 2210:
			gameEntity = new ICC_03_SECRETS();
			break;
		case 2212:
			gameEntity = new ICC_04_Sindragosa();
			break;
		case 2262:
			gameEntity = new ICC_05_Lanathel();
			break;
		case 2263:
			gameEntity = new ICC_06_Marrowgar();
			break;
		case 2264:
			gameEntity = new ICC_07_Putricide();
			break;
		case 2265:
			gameEntity = new ICC_08_Finale();
			break;
		case 2271:
			gameEntity = new ICC_09_Saurfang();
			break;
		case 2495:
			gameEntity = new ICC_10_Deathwhisper();
			break;
		case 2667:
			gameEntity = new TB_HeadlessHorseman();
			break;
		case 2694:
			gameEntity = new TB_HeadlessRedux();
			break;
		case 2663:
			gameEntity = LOOT_Dungeon.InstantiateLootDungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 2706:
			gameEntity = GIL_Dungeon.InstantiateGilDungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 2821:
			gameEntity = GIL_Dungeon.InstantiateGilDungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 2902:
			gameEntity = new BOTA_Mirror_Puzzle_1();
			break;
		case 2911:
			gameEntity = new BOTA_Mirror_Puzzle_2();
			break;
		case 2913:
			gameEntity = new BOTA_Mirror_Puzzle_3();
			break;
		case 2914:
			gameEntity = new BOTA_Mirror_Puzzle_4();
			break;
		case 2920:
			gameEntity = new BOTA_Mirror_Boom();
			break;
		case 2909:
			gameEntity = new BOTA_Survival_Puzzle_1();
			break;
		case 2917:
			gameEntity = new BOTA_Survival_Puzzle_2();
			break;
		case 2918:
			gameEntity = new BOTA_Survival_Puzzle_3();
			break;
		case 2919:
			gameEntity = new BOTA_Survival_Puzzle_4();
			break;
		case 2978:
			gameEntity = new BOTA_Survival_Boom();
			break;
		case 2941:
			gameEntity = new BOTA_Lethal_Puzzle_1();
			break;
		case 2942:
			gameEntity = new BOTA_Lethal_Puzzle_2();
			break;
		case 2943:
			gameEntity = new BOTA_Lethal_Puzzle_3();
			break;
		case 2944:
			gameEntity = new BOTA_Lethal_Puzzle_4();
			break;
		case 2977:
			gameEntity = new BOTA_Lethal_Boom();
			break;
		case 2956:
			gameEntity = new BOTA_Clear_Puzzle_1();
			break;
		case 2957:
			gameEntity = new BOTA_Clear_Puzzle_2();
			break;
		case 2958:
			gameEntity = new BOTA_Clear_Puzzle_3();
			break;
		case 2959:
			gameEntity = new BOTA_Clear_Puzzle_4();
			break;
		case 2979:
			gameEntity = new BOTA_Clear_Boom();
			break;
		case 2890:
			gameEntity = TRL_Dungeon.InstantiateTRLDungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3005:
		case 3188:
		case 3189:
		case 3190:
		case 3191:
		case 3328:
		case 3329:
		case 3330:
		case 3331:
		case 3332:
			gameEntity = DALA_Dungeon.InstantiateDALADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3236:
		case 3359:
			gameEntity = new DALA_Tavern();
			break;
		case 3428:
		case 3429:
		case 3430:
		case 3431:
		case 3432:
		case 3433:
		case 3434:
		case 3435:
		case 3436:
		case 3437:
			gameEntity = ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3438:
		case 3439:
			gameEntity = new ULDA_Tavern();
			break;
		case 3469:
		case 3470:
		case 3471:
		case 3472:
		case 3473:
		case 3475:
		case 3477:
		case 3478:
		case 3479:
		case 3480:
		case 3481:
		case 3483:
		case 3484:
		case 3488:
		case 3489:
		case 3490:
		case 3491:
		case 3493:
		case 3494:
		case 3495:
		case 3497:
		case 3498:
		case 3499:
		case 3500:
		case 3556:
		case 3583:
		case 3584:
		case 3585:
		case 3586:
		case 3587:
		case 3588:
		case 3589:
		case 3590:
		case 3591:
		case 3592:
		case 3593:
		case 3594:
		case 3595:
		case 3596:
		case 3597:
		case 3598:
		case 3599:
		case 3600:
		case 3601:
		case 3602:
		case 3603:
		case 3604:
		case 3605:
			gameEntity = DRGA_Dungeon.InstantiateDRGADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3557:
		case 3558:
		case 3559:
		case 3560:
		case 3561:
		case 3562:
		case 3563:
		case 3564:
		case 3565:
		case 3566:
		case 3567:
		case 3568:
		case 3569:
		case 3570:
		case 3571:
		case 3572:
		case 3573:
			gameEntity = BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3574:
		case 3575:
		case 3576:
		case 3577:
		case 3578:
		case 3579:
		case 3580:
		case 3581:
		case 3582:
			gameEntity = BTA_Dungeon_Heroic.InstantiateBTADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 2695:
			gameEntity = new FB_ELObrawl();
			break;
		case 2731:
			gameEntity = new TB_KoboldGifts();
			break;
		case 2738:
			gameEntity = new TB_Marin();
			break;
		case 2742:
		case 2749:
		case 3347:
			gameEntity = new FB_Champs();
			break;
		case 2767:
		case 2770:
			gameEntity = new FB_BuildABrawl();
			break;
		case 2803:
		case 2830:
			gameEntity = new TB_NewYearRaven();
			break;
		case 2894:
		case 2901:
			gameEntity = new TB_Firefest2();
			break;
		case 2672:
		case 2940:
		case 3011:
			gameEntity = new FB_DuelersBrawl();
			break;
		case 2856:
		case 3028:
			gameEntity = new FB_TokiCoop();
			break;
		case 3200:
		case 3209:
			gameEntity = new TB_TrollsWeek1();
			break;
		case 3248:
		case 3266:
			gameEntity = new TB_Henchmania();
			break;
		case 3275:
		case 3325:
			gameEntity = new TB_Ignoblegarden();
			break;
		case 3327:
		case 3334:
			gameEntity = new TB_207();
			break;
		case 3357:
		case 3358:
			gameEntity = new TB_AutoBrawl();
			break;
		case 3387:
		case 3389:
			gameEntity = new TB_Firefest3();
			break;
		case 3341:
		case 3501:
		case 3714:
			gameEntity = new TB_RandomDeckKeepWinnerDeck();
			break;
		case 3447:
		case 3449:
		case 3451:
			gameEntity = new TB_EVILBRM();
			break;
		case 3422:
			gameEntity = new FB_RagRaidScript();
			break;
		case 3414:
		case 3459:
			gameEntity = new TB_BaconShop();
			break;
		case 3539:
			gameEntity = new TB_BaconShop_Tutorial();
			break;
		case 3427:
			gameEntity = new TB_BaconHand();
			break;
		case 3426:
			gameEntity = new TB_MartinAutoBrawl();
			break;
		case 3237:
		case 3238:
			gameEntity = new TB_NoMulligan();
			break;
		case 3372:
			gameEntity = new TB_DrawnDiscovery();
			break;
		case 3458:
			gameEntity = new TB_LEAGUE_REVIVAL();
			break;
		case 3367:
		case 3525:
			gameEntity = new TB_Carousel();
			break;
		case 3529:
		case 3530:
			gameEntity = ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss(powerList, createGame);
			break;
		case 3552:
		case 3553:
		case 3614:
		case 3615:
		case 3616:
		case 3617:
		case 3618:
		case 3619:
			gameEntity = new TB_RoadToNR();
			break;
		case 3555:
			gameEntity = new TB_RoadToNR_Tavern();
			break;
		case 3611:
		case 3620:
			gameEntity = new TB_SPT_DALA();
			break;
		case 3540:
			gameEntity = new RP_Fight_01();
			break;
		case 3541:
			gameEntity = new RP_Fight_02();
			break;
		case 3543:
			gameEntity = new RP_Fight_03();
			break;
		case 3624:
			gameEntity = new BTA_Prologue_Fight_01();
			break;
		case 3648:
			gameEntity = new BTA_Prologue_Fight_02();
			break;
		case 3649:
			gameEntity = new BTA_Prologue_Fight_03();
			break;
		case 3650:
			gameEntity = new BTA_Prologue_Fight_04();
			break;
		case 3710:
		case 3713:
			gameEntity = new TB_RumbleDome();
			break;
		case 3724:
			gameEntity = new BoH_Jaina_01();
			break;
		case 3725:
			gameEntity = new BoH_Jaina_02();
			break;
		case 3726:
			gameEntity = new BoH_Jaina_03();
			break;
		case 3727:
			gameEntity = new BoH_Jaina_04();
			break;
		case 3728:
			gameEntity = new BoH_Jaina_05();
			break;
		case 3729:
			gameEntity = new BoH_Jaina_06();
			break;
		case 3730:
			gameEntity = new BoH_Jaina_07();
			break;
		case 3731:
			gameEntity = new BoH_Jaina_08();
			break;
		case 3766:
			gameEntity = new BoH_Rexxar_01();
			break;
		case 3767:
			gameEntity = new BoH_Rexxar_02();
			break;
		case 3768:
			gameEntity = new BoH_Rexxar_03();
			break;
		case 3769:
			gameEntity = new BoH_Rexxar_04();
			break;
		case 3770:
			gameEntity = new BoH_Rexxar_05();
			break;
		case 3771:
			gameEntity = new BoH_Rexxar_06();
			break;
		case 3772:
			gameEntity = new BoH_Rexxar_07();
			break;
		case 3773:
			gameEntity = new BoH_Rexxar_08();
			break;
		case 3793:
			gameEntity = new BoH_Garrosh_01();
			break;
		case 3794:
			gameEntity = new BoH_Garrosh_02();
			break;
		case 3795:
			gameEntity = new BoH_Garrosh_03();
			break;
		case 3796:
			gameEntity = new BoH_Garrosh_04();
			break;
		case 3797:
			gameEntity = new BoH_Garrosh_05();
			break;
		case 3798:
			gameEntity = new BoH_Garrosh_06();
			break;
		case 3799:
			gameEntity = new BoH_Garrosh_07();
			break;
		case 3800:
			gameEntity = new BoH_Garrosh_08();
			break;
		case 3810:
			gameEntity = new BoH_Uther_01();
			break;
		case 3811:
			gameEntity = new BoH_Uther_02();
			break;
		case 3812:
			gameEntity = new BoH_Uther_03();
			break;
		case 3813:
			gameEntity = new BoH_Uther_04();
			break;
		case 3814:
			gameEntity = new BoH_Uther_05();
			break;
		case 3815:
			gameEntity = new BoH_Uther_06();
			break;
		case 3816:
			gameEntity = new BoH_Uther_07();
			break;
		case 3817:
			gameEntity = new BoH_Uther_08();
			break;
		case 3466:
			gameEntity = new WizardDuels();
			break;
		case 3825:
			gameEntity = new BoH_Anduin_01();
			break;
		case 3826:
			gameEntity = new BoH_Anduin_02();
			break;
		case 3827:
			gameEntity = new BoH_Anduin_03();
			break;
		case 3828:
			gameEntity = new BoH_Anduin_04();
			break;
		case 3829:
			gameEntity = new BoH_Anduin_05();
			break;
		case 3830:
			gameEntity = new BoH_Anduin_06();
			break;
		case 3831:
			gameEntity = new BoH_Anduin_07();
			break;
		case 3832:
			gameEntity = new BoH_Anduin_08();
			break;
		case 3851:
			gameEntity = new BoH_Valeera_01();
			break;
		case 3852:
			gameEntity = new BoH_Valeera_02();
			break;
		case 3853:
			gameEntity = new BoH_Valeera_03();
			break;
		case 3854:
			gameEntity = new BoH_Valeera_04();
			break;
		case 3855:
			gameEntity = new BoH_Valeera_05();
			break;
		case 3856:
			gameEntity = new BoH_Valeera_06();
			break;
		case 3857:
			gameEntity = new BoH_Valeera_07();
			break;
		case 3858:
			gameEntity = new BoH_Valeera_08();
			break;
		case 3839:
			gameEntity = new BOM_01_Rokara_01();
			break;
		case 3840:
			gameEntity = new BOM_01_Rokara_02();
			break;
		case 3841:
			gameEntity = new BOM_01_Rokara_03();
			break;
		case 3842:
			gameEntity = new BOM_01_Rokara_04();
			break;
		case 3843:
			gameEntity = new BOM_01_Rokara_05();
			break;
		case 3844:
			gameEntity = new BOM_01_Rokara_06();
			break;
		case 3845:
			gameEntity = new BOM_01_Rokara_07();
			break;
		case 3846:
			gameEntity = new BOM_01_Rokara_08();
			break;
		case 3891:
			gameEntity = new BoH_Thrall_01();
			break;
		case 3892:
			gameEntity = new BoH_Thrall_02();
			break;
		case 3893:
			gameEntity = new BoH_Thrall_03();
			break;
		case 3894:
			gameEntity = new BoH_Thrall_04();
			break;
		case 3895:
			gameEntity = new BoH_Thrall_05();
			break;
		case 3896:
			gameEntity = new BoH_Thrall_06();
			break;
		case 3897:
			gameEntity = new BoH_Thrall_07();
			break;
		case 3898:
			gameEntity = new BoH_Thrall_08();
			break;
		case 3991:
			gameEntity = new BOM_02_Xyrella_Fight_01();
			break;
		case 3992:
			gameEntity = new BOM_02_Xyrella_Fight_02();
			break;
		case 3993:
			gameEntity = new BOM_02_Xyrella_Fight_03();
			break;
		case 3994:
			gameEntity = new BOM_02_Xyrella_Fight_04();
			break;
		case 3995:
			gameEntity = new BOM_02_Xyrella_Fight_05();
			break;
		case 3996:
			gameEntity = new BOM_02_Xyrella_Fight_06();
			break;
		case 3997:
			gameEntity = new BOM_02_Xyrella_Fight_07();
			break;
		case 3998:
			gameEntity = new BOM_02_Xyrella_Fight_08();
			break;
		case 4074:
			gameEntity = new BOM_03_Guff_Fight_01();
			break;
		case 4075:
			gameEntity = new BOM_03_Guff_Fight_02();
			break;
		case 4076:
			gameEntity = new BOM_03_Guff_Fight_03();
			break;
		case 4077:
			gameEntity = new BOM_03_Guff_Fight_04();
			break;
		case 4078:
			gameEntity = new BOM_03_Guff_Fight_05();
			break;
		case 4079:
			gameEntity = new BOM_03_Guff_Fight_06();
			break;
		case 4080:
			gameEntity = new BOM_03_Guff_Fight_07();
			break;
		case 4081:
			gameEntity = new BOM_03_Guff_Fight_08();
			break;
		case 3923:
			gameEntity = new BoH_Malfurion_01();
			break;
		case 3925:
			gameEntity = new BoH_Malfurion_02();
			break;
		case 3926:
			gameEntity = new BoH_Malfurion_03();
			break;
		case 3927:
			gameEntity = new BoH_Malfurion_04();
			break;
		case 3928:
			gameEntity = new BoH_Malfurion_05();
			break;
		case 3929:
			gameEntity = new BoH_Malfurion_06();
			break;
		case 3932:
			gameEntity = new BoH_Malfurion_07();
			break;
		case 3933:
			gameEntity = new BoH_Malfurion_08();
			break;
		default:
			gameEntity = new StandardGameEntity();
			break;
		}
		gameEntity.OnCreateGame();
		return gameEntity;
	}

	public bool IsAI()
	{
		return GameUtils.IsAIMission(m_missionId);
	}

	public bool WasAI()
	{
		return GameUtils.IsAIMission(m_prevMissionId);
	}

	public bool IsNextAI()
	{
		return GameUtils.IsAIMission(m_nextMissionId);
	}

	public bool IsTutorial()
	{
		return GameUtils.IsTutorialMission(m_missionId);
	}

	public bool WasTutorial()
	{
		return GameUtils.IsTutorialMission(m_prevMissionId);
	}

	public bool IsNextTutorial()
	{
		return GameUtils.IsTutorialMission(m_nextMissionId);
	}

	public bool IsPractice()
	{
		return GameUtils.IsPracticeMission(m_missionId);
	}

	public bool WasPractice()
	{
		return GameUtils.IsPracticeMission(m_prevMissionId);
	}

	public bool IsNextPractice()
	{
		return GameUtils.IsPracticeMission(m_nextMissionId);
	}

	public bool IsClassChallengeMission()
	{
		return GameUtils.IsClassChallengeMission(m_missionId);
	}

	public bool IsHeroicMission()
	{
		return GameUtils.IsHeroicAdventureMission(m_missionId);
	}

	public bool IsExpansionMission()
	{
		return GameUtils.IsExpansionMission(m_missionId);
	}

	public bool WasExpansionMission()
	{
		return GameUtils.IsExpansionMission(m_prevMissionId);
	}

	public bool IsNextExpansionMission()
	{
		return GameUtils.IsExpansionMission(m_nextMissionId);
	}

	public bool IsDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(m_missionId);
	}

	public bool WasDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(m_prevMissionId);
	}

	public bool IsNextDungeonCrawlMission()
	{
		return GameUtils.IsDungeonCrawlMission(m_nextMissionId);
	}

	public bool IsPlay()
	{
		if (!IsRankedPlay())
		{
			return IsUnrankedPlay();
		}
		return true;
	}

	public bool WasPlay()
	{
		if (!WasRankedPlay())
		{
			return WasUnrankedPlay();
		}
		return true;
	}

	public bool IsNextPlay()
	{
		if (!IsNextRankedPlay())
		{
			return IsNextUnrankedPlay();
		}
		return true;
	}

	public bool IsRankedPlay()
	{
		return m_gameType == GameType.GT_RANKED;
	}

	public bool WasRankedPlay()
	{
		return m_prevGameType == GameType.GT_RANKED;
	}

	public bool IsNextRankedPlay()
	{
		return m_nextGameType == GameType.GT_RANKED;
	}

	public bool IsUnrankedPlay()
	{
		return m_gameType == GameType.GT_CASUAL;
	}

	public bool WasUnrankedPlay()
	{
		return m_prevGameType == GameType.GT_CASUAL;
	}

	public bool IsNextUnrankedPlay()
	{
		return m_nextGameType == GameType.GT_CASUAL;
	}

	public bool IsArena()
	{
		return m_gameType == GameType.GT_ARENA;
	}

	public bool WasArena()
	{
		return m_prevGameType == GameType.GT_ARENA;
	}

	public bool IsNextArena()
	{
		return m_nextGameType == GameType.GT_ARENA;
	}

	public bool IsFriendly()
	{
		if (m_gameType != GameType.GT_VS_FRIEND)
		{
			return m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
		}
		return true;
	}

	public bool WasFriendly()
	{
		if (m_prevGameType != GameType.GT_VS_FRIEND)
		{
			return m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
		}
		return true;
	}

	public bool IsNextFriendly()
	{
		if (m_nextGameType != GameType.GT_VS_FRIEND)
		{
			return m_gameType == GameType.GT_FSG_BRAWL_VS_FRIEND;
		}
		return true;
	}

	public bool WasTavernBrawl()
	{
		if (GameUtils.IsTavernBrawlGameType(m_prevGameType))
		{
			return !WasFriendly();
		}
		return false;
	}

	public bool IsTavernBrawl()
	{
		if (GameUtils.IsTavernBrawlGameType(m_gameType))
		{
			return !IsFriendly();
		}
		return false;
	}

	public bool IsNextTavernBrawl()
	{
		if (GameUtils.IsTavernBrawlGameType(m_nextGameType))
		{
			return !IsNextFriendly();
		}
		return false;
	}

	public bool IsBattlegrounds()
	{
		if (m_gameType != GameType.GT_BATTLEGROUNDS)
		{
			return m_gameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
		}
		return true;
	}

	public bool WasBattlegrounds()
	{
		if (m_prevGameType != GameType.GT_BATTLEGROUNDS)
		{
			return m_prevGameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
		}
		return true;
	}

	public bool IsFriendlyBattlegrounds()
	{
		return m_gameType == GameType.GT_BATTLEGROUNDS_FRIENDLY;
	}

	public bool IsStandardFormatType()
	{
		return m_formatType == FormatType.FT_STANDARD;
	}

	public bool IsWildFormatType()
	{
		return m_formatType == FormatType.FT_WILD;
	}

	public bool IsClassicFormatType()
	{
		return m_formatType == FormatType.FT_CLASSIC;
	}

	public bool IsNextWildFormatType()
	{
		return m_nextFormatType == FormatType.FT_WILD;
	}

	public bool IsDuels()
	{
		if (m_gameType != GameType.GT_PVPDR)
		{
			return m_gameType == GameType.GT_PVPDR_PAID;
		}
		return true;
	}

	public bool WasDuels()
	{
		if (m_prevGameType != GameType.GT_PVPDR)
		{
			return m_prevGameType == GameType.GT_PVPDR_PAID;
		}
		return true;
	}

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

	public SceneMgr.Mode GetPostGameSceneMode()
	{
		if (IsSpectator())
		{
			return GetSpectatorPostGameSceneMode();
		}
		SceneMgr.Mode result = SceneMgr.Mode.HUB;
		bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode != FiresideGatheringManager.FiresideGatheringMode.NONE;
		switch (m_gameType)
		{
		case GameType.GT_RANKED:
		case GameType.GT_CASUAL:
			result = SceneMgr.Mode.TOURNAMENT;
			break;
		case GameType.GT_VS_AI:
		{
			if (m_missionId == 3539)
			{
				result = SceneMgr.Mode.BACON;
				break;
			}
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			result = ((tavernBrawlMission == null || tavernBrawlMission.missionId != m_missionId) ? SceneMgr.Mode.ADVENTURE : SceneMgr.Mode.TAVERN_BRAWL);
			break;
		}
		case GameType.GT_ARENA:
			result = SceneMgr.Mode.DRAFT;
			break;
		case GameType.GT_VS_FRIEND:
		case GameType.GT_FSG_BRAWL_VS_FRIEND:
			if (GameUtils.IsFiresideGatheringGameType(m_gameType) && GameUtils.IsTavernBrawlGameType(m_gameType))
			{
				flag = true;
			}
			result = (FriendChallengeMgr.Get().HasChallenge() ? ((flag && GameUtils.IsFiresideGatheringGameType(m_gameType)) ? SceneMgr.Mode.FIRESIDE_GATHERING : ((!FriendChallengeMgr.Get().IsChallengeTavernBrawl()) ? SceneMgr.Mode.FRIENDLY : ((!FriendChallengeMgr.Get().IsChallengeFiresideBrawl()) ? SceneMgr.Mode.TAVERN_BRAWL : SceneMgr.Mode.HUB))) : (flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.HUB));
			break;
		case GameType.GT_FSG_BRAWL_1P_VS_AI:
			result = (flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.HUB);
			break;
		case GameType.GT_TAVERNBRAWL:
		case GameType.GT_FSG_BRAWL:
		case GameType.GT_FSG_BRAWL_2P_COOP:
			result = (flag ? SceneMgr.Mode.FIRESIDE_GATHERING : SceneMgr.Mode.TAVERN_BRAWL);
			if (TavernBrawlManager.Get().CurrentTavernBrawlSeasonEndInSeconds < 10 && !flag)
			{
				result = SceneMgr.Mode.HUB;
			}
			break;
		case GameType.GT_BATTLEGROUNDS:
		case GameType.GT_BATTLEGROUNDS_FRIENDLY:
			result = SceneMgr.Mode.BACON;
			break;
		case GameType.GT_PVPDR_PAID:
		case GameType.GT_PVPDR:
			result = SceneMgr.Mode.PVP_DUNGEON_RUN;
			break;
		}
		return result;
	}

	public SceneMgr.Mode GetPostDisconnectSceneMode()
	{
		if (IsSpectator())
		{
			return GetSpectatorPostGameSceneMode();
		}
		if (IsTutorial())
		{
			return SceneMgr.Mode.INVALID;
		}
		return GetPostGameSceneMode();
	}

	public void PreparePostGameSceneMode(SceneMgr.Mode mode)
	{
		if (mode != SceneMgr.Mode.ADVENTURE || AdventureConfig.Get().CurrentSubScene != 0)
		{
			return;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_missionId);
		if (record == null)
		{
			return;
		}
		int adventureId = record.AdventureId;
		if (adventureId != 0)
		{
			int modeId = record.ModeId;
			if (modeId != 0)
			{
				AdventureConfig.Get().SetSelectedAdventureMode((AdventureDbId)adventureId, (AdventureModeDbId)modeId);
				AdventureConfig.Get().ChangeSubSceneToSelectedAdventure();
				AdventureConfig.Get().SetMission((ScenarioDbId)m_missionId, showDetails: false);
			}
		}
	}

	public bool IsTransitionPopupShown()
	{
		if (m_transitionPopup == null)
		{
			return false;
		}
		return m_transitionPopup.IsShown();
	}

	public TransitionPopup GetTransitionPopup()
	{
		return m_transitionPopup;
	}

	public void UpdatePresence()
	{
		if (!Network.ShouldBeConnectedToAurora() || !Network.IsLoggedIn())
		{
			return;
		}
		if (IsSpectator())
		{
			PresenceMgr presenceMgr = PresenceMgr.Get();
			if (IsTutorial())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_TUTORIAL);
			}
			else if (IsBattlegrounds() || m_missionId == 3539)
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.SPECTATING_GAME_BATTLEGROUNDS);
			}
			else if (IsPractice())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_PRACTICE);
			}
			else if (IsPlay())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_PLAY);
			}
			else if (IsArena())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_ARENA);
			}
			else if (IsFriendly())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_FRIENDLY);
			}
			else if (IsTavernBrawl())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_TAVERN_BRAWL);
			}
			else if (IsDuels())
			{
				presenceMgr.SetStatus(Global.PresenceStatus.SPECTATING_GAME_DUELS);
			}
			else if (IsExpansionMission())
			{
				ScenarioDbId missionId = (ScenarioDbId)m_missionId;
				presenceMgr.SetStatus_SpectatingMission(missionId);
			}
			SpectatorManager.Get().UpdateMySpectatorInfo();
			return;
		}
		if (IsTutorial())
		{
			switch (m_missionId)
			{
			case 3:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.HOGGER);
				break;
			case 4:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.MILLHOUSE);
				break;
			case 181:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.MUKLA);
				break;
			case 201:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.HEMET);
				break;
			case 248:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.ILLIDAN);
				break;
			case 249:
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_GAME, PresenceTutorial.CHO);
				break;
			}
		}
		else if (IsBattlegrounds() || m_missionId == 3539)
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.BATTLEGROUNDS_GAME);
		}
		else if (IsDuels())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_GAME);
		}
		else if (IsPractice())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.PRACTICE_GAME);
		}
		else if (IsPlay())
		{
			if (IsRankedPlay())
			{
				if (IsStandardFormatType())
				{
					PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_RANKED_STANDARD);
				}
				else if (IsWildFormatType())
				{
					PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_RANKED_WILD);
				}
				else if (IsClassicFormatType())
				{
					PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_RANKED_CLASSIC);
				}
				else
				{
					PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_GAME);
				}
			}
			else if (IsStandardFormatType())
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_CASUAL_STANDARD);
			}
			else if (IsWildFormatType())
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_CASUAL_WILD);
			}
			else if (IsClassicFormatType())
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_CASUAL_CLASSIC);
			}
			else
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_GAME);
			}
		}
		else if (IsArena())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.ARENA_GAME);
		}
		else if (IsFriendly())
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
			PresenceMgr.Get().SetStatus(presenceStatus);
		}
		else if (IsTavernBrawl())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_GAME);
		}
		else if (IsExpansionMission())
		{
			ScenarioDbId missionId2 = (ScenarioDbId)m_missionId;
			PresenceMgr.Get().SetStatus_PlayingMission(missionId2);
		}
		SpectatorManager.Get().UpdateMySpectatorInfo();
	}

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
			BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord);
		}
		else if (GameUtils.IsTavernBrawlGameType(gameType) && TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
		{
			int gamesWon = TavernBrawlManager.Get().GamesWon;
			int gamesLost = TavernBrawlManager.Get().GamesLost;
			SessionRecord sessionRecord2 = new SessionRecord();
			sessionRecord2.Wins = (uint)gamesWon;
			sessionRecord2.Losses = (uint)gamesLost;
			sessionRecord2.RunFinished = false;
			sessionRecord2.SessionRecordType = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode != 0) ? SessionRecordType.HEROIC_BRAWL : SessionRecordType.TAVERN_BRAWL);
			BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord2);
		}
	}

	public void SetLastDisplayedPlayerName(int playerId, string name)
	{
		m_lastDisplayedPlayerNames[playerId] = name;
	}

	public string GetLastDisplayedPlayerName(int playerId)
	{
		m_lastDisplayedPlayerNames.TryGetValue(playerId, out var value);
		return value;
	}

	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode == SceneMgr.Mode.GAMEPLAY && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			OnGameEnded();
		}
	}

	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		PreloadTransitionPopup();
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB)
		{
			DestroyTransitionPopup();
		}
		if (mode == SceneMgr.Mode.GAMEPLAY && prevMode != SceneMgr.Mode.GAMEPLAY)
		{
			Screen.sleepTimeout = -1;
		}
		else if (prevMode == SceneMgr.Mode.GAMEPLAY && mode != SceneMgr.Mode.GAMEPLAY && !SpectatorManager.Get().IsInSpectatorMode())
		{
			Screen.sleepTimeout = -2;
		}
	}

	private void OnServerResult()
	{
		if (IsFindingGame())
		{
			ServerResult serverResult = Network.Get().GetServerResult();
			if (serverResult.ResultCode == 1)
			{
				float secondsToWait = Mathf.Max(serverResult.HasRetryDelaySeconds ? serverResult.RetryDelaySeconds : 2f, 0.5f);
				Processor.CancelScheduledCallback(OnServerResult_Retry);
				Processor.ScheduleCallback(secondsToWait, realTime: true, OnServerResult_Retry);
			}
			else if (serverResult.ResultCode == 2)
			{
				OnGameCanceled();
			}
		}
	}

	private void OnServerResult_Retry(object userData)
	{
		Network.Get().RetryGotoGameServer();
	}

	private void ChangeBoardIfNecessary()
	{
		int board = m_gameSetup.Board;
		if (DemoMgr.Get().IsExpoDemo())
		{
			string str = Vars.Key("Demo.ForceBoard").GetStr(null);
			if (str != null)
			{
				board = GameUtils.GetBoardIdFromAssetName(str);
			}
		}
		m_gameSetup.Board = board;
	}

	private void PreloadTransitionPopup()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.TOURNAMENT:
		case SceneMgr.Mode.DRAFT:
		case SceneMgr.Mode.TAVERN_BRAWL:
			LoadTransitionPopup(MATCHING_POPUP_NAME);
			break;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.ADVENTURE:
			LoadTransitionPopup("LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168");
			break;
		case SceneMgr.Mode.FATAL_ERROR:
		case SceneMgr.Mode.CREDITS:
		case SceneMgr.Mode.RESET:
			break;
		}
	}

	private string DetermineTransitionPopupForFindGame(GameType gameType, int missionId)
	{
		if (gameType == GameType.GT_TUTORIAL)
		{
			return null;
		}
		if (GameUtils.IsMatchmadeGameType(gameType, missionId))
		{
			return MATCHING_POPUP_NAME;
		}
		return "LoadingPopup.prefab:ff9266f7c55faa94b9cd0f1371df7168";
	}

	private void LoadTransitionPopup(string prefabPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Error.AddDevFatal("GameMgr.LoadTransitionPopup() - Failed to load {0}", prefabPath);
			return;
		}
		if (m_transitionPopup != null)
		{
			UnityEngine.Object.Destroy(m_transitionPopup.gameObject);
		}
		m_transitionPopup = gameObject.GetComponent<TransitionPopup>();
		m_initialTransitionPopupPos = m_transitionPopup.transform.position;
		m_transitionPopup.RegisterMatchCanceledEvent(OnTransitionPopupCanceled);
		SceneUtils.SetLayer(m_transitionPopup, GameLayer.IgnoreFullScreenEffects);
	}

	private void ShowTransitionPopup(string popupName, int scenarioId)
	{
		Type type = s_transitionPopupNameToType[popupName];
		if (!m_transitionPopup || m_transitionPopup.GetType() != type)
		{
			DestroyTransitionPopup();
			LoadTransitionPopup(popupName);
		}
		if (!m_transitionPopup.IsShown())
		{
			if (Box.Get() != null && Box.Get().GetState() != Box.State.OPEN)
			{
				Vector3 vector = Box.Get().m_Camera.GetCameraPosition(BoxCamera.State.OPENED) - m_initialTransitionPopupPos;
				Vector3 position = Box.Get().GetBoxCamera().m_IgnoreFullscreenEffectsCamera.transform.position - vector;
				m_transitionPopup.transform.position = position;
			}
			AdventureDbId adventureId = GameUtils.GetAdventureId(m_nextMissionId);
			m_transitionPopup.SetAdventureId(adventureId);
			m_transitionPopup.SetFormatType(m_nextFormatType);
			m_transitionPopup.SetGameType(m_nextGameType);
			m_transitionPopup.SetDeckId(m_lastDeckId);
			m_transitionPopup.SetScenarioId(scenarioId);
			m_transitionPopup.Show();
			if (this.OnTransitionPopupShown != null)
			{
				this.OnTransitionPopupShown();
			}
		}
	}

	private void OnTransitionPopupCanceled()
	{
		bool num = Network.Get().IsFindingGame();
		if (num)
		{
			Network.Get().CancelFindGame();
		}
		ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		if (!num)
		{
			ChangeFindGameState(FindGameState.INVALID);
		}
	}

	private void DestroyTransitionPopup()
	{
		if ((bool)m_transitionPopup)
		{
			UnityEngine.Object.Destroy(m_transitionPopup.gameObject);
		}
	}

	private bool GetFriendlyErrorMessage(int errorCode, ref string headerKey, ref string messageKey, ref object[] messageParams)
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
		case 1002008:
			headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
			messageKey = "GLOBAL_ERROR_FIND_GAME_SCENARIO_MISCONFIGURED";
			return true;
		case 1001001:
			headerKey = "GLOBAL_TAVERN_BRAWL";
			messageKey = "GLOBAL_TAVERN_BRAWL_ERROR_NOT_ACTIVE";
			TavernBrawlManager.Get().RefreshServerData();
			return true;
		case 1001000:
			headerKey = "GLOBAL_TAVERN_BRAWL";
			messageKey = "GLOBAL_TAVERN_BRAWL_ERROR_SEASON_INCREMENTED";
			TavernBrawlManager.Get().RefreshServerData();
			return true;
		case 1003005:
			if (m_nextGameType == GameType.GT_ARENA)
			{
				headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
				messageKey = "GLOBAL_ARENA_SEASON_ERROR_NOT_ACTIVE";
				DraftManager.Get().RefreshCurrentSeasonFromServer();
				if (SceneMgr.Get().GetMode() == SceneMgr.Mode.DRAFT)
				{
					Processor.ScheduleCallback(0f, realTime: false, delegate
					{
						Navigation.GoBack();
					});
				}
				return true;
			}
			if (m_nextGameType != GameType.GT_PVPDR && m_nextGameType != GameType.GT_PVPDR_PAID)
			{
				break;
			}
			headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
			messageKey = "GLOBAL_PVPDR_SEASON_ERROR_NOT_ACTIVE";
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN)
			{
				Processor.ScheduleCallback(0f, realTime: false, delegate
				{
					Navigation.GoBack();
				});
			}
			return true;
		case 1002007:
			headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
			messageKey = "GLUE_ERROR_DECK_VALIDATION_WRONG_FORMAT";
			return true;
		case 1002002:
		{
			GameType gameType = GetGameType();
			if (gameType == GameType.GT_UNKNOWN)
			{
				gameType = GetNextGameType();
			}
			if (!GameUtils.IsMatchmadeGameType(gameType))
			{
				headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
				messageKey = "GLUE_ERROR_DECK_RULESET_RULE_VIOLATION";
				return true;
			}
			break;
		}
		case 1003015:
			headerKey = "GLOBAL_ERROR_GENERIC_HEADER";
			messageKey = "GLUE_ERROR_PLAY_GAME_PARTY_NOT_ALLOWED";
			return true;
		}
		return false;
	}

	private void OnGameQueueEvent(QueueEvent queueEvent)
	{
		FindGameState? value = null;
		s_bnetToFindGameResultMap.TryGetValue(queueEvent.EventType, out value);
		if (queueEvent.BnetError != 0)
		{
			m_lastEnterGameError = (uint)queueEvent.BnetError;
		}
		if (!value.HasValue)
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
			object[] messageParams = new object[0];
			if (GetFriendlyErrorMessage(queueEvent.BnetError, ref headerKey, ref messageKey, ref messageParams))
			{
				Error.AddWarningLoc(headerKey, messageKey, messageParams);
				value = FindGameState.BNET_QUEUE_CANCELED;
				HandleGameCanceled();
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
			string text = $"OnGameQueueEvent error={queueEvent.BnetError} {arg}";
			if (HearthstoneApplication.IsInternal())
			{
				Error.AddDevWarning("OnGameQueueEvent", text);
			}
			else
			{
				Log.BattleNet.PrintWarning(text);
			}
		}
		if (queueEvent.EventType == QueueEvent.Type.QUEUE_GAME_STARTED)
		{
			queueEvent.GameServer.Mission = m_nextMissionId;
			ChangeFindGameState(value.Value, queueEvent, queueEvent.GameServer, null);
		}
		else
		{
			ChangeFindGameState(value.Value, queueEvent);
		}
	}

	private void OnGameToJoinNotification()
	{
		GameToConnectNotification gameToConnectNotification = Network.Get().GetGameToConnectNotification();
		ConnectToGame(gameToConnectNotification.Info);
	}

	private void OnGameSetup()
	{
		if (SpectatorManager.Get().IsSpectatingOpposingSide() && m_gameSetup != null)
		{
			return;
		}
		m_gameSetup = Network.Get().GetGameSetupInfo();
		ChangeBoardIfNecessary();
		if (m_findGameState == FindGameState.INVALID && m_gameType == GameType.GT_UNKNOWN)
		{
			Debug.LogError($"GameMgr.OnGameStarting() - Received {GameSetup.PacketID.ID} packet even though we're not looking for a game.");
			return;
		}
		m_lastGameData.Clear();
		m_lastGameData.GameConnectionInfo = m_connectionInfoForGameConnectingTo;
		m_connectionInfoForGameConnectingTo = null;
		m_prevGameType = m_gameType;
		m_gameType = m_nextGameType;
		m_nextGameType = GameType.GT_UNKNOWN;
		m_prevFormatType = m_formatType;
		m_formatType = m_nextFormatType;
		m_nextFormatType = FormatType.FT_UNKNOWN;
		m_prevMissionId = m_missionId;
		m_missionId = m_nextMissionId;
		m_nextMissionId = 0;
		m_brawlLibraryItemId = m_nextBrawlLibraryItemId;
		m_nextBrawlLibraryItemId = 0;
		m_prevReconnectType = m_reconnectType;
		m_reconnectType = m_nextReconnectType;
		m_nextReconnectType = ReconnectType.INVALID;
		m_prevSpectator = m_spectator;
		m_spectator = m_nextSpectator;
		m_nextSpectator = false;
		if (!m_spectator)
		{
			HearthstonePerformance.Get()?.StartPerformanceFlow(new FlowPerformanceGame.GameSetupConfig
			{
				GameType = m_gameType,
				BoardId = m_gameSetup.Board,
				ScenarioId = m_missionId,
				FormatType = m_formatType
			});
		}
		ChangeFindGameState(FindGameState.SERVER_GAME_STARTED);
	}

	private void OnGameCanceled()
	{
		HandleGameCanceled();
		Network network = Network.Get();
		Network.GameCancelInfo gameCancelInfo = network.GetGameCancelInfo();
		network.DisconnectFromGameServer();
		ChangeFindGameState(FindGameState.SERVER_GAME_CANCELED, gameCancelInfo);
	}

	public bool OnBnetError(BnetErrorInfo info, object userData)
	{
		if (info.GetFeature() == BnetFeature.Games)
		{
			BattleNetErrors battleNetErrors = (BattleNetErrors)(m_lastEnterGameError = (uint)info.GetError());
			string arg = null;
			bool flag = false;
			FindGameState state = FindGameState.BNET_ERROR;
			if (battleNetErrors == BattleNetErrors.ERROR_GAME_MASTER_INVALID_FACTORY || battleNetErrors == BattleNetErrors.ERROR_GAME_MASTER_NO_GAME_SERVER || battleNetErrors == BattleNetErrors.ERROR_GAME_MASTER_NO_FACTORY)
			{
				arg = battleNetErrors.ToString();
				flag = true;
			}
			if (!flag)
			{
				string headerKey = "";
				string messageKey = null;
				object[] messageParams = new object[0];
				ReconnectMgr reconnectMgr = ReconnectMgr.Get();
				if (GetFriendlyErrorMessage((int)m_lastEnterGameError, ref headerKey, ref messageKey, ref messageParams) && !reconnectMgr.IsReconnecting() && !reconnectMgr.IsRestoringGameStateFromDatabase())
				{
					Error.AddWarningLoc(headerKey, messageKey, messageParams);
					ErrorCode lastEnterGameError = (ErrorCode)m_lastEnterGameError;
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
				string text = $"GameMgr.OnBnetError() - received error {m_lastEnterGameError} {arg}";
				Log.BattleNet.PrintError(text);
				if (!Log.BattleNet.CanPrint(LogTarget.CONSOLE, Log.LogLevel.Error, verbose: false))
				{
					Debug.LogError(string.Format("[{0}] {1}", "BattleNet", text));
				}
				HandleGameCanceled();
				ChangeFindGameState(state);
				return true;
			}
		}
		return false;
	}

	private void HandleGameCanceled()
	{
		m_nextGameType = GameType.GT_UNKNOWN;
		m_nextFormatType = FormatType.FT_UNKNOWN;
		m_nextMissionId = 0;
		m_nextBrawlLibraryItemId = 0;
		m_nextReconnectType = ReconnectType.INVALID;
		m_nextSpectator = false;
		Network.Get().ClearLastGameServerJoined();
	}

	private bool OnReconnectTimeout(object userData)
	{
		HandleGameCanceled();
		ChangeFindGameState(FindGameState.CLIENT_CANCELED);
		ChangeFindGameState(FindGameState.INVALID);
		return false;
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		if (IsFindingGame())
		{
			ChangeFindGameState(FindGameState.CLIENT_CANCELED);
			ChangeFindGameState(FindGameState.INVALID);
			DialogManager.Get().ShowReconnectHelperDialog();
		}
	}

	private bool ChangeFindGameState(FindGameState state)
	{
		return ChangeFindGameState(state, null, null, null);
	}

	private bool ChangeFindGameState(FindGameState state, QueueEvent queueEvent)
	{
		return ChangeFindGameState(state, queueEvent, null, null);
	}

	private bool ChangeFindGameState(FindGameState state, GameServerInfo serverInfo)
	{
		return ChangeFindGameState(state, null, serverInfo, null);
	}

	private bool ChangeFindGameState(FindGameState state, Network.GameCancelInfo cancelInfo)
	{
		return ChangeFindGameState(state, null, null, cancelInfo);
	}

	private bool ChangeFindGameState(FindGameState state, QueueEvent queueEvent, GameServerInfo serverInfo, Network.GameCancelInfo cancelInfo)
	{
		FindGameState findGameState = m_findGameState;
		uint lastEnterGameError = m_lastEnterGameError;
		m_findGameState = state;
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
			Network.Get().RemoveGameServerDisconnectEventListener(OnGameServerDisconnect);
			break;
		}
		bool num = FireFindGameEvent(findGameEventData);
		if (!num)
		{
			DoDefaultFindGameEventBehavior(findGameEventData);
		}
		FinalizeState(findGameEventData);
		if (findGameState != state)
		{
			Network.Get().OnFindGameStateChanged(findGameState, state, lastEnterGameError);
		}
		return num;
	}

	private bool FireFindGameEvent(FindGameEventData eventData)
	{
		bool flag = false;
		FindGameListener[] array = m_findGameListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			flag = array[i].Fire(eventData) || flag;
		}
		return flag;
	}

	private void DoDefaultFindGameEventBehavior(FindGameEventData eventData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_ERROR:
		{
			ReconnectMgr reconnectMgr = ReconnectMgr.Get();
			if (!reconnectMgr.IsReconnecting() && !reconnectMgr.IsRestoringGameStateFromDatabase())
			{
				Error.AddWarningLoc("GLOBAL_ERROR_GENERIC_HEADER", "GLOBAL_ERROR_GAME_DENIED");
			}
			HideTransitionPopup();
			break;
		}
		case FindGameState.BNET_QUEUE_CANCELED:
			HideTransitionPopup();
			break;
		case FindGameState.SERVER_GAME_CONNECTING:
			Network.Get().GotoGameServer(eventData.m_gameServer, IsNextReconnect());
			break;
		case FindGameState.SERVER_GAME_STARTED:
			if (Box.Get() != null)
			{
				LoadingScreen.Get().SetFreezeFrameCamera(Box.Get().GetCamera());
				LoadingScreen.Get().SetTransitionAudioListener(Box.Get().GetAudioListener());
			}
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				if (!SpectatorManager.Get().IsSpectatingOpposingSide())
				{
					SceneMgr.Get().ReloadMode();
				}
			}
			else
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAMEPLAY);
			}
			break;
		case FindGameState.SERVER_GAME_CANCELED:
			if (eventData.m_cancelInfo != null)
			{
				Network.GameCancelInfo.Reason cancelReason = eventData.m_cancelInfo.CancelReason;
				if ((uint)(cancelReason - 1) <= 2u)
				{
					Error.AddWarningLoc("GLOBAL_ERROR_GENERIC_HEADER", "GLOBAL_ERROR_GAME_OPPONENT_TIMEOUT");
				}
				else
				{
					Error.AddDevWarning("GAME ERROR", "The Game Server canceled the game. Error: {0}", eventData.m_cancelInfo.CancelReason);
				}
			}
			HideTransitionPopup();
			break;
		case FindGameState.CLIENT_CANCELED:
			HideTransitionPopup();
			break;
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
			break;
		}
	}

	private void FinalizeState(FindGameEventData eventData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_STARTED:
		case FindGameState.SERVER_GAME_CANCELED:
			ChangeFindGameState(FindGameState.INVALID);
			break;
		case FindGameState.BNET_QUEUE_ENTERED:
		case FindGameState.BNET_QUEUE_DELAYED:
		case FindGameState.BNET_QUEUE_UPDATED:
		case FindGameState.SERVER_GAME_CONNECTING:
			break;
		}
	}

	private void OnGameEnded()
	{
		if (!m_spectator)
		{
			HearthstonePerformance.Get()?.StopCurrentFlow();
		}
		m_prevGameType = m_gameType;
		m_gameType = GameType.GT_UNKNOWN;
		m_prevFormatType = m_formatType;
		m_formatType = FormatType.FT_UNKNOWN;
		m_prevMissionId = m_missionId;
		m_missionId = 0;
		m_brawlLibraryItemId = 0;
		m_prevReconnectType = m_reconnectType;
		m_reconnectType = ReconnectType.INVALID;
		m_prevSpectator = m_spectator;
		m_spectator = false;
		m_lastEnterGameError = 0u;
		m_pendingAutoConcede = false;
		m_gameSetup = null;
		m_lastDisplayedPlayerNames.Clear();
	}
}
