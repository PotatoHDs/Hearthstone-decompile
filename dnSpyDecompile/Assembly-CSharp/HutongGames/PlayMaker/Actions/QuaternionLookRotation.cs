using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D46 RID: 3398
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation that looks along forward with the head upwards along upwards.")]
	public class QuaternionLookRotation : QuaternionBaseAction
	{
		// Token: 0x0600A362 RID: 41826 RVA: 0x0033EF15 File Offset: 0x0033D115
		public override void Reset()
		{
			this.direction = null;
			this.upVector = new FsmVector3
			{
				UseVariable = true
			};
			this.result = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A363 RID: 41827 RVA: 0x0033EF45 File Offset: 0x0033D145
		public override void OnEnter()
		{
			this.DoQuatLookRotation();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A364 RID: 41828 RVA: 0x0033EF5B File Offset: 0x0033D15B
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatLookRotation();
			}
		}

		// Token: 0x0600A365 RID: 41829 RVA: 0x0033EF6B File Offset: 0x0033D16B
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatLookRotation();
			}
		}

		// Token: 0x0600A366 RID: 41830 RVA: 0x0033EF7C File Offset: 0x0033D17C
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatLookRotation();
			}
		}

		// Token: 0x0600A367 RID: 41831 RVA: 0x0033EF90 File Offset: 0x0033D190
		private void DoQuatLookRotation()
		{
			if (!this.upVector.IsNone)
			{
				this.result.Value = Quaternion.LookRotation(this.direction.Value, this.upVector.Value);
				return;
			}
			this.result.Value = Quaternion.LookRotation(this.direction.Value);
		}

		// Token: 0x0400899E RID: 35230
		[RequiredField]
		[Tooltip("the rotation direction")]
		public FsmVector3 direction;

		// Token: 0x0400899F RID: 35231
		[Tooltip("The up direction")]
		public FsmVector3 upVector;

		// Token: 0x040089A0 RID: 35232
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;
	}
}
