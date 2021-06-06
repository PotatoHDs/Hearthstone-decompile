using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F21 RID: 3873
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Randomly sets the pitch of an AudioSource on a Game Object.")]
	public class AudioSetRandomPitchAction : FsmStateAction
	{
		// Token: 0x0600AC0C RID: 44044 RVA: 0x0035B246 File Offset: 0x00359446
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_MinPitch = 1f;
			this.m_MaxPitch = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AC0D RID: 44045 RVA: 0x0035B276 File Offset: 0x00359476
		public override void OnEnter()
		{
			this.ChoosePitch();
			this.UpdatePitch();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC0E RID: 44046 RVA: 0x0035B292 File Offset: 0x00359492
		public override void OnUpdate()
		{
			this.UpdatePitch();
		}

		// Token: 0x0600AC0F RID: 44047 RVA: 0x0035B29C File Offset: 0x0035949C
		private void ChoosePitch()
		{
			float min = this.m_MinPitch.IsNone ? 1f : this.m_MinPitch.Value;
			float max = this.m_MaxPitch.IsNone ? 1f : this.m_MaxPitch.Value;
			this.m_pitch = UnityEngine.Random.Range(min, max);
		}

		// Token: 0x0600AC10 RID: 44048 RVA: 0x0035B2F8 File Offset: 0x003594F8
		private void UpdatePitch()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
			if (component == null)
			{
				return;
			}
			SoundManager.Get().SetPitch(component, this.m_pitch);
		}

		// Token: 0x040092D9 RID: 37593
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092DA RID: 37594
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MinPitch;

		// Token: 0x040092DB RID: 37595
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MaxPitch;

		// Token: 0x040092DC RID: 37596
		public bool m_EveryFrame;

		// Token: 0x040092DD RID: 37597
		private float m_pitch;
	}
}
