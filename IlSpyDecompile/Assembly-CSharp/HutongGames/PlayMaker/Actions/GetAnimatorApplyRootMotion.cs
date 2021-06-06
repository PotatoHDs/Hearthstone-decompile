using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of ApplyRootMotion of an avatar. If true, root is controlled by animations")]
	public class GetAnimatorApplyRootMotion : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the rootMotionapplied. If true, root is controlled by animations")]
		public FsmBool rootMotionApplied;

		[Tooltip("Event send if the root motion is applied")]
		public FsmEvent rootMotionIsAppliedEvent;

		[Tooltip("Event send if the root motion is not applied")]
		public FsmEvent rootMotionIsNotAppliedEvent;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			rootMotionApplied = null;
			rootMotionIsAppliedEvent = null;
			rootMotionIsNotAppliedEvent = null;
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
			GetApplyMotionRoot();
			Finish();
		}

		private void GetApplyMotionRoot()
		{
			if (_animator != null)
			{
				bool applyRootMotion = _animator.applyRootMotion;
				rootMotionApplied.Value = applyRootMotion;
				if (applyRootMotion)
				{
					base.Fsm.Event(rootMotionIsAppliedEvent);
				}
				else
				{
					base.Fsm.Event(rootMotionIsNotAppliedEvent);
				}
			}
		}
	}
}
