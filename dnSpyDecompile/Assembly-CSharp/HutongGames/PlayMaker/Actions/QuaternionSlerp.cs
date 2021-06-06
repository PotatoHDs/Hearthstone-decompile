using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D49 RID: 3401
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Spherically interpolates between from and to by t.")]
	public class QuaternionSlerp : QuaternionBaseAction
	{
		// Token: 0x0600A377 RID: 41847 RVA: 0x0033F2E0 File Offset: 0x0033D4E0
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
			this.amount = 0.1f;
			this.storeResult = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A378 RID: 41848 RVA: 0x0033F336 File Offset: 0x0033D536
		public override void OnEnter()
		{
			this.DoQuatSlerp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A379 RID: 41849 RVA: 0x0033F34C File Offset: 0x0033D54C
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatSlerp();
			}
		}

		// Token: 0x0600A37A RID: 41850 RVA: 0x0033F35C File Offset: 0x0033D55C
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatSlerp();
			}
		}

		// Token: 0x0600A37B RID: 41851 RVA: 0x0033F36D File Offset: 0x0033D56D
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatSlerp();
			}
		}

		// Token: 0x0600A37C RID: 41852 RVA: 0x0033F37E File Offset: 0x0033D57E
		private void DoQuatSlerp()
		{
			this.storeResult.Value = Quaternion.Slerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
		}

		// Token: 0x040089A8 RID: 35240
		[RequiredField]
		[Tooltip("From Quaternion.")]
		public FsmQuaternion fromQuaternion;

		// Token: 0x040089A9 RID: 35241
		[RequiredField]
		[Tooltip("To Quaternion.")]
		public FsmQuaternion toQuaternion;

		// Token: 0x040089AA RID: 35242
		[RequiredField]
		[Tooltip("Interpolate between fromQuaternion and toQuaternion by this amount. Value is clamped to 0-1 range. 0 = fromQuaternion; 1 = toQuaternion; 0.5 = half way between.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat amount;

		// Token: 0x040089AB RID: 35243
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this quaternion variable.")]
		public FsmQuaternion storeResult;
	}
}
