using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F03 RID: 3843
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("If true, automatically stabilize feet during transition and blending")]
	public class SetAnimatorStabilizeFeet : FsmStateAction
	{
		// Token: 0x0600AB80 RID: 43904 RVA: 0x00359137 File Offset: 0x00357337
		public override void Reset()
		{
			this.gameObject = null;
			this.stabilizeFeet = null;
		}

		// Token: 0x0600AB81 RID: 43905 RVA: 0x00359148 File Offset: 0x00357348
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator == null)
			{
				base.Finish();
				return;
			}
			this.DoStabilizeFeet();
			base.Finish();
		}

		// Token: 0x0600AB82 RID: 43906 RVA: 0x003591A4 File Offset: 0x003573A4
		private void DoStabilizeFeet()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.stabilizeFeet = this.stabilizeFeet.Value;
		}

		// Token: 0x04009243 RID: 37443
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009244 RID: 37444
		[Tooltip("If true, automatically stabilize feet during transition and blending")]
		public FsmBool stabilizeFeet;

		// Token: 0x04009245 RID: 37445
		private Animator _animator;
	}
}
