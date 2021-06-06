using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFA RID: 3834
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the value of a float parameter")]
	public class SetAnimatorFloat : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB52 RID: 43858 RVA: 0x003586A7 File Offset: 0x003568A7
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.dampTime = new FsmFloat
			{
				UseVariable = true
			};
			this.Value = null;
		}

		// Token: 0x0600AB53 RID: 43859 RVA: 0x003586D8 File Offset: 0x003568D8
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

		// Token: 0x0600AB54 RID: 43860 RVA: 0x00358752 File Offset: 0x00356952
		public override void OnActionUpdate()
		{
			this.SetParameter();
		}

		// Token: 0x0600AB55 RID: 43861 RVA: 0x0035875C File Offset: 0x0035695C
		private void SetParameter()
		{
			if (this._animator == null)
			{
				return;
			}
			if (this.dampTime.Value > 0f)
			{
				this._animator.SetFloat(this._paramID, this.Value.Value, this.dampTime.Value, Time.deltaTime);
				return;
			}
			this._animator.SetFloat(this._paramID, this.Value.Value);
		}

		// Token: 0x0400920F RID: 37391
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009210 RID: 37392
		[RequiredField]
		[UIHint(UIHint.AnimatorFloat)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009211 RID: 37393
		[Tooltip("The float value to assign to the animator parameter")]
		public FsmFloat Value;

		// Token: 0x04009212 RID: 37394
		[Tooltip("Optional: The time allowed to parameter to reach the value. Requires everyFrame Checked on")]
		public FsmFloat dampTime;

		// Token: 0x04009213 RID: 37395
		private Animator _animator;

		// Token: 0x04009214 RID: 37396
		private int _paramID;
	}
}
