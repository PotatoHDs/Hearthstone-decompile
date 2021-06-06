using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F05 RID: 3845
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets a trigger parameter to active. Triggers are parameters that act mostly like booleans, but get reset to inactive when they are used in a transition.")]
	public class SetAnimatorTrigger : FsmStateAction
	{
		// Token: 0x0600AB8A RID: 43914 RVA: 0x00359284 File Offset: 0x00357484
		public override void Reset()
		{
			this.gameObject = null;
			this.trigger = null;
		}

		// Token: 0x0600AB8B RID: 43915 RVA: 0x00359294 File Offset: 0x00357494
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
			this.SetTrigger();
			base.Finish();
		}

		// Token: 0x0600AB8C RID: 43916 RVA: 0x003592F0 File Offset: 0x003574F0
		private void SetTrigger()
		{
			if (this._animator != null)
			{
				this._animator.SetTrigger(this.trigger.Value);
			}
		}

		// Token: 0x0400924B RID: 37451
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400924C RID: 37452
		[RequiredField]
		[UIHint(UIHint.AnimatorTrigger)]
		[Tooltip("The trigger name")]
		public FsmString trigger;

		// Token: 0x0400924D RID: 37453
		private Animator _animator;
	}
}
