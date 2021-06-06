using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D64 RID: 3428
	[ActionCategory("RectTransform")]
	[Tooltip("The position of the pivot of this RectTransform relative to the anchor reference point.The anchor reference point is where the anchors are. If the anchor are not together, the four anchor positions are interpolated according to the pivot normalized values.")]
	public class RectTransformSetAnchoredPosition : BaseUpdateAction
	{
		// Token: 0x0600A3FB RID: 41979 RVA: 0x00341617 File Offset: 0x0033F817
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.position = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A3FC RID: 41980 RVA: 0x00341654 File Offset: 0x0033F854
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetAnchoredPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A3FD RID: 41981 RVA: 0x0034169C File Offset: 0x0033F89C
		public override void OnActionUpdate()
		{
			this.DoSetAnchoredPosition();
		}

		// Token: 0x0600A3FE RID: 41982 RVA: 0x003416A4 File Offset: 0x0033F8A4
		private void DoSetAnchoredPosition()
		{
			Vector2 anchoredPosition = this._rt.anchoredPosition;
			if (!this.position.IsNone)
			{
				anchoredPosition = this.position.Value;
			}
			if (!this.x.IsNone)
			{
				anchoredPosition.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				anchoredPosition.y = this.y.Value;
			}
			this._rt.anchoredPosition = anchoredPosition;
		}

		// Token: 0x04008A67 RID: 35431
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A68 RID: 35432
		[Tooltip("The Vector2 position. Set to none for no effect, and/or set individual axis below. ")]
		public FsmVector2 position;

		// Token: 0x04008A69 RID: 35433
		[Tooltip("Setting only the x value. Overrides position x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A6A RID: 35434
		[Tooltip("Setting only the y value. Overrides position x value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A6B RID: 35435
		private RectTransform _rt;
	}
}
