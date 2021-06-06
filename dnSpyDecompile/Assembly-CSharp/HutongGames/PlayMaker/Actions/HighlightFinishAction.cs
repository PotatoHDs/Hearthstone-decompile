using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F49 RID: 3913
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the Highlight system when the state is finished.")]
	public class HighlightFinishAction : FsmStateAction
	{
		// Token: 0x0600ACAA RID: 44202 RVA: 0x0035DFA4 File Offset: 0x0035C1A4
		public HighlightState CacheHighlightState()
		{
			if (this.m_HighlightState == null)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
				if (ownerDefaultTarget != null)
				{
					this.m_HighlightState = SceneUtils.FindComponentInThisOrParents<HighlightState>(ownerDefaultTarget);
				}
			}
			return this.m_HighlightState;
		}

		// Token: 0x0600ACAB RID: 44203 RVA: 0x0035DFEC File Offset: 0x0035C1EC
		public override void Reset()
		{
			this.m_GameObject = null;
		}

		// Token: 0x0600ACAC RID: 44204 RVA: 0x0035DFF8 File Offset: 0x0035C1F8
		public override void OnEnter()
		{
			this.CacheHighlightState();
			if (this.m_HighlightState == null)
			{
				Debug.LogError(string.Format("{0}.OnEnter() - FAILED to find {1} in hierarchy", this, typeof(HighlightState)));
				base.Finish();
				return;
			}
			this.m_HighlightState.OnActionFinished();
			base.Finish();
		}

		// Token: 0x04009380 RID: 37760
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009381 RID: 37761
		protected HighlightState m_HighlightState;
	}
}
