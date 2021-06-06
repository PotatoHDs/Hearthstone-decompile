using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB8 RID: 3512
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of a Float Variable.")]
	public class SetFloatValue : FsmStateAction
	{
		// Token: 0x0600A58A RID: 42378 RVA: 0x00346DD2 File Offset: 0x00344FD2
		public override void Reset()
		{
			this.floatVariable = null;
			this.floatValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A58B RID: 42379 RVA: 0x00346DE9 File Offset: 0x00344FE9
		public override void OnEnter()
		{
			this.floatVariable.Value = this.floatValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A58C RID: 42380 RVA: 0x00346E0F File Offset: 0x0034500F
		public override void OnUpdate()
		{
			this.floatVariable.Value = this.floatValue.Value;
		}

		// Token: 0x04008C12 RID: 35858
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x04008C13 RID: 35859
		[RequiredField]
		public FsmFloat floatValue;

		// Token: 0x04008C14 RID: 35860
		public bool everyFrame;
	}
}
