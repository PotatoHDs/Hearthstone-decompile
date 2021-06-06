using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4B RID: 3915
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control the state of the Pegasus Highlight system")]
	public class HighlightStateAction : FsmStateAction
	{
		// Token: 0x0600ACB1 RID: 44209 RVA: 0x0035E0F0 File Offset: 0x0035C2F0
		public override void Reset()
		{
			this.m_gameObj = null;
			this.m_state = ActorStateType.HIGHLIGHT_OFF;
		}

		// Token: 0x0600ACB2 RID: 44210 RVA: 0x0035E104 File Offset: 0x0035C304
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_gameObj);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			HighlightState[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<HighlightState>();
			if (componentsInChildren == null)
			{
				base.Finish();
				return;
			}
			HighlightState[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ChangeState(this.m_state);
			}
			base.Finish();
		}

		// Token: 0x04009385 RID: 37765
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		// Token: 0x04009386 RID: 37766
		[RequiredField]
		[Tooltip("State to send")]
		public ActorStateType m_state = ActorStateType.HIGHLIGHT_OFF;

		// Token: 0x04009387 RID: 37767
		private DelayedEvent delayedEvent;
	}
}
