using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D56 RID: 3414
	[ActionCategory("RectTransform")]
	[Tooltip("Get the normalized position in the parent RectTransform that the upper right corner is anchored to.")]
	public class RectTransformGetAnchorMax : BaseUpdateAction
	{
		// Token: 0x0600A3B5 RID: 41909 RVA: 0x0034042C File Offset: 0x0033E62C
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMax = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3B6 RID: 41910 RVA: 0x00340450 File Offset: 0x0033E650
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

		// Token: 0x0600A3B7 RID: 41911 RVA: 0x00340498 File Offset: 0x0033E698
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3B8 RID: 41912 RVA: 0x003404A0 File Offset: 0x0033E6A0
		private void DoGetValues()
		{
			if (!this.anchorMax.IsNone)
			{
				this.anchorMax.Value = this._rt.anchorMax;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.anchorMax.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.anchorMax.y;
			}
		}

		// Token: 0x04008A08 RID: 35336
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A09 RID: 35337
		[Tooltip("The anchorMax")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 anchorMax;

		// Token: 0x04008A0A RID: 35338
		[Tooltip("The x component of the anchorMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A0B RID: 35339
		[Tooltip("The y component of the anchorMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A0C RID: 35340
		private RectTransform _rt;
	}
}
