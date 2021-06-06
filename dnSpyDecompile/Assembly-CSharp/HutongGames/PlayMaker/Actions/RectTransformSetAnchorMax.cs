using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D65 RID: 3429
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformSetAnchorMax : BaseUpdateAction
	{
		// Token: 0x0600A400 RID: 41984 RVA: 0x00341720 File Offset: 0x0033F920
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMax = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A401 RID: 41985 RVA: 0x0034175C File Offset: 0x0033F95C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetAnchorMax();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A402 RID: 41986 RVA: 0x003417A4 File Offset: 0x0033F9A4
		public override void OnActionUpdate()
		{
			this.DoSetAnchorMax();
		}

		// Token: 0x0600A403 RID: 41987 RVA: 0x003417AC File Offset: 0x0033F9AC
		private void DoSetAnchorMax()
		{
			Vector2 value = this._rt.anchorMax;
			if (!this.anchorMax.IsNone)
			{
				value = this.anchorMax.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.anchorMax = value;
		}

		// Token: 0x04008A6C RID: 35436
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A6D RID: 35437
		[Tooltip("The Vector2 anchor. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMax;

		// Token: 0x04008A6E RID: 35438
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A6F RID: 35439
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A70 RID: 35440
		private RectTransform _rt;
	}
}
