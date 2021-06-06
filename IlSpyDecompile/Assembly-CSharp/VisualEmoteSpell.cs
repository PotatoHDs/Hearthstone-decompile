using UnityEngine;

public class VisualEmoteSpell : Spell
{
	public Spell m_FriendlySpellPrefab;

	public Spell m_OpponentSpellPrefab;

	public bool m_PositionOnSpeechBubble;

	protected int m_effectsPendingFinish;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		Spell spell = null;
		Card sourceCard = GetSourceCard();
		if (sourceCard != null)
		{
			Player controller = sourceCard.GetController();
			if (controller != null)
			{
				if (controller.IsFriendlySide())
				{
					spell = m_FriendlySpellPrefab;
				}
				else if (!controller.IsFriendlySide())
				{
					spell = m_OpponentSpellPrefab;
				}
			}
		}
		if (spell != null)
		{
			Spell spell2 = Object.Instantiate(spell);
			spell2.SetSource(GetSource());
			spell2.AddStateFinishedCallback(OnSpellStateFinished);
			spell2.AddFinishedCallback(OnSpellEffectFinished);
			m_effectsPendingFinish++;
			spell2.Activate();
		}
		if (!HasStateContent(SpellStateType.BIRTH))
		{
			OnStateFinished();
		}
	}

	private void FinishIfPossible()
	{
		if (m_effectsPendingFinish == 0)
		{
			base.OnSpellFinished();
		}
	}

	public override void OnSpellFinished()
	{
		FinishIfPossible();
	}

	private void OnSpellEffectFinished(Spell spell, object userData)
	{
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private void OnSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell);
		}
	}
}
