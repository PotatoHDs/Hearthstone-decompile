using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EA9 RID: 3753
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Interpolates between 2 Vector2 values over a specified Time.")]
	public class Vector2Interpolate : FsmStateAction
	{
		// Token: 0x0600A9E9 RID: 43497 RVA: 0x00353C24 File Offset: 0x00351E24
		public override void Reset()
		{
			this.mode = InterpolationType.Linear;
			this.fromVector = new FsmVector2
			{
				UseVariable = true
			};
			this.toVector = new FsmVector2
			{
				UseVariable = true
			};
			this.time = 1f;
			this.storeResult = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600A9EA RID: 43498 RVA: 0x00353C81 File Offset: 0x00351E81
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			if (this.storeResult == null)
			{
				base.Finish();
				return;
			}
			this.storeResult.Value = this.fromVector.Value;
		}

		// Token: 0x0600A9EB RID: 43499 RVA: 0x00353CC0 File Offset: 0x00351EC0
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.currentTime = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.currentTime += Time.deltaTime;
			}
			float num = this.currentTime / this.time.Value;
			InterpolationType interpolationType = this.mode;
			if (interpolationType != InterpolationType.Linear && interpolationType == InterpolationType.EaseInOut)
			{
				num = Mathf.SmoothStep(0f, 1f, num);
			}
			this.storeResult.Value = Vector2.Lerp(this.fromVector.Value, this.toVector.Value, num);
			if (num >= 1f)
			{
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				base.Finish();
			}
		}

		// Token: 0x04009087 RID: 36999
		[Tooltip("The interpolation type")]
		public InterpolationType mode;

		// Token: 0x04009088 RID: 37000
		[RequiredField]
		[Tooltip("The vector to interpolate from")]
		public FsmVector2 fromVector;

		// Token: 0x04009089 RID: 37001
		[RequiredField]
		[Tooltip("The vector to interpolate to")]
		public FsmVector2 toVector;

		// Token: 0x0400908A RID: 37002
		[RequiredField]
		[Tooltip("the interpolate time")]
		public FsmFloat time;

		// Token: 0x0400908B RID: 37003
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the interpolated result")]
		public FsmVector2 storeResult;

		// Token: 0x0400908C RID: 37004
		[Tooltip("This event is fired when the interpolation is done.")]
		public FsmEvent finishEvent;

		// Token: 0x0400908D RID: 37005
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		// Token: 0x0400908E RID: 37006
		private float startTime;

		// Token: 0x0400908F RID: 37007
		private float currentTime;
	}
}
