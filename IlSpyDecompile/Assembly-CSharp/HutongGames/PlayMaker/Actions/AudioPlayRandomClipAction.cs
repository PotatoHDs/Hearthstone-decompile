using System.Collections;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays a random AudioClip. An AudioSource for the clip is created automatically based on the parameters.")]
	public class AudioPlayRandomClipAction : FsmStateAction
	{
		[Tooltip("Optional. If specified, the generated Audio Source will use the same transform as this object.")]
		public FsmOwnerDefault m_ParentObject;

		[RequiredField]
		[CompoundArray("Sounds", "SoundDef", "Weight")]
		public SoundDef[] m_Sounds;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] m_Weights;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MinVolume;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MaxVolume;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MinPitch;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MaxPitch;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_SpatialBlend;

		public Global.SoundCategory m_Category;

		public float m_Delay;

		[Tooltip("If specified, this Audio Source will be used as a template for the generated Audio Source, otherwise the one in the SoundConfig will be the template.")]
		public AudioSource m_TemplateSource;

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
			m_Sounds = new SoundDef[3];
			m_Weights = new FsmFloat[3] { 1f, 1f, 1f };
			m_MinVolume = 1f;
			m_MaxVolume = 1f;
			m_MinPitch = 1f;
			m_MaxPitch = 1f;
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

		private SoundDef ChooseClip()
		{
			if (m_Weights == null || m_Weights.Length == 0)
			{
				return m_Sounds[0];
			}
			return m_Sounds[ActionHelpers.GetRandomWeightedIndex(m_Weights)];
		}

		private float ChooseVolume()
		{
			float num = (m_MinVolume.IsNone ? 1f : m_MinVolume.Value);
			float num2 = (m_MaxVolume.IsNone ? 1f : m_MaxVolume.Value);
			if (Mathf.Approximately(num, num2))
			{
				return num;
			}
			return Random.Range(num, num2);
		}

		private float ChoosePitch()
		{
			float num = (m_MinPitch.IsNone ? 1f : m_MinPitch.Value);
			float num2 = (m_MaxPitch.IsNone ? 1f : m_MaxPitch.Value);
			if (Mathf.Approximately(num, num2))
			{
				return num;
			}
			return Random.Range(num, num2);
		}

		private void Play()
		{
			if (m_Sounds == null || m_Sounds.Length == 0)
			{
				Finish();
				return;
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_templateSource = m_TemplateSource;
			soundPlayClipArgs.m_def = ChooseClip();
			soundPlayClipArgs.m_volume = ChooseVolume();
			soundPlayClipArgs.m_pitch = ChoosePitch();
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
	}
}
