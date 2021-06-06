using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1F RID: 3615
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events when a GUI Texture or GUI Text is touched. Optionally filter by a fingerID.")]
	[Obsolete("GUIElement is part of the legacy UI system and will be removed in a future release")]
	public class TouchGUIEvent : FsmStateAction
	{
		// Token: 0x0600A748 RID: 42824 RVA: 0x0034BFC8 File Offset: 0x0034A1C8
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
			this.normalizeHitPoint = false;
			this.storeOffset = null;
			this.relativeTo = TouchGUIEvent.OffsetOptions.Center;
			this.normalizeOffset = true;
			this.everyFrame = true;
		}

		// Token: 0x0600A749 RID: 42825 RVA: 0x0034C04C File Offset: 0x0034A24C
		public override void OnEnter()
		{
			this.DoTouchGUIEvent();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A74A RID: 42826 RVA: 0x0034C062 File Offset: 0x0034A262
		public override void OnUpdate()
		{
			this.DoTouchGUIEvent();
		}

		// Token: 0x0600A74B RID: 42827 RVA: 0x0034C06C File Offset: 0x0034A26C
		private void DoTouchGUIEvent()
		{
			if (Input.touchCount > 0)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
				if (ownerDefaultTarget == null)
				{
					return;
				}
				this.guiElement = (ownerDefaultTarget.GetComponent<GUITexture>() ?? ownerDefaultTarget.GetComponent<GUIText>());
				if (this.guiElement == null)
				{
					return;
				}
				foreach (Touch touch in Input.touches)
				{
					this.DoTouch(touch);
				}
			}
		}

		// Token: 0x0600A74C RID: 42828 RVA: 0x0034C0E8 File Offset: 0x0034A2E8
		private void DoTouch(Touch touch)
		{
			if (this.fingerId.IsNone || touch.fingerId == this.fingerId.Value)
			{
				Vector3 vector = touch.position;
				if (this.guiElement.HitTest(vector))
				{
					if (touch.phase == TouchPhase.Began)
					{
						this.touchStartPos = vector;
					}
					this.storeFingerId.Value = touch.fingerId;
					if (this.normalizeHitPoint.Value)
					{
						vector.x /= (float)Screen.width;
						vector.y /= (float)Screen.height;
					}
					this.storeHitPoint.Value = vector;
					this.DoTouchOffset(vector);
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
					default:
						return;
					}
				}
				else
				{
					base.Fsm.Event(this.notTouching);
				}
			}
		}

		// Token: 0x0600A74D RID: 42829 RVA: 0x0034C22C File Offset: 0x0034A42C
		private void DoTouchOffset(Vector3 touchPos)
		{
			if (this.storeOffset.IsNone)
			{
				return;
			}
			Rect screenRect = this.guiElement.GetScreenRect();
			Vector3 value = default(Vector3);
			switch (this.relativeTo)
			{
			case TouchGUIEvent.OffsetOptions.TopLeft:
				value.x = touchPos.x - screenRect.x;
				value.y = touchPos.y - screenRect.y;
				break;
			case TouchGUIEvent.OffsetOptions.Center:
			{
				Vector3 b = new Vector3(screenRect.x + screenRect.width * 0.5f, screenRect.y + screenRect.height * 0.5f, 0f);
				value = touchPos - b;
				break;
			}
			case TouchGUIEvent.OffsetOptions.TouchStart:
				value = touchPos - this.touchStartPos;
				break;
			}
			if (this.normalizeOffset.Value)
			{
				value.x /= screenRect.width;
				value.y /= screenRect.height;
			}
			this.storeOffset.Value = value;
		}

		// Token: 0x04008DD0 RID: 36304
		[RequiredField]
		[CheckForComponent(typeof(GUIElement))]
		[Tooltip("The Game Object that owns the GUI Texture or GUI Text.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008DD1 RID: 36305
		[Tooltip("Only detect touches that match this fingerID, or set to None.")]
		public FsmInt fingerId;

		// Token: 0x04008DD2 RID: 36306
		[ActionSection("Events")]
		[Tooltip("Event to send on touch began.")]
		public FsmEvent touchBegan;

		// Token: 0x04008DD3 RID: 36307
		[Tooltip("Event to send on touch moved.")]
		public FsmEvent touchMoved;

		// Token: 0x04008DD4 RID: 36308
		[Tooltip("Event to send on stationary touch.")]
		public FsmEvent touchStationary;

		// Token: 0x04008DD5 RID: 36309
		[Tooltip("Event to send on touch ended.")]
		public FsmEvent touchEnded;

		// Token: 0x04008DD6 RID: 36310
		[Tooltip("Event to send on touch cancel.")]
		public FsmEvent touchCanceled;

		// Token: 0x04008DD7 RID: 36311
		[Tooltip("Event to send if not touching (finger down but not over the GUI element)")]
		public FsmEvent notTouching;

		// Token: 0x04008DD8 RID: 36312
		[ActionSection("Store Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fingerId of the touch.")]
		public FsmInt storeFingerId;

		// Token: 0x04008DD9 RID: 36313
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen position where the GUI element was touched.")]
		public FsmVector3 storeHitPoint;

		// Token: 0x04008DDA RID: 36314
		[Tooltip("Normalize the hit point screen coordinates (0-1).")]
		public FsmBool normalizeHitPoint;

		// Token: 0x04008DDB RID: 36315
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the offset position of the hit.")]
		public FsmVector3 storeOffset;

		// Token: 0x04008DDC RID: 36316
		[Tooltip("How to measure the offset.")]
		public TouchGUIEvent.OffsetOptions relativeTo;

		// Token: 0x04008DDD RID: 36317
		[Tooltip("Normalize the offset.")]
		public FsmBool normalizeOffset;

		// Token: 0x04008DDE RID: 36318
		[ActionSection("")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008DDF RID: 36319
		private Vector3 touchStartPos;

		// Token: 0x04008DE0 RID: 36320
		private GUIElement guiElement;

		// Token: 0x020027AD RID: 10157
		public enum OffsetOptions
		{
			// Token: 0x0400F52E RID: 62766
			TopLeft,
			// Token: 0x0400F52F RID: 62767
			Center,
			// Token: 0x0400F530 RID: 62768
			TouchStart
		}
	}
}
