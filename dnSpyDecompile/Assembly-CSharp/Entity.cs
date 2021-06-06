using System;
using System.Collections.Generic;
using System.Linq;
using PegasusGame;
using UnityEngine;

// Token: 0x020008A1 RID: 2209
public class Entity : EntityBase
{
	// Token: 0x06007986 RID: 31110 RVA: 0x0027A09F File Offset: 0x0027829F
	public override string ToString()
	{
		return this.GetDebugName();
	}

	// Token: 0x06007987 RID: 31111 RVA: 0x0027A0A7 File Offset: 0x002782A7
	public virtual void OnRealTimeFullEntity(Network.HistFullEntity fullEntity)
	{
		base.SetTags(fullEntity.Entity.Tags);
		this.InitRealTimeValues(fullEntity.Entity.Tags);
		this.InitCard();
		this.LoadEntityDef(fullEntity.Entity.CardID);
	}

	// Token: 0x06007988 RID: 31112 RVA: 0x0027A0E4 File Offset: 0x002782E4
	public void OnFullEntity(Network.HistFullEntity fullEntity)
	{
		this.m_loadState = global::Entity.LoadState.PENDING;
		this.LoadCard(fullEntity.Entity.CardID, null);
		int tag = base.GetTag(GAME_TAG.ATTACHED);
		if (tag != 0)
		{
			GameState.Get().GetEntity(tag).AddAttachment(this);
		}
		int tag2 = base.GetTag(GAME_TAG.PARENT_CARD);
		if (tag2 != 0)
		{
			global::Entity entity = GameState.Get().GetEntity(tag2);
			if (entity != null)
			{
				entity.AddSubCard(this);
			}
			else
			{
				Log.Gameplay.PrintError("Unable to find parent entity id={0}", new object[]
				{
					tag2
				});
			}
		}
		if (base.GetZone() == TAG_ZONE.PLAY)
		{
			if (base.IsHero())
			{
				this.GetController().SetHero(this);
			}
			else if (base.IsHeroPower())
			{
				this.GetController().SetHeroPower(this);
			}
		}
		if (fullEntity.Entity.DefTags.Count > 0)
		{
			EntityDef orCreateDynamicDefinition = this.GetOrCreateDynamicDefinition();
			for (int i = 0; i < fullEntity.Entity.DefTags.Count; i++)
			{
				orCreateDynamicDefinition.SetTag(fullEntity.Entity.DefTags[i].Name, fullEntity.Entity.DefTags[i].Value);
			}
		}
		if (base.HasTag(GAME_TAG.DISPLAYED_CREATOR))
		{
			this.SetDisplayedCreatorName(base.GetTag(GAME_TAG.DISPLAYED_CREATOR));
		}
		if (base.HasTag(GAME_TAG.CREATOR_DBID))
		{
			this.ResolveEnchantmentPortraitCardID(base.GetTag(GAME_TAG.CREATOR_DBID));
		}
		if (base.HasTag(GAME_TAG.PLAYER_LEADERBOARD_PLACE))
		{
			int tag3 = base.GetTag(GAME_TAG.PLAYER_ID);
			if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag3))
			{
				GameState.Get().GetPlayerInfoMap()[tag3].SetPlayerHero(this);
			}
			PlayerLeaderboardManager.Get().CreatePlayerTile(this);
			if (base.HasTag(GAME_TAG.REPLACEMENT_ENTITY))
			{
				PlayerLeaderboardManager.Get().ApplyEntityReplacement(tag3, this);
			}
		}
		if (base.HasTag(GAME_TAG.BACON_IS_KEL_THUZAD))
		{
			PlayerLeaderboardManager.Get().SetOddManOutOpponentHero(this);
		}
	}

	// Token: 0x06007989 RID: 31113 RVA: 0x0027A2C6 File Offset: 0x002784C6
	public virtual void OnRealTimeShowEntity(Network.HistShowEntity showEntity)
	{
		this.HandleRealTimeEntityChange(showEntity.Entity);
	}

	// Token: 0x0600798A RID: 31114 RVA: 0x0027A2D4 File Offset: 0x002784D4
	public void OnShowEntity(Network.HistShowEntity showEntity)
	{
		this.HandleEntityChange(showEntity.Entity, new global::Entity.LoadCardData
		{
			updateActor = false,
			restartStateSpells = false,
			fromChangeEntity = false
		}, true);
	}

	// Token: 0x0600798B RID: 31115 RVA: 0x0027A300 File Offset: 0x00278500
	public void OnHideEntity(Network.HistHideEntity hideEntity)
	{
		this.SetTagAndHandleChange<int>(GAME_TAG.ZONE, hideEntity.Zone);
		EntityDef entityDef = this.GetEntityDef();
		base.SetTag(GAME_TAG.ATK, entityDef.GetATK());
		base.SetTag(GAME_TAG.HEALTH, entityDef.GetHealth());
		base.SetTag(GAME_TAG.COST, entityDef.GetCost());
		base.SetTag(GAME_TAG.DAMAGE, 0);
		base.SetCardId(null);
	}

	// Token: 0x0600798C RID: 31116 RVA: 0x0027A35C File Offset: 0x0027855C
	public virtual void OnRealTimeChangeEntity(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		this.m_queuedChangeEntityCount++;
		this.HandleRealTimeEntityChange(changeEntity.Entity);
		this.CheckRealTimeTransform(powerList, index, changeEntity);
	}

	// Token: 0x0600798D RID: 31117 RVA: 0x0027A384 File Offset: 0x00278584
	public void OnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		if (this.m_transformPowersProcessed.Contains(changeEntity))
		{
			this.m_transformPowersProcessed.Remove(changeEntity);
			return;
		}
		this.m_subCardIDs.Clear();
		this.m_queuedChangeEntityCount--;
		global::Entity.LoadCardData data = new global::Entity.LoadCardData
		{
			updateActor = this.ShouldUpdateActorOnChangeEntity(changeEntity),
			restartStateSpells = this.ShouldRestartStateSpellsOnChangeEntity(changeEntity),
			fromChangeEntity = true
		};
		this.HandleEntityChange(changeEntity.Entity, data, false);
	}

	// Token: 0x0600798E RID: 31118 RVA: 0x0027A3FC File Offset: 0x002785FC
	private bool IsTagChanged(Network.HistChangeEntity changeEntity, GAME_TAG tag)
	{
		Network.Entity.Tag tag2 = changeEntity.Entity.Tags.Find((Network.Entity.Tag currTag) => currTag.Name == (int)tag);
		return tag2 != null && base.GetTag(tag) != tag2.Value;
	}

	// Token: 0x0600798F RID: 31119 RVA: 0x0027A450 File Offset: 0x00278650
	private bool ShouldUpdateActorOnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		return this.IsTagChanged(changeEntity, GAME_TAG.CARDTYPE) || base.GetTag(GAME_TAG.CARDTYPE) != (int)this.m_realTimeCardType || this.IsTagChanged(changeEntity, GAME_TAG.PREMIUM) || base.GetTag(GAME_TAG.PREMIUM) != (int)this.m_realTimePremium;
	}

	// Token: 0x06007990 RID: 31120 RVA: 0x0027A49E File Offset: 0x0027869E
	private bool ShouldRestartStateSpellsOnChangeEntity(Network.HistChangeEntity changeEntity)
	{
		return this.IsTagChanged(changeEntity, GAME_TAG.ELITE);
	}

	// Token: 0x06007991 RID: 31121 RVA: 0x0027A4AC File Offset: 0x002786AC
	public virtual void OnRealTimeTagChanged(Network.HistTagChange change)
	{
		GAME_TAG tag = (GAME_TAG)change.Tag;
		if (tag <= GAME_TAG.POISONOUS)
		{
			if (tag <= GAME_TAG.CARDTYPE)
			{
				if (tag <= GAME_TAG.DURABILITY)
				{
					if (tag != GAME_TAG.PREMIUM)
					{
						switch (tag)
						{
						case GAME_TAG.DAMAGE:
							this.SetRealTimeDamage(change.Value);
							return;
						case GAME_TAG.HEALTH:
							break;
						case (GAME_TAG)46:
							return;
						case GAME_TAG.ATK:
							this.SetRealTimeAttack(change.Value);
							return;
						case GAME_TAG.COST:
							this.SetRealTimeCost(change.Value);
							return;
						case GAME_TAG.ZONE:
							this.SetRealTimeZone(change.Value);
							return;
						case GAME_TAG.CONTROLLER:
							this.m_queuedRealTimeControllerTagChangeCount++;
							return;
						default:
							if (tag != GAME_TAG.DURABILITY)
							{
								return;
							}
							break;
						}
						this.SetRealTimeHealth(change.Value);
						return;
					}
					this.SetRealTimePremium((TAG_PREMIUM)change.Value);
					return;
				}
				else
				{
					if (tag == GAME_TAG.SPELLPOWER)
					{
						this.SetRealTimeHasSpellpower(change.Value);
						return;
					}
					if (tag == GAME_TAG.DIVINE_SHIELD)
					{
						this.SetRealTimeDivineShield(change.Value);
						return;
					}
					if (tag != GAME_TAG.CARDTYPE)
					{
						return;
					}
					this.SetRealTimeCardType((TAG_CARDTYPE)change.Value);
					return;
				}
			}
			else if (tag <= GAME_TAG.ZONE_POSITION)
			{
				if (tag == GAME_TAG.IMMUNE)
				{
					this.SetRealTimeIsImmune(change.Value);
					return;
				}
				if (tag == GAME_TAG.LINKED_ENTITY)
				{
					this.SetRealTimeLinkedEntityId(change.Value);
					return;
				}
				if (tag != GAME_TAG.ZONE_POSITION)
				{
					return;
				}
				this.SetRealTimeZonePosition(change.Value);
				return;
			}
			else
			{
				if (tag == GAME_TAG.ARMOR)
				{
					this.SetRealTimeArmor(change.Value);
					return;
				}
				if (tag == GAME_TAG.SPELLPOWER_DOUBLE)
				{
					this.SetRealTimeSpellpowerDouble(change.Value);
					return;
				}
				if (tag != GAME_TAG.POISONOUS)
				{
					return;
				}
			}
		}
		else if (tag <= GAME_TAG.HEALING_DOES_DAMAGE_HINT)
		{
			if (tag <= GAME_TAG.CARD_COSTS_HEALTH)
			{
				if (tag == GAME_TAG.IMMUNE_WHILE_ATTACKING)
				{
					this.SetRealTimeIsImmuneWhileAttacking(change.Value);
					return;
				}
				if (tag == GAME_TAG.POWERED_UP)
				{
					this.SetRealTimePoweredUp(change.Value);
					return;
				}
				if (tag != GAME_TAG.CARD_COSTS_HEALTH)
				{
					return;
				}
				this.SetRealTimeCardCostsHealth(change.Value);
				return;
			}
			else
			{
				if (tag == GAME_TAG.ATTACKABLE_BY_RUSH)
				{
					this.SetRealTimeAttackableByRush(change.Value);
					return;
				}
				if (tag == GAME_TAG.PUZZLE_COMPLETED)
				{
					this.OnRealTimePuzzleCompleted(change.Value);
					return;
				}
				if (tag != GAME_TAG.HEALING_DOES_DAMAGE_HINT)
				{
					return;
				}
				this.SetRealTimeHealingDoesDamageHint(change.Value);
				return;
			}
		}
		else if (tag <= GAME_TAG.PLAYER_TRIPLES)
		{
			if (tag == GAME_TAG.PLAYER_LEADERBOARD_PLACE)
			{
				this.SetRealTimePlayerLeaderboardPlace(change.Value);
				this.UpdateSharedPlayer();
				return;
			}
			if (tag == GAME_TAG.PLAYER_TECH_LEVEL)
			{
				this.SetRealTimePlayerTechLevel(change.Value);
				PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(base.GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.TECH_LEVEL);
				return;
			}
			if (tag != GAME_TAG.PLAYER_TRIPLES)
			{
				return;
			}
			PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(base.GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.TRIPLE);
			return;
		}
		else if (tag <= GAME_TAG.BACON_MUKLA_BANANA_SPAWN_COUNT)
		{
			if (tag == GAME_TAG.DORMANT)
			{
				this.SetRealTimeIsDormant(change.Value);
				return;
			}
			if (tag != GAME_TAG.BACON_MUKLA_BANANA_SPAWN_COUNT)
			{
				return;
			}
			PlayerLeaderboardManager.Get().NotifyPlayerTileEvent(base.GetTag(GAME_TAG.PLAYER_ID), PlayerLeaderboardManager.PlayerTileEvent.BANANA);
			return;
		}
		else
		{
			if (tag == GAME_TAG.LIFESTEAL_DOES_DAMAGE_HINT)
			{
				this.SetRealTimeLifestealDoesDamageHint(change.Value);
				return;
			}
			if (tag != GAME_TAG.NON_KEYWORD_POISONOUS)
			{
				return;
			}
		}
		this.SetRealTimeIsPoisonous(change.Value);
	}

	// Token: 0x06007992 RID: 31122 RVA: 0x0027A7CC File Offset: 0x002789CC
	private void UpdateSharedPlayer()
	{
		PlayerLeaderboardManager.Get().CreatePlayerTile(this);
		int tag = base.GetTag(GAME_TAG.PLAYER_ID);
		if (tag == 0)
		{
			tag = base.GetTag(GAME_TAG.CONTROLLER);
		}
		if (GameState.Get().GetPlayerInfoMap().ContainsKey(tag) && GameState.Get().GetPlayerInfoMap()[tag].GetPlayerHero() == null)
		{
			GameState.Get().GetPlayerInfoMap()[tag].SetPlayerHero(this);
		}
	}

	// Token: 0x06007993 RID: 31123 RVA: 0x0027A838 File Offset: 0x00278A38
	public void OnRealTimePuzzleCompleted(int newValue)
	{
		if (!base.IsPuzzle())
		{
			return;
		}
		if (this.m_card == null)
		{
			return;
		}
		if (this.m_card.GetActor() == null)
		{
			return;
		}
		PuzzleController component = this.m_card.GetActor().GetComponent<PuzzleController>();
		if (component == null)
		{
			Log.Gameplay.PrintError("Puzzle card {0} does not have a PuzzleController component.", new object[]
			{
				this
			});
			return;
		}
		component.OnRealTimePuzzleCompleted(newValue);
	}

	// Token: 0x06007994 RID: 31124 RVA: 0x0027A8AC File Offset: 0x00278AAC
	public virtual void HandlePreTransformTagChanges(TagDeltaList changeList)
	{
		if (this.m_card != null)
		{
			this.m_card.DeactivateCustomKeywordEffect();
		}
	}

	// Token: 0x06007995 RID: 31125 RVA: 0x0027A8C8 File Offset: 0x00278AC8
	public virtual void OnTagsChanged(TagDeltaList changeList, bool fromShowEntity)
	{
		bool flag = false;
		for (int i = 0; i < changeList.Count; i++)
		{
			TagDelta change = changeList[i];
			if (this.IsNameChange(change))
			{
				flag = true;
			}
			this.HandleTagChange(change);
		}
		if (this.m_card == null)
		{
			return;
		}
		if (flag)
		{
			this.UpdateCardName();
		}
		this.m_card.OnTagsChanged(changeList, fromShowEntity);
	}

	// Token: 0x06007996 RID: 31126 RVA: 0x0027A927 File Offset: 0x00278B27
	public virtual void OnTagChanged(TagDelta change)
	{
		this.HandleTagChange(change);
		if (this.m_card == null)
		{
			return;
		}
		if (this.IsNameChange(change))
		{
			this.UpdateCardName();
		}
		this.m_card.OnTagChanged(change, false);
	}

	// Token: 0x06007997 RID: 31127 RVA: 0x0027A95B File Offset: 0x00278B5B
	public virtual void OnCachedTagForDormantChanged(TagDelta change)
	{
		base.SetCachedTagForDormant(change.tag, change.newValue);
	}

	// Token: 0x06007998 RID: 31128 RVA: 0x0027A96F File Offset: 0x00278B6F
	protected override void OnUpdateCardId()
	{
		this.UpdateCardName();
	}

	// Token: 0x06007999 RID: 31129 RVA: 0x0027A977 File Offset: 0x00278B77
	public virtual void OnMetaData(Network.HistMetaData metaData)
	{
		if (this.m_card == null)
		{
			return;
		}
		this.m_card.OnMetaData(metaData);
	}

	// Token: 0x0600799A RID: 31130 RVA: 0x0027A994 File Offset: 0x00278B94
	private void HandleRealTimeEntityChange(Network.Entity netEntity)
	{
		this.InitRealTimeValues(netEntity.Tags);
	}

	// Token: 0x0600799B RID: 31131 RVA: 0x0027A9A4 File Offset: 0x00278BA4
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

	// Token: 0x0600799C RID: 31132 RVA: 0x0027AA10 File Offset: 0x00278C10
	private void CheckRealTimeTransform(List<Network.PowerHistory> powerList, int index, Network.HistChangeEntity changeEntity)
	{
		if (!this.HasRealTimeTransformTag(changeEntity.Entity))
		{
			return;
		}
		if (!this.CanRealTimeTransform(powerList, index))
		{
			return;
		}
		this.OnChangeEntity(changeEntity);
		this.m_transformPowersProcessed.Add(changeEntity);
	}

	// Token: 0x0600799D RID: 31133 RVA: 0x0027AA40 File Offset: 0x00278C40
	private bool CanRealTimeTransform(List<Network.PowerHistory> powerList, int index)
	{
		for (int i = 0; i < index; i++)
		{
			Network.PowerHistory power = powerList[i];
			if (!this.CheckPowerHistoryForRealTimeTransform(power))
			{
				return false;
			}
		}
		foreach (PowerTaskList powerTaskList in GameState.Get().GetPowerProcessor().GetPowerQueue())
		{
			if (!this.CheckPowerTaskListForRealTimeTransform(powerTaskList))
			{
				return false;
			}
		}
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		return this.CheckPowerTaskListForRealTimeTransform(currentTaskList);
	}

	// Token: 0x0600799E RID: 31134 RVA: 0x0027AAE0 File Offset: 0x00278CE0
	private bool CheckPowerHistoryForRealTimeTransform(Network.PowerHistory power)
	{
		switch (power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
			if (((Network.HistFullEntity)power).Entity.ID == base.GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.SHOW_ENTITY:
			if (((Network.HistShowEntity)power).Entity.ID == base.GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.HIDE_ENTITY:
			if (((Network.HistHideEntity)power).Entity == base.GetEntityId())
			{
				return false;
			}
			break;
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			if (histTagChange.Entity == base.GetEntityId() && histTagChange.Tag != 263 && histTagChange.Tag != 385 && histTagChange.Tag != 466)
			{
				return false;
			}
			break;
		}
		case Network.PowerType.META_DATA:
		{
			Network.HistMetaData metaDataEntity = (Network.HistMetaData)power;
			if (!this.CheckPowerHistoryMetaDataForRealTimeTransform(metaDataEntity))
			{
				return false;
			}
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)power;
			if (histChangeEntity.Entity.ID == base.GetEntityId() && !this.m_transformPowersProcessed.Contains(histChangeEntity))
			{
				return false;
			}
			break;
		}
		}
		return true;
	}

	// Token: 0x0600799F RID: 31135 RVA: 0x0027ABF0 File Offset: 0x00278DF0
	private bool CheckPowerHistoryMetaDataForRealTimeTransform(Network.HistMetaData metaDataEntity)
	{
		switch (metaDataEntity.MetaType)
		{
		case HistoryMeta.Type.TARGET:
		case HistoryMeta.Type.DAMAGE:
		case HistoryMeta.Type.HEALING:
		case HistoryMeta.Type.JOUST:
		case HistoryMeta.Type.HISTORY_TARGET:
			using (List<int>.Enumerator enumerator = metaDataEntity.Info.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == base.GetEntityId())
					{
						return false;
					}
				}
				return true;
			}
			break;
		case (HistoryMeta.Type)4:
		case HistoryMeta.Type.END_ARTIFICIAL_HISTORY_TILE:
		case HistoryMeta.Type.START_DRAW:
			return true;
		case HistoryMeta.Type.SHOW_BIG_CARD:
		case HistoryMeta.Type.EFFECT_TIMING:
		case HistoryMeta.Type.OVERRIDE_HISTORY:
		case HistoryMeta.Type.HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END:
		case HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TILE:
		case HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE:
		case HistoryMeta.Type.BURNED_CARD:
			break;
		default:
			return true;
		}
		if (metaDataEntity.Info.Count > 0 && metaDataEntity.Info[0] == base.GetEntityId())
		{
			return false;
		}
		return true;
	}

	// Token: 0x060079A0 RID: 31136 RVA: 0x0027ACBC File Offset: 0x00278EBC
	private bool CheckPowerTaskListForRealTimeTransform(PowerTaskList powerTaskList)
	{
		if (powerTaskList == null)
		{
			return true;
		}
		foreach (PowerTask powerTask in powerTaskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (!powerTask.IsCompleted() && !this.CheckPowerHistoryForRealTimeTransform(power))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060079A1 RID: 31137 RVA: 0x0027AD2C File Offset: 0x00278F2C
	private void HandleEntityChange(Network.Entity netEntity, global::Entity.LoadCardData data, bool fromShowEntity)
	{
		TagDeltaList changeList = this.m_tags.CreateDeltas(netEntity.Tags);
		base.SetTags(netEntity.Tags);
		this.HandlePreTransformTagChanges(changeList);
		if (this.m_card != null)
		{
			this.m_card.DestroyCardDefAssetsOnEntityChanged();
		}
		this.LoadCard(netEntity.CardID, data);
		if (base.GetZone() == TAG_ZONE.HAND && this.GetCard() != null && this.GetCard().GetZone() != null)
		{
			if (data.updateActor)
			{
				this.GetCard().GetZone().UpdateLayout();
			}
			this.GetCard().UpdateActorState(true);
		}
		if (netEntity.DefTags.Count > 0)
		{
			EntityDef orCreateDynamicDefinition = this.GetOrCreateDynamicDefinition();
			for (int i = 0; i < netEntity.DefTags.Count; i++)
			{
				orCreateDynamicDefinition.SetTag(netEntity.DefTags[i].Name, netEntity.DefTags[i].Value);
			}
		}
		this.OnTagsChanged(changeList, fromShowEntity);
	}

	// Token: 0x060079A2 RID: 31138 RVA: 0x0027AE30 File Offset: 0x00279030
	private void HandleTagChange(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag <= GAME_TAG.PARENT_CARD)
		{
			if (tag <= GAME_TAG.ZONE)
			{
				if (tag != GAME_TAG.ATTACHED)
				{
					if (tag != GAME_TAG.ZONE)
					{
						return;
					}
					this.UpdateUseBattlecryFlag(false);
					if (GameState.Get().IsTurnStartManagerActive() && change.oldValue == 2 && change.newValue == 3)
					{
						PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
						if (currentTaskList != null && currentTaskList.GetSourceEntity(true) == GameState.Get().GetFriendlySidePlayer())
						{
							TurnStartManager.Get().NotifyOfCardDrawn(this);
						}
					}
					if (change.newValue == 1)
					{
						if (base.IsHero())
						{
							this.GetController().SetHero(this);
						}
						else if (base.IsHeroPower())
						{
							this.GetController().SetHeroPower(this);
						}
					}
					this.CheckZoneChangeForEnchantment(change);
					if (change.newValue == 5)
					{
						GameState.Get().GetGameEntity().QueueEntityForRemoval(this);
						return;
					}
				}
				else
				{
					global::Entity entity = GameState.Get().GetEntity(change.oldValue);
					if (entity != null)
					{
						entity.RemoveAttachment(this);
					}
					global::Entity entity2 = GameState.Get().GetEntity(change.newValue);
					if (entity2 != null)
					{
						entity2.AddAttachment(this);
						return;
					}
				}
			}
			else
			{
				if (tag == GAME_TAG.CONTROLLER)
				{
					global::Entity parentEntity = this.GetParentEntity();
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
					if (base.IsHeroPower())
					{
						this.GetController().SetHeroPower(this);
					}
					this.m_queuedRealTimeControllerTagChangeCount--;
					return;
				}
				if (tag != GAME_TAG.PARENT_CARD)
				{
					return;
				}
				global::Entity entity3 = GameState.Get().GetEntity(change.oldValue);
				if (entity3 != null)
				{
					entity3.RemoveSubCard(this);
				}
				global::Entity entity4 = GameState.Get().GetEntity(change.newValue);
				if (entity4 != null)
				{
					entity4.AddSubCard(this);
					return;
				}
			}
		}
		else
		{
			if (tag <= GAME_TAG.DISPLAYED_CREATOR)
			{
				if (tag != GAME_TAG.HERO_POWER)
				{
					if (tag != GAME_TAG.DISPLAYED_CREATOR)
					{
						return;
					}
					this.SetDisplayedCreatorName(change.newValue);
					return;
				}
			}
			else
			{
				if (tag == GAME_TAG.CREATOR_DBID)
				{
					this.ResolveEnchantmentPortraitCardID(change.newValue);
					return;
				}
				if (tag != GAME_TAG.HERO_POWER_ENTITY)
				{
					return;
				}
			}
			PlayerLeaderboardManager playerLeaderboardManager = PlayerLeaderboardManager.Get();
			if (playerLeaderboardManager != null && playerLeaderboardManager.IsEnabled())
			{
				playerLeaderboardManager.UpdatePlayerTileHeroPower(this, change.newValue);
			}
		}
	}

	// Token: 0x060079A3 RID: 31139 RVA: 0x0027B068 File Offset: 0x00279268
	private void SetDisplayedCreatorName(int entityID)
	{
		global::Entity entity = GameState.Get().GetEntity(entityID);
		if (entity == null)
		{
			this.m_displayedCreatorName = null;
			return;
		}
		if (string.IsNullOrEmpty(entity.m_cardId))
		{
			this.m_displayedCreatorName = GameStrings.Get("GAMEPLAY_UNKNOWN_CREATED_BY");
			return;
		}
		this.m_displayedCreatorName = entity.GetName();
	}

	// Token: 0x060079A4 RID: 31140 RVA: 0x0027B0B8 File Offset: 0x002792B8
	private bool HasEnchantmentPortrait(string enchantmentPortraitCardID)
	{
		if (string.IsNullOrEmpty(enchantmentPortraitCardID))
		{
			return false;
		}
		bool result;
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(enchantmentPortraitCardID, null))
		{
			if (cardDef == null)
			{
				result = false;
			}
			else
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(enchantmentPortraitCardID);
				if (entityDef == null)
				{
					result = false;
				}
				else
				{
					if (entityDef.GetCardType() == TAG_CARDTYPE.ENCHANTMENT)
					{
						if (cardDef.CardDef.GetEnchantmentPortrait() != null)
						{
							return true;
						}
						if (cardDef.CardDef.GetPortraitTexture() != null)
						{
							return true;
						}
					}
					else
					{
						if (cardDef.CardDef.GetHistoryTileFullPortrait() != null)
						{
							return true;
						}
						if (cardDef.CardDef.GetPortraitTexture() != null)
						{
							return true;
						}
					}
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x060079A5 RID: 31141 RVA: 0x0027B180 File Offset: 0x00279380
	public string GetEnchantmentCreatorCardIDForPortrait()
	{
		return this.m_enchantmentCreatorCardIDForPortrait;
	}

	// Token: 0x060079A6 RID: 31142 RVA: 0x0027B188 File Offset: 0x00279388
	private void ResolveEnchantmentPortraitCardID(int creatorDBID)
	{
		this.m_enchantmentCreatorCardIDForPortrait = null;
		if (!this.IsEnchantment())
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(creatorDBID, true);
		if (entityDef == null)
		{
			return;
		}
		this.m_enchantmentCreatorCardIDForPortrait = entityDef.GetCardId();
		global::Entity creator = this.GetCreator();
		while (!this.HasEnchantmentPortrait(this.m_enchantmentCreatorCardIDForPortrait))
		{
			if (creator == null || (!creator.IsEnchantment() && creator.GetCardType() != TAG_CARDTYPE.INVALID))
			{
				this.m_enchantmentCreatorCardIDForPortrait = null;
				return;
			}
			entityDef = creator.GetCreatorDef();
			creator = creator.GetCreator();
			if (entityDef == null)
			{
				this.m_enchantmentCreatorCardIDForPortrait = null;
				return;
			}
			this.m_enchantmentCreatorCardIDForPortrait = entityDef.GetCardId();
		}
		global::Entity entity = GameState.Get().GetEntity(base.GetAttached());
		if (entity != null && entity.m_card != null)
		{
			entity.m_card.UpdateTooltip();
		}
	}

	// Token: 0x060079A7 RID: 31143 RVA: 0x0027B248 File Offset: 0x00279448
	private void CheckZoneChangeForEnchantment(TagDelta change)
	{
		if (change.tag != 49)
		{
			return;
		}
		if (!this.IsEnchantment())
		{
			return;
		}
		if (change.oldValue == change.newValue)
		{
			return;
		}
		if (change.newValue == 5 || change.newValue == 4)
		{
			global::Entity entity = GameState.Get().GetEntity(base.GetAttached());
			if (entity != null)
			{
				entity.RemoveAttachment(this);
			}
			if (this.m_card != null)
			{
				this.m_card.Destroy();
			}
		}
	}

	// Token: 0x060079A8 RID: 31144 RVA: 0x0027B2C0 File Offset: 0x002794C0
	private bool IsNameChange(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag <= GAME_TAG.ENTITY_ID)
		{
			if (tag - GAME_TAG.ZONE > 1 && tag != GAME_TAG.ENTITY_ID)
			{
				return false;
			}
		}
		else if (tag != GAME_TAG.CARDTYPE && tag != GAME_TAG.ZONE_POSITION && tag != GAME_TAG.OVERRIDECARDNAME)
		{
			return false;
		}
		return true;
	}

	// Token: 0x060079AA RID: 31146 RVA: 0x0027B336 File Offset: 0x00279536
	public EntityDef GetEntityDef()
	{
		if (this.m_dynamicEntityDef == null)
		{
			return this.m_staticEntityDef;
		}
		return this.m_dynamicEntityDef;
	}

	// Token: 0x060079AB RID: 31147 RVA: 0x0027B34D File Offset: 0x0027954D
	public EntityDef GetOrCreateDynamicDefinition()
	{
		if (this.m_dynamicEntityDef == null)
		{
			this.m_dynamicEntityDef = this.m_staticEntityDef.Clone();
			this.m_staticEntityDef = null;
		}
		return this.m_dynamicEntityDef;
	}

	// Token: 0x060079AC RID: 31148 RVA: 0x0027B378 File Offset: 0x00279578
	public Card InitCard()
	{
		GameObject gameObject = new GameObject();
		this.m_card = gameObject.AddComponent<Card>();
		this.m_card.SetEntity(this);
		this.UpdateCardName();
		return this.m_card;
	}

	// Token: 0x060079AD RID: 31149 RVA: 0x0027B3B0 File Offset: 0x002795B0
	public DefLoader.DisposableCardDef ShareDisposableCardDef()
	{
		if (this.m_duplicateForHistory)
		{
			return this.GetCardDefForHistory();
		}
		if (this.m_card != null)
		{
			return this.m_card.ShareDisposableCardDef();
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			return DefLoader.Get().GetCardDef(base.m_cardId, null);
		}
		return null;
	}

	// Token: 0x060079AE RID: 31150 RVA: 0x0027B408 File Offset: 0x00279608
	private DefLoader.DisposableCardDef GetCardDefForHistory()
	{
		if (this.m_card != null)
		{
			if (this.IsHidden() && !this.m_card.HasHiddenCardDef)
			{
				return DefLoader.Get().GetCardDef("HiddenCard", null);
			}
			if (base.m_cardId == this.m_card.GetEntity().GetCardId())
			{
				return this.m_card.ShareDisposableCardDef();
			}
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			return DefLoader.Get().GetCardDef(base.m_cardId, null);
		}
		return DefLoader.Get().GetCardDef("HiddenCard", null);
	}

	// Token: 0x060079AF RID: 31151 RVA: 0x0027B4A1 File Offset: 0x002796A1
	public Card GetCard()
	{
		return this.m_card;
	}

	// Token: 0x060079B0 RID: 31152 RVA: 0x0027B4A9 File Offset: 0x002796A9
	public void SetCard(Card card)
	{
		this.m_card = card;
	}

	// Token: 0x060079B1 RID: 31153 RVA: 0x0027B4B2 File Offset: 0x002796B2
	public void Destroy()
	{
		if (this.m_card != null)
		{
			this.m_card.Destroy();
		}
	}

	// Token: 0x060079B2 RID: 31154 RVA: 0x0027B4CD File Offset: 0x002796CD
	public global::Entity.LoadState GetLoadState()
	{
		return this.m_loadState;
	}

	// Token: 0x060079B3 RID: 31155 RVA: 0x0027B4D5 File Offset: 0x002796D5
	public bool IsLoadingAssets()
	{
		return this.m_loadState == global::Entity.LoadState.LOADING;
	}

	// Token: 0x060079B4 RID: 31156 RVA: 0x0027B4E0 File Offset: 0x002796E0
	public bool IsBusy()
	{
		return this.IsLoadingAssets() || (this.m_card != null && !this.m_card.IsActorReady());
	}

	// Token: 0x060079B5 RID: 31157 RVA: 0x0027B50A File Offset: 0x0027970A
	public bool IsHidden()
	{
		return string.IsNullOrEmpty(base.m_cardId);
	}

	// Token: 0x060079B6 RID: 31158 RVA: 0x0027B517 File Offset: 0x00279717
	public bool HasQueuedChangeEntity()
	{
		return this.m_queuedChangeEntityCount > 0;
	}

	// Token: 0x060079B7 RID: 31159 RVA: 0x0027B522 File Offset: 0x00279722
	public bool HasQueuedControllerTagChange()
	{
		return this.m_queuedRealTimeControllerTagChangeCount > 0;
	}

	// Token: 0x060079B8 RID: 31160 RVA: 0x0027B52D File Offset: 0x0027972D
	public void ClearTags()
	{
		this.m_tags.Clear();
	}

	// Token: 0x060079B9 RID: 31161 RVA: 0x0027B53A File Offset: 0x0027973A
	public void SetTagAndHandleChange<TagEnum>(GAME_TAG tag, TagEnum tagValue)
	{
		this.SetTagAndHandleChange((int)tag, Convert.ToInt32(tagValue));
	}

	// Token: 0x060079BA RID: 31162 RVA: 0x0027B550 File Offset: 0x00279750
	public TagDelta SetTagAndHandleChange(int tag, int tagValue)
	{
		int tag2 = this.m_tags.GetTag(tag);
		base.SetTag(tag, tagValue);
		TagDelta tagDelta = new TagDelta();
		tagDelta.tag = tag;
		tagDelta.oldValue = tag2;
		tagDelta.newValue = tagValue;
		this.OnTagChanged(tagDelta);
		return tagDelta;
	}

	// Token: 0x060079BB RID: 31163 RVA: 0x0027B595 File Offset: 0x00279795
	public override int GetReferencedTag(int tag)
	{
		return this.GetEntityDef().GetReferencedTag(tag);
	}

	// Token: 0x060079BC RID: 31164 RVA: 0x0027B5A3 File Offset: 0x002797A3
	public int GetDefCost()
	{
		return this.GetEntityDef().GetCost();
	}

	// Token: 0x060079BD RID: 31165 RVA: 0x0027B5B0 File Offset: 0x002797B0
	public int GetDefATK()
	{
		return this.GetEntityDef().GetATK();
	}

	// Token: 0x060079BE RID: 31166 RVA: 0x0027B5BD File Offset: 0x002797BD
	public int GetDefHealth()
	{
		return this.GetEntityDef().GetHealth();
	}

	// Token: 0x060079BF RID: 31167 RVA: 0x0027B5CA File Offset: 0x002797CA
	public int GetDefDurability()
	{
		return this.GetEntityDef().GetDurability();
	}

	// Token: 0x060079C0 RID: 31168 RVA: 0x0027B5D8 File Offset: 0x002797D8
	public bool HasRace(TAG_RACE race)
	{
		TAG_RACE tag_RACE = base.HasTag(GAME_TAG.CARDRACE) ? base.GetTag<TAG_RACE>(GAME_TAG.CARDRACE) : this.GetEntityDef().GetRace();
		return (tag_RACE == TAG_RACE.ALL && race != TAG_RACE.INVALID) || tag_RACE == race;
	}

	// Token: 0x060079C1 RID: 31169 RVA: 0x0027B619 File Offset: 0x00279819
	public override TAG_CLASS GetClass()
	{
		if (base.IsSecret())
		{
			return base.GetClass();
		}
		return this.GetEntityDef().GetClass();
	}

	// Token: 0x060079C2 RID: 31170 RVA: 0x0027B635 File Offset: 0x00279835
	public override IEnumerable<TAG_CLASS> GetClasses(Comparison<TAG_CLASS> classSorter = null)
	{
		if (base.IsSecret())
		{
			return base.GetClasses(null);
		}
		return this.GetEntityDef().GetClasses(null);
	}

	// Token: 0x060079C3 RID: 31171 RVA: 0x0027B653 File Offset: 0x00279853
	public TAG_ENCHANTMENT_VISUAL GetEnchantmentBirthVisual()
	{
		return this.GetEntityDef().GetEnchantmentBirthVisual();
	}

	// Token: 0x060079C4 RID: 31172 RVA: 0x0027B660 File Offset: 0x00279860
	public TAG_ENCHANTMENT_VISUAL GetEnchantmentIdleVisual()
	{
		return this.GetEntityDef().GetEnchantmentIdleVisual();
	}

	// Token: 0x060079C5 RID: 31173 RVA: 0x0027B66D File Offset: 0x0027986D
	public TAG_RARITY GetRarity()
	{
		return this.GetEntityDef().GetRarity();
	}

	// Token: 0x060079C6 RID: 31174 RVA: 0x0027B67A File Offset: 0x0027987A
	public new TAG_CARD_SET GetCardSet()
	{
		return this.GetEntityDef().GetCardSet();
	}

	// Token: 0x060079C7 RID: 31175 RVA: 0x0027B687 File Offset: 0x00279887
	public TAG_PREMIUM GetPremiumType()
	{
		return (TAG_PREMIUM)base.GetTag(GAME_TAG.PREMIUM);
	}

	// Token: 0x060079C8 RID: 31176 RVA: 0x0027B691 File Offset: 0x00279891
	public bool CanBeDamagedRealTime()
	{
		return !this.GetRealTimeDivineShield() && !this.GetRealTimeIsImmune() && (!this.GetRealTimeIsImmuneWhileAttacking() || !TargetReticleManager.Get() || TargetReticleManager.Get().ArrowSourceEntityID != base.GetEntityId());
	}

	// Token: 0x060079C9 RID: 31177 RVA: 0x0027B6CE File Offset: 0x002798CE
	public int GetCurrentHealth()
	{
		return base.GetTag(GAME_TAG.HEALTH) - base.GetTag(GAME_TAG.DAMAGE) - base.GetTag(GAME_TAG.PREDAMAGE);
	}

	// Token: 0x060079CA RID: 31178 RVA: 0x0027B6ED File Offset: 0x002798ED
	public int GetCurrentDurability()
	{
		return base.GetTag(GAME_TAG.DURABILITY) - base.GetTag(GAME_TAG.DAMAGE) - base.GetTag(GAME_TAG.PREDAMAGE);
	}

	// Token: 0x060079CB RID: 31179 RVA: 0x0027B70F File Offset: 0x0027990F
	public int GetCurrentDefense()
	{
		return this.GetCurrentHealth() + base.GetArmor();
	}

	// Token: 0x060079CC RID: 31180 RVA: 0x0027B71E File Offset: 0x0027991E
	public int GetCurrentVitality()
	{
		if (base.IsCharacter())
		{
			return this.GetCurrentDefense();
		}
		if (base.IsWeapon())
		{
			return this.GetCurrentDurability();
		}
		Error.AddDevFatal("Entity.GetCurrentVitality() should not be called on {0}. This entity is neither a character nor a weapon.", new object[]
		{
			this
		});
		return 0;
	}

	// Token: 0x060079CD RID: 31181 RVA: 0x0027B753 File Offset: 0x00279953
	public global::Player GetController()
	{
		return GameState.Get().GetPlayer(base.GetControllerId());
	}

	// Token: 0x060079CE RID: 31182 RVA: 0x0027B768 File Offset: 0x00279968
	public global::Player.Side GetControllerSide()
	{
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return global::Player.Side.NEUTRAL;
		}
		return controller.GetSide();
	}

	// Token: 0x060079CF RID: 31183 RVA: 0x0027B787 File Offset: 0x00279987
	public bool IsControlledByLocalUser()
	{
		return this.GetController().IsLocalUser();
	}

	// Token: 0x060079D0 RID: 31184 RVA: 0x0027B794 File Offset: 0x00279994
	public bool IsControlledByFriendlySidePlayer()
	{
		return this.GetController().IsFriendlySide();
	}

	// Token: 0x060079D1 RID: 31185 RVA: 0x0027B7A1 File Offset: 0x002799A1
	public bool IsControlledByOpposingSidePlayer()
	{
		return this.GetController().IsOpposingSide();
	}

	// Token: 0x060079D2 RID: 31186 RVA: 0x0027B7AE File Offset: 0x002799AE
	public bool IsControlledByRevealedPlayer()
	{
		return this.GetController().IsRevealed();
	}

	// Token: 0x060079D3 RID: 31187 RVA: 0x0027B7BB File Offset: 0x002799BB
	public bool IsControlledByConcealedPlayer()
	{
		return !this.IsControlledByRevealedPlayer();
	}

	// Token: 0x060079D4 RID: 31188 RVA: 0x0027B7C6 File Offset: 0x002799C6
	public global::Entity GetCreator()
	{
		return GameState.Get().GetEntity(base.GetCreatorId());
	}

	// Token: 0x060079D5 RID: 31189 RVA: 0x0027B7D8 File Offset: 0x002799D8
	public EntityDef GetCreatorDef()
	{
		return DefLoader.Get().GetEntityDef(base.GetCreatorDBID(), true);
	}

	// Token: 0x060079D6 RID: 31190 RVA: 0x0027B7EB File Offset: 0x002799EB
	public string GetDisplayedCreatorName()
	{
		return this.m_displayedCreatorName;
	}

	// Token: 0x060079D7 RID: 31191 RVA: 0x0027B7F4 File Offset: 0x002799F4
	public virtual global::Entity GetHero()
	{
		if (base.IsHero())
		{
			return this;
		}
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHero();
	}

	// Token: 0x060079D8 RID: 31192 RVA: 0x0027B820 File Offset: 0x00279A20
	public virtual Card GetHeroCard()
	{
		if (base.IsHero())
		{
			return this.GetCard();
		}
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroCard();
	}

	// Token: 0x060079D9 RID: 31193 RVA: 0x0027B850 File Offset: 0x00279A50
	public virtual global::Entity GetHeroPower()
	{
		if (base.IsHeroPower())
		{
			return this;
		}
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroPower();
	}

	// Token: 0x060079DA RID: 31194 RVA: 0x0027B87C File Offset: 0x00279A7C
	public virtual Card GetHeroPowerCard()
	{
		if (base.IsHeroPower())
		{
			return this.GetCard();
		}
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetHeroPowerCard();
	}

	// Token: 0x060079DB RID: 31195 RVA: 0x0027B8AC File Offset: 0x00279AAC
	public virtual Card GetWeaponCard()
	{
		if (base.IsWeapon())
		{
			return this.GetCard();
		}
		global::Player controller = this.GetController();
		if (controller == null)
		{
			return null;
		}
		return controller.GetWeaponCard();
	}

	// Token: 0x060079DC RID: 31196 RVA: 0x0027B8DA File Offset: 0x00279ADA
	public virtual bool HasValidDisplayName()
	{
		return this.GetEntityDef().HasValidDisplayName();
	}

	// Token: 0x060079DD RID: 31197 RVA: 0x0027B8E8 File Offset: 0x00279AE8
	public virtual string GetName()
	{
		int tag = base.GetTag(GAME_TAG.OVERRIDECARDNAME);
		EntityDef entityDef;
		if (tag > 0)
		{
			entityDef = DefLoader.Get().GetEntityDef(tag, true);
			if (entityDef != null)
			{
				return entityDef.GetName();
			}
		}
		entityDef = this.GetEntityDef();
		if (entityDef != null && entityDef.GetCardTextBuilder() != null)
		{
			return entityDef.GetCardTextBuilder().BuildCardName(this);
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			Debug.LogWarning(string.Format("Entity.GetName: No textbuilder found for {0}, returning default name", base.m_cardId));
		}
		return CardTextBuilder.GetDefaultCardName(this.GetEntityDef());
	}

	// Token: 0x060079DE RID: 31198 RVA: 0x0027B968 File Offset: 0x00279B68
	public virtual string GetDebugName()
	{
		if (this.m_cachedDebugName.Dirty)
		{
			string name = this.GetEntityDef().GetName();
			if (name != null)
			{
				this.m_cachedDebugName.Name = string.Format("[entityName={0} id={1} zone={2} zonePos={3} cardId={4} player={5}]", new object[]
				{
					name,
					base.GetEntityId(),
					base.GetZone(),
					base.GetZonePosition(),
					base.m_cardId,
					base.GetControllerId()
				});
			}
			else if (base.m_cardId != null)
			{
				this.m_cachedDebugName.Name = string.Format("[id={0} cardId={1} type={2} zone={3} zonePos={4} player={5}]", new object[]
				{
					base.GetEntityId(),
					base.m_cardId,
					base.GetCardType(),
					base.GetZone(),
					base.GetZonePosition(),
					base.GetControllerId()
				});
			}
			else
			{
				this.m_cachedDebugName.Name = string.Format("UNKNOWN ENTITY [id={0} type={1} zone={2} zonePos={3}]", new object[]
				{
					base.GetEntityId(),
					base.GetCardType(),
					base.GetZone(),
					base.GetZonePosition()
				});
			}
			this.m_cachedDebugName.Dirty = false;
		}
		return this.m_cachedDebugName.Name;
	}

	// Token: 0x060079DF RID: 31199 RVA: 0x0027BAD8 File Offset: 0x00279CD8
	public void UpdateCardName()
	{
		this.m_cachedDebugName.Dirty = true;
		if (this.m_card == null)
		{
			return;
		}
		string name = this.GetEntityDef().GetName();
		if (name != null)
		{
			if (string.IsNullOrEmpty(base.m_cardId))
			{
				this.m_card.gameObject.name = string.Format("{0} [id={1} zone={2} zonePos={3}]", new object[]
				{
					name,
					base.GetEntityId(),
					base.GetZone(),
					base.GetZonePosition()
				});
			}
			else
			{
				this.m_card.gameObject.name = string.Format("{0} [id={1} cardId={2} zone={3} zonePos={4} player={5}]", new object[]
				{
					name,
					base.GetEntityId(),
					base.GetCardId(),
					base.GetZone(),
					base.GetZonePosition(),
					base.GetControllerId()
				});
			}
		}
		else
		{
			this.m_card.gameObject.name = string.Format("Hidden Entity [id={0} zone={1} zonePos={2}]", base.GetEntityId(), base.GetZone(), base.GetZonePosition());
		}
		if (this.m_card.GetActor() != null)
		{
			this.m_card.GetActor().UpdateNameText();
		}
	}

	// Token: 0x060079E0 RID: 31200 RVA: 0x0027BC38 File Offset: 0x00279E38
	public string GetCardTextInHand()
	{
		string result;
		using (DefLoader.DisposableCardDef disposableCardDef = this.ShareDisposableCardDef())
		{
			if (((disposableCardDef != null) ? disposableCardDef.CardDef : null) == null)
			{
				Log.All.PrintError("Entity.GetCardTextInHand(): entity {0} does not have a CardDef", new object[]
				{
					base.GetEntityId()
				});
				result = string.Empty;
			}
			else
			{
				result = this.GetCardTextBuilder().BuildCardTextInHand(this);
			}
		}
		return result;
	}

	// Token: 0x060079E1 RID: 31201 RVA: 0x0027BCB8 File Offset: 0x00279EB8
	public string GetCardTextInHistory()
	{
		string result;
		using (DefLoader.DisposableCardDef disposableCardDef = this.ShareDisposableCardDef())
		{
			if (((disposableCardDef != null) ? disposableCardDef.CardDef : null) == null)
			{
				Log.All.PrintError("Entity.GetCardTextInHand(): entity {0} does not have a CardDef", new object[]
				{
					base.GetEntityId()
				});
				result = string.Empty;
			}
			else
			{
				result = this.GetCardTextBuilder().BuildCardTextInHistory(this);
			}
		}
		return result;
	}

	// Token: 0x060079E2 RID: 31202 RVA: 0x0027BD38 File Offset: 0x00279F38
	public string GetTargetingArrowText()
	{
		string result;
		using (DefLoader.DisposableCardDef disposableCardDef = this.ShareDisposableCardDef())
		{
			if (((disposableCardDef != null) ? disposableCardDef.CardDef : null) == null)
			{
				Log.All.PrintError("Entity.GetTargetingArrowText(): entity {0} does not have a CardDef", new object[]
				{
					base.GetEntityId()
				});
				result = string.Empty;
			}
			else
			{
				result = this.GetCardTextBuilder().GetTargetingArrowText(this);
			}
		}
		return result;
	}

	// Token: 0x060079E3 RID: 31203 RVA: 0x0027BDB8 File Offset: 0x00279FB8
	public string GetRaceText()
	{
		return this.GetEntityDef().GetRaceText();
	}

	// Token: 0x060079E4 RID: 31204 RVA: 0x0027BDC8 File Offset: 0x00279FC8
	public void AddAttachment(global::Entity entity)
	{
		int count = this.m_attachments.Count;
		if (this.m_attachments.Contains(entity))
		{
			Log.Gameplay.Print(string.Format("Entity.AddAttachment() - {0} is already an attachment of {1}", entity, this), Array.Empty<object>());
			return;
		}
		this.m_attachments.Add(entity);
		if (this.m_card == null)
		{
			return;
		}
		this.m_card.OnEnchantmentAdded(count, entity);
	}

	// Token: 0x060079E5 RID: 31205 RVA: 0x0027BE34 File Offset: 0x0027A034
	public void RemoveAttachment(global::Entity entity)
	{
		int count = this.m_attachments.Count;
		if (!this.m_attachments.Remove(entity))
		{
			Log.Gameplay.Print("Entity.RemoveAttachment() - {0} is not an attachment of {1}", new object[]
			{
				entity,
				this
			});
			return;
		}
		if (this.m_card == null)
		{
			return;
		}
		this.m_card.OnEnchantmentRemoved(count, entity);
	}

	// Token: 0x060079E6 RID: 31206 RVA: 0x0027BE95 File Offset: 0x0027A095
	private void AddSubCard(global::Entity entity)
	{
		if (this.m_subCardIDs.Contains(entity.GetEntityId()))
		{
			return;
		}
		this.m_subCardIDs.Add(entity.GetEntityId());
	}

	// Token: 0x060079E7 RID: 31207 RVA: 0x0027BEBC File Offset: 0x0027A0BC
	private void RemoveSubCard(global::Entity entity)
	{
		this.m_subCardIDs.Remove(entity.GetEntityId());
	}

	// Token: 0x060079E8 RID: 31208 RVA: 0x0027BED0 File Offset: 0x0027A0D0
	public List<global::Entity> GetAttachments()
	{
		return this.m_attachments;
	}

	// Token: 0x060079E9 RID: 31209 RVA: 0x0027BED8 File Offset: 0x0027A0D8
	public bool DoEnchantmentsHaveVoodooLink()
	{
		using (List<global::Entity>.Enumerator enumerator = this.m_attachments.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasTag(GAME_TAG.VOODOO_LINK))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060079EA RID: 31210 RVA: 0x0027BF38 File Offset: 0x0027A138
	public bool DoEnchantmentsHaveTriggerVisuals()
	{
		using (List<global::Entity>.Enumerator enumerator = this.m_attachments.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasTriggerVisual())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060079EB RID: 31211 RVA: 0x0027BF94 File Offset: 0x0027A194
	public bool DoEnchantmentsHaveOverKill()
	{
		using (List<global::Entity>.Enumerator enumerator = this.m_attachments.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasTag(GAME_TAG.OVERKILL))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060079EC RID: 31212 RVA: 0x0027BFF4 File Offset: 0x0027A1F4
	public bool DoEnchantmentsHaveSpellburst()
	{
		using (List<global::Entity>.Enumerator enumerator = this.m_attachments.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasSpellburst())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060079ED RID: 31213 RVA: 0x0027C050 File Offset: 0x0027A250
	public bool IsEnchanted()
	{
		return this.m_attachments.Count > 0;
	}

	// Token: 0x060079EE RID: 31214 RVA: 0x0027C060 File Offset: 0x0027A260
	public bool IsEnchantment()
	{
		return this.GetRealTimeCardType() == TAG_CARDTYPE.ENCHANTMENT;
	}

	// Token: 0x060079EF RID: 31215 RVA: 0x0027C06B File Offset: 0x0027A26B
	public bool IsDarkWandererSecret()
	{
		return base.IsSecret() && this.GetClass() == TAG_CLASS.WARRIOR;
	}

	// Token: 0x060079F0 RID: 31216 RVA: 0x0027C081 File Offset: 0x0027A281
	public bool IsDeathrattleDisabled()
	{
		return this.GetController() != null && this.GetController().HasTag(GAME_TAG.CANT_TRIGGER_DEATHRATTLE);
	}

	// Token: 0x060079F1 RID: 31217 RVA: 0x0027C09D File Offset: 0x0027A29D
	public List<global::Entity> GetEnchantments()
	{
		return this.GetAttachments();
	}

	// Token: 0x060079F2 RID: 31218 RVA: 0x0027C0A8 File Offset: 0x0027A2A8
	public List<global::Entity> GetDisplayedEnchantments(bool unique = false)
	{
		List<global::Entity> list = new List<global::Entity>(this.GetAttachments());
		list.RemoveAll((global::Entity enchant) => enchant.HasTag(GAME_TAG.ENCHANTMENT_INVISIBLE));
		if (!unique)
		{
			return list;
		}
		return list.Distinct(new global::Entity.EnchantmentComparer()).ToList<global::Entity>();
	}

	// Token: 0x060079F3 RID: 31219 RVA: 0x0027C0FC File Offset: 0x0027A2FC
	public bool HasSubCards()
	{
		return this.m_subCardIDs != null && this.m_subCardIDs.Count > 0;
	}

	// Token: 0x060079F4 RID: 31220 RVA: 0x0027C116 File Offset: 0x0027A316
	public List<int> GetSubCardIDs()
	{
		return this.m_subCardIDs;
	}

	// Token: 0x060079F5 RID: 31221 RVA: 0x0027C120 File Offset: 0x0027A320
	public int GetSubCardIndex(global::Entity entity)
	{
		if (entity == null)
		{
			return -1;
		}
		int entityId = entity.GetEntityId();
		for (int i = 0; i < this.m_subCardIDs.Count; i++)
		{
			if (this.m_subCardIDs[i] == entityId)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x060079F6 RID: 31222 RVA: 0x0027C164 File Offset: 0x0027A364
	public global::Entity GetParentEntity()
	{
		int tag = base.GetTag(GAME_TAG.PARENT_CARD);
		return GameState.Get().GetEntity(tag);
	}

	// Token: 0x060079F7 RID: 31223 RVA: 0x0027C188 File Offset: 0x0027A388
	public CardTextBuilder GetCardTextBuilder()
	{
		if (this.GetEntityDef() != null && this.GetEntityDef().GetCardTextBuilder() != null)
		{
			return this.GetEntityDef().GetCardTextBuilder();
		}
		if (!string.IsNullOrEmpty(base.m_cardId))
		{
			Debug.LogWarning(string.Format("Entity.GetCardTextBuilder: No textbuilder found for {0}, returning fallback text builder", base.m_cardId));
		}
		return CardTextBuilder.GetFallbackCardTextBuilder();
	}

	// Token: 0x060079F8 RID: 31224 RVA: 0x0027C1E0 File Offset: 0x0027A3E0
	public global::Entity CloneForZoneMgr()
	{
		global::Entity entity = new global::Entity();
		entity.m_staticEntityDef = this.GetEntityDef();
		entity.m_dynamicEntityDef = null;
		entity.m_card = this.m_card;
		entity.m_cardId = base.m_cardId;
		entity.ReplaceTags(this.m_tags);
		entity.m_loadState = this.m_loadState;
		return entity;
	}

	// Token: 0x060079F9 RID: 31225 RVA: 0x0027C238 File Offset: 0x0027A438
	public global::Entity CloneForHistory(HistoryInfo historyInfo)
	{
		global::Entity entity = new global::Entity();
		entity.m_duplicateForHistory = true;
		entity.m_staticEntityDef = this.GetEntityDef();
		entity.m_dynamicEntityDef = null;
		entity.m_card = this.m_card;
		entity.m_cardId = base.m_cardId;
		entity.ReplaceTags(this.m_tags);
		entity.m_cardTextHistoryData = this.GetCardTextBuilder().CreateCardTextHistoryData();
		entity.m_cardTextHistoryData.SetHistoryData(this, historyInfo);
		entity.m_subCardIDs = this.m_subCardIDs;
		if (!base.IsHero())
		{
			entity.SetTag<TAG_ZONE>(GAME_TAG.ZONE, TAG_ZONE.HAND);
		}
		entity.m_loadState = this.m_loadState;
		entity.m_displayedCreatorName = this.m_displayedCreatorName;
		entity.m_enchantmentCreatorCardIDForPortrait = this.m_enchantmentCreatorCardIDForPortrait;
		return entity;
	}

	// Token: 0x060079FA RID: 31226 RVA: 0x0027C2E9 File Offset: 0x0027A4E9
	public bool IsHistoryDupe()
	{
		return this.m_duplicateForHistory;
	}

	// Token: 0x060079FB RID: 31227 RVA: 0x0027C2F1 File Offset: 0x0027A4F1
	public int GetJadeGolem()
	{
		return Mathf.Min(this.GetController().GetTag(GAME_TAG.JADE_GOLEM) + 1, 30);
	}

	// Token: 0x060079FC RID: 31228 RVA: 0x0027C30C File Offset: 0x0027A50C
	public int GetDamageBonus()
	{
		global::Player controller = this.GetController();
		if (base.HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(base.GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		if (base.IsSpell() || base.IsMinion())
		{
			int num = controller.TotalSpellpower(base.GetSpellSchool());
			if (base.HasTag(GAME_TAG.RECEIVES_DOUBLE_SPELLDAMAGE_BONUS))
			{
				num *= 2;
			}
			return num;
		}
		if (base.IsHeroPower())
		{
			int num2 = controller.GetTag(GAME_TAG.CURRENT_HEROPOWER_DAMAGE_BONUS);
			if (this.GetCardTextBuilder() is SpellDamageOnlyCardTextBuilder)
			{
				int num3 = controller.TotalSpellpower(base.GetSpellSchool());
				if (base.HasTag(GAME_TAG.RECEIVES_DOUBLE_SPELLDAMAGE_BONUS))
				{
					num3 *= 2;
				}
				num2 += num3;
			}
			return num2;
		}
		return 0;
	}

	// Token: 0x060079FD RID: 31229 RVA: 0x0027C3C4 File Offset: 0x0027A5C4
	public int GetDamageBonusDouble()
	{
		global::Player controller = this.GetController();
		if (base.HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(base.GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		if (base.IsSpell())
		{
			return controller.GetTag(GAME_TAG.SPELLPOWER_DOUBLE);
		}
		if (base.IsHeroPower())
		{
			return controller.GetTag(GAME_TAG.HERO_POWER_DOUBLE);
		}
		return 0;
	}

	// Token: 0x060079FE RID: 31230 RVA: 0x0027C430 File Offset: 0x0027A630
	public int GetHealingDouble()
	{
		global::Player controller = this.GetController();
		if (base.HasTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT))
		{
			controller = GameState.Get().GetEntity(base.GetTag(GAME_TAG.SOURCE_OVERRIDE_FOR_MODIFIER_TEXT)).GetController();
		}
		if (controller == null)
		{
			return 0;
		}
		int tag = controller.GetTag(GAME_TAG.ALL_HEALING_DOUBLE);
		if (base.IsSpell())
		{
			return controller.GetTag(GAME_TAG.SPELL_HEALING_DOUBLE) + tag;
		}
		if (base.IsHeroPower())
		{
			return controller.GetTag(GAME_TAG.HERO_POWER_DOUBLE) + tag;
		}
		return tag;
	}

	// Token: 0x060079FF RID: 31231 RVA: 0x0027C4AA File Offset: 0x0027A6AA
	public void ClearBattlecryFlag()
	{
		this.m_useBattlecryPower = false;
	}

	// Token: 0x06007A00 RID: 31232 RVA: 0x0027C4B3 File Offset: 0x0027A6B3
	public bool ShouldUseBattlecryPower()
	{
		return this.m_useBattlecryPower;
	}

	// Token: 0x06007A01 RID: 31233 RVA: 0x0027C4BC File Offset: 0x0027A6BC
	public void UpdateUseBattlecryFlag(bool fromGameState)
	{
		if (!base.IsMinion())
		{
			return;
		}
		bool flag = fromGameState || GameState.Get().EntityHasTargets(this);
		if (TAG_ZONE.HAND == base.GetZone() && flag)
		{
			this.m_useBattlecryPower = true;
		}
	}

	// Token: 0x06007A02 RID: 31234 RVA: 0x0027C4F8 File Offset: 0x0027A6F8
	public virtual void InitRealTimeValues(List<Network.Entity.Tag> tags)
	{
		foreach (Network.Entity.Tag tag in tags)
		{
			GAME_TAG name = (GAME_TAG)tag.Name;
			if (name <= GAME_TAG.ZONE_POSITION)
			{
				if (name <= GAME_TAG.DIVINE_SHIELD)
				{
					if (name <= GAME_TAG.ZONE)
					{
						if (name == GAME_TAG.PREMIUM)
						{
							this.SetRealTimePremium((TAG_PREMIUM)tag.Value);
							continue;
						}
						switch (name)
						{
						case GAME_TAG.DAMAGE:
							this.SetRealTimeDamage(tag.Value);
							continue;
						case GAME_TAG.HEALTH:
							break;
						case (GAME_TAG)46:
							continue;
						case GAME_TAG.ATK:
							this.SetRealTimeAttack(tag.Value);
							continue;
						case GAME_TAG.COST:
							this.SetRealTimeCost(tag.Value);
							continue;
						case GAME_TAG.ZONE:
							this.SetRealTimeZone(tag.Value);
							continue;
						default:
							continue;
						}
					}
					else if (name != GAME_TAG.DURABILITY)
					{
						if (name != GAME_TAG.DIVINE_SHIELD)
						{
							continue;
						}
						this.SetRealTimeDivineShield(tag.Value);
						continue;
					}
					this.SetRealTimeHealth(tag.Value);
				}
				else if (name <= GAME_TAG.IMMUNE)
				{
					if (name != GAME_TAG.CARDTYPE)
					{
						if (name == GAME_TAG.IMMUNE)
						{
							this.SetRealTimeIsImmune(tag.Value);
						}
					}
					else
					{
						this.SetRealTimeCardType((TAG_CARDTYPE)tag.Value);
					}
				}
				else if (name != GAME_TAG.LINKED_ENTITY)
				{
					if (name == GAME_TAG.ZONE_POSITION)
					{
						this.SetRealTimeZonePosition(tag.Value);
					}
				}
				else
				{
					this.SetRealTimeLinkedEntityId(tag.Value);
				}
			}
			else
			{
				if (name <= GAME_TAG.POWERED_UP)
				{
					if (name <= GAME_TAG.POISONOUS)
					{
						if (name == GAME_TAG.ARMOR)
						{
							this.SetRealTimeArmor(tag.Value);
							continue;
						}
						if (name != GAME_TAG.POISONOUS)
						{
							continue;
						}
					}
					else
					{
						if (name == GAME_TAG.IMMUNE_WHILE_ATTACKING)
						{
							this.SetRealTimeIsImmuneWhileAttacking(tag.Value);
							continue;
						}
						if (name != GAME_TAG.POWERED_UP)
						{
							continue;
						}
						this.SetRealTimePoweredUp(tag.Value);
						continue;
					}
				}
				else if (name <= GAME_TAG.ATTACKABLE_BY_RUSH)
				{
					if (name == GAME_TAG.CARD_COSTS_HEALTH)
					{
						this.SetRealTimeCardCostsHealth(tag.Value);
						continue;
					}
					if (name != GAME_TAG.ATTACKABLE_BY_RUSH)
					{
						continue;
					}
					this.SetRealTimeAttackableByRush(tag.Value);
					continue;
				}
				else
				{
					if (name == GAME_TAG.PLAYER_LEADERBOARD_PLACE)
					{
						this.SetRealTimePlayerLeaderboardPlace(tag.Value);
						continue;
					}
					if (name == GAME_TAG.PLAYER_TECH_LEVEL)
					{
						this.SetRealTimePlayerTechLevel(tag.Value);
						continue;
					}
					if (name != GAME_TAG.NON_KEYWORD_POISONOUS)
					{
						continue;
					}
				}
				this.SetRealTimeIsPoisonous(tag.Value);
			}
		}
	}

	// Token: 0x06007A03 RID: 31235 RVA: 0x0027C7AC File Offset: 0x0027A9AC
	public void SetRealTimeCost(int newCost)
	{
		this.m_realTimeCost = newCost;
	}

	// Token: 0x06007A04 RID: 31236 RVA: 0x0027C7B5 File Offset: 0x0027A9B5
	public int GetRealTimeCost()
	{
		if (this.m_realTimeCost == -1)
		{
			return base.GetCost();
		}
		return this.m_realTimeCost;
	}

	// Token: 0x06007A05 RID: 31237 RVA: 0x0027C7CD File Offset: 0x0027A9CD
	public void SetRealTimeAttack(int newAttack)
	{
		this.m_realTimeAttack = newAttack;
	}

	// Token: 0x06007A06 RID: 31238 RVA: 0x0027C7D6 File Offset: 0x0027A9D6
	public int GetRealTimeAttack()
	{
		return this.m_realTimeAttack;
	}

	// Token: 0x06007A07 RID: 31239 RVA: 0x0027C7DE File Offset: 0x0027A9DE
	public void SetRealTimeHealth(int newHealth)
	{
		this.m_realTimeHealth = newHealth;
	}

	// Token: 0x06007A08 RID: 31240 RVA: 0x0027C7E7 File Offset: 0x0027A9E7
	public void SetRealTimeDamage(int newDamage)
	{
		this.m_realTimeDamage = newDamage;
	}

	// Token: 0x06007A09 RID: 31241 RVA: 0x0027C7F0 File Offset: 0x0027A9F0
	public void SetRealTimeArmor(int newArmor)
	{
		this.m_realTimeArmor = newArmor;
	}

	// Token: 0x06007A0A RID: 31242 RVA: 0x0027C7F9 File Offset: 0x0027A9F9
	public int GetRealTimeRemainingHP()
	{
		return this.m_realTimeHealth + this.m_realTimeArmor - this.m_realTimeDamage;
	}

	// Token: 0x06007A0B RID: 31243 RVA: 0x0027C80F File Offset: 0x0027AA0F
	public void SetRealTimeZone(int zone)
	{
		this.m_realTimeZone = zone;
	}

	// Token: 0x06007A0C RID: 31244 RVA: 0x0027C818 File Offset: 0x0027AA18
	public TAG_ZONE GetRealTimeZone()
	{
		return (TAG_ZONE)this.m_realTimeZone;
	}

	// Token: 0x06007A0D RID: 31245 RVA: 0x0027C820 File Offset: 0x0027AA20
	public void SetRealTimeZonePosition(int zonePosition)
	{
		this.m_realTimeZonePosition = zonePosition;
	}

	// Token: 0x06007A0E RID: 31246 RVA: 0x0027C829 File Offset: 0x0027AA29
	public int GetRealTimeZonePosition()
	{
		return this.m_realTimeZonePosition;
	}

	// Token: 0x06007A0F RID: 31247 RVA: 0x0027C831 File Offset: 0x0027AA31
	public void SetRealTimeLinkedEntityId(int linkedEntityId)
	{
		this.m_realTimeLinkedEntityId = linkedEntityId;
	}

	// Token: 0x06007A10 RID: 31248 RVA: 0x0027C83A File Offset: 0x0027AA3A
	public int GetRealTimeLinkedEntityId()
	{
		return this.m_realTimeLinkedEntityId;
	}

	// Token: 0x06007A11 RID: 31249 RVA: 0x0027C842 File Offset: 0x0027AA42
	public void SetRealTimePoweredUp(int poweredUp)
	{
		this.m_realTimePoweredUp = (poweredUp > 0);
	}

	// Token: 0x06007A12 RID: 31250 RVA: 0x0027C852 File Offset: 0x0027AA52
	public bool GetRealTimePoweredUp()
	{
		return this.m_realTimePoweredUp;
	}

	// Token: 0x06007A13 RID: 31251 RVA: 0x0027C85A File Offset: 0x0027AA5A
	public void SetRealTimeDivineShield(int divineShield)
	{
		this.m_realTimeDivineShield = (divineShield > 0);
	}

	// Token: 0x06007A14 RID: 31252 RVA: 0x0027C86A File Offset: 0x0027AA6A
	public bool GetRealTimeDivineShield()
	{
		return this.m_realTimeDivineShield;
	}

	// Token: 0x06007A15 RID: 31253 RVA: 0x0027C872 File Offset: 0x0027AA72
	public void SetRealTimeIsImmune(int immune)
	{
		this.m_realTimeIsImmune = (immune > 0);
	}

	// Token: 0x06007A16 RID: 31254 RVA: 0x0027C882 File Offset: 0x0027AA82
	public bool GetRealTimeIsImmune()
	{
		return this.m_realTimeIsImmune;
	}

	// Token: 0x06007A17 RID: 31255 RVA: 0x0027C88A File Offset: 0x0027AA8A
	public void SetRealTimeIsImmuneWhileAttacking(int immune)
	{
		this.m_realTimeIsImmuneWhileAttacking = (immune > 0);
	}

	// Token: 0x06007A18 RID: 31256 RVA: 0x0027C89A File Offset: 0x0027AA9A
	public bool GetRealTimeIsImmuneWhileAttacking()
	{
		return this.m_realTimeIsImmuneWhileAttacking;
	}

	// Token: 0x06007A19 RID: 31257 RVA: 0x0027C8A2 File Offset: 0x0027AAA2
	public void SetRealTimeIsPoisonous(int poisonous)
	{
		this.m_realTimeIsPoisonous = (poisonous > 0);
	}

	// Token: 0x06007A1A RID: 31258 RVA: 0x0027C8B2 File Offset: 0x0027AAB2
	public bool GetRealTimeIsPoisonous()
	{
		return this.m_realTimeIsPoisonous;
	}

	// Token: 0x06007A1B RID: 31259 RVA: 0x0027C8BA File Offset: 0x0027AABA
	public void SetRealTimeIsDormant(int dormant)
	{
		this.m_realTimeIsDormant = (dormant > 0);
	}

	// Token: 0x06007A1C RID: 31260 RVA: 0x0027C8C6 File Offset: 0x0027AAC6
	public bool GetRealTimeIsDormant()
	{
		return this.m_realTimeIsDormant;
	}

	// Token: 0x06007A1D RID: 31261 RVA: 0x0027C8CE File Offset: 0x0027AACE
	public void SetRealTimeHasSpellpower(int spellpower)
	{
		this.m_realTimeSpellpower = spellpower;
	}

	// Token: 0x06007A1E RID: 31262 RVA: 0x0027C8D7 File Offset: 0x0027AAD7
	public int GetRealTimeSpellpower()
	{
		return this.m_realTimeSpellpower;
	}

	// Token: 0x06007A1F RID: 31263 RVA: 0x0027C8DF File Offset: 0x0027AADF
	public void SetRealTimeSpellpowerDouble(int powerDouble)
	{
		this.m_realTimeSpellpowerDouble = (powerDouble > 0);
	}

	// Token: 0x06007A20 RID: 31264 RVA: 0x0027C8EB File Offset: 0x0027AAEB
	public bool GetRealTimeSpellpowerDouble()
	{
		return this.m_realTimeSpellpowerDouble;
	}

	// Token: 0x06007A21 RID: 31265 RVA: 0x0027C8F3 File Offset: 0x0027AAF3
	public void SetRealTimeHealingDoesDamageHint(int healingDoesDamageHint)
	{
		this.m_realTimeHealingDoesDamageHint = (healingDoesDamageHint > 0);
	}

	// Token: 0x06007A22 RID: 31266 RVA: 0x0027C8FF File Offset: 0x0027AAFF
	public bool GetRealTimeHealingDoeDamageHint()
	{
		return this.m_realTimeHealingDoesDamageHint;
	}

	// Token: 0x06007A23 RID: 31267 RVA: 0x0027C907 File Offset: 0x0027AB07
	public void SetRealTimeLifestealDoesDamageHint(int lifestealDoesDamageHint)
	{
		this.m_realTimeLifestealDoesDamageHint = (lifestealDoesDamageHint > 0);
	}

	// Token: 0x06007A24 RID: 31268 RVA: 0x0027C913 File Offset: 0x0027AB13
	public bool GetRealTimeLifestealDoesDamageHint()
	{
		return this.m_realTimeLifestealDoesDamageHint;
	}

	// Token: 0x06007A25 RID: 31269 RVA: 0x0027C91B File Offset: 0x0027AB1B
	public void SetRealTimeCardCostsHealth(int value)
	{
		this.m_realTimeCardCostsHealth = (value > 0);
	}

	// Token: 0x06007A26 RID: 31270 RVA: 0x0027C92B File Offset: 0x0027AB2B
	public bool GetRealTimeCardCostsHealth()
	{
		return this.m_realTimeCardCostsHealth;
	}

	// Token: 0x06007A27 RID: 31271 RVA: 0x0027C933 File Offset: 0x0027AB33
	public void SetRealTimeAttackableByRush(int value)
	{
		this.m_realTimeAttackableByRush = (value > 0);
	}

	// Token: 0x06007A28 RID: 31272 RVA: 0x0027C943 File Offset: 0x0027AB43
	public bool GetRealTimeAttackableByRush()
	{
		return this.m_realTimeAttackableByRush;
	}

	// Token: 0x06007A29 RID: 31273 RVA: 0x0027C94B File Offset: 0x0027AB4B
	public void SetRealTimeCardType(TAG_CARDTYPE cardType)
	{
		this.m_realTimeCardType = cardType;
	}

	// Token: 0x06007A2A RID: 31274 RVA: 0x0027C954 File Offset: 0x0027AB54
	public TAG_CARDTYPE GetRealTimeCardType()
	{
		return this.m_realTimeCardType;
	}

	// Token: 0x06007A2B RID: 31275 RVA: 0x0027C95C File Offset: 0x0027AB5C
	public void SetRealTimePremium(TAG_PREMIUM premium)
	{
		this.m_realTimePremium = premium;
	}

	// Token: 0x06007A2C RID: 31276 RVA: 0x0027C965 File Offset: 0x0027AB65
	public TAG_PREMIUM GetRealTimePremium()
	{
		return this.m_realTimePremium;
	}

	// Token: 0x06007A2D RID: 31277 RVA: 0x0027C96D File Offset: 0x0027AB6D
	public void SetRealTimePlayerLeaderboardPlace(int playerLeaderboardPlace)
	{
		this.m_realTimePlayerLeaderboardPlace = playerLeaderboardPlace;
	}

	// Token: 0x06007A2E RID: 31278 RVA: 0x0027C976 File Offset: 0x0027AB76
	public int GetRealTimePlayerLeaderboardPlace()
	{
		return this.m_realTimePlayerLeaderboardPlace;
	}

	// Token: 0x06007A2F RID: 31279 RVA: 0x0027C97E File Offset: 0x0027AB7E
	public void SetRealTimePlayerTechLevel(int playerTechLevel)
	{
		this.m_realTimePlayerTechLevel = playerTechLevel;
	}

	// Token: 0x06007A30 RID: 31280 RVA: 0x0027C987 File Offset: 0x0027AB87
	public int GetRealTimePlayerTechLevel()
	{
		return this.m_realTimePlayerTechLevel;
	}

	// Token: 0x06007A31 RID: 31281 RVA: 0x0027C98F File Offset: 0x0027AB8F
	public CardTextHistoryData GetCardTextHistoryData()
	{
		return this.m_cardTextHistoryData;
	}

	// Token: 0x06007A32 RID: 31282 RVA: 0x0027C998 File Offset: 0x0027AB98
	private void LoadEntityDef(string cardId)
	{
		if (base.m_cardId != cardId)
		{
			base.m_cardId = cardId;
		}
		if (string.IsNullOrEmpty(cardId))
		{
			return;
		}
		this.m_dynamicEntityDef = null;
		this.m_staticEntityDef = DefLoader.Get().GetEntityDef(cardId);
		if (this.m_staticEntityDef == null)
		{
			Error.AddDevFatal("Failed to load a card xml for {0}", new object[]
			{
				cardId
			});
			return;
		}
		this.UpdateCardName();
	}

	// Token: 0x06007A33 RID: 31283 RVA: 0x0027CA00 File Offset: 0x0027AC00
	public void LoadCard(string cardId, global::Entity.LoadCardData data = null)
	{
		this.LoadEntityDef(cardId);
		this.m_loadState = global::Entity.LoadState.LOADING;
		if (string.IsNullOrEmpty(cardId))
		{
			DefLoader.Get().LoadCardDef("HiddenCard", new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnCardDefLoaded), null, null);
			return;
		}
		DefLoader.Get().LoadCardDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnCardDefLoaded), data, null);
	}

	// Token: 0x06007A34 RID: 31284 RVA: 0x0027CA5C File Offset: 0x0027AC5C
	private void OnCardDefLoaded(string cardId, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		try
		{
			if (cardDef == null)
			{
				Debug.LogErrorFormat("Entity.OnCardDefLoaded() - {0} does not have an asset!", new object[]
				{
					cardId
				});
				this.m_loadState = global::Entity.LoadState.DONE;
			}
			else
			{
				global::Entity.LoadCardData loadCardData = new global::Entity.LoadCardData
				{
					updateActor = false,
					restartStateSpells = false,
					fromChangeEntity = false
				};
				if (callbackData is global::Entity.LoadCardData)
				{
					loadCardData = (global::Entity.LoadCardData)callbackData;
				}
				if (this.m_card != null)
				{
					this.m_card.SetCardDef(cardDef, loadCardData.updateActor);
					if (loadCardData.updateActor)
					{
						this.m_card.UpdateActor(false, null);
						this.m_card.ActivateStateSpells(false);
					}
					else if (loadCardData.restartStateSpells)
					{
						this.m_card.ActivateStateSpells(true);
					}
					this.m_card.RefreshHeroPowerTooltip();
					if (loadCardData.fromChangeEntity && base.IsMinion() && this.m_card.GetZone() is ZonePlay)
					{
						this.m_card.ActivateCharacterPlayEffects();
					}
				}
				this.UpdateUseBattlecryFlag(false);
				this.m_loadState = global::Entity.LoadState.DONE;
				if (this.m_card != null)
				{
					this.m_card.RefreshActor();
				}
			}
		}
		finally
		{
			if (cardDef != null)
			{
				((IDisposable)cardDef).Dispose();
			}
		}
	}

	// Token: 0x06007A35 RID: 31285 RVA: 0x0027CB9C File Offset: 0x0027AD9C
	public SpellType GetPrioritizedBaubleSpellType()
	{
		if (base.IsPoisonous())
		{
			return SpellType.POISONOUS;
		}
		if (base.HasTriggerVisual() || this.DoEnchantmentsHaveTriggerVisuals())
		{
			return SpellType.TRIGGER;
		}
		if (base.HasLifesteal())
		{
			return SpellType.LIFESTEAL;
		}
		if (base.HasInspire())
		{
			return SpellType.INSPIRE;
		}
		if (base.HasOverKill() || this.DoEnchantmentsHaveOverKill())
		{
			return SpellType.OVERKILL;
		}
		if (base.HasSpellburst() || this.DoEnchantmentsHaveSpellburst())
		{
			return SpellType.SPELLBURST;
		}
		if (base.HasFrenzy())
		{
			return SpellType.FRENZY;
		}
		return SpellType.NONE;
	}

	// Token: 0x06007A36 RID: 31286 RVA: 0x0027CC18 File Offset: 0x0027AE18
	public TAG_CARD_SET GetWatermarkCardSetOverride()
	{
		if (base.HasTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET))
		{
			return (TAG_CARD_SET)base.GetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET);
		}
		EntityDef entityDef = this.GetEntityDef();
		if (entityDef != null && entityDef.HasTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET))
		{
			return (TAG_CARD_SET)entityDef.GetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET);
		}
		return TAG_CARD_SET.INVALID;
	}

	// Token: 0x06007A37 RID: 31287 RVA: 0x0027CC62 File Offset: 0x0027AE62
	public bool IsTauntIgnored()
	{
		return GameState.Get().GetFirstOpponentPlayer(this.GetController()).HasTag(GAME_TAG.IGNORE_TAUNT);
	}

	// Token: 0x04005EA7 RID: 24231
	private EntityDef m_staticEntityDef = new EntityDef();

	// Token: 0x04005EA8 RID: 24232
	private EntityDef m_dynamicEntityDef;

	// Token: 0x04005EA9 RID: 24233
	private Card m_card;

	// Token: 0x04005EAA RID: 24234
	private global::Entity.LoadState m_loadState;

	// Token: 0x04005EAB RID: 24235
	private int m_cardAssetLoadCount;

	// Token: 0x04005EAC RID: 24236
	private bool m_useBattlecryPower;

	// Token: 0x04005EAD RID: 24237
	private bool m_duplicateForHistory;

	// Token: 0x04005EAE RID: 24238
	private CardTextHistoryData m_cardTextHistoryData;

	// Token: 0x04005EAF RID: 24239
	private List<global::Entity> m_attachments = new List<global::Entity>();

	// Token: 0x04005EB0 RID: 24240
	private List<int> m_subCardIDs = new List<int>();

	// Token: 0x04005EB1 RID: 24241
	private int m_realTimeCost;

	// Token: 0x04005EB2 RID: 24242
	private int m_realTimeAttack;

	// Token: 0x04005EB3 RID: 24243
	private int m_realTimeHealth;

	// Token: 0x04005EB4 RID: 24244
	private int m_realTimeDamage;

	// Token: 0x04005EB5 RID: 24245
	private int m_realTimeArmor;

	// Token: 0x04005EB6 RID: 24246
	private int m_realTimeZone;

	// Token: 0x04005EB7 RID: 24247
	private int m_realTimeZonePosition;

	// Token: 0x04005EB8 RID: 24248
	private int m_realTimeLinkedEntityId;

	// Token: 0x04005EB9 RID: 24249
	private bool m_realTimePoweredUp;

	// Token: 0x04005EBA RID: 24250
	private bool m_realTimeDivineShield;

	// Token: 0x04005EBB RID: 24251
	private bool m_realTimeIsImmune;

	// Token: 0x04005EBC RID: 24252
	private bool m_realTimeIsImmuneWhileAttacking;

	// Token: 0x04005EBD RID: 24253
	private bool m_realTimeIsPoisonous;

	// Token: 0x04005EBE RID: 24254
	private bool m_realTimeIsDormant;

	// Token: 0x04005EBF RID: 24255
	private int m_realTimeSpellpower;

	// Token: 0x04005EC0 RID: 24256
	private bool m_realTimeSpellpowerDouble;

	// Token: 0x04005EC1 RID: 24257
	private bool m_realTimeHealingDoesDamageHint;

	// Token: 0x04005EC2 RID: 24258
	private bool m_realTimeLifestealDoesDamageHint;

	// Token: 0x04005EC3 RID: 24259
	private bool m_realTimeCardCostsHealth;

	// Token: 0x04005EC4 RID: 24260
	private bool m_realTimeAttackableByRush;

	// Token: 0x04005EC5 RID: 24261
	private TAG_CARDTYPE m_realTimeCardType;

	// Token: 0x04005EC6 RID: 24262
	private TAG_PREMIUM m_realTimePremium;

	// Token: 0x04005EC7 RID: 24263
	private int m_realTimePlayerLeaderboardPlace;

	// Token: 0x04005EC8 RID: 24264
	private int m_realTimePlayerTechLevel;

	// Token: 0x04005EC9 RID: 24265
	private int m_queuedRealTimeControllerTagChangeCount;

	// Token: 0x04005ECA RID: 24266
	private int m_queuedChangeEntityCount;

	// Token: 0x04005ECB RID: 24267
	private List<Network.HistChangeEntity> m_transformPowersProcessed = new List<Network.HistChangeEntity>();

	// Token: 0x04005ECC RID: 24268
	private string m_displayedCreatorName;

	// Token: 0x04005ECD RID: 24269
	private string m_enchantmentCreatorCardIDForPortrait;

	// Token: 0x04005ECE RID: 24270
	private global::Entity.CachedDebugName m_cachedDebugName;

	// Token: 0x0200251F RID: 9503
	public enum LoadState
	{
		// Token: 0x0400ECBA RID: 60602
		INVALID,
		// Token: 0x0400ECBB RID: 60603
		PENDING,
		// Token: 0x0400ECBC RID: 60604
		LOADING,
		// Token: 0x0400ECBD RID: 60605
		DONE
	}

	// Token: 0x02002520 RID: 9504
	public class LoadCardData
	{
		// Token: 0x0400ECBE RID: 60606
		public bool updateActor;

		// Token: 0x0400ECBF RID: 60607
		public bool restartStateSpells;

		// Token: 0x0400ECC0 RID: 60608
		public bool fromChangeEntity;
	}

	// Token: 0x02002521 RID: 9505
	private struct CachedDebugName
	{
		// Token: 0x0400ECC1 RID: 60609
		public bool Dirty;

		// Token: 0x0400ECC2 RID: 60610
		public string Name;
	}

	// Token: 0x02002522 RID: 9506
	private class EnchantmentComparer : IEqualityComparer<global::Entity>
	{
		// Token: 0x06013215 RID: 78357 RVA: 0x0052A96A File Offset: 0x00528B6A
		public bool Equals(global::Entity a, global::Entity b)
		{
			return a == b || (a != null && b != null && a.GetCardId() == b.GetCardId());
		}

		// Token: 0x06013216 RID: 78358 RVA: 0x0052A98B File Offset: 0x00528B8B
		public int GetHashCode(global::Entity entity)
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
}
