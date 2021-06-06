using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F17 RID: 3863
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Fades out a looping audio clip.")]
	public class AudioLoopingEndAction : FsmStateAction
	{
		// Token: 0x0600ABD9 RID: 43993 RVA: 0x0035A3C8 File Offset: 0x003585C8
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_FadeOutTime = 0f;
			this.m_stop = true;
		}

		// Token: 0x0600ABDA RID: 43994 RVA: 0x0035A3F0 File Offset: 0x003585F0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this.m_audioSource = ownerDefaultTarget.GetComponent<AudioSource>();
			if (this.m_FadeOutTime.Value <= Mathf.Epsilon)
			{
				base.Finish();
				return;
			}
			this.m_startVolume = SoundManager.Get().GetVolume(this.m_audioSource);
			this.m_startTime = FsmTime.RealtimeSinceStartup;
			this.m_currentTime = this.m_startTime;
			this.m_endTime = this.m_startTime + this.m_FadeOutTime.Value;
		}

		// Token: 0x0600ABDB RID: 43995 RVA: 0x0035A48C File Offset: 0x0035868C
		public override void OnUpdate()
		{
			this.m_currentTime += Time.deltaTime;
			if (this.m_currentTime < this.m_endTime)
			{
				float num = (this.m_currentTime - this.m_startTime) / this.m_FadeOutTime.Value;
				float volume = Mathf.Lerp(this.m_startVolume, 0f, num);
				this.m_progress = num;
				SoundManager.Get().SetVolume(this.m_audioSource, volume);
				return;
			}
			SoundManager.Get().SetVolume(this.m_audioSource, 0f);
			if (this.m_stop.Value)
			{
				SoundManager.Get().Stop(this.m_audioSource);
			}
			base.Finish();
		}

		// Token: 0x0600ABDC RID: 43996 RVA: 0x0035A537 File Offset: 0x00358737
		public override float GetProgress()
		{
			return this.m_progress;
		}

		// Token: 0x04009290 RID: 37520
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009291 RID: 37521
		public FsmFloat m_FadeOutTime;

		// Token: 0x04009292 RID: 37522
		public FsmBool m_stop;

		// Token: 0x04009293 RID: 37523
		private AudioSource m_audioSource;

		// Token: 0x04009294 RID: 37524
		private float m_startVolume;

		// Token: 0x04009295 RID: 37525
		private float m_startTime;

		// Token: 0x04009296 RID: 37526
		private float m_currentTime;

		// Token: 0x04009297 RID: 37527
		private float m_endTime;

		// Token: 0x04009298 RID: 37528
		private float m_progress;
	}
}
