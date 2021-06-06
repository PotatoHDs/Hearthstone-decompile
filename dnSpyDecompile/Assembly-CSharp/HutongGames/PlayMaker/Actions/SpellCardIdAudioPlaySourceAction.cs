using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F83 RID: 3971
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays an audio source dependent on a Spell's Card's ID.")]
	public class SpellCardIdAudioPlaySourceAction : SpellCardIdAudioAction
	{
		// Token: 0x0600AD9F RID: 44447 RVA: 0x00361BE7 File Offset: 0x0035FDE7
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_SpellObject);
		}

		// Token: 0x0600ADA0 RID: 44448 RVA: 0x00361BFC File Offset: 0x0035FDFC
		public override void Reset()
		{
			this.m_SpellObject = null;
			this.m_WhichCard = SpellAction.Which.SOURCE;
			this.m_CardIds = new string[2];
			this.m_Sources = new FsmGameObject[2];
			this.m_DefaultSource = null;
			this.m_PickedSource = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_VolumeScale = 1f;
			this.m_WaitForFinish = false;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
		}

		// Token: 0x0600ADA1 RID: 44449 RVA: 0x00361C7A File Offset: 0x0035FE7A
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

		// Token: 0x0600ADA2 RID: 44450 RVA: 0x00361CAF File Offset: 0x0035FEAF
		public override void OnExit()
		{
			if (!this.m_VolumeScale.IsNone && this.m_originalVolume != null)
			{
				SoundManager.Get().SetVolume(this.m_source, this.m_originalVolume.Value);
			}
		}

		// Token: 0x0600ADA3 RID: 44451 RVA: 0x00361CE6 File Offset: 0x0035FEE6
		public override void OnUpdate()
		{
			if (this.m_WaitForFinish.Value && SoundManager.Get().IsActive(this.m_source))
			{
				return;
			}
			base.Finish();
		}

		// Token: 0x0600ADA4 RID: 44452 RVA: 0x00361D10 File Offset: 0x0035FF10
		private void Play()
		{
			this.m_source = base.GetSourceMatchingCardId(this.m_WhichCard, this.m_CardIds, this.m_Sources, this.m_DefaultSource);
			if (this.m_source == null)
			{
				base.Finish();
				return;
			}
			if (!this.m_PickedSource.IsNone)
			{
				this.m_PickedSource.Value = this.m_source.gameObject;
			}
			if (!this.m_VolumeScale.IsNone)
			{
				SoundManager.Get().SetVolume(this.m_source, this.m_VolumeScale.Value);
				this.m_originalVolume = new float?(this.m_VolumeScale.Value);
			}
			SoundManager.Get().Play(this.m_source, null, null, null);
			if (!this.m_WaitForFinish.Value || !SoundManager.Get().IsActive(this.m_source))
			{
				base.Finish();
			}
		}

		// Token: 0x0600ADA5 RID: 44453 RVA: 0x00361DF0 File Offset: 0x0035FFF0
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

		// Token: 0x0400947A RID: 38010
		public FsmOwnerDefault m_SpellObject;

		// Token: 0x0400947B RID: 38011
		[Tooltip("Which Card to check on the Spell.")]
		public SpellAction.Which m_WhichCard;

		// Token: 0x0400947C RID: 38012
		[RequiredField]
		[CompoundArray("Sources", "Card Id", "Source")]
		public string[] m_CardIds;

		// Token: 0x0400947D RID: 38013
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject[] m_Sources;

		// Token: 0x0400947E RID: 38014
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject m_DefaultSource;

		// Token: 0x0400947F RID: 38015
		[Tooltip("Optional. The source that gets picked will be put into this variable so you can track it.")]
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject m_PickedSource;

		// Token: 0x04009480 RID: 38016
		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		// Token: 0x04009481 RID: 38017
		[Tooltip("Wait for the Audio Source to finish playing before moving on.")]
		public FsmBool m_WaitForFinish;

		// Token: 0x04009482 RID: 38018
		public float m_Delay;

		// Token: 0x04009483 RID: 38019
		private float m_DelayTime;

		// Token: 0x04009484 RID: 38020
		private AudioSource m_source;

		// Token: 0x04009485 RID: 38021
		private float? m_originalVolume;
	}
}
