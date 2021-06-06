using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F35 RID: 3893
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of a bool parameter")]
	public class GetAnimatorBoolWithEvent : FsmStateAction
	{
		// Token: 0x0600AC64 RID: 44132 RVA: 0x0035CA01 File Offset: 0x0035AC01
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.parameter = null;
			this.result = null;
			this.boolIsTrueEvent = null;
			this.boolIsFalseEvent = null;
		}

		// Token: 0x0600AC65 RID: 44133 RVA: 0x0035CA2C File Offset: 0x0035AC2C
		public override void OnEnter()
		{
			Animator component = base.Fsm.GetOwnerDefaultTarget(this.gameObject).GetComponent<Animator>();
			int id = Animator.StringToHash(this.parameter.Value);
			this.result.Value = component.GetBool(id);
			base.Fsm.Event(this.result.Value ? this.boolIsTrueEvent : this.boolIsFalseEvent);
			base.Finish();
		}

		// Token: 0x0400933E RID: 37694
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400933F RID: 37695
		[RequiredField]
		[UIHint(UIHint.AnimatorBool)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		// Token: 0x04009340 RID: 37696
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The bool value of the animator parameter")]
		public FsmBool result;

		// Token: 0x04009341 RID: 37697
		[Space]
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Bool parameter is TRUE event.")]
		public FsmEvent boolIsTrueEvent;

		// Token: 0x04009342 RID: 37698
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Bool parameter is FALSE event.")]
		public FsmEvent boolIsFalseEvent;
	}
}
