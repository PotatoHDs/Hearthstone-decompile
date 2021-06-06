using UnityEngine;

public class DistinctSpellOnEachSide : Spell
{
	public Spell m_FriendlySideSpell;

	public Spell m_OpponentSideSpell;

	private Spell m_oneSideSpell;

	private bool InitSpell()
	{
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			return false;
		}
		Spell original = ((sourceCard.GetControllerSide() == Player.Side.FRIENDLY) ? m_FriendlySideSpell : m_OpponentSideSpell);
		m_oneSideSpell = Object.Instantiate(original);
		m_oneSideSpell.SetSource(sourceCard.gameObject);
		return true;
	}

	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!InitSpell())
		{
			return false;
		}
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		return m_oneSideSpell.AttachPowerTaskList(taskList);
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		m_oneSideSpell.AddFinishedCallback(OnOneSideSpellFinished);
		m_oneSideSpell.AddStateFinishedCallback(OnOneSideSpellStateFinished);
		m_oneSideSpell.ActivateState(SpellStateType.ACTION);
	}

	private void OnOneSideSpellFinished(Spell spell, object userData)
	{
		OnSpellFinished();
	}

	private void OnOneSideSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		Object.Destroy(m_oneSideSpell.gameObject);
		m_oneSideSpell = null;
		Deactivate();
	}
}
