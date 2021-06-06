using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Plays an Animation on a Game Object. Does not wait for the animation to finish.")]
	public class AnimationPlaythroughAction : FsmStateAction
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

		public override void Reset()
		{
			m_GameObject = null;
			m_AnimName = null;
			m_PhoneAnimName = null;
			m_PlayMode = PlayMode.StopAll;
			m_CrossFadeSec = 0.3f;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Debug.LogWarning("AnimationPlaythroughAction GameObject is null!");
				Finish();
				return;
			}
			string value = m_AnimName.Value;
			if ((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(m_PhoneAnimName.Value))
			{
				value = m_PhoneAnimName.Value;
			}
			if (string.IsNullOrEmpty(value))
			{
				Finish();
				return;
			}
			StartAnimation(ownerDefaultTarget, value);
			Finish();
		}

		private void StartAnimation(GameObject go, string animName)
		{
			float value = m_CrossFadeSec.Value;
			if (value <= Mathf.Epsilon)
			{
				go.GetComponent<Animation>().Play(animName, m_PlayMode);
			}
			else
			{
				go.GetComponent<Animation>().CrossFade(animName, value, m_PlayMode);
			}
		}
	}
}
