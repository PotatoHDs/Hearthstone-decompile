using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDE RID: 3806
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns The current gravity weight based on current animations that are played")]
	public class GetAnimatorGravityWeight : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AACD RID: 43725 RVA: 0x00356D52 File Offset: 0x00354F52
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.gravityWeight = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AACE RID: 43726 RVA: 0x00356D70 File Offset: 0x00354F70
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
			this.DoGetGravityWeight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AACF RID: 43727 RVA: 0x00356DD4 File Offset: 0x00354FD4
		public override void OnActionUpdate()
		{
			this.DoGetGravityWeight();
		}

		// Token: 0x0600AAD0 RID: 43728 RVA: 0x00356DDC File Offset: 0x00354FDC
		private void DoGetGravityWeight()
		{
			if (this._animator == null)
			{
				return;
			}
			this.gravityWeight.Value = this._animator.gravityWeight;
		}

		// Token: 0x04009188 RID: 37256
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009189 RID: 37257
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The current gravity weight based on current animations that are played")]
		public FsmFloat gravityWeight;

		// Token: 0x0400918A RID: 37258
		private Animator _animator;
	}
}
