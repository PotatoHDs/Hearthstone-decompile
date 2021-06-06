using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAA RID: 3498
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets looping on the AudioSource component on a Game Object.")]
	public class SetAudioLoop : ComponentAction<AudioSource>
	{
		// Token: 0x0600A54B RID: 42315 RVA: 0x003465B5 File Offset: 0x003447B5
		public override void Reset()
		{
			this.gameObject = null;
			this.loop = false;
		}

		// Token: 0x0600A54C RID: 42316 RVA: 0x003465CC File Offset: 0x003447CC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.audio.loop = this.loop.Value;
			}
			base.Finish();
		}

		// Token: 0x04008BDE RID: 35806
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BDF RID: 35807
		public FsmBool loop;
	}
}
