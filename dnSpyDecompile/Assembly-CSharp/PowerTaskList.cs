using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class PowerTaskList
{
	// Token: 0x06002F8F RID: 12175 RVA: 0x000F3891 File Offset: 0x000F1A91
	public int GetId()
	{
		return this.m_id;
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x000F3899 File Offset: 0x000F1A99
	public void SetId(int id)
	{
		this.m_id = id;
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x000F38A2 File Offset: 0x000F1AA2
	public int GetDeferredSourceId()
	{
		return this.m_deferredSourceId;
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x000F38AA File Offset: 0x000F1AAA
	public void SetDeferredSourceId(int id)
	{
		this.m_deferredSourceId = id;
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x000F38B3 File Offset: 0x000F1AB3
	public void AddTasks(PowerTaskList otherTaskList)
	{
		this.m_tasks.AddRange(otherTaskList.m_tasks);
		this.m_pendingTasks += otherTaskList.m_pendingTasks;
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x000F38D9 File Offset: 0x000F1AD9
	public void SetProcessStartTime()
	{
		this.m_taskListStartTime = Time.realtimeSinceStartup;
		GameState.Get().GetPowerProcessor().HandleTimelineStartEvent(this.m_id, this.m_taskListStartTime, this.m_isHistoryBlockStart, this.GetBlockStart());
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x000F390D File Offset: 0x000F1B0D
	public void SetProcessEndTime()
	{
		this.m_taskListEndTime = Time.realtimeSinceStartup;
		GameState.Get().GetPowerProcessor().HandleTimelineEndEvent(this.m_id, this.m_taskListEndTime, this.m_isHistoryBlockEnd);
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x000F393B File Offset: 0x000F1B3B
	public void SetDeferrable(bool deferrable)
	{
		this.m_isDeferrable = deferrable;
	}

	// Token: 0x06002F97 RID: 12183 RVA: 0x000F3944 File Offset: 0x000F1B44
	public bool IsDeferrable()
	{
		return this.m_isDeferrable;
	}

	// Token: 0x06002F98 RID: 12184 RVA: 0x000F394C File Offset: 0x000F1B4C
	public void SetBatchable(bool batchable)
	{
		this.m_isBatchable = batchable;
	}

	// Token: 0x06002F99 RID: 12185 RVA: 0x000F3955 File Offset: 0x000F1B55
	public bool IsBatchable()
	{
		return this.m_isBatchable && this.m_blockStart != null && this.m_blockEnd != null;
	}

	// Token: 0x06002F9A RID: 12186 RVA: 0x000F3974 File Offset: 0x000F1B74
	public bool IsCollapsible(bool isEarlier)
	{
		if (isEarlier && !this.m_collapsible)
		{
			return false;
		}
		bool flag = false;
		if (this.m_tasks.Count > 0)
		{
			PowerTask powerTask = this.m_tasks[this.m_tasks.Count - 1];
			if (powerTask.GetPower() is Network.HistMetaData)
			{
				flag = (((Network.HistMetaData)powerTask.GetPower()).MetaType == HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT);
			}
		}
		return (this.m_subSpellStart == null || isEarlier) && (this.m_subSpellEnd == null || !isEarlier) && (!this.m_isHistoryBlockStart || isEarlier) && (!this.m_isHistoryBlockEnd || !isEarlier) && !flag;
	}

	// Token: 0x06002F9B RID: 12187 RVA: 0x000F3A12 File Offset: 0x000F1C12
	public void SetCollapsible(bool collapsible)
	{
		this.m_collapsible = collapsible;
	}

	// Token: 0x06002F9C RID: 12188 RVA: 0x000F3A1C File Offset: 0x000F1C1C
	public bool IsSlushTimeHelper()
	{
		return this.m_tasks.Count == 1 && this.m_tasks[0].GetPower() is Network.HistMetaData && ((Network.HistMetaData)this.m_tasks[0].GetPower()).MetaType == HistoryMeta.Type.SLUSH_TIME;
	}

	// Token: 0x06002F9D RID: 12189 RVA: 0x000F3A72 File Offset: 0x000F1C72
	public bool HasAnyTasksInImmediate()
	{
		return this.m_tasks.Count > 0;
	}

	// Token: 0x06002F9E RID: 12190 RVA: 0x000F3A82 File Offset: 0x000F1C82
	public void SetHistoryBlockStart(bool isStart)
	{
		this.m_isHistoryBlockStart = isStart;
	}

	// Token: 0x06002F9F RID: 12191 RVA: 0x000F3A8B File Offset: 0x000F1C8B
	public void SetHistoryBlockEnd(bool isEnd)
	{
		this.m_isHistoryBlockEnd = isEnd;
	}

	// Token: 0x06002FA0 RID: 12192 RVA: 0x000F3A94 File Offset: 0x000F1C94
	public void OnTaskCompleted()
	{
		int num = this.m_pendingTasks - 1;
		this.m_pendingTasks = num;
		if (num == 0)
		{
			this.OnTaskListCompleted();
		}
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x000F3ABC File Offset: 0x000F1CBC
	public bool IsEmpty()
	{
		PowerTaskList origin = this.GetOrigin();
		return origin.m_blockStart == null && origin.m_blockEnd == null && origin.m_tasks.Count <= 0;
	}

	// Token: 0x06002FA2 RID: 12194 RVA: 0x000F3AF5 File Offset: 0x000F1CF5
	public bool IsOrigin()
	{
		return this.m_previous == null;
	}

	// Token: 0x06002FA3 RID: 12195 RVA: 0x000F3B00 File Offset: 0x000F1D00
	public void FillMetaDataTargetSourceData()
	{
		foreach (PowerTask powerTask in this.m_tasks)
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Data == 0)
				{
					global::Entity sourceEntity = this.GetSourceEntity(true);
					histMetaData.Data = ((sourceEntity == null) ? 0 : sourceEntity.GetEntityId());
				}
			}
		}
	}

	// Token: 0x06002FA4 RID: 12196 RVA: 0x000F3B8C File Offset: 0x000F1D8C
	public PowerTaskList GetOrigin()
	{
		PowerTaskList powerTaskList = this;
		while (powerTaskList.m_previous != null)
		{
			powerTaskList = powerTaskList.m_previous;
		}
		return powerTaskList;
	}

	// Token: 0x06002FA5 RID: 12197 RVA: 0x000F3BAD File Offset: 0x000F1DAD
	public PowerTaskList GetPrevious()
	{
		return this.m_previous;
	}

	// Token: 0x06002FA6 RID: 12198 RVA: 0x000F3BB5 File Offset: 0x000F1DB5
	public void SetPrevious(PowerTaskList taskList)
	{
		this.m_previous = taskList;
		taskList.m_next = this;
	}

	// Token: 0x06002FA7 RID: 12199 RVA: 0x000F3BC5 File Offset: 0x000F1DC5
	public PowerTaskList GetNext()
	{
		return this.m_next;
	}

	// Token: 0x06002FA8 RID: 12200 RVA: 0x000F3BCD File Offset: 0x000F1DCD
	public void SetNext(PowerTaskList next)
	{
		this.m_next = next;
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x000F3BD8 File Offset: 0x000F1DD8
	public PowerTaskList GetLast()
	{
		PowerTaskList powerTaskList = this;
		while (powerTaskList.m_next != null)
		{
			powerTaskList = powerTaskList.m_next;
		}
		return powerTaskList;
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x000F3BF9 File Offset: 0x000F1DF9
	public Network.HistBlockStart GetBlockStart()
	{
		return this.GetOrigin().m_blockStart;
	}

	// Token: 0x06002FAB RID: 12203 RVA: 0x000F3C06 File Offset: 0x000F1E06
	public void SetBlockStart(Network.HistBlockStart blockStart)
	{
		this.m_blockStart = blockStart;
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x000F3C0F File Offset: 0x000F1E0F
	public Network.HistBlockEnd GetBlockEnd()
	{
		return this.m_blockEnd;
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x000F3C17 File Offset: 0x000F1E17
	public void SetBlockEnd(Network.HistBlockEnd blockEnd)
	{
		this.m_blockEnd = blockEnd;
	}

	// Token: 0x06002FAE RID: 12206 RVA: 0x000F3C20 File Offset: 0x000F1E20
	public PowerTaskList GetParent()
	{
		return this.GetOrigin().m_parent;
	}

	// Token: 0x06002FAF RID: 12207 RVA: 0x000F3C2D File Offset: 0x000F1E2D
	public void SetParent(PowerTaskList parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x000F3C36 File Offset: 0x000F1E36
	public Network.HistSubSpellStart GetSubSpellStart()
	{
		return this.m_subSpellStart;
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x000F3C3E File Offset: 0x000F1E3E
	public void SetSubSpellStart(Network.HistSubSpellStart subSpellStart)
	{
		this.m_subSpellStart = subSpellStart;
	}

	// Token: 0x06002FB2 RID: 12210 RVA: 0x000F3C47 File Offset: 0x000F1E47
	public void SetVoSpellStart(Network.HistVoSpell voSpell)
	{
		this.m_voSpell = voSpell;
	}

	// Token: 0x06002FB3 RID: 12211 RVA: 0x000F3C50 File Offset: 0x000F1E50
	public Network.HistVoSpell GetVoSpell()
	{
		return this.m_voSpell;
	}

	// Token: 0x06002FB4 RID: 12212 RVA: 0x000F3C58 File Offset: 0x000F1E58
	public Network.HistSubSpellEnd GetSubSpellEnd()
	{
		return this.m_subSpellEnd;
	}

	// Token: 0x06002FB5 RID: 12213 RVA: 0x000F3C60 File Offset: 0x000F1E60
	public void SetSubSpellEnd(Network.HistSubSpellEnd subSpellEnd)
	{
		this.m_subSpellEnd = subSpellEnd;
	}

	// Token: 0x06002FB6 RID: 12214 RVA: 0x000F3C69 File Offset: 0x000F1E69
	public PowerTaskList GetSubSpellOrigin()
	{
		return this.m_subSpellOrigin;
	}

	// Token: 0x06002FB7 RID: 12215 RVA: 0x000F3C71 File Offset: 0x000F1E71
	public void SetSubSpellOrigin(PowerTaskList taskList)
	{
		this.m_subSpellOrigin = taskList;
	}

	// Token: 0x06002FB8 RID: 12216 RVA: 0x000F3C7A File Offset: 0x000F1E7A
	public bool IsBlock()
	{
		return this.GetOrigin().m_blockStart != null;
	}

	// Token: 0x06002FB9 RID: 12217 RVA: 0x000F3C8A File Offset: 0x000F1E8A
	public bool IsStartOfBlock()
	{
		return this.IsBlock() && this.m_blockStart != null;
	}

	// Token: 0x06002FBA RID: 12218 RVA: 0x000F3C9F File Offset: 0x000F1E9F
	public bool IsEndOfBlock()
	{
		return this.IsBlock() && this.m_blockEnd != null;
	}

	// Token: 0x06002FBB RID: 12219 RVA: 0x000F3CB4 File Offset: 0x000F1EB4
	public bool DoesBlockHaveEndAction()
	{
		return this.GetLast().m_blockEnd != null;
	}

	// Token: 0x06002FBC RID: 12220 RVA: 0x000F3CC4 File Offset: 0x000F1EC4
	public bool IsBlockUnended()
	{
		return this.IsBlock() && !this.DoesBlockHaveEndAction();
	}

	// Token: 0x06002FBD RID: 12221 RVA: 0x000F3CDC File Offset: 0x000F1EDC
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

	// Token: 0x06002FBE RID: 12222 RVA: 0x000F3D08 File Offset: 0x000F1F08
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

	// Token: 0x06002FBF RID: 12223 RVA: 0x000F3D34 File Offset: 0x000F1F34
	public bool IsInBlock(PowerTaskList taskList)
	{
		return this == taskList || this.IsEarlierInBlockThan(taskList) || this.IsLaterInBlockThan(taskList);
	}

	// Token: 0x06002FC0 RID: 12224 RVA: 0x000F3D54 File Offset: 0x000F1F54
	public bool IsDescendantOfBlock(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return false;
		}
		if (this.IsInBlock(taskList))
		{
			return true;
		}
		PowerTaskList origin = taskList.GetOrigin();
		for (PowerTaskList parent = this.GetParent(); parent != null; parent = parent.m_parent)
		{
			if (parent == origin)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FC1 RID: 12225 RVA: 0x000F3D92 File Offset: 0x000F1F92
	public List<PowerTask> GetTaskList()
	{
		return this.m_tasks;
	}

	// Token: 0x06002FC2 RID: 12226 RVA: 0x000F3A72 File Offset: 0x000F1C72
	public bool HasTasks()
	{
		return this.m_tasks.Count > 0;
	}

	// Token: 0x06002FC3 RID: 12227 RVA: 0x000F3D9C File Offset: 0x000F1F9C
	public PowerTask CreateTask(Network.PowerHistory netPower)
	{
		this.m_pendingTasks++;
		PowerTask powerTask = new PowerTask();
		powerTask.SetPower(netPower);
		powerTask.SetTaskCompleteCallback(new PowerTask.TaskCompleteCallback(this.OnTaskCompleted));
		this.m_tasks.Add(powerTask);
		return powerTask;
	}

	// Token: 0x06002FC4 RID: 12228 RVA: 0x000F3DE4 File Offset: 0x000F1FE4
	public bool HasTasksOfType(Network.PowerType powType)
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetPower().Type == powType)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002FC5 RID: 12229 RVA: 0x000F3E44 File Offset: 0x000F2044
	public global::Entity GetSourceEntity(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				int num = enumerator.Current;
				global::Entity entity = GameState.Get().GetEntity(num);
				if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(num))
				{
					string format = string.Format("PowerProcessor.GetSourceEntity() - task list {0} has a source entity with id {1} but there is no entity with that id", this.m_id, num);
					Log.Power.PrintWarning(format, Array.Empty<object>());
					return null;
				}
				return entity;
			}
		}
		return null;
	}

	// Token: 0x06002FC6 RID: 12230 RVA: 0x000F3EFC File Offset: 0x000F20FC
	public List<global::Entity> GetSourceEntities(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		List<int> entities = blockStart.Entities;
		List<global::Entity> list = new List<global::Entity>();
		foreach (int num in entities)
		{
			global::Entity entity = GameState.Get().GetEntity(num);
			if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(num))
			{
				string format = string.Format("PowerProcessor.GetSourceEntity() - task list {0} has a source entity with id {1} but there is no entity with that id", this.m_id, num);
				Log.Power.PrintWarning(format, Array.Empty<object>());
				return null;
			}
			list.Add(entity);
		}
		return list;
	}

	// Token: 0x06002FC7 RID: 12231 RVA: 0x000F3FBC File Offset: 0x000F21BC
	public bool IsEffectCardIdClientCached(int entityId)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		int num = 0;
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == entityId)
				{
					break;
				}
				num++;
			}
		}
		return num < blockStart.IsEffectCardIdClientCached.Count && blockStart.IsEffectCardIdClientCached[num];
	}

	// Token: 0x06002FC8 RID: 12232 RVA: 0x000F403C File Offset: 0x000F223C
	public string GetEffectCardId(int entityId)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		int num = 0;
		using (List<int>.Enumerator enumerator = blockStart.Entities.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == entityId)
				{
					break;
				}
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
		global::Entity sourceEntity = this.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			return null;
		}
		return sourceEntity.GetCardId();
	}

	// Token: 0x06002FC9 RID: 12233 RVA: 0x000F40DC File Offset: 0x000F22DC
	public EntityDef GetEffectEntityDef(int entityId)
	{
		string effectCardId = this.GetEffectCardId(entityId);
		if (string.IsNullOrEmpty(effectCardId))
		{
			return null;
		}
		return DefLoader.Get().GetEntityDef(effectCardId);
	}

	// Token: 0x06002FCA RID: 12234 RVA: 0x000F4108 File Offset: 0x000F2308
	public string GetEffectCardId()
	{
		global::Entity sourceEntity = this.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			return null;
		}
		return this.GetEffectCardId(sourceEntity.GetEntityId());
	}

	// Token: 0x06002FCB RID: 12235 RVA: 0x000F4130 File Offset: 0x000F2330
	public EntityDef GetEffectEntityDef()
	{
		global::Entity sourceEntity = this.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			return null;
		}
		return this.GetEffectEntityDef(sourceEntity.GetEntityId());
	}

	// Token: 0x06002FCC RID: 12236 RVA: 0x000F4158 File Offset: 0x000F2358
	public global::Entity GetTargetEntity(bool warnIfNull = true)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return null;
		}
		int target = blockStart.Target;
		global::Entity entity = GameState.Get().GetEntity(target);
		if (entity == null && warnIfNull && !GameState.Get().EntityRemovedFromGame(target))
		{
			string format = string.Format("PowerProcessor.GetTargetEntity() - task list {0} has a target entity with id {1} but there is no entity with that id", this.m_id, target);
			Log.Power.PrintWarning(format, Array.Empty<object>());
			return null;
		}
		return entity;
	}

	// Token: 0x06002FCD RID: 12237 RVA: 0x000F41C8 File Offset: 0x000F23C8
	public bool HasTargetEntity()
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		int target = blockStart.Target;
		return GameState.Get().GetEntity(target) != null;
	}

	// Token: 0x06002FCE RID: 12238 RVA: 0x000F41F8 File Offset: 0x000F23F8
	public bool HasMetaDataTasks()
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetPower().Type == Network.PowerType.META_DATA)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002FCF RID: 12239 RVA: 0x000F4258 File Offset: 0x000F2458
	public bool DoesBlockHaveMetaDataTasks()
	{
		for (PowerTaskList powerTaskList = this.GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.HasMetaDataTasks())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FD0 RID: 12240 RVA: 0x000F4284 File Offset: 0x000F2484
	public bool HasCardDraw()
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsCardDraw())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002FD1 RID: 12241 RVA: 0x000F42E0 File Offset: 0x000F24E0
	public bool HasCardMill()
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsCardMill())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002FD2 RID: 12242 RVA: 0x000F433C File Offset: 0x000F253C
	public bool HasFatigue()
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsFatigue())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002FD3 RID: 12243 RVA: 0x000F4398 File Offset: 0x000F2598
	public int GetTotalSlushTime()
	{
		if (this.m_taskListSlushTimeMilliseconds > -1)
		{
			return this.m_taskListSlushTimeMilliseconds;
		}
		int num = 0;
		foreach (PowerTask powerTask in this.m_tasks)
		{
			Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.SLUSH_TIME)
			{
				num += histMetaData.Data;
			}
		}
		this.m_taskListSlushTimeMilliseconds = num;
		return num;
	}

	// Token: 0x06002FD4 RID: 12244 RVA: 0x000F4420 File Offset: 0x000F2620
	public bool HasEffectTimingMetaData()
	{
		foreach (PowerTask powerTask in this.m_tasks)
		{
			Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
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

	// Token: 0x06002FD5 RID: 12245 RVA: 0x000F4498 File Offset: 0x000F2698
	public List<PowerTask> GetTagChangeTasks()
	{
		List<PowerTask> list = new List<PowerTask>();
		foreach (PowerTask powerTask in this.m_tasks)
		{
			if (powerTask.GetPower() is Network.HistTagChange)
			{
				list.Add(powerTask);
			}
		}
		return list;
	}

	// Token: 0x06002FD6 RID: 12246 RVA: 0x000F4500 File Offset: 0x000F2700
	public bool DoesBlockHaveEffectTimingMetaData()
	{
		for (PowerTaskList powerTaskList = this.GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.GetSubSpellOrigin() == this.GetSubSpellOrigin() && powerTaskList.HasEffectTimingMetaData())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FD7 RID: 12247 RVA: 0x000F453C File Offset: 0x000F273C
	public HistoryBlock.Type GetBlockType()
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return HistoryBlock.Type.INVALID;
		}
		return blockStart.BlockType;
	}

	// Token: 0x06002FD8 RID: 12248 RVA: 0x000F455C File Offset: 0x000F275C
	public bool IsBlockType(HistoryBlock.Type type)
	{
		Network.HistBlockStart blockStart = this.GetBlockStart();
		return blockStart != null && blockStart.BlockType == type;
	}

	// Token: 0x06002FD9 RID: 12249 RVA: 0x000F457E File Offset: 0x000F277E
	public bool IsPlayBlock()
	{
		return this.IsBlockType(HistoryBlock.Type.PLAY);
	}

	// Token: 0x06002FDA RID: 12250 RVA: 0x000F4587 File Offset: 0x000F2787
	public bool IsPowerBlock()
	{
		return this.IsBlockType(HistoryBlock.Type.POWER);
	}

	// Token: 0x06002FDB RID: 12251 RVA: 0x000F4590 File Offset: 0x000F2790
	public bool IsTriggerBlock()
	{
		return this.IsBlockType(HistoryBlock.Type.TRIGGER);
	}

	// Token: 0x06002FDC RID: 12252 RVA: 0x000F4599 File Offset: 0x000F2799
	public bool IsDeathBlock()
	{
		return this.IsBlockType(HistoryBlock.Type.DEATHS);
	}

	// Token: 0x06002FDD RID: 12253 RVA: 0x000F45A2 File Offset: 0x000F27A2
	public bool IsRitualBlock()
	{
		return this.IsBlockType(HistoryBlock.Type.RITUAL);
	}

	// Token: 0x06002FDE RID: 12254 RVA: 0x000F45AC File Offset: 0x000F27AC
	public bool IsSubSpellTaskList()
	{
		return this.m_subSpellOrigin != null;
	}

	// Token: 0x06002FDF RID: 12255 RVA: 0x000F45B7 File Offset: 0x000F27B7
	public void DoTasks(int startIndex, int count)
	{
		this.DoTasks(startIndex, count, null, null);
	}

	// Token: 0x06002FE0 RID: 12256 RVA: 0x000F45C3 File Offset: 0x000F27C3
	public void DoTasks(int startIndex, int count, PowerTaskList.CompleteCallback callback)
	{
		this.DoTasks(startIndex, count, callback, null);
	}

	// Token: 0x06002FE1 RID: 12257 RVA: 0x000F45D0 File Offset: 0x000F27D0
	public void DoTasks(int startIndex, int count, PowerTaskList.CompleteCallback callback, object userData)
	{
		bool flag = false;
		int num = -1;
		int num2 = Mathf.Min(startIndex + count - 1, this.m_tasks.Count - 1);
		for (int i = startIndex; i <= num2; i++)
		{
			PowerTask powerTask = this.m_tasks[i];
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
			PowerTaskList.ZoneChangeCallbackData zoneChangeCallbackData = new PowerTaskList.ZoneChangeCallbackData();
			zoneChangeCallbackData.m_startIndex = startIndex;
			zoneChangeCallbackData.m_count = count;
			zoneChangeCallbackData.m_taskListCallback = callback;
			zoneChangeCallbackData.m_taskListUserData = userData;
			this.m_zoneChangeList = ZoneMgr.Get().AddServerZoneChanges(this, num, num2, new ZoneMgr.ChangeCompleteCallback(this.OnZoneChangeComplete), zoneChangeCallbackData);
			if (this.m_zoneChangeList != null)
			{
				return;
			}
		}
		if (Gameplay.Get() != null)
		{
			Gameplay.Get().StartCoroutine(this.WaitForGameStateAndDoTasks(num, num2, startIndex, count, callback, userData));
			return;
		}
		this.DoTasks(num, num2, startIndex, count, callback, userData);
	}

	// Token: 0x06002FE2 RID: 12258 RVA: 0x000F46C2 File Offset: 0x000F28C2
	public void DoAllTasks(PowerTaskList.CompleteCallback callback, object userData)
	{
		this.DoTasks(0, this.m_tasks.Count, callback, userData);
	}

	// Token: 0x06002FE3 RID: 12259 RVA: 0x000F46D8 File Offset: 0x000F28D8
	public void DoAllTasks(PowerTaskList.CompleteCallback callback)
	{
		this.DoTasks(0, this.m_tasks.Count, callback, null);
	}

	// Token: 0x06002FE4 RID: 12260 RVA: 0x000F46EE File Offset: 0x000F28EE
	public void DoAllTasks()
	{
		this.DoTasks(0, this.m_tasks.Count, null, null);
	}

	// Token: 0x06002FE5 RID: 12261 RVA: 0x000F4704 File Offset: 0x000F2904
	public void DoEarlyConcedeTasks()
	{
		for (int i = 0; i < this.m_tasks.Count; i++)
		{
			this.m_tasks[i].DoEarlyConcedeTask();
		}
	}

	// Token: 0x06002FE6 RID: 12262 RVA: 0x000F4738 File Offset: 0x000F2938
	public bool IsComplete()
	{
		return this.AreTasksComplete() && this.AreZoneChangesComplete();
	}

	// Token: 0x06002FE7 RID: 12263 RVA: 0x000F4750 File Offset: 0x000F2950
	public bool AreTasksComplete()
	{
		using (List<PowerTask>.Enumerator enumerator = this.m_tasks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsCompleted())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06002FE8 RID: 12264 RVA: 0x000F47AC File Offset: 0x000F29AC
	public bool IsTaskPartOfMetaData(int taskIndex, HistoryMeta.Type metaType)
	{
		for (int i = taskIndex; i >= 0; i--)
		{
			Network.PowerHistory power = this.m_tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == metaType)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FE9 RID: 12265 RVA: 0x000F47F4 File Offset: 0x000F29F4
	public Card GetStartDrawMetaDataCard()
	{
		for (int i = 0; i < this.m_tasks.Count; i++)
		{
			Network.PowerHistory power = this.m_tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.START_DRAW)
				{
					global::Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
					if (entity != null)
					{
						return entity.GetCard();
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06002FEA RID: 12266 RVA: 0x000F4868 File Offset: 0x000F2A68
	public int FindEarlierIncompleteTaskIndex(int taskIndex)
	{
		for (int i = taskIndex - 1; i >= 0; i--)
		{
			if (!this.m_tasks[i].IsCompleted())
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06002FEB RID: 12267 RVA: 0x000F4899 File Offset: 0x000F2A99
	public bool HasEarlierIncompleteTask(int taskIndex)
	{
		return this.FindEarlierIncompleteTaskIndex(taskIndex) >= 0;
	}

	// Token: 0x06002FEC RID: 12268 RVA: 0x000F48A8 File Offset: 0x000F2AA8
	public bool HasZoneChanges()
	{
		return this.m_zoneChangeList != null;
	}

	// Token: 0x06002FED RID: 12269 RVA: 0x000F48B3 File Offset: 0x000F2AB3
	public bool AreZoneChangesComplete()
	{
		return this.m_zoneChangeList == null || this.m_zoneChangeList.IsComplete();
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x000F48CA File Offset: 0x000F2ACA
	public AttackInfo GetAttackInfo()
	{
		this.BuildAttackData();
		return this.m_attackInfo;
	}

	// Token: 0x06002FEF RID: 12271 RVA: 0x000F48D8 File Offset: 0x000F2AD8
	public AttackType GetAttackType()
	{
		this.BuildAttackData();
		return this.m_attackType;
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x000F48E6 File Offset: 0x000F2AE6
	public global::Entity GetAttacker()
	{
		this.BuildAttackData();
		return this.m_attacker;
	}

	// Token: 0x06002FF1 RID: 12273 RVA: 0x000F48F4 File Offset: 0x000F2AF4
	public global::Entity GetDefender()
	{
		this.BuildAttackData();
		return this.m_defender;
	}

	// Token: 0x06002FF2 RID: 12274 RVA: 0x000F4902 File Offset: 0x000F2B02
	public global::Entity GetProposedDefender()
	{
		this.BuildAttackData();
		return this.m_proposedDefender;
	}

	// Token: 0x06002FF3 RID: 12275 RVA: 0x000F4910 File Offset: 0x000F2B10
	public bool IsRepeatProposedAttack()
	{
		this.BuildAttackData();
		return this.m_repeatProposed;
	}

	// Token: 0x06002FF4 RID: 12276 RVA: 0x000F4920 File Offset: 0x000F2B20
	public bool HasGameOver()
	{
		for (int i = 0; i < this.m_tasks.Count; i++)
		{
			Network.PowerHistory power = this.m_tasks[i].GetPower();
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

	// Token: 0x06002FF5 RID: 12277 RVA: 0x000F4984 File Offset: 0x000F2B84
	public bool HasFriendlyConcede()
	{
		for (int i = 0; i < this.m_tasks.Count; i++)
		{
			Network.PowerHistory power = this.m_tasks[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE && GameUtils.IsFriendlyConcede((Network.HistTagChange)power))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FF6 RID: 12278 RVA: 0x000F49D4 File Offset: 0x000F2BD4
	public PowerTaskList.DamageInfo GetDamageInfo(global::Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		int entityId = entity.GetEntityId();
		foreach (PowerTask powerTask in this.m_tasks)
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 44 && histTagChange.Entity == entityId)
				{
					PowerTaskList.DamageInfo damageInfo = new PowerTaskList.DamageInfo();
					damageInfo.m_entity = GameState.Get().GetEntity(histTagChange.Entity);
					damageInfo.m_damage = histTagChange.Value - damageInfo.m_entity.GetDamage();
					return damageInfo;
				}
			}
		}
		return null;
	}

	// Token: 0x06002FF7 RID: 12279 RVA: 0x000F4A98 File Offset: 0x000F2C98
	public void SetWillCompleteHistoryEntry(bool set)
	{
		this.m_willCompleteHistoryEntry = set;
	}

	// Token: 0x06002FF8 RID: 12280 RVA: 0x000F4AA1 File Offset: 0x000F2CA1
	public bool WillCompleteHistoryEntry()
	{
		return this.m_willCompleteHistoryEntry;
	}

	// Token: 0x06002FF9 RID: 12281 RVA: 0x000F4AAC File Offset: 0x000F2CAC
	public bool WillBlockCompleteHistoryEntry()
	{
		for (PowerTaskList powerTaskList = this.GetOrigin(); powerTaskList != null; powerTaskList = powerTaskList.m_next)
		{
			if (powerTaskList.WillCompleteHistoryEntry())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002FFA RID: 12282 RVA: 0x000F4AD7 File Offset: 0x000F2CD7
	public global::Entity GetRitualEntityClone()
	{
		return this.m_ritualEntityClone;
	}

	// Token: 0x06002FFB RID: 12283 RVA: 0x000F4ADF File Offset: 0x000F2CDF
	public void SetRitualEntityClone(global::Entity ent)
	{
		this.m_ritualEntityClone = ent;
	}

	// Token: 0x06002FFC RID: 12284 RVA: 0x000F4AE8 File Offset: 0x000F2CE8
	public global::Entity GetInvokeEntityClone()
	{
		return this.m_invokeEntityClone;
	}

	// Token: 0x06002FFD RID: 12285 RVA: 0x000F4AF0 File Offset: 0x000F2CF0
	public void SetInvokeEntityClone(global::Entity ent)
	{
		this.m_invokeEntityClone = ent;
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x000F4AFC File Offset: 0x000F2CFC
	public bool WasThePlayedSpellCountered(global::Entity entity)
	{
		foreach (PowerTask powerTask in this.m_tasks)
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Entity == entity.GetEntityId() && histTagChange.Tag == 231 && histTagChange.Value == 1)
				{
					return true;
				}
			}
		}
		foreach (PowerTaskList powerTaskList in GameState.Get().GetPowerProcessor().GetPowerQueue().GetList())
		{
			foreach (PowerTask powerTask2 in powerTaskList.GetTaskList())
			{
				Network.PowerHistory power2 = powerTask2.GetPower();
				if (power2.Type == Network.PowerType.TAG_CHANGE)
				{
					Network.HistTagChange histTagChange2 = power2 as Network.HistTagChange;
					if (histTagChange2.Entity == entity.GetEntityId() && histTagChange2.Tag == 231 && histTagChange2.Value == 1)
					{
						return true;
					}
				}
			}
			if (powerTaskList.GetBlockEnd() != null && powerTaskList.GetBlockStart().BlockType == HistoryBlock.Type.PLAY)
			{
				return false;
			}
		}
		return false;
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x000F4C78 File Offset: 0x000F2E78
	public void CreateArtificialHistoryTilesFromMetadata()
	{
		List<PowerTask> list = new List<PowerTask>();
		bool flag = false;
		foreach (PowerTask powerTask in this.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TILE || histMetaData.MetaType == HistoryMeta.Type.BEGIN_ARTIFICIAL_HISTORY_TRIGGER_TILE)
				{
					int id = histMetaData.Info[0];
					global::Entity entity = GameState.Get().GetEntity(id);
					if (entity != null)
					{
						if (flag)
						{
							this.NotifyHistoryOfAdditionalTargets(list);
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
					this.NotifyHistoryOfAdditionalTargets(list);
					HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
					list.Clear();
				}
				else if (flag)
				{
					list.Add(powerTask);
				}
			}
			else if (flag)
			{
				list.Add(powerTask);
			}
		}
		if (flag)
		{
			this.NotifyHistoryOfAdditionalTargets(list);
			HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
		}
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x000F4DC4 File Offset: 0x000F2FC4
	public void NotifyHistoryOfAdditionalTargets(List<PowerTask> tasksToInclude = null)
	{
		if (tasksToInclude == null)
		{
			tasksToInclude = this.GetTaskList();
		}
		bool flag = false;
		Network.HistBlockStart blockStart = this.GetBlockStart();
		List<int> list = (blockStart == null) ? null : blockStart.Entities;
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		bool flag2 = true;
		foreach (PowerTask powerTask in tasksToInclude)
		{
			Network.PowerHistory power = powerTask.GetPower();
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
						HistoryManager.Get().NotifyEntityAffected(histMetaData.Info[i], false, false, false, false, false);
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
						global::Entity entity = GameState.Get().GetEntity(id);
						if (entity != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity, false, true, false, false, false);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.BURNED_CARD)
				{
					for (int k = 0; k < histMetaData.Info.Count; k++)
					{
						int id2 = histMetaData.Info[k];
						global::Entity entity2 = GameState.Get().GetEntity(id2);
						if (entity2 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity2, false, true, false, true, false);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.POISONOUS)
				{
					for (int l = 0; l < histMetaData.Info.Count; l++)
					{
						int id3 = histMetaData.Info[l];
						global::Entity entity3 = GameState.Get().GetEntity(id3);
						if (entity3 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity3, false, true, false, false, true);
						}
					}
				}
				else if (histMetaData.MetaType == HistoryMeta.Type.HISTORY_TARGET_DONT_DUPLICATE_UNTIL_END)
				{
					for (int m = 0; m < histMetaData.Info.Count; m++)
					{
						int id4 = histMetaData.Info[m];
						global::Entity entity4 = GameState.Get().GetEntity(id4);
						if (entity4 != null)
						{
							HistoryManager.Get().NotifyEntityAffected(entity4, true, true, true, false, false);
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
				global::Entity entity5 = GameState.Get().GetEntity(histShowEntity.Entity.ID);
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
				if (!flag3 && (!flag4 || !flag7) && (!flag5 || !flag7))
				{
					if (flag4 && !flag6)
					{
						HistoryManager.Get().NotifyEntityDied(histShowEntity.Entity.ID);
					}
					else
					{
						HistoryManager.Get().NotifyEntityAffected(histShowEntity.Entity.ID, false, false, false, false, false);
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
					HistoryManager.Get().NotifyEntityAffected(histFullEntity.Entity.ID, false, false, false, false, false);
				}
			}
			else if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (!histTagChange.ChangeDef)
				{
					global::Entity entity6 = GameState.Get().GetEntity(histTagChange.Entity);
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
						HistoryManager.Get().NotifyEntityAffected(entity6, false, false, false, false, false);
					}
					else if (histTagChange.Tag == 385 && list != null && list.Contains(histTagChange.Value))
					{
						HistoryManager.Get().NotifyEntityAffected(entity6, false, false, false, false, false);
					}
					else if (histTagChange.Tag == 262)
					{
						HistoryManager.Get().NotifyEntityAffected(entity6, false, false, false, false, false);
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
						HistoryManager.Get().NotifyEntityAffected(entity6, false, false, false, false, false);
					}
				}
			}
		}
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x000F54A8 File Offset: 0x000F36A8
	public bool ShouldCreatePlayBlockHistoryTile()
	{
		if (HistoryManager.Get() == null || !HistoryManager.Get().IsHistoryEnabled())
		{
			return false;
		}
		if (!this.IsPlayBlock())
		{
			return false;
		}
		PowerTaskList parent = this.GetParent();
		if (parent == null)
		{
			return true;
		}
		global::Entity sourceEntity = parent.GetSourceEntity(true);
		return sourceEntity == null || !sourceEntity.HasTag(GAME_TAG.CAST_RANDOM_SPELLS);
	}

	// Token: 0x06003002 RID: 12290 RVA: 0x000F5504 File Offset: 0x000F3704
	public void SetActivateBattlecrySpellState()
	{
		PowerTaskList parent = this.GetParent();
		if (parent == null)
		{
			return;
		}
		if (!parent.IsPlayBlock())
		{
			return;
		}
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return;
		}
		parent.m_lastBattlecryEffectIndex = new int?(blockStart.EffectIndex);
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x000F5544 File Offset: 0x000F3744
	public bool ShouldActivateBattlecrySpell()
	{
		if (!this.IsOrigin())
		{
			return false;
		}
		PowerTaskList parent = this.GetParent();
		if (parent == null)
		{
			return false;
		}
		if (!parent.IsPlayBlock())
		{
			return false;
		}
		Network.HistBlockStart blockStart = this.GetBlockStart();
		if (blockStart == null)
		{
			return false;
		}
		if (parent.m_lastBattlecryEffectIndex != null)
		{
			int? lastBattlecryEffectIndex = parent.m_lastBattlecryEffectIndex;
			int effectIndex = blockStart.EffectIndex;
			if (!(lastBattlecryEffectIndex.GetValueOrDefault() == effectIndex & lastBattlecryEffectIndex != null))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x000F55AF File Offset: 0x000F37AF
	public void DebugDump()
	{
		this.DebugDump(Log.Power);
	}

	// Token: 0x06003005 RID: 12293 RVA: 0x000F55BC File Offset: 0x000F37BC
	public void DebugDump(global::Logger logger)
	{
		if (!logger.CanPrint())
		{
			return;
		}
		GameState gameState = GameState.Get();
		string text = string.Empty;
		int num = (this.m_parent == null) ? 0 : this.m_parent.GetId();
		int num2 = (this.m_previous == null) ? 0 : this.m_previous.GetId();
		logger.Print("PowerTaskList.DebugDump() - ID={0} ParentID={1} PreviousID={2} TaskCount={3}", new object[]
		{
			this.m_id,
			num,
			num2,
			this.m_tasks.Count
		});
		if (this.m_blockStart == null)
		{
			logger.Print("PowerTaskList.DebugDump() - {0}Block Start=(null)", new object[]
			{
				text
			});
			text += "    ";
		}
		else
		{
			gameState.DebugPrintPower(logger, "PowerTaskList", this.m_blockStart, ref text);
		}
		for (int i = 0; i < this.m_tasks.Count; i++)
		{
			Network.PowerHistory power = this.m_tasks[i].GetPower();
			gameState.DebugPrintPower(logger, "PowerTaskList", power, ref text);
		}
		if (this.m_blockEnd == null)
		{
			if (text.Length >= "    ".Length)
			{
				text = text.Remove(text.Length - "    ".Length);
			}
			logger.Print("PowerTaskList.DebugDump() - {0}Block End=(null)", new object[]
			{
				text
			});
			return;
		}
		gameState.DebugPrintPower(logger, "PowerTaskList", this.m_blockEnd, ref text);
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x000F572C File Offset: 0x000F392C
	public override string ToString()
	{
		return string.Format("id={0} tasks={1} prevId={2} nextId={3} parentId={4}", new object[]
		{
			this.m_id,
			this.m_tasks.Count,
			(this.m_previous == null) ? 0 : this.m_previous.GetId(),
			(this.m_next == null) ? 0 : this.m_next.GetId(),
			(this.m_parent == null) ? 0 : this.m_parent.GetId()
		});
	}

	// Token: 0x06003007 RID: 12295 RVA: 0x000F57C4 File Offset: 0x000F39C4
	private void OnZoneChangeComplete(ZoneChangeList changeList, object userData)
	{
		PowerTaskList.ZoneChangeCallbackData zoneChangeCallbackData = (PowerTaskList.ZoneChangeCallbackData)userData;
		if (zoneChangeCallbackData.m_taskListCallback != null)
		{
			zoneChangeCallbackData.m_taskListCallback(this, zoneChangeCallbackData.m_startIndex, zoneChangeCallbackData.m_count, zoneChangeCallbackData.m_taskListUserData);
		}
	}

	// Token: 0x06003008 RID: 12296 RVA: 0x000F57FE File Offset: 0x000F39FE
	private void OnTaskListCompleted()
	{
		this.SetProcessEndTime();
	}

	// Token: 0x06003009 RID: 12297 RVA: 0x000F5806 File Offset: 0x000F3A06
	private IEnumerator WaitForGameStateAndDoTasks(int incompleteStartIndex, int endIndex, int startIndex, int count, PowerTaskList.CompleteCallback callback, object userData)
	{
		int num;
		for (int i = incompleteStartIndex; i <= endIndex; i = num)
		{
			PowerTask task = this.m_tasks[i];
			while (!GameState.Get().GetPowerProcessor().CanDoTask(task))
			{
				yield return null;
			}
			task.DoTask();
			while (GameState.Get().IsMulliganBusy())
			{
				yield return null;
			}
			task = null;
			num = i + 1;
		}
		if (callback != null)
		{
			callback(this, startIndex, count, userData);
		}
		yield break;
	}

	// Token: 0x0600300A RID: 12298 RVA: 0x000F5844 File Offset: 0x000F3A44
	private void DoTasks(int incompleteStartIndex, int endIndex, int startIndex, int count, PowerTaskList.CompleteCallback callback, object userData)
	{
		for (int i = incompleteStartIndex; i <= endIndex; i++)
		{
			this.m_tasks[i].DoTask();
		}
		if (callback != null)
		{
			callback(this, startIndex, count, userData);
		}
	}

	// Token: 0x0600300B RID: 12299 RVA: 0x000F5880 File Offset: 0x000F3A80
	private void BuildAttackData()
	{
		if (this.m_attackDataBuilt)
		{
			return;
		}
		this.m_attackInfo = this.BuildAttackInfo();
		AttackInfo attackInfo;
		this.m_attackType = this.DetermineAttackType(out attackInfo);
		this.m_attacker = null;
		this.m_defender = null;
		this.m_proposedDefender = null;
		switch (this.m_attackType)
		{
		case AttackType.REGULAR:
			this.m_attacker = attackInfo.m_attacker;
			this.m_defender = attackInfo.m_defender;
			break;
		case AttackType.PROPOSED:
			this.m_attacker = attackInfo.m_proposedAttacker;
			this.m_defender = attackInfo.m_proposedDefender;
			this.m_proposedDefender = attackInfo.m_proposedDefender;
			this.m_repeatProposed = attackInfo.m_repeatProposed;
			break;
		case AttackType.CANCELED:
			this.m_attacker = this.m_previous.GetAttacker();
			this.m_proposedDefender = this.m_previous.GetProposedDefender();
			break;
		case AttackType.ONLY_ATTACKER:
			this.m_attacker = attackInfo.m_attacker;
			break;
		case AttackType.ONLY_DEFENDER:
			this.m_defender = attackInfo.m_defender;
			break;
		case AttackType.ONLY_PROPOSED_ATTACKER:
			this.m_attacker = attackInfo.m_proposedAttacker;
			break;
		case AttackType.ONLY_PROPOSED_DEFENDER:
			this.m_proposedDefender = attackInfo.m_proposedDefender;
			this.m_defender = attackInfo.m_proposedDefender;
			break;
		case AttackType.WAITING_ON_PROPOSED_ATTACKER:
		case AttackType.WAITING_ON_PROPOSED_DEFENDER:
		case AttackType.WAITING_ON_ATTACKER:
		case AttackType.WAITING_ON_DEFENDER:
			this.m_attacker = this.m_previous.GetAttacker();
			this.m_defender = this.m_previous.GetDefender();
			break;
		}
		this.m_attackDataBuilt = true;
	}

	// Token: 0x0600300C RID: 12300 RVA: 0x000F59E8 File Offset: 0x000F3BE8
	private AttackInfo BuildAttackInfo()
	{
		GameState gameState = GameState.Get();
		AttackInfo attackInfo = new AttackInfo();
		bool flag = false;
		foreach (PowerTask powerTask in this.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (histTagChange.Tag == 36)
				{
					attackInfo.m_defenderTagValue = new int?(histTagChange.Value);
					if (histTagChange.Value == 1)
					{
						attackInfo.m_defender = gameState.GetEntity(histTagChange.Entity);
					}
					flag = true;
				}
				else if (histTagChange.Tag == 38)
				{
					attackInfo.m_attackerTagValue = new int?(histTagChange.Value);
					if (histTagChange.Value == 1)
					{
						attackInfo.m_attacker = gameState.GetEntity(histTagChange.Entity);
					}
					flag = true;
				}
				else if (histTagChange.Tag == 39)
				{
					attackInfo.m_proposedAttackerTagValue = new int?(histTagChange.Value);
					if (histTagChange.Value != 0)
					{
						attackInfo.m_proposedAttacker = gameState.GetEntity(histTagChange.Value);
					}
					flag = true;
				}
				else if (histTagChange.Tag == 37)
				{
					attackInfo.m_proposedDefenderTagValue = new int?(histTagChange.Value);
					if (histTagChange.Value != 0)
					{
						attackInfo.m_proposedDefender = gameState.GetEntity(histTagChange.Value);
					}
					flag = true;
				}
			}
		}
		if (flag)
		{
			return attackInfo;
		}
		return null;
	}

	// Token: 0x0600300D RID: 12301 RVA: 0x000F5B70 File Offset: 0x000F3D70
	private AttackType DetermineAttackType(out AttackInfo info)
	{
		info = this.m_attackInfo;
		GameState gameState = GameState.Get();
		GameEntity gameEntity = gameState.GetGameEntity();
		global::Entity entity = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_ATTACKER));
		global::Entity entity2 = gameState.GetEntity(gameEntity.GetTag(GAME_TAG.PROPOSED_DEFENDER));
		AttackType attackType = AttackType.INVALID;
		global::Entity entity3 = null;
		global::Entity entity4 = null;
		if (this.m_previous != null)
		{
			attackType = this.m_previous.GetAttackType();
			entity3 = this.m_previous.GetAttacker();
			entity4 = this.m_previous.GetDefender();
		}
		if (this.m_attackInfo != null)
		{
			if (this.m_attackInfo.m_attacker != null || this.m_attackInfo.m_defender != null)
			{
				if (this.m_attackInfo.m_attacker == null)
				{
					if (attackType == AttackType.ONLY_ATTACKER || attackType == AttackType.WAITING_ON_DEFENDER)
					{
						info = new AttackInfo();
						info.m_attacker = entity3;
						info.m_defender = this.m_attackInfo.m_defender;
						return AttackType.REGULAR;
					}
					return AttackType.ONLY_DEFENDER;
				}
				else
				{
					if (this.m_attackInfo.m_defender != null)
					{
						return AttackType.REGULAR;
					}
					if (attackType == AttackType.ONLY_DEFENDER || attackType == AttackType.WAITING_ON_ATTACKER)
					{
						info = new AttackInfo();
						info.m_attacker = this.m_attackInfo.m_attacker;
						info.m_defender = entity4;
						return AttackType.REGULAR;
					}
					return AttackType.ONLY_ATTACKER;
				}
			}
			else if (this.m_attackInfo.m_proposedAttacker != null || this.m_attackInfo.m_proposedDefender != null)
			{
				if (this.m_attackInfo.m_proposedAttacker == null)
				{
					if (entity != null)
					{
						info = new AttackInfo();
						info.m_proposedAttacker = entity;
						info.m_proposedDefender = this.m_attackInfo.m_proposedDefender;
						return AttackType.PROPOSED;
					}
					return AttackType.ONLY_PROPOSED_DEFENDER;
				}
				else
				{
					if (this.m_attackInfo.m_proposedDefender != null)
					{
						return AttackType.PROPOSED;
					}
					if (entity2 != null)
					{
						info = new AttackInfo();
						info.m_proposedAttacker = this.m_attackInfo.m_proposedAttacker;
						info.m_proposedDefender = entity2;
						return AttackType.PROPOSED;
					}
					return AttackType.ONLY_PROPOSED_ATTACKER;
				}
			}
			else if (attackType == AttackType.REGULAR || attackType == AttackType.INVALID)
			{
				return AttackType.INVALID;
			}
		}
		if (attackType == AttackType.PROPOSED)
		{
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
			if (entity != null && entity2 != null && !this.IsEndOfBlock())
			{
				info = new AttackInfo();
				info.m_proposedAttacker = entity;
				info.m_proposedDefender = entity2;
				info.m_repeatProposed = true;
				return AttackType.PROPOSED;
			}
			return AttackType.CANCELED;
		}
		else
		{
			if (attackType == AttackType.CANCELED)
			{
				return AttackType.INVALID;
			}
			if (this.IsEndOfBlock())
			{
				if (attackType == AttackType.ONLY_ATTACKER || attackType == AttackType.WAITING_ON_DEFENDER)
				{
					return AttackType.CANCELED;
				}
				Debug.LogWarningFormat("AttackSpellController.DetermineAttackType() - INVALID ATTACK prevAttackType={0} prevAttacker={1} prevDefender={2}", new object[]
				{
					attackType,
					entity3,
					entity4
				});
				return AttackType.INVALID;
			}
			else
			{
				if (attackType == AttackType.ONLY_PROPOSED_ATTACKER || attackType == AttackType.WAITING_ON_PROPOSED_DEFENDER)
				{
					return AttackType.WAITING_ON_PROPOSED_DEFENDER;
				}
				if (attackType == AttackType.ONLY_PROPOSED_DEFENDER || attackType == AttackType.WAITING_ON_PROPOSED_ATTACKER)
				{
					return AttackType.WAITING_ON_PROPOSED_ATTACKER;
				}
				if (attackType == AttackType.ONLY_ATTACKER || attackType == AttackType.WAITING_ON_DEFENDER)
				{
					return AttackType.WAITING_ON_DEFENDER;
				}
				if (attackType == AttackType.ONLY_DEFENDER || attackType == AttackType.WAITING_ON_ATTACKER)
				{
					return AttackType.WAITING_ON_ATTACKER;
				}
				return AttackType.INVALID;
			}
		}
	}

	// Token: 0x0600300E RID: 12302 RVA: 0x000F5E0C File Offset: 0x000F400C
	public void FixupLastTagChangeForEntityTag(int changeEntity, int changeTag, int newValue, bool fixLast = true)
	{
		if (fixLast)
		{
			for (int i = this.m_tasks.Count - 1; i >= 0; i--)
			{
				Network.HistTagChange histTagChange = this.m_tasks[i].GetPower() as Network.HistTagChange;
				if (histTagChange != null && changeEntity == histTagChange.Entity && changeTag == histTagChange.Tag)
				{
					histTagChange.Value = newValue;
					return;
				}
			}
			return;
		}
		for (int j = 0; j < this.m_tasks.Count; j++)
		{
			Network.HistTagChange histTagChange2 = this.m_tasks[j].GetPower() as Network.HistTagChange;
			if (histTagChange2 != null && changeEntity == histTagChange2.Entity && changeTag == histTagChange2.Tag)
			{
				histTagChange2.Value = newValue;
				return;
			}
		}
	}

	// Token: 0x04001A98 RID: 6808
	private int m_id;

	// Token: 0x04001A99 RID: 6809
	private Network.HistBlockStart m_blockStart;

	// Token: 0x04001A9A RID: 6810
	private Network.HistBlockEnd m_blockEnd;

	// Token: 0x04001A9B RID: 6811
	private List<PowerTask> m_tasks = new List<PowerTask>();

	// Token: 0x04001A9C RID: 6812
	private ZoneChangeList m_zoneChangeList;

	// Token: 0x04001A9D RID: 6813
	private int m_pendingTasks;

	// Token: 0x04001A9E RID: 6814
	private bool m_isBatchable;

	// Token: 0x04001A9F RID: 6815
	private bool m_isDeferrable;

	// Token: 0x04001AA0 RID: 6816
	private int m_deferredSourceId;

	// Token: 0x04001AA1 RID: 6817
	private PowerTaskList m_previous;

	// Token: 0x04001AA2 RID: 6818
	private PowerTaskList m_next;

	// Token: 0x04001AA3 RID: 6819
	private PowerTaskList m_subSpellOrigin;

	// Token: 0x04001AA4 RID: 6820
	private Network.HistSubSpellStart m_subSpellStart;

	// Token: 0x04001AA5 RID: 6821
	private Network.HistSubSpellEnd m_subSpellEnd;

	// Token: 0x04001AA6 RID: 6822
	private Network.HistVoSpell m_voSpell;

	// Token: 0x04001AA7 RID: 6823
	private PowerTaskList m_parent;

	// Token: 0x04001AA8 RID: 6824
	private bool m_attackDataBuilt;

	// Token: 0x04001AA9 RID: 6825
	private AttackInfo m_attackInfo;

	// Token: 0x04001AAA RID: 6826
	private AttackType m_attackType;

	// Token: 0x04001AAB RID: 6827
	private global::Entity m_attacker;

	// Token: 0x04001AAC RID: 6828
	private global::Entity m_defender;

	// Token: 0x04001AAD RID: 6829
	private global::Entity m_proposedDefender;

	// Token: 0x04001AAE RID: 6830
	private bool m_repeatProposed;

	// Token: 0x04001AAF RID: 6831
	private bool m_willCompleteHistoryEntry;

	// Token: 0x04001AB0 RID: 6832
	private global::Entity m_ritualEntityClone;

	// Token: 0x04001AB1 RID: 6833
	private global::Entity m_invokeEntityClone;

	// Token: 0x04001AB2 RID: 6834
	private int? m_lastBattlecryEffectIndex;

	// Token: 0x04001AB3 RID: 6835
	private float m_taskListStartTime;

	// Token: 0x04001AB4 RID: 6836
	private float m_taskListEndTime;

	// Token: 0x04001AB5 RID: 6837
	private int m_taskListSlushTimeMilliseconds = -1;

	// Token: 0x04001AB6 RID: 6838
	private bool m_isHistoryBlockStart;

	// Token: 0x04001AB7 RID: 6839
	private bool m_isHistoryBlockEnd;

	// Token: 0x04001AB8 RID: 6840
	private bool m_collapsible;

	// Token: 0x020016DC RID: 5852
	// (Invoke) Token: 0x0600E5DF RID: 58847
	public delegate void CompleteCallback(PowerTaskList taskList, int startIndex, int count, object userData);

	// Token: 0x020016DD RID: 5853
	public class DamageInfo
	{
		// Token: 0x0400B29D RID: 45725
		public global::Entity m_entity;

		// Token: 0x0400B29E RID: 45726
		public int m_damage;
	}

	// Token: 0x020016DE RID: 5854
	private class ZoneChangeCallbackData
	{
		// Token: 0x0400B29F RID: 45727
		public int m_startIndex;

		// Token: 0x0400B2A0 RID: 45728
		public int m_count;

		// Token: 0x0400B2A1 RID: 45729
		public PowerTaskList.CompleteCallback m_taskListCallback;

		// Token: 0x0400B2A2 RID: 45730
		public object m_taskListUserData;
	}
}
