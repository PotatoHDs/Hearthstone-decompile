using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFC RID: 3836
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the value of a int parameter")]
	public class SetAnimatorInt : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB5D RID: 43869 RVA: 0x00358A48 File Offset: 0x00356C48
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.Value = null;
		}

		// Token: 0x0600AB5E RID: 43870 RVA: 0x00358A68 File Offset: 0x00356C68
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
			this._paramID = Animator.StringToHash(this.parameter.Value);
			this.SetParameter();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB5F RID: 43871 RVA: 0x00358AE2 File Offset: 0x00356CE2
		public override void OnActionUpdate()
		{
			this.SetParameter();
		}

		// Token: 0x0600AB60 RID: 43872 RVA: 0x00358AEA File Offset: 0x00356CEA
		private void SetParameter()
		{
			if (this._animator != null)
			{
				this._animator.SetInteger(this._paramID, this.Value.Value);
			}
		}

		// Token: 0x0400921F RID: 37407
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009220 RID: 37408
		[RequiredField]
		[UIHint(UIHint.AnimatorInt)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009221 RID: 37409
		[Tooltip("The Int value to assign to the animator parameter")]
		public FsmInt Value;

		// Token: 0x04009222 RID: 37410
		private Animator _animator;

		// Token: 0x04009223 RID: 37411
		private int _paramID;
	}
}
