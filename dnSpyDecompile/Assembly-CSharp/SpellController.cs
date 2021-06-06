using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200096F RID: 2415
public class SpellController : MonoBehaviour
{
	// Token: 0x0600851F RID: 34079 RVA: 0x002B01D2 File Offset: 0x002AE3D2
	public Card GetSource()
	{
		if (this.m_sources == null || this.m_sources.Count <= 0)
		{
			return null;
		}
		return this.m_sources[0];
	}

	// Token: 0x06008520 RID: 34080 RVA: 0x002B01F8 File Offset: 0x002AE3F8
	public List<Card> GetSources()
	{
		return this.m_sources;
	}

	// Token: 0x06008521 RID: 34081 RVA: 0x002B0200 File Offset: 0x002AE400
	public void SetSource(Card card)
	{
		this.m_sources.Clear();
		this.m_sources.Add(card);
	}

	// Token: 0x06008522 RID: 34082 RVA: 0x002B0219 File Offset: 0x002AE419
	public void SetSource(List<Card> cards)
	{
		this.m_sources.Clear();
		this.m_sources.AddRange(cards);
	}

	// Token: 0x06008523 RID: 34083 RVA: 0x002B0232 File Offset: 0x002AE432
	public bool IsSource(Card card)
	{
		return this.m_sources.Contains(card);
	}

	// Token: 0x06008524 RID: 34084 RVA: 0x002B0240 File Offset: 0x002AE440
	public void RemoveSource()
	{
		this.m_sources.Clear();
	}

	// Token: 0x06008525 RID: 34085 RVA: 0x002B024D File Offset: 0x002AE44D
	public List<Card> GetTargets()
	{
		return this.m_targets;
	}

	// Token: 0x06008526 RID: 34086 RVA: 0x002B0255 File Offset: 0x002AE455
	public Card GetTarget()
	{
		if (this.m_targets.Count != 0)
		{
			return this.m_targets[0];
		}
		return null;
	}

	// Token: 0x06008527 RID: 34087 RVA: 0x002B0272 File Offset: 0x002AE472
	public void AddTarget(Card card)
	{
		this.m_targets.Add(card);
	}

	// Token: 0x06008528 RID: 34088 RVA: 0x002B0280 File Offset: 0x002AE480
	public void RemoveTarget(Card card)
	{
		this.m_targets.Remove(card);
	}

	// Token: 0x06008529 RID: 34089 RVA: 0x002B028F File Offset: 0x002AE48F
	public void RemoveAllTargets()
	{
		this.m_targets.Clear();
	}

	// Token: 0x0600852A RID: 34090 RVA: 0x002B029C File Offset: 0x002AE49C
	public bool IsTarget(Card card)
	{
		return this.m_targets.Contains(card);
	}

	// Token: 0x0600852B RID: 34091 RVA: 0x002B02AA File Offset: 0x002AE4AA
	public void AddFinishedTaskListCallback(SpellController.FinishedTaskListCallback callback)
	{
		if (this.m_finishedTaskListListeners.Contains(callback))
		{
			return;
		}
		this.m_finishedTaskListListeners.Add(callback);
	}

	// Token: 0x0600852C RID: 34092 RVA: 0x002B02C7 File Offset: 0x002AE4C7
	public void AddFinishedCallback(SpellController.FinishedCallback callback)
	{
		if (this.m_finishedListeners.Contains(callback))
		{
			return;
		}
		this.m_finishedListeners.Add(callback);
	}

	// Token: 0x0600852D RID: 34093 RVA: 0x002B02E4 File Offset: 0x002AE4E4
	public bool IsProcessingTaskList()
	{
		return this.m_processingTaskList;
	}

	// Token: 0x0600852E RID: 34094 RVA: 0x002B02EC File Offset: 0x002AE4EC
	public PowerTaskList GetPowerTaskList()
	{
		return this.m_taskList;
	}

	// Token: 0x0600852F RID: 34095 RVA: 0x002B02F4 File Offset: 0x002AE4F4
	public bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (this.m_taskList != taskList)
		{
			this.DetachPowerTaskList();
			this.m_taskList = taskList;
		}
		this.m_taskListId = this.m_taskList.GetId();
		return this.AddPowerSourceAndTargets(taskList);
	}

	// Token: 0x06008530 RID: 34096 RVA: 0x002B0325 File Offset: 0x002AE525
	public void SetPowerTaskList(PowerTaskList taskList)
	{
		if (this.m_taskList == taskList)
		{
			return;
		}
		this.DetachPowerTaskList();
		this.m_taskList = taskList;
	}

	// Token: 0x06008531 RID: 34097 RVA: 0x002B033F File Offset: 0x002AE53F
	public PowerTaskList DetachPowerTaskList()
	{
		PowerTaskList taskList = this.m_taskList;
		this.RemoveSource();
		this.RemoveAllTargets();
		this.m_taskList = null;
		return taskList;
	}

	// Token: 0x06008532 RID: 34098 RVA: 0x002B035C File Offset: 0x002AE55C
	public void DoPowerTaskList()
	{
		this.m_processingTaskList = true;
		if (this.IsLostFrameTimeCatchUpEnabled())
		{
			float clientLostTimeCatchUpThreshold = GameState.Get().GetClientLostTimeCatchUpThreshold();
			float lostFrameTimeCatchUpSeconds = this.GetLostFrameTimeCatchUpSeconds();
			if (lostFrameTimeCatchUpSeconds > 0f && clientLostTimeCatchUpThreshold > 0f && GameState.Get().GetTimeTracker().GetAccruedLostTimeInSeconds() > Math.Max(lostFrameTimeCatchUpSeconds, clientLostTimeCatchUpThreshold))
			{
				if (GameState.Get().GetTimeTracker() is GameStateFrameTimeTracker)
				{
					GameState.Get().GetTimeTracker().AdjustAccruedLostTime(-lostFrameTimeCatchUpSeconds);
				}
				this.OnFinishedTaskList();
				this.OnFinished();
				return;
			}
		}
		base.gameObject.SetActive(true);
		GameState.Get().AddServerBlockingSpellController(this);
		base.StartCoroutine(this.WaitForCardsThenDoTaskList());
	}

	// Token: 0x06008533 RID: 34099 RVA: 0x001FDA9A File Offset: 0x001FBC9A
	public void ForceKill()
	{
		this.OnFinishedTaskList();
	}

	// Token: 0x06008534 RID: 34100 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool ShouldReconnectIfStuck()
	{
		return true;
	}

	// Token: 0x06008535 RID: 34101 RVA: 0x001FC3FA File Offset: 0x001FA5FA
	protected virtual void OnProcessTaskList()
	{
		this.OnFinishedTaskList();
		this.OnFinished();
	}

	// Token: 0x06008536 RID: 34102 RVA: 0x002B0405 File Offset: 0x002AE605
	protected virtual void OnFinishedTaskList()
	{
		GameState.Get().RemoveServerBlockingSpellController(this);
		this.m_processingTaskList = false;
		this.FireFinishedTaskListCallbacks();
		if (this.m_pendingFinish)
		{
			this.m_pendingFinish = false;
			this.OnFinished();
		}
	}

	// Token: 0x06008537 RID: 34103 RVA: 0x002B0435 File Offset: 0x002AE635
	protected virtual void OnFinished()
	{
		if (this.m_processingTaskList)
		{
			this.m_pendingFinish = true;
			return;
		}
		base.gameObject.SetActive(false);
		this.FireFinishedCallbacks();
	}

	// Token: 0x06008538 RID: 34104 RVA: 0x002B045C File Offset: 0x002AE65C
	protected virtual bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		if (!SpellUtils.CanAddPowerTargets(taskList))
		{
			return false;
		}
		List<Entity> sourceEntities = taskList.GetSourceEntities(true);
		List<Card> list = new List<Card>();
		foreach (Entity entity in sourceEntities)
		{
			if (entity != null)
			{
				list.Add(entity.GetCard());
			}
		}
		this.SetSource(list);
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			PowerTask task = taskList2[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(task);
			if (!(targetCardFromPowerTask == null) && !list.Contains(targetCardFromPowerTask) && !this.IsTarget(targetCardFromPowerTask))
			{
				this.AddTarget(targetCardFromPowerTask);
			}
		}
		if (list.Count > 0)
		{
			if (!list.Exists((Card c) => c == null))
			{
				return true;
			}
		}
		return this.m_targets.Count > 0;
	}

	// Token: 0x06008539 RID: 34105 RVA: 0x002B0578 File Offset: 0x002AE778
	protected virtual bool HasSourceCard(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities(true);
		if (sourceEntities == null || sourceEntities.Count == 0)
		{
			return false;
		}
		List<Card> list = new List<Card>();
		foreach (Entity entity in sourceEntities)
		{
			if (entity != null)
			{
				list.Add(entity.GetCard());
			}
		}
		if (list != null && list.Count != 0)
		{
			if (!list.Exists((Card c) => c == null))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600853A RID: 34106 RVA: 0x000D52B1 File Offset: 0x000D34B1
	protected virtual float GetLostFrameTimeCatchUpSeconds()
	{
		return 0f;
	}

	// Token: 0x0600853B RID: 34107 RVA: 0x002B0620 File Offset: 0x002AE820
	private IEnumerator WaitForCardsThenDoTaskList()
	{
		Card sourceCard = this.GetSource();
		if (sourceCard != null)
		{
			while (this.IsCardBusy(sourceCard))
			{
				yield return null;
			}
		}
		foreach (Card targetCard in this.m_targets)
		{
			if (!(targetCard == null))
			{
				while (this.IsCardBusy(targetCard))
				{
					yield return null;
				}
				targetCard = null;
			}
		}
		List<Card>.Enumerator enumerator = default(List<Card>.Enumerator);
		this.OnProcessTaskList();
		yield break;
		yield break;
	}

	// Token: 0x0600853C RID: 34108 RVA: 0x002B062F File Offset: 0x002AE82F
	protected bool IsLostFrameTimeCatchUpEnabled()
	{
		return SpellController.ALLOW_LOST_FRAME_TIME_CATCH_UP && GameState.Get() != null && GameState.Get().GetGameEntity() != null && GameState.Get().AreLostTimeGuardianConditionsMet() && GameState.Get().GetGameEntity().IsGameSpeedupConditionInEffect();
	}

	// Token: 0x0600853D RID: 34109 RVA: 0x002B0670 File Offset: 0x002AE870
	protected bool IsCardBusy(Card card)
	{
		Entity entity = card.GetEntity();
		return !this.WillEntityLoadCard(entity) && (entity.IsLoadingAssets() || ((!TurnStartManager.Get() || !TurnStartManager.Get().IsCardDrawHandled(card)) && !card.IsActorReady()));
	}

	// Token: 0x0600853E RID: 34110 RVA: 0x002B06C0 File Offset: 0x002AE8C0
	private bool WillEntityLoadCard(Entity entity)
	{
		int entityId = entity.GetEntityId();
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			Network.PowerType type = power.Type;
			if (type == Network.PowerType.FULL_ENTITY)
			{
				Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
				if (entityId == histFullEntity.Entity.ID)
				{
					return true;
				}
			}
			else if (type == Network.PowerType.SHOW_ENTITY)
			{
				Network.HistShowEntity histShowEntity = power as Network.HistShowEntity;
				if (entityId == histShowEntity.Entity.ID)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600853F RID: 34111 RVA: 0x002B0768 File Offset: 0x002AE968
	private void FireFinishedTaskListCallbacks()
	{
		SpellController.FinishedTaskListCallback[] array = this.m_finishedTaskListListeners.ToArray();
		this.m_finishedTaskListListeners.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	// Token: 0x06008540 RID: 34112 RVA: 0x002B07A4 File Offset: 0x002AE9A4
	private void FireFinishedCallbacks()
	{
		SpellController.FinishedCallback[] array = this.m_finishedListeners.ToArray();
		this.m_finishedListeners.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this);
		}
	}

	// Token: 0x06008541 RID: 34113 RVA: 0x002B07E0 File Offset: 0x002AE9E0
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
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, histTagChange.Entity));
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x04006FA8 RID: 28584
	public const float FINISH_FUDGE_SEC = 10f;

	// Token: 0x04006FA9 RID: 28585
	private static readonly PlatformDependentValue<bool> ALLOW_LOST_FRAME_TIME_CATCH_UP = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		PC = false,
		Mac = false,
		iOS = true,
		Android = true
	};

	// Token: 0x04006FAA RID: 28586
	private List<SpellController.FinishedTaskListCallback> m_finishedTaskListListeners = new List<SpellController.FinishedTaskListCallback>();

	// Token: 0x04006FAB RID: 28587
	private List<SpellController.FinishedCallback> m_finishedListeners = new List<SpellController.FinishedCallback>();

	// Token: 0x04006FAC RID: 28588
	protected List<Card> m_sources = new List<Card>();

	// Token: 0x04006FAD RID: 28589
	protected List<Card> m_targets = new List<Card>();

	// Token: 0x04006FAE RID: 28590
	protected PowerTaskList m_taskList;

	// Token: 0x04006FAF RID: 28591
	protected int m_taskListId;

	// Token: 0x04006FB0 RID: 28592
	protected bool m_processingTaskList;

	// Token: 0x04006FB1 RID: 28593
	protected bool m_pendingFinish;

	// Token: 0x02002639 RID: 9785
	// (Invoke) Token: 0x06013636 RID: 79414
	public delegate void FinishedTaskListCallback(SpellController spellController);

	// Token: 0x0200263A RID: 9786
	// (Invoke) Token: 0x0601363A RID: 79418
	public delegate void FinishedCallback(SpellController spellController);
}
