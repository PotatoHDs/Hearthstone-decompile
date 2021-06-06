using System;
using System.Collections.Generic;
using System.Linq;
using PegasusGame;
using UnityEngine;

public class PlayerLeaderboardManager : CardTileListDisplay
{
	public enum PlayerTileEvent
	{
		TRIPLE,
		WIN_STREAK,
		TECH_LEVEL,
		RECENT_COMBAT,
		KNOCK_OUT,
		RACES,
		BANANA
	}

	private readonly PlatformDependentValue<float> SPACE_BETWEEN_TILES = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.125f,
		Phone = 0.1f
	};

	private static PlayerLeaderboardManager s_instance;

	private bool m_disabled;

	private List<PlayerLeaderboardCard> m_playerTiles = new List<PlayerLeaderboardCard>();

	private PlayerLeaderboardCard m_currentlyMousedOverTile;

	private bool m_isMousedOver;

	private bool m_isNewMouseOver = true;

	private List<int> m_addedTileForPlayerId = new List<int>();

	private Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> m_combatHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>();

	private Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> m_incomingHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>();

	private Map<int, GameRealTimeBattlefieldRaces> m_pendingRaceCountUpdates = new Map<int, GameRealTimeBattlefieldRaces>();

	private Entity m_oddManOutOpponentHero;

	private const int NULL_PLAYER = 0;

	private bool m_allowFakePlayerTiles;

	private static HistoryTileInitInfo CreateHistoryTileInitInfo(Entity entity)
	{
		HistoryTileInitInfo historyTileInitInfo = new HistoryTileInitInfo();
		historyTileInitInfo.m_entity = entity;
		historyTileInitInfo.m_cardDef = entity.ShareDisposableCardDef();
		using (historyTileInitInfo.m_cardDef)
		{
			if (historyTileInitInfo.m_cardDef?.CardDef != null)
			{
				historyTileInitInfo.m_portraitTexture = historyTileInitInfo.m_cardDef.CardDef.GetPortraitTexture();
				historyTileInitInfo.m_portraitGoldenMaterial = historyTileInitInfo.m_cardDef.CardDef.GetPremiumPortraitMaterial();
				if (historyTileInitInfo.m_cardDef.CardDef.GetLeaderboardTileFullPortrait() != null)
				{
					historyTileInitInfo.m_fullTileMaterial = historyTileInitInfo.m_cardDef.CardDef.GetLeaderboardTileFullPortrait();
				}
				else
				{
					historyTileInitInfo.m_fullTileMaterial = historyTileInitInfo.m_cardDef.CardDef.GetHistoryTileFullPortrait();
				}
				historyTileInitInfo.m_halfTileMaterial = historyTileInitInfo.m_cardDef.CardDef.GetHistoryTileHalfPortrait();
			}
			return historyTileInitInfo;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.15f, base.transform.position.z);
		SetEnabled(enabled: false);
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			Debug.LogWarning("PlayerLeaderboardManager.Awake() - GameState was not Initialized. Initializing now...");
			gameState = GameState.Initialize();
		}
		gameState.RegisterTurnChangedListener(OnTurnChanged);
		gameState.RegisterCreateGameListener(OnCreateGame);
	}

	protected override void OnDestroy()
	{
		s_instance = null;
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterTurnChangedListener(OnTurnChanged);
			GameState.Get().UnregisterCreateGameListener(OnCreateGame);
		}
		base.OnDestroy();
	}

	public static PlayerLeaderboardManager Get()
	{
		return s_instance;
	}

	public void SetEnabled(bool enabled)
	{
		m_disabled = !enabled;
		GetComponent<Collider>().enabled = enabled;
	}

	public bool IsEnabled()
	{
		return !m_disabled;
	}

	public void SetAllowFakePlayers(bool enabled)
	{
		m_allowFakePlayerTiles = enabled;
	}

	public void CreatePlayerTile(Entity playerHero)
	{
		if (m_disabled)
		{
			return;
		}
		int playerHeroId = playerHero.GetTag(GAME_TAG.PLAYER_ID);
		if (playerHeroId == 0)
		{
			playerHeroId = playerHero.GetTag(GAME_TAG.CONTROLLER);
		}
		if (!GameState.Get().GetPlayerInfoMap().ContainsKey(playerHeroId))
		{
			if (!m_allowFakePlayerTiles)
			{
				Log.Gameplay.PrintError($"PlayerLeaderboardManager.CreatePlayerTile() - Attempt to add player id {playerHeroId} to leaderboard, but that is not a valid id.");
				return;
			}
			SharedPlayerInfo sharedPlayerInfo = new SharedPlayerInfo();
			sharedPlayerInfo.SetPlayerId(playerHeroId);
			GameState.Get().AddPlayerInfo(sharedPlayerInfo);
		}
		if (!m_addedTileForPlayerId.Any((int t) => t == playerHeroId))
		{
			m_addedTileForPlayerId.Add(playerHeroId);
			AssetLoader.Get().InstantiatePrefab("PlayerLeaderboardCard.prefab:d44578463b3005d4a938fb1bd2181a82", TileLoadedCallback, playerHero, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	public void UpdatePlayerTileHeroPower(Entity hero, int newHeroPowerId)
	{
		int playerId = hero.GetTag(GAME_TAG.PLAYER_ID);
		PlayerLeaderboardCard tileForPlayerId = GetTileForPlayerId(playerId);
		if (tileForPlayerId != null)
		{
			tileForPlayerId.SetHeroPower(hero);
		}
	}

	public void NotifyPlayerTileEvent(int playerId, PlayerTileEvent tileEvent)
	{
		if (!m_addedTileForPlayerId.Contains(playerId))
		{
			return;
		}
		EmoteType emoteType = EmoteType.INVALID;
		PlayerLeaderboardCard tileForPlayerId = GetTileForPlayerId(playerId);
		switch (tileEvent)
		{
		default:
			return;
		case PlayerTileEvent.TECH_LEVEL:
		{
			int value = 1;
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(playerId) && GameState.Get().GetPlayerInfoMap()[playerId].GetPlayerHero() != null)
			{
				value = GameState.Get().GetPlayerInfoMap()[playerId].GetPlayerHero().GetRealTimePlayerTechLevel();
			}
			value = Mathf.Clamp(value, 1, 6);
			if (tileForPlayerId != null)
			{
				tileForPlayerId.SetTechLevelDirty();
			}
			switch (value)
			{
			default:
				return;
			case 2:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_02;
				break;
			case 3:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_03;
				break;
			case 4:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_04;
				break;
			case 5:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_05;
				break;
			case 6:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TECH_UP_06;
				break;
			}
			break;
		}
		case PlayerTileEvent.TRIPLE:
			if (tileForPlayerId != null)
			{
				tileForPlayerId.SetTriplesDirty();
			}
			emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE;
			break;
		case PlayerTileEvent.WIN_STREAK:
			emoteType = EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK;
			break;
		case PlayerTileEvent.BANANA:
			emoteType = EmoteType.BATTLEGROUNDS_VISUAL_BANANA;
			break;
		case PlayerTileEvent.KNOCK_OUT:
			return;
		case PlayerTileEvent.RECENT_COMBAT:
			if (tileForPlayerId != null)
			{
				tileForPlayerId.SetRecentCombatsDirty();
			}
			return;
		case PlayerTileEvent.RACES:
			if (tileForPlayerId != null)
			{
				tileForPlayerId.SetRacesDirty();
			}
			return;
		}
		GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(playerId, emoteType);
	}

	public void NotifyOfInput(Vector3 hitPoint)
	{
		m_isMousedOver = true;
		if (m_playerTiles.Count == 0)
		{
			CheckForMouseOff();
			return;
		}
		float num = 1000f;
		float num2 = -1000f;
		float num3 = 1000f;
		PlayerLeaderboardCard playerLeaderboardCard = null;
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			if (!playerTile.HasBeenShown())
			{
				continue;
			}
			Collider tileCollider = playerTile.GetTileCollider();
			if (!(tileCollider == null))
			{
				float num4 = tileCollider.bounds.center.z - tileCollider.bounds.extents.z;
				float num5 = tileCollider.bounds.center.z + tileCollider.bounds.extents.z;
				if (num4 < num)
				{
					num = num4;
				}
				if (num5 > num2)
				{
					num2 = num5;
				}
				float num6 = Mathf.Abs(hitPoint.z - num4);
				if (num6 < num3)
				{
					num3 = num6;
					playerLeaderboardCard = playerTile;
				}
				float num7 = Mathf.Abs(hitPoint.z - num5);
				if (num7 < num3)
				{
					num3 = num7;
					playerLeaderboardCard = playerTile;
				}
			}
		}
		if (hitPoint.z < num || hitPoint.z > num2)
		{
			CheckForMouseOff();
			return;
		}
		if (playerLeaderboardCard == null)
		{
			CheckForMouseOff();
			return;
		}
		Collider component = base.gameObject.GetComponent<BoxCollider>();
		Collider tileCollider2 = playerLeaderboardCard.GetTileCollider();
		float num8 = 0f;
		if (playerLeaderboardCard.GetNextOpponentState())
		{
			num8 = playerLeaderboardCard.GetPoppedOutBoneX() * playerLeaderboardCard.m_tileActor.transform.localScale.x;
		}
		if (hitPoint.x < component.bounds.center.x - tileCollider2.bounds.extents.x || hitPoint.x > component.bounds.center.x + tileCollider2.bounds.extents.x + num8)
		{
			CheckForMouseOff();
		}
		else if (!(playerLeaderboardCard == m_currentlyMousedOverTile))
		{
			if (m_currentlyMousedOverTile != null)
			{
				m_currentlyMousedOverTile.NotifyMousedOut();
			}
			else
			{
				FadeVignetteIn();
			}
			m_currentlyMousedOverTile = playerLeaderboardCard;
			playerLeaderboardCard.NotifyMousedOver();
			m_isNewMouseOver = false;
		}
	}

	public void NotifyOfMouseOff()
	{
		CheckForMouseOff();
	}

	public void SetNextOpponent(int opponentPlayerId)
	{
		if (opponentPlayerId == 0)
		{
			return;
		}
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			bool nextOpponentState = playerTile.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == opponentPlayerId;
			playerTile.SetNextOpponentState(nextOpponentState);
		}
	}

	public void SetCurrentOpponent(int opponentPlayerId)
	{
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			bool currentOpponentState = playerTile.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == opponentPlayerId;
			playerTile.SetCurrentOpponentState(currentOpponentState);
		}
	}

	public void ApplyEntityReplacement(int playerID, Entity replacementEntity)
	{
		for (int i = 0; i < m_playerTiles.Count; i++)
		{
			PlayerLeaderboardCard playerLeaderboardCard = m_playerTiles[i];
			if (playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == playerID)
			{
				HistoryTileInitInfo info = CreateHistoryTileInitInfo(replacementEntity);
				playerLeaderboardCard.Initialize(replacementEntity);
				playerLeaderboardCard.RefreshTileVisuals(info);
				playerLeaderboardCard.RefreshMainCardActor();
				playerLeaderboardCard.RefreshMainCardName();
			}
			playerLeaderboardCard.RefreshRecentCombats();
		}
		if (m_oddManOutOpponentHero.GetTag(GAME_TAG.PLAYER_ID) == playerID)
		{
			m_oddManOutOpponentHero = replacementEntity;
		}
	}

	private void OnTurnChanged(int oldTurn, int newTurn, object userdata)
	{
		int num = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
		if (GameState.Get().GetCurrentPlayer().IsFriendlySide())
		{
			SetNextOpponent(num);
			SetCurrentOpponent(-1);
			ApplyIncomingCombatHistory();
			return;
		}
		SetCurrentOpponent(num);
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			playerTile.PauseHealthUpdates();
		}
	}

	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		ApplyIncomingCombatHistory(suppressNotifications: true);
	}

	public bool IsMousedOver()
	{
		return m_isMousedOver;
	}

	public bool IsNewlyMousedOver()
	{
		if (m_isMousedOver)
		{
			return m_isNewMouseOver;
		}
		return false;
	}

	private void CheckForMouseOff()
	{
		if (!m_isMousedOver)
		{
			return;
		}
		m_isMousedOver = false;
		m_isNewMouseOver = true;
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			playerTile.NotifyMousedOut();
		}
		if (m_currentlyMousedOverTile != null)
		{
			FadeVignetteOut();
		}
		m_currentlyMousedOverTile = null;
	}

	private void FadeVignetteIn()
	{
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			if (!(playerTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerTile.m_tileActor.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		activeCameraFullScreenEffects.VignettingEnable = true;
		activeCameraFullScreenEffects.DesaturationEnabled = true;
		activeCameraFullScreenEffects.BlurEnabled = true;
		AnimateBlurVignetteIn();
		FullScreenFXMgr.Get().SetIgnoreStandardBlurVignette(shouldIgnore: true);
	}

	private void FadeVignetteOut()
	{
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			if (!(playerTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerTile.GetTileCollider().gameObject, GameLayer.Default);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.CardRaycast);
		AnimateBlurVignetteOut();
	}

	protected override void OnFullScreenEffectOutFinished()
	{
		if (m_animatingDesat || m_animatingVignette)
		{
			return;
		}
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Disable();
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			if (!(playerTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerTile.m_tileActor.gameObject, GameLayer.Default);
			}
		}
		FullScreenFXMgr.Get().SetIgnoreStandardBlurVignette(shouldIgnore: false);
	}

	private void TileLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		Entity entity = (Entity)callbackData;
		using (DefLoader.DisposableCardDef disposableCardDef = entity.ShareDisposableCardDef())
		{
			if (disposableCardDef?.CardDef == null)
			{
				int item = entity.GetTag(GAME_TAG.PLAYER_ID);
				m_addedTileForPlayerId.Remove(item);
				return;
			}
		}
		PlayerLeaderboardCard component = go.GetComponent<PlayerLeaderboardCard>();
		component.Initialize(entity);
		m_playerTiles.Add(component);
		HistoryTileInitInfo info = CreateHistoryTileInitInfo(entity);
		component.LoadTile(info);
		int key = component.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (m_pendingRaceCountUpdates.ContainsKey(key))
		{
			UpdatePlayerRaces(m_pendingRaceCountUpdates[key]);
		}
		SetAsideTileAndTryToUpdate(component);
	}

	public PlayerLeaderboardCard GetTileForPlayerId(int playerId)
	{
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			if (playerTile.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == playerId)
			{
				return playerTile;
			}
		}
		return null;
	}

	private void SetAsideTileAndTryToUpdate(PlayerLeaderboardCard tile)
	{
		Vector3 topTilePosition = GetTopTilePosition();
		int num = m_playerTiles.IndexOf(tile);
		Collider tileCollider = tile.GetTileCollider();
		float num2 = 0f;
		if (tileCollider != null)
		{
			num2 = (tileCollider.bounds.size.z + (float)SPACE_BETWEEN_TILES) * (float)num;
		}
		tile.transform.position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num2);
		if (GameState.Get().IsMulliganManagerActive())
		{
			tile.m_PlayerLeaderboardTile.SetTileRevealed(revealed: false, isNextOpponent: false);
			return;
		}
		tile.MarkAsShown();
		UpdateLayout(animate: false);
	}

	private Vector3 GetTopTilePosition()
	{
		return new Vector3(base.transform.position.x, base.transform.position.y - 0.15f, base.transform.position.z);
	}

	public void UpdateLayout(bool animate = true)
	{
		SortPlayers();
		UpdateHealthTotals();
		float num = 0f;
		Vector3 topTilePosition = GetTopTilePosition();
		for (int i = 0; i < m_playerTiles.Count; i++)
		{
			Collider tileCollider = m_playerTiles[i].GetTileCollider();
			Vector3 position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num);
			if (animate)
			{
				iTween.MoveTo(m_playerTiles[i].gameObject, position, 1f);
			}
			else
			{
				m_playerTiles[i].gameObject.transform.position = position;
			}
			bool isNextOpponent = m_playerTiles[i].m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
			if (!m_playerTiles[i].HasBeenShown() && GameState.Get().IsMulliganManagerActive())
			{
				m_playerTiles[i].m_PlayerLeaderboardTile.SetTileRevealed(revealed: true, isNextOpponent);
			}
			m_playerTiles[i].MarkAsShown();
			m_playerTiles[i].UpdateOddPlayerOutFx(isNextOpponent);
			if (tileCollider != null)
			{
				num += tileCollider.bounds.size.z + (float)SPACE_BETWEEN_TILES;
			}
		}
	}

	public void UpdateRoundHistory(GameRoundHistory gameRoundHistory)
	{
		m_incomingHistory.Clear();
		for (int i = 0; i < gameRoundHistory.Rounds.Count; i++)
		{
			GameRoundHistoryEntry gameRound = gameRoundHistory.Rounds[i];
			AddCombatRound(gameRound);
		}
	}

	public void UpdatePlayerRaces(GameRealTimeBattlefieldRaces realTimeBattlefieldRaces)
	{
		PlayerLeaderboardCard tileForPlayerId = GetTileForPlayerId(realTimeBattlefieldRaces.PlayerId);
		if (tileForPlayerId != null)
		{
			tileForPlayerId.UpdateRacesCount(realTimeBattlefieldRaces.Races);
		}
		else if (!m_pendingRaceCountUpdates.ContainsKey(realTimeBattlefieldRaces.PlayerId))
		{
			m_pendingRaceCountUpdates.Add(realTimeBattlefieldRaces.PlayerId, realTimeBattlefieldRaces);
		}
		else
		{
			m_pendingRaceCountUpdates[realTimeBattlefieldRaces.PlayerId] = realTimeBattlefieldRaces;
		}
	}

	private void ApplyIncomingCombatHistory(bool suppressNotifications = false)
	{
		m_combatHistory.Clear();
		m_combatHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>(m_incomingHistory);
		int num = 0;
		foreach (KeyValuePair<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> item in m_combatHistory)
		{
			if (item.Value != null && item.Value.Count != 0)
			{
				num = Math.Max(num, item.Value.Count);
			}
		}
		foreach (KeyValuePair<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> item2 in m_combatHistory)
		{
			if (item2.Value != null && item2.Value.Count != 0)
			{
				PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo = default(PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo);
				recentCombatInfo.winStreak = 0;
				PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo2 = item2.Value.LastOrDefault();
				if (item2.Value.Count > 1)
				{
					recentCombatInfo = item2.Value[item2.Value.Count - 2];
				}
				if (recentCombatInfo2.winStreak > 1 && recentCombatInfo2.winStreak > recentCombatInfo.winStreak && item2.Value.Count == num && !suppressNotifications)
				{
					NotifyPlayerTileEvent(item2.Key, PlayerTileEvent.WIN_STREAK);
				}
				NotifyPlayerTileEvent(item2.Key, PlayerTileEvent.RECENT_COMBAT);
			}
		}
	}

	private void AddCombatRound(GameRoundHistoryEntry gameRound)
	{
		Dictionary<int, GameRoundHistoryPlayerEntry> dictionary = gameRound.Combats.ToDictionary((GameRoundHistoryPlayerEntry combat) => combat.PlayerId, (GameRoundHistoryPlayerEntry combat) => combat);
		foreach (KeyValuePair<int, GameRoundHistoryPlayerEntry> item in dictionary)
		{
			int key = item.Key;
			if (key != 0 && (!item.Value.PlayerIsDead || item.Value.PlayerDiedThisRound))
			{
				AddPlayerToCombatHistoryIfNeeded(key);
				GameRoundHistoryPlayerEntry value = item.Value;
				GameRoundHistoryPlayerEntry opponentEntry = dictionary[value.PlayerOpponentId];
				m_incomingHistory[key].Add(ConvertGameRoundHistoryToRecentCombatInfo(value, opponentEntry));
			}
		}
	}

	private PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo ConvertGameRoundHistoryToRecentCombatInfo(GameRoundHistoryPlayerEntry playerEntry, GameRoundHistoryPlayerEntry opponentEntry)
	{
		PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo result = default(PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo);
		result.ownerId = playerEntry.PlayerId;
		result.opponentId = opponentEntry.PlayerId;
		result.damage = ((playerEntry.PlayerDamageTaken != 0) ? playerEntry.PlayerDamageTaken : opponentEntry.PlayerDamageTaken);
		result.isDefeated = playerEntry.PlayerIsDead || opponentEntry.PlayerIsDead;
		if (playerEntry.PlayerDamageTaken != 0)
		{
			result.damageTarget = playerEntry.PlayerId;
		}
		else if (opponentEntry.PlayerDamageTaken != 0)
		{
			result.damageTarget = opponentEntry.PlayerId;
		}
		else
		{
			result.damageTarget = PlayerLeaderboardRecentCombatsPanel.NO_DAMAGE_TARGET;
		}
		if (result.damageTarget == playerEntry.PlayerId && (result.damage > 0 || result.isDefeated))
		{
			result.winStreak = 0;
		}
		else
		{
			int num = 0;
			if (m_incomingHistory[playerEntry.PlayerId].Count > 0)
			{
				num = m_incomingHistory[playerEntry.PlayerId].Last().winStreak;
			}
			if (result.damageTarget != PlayerLeaderboardRecentCombatsPanel.NO_DAMAGE_TARGET)
			{
				result.winStreak = num + 1;
			}
			else
			{
				result.winStreak = num;
			}
		}
		return result;
	}

	private void AddPlayerToCombatHistoryIfNeeded(int playerId)
	{
		if (playerId != 0 && !m_incomingHistory.ContainsKey(playerId))
		{
			m_incomingHistory.Add(playerId, new List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>());
		}
	}

	public List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo> GetRecentCombatHistoryForPlayer(int playerId)
	{
		if (m_combatHistory.ContainsKey(playerId))
		{
			return m_combatHistory[playerId];
		}
		return null;
	}

	public int GetNumTiles()
	{
		return m_playerTiles.Count;
	}

	public int GetIndexForTile(PlayerLeaderboardCard tile)
	{
		for (int i = 0; i < m_playerTiles.Count; i++)
		{
			if (m_playerTiles[i] == tile)
			{
				return i;
			}
		}
		Debug.LogWarning("PlayerLeaderboardManager.GetIndexForTile() - that Tile doesn't exist!");
		return -1;
	}

	private void SortPlayers()
	{
		m_playerTiles = m_playerTiles.OrderBy((PlayerLeaderboardCard t) => t.m_playerHeroEntity.GetRealTimePlayerLeaderboardPlace()).ToList();
		for (int i = 0; i < m_playerTiles.Count; i++)
		{
			m_playerTiles[i].m_PlayerLeaderboardTile.SetPlaceIcon(i + 1);
		}
	}

	private void UpdateHealthTotals()
	{
		foreach (PlayerLeaderboardCard playerTile in m_playerTiles)
		{
			playerTile.UpdateTileHealth();
		}
	}

	public void SetOddManOutOpponentHero(Entity entity)
	{
		m_oddManOutOpponentHero = entity;
	}

	public Entity GetOddManOutOpponentHero()
	{
		return m_oddManOutOpponentHero;
	}
}
