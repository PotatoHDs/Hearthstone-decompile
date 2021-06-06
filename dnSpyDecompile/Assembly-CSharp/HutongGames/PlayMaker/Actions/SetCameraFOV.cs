using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB0 RID: 3504
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets Field of View used by the Camera.")]
	public class SetCameraFOV : ComponentAction<Camera>
	{
		// Token: 0x0600A566 RID: 42342 RVA: 0x00346895 File Offset: 0x00344A95
		public override void Reset()
		{
			this.gameObject = null;
			this.fieldOfView = 50f;
			this.everyFrame = false;
		}

		// Token: 0x0600A567 RID: 42343 RVA: 0x003468B5 File Offset: 0x00344AB5
		public override void OnEnter()
		{
			this.DoSetCameraFOV();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A568 RID: 42344 RVA: 0x003468CB File Offset: 0x00344ACB
		public override void OnUpdate()
		{
			this.DoSetCameraFOV();
		}

		// Token: 0x0600A569 RID: 42345 RVA: 0x003468D4 File Offset: 0x00344AD4
		private void DoSetCameraFOV()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.camera.fieldOfView = this.fieldOfView.Value;
			}
		}

		// Token: 0x04008BF0 RID: 35824
		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BF1 RID: 35825
		[RequiredField]
		public FsmFloat fieldOfView;

		// Token: 0x04008BF2 RID: 35826
		public bool everyFrame;
	}
}
