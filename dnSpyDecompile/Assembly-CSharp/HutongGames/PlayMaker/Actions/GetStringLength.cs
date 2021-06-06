using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C89 RID: 3209
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Length of a String.")]
	public class GetStringLength : FsmStateAction
	{
		// Token: 0x06009FE9 RID: 40937 RVA: 0x0032F88A File Offset: 0x0032DA8A
		public override void Reset()
		{
			this.stringVariable = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009FEA RID: 40938 RVA: 0x0032F8A1 File Offset: 0x0032DAA1
		public override void OnEnter()
		{
			this.DoGetStringLength();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009FEB RID: 40939 RVA: 0x0032F8B7 File Offset: 0x0032DAB7
		public override void OnUpdate()
		{
			this.DoGetStringLength();
		}

		// Token: 0x06009FEC RID: 40940 RVA: 0x0032F8BF File Offset: 0x0032DABF
		private void DoGetStringLength()
		{
			if (this.stringVariable == null)
			{
				return;
			}
			if (this.storeResult == null)
			{
				return;
			}
			this.storeResult.Value = this.stringVariable.Value.Length;
		}

		// Token: 0x0400856F RID: 34159
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008570 RID: 34160
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;

		// Token: 0x04008571 RID: 34161
		public bool everyFrame;
	}
}
