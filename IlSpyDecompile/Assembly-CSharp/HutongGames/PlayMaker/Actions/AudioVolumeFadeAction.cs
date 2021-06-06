using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Fades an Audio Source component's volume towards a target value.")]
	public class AudioVolumeFadeAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		[RequiredField]
		public FsmFloat m_FadeTime;

		[RequiredField]
		public FsmFloat m_TargetVolume;

		[Tooltip("Stop the audio source when the target volume is reached.")]
		public FsmBool m_StopWhenDone;

		[Tooltip("Use real time. Useful if time is scaled and you don't want this action to scale.")]
		public FsmBool m_RealTime;

		private AudioSource m_audio;

		private float m_startVolume;

		private float m_startTime;

		private float m_currentTime;

		private float m_endTime;

		public override void Reset()
		{
			m_GameObject = null;
			m_FadeTime = 1f;
			m_TargetVolume = 0f;
			m_StopWhenDone = true;
			m_RealTime = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			m_audio = ownerDefaultTarget.GetComponent<AudioSource>();
			if (m_audio == null)
			{
				Finish();
				return;
			}
			if (m_FadeTime.Value <= Mathf.Epsilon)
			{
				Finish();
				return;
			}
			m_startVolume = SoundManager.Get().GetVolume(m_audio);
			m_startTime = FsmTime.RealtimeSinceStartup;
			m_currentTime = m_startTime;
			m_endTime = m_startTime + m_FadeTime.Value;
			SoundManager.Get().SetVolume(m_audio, m_startVolume);
		}

		public override void OnUpdate()
		{
			if (m_RealTime.Value)
			{
				m_currentTime = FsmTime.RealtimeSinceStartup - m_startTime;
			}
			else
			{
				m_currentTime += Time.deltaTime;
			}
			if (m_currentTime < m_endTime)
			{
				float t = (m_currentTime - m_startTime) / m_FadeTime.Value;
				float volume = Mathf.Lerp(m_startVolume, m_TargetVolume.Value, t);
				SoundManager.Get().SetVolume(m_audio, volume);
				return;
			}
			SoundManager.Get().SetVolume(m_audio, m_TargetVolume.Value);
			if (m_StopWhenDone.Value)
			{
				SoundManager.Get().Stop(m_audio);
			}
			Finish();
		}
	}
}
