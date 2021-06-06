using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE1 RID: 3809
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of an int parameter")]
	public class GetAnimatorInt : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AADB RID: 43739 RVA: 0x00357074 File Offset: 0x00355274
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.result = null;
		}

		// Token: 0x0600AADC RID: 43740 RVA: 0x00357094 File Offset: 0x00355294
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
			this.GetParameter();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AADD RID: 43741 RVA: 0x0035710E File Offset: 0x0035530E
		public override void OnActionUpdate()
		{
			this.GetParameter();
		}

		// Token: 0x0600AADE RID: 43742 RVA: 0x00357116 File Offset: 0x00355316
		private void GetParameter()
		{
			if (this._animator != null)
			{
				this.result.Value = this._animator.GetInteger(this._paramID);
			}
		}

		// Token: 0x04009198 RID: 37272
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009199 RID: 37273
		[RequiredField]
		[UIHint(UIHint.AnimatorInt)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x0400919A RID: 37274
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The int value of the animator parameter")]
		public FsmInt result;

		// Token: 0x0400919B RID: 37275
		private Animator _animator;

		// Token: 0x0400919C RID: 37276
		private int _paramID;
	}
}
