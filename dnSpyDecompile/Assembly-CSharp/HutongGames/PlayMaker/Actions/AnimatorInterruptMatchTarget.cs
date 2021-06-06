using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC9 RID: 3785
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Interrupts the automatic target matching. CompleteMatch will make the gameobject match the target completely at the next frame.")]
	public class AnimatorInterruptMatchTarget : FsmStateAction
	{
		// Token: 0x0600AA71 RID: 43633 RVA: 0x00355946 File Offset: 0x00353B46
		public override void Reset()
		{
			this.gameObject = null;
			this.completeMatch = true;
		}

		// Token: 0x0600AA72 RID: 43634 RVA: 0x0035595C File Offset: 0x00353B5C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component != null)
			{
				component.InterruptMatchTarget(this.completeMatch.Value);
			}
			base.Finish();
		}

		// Token: 0x04009118 RID: 37144
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009119 RID: 37145
		[Tooltip("Will make the gameobject match the target completely at the next frame")]
		public FsmBool completeMatch;
	}
}
