using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D67 RID: 3431
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the upper right corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformSetAnchorMinAndMax : BaseUpdateAction
	{
		// Token: 0x0600A40A RID: 41994 RVA: 0x00341930 File Offset: 0x0033FB30
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMax = null;
			this.anchorMin = null;
			this.xMax = new FsmFloat
			{
				UseVariable = true
			};
			this.yMax = new FsmFloat
			{
				UseVariable = true
			};
			this.xMin = new FsmFloat
			{
				UseVariable = true
			};
			this.yMin = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A40B RID: 41995 RVA: 0x003419A0 File Offset: 0x0033FBA0
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

		// Token: 0x0600A40C RID: 41996 RVA: 0x003419E8 File Offset: 0x0033FBE8
		public override void OnActionUpdate()
		{
			this.DoSetAnchorMax();
		}

		// Token: 0x0600A40D RID: 41997 RVA: 0x003419F0 File Offset: 0x0033FBF0
		private void DoSetAnchorMax()
		{
			Vector2 value = this._rt.anchorMax;
			Vector2 value2 = this._rt.anchorMin;
			if (!this.anchorMax.IsNone)
			{
				value = this.anchorMax.Value;
				value2 = this.anchorMin.Value;
			}
			if (!this.xMax.IsNone)
			{
				value.x = this.xMax.Value;
			}
			if (!this.yMax.IsNone)
			{
				value.y = this.yMax.Value;
			}
			if (!this.xMin.IsNone)
			{
				value2.x = this.xMin.Value;
			}
			if (!this.yMin.IsNone)
			{
				value2.y = this.yMin.Value;
			}
			this._rt.anchorMax = value;
			this._rt.anchorMin = value2;
		}

		// Token: 0x04008A76 RID: 35446
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A77 RID: 35447
		[Tooltip("The Vector2 anchor max. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMax;

		// Token: 0x04008A78 RID: 35448
		[Tooltip("The Vector2 anchor min. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMin;

		// Token: 0x04008A79 RID: 35449
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat xMax;

		// Token: 0x04008A7A RID: 35450
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMax x value if set. Set to none for no effect")]
		public FsmFloat yMax;

		// Token: 0x04008A7B RID: 35451
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat xMin;

		// Token: 0x04008A7C RID: 35452
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat yMin;

		// Token: 0x04008A7D RID: 35453
		private RectTransform _rt;
	}
}
