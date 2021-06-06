using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EFE RID: 3838
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the layer's current weight")]
	public class SetAnimatorLayerWeight : FsmStateAction
	{
		// Token: 0x0600AB66 RID: 43878 RVA: 0x00358BAB File Offset: 0x00356DAB
		public override void Reset()
		{
			this.gameObject = null;
			this.layerIndex = null;
			this.layerWeight = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AB67 RID: 43879 RVA: 0x00358BCC File Offset: 0x00356DCC
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
			this.DoLayerWeight();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AB68 RID: 43880 RVA: 0x00358C30 File Offset: 0x00356E30
		public override void OnUpdate()
		{
			this.DoLayerWeight();
		}

		// Token: 0x0600AB69 RID: 43881 RVA: 0x00358C38 File Offset: 0x00356E38
		private void DoLayerWeight()
		{
			if (this._animator == null)
			{
				return;
			}
			this._animator.SetLayerWeight(this.layerIndex.Value, this.layerWeight.Value);
		}

		// Token: 0x04009227 RID: 37415
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009228 RID: 37416
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;

		// Token: 0x04009229 RID: 37417
		[RequiredField]
		[Tooltip("Sets the layer's current weight")]
		public FsmFloat layerWeight;

		// Token: 0x0400922A RID: 37418
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;

		// Token: 0x0400922B RID: 37419
		private Animator _animator;
	}
}
