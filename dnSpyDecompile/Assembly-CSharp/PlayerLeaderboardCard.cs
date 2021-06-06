using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class PlayerLeaderboardCard : HistoryItem
{
	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000EEE1F File Offset: 0x000ED01F
	public PlayerLeaderboardTile m_PlayerLeaderboardTile
	{
		get
		{
			return this.m_tileActor.GetComponent<PlayerLeaderboardTile>();
		}
	}

	// Token: 0x06002EED RID: 12013 RVA: 0x000EEE2C File Offset: 0x000ED02C
	public void Initialize(global::Entity playerHeroEntity)
	{
		this.m_playerHeroEntity = playerHeroEntity;
	}

	// Token: 0x06002EEE RID: 12014 RVA: 0x000EEE35 File Offset: 0x000ED035
	public bool HasBeenShown()
	{
		return this.m_hasBeenShown;
	}

	// Token: 0x06002EEF RID: 12015 RVA: 0x000EEE3D File Offset: 0x000ED03D
	public void MarkAsShown()
	{
		if (this.m_hasBeenShown)
		{
			return;
		}
		this.m_hasBeenShown = true;
	}

	// Token: 0x06002EF0 RID: 12016 RVA: 0x000EEE4F File Offset: 0x000ED04F
	public void SetTechLevelDirty()
	{
		this.m_techLevelDirty = true;
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x000EEE58 File Offset: 0x000ED058
	public void SetTriplesDirty()
	{
		this.m_triplesDirty = true;
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x000EEE61 File Offset: 0x000ED061
	public void SetRacesDirty()
	{
		this.m_racesDirty = true;
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x000EEE6A File Offset: 0x000ED06A
	public void SetRecentCombatsDirty()
	{
		this.m_recentCombatsDirty = true;
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x000EEE74 File Offset: 0x000ED074
	public void LoadTile(HistoryTileInitInfo info)
	{
		this.m_entity = info.m_entity;
		this.m_portraitTexture = info.m_portraitTexture;
		this.m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		base.SetCardDef(info.m_cardDef);
		this.m_fullTileMaterial = info.m_fullTileMaterial;
		this.m_splatAmount = info.m_splatAmount;
		this.m_dead = info.m_dead;
		this.LoadTileImpl("HistoryTile_Player.prefab:8a2a1b0cd86ca4d4ba3e4b565ca00e0c");
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x000EEEE0 File Offset: 0x000ED0E0
	public void RefreshTileVisuals(HistoryTileInitInfo info)
	{
		this.m_entity = info.m_entity;
		this.m_portraitTexture = info.m_portraitTexture;
		this.m_portraitGoldenMaterial = info.m_portraitGoldenMaterial;
		this.m_fullTileMaterial = info.m_fullTileMaterial;
		base.SetCardDef(info.m_cardDef);
		this.m_splatAmount = info.m_splatAmount;
		this.m_dead = info.m_dead;
		this.RefreshTileVisuals();
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x000EEF47 File Offset: 0x000ED147
	public void PauseHealthUpdates()
	{
		if (this.m_mainCardActor is PlayerLeaderboardMainCardActor)
		{
			((PlayerLeaderboardMainCardActor)this.m_mainCardActor).PauseHealthUpdates();
		}
	}

	// Token: 0x06002EF7 RID: 12023 RVA: 0x000EEF68 File Offset: 0x000ED168
	public void UpdateTileHealth()
	{
		if (this.m_mainCardActor is PlayerLeaderboardMainCardActor)
		{
			((PlayerLeaderboardMainCardActor)this.m_mainCardActor).ResumeHealthUpdates();
		}
		float num = (float)this.m_playerHeroEntity.GetRealTimeRemainingHP() / (float)this.m_playerHeroEntity.GetHealth();
		num = Mathf.Clamp01(num);
		if (num == 0f)
		{
			this.m_tileActor.GetMeshRenderer(false).GetMaterial(1).color = this.m_deadColor;
		}
		this.m_PlayerLeaderboardTile.SetCurrentHealth(num);
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x000EEFE5 File Offset: 0x000ED1E5
	public void SetNextOpponentState(bool active)
	{
		this.m_isNextOpponent = active;
		this.m_PlayerLeaderboardTile.SetTilePopOutActive(active);
		this.UpdateOddPlayerOutFx(active);
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x000EF004 File Offset: 0x000ED204
	public void UpdateOddPlayerOutFx(bool isNextOpponent)
	{
		if (!this.HasBeenShown())
		{
			return;
		}
		bool flag = isNextOpponent && GameState.Get().GetFriendlySidePlayer().HasTag(GAME_TAG.BACON_ODD_PLAYER_OUT);
		if (!flag || this.m_isShowingOddPlayerFx)
		{
			if (!flag && this.m_isShowingOddPlayerFx)
			{
				this.m_tileActor.ActivateSpellDeathState(SpellType.BACON_ODD_PLAYER);
				this.m_isShowingOddPlayerFx = false;
			}
			return;
		}
		Card card = this.m_playerHeroEntity.GetCard();
		if (card == null)
		{
			return;
		}
		this.m_isShowingOddPlayerFx = true;
		Spell spell = this.m_tileActor.GetSpell(SpellType.BACON_ODD_PLAYER);
		spell.AddTarget(card.gameObject);
		spell.ChangeState(SpellStateType.BIRTH);
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x000EF0A2 File Offset: 0x000ED2A2
	public bool GetNextOpponentState()
	{
		return this.m_isNextOpponent;
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x000EF0AA File Offset: 0x000ED2AA
	public void SetCurrentOpponentState(bool active)
	{
		this.m_PlayerLeaderboardTile.SetSwordsIconActive(active);
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x000EF0B8 File Offset: 0x000ED2B8
	public void SetBorderColor(bool isEnemy)
	{
		this.m_tileActor.GetMeshRenderer(false).GetMaterial().color = (isEnemy ? this.m_enemyBorderColor : this.m_selfBorderColor);
	}

	// Token: 0x06002EFD RID: 12029 RVA: 0x000EF0E4 File Offset: 0x000ED2E4
	public void RefreshTileVisuals()
	{
		if (this.m_tileActor == null)
		{
			return;
		}
		Material[] array = new Material[2];
		array[0] = this.m_tileActor.GetMeshRenderer(false).GetMaterial();
		if (this.m_fullTileMaterial != null)
		{
			array[1] = this.m_fullTileMaterial;
			this.m_tileActor.GetMeshRenderer(false).SetMaterials(array);
			return;
		}
		this.m_tileActor.GetMeshRenderer(false).GetMaterial(1).mainTexture = this.m_portraitTexture;
	}

	// Token: 0x06002EFE RID: 12030 RVA: 0x000EF164 File Offset: 0x000ED364
	private void LoadTileImpl(string actorPath)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(actorPath, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - FAILED to load actor \"{0}\"", new object[]
			{
				actorPath
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("HistoryCard.LoadTileImpl() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				actorPath
			});
			return;
		}
		this.m_tileActor = component;
		this.m_tileActor.transform.parent = base.transform;
		TransformUtil.Identity(this.m_tileActor.transform);
		this.m_tileActor.transform.localScale = PlayerLeaderboardManager.Get().transform.localScale;
		this.RefreshTileVisuals();
		foreach (Renderer renderer in this.m_tileActor.GetMeshRenderer(false).GetComponentsInChildren<Renderer>())
		{
			if (!(renderer.tag == "FakeShadow"))
			{
				renderer.GetMaterial().color = Board.Get().m_HistoryTileColor;
			}
		}
		this.m_tileActor.GetMeshRenderer(false).GetMaterial(1).color = Board.Get().m_HistoryTileColor;
		this.UpdateTileHealth();
		int playerId = GameState.Get().GetFriendlySidePlayer().GetPlayerId();
		int tag = this.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (tag == 0)
		{
			tag = this.m_playerHeroEntity.GetTag(GAME_TAG.CONTROLLER);
		}
		this.SetBorderColor(tag != playerId);
		this.m_PlayerLeaderboardTile.SetOwnerId(tag);
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x000EF2E0 File Offset: 0x000ED4E0
	public void NotifyMousedOver()
	{
		if (this.m_mousedOver)
		{
			return;
		}
		if (this == HistoryManager.Get().GetCurrentBigCard())
		{
			return;
		}
		this.m_mousedOver = true;
		if (PlayerLeaderboardManager.Get().IsNewlyMousedOver())
		{
			SoundManager.Get().LoadAndPlay("history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5", this.m_tileActor.gameObject);
		}
		if (!this.m_mainCardActor)
		{
			this.LoadMainCardActor();
			this.LoadRecentCombatsPanel();
			SceneUtils.SetLayer(this.m_mainCardActor, GameLayer.Tooltip);
			SceneUtils.SetLayer(this.m_recentCombatsPanel, GameLayer.Tooltip);
		}
		this.ShowTile();
	}

	// Token: 0x06002F00 RID: 12032 RVA: 0x000EF374 File Offset: 0x000ED574
	public void NotifyMousedOut()
	{
		if (!this.m_mousedOver)
		{
			return;
		}
		this.m_mousedOver = false;
		if (this.m_gameEntityMousedOver)
		{
			GameState.Get().GetGameEntity().NotifyOfHistoryTokenMousedOut();
			this.m_gameEntityMousedOver = false;
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		if (this.m_mainCardActor)
		{
			this.m_mainCardActor.ActivateAllSpellsDeathStates();
			this.m_mainCardActor.Hide();
		}
		if (this.m_heroPowerActor)
		{
			this.m_heroPowerActor.ActivateAllSpellsDeathStates();
			this.m_heroPowerActor.Hide();
		}
		if (this.m_recentCombatsPanel)
		{
			this.m_recentCombatsPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002F01 RID: 12033 RVA: 0x000EF420 File Offset: 0x000ED620
	public void LoadRecentCombatsPanel()
	{
		string text = "PlayerLeaderBoardRecentActionsPanel.prefab:c4b73d23d6a0cd6469360d1436ac5529";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadRecentCombatsPanel() - FAILED to load GameObject \"{0}\"", new object[]
			{
				text
			});
			return;
		}
		this.m_recentCombatsPanel = gameObject.GetComponent<PlayerLeaderboardRecentCombatsPanel>();
		if (this.m_recentCombatsPanel == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadRecentCombatsPanel() - ERROR GameObject \"{0}\" has no PlayerLeaderboardRecentCombatsPanel component", new object[]
			{
				text
			});
			return;
		}
		this.m_recentCombatsPanel.transform.parent = base.transform;
		TransformUtil.Identity(this.m_recentCombatsPanel.transform);
		TransformUtil.SetLocalScaleX(this.m_recentCombatsPanel.gameObject, 0.35f);
		TransformUtil.SetLocalScaleY(this.m_recentCombatsPanel.gameObject, 0.35f);
		TransformUtil.SetLocalScaleZ(this.m_recentCombatsPanel.gameObject, 0.35f);
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x000EF4F8 File Offset: 0x000ED6F8
	public void RefreshMainCardActor()
	{
		if (this.m_mainCardActor == null)
		{
			return;
		}
		this.m_mainCardActor.SetCardDefFromEntity(this.m_entity);
		this.m_mainCardActor.SetPremium(this.m_entity.GetPremiumType());
		this.m_mainCardActor.SetWatermarkCardSetOverride(this.m_entity.GetWatermarkCardSetOverride());
		this.m_mainCardActor.SetHistoryItem(this);
		this.m_mainCardActor.UpdateAllComponents();
		this.m_mainCardActor.GetAttackObject().Hide();
	}

	// Token: 0x06002F03 RID: 12035 RVA: 0x000EF578 File Offset: 0x000ED778
	public void LoadMainCardActor()
	{
		string text = "Bacon_Leaderboard_Hero.prefab:776977f5238a24647adcd67933f7d4b0";
		string input = "History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadMainCardActor() - FAILED to load actor \"{0}\"", new object[]
			{
				text
			});
			return;
		}
		Actor component = gameObject.GetComponent<Actor>();
		Actor component2 = gameObject2.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardCard.LoadMainCardActor() - ERROR actor \"{0}\" has no Actor component", new object[]
			{
				text
			});
			return;
		}
		this.m_mainCardActor = component;
		this.m_heroPowerActor = component2;
		this.RefreshMainCardActor();
		if (component2 != null)
		{
			SceneUtils.SetLayer(component2, GameLayer.Tooltip);
			component2.Hide();
			this.SetHeroPower(this.m_entity);
		}
	}

	// Token: 0x06002F04 RID: 12036 RVA: 0x000EF640 File Offset: 0x000ED840
	public void SetHeroPower(global::Entity hero)
	{
		if (this.m_heroPowerActor == null)
		{
			return;
		}
		global::Player controller = hero.GetController();
		if (controller.GetHero() == hero)
		{
			global::Entity heroPower = controller.GetHeroPower();
			if (heroPower == null)
			{
				return;
			}
			using (DefLoader.DisposableCardDef disposableCardDef = heroPower.ShareDisposableCardDef())
			{
				this.SetHeroPower(heroPower, disposableCardDef, heroPower.GetEntityDef());
				return;
			}
		}
		global::Entity entity = null;
		if (hero.HasTag(GAME_TAG.HERO_POWER_ENTITY))
		{
			entity = GameState.Get().GetEntity(hero.GetTag(GAME_TAG.HERO_POWER_ENTITY));
		}
		if (entity != null)
		{
			using (DefLoader.DisposableCardDef disposableCardDef2 = entity.ShareDisposableCardDef())
			{
				this.SetHeroPower(entity, disposableCardDef2, entity.GetEntityDef());
				return;
			}
		}
		string text = GameUtils.GetHeroPowerCardIdFromHero(hero.GetCardId());
		if (hero.HasTag(GAME_TAG.HERO_POWER))
		{
			text = GameUtils.TranslateDbIdToCardId(hero.GetTag(GAME_TAG.HERO_POWER), false);
		}
		if (text != null)
		{
			using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(text, null))
			{
				this.SetHeroPower(null, (fullDef != null) ? fullDef.DisposableCardDef : null, (fullDef != null) ? fullDef.EntityDef : null);
			}
		}
	}

	// Token: 0x06002F05 RID: 12037 RVA: 0x000EF784 File Offset: 0x000ED984
	private void SetHeroPower(global::Entity entity, DefLoader.DisposableCardDef cardDef, EntityDef entityDef)
	{
		if (this.m_heroPowerActor == null)
		{
			return;
		}
		this.m_heroPowerActor.SetEntity(entity);
		this.m_heroPowerActor.SetCardDef(cardDef);
		this.m_heroPowerActor.SetEntityDef(entityDef);
		if (entity == null)
		{
			this.m_heroPowerActor.SetPremium(this.m_entity.GetPremiumType());
		}
		this.m_heroPowerActor.transform.parent = base.transform;
		TransformUtil.Identity(this.m_heroPowerActor.transform);
		this.m_heroPowerActor.UpdateAllComponents();
		if (this.m_mousedOver)
		{
			this.UpdateHoverStatePosition();
		}
	}

	// Token: 0x06002F06 RID: 12038 RVA: 0x000EF81C File Offset: 0x000EDA1C
	private void ShowTile()
	{
		if (!this.m_mousedOver)
		{
			this.m_mainCardActor.Hide();
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.Hide();
			}
			if (this.m_recentCombatsPanel != null)
			{
				this.m_recentCombatsPanel.gameObject.SetActive(false);
			}
			return;
		}
		this.m_mainCardActor.Show();
		if (GameState.Get().GetGameEntity() is TB_BaconShop_Tutorial)
		{
			if (this.m_recentCombatsPanel != null)
			{
				this.m_recentCombatsPanel.gameObject.SetActive(false);
			}
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.Hide();
				if (this.m_heroPowerActor.UseCoinManaGem())
				{
				}
			}
		}
		else
		{
			if (this.m_heroPowerActor != null)
			{
				this.m_heroPowerActor.Show();
			}
			if (this.m_recentCombatsPanel != null)
			{
				this.m_recentCombatsPanel.gameObject.SetActive(true);
			}
		}
		base.InitializeMainCardActor();
		this.m_mainCardActor.SetActorState(ActorStateType.CARD_IDLE);
		base.DisplaySpells();
		if (!this.m_heroNameInitialized)
		{
			this.RefreshMainCardName();
			this.m_heroNameInitialized = true;
		}
		this.UpdateHoverStatePosition();
		if (this.m_recentCombatsDirty)
		{
			this.UpdateRecentCombats();
			this.m_recentCombatsDirty = false;
		}
		if (this.m_techLevelDirty)
		{
			this.UpdateTechLevel();
			this.m_techLevelDirty = false;
		}
		if (this.m_triplesDirty)
		{
			this.UpdateTriples();
			this.m_triplesDirty = false;
		}
		if (this.m_racesDirty)
		{
			this.m_racesDirty = !this.UpdateRaces();
		}
	}

	// Token: 0x06002F07 RID: 12039 RVA: 0x000EF99C File Offset: 0x000EDB9C
	private void UpdateHoverStatePosition()
	{
		GameObject gameObject = this.m_tileActor.FindBone("MainActorBone" + this.PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject2 = this.m_tileActor.FindBone("HeroPowerActorBone" + this.PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject3 = this.m_tileActor.FindBone("HistoryPanelBone" + this.PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject4 = this.m_tileActor.FindBone("HighestMainActorZWorld" + this.PLATFORM_DEPENDENT_BONE_SUFFIX);
		GameObject gameObject5 = this.m_tileActor.FindBone("LowestMainActorZWorld" + this.PLATFORM_DEPENDENT_BONE_SUFFIX);
		this.m_mainCardActor.transform.position = new Vector3(base.transform.position.x + gameObject.transform.localPosition.x, base.transform.position.y + gameObject.transform.localPosition.y, this.GetZForThisTilesMouseOverCard(base.transform.position.z, gameObject.transform.localPosition.z, gameObject4.transform.localPosition.z, gameObject5.transform.localPosition.z));
		this.m_mainCardActor.transform.localScale = gameObject.transform.localScale;
		if (this.m_heroPowerActor != null && this.m_heroPowerActor.IsShown())
		{
			this.m_heroPowerActor.transform.position = new Vector3(base.transform.position.x + gameObject2.transform.localPosition.x, base.transform.position.y + gameObject2.transform.localPosition.y, this.m_mainCardActor.transform.position.z + gameObject2.transform.localPosition.z);
			this.m_heroPowerActor.transform.localScale = gameObject2.transform.localScale;
			if (this.m_heroPowerActor.UseCoinManaGem())
			{
				this.m_heroPowerActor.ActivateSpellBirthState(SpellType.COIN_MANA_GEM);
			}
		}
		if (this.m_recentCombatsPanel != null)
		{
			this.m_recentCombatsPanel.transform.position = new Vector3(base.transform.position.x + gameObject3.transform.localPosition.x, base.transform.position.y + gameObject3.transform.localPosition.y, this.m_mainCardActor.transform.position.z + gameObject3.transform.localPosition.z);
			this.m_recentCombatsPanel.transform.localScale = gameObject3.transform.localScale;
		}
	}

	// Token: 0x06002F08 RID: 12040 RVA: 0x000EFC88 File Offset: 0x000EDE88
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

	// Token: 0x06002F09 RID: 12041 RVA: 0x000EFCA0 File Offset: 0x000EDEA0
	private void UpdateTechLevel()
	{
		if (this.m_recentCombatsPanel == null)
		{
			return;
		}
		int num = 1;
		int tag = this.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag) && GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero() != null)
		{
			num = GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero().GetRealTimePlayerTechLevel();
		}
		num = Mathf.Clamp(num, 1, 6);
		this.m_recentCombatsPanel.SetTechLevel(num);
	}

	// Token: 0x06002F0A RID: 12042 RVA: 0x000EFD28 File Offset: 0x000EDF28
	private void UpdateTriples()
	{
		if (this.m_recentCombatsPanel == null)
		{
			return;
		}
		int triples = 0;
		int tag = this.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag) && GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero() != null)
		{
			triples = GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero().GetTag(GAME_TAG.PLAYER_TRIPLES);
		}
		this.m_recentCombatsPanel.SetTriples(triples);
	}

	// Token: 0x06002F0B RID: 12043 RVA: 0x000EFDA9 File Offset: 0x000EDFA9
	private bool UpdateRaces()
	{
		return !(this.m_recentCombatsPanel == null) && this.m_recentCombatsPanel.SetRaces(this.m_raceCounts);
	}

	// Token: 0x06002F0C RID: 12044 RVA: 0x000EFDCC File Offset: 0x000EDFCC
	public string GetHeroName()
	{
		if (this.m_playerHeroEntity != null)
		{
			return this.m_playerHeroEntity.GetName();
		}
		return null;
	}

	// Token: 0x06002F0D RID: 12045 RVA: 0x000EFDE4 File Offset: 0x000EDFE4
	public void RefreshMainCardName()
	{
		if (this.m_mainCardActor == null)
		{
			return;
		}
		PlayerLeaderboardMainCardActor playerLeaderboardMainCardActor = this.m_mainCardActor as PlayerLeaderboardMainCardActor;
		int tag = this.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		playerLeaderboardMainCardActor.UpdatePlayerNameText(GameState.Get().GetGameEntity().GetBestNameForPlayer(tag));
		if (playerLeaderboardMainCardActor.GetEntity() != null && !Options.Get().GetBool(global::Option.STREAMER_MODE))
		{
			playerLeaderboardMainCardActor.UpdateAlternateNameText(playerLeaderboardMainCardActor.GetEntity().GetName());
			return;
		}
		playerLeaderboardMainCardActor.SetAlternateNameTextActive(false);
	}

	// Token: 0x06002F0E RID: 12046 RVA: 0x000EFE60 File Offset: 0x000EE060
	internal void UpdateRacesCount(List<GameRealTimeRaceCount> races)
	{
		foreach (GameRealTimeRaceCount gameRealTimeRaceCount in races)
		{
			TAG_RACE race = (TAG_RACE)gameRealTimeRaceCount.Race;
			if (!this.m_raceCounts.ContainsKey(race))
			{
				this.m_raceCounts.Add(race, 0);
			}
			this.m_raceCounts[race] = gameRealTimeRaceCount.Count;
		}
		this.m_racesDirty = true;
	}

	// Token: 0x06002F0F RID: 12047 RVA: 0x000EFEE4 File Offset: 0x000EE0E4
	public void RefreshRecentCombats()
	{
		if (this.m_recentCombatsPanel == null)
		{
			return;
		}
		int tag = this.m_playerHeroEntity.GetTag(GAME_TAG.PLAYER_ID);
		List<PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo> recentCombatHistoryForPlayer = PlayerLeaderboardManager.Get().GetRecentCombatHistoryForPlayer(tag);
		this.m_recentCombatsPanel.ClearRecentCombats();
		if (recentCombatHistoryForPlayer != null)
		{
			int num = (int)Math.Max(0L, (long)recentCombatHistoryForPlayer.Count - (long)((ulong)(this.m_recentCombatsPanel.m_maxDisplayItems + 1U)));
			int count = recentCombatHistoryForPlayer.Count;
			for (int i = num; i < count; i++)
			{
				this.m_recentCombatsPanel.AddRecentCombat(this, recentCombatHistoryForPlayer[i]);
			}
		}
	}

	// Token: 0x06002F10 RID: 12048 RVA: 0x000EFF6B File Offset: 0x000EE16B
	public void UpdateRecentCombats()
	{
		if (this.m_recentCombatsPanel == null)
		{
			this.LoadRecentCombatsPanel();
			SceneUtils.SetLayer(this.m_recentCombatsPanel, GameLayer.Tooltip);
		}
		this.RefreshRecentCombats();
	}

	// Token: 0x06002F11 RID: 12049 RVA: 0x000EFF94 File Offset: 0x000EE194
	public float GetPoppedOutBoneX()
	{
		GameObject gameObject = this.m_tileActor.FindBone("PoppedOutBone");
		if (gameObject == null)
		{
			return 0f;
		}
		return gameObject.transform.localPosition.x;
	}

	// Token: 0x04001A1C RID: 6684
	public Color m_deadColor;

	// Token: 0x04001A1D RID: 6685
	public Color m_enemyBorderColor;

	// Token: 0x04001A1E RID: 6686
	public Color m_selfBorderColor;

	// Token: 0x04001A1F RID: 6687
	private const float RECENT_ACTION_PANEL_SCALE = 0.35f;

	// Token: 0x04001A20 RID: 6688
	private const string HISTORY_PLAYER_TILE_PREFAB = "HistoryTile_Player.prefab:8a2a1b0cd86ca4d4ba3e4b565ca00e0c";

	// Token: 0x04001A21 RID: 6689
	private const string RECENT_COMBAT_PANEL_PREFAB = "PlayerLeaderBoardRecentActionsPanel.prefab:c4b73d23d6a0cd6469360d1436ac5529";

	// Token: 0x04001A22 RID: 6690
	private const string HISTORY_MOUSEOVER_AUDIO_PREFAB = "history_event_mouseover.prefab:0bc4f1638257a264a9b02e811c0a61b5";

	// Token: 0x04001A23 RID: 6691
	private const string MAIN_ACTOR_BONE_NAME = "MainActorBone";

	// Token: 0x04001A24 RID: 6692
	private const string HERO_POWER_ACTOR_BONE_NAME = "HeroPowerActorBone";

	// Token: 0x04001A25 RID: 6693
	private const string HISTORY_ACTOR_BONE_NAME = "HistoryPanelBone";

	// Token: 0x04001A26 RID: 6694
	private const string MAIN_ACTOR_UPPER_LIMIT_BONE_NAME = "HighestMainActorZWorld";

	// Token: 0x04001A27 RID: 6695
	private const string MAIN_ACTOR_LOWER_LIMIT_BONE_NAME = "LowestMainActorZWorld";

	// Token: 0x04001A28 RID: 6696
	private readonly PlatformDependentValue<string> PLATFORM_DEPENDENT_BONE_SUFFIX = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "PC",
		Tablet = "PC",
		Phone = "Phone"
	};

	// Token: 0x04001A29 RID: 6697
	public global::Player m_player;

	// Token: 0x04001A2A RID: 6698
	public global::Entity m_playerHeroEntity;

	// Token: 0x04001A2B RID: 6699
	private Material m_fullTileMaterial;

	// Token: 0x04001A2C RID: 6700
	private bool m_mousedOver;

	// Token: 0x04001A2D RID: 6701
	private bool m_halfSize;

	// Token: 0x04001A2E RID: 6702
	private bool m_hasBeenShown;

	// Token: 0x04001A2F RID: 6703
	private bool m_isShowingOddPlayerFx;

	// Token: 0x04001A30 RID: 6704
	private bool m_gameEntityMousedOver;

	// Token: 0x04001A31 RID: 6705
	private bool m_heroNameInitialized;

	// Token: 0x04001A32 RID: 6706
	private bool m_techLevelDirty = true;

	// Token: 0x04001A33 RID: 6707
	private bool m_triplesDirty = true;

	// Token: 0x04001A34 RID: 6708
	private bool m_racesDirty = true;

	// Token: 0x04001A35 RID: 6709
	private bool m_recentCombatsDirty = true;

	// Token: 0x04001A36 RID: 6710
	private bool m_bigCardFinishedCallbackHasRun;

	// Token: 0x04001A37 RID: 6711
	private HistoryManager.BigCardFinishedCallback m_bigCardFinishedCallback;

	// Token: 0x04001A38 RID: 6712
	private bool m_bigCardCountered;

	// Token: 0x04001A39 RID: 6713
	private bool m_bigCardWaitingForSecret;

	// Token: 0x04001A3A RID: 6714
	private bool m_bigCardFromMetaData;

	// Token: 0x04001A3B RID: 6715
	private global::Entity m_bigCardPostTransformedEntity;

	// Token: 0x04001A3C RID: 6716
	private int m_displayTimeMS;

	// Token: 0x04001A3D RID: 6717
	private Actor m_heroPowerActor;

	// Token: 0x04001A3E RID: 6718
	private PlayerLeaderboardRecentCombatsPanel m_recentCombatsPanel;

	// Token: 0x04001A3F RID: 6719
	private List<PlayerLeaderboardInformationPanel> m_additionalInfoPanels;

	// Token: 0x04001A40 RID: 6720
	private Map<TAG_RACE, int> m_raceCounts = new Map<TAG_RACE, int>();

	// Token: 0x04001A41 RID: 6721
	private bool m_isNextOpponent;
}
