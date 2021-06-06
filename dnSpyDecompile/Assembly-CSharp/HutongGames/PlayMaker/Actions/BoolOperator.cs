using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE2 RID: 3042
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs boolean operations on 2 Bool Variables.")]
	public class BoolOperator : FsmStateAction
	{
		// Token: 0x06009CF1 RID: 40177 RVA: 0x00326F07 File Offset: 0x00325107
		public override void Reset()
		{
			this.bool1 = false;
			this.bool2 = false;
			this.operation = BoolOperator.Operation.AND;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CF2 RID: 40178 RVA: 0x00326F36 File Offset: 0x00325136
		public override void OnEnter()
		{
			this.DoBoolOperator();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CF3 RID: 40179 RVA: 0x00326F4C File Offset: 0x0032514C
		public override void OnUpdate()
		{
			this.DoBoolOperator();
		}

		// Token: 0x06009CF4 RID: 40180 RVA: 0x00326F54 File Offset: 0x00325154
		private void DoBoolOperator()
		{
			bool value = this.bool1.Value;
			bool value2 = this.bool2.Value;
			switch (this.operation)
			{
			case BoolOperator.Operation.AND:
				this.storeResult.Value = (value && value2);
				return;
			case BoolOperator.Operation.NAND:
				this.storeResult.Value = (!value || !value2);
				return;
			case BoolOperator.Operation.OR:
				this.storeResult.Value = (value || value2);
				return;
			case BoolOperator.Operation.XOR:
				this.storeResult.Value = (value ^ value2);
				return;
			default:
				return;
			}
		}

		// Token: 0x04008266 RID: 33382
		[RequiredField]
		[Tooltip("The first Bool variable.")]
		public FsmBool bool1;

		// Token: 0x04008267 RID: 33383
		[RequiredField]
		[Tooltip("The second Bool variable.")]
		public FsmBool bool2;

		// Token: 0x04008268 RID: 33384
		[Tooltip("Boolean Operation.")]
		public BoolOperator.Operation operation;

		// Token: 0x04008269 RID: 33385
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool Variable.")]
		public FsmBool storeResult;

		// Token: 0x0400826A RID: 33386
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x02002793 RID: 10131
		public enum Operation
		{
			// Token: 0x0400F4AA RID: 62634
			AND,
			// Token: 0x0400F4AB RID: 62635
			NAND,
			// Token: 0x0400F4AC RID: 62636
			OR,
			// Token: 0x0400F4AD RID: 62637
			XOR
		}
	}
}
