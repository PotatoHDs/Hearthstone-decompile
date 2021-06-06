using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE7 RID: 3815
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Returns the name of a layer from its index")]
	public class GetAnimatorLayerName : FsmStateAction
	{
		// Token: 0x0600AAF6 RID: 43766 RVA: 0x0035758F File Offset: 0x0035578F
		public override void Reset()
		{
			this.gameObject = null;
			this.layerIndex = null;
			this.layerName = null;
		}

		// Token: 0x0600AAF7 RID: 43767 RVA: 0x003575A8 File Offset: 0x003557A8
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
			this.DoGetLayerName();
			base.Finish();
		}

		// Token: 0x0600AAF8 RID: 43768 RVA: 0x00357604 File Offset: 0x00355804
		private void DoGetLayerName()
		{
			if (this._animator == null)
			{
				return;
			}
			this.layerName.Value = this._animator.GetLayerName(this.layerIndex.Value);
		}

		// Token: 0x040091B6 RID: 37302
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091B7 RID: 37303
		[RequiredField]
		[Tooltip("The layer index")]
		public FsmInt layerIndex;

		// Token: 0x040091B8 RID: 37304
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer name")]
		public FsmString layerName;

		// Token: 0x040091B9 RID: 37305
		private Animator _animator;
	}
}
