using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class PowerTaskList
{
	public delegate void CompleteCallback(PowerTaskList taskList, int startIndex, int count, object userData);

	public class DamageInfo
	{
		public Entity m_entity;

		public int m_damage;
	}

	private class ZoneChangeCallbackData
	{
		public int m_startIndex;

		public int m_count;

		public CompleteCallback m_taskListCallback;

		public object m_taskListUserData;
	}

	private int m_id;

	private Network.HistBlockStart m_blockStart;

	private Network.HistBlockEnd m_blockEnd;

	private List<PowerTask> m_tasks = new List<PowerTask>();

	private ZoneChangeList m_zoneChangeList;

	private int m_pendingTasks;

	private bool m_isBatchable;

	private bool m_isDeferrable;

	private int m_deferredSourceId;

	private PowerTaskList m_previous;

	private PowerTaskList m_next;

	private PowerTaskList m_subSpellOrigin;

	private Network.HistSubSpellStart m_subSpellStart;

	private Network.HistSubSpellEnd m_subSpellEnd;

	private Network.HistVoSpell m_voSpell;

	private PowerTaskList m_parent;

	private bool m_attackDataBuilt;

	private AttackInfo m_attackInfo;

	private AttackType m_attackType;

	private Entity m_attacker;

	private Entity m_defender;

	private Entity m_proposedDefender;

	private bool m_repeatProposed;

	private bool m_willCompleteHistoryEntry;

	private Entity m_ritualEntityClone;

	private Entity m_invokeEntityClone;

	private int? m_lastBattlecryEffectIndex;

	private float m_taskListStartTime;

	private float m_taskListEndTime;

	private int m_taskListSlushTimeMilliseconds = -1;

	private bool m_isHistoryBlockStart;

	private bool m_isHistoryBlockEnd;

	private bool m_collapsible;

	public int GetId()
	{
		return m_id;
	}

	public void SetId(int id)
	{
		m_id = id;
	}

	public int GetDeferredSourceId()
	{
		return m_deferredSourceId;
	}

	public void SetDeferredSourceId(int id)
	{
		m_deferredSourceId = id;
	}

	public void AddTasks(PowerTaskList otherTaskList)
	{
		m_tasks.AddRange(otherTaskList.m_tasks);
		m_pendingTasks += otherTaskList.m_pendingTasks;
	}

	public void SetProcessStartTime()
	{
		m_taskListStartTime = Time.realtimeSinceStartup;
		GameState.Get().GetPowerProcessor().HandleTimelineStartEvent(m_id, m_taskListStartTime, m_isHistoryBlockStart, GetBlockStart());
	}

	public void SetProcessEndTime()
	{
		m_taskListEndTime = Time.realtimeSinceStartup;
		GameState.Get().GetPowerProcessor().HandleTimelineEndEvent(m_id, m_taskListEndTime, m_isHistoryBlockEnd);
	}

	public void SetDeferrable(bool deferrable)
	{
		m_isDeferrable = deferrable;
	}

	public bool IsDeferrable()
	{
		return m_isDeferrable;
	}

	public void SetBatchable(bool batchable)
	{
		m_isBatchable = batchable;
	}

	public bool IsBatchable()
	{
		if (m_isBatchable && m_blockStart != null)
		{
			return m_blockEnd != null;
		}
		return false;
	}

	public bool IsCollapsible(bool isEarlier)
	{
		if (isEarlier && !m_collapsible)
		{
			return false;
		}
		bool flag = false;
		if (m_tasks.Count > 0)
		{
			PowerTask powerTask = m_tasks[m_tasks.Count - 1];
			if (powerTask.GetPower() is Network.HistMetaData)
			{
				flag = ((Network.HistMetaData)powerTask.GetPower()).MetaType == HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT;
			}
		}
		if (m_subSpellStart != null && !isEarlier)
		{
			return false;
		}
		if (m_subSpellEnd != null && isEarlier)
		{
			return false;
		}
		if (m_isHistoryBlockStart && !isEarlier)
		{
			return false;
		}
		if (m_isHistoryBlockEnd && isEarlier)
		{
			return false;
		}
		return !flag;
	}

	public void SetCollapsible(bool collapsible)
	{
		m_collapsible = collapsible;
	}

	public bool IsSlushTimeHelper()
	{
		if (m_tasks.Count != 1)
		{
			return false;
		}
		if (m_tasks[0].GetPower() is Network.HistMetaData)
		{
			return ((Network.HistMetaData)m_tasks[0].GetPower()).MetaType == HistoryMeta.Type.SLUSH_TIME;
		}
		return false;
	}

	public bool HasAnyTasksInImmediate()
	{
		return m_tasks.Count > 0;
	}

	public void SetHistoryBlockStart(bool isStart)
	{
		m_isHistoryBlockStart = isStart;
	}

	public void SetHistoryBlockEnd(bool isEnd)
	{
		m_isHistoryBlockEnd = isEnd;
	}

	public void OnTaskCompleted()
	{
		if (--m_pendingTasks == 0)
		{
			OnTaskListCompleted();
		}
	}

	public bool IsEmpty()
	{
		PowerTaskList origin = GetOrigin();
		if (origin.m_blockStart != null)
		{
			return false;
		}
		if (origin.m_blockEnd != null)
		{
			return false;
		}
		if (origin.m_tasks.Count > 0)
		{
			return false;
		}
		return true;
	}

	public bool IsOrigin()
	{
		return m_previous == null;
	}

	public void FillMetaDataTargetSourceData()
	{
		foreach (PowerTask task in m_tasks)
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Data == 0)
				{
					histMetaData.Data = GetSourceEntity()?.GetEntityId() ?? 0;
				}
			}
		}
	}

	public PowerTaskList GetOrigin()
	{
		PowerTaskList powerTaskList = this;
		while (powerTaskList.m_previous != null)
		{
			powerTaskList = powerTaskList.m_previous;
		}
		return powerTaskList;
	}

	public PowerTaskList GetPrevious()
	{
		return m_previous;
	}

	public void SetPrevious(PowerTaskList taskList)
	{
		m_previous = taskList;
		taskList.m_next = this;
	}

	public PowerTaskList GetNext()
	{
		return m_next;
	}

	public void SetNext(PowerTaskList next)
	{
		m_next = next;
	}

	public PowerTaskList GetLast()
	{
		PowerTaskList powerTaskList = this;
		while (powerTaskList.m_next != null)
		{
			powerTaskList = powerTaskList.m_next;
		}
		return powerTaskList;
	}

	public Network.HistBlockStart GetBlockStart()
	{
		return GetOrigin().m_blockStart;
	}

	public void SetBlockStart(Network.HistBlockStart blockStart)
	{
		m_blockStart = blockStart;
	}

	public Network.HistBlockEnd GetBlockEnd()
	{
		return m_blockEnd;
	}

	public void SetBlockEnd(Network.HistBlockEnd blockEnd)
	{
		m_blockEnd = blockEnd;
	}

	public PowerTaskList GetParent()
	{
		return GetOrigin().m_parent;
	}

	public void SetParent(PowerTaskList parent)
	{
		m_parent = parent;
	}

	public Network.HistSubSpellStart GetSubSpellStart()
	{
		return m_subSpellStart;
	}

	public void SetSubSpellStart(Network.HistSubSpellStart subSpellStart)
	{
		m_subSpellStart = subSpellStart;
	}

	public void SetVoSpellStart(Network.HistVoSpell voSpell)
	{
		m_voSpell = voSpell;
	}

	public Network.HistVoSpell GetVoSpell()
	{
		return m_voSpell;
	}

	public Network.HistSubSpellEnd GetSubSpellEnd()
	{
		return m_subSpellEnd;
	}

	public void SetSubSpellEnd(Network.HistSubSpellEnd subSpellEnd)
	{
		m_subSpellEnd = subSpellEnd;
	}

	public PowerTaskList GetSubSpellOrigin()
	{
		return m_subSpellOrigin;
	}

	public void SetSubSpellOrigin(PowerTaskList taskList)
	{
		m_subSpellOrigin = taskList;
	}

	public bool IsBlock()
	{
		return GetOrigin().m_blockStart != null;
	}

	public bool IsStartOfBlock()
	{
		if (!IsBlock())
		{
			return false;
		}
		return m_blockStart != null;
	}

	public bool IsEndOfBlock()
	{
		if (!IsBlock())
		{
			return false;
		}
		return m_blockEnd != null;
	}

	public bool DoesBlockHaveEndAction()
	{
		return GetLast().m_blockEnd != null;
	}

	public bool IsBlockUnended()
	{
		if (!IsBlock())
		{
			return false;
		}
		if (DoesBlockHaveEndAction())
		{
			return false;
		}
		return true;
	}

	public bool IsEarlierInBlockThan(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return false;
		}
		for (PowerTaskList previous = taskList.m_previous; previous != null; previous = previous.m_previous)
		{
			if (this == previous)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsLaterInBlockThan(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return false;
		}
		for (PowerTaskList next = taskList.m_next; next != null; next = next.m_next)
		{
			if (this == next)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInBlock(PowerTaskList taskList)
	{
		if (this == taskList)
		{
			return true;
		}
		if (IsEarlierInBlockThan(taskList))
		{
			return true;
		}
		if (IsLaterInBlockThan(taskList))
		{
			return true;
		}
		return false;
	}

	public bool IsDescendantOfBlock(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return false;
		}
		if (IsInBlock(taskList))
		{
			return true;
		}
		PowerTaskList origin = taskList.GetOrigin();
		for (PowerTaskList parent = GetParent(); parent != null; parent = parent.m_parent)
		{
			if (parent == origin)
			{
				return true;
			}
		}
		return false;
	}

	public List<PowerTask> GetTaskList()
	{
		return m_tasks;
	}

	public bool HasTasks()
	{
		return m_tasks.Count > 0;
	}

	public PowerTask CreateTask(Network.PowerHistory netPower)
	{
		m_pendingTasks++;
		PowerTask powerTask = new PowerTask();
		powerTask.SetPower(netPower);
		powerTask.SetTaskCompleteCallback(OnTaskCompleted);
		m_tasks.Add(powerTask);
		return powerTask;
	}

	public bool HasTasksOfType(Network.PowerType powType)
	{
		foreach (PowerTask task in m_tasks)
		{
			if (task.GetPower().Type == powType)
			{
				return true;
			}
		}
		return false;
	}

	public Entity GetSourceEntity(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				int current = enumerator.Current;
				Entity entity = GameState.Get().GetEntity(current);
				if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(current))
				{
					string format = $"PowerProcessor.GetSourceEntity() - task list {m_id} has a source entity with id {current} but there is no entity with that id";
					Log.Power.PrintWarning(format);
					return null;
				}
				return entity;
			}
		}
		return null;
	}

	public List<Entity> GetSourceEntities(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		List<int> entities = blockStart.Entities;
		List<Entity> list = new List<Entity>();
		foreach (int item in entities)
		{
			Entity entity = GameState.Get().GetEntity(item);
			if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(item))
			{
				string format = $"PowerProcessor.GetSourceEntity() - task list {m_id} has a source entity with id {item} but there is no entity with that id";
				Log.Power.PrintWarning(format);
				return null;
			}
			list.Add(entity);
		}
		return list;
	}

	public bool IsEffectCardIdClientCached(int entityId)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		int num = 0;
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			while (enumerator.MoveNext() && enumerator.Current != entityId)
			{
				num++;
			}
		}
		if (num >= blockStart.IsEffectCardIdClientCached.Count)
		{
			return false;
		}
		return blockStart.IsEffectCardIdClientCached[num];
	}

	public string GetEffectCardId(int entityId)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		int num = 0;
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			while (enumerator.MoveNext() && enumerator.Current != entityId)
			{
				num++;
			}
		}
		if (num >= blockStart.EffectCardId.Count)
		{
			return null;
		}
		string text = blockStart.EffectCardId[num];
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		return GetSourceEntity()?.GetCardId();
	}

	public EntityDef GetEffectEntityDef(int entityId)
	{
		string effectCardId = GetEffectCardId(entityId);
		if (string.IsNullOrEmpty(effectCardId))
		{
			return null;
		}
		return DefLoader.Get().GetEntityDef(effectCardId);
	}

	public string GetEffectCardId()
	{
		Entity sourceEntity = GetSourceEntity();
		if (sourceEntity == null)
		{
			return null;
		}
		return GetEffectCardId(sourceEntity.GetEntityId());
	}

	public EntityDef GetEffectEntityDef()
	{
		Entity sourceEntity = GetSourceEntity();
		if (sourceEntity == null)
		{
			return null;
		}
		return GetEffectEntityDef(sourceEntity.GetEntityId());
	}

	public Entity GetTargetEntity(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		int target = blockStart.Target;
		Entity entity = GameState.Get().GetEntity(target);
		if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(target))
		{
			string format = $"PowerProcessor.GetTargetEntity() - task list {m_id} has a target entity with id {target} but there is no entity with that id";
			Log.Power.PrintWarning(format);
			return null;
		}
		return entity;
	}

	public bool HasTargetEntity()
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		int target = blockStart.Target;
		return GameState.Get().GetEntity(target) != null;
	}

	public bool HasMetaDataTasks()
	{
		foreach (PowerTask task in m_tasks)
		{
			if (task.GetPower().Type == Network.PowerType.META_DATA)
			{
				return true;
			}
		}
		return false;
	}

	public bool DoesBlockHaveMetaDataTasks()
	{
		for (PowerTaskList powerTaskList = GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.HasMetaDataTasks())
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCardDraw()
	{
		foreach (PowerTask task in m_tasks)
		{
			if (task.IsCardDraw())
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCardMill()
	{
		foreach (PowerTask task in m_tasks)
		{
			if (task.IsCardMill())
			{
				return true;
			}
		}
		return false;
	}

	public bool HasFatigue()
	{
		foreach (PowerTask task in m_tasks)
		{
			if (task.IsFatigue())
			{
				return true;
			}
		}
		return false;
	}

	public int GetTotalSlushTime()
	{
		if (m_taskListSlushTimeMilliseconds > -1)
		{
			return m_taskListSlushTimeMilliseconds;
		}
		int num = 0;
		foreach (PowerTask task in m_tasks)
		{
			Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.SLUSH_TIME)
			{
				num += histMetaData.Data;
			}
		}
		m_taskListSlushTimeMilliseconds = num;
		return num;
	}

	public bool HasEffectTimingMetaData()
	{
		foreach (PowerTask task in m_tasks)
		{
			Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
			if (histMetaData != null)
			{
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET)
				{
					return true;
				}
				if (histMetaData.MetaType == HistoryMeta.Type.EFFECT_TIMING)
				{
					return true;
				}
			}
		}
		return false;
	}

	public List<PowerTask> GetTagChangeTasks()
	{
		List<PowerTask> list = new List<PowerTask>();
		foreach (PowerTask task in m_tasks)
		{
			if (task.GetPower() is Network.HistTagChange)
			{
				list.Add(task);
			}
		}
		return list;
	}

	public bool DoesBlockHaveEffectTimingMetaData()
	{
		for (PowerTaskList powerTaskList = GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.GetSubSpellOrigin() == GetSubSpellOrigin() && powerTaskList.HasEffectTimingMetaData())
			{
				return true;
			}
		}
		return false;
	}

	public HistoryBlock.Type GetBlockType()
	{
		return GetBlockStart()?.BlockType ?? HistoryBlock.Type.INVALID;
	}

	public bool IsBlockType(HistoryBlock.Type type)
	{
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		return blockStart.BlockType == type;
	}

	public bool IsPlayBlock()
	{
		return IsBlockType(HistoryBlock.Type.PLAY);
	}

	public bool IsPowerBlock()
	{
		return IsBlockType(HistoryBlock.Type.POWER);
	}

	public bool IsTriggerBlock()
	{
		return IsBlockType(HistoryBlock.Type.TRIGGER);
	}

	public bool IsDeathBlock()
	{
		return IsBlockType(HistoryBlock.Type.DEATHS);
	}

	public bool IsRitualBlock()
	{
		return IsBlockType(HistoryBlock.Type.RITUAL);
	}

	public bool IsSubSpellTaskList()
	{
		return m_subSpellOrigin != null;
	}

	public void DoTasks(int startIndex, int count)
	{
		DoTasks(startIndex, count, null, null);
	}

	public void DoTasks(int startIndex, int count, CompleteCallback callback)
	{
		DoTasks(startIndex, count, callback, null);
	}

	public void DoTasks(int startIndex, int count, CompleteCallback callback, object userData)
	{
		bool flag = false;
		int num = -1;
		int num2 = Mathf.Min(startIndex + count - 1, m_tasks.Count - 1);
		for (int i = startIndex; i <= num2; i++)
		{
			PowerTask powerTask = m_tasks[i];
			if (!powerTask.IsCompleted())
			{
				if (num < 0)
				{
					num = i;
				}
				if (ZoneMgr.IsHandledPower(powerTask.GetPower()))
				{
					flag = true;
					break;
				}
			}
		}
		if (num < 0)
		{
			num = startIndex;
		}
		if (flag)
		{
			ZoneChangeCallbackData zoneChangeCallbackData = new ZoneChangeCallbackData();
			zoneChangeCallbackData.m_startIndex = startIndex;
			zoneChangeCallbackData.m_count = count;
			zoneChangeCallbackData.m_taskListCallback = callback;
			zoneChangeCallbackData.m_taskListUserData = userData;
			m_zoneChangeList = ZoneMgr.Get().AddServerZoneChanges(this, num, num2, OnZoneChangeComplete, zoneChangeCallbackData);
			if (m_zoneChangeList != null)
			{
				return;
			}
		}
		if (Gameplay.Get() != null)
		{
			Gameplay.Get().StartCoroutine(WaitForGameStateAndDoTasks(num, num2, startIndex, count, callback, userData));
		}
		else
		{
			DoTasks(num, num2, startIndex, count, callback, userData);
		}
	}

	public void DoAllTasks(CompleteCallback callback, object userData)
	{
		DoTasks(0, m_tasks.Count, callback, userData);
	}

	public void DoAllTasks(CompleteCallback callback)
	{
		DoTasks(0, m_tasks.Count, callback, null);
	}

	public void DoAllTasks()
	{
		DoTasks(0, m_tasks.Count, null, null);
	}

	public void DoEarlyConcedeTasks()
	{
		for (int i = 0; i < m_tasks.Count; i++)
		{
			m_tasks[i].DoEarlyConcedeTask();
		}
	}

	public bool IsComplete()
	{
		if (!AreTasksComplete())
		{
			return false;
		}
		if (!AreZoneChangesComplete())
		{
			return false;
		}
		return true;
	}

	public bool AreTasksComplete()
	{
		foreach (PowerTask task in m_tasks)
		{
			if (!task.IsCompleted())
			{
				return false;
			}
		}
		return true;
	}

	public bool IsTaskPartOfMetaData(int taskIndex, HistoryMeta.Type metaType)
	{
		for (int num = taskIndex; num >= 0; num--)
		{
			Network.PowerHistory power = m_tasks[num].GetPower();
			if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == metaType)
			{
				return true;
			}
		}
		return false;
	}

	public Card GetStartDrawMetaDataCard()
	{
		for (int i = 0; i < m_tasks.Count; i++)
		{
			Network.PowerHistory power = m_tasks[i].GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType == HistoryMeta.Type.START_DRAW)
			{
				Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
				if (entity != null)
				{
					return entity.GetCard();
				}
			}
		}
		return null;
	}

	public int FindEarlierIncompleteTaskIndex(int taskIndex)
	{
		for (int num = taskIndex - 1; num >= 0; num--)
		{
			if (!m_tasks[num].IsCompleted())
			{
				return num;
			}
		}
		return -1;
	}

	public bool HasEarlierIncompleteTask(int taskIndex)
	{
		return FindEarlierIncompleteTaskIndex(taskIndex) >= 0;
	}

	public bool HasZoneChanges()
	{
		return m_zoneChangeList != null;
	}

	public bool AreZoneChangesComplete()
	{
		if (m_zoneChangeList == null)
		{
			return true;
		}
		return m_zoneChangeList.IsComplete();
	}

	public AttackInfo GetAttackInfo()
	{
		BuildAttackData();
		return m_attackInfo;
	}

	public AttackType GetAttackType()
	{
		BuildAttackData();
		return m_attackType;
	}

	public Entity GetAttacker()
	{
		BuildAttackData();
		return m_attacker;
	}

	public Entity GetDefender()
	{
		BuildAttackData();
		return m_defender;
	}

	public Entity GetProposedDefender()
	{
		BuildAttackData();
		return m_proposedDefender;
	}

	public bool IsRepeatProposedAttack()
	{
		BuildAttackData();
		return m_repeatProposed;
	}

	public bool HasGameOver()
	{
		for (int i = 0; i < m_tasks.Count; i++)
		{
			Network.PowerHistory power = m_tasks[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (GameUtils.IsGameOverTag(histTagChange.Entity, histTagChange.Tag, histTagChange.Value))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool HasFriendlyConcede()
	{
		for (int i = 0; i < m_tasks.Count; i++)
		{
			Network.PowerHistory power = m_tasks[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE && GameUtils.IsFriendlyConcede((Network.HistTagChange)power))
			{
				return true;
			}
		}
		return false;
	}

	public DamageInfo GetDamageInfo(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		int entityId = entity.GetEntityId();
		foreach (PowerTask task in m_tasks)
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 44 && histTagChange.Entity == entityId)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.m_entity = GameState.Get().GetEntity(histTagChange.Entity);
					damageInfo.m_damage = histTagChange.Value - damageInfo.m_entity.GetDamage();
					return damageInfo;
				}
			}
		}
		return null;
	}

	public void SetWillCompleteHistoryEntry(bool set)
	{
		m_willCompleteHistoryEntry = set;
	}

	public bool WillCompleteHistoryEntry()
	{
		return m_willCompleteHistoryEntry;
	}

	public bool WillBlockCompleteHistoryEntry()
	{
		for (PowerTaskList powerTaskList = GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.WillCompleteHistoryEntry())
			{
				return true;
			}
		}
		return false;
	}

	public Entity GetRitualEntityClone()
	{
		return m_ritualEntityClone;
	}

	public void SetRitualEntityClone(Entity ent)
	{
		m_ritualEntityClone = ent;
	}

	public Entity GetInvokeEntityClone()
	{
		return m_invokeEntityClone;
	}

	public void SetInvokeEntityClone(Entity ent)
	{
		m_invokeEntityClone = ent;
	}

	public bool WasThePlayedSpellCountered(Entity entity)
	{
		foreach (PowerTask task in m_tasks)
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 231 && histTagChange.Value == 1)
				{
					return true;
				}
			}
		}
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue()
			.GetList())
		{
			foreach (PowerTask task2 in item.GetTaskList())
			{
				Network.PowerHistory power2 = task2.GetPower();
				if (power2.Type == Network.PowerType.TAG_CHANGE)
				{
					Network.HistTagChange histTagChange2 = power2 as Network.HistTagChange;
					if (histTagChange2.Entity == entity.GetEntityId() && histTagChange2.Tag == 231 && histTagChange2.Value == 1)
					{
						return true;
					}
				}
			}
			if (item.GetBlockEnd() != null && item.GetBlockStart().BlockType == HistoryBlock.Type.PLAY)
			{
				return false;
			}
		}
		return false;
	}

	public void CreateArtificialHistoryTilesFromMetadata()
	{
		List<PowerTask> list = new List<PowerTask>();
		bool flag = false;
		foreach (PowerTask task in GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TILE || histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE)
				{
					int id = histMetaData.Info[0];
					Entity entity = GameState.Get().GetEntity(id);
					if (entity != null)
					{
						if (flag)
						{
							NotifyHistoryOfAdditionalTargets(list);
							HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
							list.Clear();
						}
						else
						{
							flag = true;
						}
						if (histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE)
						{
							HistoryManager.Get().CreateTriggerTile(entity);
						}
						else
						{
							HistoryManager.Get().CreatePlayedTile(entity, null);
						}
					}
				}
				else if (flag && histMetaData.MetaType == HistoryMeta.Type.END_ARTIFICIAL_HISTORY_TILE)
				{
					flag = false;
					NotifyHistoryOfAdditionalTargets(list);
					HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
					list.Clear();
				}
				else if (flag)
				{
					list.Add(task);
				}
			}
			else if (flag)
			{
				list.Add(task);
			}
		}
		if (flag)
		{
			NotifyHistoryOfAdditionalTargets(list);
			HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
		}
	}

	public void NotifyHistoryOfAdditionalTargets(List<PowerTask> tasksToInclude = null)
	{
		if (tasksToInclude == null)
		{
			tasksToInclude = GetTaskList();
		}
		bool flag = false;
		List<int> list = GetBlockStart()?.Entities;
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		bool flag2 = true;
		foreach (PowerTask item in tasksToInclude)
		{
			Network.PowerHistory power = item.GetPower();
			if (flag)
			{
				if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == HistoryMeta.Type.END_ARTIFICIAL_HISTORY_TILE)
				{
					flag = false;
				}
			}
			else if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET)
				{
					for (int i = 0; i < histMetaData.Info.Count; i++)
					{
						HistoryManager.Get().NotifyEntityAffected(histMetaData.Info[i], allowDuplicates: false, fromMetaData: false);
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.DAMAGE || histMetaData.MetaType == HistoryMeta.Type.HEALING)
				{
					flag2 = false;
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.OVERRIDE_HISTORY)
				{
					HistoryManager.Get().OverrideCurrentHistoryEntryWithMetaData();
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.HISTORY_TARGET)
				{
					for (int j = 0; j < histMetaData.Info.Count; j++)
					{
						int id = histMetaData.Info[j];
						Entity entity = GameState.Get().GetEntity(id);
						if (entity != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity, allowDuplicates: false, fromMetaData: true);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.BURNED_CARD)
				{
					for (int k = 0; k < histMetaData.Info.Count; k++)
					{
						int id2 = histMetaData.Info[k];
						Entity entity2 = GameState.Get().GetEntity(id2);
						if (entity2 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity2, allowDuplicates: false, fromMetaData: true, dontDuplicateUntilEnd: false, isBurnedCard: true);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.POISONOUS)
				{
					for (int l = 0; l < histMetaData.Info.Count; l++)
					{
						int id3 = histMetaData.Info[l];
						Entity entity3 = GameState.Get().GetEntity(id3);
						if (entity3 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity3, allowDuplicates: false, fromMetaData: true, dontDuplicateUntilEnd: false, isBurnedCard: false, isPoisonous: true);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END)
				{
					for (int m = 0; m < histMetaData.Info.Count; m++)
					{
						int id4 = histMetaData.Info[m];
						Entity entity4 = GameState.Get().GetEntity(id4);
						if (entity4 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity4, allowDuplicates: true, fromMetaData: true, dontDuplicateUntilEnd: true);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TILE || histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE)
				{
					flag = true;
				}
			}
			else if (power.Type == Network.PowerType.SHOW_ENTITY)
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				Entity entity5 = GameState.Get().GetEntity(histShowEntity.Entity.ID);
				bool flag6 = entity5.GetZone() == TAG_ZONE.HAND;
				bool flag7 = entity5.GetZone() == TAG_ZONE.SETASIDE;
				foreach (Network.Entity.Tag tag in histShowEntity.Entity.Tags)
				{
					if (tag.Name == 202 && tag.Value == 6)
					{
						flag3 = true;
						break;
					}
					if (tag.Name == 49)
					{
						if (tag.Value == 4)
						{
							flag4 = true;
						}
						else if (tag.Value == 6)
						{
							flag5 = true;
						}
					}
				}
				if (!flag3 && !(flag4 && flag7) && !(flag5 && flag7))
				{
					if (flag4 && !flag6)
					{
						HistoryManager.Get().NotifyEntityDied(histShowEntity.Entity.ID);
					}
					else
					{
						HistoryManager.Get().NotifyEntityAffected(histShowEntity.Entity.ID, allowDuplicates: false, fromMetaData: false);
					}
				}
			}
			else if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = (Network.HistFullEntity)power;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				foreach (Network.Entity.Tag tag2 in histFullEntity.Entity.Tags)
				{
					if (tag2.Name == 202 && tag2.Value == 6)
					{
						flag8 = true;
						break;
					}
					if (tag2.Name == 49 && (tag2.Value == 1 || tag2.Value == 7))
					{
						flag9 = true;
					}
					else if (tag2.Name == 385 && list != null && list.Contains(tag2.Value))
					{
						flag10 = true;
					}
				}
				if (!flag8 && (flag9 || flag10))
				{
					HistoryManager.Get().NotifyEntityAffected(histFullEntity.Entity.ID, allowDuplicates: false, fromMetaData: false);
				}
			}
			else
			{
				if (power.Type != Network.PowerType.TAG_CHANGE)
				{
					continue;
				}
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.ChangeDef)
				{
					continue;
				}
				Entity entity6 = GameState.Get().GetEntity(histTagChange.Entity);
				if (histTagChange.Tag == 44)
				{
					if (!list2.Contains(histTagChange.Entity) && !flag2)
					{
						HistoryManager.Get().NotifyDamageChanged(entity6, histTagChange.Value);
						flag2 = true;
					}
				}
				else if (histTagChange.Tag == 292)
				{
					if (!list2.Contains(histTagChange.Entity) && !list3.Contains(histTagChange.Entity))
					{
						HistoryManager.Get().NotifyArmorChanged(entity6, histTagChange.Value);
					}
				}
				else if (histTagChange.Tag == 45)
				{
					if (!list2.Contains(histTagChange.Entity))
					{
						HistoryManager.Get().NotifyHealthChanged(entity6, histTagChange.Value);
					}
				}
				else if (histTagChange.Tag == 318)
				{
					HistoryManager.Get().NotifyEntityAffected(entity6, allowDuplicates: false, fromMetaData: false);
				}
				else if (histTagChange.Tag == 385 && list != null && list.Contains(histTagChange.Value))
				{
					HistoryManager.Get().NotifyEntityAffected(entity6, allowDuplicates: false, fromMetaData: false);
				}
				else if (histTagChange.Tag == 262)
				{
					HistoryManager.Get().NotifyEntityAffected(entity6, allowDuplicates: false, fromMetaData: false);
				}
				if (GameUtils.IsHistoryDeathTagChange(histTagChange))
				{
					HistoryManager.Get().NotifyEntityDied(entity6);
					list2.Add(histTagChange.Entity);
				}
				if (GameUtils.IsHistoryMovedToSetAsideTagChange(histTagChange))
				{
					list3.Add(histTagChange.Entity);
				}
				if (GameUtils.IsHistoryDiscardTagChange(histTagChange))
				{
					HistoryManager.Get().NotifyEntityAffected(entity6, allowDuplicates: false, fromMetaData: false);
				}
			}
		}
	}

	public bool ShouldCreatePlayBlockHistoryTile()
	{
		if (HistoryManager.Get() == null || !HistoryManager.Get().IsHistoryEnabled())
		{
			return false;
		}
		if (!IsPlayBlock())
		{
			return false;
		}
		PowerTaskList parent = GetParent();
		if (parent == null)
		{
			return true;
		}
		Entity sourceEntity = parent.GetSourceEntity();
		if (sourceEntity != null && sourceEntity.HasTag(GAME_TAG.CAST_RANDOM_SPELLS))
		{
			return false;
		}
		return true;
	}

	public void SetActivateBattlecrySpellState()
	{
		PowerTaskList parent = GetParent();
		if (parent != null && parent.IsPlayBlock())
		{
			Network.HistBlockStart blockStart = GetBlockStart();
			if (blockStart != null)
			{
				parent.m_lastBattlecryEffectIndex = blockStart.EffectIndex;
			}
		}
	}

	public bool ShouldActivateBattlecrySpell()
	{
		if (!IsOrigin())
		{
			return false;
		}
		PowerTaskList parent = GetParent();
		if (parent == null)
		{
			return false;
		}
		if (!parent.IsPlayBlock())
		{
			return false;
		}
		Network.HistBlockStart blockStart = GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		if (parent.m_lastBattlecryEffectIndex.HasValue && parent.m_lastBattlecryEffectIndex != blockStart.EffectIndex)
		{
			return false;
		}
		return true;
	}

	public void DebugDump()
	{
		DebugDump(Log.Power);
	}

	public void DebugDump(Logger logger)
	{
		if (!logger.CanPrint())
		{
			return;
		}
		GameState gameState = GameState.Get();
		string indentation = string.Empty;
		int num = ((m_parent != null) ? m_parent.GetId() : 0);
		int num2 = ((m_previous != null) ? m_previous.GetId() : 0);
		logger.Print("PowerTaskList.DebugDump() - ID={0} ParentID={1} PreviousID={2} TaskCount={3}", m_id, num, num2, m_tasks.Count);
		if (m_blockStart == null)
		{
			logger.Print("PowerTaskList.DebugDump() - {0}Block Start=(null)", indentation);
			indentation += "    ";
		}
		else
		{
			gameState.DebugPrintPower(logger, "PowerTaskList", m_blockStart, ref indentation);
		}
		for (int i = 0; i < m_tasks.Count; i++)
		{
			Network.PowerHistory power = m_tasks[i].GetPower();
			gameState.DebugPrintPower(logger, "PowerTaskList", power, ref indentation);
		}
		if (m_blockEnd == null)
		{
			if (indentation.Length >= "    ".Length)
			{
				indentation = indentation.Remove(indentation.Length - "    ".Length);
			}
			logger.Print("PowerTaskList.DebugDump() - {0}Block End=(null)", indentation);
		}
		else
		{
			gameState.DebugPrintPower(logger, "PowerTaskList", m_blockEnd, ref indentation);
		}
	}

	public override string ToString()
	{
		return $"id={m_id} tasks={m_tasks.Count} prevId={((m_previous != null) ? m_previous.GetId() : 0)} nextId={((m_next != null) ? m_next.GetId() : 0)} parentId={((m_parent != null) ? m_parent.GetId() : 0)}";
	}

	private void OnZoneChangeComplete(ZoneChangeList changeList, object userData)
	{
		ZoneChangeCallbackData zoneChangeCallbackData = (ZoneChangeCallbackData)userData;
		if (zoneChangeCallbackData.m_taskListCallback != null)
		{
			zoneChangeCallbackData.m_taskListCallback(this, zoneChangeCallbackData.m_startIndex, zoneChangeCallbackData.m_count, zoneChangeCallbackData.m_taskListUserData);
		}
	}

	private void OnTaskListCompleted()
	{
		SetProcessEndTime();
	}

	private IEnumerator WaitForGameStateAndDoTasks(int incompleteStartIndex, int endIndex, int startIndex, int count, CompleteCallback callback, object userData)
	{
		int i = incompleteStartIndex;
		while (i <= endIndex)
		{
			PowerTask task = m_tasks[i];
			while (!GameState.Get().GetPowerProcessor().CanDoTask(task))
			{
				yield return null;
			}
			task.DoTask();
			while (GameState.Get().IsMulliganBusy())
			{
				yield return null;
			}
			int num = i + 1;
			i = num;
		}
		callback?.Invoke(this, startIndex, count, userData);
	}

	private void DoTasks(int incompleteStartIndex, int endIndex, int startIndex, int count, CompleteCallback callback, object userData)
	{
		for (int i = incompleteStartIndex; i <= endIndex; i++)
		{
			m_tasks[i].DoTask();
		}
		callback?.Invoke(this, startIndex, count, userData);
	}

	private void BuildAttackData()
	{
		if (!m_attackDataBuilt)
		{
			m_attackInfo = BuildAttackInfo();
			m_attackType = DetermineAttackType(out var info);
			m_attacker = null;
			m_defender = null;
			m_proposedDefender = null;
			switch (m_attackType)
			{
			case AttackType.REGULAR:
				m_attacker = info.m_attacker;
				m_defender = info.m_defender;
				break;
			case AttackType.PROPOSED:
				m_attacker = info.m_proposedAttacker;
				m_defender = info.m_proposedDefender;
				m_proposedDefender = info.m_proposedDefender;
				m_repeatProposed = info.m_repeatProposed;
				break;
			case AttackType.CANCELED:
				m_attacker = m_previous.GetAttacker();
				m_proposedDefender = m_previous.GetProposedDefender();
				break;
			case AttackType.ONLY_ATTACKER:
				m_attacker = info.m_attacker;
				break;
			case AttackType.ONLY_DEFENDER:
				m_defender = info.m_defender;
				break;
			case AttackType.ONLY_PROPOSED_ATTACKER:
				m_attacker = info.m_proposedAttacker;
				break;
			case AttackType.ONLY_PROPOSED_DEFENDER:
				m_proposedDefender = info.m_proposedDefender;
				m_defender = info.m_proposedDefender;
				break;
			case AttackType.WAITING_ON_PROPOSED_ATTACKER:
			case AttackType.WAITING_ON_PROPOSED_DEFENDER:
			case AttackType.WAITING_ON_ATTACKER:
			case AttackType.WAITING_ON_DEFENDER:
				m_attacker = m_previous.GetAttacker();
				m_defender = m_previous.GetDefender();
				break;
			}
			m_attackDataBuilt = true;
		}
	}

	private AttackInfo BuildAttackInfo()
	{
		GameState gameState = GameState.Get();
		AttackInfo attackInfo = new AttackInfo();
		bool flag = false;
		foreach (PowerTask task in GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.TAG_CHANGE)
			{
				continue;
			}
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange.Tag == 36)
			{
				attackInfo.m_defenderTagValue = histTagChange.Value;
				if (histTagChange.Value == 1)
				{
					attackInfo.m_defender = gameState.GetEntity(histTagChange.Entity);
				}
				flag = true;
			}
			else if (histTagChange.Tag == 38)
			{
				attackInfo.m_attackerTagValue = histTagChange.Value;
				if (histTagChange.Value == 1)
				{
					attackInfo.m_attacker = gameState.GetEntity(histTagChange.Entity);
				}
				flag = true;
			}
			else if (histTagChange.Tag == 39)
			{
				attackInfo.m_proposedAttackerTagValue = histTagChange.Value;
				if (histTagChange.Value != 0)
				{
					attackInfo.m_proposedAttacker = gameState.GetEntity(histTagChange.Value);
				}
				flag = true;
			}
			else if (histTagChange.Tag == 37)
			{
				attackInfo.m_proposedDefenderTagValue = histTagChange.Value;
				if (histTagChange.Value != 0)
				{
					attackInfo.m_proposedDefender = gameState.GetEntity(histTagChange.Value);
				}
				flag = true;
			}
		}
		if (flag)
		{
			return attackInfo;
		}
		return null;
	}

	private AttackType DetermineAttackType(out AttackInfo info)
	{
		info = m_attackInfo;
		GameState gameState = GameState.Get();
		GameEntity gameEntity = gameState.GetGameEntity();
		Entity entity = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_ATTACKER));
		Entity entity2 = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_DEFENDER));
		AttackType attackType = AttackType.INVALID;
		Entity entity3 = null;
		Entity entity4 = null;
		if (m_previous != null)
		{
			attackType = m_previous.GetAttackType();
			entity3 = m_previous.GetAttacker();
			entity4 = m_previous.GetDefender();
		}
		if (m_attackInfo != null)
		{
			if (m_attackInfo.m_attacker != null || m_attackInfo.m_defender != null)
			{
				if (m_attackInfo.m_attacker == null)
				{
					if (attackType == AttackType.ONLY_ATTACKER || attackType == AttackType.WAITING_ON_DEFENDER)
					{
						info = new AttackInfo();
						info.m_attacker = entity3;
						info.m_defender = m_attackInfo.m_defender;
						return AttackType.REGULAR;
					}
					return AttackType.ONLY_DEFENDER;
				}
				if (m_attackInfo.m_defender == null)
				{
					if (attackType == AttackType.ONLY_DEFENDER || attackType == AttackType.WAITING_ON_ATTACKER)
					{
						info = new AttackInfo();
						info.m_attacker = m_attackInfo.m_attacker;
						info.m_defender = entity4;
						return AttackType.REGULAR;
					}
					return AttackType.ONLY_ATTACKER;
				}
				return AttackType.REGULAR;
			}
			if (m_attackInfo.m_proposedAttacker != null || m_attackInfo.m_proposedDefender != null)
			{
				if (m_attackInfo.m_proposedAttacker == null)
				{
					if (entity != null)
					{
						info = new AttackInfo();
						info.m_proposedAttacker = entity;
						info.m_proposedDefender = m_attackInfo.m_proposedDefender;
						return AttackType.PROPOSED;
					}
					return AttackType.ONLY_PROPOSED_DEFENDER;
				}
				if (m_attackInfo.m_proposedDefender == null)
				{
					if (entity2 != null)
					{
						info = new AttackInfo();
						info.m_proposedAttacker = m_attackInfo.m_proposedAttacker;
						info.m_proposedDefender = entity2;
						return AttackType.PROPOSED;
					}
					return AttackType.ONLY_PROPOSED_ATTACKER;
				}
				return AttackType.PROPOSED;
			}
			if (attackType == AttackType.REGULAR || attackType == AttackType.INVALID)
			{
				return AttackType.INVALID;
			}
		}
		switch (attackType)
		{
		case AttackType.PROPOSED:
			if ((entity != null && entity.GetZone() != TAG_ZONE.PLAY) || (entity2 != null && entity2.GetZone() != TAG_ZONE.PLAY) || (entity != null && entity.IsDormant()) || (entity2 != null && entity2.IsDormant()))
			{
				return AttackType.CANCELED;
			}
			if (entity3 != entity || entity4 != entity2)
			{
				info = new AttackInfo();
				info.m_proposedAttacker = entity;
				info.m_proposedDefender = entity2;
				return AttackType.PROPOSED;
			}
			if (entity != null && entity2 != null && !IsEndOfBlock())
			{
				info = new AttackInfo();
				info.m_proposedAttacker = entity;
				info.m_proposedDefender = entity2;
				info.m_repeatProposed = true;
				return AttackType.PROPOSED;
			}
			return AttackType.CANCELED;
		case AttackType.CANCELED:
			return AttackType.INVALID;
		default:
			if (IsEndOfBlock())
			{
				if (attackType == AttackType.ONLY_ATTACKER || attackType == AttackType.WAITING_ON_DEFENDER)
				{
					return AttackType.CANCELED;
				}
				Debug.LogWarningFormat("AttackSpellController.DetermineAttackType() - INVALID ATTACK prevAttackType={0} prevAttacker={1} prevDefender={2}", attackType, entity3, entity4);
				return AttackType.INVALID;
			}
			switch (attackType)
			{
			case AttackType.ONLY_PROPOSED_ATTACKER:
			case AttackType.WAITING_ON_PROPOSED_DEFENDER:
				return AttackType.WAITING_ON_PROPOSED_DEFENDER;
			case AttackType.ONLY_PROPOSED_DEFENDER:
			case AttackType.WAITING_ON_PROPOSED_ATTACKER:
				return AttackType.WAITING_ON_PROPOSED_ATTACKER;
			case AttackType.ONLY_ATTACKER:
			case AttackType.WAITING_ON_DEFENDER:
				return AttackType.WAITING_ON_DEFENDER;
			case AttackType.ONLY_DEFENDER:
			case AttackType.WAITING_ON_ATTACKER:
				return AttackType.WAITING_ON_ATTACKER;
			default:
				return AttackType.INVALID;
			}
		}
	}

	public void FixupLastTagChangeForEntityTag(int changeEntity, int changeTag, int newValue, bool fixLast = true)
	{
		if (fixLast)
		{
			for (int num = m_tasks.Count - 1; num >= 0; num--)
			{
				Network.HistTagChange histTagChange = m_tasks[num].GetPower() as Network.HistTagChange;
				if (histTagChange != null && changeEntity == histTagChange.Entity && changeTag == histTagChange.Tag)
				{
					histTagChange.Value = newValue;
					break;
				}
			}
			return;
		}
		for (int i = 0; i < m_tasks.Count; i++)
		{
			Network.HistTagChange histTagChange2 = m_tasks[i].GetPower() as Network.HistTagChange;
			if (histTagChange2 != null && changeEntity == histTagChange2.Entity && changeTag == histTagChange2.Tag)
			{
				histTagChange2.Value = newValue;
				break;
			}
		}
	}
}
