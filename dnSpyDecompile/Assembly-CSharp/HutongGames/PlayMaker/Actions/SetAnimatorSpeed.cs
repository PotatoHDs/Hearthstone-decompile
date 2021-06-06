using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F02 RID: 3842
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the playback speed of the Animator. 1 is normal playback speed")]
	public class SetAnimatorSpeed : FsmStateAction
	{
		// Token: 0x0600AB7B RID: 43899 RVA: 0x0035908B File Offset: 0x0035728B
		public override void Reset()
		{
			this.gameObject = null;
			this.speed = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB7C RID: 43900 RVA: 0x003590A4 File Offset: 0x003572A4
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
			this.DoPlaybackSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB7D RID: 43901 RVA: 0x00359108 File Offset: 0x00357308
		public override void OnUpdate()
		{
			this.DoPlaybackSpeed();
		}

		// Token: 0x0600AB7E RID: 43902 RVA: 0x00359110 File Offset: 0x00357310
		private void DoPlaybackSpeed()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.speed = this.speed.Value;
		}

		// Token: 0x0400923F RID: 37439
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009240 RID: 37440
		[Tooltip("The playBack speed")]
		public FsmFloat speed;

		// Token: 0x04009241 RID: 37441
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;

		// Token: 0x04009242 RID: 37442
		private Animator _animator;
	}
}
