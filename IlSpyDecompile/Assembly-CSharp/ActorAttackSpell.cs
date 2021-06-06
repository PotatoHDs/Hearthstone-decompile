using System.Collections;

public class ActorAttackSpell : Spell
{
	private bool m_waitingToAct = true;

	protected override void Start()
	{
		base.Start();
	}

	protected override void OnBirth(SpellStateType prevStateType)
	{
		m_waitingToAct = true;
		base.OnBirth(prevStateType);
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(WaitThenDoAction(prevStateType));
	}

	private void StopWaitingToAct()
	{
		m_waitingToAct = false;
	}

	protected IEnumerator WaitThenDoAction(SpellStateType prevStateType)
	{
		while (m_waitingToAct)
		{
			yield return null;
		}
		base.OnAction(prevStateType);
	}
}
