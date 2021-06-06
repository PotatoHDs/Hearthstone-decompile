using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DAE RID: 3502
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of a Bool Variable.")]
	public class SetBoolValue : FsmStateAction
	{
		// Token: 0x0600A55D RID: 42333 RVA: 0x003467AE File Offset: 0x003449AE
		public override void Reset()
		{
			this.boolVariable = null;
			this.boolValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A55E RID: 42334 RVA: 0x003467C5 File Offset: 0x003449C5
		public override void OnEnter()
		{
			this.boolVariable.Value = this.boolValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A55F RID: 42335 RVA: 0x003467EB File Offset: 0x003449EB
		public override void OnUpdate()
		{
			this.boolVariable.Value = this.boolValue.Value;
		}

		// Token: 0x04008BE9 RID: 35817
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool boolVariable;

		// Token: 0x04008BEA RID: 35818
		[RequiredField]
		public FsmBool boolValue;

		// Token: 0x04008BEB RID: 35819
		public bool everyFrame;
	}
}
