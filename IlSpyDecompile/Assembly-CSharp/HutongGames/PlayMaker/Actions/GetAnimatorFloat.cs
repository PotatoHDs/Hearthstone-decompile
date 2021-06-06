using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of a float parameter")]
	public class GetAnimatorFloat : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.AnimatorFloat)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float value of the animator parameter")]
		public FsmFloat result;

		private Animator _animator;

		private int _paramID;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			result = null;
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
			_paramID = Animator.StringToHash(parameter.Value);
			GetParameter();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			GetParameter();
		}

		private void GetParameter()
		{
			if (_animator != null)
			{
				result.Value = _animator.GetFloat(_paramID);
			}
		}
	}
}
