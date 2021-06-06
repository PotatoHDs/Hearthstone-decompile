using System;
using System.Collections;

// Token: 0x020006D7 RID: 1751
public class ActorAttackSpell : Spell
{
	// Token: 0x060061F5 RID: 25077 RVA: 0x001FFC12 File Offset: 0x001FDE12
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x060061F6 RID: 25078 RVA: 0x001FFC1A File Offset: 0x001FDE1A
	protected override void OnBirth(SpellStateType prevStateType)
	{
		this.m_waitingToAct = true;
		base.OnBirth(prevStateType);
	}

	// Token: 0x060061F7 RID: 25079 RVA: 0x001FFC2A File Offset: 0x001FDE2A
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.WaitThenDoAction(prevStateType));
	}

	// Token: 0x060061F8 RID: 25080 RVA: 0x001FFC3A File Offset: 0x001FDE3A
	private void StopWaitingToAct()
	{
		this.m_waitingToAct = false;
	}

	// Token: 0x060061F9 RID: 25081 RVA: 0x001FFC43 File Offset: 0x001FDE43
	protected IEnumerator WaitThenDoAction(SpellStateType prevStateType)
	{
		while (this.m_waitingToAct)
		{
			yield return null;
		}
		base.OnAction(prevStateType);
		yield break;
	}

	// Token: 0x04005181 RID: 20865
	private bool m_waitingToAct = true;
}
