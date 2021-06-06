using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EE9 RID: 3817
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the layer's current weight")]
	public class GetAnimatorLayerWeight : FsmStateActionAnimatorBase
	{
		// Token: 0x0600AAFE RID: 43774 RVA: 0x0035770A File Offset: 0x0035590A
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.layerIndex = null;
			this.layerWeight = null;
		}

		// Token: 0x0600AAFF RID: 43775 RVA: 0x00357728 File Offset: 0x00355928
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
			this.GetLayerWeight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB00 RID: 43776 RVA: 0x0035778C File Offset: 0x0035598C
		public override void OnActionUpdate()
		{
			this.GetLayerWeight();
		}

		// Token: 0x0600AB01 RID: 43777 RVA: 0x00357794 File Offset: 0x00355994
		private void GetLayerWeight()
		{
			if (this._animator != null)
			{
				this.layerWeight.Value = this._animator.GetLayerWeight(this.layerIndex.Value);
			}
		}

		// Token: 0x040091BF RID: 37311
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040091C0 RID: 37312
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x040091C1 RID: 37313
		[ActionSection("Results")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The layer's current weight")]
		public FsmFloat layerWeight;

		// Token: 0x040091C2 RID: 37314
		private Animator _animator;
	}
}
