using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
	public delegate void FinishedTaskListCallback(SpellController spellController);

	public delegate void FinishedCallback(SpellController spellController);

	public const float FINISH_FUDGE_SEC = 10f;

	private static readonly PlatformDependentValue<bool> ALLOW_LOST_FRAME_TIME_CATCH_UP = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	private List<FinishedTaskListCallback> m_finishedTaskListListeners = new List<FinishedTaskListCallback>();

	private List<FinishedCallback> m_finishedListeners = new List<FinishedCallback>();

	protected List<Card> m_sources = new List<Card>();

	protected List<Card> m_targets = new List<Card>();

	protected PowerTaskList m_taskList;

	protected int m_taskListId;

	protected bool m_processingTaskList;

	protected bool m_pendingFinish;

	public Card GetSource()
	{
		if (m_sources == null || m_sources.Count <= 0)
		{
			return null;
		}
		return m_sources[0];
	}

	public List<Card> GetSources()
	{
		return m_sources;
	}

	public void SetSource(Card card)
	{
		m_sources.Clear();
		m_sources.Add(card);
	}

	public void SetSource(List<Card> cards)
	{
		m_sources.Clear();
		m_sources.AddRange(cards);
	}

	public bool IsSource(Card card)
	{
		return m_sources.Contains(card);
	}

	public void RemoveSource()
	{
		m_sources.Clear();
	}

	public List<Card> GetTargets()
	{
		return m_targets;
	}

	public Card GetTarget()
	{
		if (m_targets.Count != 0)
		{
			return m_targets[0];
		}
		return null;
	}

	public void AddTarget(Card card)
	{
		m_targets.Add(card);
	}

	public void RemoveTarget(Card card)
	{
		m_targets.Remove(card);
	}

	public void RemoveAllTargets()
	{
		m_targets.Clear();
	}

	public bool IsTarget(Card card)
	{
		return m_targets.Contains(card);
	}

	public void AddFinishedTaskListCallback(FinishedTaskListCallback callback)
	{
		if (!m_finishedTaskListListeners.Contains(callback))
		{
			m_finishedTaskListListeners.Add(callback);
		}
	}

	public void AddFinishedCallback(FinishedCallback callback)
	{
		if (!m_finishedListeners.Contains(callback))
		{
			m_finishedListeners.Add(callback);
		}
	}

	public bool IsProcessingTaskList()
	{
		return m_processingTaskList;
	}

	public PowerTaskList GetPowerTaskList()
	{
		return m_taskList;
	}

	public bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (m_taskList != taskList)
		{
			DetachPowerTaskList();
			m_taskList = taskList;
		}
		m_taskListId = m_taskList.GetId();
		return AddPowerSourceAndTargets(taskList);
	}

	public void SetPowerTaskList(PowerTaskList taskList)
	{
		if (m_taskList != taskList)
		{
			DetachPowerTaskList();
			m_taskList = taskList;
		}
	}

	public PowerTaskList DetachPowerTaskList()
	{
		PowerTaskList taskList = m_taskList;
		RemoveSource();
		RemoveAllTargets();
		m_taskList = null;
		return taskList;
	}

	public void DoPowerTaskList()
	{
		m_processingTaskList = true;
		if (IsLostFrameTimeCatchUpEnabled())
		{
			float clientLostTimeCatchUpThreshold = GameState.Get().GetClientLostTimeCatchUpThreshold();
			float lostFrameTimeCatchUpSeconds = GetLostFrameTimeCatchUpSeconds();
			if (lostFrameTimeCatchUpSeconds > 0f && clientLostTimeCatchUpThreshold > 0f && GameState.Get().GetTimeTracker().GetAccruedLostTimeInSeconds() > Math.Max(lostFrameTimeCatchUpSeconds, clientLostTimeCatchUpThreshold))
			{
				if (GameState.Get().GetTimeTracker() is GameStateFrameTimeTracker)
				{
					GameState.Get().GetTimeTracker().AdjustAccruedLostTime(0f - lostFrameTimeCatchUpSeconds);
				}
				OnFinishedTaskList();
				OnFinished();
				return;
			}
		}
		base.gameObject.SetActive(value: true);
		GameState.Get().AddServerBlockingSpellController(this);
		StartCoroutine(WaitForCardsThenDoTaskList());
	}

	public void ForceKill()
	{
		OnFinishedTaskList();
	}

	public virtual bool ShouldReconnectIfStuck()
	{
		return true;
	}

	protected virtual void OnProcessTaskList()
	{
		OnFinishedTaskList();
		OnFinished();
	}

	protected virtual void OnFinishedTaskList()
	{
		GameState.Get().RemoveServerBlockingSpellController(this);
		m_processingTaskList = false;
		FireFinishedTaskListCallbacks();
		if (m_pendingFinish)
		{
			m_pendingFinish = false;
			OnFinished();
		}
	}

	protected virtual void OnFinished()
	{
		if (m_processingTaskList)
		{
			m_pendingFinish = true;
			return;
		}
		base.gameObject.SetActive(value: false);
		FireFinishedCallbacks();
	}

	protected virtual bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!HasSourceCard(taskList))
		{
			return false;
		}
		if (!SpellUtils.CanAddPowerTargets(taskList))
		{
			return false;
		}
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		List<Card> list = new List<Card>();
		foreach (Entity item in sourceEntities)
		{
			if (item != null)
			{
				list.Add(item.GetCard());
			}
		}
		SetSource(list);
		List<PowerTask> taskList2 = m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			PowerTask task = taskList2[i];
			Card targetCardFromPowerTask = GetTargetCardFromPowerTask(task);
			if (!(targetCardFromPowerTask == null) && !list.Contains(targetCardFromPowerTask) && !IsTarget(targetCardFromPowerTask))
			{
				AddTarget(targetCardFromPowerTask);
			}
		}
		if (list.Count <= 0 || list.Exists((Card c) => c == null))
		{
			return m_targets.Count > 0;
		}
		return true;
	}

	protected virtual bool HasSourceCard(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		if (sourceEntities == null || sourceEntities.Count == 0)
		{
			return false;
		}
		List<Card> list = new List<Card>();
		foreach (Entity item in sourceEntities)
		{
			if (item != null)
			{
				list.Add(item.GetCard());
			}
		}
		if (list == null || list.Count == 0 || list.Exists((Card c) => c == null))
		{
			return false;
		}
		return true;
	}

	protected virtual float GetLostFrameTimeCatchUpSeconds()
	{
		return 0f;
	}

	private IEnumerator WaitForCardsThenDoTaskList()
	{
		Card sourceCard = GetSource();
		if (sourceCard != null)
		{
			while (IsCardBusy(sourceCard))
			{
				yield return null;
			}
		}
		foreach (Card targetCard in m_targets)
		{
			if (!(targetCard == null))
			{
				while (IsCardBusy(targetCard))
				{
					yield return null;
				}
			}
		}
		OnProcessTaskList();
	}

	protected bool IsLostFrameTimeCatchUpEnabled()
	{
		if ((bool)ALLOW_LOST_FRAME_TIME_CATCH_UP && GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().AreLostTimeGuardianConditionsMet())
		{
			return GameState.Get().GetGameEntity().IsGameSpeedupConditionInEffect();
		}
		return false;
	}

	protected bool IsCardBusy(Card card)
	{
		Entity entity = card.GetEntity();
		if (WillEntityLoadCard(entity))
		{
			return false;
		}
		if (entity.IsLoadingAssets())
		{
			return true;
		}
		if ((bool)TurnStartManager.Get() && TurnStartManager.Get().IsCardDrawHandled(card))
		{
			return false;
		}
		if (!card.IsActorReady())
		{
			return true;
		}
		return false;
	}

	private bool WillEntityLoadCard(Entity entity)
	{
		int entityId = entity.GetEntityId();
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			switch (power.Type)
			{
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
				if (entityId == histFullEntity.Entity.ID)
				{
					return true;
				}
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = power as Network.HistShowEntity;
				if (entityId == histShowEntity.Entity.ID)
				{
					return true;
				}
				break;
			}
			}
		}
		return false;
	}

	private void FireFinishedTaskListCallbacks()
	{
		FinishedTaskListCallback[] array = m_finishedTaskListListeners.ToArray();
		m_finishedTaskListListeners.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	private void FireFinishedCallbacks()
	{
		FinishedCallback[] array = m_finishedListeners.ToArray();
		m_finishedListeners.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	protected Card GetTargetCardFromPowerTask(PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {histTagChange.Entity} but there is no entity with that id");
			return null;
		}
		return entity.GetCard();
	}
}
