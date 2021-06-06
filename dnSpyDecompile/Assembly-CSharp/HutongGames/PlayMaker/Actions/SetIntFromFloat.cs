using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD8 RID: 3544
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of an integer variable using a float value.")]
	public class SetIntFromFloat : FsmStateAction
	{
		// Token: 0x0600A61A RID: 42522 RVA: 0x00348757 File Offset: 0x00346957
		public override void Reset()
		{
			this.intVariable = null;
			this.floatValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A61B RID: 42523 RVA: 0x0034876E File Offset: 0x0034696E
		public override void OnEnter()
		{
			this.intVariable.Value = (int)this.floatValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A61C RID: 42524 RVA: 0x00348795 File Offset: 0x00346995
		public override void OnUpdate()
		{
			this.intVariable.Value = (int)this.floatValue.Value;
		}

		// Token: 0x04008CB5 RID: 36021
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		// Token: 0x04008CB6 RID: 36022
		public FsmFloat floatValue;

		// Token: 0x04008CB7 RID: 36023
		public bool everyFrame;
	}
}
