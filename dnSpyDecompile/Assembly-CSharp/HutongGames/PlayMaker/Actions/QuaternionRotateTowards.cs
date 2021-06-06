using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D48 RID: 3400
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Rotates a rotation from towards to. This is essentially the same as Quaternion.Slerp but instead the function will ensure that the angular speed never exceeds maxDegreesDelta. Negative values of maxDegreesDelta pushes the rotation away from to.")]
	public class QuaternionRotateTowards : QuaternionBaseAction
	{
		// Token: 0x0600A370 RID: 41840 RVA: 0x0033F20C File Offset: 0x0033D40C
		public override void Reset()
		{
			this.fromQuaternion = new FsmQuaternion
			{
				UseVariable = true
			};
			this.toQuaternion = new FsmQuaternion
			{
				UseVariable = true
			};
			this.maxDegreesDelta = 10f;
			this.storeResult = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A371 RID: 41841 RVA: 0x0033F262 File Offset: 0x0033D462
		public override void OnEnter()
		{
			this.DoQuatRotateTowards();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A372 RID: 41842 RVA: 0x0033F278 File Offset: 0x0033D478
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatRotateTowards();
			}
		}

		// Token: 0x0600A373 RID: 41843 RVA: 0x0033F288 File Offset: 0x0033D488
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatRotateTowards();
			}
		}

		// Token: 0x0600A374 RID: 41844 RVA: 0x0033F299 File Offset: 0x0033D499
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatRotateTowards();
			}
		}

		// Token: 0x0600A375 RID: 41845 RVA: 0x0033F2AA File Offset: 0x0033D4AA
		private void DoQuatRotateTowards()
		{
			this.storeResult.Value = Quaternion.RotateTowards(this.fromQuaternion.Value, this.toQuaternion.Value, this.maxDegreesDelta.Value);
		}

		// Token: 0x040089A4 RID: 35236
		[RequiredField]
		[Tooltip("From Quaternion.")]
		public FsmQuaternion fromQuaternion;

		// Token: 0x040089A5 RID: 35237
		[RequiredField]
		[Tooltip("To Quaternion.")]
		public FsmQuaternion toQuaternion;

		// Token: 0x040089A6 RID: 35238
		[RequiredField]
		[Tooltip("The angular speed never exceeds maxDegreesDelta. Negative values of maxDegreesDelta pushes the rotation away from to.")]
		public FsmFloat maxDegreesDelta;

		// Token: 0x040089A7 RID: 35239
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this quaternion variable.")]
		public FsmQuaternion storeResult;
	}
}
