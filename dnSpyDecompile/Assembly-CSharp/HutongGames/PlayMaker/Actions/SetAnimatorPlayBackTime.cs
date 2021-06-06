using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F01 RID: 3841
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the playback position in the recording buffer. When in playback mode (use AnimatorStartPlayback), this value is used for controlling the current playback position in the buffer (in seconds). The value can range between recordingStartTime and recordingStopTime ")]
	public class SetAnimatorPlayBackTime : FsmStateAction
	{
		// Token: 0x0600AB76 RID: 43894 RVA: 0x00358FDF File Offset: 0x003571DF
		public override void Reset()
		{
			this.gameObject = null;
			this.playbackTime = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB77 RID: 43895 RVA: 0x00358FF8 File Offset: 0x003571F8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			this._animator = ownerDefaultTarget.GetComponent<Animator>();
			if (this._animator == null)
			{
				base.Finish();
				return;
			}
			this.DoPlaybackTime();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB78 RID: 43896 RVA: 0x0035905C File Offset: 0x0035725C
		public override void OnUpdate()
		{
			this.DoPlaybackTime();
		}

		// Token: 0x0600AB79 RID: 43897 RVA: 0x00359064 File Offset: 0x00357264
		private void DoPlaybackTime()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.playbackTime = this.playbackTime.Value;
		}

		// Token: 0x0400923B RID: 37435
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400923C RID: 37436
		[Tooltip("The playBack time")]
		public FsmFloat playbackTime;

		// Token: 0x0400923D RID: 37437
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;

		// Token: 0x0400923E RID: 37438
		private Animator _animator;
	}
}
