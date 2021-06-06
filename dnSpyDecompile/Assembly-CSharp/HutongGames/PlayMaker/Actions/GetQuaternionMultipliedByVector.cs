using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3F RID: 3391
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Get the vector3 from a quaternion multiplied by a vector.")]
	public class GetQuaternionMultipliedByVector : QuaternionBaseAction
	{
		// Token: 0x0600A336 RID: 41782 RVA: 0x0033EAD1 File Offset: 0x0033CCD1
		public override void Reset()
		{
			this.quaternion = null;
			this.vector3 = null;
			this.result = null;
			this.everyFrame = false;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A337 RID: 41783 RVA: 0x0033EAF6 File Offset: 0x0033CCF6
		public override void OnEnter()
		{
			this.DoQuatMult();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A338 RID: 41784 RVA: 0x0033EB0C File Offset: 0x0033CD0C
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A339 RID: 41785 RVA: 0x0033EB1C File Offset: 0x0033CD1C
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A33A RID: 41786 RVA: 0x0033EB2D File Offset: 0x0033CD2D
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatMult();
			}
		}

		// Token: 0x0600A33B RID: 41787 RVA: 0x0033EB3E File Offset: 0x0033CD3E
		private void DoQuatMult()
		{
			this.result.Value = this.quaternion.Value * this.vector3.Value;
		}

		// Token: 0x04008989 RID: 35209
		[RequiredField]
		[Tooltip("The quaternion to multiply")]
		public FsmQuaternion quaternion;

		// Token: 0x0400898A RID: 35210
		[RequiredField]
		[Tooltip("The vector3 to multiply")]
		public FsmVector3 vector3;

		// Token: 0x0400898B RID: 35211
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting vector3")]
		public FsmVector3 result;
	}
}
