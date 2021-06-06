using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5C RID: 3420
	[ActionCategory("RectTransform")]
	[Tooltip("Get the offset of the lower left corner of the rectangle relative to the lower left anchor")]
	public class RectTransformGetOffsetMin : BaseUpdateAction
	{
		// Token: 0x0600A3D3 RID: 41939 RVA: 0x00340B90 File Offset: 0x0033ED90
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.offsetMin = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3D4 RID: 41940 RVA: 0x00340BB4 File Offset: 0x0033EDB4
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

		// Token: 0x0600A3D5 RID: 41941 RVA: 0x00340BFC File Offset: 0x0033EDFC
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3D6 RID: 41942 RVA: 0x00340C04 File Offset: 0x0033EE04
		private void DoGetValues()
		{
			if (!this.offsetMin.IsNone)
			{
				this.offsetMin.Value = this._rt.offsetMin;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.offsetMin.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.offsetMin.y;
			}
		}

		// Token: 0x04008A2D RID: 35373
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A2E RID: 35374
		[Tooltip("The offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 offsetMin;

		// Token: 0x04008A2F RID: 35375
		[Tooltip("The x component of the offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A30 RID: 35376
		[Tooltip("The y component of the offsetMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A31 RID: 35377
		private RectTransform _rt;
	}
}
