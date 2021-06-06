using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F32 RID: 3890
	[ActionCategory("Pegasus")]
	[Tooltip("Uses GameString.Format() and supports GLUE strings. Replaces each format item in a specified string with the text equivalent of variable's value. Stores the result in a string variable.")]
	public class FormatGameString : FsmStateAction
	{
		// Token: 0x0600AC56 RID: 44118 RVA: 0x0035C834 File Offset: 0x0035AA34
		public override void Reset()
		{
			this.format = null;
			this.variables = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AC57 RID: 44119 RVA: 0x0035C852 File Offset: 0x0035AA52
		public override void OnEnter()
		{
			this.objectArray = new object[this.variables.Length];
			this.DoFormatString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC58 RID: 44120 RVA: 0x0035C87B File Offset: 0x0035AA7B
		public override void OnUpdate()
		{
			this.DoFormatString();
		}

		// Token: 0x0600AC59 RID: 44121 RVA: 0x0035C884 File Offset: 0x0035AA84
		private void DoFormatString()
		{
			for (int i = 0; i < this.variables.Length; i++)
			{
				this.variables[i].UpdateValue();
				this.objectArray[i] = this.variables[i].GetValue();
			}
			try
			{
				this.storeResult.Value = GameStrings.Format(this.format.Value, this.objectArray);
			}
			catch (FormatException ex)
			{
				base.LogError(ex.Message);
				base.Finish();
			}
		}

		// Token: 0x04009332 RID: 37682
		[RequiredField]
		[Tooltip("E.g. Hello {0} and {1}\nWith 2 variables that replace {0} and {1}\nSee C# string.Format docs.")]
		public FsmString format;

		// Token: 0x04009333 RID: 37683
		[Tooltip("Variables to use for each formatting item.")]
		public FsmVar[] variables;

		// Token: 0x04009334 RID: 37684
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the formatted result in a string variable.")]
		public FsmString storeResult;

		// Token: 0x04009335 RID: 37685
		[Tooltip("Repeat every frame. This is useful if the variables are changing.")]
		public bool everyFrame;

		// Token: 0x04009336 RID: 37686
		private object[] objectArray;
	}
}
