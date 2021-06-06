using System;
using System.Collections;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F82 RID: 3970
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Generates an AudioSource based on a template, then plays an audio clip dependent on a Spell's Card's ID.")]
	public class SpellCardIdAudioPlayClipAction : SpellCardIdAudioAction
	{
		// Token: 0x0600AD99 RID: 44441 RVA: 0x00361A3B File Offset: 0x0035FC3B
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600AD9A RID: 44442 RVA: 0x00361A50 File Offset: 0x0035FC50
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_TemplateSource = null;
			this.m_CardIds = new string[2];
			this.m_Sounds = new SoundDef[2];
			this.m_DefaultSound = null;
			this.m_Volume = 1f;
			this.m_Pitch = 1f;
			this.m_Category = Global.SoundCategory.FX;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
		}

		// Token: 0x0600AD9B RID: 44443 RVA: 0x00361ACE File Offset: 0x0035FCCE
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_Delay > 0f)
			{
				this.m_DelayTime = this.m_Delay;
				base.StartCoroutine(this.Delay());
				return;
			}
			this.Play();
		}

		// Token: 0x0600AD9C RID: 44444 RVA: 0x00361B04 File Offset: 0x0035FD04
		private void Play()
		{
			SoundDef clipMatchingCardId = base.GetClipMatchingCardId(this.m_WhichCard, this.m_CardIds, this.m_Sounds, this.m_DefaultSound);
			if (clipMatchingCardId == null)
			{
				base.Finish();
				return;
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_templateSource = this.m_TemplateSource;
			soundPlayClipArgs.m_def = clipMatchingCardId;
			if (!this.m_Volume.IsNone)
			{
				soundPlayClipArgs.m_volume = new float?(this.m_Volume.Value);
			}
			if (!this.m_Pitch.IsNone)
			{
				soundPlayClipArgs.m_pitch = new float?(this.m_Pitch.Value);
			}
			if (this.m_Category != Global.SoundCategory.NONE)
			{
				soundPlayClipArgs.m_category = new Global.SoundCategory?(this.m_Category);
			}
			soundPlayClipArgs.m_parentObject = base.Owner;
			SoundManager.Get().PlayClip(soundPlayClipArgs, true, null);
			base.Finish();
		}

		// Token: 0x0600AD9D RID: 44445 RVA: 0x00361BD8 File Offset: 0x0035FDD8
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

		// Token: 0x0400946F RID: 37999
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009470 RID: 38000
		[Tooltip("Which Card to check on the Spell.")]
		public SpellAction.Which m_WhichCard;

		// Token: 0x04009471 RID: 38001
		[Tooltip("If specified, this Audio Source will be used as a template for the generated Audio Source, otherwise the one in the SoundConfig will be the template.")]
		public AudioSource m_TemplateSource;

		// Token: 0x04009472 RID: 38002
		[CompoundArray("Sounds", "Card Id", "Sound")]
		public string[] m_CardIds;

		// Token: 0x04009473 RID: 38003
		public SoundDef[] m_Sounds;

		// Token: 0x04009474 RID: 38004
		public SoundDef m_DefaultSound;

		// Token: 0x04009475 RID: 38005
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Volume;

		// Token: 0x04009476 RID: 38006
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_Pitch;

		// Token: 0x04009477 RID: 38007
		[Tooltip("If you want the template Category the Category, change this so that it's not NONE.")]
		public Global.SoundCategory m_Category;

		// Token: 0x04009478 RID: 38008
		public float m_Delay;

		// Token: 0x04009479 RID: 38009
		private float m_DelayTime;
	}
}
