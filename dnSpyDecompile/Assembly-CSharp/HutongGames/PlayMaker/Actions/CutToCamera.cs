using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C02 RID: 3074
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Activates a Camera in the scene.")]
	public class CutToCamera : FsmStateAction
	{
		// Token: 0x06009DA6 RID: 40358 RVA: 0x0032984D File Offset: 0x00327A4D
		public override void Reset()
		{
			this.camera = null;
			this.makeMainCamera = true;
			this.cutBackOnExit = false;
		}

		// Token: 0x06009DA7 RID: 40359 RVA: 0x00329864 File Offset: 0x00327A64
		public override void OnEnter()
		{
			if (this.camera == null)
			{
				base.LogError("Missing camera!");
				return;
			}
			this.oldCamera = Camera.main;
			CutToCamera.SwitchCamera(Camera.main, this.camera);
			if (this.makeMainCamera)
			{
				this.camera.tag = "MainCamera";
			}
			base.Finish();
		}

		// Token: 0x06009DA8 RID: 40360 RVA: 0x003298C4 File Offset: 0x00327AC4
		public override void OnExit()
		{
			if (this.cutBackOnExit)
			{
				CutToCamera.SwitchCamera(this.camera, this.oldCamera);
			}
		}

		// Token: 0x06009DA9 RID: 40361 RVA: 0x003298DF File Offset: 0x00327ADF
		private static void SwitchCamera(Camera camera1, Camera camera2)
		{
			if (camera1 != null)
			{
				camera1.enabled = false;
			}
			if (camera2 != null)
			{
				camera2.enabled = true;
			}
		}

		// Token: 0x04008311 RID: 33553
		[RequiredField]
		[Tooltip("The Camera to activate.")]
		public Camera camera;

		// Token: 0x04008312 RID: 33554
		[Tooltip("Makes the camera the new MainCamera. The old MainCamera will be untagged.")]
		public bool makeMainCamera;

		// Token: 0x04008313 RID: 33555
		[Tooltip("Cut back to the original MainCamera when exiting this state.")]
		public bool cutBackOnExit;

		// Token: 0x04008314 RID: 33556
		private Camera oldCamera;
	}
}
