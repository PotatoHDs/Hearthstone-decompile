using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDC RID: 3804
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the feet pivot. At 0% blending point is body mass center. At 100% blending point is feet pivot")]
	public class GetAnimatorFeetPivotActive : FsmStateAction
	{
		// Token: 0x0600AAC4 RID: 43716 RVA: 0x00356BF1 File Offset: 0x00354DF1
		public override void Reset()
		{
			this.gameObject = null;
			this.feetPivotActive = null;
		}

		// Token: 0x0600AAC5 RID: 43717 RVA: 0x00356C04 File Offset: 0x00354E04
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
			this.DoGetFeetPivotActive();
			base.Finish();
		}

		// Token: 0x0600AAC6 RID: 43718 RVA: 0x00356C60 File Offset: 0x00354E60
		private void DoGetFeetPivotActive()
		{
			if (this._animator == null)
			{
				return;
			}
			this.feetPivotActive.Value = this._animator.feetPivotActive;
		}

		// Token: 0x04009180 RID: 37248
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009181 RID: 37249
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The feet pivot Blending. At 0% blending point is body mass center. At 100% blending point is feet pivot")]
		public FsmFloat feetPivotActive;

		// Token: 0x04009182 RID: 37250
		private Animator _animator;
	}
}
