using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC1 RID: 3265
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Vertical Slider connected to a Float Variable.")]
	public class GUIVerticalSlider : GUIAction
	{
		// Token: 0x0600A0B0 RID: 41136 RVA: 0x00332078 File Offset: 0x00330278
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.topValue = 100f;
			this.bottomValue = 0f;
			this.sliderStyle = "verticalslider";
			this.thumbStyle = "verticalsliderthumb";
			this.width = 0.1f;
		}

		// Token: 0x0600A0B1 RID: 41137 RVA: 0x003320E4 File Offset: 0x003302E4
		public override void OnGUI()
		{
			base.OnGUI();
			if (this.floatVariable != null)
			{
				this.floatVariable.Value = GUI.VerticalSlider(this.rect, this.floatVariable.Value, this.topValue.Value, this.bottomValue.Value, (this.sliderStyle.Value != "") ? this.sliderStyle.Value : "verticalslider", (this.thumbStyle.Value != "") ? this.thumbStyle.Value : "verticalsliderthumb");
			}
		}

		// Token: 0x0400863E RID: 34366
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x0400863F RID: 34367
		[RequiredField]
		public FsmFloat topValue;

		// Token: 0x04008640 RID: 34368
		[RequiredField]
		public FsmFloat bottomValue;

		// Token: 0x04008641 RID: 34369
		public FsmString sliderStyle;

		// Token: 0x04008642 RID: 34370
		public FsmString thumbStyle;
	}
}
