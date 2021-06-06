using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x02000341 RID: 833
public class PowerProcessor
{
	// Token: 0x06003016 RID: 12310 RVA: 0x000F6028 File Offset: 0x000F4228
	public PowerProcessor()
	{
		this.m_deferredStack.Push(new List<PowerTaskList>());
	}

	// Token: 0x06003017 RID: 12311 RVA: 0x000F60AA File Offset: 0x000F42AA
	public void Clear()
	{
		this.m_powerQueue.Clear();
		this.m_currentTaskList = null;
	}

	// Token: 0x06003018 RID: 12312 RVA: 0x000F60BE File Offset: 0x000F42BE
	public bool IsBuildingTaskList()
	{
		return this.m_buildingTaskList;
	}

	// Token: 0x06003019 RID: 12313 RVA: 0x000F60C6 File Offset: 0x000F42C6
	public PowerTaskList GetCurrentTaskList()
	{
		return this.m_currentTaskList;
	}

	// Token: 0x0600301A RID: 12314 RVA: 0x000F60CE File Offset: 0x000F42CE
	public PowerQueue GetPowerQueue()
	{
		return this.m_powerQueue;
	}

	// Token: 0x0600301B RID: 12315 RVA: 0x000F60D6 File Offset: 0x000F42D6
	public void AddTaskEventListener(PowerProcessor.OnTaskEvent listener)
	{
		this.m_taskEventListeners.Add(listener);
	}

	// Token: 0x0600301C RID: 12316 RVA: 0x000F60E4 File Offset: 0x000F42E4
	public void RemoveTaskEventListener(PowerProcessor.OnTaskEvent listener)
	{
		this.m_taskEventListeners.Remove(listener);
	}

	// Token: 0x0600301D RID: 12317 RVA: 0x000F60F4 File Offset: 0x000F42F4
	public void FireTaskEvent(float expectedDiff)
	{
		PowerProcessor.OnTaskEvent[] array = this.m_taskEventListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](expectedDiff);
		}
	}

	// Token: 0x0600301E RID: 12318 RVA: 0x000F6124 File Offset: 0x000F4324
	public void OnMetaData(Network.HistMetaData metaData)
	{
		if (metaData.MetaType == HistoryMeta.Type.SHOW_BIG_CARD)
		{
			int data = metaData.Data;
			global::Player player = GameState.Get().GetPlayer(data);
			if (player != null && player.GetSide() != global::Player.Side.FRIENDLY && InputManager.Get().PermitDecisionMakingInput())
			{
				return;
			}
			int id = metaData.Info[0];
			global::Entity entity = GameState.Get().GetEntity(id);
			if (entity == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(entity.GetCardId()))
			{
				return;
			}
			this.SetHistoryBlockingTaskList();
			global::Entity sourceEntity = this.m_currentTaskList.GetSourceEntity(true);
			HistoryBlock.Type blockType = this.m_currentTaskList.GetBlockType();
			if (sourceEntity != null && sourceEntity.HasTag(GAME_TAG.FAST_BATTLECRY) && blockType == HistoryBlock.Type.POWER)
			{
				HistoryManager.Get().CreateFastBigCardFromMetaData(entity);
				return;
			}
			int displayTimeMS = (metaData.Info.Count > 1) ? metaData.Info[1] : 0;
			HistoryManager.Get().CreatePlayedBigCard(entity, new HistoryManager.BigCardStartedCallback(this.OnBigCardStarted), new HistoryManager.BigCardFinishedCallback(this.OnBigCardFinished), true, false, displayTimeMS);
			return;
		}
		else
		{
			if (metaData.MetaType == HistoryMeta.Type.BEGIN_LISTENING_FOR_TURN_EVENTS)
			{
				TurnStartManager.Get().BeginListeningForTurnEvents(true);
				return;
			}
			if (metaData.MetaType == HistoryMeta.Type.ARTIFICIAL_PAUSE)
			{
				int data2 = metaData.Data;
				if (Gameplay.Get() != null)
				{
					Gameplay.Get().StartCoroutine(this.ArtificiallyPausePowerProcessor((float)data2));
				}
			}
			return;
		}
	}

	// Token: 0x0600301F RID: 12319 RVA: 0x000F6268 File Offset: 0x000F4468
	public IEnumerator ArtificiallyPausePowerProcessor(float pauseTimeMS)
	{
		this.m_artificialPauseFromMetadata = true;
		float timeToWait = pauseTimeMS / 1000f;
		float timeWaited = 0f;
		if (timeToWait > 0f)
		{
			GameState.Get().GetFriendlySidePlayer().GetHandZone().UpdateLayout();
			GameState.Get().GetOpposingSidePlayer().GetHandZone().UpdateLayout();
			GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().UpdateLayout();
			GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().UpdateLayout();
		}
		while (timeWaited < timeToWait)
		{
			yield return null;
			timeWaited += Time.deltaTime;
		}
		this.m_artificialPauseFromMetadata = false;
		yield break;
	}

	// Token: 0x06003020 RID: 12320 RVA: 0x000F627E File Offset: 0x000F447E
	public bool IsHistoryBlocking()
	{
		return this.m_historyBlocking;
	}

	// Token: 0x06003021 RID: 12321 RVA: 0x000F6286 File Offset: 0x000F4486
	public PowerTaskList GetHistoryBlockingTaskList()
	{
		return this.m_historyBlockingTaskList;
	}

	// Token: 0x06003022 RID: 12322 RVA: 0x000F628E File Offset: 0x000F448E
	public void SetHistoryBlockingTaskList()
	{
		if (this.m_historyBlockingTaskList == null)
		{
			this.m_historyBlockingTaskList = this.m_currentTaskList;
		}
	}

	// Token: 0x06003023 RID: 12323 RVA: 0x000F62A4 File Offset: 0x000F44A4
	public void ForceStopHistoryBlocking()
	{
		this.m_historyBlocking = false;
		this.m_historyBlockingTaskList = null;
	}

	// Token: 0x06003024 RID: 12324 RVA: 0x000F62B4 File Offset: 0x000F44B4
	public PowerHistoryTimeline GetCurrentTimeline()
	{
		return this.m_currentTimeline;
	}

	// Token: 0x06003025 RID: 12325 RVA: 0x000F62BC File Offset: 0x000F44BC
	public PowerTaskList GetLatestUnendedTaskList()
	{
		int count = this.m_powerQueue.Count;
		if (count == 0)
		{
			return this.m_currentTaskList;
		}
		return this.m_powerQueue[count - 1];
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x000F62F0 File Offset: 0x000F44F0
	public PowerTaskList GetLastTaskList()
	{
		int count = this.m_powerQueue.Count;
		if (count > 0)
		{
			return this.m_powerQueue[count - 1];
		}
		return this.m_currentTaskList;
	}

	// Token: 0x06003027 RID: 12327 RVA: 0x000F6322 File Offset: 0x000F4522
	public PowerTaskList GetEarlyConcedeTaskList()
	{
		return this.m_earlyConcedeTaskList;
	}

	// Token: 0x06003028 RID: 12328 RVA: 0x000F632A File Offset: 0x000F452A
	public bool HasEarlyConcedeTaskList()
	{
		return this.m_earlyConcedeTaskList != null;
	}

	// Token: 0x06003029 RID: 12329 RVA: 0x000F6335 File Offset: 0x000F4535
	public PowerTaskList GetGameOverTaskList()
	{
		return this.m_gameOverTaskList;
	}

	// Token: 0x0600302A RID: 12330 RVA: 0x000F633D File Offset: 0x000F453D
	public bool HasGameOverTaskList()
	{
		return this.m_gameOverTaskList != null;
	}

	// Token: 0x0600302B RID: 12331 RVA: 0x000F6348 File Offset: 0x000F4548
	public bool CanDoRealTimeTask()
	{
		GameState gameState = GameState.Get();
		return gameState != null && !gameState.IsResetGamePending();
	}

	// Token: 0x0600302C RID: 12332 RVA: 0x000F636C File Offset: 0x000F456C
	public bool CanDoTask(PowerTask task)
	{
		if (task.IsCompleted())
		{
			return true;
		}
		Network.PowerHistory power = task.GetPower();
		return (power.Type != Network.PowerType.META_DATA || ((Network.HistMetaData)power).MetaType != HistoryMeta.Type.SHOW_BIG_CARD || !HistoryManager.Get().IsShowingBigCard()) && !GameState.Get().IsBusy() && !this.m_artificialPauseFromMetadata;
	}

	// Token: 0x0600302D RID: 12333 RVA: 0x000F63C8 File Offset: 0x000F45C8
	public void ForEachTaskList(Action<int, PowerTaskList> predicate)
	{
		if (this.m_currentTaskList != null)
		{
			predicate(-1, this.m_currentTaskList);
		}
		for (int i = 0; i < this.m_powerQueue.Count; i++)
		{
			predicate(i, this.m_powerQueue[i]);
		}
	}

	// Token: 0x0600302E RID: 12334 RVA: 0x000F6413 File Offset: 0x000F4613
	public bool HasTaskLists()
	{
		return this.m_currentTaskList != null || this.m_powerQueue.Count > 0;
	}

	// Token: 0x0600302F RID: 12335 RVA: 0x000F6430 File Offset: 0x000F4630
	public bool HasTaskList(PowerTaskList taskList)
	{
		return taskList != null && (this.m_currentTaskList == taskList || this.m_powerQueue.Contains(taskList));
	}

	// Token: 0x06003030 RID: 12336 RVA: 0x000F6454 File Offset: 0x000F4654
	public void OnPowerHistory(List<Network.PowerHistory> powerList)
	{
		this.m_totalSlushTime = 0;
		this.m_buildingTaskList = true;
		this.m_powerHistoryFirstTaskList = null;
		this.m_powerHistoryLastTaskList = null;
		this.m_currentTimeline = new PowerHistoryTimeline();
		for (int i = 0; i < powerList.Count; i++)
		{
			PowerTaskList powerTaskList = new PowerTaskList();
			if (this.m_previousStack.Count > 0)
			{
				PowerTaskList previous = this.m_previousStack.Pop();
				powerTaskList.SetPrevious(previous);
				this.m_previousStack.Push(powerTaskList);
			}
			if (this.m_subSpellOriginStack.Count > 0)
			{
				PowerTaskList powerTaskList2 = this.m_subSpellOriginStack.Peek();
				if (powerTaskList.GetOrigin() == powerTaskList2.GetOrigin())
				{
					powerTaskList.SetSubSpellOrigin(powerTaskList2);
				}
			}
			this.BuildTaskList(powerList, ref i, powerTaskList);
		}
		if (GameState.Get().AllowBatchedPowers())
		{
			for (int j = this.m_powerQueue.Count - 1; j > 0; j--)
			{
				PowerTaskList powerTaskList3 = this.m_powerQueue[j];
				if (powerTaskList3.IsBatchable())
				{
					int num = j - 1;
					PowerTaskList powerTaskList4 = this.m_powerQueue[num];
					while ((powerTaskList4.IsSlushTimeHelper() || !powerTaskList4.HasAnyTasksInImmediate()) && num > 0)
					{
						powerTaskList4 = this.m_powerQueue[--num];
					}
					if (powerTaskList3.IsBatchable() && powerTaskList4.IsBatchable())
					{
						powerTaskList3.FillMetaDataTargetSourceData();
						powerTaskList4.FillMetaDataTargetSourceData();
						powerTaskList4.AddTasks(powerTaskList3);
						foreach (int item in powerTaskList3.GetBlockStart().Entities)
						{
							if (!powerTaskList4.GetBlockStart().Entities.Contains(item))
							{
								powerTaskList4.GetBlockStart().Entities.Add(item);
							}
						}
						this.m_powerQueue.RemoveAt(j);
					}
				}
			}
		}
		if (GameState.Get().AllowDeferredPowers())
		{
			this.FixUpOutOfOrderDeferredTasks();
			for (int k = this.m_powerQueue.Count - 1; k > 0; k--)
			{
				PowerTaskList powerTaskList5 = this.m_powerQueue[k];
				if (powerTaskList5.GetPrevious() == this.m_powerQueue[k - 1] && powerTaskList5.IsCollapsible(false) && powerTaskList5.GetPrevious().IsCollapsible(true))
				{
					powerTaskList5.GetPrevious().AddTasks(powerTaskList5);
					powerTaskList5.GetPrevious().SetNext(null);
					if (powerTaskList5.GetBlockEnd() != null)
					{
						powerTaskList5.GetPrevious().SetBlockEnd(powerTaskList5.GetBlockEnd());
					}
					foreach (PowerTaskList powerTaskList6 in this.m_powerQueue)
					{
						if (powerTaskList6.GetPrevious() == powerTaskList5)
						{
							powerTaskList6.SetPrevious(powerTaskList5.GetPrevious());
						}
					}
					this.m_powerQueue.RemoveAt(k);
				}
			}
		}
		if (this.m_totalSlushTime > 0 && this.m_powerHistoryFirstTaskList != null && this.m_powerHistoryLastTaskList != null)
		{
			PowerTaskList powerHistoryFirstTaskList = this.m_powerHistoryFirstTaskList;
			PowerTaskList powerHistoryLastTaskList = this.m_powerHistoryLastTaskList;
			powerHistoryFirstTaskList.SetHistoryBlockStart(true);
			powerHistoryLastTaskList.SetHistoryBlockEnd(true);
			this.m_currentTimeline.m_firstTaskId = powerHistoryFirstTaskList.GetId();
			this.m_currentTimeline.m_lastTaskId = powerHistoryLastTaskList.GetId();
			this.m_currentTimeline.m_slushTime = this.m_totalSlushTime;
			this.m_powerHistoryTimeline.Add(this.m_currentTimeline);
			this.m_powerHistoryTimelineIdIndex.Add(this.m_currentTimeline.m_firstTaskId, this.m_powerHistoryTimeline.Count - 1);
			this.m_powerHistoryTimelineIdIndex.Add(this.m_currentTimeline.m_lastTaskId, this.m_powerHistoryTimeline.Count - 1);
			foreach (PowerHistoryTimelineEntry powerHistoryTimelineEntry in this.m_currentTimeline.m_orderedEvents)
			{
				if (!this.m_powerHistoryTimelineIdIndex.ContainsKey(powerHistoryTimelineEntry.taskId))
				{
					this.m_powerHistoryTimelineIdIndex.Add(powerHistoryTimelineEntry.taskId, this.m_powerHistoryTimeline.Count - 1);
				}
			}
		}
		this.m_buildingTaskList = false;
	}

	// Token: 0x06003031 RID: 12337 RVA: 0x000F68A4 File Offset: 0x000F4AA4
	private void FixUpOutOfOrderDeferredTasks()
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return;
		}
		for (int i = this.m_powerQueue.Count - 1; i >= 0; i--)
		{
			PowerTaskList powerTaskList = this.m_powerQueue[i];
			if (powerTaskList.IsDeferrable())
			{
				this.FixUpOutOfOrderDeferredTasksInTasklist(powerTaskList);
			}
		}
	}

	// Token: 0x06003032 RID: 12338 RVA: 0x000F68F4 File Offset: 0x000F4AF4
	private void FixUpOutOfOrderDeferredTasksInTasklist(PowerTaskList deferredTaskList)
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return;
		}
		Map<int, Map<int, List<int>>> entityChangesForTaskList = this.GetEntityChangesForTaskList(deferredTaskList);
		for (int i = 0; i < this.m_powerQueue.Count; i++)
		{
			PowerTaskList powerTaskList = this.m_powerQueue[i];
			if (powerTaskList.GetId() == deferredTaskList.GetId())
			{
				break;
			}
			if (powerTaskList.GetId() >= deferredTaskList.GetDeferredSourceId())
			{
				if (powerTaskList.IsDeferrable())
				{
					break;
				}
				Map<int, Map<int, List<int>>> entityChangesForTaskList2 = this.GetEntityChangesForTaskList(powerTaskList);
				foreach (KeyValuePair<int, Map<int, List<int>>> keyValuePair in entityChangesForTaskList)
				{
					int key = keyValuePair.Key;
					Map<int, List<int>> value = keyValuePair.Value;
					if (entityChangesForTaskList2.ContainsKey(key))
					{
						Map<int, List<int>> map = entityChangesForTaskList2[key];
						foreach (KeyValuePair<int, List<int>> keyValuePair2 in value)
						{
							int key2 = keyValuePair2.Key;
							List<int> value2 = keyValuePair2.Value;
							if (map.ContainsKey(key2))
							{
								List<int> list = map[key2];
								int newValue = value2[value2.Count - 1];
								int newValue2 = list[list.Count - 1];
								deferredTaskList.FixupLastTagChangeForEntityTag(key, key2, newValue2, true);
								powerTaskList.FixupLastTagChangeForEntityTag(key, key2, newValue, false);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06003033 RID: 12339 RVA: 0x000F6A7C File Offset: 0x000F4C7C
	private Map<int, Map<int, List<int>>> GetEntityChangesForTaskList(PowerTaskList taskList)
	{
		Map<int, Map<int, List<int>>> map = new Map<int, Map<int, List<int>>>();
		foreach (PowerTask powerTask in taskList.GetTagChangeTasks())
		{
			Network.HistTagChange histTagChange = powerTask.GetPower() as Network.HistTagChange;
			if (!map.ContainsKey(histTagChange.Entity))
			{
				map.Add(histTagChange.Entity, new Map<int, List<int>>());
			}
			if (!map[histTagChange.Entity].ContainsKey(histTagChange.Tag))
			{
				map[histTagChange.Entity].Add(histTagChange.Tag, new List<int>());
			}
			map[histTagChange.Entity][histTagChange.Tag].Add(histTagChange.Value);
		}
		return map;
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x000F6B58 File Offset: 0x000F4D58
	public void HandleTimelineStartEvent(int tasklistId, float time, bool isBlockStart, Network.HistBlockStart blockStart)
	{
		if (this.m_powerHistoryTimelineIdIndex.ContainsKey(tasklistId))
		{
			int index = this.m_powerHistoryTimelineIdIndex[tasklistId];
			PowerHistoryTimeline powerHistoryTimeline = this.m_powerHistoryTimeline[index];
			if (isBlockStart)
			{
				powerHistoryTimeline.m_startTime = time;
				if (!HearthstoneApplication.IsPublic())
				{
					Debug.Log(string.Format("Timeline start event: (TasklistId: {0}) ---- (Expected Duration: {1})", tasklistId, (float)powerHistoryTimeline.m_slushTime * 0.001f));
				}
			}
			if (powerHistoryTimeline.m_orderedEventIndexLookup.ContainsKey(tasklistId))
			{
				int index2 = powerHistoryTimeline.m_orderedEventIndexLookup[tasklistId];
				PowerHistoryTimelineEntry powerHistoryTimelineEntry = powerHistoryTimeline.m_orderedEvents[index2];
				powerHistoryTimelineEntry.entityId = ((blockStart != null) ? blockStart.Entities[0] : 0);
				float num = (float)powerHistoryTimelineEntry.expectedStartOffset * 0.001f;
				float num2 = time - powerHistoryTimeline.m_startTime;
				powerHistoryTimelineEntry.actualStartTime = num2;
				this.FireTaskEvent(num2 - num);
				if (!HearthstoneApplication.IsPublic())
				{
					Debug.Log(string.Format("Task start event: (TasklistId: {0}) ---- (Expected: {1} ---- (Actual: {2}))", tasklistId, num, num2));
				}
			}
		}
	}

	// Token: 0x06003035 RID: 12341 RVA: 0x000F6C60 File Offset: 0x000F4E60
	public void HandleTimelineEndEvent(int tasklistId, float time, bool isBlockEnd)
	{
		if (this.m_powerHistoryTimelineIdIndex.ContainsKey(tasklistId))
		{
			int index = this.m_powerHistoryTimelineIdIndex[tasklistId];
			PowerHistoryTimeline powerHistoryTimeline = this.m_powerHistoryTimeline[index];
			if (powerHistoryTimeline.m_orderedEventIndexLookup.ContainsKey(tasklistId))
			{
				int index2 = powerHistoryTimeline.m_orderedEventIndexLookup[tasklistId];
				PowerHistoryTimelineEntry powerHistoryTimelineEntry = powerHistoryTimeline.m_orderedEvents[index2];
				float num = (float)(powerHistoryTimelineEntry.expectedStartOffset + powerHistoryTimelineEntry.expectedTime) * 0.001f;
				float num2 = time - powerHistoryTimeline.m_startTime;
				this.FireTaskEvent(num2 - num);
				if (!HearthstoneApplication.IsPublic())
				{
					Debug.Log(string.Format("Task end event: (TasklistId: {0}) ---- (Expected: {1} ---- (Actual: {2}))", tasklistId, num, num2));
					SceneDebugger.Get().AddSlushTimeEntry(tasklistId, (float)powerHistoryTimelineEntry.expectedStartOffset * 0.001f, num, powerHistoryTimelineEntry.actualStartTime, num2, powerHistoryTimelineEntry.entityId);
				}
			}
			if (isBlockEnd)
			{
				powerHistoryTimeline.m_endTime = time;
				if (!HearthstoneApplication.IsPublic())
				{
					Debug.Log(string.Format("Timeline end event: (TasklistId: {0}) ---- (Expected: {1}) ---- (Actual: {2})", tasklistId, (float)powerHistoryTimeline.m_slushTime * 0.001f, powerHistoryTimeline.m_endTime - powerHistoryTimeline.m_startTime));
				}
			}
		}
	}

	// Token: 0x06003036 RID: 12342 RVA: 0x000F6D8C File Offset: 0x000F4F8C
	public void ProcessPowerQueue()
	{
		while (GameState.Get().CanProcessPowerQueue())
		{
			if (this.m_busyTaskList != null)
			{
				this.m_busyTaskList = null;
			}
			else
			{
				PowerTaskList powerTaskList = this.m_powerQueue.Peek();
				if (HistoryManager.Get() != null && HistoryManager.Get().IsShowingBigCard())
				{
					if (this.m_historyBlockingTaskList != null && !powerTaskList.IsDescendantOfBlock(this.m_historyBlockingTaskList))
					{
						break;
					}
					if (this.m_historyBlockingTaskList == null)
					{
						return;
					}
				}
				else
				{
					this.m_historyBlockingTaskList = null;
				}
				this.OnWillProcessTaskList(powerTaskList);
				if (GameState.Get().IsBusy())
				{
					this.m_busyTaskList = powerTaskList;
					return;
				}
			}
			if (this.CanEarlyConcede())
			{
				if (this.m_earlyConcedeTaskList == null && !this.m_handledFirstEarlyConcede)
				{
					this.DoEarlyConcedeVisuals();
					this.m_handledFirstEarlyConcede = true;
				}
				while (this.m_powerQueue.Count > 0)
				{
					this.m_currentTaskList = this.m_powerQueue.Dequeue();
					this.m_currentTaskList.DebugDump();
					this.CancelSpellsForEarlyConcede(this.m_currentTaskList);
					this.m_currentTaskList.DoEarlyConcedeTasks();
					this.m_currentTaskList = null;
				}
				return;
			}
			this.m_currentTaskList = this.m_powerQueue.Dequeue();
			if (this.m_previousTaskList == null || this.m_previousTaskList.GetOrigin() != this.m_currentTaskList.GetOrigin() || this.m_previousTaskList.GetParent() != this.m_currentTaskList.GetParent())
			{
				GameState.Get().ResetFriendlyCardDrawCounter();
				GameState.Get().ResetOpponentCardDrawCounter();
			}
			this.m_currentTaskList.DebugDump();
			this.OnProcessTaskList();
			this.StartCurrentTaskList();
		}
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x000F6F0B File Offset: 0x000F510B
	private int GetNextTaskListId()
	{
		int nextTaskListId = this.m_nextTaskListId;
		this.m_nextTaskListId = ((this.m_nextTaskListId == int.MaxValue) ? 1 : (this.m_nextTaskListId + 1));
		return nextTaskListId;
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x000F6F34 File Offset: 0x000F5134
	private bool CanDeferTaskList(Network.PowerHistory power)
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return false;
		}
		Network.HistBlockStart histBlockStart = power as Network.HistBlockStart;
		return histBlockStart != null && histBlockStart.IsDeferrable;
	}

	// Token: 0x06003039 RID: 12345 RVA: 0x000F6F64 File Offset: 0x000F5164
	private bool CanBatchTaskList(Network.PowerHistory power)
	{
		if (!GameState.Get().AllowBatchedPowers())
		{
			return false;
		}
		Network.HistBlockStart histBlockStart = power as Network.HistBlockStart;
		return histBlockStart != null && histBlockStart.IsBatchable;
	}

	// Token: 0x0600303A RID: 12346 RVA: 0x000F6F94 File Offset: 0x000F5194
	private bool IsDeferBlockerTaskList(Network.PowerHistory power)
	{
		Network.HistBlockStart histBlockStart = power as Network.HistBlockStart;
		return histBlockStart != null && (histBlockStart.IsDeferBlocker || (histBlockStart.BlockType == HistoryBlock.Type.TRIGGER && !histBlockStart.IsDeferrable) || (histBlockStart.BlockType == HistoryBlock.Type.ATTACK && !histBlockStart.IsDeferrable));
	}

	// Token: 0x0600303B RID: 12347 RVA: 0x000F6FDC File Offset: 0x000F51DC
	private void BuildTaskList(List<Network.PowerHistory> powerList, ref int index, PowerTaskList taskList)
	{
		while (index < powerList.Count)
		{
			Network.PowerHistory powerHistory = powerList[index];
			Network.PowerType type = powerHistory.Type;
			if (type == Network.PowerType.BLOCK_START)
			{
				if (!taskList.IsEmpty())
				{
					this.EnqueueTaskList(taskList);
					if (taskList.IsDeferrable())
					{
						taskList.SetDeferrable(false);
						List<PowerTaskList> item = this.m_deferredStack.Pop();
						if (this.m_deferredStack.Count > 0 && this.m_deferredStack.Peek().Contains(taskList))
						{
							this.m_deferredStack.Peek().Remove(taskList);
						}
						this.m_deferredStack.Push(item);
					}
				}
				PowerTaskList powerTaskList = new PowerTaskList();
				powerTaskList.SetBlockStart((Network.HistBlockStart)powerHistory);
				PowerTaskList origin = taskList.GetOrigin();
				if (origin.IsStartOfBlock())
				{
					powerTaskList.SetParent(origin);
				}
				this.m_previousStack.Push(powerTaskList);
				if (this.IsDeferBlockerTaskList(powerHistory))
				{
					this.EnqueueDeferredTaskLists(false);
					this.m_deferredStack.Push(new List<PowerTaskList>());
				}
				if (this.CanDeferTaskList(powerHistory))
				{
					this.m_deferredStack.Peek().Add(powerTaskList);
					powerTaskList.SetDeferrable(true);
				}
				else
				{
					powerTaskList.SetBatchable(this.CanBatchTaskList(powerHistory));
				}
				this.m_deferredStack.Push(new List<PowerTaskList>());
				index++;
				this.BuildTaskList(powerList, ref index, powerTaskList);
				return;
			}
			if (type == Network.PowerType.BLOCK_END)
			{
				taskList.SetBlockEnd((Network.HistBlockEnd)powerHistory);
				if (this.m_previousStack.Count <= 0)
				{
					break;
				}
				this.m_previousStack.Pop();
				if (!taskList.IsDeferrable())
				{
					this.EnqueueTaskList(taskList);
					this.EnqueueDeferredTaskLists(true);
					return;
				}
				if (this.m_powerQueue.Count > 0)
				{
					this.m_powerQueue.GetItem(this.m_powerQueue.Count - 1).SetCollapsible(true);
				}
				taskList.SetDeferredSourceId(this.m_nextTaskListId);
				if (this.m_deferredStack.Count > 0)
				{
					List<PowerTaskList> list = this.m_deferredStack.Pop();
					if (this.m_deferredStack.Count > 0)
					{
						List<PowerTaskList> list2 = this.m_deferredStack.Peek();
						if (list2 != null)
						{
							list2.AddRange(list);
							return;
						}
					}
					else
					{
						this.m_deferredStack.Push(list);
					}
				}
				return;
			}
			else
			{
				if (type == Network.PowerType.SUB_SPELL_START)
				{
					if (!taskList.HasTasks())
					{
						Network.HistMetaData netPower = new Network.HistMetaData
						{
							MetaType = HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT
						};
						taskList.CreateTask(netPower);
					}
					this.EnqueueTaskList(taskList);
					PowerTaskList powerTaskList2 = new PowerTaskList();
					powerTaskList2.SetPrevious(taskList);
					powerTaskList2.SetParent(taskList.GetParent());
					powerTaskList2.SetSubSpellOrigin(powerTaskList2);
					powerTaskList2.SetSubSpellStart((Network.HistSubSpellStart)powerHistory);
					this.m_subSpellOriginStack.Push(powerTaskList2);
					if (this.m_previousStack.Count > 0 && this.m_previousStack.Peek() == taskList)
					{
						this.m_previousStack.Pop();
						this.m_previousStack.Push(powerTaskList2);
					}
					taskList = powerTaskList2;
					goto IL_3C1;
				}
				if (type != Network.PowerType.SUB_SPELL_END)
				{
					goto IL_3C1;
				}
				taskList.CreateTask(powerHistory);
				taskList.SetSubSpellEnd((Network.HistSubSpellEnd)powerHistory);
				this.EnqueueTaskList(taskList);
				if (this.m_subSpellOriginStack.Count > 0)
				{
					if (this.m_subSpellOriginStack.Pop() != taskList.GetSubSpellOrigin())
					{
						Log.Power.PrintError("{0}.BuildTaskList(): Mismatch between SUB_SPELL_END task and current task list's SubSpellOrigin!", new object[]
						{
							this
						});
					}
				}
				else
				{
					Log.Power.PrintError("{0}.BuildTaskList(): Hit a SUB_SPELL_END task without a corresponding open SubSpellOrigin!", new object[]
					{
						this
					});
				}
				if (index + 1 >= powerList.Count)
				{
					goto IL_3C1;
				}
				PowerTaskList powerTaskList3 = new PowerTaskList();
				powerTaskList3.SetPrevious(taskList);
				powerTaskList3.SetParent(taskList.GetParent());
				if (this.m_subSpellOriginStack.Count > 0 && this.m_subSpellOriginStack.Peek().GetParent() == taskList.GetParent())
				{
					powerTaskList3.SetSubSpellOrigin(this.m_subSpellOriginStack.Peek());
				}
				if (this.m_previousStack.Count > 0 && this.m_previousStack.Peek() == taskList)
				{
					this.m_previousStack.Pop();
					this.m_previousStack.Push(powerTaskList3);
				}
				taskList = powerTaskList3;
				IL_429:
				index++;
				continue;
				IL_3C1:
				PowerTask powerTask = taskList.CreateTask(powerHistory);
				if (type == Network.PowerType.META_DATA && ((Network.HistMetaData)powerHistory).MetaType == HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT)
				{
					this.EnqueueTaskList(taskList);
					return;
				}
				if (this.CanDoRealTimeTask())
				{
					powerTask.DoRealTimeTask(powerList, index);
					goto IL_429;
				}
				PowerProcessor.DelayedRealTimeTask delayedRealTimeTask = new PowerProcessor.DelayedRealTimeTask();
				delayedRealTimeTask.m_index = index;
				delayedRealTimeTask.m_powerTask = powerTask;
				delayedRealTimeTask.m_powerHistory = new List<Network.PowerHistory>(powerList);
				this.m_delayedRealTimeTasks.Enqueue(delayedRealTimeTask);
				goto IL_429;
			}
		}
		if (!taskList.IsEmpty())
		{
			this.EnqueueTaskList(taskList);
		}
		if (this.m_deferredStack.Count == 0)
		{
			return;
		}
		this.EnqueueDeferredTaskLists(true);
		if (this.m_deferredStack.Count == 0)
		{
			this.m_deferredStack.Push(new List<PowerTaskList>());
		}
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x000F7468 File Offset: 0x000F5668
	private void EnqueueDeferredTaskLists(bool combine)
	{
		if (this.m_deferredStack.Count > 0)
		{
			List<PowerTaskList> list = this.m_deferredStack.Pop();
			for (int i = list.Count - 1; i > 0; i--)
			{
				PowerTaskList powerTaskList = list[i];
				if (powerTaskList.GetBlockStart() != null && combine)
				{
					for (int j = i - 1; j >= 0; j--)
					{
						PowerTaskList powerTaskList2 = list[j];
						if (powerTaskList2.GetBlockStart() != null && powerTaskList2.GetBlockStart().Entities.Count == powerTaskList.GetBlockStart().Entities.Count)
						{
							bool flag = true;
							foreach (int item in powerTaskList2.GetBlockStart().Entities)
							{
								if (!powerTaskList.GetBlockStart().Entities.Contains(item))
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								powerTaskList2.AddTasks(powerTaskList);
								list.RemoveAt(i);
								break;
							}
						}
					}
				}
			}
			foreach (PowerTaskList taskList in list)
			{
				this.EnqueueTaskList(taskList);
			}
		}
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x000F75C8 File Offset: 0x000F57C8
	public bool EntityHasPendingTasks(global::Entity entity)
	{
		int entityId = entity.GetEntityId();
		Predicate<global::Entity> <>9__0;
		Predicate<global::Entity> <>9__1;
		foreach (PowerTaskList powerTaskList in this.m_powerQueue)
		{
			List<global::Entity> sourceEntities = powerTaskList.GetSourceEntities(false);
			if (sourceEntities != null)
			{
				List<global::Entity> list = sourceEntities;
				Predicate<global::Entity> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((global::Entity e) => e != null && e.GetEntityId() == entityId));
				}
				if (list.Exists(match))
				{
					return true;
				}
			}
			global::Entity targetEntity = powerTaskList.GetTargetEntity(false);
			if (targetEntity != null && targetEntity.GetEntityId() == entityId)
			{
				return true;
			}
			PowerTaskList parent = powerTaskList.GetParent();
			if (parent != null)
			{
				List<global::Entity> sourceEntities2 = parent.GetSourceEntities(false);
				if (sourceEntities2 != null)
				{
					List<global::Entity> list2 = sourceEntities2;
					Predicate<global::Entity> match2;
					if ((match2 = <>9__1) == null)
					{
						match2 = (<>9__1 = ((global::Entity e) => e != null && e.GetEntityId() == entityId));
					}
					if (list2.Exists(match2))
					{
						return true;
					}
				}
				global::Entity targetEntity2 = parent.GetTargetEntity(false);
				if (targetEntity2 != null && targetEntity2.GetEntityId() == entityId)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600303E RID: 12350 RVA: 0x000F76F8 File Offset: 0x000F58F8
	public void FlushDelayedRealTimeTasks()
	{
		while (this.CanDoRealTimeTask() && this.m_delayedRealTimeTasks.Count > 0)
		{
			PowerProcessor.DelayedRealTimeTask delayedRealTimeTask = this.m_delayedRealTimeTasks.Dequeue();
			delayedRealTimeTask.m_powerTask.DoRealTimeTask(delayedRealTimeTask.m_powerHistory, delayedRealTimeTask.m_index);
		}
	}

	// Token: 0x0600303F RID: 12351 RVA: 0x000F7740 File Offset: 0x000F5940
	private void EnqueueTaskList(PowerTaskList taskList)
	{
		this.m_totalSlushTime += taskList.GetTotalSlushTime();
		if (this.m_powerHistoryFirstTaskList == null)
		{
			this.m_powerHistoryFirstTaskList = taskList;
		}
		else
		{
			this.m_powerHistoryLastTaskList = taskList;
		}
		taskList.SetId(this.GetNextTaskListId());
		this.m_powerQueue.Enqueue(taskList);
		if (this.m_currentTimeline != null && taskList.GetTotalSlushTime() > 0)
		{
			this.m_currentTimeline.AddTimelineEntry(taskList.GetId(), taskList.GetTotalSlushTime());
		}
		if (taskList.HasFriendlyConcede())
		{
			this.m_earlyConcedeTaskList = taskList;
		}
		if (taskList.HasGameOver())
		{
			this.m_gameOverTaskList = taskList;
		}
	}

	// Token: 0x06003040 RID: 12352 RVA: 0x000F77D8 File Offset: 0x000F59D8
	private void OnWillProcessTaskList(PowerTaskList taskList)
	{
		if (ThinkEmoteManager.Get())
		{
			ThinkEmoteManager.Get().NotifyOfActivity();
		}
		if (taskList.IsStartOfBlock() && taskList.GetBlockStart().BlockType == HistoryBlock.Type.PLAY)
		{
			global::Entity sourceEntity = taskList.GetSourceEntity(false);
			if (sourceEntity.GetController().IsOpposingSide())
			{
				string text = sourceEntity.GetCardId();
				if (string.IsNullOrEmpty(text))
				{
					text = this.FindRevealedCardId(taskList);
				}
				GameState.Get().GetGameEntity().NotifyOfOpponentWillPlayCard(text);
			}
		}
	}

	// Token: 0x06003041 RID: 12353 RVA: 0x000F784D File Offset: 0x000F5A4D
	private bool ContainsBurnedCard(PowerTaskList taskList)
	{
		return this.ContainsMetaDataTaskWithInfo(taskList, HistoryMeta.Type.BURNED_CARD);
	}

	// Token: 0x06003042 RID: 12354 RVA: 0x000F7858 File Offset: 0x000F5A58
	private bool ContainsPoisonousEffect(PowerTaskList taskList)
	{
		return this.ContainsMetaDataTaskWithInfo(taskList, HistoryMeta.Type.POISONOUS);
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x000F7864 File Offset: 0x000F5A64
	private bool ContainsMetaDataTaskWithInfo(PowerTaskList taskList, HistoryMeta.Type metaType)
	{
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == metaType)
				{
					if (histMetaData.Info.Count == 0)
					{
						Log.Power.PrintError("PowerProcessor.ContainsMetaDataTaskWithInfo(): metaData.Info.Count is 0, metaType: {0}", new object[]
						{
							metaType
						});
					}
					else
					{
						if (GameState.Get().GetEntity(histMetaData.Info[0]) != null)
						{
							return true;
						}
						Log.Power.PrintError("PowerProcessor.ContainsMetaDataTaskWithInfo(): metaData.Info contains an invalid entity (ID {0}), metaType: {1}", new object[]
						{
							histMetaData.Info[0],
							metaType
						});
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x000F7934 File Offset: 0x000F5B34
	private string FindRevealedCardId(PowerTaskList taskList)
	{
		taskList.GetBlockStart();
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			Network.HistShowEntity showEntity = power as Network.HistShowEntity;
			if (showEntity != null && taskList.GetSourceEntities(true) != null && taskList.GetSourceEntities(true).Exists((global::Entity e) => e != null && e.GetEntityId() == showEntity.Entity.ID))
			{
				return showEntity.Entity.CardID;
			}
		}
		return null;
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x000F79BC File Offset: 0x000F5BBC
	private void OnProcessTaskList()
	{
		if (this.m_currentTaskList.IsStartOfBlock())
		{
			Network.HistBlockStart blockStart = this.m_currentTaskList.GetBlockStart();
			HistoryBlock.Type blockType = blockStart.BlockType;
			if (blockType != HistoryBlock.Type.ATTACK)
			{
				if (blockType != HistoryBlock.Type.DEATHS)
				{
					if (blockType == HistoryBlock.Type.PLAY)
					{
						global::Entity sourceEntity = this.m_currentTaskList.GetSourceEntity(false);
						if (sourceEntity.IsControlledByFriendlySidePlayer())
						{
							GameState.Get().GetGameEntity().NotifyOfFriendlyPlayedCard(sourceEntity);
						}
						else
						{
							GameState.Get().GetGameEntity().NotifyOfOpponentPlayedCard(sourceEntity);
						}
						if (sourceEntity.IsMinion())
						{
							GameState.Get().GetGameEntity().NotifyOfMinionPlayed(sourceEntity);
						}
						else if (sourceEntity.IsHero())
						{
							GameState.Get().GetGameEntity().NotifyOfHeroChanged(sourceEntity);
						}
						else if (sourceEntity.IsWeapon())
						{
							GameState.Get().GetGameEntity().NotifyOfWeaponEquipped(sourceEntity);
						}
						else if (sourceEntity.IsSpell())
						{
							global::Entity targetEntity = this.m_currentTaskList.GetTargetEntity(false);
							GameState.Get().GetGameEntity().NotifyOfSpellPlayed(sourceEntity, targetEntity);
						}
						else if (sourceEntity.IsHeroPower())
						{
							global::Entity targetEntity2 = this.m_currentTaskList.GetTargetEntity(false);
							GameState.Get().GetGameEntity().NotifyOfHeroPowerUsed(sourceEntity, targetEntity2);
						}
					}
				}
				else
				{
					foreach (PowerTask powerTask in this.m_currentTaskList.GetTaskList())
					{
						Network.PowerHistory power = powerTask.GetPower();
						if (power.Type == Network.PowerType.TAG_CHANGE)
						{
							Network.HistTagChange histTagChange = power as Network.HistTagChange;
							if (GameUtils.IsEntityDeathTagChange(histTagChange))
							{
								global::Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
								if (entity.IsMinion())
								{
									GameState.Get().GetGameEntity().NotifyOfMinionDied(entity);
								}
								else if (entity.IsHero())
								{
									GameState.Get().GetGameEntity().NotifyOfHeroDied(entity);
								}
								else if (entity.IsWeapon())
								{
									GameState.Get().GetGameEntity().NotifyOfWeaponDestroyed(entity);
								}
							}
						}
					}
				}
			}
			else
			{
				global::Entity attacker = this.m_currentTaskList.GetAttacker();
				global::Entity entity2 = null;
				AttackType attackType = this.m_currentTaskList.GetAttackType();
				if (attackType != AttackType.REGULAR)
				{
					if (attackType == AttackType.CANCELED)
					{
						entity2 = this.m_currentTaskList.GetProposedDefender();
					}
				}
				else
				{
					entity2 = this.m_currentTaskList.GetDefender();
				}
				if (attacker != null && entity2 != null)
				{
					GameState.Get().GetGameEntity().NotifyOfEntityAttacked(attacker, entity2);
				}
			}
			if (blockStart.BlockType == HistoryBlock.Type.POWER || blockStart.BlockType == HistoryBlock.Type.TRIGGER)
			{
				for (int i = 0; i < blockStart.EffectCardId.Count; i++)
				{
					if (string.IsNullOrEmpty(blockStart.EffectCardId[i]))
					{
						List<global::Entity> sourceEntities = this.m_currentTaskList.GetSourceEntities(true);
						if (sourceEntities != null && i < sourceEntities.Count && sourceEntities[i] != null)
						{
							blockStart.EffectCardId[i] = sourceEntities[i].GetCardId();
							blockStart.IsEffectCardIdClientCached[i] = true;
						}
					}
				}
			}
		}
		this.PrepareHistoryForCurrentTaskList();
		this.m_currentTaskList.CreateArtificialHistoryTilesFromMetadata();
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x000F7CCC File Offset: 0x000F5ECC
	private void PrepareHistoryForCurrentTaskList()
	{
		Log.Power.Print("PowerProcessor.PrepareHistoryForCurrentTaskList() - m_currentTaskList={0}", new object[]
		{
			this.m_currentTaskList.GetId()
		});
		Network.HistBlockStart blockStart = this.m_currentTaskList.GetBlockStart();
		if (blockStart == null)
		{
			return;
		}
		List<global::Entity> sourceEntities = this.m_currentTaskList.GetSourceEntities(true);
		if (sourceEntities != null)
		{
			if (sourceEntities.Exists((global::Entity e) => e != null && e.HasTag(GAME_TAG.CARD_DOES_NOTHING)))
			{
				return;
			}
		}
		HistoryBlock.Type blockType = blockStart.BlockType;
		if (blockType == HistoryBlock.Type.ATTACK)
		{
			AttackType attackType = this.m_currentTaskList.GetAttackType();
			global::Entity entity = null;
			global::Entity entity2 = null;
			if (attackType != AttackType.REGULAR)
			{
				if (attackType == AttackType.CANCELED)
				{
					entity = this.m_currentTaskList.GetAttacker();
					entity2 = this.m_currentTaskList.GetProposedDefender();
				}
			}
			else
			{
				entity = this.m_currentTaskList.GetAttacker();
				entity2 = this.m_currentTaskList.GetDefender();
			}
			if (entity != null && entity2 != null)
			{
				HistoryManager.Get().CreateAttackTile(entity, entity2, this.m_currentTaskList);
				this.m_currentTaskList.SetWillCompleteHistoryEntry(true);
			}
			if (HistoryManager.Get().HasHistoryEntry())
			{
				this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
				return;
			}
		}
		else if (blockType == HistoryBlock.Type.PLAY)
		{
			global::Entity sourceEntity = this.m_currentTaskList.GetSourceEntity(false);
			if (sourceEntity == null)
			{
				return;
			}
			if (this.m_currentTaskList.IsStartOfBlock())
			{
				if (this.m_currentTaskList.ShouldCreatePlayBlockHistoryTile())
				{
					global::Entity entity3 = GameState.Get().GetEntity(blockStart.Target);
					HistoryManager.Get().CreatePlayedTile(sourceEntity, entity3);
					this.m_currentTaskList.SetWillCompleteHistoryEntry(true);
				}
				if (this.ShouldShowPlayedBigCard(sourceEntity, blockStart))
				{
					bool countered = this.m_currentTaskList.WasThePlayedSpellCountered(sourceEntity);
					this.SetHistoryBlockingTaskList();
					HistoryManager.Get().CreatePlayedBigCard(sourceEntity, new HistoryManager.BigCardStartedCallback(this.OnBigCardStarted), new HistoryManager.BigCardFinishedCallback(this.OnBigCardFinished), false, countered, 0);
				}
			}
			this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
			return;
		}
		else if (blockType == HistoryBlock.Type.POWER)
		{
			if (HistoryManager.Get().HasHistoryEntry())
			{
				this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
				return;
			}
		}
		else
		{
			if (blockType == HistoryBlock.Type.JOUST)
			{
				this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
				return;
			}
			if (blockType == HistoryBlock.Type.REVEAL_CARD)
			{
				this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
				return;
			}
			if (blockType == HistoryBlock.Type.TRIGGER)
			{
				global::Entity sourceEntity2 = this.m_currentTaskList.GetSourceEntity(false);
				if (sourceEntity2 == null)
				{
					return;
				}
				if (sourceEntity2.IsSecret() || blockStart.TriggerKeyword == 1192 || blockStart.TriggerKeyword == 1749)
				{
					if (this.m_currentTaskList.IsStartOfBlock())
					{
						HistoryManager.Get().CreateTriggerTile(sourceEntity2);
						this.m_currentTaskList.SetWillCompleteHistoryEntry(true);
						this.SetHistoryBlockingTaskList();
						HistoryManager.Get().CreateTriggeredBigCard(sourceEntity2, new HistoryManager.BigCardStartedCallback(this.OnBigCardStarted), new HistoryManager.BigCardFinishedCallback(this.OnBigCardFinished), false, true);
					}
					this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
					return;
				}
				bool flag = false;
				if (!this.m_currentTaskList.IsStartOfBlock())
				{
					flag = this.GetTriggerTaskListThatShouldCompleteHistoryEntry().WillBlockCompleteHistoryEntry();
				}
				else if (blockStart.ShowInHistory)
				{
					if (sourceEntity2.HasTag(GAME_TAG.HISTORY_PROXY))
					{
						global::Entity entity4 = GameState.Get().GetEntity(sourceEntity2.GetTag(GAME_TAG.HISTORY_PROXY));
						HistoryManager.Get().CreatePlayedTile(entity4, null);
						if (sourceEntity2.GetController() != GameState.Get().GetFriendlySidePlayer() || !sourceEntity2.HasTag(GAME_TAG.HISTORY_PROXY_NO_BIG_CARD))
						{
							this.SetHistoryBlockingTaskList();
							HistoryManager.Get().CreateTriggeredBigCard(entity4, new HistoryManager.BigCardStartedCallback(this.OnBigCardStarted), new HistoryManager.BigCardFinishedCallback(this.OnBigCardFinished), false, false);
						}
					}
					else
					{
						if (this.ShouldShowTriggeredBigCard(sourceEntity2))
						{
							this.SetHistoryBlockingTaskList();
							HistoryManager.Get().CreateTriggeredBigCard(sourceEntity2, new HistoryManager.BigCardStartedCallback(this.OnBigCardStarted), new HistoryManager.BigCardFinishedCallback(this.OnBigCardFinished), false, false);
						}
						HistoryManager.Get().CreateTriggerTile(sourceEntity2);
					}
					this.GetTriggerTaskListThatShouldCompleteHistoryEntry().SetWillCompleteHistoryEntry(true);
					flag = true;
				}
				else if ((blockStart.TriggerKeyword == 685 || blockStart.TriggerKeyword == 923 || blockStart.TriggerKeyword == 363 || blockStart.TriggerKeyword == 1944 || blockStart.TriggerKeyword == 1675) && HistoryManager.Get().HasHistoryEntry())
				{
					flag = true;
				}
				else if (this.ContainsBurnedCard(this.m_currentTaskList))
				{
					if (this.m_currentTaskList.IsStartOfBlock())
					{
						HistoryManager.Get().CreateBurnedCardsTile();
						this.m_currentTaskList.SetWillCompleteHistoryEntry(true);
					}
					this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
				}
				else if (this.ContainsPoisonousEffect(this.m_currentTaskList))
				{
					flag = true;
				}
				if (flag)
				{
					this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
					return;
				}
			}
			else if (blockType == HistoryBlock.Type.FATIGUE)
			{
				if (this.m_currentTaskList.IsStartOfBlock())
				{
					HistoryManager.Get().CreateFatigueTile();
					this.m_currentTaskList.SetWillCompleteHistoryEntry(true);
				}
				this.m_currentTaskList.NotifyHistoryOfAdditionalTargets(null);
			}
		}
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x000F8168 File Offset: 0x000F6368
	private void OnBigCardStarted()
	{
		this.m_historyBlocking = true;
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x000F8171 File Offset: 0x000F6371
	private void OnBigCardFinished()
	{
		this.m_historyBlocking = false;
	}

	// Token: 0x06003049 RID: 12361 RVA: 0x000F817A File Offset: 0x000F637A
	private bool ShouldShowPlayedBigCard(global::Entity sourceEntity, Network.HistBlockStart blockStart)
	{
		return GameState.Get().GetBooleanGameOption(GameEntityOption.USES_BIG_CARDS) && (!InputManager.Get().PermitDecisionMakingInput() || sourceEntity.IsControlledByOpposingSidePlayer() || blockStart.ForceShowBigCard);
	}

	// Token: 0x0600304A RID: 12362 RVA: 0x000F81AF File Offset: 0x000F63AF
	private bool ShouldShowTriggeredBigCard(global::Entity sourceEntity)
	{
		return sourceEntity.GetZone() == TAG_ZONE.HAND && !sourceEntity.IsHidden() && sourceEntity.HasTriggerVisual();
	}

	// Token: 0x0600304B RID: 12363 RVA: 0x000F81D4 File Offset: 0x000F63D4
	private PowerTaskList GetTriggerTaskListThatShouldCompleteHistoryEntry()
	{
		if (this.m_currentTaskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return null;
		}
		PowerTaskList parent = this.m_currentTaskList.GetParent();
		if (parent != null && parent.GetBlockType() == HistoryBlock.Type.RITUAL)
		{
			return parent;
		}
		return this.m_currentTaskList.GetOrigin();
	}

	// Token: 0x0600304C RID: 12364 RVA: 0x000F8218 File Offset: 0x000F6418
	private bool CanEarlyConcede()
	{
		if (!GameState.Get().IsGameCreated())
		{
			return false;
		}
		if (this.m_earlyConcedeTaskList != null)
		{
			return true;
		}
		if (GameState.Get().IsGameOver())
		{
			return false;
		}
		if (GameState.Get().WasConcedeRequested())
		{
			Network.HistTagChange realTimeGameOverTagChange = GameState.Get().GetRealTimeGameOverTagChange();
			if (realTimeGameOverTagChange != null && realTimeGameOverTagChange.Value != 4)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600304D RID: 12365 RVA: 0x000F8274 File Offset: 0x000F6474
	private void DoEarlyConcedeVisuals()
	{
		if (GameUtils.IsWaitingForOpponentReconnect())
		{
			return;
		}
		global::Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			friendlySidePlayer.PlayConcedeEmote();
		}
	}

	// Token: 0x0600304E RID: 12366 RVA: 0x000F82A0 File Offset: 0x000F64A0
	private void CancelSpellsForEarlyConcede(PowerTaskList taskList)
	{
		List<global::Entity> sourceEntities = taskList.GetSourceEntities(true);
		if (sourceEntities == null)
		{
			return;
		}
		foreach (global::Entity entity in sourceEntities)
		{
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (card && taskList.GetBlockStart().BlockType == HistoryBlock.Type.POWER)
				{
					Spell playSpell = card.GetPlaySpell(0, true);
					if (playSpell)
					{
						SpellStateType activeState = playSpell.GetActiveState();
						if (activeState != SpellStateType.NONE && activeState != SpellStateType.CANCEL)
						{
							playSpell.ActivateState(SpellStateType.CANCEL);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600304F RID: 12367 RVA: 0x000F8340 File Offset: 0x000F6540
	private void StartCurrentTaskList()
	{
		this.m_currentTaskList.SetProcessStartTime();
		GameState gameState = GameState.Get();
		Network.HistBlockStart blockStart = this.m_currentTaskList.GetBlockStart();
		if (blockStart == null)
		{
			this.DoCurrentTaskList();
			return;
		}
		int num = (blockStart.Entities.Count == 0) ? 0 : blockStart.Entities[0];
		if (this.m_currentTaskList.GetSourceEntities(true) == null || this.m_currentTaskList.GetSourceEntity(true) == null)
		{
			if (!gameState.EntityRemovedFromGame(num))
			{
				Debug.LogErrorFormat("PowerProcessor.StartCurrentTaskList() - WARNING got a power with a null source entity (ID={0})", new object[]
				{
					num
				});
			}
			this.DoCurrentTaskList();
			return;
		}
		if (!this.DoTaskListWithSpellController(gameState, this.m_currentTaskList, this.m_currentTaskList.GetSourceEntity(true)))
		{
			this.DoCurrentTaskList();
			return;
		}
	}

	// Token: 0x06003050 RID: 12368 RVA: 0x000F83F8 File Offset: 0x000F65F8
	private void DoCurrentTaskList()
	{
		this.m_currentTaskList.DoAllTasks(delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			this.EndCurrentTaskList();
		});
	}

	// Token: 0x06003051 RID: 12369 RVA: 0x000F8414 File Offset: 0x000F6614
	private void EndCurrentTaskList()
	{
		Log.Power.Print("PowerProcessor.EndCurrentTaskList() - m_currentTaskList={0}", new object[]
		{
			(this.m_currentTaskList == null) ? "null" : this.m_currentTaskList.GetId().ToString()
		});
		if (this.m_currentTaskList == null)
		{
			GameState.Get().OnTaskListEnded(null);
			return;
		}
		if (this.m_currentTaskList.GetBlockEnd() != null)
		{
			if (this.m_currentTaskList.GetOrigin() == this.m_historyBlockingTaskList && this.m_currentTaskList.GetNext() == null)
			{
				this.m_historyBlockingTaskList = null;
			}
			if (this.m_currentTaskList.IsRitualBlock() && HistoryManager.Get().HasHistoryEntry())
			{
				this.AddCthunToHistory();
			}
			global::Entity sourceEntity = this.m_currentTaskList.GetSourceEntity(true);
			if (sourceEntity != null && sourceEntity.IsTwinspell())
			{
				this.CleanupTwinspellEffects(sourceEntity);
			}
			if (this.m_currentTaskList.WillBlockCompleteHistoryEntry())
			{
				HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
			}
		}
		GameState.Get().OnTaskListEnded(this.m_currentTaskList);
		this.m_previousTaskList = this.m_currentTaskList;
		this.m_currentTaskList = null;
	}

	// Token: 0x06003052 RID: 12370 RVA: 0x000F851C File Offset: 0x000F671C
	private void AddCthunToHistory()
	{
		global::Entity ritualEntityClone = this.m_currentTaskList.GetOrigin().GetRitualEntityClone();
		if (ritualEntityClone == null)
		{
			return;
		}
		global::Entity sourceEntity = this.m_currentTaskList.GetSourceEntity(true);
		if (sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN))
		{
			HistoryManager.Get().NotifyEntityAffected(ritualEntityClone, true, false, false, false, false);
			return;
		}
		int tag = sourceEntity.GetController().GetTag(GAME_TAG.PROXY_CTHUN);
		global::Entity entity = GameState.Get().GetEntity(tag);
		if (entity == null)
		{
			return;
		}
		if (entity.GetTag(GAME_TAG.ATK) != ritualEntityClone.GetTag(GAME_TAG.ATK) || entity.GetTag(GAME_TAG.HEALTH) != ritualEntityClone.GetTag(GAME_TAG.HEALTH) || entity.GetTag(GAME_TAG.TAUNT) != ritualEntityClone.GetTag(GAME_TAG.TAUNT))
		{
			HistoryManager.Get().NotifyEntityAffected(entity, true, false, false, false, false);
		}
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x000F85D8 File Offset: 0x000F67D8
	private void CleanupTwinspellEffects(global::Entity twinspellEntity)
	{
		if (InputManager.Get().GetFriendlyHand().IsTwinspellBeingPlayed(twinspellEntity))
		{
			InputManager.Get().GetFriendlyHand().ActivateTwinspellSpellDeath();
			InputManager.Get().GetFriendlyHand().ClearReservedCard();
		}
	}

	// Token: 0x06003054 RID: 12372 RVA: 0x000F860C File Offset: 0x000F680C
	private bool DoTaskListWithSpellController(GameState state, PowerTaskList taskList, global::Entity sourceEntity)
	{
		HistoryBlock.Type blockType = taskList.GetBlockType();
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		if (taskList.IsSubSpellTaskList())
		{
			return this.DoSubSpellTaskListWithController(taskList);
		}
		if (blockType == HistoryBlock.Type.ATTACK)
		{
			AttackSpellController spellController = this.CreateAttackSpellController(taskList);
			if (!this.DoTaskListUsingController(spellController, taskList))
			{
				this.DestroySpellController(spellController);
				return false;
			}
			return true;
		}
		else if (blockType == HistoryBlock.Type.MOVE_MINION)
		{
			MoveMinionSpellController spellController2 = this.CreateMoveMinionSpellController(taskList);
			if (!this.DoTaskListUsingController(spellController2, taskList))
			{
				this.DestroySpellController(spellController2);
				return false;
			}
			return true;
		}
		else if (blockType == HistoryBlock.Type.POWER)
		{
			PowerSpellController spellController3 = this.CreatePowerSpellController(taskList);
			if (!this.DoTaskListUsingController(spellController3, taskList))
			{
				this.DestroySpellController(spellController3);
				return false;
			}
			return true;
		}
		else
		{
			if (blockType == HistoryBlock.Type.TRIGGER)
			{
				if (sourceEntity != null && sourceEntity.IsSecret())
				{
					SecretSpellController spellController4 = this.CreateSecretSpellController(taskList);
					if (!this.DoTaskListUsingController(spellController4, taskList))
					{
						this.DestroySpellController(spellController4);
						return false;
					}
				}
				else if (blockStart != null && blockStart.TriggerKeyword == 1192)
				{
					SideQuestSpellController spellController5 = this.CreateSideQuestSpellController(taskList);
					if (!this.DoTaskListUsingController(spellController5, taskList))
					{
						this.DestroySpellController(spellController5);
						return false;
					}
				}
				else if (blockStart != null && blockStart.TriggerKeyword == 1749)
				{
					SigilSpellController spellController6 = this.CreateSigilSpellController(taskList);
					if (!this.DoTaskListUsingController(spellController6, taskList))
					{
						this.DestroySpellController(spellController6);
						return false;
					}
				}
				else
				{
					TriggerSpellController triggerSpellController = this.CreateTriggerSpellController(taskList);
					Card card = (sourceEntity != null) ? sourceEntity.GetCard() : null;
					Card startDrawMetaDataCard = taskList.GetStartDrawMetaDataCard();
					if (TurnStartManager.Get().IsCardDrawHandled(card) || TurnStartManager.Get().IsCardDrawHandled(startDrawMetaDataCard))
					{
						if (!triggerSpellController.AttachPowerTaskList(taskList))
						{
							Log.Power.PrintWarning("TurnStartManager failed to handle a trigger. sourceCard:{0}, metadataCard:{1}, taskList:{2}", new object[]
							{
								card,
								startDrawMetaDataCard,
								taskList
							});
							this.DestroySpellController(triggerSpellController);
							return false;
						}
						triggerSpellController.AddFinishedTaskListCallback(new SpellController.FinishedTaskListCallback(this.OnSpellControllerFinishedTaskList));
						triggerSpellController.AddFinishedCallback(new SpellController.FinishedCallback(this.OnSpellControllerFinished));
						TurnStartManager.Get().NotifyOfSpellController(triggerSpellController);
					}
					else if (!this.DoTaskListUsingController(triggerSpellController, taskList))
					{
						this.DestroySpellController(triggerSpellController);
						return false;
					}
				}
				return true;
			}
			if (blockType == HistoryBlock.Type.DEATHS)
			{
				DeathSpellController spellController7 = this.CreateDeathSpellController(taskList);
				if (!this.DoTaskListUsingController(spellController7, taskList))
				{
					this.DestroySpellController(spellController7);
					return false;
				}
				return true;
			}
			else if (blockType == HistoryBlock.Type.FATIGUE)
			{
				FatigueSpellController fatigueSpellController = this.CreateFatigueSpellController(taskList);
				if (!fatigueSpellController.AttachPowerTaskList(taskList))
				{
					this.DestroySpellController(fatigueSpellController);
					return false;
				}
				fatigueSpellController.AddFinishedTaskListCallback(new SpellController.FinishedTaskListCallback(this.OnSpellControllerFinishedTaskList));
				fatigueSpellController.AddFinishedCallback(new SpellController.FinishedCallback(this.OnSpellControllerFinished));
				if (state.IsTurnStartManagerActive())
				{
					TurnStartManager.Get().NotifyOfSpellController(fatigueSpellController);
				}
				else
				{
					fatigueSpellController.DoPowerTaskList();
				}
				return true;
			}
			else if (blockType == HistoryBlock.Type.JOUST)
			{
				JoustSpellController spellController8 = this.CreateJoustSpellController(taskList);
				if (!this.DoTaskListUsingController(spellController8, taskList))
				{
					this.DestroySpellController(spellController8);
					return false;
				}
				return true;
			}
			else if (blockType == HistoryBlock.Type.RITUAL)
			{
				RitualSpellController spellController9 = this.CreateRitualSpellController(taskList);
				if (!this.DoTaskListUsingController(spellController9, taskList))
				{
					this.DestroySpellController(spellController9);
					return false;
				}
				return true;
			}
			else if (blockType == HistoryBlock.Type.REVEAL_CARD)
			{
				RevealCardSpellController spellController10 = this.CreateRevealCardSpellController(taskList);
				if (!this.DoTaskListUsingController(spellController10, taskList))
				{
					this.DestroySpellController(spellController10);
					return false;
				}
				return true;
			}
			else
			{
				if (blockType != HistoryBlock.Type.GAME_RESET)
				{
					if (blockType == HistoryBlock.Type.PLAY)
					{
						this.CheckDeactivatePlaySpellForSpellPlayBlock(taskList);
						this.CheckDeactivatePlaySpellForTransformation(taskList);
					}
					Log.Power.Print("PowerProcessor.DoTaskListForCard() - unhandled BlockType {0} for sourceEntity {1}", new object[]
					{
						blockType,
						sourceEntity
					});
					return false;
				}
				ResetGameSpellController spellController11 = this.CreateResetGameSpellController(taskList);
				if (!this.DoTaskListUsingController(spellController11, taskList))
				{
					this.DestroySpellController(spellController11);
					return false;
				}
				return true;
			}
		}
	}

	// Token: 0x06003055 RID: 12373 RVA: 0x000F8948 File Offset: 0x000F6B48
	private void CheckDeactivatePlaySpellForSpellPlayBlock(PowerTaskList taskList)
	{
		if (taskList.GetOrigin() != taskList)
		{
			return;
		}
		PowerTaskList powerTaskList = (this.GetPowerQueue().Count > 0) ? this.GetPowerQueue().Peek() : null;
		if (powerTaskList != null && powerTaskList.GetParent() == taskList)
		{
			return;
		}
		global::Entity sourceEntity = taskList.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			return;
		}
		if (sourceEntity.GetCardType() != TAG_CARDTYPE.SPELL)
		{
			return;
		}
		Card card = sourceEntity.GetCard();
		if (card == null)
		{
			return;
		}
		card.DeactivatePlaySpell();
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x000F89B8 File Offset: 0x000F6BB8
	private void CheckDeactivatePlaySpellForTransformation(PowerTaskList taskList)
	{
		if (taskList.GetBlockEnd() == null)
		{
			return;
		}
		PowerTaskList powerTaskList = (this.GetPowerQueue().Count > 0) ? this.GetPowerQueue().Peek() : null;
		if (powerTaskList != null && powerTaskList.GetParent() == taskList)
		{
			return;
		}
		global::Entity sourceEntity = taskList.GetSourceEntity(true);
		if (sourceEntity == null)
		{
			return;
		}
		if (!sourceEntity.HasTag(GAME_TAG.TRANSFORMED_FROM_CARD))
		{
			return;
		}
		if (sourceEntity.GetCardType() != TAG_CARDTYPE.SPELL)
		{
			return;
		}
		Card card = sourceEntity.GetCard();
		if (card == null)
		{
			return;
		}
		card.DeactivatePlaySpell();
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x000F8A34 File Offset: 0x000F6C34
	private bool DoSubSpellTaskListWithController(PowerTaskList taskList)
	{
		if (this.m_subSpellController == null)
		{
			this.m_subSpellController = this.CreateSpellController<SubSpellController>(null, "SubSpellController.prefab:34966ff41154fce469d3ccb6d3b1655e");
		}
		if (!this.m_subSpellController.AttachPowerTaskList(taskList))
		{
			return false;
		}
		this.m_subSpellController.AddFinishedTaskListCallback(new SpellController.FinishedTaskListCallback(this.OnSpellControllerFinishedTaskList));
		this.m_subSpellController.DoPowerTaskList();
		return true;
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x000F8A94 File Offset: 0x000F6C94
	private bool DoTaskListUsingController(SpellController spellController, PowerTaskList taskList)
	{
		if (spellController == null)
		{
			Log.Power.Print("PowerProcessor.DoTaskListUsingController() - spellController=null", Array.Empty<object>());
			return false;
		}
		if (!spellController.AttachPowerTaskList(taskList))
		{
			return false;
		}
		spellController.AddFinishedTaskListCallback(new SpellController.FinishedTaskListCallback(this.OnSpellControllerFinishedTaskList));
		spellController.AddFinishedCallback(new SpellController.FinishedCallback(this.OnSpellControllerFinished));
		spellController.DoPowerTaskList();
		return true;
	}

	// Token: 0x06003059 RID: 12377 RVA: 0x000F8AF6 File Offset: 0x000F6CF6
	private void OnSpellControllerFinishedTaskList(SpellController spellController)
	{
		spellController.DetachPowerTaskList();
		if (this.m_currentTaskList == null)
		{
			return;
		}
		this.DoCurrentTaskList();
	}

	// Token: 0x0600305A RID: 12378 RVA: 0x000F8B0E File Offset: 0x000F6D0E
	private void OnSpellControllerFinished(SpellController spellController)
	{
		this.DestroySpellController(spellController);
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x000F8B18 File Offset: 0x000F6D18
	private AttackSpellController CreateAttackSpellController(PowerTaskList taskList)
	{
		string prefabPath = "AttackSpellController.prefab:12acecc85ac575e43b87ec141b89269a";
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && !string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAttackSpellControllerOverride(taskList.GetAttacker())))
		{
			prefabPath = GameState.Get().GetGameEntity().GetAttackSpellControllerOverride(taskList.GetAttacker());
		}
		return this.CreateSpellController<AttackSpellController>(taskList, prefabPath);
	}

	// Token: 0x0600305C RID: 12380 RVA: 0x000F8B78 File Offset: 0x000F6D78
	private MoveMinionSpellController CreateMoveMinionSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<MoveMinionSpellController>(taskList, null);
	}

	// Token: 0x0600305D RID: 12381 RVA: 0x000F8B82 File Offset: 0x000F6D82
	private SecretSpellController CreateSecretSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<SecretSpellController>(taskList, "SecretSpellController.prefab:553af99c12154c547bc05dc3d9832931");
	}

	// Token: 0x0600305E RID: 12382 RVA: 0x000F8B90 File Offset: 0x000F6D90
	private SigilSpellController CreateSigilSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<SigilSpellController>(taskList, "SigilSpellController.prefab:1f80634fbf70a654bbae7bf796bf11b2");
	}

	// Token: 0x0600305F RID: 12383 RVA: 0x000F8B9E File Offset: 0x000F6D9E
	private SideQuestSpellController CreateSideQuestSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<SideQuestSpellController>(taskList, "SideQuestSpellController.prefab:63762d08481f04642bbf3cde299feea2");
	}

	// Token: 0x06003060 RID: 12384 RVA: 0x000F8BAC File Offset: 0x000F6DAC
	private PowerSpellController CreatePowerSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<PowerSpellController>(taskList, null);
	}

	// Token: 0x06003061 RID: 12385 RVA: 0x000F8BB6 File Offset: 0x000F6DB6
	private TriggerSpellController CreateTriggerSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<TriggerSpellController>(taskList, "TriggerSpellController.prefab:e0a2661f98a720d47ad4b85de228f4b4");
	}

	// Token: 0x06003062 RID: 12386 RVA: 0x000F8BC4 File Offset: 0x000F6DC4
	private DeathSpellController CreateDeathSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<DeathSpellController>(taskList, null);
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x000F8BCE File Offset: 0x000F6DCE
	private FatigueSpellController CreateFatigueSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<FatigueSpellController>(taskList, null);
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x000F8BD8 File Offset: 0x000F6DD8
	private JoustSpellController CreateJoustSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<JoustSpellController>(taskList, "JoustSpellController.prefab:89ac256005a4a8a46939a84460c2c221");
	}

	// Token: 0x06003065 RID: 12389 RVA: 0x000F8BE6 File Offset: 0x000F6DE6
	private RitualSpellController CreateRitualSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<RitualSpellController>(taskList, "RitualSpellController.prefab:27c7bd4ffaa54fb4e9e64dad14a6e701");
	}

	// Token: 0x06003066 RID: 12390 RVA: 0x000F8BF4 File Offset: 0x000F6DF4
	private RevealCardSpellController CreateRevealCardSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<RevealCardSpellController>(taskList, "RevealCardSpellController.prefab:17fd7ea79bfd4c24389d535a074199b6");
	}

	// Token: 0x06003067 RID: 12391 RVA: 0x000F8C02 File Offset: 0x000F6E02
	private ResetGameSpellController CreateResetGameSpellController(PowerTaskList taskList)
	{
		return this.CreateSpellController<ResetGameSpellController>(taskList, "ResetGameSpellController.prefab:d8c1994d523574e42bffa17990917754");
	}

	// Token: 0x06003068 RID: 12392 RVA: 0x000F8C10 File Offset: 0x000F6E10
	private T CreateSpellController<T>(PowerTaskList taskList = null, string prefabPath = null) where T : SpellController
	{
		GameObject gameObject;
		T result;
		if (prefabPath == null)
		{
			gameObject = new GameObject();
			result = gameObject.AddComponent<T>();
		}
		else
		{
			gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath, AssetLoadingOptions.None);
			result = gameObject.GetComponent<T>();
		}
		if (taskList != null)
		{
			gameObject.name = string.Format("{0} [taskListId={1}]", typeof(T), taskList.GetId());
		}
		else
		{
			gameObject.name = string.Format("{0}", typeof(T));
		}
		return result;
	}

	// Token: 0x06003069 RID: 12393 RVA: 0x000CDD07 File Offset: 0x000CBF07
	private void DestroySpellController(SpellController spellController)
	{
		UnityEngine.Object.Destroy(spellController.gameObject);
	}

	// Token: 0x04001AC5 RID: 6853
	private const string ATTACK_SPELL_CONTROLLER_PREFAB_PATH = "AttackSpellController.prefab:12acecc85ac575e43b87ec141b89269a";

	// Token: 0x04001AC6 RID: 6854
	private const string SECRET_SPELL_CONTROLLER_PREFAB_PATH = "SecretSpellController.prefab:553af99c12154c547bc05dc3d9832931";

	// Token: 0x04001AC7 RID: 6855
	private const string SIDE_QUEST_SPELL_CONTROLLER_PREFAB_PATH = "SideQuestSpellController.prefab:63762d08481f04642bbf3cde299feea2";

	// Token: 0x04001AC8 RID: 6856
	private const string SIGIL_SPELL_CONTROLLER_PREFAB_PATH = "SigilSpellController.prefab:1f80634fbf70a654bbae7bf796bf11b2";

	// Token: 0x04001AC9 RID: 6857
	private const string JOUST_SPELL_CONTROLLER_PREFAB_PATH = "JoustSpellController.prefab:89ac256005a4a8a46939a84460c2c221";

	// Token: 0x04001ACA RID: 6858
	private const string RITUAL_SPELL_CONTROLLER_PREFAB_PATH = "RitualSpellController.prefab:27c7bd4ffaa54fb4e9e64dad14a6e701";

	// Token: 0x04001ACB RID: 6859
	private const string REVEAL_CARD_SPELL_CONTROLLER_PREFAB_PATH = "RevealCardSpellController.prefab:17fd7ea79bfd4c24389d535a074199b6";

	// Token: 0x04001ACC RID: 6860
	private const string TRIGGER_SPELL_CONTROLLER_PREFAB_PATH = "TriggerSpellController.prefab:e0a2661f98a720d47ad4b85de228f4b4";

	// Token: 0x04001ACD RID: 6861
	private const string RESET_GAME_SPELL_CONTROLLER_PREFAB_PATH = "ResetGameSpellController.prefab:d8c1994d523574e42bffa17990917754";

	// Token: 0x04001ACE RID: 6862
	private const string SUB_SPELL_CONTROLLER_PREFAB_PATH = "SubSpellController.prefab:34966ff41154fce469d3ccb6d3b1655e";

	// Token: 0x04001ACF RID: 6863
	private const string INVOKE_SPELL_CONTROLLER_PREFAB_PATH = "InvokeSpellController.prefab:333b9273e033dd348ab0d5f81a5bbbcd";

	// Token: 0x04001AD0 RID: 6864
	private int m_nextTaskListId = 1;

	// Token: 0x04001AD1 RID: 6865
	private bool m_buildingTaskList;

	// Token: 0x04001AD2 RID: 6866
	private int m_totalSlushTime;

	// Token: 0x04001AD3 RID: 6867
	private PowerHistoryTimeline m_currentTimeline;

	// Token: 0x04001AD4 RID: 6868
	private List<PowerProcessor.OnTaskEvent> m_taskEventListeners = new List<PowerProcessor.OnTaskEvent>();

	// Token: 0x04001AD5 RID: 6869
	private Stack<PowerTaskList> m_previousStack = new Stack<PowerTaskList>();

	// Token: 0x04001AD6 RID: 6870
	private Stack<List<PowerTaskList>> m_deferredStack = new Stack<List<PowerTaskList>>();

	// Token: 0x04001AD7 RID: 6871
	private Stack<PowerTaskList> m_subSpellOriginStack = new Stack<PowerTaskList>();

	// Token: 0x04001AD8 RID: 6872
	private Queue<PowerProcessor.DelayedRealTimeTask> m_delayedRealTimeTasks = new Queue<PowerProcessor.DelayedRealTimeTask>();

	// Token: 0x04001AD9 RID: 6873
	private PowerQueue m_powerQueue = new PowerQueue();

	// Token: 0x04001ADA RID: 6874
	private PowerTaskList m_currentTaskList;

	// Token: 0x04001ADB RID: 6875
	private PowerTaskList m_previousTaskList;

	// Token: 0x04001ADC RID: 6876
	private SubSpellController m_subSpellController;

	// Token: 0x04001ADD RID: 6877
	private bool m_historyBlocking;

	// Token: 0x04001ADE RID: 6878
	private bool m_artificialPauseFromMetadata;

	// Token: 0x04001ADF RID: 6879
	private PowerTaskList m_historyBlockingTaskList;

	// Token: 0x04001AE0 RID: 6880
	private PowerTaskList m_busyTaskList;

	// Token: 0x04001AE1 RID: 6881
	private PowerTaskList m_earlyConcedeTaskList;

	// Token: 0x04001AE2 RID: 6882
	private bool m_handledFirstEarlyConcede;

	// Token: 0x04001AE3 RID: 6883
	private PowerTaskList m_gameOverTaskList;

	// Token: 0x04001AE4 RID: 6884
	private List<PowerHistoryTimeline> m_powerHistoryTimeline = new List<PowerHistoryTimeline>();

	// Token: 0x04001AE5 RID: 6885
	private Map<int, int> m_powerHistoryTimelineIdIndex = new Map<int, int>();

	// Token: 0x04001AE6 RID: 6886
	private PowerTaskList m_powerHistoryFirstTaskList;

	// Token: 0x04001AE7 RID: 6887
	private PowerTaskList m_powerHistoryLastTaskList;

	// Token: 0x020016E1 RID: 5857
	// (Invoke) Token: 0x0600E5ED RID: 58861
	public delegate void OnTaskEvent(float scheduleDiff);

	// Token: 0x020016E2 RID: 5858
	private class DelayedRealTimeTask
	{
		// Token: 0x0400B2AE RID: 45742
		public PowerTask m_powerTask;

		// Token: 0x0400B2AF RID: 45743
		public List<Network.PowerHistory> m_powerHistory;

		// Token: 0x0400B2B0 RID: 45744
		public int m_index;
	}
}
