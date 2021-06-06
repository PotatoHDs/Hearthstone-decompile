using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D53 RID: 3411
	[ActionCategory("RectTransform")]
	[Tooltip("Check if a RectTransform contains the screen point as seen from the given camera.")]
	public class RectTransformContainsScreenPoint : FsmStateAction
	{
		// Token: 0x0600A3A7 RID: 41895 RVA: 0x00340084 File Offset: 0x0033E284
		public override void Reset()
		{
			this.gameObject = null;
			this.screenPointVector2 = null;
			this.orScreenPointVector3 = new FsmVector3
			{
				UseVariable = true
			};
			this.normalizedScreenPoint = false;
			this.camera = null;
			this.everyFrame = false;
			this.isContained = null;
			this.isContainedEvent = null;
			this.isNotContainedEvent = null;
		}

		// Token: 0x0600A3A8 RID: 41896 RVA: 0x003400DC File Offset: 0x0033E2DC
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

		// Token: 0x0600A3A9 RID: 41897 RVA: 0x00340159 File Offset: 0x0033E359
		public override void OnUpdate()
		{
			this.DoCheck();
		}

		// Token: 0x0600A3AA RID: 41898 RVA: 0x00340164 File Offset: 0x0033E364
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
			bool flag = RectTransformUtility.RectangleContainsScreenPoint(this._rt, value, this._camera);
			if (!this.isContained.IsNone)
			{
				this.isContained.Value = flag;
			}
			if (flag)
			{
				if (this.isContainedEvent != null)
				{
					base.Fsm.Event(this.isContainedEvent);
					return;
				}
			}
			else if (this.isNotContainedEvent != null)
			{
				base.Fsm.Event(this.isNotContainedEvent);
			}
		}

		// Token: 0x040089F4 RID: 35316
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040089F5 RID: 35317
		[Tooltip("The screenPoint as a Vector2. Leave to none if you want to use the Vector3 alternative")]
		public FsmVector2 screenPointVector2;

		// Token: 0x040089F6 RID: 35318
		[Tooltip("The screenPoint as a Vector3. Leave to none if you want to use the Vector2 alternative")]
		public FsmVector3 orScreenPointVector3;

		// Token: 0x040089F7 RID: 35319
		[Tooltip("Define if screenPoint are expressed as normalized screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public bool normalizedScreenPoint;

		// Token: 0x040089F8 RID: 35320
		[Tooltip("The Camera. For a RectTransform in a Canvas set to Screen Space - Overlay mode, the cam parameter should be set to null explicitly (default).\nLeave to none and the camera will be the one from EventSystem.current.camera")]
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject camera;

		// Token: 0x040089F9 RID: 35321
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x040089FA RID: 35322
		[ActionSection("Result")]
		[Tooltip("Store the result.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isContained;

		// Token: 0x040089FB RID: 35323
		[Tooltip("Event sent if screenPoint is contained in RectTransform.")]
		public FsmEvent isContainedEvent;

		// Token: 0x040089FC RID: 35324
		[Tooltip("Event sent if screenPoint is NOT contained in RectTransform.")]
		public FsmEvent isNotContainedEvent;

		// Token: 0x040089FD RID: 35325
		private RectTransform _rt;

		// Token: 0x040089FE RID: 35326
		private Camera _camera;
	}
}
