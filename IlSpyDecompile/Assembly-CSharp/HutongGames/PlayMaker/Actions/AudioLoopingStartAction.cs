using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays the Audio Clip on a Game Object and sets it to loop.")]
	public class AudioLoopingStartAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[CheckForComponent(typeof(SoundDef))]
		public FsmOwnerDefault m_GameObject;

		public FsmFloat m_FadeInTime;

		public FsmFloat m_TargetVolume;

		public FsmBool m_randomizeStartPoint;

		private SoundDef m_soundDef;

		private AudioSource m_audioSource;

		private float m_startVolume;

		private float m_startTime;

		private float m_currentTime;

		private float m_endTime;

		private float m_progress;

		public override void Reset()
		{
			m_GameObject = null;
			m_FadeInTime = 0f;
			m_TargetVolume = 0f;
			m_randomizeStartPoint = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			m_soundDef = ownerDefaultTarget.GetComponent<SoundDef>();
			if (m_soundDef == null)
			{
				Finish();
				return;
			}
			if (m_FadeInTime.Value <= Mathf.Epsilon)
			{
				Finish();
				return;
			}
			SoundManager soundManager = SoundManager.Get();
			m_audioSource = ownerDefaultTarget.GetComponent<AudioSource>();
			m_audioSource.loop = true;
			m_audioSource.enabled = true;
			m_startVolume = soundManager.GetVolume(m_audioSource);
			m_startTime = FsmTime.RealtimeSinceStartup;
			m_currentTime = m_startTime;
			m_endTime = m_startTime + m_FadeInTime.Value;
			if (m_randomizeStartPoint.Value)
			{
				m_audioSource.time = Random.value * m_audioSource.clip.length;
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_def = m_soundDef;
			soundPlayClipArgs.m_templateSource = m_audioSource;
			SoundManager.Get().PlayClip(soundPlayClipArgs, createNewSource: false);
		}

		public override void OnUpdate()
		{
			m_currentTime += Time.deltaTime;
			if (m_currentTime < m_endTime)
			{
				float num = (m_currentTime - m_startTime) / m_FadeInTime.Value;
				float volume = Mathf.Lerp(m_startVolume, m_TargetVolume.Value, num);
				m_progress = num;
				SoundManager.Get().SetVolume(m_audioSource, volume);
			}
			else
			{
				SoundManager.Get().SetVolume(m_audioSource, m_TargetVolume.Value);
				Finish();
			}
		}

		public override float GetProgress()
		{
			return m_progress;
		}
	}
}
