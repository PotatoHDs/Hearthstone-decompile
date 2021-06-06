using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E15 RID: 3605
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Join an array of strings into a single string.")]
	public class StringJoin : FsmStateAction
	{
		// Token: 0x0600A720 RID: 42784 RVA: 0x0034B724 File Offset: 0x00349924
		public override void OnEnter()
		{
			if (!this.stringArray.IsNone && !this.storeResult.IsNone)
			{
				this.storeResult.Value = string.Join(this.separator.Value, this.stringArray.stringValues);
			}
			base.Finish();
		}

		// Token: 0x04008D9B RID: 36251
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String, "", 0, 0, 65536)]
		[Tooltip("Array of string to join into a single string.")]
		public FsmArray stringArray;

		// Token: 0x04008D9C RID: 36252
		[Tooltip("Separator to add between each string.")]
		public FsmString separator;

		// Token: 0x04008D9D RID: 36253
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the joined string in string variable.")]
		public FsmString storeResult;
	}
}
