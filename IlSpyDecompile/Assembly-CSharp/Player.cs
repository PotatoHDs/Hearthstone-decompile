using System.Collections.Generic;
using bgs;
using PegasusClient;
using UnityEngine;

public class Player : Entity
{
	public enum Side
	{
		NEUTRAL,
		FRIENDLY,
		OPPOSING
	}

	private BnetGameAccountId m_gameAccountId;

	private bool m_waitingForHeroEntity;

	private string m_name;

	private bool m_local;

	private Side m_side;

	private int m_cardBackId;

	private ManaCounter m_manaCounter;

	private Entity m_hero;

	private Entity m_heroPower;

	private int m_queuedSpentMana;

	private int m_usedTempMana;

	private int m_realtimeTempMana;

	private bool m_realTimeComboActive;

	private bool m_realTimeSpellsCostHealth;

	private MedalInfoTranslator m_medalInfo;

	private uint m_arenaWins;

	private uint m_arenaLoss;

	private uint m_tavernBrawlWins;

	private uint m_tavernBrawlLoss;

	private uint m_duelsWins;

	private uint m_duelsLoss;

	private bool m_concedeEmotePlayed;

	private TAG_PLAYSTATE m_preGameOverPlayState;

	private HashSet<EntityDef> m_seenStartOfGameSpells = new HashSet<EntityDef>();

	private MarkOfEvilCounter m_markOfEvilCounter;

	public static Side GetOppositePlayerSide(Side side)
	{
		return side switch
		{
			Side.FRIENDLY => Side.OPPOSING, 
			Side.OPPOSING => Side.FRIENDLY, 
			_ => side, 
		};
	}

	public void OnShuffleDeck()
	{
		ZoneDeck deckZone = GetDeckZone();
		if (!(deckZone == null))
		{
			deckZone.UpdateLayout();
			Actor activeThickness = deckZone.GetActiveThickness();
			if (!(activeThickness == null))
			{
				activeThickness.ActivateSpellBirthState(SpellType.SHUFFLE_DECK);
			}
		}
	}

	public void InitPlayer(Network.HistCreateGame.PlayerData netPlayer)
	{
		SetPlayerId(netPlayer.ID);
		SetGameAccountId(netPlayer.GameAccountId);
		SetCardBackId(netPlayer.CardBackID);
		SetTags(netPlayer.Player.Tags);
		InitRealTimeValues(netPlayer.Player.Tags);
		if (IsLocalUser())
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
			GetOrCreateMarkOfEvilCounter().OnMarksChanged(tag2.Value);
		}
		GameState.Get().RegisterTurnChangedListener(OnTurnChanged);
	}

	public override bool HasValidDisplayName()
	{
		return !string.IsNullOrEmpty(m_name);
	}

	public override string GetName()
	{
		return m_name;
	}

	public MedalInfoTranslator GetRank()
	{
		return m_medalInfo;
	}

	public override string GetDebugName()
	{
		if (m_name != null)
		{
			return m_name;
		}
		if (IsAI())
		{
			return GameStrings.Get("GAMEPLAY_AI_OPPONENT_NAME");
		}
		return "UNKNOWN HUMAN PLAYER";
	}

	public void SetName(string name)
	{
		m_name = name;
	}

	public void SetGameAccountId(BnetGameAccountId id)
	{
		m_gameAccountId = id;
		UpdateLocal();
		if (IsDisplayable())
		{
			UpdateDisplayInfo();
			return;
		}
		UpdateRank();
		UpdateSessionRecord();
		if (IsBnetPlayer())
		{
			BnetPresenceMgr.Get().AddPlayersChangedListener(OnBnetPlayersChanged);
			if (!BnetFriendMgr.Get().IsFriend(m_gameAccountId))
			{
				GameUtils.RequestPlayerPresence(m_gameAccountId);
			}
		}
	}

	public bool IsLocalUser()
	{
		return m_local;
	}

	public void SetLocalUser(bool local)
	{
		m_local = local;
	}

	public bool IsAI()
	{
		return GameUtils.IsAIPlayer(m_gameAccountId);
	}

	public bool IsHuman()
	{
		return GameUtils.IsHumanPlayer(m_gameAccountId);
	}

	public bool IsBnetPlayer()
	{
		return GameUtils.IsBnetPlayer(m_gameAccountId);
	}

	public bool IsGuestPlayer()
	{
		return GameUtils.IsGuestPlayer(m_gameAccountId);
	}

	public Side GetSide()
	{
		return m_side;
	}

	public bool IsFriendlySide()
	{
		return m_side == Side.FRIENDLY;
	}

	public bool IsOpposingSide()
	{
		return m_side == Side.OPPOSING;
	}

	public int TotalSpellpower(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		int num = 0;
		switch (spellSchool)
		{
		case TAG_SPELL_SCHOOL.ARCANE:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_ARCANE);
			break;
		case TAG_SPELL_SCHOOL.FIRE:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_FIRE);
			break;
		case TAG_SPELL_SCHOOL.FROST:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_FROST);
			break;
		case TAG_SPELL_SCHOOL.NATURE:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_NATURE);
			break;
		case TAG_SPELL_SCHOOL.HOLY:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_HOLY);
			break;
		case TAG_SPELL_SCHOOL.SHADOW:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_SHADOW);
			break;
		case TAG_SPELL_SCHOOL.FEL:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_FEL);
			break;
		case TAG_SPELL_SCHOOL.PHYSICAL_COMBAT:
			num += GetTag(GAME_TAG.CURRENT_SPELLPOWER_PHYSICAL);
			break;
		}
		return GetTag(GAME_TAG.CURRENT_SPELLPOWER) - GetTag(GAME_TAG.CURRENT_NEGATIVE_SPELLPOWER) + num;
	}

	public new bool IsRevealed()
	{
		if (IsFriendlySide())
		{
			return true;
		}
		if (SpectatorManager.Get().IsSpectatingPlayer(m_gameAccountId))
		{
			return true;
		}
		if (HasTag(GAME_TAG.ZONES_REVEALED))
		{
			return true;
		}
		return false;
	}

	public void SetSide(Side side)
	{
		m_side = side;
	}

	public int GetCardBackId()
	{
		return m_cardBackId;
	}

	public void SetCardBackId(int id)
	{
		m_cardBackId = id;
	}

	public int GetPlayerId()
	{
		return GetTag(GAME_TAG.PLAYER_ID);
	}

	public void SetPlayerId(int playerId)
	{
		SetTag(GAME_TAG.PLAYER_ID, playerId);
	}

	public int GetTeamId()
	{
		return GetTag(GAME_TAG.TEAM_ID);
	}

	public bool IsTeamLeader()
	{
		return GetPlayerId() == GetTeamId();
	}

	public List<string> GetSecretDefinitions()
	{
		List<string> list = new List<string>();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (!(zone is ZoneSecret) || zone.m_Side != Side.FRIENDLY)
			{
				continue;
			}
			foreach (Card card in zone.GetCards())
			{
				if (card.GetEntity().IsSecret())
				{
					list.Add(card.GetEntity().GetCardId());
				}
			}
		}
		return list;
	}

	public List<string> GetQuestDefinitions()
	{
		List<string> list = new List<string>();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (!(zone is ZoneSecret) || zone.m_Side != Side.FRIENDLY)
			{
				continue;
			}
			foreach (Card card in zone.GetCards())
			{
				if (card.GetEntity().IsQuest())
				{
					list.Add(card.GetEntity().GetCardId());
				}
			}
		}
		return list;
	}

	public bool IsCurrentPlayer()
	{
		return HasTag(GAME_TAG.CURRENT_PLAYER);
	}

	public bool IsComboActive()
	{
		return HasTag(GAME_TAG.COMBO_ACTIVE);
	}

	public bool IsRealTimeComboActive()
	{
		return m_realTimeComboActive;
	}

	public void SetRealTimeComboActive(int tagValue)
	{
		SetRealTimeComboActive((tagValue == 1) ? true : false);
	}

	public void SetRealTimeComboActive(bool active)
	{
		m_realTimeComboActive = active;
	}

	public void SetRealTimeSpellsCostHealth(int value)
	{
		m_realTimeSpellsCostHealth = ((value > 0) ? true : false);
	}

	public bool GetRealTimeSpellsCostHealth()
	{
		return m_realTimeSpellsCostHealth;
	}

	public override void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		base.InitRealTimeValues(tags);
		foreach (Network.Entity.Tag tag in tags)
		{
			switch (tag.Name)
			{
			case 295:
				SetRealTimeTempMana(tag.Value);
				break;
			case 266:
				SetRealTimeComboActive(tag.Value);
				break;
			case 431:
				SetRealTimeSpellsCostHealth(tag.Value);
				break;
			}
		}
	}

	public int GetNumAvailableResources()
	{
		int tag = GetTag(GAME_TAG.TEMP_RESOURCES);
		int tag2 = GetTag(GAME_TAG.RESOURCES);
		int tag3 = GetTag(GAME_TAG.RESOURCES_USED);
		int num = tag2 + tag - tag3 - m_queuedSpentMana - m_usedTempMana;
		if (num >= 0)
		{
			return num;
		}
		return 0;
	}

	public bool HasWeapon()
	{
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone is ZoneWeapon && zone.m_Side == m_side)
			{
				return zone.GetCards().Count > 0;
			}
		}
		return false;
	}

	public void SetHero(Entity hero)
	{
		m_hero = hero;
		if (ShouldUseHeroName())
		{
			UpdateDisplayInfo();
		}
		foreach (Card card in GetHandZone().GetCards())
		{
			if (card.GetEntity().IsMultiClass())
			{
				card.UpdateActorComponents();
			}
		}
		if (IsFriendlySide())
		{
			GameState.Get().FireHeroChangedEvent(this);
		}
	}

	public Entity GetStartingHero()
	{
		Entity entity = GetHero();
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
				Log.Gameplay.PrintError("Player.GetStartingHero() - Hero entity {0} has a LINKED_ENTITY tag value of {1} which corresponds to invalid Entity {2}.", entity, tag, entity2);
				break;
			}
			entity = entity2;
		}
		return entity;
	}

	public override Entity GetHero()
	{
		return m_hero;
	}

	public EntityDef GetHeroEntityDef()
	{
		if (m_hero == null)
		{
			return null;
		}
		EntityDef entityDef = m_hero.GetEntityDef();
		if (entityDef == null)
		{
			return null;
		}
		return entityDef;
	}

	public override Card GetHeroCard()
	{
		if (m_hero == null)
		{
			return null;
		}
		return m_hero.GetCard();
	}

	public void SetHeroPower(Entity heroPower)
	{
		m_heroPower = heroPower;
	}

	public override Entity GetHeroPower()
	{
		return m_heroPower;
	}

	public override Card GetHeroPowerCard()
	{
		if (m_heroPower == null)
		{
			return null;
		}
		return m_heroPower.GetCard();
	}

	public bool IsHeroPowerAffectedByBonusDamage()
	{
		Card heroPowerCard = GetHeroPowerCard();
		if (heroPowerCard == null)
		{
			return false;
		}
		Entity entity = heroPowerCard.GetEntity();
		if (!entity.IsHeroPower())
		{
			return false;
		}
		return entity.GetCardTextBuilder().ContainsBonusDamageToken(entity);
	}

	public override Card GetWeaponCard()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneWeapon>(GetSide()).GetFirstCard();
	}

	public ZoneHand GetHandZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneHand>(GetSide());
	}

	public ZonePlay GetBattlefieldZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZonePlay>(GetSide());
	}

	public ZoneDeck GetDeckZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneDeck>(GetSide());
	}

	public ZoneGraveyard GetGraveyardZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneGraveyard>(GetSide());
	}

	public ZoneSecret GetSecretZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneSecret>(GetSide());
	}

	public ZoneHero GetHeroZone()
	{
		return ZoneMgr.Get().FindZoneOfType<ZoneHero>(GetSide());
	}

	public bool HasReadyAttackers()
	{
		List<Card> cards = GetBattlefieldZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			if (GameState.Get().HasResponse(cards[i].GetEntity()))
			{
				return true;
			}
		}
		return false;
	}

	public bool HasATauntMinion()
	{
		List<Card> cards = GetBattlefieldZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			if (cards[i].GetEntity().HasTaunt())
			{
				return true;
			}
		}
		return false;
	}

	public uint GetArenaWins()
	{
		return m_arenaWins;
	}

	public uint GetArenaLosses()
	{
		return m_arenaLoss;
	}

	public uint GetTavernBrawlWins()
	{
		return m_tavernBrawlWins;
	}

	public uint GetTavernBrawlLosses()
	{
		return m_tavernBrawlLoss;
	}

	public uint GetDuelsWins()
	{
		return m_duelsWins;
	}

	public uint GetDuelsLosses()
	{
		return m_duelsLoss;
	}

	public void PlayConcedeEmote()
	{
		if (!m_concedeEmotePlayed)
		{
			Card heroCard = GetHeroCard();
			if (!(heroCard == null))
			{
				heroCard.PlayEmote(EmoteType.CONCEDE);
				m_concedeEmotePlayed = true;
			}
		}
	}

	public BnetGameAccountId GetGameAccountId()
	{
		return m_gameAccountId;
	}

	public BnetPlayer GetBnetPlayer()
	{
		return BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
	}

	public bool IsDisplayable()
	{
		if (m_gameAccountId == null)
		{
			return false;
		}
		if (!IsBnetPlayer())
		{
			if (ShouldUseHeroName() && GetHeroEntityDef() == null)
			{
				return false;
			}
			return true;
		}
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
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
			if (!hearthstoneGameAccount.HasGameField(18u))
			{
				return false;
			}
		}
		return true;
	}

	public void WipeZzzs()
	{
		foreach (Card card in GetBattlefieldZone().GetCards())
		{
			Spell actorSpell = card.GetActorSpell(SpellType.Zzz);
			if (!(actorSpell == null))
			{
				actorSpell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	public TAG_PLAYSTATE GetPreGameOverPlayState()
	{
		return m_preGameOverPlayState;
	}

	public bool HasSeenStartOfGameSpell(EntityDef entityDef)
	{
		return m_seenStartOfGameSpells.Contains(entityDef);
	}

	public void MarkStartOfGameSpellAsSeen(EntityDef entityDef)
	{
		m_seenStartOfGameSpells.Add(entityDef);
	}

	public void AddManaCrystal(int numCrystals, bool isTurnStart)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().AddManaCrystals(numCrystals, isTurnStart);
		}
	}

	public void AddManaCrystal(int numCrystals)
	{
		AddManaCrystal(numCrystals, isTurnStart: false);
	}

	public void DestroyManaCrystal(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().DestroyManaCrystals(numCrystals);
		}
	}

	public void AddTempManaCrystal(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().AddTempManaCrystals(numCrystals);
		}
	}

	public void DestroyTempManaCrystal(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().DestroyTempManaCrystals(numCrystals);
		}
	}

	public void SpendManaCrystal(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().SpendManaCrystals(numCrystals);
		}
	}

	public void ReadyManaCrystal(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().ReadyManaCrystals(numCrystals);
		}
	}

	public void HandleSameTurnOverloadChanged(int crystalsChanged)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().HandleSameTurnOverloadChanged(crystalsChanged);
		}
	}

	public void UnlockCrystals(int numCrystals)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().UnlockCrystals(numCrystals);
		}
	}

	public void CancelAllProposedMana(Entity entity)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().CancelAllProposedMana(entity);
		}
	}

	public void ProposeManaCrystalUsage(Entity entity)
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().ProposeManaCrystalUsage(entity);
		}
	}

	public void ResetUnresolvedManaToBeReadied()
	{
		if (IsFriendlySide())
		{
			ManaCrystalMgr.Get().ResetUnresolvedManaToBeReadied();
		}
	}

	public void UpdateManaCounter()
	{
		if (!(m_manaCounter == null))
		{
			m_manaCounter.UpdateText();
		}
	}

	public void NotifyOfSpentMana(int spentMana)
	{
		m_queuedSpentMana += spentMana;
	}

	public void NotifyOfUsedTempMana(int usedMana)
	{
		m_usedTempMana += usedMana;
	}

	public int GetQueuedUsedTempMana()
	{
		return m_usedTempMana;
	}

	public int GetQueuedSpentMana()
	{
		return m_queuedSpentMana;
	}

	public void SetRealTimeTempMana(int tempMana)
	{
		m_realtimeTempMana = tempMana;
	}

	public int GetRealTimeTempMana()
	{
		return m_realtimeTempMana;
	}

	public void OnBoardLoaded()
	{
		AssignPlayerBoardObjects();
	}

	public override void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		switch (change.Tag)
		{
		case 295:
			SetRealTimeTempMana(change.Value);
			break;
		case 266:
			SetRealTimeComboActive(change.Value);
			break;
		case 17:
		{
			TAG_PLAYSTATE value = (TAG_PLAYSTATE)change.Value;
			if (GameUtils.IsPreGameOverPlayState(value))
			{
				m_preGameOverPlayState = value;
			}
			break;
		}
		case 431:
			SetRealTimeSpellsCostHealth(change.Value);
			break;
		}
	}

	public override void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			OnTagChanged(change);
		}
	}

	public override void OnTagChanged(TagDelta change)
	{
		if (IsFriendlySide())
		{
			OnFriendlyPlayerTagChanged(change);
		}
		else
		{
			OnOpposingPlayerTagChanged(change);
		}
		switch (change.tag)
		{
		case 23:
			if (change.newValue == 1 && GameState.Get().IsLocalSidePlayerTurn())
			{
				ManaCrystalMgr.Get().OnCurrentPlayerChanged();
				m_queuedSpentMana = 0;
				if (GameState.Get().IsMainPhase())
				{
					TurnStartManager.Get().BeginListeningForTurnEvents();
				}
			}
			break;
		case 25:
		case 26:
		case 295:
			if (!GameState.Get().IsTurnStartManagerActive() || !IsFriendlySide())
			{
				UpdateManaCounter();
			}
			GameState.Get().UpdateOptionHighlights();
			break;
		case 266:
			foreach (Card card2 in GetHandZone().GetCards())
			{
				card2.UpdateActorState();
			}
			GetHeroPower()?.GetCard().UpdateActorState();
			break;
		case 17:
			if (change.newValue == 8)
			{
				PlayConcedeEmote();
			}
			break;
		case 305:
			if (change.newValue == 4 && MulliganManager.Get() != null)
			{
				MulliganManager.Get().ServerHasDealtReplacementCards(IsFriendlySide());
			}
			break;
		case 383:
		{
			Card heroPowerCard3 = GetHeroPowerCard();
			ToggleActorSpellOnCard(heroPowerCard3, change, SpellType.STEADY_SHOT_CAN_TARGET);
			break;
		}
		case 395:
			if (IsHeroPowerAffectedByBonusDamage())
			{
				Card heroPowerCard2 = GetHeroPowerCard();
				ToggleActorSpellOnCard(heroPowerCard2, change, SpellType.CURRENT_HEROPOWER_DAMAGE_BONUS);
			}
			break;
		case 414:
		{
			Card heroCard6 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard6, change, SpellType.LOCK_AND_LOAD);
			break;
		}
		case 554:
		{
			Card heroCard5 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard5, change, SpellType.DEATH_KNIGHT);
			break;
		}
		case 431:
			UpdateSpellsCostHealth(change);
			break;
		case 419:
			UpdateChooseBoth();
			break;
		case 442:
		{
			Card heroCard4 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard4, change, SpellType.EMBRACE_THE_SHADOW);
			break;
		}
		case 564:
		{
			Card heroCard2 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard2, change, SpellType.STAMPEDE);
			break;
		}
		case 680:
		{
			Card heroCard = GetHeroCard();
			ToggleActorSpellOnCard(heroCard, change, SpellType.IS_VAMPIRE);
			break;
		}
		case 1044:
		{
			Card heroCard3 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard3, change, SpellType.GLORIOUSGLOOP);
			break;
		}
		case 1681:
		{
			Card heroCard8 = GetHeroCard();
			ToggleActorSpellOnCard(heroCard8, change, SpellType.SPELLS_CAST_TWICE);
			break;
		}
		case 1080:
		{
			Card heroCard7 = GetHeroCard();
			Spell spell = ToggleActorSpellOnCard(heroCard7, change, SpellType.DECK_POWER_UP);
			if (spell != null && GetHeroCard() != null && GetHeroCard().gameObject != null)
			{
				spell.SetSource(GetHeroCard().gameObject);
				spell.ForceUpdateTransform();
			}
			break;
		}
		case 1772:
			if (change.newValue == 1)
			{
				AssetLoader.Get().InstantiatePrefab("PopUpProgressBar.prefab:1e74ef51d3388674792ddf7d6233f5d7").GetComponent<PopUpController>()
					.Populate(GetTag(GAME_TAG.PROGRESSBAR_PROGRESS), GetTag(GAME_TAG.PROGRESSBAR_TOTAL), GetTag(GAME_TAG.PROGRESSBAR_CARDID));
			}
			break;
		case 740:
		case 741:
		case 742:
		case 743:
		case 744:
		case 745:
			if (EmoteHandler.Get() != null)
			{
				EmoteHandler.Get().ChangeAvailableEmotes();
			}
			break;
		case 777:
		{
			Card heroPowerCard = GetHeroPowerCard();
			if (heroPowerCard != null && heroPowerCard.GetEntity() != null && heroPowerCard.GetEntity().GetTag(GAME_TAG.EXHAUSTED) == 0)
			{
				heroPowerCard.HandleCardExhaustedTagChanged(change);
			}
			break;
		}
		case 994:
			GetOrCreateMarkOfEvilCounter().OnMarksChanged(change.newValue);
			break;
		case 1048:
			if (IsLocalUser())
			{
				GameMgr.Get().LastGameData.WhizbangDeckID = change.newValue;
			}
			break;
		case 433:
			foreach (Card card3 in GameState.Get().GetFirstOpponentPlayer(GetController()).GetBattlefieldZone()
				.GetCards())
			{
				if (!card3.CanShowActorVisuals())
				{
					continue;
				}
				Entity entity3 = card3.GetEntity();
				if (entity3 != null && entity3.HasTaunt())
				{
					Actor actor = card3.GetActor();
					if (!(actor == null))
					{
						actor.ActivateTaunt();
					}
				}
			}
			break;
		case 1831:
		{
			ZonePlay battlefieldZone = GetBattlefieldZone();
			if ((bool)battlefieldZone)
			{
				List<Card> cards = battlefieldZone.GetCards();
				for (int i = 0; i < cards.Count; i++)
				{
					Card card = cards[i];
					if (card.CanShowActorVisuals())
					{
						Entity entity = card.GetEntity();
						if (entity != null && entity.HasDeathrattle())
						{
							card.ToggleDeathrattle(change.newValue == 0);
						}
					}
				}
			}
			Card weaponCard = GetWeaponCard();
			if (weaponCard != null)
			{
				Entity entity2 = weaponCard.GetEntity();
				if (entity2 != null && entity2.HasDeathrattle())
				{
					weaponCard.ToggleDeathrattle(change.newValue == 0);
				}
			}
			break;
		}
		}
	}

	public MarkOfEvilCounter GetOrCreateMarkOfEvilCounter()
	{
		if (m_markOfEvilCounter == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MarkOfEvilCounter.prefab:ff08f2e19826b354bb37bb25bf81471d", AssetLoadingOptions.IgnorePrefabPosition);
			m_markOfEvilCounter = gameObject.GetComponent<MarkOfEvilCounter>();
			string name = ((GetSide() == Side.FRIENDLY) ? "MarkOfEvil" : "MarkOfEvil_Opponent");
			Transform source = Board.Get().FindBone(name);
			TransformUtil.CopyWorld(gameObject, source);
		}
		return m_markOfEvilCounter;
	}

	private void OnFriendlyPlayerTagChanged(TagDelta change)
	{
		switch (change.tag)
		{
		case 296:
			HandleSameTurnOverloadChanged(change.newValue - change.oldValue);
			break;
		case 393:
			if (change.newValue < change.oldValue && !GameState.Get().IsTurnStartManagerActive())
			{
				UnlockCrystals(change.oldValue - change.newValue);
			}
			break;
		case 295:
		{
			int num3 = change.oldValue - m_usedTempMana;
			int num4 = change.newValue - change.oldValue;
			if (num4 < 0)
			{
				m_usedTempMana += num4;
			}
			if (m_usedTempMana < 0)
			{
				m_usedTempMana = 0;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num5 = change.newValue - num3 - m_usedTempMana;
			if (num5 > 0)
			{
				AddTempManaCrystal(num5);
			}
			else
			{
				DestroyTempManaCrystal(-num5);
			}
			break;
		}
		case 26:
			if (change.newValue > change.oldValue)
			{
				if (GameState.Get().IsTurnStartManagerActive() && IsFriendlySide())
				{
					TurnStartManager.Get().NotifyOfManaCrystalGained(change.newValue - change.oldValue);
				}
				else
				{
					AddManaCrystal(change.newValue - change.oldValue);
				}
			}
			else
			{
				DestroyManaCrystal(change.oldValue - change.newValue);
			}
			break;
		case 25:
		{
			int num = change.oldValue + m_queuedSpentMana;
			int num2 = change.newValue - change.oldValue;
			if (num2 > 0)
			{
				m_queuedSpentMana -= num2;
			}
			if (m_queuedSpentMana < 0)
			{
				m_queuedSpentMana = 0;
			}
			int shownChangeAmount = change.newValue - num + m_queuedSpentMana;
			ManaCrystalMgr.Get().UpdateSpentMana(shownChangeAmount);
			break;
		}
		case 305:
			if (change.newValue != 4 || !(MulliganManager.Get() == null))
			{
				break;
			}
			foreach (Card card in GetHandZone().GetCards())
			{
				card.GetActor().TurnOnCollider();
			}
			break;
		case 291:
		case 356:
		case 357:
		case 651:
		case 1936:
		case 1937:
		case 1938:
		case 1939:
		case 1940:
		case 1941:
		case 1942:
		case 1943:
			UpdateHandCardPowersText(onlySpells: false);
			break;
		case 441:
		case 958:
		case 1025:
		case 1058:
		case 1780:
			UpdateHandCardPowersText(onlySpells: false);
			break;
		case 449:
			ManaCrystalMgr.Get().TurnCrystalsRed(change.oldValue, change.newValue);
			break;
		case 272:
		{
			TurnStartManager turnStartManager = TurnStartManager.Get();
			if (turnStartManager != null)
			{
				Spell extraTurnSpell = turnStartManager.GetExtraTurnSpell();
				if (change.oldValue >= 2 && change.newValue == 1)
				{
					turnStartManager.NotifyOfExtraTurn(extraTurnSpell, isEnding: true);
				}
				if (change.newValue >= 2 && change.newValue > change.oldValue)
				{
					turnStartManager.NotifyOfExtraTurn(extraTurnSpell);
				}
			}
			break;
		}
		}
	}

	private void OnOpposingPlayerTagChanged(TagDelta change)
	{
		switch (change.tag)
		{
		case 17:
			if (change.newValue == 7)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_ANNOUNCER_DISCONNECT_45"), "VO_ANNOUNCER_DISCONNECT_45.prefab:911a83eb9ad91fc41acf1aca808c5e5a");
			}
			break;
		case 26:
			if (change.newValue > change.oldValue)
			{
				GameState.Get().GetGameEntity().NotifyOfEnemyManaCrystalSpawned();
			}
			break;
		case 272:
		{
			TurnStartManager turnStartManager = TurnStartManager.Get();
			if (turnStartManager != null)
			{
				Spell extraTurnSpell = turnStartManager.GetExtraTurnSpell(isFriendly: false);
				if (change.oldValue >= 2 && change.newValue == 1)
				{
					TurnStartManager.Get().NotifyOfExtraTurn(extraTurnSpell, isEnding: true, isFriendly: false);
				}
				if (change.newValue >= 2 && change.newValue > change.oldValue)
				{
					TurnStartManager.Get().NotifyOfExtraTurn(extraTurnSpell, isEnding: false, isFriendly: false);
				}
			}
			break;
		}
		}
	}

	private void UpdateName()
	{
		GameEntity gameEntity = GameState.Get()?.GetGameEntity();
		if (gameEntity != null && gameEntity.ShouldUseAlternateNameForPlayer(GetSide()))
		{
			m_name = gameEntity.GetNameBannerOverride(GetSide());
		}
		else if (ShouldUseHeroName())
		{
			UpdateNameWithHeroName();
		}
		else if (IsAI())
		{
			m_name = GameStrings.Get("GAMEPLAY_AI_OPPONENT_NAME");
		}
		else if (IsBnetPlayer())
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
			if (player != null)
			{
				m_name = player.GetBestName();
			}
			if (!string.IsNullOrEmpty(m_name))
			{
				GameMgr.Get().SetLastDisplayedPlayerName(GetPlayerId(), m_name);
			}
		}
		else
		{
			Debug.LogError($"Player.UpdateName() - unable to determine player name");
		}
	}

	private bool ShouldUseHeroName()
	{
		if (IsBnetPlayer())
		{
			return false;
		}
		if (IsAI() && GameMgr.Get().IsPractice())
		{
			return false;
		}
		return true;
	}

	private void UpdateNameWithHeroName()
	{
		if (m_hero != null)
		{
			EntityDef entityDef = m_hero.GetEntityDef();
			if (entityDef != null)
			{
				m_name = entityDef.GetName();
			}
		}
	}

	private bool ShouldUseBogusRank()
	{
		if (IsBnetPlayer())
		{
			return false;
		}
		return true;
	}

	private void UpdateRank()
	{
		MedalInfoTranslator medalInfoTranslator = null;
		if (ShouldUseBogusRank())
		{
			medalInfoTranslator = new MedalInfoTranslator();
		}
		else if (m_gameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
		{
			medalInfoTranslator = RankMgr.Get().GetLocalPlayerMedalInfo();
		}
		if (medalInfoTranslator == null)
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
			if (player != null)
			{
				medalInfoTranslator = RankMgr.Get().GetRankPresenceField(player);
			}
		}
		m_medalInfo = medalInfoTranslator;
	}

	public void UpdateDisplayInfo()
	{
		UpdateName();
		UpdateRank();
		UpdateSessionRecord();
		if (IsBnetPlayer() && !IsLocalUser())
		{
			BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
			if (player != null && BnetFriendMgr.Get().IsFriend(player))
			{
				ChatMgr.Get().AddRecentWhisperPlayerToBottom(player);
			}
		}
	}

	private void UpdateSessionRecord()
	{
		BnetPlayer player = BnetPresenceMgr.Get().GetPlayer(m_gameAccountId);
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
		if (sessionRecord != null)
		{
			if (sessionRecord.SessionRecordType == SessionRecordType.ARENA)
			{
				m_arenaWins = sessionRecord.Wins;
				m_arenaLoss = sessionRecord.Losses;
			}
			else if (sessionRecord.SessionRecordType == SessionRecordType.TAVERN_BRAWL)
			{
				m_tavernBrawlWins = sessionRecord.Wins;
				m_tavernBrawlLoss = sessionRecord.Losses;
			}
			else if (sessionRecord.SessionRecordType == SessionRecordType.DUELS)
			{
				m_duelsWins = sessionRecord.Wins;
				m_duelsLoss = sessionRecord.Losses;
			}
		}
	}

	private void OnBnetPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (changelist.FindChange(m_gameAccountId) != null && IsDisplayable())
		{
			BnetPresenceMgr.Get().RemovePlayersChangedListener(OnBnetPlayersChanged);
			UpdateDisplayInfo();
		}
	}

	private void UpdateLocal()
	{
		if (GameMgr.Get() != null && SpectatorManager.Get().IsSpectatingOrWatching)
		{
			m_local = false;
		}
		else if (IsBnetPlayer())
		{
			BnetGameAccountId myGameAccountId = BnetPresenceMgr.Get().GetMyGameAccountId();
			m_local = myGameAccountId == m_gameAccountId;
		}
		else
		{
			m_local = m_gameAccountId.GetLo() == 1;
		}
	}

	public void UpdateSide(int friendlySideTeamId)
	{
		if (GetTeamId() == friendlySideTeamId)
		{
			m_side = Side.FRIENDLY;
			GameState.Get().RegisterOptionsReceivedListener(OnFriendlyOptionsReceived);
			GameState.Get().RegisterOptionsSentListener(OnFriendlyOptionsSent);
			GameState.Get().RegisterFriendlyTurnStartedListener(OnFriendlyTurnStarted);
		}
		else
		{
			m_side = Side.OPPOSING;
		}
	}

	private void AssignPlayerBoardObjects()
	{
		if (!IsTeamLeader())
		{
			return;
		}
		ManaCounter[] componentsInChildren = Gameplay.Get().GetBoardLayout().gameObject.GetComponentsInChildren<ManaCounter>(includeInactive: true);
		foreach (ManaCounter manaCounter in componentsInChildren)
		{
			if (manaCounter.m_Side == m_side)
			{
				m_manaCounter = manaCounter;
				m_manaCounter.SetPlayer(this);
				m_manaCounter.UpdateText();
				break;
			}
		}
		InitManaCrystalMgr();
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone.m_Side == m_side)
			{
				zone.SetController(this);
			}
		}
	}

	private void InitManaCrystalMgr()
	{
		if (IsFriendlySide())
		{
			int tag = GetTag(GAME_TAG.TEMP_RESOURCES);
			int tag2 = GetTag(GAME_TAG.RESOURCES);
			int tag3 = GetTag(GAME_TAG.RESOURCES_USED);
			int tag4 = GetTag(GAME_TAG.OVERLOAD_OWED);
			int tag5 = GetTag(GAME_TAG.OVERLOAD_LOCKED);
			ManaCrystalMgr.Get().AddManaCrystals(tag2, isTurnStart: false);
			ManaCrystalMgr.Get().AddTempManaCrystals(tag);
			ManaCrystalMgr.Get().UpdateSpentMana(tag3);
			ManaCrystalMgr.Get().MarkCrystalsOwedForOverload(tag4);
			ManaCrystalMgr.Get().SetCrystalsLockedForOverload(tag5);
			ManaCrystalMgr.Get().ResetUnresolvedManaToBeReadied();
		}
	}

	private void OnTurnChanged(int oldTurn, int newTurn, object userData)
	{
		WipeZzzs();
		UpdateChooseBoth();
	}

	private void OnFriendlyOptionsReceived(object userData)
	{
		UpdateChooseBoth();
	}

	private void OnFriendlyOptionsSent(Network.Options.Option option, object userData)
	{
		UpdateChooseBoth();
		Entity entity = GameState.Get().GetEntity(option.Main.ID);
		CancelAllProposedMana(entity);
	}

	private void OnFriendlyTurnStarted(object userData)
	{
		UpdateChooseBoth();
	}

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

	private void UpdateHandCardPowersText(bool onlySpells)
	{
		List<Card> cards = GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (!(card.GetActor() == null) && (!onlySpells || card.GetEntity().IsSpell()))
			{
				card.GetActor().UpdatePowersText();
			}
		}
	}

	private void UpdateSpellsCostHealth(TagDelta change)
	{
		if (IsFriendlySide())
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
		List<Card> cards = GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			if (!card.CanShowActorVisuals())
			{
				continue;
			}
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

	private void UpdateChooseBoth()
	{
		List<Card> cards = GetHandZone().GetCards();
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			UpdateChooseBoth(card);
		}
		UpdateChooseBoth(GetHeroPowerCard());
	}

	private void UpdateChooseBoth(Card card)
	{
		if (card == null || !card.CanShowActorVisuals())
		{
			return;
		}
		Entity entity = card.GetEntity();
		if (entity.HasTag(GAME_TAG.CHOOSE_ONE))
		{
			Actor actor = card.GetActor();
			SpellType spellType = SpellType.CHOOSE_BOTH;
			if (HasTag(GAME_TAG.CHOOSE_BOTH) && GameState.Get().IsValidOption(entity))
			{
				SpellUtils.ActivateBirthIfNecessary(actor.GetSpell(spellType));
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(actor.GetSpellIfLoaded(spellType));
			}
		}
	}
}
