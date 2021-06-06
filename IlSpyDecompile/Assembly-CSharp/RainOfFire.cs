public class RainOfFire : SuperSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	protected override void UpdateVisualTargets()
	{
		int num = NumberOfCardsInOpponentsHand();
		m_TargetInfo.m_RandomTargetCountMin = num;
		m_TargetInfo.m_RandomTargetCountMax = num;
		ZonePlay zonePlay = SpellUtils.FindOpponentPlayZone(this);
		GenerateRandomPlayZoneVisualTargets(zonePlay);
		for (int i = 0; i < m_targets.Count; i++)
		{
			if (i < m_visualTargets.Count)
			{
				m_visualTargets[i] = m_targets[i];
			}
			else
			{
				AddVisualTarget(m_targets[i]);
			}
		}
	}

	private int NumberOfCardsInOpponentsHand()
	{
		return GameState.Get().GetFirstOpponentPlayer(GetSourceCard().GetController()).GetHandZone()
			.GetCardCount();
	}
}
