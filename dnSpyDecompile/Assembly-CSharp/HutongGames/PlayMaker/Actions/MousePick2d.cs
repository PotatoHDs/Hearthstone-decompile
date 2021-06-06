using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D14 RID: 3348
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a Mouse Pick on a 2d scene and stores the results. Use Ray Distance to set how close the camera must be to pick the 2d object.")]
	public class MousePick2d : FsmStateAction
	{
		// Token: 0x0600A260 RID: 41568 RVA: 0x0033BCC9 File Offset: 0x00339EC9
		public override void Reset()
		{
			this.storeDidPickObject = null;
			this.storeGameObject = null;
			this.storePoint = null;
			this.layerMask = new FsmInt[0];
			this.invertMask = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A261 RID: 41569 RVA: 0x0033BCFF File Offset: 0x00339EFF
		public override void OnEnter()
		{
			this.DoMousePick2d();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A262 RID: 41570 RVA: 0x0033BD15 File Offset: 0x00339F15
		public override void OnUpdate()
		{
			this.DoMousePick2d();
		}

		// Token: 0x0600A263 RID: 41571 RVA: 0x0033BD20 File Offset: 0x00339F20
		private void DoMousePick2d()
		{
			RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), float.PositiveInfinity, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
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

		// Token: 0x040088B2 RID: 34994
		[UIHint(UIHint.Variable)]
		[Tooltip("Store if a GameObject was picked in a Bool variable. True if a GameObject was picked, otherwise false.")]
		public FsmBool storeDidPickObject;

		// Token: 0x040088B3 RID: 34995
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the picked GameObject in a variable.")]
		public FsmGameObject storeGameObject;

		// Token: 0x040088B4 RID: 34996
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the picked point in a variable.")]
		public FsmVector2 storePoint;

		// Token: 0x040088B5 RID: 34997
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x040088B6 RID: 34998
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040088B7 RID: 34999
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
