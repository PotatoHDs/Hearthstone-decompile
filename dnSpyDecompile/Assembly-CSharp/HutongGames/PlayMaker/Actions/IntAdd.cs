using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC3 RID: 3267
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Adds a value to an Integer Variable.")]
	public class IntAdd : FsmStateAction
	{
		// Token: 0x0600A0B9 RID: 41145 RVA: 0x00332300 File Offset: 0x00330500
		public override void Reset()
		{
			this.intVariable = null;
			this.add = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0BA RID: 41146 RVA: 0x00332317 File Offset: 0x00330517
		public override void OnEnter()
		{
			this.intVariable.Value += this.add.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0BB RID: 41147 RVA: 0x00332344 File Offset: 0x00330544
		public override void OnUpdate()
		{
			this.intVariable.Value += this.add.Value;
		}

		// Token: 0x0400864B RID: 34379
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		// Token: 0x0400864C RID: 34380
		[RequiredField]
		public FsmInt add;

		// Token: 0x0400864D RID: 34381
		public bool everyFrame;
	}
}
