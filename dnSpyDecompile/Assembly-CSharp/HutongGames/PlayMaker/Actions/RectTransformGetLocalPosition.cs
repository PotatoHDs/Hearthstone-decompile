using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D59 RID: 3417
	[ActionCategory("RectTransform")]
	[Tooltip("Get the Local position of this RectTransform. This is Screen Space values using the anchoring as reference, so 0,0 is the center of the screen if the anchor is te center of the screen.")]
	public class RectTransformGetLocalPosition : BaseUpdateAction
	{
		// Token: 0x0600A3C4 RID: 41924 RVA: 0x00340793 File Offset: 0x0033E993
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.reference = RectTransformGetLocalPosition.LocalPositionReference.Anchor;
			this.position = null;
			this.position2d = null;
			this.x = null;
			this.y = null;
			this.z = null;
		}

		// Token: 0x0600A3C5 RID: 41925 RVA: 0x003407CC File Offset: 0x0033E9CC
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

		// Token: 0x0600A3C6 RID: 41926 RVA: 0x00340814 File Offset: 0x0033EA14
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3C7 RID: 41927 RVA: 0x0034081C File Offset: 0x0033EA1C
		private void DoGetValues()
		{
			if (this._rt == null)
			{
				return;
			}
			Vector3 localPosition = this._rt.localPosition;
			if (this.reference == RectTransformGetLocalPosition.LocalPositionReference.CenterPosition)
			{
				localPosition.x += this._rt.rect.center.x;
				localPosition.y += this._rt.rect.center.y;
			}
			if (!this.position.IsNone)
			{
				this.position.Value = localPosition;
			}
			if (!this.position2d.IsNone)
			{
				this.position2d.Value = new Vector2(localPosition.x, localPosition.y);
			}
			if (!this.x.IsNone)
			{
				this.x.Value = localPosition.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = localPosition.y;
			}
			if (!this.z.IsNone)
			{
				this.z.Value = localPosition.z;
			}
		}

		// Token: 0x04008A1A RID: 35354
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A1B RID: 35355
		public RectTransformGetLocalPosition.LocalPositionReference reference;

		// Token: 0x04008A1C RID: 35356
		[Tooltip("The position")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;

		// Token: 0x04008A1D RID: 35357
		[Tooltip("The position in a Vector 2d ")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 position2d;

		// Token: 0x04008A1E RID: 35358
		[Tooltip("The x component of the Position")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A1F RID: 35359
		[Tooltip("The y component of the Position")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A20 RID: 35360
		[Tooltip("The z component of the Position")]
		[UIHint(UIHint.Variable)]
		public FsmFloat z;

		// Token: 0x04008A21 RID: 35361
		private RectTransform _rt;

		// Token: 0x020027A2 RID: 10146
		public enum LocalPositionReference
		{
			// Token: 0x0400F4F5 RID: 62709
			Anchor,
			// Token: 0x0400F4F6 RID: 62710
			CenterPosition
		}
	}
}
