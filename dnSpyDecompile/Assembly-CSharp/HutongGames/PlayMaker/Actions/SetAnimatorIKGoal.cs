using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFB RID: 3835
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the position, rotation and weights of an IK goal. A GameObject can be set to control the position and rotation, or it can be manually expressed.")]
	public class SetAnimatorIKGoal : FsmStateAction
	{
		// Token: 0x0600AB57 RID: 43863 RVA: 0x003587D4 File Offset: 0x003569D4
		public override void Reset()
		{
			this.gameObject = null;
			this.goal = null;
			this.position = new FsmVector3
			{
				UseVariable = true
			};
			this.rotation = new FsmQuaternion
			{
				UseVariable = true
			};
			this.positionWeight = 1f;
			this.rotationWeight = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600AB58 RID: 43864 RVA: 0x0035830A File Offset: 0x0035650A
		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorIK = true;
		}

		// Token: 0x0600AB59 RID: 43865 RVA: 0x0035883C File Offset: 0x00356A3C
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

		// Token: 0x0600AB5A RID: 43866 RVA: 0x003588AD File Offset: 0x00356AAD
		public override void DoAnimatorIK(int layerIndex)
		{
			this.DoSetIKGoal();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB5B RID: 43867 RVA: 0x003588C4 File Offset: 0x00356AC4
		private void DoSetIKGoal()
		{
			if (this._animator == null)
			{
				return;
			}
			if (this._transform != null)
			{
				if (this.position.IsNone)
				{
					this._animator.SetIKPosition(this.iKGoal, this._transform.position);
				}
				else
				{
					this._animator.SetIKPosition(this.iKGoal, this._transform.position + this.position.Value);
				}
				if (this.rotation.IsNone)
				{
					this._animator.SetIKRotation(this.iKGoal, this._transform.rotation);
				}
				else
				{
					this._animator.SetIKRotation(this.iKGoal, this._transform.rotation * this.rotation.Value);
				}
			}
			else
			{
				if (!this.position.IsNone)
				{
					this._animator.SetIKPosition(this.iKGoal, this.position.Value);
				}
				if (!this.rotation.IsNone)
				{
					this._animator.SetIKRotation(this.iKGoal, this.rotation.Value);
				}
			}
			if (!this.positionWeight.IsNone)
			{
				this._animator.SetIKPositionWeight(this.iKGoal, this.positionWeight.Value);
			}
			if (!this.rotationWeight.IsNone)
			{
				this._animator.SetIKRotationWeight(this.iKGoal, this.rotationWeight.Value);
			}
		}

		// Token: 0x04009215 RID: 37397
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009216 RID: 37398
		[Tooltip("The IK goal")]
		public AvatarIKGoal iKGoal;

		// Token: 0x04009217 RID: 37399
		[Tooltip("The gameObject target of the ik goal")]
		public FsmGameObject goal;

		// Token: 0x04009218 RID: 37400
		[Tooltip("The position of the ik goal. If Goal GameObject set, position is used as an offset from Goal")]
		public FsmVector3 position;

		// Token: 0x04009219 RID: 37401
		[Tooltip("The rotation of the ik goal.If Goal GameObject set, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;

		// Token: 0x0400921A RID: 37402
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		public FsmFloat positionWeight;

		// Token: 0x0400921B RID: 37403
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Sets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		public FsmFloat rotationWeight;

		// Token: 0x0400921C RID: 37404
		[Tooltip("Repeat every frame. Useful when changing over time.")]
		public bool everyFrame;

		// Token: 0x0400921D RID: 37405
		private Animator _animator;

		// Token: 0x0400921E RID: 37406
		private Transform _transform;
	}
}
