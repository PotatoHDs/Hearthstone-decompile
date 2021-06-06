using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECD RID: 3789
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the animator in recording mode, and allocates a circular buffer of size frameCount. After this call, the recorder starts collecting up to frameCount frames in the buffer. Note it is not possible to start playback until a call to StopRecording is made")]
	public class AnimatorStartRecording : FsmStateAction
	{
		// Token: 0x0600AA81 RID: 43649 RVA: 0x00355CEB File Offset: 0x00353EEB
		public override void Reset()
		{
			this.gameObject = null;
			this.frameCount = 0;
		}

		// Token: 0x0600AA82 RID: 43650 RVA: 0x00355D00 File Offset: 0x00353F00
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			Animator component = ownerDefaultTarget.GetComponent<Animator>();
			if (component != null)
			{
				component.StartRecording(this.frameCount.Value);
			}
			base.Finish();
		}

		// Token: 0x0400912D RID: 37165
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400912E RID: 37166
		[RequiredField]
		[Tooltip("The number of frames (updates) that will be recorded. If frameCount is 0, the recording will continue until the user calls StopRecording. The maximum value for frameCount is 10000.")]
		public FsmInt frameCount;
	}
}
