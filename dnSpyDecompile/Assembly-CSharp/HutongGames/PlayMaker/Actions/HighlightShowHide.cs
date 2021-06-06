using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4A RID: 3914
	[ActionCategory("Pegasus")]
	[Tooltip("Used to Show and Hide card Highlights")]
	public class HighlightShowHide : FsmStateAction
	{
		// Token: 0x0600ACAE RID: 44206 RVA: 0x0035E04C File Offset: 0x0035C24C
		public override void Reset()
		{
			this.m_gameObj = null;
			this.m_Show = true;
		}

		// Token: 0x0600ACAF RID: 44207 RVA: 0x0035E064 File Offset: 0x0035C264
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
			foreach (HighlightState highlightState in componentsInChildren)
			{
				if (this.m_Show.Value)
				{
					highlightState.Show();
				}
				else
				{
					highlightState.Hide();
				}
			}
			base.Finish();
		}

		// Token: 0x04009382 RID: 37762
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		// Token: 0x04009383 RID: 37763
		[RequiredField]
		[Tooltip("Show or Hide")]
		public FsmBool m_Show = true;

		// Token: 0x04009384 RID: 37764
		private DelayedEvent delayedEvent;
	}
}
