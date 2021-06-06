using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E11 RID: 3601
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Stops all playing Animations on a Game Object. Optionally, specify a single Animation to Stop.")]
	public class StopAnimation : BaseAnimationAction
	{
		// Token: 0x0600A713 RID: 42771 RVA: 0x0034B633 File Offset: 0x00349833
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
		}

		// Token: 0x0600A714 RID: 42772 RVA: 0x0034B643 File Offset: 0x00349843
		public override void OnEnter()
		{
			this.DoStopAnimation();
			base.Finish();
		}

		// Token: 0x0600A715 RID: 42773 RVA: 0x0034B654 File Offset: 0x00349854
		private void DoStopAnimation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			if (FsmString.IsNullOrEmpty(this.animName))
			{
				base.animation.Stop();
				return;
			}
			base.animation.Stop(this.animName.Value);
		}

		// Token: 0x04008D96 RID: 36246
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D97 RID: 36247
		[Tooltip("Leave empty to stop all playing animations.")]
		[UIHint(UIHint.Animation)]
		public FsmString animName;
	}
}
