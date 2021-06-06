using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D54 RID: 3412
	[ActionCategory("RectTransform")]
	[Tooltip("Flips the horizontal and vertical axes of the RectTransform size and alignment, and optionally its children as well.")]
	public class RectTransformFlipLayoutAxis : FsmStateAction
	{
		// Token: 0x0600A3AC RID: 41900 RVA: 0x00340253 File Offset: 0x0033E453
		public override void Reset()
		{
			this.gameObject = null;
			this.axis = RectTransformFlipLayoutAxis.RectTransformFlipOptions.Both;
			this.keepPositioning = null;
			this.recursive = null;
		}

		// Token: 0x0600A3AD RID: 41901 RVA: 0x00340271 File Offset: 0x0033E471
		public override void OnEnter()
		{
			this.DoFlip();
			base.Finish();
		}

		// Token: 0x0600A3AE RID: 41902 RVA: 0x00340280 File Offset: 0x0033E480
		private void DoFlip()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				RectTransform component = ownerDefaultTarget.GetComponent<RectTransform>();
				if (component != null)
				{
					if (this.axis == RectTransformFlipLayoutAxis.RectTransformFlipOptions.Both)
					{
						RectTransformUtility.FlipLayoutAxes(component, this.keepPositioning.Value, this.recursive.Value);
						return;
					}
					if (this.axis == RectTransformFlipLayoutAxis.RectTransformFlipOptions.Horizontal)
					{
						RectTransformUtility.FlipLayoutOnAxis(component, 0, this.keepPositioning.Value, this.recursive.Value);
						return;
					}
					if (this.axis == RectTransformFlipLayoutAxis.RectTransformFlipOptions.Vertical)
					{
						RectTransformUtility.FlipLayoutOnAxis(component, 1, this.keepPositioning.Value, this.recursive.Value);
					}
				}
			}
		}

		// Token: 0x040089FF RID: 35327
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008A00 RID: 35328
		[Tooltip("The axis to flip")]
		public RectTransformFlipLayoutAxis.RectTransformFlipOptions axis;

		// Token: 0x04008A01 RID: 35329
		[Tooltip("Flips around the pivot if true. Flips within the parent rect if false.")]
		public FsmBool keepPositioning;

		// Token: 0x04008A02 RID: 35330
		[Tooltip("Flip the children as well?")]
		public FsmBool recursive;

		// Token: 0x020027A1 RID: 10145
		public enum RectTransformFlipOptions
		{
			// Token: 0x0400F4F1 RID: 62705
			Horizontal,
			// Token: 0x0400F4F2 RID: 62706
			Vertical,
			// Token: 0x0400F4F3 RID: 62707
			Both
		}
	}
}
