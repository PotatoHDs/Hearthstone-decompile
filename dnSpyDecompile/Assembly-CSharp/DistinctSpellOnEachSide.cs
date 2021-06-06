using System;
using UnityEngine;

// Token: 0x020007E9 RID: 2025
public class DistinctSpellOnEachSide : Spell
{
	// Token: 0x06006EA0 RID: 28320 RVA: 0x0023AC5C File Offset: 0x00238E5C
	private bool InitSpell()
	{
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			return false;
		}
		Spell original = (sourceCard.GetControllerSide() == Player.Side.FRIENDLY) ? this.m_FriendlySideSpell : this.m_OpponentSideSpell;
		this.m_oneSideSpell = UnityEngine.Object.Instantiate<Spell>(original);
		this.m_oneSideSpell.SetSource(sourceCard.gameObject);
		return true;
	}

	// Token: 0x06006EA1 RID: 28321 RVA: 0x0023ACB1 File Offset: 0x00238EB1
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		return this.InitSpell() && base.AttachPowerTaskList(taskList) && this.m_oneSideSpell.AttachPowerTaskList(taskList);
	}

	// Token: 0x06006EA2 RID: 28322 RVA: 0x0023ACD4 File Offset: 0x00238ED4
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.m_oneSideSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnOneSideSpellFinished));
		this.m_oneSideSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnOneSideSpellStateFinished));
		this.m_oneSideSpell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x06006EA3 RID: 28323 RVA: 0x0023AD22 File Offset: 0x00238F22
	private void OnOneSideSpellFinished(Spell spell, object userData)
	{
		this.OnSpellFinished();
	}

	// Token: 0x06006EA4 RID: 28324 RVA: 0x0023AD2A File Offset: 0x00238F2A
	private void OnOneSideSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		UnityEngine.Object.Destroy(this.m_oneSideSpell.gameObject);
		this.m_oneSideSpell = null;
		base.Deactivate();
	}

	// Token: 0x040058C1 RID: 22721
	public Spell m_FriendlySideSpell;

	// Token: 0x040058C2 RID: 22722
	public Spell m_OpponentSideSpell;

	// Token: 0x040058C3 RID: 22723
	private Spell m_oneSideSpell;
}
