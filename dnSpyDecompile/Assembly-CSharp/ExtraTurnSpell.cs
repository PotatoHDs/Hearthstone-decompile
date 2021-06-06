using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007EF RID: 2031
public class ExtraTurnSpell : Spell
{
	// Token: 0x06006EC1 RID: 28353 RVA: 0x0023B541 File Offset: 0x00239741
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.SpellEffect(prevStateType));
		base.OnAction(prevStateType);
	}

	// Token: 0x06006EC2 RID: 28354 RVA: 0x0023B558 File Offset: 0x00239758
	private IEnumerator SpellEffect(SpellStateType prevStateType)
	{
		Entity sourceEntity = this.m_taskList.GetSourceEntity(true);
		if (sourceEntity != null)
		{
			Player controller = sourceEntity.GetController();
			if (controller != null && controller.GetSide() == Player.Side.FRIENDLY)
			{
				EndTurnButton endButton = EndTurnButton.Get();
				if (endButton == null)
				{
					yield break;
				}
				endButton.AddInputBlocker();
				yield return new WaitForSeconds(this.m_WaitAnim);
				Animation anim = endButton.m_EndTurnButtonMesh.gameObject.GetComponent<Animation>();
				float length = anim.GetClip(this.m_AnimName).length;
				anim.Play(this.m_AnimName);
				List<PowerTask> taskList = this.m_taskList.GetTaskList();
				for (int i = 0; i < taskList.Count; i++)
				{
					Network.HistTagChange histTagChange = taskList[i].GetPower() as Network.HistTagChange;
					if (histTagChange != null && histTagChange.Tag == 272)
					{
						this.m_taskList.DoTasks(0, i + 1, null);
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
				endButton = null;
				anim = null;
			}
		}
		yield break;
	}

	// Token: 0x040058E1 RID: 22753
	public float m_WaitAnim = 4f;

	// Token: 0x040058E2 RID: 22754
	public string m_TurnText = "GAMEPLAY_NEXT_TURN";

	// Token: 0x040058E3 RID: 22755
	public string m_AnimName = "ENDTURN_NEXT_TURN";

	// Token: 0x040058E4 RID: 22756
	public bool m_DoTimeScale = true;
}
