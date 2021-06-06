using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F48 RID: 3912
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control the state of the Pegasus Highlight system")]
	public class HighlightContinuousUpdateAction : FsmStateAction
	{
		// Token: 0x0600ACA7 RID: 44199 RVA: 0x0035DF05 File Offset: 0x0035C105
		public override void Reset()
		{
			this.m_gameObj = null;
			this.m_updateTime = 1f;
		}

		// Token: 0x0600ACA8 RID: 44200 RVA: 0x0035DF20 File Offset: 0x0035C120
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
				array[i].ContinuousUpdate(this.m_updateTime.Value);
			}
			base.Finish();
		}

		// Token: 0x0400937D RID: 37757
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		// Token: 0x0400937E RID: 37758
		[RequiredField]
		[Tooltip("Amount of time to render")]
		public FsmFloat m_updateTime = 1f;

		// Token: 0x0400937F RID: 37759
		private DelayedEvent delayedEvent;
	}
}
