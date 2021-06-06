using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5F RID: 3423
	[ActionCategory("RectTransform")]
	[Tooltip("Get the size of this RectTransform relative to the distances between the anchors. this is the 'Width' and 'Height' values in the RectTransform inspector.")]
	public class RectTransformGetSizeDelta : BaseUpdateAction
	{
		// Token: 0x0600A3E2 RID: 41954 RVA: 0x00340F10 File Offset: 0x0033F110
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.sizeDelta = null;
			this.width = null;
			this.height = null;
		}

		// Token: 0x0600A3E3 RID: 41955 RVA: 0x00340F34 File Offset: 0x0033F134
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

		// Token: 0x0600A3E4 RID: 41956 RVA: 0x00340F7C File Offset: 0x0033F17C
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3E5 RID: 41957 RVA: 0x00340F84 File Offset: 0x0033F184
		private void DoGetValues()
		{
			if (!this.sizeDelta.IsNone)
			{
				this.sizeDelta.Value = this._rt.sizeDelta;
			}
			if (!this.width.IsNone)
			{
				this.width.Value = this._rt.sizeDelta.x;
			}
			if (!this.height.IsNone)
			{
				this.height.Value = this._rt.sizeDelta.y;
			}
		}

		// Token: 0x04008A3E RID: 35390
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A3F RID: 35391
		[Tooltip("The sizeDelta")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 sizeDelta;

		// Token: 0x04008A40 RID: 35392
		[Tooltip("The x component of the sizeDelta, the width.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat width;

		// Token: 0x04008A41 RID: 35393
		[Tooltip("The y component of the sizeDelta, the height")]
		[UIHint(UIHint.Variable)]
		public FsmFloat height;

		// Token: 0x04008A42 RID: 35394
		private RectTransform _rt;
	}
}
