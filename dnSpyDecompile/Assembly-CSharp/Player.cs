using System;
using System.Collections.Generic;
using bgs;
using PegasusClient;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class Player : Entity
{
	// Token: 0x06002E75 RID: 11893 RVA: 0x000ECEA9 File Offset: 0x000EB0A9
	public static Player.Side GetOppositePlayerSide(Player.Side side)
	{
		if (side == Player.Side.FRIENDLY)
		{
			return Player.Side.OPPOSING;
		}
		if (side == Player.Side.OPPOSING)
		{
			return Player.Side.FRIENDLY;
		}
		return side;
	}

	// Token: 0x06002E76 RID: 11894 RVA: 0x000ECEB8 File Offset: 0x000EB0B8
	public void OnShuffleDeck()
	{
		ZoneDeck deckZone = this.GetDeckZone();
		if (deckZone == null)
		{
			return;
		}
		deckZone.UpdateLayout();
		Actor activeThickness = deckZone.GetActiveThickness();
		if (activeThickness == null)
		{
			return;
		}
		activeThickness.ActivateSpellBirthState(SpellType.SHUFFLE_DECK);
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x000ECEFC File Offset: 0x000EB0FC
	public void InitPlayer(Network.HistCreateGame.PlayerData netPlayer)
	{
		this.SetPlayerId(netPlayer.ID);
		this.SetGameAccountId(netPlayer.GameAccountId);
		this.SetCardBackId(netPlayer.CardBackID);
		base.SetTags(netPlayer.Player.Tags);
		this.InitRealTimeValues(netPlayer.Player.Tags);
		if (this.IsLocalUser())
		{
			foreach (Network.Entity.Tag tag3 in netPlayer.Player.Tags)
			{
				if (tag3.Name == 1048)
				{
					GameMgr.Get().LastGameData.WhizbangDeckID = tag3.Value;
				}
			}
		}
		Network.Entity.Tag tag2 = netPlayer.Player.Tags.Find((Network.Entity.Tag tag) => tag.Name == 994);
		if (tag2 != null)
		{
			this.GetOrCreateMarkOfEvilCounter().OnMarksChanged(tag2.Value);
		}
		GameState.Get().RegisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x000ED018 File Offset: 0x000EB218
	public override bool HasValidDisplayName()
	{
		return !string.IsNullOrEmpty(this.m_name);
	}

	// Token: 0x06002E79 RID: 11897 RVA: 0x000ED028 File Offset: 0x000EB228
	public override string GetName()
	{
		return this.m_name;
	}

	// Token: 0x06002E7A RID: 11898 RVA: 0x000ED030 File Offset: 0x000EB230
	public MedalInfoTranslator GetRank()
	{
		return this.m_medalInfo;
	}

	// Token: 0x06002E7B RID: 11899 RVA: 0x000ED038 File Offset: 0x000EB238
	public override string GetDebugName()
	{
		if (this.m_name != null)
		{
			return this.m_name;
		}
		if (this.IsAI())
		{
			return GameStrings.Get("GAMEPLAY_AI_OPPONENT_NAME");
		}
		return "UNKNOWN HUMAN PLAYER";
	}

	// Token: 0x06002E7C RID: 11900 RVA: 0x000ED061 File Offset: 0x000EB261
	public void SetName(string name)
	{
		this.m_name = name;
	}

	// Token: 0x06002E7D RID: 11901 RVA: 0x000ED06C File Offset: 0x000EB26C
	public void SetGameAccountId(BnetGameAccountId id)
	{
		this.m_gameAccountId = id;
		this.UpdateLocal();
		if (this.IsDisplayable())
		{
			this.UpdateDisplayInfo();
			return;
		}
		this.UpdateRank();
		this.UpdateSessionRecord();
		if (this.IsBnetPlayer())
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnBnetPlayersChanged));
			if (!BnetFriendMgr.Get().IsFriend(this.m_gameAccountId))
			{
				GameUtils.RequestPlayerPresence(this.m_gameAccountId);
			}
		}
	}

	// Token: 0x06002E7E RID: 11902 RVA: 0x000ED0DD File Offset: 0x000EB2DD
	public bool IsLocalUser()
	{
		return this.m_local;
	}

	// Token: 0x06002E7F RID: 11903 RVA: 0x000ED0E5 File Offset: 0x000EB2E5
	public void SetLocalUser(bool local)
	{
		this.m_local = local;
	}

	// Token: 0x06002E80 RID: 11904 RVA: 0x000ED0EE File Offset: 0x000EB2EE
	public bool IsAI()
	{
		return GameUtils.IsAIPlayer(this.m_gameAccountId);
	}

	// Token: 0x06002E81 RID: 11905 RVA: 0x000ED0FB File Offset: 0x000EB2FB
	public bool IsHuman()
	{
		return GameUtils.IsHumanPlayer(this.m_gameAccountId);
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x000ED108 File Offset: 0x000EB308
	public bool IsBnetPlayer()
	{
		return GameUtils.IsBnetPlayer(this.m_gameAccountId);
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x000ED115 File Offset: 0x000EB315
	public bool IsGuestPlayer()
	{
		return GameUtils.IsGuestPlayer(this.m_gameAccountId);
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x000ED122 File Offset: 0x000EB322
	public Player.Side GetSide()
	{
		return this.m_side;
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x000ED12A File Offset: 0x000EB32A
	public bool IsFriendlySide()
	{
		return this.m_side == Player.Side.FRIENDLY;
	}

	// Token: 0x06002E86 RID: 11910 RVA: 0x000ED135 File Offset: 0x000EB335
	public bool IsOpposingSide()
	{
		return this.m_side == Player.Side.OPPOSING;
	}

	// Token: 0x06002E87 RID: 11911 RVA: 0x000ED140 File Offset: 0x000EB340
	public int TotalSpellpower(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		int num = 0;
		switch (spellSchool)
		{
		case TAG_SPELL_SCHOOL.ARCANE:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_ARCANE);
			break;
		case TAG_SPELL_SCHOOL.FIRE:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_FIRE);
			break;
		case TAG_SPELL_SCHOOL.FROST:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_FROST);
			break;
		case TAG_SPELL_SCHOOL.NATURE:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_NATURE);
			break;
		case TAG_SPELL_SCHOOL.HOLY:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_HOLY);
			break;
		case TAG_SPELL_SCHOOL.SHADOW:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_SHADOW);
			break;
		case TAG_SPELL_SCHOOL.FEL:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_FEL);
			break;
		case TAG_SPELL_SCHOOL.PHYSICAL_COMBAT:
			num += base.GetTag(GAME_TAG.CURRENT_SPELLPOWER_PHYSICAL);
			break;
		}
		return base.GetTag(GAME_TAG.CURRENT_SPELLPOWER) - base.GetTag(GAME_TAG.CURRENT_NEGATIVE_SPELLPOWER) + num;
	}

	// Token: 0x06002E88 RID: 11912 RVA: 0x000ED210 File Offset: 0x000EB410
	public new bool IsRevealed()
	{
		return this.IsFriendlySide() || SpectatorManager.Get().IsSpectatingPlayer(this.m_gameAccountId) || base.HasTag(GAME_TAG.ZONES_REVEALED);
	}

	// Token: 0x06002E89 RID: 11913 RVA: 0x000ED240 File Offset: 0x000EB440
	public void SetSide(Player.Side side)
	{
		this.m_side = side;
	}

	// Token: 0x06002E8A RID: 11914 RVA: 0x000ED249 File Offset: 0x000EB449
	public int GetCardBackId()
	{
		return this.m_cardBackId;
	}

	// Token: 0x06002E8B RID: 11915 RVA: 0x000ED251 File Offset: 0x000EB451
	public void SetCardBackId(int id)
	{
		this.m_cardBackId = id;
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x000ED25A File Offset: 0x000EB45A
	public int GetPlayerId()
	{
		return base.GetTag(GAME_TAG.PLAYER_ID);
	}

	// Token: 0x06002E8D RID: 11917 RVA: 0x000ED264 File Offset: 0x000EB464
	public void SetPlayerId(int playerId)
	{
		base.SetTag(GAME_TAG.PLAYER_ID, playerId);
	}

	// Token: 0x06002E8E RID: 11918 RVA: 0x000ED26F File Offset: 0x000EB46F
	public int GetTeamId()
	{
		return base.GetTag(GAME_TAG.TEAM_ID);
	}

	// Token: 0x06002E8F RID: 11919 RVA: 0x000ED279 File Offset: 0x000EB479
	public bool IsTeamLeader()
	{
		return this.GetPlayerId() == this.GetTeamId();
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x000ED28C File Offset: 0x000EB48C
	public List<string> GetSecretDefinitions()
	{
		List<string> list = new List<string>();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneSecret && zone.m_Side == Player.Side.FRIENDLY)
			{
				foreach (Card card in zone.GetCards())
				{
					if (card.GetEntity().IsSecret())
					{
						list.Add(card.GetEntity().GetCardId());
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x000ED350 File Offset: 0x000EB550
	public List<string> GetQuestDefinitions()
	{
		List<string> list = new List<string>();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneSecret && zone.m_Side == Player.Side.FRIENDLY)
			{
				foreach (Card card in zone.GetCards())
				{
					if (card.GetEntity().IsQuest())
					{
						list.Add(card.GetEntity().GetCardId());
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x000ED414 File Offset: 0x000EB614
	public bool IsCurrentPlayer()
	{
		return base.HasTag(GAME_TAG.CURRENT_PLAYER);
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x000ED41E File Offset: 0x000EB61E
	public bool IsComboActive()
	{
		return base.HasTag(GAME_TAG.COMBO_ACTIVE);
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x000ED42B File Offset: 0x000EB62B
	public bool IsRealTimeComboActive()
	{
		return this.m_realTimeComboActive;
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x000ED433 File Offset: 0x000EB633
	public void SetRealTimeComboActive(int tagValue)
	{
		this.SetRealTimeComboActive(tagValue == 1);
	}

	// Token: 0x06002E96 RID: 11926 RVA: 0x000ED443 File Offset: 0x000EB643
	public void SetRealTimeComboActive(bool active)
	{
		this.m_realTimeComboActive = active;
	}

	// Token: 0x06002E97 RID: 11927 RVA: 0x000ED44C File Offset: 0x000EB64C
	public void SetRealTimeSpellsCostHealth(int value)
	{
		this.m_realTimeSpellsCostHealth = (value > 0);
	}

	// Token: 0x06002E98 RID: 11928 RVA: 0x000ED45C File Offset: 0x000EB65C
	public bool GetRealTimeSpellsCostHealth()
	{
		return this.m_realTimeSpellsCostHealth;
	}

	// Token: 0x06002E99 RID: 11929 RVA: 0x000ED464 File Offset: 0x000EB664
	public override void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		base.InitRealTimeValues(tags);
		foreach (Network.Entity.Tag tag in tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name != GAME_TAG.COMBO_ACTIVE)
			{
				if (name != GAME_TAG.TEMP_RESOURCES)
				{
					if (name == GAME_TAG.SPELLS_COST_HEALTH)
					{
						this.SetRealTimeSpellsCostHealth(tag.Value);
					}
				}
				else
				{
					this.SetRealTimeTempMana(tag.Value);
				}
			}
			else
			{
				this.SetRealTimeComboActive(tag.Value);
			}
		}
	}

	// Token: 0x06002E9A RID: 11930 RVA: 0x000ED4FC File Offset: 0x000EB6FC
	public int GetNumAvailableResources()
	{
		int tag = base.GetTag(GAME_TAG.TEMP_RESOURCES);
		int tag2 = base.GetTag(GAME_TAG.RESOURCES);
		int tag3 = base.GetTag(GAME_TAG.RESOURCES_USED);
		int num = tag2 + tag - tag3 - this.m_queuedSpentMana - this.m_usedTempMana;
		if (num >= 0)
		{
			return num;
		}
		return 0;
	}

	// Token: 0x06002E9B RID: 11931 RVA: 0x000ED540 File Offset: 0x000EB740
	public bool HasWeapon()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneWeapon && zone.m_Side == this.m_side)
			{
				return zone.GetCards().Count > 0;
			}
		}
		return false;
	}

	// Token: 0x06002E9C RID: 11932 RVA: 0x000ED5BC File Offset: 0x000EB7BC
	public void SetHero(Entity hero)
	{
		this.m_hero = hero;
		if (this.ShouldUseHeroName())
		{
			this.UpdateDisplayInfo();
		}
		foreach (Card card in this.GetHandZone().GetCards())
		{
			if (card.GetEntity().IsMultiClass())
			{
				card.UpdateActorComponents();
			}
		}
		if (this.IsFriendlySide())
		{
			GameState.Get().FireHeroChangedEvent(this);
		}
	}

	// Token: 0x06002E9D RID: 11933 RVA: 0x000ED648 File Offset: 0x000EB848
	public Entity GetStartingHero()
	{
		Entity entity = this.GetHero();
		if (entity == null)
		{
			return entity;
		}
		while (entity.HasTag(GAME_TAG.LINKED_ENTITY))
		{
			int tag = entity.GetTag(GAME_TAG.LINKED_ENTITY);
			Entity entity2 = GameState.Get().GetEntity(tag);
			if (entity2 == null || !entity2.IsHero())
			{
				global::Log.Gameplay.PrintError("Player.GetStartingHero() - Hero entity {0} has a LINKED_ENTITY tag value of {1} which corresponds to invalid Entity {2}.", new object[]
				{
					entity,
					tag,
					entity2
				});
				break;
			}
			entity = entity2;
		}
		return entity;
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x000ED6BC File Offset: 0x000EB8BC
	public override Entity GetHero()
	{
		return this.m_hero;
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x000ED6C4 File Offset: 0x000EB8C4
	public EntityDef GetHeroEntityDef()
	{
		if (this.m_hero == null)
		{
			return null;
		}
		EntityDef entityDef = this.m_hero.GetEntityDef();
		if (entityDef == null)
		{
			return null;
		}
		return entityDef;
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x000ED6ED File Offset: 0x000EB8ED
	public override Card GetHeroCard()
	{
		if (this.m_hero == null)
		{
			return null;
		}
		return this.m_hero.GetCard();
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x000ED704 File Offset: 0x000EB904
	public void SetHeroPower(Entity heroPower)
	{
		this.m_heroPower = heroPower;
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x000ED70D File Offset: 0x000EB90D
	public override Entity GetHeroPower()
	{
		return this.m_heroPower;
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x000ED715 File Offset: 0x000EB915
	public override Card GetHeroPowerCard()
	{
		if (this.m_heroPower == null)
		{
			return null;
		}
		return this.m_heroPower.GetCard();
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x000ED72C File Offset: 0x000EB92C
	public bool IsHeroPowerAffectedByBonusDamage()
	{
		Card heroPowerCard = this.GetHeroPowerCard();
		if (heroPowerCard == null)
		{
			return false;
		}
		Entity entity = heroPowerCard.GetEntity();
		return entity.IsHeroPower() && entity.GetCardTextBuilder().ContainsBonusDamageToken(entity);
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x000ED768 File Offset: 0x000EB968
	public override Card GetWeaponCard()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(this.GetSide()).GetFirstCard();
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x000ED77F File Offset: 0x000EB97F
	public ZoneHand GetHandZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneHand>(this.GetSide());
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x000ED791 File Offset: 0x000EB991
	public ZonePlay GetBattlefieldZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(this.GetSide());
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x000ED7A3 File Offset: 0x000EB9A3
	public ZoneDeck GetDeckZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneDeck>(this.GetSide());
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x000ED7B5 File Offset: 0x000EB9B5
	public ZoneGraveyard GetGraveyardZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneGraveyard>(this.GetSide());
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x000ED7C7 File Offset: 0x000EB9C7
	public ZoneSecret GetSecretZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneSecret>(this.GetSide());
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x000ED7D9 File Offset: 0x000EB9D9
	public ZoneHero GetHeroZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneHero>(this.GetSide());
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x000ED7EC File Offset: 0x000EB9EC
	public bool HasReadyAttackers()
	{
		List<Card> cards = this.GetBattlefieldZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			if (GameState.Get().HasResponse(cards[i].GetEntity()))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x000ED834 File Offset: 0x000EBA34
	public bool HasATauntMinion()
	{
		List<Card> cards = this.GetBattlefieldZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			if (cards[i].GetEntity().HasTaunt())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x000ED874 File Offset: 0x000EBA74
	public uint GetArenaWins()
	{
		return this.m_arenaWins;
	}

	// Token: 0x06002EAF RID: 11951 RVA: 0x000ED87C File Offset: 0x000EBA7C
	public uint GetArenaLosses()
	{
		return this.m_arenaLoss;
	}

	// Token: 0x06002EB0 RID: 11952 RVA: 0x000ED884 File Offset: 0x000EBA84
	public uint GetTavernBrawlWins()
	{
		return this.m_tavernBrawlWins;
	}

	// Token: 0x06002EB1 RID: 11953 RVA: 0x000ED88C File Offset: 0x000EBA8C
	public uint GetTavernBrawlLosses()
	{
		return this.m_tavernBrawlLoss;
	}

	// Token: 0x06002EB2 RID: 11954 RVA: 0x000ED894 File Offset: 0x000EBA94
	public uint GetDuelsWins()
	{
		return this.m_duelsWins;
	}

	// Token: 0x06002EB3 RID: 11955 RVA: 0x000ED89C File Offset: 0x000EBA9C
	public uint GetDuelsLosses()
	{
		return this.m_duelsLoss;
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x000ED8A4 File Offset: 0x000EBAA4
	public void PlayConcedeEmote()
	{
		if (this.m_concedeEmotePlayed)
		{
			return;
		}
		Card heroCard = this.GetHeroCard();
		if (heroCard == null)
		{
			return;
		}
		heroCard.PlayEmote(EmoteType.CONCEDE);
		this.m_concedeEmotePlayed = true;
	}

	// Token: 0x06002EB5 RID: 11957 RVA: 0x000ED8DA File Offset: 0x000EBADA
	public BnetGameAccountId GetGameAccountId()
	{
		return this.m_gameAccountId;
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x000ED8E2 File Offset: 0x000EBAE2
	public BnetPlayer GetBnetPlayer()
	{
		return BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x000ED8F4 File Offset: 0x000EBAF4
	public bool IsDisplayable()
	{
		if (this.m_gameAccountId == null)
		{
			return false;
		}
		if (!this.IsBnetPlayer())
		{
			return !this.ShouldUseHeroName() || this.GetHeroEntityDef() != null;
		}
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
		if (player == null)
		{
			return false;
		}
		if (!player.IsDisplayable())
		{
			return false;
		}
		if (GameUtils.IsGameTypeRanked())
		{
			BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
			if (hearthstoneGameAccount == null)
			{
				return false;
			}
			if (!hearthstoneGameAccount.HasGameField(18U))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002EB8 RID: 11960 RVA: 0x000ED974 File Offset: 0x000EBB74
	public void WipeZzzs()
	{
		foreach (Card card in this.GetBattlefieldZone().GetCards())
		{
			Spell actorSpell = card.GetActorSpell(SpellType.Zzz, true);
			if (!(actorSpell == null))
			{
				actorSpell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	// Token: 0x06002EB9 RID: 11961 RVA: 0x000ED9E0 File Offset: 0x000EBBE0
	public TAG_PLAYSTATE GetPreGameOverPlayState()
	{
		return this.m_preGameOverPlayState;
	}

	// Token: 0x06002EBA RID: 11962 RVA: 0x000ED9E8 File Offset: 0x000EBBE8
	public bool HasSeenStartOfGameSpell(EntityDef entityDef)
	{
		return this.m_seenStartOfGameSpells.Contains(entityDef);
	}

	// Token: 0x06002EBB RID: 11963 RVA: 0x000ED9F6 File Offset: 0x000EBBF6
	public void MarkStartOfGameSpellAsSeen(EntityDef entityDef)
	{
		this.m_seenStartOfGameSpells.Add(entityDef);
	}

	// Token: 0x06002EBC RID: 11964 RVA: 0x000EDA05 File Offset: 0x000EBC05
	public void AddManaCrystal(int numCrystals, bool isTurnStart)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().AddManaCrystals(numCrystals, isTurnStart);
	}

	// Token: 0x06002EBD RID: 11965 RVA: 0x000EDA1C File Offset: 0x000EBC1C
	public void AddManaCrystal(int numCrystals)
	{
		this.AddManaCrystal(numCrystals, false);
	}

	// Token: 0x06002EBE RID: 11966 RVA: 0x000EDA26 File Offset: 0x000EBC26
	public void DestroyManaCrystal(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().DestroyManaCrystals(numCrystals);
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x000EDA3C File Offset: 0x000EBC3C
	public void AddTempManaCrystal(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().AddTempManaCrystals(numCrystals);
	}

	// Token: 0x06002EC0 RID: 11968 RVA: 0x000EDA52 File Offset: 0x000EBC52
	public void DestroyTempManaCrystal(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().DestroyTempManaCrystals(numCrystals);
	}

	// Token: 0x06002EC1 RID: 11969 RVA: 0x000EDA68 File Offset: 0x000EBC68
	public void SpendManaCrystal(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().SpendManaCrystals(numCrystals);
	}

	// Token: 0x06002EC2 RID: 11970 RVA: 0x000EDA7E File Offset: 0x000EBC7E
	public void ReadyManaCrystal(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().ReadyManaCrystals(numCrystals);
	}

	// Token: 0x06002EC3 RID: 11971 RVA: 0x000EDA94 File Offset: 0x000EBC94
	public void HandleSameTurnOverloadChanged(int crystalsChanged)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().HandleSameTurnOverloadChanged(crystalsChanged);
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x000EDAAA File Offset: 0x000EBCAA
	public void UnlockCrystals(int numCrystals)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().UnlockCrystals(numCrystals);
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x000EDAC0 File Offset: 0x000EBCC0
	public void CancelAllProposedMana(Entity entity)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().CancelAllProposedMana(entity);
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x000EDAD6 File Offset: 0x000EBCD6
	public void ProposeManaCrystalUsage(Entity entity)
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().ProposeManaCrystalUsage(entity);
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x000EDAEC File Offset: 0x000EBCEC
	public void ResetUnresolvedManaToBeReadied()
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		ManaCrystalMgr.Get().ResetUnresolvedManaToBeReadied();
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x000EDB01 File Offset: 0x000EBD01
	public void UpdateManaCounter()
	{
		if (this.m_manaCounter == null)
		{
			return;
		}
		this.m_manaCounter.UpdateText();
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x000EDB1D File Offset: 0x000EBD1D
	public void NotifyOfSpentMana(int spentMana)
	{
		this.m_queuedSpentMana += spentMana;
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x000EDB2D File Offset: 0x000EBD2D
	public void NotifyOfUsedTempMana(int usedMana)
	{
		this.m_usedTempMana += usedMana;
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x000EDB3D File Offset: 0x000EBD3D
	public int GetQueuedUsedTempMana()
	{
		return this.m_usedTempMana;
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x000EDB45 File Offset: 0x000EBD45
	public int GetQueuedSpentMana()
	{
		return this.m_queuedSpentMana;
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x000EDB4D File Offset: 0x000EBD4D
	public void SetRealTimeTempMana(int tempMana)
	{
		this.m_realtimeTempMana = tempMana;
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x000EDB56 File Offset: 0x000EBD56
	public int GetRealTimeTempMana()
	{
		return this.m_realtimeTempMana;
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x000EDB5E File Offset: 0x000EBD5E
	public void OnBoardLoaded()
	{
		this.AssignPlayerBoardObjects();
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x000EDB68 File Offset: 0x000EBD68
	public override void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		GAME_TAG tag = (GAME_TAG)change.Tag;
		if (tag <= GAME_TAG.COMBO_ACTIVE)
		{
			if (tag != GAME_TAG.PLAYSTATE)
			{
				if (tag != GAME_TAG.COMBO_ACTIVE)
				{
					return;
				}
				this.SetRealTimeComboActive(change.Value);
				return;
			}
			else
			{
				TAG_PLAYSTATE value = (TAG_PLAYSTATE)change.Value;
				if (GameUtils.IsPreGameOverPlayState(value))
				{
					this.m_preGameOverPlayState = value;
					return;
				}
			}
		}
		else
		{
			if (tag == GAME_TAG.TEMP_RESOURCES)
			{
				this.SetRealTimeTempMana(change.Value);
				return;
			}
			if (tag != GAME_TAG.SPELLS_COST_HEALTH)
			{
				return;
			}
			this.SetRealTimeSpellsCostHealth(change.Value);
		}
	}

	// Token: 0x06002ED1 RID: 11985 RVA: 0x000EDBE0 File Offset: 0x000EBDE0
	public override void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			this.OnTagChanged(change);
		}
	}

	// Token: 0x06002ED2 RID: 11986 RVA: 0x000EDC10 File Offset: 0x000EBE10
	public override void OnTagChanged(TagDelta change)
	{
		if (this.IsFriendlySide())
		{
			this.OnFriendlyPlayerTagChanged(change);
		}
		else
		{
			this.OnOpposingPlayerTagChanged(change);
		}
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag <= GAME_TAG.IGNORE_TAUNT)
		{
			if (tag <= GAME_TAG.MULLIGAN_STATE)
			{
				if (tag <= GAME_TAG.RESOURCES)
				{
					if (tag != GAME_TAG.PLAYSTATE)
					{
						if (tag != GAME_TAG.CURRENT_PLAYER)
						{
							if (tag - GAME_TAG.RESOURCES_USED > 1)
							{
								return;
							}
						}
						else
						{
							if (change.newValue != 1 || !GameState.Get().IsLocalSidePlayerTurn())
							{
								return;
							}
							ManaCrystalMgr.Get().OnCurrentPlayerChanged();
							this.m_queuedSpentMana = 0;
							if (GameState.Get().IsMainPhase())
							{
								TurnStartManager.Get().BeginListeningForTurnEvents(false);
								return;
							}
							return;
						}
					}
					else
					{
						if (change.newValue == 8)
						{
							this.PlayConcedeEmote();
							return;
						}
						return;
					}
				}
				else if (tag != GAME_TAG.COMBO_ACTIVE)
				{
					if (tag != GAME_TAG.TEMP_RESOURCES)
					{
						if (tag != GAME_TAG.MULLIGAN_STATE)
						{
							return;
						}
						if (change.newValue == 4 && MulliganManager.Get() != null)
						{
							MulliganManager.Get().ServerHasDealtReplacementCards(this.IsFriendlySide());
							return;
						}
						return;
					}
				}
				else
				{
					foreach (Card card in this.GetHandZone().GetCards())
					{
						card.UpdateActorState(false);
					}
					Entity heroPower = this.GetHeroPower();
					if (heroPower != null)
					{
						heroPower.GetCard().UpdateActorState(false);
						return;
					}
					return;
				}
				if (!GameState.Get().IsTurnStartManagerActive() || !this.IsFriendlySide())
				{
					this.UpdateManaCounter();
				}
				GameState.Get().UpdateOptionHighlights();
				return;
			}
			if (tag <= GAME_TAG.LOCK_AND_LOAD)
			{
				if (tag == GAME_TAG.STEADY_SHOT_CAN_TARGET)
				{
					Card heroPowerCard = this.GetHeroPowerCard();
					this.ToggleActorSpellOnCard(heroPowerCard, change, SpellType.STEADY_SHOT_CAN_TARGET);
					return;
				}
				if (tag != GAME_TAG.CURRENT_HEROPOWER_DAMAGE_BONUS)
				{
					if (tag != GAME_TAG.LOCK_AND_LOAD)
					{
						return;
					}
					Card heroCard = this.GetHeroCard();
					this.ToggleActorSpellOnCard(heroCard, change, SpellType.LOCK_AND_LOAD);
					return;
				}
				else
				{
					if (this.IsHeroPowerAffectedByBonusDamage())
					{
						Card heroPowerCard2 = this.GetHeroPowerCard();
						this.ToggleActorSpellOnCard(heroPowerCard2, change, SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
						return;
					}
					return;
				}
			}
			else
			{
				if (tag == GAME_TAG.CHOOSE_BOTH)
				{
					this.UpdateChooseBoth();
					return;
				}
				if (tag == GAME_TAG.SPELLS_COST_HEALTH)
				{
					this.UpdateSpellsCostHealth(change);
					return;
				}
				if (tag != GAME_TAG.IGNORE_TAUNT)
				{
					return;
				}
				using (List<Card>.Enumerator enumerator = GameState.Get().GetFirstOpponentPlayer(base.GetController()).GetBattlefieldZone().GetCards().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Card card2 = enumerator.Current;
						if (card2.CanShowActorVisuals())
						{
							Entity entity = card2.GetEntity();
							if (entity != null && entity.HasTaunt())
							{
								Actor actor = card2.GetActor();
								if (!(actor == null))
								{
									actor.ActivateTaunt();
								}
							}
						}
					}
					return;
				}
			}
		}
		else if (tag <= GAME_TAG.HERO_POWER_DISABLED)
		{
			if (tag <= GAME_TAG.STAMPEDE)
			{
				if (tag == GAME_TAG.EMBRACE_THE_SHADOW)
				{
					Card heroCard2 = this.GetHeroCard();
					this.ToggleActorSpellOnCard(heroCard2, change, SpellType.EMBRACE_THE_SHADOW);
					return;
				}
				if (tag == GAME_TAG.DEATH_KNIGHT)
				{
					Card heroCard3 = this.GetHeroCard();
					this.ToggleActorSpellOnCard(heroCard3, change, SpellType.DEATH_KNIGHT);
					return;
				}
				if (tag != GAME_TAG.STAMPEDE)
				{
					return;
				}
				Card heroCard4 = this.GetHeroCard();
				this.ToggleActorSpellOnCard(heroCard4, change, SpellType.STAMPEDE);
				return;
			}
			else
			{
				if (tag == GAME_TAG.IS_VAMPIRE)
				{
					Card heroCard5 = this.GetHeroCard();
					this.ToggleActorSpellOnCard(heroCard5, change, SpellType.IS_VAMPIRE);
					return;
				}
				if (tag - GAME_TAG.OVERRIDE_EMOTE_0 > 5)
				{
					if (tag != GAME_TAG.HERO_POWER_DISABLED)
					{
						return;
					}
					Card heroPowerCard3 = this.GetHeroPowerCard();
					if (heroPowerCard3 != null && heroPowerCard3.GetEntity() != null && heroPowerCard3.GetEntity().GetTag(GAME_TAG.EXHAUSTED) == 0)
					{
						heroPowerCard3.HandleCardExhaustedTagChanged(change);
						return;
					}
					return;
				}
				else
				{
					if (EmoteHandler.Get() != null)
					{
						EmoteHandler.Get().ChangeAvailableEmotes();
						return;
					}
					return;
				}
			}
		}
		else if (tag <= GAME_TAG.WHIZBANG_DECK_ID)
		{
			if (tag == GAME_TAG.MARK_OF_EVIL)
			{
				this.GetOrCreateMarkOfEvilCounter().OnMarksChanged(change.newValue);
				return;
			}
			if (tag == GAME_TAG.GLORIOUSGLOOP)
			{
				Card heroCard6 = this.GetHeroCard();
				this.ToggleActorSpellOnCard(heroCard6, change, SpellType.GLORIOUSGLOOP);
				return;
			}
			if (tag != GAME_TAG.WHIZBANG_DECK_ID)
			{
				return;
			}
			if (this.IsLocalUser())
			{
				GameMgr.Get().LastGameData.WhizbangDeckID = change.newValue;
				return;
			}
			return;
		}
		else if (tag <= GAME_TAG.SPELLS_CAST_TWICE)
		{
			if (tag != GAME_TAG.DECK_POWER_UP)
			{
				if (tag != GAME_TAG.SPELLS_CAST_TWICE)
				{
					return;
				}
				Card heroCard7 = this.GetHeroCard();
				this.ToggleActorSpellOnCard(heroCard7, change, SpellType.SPELLS_CAST_TWICE);
				return;
			}
			else
			{
				Card heroCard8 = this.GetHeroCard();
				Spell spell = this.ToggleActorSpellOnCard(heroCard8, change, SpellType.DECK_POWER_UP);
				if (spell != null && this.GetHeroCard() != null && this.GetHeroCard().gameObject != null)
				{
					spell.SetSource(this.GetHeroCard().gameObject);
					spell.ForceUpdateTransform();
					return;
				}
				return;
			}
		}
		else if (tag != GAME_TAG.PROGRESSBAR_SHOW)
		{
			if (tag != GAME_TAG.CANT_TRIGGER_DEATHRATTLE)
			{
				return;
			}
		}
		else
		{
			if (change.newValue == 1)
			{
				AssetLoader.Get().InstantiatePrefab("PopUpProgressBar.prefab:1e74ef51d3388674792ddf7d6233f5d7", AssetLoadingOptions.None).GetComponent<PopUpController>().Populate(base.GetTag(GAME_TAG.PROGRESSBAR_PROGRESS), base.GetTag(GAME_TAG.PROGRESSBAR_TOTAL), base.GetTag(GAME_TAG.PROGRESSBAR_CARDID));
				return;
			}
			return;
		}
		ZonePlay battlefieldZone = this.GetBattlefieldZone();
		if (battlefieldZone)
		{
			List<Card> cards = battlefieldZone.GetCards();
			for (int i = 0; i < cards.Count; i++)
			{
				Card card3 = cards[i];
				if (card3.CanShowActorVisuals())
				{
					Entity entity2 = card3.GetEntity();
					if (entity2 != null && entity2.HasDeathrattle())
					{
						card3.ToggleDeathrattle(change.newValue == 0);
					}
				}
			}
		}
		Card weaponCard = this.GetWeaponCard();
		if (weaponCard != null)
		{
			Entity entity3 = weaponCard.GetEntity();
			if (entity3 != null && entity3.HasDeathrattle())
			{
				weaponCard.ToggleDeathrattle(change.newValue == 0);
			}
		}
	}

	// Token: 0x06002ED3 RID: 11987 RVA: 0x000EE1EC File Offset: 0x000EC3EC
	public MarkOfEvilCounter GetOrCreateMarkOfEvilCounter()
	{
		if (this.m_markOfEvilCounter == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MarkOfEvilCounter.prefab:ff08f2e19826b354bb37bb25bf81471d", AssetLoadingOptions.IgnorePrefabPosition);
			this.m_markOfEvilCounter = gameObject.GetComponent<MarkOfEvilCounter>();
			string name = (this.GetSide() == Player.Side.FRIENDLY) ? "MarkOfEvil" : "MarkOfEvil_Opponent";
			Transform source = Board.Get().FindBone(name);
			TransformUtil.CopyWorld(gameObject, source);
		}
		return this.m_markOfEvilCounter;
	}

	// Token: 0x06002ED4 RID: 11988 RVA: 0x000EE258 File Offset: 0x000EC458
	private void OnFriendlyPlayerTagChanged(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag > GAME_TAG.SPELL_HEALING_DOUBLE)
		{
			if (tag <= GAME_TAG.CURRENT_NEGATIVE_SPELLPOWER)
			{
				if (tag <= GAME_TAG.JADE_GOLEM)
				{
					if (tag != GAME_TAG.OVERLOAD_LOCKED)
					{
						if (tag != GAME_TAG.JADE_GOLEM)
						{
							return;
						}
					}
					else
					{
						if (change.newValue < change.oldValue && !GameState.Get().IsTurnStartManagerActive())
						{
							this.UnlockCrystals(change.oldValue - change.newValue);
							return;
						}
						return;
					}
				}
				else
				{
					if (tag == GAME_TAG.RED_MANA_CRYSTALS)
					{
						ManaCrystalMgr.Get().TurnCrystalsRed(change.oldValue, change.newValue);
						return;
					}
					if (tag != GAME_TAG.CURRENT_NEGATIVE_SPELLPOWER)
					{
						return;
					}
					goto IL_2CB;
				}
			}
			else if (tag <= GAME_TAG.NUM_HERO_POWER_DAMAGE_THIS_GAME)
			{
				if (tag != GAME_TAG.AMOUNT_HEALED_THIS_GAME && tag != GAME_TAG.NUM_HERO_POWER_DAMAGE_THIS_GAME)
				{
					return;
				}
			}
			else if (tag != GAME_TAG.ALL_HEALING_DOUBLE && tag != GAME_TAG.NUM_SPELLS_PLAYED_THIS_GAME)
			{
				if (tag - GAME_TAG.CURRENT_SPELLPOWER_ARCANE > 7)
				{
					return;
				}
				goto IL_2CB;
			}
			this.UpdateHandCardPowersText(false);
			return;
		}
		if (tag <= GAME_TAG.CURRENT_SPELLPOWER)
		{
			if (tag <= GAME_TAG.RESOURCES)
			{
				if (tag == GAME_TAG.RESOURCES_USED)
				{
					int num = change.oldValue + this.m_queuedSpentMana;
					int num2 = change.newValue - change.oldValue;
					if (num2 > 0)
					{
						this.m_queuedSpentMana -= num2;
					}
					if (this.m_queuedSpentMana < 0)
					{
						this.m_queuedSpentMana = 0;
					}
					int shownChangeAmount = change.newValue - num + this.m_queuedSpentMana;
					ManaCrystalMgr.Get().UpdateSpentMana(shownChangeAmount);
					return;
				}
				if (tag != GAME_TAG.RESOURCES)
				{
					return;
				}
				if (change.newValue <= change.oldValue)
				{
					this.DestroyManaCrystal(change.oldValue - change.newValue);
					return;
				}
				if (GameState.Get().IsTurnStartManagerActive() && this.IsFriendlySide())
				{
					TurnStartManager.Get().NotifyOfManaCrystalGained(change.newValue - change.oldValue);
					return;
				}
				this.AddManaCrystal(change.newValue - change.oldValue);
				return;
			}
			else if (tag != GAME_TAG.NUM_TURNS_LEFT)
			{
				if (tag != GAME_TAG.CURRENT_SPELLPOWER)
				{
					return;
				}
			}
			else
			{
				TurnStartManager turnStartManager = TurnStartManager.Get();
				if (!(turnStartManager != null))
				{
					return;
				}
				Spell extraTurnSpell = turnStartManager.GetExtraTurnSpell(true);
				if (change.oldValue >= 2 && change.newValue == 1)
				{
					turnStartManager.NotifyOfExtraTurn(extraTurnSpell, true, true);
				}
				if (change.newValue >= 2 && change.newValue > change.oldValue)
				{
					turnStartManager.NotifyOfExtraTurn(extraTurnSpell, false, true);
					return;
				}
				return;
			}
		}
		else if (tag <= GAME_TAG.OVERLOAD_OWED)
		{
			if (tag != GAME_TAG.TEMP_RESOURCES)
			{
				if (tag != GAME_TAG.OVERLOAD_OWED)
				{
					return;
				}
				this.HandleSameTurnOverloadChanged(change.newValue - change.oldValue);
				return;
			}
			else
			{
				int num3 = change.oldValue - this.m_usedTempMana;
				int num4 = change.newValue - change.oldValue;
				if (num4 < 0)
				{
					this.m_usedTempMana += num4;
				}
				if (this.m_usedTempMana < 0)
				{
					this.m_usedTempMana = 0;
				}
				if (num3 < 0)
				{
					num3 = 0;
				}
				int num5 = change.newValue - num3 - this.m_usedTempMana;
				if (num5 > 0)
				{
					this.AddTempManaCrystal(num5);
					return;
				}
				this.DestroyTempManaCrystal(-num5);
				return;
			}
		}
		else if (tag != GAME_TAG.MULLIGAN_STATE)
		{
			if (tag - GAME_TAG.SPELLPOWER_DOUBLE > 1)
			{
				return;
			}
		}
		else
		{
			if (change.newValue != 4 || !(MulliganManager.Get() == null))
			{
				return;
			}
			using (List<Card>.Enumerator enumerator = this.GetHandZone().GetCards().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Card card = enumerator.Current;
					card.GetActor().TurnOnCollider();
				}
				return;
			}
		}
		IL_2CB:
		this.UpdateHandCardPowersText(false);
	}

	// Token: 0x06002ED5 RID: 11989 RVA: 0x000EE5C4 File Offset: 0x000EC7C4
	private void OnOpposingPlayerTagChanged(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag != GAME_TAG.PLAYSTATE)
		{
			if (tag != GAME_TAG.RESOURCES)
			{
				if (tag != GAME_TAG.NUM_TURNS_LEFT)
				{
					return;
				}
				TurnStartManager turnStartManager = TurnStartManager.Get();
				if (turnStartManager != null)
				{
					Spell extraTurnSpell = turnStartManager.GetExtraTurnSpell(false);
					if (change.oldValue >= 2 && change.newValue == 1)
					{
						TurnStartManager.Get().NotifyOfExtraTurn(extraTurnSpell, true, false);
					}
					if (change.newValue >= 2 && change.newValue > change.oldValue)
					{
						TurnStartManager.Get().NotifyOfExtraTurn(extraTurnSpell, false, false);
					}
				}
			}
			else if (change.newValue > change.oldValue)
			{
				GameState.Get().GetGameEntity().NotifyOfEnemyManaCrystalSpawned();
				return;
			}
		}
		else if (change.newValue == 7)
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_ANNOUNCER_DISCONNECT_45"), "VO_ANNOUNCER_DISCONNECT_45.prefab:911a83eb9ad91fc41acf1aca808c5e5a", 0f, null, false);
			return;
		}
	}

	// Token: 0x06002ED6 RID: 11990 RVA: 0x000EE694 File Offset: 0x000EC894
	private void UpdateName()
	{
		GameState gameState = GameState.Get();
		GameEntity gameEntity = (gameState != null) ? gameState.GetGameEntity() : null;
		if (gameEntity != null && gameEntity.ShouldUseAlternateNameForPlayer(this.GetSide()))
		{
			this.m_name = gameEntity.GetNameBannerOverride(this.GetSide());
			return;
		}
		if (this.ShouldUseHeroName())
		{
			this.UpdateNameWithHeroName();
			return;
		}
		if (this.IsAI())
		{
			this.m_name = GameStrings.Get("GAMEPLAY_AI_OPPONENT_NAME");
			return;
		}
		if (this.IsBnetPlayer())
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
			if (player != null)
			{
				this.m_name = player.GetBestName();
			}
			if (!string.IsNullOrEmpty(this.m_name))
			{
				GameMgr.Get().SetLastDisplayedPlayerName(this.GetPlayerId(), this.m_name);
				return;
			}
		}
		else
		{
			Debug.LogError(string.Format("Player.UpdateName() - unable to determine player name", Array.Empty<object>()));
		}
	}

	// Token: 0x06002ED7 RID: 11991 RVA: 0x000EE760 File Offset: 0x000EC960
	private bool ShouldUseHeroName()
	{
		return !this.IsBnetPlayer() && (!this.IsAI() || !GameMgr.Get().IsPractice());
	}

	// Token: 0x06002ED8 RID: 11992 RVA: 0x000EE784 File Offset: 0x000EC984
	private void UpdateNameWithHeroName()
	{
		if (this.m_hero == null)
		{
			return;
		}
		EntityDef entityDef = this.m_hero.GetEntityDef();
		if (entityDef == null)
		{
			return;
		}
		this.m_name = entityDef.GetName();
	}

	// Token: 0x06002ED9 RID: 11993 RVA: 0x000EE7B6 File Offset: 0x000EC9B6
	private bool ShouldUseBogusRank()
	{
		return !this.IsBnetPlayer();
	}

	// Token: 0x06002EDA RID: 11994 RVA: 0x000EE7C4 File Offset: 0x000EC9C4
	private void UpdateRank()
	{
		MedalInfoTranslator medalInfoTranslator = null;
		if (this.ShouldUseBogusRank())
		{
			medalInfoTranslator = new MedalInfoTranslator();
		}
		else if (this.m_gameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			medalInfoTranslator = RankMgr.Get().GetLocalPlayerMedalInfo();
		}
		if (medalInfoTranslator == null)
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
			if (player != null)
			{
				medalInfoTranslator = RankMgr.Get().GetRankPresenceField(player);
			}
		}
		this.m_medalInfo = medalInfoTranslator;
	}

	// Token: 0x06002EDB RID: 11995 RVA: 0x000EE830 File Offset: 0x000ECA30
	public void UpdateDisplayInfo()
	{
		this.UpdateName();
		this.UpdateRank();
		this.UpdateSessionRecord();
		if (this.IsBnetPlayer() && !this.IsLocalUser())
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
			if (player != null && BnetFriendMgr.Get().IsFriend(player))
			{
				ChatMgr.Get().AddRecentWhisperPlayerToBottom(player);
			}
		}
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x000EE88C File Offset: 0x000ECA8C
	private void UpdateSessionRecord()
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(this.m_gameAccountId);
		if (player == null)
		{
			return;
		}
		BnetGameAccount hearthstoneGameAccount = player.GetHearthstoneGameAccount();
		if (hearthstoneGameAccount == null)
		{
			return;
		}
		SessionRecord sessionRecord = hearthstoneGameAccount.GetSessionRecord();
		if (sessionRecord == null)
		{
			return;
		}
		if (sessionRecord.SessionRecordType == SessionRecordType.ARENA)
		{
			this.m_arenaWins = sessionRecord.Wins;
			this.m_arenaLoss = sessionRecord.Losses;
			return;
		}
		if (sessionRecord.SessionRecordType == SessionRecordType.TAVERN_BRAWL)
		{
			this.m_tavernBrawlWins = sessionRecord.Wins;
			this.m_tavernBrawlLoss = sessionRecord.Losses;
			return;
		}
		if (sessionRecord.SessionRecordType == SessionRecordType.DUELS)
		{
			this.m_duelsWins = sessionRecord.Wins;
			this.m_duelsLoss = sessionRecord.Losses;
		}
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x000EE92E File Offset: 0x000ECB2E
	private void OnBnetPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(this.m_gameAccountId) == null)
		{
			return;
		}
		if (!this.IsDisplayable())
		{
			return;
		}
		BnetPresenceMgr.Get().RemovePlayersChangedListener(new BnetPresenceMgr.PlayersChangedCallback(this.OnBnetPlayersChanged));
		this.UpdateDisplayInfo();
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x000EE968 File Offset: 0x000ECB68
	private void UpdateLocal()
	{
		if (GameMgr.Get() != null && SpectatorManager.Get().IsSpectatingOrWatching)
		{
			this.m_local = false;
			return;
		}
		if (this.IsBnetPlayer())
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			this.m_local = (myGameAccountId == this.m_gameAccountId);
			return;
		}
		this.m_local = (this.m_gameAccountId.GetLo() == 1UL);
	}

	// Token: 0x06002EDF RID: 11999 RVA: 0x000EE9CC File Offset: 0x000ECBCC
	public void UpdateSide(int friendlySideTeamId)
	{
		if (this.GetTeamId() == friendlySideTeamId)
		{
			this.m_side = Player.Side.FRIENDLY;
			GameState.Get().RegisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnFriendlyOptionsReceived));
			GameState.Get().RegisterOptionsSentListener(new GameState.OptionsSentCallback(this.OnFriendlyOptionsSent), null);
			GameState.Get().RegisterFriendlyTurnStartedListener(new GameState.FriendlyTurnStartedCallback(this.OnFriendlyTurnStarted), null);
			return;
		}
		this.m_side = Player.Side.OPPOSING;
	}

	// Token: 0x06002EE0 RID: 12000 RVA: 0x000EEA38 File Offset: 0x000ECC38
	private void AssignPlayerBoardObjects()
	{
		if (!this.IsTeamLeader())
		{
			return;
		}
		foreach (ManaCounter manaCounter in Gameplay.Get().GetBoardLayout().gameObject.GetComponentsInChildren<ManaCounter>(true))
		{
			if (manaCounter.m_Side == this.m_side)
			{
				this.m_manaCounter = manaCounter;
				this.m_manaCounter.SetPlayer(this);
				this.m_manaCounter.UpdateText();
				break;
			}
		}
		this.InitManaCrystalMgr();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone.m_Side == this.m_side)
			{
				zone.SetController(this);
			}
		}
	}

	// Token: 0x06002EE1 RID: 12001 RVA: 0x000EEB08 File Offset: 0x000ECD08
	private void InitManaCrystalMgr()
	{
		if (!this.IsFriendlySide())
		{
			return;
		}
		int tag = base.GetTag(GAME_TAG.TEMP_RESOURCES);
		int tag2 = base.GetTag(GAME_TAG.RESOURCES);
		int tag3 = base.GetTag(GAME_TAG.RESOURCES_USED);
		int tag4 = base.GetTag(GAME_TAG.OVERLOAD_OWED);
		int tag5 = base.GetTag(GAME_TAG.OVERLOAD_LOCKED);
		ManaCrystalMgr.Get().AddManaCrystals(tag2, false);
		ManaCrystalMgr.Get().AddTempManaCrystals(tag);
		ManaCrystalMgr.Get().UpdateSpentMana(tag3);
		ManaCrystalMgr.Get().MarkCrystalsOwedForOverload(tag4);
		ManaCrystalMgr.Get().SetCrystalsLockedForOverload(tag5);
		ManaCrystalMgr.Get().ResetUnresolvedManaToBeReadied();
	}

	// Token: 0x06002EE2 RID: 12002 RVA: 0x000EEB98 File Offset: 0x000ECD98
	private void OnTurnChanged(int oldTurn, int newTurn, object userData)
	{
		this.WipeZzzs();
		this.UpdateChooseBoth();
	}

	// Token: 0x06002EE3 RID: 12003 RVA: 0x000EEBA6 File Offset: 0x000ECDA6
	private void OnFriendlyOptionsReceived(object userData)
	{
		this.UpdateChooseBoth();
	}

	// Token: 0x06002EE4 RID: 12004 RVA: 0x000EEBB0 File Offset: 0x000ECDB0
	private void OnFriendlyOptionsSent(Network.Options.Option option, object userData)
	{
		this.UpdateChooseBoth();
		Entity entity = GameState.Get().GetEntity(option.Main.ID);
		this.CancelAllProposedMana(entity);
	}

	// Token: 0x06002EE5 RID: 12005 RVA: 0x000EEBA6 File Offset: 0x000ECDA6
	private void OnFriendlyTurnStarted(object userData)
	{
		this.UpdateChooseBoth();
	}

	// Token: 0x06002EE6 RID: 12006 RVA: 0x000EEBE0 File Offset: 0x000ECDE0
	private Spell ToggleActorSpellOnCard(Card card, TagDelta change, SpellType spellType)
	{
		if (card == null)
		{
			return null;
		}
		if (!card.CanShowActorVisuals())
		{
			return null;
		}
		Actor actor = card.GetActor();
		if (change.newValue > 0)
		{
			return actor.ActivateSpellBirthState(spellType);
		}
		actor.ActivateSpellDeathState(spellType);
		return null;
	}

	// Token: 0x06002EE7 RID: 12007 RVA: 0x000EEC24 File Offset: 0x000ECE24
	private void UpdateHandCardPowersText(bool onlySpells)
	{
		List<Card> cards = this.GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (!(card.GetActor() == null) && (!onlySpells || card.GetEntity().IsSpell()))
			{
				card.GetActor().UpdatePowersText();
			}
		}
	}

	// Token: 0x06002EE8 RID: 12008 RVA: 0x000EEC80 File Offset: 0x000ECE80
	private void UpdateSpellsCostHealth(TagDelta change)
	{
		if (this.IsFriendlySide())
		{
			Card mousedOverCard = InputManager.Get().GetMousedOverCard();
			if (mousedOverCard != null)
			{
				Entity entity = mousedOverCard.GetEntity();
				if (entity.IsSpell())
				{
					if (change.newValue > 0)
					{
						ManaCrystalMgr.Get().CancelAllProposedMana(entity);
					}
					else
					{
						ManaCrystalMgr.Get().ProposeManaCrystalUsage(entity);
					}
				}
			}
		}
		List<Card> cards = this.GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (card.CanShowActorVisuals())
			{
				Entity entity2 = card.GetEntity();
				if (entity2.IsSpell() && !entity2.HasTag(GAME_TAG.CARD_COSTS_HEALTH))
				{
					Actor actor = card.GetActor();
					if (change.newValue > 0)
					{
						actor.ActivateSpellBirthState(SpellType.SPELLS_COST_HEALTH);
					}
					else
					{
						actor.ActivateSpellDeathState(SpellType.SPELLS_COST_HEALTH);
					}
				}
			}
		}
	}

	// Token: 0x06002EE9 RID: 12009 RVA: 0x000EED50 File Offset: 0x000ECF50
	private void UpdateChooseBoth()
	{
		List<Card> cards = this.GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			this.UpdateChooseBoth(card);
		}
		this.UpdateChooseBoth(this.GetHeroPowerCard());
	}

	// Token: 0x06002EEA RID: 12010 RVA: 0x000EED98 File Offset: 0x000ECF98
	private void UpdateChooseBoth(Card card)
	{
		if (card == null)
		{
			return;
		}
		if (!card.CanShowActorVisuals())
		{
			return;
		}
		Entity entity = card.GetEntity();
		if (!entity.HasTag(GAME_TAG.CHOOSE_ONE))
		{
			return;
		}
		Actor actor = card.GetActor();
		SpellType spellType = SpellType.CHOOSE_BOTH;
		if (base.HasTag(GAME_TAG.CHOOSE_BOTH) && GameState.Get().IsValidOption(entity))
		{
			SpellUtils.ActivateBirthIfNecessary(actor.GetSpell(spellType));
			return;
		}
		SpellUtils.ActivateDeathIfNecessary(actor.GetSpellIfLoaded(spellType));
	}

	// Token: 0x04001A03 RID: 6659
	private BnetGameAccountId m_gameAccountId;

	// Token: 0x04001A04 RID: 6660
	private bool m_waitingForHeroEntity;

	// Token: 0x04001A05 RID: 6661
	private string m_name;

	// Token: 0x04001A06 RID: 6662
	private bool m_local;

	// Token: 0x04001A07 RID: 6663
	private Player.Side m_side;

	// Token: 0x04001A08 RID: 6664
	private int m_cardBackId;

	// Token: 0x04001A09 RID: 6665
	private ManaCounter m_manaCounter;

	// Token: 0x04001A0A RID: 6666
	private Entity m_hero;

	// Token: 0x04001A0B RID: 6667
	private Entity m_heroPower;

	// Token: 0x04001A0C RID: 6668
	private int m_queuedSpentMana;

	// Token: 0x04001A0D RID: 6669
	private int m_usedTempMana;

	// Token: 0x04001A0E RID: 6670
	private int m_realtimeTempMana;

	// Token: 0x04001A0F RID: 6671
	private bool m_realTimeComboActive;

	// Token: 0x04001A10 RID: 6672
	private bool m_realTimeSpellsCostHealth;

	// Token: 0x04001A11 RID: 6673
	private MedalInfoTranslator m_medalInfo;

	// Token: 0x04001A12 RID: 6674
	private uint m_arenaWins;

	// Token: 0x04001A13 RID: 6675
	private uint m_arenaLoss;

	// Token: 0x04001A14 RID: 6676
	private uint m_tavernBrawlWins;

	// Token: 0x04001A15 RID: 6677
	private uint m_tavernBrawlLoss;

	// Token: 0x04001A16 RID: 6678
	private uint m_duelsWins;

	// Token: 0x04001A17 RID: 6679
	private uint m_duelsLoss;

	// Token: 0x04001A18 RID: 6680
	private bool m_concedeEmotePlayed;

	// Token: 0x04001A19 RID: 6681
	private TAG_PLAYSTATE m_preGameOverPlayState;

	// Token: 0x04001A1A RID: 6682
	private HashSet<EntityDef> m_seenStartOfGameSpells = new HashSet<EntityDef>();

	// Token: 0x04001A1B RID: 6683
	private MarkOfEvilCounter m_markOfEvilCounter;

	// Token: 0x020016D1 RID: 5841
	public enum Side
	{
		// Token: 0x0400B213 RID: 45587
		NEUTRAL,
		// Token: 0x0400B214 RID: 45588
		FRIENDLY,
		// Token: 0x0400B215 RID: 45589
		OPPOSING
	}
}
