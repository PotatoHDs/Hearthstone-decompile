using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if automatic matching is active. Can also send events")]
	public class GetAnimatorIsMatchingTarget : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isMatchingActive;

		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent matchingActivatedEvent;

		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent matchingDeactivedEvent;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			isMatchingActive = null;
			matchingActivatedEvent = null;
			matchingDeactivedEvent = null;
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
			DoCheckIsMatchingActive();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoCheckIsMatchingActive();
		}

		private void DoCheckIsMatchingActive()
		{
			if (!(_animator == null))
			{
				bool isMatchingTarget = _animator.isMatchingTarget;
				isMatchingActive.Value = isMatchingTarget;
				if (isMatchingTarget)
				{
					base.Fsm.Event(matchingActivatedEvent);
				}
				else
				{
					base.Fsm.Event(matchingDeactivedEvent);
				}
			}
		}
	}
}
