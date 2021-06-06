using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C53 RID: 3155
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the rotation of the device around its z axis (into the screen). For example when you steer with the iPhone in a driving game.")]
	public class GetDeviceRoll : FsmStateAction
	{
		// Token: 0x06009EF8 RID: 40696 RVA: 0x0032CFB8 File Offset: 0x0032B1B8
		public override void Reset()
		{
			this.baseOrientation = GetDeviceRoll.BaseOrientation.LandscapeLeft;
			this.storeAngle = null;
			this.limitAngle = new FsmFloat
			{
				UseVariable = true
			};
			this.smoothing = 5f;
			this.everyFrame = true;
		}

		// Token: 0x06009EF9 RID: 40697 RVA: 0x0032CFF1 File Offset: 0x0032B1F1
		public override void OnEnter()
		{
			this.DoGetDeviceRoll();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EFA RID: 40698 RVA: 0x0032D007 File Offset: 0x0032B207
		public override void OnUpdate()
		{
			this.DoGetDeviceRoll();
		}

		// Token: 0x06009EFB RID: 40699 RVA: 0x0032D010 File Offset: 0x0032B210
		private void DoGetDeviceRoll()
		{
			float x = Input.acceleration.x;
			float y = Input.acceleration.y;
			float num = 0f;
			switch (this.baseOrientation)
			{
			case GetDeviceRoll.BaseOrientation.Portrait:
				num = -Mathf.Atan2(x, -y);
				break;
			case GetDeviceRoll.BaseOrientation.LandscapeLeft:
				num = Mathf.Atan2(y, -x);
				break;
			case GetDeviceRoll.BaseOrientation.LandscapeRight:
				num = -Mathf.Atan2(y, x);
				break;
			}
			if (!this.limitAngle.IsNone)
			{
				num = Mathf.Clamp(57.29578f * num, -this.limitAngle.Value, this.limitAngle.Value);
			}
			if (this.smoothing.Value > 0f)
			{
				num = Mathf.LerpAngle(this.lastZAngle, num, this.smoothing.Value * Time.deltaTime);
			}
			this.lastZAngle = num;
			this.storeAngle.Value = num;
		}

		// Token: 0x04008459 RID: 33881
		[Tooltip("How the user is expected to hold the device (where angle will be zero).")]
		public GetDeviceRoll.BaseOrientation baseOrientation;

		// Token: 0x0400845A RID: 33882
		[UIHint(UIHint.Variable)]
		public FsmFloat storeAngle;

		// Token: 0x0400845B RID: 33883
		public FsmFloat limitAngle;

		// Token: 0x0400845C RID: 33884
		public FsmFloat smoothing;

		// Token: 0x0400845D RID: 33885
		public bool everyFrame;

		// Token: 0x0400845E RID: 33886
		private float lastZAngle;

		// Token: 0x02002799 RID: 10137
		public enum BaseOrientation
		{
			// Token: 0x0400F4C6 RID: 62662
			Portrait,
			// Token: 0x0400F4C7 RID: 62663
			LandscapeLeft,
			// Token: 0x0400F4C8 RID: 62664
			LandscapeRight
		}
	}
}
