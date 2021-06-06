using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF5 RID: 3061
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Bool value to a Float value.")]
	public class ConvertBoolToFloat : FsmStateAction
	{
		// Token: 0x06009D69 RID: 40297 RVA: 0x00328F58 File Offset: 0x00327158
		public override void Reset()
		{
			this.boolVariable = null;
			this.floatVariable = null;
			this.falseValue = 0f;
			this.trueValue = 1f;
			this.everyFrame = false;
		}

		// Token: 0x06009D6A RID: 40298 RVA: 0x00328F8F File Offset: 0x0032718F
		public override void OnEnter()
		{
			this.DoConvertBoolToFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x00328FA5 File Offset: 0x003271A5
		public override void OnUpdate()
		{
			this.DoConvertBoolToFloat();
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x00328FAD File Offset: 0x003271AD
		private void DoConvertBoolToFloat()
		{
			this.floatVariable.Value = (this.boolVariable.Value ? this.trueValue.Value : this.falseValue.Value);
		}

		// Token: 0x040082DC RID: 33500
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		// Token: 0x040082DD RID: 33501
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to set based on the Bool variable value.")]
		public FsmFloat floatVariable;

		// Token: 0x040082DE RID: 33502
		[Tooltip("Float value if Bool variable is false.")]
		public FsmFloat falseValue;

		// Token: 0x040082DF RID: 33503
		[Tooltip("Float value if Bool variable is true.")]
		public FsmFloat trueValue;

		// Token: 0x040082E0 RID: 33504
		[Tooltip("Repeat every frame. Useful if the Bool variable is changing.")]
		public bool everyFrame;
	}
}
