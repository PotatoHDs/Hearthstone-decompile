using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F23 RID: 3875
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets the volume of an AudioSource on a Game Object.")]
	public class AudioSetVolumeAction : FsmStateAction
	{
		// Token: 0x0600AC18 RID: 44056 RVA: 0x0035B43B File Offset: 0x0035963B
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Volume = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AC19 RID: 44057 RVA: 0x0035B45B File Offset: 0x0035965B
		public override void OnEnter()
		{
			this.UpdateVolume();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC1A RID: 44058 RVA: 0x0035B471 File Offset: 0x00359671
		public override void OnUpdate()
		{
			this.UpdateVolume();
		}

		// Token: 0x0600AC1B RID: 44059 RVA: 0x0035B47C File Offset: 0x0035967C
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
			if (this.m_Volume.IsNone)
			{
				return;
			}
			SoundManager.Get().SetVolume(component, this.m_Volume.Value);
		}

		// Token: 0x040092E3 RID: 37603
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092E4 RID: 37604
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Volume;

		// Token: 0x040092E5 RID: 37605
		public bool m_EveryFrame;
	}
}
