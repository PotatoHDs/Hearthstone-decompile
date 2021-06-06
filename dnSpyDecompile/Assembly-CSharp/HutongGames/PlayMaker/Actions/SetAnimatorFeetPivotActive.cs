using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EF9 RID: 3833
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Activates feet pivot. At 0% blending point is body mass center. At 100% blending point is feet pivot")]
	public class SetAnimatorFeetPivotActive : FsmStateAction
	{
		// Token: 0x0600AB4E RID: 43854 RVA: 0x00358611 File Offset: 0x00356811
		public override void Reset()
		{
			this.gameObject = null;
			this.feetPivotActive = null;
		}

		// Token: 0x0600AB4F RID: 43855 RVA: 0x00358624 File Offset: 0x00356824
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
			this.DoFeetPivotActive();
			base.Finish();
		}

		// Token: 0x0600AB50 RID: 43856 RVA: 0x00358680 File Offset: 0x00356880
		private void DoFeetPivotActive()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.feetPivotActive = this.feetPivotActive.Value;
		}

		// Token: 0x0400920C RID: 37388
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400920D RID: 37389
		[Tooltip("Activates feet pivot. At 0% blending point is body mass center. At 100% blending point is feet pivot")]
		public FsmFloat feetPivotActive;

		// Token: 0x0400920E RID: 37390
		private Animator _animator;
	}
}
