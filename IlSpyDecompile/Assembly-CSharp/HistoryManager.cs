using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class HistoryManager : CardTileListDisplay
{
	public delegate void BigCardStartedCallback();

	public delegate void BigCardFinishedCallback();

	private class BigCardEntry
	{
		public HistoryInfo m_info;

		public BigCardStartedCallback m_startedCallback;

		public BigCardFinishedCallback m_finishedCallback;

		public bool m_fromMetaData;

		public bool m_countered;

		public bool m_waitForSecretSpell;

		public int m_displayTimeMS;
	}

	private enum BigCardTransformState
	{
		INVALID,
		PRE_TRANSFORM,
		TRANSFORM,
		POST_TRANSFORM
	}

	private class TileEntry
	{
		public HistoryInfo m_lastAttacker;

		public HistoryInfo m_lastDefender;

		public HistoryInfo m_lastCardPlayed;

		public HistoryInfo m_lastCardTriggered;

		public HistoryInfo m_lastCardTargeted;

		public List<HistoryInfo> m_affectedCards = new List<HistoryInfo>();

		public HistoryInfo m_fatigueInfo;

		public HistoryInfo m_burnedCardsInfo;

		public bool m_usingMetaDataOverride;

		public bool m_complete;

		public void SetAttacker(Entity attacker)
		{
			m_lastAttacker = new HistoryInfo();
			m_lastAttacker.m_infoType = HistoryInfoType.ATTACK;
			m_lastAttacker.SetOriginalEntity(attacker);
		}

		public void SetDefender(Entity defender)
		{
			m_lastDefender = new HistoryInfo();
			m_lastDefender.SetOriginalEntity(defender);
		}

		public void SetCardPlayed(Entity entity)
		{
			m_lastCardPlayed = new HistoryInfo();
			if (entity.IsWeapon())
			{
				m_lastCardPlayed.m_infoType = HistoryInfoType.WEAPON_PLAYED;
			}
			else
			{
				m_lastCardPlayed.m_infoType = HistoryInfoType.CARD_PLAYED;
			}
			m_lastCardPlayed.SetOriginalEntity(entity);
		}

		public void SetCardTargeted(Entity entity)
		{
			if (entity != null)
			{
				m_lastCardTargeted = new HistoryInfo();
				m_lastCardTargeted.SetOriginalEntity(entity);
			}
		}

		public void SetCardTriggered(Entity entity)
		{
			if (!entity.IsGame() && !entity.IsPlayer())
			{
				m_lastCardTriggered = new HistoryInfo();
				m_lastCardTriggered.m_infoType = HistoryInfoType.TRIGGER;
				m_lastCardTriggered.SetOriginalEntity(entity);
			}
		}

		public void SetFatigue()
		{
			m_fatigueInfo = new HistoryInfo();
			m_fatigueInfo.m_infoType = HistoryInfoType.FATIGUE;
		}

		public void SetBurnedCards()
		{
			m_burnedCardsInfo = new HistoryInfo();
			m_burnedCardsInfo.m_infoType = HistoryInfoType.BURNED_CARDS;
		}

		public bool CanDuplicateAllEntities(bool duplicateHiddenNonSecrets, bool isEndOfHistory = false)
		{
			HistoryInfo sourceInfo = GetSourceInfo();
			if (ShouldDuplicateEntity(sourceInfo) && !sourceInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
			{
				return false;
			}
			HistoryInfo targetInfo = GetTargetInfo();
			if (ShouldDuplicateEntity(targetInfo) && !targetInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
			{
				return false;
			}
			for (int i = 0; i < m_affectedCards.Count; i++)
			{
				HistoryInfo historyInfo = m_affectedCards[i];
				if (ShouldDuplicateEntity(historyInfo) && !historyInfo.CanDuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory))
				{
					return false;
				}
			}
			return true;
		}

		public void DuplicateAllEntities(bool duplicateHiddenNonSecrets, bool isEndOfHistory = false)
		{
			HistoryInfo sourceInfo = GetSourceInfo();
			if (ShouldDuplicateEntity(sourceInfo))
			{
				sourceInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
			}
			HistoryInfo targetInfo = GetTargetInfo();
			if (ShouldDuplicateEntity(targetInfo))
			{
				targetInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
			}
			for (int i = 0; i < m_affectedCards.Count; i++)
			{
				HistoryInfo historyInfo = m_affectedCards[i];
				if (ShouldDuplicateEntity(historyInfo))
				{
					historyInfo.DuplicateEntity(duplicateHiddenNonSecrets, isEndOfHistory);
				}
			}
		}

		public bool ShouldDuplicateEntity(HistoryInfo info)
		{
			if (info == null)
			{
				return false;
			}
			if (info == m_fatigueInfo)
			{
				return false;
			}
			if (info == m_burnedCardsInfo)
			{
				return false;
			}
			return true;
		}

		public HistoryInfo GetSourceInfo()
		{
			if (m_lastCardPlayed != null)
			{
				return m_lastCardPlayed;
			}
			if (m_lastAttacker != null)
			{
				return m_lastAttacker;
			}
			if (m_lastCardTriggered != null)
			{
				return m_lastCardTriggered;
			}
			if (m_fatigueInfo != null)
			{
				return m_fatigueInfo;
			}
			if (m_burnedCardsInfo != null)
			{
				return m_burnedCardsInfo;
			}
			Debug.LogError("HistoryEntry.GetSourceInfo() - no source info");
			return null;
		}

		public HistoryInfo GetTargetInfo()
		{
			if (m_lastCardPlayed != null && m_lastCardTargeted != null)
			{
				return m_lastCardTargeted;
			}
			if (m_lastAttacker != null && m_lastDefender != null)
			{
				return m_lastDefender;
			}
			return null;
		}
	}

	private class TileEntryBuffer
	{
		private const int MAX_PREVIOUS_TILE_ENTRIES = 5;

		private int m_queuedEntriesBufferIndex;

		private TileEntry[] m_queuedEntriesBuffer = new TileEntry[5];

		public int Length => m_queuedEntriesBuffer.Length;

		public void Clear()
		{
			for (int i = 0; i < 5; i++)
			{
				m_queuedEntriesBuffer[i] = null;
			}
		}

		public void AddHistoryEntry(ref TileEntry newEntry)
		{
			m_queuedEntriesBuffer[m_queuedEntriesBufferIndex] = newEntry;
			m_queuedEntriesBufferIndex++;
			m_queuedEntriesBufferIndex %= 5;
		}

		public TileEntry GetHistoryEntry(int index)
		{
			int num = m_queuedEntriesBufferIndex - 1 - index;
			num %= 5;
			if (num < 0)
			{
				num += 5;
			}
			return m_queuedEntriesBuffer[num];
		}
	}

	public Texture m_mageSecretTexture;

	public Texture m_paladinSecretTexture;

	public Texture m_hunterSecretTexture;

	public Texture m_rogueSecretTexture;

	public Texture m_wandererSecretTexture;

	public Texture m_FatigueTexture;

	public Texture m_BurnedCardsTexture;

	public Spell[] m_TransformSpells;

	private const float BIG_CARD_POWER_PROCESSOR_DELAY_TIME = 1f;

	private const float BIG_CARD_SPELL_DISPLAY_TIME = 4f;

	private const float BIG_CARD_MINION_DISPLAY_TIME = 3f;

	private const float BIG_CARD_HERO_POWER_DISPLAY_TIME = 4f;

	private const float BIG_CARD_SECRET_DISPLAY_TIME = 4f;

	private const float BIG_CARD_POST_TRANSFORM_DISPLAY_TIME = 2f;

	private const float BIG_CARD_META_DATA_DEFAULT_DISPLAY_TIME = 1.5f;

	private const float BIG_CARD_META_DATA_FAST_DISPLAY_TIME = 1f;

	private const float SPACE_BETWEEN_TILES = 0.15f;

	private static HistoryManager s_instance;

	private bool m_historyDisabled;

	private List<HistoryCard> m_historyTiles = new List<HistoryCard>();

	private HistoryCard m_currentlyMousedOverTile;

	private List<TileEntry> m_queuedEntries = new List<TileEntry>();

	private TileEntryBuffer m_queuedEntriesPrevious = new TileEntryBuffer();

	private Vector3[] m_bigCardPath;

	private BigCardEntry m_pendingBigCardEntry;

	private HistoryCard m_currentBigCard;

	private bool m_showingBigCard;

	private bool m_bigCardWaitingForSecret;

	private BigCardTransformState m_bigCardTransformState;

	private Spell m_bigCardTransformSpell;

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.15f, base.transform.position.z);
		m_queuedEntriesPrevious.Clear();
	}

	protected override void OnDestroy()
	{
		s_instance = null;
		base.OnDestroy();
	}

	protected override void Start()
	{
		base.Start();
		StartCoroutine(WaitForBoardLoadedAndSetPaths());
	}

	public static HistoryManager Get()
	{
		return s_instance;
	}

	public bool IsHistoryEnabled()
	{
		return !m_historyDisabled;
	}

	public void DisableHistory()
	{
		m_historyDisabled = true;
		GetComponent<Collider>().enabled = false;
	}

	public void EnableHistory()
	{
		m_historyDisabled = false;
		GetComponent<Collider>().enabled = true;
	}

	private Entity CreatePreTransformedEntity(Entity entity)
	{
		int num = entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
		if (num == 0)
		{
			return null;
		}
		string text = GameUtils.TranslateDbIdToCardId(num);
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		Entity entity2 = new Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(text);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	private Entity CreatePostTransformedEntity(Entity entity)
	{
		string cardId = entity.GetCardId();
		if (string.IsNullOrEmpty(cardId))
		{
			return null;
		}
		Entity entity2 = new Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(cardId);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	private Entity CreateSecretDeathrattleEntity(Entity entity)
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
		Entity entity2 = new Entity();
		EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
		entity2.InitCard();
		entity2.ReplaceTags(entityDef.GetTags());
		entity2.LoadCard(text);
		entity2.SetTag(GAME_TAG.CONTROLLER, entity.GetControllerId());
		entity2.SetTag(GAME_TAG.ZONE, TAG_ZONE.HAND);
		entity2.SetTag(GAME_TAG.PREMIUM, entity.GetPremiumType());
		entity2.SetTag(GAME_TAG.WATERMARK_OVERRIDE_CARD_SET, entity.GetWatermarkCardSetOverride());
		return entity2;
	}

	public void CreatePlayedTile(Entity playedEntity, Entity targetedEntity)
	{
		if (!m_historyDisabled)
		{
			TileEntry tileEntry = new TileEntry();
			m_queuedEntries.Add(tileEntry);
			tileEntry.SetCardPlayed(playedEntity);
			tileEntry.SetCardTargeted(targetedEntity);
			if (tileEntry.m_lastCardPlayed.GetDuplicatedEntity() == null)
			{
				StartCoroutine("WaitForCardLoadedAndDuplicateInfo", tileEntry.m_lastCardPlayed);
			}
		}
	}

	public void CreateTriggerTile(Entity triggeredEntity)
	{
		if (!m_historyDisabled)
		{
			TileEntry tileEntry = new TileEntry();
			m_queuedEntries.Add(tileEntry);
			tileEntry.SetCardTriggered(triggeredEntity);
		}
	}

	public void CreateAttackTile(Entity attacker, Entity defender, PowerTaskList taskList)
	{
		if (m_historyDisabled)
		{
			return;
		}
		TileEntry tileEntry = new TileEntry();
		m_queuedEntries.Add(tileEntry);
		tileEntry.SetAttacker(attacker);
		tileEntry.SetDefender(defender);
		Entity duplicatedEntity = tileEntry.m_lastAttacker.GetDuplicatedEntity();
		Entity duplicatedEntity2 = tileEntry.m_lastDefender.GetDuplicatedEntity();
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

	public void CreateFatigueTile()
	{
		if (!m_historyDisabled)
		{
			TileEntry tileEntry = new TileEntry();
			m_queuedEntries.Add(tileEntry);
			tileEntry.SetFatigue();
		}
	}

	public void CreateBurnedCardsTile()
	{
		if (!m_historyDisabled)
		{
			TileEntry tileEntry = new TileEntry();
			m_queuedEntries.Add(tileEntry);
			tileEntry.SetBurnedCards();
		}
	}

	public void MarkCurrentHistoryEntryAsCompleted()
	{
		if (!m_historyDisabled)
		{
			TileEntry newEntry = GetCurrentHistoryEntry();
			if (newEntry == null)
			{
				Log.Power.Print("HistoryManager.MarkCurrentHistoryEntryAsCompleted: There is no current History Entry!");
				return;
			}
			newEntry.m_complete = true;
			m_queuedEntriesPrevious.AddHistoryEntry(ref newEntry);
			LoadNextHistoryEntry();
		}
	}

	public bool HasHistoryEntry()
	{
		return GetCurrentHistoryEntry() != null;
	}

	public void NotifyDamageChanged(Entity entity, int damage)
	{
		if (entity == null || m_historyDisabled)
		{
			return;
		}
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyDamageChanged: There is no current History Entry!");
			return;
		}
		if (IsEntityTheLastCardPlayed(entity))
		{
			Entity duplicatedEntity = currentHistoryEntry.m_lastCardPlayed.GetDuplicatedEntity();
			if (duplicatedEntity != null)
			{
				int damageChangeAmount = damage - duplicatedEntity.GetDamage();
				currentHistoryEntry.m_lastCardPlayed.m_damageChangeAmount = damageChangeAmount;
			}
			return;
		}
		if (IsEntityTheLastAttacker(entity))
		{
			Entity duplicatedEntity2 = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity2 != null)
			{
				int damageChangeAmount2 = damage - duplicatedEntity2.GetDamage();
				currentHistoryEntry.m_lastAttacker.m_damageChangeAmount = damageChangeAmount2;
			}
			return;
		}
		if (IsEntityTheLastDefender(entity))
		{
			Entity duplicatedEntity3 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity3 != null)
			{
				int damageChangeAmount3 = damage - duplicatedEntity3.GetDamage();
				currentHistoryEntry.m_lastDefender.m_damageChangeAmount = damageChangeAmount3;
			}
			return;
		}
		if (IsEntityTheLastCardTargeted(entity))
		{
			Entity duplicatedEntity4 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity4 != null)
			{
				int damageChangeAmount4 = damage - duplicatedEntity4.GetDamage();
				currentHistoryEntry.m_lastCardTargeted.m_damageChangeAmount = damageChangeAmount4;
			}
			return;
		}
		for (int i = 0; i < currentHistoryEntry.m_affectedCards.Count; i++)
		{
			if (IsEntityTheAffectedCard(entity, i))
			{
				Entity duplicatedEntity5 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
				if (duplicatedEntity5 != null)
				{
					int damageChangeAmount5 = damage - duplicatedEntity5.GetDamage();
					currentHistoryEntry.m_affectedCards[i].m_damageChangeAmount = damageChangeAmount5;
				}
				return;
			}
		}
		if (NotifyEntityAffected(entity, allowDuplicates: false, fromMetaData: false))
		{
			NotifyDamageChanged(entity, damage);
		}
	}

	public void NotifyArmorChanged(Entity entity, int newArmor)
	{
		if (entity == null || m_historyDisabled || entity.GetArmor() - newArmor <= 0 || IsEntityTheLastCardPlayed(entity))
		{
			return;
		}
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyArmorChanged: There is no current History Entry!");
			return;
		}
		if (IsEntityTheLastAttacker(entity))
		{
			Entity duplicatedEntity = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity != null)
			{
				int b = duplicatedEntity.GetArmor() - newArmor;
				currentHistoryEntry.m_lastAttacker.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastAttacker.m_armorChangeAmount, b);
			}
			return;
		}
		if (IsEntityTheLastDefender(entity))
		{
			Entity duplicatedEntity2 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity2 != null)
			{
				int b2 = duplicatedEntity2.GetArmor() - newArmor;
				currentHistoryEntry.m_lastDefender.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastDefender.m_armorChangeAmount, b2);
			}
			return;
		}
		if (IsEntityTheLastCardTargeted(entity))
		{
			Entity duplicatedEntity3 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity3 != null)
			{
				int b3 = duplicatedEntity3.GetArmor() - newArmor;
				currentHistoryEntry.m_lastCardTargeted.m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_lastCardTargeted.m_armorChangeAmount, b3);
			}
			return;
		}
		for (int i = 0; i < currentHistoryEntry.m_affectedCards.Count; i++)
		{
			if (IsEntityTheAffectedCard(entity, i))
			{
				Entity duplicatedEntity4 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
				if (duplicatedEntity4 != null)
				{
					int b4 = duplicatedEntity4.GetArmor() - newArmor;
					currentHistoryEntry.m_affectedCards[i].m_armorChangeAmount = Mathf.Max(currentHistoryEntry.m_affectedCards[i].m_armorChangeAmount, b4);
				}
				return;
			}
		}
		if (NotifyEntityAffected(entity, allowDuplicates: false, fromMetaData: false))
		{
			NotifyArmorChanged(entity, newArmor);
		}
	}

	public void NotifyHealthChanged(Entity entity, int health)
	{
		if (entity == null || m_historyDisabled)
		{
			return;
		}
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.NotifyHealthChanged: There is no current History Entry!");
			return;
		}
		if (IsEntityTheLastCardPlayed(entity))
		{
			Entity duplicatedEntity = currentHistoryEntry.m_lastCardPlayed.GetDuplicatedEntity();
			if (duplicatedEntity != null)
			{
				int maxHealthChangeAmount = health - duplicatedEntity.GetHealth();
				currentHistoryEntry.m_lastCardPlayed.m_maxHealthChangeAmount = maxHealthChangeAmount;
			}
			return;
		}
		if (IsEntityTheLastAttacker(entity))
		{
			Entity duplicatedEntity2 = currentHistoryEntry.m_lastAttacker.GetDuplicatedEntity();
			if (duplicatedEntity2 != null)
			{
				int maxHealthChangeAmount2 = health - duplicatedEntity2.GetHealth();
				currentHistoryEntry.m_lastAttacker.m_maxHealthChangeAmount = maxHealthChangeAmount2;
			}
			return;
		}
		if (IsEntityTheLastDefender(entity))
		{
			Entity duplicatedEntity3 = currentHistoryEntry.m_lastDefender.GetDuplicatedEntity();
			if (duplicatedEntity3 != null)
			{
				int maxHealthChangeAmount3 = health - duplicatedEntity3.GetHealth();
				currentHistoryEntry.m_lastDefender.m_maxHealthChangeAmount = maxHealthChangeAmount3;
			}
			return;
		}
		if (IsEntityTheLastCardTargeted(entity))
		{
			Entity duplicatedEntity4 = currentHistoryEntry.m_lastCardTargeted.GetDuplicatedEntity();
			if (duplicatedEntity4 != null)
			{
				int maxHealthChangeAmount4 = health - duplicatedEntity4.GetHealth();
				currentHistoryEntry.m_lastCardTargeted.m_maxHealthChangeAmount = maxHealthChangeAmount4;
			}
			return;
		}
		for (int i = 0; i < currentHistoryEntry.m_affectedCards.Count; i++)
		{
			if (IsEntityTheAffectedCard(entity, i))
			{
				Entity duplicatedEntity5 = currentHistoryEntry.m_affectedCards[i].GetDuplicatedEntity();
				if (duplicatedEntity5 != null)
				{
					int maxHealthChangeAmount5 = health - duplicatedEntity5.GetHealth();
					currentHistoryEntry.m_affectedCards[i].m_maxHealthChangeAmount = maxHealthChangeAmount5;
				}
				return;
			}
		}
		if (NotifyEntityAffected(entity, allowDuplicates: false, fromMetaData: false))
		{
			NotifyHealthChanged(entity, health);
		}
	}

	public void OverrideCurrentHistoryEntryWithMetaData()
	{
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry != null && !currentHistoryEntry.m_usingMetaDataOverride)
		{
			currentHistoryEntry.m_usingMetaDataOverride = true;
			currentHistoryEntry.m_affectedCards.Clear();
		}
	}

	private HistoryInfo GetHistoryInfoForEntity(TileEntry entry, Entity entity)
	{
		if (IsEntityTheLastAttacker(entity))
		{
			return entry.m_lastAttacker;
		}
		if (IsEntityTheLastDefender(entity))
		{
			return entry.m_lastDefender;
		}
		if (IsEntityTheLastCardTargeted(entity))
		{
			return entry.m_lastCardTargeted;
		}
		if (entry.m_lastCardPlayed != null && entity == entry.m_lastCardPlayed.GetOriginalEntity())
		{
			return entry.m_lastCardPlayed;
		}
		for (int i = 0; i < entry.m_affectedCards.Count; i++)
		{
			if (IsEntityTheAffectedCard(entry, entity, i))
			{
				return entry.m_affectedCards[i];
			}
		}
		return null;
	}

	public bool NotifyEntityAffected(int entityId, bool allowDuplicates, bool fromMetaData, bool dontDuplicateUntilEnd = false, bool isBurnedCard = false, bool isPoisonous = false)
	{
		Entity entity = GameState.Get().GetEntity(entityId);
		return NotifyEntityAffected(entity, allowDuplicates, fromMetaData, dontDuplicateUntilEnd, isBurnedCard, isPoisonous);
	}

	public bool NotifyEntityAffected(Entity entity, bool allowDuplicates, bool fromMetaData, bool dontDuplicateUntilEnd = false, bool isBurnedCard = false, bool isPoisonous = false)
	{
		if (entity == null)
		{
			return false;
		}
		if (m_historyDisabled)
		{
			return false;
		}
		if (entity.IsEnchantment())
		{
			return false;
		}
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry != null)
		{
			if (!fromMetaData && currentHistoryEntry.m_usingMetaDataOverride)
			{
				return false;
			}
			if (!allowDuplicates)
			{
				HistoryInfo historyInfoForEntity = GetHistoryInfoForEntity(currentHistoryEntry, entity);
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
			HistoryInfo historyInfo = new HistoryInfo();
			historyInfo.m_dontDuplicateUntilEnd = dontDuplicateUntilEnd;
			historyInfo.m_isBurnedCard = isBurnedCard;
			historyInfo.m_isPoisonous = isPoisonous;
			historyInfo.SetOriginalEntity(entity);
			currentHistoryEntry.m_affectedCards.Add(historyInfo);
			return true;
		}
		for (int i = 0; i < m_queuedEntriesPrevious.Length; i++)
		{
			currentHistoryEntry = m_queuedEntriesPrevious.GetHistoryEntry(i);
			if (currentHistoryEntry == null)
			{
				Log.Power.Print("HistoryManager.NotifyEntityAffected(): There is no current History Entry!");
				return false;
			}
			if ((!fromMetaData && currentHistoryEntry.m_usingMetaDataOverride) || allowDuplicates)
			{
				continue;
			}
			HistoryInfo historyInfoForEntity2 = GetHistoryInfoForEntity(currentHistoryEntry, entity);
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
		return false;
	}

	public void NotifyEntityDied(int entityId)
	{
		Entity entity = GameState.Get().GetEntity(entityId);
		NotifyEntityDied(entity);
	}

	public void NotifyEntityDied(Entity entity)
	{
		if (m_historyDisabled || entity.IsEnchantment() || IsEntityTheLastCardPlayed(entity))
		{
			return;
		}
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (IsEntityTheLastAttacker(entity))
		{
			currentHistoryEntry.m_lastAttacker.SetDied(set: true);
			return;
		}
		if (IsEntityTheLastDefender(entity))
		{
			currentHistoryEntry.m_lastDefender.SetDied(set: true);
			return;
		}
		if (IsEntityTheLastCardTargeted(entity))
		{
			currentHistoryEntry.m_lastCardTargeted.SetDied(set: true);
			return;
		}
		if (currentHistoryEntry != null)
		{
			for (int i = 0; i < currentHistoryEntry.m_affectedCards.Count; i++)
			{
				if (IsEntityTheAffectedCard(entity, i))
				{
					currentHistoryEntry.m_affectedCards[i].SetDied(set: true);
					return;
				}
			}
		}
		if (!IsDeadInLaterHistoryEntry(entity) && NotifyEntityAffected(entity, allowDuplicates: false, fromMetaData: false))
		{
			NotifyEntityDied(entity);
		}
	}

	public void NotifyOfInput(float zPosition)
	{
		if (m_historyTiles.Count == 0)
		{
			CheckForMouseOff();
			return;
		}
		float num = 1000f;
		float num2 = -1000f;
		float num3 = 1000f;
		HistoryCard historyCard = null;
		foreach (HistoryCard historyTile in m_historyTiles)
		{
			if (!historyTile.HasBeenShown())
			{
				continue;
			}
			Collider tileCollider = historyTile.GetTileCollider();
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
					historyCard = historyTile;
				}
				float num7 = Mathf.Abs(zPosition - num5);
				if (num7 < num3)
				{
					num3 = num7;
					historyCard = historyTile;
				}
			}
		}
		if (zPosition < num || zPosition > num2)
		{
			CheckForMouseOff();
			return;
		}
		if (historyCard == null)
		{
			CheckForMouseOff();
			return;
		}
		m_SoundDucker.StartDucking();
		if (!(historyCard == m_currentlyMousedOverTile))
		{
			if (m_currentlyMousedOverTile != null)
			{
				m_currentlyMousedOverTile.NotifyMousedOut();
			}
			else
			{
				FadeVignetteIn();
			}
			m_currentlyMousedOverTile = historyCard;
			historyCard.NotifyMousedOver();
		}
	}

	public void NotifyOfMouseOff()
	{
		CheckForMouseOff();
	}

	public void UpdateLayout()
	{
		if (UserIsMousedOverAHistoryTile())
		{
			return;
		}
		float num = 0f;
		Vector3 topTilePosition = GetTopTilePosition();
		for (int num2 = m_historyTiles.Count - 1; num2 >= 0; num2--)
		{
			int num3 = 0;
			if (m_historyTiles[num2].IsHalfSize())
			{
				num3 = 1;
			}
			Collider tileCollider = m_historyTiles[num2].GetTileCollider();
			float num4 = 0f;
			if (tileCollider != null)
			{
				num4 = tileCollider.bounds.size.z / 2f;
			}
			Vector3 position = new Vector3(topTilePosition.x, topTilePosition.y, topTilePosition.z - num + (float)num3 * num4);
			m_historyTiles[num2].MarkAsShown();
			iTween.MoveTo(m_historyTiles[num2].gameObject, position, 1f);
			if (tileCollider != null)
			{
				num += tileCollider.bounds.size.z + 0.15f;
			}
		}
		DestroyHistoryTilesThatFallOffTheEnd();
	}

	public int GetNumHistoryTiles()
	{
		return m_historyTiles.Count;
	}

	public int GetIndexForTile(HistoryCard tile)
	{
		for (int i = 0; i < m_historyTiles.Count; i++)
		{
			if (m_historyTiles[i] == tile)
			{
				return i;
			}
		}
		Debug.LogWarning("HistoryManager.GetIndexForTile() - that Tile doesn't exist!");
		return -1;
	}

	public void OnEntityRevealed()
	{
		GetCurrentHistoryEntry()?.DuplicateAllEntities(duplicateHiddenNonSecrets: false);
	}

	private void LoadNextHistoryEntry()
	{
		if (m_queuedEntries.Count != 0 && m_queuedEntries[0].m_complete)
		{
			StartCoroutine(LoadNextHistoryEntryWhenLoaded());
		}
	}

	private IEnumerator LoadNextHistoryEntryWhenLoaded()
	{
		TileEntry currentEntry = m_queuedEntries[0];
		m_queuedEntries.RemoveAt(0);
		while (!currentEntry.CanDuplicateAllEntities(duplicateHiddenNonSecrets: true, isEndOfHistory: true))
		{
			yield return null;
		}
		if (currentEntry.GetSourceInfo() != null && currentEntry.GetSourceInfo().GetOriginalEntity() != null && currentEntry.GetSourceInfo().GetOriginalEntity().IsEnchantment())
		{
			LoadNextHistoryEntry();
			yield break;
		}
		currentEntry.DuplicateAllEntities(duplicateHiddenNonSecrets: true, isEndOfHistory: true);
		HistoryInfo sourceInfo = currentEntry.GetSourceInfo();
		if (sourceInfo == null || !sourceInfo.HasValidDisplayEntity())
		{
			LoadNextHistoryEntry();
			yield break;
		}
		CreateTransformTile(sourceInfo);
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
		AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", TileLoadedCallback, list, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void CreateTransformTile(HistoryInfo sourceInfo)
	{
		if (sourceInfo.m_infoType == HistoryInfoType.FATIGUE || sourceInfo.m_infoType == HistoryInfoType.BURNED_CARDS)
		{
			return;
		}
		Entity duplicatedEntity = sourceInfo.GetDuplicatedEntity();
		Entity originalEntity = sourceInfo.GetOriginalEntity();
		if (duplicatedEntity != null && originalEntity != null)
		{
			int num = duplicatedEntity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD);
			if (num != 0 && !string.IsNullOrEmpty(GameUtils.TranslateDbIdToCardId(num)))
			{
				Entity originalEntity2 = CreatePreTransformedEntity(duplicatedEntity);
				HistoryInfo historyInfo = new HistoryInfo();
				historyInfo.SetOriginalEntity(originalEntity2);
				historyInfo.DuplicateEntity(duplicateHiddenNonSecret: true, isEndOfHistory: true);
				Entity originalEntity3 = CreatePostTransformedEntity(originalEntity);
				HistoryInfo historyInfo2 = new HistoryInfo();
				historyInfo2.SetOriginalEntity(originalEntity3);
				historyInfo2.DuplicateEntity(duplicateHiddenNonSecret: true, isEndOfHistory: true);
				List<HistoryInfo> list = new List<HistoryInfo>();
				list.Add(historyInfo);
				list.Add(historyInfo2);
				AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", TileLoadedCallback, list, AssetLoadingOptions.IgnorePrefabPosition);
			}
		}
	}

	private IEnumerator WaitForCardLoadedAndDuplicateInfo(HistoryInfo info)
	{
		while (!info.CanDuplicateEntity(duplicateHiddenNonSecret: false))
		{
			yield return null;
		}
		info.DuplicateEntity(duplicateHiddenNonSecret: false, isEndOfHistory: false);
	}

	private bool IsEntityTheLastCardTargeted(Entity entity)
	{
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastCardTargeted: There is no current History Entry!");
			return false;
		}
		if (currentHistoryEntry.m_lastCardTargeted != null)
		{
			return entity == currentHistoryEntry.m_lastCardTargeted.GetOriginalEntity();
		}
		return false;
	}

	private bool IsEntityTheLastAttacker(Entity entity)
	{
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastAttacker: There is no current History Entry!");
			return false;
		}
		if (currentHistoryEntry.m_lastAttacker != null)
		{
			return entity == currentHistoryEntry.m_lastAttacker.GetOriginalEntity();
		}
		return false;
	}

	private bool IsEntityTheLastCardPlayed(Entity entity)
	{
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastCardPlayed: There is no current History Entry!");
			return false;
		}
		if (currentHistoryEntry.m_lastCardPlayed != null)
		{
			return entity == currentHistoryEntry.m_lastCardPlayed.GetOriginalEntity();
		}
		return false;
	}

	private bool IsEntityTheLastDefender(Entity entity)
	{
		TileEntry currentHistoryEntry = GetCurrentHistoryEntry();
		if (currentHistoryEntry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheLastDefender: There is no current History Entry!");
			return false;
		}
		if (currentHistoryEntry.m_lastDefender != null)
		{
			return entity == currentHistoryEntry.m_lastDefender.GetOriginalEntity();
		}
		return false;
	}

	private bool IsEntityTheAffectedCard(Entity entity, int index)
	{
		return IsEntityTheAffectedCard(GetCurrentHistoryEntry(), entity, index);
	}

	private bool IsEntityTheAffectedCard(TileEntry entry, Entity entity, int index)
	{
		if (entry == null)
		{
			Log.Power.Print("HistoryManager.IsEntityTheAffectedCard: There is no current History Entry!");
			return false;
		}
		if (entry.m_affectedCards[index] != null)
		{
			return entity == entry.m_affectedCards[index].GetOriginalEntity();
		}
		return false;
	}

	private TileEntry GetCurrentHistoryEntry()
	{
		if (m_queuedEntries.Count == 0)
		{
			return null;
		}
		for (int num = m_queuedEntries.Count - 1; num >= 0; num--)
		{
			if (!m_queuedEntries[num].m_complete)
			{
				return m_queuedEntries[num];
			}
		}
		return null;
	}

	private bool IsDeadInLaterHistoryEntry(Entity entity)
	{
		bool result = false;
		for (int num = m_queuedEntries.Count - 1; num >= 0; num--)
		{
			TileEntry tileEntry = m_queuedEntries[num];
			if (!tileEntry.m_complete)
			{
				return result;
			}
			for (int i = 0; i < tileEntry.m_affectedCards.Count; i++)
			{
				HistoryInfo historyInfo = tileEntry.m_affectedCards[i];
				if (historyInfo.GetOriginalEntity() == entity && historyInfo.HasDied())
				{
					result = true;
				}
			}
		}
		return false;
	}

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
			historyTileInitInfo.m_fatigueTexture = m_FatigueTexture;
		}
		else if (historyTileInitInfo.m_type == HistoryInfoType.BURNED_CARDS)
		{
			historyTileInitInfo.m_burnedCardsTexture = m_BurnedCardsTexture;
		}
		else
		{
			Entity duplicatedEntity = historyInfo.GetDuplicatedEntity();
			historyTileInitInfo.m_cardDef = duplicatedEntity.ShareDisposableCardDef();
			historyTileInitInfo.m_entity = duplicatedEntity;
			historyTileInitInfo.m_portraitTexture = DeterminePortraitTextureForTiles(duplicatedEntity, historyTileInitInfo.m_cardDef.CardDef);
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
			m_historyTiles.Add(component);
			component.LoadTile(historyTileInitInfo);
			SetAsideTileAndTryToUpdate(component);
			LoadNextHistoryEntry();
		}
	}

	private Texture DeterminePortraitTextureForTiles(Entity entity, CardDef cardDef)
	{
		Texture texture = null;
		if (entity.IsSecret() && entity.IsHidden() && entity.IsControlledByConcealedPlayer())
		{
			if (entity.GetClass() == TAG_CLASS.PALADIN)
			{
				return m_paladinSecretTexture;
			}
			if (entity.GetClass() == TAG_CLASS.HUNTER)
			{
				return m_hunterSecretTexture;
			}
			if (entity.GetClass() == TAG_CLASS.ROGUE)
			{
				return m_rogueSecretTexture;
			}
			if (entity.IsDarkWandererSecret())
			{
				return m_wandererSecretTexture;
			}
			return m_mageSecretTexture;
		}
		if (entity.GetController() != null && !entity.GetController().IsFriendlySide() && entity.IsObfuscated())
		{
			return m_paladinSecretTexture;
		}
		return cardDef.GetPortraitTexture();
	}

	private void CheckForMouseOff()
	{
		if (!(m_currentlyMousedOverTile == null))
		{
			m_currentlyMousedOverTile.NotifyMousedOut();
			m_currentlyMousedOverTile = null;
			m_SoundDucker.StopDucking();
			FadeVignetteOut();
		}
	}

	private void DestroyHistoryTilesThatFallOffTheEnd()
	{
		if (m_historyTiles.Count != 0)
		{
			float num = 0f;
			float z = GetComponent<Collider>().bounds.size.z;
			for (int i = 0; i < m_historyTiles.Count; i++)
			{
				num += m_historyTiles[i].GetTileSize();
			}
			num += 0.15f * (float)(m_historyTiles.Count - 1);
			while (num > z)
			{
				num -= m_historyTiles[0].GetTileSize();
				num -= 0.15f;
				Object.Destroy(m_historyTiles[0].gameObject);
				m_historyTiles.RemoveAt(0);
			}
		}
	}

	private void SetAsideTileAndTryToUpdate(HistoryCard tile)
	{
		Vector3 topTilePosition = GetTopTilePosition();
		tile.transform.position = new Vector3(topTilePosition.x - 20f, topTilePosition.y, topTilePosition.z);
		UpdateLayout();
	}

	private Vector3 GetTopTilePosition()
	{
		return new Vector3(base.transform.position.x, base.transform.position.y - 0.15f, base.transform.position.z);
	}

	private bool UserIsMousedOverAHistoryTile()
	{
		if (UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0))
		{
			return false;
		}
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out var hitInfo) && hitInfo.transform.GetComponentInChildren<HistoryManager>() == null && hitInfo.transform.GetComponentInChildren<HistoryCard>() == null)
		{
			return false;
		}
		float z = hitInfo.point.z;
		float num = 1000f;
		float num2 = -1000f;
		foreach (HistoryCard historyTile in m_historyTiles)
		{
			if (!historyTile.HasBeenShown())
			{
				continue;
			}
			Collider tileCollider = historyTile.GetTileCollider();
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
		if (z < num || z > num2)
		{
			return false;
		}
		return true;
	}

	private void FadeVignetteIn()
	{
		foreach (HistoryCard historyTile in m_historyTiles)
		{
			if (!(historyTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyTile.m_tileActor.gameObject, GameLayer.IgnoreFullScreenEffects);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		activeCameraFullScreenEffects.VignettingEnable = true;
		activeCameraFullScreenEffects.DesaturationEnabled = true;
		AnimateVignetteIn();
	}

	private void FadeVignetteOut()
	{
		foreach (HistoryCard historyTile in m_historyTiles)
		{
			if (!(historyTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyTile.GetTileCollider().gameObject, GameLayer.Default);
			}
		}
		SceneUtils.SetLayer(base.gameObject, GameLayer.CardRaycast);
		AnimateVignetteOut();
	}

	protected override void OnFullScreenEffectOutFinished()
	{
		if (m_animatingDesat || m_animatingVignette)
		{
			return;
		}
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Disable();
		foreach (HistoryCard historyTile in m_historyTiles)
		{
			if (!(historyTile.m_tileActor == null))
			{
				SceneUtils.SetLayer(historyTile.m_tileActor.gameObject, GameLayer.Default);
			}
		}
	}

	public bool IsShowingBigCard()
	{
		return m_showingBigCard;
	}

	public bool HasBigCard()
	{
		return m_currentBigCard != null;
	}

	public HistoryCard GetCurrentBigCard()
	{
		return m_currentBigCard;
	}

	public Entity GetPendingBigCardEntity()
	{
		if (m_pendingBigCardEntry == null)
		{
			return null;
		}
		return m_pendingBigCardEntry.m_info.GetOriginalEntity();
	}

	public void CreateFastBigCardFromMetaData(Entity entity)
	{
		int displayTimeMS = 1000;
		CreatePlayedBigCard(entity, delegate
		{
		}, delegate
		{
		}, fromMetaData: true, countered: false, displayTimeMS);
	}

	public void CreatePlayedBigCard(Entity entity, BigCardStartedCallback startedCallback, BigCardFinishedCallback finishedCallback, bool fromMetaData, bool countered, int displayTimeMS)
	{
		if (!GameState.Get().GetGameEntity().ShouldShowBigCard())
		{
			finishedCallback();
			return;
		}
		m_showingBigCard = true;
		StopCoroutine("WaitForCardLoadedAndCreateBigCard");
		BigCardEntry bigCardEntry = new BigCardEntry();
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
		StartCoroutine("WaitForCardLoadedAndCreateBigCard", bigCardEntry);
	}

	public void CreateTriggeredBigCard(Entity entity, BigCardStartedCallback startedCallback, BigCardFinishedCallback finishedCallback, bool fromMetaData, bool isSecret)
	{
		if (!GameState.Get().GetGameEntity().ShouldShowBigCard())
		{
			finishedCallback();
			return;
		}
		m_showingBigCard = true;
		StopCoroutine("WaitForCardLoadedAndCreateBigCard");
		BigCardEntry bigCardEntry = new BigCardEntry();
		bigCardEntry.m_info = new HistoryInfo();
		bigCardEntry.m_info.SetOriginalEntity(entity);
		bigCardEntry.m_info.m_infoType = HistoryInfoType.TRIGGER;
		bigCardEntry.m_fromMetaData = fromMetaData;
		bigCardEntry.m_startedCallback = startedCallback;
		bigCardEntry.m_finishedCallback = finishedCallback;
		bigCardEntry.m_waitForSecretSpell = isSecret;
		StartCoroutine("WaitForCardLoadedAndCreateBigCard", bigCardEntry);
	}

	public void NotifyOfSecretSpellFinished()
	{
		m_bigCardWaitingForSecret = false;
	}

	public void HandleClickOnBigCard(HistoryCard card)
	{
		if (m_currentBigCard != null && m_currentBigCard == card)
		{
			OnCurrentBigCardClicked();
		}
	}

	public string GetBigCardBoneName()
	{
		string text = "BigCardPosition";
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			text += "_phone";
		}
		return text;
	}

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
		if (!(transform == null))
		{
			m_bigCardPath = new Vector3[3];
			m_bigCardPath[1] = transform.position;
			m_bigCardPath[2] = GetBigCardPosition();
		}
	}

	private Vector3 GetBigCardPosition()
	{
		return Board.Get().FindBone(GetBigCardBoneName()).position;
	}

	private IEnumerator WaitForCardLoadedAndCreateBigCard(BigCardEntry bigCardEntry)
	{
		m_pendingBigCardEntry = bigCardEntry;
		HistoryInfo info = bigCardEntry.m_info;
		while (!info.CanDuplicateEntity(duplicateHiddenNonSecret: false))
		{
			yield return null;
		}
		bigCardEntry.m_startedCallback();
		info.DuplicateEntity(duplicateHiddenNonSecret: false, isEndOfHistory: false);
		m_pendingBigCardEntry = null;
		AssetLoader.Get().InstantiatePrefab("HistoryCard.prefab:f8193c3e146b62342b8fb2c0494ec447", BigCardLoadedCallback, bigCardEntry, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void BigCardLoadedCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		BigCardEntry bigCardEntry = (BigCardEntry)callbackData;
		Entity entity = bigCardEntry.m_info.GetDuplicatedEntity();
		Card card = entity.GetCard();
		DefLoader.DisposableCardDef disposableCardDef = card.ShareDisposableCardDef();
		if (entity.GetCardType() == TAG_CARDTYPE.SPELL || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER || bigCardEntry.m_fromMetaData)
		{
			go.transform.position = card.transform.position;
			go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		else
		{
			go.transform.position = GetBigCardPosition();
		}
		Entity entity2 = CreatePreTransformedEntity(entity);
		Entity postTransformedEntity = null;
		if (entity2 != null)
		{
			postTransformedEntity = entity;
			entity = entity2;
			card = entity.GetCard();
			disposableCardDef?.Dispose();
			disposableCardDef = card.ShareDisposableCardDef();
		}
		Entity entity3 = CreateSecretDeathrattleEntity(entity);
		if (entity3 != null)
		{
			entity = entity3;
			card = entity.GetCard();
			disposableCardDef?.Dispose();
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
			if ((bool)m_currentBigCard)
			{
				InterruptCurrentBigCard();
			}
			m_currentBigCard = component;
			StartCoroutine("WaitThenShowBigCard");
		}
	}

	private IEnumerator WaitThenShowBigCard()
	{
		if (m_currentBigCard.IsBigCardWaitingForSecret())
		{
			m_bigCardWaitingForSecret = true;
			m_currentBigCard.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
			while (m_bigCardWaitingForSecret)
			{
				yield return null;
			}
			if (m_currentBigCard.HasBigCardPostTransformedEntity())
			{
				m_bigCardTransformState = BigCardTransformState.PRE_TRANSFORM;
			}
			m_currentBigCard.ShowBigCard(m_bigCardPath);
			StartCoroutine("WaitThenDestroyBigCard");
			if (m_currentBigCard.HasBigCardPostTransformedEntity())
			{
				while (m_bigCardTransformState == BigCardTransformState.PRE_TRANSFORM || m_bigCardTransformState == BigCardTransformState.TRANSFORM)
				{
					yield return null;
				}
			}
		}
		else if (m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			m_bigCardTransformState = BigCardTransformState.PRE_TRANSFORM;
			m_currentBigCard.ShowBigCard(m_bigCardPath);
			StartCoroutine("WaitThenDestroyBigCard");
			while (m_bigCardTransformState == BigCardTransformState.PRE_TRANSFORM || m_bigCardTransformState == BigCardTransformState.TRANSFORM)
			{
				yield return null;
			}
		}
		else
		{
			m_currentBigCard.ShowBigCard(m_bigCardPath);
			StartCoroutine("WaitThenDestroyBigCard");
		}
		Entity entity = m_currentBigCard.GetEntity();
		if (entity.HasSubCards())
		{
			Network.HistBlockStart histBlockStart = GameState.Get().GetPowerProcessor().GetHistoryBlockingTaskList()?.GetBlockStart();
			if (histBlockStart.SubOption != -1)
			{
				Card card = entity.GetCard();
				ChoiceCardMgr.Get().ShowSubOptions(card);
				StartCoroutine(FinishSpectatorSubOption(entity, histBlockStart.SubOption));
			}
		}
		yield return new WaitForSeconds(1f);
		m_currentBigCard.RunBigCardFinishedCallback();
	}

	private IEnumerator FinishSpectatorSubOption(Entity mainEntity, int chosenSubOption)
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
			Log.All.PrintError("actualChoiceCards is NULL. Attempting workaround.");
			choiceCards = new List<Card>();
		}
		else
		{
			choiceCards = new List<Card>(friendlyCards);
		}
		Card subCard = ((chosenSubOption < choiceCards.Count) ? choiceCards[chosenSubOption] : null);
		Entity subEntity = (subCard ? subCard.GetEntity() : null);
		if (subCard != null)
		{
			subCard.SetInputEnabled(enabled: false);
		}
		yield return new WaitForSeconds(1f);
		if (subCard != null)
		{
			subCard.SetInputEnabled(enabled: true);
		}
		GameState gameState = GameState.Get();
		if (gameState == null || gameState.IsGameOver())
		{
			foreach (Card item in choiceCards)
			{
				item.HideCard();
			}
		}
		else
		{
			InputManager.Get().HandleClickOnSubOption(subEntity, isSimulated: true);
		}
	}

	private IEnumerator WaitThenDestroyBigCard()
	{
		float num = (float)m_currentBigCard.GetDisplayTimeMS() / 1000f;
		if (num <= 0f)
		{
			if (m_currentBigCard.IsBigCardFromMetaData())
			{
				num = 1.5f;
			}
			else
			{
				num = ((m_currentBigCard.GetEntity() == null) ? 4f : (m_currentBigCard.GetEntity().GetCardType() switch
				{
					TAG_CARDTYPE.SPELL => 4f + GameState.Get().GetGameEntity().GetAdditionalTimeToWaitForSpells(), 
					TAG_CARDTYPE.HERO_POWER => 4f + GameState.Get().GetGameEntity().GetAdditionalTimeToWaitForSpells(), 
					_ => 3f, 
				}));
				if (m_currentBigCard.HasBigCardPostTransformedEntity())
				{
					num *= 0.5f;
				}
			}
		}
		yield return new WaitForSeconds(num);
		DestroyBigCard();
	}

	private void DestroyBigCard()
	{
		if (!(m_currentBigCard == null))
		{
			if (m_currentBigCard.m_mainCardActor == null)
			{
				RunFinishedCallbackAndDestroyBigCard();
			}
			else if (m_currentBigCard.HasBigCardPostTransformedEntity())
			{
				PlayBigCardTransformEffects();
			}
			else if (m_currentBigCard.WasBigCardCountered())
			{
				PlayBigCardCounteredEffects();
			}
			else
			{
				RunFinishedCallbackAndDestroyBigCard();
			}
		}
	}

	private void RunFinishedCallbackAndDestroyBigCard()
	{
		if (!(m_currentBigCard == null))
		{
			m_currentBigCard.RunBigCardFinishedCallback();
			m_showingBigCard = false;
			Object.Destroy(m_currentBigCard.gameObject);
		}
	}

	private void PlayBigCardCounteredEffects()
	{
		Spell.StateFinishedCallback callback = delegate(Spell s, SpellStateType prevStateType, object userData)
		{
			if (s.GetActiveState() == SpellStateType.NONE)
			{
				HistoryCard obj = (HistoryCard)userData;
				m_showingBigCard = false;
				Object.Destroy(obj.gameObject);
			}
		};
		Spell spell = m_currentBigCard.m_mainCardActor.GetSpell(SpellType.DEATH);
		if (spell == null)
		{
			RunFinishedCallbackAndDestroyBigCard();
			return;
		}
		spell.AddStateFinishedCallback(callback, m_currentBigCard);
		m_currentBigCard.RunBigCardFinishedCallback();
		m_currentBigCard = null;
		spell.Activate();
	}

	private void PlayBigCardTransformEffects()
	{
		StartCoroutine("PlayBigCardTransformEffectsWithTiming");
	}

	private IEnumerator PlayBigCardTransformEffectsWithTiming()
	{
		if (m_bigCardTransformState == BigCardTransformState.INVALID)
		{
			RunFinishedCallbackAndDestroyBigCard();
			yield break;
		}
		if (m_bigCardTransformState == BigCardTransformState.PRE_TRANSFORM)
		{
			m_bigCardTransformState = BigCardTransformState.TRANSFORM;
			yield return StartCoroutine(PlayBigCardTransformSpell());
		}
		if (m_bigCardTransformState == BigCardTransformState.TRANSFORM)
		{
			m_bigCardTransformState = BigCardTransformState.POST_TRANSFORM;
			yield return StartCoroutine(WaitForBigCardPostTransform());
		}
		if (m_bigCardTransformState == BigCardTransformState.POST_TRANSFORM)
		{
			m_bigCardTransformState = BigCardTransformState.INVALID;
			RunFinishedCallbackAndDestroyBigCard();
		}
	}

	private IEnumerator PlayBigCardTransformSpell()
	{
		if (m_TransformSpells == null || m_TransformSpells.Length == 0)
		{
			yield break;
		}
		Entity entity = m_currentBigCard.GetEntity();
		int num = entity.GetTag(GAME_TAG.TRANSFORMED_FROM_CARD_VISUAL_TYPE);
		if (num < 0 || num >= m_TransformSpells.Length)
		{
			num = 0;
		}
		m_bigCardTransformSpell = Object.Instantiate(m_TransformSpells[num]);
		if (m_bigCardTransformSpell == null)
		{
			yield break;
		}
		Card card = entity.GetCard();
		m_bigCardTransformSpell.SetSource(card.gameObject);
		m_bigCardTransformSpell.AddTarget(card.gameObject);
		m_bigCardTransformSpell.m_SetParentToLocation = true;
		m_bigCardTransformSpell.UpdateTransform();
		m_bigCardTransformSpell.SetPosition(m_currentBigCard.m_mainCardActor.transform.position);
		Spell.StateFinishedCallback callback = delegate(Spell s, SpellStateType prevStateType, object userData)
		{
			if (s.GetActiveState() == SpellStateType.NONE)
			{
				Object.Destroy(s.gameObject);
			}
		};
		m_bigCardTransformSpell.AddStateFinishedCallback(callback);
		m_bigCardTransformSpell.Activate();
		while ((bool)m_bigCardTransformSpell && !m_bigCardTransformSpell.IsFinished())
		{
			yield return null;
		}
	}

	private IEnumerator WaitForBigCardPostTransform()
	{
		Actor mainCardActor = m_currentBigCard.m_mainCardActor;
		mainCardActor.Hide(ignoreSpells: true);
		m_currentBigCard.LoadBigCardPostTransformedEntity();
		TransformUtil.CopyLocal(m_currentBigCard.m_mainCardActor, mainCardActor);
		yield return new WaitForSeconds(2f);
	}

	private void OnCurrentBigCardClicked()
	{
		if (m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			ForceNextBigCardTransformState();
		}
		else
		{
			InterruptCurrentBigCard();
		}
	}

	private void ForceNextBigCardTransformState()
	{
		switch (m_bigCardTransformState)
		{
		case BigCardTransformState.PRE_TRANSFORM:
			m_bigCardTransformState = BigCardTransformState.TRANSFORM;
			StopWaitingThenDestroyBigCard();
			break;
		case BigCardTransformState.TRANSFORM:
			if ((bool)m_bigCardTransformSpell)
			{
				Object.Destroy(m_bigCardTransformSpell.gameObject);
			}
			break;
		case BigCardTransformState.POST_TRANSFORM:
			InterruptCurrentBigCard();
			break;
		}
	}

	private void StopWaitingThenDestroyBigCard()
	{
		StopCoroutine("WaitThenDestroyBigCard");
		DestroyBigCard();
	}

	private void InterruptCurrentBigCard()
	{
		StopCoroutine("WaitThenShowBigCard");
		if (m_currentBigCard.HasBigCardPostTransformedEntity())
		{
			CutoffBigCardTransformEffects();
		}
		else
		{
			StopWaitingThenDestroyBigCard();
		}
	}

	private void CutoffBigCardTransformEffects()
	{
		if ((bool)m_bigCardTransformSpell)
		{
			Object.Destroy(m_bigCardTransformSpell.gameObject);
		}
		StopCoroutine("PlayBigCardTransformEffectsWithTiming");
		m_bigCardTransformState = BigCardTransformState.INVALID;
		RunFinishedCallbackAndDestroyBigCard();
	}
}
