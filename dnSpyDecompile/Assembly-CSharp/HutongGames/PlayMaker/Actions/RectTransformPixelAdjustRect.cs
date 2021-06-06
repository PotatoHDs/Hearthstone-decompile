using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D61 RID: 3425
	[ActionCategory("RectTransform")]
	[Tooltip("Given a rect transform, return the corner points in pixel accurate coordinates.")]
	public class RectTransformPixelAdjustRect : BaseUpdateAction
	{
		// Token: 0x0600A3EC RID: 41964 RVA: 0x00341101 File Offset: 0x0033F301
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.canvas = new FsmGameObject
			{
				UseVariable = true
			};
			this.pixelRect = null;
		}

		// Token: 0x0600A3ED RID: 41965 RVA: 0x0034112C File Offset: 0x0033F32C
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

		// Token: 0x0600A3EE RID: 41966 RVA: 0x003411C8 File Offset: 0x0033F3C8
		public override void OnActionUpdate()
		{
			this.DoAction();
		}

		// Token: 0x0600A3EF RID: 41967 RVA: 0x003411D0 File Offset: 0x0033F3D0
		private void DoAction()
		{
			this.pixelRect.Value = RectTransformUtility.PixelAdjustRect(this._rt, this._canvas);
		}

		// Token: 0x04008A49 RID: 35401
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A4A RID: 35402
		[RequiredField]
		[CheckForComponent(typeof(Canvas))]
		[Tooltip("The canvas. Leave to none to use the canvas of the gameObject")]
		public FsmGameObject canvas;

		// Token: 0x04008A4B RID: 35403
		[ActionSection("Result")]
		[RequiredField]
		[Tooltip("Pixel adjusted rect.")]
		[UIHint(UIHint.Variable)]
		public FsmRect pixelRect;

		// Token: 0x04008A4C RID: 35404
		private RectTransform _rt;

		// Token: 0x04008A4D RID: 35405
		private Canvas _canvas;
	}
}
