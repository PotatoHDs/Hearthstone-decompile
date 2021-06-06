using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress")]
	public class AnimatorMatchTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget bodyPart;

		[Tooltip("The gameObject target to match")]
		public FsmGameObject target;

		[Tooltip("The position of the ik goal. If Goal GameObject set, position is used as an offset from Goal")]
		public FsmVector3 targetPosition;

		[Tooltip("The rotation of the ik goal.If Goal GameObject set, rotation is used as an offset from Goal")]
		public FsmQuaternion targetRotation;

		[Tooltip("The MatchTargetWeightMask Position XYZ weight")]
		public FsmVector3 positionWeight;

		[Tooltip("The MatchTargetWeightMask Rotation weight")]
		public FsmFloat rotationWeight;

		[Tooltip("Start time within the animation clip (0 - beginning of clip, 1 - end of clip)")]
		public FsmFloat startNormalizedTime;

		[Tooltip("End time within the animation clip (0 - beginning of clip, 1 - end of clip), values greater than 1 can be set to trigger a match after a certain number of loops. Ex: 2.3 means at 30% of 2nd loop")]
		public FsmFloat targetNormalizedTime;

		[Tooltip("Should always be true")]
		public bool everyFrame;

		private Animator _animator;

		private Transform _transform;

		public override void Reset()
		{
			gameObject = null;
			bodyPart = AvatarTarget.Root;
			target = null;
			targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			targetRotation = new FsmQuaternion
			{
				UseVariable = true
			};
			positionWeight = Vector3.one;
			rotationWeight = 0f;
			startNormalizedTime = null;
			targetNormalizedTime = null;
			everyFrame = true;
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
			GameObject value = target.Value;
			if (value != null)
			{
				_transform = value.transform;
			}
			DoMatchTarget();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMatchTarget();
		}

		private void DoMatchTarget()
		{
			if (!(_animator == null))
			{
				Vector3 matchPosition = Vector3.zero;
				Quaternion matchRotation = Quaternion.identity;
				if (_transform != null)
				{
					matchPosition = _transform.position;
					matchRotation = _transform.rotation;
				}
				if (!targetPosition.IsNone)
				{
					matchPosition += targetPosition.Value;
				}
				if (!targetRotation.IsNone)
				{
					matchRotation *= targetRotation.Value;
				}
				MatchTargetWeightMask weightMask = new MatchTargetWeightMask(positionWeight.Value, rotationWeight.Value);
				_animator.MatchTarget(matchPosition, matchRotation, bodyPart, weightMask, startNormalizedTime.Value, targetNormalizedTime.Value);
			}
		}
	}
}
