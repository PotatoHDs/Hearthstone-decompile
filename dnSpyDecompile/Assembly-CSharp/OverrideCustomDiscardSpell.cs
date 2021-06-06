using System;
using UnityEngine;

// Token: 0x0200080F RID: 2063
public class OverrideCustomDiscardSpell : SuperSpell
{
	// Token: 0x06006F92 RID: 28562 RVA: 0x0023FAF4 File Offset: 0x0023DCF4
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Card>().OverrideCustomDiscardSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomDiscardSpell));
			}
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x04005978 RID: 22904
	public Spell m_CustomDiscardSpell;
}
