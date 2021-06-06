using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200083A RID: 2106
public class YoggSaronSpell : Spell
{
	// Token: 0x06007082 RID: 28802 RVA: 0x00244CD0 File Offset: 0x00242ED0
	public override bool CanPurge()
	{
		return YoggSaronSpell.s_mistSpellInstances.Count == 0;
	}

	// Token: 0x06007083 RID: 28803 RVA: 0x00244CE0 File Offset: 0x00242EE0
	public override bool AddPowerTargets()
	{
		int id = this.m_taskList.GetOrigin().GetId();
		return !YoggSaronSpell.s_mistSpellInstances.ContainsKey(id) || this.m_taskList.IsEndOfBlock();
	}

	// Token: 0x06007084 RID: 28804 RVA: 0x00244D1B File Offset: 0x00242F1B
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoEffectsWithTiming());
	}

	// Token: 0x06007085 RID: 28805 RVA: 0x00244D31 File Offset: 0x00242F31
	private IEnumerator DoEffectsWithTiming()
	{
		int taskListID = this.m_taskList.GetOrigin().GetId();
		Spell mistSpellInstance = null;
		if (!YoggSaronSpell.s_mistSpellInstances.ContainsKey(taskListID))
		{
			mistSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_MistSpellPrefab);
			YoggSaronSpell.s_mistSpellInstances[taskListID] = mistSpellInstance;
			if (mistSpellInstance)
			{
				mistSpellInstance.ActivateState(SpellStateType.BIRTH);
				while (mistSpellInstance.GetActiveState() != SpellStateType.IDLE)
				{
					yield return null;
				}
			}
		}
		else
		{
			mistSpellInstance = YoggSaronSpell.s_mistSpellInstances[taskListID];
		}
		if (mistSpellInstance && this.m_taskList.IsEndOfBlock())
		{
			mistSpellInstance.ActivateState(SpellStateType.DEATH);
			while (!mistSpellInstance.IsFinished())
			{
				yield return null;
			}
			this.OnSpellFinished();
			while (mistSpellInstance.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
			YoggSaronSpell.s_mistSpellInstances.Remove(taskListID);
			UnityEngine.Object.Destroy(mistSpellInstance.gameObject);
		}
		if (base.GetActiveState() != SpellStateType.NONE)
		{
			this.OnStateFinished();
		}
		yield break;
	}

	// Token: 0x04005A67 RID: 23143
	public Spell m_MistSpellPrefab;

	// Token: 0x04005A68 RID: 23144
	private static Map<int, Spell> s_mistSpellInstances = new Map<int, Spell>();
}
