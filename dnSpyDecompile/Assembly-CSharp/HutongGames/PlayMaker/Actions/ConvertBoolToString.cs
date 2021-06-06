using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF7 RID: 3063
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Bool value to a String value.")]
	public class ConvertBoolToString : FsmStateAction
	{
		// Token: 0x06009D73 RID: 40307 RVA: 0x0032905E File Offset: 0x0032725E
		public override void Reset()
		{
			this.boolVariable = null;
			this.stringVariable = null;
			this.falseString = "False";
			this.trueString = "True";
			this.everyFrame = false;
		}

		// Token: 0x06009D74 RID: 40308 RVA: 0x00329095 File Offset: 0x00327295
		public override void OnEnter()
		{
			this.DoConvertBoolToString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D75 RID: 40309 RVA: 0x003290AB File Offset: 0x003272AB
		public override void OnUpdate()
		{
			this.DoConvertBoolToString();
		}

		// Token: 0x06009D76 RID: 40310 RVA: 0x003290B3 File Offset: 0x003272B3
		private void DoConvertBoolToString()
		{
			this.stringVariable.Value = (this.boolVariable.Value ? this.trueString.Value : this.falseString.Value);
		}

		// Token: 0x040082E6 RID: 33510
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		// Token: 0x040082E7 RID: 33511
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The String variable to set based on the Bool variable value.")]
		public FsmString stringVariable;

		// Token: 0x040082E8 RID: 33512
		[Tooltip("String value if Bool variable is false.")]
		public FsmString falseString;

		// Token: 0x040082E9 RID: 33513
		[Tooltip("String value if Bool variable is true.")]
		public FsmString trueString;

		// Token: 0x040082EA RID: 33514
		[Tooltip("Repeat every frame. Useful if the Bool variable is changing.")]
		public bool everyFrame;
	}
}
