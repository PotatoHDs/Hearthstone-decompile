using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4C RID: 3916
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control the state of the Pegasus Highlight system")]
	public class HighlightUpdateAction : FsmStateAction
	{
		// Token: 0x0600ACB4 RID: 44212 RVA: 0x0035E179 File Offset: 0x0035C379
		public override void Reset()
		{
			this.m_gameObj = null;
		}

		// Token: 0x0600ACB5 RID: 44213 RVA: 0x0035E184 File Offset: 0x0035C384
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
				array[i].ForceUpdate();
			}
			base.Finish();
		}

		// Token: 0x04009388 RID: 37768
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		// Token: 0x04009389 RID: 37769
		private DelayedEvent delayedEvent;
	}
}
