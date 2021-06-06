using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000838 RID: 2104
public class VarianWrynn : SuperSpell
{
	// Token: 0x06007078 RID: 28792 RVA: 0x002449BC File Offset: 0x00242BBC
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoVariansCoolThing());
	}

	// Token: 0x06007079 RID: 28793 RVA: 0x002449E0 File Offset: 0x00242BE0
	private IEnumerator DoVariansCoolThing()
	{
		Card card = this.m_taskList.GetSourceEntity(true).GetCard();
		List<GameObject> fxObjects = new List<GameObject>();
		if (this.m_varianSpellPrefab != null && this.m_taskList.IsOrigin())
		{
			Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_varianSpellPrefab);
			fxObjects.Add(spell2.gameObject);
			spell2.SetSource(card.gameObject);
			spell2.Activate();
		}
		List<PowerTask> tasks = this.m_taskList.GetTaskList();
		bool foundTarget = false;
		bool lastWasMinion = false;
		int num;
		for (int i = 0; i < tasks.Count; i = num)
		{
			VarianWrynn.<>c__DisplayClass5_0 CS$<>8__locals1 = new VarianWrynn.<>c__DisplayClass5_0();
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY)
			{
				Network.HistShowEntity showEntity = (Network.HistShowEntity)power;
				if (!foundTarget)
				{
					Card card2 = GameState.Get().GetEntity(showEntity.Entity.ID).GetCard();
					foundTarget = true;
					if (this.m_deckSpellPrefab != null && this.m_taskList.IsOrigin())
					{
						Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_deckSpellPrefab);
						fxObjects.Add(spell.gameObject);
						spell.SetSource(card2.gameObject);
						spell.Activate();
						while (!spell.IsFinished())
						{
							yield return null;
						}
						spell = null;
					}
				}
				CS$<>8__locals1.complete = false;
				PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
				{
					CS$<>8__locals1.complete = true;
				};
				this.m_taskList.DoTasks(0, i, callback);
				if (lastWasMinion)
				{
					yield return new WaitForSeconds(this.m_spellLeadTime);
				}
				lastWasMinion = this.IsMinion(showEntity);
				while (!CS$<>8__locals1.complete)
				{
					yield return null;
				}
				CS$<>8__locals1 = null;
				showEntity = null;
			}
			num = i + 1;
		}
		foreach (GameObject obj in fxObjects)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x0600707A RID: 28794 RVA: 0x002449F0 File Offset: 0x00242BF0
	private bool IsMinion(Network.HistShowEntity showEntity)
	{
		for (int i = 0; i < showEntity.Entity.Tags.Count; i++)
		{
			Network.Entity.Tag tag = showEntity.Entity.Tags[i];
			if (tag.Name == 202)
			{
				return tag.Value == 4;
			}
		}
		return false;
	}

	// Token: 0x04005A61 RID: 23137
	public string m_perMinionSound;

	// Token: 0x04005A62 RID: 23138
	public Spell m_varianSpellPrefab;

	// Token: 0x04005A63 RID: 23139
	public Spell m_deckSpellPrefab;

	// Token: 0x04005A64 RID: 23140
	public float m_spellLeadTime = 1f;
}
