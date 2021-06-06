using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF4 RID: 3828
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Resets a trigger parameter. Triggers are parameters that act mostly like booleans, but get reset to inactive when they are used in a transition.")]
	public class ResetAnimatorTrigger : FsmStateAction
	{
		// Token: 0x0600AB37 RID: 43831 RVA: 0x003581A8 File Offset: 0x003563A8
		public override void Reset()
		{
			this.gameObject = null;
			this.trigger = null;
		}

		// Token: 0x0600AB38 RID: 43832 RVA: 0x003581B8 File Offset: 0x003563B8
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
			this.ResetTrigger();
			base.Finish();
		}

		// Token: 0x0600AB39 RID: 43833 RVA: 0x00358214 File Offset: 0x00356414
		private void ResetTrigger()
		{
			if (this._animator != null)
			{
				this._animator.ResetTrigger(this.trigger.Value);
			}
		}

		// Token: 0x040091F7 RID: 37367
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091F8 RID: 37368
		[RequiredField]
		[UIHint(UIHint.AnimatorTrigger)]
		[Tooltip("The trigger name")]
		public FsmString trigger;

		// Token: 0x040091F9 RID: 37369
		private Animator _animator;
	}
}
