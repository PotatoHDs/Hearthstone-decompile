using System;
using System.Collections;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1C RID: 3868
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays a random AudioClip. An AudioSource for the clip is created automatically based on the parameters.")]
	public class AudioPlayRandomClipAction : FsmStateAction
	{
		// Token: 0x0600ABF2 RID: 44018 RVA: 0x0035AB80 File Offset: 0x00358D80
		public override void Reset()
		{
			this.m_ParentObject = null;
			this.m_Sounds = new SoundDef[3];
			this.m_Weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.m_MinVolume = 1f;
			this.m_MaxVolume = 1f;
			this.m_MinPitch = 1f;
			this.m_MaxPitch = 1f;
			this.m_SpatialBlend = 0f;
			this.m_Category = Global.SoundCategory.FX;
			this.m_TemplateSource = null;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
		}

		// Token: 0x0600ABF3 RID: 44019 RVA: 0x0035AC47 File Offset: 0x00358E47
		public override void OnEnter()
		{
			if (this.m_Delay > 0f)
			{
				this.m_DelayTime = this.m_Delay;
				base.StartCoroutine(this.Delay());
				return;
			}
			this.Play();
		}

		// Token: 0x0600ABF4 RID: 44020 RVA: 0x0035AC76 File Offset: 0x00358E76
		private SoundDef ChooseClip()
		{
			if (this.m_Weights == null || this.m_Weights.Length == 0)
			{
				return this.m_Sounds[0];
			}
			return this.m_Sounds[ActionHelpers.GetRandomWeightedIndex(this.m_Weights)];
		}

		// Token: 0x0600ABF5 RID: 44021 RVA: 0x0035ACA4 File Offset: 0x00358EA4
		private float ChooseVolume()
		{
			float num = this.m_MinVolume.IsNone ? 1f : this.m_MinVolume.Value;
			float num2 = this.m_MaxVolume.IsNone ? 1f : this.m_MaxVolume.Value;
			if (Mathf.Approximately(num, num2))
			{
				return num;
			}
			return UnityEngine.Random.Range(num, num2);
		}

		// Token: 0x0600ABF6 RID: 44022 RVA: 0x0035AD04 File Offset: 0x00358F04
		private float ChoosePitch()
		{
			float num = this.m_MinPitch.IsNone ? 1f : this.m_MinPitch.Value;
			float num2 = this.m_MaxPitch.IsNone ? 1f : this.m_MaxPitch.Value;
			if (Mathf.Approximately(num, num2))
			{
				return num;
			}
			return UnityEngine.Random.Range(num, num2);
		}

		// Token: 0x0600ABF7 RID: 44023 RVA: 0x0035AD64 File Offset: 0x00358F64
		private void Play()
		{
			if (this.m_Sounds == null || this.m_Sounds.Length == 0)
			{
				base.Finish();
				return;
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_templateSource = this.m_TemplateSource;
			soundPlayClipArgs.m_def = this.ChooseClip();
			soundPlayClipArgs.m_volume = new float?(this.ChooseVolume());
			soundPlayClipArgs.m_pitch = new float?(this.ChoosePitch());
			if (!this.m_SpatialBlend.IsNone)
			{
				soundPlayClipArgs.m_spatialBlend = new float?(this.m_SpatialBlend.Value);
			}
			if (this.m_Category != Global.SoundCategory.NONE)
			{
				soundPlayClipArgs.m_category = new Global.SoundCategory?(this.m_Category);
			}
			soundPlayClipArgs.m_parentObject = base.Fsm.GetOwnerDefaultTarget(this.m_ParentObject);
			SoundManager.SoundOptions options = new SoundManager.SoundOptions
			{
				InstanceLimited = this.m_InstanceLimited,
				InstanceTimeLimit = this.m_InstanceLimitedDuration,
				MaxInstancesOfThisSound = this.m_InstanceLimitMaximum
			};
			SoundManager.Get().PlayClip(soundPlayClipArgs, true, options);
			base.Finish();
		}

		// Token: 0x0600ABF8 RID: 44024 RVA: 0x0035AE5A File Offset: 0x0035905A
		private IEnumerator Delay()
		{
			while (this.m_DelayTime > 0f)
			{
				this.m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			this.Play();
			yield break;
		}

		// Token: 0x040092B8 RID: 37560
		[Tooltip("Optional. If specified, the generated Audio Source will use the same transform as this object.")]
		public FsmOwnerDefault m_ParentObject;

		// Token: 0x040092B9 RID: 37561
		[RequiredField]
		[CompoundArray("Sounds", "SoundDef", "Weight")]
		public SoundDef[] m_Sounds;

		// Token: 0x040092BA RID: 37562
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] m_Weights;

		// Token: 0x040092BB RID: 37563
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MinVolume;

		// Token: 0x040092BC RID: 37564
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MaxVolume;

		// Token: 0x040092BD RID: 37565
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MinPitch;

		// Token: 0x040092BE RID: 37566
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MaxPitch;

		// Token: 0x040092BF RID: 37567
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_SpatialBlend;

		// Token: 0x040092C0 RID: 37568
		public Global.SoundCategory m_Category;

		// Token: 0x040092C1 RID: 37569
		public float m_Delay;

		// Token: 0x040092C2 RID: 37570
		[Tooltip("If specified, this Audio Source will be used as a template for the generated Audio Source, otherwise the one in the SoundConfig will be the template.")]
		public AudioSource m_TemplateSource;

		// Token: 0x040092C3 RID: 37571
		[Tooltip("If true, there will be a limit to the number instances of of this sound that can play at once.")]
		public bool m_InstanceLimited;

		// Token: 0x040092C4 RID: 37572
		[Tooltip("If instance limited, this defines the duration that each clip will prevent another from playing.  If zero, then it will measure the length of the audio file.")]
		public float m_InstanceLimitedDuration;

		// Token: 0x040092C5 RID: 37573
		[Tooltip("If instance limited, this defines the maximum number of instances of the sound that can be playing at once.")]
		public int m_InstanceLimitMaximum = 1;

		// Token: 0x040092C6 RID: 37574
		private float m_DelayTime;
	}
}
