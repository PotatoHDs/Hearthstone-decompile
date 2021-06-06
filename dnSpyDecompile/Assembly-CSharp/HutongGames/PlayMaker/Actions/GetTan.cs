using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2E RID: 3630
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Tangent. You can use degrees, simply check on the DegToRad conversion")]
	public class GetTan : FsmStateAction
	{
		// Token: 0x0600A7A0 RID: 42912 RVA: 0x0034D2AB File Offset: 0x0034B4AB
		public override void Reset()
		{
			this.angle = null;
			this.DegToRad = true;
			this.everyFrame = false;
			this.result = null;
		}

		// Token: 0x0600A7A1 RID: 42913 RVA: 0x0034D2CE File Offset: 0x0034B4CE
		public override void OnEnter()
		{
			this.DoTan();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A7A2 RID: 42914 RVA: 0x0034D2E4 File Offset: 0x0034B4E4
		public override void OnUpdate()
		{
			this.DoTan();
		}

		// Token: 0x0600A7A3 RID: 42915 RVA: 0x0034D2EC File Offset: 0x0034B4EC
		private void DoTan()
		{
			float num = this.angle.Value;
			if (this.DegToRad.Value)
			{
				num *= 0.017453292f;
			}
			this.result.Value = Mathf.Tan(num);
		}

		// Token: 0x04008E2E RID: 36398
		[RequiredField]
		[Tooltip("The angle. Note: You can use degrees, simply check DegtoRad if the angle is expressed in degrees.")]
		public FsmFloat angle;

		// Token: 0x04008E2F RID: 36399
		[Tooltip("Check on if the angle is expressed in degrees.")]
		public FsmBool DegToRad;

		// Token: 0x04008E30 RID: 36400
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle tan")]
		public FsmFloat result;

		// Token: 0x04008E31 RID: 36401
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
