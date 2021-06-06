using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2D RID: 3629
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the sine. You can use degrees, simply check on the DegToRad conversion")]
	public class GetSine : FsmStateAction
	{
		// Token: 0x0600A79B RID: 42907 RVA: 0x0034D22B File Offset: 0x0034B42B
		public override void Reset()
		{
			this.angle = null;
			this.DegToRad = true;
			this.everyFrame = false;
			this.result = null;
		}

		// Token: 0x0600A79C RID: 42908 RVA: 0x0034D24E File Offset: 0x0034B44E
		public override void OnEnter()
		{
			this.DoSine();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A79D RID: 42909 RVA: 0x0034D264 File Offset: 0x0034B464
		public override void OnUpdate()
		{
			this.DoSine();
		}

		// Token: 0x0600A79E RID: 42910 RVA: 0x0034D26C File Offset: 0x0034B46C
		private void DoSine()
		{
			float num = this.angle.Value;
			if (this.DegToRad.Value)
			{
				num *= 0.017453292f;
			}
			this.result.Value = Mathf.Sin(num);
		}

		// Token: 0x04008E2A RID: 36394
		[RequiredField]
		[Tooltip("The angle. Note: You can use degrees, simply check DegtoRad if the angle is expressed in degrees.")]
		public FsmFloat angle;

		// Token: 0x04008E2B RID: 36395
		[Tooltip("Check on if the angle is expressed in degrees.")]
		public FsmBool DegToRad;

		// Token: 0x04008E2C RID: 36396
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle tan")]
		public FsmFloat result;

		// Token: 0x04008E2D RID: 36397
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
