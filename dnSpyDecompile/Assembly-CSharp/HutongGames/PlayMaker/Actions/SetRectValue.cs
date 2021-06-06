using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF5 RID: 3573
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Sets the value of a Rect Variable.")]
	public class SetRectValue : FsmStateAction
	{
		// Token: 0x0600A694 RID: 42644 RVA: 0x00349AAF File Offset: 0x00347CAF
		public override void Reset()
		{
			this.rectVariable = null;
			this.rectValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A695 RID: 42645 RVA: 0x00349AC6 File Offset: 0x00347CC6
		public override void OnEnter()
		{
			this.rectVariable.Value = this.rectValue.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A696 RID: 42646 RVA: 0x00349AEC File Offset: 0x00347CEC
		public override void OnUpdate()
		{
			this.rectVariable.Value = this.rectValue.Value;
		}

		// Token: 0x04008D16 RID: 36118
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		// Token: 0x04008D17 RID: 36119
		[RequiredField]
		public FsmRect rectValue;

		// Token: 0x04008D18 RID: 36120
		public bool everyFrame;
	}
}
