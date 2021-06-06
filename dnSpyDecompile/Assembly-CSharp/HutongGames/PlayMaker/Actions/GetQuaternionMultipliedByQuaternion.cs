using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3E RID: 3390
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Get the quaternion from a quaternion multiplied by a quaternion.")]
	public class GetQuaternionMultipliedByQuaternion : QuaternionBaseAction
	{
		// Token: 0x0600A32F RID: 41775 RVA: 0x0033EA3C File Offset: 0x0033CC3C
		public override void Reset()
		{
			this.quaternionA = null;
			this.quaternionB = null;
			this.result = null;
			this.everyFrame = false;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A330 RID: 41776 RVA: 0x0033EA61 File Offset: 0x0033CC61
		public override void OnEnter()
		{
			this.DoQuatMult();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A331 RID: 41777 RVA: 0x0033EA77 File Offset: 0x0033CC77
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A332 RID: 41778 RVA: 0x0033EA87 File Offset: 0x0033CC87
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A333 RID: 41779 RVA: 0x0033EA98 File Offset: 0x0033CC98
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A334 RID: 41780 RVA: 0x0033EAA9 File Offset: 0x0033CCA9
		private void DoQuatMult()
		{
			this.result.Value = this.quaternionA.Value * this.quaternionB.Value;
		}

		// Token: 0x04008986 RID: 35206
		[RequiredField]
		[Tooltip("The first quaternion to multiply")]
		public FsmQuaternion quaternionA;

		// Token: 0x04008987 RID: 35207
		[RequiredField]
		[Tooltip("The second quaternion to multiply")]
		public FsmQuaternion quaternionB;

		// Token: 0x04008988 RID: 35208
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting quaternion")]
		public FsmQuaternion result;
	}
}
