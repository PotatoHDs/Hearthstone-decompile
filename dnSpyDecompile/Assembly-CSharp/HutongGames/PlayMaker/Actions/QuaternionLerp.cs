using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D45 RID: 3397
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Interpolates between from and to by t and normalizes the result afterwards.")]
	public class QuaternionLerp : QuaternionBaseAction
	{
		// Token: 0x0600A35B RID: 41819 RVA: 0x0033EE44 File Offset: 0x0033D044
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
			this.amount = 0.5f;
			this.storeResult = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A35C RID: 41820 RVA: 0x0033EE9A File Offset: 0x0033D09A
		public override void OnEnter()
		{
			this.DoQuatLerp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A35D RID: 41821 RVA: 0x0033EEB0 File Offset: 0x0033D0B0
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatLerp();
			}
		}

		// Token: 0x0600A35E RID: 41822 RVA: 0x0033EEC0 File Offset: 0x0033D0C0
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatLerp();
			}
		}

		// Token: 0x0600A35F RID: 41823 RVA: 0x0033EED1 File Offset: 0x0033D0D1
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatLerp();
			}
		}

		// Token: 0x0600A360 RID: 41824 RVA: 0x0033EEE2 File Offset: 0x0033D0E2
		private void DoQuatLerp()
		{
			this.storeResult.Value = Quaternion.Lerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
		}

		// Token: 0x0400899A RID: 35226
		[RequiredField]
		[Tooltip("From Quaternion.")]
		public FsmQuaternion fromQuaternion;

		// Token: 0x0400899B RID: 35227
		[RequiredField]
		[Tooltip("To Quaternion.")]
		public FsmQuaternion toQuaternion;

		// Token: 0x0400899C RID: 35228
		[RequiredField]
		[Tooltip("Interpolate between fromQuaternion and toQuaternion by this amount. Value is clamped to 0-1 range. 0 = fromQuaternion; 1 = toQuaternion; 0.5 = half way between.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat amount;

		// Token: 0x0400899D RID: 35229
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in this quaternion variable.")]
		public FsmQuaternion storeResult;
	}
}
