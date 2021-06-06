using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFF RID: 3839
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets look at position and weights. A GameObject can be set to control the look at position, or it can be manually expressed.")]
	public class SetAnimatorLookAt : FsmStateAction
	{
		// Token: 0x0600AB6B RID: 43883 RVA: 0x00358C6C File Offset: 0x00356E6C
		public override void Reset()
		{
			this.gameObject = null;
			this.target = null;
			this.targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			this.weight = 1f;
			this.bodyWeight = 0.3f;
			this.headWeight = 0.6f;
			this.eyesWeight = 1f;
			this.clampWeight = 0.5f;
			this.everyFrame = false;
		}

		// Token: 0x0600AB6C RID: 43884 RVA: 0x0035830A File Offset: 0x0035650A
		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorIK = true;
		}

		// Token: 0x0600AB6D RID: 43885 RVA: 0x00358CF0 File Offset: 0x00356EF0
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
		}

		// Token: 0x0600AB6E RID: 43886 RVA: 0x00358D61 File Offset: 0x00356F61
		public override void DoAnimatorIK(int layerIndex)
		{
			this.DoSetLookAt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB6F RID: 43887 RVA: 0x00358D78 File Offset: 0x00356F78
		private void DoSetLookAt()
		{
			if (this._animator == null)
			{
				return;
			}
			if (this._transform != null)
			{
				if (this.targetPosition.IsNone)
				{
					this._animator.SetLookAtPosition(this._transform.position);
				}
				else
				{
					this._animator.SetLookAtPosition(this._transform.position + this.targetPosition.Value);
				}
			}
			else if (!this.targetPosition.IsNone)
			{
				this._animator.SetLookAtPosition(this.targetPosition.Value);
			}
			if (!this.clampWeight.IsNone)
			{
				this._animator.SetLookAtWeight(this.weight.Value, this.bodyWeight.Value, this.headWeight.Value, this.eyesWeight.Value, this.clampWeight.Value);
				return;
			}
			if (!this.eyesWeight.IsNone)
			{
				this._animator.SetLookAtWeight(this.weight.Value, this.bodyWeight.Value, this.headWeight.Value, this.eyesWeight.Value);
				return;
			}
			if (!this.headWeight.IsNone)
			{
				this._animator.SetLookAtWeight(this.weight.Value, this.bodyWeight.Value, this.headWeight.Value);
				return;
			}
			if (!this.bodyWeight.IsNone)
			{
				this._animator.SetLookAtWeight(this.weight.Value, this.bodyWeight.Value);
				return;
			}
			if (!this.weight.IsNone)
			{
				this._animator.SetLookAtWeight(this.weight.Value);
			}
		}

		// Token: 0x0400922C RID: 37420
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400922D RID: 37421
		[Tooltip("The gameObject to look at")]
		public FsmGameObject target;

		// Token: 0x0400922E RID: 37422
		[Tooltip("The look-at position. If Target GameObject set, targetPosition is used as an offset from Target")]
		public FsmVector3 targetPosition;

		// Token: 0x0400922F RID: 37423
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The global weight of the LookAt, multiplier for other parameters. Range from 0 to 1")]
		public FsmFloat weight;

		// Token: 0x04009230 RID: 37424
		[HasFloatSlider(0f, 1f)]
		[Tooltip("determines how much the body is involved in the LookAt. Range from 0 to 1")]
		public FsmFloat bodyWeight;

		// Token: 0x04009231 RID: 37425
		[HasFloatSlider(0f, 1f)]
		[Tooltip("determines how much the head is involved in the LookAt. Range from 0 to 1")]
		public FsmFloat headWeight;

		// Token: 0x04009232 RID: 37426
		[HasFloatSlider(0f, 1f)]
		[Tooltip("determines how much the eyes are involved in the LookAt. Range from 0 to 1")]
		public FsmFloat eyesWeight;

		// Token: 0x04009233 RID: 37427
		[HasFloatSlider(0f, 1f)]
		[Tooltip("0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
		public FsmFloat clampWeight;

		// Token: 0x04009234 RID: 37428
		[Tooltip("Repeat every frame during OnAnimatorIK(). Useful for changing over time.")]
		public bool everyFrame;

		// Token: 0x04009235 RID: 37429
		private Animator _animator;

		// Token: 0x04009236 RID: 37430
		private Transform _transform;
	}
}
