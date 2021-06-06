using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the current transition information on a specified layer. Only valid when during a transition.")]
	public class GetAnimatorCurrentTransitionInfo : FsmStateActionAnimatorBase
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
		[Tooltip("The unique name of the Transition")]
		public FsmString name;

		[UIHint(UIHint.Variable)]
		[Tooltip("The unique name of the Transition")]
		public FsmInt nameHash;

		[UIHint(UIHint.Variable)]
		[Tooltip("The user-specified name of the Transition")]
		public FsmInt userNameHash;

		[UIHint(UIHint.Variable)]
		[Tooltip("Normalized time of the Transition")]
		public FsmFloat normalizedTime;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			layerIndex = null;
			name = null;
			nameHash = null;
			userNameHash = null;
			normalizedTime = null;
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
			GetTransitionInfo();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			GetTransitionInfo();
		}

		private void GetTransitionInfo()
		{
			if (_animator != null)
			{
				AnimatorTransitionInfo animatorTransitionInfo = _animator.GetAnimatorTransitionInfo(layerIndex.Value);
				if (!name.IsNone)
				{
					name.Value = _animator.GetLayerName(layerIndex.Value);
				}
				if (!nameHash.IsNone)
				{
					nameHash.Value = animatorTransitionInfo.nameHash;
				}
				if (!userNameHash.IsNone)
				{
					userNameHash.Value = animatorTransitionInfo.userNameHash;
				}
				if (!normalizedTime.IsNone)
				{
					normalizedTime.Value = animatorTransitionInfo.normalizedTime;
				}
			}
		}
	}
}
