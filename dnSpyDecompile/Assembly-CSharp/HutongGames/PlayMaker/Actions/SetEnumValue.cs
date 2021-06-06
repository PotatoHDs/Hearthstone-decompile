using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB4 RID: 3508
	[ActionCategory(ActionCategory.Enum)]
	[Tooltip("Sets the value of an Enum Variable.")]
	public class SetEnumValue : FsmStateAction
	{
		// Token: 0x0600A57A RID: 42362 RVA: 0x00346B06 File Offset: 0x00344D06
		public override void Reset()
		{
			this.enumVariable = null;
			this.enumValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A57B RID: 42363 RVA: 0x00346B1D File Offset: 0x00344D1D
		public override void OnEnter()
		{
			this.DoSetEnumValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A57C RID: 42364 RVA: 0x00346B33 File Offset: 0x00344D33
		public override void OnUpdate()
		{
			this.DoSetEnumValue();
		}

		// Token: 0x0600A57D RID: 42365 RVA: 0x00346B3B File Offset: 0x00344D3B
		private void DoSetEnumValue()
		{
			this.enumVariable.Value = this.enumValue.Value;
		}

		// Token: 0x04008BFF RID: 35839
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum Variable to set.")]
		public FsmEnum enumVariable;

		// Token: 0x04008C00 RID: 35840
		[MatchFieldType("enumVariable")]
		[Tooltip("The Enum value to set the variable to.")]
		public FsmEnum enumValue;

		// Token: 0x04008C01 RID: 35841
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
