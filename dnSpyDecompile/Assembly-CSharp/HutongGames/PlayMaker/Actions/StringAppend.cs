using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E14 RID: 3604
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Adds a String to the end of a String.")]
	public class StringAppend : FsmStateAction
	{
		// Token: 0x0600A71D RID: 42781 RVA: 0x0034B6EB File Offset: 0x003498EB
		public override void Reset()
		{
			this.stringVariable = null;
			this.appendString = null;
		}

		// Token: 0x0600A71E RID: 42782 RVA: 0x0034B6FB File Offset: 0x003498FB
		public override void OnEnter()
		{
			FsmString fsmString = this.stringVariable;
			fsmString.Value += this.appendString.Value;
			base.Finish();
		}

		// Token: 0x04008D99 RID: 36249
		[RequiredField]
		[Tooltip("Strings to add to.")]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008D9A RID: 36250
		[Tooltip("String to append")]
		public FsmString appendString;
	}
}
