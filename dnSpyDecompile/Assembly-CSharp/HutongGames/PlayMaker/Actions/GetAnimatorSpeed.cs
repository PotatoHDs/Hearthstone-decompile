using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF1 RID: 3825
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the playback speed of the Animator. 1 is normal playback speed")]
	public class GetAnimatorSpeed : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AB28 RID: 43816 RVA: 0x00357EFE File Offset: 0x003560FE
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.speed = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB29 RID: 43817 RVA: 0x00357F1C File Offset: 0x0035611C
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
			this.GetPlaybackSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB2A RID: 43818 RVA: 0x00357F80 File Offset: 0x00356180
		public override void OnActionUpdate()
		{
			this.GetPlaybackSpeed();
		}

		// Token: 0x0600AB2B RID: 43819 RVA: 0x00357F88 File Offset: 0x00356188
		private void GetPlaybackSpeed()
		{
			if (this._animator != null)
			{
				this.speed.Value = this._animator.speed;
			}
		}

		// Token: 0x040091EA RID: 37354
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091EB RID: 37355
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The playBack speed of the animator. 1 is normal playback speed")]
		public FsmFloat speed;

		// Token: 0x040091EC RID: 37356
		private Animator _animator;
	}
}
