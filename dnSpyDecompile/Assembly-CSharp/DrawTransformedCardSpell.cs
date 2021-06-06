using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007EB RID: 2027
public class DrawTransformedCardSpell : SuperSpell
{
	// Token: 0x06006EA8 RID: 28328 RVA: 0x0023ADDC File Offset: 0x00238FDC
	public override bool AddPowerTargets()
	{
		base.AddPowerTargets();
		return this.FindTransformTask();
	}

	// Token: 0x06006EA9 RID: 28329 RVA: 0x0023ADEC File Offset: 0x00238FEC
	private bool FindTransformTask()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.CHANGE_ENTITY)
			{
				Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)power;
				Entity entity = GameState.Get().GetEntity(histChangeEntity.Entity.ID);
				if (entity != null)
				{
					Card card = entity.GetCard();
					if (!(card == null) && (!this.m_FriendlyOnly || card.GetEntity().IsControlledByFriendlySidePlayer()))
					{
						this.m_transformTaskIndex = i;
						this.AddTarget(card.gameObject);
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06006EAA RID: 28330 RVA: 0x0023AE92 File Offset: 0x00239092
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoTasksBeforeTransform());
		base.StartCoroutine(this.DoEffectWithTiming());
	}

	// Token: 0x06006EAB RID: 28331 RVA: 0x0023AEB5 File Offset: 0x002390B5
	private IEnumerator DoTasksBeforeTransform()
	{
		bool complete = false;
		this.m_taskList.DoTasks(0, this.m_transformTaskIndex, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006EAC RID: 28332 RVA: 0x0023AEC4 File Offset: 0x002390C4
	private IEnumerator DoEffectWithTiming()
	{
		yield return new WaitForSeconds(this.m_OldCardHoldTime);
		bool complete = false;
		this.m_taskList.DoTasks(this.m_transformTaskIndex, 1, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		PowerTask transformTask = this.m_taskList.GetTaskList()[this.m_transformTaskIndex];
		transformTask.SetCompleted(false);
		yield return new WaitForSeconds(this.m_NewCardHoldTime);
		transformTask.SetCompleted(true);
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x040058C5 RID: 22725
	public float m_OldCardHoldTime;

	// Token: 0x040058C6 RID: 22726
	public float m_NewCardHoldTime;

	// Token: 0x040058C7 RID: 22727
	public bool m_FriendlyOnly;

	// Token: 0x040058C8 RID: 22728
	private int m_transformTaskIndex;
}
