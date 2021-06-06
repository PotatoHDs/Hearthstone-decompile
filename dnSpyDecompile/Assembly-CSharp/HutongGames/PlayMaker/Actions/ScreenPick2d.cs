using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D17 RID: 3351
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a raycast into the 2d scene using screen coordinates and stores the results. Use Ray Distance to set how close the camera must be to pick the 2d object. NOTE: Uses the MainCamera!")]
	public class ScreenPick2d : FsmStateAction
	{
		// Token: 0x0600A270 RID: 41584 RVA: 0x0033C380 File Offset: 0x0033A580
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
			this.storeDidPickObject = null;
			this.storeGameObject = null;
			this.storePoint = null;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A271 RID: 41585 RVA: 0x0033C403 File Offset: 0x0033A603
		public override void OnEnter()
		{
			this.DoScreenPick();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A272 RID: 41586 RVA: 0x0033C419 File Offset: 0x0033A619
		public override void OnUpdate()
		{
			this.DoScreenPick();
		}

		// Token: 0x0600A273 RID: 41587 RVA: 0x0033C424 File Offset: 0x0033A624
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
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(pos), float.PositiveInfinity, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
			bool flag = rayIntersection.collider != null;
			this.storeDidPickObject.Value = flag;
			if (flag)
			{
				this.storeGameObject.Value = rayIntersection.collider.gameObject;
				this.storePoint.Value = rayIntersection.point;
				return;
			}
			this.storeGameObject.Value = null;
			this.storePoint.Value = Vector3.zero;
		}

		// Token: 0x040088D5 RID: 35029
		[Tooltip("A Vector3 screen position. Commonly stored by other actions.")]
		public FsmVector3 screenVector;

		// Token: 0x040088D6 RID: 35030
		[Tooltip("X position on screen.")]
		public FsmFloat screenX;

		// Token: 0x040088D7 RID: 35031
		[Tooltip("Y position on screen.")]
		public FsmFloat screenY;

		// Token: 0x040088D8 RID: 35032
		[Tooltip("Are the supplied screen coordinates normalized (0-1), or in pixels.")]
		public FsmBool normalized;

		// Token: 0x040088D9 RID: 35033
		[UIHint(UIHint.Variable)]
		[Tooltip("Store whether the Screen pick did pick a GameObject")]
		public FsmBool storeDidPickObject;

		// Token: 0x040088DA RID: 35034
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the picked GameObject")]
		public FsmGameObject storeGameObject;

		// Token: 0x040088DB RID: 35035
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the picked position in world Space")]
		public FsmVector3 storePoint;

		// Token: 0x040088DC RID: 35036
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x040088DD RID: 35037
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040088DE RID: 35038
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
