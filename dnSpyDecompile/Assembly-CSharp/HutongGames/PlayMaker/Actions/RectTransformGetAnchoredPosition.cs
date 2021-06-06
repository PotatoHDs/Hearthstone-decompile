using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D55 RID: 3413
	[ActionCategory("RectTransform")]
	[Tooltip("Get the position of the pivot of this RectTransform relative to the anchor reference point.")]
	public class RectTransformGetAnchoredPosition : BaseUpdateAction
	{
		// Token: 0x0600A3B0 RID: 41904 RVA: 0x0034032D File Offset: 0x0033E52D
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.position = null;
			this.x = null;
			this.y = null;
		}

		// Token: 0x0600A3B1 RID: 41905 RVA: 0x00340354 File Offset: 0x0033E554
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

		// Token: 0x0600A3B2 RID: 41906 RVA: 0x0034039C File Offset: 0x0033E59C
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3B3 RID: 41907 RVA: 0x003403A4 File Offset: 0x0033E5A4
		private void DoGetValues()
		{
			if (!this.position.IsNone)
			{
				this.position.Value = this._rt.anchoredPosition;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.anchoredPosition.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.anchoredPosition.y;
			}
		}

		// Token: 0x04008A03 RID: 35331
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A04 RID: 35332
		[Tooltip("The anchored Position")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 position;

		// Token: 0x04008A05 RID: 35333
		[Tooltip("The x component of the anchored Position")]
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A06 RID: 35334
		[Tooltip("The y component of the anchored Position")]
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A07 RID: 35335
		private RectTransform _rt;
	}
}
