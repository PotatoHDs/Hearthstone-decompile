using System.Collections;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Generates an AudioSource based on a template, then plays that source.")]
	public class AudioPlayClipAction : FsmStateAction
	{
		[Tooltip("Optional. If specified, the generated Audio Source will be placed at the same location as this object.")]
		public FsmOwnerDefault m_ParentObject;

		[RequiredField]
		[ObjectType(typeof(SoundDef))]
		public FsmObject m_Sound;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Volume;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_Pitch;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_SpatialBlend;

		public Global.SoundCategory m_Category;

		public float m_Delay;

		[Tooltip("If specified, this Audio Source will be used as a template for the generated Audio Source, otherwise the one in the SoundConfig will be the template.")]
		public AudioSource m_TemplateSource;

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
			m_ParentObject = null;
			m_Sound = null;
			m_Volume = 1f;
			m_Pitch = 1f;
			m_SpatialBlend = 0f;
			m_Category = Global.SoundCategory.FX;
			m_TemplateSource = null;
			m_Delay = 0f;
			m_DelayTime = 0f;
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

		private void Play()
		{
			if (m_Sound == null)
			{
				Finish();
				return;
			}
			if (m_IsMinionSummonVO)
			{
				Actor actor = GetActor();
				if (actor != null && actor.GetEntity() != null && actor.GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO))
				{
					Finish();
					return;
				}
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_templateSource = m_TemplateSource;
			soundPlayClipArgs.m_def = m_Sound.Value as SoundDef;
			if (!m_Volume.IsNone)
			{
				soundPlayClipArgs.m_volume = m_Volume.Value;
			}
			if (!m_Pitch.IsNone)
			{
				soundPlayClipArgs.m_pitch = m_Pitch.Value;
			}
			if (!m_SpatialBlend.IsNone)
			{
				soundPlayClipArgs.m_spatialBlend = m_SpatialBlend.Value;
			}
			if (m_Category != 0)
			{
				soundPlayClipArgs.m_category = m_Category;
			}
			soundPlayClipArgs.m_parentObject = base.Fsm.GetOwnerDefaultTarget(m_ParentObject);
			SoundManager.SoundOptions options = new SoundManager.SoundOptions
			{
				InstanceLimited = m_InstanceLimited,
				InstanceTimeLimit = m_InstanceLimitedDuration,
				MaxInstancesOfThisSound = m_InstanceLimitMaximum
			};
			SoundManager.Get().PlayClip(soundPlayClipArgs, createNewSource: true, options);
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
