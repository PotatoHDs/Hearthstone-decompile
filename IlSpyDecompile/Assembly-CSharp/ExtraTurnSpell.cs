using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTurnSpell : Spell
{
	public float m_WaitAnim = 4f;

	public string m_TurnText = "GAMEPLAY_NEXT_TURN";

	public string m_AnimName = "ENDTURN_NEXT_TURN";

	public bool m_DoTimeScale = true;

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(SpellEffect(prevStateType));
		base.OnAction(prevStateType);
	}

	private IEnumerator SpellEffect(SpellStateType prevStateType)
	{
		Entity sourceEntity = m_taskList.GetSourceEntity();
		if (sourceEntity == null)
		{
			yield break;
		}
		Player controller = sourceEntity.GetController();
		if (controller == null || controller.GetSide() != Player.Side.FRIENDLY)
		{
			yield break;
		}
		EndTurnButton endButton = EndTurnButton.Get();
		if (endButton == null)
		{
			yield break;
		}
		endButton.AddInputBlocker();
		yield return new WaitForSeconds(m_WaitAnim);
		Animation anim = endButton.m_EndTurnButtonMesh.gameObject.GetComponent<Animation>();
		float length = anim.GetClip(m_AnimName).length;
		anim.Play(m_AnimName);
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistTagChange histTagChange = taskList[i].GetPower() as Network.HistTagChange;
			if (histTagChange != null && histTagChange.Tag == 272)
			{
				m_taskList.DoTasks(0, i + 1, null);
				break;
			}
		}
		endButton.DisplayExtraTurnState();
		yield return new WaitForSeconds(length);
		if (endButton.IsInWaitingState())
		{
			length = anim.GetClip("ENDTURN_WAITING").length;
			anim.Play("ENDTURN_WAITING");
			yield return new WaitForSeconds(length);
		}
		endButton.RemoveInputBlocker();
		endButton.DisplayExtraTurnState();
	}
}
