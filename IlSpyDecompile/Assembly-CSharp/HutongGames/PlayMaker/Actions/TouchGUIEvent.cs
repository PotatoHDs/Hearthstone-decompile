using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events when a GUI Texture or GUI Text is touched. Optionally filter by a fingerID.")]
	[Obsolete("GUIElement is part of the legacy UI system and will be removed in a future release")]
	public class TouchGUIEvent : FsmStateAction
	{
		public enum OffsetOptions
		{
			TopLeft,
			Center,
			TouchStart
		}

		[RequiredField]
		[CheckForComponent(typeof(GUIElement))]
		[Tooltip("The Game Object that owns the GUI Texture or GUI Text.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Only detect touches that match this fingerID, or set to None.")]
		public FsmInt fingerId;

		[ActionSection("Events")]
		[Tooltip("Event to send on touch began.")]
		public FsmEvent touchBegan;

		[Tooltip("Event to send on touch moved.")]
		public FsmEvent touchMoved;

		[Tooltip("Event to send on stationary touch.")]
		public FsmEvent touchStationary;

		[Tooltip("Event to send on touch ended.")]
		public FsmEvent touchEnded;

		[Tooltip("Event to send on touch cancel.")]
		public FsmEvent touchCanceled;

		[Tooltip("Event to send if not touching (finger down but not over the GUI element)")]
		public FsmEvent notTouching;

		[ActionSection("Store Results")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fingerId of the touch.")]
		public FsmInt storeFingerId;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen position where the GUI element was touched.")]
		public FsmVector3 storeHitPoint;

		[Tooltip("Normalize the hit point screen coordinates (0-1).")]
		public FsmBool normalizeHitPoint;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the offset position of the hit.")]
		public FsmVector3 storeOffset;

		[Tooltip("How to measure the offset.")]
		public OffsetOptions relativeTo;

		[Tooltip("Normalize the offset.")]
		public FsmBool normalizeOffset;

		[ActionSection("")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private Vector3 touchStartPos;

		private GUIElement guiElement;

		public override void Reset()
		{
			gameObject = null;
			fingerId = new FsmInt
			{
				UseVariable = true
			};
			touchBegan = null;
			touchMoved = null;
			touchStationary = null;
			touchEnded = null;
			touchCanceled = null;
			storeFingerId = null;
			storeHitPoint = null;
			normalizeHitPoint = false;
			storeOffset = null;
			relativeTo = OffsetOptions.Center;
			normalizeOffset = true;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoTouchGUIEvent();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoTouchGUIEvent();
		}

		private void DoTouchGUIEvent()
		{
			if (Input.touchCount <= 0)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			guiElement = (GUIElement)(((object)ownerDefaultTarget.GetComponent<GUITexture>()) ?? ((object)ownerDefaultTarget.GetComponent<GUIText>()));
			if (!(guiElement == null))
			{
				Touch[] touches = Input.touches;
				foreach (Touch touch in touches)
				{
					DoTouch(touch);
				}
			}
		}

		private void DoTouch(Touch touch)
		{
			if (!fingerId.IsNone && touch.fingerId != fingerId.Value)
			{
				return;
			}
			Vector3 vector = touch.position;
			if (guiElement.HitTest(vector))
			{
				if (touch.phase == TouchPhase.Began)
				{
					touchStartPos = vector;
				}
				storeFingerId.Value = touch.fingerId;
				if (normalizeHitPoint.Value)
				{
					vector.x /= Screen.width;
					vector.y /= Screen.height;
				}
				storeHitPoint.Value = vector;
				DoTouchOffset(vector);
				switch (touch.phase)
				{
				case TouchPhase.Began:
					base.Fsm.Event(touchBegan);
					break;
				case TouchPhase.Moved:
					base.Fsm.Event(touchMoved);
					break;
				case TouchPhase.Stationary:
					base.Fsm.Event(touchStationary);
					break;
				case TouchPhase.Ended:
					base.Fsm.Event(touchEnded);
					break;
				case TouchPhase.Canceled:
					base.Fsm.Event(touchCanceled);
					break;
				}
			}
			else
			{
				base.Fsm.Event(notTouching);
			}
		}

		private void DoTouchOffset(Vector3 touchPos)
		{
			if (!storeOffset.IsNone)
			{
				Rect screenRect = guiElement.GetScreenRect();
				Vector3 value = default(Vector3);
				switch (relativeTo)
				{
				case OffsetOptions.TopLeft:
					value.x = touchPos.x - screenRect.x;
					value.y = touchPos.y - screenRect.y;
					break;
				case OffsetOptions.Center:
				{
					Vector3 vector = new Vector3(screenRect.x + screenRect.width * 0.5f, screenRect.y + screenRect.height * 0.5f, 0f);
					value = touchPos - vector;
					break;
				}
				case OffsetOptions.TouchStart:
					value = touchPos - touchStartPos;
					break;
				}
				if (normalizeOffset.Value)
				{
					value.x /= screenRect.width;
					value.y /= screenRect.height;
				}
				storeOffset.Value = value;
			}
		}
	}
}
