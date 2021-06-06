using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EED RID: 3821
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the playback speed of the Animator. 1 is normal playback speed")]
	public class GetAnimatorPlayBackSpeed : FsmStateAction
	{
		// Token: 0x0600AB13 RID: 43795 RVA: 0x00357BBE File Offset: 0x00355DBE
		public override void Reset()
		{
			this.gameObject = null;
			this.playBackSpeed = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB14 RID: 43796 RVA: 0x00357BD8 File Offset: 0x00355DD8
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
			this.GetPlayBackSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB15 RID: 43797 RVA: 0x00357C3C File Offset: 0x00355E3C
		public override void OnUpdate()
		{
			this.GetPlayBackSpeed();
		}

		// Token: 0x0600AB16 RID: 43798 RVA: 0x00357C44 File Offset: 0x00355E44
		private void GetPlayBackSpeed()
		{
			if (this._animator != null)
			{
				this.playBackSpeed.Value = this._animator.speed;
			}
		}

		// Token: 0x040091D8 RID: 37336
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091D9 RID: 37337
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack speed of the animator. 1 is normal playback speed")]
		public FsmFloat playBackSpeed;

		// Token: 0x040091DA RID: 37338
		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;

		// Token: 0x040091DB RID: 37339
		private Animator _animator;
	}
}
