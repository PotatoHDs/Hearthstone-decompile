using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x0200031C RID: 796
public class HistoryManager : CardTileListDisplay
{
	// Token: 0x06002C90 RID: 11408 RVA: 0x000E0260 File Offset: 0x000DE460
	protected override void Awake()
	{
		base.Awake();
		HistoryManager.s_instance = this;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.15f, base.transform.position.z);
		this.m_queuedEntriesPrevious.Clear();
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x000E02CA File Offset: 0x000DE4CA
	protected override void OnDestroy()
	{
		HistoryManager.s_instance = null;
		base.OnDestroy();
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x000E02D8 File Offset: 0x000DE4D8
	protected override void Start()
	{
		base.Start();
		base.StartCoroutine(this.WaitForBoardLoadedAndSetPaths());
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x000E02ED File Offset: 0x000DE4ED
	public static HistoryManager Get()
	{
		return HistoryManager.s_instance;
	}

	// Token: 0x06002C94 RID: 11412 RVA: 0x000E02F4 File Offset: 0x000DE4F4
	public bool IsHistoryEnabled()
	{
		return !this.m_historyDisabled;
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x000E02FF File Offset: 0x000DE4FF
	public void DisableHistory()
	{
		this.m_historyDisabled = true;
		base.GetComponent<Collider>().enabled = false;
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x000E0314 File Offset: 0x000DE514
	public void EnableHistory()
	{
		this.m_historyDisabled = false;
		base.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x000E032C File Offset: 0x000DE52C
	private global::Entity CreatePreTransformedEntity(global::Entity entity)
	{
		int tag = entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
		if (tag == 0)
		{
			return null;
		}
		string text = GameUtils.TranslateDbIdToCardId(tag, false);
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		global::Entity entity2 = new global::Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(text, null);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag<TAG_ZONE>(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag<TAG_PREMIUM>(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag<TAG_CARD_SET>(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	// Token: 0x06002C98 RID: 11416 RVA: 0x000E03C0 File Offset: 0x000DE5C0
	private global::Entity CreatePostTransformedEntity(global::Entity entity)
	{
		string cardId = entity.GetCardId();
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		global::Entity entity2 = new global::Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(cardId, null);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag<TAG_ZONE>(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag<TAG_PREMIUM>(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag<TAG_CARD_SET>(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	// Token: 0x06002C99 RID: 11417 RVA: 0x000E0440 File Offset: 0x000DE640
	private global::Entity CreateSecretDeathrattleEntity(global::Entity entity)
	{
		if (!entity.HasSecretDeathrattle())
		{
			return null;
		}
		string text = "GIL_222t";
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		global::Entity entity2 = new global::Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(text, null);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag<TAG_ZONE>(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag<TAG_PREMIUM>(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag<TAG_CARD_SET>(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	// Token: 0x06002C9A RID: 11418 RVA: 0x000E04CC File Offset: 0x000DE6CC
	public void CreatePlayedTile(global::Entity playedEntity, global::Entity targetedEntity)
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry tileEntry = new HistoryManager.TileEntry();
		this.m_queuedEntries.Add(tileEntry);
		tileEntry.SetCardPlayed(playedEntity);
		tileEntry.SetCardTargeted(targetedEntity);
		if (tileEntry.m_lastCardPlayed.GetDuplicatedEntity() == null)
		{
			base.StartCoroutine("WaitForCardLoadedAndDuplicateInfo", tileEntry.m_lastCardPlayed);
		}
	}

	// Token: 0x06002C9B RID: 11419 RVA: 0x000E0524 File Offset: 0x000DE724
	public void CreateTriggerTile(global::Entity triggeredEntity)
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry tileEntry = new HistoryManager.TileEntry();
		this.m_queuedEntries.Add(tileEntry);
		tileEntry.SetCardTriggered(triggeredEntity);
	}

	// Token: 0x06002C9C RID: 11420 RVA: 0x000E0554 File Offset: 0x000DE754
	public void CreateAttackTile(global::Entity attacker, global::Entity defender, PowerTaskList taskList)
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry tileEntry = new HistoryManager.TileEntry();
		this.m_queuedEntries.Add(tileEntry);
		tileEntry.SetAttacker(attacker);
		tileEntry.SetDefender(defender);
		global::Entity duplicatedEntity = tileEntry.m_lastAttacker.GetDuplicatedEntity();
		global::Entity duplicatedEntity2 = tileEntry.m_lastDefender.GetDuplicatedEntity();
		int entityId = attacker.GetEntityId();
		int entityId2 = defender.GetEntityId();
		int num = -1;
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.DAMAGE && histMetaData.Info.Contains(entityId2))
				{
					num = i;
					break;
				}
			}
		}
		for (int j = 0; j < num; j++)
		{
			Network.PowerHistory power2 = taskList2[j].GetPower();
			switch (power2.Type)
			{
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power2;
				if (entityId == histShowEntity.Entity.ID)
				{
					GameUtils.ApplyShowEntity(duplicatedEntity, histShowEntity);
				}
				if (entityId2 == histShowEntity.Entity.ID)
				{
					GameUtils.ApplyShowEntity(duplicatedEntity2, histShowEntity);
				}
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity histHideEntity = (Network.HistHideEntity)power2;
				if (entityId == histHideEntity.Entity)
				{
					GameUtils.ApplyHideEntity(duplicatedEntity, histHideEntity);
				}
				if (entityId2 == histHideEntity.Entity)
				{
					GameUtils.ApplyHideEntity(duplicatedEntity2, histHideEntity);
				}
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power2;
				if (entityId == histTagChange.Entity)
				{
					GameUtils.ApplyTagChange(duplicatedEntity, histTagChange);
				}
				if (entityId2 == histTagChange.Entity)
				{
					GameUtils.ApplyTagChange(duplicatedEntity2, histTagChange);
				}
				break;
			}
			}
		}
	}

	// Token: 0x06002C9D RID: 11421 RVA: 0x000E06F8 File Offset: 0x000DE8F8
	public void CreateFatigueTile()
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry tileEntry = new HistoryManager.TileEntry();
		this.m_queuedEntries.Add(tileEntry);
		tileEntry.SetFatigue();
	}

	// Token: 0x06002C9E RID: 11422 RVA: 0x000E0728 File Offset: 0x000DE928
	public void CreateBurnedCardsTile()
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry tileEntry = new HistoryManager.TileEntry();
		this.m_queuedEntries.Add(tileEntry);
		tileEntry.SetBurnedCards();
	}

	// Token: 0x06002C9F RID: 11423 RVA: 0x000E0758 File Offset: 0x000DE958
	public void MarkCurrentHistoryEntryAsCompleted()
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.MarkCurrentHistoryEntryAsCompleted: There is no current History Entry!", Array.Empty<object>());
			return;
		}
		currentHistoryEntry.m_complete = true;
		this.m_queuedEntriesPrevious.AddHistoryEntry(ref currentHistoryEntry);
		this.LoadNextHistoryEntry();
	}

	// Token: 0x06002CA0 RID: 11424 RVA: 0x000E07A7 File Offset: 0x000DE9A7
	public bool HasHistoryEntry()
	{
		return this.GetCurrentHistoryEntry() != null;
	}

	// Token: 0x06002CA1 RID: 11425 RVA: 0x000E07B4 File Offset: 0x000DE9B4
	public void NotifyDamageChanged(global::Entity entity, int damage)
	{
		if (entity == null)
		{
			return;
		}
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyDamageChanged: There is no current History Entry!", Array.Empty<object>());
			return;
		}
		if (this.IsEntityTheLastCardPlayed(entity))
		{
			global::Entity duplicatedEntity = currentHistoryEntry.m_lastCardPlayed.GetDuplicatedEntity();
			if (duplicatedEntity == null)
			{
				return;
			}
			int damageChangeAmount = damage - duplicatedEntity.GetDamage();
			currentHistoryEntry.m_lastCardPlayed.m_damageChangeAmount = damageChangeAmount;
			return;
		}
		else if (this.IsEntityTheLastAttacker(entity))
		{
			global::Entity duplicatedEntity2 = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity2 == null)
			{
				return;
			}
			int damageChangeAmount2 = damage - duplicatedEntity2.GetDamage();
			currentHistoryEntry.m_lastAttacker.m_damageChangeAmount = damageChangeAmount2;
			return;
		}
		else if (this.IsEntityTheLastDefender(entity))
		{
			global::Entity duplicatedEntity3 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity3 == null)
			{
				return;
			}
			int damageChangeAmount3 = damage - duplicatedEntity3.GetDamage();
			currentHistoryEntry.m_lastDefender.m_damageChangeAmount = damageChangeAmount3;
			return;
		}
		else
		{
			if (!this.IsEntityTheLastCardTargeted(entity))
			{
				int i = 0;
				while (i < currentHistoryEntry.m_affectedCards.Count)
				{
					if (this.IsEntityTheAffectedCard(entity, i))
					{
						global::Entity duplicatedEntity4 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
						if (duplicatedEntity4 == null)
						{
							return;
						}
						int damageChangeAmount4 = damage - duplicatedEntity4.GetDamage();
						currentHistoryEntry.m_affectedCards[i].m_damageChangeAmount = damageChangeAmount4;
						return;
					}
					else
					{
						i++;
					}
				}
				if (this.NotifyEntityAffected(entity, false, false, false, false, false))
				{
					this.NotifyDamageChanged(entity, damage);
				}
				return;
			}
			global::Entity duplicatedEntity5 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity5 == null)
			{
				return;
			}
			int damageChangeAmount5 = damage - duplicatedEntity5.GetDamage();
			currentHistoryEntry.m_lastCardTargeted.m_damageChangeAmount = damageChangeAmount5;
			return;
		}
	}

	// Token: 0x06002CA2 RID: 11426 RVA: 0x000E092C File Offset: 0x000DEB2C
	public void NotifyArmorChanged(global::Entity entity, int newArmor)
	{
		if (entity == null)
		{
			return;
		}
		if (this.m_historyDisabled)
		{
			return;
		}
		if (entity.GetArmor() - newArmor <= 0)
		{
			return;
		}
		if (this.IsEntityTheLastCardPlayed(entity))
		{
			return;
		}
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyArmorChanged: There is no current History Entry!", Array.Empty<object>());
			return;
		}
		if (this.IsEntityTheLastAttacker(entity))
		{
			global::Entity duplicatedEntity = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity == null)
			{
				return;
			}
			int b = duplicatedEntity.GetArmor() - newArmor;
			currentHistoryEntry.m_lastAttacker.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastAttacker.m_armorChangeAmount, b);
			return;
		}
		else if (this.IsEntityTheLastDefender(entity))
		{
			global::Entity duplicatedEntity2 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity2 == null)
			{
				return;
			}
			int b2 = duplicatedEntity2.GetArmor() - newArmor;
			currentHistoryEntry.m_lastDefender.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastDefender.m_armorChangeAmount, b2);
			return;
		}
		else
		{
			if (!this.IsEntityTheLastCardTargeted(entity))
			{
				int i = 0;
				while (i < currentHistoryEntry.m_affectedCards.Count)
				{
					if (this.IsEntityTheAffectedCard(entity, i))
					{
						global::Entity duplicatedEntity3 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
						if (duplicatedEntity3 == null)
						{
							return;
						}
						int b3 = duplicatedEntity3.GetArmor() - newArmor;
						currentHistoryEntry.m_affectedCards[i].m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_affectedCards[i].m_armorChangeAmount, b3);
						return;
					}
					else
					{
						i++;
					}
				}
				if (this.NotifyEntityAffected(entity, false, false, false, false, false))
				{
					this.NotifyArmorChanged(entity, newArmor);
				}
				return;
			}
			global::Entity duplicatedEntity4 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity4 == null)
			{
				return;
			}
			int b4 = duplicatedEntity4.GetArmor() - newArmor;
			currentHistoryEntry.m_lastCardTargeted.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastCardTargeted.m_armorChangeAmount, b4);
			return;
		}
	}

	// Token: 0x06002CA3 RID: 11427 RVA: 0x000E0ACC File Offset: 0x000DECCC
	public void NotifyHealthChanged(global::Entity entity, int health)
	{
		if (entity == null)
		{
			return;
		}
		if (this.m_historyDisabled)
		{
			return;
		}
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyHealthChanged: There is no current History Entry!", Array.Empty<object>());
			return;
		}
		if (this.IsEntityTheLastCardPlayed(entity))
		{
			global::Entity duplicatedEntity = currentHistoryEntry.m_lastCardPlayed.GetDuplicatedEntity();
			if (duplicatedEntity == null)
			{
				return;
			}
			int maxHealthChangeAmount = health - duplicatedEntity.GetHealth();
			currentHistoryEntry.m_lastCardPlayed.m_maxHealthChangeAmount = maxHealthChangeAmount;
			return;
		}
		else if (this.IsEntityTheLastAttacker(entity))
		{
			global::Entity duplicatedEntity2 = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity2 == null)
			{
				return;
			}
			int maxHealthChangeAmount2 = health - duplicatedEntity2.GetHealth();
			currentHistoryEntry.m_lastAttacker.m_maxHealthChangeAmount = maxHealthChangeAmount2;
			return;
		}
		else if (this.IsEntityTheLastDefender(entity))
		{
			global::Entity duplicatedEntity3 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity3 == null)
			{
				return;
			}
			int maxHealthChangeAmount3 = health - duplicatedEntity3.GetHealth();
			currentHistoryEntry.m_lastDefender.m_maxHealthChangeAmount = maxHealthChangeAmount3;
			return;
		}
		else
		{
			if (!this.IsEntityTheLastCardTargeted(entity))
			{
				int i = 0;
				while (i < currentHistoryEntry.m_affectedCards.Count)
				{
					if (this.IsEntityTheAffectedCard(entity, i))
					{
						global::Entity duplicatedEntity4 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
						if (duplicatedEntity4 == null)
						{
							return;
						}
						int maxHealthChangeAmount4 = health - duplicatedEntity4.GetHealth();
						currentHistoryEntry.m_affectedCards[i].m_maxHealthChangeAmount = maxHealthChangeAmount4;
						return;
					}
					else
					{
						i++;
					}
				}
				if (this.NotifyEntityAffected(entity, false, false, false, false, false))
				{
					this.NotifyHealthChanged(entity, health);
				}
				return;
			}
			global::Entity duplicatedEntity5 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity5 == null)
			{
				return;
			}
			int maxHealthChangeAmount5 = health - duplicatedEntity5.GetHealth();
			currentHistoryEntry.m_lastCardTargeted.m_maxHealthChangeAmount = maxHealthChangeAmount5;
			return;
		}
	}

	// Token: 0x06002CA4 RID: 11428 RVA: 0x000E0C44 File Offset: 0x000DEE44
	public void OverrideCurrentHistoryEntryWithMetaData()
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry != null && !currentHistoryEntry.m_usingMetaDataOverride)
		{
			currentHistoryEntry.m_usingMetaDataOverride = true;
			currentHistoryEntry.m_affectedCards.Clear();
		}
	}

	// Token: 0x06002CA5 RID: 11429 RVA: 0x000E0C78 File Offset: 0x000DEE78
	private HistoryInfo GetHistoryInfoForEntity(HistoryManager.TileEntry entry, global::Entity entity)
	{
		if (this.IsEntityTheLastAttacker(entity))
		{
			return entry.m_lastAttacker;
		}
		if (this.IsEntityTheLastDefender(entity))
		{
			return entry.m_lastDefender;
		}
		if (this.IsEntityTheLastCardTargeted(entity))
		{
			return entry.m_lastCardTargeted;
		}
		if (entry.m_lastCardPlayed != null && entity == entry.m_lastCardPlayed.GetOriginalEntity())
		{
			return entry.m_lastCardPlayed;
		}
		for (int i = 0; i < entry.m_affectedCards.Count; i++)
		{
			if (this.IsEntityTheAffectedCard(entry, entity, i))
			{
				return entry.m_affectedCards[i];
			}
		}
		return null;
	}

	// Token: 0x06002CA6 RID: 11430 RVA: 0x000E0D04 File Offset: 0x000DEF04
	public bool NotifyEntityAffected(int entityId, bool allowDuplicates, bool fromMetaData, bool dontDuplicateUntilEnd = false, bool isBurnedCard = false, bool isPoisonous = false)
	{
		global::Entity entity = GameState.Get().GetEntity(entityId);
		return this.NotifyEntityAffected(entity, allowDuplicates, fromMetaData, dontDuplicateUntilEnd, isBurnedCard, isPoisonous);
	}

	// Token: 0x06002CA7 RID: 11431 RVA: 0x000E0D2C File Offset: 0x000DEF2C
	public bool NotifyEntityAffected(global::Entity entity, bool allowDuplicates, bool fromMetaData, bool dontDuplicateUntilEnd = false, bool isBurnedCard = false, bool isPoisonous = false)
	{
		if (entity == null)
		{
			return false;
		}
		if (this.m_historyDisabled)
		{
			return false;
		}
		if (entity.IsEnchantment())
		{
			return false;
		}
		HistoryManager.TileEntry tileEntry = this.GetCurrentHistoryEntry();
		if (tileEntry == null)
		{
			for (int i = 0; i < this.m_queuedEntriesPrevious.Length; i++)
			{
				tileEntry = this.m_queuedEntriesPrevious.GetHistoryEntry(i);
				if (tileEntry == null)
				{
					Log.Power.Print("HistoryManager.NotifyEntityAffected(): There is no current History Entry!", Array.Empty<object>());
					return false;
				}
				if ((fromMetaData || !tileEntry.m_usingMetaDataOverride) && !allowDuplicates)
				{
					HistoryInfo historyInfoForEntity = this.GetHistoryInfoForEntity(tileEntry, entity);
					if (historyInfoForEntity != null)
					{
						if (dontDuplicateUntilEnd)
						{
							historyInfoForEntity.m_dontDuplicateUntilEnd = dontDuplicateUntilEnd;
						}
						if (isBurnedCard)
						{
							historyInfoForEntity.m_isBurnedCard = isBurnedCard;
						}
						if (isPoisonous)
						{
							historyInfoForEntity.m_isPoisonous = isPoisonous;
						}
						return false;
					}
				}
			}
			return false;
		}
		if (!fromMetaData && tileEntry.m_usingMetaDataOverride)
		{
			return false;
		}
		if (!allowDuplicates)
		{
			HistoryInfo historyInfoForEntity2 = this.GetHistoryInfoForEntity(tileEntry, entity);
			if (historyInfoForEntity2 != null)
			{
				if (dontDuplicateUntilEnd)
				{
					historyInfoForEntity2.m_dontDuplicateUntilEnd = dontDuplicateUntilEnd;
				}
				if (isBurnedCard)
				{
					historyInfoForEntity2.m_isBurnedCard = isBurnedCard;
				}
				if (isPoisonous)
				{
					historyInfoForEntity2.m_isPoisonous = isPoisonous;
				}
				return false;
			}
		}
		HistoryInfo historyInfo = new HistoryInfo();
		historyInfo.m_dontDuplicateUntilEnd = dontDuplicateUntilEnd;
		historyInfo.m_isBurnedCard = isBurnedCard;
		historyInfo.m_isPoisonous = isPoisonous;
		historyInfo.SetOriginalEntity(entity);
		tileEntry.m_affectedCards.Add(historyInfo);
		return true;
	}

	// Token: 0x06002CA8 RID: 11432 RVA: 0x000E0E54 File Offset: 0x000DF054
	public void NotifyEntityDied(int entityId)
	{
		global::Entity entity = GameState.Get().GetEntity(entityId);
		this.NotifyEntityDied(entity);
	}

	// Token: 0x06002CA9 RID: 11433 RVA: 0x000E0E74 File Offset: 0x000DF074
	public void NotifyEntityDied(global::Entity entity)
	{
		if (this.m_historyDisabled)
		{
			return;
		}
		if (entity.IsEnchantment())
		{
			return;
		}
		if (this.IsEntityTheLastCardPlayed(entity))
		{
			return;
		}
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (this.IsEntityTheLastAttacker(entity))
		{
			currentHistoryEntry.m_lastAttacker.SetDied(true);
			return;
		}
		if (this.IsEntityTheLastDefender(entity))
		{
			currentHistoryEntry.m_lastDefender.SetDied(true);
			return;
		}
		if (this.IsEntityTheLastCardTargeted(entity))
		{
			currentHistoryEntry.m_lastCardTargeted.SetDied(true);
			return;
		}
		if (currentHistoryEntry != null)
		{
			for (int i = 0; i < currentHistoryEntry.m_affectedCards.Count; i++)
			{
				if (this.IsEntityTheAffectedCard(entity, i))
				{
					currentHistoryEntry.m_affectedCards[i].SetDied(true);
					return;
				}
			}
		}
		if (this.IsDeadInLaterHistoryEntry(entity))
		{
			return;
		}
		if (this.NotifyEntityAffected(entity, false, false, false, false, false))
		{
			this.NotifyEntityDied(entity);
		}
	}

	// Token: 0x06002CAA RID: 11434 RVA: 0x000E0F3C File Offset: 0x000DF13C
	public void NotifyOfInput(float zPosition)
	{
		if (this.m_historyTiles.Count == 0)
		{
			this.CheckForMouseOff();
			return;
		}
		float num = 1000f;
		float num2 = -1000f;
		float num3 = 1000f;
		HistoryCard historyCard = null;
		foreach (HistoryCard historyCard2 in this.m_historyTiles)
		{
			if (historyCard2.HasBeenShown())
			{
				Collider tileCollider = historyCard2.GetTileCollider();
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
					float num6 = Mathf.Abs(zPosition - num4);
					if (num6 < num3)
					{
						num3 = num6;
						historyCard = historyCard2;
					}
					float num7 = Mathf.Abs(zPosition - num5);
					if (num7 < num3)
					{
						num3 = num7;
						historyCard = historyCard2;
					}
				}
			}
		}
		if (zPosition < num || zPosition > num2)
		{
			this.CheckForMouseOff();
			return;
		}
		if (historyCard == null)
		{
			this.CheckForMouseOff();
			return;
		}
		this.m_SoundDucker.StartDucking();
		if (historyCard == this.m_currentlyMousedOverTile)
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
		this.m_currentlyMousedOverTile = historyCard;
		historyCard.NotifyMousedOver();
	}

	// Token: 0x06002CAB RID: 11435 RVA: 0x000E10D8 File Offset: 0x000DF2D8
	public void NotifyOfMouseOff()
	{
		this.CheckForMouseOff();
	}

	// Token: 0x06002CAC RID: 11436 RVA: 0x000E10E0 File Offset: 0x000DF2E0
	public void UpdateLayout()
	{
		if (this.UserIsMousedOverAHistoryTile())
		{
			return;
		}
		float num = 0f;
		Vector3 topTilePosition = this.GetTopTilePosition();
		for (int i = this.m_historyTiles.Count - 1; i >= 0; i--)
		{
			int num2 = 0;
			if (this.m_historyTiles[i].IsHalfSize())
			{
				num2 = 1;
			}
			Collider tileCollider = this.m_historyTiles[i].GetTileCollider();
			float num3 = 0f;
			if (tileCollider != null)
			{
				num3 = tileCollider.bounds.size.z / 2f;
			}
			Vector3 position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num + (float)num2 * num3);
			this.m_historyTiles[i].MarkAsShown();
			iTween.MoveTo(this.m_historyTiles[i].gameObject, position, 1f);
			if (tileCollider != null)
			{
				num += tileCollider.bounds.size.z + 0.15f;
			}
		}
		this.DestroyHistoryTilesThatFallOffTheEnd();
	}

	// Token: 0x06002CAD RID: 11437 RVA: 0x000E11F6 File Offset: 0x000DF3F6
	public int GetNumHistoryTiles()
	{
		return this.m_historyTiles.Count;
	}

	// Token: 0x06002CAE RID: 11438 RVA: 0x000E1204 File Offset: 0x000DF404
	public int GetIndexForTile(HistoryCard tile)
	{
		for (int i = 0; i < this.m_historyTiles.Count; i++)
		{
			if (this.m_historyTiles[i] == tile)
			{
				return i;
			}
		}
		Debug.LogWarning("HistoryManager.GetIndexForTile() - that Tile doesn't exist!");
		return -1;
	}

	// Token: 0x06002CAF RID: 11439 RVA: 0x000E1248 File Offset: 0x000DF448
	public void OnEntityRevealed()
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry != null)
		{
			currentHistoryEntry.DuplicateAllEntities(false, false);
		}
	}

	// Token: 0x06002CB0 RID: 11440 RVA: 0x000E1267 File Offset: 0x000DF467
	private void LoadNextHistoryEntry()
	{
		if (this.m_queuedEntries.Count == 0)
		{
			return;
		}
		if (!this.m_queuedEntries[0].m_complete)
		{
			return;
		}
		base.StartCoroutine(this.LoadNextHistoryEntryWhenLoaded());
	}

	// Token: 0x06002CB1 RID: 11441 RVA: 0x000E1298 File Offset: 0x000DF498
	private IEnumerator LoadNextHistoryEntryWhenLoaded()
	{
		HistoryManager.TileEntry currentEntry = this.m_queuedEntries[0];
		this.m_queuedEntries.RemoveAt(0);
		while (!currentEntry.CanDuplicateAllEntities(true, true))
		{
			yield return null;
		}
		if (currentEntry.GetSourceInfo() != null && currentEntry.GetSourceInfo().GetOriginalEntity() != null && currentEntry.GetSourceInfo().GetOriginalEntity().IsEnchantment())
		{
			this.LoadNextHistoryEntry();
			yield break;
		}
		currentEntry.DuplicateAllEntities(true, true);
		HistoryInfo sourceInfo = currentEntry.GetSourceInfo();
		if (sourceInfo == null || !sourceInfo.HasValidDisplayEntity())
		{
			this.LoadNextHistoryEntry();
			yield break;
		}
		this.CreateTransformTile(sourceInfo);
		List<HistoryInfo> list = new List<HistoryInfo>();
		list.Add(sourceInfo);
		HistoryInfo targetInfo = currentEntry.GetTargetInfo();
		if (targetInfo != null)
		{
			list.Add(targetInfo);
		}
		if (currentEntry.m_affectedCards.Count > 0)
		{
			list.AddRange(currentEntry.m_affectedCards);
		}
		AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", new PrefabCallback<GameObject>(this.TileLoadedCallback), list, AssetLoadingOptions.IgnorePrefabPosition);
		yield break;
	}

	// Token: 0x06002CB2 RID: 11442 RVA: 0x000E12A8 File Offset: 0x000DF4A8
	private void CreateTransformTile(HistoryInfo sourceInfo)
	{
		if (sourceInfo.m_infoType == HistoryInfoType.FATIGUE)
		{
			return;
		}
		if (sourceInfo.m_infoType == HistoryInfoType.BURNED_CARDS)
		{
			return;
		}
		global::Entity duplicatedEntity = sourceInfo.GetDuplicatedEntity();
		global::Entity originalEntity = sourceInfo.GetOriginalEntity();
		if (duplicatedEntity == null || originalEntity == null)
		{
			return;
		}
		int tag = duplicatedEntity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
		if (tag == 0)
		{
			return;
		}
		if (string.IsNullOrEmpty(GameUtils.TranslateDbIdToCardId(tag, false)))
		{
			return;
		}
		global::Entity originalEntity2 = this.CreatePreTransformedEntity(duplicatedEntity);
		HistoryInfo historyInfo = new HistoryInfo();
		historyInfo.SetOriginalEntity(originalEntity2);
		historyInfo.DuplicateEntity(true, true);
		global::Entity originalEntity3 = this.CreatePostTransformedEntity(originalEntity);
		HistoryInfo historyInfo2 = new HistoryInfo();
		historyInfo2.SetOriginalEntity(originalEntity3);
		historyInfo2.DuplicateEntity(true, true);
		List<HistoryInfo> list = new List<HistoryInfo>();
		list.Add(historyInfo);
		list.Add(historyInfo2);
		AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", new PrefabCallback<GameObject>(this.TileLoadedCallback), list, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06002CB3 RID: 11443 RVA: 0x000E137C File Offset: 0x000DF57C
	private IEnumerator WaitForCardLoadedAndDuplicateInfo(HistoryInfo info)
	{
		while (!info.CanDuplicateEntity(false, false))
		{
			yield return null;
		}
		info.DuplicateEntity(false, false);
		yield break;
	}

	// Token: 0x06002CB4 RID: 11444 RVA: 0x000E138C File Offset: 0x000DF58C
	private bool IsEntityTheLastCardTargeted(global::Entity entity)
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastCardTargeted: There is no current History Entry!", Array.Empty<object>());
			return false;
		}
		return currentHistoryEntry.m_lastCardTargeted != null && entity == currentHistoryEntry.m_lastCardTargeted.GetOriginalEntity();
	}

	// Token: 0x06002CB5 RID: 11445 RVA: 0x000E13D4 File Offset: 0x000DF5D4
	private bool IsEntityTheLastAttacker(global::Entity entity)
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastAttacker: There is no current History Entry!", Array.Empty<object>());
			return false;
		}
		return currentHistoryEntry.m_lastAttacker != null && entity == currentHistoryEntry.m_lastAttacker.GetOriginalEntity();
	}

	// Token: 0x06002CB6 RID: 11446 RVA: 0x000E141C File Offset: 0x000DF61C
	private bool IsEntityTheLastCardPlayed(global::Entity entity)
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastCardPlayed: There is no current History Entry!", Array.Empty<object>());
			return false;
		}
		return currentHistoryEntry.m_lastCardPlayed != null && entity == currentHistoryEntry.m_lastCardPlayed.GetOriginalEntity();
	}

	// Token: 0x06002CB7 RID: 11447 RVA: 0x000E1464 File Offset: 0x000DF664
	private bool IsEntityTheLastDefender(global::Entity entity)
	{
		HistoryManager.TileEntry currentHistoryEntry = this.GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastDefender: There is no current History Entry!", Array.Empty<object>());
			return false;
		}
		return currentHistoryEntry.m_lastDefender != null && entity == currentHistoryEntry.m_lastDefender.GetOriginalEntity();
	}

	// Token: 0x06002CB8 RID: 11448 RVA: 0x000E14A9 File Offset: 0x000DF6A9
	private bool IsEntityTheAffectedCard(global::Entity entity, int index)
	{
		return this.IsEntityTheAffectedCard(this.GetCurrentHistoryEntry(), entity, index);
	}

	// Token: 0x06002CB9 RID: 11449 RVA: 0x000E14B9 File Offset: 0x000DF6B9
	private bool IsEntityTheAffectedCard(HistoryManager.TileEntry entry, global::Entity entity, int index)
	{
		if (entry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheAffectedCard: There is no current History Entry!", Array.Empty<object>());
			return false;
		}
		return entry.m_affectedCards[index] != null && entity == entry.m_affectedCards[index].GetOriginalEntity();
	}

	// Token: 0x06002CBA RID: 11450 RVA: 0x000E14F8 File Offset: 0x000DF6F8
	private HistoryManager.TileEntry GetCurrentHistoryEntry()
	{
		if (this.m_queuedEntries.Count == 0)
		{
			return null;
		}
		for (int i = this.m_queuedEntries.Count - 1; i >= 0; i--)
		{
			if (!this.m_queuedEntries[i].m_complete)
			{
				return this.m_queuedEntries[i];
			}
		}
		return null;
	}

	// Token: 0x06002CBB RID: 11451 RVA: 0x000E1550 File Offset: 0x000DF750
	private bool IsDeadInLaterHistoryEntry(global::Entity entity)
	{
		bool result = false;
		for (int i = this.m_queuedEntries.Count - 1; i >= 0; i--)
		{
			HistoryManager.TileEntry tileEntry = this.m_queuedEntries[i];
			if (!tileEntry.m_complete)
			{
				return result;
			}
			for (int j = 0; j < tileEntry.m_affectedCards.Count; j++)
			{
				HistoryInfo historyInfo = tileEntry.m_affectedCards[j];
				if (historyInfo.GetOriginalEntity() == entity && historyInfo.HasDied())
				{
					result = true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002CBC RID: 11452 RVA: 0x000E15C8 File Offset: 0x000DF7C8
	private void TileLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		List<HistoryInfo> list = (List<HistoryInfo>)callbackData;
		HistoryInfo historyInfo = list[0];
		list.RemoveAt(0);
		HistoryTileInitInfo historyTileInitInfo = new HistoryTileInitInfo();
		historyTileInitInfo.m_type = historyInfo.m_infoType;
		historyTileInitInfo.m_childInfos = list;
		if (historyTileInitInfo.m_type == HistoryInfoType.FATIGUE)
		{
			historyTileInitInfo.m_fatigueTexture = this.m_FatigueTexture;
		}
		else if (historyTileInitInfo.m_type == HistoryInfoType.BURNED_CARDS)
		{
			historyTileInitInfo.m_burnedCardsTexture = this.m_BurnedCardsTexture;
		}
		else
		{
			global::Entity duplicatedEntity = historyInfo.GetDuplicatedEntity();
			historyTileInitInfo.m_cardDef = duplicatedEntity.ShareDisposableCardDef();
			historyTileInitInfo.m_entity = duplicatedEntity;
			historyTileInitInfo.m_portraitTexture = this.DeterminePortraitTextureForTiles(duplicatedEntity, historyTileInitInfo.m_cardDef.CardDef);
			historyTileInitInfo.m_portraitGoldenMaterial = historyTileInitInfo.m_cardDef.CardDef.GetPremiumPortraitMaterial();
			historyTileInitInfo.m_fullTileMaterial = historyTileInitInfo.m_cardDef.CardDef.GetHistoryTileFullPortrait();
			historyTileInitInfo.m_halfTileMaterial = historyTileInitInfo.m_cardDef.CardDef.GetHistoryTileHalfPortrait();
			historyTileInitInfo.m_splatAmount = historyInfo.GetSplatAmount();
			historyTileInitInfo.m_dead = historyInfo.HasDied();
			historyTileInitInfo.m_burned = historyInfo.m_isBurnedCard;
			historyTileInitInfo.m_isPoisonous = historyInfo.m_isPoisonous;
		}
		using (historyTileInitInfo.m_cardDef)
		{
			HistoryCard component = go.GetComponent<HistoryCard>();
			this.m_historyTiles.Add(component);
			component.LoadTile(historyTileInitInfo);
			this.SetAsideTileAndTryToUpdate(component);
			this.LoadNextHistoryEntry();
		}
	}

	// Token: 0x06002CBD RID: 11453 RVA: 0x000E1730 File Offset: 0x000DF930
	private Texture DeterminePortraitTextureForTiles(global::Entity entity, CardDef cardDef)
	{
		Texture result;
		if (entity.IsSecret() && entity.IsHidden() && entity.IsControlledByConcealedPlayer())
		{
			if (entity.GetClass() == TAG_CLASS.PALADIN)
			{
				result = this.m_paladinSecretTexture;
			}
			else if (entity.GetClass() == TAG_CLASS.HUNTER)
			{
				result = this.m_hunterSecretTexture;
			}
			else if (entity.GetClass() == TAG_CLASS.ROGUE)
			{
				result = this.m_rogueSecretTexture;
			}
			else if (entity.IsDarkWandererSecret())
			{
				result = this.m_wandererSecretTexture;
			}
			else
			{
				result = this.m_mageSecretTexture;
			}
		}
		else if (entity.GetController() != null && !entity.GetController().IsFriendlySide() && entity.IsObfuscated())
		{
			result = this.m_paladinSecretTexture;
		}
		else
		{
			result = cardDef.GetPortraitTexture();
		}
		return result;
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x000E17D5 File Offset: 0x000DF9D5
	private void CheckForMouseOff()
	{
		if (this.m_currentlyMousedOverTile == null)
		{
			return;
		}
		this.m_currentlyMousedOverTile.NotifyMousedOut();
		this.m_currentlyMousedOverTile = null;
		this.m_SoundDucker.StopDucking();
		this.FadeVignetteOut();
	}

	// Token: 0x06002CBF RID: 11455 RVA: 0x000E180C File Offset: 0x000DFA0C
	private void DestroyHistoryTilesThatFallOffTheEnd()
	{
		if (this.m_historyTiles.Count == 0)
		{
			return;
		}
		float num = 0f;
		float z = base.GetComponent<Collider>().bounds.size.z;
		for (int i = 0; i < this.m_historyTiles.Count; i++)
		{
			num += this.m_historyTiles[i].GetTileSize();
		}
		num += 0.15f * (float)(this.m_historyTiles.Count - 1);
		while (num > z)
		{
			num -= this.m_historyTiles[0].GetTileSize();
			num -= 0.15f;
			UnityEngine.Object.Destroy(this.m_historyTiles[0].gameObject);
			this.m_historyTiles.RemoveAt(0);
		}
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x000E18CC File Offset: 0x000DFACC
	private void SetAsideTileAndTryToUpdate(HistoryCard tile)
	{
		Vector3 topTilePosition = this.GetTopTilePosition();
		tile.transform.position = new Vector3(topTilePosition.x - 20f, topTilePosition.y, topTilePosition.z);
		this.UpdateLayout();
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x000E190E File Offset: 0x000DFB0E
	private Vector3 GetTopTilePosition()
	{
		return new Vector3(base.transform.position.x, base.transform.position.y - 0.15f, base.transform.position.z);
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x000E194C File Offset: 0x000DFB4C
	private bool UserIsMousedOverAHistoryTile()
	{
		if (UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0))
		{
			return false;
		}
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out raycastHit) && raycastHit.transform.GetComponentInChildren<HistoryManager>() == null && raycastHit.transform.GetComponentInChildren<HistoryCard>() == null)
		{
			return false;
		}
		float z = raycastHit.point.z;
		float num = 1000f;
		float num2 = -1000f;
		foreach (HistoryCard historyCard in this.m_historyTiles)
		{
			if (historyCard.HasBeenShown())
			{
				Collider tileCollider = historyCard.GetTileCollider();
				if (!(tileCollider == null))
				{
					float num3 = tileCollider.bounds.center.z - tileCollider.bounds.extents.z;
					float num4 = tileCollider.bounds.center.z + tileCollider.bounds.extents.z;
					if (num3 < num)
					{
						num = num3;
					}
					if (num4 > num2)
					{
						num2 = num4;
					}
				}
			}
		}
		return z >= num && z <= num2;
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x000E1AA8 File Offset: 0x000DFCA8
	private void FadeVignetteIn()
	{
		foreach (HistoryCard historyCard in this.m_historyTiles)
		{
			if (!(historyCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyCard.m_tileActor.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		activeCameraFullScreenEffects.VignettingEnable = true;
		activeCameraFullScreenEffects.DesaturationEnabled = true;
		base.AnimateVignetteIn();
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x000E1B40 File Offset: 0x000DFD40
	private void FadeVignetteOut()
	{
		foreach (HistoryCard historyCard in this.m_historyTiles)
		{
			if (!(historyCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyCard.GetTileCollider().gameObject, GameLayer.Default);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.CardRaycast);
		base.AnimateVignetteOut();
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x000E1BC0 File Offset: 0x000DFDC0
	protected override void OnFullScreenEffectOutFinished()
	{
		if (this.m_animatingDesat || this.m_animatingVignette)
		{
			return;
		}
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Disable();
		foreach (HistoryCard historyCard in this.m_historyTiles)
		{
			if (!(historyCard.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyCard.m_tileActor.gameObject, GameLayer.Default);
			}
		}
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x000E1C4C File Offset: 0x000DFE4C
	public bool IsShowingBigCard()
	{
		return this.m_showingBigCard;
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x000E1C54 File Offset: 0x000DFE54
	public bool HasBigCard()
	{
		return this.m_currentBigCard != null;
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x000E1C62 File Offset: 0x000DFE62
	public HistoryCard GetCurrentBigCard()
	{
		return this.m_currentBigCard;
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x000E1C6A File Offset: 0x000DFE6A
	public global::Entity GetPendingBigCardEntity()
	{
		if (this.m_pendingBigCardEntry == null)
		{
			return null;
		}
		return this.m_pendingBigCardEntry.m_info.GetOriginalEntity();
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x000E1C88 File Offset: 0x000DFE88
	public void CreateFastBigCardFromMetaData(global::Entity entity)
	{
		int displayTimeMS = 1000;
		this.CreatePlayedBigCard(entity, delegate
		{
		}, delegate
		{
		}, true, false, displayTimeMS);
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x000E1CE4 File Offset: 0x000DFEE4
	public void CreatePlayedBigCard(global::Entity entity, HistoryManager.BigCardStartedCallback startedCallback, HistoryManager.BigCardFinishedCallback finishedCallback, bool fromMetaData, bool countered, int displayTimeMS)
	{
		if (!GameState.Get().GetGameEntity().ShouldShowBigCard())
		{
			finishedCallback();
			return;
		}
		this.m_showingBigCard = true;
		base.StopCoroutine("WaitForCardLoadedAndCreateBigCard");
		HistoryManager.BigCardEntry bigCardEntry = new HistoryManager.BigCardEntry();
		bigCardEntry.m_info = new HistoryInfo();
		bigCardEntry.m_info.SetOriginalEntity(entity);
		if (entity.IsWeapon())
		{
			bigCardEntry.m_info.m_infoType = HistoryInfoType.WEAPON_PLAYED;
		}
		else
		{
			bigCardEntry.m_info.m_infoType = HistoryInfoType.CARD_PLAYED;
		}
		bigCardEntry.m_startedCallback = startedCallback;
		bigCardEntry.m_finishedCallback = finishedCallback;
		bigCardEntry.m_fromMetaData = fromMetaData;
		bigCardEntry.m_countered = countered;
		bigCardEntry.m_displayTimeMS = displayTimeMS;
		base.StartCoroutine("WaitForCardLoadedAndCreateBigCard", bigCardEntry);
	}

	// Token: 0x06002CCC RID: 11468 RVA: 0x000E1D90 File Offset: 0x000DFF90
	public void CreateTriggeredBigCard(global::Entity entity, HistoryManager.BigCardStartedCallback startedCallback, HistoryManager.BigCardFinishedCallback finishedCallback, bool fromMetaData, bool isSecret)
	{
		if (!GameState.Get().GetGameEntity().ShouldShowBigCard())
		{
			finishedCallback();
			return;
		}
		this.m_showingBigCard = true;
		base.StopCoroutine("WaitForCardLoadedAndCreateBigCard");
		HistoryManager.BigCardEntry bigCardEntry = new HistoryManager.BigCardEntry();
		bigCardEntry.m_info = new HistoryInfo();
		bigCardEntry.m_info.SetOriginalEntity(entity);
		bigCardEntry.m_info.m_infoType = HistoryInfoType.TRIGGER;
		bigCardEntry.m_fromMetaData = fromMetaData;
		bigCardEntry.m_startedCallback = startedCallback;
		bigCardEntry.m_finishedCallback = finishedCallback;
		bigCardEntry.m_waitForSecretSpell = isSecret;
		base.StartCoroutine("WaitForCardLoadedAndCreateBigCard", bigCardEntry);
	}

	// Token: 0x06002CCD RID: 11469 RVA: 0x000E1E1B File Offset: 0x000E001B
	public void NotifyOfSecretSpellFinished()
	{
		this.m_bigCardWaitingForSecret = false;
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x000E1E24 File Offset: 0x000E0024
	public void HandleClickOnBigCard(HistoryCard card)
	{
		if (this.m_currentBigCard != null && this.m_currentBigCard == card)
		{
			this.OnCurrentBigCardClicked();
		}
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x000E1E48 File Offset: 0x000E0048
	public string GetBigCardBoneName()
	{
		string text = "BigCardPosition";
		if (UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		return text;
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x000E1E74 File Offset: 0x000E0074
	private IEnumerator WaitForBoardLoadedAndSetPaths()
	{
		while (ZoneMgr.Get() == null)
		{
			yield return null;
		}
		while (Gameplay.Get().GetBoardLayout() == null)
		{
			yield return null;
		}
		Transform transform = Board.Get().FindBone("BigCardPathPoint");
		if (transform == null)
		{
			yield break;
		}
		this.m_bigCardPath = new Vector3[3];
		this.m_bigCardPath[1] = transform.position;
		this.m_bigCardPath[2] = this.GetBigCardPosition();
		yield break;
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x000E1E83 File Offset: 0x000E0083
	private Vector3 GetBigCardPosition()
	{
		return Board.Get().FindBone(this.GetBigCardBoneName()).position;
	}

	// Token: 0x06002CD2 RID: 11474 RVA: 0x000E1E9A File Offset: 0x000E009A
	private IEnumerator WaitForCardLoadedAndCreateBigCard(HistoryManager.BigCardEntry bigCardEntry)
	{
		this.m_pendingBigCardEntry = bigCardEntry;
		HistoryInfo info = bigCardEntry.m_info;
		while (!info.CanDuplicateEntity(false, false))
		{
			yield return null;
		}
		bigCardEntry.m_startedCallback();
		info.DuplicateEntity(false, false);
		this.m_pendingBigCardEntry = null;
		AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", new PrefabCallback<GameObject>(this.BigCardLoadedCallback), bigCardEntry, AssetLoadingOptions.IgnorePrefabPosition);
		yield break;
	}

	// Token: 0x06002CD3 RID: 11475 RVA: 0x000E1EB0 File Offset: 0x000E00B0
	private void BigCardLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		HistoryManager.BigCardEntry bigCardEntry = (HistoryManager.BigCardEntry)callbackData;
		global::Entity entity = bigCardEntry.m_info.GetDuplicatedEntity();
		Card card = entity.GetCard();
		DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef();
		if (entity.GetCardType() == TAG_CARDTYPE.SPELL || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER || bigCardEntry.m_fromMetaData)
		{
			go.transform.position = card.transform.position;
			go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		else
		{
			go.transform.position = this.GetBigCardPosition();
		}
		global::Entity entity2 = this.CreatePreTransformedEntity(entity);
		global::Entity postTransformedEntity = null;
		if (entity2 != null)
		{
			postTransformedEntity = entity;
			entity = entity2;
			card = entity.GetCard();
			if (disposableCardDef != null)
			{
				disposableCardDef.Dispose();
			}
			disposableCardDef = card.ShareDisposableCardDef();
		}
		global::Entity entity3 = this.CreateSecretDeathrattleEntity(entity);
		if (entity3 != null)
		{
			entity = entity3;
			card = entity.GetCard();
			if (disposableCardDef != null)
			{
				disposableCardDef.Dispose();
			}
			disposableCardDef = card.ShareDisposableCardDef();
		}
		using (disposableCardDef)
		{
			HistoryBigCardInitInfo historyBigCardInitInfo = new HistoryBigCardInitInfo();
			historyBigCardInitInfo.m_historyInfoType = bigCardEntry.m_info.m_infoType;
			historyBigCardInitInfo.m_entity = entity;
			historyBigCardInitInfo.m_portraitTexture = disposableCardDef.CardDef.GetPortraitTexture();
			historyBigCardInitInfo.m_portraitGoldenMaterial = disposableCardDef.CardDef.GetPremiumPortraitMaterial();
			historyBigCardInitInfo.m_cardDef = disposableCardDef;
			historyBigCardInitInfo.m_finishedCallback = bigCardEntry.m_finishedCallback;
			historyBigCardInitInfo.m_countered = bigCardEntry.m_countered;
			historyBigCardInitInfo.m_waitForSecretSpell = bigCardEntry.m_waitForSecretSpell;
			historyBigCardInitInfo.m_fromMetaData = bigCardEntry.m_fromMetaData;
			historyBigCardInitInfo.m_postTransformedEntity = postTransformedEntity;
			historyBigCardInitInfo.m_displayTimeMS = bigCardEntry.m_displayTimeMS;
			HistoryCard component = go.GetComponent<HistoryCard>();
			component.LoadBigCard(historyBigCardInitInfo);
			if (this.m_currentBigCard)
			{
				this.InterruptCurrentBigCard();
			}
			this.m_currentBigCard = component;
			base.StartCoroutine("WaitThenShowBigCard");
		}
	}

	// Token: 0x06002CD4 RID: 11476 RVA: 0x000E2088 File Offset: 0x000E0288
	private IEnumerator WaitThenShowBigCard()
	{
		if (this.m_currentBigCard.IsBigCardWaitingForSecret())
		{
			this.m_bigCardWaitingForSecret = true;
			this.m_currentBigCard.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			while (this.m_bigCardWaitingForSecret)
			{
				yield return null;
			}
			if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
			{
				this.m_bigCardTransformState = HistoryManager.BigCardTransformState.PRE_TRANSFORM;
			}
			this.m_currentBigCard.ShowBigCard(this.m_bigCardPath);
			base.StartCoroutine("WaitThenDestroyBigCard");
			if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
			{
				while (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.PRE_TRANSFORM || this.m_bigCardTransformState == HistoryManager.BigCardTransformState.TRANSFORM)
				{
					yield return null;
				}
			}
		}
		else if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			this.m_bigCardTransformState = HistoryManager.BigCardTransformState.PRE_TRANSFORM;
			this.m_currentBigCard.ShowBigCard(this.m_bigCardPath);
			base.StartCoroutine("WaitThenDestroyBigCard");
			while (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.PRE_TRANSFORM || this.m_bigCardTransformState == HistoryManager.BigCardTransformState.TRANSFORM)
			{
				yield return null;
			}
		}
		else
		{
			this.m_currentBigCard.ShowBigCard(this.m_bigCardPath);
			base.StartCoroutine("WaitThenDestroyBigCard");
		}
		global::Entity entity = this.m_currentBigCard.GetEntity();
		if (entity.HasSubCards())
		{
			PowerTaskList historyBlockingTaskList = GameState.Get().GetPowerProcessor().GetHistoryBlockingTaskList();
			Network.HistBlockStart histBlockStart = (historyBlockingTaskList != null) ? historyBlockingTaskList.GetBlockStart() : null;
			if (histBlockStart.SubOption != -1)
			{
				Card card = entity.GetCard();
				ChoiceCardMgr.Get().ShowSubOptions(card);
				base.StartCoroutine(this.FinishSpectatorSubOption(entity, histBlockStart.SubOption));
			}
		}
		yield return new WaitForSeconds(1f);
		this.m_currentBigCard.RunBigCardFinishedCallback();
		yield break;
	}

	// Token: 0x06002CD5 RID: 11477 RVA: 0x000E2097 File Offset: 0x000E0297
	private IEnumerator FinishSpectatorSubOption(global::Entity mainEntity, int chosenSubOption)
	{
		while (ChoiceCardMgr.Get().IsWaitingToShowSubOptions())
		{
			yield return null;
			if (ChoiceCardMgr.Get() == null || !ChoiceCardMgr.Get().HasSubOption())
			{
				yield break;
			}
		}
		List<Card> friendlyCards = ChoiceCardMgr.Get().GetFriendlyCards();
		List<Card> choiceCards;
		if (friendlyCards == null)
		{
			Log.All.PrintError("actualChoiceCards is NULL. Attempting workaround.", Array.Empty<object>());
			choiceCards = new List<Card>();
		}
		else
		{
			choiceCards = new List<Card>(friendlyCards);
		}
		Card subCard = (chosenSubOption < choiceCards.Count) ? choiceCards[chosenSubOption] : null;
		global::Entity subEntity = subCard ? subCard.GetEntity() : null;
		if (subCard != null)
		{
			subCard.SetInputEnabled(false);
		}
		yield return new WaitForSeconds(1f);
		if (subCard != null)
		{
			subCard.SetInputEnabled(true);
		}
		GameState gameState = GameState.Get();
		if (gameState == null || gameState.IsGameOver())
		{
			foreach (Card card in choiceCards)
			{
				card.HideCard();
			}
			yield break;
		}
		InputManager.Get().HandleClickOnSubOption(subEntity, true);
		yield break;
	}

	// Token: 0x06002CD6 RID: 11478 RVA: 0x000E20A6 File Offset: 0x000E02A6
	private IEnumerator WaitThenDestroyBigCard()
	{
		float num = (float)this.m_currentBigCard.GetDisplayTimeMS() / 1000f;
		if (num <= 0f)
		{
			if (this.m_currentBigCard.IsBigCardFromMetaData())
			{
				num = 1.5f;
			}
			else
			{
				if (this.m_currentBigCard.GetEntity() != null)
				{
					TAG_CARDTYPE cardType = this.m_currentBigCard.GetEntity().GetCardType();
					if (cardType == TAG_CARDTYPE.SPELL)
					{
						num = 4f + GameState.Get().GetGameEntity().GetAdditionalTimeToWaitForSpells();
					}
					else if (cardType == TAG_CARDTYPE.HERO_POWER)
					{
						num = 4f + GameState.Get().GetGameEntity().GetAdditionalTimeToWaitForSpells();
					}
					else
					{
						num = 3f;
					}
				}
				else
				{
					num = 4f;
				}
				if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
				{
					num *= 0.5f;
				}
			}
		}
		yield return new WaitForSeconds(num);
		this.DestroyBigCard();
		yield break;
	}

	// Token: 0x06002CD7 RID: 11479 RVA: 0x000E20B8 File Offset: 0x000E02B8
	private void DestroyBigCard()
	{
		if (this.m_currentBigCard == null)
		{
			return;
		}
		if (this.m_currentBigCard.m_mainCardActor == null)
		{
			this.RunFinishedCallbackAndDestroyBigCard();
			return;
		}
		if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			this.PlayBigCardTransformEffects();
			return;
		}
		if (this.m_currentBigCard.WasBigCardCountered())
		{
			this.PlayBigCardCounteredEffects();
			return;
		}
		this.RunFinishedCallbackAndDestroyBigCard();
	}

	// Token: 0x06002CD8 RID: 11480 RVA: 0x000E211C File Offset: 0x000E031C
	private void RunFinishedCallbackAndDestroyBigCard()
	{
		if (this.m_currentBigCard == null)
		{
			return;
		}
		this.m_currentBigCard.RunBigCardFinishedCallback();
		this.m_showingBigCard = false;
		UnityEngine.Object.Destroy(this.m_currentBigCard.gameObject);
	}

	// Token: 0x06002CD9 RID: 11481 RVA: 0x000E2150 File Offset: 0x000E0350
	private void PlayBigCardCounteredEffects()
	{
		Spell.StateFinishedCallback callback = delegate(Spell s, SpellStateType prevStateType, object userData)
		{
			if (s.GetActiveState() != SpellStateType.NONE)
			{
				return;
			}
			Component component = (HistoryCard)userData;
			this.m_showingBigCard = false;
			UnityEngine.Object.Destroy(component.gameObject);
		};
		Spell spell = this.m_currentBigCard.m_mainCardActor.GetSpell(SpellType.DEATH);
		if (spell == null)
		{
			this.RunFinishedCallbackAndDestroyBigCard();
			return;
		}
		spell.AddStateFinishedCallback(callback, this.m_currentBigCard);
		this.m_currentBigCard.RunBigCardFinishedCallback();
		this.m_currentBigCard = null;
		spell.Activate();
	}

	// Token: 0x06002CDA RID: 11482 RVA: 0x000E21B1 File Offset: 0x000E03B1
	private void PlayBigCardTransformEffects()
	{
		base.StartCoroutine("PlayBigCardTransformEffectsWithTiming");
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x000E21BF File Offset: 0x000E03BF
	private IEnumerator PlayBigCardTransformEffectsWithTiming()
	{
		if (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.INVALID)
		{
			this.RunFinishedCallbackAndDestroyBigCard();
			yield break;
		}
		if (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.PRE_TRANSFORM)
		{
			this.m_bigCardTransformState = HistoryManager.BigCardTransformState.TRANSFORM;
			yield return base.StartCoroutine(this.PlayBigCardTransformSpell());
		}
		if (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.TRANSFORM)
		{
			this.m_bigCardTransformState = HistoryManager.BigCardTransformState.POST_TRANSFORM;
			yield return base.StartCoroutine(this.WaitForBigCardPostTransform());
		}
		if (this.m_bigCardTransformState == HistoryManager.BigCardTransformState.POST_TRANSFORM)
		{
			this.m_bigCardTransformState = HistoryManager.BigCardTransformState.INVALID;
			this.RunFinishedCallbackAndDestroyBigCard();
		}
		yield break;
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x000E21CE File Offset: 0x000E03CE
	private IEnumerator PlayBigCardTransformSpell()
	{
		if (this.m_TransformSpells == null || this.m_TransformSpells.Length == 0)
		{
			yield break;
		}
		global::Entity entity = this.m_currentBigCard.GetEntity();
		int num = entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD_VISUAL_TYPE);
		if (num < 0 || num >= this.m_TransformSpells.Length)
		{
			num = 0;
		}
		this.m_bigCardTransformSpell = UnityEngine.Object.Instantiate<Spell>(this.m_TransformSpells[num]);
		if (this.m_bigCardTransformSpell == null)
		{
			yield break;
		}
		Card card = entity.GetCard();
		this.m_bigCardTransformSpell.SetSource(card.gameObject);
		this.m_bigCardTransformSpell.AddTarget(card.gameObject);
		this.m_bigCardTransformSpell.m_SetParentToLocation = true;
		this.m_bigCardTransformSpell.UpdateTransform();
		this.m_bigCardTransformSpell.SetPosition(this.m_currentBigCard.m_mainCardActor.transform.position);
		Spell.StateFinishedCallback callback = delegate(Spell s, SpellStateType prevStateType, object userData)
		{
			if (s.GetActiveState() != SpellStateType.NONE)
			{
				return;
			}
			UnityEngine.Object.Destroy(s.gameObject);
		};
		this.m_bigCardTransformSpell.AddStateFinishedCallback(callback);
		this.m_bigCardTransformSpell.Activate();
		while (this.m_bigCardTransformSpell && !this.m_bigCardTransformSpell.IsFinished())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x000E21DD File Offset: 0x000E03DD
	private IEnumerator WaitForBigCardPostTransform()
	{
		Actor mainCardActor = this.m_currentBigCard.m_mainCardActor;
		mainCardActor.Hide(true);
		this.m_currentBigCard.LoadBigCardPostTransformedEntity();
		TransformUtil.CopyLocal(this.m_currentBigCard.m_mainCardActor, mainCardActor);
		yield return new WaitForSeconds(2f);
		yield break;
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x000E21EC File Offset: 0x000E03EC
	private void OnCurrentBigCardClicked()
	{
		if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			this.ForceNextBigCardTransformState();
			return;
		}
		this.InterruptCurrentBigCard();
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x000E2208 File Offset: 0x000E0408
	private void ForceNextBigCardTransformState()
	{
		switch (this.m_bigCardTransformState)
		{
		case HistoryManager.BigCardTransformState.PRE_TRANSFORM:
			this.m_bigCardTransformState = HistoryManager.BigCardTransformState.TRANSFORM;
			this.StopWaitingThenDestroyBigCard();
			return;
		case HistoryManager.BigCardTransformState.TRANSFORM:
			if (this.m_bigCardTransformSpell)
			{
				UnityEngine.Object.Destroy(this.m_bigCardTransformSpell.gameObject);
				return;
			}
			break;
		case HistoryManager.BigCardTransformState.POST_TRANSFORM:
			this.InterruptCurrentBigCard();
			break;
		default:
			return;
		}
	}

	// Token: 0x06002CE0 RID: 11488 RVA: 0x000E2263 File Offset: 0x000E0463
	private void StopWaitingThenDestroyBigCard()
	{
		base.StopCoroutine("WaitThenDestroyBigCard");
		this.DestroyBigCard();
	}

	// Token: 0x06002CE1 RID: 11489 RVA: 0x000E2276 File Offset: 0x000E0476
	private void InterruptCurrentBigCard()
	{
		base.StopCoroutine("WaitThenShowBigCard");
		if (this.m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			this.CutoffBigCardTransformEffects();
			return;
		}
		this.StopWaitingThenDestroyBigCard();
	}

	// Token: 0x06002CE2 RID: 11490 RVA: 0x000E229D File Offset: 0x000E049D
	private void CutoffBigCardTransformEffects()
	{
		if (this.m_bigCardTransformSpell)
		{
			UnityEngine.Object.Destroy(this.m_bigCardTransformSpell.gameObject);
		}
		base.StopCoroutine("PlayBigCardTransformEffectsWithTiming");
		this.m_bigCardTransformState = HistoryManager.BigCardTransformState.INVALID;
		this.RunFinishedCallbackAndDestroyBigCard();
	}

	// Token: 0x040018B5 RID: 6325
	public Texture m_mageSecretTexture;

	// Token: 0x040018B6 RID: 6326
	public Texture m_paladinSecretTexture;

	// Token: 0x040018B7 RID: 6327
	public Texture m_hunterSecretTexture;

	// Token: 0x040018B8 RID: 6328
	public Texture m_rogueSecretTexture;

	// Token: 0x040018B9 RID: 6329
	public Texture m_wandererSecretTexture;

	// Token: 0x040018BA RID: 6330
	public Texture m_FatigueTexture;

	// Token: 0x040018BB RID: 6331
	public Texture m_BurnedCardsTexture;

	// Token: 0x040018BC RID: 6332
	public Spell[] m_TransformSpells;

	// Token: 0x040018BD RID: 6333
	private const float BIG_CARD_POWER_PROCESSOR_DELAY_TIME = 1f;

	// Token: 0x040018BE RID: 6334
	private const float BIG_CARD_SPELL_DISPLAY_TIME = 4f;

	// Token: 0x040018BF RID: 6335
	private const float BIG_CARD_MINION_DISPLAY_TIME = 3f;

	// Token: 0x040018C0 RID: 6336
	private const float BIG_CARD_HERO_POWER_DISPLAY_TIME = 4f;

	// Token: 0x040018C1 RID: 6337
	private const float BIG_CARD_SECRET_DISPLAY_TIME = 4f;

	// Token: 0x040018C2 RID: 6338
	private const float BIG_CARD_POST_TRANSFORM_DISPLAY_TIME = 2f;

	// Token: 0x040018C3 RID: 6339
	private const float BIG_CARD_META_DATA_DEFAULT_DISPLAY_TIME = 1.5f;

	// Token: 0x040018C4 RID: 6340
	private const float BIG_CARD_META_DATA_FAST_DISPLAY_TIME = 1f;

	// Token: 0x040018C5 RID: 6341
	private const float SPACE_BETWEEN_TILES = 0.15f;

	// Token: 0x040018C6 RID: 6342
	private static HistoryManager s_instance;

	// Token: 0x040018C7 RID: 6343
	private bool m_historyDisabled;

	// Token: 0x040018C8 RID: 6344
	private List<HistoryCard> m_historyTiles = new List<HistoryCard>();

	// Token: 0x040018C9 RID: 6345
	private HistoryCard m_currentlyMousedOverTile;

	// Token: 0x040018CA RID: 6346
	private List<HistoryManager.TileEntry> m_queuedEntries = new List<HistoryManager.TileEntry>();

	// Token: 0x040018CB RID: 6347
	private HistoryManager.TileEntryBuffer m_queuedEntriesPrevious = new HistoryManager.TileEntryBuffer();

	// Token: 0x040018CC RID: 6348
	private Vector3[] m_bigCardPath;

	// Token: 0x040018CD RID: 6349
	private HistoryManager.BigCardEntry m_pendingBigCardEntry;

	// Token: 0x040018CE RID: 6350
	private HistoryCard m_currentBigCard;

	// Token: 0x040018CF RID: 6351
	private bool m_showingBigCard;

	// Token: 0x040018D0 RID: 6352
	private bool m_bigCardWaitingForSecret;

	// Token: 0x040018D1 RID: 6353
	private HistoryManager.BigCardTransformState m_bigCardTransformState;

	// Token: 0x040018D2 RID: 6354
	private Spell m_bigCardTransformSpell;

	// Token: 0x0200168C RID: 5772
	// (Invoke) Token: 0x0600E46E RID: 58478
	public delegate void BigCardStartedCallback();

	// Token: 0x0200168D RID: 5773
	// (Invoke) Token: 0x0600E472 RID: 58482
	public delegate void BigCardFinishedCallback();

	// Token: 0x0200168E RID: 5774
	private class BigCardEntry
	{
		// Token: 0x0400B0EB RID: 45291
		public HistoryInfo m_info;

		// Token: 0x0400B0EC RID: 45292
		public HistoryManager.BigCardStartedCallback m_startedCallback;

		// Token: 0x0400B0ED RID: 45293
		public HistoryManager.BigCardFinishedCallback m_finishedCallback;

		// Token: 0x0400B0EE RID: 45294
		public bool m_fromMetaData;

		// Token: 0x0400B0EF RID: 45295
		public bool m_countered;

		// Token: 0x0400B0F0 RID: 45296
		public bool m_waitForSecretSpell;

		// Token: 0x0400B0F1 RID: 45297
		public int m_displayTimeMS;
	}

	// Token: 0x0200168F RID: 5775
	private enum BigCardTransformState
	{
		// Token: 0x0400B0F3 RID: 45299
		INVALID,
		// Token: 0x0400B0F4 RID: 45300
		PRE_TRANSFORM,
		// Token: 0x0400B0F5 RID: 45301
		TRANSFORM,
		// Token: 0x0400B0F6 RID: 45302
		POST_TRANSFORM
	}

	// Token: 0x02001690 RID: 5776
	private class TileEntry
	{
		// Token: 0x0600E476 RID: 58486 RVA: 0x0040627B File Offset: 0x0040447B
		public void SetAttacker(global::Entity attacker)
		{
			this.m_lastAttacker = new HistoryInfo();
			this.m_lastAttacker.m_infoType = HistoryInfoType.ATTACK;
			this.m_lastAttacker.SetOriginalEntity(attacker);
		}

		// Token: 0x0600E477 RID: 58487 RVA: 0x004062A0 File Offset: 0x004044A0
		public void SetDefender(global::Entity defender)
		{
			this.m_lastDefender = new HistoryInfo();
			this.m_lastDefender.SetOriginalEntity(defender);
		}

		// Token: 0x0600E478 RID: 58488 RVA: 0x004062B9 File Offset: 0x004044B9
		public void SetCardPlayed(global::Entity entity)
		{
			this.m_lastCardPlayed = new HistoryInfo();
			if (entity.IsWeapon())
			{
				this.m_lastCardPlayed.m_infoType = HistoryInfoType.WEAPON_PLAYED;
			}
			else
			{
				this.m_lastCardPlayed.m_infoType = HistoryInfoType.CARD_PLAYED;
			}
			this.m_lastCardPlayed.SetOriginalEntity(entity);
		}

		// Token: 0x0600E479 RID: 58489 RVA: 0x004062F4 File Offset: 0x004044F4
		public void SetCardTargeted(global::Entity entity)
		{
			if (entity == null)
			{
				return;
			}
			this.m_lastCardTargeted = new HistoryInfo();
			this.m_lastCardTargeted.SetOriginalEntity(entity);
		}

		// Token: 0x0600E47A RID: 58490 RVA: 0x00406311 File Offset: 0x00404511
		public void SetCardTriggered(global::Entity entity)
		{
			if (entity.IsGame())
			{
				return;
			}
			if (entity.IsPlayer())
			{
				return;
			}
			this.m_lastCardTriggered = new HistoryInfo();
			this.m_lastCardTriggered.m_infoType = HistoryInfoType.TRIGGER;
			this.m_lastCardTriggered.SetOriginalEntity(entity);
		}

		// Token: 0x0600E47B RID: 58491 RVA: 0x00406348 File Offset: 0x00404548
		public void SetFatigue()
		{
			this.m_fatigueInfo = new HistoryInfo();
			this.m_fatigueInfo.m_infoType = HistoryInfoType.FATIGUE;
		}

		// Token: 0x0600E47C RID: 58492 RVA: 0x00406361 File Offset: 0x00404561
		public void SetBurnedCards()
		{
			this.m_burnedCardsInfo = new HistoryInfo();
			this.m_burnedCardsInfo.m_infoType = HistoryInfoType.BURNED_CARDS;
		}

		// Token: 0x0600E47D RID: 58493 RVA: 0x0040637C File Offset: 0x0040457C
		public bool CanDuplicateAllEntities(bool duplicateHiddenNonSecrets, bool isEndOfHistory = false)
		{
			HistoryInfo sourceInfo = this.GetSourceInfo();
			if (this.ShouldDuplicateEntity(sourceInfo) && !sourceInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
			{
				return false;
			}
			HistoryInfo targetInfo = this.GetTargetInfo();
			if (this.ShouldDuplicateEntity(targetInfo) && !targetInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
			{
				return false;
			}
			for (int i = 0; i < this.m_affectedCards.Count; i++)
			{
				HistoryInfo historyInfo = this.m_affectedCards[i];
				if (this.ShouldDuplicateEntity(historyInfo) && !historyInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600E47E RID: 58494 RVA: 0x004063FC File Offset: 0x004045FC
		public void DuplicateAllEntities(bool duplicateHiddenNonSecrets, bool isEndOfHistory = false)
		{
			HistoryInfo sourceInfo = this.GetSourceInfo();
			if (this.ShouldDuplicateEntity(sourceInfo))
			{
				sourceInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
			}
			HistoryInfo targetInfo = this.GetTargetInfo();
			if (this.ShouldDuplicateEntity(targetInfo))
			{
				targetInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
			}
			for (int i = 0; i < this.m_affectedCards.Count; i++)
			{
				HistoryInfo historyInfo = this.m_affectedCards[i];
				if (this.ShouldDuplicateEntity(historyInfo))
				{
					historyInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
				}
			}
		}

		// Token: 0x0600E47F RID: 58495 RVA: 0x0040646D File Offset: 0x0040466D
		public bool ShouldDuplicateEntity(HistoryInfo info)
		{
			return info != null && info != this.m_fatigueInfo && info != this.m_burnedCardsInfo;
		}

		// Token: 0x0600E480 RID: 58496 RVA: 0x0040648C File Offset: 0x0040468C
		public HistoryInfo GetSourceInfo()
		{
			if (this.m_lastCardPlayed != null)
			{
				return this.m_lastCardPlayed;
			}
			if (this.m_lastAttacker != null)
			{
				return this.m_lastAttacker;
			}
			if (this.m_lastCardTriggered != null)
			{
				return this.m_lastCardTriggered;
			}
			if (this.m_fatigueInfo != null)
			{
				return this.m_fatigueInfo;
			}
			if (this.m_burnedCardsInfo != null)
			{
				return this.m_burnedCardsInfo;
			}
			Debug.LogError("HistoryEntry.GetSourceInfo() - no source info");
			return null;
		}

		// Token: 0x0600E481 RID: 58497 RVA: 0x004064EF File Offset: 0x004046EF
		public HistoryInfo GetTargetInfo()
		{
			if (this.m_lastCardPlayed != null && this.m_lastCardTargeted != null)
			{
				return this.m_lastCardTargeted;
			}
			if (this.m_lastAttacker != null && this.m_lastDefender != null)
			{
				return this.m_lastDefender;
			}
			return null;
		}

		// Token: 0x0400B0F7 RID: 45303
		public HistoryInfo m_lastAttacker;

		// Token: 0x0400B0F8 RID: 45304
		public HistoryInfo m_lastDefender;

		// Token: 0x0400B0F9 RID: 45305
		public HistoryInfo m_lastCardPlayed;

		// Token: 0x0400B0FA RID: 45306
		public HistoryInfo m_lastCardTriggered;

		// Token: 0x0400B0FB RID: 45307
		public HistoryInfo m_lastCardTargeted;

		// Token: 0x0400B0FC RID: 45308
		public List<HistoryInfo> m_affectedCards = new List<HistoryInfo>();

		// Token: 0x0400B0FD RID: 45309
		public HistoryInfo m_fatigueInfo;

		// Token: 0x0400B0FE RID: 45310
		public HistoryInfo m_burnedCardsInfo;

		// Token: 0x0400B0FF RID: 45311
		public bool m_usingMetaDataOverride;

		// Token: 0x0400B100 RID: 45312
		public bool m_complete;
	}

	// Token: 0x02001691 RID: 5777
	private class TileEntryBuffer
	{
		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x0600E483 RID: 58499 RVA: 0x00406533 File Offset: 0x00404733
		public int Length
		{
			get
			{
				return this.m_queuedEntriesBuffer.Length;
			}
		}

		// Token: 0x0600E484 RID: 58500 RVA: 0x00406540 File Offset: 0x00404740
		public void Clear()
		{
			for (int i = 0; i < 5; i++)
			{
				this.m_queuedEntriesBuffer[i] = null;
			}
		}

		// Token: 0x0600E485 RID: 58501 RVA: 0x00406562 File Offset: 0x00404762
		public void AddHistoryEntry(ref HistoryManager.TileEntry newEntry)
		{
			this.m_queuedEntriesBuffer[this.m_queuedEntriesBufferIndex] = newEntry;
			this.m_queuedEntriesBufferIndex++;
			this.m_queuedEntriesBufferIndex %= 5;
		}

		// Token: 0x0600E486 RID: 58502 RVA: 0x00406590 File Offset: 0x00404790
		public HistoryManager.TileEntry GetHistoryEntry(int index)
		{
			int num = this.m_queuedEntriesBufferIndex - 1 - index;
			num %= 5;
			if (num < 0)
			{
				num += 5;
			}
			return this.m_queuedEntriesBuffer[num];
		}

		// Token: 0x0400B101 RID: 45313
		private const int MAX_PREVIOUS_TILE_ENTRIES = 5;

		// Token: 0x0400B102 RID: 45314
		private int m_queuedEntriesBufferIndex;

		// Token: 0x0400B103 RID: 45315
		private HistoryManager.TileEntry[] m_queuedEntriesBuffer = new HistoryManager.TileEntry[5];
	}
}
