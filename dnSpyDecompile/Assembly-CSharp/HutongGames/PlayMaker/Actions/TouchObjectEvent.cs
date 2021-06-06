using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E20 RID: 3616
	[ActionCategory(ActionCategory.Device)]
	[ActionTarget(typeof(GameObject), "gameObject", false)]
	[Tooltip("Sends events when an object is touched. Optionally filter by a fingerID. NOTE: Uses the MainCamera!")]
	public class TouchObjectEvent : FsmStateAction
	{
		// Token: 0x0600A74F RID: 42831 RVA: 0x0034C32C File Offset: 0x0034A52C
		public override void Reset()
		{
			this.gameObject = null;
			this.pickDistance = 100f;
			this.fingerId = new FsmInt
			{
				UseVariable = true
			};
			this.touchBegan = null;
			this.touchMoved = null;
			this.touchStationary = null;
			this.touchEnded = null;
			this.touchCanceled = null;
			this.storeFingerId = null;
			this.storeHitPoint = null;
			this.storeHitNormal = null;
		}

		// Token: 0x0600A750 RID: 42832 RVA: 0x0034C39C File Offset: 0x0034A59C
		public override void OnUpdate()
		{
			if (Camera.main == null)
			{
				base.LogError("No MainCamera defined!");
				base.Finish();
				return;
			}
			if (Input.touchCount > 0)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
				if (ownerDefaultTarget == null)
				{
					return;
				}
				foreach (Touch touch in Input.touches)
				{
					if (this.fingerId.IsNone || touch.fingerId == this.fingerId.Value)
					{
						Vector2 position = touch.position;
						RaycastHit raycastHitInfo;
						Physics.Raycast(Camera.main.ScreenPointToRay(position), out raycastHitInfo, this.pickDistance.Value);
						base.Fsm.RaycastHitInfo = raycastHitInfo;
						if (raycastHitInfo.transform != null && raycastHitInfo.transform.gameObject == ownerDefaultTarget)
						{
							this.storeFingerId.Value = touch.fingerId;
							this.storeHitPoint.Value = raycastHitInfo.point;
							this.storeHitNormal.Value = raycastHitInfo.normal;
							switch (touch.phase)
							{
							case TouchPhase.Began:
								base.Fsm.Event(this.touchBegan);
								return;
							case TouchPhase.Moved:
								base.Fsm.Event(this.touchMoved);
								return;
							case TouchPhase.Stationary:
								base.Fsm.Event(this.touchStationary);
								return;
							case TouchPhase.Ended:
								base.Fsm.Event(this.touchEnded);
								return;
							case TouchPhase.Canceled:
								base.Fsm.Event(this.touchCanceled);
								return;
							}
						}
					}
				}
			}
		}

		// Token: 0x04008DE1 RID: 36321
		[RequiredField]
		[CheckForComponent(typeof(Collider))]
		[Tooltip("The Game Object to detect touches on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008DE2 RID: 36322
		[RequiredField]
		[Tooltip("How far from the camera is the Game Object pickable.")]
		public FsmFloat pickDistance;

		// Token: 0x04008DE3 RID: 36323
		[Tooltip("Only detect touches that match this fingerID, or set to None.")]
		public FsmInt fingerId;

		// Token: 0x04008DE4 RID: 36324
		[ActionSection("Events")]
		[Tooltip("Event to send on touch began.")]
		public FsmEvent touchBegan;

		// Token: 0x04008DE5 RID: 36325
		[Tooltip("Event to send on touch moved.")]
		public FsmEvent touchMoved;

		// Token: 0x04008DE6 RID: 36326
		[Tooltip("Event to send on stationary touch.")]
		public FsmEvent touchStationary;

		// Token: 0x04008DE7 RID: 36327
		[Tooltip("Event to send on touch ended.")]
		public FsmEvent touchEnded;

		// Token: 0x04008DE8 RID: 36328
		[Tooltip("Event to send on touch cancel.")]
		public FsmEvent touchCanceled;

		// Token: 0x04008DE9 RID: 36329
		[ActionSection("Store Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fingerId of the touch.")]
		public FsmInt storeFingerId;

		// Token: 0x04008DEA RID: 36330
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the world position where the object was touched.")]
		public FsmVector3 storeHitPoint;

		// Token: 0x04008DEB RID: 36331
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the surface normal vector where the object was touched.")]
		public FsmVector3 storeHitNormal;
	}
}
