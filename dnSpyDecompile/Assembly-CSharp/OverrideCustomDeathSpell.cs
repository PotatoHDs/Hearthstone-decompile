using System;
using UnityEngine;

// Token: 0x0200080E RID: 2062
public class OverrideCustomDeathSpell : SuperSpell
{
	// Token: 0x06006F90 RID: 28560 RVA: 0x0023FA2C File Offset: 0x0023DC2C
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				Card component = gameObject.GetComponent<Card>();
				component.OverrideCustomDeathSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomDeathSpell));
				component.SuppressKeywordDeaths(this.m_SuppressKeywordDeaths);
				component.SetKeywordDeathDelaySec(this.m_KeywordDeathDelay);
			}
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x04005975 RID: 22901
	public Spell m_CustomDeathSpell;

	// Token: 0x04005976 RID: 22902
	public bool m_SuppressKeywordDeaths = true;

	// Token: 0x04005977 RID: 22903
	public float m_KeywordDeathDelay = 0.6f;
}
