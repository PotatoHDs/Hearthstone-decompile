using System;

// Token: 0x02000801 RID: 2049
public class KrulBattlecrySpell : Spell
{
	// Token: 0x06006F2C RID: 28460 RVA: 0x0023CB0E File Offset: 0x0023AD0E
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (this.m_targets.Count == 0)
		{
			this.OnStateFinished();
			return;
		}
		base.OnAction(prevStateType);
	}
}
