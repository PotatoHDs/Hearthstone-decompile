using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9E RID: 3230
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Performs a Hit Test on a Game Object with a GUITexture or GUIText component.")]
	[Obsolete("GUIElement is part of the legacy UI system and will be removed in a future release")]
	public class GUIElementHitTest : FsmStateAction
	{
		// Token: 0x0600A042 RID: 41026 RVA: 0x003306DC File Offset: 0x0032E8DC
		public override void Reset()
		{
			this.gameObject = null;
			this.camera = null;
			this.screenPoint = new FsmVector3
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
			this.normalized = true;
			this.hitEvent = null;
			this.everyFrame = true;
		}

		// Token: 0x0600A043 RID: 41027 RVA: 0x0033074C File Offset: 0x0032E94C
		public override void OnEnter()
		{
			this.DoHitTest();
			if (!this.everyFrame.Value)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A044 RID: 41028 RVA: 0x00330767 File Offset: 0x0032E967
		public override void OnUpdate()
		{
			this.DoHitTest();
		}

		// Token: 0x0600A045 RID: 41029 RVA: 0x00330770 File Offset: 0x0032E970
		private void DoHitTest()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.gameObjectCached)
			{
				this.guiElement = (ownerDefaultTarget.GetComponent<GUITexture>() ?? ownerDefaultTarget.GetComponent<GUIText>());
				this.gameObjectCached = ownerDefaultTarget;
			}
			if (this.guiElement == null)
			{
				base.Finish();
				return;
			}
			Vector3 screenPosition = this.screenPoint.IsNone ? new Vector3(0f, 0f) : this.screenPoint.Value;
			if (!this.screenX.IsNone)
			{
				screenPosition.x = this.screenX.Value;
			}
			if (!this.screenY.IsNone)
			{
				screenPosition.y = this.screenY.Value;
			}
			if (this.normalized.Value)
			{
				screenPosition.x *= (float)Screen.width;
				screenPosition.y *= (float)Screen.height;
			}
			if (this.guiElement.HitTest(screenPosition, this.camera))
			{
				this.storeResult.Value = true;
				base.Fsm.Event(this.hitEvent);
				return;
			}
			this.storeResult.Value = false;
		}

		// Token: 0x040085BE RID: 34238
		[RequiredField]
		[CheckForComponent(typeof(GUIElement))]
		[Tooltip("The GameObject that has a GUITexture or GUIText component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040085BF RID: 34239
		[Tooltip("Specify camera or use MainCamera as default.")]
		public Camera camera;

		// Token: 0x040085C0 RID: 34240
		[Tooltip("A vector position on screen. Usually stored by actions like GetTouchInfo, or World To Screen Point.")]
		public FsmVector3 screenPoint;

		// Token: 0x040085C1 RID: 34241
		[Tooltip("Specify screen X coordinate.")]
		public FsmFloat screenX;

		// Token: 0x040085C2 RID: 34242
		[Tooltip("Specify screen Y coordinate.")]
		public FsmFloat screenY;

		// Token: 0x040085C3 RID: 34243
		[Tooltip("Whether the specified screen coordinates are normalized (0-1).")]
		public FsmBool normalized;

		// Token: 0x040085C4 RID: 34244
		[Tooltip("Event to send if the Hit Test is true.")]
		public FsmEvent hitEvent;

		// Token: 0x040085C5 RID: 34245
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the Hit Test in a bool variable (true/false).")]
		public FsmBool storeResult;

		// Token: 0x040085C6 RID: 34246
		[Tooltip("Repeat every frame. Useful if you want to wait for the hit test to return true.")]
		public FsmBool everyFrame;

		// Token: 0x040085C7 RID: 34247
		private GUIElement guiElement;

		// Token: 0x040085C8 RID: 34248
		private GameObject gameObjectCached;
	}
}
