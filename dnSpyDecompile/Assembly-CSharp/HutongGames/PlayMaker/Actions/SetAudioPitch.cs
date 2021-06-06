using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAB RID: 3499
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Pitch of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioPitch : ComponentAction<AudioSource>
	{
		// Token: 0x0600A54E RID: 42318 RVA: 0x00346610 File Offset: 0x00344810
		public override void Reset()
		{
			this.gameObject = null;
			this.pitch = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A54F RID: 42319 RVA: 0x00346630 File Offset: 0x00344830
		public override void OnEnter()
		{
			this.DoSetAudioPitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A550 RID: 42320 RVA: 0x00346646 File Offset: 0x00344846
		public override void OnUpdate()
		{
			this.DoSetAudioPitch();
		}

		// Token: 0x0600A551 RID: 42321 RVA: 0x00346650 File Offset: 0x00344850
		private void DoSetAudioPitch()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget) && !this.pitch.IsNone)
			{
				base.audio.pitch = this.pitch.Value;
			}
		}

		// Token: 0x04008BE0 RID: 35808
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BE1 RID: 35809
		public FsmFloat pitch;

		// Token: 0x04008BE2 RID: 35810
		public bool everyFrame;
	}
}
