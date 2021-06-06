using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6E RID: 3438
	[ActionCategory("RectTransform")]
	[Tooltip("Set the size of this RectTransform relative to the distances between the anchors. this is the 'Width' and 'Height' values in the RectTransform inspector.")]
	public class RectTransformSetSizeDelta : BaseUpdateAction
	{
		// Token: 0x0600A42D RID: 42029 RVA: 0x003424FC File Offset: 0x003406FC
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.sizeDelta = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A42E RID: 42030 RVA: 0x00342538 File Offset: 0x00340738
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetSizeDelta();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A42F RID: 42031 RVA: 0x00342580 File Offset: 0x00340780
		public override void OnActionUpdate()
		{
			this.DoSetSizeDelta();
		}

		// Token: 0x0600A430 RID: 42032 RVA: 0x00342588 File Offset: 0x00340788
		private void DoSetSizeDelta()
		{
			Vector2 value = this._rt.sizeDelta;
			if (!this.sizeDelta.IsNone)
			{
				value = this.sizeDelta.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.sizeDelta = value;
		}

		// Token: 0x04008AA2 RID: 35490
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008AA3 RID: 35491
		[Tooltip("TheVector2 sizeDelta. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 sizeDelta;

		// Token: 0x04008AA4 RID: 35492
		[Tooltip("Setting only the x value. Overrides sizeDelta x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008AA5 RID: 35493
		[Tooltip("Setting only the x value. Overrides sizeDelta y value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008AA6 RID: 35494
		private RectTransform _rt;
	}
}
