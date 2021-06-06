using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F18 RID: 3864
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays the Audio Clip on a Game Object and sets it to loop.")]
	public class AudioLoopingStartAction : FsmStateAction
	{
		// Token: 0x0600ABDE RID: 43998 RVA: 0x0035A53F File Offset: 0x0035873F
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_FadeInTime = 0f;
			this.m_TargetVolume = 0f;
			this.m_randomizeStartPoint = false;
		}

		// Token: 0x0600ABDF RID: 43999 RVA: 0x0035A574 File Offset: 0x00358774
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this.m_soundDef = ownerDefaultTarget.GetComponent<SoundDef>();
			if (this.m_soundDef == null)
			{
				base.Finish();
				return;
			}
			if (this.m_FadeInTime.Value <= Mathf.Epsilon)
			{
				base.Finish();
				return;
			}
			SoundManager soundManager = SoundManager.Get();
			this.m_audioSource = ownerDefaultTarget.GetComponent<AudioSource>();
			this.m_audioSource.loop = true;
			this.m_audioSource.enabled = true;
			this.m_startVolume = soundManager.GetVolume(this.m_audioSource);
			this.m_startTime = FsmTime.RealtimeSinceStartup;
			this.m_currentTime = this.m_startTime;
			this.m_endTime = this.m_startTime + this.m_FadeInTime.Value;
			if (this.m_randomizeStartPoint.Value)
			{
				this.m_audioSource.time = UnityEngine.Random.value * this.m_audioSource.clip.length;
			}
			SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
			soundPlayClipArgs.m_def = this.m_soundDef;
			soundPlayClipArgs.m_templateSource = this.m_audioSource;
			SoundManager.Get().PlayClip(soundPlayClipArgs, false, null);
		}

		// Token: 0x0600ABE0 RID: 44000 RVA: 0x0035A6A4 File Offset: 0x003588A4
		public override void OnUpdate()
		{
			this.m_currentTime += Time.deltaTime;
			if (this.m_currentTime < this.m_endTime)
			{
				float num = (this.m_currentTime - this.m_startTime) / this.m_FadeInTime.Value;
				float volume = Mathf.Lerp(this.m_startVolume, this.m_TargetVolume.Value, num);
				this.m_progress = num;
				SoundManager.Get().SetVolume(this.m_audioSource, volume);
				return;
			}
			SoundManager.Get().SetVolume(this.m_audioSource, this.m_TargetVolume.Value);
			base.Finish();
		}

		// Token: 0x0600ABE1 RID: 44001 RVA: 0x0035A73D File Offset: 0x0035893D
		public override float GetProgress()
		{
			return this.m_progress;
		}

		// Token: 0x04009299 RID: 37529
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[CheckForComponent(typeof(SoundDef))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x0400929A RID: 37530
		public FsmFloat m_FadeInTime;

		// Token: 0x0400929B RID: 37531
		public FsmFloat m_TargetVolume;

		// Token: 0x0400929C RID: 37532
		public FsmBool m_randomizeStartPoint;

		// Token: 0x0400929D RID: 37533
		private SoundDef m_soundDef;

		// Token: 0x0400929E RID: 37534
		private AudioSource m_audioSource;

		// Token: 0x0400929F RID: 37535
		private float m_startVolume;

		// Token: 0x040092A0 RID: 37536
		private float m_startTime;

		// Token: 0x040092A1 RID: 37537
		private float m_currentTime;

		// Token: 0x040092A2 RID: 37538
		private float m_endTime;

		// Token: 0x040092A3 RID: 37539
		private float m_progress;
	}
}
