using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the position, rotation and weights of an IK goal. A GameObject can be set to use for the position and rotation")]
	public class GetAnimatorIKGoal : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The IK goal")]
		[ObjectType(typeof(AvatarIKGoal))]
		public FsmEnum iKGoal;

		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The gameObject to apply ik goal position and rotation to")]
		public FsmGameObject goal;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The position of the ik goal. If Goal GameObject define, position is used as an offset from Goal")]
		public FsmVector3 position;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The rotation of the ik goal.If Goal GameObject define, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		public FsmFloat positionWeight;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		public FsmFloat rotationWeight;

		private Animator _animator;

		private Transform _transform;

		private AvatarIKGoal _iKGoal;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			iKGoal = null;
			goal = null;
			position = null;
			rotation = null;
			positionWeight = null;
			rotationWeight = null;
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
			GameObject value = goal.Value;
			if (value != null)
			{
				_transform = value.transform;
			}
		}

		public override void OnActionUpdate()
		{
			DoGetIKGoal();
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoGetIKGoal()
		{
			if (!(_animator == null))
			{
				_iKGoal = (AvatarIKGoal)(object)iKGoal.Value;
				if (_transform != null)
				{
					_transform.position = _animator.GetIKPosition(_iKGoal);
					_transform.rotation = _animator.GetIKRotation(_iKGoal);
				}
				if (!position.IsNone)
				{
					position.Value = _animator.GetIKPosition(_iKGoal);
				}
				if (!rotation.IsNone)
				{
					rotation.Value = _animator.GetIKRotation(_iKGoal);
				}
				if (!positionWeight.IsNone)
				{
					positionWeight.Value = _animator.GetIKPositionWeight(_iKGoal);
				}
				if (!rotationWeight.IsNone)
				{
					rotationWeight.Value = _animator.GetIKRotationWeight(_iKGoal);
				}
			}
		}
	}
}
