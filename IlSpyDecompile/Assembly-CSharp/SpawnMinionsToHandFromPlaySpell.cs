public class SpawnMinionsToHandFromPlaySpell : SpawnToHandSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		ZonePlay battlefieldZone = GetSourceCard().GetEntity().GetController().GetBattlefieldZone();
		for (int i = 0; i < m_targets.Count; i++)
		{
			for (int j = 0; j < battlefieldZone.GetCardCount(); j++)
			{
				Card cardAtIndex = battlefieldZone.GetCardAtIndex(j);
				Card component = m_targets[i].GetComponent<Card>();
				if (cardAtIndex.GetEntity().GetEntityId() == component.GetEntity().GetRealTimeLinkedEntityId() && AddUniqueOriginForTarget(i, cardAtIndex))
				{
					break;
				}
			}
		}
		base.OnAction(prevStateType);
	}
}
