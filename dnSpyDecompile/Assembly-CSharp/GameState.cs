using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Assets;
using PegasusGame;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class GameState
{
	// Token: 0x06002ADB RID: 10971 RVA: 0x000D7D6F File Offset: 0x000D5F6F
	public static GameState Get()
	{
		return GameState.s_instance;
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x000D7D76 File Offset: 0x000D5F76
	public static GameState Initialize()
	{
		if (GameState.s_instance == null)
		{
			GameState.s_instance = new GameState();
			GameState.FireGameStateInitializedEvent();
			GameState.s_instance.m_powerProcessor.AddTaskEventListener(new PowerProcessor.OnTaskEvent(GameState.s_instance.HandleTaskTimeEvent));
		}
		return GameState.s_instance;
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x000D7DB4 File Offset: 0x000D5FB4
	public static void Shutdown()
	{
		if (GameState.s_instance == null)
		{
			return;
		}
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().DestroyAll(Global.SoundCategory.FX);
		}
		GameState.s_instance.ClearEntityMap();
		GameState.s_instance.HideWaitForOpponentReconnectPopup();
		GameState.s_instance.m_powerProcessor.RemoveTaskEventListener(new PowerProcessor.OnTaskEvent(GameState.s_instance.HandleTaskTimeEvent));
		GameState.s_instance = null;
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x000D7E14 File Offset: 0x000D6014
	public void Update()
	{
		this.m_lostFrameTimeTracker.Update();
		this.m_lostSlushTimeTracker.Update();
		if (this.CheckReconnectIfStuck())
		{
			return;
		}
		this.m_powerProcessor.ProcessPowerQueue();
		this.m_lostFrameTimeTracker.AdjustAccruedLostTime(-0.016667f);
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x000D7E50 File Offset: 0x000D6050
	public PowerProcessor GetPowerProcessor()
	{
		return this.m_powerProcessor;
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x000D7E58 File Offset: 0x000D6058
	public IGameStateTimeTracker GetTimeTracker()
	{
		if (this.m_useSlushTimeCatchUp)
		{
			return this.GetSlushTimeTracker();
		}
		return this.GetFrameTimeTracker();
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x000D7E6F File Offset: 0x000D606F
	public GameStateSlushTimeTracker GetSlushTimeTracker()
	{
		return this.m_lostSlushTimeTracker;
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x000D7E77 File Offset: 0x000D6077
	public GameStateFrameTimeTracker GetFrameTimeTracker()
	{
		return this.m_lostFrameTimeTracker;
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x000D7E7F File Offset: 0x000D607F
	public void HandleTaskTimeEvent(float diff)
	{
		this.m_lostSlushTimeTracker.AdjustAccruedLostTime(diff);
	}

	// Token: 0x06002AE4 RID: 10980 RVA: 0x000D7E8D File Offset: 0x000D608D
	private static GameStateSlushTimeTracker CreateSlushTimeTracker()
	{
		return new GameStateSlushTimeTracker();
	}

	// Token: 0x06002AE5 RID: 10981 RVA: 0x000D7E94 File Offset: 0x000D6094
	private static GameStateFrameTimeTracker CreateFrameTimeTracker()
	{
		return new GameStateFrameTimeTracker(15, 0.033333f);
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x000D7EA2 File Offset: 0x000D60A2
	public bool AreLostTimeGuardianConditionsMet()
	{
		return this.m_clientLostTimeCatchUpThreshold > 0f && (!this.m_restrictClientLostTimeCatchUpToLowEndDevices || PlatformSettings.Memory != MemoryCategory.High);
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x000D7EC8 File Offset: 0x000D60C8
	public bool AllowDeferredPowers()
	{
		return this.m_allowDeferredPowers;
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x000D7ED0 File Offset: 0x000D60D0
	public bool AllowBatchedPowers()
	{
		return this.m_allowBatchedPowers;
	}

	// Token: 0x06002AE9 RID: 10985 RVA: 0x000D7ED8 File Offset: 0x000D60D8
	public bool AllowDiamondCards()
	{
		return this.m_allowDiamondCards;
	}

	// Token: 0x06002AEA RID: 10986 RVA: 0x000D7EE0 File Offset: 0x000D60E0
	public bool PrintBattlegroundMinionPoolOnUpdate()
	{
		return this.m_printBattlegroundMinionPoolOnUpdate;
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x000D7EE8 File Offset: 0x000D60E8
	public bool PrintBattlegroundDenyListOnUpdate()
	{
		return this.m_printBattlegroundDenyListOnUpdate;
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x000D7EF0 File Offset: 0x000D60F0
	public void SetPrintBattlegroundMinionPoolOnUpdate(bool isPrinting)
	{
		this.m_printBattlegroundMinionPoolOnUpdate = isPrinting;
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x000D7EF9 File Offset: 0x000D60F9
	public void SetPrintBattlegroundDenyListOnUpdate(bool isPrinting)
	{
		this.m_printBattlegroundDenyListOnUpdate = isPrinting;
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x000D7F02 File Offset: 0x000D6102
	public string BattlegroundDenyList()
	{
		return this.m_battlegroundDenyList;
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x000D7F0A File Offset: 0x000D610A
	public string BattlegroundMinionPool()
	{
		return this.m_battlegroundMinionPool;
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x000D7F12 File Offset: 0x000D6112
	public bool HasPowersToProcess()
	{
		return this.m_powerProcessor.GetCurrentTaskList() != null || this.m_powerProcessor.GetPowerQueue().Count > 0;
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x000D7F3C File Offset: 0x000D613C
	public global::Entity GetEntity(int id)
	{
		global::Entity result;
		this.m_entityMap.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06002AF2 RID: 10994 RVA: 0x000D7F5C File Offset: 0x000D615C
	public global::Player GetPlayer(int id)
	{
		global::Player result;
		this.m_playerMap.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06002AF3 RID: 10995 RVA: 0x000D7F79 File Offset: 0x000D6179
	public GameEntity GetGameEntity()
	{
		return this.m_gameEntity;
	}

	// Token: 0x06002AF4 RID: 10996 RVA: 0x000D7F84 File Offset: 0x000D6184
	public bool GetBooleanGameOption(GameEntityOption option)
	{
		GameEntity gameEntity = this.m_gameEntity;
		GameEntityOptions gameEntityOptions = (gameEntity != null) ? gameEntity.GetGameOptions() : null;
		return gameEntityOptions != null && gameEntityOptions.GetBooleanOption(option);
	}

	// Token: 0x06002AF5 RID: 10997 RVA: 0x000D7FB0 File Offset: 0x000D61B0
	public string GetStringGameOption(GameEntityOption option)
	{
		GameEntity gameEntity = this.m_gameEntity;
		if (gameEntity == null)
		{
			return null;
		}
		GameEntityOptions gameOptions = gameEntity.GetGameOptions();
		if (gameOptions == null)
		{
			return null;
		}
		return gameOptions.GetStringOption(option);
	}

	// Token: 0x06002AF6 RID: 10998 RVA: 0x000D7FCF File Offset: 0x000D61CF
	[Conditional("UNITY_EDITOR")]
	public void DebugSetGameEntity(GameEntity gameEntity)
	{
		this.m_gameEntity = gameEntity;
	}

	// Token: 0x06002AF7 RID: 10999 RVA: 0x000D7FD8 File Offset: 0x000D61D8
	public bool WasGameCreated()
	{
		return this.m_gameEntity != null;
	}

	// Token: 0x06002AF8 RID: 11000 RVA: 0x000D7FE4 File Offset: 0x000D61E4
	public global::Player GetPlayerBySide(global::Player.Side playerSide)
	{
		foreach (global::Player player in this.m_playerMap.Values)
		{
			if (player.GetSide() == playerSide)
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002AF9 RID: 11001 RVA: 0x000D8048 File Offset: 0x000D6248
	public global::Player GetLocalSidePlayer()
	{
		bool isSpectatingOrWatching = SpectatorManager.Get().IsSpectatingOrWatching;
		foreach (global::Player player in this.m_playerMap.Values)
		{
			if (player.IsLocalUser())
			{
				return player;
			}
			if (isSpectatingOrWatching && player.GetGameAccountId() == SpectatorManager.Get().GetSpectateeFriendlySide())
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002AFA RID: 11002 RVA: 0x000D80D4 File Offset: 0x000D62D4
	public int GetFriendlySideTeamId()
	{
		global::Player localSidePlayer = this.GetLocalSidePlayer();
		if (localSidePlayer == null)
		{
			return 0;
		}
		int teamId = localSidePlayer.GetTeamId();
		if (teamId <= 0)
		{
			return localSidePlayer.GetPlayerId();
		}
		return teamId;
	}

	// Token: 0x06002AFB RID: 11003 RVA: 0x000D8100 File Offset: 0x000D6300
	public global::Player GetFriendlySidePlayer()
	{
		foreach (global::Player player in this.m_playerMap.Values)
		{
			if (player.IsFriendlySide() && player.IsTeamLeader())
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002AFC RID: 11004 RVA: 0x000D8168 File Offset: 0x000D6368
	public void HideZzzEffects()
	{
		global::Player friendlySidePlayer = this.GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			ZonePlay battlefieldZone = friendlySidePlayer.GetBattlefieldZone();
			if (battlefieldZone != null)
			{
				battlefieldZone.HideCardZzzEffects();
			}
		}
		global::Player opposingSidePlayer = this.GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			ZonePlay battlefieldZone2 = opposingSidePlayer.GetBattlefieldZone();
			if (battlefieldZone2 != null)
			{
				battlefieldZone2.HideCardZzzEffects();
			}
		}
	}

	// Token: 0x06002AFD RID: 11005 RVA: 0x000D81B8 File Offset: 0x000D63B8
	public void UnhideZzzEffects()
	{
		global::Player friendlySidePlayer = this.GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			ZonePlay battlefieldZone = friendlySidePlayer.GetBattlefieldZone();
			if (battlefieldZone != null)
			{
				battlefieldZone.UnhideCardZzzEffects();
			}
		}
		global::Player opposingSidePlayer = this.GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			ZonePlay battlefieldZone2 = opposingSidePlayer.GetBattlefieldZone();
			if (battlefieldZone2 != null)
			{
				battlefieldZone2.UnhideCardZzzEffects();
			}
		}
	}

	// Token: 0x06002AFE RID: 11006 RVA: 0x000D8208 File Offset: 0x000D6408
	public global::Player GetOpposingSidePlayer()
	{
		foreach (global::Player player in this.m_playerMap.Values)
		{
			if (player.IsOpposingSide() && player.IsTeamLeader())
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002AFF RID: 11007 RVA: 0x000D8270 File Offset: 0x000D6470
	public int GetFriendlyPlayerId()
	{
		global::Player friendlySidePlayer = this.GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return 0;
		}
		return friendlySidePlayer.GetPlayerId();
	}

	// Token: 0x06002B00 RID: 11008 RVA: 0x000D8290 File Offset: 0x000D6490
	public int GetOpposingPlayerId()
	{
		global::Player opposingSidePlayer = this.GetOpposingSidePlayer();
		if (opposingSidePlayer == null)
		{
			return 0;
		}
		return opposingSidePlayer.GetPlayerId();
	}

	// Token: 0x06002B01 RID: 11009 RVA: 0x000D82B0 File Offset: 0x000D64B0
	public bool IsFriendlySidePlayerTurn()
	{
		global::Player friendlySidePlayer = this.GetFriendlySidePlayer();
		return friendlySidePlayer != null && friendlySidePlayer.IsCurrentPlayer();
	}

	// Token: 0x06002B02 RID: 11010 RVA: 0x000D82D0 File Offset: 0x000D64D0
	public bool IsLocalSidePlayerTurn()
	{
		global::Player localSidePlayer = this.GetLocalSidePlayer();
		return localSidePlayer != null && (!localSidePlayer.IsTeamLeader() || localSidePlayer.IsCurrentPlayer());
	}

	// Token: 0x06002B03 RID: 11011 RVA: 0x000D82FC File Offset: 0x000D64FC
	public global::Player GetCurrentPlayer()
	{
		foreach (global::Player player in this.m_playerMap.Values)
		{
			if (player.IsCurrentPlayer())
			{
				return player;
			}
		}
		return null;
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x000D835C File Offset: 0x000D655C
	public bool IsCurrentPlayerRevealed()
	{
		global::Player currentPlayer = this.GetCurrentPlayer();
		return currentPlayer != null && currentPlayer.IsRevealed();
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x000D837C File Offset: 0x000D657C
	public global::Player GetFirstOpponentPlayer(global::Player player)
	{
		foreach (global::Player player2 in this.m_playerMap.Values)
		{
			if (player2.GetSide() != player.GetSide())
			{
				return player2;
			}
		}
		return null;
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x000D83E4 File Offset: 0x000D65E4
	public int GetNumFriendlyMinionsInPlay(bool includeUntouchables)
	{
		return this.GetNumMinionsInPlay(this.GetFriendlySidePlayer(), includeUntouchables);
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x000D83F3 File Offset: 0x000D65F3
	public int GetNumEnemyMinionsInPlay(bool includeUntouchables)
	{
		return this.GetNumMinionsInPlay(this.GetOpposingSidePlayer(), includeUntouchables);
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x000D8404 File Offset: 0x000D6604
	private int GetNumMinionsInPlay(global::Player player, bool includeUntouchables)
	{
		if (player == null)
		{
			return 0;
		}
		int num = 0;
		foreach (global::Card card in player.GetBattlefieldZone().GetCards())
		{
			global::Entity entity = card.GetEntity();
			if (entity.GetController() == player && entity.IsMinion() && (includeUntouchables || !entity.HasTag(GAME_TAG.UNTOUCHABLE)))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06002B09 RID: 11017 RVA: 0x000D8488 File Offset: 0x000D6688
	public int GetTurn()
	{
		if (this.m_gameEntity != null)
		{
			return this.m_gameEntity.GetTag(GAME_TAG.TURN);
		}
		return 0;
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x000D84A1 File Offset: 0x000D66A1
	public bool IsTagBlockingInput()
	{
		return this.m_gameEntity != null && this.m_gameEntity.HasTag(GAME_TAG.BLOCK_ALL_INPUT);
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x000D84C0 File Offset: 0x000D66C0
	public bool IsResponsePacketBlocked()
	{
		if (this.IsMulliganManagerIntroActive())
		{
			return true;
		}
		if (this.m_gameEntity.IsMulliganActiveRealTime())
		{
			return false;
		}
		if (this.IsMulliganManagerActive())
		{
			return true;
		}
		if (!this.IsCurrentPlayerRevealed() && !this.IsLocalSidePlayerTurn())
		{
			return true;
		}
		if (!this.m_gameEntity.IsCurrentTurnRealTime())
		{
			return true;
		}
		if (this.IsTurnStartManagerBlockingInput())
		{
			return true;
		}
		if (this.IsTagBlockingInput())
		{
			return true;
		}
		if (this.IsResetGamePending())
		{
			return false;
		}
		switch (this.m_responseMode)
		{
		case GameState.ResponseMode.NONE:
			return true;
		case GameState.ResponseMode.OPTION:
		case GameState.ResponseMode.SUB_OPTION:
		case GameState.ResponseMode.OPTION_TARGET:
			if (this.m_options == null)
			{
				return true;
			}
			break;
		case GameState.ResponseMode.CHOICE:
			if (this.GetFriendlyEntityChoices() == null)
			{
				return true;
			}
			break;
		default:
			UnityEngine.Debug.LogWarning(string.Format("GameState.IsResponsePacketBlocked() - unhandled response mode {0}", this.m_responseMode));
			break;
		}
		return false;
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x000D8583 File Offset: 0x000D6783
	public TAG_RACE[] GetAvailableRacesInBattlegroundsExcludingAmalgam()
	{
		return this.m_availableRacesInBattlegroundsExcludingAmalgam;
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x000D858C File Offset: 0x000D678C
	public TAG_RACE[] GetMissingRacesInBattlegrounds()
	{
		if (this.m_missingRacesInBattlegrounds[0] == TAG_RACE.INVALID && this.m_availableRacesInBattlegroundsExcludingAmalgam[0] != TAG_RACE.INVALID)
		{
			int num = 0;
			for (int i = 0; i < 8; i++)
			{
				TAG_RACE tag_RACE = this.m_allRaces[i];
				if (!this.m_availableRacesInBattlegroundsExcludingAmalgam.Contains(tag_RACE))
				{
					this.m_missingRacesInBattlegrounds[num] = tag_RACE;
					num++;
				}
			}
		}
		return this.m_missingRacesInBattlegrounds;
	}

	// Token: 0x06002B0E RID: 11022 RVA: 0x000D85E5 File Offset: 0x000D67E5
	public Map<int, global::Entity> GetEntityMap()
	{
		return this.m_entityMap;
	}

	// Token: 0x06002B0F RID: 11023 RVA: 0x000D85ED File Offset: 0x000D67ED
	public Map<int, global::Player> GetPlayerMap()
	{
		return this.m_playerMap;
	}

	// Token: 0x06002B10 RID: 11024 RVA: 0x000D85F5 File Offset: 0x000D67F5
	public Map<int, global::SharedPlayerInfo> GetPlayerInfoMap()
	{
		return this.m_playerInfoMap;
	}

	// Token: 0x06002B11 RID: 11025 RVA: 0x000D85FD File Offset: 0x000D67FD
	public void AddPlayerInfo(global::SharedPlayerInfo playerInfo)
	{
		this.m_playerInfoMap.Add(playerInfo.GetPlayerId(), playerInfo);
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x000D8611 File Offset: 0x000D6811
	public void AddPlayer(global::Player player)
	{
		this.m_playerMap.Add(player.GetPlayerId(), player);
		this.m_entityMap.Add(player.GetEntityId(), player);
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x000D8637 File Offset: 0x000D6837
	public void RemovePlayer(global::Player player)
	{
		player.Destroy();
		this.m_playerMap.Remove(player.GetPlayerId());
		this.m_entityMap.Remove(player.GetEntityId());
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x000D8663 File Offset: 0x000D6863
	public void AddEntity(global::Entity entity)
	{
		this.m_entityMap.Add(entity.GetEntityId(), entity);
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x000D8678 File Offset: 0x000D6878
	public void RemoveEntity(global::Entity entity)
	{
		if (entity.IsPlayer())
		{
			this.RemovePlayer(entity as global::Player);
			return;
		}
		if (entity.IsGame())
		{
			this.m_gameEntity = null;
			return;
		}
		if (entity.IsAttached())
		{
			global::Entity entity2 = this.GetEntity(entity.GetAttached());
			if (entity2 != null)
			{
				entity2.RemoveAttachment(entity);
			}
		}
		if (entity.IsHero())
		{
			global::Player player = this.GetPlayer(entity.GetControllerId());
			if (player != null && player.GetHero() == entity)
			{
				player.SetHero(null);
			}
		}
		else if (entity.IsHeroPower())
		{
			global::Player player2 = this.GetPlayer(entity.GetControllerId());
			if (player2 != null && player2.GetHeroPower() == entity)
			{
				player2.SetHeroPower(null);
			}
		}
		entity.Destroy();
		this.m_entityMap.Remove(entity.GetEntityId());
	}

	// Token: 0x06002B16 RID: 11030 RVA: 0x000D8734 File Offset: 0x000D6934
	public void RemoveQueuedEntitiesFromGame()
	{
		if (this.m_removedFromGameEntities.Count == 0)
		{
			return;
		}
		bool flag;
		do
		{
			global::Entity entity = this.m_removedFromGameEntities.Peek();
			flag = this.AttemptRemovalOfQueuedEntity(entity);
			if (flag)
			{
				this.m_removedFromGameEntities.Dequeue();
				this.m_removedFromGameEntityLog.Add(entity.GetEntityId());
			}
		}
		while (flag && this.m_removedFromGameEntities.Count > 0);
	}

	// Token: 0x06002B17 RID: 11031 RVA: 0x000D8797 File Offset: 0x000D6997
	public bool EntityRemovedFromGame(int entityId)
	{
		return this.m_removedFromGameEntityLog.Contains(entityId);
	}

	// Token: 0x06002B18 RID: 11032 RVA: 0x000D87A5 File Offset: 0x000D69A5
	private bool AttemptRemovalOfQueuedEntity(global::Entity entity)
	{
		if (this.GetPowerProcessor().EntityHasPendingTasks(entity))
		{
			return false;
		}
		GameState.Get().RemoveEntity(entity);
		return true;
	}

	// Token: 0x06002B19 RID: 11033 RVA: 0x000D87C3 File Offset: 0x000D69C3
	public int GetMaxSecretZoneSizePerPlayer()
	{
		return this.m_maxSecretZoneSizePerPlayer;
	}

	// Token: 0x06002B1A RID: 11034 RVA: 0x000D87CB File Offset: 0x000D69CB
	public int GetMaxSecretsPerPlayer()
	{
		return this.m_maxSecretsPerPlayer;
	}

	// Token: 0x06002B1B RID: 11035 RVA: 0x000D87D3 File Offset: 0x000D69D3
	public int GetMaxQuestsPerPlayer()
	{
		return this.m_maxQuestsPerPlayer;
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x000D87DB File Offset: 0x000D69DB
	public int GetMaxFriendlyMinionsPerPlayer()
	{
		return this.m_maxFriendlyMinionsPerPlayer;
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x000D87E3 File Offset: 0x000D69E3
	public bool IsBusy()
	{
		return this.m_busy;
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x000D87EB File Offset: 0x000D69EB
	public void SetBusy(bool busy)
	{
		this.m_busy = busy;
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x000D87F4 File Offset: 0x000D69F4
	public bool IsMulliganBusy()
	{
		return this.m_mulliganBusy;
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x000D87FC File Offset: 0x000D69FC
	public void SetMulliganBusy(bool busy)
	{
		this.m_mulliganBusy = busy;
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x000D8805 File Offset: 0x000D6A05
	public bool IsMulliganManagerActive()
	{
		return !(MulliganManager.Get() == null) && MulliganManager.Get().IsMulliganActive();
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x000D8820 File Offset: 0x000D6A20
	public bool IsMulliganManagerIntroActive()
	{
		return !(MulliganManager.Get() == null) && MulliganManager.Get().IsMulliganIntroActive();
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x000D883B File Offset: 0x000D6A3B
	public bool IsTurnStartManagerActive()
	{
		return !(TurnStartManager.Get() == null) && TurnStartManager.Get().IsListeningForTurnEvents();
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x000D8856 File Offset: 0x000D6A56
	public bool IsTurnStartManagerBlockingInput()
	{
		return !(TurnStartManager.Get() == null) && TurnStartManager.Get().IsBlockingInput();
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000D8871 File Offset: 0x000D6A71
	public bool HasTheCoinBeenSpawned()
	{
		return this.m_coinHasSpawned;
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000D8879 File Offset: 0x000D6A79
	public void NotifyOfCoinSpawn()
	{
		this.m_coinHasSpawned = true;
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x000D8882 File Offset: 0x000D6A82
	public bool IsBeginPhase()
	{
		return this.m_gameEntity != null && GameUtils.IsBeginPhase(this.m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x000D88A0 File Offset: 0x000D6AA0
	public bool IsPastBeginPhase()
	{
		return this.m_gameEntity != null && GameUtils.IsPastBeginPhase(this.m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x000D88BE File Offset: 0x000D6ABE
	public bool IsMainPhase()
	{
		return this.m_gameEntity != null && GameUtils.IsMainPhase(this.m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP));
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x000D88DC File Offset: 0x000D6ADC
	public bool IsMulliganPhase()
	{
		return this.m_gameEntity != null && this.m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.STEP) == TAG_STEP.BEGIN_MULLIGAN;
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x000D88F8 File Offset: 0x000D6AF8
	public bool IsMulliganPhasePending()
	{
		if (this.m_gameEntity == null)
		{
			return false;
		}
		if (this.m_gameEntity.GetTag<TAG_STEP>(GAME_TAG.NEXT_STEP) == TAG_STEP.BEGIN_MULLIGAN)
		{
			return true;
		}
		bool foundMulliganStep = false;
		int gameEntityId = this.m_gameEntity.GetEntityId();
		this.m_powerProcessor.ForEachTaskList(delegate(int queueIndex, PowerTaskList taskList)
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
						return;
					}
				}
			}
		});
		return foundMulliganStep;
	}

	// Token: 0x06002B2C RID: 11052 RVA: 0x000D895F File Offset: 0x000D6B5F
	public bool IsMulliganPhaseNowOrPending()
	{
		return this.IsMulliganPhase() || this.IsMulliganPhasePending();
	}

	// Token: 0x06002B2D RID: 11053 RVA: 0x000D8976 File Offset: 0x000D6B76
	public bool IsResetGamePending()
	{
		return this.m_realTimeResetGame != null;
	}

	// Token: 0x06002B2E RID: 11054 RVA: 0x000D8981 File Offset: 0x000D6B81
	public GameState.CreateGamePhase GetCreateGamePhase()
	{
		return this.m_createGamePhase;
	}

	// Token: 0x06002B2F RID: 11055 RVA: 0x000D8989 File Offset: 0x000D6B89
	public bool IsGameCreating()
	{
		return this.m_createGamePhase == GameState.CreateGamePhase.CREATING;
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x000D8994 File Offset: 0x000D6B94
	public bool IsGameCreated()
	{
		return this.m_createGamePhase == GameState.CreateGamePhase.CREATED;
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x000D899F File Offset: 0x000D6B9F
	public bool IsGameCreatedOrCreating()
	{
		return this.IsGameCreated() || this.IsGameCreating();
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x000D89B1 File Offset: 0x000D6BB1
	public bool WasConcedeRequested()
	{
		return this.m_concedeRequested;
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x000D89B9 File Offset: 0x000D6BB9
	public void Concede()
	{
		if (this.m_concedeRequested)
		{
			return;
		}
		this.m_concedeRequested = true;
		Network.Get().Concede();
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x000D89D5 File Offset: 0x000D6BD5
	public bool WasRestartRequested()
	{
		return this.m_restartRequested;
	}

	// Token: 0x06002B35 RID: 11061 RVA: 0x000D89DD File Offset: 0x000D6BDD
	public void Restart()
	{
		if (this.m_restartRequested)
		{
			return;
		}
		this.m_restartRequested = true;
		if (this.IsGameOverNowOrPending())
		{
			this.CheckRestartOnRealTimeGameOver();
			return;
		}
		this.Concede();
	}

	// Token: 0x06002B36 RID: 11062 RVA: 0x000D8A04 File Offset: 0x000D6C04
	private void CheckRestartOnRealTimeGameOver()
	{
		if (!this.WasRestartRequested())
		{
			return;
		}
		this.m_gameOver = true;
		this.m_realTimeGameOverTagChange = null;
		Network.Get().DisconnectFromGameServer();
		NotificationManager.Get().DestroyAllNotificationsNowWithNoAnim();
		ReconnectMgr.Get().SetBypassReconnect(true);
		GameMgr.Get().RestartGame();
	}

	// Token: 0x06002B37 RID: 11063 RVA: 0x000D8A51 File Offset: 0x000D6C51
	public bool IsGameOver()
	{
		return this.m_gameOver;
	}

	// Token: 0x06002B38 RID: 11064 RVA: 0x000D8A59 File Offset: 0x000D6C59
	public bool IsGameOverPending()
	{
		return this.m_realTimeGameOverTagChange != null;
	}

	// Token: 0x06002B39 RID: 11065 RVA: 0x000D8A64 File Offset: 0x000D6C64
	public bool IsGameOverNowOrPending()
	{
		return this.IsGameOver() || this.IsGameOverPending();
	}

	// Token: 0x06002B3A RID: 11066 RVA: 0x000D8A7B File Offset: 0x000D6C7B
	public Network.HistTagChange GetRealTimeGameOverTagChange()
	{
		return this.m_realTimeGameOverTagChange;
	}

	// Token: 0x06002B3B RID: 11067 RVA: 0x000D8A84 File Offset: 0x000D6C84
	public void ShowEnemyTauntCharacters()
	{
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag == TAG_ZONE.PLAY && zone.m_Side == global::Player.Side.OPPOSING)
			{
				List<global::Card> cards = zone.GetCards();
				for (int j = 0; j < cards.Count; j++)
				{
					global::Card card = cards[j];
					global::Entity entity = card.GetEntity();
					if (entity.HasTaunt() && !entity.IsStealthed())
					{
						card.DoTauntNotification();
					}
				}
			}
		}
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x000D8B10 File Offset: 0x000D6D10
	public void GetTauntCounts(global::Player player, out int minionCount, out int heroCount)
	{
		minionCount = 0;
		heroCount = 0;
		List<Zone> zones = ZoneMgr.Get().GetZones();
		for (int i = 0; i < zones.Count; i++)
		{
			Zone zone = zones[i];
			if (zone.m_ServerTag == TAG_ZONE.PLAY && player == zone.GetController())
			{
				List<global::Card> cards = zone.GetCards();
				for (int j = 0; j < cards.Count; j++)
				{
					global::Entity entity = cards[j].GetEntity();
					if (entity.HasTaunt() && !entity.IsStealthed())
					{
						TAG_CARDTYPE cardType = entity.GetCardType();
						if (cardType != TAG_CARDTYPE.HERO)
						{
							if (cardType == TAG_CARDTYPE.MINION)
							{
								minionCount++;
							}
						}
						else
						{
							heroCount++;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002B3D RID: 11069 RVA: 0x000D8BBA File Offset: 0x000D6DBA
	public global::Card GetFriendlyCardBeingDrawn()
	{
		return this.m_friendlyCardBeingDrawn;
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x000D8BC2 File Offset: 0x000D6DC2
	public void SetFriendlyCardBeingDrawn(global::Card card)
	{
		this.m_friendlyCardBeingDrawn = card;
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x000D8BCB File Offset: 0x000D6DCB
	public global::Card GetOpponentCardBeingDrawn()
	{
		return this.m_opponentCardBeingDrawn;
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x000D8BD3 File Offset: 0x000D6DD3
	public void SetOpponentCardBeingDrawn(global::Card card)
	{
		this.m_opponentCardBeingDrawn = card;
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x000D8BDC File Offset: 0x000D6DDC
	public bool IsBeingDrawn(global::Card card)
	{
		return card == this.m_friendlyCardBeingDrawn || card == this.m_opponentCardBeingDrawn;
	}

	// Token: 0x06002B42 RID: 11074 RVA: 0x000D8BFF File Offset: 0x000D6DFF
	public bool ClearCardBeingDrawn(global::Card card)
	{
		if (card == this.m_friendlyCardBeingDrawn)
		{
			this.m_friendlyCardBeingDrawn = null;
			return true;
		}
		if (card == this.m_opponentCardBeingDrawn)
		{
			this.m_opponentCardBeingDrawn = null;
			return true;
		}
		return false;
	}

	// Token: 0x06002B43 RID: 11075 RVA: 0x000D8C30 File Offset: 0x000D6E30
	public int GetLastTurnRemindedOfFullHand()
	{
		return this.m_lastTurnRemindedOfFullHand;
	}

	// Token: 0x06002B44 RID: 11076 RVA: 0x000D8C38 File Offset: 0x000D6E38
	public void SetLastTurnRemindedOfFullHand(int turn)
	{
		this.m_lastTurnRemindedOfFullHand = turn;
	}

	// Token: 0x06002B45 RID: 11077 RVA: 0x000D8C44 File Offset: 0x000D6E44
	public bool IsUsingFastActorTriggers()
	{
		GameEntity gameEntity = this.GetGameEntity();
		return (gameEntity != null && gameEntity.HasTag(GAME_TAG.ALWAYS_USE_FAST_ACTOR_TRIGGERS)) || this.m_usingFastActorTriggers;
	}

	// Token: 0x06002B46 RID: 11078 RVA: 0x000D8C70 File Offset: 0x000D6E70
	public void SetUsingFastActorTriggers(bool enable)
	{
		this.m_usingFastActorTriggers = enable;
	}

	// Token: 0x06002B47 RID: 11079 RVA: 0x000D8C7C File Offset: 0x000D6E7C
	public bool HasHandPlays()
	{
		if (this.m_options == null)
		{
			return false;
		}
		foreach (Network.Options.Option option in this.m_options.List)
		{
			if (option.Type == Network.Options.Option.OptionType.POWER)
			{
				global::Entity entity = this.GetEntity(option.Main.ID);
				if (entity != null)
				{
					global::Card card = entity.GetCard();
					if (!(card == null) && !(card.GetZone() as ZoneHand == null))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06002B48 RID: 11080 RVA: 0x000D8D24 File Offset: 0x000D6F24
	public bool CanShowScoreScreen()
	{
		return this.HasScoreLabels(this.m_gameEntity) || this.HasScoreLabels(this.GetFriendlySidePlayer());
	}

	// Token: 0x06002B49 RID: 11081 RVA: 0x000D8D47 File Offset: 0x000D6F47
	private bool HasScoreLabels(global::Entity entity)
	{
		return entity.HasTag(GAME_TAG.SCORE_LABELID_1) || entity.HasTag(GAME_TAG.SCORE_LABELID_2) || entity.HasTag(GAME_TAG.SCORE_LABELID_3) || entity.HasTag(GAME_TAG.SCORE_FOOTERID);
	}

	// Token: 0x06002B4A RID: 11082 RVA: 0x000D8D86 File Offset: 0x000D6F86
	public int GetFriendlyCardDrawCounter()
	{
		return this.m_friendlyDrawCounter;
	}

	// Token: 0x06002B4B RID: 11083 RVA: 0x000D8D8E File Offset: 0x000D6F8E
	public void IncrementFriendlyCardDrawCounter()
	{
		this.m_friendlyDrawCounter++;
	}

	// Token: 0x06002B4C RID: 11084 RVA: 0x000D8D9E File Offset: 0x000D6F9E
	public void ResetFriendlyCardDrawCounter()
	{
		this.m_friendlyDrawCounter = 0;
	}

	// Token: 0x06002B4D RID: 11085 RVA: 0x000D8DA7 File Offset: 0x000D6FA7
	public int GetOpponentCardDrawCounter()
	{
		return this.m_opponentDrawCounter;
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x000D8DAF File Offset: 0x000D6FAF
	public void IncrementOpponentCardDrawCounter()
	{
		this.m_opponentDrawCounter++;
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x000D8DBF File Offset: 0x000D6FBF
	public void ResetOpponentCardDrawCounter()
	{
		this.m_opponentDrawCounter = 0;
	}

	// Token: 0x06002B50 RID: 11088 RVA: 0x000D8DC8 File Offset: 0x000D6FC8
	private void PreprocessRealTimeTagChange(global::Entity entity, Network.HistTagChange change)
	{
		GAME_TAG tag = (GAME_TAG)change.Tag;
		if (tag != GAME_TAG.PLAYSTATE)
		{
			if (tag != GAME_TAG.CANT_PLAY)
			{
				if (tag != GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD)
				{
					return;
				}
				this.HandleWaitForOpponentReconnectPeriod(change.Value);
				return;
			}
			else if (change.Value > 0)
			{
				this.OnCantPlay(entity);
			}
		}
		else if (GameUtils.IsGameOverTag(change.Entity, change.Tag, change.Value))
		{
			this.OnRealTimeGameOver(change);
			return;
		}
	}

	// Token: 0x06002B51 RID: 11089 RVA: 0x000D8E30 File Offset: 0x000D7030
	private void HandleWaitForOpponentReconnectPeriod(int periodInSeconds)
	{
		this.m_gameEntity.SetTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD, periodInSeconds);
		if (periodInSeconds > 0)
		{
			this.ShowWaitForOpponentReconnectPopup(periodInSeconds);
			TurnTimerUpdate turnTimerUpdate = new TurnTimerUpdate();
			turnTimerUpdate.SetSecondsRemaining(float.PositiveInfinity);
			turnTimerUpdate.SetEndTimestamp(float.PositiveInfinity);
			turnTimerUpdate.SetShow(false);
			this.TriggerTurnTimerUpdate(turnTimerUpdate);
		}
		else
		{
			this.HideWaitForOpponentReconnectPopup();
		}
		GameMgr.Get().UpdatePresence();
	}

	// Token: 0x06002B52 RID: 11090 RVA: 0x000D8E98 File Offset: 0x000D7098
	private void ShowWaitForOpponentReconnectPopup(int periodInSeconds)
	{
		if (this.m_waitForOpponentReconnectPopupInfo == null)
		{
			this.m_waitForOpponentReconnectPopupInfo = new AlertPopup.PopupInfo();
			this.m_waitForOpponentReconnectPopupInfo.m_headerText = GameStrings.Get("GLOBAL_WAIT_FOR_OPPONENT_RECONNECT_HEADER");
			this.m_waitForOpponentReconnectPopupInfo.m_showAlertIcon = false;
			this.m_waitForOpponentReconnectPopupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
			this.m_waitForOpponentReconnectPopupInfo.m_responseUserData = periodInSeconds;
			this.m_waitForOpponentReconnectPopupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			this.m_waitForOpponentReconnectPopupInfo.m_layerToUse = new GameLayer?(GameLayer.UI);
			DialogManager.Get().ShowPopup(this.m_waitForOpponentReconnectPopupInfo, new DialogManager.DialogProcessCallback(this.OnWaitForOpponentReconnectPopupProcessed));
			Gameplay.Get().StartCoroutine(this.IncreaseWaitForOpponentReconnectPeriod());
			return;
		}
		this.UpdateWaitForOpponentReconnectPopup(periodInSeconds);
	}

	// Token: 0x06002B53 RID: 11091 RVA: 0x000D8F4B File Offset: 0x000D714B
	private bool OnWaitForOpponentReconnectPopupProcessed(DialogBase dialog, object userData)
	{
		this.m_waitForOpponentReconnectPopup = (AlertPopup)dialog;
		if (this.m_waitForOpponentReconnectPopupInfo != null)
		{
			this.UpdateWaitForOpponentReconnectPopup((int)this.m_waitForOpponentReconnectPopupInfo.m_responseUserData);
			return true;
		}
		return false;
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x000D8F7A File Offset: 0x000D717A
	private void HideWaitForOpponentReconnectPopup()
	{
		Gameplay.Get().StopCoroutine("IncreaseWaitForOpponentReconnectPeriod");
		if (this.m_waitForOpponentReconnectPopup != null)
		{
			this.m_waitForOpponentReconnectPopup.Hide();
		}
		this.m_waitForOpponentReconnectPopup = null;
		this.m_waitForOpponentReconnectPopupInfo = null;
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x000D8FB4 File Offset: 0x000D71B4
	private void UpdateWaitForOpponentReconnectPopup(int periodInSeconds)
	{
		this.m_waitForOpponentReconnectPopupInfo.m_responseUserData = periodInSeconds;
		int num = periodInSeconds / 60;
		int num2 = periodInSeconds % 60;
		string key = GameMgr.Get().IsSpectator() ? "GLOBAL_WAIT_FOR_OPPONENT_RECONNECT_SPECTATOR" : "GLOBAL_WAIT_FOR_OPPONENT_RECONNECT";
		this.m_waitForOpponentReconnectPopupInfo.m_text = string.Format(GameStrings.Get(key), num, num2);
		if (this.m_waitForOpponentReconnectPopup != null)
		{
			this.m_waitForOpponentReconnectPopup.UpdateInfo(this.m_waitForOpponentReconnectPopupInfo);
		}
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x000D9036 File Offset: 0x000D7236
	private IEnumerator IncreaseWaitForOpponentReconnectPeriod()
	{
		for (;;)
		{
			yield return new WaitForSecondsRealtime(1f);
			if (this.m_waitForOpponentReconnectPopupInfo == null)
			{
				break;
			}
			int num = (int)this.m_waitForOpponentReconnectPopupInfo.m_responseUserData;
			this.UpdateWaitForOpponentReconnectPopup(num + 1);
		}
		yield break;
		yield break;
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x000D9048 File Offset: 0x000D7248
	private void PreprocessTagChange(global::Entity entity, TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag != GAME_TAG.PLAYSTATE)
		{
			if (tag == GAME_TAG.TURN)
			{
				this.OnTurnChanged(change.oldValue, change.newValue);
				return;
			}
			if (tag == GAME_TAG.CURRENT_PLAYER && change.newValue == 1)
			{
				global::Player player = (global::Player)entity;
				this.OnCurrentPlayerChanged(player);
				return;
			}
		}
		else if (GameUtils.IsGameOverTag((global::Player)entity, change.tag, change.newValue))
		{
			this.OnGameOver((TAG_PLAYSTATE)change.newValue);
		}
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x000D90BC File Offset: 0x000D72BC
	private void PreprocessEarlyConcedeTagChange(global::Entity entity, TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.PLAYSTATE && GameUtils.IsGameOverTag((global::Player)entity, change.tag, change.newValue))
		{
			this.OnGameOver((TAG_PLAYSTATE)change.newValue);
		}
	}

	// Token: 0x06002B59 RID: 11097 RVA: 0x000D90FC File Offset: 0x000D72FC
	private void ProcessEarlyConcedeTagChange(global::Entity entity, TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.PLAYSTATE)
		{
			entity.OnTagChanged(change);
		}
	}

	// Token: 0x06002B5A RID: 11098 RVA: 0x000D911C File Offset: 0x000D731C
	private void OnRealTimeGameOver(Network.HistTagChange change)
	{
		this.m_realTimeGameOverTagChange = change;
		if (Network.ShouldBeConnectedToAurora() && Network.IsLoggedIn())
		{
			BnetPresenceMgr.Get().SetPresenceSpectatorJoinInfo(null);
		}
		SpectatorManager.Get().OnRealTimeGameOver();
		this.CheckRestartOnRealTimeGameOver();
	}

	// Token: 0x06002B5B RID: 11099 RVA: 0x000D9150 File Offset: 0x000D7350
	private void OnGameOver(TAG_PLAYSTATE playState)
	{
		this.m_gameOver = true;
		this.m_realTimeGameOverTagChange = null;
		this.m_gameEntity.NotifyOfGameOver(playState);
		this.FireGameOverEvent(playState);
		this.HideWaitForOpponentReconnectPopup();
		GameMgr.Get().LastGameData.GameResult = playState;
		if (this.GetFriendlySidePlayer() != null && this.GetFriendlySidePlayer().GetHero() != null)
		{
			GameMgr.Get().LastGameData.BattlegroundsLeaderboardPlace = this.GetFriendlySidePlayer().GetHero().GetRealTimePlayerLeaderboardPlace();
		}
	}

	// Token: 0x06002B5C RID: 11100 RVA: 0x000D91C8 File Offset: 0x000D73C8
	private void OnCurrentPlayerChanged(global::Player player)
	{
		this.FireCurrentPlayerChangedEvent(player);
	}

	// Token: 0x06002B5D RID: 11101 RVA: 0x000D91D1 File Offset: 0x000D73D1
	private void OnTurnChanged(int oldTurn, int newTurn)
	{
		this.OnTurnChanged_TurnTimer(oldTurn, newTurn);
		this.FireTurnChangedEvent(oldTurn, newTurn);
	}

	// Token: 0x06002B5E RID: 11102 RVA: 0x000D91E3 File Offset: 0x000D73E3
	public IEnumerator RejectUnresolvedChangesAfterDelay()
	{
		yield return new WaitForSecondsRealtime(1f);
		this.RejectUnresolvedOptions();
		yield break;
	}

	// Token: 0x06002B5F RID: 11103 RVA: 0x000D91F2 File Offset: 0x000D73F2
	private void RejectUnresolvedOptions()
	{
		if (this.m_lastSelectedOption == null || this.m_lastOptions == null)
		{
			return;
		}
		if (!ZoneMgr.Get().HasUnresolvedLocalChange())
		{
			return;
		}
		GameState.Get().OnOptionRejected(this.m_lastOptions.ID);
	}

	// Token: 0x06002B60 RID: 11104 RVA: 0x000D9227 File Offset: 0x000D7427
	private void OnCantPlay(global::Entity entity)
	{
		this.FireCantPlayEvent(entity);
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x000D9230 File Offset: 0x000D7430
	public void AddServerBlockingSpell(Spell spell)
	{
		if (spell == null)
		{
			return;
		}
		if (this.m_serverBlockingSpells.Contains(spell))
		{
			return;
		}
		this.m_serverBlockingSpells.Add(spell);
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x000D9257 File Offset: 0x000D7457
	public bool RemoveServerBlockingSpell(Spell spell)
	{
		return this.m_serverBlockingSpells.Remove(spell);
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x000D9265 File Offset: 0x000D7465
	public void AddServerBlockingSpellController(SpellController spellController)
	{
		if (spellController == null)
		{
			return;
		}
		if (this.m_serverBlockingSpellControllers.Contains(spellController))
		{
			return;
		}
		this.m_serverBlockingSpellControllers.Add(spellController);
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x000D928C File Offset: 0x000D748C
	public bool RemoveServerBlockingSpellController(SpellController spellController)
	{
		return this.m_serverBlockingSpellControllers.Remove(spellController);
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x000D929C File Offset: 0x000D749C
	public void DebugNukeServerBlocks()
	{
		while (this.m_serverBlockingSpells.Count > 0)
		{
			this.m_serverBlockingSpells[0].OnSpellFinished();
		}
		while (this.m_serverBlockingSpellControllers.Count > 0)
		{
			this.m_serverBlockingSpellControllers[0].ForceKill();
		}
		this.m_powerProcessor.ForceStopHistoryBlocking();
		this.m_busy = false;
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x000D92FD File Offset: 0x000D74FD
	private bool IsBlockingPowerProcessor()
	{
		return this.m_serverBlockingSpells.Count > 0 || this.m_serverBlockingSpellControllers.Count > 0 || this.m_powerProcessor.IsHistoryBlocking();
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x000D9330 File Offset: 0x000D7530
	private bool ShouldAdvanceReconnectIfStuckTimer()
	{
		using (List<Spell>.Enumerator enumerator = this.m_serverBlockingSpells.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.ShouldReconnectIfStuck())
				{
					return true;
				}
			}
		}
		using (List<SpellController>.Enumerator enumerator2 = this.m_serverBlockingSpellControllers.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.ShouldReconnectIfStuck())
				{
					return true;
				}
			}
		}
		return this.m_powerProcessor.IsHistoryBlocking();
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x000D93E0 File Offset: 0x000D75E0
	public bool MustWaitForChoices()
	{
		if (!ChoiceCardMgr.Get().HasChoices())
		{
			return false;
		}
		PowerProcessor powerProcessor = GameState.Get().GetPowerProcessor();
		if (powerProcessor.HasGameOverTaskList())
		{
			return false;
		}
		foreach (int playerId in GameState.Get().GetPlayerMap().Keys)
		{
			PowerTaskList preChoiceTaskList = ChoiceCardMgr.Get().GetPreChoiceTaskList(playerId);
			if (preChoiceTaskList != null && !powerProcessor.HasTaskList(preChoiceTaskList))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002B69 RID: 11113 RVA: 0x000D947C File Offset: 0x000D767C
	public bool CanProcessPowerQueue()
	{
		return !this.IsBlockingPowerProcessor() && !this.IsBusy() && !this.MustWaitForChoices() && this.m_powerProcessor.GetCurrentTaskList() == null && this.m_powerProcessor.GetPowerQueue().Count != 0 && !this.WasRestartRequested();
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x000D94D5 File Offset: 0x000D76D5
	private bool CheckReconnectIfStuck()
	{
		if (!this.ShouldAdvanceReconnectIfStuckTimer())
		{
			this.m_reconnectIfStuckTimer = 0f;
			return false;
		}
		this.m_reconnectIfStuckTimer += Time.deltaTime;
		if (this.ReconnectIfStuck())
		{
			return true;
		}
		this.ReportStuck();
		return true;
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x000D9510 File Offset: 0x000D7710
	private bool ReconnectIfStuck()
	{
		Network.GameSetup gameSetup = GameMgr.Get().GetGameSetup();
		if (gameSetup.DisconnectWhenStuckSeconds > 0U && this.m_reconnectIfStuckTimer < gameSetup.DisconnectWhenStuckSeconds)
		{
			return false;
		}
		string devElapsedTimeString = TimeUtils.GetDevElapsedTimeString(this.m_reconnectIfStuckTimer);
		string text = this.BuildServerBlockingCausesString();
		Log.Power.PrintWarning("GameState.ReconnectIfStuck() - Blocked more than {0}. Cause:\n{1}", new object[]
		{
			devElapsedTimeString,
			text
		});
		PerformanceAnalytics performanceAnalytics = PerformanceAnalytics.Get();
		if (performanceAnalytics != null)
		{
			performanceAnalytics.ReconnectStart("STUCK");
		}
		Network.Get().DisconnectFromGameServer();
		return true;
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x000D9594 File Offset: 0x000D7794
	private void ReportStuck()
	{
		if (this.m_reconnectIfStuckTimer < 10f)
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup - this.m_lastBlockedReportTimestamp < 3f)
		{
			return;
		}
		this.m_lastBlockedReportTimestamp = realtimeSinceStartup;
		string devElapsedTimeString = TimeUtils.GetDevElapsedTimeString(this.m_reconnectIfStuckTimer);
		string text = this.BuildServerBlockingCausesString();
		Log.Power.PrintWarning("GameState.ReportStuck() - Stuck for {0}. {1}", new object[]
		{
			devElapsedTimeString,
			text
		});
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x000D95FC File Offset: 0x000D77FC
	private string BuildServerBlockingCausesString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		this.AppendServerBlockingSection<Spell>(stringBuilder, "Spells:", this.m_serverBlockingSpells, new GameState.AppendBlockingServerItemCallback<Spell>(this.AppendServerBlockingSpell), ref num);
		this.AppendServerBlockingSection<SpellController>(stringBuilder, "SpellControllers:", this.m_serverBlockingSpellControllers, new GameState.AppendBlockingServerItemCallback<SpellController>(this.AppendServerBlockingSpellController), ref num);
		this.AppendServerBlockingHistory(stringBuilder, ref num);
		if (this.m_busy)
		{
			if (num > 0)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append("Busy=true");
			num++;
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x000D9688 File Offset: 0x000D7888
	private void AppendServerBlockingSection<T>(StringBuilder builder, string sectionPrefix, List<T> items, GameState.AppendBlockingServerItemCallback<T> itemCallback, ref int sectionCount) where T : Component
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

	// Token: 0x06002B6F RID: 11119 RVA: 0x000D9718 File Offset: 0x000D7918
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

	// Token: 0x06002B70 RID: 11120 RVA: 0x000D97E0 File Offset: 0x000D79E0
	private void AppendServerBlockingSpellController(StringBuilder builder, SpellController spellController)
	{
		builder.Append('[');
		builder.Append(spellController.name);
		builder.Append(' ');
		builder.AppendFormat("Source: {0}", spellController.GetSource());
		builder.Append(' ');
		builder.Append("Targets:");
		List<global::Card> targets = spellController.GetTargets();
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
				global::Card card = targets[i];
				builder.Append(card.ToString());
			}
		}
		builder.Append(']');
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x000D9894 File Offset: 0x000D7A94
	private void AppendServerBlockingHistory(StringBuilder builder, ref int sectionCount)
	{
		if (!this.m_powerProcessor.IsHistoryBlocking())
		{
			return;
		}
		global::Entity pendingBigCardEntity = HistoryManager.Get().GetPendingBigCardEntity();
		PowerTaskList historyBlockingTaskList = this.m_powerProcessor.GetHistoryBlockingTaskList();
		PowerTaskList currentTaskList = this.m_powerProcessor.GetCurrentTaskList();
		if (sectionCount > 0)
		{
			builder.Append(' ');
		}
		builder.Append("History: ");
		builder.Append('{');
		builder.AppendFormat("PendingBigCard: {0}", pendingBigCardEntity);
		builder.Append(' ');
		builder.AppendFormat("BlockingTaskList: ", Array.Empty<object>());
		this.PrintBlockingTaskList(builder, historyBlockingTaskList);
		builder.Append(' ');
		builder.AppendFormat("CurrentTaskList: ", Array.Empty<object>());
		this.PrintBlockingTaskList(builder, currentTaskList);
		builder.Append('}');
		sectionCount++;
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x000D9958 File Offset: 0x000D7B58
	public static bool RegisterGameStateInitializedListener(GameState.GameStateInitializedCallback callback, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		GameState.GameStateInitializedListener gameStateInitializedListener = new GameState.GameStateInitializedListener();
		gameStateInitializedListener.SetCallback(callback);
		gameStateInitializedListener.SetUserData(userData);
		if (GameState.s_gameStateInitializedListeners == null)
		{
			GameState.s_gameStateInitializedListeners = new List<GameState.GameStateInitializedListener>();
		}
		else if (GameState.s_gameStateInitializedListeners.Contains(gameStateInitializedListener))
		{
			return false;
		}
		GameState.s_gameStateInitializedListeners.Add(gameStateInitializedListener);
		return true;
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x000D99AC File Offset: 0x000D7BAC
	public static bool UnregisterGameStateInitializedListener(GameState.GameStateInitializedCallback callback, object userData = null)
	{
		if (callback == null || GameState.s_gameStateInitializedListeners == null)
		{
			return false;
		}
		GameState.GameStateInitializedListener gameStateInitializedListener = new GameState.GameStateInitializedListener();
		gameStateInitializedListener.SetCallback(callback);
		gameStateInitializedListener.SetUserData(userData);
		return GameState.s_gameStateInitializedListeners.Remove(gameStateInitializedListener);
	}

	// Token: 0x06002B74 RID: 11124 RVA: 0x000D99E4 File Offset: 0x000D7BE4
	public bool RegisterCreateGameListener(GameState.CreateGameCallback callback)
	{
		return this.RegisterCreateGameListener(callback, null);
	}

	// Token: 0x06002B75 RID: 11125 RVA: 0x000D99F0 File Offset: 0x000D7BF0
	public bool RegisterCreateGameListener(GameState.CreateGameCallback callback, object userData)
	{
		GameState.CreateGameListener createGameListener = new GameState.CreateGameListener();
		createGameListener.SetCallback(callback);
		createGameListener.SetUserData(userData);
		if (this.m_createGameListeners.Contains(createGameListener))
		{
			return false;
		}
		this.m_createGameListeners.Add(createGameListener);
		return true;
	}

	// Token: 0x06002B76 RID: 11126 RVA: 0x000D9A2E File Offset: 0x000D7C2E
	public bool UnregisterCreateGameListener(GameState.CreateGameCallback callback)
	{
		return this.UnregisterCreateGameListener(callback, null);
	}

	// Token: 0x06002B77 RID: 11127 RVA: 0x000D9A38 File Offset: 0x000D7C38
	public bool UnregisterCreateGameListener(GameState.CreateGameCallback callback, object userData)
	{
		GameState.CreateGameListener createGameListener = new GameState.CreateGameListener();
		createGameListener.SetCallback(callback);
		createGameListener.SetUserData(userData);
		return this.m_createGameListeners.Remove(createGameListener);
	}

	// Token: 0x06002B78 RID: 11128 RVA: 0x000D9A65 File Offset: 0x000D7C65
	public bool RegisterOptionsReceivedListener(GameState.OptionsReceivedCallback callback)
	{
		return this.RegisterOptionsReceivedListener(callback, null);
	}

	// Token: 0x06002B79 RID: 11129 RVA: 0x000D9A70 File Offset: 0x000D7C70
	public bool RegisterOptionsReceivedListener(GameState.OptionsReceivedCallback callback, object userData)
	{
		GameState.OptionsReceivedListener optionsReceivedListener = new GameState.OptionsReceivedListener();
		optionsReceivedListener.SetCallback(callback);
		optionsReceivedListener.SetUserData(userData);
		if (this.m_optionsReceivedListeners.Contains(optionsReceivedListener))
		{
			return false;
		}
		this.m_optionsReceivedListeners.Add(optionsReceivedListener);
		return true;
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x000D9AAE File Offset: 0x000D7CAE
	public bool UnregisterOptionsReceivedListener(GameState.OptionsReceivedCallback callback)
	{
		return this.UnregisterOptionsReceivedListener(callback, null);
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x000D9AB8 File Offset: 0x000D7CB8
	public bool UnregisterOptionsReceivedListener(GameState.OptionsReceivedCallback callback, object userData)
	{
		GameState.OptionsReceivedListener optionsReceivedListener = new GameState.OptionsReceivedListener();
		optionsReceivedListener.SetCallback(callback);
		optionsReceivedListener.SetUserData(userData);
		return this.m_optionsReceivedListeners.Remove(optionsReceivedListener);
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x000D9AE8 File Offset: 0x000D7CE8
	public bool RegisterOptionsSentListener(GameState.OptionsSentCallback callback, object userData = null)
	{
		GameState.OptionsSentListener optionsSentListener = new GameState.OptionsSentListener();
		optionsSentListener.SetCallback(callback);
		optionsSentListener.SetUserData(userData);
		if (this.m_optionsSentListeners.Contains(optionsSentListener))
		{
			return false;
		}
		this.m_optionsSentListeners.Add(optionsSentListener);
		return true;
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x000D9B28 File Offset: 0x000D7D28
	public bool UnregisterOptionsReceivedListener(GameState.OptionsSentCallback callback, object userData = null)
	{
		GameState.OptionsSentListener optionsSentListener = new GameState.OptionsSentListener();
		optionsSentListener.SetCallback(callback);
		optionsSentListener.SetUserData(userData);
		return this.m_optionsSentListeners.Remove(optionsSentListener);
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x000D9B58 File Offset: 0x000D7D58
	public bool RegisterOptionRejectedListener(GameState.OptionRejectedCallback callback, object userData = null)
	{
		GameState.OptionRejectedListener optionRejectedListener = new GameState.OptionRejectedListener();
		optionRejectedListener.SetCallback(callback);
		optionRejectedListener.SetUserData(userData);
		if (this.m_optionRejectedListeners.Contains(optionRejectedListener))
		{
			return false;
		}
		this.m_optionRejectedListeners.Add(optionRejectedListener);
		return true;
	}

	// Token: 0x06002B7F RID: 11135 RVA: 0x000D9B98 File Offset: 0x000D7D98
	public bool UnregisterOptionRejectedListener(GameState.OptionRejectedCallback callback, object userData = null)
	{
		GameState.OptionRejectedListener optionRejectedListener = new GameState.OptionRejectedListener();
		optionRejectedListener.SetCallback(callback);
		optionRejectedListener.SetUserData(userData);
		return this.m_optionRejectedListeners.Remove(optionRejectedListener);
	}

	// Token: 0x06002B80 RID: 11136 RVA: 0x000D9BC5 File Offset: 0x000D7DC5
	public bool RegisterEntityChoicesReceivedListener(GameState.EntityChoicesReceivedCallback callback)
	{
		return this.RegisterEntityChoicesReceivedListener(callback, null);
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x000D9BD0 File Offset: 0x000D7DD0
	public bool RegisterEntityChoicesReceivedListener(GameState.EntityChoicesReceivedCallback callback, object userData)
	{
		GameState.EntityChoicesReceivedListener entityChoicesReceivedListener = new GameState.EntityChoicesReceivedListener();
		entityChoicesReceivedListener.SetCallback(callback);
		entityChoicesReceivedListener.SetUserData(userData);
		if (this.m_entityChoicesReceivedListeners.Contains(entityChoicesReceivedListener))
		{
			return false;
		}
		this.m_entityChoicesReceivedListeners.Add(entityChoicesReceivedListener);
		return true;
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x000D9C0E File Offset: 0x000D7E0E
	public bool UnregisterEntityChoicesReceivedListener(GameState.EntityChoicesReceivedCallback callback)
	{
		return this.UnregisterEntityChoicesReceivedListener(callback, null);
	}

	// Token: 0x06002B83 RID: 11139 RVA: 0x000D9C18 File Offset: 0x000D7E18
	public bool UnregisterEntityChoicesReceivedListener(GameState.EntityChoicesReceivedCallback callback, object userData)
	{
		GameState.EntityChoicesReceivedListener entityChoicesReceivedListener = new GameState.EntityChoicesReceivedListener();
		entityChoicesReceivedListener.SetCallback(callback);
		entityChoicesReceivedListener.SetUserData(userData);
		return this.m_entityChoicesReceivedListeners.Remove(entityChoicesReceivedListener);
	}

	// Token: 0x06002B84 RID: 11140 RVA: 0x000D9C45 File Offset: 0x000D7E45
	public bool RegisterEntitiesChosenReceivedListener(GameState.EntitiesChosenReceivedCallback callback)
	{
		return this.RegisterEntitiesChosenReceivedListener(callback, null);
	}

	// Token: 0x06002B85 RID: 11141 RVA: 0x000D9C50 File Offset: 0x000D7E50
	public bool RegisterEntitiesChosenReceivedListener(GameState.EntitiesChosenReceivedCallback callback, object userData)
	{
		GameState.EntitiesChosenReceivedListener entitiesChosenReceivedListener = new GameState.EntitiesChosenReceivedListener();
		entitiesChosenReceivedListener.SetCallback(callback);
		entitiesChosenReceivedListener.SetUserData(userData);
		if (this.m_entitiesChosenReceivedListeners.Contains(entitiesChosenReceivedListener))
		{
			return false;
		}
		this.m_entitiesChosenReceivedListeners.Add(entitiesChosenReceivedListener);
		return true;
	}

	// Token: 0x06002B86 RID: 11142 RVA: 0x000D9C8E File Offset: 0x000D7E8E
	public bool UnregisterEntitiesChosenReceivedListener(GameState.EntitiesChosenReceivedCallback callback)
	{
		return this.UnregisterEntitiesChosenReceivedListener(callback, null);
	}

	// Token: 0x06002B87 RID: 11143 RVA: 0x000D9C98 File Offset: 0x000D7E98
	public bool UnregisterEntitiesChosenReceivedListener(GameState.EntitiesChosenReceivedCallback callback, object userData)
	{
		GameState.EntitiesChosenReceivedListener entitiesChosenReceivedListener = new GameState.EntitiesChosenReceivedListener();
		entitiesChosenReceivedListener.SetCallback(callback);
		entitiesChosenReceivedListener.SetUserData(userData);
		return this.m_entitiesChosenReceivedListeners.Remove(entitiesChosenReceivedListener);
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x000D9CC5 File Offset: 0x000D7EC5
	public bool RegisterCurrentPlayerChangedListener(GameState.CurrentPlayerChangedCallback callback)
	{
		return this.RegisterCurrentPlayerChangedListener(callback, null);
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x000D9CD0 File Offset: 0x000D7ED0
	public bool RegisterCurrentPlayerChangedListener(GameState.CurrentPlayerChangedCallback callback, object userData)
	{
		GameState.CurrentPlayerChangedListener currentPlayerChangedListener = new GameState.CurrentPlayerChangedListener();
		currentPlayerChangedListener.SetCallback(callback);
		currentPlayerChangedListener.SetUserData(userData);
		if (this.m_currentPlayerChangedListeners.Contains(currentPlayerChangedListener))
		{
			return false;
		}
		this.m_currentPlayerChangedListeners.Add(currentPlayerChangedListener);
		return true;
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x000D9D0E File Offset: 0x000D7F0E
	public bool UnregisterCurrentPlayerChangedListener(GameState.CurrentPlayerChangedCallback callback)
	{
		return this.UnregisterCurrentPlayerChangedListener(callback, null);
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x000D9D18 File Offset: 0x000D7F18
	public bool UnregisterCurrentPlayerChangedListener(GameState.CurrentPlayerChangedCallback callback, object userData)
	{
		GameState.CurrentPlayerChangedListener currentPlayerChangedListener = new GameState.CurrentPlayerChangedListener();
		currentPlayerChangedListener.SetCallback(callback);
		currentPlayerChangedListener.SetUserData(userData);
		return this.m_currentPlayerChangedListeners.Remove(currentPlayerChangedListener);
	}

	// Token: 0x06002B8C RID: 11148 RVA: 0x000D9D45 File Offset: 0x000D7F45
	public bool RegisterTurnChangedListener(GameState.TurnChangedCallback callback)
	{
		return this.RegisterTurnChangedListener(callback, null);
	}

	// Token: 0x06002B8D RID: 11149 RVA: 0x000D9D50 File Offset: 0x000D7F50
	public bool RegisterTurnChangedListener(GameState.TurnChangedCallback callback, object userData)
	{
		GameState.TurnChangedListener turnChangedListener = new GameState.TurnChangedListener();
		turnChangedListener.SetCallback(callback);
		turnChangedListener.SetUserData(userData);
		if (this.m_turnChangedListeners.Contains(turnChangedListener))
		{
			return false;
		}
		this.m_turnChangedListeners.Add(turnChangedListener);
		return true;
	}

	// Token: 0x06002B8E RID: 11150 RVA: 0x000D9D8E File Offset: 0x000D7F8E
	public bool UnregisterTurnChangedListener(GameState.TurnChangedCallback callback)
	{
		return this.UnregisterTurnChangedListener(callback, null);
	}

	// Token: 0x06002B8F RID: 11151 RVA: 0x000D9D98 File Offset: 0x000D7F98
	public bool UnregisterTurnChangedListener(GameState.TurnChangedCallback callback, object userData)
	{
		GameState.TurnChangedListener turnChangedListener = new GameState.TurnChangedListener();
		turnChangedListener.SetCallback(callback);
		turnChangedListener.SetUserData(userData);
		return this.m_turnChangedListeners.Remove(turnChangedListener);
	}

	// Token: 0x06002B90 RID: 11152 RVA: 0x000D9DC8 File Offset: 0x000D7FC8
	public bool RegisterFriendlyTurnStartedListener(GameState.FriendlyTurnStartedCallback callback, object userData = null)
	{
		GameState.FriendlyTurnStartedListener friendlyTurnStartedListener = new GameState.FriendlyTurnStartedListener();
		friendlyTurnStartedListener.SetCallback(callback);
		friendlyTurnStartedListener.SetUserData(userData);
		if (this.m_friendlyTurnStartedListeners.Contains(friendlyTurnStartedListener))
		{
			return false;
		}
		this.m_friendlyTurnStartedListeners.Add(friendlyTurnStartedListener);
		return true;
	}

	// Token: 0x06002B91 RID: 11153 RVA: 0x000D9E08 File Offset: 0x000D8008
	public bool UnregisterFriendlyTurnStartedListener(GameState.FriendlyTurnStartedCallback callback, object userData = null)
	{
		GameState.FriendlyTurnStartedListener friendlyTurnStartedListener = new GameState.FriendlyTurnStartedListener();
		friendlyTurnStartedListener.SetCallback(callback);
		friendlyTurnStartedListener.SetUserData(userData);
		return this.m_friendlyTurnStartedListeners.Remove(friendlyTurnStartedListener);
	}

	// Token: 0x06002B92 RID: 11154 RVA: 0x000D9E35 File Offset: 0x000D8035
	public bool RegisterTurnTimerUpdateListener(GameState.TurnTimerUpdateCallback callback)
	{
		return this.RegisterTurnTimerUpdateListener(callback, null);
	}

	// Token: 0x06002B93 RID: 11155 RVA: 0x000D9E40 File Offset: 0x000D8040
	public bool RegisterTurnTimerUpdateListener(GameState.TurnTimerUpdateCallback callback, object userData)
	{
		GameState.TurnTimerUpdateListener turnTimerUpdateListener = new GameState.TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		if (this.m_turnTimerUpdateListeners.Contains(turnTimerUpdateListener))
		{
			return false;
		}
		this.m_turnTimerUpdateListeners.Add(turnTimerUpdateListener);
		return true;
	}

	// Token: 0x06002B94 RID: 11156 RVA: 0x000D9E7E File Offset: 0x000D807E
	public bool UnregisterTurnTimerUpdateListener(GameState.TurnTimerUpdateCallback callback)
	{
		return this.UnregisterTurnTimerUpdateListener(callback, null);
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x000D9E88 File Offset: 0x000D8088
	public bool UnregisterTurnTimerUpdateListener(GameState.TurnTimerUpdateCallback callback, object userData)
	{
		GameState.TurnTimerUpdateListener turnTimerUpdateListener = new GameState.TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		return this.m_turnTimerUpdateListeners.Remove(turnTimerUpdateListener);
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x000D9EB5 File Offset: 0x000D80B5
	public bool RegisterMulliganTimerUpdateListener(GameState.TurnTimerUpdateCallback callback)
	{
		return this.RegisterMulliganTimerUpdateListener(callback, null);
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x000D9EC0 File Offset: 0x000D80C0
	public bool RegisterMulliganTimerUpdateListener(GameState.TurnTimerUpdateCallback callback, object userData)
	{
		GameState.TurnTimerUpdateListener turnTimerUpdateListener = new GameState.TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		if (this.m_mulliganTimerUpdateListeners.Contains(turnTimerUpdateListener))
		{
			return false;
		}
		this.m_mulliganTimerUpdateListeners.Add(turnTimerUpdateListener);
		return true;
	}

	// Token: 0x06002B98 RID: 11160 RVA: 0x000D9EFE File Offset: 0x000D80FE
	public bool UnregisterMulliganTimerUpdateListener(GameState.TurnTimerUpdateCallback callback)
	{
		return this.UnregisterMulliganTimerUpdateListener(callback, null);
	}

	// Token: 0x06002B99 RID: 11161 RVA: 0x000D9F08 File Offset: 0x000D8108
	public bool UnregisterMulliganTimerUpdateListener(GameState.TurnTimerUpdateCallback callback, object userData)
	{
		GameState.TurnTimerUpdateListener turnTimerUpdateListener = new GameState.TurnTimerUpdateListener();
		turnTimerUpdateListener.SetCallback(callback);
		turnTimerUpdateListener.SetUserData(userData);
		return this.m_mulliganTimerUpdateListeners.Remove(turnTimerUpdateListener);
	}

	// Token: 0x06002B9A RID: 11162 RVA: 0x000D9F38 File Offset: 0x000D8138
	public bool RegisterSpectatorNotifyListener(GameState.SpectatorNotifyEventCallback callback, object userData = null)
	{
		GameState.SpectatorNotifyListener spectatorNotifyListener = new GameState.SpectatorNotifyListener();
		spectatorNotifyListener.SetCallback(callback);
		spectatorNotifyListener.SetUserData(userData);
		if (this.m_spectatorNotifyListeners.Contains(spectatorNotifyListener))
		{
			return false;
		}
		this.m_spectatorNotifyListeners.Add(spectatorNotifyListener);
		return true;
	}

	// Token: 0x06002B9B RID: 11163 RVA: 0x000D9F78 File Offset: 0x000D8178
	public bool UnregisterSpectatorNotifyListener(GameState.SpectatorNotifyEventCallback callback, object userData = null)
	{
		GameState.SpectatorNotifyListener spectatorNotifyListener = new GameState.SpectatorNotifyListener();
		spectatorNotifyListener.SetCallback(callback);
		spectatorNotifyListener.SetUserData(userData);
		return this.m_spectatorNotifyListeners.Remove(spectatorNotifyListener);
	}

	// Token: 0x06002B9C RID: 11164 RVA: 0x000D9FA8 File Offset: 0x000D81A8
	public bool RegisterGameOverListener(GameState.GameOverCallback callback, object userData = null)
	{
		GameState.GameOverListener gameOverListener = new GameState.GameOverListener();
		gameOverListener.SetCallback(callback);
		gameOverListener.SetUserData(userData);
		if (this.m_gameOverListeners.Contains(gameOverListener))
		{
			return false;
		}
		this.m_gameOverListeners.Add(gameOverListener);
		return true;
	}

	// Token: 0x06002B9D RID: 11165 RVA: 0x000D9FE8 File Offset: 0x000D81E8
	public bool UnregisterGameOverListener(GameState.GameOverCallback callback, object userData = null)
	{
		GameState.GameOverListener gameOverListener = new GameState.GameOverListener();
		gameOverListener.SetCallback(callback);
		gameOverListener.SetUserData(userData);
		return this.m_gameOverListeners.Remove(gameOverListener);
	}

	// Token: 0x06002B9E RID: 11166 RVA: 0x000DA018 File Offset: 0x000D8218
	public bool RegisterHeroChangedListener(GameState.HeroChangedCallback callback, object userData = null)
	{
		GameState.HeroChangedListener heroChangedListener = new GameState.HeroChangedListener();
		heroChangedListener.SetCallback(callback);
		heroChangedListener.SetUserData(userData);
		if (this.m_heroChangedListeners.Contains(heroChangedListener))
		{
			return false;
		}
		this.m_heroChangedListeners.Add(heroChangedListener);
		return true;
	}

	// Token: 0x06002B9F RID: 11167 RVA: 0x000DA058 File Offset: 0x000D8258
	public bool UnregisterHeroChangedListener(GameState.HeroChangedCallback callback, object userData = null)
	{
		GameState.HeroChangedListener heroChangedListener = new GameState.HeroChangedListener();
		heroChangedListener.SetCallback(callback);
		heroChangedListener.SetUserData(userData);
		return this.m_heroChangedListeners.Remove(heroChangedListener);
	}

	// Token: 0x06002BA0 RID: 11168 RVA: 0x000DA088 File Offset: 0x000D8288
	public bool RegisterCantPlayListener(GameState.CantPlayCallback callback, object userData = null)
	{
		GameState.CantPlayListener cantPlayListener = new GameState.CantPlayListener();
		cantPlayListener.SetCallback(callback);
		cantPlayListener.SetUserData(userData);
		if (this.m_cantPlayListeners.Contains(cantPlayListener))
		{
			return false;
		}
		this.m_cantPlayListeners.Add(cantPlayListener);
		return true;
	}

	// Token: 0x06002BA1 RID: 11169 RVA: 0x000DA0C8 File Offset: 0x000D82C8
	public bool UnregisterCantPlayListener(GameState.CantPlayCallback callback, object userData = null)
	{
		GameState.CantPlayListener cantPlayListener = new GameState.CantPlayListener();
		cantPlayListener.SetCallback(callback);
		cantPlayListener.SetUserData(userData);
		return this.m_cantPlayListeners.Remove(cantPlayListener);
	}

	// Token: 0x06002BA2 RID: 11170 RVA: 0x000DA0F8 File Offset: 0x000D82F8
	private static void FireGameStateInitializedEvent()
	{
		if (GameState.s_gameStateInitializedListeners == null)
		{
			return;
		}
		GameState.GameStateInitializedListener[] array = GameState.s_gameStateInitializedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(GameState.s_instance);
		}
	}

	// Token: 0x06002BA3 RID: 11171 RVA: 0x000DA134 File Offset: 0x000D8334
	private void FireCreateGameEvent()
	{
		GameState.CreateGameListener[] array = this.m_createGameListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this.m_createGamePhase);
		}
	}

	// Token: 0x06002BA4 RID: 11172 RVA: 0x000DA16C File Offset: 0x000D836C
	private void FireOptionsReceivedEvent()
	{
		GameState.OptionsReceivedListener[] array = this.m_optionsReceivedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06002BA5 RID: 11173 RVA: 0x000DA19C File Offset: 0x000D839C
	private void FireOptionsSentEvent(Network.Options.Option option)
	{
		GameState.OptionsSentListener[] array = this.m_optionsSentListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(option);
		}
	}

	// Token: 0x06002BA6 RID: 11174 RVA: 0x000DA1CC File Offset: 0x000D83CC
	private void FireOptionRejectedEvent(Network.Options.Option option)
	{
		GameState.OptionRejectedListener[] array = this.m_optionRejectedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(option);
		}
	}

	// Token: 0x06002BA7 RID: 11175 RVA: 0x000DA1FC File Offset: 0x000D83FC
	private void FireEntityChoicesReceivedEvent(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		GameState.EntityChoicesReceivedListener[] array = this.m_entityChoicesReceivedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(choices, preChoiceTaskList);
		}
	}

	// Token: 0x06002BA8 RID: 11176 RVA: 0x000DA230 File Offset: 0x000D8430
	private bool FireEntitiesChosenReceivedEvent(Network.EntitiesChosen chosen)
	{
		GameState.EntitiesChosenReceivedListener[] array = this.m_entitiesChosenReceivedListeners.ToArray();
		bool flag = false;
		GameState.EntitiesChosenReceivedListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			flag = (array2[i].Fire(chosen) || flag);
		}
		return flag;
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x000DA268 File Offset: 0x000D8468
	private void FireTurnChangedEvent(int oldTurn, int newTurn)
	{
		GameState.TurnChangedListener[] array = this.m_turnChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(oldTurn, newTurn);
		}
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x000DA29C File Offset: 0x000D849C
	public void FireFriendlyTurnStartedEvent()
	{
		this.m_gameEntity.NotifyOfStartOfTurnEventsFinished();
		GameState.FriendlyTurnStartedListener[] array = this.m_friendlyTurnStartedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x000DA2D8 File Offset: 0x000D84D8
	private void FireTurnTimerUpdateEvent(TurnTimerUpdate update)
	{
		if (this.GetGameEntity() == null)
		{
			UnityEngine.Debug.LogWarning("FireTurnTimerUpdateEvent - Turn timer update received before game entity created.");
			return;
		}
		GameState.TurnTimerUpdateListener[] array;
		if (this.GetGameEntity().IsMulliganActiveRealTime())
		{
			array = this.m_mulliganTimerUpdateListeners.ToArray();
		}
		else
		{
			array = this.m_turnTimerUpdateListeners.ToArray();
		}
		GameState.TurnTimerUpdateListener[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].Fire(update);
		}
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x000DA33C File Offset: 0x000D853C
	private void FireCantPlayEvent(global::Entity entity)
	{
		GameState.CantPlayListener[] array = this.m_cantPlayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(entity);
		}
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x000DA36C File Offset: 0x000D856C
	private void FireCurrentPlayerChangedEvent(global::Player player)
	{
		GameState.CurrentPlayerChangedListener[] array = this.m_currentPlayerChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(player);
		}
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x000DA39C File Offset: 0x000D859C
	private void FireSpectatorNotifyEvent(SpectatorNotify notify)
	{
		GameState.SpectatorNotifyListener[] array = this.m_spectatorNotifyListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(notify);
		}
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x000DA3CC File Offset: 0x000D85CC
	private void FireGameOverEvent(TAG_PLAYSTATE playState)
	{
		GameState.GameOverListener[] array = this.m_gameOverListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(playState);
		}
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x000DA3FC File Offset: 0x000D85FC
	public void FireHeroChangedEvent(global::Player player)
	{
		GameState.HeroChangedListener[] array = this.m_heroChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(player);
		}
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x000DA42C File Offset: 0x000D862C
	public GameState.ResponseMode GetResponseMode()
	{
		return this.m_responseMode;
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x000DA434 File Offset: 0x000D8634
	public Network.EntityChoices GetFriendlyEntityChoices()
	{
		int friendlyPlayerId = this.GetFriendlyPlayerId();
		return this.GetEntityChoices(friendlyPlayerId);
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x000DA450 File Offset: 0x000D8650
	public Network.EntityChoices GetOpponentEntityChoices()
	{
		int opposingPlayerId = this.GetOpposingPlayerId();
		return this.GetEntityChoices(opposingPlayerId);
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x000DA46C File Offset: 0x000D866C
	public Network.EntityChoices GetEntityChoices(int playerId)
	{
		Network.EntityChoices result;
		this.m_choicesMap.TryGetValue(playerId, out result);
		return result;
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x000DA489 File Offset: 0x000D8689
	public Map<int, Network.EntityChoices> GetEntityChoicesMap()
	{
		return this.m_choicesMap;
	}

	// Token: 0x06002BB6 RID: 11190 RVA: 0x000DA494 File Offset: 0x000D8694
	public bool IsChoosableEntity(global::Entity entity)
	{
		Network.EntityChoices friendlyEntityChoices = this.GetFriendlyEntityChoices();
		return friendlyEntityChoices != null && friendlyEntityChoices.Entities.Contains(entity.GetEntityId());
	}

	// Token: 0x06002BB7 RID: 11191 RVA: 0x000DA4BE File Offset: 0x000D86BE
	public bool IsChosenEntity(global::Entity entity)
	{
		return this.GetFriendlyEntityChoices() != null && this.m_chosenEntities.Contains(entity);
	}

	// Token: 0x06002BB8 RID: 11192 RVA: 0x000DA4D8 File Offset: 0x000D86D8
	public bool AddChosenEntity(global::Entity entity)
	{
		if (this.m_chosenEntities.Contains(entity))
		{
			return false;
		}
		this.m_chosenEntities.Add(entity);
		ChoiceCardMgr.Get().OnChosenEntityAdded(entity);
		global::Card card = entity.GetCard();
		if (card != null)
		{
			card.UpdateActorState(false);
		}
		return true;
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x000DA524 File Offset: 0x000D8724
	public bool RemoveChosenEntity(global::Entity entity)
	{
		if (!this.m_chosenEntities.Remove(entity))
		{
			return false;
		}
		ChoiceCardMgr.Get().OnChosenEntityRemoved(entity);
		global::Card card = entity.GetCard();
		if (card != null)
		{
			card.UpdateActorState(false);
		}
		return true;
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x000DA564 File Offset: 0x000D8764
	public List<global::Entity> GetChosenEntities()
	{
		return this.m_chosenEntities;
	}

	// Token: 0x06002BBB RID: 11195 RVA: 0x000DA56C File Offset: 0x000D876C
	public Network.Options GetOptionsPacket()
	{
		return this.m_options;
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x000DA574 File Offset: 0x000D8774
	public void EnterChoiceMode()
	{
		this.m_responseMode = GameState.ResponseMode.CHOICE;
		this.UpdateOptionHighlights();
		this.UpdateChoiceHighlights();
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000DA58C File Offset: 0x000D878C
	public void EnterMainOptionMode()
	{
		GameState.ResponseMode responseMode = this.m_responseMode;
		this.m_responseMode = GameState.ResponseMode.OPTION;
		if (responseMode == GameState.ResponseMode.SUB_OPTION)
		{
			Network.Options.Option option = this.m_options.List[this.m_selectedOption.m_main];
			this.UpdateSubOptionHighlights(option);
		}
		else if (responseMode == GameState.ResponseMode.OPTION_TARGET)
		{
			Network.Options.Option option2 = this.m_options.List[this.m_selectedOption.m_main];
			this.UpdateTargetHighlights(option2.Main);
			if (this.m_selectedOption.m_sub != -1)
			{
				Network.Options.Option.SubOption subOption = option2.Subs[this.m_selectedOption.m_sub];
				this.UpdateTargetHighlights(subOption);
			}
		}
		this.UpdateOptionHighlights(this.m_lastOptions);
		this.UpdateOptionHighlights();
		this.m_selectedOption.Clear();
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x000DA648 File Offset: 0x000D8848
	public void EnterSubOptionMode()
	{
		Network.Options.Option option = this.m_options.List[this.m_selectedOption.m_main];
		if (this.m_responseMode == GameState.ResponseMode.OPTION)
		{
			this.m_responseMode = GameState.ResponseMode.SUB_OPTION;
			this.UpdateOptionHighlights();
		}
		else if (this.m_responseMode == GameState.ResponseMode.OPTION_TARGET)
		{
			this.m_responseMode = GameState.ResponseMode.SUB_OPTION;
			Network.Options.Option.SubOption subOption = option.Subs[this.m_selectedOption.m_sub];
			this.UpdateTargetHighlights(subOption);
		}
		this.UpdateSubOptionHighlights(option);
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x000DA6C0 File Offset: 0x000D88C0
	public void EnterOptionTargetMode()
	{
		if (this.m_responseMode == GameState.ResponseMode.OPTION)
		{
			this.m_responseMode = GameState.ResponseMode.OPTION_TARGET;
			this.UpdateOptionHighlights();
			Network.Options.Option option = this.m_options.List[this.m_selectedOption.m_main];
			this.UpdateTargetHighlights(option.Main);
			return;
		}
		if (this.m_responseMode == GameState.ResponseMode.SUB_OPTION)
		{
			this.m_responseMode = GameState.ResponseMode.OPTION_TARGET;
			Network.Options.Option option2 = this.m_options.List[this.m_selectedOption.m_main];
			this.UpdateSubOptionHighlights(option2);
			Network.Options.Option.SubOption subOption = option2.Subs[this.m_selectedOption.m_sub];
			this.UpdateTargetHighlights(subOption);
		}
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x000DA75D File Offset: 0x000D895D
	public void EnterMoveMinionMode(global::Entity heldEntity, bool suppressGlow = false)
	{
		this.ActivateMoveMinionTargets(heldEntity, suppressGlow);
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x000DA767 File Offset: 0x000D8967
	public void ExitMoveMinionMode()
	{
		this.DeactivateMoveMinionTargetHighlights();
	}

	// Token: 0x06002BC2 RID: 11202 RVA: 0x000DA76F File Offset: 0x000D896F
	public void CancelCurrentOptionMode()
	{
		if (this.IsInTargetMode())
		{
			this.GetGameEntity().NotifyOfTargetModeCancelled();
		}
		this.CancelSelectedOptionProposedMana();
		this.EnterMainOptionMode();
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x000DA790 File Offset: 0x000D8990
	public bool IsInMainOptionMode()
	{
		return this.m_responseMode == GameState.ResponseMode.OPTION;
	}

	// Token: 0x06002BC4 RID: 11204 RVA: 0x000DA79B File Offset: 0x000D899B
	public bool IsInSubOptionMode()
	{
		return this.m_responseMode == GameState.ResponseMode.SUB_OPTION;
	}

	// Token: 0x06002BC5 RID: 11205 RVA: 0x000DA7A6 File Offset: 0x000D89A6
	public bool IsInTargetMode()
	{
		return this.m_responseMode == GameState.ResponseMode.OPTION_TARGET;
	}

	// Token: 0x06002BC6 RID: 11206 RVA: 0x000DA7B1 File Offset: 0x000D89B1
	public bool IsInChoiceMode()
	{
		return this.m_responseMode == GameState.ResponseMode.CHOICE;
	}

	// Token: 0x06002BC7 RID: 11207 RVA: 0x000DA7BC File Offset: 0x000D89BC
	public void SetSelectedOption(ChooseOption packet)
	{
		this.m_selectedOption.m_main = packet.Index;
		this.m_selectedOption.m_sub = packet.SubOption;
		this.m_selectedOption.m_target = packet.Target;
		this.m_selectedOption.m_position = packet.Position;
	}

	// Token: 0x06002BC8 RID: 11208 RVA: 0x000DA810 File Offset: 0x000D8A10
	public void SetChosenEntities(ChooseEntities packet)
	{
		this.m_chosenEntities.Clear();
		foreach (int id in packet.Entities)
		{
			global::Entity entity = this.GetEntity(id);
			if (entity != null)
			{
				this.m_chosenEntities.Add(entity);
			}
		}
	}

	// Token: 0x06002BC9 RID: 11209 RVA: 0x000DA880 File Offset: 0x000D8A80
	public void SetSelectedOption(int index)
	{
		this.m_selectedOption.m_main = index;
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x000DA88E File Offset: 0x000D8A8E
	public int GetSelectedOption()
	{
		return this.m_selectedOption.m_main;
	}

	// Token: 0x06002BCB RID: 11211 RVA: 0x000DA89B File Offset: 0x000D8A9B
	public void SetSelectedSubOption(int index)
	{
		this.m_selectedOption.m_sub = index;
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x000DA8A9 File Offset: 0x000D8AA9
	public int GetSelectedSubOption()
	{
		return this.m_selectedOption.m_sub;
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x000DA8B6 File Offset: 0x000D8AB6
	public void SetSelectedOptionTarget(int target)
	{
		this.m_selectedOption.m_target = target;
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x000DA8C4 File Offset: 0x000D8AC4
	public int GetSelectedOptionTarget()
	{
		return this.m_selectedOption.m_target;
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x000DA8D4 File Offset: 0x000D8AD4
	public bool IsSelectedOptionFriendlyHero()
	{
		global::Entity hero = this.GetFriendlySidePlayer().GetHero();
		Network.Options.Option selectedNetworkOption = this.GetSelectedNetworkOption();
		return selectedNetworkOption != null && selectedNetworkOption.Main.ID == hero.GetEntityId();
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x000DA90C File Offset: 0x000D8B0C
	public bool IsSelectedOptionFriendlyHeroPower()
	{
		global::Entity heroPower = this.GetFriendlySidePlayer().GetHeroPower();
		if (heroPower == null)
		{
			return false;
		}
		Network.Options.Option selectedNetworkOption = this.GetSelectedNetworkOption();
		return selectedNetworkOption != null && selectedNetworkOption.Main.ID == heroPower.GetEntityId();
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000DA949 File Offset: 0x000D8B49
	public void SetSelectedOptionPosition(int position)
	{
		this.m_selectedOption.m_position = position;
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x000DA957 File Offset: 0x000D8B57
	public int GetSelectedOptionPosition()
	{
		return this.m_selectedOption.m_position;
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x000DA964 File Offset: 0x000D8B64
	public Network.Options.Option GetSelectedNetworkOption()
	{
		if (this.m_selectedOption.m_main < 0)
		{
			return null;
		}
		return this.m_options.List[this.m_selectedOption.m_main];
	}

	// Token: 0x06002BD4 RID: 11220 RVA: 0x000DA994 File Offset: 0x000D8B94
	public Network.Options.Option.SubOption GetSelectedNetworkSubOption()
	{
		if (this.m_selectedOption.m_main < 0)
		{
			return null;
		}
		Network.Options.Option option = this.m_options.List[this.m_selectedOption.m_main];
		if (this.m_selectedOption.m_sub == -1)
		{
			return option.Main;
		}
		return option.Subs[this.m_selectedOption.m_sub];
	}

	// Token: 0x06002BD5 RID: 11221 RVA: 0x000DA9F8 File Offset: 0x000D8BF8
	public bool EntityHasSubOptions(global::Entity entity)
	{
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = this.GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.ID == entityId)
			{
				return option.Subs != null && option.Subs.Count > 0;
			}
		}
		return false;
	}

	// Token: 0x06002BD6 RID: 11222 RVA: 0x000DAA6C File Offset: 0x000D8C6C
	public bool EntityHasTargets(global::Entity entity)
	{
		return this.EntityHasTargets(entity, false);
	}

	// Token: 0x06002BD7 RID: 11223 RVA: 0x000DAA76 File Offset: 0x000D8C76
	public bool SubEntityHasTargets(global::Entity subEntity)
	{
		return this.EntityHasTargets(subEntity, true);
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x000DAA80 File Offset: 0x000D8C80
	public bool HasSubOptions(global::Entity entity)
	{
		if (!this.IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = this.GetOptionsPacket();
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

	// Token: 0x06002BD9 RID: 11225 RVA: 0x000DAAF0 File Offset: 0x000D8CF0
	public int? GetErrorParam(global::Entity entity)
	{
		Network.Options optionsPacket = this.GetOptionsPacket();
		if (optionsPacket == null)
		{
			return null;
		}
		switch (this.GetResponseMode())
		{
		case GameState.ResponseMode.OPTION:
		{
			Network.Options.Option optionFromEntityID = optionsPacket.GetOptionFromEntityID(entity.GetEntityId());
			if (optionFromEntityID != null && optionFromEntityID.Type == Network.Options.Option.OptionType.POWER)
			{
				return optionFromEntityID.Main.PlayErrorInfo.PlayErrorParam;
			}
			break;
		}
		case GameState.ResponseMode.SUB_OPTION:
		{
			Network.Options.Option.SubOption subOptionFromEntityID = this.GetSelectedNetworkOption().GetSubOptionFromEntityID(entity.GetEntityId());
			if (subOptionFromEntityID != null)
			{
				return subOptionFromEntityID.PlayErrorInfo.PlayErrorParam;
			}
			break;
		}
		case GameState.ResponseMode.OPTION_TARGET:
			return this.GetSelectedNetworkSubOption().GetErrorParamForTarget(entity.GetEntityId());
		}
		return null;
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x000DAB98 File Offset: 0x000D8D98
	public PlayErrors.ErrorType GetErrorType(global::Entity entity)
	{
		Network.Options optionsPacket = this.GetOptionsPacket();
		if (optionsPacket == null || !GameState.Get().IsFriendlySidePlayerTurn())
		{
			return PlayErrors.ErrorType.REQ_YOUR_TURN;
		}
		switch (this.GetResponseMode())
		{
		case GameState.ResponseMode.OPTION:
		{
			Network.Options.Option optionFromEntityID = optionsPacket.GetOptionFromEntityID(entity.GetEntityId());
			if (optionFromEntityID != null && optionFromEntityID.Type == Network.Options.Option.OptionType.POWER)
			{
				return optionFromEntityID.Main.PlayErrorInfo.PlayError;
			}
			break;
		}
		case GameState.ResponseMode.SUB_OPTION:
		{
			Network.Options.Option.SubOption subOptionFromEntityID = this.GetSelectedNetworkOption().GetSubOptionFromEntityID(entity.GetEntityId());
			if (subOptionFromEntityID != null)
			{
				return subOptionFromEntityID.PlayErrorInfo.PlayError;
			}
			break;
		}
		case GameState.ResponseMode.OPTION_TARGET:
			return this.GetSelectedNetworkSubOption().GetErrorForTarget(entity.GetEntityId());
		}
		return PlayErrors.ErrorType.INVALID;
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x000DAC3C File Offset: 0x000D8E3C
	public bool HasResponse(global::Entity entity)
	{
		switch (this.GetResponseMode())
		{
		case GameState.ResponseMode.OPTION:
			return this.IsValidOption(entity);
		case GameState.ResponseMode.SUB_OPTION:
			return this.IsValidSubOption(entity);
		case GameState.ResponseMode.OPTION_TARGET:
			return this.IsValidOptionTarget(entity, true);
		case GameState.ResponseMode.CHOICE:
			return this.IsChoice(entity);
		default:
			return false;
		}
	}

	// Token: 0x06002BDC RID: 11228 RVA: 0x000DAC8C File Offset: 0x000D8E8C
	public bool IsChoice(global::Entity entity)
	{
		return this.IsEntityInputEnabled(entity) && this.IsChoosableEntity(entity) && !this.IsChosenEntity(entity);
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x000DACB0 File Offset: 0x000D8EB0
	public bool IsValidOption(global::Entity entity)
	{
		if (!this.IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = this.GetOptionsPacket();
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

	// Token: 0x06002BDE RID: 11230 RVA: 0x000DAD2C File Offset: 0x000D8F2C
	public bool IsValidSubOption(global::Entity entity)
	{
		if (!this.IsEntityInputEnabled(entity))
		{
			return false;
		}
		int entityId = entity.GetEntityId();
		Network.Options.Option selectedNetworkOption = this.GetSelectedNetworkOption();
		for (int i = 0; i < selectedNetworkOption.Subs.Count; i++)
		{
			Network.Options.Option.SubOption subOption = selectedNetworkOption.Subs[i];
			if (subOption.ID == entityId)
			{
				return subOption.PlayErrorInfo.IsValid();
			}
		}
		return false;
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x000DAD90 File Offset: 0x000D8F90
	public bool IsValidOptionTarget(global::Entity entity, bool checkInputEnabled)
	{
		if (checkInputEnabled && !this.IsEntityInputEnabled(entity))
		{
			return false;
		}
		Network.Options.Option.SubOption selectedNetworkSubOption = this.GetSelectedNetworkSubOption();
		return selectedNetworkSubOption != null && selectedNetworkSubOption.IsValidTarget(entity.GetEntityId());
	}

	// Token: 0x06002BE0 RID: 11232 RVA: 0x000DADC4 File Offset: 0x000D8FC4
	public bool IsEntityInputEnabled(global::Entity entity)
	{
		if (this.IsResponsePacketBlocked())
		{
			return false;
		}
		if (entity.IsBusy())
		{
			return false;
		}
		global::Card card = entity.GetCard();
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

	// Token: 0x06002BE1 RID: 11233 RVA: 0x000DAE1C File Offset: 0x000D901C
	private bool EntityHasTargets(global::Entity entity, bool isSubEntity)
	{
		int entityId = entity.GetEntityId();
		Network.Options optionsPacket = this.GetOptionsPacket();
		if (optionsPacket == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER)
			{
				if (isSubEntity)
				{
					if (option.Subs != null)
					{
						for (int j = 0; j < option.Subs.Count; j++)
						{
							Network.Options.Option.SubOption subOption = option.Subs[j];
							if (subOption.ID == entityId)
							{
								return subOption.HasValidTarget();
							}
						}
					}
				}
				else if (option.Main.ID == entityId)
				{
					return option.Main.HasValidTarget();
				}
			}
		}
		return false;
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x000DAED0 File Offset: 0x000D90D0
	private void CancelSelectedOptionProposedMana()
	{
		Network.Options.Option selectedNetworkOption = this.GetSelectedNetworkOption();
		if (selectedNetworkOption == null)
		{
			return;
		}
		this.GetFriendlySidePlayer().CancelAllProposedMana(this.GetEntity(selectedNetworkOption.Main.ID));
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x000DAF04 File Offset: 0x000D9104
	public void ClearResponseMode()
	{
		Log.Hand.Print("ClearResponseMode", Array.Empty<object>());
		this.m_responseMode = GameState.ResponseMode.NONE;
		if (this.m_options != null)
		{
			for (int i = 0; i < this.m_options.List.Count; i++)
			{
				Network.Options.Option option = this.m_options.List[i];
				if (option.Type == Network.Options.Option.OptionType.POWER)
				{
					global::Entity entity = this.GetEntity(option.Main.ID);
					if (entity != null)
					{
						entity.ClearBattlecryFlag();
					}
				}
			}
			this.UpdateHighlightsBasedOnSelection();
			this.UpdateOptionHighlights(this.m_options);
			return;
		}
		if (this.GetFriendlyEntityChoices() != null)
		{
			this.UpdateChoiceHighlights();
		}
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000DAFA8 File Offset: 0x000D91A8
	public void UpdateChoiceHighlights()
	{
		foreach (Network.EntityChoices entityChoices in this.m_choicesMap.Values)
		{
			global::Entity entity = this.GetEntity(entityChoices.Source);
			if (entity != null)
			{
				global::Card card = entity.GetCard();
				if (card != null)
				{
					card.UpdateActorState(false);
				}
			}
			foreach (int id in entityChoices.Entities)
			{
				global::Entity entity2 = this.GetEntity(id);
				if (entity2 != null)
				{
					global::Card card2 = entity2.GetCard();
					if (!(card2 == null))
					{
						card2.UpdateActorState(false);
					}
				}
			}
		}
		foreach (global::Entity entity3 in this.m_chosenEntities)
		{
			global::Card card3 = entity3.GetCard();
			if (!(card3 == null))
			{
				card3.UpdateActorState(false);
			}
		}
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x000DB0E0 File Offset: 0x000D92E0
	private void UpdateHighlightsBasedOnSelection()
	{
		if (this.m_selectedOption.m_target != 0)
		{
			Network.Options.Option.SubOption selectedNetworkSubOption = this.GetSelectedNetworkSubOption();
			if (selectedNetworkSubOption != null)
			{
				this.UpdateTargetHighlights(selectedNetworkSubOption);
				return;
			}
		}
		else if (this.m_selectedOption.m_sub >= 0)
		{
			Network.Options.Option selectedNetworkOption = this.GetSelectedNetworkOption();
			this.UpdateSubOptionHighlights(selectedNetworkOption);
		}
	}

	// Token: 0x06002BE6 RID: 11238 RVA: 0x000DB128 File Offset: 0x000D9328
	public void UpdateOptionHighlights()
	{
		this.UpdateOptionHighlights(this.m_options);
	}

	// Token: 0x06002BE7 RID: 11239 RVA: 0x000DB138 File Offset: 0x000D9338
	public void UpdateOptionHighlights(Network.Options options)
	{
		if (options == null || options.List == null)
		{
			return;
		}
		for (int i = 0; i < options.List.Count; i++)
		{
			Network.Options.Option option = options.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER)
			{
				global::Entity entity = this.GetEntity(option.Main.ID);
				if (entity != null)
				{
					global::Card card = entity.GetCard();
					if (!(card == null))
					{
						card.UpdateActorState(false);
					}
				}
			}
		}
	}

	// Token: 0x06002BE8 RID: 11240 RVA: 0x000DB1AC File Offset: 0x000D93AC
	private void UpdateSubOptionHighlights(Network.Options.Option option)
	{
		global::Entity entity = this.GetEntity(option.Main.ID);
		if (entity != null)
		{
			global::Card card = entity.GetCard();
			if (card != null)
			{
				card.UpdateActorState(false);
			}
		}
		foreach (Network.Options.Option.SubOption subOption in option.Subs)
		{
			global::Entity entity2 = this.GetEntity(subOption.ID);
			if (entity2 != null)
			{
				global::Card card2 = entity2.GetCard();
				if (!(card2 == null))
				{
					card2.UpdateActorState(false);
				}
			}
		}
	}

	// Token: 0x06002BE9 RID: 11241 RVA: 0x000DB254 File Offset: 0x000D9454
	private void UpdateTargetHighlights(Network.Options.Option.SubOption subOption)
	{
		global::Entity entity = this.GetEntity(subOption.ID);
		if (entity != null)
		{
			global::Card card = entity.GetCard();
			if (card != null)
			{
				card.UpdateActorState(false);
			}
		}
		foreach (Network.Options.Option.TargetOption targetOption in subOption.Targets)
		{
			if (targetOption.PlayErrorInfo.IsValid())
			{
				int id = targetOption.ID;
				global::Entity entity2 = this.GetEntity(id);
				if (entity2 != null)
				{
					global::Card card2 = entity2.GetCard();
					if (!(card2 == null))
					{
						card2.UpdateActorState(false);
					}
				}
			}
		}
	}

	// Token: 0x06002BEA RID: 11242 RVA: 0x000DB308 File Offset: 0x000D9508
	public void DisableOptionHighlights(Network.Options options)
	{
		if (options == null || options.List == null)
		{
			return;
		}
		for (int i = 0; i < options.List.Count; i++)
		{
			Network.Options.Option option = options.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER)
			{
				global::Entity entity = this.GetEntity(option.Main.ID);
				if (entity != null)
				{
					global::Card card = entity.GetCard();
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
		}
	}

	// Token: 0x06002BEB RID: 11243 RVA: 0x000DB38C File Offset: 0x000D958C
	public bool HasValidHoverTargetForMovedMinion(global::Entity movedEntity, out PlayErrors.ErrorType mainOptionPlayError)
	{
		mainOptionPlayError = PlayErrors.ErrorType.INVALID;
		List<global::Card> moveMinionHoverTargetsInPlay = this.GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any<global::Card>())
		{
			return false;
		}
		if (this.m_options == null || this.m_options.List == null)
		{
			return false;
		}
		using (List<Network.Options.Option>.Enumerator enumerator = this.m_options.List.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Network.Options.Option option = enumerator.Current;
				if (!(moveMinionHoverTargetsInPlay.FirstOrDefault((global::Card t) => t.GetEntity().GetEntityId() == option.Main.ID) == null))
				{
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
			}
		}
		if (movedEntity.IsDormant())
		{
			mainOptionPlayError = PlayErrors.ErrorType.REQ_TARGET_NOT_DORMANT;
		}
		return false;
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x000DB4A8 File Offset: 0x000D96A8
	private void ActivateMoveMinionTargets(global::Entity movedEntity, bool suppressGlow = false)
	{
		if (movedEntity == null)
		{
			return;
		}
		this.DisableOptionHighlights(this.m_options);
		List<global::Card> moveMinionHoverTargetsInPlay = this.GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any<global::Card>())
		{
			return;
		}
		if (this.m_options == null || this.m_options.List == null)
		{
			return;
		}
		using (List<Network.Options.Option>.Enumerator enumerator = this.m_options.List.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Network.Options.Option option = enumerator.Current;
				global::Card card = moveMinionHoverTargetsInPlay.FirstOrDefault((global::Card t) => t.GetEntity().GetEntityId() == option.Main.ID);
				if (!(card == null) && card.HasCardDef)
				{
					PlayMakerFSM cardDefComponent = card.GetCardDefComponent<PlayMakerFSM>();
					if (!(cardDefComponent == null))
					{
						bool flag = option.Main.IsValidTarget(movedEntity.GetEntityId());
						cardDefComponent.Fsm.GetFsmGameObject("HoverTargetCard").Value = card.gameObject;
						cardDefComponent.Fsm.GetFsmBool("SuppressGlow").Value = (suppressGlow || !flag);
						cardDefComponent.SendEvent("Action");
						if (flag)
						{
							ManaCrystalMgr.Get().ProposeManaCrystalUsage(card.GetEntity());
						}
					}
				}
			}
		}
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x000DB5F4 File Offset: 0x000D97F4
	private void DeactivateMoveMinionTargetHighlights()
	{
		List<global::Card> moveMinionHoverTargetsInPlay = this.GetMoveMinionHoverTargetsInPlay();
		if (!moveMinionHoverTargetsInPlay.Any<global::Card>())
		{
			return;
		}
		foreach (global::Card card in moveMinionHoverTargetsInPlay)
		{
			if (card.HasCardDef)
			{
				PlayMakerFSM cardDefComponent = card.GetCardDefComponent<PlayMakerFSM>();
				if (!(cardDefComponent == null))
				{
					cardDefComponent.SendEvent("Death");
					ManaCrystalMgr.Get().CancelAllProposedMana(card.GetEntity());
				}
			}
		}
		this.UpdateOptionHighlights();
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x000DB684 File Offset: 0x000D9884
	public bool HasEnoughManaForMoveMinionHoverTarget(global::Entity heldEntity)
	{
		global::Player friendlySidePlayer = this.GetFriendlySidePlayer();
		List<global::Card> moveMinionHoverTargetsInPlay = this.GetMoveMinionHoverTargetsInPlay();
		using (List<Network.Options.Option>.Enumerator enumerator = this.m_options.List.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Network.Options.Option option = enumerator.Current;
				if (option.Main.IsValidTarget(heldEntity.GetEntityId()))
				{
					global::Card card = moveMinionHoverTargetsInPlay.FirstOrDefault((global::Card t) => t.GetEntity().GetEntityId() == option.Main.ID);
					if (!(card == null) && friendlySidePlayer.GetNumAvailableResources() >= card.GetEntity().GetCost())
					{
						return true;
					}
				}
			}
		}
		return moveMinionHoverTargetsInPlay.Count <= 0;
	}

	// Token: 0x06002BEF RID: 11247 RVA: 0x000DB74C File Offset: 0x000D994C
	private List<global::Card> GetMoveMinionHoverTargetsInPlay()
	{
		List<ZoneMoveMinionHoverTarget> list = ZoneMgr.Get().FindZonesOfType<ZoneMoveMinionHoverTarget>(global::Player.Side.FRIENDLY);
		List<global::Card> moveMinionHoverTargets = new List<global::Card>();
		list.ForEach(delegate(ZoneMoveMinionHoverTarget z)
		{
			moveMinionHoverTargets.AddRange(z.GetCards());
		});
		return moveMinionHoverTargets;
	}

	// Token: 0x06002BF0 RID: 11248 RVA: 0x000DB78C File Offset: 0x000D998C
	public Network.Options GetLastOptions()
	{
		return this.m_lastOptions;
	}

	// Token: 0x06002BF1 RID: 11249 RVA: 0x000DB794 File Offset: 0x000D9994
	public bool FriendlyHeroIsTargetable()
	{
		if (this.m_responseMode == GameState.ResponseMode.OPTION_TARGET)
		{
			Network.Options.Option option = this.m_options.List[this.m_selectedOption.m_main];
			foreach (Network.Options.Option.TargetOption targetOption in ((this.m_selectedOption.m_sub != -1) ? option.Subs[this.m_selectedOption.m_sub] : option.Main).Targets)
			{
				if (targetOption.PlayErrorInfo.IsValid())
				{
					int id = targetOption.ID;
					global::Entity entity = this.GetEntity(id);
					if (entity != null && !(entity.GetCard() == null) && entity.IsHero() && entity.IsControlledByFriendlySidePlayer())
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x000DB880 File Offset: 0x000D9A80
	private void ClearLastOptions()
	{
		this.m_lastOptions = null;
		this.m_lastSelectedOption = null;
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x000DB890 File Offset: 0x000D9A90
	private void ClearOptions()
	{
		this.m_options = null;
		this.m_selectedOption.Clear();
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x000DB8A4 File Offset: 0x000D9AA4
	private void ClearFriendlyChoices()
	{
		this.m_chosenEntities.Clear();
		int friendlyPlayerId = this.GetFriendlyPlayerId();
		this.m_choicesMap.Remove(friendlyPlayerId);
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x000DB8D0 File Offset: 0x000D9AD0
	private void OnSelectedOptionsSent()
	{
		this.ClearResponseMode();
		this.m_lastOptions = new Network.Options();
		this.m_lastOptions.CopyFrom(this.m_options);
		this.m_lastSelectedOption = new GameState.SelectedOption();
		this.m_lastSelectedOption.CopyFrom(this.m_selectedOption);
		this.ClearOptions();
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x000DB921 File Offset: 0x000D9B21
	private void OnTimeout()
	{
		if (this.m_responseMode == GameState.ResponseMode.NONE)
		{
			return;
		}
		this.ClearResponseMode();
		this.ClearLastOptions();
		this.ClearOptions();
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x000DB940 File Offset: 0x000D9B40
	private void ClearEntityMap()
	{
		global::Entity[] array = this.m_entityMap.Values.ToArray<global::Entity>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Destroy();
		}
		this.m_entityMap.Clear();
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x000DB980 File Offset: 0x000D9B80
	private void CleanGameState()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			zone.Reset();
		}
		ManaCrystalMgr.Get().Reset();
		foreach (global::Entity entity in this.m_entityMap.Values)
		{
			global::Card card = entity.GetCard();
			if (card != null)
			{
				card.DeactivatePlaySpell();
				card.CancelActiveSpells();
				card.CancelCustomSpells();
			}
		}
		foreach (global::Entity entity2 in this.m_entityMap.Values)
		{
			global::Card card2 = entity2.GetCard();
			if (card2 != null)
			{
				card2.Destroy();
			}
		}
		this.m_playerMap.Clear();
		this.m_entityMap.Clear();
		this.m_removedFromGameEntities.Clear();
		this.m_removedFromGameEntityLog.Clear();
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x000DBAC0 File Offset: 0x000D9CC0
	private void CreateGameEntity(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		this.m_gameEntity = GameMgr.Get().CreateGameEntity(powerList, createGame);
		this.m_gameEntity.m_uuid = createGame.Uuid;
		this.m_gameEntity.SetTags(createGame.Game.Tags);
		this.m_gameEntity.InitRealTimeValues(createGame.Game.Tags);
		this.AddEntity(this.m_gameEntity);
		this.m_gameEntity.OnCreate();
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x000DBB34 File Offset: 0x000D9D34
	public void OnRealTimeCreateGame(List<Network.PowerHistory> powerList, int index, Network.HistCreateGame createGame)
	{
		if (this.m_gameEntity != null)
		{
			Log.Power.PrintError("{0}.OnRealTimeCreateGame(): there is already a game entity!", new object[]
			{
				this
			});
			this.m_gameEntity.OnDecommissionGame();
			this.CleanGameState();
		}
		if (powerList.Count == 1)
		{
			string text = "Game Created without entries:";
			text += string.Format(" BuildNumber={0}", 84593);
			text += string.Format(" GameType={0}", GameMgr.Get().GetGameType());
			text += string.Format(" FormatType={0}", GameMgr.Get().GetFormatType());
			text += string.Format(" ScenarioID={0}", GameMgr.Get().GetMissionId());
			text += string.Format(" IsReconnect={0}", GameMgr.Get().IsReconnect());
			if (GameMgr.Get().IsReconnect())
			{
				text += string.Format(" ReconnectType={0}", GameMgr.Get().GetReconnectType());
			}
			Log.Power.Print(text, Array.Empty<object>());
			TelemetryManager.Client().SendLiveIssue("Gameplay_GameState", text);
		}
		this.CreateGameEntity(powerList, createGame);
		foreach (Network.HistCreateGame.PlayerData netPlayer in createGame.Players)
		{
			global::Player player = new global::Player();
			player.InitPlayer(netPlayer);
			this.AddPlayer(player);
		}
		int friendlySideTeamId = this.GetFriendlySideTeamId();
		foreach (global::Player player2 in this.m_playerMap.Values)
		{
			player2.UpdateSide(friendlySideTeamId);
		}
		foreach (Network.HistCreateGame.SharedPlayerInfo netPlayerInfo in createGame.PlayerInfos)
		{
			global::SharedPlayerInfo sharedPlayerInfo = new global::SharedPlayerInfo();
			sharedPlayerInfo.InitPlayerInfo(netPlayerInfo);
			this.AddPlayerInfo(sharedPlayerInfo);
		}
		this.m_createGamePhase = GameState.CreateGamePhase.CREATING;
		this.FireCreateGameEvent();
		if (this.m_gameEntity.HasTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD))
		{
			this.HandleWaitForOpponentReconnectPeriod(this.m_gameEntity.GetTag(GAME_TAG.WAIT_FOR_PLAYER_RECONNECT_PERIOD));
		}
		this.DebugPrintGame();
	}

	// Token: 0x06002BFB RID: 11259 RVA: 0x000DBDA8 File Offset: 0x000D9FA8
	public bool OnRealTimeFullEntity(Network.HistFullEntity fullEntity)
	{
		global::Entity entity = new global::Entity();
		entity.OnRealTimeFullEntity(fullEntity);
		this.AddEntity(entity);
		return true;
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x000DBDCC File Offset: 0x000D9FCC
	public bool OnFullEntity(Network.HistFullEntity fullEntity)
	{
		Network.Entity entity = fullEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnFullEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.OnFullEntity(fullEntity);
		return true;
	}

	// Token: 0x06002BFD RID: 11261 RVA: 0x000DBE20 File Offset: 0x000DA020
	public bool OnRealTimeShowEntity(Network.HistShowEntity showEntity)
	{
		if (this.EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeShowEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.OnRealTimeShowEntity(showEntity);
		return true;
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x000DBE88 File Offset: 0x000DA088
	public bool OnShowEntity(Network.HistShowEntity showEntity)
	{
		if (this.EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnShowEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.OnShowEntity(showEntity);
		return true;
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x000DBEF0 File Offset: 0x000DA0F0
	public bool OnEarlyConcedeShowEntity(Network.HistShowEntity showEntity)
	{
		if (this.EntityRemovedFromGame(showEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = showEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeShowEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.SetTags(entity.Tags);
		return true;
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x000DBF5C File Offset: 0x000DA15C
	public bool OnHideEntity(Network.HistHideEntity hideEntity)
	{
		if (this.EntityRemovedFromGame(hideEntity.Entity))
		{
			return false;
		}
		global::Entity entity = this.GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnHideEntity() - WARNING entity {0} DOES NOT EXIST! zone={1}", new object[]
			{
				hideEntity.Entity,
				hideEntity.Zone
			});
			return false;
		}
		entity.OnHideEntity(hideEntity);
		return true;
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x000DBFC4 File Offset: 0x000DA1C4
	public bool OnEarlyConcedeHideEntity(Network.HistHideEntity hideEntity)
	{
		if (this.EntityRemovedFromGame(hideEntity.Entity))
		{
			return false;
		}
		global::Entity entity = this.GetEntity(hideEntity.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeHideEntity() - WARNING entity {0} DOES NOT EXIST! zone={1}", new object[]
			{
				hideEntity.Entity,
				hideEntity.Zone
			});
			return false;
		}
		entity.SetTag(GAME_TAG.ZONE, hideEntity.Zone);
		return true;
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x000DC034 File Offset: 0x000DA234
	public bool OnRealTimeChangeEntity(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		if (this.EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeChangeEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.OnRealTimeChangeEntity(powerList, index, changeEntity);
		return true;
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x000DC09C File Offset: 0x000DA29C
	public bool OnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (this.EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnChangeEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.OnChangeEntity(changeEntity);
		return true;
	}

	// Token: 0x06002C04 RID: 11268 RVA: 0x000DC104 File Offset: 0x000DA304
	public bool OnEarlyConcedeChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (this.EntityRemovedFromGame(changeEntity.Entity.ID))
		{
			return false;
		}
		Network.Entity entity = changeEntity.Entity;
		global::Entity entity2 = this.GetEntity(entity.ID);
		if (entity2 == null)
		{
			Log.Power.PrintWarning("GameState.OnEarlyConcedeChangeEntity() - WARNING entity {0} DOES NOT EXIST!", new object[]
			{
				entity.ID
			});
			return false;
		}
		entity2.SetTags(entity.Tags);
		return true;
	}

	// Token: 0x06002C05 RID: 11269 RVA: 0x000DC170 File Offset: 0x000DA370
	public bool OnRealTimeTagChange(Network.HistTagChange change)
	{
		if (this.EntityRemovedFromGame(change.Entity))
		{
			return false;
		}
		global::Entity entity = GameState.Get().GetEntity(change.Entity);
		if (entity == null)
		{
			Log.Power.PrintWarning("GameState.OnRealTimeTagChange() - WARNING Entity {0} does not exist", new object[]
			{
				change.Entity
			});
			return false;
		}
		if (change.ChangeDef)
		{
			return false;
		}
		this.PreprocessRealTimeTagChange(entity, change);
		this.m_gameEntity.NotifyOfRealTimeTagChange(entity, change);
		entity.OnRealTimeTagChanged(change);
		return true;
	}

	// Token: 0x06002C06 RID: 11270 RVA: 0x000DC1F0 File Offset: 0x000DA3F0
	public bool OnTagChange(Network.HistTagChange netChange)
	{
		if (this.EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		global::Entity entity = this.GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnTagChange() - WARNING Entity {0} does not exist", new object[]
			{
				netChange.Entity
			});
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
		this.PreprocessTagChange(entity, tagDelta);
		entity.OnTagChanged(tagDelta);
		return true;
	}

	// Token: 0x06002C07 RID: 11271 RVA: 0x000DC2B0 File Offset: 0x000DA4B0
	public void OnRealTimeVoSpell(Network.HistVoSpell voSpell)
	{
		if (voSpell != null)
		{
			SoundLoader.LoadSound(new AssetReference(voSpell.SpellPrefabGUID), new PrefabCallback<GameObject>(this.OnSoundLoaded), voSpell, SoundManager.Get().GetPlaceholderSound());
		}
	}

	// Token: 0x06002C08 RID: 11272 RVA: 0x000DC2E0 File Offset: 0x000DA4E0
	public bool OnCachedTagForDormantChange(Network.HistCachedTagForDormantChange netChange)
	{
		if (this.EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		global::Entity entity = this.GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnCachedTagForDormantChange() - WARNING Entity {0} does not exist", new object[]
			{
				netChange.Entity
			});
			return false;
		}
		entity.OnCachedTagForDormantChanged(new TagDelta
		{
			tag = netChange.Tag,
			oldValue = entity.GetTag(netChange.Tag),
			newValue = netChange.Value
		});
		return true;
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x000DC368 File Offset: 0x000DA568
	public bool OnShuffleDeck(Network.HistShuffleDeck shuffleDeck)
	{
		global::Player player = this.GetPlayer(shuffleDeck.PlayerID);
		if (player == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnShuffleDeck() - WARNING Player for ID {0} does not exist", new object[]
			{
				shuffleDeck.PlayerID
			});
			return false;
		}
		if (this.EntityRemovedFromGame(player.GetEntityId()))
		{
			return false;
		}
		player.OnShuffleDeck();
		return true;
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x000DC3BC File Offset: 0x000DA5BC
	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("{0} - FAILED to load \"{1}\"", MethodBase.GetCurrentMethod().Name, assetRef));
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

	// Token: 0x06002C0B RID: 11275 RVA: 0x000DC438 File Offset: 0x000DA638
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
		float num = (float)voSpell.AdditionalDelayMs;
		float num2 = audioSource.clip.length * 1000f;
		float num3 = num;
		if (voSpell.Blocking)
		{
			num3 += num2;
		}
		if (num3 > 0f)
		{
			Gameplay.Get().StartCoroutine(this.m_powerProcessor.ArtificiallyPausePowerProcessor(num3));
			if (this.m_gameEntity is MissionEntity)
			{
				(this.m_gameEntity as MissionEntity).SetBlockVo(true, num3 / 1000f);
			}
		}
		string[] array = voSpell.SpellPrefabGUID.Split(new char[]
		{
			':'
		});
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
			global::Entity entity = this.GetEntity(voSpell.Speaker);
			Actor actor;
			if (entity == null)
			{
				actor = null;
			}
			else
			{
				global::Card card = entity.GetCard();
				actor = ((card != null) ? card.GetActor() : null);
			}
			Actor actor2 = actor;
			if (actor2 != null)
			{
				this.CharacterInPlaySpeak(voSpell, actor2, localizedTextKey, num3);
			}
		}
		else if (!string.IsNullOrEmpty(voSpell.BrassRingGUID))
		{
			this.BrassRingCharacterSpeak(voSpell, localizedTextKey, num3);
		}
		return true;
	}

	// Token: 0x06002C0C RID: 11276 RVA: 0x000DC58C File Offset: 0x000DA78C
	private void CharacterInPlaySpeak(Network.HistVoSpell voSpell, Actor speakingActor, string localizedTextKey, float totalPauseTimeMs)
	{
		if (voSpell == null || speakingActor == null || string.IsNullOrEmpty(localizedTextKey) || totalPauseTimeMs < 0f)
		{
			return;
		}
		if (voSpell.m_audioSource != null)
		{
			SoundManager.Get().PlayPreloaded(voSpell.m_audioSource);
		}
		global::Entity entity = speakingActor.GetEntity();
		Notification.SpeechBubbleDirection speechBubbleDirection;
		if (entity.IsControlledByFriendlySidePlayer())
		{
			speechBubbleDirection = Notification.SpeechBubbleDirection.BottomLeft;
		}
		else if (entity.IsMinion())
		{
			speechBubbleDirection = Notification.SpeechBubbleDirection.BottomLeft;
		}
		else
		{
			speechBubbleDirection = Notification.SpeechBubbleDirection.TopLeft;
		}
		if (totalPauseTimeMs > 0f && speechBubbleDirection != Notification.SpeechBubbleDirection.None)
		{
			NotificationManager notificationManager = NotificationManager.Get();
			bool parentToActor = !(speakingActor.GetCard() != null) || speakingActor.GetCard().GetEntity() == null || !speakingActor.GetCard().GetEntity().IsHeroPower();
			Notification notification = notificationManager.CreateSpeechBubble(GameStrings.Get(localizedTextKey), speechBubbleDirection, speakingActor, false, parentToActor, 0f);
			notificationManager.DestroyNotification(notification, totalPauseTimeMs / 1000f);
		}
	}

	// Token: 0x06002C0D RID: 11277 RVA: 0x000DC660 File Offset: 0x000DA860
	private void BrassRingCharacterSpeak(Network.HistVoSpell voSpell, string localizedTextKey, float soundLengthMs)
	{
		if (voSpell == null || string.IsNullOrEmpty(localizedTextKey) || soundLengthMs <= 0f)
		{
			return;
		}
		NotificationManager notificationManager = NotificationManager.Get();
		if (notificationManager == null)
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None;
		notificationManager.CreateBigCharacterQuoteWithGameString(voSpell.BrassRingGUID, zero, voSpell.SpellPrefabGUID, localizedTextKey, true, soundLengthMs / 1000f, null, false, bubbleDir, false, false);
	}

	// Token: 0x06002C0E RID: 11278 RVA: 0x000DC6BC File Offset: 0x000DA8BC
	public bool OnEarlyConcedeTagChange(Network.HistTagChange netChange)
	{
		if (this.EntityRemovedFromGame(netChange.Entity))
		{
			return false;
		}
		global::Entity entity = this.GetEntity(netChange.Entity);
		if (entity == null)
		{
			UnityEngine.Debug.LogWarningFormat("GameState.OnEarlyConcedeTagChange() - WARNING Entity {0} does not exist", new object[]
			{
				netChange.Entity
			});
			return false;
		}
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = netChange.Tag;
		tagDelta.oldValue = entity.GetTag(netChange.Tag);
		tagDelta.newValue = netChange.Value;
		entity.SetTag(tagDelta.tag, tagDelta.newValue);
		this.PreprocessEarlyConcedeTagChange(entity, tagDelta);
		this.ProcessEarlyConcedeTagChange(entity, tagDelta);
		return true;
	}

	// Token: 0x06002C0F RID: 11279 RVA: 0x000DC75C File Offset: 0x000DA95C
	public bool OnRealTimeResetGame(Network.HistResetGame resetGame)
	{
		if (this.m_realTimeResetGame != null)
		{
			Log.Gameplay.PrintError("{0}.OnRealTimeResetGame: There is already a ResetGame task we're waiting to execute!", new object[]
			{
				this
			});
		}
		this.m_realTimeResetGame = resetGame;
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			zone.AddInputBlocker();
		}
		return true;
	}

	// Token: 0x06002C10 RID: 11280 RVA: 0x000DC7DC File Offset: 0x000DA9DC
	public bool OnResetGame(Network.HistResetGame resetGame)
	{
		if (this.m_realTimeResetGame != resetGame)
		{
			Log.Power.PrintError("{0}.OnResetGame(): Passed ResetGame Task {0} does not match the expected ResetGame Task {1}!", new object[]
			{
				this,
				resetGame,
				this.m_realTimeResetGame
			});
		}
		if (this.m_gameEntity != null)
		{
			this.m_gameEntity.OnDecommissionGame();
			this.CleanGameState();
		}
		List<Network.PowerHistory> list = new List<Network.PowerHistory>();
		foreach (PowerTask powerTask in this.m_powerProcessor.GetCurrentTaskList().GetTaskList())
		{
			list.Add(powerTask.GetPower());
		}
		this.CreateGameEntity(list, resetGame.CreateGame);
		foreach (Network.HistCreateGame.PlayerData netPlayer in resetGame.CreateGame.Players)
		{
			global::Player player = new global::Player();
			player.InitPlayer(netPlayer);
			this.AddPlayer(player);
		}
		int friendlySideTeamId = this.GetFriendlySideTeamId();
		foreach (global::Player player2 in this.m_playerMap.Values)
		{
			player2.UpdateSide(friendlySideTeamId);
			player2.OnBoardLoaded();
		}
		this.m_realTimeResetGame = null;
		this.m_powerProcessor.FlushDelayedRealTimeTasks();
		return true;
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x000DC95C File Offset: 0x000DAB5C
	public bool OnMetaData(Network.HistMetaData metaData)
	{
		this.m_powerProcessor.OnMetaData(metaData);
		foreach (int num in metaData.Info)
		{
			global::Entity entity = this.GetEntity(num);
			if (entity == null)
			{
				if (!this.EntityRemovedFromGame(num))
				{
					UnityEngine.Debug.LogWarning(string.Format("GameState.OnMetaData() - WARNING Entity {0} does not exist", num));
				}
				return false;
			}
			entity.OnMetaData(metaData);
		}
		return true;
	}

	// Token: 0x06002C12 RID: 11282 RVA: 0x000DC9EC File Offset: 0x000DABEC
	public void OnTaskListEnded(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return;
		}
		using (List<PowerTask>.Enumerator enumerator = taskList.GetTaskList().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetPower().Type == Network.PowerType.CREATE_GAME)
				{
					this.m_createGamePhase = GameState.CreateGamePhase.CREATED;
					this.FireCreateGameEvent();
					this.m_createGameListeners.Clear();
				}
			}
		}
		this.RemoveQueuedEntitiesFromGame();
	}

	// Token: 0x06002C13 RID: 11283 RVA: 0x000DCA68 File Offset: 0x000DAC68
	public void OnPowerHistory(List<Network.PowerHistory> powerList)
	{
		this.DebugPrintPowerList(powerList);
		int num = this.m_powerProcessor.HasEarlyConcedeTaskList() ? 1 : 0;
		this.m_powerProcessor.OnPowerHistory(powerList);
		this.ProcessAllQueuedChoices();
		bool flag = this.m_powerProcessor.HasEarlyConcedeTaskList();
		if (num == 0 && flag)
		{
			this.OnReceivedEarlyConcede();
		}
	}

	// Token: 0x06002C14 RID: 11284 RVA: 0x000DCAB2 File Offset: 0x000DACB2
	private void OnReceivedEarlyConcede()
	{
		this.ClearResponseMode();
		this.ClearLastOptions();
		this.ClearOptions();
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x000DCAC8 File Offset: 0x000DACC8
	public void OnAllOptions(Network.Options options)
	{
		this.m_responseMode = GameState.ResponseMode.OPTION;
		this.m_chosenEntities.Clear();
		if (this.m_options != null && (this.m_lastOptions == null || this.m_lastOptions.ID < this.m_options.ID))
		{
			this.m_lastOptions = new Network.Options();
			this.m_lastOptions.CopyFrom(this.m_options);
		}
		this.m_options = options;
		foreach (Network.Options.Option option in this.m_options.List)
		{
			if (option.Type == Network.Options.Option.OptionType.POWER)
			{
				global::Entity entity = this.GetEntity(option.Main.ID);
				if (entity != null && option.Main.Targets != null && option.Main.Targets.Count > 0)
				{
					entity.UpdateUseBattlecryFlag(true);
				}
			}
		}
		this.DebugPrintOptions(Log.Power);
		this.EnterMainOptionMode();
		this.FireOptionsReceivedEvent();
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x000DCBD4 File Offset: 0x000DADD4
	public void OnEntityChoices(Network.EntityChoices choices)
	{
		PowerTaskList lastTaskList = this.m_powerProcessor.GetLastTaskList();
		if (!this.CanProcessEntityChoices(choices))
		{
			Log.Power.Print("GameState.OnEntityChoices() - id={0} playerId={1} queued", new object[]
			{
				choices.ID,
				choices.PlayerId
			});
			GameState.QueuedChoice item = new GameState.QueuedChoice
			{
				m_type = GameState.QueuedChoice.PacketType.ENTITY_CHOICES,
				m_packet = choices,
				m_eventData = lastTaskList
			};
			this.m_queuedChoices.Enqueue(item);
			return;
		}
		this.ProcessEntityChoices(choices, lastTaskList);
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x000DCC58 File Offset: 0x000DAE58
	public void OnEntitiesChosen(Network.EntitiesChosen chosen)
	{
		if (!this.CanProcessEntitiesChosen(chosen))
		{
			Log.Power.Print("GameState.OnEntitiesChosen() - id={0} playerId={1} queued", new object[]
			{
				chosen.ID,
				chosen.PlayerId
			});
			GameState.QueuedChoice item = new GameState.QueuedChoice
			{
				m_type = GameState.QueuedChoice.PacketType.ENTITIES_CHOSEN,
				m_packet = chosen
			};
			this.m_queuedChoices.Enqueue(item);
			return;
		}
		this.ProcessEntitiesChosen(chosen);
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x000DCCC7 File Offset: 0x000DAEC7
	public float GetClientLostTimeCatchUpThreshold()
	{
		return this.m_clientLostTimeCatchUpThreshold;
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x000DCCCF File Offset: 0x000DAECF
	public bool ShouldUseSlushTimeTracker()
	{
		return this.m_useSlushTimeCatchUp;
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x000DCCD7 File Offset: 0x000DAED7
	public bool ShoudRestrictLostTimeCatchUpToLowEndDevices()
	{
		return this.m_restrictClientLostTimeCatchUpToLowEndDevices;
	}

	// Token: 0x06002C1B RID: 11291 RVA: 0x000DCCE0 File Offset: 0x000DAEE0
	public void UpdateGameGuardianVars(GameGuardianVars gameGuardianVars)
	{
		this.m_clientLostTimeCatchUpThreshold = (gameGuardianVars.HasClientLostFrameTimeCatchUpThreshold ? gameGuardianVars.ClientLostFrameTimeCatchUpThreshold : 0f);
		this.m_useSlushTimeCatchUp = (gameGuardianVars.HasClientLostFrameTimeCatchUpUseSlush && gameGuardianVars.ClientLostFrameTimeCatchUpUseSlush);
		this.m_restrictClientLostTimeCatchUpToLowEndDevices = (gameGuardianVars.HasClientLostFrameTimeCatchUpLowEndOnly && gameGuardianVars.ClientLostFrameTimeCatchUpLowEndOnly);
		this.m_allowDeferredPowers = (!gameGuardianVars.HasGameAllowDeferredPowers || gameGuardianVars.GameAllowDeferredPowers);
		this.m_allowBatchedPowers = (!gameGuardianVars.HasGameAllowBatchedPowers || gameGuardianVars.GameAllowBatchedPowers);
		this.m_allowDiamondCards = (!gameGuardianVars.HasGameAllowDiamondCards || gameGuardianVars.GameAllowDiamondCards);
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x000DCD7C File Offset: 0x000DAF7C
	public void UpdateBattlegroundInfo(UpdateBattlegroundInfo battlegroundMinionPoolDenyList)
	{
		this.m_battlegroundMinionPool = (battlegroundMinionPoolDenyList.HasBattlegroundMinionPool ? battlegroundMinionPoolDenyList.BattlegroundMinionPool : "Battleground minion pool not available");
		if (this.m_printBattlegroundMinionPoolOnUpdate)
		{
			Log.All.Print(this.m_battlegroundMinionPool, Array.Empty<object>());
			this.m_printBattlegroundMinionPoolOnUpdate = false;
		}
		this.m_battlegroundDenyList = (battlegroundMinionPoolDenyList.HasBattlegroundDenyList ? battlegroundMinionPoolDenyList.BattlegroundDenyList : "Battle ground deny list not available");
		if (this.m_printBattlegroundDenyListOnUpdate)
		{
			Log.All.Print(this.m_battlegroundDenyList, Array.Empty<object>());
			this.m_printBattlegroundDenyListOnUpdate = false;
		}
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000DCE08 File Offset: 0x000DB008
	private bool CanProcessEntityChoices(Network.EntityChoices choices)
	{
		int playerId = choices.PlayerId;
		if (!this.m_playerMap.ContainsKey(playerId))
		{
			return false;
		}
		foreach (int key in choices.Entities)
		{
			if (!this.m_entityMap.ContainsKey(key))
			{
				return false;
			}
		}
		return !this.m_choicesMap.ContainsKey(playerId);
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x000DCE90 File Offset: 0x000DB090
	private bool CanProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		if (!this.m_playerMap.ContainsKey(playerId))
		{
			return false;
		}
		foreach (int key in chosen.Entities)
		{
			if (!this.m_entityMap.ContainsKey(key))
			{
				return false;
			}
		}
		Network.EntityChoices entityChoices;
		return !this.m_choicesMap.TryGetValue(playerId, out entityChoices) || entityChoices.ID == chosen.ID;
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000DCF2C File Offset: 0x000DB12C
	private void ProcessAllQueuedChoices()
	{
		while (this.m_queuedChoices.Count > 0)
		{
			GameState.QueuedChoice queuedChoice = this.m_queuedChoices.Peek();
			GameState.QueuedChoice.PacketType type = queuedChoice.m_type;
			if (type != GameState.QueuedChoice.PacketType.ENTITY_CHOICES)
			{
				if (type == GameState.QueuedChoice.PacketType.ENTITIES_CHOSEN)
				{
					Network.EntitiesChosen chosen = (Network.EntitiesChosen)queuedChoice.m_packet;
					if (!this.CanProcessEntitiesChosen(chosen))
					{
						return;
					}
					this.m_queuedChoices.Dequeue();
					this.ProcessEntitiesChosen(chosen);
				}
			}
			else
			{
				Network.EntityChoices choices = (Network.EntityChoices)queuedChoice.m_packet;
				if (!this.CanProcessEntityChoices(choices))
				{
					return;
				}
				this.m_queuedChoices.Dequeue();
				PowerTaskList preChoiceTaskList = (PowerTaskList)queuedChoice.m_eventData;
				this.ProcessEntityChoices(choices, preChoiceTaskList);
			}
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000DCFD0 File Offset: 0x000DB1D0
	private void ProcessEntityChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		this.DebugPrintEntityChoices(choices, preChoiceTaskList);
		if (this.m_powerProcessor.HasEarlyConcedeTaskList())
		{
			return;
		}
		int playerId = choices.PlayerId;
		this.m_choicesMap[playerId] = choices;
		int friendlyPlayerId = this.GetFriendlyPlayerId();
		if (playerId == friendlyPlayerId)
		{
			this.m_responseMode = GameState.ResponseMode.CHOICE;
			this.m_chosenEntities.Clear();
			this.EnterChoiceMode();
		}
		this.FireEntityChoicesReceivedEvent(choices, preChoiceTaskList);
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x000DD032 File Offset: 0x000DB232
	private void ProcessEntitiesChosen(Network.EntitiesChosen chosen)
	{
		this.DebugPrintEntitiesChosen(chosen);
		if (this.m_powerProcessor.HasEarlyConcedeTaskList())
		{
			return;
		}
		if (this.FireEntitiesChosenReceivedEvent(chosen))
		{
			return;
		}
		this.OnEntitiesChosenProcessed(chosen);
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x000DD05A File Offset: 0x000DB25A
	public void OnGameSetup(Network.GameSetup setup)
	{
		this.m_maxSecretZoneSizePerPlayer = setup.MaxSecretZoneSizePerPlayer;
		this.m_maxSecretsPerPlayer = setup.MaxSecretsPerPlayer;
		this.m_maxQuestsPerPlayer = setup.MaxQuestsPerPlayer;
		this.m_maxFriendlyMinionsPerPlayer = setup.MaxFriendlyMinionsPerPlayer;
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x000DD08C File Offset: 0x000DB28C
	public void QueueEntityForRemoval(global::Entity entity)
	{
		this.m_removedFromGameEntities.Enqueue(entity);
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x000DD09C File Offset: 0x000DB29C
	public void OnOptionRejected(int optionId)
	{
		if (this.m_lastSelectedOption == null)
		{
			UnityEngine.Debug.LogError("GameState.OnOptionRejected() - got an option rejection without a last selected option");
			return;
		}
		if (this.m_lastOptions.ID != optionId)
		{
			UnityEngine.Debug.LogErrorFormat("GameState.OnOptionRejected() - rejected option id ({0}) does not match last option id ({1})", new object[]
			{
				optionId,
				this.m_lastOptions.ID
			});
			return;
		}
		Network.Options.Option option = this.m_lastOptions.List[this.m_lastSelectedOption.m_main];
		this.FireOptionRejectedEvent(option);
		this.ClearLastOptions();
	}

	// Token: 0x06002C25 RID: 11301 RVA: 0x000DD120 File Offset: 0x000DB320
	public void OnTurnTimerUpdate(Network.TurnTimerInfo info)
	{
		TurnTimerUpdate turnTimerUpdate = new TurnTimerUpdate();
		turnTimerUpdate.SetSecondsRemaining(info.Seconds);
		turnTimerUpdate.SetEndTimestamp(Time.realtimeSinceStartup + info.Seconds);
		turnTimerUpdate.SetShow(info.Show);
		if (this.IsMulliganManagerActive() && this.m_gameEntity != null && this.GetBooleanGameOption(GameEntityOption.ALWAYS_SHOW_MULLIGAN_TIMER))
		{
			turnTimerUpdate.SetShow(true);
		}
		int turn = this.GetTurn();
		if (info.Turn > turn)
		{
			this.m_turnTimerUpdates[info.Turn] = turnTimerUpdate;
			return;
		}
		this.TriggerTurnTimerUpdate(turnTimerUpdate);
	}

	// Token: 0x06002C26 RID: 11302 RVA: 0x000DD1A7 File Offset: 0x000DB3A7
	public void TriggerTurnTimerUpdateForTurn(int turn)
	{
		this.OnTurnChanged_TurnTimer(this.GetTurn(), turn);
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x000DD1B6 File Offset: 0x000DB3B6
	public void OnSpectatorNotifyEvent(SpectatorNotify notify)
	{
		this.FireSpectatorNotifyEvent(notify);
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x000DD1C0 File Offset: 0x000DB3C0
	public void SendChoices()
	{
		if (this.m_responseMode != GameState.ResponseMode.CHOICE)
		{
			return;
		}
		Network.EntityChoices friendlyEntityChoices = this.GetFriendlyEntityChoices();
		if (friendlyEntityChoices == null)
		{
			return;
		}
		if (this.m_chosenEntities.Count < friendlyEntityChoices.CountMin || this.m_chosenEntities.Count > friendlyEntityChoices.CountMax)
		{
			return;
		}
		ChoiceCardMgr.Get().OnSendChoices(friendlyEntityChoices, this.m_chosenEntities);
		Log.Power.Print("GameState.SendChoices() - id={0} ChoiceType={1}", new object[]
		{
			friendlyEntityChoices.ID,
			friendlyEntityChoices.ChoiceType
		});
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_chosenEntities.Count; i++)
		{
			global::Entity entity = this.m_chosenEntities[i];
			int entityId = entity.GetEntityId();
			Log.Power.Print("GameState.SendChoices() -   m_chosenEntities[{0}]={1}", new object[]
			{
				i,
				entity
			});
			list.Add(entityId);
		}
		if (!GameMgr.Get().IsSpectator())
		{
			Network.Get().SendChoices(friendlyEntityChoices.ID, list);
		}
		this.ClearResponseMode();
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x000DD2CC File Offset: 0x000DB4CC
	public void OnEntitiesChosenProcessed(Network.EntitiesChosen chosen)
	{
		int playerId = chosen.PlayerId;
		int friendlyPlayerId = this.GetFriendlyPlayerId();
		if (playerId == friendlyPlayerId)
		{
			if (this.m_responseMode == GameState.ResponseMode.CHOICE)
			{
				this.ClearResponseMode();
			}
			this.ClearFriendlyChoices();
		}
		else
		{
			this.m_choicesMap.Remove(playerId);
		}
		this.ProcessAllQueuedChoices();
	}

	// Token: 0x06002C2A RID: 11306 RVA: 0x000DD318 File Offset: 0x000DB518
	public void SendOption()
	{
		if (!GameMgr.Get().IsSpectator())
		{
			Network.Get().SendOption(this.m_options.ID, this.m_selectedOption.m_main, this.m_selectedOption.m_target, this.m_selectedOption.m_sub, this.m_selectedOption.m_position);
			Log.Power.Print("GameState.SendOption() - selectedOption={0} selectedSubOption={1} selectedTarget={2} selectedPosition={3}", new object[]
			{
				this.m_selectedOption.m_main,
				this.m_selectedOption.m_sub,
				this.m_selectedOption.m_target,
				this.m_selectedOption.m_position
			});
		}
		this.OnSelectedOptionsSent();
		Network.Options.Option option = this.m_lastOptions.List[this.m_lastSelectedOption.m_main];
		this.FireOptionsSentEvent(option);
	}

	// Token: 0x06002C2B RID: 11307 RVA: 0x000DD400 File Offset: 0x000DB600
	private void OnTurnChanged_TurnTimer(int oldTurn, int newTurn)
	{
		if (this.m_turnTimerUpdates.Count == 0)
		{
			return;
		}
		TurnTimerUpdate turnTimerUpdate;
		if (!this.m_turnTimerUpdates.TryGetValue(newTurn, out turnTimerUpdate))
		{
			return;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float endTimestamp = turnTimerUpdate.GetEndTimestamp();
		float secondsRemaining = Mathf.Max(0f, endTimestamp - realtimeSinceStartup);
		turnTimerUpdate.SetSecondsRemaining(secondsRemaining);
		this.TriggerTurnTimerUpdate(turnTimerUpdate);
		this.m_turnTimerUpdates.Remove(newTurn);
	}

	// Token: 0x06002C2C RID: 11308 RVA: 0x000DD462 File Offset: 0x000DB662
	private void TriggerTurnTimerUpdate(TurnTimerUpdate update)
	{
		this.FireTurnTimerUpdateEvent(update);
		if (update.GetSecondsRemaining() > Mathf.Epsilon)
		{
			return;
		}
		this.OnTimeout();
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x000DD480 File Offset: 0x000DB680
	private void DebugPrintGame()
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		Log.Power.Print(string.Format("GameState.DebugPrintGame() - BuildNumber={0}", 84593), Array.Empty<object>());
		Log.Power.Print(string.Format("GameState.DebugPrintGame() - GameType={0}", GameMgr.Get().GetGameType()), Array.Empty<object>());
		Log.Power.Print(string.Format("GameState.DebugPrintGame() - FormatType={0}", GameMgr.Get().GetFormatType()), Array.Empty<object>());
		Log.Power.Print(string.Format("GameState.DebugPrintGame() - ScenarioID={0}", GameMgr.Get().GetMissionId()), Array.Empty<object>());
		foreach (global::Player player in this.m_playerMap.Values)
		{
			Log.Power.Print(string.Format("GameState.DebugPrintGame() - PlayerID={0}, PlayerName={1}", player.GetPlayerId(), this.GetEntityLogName(player.GetEntityId())), Array.Empty<object>());
		}
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x000DD5AC File Offset: 0x000DB7AC
	private void DebugPrintPowerList(List<Network.PowerHistory> powerList)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		string text = "";
		Log.Power.Print(string.Format("GameState.DebugPrintPowerList() - Count={0}", powerList.Count), Array.Empty<object>());
		for (int i = 0; i < powerList.Count; i++)
		{
			Network.PowerHistory power = powerList[i];
			this.DebugPrintPower(Log.Power, "GameState", power, ref text);
		}
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x000DD61C File Offset: 0x000DB81C
	public void DebugPrintPower(global::Logger logger, string callerName, Network.PowerHistory power)
	{
		string empty = string.Empty;
		this.DebugPrintPower(logger, callerName, power, ref empty);
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x000DD63C File Offset: 0x000DB83C
	public void DebugPrintPower(global::Logger logger, string callerName, Network.PowerHistory power, ref string indentation)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.Entity entity = ((Network.HistFullEntity)power).Entity;
			global::Entity entity2 = this.GetEntity(entity.ID);
			if (entity2 == null)
			{
				logger.Print("{0}.DebugPrintPower() - {1}FULL_ENTITY - Creating ID={2} CardID={3}", new object[]
				{
					callerName,
					indentation,
					entity.ID,
					entity.CardID
				});
			}
			else
			{
				logger.Print("{0}.DebugPrintPower() - {1}FULL_ENTITY - Updating {2} CardID={3}", new object[]
				{
					callerName,
					indentation,
					entity2,
					entity.CardID
				});
			}
			this.DebugPrintTags(logger, callerName, indentation, entity);
			return;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.Entity entity3 = ((Network.HistShowEntity)power).Entity;
			logger.Print("{0}.DebugPrintPower() - {1}SHOW_ENTITY - Updating Entity={2} CardID={3}", new object[]
			{
				callerName,
				indentation,
				this.GetEntityLogName(entity3.ID),
				entity3.CardID
			});
			this.DebugPrintTags(logger, callerName, indentation, entity3);
			return;
		}
		case Network.PowerType.HIDE_ENTITY:
		{
			Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power;
			logger.Print("{0}.DebugPrintPower() - {1}HIDE_ENTITY - Entity={2} {3}", new object[]
			{
				callerName,
				indentation,
				this.GetEntityLogName(histHideEntity.Entity),
				Tags.DebugTag(49, histHideEntity.Zone)
			});
			return;
		}
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			logger.Print("{0}.DebugPrintPower() - {1}TAG_CHANGE Entity={2} {3} {4}", new object[]
			{
				callerName,
				indentation,
				this.GetEntityLogName(histTagChange.Entity),
				Tags.DebugTag(histTagChange.Tag, histTagChange.Value),
				histTagChange.ChangeDef ? "DEF CHANGE" : ""
			});
			return;
		}
		case Network.PowerType.BLOCK_START:
		{
			Network.HistBlockStart histBlockStart = (Network.HistBlockStart)power;
			string text = string.Empty;
			if (histBlockStart.BlockType == HistoryBlock.Type.TRIGGER)
			{
				string arg = ((GAME_TAG)histBlockStart.TriggerKeyword).ToString();
				text = string.Format("TriggerKeyword={0}", arg);
			}
			logger.Print("{0}.DebugPrintPower() - {1}BLOCK_START BlockType={2} Entity={3} EffectCardId={4} EffectIndex={5} Target={6} SubOption={7} {8}", new object[]
			{
				callerName,
				indentation,
				histBlockStart.BlockType,
				this.GetEntitiesLogNames(histBlockStart.Entities),
				histBlockStart.EffectCardId,
				histBlockStart.EffectIndex,
				this.GetEntityLogName(histBlockStart.Target),
				histBlockStart.SubOption,
				text
			});
			indentation += "    ";
			return;
		}
		case Network.PowerType.BLOCK_END:
			if (indentation.Length >= "    ".Length)
			{
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			logger.Print("{0}.DebugPrintPower() - {1}BLOCK_END", new object[]
			{
				callerName,
				indentation
			});
			return;
		case Network.PowerType.CREATE_GAME:
		{
			Network.HistCreateGame histCreateGame = (Network.HistCreateGame)power;
			logger.Print("{0}.DebugPrintPower() - {1}CREATE_GAME", new object[]
			{
				callerName,
				indentation
			});
			indentation += "    ";
			logger.Print("{0}.DebugPrintPower() - {1}GameEntity EntityID={2}", new object[]
			{
				callerName,
				indentation,
				histCreateGame.Game.ID
			});
			this.DebugPrintTags(logger, callerName, indentation, histCreateGame.Game);
			foreach (Network.HistCreateGame.PlayerData playerData in histCreateGame.Players)
			{
				logger.Print("{0}.DebugPrintPower() - {1}Player EntityID={2} PlayerID={3} GameAccountId={4}", new object[]
				{
					callerName,
					indentation,
					playerData.Player.ID,
					playerData.ID,
					playerData.GameAccountId
				});
				this.DebugPrintTags(logger, callerName, indentation, playerData.Player);
			}
			indentation = indentation.Remove(indentation.Length - "    ".Length);
			return;
		}
		case Network.PowerType.META_DATA:
		{
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			string text2 = histMetaData.Data.ToString();
			HistoryMeta.Type metaType = histMetaData.MetaType;
			if (metaType == HistoryMeta.Type.JOUST)
			{
				text2 = this.GetEntityLogName(histMetaData.Data);
			}
			logger.Print("{0}.DebugPrintPower() - {1}META_DATA - Meta={2} Data={3} InfoCount={4}", new object[]
			{
				callerName,
				indentation,
				histMetaData.MetaType,
				text2,
				histMetaData.Info.Count
			});
			if (histMetaData.Info.Count > 0 && logger.IsVerbose())
			{
				indentation += "    ";
				for (int i = 0; i < histMetaData.Info.Count; i++)
				{
					int id = histMetaData.Info[i];
					logger.Print(true, "{0}.DebugPrintPower() - {1}        Info[{2}] = {3}", new object[]
					{
						callerName,
						indentation,
						i,
						this.GetEntityLogName(id)
					});
				}
				indentation = indentation.Remove(indentation.Length - "    ".Length);
				return;
			}
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.Entity entity4 = ((Network.HistChangeEntity)power).Entity;
			logger.Print("{0}.DebugPrintPower() - {1}CHANGE_ENTITY - Updating Entity={2} CardID={3}", new object[]
			{
				callerName,
				indentation,
				this.GetEntityLogName(entity4.ID),
				entity4.CardID
			});
			this.DebugPrintTags(logger, callerName, indentation, entity4);
			return;
		}
		case Network.PowerType.RESET_GAME:
			logger.Print("{0}.DebugPrintPower() - {1}RESET_GAME", new object[]
			{
				callerName,
				indentation
			});
			return;
		case Network.PowerType.SUB_SPELL_START:
		{
			Network.HistSubSpellStart histSubSpellStart = power as Network.HistSubSpellStart;
			logger.Print("{0}.DebugPrintPower() - {1}SUB_SPELL_START - SpellPrefabGUID={2} Source={3} TargetCount={4}", new object[]
			{
				callerName,
				indentation,
				histSubSpellStart.SpellPrefabGUID,
				histSubSpellStart.SourceEntityID,
				histSubSpellStart.TargetEntityIDS.Count
			});
			if (logger.IsVerbose())
			{
				if (histSubSpellStart.SourceEntityID != 0)
				{
					logger.Print(true, "{0}.DebugPrintPower() - {1}                  Source = {2}", new object[]
					{
						callerName,
						indentation,
						this.GetEntityLogName(histSubSpellStart.SourceEntityID)
					});
				}
				for (int j = 0; j < histSubSpellStart.TargetEntityIDS.Count; j++)
				{
					int id2 = histSubSpellStart.TargetEntityIDS[j];
					logger.Print(true, "{0}.DebugPrintPower() - {1}                  Targets[{2}] = {3}", new object[]
					{
						callerName,
						indentation,
						j,
						this.GetEntityLogName(id2)
					});
				}
			}
			indentation += "    ";
			return;
		}
		case Network.PowerType.SUB_SPELL_END:
			if (indentation.Length >= "    ".Length)
			{
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			logger.Print("{0}.DebugPrintPower() - {1}SUB_SPELL_END", new object[]
			{
				callerName,
				indentation
			});
			return;
		case Network.PowerType.VO_SPELL:
		{
			Network.HistVoSpell histVoSpell = power as Network.HistVoSpell;
			logger.Print("{0}.DebugPrintPower() - {1}VO_SPELL - BrassRingGuid={2} - VoSpellPrefabGUID={3} - Blocking={4} - AdditionalDelayInMs={5}", new object[]
			{
				callerName,
				indentation,
				histVoSpell.SpellPrefabGUID,
				histVoSpell.BrassRingGUID,
				histVoSpell.Blocking,
				histVoSpell.AdditionalDelayMs
			});
			return;
		}
		case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
		{
			Network.HistCachedTagForDormantChange histCachedTagForDormantChange = (Network.HistCachedTagForDormantChange)power;
			logger.Print("{0}.DebugPrintPower() - {1}CACHED_TAG_FOR_DORMANT_CHANGE Entity={2} {3}", new object[]
			{
				callerName,
				indentation,
				this.GetEntityLogName(histCachedTagForDormantChange.Entity),
				Tags.DebugTag(histCachedTagForDormantChange.Tag, histCachedTagForDormantChange.Value)
			});
			return;
		}
		case Network.PowerType.SHUFFLE_DECK:
		{
			Network.HistShuffleDeck histShuffleDeck = (Network.HistShuffleDeck)power;
			logger.Print("{0}.DebugPrintPower() - {1}SHUFFLE_DECK PlayerID={2}", new object[]
			{
				callerName,
				indentation,
				histShuffleDeck.PlayerID
			});
			return;
		}
		default:
			logger.Print("{0}.DebugPrintPower() - ERROR: unhandled PowType {1}", new object[]
			{
				callerName,
				power.Type
			});
			break;
		}
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x000DDE34 File Offset: 0x000DC034
	private void DebugPrintTags(global::Logger logger, string callerName, string indentation, Network.Entity netEntity)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		if (indentation != null)
		{
			indentation += "    ";
		}
		for (int i = 0; i < netEntity.Tags.Count; i++)
		{
			Network.Entity.Tag tag = netEntity.Tags[i];
			logger.Print("{0}.DebugPrintPower() - {1}{2}", new object[]
			{
				callerName,
				indentation,
				Tags.DebugTag(tag.Name, tag.Value)
			});
		}
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x000DDEB0 File Offset: 0x000DC0B0
	private void DebugPrintOptions(global::Logger logger)
	{
		if (!logger.CanPrint())
		{
			return;
		}
		logger.Print("GameState.DebugPrintOptions() - id={0}", new object[]
		{
			this.m_options.ID
		});
		for (int i = 0; i < this.m_options.List.Count; i++)
		{
			Network.Options.Option option = this.m_options.List[i];
			global::Entity entity = this.GetEntity(option.Main.ID);
			logger.Print("GameState.DebugPrintOptions() -   option {0} type={1} mainEntity={2} error={3} errorParam={4}", new object[]
			{
				i,
				option.Type,
				entity,
				option.Main.PlayErrorInfo.PlayError,
				option.Main.PlayErrorInfo.PlayErrorParam
			});
			if (option.Main.Targets != null)
			{
				for (int j = 0; j < option.Main.Targets.Count; j++)
				{
					Network.Options.Option.TargetOption targetOption = option.Main.Targets[j];
					global::Entity entity2 = this.GetEntity(targetOption.ID);
					logger.Print("GameState.DebugPrintOptions() -     target {0} entity={1} error={2} errorParam={3}", new object[]
					{
						j,
						entity2,
						targetOption.PlayErrorInfo.PlayError,
						targetOption.PlayErrorInfo.PlayErrorParam
					});
				}
			}
			for (int k = 0; k < option.Subs.Count; k++)
			{
				Network.Options.Option.SubOption subOption = option.Subs[k];
				global::Entity entity3 = this.GetEntity(subOption.ID);
				logger.Print("GameState.DebugPrintOptions() -     subOption {0} entity={1} error={2} errorParam={3}", new object[]
				{
					k,
					entity3,
					subOption.PlayErrorInfo.PlayError,
					subOption.PlayErrorInfo.PlayErrorParam
				});
				if (subOption.Targets != null)
				{
					for (int l = 0; l < subOption.Targets.Count; l++)
					{
						Network.Options.Option.TargetOption targetOption2 = subOption.Targets[l];
						global::Entity entity4 = this.GetEntity(targetOption2.ID);
						logger.Print("GameState.DebugPrintOptions() -       target {0} entity={1} error={2} errorParam={3}", new object[]
						{
							l,
							entity4,
							targetOption2.PlayErrorInfo.PlayError,
							targetOption2.PlayErrorInfo.PlayErrorParam
						});
					}
				}
			}
		}
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x000DE134 File Offset: 0x000DC334
	private void DebugPrintEntityChoices(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		global::Player player = this.GetPlayer(choices.PlayerId);
		object obj = null;
		if (preChoiceTaskList != null)
		{
			obj = preChoiceTaskList.GetId();
		}
		Log.Power.Print("GameState.DebugPrintEntityChoices() - id={0} Player={1} TaskList={2} ChoiceType={3} CountMin={4} CountMax={5}", new object[]
		{
			choices.ID,
			this.GetEntityLogName(player.GetEntityId()),
			obj,
			choices.ChoiceType,
			choices.CountMin,
			choices.CountMax
		});
		Log.Power.Print("GameState.DebugPrintEntityChoices() -   Source={0}", new object[]
		{
			this.GetEntityLogName(choices.Source)
		});
		for (int i = 0; i < choices.Entities.Count; i++)
		{
			Log.Power.Print("GameState.DebugPrintEntityChoices() -   Entities[{0}]={1}", new object[]
			{
				i,
				this.GetEntityLogName(choices.Entities[i])
			});
		}
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x000DE23C File Offset: 0x000DC43C
	private void DebugPrintEntitiesChosen(Network.EntitiesChosen chosen)
	{
		if (!Log.Power.CanPrint())
		{
			return;
		}
		global::Player player = this.GetPlayer(chosen.PlayerId);
		Log.Power.Print("GameState.DebugPrintEntitiesChosen() - id={0} Player={1} EntitiesCount={2}", new object[]
		{
			chosen.ID,
			this.GetEntityLogName(player.GetEntityId()),
			chosen.Entities.Count
		});
		for (int i = 0; i < chosen.Entities.Count; i++)
		{
			Log.Power.Print("GameState.DebugPrintEntitiesChosen() -   Entities[{0}]={1}", new object[]
			{
				i,
				this.GetEntityLogName(chosen.Entities[i])
			});
		}
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
	private string GetEntityLogName(int id)
	{
		global::Entity entity = this.GetEntity(id);
		if (entity == null)
		{
			return id.ToString();
		}
		if (entity.IsPlayer())
		{
			BnetPlayer bnetPlayer = (entity as global::Player).GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetBattleTag() != null)
			{
				return string.Format("{0}#{1}", bnetPlayer.GetBattleTag().GetName(), bnetPlayer.GetBattleTag().GetNumber());
			}
		}
		return entity.ToString();
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x000DE368 File Offset: 0x000DC568
	private string GetEntitiesLogNames(List<int> ids)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (int id in ids)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append(this.GetEntityLogName(id));
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06002C37 RID: 11319 RVA: 0x000DE3E0 File Offset: 0x000DC5E0
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
			builder.AppendFormat("Entities={0}", this.GetEntitiesLogNames(blockStart.Entities));
			builder.Append(' ');
			builder.AppendFormat("Target={0}", this.GetEntityLogName(blockStart.Target));
		}
		builder.Append(']');
		builder.AppendFormat(" Tasks={0}", taskList.GetTaskList().Count);
	}

	// Token: 0x06002C38 RID: 11320 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void QuickGameFlipHeroesCheat(List<Network.PowerHistory> powerList)
	{
	}

	// Token: 0x04001808 RID: 6152
	public const int DEFAULT_SUBOPTION = -1;

	// Token: 0x04001809 RID: 6153
	public const int RACE_COUNT_IN_BATTLEGROUNDS_EXCLUDING_AMALGAM = 5;

	// Token: 0x0400180A RID: 6154
	public const int RACE_COUNT_MISSING_BATTLEGROUNDS = 3;

	// Token: 0x0400180B RID: 6155
	public const int TOTAL_RACES = 8;

	// Token: 0x0400180C RID: 6156
	private const string INDENT = "    ";

	// Token: 0x0400180D RID: 6157
	private const float BLOCK_REPORT_START_SEC = 10f;

	// Token: 0x0400180E RID: 6158
	private const float BLOCK_REPORT_INTERVAL_SEC = 3f;

	// Token: 0x0400180F RID: 6159
	private static GameState s_instance;

	// Token: 0x04001810 RID: 6160
	private static List<GameState.GameStateInitializedListener> s_gameStateInitializedListeners;

	// Token: 0x04001811 RID: 6161
	private readonly TAG_RACE[] m_allRaces = new TAG_RACE[]
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

	// Token: 0x04001812 RID: 6162
	private TAG_RACE[] m_availableRacesInBattlegroundsExcludingAmalgam = new TAG_RACE[5];

	// Token: 0x04001813 RID: 6163
	private TAG_RACE[] m_missingRacesInBattlegrounds = new TAG_RACE[3];

	// Token: 0x04001814 RID: 6164
	private Map<int, global::Entity> m_entityMap = new Map<int, global::Entity>();

	// Token: 0x04001815 RID: 6165
	private Map<int, global::Player> m_playerMap = new Map<int, global::Player>();

	// Token: 0x04001816 RID: 6166
	private Map<int, global::SharedPlayerInfo> m_playerInfoMap = new Map<int, global::SharedPlayerInfo>();

	// Token: 0x04001817 RID: 6167
	private GameEntity m_gameEntity;

	// Token: 0x04001818 RID: 6168
	private Queue<global::Entity> m_removedFromGameEntities = new Queue<global::Entity>();

	// Token: 0x04001819 RID: 6169
	private HashSet<int> m_removedFromGameEntityLog = new HashSet<int>();

	// Token: 0x0400181A RID: 6170
	private GameState.CreateGamePhase m_createGamePhase;

	// Token: 0x0400181B RID: 6171
	private Network.HistResetGame m_realTimeResetGame;

	// Token: 0x0400181C RID: 6172
	private Network.HistTagChange m_realTimeGameOverTagChange;

	// Token: 0x0400181D RID: 6173
	private bool m_gameOver;

	// Token: 0x0400181E RID: 6174
	private bool m_concedeRequested;

	// Token: 0x0400181F RID: 6175
	private bool m_restartRequested;

	// Token: 0x04001820 RID: 6176
	private int m_maxSecretZoneSizePerPlayer;

	// Token: 0x04001821 RID: 6177
	private int m_maxSecretsPerPlayer;

	// Token: 0x04001822 RID: 6178
	private int m_maxQuestsPerPlayer;

	// Token: 0x04001823 RID: 6179
	private int m_maxFriendlyMinionsPerPlayer;

	// Token: 0x04001824 RID: 6180
	private GameState.ResponseMode m_responseMode;

	// Token: 0x04001825 RID: 6181
	private Map<int, Network.EntityChoices> m_choicesMap = new Map<int, Network.EntityChoices>();

	// Token: 0x04001826 RID: 6182
	private Queue<GameState.QueuedChoice> m_queuedChoices = new Queue<GameState.QueuedChoice>();

	// Token: 0x04001827 RID: 6183
	private List<global::Entity> m_chosenEntities = new List<global::Entity>();

	// Token: 0x04001828 RID: 6184
	private Network.Options m_options;

	// Token: 0x04001829 RID: 6185
	private GameState.SelectedOption m_selectedOption = new GameState.SelectedOption();

	// Token: 0x0400182A RID: 6186
	private Network.Options m_lastOptions;

	// Token: 0x0400182B RID: 6187
	private GameState.SelectedOption m_lastSelectedOption;

	// Token: 0x0400182C RID: 6188
	private bool m_coinHasSpawned;

	// Token: 0x0400182D RID: 6189
	private global::Card m_friendlyCardBeingDrawn;

	// Token: 0x0400182E RID: 6190
	private global::Card m_opponentCardBeingDrawn;

	// Token: 0x0400182F RID: 6191
	private int m_lastTurnRemindedOfFullHand;

	// Token: 0x04001830 RID: 6192
	private bool m_usingFastActorTriggers;

	// Token: 0x04001831 RID: 6193
	private List<GameState.CreateGameListener> m_createGameListeners = new List<GameState.CreateGameListener>();

	// Token: 0x04001832 RID: 6194
	private List<GameState.OptionsReceivedListener> m_optionsReceivedListeners = new List<GameState.OptionsReceivedListener>();

	// Token: 0x04001833 RID: 6195
	private List<GameState.OptionsSentListener> m_optionsSentListeners = new List<GameState.OptionsSentListener>();

	// Token: 0x04001834 RID: 6196
	private List<GameState.OptionRejectedListener> m_optionRejectedListeners = new List<GameState.OptionRejectedListener>();

	// Token: 0x04001835 RID: 6197
	private List<GameState.EntityChoicesReceivedListener> m_entityChoicesReceivedListeners = new List<GameState.EntityChoicesReceivedListener>();

	// Token: 0x04001836 RID: 6198
	private List<GameState.EntitiesChosenReceivedListener> m_entitiesChosenReceivedListeners = new List<GameState.EntitiesChosenReceivedListener>();

	// Token: 0x04001837 RID: 6199
	private List<GameState.CurrentPlayerChangedListener> m_currentPlayerChangedListeners = new List<GameState.CurrentPlayerChangedListener>();

	// Token: 0x04001838 RID: 6200
	private List<GameState.FriendlyTurnStartedListener> m_friendlyTurnStartedListeners = new List<GameState.FriendlyTurnStartedListener>();

	// Token: 0x04001839 RID: 6201
	private List<GameState.TurnChangedListener> m_turnChangedListeners = new List<GameState.TurnChangedListener>();

	// Token: 0x0400183A RID: 6202
	private List<GameState.SpectatorNotifyListener> m_spectatorNotifyListeners = new List<GameState.SpectatorNotifyListener>();

	// Token: 0x0400183B RID: 6203
	private List<GameState.GameOverListener> m_gameOverListeners = new List<GameState.GameOverListener>();

	// Token: 0x0400183C RID: 6204
	private List<GameState.HeroChangedListener> m_heroChangedListeners = new List<GameState.HeroChangedListener>();

	// Token: 0x0400183D RID: 6205
	private List<GameState.CantPlayListener> m_cantPlayListeners = new List<GameState.CantPlayListener>();

	// Token: 0x0400183E RID: 6206
	private PowerProcessor m_powerProcessor = new PowerProcessor();

	// Token: 0x0400183F RID: 6207
	private float m_reconnectIfStuckTimer;

	// Token: 0x04001840 RID: 6208
	private float m_lastBlockedReportTimestamp;

	// Token: 0x04001841 RID: 6209
	private bool m_busy;

	// Token: 0x04001842 RID: 6210
	private bool m_mulliganBusy;

	// Token: 0x04001843 RID: 6211
	private List<Spell> m_serverBlockingSpells = new List<Spell>();

	// Token: 0x04001844 RID: 6212
	private List<SpellController> m_serverBlockingSpellControllers = new List<SpellController>();

	// Token: 0x04001845 RID: 6213
	private List<GameState.TurnTimerUpdateListener> m_turnTimerUpdateListeners = new List<GameState.TurnTimerUpdateListener>();

	// Token: 0x04001846 RID: 6214
	private List<GameState.TurnTimerUpdateListener> m_mulliganTimerUpdateListeners = new List<GameState.TurnTimerUpdateListener>();

	// Token: 0x04001847 RID: 6215
	private Map<int, TurnTimerUpdate> m_turnTimerUpdates = new Map<int, TurnTimerUpdate>();

	// Token: 0x04001848 RID: 6216
	private AlertPopup m_waitForOpponentReconnectPopup;

	// Token: 0x04001849 RID: 6217
	private AlertPopup.PopupInfo m_waitForOpponentReconnectPopupInfo;

	// Token: 0x0400184A RID: 6218
	private int m_friendlyDrawCounter;

	// Token: 0x0400184B RID: 6219
	private int m_opponentDrawCounter;

	// Token: 0x0400184C RID: 6220
	private GameStateFrameTimeTracker m_lostFrameTimeTracker = GameState.CreateFrameTimeTracker();

	// Token: 0x0400184D RID: 6221
	private GameStateSlushTimeTracker m_lostSlushTimeTracker = GameState.CreateSlushTimeTracker();

	// Token: 0x0400184E RID: 6222
	private float m_clientLostTimeCatchUpThreshold;

	// Token: 0x0400184F RID: 6223
	private bool m_useSlushTimeCatchUp;

	// Token: 0x04001850 RID: 6224
	private bool m_restrictClientLostTimeCatchUpToLowEndDevices;

	// Token: 0x04001851 RID: 6225
	private bool m_allowDeferredPowers = true;

	// Token: 0x04001852 RID: 6226
	private bool m_allowBatchedPowers = true;

	// Token: 0x04001853 RID: 6227
	private bool m_allowDiamondCards = true;

	// Token: 0x04001854 RID: 6228
	private string m_battlegroundMinionPool = "";

	// Token: 0x04001855 RID: 6229
	private string m_battlegroundDenyList = "";

	// Token: 0x04001856 RID: 6230
	private bool m_printBattlegroundMinionPoolOnUpdate;

	// Token: 0x04001857 RID: 6231
	private bool m_printBattlegroundDenyListOnUpdate;

	// Token: 0x02001661 RID: 5729
	public enum ResponseMode
	{
		// Token: 0x0400B0CC RID: 45260
		NONE,
		// Token: 0x0400B0CD RID: 45261
		OPTION,
		// Token: 0x0400B0CE RID: 45262
		SUB_OPTION,
		// Token: 0x0400B0CF RID: 45263
		OPTION_TARGET,
		// Token: 0x0400B0D0 RID: 45264
		CHOICE
	}

	// Token: 0x02001662 RID: 5730
	public enum CreateGamePhase
	{
		// Token: 0x0400B0D2 RID: 45266
		INVALID,
		// Token: 0x0400B0D3 RID: 45267
		CREATING,
		// Token: 0x0400B0D4 RID: 45268
		CREATED
	}

	// Token: 0x02001663 RID: 5731
	// (Invoke) Token: 0x0600E3F0 RID: 58352
	public delegate void GameStateInitializedCallback(GameState instance, object userData);

	// Token: 0x02001664 RID: 5732
	// (Invoke) Token: 0x0600E3F4 RID: 58356
	public delegate void CreateGameCallback(GameState.CreateGamePhase phase, object userData);

	// Token: 0x02001665 RID: 5733
	// (Invoke) Token: 0x0600E3F8 RID: 58360
	public delegate void OptionsReceivedCallback(object userData);

	// Token: 0x02001666 RID: 5734
	// (Invoke) Token: 0x0600E3FC RID: 58364
	public delegate void OptionsSentCallback(Network.Options.Option option, object userData);

	// Token: 0x02001667 RID: 5735
	// (Invoke) Token: 0x0600E400 RID: 58368
	public delegate void OptionRejectedCallback(Network.Options.Option option, object userData);

	// Token: 0x02001668 RID: 5736
	// (Invoke) Token: 0x0600E404 RID: 58372
	public delegate void EntityChoicesReceivedCallback(Network.EntityChoices choices, PowerTaskList preChoiceTaskList, object userData);

	// Token: 0x02001669 RID: 5737
	// (Invoke) Token: 0x0600E408 RID: 58376
	public delegate bool EntitiesChosenReceivedCallback(Network.EntitiesChosen chosen, object userData);

	// Token: 0x0200166A RID: 5738
	// (Invoke) Token: 0x0600E40C RID: 58380
	public delegate void CurrentPlayerChangedCallback(global::Player player, object userData);

	// Token: 0x0200166B RID: 5739
	// (Invoke) Token: 0x0600E410 RID: 58384
	public delegate void TurnChangedCallback(int oldTurn, int newTurn, object userData);

	// Token: 0x0200166C RID: 5740
	// (Invoke) Token: 0x0600E414 RID: 58388
	public delegate void FriendlyTurnStartedCallback(object userData);

	// Token: 0x0200166D RID: 5741
	// (Invoke) Token: 0x0600E418 RID: 58392
	public delegate void TurnTimerUpdateCallback(TurnTimerUpdate update, object userData);

	// Token: 0x0200166E RID: 5742
	// (Invoke) Token: 0x0600E41C RID: 58396
	public delegate void SpectatorNotifyEventCallback(SpectatorNotify notify, object userData);

	// Token: 0x0200166F RID: 5743
	// (Invoke) Token: 0x0600E420 RID: 58400
	public delegate void GameOverCallback(TAG_PLAYSTATE playState, object userData);

	// Token: 0x02001670 RID: 5744
	// (Invoke) Token: 0x0600E424 RID: 58404
	public delegate void HeroChangedCallback(global::Player player, object userData);

	// Token: 0x02001671 RID: 5745
	// (Invoke) Token: 0x0600E428 RID: 58408
	public delegate void CantPlayCallback(global::Entity entity, object userData);

	// Token: 0x02001672 RID: 5746
	// (Invoke) Token: 0x0600E42C RID: 58412
	private delegate void AppendBlockingServerItemCallback<T>(StringBuilder builder, T item);

	// Token: 0x02001673 RID: 5747
	private class SelectedOption
	{
		// Token: 0x0600E42F RID: 58415 RVA: 0x00405E16 File Offset: 0x00404016
		public void Clear()
		{
			this.m_main = -1;
			this.m_sub = -1;
			this.m_target = 0;
			this.m_position = 0;
		}

		// Token: 0x0600E430 RID: 58416 RVA: 0x00405E34 File Offset: 0x00404034
		public void CopyFrom(GameState.SelectedOption original)
		{
			this.m_main = original.m_main;
			this.m_sub = original.m_sub;
			this.m_target = original.m_target;
			this.m_position = original.m_position;
		}

		// Token: 0x0400B0D5 RID: 45269
		public int m_main = -1;

		// Token: 0x0400B0D6 RID: 45270
		public int m_sub = -1;

		// Token: 0x0400B0D7 RID: 45271
		public int m_target;

		// Token: 0x0400B0D8 RID: 45272
		public int m_position;
	}

	// Token: 0x02001674 RID: 5748
	private class QueuedChoice
	{
		// Token: 0x0400B0D9 RID: 45273
		public GameState.QueuedChoice.PacketType m_type;

		// Token: 0x0400B0DA RID: 45274
		public object m_packet;

		// Token: 0x0400B0DB RID: 45275
		public object m_eventData;

		// Token: 0x0200297E RID: 10622
		public enum PacketType
		{
			// Token: 0x0400FCE9 RID: 64745
			ENTITY_CHOICES,
			// Token: 0x0400FCEA RID: 64746
			ENTITIES_CHOSEN
		}
	}

	// Token: 0x02001675 RID: 5749
	private class GameStateInitializedListener : EventListener<GameState.GameStateInitializedCallback>
	{
		// Token: 0x0600E433 RID: 58419 RVA: 0x00405E7C File Offset: 0x0040407C
		public void Fire(GameState instance)
		{
			this.m_callback(instance, this.m_userData);
		}
	}

	// Token: 0x02001676 RID: 5750
	private class CreateGameListener : EventListener<GameState.CreateGameCallback>
	{
		// Token: 0x0600E435 RID: 58421 RVA: 0x00405E98 File Offset: 0x00404098
		public void Fire(GameState.CreateGamePhase phase)
		{
			this.m_callback(phase, this.m_userData);
		}
	}

	// Token: 0x02001677 RID: 5751
	private class OptionsReceivedListener : EventListener<GameState.OptionsReceivedCallback>
	{
		// Token: 0x0600E437 RID: 58423 RVA: 0x00405EB4 File Offset: 0x004040B4
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x02001678 RID: 5752
	private class OptionsSentListener : EventListener<GameState.OptionsSentCallback>
	{
		// Token: 0x0600E439 RID: 58425 RVA: 0x00405ECF File Offset: 0x004040CF
		public void Fire(Network.Options.Option option)
		{
			this.m_callback(option, this.m_userData);
		}
	}

	// Token: 0x02001679 RID: 5753
	private class OptionRejectedListener : EventListener<GameState.OptionRejectedCallback>
	{
		// Token: 0x0600E43B RID: 58427 RVA: 0x00405EEB File Offset: 0x004040EB
		public void Fire(Network.Options.Option option)
		{
			this.m_callback(option, this.m_userData);
		}
	}

	// Token: 0x0200167A RID: 5754
	private class EntityChoicesReceivedListener : EventListener<GameState.EntityChoicesReceivedCallback>
	{
		// Token: 0x0600E43D RID: 58429 RVA: 0x00405F07 File Offset: 0x00404107
		public void Fire(Network.EntityChoices choices, PowerTaskList preChoiceTaskList)
		{
			this.m_callback(choices, preChoiceTaskList, this.m_userData);
		}
	}

	// Token: 0x0200167B RID: 5755
	private class EntitiesChosenReceivedListener : EventListener<GameState.EntitiesChosenReceivedCallback>
	{
		// Token: 0x0600E43F RID: 58431 RVA: 0x00405F24 File Offset: 0x00404124
		public bool Fire(Network.EntitiesChosen chosen)
		{
			return this.m_callback(chosen, this.m_userData);
		}
	}

	// Token: 0x0200167C RID: 5756
	private class CurrentPlayerChangedListener : EventListener<GameState.CurrentPlayerChangedCallback>
	{
		// Token: 0x0600E441 RID: 58433 RVA: 0x00405F40 File Offset: 0x00404140
		public void Fire(global::Player player)
		{
			this.m_callback(player, this.m_userData);
		}
	}

	// Token: 0x0200167D RID: 5757
	private class TurnChangedListener : EventListener<GameState.TurnChangedCallback>
	{
		// Token: 0x0600E443 RID: 58435 RVA: 0x00405F5C File Offset: 0x0040415C
		public void Fire(int oldTurn, int newTurn)
		{
			this.m_callback(oldTurn, newTurn, this.m_userData);
		}
	}

	// Token: 0x0200167E RID: 5758
	private class FriendlyTurnStartedListener : EventListener<GameState.FriendlyTurnStartedCallback>
	{
		// Token: 0x0600E445 RID: 58437 RVA: 0x00405F79 File Offset: 0x00404179
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x0200167F RID: 5759
	private class TurnTimerUpdateListener : EventListener<GameState.TurnTimerUpdateCallback>
	{
		// Token: 0x0600E447 RID: 58439 RVA: 0x00405F94 File Offset: 0x00404194
		public void Fire(TurnTimerUpdate update)
		{
			this.m_callback(update, this.m_userData);
		}
	}

	// Token: 0x02001680 RID: 5760
	private class SpectatorNotifyListener : EventListener<GameState.SpectatorNotifyEventCallback>
	{
		// Token: 0x0600E449 RID: 58441 RVA: 0x00405FB0 File Offset: 0x004041B0
		public void Fire(SpectatorNotify notify)
		{
			this.m_callback(notify, this.m_userData);
		}
	}

	// Token: 0x02001681 RID: 5761
	private class GameOverListener : EventListener<GameState.GameOverCallback>
	{
		// Token: 0x0600E44B RID: 58443 RVA: 0x00405FCC File Offset: 0x004041CC
		public void Fire(TAG_PLAYSTATE playState)
		{
			this.m_callback(playState, this.m_userData);
		}
	}

	// Token: 0x02001682 RID: 5762
	private class HeroChangedListener : EventListener<GameState.HeroChangedCallback>
	{
		// Token: 0x0600E44D RID: 58445 RVA: 0x00405FE8 File Offset: 0x004041E8
		public void Fire(global::Player player)
		{
			this.m_callback(player, this.m_userData);
		}
	}

	// Token: 0x02001683 RID: 5763
	private class CantPlayListener : EventListener<GameState.CantPlayCallback>
	{
		// Token: 0x0600E44F RID: 58447 RVA: 0x00406004 File Offset: 0x00404204
		public void Fire(global::Entity entity)
		{
			this.m_callback(entity, this.m_userData);
		}
	}
}
