using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EDF RID: 3807
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the scale of the current Avatar for a humanoid rig, (1 by default if the rig is generic).\n The scale is relative to Unity's Default Avatar")]
	public class GetAnimatorHumanScale : FsmStateAction
	{
		// Token: 0x0600AAD2 RID: 43730 RVA: 0x00356E03 File Offset: 0x00355003
		public override void Reset()
		{
			this.gameObject = null;
			this.humanScale = null;
		}

		// Token: 0x0600AAD3 RID: 43731 RVA: 0x00356E14 File Offset: 0x00355014
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
			this.DoGetHumanScale();
			base.Finish();
		}

		// Token: 0x0600AAD4 RID: 43732 RVA: 0x00356E70 File Offset: 0x00355070
		private void DoGetHumanScale()
		{
			if (this._animator == null)
			{
				return;
			}
			this.humanScale.Value = this._animator.humanScale;
		}

		// Token: 0x0400918B RID: 37259
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400918C RID: 37260
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("the scale of the current Avatar")]
		public FsmFloat humanScale;

		// Token: 0x0400918D RID: 37261
		private Animator _animator;
	}
}
