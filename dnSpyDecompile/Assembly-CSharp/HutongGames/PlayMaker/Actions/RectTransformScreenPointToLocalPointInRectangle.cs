using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D62 RID: 3426
	[ActionCategory("RectTransform")]
	[Tooltip("Transform a screen space point to a local position that is on the plane of the given RectTransform. Also check if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
	public class RectTransformScreenPointToLocalPointInRectangle : FsmStateAction
	{
		// Token: 0x0600A3F1 RID: 41969 RVA: 0x003411F0 File Offset: 0x0033F3F0
		public override void Reset()
		{
			this.gameObject = null;
			this.screenPointVector2 = null;
			this.orScreenPointVector3 = new FsmVector3
			{
				UseVariable = true
			};
			this.normalizedScreenPoint = false;
			this.camera = new FsmGameObject
			{
				UseVariable = true
			};
			this.everyFrame = false;
			this.localPosition = null;
			this.localPosition2d = null;
			this.isHit = null;
			this.hitEvent = null;
			this.noHitEvent = null;
		}

		// Token: 0x0600A3F2 RID: 41970 RVA: 0x00341260 File Offset: 0x0033F460
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			if (!this.camera.IsNone)
			{
				this._camera = this.camera.Value.GetComponent<Camera>();
			}
			else
			{
				this._camera = EventSystem.current.GetComponent<Camera>();
			}
			this.DoCheck();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3F3 RID: 41971 RVA: 0x003412DD File Offset: 0x0033F4DD
		public override void OnUpdate()
		{
			this.DoCheck();
		}

		// Token: 0x0600A3F4 RID: 41972 RVA: 0x003412E8 File Offset: 0x0033F4E8
		private void DoCheck()
		{
			if (this._rt == null)
			{
				return;
			}
			Vector2 value = this.screenPointVector2.Value;
			if (!this.orScreenPointVector3.IsNone)
			{
				value.x = this.orScreenPointVector3.Value.x;
				value.y = this.orScreenPointVector3.Value.y;
			}
			if (this.normalizedScreenPoint)
			{
				value.x *= (float)Screen.width;
				value.y *= (float)Screen.height;
			}
			Vector2 vector;
			bool flag = RectTransformUtility.ScreenPointToLocalPointInRectangle(this._rt, value, this._camera, out vector);
			if (!this.localPosition2d.IsNone)
			{
				this.localPosition2d.Value = vector;
			}
			if (!this.localPosition.IsNone)
			{
				this.localPosition.Value = new Vector3(vector.x, vector.y, 0f);
			}
			if (!this.isHit.IsNone)
			{
				this.isHit.Value = flag;
			}
			if (flag)
			{
				if (this.hitEvent != null)
				{
					base.Fsm.Event(this.hitEvent);
					return;
				}
			}
			else if (this.noHitEvent != null)
			{
				base.Fsm.Event(this.noHitEvent);
			}
		}

		// Token: 0x04008A4E RID: 35406
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A4F RID: 35407
		[Tooltip("The screenPoint as a Vector2. Leave to none if you want to use the Vector3 alternative")]
		public FsmVector2 screenPointVector2;

		// Token: 0x04008A50 RID: 35408
		[Tooltip("The screenPoint as a Vector3. Leave to none if you want to use the Vector2 alternative")]
		public FsmVector3 orScreenPointVector3;

		// Token: 0x04008A51 RID: 35409
		[Tooltip("Define if screenPoint are expressed as normalized screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public bool normalizedScreenPoint;

		// Token: 0x04008A52 RID: 35410
		[Tooltip("The Camera. For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be set to null explicitly (default).\nLeave to none and the camera will be the one from EventSystem.current.camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;

		// Token: 0x04008A53 RID: 35411
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x04008A54 RID: 35412
		[ActionSection("Result")]
		[Tooltip("Store the local Position as a vector3 of the screenPoint on the RectTransform Plane.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 localPosition;

		// Token: 0x04008A55 RID: 35413
		[Tooltip("Store the local Position as a vector2 of the screenPoint on the RectTransform Plane.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 localPosition2d;

		// Token: 0x04008A56 RID: 35414
		[Tooltip("True if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isHit;

		// Token: 0x04008A57 RID: 35415
		[Tooltip("Event sent if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle.")]
		public FsmEvent hitEvent;

		// Token: 0x04008A58 RID: 35416
		[Tooltip("Event sent if the plane of the RectTransform is NOT hit, regardless of whether the point is inside the rectangle.")]
		public FsmEvent noHitEvent;

		// Token: 0x04008A59 RID: 35417
		private RectTransform _rt;

		// Token: 0x04008A5A RID: 35418
		private Camera _camera;
	}
}
