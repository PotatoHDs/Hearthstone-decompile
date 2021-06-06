using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F99 RID: 3993
	[ActionCategory("Pegasus")]
	[Tooltip("Interpolate 2 Colors over a specified amount of Time.")]
	public class TwoColorInterpolateAction : FsmStateAction
	{
		// Token: 0x0600ADF7 RID: 44535 RVA: 0x00362BE0 File Offset: 0x00360DE0
		public override void Reset()
		{
			this.color1 = new FsmColor();
			this.color2 = new FsmColor();
			this.color1.Value = Color.black;
			this.color2.Value = Color.white;
			this.time = 1f;
			this.storeColor = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600ADF8 RID: 44536 RVA: 0x00362C48 File Offset: 0x00360E48
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			this.storeColor.Value = this.color1.Value;
		}

		// Token: 0x0600ADF9 RID: 44537 RVA: 0x00362C78 File Offset: 0x00360E78
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
			if (this.currentTime > this.time.Value)
			{
				base.Finish();
				this.storeColor.Value = this.color2.Value;
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				return;
			}
			float num = this.currentTime / this.time.Value;
			Color value;
			if (num.Equals(0f))
			{
				value = this.color1.Value;
			}
			else if (num >= 1f)
			{
				value = this.color2.Value;
			}
			else
			{
				value = Color.Lerp(this.color1.Value, this.color2.Value, num);
			}
			this.storeColor.Value = value;
		}

		// Token: 0x040094C2 RID: 38082
		[RequiredField]
		[Tooltip("Color 1")]
		public FsmColor color1;

		// Token: 0x040094C3 RID: 38083
		[RequiredField]
		[Tooltip("Color 2")]
		public FsmColor color2;

		// Token: 0x040094C4 RID: 38084
		[RequiredField]
		[Tooltip("Interpolation time.")]
		public FsmFloat time;

		// Token: 0x040094C5 RID: 38085
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the interpolated color in a Color variable.")]
		public FsmColor storeColor;

		// Token: 0x040094C6 RID: 38086
		[Tooltip("Event to send when the interpolation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x040094C7 RID: 38087
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		// Token: 0x040094C8 RID: 38088
		private float startTime;

		// Token: 0x040094C9 RID: 38089
		private float currentTime;
	}
}
