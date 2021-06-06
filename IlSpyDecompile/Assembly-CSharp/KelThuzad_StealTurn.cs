using System.Collections;
using UnityEngine;

public class KelThuzad_StealTurn : Spell
{
	public GameObject m_Lightning;

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(SpellEffect(prevStateType));
		base.OnAction(prevStateType);
	}

	private IEnumerator SpellEffect(SpellStateType prevStateType)
	{
		yield return new WaitForSeconds(0.25f);
		if (TurnTimer.Get() != null)
		{
			TurnTimer.Get().OnEndTurnRequested();
		}
		EndTurnButton.Get().m_EndTurnButtonMesh.GetComponent<Animation>()["ENDTURN_YOUR_TIMER_DONE"].speed = 0.7f;
		EndTurnButton.Get().OnTurnTimerEnded(isFriendlyPlayerTurnTimer: true);
		yield return new WaitForSeconds(1f);
		EndTurnButton.Get().m_EndTurnButtonMesh.GetComponent<Animation>()["ENDTURN_YOUR_TIMER_DONE"].speed = 1f;
	}
}
