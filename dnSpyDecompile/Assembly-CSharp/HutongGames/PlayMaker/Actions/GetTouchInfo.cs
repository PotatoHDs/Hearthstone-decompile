using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C91 RID: 3217
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets info on a touch event.")]
	public class GetTouchInfo : FsmStateAction
	{
		// Token: 0x0600A00E RID: 40974 RVA: 0x0032FCE4 File Offset: 0x0032DEE4
		public override void Reset()
		{
			this.fingerId = new FsmInt
			{
				UseVariable = true
			};
			this.normalize = true;
			this.storePosition = null;
			this.storeDeltaPosition = null;
			this.storeDeltaTime = null;
			this.storeTapCount = null;
			this.everyFrame = true;
		}

		// Token: 0x0600A00F RID: 40975 RVA: 0x0032FD32 File Offset: 0x0032DF32
		public override void OnEnter()
		{
			this.screenWidth = (float)Screen.width;
			this.screenHeight = (float)Screen.height;
			this.DoGetTouchInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A010 RID: 40976 RVA: 0x0032FD60 File Offset: 0x0032DF60
		public override void OnUpdate()
		{
			this.DoGetTouchInfo();
		}

		// Token: 0x0600A011 RID: 40977 RVA: 0x0032FD68 File Offset: 0x0032DF68
		private void DoGetTouchInfo()
		{
			if (Input.touchCount > 0)
			{
				foreach (Touch touch in Input.touches)
				{
					if (this.fingerId.IsNone || touch.fingerId == this.fingerId.Value)
					{
						float num = (!this.normalize.Value) ? touch.position.x : (touch.position.x / this.screenWidth);
						float num2 = (!this.normalize.Value) ? touch.position.y : (touch.position.y / this.screenHeight);
						if (!this.storePosition.IsNone)
						{
							this.storePosition.Value = new Vector3(num, num2, 0f);
						}
						this.storeX.Value = num;
						this.storeY.Value = num2;
						float num3 = (!this.normalize.Value) ? touch.deltaPosition.x : (touch.deltaPosition.x / this.screenWidth);
						float num4 = (!this.normalize.Value) ? touch.deltaPosition.y : (touch.deltaPosition.y / this.screenHeight);
						if (!this.storeDeltaPosition.IsNone)
						{
							this.storeDeltaPosition.Value = new Vector3(num3, num4, 0f);
						}
						this.storeDeltaX.Value = num3;
						this.storeDeltaY.Value = num4;
						this.storeDeltaTime.Value = touch.deltaTime;
						this.storeTapCount.Value = touch.tapCount;
					}
				}
			}
		}

		// Token: 0x04008588 RID: 34184
		[Tooltip("Filter by a Finger ID. You can store a Finger ID in other Touch actions, e.g., Touch Event.")]
		public FsmInt fingerId;

		// Token: 0x04008589 RID: 34185
		[Tooltip("If true, all screen coordinates are returned normalized (0-1), otherwise in pixels.")]
		public FsmBool normalize;

		// Token: 0x0400858A RID: 34186
		[UIHint(UIHint.Variable)]
		public FsmVector3 storePosition;

		// Token: 0x0400858B RID: 34187
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;

		// Token: 0x0400858C RID: 34188
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;

		// Token: 0x0400858D RID: 34189
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeDeltaPosition;

		// Token: 0x0400858E RID: 34190
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDeltaX;

		// Token: 0x0400858F RID: 34191
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDeltaY;

		// Token: 0x04008590 RID: 34192
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDeltaTime;

		// Token: 0x04008591 RID: 34193
		[UIHint(UIHint.Variable)]
		public FsmInt storeTapCount;

		// Token: 0x04008592 RID: 34194
		public bool everyFrame = true;

		// Token: 0x04008593 RID: 34195
		private float screenWidth;

		// Token: 0x04008594 RID: 34196
		private float screenHeight;
	}
}
