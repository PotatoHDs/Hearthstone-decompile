using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E16 RID: 3606
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Splits a string into substrings using separator characters.")]
	public class StringSplit : FsmStateAction
	{
		// Token: 0x0600A722 RID: 42786 RVA: 0x0034B777 File Offset: 0x00349977
		public override void Reset()
		{
			this.stringToSplit = null;
			this.separators = null;
			this.trimStrings = false;
			this.trimChars = null;
			this.stringArray = null;
		}

		// Token: 0x0600A723 RID: 42787 RVA: 0x0034B7A4 File Offset: 0x003499A4
		public override void OnEnter()
		{
			char[] array = this.trimChars.Value.ToCharArray();
			if (!this.stringToSplit.IsNone && !this.stringArray.IsNone)
			{
				FsmArray fsmArray = this.stringArray;
				object[] values = this.stringToSplit.Value.Split(this.separators.Value.ToCharArray());
				fsmArray.Values = values;
				if (this.trimStrings.Value)
				{
					for (int i = 0; i < this.stringArray.Values.Length; i++)
					{
						string text = this.stringArray.Values[i] as string;
						if (text != null)
						{
							if (!this.trimChars.IsNone && array.Length != 0)
							{
								this.stringArray.Set(i, text.Trim(array));
							}
							else
							{
								this.stringArray.Set(i, text.Trim());
							}
						}
					}
				}
				this.stringArray.SaveChanges();
			}
			base.Finish();
		}

		// Token: 0x04008D9E RID: 36254
		[UIHint(UIHint.Variable)]
		[Tooltip("String to split.")]
		public FsmString stringToSplit;

		// Token: 0x04008D9F RID: 36255
		[Tooltip("Characters used to split the string.\nUse '\\n' for newline\nUse '\\t' for tab")]
		public FsmString separators;

		// Token: 0x04008DA0 RID: 36256
		[Tooltip("Remove all leading and trailing white-space characters from each separated string.")]
		public FsmBool trimStrings;

		// Token: 0x04008DA1 RID: 36257
		[Tooltip("Optional characters used to trim each separated string.")]
		public FsmString trimChars;

		// Token: 0x04008DA2 RID: 36258
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.String, "", 0, 0, 65536)]
		[Tooltip("Store the split strings in a String Array.")]
		public FsmArray stringArray;
	}
}
