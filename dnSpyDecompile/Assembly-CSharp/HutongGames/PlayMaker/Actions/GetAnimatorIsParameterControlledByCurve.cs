using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE5 RID: 3813
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if a parameter is controlled by an additional curve on an animation")]
	public class GetAnimatorIsParameterControlledByCurve : FsmStateAction
	{
		// Token: 0x0600AAEE RID: 43758 RVA: 0x00357412 File Offset: 0x00355612
		public override void Reset()
		{
			this.gameObject = null;
			this.parameterName = null;
			this.isControlledByCurve = null;
			this.isControlledByCurveEvent = null;
			this.isNotControlledByCurveEvent = null;
		}

		// Token: 0x0600AAEF RID: 43759 RVA: 0x00357438 File Offset: 0x00355638
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
			this.DoCheckIsParameterControlledByCurve();
			base.Finish();
		}

		// Token: 0x0600AAF0 RID: 43760 RVA: 0x00357494 File Offset: 0x00355694
		private void DoCheckIsParameterControlledByCurve()
		{
			if (this._animator == null)
			{
				return;
			}
			bool flag = this._animator.IsParameterControlledByCurve(this.parameterName.Value);
			this.isControlledByCurve.Value = flag;
			if (flag)
			{
				base.Fsm.Event(this.isControlledByCurveEvent);
				return;
			}
			base.Fsm.Event(this.isNotControlledByCurveEvent);
		}

		// Token: 0x040091AD RID: 37293
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091AE RID: 37294
		[Tooltip("The parameter's name")]
		public FsmString parameterName;

		// Token: 0x040091AF RID: 37295
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if controlled by curve")]
		public FsmBool isControlledByCurve;

		// Token: 0x040091B0 RID: 37296
		[Tooltip("Event send if controlled by curve")]
		public FsmEvent isControlledByCurveEvent;

		// Token: 0x040091B1 RID: 37297
		[Tooltip("Event send if not controlled by curve")]
		public FsmEvent isNotControlledByCurveEvent;

		// Token: 0x040091B2 RID: 37298
		private Animator _animator;
	}
}
