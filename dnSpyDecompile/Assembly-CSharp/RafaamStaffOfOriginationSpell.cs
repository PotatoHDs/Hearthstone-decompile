using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000819 RID: 2073
public class RafaamStaffOfOriginationSpell : Spell
{
	// Token: 0x06006FCB RID: 28619 RVA: 0x0024127C File Offset: 0x0023F47C
	public override bool AddPowerTargets()
	{
		if (!this.m_taskList.DoesBlockHaveMetaDataTasks())
		{
			return false;
		}
		this.m_spawnTaskIndex = -1;
		bool flag = false;
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange != null && histTagChange.Tag == 420)
			{
				flag = true;
			}
			else
			{
				Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
				if (histFullEntity != null && flag)
				{
					Card card = GameState.Get().GetEntity(histFullEntity.Entity.ID).GetCard();
					if (!(card == null))
					{
						this.m_targets.Add(card.gameObject);
						this.m_spawnTaskIndex = i;
						break;
					}
				}
			}
		}
		return this.m_spawnTaskIndex >= 0;
	}

	// Token: 0x06006FCC RID: 28620 RVA: 0x0024134C File Offset: 0x0023F54C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.ApplyCustomSpawnOverride();
		this.DoTasksUntilSpawn();
	}

	// Token: 0x06006FCD RID: 28621 RVA: 0x00241364 File Offset: 0x0023F564
	private void ApplyCustomSpawnOverride()
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_CustomSpawnSpell);
			component.OverrideCustomSpawnSpell(spell);
		}
	}

	// Token: 0x06006FCE RID: 28622 RVA: 0x002413C8 File Offset: 0x0023F5C8
	private void DoTasksUntilSpawn()
	{
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			base.StartCoroutine(this.WaitThenFinish());
		};
		this.m_taskList.DoTasks(0, this.m_spawnTaskIndex, callback);
	}

	// Token: 0x06006FCF RID: 28623 RVA: 0x002413F5 File Offset: 0x0023F5F5
	private IEnumerator WaitThenFinish()
	{
		Network.HistFullEntity histFullEntity = (Network.HistFullEntity)this.m_taskList.GetTaskList()[this.m_spawnTaskIndex].GetPower();
		Card heroPowerCard = GameState.Get().GetEntity(histFullEntity.Entity.ID).GetHeroPowerCard();
		Spell electricSpell = heroPowerCard.GetActorSpell(SpellType.ELECTRIC_CHARGE_LEVEL_LARGE, true);
		while (!electricSpell.IsFinished())
		{
			yield return null;
		}
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x040059A9 RID: 22953
	public Spell m_CustomSpawnSpell;

	// Token: 0x040059AA RID: 22954
	private int m_spawnTaskIndex;
}
