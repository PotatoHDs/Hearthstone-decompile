using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Fades out a looping audio clip.")]
	public class AudioLoopingEndAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		public FsmFloat m_FadeOutTime;

		public FsmBool m_stop;

		private AudioSource m_audioSource;

		private float m_startVolume;

		private float m_startTime;

		private float m_currentTime;

		private float m_endTime;

		private float m_progress;

		public override void Reset()
		{
			m_GameObject = null;
			m_FadeOutTime = 0f;
			m_stop = true;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			m_audioSource = ownerDefaultTarget.GetComponent<AudioSource>();
			if (m_FadeOutTime.Value <= Mathf.Epsilon)
			{
				Finish();
				return;
			}
			m_startVolume = SoundManager.Get().GetVolume(m_audioSource);
			m_startTime = FsmTime.RealtimeSinceStartup;
			m_currentTime = m_startTime;
			m_endTime = m_startTime + m_FadeOutTime.Value;
		}

		public override void OnUpdate()
		{
			m_currentTime += Time.deltaTime;
			if (m_currentTime < m_endTime)
			{
				float num = (m_currentTime - m_startTime) / m_FadeOutTime.Value;
				float volume = Mathf.Lerp(m_startVolume, 0f, num);
				m_progress = num;
				SoundManager.Get().SetVolume(m_audioSource, volume);
				return;
			}
			SoundManager.Get().SetVolume(m_audioSource, 0f);
			if (m_stop.Value)
			{
				SoundManager.Get().Stop(m_audioSource);
			}
			Finish();
		}

		public override float GetProgress()
		{
			return m_progress;
		}
	}
}
