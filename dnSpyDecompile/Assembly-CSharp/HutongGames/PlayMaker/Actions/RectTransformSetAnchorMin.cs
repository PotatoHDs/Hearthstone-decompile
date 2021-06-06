using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D66 RID: 3430
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in the parent RectTransform that the lower left corner is anchored to. This is relative screen space, values ranges from 0 to 1")]
	public class RectTransformSetAnchorMin : BaseUpdateAction
	{
		// Token: 0x0600A405 RID: 41989 RVA: 0x00341828 File Offset: 0x0033FA28
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.anchorMin = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A406 RID: 41990 RVA: 0x00341864 File Offset: 0x0033FA64
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetAnchorMin();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A407 RID: 41991 RVA: 0x003418AC File Offset: 0x0033FAAC
		public override void OnActionUpdate()
		{
			this.DoSetAnchorMin();
		}

		// Token: 0x0600A408 RID: 41992 RVA: 0x003418B4 File Offset: 0x0033FAB4
		private void DoSetAnchorMin()
		{
			Vector2 value = this._rt.anchorMin;
			if (!this.anchorMin.IsNone)
			{
				value = this.anchorMin.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.anchorMin = value;
		}

		// Token: 0x04008A71 RID: 35441
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A72 RID: 35442
		[Tooltip("The Vector2 anchor. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 anchorMin;

		// Token: 0x04008A73 RID: 35443
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008A74 RID: 35444
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides anchorMin x value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008A75 RID: 35445
		private RectTransform _rt;
	}
}
