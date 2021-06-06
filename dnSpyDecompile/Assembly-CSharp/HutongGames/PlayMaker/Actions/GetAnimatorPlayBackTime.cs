using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EEE RID: 3822
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the playback position in the recording buffer. When in playback mode (use  AnimatorStartPlayback), this value is used for controlling the current playback position in the buffer (in seconds). The value can range between recordingStartTime and recordingStopTime See Also: StartPlayback, StopPlayback.")]
	public class GetAnimatorPlayBackTime : FsmStateAction
	{
		// Token: 0x0600AB18 RID: 43800 RVA: 0x00357C6A File Offset: 0x00355E6A
		public override void Reset()
		{
			this.gameObject = null;
			this.playBackTime = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB19 RID: 43801 RVA: 0x00357C84 File Offset: 0x00355E84
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
			this.GetPlayBackTime();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB1A RID: 43802 RVA: 0x00357CE8 File Offset: 0x00355EE8
		public override void OnUpdate()
		{
			this.GetPlayBackTime();
		}

		// Token: 0x0600AB1B RID: 43803 RVA: 0x00357CF0 File Offset: 0x00355EF0
		private void GetPlayBackTime()
		{
			if (this._animator != null)
			{
				this.playBackTime.Value = this._animator.playbackTime;
			}
		}

		// Token: 0x040091DC RID: 37340
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091DD RID: 37341
		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack time of the animator.")]
		public FsmFloat playBackTime;

		// Token: 0x040091DE RID: 37342
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;

		// Token: 0x040091DF RID: 37343
		private Animator _animator;
	}
}
