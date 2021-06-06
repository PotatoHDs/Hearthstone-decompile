using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D5E RID: 3422
	[ActionCategory("RectTransform")]
	[Tooltip("The calculated rectangle in the local space of the Transform.")]
	public class RectTransformGetRect : BaseUpdateAction
	{
		// Token: 0x0600A3DD RID: 41949 RVA: 0x00340D78 File Offset: 0x0033EF78
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.rect = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.width = new FsmFloat
			{
				UseVariable = true
			};
			this.height = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A3DE RID: 41950 RVA: 0x00340DE4 File Offset: 0x0033EFE4
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

		// Token: 0x0600A3DF RID: 41951 RVA: 0x00340E2C File Offset: 0x0033F02C
		public override void OnActionUpdate()
		{
			this.DoGetValues();
		}

		// Token: 0x0600A3E0 RID: 41952 RVA: 0x00340E34 File Offset: 0x0033F034
		private void DoGetValues()
		{
			if (!this.rect.IsNone)
			{
				this.rect.Value = this._rt.rect;
			}
			if (!this.x.IsNone)
			{
				this.x.Value = this._rt.rect.x;
			}
			if (!this.y.IsNone)
			{
				this.y.Value = this._rt.rect.y;
			}
			if (!this.width.IsNone)
			{
				this.width.Value = this._rt.rect.width;
			}
			if (!this.height.IsNone)
			{
				this.height.Value = this._rt.rect.height;
			}
		}

		// Token: 0x04008A37 RID: 35383
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A38 RID: 35384
		[UIHint(UIHint.Variable)]
		[Tooltip("The rect")]
		public FsmRect rect;

		// Token: 0x04008A39 RID: 35385
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x04008A3A RID: 35386
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x04008A3B RID: 35387
		[UIHint(UIHint.Variable)]
		public FsmFloat width;

		// Token: 0x04008A3C RID: 35388
		[UIHint(UIHint.Variable)]
		public FsmFloat height;

		// Token: 0x04008A3D RID: 35389
		private RectTransform _rt;
	}
}
