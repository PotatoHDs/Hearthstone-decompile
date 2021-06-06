using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the speed of an Animation.")]
	public class SetAnimationTimeAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		[RequiredField]
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		public FsmString m_PhoneAnimName;

		public FsmFloat m_Time;

		public bool m_EveryFrame;

		private AnimationState m_animState;

		public override void Reset()
		{
			m_GameObject = null;
			m_AnimName = null;
			m_PhoneAnimName = null;
			m_Time = 0f;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			if (!CacheAnim())
			{
				Finish();
				return;
			}
			UpdateTime();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
			}
			else if (ownerDefaultTarget.GetComponent<Animation>() == null)
			{
				Debug.LogWarning($"SetAnimationSpeedAction.OnUpdate() - GameObject {ownerDefaultTarget} is missing an animation component");
				Finish();
			}
			else
			{
				UpdateTime();
			}
		}

		private bool CacheAnim()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return false;
			}
			string value = m_AnimName.Value;
			if ((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(m_PhoneAnimName.Value))
			{
				value = m_PhoneAnimName.Value;
			}
			m_animState = ownerDefaultTarget.GetComponent<Animation>()[value];
			return true;
		}

		private void UpdateTime()
		{
			m_animState.time = m_Time.Value;
		}
	}
}
