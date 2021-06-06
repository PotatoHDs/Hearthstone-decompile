using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the position and rotation of the target specified by SetTarget(AvatarTarget targetIndex, float targetNormalizedTime)).\nThe position and rotation are only valid when a frame has being evaluated after the SetTarget call")]
	public class GetAnimatorTarget : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The target position")]
		public FsmVector3 targetPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The target rotation")]
		public FsmQuaternion targetRotation;

		[Tooltip("If set, apply the position and rotation to this gameObject")]
		public FsmGameObject targetGameObject;

		private Animator _animator;

		private Transform _transform;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			targetPosition = null;
			targetRotation = null;
			targetGameObject = null;
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
			GameObject value = targetGameObject.Value;
			if (value != null)
			{
				_transform = value.transform;
			}
			DoGetTarget();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetTarget();
		}

		private void DoGetTarget()
		{
			if (!(_animator == null))
			{
				targetPosition.Value = _animator.targetPosition;
				targetRotation.Value = _animator.targetRotation;
				if (_transform != null)
				{
					_transform.position = _animator.targetPosition;
					_transform.rotation = _animator.targetRotation;
				}
			}
		}
	}
}
