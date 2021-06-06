using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x020007E6 RID: 2022
public class DefileSpell : SuperSpell
{
	// Token: 0x06006E7F RID: 28287 RVA: 0x0023A249 File Offset: 0x00238449
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DefileEffect());
	}

	// Token: 0x06006E80 RID: 28288 RVA: 0x0023A270 File Offset: 0x00238470
	private void FindTargetsForSingleCast(int index)
	{
		this.m_singleCastTargets.Clear();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = index + 1; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.EFFECT_TIMING)
				{
					return;
				}
				if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Info != null && histMetaData.Info.Count != 0)
				{
					for (int j = 0; j < histMetaData.Info.Count; j++)
					{
						global::Entity entity = GameState.Get().GetEntity(histMetaData.Info[j]);
						if (entity != null)
						{
							Card card = entity.GetCard();
							this.m_singleCastTargets.Add(card.gameObject);
						}
					}
				}
			}
		}
	}

	// Token: 0x06006E81 RID: 28289 RVA: 0x0023A346 File Offset: 0x00238546
	private IEnumerator DefileEffect()
	{
		this.m_singleCastTargets = new List<GameObject>();
		Card sourceCard = this.m_taskList.GetSourceEntity(true).GetCard();
		List<PowerTask> tasks = this.m_taskList.GetTaskList();
		int num;
		for (int i = 0; i < tasks.Count; i = num)
		{
			DefileSpell.<>c__DisplayClass5_0 CS$<>8__locals1 = new DefileSpell.<>c__DisplayClass5_0();
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == HistoryMeta.Type.EFFECT_TIMING)
			{
				CS$<>8__locals1.complete = false;
				PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
				{
					CS$<>8__locals1.complete = true;
				};
				this.FindTargetsForSingleCast(i);
				this.m_taskList.DoTasks(0, i, callback);
				while (!CS$<>8__locals1.complete)
				{
					yield return null;
				}
				if (this.m_SpellPrefab != null)
				{
					this.m_effectsPendingFinish++;
					Spell spell = base.CloneSpell(this.m_SpellPrefab, null, null);
					spell.SetSource(sourceCard.gameObject);
					spell.AddTargets(this.m_singleCastTargets);
					spell.ActivateState(SpellStateType.ACTION);
					while (!spell.IsFinished())
					{
						yield return null;
					}
					spell = null;
				}
				yield return new WaitForSeconds(this.m_TimeBetweenCasts);
				CS$<>8__locals1 = null;
			}
			num = i + 1;
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x040058B1 RID: 22705
	public Spell m_SpellPrefab;

	// Token: 0x040058B2 RID: 22706
	public float m_TimeBetweenCasts = 1f;

	// Token: 0x040058B3 RID: 22707
	private List<GameObject> m_singleCastTargets;
}
