public class OverkillAuxiliarySpell : Spell
{
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): No source card found.", this);
			return false;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Current tasklist has no source entity.", this);
			return false;
		}
		if (!entity.IsSpell())
		{
			return true;
		}
		return false;
	}
}
