using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of a bool parameter")]
	public class GetAnimatorBoolWithEvent : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.AnimatorBool)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The bool value of the animator parameter")]
		public FsmBool result;

		[Space]
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Bool parameter is TRUE event.")]
		public FsmEvent boolIsTrueEvent;

		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Bool parameter is FALSE event.")]
		public FsmEvent boolIsFalseEvent;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			result = null;
			boolIsTrueEvent = null;
			boolIsFalseEvent = null;
		}

		public override void OnEnter()
		{
			Animator component = base.Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<Animator>();
			int id = Animator.StringToHash(parameter.Value);
			result.Value = component.GetBool(id);
			base.Fsm.Event(result.Value ? boolIsTrueEvent : boolIsFalseEvent);
			Finish();
		}
	}
}
