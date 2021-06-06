using System;

// Token: 0x02000821 RID: 2081
public class SpawnMinionsToHandFromPlaySpell : SpawnToHandSpell
{
	// Token: 0x06006FF1 RID: 28657 RVA: 0x00241B58 File Offset: 0x0023FD58
	protected override void OnAction(SpellStateType prevStateType)
	{
		ZonePlay battlefieldZone = base.GetSourceCard().GetEntity().GetController().GetBattlefieldZone();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			for (int j = 0; j < battlefieldZone.GetCardCount(); j++)
			{
				Card cardAtIndex = battlefieldZone.GetCardAtIndex(j);
				Card component = this.m_targets[i].GetComponent<Card>();
				if (cardAtIndex.GetEntity().GetEntityId() == component.GetEntity().GetRealTimeLinkedEntityId() && base.AddUniqueOriginForTarget(i, cardAtIndex))
				{
					break;
				}
			}
		}
		base.OnAction(prevStateType);
	}
}
