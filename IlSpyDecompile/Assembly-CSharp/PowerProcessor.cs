using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using PegasusGame;
using UnityEngine;

public class PowerProcessor
{
	public delegate void OnTaskEvent(float scheduleDiff);

	private class DelayedRealTimeTask
	{
		public PowerTask m_powerTask;

		public List<Network.PowerHistory> m_powerHistory;

		public int m_index;
	}

	private const string ATTACK_SPELL_CONTROLLER_PREFAB_PATH = "AttackSpellController.prefab:12acecc85ac575e43b87ec141b89269a";

	private const string SECRET_SPELL_CONTROLLER_PREFAB_PATH = "SecretSpellController.prefab:553af99c12154c547bc05dc3d9832931";

	private const string SIDE_QUEST_SPELL_CONTROLLER_PREFAB_PATH = "SideQuestSpellController.prefab:63762d08481f04642bbf3cde299feea2";

	private const string SIGIL_SPELL_CONTROLLER_PREFAB_PATH = "SigilSpellController.prefab:1f80634fbf70a654bbae7bf796bf11b2";

	private const string JOUST_SPELL_CONTROLLER_PREFAB_PATH = "JoustSpellController.prefab:89ac256005a4a8a46939a84460c2c221";

	private const string RITUAL_SPELL_CONTROLLER_PREFAB_PATH = "RitualSpellController.prefab:27c7bd4ffaa54fb4e9e64dad14a6e701";

	private const string REVEAL_CARD_SPELL_CONTROLLER_PREFAB_PATH = "RevealCardSpellController.prefab:17fd7ea79bfd4c24389d535a074199b6";

	private const string TRIGGER_SPELL_CONTROLLER_PREFAB_PATH = "TriggerSpellController.prefab:e0a2661f98a720d47ad4b85de228f4b4";

	private const string RESET_GAME_SPELL_CONTROLLER_PREFAB_PATH = "ResetGameSpellController.prefab:d8c1994d523574e42bffa17990917754";

	private const string SUB_SPELL_CONTROLLER_PREFAB_PATH = "SubSpellController.prefab:34966ff41154fce469d3ccb6d3b1655e";

	private const string INVOKE_SPELL_CONTROLLER_PREFAB_PATH = "InvokeSpellController.prefab:333b9273e033dd348ab0d5f81a5bbbcd";

	private int m_nextTaskListId = 1;

	private bool m_buildingTaskList;

	private int m_totalSlushTime;

	private PowerHistoryTimeline m_currentTimeline;

	private List<OnTaskEvent> m_taskEventListeners = new List<OnTaskEvent>();

	private Stack<PowerTaskList> m_previousStack = new Stack<PowerTaskList>();

	private Stack<List<PowerTaskList>> m_deferredStack = new Stack<List<PowerTaskList>>();

	private Stack<PowerTaskList> m_subSpellOriginStack = new Stack<PowerTaskList>();

	private Queue<DelayedRealTimeTask> m_delayedRealTimeTasks = new Queue<DelayedRealTimeTask>();

	private PowerQueue m_powerQueue = new PowerQueue();

	private PowerTaskList m_currentTaskList;

	private PowerTaskList m_previousTaskList;

	private SubSpellController m_subSpellController;

	private bool m_historyBlocking;

	private bool m_artificialPauseFromMetadata;

	private PowerTaskList m_historyBlockingTaskList;

	private PowerTaskList m_busyTaskList;

	private PowerTaskList m_earlyConcedeTaskList;

	private bool m_handledFirstEarlyConcede;

	private PowerTaskList m_gameOverTaskList;

	private List<PowerHistoryTimeline> m_powerHistoryTimeline = new List<PowerHistoryTimeline>();

	private Map<int, int> m_powerHistoryTimelineIdIndex = new Map<int, int>();

	private PowerTaskList m_powerHistoryFirstTaskList;

	private PowerTaskList m_powerHistoryLastTaskList;

	public PowerProcessor()
	{
		m_deferredStack.Push(new List<PowerTaskList>());
	}

	public void Clear()
	{
		m_powerQueue.Clear();
		m_currentTaskList = null;
	}

	public bool IsBuildingTaskList()
	{
		return m_buildingTaskList;
	}

	public PowerTaskList GetCurrentTaskList()
	{
		return m_currentTaskList;
	}

	public PowerQueue GetPowerQueue()
	{
		return m_powerQueue;
	}

	public void AddTaskEventListener(OnTaskEvent listener)
	{
		m_taskEventListeners.Add(listener);
	}

	public void RemoveTaskEventListener(OnTaskEvent listener)
	{
		m_taskEventListeners.Remove(listener);
	}

	public void FireTaskEvent(float expectedDiff)
	{
		OnTaskEvent[] array = m_taskEventListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](expectedDiff);
		}
	}

	public void OnMetaData(Network.HistMetaData metaData)
	{
		if (metaData.MetaType == HistoryMeta.Type.SHOW_BIG_CARD)
		{
			int data = metaData.Data;
			Player player = GameState.Get().GetPlayer(data);
			if (player != null && player.GetSide() != Player.Side.FRIENDLY && InputManager.Get().PermitDecisionMakingInput())
			{
				return;
			}
			int id = metaData.Info[0];
			Entity entity = GameState.Get().GetEntity(id);
			if (entity != null && !string.IsNullOrEmpty(entity.GetCardId()))
			{
				SetHistoryBlockingTaskList();
				Entity sourceEntity = m_currentTaskList.GetSourceEntity();
				HistoryBlock.Type blockType = m_currentTaskList.GetBlockType();
				if (sourceEntity != null && sourceEntity.HasTag(GAME_TAG.FAST_BATTLECRY) && blockType == HistoryBlock.Type.POWER)
				{
					HistoryManager.Get().CreateFastBigCardFromMetaData(entity);
					return;
				}
				int displayTimeMS = ((metaData.Info.Count > 1) ? metaData.Info[1] : 0);
				HistoryManager.Get().CreatePlayedBigCard(entity, OnBigCardStarted, OnBigCardFinished, fromMetaData: true, countered: false, displayTimeMS);
			}
		}
		else if (metaData.MetaType == HistoryMeta.Type.BEGIN_LISTENING_FOR_TURN_EVENTS)
		{
			TurnStartManager.Get().BeginListeningForTurnEvents(fromMetadata: true);
		}
		else if (metaData.MetaType == HistoryMeta.Type.ARTIFICIAL_PAUSE)
		{
			int data2 = metaData.Data;
			if (Gameplay.Get() != null)
			{
				Gameplay.Get().StartCoroutine(ArtificiallyPausePowerProcessor(data2));
			}
		}
	}

	public IEnumerator ArtificiallyPausePowerProcessor(float pauseTimeMS)
	{
		m_artificialPauseFromMetadata = true;
		float timeToWait = pauseTimeMS / 1000f;
		float timeWaited = 0f;
		if (timeToWait > 0f)
		{
			GameState.Get().GetFriendlySidePlayer().GetHandZone()
				.UpdateLayout();
			GameState.Get().GetOpposingSidePlayer().GetHandZone()
				.UpdateLayout();
			GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
				.UpdateLayout();
			GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
				.UpdateLayout();
		}
		for (; timeWaited < timeToWait; timeWaited += Time.deltaTime)
		{
			yield return null;
		}
		m_artificialPauseFromMetadata = false;
	}

	public bool IsHistoryBlocking()
	{
		return m_historyBlocking;
	}

	public PowerTaskList GetHistoryBlockingTaskList()
	{
		return m_historyBlockingTaskList;
	}

	public void SetHistoryBlockingTaskList()
	{
		if (m_historyBlockingTaskList == null)
		{
			m_historyBlockingTaskList = m_currentTaskList;
		}
	}

	public void ForceStopHistoryBlocking()
	{
		m_historyBlocking = false;
		m_historyBlockingTaskList = null;
	}

	public PowerHistoryTimeline GetCurrentTimeline()
	{
		return m_currentTimeline;
	}

	public PowerTaskList GetLatestUnendedTaskList()
	{
		int count = m_powerQueue.Count;
		if (count == 0)
		{
			return m_currentTaskList;
		}
		return m_powerQueue[count - 1];
	}

	public PowerTaskList GetLastTaskList()
	{
		int count = m_powerQueue.Count;
		if (count > 0)
		{
			return m_powerQueue[count - 1];
		}
		return m_currentTaskList;
	}

	public PowerTaskList GetEarlyConcedeTaskList()
	{
		return m_earlyConcedeTaskList;
	}

	public bool HasEarlyConcedeTaskList()
	{
		return m_earlyConcedeTaskList != null;
	}

	public PowerTaskList GetGameOverTaskList()
	{
		return m_gameOverTaskList;
	}

	public bool HasGameOverTaskList()
	{
		return m_gameOverTaskList != null;
	}

	public bool CanDoRealTimeTask()
	{
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		if (gameState.IsResetGamePending())
		{
			return false;
		}
		return true;
	}

	public bool CanDoTask(PowerTask task)
	{
		if (task.IsCompleted())
		{
			return true;
		}
		Network.PowerHistory power = task.GetPower();
		if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == HistoryMeta.Type.SHOW_BIG_CARD && HistoryManager.Get().IsShowingBigCard())
		{
			return false;
		}
		if (GameState.Get().IsBusy())
		{
			return false;
		}
		if (m_artificialPauseFromMetadata)
		{
			return false;
		}
		return true;
	}

	public void ForEachTaskList(Action<int, PowerTaskList> predicate)
	{
		if (m_currentTaskList != null)
		{
			predicate(-1, m_currentTaskList);
		}
		for (int i = 0; i < m_powerQueue.Count; i++)
		{
			predicate(i, m_powerQueue[i]);
		}
	}

	public bool HasTaskLists()
	{
		if (m_currentTaskList != null)
		{
			return true;
		}
		if (m_powerQueue.Count > 0)
		{
			return true;
		}
		return false;
	}

	public bool HasTaskList(PowerTaskList taskList)
	{
		if (taskList == null)
		{
			return false;
		}
		if (m_currentTaskList == taskList)
		{
			return true;
		}
		if (m_powerQueue.Contains(taskList))
		{
			return true;
		}
		return false;
	}

	public void OnPowerHistory(List<Network.PowerHistory> powerList)
	{
		m_totalSlushTime = 0;
		m_buildingTaskList = true;
		m_powerHistoryFirstTaskList = null;
		m_powerHistoryLastTaskList = null;
		m_currentTimeline = new PowerHistoryTimeline();
		for (int i = 0; i < powerList.Count; i++)
		{
			PowerTaskList powerTaskList = new PowerTaskList();
			if (m_previousStack.Count > 0)
			{
				PowerTaskList previous = m_previousStack.Pop();
				powerTaskList.SetPrevious(previous);
				m_previousStack.Push(powerTaskList);
			}
			if (m_subSpellOriginStack.Count > 0)
			{
				PowerTaskList powerTaskList2 = m_subSpellOriginStack.Peek();
				if (powerTaskList.GetOrigin() == powerTaskList2.GetOrigin())
				{
					powerTaskList.SetSubSpellOrigin(powerTaskList2);
				}
			}
			BuildTaskList(powerList, ref i, powerTaskList);
		}
		if (GameState.Get().AllowBatchedPowers())
		{
			for (int num = m_powerQueue.Count - 1; num > 0; num--)
			{
				PowerTaskList powerTaskList3 = m_powerQueue[num];
				if (powerTaskList3.IsBatchable())
				{
					int num2 = num - 1;
					PowerTaskList powerTaskList4 = m_powerQueue[num2];
					while ((powerTaskList4.IsSlushTimeHelper() || !powerTaskList4.HasAnyTasksInImmediate()) && num2 > 0)
					{
						powerTaskList4 = m_powerQueue[--num2];
					}
					if (powerTaskList3.IsBatchable() && powerTaskList4.IsBatchable())
					{
						powerTaskList3.FillMetaDataTargetSourceData();
						powerTaskList4.FillMetaDataTargetSourceData();
						powerTaskList4.AddTasks(powerTaskList3);
						foreach (int entity in powerTaskList3.GetBlockStart().Entities)
						{
							if (!powerTaskList4.GetBlockStart().Entities.Contains(entity))
							{
								powerTaskList4.GetBlockStart().Entities.Add(entity);
							}
						}
						m_powerQueue.RemoveAt(num);
					}
				}
			}
		}
		if (GameState.Get().AllowDeferredPowers())
		{
			FixUpOutOfOrderDeferredTasks();
			for (int num3 = m_powerQueue.Count - 1; num3 > 0; num3--)
			{
				PowerTaskList powerTaskList5 = m_powerQueue[num3];
				if (powerTaskList5.GetPrevious() == m_powerQueue[num3 - 1] && powerTaskList5.IsCollapsible(isEarlier: false) && powerTaskList5.GetPrevious().IsCollapsible(isEarlier: true))
				{
					powerTaskList5.GetPrevious().AddTasks(powerTaskList5);
					powerTaskList5.GetPrevious().SetNext(null);
					if (powerTaskList5.GetBlockEnd() != null)
					{
						powerTaskList5.GetPrevious().SetBlockEnd(powerTaskList5.GetBlockEnd());
					}
					foreach (PowerTaskList item in m_powerQueue)
					{
						if (item.GetPrevious() == powerTaskList5)
						{
							item.SetPrevious(powerTaskList5.GetPrevious());
						}
					}
					m_powerQueue.RemoveAt(num3);
				}
			}
		}
		if (m_totalSlushTime > 0 && m_powerHistoryFirstTaskList != null && m_powerHistoryLastTaskList != null)
		{
			PowerTaskList powerHistoryFirstTaskList = m_powerHistoryFirstTaskList;
			PowerTaskList powerHistoryLastTaskList = m_powerHistoryLastTaskList;
			powerHistoryFirstTaskList.SetHistoryBlockStart(isStart: true);
			powerHistoryLastTaskList.SetHistoryBlockEnd(isEnd: true);
			m_currentTimeline.m_firstTaskId = powerHistoryFirstTaskList.GetId();
			m_currentTimeline.m_lastTaskId = powerHistoryLastTaskList.GetId();
			m_currentTimeline.m_slushTime = m_totalSlushTime;
			m_powerHistoryTimeline.Add(m_currentTimeline);
			m_powerHistoryTimelineIdIndex.Add(m_currentTimeline.m_firstTaskId, m_powerHistoryTimeline.Count - 1);
			m_powerHistoryTimelineIdIndex.Add(m_currentTimeline.m_lastTaskId, m_powerHistoryTimeline.Count - 1);
			foreach (PowerHistoryTimelineEntry orderedEvent in m_currentTimeline.m_orderedEvents)
			{
				if (!m_powerHistoryTimelineIdIndex.ContainsKey(orderedEvent.taskId))
				{
					m_powerHistoryTimelineIdIndex.Add(orderedEvent.taskId, m_powerHistoryTimeline.Count - 1);
				}
			}
		}
		m_buildingTaskList = false;
	}

	private void FixUpOutOfOrderDeferredTasks()
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return;
		}
		for (int num = m_powerQueue.Count - 1; num >= 0; num--)
		{
			PowerTaskList powerTaskList = m_powerQueue[num];
			if (powerTaskList.IsDeferrable())
			{
				FixUpOutOfOrderDeferredTasksInTasklist(powerTaskList);
			}
		}
	}

	private void FixUpOutOfOrderDeferredTasksInTasklist(PowerTaskList deferredTaskList)
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return;
		}
		Map<int, Map<int, List<int>>> entityChangesForTaskList = GetEntityChangesForTaskList(deferredTaskList);
		for (int i = 0; i < m_powerQueue.Count; i++)
		{
			PowerTaskList powerTaskList = m_powerQueue[i];
			if (powerTaskList.GetId() == deferredTaskList.GetId())
			{
				break;
			}
			if (powerTaskList.GetId() < deferredTaskList.GetDeferredSourceId())
			{
				continue;
			}
			if (powerTaskList.IsDeferrable())
			{
				break;
			}
			Map<int, Map<int, List<int>>> entityChangesForTaskList2 = GetEntityChangesForTaskList(powerTaskList);
			foreach (KeyValuePair<int, Map<int, List<int>>> item in entityChangesForTaskList)
			{
				int key = item.Key;
				Map<int, List<int>> value = item.Value;
				if (!entityChangesForTaskList2.ContainsKey(key))
				{
					continue;
				}
				Map<int, List<int>> map = entityChangesForTaskList2[key];
				foreach (KeyValuePair<int, List<int>> item2 in value)
				{
					int key2 = item2.Key;
					List<int> value2 = item2.Value;
					if (map.ContainsKey(key2))
					{
						List<int> list = map[key2];
						int newValue = value2[value2.Count - 1];
						int newValue2 = list[list.Count - 1];
						deferredTaskList.FixupLastTagChangeForEntityTag(key, key2, newValue2);
						powerTaskList.FixupLastTagChangeForEntityTag(key, key2, newValue, fixLast: false);
					}
				}
			}
		}
	}

	private Map<int, Map<int, List<int>>> GetEntityChangesForTaskList(PowerTaskList taskList)
	{
		Map<int, Map<int, List<int>>> map = new Map<int, Map<int, List<int>>>();
		foreach (PowerTask tagChangeTask in taskList.GetTagChangeTasks())
		{
			Network.HistTagChange histTagChange = tagChangeTask.GetPower() as Network.HistTagChange;
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

	public void HandleTimelineStartEvent(int tasklistId, float time, bool isBlockStart, Network.HistBlockStart blockStart)
	{
		if (!m_powerHistoryTimelineIdIndex.ContainsKey(tasklistId))
		{
			return;
		}
		int index = m_powerHistoryTimelineIdIndex[tasklistId];
		PowerHistoryTimeline powerHistoryTimeline = m_powerHistoryTimeline[index];
		if (isBlockStart)
		{
			powerHistoryTimeline.m_startTime = time;
			if (!HearthstoneApplication.IsPublic())
			{
				Debug.Log($"Timeline start event: (TasklistId: {tasklistId}) ---- (Expected Duration: {(float)powerHistoryTimeline.m_slushTime * 0.001f})");
			}
		}
		if (powerHistoryTimeline.m_orderedEventIndexLookup.ContainsKey(tasklistId))
		{
			int index2 = powerHistoryTimeline.m_orderedEventIndexLookup[tasklistId];
			PowerHistoryTimelineEntry powerHistoryTimelineEntry = powerHistoryTimeline.m_orderedEvents[index2];
			powerHistoryTimelineEntry.entityId = blockStart?.Entities[0] ?? 0;
			float num = (float)powerHistoryTimelineEntry.expectedStartOffset * 0.001f;
			float num2 = (powerHistoryTimelineEntry.actualStartTime = time - powerHistoryTimeline.m_startTime);
			FireTaskEvent(num2 - num);
			if (!HearthstoneApplication.IsPublic())
			{
				Debug.Log($"Task start event: (TasklistId: {tasklistId}) ---- (Expected: {num} ---- (Actual: {num2}))");
			}
		}
	}

	public void HandleTimelineEndEvent(int tasklistId, float time, bool isBlockEnd)
	{
		if (!m_powerHistoryTimelineIdIndex.ContainsKey(tasklistId))
		{
			return;
		}
		int index = m_powerHistoryTimelineIdIndex[tasklistId];
		PowerHistoryTimeline powerHistoryTimeline = m_powerHistoryTimeline[index];
		if (powerHistoryTimeline.m_orderedEventIndexLookup.ContainsKey(tasklistId))
		{
			int index2 = powerHistoryTimeline.m_orderedEventIndexLookup[tasklistId];
			PowerHistoryTimelineEntry powerHistoryTimelineEntry = powerHistoryTimeline.m_orderedEvents[index2];
			float num = (float)(powerHistoryTimelineEntry.expectedStartOffset + powerHistoryTimelineEntry.expectedTime) * 0.001f;
			float num2 = time - powerHistoryTimeline.m_startTime;
			FireTaskEvent(num2 - num);
			if (!HearthstoneApplication.IsPublic())
			{
				Debug.Log($"Task end event: (TasklistId: {tasklistId}) ---- (Expected: {num} ---- (Actual: {num2}))");
				SceneDebugger.Get().AddSlushTimeEntry(tasklistId, (float)powerHistoryTimelineEntry.expectedStartOffset * 0.001f, num, powerHistoryTimelineEntry.actualStartTime, num2, powerHistoryTimelineEntry.entityId);
			}
		}
		if (isBlockEnd)
		{
			powerHistoryTimeline.m_endTime = time;
			if (!HearthstoneApplication.IsPublic())
			{
				Debug.Log($"Timeline end event: (TasklistId: {tasklistId}) ---- (Expected: {(float)powerHistoryTimeline.m_slushTime * 0.001f}) ---- (Actual: {powerHistoryTimeline.m_endTime - powerHistoryTimeline.m_startTime})");
			}
		}
	}

	public void ProcessPowerQueue()
	{
		while (GameState.Get().CanProcessPowerQueue())
		{
			if (m_busyTaskList != null)
			{
				m_busyTaskList = null;
			}
			else
			{
				PowerTaskList powerTaskList = m_powerQueue.Peek();
				if (HistoryManager.Get() != null && HistoryManager.Get().IsShowingBigCard())
				{
					if ((m_historyBlockingTaskList != null && !powerTaskList.IsDescendantOfBlock(m_historyBlockingTaskList)) || m_historyBlockingTaskList == null)
					{
						break;
					}
				}
				else
				{
					m_historyBlockingTaskList = null;
				}
				OnWillProcessTaskList(powerTaskList);
				if (GameState.Get().IsBusy())
				{
					m_busyTaskList = powerTaskList;
					break;
				}
			}
			if (CanEarlyConcede())
			{
				if (m_earlyConcedeTaskList == null && !m_handledFirstEarlyConcede)
				{
					DoEarlyConcedeVisuals();
					m_handledFirstEarlyConcede = true;
				}
				while (m_powerQueue.Count > 0)
				{
					m_currentTaskList = m_powerQueue.Dequeue();
					m_currentTaskList.DebugDump();
					CancelSpellsForEarlyConcede(m_currentTaskList);
					m_currentTaskList.DoEarlyConcedeTasks();
					m_currentTaskList = null;
				}
				break;
			}
			m_currentTaskList = m_powerQueue.Dequeue();
			if (m_previousTaskList == null || m_previousTaskList.GetOrigin() != m_currentTaskList.GetOrigin() || m_previousTaskList.GetParent() != m_currentTaskList.GetParent())
			{
				GameState.Get().ResetFriendlyCardDrawCounter();
				GameState.Get().ResetOpponentCardDrawCounter();
			}
			m_currentTaskList.DebugDump();
			OnProcessTaskList();
			StartCurrentTaskList();
		}
	}

	private int GetNextTaskListId()
	{
		int nextTaskListId = m_nextTaskListId;
		m_nextTaskListId = ((m_nextTaskListId == int.MaxValue) ? 1 : (m_nextTaskListId + 1));
		return nextTaskListId;
	}

	private bool CanDeferTaskList(Network.PowerHistory power)
	{
		if (!GameState.Get().AllowDeferredPowers())
		{
			return false;
		}
		return (power as Network.HistBlockStart)?.IsDeferrable ?? false;
	}

	private bool CanBatchTaskList(Network.PowerHistory power)
	{
		if (!GameState.Get().AllowBatchedPowers())
		{
			return false;
		}
		return (power as Network.HistBlockStart)?.IsBatchable ?? false;
	}

	private bool IsDeferBlockerTaskList(Network.PowerHistory power)
	{
		Network.HistBlockStart histBlockStart = power as Network.HistBlockStart;
		if (histBlockStart != null)
		{
			if (!histBlockStart.IsDeferBlocker && (histBlockStart.BlockType != HistoryBlock.Type.TRIGGER || histBlockStart.IsDeferrable))
			{
				if (histBlockStart.BlockType == HistoryBlock.Type.ATTACK)
				{
					return !histBlockStart.IsDeferrable;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	private void BuildTaskList(List<Network.PowerHistory> powerList, ref int index, PowerTaskList taskList)
	{
		for (; index < powerList.Count; index++)
		{
			Network.PowerHistory powerHistory = powerList[index];
			Network.PowerType type = powerHistory.Type;
			if (type == Network.PowerType.BLOCK_START)
			{
				if (!taskList.IsEmpty())
				{
					EnqueueTaskList(taskList);
					if (taskList.IsDeferrable())
					{
						taskList.SetDeferrable(deferrable: false);
						List<PowerTaskList> item = m_deferredStack.Pop();
						if (m_deferredStack.Count > 0 && m_deferredStack.Peek().Contains(taskList))
						{
							m_deferredStack.Peek().Remove(taskList);
						}
						m_deferredStack.Push(item);
					}
				}
				PowerTaskList powerTaskList = new PowerTaskList();
				powerTaskList.SetBlockStart((Network.HistBlockStart)powerHistory);
				PowerTaskList origin = taskList.GetOrigin();
				if (origin.IsStartOfBlock())
				{
					powerTaskList.SetParent(origin);
				}
				m_previousStack.Push(powerTaskList);
				if (IsDeferBlockerTaskList(powerHistory))
				{
					EnqueueDeferredTaskLists(combine: false);
					m_deferredStack.Push(new List<PowerTaskList>());
				}
				if (CanDeferTaskList(powerHistory))
				{
					m_deferredStack.Peek().Add(powerTaskList);
					powerTaskList.SetDeferrable(deferrable: true);
				}
				else
				{
					powerTaskList.SetBatchable(CanBatchTaskList(powerHistory));
				}
				m_deferredStack.Push(new List<PowerTaskList>());
				index++;
				BuildTaskList(powerList, ref index, powerTaskList);
				return;
			}
			if (type == Network.PowerType.BLOCK_END)
			{
				taskList.SetBlockEnd((Network.HistBlockEnd)powerHistory);
				if (m_previousStack.Count <= 0)
				{
					break;
				}
				m_previousStack.Pop();
				if (!taskList.IsDeferrable())
				{
					EnqueueTaskList(taskList);
					EnqueueDeferredTaskLists(combine: true);
					return;
				}
				if (m_powerQueue.Count > 0)
				{
					m_powerQueue.GetItem(m_powerQueue.Count - 1).SetCollapsible(collapsible: true);
				}
				taskList.SetDeferredSourceId(m_nextTaskListId);
				if (m_deferredStack.Count > 0)
				{
					List<PowerTaskList> list = m_deferredStack.Pop();
					if (m_deferredStack.Count > 0)
					{
						m_deferredStack.Peek()?.AddRange(list);
					}
					else
					{
						m_deferredStack.Push(list);
					}
				}
				return;
			}
			switch (type)
			{
			case Network.PowerType.SUB_SPELL_START:
			{
				if (!taskList.HasTasks())
				{
					Network.HistMetaData netPower = new Network.HistMetaData
					{
						MetaType = HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT
					};
					taskList.CreateTask(netPower);
				}
				EnqueueTaskList(taskList);
				PowerTaskList powerTaskList3 = new PowerTaskList();
				powerTaskList3.SetPrevious(taskList);
				powerTaskList3.SetParent(taskList.GetParent());
				powerTaskList3.SetSubSpellOrigin(powerTaskList3);
				powerTaskList3.SetSubSpellStart((Network.HistSubSpellStart)powerHistory);
				m_subSpellOriginStack.Push(powerTaskList3);
				if (m_previousStack.Count > 0 && m_previousStack.Peek() == taskList)
				{
					m_previousStack.Pop();
					m_previousStack.Push(powerTaskList3);
				}
				taskList = powerTaskList3;
				break;
			}
			case Network.PowerType.SUB_SPELL_END:
				taskList.CreateTask(powerHistory);
				taskList.SetSubSpellEnd((Network.HistSubSpellEnd)powerHistory);
				EnqueueTaskList(taskList);
				if (m_subSpellOriginStack.Count > 0)
				{
					if (m_subSpellOriginStack.Pop() != taskList.GetSubSpellOrigin())
					{
						Log.Power.PrintError("{0}.BuildTaskList(): Mismatch between SUB_SPELL_END task and current task list's SubSpellOrigin!", this);
					}
				}
				else
				{
					Log.Power.PrintError("{0}.BuildTaskList(): Hit a SUB_SPELL_END task without a corresponding open SubSpellOrigin!", this);
				}
				if (index + 1 < powerList.Count)
				{
					PowerTaskList powerTaskList2 = new PowerTaskList();
					powerTaskList2.SetPrevious(taskList);
					powerTaskList2.SetParent(taskList.GetParent());
					if (m_subSpellOriginStack.Count > 0 && m_subSpellOriginStack.Peek().GetParent() == taskList.GetParent())
					{
						powerTaskList2.SetSubSpellOrigin(m_subSpellOriginStack.Peek());
					}
					if (m_previousStack.Count > 0 && m_previousStack.Peek() == taskList)
					{
						m_previousStack.Pop();
						m_previousStack.Push(powerTaskList2);
					}
					taskList = powerTaskList2;
					continue;
				}
				break;
			}
			PowerTask powerTask = taskList.CreateTask(powerHistory);
			if (type == Network.PowerType.META_DATA && ((Network.HistMetaData)powerHistory).MetaType == HistoryMeta.Type.ARTIFICIAL_HISTORY_INTERRUPT)
			{
				EnqueueTaskList(taskList);
				return;
			}
			if (CanDoRealTimeTask())
			{
				powerTask.DoRealTimeTask(powerList, index);
				continue;
			}
			DelayedRealTimeTask delayedRealTimeTask = new DelayedRealTimeTask();
			delayedRealTimeTask.m_index = index;
			delayedRealTimeTask.m_powerTask = powerTask;
			delayedRealTimeTask.m_powerHistory = new List<Network.PowerHistory>(powerList);
			m_delayedRealTimeTasks.Enqueue(delayedRealTimeTask);
		}
		if (!taskList.IsEmpty())
		{
			EnqueueTaskList(taskList);
		}
		if (m_deferredStack.Count != 0)
		{
			EnqueueDeferredTaskLists(combine: true);
			if (m_deferredStack.Count == 0)
			{
				m_deferredStack.Push(new List<PowerTaskList>());
			}
		}
	}

	private void EnqueueDeferredTaskLists(bool combine)
	{
		if (m_deferredStack.Count <= 0)
		{
			return;
		}
		List<PowerTaskList> list = m_deferredStack.Pop();
		for (int num = list.Count - 1; num > 0; num--)
		{
			PowerTaskList powerTaskList = list[num];
			if (powerTaskList.GetBlockStart() != null && combine)
			{
				for (int num2 = num - 1; num2 >= 0; num2--)
				{
					PowerTaskList powerTaskList2 = list[num2];
					if (powerTaskList2.GetBlockStart() != null && powerTaskList2.GetBlockStart().Entities.Count == powerTaskList.GetBlockStart().Entities.Count)
					{
						bool flag = true;
						foreach (int entity in powerTaskList2.GetBlockStart().Entities)
						{
							if (!powerTaskList.GetBlockStart().Entities.Contains(entity))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							powerTaskList2.AddTasks(powerTaskList);
							list.RemoveAt(num);
							break;
						}
					}
				}
			}
		}
		foreach (PowerTaskList item in list)
		{
			EnqueueTaskList(item);
		}
	}

	public bool EntityHasPendingTasks(Entity entity)
	{
		int entityId = entity.GetEntityId();
		foreach (PowerTaskList item in m_powerQueue)
		{
			List<Entity> sourceEntities = item.GetSourceEntities(warnIfNull: false);
			if (sourceEntities != null && sourceEntities.Exists((Entity e) => e != null && e.GetEntityId() == entityId))
			{
				return true;
			}
			Entity targetEntity = item.GetTargetEntity(warnIfNull: false);
			if (targetEntity != null && targetEntity.GetEntityId() == entityId)
			{
				return true;
			}
			PowerTaskList parent = item.GetParent();
			if (parent != null)
			{
				List<Entity> sourceEntities2 = parent.GetSourceEntities(warnIfNull: false);
				if (sourceEntities2 != null && sourceEntities2.Exists((Entity e) => e != null && e.GetEntityId() == entityId))
				{
					return true;
				}
				Entity targetEntity2 = parent.GetTargetEntity(warnIfNull: false);
				if (targetEntity2 != null && targetEntity2.GetEntityId() == entityId)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void FlushDelayedRealTimeTasks()
	{
		while (CanDoRealTimeTask() && m_delayedRealTimeTasks.Count > 0)
		{
			DelayedRealTimeTask delayedRealTimeTask = m_delayedRealTimeTasks.Dequeue();
			delayedRealTimeTask.m_powerTask.DoRealTimeTask(delayedRealTimeTask.m_powerHistory, delayedRealTimeTask.m_index);
		}
	}

	private void EnqueueTaskList(PowerTaskList taskList)
	{
		m_totalSlushTime += taskList.GetTotalSlushTime();
		if (m_powerHistoryFirstTaskList == null)
		{
			m_powerHistoryFirstTaskList = taskList;
		}
		else
		{
			m_powerHistoryLastTaskList = taskList;
		}
		taskList.SetId(GetNextTaskListId());
		m_powerQueue.Enqueue(taskList);
		if (m_currentTimeline != null && taskList.GetTotalSlushTime() > 0)
		{
			m_currentTimeline.AddTimelineEntry(taskList.GetId(), taskList.GetTotalSlushTime());
		}
		if (taskList.HasFriendlyConcede())
		{
			m_earlyConcedeTaskList = taskList;
		}
		if (taskList.HasGameOver())
		{
			m_gameOverTaskList = taskList;
		}
	}

	private void OnWillProcessTaskList(PowerTaskList taskList)
	{
		if ((bool)ThinkEmoteManager.Get())
		{
			ThinkEmoteManager.Get().NotifyOfActivity();
		}
		if (!taskList.IsStartOfBlock() || taskList.GetBlockStart().BlockType != HistoryBlock.Type.PLAY)
		{
			return;
		}
		Entity sourceEntity = taskList.GetSourceEntity(warnIfNull: false);
		if (sourceEntity.GetController().IsOpposingSide())
		{
			string text = sourceEntity.GetCardId();
			if (string.IsNullOrEmpty(text))
			{
				text = FindRevealedCardId(taskList);
			}
			GameState.Get().GetGameEntity().NotifyOfOpponentWillPlayCard(text);
		}
	}

	private bool ContainsBurnedCard(PowerTaskList taskList)
	{
		return ContainsMetaDataTaskWithInfo(taskList, HistoryMeta.Type.BURNED_CARD);
	}

	private bool ContainsPoisonousEffect(PowerTaskList taskList)
	{
		return ContainsMetaDataTaskWithInfo(taskList, HistoryMeta.Type.POISONOUS);
	}

	private bool ContainsMetaDataTaskWithInfo(PowerTaskList taskList, HistoryMeta.Type metaType)
	{
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType != metaType)
			{
				continue;
			}
			if (histMetaData.Info.Count == 0)
			{
				Log.Power.PrintError("PowerProcessor.ContainsMetaDataTaskWithInfo(): metaData.Info.Count is 0, metaType: {0}", metaType);
				continue;
			}
			if (GameState.Get().GetEntity(histMetaData.Info[0]) == null)
			{
				Log.Power.PrintError("PowerProcessor.ContainsMetaDataTaskWithInfo(): metaData.Info contains an invalid entity (ID {0}), metaType: {1}", histMetaData.Info[0], metaType);
				continue;
			}
			return true;
		}
		return false;
	}

	private string FindRevealedCardId(PowerTaskList taskList)
	{
		taskList.GetBlockStart();
		List<PowerTask> taskList2 = taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			Network.HistShowEntity showEntity = power as Network.HistShowEntity;
			if (showEntity != null && taskList.GetSourceEntities() != null && taskList.GetSourceEntities().Exists((Entity e) => e != null && e.GetEntityId() == showEntity.Entity.ID))
			{
				return showEntity.Entity.CardID;
			}
		}
		return null;
	}

	private void OnProcessTaskList()
	{
		if (m_currentTaskList.IsStartOfBlock())
		{
			Network.HistBlockStart blockStart = m_currentTaskList.GetBlockStart();
			switch (blockStart.BlockType)
			{
			case HistoryBlock.Type.PLAY:
			{
				Entity sourceEntity = m_currentTaskList.GetSourceEntity(warnIfNull: false);
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
					Entity targetEntity = m_currentTaskList.GetTargetEntity(warnIfNull: false);
					GameState.Get().GetGameEntity().NotifyOfSpellPlayed(sourceEntity, targetEntity);
				}
				else if (sourceEntity.IsHeroPower())
				{
					Entity targetEntity2 = m_currentTaskList.GetTargetEntity(warnIfNull: false);
					GameState.Get().GetGameEntity().NotifyOfHeroPowerUsed(sourceEntity, targetEntity2);
				}
				break;
			}
			case HistoryBlock.Type.ATTACK:
			{
				Entity attacker = m_currentTaskList.GetAttacker();
				Entity entity2 = null;
				switch (m_currentTaskList.GetAttackType())
				{
				case AttackType.REGULAR:
					entity2 = m_currentTaskList.GetDefender();
					break;
				case AttackType.CANCELED:
					entity2 = m_currentTaskList.GetProposedDefender();
					break;
				}
				if (attacker != null && entity2 != null)
				{
					GameState.Get().GetGameEntity().NotifyOfEntityAttacked(attacker, entity2);
				}
				break;
			}
			case HistoryBlock.Type.DEATHS:
				foreach (PowerTask task in m_currentTaskList.GetTaskList())
				{
					Network.PowerHistory power = task.GetPower();
					if (power.Type != Network.PowerType.TAG_CHANGE)
					{
						continue;
					}
					Network.HistTagChange histTagChange = power as Network.HistTagChange;
					if (GameUtils.IsEntityDeathTagChange(histTagChange))
					{
						Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
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
				break;
			}
			if (blockStart.BlockType == HistoryBlock.Type.POWER || blockStart.BlockType == HistoryBlock.Type.TRIGGER)
			{
				for (int i = 0; i < blockStart.EffectCardId.Count; i++)
				{
					if (string.IsNullOrEmpty(blockStart.EffectCardId[i]))
					{
						List<Entity> sourceEntities = m_currentTaskList.GetSourceEntities();
						if (sourceEntities != null && i < sourceEntities.Count && sourceEntities[i] != null)
						{
							blockStart.EffectCardId[i] = sourceEntities[i].GetCardId();
							blockStart.IsEffectCardIdClientCached[i] = true;
						}
					}
				}
			}
		}
		PrepareHistoryForCurrentTaskList();
		m_currentTaskList.CreateArtificialHistoryTilesFromMetadata();
	}

	private void PrepareHistoryForCurrentTaskList()
	{
		Log.Power.Print("PowerProcessor.PrepareHistoryForCurrentTaskList() - m_currentTaskList={0}", m_currentTaskList.GetId());
		Network.HistBlockStart blockStart = m_currentTaskList.GetBlockStart();
		if (blockStart == null)
		{
			return;
		}
		List<Entity> sourceEntities = m_currentTaskList.GetSourceEntities();
		if (sourceEntities != null && sourceEntities.Exists((Entity e) => e?.HasTag(GAME_TAG.CARD_DOES_NOTHING) ?? false))
		{
			return;
		}
		switch (blockStart.BlockType)
		{
		case HistoryBlock.Type.ATTACK:
		{
			AttackType attackType = m_currentTaskList.GetAttackType();
			Entity entity = null;
			Entity entity2 = null;
			switch (attackType)
			{
			case AttackType.REGULAR:
				entity = m_currentTaskList.GetAttacker();
				entity2 = m_currentTaskList.GetDefender();
				break;
			case AttackType.CANCELED:
				entity = m_currentTaskList.GetAttacker();
				entity2 = m_currentTaskList.GetProposedDefender();
				break;
			}
			if (entity != null && entity2 != null)
			{
				HistoryManager.Get().CreateAttackTile(entity, entity2, m_currentTaskList);
				m_currentTaskList.SetWillCompleteHistoryEntry(set: true);
			}
			if (HistoryManager.Get().HasHistoryEntry())
			{
				m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			}
			break;
		}
		case HistoryBlock.Type.PLAY:
		{
			Entity sourceEntity2 = m_currentTaskList.GetSourceEntity(warnIfNull: false);
			if (sourceEntity2 == null)
			{
				break;
			}
			if (m_currentTaskList.IsStartOfBlock())
			{
				if (m_currentTaskList.ShouldCreatePlayBlockHistoryTile())
				{
					Entity entity4 = GameState.Get().GetEntity(blockStart.Target);
					HistoryManager.Get().CreatePlayedTile(sourceEntity2, entity4);
					m_currentTaskList.SetWillCompleteHistoryEntry(set: true);
				}
				if (ShouldShowPlayedBigCard(sourceEntity2, blockStart))
				{
					bool countered = m_currentTaskList.WasThePlayedSpellCountered(sourceEntity2);
					SetHistoryBlockingTaskList();
					HistoryManager.Get().CreatePlayedBigCard(sourceEntity2, OnBigCardStarted, OnBigCardFinished, fromMetaData: false, countered, 0);
				}
			}
			m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			break;
		}
		case HistoryBlock.Type.POWER:
			if (HistoryManager.Get().HasHistoryEntry())
			{
				m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			}
			break;
		case HistoryBlock.Type.JOUST:
			m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			break;
		case HistoryBlock.Type.REVEAL_CARD:
			m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			break;
		case HistoryBlock.Type.TRIGGER:
		{
			Entity sourceEntity = m_currentTaskList.GetSourceEntity(warnIfNull: false);
			if (sourceEntity == null)
			{
				break;
			}
			if (sourceEntity.IsSecret() || blockStart.TriggerKeyword == 1192 || blockStart.TriggerKeyword == 1749)
			{
				if (m_currentTaskList.IsStartOfBlock())
				{
					HistoryManager.Get().CreateTriggerTile(sourceEntity);
					m_currentTaskList.SetWillCompleteHistoryEntry(set: true);
					SetHistoryBlockingTaskList();
					HistoryManager.Get().CreateTriggeredBigCard(sourceEntity, OnBigCardStarted, OnBigCardFinished, fromMetaData: false, isSecret: true);
				}
				m_currentTaskList.NotifyHistoryOfAdditionalTargets();
				break;
			}
			bool flag = false;
			if (!m_currentTaskList.IsStartOfBlock())
			{
				flag = GetTriggerTaskListThatShouldCompleteHistoryEntry().WillBlockCompleteHistoryEntry();
			}
			else if (blockStart.ShowInHistory)
			{
				if (sourceEntity.HasTag(GAME_TAG.HISTORY_PROXY))
				{
					Entity entity3 = GameState.Get().GetEntity(sourceEntity.GetTag(GAME_TAG.HISTORY_PROXY));
					HistoryManager.Get().CreatePlayedTile(entity3, null);
					if (sourceEntity.GetController() != GameState.Get().GetFriendlySidePlayer() || !sourceEntity.HasTag(GAME_TAG.HISTORY_PROXY_NO_BIG_CARD))
					{
						SetHistoryBlockingTaskList();
						HistoryManager.Get().CreateTriggeredBigCard(entity3, OnBigCardStarted, OnBigCardFinished, fromMetaData: false, isSecret: false);
					}
				}
				else
				{
					if (ShouldShowTriggeredBigCard(sourceEntity))
					{
						SetHistoryBlockingTaskList();
						HistoryManager.Get().CreateTriggeredBigCard(sourceEntity, OnBigCardStarted, OnBigCardFinished, fromMetaData: false, isSecret: false);
					}
					HistoryManager.Get().CreateTriggerTile(sourceEntity);
				}
				GetTriggerTaskListThatShouldCompleteHistoryEntry().SetWillCompleteHistoryEntry(set: true);
				flag = true;
			}
			else if ((blockStart.TriggerKeyword == 685 || blockStart.TriggerKeyword == 923 || blockStart.TriggerKeyword == 363 || blockStart.TriggerKeyword == 1944 || blockStart.TriggerKeyword == 1675) && HistoryManager.Get().HasHistoryEntry())
			{
				flag = true;
			}
			else if (ContainsBurnedCard(m_currentTaskList))
			{
				if (m_currentTaskList.IsStartOfBlock())
				{
					HistoryManager.Get().CreateBurnedCardsTile();
					m_currentTaskList.SetWillCompleteHistoryEntry(set: true);
				}
				m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			}
			else if (ContainsPoisonousEffect(m_currentTaskList))
			{
				flag = true;
			}
			if (flag)
			{
				m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			}
			break;
		}
		case HistoryBlock.Type.FATIGUE:
			if (m_currentTaskList.IsStartOfBlock())
			{
				HistoryManager.Get().CreateFatigueTile();
				m_currentTaskList.SetWillCompleteHistoryEntry(set: true);
			}
			m_currentTaskList.NotifyHistoryOfAdditionalTargets();
			break;
		}
	}

	private void OnBigCardStarted()
	{
		m_historyBlocking = true;
	}

	private void OnBigCardFinished()
	{
		m_historyBlocking = false;
	}

	private bool ShouldShowPlayedBigCard(Entity sourceEntity, Network.HistBlockStart blockStart)
	{
		if (!GameState.Get().GetBooleanGameOption(GameEntityOption.USES_BIG_CARDS))
		{
			return false;
		}
		if (!InputManager.Get().PermitDecisionMakingInput())
		{
			return true;
		}
		if (sourceEntity.IsControlledByOpposingSidePlayer())
		{
			return true;
		}
		if (blockStart.ForceShowBigCard)
		{
			return true;
		}
		return false;
	}

	private bool ShouldShowTriggeredBigCard(Entity sourceEntity)
	{
		if (sourceEntity.GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		if (sourceEntity.IsHidden())
		{
			return false;
		}
		if (!sourceEntity.HasTriggerVisual())
		{
			return false;
		}
		return true;
	}

	private PowerTaskList GetTriggerTaskListThatShouldCompleteHistoryEntry()
	{
		if (m_currentTaskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return null;
		}
		PowerTaskList parent = m_currentTaskList.GetParent();
		if (parent != null && parent.GetBlockType() == HistoryBlock.Type.RITUAL)
		{
			return parent;
		}
		return m_currentTaskList.GetOrigin();
	}

	private bool CanEarlyConcede()
	{
		if (!GameState.Get().IsGameCreated())
		{
			return false;
		}
		if (m_earlyConcedeTaskList != null)
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

	private void DoEarlyConcedeVisuals()
	{
		if (!GameUtils.IsWaitingForOpponentReconnect())
		{
			GameState.Get().GetFriendlySidePlayer()?.PlayConcedeEmote();
		}
	}

	private void CancelSpellsForEarlyConcede(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		if (sourceEntities == null)
		{
			return;
		}
		foreach (Entity item in sourceEntities)
		{
			if (item == null)
			{
				continue;
			}
			Card card = item.GetCard();
			if (!card || taskList.GetBlockStart().BlockType != HistoryBlock.Type.POWER)
			{
				continue;
			}
			Spell playSpell = card.GetPlaySpell(0);
			if ((bool)playSpell)
			{
				SpellStateType activeState = playSpell.GetActiveState();
				if (activeState != 0 && activeState != SpellStateType.CANCEL)
				{
					playSpell.ActivateState(SpellStateType.CANCEL);
				}
			}
		}
	}

	private void StartCurrentTaskList()
	{
		m_currentTaskList.SetProcessStartTime();
		GameState gameState = GameState.Get();
		Network.HistBlockStart blockStart = m_currentTaskList.GetBlockStart();
		if (blockStart == null)
		{
			DoCurrentTaskList();
			return;
		}
		int num = ((blockStart.Entities.Count != 0) ? blockStart.Entities[0] : 0);
		if (m_currentTaskList.GetSourceEntities() == null || m_currentTaskList.GetSourceEntity() == null)
		{
			if (!gameState.EntityRemovedFromGame(num))
			{
				Debug.LogErrorFormat("PowerProcessor.StartCurrentTaskList() - WARNING got a power with a null source entity (ID={0})", num);
			}
			DoCurrentTaskList();
		}
		else if (!DoTaskListWithSpellController(gameState, m_currentTaskList, m_currentTaskList.GetSourceEntity()))
		{
			DoCurrentTaskList();
		}
	}

	private void DoCurrentTaskList()
	{
		m_currentTaskList.DoAllTasks(delegate
		{
			EndCurrentTaskList();
		});
	}

	private void EndCurrentTaskList()
	{
		Log.Power.Print("PowerProcessor.EndCurrentTaskList() - m_currentTaskList={0}", (m_currentTaskList == null) ? "null" : m_currentTaskList.GetId().ToString());
		if (m_currentTaskList == null)
		{
			GameState.Get().OnTaskListEnded(null);
			return;
		}
		if (m_currentTaskList.GetBlockEnd() != null)
		{
			if (m_currentTaskList.GetOrigin() == m_historyBlockingTaskList && m_currentTaskList.GetNext() == null)
			{
				m_historyBlockingTaskList = null;
			}
			if (m_currentTaskList.IsRitualBlock() && HistoryManager.Get().HasHistoryEntry())
			{
				AddCthunToHistory();
			}
			Entity sourceEntity = m_currentTaskList.GetSourceEntity();
			if (sourceEntity != null && sourceEntity.IsTwinspell())
			{
				CleanupTwinspellEffects(sourceEntity);
			}
			if (m_currentTaskList.WillBlockCompleteHistoryEntry())
			{
				HistoryManager.Get().MarkCurrentHistoryEntryAsCompleted();
			}
		}
		GameState.Get().OnTaskListEnded(m_currentTaskList);
		m_previousTaskList = m_currentTaskList;
		m_currentTaskList = null;
	}

	private void AddCthunToHistory()
	{
		Entity ritualEntityClone = m_currentTaskList.GetOrigin().GetRitualEntityClone();
		if (ritualEntityClone == null)
		{
			return;
		}
		Entity sourceEntity = m_currentTaskList.GetSourceEntity();
		if (sourceEntity.HasTag(GAME_TAG.PIECE_OF_CTHUN))
		{
			HistoryManager.Get().NotifyEntityAffected(ritualEntityClone, allowDuplicates: true, fromMetaData: false);
			return;
		}
		int tag = sourceEntity.GetController().GetTag(GAME_TAG.PROXY_CTHUN);
		Entity entity = GameState.Get().GetEntity(tag);
		if (entity != null && (entity.GetTag(GAME_TAG.ATK) != ritualEntityClone.GetTag(GAME_TAG.ATK) || entity.GetTag(GAME_TAG.HEALTH) != ritualEntityClone.GetTag(GAME_TAG.HEALTH) || entity.GetTag(GAME_TAG.TAUNT) != ritualEntityClone.GetTag(GAME_TAG.TAUNT)))
		{
			HistoryManager.Get().NotifyEntityAffected(entity, allowDuplicates: true, fromMetaData: false);
		}
	}

	private void CleanupTwinspellEffects(Entity twinspellEntity)
	{
		if (InputManager.Get().GetFriendlyHand().IsTwinspellBeingPlayed(twinspellEntity))
		{
			InputManager.Get().GetFriendlyHand().ActivateTwinspellSpellDeath();
			InputManager.Get().GetFriendlyHand().ClearReservedCard();
		}
	}

	private bool DoTaskListWithSpellController(GameState state, PowerTaskList taskList, Entity sourceEntity)
	{
		HistoryBlock.Type blockType = taskList.GetBlockType();
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		if (taskList.IsSubSpellTaskList())
		{
			if (!DoSubSpellTaskListWithController(taskList))
			{
				return false;
			}
			return true;
		}
		switch (blockType)
		{
		case HistoryBlock.Type.ATTACK:
		{
			AttackSpellController spellController2 = CreateAttackSpellController(taskList);
			if (!DoTaskListUsingController(spellController2, taskList))
			{
				DestroySpellController(spellController2);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.MOVE_MINION:
		{
			MoveMinionSpellController spellController3 = CreateMoveMinionSpellController(taskList);
			if (!DoTaskListUsingController(spellController3, taskList))
			{
				DestroySpellController(spellController3);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.POWER:
		{
			PowerSpellController spellController7 = CreatePowerSpellController(taskList);
			if (!DoTaskListUsingController(spellController7, taskList))
			{
				DestroySpellController(spellController7);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.TRIGGER:
			if (sourceEntity != null && sourceEntity.IsSecret())
			{
				SecretSpellController spellController8 = CreateSecretSpellController(taskList);
				if (!DoTaskListUsingController(spellController8, taskList))
				{
					DestroySpellController(spellController8);
					return false;
				}
			}
			else if (blockStart != null && blockStart.TriggerKeyword == 1192)
			{
				SideQuestSpellController spellController9 = CreateSideQuestSpellController(taskList);
				if (!DoTaskListUsingController(spellController9, taskList))
				{
					DestroySpellController(spellController9);
					return false;
				}
			}
			else if (blockStart != null && blockStart.TriggerKeyword == 1749)
			{
				SigilSpellController spellController10 = CreateSigilSpellController(taskList);
				if (!DoTaskListUsingController(spellController10, taskList))
				{
					DestroySpellController(spellController10);
					return false;
				}
			}
			else
			{
				TriggerSpellController triggerSpellController = CreateTriggerSpellController(taskList);
				Card card = sourceEntity?.GetCard();
				Card startDrawMetaDataCard = taskList.GetStartDrawMetaDataCard();
				if (TurnStartManager.Get().IsCardDrawHandled(card) || TurnStartManager.Get().IsCardDrawHandled(startDrawMetaDataCard))
				{
					if (!triggerSpellController.AttachPowerTaskList(taskList))
					{
						Log.Power.PrintWarning("TurnStartManager failed to handle a trigger. sourceCard:{0}, metadataCard:{1}, taskList:{2}", card, startDrawMetaDataCard, taskList);
						DestroySpellController(triggerSpellController);
						return false;
					}
					triggerSpellController.AddFinishedTaskListCallback(OnSpellControllerFinishedTaskList);
					triggerSpellController.AddFinishedCallback(OnSpellControllerFinished);
					TurnStartManager.Get().NotifyOfSpellController(triggerSpellController);
				}
				else if (!DoTaskListUsingController(triggerSpellController, taskList))
				{
					DestroySpellController(triggerSpellController);
					return false;
				}
			}
			return true;
		case HistoryBlock.Type.DEATHS:
		{
			DeathSpellController spellController5 = CreateDeathSpellController(taskList);
			if (!DoTaskListUsingController(spellController5, taskList))
			{
				DestroySpellController(spellController5);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.FATIGUE:
		{
			FatigueSpellController fatigueSpellController = CreateFatigueSpellController(taskList);
			if (!fatigueSpellController.AttachPowerTaskList(taskList))
			{
				DestroySpellController(fatigueSpellController);
				return false;
			}
			fatigueSpellController.AddFinishedTaskListCallback(OnSpellControllerFinishedTaskList);
			fatigueSpellController.AddFinishedCallback(OnSpellControllerFinished);
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
		case HistoryBlock.Type.JOUST:
		{
			JoustSpellController spellController11 = CreateJoustSpellController(taskList);
			if (!DoTaskListUsingController(spellController11, taskList))
			{
				DestroySpellController(spellController11);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.RITUAL:
		{
			RitualSpellController spellController6 = CreateRitualSpellController(taskList);
			if (!DoTaskListUsingController(spellController6, taskList))
			{
				DestroySpellController(spellController6);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.REVEAL_CARD:
		{
			RevealCardSpellController spellController4 = CreateRevealCardSpellController(taskList);
			if (!DoTaskListUsingController(spellController4, taskList))
			{
				DestroySpellController(spellController4);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.GAME_RESET:
		{
			ResetGameSpellController spellController = CreateResetGameSpellController(taskList);
			if (!DoTaskListUsingController(spellController, taskList))
			{
				DestroySpellController(spellController);
				return false;
			}
			return true;
		}
		case HistoryBlock.Type.PLAY:
			CheckDeactivatePlaySpellForSpellPlayBlock(taskList);
			CheckDeactivatePlaySpellForTransformation(taskList);
			break;
		}
		Log.Power.Print("PowerProcessor.DoTaskListForCard() - unhandled BlockType {0} for sourceEntity {1}", blockType, sourceEntity);
		return false;
	}

	private void CheckDeactivatePlaySpellForSpellPlayBlock(PowerTaskList taskList)
	{
		if (taskList.GetOrigin() != taskList)
		{
			return;
		}
		PowerTaskList powerTaskList = ((GetPowerQueue().Count > 0) ? GetPowerQueue().Peek() : null);
		if (powerTaskList != null && powerTaskList.GetParent() == taskList)
		{
			return;
		}
		Entity sourceEntity = taskList.GetSourceEntity();
		if (sourceEntity != null && sourceEntity.GetCardType() == TAG_CARDTYPE.SPELL)
		{
			Card card = sourceEntity.GetCard();
			if (!(card == null))
			{
				card.DeactivatePlaySpell();
			}
		}
	}

	private void CheckDeactivatePlaySpellForTransformation(PowerTaskList taskList)
	{
		if (taskList.GetBlockEnd() == null)
		{
			return;
		}
		PowerTaskList powerTaskList = ((GetPowerQueue().Count > 0) ? GetPowerQueue().Peek() : null);
		if (powerTaskList != null && powerTaskList.GetParent() == taskList)
		{
			return;
		}
		Entity sourceEntity = taskList.GetSourceEntity();
		if (sourceEntity != null && sourceEntity.HasTag(GAME_TAG.TRANSFORMED_FROM_CARD) && sourceEntity.GetCardType() == TAG_CARDTYPE.SPELL)
		{
			Card card = sourceEntity.GetCard();
			if (!(card == null))
			{
				card.DeactivatePlaySpell();
			}
		}
	}

	private bool DoSubSpellTaskListWithController(PowerTaskList taskList)
	{
		if (m_subSpellController == null)
		{
			m_subSpellController = CreateSpellController<SubSpellController>(null, "SubSpellController.prefab:34966ff41154fce469d3ccb6d3b1655e");
		}
		if (!m_subSpellController.AttachPowerTaskList(taskList))
		{
			return false;
		}
		m_subSpellController.AddFinishedTaskListCallback(OnSpellControllerFinishedTaskList);
		m_subSpellController.DoPowerTaskList();
		return true;
	}

	private bool DoTaskListUsingController(SpellController spellController, PowerTaskList taskList)
	{
		if (spellController == null)
		{
			Log.Power.Print("PowerProcessor.DoTaskListUsingController() - spellController=null");
			return false;
		}
		if (!spellController.AttachPowerTaskList(taskList))
		{
			return false;
		}
		spellController.AddFinishedTaskListCallback(OnSpellControllerFinishedTaskList);
		spellController.AddFinishedCallback(OnSpellControllerFinished);
		spellController.DoPowerTaskList();
		return true;
	}

	private void OnSpellControllerFinishedTaskList(SpellController spellController)
	{
		spellController.DetachPowerTaskList();
		if (m_currentTaskList != null)
		{
			DoCurrentTaskList();
		}
	}

	private void OnSpellControllerFinished(SpellController spellController)
	{
		DestroySpellController(spellController);
	}

	private AttackSpellController CreateAttackSpellController(PowerTaskList taskList)
	{
		string prefabPath = "AttackSpellController.prefab:12acecc85ac575e43b87ec141b89269a";
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null && !string.IsNullOrEmpty(GameState.Get().GetGameEntity().GetAttackSpellControllerOverride(taskList.GetAttacker())))
		{
			prefabPath = GameState.Get().GetGameEntity().GetAttackSpellControllerOverride(taskList.GetAttacker());
		}
		return CreateSpellController<AttackSpellController>(taskList, prefabPath);
	}

	private MoveMinionSpellController CreateMoveMinionSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<MoveMinionSpellController>(taskList);
	}

	private SecretSpellController CreateSecretSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<SecretSpellController>(taskList, "SecretSpellController.prefab:553af99c12154c547bc05dc3d9832931");
	}

	private SigilSpellController CreateSigilSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<SigilSpellController>(taskList, "SigilSpellController.prefab:1f80634fbf70a654bbae7bf796bf11b2");
	}

	private SideQuestSpellController CreateSideQuestSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<SideQuestSpellController>(taskList, "SideQuestSpellController.prefab:63762d08481f04642bbf3cde299feea2");
	}

	private PowerSpellController CreatePowerSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<PowerSpellController>(taskList);
	}

	private TriggerSpellController CreateTriggerSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<TriggerSpellController>(taskList, "TriggerSpellController.prefab:e0a2661f98a720d47ad4b85de228f4b4");
	}

	private DeathSpellController CreateDeathSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<DeathSpellController>(taskList);
	}

	private FatigueSpellController CreateFatigueSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<FatigueSpellController>(taskList);
	}

	private JoustSpellController CreateJoustSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<JoustSpellController>(taskList, "JoustSpellController.prefab:89ac256005a4a8a46939a84460c2c221");
	}

	private RitualSpellController CreateRitualSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<RitualSpellController>(taskList, "RitualSpellController.prefab:27c7bd4ffaa54fb4e9e64dad14a6e701");
	}

	private RevealCardSpellController CreateRevealCardSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<RevealCardSpellController>(taskList, "RevealCardSpellController.prefab:17fd7ea79bfd4c24389d535a074199b6");
	}

	private ResetGameSpellController CreateResetGameSpellController(PowerTaskList taskList)
	{
		return CreateSpellController<ResetGameSpellController>(taskList, "ResetGameSpellController.prefab:d8c1994d523574e42bffa17990917754");
	}

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
			gameObject = AssetLoader.Get().InstantiatePrefab(prefabPath);
			result = gameObject.GetComponent<T>();
		}
		if (taskList != null)
		{
			gameObject.name = $"{typeof(T)} [taskListId={taskList.GetId()}]";
		}
		else
		{
			gameObject.name = $"{typeof(T)}";
		}
		return result;
	}

	private void DestroySpellController(SpellController spellController)
	{
		UnityEngine.Object.Destroy(spellController.gameObject);
	}
}
