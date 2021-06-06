using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6F RID: 3439
	[ActionCategory("RectTransform")]
	[Tooltip("RectTransforms position from world space into screen space. Leave the camera to none for default behavior")]
	public class RectTransformWorldToScreenPoint : BaseUpdateAction
	{
		// Token: 0x0600A432 RID: 42034 RVA: 0x00342604 File Offset: 0x00340804
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.camera = new FsmOwnerDefault();
			this.camera.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			this.camera.GameObject = new FsmGameObject
			{
				UseVariable = true
			};
			this.screenPoint = null;
			this.screenX = null;
			this.screenY = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A433 RID: 42035 RVA: 0x00342668 File Offset: 0x00340868
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			if (base.Fsm.GetOwnerDefaultTarget(this.camera) != null)
			{
				this._cam = ownerDefaultTarget.GetComponent<Camera>();
			}
			this.DoWorldToScreenPoint();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A434 RID: 42036 RVA: 0x003426D5 File Offset: 0x003408D5
		public override void OnActionUpdate()
		{
			this.DoWorldToScreenPoint();
		}

		// Token: 0x0600A435 RID: 42037 RVA: 0x003426E0 File Offset: 0x003408E0
		private void DoWorldToScreenPoint()
		{
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(this._cam, this._rt.position);
			if (this.normalize.Value)
			{
				vector.x /= (float)Screen.width;
				vector.y /= (float)Screen.height;
			}
			this.screenPoint.Value = vector;
			this.screenX.Value = vector.x;
			this.screenY.Value = vector.y;
		}

		// Token: 0x04008AA7 RID: 35495
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008AA8 RID: 35496
		[CheckForComponent(typeof(Camera))]
		[Tooltip("The camera to perform the calculation. Leave to none for default behavior")]
		public FsmOwnerDefault camera;

		// Token: 0x04008AA9 RID: 35497
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen position in a Vector3 Variable. Z will equal zero.")]
		public FsmVector3 screenPoint;

		// Token: 0x04008AAA RID: 35498
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen X position in a Float Variable.")]
		public FsmFloat screenX;

		// Token: 0x04008AAB RID: 35499
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the screen Y position in a Float Variable.")]
		public FsmFloat screenY;

		// Token: 0x04008AAC RID: 35500
		[Tooltip("Normalize screen coordinates (0-1). Otherwise coordinates are in pixels.")]
		public FsmBool normalize;

		// Token: 0x04008AAD RID: 35501
		private RectTransform _rt;

		// Token: 0x04008AAE RID: 35502
		private Camera _cam;
	}
}
