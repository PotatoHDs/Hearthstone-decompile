using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F81 RID: 3969
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays an audio clip dependent on a Spell's Card's ID.")]
	public class SpellCardIdAudioPlayAction : SpellCardIdAudioAction
	{
		// Token: 0x0600AD93 RID: 44435 RVA: 0x003618C8 File Offset: 0x0035FAC8
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600AD94 RID: 44436 RVA: 0x003618DC File Offset: 0x0035FADC
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_AudioSourceObject = null;
			this.m_CardIds = new string[2];
			this.m_Sounds = new SoundDef[2];
			this.m_DefaultSound = null;
			this.m_VolumeScale = 1f;
			this.m_WaitForFinish = false;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
		}

		// Token: 0x0600AD95 RID: 44437 RVA: 0x0036194F File Offset: 0x0035FB4F
		public override void OnEnter()
		{
			base.OnEnter();
			this.m_DelayTime = this.m_Delay;
			base.StartCoroutine(this.Delay());
		}

		// Token: 0x0600AD96 RID: 44438 RVA: 0x00361970 File Offset: 0x0035FB70
		private void Play()
		{
			AudioSource audioSource = base.GetAudioSource(this.m_AudioSourceObject);
			if (audioSource == null)
			{
				base.Finish();
				return;
			}
			SoundDef clipMatchingCardId = base.GetClipMatchingCardId(this.m_WhichCard, this.m_CardIds, this.m_Sounds, this.m_DefaultSound);
			if (clipMatchingCardId == null)
			{
				base.Finish();
				return;
			}
			if (this.m_VolumeScale.IsNone)
			{
				SoundManager.Get().PlayOneShot(audioSource, clipMatchingCardId, 1f, null);
			}
			else
			{
				SoundManager.Get().PlayOneShot(audioSource, clipMatchingCardId, this.m_VolumeScale.Value, null);
			}
			if (!this.m_WaitForFinish.Value || !SoundManager.Get().IsActive(audioSource))
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD97 RID: 44439 RVA: 0x00361A24 File Offset: 0x0035FC24
		private IEnumerator Delay()
		{
			while (this.m_DelayTime > 0f)
			{
				this.m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			this.Play();
			AudioSource source = base.GetAudioSource(this.m_AudioSourceObject);
			while (this.m_WaitForFinish.Value && SoundManager.Get().IsActive(source))
			{
				yield return null;
			}
			base.Finish();
			yield break;
		}

		// Token: 0x04009465 RID: 37989
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x04009466 RID: 37990
		[Tooltip("Which Card to check on the Spell.")]
		public SpellAction.Which m_WhichCard;

		// Token: 0x04009467 RID: 37991
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault m_AudioSourceObject;

		// Token: 0x04009468 RID: 37992
		[RequiredField]
		[CompoundArray("Sounds", "Card Id", "Sound")]
		public string[] m_CardIds;

		// Token: 0x04009469 RID: 37993
		public SoundDef[] m_Sounds;

		// Token: 0x0400946A RID: 37994
		public SoundDef m_DefaultSound;

		// Token: 0x0400946B RID: 37995
		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		// Token: 0x0400946C RID: 37996
		public float m_Delay;

		// Token: 0x0400946D RID: 37997
		[Tooltip("Wait for the Audio Source to finish playing before moving on.")]
		public FsmBool m_WaitForFinish;

		// Token: 0x0400946E RID: 37998
		private float m_DelayTime;
	}
}
