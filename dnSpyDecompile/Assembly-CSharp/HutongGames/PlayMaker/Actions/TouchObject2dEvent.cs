using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D23 RID: 3363
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events when a 2d object is touched. Optionally filter by a fingerID. NOTE: Uses the MainCamera!")]
	public class TouchObject2dEvent : FsmStateAction
	{
		// Token: 0x0600A2A9 RID: 41641 RVA: 0x0033D150 File Offset: 0x0033B350
		public override void Reset()
		{
			this.gameObject = null;
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
		}

		// Token: 0x0600A2AA RID: 41642 RVA: 0x0033D1A8 File Offset: 0x0033B3A8
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
						RaycastHit2D rayIntersection = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(position), float.PositiveInfinity);
						Fsm.RecordLastRaycastHit2DInfo(base.Fsm, rayIntersection);
						if (rayIntersection.transform != null && rayIntersection.transform.gameObject == ownerDefaultTarget)
						{
							this.storeFingerId.Value = touch.fingerId;
							this.storeHitPoint.Value = rayIntersection.point;
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

		// Token: 0x04008917 RID: 35095
		[RequiredField]
		[CheckForComponent(typeof(Collider2D))]
		[Tooltip("The Game Object to detect touches on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008918 RID: 35096
		[Tooltip("Only detect touches that match this fingerID, or set to None.")]
		public FsmInt fingerId;

		// Token: 0x04008919 RID: 35097
		[ActionSection("Events")]
		[Tooltip("Event to send on touch began.")]
		public FsmEvent touchBegan;

		// Token: 0x0400891A RID: 35098
		[Tooltip("Event to send on touch moved.")]
		public FsmEvent touchMoved;

		// Token: 0x0400891B RID: 35099
		[Tooltip("Event to send on stationary touch.")]
		public FsmEvent touchStationary;

		// Token: 0x0400891C RID: 35100
		[Tooltip("Event to send on touch ended.")]
		public FsmEvent touchEnded;

		// Token: 0x0400891D RID: 35101
		[Tooltip("Event to send on touch cancel.")]
		public FsmEvent touchCanceled;

		// Token: 0x0400891E RID: 35102
		[ActionSection("Store Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fingerId of the touch.")]
		public FsmInt storeFingerId;

		// Token: 0x0400891F RID: 35103
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the 2d position where the object was touched.")]
		public FsmVector2 storeHitPoint;
	}
}
