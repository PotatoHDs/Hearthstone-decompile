using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF6 RID: 3062
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Bool value to an Integer value.")]
	public class ConvertBoolToInt : FsmStateAction
	{
		// Token: 0x06009D6E RID: 40302 RVA: 0x00328FDF File Offset: 0x003271DF
		public override void Reset()
		{
			this.boolVariable = null;
			this.intVariable = null;
			this.falseValue = 0;
			this.trueValue = 1;
			this.everyFrame = false;
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x0032900E File Offset: 0x0032720E
		public override void OnEnter()
		{
			this.DoConvertBoolToInt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x00329024 File Offset: 0x00327224
		public override void OnUpdate()
		{
			this.DoConvertBoolToInt();
		}

		// Token: 0x06009D71 RID: 40305 RVA: 0x0032902C File Offset: 0x0032722C
		private void DoConvertBoolToInt()
		{
			this.intVariable.Value = (this.boolVariable.Value ? this.trueValue.Value : this.falseValue.Value);
		}

		// Token: 0x040082E1 RID: 33505
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		// Token: 0x040082E2 RID: 33506
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Integer variable to set based on the Bool variable value.")]
		public FsmInt intVariable;

		// Token: 0x040082E3 RID: 33507
		[Tooltip("Integer value if Bool variable is false.")]
		public FsmInt falseValue;

		// Token: 0x040082E4 RID: 33508
		[Tooltip("Integer value if Bool variable is false.")]
		public FsmInt trueValue;

		// Token: 0x040082E5 RID: 33509
		[Tooltip("Repeat every frame. Useful if the Bool variable is changing.")]
		public bool everyFrame;
	}
}
