using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E2B RID: 3627
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent 2 as in atan2(y,x) from a vector 3, where you pick which is x and y from the vector 3. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan2FromVector3 : FsmStateAction
	{
		// Token: 0x0600A791 RID: 42897 RVA: 0x0034D098 File Offset: 0x0034B298
		public override void Reset()
		{
			this.vector3 = null;
			this.xAxis = GetAtan2FromVector3.aTan2EnumAxis.x;
			this.yAxis = GetAtan2FromVector3.aTan2EnumAxis.y;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.angle = null;
		}

		// Token: 0x0600A792 RID: 42898 RVA: 0x0034D0C9 File Offset: 0x0034B2C9
		public override void OnEnter()
		{
			this.DoATan();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A793 RID: 42899 RVA: 0x0034D0DF File Offset: 0x0034B2DF
		public override void OnUpdate()
		{
			this.DoATan();
		}

		// Token: 0x0600A794 RID: 42900 RVA: 0x0034D0E8 File Offset: 0x0034B2E8
		private void DoATan()
		{
			float x = this.vector3.Value.x;
			if (this.xAxis == GetAtan2FromVector3.aTan2EnumAxis.y)
			{
				x = this.vector3.Value.y;
			}
			else if (this.xAxis == GetAtan2FromVector3.aTan2EnumAxis.z)
			{
				x = this.vector3.Value.z;
			}
			float y = this.vector3.Value.y;
			if (this.yAxis == GetAtan2FromVector3.aTan2EnumAxis.x)
			{
				y = this.vector3.Value.x;
			}
			else if (this.yAxis == GetAtan2FromVector3.aTan2EnumAxis.z)
			{
				y = this.vector3.Value.z;
			}
			float num = Mathf.Atan2(y, x);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E20 RID: 36384
		[RequiredField]
		[Tooltip("The vector3 definition of the tan")]
		public FsmVector3 vector3;

		// Token: 0x04008E21 RID: 36385
		[RequiredField]
		[Tooltip("which axis in the vector3 to use as the x value of the tan")]
		public GetAtan2FromVector3.aTan2EnumAxis xAxis;

		// Token: 0x04008E22 RID: 36386
		[RequiredField]
		[Tooltip("which axis in the vector3 to use as the y value of the tan")]
		public GetAtan2FromVector3.aTan2EnumAxis yAxis;

		// Token: 0x04008E23 RID: 36387
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E24 RID: 36388
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E25 RID: 36389
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x020027AF RID: 10159
		public enum aTan2EnumAxis
		{
			// Token: 0x0400F536 RID: 62774
			x,
			// Token: 0x0400F537 RID: 62775
			y,
			// Token: 0x0400F538 RID: 62776
			z
		}
	}
}
