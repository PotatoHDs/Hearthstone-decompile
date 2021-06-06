using System;

// Token: 0x0200081A RID: 2074
public class RainOfFire : SuperSpell
{
	// Token: 0x06006FD2 RID: 28626 RVA: 0x00241413 File Offset: 0x0023F613
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x06006FD3 RID: 28627 RVA: 0x00241440 File Offset: 0x0023F640
	protected override void UpdateVisualTargets()
	{
		int num = this.NumberOfCardsInOpponentsHand();
		this.m_TargetInfo.m_RandomTargetCountMin = num;
		this.m_TargetInfo.m_RandomTargetCountMax = num;
		ZonePlay zonePlay = SpellUtils.FindOpponentPlayZone(this);
		base.GenerateRandomPlayZoneVisualTargets(zonePlay);
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			if (i < this.m_visualTargets.Count)
			{
				this.m_visualTargets[i] = this.m_targets[i];
			}
			else
			{
				this.AddVisualTarget(this.m_targets[i]);
			}
		}
	}

	// Token: 0x06006FD4 RID: 28628 RVA: 0x002414CA File Offset: 0x0023F6CA
	private int NumberOfCardsInOpponentsHand()
	{
		return GameState.Get().GetFirstOpponentPlayer(base.GetSourceCard().GetController()).GetHandZone().GetCardCount();
	}
}
