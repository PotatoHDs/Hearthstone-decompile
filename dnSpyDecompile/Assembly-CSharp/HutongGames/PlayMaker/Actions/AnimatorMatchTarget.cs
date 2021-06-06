using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECA RID: 3786
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress")]
	public class AnimatorMatchTarget : FsmStateAction
	{
		// Token: 0x0600AA74 RID: 43636 RVA: 0x003559B4 File Offset: 0x00353BB4
		public override void Reset()
		{
			this.gameObject = null;
			this.bodyPart = AvatarTarget.Root;
			this.target = null;
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.targetRotation = new FsmQuaternion
			{
				UseVariable = true
			};
			this.positionWeight = Vector3.one;
			this.rotationWeight = 0f;
			this.startNormalizedTime = null;
			this.targetNormalizedTime = null;
			this.everyFrame = true;
		}

		// Token: 0x0600AA75 RID: 43637 RVA: 0x00355A30 File Offset: 0x00353C30
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
			GameObject value = this.target.Value;
			if (value != null)
			{
				this._transform = value.transform;
			}
			this.DoMatchTarget();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA76 RID: 43638 RVA: 0x00355AB5 File Offset: 0x00353CB5
		public override void OnUpdate()
		{
			this.DoMatchTarget();
		}

		// Token: 0x0600AA77 RID: 43639 RVA: 0x00355AC0 File Offset: 0x00353CC0
		private void DoMatchTarget()
		{
			if (this._animator == null)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			Quaternion quaternion = Quaternion.identity;
			if (this._transform != null)
			{
				vector = this._transform.position;
				quaternion = this._transform.rotation;
			}
			if (!this.targetPosition.IsNone)
			{
				vector += this.targetPosition.Value;
			}
			if (!this.targetRotation.IsNone)
			{
				quaternion *= this.targetRotation.Value;
			}
			MatchTargetWeightMask weightMask = new MatchTargetWeightMask(this.positionWeight.Value, this.rotationWeight.Value);
			this._animator.MatchTarget(vector, quaternion, this.bodyPart, weightMask, this.startNormalizedTime.Value, this.targetNormalizedTime.Value);
		}

		// Token: 0x0400911A RID: 37146
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400911B RID: 37147
		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget bodyPart;

		// Token: 0x0400911C RID: 37148
		[Tooltip("The gameObject target to match")]
		public FsmGameObject target;

		// Token: 0x0400911D RID: 37149
		[Tooltip("The position of the ik goal. If Goal GameObject set, position is used as an offset from Goal")]
		public FsmVector3 targetPosition;

		// Token: 0x0400911E RID: 37150
		[Tooltip("The rotation of the ik goal.If Goal GameObject set, rotation is used as an offset from Goal")]
		public FsmQuaternion targetRotation;

		// Token: 0x0400911F RID: 37151
		[Tooltip("The MatchTargetWeightMask Position XYZ weight")]
		public FsmVector3 positionWeight;

		// Token: 0x04009120 RID: 37152
		[Tooltip("The MatchTargetWeightMask Rotation weight")]
		public FsmFloat rotationWeight;

		// Token: 0x04009121 RID: 37153
		[Tooltip("Start time within the animation clip (0 - beginning of clip, 1 - end of clip)")]
		public FsmFloat startNormalizedTime;

		// Token: 0x04009122 RID: 37154
		[Tooltip("End time within the animation clip (0 - beginning of clip, 1 - end of clip), values greater than 1 can be set to trigger a match after a certain number of loops. Ex: 2.3 means at 30% of 2nd loop")]
		public FsmFloat targetNormalizedTime;

		// Token: 0x04009123 RID: 37155
		[Tooltip("Should always be true")]
		public bool everyFrame;

		// Token: 0x04009124 RID: 37156
		private Animator _animator;

		// Token: 0x04009125 RID: 37157
		private Transform _transform;
	}
}
