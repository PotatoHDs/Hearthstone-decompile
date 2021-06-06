using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD1 RID: 3025
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Mute/unmute the Audio Clip played by an Audio Source component on a Game Object.")]
	public class AudioMute : FsmStateAction
	{
		// Token: 0x06009CAE RID: 40110 RVA: 0x003262E6 File Offset: 0x003244E6
		public override void Reset()
		{
			this.gameObject = null;
			this.mute = false;
		}

		// Token: 0x06009CAF RID: 40111 RVA: 0x003262FC File Offset: 0x003244FC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.mute = this.mute.Value;
				}
			}
			base.Finish();
		}

		// Token: 0x04008226 RID: 33318
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an Audio Source component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008227 RID: 33319
		[RequiredField]
		[Tooltip("Check to mute, uncheck to unmute.")]
		public FsmBool mute;
	}
}
