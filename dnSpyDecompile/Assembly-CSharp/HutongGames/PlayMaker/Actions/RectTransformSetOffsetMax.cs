using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6B RID: 3435
	[ActionCategory("RectTransform")]
	[Tooltip("\tThe offset of the upper right corner of the rectangle relative to the upper right anchor.")]
	public class RectTransformSetOffsetMax : BaseUpdateAction
	{
		// Token: 0x0600A41E RID: 42014 RVA: 0x003421E6 File Offset: 0x003403E6
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.offsetMax = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A41F RID: 42015 RVA: 0x00342220 File Offset: 0x00340420
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetOffsetMax();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A420 RID: 42016 RVA: 0x00342268 File Offset: 0x00340468
		public override void OnActionUpdate()
		{
			this.DoSetOffsetMax();
		}

		// Token: 0x0600A421 RID: 42017 RVA: 0x00342270 File Offset: 0x00340470
		private void DoSetOffsetMax()
		{
			Vector2 value = this._rt.offsetMax;
			if (!this.offsetMax.IsNone)
			{
				value = this.offsetMax.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.offsetMax = value;
		}

		// Token: 0x04008A93 RID: 35475
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A94 RID: 35476
		[Tooltip("The Vector2 offsetMax. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 offsetMax;

		// Token: 0x04008A95 RID: 35477
		[Tooltip("Setting only the x value. Overrides offsetMax x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A96 RID: 35478
		[Tooltip("Setting only the y value. Overrides offsetMax y value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A97 RID: 35479
		private RectTransform _rt;
	}
}
