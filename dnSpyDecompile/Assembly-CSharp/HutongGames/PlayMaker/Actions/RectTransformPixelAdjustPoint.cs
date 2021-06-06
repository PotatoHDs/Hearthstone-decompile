using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D60 RID: 3424
	[ActionCategory("RectTransform")]
	[Tooltip("Convert a given point in screen space into a pixel correct point.")]
	public class RectTransformPixelAdjustPoint : BaseUpdateAction
	{
		// Token: 0x0600A3E7 RID: 41959 RVA: 0x00341004 File Offset: 0x0033F204
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.canvas = new FsmGameObject
			{
				UseVariable = true
			};
			this.screenPoint = null;
			this.pixelPoint = null;
		}

		// Token: 0x0600A3E8 RID: 41960 RVA: 0x00341034 File Offset: 0x0033F234
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			GameObject value = this.canvas.Value;
			if (value != null)
			{
				this._canvas = value.GetComponent<Canvas>();
			}
			if (this._canvas == null && ownerDefaultTarget != null)
			{
				Graphic component = ownerDefaultTarget.GetComponent<Graphic>();
				if (component != null)
				{
					this._canvas = component.canvas;
				}
			}
			this.DoAction();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3E9 RID: 41961 RVA: 0x003410D0 File Offset: 0x0033F2D0
		public override void OnActionUpdate()
		{
			this.DoAction();
		}

		// Token: 0x0600A3EA RID: 41962 RVA: 0x003410D8 File Offset: 0x0033F2D8
		private void DoAction()
		{
			this.pixelPoint.Value = RectTransformUtility.PixelAdjustPoint(this.screenPoint.Value, this._rt, this._canvas);
		}

		// Token: 0x04008A43 RID: 35395
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A44 RID: 35396
		[RequiredField]
		[CheckForComponent(typeof(Canvas))]
		[Tooltip("The canvas. Leave to none to use the canvas of the gameObject")]
		public FsmGameObject canvas;

		// Token: 0x04008A45 RID: 35397
		[Tooltip("The screen position.")]
		public FsmVector2 screenPoint;

		// Token: 0x04008A46 RID: 35398
		[ActionSection("Result")]
		[RequiredField]
		[Tooltip("Pixel adjusted point from the screen position.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 pixelPoint;

		// Token: 0x04008A47 RID: 35399
		private RectTransform _rt;

		// Token: 0x04008A48 RID: 35400
		private Canvas _canvas;
	}
}
