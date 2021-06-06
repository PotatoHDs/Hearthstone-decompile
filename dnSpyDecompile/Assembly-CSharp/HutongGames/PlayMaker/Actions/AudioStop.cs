using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD4 RID: 3028
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Stops playing the Audio Clip played by an Audio Source component on a Game Object.")]
	public class AudioStop : FsmStateAction
	{
		// Token: 0x06009CB8 RID: 40120 RVA: 0x0032653E File Offset: 0x0032473E
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x06009CB9 RID: 40121 RVA: 0x00326548 File Offset: 0x00324748
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.Stop();
				}
			}
			base.Finish();
		}

		// Token: 0x0400822F RID: 33327
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;
	}
}
