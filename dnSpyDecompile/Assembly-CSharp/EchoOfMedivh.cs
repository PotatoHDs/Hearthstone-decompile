using System;

// Token: 0x020007ED RID: 2029
public class EchoOfMedivh : SpawnToHandSpell
{
	// Token: 0x06006EB4 RID: 28340 RVA: 0x0023B018 File Offset: 0x00239218
	protected override void OnAction(SpellStateType prevStateType)
	{
		Player controller = base.GetSourceCard().GetEntity().GetController();
		ZonePlay battlefieldZone = controller.GetBattlefieldZone();
		if (controller.IsRevealed())
		{
			for (int i = 0; i < this.m_targets.Count; i++)
			{
				string cardIdForTarget = base.GetCardIdForTarget(i);
				for (int j = 0; j < battlefieldZone.GetCardCount(); j++)
				{
					Card cardAtIndex = battlefieldZone.GetCardAtIndex(j);
					if (cardAtIndex.GetPredictedZonePosition() == 0)
					{
						string cardId = cardAtIndex.GetEntity().GetCardId();
						if (!(cardIdForTarget != cardId) && base.AddUniqueOriginForTarget(i, cardAtIndex))
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
			for (int k = 0; k < this.m_targets.Count; k++)
			{
				Card cardAtIndex2 = battlefieldZone.GetCardAtIndex(num);
				base.AddOriginForTarget(k, cardAtIndex2);
				num++;
			}
		}
		base.OnAction(prevStateType);
	}
}
