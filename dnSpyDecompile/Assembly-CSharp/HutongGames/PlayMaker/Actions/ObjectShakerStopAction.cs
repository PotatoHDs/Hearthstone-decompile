using System;
using Hearthstone.FX;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F59 RID: 3929
	[ActionCategory("Pegasus")]
	[Tooltip("Cancels an ObjectShakerAction.")]
	public class ObjectShakerStopAction : FsmStateAction
	{
		// Token: 0x0600ACF3 RID: 44275 RVA: 0x0035F63F File Offset: 0x0035D83F
		public override void Reset()
		{
			this.m_GameObject = null;
		}

		// Token: 0x0600ACF4 RID: 44276 RVA: 0x0035F648 File Offset: 0x0035D848
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget != null)
			{
				ObjectShaker.Cancel(ownerDefaultTarget, false);
			}
			base.Finish();
		}

		// Token: 0x040093D7 RID: 37847
		[RequiredField]
		public FsmOwnerDefault m_GameObject;
	}
}
