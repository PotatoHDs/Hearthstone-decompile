using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE3 RID: 3811
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if the specified layer is in a transition. Can also send events")]
	public class GetAnimatorIsLayerInTransition : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAE4 RID: 43748 RVA: 0x00357223 File Offset: 0x00355423
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.isInTransition = null;
			this.isInTransitionEvent = null;
			this.isNotInTransitionEvent = null;
		}

		// Token: 0x0600AAE5 RID: 43749 RVA: 0x00357248 File Offset: 0x00355448
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
			this.DoCheckIsInTransition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AAE6 RID: 43750 RVA: 0x003572AC File Offset: 0x003554AC
		public override void OnActionUpdate()
		{
			this.DoCheckIsInTransition();
		}

		// Token: 0x0600AAE7 RID: 43751 RVA: 0x003572B4 File Offset: 0x003554B4
		private void DoCheckIsInTransition()
		{
			if (this._animator == null)
			{
				return;
			}
			bool flag = this._animator.IsInTransition(this.layerIndex.Value);
			if (!this.isInTransition.IsNone)
			{
				this.isInTransition.Value = flag;
			}
			if (flag)
			{
				base.Fsm.Event(this.isInTransitionEvent);
				return;
			}
			base.Fsm.Event(this.isNotInTransitionEvent);
		}

		// Token: 0x040091A2 RID: 37282
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091A3 RID: 37283
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x040091A4 RID: 37284
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isInTransition;

		// Token: 0x040091A5 RID: 37285
		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent isInTransitionEvent;

		// Token: 0x040091A6 RID: 37286
		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent isNotInTransitionEvent;

		// Token: 0x040091A7 RID: 37287
		private Animator _animator;
	}
}
