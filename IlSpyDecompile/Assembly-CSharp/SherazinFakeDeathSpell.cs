using PegasusGame;

public class SherazinFakeDeathSpell : OverrideCustomSpawnSpell
{
	private bool m_mustPlayFakeDeath;

	public float m_delayBeforeHideActor = 3f;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		if (m_taskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		if (GetSourceCard() == null || GetSourceCard().GetEntity() == null)
		{
			Log.Spells.PrintError("SherazinFakeDeathSpell.AddPowerTargets(): Failed to find source entity for Sherazin.");
			return false;
		}
		if (GetSourceCard().GetEntity().GetZone() == TAG_ZONE.PLAY)
		{
			m_mustPlayFakeDeath = true;
			return true;
		}
		m_mustPlayFakeDeath = false;
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_mustPlayFakeDeath)
		{
			m_effectsPendingFinish++;
			base.OnAction(prevStateType);
			GetSourceCard().FakeDeath();
			GetSourceCard().SetDelayBeforeHideInNullZoneVisuals(m_delayBeforeHideActor);
			m_effectsPendingFinish--;
			OnSpellFinished();
			OnStateFinished();
		}
		else
		{
			base.OnAction(prevStateType);
		}
	}
}
