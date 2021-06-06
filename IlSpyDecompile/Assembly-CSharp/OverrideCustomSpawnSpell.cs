using UnityEngine;

public class OverrideCustomSpawnSpell : SuperSpell
{
	public Spell m_CustomSpawnSpell;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (m_CustomSpawnSpell == null)
		{
			Debug.LogError("OverrideCustomSpawnSpell.OverrideCustomSpawnSpell in null!");
			m_effectsPendingFinish--;
			FinishIfPossible();
			return;
		}
		foreach (GameObject visualTarget in GetVisualTargets())
		{
			if (!(visualTarget == null))
			{
				visualTarget.GetComponent<Card>().OverrideCustomSpawnSpell(Object.Instantiate(m_CustomSpawnSpell));
			}
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
