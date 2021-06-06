using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EEF RID: 3823
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Get the right foot bottom height.")]
	public class GetAnimatorRightFootBottomHeight : FsmStateAction
	{
		// Token: 0x0600AB1D RID: 43805 RVA: 0x00357D16 File Offset: 0x00355F16
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.rightFootHeight = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB1E RID: 43806 RVA: 0x0032C298 File Offset: 0x0032A498
		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		// Token: 0x0600AB1F RID: 43807 RVA: 0x00357D34 File Offset: 0x00355F34
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
			this._getRightFootBottonHeight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB20 RID: 43808 RVA: 0x00357D98 File Offset: 0x00355F98
		public override void OnLateUpdate()
		{
			this._getRightFootBottonHeight();
		}

		// Token: 0x0600AB21 RID: 43809 RVA: 0x00357DA0 File Offset: 0x00355FA0
		private void _getRightFootBottonHeight()
		{
			if (this._animator != null)
			{
				this.rightFootHeight.Value = this._animator.rightFeetBottomHeight;
			}
		}

		// Token: 0x040091E0 RID: 37344
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091E1 RID: 37345
		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The right foot bottom height.")]
		public FsmFloat rightFootHeight;

		// Token: 0x040091E2 RID: 37346
		[Tooltip("Repeat every frame during LateUpdate. Useful when value is subject to change over time.")]
		public bool everyFrame;

		// Token: 0x040091E3 RID: 37347
		private Animator _animator;
	}
}
