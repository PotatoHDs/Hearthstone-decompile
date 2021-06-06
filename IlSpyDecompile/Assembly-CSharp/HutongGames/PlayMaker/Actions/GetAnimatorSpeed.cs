using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the playback speed of the Animator. 1 is normal playback speed")]
	public class GetAnimatorSpeed : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack speed of the animator. 1 is normal playback speed")]
		public FsmFloat speed;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			speed = null;
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
			GetPlaybackSpeed();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			GetPlaybackSpeed();
		}

		private void GetPlaybackSpeed()
		{
			if (_animator != null)
			{
				speed.Value = _animator.speed;
			}
		}
	}
}
