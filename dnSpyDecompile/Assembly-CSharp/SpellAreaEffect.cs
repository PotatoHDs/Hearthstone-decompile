using System;
using UnityEngine;

// Token: 0x0200096D RID: 2413
public class SpellAreaEffect : Spell
{
	// Token: 0x0600850F RID: 34063 RVA: 0x002AFC5F File Offset: 0x002ADE5F
	public override bool AddPowerTargets()
	{
		return base.CanAddPowerTargets() && base.AddMultiplePowerTargets() && base.GetTargets().Count > 0;
	}

	// Token: 0x06008510 RID: 34064 RVA: 0x002AFC84 File Offset: 0x002ADE84
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (this.m_ImpactSpellPrefab == null)
		{
			return;
		}
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			this.SpawnImpactSpell(this.m_targets[i]);
		}
	}

	// Token: 0x06008511 RID: 34065 RVA: 0x002AFCCF File Offset: 0x002ADECF
	private void SpawnImpactSpell(GameObject targetObject)
	{
		Spell component = UnityEngine.Object.Instantiate<GameObject>(this.m_ImpactSpellPrefab.gameObject, targetObject.transform.position, Quaternion.identity).GetComponent<Spell>();
		component.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnImpactSpellStateFinished));
		component.Activate();
	}

	// Token: 0x06008512 RID: 34066 RVA: 0x001FACD3 File Offset: 0x001F8ED3
	private void OnImpactSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x04006FA5 RID: 28581
	public Spell m_ImpactSpellPrefab;
}
