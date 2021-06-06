using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF4 RID: 3060
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Bool value to a Color.")]
	public class ConvertBoolToColor : FsmStateAction
	{
		// Token: 0x06009D64 RID: 40292 RVA: 0x00328ED1 File Offset: 0x003270D1
		public override void Reset()
		{
			this.boolVariable = null;
			this.colorVariable = null;
			this.falseColor = Color.black;
			this.trueColor = Color.white;
			this.everyFrame = false;
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x00328F08 File Offset: 0x00327108
		public override void OnEnter()
		{
			this.DoConvertBoolToColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x00328F1E File Offset: 0x0032711E
		public override void OnUpdate()
		{
			this.DoConvertBoolToColor();
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x00328F26 File Offset: 0x00327126
		private void DoConvertBoolToColor()
		{
			this.colorVariable.Value = (this.boolVariable.Value ? this.trueColor.Value : this.falseColor.Value);
		}

		// Token: 0x040082D7 RID: 33495
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		// Token: 0x040082D8 RID: 33496
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color variable to set based on the bool variable value.")]
		public FsmColor colorVariable;

		// Token: 0x040082D9 RID: 33497
		[Tooltip("Color if Bool variable is false.")]
		public FsmColor falseColor;

		// Token: 0x040082DA RID: 33498
		[Tooltip("Color if Bool variable is true.")]
		public FsmColor trueColor;

		// Token: 0x040082DB RID: 33499
		[Tooltip("Repeat every frame. Useful if the Bool variable is changing.")]
		public bool everyFrame;
	}
}
