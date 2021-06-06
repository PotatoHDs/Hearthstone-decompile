using UnityEngine;

public class SpellAreaEffect : Spell
{
	public Spell m_ImpactSpellPrefab;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		if (!AddMultiplePowerTargets())
		{
			return false;
		}
		return GetTargets().Count > 0;
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (!(m_ImpactSpellPrefab == null))
		{
			for (int i = 0; i < m_targets.Count; i++)
			{
				SpawnImpactSpell(m_targets[i]);
			}
		}
	}

	private void SpawnImpactSpell(GameObject targetObject)
	{
		Spell component = Object.Instantiate(m_ImpactSpellPrefab.gameObject, targetObject.transform.position, Quaternion.identity).GetComponent<Spell>();
		component.AddStateFinishedCallback(OnImpactSpellStateFinished);
		component.Activate();
	}

	private void OnImpactSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
