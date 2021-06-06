using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDB RID: 3803
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar delta position and rotation for the last evaluated frame.")]
	public class GetAnimatorDelta : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AABF RID: 43711 RVA: 0x00356B30 File Offset: 0x00354D30
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.deltaPosition = null;
			this.deltaRotation = null;
		}

		// Token: 0x0600AAC0 RID: 43712 RVA: 0x00356B50 File Offset: 0x00354D50
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
			this.DoGetDeltaPosition();
			base.Finish();
		}

		// Token: 0x0600AAC1 RID: 43713 RVA: 0x00356BAC File Offset: 0x00354DAC
		public override void OnActionUpdate()
		{
			this.DoGetDeltaPosition();
		}

		// Token: 0x0600AAC2 RID: 43714 RVA: 0x00356BB4 File Offset: 0x00354DB4
		private void DoGetDeltaPosition()
		{
			if (this._animator == null)
			{
				return;
			}
			this.deltaPosition.Value = this._animator.deltaPosition;
			this.deltaRotation.Value = this._animator.deltaRotation;
		}

		// Token: 0x0400917C RID: 37244
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400917D RID: 37245
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmVector3 deltaPosition;

		// Token: 0x0400917E RID: 37246
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar delta position for the last evaluated frame")]
		public FsmQuaternion deltaRotation;

		// Token: 0x0400917F RID: 37247
		private Animator _animator;
	}
}
