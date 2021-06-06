using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ED0 RID: 3792
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of ApplyRootMotion of an avatar. If true, root is controlled by animations")]
	public class GetAnimatorApplyRootMotion : FsmStateAction
	{
		// Token: 0x0600AA8A RID: 43658 RVA: 0x00355E31 File Offset: 0x00354031
		public override void Reset()
		{
			this.gameObject = null;
			this.rootMotionApplied = null;
			this.rootMotionIsAppliedEvent = null;
			this.rootMotionIsNotAppliedEvent = null;
		}

		// Token: 0x0600AA8B RID: 43659 RVA: 0x00355E50 File Offset: 0x00354050
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
			this.GetApplyMotionRoot();
			base.Finish();
		}

		// Token: 0x0600AA8C RID: 43660 RVA: 0x00355EAC File Offset: 0x003540AC
		private void GetApplyMotionRoot()
		{
			if (this._animator != null)
			{
				bool applyRootMotion = this._animator.applyRootMotion;
				this.rootMotionApplied.Value = applyRootMotion;
				if (applyRootMotion)
				{
					base.Fsm.Event(this.rootMotionIsAppliedEvent);
					return;
				}
				base.Fsm.Event(this.rootMotionIsNotAppliedEvent);
			}
		}

		// Token: 0x04009133 RID: 37171
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009134 RID: 37172
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the rootMotionapplied. If true, root is controlled by animations")]
		public FsmBool rootMotionApplied;

		// Token: 0x04009135 RID: 37173
		[Tooltip("Event send if the root motion is applied")]
		public FsmEvent rootMotionIsAppliedEvent;

		// Token: 0x04009136 RID: 37174
		[Tooltip("Event send if the root motion is not applied")]
		public FsmEvent rootMotionIsNotAppliedEvent;

		// Token: 0x04009137 RID: 37175
		private Animator _animator;
	}
}
