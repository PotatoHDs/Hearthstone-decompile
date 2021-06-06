using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Plays an Animation on a Game Object and waits for the animation to finish.")]
	public class AnimationPlayAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		public FsmString m_PhoneAnimName;

		[Tooltip("How to treat previously playing animations.")]
		public PlayMode m_PlayMode;

		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time taken to cross fade to this animation.")]
		public FsmFloat m_CrossFadeSec;

		[Tooltip("Event to send when the animation is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
		public FsmEvent m_FinishEvent;

		[Tooltip("Event to send when the animation loops. If you want to send this event to another FSM use Set Event Target. NOTE: This event is only sent with Loop and PingPong wrap modes.")]
		public FsmEvent m_LoopEvent;

		[Tooltip("Stop playing the animation when this state is exited.")]
		public bool m_StopOnExit;

		private string m_animName;

		private AnimationState m_animState;

		private float m_prevAnimTime;

		public override void Reset()
		{
			m_GameObject = null;
			m_AnimName = null;
			m_PhoneAnimName = null;
			m_PlayMode = PlayMode.StopAll;
			m_CrossFadeSec = 0.3f;
			m_FinishEvent = null;
			m_LoopEvent = null;
			m_StopOnExit = false;
		}

		public override void OnEnter()
		{
			if (!CacheAnim())
			{
				Finish();
			}
			else
			{
				StartAnimation();
			}
		}

		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			if (ownerDefaultTarget.GetComponent<Animation>() == null)
			{
				Debug.LogWarning($"AnimationPlayAction.OnUpdate() - GameObject {ownerDefaultTarget} is missing an animation component");
				Finish();
				return;
			}
			if (!m_animState.enabled || (m_animState.wrapMode == WrapMode.ClampForever && m_animState.time > m_animState.length))
			{
				base.Fsm.Event(m_FinishEvent);
				Finish();
			}
			if (m_animState.wrapMode != WrapMode.ClampForever && m_animState.time > m_animState.length && m_prevAnimTime < m_animState.length)
			{
				base.Fsm.Event(m_LoopEvent);
			}
		}

		public override void OnExit()
		{
			if (m_StopOnExit)
			{
				StopAnimation();
			}
		}

		private bool CacheAnim()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			m_animName = m_AnimName.Value;
			if ((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(m_PhoneAnimName.Value))
			{
				m_animName = m_PhoneAnimName.Value;
			}
			m_animState = ownerDefaultTarget.GetComponent<Animation>()[m_animName];
			return true;
		}

		private void StartAnimation()
		{
			if (base.Fsm == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Animation component = ownerDefaultTarget.GetComponent<Animation>();
			if (!(component == null))
			{
				float num = ((m_CrossFadeSec == null) ? 0f : m_CrossFadeSec.Value);
				if (num <= Mathf.Epsilon)
				{
					component.Play(m_animName, m_PlayMode);
				}
				else
				{
					component.CrossFade(m_animName, num, m_PlayMode);
				}
				m_prevAnimTime = ((m_animState == null) ? 0f : m_animState.time);
			}
		}

		private void StopAnimation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null) && !(ownerDefaultTarget.GetComponent<Animation>() == null))
			{
				ownerDefaultTarget.GetComponent<Animation>().Stop(m_animName);
			}
		}
	}
}
