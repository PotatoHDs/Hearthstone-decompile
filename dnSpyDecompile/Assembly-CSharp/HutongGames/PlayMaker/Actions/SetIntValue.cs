using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD9 RID: 3545
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of an Integer Variable.")]
	public class SetIntValue : FsmStateAction
	{
		// Token: 0x0600A61E RID: 42526 RVA: 0x003487AE File Offset: 0x003469AE
		public override void Reset()
		{
			this.intVariable = null;
			this.intValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A61F RID: 42527 RVA: 0x003487C5 File Offset: 0x003469C5
		public override void OnEnter()
		{
			this.intVariable.Value = this.intValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A620 RID: 42528 RVA: 0x003487EB File Offset: 0x003469EB
		public override void OnUpdate()
		{
			this.intVariable.Value = this.intValue.Value;
		}

		// Token: 0x04008CB8 RID: 36024
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Int Variable to Set")]
		public FsmInt intVariable;

		// Token: 0x04008CB9 RID: 36025
		[RequiredField]
		[Tooltip("Int Value")]
		public FsmInt intValue;

		// Token: 0x04008CBA RID: 36026
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
