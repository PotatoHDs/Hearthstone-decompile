using System;
using System.Collections;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1B RID: 3867
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Generates an AudioSource based on a template, then plays that source.")]
	public class AudioPlayClipAction : FsmStateAction
	{
		// Token: 0x0600ABEC RID: 44012 RVA: 0x0035A92C File Offset: 0x00358B2C
		public override void Reset()
		{
			this.m_ParentObject = null;
			this.m_Sound = null;
			this.m_Volume = 1f;
			this.m_Pitch = 1f;
			this.m_SpatialBlend = 0f;
			this.m_Category = Global.SoundCategory.FX;
			this.m_TemplateSource = null;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
		}

		// Token: 0x0600ABED RID: 44013 RVA: 0x0035A99B File Offset: 0x00358B9B
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

		// Token: 0x0600ABEE RID: 44014 RVA: 0x0035A9CC File Offset: 0x00358BCC
		private void Play()
		{
			if (this.m_Sound == null)
			{
				base.Finish();
				return;
			}
			if (this.m_IsMinionSummonVO)
			{
				Actor actor = this.GetActor();
				if (actor != null && actor.GetEntity() != null && actor.GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO))
				{
					base.Finish();
					return;
				}
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_templateSource = this.m_TemplateSource;
			soundPlayClipArgs.m_def = (this.m_Sound.Value as SoundDef);
			if (!this.m_Volume.IsNone)
			{
				soundPlayClipArgs.m_volume = new float?(this.m_Volume.Value);
			}
			if (!this.m_Pitch.IsNone)
			{
				soundPlayClipArgs.m_pitch = new float?(this.m_Pitch.Value);
			}
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

		// Token: 0x0600ABEF RID: 44015 RVA: 0x0035AB20 File Offset: 0x00358D20
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

		// Token: 0x0600ABF0 RID: 44016 RVA: 0x0035AB30 File Offset: 0x00358D30
		protected Actor GetActor()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				global::Card card = SceneUtils.FindComponentInThisOrParents<global::Card>(base.Owner);
				if (card != null)
				{
					actor = card.GetActor();
				}
			}
			return actor;
		}

		// Token: 0x040092AB RID: 37547
		[Tooltip("Optional. If specified, the generated Audio Source will be placed at the same location as this object.")]
		public FsmOwnerDefault m_ParentObject;

		// Token: 0x040092AC RID: 37548
		[RequiredField]
		[ObjectType(typeof(SoundDef))]
		public FsmObject m_Sound;

		// Token: 0x040092AD RID: 37549
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Volume;

		// Token: 0x040092AE RID: 37550
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_Pitch;

		// Token: 0x040092AF RID: 37551
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_SpatialBlend;

		// Token: 0x040092B0 RID: 37552
		public Global.SoundCategory m_Category;

		// Token: 0x040092B1 RID: 37553
		public float m_Delay;

		// Token: 0x040092B2 RID: 37554
		[Tooltip("If specified, this Audio Source will be used as a template for the generated Audio Source, otherwise the one in the SoundConfig will be the template.")]
		public AudioSource m_TemplateSource;

		// Token: 0x040092B3 RID: 37555
		[Tooltip("If true, this audio clip will be suppressed by SUPPRESS_ALL_SUMMON_VO")]
		public bool m_IsMinionSummonVO;

		// Token: 0x040092B4 RID: 37556
		[Tooltip("If true, there will be a limit to the number instances of of this sound that can play at once.")]
		public bool m_InstanceLimited;

		// Token: 0x040092B5 RID: 37557
		[Tooltip("If instance limited, this defines the duration that each clip will prevent another from playing.  If zero, then it will measure the length of the audio file.")]
		public float m_InstanceLimitedDuration;

		// Token: 0x040092B6 RID: 37558
		[Tooltip("If instance limited, this defines the maximum number of instances of the sound that can be playing at once.")]
		public int m_InstanceLimitMaximum = 1;

		// Token: 0x040092B7 RID: 37559
		private float m_DelayTime;
	}
}
