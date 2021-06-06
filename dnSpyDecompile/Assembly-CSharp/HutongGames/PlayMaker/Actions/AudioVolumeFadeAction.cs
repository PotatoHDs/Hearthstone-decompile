using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F28 RID: 3880
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Fades an Audio Source component's volume towards a target value.")]
	public class AudioVolumeFadeAction : FsmStateAction
	{
		// Token: 0x0600AC29 RID: 44073 RVA: 0x0035B5F8 File Offset: 0x003597F8
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_FadeTime = 1f;
			this.m_TargetVolume = 0f;
			this.m_StopWhenDone = true;
			this.m_RealTime = false;
		}

		// Token: 0x0600AC2A RID: 44074 RVA: 0x0035B644 File Offset: 0x00359844
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this.m_audio = ownerDefaultTarget.GetComponent<AudioSource>();
			if (this.m_audio == null)
			{
				base.Finish();
				return;
			}
			if (this.m_FadeTime.Value <= Mathf.Epsilon)
			{
				base.Finish();
				return;
			}
			this.m_startVolume = SoundManager.Get().GetVolume(this.m_audio);
			this.m_startTime = FsmTime.RealtimeSinceStartup;
			this.m_currentTime = this.m_startTime;
			this.m_endTime = this.m_startTime + this.m_FadeTime.Value;
			SoundManager.Get().SetVolume(this.m_audio, this.m_startVolume);
		}

		// Token: 0x0600AC2B RID: 44075 RVA: 0x0035B708 File Offset: 0x00359908
		public override void OnUpdate()
		{
			if (this.m_RealTime.Value)
			{
				this.m_currentTime = FsmTime.RealtimeSinceStartup - this.m_startTime;
			}
			else
			{
				this.m_currentTime += Time.deltaTime;
			}
			if (this.m_currentTime < this.m_endTime)
			{
				float t = (this.m_currentTime - this.m_startTime) / this.m_FadeTime.Value;
				float volume = Mathf.Lerp(this.m_startVolume, this.m_TargetVolume.Value, t);
				SoundManager.Get().SetVolume(this.m_audio, volume);
				return;
			}
			SoundManager.Get().SetVolume(this.m_audio, this.m_TargetVolume.Value);
			if (this.m_StopWhenDone.Value)
			{
				SoundManager.Get().Stop(this.m_audio);
			}
			base.Finish();
		}

		// Token: 0x040092EB RID: 37611
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092EC RID: 37612
		[RequiredField]
		public FsmFloat m_FadeTime;

		// Token: 0x040092ED RID: 37613
		[RequiredField]
		public FsmFloat m_TargetVolume;

		// Token: 0x040092EE RID: 37614
		[Tooltip("Stop the audio source when the target volume is reached.")]
		public FsmBool m_StopWhenDone;

		// Token: 0x040092EF RID: 37615
		[Tooltip("Use real time. Useful if time is scaled and you don't want this action to scale.")]
		public FsmBool m_RealTime;

		// Token: 0x040092F0 RID: 37616
		private AudioSource m_audio;

		// Token: 0x040092F1 RID: 37617
		private float m_startVolume;

		// Token: 0x040092F2 RID: 37618
		private float m_startTime;

		// Token: 0x040092F3 RID: 37619
		private float m_currentTime;

		// Token: 0x040092F4 RID: 37620
		private float m_endTime;
	}
}
