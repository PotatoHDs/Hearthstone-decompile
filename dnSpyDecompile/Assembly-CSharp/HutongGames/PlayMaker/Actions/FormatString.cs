using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C36 RID: 3126
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Replaces each format item in a specified string with the text equivalent of variable's value. Stores the result in a string variable.")]
	public class FormatString : FsmStateAction
	{
		// Token: 0x06009E79 RID: 40569 RVA: 0x0032B938 File Offset: 0x00329B38
		public override void Reset()
		{
			this.format = null;
			this.variables = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E7A RID: 40570 RVA: 0x0032B956 File Offset: 0x00329B56
		public override void OnEnter()
		{
			this.objectArray = new object[this.variables.Length];
			this.DoFormatString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E7B RID: 40571 RVA: 0x0032B97F File Offset: 0x00329B7F
		public override void OnUpdate()
		{
			this.DoFormatString();
		}

		// Token: 0x06009E7C RID: 40572 RVA: 0x0032B988 File Offset: 0x00329B88
		private void DoFormatString()
		{
			for (int i = 0; i < this.variables.Length; i++)
			{
				this.variables[i].UpdateValue();
				this.objectArray[i] = this.variables[i].GetValue();
			}
			try
			{
				this.storeResult.Value = string.Format(this.format.Value, this.objectArray);
			}
			catch (FormatException ex)
			{
				base.LogError(ex.Message);
				base.Finish();
			}
		}

		// Token: 0x040083CB RID: 33739
		[RequiredField]
		[Tooltip("E.g. Hello {0} and {1}\nWith 2 variables that replace {0} and {1}\nSee C# string.Format docs.")]
		public FsmString format;

		// Token: 0x040083CC RID: 33740
		[Tooltip("Variables to use for each formatting item.")]
		public FsmVar[] variables;

		// Token: 0x040083CD RID: 33741
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the formatted result in a string variable.")]
		public FsmString storeResult;

		// Token: 0x040083CE RID: 33742
		[Tooltip("Repeat every frame. This is useful if the variables are changing.")]
		public bool everyFrame;

		// Token: 0x040083CF RID: 33743
		private object[] objectArray;
	}
}
