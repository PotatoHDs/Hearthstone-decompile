using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class PlayerLeaderboardCard : HistoryItem
{
	public Color m_deadColor;

	public Color m_enemyBorderColor;

	public Color m_selfBorderColor;

	private const float RECENT_ACTION_PANEL_SCALE = 0.35f;

	private const string HISTORY_PLAYER_TILE_PREFAB = "HistoryTile_Player.prefab:8a2a1b0cd86ca4d4ba3e4b565ca00e0c";

	private const string RECENT_COMBAT_PANEL_PREFAB = "PlayerLeaderBoardRecentActionsPanel.prefab:c4b73d23d6a0cd6469360d1436ac5529";

	private const string HISTORY_MOUSEOVER_AUDIO_PREFAB = "history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5";

	private const string MAIN_ACTOR_BONE_NAME = "MainActorBone";

	private const string HERO_POWER_ACTOR_BONE_NAME = "HeroPowerActorBone";

	private const string HISTORY_ACTOR_BONE_NAME = "HistoryPanelBone";

	private const string MAIN_ACTOR_UPPER_LIMIT_BONE_NAME = "HighestMainActorZWorld";

	private const string MAIN_ACTOR_LOWER_LIMIT_BONE_NAME = "LowestMainActorZWorld";

	private readonly PlatformDependentValue<string> PLATFORM_DEPENDENT_BONE_SUFFIX = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "PC",
		Tablet = "PC",
		Phone = "Phone"
	};

	public Player m_player;

	public Entity m_playerHeroEntity;

	private Material m_fullTileMaterial;

	private bool m_mousedOver;

	private bool m_halfSize;

	private bool m_hasBeenShown;

	private bool m_isShowingOddPlayerFx;

	private bool m_gameEntityMousedOver;

	private bool m_heroNameInitialized;

	private bool m_techLevelDirty = true;

	private bool m_triplesDirty = true;

	private bool m_racesDirty = true;

	private bool m_recentCombatsDirty = true;

	private bool m_bigCardFinishedCallbackHasRun;

	private HistoryManager.BigCardFinishedCallback m_bigCardFinishedCallback;

	private bool m_bigCardCountered;

	private bool m_bigCardWaitingForSecret;

	private bool m_bigCardFromMetaData;

	private Entity m_bigCardPostTransformedEntity;

	private int m_displayTimeMS;

	private Actor m_heroPowerActor;

	private PlayerLeaderboardRecentCombatsPanel m_recentCombatsPanel;

	private List<PlayerLeaderboardInformationPanel> m_additionalInfoPanels;

	private Map<TAG_RACE, int> m_raceCounts = new Map<TAG_RACE, int>();

	private bool m_isNextOpponent;

	public PlayerLeaderboardTile m_PlayerLeaderboardTile => m_tileActor.GetComponent<PlayerLeaderboardTile>();

	public void Initialize(Entity playerHeroEntity)
	{
		m_playerHeroEntity = playerHeroEntity;
	}

	public bool HasBeenShown()
	{
		return m_hasBeenShown;
	}

	public void MarkAsShown()
	{
		if (!m_hasBeenShown)
		{
			m_hasBeenShown = true;
		}
	}

	public void SetTechLevelDirty()
	{
		m_techLevelDirty = true;
	}

	public void SetTriplesDirty()
	{
		m_triplesDirty = true;
	}

	public void SetRacesDirty()
	{
		m_racesDirty = true;
	}

	public void SetRecentCombatsDirty()
	{
		m_recentCombatsDirty = true;
	}

	public void LoadTile(HistoryTileInitInfo info)
	{
		m_entity = info.m_entity;
		m_portraitTexture = info.m_portraitTexture;
		m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		SetCardDef(info.m_cardDef);
		m_fullTileMaterial = info.m_fullTileMaterial;
		m_splatAmount = info.m_splatAmount;
		m_dead = info.m_dead;
		LoadTileImpl("HistoryTile_Player.prefab:8a2a1b0cd86ca4d4ba3e4b565ca00e0c");
	}

	public void RefreshTileVisuals(HistoryTileInitInfo info)
	{
		m_entity = info.m_entity;
		m_portraitTexture = info.m_portraitTexture;
		m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		m_fullTileMaterial = info.m_fullTileMaterial;
		SetCardDef(info.m_cardDef);
		m_splatAmount = info.m_splatAmount;
		m_dead = info.m_dead;
		RefreshTileVisuals();
	}

	public void PauseHealthUpdates()
	{
		if (m_mainCardActor is PlayerLeaderboardMainCardActor)
		{
			((PlayerLeaderboardMainCardActor)m_mainCardActor).PauseHealthUpdates();
		}
	}

	public void UpdateTileHealth()
	{
		if (m_mainCardActor is PlayerLeaderboardMainCardActor)
		{
			((PlayerLeaderboardMainCardActor)m_mainCardActor).ResumeHealthUpdates();
		}
		float value = (float)m_playerHeroEntity.GetRealTimeRemainingHP() / (float)m_playerHeroEntity.GetHealth();
		value = Mathf.Clamp01(value);
		if (value == 0f)
		{
			m_tileActor.GetMeshRenderer().GetMaterial(1).color = m_deadColor;
		}
		m_PlayerLeaderboardTile.SetCurrentHealth(value);
	}

	public void SetNextOpponentState(bool active)
	{
		m_isNextOpponent = active;
		m_PlayerLeaderboardTile.SetTilePopOutActive(active);
		UpdateOddPlayerOutFx(active);
	}

	public void UpdateOddPlayerOutFx(bool isNextOpponent)
	{
		if (!HasBeenShown())
		{
			return;
		}
		bool flag = isNextOpponent && GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BACON_ODD_PLAYER_OUT);
		if (flag && !m_isShowingOddPlayerFx)
		{
			Card card = m_playerHeroEntity.GetCard();
			if (!(card == null))
			{
				m_isShowingOddPlayerFx = true;
				Spell spell = m_tileActor.GetSpell(SpellType.BACON_ODD_PLAYER);
				spell.AddTarget(card.gameObject);
				spell.ChangeState(SpellStateType.BIRTH);
			}
		}
		else if (!flag && m_isShowingOddPlayerFx)
		{
			m_tileActor.ActivateSpellDeathState(SpellType.BACON_ODD_PLAYER);
			m_isShowingOddPlayerFx = false;
		}
	}

	public bool GetNextOpponentState()
	{
		return m_isNextOpponent;
	}

	public void SetCurrentOpponentState(bool active)
	{
		m_PlayerLeaderboardTile.SetSwordsIconActive(active);
	}

	public void SetBorderColor(bool isEnemy)
	{
		m_tileActor.GetMeshRenderer().GetMaterial().color = (isEnemy ? m_enemyBorderColor : m_selfBorderColor);
	}

	public void RefreshTileVisuals()
	{
		if (!(m_tileActor == null))
		{
			Material[] array = new Material[2]
			{
				m_tileActor.GetMeshRenderer().GetMaterial(),
				null
			};
			if (m_fullTileMaterial != null)
			{
				array[1] = m_fullTileMaterial;
				m_tileActor.GetMeshRenderer().SetMaterials(array);
			}
			else
			{
				m_tileActor.GetMeshRenderer().GetMaterial(1).mainTexture = m_portraitTexture;
			}
		}
	}

	private void LoadTileImpl(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - FAILED to load actor \"{0}\"", actorPath);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - ERROR actor \"{0}\" has no Actor component", actorPath);
			return;
		}
		m_tileActor = component;
		m_tileActor.transform.parent = base.transform;
		TransformUtil.Identity(m_tileActor.transform);
		m_tileActor.transform.localScale = PlayerLeaderboardManager.Get().transform.localScale;
		RefreshTileVisuals();
		Renderer[] componentsInChildren = m_tileActor.GetMeshRenderer().GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		m_tileActor.GetMeshRenderer().GetMaterial(1).color = Board.Get().m_HistoryTileColor;
		UpdateTileHealth();
		int playerId = GameState.Get().GetFriendlySidePlayer().GetPlayerId();
		int num = m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (num == 0)
		{
			num = m_playerHeroEntity.GetTag(GAME_TAG.CONTROLLER);
		}
		SetBorderColor(num != playerId);
		m_PlayerLeaderboardTile.SetOwnerId(num);
	}

	public void NotifyMousedOver()
	{
		if (!m_mousedOver && !(this == HistoryManager.Get().GetCurrentBigCard()))
		{
			m_mousedOver = true;
			if (PlayerLeaderboardManager.Get().IsNewlyMousedOver())
			{
				SoundManager.Get().LoadAndPlay("history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5", m_tileActor.gameObject);
			}
			if (!m_mainCardActor)
			{
				LoadMainCardActor();
				LoadRecentCombatsPanel();
				SceneUtils.SetLayer(m_mainCardActor, GameLayer.Tooltip);
				SceneUtils.SetLayer(m_recentCombatsPanel, GameLayer.Tooltip);
			}
			ShowTile();
		}
	}

	public void NotifyMousedOut()
	{
		if (m_mousedOver)
		{
			m_mousedOver = false;
			if (m_gameEntityMousedOver)
			{
				GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOut();
				m_gameEntityMousedOver = false;
			}
			TooltipPanelManager.Get().HideKeywordHelp();
			if ((bool)m_mainCardActor)
			{
				m_mainCardActor.ActivateAllSpellsDeathStates();
				m_mainCardActor.Hide();
			}
			if ((bool)m_heroPowerActor)
			{
				m_heroPowerActor.ActivateAllSpellsDeathStates();
				m_heroPowerActor.Hide();
			}
			if ((bool)m_recentCombatsPanel)
			{
				m_recentCombatsPanel.gameObject.SetActive(value: false);
			}
		}
	}

	public void LoadRecentCombatsPanel()
	{
		string text = "PlayerLeaderBoardRecentActionsPanel.prefab:c4b73d23d6a0cd6469360d1436ac5529";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadRecentCombatsPanel() - FAILED to load GameObject \"{0}\"", text);
			return;
		}
		m_recentCombatsPanel = gameObject.GetComponent<PlayerLeaderboardRecentCombatsPanel>();
		if (m_recentCombatsPanel == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadRecentCombatsPanel() - ERROR GameObject \"{0}\" has no PlayerLeaderboardRecentCombatsPanel component", text);
			return;
		}
		m_recentCombatsPanel.transform.parent = base.transform;
		TransformUtil.Identity(m_recentCombatsPanel.transform);
		TransformUtil.SetLocalScaleX(m_recentCombatsPanel.gameObject, 0.35f);
		TransformUtil.SetLocalScaleY(m_recentCombatsPanel.gameObject, 0.35f);
		TransformUtil.SetLocalScaleZ(m_recentCombatsPanel.gameObject, 0.35f);
	}

	public void RefreshMainCardActor()
	{
		if (!(m_mainCardActor == null))
		{
			m_mainCardActor.SetCardDefFromEntity(m_entity);
			m_mainCardActor.SetPremium(m_entity.GetPremiumType());
			m_mainCardActor.SetWatermarkCardSetOverride(m_entity.GetWatermarkCardSetOverride());
			m_mainCardActor.SetHistoryItem(this);
			m_mainCardActor.UpdateAllComponents();
			m_mainCardActor.GetAttackObject().Hide();
		}
	}

	public void LoadMainCardActor()
	{
		string text = "Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0";
		string text2 = "History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(text2, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadMainCardActor() - FAILED to load actor \"{0}\"", text);
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		Actor component2 = gameObject2.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadMainCardActor() - ERROR actor \"{0}\" has no Actor component", text);
			return;
		}
		m_mainCardActor = component;
		m_heroPowerActor = component2;
		RefreshMainCardActor();
		if (component2 != null)
		{
			SceneUtils.SetLayer(component2, GameLayer.Tooltip);
			component2.Hide();
			SetHeroPower(m_entity);
		}
	}

	public void SetHeroPower(Entity hero)
	{
		if (m_heroPowerActor == null)
		{
			return;
		}
		Player controller = hero.GetController();
		if (controller.GetHero() == hero)
		{
			Entity heroPower = controller.GetHeroPower();
			if (heroPower != null)
			{
				using DefLoader.DisposableCardDef cardDef = heroPower.ShareDisposableCardDef();
				SetHeroPower(heroPower, cardDef, heroPower.GetEntityDef());
			}
			return;
		}
		Entity entity = null;
		if (hero.HasTag(GAME_TAG.HERO_POWER_ENTITY))
		{
			entity = GameState.Get().GetEntity(hero.GetTag(GAME_TAG.HERO_POWER_ENTITY));
		}
		if (entity != null)
		{
			using (DefLoader.DisposableCardDef cardDef2 = entity.ShareDisposableCardDef())
			{
				SetHeroPower(entity, cardDef2, entity.GetEntityDef());
			}
			return;
		}
		string text = GameUtils.GetHeroPowerCardIdFromHero(hero.GetCardId());
		if (hero.HasTag(GAME_TAG.HERO_POWER))
		{
			text = GameUtils.TranslateDbIdToCardId(hero.GetTag(GAME_TAG.HERO_POWER));
		}
		if (text != null)
		{
			using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(text);
			SetHeroPower(null, disposableFullDef?.DisposableCardDef, disposableFullDef?.EntityDef);
		}
	}

	private void SetHeroPower(Entity entity, DefLoader.DisposableCardDef cardDef, EntityDef entityDef)
	{
		if (!(m_heroPowerActor == null))
		{
			m_heroPowerActor.SetEntity(entity);
			m_heroPowerActor.SetCardDef(cardDef);
			m_heroPowerActor.SetEntityDef(entityDef);
			if (entity == null)
			{
				m_heroPowerActor.SetPremium(m_entity.GetPremiumType());
			}
			m_heroPowerActor.transform.parent = base.transform;
			TransformUtil.Identity(m_heroPowerActor.transform);
			m_heroPowerActor.UpdateAllComponents();
			if (m_mousedOver)
			{
				UpdateHoverStatePosition();
			}
		}
	}

	private void ShowTile()
	{
		if (!m_mousedOver)
		{
			m_mainCardActor.Hide();
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.Hide();
			}
			if (m_recentCombatsPanel != null)
			{
				m_recentCombatsPanel.gameObject.SetActive(value: false);
			}
			return;
		}
		m_mainCardActor.Show();
		if (GameState.Get().GetGameEntity() is TB_BaconShop_Tutorial)
		{
			if (m_recentCombatsPanel != null)
			{
				m_recentCombatsPanel.gameObject.SetActive(value: false);
			}
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.Hide();
				if (!m_heroPowerActor.UseCoinManaGem())
				{
				}
			}
		}
		else
		{
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.Show();
			}
			if (m_recentCombatsPanel != null)
			{
				m_recentCombatsPanel.gameObject.SetActive(value: true);
			}
		}
		InitializeMainCardActor();
		m_mainCardActor.SetActorState(ActorStateType.CARD_IDLE);
		DisplaySpells();
		if (!m_heroNameInitialized)
		{
			RefreshMainCardName();
			m_heroNameInitialized = true;
		}
		UpdateHoverStatePosition();
		if (m_recentCombatsDirty)
		{
			UpdateRecentCombats();
			m_recentCombatsDirty = false;
		}
		if (m_techLevelDirty)
		{
			UpdateTechLevel();
			m_techLevelDirty = false;
		}
		if (m_triplesDirty)
		{
			UpdateTriples();
			m_triplesDirty = false;
		}
		if (m_racesDirty)
		{
			m_racesDirty = !UpdateRaces();
		}
	}

	private void UpdateHoverStatePosition()
	{
		GameObject gameObject = m_tileActor.FindBone("MainActorBone" + PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject2 = m_tileActor.FindBone("HeroPowerActorBone" + PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject3 = m_tileActor.FindBone("HistoryPanelBone" + PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject4 = m_tileActor.FindBone("HighestMainActorZWorld" + PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject5 = m_tileActor.FindBone("LowestMainActorZWorld" + PLATFORM_DEPENDENT_BONE_SUFFIX);
		m_mainCardActor.transform.position = new Vector3(base.transform.position.x + gameObject.transform.localPosition.x, base.transform.position.y + gameObject.transform.localPosition.y, GetZForThisTilesMouseOverCard(base.transform.position.z, gameObject.transform.localPosition.z, gameObject4.transform.localPosition.z, gameObject5.transform.localPosition.z));
		m_mainCardActor.transform.localScale = gameObject.transform.localScale;
		if (m_heroPowerActor != null && m_heroPowerActor.IsShown())
		{
			m_heroPowerActor.transform.position = new Vector3(base.transform.position.x + gameObject2.transform.localPosition.x, base.transform.position.y + gameObject2.transform.localPosition.y, m_mainCardActor.transform.position.z + gameObject2.transform.localPosition.z);
			m_heroPowerActor.transform.localScale = gameObject2.transform.localScale;
			if (m_heroPowerActor.UseCoinManaGem())
			{
				m_heroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
			}
		}
		if (m_recentCombatsPanel != null)
		{
			m_recentCombatsPanel.transform.position = new Vector3(base.transform.position.x + gameObject3.transform.localPosition.x, base.transform.position.y + gameObject3.transform.localPosition.y, m_mainCardActor.transform.position.z + gameObject3.transform.localPosition.z);
			m_recentCombatsPanel.transform.localScale = gameObject3.transform.localScale;
		}
	}

	private float GetZForThisTilesMouseOverCard(float tileZPosition, float desiredZOffset, float globalTop, float globalBottom)
	{
		if (tileZPosition + desiredZOffset > globalTop)
		{
			return globalTop;
		}
		if (tileZPosition + desiredZOffset < globalBottom)
		{
			return globalBottom;
		}
		return tileZPosition + desiredZOffset;
	}

	private void UpdateTechLevel()
	{
		if (!(m_recentCombatsPanel == null))
		{
			int value = 1;
			int key = m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(key) && GameState.Get().GetPlayerInfoMap()[key].GetPlayerHero() != null)
			{
				value = GameState.Get().GetPlayerInfoMap()[key].GetPlayerHero().GetRealTimePlayerTechLevel();
			}
			value = Mathf.Clamp(value, 1, 6);
			m_recentCombatsPanel.SetTechLevel(value);
		}
	}

	private void UpdateTriples()
	{
		if (!(m_recentCombatsPanel == null))
		{
			int triples = 0;
			int key = m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(key) && GameState.Get().GetPlayerInfoMap()[key].GetPlayerHero() != null)
			{
				triples = GameState.Get().GetPlayerInfoMap()[key].GetPlayerHero().GetTag(GAME_TAG.PLAYER_TRIPLES);
			}
			m_recentCombatsPanel.SetTriples(triples);
		}
	}

	private bool UpdateRaces()
	{
		if (m_recentCombatsPanel == null)
		{
			return false;
		}
		return m_recentCombatsPanel.SetRaces(m_raceCounts);
	}

	public string GetHeroName()
	{
		if (m_playerHeroEntity != null)
		{
			return m_playerHeroEntity.GetName();
		}
		return null;
	}

	public void RefreshMainCardName()
	{
		if (!(m_mainCardActor == null))
		{
			PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = m_mainCardActor as PlayerLeaderboardMainCardActor;
			int playerId = m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
			playerLeaderboardMainCardActor.UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(playerId));
			if (playerLeaderboardMainCardActor.GetEntity() != null && !Options.Get().GetBool(Option.STREAMER_MODE))
			{
				playerLeaderboardMainCardActor.UpdateAlternateNameText(playerLeaderboardMainCardActor.GetEntity().GetName());
			}
			else
			{
				playerLeaderboardMainCardActor.SetAlternateNameTextActive(active: false);
			}
		}
	}

	internal void UpdateRacesCount(List<GameRealTimeRaceCount> races)
	{
		foreach (GameRealTimeRaceCount race2 in races)
		{
			TAG_RACE race = (TAG_RACE)race2.Race;
			if (!m_raceCounts.ContainsKey(race))
			{
				m_raceCounts.Add(race, 0);
			}
			m_raceCounts[race] = race2.Count;
		}
		m_racesDirty = true;
	}

	public void RefreshRecentCombats()
	{
		if (m_recentCombatsPanel == null)
		{
			return;
		}
		int playerId = m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo> recentCombatHistoryForPlayer = PlayerLeaderboardManager.Get().GetRecentCombatHistoryForPlayer(playerId);
		m_recentCombatsPanel.ClearRecentCombats();
		if (recentCombatHistoryForPlayer != null)
		{
			int num = (int)Math.Max(0L, recentCombatHistoryForPlayer.Count - (m_recentCombatsPanel.m_maxDisplayItems + 1));
			int count = recentCombatHistoryForPlayer.Count;
			for (int i = num; i < count; i++)
			{
				m_recentCombatsPanel.AddRecentCombat(this, recentCombatHistoryForPlayer[i]);
			}
		}
	}

	public void UpdateRecentCombats()
	{
		if (m_recentCombatsPanel == null)
		{
			LoadRecentCombatsPanel();
			SceneUtils.SetLayer(m_recentCombatsPanel, GameLayer.Tooltip);
		}
		RefreshRecentCombats();
	}

	public float GetPoppedOutBoneX()
	{
		GameObject gameObject = m_tileActor.FindBone("PoppedOutBone");
		if (gameObject == null)
		{
			return 0f;
		}
		return gameObject.transform.localPosition.x;
	}
}
