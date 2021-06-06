using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1A RID: 3610
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Replace a substring with a new String.")]
	public class StringReplace : FsmStateAction
	{
		// Token: 0x0600A733 RID: 42803 RVA: 0x0034BAC9 File Offset: 0x00349CC9
		public override void Reset()
		{
			this.stringVariable = null;
			this.replace = "";
			this.with = "";
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A734 RID: 42804 RVA: 0x0034BB00 File Offset: 0x00349D00
		public override void OnEnter()
		{
			this.DoReplace();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A735 RID: 42805 RVA: 0x0034BB16 File Offset: 0x00349D16
		public override void OnUpdate()
		{
			this.DoReplace();
		}

		// Token: 0x0600A736 RID: 42806 RVA: 0x0034BB20 File Offset: 0x00349D20
		private void DoReplace()
		{
			if (this.stringVariable == null)
			{
				return;
			}
			if (this.storeResult == null)
			{
				return;
			}
			this.storeResult.Value = this.stringVariable.Value.Replace(this.replace.Value, this.with.Value);
		}

		// Token: 0x04008DB3 RID: 36275
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008DB4 RID: 36276
		public FsmString replace;

		// Token: 0x04008DB5 RID: 36277
		public FsmString with;

		// Token: 0x04008DB6 RID: 36278
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		// Token: 0x04008DB7 RID: 36279
		public bool everyFrame;
	}
}
