using System;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
public class FourHorsemenSpell : SuperSpell
{
	// Token: 0x06006EC8 RID: 28360 RVA: 0x0023B6EE File Offset: 0x002398EE
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_targets.Count <= 0)
		{
			this.OnSpellFinished();
			this.OnStateFinished();
			return;
		}
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		this.FireMissileVolley();
	}

	// Token: 0x06006EC9 RID: 28361 RVA: 0x0023B728 File Offset: 0x00239928
	private void FireMissileVolley()
	{
		if (this.m_MissileSpell != null)
		{
			for (int i = 0; i < this.m_visualTargets.Count; i++)
			{
				this.m_missilesWAitingToFinish++;
				this.FireSingleMissile(i);
			}
		}
	}

	// Token: 0x06006ECA RID: 28362 RVA: 0x0023B770 File Offset: 0x00239970
	private void FireSingleMissile(int targetIndex)
	{
		this.m_effectsPendingFinish++;
		SuperSpell superSpell = (SuperSpell)base.CloneSpell(this.m_MissileSpell, null, null);
		GameObject source = this.m_visualTargets[targetIndex];
		GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(this, SpellLocation.OPPONENT_HERO, null);
		superSpell.SetSource(source);
		superSpell.AddTarget(spellLocationObject);
		if (targetIndex > 0)
		{
			superSpell.m_ImpactInfo = null;
		}
		superSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnMissileFinished));
		superSpell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x06006ECB RID: 28363 RVA: 0x0023B7F0 File Offset: 0x002399F0
	private void OnMissileFinished(Spell spell, object userData)
	{
		this.m_missilesWAitingToFinish--;
		this.DoFinalImpactIfPossible();
	}

	// Token: 0x06006ECC RID: 28364 RVA: 0x0023B808 File Offset: 0x00239A08
	protected void DoFinalImpactIfPossible()
	{
		if (this.m_missilesWAitingToFinish > 0)
		{
			return;
		}
		Spell spell = base.CloneSpell(this.m_DeathSpell, null, null);
		GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(this, SpellLocation.OPPONENT_HERO, null);
		spell.SetSource(spellLocationObject);
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnDeathFinished));
		spell.Activate();
	}

	// Token: 0x06006ECD RID: 28365 RVA: 0x0023B85D File Offset: 0x00239A5D
	private void OnDeathFinished(Spell spell, object userData)
	{
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x040058E8 RID: 22760
	public SuperSpell m_MissileSpell;

	// Token: 0x040058E9 RID: 22761
	public Spell m_DeathSpell;

	// Token: 0x040058EA RID: 22762
	private int m_missilesWAitingToFinish;
}
