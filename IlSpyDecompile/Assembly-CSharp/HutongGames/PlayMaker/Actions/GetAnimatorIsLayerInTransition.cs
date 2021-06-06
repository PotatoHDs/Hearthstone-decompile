using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns true if the specified layer is in a transition. Can also send events")]
	public class GetAnimatorIsLayerInTransition : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("True if automatic matching is active")]
		public FsmBool isInTransition;

		[Tooltip("Event send if automatic matching is active")]
		public FsmEvent isInTransitionEvent;

		[Tooltip("Event send if automatic matching is not active")]
		public FsmEvent isNotInTransitionEvent;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			isInTransition = null;
			isInTransitionEvent = null;
			isNotInTransitionEvent = null;
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
			DoCheckIsInTransition();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoCheckIsInTransition();
		}

		private void DoCheckIsInTransition()
		{
			if (!(_animator == null))
			{
				bool flag = _animator.IsInTransition(layerIndex.Value);
				if (!isInTransition.IsNone)
				{
					isInTransition.Value = flag;
				}
				if (flag)
				{
					base.Fsm.Event(isInTransitionEvent);
				}
				else
				{
					base.Fsm.Event(isNotInTransitionEvent);
				}
			}
		}
	}
}
