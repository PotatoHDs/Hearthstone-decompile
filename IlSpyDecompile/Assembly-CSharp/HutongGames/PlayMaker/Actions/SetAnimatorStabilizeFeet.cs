using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("If true, automatically stabilize feet during transition and blending")]
	public class SetAnimatorStabilizeFeet : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("If true, automatically stabilize feet during transition and blending")]
		public FsmBool stabilizeFeet;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			stabilizeFeet = null;
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
			DoStabilizeFeet();
			Finish();
		}

		private void DoStabilizeFeet()
		{
			if (!(_animator == null))
			{
				_animator.stabilizeFeet = stabilizeFeet.Value;
			}
		}
	}
}
