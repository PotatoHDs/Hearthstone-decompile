using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F00 RID: 3840
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the playback speed of the Animator. 1 is normal playback speed")]
	public class SetAnimatorPlayBackSpeed : FsmStateAction
	{
		// Token: 0x0600AB71 RID: 43889 RVA: 0x00358F33 File Offset: 0x00357133
		public override void Reset()
		{
			this.gameObject = null;
			this.playBackSpeed = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB72 RID: 43890 RVA: 0x00358F4C File Offset: 0x0035714C
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
			this.DoPlayBackSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB73 RID: 43891 RVA: 0x00358FB0 File Offset: 0x003571B0
		public override void OnUpdate()
		{
			this.DoPlayBackSpeed();
		}

		// Token: 0x0600AB74 RID: 43892 RVA: 0x00358FB8 File Offset: 0x003571B8
		private void DoPlayBackSpeed()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.speed = this.playBackSpeed.Value;
		}

		// Token: 0x04009237 RID: 37431
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009238 RID: 37432
		[Tooltip("If true, automatically stabilize feet during transition and blending")]
		public FsmFloat playBackSpeed;

		// Token: 0x04009239 RID: 37433
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;

		// Token: 0x0400923A RID: 37434
		private Animator _animator;
	}
}
