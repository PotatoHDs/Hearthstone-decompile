using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BED RID: 3053
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Samples a Color on a continuous Colors gradient.")]
	public class ColorRamp : FsmStateAction
	{
		// Token: 0x06009D3E RID: 40254 RVA: 0x00328743 File Offset: 0x00326943
		public override void Reset()
		{
			this.colors = new FsmColor[3];
			this.sampleAt = 0f;
			this.storeColor = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D3F RID: 40255 RVA: 0x0032876F File Offset: 0x0032696F
		public override void OnEnter()
		{
			this.DoColorRamp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D40 RID: 40256 RVA: 0x00328785 File Offset: 0x00326985
		public override void OnUpdate()
		{
			this.DoColorRamp();
		}

		// Token: 0x06009D41 RID: 40257 RVA: 0x00328790 File Offset: 0x00326990
		private void DoColorRamp()
		{
			if (this.colors == null)
			{
				return;
			}
			if (this.colors.Length == 0)
			{
				return;
			}
			if (this.sampleAt == null)
			{
				return;
			}
			if (this.storeColor == null)
			{
				return;
			}
			float num = Mathf.Clamp(this.sampleAt.Value, 0f, (float)(this.colors.Length - 1));
			Color value;
			if (num == 0f)
			{
				value = this.colors[0].Value;
			}
			else if (num == (float)this.colors.Length)
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

		// Token: 0x06009D42 RID: 40258 RVA: 0x00328862 File Offset: 0x00326A62
		public override string ErrorCheck()
		{
			if (this.colors.Length < 2)
			{
				return "Define at least 2 colors to make a gradient.";
			}
			return null;
		}

		// Token: 0x040082B3 RID: 33459
		[RequiredField]
		[Tooltip("Array of colors to defining the gradient.")]
		public FsmColor[] colors;

		// Token: 0x040082B4 RID: 33460
		[RequiredField]
		[Tooltip("Point on the gradient to sample. Should be between 0 and the number of colors in the gradient.")]
		public FsmFloat sampleAt;

		// Token: 0x040082B5 RID: 33461
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the sampled color in a Color variable.")]
		public FsmColor storeColor;

		// Token: 0x040082B6 RID: 33462
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
