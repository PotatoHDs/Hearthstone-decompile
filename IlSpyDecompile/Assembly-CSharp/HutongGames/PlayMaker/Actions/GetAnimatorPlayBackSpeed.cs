using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the playback speed of the Animator. 1 is normal playback speed")]
	public class GetAnimatorPlayBackSpeed : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack speed of the animator. 1 is normal playback speed")]
		public FsmFloat playBackSpeed;

		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			playBackSpeed = null;
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
			GetPlayBackSpeed();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GetPlayBackSpeed();
		}

		private void GetPlayBackSpeed()
		{
			if (_animator != null)
			{
				playBackSpeed.Value = _animator.speed;
			}
		}
	}
}
