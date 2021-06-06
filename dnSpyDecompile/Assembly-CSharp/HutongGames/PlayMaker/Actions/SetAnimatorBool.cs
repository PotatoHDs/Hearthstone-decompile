using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF7 RID: 3831
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the value of a bool parameter")]
	public class SetAnimatorBool : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB45 RID: 43845 RVA: 0x003584A9 File Offset: 0x003566A9
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.Value = null;
		}

		// Token: 0x0600AB46 RID: 43846 RVA: 0x003584C8 File Offset: 0x003566C8
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

		// Token: 0x0600AB47 RID: 43847 RVA: 0x00358542 File Offset: 0x00356742
		public override void OnActionUpdate()
		{
			this.SetParameter();
		}

		// Token: 0x0600AB48 RID: 43848 RVA: 0x0035854A File Offset: 0x0035674A
		private void SetParameter()
		{
			if (this._animator != null)
			{
				this._animator.SetBool(this._paramID, this.Value.Value);
			}
		}

		// Token: 0x04009204 RID: 37380
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009205 RID: 37381
		[RequiredField]
		[UIHint(UIHint.AnimatorBool)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009206 RID: 37382
		[Tooltip("The Bool value to assign to the animator parameter")]
		public FsmBool Value;

		// Token: 0x04009207 RID: 37383
		private Animator _animator;

		// Token: 0x04009208 RID: 37384
		private int _paramID;
	}
}
