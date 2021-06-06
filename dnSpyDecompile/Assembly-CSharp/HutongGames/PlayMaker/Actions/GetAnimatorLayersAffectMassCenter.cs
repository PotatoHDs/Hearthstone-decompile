using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE8 RID: 3816
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns if additional layers affects the mass center")]
	public class GetAnimatorLayersAffectMassCenter : FsmStateAction
	{
		// Token: 0x0600AAFA RID: 43770 RVA: 0x00357636 File Offset: 0x00355836
		public override void Reset()
		{
			this.gameObject = null;
			this.affectMassCenter = null;
			this.affectMassCenterEvent = null;
			this.doNotAffectMassCenterEvent = null;
		}

		// Token: 0x0600AAFB RID: 43771 RVA: 0x00357654 File Offset: 0x00355854
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
			this.CheckAffectMassCenter();
			base.Finish();
		}

		// Token: 0x0600AAFC RID: 43772 RVA: 0x003576B0 File Offset: 0x003558B0
		private void CheckAffectMassCenter()
		{
			if (this._animator == null)
			{
				return;
			}
			bool layersAffectMassCenter = this._animator.layersAffectMassCenter;
			this.affectMassCenter.Value = layersAffectMassCenter;
			if (layersAffectMassCenter)
			{
				base.Fsm.Event(this.affectMassCenterEvent);
				return;
			}
			base.Fsm.Event(this.doNotAffectMassCenterEvent);
		}

		// Token: 0x040091BA RID: 37306
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091BB RID: 37307
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("If true, additional layers affects the mass center")]
		public FsmBool affectMassCenter;

		// Token: 0x040091BC RID: 37308
		[Tooltip("Event send if additional layers affects the mass center")]
		public FsmEvent affectMassCenterEvent;

		// Token: 0x040091BD RID: 37309
		[Tooltip("Event send if additional layers do no affects the mass center")]
		public FsmEvent doNotAffectMassCenterEvent;

		// Token: 0x040091BE RID: 37310
		private Animator _animator;
	}
}
