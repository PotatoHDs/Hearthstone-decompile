using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFA RID: 3578
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Sets the value of a String Variable.")]
	public class SetStringValue : FsmStateAction
	{
		// Token: 0x0600A6AF RID: 42671 RVA: 0x00349F09 File Offset: 0x00348109
		public override void Reset()
		{
			this.stringVariable = null;
			this.stringValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A6B0 RID: 42672 RVA: 0x00349F20 File Offset: 0x00348120
		public override void OnEnter()
		{
			this.DoSetStringValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6B1 RID: 42673 RVA: 0x00349F36 File Offset: 0x00348136
		public override void OnUpdate()
		{
			this.DoSetStringValue();
		}

		// Token: 0x0600A6B2 RID: 42674 RVA: 0x00349F3E File Offset: 0x0034813E
		private void DoSetStringValue()
		{
			if (this.stringVariable == null)
			{
				return;
			}
			if (this.stringValue == null)
			{
				return;
			}
			this.stringVariable.Value = this.stringValue.Value;
		}

		// Token: 0x04008D2E RID: 36142
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008D2F RID: 36143
		[UIHint(UIHint.TextArea)]
		public FsmString stringValue;

		// Token: 0x04008D30 RID: 36144
		public bool everyFrame;
	}
}
