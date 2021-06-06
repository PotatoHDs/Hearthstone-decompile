using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2A RID: 3626
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent 2 as in atan2(y,x) from a vector 2. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan2FromVector2 : FsmStateAction
	{
		// Token: 0x0600A78C RID: 42892 RVA: 0x0034D002 File Offset: 0x0034B202
		public override void Reset()
		{
			this.vector2 = null;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.angle = null;
		}

		// Token: 0x0600A78D RID: 42893 RVA: 0x0034D025 File Offset: 0x0034B225
		public override void OnEnter()
		{
			this.DoATan();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A78E RID: 42894 RVA: 0x0034D03B File Offset: 0x0034B23B
		public override void OnUpdate()
		{
			this.DoATan();
		}

		// Token: 0x0600A78F RID: 42895 RVA: 0x0034D044 File Offset: 0x0034B244
		private void DoATan()
		{
			float num = Mathf.Atan2(this.vector2.Value.y, this.vector2.Value.x);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E1C RID: 36380
		[RequiredField]
		[Tooltip("The vector2 of the tan")]
		public FsmVector2 vector2;

		// Token: 0x04008E1D RID: 36381
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E1E RID: 36382
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E1F RID: 36383
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
