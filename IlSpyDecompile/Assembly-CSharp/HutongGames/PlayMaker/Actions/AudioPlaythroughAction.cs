using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays the Audio Clip on a Game Object or plays a one shot clip. Does not wait for the audio to finish.")]
	public class AudioPlaythroughAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault m_GameObject;

		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a One Shot AudioClip.")]
		public FsmObject m_OneShotClip;

		public float m_Delay;

		[ObjectType(typeof(SoundDef))]
		[Tooltip("Optionally play a one shot AudioClip.")]
		public FsmObject m_OneShotSound;

		[Tooltip("Event to send when the AudioSource finishes playing.")]
		public FsmEvent m_FinishedEvent;

		[Tooltip("If true, this audio clip will be suppressed by SUPPRESS_ALL_SUMMON_VO")]
		public bool m_IsMinionSummonVO;

		[Tooltip("If true, there will be a limit to the number instances of of this sound that can play at once.")]
		public bool m_InstanceLimited;

		[Tooltip("If instance limited, this defines the duration that each clip will prevent another from playing.  If zero, then it will measure the length of the audio file.")]
		public float m_InstanceLimitedDuration;

		[Tooltip("If instance limited, this defines the maximum number of instances of the sound that can be playing at once.")]
		public int m_InstanceLimitMaximum = 1;

		private float m_DelayTime;

		public override void Reset()
		{
			m_GameObject = null;
			m_VolumeScale = 1f;
			m_OneShotClip = null;
			m_Delay = 0f;
			m_DelayTime = 0f;
			m_OneShotSound = null;
		}

		public override void OnEnter()
		{
			if (m_Delay > 0f)
			{
				m_DelayTime = m_Delay;
				StartCoroutine(Delay());
			}
			else
			{
				Play();
			}
		}

		private AudioSource GetSource()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return null;
			}
			return ownerDefaultTarget.GetComponent<AudioSource>();
		}

		private void Play()
		{
			AudioSource source = GetSource();
			if (source == null)
			{
				base.Fsm.Event(m_FinishedEvent);
				Finish();
				return;
			}
			if (m_IsMinionSummonVO)
			{
				Actor actor = GetActor();
				if (actor != null && actor.GetEntity() != null && actor.GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO))
				{
					base.Fsm.Event(m_FinishedEvent);
					Finish();
					return;
				}
			}
			SoundManager.SoundOptions options = new SoundManager.SoundOptions
			{
				InstanceLimited = m_InstanceLimited,
				InstanceTimeLimit = m_InstanceLimitedDuration,
				MaxInstancesOfThisSound = m_InstanceLimitMaximum
			};
			SoundDef soundDef = m_OneShotSound.Value as SoundDef;
			if (soundDef == null)
			{
				if (!m_VolumeScale.IsNone)
				{
					SoundManager.Get().SetVolume(source, m_VolumeScale.Value);
				}
				SoundManager.Get().Play(source, null, null, options);
			}
			else
			{
				SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
				soundPlayClipArgs.m_templateSource = source;
				soundPlayClipArgs.m_def = soundDef;
				if (!m_VolumeScale.IsNone)
				{
					soundPlayClipArgs.m_volume = m_VolumeScale.Value;
				}
				soundPlayClipArgs.m_parentObject = source.gameObject;
				if (SoundManager.Get() != null)
				{
					SoundManager.Get().PlayClip(soundPlayClipArgs, createNewSource: true, options);
				}
			}
			base.Fsm.Event(m_FinishedEvent);
			Finish();
		}

		private IEnumerator Delay()
		{
			while (m_DelayTime > 0f)
			{
				m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			Play();
		}

		protected Actor GetActor()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				Card card = SceneUtils.FindComponentInThisOrParents<Card>(base.Owner);
				if (card != null)
				{
					actor = card.GetActor();
				}
			}
			return actor;
		}
	}
}
