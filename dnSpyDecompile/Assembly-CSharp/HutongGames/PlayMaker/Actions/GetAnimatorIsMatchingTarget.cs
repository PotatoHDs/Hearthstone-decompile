using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE4 RID: 3812
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if automatic matching is active. Can also send events")]
	public class GetAnimatorIsMatchingTarget : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAE9 RID: 43753 RVA: 0x00357326 File Offset: 0x00355526
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.isMatchingActive = null;
			this.matchingActivatedEvent = null;
			this.matchingDeactivedEvent = null;
		}

		// Token: 0x0600AAEA RID: 43754 RVA: 0x0035734C File Offset: 0x0035554C
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
			this.DoCheckIsMatchingActive();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAEB RID: 43755 RVA: 0x003573B0 File Offset: 0x003555B0
		public override void OnActionUpdate()
		{
			this.DoCheckIsMatchingActive();
		}

		// Token: 0x0600AAEC RID: 43756 RVA: 0x003573B8 File Offset: 0x003555B8
		private void DoCheckIsMatchingActive()
		{
			if (this._animator == null)
			{
				return;
			}
			bool isMatchingTarget = this._animator.isMatchingTarget;
			this.isMatchingActive.Value = isMatchingTarget;
			if (isMatchingTarget)
			{
				base.Fsm.Event(this.matchingActivatedEvent);
				return;
			}
			base.Fsm.Event(this.matchingDeactivedEvent);
		}

		// Token: 0x040091A8 RID: 37288
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091A9 RID: 37289
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isMatchingActive;

		// Token: 0x040091AA RID: 37290
		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent matchingActivatedEvent;

		// Token: 0x040091AB RID: 37291
		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent matchingDeactivedEvent;

		// Token: 0x040091AC RID: 37292
		private Animator _animator;
	}
}
