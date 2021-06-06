using UnityEngine;

public class DoomedMinionSpell : SuperSpell
{
	public SpellType m_SpellType;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject visualTarget in GetVisualTargets())
		{
			if (!(visualTarget == null))
			{
				visualTarget.GetComponent<Card>().ActivateActorSpell(m_SpellType);
			}
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
