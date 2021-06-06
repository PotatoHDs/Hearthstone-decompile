using UnityEngine;

public class OverrideCustomDeathSpell : SuperSpell
{
	public Spell m_CustomDeathSpell;

	public bool m_SuppressKeywordDeaths = true;

	public float m_KeywordDeathDelay = 0.6f;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject visualTarget in GetVisualTargets())
		{
			if (!(visualTarget == null))
			{
				Card component = visualTarget.GetComponent<Card>();
				component.OverrideCustomDeathSpell(Object.Instantiate(m_CustomDeathSpell));
				component.SuppressKeywordDeaths(m_SuppressKeywordDeaths);
				component.SetKeywordDeathDelaySec(m_KeywordDeathDelay);
			}
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
