public class EchoOfMedivh : SpawnToHandSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		Player controller = GetSourceCard().GetEntity().GetController();
		ZonePlay battlefieldZone = controller.GetBattlefieldZone();
		if (controller.IsRevealed())
		{
			for (int i = 0; i < m_targets.Count; i++)
			{
				string cardIdForTarget = GetCardIdForTarget(i);
				for (int j = 0; j < battlefieldZone.GetCardCount(); j++)
				{
					Card cardAtIndex = battlefieldZone.GetCardAtIndex(j);
					if (cardAtIndex.GetPredictedZonePosition() == 0)
					{
						string cardId = cardAtIndex.GetEntity().GetCardId();
						if (!(cardIdForTarget != cardId) && AddUniqueOriginForTarget(i, cardAtIndex))
						{
							break;
						}
					}
				}
			}
		}
		else
		{
			int num = 0;
			for (int k = 0; k < m_targets.Count; k++)
			{
				Card cardAtIndex2 = battlefieldZone.GetCardAtIndex(num);
				AddOriginForTarget(k, cardAtIndex2);
				num++;
			}
		}
		base.OnAction(prevStateType);
	}
}
