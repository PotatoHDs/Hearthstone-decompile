using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF0 RID: 3824
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the avatar body mass center position and rotation.Optionally accept a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	public class GetAnimatorRoot : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB23 RID: 43811 RVA: 0x00357DC6 File Offset: 0x00355FC6
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.rootPosition = null;
			this.rootRotation = null;
			this.bodyGameObject = null;
		}

		// Token: 0x0600AB24 RID: 43812 RVA: 0x00357DEC File Offset: 0x00355FEC
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
			this.DoGetBodyPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB25 RID: 43813 RVA: 0x00357E71 File Offset: 0x00356071
		public override void OnActionUpdate()
		{
			this.DoGetBodyPosition();
		}

		// Token: 0x0600AB26 RID: 43814 RVA: 0x00357E7C File Offset: 0x0035607C
		private void DoGetBodyPosition()
		{
			if (this._animator == null)
			{
				return;
			}
			this.rootPosition.Value = this._animator.rootPosition;
			this.rootRotation.Value = this._animator.rootRotation;
			if (this._transform != null)
			{
				this._transform.position = this._animator.rootPosition;
				this._transform.rotation = this._animator.rootRotation;
			}
		}

		// Token: 0x040091E4 RID: 37348
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091E5 RID: 37349
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 rootPosition;

		// Token: 0x040091E6 RID: 37350
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion rootRotation;

		// Token: 0x040091E7 RID: 37351
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;

		// Token: 0x040091E8 RID: 37352
		private Animator _animator;

		// Token: 0x040091E9 RID: 37353
		private Transform _transform;
	}
}
