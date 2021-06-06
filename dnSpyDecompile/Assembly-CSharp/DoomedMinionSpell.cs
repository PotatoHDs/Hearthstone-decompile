using System;
using UnityEngine;

// Token: 0x020007EA RID: 2026
public class DoomedMinionSpell : SuperSpell
{
	// Token: 0x06006EA6 RID: 28326 RVA: 0x0023AD4C File Offset: 0x00238F4C
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Card>().ActivateActorSpell(this.m_SpellType);
			}
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x040058C4 RID: 22724
	public SpellType m_SpellType;
}
