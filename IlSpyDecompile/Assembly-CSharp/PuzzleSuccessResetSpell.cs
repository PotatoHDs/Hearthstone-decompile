public class PuzzleSuccessResetSpell : Spell
{
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		return true;
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		EndTurnButton endTurnButton = EndTurnButton.Get();
		if (endTurnButton != null)
		{
			endTurnButton.RemoveInputBlocker();
		}
	}
}
