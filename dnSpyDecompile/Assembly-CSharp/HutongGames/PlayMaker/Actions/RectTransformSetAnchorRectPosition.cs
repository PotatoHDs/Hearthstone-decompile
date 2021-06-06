using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D68 RID: 3432
	[ActionCategory("RectTransform")]
	[Tooltip("The position ( normalized or not) in the parent RectTransform keeping the anchor rect size intact. This lets you position the whole Rect in one go. Use this to easily animate movement (like IOS sliding UIView)")]
	public class RectTransformSetAnchorRectPosition : BaseUpdateAction
	{
		// Token: 0x0600A40F RID: 41999 RVA: 0x00341AD0 File Offset: 0x0033FCD0
		public override void Reset()
		{
			base.Reset();
			this.normalized = true;
			this.gameObject = null;
			this.anchorReference = RectTransformSetAnchorRectPosition.AnchorReference.BottomLeft;
			this.anchor = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A410 RID: 42000 RVA: 0x00341B28 File Offset: 0x0033FD28
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetAnchor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A411 RID: 42001 RVA: 0x00341B70 File Offset: 0x0033FD70
		public override void OnActionUpdate()
		{
			this.DoSetAnchor();
		}

		// Token: 0x0600A412 RID: 42002 RVA: 0x00341B78 File Offset: 0x0033FD78
		private void DoSetAnchor()
		{
			this._anchorRect = default(Rect);
			this._anchorRect.min = this._rt.anchorMin;
			this._anchorRect.max = this._rt.anchorMax;
			Vector2 vector = Vector2.zero;
			vector = this._anchorRect.min;
			if (!this.anchor.IsNone)
			{
				if (this.normalized.Value)
				{
					vector = this.anchor.Value;
				}
				else
				{
					vector.x = this.anchor.Value.x / (float)Screen.width;
					vector.y = this.anchor.Value.y / (float)Screen.height;
				}
			}
			if (!this.x.IsNone)
			{
				if (this.normalized.Value)
				{
					vector.x = this.x.Value;
				}
				else
				{
					vector.x = this.x.Value / (float)Screen.width;
				}
			}
			if (!this.y.IsNone)
			{
				if (this.normalized.Value)
				{
					vector.y = this.y.Value;
				}
				else
				{
					vector.y = this.y.Value / (float)Screen.height;
				}
			}
			if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.BottomLeft)
			{
				this._anchorRect.x = vector.x;
				this._anchorRect.y = vector.y;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.Left)
			{
				this._anchorRect.x = vector.x;
				this._anchorRect.y = vector.y - 0.5f;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.TopLeft)
			{
				this._anchorRect.x = vector.x;
				this._anchorRect.y = vector.y - 1f;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.Top)
			{
				this._anchorRect.x = vector.x - 0.5f;
				this._anchorRect.y = vector.y - 1f;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.TopRight)
			{
				this._anchorRect.x = vector.x - 1f;
				this._anchorRect.y = vector.y - 1f;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.Right)
			{
				this._anchorRect.x = vector.x - 1f;
				this._anchorRect.y = vector.y - 0.5f;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.BottomRight)
			{
				this._anchorRect.x = vector.x - 1f;
				this._anchorRect.y = vector.y;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.Bottom)
			{
				this._anchorRect.x = vector.x - 0.5f;
				this._anchorRect.y = vector.y;
			}
			else if (this.anchorReference == RectTransformSetAnchorRectPosition.AnchorReference.Center)
			{
				this._anchorRect.x = vector.x - 0.5f;
				this._anchorRect.y = vector.y - 0.5f;
			}
			this._rt.anchorMin = this._anchorRect.min;
			this._rt.anchorMax = this._anchorRect.max;
		}

		// Token: 0x04008A7E RID: 35454
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A7F RID: 35455
		[Tooltip("The reference for the given position")]
		public RectTransformSetAnchorRectPosition.AnchorReference anchorReference;

		// Token: 0x04008A80 RID: 35456
		[Tooltip("Are the supplied screen coordinates normalized (0-1), or in pixels.")]
		public FsmBool normalized;

		// Token: 0x04008A81 RID: 35457
		[Tooltip("The Vector2 position, and/or set individual axis below.")]
		public FsmVector2 anchor;

		// Token: 0x04008A82 RID: 35458
		[HasFloatSlider(0f, 1f)]
		public FsmFloat x;

		// Token: 0x04008A83 RID: 35459
		[HasFloatSlider(0f, 1f)]
		public FsmFloat y;

		// Token: 0x04008A84 RID: 35460
		private RectTransform _rt;

		// Token: 0x04008A85 RID: 35461
		private Rect _anchorRect;

		// Token: 0x020027A3 RID: 10147
		public enum AnchorReference
		{
			// Token: 0x0400F4F8 RID: 62712
			TopLeft,
			// Token: 0x0400F4F9 RID: 62713
			Top,
			// Token: 0x0400F4FA RID: 62714
			TopRight,
			// Token: 0x0400F4FB RID: 62715
			Right,
			// Token: 0x0400F4FC RID: 62716
			BottomRight,
			// Token: 0x0400F4FD RID: 62717
			Bottom,
			// Token: 0x0400F4FE RID: 62718
			BottomLeft,
			// Token: 0x0400F4FF RID: 62719
			Left,
			// Token: 0x0400F500 RID: 62720
			Center
		}
	}
}
