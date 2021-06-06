using System;

// Token: 0x0200080B RID: 2059
public class NefarianTB : Spell
{
	// Token: 0x06006F80 RID: 28544 RVA: 0x0023F2CC File Offset: 0x0023D4CC
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.BlockZoneLayout();
		base.OnAction(prevStateType);
	}

	// Token: 0x06006F81 RID: 28545 RVA: 0x0023F2DC File Offset: 0x0023D4DC
	private void BlockZoneLayout()
	{
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			return;
		}
		Player controller = sourceCard.GetController();
		if (controller == null)
		{
			return;
		}
		ZonePlay battlefieldZone = controller.GetBattlefieldZone();
		if (battlefieldZone == null)
		{
			return;
		}
		battlefieldZone.AddLayoutBlocker();
	}
}
