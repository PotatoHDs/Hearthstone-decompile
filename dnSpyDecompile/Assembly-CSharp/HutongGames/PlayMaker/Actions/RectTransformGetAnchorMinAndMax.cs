using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D58 RID: 3416
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformGetAnchorMinAndMax : BaseUpdateAction
	{
		// Token: 0x0600A3BF RID: 41919 RVA: 0x00340614 File Offset: 0x0033E814
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMax = null;
			this.anchorMin = null;
			this.xMax = null;
			this.yMax = null;
			this.xMin = null;
			this.yMin = null;
		}

		// Token: 0x0600A3C0 RID: 41920 RVA: 0x00340650 File Offset: 0x0033E850
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoGetValues();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3C1 RID: 41921 RVA: 0x00340698 File Offset: 0x0033E898
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3C2 RID: 41922 RVA: 0x003406A0 File Offset: 0x0033E8A0
		private void DoGetValues()
		{
			if (!this.anchorMax.IsNone)
			{
				this.anchorMax.Value = this._rt.anchorMax;
			}
			if (!this.anchorMin.IsNone)
			{
				this.anchorMin.Value = this._rt.anchorMax;
			}
			if (!this.xMax.IsNone)
			{
				this.xMax.Value = this._rt.anchorMax.x;
			}
			if (!this.yMax.IsNone)
			{
				this.yMax.Value = this._rt.anchorMax.y;
			}
			if (!this.xMin.IsNone)
			{
				this.xMin.Value = this._rt.anchorMin.x;
			}
			if (!this.yMin.IsNone)
			{
				this.yMin.Value = this._rt.anchorMin.y;
			}
		}

		// Token: 0x04008A12 RID: 35346
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A13 RID: 35347
		[Tooltip("The Vector2 anchor max. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMax;

		// Token: 0x04008A14 RID: 35348
		[Tooltip("The Vector2 anchor min. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMin;

		// Token: 0x04008A15 RID: 35349
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat xMax;

		// Token: 0x04008A16 RID: 35350
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat yMax;

		// Token: 0x04008A17 RID: 35351
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat xMin;

		// Token: 0x04008A18 RID: 35352
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat yMin;

		// Token: 0x04008A19 RID: 35353
		private RectTransform _rt;
	}
}
