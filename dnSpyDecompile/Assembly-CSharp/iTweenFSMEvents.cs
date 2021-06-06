using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

// Token: 0x02000753 RID: 1875
public class iTweenFSMEvents : MonoBehaviour
{
	// Token: 0x060069B9 RID: 27065 RVA: 0x00226F4D File Offset: 0x0022514D
	private void iTweenOnStart(int aniTweenID)
	{
		if (this.itweenID == aniTweenID && this.itweenFSMAction != null && this.itweenFSMAction.Fsm != null)
		{
			this.itweenFSMAction.Fsm.Event(this.itweenFSMAction.startEvent);
		}
	}

	// Token: 0x060069BA RID: 27066 RVA: 0x00226F88 File Offset: 0x00225188
	private void iTweenOnComplete(int aniTweenID)
	{
		if (this.itweenID == aniTweenID && this.itweenFSMAction != null && this.itweenFSMAction.Fsm != null)
		{
			if (this.islooping)
			{
				if (!this.donotfinish)
				{
					this.itweenFSMAction.Fsm.Event(this.itweenFSMAction.finishEvent);
					this.itweenFSMAction.Finish();
					return;
				}
			}
			else
			{
				this.itweenFSMAction.Fsm.Event(this.itweenFSMAction.finishEvent);
				this.itweenFSMAction.Finish();
			}
		}
	}

	// Token: 0x04005673 RID: 22131
	public static int itweenIDCount;

	// Token: 0x04005674 RID: 22132
	public int itweenID;

	// Token: 0x04005675 RID: 22133
	public iTweenFsmAction itweenFSMAction;

	// Token: 0x04005676 RID: 22134
	public bool donotfinish;

	// Token: 0x04005677 RID: 22135
	public bool islooping;

	// Token: 0x04005678 RID: 22136
	public string targetGameObjectName;
}
