public class NefarianTB : Spell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		BlockZoneLayout();
		base.OnAction(prevStateType);
	}

	private void BlockZoneLayout()
	{
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			return;
		}
		Player controller = sourceCard.GetController();
		if (controller != null)
		{
			ZonePlay battlefieldZone = controller.GetBattlefieldZone();
			if (!(battlefieldZone == null))
			{
				battlefieldZone.AddLayoutBlocker();
			}
		}
	}
}
