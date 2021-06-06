using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE2 RID: 3810
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if the current rig is humanoid, false if it is generic. Can also sends events")]
	public class GetAnimatorIsHuman : FsmStateAction
	{
		// Token: 0x0600AAE0 RID: 43744 RVA: 0x00357142 File Offset: 0x00355342
		public override void Reset()
		{
			this.gameObject = null;
			this.isHuman = null;
			this.isHumanEvent = null;
			this.isGenericEvent = null;
		}

		// Token: 0x0600AAE1 RID: 43745 RVA: 0x00357160 File Offset: 0x00355360
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
			this.DoCheckIsHuman();
			base.Finish();
		}

		// Token: 0x0600AAE2 RID: 43746 RVA: 0x003571BC File Offset: 0x003553BC
		private void DoCheckIsHuman()
		{
			if (this._animator == null)
			{
				return;
			}
			bool flag = this._animator.isHuman;
			if (!this.isHuman.IsNone)
			{
				this.isHuman.Value = flag;
			}
			if (flag)
			{
				base.Fsm.Event(this.isHumanEvent);
				return;
			}
			base.Fsm.Event(this.isGenericEvent);
		}

		// Token: 0x0400919D RID: 37277
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400919E RID: 37278
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the current rig is humanoid, False if it is generic")]
		public FsmBool isHuman;

		// Token: 0x0400919F RID: 37279
		[Tooltip("Event send if rig is humanoid")]
		public FsmEvent isHumanEvent;

		// Token: 0x040091A0 RID: 37280
		[Tooltip("Event send if rig is generic")]
		public FsmEvent isGenericEvent;

		// Token: 0x040091A1 RID: 37281
		private Animator _animator;
	}
}
