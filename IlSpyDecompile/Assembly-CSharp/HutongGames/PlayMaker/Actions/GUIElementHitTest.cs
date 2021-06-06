using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Performs a Hit Test on a Game Object with a GUITexture or GUIText component.")]
	[Obsolete("GUIElement is part of the legacy UI system and will be removed in a future release")]
	public class GUIElementHitTest : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(GUIElement))]
		[Tooltip("The GameObject that has a GUITexture or GUIText component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Specify camera or use MainCamera as default.")]
		public Camera camera;

		[Tooltip("A vector position on screen. Usually stored by actions like GetTouchInfo, or World To Screen Point.")]
		public FsmVector3 screenPoint;

		[Tooltip("Specify screen X coordinate.")]
		public FsmFloat screenX;

		[Tooltip("Specify screen Y coordinate.")]
		public FsmFloat screenY;

		[Tooltip("Whether the specified screen coordinates are normalized (0-1).")]
		public FsmBool normalized;

		[Tooltip("Event to send if the Hit Test is true.")]
		public FsmEvent hitEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the Hit Test in a bool variable (true/false).")]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame. Useful if you want to wait for the hit test to return true.")]
		public FsmBool everyFrame;

		private GUIElement guiElement;

		private GameObject gameObjectCached;

		public override void Reset()
		{
			gameObject = null;
			camera = null;
			screenPoint = new FsmVector3
			{
				UseVariable = true
			};
			screenX = new FsmFloat
			{
				UseVariable = true
			};
			screenY = new FsmFloat
			{
				UseVariable = true
			};
			normalized = true;
			hitEvent = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoHitTest();
			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoHitTest();
		}

		private void DoHitTest()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != gameObjectCached)
			{
				guiElement = (GUIElement)(((object)ownerDefaultTarget.GetComponent<GUITexture>()) ?? ((object)ownerDefaultTarget.GetComponent<GUIText>()));
				gameObjectCached = ownerDefaultTarget;
			}
			if (guiElement == null)
			{
				Finish();
				return;
			}
			Vector3 screenPosition = (screenPoint.IsNone ? new Vector3(0f, 0f) : screenPoint.Value);
			if (!screenX.IsNone)
			{
				screenPosition.x = screenX.Value;
			}
			if (!screenY.IsNone)
			{
				screenPosition.y = screenY.Value;
			}
			if (normalized.Value)
			{
				screenPosition.x *= Screen.width;
				screenPosition.y *= Screen.height;
			}
			if (guiElement.HitTest(screenPosition, camera))
			{
				storeResult.Value = true;
				base.Fsm.Event(hitEvent);
			}
			else
			{
				storeResult.Value = false;
			}
		}
	}
}
