using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF5 RID: 3829
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Set Apply Root Motion: If true, Root is controlled by animations")]
	public class SetAnimatorApplyRootMotion : FsmStateAction
	{
		// Token: 0x0600AB3B RID: 43835 RVA: 0x0035823A File Offset: 0x0035643A
		public override void Reset()
		{
			this.gameObject = null;
			this.applyRootMotion = null;
		}

		// Token: 0x0600AB3C RID: 43836 RVA: 0x0035824C File Offset: 0x0035644C
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
			this.DoApplyRootMotion();
			base.Finish();
		}

		// Token: 0x0600AB3D RID: 43837 RVA: 0x003582A8 File Offset: 0x003564A8
		private void DoApplyRootMotion()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.applyRootMotion = this.applyRootMotion.Value;
		}

		// Token: 0x040091FA RID: 37370
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091FB RID: 37371
		[Tooltip("If true, Root is controlled by animations")]
		public FsmBool applyRootMotion;

		// Token: 0x040091FC RID: 37372
		private Animator _animator;
	}
}
