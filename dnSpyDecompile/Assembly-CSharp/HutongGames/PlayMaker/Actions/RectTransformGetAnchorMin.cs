using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D57 RID: 3415
	[ActionCategory("RectTransform")]
	[Tooltip("Get the normalized position in the parent RectTransform that the lower left corner is anchored to.")]
	public class RectTransformGetAnchorMin : BaseUpdateAction
	{
		// Token: 0x0600A3BA RID: 41914 RVA: 0x00340520 File Offset: 0x0033E720
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMin = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3BB RID: 41915 RVA: 0x00340544 File Offset: 0x0033E744
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

		// Token: 0x0600A3BC RID: 41916 RVA: 0x0034058C File Offset: 0x0033E78C
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3BD RID: 41917 RVA: 0x00340594 File Offset: 0x0033E794
		private void DoGetValues()
		{
			if (!this.anchorMin.IsNone)
			{
				this.anchorMin.Value = this._rt.anchorMin;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.anchorMin.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.anchorMin.y;
			}
		}

		// Token: 0x04008A0D RID: 35341
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A0E RID: 35342
		[Tooltip("The anchorMin")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 anchorMin;

		// Token: 0x04008A0F RID: 35343
		[Tooltip("The x component of the anchorMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A10 RID: 35344
		[Tooltip("The y component of the anchorMin")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A11 RID: 35345
		private RectTransform _rt;
	}
}
