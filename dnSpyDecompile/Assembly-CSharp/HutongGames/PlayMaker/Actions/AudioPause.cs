using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD2 RID: 3026
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Pauses playing the Audio Clip played by an Audio Source component on a Game Object.")]
	public class AudioPause : FsmStateAction
	{
		// Token: 0x06009CB1 RID: 40113 RVA: 0x0032634B File Offset: 0x0032454B
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x06009CB2 RID: 40114 RVA: 0x00326354 File Offset: 0x00324554
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.Pause();
				}
			}
			base.Finish();
		}

		// Token: 0x04008228 RID: 33320
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an Audio Source component.")]
		public FsmOwnerDefault gameObject;
	}
}
