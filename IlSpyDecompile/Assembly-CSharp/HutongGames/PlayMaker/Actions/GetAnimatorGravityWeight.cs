using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns The current gravity weight based on current animations that are played")]
	public class GetAnimatorGravityWeight : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The current gravity weight based on current animations that are played")]
		public FsmFloat gravityWeight;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			gravityWeight = null;
			everyFrame = false;
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
			DoGetGravityWeight();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetGravityWeight();
		}

		private void DoGetGravityWeight()
		{
			if (!(_animator == null))
			{
				gravityWeight.Value = _animator.gravityWeight;
			}
		}
	}
}
