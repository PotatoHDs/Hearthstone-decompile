using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F20 RID: 3872
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets the pitch of an AudioSource on a Game Object.")]
	public class AudioSetPitchAction : FsmStateAction
	{
		// Token: 0x0600AC07 RID: 44039 RVA: 0x0035B1A7 File Offset: 0x003593A7
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_Pitch = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600AC08 RID: 44040 RVA: 0x0035B1C7 File Offset: 0x003593C7
		public override void OnEnter()
		{
			this.UpdatePitch();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC09 RID: 44041 RVA: 0x0035B1DD File Offset: 0x003593DD
		public override void OnUpdate()
		{
			this.UpdatePitch();
		}

		// Token: 0x0600AC0A RID: 44042 RVA: 0x0035B1E8 File Offset: 0x003593E8
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
			if (this.m_Pitch.IsNone)
			{
				return;
			}
			SoundManager.Get().SetPitch(component, this.m_Pitch.Value);
		}

		// Token: 0x040092D6 RID: 37590
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092D7 RID: 37591
		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_Pitch;

		// Token: 0x040092D8 RID: 37592
		public bool m_EveryFrame;
	}
}
