using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF2 RID: 3826
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the position and rotation of the target specified by SetTarget(AvatarTarget targetIndex, float targetNormalizedTime)).\nThe position and rotation are only valid when a frame has being evaluated after the SetTarget call")]
	public class GetAnimatorTarget : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB2D RID: 43821 RVA: 0x00357FAE File Offset: 0x003561AE
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.targetPosition = null;
			this.targetRotation = null;
			this.targetGameObject = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB2E RID: 43822 RVA: 0x00357FDC File Offset: 0x003561DC
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
			GameObject value = this.targetGameObject.Value;
			if (value != null)
			{
				this._transform = value.transform;
			}
			this.DoGetTarget();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB2F RID: 43823 RVA: 0x00358061 File Offset: 0x00356261
		public override void OnActionUpdate()
		{
			this.DoGetTarget();
		}

		// Token: 0x0600AB30 RID: 43824 RVA: 0x0035806C File Offset: 0x0035626C
		private void DoGetTarget()
		{
			if (this._animator == null)
			{
				return;
			}
			this.targetPosition.Value = this._animator.targetPosition;
			this.targetRotation.Value = this._animator.targetRotation;
			if (this._transform != null)
			{
				this._transform.position = this._animator.targetPosition;
				this._transform.rotation = this._animator.targetRotation;
			}
		}

		// Token: 0x040091ED RID: 37357
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091EE RID: 37358
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The target position")]
		public FsmVector3 targetPosition;

		// Token: 0x040091EF RID: 37359
		[UIHint(UIHint.Variable)]
		[Tooltip("The target rotation")]
		public FsmQuaternion targetRotation;

		// Token: 0x040091F0 RID: 37360
		[Tooltip("If set, apply the position and rotation to this gameObject")]
		public FsmGameObject targetGameObject;

		// Token: 0x040091F1 RID: 37361
		private Animator _animator;

		// Token: 0x040091F2 RID: 37362
		private Transform _transform;
	}
}
