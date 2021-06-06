using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFD RID: 3837
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("If true, additional layers affects the mass center")]
	public class SetAnimatorLayersAffectMassCenter : FsmStateAction
	{
		// Token: 0x0600AB62 RID: 43874 RVA: 0x00358B16 File Offset: 0x00356D16
		public override void Reset()
		{
			this.gameObject = null;
			this.affectMassCenter = null;
		}

		// Token: 0x0600AB63 RID: 43875 RVA: 0x00358B28 File Offset: 0x00356D28
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
			this.SetAffectMassCenter();
			base.Finish();
		}

		// Token: 0x0600AB64 RID: 43876 RVA: 0x00358B84 File Offset: 0x00356D84
		private void SetAffectMassCenter()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.layersAffectMassCenter = this.affectMassCenter.Value;
		}

		// Token: 0x04009224 RID: 37412
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009225 RID: 37413
		[Tooltip("If true, additional layers affects the mass center")]
		public FsmBool affectMassCenter;

		// Token: 0x04009226 RID: 37414
		private Animator _animator;
	}
}
