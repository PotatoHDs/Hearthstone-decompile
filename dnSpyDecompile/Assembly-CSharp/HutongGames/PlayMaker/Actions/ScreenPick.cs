using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D97 RID: 3479
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a raycast into the scene using screen coordinates and stores the results. Use Ray Distance to set how close the camera must be to pick the object. NOTE: Uses the MainCamera!")]
	public class ScreenPick : FsmStateAction
	{
		// Token: 0x0600A4FA RID: 42234 RVA: 0x003450F8 File Offset: 0x003432F8
		public override void Reset()
		{
			this.screenVector = new FsmVector3
			{
				UseVariable = true
			};
			this.screenX = new FsmFloat
			{
				UseVariable = true
			};
			this.screenY = new FsmFloat
			{
				UseVariable = true
			};
			this.normalized = false;
			this.rayDistance = 100f;
			this.storeDidPickObject = null;
			this.storeGameObject = null;
			this.storePoint = null;
			this.storeNormal = null;
			this.storeDistance = null;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A4FB RID: 42235 RVA: 0x00345199 File Offset: 0x00343399
		public override void OnEnter()
		{
			this.DoScreenPick();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A4FC RID: 42236 RVA: 0x003451AF File Offset: 0x003433AF
		public override void OnUpdate()
		{
			this.DoScreenPick();
		}

		// Token: 0x0600A4FD RID: 42237 RVA: 0x003451B8 File Offset: 0x003433B8
		private void DoScreenPick()
		{
			if (Camera.main == null)
			{
				base.LogError("No MainCamera defined!");
				base.Finish();
				return;
			}
			Vector3 pos = Vector3.zero;
			if (!this.screenVector.IsNone)
			{
				pos = this.screenVector.Value;
			}
			if (!this.screenX.IsNone)
			{
				pos.x = this.screenX.Value;
			}
			if (!this.screenY.IsNone)
			{
				pos.y = this.screenY.Value;
			}
			if (this.normalized.Value)
			{
				pos.x *= (float)Screen.width;
				pos.y *= (float)Screen.height;
			}
			RaycastHit raycastHit;
			Physics.Raycast(Camera.main.ScreenPointToRay(pos), out raycastHit, this.rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			bool flag = raycastHit.collider != null;
			this.storeDidPickObject.Value = flag;
			if (flag)
			{
				this.storeGameObject.Value = raycastHit.collider.gameObject;
				this.storeDistance.Value = raycastHit.distance;
				this.storePoint.Value = raycastHit.point;
				this.storeNormal.Value = raycastHit.normal;
				return;
			}
			this.storeGameObject.Value = null;
			this.storeDistance = float.PositiveInfinity;
			this.storePoint.Value = Vector3.zero;
			this.storeNormal.Value = Vector3.zero;
		}

		// Token: 0x04008B87 RID: 35719
		[Tooltip("A Vector3 screen position. Commonly stored by other actions.")]
		public FsmVector3 screenVector;

		// Token: 0x04008B88 RID: 35720
		[Tooltip("X position on screen.")]
		public FsmFloat screenX;

		// Token: 0x04008B89 RID: 35721
		[Tooltip("Y position on screen.")]
		public FsmFloat screenY;

		// Token: 0x04008B8A RID: 35722
		[Tooltip("Are the supplied screen coordinates normalized (0-1), or in pixels.")]
		public FsmBool normalized;

		// Token: 0x04008B8B RID: 35723
		[RequiredField]
		public FsmFloat rayDistance = 100f;

		// Token: 0x04008B8C RID: 35724
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidPickObject;

		// Token: 0x04008B8D RID: 35725
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;

		// Token: 0x04008B8E RID: 35726
		[UIHint(UIHint.Variable)]
		public FsmVector3 storePoint;

		// Token: 0x04008B8F RID: 35727
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeNormal;

		// Token: 0x04008B90 RID: 35728
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDistance;

		// Token: 0x04008B91 RID: 35729
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x04008B92 RID: 35730
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x04008B93 RID: 35731
		public bool everyFrame;
	}
}
