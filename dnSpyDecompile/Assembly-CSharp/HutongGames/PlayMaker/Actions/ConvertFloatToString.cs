using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFA RID: 3066
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Float value to a String value with optional format.")]
	public class ConvertFloatToString : FsmStateAction
	{
		// Token: 0x06009D82 RID: 40322 RVA: 0x00329202 File Offset: 0x00327402
		public override void Reset()
		{
			this.floatVariable = null;
			this.stringVariable = null;
			this.everyFrame = false;
			this.format = null;
		}

		// Token: 0x06009D83 RID: 40323 RVA: 0x00329220 File Offset: 0x00327420
		public override void OnEnter()
		{
			this.DoConvertFloatToString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D84 RID: 40324 RVA: 0x00329236 File Offset: 0x00327436
		public override void OnUpdate()
		{
			this.DoConvertFloatToString();
		}

		// Token: 0x06009D85 RID: 40325 RVA: 0x00329240 File Offset: 0x00327440
		private void DoConvertFloatToString()
		{
			if (this.format.IsNone || string.IsNullOrEmpty(this.format.Value))
			{
				this.stringVariable.Value = this.floatVariable.Value.ToString();
				return;
			}
			this.stringVariable.Value = this.floatVariable.Value.ToString(this.format.Value);
		}

		// Token: 0x040082F2 RID: 33522
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to convert.")]
		public FsmFloat floatVariable;

		// Token: 0x040082F3 RID: 33523
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("A string variable to store the converted value.")]
		public FsmString stringVariable;

		// Token: 0x040082F4 RID: 33524
		[Tooltip("Optional Format, allows for leading zeros. E.g., 0000")]
		public FsmString format;

		// Token: 0x040082F5 RID: 33525
		[Tooltip("Repeat every frame. Useful if the float variable is changing.")]
		public bool everyFrame;
	}
}
