using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C30 RID: 3120
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Interpolates between 2 Float values over a specified Time.")]
	public class FloatInterpolate : FsmStateAction
	{
		// Token: 0x06009E5C RID: 40540 RVA: 0x0032B4BE File Offset: 0x003296BE
		public override void Reset()
		{
			this.mode = InterpolationType.Linear;
			this.fromFloat = null;
			this.toFloat = null;
			this.time = 1f;
			this.storeResult = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x06009E5D RID: 40541 RVA: 0x0032B4FA File Offset: 0x003296FA
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			if (this.storeResult == null)
			{
				base.Finish();
				return;
			}
			this.storeResult.Value = this.fromFloat.Value;
		}

		// Token: 0x06009E5E RID: 40542 RVA: 0x0032B538 File Offset: 0x00329738
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
			if (interpolationType != InterpolationType.Linear)
			{
				if (interpolationType == InterpolationType.EaseInOut)
				{
					this.storeResult.Value = Mathf.SmoothStep(this.fromFloat.Value, this.toFloat.Value, num);
				}
			}
			else
			{
				this.storeResult.Value = Mathf.Lerp(this.fromFloat.Value, this.toFloat.Value, num);
			}
			if (num >= 1f)
			{
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				base.Finish();
			}
		}

		// Token: 0x040083AE RID: 33710
		[Tooltip("Interpolation mode: Linear or EaseInOut.")]
		public InterpolationType mode;

		// Token: 0x040083AF RID: 33711
		[RequiredField]
		[Tooltip("Interpolate from this value.")]
		public FsmFloat fromFloat;

		// Token: 0x040083B0 RID: 33712
		[RequiredField]
		[Tooltip("Interpolate to this value.")]
		public FsmFloat toFloat;

		// Token: 0x040083B1 RID: 33713
		[RequiredField]
		[Tooltip("Interpolate over this amount of time in seconds.")]
		public FsmFloat time;

		// Token: 0x040083B2 RID: 33714
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current value in a float variable.")]
		public FsmFloat storeResult;

		// Token: 0x040083B3 RID: 33715
		[Tooltip("Event to send when the interpolation is finished.")]
		public FsmEvent finishEvent;

		// Token: 0x040083B4 RID: 33716
		[Tooltip("Ignore TimeScale. Useful if the game is paused (Time scaled to 0).")]
		public bool realTime;

		// Token: 0x040083B5 RID: 33717
		private float startTime;

		// Token: 0x040083B6 RID: 33718
		private float currentTime;
	}
}
