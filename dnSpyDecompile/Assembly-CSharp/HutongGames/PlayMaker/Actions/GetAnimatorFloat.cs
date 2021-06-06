using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDD RID: 3805
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of a float parameter")]
	public class GetAnimatorFloat : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAC8 RID: 43720 RVA: 0x00356C87 File Offset: 0x00354E87
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.result = null;
		}

		// Token: 0x0600AAC9 RID: 43721 RVA: 0x00356CA4 File Offset: 0x00354EA4
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

		// Token: 0x0600AACA RID: 43722 RVA: 0x00356D1E File Offset: 0x00354F1E
		public override void OnActionUpdate()
		{
			this.GetParameter();
		}

		// Token: 0x0600AACB RID: 43723 RVA: 0x00356D26 File Offset: 0x00354F26
		private void GetParameter()
		{
			if (this._animator != null)
			{
				this.result.Value = this._animator.GetFloat(this._paramID);
			}
		}

		// Token: 0x04009183 RID: 37251
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009184 RID: 37252
		[RequiredField]
		[UIHint(UIHint.AnimatorFloat)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009185 RID: 37253
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float value of the animator parameter")]
		public FsmFloat result;

		// Token: 0x04009186 RID: 37254
		private Animator _animator;

		// Token: 0x04009187 RID: 37255
		private int _paramID;
	}
}
