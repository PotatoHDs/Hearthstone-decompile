using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Set Apply Root Motion: If true, Root is controlled by animations")]
	public class SetAnimatorApplyRootMotion : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("If true, Root is controlled by animations")]
		public FsmBool applyRootMotion;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			applyRootMotion = null;
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
			DoApplyRootMotion();
			Finish();
		}

		private void DoApplyRootMotion()
		{
			if (!(_animator == null))
			{
				_animator.applyRootMotion = applyRootMotion.Value;
			}
		}
	}
}
