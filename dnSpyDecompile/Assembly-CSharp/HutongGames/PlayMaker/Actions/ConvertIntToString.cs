using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFC RID: 3068
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an Integer value to a String value with an optional format.")]
	public class ConvertIntToString : FsmStateAction
	{
		// Token: 0x06009D8C RID: 40332 RVA: 0x00329302 File Offset: 0x00327502
		public override void Reset()
		{
			this.intVariable = null;
			this.stringVariable = null;
			this.everyFrame = false;
			this.format = null;
		}

		// Token: 0x06009D8D RID: 40333 RVA: 0x00329320 File Offset: 0x00327520
		public override void OnEnter()
		{
			this.DoConvertIntToString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D8E RID: 40334 RVA: 0x00329336 File Offset: 0x00327536
		public override void OnUpdate()
		{
			this.DoConvertIntToString();
		}

		// Token: 0x06009D8F RID: 40335 RVA: 0x00329340 File Offset: 0x00327540
		private void DoConvertIntToString()
		{
			if (this.format.IsNone || string.IsNullOrEmpty(this.format.Value))
			{
				this.stringVariable.Value = this.intVariable.Value.ToString();
				return;
			}
			this.stringVariable.Value = this.intVariable.Value.ToString(this.format.Value);
		}

		// Token: 0x040082F9 RID: 33529
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Int variable to convert.")]
		public FsmInt intVariable;

		// Token: 0x040082FA RID: 33530
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("A String variable to store the converted value.")]
		public FsmString stringVariable;

		// Token: 0x040082FB RID: 33531
		[Tooltip("Optional Format, allows for leading zeros. E.g., 0000")]
		public FsmString format;

		// Token: 0x040082FC RID: 33532
		[Tooltip("Repeat every frame. Useful if the Int variable is changing.")]
		public bool everyFrame;
	}
}
