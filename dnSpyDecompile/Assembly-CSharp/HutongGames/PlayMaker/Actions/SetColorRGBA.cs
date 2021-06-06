using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB1 RID: 3505
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Sets the RGBA channels of a Color Variable. To leave any channel unchanged, set variable to 'None'.")]
	public class SetColorRGBA : FsmStateAction
	{
		// Token: 0x0600A56B RID: 42347 RVA: 0x00346914 File Offset: 0x00344B14
		public override void Reset()
		{
			this.colorVariable = null;
			this.red = 0f;
			this.green = 0f;
			this.blue = 0f;
			this.alpha = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A56C RID: 42348 RVA: 0x0034696F File Offset: 0x00344B6F
		public override void OnEnter()
		{
			this.DoSetColorRGBA();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A56D RID: 42349 RVA: 0x00346985 File Offset: 0x00344B85
		public override void OnUpdate()
		{
			this.DoSetColorRGBA();
		}

		// Token: 0x0600A56E RID: 42350 RVA: 0x00346990 File Offset: 0x00344B90
		private void DoSetColorRGBA()
		{
			if (this.colorVariable == null)
			{
				return;
			}
			Color value = this.colorVariable.Value;
			if (!this.red.IsNone)
			{
				value.r = this.red.Value;
			}
			if (!this.green.IsNone)
			{
				value.g = this.green.Value;
			}
			if (!this.blue.IsNone)
			{
				value.b = this.blue.Value;
			}
			if (!this.alpha.IsNone)
			{
				value.a = this.alpha.Value;
			}
			this.colorVariable.Value = value;
		}

		// Token: 0x04008BF3 RID: 35827
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;

		// Token: 0x04008BF4 RID: 35828
		[HasFloatSlider(0f, 1f)]
		public FsmFloat red;

		// Token: 0x04008BF5 RID: 35829
		[HasFloatSlider(0f, 1f)]
		public FsmFloat green;

		// Token: 0x04008BF6 RID: 35830
		[HasFloatSlider(0f, 1f)]
		public FsmFloat blue;

		// Token: 0x04008BF7 RID: 35831
		[HasFloatSlider(0f, 1f)]
		public FsmFloat alpha;

		// Token: 0x04008BF8 RID: 35832
		public bool everyFrame;
	}
}
