using System;
using System.Collections.Generic;
using System.Linq;
using PegasusGame;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class PlayerLeaderboardManager : CardTileListDisplay
{
	// Token: 0x06002F25 RID: 12069 RVA: 0x000F02FC File Offset: 0x000EE4FC
	private static HistoryTileInitInfo CreateHistoryTileInitInfo(global::Entity entity)
	{
		HistoryTileInitInfo historyTileInitInfo = new HistoryTileInitInfo();
		historyTileInitInfo.m_entity = entity;
		historyTileInitInfo.m_cardDef = entity.ShareDisposableCardDef();
		HistoryTileInitInfo result;
		using (historyTileInitInfo.m_cardDef)
		{
			DefLoader.DisposableCardDef cardDef2 = historyTileInitInfo.m_cardDef;
			if (((cardDef2 != null) ? cardDef2.CardDef : null) != null)
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
			result = historyTileInitInfo;
		}
		return result;
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x000F03F0 File Offset: 0x000EE5F0
	protected override void Awake()
	{
		base.Awake();
		PlayerLeaderboardManager.s_instance = this;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.15f, base.transform.position.z);
		this.SetEnabled(false);
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			Debug.LogWarning("PlayerLeaderboardManager.Awake() - GameState was not Initialized. Initializing now...");
			gameState = GameState.Initialize();
		}
		gameState.RegisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
		gameState.RegisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
	}

	// Token: 0x06002F27 RID: 12071 RVA: 0x000F0498 File Offset: 0x000EE698
	protected override void OnDestroy()
	{
		PlayerLeaderboardManager.s_instance = null;
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
			GameState.Get().UnregisterCreateGameListener(new GameState.CreateGameCallback(this.OnCreateGame));
		}
		base.OnDestroy();
	}

	// Token: 0x06002F28 RID: 12072 RVA: 0x000F04E6 File Offset: 0x000EE6E6
	public static PlayerLeaderboardManager Get()
	{
		return PlayerLeaderboardManager.s_instance;
	}

	// Token: 0x06002F29 RID: 12073 RVA: 0x000F04ED File Offset: 0x000EE6ED
	public void SetEnabled(bool enabled)
	{
		this.m_disabled = !enabled;
		base.GetComponent<Collider>().enabled = enabled;
	}

	// Token: 0x06002F2A RID: 12074 RVA: 0x000F0505 File Offset: 0x000EE705
	public bool IsEnabled()
	{
		return !this.m_disabled;
	}

	// Token: 0x06002F2B RID: 12075 RVA: 0x000F0510 File Offset: 0x000EE710
	public void SetAllowFakePlayers(bool enabled)
	{
		this.m_allowFakePlayerTiles = enabled;
	}

	// Token: 0x06002F2C RID: 12076 RVA: 0x000F051C File Offset: 0x000EE71C
	public void CreatePlayerTile(global::Entity playerHero)
	{
		if (this.m_disabled)
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
			if (!this.m_allowFakePlayerTiles)
			{
				Log.Gameplay.PrintError(string.Format("PlayerLeaderboardManager.CreatePlayerTile() - Attempt to add player id {0} to leaderboard, but that is not a valid id.", playerHeroId), Array.Empty<object>());
				return;
			}
			global::SharedPlayerInfo sharedPlayerInfo = new global::SharedPlayerInfo();
			sharedPlayerInfo.SetPlayerId(playerHeroId);
			GameState.Get().AddPlayerInfo(sharedPlayerInfo);
		}
		if (this.m_addedTileForPlayerId.Any((int t) => t == playerHeroId))
		{
			return;
		}
		this.m_addedTileForPlayerId.Add(playerHeroId);
		AssetLoader.Get().InstantiatePrefab("PlayerLeaderboardCard.prefab:d44578463b3005d4a938fb1bd2181a82", new PrefabCallback<GameObject>(this.TileLoadedCallback), playerHero, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002F2D RID: 12077 RVA: 0x000F0610 File Offset: 0x000EE810
	public void UpdatePlayerTileHeroPower(global::Entity hero, int newHeroPowerId)
	{
		int tag = hero.GetTag(GAME_TAG.PLAYER_ID);
		PlayerLeaderboardCard tileForPlayerId = this.GetTileForPlayerId(tag);
		if (tileForPlayerId != null)
		{
			tileForPlayerId.SetHeroPower(hero);
		}
	}

	// Token: 0x06002F2E RID: 12078 RVA: 0x000F0640 File Offset: 0x000EE840
	public void NotifyPlayerTileEvent(int playerId, PlayerLeaderboardManager.PlayerTileEvent tileEvent)
	{
		if (this.m_addedTileForPlayerId.Contains(playerId))
		{
			PlayerLeaderboardCard tileForPlayerId = this.GetTileForPlayerId(playerId);
			EmoteType emoteType;
			switch (tileEvent)
			{
			case PlayerLeaderboardManager.PlayerTileEvent.TRIPLE:
				if (tileForPlayerId != null)
				{
					tileForPlayerId.SetTriplesDirty();
				}
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_TRIPLE;
				break;
			case PlayerLeaderboardManager.PlayerTileEvent.WIN_STREAK:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_HOT_STREAK;
				break;
			case PlayerLeaderboardManager.PlayerTileEvent.TECH_LEVEL:
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
				default:
					return;
				}
				break;
			}
			case PlayerLeaderboardManager.PlayerTileEvent.RECENT_COMBAT:
				if (tileForPlayerId != null)
				{
					tileForPlayerId.SetRecentCombatsDirty();
				}
				return;
			case PlayerLeaderboardManager.PlayerTileEvent.KNOCK_OUT:
				return;
			case PlayerLeaderboardManager.PlayerTileEvent.RACES:
				if (tileForPlayerId != null)
				{
					tileForPlayerId.SetRacesDirty();
				}
				return;
			case PlayerLeaderboardManager.PlayerTileEvent.BANANA:
				emoteType = EmoteType.BATTLEGROUNDS_VISUAL_BANANA;
				break;
			default:
				return;
			}
			GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(playerId, emoteType);
		}
	}

	// Token: 0x06002F2F RID: 12079 RVA: 0x000F0770 File Offset: 0x000EE970
	public void NotifyOfInput(Vector3 hitPoint)
	{
		this.m_isMousedOver = true;
		if (this.m_playerTiles.Count == 0)
		{
			this.CheckForMouseOff();
			return;
		}
		float num = 1000f;
		float num2 = -1000f;
		float num3 = 1000f;
		PlayerLeaderboardCard playerLeaderboardCard = null;
		foreach (PlayerLeaderboardCard playerLeaderboardCard2 in this.m_playerTiles)
		{
			if (playerLeaderboardCard2.HasBeenShown())
			{
				Collider tileCollider = playerLeaderboardCard2.GetTileCollider();
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
						playerLeaderboardCard = playerLeaderboardCard2;
					}
					float num7 = Mathf.Abs(hitPoint.z - num5);
					if (num7 < num3)
					{
						num3 = num7;
						playerLeaderboardCard = playerLeaderboardCard2;
					}
				}
			}
		}
		if (hitPoint.z < num || hitPoint.z > num2)
		{
			this.CheckForMouseOff();
			return;
		}
		if (playerLeaderboardCard == null)
		{
			this.CheckForMouseOff();
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
			this.CheckForMouseOff();
			return;
		}
		if (playerLeaderboardCard == this.m_currentlyMousedOverTile)
		{
			return;
		}
		if (this.m_currentlyMousedOverTile != null)
		{
			this.m_currentlyMousedOverTile.NotifyMousedOut();
		}
		else
		{
			this.FadeVignetteIn();
		}
		this.m_currentlyMousedOverTile = playerLeaderboardCard;
		playerLeaderboardCard.NotifyMousedOver();
		this.m_isNewMouseOver = false;
	}

	// Token: 0x06002F30 RID: 12080 RVA: 0x000F09D4 File Offset: 0x000EEBD4
	public void NotifyOfMouseOff()
	{
		this.CheckForMouseOff();
	}

	// Token: 0x06002F31 RID: 12081 RVA: 0x000F09DC File Offset: 0x000EEBDC
	public void SetNextOpponent(int opponentPlayerId)
	{
		if (opponentPlayerId == 0)
		{
			return;
		}
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			bool nextOpponentState = playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == opponentPlayerId;
			playerLeaderboardCard.SetNextOpponentState(nextOpponentState);
		}
	}

	// Token: 0x06002F32 RID: 12082 RVA: 0x000F0A44 File Offset: 0x000EEC44
	public void SetCurrentOpponent(int opponentPlayerId)
	{
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			bool currentOpponentState = playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == opponentPlayerId;
			playerLeaderboardCard.SetCurrentOpponentState(currentOpponentState);
		}
	}

	// Token: 0x06002F33 RID: 12083 RVA: 0x000F0AA8 File Offset: 0x000EECA8
	public void ApplyEntityReplacement(int playerID, global::Entity replacementEntity)
	{
		for (int i = 0; i < this.m_playerTiles.Count; i++)
		{
			PlayerLeaderboardCard playerLeaderboardCard = this.m_playerTiles[i];
			if (playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == playerID)
			{
				HistoryTileInitInfo info = PlayerLeaderboardManager.CreateHistoryTileInitInfo(replacementEntity);
				playerLeaderboardCard.Initialize(replacementEntity);
				playerLeaderboardCard.RefreshTileVisuals(info);
				playerLeaderboardCard.RefreshMainCardActor();
				playerLeaderboardCard.RefreshMainCardName();
			}
			playerLeaderboardCard.RefreshRecentCombats();
		}
		if (this.m_oddManOutOpponentHero.GetTag(GAME_TAG.PLAYER_ID) == playerID)
		{
			this.m_oddManOutOpponentHero = replacementEntity;
		}
	}

	// Token: 0x06002F34 RID: 12084 RVA: 0x000F0B28 File Offset: 0x000EED28
	private void OnTurnChanged(int oldTurn, int newTurn, object userdata)
	{
		int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
		if (GameState.Get().GetCurrentPlayer().IsFriendlySide())
		{
			this.SetNextOpponent(tag);
			this.SetCurrentOpponent(-1);
			this.ApplyIncomingCombatHistory(false);
			return;
		}
		this.SetCurrentOpponent(tag);
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			playerLeaderboardCard.PauseHealthUpdates();
		}
	}

	// Token: 0x06002F35 RID: 12085 RVA: 0x000F0BBC File Offset: 0x000EEDBC
	private void OnCreateGame(GameState.CreateGamePhase phase, object userData)
	{
		this.ApplyIncomingCombatHistory(true);
	}

	// Token: 0x06002F36 RID: 12086 RVA: 0x000F0BC5 File Offset: 0x000EEDC5
	public bool IsMousedOver()
	{
		return this.m_isMousedOver;
	}

	// Token: 0x06002F37 RID: 12087 RVA: 0x000F0BCD File Offset: 0x000EEDCD
	public bool IsNewlyMousedOver()
	{
		return this.m_isMousedOver && this.m_isNewMouseOver;
	}

	// Token: 0x06002F38 RID: 12088 RVA: 0x000F0BE0 File Offset: 0x000EEDE0
	private void CheckForMouseOff()
	{
		if (!this.m_isMousedOver)
		{
			return;
		}
		this.m_isMousedOver = false;
		this.m_isNewMouseOver = true;
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			playerLeaderboardCard.NotifyMousedOut();
		}
		if (this.m_currentlyMousedOverTile != null)
		{
			this.FadeVignetteOut();
		}
		this.m_currentlyMousedOverTile = null;
	}

	// Token: 0x06002F39 RID: 12089 RVA: 0x000F0C64 File Offset: 0x000EEE64
	private void FadeVignetteIn()
	{
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			if (!(playerLeaderboardCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerLeaderboardCard.m_tileActor.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		activeCameraFullScreenEffects.VignettingEnable = true;
		activeCameraFullScreenEffects.DesaturationEnabled = true;
		activeCameraFullScreenEffects.BlurEnabled = true;
		base.AnimateBlurVignetteIn();
		FullScreenFXMgr.Get().SetIgnoreStandardBlurVignette(true);
	}

	// Token: 0x06002F3A RID: 12090 RVA: 0x000F0D0C File Offset: 0x000EEF0C
	private void FadeVignetteOut()
	{
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			if (!(playerLeaderboardCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerLeaderboardCard.GetTileCollider().gameObject, GameLayer.Default);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.CardRaycast);
		base.AnimateBlurVignetteOut();
	}

	// Token: 0x06002F3B RID: 12091 RVA: 0x000F0D8C File Offset: 0x000EEF8C
	protected override void OnFullScreenEffectOutFinished()
	{
		if (this.m_animatingDesat || this.m_animatingVignette)
		{
			return;
		}
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Disable();
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			if (!(playerLeaderboardCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(playerLeaderboardCard.m_tileActor.gameObject, GameLayer.Default);
			}
		}
		FullScreenFXMgr.Get().SetIgnoreStandardBlurVignette(false);
	}

	// Token: 0x06002F3C RID: 12092 RVA: 0x000F0E24 File Offset: 0x000EF024
	private void TileLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		global::Entity entity = (global::Entity)callbackData;
		using (DefLoader.DisposableCardDef disposableCardDef = entity.ShareDisposableCardDef())
		{
			if (((disposableCardDef != null) ? disposableCardDef.CardDef : null) == null)
			{
				int tag = entity.GetTag(GAME_TAG.PLAYER_ID);
				this.m_addedTileForPlayerId.Remove(tag);
				return;
			}
		}
		PlayerLeaderboardCard component = go.GetComponent<PlayerLeaderboardCard>();
		component.Initialize(entity);
		this.m_playerTiles.Add(component);
		HistoryTileInitInfo info = PlayerLeaderboardManager.CreateHistoryTileInitInfo(entity);
		component.LoadTile(info);
		int tag2 = component.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (this.m_pendingRaceCountUpdates.ContainsKey(tag2))
		{
			this.UpdatePlayerRaces(this.m_pendingRaceCountUpdates[tag2]);
		}
		this.SetAsideTileAndTryToUpdate(component);
	}

	// Token: 0x06002F3D RID: 12093 RVA: 0x000F0EEC File Offset: 0x000EF0EC
	public PlayerLeaderboardCard GetTileForPlayerId(int playerId)
	{
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			if (playerLeaderboardCard.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == playerId)
			{
				return playerLeaderboardCard;
			}
		}
		return null;
	}

	// Token: 0x06002F3E RID: 12094 RVA: 0x000F0F50 File Offset: 0x000EF150
	private void SetAsideTileAndTryToUpdate(PlayerLeaderboardCard tile)
	{
		Vector3 topTilePosition = this.GetTopTilePosition();
		int num = this.m_playerTiles.IndexOf(tile);
		Collider tileCollider = tile.GetTileCollider();
		float num2 = 0f;
		if (tileCollider != null)
		{
			num2 = (tileCollider.bounds.size.z + this.SPACE_BETWEEN_TILES) * (float)num;
		}
		tile.transform.position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num2);
		if (GameState.Get().IsMulliganManagerActive())
		{
			tile.m_PlayerLeaderboardTile.SetTileRevealed(false, false);
			return;
		}
		tile.MarkAsShown();
		this.UpdateLayout(false);
	}

	// Token: 0x06002F3F RID: 12095 RVA: 0x000E190E File Offset: 0x000DFB0E
	private Vector3 GetTopTilePosition()
	{
		return new Vector3(base.transform.position.x, base.transform.position.y - 0.15f, base.transform.position.z);
	}

	// Token: 0x06002F40 RID: 12096 RVA: 0x000F0FF8 File Offset: 0x000EF1F8
	public void UpdateLayout(bool animate = true)
	{
		this.SortPlayers();
		this.UpdateHealthTotals();
		float num = 0f;
		Vector3 topTilePosition = this.GetTopTilePosition();
		for (int i = 0; i < this.m_playerTiles.Count; i++)
		{
			Collider tileCollider = this.m_playerTiles[i].GetTileCollider();
			Vector3 position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num);
			if (animate)
			{
				iTween.MoveTo(this.m_playerTiles[i].gameObject, position, 1f);
			}
			else
			{
				this.m_playerTiles[i].gameObject.transform.position = position;
			}
			bool isNextOpponent = this.m_playerTiles[i].m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID) == GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.NEXT_OPPONENT_PLAYER_ID);
			if (!this.m_playerTiles[i].HasBeenShown() && GameState.Get().IsMulliganManagerActive())
			{
				this.m_playerTiles[i].m_PlayerLeaderboardTile.SetTileRevealed(true, isNextOpponent);
			}
			this.m_playerTiles[i].MarkAsShown();
			this.m_playerTiles[i].UpdateOddPlayerOutFx(isNextOpponent);
			if (tileCollider != null)
			{
				num += tileCollider.bounds.size.z + this.SPACE_BETWEEN_TILES;
			}
		}
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x000F1160 File Offset: 0x000EF360
	public void UpdateRoundHistory(GameRoundHistory gameRoundHistory)
	{
		this.m_incomingHistory.Clear();
		for (int i = 0; i < gameRoundHistory.Rounds.Count; i++)
		{
			GameRoundHistoryEntry gameRound = gameRoundHistory.Rounds[i];
			this.AddCombatRound(gameRound);
		}
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x000F11A4 File Offset: 0x000EF3A4
	public void UpdatePlayerRaces(GameRealTimeBattlefieldRaces realTimeBattlefieldRaces)
	{
		PlayerLeaderboardCard tileForPlayerId = this.GetTileForPlayerId(realTimeBattlefieldRaces.PlayerId);
		if (tileForPlayerId != null)
		{
			tileForPlayerId.UpdateRacesCount(realTimeBattlefieldRaces.Races);
			return;
		}
		if (!this.m_pendingRaceCountUpdates.ContainsKey(realTimeBattlefieldRaces.PlayerId))
		{
			this.m_pendingRaceCountUpdates.Add(realTimeBattlefieldRaces.PlayerId, realTimeBattlefieldRaces);
			return;
		}
		this.m_pendingRaceCountUpdates[realTimeBattlefieldRaces.PlayerId] = realTimeBattlefieldRaces;
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x000F120C File Offset: 0x000EF40C
	private void ApplyIncomingCombatHistory(bool suppressNotifications = false)
	{
		this.m_combatHistory.Clear();
		this.m_combatHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>(this.m_incomingHistory);
		int num = 0;
		foreach (KeyValuePair<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> keyValuePair in this.m_combatHistory)
		{
			if (keyValuePair.Value != null && keyValuePair.Value.Count != 0)
			{
				num = Math.Max(num, keyValuePair.Value.Count);
			}
		}
		foreach (KeyValuePair<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> keyValuePair2 in this.m_combatHistory)
		{
			if (keyValuePair2.Value != null && keyValuePair2.Value.Count != 0)
			{
				PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo = default(PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo);
				recentCombatInfo.winStreak = 0;
				PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo2 = keyValuePair2.Value.LastOrDefault<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>();
				if (keyValuePair2.Value.Count > 1)
				{
					recentCombatInfo = keyValuePair2.Value[keyValuePair2.Value.Count - 2];
				}
				if (recentCombatInfo2.winStreak > 1 && recentCombatInfo2.winStreak > recentCombatInfo.winStreak && keyValuePair2.Value.Count == num && !suppressNotifications)
				{
					this.NotifyPlayerTileEvent(keyValuePair2.Key, PlayerLeaderboardManager.PlayerTileEvent.WIN_STREAK);
				}
				this.NotifyPlayerTileEvent(keyValuePair2.Key, PlayerLeaderboardManager.PlayerTileEvent.RECENT_COMBAT);
			}
		}
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x000F1390 File Offset: 0x000EF590
	private void AddCombatRound(GameRoundHistoryEntry gameRound)
	{
		Dictionary<int, GameRoundHistoryPlayerEntry> dictionary = gameRound.Combats.ToDictionary((GameRoundHistoryPlayerEntry combat) => combat.PlayerId, (GameRoundHistoryPlayerEntry combat) => combat);
		foreach (KeyValuePair<int, GameRoundHistoryPlayerEntry> keyValuePair in dictionary)
		{
			int key = keyValuePair.Key;
			if (key != 0 && (!keyValuePair.Value.PlayerIsDead || keyValuePair.Value.PlayerDiedThisRound))
			{
				this.AddPlayerToCombatHistoryIfNeeded(key);
				GameRoundHistoryPlayerEntry value = keyValuePair.Value;
				GameRoundHistoryPlayerEntry opponentEntry = dictionary[value.PlayerOpponentId];
				this.m_incomingHistory[key].Add(this.ConvertGameRoundHistoryToRecentCombatInfo(value, opponentEntry));
			}
		}
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x000F1484 File Offset: 0x000EF684
	private PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo ConvertGameRoundHistoryToRecentCombatInfo(GameRoundHistoryPlayerEntry playerEntry, GameRoundHistoryPlayerEntry opponentEntry)
	{
		PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo = default(PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo);
		recentCombatInfo.ownerId = playerEntry.PlayerId;
		recentCombatInfo.opponentId = opponentEntry.PlayerId;
		recentCombatInfo.damage = ((playerEntry.PlayerDamageTaken != 0) ? playerEntry.PlayerDamageTaken : opponentEntry.PlayerDamageTaken);
		recentCombatInfo.isDefeated = (playerEntry.PlayerIsDead || opponentEntry.PlayerIsDead);
		if (playerEntry.PlayerDamageTaken != 0)
		{
			recentCombatInfo.damageTarget = playerEntry.PlayerId;
		}
		else if (opponentEntry.PlayerDamageTaken != 0)
		{
			recentCombatInfo.damageTarget = opponentEntry.PlayerId;
		}
		else
		{
			recentCombatInfo.damageTarget = PlayerLeaderboardRecentCombatsPanel.NO_DAMAGE_TARGET;
		}
		if (recentCombatInfo.damageTarget == playerEntry.PlayerId && (recentCombatInfo.damage > 0 || recentCombatInfo.isDefeated))
		{
			recentCombatInfo.winStreak = 0;
		}
		else
		{
			int num = 0;
			if (this.m_incomingHistory[playerEntry.PlayerId].Count > 0)
			{
				num = this.m_incomingHistory[playerEntry.PlayerId].Last<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>().winStreak;
			}
			if (recentCombatInfo.damageTarget != PlayerLeaderboardRecentCombatsPanel.NO_DAMAGE_TARGET)
			{
				recentCombatInfo.winStreak = num + 1;
			}
			else
			{
				recentCombatInfo.winStreak = num;
			}
		}
		return recentCombatInfo;
	}

	// Token: 0x06002F46 RID: 12102 RVA: 0x000F15A4 File Offset: 0x000EF7A4
	private void AddPlayerToCombatHistoryIfNeeded(int playerId)
	{
		if (playerId != 0 && !this.m_incomingHistory.ContainsKey(playerId))
		{
			this.m_incomingHistory.Add(playerId, new List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>());
		}
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x000F15C8 File Offset: 0x000EF7C8
	public List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo> GetRecentCombatHistoryForPlayer(int playerId)
	{
		if (this.m_combatHistory.ContainsKey(playerId))
		{
			return this.m_combatHistory[playerId];
		}
		return null;
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x000F15E6 File Offset: 0x000EF7E6
	public int GetNumTiles()
	{
		return this.m_playerTiles.Count;
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x000F15F4 File Offset: 0x000EF7F4
	public int GetIndexForTile(PlayerLeaderboardCard tile)
	{
		for (int i = 0; i < this.m_playerTiles.Count; i++)
		{
			if (this.m_playerTiles[i] == tile)
			{
				return i;
			}
		}
		Debug.LogWarning("PlayerLeaderboardManager.GetIndexForTile() - that Tile doesn't exist!");
		return -1;
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x000F1638 File Offset: 0x000EF838
	private void SortPlayers()
	{
		this.m_playerTiles = (from t in this.m_playerTiles
		orderby t.m_playerHeroEntity.GetRealTimePlayerLeaderboardPlace()
		select t).ToList<PlayerLeaderboardCard>();
		for (int i = 0; i < this.m_playerTiles.Count; i++)
		{
			this.m_playerTiles[i].m_PlayerLeaderboardTile.SetPlaceIcon(i + 1);
		}
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x000F16AC File Offset: 0x000EF8AC
	private void UpdateHealthTotals()
	{
		foreach (PlayerLeaderboardCard playerLeaderboardCard in this.m_playerTiles)
		{
			playerLeaderboardCard.UpdateTileHealth();
		}
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x000F16FC File Offset: 0x000EF8FC
	public void SetOddManOutOpponentHero(global::Entity entity)
	{
		this.m_oddManOutOpponentHero = entity;
	}

	// Token: 0x06002F4D RID: 12109 RVA: 0x000F1705 File Offset: 0x000EF905
	public global::Entity GetOddManOutOpponentHero()
	{
		return this.m_oddManOutOpponentHero;
	}

	// Token: 0x04001A4F RID: 6735
	private readonly PlatformDependentValue<float> SPACE_BETWEEN_TILES = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.125f,
		Phone = 0.1f
	};

	// Token: 0x04001A50 RID: 6736
	private static PlayerLeaderboardManager s_instance;

	// Token: 0x04001A51 RID: 6737
	private bool m_disabled;

	// Token: 0x04001A52 RID: 6738
	private List<PlayerLeaderboardCard> m_playerTiles = new List<PlayerLeaderboardCard>();

	// Token: 0x04001A53 RID: 6739
	private PlayerLeaderboardCard m_currentlyMousedOverTile;

	// Token: 0x04001A54 RID: 6740
	private bool m_isMousedOver;

	// Token: 0x04001A55 RID: 6741
	private bool m_isNewMouseOver = true;

	// Token: 0x04001A56 RID: 6742
	private List<int> m_addedTileForPlayerId = new List<int>();

	// Token: 0x04001A57 RID: 6743
	private Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> m_combatHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>();

	// Token: 0x04001A58 RID: 6744
	private Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>> m_incomingHistory = new Map<int, List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo>>();

	// Token: 0x04001A59 RID: 6745
	private Map<int, GameRealTimeBattlefieldRaces> m_pendingRaceCountUpdates = new Map<int, GameRealTimeBattlefieldRaces>();

	// Token: 0x04001A5A RID: 6746
	private global::Entity m_oddManOutOpponentHero;

	// Token: 0x04001A5B RID: 6747
	private const int NULL_PLAYER = 0;

	// Token: 0x04001A5C RID: 6748
	private bool m_allowFakePlayerTiles;

	// Token: 0x020016D3 RID: 5843
	public enum PlayerTileEvent
	{
		// Token: 0x0400B219 RID: 45593
		TRIPLE,
		// Token: 0x0400B21A RID: 45594
		WIN_STREAK,
		// Token: 0x0400B21B RID: 45595
		TECH_LEVEL,
		// Token: 0x0400B21C RID: 45596
		RECENT_COMBAT,
		// Token: 0x0400B21D RID: 45597
		KNOCK_OUT,
		// Token: 0x0400B21E RID: 45598
		RACES,
		// Token: 0x0400B21F RID: 45599
		BANANA
	}
}
