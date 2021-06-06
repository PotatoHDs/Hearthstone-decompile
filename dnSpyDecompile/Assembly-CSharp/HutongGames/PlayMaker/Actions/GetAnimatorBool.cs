using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED3 RID: 3795
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of a bool parameter")]
	public class GetAnimatorBool : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AA98 RID: 43672 RVA: 0x0035611D File Offset: 0x0035431D
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.result = null;
		}

		// Token: 0x0600AA99 RID: 43673 RVA: 0x0035613C File Offset: 0x0035433C
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

		// Token: 0x0600AA9A RID: 43674 RVA: 0x003561B6 File Offset: 0x003543B6
		public override void OnActionUpdate()
		{
			this.GetParameter();
		}

		// Token: 0x0600AA9B RID: 43675 RVA: 0x003561BE File Offset: 0x003543BE
		private void GetParameter()
		{
			if (this._animator != null)
			{
				this.result.Value = this._animator.GetBool(this._paramID);
			}
		}

		// Token: 0x04009142 RID: 37186
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009143 RID: 37187
		[RequiredField]
		[UIHint(UIHint.AnimatorBool)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009144 RID: 37188
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The bool value of the animator parameter")]
		public FsmBool result;

		// Token: 0x04009145 RID: 37189
		private Animator _animator;

		// Token: 0x04009146 RID: 37190
		private int _paramID;
	}
}
