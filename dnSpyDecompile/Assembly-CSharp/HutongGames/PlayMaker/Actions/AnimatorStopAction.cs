using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F14 RID: 3860
	[ActionCategory("Pegasus")]
	[Tooltip("Disables an Animator.")]
	public class AnimatorStopAction : FsmStateAction
	{
		// Token: 0x0600ABD0 RID: 43984 RVA: 0x0035A17C File Offset: 0x0035837C
		public override void Reset()
		{
			this.m_GameObject = null;
		}

		// Token: 0x0600ABD1 RID: 43985 RVA: 0x0035A188 File Offset: 0x00358388
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (!ownerDefaultTarget)
			{
				base.Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component)
			{
				component.enabled = false;
			}
			base.Finish();
		}

		// Token: 0x0400928A RID: 37514
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;
	}
}
