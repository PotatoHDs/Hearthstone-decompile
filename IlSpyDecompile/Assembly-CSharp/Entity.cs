using System;
using System.Collections.Generic;
using System.Linq;
using PegasusGame;
using UnityEngine;

public class Entity : EntityBase
{
	public enum LoadState
	{
		INVALID,
		PENDING,
		LOADING,
		DONE
	}

	public class LoadCardData
	{
		public bool updateActor;

		public bool restartStateSpells;

		public bool fromChangeEntity;
	}

	private struct CachedDebugName
	{
		public bool Dirty;

		public string Name;
	}

	private class EnchantmentComparer : IEqualityComparer<Entity>
	{
		public bool Equals(Entity a, Entity b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			return a.GetCardId() == b.GetCardId();
		}

		public int GetHashCode(Entity entity)
		{
			if (entity == null)
			{
				return 0;
			}
			if (entity.GetCardId() == null)
			{
				return 0;
			}
			return entity.GetCardId().GetHashCode();
		}
	}

	private EntityDef m_staticEntityDef = new EntityDef();

	private EntityDef m_dynamicEntityDef;

	private Card m_card;

	private LoadState m_loadState;

	private int m_cardAssetLoadCount;

	private bool m_useBattlecryPower;

	private bool m_duplicateForHistory;

	private CardTextHistoryData m_cardTextHistoryData;

	private List<Entity> m_attachments = new List<Entity>();

	private List<int> m_subCardIDs = new List<int>();

	private int m_realTimeCost;

	private int m_realTimeAttack;

	private int m_realTimeHealth;

	private int m_realTimeDamage;

	private int m_realTimeArmor;

	private int m_realTimeZone;

	private int m_realTimeZonePosition;

	private int m_realTimeLinkedEntityId;

	private bool m_realTimePoweredUp;

	private bool m_realTimeDivineShield;

	private bool m_realTimeIsImmune;

	private bool m_realTimeIsImmuneWhileAttacking;

	private bool m_realTimeIsPoisonous;

	private bool m_realTimeIsDormant;

	private int m_realTimeSpellpower;

	private bool m_realTimeSpellpowerDouble;

	private bool m_realTimeHealingDoesDamageHint;

	private bool m_realTimeLifestealDoesDamageHint;

	private bool m_realTimeCardCostsHealth;

	private bool m_realTimeAttackableByRush;

	private TAG_CARDTYPE m_realTimeCardType;

	private TAG_PREMIUM m_realTimePremium;

	private int m_realTimePlayerLeaderboardPlace;

	private int m_realTimePlayerTechLevel;

	private int m_queuedRealTimeControllerTagChangeCount;

	private int m_queuedChangeEntityCount;

	private List<Network.HistChangeEntity> m_transformPowersProcessed = new List<Network.HistChangeEntity>();

	private string m_displayedCreatorName;

	private string m_enchantmentCreatorCardIDForPortrait;

	private CachedDebugName m_cachedDebugName;

	public override string ToString()
	{
		return GetDebugName();
	}

	public virtual void OnRealTimeFullEntity(Network.HistFullEntity fullEntity)
	{
		SetTags(fullEntity.Entity.Tags);
		InitRealTimeValues(fullEntity.Entity.Tags);
		InitCard();
		LoadEntityDef(fullEntity.Entity.CardID);
	}

	public void OnFullEntity(Network.HistFullEntity fullEntity)
	{
		m_loadState = LoadState.PENDING;
		LoadCard(fullEntity.Entity.CardID);
		int tag = GetTag(GAME_TAG.ATTACHED);
		if (tag != 0)
		{
			GameState.Get().GetEntity(tag).AddAttachment(this);
		}
		int tag2 = GetTag(GAME_TAG.PARENT_CARD);
		if (tag2 != 0)
		{
			Entity entity = GameState.Get().GetEntity(tag2);
			if (entity != null)
			{
				entity.AddSubCard(this);
			}
			else
			{
				Log.Gameplay.PrintError("Unable to find parent entity id={0}", tag2);
			}
		}
		if (GetZone() == TAG_ZONE.PLAY)
		{
			if (IsHero())
			{
				GetController().SetHero(this);
			}
			else if (IsHeroPower())
			{
				GetController().SetHeroPower(this);
			}
		}
		if (fullEntity.Entity.DefTags.Count > 0)
		{
			EntityDef orCreateDynamicDefinition = GetOrCreateDynamicDefinition();
			for (int i = 0; i < fullEntity.Entity.DefTags.Count; i++)
			{
				orCreateDynamicDefinition.SetTag(fullEntity.Entity.DefTags[i].Name, fullEntity.Entity.DefTags[i].Value);
			}
		}
		if (HasTag(GAME_TAG.DISPLAYED_CREATOR))
		{
			SetDisplayedCreatorName(GetTag(GAME_TAG.DISPLAYED_CREATOR));
		}
		if (HasTag(GAME_TAG.CREATOR_DBID))
		{
			ResolveEnchantmentPortraitCardID(GetTag(GAME_TAG.CREATOR_DBID));
		}
		if (HasTag(GAME_TAG.PLAYER_LEADERBOARD_PLACE))
		{
			int tag3 = GetTag(GAME_TAG.PLAYER_ID);
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag3))
			{
				GameState.Get().GetPlayerInfoMap()[tag3].SetPlayerHero(this);
			}
			PlayerLeaderboardManager.Get().CreatePlayerTile(this);
			if (HasTag(GAME_TAG.REPLACEMENT_ENTITY))
			{
				PlayerLeaderboardManager.Get().ApplyEntityReplacement(tag3, this);
			}
		}
		if (HasTag(GAME_TAG.BACON_IS_KEL_THUZAD))
		{
			PlayerLeaderboardManager.Get().SetOddManOutOpponentHero(this);
		}
	}

	public virtual void OnRealTimeShowEntity(Network.HistShowEntity showEntity)
	{
		HandleRealTimeEntityChange(showEntity.Entity);
	}

	public void OnShowEntity(Network.HistShowEntity showEntity)
	{
		HandleEntityChange(showEntity.Entity, new LoadCardData
		{
			updateActor = false,
			restartStateSpells = false,
			fromChangeEntity = false
		}, fromShowEntity: true);
	}

	public void OnHideEntity(Network.HistHideEntity hideEntity)
	{
		SetTagAndHandleChange(GAME_TAG.ZONE, hideEntity.Zone);
		EntityDef entityDef = GetEntityDef();
		SetTag(GAME_TAG.ATK, entityDef.GetATK());
		SetTag(GAME_TAG.HEALTH, entityDef.GetHealth());
		SetTag(GAME_TAG.COST, entityDef.GetCost());
		SetTag(GAME_TAG.DAMAGE, 0);
		SetCardId(null);
	}

	public virtual void OnRealTimeChangeEntity(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		m_queuedChangeEntityCount++;
		HandleRealTimeEntityChange(changeEntity.Entity);
		CheckRealTimeTransform(powerList, index, changeEntity);
	}

	public void OnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (m_transformPowersProcessed.Contains(changeEntity))
		{
			m_transformPowersProcessed.Remove(changeEntity);
			return;
		}
		m_subCardIDs.Clear();
		m_queuedChangeEntityCount--;
		LoadCardData data = new LoadCardData
		{
			updateActor = ShouldUpdateActorOnChangeEntity(changeEntity),
			restartStateSpells = ShouldRestartStateSpellsOnChangeEntity(changeEntity),
			fromChangeEntity = true
		};
		HandleEntityChange(changeEntity.Entity, data, fromShowEntity: false);
	}

	private bool IsTagChanged(Network.HistChangeEntity changeEntity, GAME_TAG tag)
	{
		Network.Entity.Tag tag2 = changeEntity.Entity.Tags.Find((Network.Entity.Tag currTag) => currTag.Name == (int)tag);
		if (tag2 != null)
		{
			return GetTag(tag) != tag2.Value;
		}
		return false;
	}

	private bool ShouldUpdateActorOnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (!IsTagChanged(changeEntity, GAME_TAG.CARDTYPE) && GetTag(GAME_TAG.CARDTYPE) == (int)m_realTimeCardType && !IsTagChanged(changeEntity, GAME_TAG.PREMIUM))
		{
			return GetTag(GAME_TAG.PREMIUM) != (int)m_realTimePremium;
		}
		return true;
	}

	private bool ShouldRestartStateSpellsOnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		return IsTagChanged(changeEntity, GAME_TAG.ELITE);
	}

	public virtual void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		switch (change.Tag)
		{
		case 48:
			SetRealTimeCost(change.Value);
			break;
		case 47:
			SetRealTimeAttack(change.Value);
			break;
		case 45:
		case 187:
			SetRealTimeHealth(change.Value);
			break;
		case 44:
			SetRealTimeDamage(change.Value);
			break;
		case 292:
			SetRealTimeArmor(change.Value);
			break;
		case 49:
			SetRealTimeZone(change.Value);
			break;
		case 263:
			SetRealTimeZonePosition(change.Value);
			break;
		case 386:
			SetRealTimePoweredUp(change.Value);
			break;
		case 262:
			SetRealTimeLinkedEntityId(change.Value);
			break;
		case 194:
			SetRealTimeDivineShield(change.Value);
			break;
		case 240:
			SetRealTimeIsImmune(change.Value);
			break;
		case 373:
			SetRealTimeIsImmuneWhileAttacking(change.Value);
			break;
		case 363:
		case 1944:
			SetRealTimeIsPoisonous(change.Value);
			break;
		case 192:
			SetRealTimeHasSpellpower(change.Value);
			break;
		case 356:
			SetRealTimeSpellpowerDouble(change.Value);
			break;
		case 1117:
			SetRealTimeHealingDoesDamageHint(change.Value);
			break;
		case 1774:
			SetRealTimeLifestealDoesDamageHint(change.Value);
			break;
		case 1518:
			SetRealTimeIsDormant(change.Value);
			break;
		case 202:
			SetRealTimeCardType((TAG_CARDTYPE)change.Value);
			break;
		case 481:
			SetRealTimeCardCostsHealth(change.Value);
			break;
		case 930:
			SetRealTimeAttackableByRush(change.Value);
			break;
		case 984:
			OnRealTimePuzzleCompleted(change.Value);
			break;
		case 12:
			SetRealTimePremium((TAG_PREMIUM)change.Value);
			break;
		case 1373:
			SetRealTimePlayerLeaderboardPlace(change.Value);
			UpdateSharedPlayer();
			break;
		case 1377:
			SetRealTimePlayerTechLevel(change.Value);
			PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.TECH_LEVEL);
			break;
		case 1447:
			PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.TRIPLE);
			break;
		case 1629:
			PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.BANANA);
			break;
		case 50:
			m_queuedRealTimeControllerTagChangeCount++;
			break;
		}
	}

	private void UpdateSharedPlayer()
	{
		PlayerLeaderboardManager.Get().CreatePlayerTile(this);
		int tag = GetTag(GAME_TAG.PLAYER_ID);
		if (tag == 0)
		{
			tag = GetTag(GAME_TAG.CONTROLLER);
		}
		if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag) && GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero() == null)
		{
			GameState.Get().GetPlayerInfoMap()[tag].SetPlayerHero(this);
		}
	}

	public void OnRealTimePuzzleCompleted(int newValue)
	{
		if (IsPuzzle() && !(m_card == null) && !(m_card.GetActor() == null))
		{
			PuzzleController component = m_card.GetActor().GetComponent<PuzzleController>();
			if (component == null)
			{
				Log.Gameplay.PrintError("Puzzle card {0} does not have a PuzzleController component.", this);
			}
			else
			{
				component.OnRealTimePuzzleCompleted(newValue);
			}
		}
	}

	public virtual void HandlePreTransformTagChanges(TagDeltaList changeList)
	{
		if (m_card != null)
		{
			m_card.DeactivateCustomKeywordEffect();
		}
	}

	public virtual void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		bool flag = false;
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			if (IsNameChange(change))
			{
				flag = true;
			}
			HandleTagChange(change);
		}
		if (!(m_card == null))
		{
			if (flag)
			{
				UpdateCardName();
			}
			m_card.OnTagsChanged(changeList, fromShowEntity);
		}
	}

	public virtual void OnTagChanged(TagDelta change)
	{
		HandleTagChange(change);
		if (!(m_card == null))
		{
			if (IsNameChange(change))
			{
				UpdateCardName();
			}
			m_card.OnTagChanged(change, fromShowEntity: false);
		}
	}

	public virtual void OnCachedTagForDormantChanged(TagDelta change)
	{
		SetCachedTagForDormant(change.tag, change.newValue);
	}

	protected override void OnUpdateCardId()
	{
		UpdateCardName();
	}

	public virtual void OnMetaData(Network.HistMetaData metaData)
	{
		if (!(m_card == null))
		{
			m_card.OnMetaData(metaData);
		}
	}

	private void HandleRealTimeEntityChange(Network.Entity netEntity)
	{
		InitRealTimeValues(netEntity.Tags);
	}

	private bool HasRealTimeTransformTag(Network.Entity netEntity)
	{
		foreach (Network.Entity.Tag tag in netEntity.Tags)
		{
			if (tag.Name == 859 && tag.Value == 1)
			{
				return true;
			}
		}
		return false;
	}

	private void CheckRealTimeTransform(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		if (HasRealTimeTransformTag(changeEntity.Entity) && CanRealTimeTransform(powerList, index))
		{
			OnChangeEntity(changeEntity);
			m_transformPowersProcessed.Add(changeEntity);
		}
	}

	private bool CanRealTimeTransform(List<Network.PowerHistory> powerList, int index)
	{
		for (int i = 0; i < index; i++)
		{
			Network.PowerHistory power = powerList[i];
			if (!CheckPowerHistoryForRealTimeTransform(power))
			{
				return false;
			}
		}
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (!CheckPowerTaskListForRealTimeTransform(item))
			{
				return false;
			}
		}
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (!CheckPowerTaskListForRealTimeTransform(currentTaskList))
		{
			return false;
		}
		return true;
	}

	private bool CheckPowerHistoryForRealTimeTransform(Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
			if (((Network.HistFullEntity)power).Entity.ID == GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.SHOW_ENTITY:
			if (((Network.HistShowEntity)power).Entity.ID == GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.HIDE_ENTITY:
			if (((Network.HistHideEntity)power).Entity == GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			if (histTagChange.Entity == GetEntityId() && histTagChange.Tag != 263 && histTagChange.Tag != 385 && histTagChange.Tag != 466)
			{
				return false;
			}
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)power;
			if (histChangeEntity.Entity.ID == GetEntityId() && !m_transformPowersProcessed.Contains(histChangeEntity))
			{
				return false;
			}
			break;
		}
		case Network.PowerType.META_DATA:
		{
			Network.HistMetaData metaDataEntity = (Network.HistMetaData)power;
			if (!CheckPowerHistoryMetaDataForRealTimeTransform(metaDataEntity))
			{
				return false;
			}
			break;
		}
		}
		return true;
	}

	private bool CheckPowerHistoryMetaDataForRealTimeTransform(Network.HistMetaData metaDataEntity)
	{
		switch (metaDataEntity.MetaType)
		{
		case HistoryMeta.Type.TARGET:
		case HistoryMeta.Type.DAMAGE:
		case HistoryMeta.Type.HEALING:
		case HistoryMeta.Type.JOUST:
		case HistoryMeta.Type.HISTORY_TARGET:
			foreach (int item in metaDataEntity.Info)
			{
				if (item == GetEntityId())
				{
					return false;
				}
			}
			break;
		case HistoryMeta.Type.SHOW_BIG_CARD:
		case HistoryMeta.Type.EFFECT_TIMING:
		case HistoryMeta.Type.OVERRIDE_HISTORY:
		case HistoryMeta.Type.HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END:
		case HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TILE:
		case HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE:
		case HistoryMeta.Type.BURNED_CARD:
			if (metaDataEntity.Info.Count > 0 && metaDataEntity.Info[0] == GetEntityId())
			{
				return false;
			}
			break;
		}
		return true;
	}

	private bool CheckPowerTaskListForRealTimeTransform(PowerTaskList powerTaskList)
	{
		if (powerTaskList == null)
		{
			return true;
		}
		foreach (PowerTask task in powerTaskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (!task.IsCompleted() && !CheckPowerHistoryForRealTimeTransform(power))
			{
				return false;
			}
		}
		return true;
	}

	private void HandleEntityChange(Network.Entity netEntity, LoadCardData data, bool fromShowEntity)
	{
		TagDeltaList changeList = m_tags.CreateDeltas(netEntity.Tags);
		SetTags(netEntity.Tags);
		HandlePreTransformTagChanges(changeList);
		if (m_card != null)
		{
			m_card.DestroyCardDefAssetsOnEntityChanged();
		}
		LoadCard(netEntity.CardID, data);
		if (GetZone() == TAG_ZONE.HAND && GetCard() != null && GetCard().GetZone() != null)
		{
			if (data.updateActor)
			{
				GetCard().GetZone().UpdateLayout();
			}
			GetCard().UpdateActorState(forceHighlightRefresh: true);
		}
		if (netEntity.DefTags.Count > 0)
		{
			EntityDef orCreateDynamicDefinition = GetOrCreateDynamicDefinition();
			for (int i = 0; i < netEntity.DefTags.Count; i++)
			{
				orCreateDynamicDefinition.SetTag(netEntity.DefTags[i].Name, netEntity.DefTags[i].Value);
			}
		}
		OnTagsChanged(changeList, fromShowEntity);
	}

	private void HandleTagChange(TagDelta change)
	{
		switch (change.tag)
		{
		case 49:
			UpdateUseBattlecryFlag(fromGameState: false);
			if (GameState.Get().IsTurnStartManagerActive() && change.oldValue == 2 && change.newValue == 3)
			{
				PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
				if (currentTaskList != null && currentTaskList.GetSourceEntity() == GameState.Get().GetFriendlySidePlayer())
				{
					TurnStartManager.Get().NotifyOfCardDrawn(this);
				}
			}
			if (change.newValue == 1)
			{
				if (IsHero())
				{
					GetController().SetHero(this);
				}
				else if (IsHeroPower())
				{
					GetController().SetHeroPower(this);
				}
			}
			CheckZoneChangeForEnchantment(change);
			if (change.newValue == 5)
			{
				GameState.Get().GetGameEntity().QueueEntityForRemoval(this);
			}
			break;
		case 40:
			GameState.Get().GetEntity(change.oldValue)?.RemoveAttachment(this);
			GameState.Get().GetEntity(change.newValue)?.AddAttachment(this);
			break;
		case 316:
			GameState.Get().GetEntity(change.oldValue)?.RemoveSubCard(this);
			GameState.Get().GetEntity(change.newValue)?.AddSubCard(this);
			break;
		case 50:
		{
			Entity parentEntity = GetParentEntity();
			if (parentEntity != null)
			{
				if (GameState.Get().GetFriendlyPlayerId() != change.newValue)
				{
					parentEntity.RemoveSubCard(this);
				}
				else
				{
					parentEntity.AddSubCard(this);
				}
			}
			if (IsHeroPower())
			{
				GetController().SetHeroPower(this);
			}
			m_queuedRealTimeControllerTagChangeCount--;
			break;
		}
		case 385:
			SetDisplayedCreatorName(change.newValue);
			break;
		case 1284:
			ResolveEnchantmentPortraitCardID(change.newValue);
			break;
		case 380:
		case 1646:
		{
			PlayerLeaderboardManager playerLeaderboardManager = PlayerLeaderboardManager.Get();
			if (playerLeaderboardManager != null && playerLeaderboardManager.IsEnabled())
			{
				playerLeaderboardManager.UpdatePlayerTileHeroPower(this, change.newValue);
			}
			break;
		}
		}
	}

	private void SetDisplayedCreatorName(int entityID)
	{
		Entity entity = GameState.Get().GetEntity(entityID);
		if (entity == null)
		{
			m_displayedCreatorName = null;
		}
		else if (string.IsNullOrEmpty(entity.m_cardId))
		{
			m_displayedCreatorName = GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
		}
		else
		{
			m_displayedCreatorName = entity.GetName();
		}
	}

	private bool HasEnchantmentPortrait(string enchantmentPortraitCardID)
	{
		if (string.IsNullOrEmpty(enchantmentPortraitCardID))
		{
			return false;
		}
		using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(enchantmentPortraitCardID);
		if (disposableCardDef == null)
		{
			return false;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(enchantmentPortraitCardID);
		if (entityDef == null)
		{
			return false;
		}
		if (entityDef.GetCardType() == TAG_CARDTYPE.ENCHANTMENT)
		{
			if (disposableCardDef.CardDef.GetEnchantmentPortrait() != null)
			{
				return true;
			}
			if (disposableCardDef.CardDef.GetPortraitTexture() != null)
			{
				return true;
			}
		}
		else
		{
			if (disposableCardDef.CardDef.GetHistoryTileFullPortrait() != null)
			{
				return true;
			}
			if (disposableCardDef.CardDef.GetPortraitTexture() != null)
			{
				return true;
			}
		}
		return false;
	}

	public string GetEnchantmentCreatorCardIDForPortrait()
	{
		return m_enchantmentCreatorCardIDForPortrait;
	}

	private void ResolveEnchantmentPortraitCardID(int creatorDBID)
	{
		m_enchantmentCreatorCardIDForPortrait = null;
		if (!IsEnchantment())
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(creatorDBID);
		if (entityDef == null)
		{
			return;
		}
		m_enchantmentCreatorCardIDForPortrait = entityDef.GetCardId();
		Entity creator = GetCreator();
		while (!HasEnchantmentPortrait(m_enchantmentCreatorCardIDForPortrait))
		{
			if (creator == null || (!creator.IsEnchantment() && creator.GetCardType() != 0))
			{
				m_enchantmentCreatorCardIDForPortrait = null;
				return;
			}
			entityDef = creator.GetCreatorDef();
			creator = creator.GetCreator();
			if (entityDef == null)
			{
				m_enchantmentCreatorCardIDForPortrait = null;
				return;
			}
			m_enchantmentCreatorCardIDForPortrait = entityDef.GetCardId();
		}
		Entity entity = GameState.Get().GetEntity(GetAttached());
		if (entity != null && entity.m_card != null)
		{
			entity.m_card.UpdateTooltip();
		}
	}

	private void CheckZoneChangeForEnchantment(TagDelta change)
	{
		if (change.tag == 49 && IsEnchantment() && change.oldValue != change.newValue && (change.newValue == 5 || change.newValue == 4))
		{
			GameState.Get().GetEntity(GetAttached())?.RemoveAttachment(this);
			if (m_card != null)
			{
				m_card.Destroy();
			}
		}
	}

	private bool IsNameChange(TagDelta change)
	{
		switch (change.tag)
		{
		case 49:
		case 50:
		case 53:
		case 202:
		case 263:
		case 781:
			return true;
		default:
			return false;
		}
	}

	public EntityDef GetEntityDef()
	{
		if (m_dynamicEntityDef == null)
		{
			return m_staticEntityDef;
		}
		return m_dynamicEntityDef;
	}

	public EntityDef GetOrCreateDynamicDefinition()
	{
		if (m_dynamicEntityDef == null)
		{
			m_dynamicEntityDef = m_staticEntityDef.Clone();
			m_staticEntityDef = null;
		}
		return m_dynamicEntityDef;
	}

	public Card InitCard()
	{
		GameObject gameObject = new GameObject();
		m_card = gameObject.AddComponent<Card>();
		m_card.SetEntity(this);
		UpdateCardName();
		return m_card;
	}

	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		if (m_duplicateForHistory)
		{
			return GetCardDefForHistory();
		}
		if (m_card != null)
		{
			return m_card.ShareDisposableCardDef();
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			return DefLoader.Get().GetCardDef(base.m_cardId);
		}
		return null;
	}

	private DefLoader.DisposableCardDef GetCardDefForHistory()
	{
		if (m_card != null)
		{
			if (IsHidden() && !m_card.HasHiddenCardDef)
			{
				return DefLoader.Get().GetCardDef("HiddenCard");
			}
			if (base.m_cardId == m_card.GetEntity().GetCardId())
			{
				return m_card.ShareDisposableCardDef();
			}
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			return DefLoader.Get().GetCardDef(base.m_cardId);
		}
		return DefLoader.Get().GetCardDef("HiddenCard");
	}

	public Card GetCard()
	{
		return m_card;
	}

	public void SetCard(Card card)
	{
		m_card = card;
	}

	public void Destroy()
	{
		if (m_card != null)
		{
			m_card.Destroy();
		}
	}

	public LoadState GetLoadState()
	{
		return m_loadState;
	}

	public bool IsLoadingAssets()
	{
		return m_loadState == LoadState.LOADING;
	}

	public bool IsBusy()
	{
		if (IsLoadingAssets())
		{
			return true;
		}
		if (m_card != null && !m_card.IsActorReady())
		{
			return true;
		}
		return false;
	}

	public bool IsHidden()
	{
		return string.IsNullOrEmpty(base.m_cardId);
	}

	public bool HasQueuedChangeEntity()
	{
		return m_queuedChangeEntityCount > 0;
	}

	public bool HasQueuedControllerTagChange()
	{
		return m_queuedRealTimeControllerTagChangeCount > 0;
	}

	public void ClearTags()
	{
		m_tags.Clear();
	}

	public void SetTagAndHandleChange<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		SetTagAndHandleChange((int)tag, Convert.ToInt32(tagValue));
	}

	public TagDelta SetTagAndHandleChange(int tag, int tagValue)
	{
		int tag2 = m_tags.GetTag(tag);
		SetTag(tag, tagValue);
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = tag;
		tagDelta.oldValue = tag2;
		tagDelta.newValue = tagValue;
		OnTagChanged(tagDelta);
		return tagDelta;
	}

	public override int GetReferencedTag(int tag)
	{
		return GetEntityDef().GetReferencedTag(tag);
	}

	public int GetDefCost()
	{
		return GetEntityDef().GetCost();
	}

	public int GetDefATK()
	{
		return GetEntityDef().GetATK();
	}

	public int GetDefHealth()
	{
		return GetEntityDef().GetHealth();
	}

	public int GetDefDurability()
	{
		return GetEntityDef().GetDurability();
	}

	public bool HasRace(TAG_RACE race)
	{
		TAG_RACE tAG_RACE = (HasTag(GAME_TAG.CARDRACE) ? GetTag<TAG_RACE>(GAME_TAG.CARDRACE) : GetEntityDef().GetRace());
		if (tAG_RACE == TAG_RACE.ALL && race != 0)
		{
			return true;
		}
		return tAG_RACE == race;
	}

	public override TAG_CLASS GetClass()
	{
		if (IsSecret())
		{
			return base.GetClass();
		}
		return GetEntityDef().GetClass();
	}

	public override IEnumerable<TAG_CLASS> GetClasses(Comparison<TAG_CLASS> classSorter = null)
	{
		if (IsSecret())
		{
			return base.GetClasses();
		}
		return GetEntityDef().GetClasses();
	}

	public TAG_ENCHANTMENT_VISUAL GetEnchantmentBirthVisual()
	{
		return GetEntityDef().GetEnchantmentBirthVisual();
	}

	public TAG_ENCHANTMENT_VISUAL GetEnchantmentIdleVisual()
	{
		return GetEntityDef().GetEnchantmentIdleVisual();
	}

	public TAG_RARITY GetRarity()
	{
		return GetEntityDef().GetRarity();
	}

	public new TAG_CARD_SET GetCardSet()
	{
		return GetEntityDef().GetCardSet();
	}

	public TAG_PREMIUM GetPremiumType()
	{
		return (TAG_PREMIUM)GetTag(GAME_TAG.PREMIUM);
	}

	public bool CanBeDamagedRealTime()
	{
		if (GetRealTimeDivineShield() || GetRealTimeIsImmune())
		{
			return false;
		}
		if (GetRealTimeIsImmuneWhileAttacking() && (bool)TargetReticleManager.Get() && TargetReticleManager.Get().ArrowSourceEntityID == GetEntityId())
		{
			return false;
		}
		return true;
	}

	public int GetCurrentHealth()
	{
		return GetTag(GAME_TAG.HEALTH) - GetTag(GAME_TAG.DAMAGE) - GetTag(GAME_TAG.PREDAMAGE);
	}

	public int GetCurrentDurability()
	{
		return GetTag(GAME_TAG.DURABILITY) - GetTag(GAME_TAG.DAMAGE) - GetTag(GAME_TAG.PREDAMAGE);
	}

	public int GetCurrentDefense()
	{
		return GetCurrentHealth() + GetArmor();
	}

	public int GetCurrentVitality()
	{
		if (IsCharacter())
		{
			return GetCurrentDefense();
		}
		if (IsWeapon())
		{
			return GetCurrentDurability();
		}
		Error.AddDevFatal("Entity.GetCurrentVitality() should not be called on {0}. This entity is neither a character nor a weapon.", this);
		return 0;
	}

	public Player GetController()
	{
		return GameState.Get().GetPlayer(GetControllerId());
	}

	public Player.Side GetControllerSide()
	{
		return GetController()?.GetSide() ?? Player.Side.NEUTRAL;
	}

	public bool IsControlledByLocalUser()
	{
		return GetController().IsLocalUser();
	}

	public bool IsControlledByFriendlySidePlayer()
	{
		return GetController().IsFriendlySide();
	}

	public bool IsControlledByOpposingSidePlayer()
	{
		return GetController().IsOpposingSide();
	}

	public bool IsControlledByRevealedPlayer()
	{
		return GetController().IsRevealed();
	}

	public bool IsControlledByConcealedPlayer()
	{
		return !IsControlledByRevealedPlayer();
	}

	public Entity GetCreator()
	{
		return GameState.Get().GetEntity(GetCreatorId());
	}

	public EntityDef GetCreatorDef()
	{
		return DefLoader.Get().GetEntityDef(GetCreatorDBID());
	}

	public string GetDisplayedCreatorName()
	{
		return m_displayedCreatorName;
	}

	public virtual Entity GetHero()
	{
		if (IsHero())
		{
			return this;
		}
		return GetController()?.GetHero();
	}

	public virtual Card GetHeroCard()
	{
		if (IsHero())
		{
			return GetCard();
		}
		return GetController()?.GetHeroCard();
	}

	public virtual Entity GetHeroPower()
	{
		if (IsHeroPower())
		{
			return this;
		}
		return GetController()?.GetHeroPower();
	}

	public virtual Card GetHeroPowerCard()
	{
		if (IsHeroPower())
		{
			return GetCard();
		}
		return GetController()?.GetHeroPowerCard();
	}

	public virtual Card GetWeaponCard()
	{
		if (IsWeapon())
		{
			return GetCard();
		}
		return GetController()?.GetWeaponCard();
	}

	public virtual bool HasValidDisplayName()
	{
		return GetEntityDef().HasValidDisplayName();
	}

	public virtual string GetName()
	{
		int tag = GetTag(GAME_TAG.OVERRIDECARDNAME);
		EntityDef entityDef;
		if (tag > 0)
		{
			entityDef = DefLoader.Get().GetEntityDef(tag);
			if (entityDef != null)
			{
				return entityDef.GetName();
			}
		}
		entityDef = GetEntityDef();
		if (entityDef != null && entityDef.GetCardTextBuilder() != null)
		{
			return entityDef.GetCardTextBuilder().BuildCardName(this);
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			Debug.LogWarning($"Entity.GetName: No textbuilder found for {base.m_cardId}, returning default name");
		}
		return CardTextBuilder.GetDefaultCardName(GetEntityDef());
	}

	public virtual string GetDebugName()
	{
		if (m_cachedDebugName.Dirty)
		{
			string name = GetEntityDef().GetName();
			if (name != null)
			{
				m_cachedDebugName.Name = $"[entityName={name} id={GetEntityId()} zone={GetZone()} zonePos={GetZonePosition()} cardId={base.m_cardId} player={GetControllerId()}]";
			}
			else if (base.m_cardId != null)
			{
				m_cachedDebugName.Name = $"[id={GetEntityId()} cardId={base.m_cardId} type={GetCardType()} zone={GetZone()} zonePos={GetZonePosition()} player={GetControllerId()}]";
			}
			else
			{
				m_cachedDebugName.Name = $"UNKNOWN ENTITY [id={GetEntityId()} type={GetCardType()} zone={GetZone()} zonePos={GetZonePosition()}]";
			}
			m_cachedDebugName.Dirty = false;
		}
		return m_cachedDebugName.Name;
	}

	public void UpdateCardName()
	{
		m_cachedDebugName.Dirty = true;
		if (m_card == null)
		{
			return;
		}
		string name = GetEntityDef().GetName();
		if (name != null)
		{
			if (string.IsNullOrEmpty(base.m_cardId))
			{
				m_card.gameObject.name = $"{name} [id={GetEntityId()} zone={GetZone()} zonePos={GetZonePosition()}]";
			}
			else
			{
				m_card.gameObject.name = $"{name} [id={GetEntityId()} cardId={GetCardId()} zone={GetZone()} zonePos={GetZonePosition()} player={GetControllerId()}]";
			}
		}
		else
		{
			m_card.gameObject.name = $"Hidden Entity [id={GetEntityId()} zone={GetZone()} zonePos={GetZonePosition()}]";
		}
		if (m_card.GetActor() != null)
		{
			m_card.GetActor().UpdateNameText();
		}
	}

	public string GetCardTextInHand()
	{
		using DefLoader.DisposableCardDef disposableCardDef = ShareDisposableCardDef();
		if (disposableCardDef?.CardDef == null)
		{
			Log.All.PrintError("Entity.GetCardTextInHand(): entity {0} does not have a CardDef", GetEntityId());
			return string.Empty;
		}
		return GetCardTextBuilder().BuildCardTextInHand(this);
	}

	public string GetCardTextInHistory()
	{
		using DefLoader.DisposableCardDef disposableCardDef = ShareDisposableCardDef();
		if (disposableCardDef?.CardDef == null)
		{
			Log.All.PrintError("Entity.GetCardTextInHand(): entity {0} does not have a CardDef", GetEntityId());
			return string.Empty;
		}
		return GetCardTextBuilder().BuildCardTextInHistory(this);
	}

	public string GetTargetingArrowText()
	{
		using DefLoader.DisposableCardDef disposableCardDef = ShareDisposableCardDef();
		if (disposableCardDef?.CardDef == null)
		{
			Log.All.PrintError("Entity.GetTargetingArrowText(): entity {0} does not have a CardDef", GetEntityId());
			return string.Empty;
		}
		return GetCardTextBuilder().GetTargetingArrowText(this);
	}

	public string GetRaceText()
	{
		return GetEntityDef().GetRaceText();
	}

	public void AddAttachment(Entity entity)
	{
		int count = m_attachments.Count;
		if (m_attachments.Contains(entity))
		{
			Log.Gameplay.Print($"Entity.AddAttachment() - {entity} is already an attachment of {this}");
			return;
		}
		m_attachments.Add(entity);
		if (!(m_card == null))
		{
			m_card.OnEnchantmentAdded(count, entity);
		}
	}

	public void RemoveAttachment(Entity entity)
	{
		int count = m_attachments.Count;
		if (!m_attachments.Remove(entity))
		{
			Log.Gameplay.Print("Entity.RemoveAttachment() - {0} is not an attachment of {1}", entity, this);
		}
		else if (!(m_card == null))
		{
			m_card.OnEnchantmentRemoved(count, entity);
		}
	}

	private void AddSubCard(Entity entity)
	{
		if (!m_subCardIDs.Contains(entity.GetEntityId()))
		{
			m_subCardIDs.Add(entity.GetEntityId());
		}
	}

	private void RemoveSubCard(Entity entity)
	{
		m_subCardIDs.Remove(entity.GetEntityId());
	}

	public List<Entity> GetAttachments()
	{
		return m_attachments;
	}

	public bool DoEnchantmentsHaveVoodooLink()
	{
		foreach (Entity attachment in m_attachments)
		{
			if (attachment.HasTag(GAME_TAG.VOODOO_LINK))
			{
				return true;
			}
		}
		return false;
	}

	public bool DoEnchantmentsHaveTriggerVisuals()
	{
		foreach (Entity attachment in m_attachments)
		{
			if (attachment.HasTriggerVisual())
			{
				return true;
			}
		}
		return false;
	}

	public bool DoEnchantmentsHaveOverKill()
	{
		foreach (Entity attachment in m_attachments)
		{
			if (attachment.HasTag(GAME_TAG.OVERKILL))
			{
				return true;
			}
		}
		return false;
	}

	public bool DoEnchantmentsHaveSpellburst()
	{
		foreach (Entity attachment in m_attachments)
		{
			if (attachment.HasSpellburst())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsEnchanted()
	{
		return m_attachments.Count > 0;
	}

	public bool IsEnchantment()
	{
		return GetRealTimeCardType() == TAG_CARDTYPE.ENCHANTMENT;
	}

	public bool IsDarkWandererSecret()
	{
		if (IsSecret())
		{
			return GetClass() == TAG_CLASS.WARRIOR;
		}
		return false;
	}

	public bool IsDeathrattleDisabled()
	{
		if (GetController() != null)
		{
			return GetController().HasTag(GAME_TAG.CANT_TRIGGER_DEATHRATTLE);
		}
		return false;
	}

	public List<Entity> GetEnchantments()
	{
		return GetAttachments();
	}

	public List<Entity> GetDisplayedEnchantments(bool unique = false)
	{
		List<Entity> list = new List<Entity>(GetAttachments());
		list.RemoveAll((Entity enchant) => enchant.HasTag(GAME_TAG.ENCHANTMENT_INVISIBLE));
		if (!unique)
		{
			return list;
		}
		return list.Distinct(new EnchantmentComparer()).ToList();
	}

	public bool HasSubCards()
	{
		if (m_subCardIDs != null)
		{
			return m_subCardIDs.Count > 0;
		}
		return false;
	}

	public List<int> GetSubCardIDs()
	{
		return m_subCardIDs;
	}

	public int GetSubCardIndex(Entity entity)
	{
		if (entity == null)
		{
			return -1;
		}
		int entityId = entity.GetEntityId();
		for (int i = 0; i < m_subCardIDs.Count; i++)
		{
			if (m_subCardIDs[i] == entityId)
			{
				return i;
			}
		}
		return -1;
	}

	public Entity GetParentEntity()
	{
		int tag = GetTag(GAME_TAG.PARENT_CARD);
		return GameState.Get().GetEntity(tag);
	}

	public CardTextBuilder GetCardTextBuilder()
	{
		if (GetEntityDef() != null && GetEntityDef().GetCardTextBuilder() != null)
		{
			return GetEntityDef().GetCardTextBuilder();
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			Debug.LogWarning($"Entity.GetCardTextBuilder: No textbuilder found for {base.m_cardId}, returning fallback text builder");
		}
		return CardTextBuilder.GetFallbackCardTextBuilder();
	}

	public Entity CloneForZoneMgr()
	{
		Entity entity = new Entity();
		entity.m_staticEntityDef = GetEntityDef();
		entity.m_dynamicEntityDef = null;
		entity.m_card = m_card;
		entity.m_cardId = base.m_cardId;
		entity.ReplaceTags(m_tags);
		entity.m_loadState = m_loadState;
		return entity;
	}

	public Entity CloneForHistory(HistoryInfo historyInfo)
	{
		Entity entity = new Entity();
		entity.m_duplicateForHistory = true;
		entity.m_staticEntityDef = GetEntityDef();
		entity.m_dynamicEntityDef = null;
		entity.m_card = m_card;
		entity.m_cardId = base.m_cardId;
		entity.ReplaceTags(m_tags);
		entity.m_cardTextHistoryData = GetCardTextBuilder().CreateCardTextHistoryData();
		entity.m_cardTextHistoryData.SetHistoryData(this, historyInfo);
		entity.m_subCardIDs = m_subCardIDs;
		if (!IsHero())
		{
			entity.SetTag(GAME_TAG.ZONE, TAG_ZONE.HAND);
		}
		entity.m_loadState = m_loadState;
		entity.m_displayedCreatorName = m_displayedCreatorName;
		entity.m_enchantmentCreatorCardIDForPortrait = m_enchantmentCreatorCardIDForPortrait;
		return entity;
	}

	public bool IsHistoryDupe()
	{
		return m_duplicateForHistory;
	}

	public int GetJadeGolem()
	{
		return Mathf.Min(GetController().GetTag(GAME_TAG.JADE_GOLEM) + 1, 30);
	}

	public int GetDamageBonus()
	{
		Player controller = GetController();
		if (HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		if (IsSpell() || IsMinion())
		{
			int num = controller.TotalSpellpower(GetSpellSchool());
			if (HasTag(GAME_TAG.RECEIVES_DOUBLE_SPELLDAMAGE_BONUS))
			{
				num *= 2;
			}
			return num;
		}
		if (IsHeroPower())
		{
			int num2 = controller.GetTag(GAME_TAG.CURRENT_HEROPOWER_DAMAGE_BONUS);
			if (GetCardTextBuilder() is SpellDamageOnlyCardTextBuilder)
			{
				int num3 = controller.TotalSpellpower(GetSpellSchool());
				if (HasTag(GAME_TAG.RECEIVES_DOUBLE_SPELLDAMAGE_BONUS))
				{
					num3 *= 2;
				}
				num2 += num3;
			}
			return num2;
		}
		return 0;
	}

	public int GetDamageBonusDouble()
	{
		Player controller = GetController();
		if (HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		if (IsSpell())
		{
			return controller.GetTag(GAME_TAG.SPELLPOWER_DOUBLE);
		}
		if (IsHeroPower())
		{
			return controller.GetTag(GAME_TAG.HERO_POWER_DOUBLE);
		}
		return 0;
	}

	public int GetHealingDouble()
	{
		Player controller = GetController();
		if (HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		int tag = controller.GetTag(GAME_TAG.ALL_HEALING_DOUBLE);
		if (IsSpell())
		{
			return controller.GetTag(GAME_TAG.SPELL_HEALING_DOUBLE) + tag;
		}
		if (IsHeroPower())
		{
			return controller.GetTag(GAME_TAG.HERO_POWER_DOUBLE) + tag;
		}
		return tag;
	}

	public void ClearBattlecryFlag()
	{
		m_useBattlecryPower = false;
	}

	public bool ShouldUseBattlecryPower()
	{
		return m_useBattlecryPower;
	}

	public void UpdateUseBattlecryFlag(bool fromGameState)
	{
		if (IsMinion())
		{
			bool flag = fromGameState || GameState.Get().EntityHasTargets(this);
			if (TAG_ZONE.HAND == GetZone() && flag)
			{
				m_useBattlecryPower = true;
			}
		}
	}

	public virtual void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		foreach (Network.Entity.Tag tag in tags)
		{
			switch (tag.Name)
			{
			case 48:
				SetRealTimeCost(tag.Value);
				break;
			case 47:
				SetRealTimeAttack(tag.Value);
				break;
			case 45:
			case 187:
				SetRealTimeHealth(tag.Value);
				break;
			case 44:
				SetRealTimeDamage(tag.Value);
				break;
			case 292:
				SetRealTimeArmor(tag.Value);
				break;
			case 49:
				SetRealTimeZone(tag.Value);
				break;
			case 263:
				SetRealTimeZonePosition(tag.Value);
				break;
			case 386:
				SetRealTimePoweredUp(tag.Value);
				break;
			case 262:
				SetRealTimeLinkedEntityId(tag.Value);
				break;
			case 194:
				SetRealTimeDivineShield(tag.Value);
				break;
			case 240:
				SetRealTimeIsImmune(tag.Value);
				break;
			case 373:
				SetRealTimeIsImmuneWhileAttacking(tag.Value);
				break;
			case 363:
			case 1944:
				SetRealTimeIsPoisonous(tag.Value);
				break;
			case 202:
				SetRealTimeCardType((TAG_CARDTYPE)tag.Value);
				break;
			case 481:
				SetRealTimeCardCostsHealth(tag.Value);
				break;
			case 930:
				SetRealTimeAttackableByRush(tag.Value);
				break;
			case 12:
				SetRealTimePremium((TAG_PREMIUM)tag.Value);
				break;
			case 1373:
				SetRealTimePlayerLeaderboardPlace(tag.Value);
				break;
			case 1377:
				SetRealTimePlayerTechLevel(tag.Value);
				break;
			}
		}
	}

	public void SetRealTimeCost(int newCost)
	{
		m_realTimeCost = newCost;
	}

	public int GetRealTimeCost()
	{
		if (m_realTimeCost == -1)
		{
			return GetCost();
		}
		return m_realTimeCost;
	}

	public void SetRealTimeAttack(int newAttack)
	{
		m_realTimeAttack = newAttack;
	}

	public int GetRealTimeAttack()
	{
		return m_realTimeAttack;
	}

	public void SetRealTimeHealth(int newHealth)
	{
		m_realTimeHealth = newHealth;
	}

	public void SetRealTimeDamage(int newDamage)
	{
		m_realTimeDamage = newDamage;
	}

	public void SetRealTimeArmor(int newArmor)
	{
		m_realTimeArmor = newArmor;
	}

	public int GetRealTimeRemainingHP()
	{
		return m_realTimeHealth + m_realTimeArmor - m_realTimeDamage;
	}

	public void SetRealTimeZone(int zone)
	{
		m_realTimeZone = zone;
	}

	public TAG_ZONE GetRealTimeZone()
	{
		return (TAG_ZONE)m_realTimeZone;
	}

	public void SetRealTimeZonePosition(int zonePosition)
	{
		m_realTimeZonePosition = zonePosition;
	}

	public int GetRealTimeZonePosition()
	{
		return m_realTimeZonePosition;
	}

	public void SetRealTimeLinkedEntityId(int linkedEntityId)
	{
		m_realTimeLinkedEntityId = linkedEntityId;
	}

	public int GetRealTimeLinkedEntityId()
	{
		return m_realTimeLinkedEntityId;
	}

	public void SetRealTimePoweredUp(int poweredUp)
	{
		m_realTimePoweredUp = ((poweredUp > 0) ? true : false);
	}

	public bool GetRealTimePoweredUp()
	{
		return m_realTimePoweredUp;
	}

	public void SetRealTimeDivineShield(int divineShield)
	{
		m_realTimeDivineShield = ((divineShield > 0) ? true : false);
	}

	public bool GetRealTimeDivineShield()
	{
		return m_realTimeDivineShield;
	}

	public void SetRealTimeIsImmune(int immune)
	{
		m_realTimeIsImmune = ((immune > 0) ? true : false);
	}

	public bool GetRealTimeIsImmune()
	{
		return m_realTimeIsImmune;
	}

	public void SetRealTimeIsImmuneWhileAttacking(int immune)
	{
		m_realTimeIsImmuneWhileAttacking = ((immune > 0) ? true : false);
	}

	public bool GetRealTimeIsImmuneWhileAttacking()
	{
		return m_realTimeIsImmuneWhileAttacking;
	}

	public void SetRealTimeIsPoisonous(int poisonous)
	{
		m_realTimeIsPoisonous = ((poisonous > 0) ? true : false);
	}

	public bool GetRealTimeIsPoisonous()
	{
		return m_realTimeIsPoisonous;
	}

	public void SetRealTimeIsDormant(int dormant)
	{
		m_realTimeIsDormant = dormant > 0;
	}

	public bool GetRealTimeIsDormant()
	{
		return m_realTimeIsDormant;
	}

	public void SetRealTimeHasSpellpower(int spellpower)
	{
		m_realTimeSpellpower = spellpower;
	}

	public int GetRealTimeSpellpower()
	{
		return m_realTimeSpellpower;
	}

	public void SetRealTimeSpellpowerDouble(int powerDouble)
	{
		m_realTimeSpellpowerDouble = powerDouble > 0;
	}

	public bool GetRealTimeSpellpowerDouble()
	{
		return m_realTimeSpellpowerDouble;
	}

	public void SetRealTimeHealingDoesDamageHint(int healingDoesDamageHint)
	{
		m_realTimeHealingDoesDamageHint = healingDoesDamageHint > 0;
	}

	public bool GetRealTimeHealingDoeDamageHint()
	{
		return m_realTimeHealingDoesDamageHint;
	}

	public void SetRealTimeLifestealDoesDamageHint(int lifestealDoesDamageHint)
	{
		m_realTimeLifestealDoesDamageHint = lifestealDoesDamageHint > 0;
	}

	public bool GetRealTimeLifestealDoesDamageHint()
	{
		return m_realTimeLifestealDoesDamageHint;
	}

	public void SetRealTimeCardCostsHealth(int value)
	{
		m_realTimeCardCostsHealth = ((value > 0) ? true : false);
	}

	public bool GetRealTimeCardCostsHealth()
	{
		return m_realTimeCardCostsHealth;
	}

	public void SetRealTimeAttackableByRush(int value)
	{
		m_realTimeAttackableByRush = ((value > 0) ? true : false);
	}

	public bool GetRealTimeAttackableByRush()
	{
		return m_realTimeAttackableByRush;
	}

	public void SetRealTimeCardType(TAG_CARDTYPE cardType)
	{
		m_realTimeCardType = cardType;
	}

	public TAG_CARDTYPE GetRealTimeCardType()
	{
		return m_realTimeCardType;
	}

	public void SetRealTimePremium(TAG_PREMIUM premium)
	{
		m_realTimePremium = premium;
	}

	public TAG_PREMIUM GetRealTimePremium()
	{
		return m_realTimePremium;
	}

	public void SetRealTimePlayerLeaderboardPlace(int playerLeaderboardPlace)
	{
		m_realTimePlayerLeaderboardPlace = playerLeaderboardPlace;
	}

	public int GetRealTimePlayerLeaderboardPlace()
	{
		return m_realTimePlayerLeaderboardPlace;
	}

	public void SetRealTimePlayerTechLevel(int playerTechLevel)
	{
		m_realTimePlayerTechLevel = playerTechLevel;
	}

	public int GetRealTimePlayerTechLevel()
	{
		return m_realTimePlayerTechLevel;
	}

	public CardTextHistoryData GetCardTextHistoryData()
	{
		return m_cardTextHistoryData;
	}

	private void LoadEntityDef(string cardId)
	{
		if (base.m_cardId != cardId)
		{
			base.m_cardId = cardId;
		}
		if (!string.IsNullOrEmpty(cardId))
		{
			m_dynamicEntityDef = null;
			m_staticEntityDef = DefLoader.Get().GetEntityDef(cardId);
			if (m_staticEntityDef == null)
			{
				Error.AddDevFatal("Failed to load a card xml for {0}", cardId);
			}
			else
			{
				UpdateCardName();
			}
		}
	}

	public void LoadCard(string cardId, LoadCardData data = null)
	{
		LoadEntityDef(cardId);
		m_loadState = LoadState.LOADING;
		if (string.IsNullOrEmpty(cardId))
		{
			DefLoader.Get().LoadCardDef("HiddenCard", OnCardDefLoaded);
		}
		else
		{
			DefLoader.Get().LoadCardDef(cardId, OnCardDefLoaded, data);
		}
	}

	private void OnCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		using (cardDef)
		{
			if (cardDef == null)
			{
				Debug.LogErrorFormat("Entity.OnCardDefLoaded() - {0} does not have an asset!", cardId);
				m_loadState = LoadState.DONE;
				return;
			}
			LoadCardData loadCardData = new LoadCardData
			{
				updateActor = false,
				restartStateSpells = false,
				fromChangeEntity = false
			};
			if (callbackData is LoadCardData)
			{
				loadCardData = (LoadCardData)callbackData;
			}
			if (m_card != null)
			{
				m_card.SetCardDef(cardDef, loadCardData.updateActor);
				if (loadCardData.updateActor)
				{
					m_card.UpdateActor();
					m_card.ActivateStateSpells();
				}
				else if (loadCardData.restartStateSpells)
				{
					m_card.ActivateStateSpells(forceActivate: true);
				}
				m_card.RefreshHeroPowerTooltip();
				if (loadCardData.fromChangeEntity && IsMinion() && m_card.GetZone() is ZonePlay)
				{
					m_card.ActivateCharacterPlayEffects();
				}
			}
			UpdateUseBattlecryFlag(fromGameState: false);
			m_loadState = LoadState.DONE;
			if (m_card != null)
			{
				m_card.RefreshActor();
			}
		}
	}

	public SpellType GetPrioritizedBaubleSpellType()
	{
		if (IsPoisonous())
		{
			return SpellType.POISONOUS;
		}
		if (HasTriggerVisual() || DoEnchantmentsHaveTriggerVisuals())
		{
			return SpellType.TRIGGER;
		}
		if (HasLifesteal())
		{
			return SpellType.LIFESTEAL;
		}
		if (HasInspire())
		{
			return SpellType.INSPIRE;
		}
		if (HasOverKill() || DoEnchantmentsHaveOverKill())
		{
			return SpellType.OVERKILL;
		}
		if (HasSpellburst() || DoEnchantmentsHaveSpellburst())
		{
			return SpellType.SPELLBURST;
		}
		if (HasFrenzy())
		{
			return SpellType.FRENZY;
		}
		return SpellType.NONE;
	}

	public TAG_CARD_SET GetWatermarkCardSetOverride()
	{
		if (HasTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET))
		{
			return (TAG_CARD_SET)GetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET);
		}
		EntityDef entityDef = GetEntityDef();
		if (entityDef != null && entityDef.HasTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET))
		{
			return (TAG_CARD_SET)entityDef.GetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET);
		}
		return TAG_CARD_SET.INVALID;
	}

	public bool IsTauntIgnored()
	{
		if (GameState.Get().GetFirstOpponentPlayer(GetController()).HasTag(GAME_TAG.IGNORE_TAUNT))
		{
			return true;
		}
		return false;
	}
}
