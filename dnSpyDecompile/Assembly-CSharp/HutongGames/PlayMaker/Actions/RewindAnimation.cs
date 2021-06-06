using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D74 RID: 3444
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Rewinds the named animation.")]
	public class RewindAnimation : BaseAnimationAction
	{
		// Token: 0x0600A442 RID: 42050 RVA: 0x0034283E File Offset: 0x00340A3E
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
		}

		// Token: 0x0600A443 RID: 42051 RVA: 0x0034284E File Offset: 0x00340A4E
		public override void OnEnter()
		{
			this.DoRewindAnimation();
			base.Finish();
		}

		// Token: 0x0600A444 RID: 42052 RVA: 0x0034285C File Offset: 0x00340A5C
		private void DoRewindAnimation()
		{
			if (string.IsNullOrEmpty(this.animName.Value))
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.animation.Rewind(this.animName.Value);
			}
		}

		// Token: 0x04008AB2 RID: 35506
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008AB3 RID: 35507
		[UIHint(UIHint.Animation)]
		public FsmString animName;
	}
}
