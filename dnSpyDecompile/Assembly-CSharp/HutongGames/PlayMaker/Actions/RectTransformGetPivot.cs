using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5D RID: 3421
	[ActionCategory("RectTransform")]
	[Tooltip("Get the normalized position in this RectTransform that it rotates around.")]
	public class RectTransformGetPivot : BaseUpdateAction
	{
		// Token: 0x0600A3D8 RID: 41944 RVA: 0x00340C84 File Offset: 0x0033EE84
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.pivot = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3D9 RID: 41945 RVA: 0x00340CA8 File Offset: 0x0033EEA8
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

		// Token: 0x0600A3DA RID: 41946 RVA: 0x00340CF0 File Offset: 0x0033EEF0
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3DB RID: 41947 RVA: 0x00340CF8 File Offset: 0x0033EEF8
		private void DoGetValues()
		{
			if (!this.pivot.IsNone)
			{
				this.pivot.Value = this._rt.pivot;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.pivot.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.pivot.y;
			}
		}

		// Token: 0x04008A32 RID: 35378
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A33 RID: 35379
		[Tooltip("The pivot")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 pivot;

		// Token: 0x04008A34 RID: 35380
		[Tooltip("The x component of the pivot")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A35 RID: 35381
		[Tooltip("The y component of the pivot")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A36 RID: 35382
		private RectTransform _rt;
	}
}
