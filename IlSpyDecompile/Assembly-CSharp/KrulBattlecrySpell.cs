public class KrulBattlecrySpell : Spell
{
	protected override void OnDeath(SpellStateType prevStateType)
	{
		if (m_targets.Count == 0)
		{
			OnStateFinished();
		}
		else
		{
			base.OnAction(prevStateType);
		}
	}
}
