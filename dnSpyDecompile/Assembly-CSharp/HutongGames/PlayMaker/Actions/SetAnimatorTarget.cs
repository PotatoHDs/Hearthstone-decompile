using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F04 RID: 3844
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets an AvatarTarget and a targetNormalizedTime for the current state")]
	public class SetAnimatorTarget : FsmStateAction
	{
		// Token: 0x0600AB84 RID: 43908 RVA: 0x003591CB File Offset: 0x003573CB
		public override void Reset()
		{
			this.gameObject = null;
			this.avatarTarget = AvatarTarget.Body;
			this.targetNormalizedTime = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB85 RID: 43909 RVA: 0x003580F7 File Offset: 0x003562F7
		public override void OnPreprocess()
		{
			base.Fsm.HandleAnimatorMove = true;
		}

		// Token: 0x0600AB86 RID: 43910 RVA: 0x003591EC File Offset: 0x003573EC
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
			this.SetTarget();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB87 RID: 43911 RVA: 0x00359250 File Offset: 0x00357450
		public override void DoAnimatorMove()
		{
			this.SetTarget();
		}

		// Token: 0x0600AB88 RID: 43912 RVA: 0x00359258 File Offset: 0x00357458
		private void SetTarget()
		{
			if (this._animator != null)
			{
				this._animator.SetTarget(this.avatarTarget, this.targetNormalizedTime.Value);
			}
		}

		// Token: 0x04009246 RID: 37446
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009247 RID: 37447
		[Tooltip("The avatar target")]
		public AvatarTarget avatarTarget;

		// Token: 0x04009248 RID: 37448
		[Tooltip("The current state Time that is queried")]
		public FsmFloat targetNormalizedTime;

		// Token: 0x04009249 RID: 37449
		[Tooltip("Repeat every frame during OnAnimatorMove. Useful when changing over time.")]
		public bool everyFrame;

		// Token: 0x0400924A RID: 37450
		private Animator _animator;
	}
}
