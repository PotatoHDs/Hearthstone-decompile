using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007EC RID: 2028
public class EchoingOozeSpell : Spell
{
	// Token: 0x06006EAE RID: 28334 RVA: 0x0023AED4 File Offset: 0x002390D4
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.HistFullEntity histFullEntity = task.GetPower() as Network.HistFullEntity;
		if (histFullEntity == null)
		{
			return null;
		}
		Network.Entity entity = histFullEntity.Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, entity.ID));
			return null;
		}
		return entity2.GetCard();
	}

	// Token: 0x06006EAF RID: 28335 RVA: 0x0023AF30 File Offset: 0x00239130
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		Card targetCard = base.GetTargetCard();
		if (targetCard == null)
		{
			this.OnStateFinished();
			return;
		}
		this.DoEffect(targetCard);
	}

	// Token: 0x06006EB0 RID: 28336 RVA: 0x0023AF64 File Offset: 0x00239164
	private void DoEffect(Card targetCard)
	{
		Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_CustomSpawnSpell);
		targetCard.OverrideCustomSpawnSpell(spell);
		this.DoTasksUntilSpawn(targetCard);
		base.StartCoroutine(this.WaitThenFinish());
	}

	// Token: 0x06006EB1 RID: 28337 RVA: 0x0023AF98 File Offset: 0x00239198
	private void DoTasksUntilSpawn(Card targetCard)
	{
		int entityId = targetCard.GetEntity().GetEntityId();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		int num = 0;
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity != null && histFullEntity.Entity.ID == entityId)
			{
				num = i;
				break;
			}
		}
		this.m_taskList.DoTasks(0, num + 1);
	}

	// Token: 0x06006EB2 RID: 28338 RVA: 0x0023B009 File Offset: 0x00239209
	private IEnumerator WaitThenFinish()
	{
		float num = UnityEngine.Random.Range(this.m_PostSpawnDelayMin, this.m_PostSpawnDelayMax);
		if (!Mathf.Approximately(num, 0f))
		{
			yield return new WaitForSeconds(num);
		}
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x040058C9 RID: 22729
	public Spell m_CustomSpawnSpell;

	// Token: 0x040058CA RID: 22730
	public float m_PostSpawnDelayMin;

	// Token: 0x040058CB RID: 22731
	public float m_PostSpawnDelayMax;
}
