using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE0 RID: 3808
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the position, rotation and weights of an IK goal. A GameObject can be set to use for the position and rotation")]
	public class GetAnimatorIKGoal : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAD6 RID: 43734 RVA: 0x00356E97 File Offset: 0x00355097
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.iKGoal = null;
			this.goal = null;
			this.position = null;
			this.rotation = null;
			this.positionWeight = null;
			this.rotationWeight = null;
		}

		// Token: 0x0600AAD7 RID: 43735 RVA: 0x00356ED0 File Offset: 0x003550D0
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
			GameObject value = this.goal.Value;
			if (value != null)
			{
				this._transform = value.transform;
			}
		}

		// Token: 0x0600AAD8 RID: 43736 RVA: 0x00356F41 File Offset: 0x00355141
		public override void OnActionUpdate()
		{
			this.DoGetIKGoal();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAD9 RID: 43737 RVA: 0x00356F58 File Offset: 0x00355158
		private void DoGetIKGoal()
		{
			if (this._animator == null)
			{
				return;
			}
			this._iKGoal = (AvatarIKGoal)this.iKGoal.Value;
			if (this._transform != null)
			{
				this._transform.position = this._animator.GetIKPosition(this._iKGoal);
				this._transform.rotation = this._animator.GetIKRotation(this._iKGoal);
			}
			if (!this.position.IsNone)
			{
				this.position.Value = this._animator.GetIKPosition(this._iKGoal);
			}
			if (!this.rotation.IsNone)
			{
				this.rotation.Value = this._animator.GetIKRotation(this._iKGoal);
			}
			if (!this.positionWeight.IsNone)
			{
				this.positionWeight.Value = this._animator.GetIKPositionWeight(this._iKGoal);
			}
			if (!this.rotationWeight.IsNone)
			{
				this.rotationWeight.Value = this._animator.GetIKRotationWeight(this._iKGoal);
			}
		}

		// Token: 0x0400918E RID: 37262
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400918F RID: 37263
		[Tooltip("The IK goal")]
		[ObjectType(typeof(AvatarIKGoal))]
		public FsmEnum iKGoal;

		// Token: 0x04009190 RID: 37264
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The gameObject to apply ik goal position and rotation to")]
		public FsmGameObject goal;

		// Token: 0x04009191 RID: 37265
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The position of the ik goal. If Goal GameObject define, position is used as an offset from Goal")]
		public FsmVector3 position;

		// Token: 0x04009192 RID: 37266
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The rotation of the ik goal.If Goal GameObject define, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;

		// Token: 0x04009193 RID: 37267
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		public FsmFloat positionWeight;

		// Token: 0x04009194 RID: 37268
		[UIHint(UIHint.Variable)]
		[Tooltip("Gets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		public FsmFloat rotationWeight;

		// Token: 0x04009195 RID: 37269
		private Animator _animator;

		// Token: 0x04009196 RID: 37270
		private Transform _transform;

		// Token: 0x04009197 RID: 37271
		private AvatarIKGoal _iKGoal;
	}
}
