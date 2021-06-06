using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets an AvatarTarget and a targetNormalizedTime for the current state")]
	public class SetAnimatorTarget : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The avatar target")]
		public AvatarTarget avatarTarget;

		[Tooltip("The current state Time that is queried")]
		public FsmFloat targetNormalizedTime;

		[Tooltip("Repeat every frame during OnAnimatorMove. Useful when changing over time.")]
		public bool everyFrame;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			avatarTarget = AvatarTarget.Body;
			targetNormalizedTime = null;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorMove = true;
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
			SetTarget();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void DoAnimatorMove()
		{
			SetTarget();
		}

		private void SetTarget()
		{
			if (_animator != null)
			{
				_animator.SetTarget(avatarTarget, targetNormalizedTime.Value);
			}
		}
	}
}
