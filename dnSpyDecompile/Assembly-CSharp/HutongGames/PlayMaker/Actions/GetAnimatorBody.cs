using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED1 RID: 3793
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar body mass center position and rotation. Optionally accepts a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	public class GetAnimatorBody : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AA8E RID: 43662 RVA: 0x00355F05 File Offset: 0x00354105
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.bodyPosition = null;
			this.bodyRotation = null;
			this.bodyGameObject = null;
			this.everyFrame = false;
			this.everyFrameOption = FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK;
		}

		// Token: 0x0600AA8F RID: 43663 RVA: 0x00355F38 File Offset: 0x00354138
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
			GameObject value = this.bodyGameObject.Value;
			if (value != null)
			{
				this._transform = value.transform;
			}
			if (this.everyFrameOption != FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				this.everyFrameOption = FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK;
			}
		}

		// Token: 0x0600AA90 RID: 43664 RVA: 0x00355FB9 File Offset: 0x003541B9
		public override void OnActionUpdate()
		{
			this.DoGetBodyPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA91 RID: 43665 RVA: 0x00355FD0 File Offset: 0x003541D0
		private void DoGetBodyPosition()
		{
			if (this._animator == null)
			{
				return;
			}
			this.bodyPosition.Value = this._animator.bodyPosition;
			this.bodyRotation.Value = this._animator.bodyRotation;
			if (this._transform != null)
			{
				this._transform.position = this._animator.bodyPosition;
				this._transform.rotation = this._animator.bodyRotation;
			}
		}

		// Token: 0x0600AA92 RID: 43666 RVA: 0x00356052 File Offset: 0x00354252
		public override string ErrorCheck()
		{
			if (this.everyFrameOption != FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				return "Getting Body Position should only be done in OnAnimatorIK";
			}
			return string.Empty;
		}

		// Token: 0x04009138 RID: 37176
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009139 RID: 37177
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 bodyPosition;

		// Token: 0x0400913A RID: 37178
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion bodyRotation;

		// Token: 0x0400913B RID: 37179
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;

		// Token: 0x0400913C RID: 37180
		private Animator _animator;

		// Token: 0x0400913D RID: 37181
		private Transform _transform;
	}
}
