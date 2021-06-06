using UnityEngine;

public class OverrideCustomDiscardSpell : SuperSpell
{
	public Spell m_CustomDiscardSpell;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject visualTarget in GetVisualTargets())
		{
			if (!(visualTarget == null))
			{
				visualTarget.GetComponent<Card>().OverrideCustomDiscardSpell(Object.Instantiate(m_CustomDiscardSpell));
			}
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
