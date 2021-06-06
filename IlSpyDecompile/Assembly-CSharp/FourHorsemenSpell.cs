using UnityEngine;

public class FourHorsemenSpell : SuperSpell
{
	public SuperSpell m_MissileSpell;

	public Spell m_DeathSpell;

	private int m_missilesWAitingToFinish;

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_targets.Count <= 0)
		{
			OnSpellFinished();
			OnStateFinished();
		}
		else
		{
			m_effectsPendingFinish++;
			base.OnAction(prevStateType);
			FireMissileVolley();
		}
	}

	private void FireMissileVolley()
	{
		if (m_MissileSpell != null)
		{
			for (int i = 0; i < m_visualTargets.Count; i++)
			{
				m_missilesWAitingToFinish++;
				FireSingleMissile(i);
			}
		}
	}

	private void FireSingleMissile(int targetIndex)
	{
		m_effectsPendingFinish++;
		SuperSpell superSpell = (SuperSpell)CloneSpell(m_MissileSpell);
		GameObject source = m_visualTargets[targetIndex];
		GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(this, SpellLocation.OPPONENT_HERO);
		superSpell.SetSource(source);
		superSpell.AddTarget(spellLocationObject);
		if (targetIndex > 0)
		{
			superSpell.m_ImpactInfo = null;
		}
		superSpell.AddFinishedCallback(OnMissileFinished);
		superSpell.ActivateState(SpellStateType.ACTION);
	}

	private void OnMissileFinished(Spell spell, object userData)
	{
		m_missilesWAitingToFinish--;
		DoFinalImpactIfPossible();
	}

	protected void DoFinalImpactIfPossible()
	{
		if (m_missilesWAitingToFinish <= 0)
		{
			Spell spell = CloneSpell(m_DeathSpell);
			GameObject spellLocationObject = SpellUtils.GetSpellLocationObject(this, SpellLocation.OPPONENT_HERO);
			spell.SetSource(spellLocationObject);
			spell.AddFinishedCallback(OnDeathFinished);
			spell.Activate();
		}
	}

	private void OnDeathFinished(Spell spell, object userData)
	{
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
