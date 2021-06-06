using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9F RID: 3231
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Horizontal Slider connected to a Float Variable.")]
	public class GUIHorizontalSlider : GUIAction
	{
		// Token: 0x0600A047 RID: 41031 RVA: 0x003308B0 File Offset: 0x0032EAB0
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.leftValue = 0f;
			this.rightValue = 100f;
			this.sliderStyle = "horizontalslider";
			this.thumbStyle = "horizontalsliderthumb";
		}

		// Token: 0x0600A048 RID: 41032 RVA: 0x0033090C File Offset: 0x0032EB0C
		public override void OnGUI()
		{
			base.OnGUI();
			if (this.floatVariable != null)
			{
				this.floatVariable.Value = GUI.HorizontalSlider(this.rect, this.floatVariable.Value, this.leftValue.Value, this.rightValue.Value, (this.sliderStyle.Value != "") ? this.sliderStyle.Value : "horizontalslider", (this.thumbStyle.Value != "") ? this.thumbStyle.Value : "horizontalsliderthumb");
			}
		}

		// Token: 0x040085C9 RID: 34249
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x040085CA RID: 34250
		[RequiredField]
		public FsmFloat leftValue;

		// Token: 0x040085CB RID: 34251
		[RequiredField]
		public FsmFloat rightValue;

		// Token: 0x040085CC RID: 34252
		public FsmString sliderStyle;

		// Token: 0x040085CD RID: 34253
		public FsmString thumbStyle;
	}
}
