using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000ECF RID: 3791
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Stops the animator record mode. It will lock the recording buffer's contents in its current state. The data get saved for subsequent playback with StartPlayback.")]
	public class AnimatorStopRecording : FsmStateAction
	{
		// Token: 0x0600AA87 RID: 43655 RVA: 0x00355DAB File Offset: 0x00353FAB
		public override void Reset()
		{
			this.gameObject = null;
			this.recorderStartTime = null;
			this.recorderStopTime = null;
		}

		// Token: 0x0600AA88 RID: 43656 RVA: 0x00355DC4 File Offset: 0x00353FC4
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
				component.StopRecording();
				this.recorderStartTime.Value = component.recorderStartTime;
				this.recorderStopTime.Value = component.recorderStopTime;
			}
			base.Finish();
		}

		// Token: 0x04009130 RID: 37168
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009131 RID: 37169
		[ActionSection("Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The recorder StartTime")]
		public FsmFloat recorderStartTime;

		// Token: 0x04009132 RID: 37170
		[UIHint(UIHint.Variable)]
		[Tooltip("The recorder StopTime")]
		public FsmFloat recorderStopTime;
	}
}
