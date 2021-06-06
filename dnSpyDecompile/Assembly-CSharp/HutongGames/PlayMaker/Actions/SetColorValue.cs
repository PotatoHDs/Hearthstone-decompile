using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB2 RID: 3506
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Sets the value of a Color Variable.")]
	public class SetColorValue : FsmStateAction
	{
		// Token: 0x0600A570 RID: 42352 RVA: 0x00346A3A File Offset: 0x00344C3A
		public override void Reset()
		{
			this.colorVariable = null;
			this.color = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A571 RID: 42353 RVA: 0x00346A51 File Offset: 0x00344C51
		public override void OnEnter()
		{
			this.DoSetColorValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A572 RID: 42354 RVA: 0x00346A67 File Offset: 0x00344C67
		public override void OnUpdate()
		{
			this.DoSetColorValue();
		}

		// Token: 0x0600A573 RID: 42355 RVA: 0x00346A6F File Offset: 0x00344C6F
		private void DoSetColorValue()
		{
			if (this.colorVariable != null)
			{
				this.colorVariable.Value = this.color.Value;
			}
		}

		// Token: 0x04008BF9 RID: 35833
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;

		// Token: 0x04008BFA RID: 35834
		[RequiredField]
		public FsmColor color;

		// Token: 0x04008BFB RID: 35835
		public bool everyFrame;
	}
}
