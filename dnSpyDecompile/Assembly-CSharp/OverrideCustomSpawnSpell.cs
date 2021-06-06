using System;
using UnityEngine;

// Token: 0x02000810 RID: 2064
public class OverrideCustomSpawnSpell : SuperSpell
{
	// Token: 0x06006F94 RID: 28564 RVA: 0x0023FB88 File Offset: 0x0023DD88
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (this.m_CustomSpawnSpell == null)
		{
			Debug.LogError("OverrideCustomSpawnSpell.OverrideCustomSpawnSpell in null!");
			this.m_effectsPendingFinish--;
			base.FinishIfPossible();
			return;
		}
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				gameObject.GetComponent<Card>().OverrideCustomSpawnSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomSpawnSpell));
			}
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x04005979 RID: 22905
	public Spell m_CustomSpawnSpell;
}
