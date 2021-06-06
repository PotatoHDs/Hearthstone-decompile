using System;

// Token: 0x020006DC RID: 1756
public class PuzzleSuccessResetSpell : Spell
{
	// Token: 0x0600621F RID: 25119 RVA: 0x00200664 File Offset: 0x001FE864
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		return base.AttachPowerTaskList(taskList);
	}

	// Token: 0x06006220 RID: 25120 RVA: 0x00200674 File Offset: 0x001FE874
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		EndTurnButton endTurnButton = EndTurnButton.Get();
		if (endTurnButton != null)
		{
			endTurnButton.RemoveInputBlocker();
		}
	}
}
