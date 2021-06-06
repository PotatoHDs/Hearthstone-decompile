using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the value of a float parameter")]
	public class SetAnimatorFloat : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.AnimatorFloat)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;

		[Tooltip("The float value to assign to the animator parameter")]
		public FsmFloat Value;

		[Tooltip("Optional: The time allowed to parameter to reach the value. Requires everyFrame Checked on")]
		public FsmFloat dampTime;

		private Animator _animator;

		private int _paramID;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			dampTime = new FsmFloat
			{
				UseVariable = true
			};
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
			if (!(_animator == null))
			{
				if (dampTime.Value > 0f)
				{
					_animator.SetFloat(_paramID, Value.Value, dampTime.Value, Time.deltaTime);
				}
				else
				{
					_animator.SetFloat(_paramID, Value.Value);
				}
			}
		}
	}
}
