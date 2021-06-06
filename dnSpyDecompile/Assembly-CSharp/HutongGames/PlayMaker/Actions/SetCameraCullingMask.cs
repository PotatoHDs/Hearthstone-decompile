using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAF RID: 3503
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the Culling Mask used by the Camera.")]
	public class SetCameraCullingMask : ComponentAction<Camera>
	{
		// Token: 0x0600A561 RID: 42337 RVA: 0x00346803 File Offset: 0x00344A03
		public override void Reset()
		{
			this.gameObject = null;
			this.cullingMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A562 RID: 42338 RVA: 0x0034682B File Offset: 0x00344A2B
		public override void OnEnter()
		{
			this.DoSetCameraCullingMask();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A563 RID: 42339 RVA: 0x00346841 File Offset: 0x00344A41
		public override void OnUpdate()
		{
			this.DoSetCameraCullingMask();
		}

		// Token: 0x0600A564 RID: 42340 RVA: 0x0034684C File Offset: 0x00344A4C
		private void DoSetCameraCullingMask()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.camera.cullingMask = ActionHelpers.LayerArrayToLayerMask(this.cullingMask, this.invertMask.Value);
			}
		}

		// Token: 0x04008BEC RID: 35820
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BED RID: 35821
		[Tooltip("Cull these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] cullingMask;

		// Token: 0x04008BEE RID: 35822
		[Tooltip("Invert the mask, so you cull all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008BEF RID: 35823
		public bool everyFrame;
	}
}
