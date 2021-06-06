using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5B RID: 3419
	[ActionCategory("RectTransform")]
	[Tooltip("Get the offset of the upper right corner of the rectangle relative to the upper right anchor")]
	public class RectTransformGetOffsetMax : BaseUpdateAction
	{
		// Token: 0x0600A3CE RID: 41934 RVA: 0x00340A9B File Offset: 0x0033EC9B
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.offsetMax = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3CF RID: 41935 RVA: 0x00340AC0 File Offset: 0x0033ECC0
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

		// Token: 0x0600A3D0 RID: 41936 RVA: 0x00340B08 File Offset: 0x0033ED08
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3D1 RID: 41937 RVA: 0x00340B10 File Offset: 0x0033ED10
		private void DoGetValues()
		{
			if (!this.offsetMax.IsNone)
			{
				this.offsetMax.Value = this._rt.offsetMax;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.offsetMax.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.offsetMax.y;
			}
		}

		// Token: 0x04008A28 RID: 35368
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A29 RID: 35369
		[Tooltip("The offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 offsetMax;

		// Token: 0x04008A2A RID: 35370
		[Tooltip("The x component of the offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A2B RID: 35371
		[Tooltip("The y component of the offsetMax")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A2C RID: 35372
		private RectTransform _rt;
	}
}
