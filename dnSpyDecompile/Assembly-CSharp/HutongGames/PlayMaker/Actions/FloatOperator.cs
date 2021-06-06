using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C32 RID: 3122
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs math operations on 2 Floats: Add, Subtract, Multiply, Divide, Min, Max.")]
	public class FloatOperator : FsmStateAction
	{
		// Token: 0x06009E64 RID: 40548 RVA: 0x0032B670 File Offset: 0x00329870
		public override void Reset()
		{
			this.float1 = null;
			this.float2 = null;
			this.operation = FloatOperator.Operation.Add;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E65 RID: 40549 RVA: 0x0032B695 File Offset: 0x00329895
		public override void OnEnter()
		{
			this.DoFloatOperator();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E66 RID: 40550 RVA: 0x0032B6AB File Offset: 0x003298AB
		public override void OnUpdate()
		{
			this.DoFloatOperator();
		}

		// Token: 0x06009E67 RID: 40551 RVA: 0x0032B6B4 File Offset: 0x003298B4
		private void DoFloatOperator()
		{
			float value = this.float1.Value;
			float value2 = this.float2.Value;
			switch (this.operation)
			{
			case FloatOperator.Operation.Add:
				this.storeResult.Value = value + value2;
				return;
			case FloatOperator.Operation.Subtract:
				this.storeResult.Value = value - value2;
				return;
			case FloatOperator.Operation.Multiply:
				this.storeResult.Value = value * value2;
				return;
			case FloatOperator.Operation.Divide:
				this.storeResult.Value = value / value2;
				return;
			case FloatOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(value, value2);
				return;
			case FloatOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(value, value2);
				return;
			default:
				return;
			}
		}

		// Token: 0x040083BA RID: 33722
		[RequiredField]
		[Tooltip("The first float.")]
		public FsmFloat float1;

		// Token: 0x040083BB RID: 33723
		[RequiredField]
		[Tooltip("The second float.")]
		public FsmFloat float2;

		// Token: 0x040083BC RID: 33724
		[Tooltip("The math operation to perform on the floats.")]
		public FloatOperator.Operation operation;

		// Token: 0x040083BD RID: 33725
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the operation in a float variable.")]
		public FsmFloat storeResult;

		// Token: 0x040083BE RID: 33726
		[Tooltip("Repeat every frame. Useful if the variables are changing.")]
		public bool everyFrame;

		// Token: 0x02002797 RID: 10135
		public enum Operation
		{
			// Token: 0x0400F4BB RID: 62651
			Add,
			// Token: 0x0400F4BC RID: 62652
			Subtract,
			// Token: 0x0400F4BD RID: 62653
			Multiply,
			// Token: 0x0400F4BE RID: 62654
			Divide,
			// Token: 0x0400F4BF RID: 62655
			Min,
			// Token: 0x0400F4C0 RID: 62656
			Max
		}
	}
}
