using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Assets;
using PegasusGame;
using UnityEngine;

public class GameState
{
	public enum ResponseMode
	{
		NONE,
		OPTION,
		SUB_OPTION,
		OPTION_TARGET,
		CHOICE
	}

	public enum CreateGamePhase
	{
		INVALID,
		CREATING,
		CREATED
	}

	public delegate void GameStateInitializedCallback(GameState instance, object userData);

	public delegate void CreateGameCallback(CreateGamePhase phase, object userData);

	public delegate void OptionsReceivedCallback(object userData);

	public delegate void OptionsSentCallback(Network.Options.Option option, object userData);

	public delegate void OptionRejectedCallback(Network.Options.Option option, object userData);

	public delegate void EntityChoicesReceivedCallback(Network.EntityChoices choices, PowerTaskList preChoiceTaskList, object userData);

	public delegate bool EntitiesChosenReceivedCallback(Network.EntitiesChosen chosen, object userData);

	public delegate void CurrentPlayerChangedCallback(Player player, object userData);

	public delegate void TurnChangedCallback(int oldTurn, int newTurn, object userData);

	public delegate void FriendlyTurnStartedCallback(object userData);

	public delegate void TurnTimerUpdateCallback(TurnTimerUpdate update, object userData);

	public delegate void SpectatorNotifyEventCallback(SpectatorNotify notify, object userData);

	public delegate void GameOverCallback(TAG_PLAYSTATE playState, object userData);

	public delegate void HeroChangedCallback(Player player, object userData);

	public delegate void CantPlayCallback(Entity entity, object userData);

	private delegate void AppendBlockingServerItemCallback<T>(StringBuilder builder, T item);

	private class SelectedOption
	{
		public int m_main = -1;

		public int m_sub = -1;

		public int m_target;

		public int m_position;

		public void Clear()
		{
			m_main = -1;
			m_sub = -1;
			m_target = 0;
			m_position = 0;
		}

		public void CopyFrom(SelectedOption original)
		{
			m_main = original.m_main;
			m_sub = original.m_sub;
			m_target = original.m_target;
			m_position = original.m_position;
		}
	}

	private class QueuedChoice
	{
		public enum PacketType
		{
			ENTITY_CHOICES,
			ENTITIES_CHOSEN
		}

		public PacketType m_type;

		public object m_packet;

		public object m_eventData;
	}

	private class GameStateInitializedListener : EventListener<GameStateInitializedCallback>
	{
		public void Fire(GameState instance)
		{
			m_callback(instance, m_userData);
		}
	}

	private class CreateGameListener : EventListener<CreateGameCallback>
	{
		public void Fire(CreateGamePhase phase)
		{
			m_callback(phase, m_userData);
		}
	}

	private class OptionsReceivedListener : EventListener<OptionsReceivedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	private class OptionsSentListener : EventListener<OptionsSentCallback>
	{
		public void Fire(Network.Options.Option option)
		{
			m_callback(option, m_userData);
		}
	}

	private class OptionRejectedListener : EventListener<OptionRejectedCallback>
	{
		public void Fire(Network.Options.Option option)
		{
			m_callback(option, m_userData);
		}
	}

	private class EntityChoicesReceivedListener : EventListener<EntityChoicesReceivedCallback>
	{
		public void Fire(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
		{
			m_callback(choices, preChoiceTaskList, m_userData);
		}
	}

	private class EntitiesChosenReceivedListener : EventListener<EntitiesChosenReceivedCallback>
	{
		public bool Fire(Network.EntitiesChosen chosen)
		{
			return m_callback(chosen, m_userData);
		}
	}

	private class CurrentPlayerChangedListener : EventListener<CurrentPlayerChangedCallback>
	{
		public void Fire(Player player)
		{
			m_callback(player, m_userData);
		}
	}

	private class TurnChangedListener : EventListener<TurnChangedCallback>
	{
		public void Fire(int oldTurn, int newTurn)
		{
			m_callback(oldTurn, newTurn, m_userData);
		}
	}

	private class FriendlyTurnStartedListener : EventListener<FriendlyTurnStartedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	private class TurnTimerUpdateListener : EventListener<TurnTimerUpdateCallback>
	{
		public void Fire(TurnTimerUpdate update)
		{
			m_callback(update, m_userData);
		}
	}

	private class SpectatorNotifyListener : EventListener<SpectatorNotifyEventCallback>
	{
		public void Fire(SpectatorNotify notify)
		{
			m_callback(notify, m_userData);
		}
	}

	private class GameOverListener : EventListener<GameOverCallback>
	{
		public void Fire(TAG_PLAYSTATE playState)
		{
			m_callback(playState, m_userData);
		}
	}

	private class HeroChangedListener : EventListener<HeroChangedCallback>
	{
		public void Fire(Player player)
		{
			m_callback(player, m_userData);
		}
	}

	private class CantPlayListener : EventListener<CantPlayCallback>
	{
		public void Fire(Entity entity)
		{
			m_callback(entity, m_userData);
		}
	}

	public const int DEFAULT_SUBOPTION = -1;

	public const int RACE_COUNT_IN_BATTLEGROUNDS_EXCLUDING_AMALGAM = 5;

	public const int RACE_COUNT_MISSING_BATTLEGROUNDS = 3;

	public const int TOTAL_RACES = 8;

	private const string INDENT = "    ";

	private const float BLOCK_REPORT_START_SEC = 10f;

	private const float BLOCK_REPORT_INTERVAL_SEC = 3f;

	private static GameState s_instance;

	private static List<GameStateInitializedListener> s_gameStateInitializedListeners;

	private readonly TAG_RACE[] m_allRaces = new TAG_RACE[8]
	{
		TAG_RACE.MURLOC,
		TAG_RACE.DEMON,
		TAG_RACE.MECHANICAL,
		TAG_RACE.ELEMENTAL,
		TAG_RACE.PIRATE,
		TAG_RACE.PET,
		TAG_RACE.DRAGON,
		TAG_RACE.QUILBOAR
	};

	private TAG_RACE[] m_availableRacesInBattlegroundsExcludingAmalgam = new TAG_RACE[5];

	private TAG_RACE[] m_missingRacesInBattlegrounds = new TAG_RACE[3];

	private Map<int, Entity> m_entityMap = new Map<int, Entity>();

	private Map<int, Player> m_playerMap = new Map<int, Player>();

	private Map<int, SharedPlayerInfo> m_playerInfoMap = new Map<int, SharedPlayerInfo>();

	private GameEntity m_gameEntity;

	private Queue<Entity> m_removedFromGameEntities = new Queue<Entity>();

	private HashSet<int> m_removedFromGameEntityLog = new HashSet<int>();

	private CreateGamePhase m_createGamePhase;

	private Network.HistResetGame m_realTimeResetGame;

	private Network.HistTagChange m_realTimeGameOverTagChange;

	private bool m_gameOver;

	private bool m_concedeRequested;

	private bool m_restartRequested;

	private int m_maxSecretZoneSizePerPlayer;

	private int m_maxSecretsPerPlayer;

	private int m_maxQuestsPerPlayer;

	private int m_maxFriendlyMinionsPerPlayer;

	private ResponseMode m_responseMode;

	private Map<int, Network.EntityChoices> m_choicesMap = new Map<int, Network.EntityChoices>();

	private Queue<QueuedChoice> m_queuedChoices = new Queue<QueuedChoice>();

	private List<Entity> m_chosenEntities = new List<Entity>();

	private Network.Options m_options;

	private SelectedOption m_selectedOption = new SelectedOption();

	private Network.Options m_lastOptions;

	private SelectedOption m_lastSelectedOption;

	private bool m_coinHasSpawned;

	private Card m_friendlyCardBeingDrawn;

	private Card m_opponentCardBeingDrawn;

	private int m_lastTurnRemindedOfFullHand;

	private bool m_usingFastActorTriggers;

	private List<CreateGameListener> m_createGameListeners = new List<CreateGameListener>();

	private List<OptionsReceivedListener> m_optionsReceivedListeners = new List<OptionsReceivedListener>();

	private List<OptionsSentListener> m_optionsSentListeners = new List<OptionsSentListener>();

	private List<OptionRejectedListener> m_optionRejectedListeners = new List<OptionRejectedListener>();

	private List<EntityChoicesReceivedListener> m_entityChoicesReceivedListeners = new List<EntityChoicesReceivedListener>();

	private List<EntitiesChosenReceivedListener> m_entitiesChosenReceivedListeners = new List<EntitiesChosenReceivedListener>();

	private List<CurrentPlayerChangedListener> m_currentPlayerChangedListeners = new List<CurrentPlayerChangedListener>();

	private List<FriendlyTurnStartedListener> m_friendlyTurnStartedListeners = new List<FriendlyTurnStartedListener>();

	private List<TurnChangedListener> m_turnChangedListeners = new List<TurnChangedListener>();

	private List<SpectatorNotifyListener> m_spectatorNotifyListeners = new List<SpectatorNotifyListener>();

	private List<GameOverListener> m_gameOverListeners = new List<GameOverListener>();

	private List<HeroChangedListener> m_heroChangedListeners = new List<HeroChangedListener>();

	private List<CantPlayListener> m_cantPlayListeners = new List<CantPlayListener>();

	private PowerProcessor m_powerProcessor = new PowerProcessor();

	private float m_reconnectIfStuckTimer;

	private float m_lastBlockedReportTimestamp;

	private bool m_busy;

	private bool m_mulliganBusy;

	private List<Spell> m_serverBlockingSpells = new List<Spell>();

	private List<SpellController> m_serverBlockingSpellControllers = new List<SpellController>();

	private List<TurnTimerUpdateListener> m_turnTimerUpdateListeners = new List<TurnTimerUpdateListener>();

	private List<TurnTimerUpdateListener> m_mulliganTimerUpdateListeners = new List<TurnTimerUpdateListener>();

	private Map<int, TurnTimerUpdate> m_turnTimerUpdates = new Map<int, TurnTimerUpdate>();

	private AlertPopup m_waitForOpponentReconnectPopup;

	private AlertPopup.PopupInfo m_waitForOpponentReconnectPopupInfo;

	private int m_friendlyDrawCounter;

	private int m_opponentDrawCounter;

	private GameStateFrameTimeTracker m_lostFrameTimeTracker = CreateFrameTimeTracker();

	private GameStateSlushTimeTracker m_lostSlushTimeTracker = CreateSlushTimeTracker();

	private float m_clientLostTimeCatchUpThreshold;

	private bool m_useSlushTimeCatchUp;

	private bool m_restrictClientLostTimeCatchUpToLowEndDevices;

	private bool m_allowDeferredPowers = true;

	private bool m_allowBatchedPowers = true;

	private bool m_allowDiamondCards = true;

	private string m_battlegroundMinionPool = "";

	private string m_battlegroundDenyList = "";

	private bool m_printBattlegroundMinionPoolOnUpdate;

	private bool m_printBattlegroundDenyListOnUpdate;

	public static GameState Get()
	{
		return s_instance;
	}

	public static GameState Initialize()
	{
		if (s_instance == null)
		{
			s_instance = new GameState();
			FireGameStateInitializedEvent();
			s_instance.m_powerProcessor.AddTaskEventListener(s_instance.HandleTaskTimeEvent);
		}
		return s_instance;
	}

	public static void Shutdown()
	{
		if (s_instance != null)
		{
			if (SoundManager.Get() != null)
			{
				SoundManager.Get().DestroyAll(Global.SoundCategory.FX);
			}
			s_instance.ClearEntityMap();
			s_instance.HideWaitForOpponentReconnectPopup();
			s_instance.m_powerProcessor.RemoveTaskEventListener(s_instance.HandleTaskTimeEvent);
			s_instance = null;
		}
	}

	public void Update()
	{
		m_lostFrameTimeTracker.Update();
		m_lostSlushTimeTracker.Update();
		if (!CheckReconnectIfStuck())
		{
			m_powerProcessor.ProcessPowerQueue();
			m_lostFrameTimeTracker.AdjustAccruedLostTime(-0.016667f);
		}
	}

	public PowerProcessor GetPowerProcessor()
	{
		return m_powerProcessor;
	}

	public IGameStateTimeTracker GetTimeTracker()
	{
		if (m_useSlushTimeCatchUp)
		{
			return GetSlushTimeTracker();
		}
		return GetFrameTimeTracker();
	}

	public GameStateSlushTimeTracker GetSlushTimeTracker()
	{
		return m_lostSlushTimeTracker;
	}

	public GameStateFrameTimeTracker GetFrameTimeTracker()
	{
		return m_lostFrameTimeTracker;
	}

	public void HandleTaskTimeEvent(float diff)
	{
		m_lostSlushTimeTracker.AdjustAccruedLostTime(diff);
	}

	private static GameStateSlushTimeTracker CreateSlushTimeTracker()
	{
		return new GameStateSlushTimeTracker();
	}

	private static GameStateFrameTimeTracker CreateFrameTimeTracker()
	{
		return new GameStateFrameTimeTracker(15, 0.033333f);
	}

	public bool AreLostTimeGuardianConditionsMet()
	{
		if (m_clientLostTimeCatchUpThreshold > 0f)
		{
			if (m_restrictClientLostTimeCatchUpToLowEndDevices)
			{
				return PlatformSettings.Memory != MemoryCategory.High;
			}
			return true;
		}
		return false;
	}

	public bool AllowDeferredPowers()
	{
		return m_allowDeferredPowers;
	}

	public bool AllowBatchedPowers()
	{
		return m_allowBatchedPowers;
	}

	public bool AllowDiamondCards()
	{
		return m_allowDiamondCards;
	}

	public bool PrintBattlegroundMinionPoolOnUpdate()
	{
		return m_printBattlegroundMinionPoolOnUpdate;
	}

	public bool PrintBattlegroundDenyListOnUpdate()
	{
		return m_printBattlegroundDenyListOnUpdate;
	}

	public void SetPrintBattlegroundMinionPoolOnUpdate(bool isPrinting)
	{
		m_printBattlegroundMinionPoolOnUpdate = isPrinting;
	}

	public void SetPrintBattlegroundDenyListOnUpdate(bool isPrinting)
	{
		m_printBattlegroundDenyListOnUpdate = isPrinting;
	}

	public string BattlegroundDenyList()
	{
		return m_battlegroundDenyList;
	}

	public string BattlegroundMinionPool()
	{
		return m_battlegroundMinionPool;
	}

	public bool HasPowersToProcess()
	{
		if (m_powerProcessor.GetCurrentTaskList() != null)
		{
			return true;
		}
		if (m_powerProcessor.GetPowerQueue().Count > 0)
		{
			return true;
		}
		return false;
	}

	public Entity GetEntity(int id)
	{
		m_entityMap.TryGetValue(id, out var value);
		return value;
	}

	public Player GetPlayer(int id)
	{
		m_playerMap.TryGetValue(id, out var value);
		return value;
	}

	public GameEntity GetGameEntity()
	{
		return m_gameEntity;
	}

	public bool GetBooleanGameOption(GameEntityOption option)
	{
		return (m_gameEntity?.GetGameOptions())?.GetBooleanOption(option) ?? false;
	}

	public string GetStringGameOption(GameEntityOption option)
	{
		return m_gameEntity?.GetGameOptions()?.GetStringOption(option);
	}

	[Conditional("UNITY_EDITOR")]
	public void DebugSetGameEntity(GameEntity gameEntity)
	{
		m_gameEntity = gameEntity;
	}

	public bool WasGameCreated()
	{
		return m_gameEntity != null;
	}

	public Player GetPlayerBySide(Player.Side playerSide)
	{
		foreach (Player value in m_playerMap.Values)
		{
			if (value.GetSide() == playerSide)
			{
				return value;
			}
		}
		return null;
	}

	public Player GetLocalSidePlayer()
	{
		bool isSpectatingOrWatching = SpectatorManager.Get().IsSpectatingOrWatching;
		foreach (Player value in m_playerMap.Values)
		{
			if (value.IsLocalUser())
			{
				return value;
			}
			if (isSpectatingOrWatching && value.GetGameAccountId() == SpectatorManager.Get().GetSpectateeFriendlySide())
			{
				return value;
			}
		}
		return null;
	}

	public int GetFriendlySideTeamId()
	{
		Player localSidePlayer = GetLocalSidePlayer();
		if (localSidePlayer != null)
		{
			int teamId = localSidePlayer.GetTeamId();
			if (teamId <= 0)
			{
				return localSidePlayer.GetPlayerId();
			}
			return teamId;
		}
		return 0;
	}

	public Player GetFriendlySidePlayer()
	{
		foreach (Player value in m_playerMap.Values)
		{
			if (value.IsFriendlySide() && value.IsTeamLeader())
			{
				return value;
			}
		}
		return null;
	}

	public void HideZzzEffects()
	{
		Player friendlySidePlayer = GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			ZonePlay battlefieldZone = friendlySidePlayer.GetBattlefieldZone();
			if (battlefieldZone != null)
			{
				battlefieldZone.HideCardZzzEffects();
			}
		}
		Player opposingSidePlayer = GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			ZonePlay battlefieldZone2 = opposingSidePlayer.GetBattlefieldZone();
			if (battlefieldZone2 != null)
			{
				battlefieldZone2.HideCardZzzEffects();
			}
		}
	}

	public void UnhideZzzEffects()
	{
		Player friendlySidePlayer = GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			ZonePlay battlefieldZone = friendlySidePlayer.GetBattlefieldZone();
			if (battlefieldZone != null)
			{
				battlefieldZone.UnhideCardZzzEffects();
			}
		}
		Player opposingSidePlayer = GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			ZonePlay battlefieldZone2 = opposingSidePlayer.GetBattlefieldZone();
			if (battlefieldZone2 != null)
			{
				battlefieldZone2.UnhideCardZzzEffects();
			}
		}
	}

	public Player GetOpposingSidePlayer()
	{
		foreach (Player value in m_playerMap.Values)
		{
			if (value.IsOpposingSide() && value.IsTeamLeader())
			{
				return value;
			}
		}
		return null;
	}

	public int GetFriendlyPlayerId()
	{
		return GetFriendlySidePlayer()?.GetPlayerId() ?? 0;
	}

	public int GetOpposingPlayerId()
	{
		return GetOpposingSidePlayer()?.GetPlayerId() ?? 0;
	}

	public bool IsFriendlySidePlayerTurn()
	{
		return GetFriendlySidePlayer()?.IsCurrentPlayer() ?? false;
	}

	public bool IsLocalSidePlayerTurn()
	{
		Player localSidePlayer = GetLocalSidePlayer();
		if (localSidePlayer == null)
		{
			return false;
		}
		if (!localSidePlayer.IsTeamLeader())
		{
			return true;
		}
		return localSidePlayer.IsCurrentPlayer();
	}

	public Player GetCurrentPlayer()
	{
		foreach (Player value in m_playerMap.Values)
		{
			if (value.IsCurrentPlayer())
			{
				return value;
			}
		}
		return null;
	}

	public bool IsCurrentPlayerRevealed()
	{
		return GetCurrentPlayer()?.IsRevealed() ?? false;
	}

	public Player GetFirstOpponentPlayer(Player player)
	{
		foreach (Player value in m_playerMap.Values)
		{
			if (value.GetSide() != player.GetSide())
			{
				return value;
			}
		}
		return null;
	}

	public int GetNumFriendlyMinionsInPlay(bool includeUntouchables)
	{
		return GetNumMinionsInPlay(GetFriendlySidePlayer(), includeUntouchables);
	}

	public int GetNumEnemyMinionsInPlay(bool includeUntouchables)
	{
		return GetNumMinionsInPlay(GetOpposingSidePlayer(), includeUntouchables);
	}

	private int GetNumMinionsInPlay(Player player, bool includeUntouchables)
	{
		if (player == null)
		{
			return 0;
		}
		int num = 0;
		foreach (Card card in player.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetController() == player && entity.IsMinion() && (includeUntouchables || !entity.HasTag(GAME_TAG.UNTOUCHABLE)))
			{
				num++;
			}
		}
		return num;
	}

	public int GetTurn()
	{
		if (m_gameEntity != null)
		{
			return m_gameEntity.GetTag(GAME_TAG.TURN);
		}
		return 0;
	}

	public bool IsTagBlockingInput()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		return m_gameEntity.HasTag(GAME_TAG.BLOCK_ALL_INPUT);
	}

	public bool IsResponsePacketBlocked()
	{
		if (IsMulliganManagerIntroActive())
		{
			return true;
		}
		if (m_gameEntity.IsMulliganActiveRealTime())
		{
			return false;
		}
		if (IsMulliganManagerActive())
		{
			return true;
		}
		if (!IsCurrentPlayerRevealed() && !IsLocalSidePlayerTurn())
		{
			return true;
		}
		if (!m_gameEntity.IsCurrentTurnRealTime())
		{
			return true;
		}
		if (IsTurnStartManagerBlockingInput())
		{
			return true;
		}
		if (IsTagBlockingInput())
		{
			return true;
		}
		if (IsResetGamePending())
		{
			return false;
		}
		switch (m_responseMode)
		{
		case ResponseMode.NONE:
			return true;
		case ResponseMode.OPTION:
		case ResponseMode.SUB_OPTION:
		case ResponseMode.OPTION_TARGET:
			if (m_options == null)
			{
				return true;
			}
			break;
		case ResponseMode.CHOICE:
			if (GetFriendlyEntityChoices() == null)
			{
				return true;
			}
			break;
		default:
			UnityEngine.Debug.LogWarning($"GameState.IsResponsePacketBlocked() - unhandled response mode {m_responseMode}");
			break;
		}
		return false;
	}

	public TAG_RACE[] GetAvailableRacesInBattlegroundsExcludingAmalgam()
	{
		return m_availableRacesInBattlegroundsExcludingAmalgam;
	}

	public TAG_RACE[] GetMissingRacesInBattlegrounds()
	{
		if (m_missingRacesInBattlegrounds[0] == TAG_RACE.INVALID && m_availableRacesInBattlegroundsExcludingAmalgam[0] != 0)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				TAG_RACE tAG_RACE = m_allRaces[i];
				if (!m_availableRacesInBattlegroundsExcludingAmalgam.Contains(tAG_RACE))
				{
					m_missingRacesInBattlegrounds[num] = tAG_RACE;
					num++;
				}
			}
		}
		return m_missingRacesInBattlegrounds;
	}

	public Map<int, Entity> GetEntityMap()
	{
		return m_entityMap;
	}

	public Map<int, Player> GetPlayerMap()
	{
		return m_playerMap;
	}

	public Map<int, SharedPlayerInfo> GetPlayerInfoMap()
	{
		return m_playerInfoMap;
	}

	public void AddPlayerInfo(SharedPlayerInfo playerInfo)
	{
		m_playerInfoMap.Add(playerInfo.GetPlayerId(), playerInfo);
	}

	public void AddPlayer(Player player)
	{
		m_playerMap.Add(player.GetPlayerId(), player);
		m_entityMap.Add(player.GetEntityId(), player);
	}

	public void RemovePlayer(Player player)
	{
		player.Destroy();
		m_playerMap.Remove(player.GetPlayerId());
		m_entityMap.Remove(player.GetEntityId());
	}

	public void AddEntity(Entity entity)
	{
		m_entityMap.Add(entity.GetEntityId(), entity);
	}

	public void RemoveEntity(Entity entity)
	{
		if (entity.IsPlayer())
		{
			RemovePlayer(entity as Player);
			return;
		}
		if (entity.IsGame())
		{
			m_gameEntity = null;
			return;
		}
		if (entity.IsAttached())
		{
			GetEntity(entity.GetAttached())?.RemoveAttachment(entity);
		}
		if (entity.IsHero())
		{
			Player player = GetPlayer(entity.GetControllerId());
			if (player != null && player.GetHero() == entity)
			{
				player.SetHero(null);
			}
		}
		else if (entity.IsHeroPower())
		{
			Player player2 = GetPlayer(entity.GetControllerId());
			if (player2 != null && player2.GetHeroPower() == entity)
			{
				player2.SetHeroPower(null);
			}
		}
		entity.Destroy();
		m_entityMap.Remove(entity.GetEntityId());
	}

	public void RemoveQueuedEntitiesFromGame()
	{
		if (m_removedFromGameEntities.Count == 0)
		{
			return;
		}
		bool flag = false;
		do
		{
			Entity entity = m_removedFromGameEntities.Peek();
			flag = AttemptRemovalOfQueuedEntity(entity);
			if (flag)
			{
				m_removedFromGameEntities.Dequeue();
				m_removedFromGameEntityLog.Add(entity.GetEntityId());
			}
		}
		while (flag && m_removedFromGameEntities.Count > 0);
	}

	public bool EntityRemovedFromGame(int entityId)
	{
		return m_removedFromGameEntityLog.Contains(entityId);
	}

	private bool AttemptRemovalOfQueuedEntity(Entity entity)
	{
		if (GetPowerProcessor().EntityHasPendingTasks(entity))
		{
			return false;
		}
		Get().RemoveEntity(entity);
		return true;
	}

	public int GetMaxSecretZoneSizePerPlayer()
	{
		return m_maxSecretZoneSizePerPlayer;
	}

	public int GetMaxSecretsPerPlayer()
	{
		return m_maxSecretsPerPlayer;
	}

	public int GetMaxQuestsPerPlayer()
	{
		return m_maxQuestsPerPlayer;
	}

	public int GetMaxFriendlyMinionsPerPlayer()
	{
		return m_maxFriendlyMinionsPerPlayer;
	}

	public bool IsBusy()
	{
		return m_busy;
	}

	public void SetBusy(bool busy)
	{
		m_busy = busy;
	}

	public bool IsMulliganBusy()
	{
		return m_mulliganBusy;
	}

	public void SetMulliganBusy(bool busy)
	{
		m_mulliganBusy = busy;
	}

	public bool IsMulliganManagerActive()
	{
		if (MulliganManager.Get() == null)
		{
			return false;
		}
		return MulliganManager.Get().IsMulliganActive();
	}

	public bool IsMulliganManagerIntroActive()
	{
		if (MulliganManager.Get() == null)
		{
			return false;
		}
		return MulliganManager.Get().IsMulliganIntroActive();
	}

	public bool IsTurnStartManagerActive()
	{
		if (TurnStartManager.Get() == null)
		{
			return false;
		}
		return TurnStartManager.Get().IsListeningForTurnEvents();
	}

	public bool IsTurnStartManagerBlockingInput()
	{
		if (TurnStartManager.Get() == null)
		{
			return false;
		}
		return TurnStartManager.Get().IsBlockingInput();
	}

	public bool HasTheCoinBeenSpawned()
	{
		return m_coinHasSpawned;
	}

	public void NotifyOfCoinSpawn()
	{
		m_coinHasSpawned = true;
	}

	public bool IsBeginPhase()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		return GameUtils.IsBeginPhase(m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	public bool IsPastBeginPhase()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		return GameUtils.IsPastBeginPhase(m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	public bool IsMainPhase()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		return GameUtils.IsMainPhase(m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	public bool IsMulliganPhase()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		return m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP) == TAG_STEP.BEGIN_MULLIGAN;
	}

	public bool IsMulliganPhasePending()
	{
		if (m_gameEntity == null)
		{
			return false;
		}
		if (m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.NEXT_STEP) == TAG_STEP.BEGIN_MULLIGAN)
		{
			return true;
		}
		bool foundMulliganStep = false;
		int gameEntityId = m_gameEntity.GetEntityId();
		m_powerProcessor.ForEachTaskList(delegate(int queueIndex, PowerTaskList taskList)
		{
			List<PowerTask> taskList2 = taskList.GetTaskList();
			for (int i = 0; i < taskList2.Count; i++)
			{
				Network.HistTagChange histTagChange = taskList2[i].GetPower() as Network.HistTagChange;
				if (histTagChange != null && histTagChange.Entity == gameEntityId)
				{
					GAME_TAG tag = (GAME_TAG)histTagChange.Tag;
					if ((tag == GAME_TAG.STEP || tag == GAME_TAG.NEXT_STEP) && histTagChange.Value == 4)
					{
						foundMulliganStep = true;
						break;
					}
				}
			}
		});
		return foundMulliganStep;
	}

	public bool IsMulliganPhaseNowOrPending()
	{
		if (IsMulliganPhase())
		{
			return true;
		}
		if (IsMulliganPhasePending())
		{
			return true;
		}
		return false;
	}

	public bool IsResetGamePending()
	{
		return m_realTimeResetGame != null;
	}

	public CreateGamePhase GetCreateGamePhase()
	{
		return m_createGamePhase;
	}

	public bool IsGameCreating()
	{
		return m_createGamePhase == CreateGamePhase.CREATING;
	}

	public bool IsGameCreated()
	{
		return m_createGamePhase == CreateGamePhase.CREATED;
	}

	public bool IsGameCreatedOrCreating()
	{
		if (!IsGameCreated())
		{
			return IsGameCreating();
		}
		return true;
	}

	public bool WasConcedeRequested()
	{
		return m_concedeRequested;
	}

	public void Concede()
	{
		if (!m_concedeRequested)
		{
			m_concedeRequested = true;
			Network.Get().Concede();
		}
	}

	public bool WasRestartRequested()
	{
		return m_restartRequested;
	}

	public void Restart()
	{
		if (!m_restartRequested)
		{
			m_restartRequested = true;
			if (IsGameOverNowOrPending())
			{
				CheckRestartOnRealTimeGameOver();
			}
			else
			{
				Concede();
			}
		}
	}

	private void CheckRestartOnRealTimeGameOver()
	{
		if (WasRestartRequested())
		{
			m_gameOver = true;
			m_realTimeGameOverTagChange = null;
			Network.Get().DisconnectFromGameServer();
			NotificationManager.Get().DestroyAllNotificationsNowWithNoAnim();
			ReconnectMgr.Get().SetBypassReconnect(shouldBypass: true);
			GameMgr.Get().RestartGame();
		}
	}

	public bool IsGameOver()
	{
		return m_gameOver;
	}

	public bool IsGameOverPending()
	{
		return m_realTimeGameOverTagChange != null;
	}

	public bool IsGameOverNowOrPending()
	{
		if (IsGameOver())
		{
			return true;
		}
		if (IsGameOverPending())
		{
			return true;
		}
		return false;
	}

	public Network.HistTagChange GetRealTimeGameOverTagChange()
	{
		return m_realTimeGameOverTagChange;
	}

	public void ShowEnemyTauntCharacters()
	{
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag != TAG_ZONE.PLAY || zone.m_Side != Player.Side.OPPOSING)
			{
				continue;
			}
			List<Card> cards = zone.GetCards();
			for (int j = 0; j < cards.Count; j++)
			{
				Card card = cards[j];
				Entity entity = card.GetEntity();
				if (entity.HasTaunt() && !entity.IsStealthed())
				{
					card.DoTauntNotification();
				}
			}
		}
	}

	public void GetTauntCounts(Player player, out int minionCount, out int heroCount)
	{
		minionCount = 0;
		heroCount = 0;
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag != TAG_ZONE.PLAY || player != zone.GetController())
			{
				continue;
			}
			List<Card> cards = zone.GetCards();
			for (int j = 0; j < cards.Count; j++)
			{
				Entity entity = cards[j].GetEntity();
				if (entity.HasTaunt() && !entity.IsStealthed())
				{
					switch (entity.GetCardType())
					{
					case TAG_CARDTYPE.MINION:
						minionCount++;
						break;
					case TAG_CARDTYPE.HERO:
						heroCount++;
						break;
					}
				}
			}
		}
	}

	public Card GetFriendlyCardBeingDrawn()
	{
		return m_friendlyCardBeingDrawn;
	}

	public void SetFriendlyCardBeingDrawn(Card card)
	{
		m_friendlyCardBeingDrawn = card;
	}

	public Card GetOpponentCardBeingDrawn()
	{
		return m_opponentCardBeingDrawn;
	}

	public void SetOpponentCardBeingDrawn(Card card)
	{
		m_opponentCardBeingDrawn = card;
	}

	public bool IsBeingDrawn(Card card)
	{
		if (card == m_friendlyCardBeingDrawn)
		{
			return true;
		}
		if (card == m_opponentCardBeingDrawn)
		{
			return true;
		}
		return false;
	}

	public bool ClearCardBeingDrawn(Card card)
	{
		if (card == m_friendlyCardBeingDrawn)
		{
			m_friendlyCardBeingDrawn = null;
			return true;
		}
		if (card == m_opponentCardBeingDrawn)
		{
			m_opponentCardBeingDrawn = null;
			return true;
		}
		return false;
	}

	public int GetLastTurnRemindedOfFullHand()
	{
		return m_lastTurnRemindedOfFullHand;
	}

	public void SetLastTurnRemindedOfFullHand(int turn)
	{
		m_lastTurnRemindedOfFullHand = turn;
	}

	public bool IsUsingFastActorTriggers()
	{
		GameEntity gameEntity = GetGameEntity();
		if (gameEntity != null && gameEntity.HasTag(GAME_TAG.ALWAYS_USE_FAST_ACTOR_TRIGGERS))
		{
			return true;
		}
		return m_usingFastActorTriggers;
	}

	public void SetUsingFastActorTriggers(bool enable)
	{
		m_usingFastActorTriggers = enable;
	}

	public bool HasHandPlays()
	{
		if (m_options == null)
		{
			return false;
		}
		foreach (Network.Options.Option item in m_options.List)
		{
			if (item.Type != Network.Options.Option.OptionType.POWER)
			{
				continue;
			}
			Entity entity = GetEntity(item.Main.ID);
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (!(card == null) && !(card.GetZone() as ZoneHand == null))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CanShowScoreScreen()
	{
		if (HasScoreLabels(m_gameEntity))
		{
			return true;
		}
		if (HasScoreLabels(GetFriendlySidePlayer()))
		{
			return true;
		}
		return false;
	}

	private bool HasScoreLabels(Entity entity)
	{
		if (entity.HasTag(GAME_TAG.SCORE_LABELID_1))
		{
			return true;
		}
		if (entity.HasTag(GAME_TAG.SCORE_LABELID_2))
		{
			return true;
		}
		if (entity.HasTag(GAME_TAG.SCORE_LABELID_3))
		{
			return true;
		}
		if (entity.HasTag(GAME_TAG.SCORE_FOOTERID))
		{
			return true;
		}
		return false;
	}

	public int GetFriendlyCardDrawCounter()
	{
		return m_friendlyDrawCounter;
	}

	public void IncrementFriendlyCardDrawCounter()
	{
		m_friendlyDrawCounter++;
	}

	public void ResetFriendlyCardDrawCounter()
	{
		m_friendlyDrawCounter = 0;
	}

	public int GetOpponentCardDrawCounter()
	{
		return m_opponentDrawCounter;
	}

	public void IncrementOpponentCardDrawCounter()
	{
		m_opponentDrawCounter++;
	}

	public void ResetOpponentCardDrawCounter()
	{
		m_opponentDrawCounter = 0;
	}

	private void PreprocessRealTimeTagChange(Entity entity, Network.HistTagChange change)
	{
		switch (change.Tag)
		{
		case 17:
			if (GameUtils.IsGameOverTag(change.Entity, change.Tag, change.Value))
			{
				OnRealTimeGameOver(change);
			}
			break;
		case 860:
			HandleWaitForOpponentReconnectPeriod(change.Value);
			break;
		case 231:
			if (change.Value > 0)
			{
				OnCantPlay(entity);
			}
			break;
		}
	}

	private void HandleWaitForOpponentReconnectPeriod(int periodInSeconds)
	{
		m_gameEntity.SetTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD, periodInSeconds);
		if (periodInSeconds > 0)
		{
			ShowWaitForOpponentReconnectPopup(periodInSeconds);
			TurnTimerUpdate turnTimerUpdate = new TurnTimerUpdate();
			turnTimerUpdate.SetSecondsRemaining(float.PositiveInfinity);
			turnTimerUpdate.SetEndTimestamp(float.PositiveInfinity);
			turnTimerUpdate.SetShow(show: false);
			TriggerTurnTimerUpdate(turnTimerUpdate);
		}
		else
		{
			HideWaitForOpponentReconnectPopup();
		}
		GameMgr.Get().UpdatePresence();
	}

	private void ShowWaitForOpponentReconnectPopup(int periodInSeconds)
	{
		if (m_waitForOpponentReconnectPopupInfo == null)
		{
			m_waitForOpponentReconnectPopupInfo = new AlertPopup.PopupInfo();
			m_waitForOpponentReconnectPopupInfo.m_headerText = GameStrings.Get("GLOBAL_WAIT_FOR_OPPONENT_RECONNECT_HEADER");
			m_waitForOpponentReconnectPopupInfo.m_showAlertIcon = false;
			m_waitForOpponentReconnectPopupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
			m_waitForOpponentReconnectPopupInfo.m_responseUserData = periodInSeconds;
			m_waitForOpponentReconnectPopupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			m_waitForOpponentReconnectPopupInfo.m_layerToUse = GameLayer.UI;
			DialogManager.Get().ShowPopup(m_waitForOpponentReconnectPopupInfo, OnWaitForOpponentReconnectPopupProcessed);
			Gameplay.Get().StartCoroutine(IncreaseWaitForOpponentReconnectPeriod());
		}
		else
		{
			UpdateWaitForOpponentReconnectPopup(periodInSeconds);
		}
	}

	private bool OnWaitForOpponentReconnectPopupProcessed(DialogBase dialog, object userData)
	{
		m_waitForOpponentReconnectPopup = (AlertPopup)dialog;
		if (m_waitForOpponentReconnectPopupInfo != null)
		{
			UpdateWaitForOpponentReconnectPopup((int)m_waitForOpponentReconnectPopupInfo.m_responseUserData);
			return true;
		}
		return false;
	}

	private void HideWaitForOpponentReconnectPopup()
	{
		Gameplay.Get().StopCoroutine("IncreaseWaitForOpponentReconnectPeriod");
		if (m_waitForOpponentReconnectPopup != null)
		{
			m_waitForOpponentReconnectPopup.Hide();
		}
		m_waitForOpponentReconnectPopup = null;
		m_waitForOpponentReconnectPopupInfo = null;
	}

	private void UpdateWaitForOpponentReconnectPopup(int periodInSeconds)
	{
		m_waitForOpponentReconnectPopupInfo.m_responseUserData = periodInSeconds;
		int num = periodInSeconds / 60;
		int num2 = periodInSeconds % 60;
		string key = (GameMgr.Get().IsSpectator() ? "GLOBAL_WAIT_FOR_OPPONENT_RECONNECT_SPECTATOR" : "GLOBAL_WAIT_FOR_OPPONENT_RECONNECT");
		m_waitForOpponentReconnectPopupInfo.m_text = string.Format(GameStrings.Get(key), num, num2);
		if (m_waitForOpponentReconnectPopup != null)
		{
			m_waitForOpponentReconnectPopup.UpdateInfo(m_waitForOpponentReconnectPopupInfo);
		}
	}

	private IEnumerator IncreaseWaitForOpponentReconnectPeriod()
	{
		while (true)
		{
			yield return new WaitForSecondsRealtime(1f);
			if (m_waitForOpponentReconnectPopupInfo == null)
			{
				break;
			}
			int num = (int)m_waitForOpponentReconnectPopupInfo.m_responseUserData;
			UpdateWaitForOpponentReconnectPopup(num + 1);
		}
	}

	private void PreprocessTagChange(Entity entity, TagDelta change)
	{
		switch (change.tag)
		{
		case 23:
			if (change.newValue == 1)
			{
				Player player = (Player)entity;
				OnCurrentPlayerChanged(player);
			}
			break;
		case 20:
			OnTurnChanged(change.oldValue, change.newValue);
			break;
		case 17:
			if (GameUtils.IsGameOverTag((Player)entity, change.tag, change.newValue))
			{
				OnGameOver((TAG_PLAYSTATE)change.newValue);
			}
			break;
		}
	}

	private void PreprocessEarlyConcedeTagChange(Entity entity, TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.PLAYSTATE && GameUtils.IsGameOverTag((Player)entity, change.tag, change.newValue))
		{
			OnGameOver((TAG_PLAYSTATE)change.newValue);
		}
	}

	private void ProcessEarlyConcedeTagChange(Entity entity, TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.PLAYSTATE)
		{
			entity.OnTagChanged(change);
		}
	}

	private void OnRealTimeGameOver(Network.HistTagChange change)
	{
		m_realTimeGameOverTagChange = change;
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(null);
		}
		SpectatorManager.Get().OnRealTimeGameOver();
		CheckRestartOnRealTimeGameOver();
	}

	private void OnGameOver(TAG_PLAYSTATE playState)
	{
		m_gameOver = true;
		m_realTimeGameOverTagChange = null;
		m_gameEntity.NotifyOfGameOver(playState);
		FireGameOverEvent(playState);
		HideWaitForOpponentReconnectPopup();
		GameMgr.Get().LastGameData.GameResult = playState;
		if (GetFriendlySidePlayer() != null && GetFriendlySidePlayer().GetHero() != null)
		{
			GameMgr.Get().LastGameData.BattlegroundsLeaderboardPlace = GetFriendlySidePlayer().GetHero().GetRealTimePlayerLeaderboardPlace();
		}
	}

	private void OnCurrentPlayerChanged(Player player)
	{
		FireCurrentPlayerChangedEvent(player);
	}

	private void OnTurnChanged(int oldTurn, int newTurn)
	{
		OnTurnChanged_TurnTimer(oldTurn, newTurn);
		FireTurnChangedEvent(oldTurn, newTurn);
	}

	public IEnumerator RejectUnresolvedChangesAfterDelay()
	{
		yield return new WaitForSecondsRealtime(1f);
		RejectUnresolvedOptions();
	}

	private void RejectUnresolvedOptions()
	{
		if (m_lastSelectedOption != null && m_lastOptions != null && ZoneMgr.Get().HasUnresolvedLocalChange())
		{
			Get().OnOptionRejected(m_lastOptions.ID);
		}
	}

	private void OnCantPlay(Entity entity)
	{
		FireCantPlayEvent(entity);
	}

	public void AddServerBlockingSpell(Spell spell)
	{
		if (!(spell == null) && !m_serverBlockingSpells.Contains(spell))
		{
			m_serverBlockingSpells.Add(spell);
		}
	}

	public bool RemoveServerBlockingSpell(Spell spell)
	{
		return m_serverBlockingSpells.Remove(spell);
	}

	public void AddServerBlockingSpellController(SpellController spellController)
	{
		if (!(spellController == null) && !m_serverBlockingSpellControllers.Contains(spellController))
		{
			m_serverBlockingSpellControllers.Add(spellController);
		}
	}

	public bool RemoveServerBlockingSpellController(SpellController spellController)
	{
		return m_serverBlockingSpellControllers.Remove(spellController);
	}

	public void DebugNukeServerBlocks()
	{
		while (m_serverBlockingSpells.Count > 0)
		{
			m_serverBlockingSpells[0].OnSpellFinished();
		}
		while (m_serverBlockingSpellControllers.Count > 0)
		{
			m_serverBlockingSpellControllers[0].ForceKill();
		}
		m_powerProcessor.ForceStopHistoryBlocking();
		m_busy = false;
	}

	private bool IsBlockingPowerProcessor()
	{
		if (m_serverBlockingSpells.Count > 0)
		{
			return true;
		}
		if (m_serverBlockingSpellControllers.Count > 0)
		{
			return true;
		}
		if (m_powerProcessor.IsHistoryBlocking())
		{
			return true;
		}
		return false;
	}

	private bool ShouldAdvanceReconnectIfStuckTimer()
	{
		foreach (Spell serverBlockingSpell in m_serverBlockingSpells)
		{
			if (serverBlockingSpell.ShouldReconnectIfStuck())
			{
				return true;
			}
		}
		foreach (SpellController serverBlockingSpellController in m_serverBlockingSpellControllers)
		{
			if (serverBlockingSpellController.ShouldReconnectIfStuck())
			{
				return true;
			}
		}
		if (m_powerProcessor.IsHistoryBlocking())
		{
			return true;
		}
		return false;
	}

	public bool MustWaitForChoices()
	{
		if (!ChoiceCardMgr.Get().HasChoices())
		{
			return false;
		}
		PowerProcessor powerProcessor = Get().GetPowerProcessor();
		if (powerProcessor.HasGameOverTaskList())
		{
			return false;
		}
		foreach (int key in Get().GetPlayerMap().Keys)
		{
			PowerTaskList preChoiceTaskList = ChoiceCardMgr.Get().GetPreChoiceTaskList(key);
			if (preChoiceTaskList != null && !powerProcessor.HasTaskList(preChoiceTaskList))
			{
				return true;
			}
		}
		return false;
	}

	public bool CanProcessPowerQueue()
	{
		if (IsBlockingPowerProcessor())
		{
			return false;
		}
		if (IsBusy())
		{
			return false;
		}
		if (MustWaitForChoices())
		{
			return false;
		}
		if (m_powerProcessor.GetCurrentTaskList() != null)
		{
			return false;
		}
		if (m_powerProcessor.GetPowerQueue().Count == 0)
		{
			return false;
		}
		if (WasRestartRequested())
		{
			return false;
		}
		return true;
	}

	private bool CheckReconnectIfStuck()
	{
		if (!ShouldAdvanceReconnectIfStuckTimer())
		{
			m_reconnectIfStuckTimer = 0f;
			return false;
		}
		m_reconnectIfStuckTimer += Time.deltaTime;
		if (ReconnectIfStuck())
		{
			return true;
		}
		ReportStuck();
		return true;
	}

	private bool ReconnectIfStuck()
	{
		Network.GameSetup gameSetup = GameMgr.Get().GetGameSetup();
		if (gameSetup.DisconnectWhenStuckSeconds != 0 && m_reconnectIfStuckTimer < (float)gameSetup.DisconnectWhenStuckSeconds)
		{
			return false;
		}
		string devElapsedTimeString = TimeUtils.GetDevElapsedTimeString(m_reconnectIfStuckTimer);
		string text = BuildServerBlockingCausesString();
		Log.Power.PrintWarning("GameState.ReconnectIfStuck() - Blocked more than {0}. Cause:\n{1}", devElapsedTimeString, text);
		PerformanceAnalytics.Get()?.ReconnectStart("STUCK");
		Network.Get().DisconnectFromGameServer();
		return true;
	}

	private void ReportStuck()
	{
		if (!(m_reconnectIfStuckTimer < 10f))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (!(realtimeSinceStartup - m_lastBlockedReportTimestamp < 3f))
			{
				m_lastBlockedReportTimestamp = realtimeSinceStartup;
				string devElapsedTimeString = TimeUtils.GetDevElapsedTimeString(m_reconnectIfStuckTimer);
				string text = BuildServerBlockingCausesString();
				Log.Power.PrintWarning("GameState.ReportStuck() - Stuck for {0}. {1}", devElapsedTimeString, text);
			}
		}
	}

	private string BuildServerBlockingCausesString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		int sectionCount = 0;
		AppendServerBlockingSection(stringBuilder, "Spells:", m_serverBlockingSpells, AppendServerBlockingSpell, ref sectionCount);
		AppendServerBlockingSection(stringBuilder, "SpellControllers:", m_serverBlockingSpellControllers, AppendServerBlockingSpellController, ref sectionCount);
		AppendServerBlockingHistory(stringBuilder, ref sectionCount);
		if (m_busy)
		{
			if (sectionCount > 0)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append("Busy=true");
			sectionCount++;
		}
		return stringBuilder.ToString();
	}

	private void AppendServerBlockingSection<T>(StringBuilder builder, string sectionPrefix, List<T> items, AppendBlockingServerItemCallback<T> itemCallback, ref int sectionCount) where T : Component
	{
		if (items.Count == 0)
		{
			return;
		}
		if (sectionCount > 0)
		{
			builder.Append(' ');
		}
		builder.Append('{');
		builder.Append(sectionPrefix);
		for (int i = 0; i < items.Count; i++)
		{
			builder.Append(' ');
			if (itemCallback == null)
			{
				builder.Append(items[i].name);
			}
			else
			{
				itemCallback(builder, items[i]);
			}
		}
		builder.Append('}');
		sectionCount++;
	}

	private void AppendServerBlockingSpell(StringBuilder builder, Spell spell)
	{
		if (spell == null)
		{
			builder.Append("[null Spell (The Spell object may have been destroyed prematurely)]");
			return;
		}
		builder.Append('[');
		builder.Append(spell.name);
		builder.Append(' ');
		builder.AppendFormat("Source: {0}", spell.GetSource());
		builder.Append(' ');
		builder.Append("Targets:");
		List<GameObject> targets = spell.GetTargets();
		if (targets.Count == 0)
		{
			builder.Append(' ');
			builder.Append("none");
		}
		else
		{
			for (int i = 0; i < targets.Count; i++)
			{
				builder.Append(' ');
				GameObject gameObject = targets[i];
				builder.Append(gameObject.ToString());
			}
		}
		builder.Append(']');
	}

	private void AppendServerBlockingSpellController(StringBuilder builder, SpellController spellController)
	{
		builder.Append('[');
		builder.Append(spellController.name);
		builder.Append(' ');
		builder.AppendFormat("Source: {0}", spellController.GetSource());
		builder.Append(' ');
		builder.Append("Targets:");
		List<Card> targets = spellController.GetTargets();
		if (targets.Count == 0)
		{
			builder.Append(' ');
			builder.Append("none");
		}
		else
		{
			for (int i = 0; i < targets.Count; i++)
			{
				builder.Append(' ');
				Card card = targets[i];
				builder.Append(card.ToString());
			}
		}
		builder.Append(']');
	}

	private void AppendServerBlockingHistory(StringBuilder builder, ref int sectionCount)
	{
		if (m_powerProcessor.IsHistoryBlocking())
		{
			Entity pendingBigCardEntity = HistoryManager.Get().GetPendingBigCardEntity();
			PowerTaskList historyBlockingTaskList = m_powerProcessor.GetHistoryBlockingTaskList();
			PowerTaskList currentTaskList = m_powerProcessor.GetCurrentTaskList();
			if (sectionCount > 0)
			{
				builder.Append(' ');
			}
			builder.Append("History: ");
			builder.Append('{');
			builder.AppendFormat("PendingBigCard: {0}", pendingBigCardEntity);
			builder.Append(' ');
			builder.AppendFormat("BlockingTaskList: ");
			PrintBlockingTaskList(builder, historyBlockingTaskList);
			builder.Append(' ');
			builder.AppendFormat("CurrentTaskList: ");
			PrintBlockingTaskList(builder, currentTaskList);
			builder.Append('}');
			sectionCount++;
		}
	}

	public static bool RegisterGameStateInitializedListener(GameStateInitializedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		GameStateInitializedListener gameStateInitializedListener = new GameStateInitializedListener();
		gameStateInitializedListener.SetCallback(callback);
		gameStateInitializedListener.SetUserData(userData);
		if (s_gameStateInitializedListeners == null)
		{
			s_gameStateInitializedListeners = new List<GameStateInitializedListener>();
		}
		else if (s_gameStateInitializedListeners.Contains(gameStateInitializedListener))
		{
			return false;
		}
		s_gameStateInitializedListeners.Add(gameStateInitializedListener);
		return true;
	}

	public static bool UnregisterGameStateInitializedListener(GameStateInitializedCallback callback, object userData = null)
	{
		if (callback == null || s_gameStateInitializedListeners == null)
		{
			return false;
		}
		GameStateInitializedListener gameStateInitializedListener = new GameStateInitializedListener();
		gameStateInitializedListener.SetCallback(callback);
		gameStateInitializedListener.SetUserData(userData);
		return s_gameStateInitializedListeners.Remove(gameStateInitializedListener);
	}

	public bool RegisterCreateGameListener(CreateGameCallback callback)
	{
		return RegisterCreateGameListener(callback, null);
	}

	public bool RegisterCreateGameListener(CreateGameCallback callback, object userData)
	{
		CreateGameListener createGameListener = new CreateGameListener();
		createGameListener.SetCallback(callback);
		createGameListener.SetUserData(userData);
		if (m_createGameListeners.Contains(createGameListener))
		{
			return false;
		}
		m_createGameListeners.Add(createGameListener);
		return true;
	}

	public bool UnregisterCreateGameListener(CreateGameCallback callback)
	{
		return UnregisterCreateGameListener(callback, null);
	}

	public bool UnregisterCreateGameListener(CreateGameCallback callback, object userData)
	{
		CreateGameListener createGameListener = new CreateGameListener();
		createGameListener.SetCallback(callback);
		createGameListener.SetUserData(userData);
		return m_createGameListeners.Remove(createGameListener);
	}

	public bool RegisterOptionsReceivedListener(OptionsReceivedCallback callback)
	{
		return RegisterOptionsReceivedListener(callback, null);
	}

	public bool RegisterOptionsReceivedListener(OptionsReceivedCallback callback, object userData)
	{
		OptionsReceivedListener optionsReceivedListener = new OptionsReceivedListener();
		optionsReceivedListener.SetCallback(callback);
		optionsReceivedListener.SetUserData(userData);
		if (m_optionsReceivedListeners.Contains(optionsReceivedListener))
		{
			return false;
		}
		m_optionsReceivedListeners.Add(optionsReceivedListener);
		return true;
	}

	public bool UnregisterOptionsReceivedListener(OptionsReceivedCallback callback)
	{
		return UnregisterOptionsReceivedListener(callback, null);
	}

	public bool UnregisterOptionsReceivedListener(OptionsReceivedCallback callback, object userData)
	{
		OptionsReceivedListener optionsReceivedListener = new OptionsReceivedListener();
		optionsReceivedListener.SetCallback(callback);
		optionsReceivedListener.SetUserData(userData);
		return m_optionsReceivedListeners.Remove(optionsReceivedListener);
	}

	public bool RegisterOptionsSentListener(OptionsSentCallback callback, object userData = null)
	{
		OptionsSentListener optionsSentListener = new OptionsSentListener();
		optionsSentListener.SetCallback(callback);
		optionsSentListener.SetUserData(userData);
		if (m_optionsSentListeners.Contains(optionsSentListener))
		{
			return false;
		}
		m_optionsSentListeners.Add(optionsSentListener);
		return true;
	}

	public bool UnregisterOptionsReceivedListener(OptionsSentCallback callback, object userData = null)
	{
		OptionsSentListener optionsSentListener = new OptionsSentListener();
		optionsSentListener.SetCallback(callback);
		optionsSentListener.SetUserData(userData);
		return m_optionsSentListeners.Remove(optionsSentListener);
	}

	public bool RegisterOptionRejectedListener(OptionRejectedCallback callback, object userData = null)
	{
		OptionRejectedListener optionRejectedListener = new OptionRejectedListener();
		optionRejectedListener.SetCallback(callback);
		optionRejectedListener.SetUserData(userData);
		if (m_optionRejectedListeners.Contains(optionRejectedListener))
		{
			return false;
		}
		m_optionRejectedListeners.Add(optionRejectedListener);
		return true;
	}

	public bool UnregisterOptionRejectedListener(OptionRejectedCallback callback, object userData = null)
	{
		OptionRejectedListener optionRejectedListener = new OptionRejectedListener();
		optionRejectedListener.SetCallback(callback);
		optionRejectedListener.SetUserData(userData);
		return m_optionRejectedListeners.Remove(optionRejectedListener);
	}

	public bool RegisterEntityChoicesReceivedListener(EntityChoicesReceivedCallback callback)
	{
		return RegisterEntityChoicesReceivedListener(callback, null);
	}

	public bool RegisterEntityChoicesReceivedListener(EntityChoicesReceivedCallback callback, object userData)
	{
		EntityChoicesReceivedListener entityChoicesReceivedListener = new EntityChoicesReceivedListener();
		entityChoicesReceivedListener.SetCallback(callback);
		entityChoicesReceivedListener.SetUserData(userData);
		if (m_entityChoicesReceivedListeners.Contains(entityChoicesReceivedListener))
		{
			return false;
		}
		m_entityChoicesReceivedListeners.Add(entityChoicesReceivedListener);
		return true;
	}

	public bool UnregisterEntityChoicesReceivedListener(EntityChoicesReceivedCallback callback)
	{
		return UnregisterEntityChoicesReceivedListener(callback, null);
	}

	public bool UnregisterEntityChoicesReceivedListener(EntityChoicesReceivedCallback callback, object userData)
	{
		EntityChoicesReceivedListener entityChoicesReceivedListener = new EntityChoicesReceivedListener();
		entityChoicesReceivedListener.SetCallback(callback);
		entityChoicesReceivedListener.SetUserData(userData);
		return m_entityChoicesReceivedListeners.Remove(entityChoicesReceivedListener);
	}

	public bool RegisterEntitiesChosenReceivedListener(EntitiesChosenReceivedCallback callback)
	{
		return RegisterEntitiesChosenReceivedListener(callback, null);
	}

	public bool RegisterEntitiesChosenReceivedListener(EntitiesChosenReceivedCallback callback, object userData)
	{
		EntitiesChosenReceivedListener entitiesChosenReceivedListener = new EntitiesChosenReceivedListener();
		entitiesChosenReceivedListener.SetCallback(callback);
		entitiesChosenReceivedListener.SetUserData(userData);
		if (m_entitiesChosenReceivedListeners.Contains(entitiesChosenReceivedListener))
		{
			return false;
		}
		m_entitiesChosenReceivedListeners.Add(entitiesChosenReceivedListener);
		return true;
	}

	public bool UnregisterEntitiesChosenReceivedListener(EntitiesChosenReceivedCallback callback)
	{
		return UnregisterEntitiesChosenReceivedListener(callback, null);
	}

	public bool UnregisterEntitiesChosenReceivedListener(EntitiesChosenReceivedCallback callback, object userData)
	{
		EntitiesChosenReceivedListener entitiesChosenReceivedListener = new EntitiesChosenReceivedListener();
		entitiesChosenReceivedListener.SetCallback(callback);
		entitiesChosenReceivedListener.SetUserData(userData);
		return m_entitiesChosenReceivedListeners.Remove(entitiesChosenReceivedListener);
	}

	public bool RegisterCurrentPlayerChangedListener(CurrentPlayerChangedCallback callback)
	{
		return RegisterCurrentPlayerChangedListener(callback, null);
	}

	public bool RegisterCurrentPlayerChangedListener(CurrentPlayerChangedCallback callback, object userData)
	{
		CurrentPlayerChangedListener currentPlayerChangedListener = new CurrentPlayerChangedListener();
		currentPlayerChangedListener.SetCallback(callback);
		currentPlayerChangedListener.SetUserData(userData);
		if (m_currentPlayerChangedListeners.Contains(currentPlayerChangedListener))
		{
			return false;
		}
		m_currentPlayerChangedListeners.Add(currentPlayerChangedListener);
		return true;
	}

	public bool UnregisterCurrentPlayerChangedListener(CurrentPlayerChangedCallback callback)
	{
		return UnregisterCurrentPlayerChangedListener(callback, null);
	}

	public bool UnregisterCurrentPlayerChangedListener(CurrentPlayerChangedCallback callback, object userData)
	{
		CurrentPlayerChangedListener currentPlayerChangedListener = new CurrentPlayerChangedListener();
		currentPlayerChangedListener.SetCallback(callback);
		currentPlayerChangedListener.SetUserData(userData);
		return m_currentPlayerChangedListeners.Remove(currentPlayerChangedListener);
	}

	public bool RegisterTurnChangedListener(TurnChangedCallback callback)
	{
		return RegisterTurnChangedListener(callback, null);
	}

	public bool RegisterTurnChangedListener(TurnChangedCallback callback, object userData)
	{
		TurnChangedListener turnChangedListener = new TurnChangedListener();
		turnChangedListener.SetCallback(callback);
		turnChangedListener.SetUserData(userData);
		if (m_turnChangedListeners.Contains(turnChangedListener))
		{
			return false;
		}
		m_turnChangedListeners.Add(turnChangedListener);
		return true;
	}

	public bool UnregisterTurnChangedListener(TurnChangedCallback callback)
	{
		return UnregisterTurnChangedListener(callback, null);
	}

	public bool UnregisterTurnChangedListener(TurnChangedCallback callback, object userData)
	{
		TurnChangedListener turnChangedListener = new TurnChangedListener();
		turnChangedListener.SetCallback(callback);
		turnChangedListener.SetUserData(userData);
		return m_turnChangedListeners.Remove(turnChangedListener);
	}

	public bool RegisterFriendlyTurnStartedListener(FriendlyTurnStartedCallback callback, object userData = null)
	{
		FriendlyTurnStartedListener friendlyTurnStartedListener = new FriendlyTurnStartedListener();
		friendlyTurnStartedListener.SetCallback(callback);
		friendlyTurnStartedListener.SetUserData(userData);
		if (m_friendlyTurnStartedListeners.Contains(friendlyTurnStartedListener))
		{
			return false;
		}
		m_friendlyTurnStartedListeners.Add(friendlyTurnStartedListener);
		return true;
	}

	public bool UnregisterFriendlyTurnStartedListener(FriendlyTurnStartedCallback callback, object userData = null)
	{
		FriendlyTurnStartedListener friendlyTurnStartedListener = new FriendlyTurnStartedListener();
		friendlyTurnStartedListener.SetCallback(callback);
		friendlyTurnStartedListener.SetUserData(userData);
		return m_friendlyTurnStartedListeners.Remove(friendlyTurnStartedListener);
	}

	public bool RegisterTurnTimerUpdateListener(TurnTimerUpdateCallback callback)
	{
		return RegisterTurnTimerUpdateListener(callback, null);
	}

	public bool RegisterTurnTimerUpdateListener(TurnTimerUpdateCallback callback, object userData)
	{
		TurnTimerUpdateListener turnTimerUpdateListener = new TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		if (m_turnTimerUpdateListeners.Contains(turnTimerUpdateListener))
		{
			return false;
		}
		m_turnTimerUpdateListeners.Add(turnTimerUpdateListener);
		return true;
	}

	public bool UnregisterTurnTimerUpdateListener(TurnTimerUpdateCallback callback)
	{
		return UnregisterTurnTimerUpdateListener(callback, null);
	}

	public bool UnregisterTurnTimerUpdateListener(TurnTimerUpdateCallback callback, object userData)
	{
		TurnTimerUpdateListener turnTimerUpdateListener = new TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		return m_turnTimerUpdateListeners.Remove(turnTimerUpdateListener);
	}

	public bool RegisterMulliganTimerUpdateListener(TurnTimerUpdateCallback callback)
	{
		return RegisterMulliganTimerUpdateListener(callback, null);
	}

	public bool RegisterMulliganTimerUpdateListener(TurnTimerUpdateCallback callback, object userData)
	{
		TurnTimerUpdateListener turnTimerUpdateListener = new TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		if (m_mulliganTimerUpdateListeners.Contains(turnTimerUpdateListener))
		{
			return false;
		}
		m_mulliganTimerUpdateListeners.Add(turnTimerUpdateListener);
		return true;
	}

	public bool UnregisterMulliganTimerUpdateListener(TurnTimerUpdateCallback callback)
	{
		return UnregisterMulliganTimerUpdateListener(callback, null);
	}

	public bool UnregisterMulliganTimerUpdateListener(TurnTimerUpdateCallback callback, object userData)
	{
		TurnTimerUpdateListener turnTimerUpdateListener = new TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		return m_mulliganTimerUpdateListeners.Remove(turnTimerUpdateListener);
	}

	public bool RegisterSpectatorNotifyListener(SpectatorNotifyEventCallback callback, object userData = null)
	{
		SpectatorNotifyListener spectatorNotifyListener = new SpectatorNotifyListener();
		spectatorNotifyListener.SetCallback(callback);
		spectatorNotifyListener.SetUserData(userData);
		if (m_spectatorNotifyListeners.Contains(spectatorNotifyListener))
		{
			return false;
		}
		m_spectatorNotifyListeners.Add(spectatorNotifyListener);
		return true;
	}

	public bool UnregisterSpectatorNotifyListener(SpectatorNotifyEventCallback callback, object userData = null)
	{
		SpectatorNotifyListener spectatorNotifyListener = new SpectatorNotifyListener();
		spectatorNotifyListener.SetCallback(callback);
		spectatorNotifyListener.SetUserData(userData);
		return m_spectatorNotifyListeners.Remove(spectatorNotifyListener);
	}

	public bool RegisterGameOverListener(GameOverCallback callback, object userData = null)
	{
		GameOverListener gameOverListener = new GameOverListener();
		gameOverListener.SetCallback(callback);
		gameOverListener.SetUserData(userData);
		if (m_gameOverListeners.Contains(gameOverListener))
		{
			return false;
		}
		m_gameOverListeners.Add(gameOverListener);
		return true;
	}

	public bool UnregisterGameOverListener(GameOverCallback callback, object userData = null)
	{
		GameOverListener gameOverListener = new GameOverListener();
		gameOverListener.SetCallback(callback);
		gameOverListener.SetUserData(userData);
		return m_gameOverListeners.Remove(gameOverListener);
	}

	public bool RegisterHeroChangedListener(HeroChangedCallback callback, object userData = null)
	{
		HeroChangedListener heroChangedListener = new HeroChangedListener();
		heroChangedListener.SetCallback(callback);
		heroChangedListener.SetUserData(userData);
		if (m_heroChangedListeners.Contains(heroChangedListener))
		{
			return false;
		}
		m_heroChangedListeners.Add(heroChangedListener);
		return true;
	}

	public bool UnregisterHeroChangedListener(HeroChangedCallback callback, object userData = null)
	{
		HeroChangedListener heroChangedListener = new HeroChangedListener();
		heroChangedListener.SetCallback(callback);
		heroChangedListener.SetUserData(userData);
		return m_heroChangedListeners.Remove(heroChangedListener);
	}

	public bool RegisterCantPlayListener(CantPlayCallback callback, object userData = null)
	{
		CantPlayListener cantPlayListener = new CantPlayListener();
		cantPlayListener.SetCallback(callback);
		cantPlayListener.SetUserData(userData);
		if (m_cantPlayListeners.Contains(cantPlayListener))
		{
			return false;
		}
		m_cantPlayListeners.Add(cantPlayListener);
		return true;
	}

	public bool UnregisterCantPlayListener(CantPlayCallback callback, object userData = null)
	{
		CantPlayListener cantPlayListener = new CantPlayListener();
		cantPlayListener.SetCallback(callback);
		cantPlayListener.SetUserData(userData);
		return m_cantPlayListeners.Remove(cantPlayListener);
	}

	private static void FireGameStateInitializedEvent()
	{
		if (s_gameStateInitializedListeners != null)
		{
			GameStateInitializedListener[] array = s_gameStateInitializedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(s_instance);
			}
		}
	}

	private void FireCreateGameEvent()
	{
		CreateGameListener[] array = m_createGameListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(m_createGamePhase);
		}
	}

	private void FireOptionsReceivedEvent()
	{
		OptionsReceivedListener[] array = m_optionsReceivedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void FireOptionsSentEvent(Network.Options.Option option)
	{
		OptionsSentListener[] array = m_optionsSentListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(option);
		}
	}

	private void FireOptionRejectedEvent(Network.Options.Option option)
	{
		OptionRejectedListener[] array = m_optionRejectedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(option);
		}
	}

	private void FireEntityChoicesReceivedEvent(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		EntityChoicesReceivedListener[] array = m_entityChoicesReceivedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(choices, preChoiceTaskList);
		}
	}

	private bool FireEntitiesChosenReceivedEvent(Network.EntitiesChosen chosen)
	{
		EntitiesChosenReceivedListener[] array = m_entitiesChosenReceivedListeners.ToArray();
		bool flag = false;
		EntitiesChosenReceivedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			flag = array2[i].Fire(chosen) || flag;
		}
		return flag;
	}

	private void FireTurnChangedEvent(int oldTurn, int newTurn)
	{
		TurnChangedListener[] array = m_turnChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(oldTurn, newTurn);
		}
	}

	public void FireFriendlyTurnStartedEvent()
	{
		m_gameEntity.NotifyOfStartOfTurnEventsFinished();
		FriendlyTurnStartedListener[] array = m_friendlyTurnStartedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	private void FireTurnTimerUpdateEvent(TurnTimerUpdate update)
	{
		TurnTimerUpdateListener[] array = null;
		if (GetGameEntity() == null)
		{
			UnityEngine.Debug.LogWarning("FireTurnTimerUpdateEvent - Turn timer update received before game entity created.");
			return;
		}
		array = ((!GetGameEntity().IsMulliganActiveRealTime()) ? m_turnTimerUpdateListeners.ToArray() : m_mulliganTimerUpdateListeners.ToArray());
		TurnTimerUpdateListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(update);
		}
	}

	private void FireCantPlayEvent(Entity entity)
	{
		CantPlayListener[] array = m_cantPlayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(entity);
		}
	}

	private void FireCurrentPlayerChangedEvent(Player player)
	{
		CurrentPlayerChangedListener[] array = m_currentPlayerChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(player);
		}
	}

	private void FireSpectatorNotifyEvent(SpectatorNotify notify)
	{
		SpectatorNotifyListener[] array = m_spectatorNotifyListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(notify);
		}
	}

	private void FireGameOverEvent(TAG_PLAYSTATE playState)
	{
		GameOverListener[] array = m_gameOverListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(playState);
		}
	}

	public void FireHeroChangedEvent(Player player)
	{
		HeroChangedListener[] array = m_heroChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(player);
		}
	}

	public ResponseMode GetResponseMode()
	{
		return m_responseMode;
	}

	public Network.EntityChoices GetFriendlyEntityChoices()
	{
		int friendlyPlayerId = GetFriendlyPlayerId();
		return GetEntityChoices(friendlyPlayerId);
	}

	public Network.EntityChoices GetOpponentEntityChoices()
	{
		int opposingPlayerId = GetOpposingPlayerId();
		return GetEntityChoices(opposingPlayerId);
	}

	public Network.EntityChoices GetEntityChoices(int playerId)
	{
		m_choicesMap.TryGetValue(playerId, out var value);
		return value;
	}

	public Map<int, Network.EntityChoices> GetEntityChoicesMap()
	{
		return m_choicesMap;
	}

	public bool IsChoosableEntity(Entity entity)
	{
		return GetFriendlyEntityChoices()?.Entities.Contains(entity.GetEntityId()) ?? false;
	}

	public bool IsChosenEntity(Entity entity)
	{
		if (GetFriendlyEntityChoices() == null)
		{
			return false;
		}
		return m_chosenEntities.Contains(entity);
	}

	public bool AddChosenEntity(Entity entity)
	{
		if (m_chosenEntities.Contains(entity))
		{
			return false;
		}
		m_chosenEntities.Add(entity);
		ChoiceCardMgr.Get().OnChosenEntityAdded(entity);
		Card card = entity.GetCard();
		if (card != null)
		{
			card.UpdateActorState();
		}
		return true;
	}

	public bool RemoveChosenEntity(Entity entity)
	{
		if (!m_chosenEntities.Remove(entity))
		{
			return false;
		}
		ChoiceCardMgr.Get().OnChosenEntityRemoved(entity);
		Card card = entity.GetCard();
		if (card != null)
		{
			card.UpdateActorState();
		}
		return true;
	}

	public List<Entity> GetChosenEntities()
	{
		return m_chosenEntities;
	}

	public Network.Options GetOptionsPacket()
	{
		return m_options;
	}

	public void EnterChoiceMode()
	{
		m_responseMode = ResponseMode.CHOICE;
		UpdateOptionHighlights();
		UpdateChoiceHighlights();
	}

	public void EnterMainOptionMode()
	{
		ResponseMode responseMode = m_responseMode;
		m_responseMode = ResponseMode.OPTION;
		switch (responseMode)
		{
		case ResponseMode.SUB_OPTION:
		{
			Network.Options.Option option2 = m_options.List[m_selectedOption.m_main];
			UpdateSubOptionHighlights(option2);
			break;
		}
		case ResponseMode.OPTION_TARGET:
		{
			Network.Options.Option option = m_options.List[m_selectedOption.m_main];
			UpdateTargetHighlights(option.Main);
			if (m_selectedOption.m_sub != -1)
			{
				Network.Options.Option.SubOption subOption = option.Subs[m_selectedOption.m_sub];
				UpdateTargetHighlights(subOption);
			}
			break;
		}
		}
		UpdateOptionHighlights(m_lastOptions);
		UpdateOptionHighlights();
		m_selectedOption.Clear();
	}

	public void EnterSubOptionMode()
	{
		Network.Options.Option option = m_options.List[m_selectedOption.m_main];
		if (m_responseMode == ResponseMode.OPTION)
		{
			m_responseMode = ResponseMode.SUB_OPTION;
			UpdateOptionHighlights();
		}
		else if (m_responseMode == ResponseMode.OPTION_TARGET)
		{
			m_responseMode = ResponseMode.SUB_OPTION;
			Network.Options.Option.SubOption subOption = option.Subs[m_selectedOption.m_sub];
			UpdateTargetHighlights(subOption);
		}
		UpdateSubOptionHighlights(option);
	}

	public void EnterOptionTargetMode()
	{
		if (m_responseMode == ResponseMode.OPTION)
		{
			m_responseMode = ResponseMode.OPTION_TARGET;
			UpdateOptionHighlights();
			Network.Options.Option option = m_options.List[m_selectedOption.m_main];
			UpdateTargetHighlights(option.Main);
		}
		else if (m_responseMode == ResponseMode.SUB_OPTION)
		{
			m_responseMode = ResponseMode.OPTION_TARGET;
			Network.Options.Option option2 = m_options.List[m_selectedOption.m_main];
			UpdateSubOptionHighlights(option2);
			Network.Options.Option.SubOption subOption = option2.Subs[m_selectedOption.m_sub];
			UpdateTargetHighlights(subOption);
		}
	}

	public void EnterMoveMinionMode(Entity heldEntity, bool suppressGlow = false)
	{
		ActivateMoveMinionTargets(heldEntity, suppressGlow);
	}

	public void ExitMoveMinionMode()
	{
		DeactivateMoveMinionTargetHighlights();
	}

	public void CancelCurrentOptionMode()
	{
		if (IsInTargetMode())
		{
			GetGameEntity().NotifyOfTargetModeCancelled();
		}
		CancelSelectedOptionProposedMana();
		EnterMainOptionMode();
	}

	public bool IsInMainOptionMode()
	{
		return m_responseMode == ResponseMode.OPTION;
	}

	public bool IsInSubOptionMode()
	{
		return m_responseMode == ResponseMode.SUB_OPTION;
	}

	public bool IsInTargetMode()
	{
		return m_responseMode == ResponseMode.OPTION_TARGET;
	}

	public bool IsInChoiceMode()
	{
		return m_responseMode == ResponseMode.CHOICE;
	}

	public void SetSelectedOption(ChooseOption packet)
	{
		m_selectedOption.m_main = packet.Index;
		m_selectedOption.m_sub = packet.SubOption;
		m_selectedOption.m_target = packet.Target;
		m_selectedOption.m_position = packet.Position;
	}

	public void SetChosenEntities(ChooseEntities packet)
	{
		m_chosenEntities.Clear();
		foreach (int entity2 in packet.Entities)
		{
			Entity entity = GetEntity(entity2);
			if (entity != null)
			{
				m_chosenEntities.Add(entity);
			}
		}
	}

	public void SetSelectedOption(int index)
	{
		m_selectedOption.m_main = index;
	}

	public int GetSelectedOption()
	{
		return m_selectedOption.m_main;
	}

	public void SetSelectedSubOption(int index)
	{
		m_selectedOption.m_sub = index;
	}

	public int GetSelectedSubOption()
	{
		return m_selectedOption.m_sub;
	}

	public void SetSelectedOptionTarget(int target)
	{
		m_selectedOption.m_target = target;
	}

	public int GetSelectedOptionTarget()
	{
		return m_selectedOption.m_target;
	}

	public bool IsSelectedOptionFriendlyHero()
	{
		Entity hero = GetFriendlySidePlayer().GetHero();
		Network.Options.Option selectedNetworkOption = GetSelectedNetworkOption();
		if (selectedNetworkOption != null)
		{
			return selectedNetworkOption.Main.ID == hero.GetEntityId();
		}
		return false;
	}

	public bool IsSelectedOptionFriendlyHeroPower()
	{
		Entity heroPower = GetFriendlySidePlayer().GetHeroPower();
		if (heroPower == null)
		{
			return false;
		}
		Network.Options.Option selectedNetworkOption = GetSelectedNetworkOption();
		if (selectedNetworkOption != null)
		{
			return selectedNetworkOption.Main.ID == heroPower.GetEntityId();
		}
		return false;
	}

	public void SetSelectedOptionPosition(int position)
	{
		m_selectedOption.m_position = position;
	}

	public int GetSelectedOptionPosition()
	{
		return m_selectedOption.m_position;
	}

	public Network.Options.Option GetSelectedNetworkOption()
	{
		if (m_selectedOption.m_main < 0)
		{
			return null;
		}
		return m_options.List[m_selectedOption.m_main];
	}

	public Network.Options.Option.SubOption GetSelectedNetworkSubOption()
	{
		if (m_selectedOption.m_main < 0)
		{
			return null;
		}
		Network.Options.Option option = m_options.List[m_selectedOption.m_main];
		if (m_selectedOption.m_sub == -1)
		{
			return option.Main;
		}
		return option.Subs[m_selectedOption.m_sub];
	}

	public bool EntityHasSubOptions(Entity entity)
	{
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.ID == entityId)
			{
				if (option.Subs != null)
				{
					return option.Subs.Count > 0;
				}
				return false;
			}
		}
		return false;
	}

	public bool EntityHasTargets(Entity entity)
	{
		return EntityHasTargets(entity, isSubEntity: false);
	}

	public bool SubEntityHasTargets(Entity subEntity)
	{
		return EntityHasTargets(subEntity, isSubEntity: true);
	}

	public bool HasSubOptions(Entity entity)
	{
		if (!IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = GetOptionsPacket();
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.ID == entityId)
			{
				return option.Subs.Count > 0;
			}
		}
		return false;
	}

	public int? GetErrorParam(Entity entity)
	{
		Network.Options optionsPacket = GetOptionsPacket();
		if (optionsPacket == null)
		{
			return null;
		}
		switch (GetResponseMode())
		{
		case ResponseMode.OPTION:
		{
			Network.Options.Option optionFromEntityID = optionsPacket.GetOptionFromEntityID(entity.GetEntityId());
			if (optionFromEntityID != null && optionFromEntityID.Type == Network.Options.Option.OptionType.POWER)
			{
				return optionFromEntityID.Main.PlayErrorInfo.PlayErrorParam;
			}
			break;
		}
		case ResponseMode.SUB_OPTION:
		{
			Network.Options.Option.SubOption subOptionFromEntityID = GetSelectedNetworkOption().GetSubOptionFromEntityID(entity.GetEntityId());
			if (subOptionFromEntityID != null)
			{
				return subOptionFromEntityID.PlayErrorInfo.PlayErrorParam;
			}
			break;
		}
		case ResponseMode.OPTION_TARGET:
			return GetSelectedNetworkSubOption().GetErrorParamForTarget(entity.GetEntityId());
		}
		return null;
	}

	public PlayErrors.ErrorType GetErrorType(Entity entity)
	{
		Network.Options optionsPacket = GetOptionsPacket();
		if (optionsPacket == null || !Get().IsFriendlySidePlayerTurn())
		{
			return PlayErrors.ErrorType.REQ_YOUR_TURN;
		}
		switch (GetResponseMode())
		{
		case ResponseMode.OPTION:
		{
			Network.Options.Option optionFromEntityID = optionsPacket.GetOptionFromEntityID(entity.GetEntityId());
			if (optionFromEntityID != null && optionFromEntityID.Type == Network.Options.Option.OptionType.POWER)
			{
				return optionFromEntityID.Main.PlayErrorInfo.PlayError;
			}
			break;
		}
		case ResponseMode.SUB_OPTION:
		{
			Network.Options.Option.SubOption subOptionFromEntityID = GetSelectedNetworkOption().GetSubOptionFromEntityID(entity.GetEntityId());
			if (subOptionFromEntityID != null)
			{
				return subOptionFromEntityID.PlayErrorInfo.PlayError;
			}
			break;
		}
		case ResponseMode.OPTION_TARGET:
			return GetSelectedNetworkSubOption().GetErrorForTarget(entity.GetEntityId());
		}
		return PlayErrors.ErrorType.INVALID;
	}

	public bool HasResponse(Entity entity)
	{
		return GetResponseMode() switch
		{
			ResponseMode.CHOICE => IsChoice(entity), 
			ResponseMode.OPTION => IsValidOption(entity), 
			ResponseMode.SUB_OPTION => IsValidSubOption(entity), 
			ResponseMode.OPTION_TARGET => IsValidOptionTarget(entity, checkInputEnabled: true), 
			_ => false, 
		};
	}

	public bool IsChoice(Entity entity)
	{
		if (!IsEntityInputEnabled(entity))
		{
			return false;
		}
		if (!IsChoosableEntity(entity))
		{
			return false;
		}
		if (IsChosenEntity(entity))
		{
			return false;
		}
		return true;
	}

	public bool IsValidOption(Entity entity)
	{
		if (!IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.PlayErrorInfo.IsValid() && option.Main.ID == entityId)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsValidSubOption(Entity entity)
	{
		if (!IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options.Option selectedNetworkOption = GetSelectedNetworkOption();
		for (int i = 0; i < selectedNetworkOption.Subs.Count; i++)
		{
			Network.Options.Option.SubOption subOption = selectedNetworkOption.Subs[i];
			if (subOption.ID == entityId)
			{
				if (!subOption.PlayErrorInfo.IsValid())
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	public bool IsValidOptionTarget(Entity entity, bool checkInputEnabled)
	{
		if (checkInputEnabled && !IsEntityInputEnabled(entity))
		{
			return false;
		}
		return GetSelectedNetworkSubOption()?.IsValidTarget(entity.GetEntityId()) ?? false;
	}

	public bool IsEntityInputEnabled(Entity entity)
	{
		if (IsResponsePacketBlocked())
		{
			return false;
		}
		if (entity.IsBusy())
		{
			return false;
		}
		Card card = entity.GetCard();
		if (card != null)
		{
			if (!card.IsInputEnabled())
			{
				return false;
			}
			Zone zone = card.GetZone();
			if (zone != null && !zone.IsInputEnabled())
			{
				return false;
			}
		}
		return true;
	}

	private bool EntityHasTargets(Entity entity, bool isSubEntity)
	{
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type != Network.Options.Option.OptionType.POWER)
			{
				continue;
			}
			if (isSubEntity)
			{
				if (option.Subs == null)
				{
					continue;
				}
				for (int j = 0; j < option.Subs.Count; j++)
				{
					Network.Options.Option.SubOption subOption = option.Subs[j];
					if (subOption.ID == entityId)
					{
						return subOption.HasValidTarget();
					}
				}
			}
			else if (option.Main.ID == entityId)
			{
				return option.Main.HasValidTarget();
			}
		}
		return false;
	}

	private void CancelSelectedOptionProposedMana()
	{
		Network.Options.Option selectedNetworkOption = GetSelectedNetworkOption();
		if (selectedNetworkOption != null)
		{
			GetFriendlySidePlayer().CancelAllProposedMana(GetEntity(selectedNetworkOption.Main.ID));
		}
	}

	public void ClearResponseMode()
	{
		Log.Hand.Print("ClearResponseMode");
		m_responseMode = ResponseMode.NONE;
		if (m_options != null)
		{
			for (int i = 0; i < m_options.List.Count; i++)
			{
				Network.Options.Option option = m_options.List[i];
				if (option.Type == Network.Options.Option.OptionType.POWER)
				{
					GetEntity(option.Main.ID)?.ClearBattlecryFlag();
				}
			}
			UpdateHighlightsBasedOnSelection();
			UpdateOptionHighlights(m_options);
		}
		else if (GetFriendlyEntityChoices() != null)
		{
			UpdateChoiceHighlights();
		}
	}

	public void UpdateChoiceHighlights()
	{
		foreach (Network.EntityChoices value in m_choicesMap.Values)
		{
			Entity entity = GetEntity(value.Source);
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (card != null)
				{
					card.UpdateActorState();
				}
			}
			foreach (int entity3 in value.Entities)
			{
				Entity entity2 = GetEntity(entity3);
				if (entity2 != null)
				{
					Card card2 = entity2.GetCard();
					if (!(card2 == null))
					{
						card2.UpdateActorState();
					}
				}
			}
		}
		foreach (Entity chosenEntity in m_chosenEntities)
		{
			Card card3 = chosenEntity.GetCard();
			if (!(card3 == null))
			{
				card3.UpdateActorState();
			}
		}
	}

	private void UpdateHighlightsBasedOnSelection()
	{
		if (m_selectedOption.m_target != 0)
		{
			Network.Options.Option.SubOption selectedNetworkSubOption = GetSelectedNetworkSubOption();
			if (selectedNetworkSubOption != null)
			{
				UpdateTargetHighlights(selectedNetworkSubOption);
			}
		}
		else if (m_selectedOption.m_sub >= 0)
		{
			Network.Options.Option selectedNetworkOption = GetSelectedNetworkOption();
			UpdateSubOptionHighlights(selectedNetworkOption);
		}
	}

	public void UpdateOptionHighlights()
	{
		UpdateOptionHighlights(m_options);
	}

	public void UpdateOptionHighlights(Network.Options options)
	{
		if (options == null || options.List == null)
		{
			return;
		}
		for (int i = 0; i < options.List.Count; i++)
		{
			Network.Options.Option option = options.List[i];
			if (option.Type != Network.Options.Option.OptionType.POWER)
			{
				continue;
			}
			Entity entity = GetEntity(option.Main.ID);
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (!(card == null))
				{
					card.UpdateActorState();
				}
			}
		}
	}

	private void UpdateSubOptionHighlights(Network.Options.Option option)
	{
		Entity entity = GetEntity(option.Main.ID);
		if (entity != null)
		{
			Card card = entity.GetCard();
			if (card != null)
			{
				card.UpdateActorState();
			}
		}
		foreach (Network.Options.Option.SubOption sub in option.Subs)
		{
			Entity entity2 = GetEntity(sub.ID);
			if (entity2 != null)
			{
				Card card2 = entity2.GetCard();
				if (!(card2 == null))
				{
					card2.UpdateActorState();
				}
			}
		}
	}

	private void UpdateTargetHighlights(Network.Options.Option.SubOption subOption)
	{
		Entity entity = GetEntity(subOption.ID);
		if (entity != null)
		{
			Card card = entity.GetCard();
			if (card != null)
			{
				card.UpdateActorState();
			}
		}
		foreach (Network.Options.Option.TargetOption target in subOption.Targets)
		{
			if (!target.PlayErrorInfo.IsValid())
			{
				continue;
			}
			int iD = target.ID;
			Entity entity2 = GetEntity(iD);
			if (entity2 != null)
			{
				Card card2 = entity2.GetCard();
				if (!(card2 == null))
				{
					card2.UpdateActorState();
				}
			}
		}
	}

	public void DisableOptionHighlights(Network.Options options)
	{
		if (options == null || options.List == null)
		{
			return;
		}
		for (int i = 0; i < options.List.Count; i++)
		{
			Network.Options.Option option = options.List[i];
			if (option.Type != Network.Options.Option.OptionType.POWER)
			{
				continue;
			}
			Entity entity = GetEntity(option.Main.ID);
			if (entity == null)
			{
				continue;
			}
			Card card = entity.GetCard();
			if (!(card == null))
			{
				Actor actor = card.GetActor();
				if (!(actor == null))
				{
					actor.SetActorState(ActorStateType.CARD_IDLE);
				}
			}
		}
	}

	public bool HasValidHoverTargetForMovedMinion(Entity movedEntity, out PlayErrors.ErrorType mainOptionPlayError)
	{
		mainOptionPlayError = PlayErrors.ErrorType.INVALID;
		List<Card> moveMinionHoverTargetsInPlay = GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any())
		{
			return false;
		}
		if (m_options == null || m_options.List == null)
		{
			return false;
		}
		foreach (Network.Options.Option option in m_options.List)
		{
			if (moveMinionHoverTargetsInPlay.FirstOrDefault((Card t) => t.GetEntity().GetEntityId() == option.Main.ID) == null)
			{
				continue;
			}
			if (!option.Main.PlayErrorInfo.IsValid())
			{
				if (option.Main.PlayErrorInfo.PlayError != PlayErrors.ErrorType.INVALID)
				{
					mainOptionPlayError = option.Main.PlayErrorInfo.PlayError;
				}
			}
			else if (option.Main.IsValidTarget(movedEntity.GetEntityId()))
			{
				return true;
			}
		}
		if (movedEntity.IsDormant())
		{
			mainOptionPlayError = PlayErrors.ErrorType.REQ_TARGET_NOT_DORMANT;
		}
		return false;
	}

	private void ActivateMoveMinionTargets(Entity movedEntity, bool suppressGlow = false)
	{
		if (movedEntity == null)
		{
			return;
		}
		DisableOptionHighlights(m_options);
		List<Card> moveMinionHoverTargetsInPlay = GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any() || m_options == null || m_options.List == null)
		{
			return;
		}
		foreach (Network.Options.Option option in m_options.List)
		{
			Card card = moveMinionHoverTargetsInPlay.FirstOrDefault((Card t) => t.GetEntity().GetEntityId() == option.Main.ID);
			if (card == null || !card.HasCardDef)
			{
				continue;
			}
			PlayMakerFSM cardDefComponent = card.GetCardDefComponent<PlayMakerFSM>();
			if (!(cardDefComponent == null))
			{
				bool flag = option.Main.IsValidTarget(movedEntity.GetEntityId());
				cardDefComponent.Fsm.GetFsmGameObject("HoverTargetCard").Value = card.gameObject;
				cardDefComponent.Fsm.GetFsmBool("SuppressGlow").Value = suppressGlow || !flag;
				cardDefComponent.SendEvent("Action");
				if (flag)
				{
					ManaCrystalMgr.Get().ProposeManaCrystalUsage(card.GetEntity());
				}
			}
		}
	}

	private void DeactivateMoveMinionTargetHighlights()
	{
		List<Card> moveMinionHoverTargetsInPlay = GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any())
		{
			return;
		}
		foreach (Card item in moveMinionHoverTargetsInPlay)
		{
			if (item.HasCardDef)
			{
				PlayMakerFSM cardDefComponent = item.GetCardDefComponent<PlayMakerFSM>();
				if (!(cardDefComponent == null))
				{
					cardDefComponent.SendEvent("Death");
					ManaCrystalMgr.Get().CancelAllProposedMana(item.GetEntity());
				}
			}
		}
		UpdateOptionHighlights();
	}

	public bool HasEnoughManaForMoveMinionHoverTarget(Entity heldEntity)
	{
		Player friendlySidePlayer = GetFriendlySidePlayer();
		List<Card> moveMinionHoverTargetsInPlay = GetMoveMinionHoverTargetsInPlay();
		foreach (Network.Options.Option option in m_options.List)
		{
			if (option.Main.IsValidTarget(heldEntity.GetEntityId()))
			{
				Card card = moveMinionHoverTargetsInPlay.FirstOrDefault((Card t) => t.GetEntity().GetEntityId() == option.Main.ID);
				if (!(card == null) && friendlySidePlayer.GetNumAvailableResources() >= card.GetEntity().GetCost())
				{
					return true;
				}
			}
		}
		return moveMinionHoverTargetsInPlay.Count <= 0;
	}

	private List<Card> GetMoveMinionHoverTargetsInPlay()
	{
		List<ZoneMoveMinionHoverTarget> list = ZoneMgr.Get().FindZonesOfType<ZoneMoveMinionHoverTarget>(Player.Side.FRIENDLY);
		List<Card> moveMinionHoverTargets = new List<Card>();
		list.ForEach(delegate(ZoneMoveMinionHoverTarget z)
		{
			moveMinionHoverTargets.AddRange(z.GetCards());
		});
		return moveMinionHoverTargets;
	}

	public Network.Options GetLastOptions()
	{
		return m_lastOptions;
	}

	public bool FriendlyHeroIsTargetable()
	{
		if (m_responseMode == ResponseMode.OPTION_TARGET)
		{
			Network.Options.Option option = m_options.List[m_selectedOption.m_main];
			foreach (Network.Options.Option.TargetOption target in ((m_selectedOption.m_sub != -1) ? option.Subs[m_selectedOption.m_sub] : option.Main).Targets)
			{
				if (target.PlayErrorInfo.IsValid())
				{
					int iD = target.ID;
					Entity entity = GetEntity(iD);
					if (entity != null && !(entity.GetCard() == null) && entity.IsHero() && entity.IsControlledByFriendlySidePlayer())
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void ClearLastOptions()
	{
		m_lastOptions = null;
		m_lastSelectedOption = null;
	}

	private void ClearOptions()
	{
		m_options = null;
		m_selectedOption.Clear();
	}

	private void ClearFriendlyChoices()
	{
		m_chosenEntities.Clear();
		int friendlyPlayerId = GetFriendlyPlayerId();
		m_choicesMap.Remove(friendlyPlayerId);
	}

	private void OnSelectedOptionsSent()
	{
		ClearResponseMode();
		m_lastOptions = new Network.Options();
		m_lastOptions.CopyFrom(m_options);
		m_lastSelectedOption = new SelectedOption();
		m_lastSelectedOption.CopyFrom(m_selectedOption);
		ClearOptions();
	}

	private void OnTimeout()
	{
		if (m_responseMode != 0)
		{
			ClearResponseMode();
			ClearLastOptions();
			ClearOptions();
		}
	}

	private void ClearEntityMap()
	{
		Entity[] array = m_entityMap.Values.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Destroy();
		}
		m_entityMap.Clear();
	}

	private void CleanGameState()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			zone.Reset();
		}
		ManaCrystalMgr.Get().Reset();
		foreach (Entity value in m_entityMap.Values)
		{
			Card card = value.GetCard();
			if (card != null)
			{
				card.DeactivatePlaySpell();
				card.CancelActiveSpells();
				card.CancelCustomSpells();
			}
		}
		foreach (Entity value2 in m_entityMap.Values)
		{
			Card card2 = value2.GetCard();
			if (card2 != null)
			{
				card2.Destroy();
			}
		}
		m_playerMap.Clear();
		m_entityMap.Clear();
		m_removedFromGameEntities.Clear();
		m_removedFromGameEntityLog.Clear();
	}

	private void CreateGameEntity(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		m_gameEntity = GameMgr.Get().CreateGameEntity(powerList, createGame);
		m_gameEntity.m_uuid = createGame.Uuid;
		m_gameEntity.SetTags(createGame.Game.Tags);
		m_gameEntity.InitRealTimeValues(createGame.Game.Tags);
		AddEntity(m_gameEntity);
		m_gameEntity.OnCreate();
	}

	public void OnRealTimeCreateGame(List<Network.PowerHistory> powerList, int index, Network.HistCreateGame createGame)
	{
		if (m_gameEntity != null)
		{
			Log.Power.PrintError("{0}.OnRealTimeCreateGame(): there is already a game entity!", this);
			m_gameEntity.OnDecommissionGame();
			CleanGameState();
		}
		if (powerList.Count == 1)
		{
			string text = "Game Created without entries:";
			text += $" BuildNumber={84593}";
			text += $" GameType={GameMgr.Get().GetGameType()}";
			text += $" FormatType={GameMgr.Get().GetFormatType()}";
			text += $" ScenarioID={GameMgr.Get().GetMissionId()}";
			text += $" IsReconnect={GameMgr.Get().IsReconnect()}";
			if (GameMgr.Get().IsReconnect())
			{
				text += $" ReconnectType={GameMgr.Get().GetReconnectType()}";
			}
			Log.Power.Print(text);
			TelemetryManager.Client().SendLiveIssue("Gameplay_GameState", text);
		}
		CreateGameEntity(powerList, createGame);
		foreach (Network.HistCreateGame.PlayerData player2 in createGame.Players)
		{
			Player player = new Player();
			player.InitPlayer(player2);
			AddPlayer(player);
		}
		int friendlySideTeamId = GetFriendlySideTeamId();
		foreach (Player value in m_playerMap.Values)
		{
			value.UpdateSide(friendlySideTeamId);
		}
		foreach (Network.HistCreateGame.SharedPlayerInfo playerInfo in createGame.PlayerInfos)
		{
			SharedPlayerInfo sharedPlayerInfo = new SharedPlayerInfo();
			sharedPlayerInfo.InitPlayerInfo(playerInfo);
			AddPlayerInfo(sharedPlayerInfo);
		}
		m_createGamePhase = CreateGamePhase.CREATING;
		FireCreateGameEvent();
		if (m_gameEntity.HasTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD))
		{
			HandleWaitForOpponentReconnectPeriod(m_gameEntity.GetTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD));
		}
		DebugPrintGame();
	}

	public bool OnRealTimeFullEntity(Network.HistFullEntity fullEntity)
	{
		Entity entity = new Entity();
		entity.OnRealTimeFullEntity(fullEntity);
		AddEntity(entity);
		return true;
	}

	public bool OnFullEntity(Network.HistFullEntity fullEntity)
	{
		Network.Entity entity = fullEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnFullEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.OnFullEntity(fullEntity);
		return true;
	}

	public bool OnRealTimeShowEntity(Network.HistShowEntity showEntity)
	{
		if (EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeShowEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.OnRealTimeShowEntity(showEntity);
		return true;
	}

	public bool OnShowEntity(Network.HistShowEntity showEntity)
	{
		if (EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnShowEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.OnShowEntity(showEntity);
		return true;
	}

	public bool OnEarlyConcedeShowEntity(Network.HistShowEntity showEntity)
	{
		if (EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeShowEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.SetTags(entity.Tags);
		return true;
	}

	public bool OnHideEntity(Network.HistHideEntity hideEntity)
	{
		if (EntityRemovedFromGame(hideEntity.Entity))
		{
			return false;
		}
		Entity entity = GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnHideEntity() - WARNING entity {0} DOES NOT EXIST! zone={1}", hideEntity.Entity, hideEntity.Zone);
			return false;
		}
		entity.OnHideEntity(hideEntity);
		return true;
	}

	public bool OnEarlyConcedeHideEntity(Network.HistHideEntity hideEntity)
	{
		if (EntityRemovedFromGame(hideEntity.Entity))
		{
			return false;
		}
		Entity entity = GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeHideEntity() - WARNING entity {0} DOES NOT EXIST! zone={1}", hideEntity.Entity, hideEntity.Zone);
			return false;
		}
		entity.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
		return true;
	}

	public bool OnRealTimeChangeEntity(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		if (EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeChangeEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.OnRealTimeChangeEntity(powerList, index, changeEntity);
		return true;
	}

	public bool OnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnChangeEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.OnChangeEntity(changeEntity);
		return true;
	}

	public bool OnEarlyConcedeChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		Entity entity2 = GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeChangeEntity() - WARNING entity {0} DOES NOT EXIST!", entity.ID);
			return false;
		}
		entity2.SetTags(entity.Tags);
		return true;
	}

	public bool OnRealTimeTagChange(Network.HistTagChange change)
	{
		if (EntityRemovedFromGame(change.Entity))
		{
			return false;
		}
		Entity entity = Get().GetEntity(change.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeTagChange() - WARNING Entity {0} does not exist", change.Entity);
			return false;
		}
		if (change.ChangeDef)
		{
			return false;
		}
		PreprocessRealTimeTagChange(entity, change);
		m_gameEntity.NotifyOfRealTimeTagChange(entity, change);
		entity.OnRealTimeTagChanged(change);
		return true;
	}

	public bool OnTagChange(Network.HistTagChange netChange)
	{
		if (EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		Entity entity = GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnTagChange() - WARNING Entity {0} does not exist", netChange.Entity);
			return false;
		}
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = netChange.Tag;
		tagDelta.oldValue = entity.GetTag(netChange.Tag);
		tagDelta.newValue = netChange.Value;
		if (netChange.ChangeDef)
		{
			entity.GetOrCreateDynamicDefinition().SetTag(tagDelta.tag, tagDelta.newValue);
		}
		else
		{
			entity.SetTag(tagDelta.tag, tagDelta.newValue);
		}
		PreprocessTagChange(entity, tagDelta);
		entity.OnTagChanged(tagDelta);
		return true;
	}

	public void OnRealTimeVoSpell(Network.HistVoSpell voSpell)
	{
		if (voSpell != null)
		{
			SoundLoader.LoadSound(new AssetReference(voSpell.SpellPrefabGUID), OnSoundLoaded, voSpell, SoundManager.Get().GetPlaceholderSound());
		}
	}

	public bool OnCachedTagForDormantChange(Network.HistCachedTagForDormantChange netChange)
	{
		if (EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		Entity entity = GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnCachedTagForDormantChange() - WARNING Entity {0} does not exist", netChange.Entity);
			return false;
		}
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = netChange.Tag;
		tagDelta.oldValue = entity.GetTag(netChange.Tag);
		tagDelta.newValue = netChange.Value;
		entity.OnCachedTagForDormantChanged(tagDelta);
		return true;
	}

	public bool OnShuffleDeck(Network.HistShuffleDeck shuffleDeck)
	{
		Player player = GetPlayer(shuffleDeck.PlayerID);
		if (player == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnShuffleDeck() - WARNING Player for ID {0} does not exist", shuffleDeck.PlayerID);
			return false;
		}
		if (EntityRemovedFromGame(player.GetEntityId()))
		{
			return false;
		}
		player.OnShuffleDeck();
		return true;
	}

	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogWarning($"{MethodBase.GetCurrentMethod().Name} - FAILED to load \"{assetRef}\"");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("{0} - ERROR \"{1}\" has no {2} component", assetRef, MethodBase.GetCurrentMethod().Name, "AudioSource"));
			return;
		}
		Network.HistVoSpell histVoSpell = callbackData as Network.HistVoSpell;
		if (histVoSpell != null)
		{
			histVoSpell.m_audioSource = component;
			histVoSpell.m_ableToLoad = true;
		}
	}

	public bool OnVoSpell(Network.HistVoSpell voSpell)
	{
		if (voSpell == null)
		{
			return false;
		}
		if (!voSpell.m_ableToLoad)
		{
			return false;
		}
		AudioSource audioSource = voSpell.m_audioSource;
		if (audioSource == null || audioSource.clip == null)
		{
			return false;
		}
		float num = voSpell.AdditionalDelayMs;
		float num2 = audioSource.clip.length * 1000f;
		float num3 = num;
		if (voSpell.Blocking)
		{
			num3 += num2;
		}
		if (num3 > 0f)
		{
			Gameplay.Get().StartCoroutine(m_powerProcessor.ArtificiallyPausePowerProcessor(num3));
			if (m_gameEntity is MissionEntity)
			{
				(m_gameEntity as MissionEntity).SetBlockVo(shouldBlock: true, num3 / 1000f);
			}
		}
		string[] array = voSpell.SpellPrefabGUID.Split(':');
		if (array.Length != 2)
		{
			return false;
		}
		string text = array[0];
		if (!text.EndsWith(".prefab"))
		{
			return false;
		}
		string localizedTextKey = text.Substring(0, text.Length - ".prefab".Length);
		if (voSpell.Speaker != 0)
		{
			Actor actor = GetEntity(voSpell.Speaker)?.GetCard()?.GetActor();
			if (actor != null)
			{
				CharacterInPlaySpeak(voSpell, actor, localizedTextKey, num3);
			}
		}
		else if (!string.IsNullOrEmpty(voSpell.BrassRingGUID))
		{
			BrassRingCharacterSpeak(voSpell, localizedTextKey, num3);
		}
		return true;
	}

	private void CharacterInPlaySpeak(Network.HistVoSpell voSpell, Actor speakingActor, string localizedTextKey, float totalPauseTimeMs)
	{
		if (voSpell != null && !(speakingActor == null) && !string.IsNullOrEmpty(localizedTextKey) && !(totalPauseTimeMs < 0f))
		{
			if (voSpell.m_audioSource != null)
			{
				SoundManager.Get().PlayPreloaded(voSpell.m_audioSource);
			}
			Notification.SpeechBubbleDirection speechBubbleDirection = Notification.SpeechBubbleDirection.None;
			Entity entity = speakingActor.GetEntity();
			speechBubbleDirection = (entity.IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : ((!entity.IsMinion()) ? Notification.SpeechBubbleDirection.TopLeft : Notification.SpeechBubbleDirection.BottomLeft));
			if (totalPauseTimeMs > 0f && speechBubbleDirection != 0)
			{
				NotificationManager notificationManager = NotificationManager.Get();
				Notification notification = notificationManager.CreateSpeechBubble(parentToActor: !(speakingActor.GetCard() != null) || speakingActor.GetCard().GetEntity() == null || !speakingActor.GetCard().GetEntity().IsHeroPower(), speechText: GameStrings.Get(localizedTextKey), direction: speechBubbleDirection, actor: speakingActor, bDestroyWhenNewCreated: false);
				notificationManager.DestroyNotification(notification, totalPauseTimeMs / 1000f);
			}
		}
	}

	private void BrassRingCharacterSpeak(Network.HistVoSpell voSpell, string localizedTextKey, float soundLengthMs)
	{
		if (voSpell != null && !string.IsNullOrEmpty(localizedTextKey) && !(soundLengthMs <= 0f))
		{
			NotificationManager notificationManager = NotificationManager.Get();
			if (!(notificationManager == null))
			{
				Vector3 zero = Vector3.zero;
				Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None;
				notificationManager.CreateBigCharacterQuoteWithGameString(voSpell.BrassRingGUID, zero, voSpell.SpellPrefabGUID, localizedTextKey, allowRepeatDuringSession: true, soundLengthMs / 1000f, null, useOverlayUI: false, bubbleDir);
			}
		}
	}

	public bool OnEarlyConcedeTagChange(Network.HistTagChange netChange)
	{
		if (EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		Entity entity = GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnEarlyConcedeTagChange() - WARNING Entity {0} does not exist", netChange.Entity);
			return false;
		}
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = netChange.Tag;
		tagDelta.oldValue = entity.GetTag(netChange.Tag);
		tagDelta.newValue = netChange.Value;
		entity.SetTag(tagDelta.tag, tagDelta.newValue);
		PreprocessEarlyConcedeTagChange(entity, tagDelta);
		ProcessEarlyConcedeTagChange(entity, tagDelta);
		return true;
	}

	public bool OnRealTimeResetGame(Network.HistResetGame resetGame)
	{
		if (m_realTimeResetGame != null)
		{
			Log.Gameplay.PrintError("{0}.OnRealTimeResetGame: There is already a ResetGame task we're waiting to execute!", this);
		}
		m_realTimeResetGame = resetGame;
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			zone.AddInputBlocker();
		}
		return true;
	}

	public bool OnResetGame(Network.HistResetGame resetGame)
	{
		if (m_realTimeResetGame != resetGame)
		{
			Log.Power.PrintError("{0}.OnResetGame(): Passed ResetGame Task {0} does not match the expected ResetGame Task {1}!", this, resetGame, m_realTimeResetGame);
		}
		if (m_gameEntity != null)
		{
			m_gameEntity.OnDecommissionGame();
			CleanGameState();
		}
		List<Network.PowerHistory> list = new List<Network.PowerHistory>();
		foreach (PowerTask task in m_powerProcessor.GetCurrentTaskList().GetTaskList())
		{
			list.Add(task.GetPower());
		}
		CreateGameEntity(list, resetGame.CreateGame);
		foreach (Network.HistCreateGame.PlayerData player2 in resetGame.CreateGame.Players)
		{
			Player player = new Player();
			player.InitPlayer(player2);
			AddPlayer(player);
		}
		int friendlySideTeamId = GetFriendlySideTeamId();
		foreach (Player value in m_playerMap.Values)
		{
			value.UpdateSide(friendlySideTeamId);
			value.OnBoardLoaded();
		}
		m_realTimeResetGame = null;
		m_powerProcessor.FlushDelayedRealTimeTasks();
		return true;
	}

	public bool OnMetaData(Network.HistMetaData metaData)
	{
		m_powerProcessor.OnMetaData(metaData);
		foreach (int item in metaData.Info)
		{
			Entity entity = GetEntity(item);
			if (entity == null)
			{
				if (!EntityRemovedFromGame(item))
				{
					UnityEngine.Debug.LogWarning($"GameState.OnMetaData() - WARNING Entity {item} does not exist");
				}
				return false;
			}
			entity.OnMetaData(metaData);
		}
		return true;
	}

	public void OnTaskListEnded(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return;
		}
		foreach (PowerTask task in taskList.GetTaskList())
		{
			if (task.GetPower().Type == Network.PowerType.CREATE_GAME)
			{
				m_createGamePhase = CreateGamePhase.CREATED;
				FireCreateGameEvent();
				m_createGameListeners.Clear();
			}
		}
		RemoveQueuedEntitiesFromGame();
	}

	public void OnPowerHistory(List<Network.PowerHistory> powerList)
	{
		DebugPrintPowerList(powerList);
		bool num = m_powerProcessor.HasEarlyConcedeTaskList();
		m_powerProcessor.OnPowerHistory(powerList);
		ProcessAllQueuedChoices();
		bool flag = m_powerProcessor.HasEarlyConcedeTaskList();
		if (!num && flag)
		{
			OnReceivedEarlyConcede();
		}
	}

	private void OnReceivedEarlyConcede()
	{
		ClearResponseMode();
		ClearLastOptions();
		ClearOptions();
	}

	public void OnAllOptions(Network.Options options)
	{
		m_responseMode = ResponseMode.OPTION;
		m_chosenEntities.Clear();
		if (m_options != null && (m_lastOptions == null || m_lastOptions.ID < m_options.ID))
		{
			m_lastOptions = new Network.Options();
			m_lastOptions.CopyFrom(m_options);
		}
		m_options = options;
		foreach (Network.Options.Option item in m_options.List)
		{
			if (item.Type == Network.Options.Option.OptionType.POWER)
			{
				Entity entity = GetEntity(item.Main.ID);
				if (entity != null && item.Main.Targets != null && item.Main.Targets.Count > 0)
				{
					entity.UpdateUseBattlecryFlag(fromGameState: true);
				}
			}
		}
		DebugPrintOptions(Log.Power);
		EnterMainOptionMode();
		FireOptionsReceivedEvent();
	}

	public void OnEntityChoices(Network.EntityChoices choices)
	{
		PowerTaskList lastTaskList = m_powerProcessor.GetLastTaskList();
		if (!CanProcessEntityChoices(choices))
		{
			Log.Power.Print("GameState.OnEntityChoices() - id={0} playerId={1} queued", choices.ID, choices.PlayerId);
			QueuedChoice item = new QueuedChoice
			{
				m_type = QueuedChoice.PacketType.ENTITY_CHOICES,
				m_packet = choices,
				m_eventData = lastTaskList
			};
			m_queuedChoices.Enqueue(item);
		}
		else
		{
			ProcessEntityChoices(choices, lastTaskList);
		}
	}

	public void OnEntitiesChosen(Network.EntitiesChosen chosen)
	{
		if (!CanProcessEntitiesChosen(chosen))
		{
			Log.Power.Print("GameState.OnEntitiesChosen() - id={0} playerId={1} queued", chosen.ID, chosen.PlayerId);
			QueuedChoice item = new QueuedChoice
			{
				m_type = QueuedChoice.PacketType.ENTITIES_CHOSEN,
				m_packet = chosen
			};
			m_queuedChoices.Enqueue(item);
		}
		else
		{
			ProcessEntitiesChosen(chosen);
		}
	}

	public float GetClientLostTimeCatchUpThreshold()
	{
		return m_clientLostTimeCatchUpThreshold;
	}

	public bool ShouldUseSlushTimeTracker()
	{
		return m_useSlushTimeCatchUp;
	}

	public bool ShoudRestrictLostTimeCatchUpToLowEndDevices()
	{
		return m_restrictClientLostTimeCatchUpToLowEndDevices;
	}

	public void UpdateGameGuardianVars(GameGuardianVars gameGuardianVars)
	{
		m_clientLostTimeCatchUpThreshold = (gameGuardianVars.HasClientLostFrameTimeCatchUpThreshold ? gameGuardianVars.ClientLostFrameTimeCatchUpThreshold : 0f);
		m_useSlushTimeCatchUp = gameGuardianVars.HasClientLostFrameTimeCatchUpUseSlush && gameGuardianVars.ClientLostFrameTimeCatchUpUseSlush;
		m_restrictClientLostTimeCatchUpToLowEndDevices = gameGuardianVars.HasClientLostFrameTimeCatchUpLowEndOnly && gameGuardianVars.ClientLostFrameTimeCatchUpLowEndOnly;
		m_allowDeferredPowers = !gameGuardianVars.HasGameAllowDeferredPowers || gameGuardianVars.GameAllowDeferredPowers;
		m_allowBatchedPowers = !gameGuardianVars.HasGameAllowBatchedPowers || gameGuardianVars.GameAllowBatchedPowers;
		m_allowDiamondCards = !gameGuardianVars.HasGameAllowDiamondCards || gameGuardianVars.GameAllowDiamondCards;
	}

	public void UpdateBattlegroundInfo(UpdateBattlegroundInfo battlegroundMinionPoolDenyList)
	{
		m_battlegroundMinionPool = (battlegroundMinionPoolDenyList.HasBattlegroundMinionPool ? battlegroundMinionPoolDenyList.BattlegroundMinionPool : "Battleground minion pool not available");
		if (m_printBattlegroundMinionPoolOnUpdate)
		{
			Log.All.Print(m_battlegroundMinionPool);
			m_printBattlegroundMinionPoolOnUpdate = false;
		}
		m_battlegroundDenyList = (battlegroundMinionPoolDenyList.HasBattlegroundDenyList ? battlegroundMinionPoolDenyList.BattlegroundDenyList : "Battle ground deny list not available");
		if (m_printBattlegroundDenyListOnUpdate)
		{
			Log.All.Print(m_battlegroundDenyList);
			m_printBattlegroundDenyListOnUpdate = false;
		}
	}

	private bool CanProcessEntityChoices(Network.EntityChoices choices)
	{
		int playerId = choices.PlayerId;
		if (!m_playerMap.ContainsKey(playerId))
		{
			return false;
		}
		foreach (int entity in choices.Entities)
		{
			if (!m_entityMap.ContainsKey(entity))
			{
				return false;
			}
		}
		if (m_choicesMap.ContainsKey(playerId))
		{
			return false;
		}
		return true;
	}

	private bool CanProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		if (!m_playerMap.ContainsKey(playerId))
		{
			return false;
		}
		foreach (int entity in chosen.Entities)
		{
			if (!m_entityMap.ContainsKey(entity))
			{
				return false;
			}
		}
		if (m_choicesMap.TryGetValue(playerId, out var value) && value.ID != chosen.ID)
		{
			return false;
		}
		return true;
	}

	private void ProcessAllQueuedChoices()
	{
		while (m_queuedChoices.Count > 0)
		{
			QueuedChoice queuedChoice = m_queuedChoices.Peek();
			switch (queuedChoice.m_type)
			{
			case QueuedChoice.PacketType.ENTITY_CHOICES:
			{
				Network.EntityChoices choices = (Network.EntityChoices)queuedChoice.m_packet;
				if (!CanProcessEntityChoices(choices))
				{
					return;
				}
				m_queuedChoices.Dequeue();
				PowerTaskList preChoiceTaskList = (PowerTaskList)queuedChoice.m_eventData;
				ProcessEntityChoices(choices, preChoiceTaskList);
				break;
			}
			case QueuedChoice.PacketType.ENTITIES_CHOSEN:
			{
				Network.EntitiesChosen chosen = (Network.EntitiesChosen)queuedChoice.m_packet;
				if (!CanProcessEntitiesChosen(chosen))
				{
					return;
				}
				m_queuedChoices.Dequeue();
				ProcessEntitiesChosen(chosen);
				break;
			}
			}
		}
	}

	private void ProcessEntityChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		DebugPrintEntityChoices(choices, preChoiceTaskList);
		if (!m_powerProcessor.HasEarlyConcedeTaskList())
		{
			int playerId = choices.PlayerId;
			m_choicesMap[playerId] = choices;
			int friendlyPlayerId = GetFriendlyPlayerId();
			if (playerId == friendlyPlayerId)
			{
				m_responseMode = ResponseMode.CHOICE;
				m_chosenEntities.Clear();
				EnterChoiceMode();
			}
			FireEntityChoicesReceivedEvent(choices, preChoiceTaskList);
		}
	}

	private void ProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		DebugPrintEntitiesChosen(chosen);
		if (!m_powerProcessor.HasEarlyConcedeTaskList() && !FireEntitiesChosenReceivedEvent(chosen))
		{
			OnEntitiesChosenProcessed(chosen);
		}
	}

	public void OnGameSetup(Network.GameSetup setup)
	{
		m_maxSecretZoneSizePerPlayer = setup.MaxSecretZoneSizePerPlayer;
		m_maxSecretsPerPlayer = setup.MaxSecretsPerPlayer;
		m_maxQuestsPerPlayer = setup.MaxQuestsPerPlayer;
		m_maxFriendlyMinionsPerPlayer = setup.MaxFriendlyMinionsPerPlayer;
	}

	public void QueueEntityForRemoval(Entity entity)
	{
		m_removedFromGameEntities.Enqueue(entity);
	}

	public void OnOptionRejected(int optionId)
	{
		if (m_lastSelectedOption == null)
		{
			UnityEngine.Debug.LogError("GameState.OnOptionRejected() - got an option rejection without a last selected option");
		}
		else if (m_lastOptions.ID != optionId)
		{
			UnityEngine.Debug.LogErrorFormat("GameState.OnOptionRejected() - rejected option id ({0}) does not match last option id ({1})", optionId, m_lastOptions.ID);
		}
		else
		{
			Network.Options.Option option = m_lastOptions.List[m_lastSelectedOption.m_main];
			FireOptionRejectedEvent(option);
			ClearLastOptions();
		}
	}

	public void OnTurnTimerUpdate(Network.TurnTimerInfo info)
	{
		TurnTimerUpdate turnTimerUpdate = new TurnTimerUpdate();
		turnTimerUpdate.SetSecondsRemaining(info.Seconds);
		turnTimerUpdate.SetEndTimestamp(Time.realtimeSinceStartup + info.Seconds);
		turnTimerUpdate.SetShow(info.Show);
		if (IsMulliganManagerActive() && m_gameEntity != null && GetBooleanGameOption(GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER))
		{
			turnTimerUpdate.SetShow(show: true);
		}
		int turn = GetTurn();
		if (info.Turn > turn)
		{
			m_turnTimerUpdates[info.Turn] = turnTimerUpdate;
		}
		else
		{
			TriggerTurnTimerUpdate(turnTimerUpdate);
		}
	}

	public void TriggerTurnTimerUpdateForTurn(int turn)
	{
		OnTurnChanged_TurnTimer(GetTurn(), turn);
	}

	public void OnSpectatorNotifyEvent(SpectatorNotify notify)
	{
		FireSpectatorNotifyEvent(notify);
	}

	public void SendChoices()
	{
		if (m_responseMode != ResponseMode.CHOICE)
		{
			return;
		}
		Network.EntityChoices friendlyEntityChoices = GetFriendlyEntityChoices();
		if (friendlyEntityChoices != null && m_chosenEntities.Count >= friendlyEntityChoices.CountMin && m_chosenEntities.Count <= friendlyEntityChoices.CountMax)
		{
			ChoiceCardMgr.Get().OnSendChoices(friendlyEntityChoices, m_chosenEntities);
			Log.Power.Print("GameState.SendChoices() - id={0} ChoiceType={1}", friendlyEntityChoices.ID, friendlyEntityChoices.ChoiceType);
			List<int> list = new List<int>();
			for (int i = 0; i < m_chosenEntities.Count; i++)
			{
				Entity entity = m_chosenEntities[i];
				int entityId = entity.GetEntityId();
				Log.Power.Print("GameState.SendChoices() -   m_chosenEntities[{0}]={1}", i, entity);
				list.Add(entityId);
			}
			if (!GameMgr.Get().IsSpectator())
			{
				Network.Get().SendChoices(friendlyEntityChoices.ID, list);
			}
			ClearResponseMode();
		}
	}

	public void OnEntitiesChosenProcessed(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		int friendlyPlayerId = GetFriendlyPlayerId();
		if (playerId == friendlyPlayerId)
		{
			if (m_responseMode == ResponseMode.CHOICE)
			{
				ClearResponseMode();
			}
			ClearFriendlyChoices();
		}
		else
		{
			m_choicesMap.Remove(playerId);
		}
		ProcessAllQueuedChoices();
	}

	public void SendOption()
	{
		if (!GameMgr.Get().IsSpectator())
		{
			Network.Get().SendOption(m_options.ID, m_selectedOption.m_main, m_selectedOption.m_target, m_selectedOption.m_sub, m_selectedOption.m_position);
			Log.Power.Print("GameState.SendOption() - selectedOption={0} selectedSubOption={1} selectedTarget={2} selectedPosition={3}", m_selectedOption.m_main, m_selectedOption.m_sub, m_selectedOption.m_target, m_selectedOption.m_position);
		}
		OnSelectedOptionsSent();
		Network.Options.Option option = m_lastOptions.List[m_lastSelectedOption.m_main];
		FireOptionsSentEvent(option);
	}

	private void OnTurnChanged_TurnTimer(int oldTurn, int newTurn)
	{
		if (m_turnTimerUpdates.Count != 0 && m_turnTimerUpdates.TryGetValue(newTurn, out var value))
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float endTimestamp = value.GetEndTimestamp();
			float secondsRemaining = Mathf.Max(0f, endTimestamp - realtimeSinceStartup);
			value.SetSecondsRemaining(secondsRemaining);
			TriggerTurnTimerUpdate(value);
			m_turnTimerUpdates.Remove(newTurn);
		}
	}

	private void TriggerTurnTimerUpdate(TurnTimerUpdate update)
	{
		FireTurnTimerUpdateEvent(update);
		if (!(update.GetSecondsRemaining() > Mathf.Epsilon))
		{
			OnTimeout();
		}
	}

	private void DebugPrintGame()
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		Log.Power.Print($"GameState.DebugPrintGame() - BuildNumber={84593}");
		Log.Power.Print($"GameState.DebugPrintGame() - GameType={GameMgr.Get().GetGameType()}");
		Log.Power.Print($"GameState.DebugPrintGame() - FormatType={GameMgr.Get().GetFormatType()}");
		Log.Power.Print($"GameState.DebugPrintGame() - ScenarioID={GameMgr.Get().GetMissionId()}");
		foreach (Player value in m_playerMap.Values)
		{
			Log.Power.Print($"GameState.DebugPrintGame() - PlayerID={value.GetPlayerId()}, PlayerName={GetEntityLogName(value.GetEntityId())}");
		}
	}

	private void DebugPrintPowerList(List<Network.PowerHistory> powerList)
	{
		if (Log.Power.CanPrint())
		{
			string indentation = "";
			Log.Power.Print($"GameState.DebugPrintPowerList() - Count={powerList.Count}");
			for (int i = 0; i < powerList.Count; i++)
			{
				Network.PowerHistory power = powerList[i];
				DebugPrintPower(Log.Power, "GameState", power, ref indentation);
			}
		}
	}

	public void DebugPrintPower(Logger logger, string callerName, Network.PowerHistory power)
	{
		string indentation = string.Empty;
		DebugPrintPower(logger, callerName, power, ref indentation);
	}

	public void DebugPrintPower(Logger logger, string callerName, Network.PowerHistory power, ref string indentation)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		switch (power.Type)
		{
		case Network.PowerType.BLOCK_START:
		{
			Network.HistBlockStart histBlockStart = (Network.HistBlockStart)power;
			string text = string.Empty;
			if (histBlockStart.BlockType == HistoryBlock.Type.TRIGGER)
			{
				string arg = ((GAME_TAG)histBlockStart.TriggerKeyword).ToString();
				text = $"TriggerKeyword={arg}";
			}
			logger.Print("{0}.DebugPrintPower() - {1}BLOCK_START BlockType={2} Entity={3} EffectCardId={4} EffectIndex={5} Target={6} SubOption={7} {8}", callerName, indentation, histBlockStart.BlockType, GetEntitiesLogNames(histBlockStart.Entities), histBlockStart.EffectCardId, histBlockStart.EffectIndex, GetEntityLogName(histBlockStart.Target), histBlockStart.SubOption, text);
			indentation += "    ";
			break;
		}
		case Network.PowerType.BLOCK_END:
			if (indentation.Length >= "    ".Length)
			{
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			logger.Print("{0}.DebugPrintPower() - {1}BLOCK_END", callerName, indentation);
			break;
		case Network.PowerType.FULL_ENTITY:
		{
			Network.Entity entity = ((Network.HistFullEntity)power).Entity;
			Entity entity2 = GetEntity(entity.ID);
			if (entity2 == null)
			{
				logger.Print("{0}.DebugPrintPower() - {1}FULL_ENTITY - Creating ID={2} CardID={3}", callerName, indentation, entity.ID, entity.CardID);
			}
			else
			{
				logger.Print("{0}.DebugPrintPower() - {1}FULL_ENTITY - Updating {2} CardID={3}", callerName, indentation, entity2, entity.CardID);
			}
			DebugPrintTags(logger, callerName, indentation, entity);
			break;
		}
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			logger.Print("{0}.DebugPrintPower() - {1}TAG_CHANGE Entity={2} {3} {4}", callerName, indentation, GetEntityLogName(histTagChange.Entity), Tags.DebugTag(histTagChange.Tag, histTagChange.Value), histTagChange.ChangeDef ? "DEF CHANGE" : "");
			break;
		}
		case Network.PowerType.CREATE_GAME:
		{
			Network.HistCreateGame histCreateGame = (Network.HistCreateGame)power;
			logger.Print("{0}.DebugPrintPower() - {1}CREATE_GAME", callerName, indentation);
			indentation += "    ";
			logger.Print("{0}.DebugPrintPower() - {1}GameEntity EntityID={2}", callerName, indentation, histCreateGame.Game.ID);
			DebugPrintTags(logger, callerName, indentation, histCreateGame.Game);
			foreach (Network.HistCreateGame.PlayerData player in histCreateGame.Players)
			{
				logger.Print("{0}.DebugPrintPower() - {1}Player EntityID={2} PlayerID={3} GameAccountId={4}", callerName, indentation, player.Player.ID, player.ID, player.GameAccountId);
				DebugPrintTags(logger, callerName, indentation, player.Player);
			}
			indentation = indentation.Remove(indentation.Length - "    ".Length);
			break;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.Entity entity3 = ((Network.HistShowEntity)power).Entity;
			logger.Print("{0}.DebugPrintPower() - {1}SHOW_ENTITY - Updating Entity={2} CardID={3}", callerName, indentation, GetEntityLogName(entity3.ID), entity3.CardID);
			DebugPrintTags(logger, callerName, indentation, entity3);
			break;
		}
		case Network.PowerType.HIDE_ENTITY:
		{
			Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power;
			logger.Print("{0}.DebugPrintPower() - {1}HIDE_ENTITY - Entity={2} {3}", callerName, indentation, GetEntityLogName(histHideEntity.Entity), Tags.DebugTag(49, histHideEntity.Zone));
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.Entity entity4 = ((Network.HistChangeEntity)power).Entity;
			logger.Print("{0}.DebugPrintPower() - {1}CHANGE_ENTITY - Updating Entity={2} CardID={3}", callerName, indentation, GetEntityLogName(entity4.ID), entity4.CardID);
			DebugPrintTags(logger, callerName, indentation, entity4);
			break;
		}
		case Network.PowerType.META_DATA:
		{
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			string text2 = histMetaData.Data.ToString();
			HistoryMeta.Type metaType = histMetaData.MetaType;
			if (metaType == HistoryMeta.Type.JOUST)
			{
				text2 = GetEntityLogName(histMetaData.Data);
			}
			logger.Print("{0}.DebugPrintPower() - {1}META_DATA - Meta={2} Data={3} InfoCount={4}", callerName, indentation, histMetaData.MetaType, text2, histMetaData.Info.Count);
			if (histMetaData.Info.Count > 0 && logger.IsVerbose())
			{
				indentation += "    ";
				for (int j = 0; j < histMetaData.Info.Count; j++)
				{
					int id2 = histMetaData.Info[j];
					logger.Print(true, "{0}.DebugPrintPower() - {1}        Info[{2}] = {3}", callerName, indentation, j, GetEntityLogName(id2));
				}
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			break;
		}
		case Network.PowerType.RESET_GAME:
			logger.Print("{0}.DebugPrintPower() - {1}RESET_GAME", callerName, indentation);
			break;
		case Network.PowerType.SUB_SPELL_START:
		{
			Network.HistSubSpellStart histSubSpellStart = power as Network.HistSubSpellStart;
			logger.Print("{0}.DebugPrintPower() - {1}SUB_SPELL_START - SpellPrefabGUID={2} Source={3} TargetCount={4}", callerName, indentation, histSubSpellStart.SpellPrefabGUID, histSubSpellStart.SourceEntityID, histSubSpellStart.TargetEntityIDS.Count);
			if (logger.IsVerbose())
			{
				if (histSubSpellStart.SourceEntityID != 0)
				{
					logger.Print(true, "{0}.DebugPrintPower() - {1}                  Source = {2}", callerName, indentation, GetEntityLogName(histSubSpellStart.SourceEntityID));
				}
				for (int i = 0; i < histSubSpellStart.TargetEntityIDS.Count; i++)
				{
					int id = histSubSpellStart.TargetEntityIDS[i];
					logger.Print(true, "{0}.DebugPrintPower() - {1}                  Targets[{2}] = {3}", callerName, indentation, i, GetEntityLogName(id));
				}
			}
			indentation += "    ";
			break;
		}
		case Network.PowerType.SUB_SPELL_END:
			if (indentation.Length >= "    ".Length)
			{
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			logger.Print("{0}.DebugPrintPower() - {1}SUB_SPELL_END", callerName, indentation);
			break;
		case Network.PowerType.VO_SPELL:
		{
			Network.HistVoSpell histVoSpell = power as Network.HistVoSpell;
			logger.Print("{0}.DebugPrintPower() - {1}VO_SPELL - BrassRingGuid={2} - VoSpellPrefabGUID={3} - Blocking={4} - AdditionalDelayInMs={5}", callerName, indentation, histVoSpell.SpellPrefabGUID, histVoSpell.BrassRingGUID, histVoSpell.Blocking, histVoSpell.AdditionalDelayMs);
			break;
		}
		case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
		{
			Network.HistCachedTagForDormantChange histCachedTagForDormantChange = (Network.HistCachedTagForDormantChange)power;
			logger.Print("{0}.DebugPrintPower() - {1}CACHED_TAG_FOR_DORMANT_CHANGE Entity={2} {3}", callerName, indentation, GetEntityLogName(histCachedTagForDormantChange.Entity), Tags.DebugTag(histCachedTagForDormantChange.Tag, histCachedTagForDormantChange.Value));
			break;
		}
		case Network.PowerType.SHUFFLE_DECK:
		{
			Network.HistShuffleDeck histShuffleDeck = (Network.HistShuffleDeck)power;
			logger.Print("{0}.DebugPrintPower() - {1}SHUFFLE_DECK PlayerID={2}", callerName, indentation, histShuffleDeck.PlayerID);
			break;
		}
		default:
			logger.Print("{0}.DebugPrintPower() - ERROR: unhandled PowType {1}", callerName, power.Type);
			break;
		}
	}

	private void DebugPrintTags(Logger logger, string callerName, string indentation, Network.Entity netEntity)
	{
		if (Log.Power.CanPrint())
		{
			if (indentation != null)
			{
				indentation += "    ";
			}
			for (int i = 0; i < netEntity.Tags.Count; i++)
			{
				Network.Entity.Tag tag = netEntity.Tags[i];
				logger.Print("{0}.DebugPrintPower() - {1}{2}", callerName, indentation, Tags.DebugTag(tag.Name, tag.Value));
			}
		}
	}

	private void DebugPrintOptions(Logger logger)
	{
		if (!logger.CanPrint())
		{
			return;
		}
		logger.Print("GameState.DebugPrintOptions() - id={0}", m_options.ID);
		for (int i = 0; i < m_options.List.Count; i++)
		{
			Network.Options.Option option = m_options.List[i];
			Entity entity = GetEntity(option.Main.ID);
			logger.Print("GameState.DebugPrintOptions() -   option {0} type={1} mainEntity={2} error={3} errorParam={4}", i, option.Type, entity, option.Main.PlayErrorInfo.PlayError, option.Main.PlayErrorInfo.PlayErrorParam);
			if (option.Main.Targets != null)
			{
				for (int j = 0; j < option.Main.Targets.Count; j++)
				{
					Network.Options.Option.TargetOption targetOption = option.Main.Targets[j];
					Entity entity2 = GetEntity(targetOption.ID);
					logger.Print("GameState.DebugPrintOptions() -     target {0} entity={1} error={2} errorParam={3}", j, entity2, targetOption.PlayErrorInfo.PlayError, targetOption.PlayErrorInfo.PlayErrorParam);
				}
			}
			for (int k = 0; k < option.Subs.Count; k++)
			{
				Network.Options.Option.SubOption subOption = option.Subs[k];
				Entity entity3 = GetEntity(subOption.ID);
				logger.Print("GameState.DebugPrintOptions() -     subOption {0} entity={1} error={2} errorParam={3}", k, entity3, subOption.PlayErrorInfo.PlayError, subOption.PlayErrorInfo.PlayErrorParam);
				if (subOption.Targets != null)
				{
					for (int l = 0; l < subOption.Targets.Count; l++)
					{
						Network.Options.Option.TargetOption targetOption2 = subOption.Targets[l];
						Entity entity4 = GetEntity(targetOption2.ID);
						logger.Print("GameState.DebugPrintOptions() -       target {0} entity={1} error={2} errorParam={3}", l, entity4, targetOption2.PlayErrorInfo.PlayError, targetOption2.PlayErrorInfo.PlayErrorParam);
					}
				}
			}
		}
	}

	private void DebugPrintEntityChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		if (Log.Power.CanPrint())
		{
			Player player = GetPlayer(choices.PlayerId);
			object obj = null;
			if (preChoiceTaskList != null)
			{
				obj = preChoiceTaskList.GetId();
			}
			Log.Power.Print("GameState.DebugPrintEntityChoices() - id={0} Player={1} TaskList={2} ChoiceType={3} CountMin={4} CountMax={5}", choices.ID, GetEntityLogName(player.GetEntityId()), obj, choices.ChoiceType, choices.CountMin, choices.CountMax);
			Log.Power.Print("GameState.DebugPrintEntityChoices() -   Source={0}", GetEntityLogName(choices.Source));
			for (int i = 0; i < choices.Entities.Count; i++)
			{
				Log.Power.Print("GameState.DebugPrintEntityChoices() -   Entities[{0}]={1}", i, GetEntityLogName(choices.Entities[i]));
			}
		}
	}

	private void DebugPrintEntitiesChosen(Network.EntitiesChosen chosen)
	{
		if (Log.Power.CanPrint())
		{
			Player player = GetPlayer(chosen.PlayerId);
			Log.Power.Print("GameState.DebugPrintEntitiesChosen() - id={0} Player={1} EntitiesCount={2}", chosen.ID, GetEntityLogName(player.GetEntityId()), chosen.Entities.Count);
			for (int i = 0; i < chosen.Entities.Count; i++)
			{
				Log.Power.Print("GameState.DebugPrintEntitiesChosen() -   Entities[{0}]={1}", i, GetEntityLogName(chosen.Entities[i]));
			}
		}
	}

	private string GetEntityLogName(int id)
	{
		Entity entity = GetEntity(id);
		if (entity == null)
		{
			return id.ToString();
		}
		if (entity.IsPlayer())
		{
			BnetPlayer bnetPlayer = (entity as Player).GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetBattleTag() != null)
			{
				return $"{bnetPlayer.GetBattleTag().GetName()}#{bnetPlayer.GetBattleTag().GetNumber()}";
			}
		}
		return entity.ToString();
	}

	private string GetEntitiesLogNames(List<int> ids)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (int id in ids)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append(GetEntityLogName(id));
		}
		return stringBuilder.ToString();
	}

	private void PrintBlockingTaskList(StringBuilder builder, PowerTaskList taskList)
	{
		if (taskList == null)
		{
			builder.Append("null");
			return;
		}
		builder.AppendFormat("ID={0} ", taskList.GetId());
		builder.Append("Source=[");
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		if (blockStart == null)
		{
			builder.Append("null");
		}
		else
		{
			builder.AppendFormat("BlockType={0}", blockStart.BlockType);
			builder.Append(' ');
			builder.AppendFormat("Entities={0}", GetEntitiesLogNames(blockStart.Entities));
			builder.Append(' ');
			builder.AppendFormat("Target={0}", GetEntityLogName(blockStart.Target));
		}
		builder.Append(']');
		builder.AppendFormat(" Tasks={0}", taskList.GetTaskList().Count);
	}

	private void QuickGameFlipHeroesCheat(List<Network.PowerHistory> powerList)
	{
	}
}
