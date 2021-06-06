using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BEC RID: 3052
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Interpolate through an array of Colors over a specified amount of Time.")]
	public class ColorInterpolate : FsmStateAction
	{
		// Token: 0x06009D39 RID: 40249 RVA: 0x00328551 File Offset: 0x00326751
		public override void Reset()
		{
			this.colors = new FsmColor[3];
			this.time = 1f;
			this.storeColor = null;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x06009D3A RID: 40250 RVA: 0x00328584 File Offset: 0x00326784
		public override void OnEnter()
		{
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.currentTime = 0f;
			if (this.colors.Length < 2)
			{
				if (this.colors.Length == 1)
				{
					this.storeColor.Value = this.colors[0].Value;
				}
				base.Finish();
				return;
			}
			this.storeColor.Value = this.colors[0].Value;
		}

		// Token: 0x06009D3B RID: 40251 RVA: 0x003285F4 File Offset: 0x003267F4
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
				this.storeColor.Value = this.colors[this.colors.Length - 1].Value;
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
				return;
			}
			float num = (float)(this.colors.Length - 1) * this.currentTime / this.time.Value;
			Color value;
			if (num.Equals(0f))
			{
				value = this.colors[0].Value;
			}
			else if (num.Equals((float)(this.colors.Length - 1)))
			{
				value = this.colors[this.colors.Length - 1].Value;
			}
			else
			{
				Color value2 = this.colors[Mathf.FloorToInt(num)].Value;
				Color value3 = this.colors[Mathf.CeilToInt(num)].Value;
				num -= Mathf.Floor(num);
				value = Color.Lerp(value2, value3, num);
			}
			this.storeColor.Value = value;
		}

		// Token: 0x06009D3C RID: 40252 RVA: 0x0032872F File Offset: 0x0032692F
		public override string ErrorCheck()
		{
			if (this.colors.Length >= 2)
			{
				return null;
			}
			return "Define at least 2 colors to make a gradient.";
		}

		// Token: 0x040082AC RID: 33452
		[RequiredField]
		[Tooltip("Array of colors to interpolate through.")]
		public FsmColor[] colors;

		// Token: 0x040082AD RID: 33453
		[RequiredField]
		[Tooltip("Interpolation time.")]
		public FsmFloat time;

		// Token: 0x040082AE RID: 33454
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the interpolated color in a Color variable.")]
		public FsmColor storeColor;

		// Token: 0x040082AF RID: 33455
		[Tooltip("Event to send when the interpolation finishes.")]
		public FsmEvent finishEvent;

		// Token: 0x040082B0 RID: 33456
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		// Token: 0x040082B1 RID: 33457
		private float startTime;

		// Token: 0x040082B2 RID: 33458
		private float currentTime;
	}
}
