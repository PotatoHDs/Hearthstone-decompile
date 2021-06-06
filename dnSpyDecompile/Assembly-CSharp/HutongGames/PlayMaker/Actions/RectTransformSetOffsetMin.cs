using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6C RID: 3436
	[ActionCategory("RectTransform")]
	[Tooltip("The offset of the lower left corner of the rectangle relative to the lower left anchor.")]
	public class RectTransformSetOffsetMin : BaseUpdateAction
	{
		// Token: 0x0600A423 RID: 42019 RVA: 0x003422EC File Offset: 0x003404EC
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.offsetMin = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A424 RID: 42020 RVA: 0x00342328 File Offset: 0x00340528
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetOffsetMin();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A425 RID: 42021 RVA: 0x00342370 File Offset: 0x00340570
		public override void OnActionUpdate()
		{
			this.DoSetOffsetMin();
		}

		// Token: 0x0600A426 RID: 42022 RVA: 0x00342378 File Offset: 0x00340578
		private void DoSetOffsetMin()
		{
			Vector2 value = this._rt.offsetMin;
			if (!this.offsetMin.IsNone)
			{
				value = this.offsetMin.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.offsetMin = value;
		}

		// Token: 0x04008A98 RID: 35480
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A99 RID: 35481
		[Tooltip("The Vector2 offsetMin. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 offsetMin;

		// Token: 0x04008A9A RID: 35482
		[Tooltip("Setting only the x value. Overrides offsetMin x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A9B RID: 35483
		[Tooltip("Setting only the x value. Overrides offsetMin y value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A9C RID: 35484
		private RectTransform _rt;
	}
}
