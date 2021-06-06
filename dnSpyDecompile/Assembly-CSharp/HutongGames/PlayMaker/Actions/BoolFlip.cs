using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE0 RID: 3040
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Flips the value of a Bool Variable.")]
	public class BoolFlip : FsmStateAction
	{
		// Token: 0x06009CE9 RID: 40169 RVA: 0x00326E3F File Offset: 0x0032503F
		public override void Reset()
		{
			this.boolVariable = null;
		}

		// Token: 0x06009CEA RID: 40170 RVA: 0x00326E48 File Offset: 0x00325048
		public override void OnEnter()
		{
			this.boolVariable.Value = !this.boolVariable.Value;
			base.Finish();
		}

		// Token: 0x04008261 RID: 33377
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Bool variable to flip.")]
		public FsmBool boolVariable;
	}
}
