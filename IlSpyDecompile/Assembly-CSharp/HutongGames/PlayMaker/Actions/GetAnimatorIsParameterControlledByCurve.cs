using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if a parameter is controlled by an additional curve on an animation")]
	public class GetAnimatorIsParameterControlledByCurve : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The parameter's name")]
		public FsmString parameterName;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if controlled by curve")]
		public FsmBool isControlledByCurve;

		[Tooltip("Event send if controlled by curve")]
		public FsmEvent isControlledByCurveEvent;

		[Tooltip("Event send if not controlled by curve")]
		public FsmEvent isNotControlledByCurveEvent;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			parameterName = null;
			isControlledByCurve = null;
			isControlledByCurveEvent = null;
			isNotControlledByCurveEvent = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			_animator = ownerDefaultTarget.GetComponent<Animator>();
			if (_animator == null)
			{
				Finish();
				return;
			}
			DoCheckIsParameterControlledByCurve();
			Finish();
		}

		private void DoCheckIsParameterControlledByCurve()
		{
			if (!(_animator == null))
			{
				bool flag = _animator.IsParameterControlledByCurve(parameterName.Value);
				isControlledByCurve.Value = flag;
				if (flag)
				{
					base.Fsm.Event(isControlledByCurveEvent);
				}
				else
				{
					base.Fsm.Event(isNotControlledByCurveEvent);
				}
			}
		}
	}
}
