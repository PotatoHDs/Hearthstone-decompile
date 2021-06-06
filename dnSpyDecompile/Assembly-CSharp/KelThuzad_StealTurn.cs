using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007FF RID: 2047
public class KelThuzad_StealTurn : Spell
{
	// Token: 0x06006F27 RID: 28455 RVA: 0x0023CACD File Offset: 0x0023ACCD
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.SpellEffect(prevStateType));
		base.OnAction(prevStateType);
	}

	// Token: 0x06006F28 RID: 28456 RVA: 0x0023CAE4 File Offset: 0x0023ACE4
	private IEnumerator SpellEffect(SpellStateType prevStateType)
	{
		yield return new WaitForSeconds(0.25f);
		if (TurnTimer.Get() != null)
		{
			TurnTimer.Get().OnEndTurnRequested();
		}
		EndTurnButton.Get().m_EndTurnButtonMesh.GetComponent<Animation>()["ENDTURN_YOUR_TIMER_DONE"].speed = 0.7f;
		EndTurnButton.Get().OnTurnTimerEnded(true);
		yield return new WaitForSeconds(1f);
		EndTurnButton.Get().m_EndTurnButtonMesh.GetComponent<Animation>()["ENDTURN_YOUR_TIMER_DONE"].speed = 1f;
		yield break;
	}

	// Token: 0x04005925 RID: 22821
	public GameObject m_Lightning;
}
