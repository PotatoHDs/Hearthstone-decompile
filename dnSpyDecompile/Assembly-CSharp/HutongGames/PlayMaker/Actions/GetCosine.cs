using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2C RID: 3628
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the cosine. You can use degrees, simply check on the DegToRad conversion")]
	public class GetCosine : FsmStateAction
	{
		// Token: 0x0600A796 RID: 42902 RVA: 0x0034D1AB File Offset: 0x0034B3AB
		public override void Reset()
		{
			this.angle = null;
			this.DegToRad = true;
			this.everyFrame = false;
			this.result = null;
		}

		// Token: 0x0600A797 RID: 42903 RVA: 0x0034D1CE File Offset: 0x0034B3CE
		public override void OnEnter()
		{
			this.DoCosine();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A798 RID: 42904 RVA: 0x0034D1E4 File Offset: 0x0034B3E4
		public override void OnUpdate()
		{
			this.DoCosine();
		}

		// Token: 0x0600A799 RID: 42905 RVA: 0x0034D1EC File Offset: 0x0034B3EC
		private void DoCosine()
		{
			float num = this.angle.Value;
			if (this.DegToRad.Value)
			{
				num *= 0.017453292f;
			}
			this.result.Value = Mathf.Cos(num);
		}

		// Token: 0x04008E26 RID: 36390
		[RequiredField]
		[Tooltip("The angle. Note: You can use degrees, simply check DegtoRad if the angle is expressed in degrees.")]
		public FsmFloat angle;

		// Token: 0x04008E27 RID: 36391
		[Tooltip("Check on if the angle is expressed in degrees.")]
		public FsmBool DegToRad;

		// Token: 0x04008E28 RID: 36392
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle cosine")]
		public FsmFloat result;

		// Token: 0x04008E29 RID: 36393
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
