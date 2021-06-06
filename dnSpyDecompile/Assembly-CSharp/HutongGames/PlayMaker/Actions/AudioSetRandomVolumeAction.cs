using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F22 RID: 3874
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Randomly sets the volume of an AudioSource on a Game Object.")]
	public class AudioSetRandomVolumeAction : FsmStateAction
	{
		// Token: 0x0600AC12 RID: 44050 RVA: 0x0035B343 File Offset: 0x00359543
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_MinVolume = 1f;
			this.m_MaxVolume = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AC13 RID: 44051 RVA: 0x0035B373 File Offset: 0x00359573
		public override void OnEnter()
		{
			this.UpdateVolume();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC14 RID: 44052 RVA: 0x0035B389 File Offset: 0x00359589
		public override void OnUpdate()
		{
			this.UpdateVolume();
		}

		// Token: 0x0600AC15 RID: 44053 RVA: 0x0035B394 File Offset: 0x00359594
		private void ChooseVolume()
		{
			float min = this.m_MinVolume.IsNone ? 1f : this.m_MinVolume.Value;
			float max = this.m_MaxVolume.IsNone ? 1f : this.m_MaxVolume.Value;
			this.m_volume = UnityEngine.Random.Range(min, max);
		}

		// Token: 0x0600AC16 RID: 44054 RVA: 0x0035B3F0 File Offset: 0x003595F0
		private void UpdateVolume()
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
			SoundManager.Get().SetVolume(component, this.m_volume);
		}

		// Token: 0x040092DE RID: 37598
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092DF RID: 37599
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MinVolume;

		// Token: 0x040092E0 RID: 37600
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MaxVolume;

		// Token: 0x040092E1 RID: 37601
		public bool m_EveryFrame;

		// Token: 0x040092E2 RID: 37602
		private float m_volume;
	}
}
