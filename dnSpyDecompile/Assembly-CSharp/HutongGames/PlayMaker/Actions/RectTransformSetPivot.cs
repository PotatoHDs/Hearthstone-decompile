using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D6D RID: 3437
	[ActionCategory("RectTransform")]
	[Tooltip("The normalized position in this RectTransform that it rotates around.")]
	public class RectTransformSetPivot : BaseUpdateAction
	{
		// Token: 0x0600A428 RID: 42024 RVA: 0x003423F4 File Offset: 0x003405F4
		public override void Reset()
		{
			base.Reset();
			this.gameObject = null;
			this.pivot = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A429 RID: 42025 RVA: 0x00342430 File Offset: 0x00340630
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._rt = ownerDefaultTarget.GetComponent<RectTransform>();
			}
			this.DoSetPivotPosition();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A42A RID: 42026 RVA: 0x00342478 File Offset: 0x00340678
		public override void OnActionUpdate()
		{
			this.DoSetPivotPosition();
		}

		// Token: 0x0600A42B RID: 42027 RVA: 0x00342480 File Offset: 0x00340680
		private void DoSetPivotPosition()
		{
			Vector2 value = this._rt.pivot;
			if (!this.pivot.IsNone)
			{
				value = this.pivot.Value;
			}
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			this._rt.pivot = value;
		}

		// Token: 0x04008A9D RID: 35485
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A9E RID: 35486
		[Tooltip("The Vector2 pivot. Set to none for no effect, and/or set individual axis below.")]
		public FsmVector2 pivot;

		// Token: 0x04008A9F RID: 35487
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides pivot x value if set. Set to none for no effect")]
		public FsmFloat x;

		// Token: 0x04008AA0 RID: 35488
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Setting only the x value. Overrides pivot y value if set. Set to none for no effect")]
		public FsmFloat y;

		// Token: 0x04008AA1 RID: 35489
		private RectTransform _rt;
	}
}
