using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E28 RID: 3624
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan : FsmStateAction
	{
		// Token: 0x0600A782 RID: 42882 RVA: 0x0034CEEF File Offset: 0x0034B0EF
		public override void Reset()
		{
			this.Value = null;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.angle = null;
		}

		// Token: 0x0600A783 RID: 42883 RVA: 0x0034CF12 File Offset: 0x0034B112
		public override void OnEnter()
		{
			this.DoATan();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A784 RID: 42884 RVA: 0x0034CF28 File Offset: 0x0034B128
		public override void OnUpdate()
		{
			this.DoATan();
		}

		// Token: 0x0600A785 RID: 42885 RVA: 0x0034CF30 File Offset: 0x0034B130
		private void DoATan()
		{
			float num = Mathf.Atan(this.Value.Value);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E13 RID: 36371
		[RequiredField]
		[Tooltip("The value of the tan")]
		public FsmFloat Value;

		// Token: 0x04008E14 RID: 36372
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E15 RID: 36373
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E16 RID: 36374
		public bool everyFrame;
	}
}
