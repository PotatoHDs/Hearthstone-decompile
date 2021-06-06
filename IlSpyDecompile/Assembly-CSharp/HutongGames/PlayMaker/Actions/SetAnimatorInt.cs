using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the value of a int parameter")]
	public class SetAnimatorInt : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.AnimatorInt)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		[Tooltip("The Int value to assign to the animator parameter")]
		public FsmInt Value;

		private Animator _animator;

		private int _paramID;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			Value = null;
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
			SetParameter();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			SetParameter();
		}

		private void SetParameter()
		{
			if (_animator != null)
			{
				_animator.SetInteger(_paramID, Value.Value);
			}
		}
	}
}
