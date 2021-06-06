using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF1 RID: 3313
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Perform a Mouse Pick on the scene from the Main Camera and stores the results. Use Ray Distance to set how close the camera must be to pick the object.")]
	public class MousePick : FsmStateAction
	{
		// Token: 0x0600A1A5 RID: 41381 RVA: 0x00338744 File Offset: 0x00336944
		public override void Reset()
		{
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

		// Token: 0x0600A1A6 RID: 41382 RVA: 0x003387A3 File Offset: 0x003369A3
		public override void OnEnter()
		{
			this.DoMousePick();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1A7 RID: 41383 RVA: 0x003387B9 File Offset: 0x003369B9
		public override void OnUpdate()
		{
			this.DoMousePick();
		}

		// Token: 0x0600A1A8 RID: 41384 RVA: 0x003387C4 File Offset: 0x003369C4
		private void DoMousePick()
		{
			RaycastHit raycastHit = ActionHelpers.MousePick(this.rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(this.layerMask, this.invertMask.Value));
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
			this.storeDistance.Value = float.PositiveInfinity;
			this.storePoint.Value = Vector3.zero;
			this.storeNormal.Value = Vector3.zero;
		}

		// Token: 0x040087AE RID: 34734
		[RequiredField]
		[Tooltip("Set the length of the ray to cast from the Main Camera.")]
		public FsmFloat rayDistance = 100f;

		// Token: 0x040087AF RID: 34735
		[UIHint(UIHint.Variable)]
		[Tooltip("Set Bool variable true if an object was picked, false if not.")]
		public FsmBool storeDidPickObject;

		// Token: 0x040087B0 RID: 34736
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the picked GameObject.")]
		public FsmGameObject storeGameObject;

		// Token: 0x040087B1 RID: 34737
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the point of contact.")]
		public FsmVector3 storePoint;

		// Token: 0x040087B2 RID: 34738
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the normal at the point of contact.")]
		public FsmVector3 storeNormal;

		// Token: 0x040087B3 RID: 34739
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the distance to the point of contact.")]
		public FsmFloat storeDistance;

		// Token: 0x040087B4 RID: 34740
		[UIHint(UIHint.Layer)]
		[Tooltip("Pick only from these layers.")]
		public FsmInt[] layerMask;

		// Token: 0x040087B5 RID: 34741
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		// Token: 0x040087B6 RID: 34742
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
