using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC7 RID: 3271
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs math operation on 2 Integers: Add, Subtract, Multiply, Divide, Min, Max.")]
	public class IntOperator : FsmStateAction
	{
		// Token: 0x0600A0CC RID: 41164 RVA: 0x00332582 File Offset: 0x00330782
		public override void Reset()
		{
			this.integer1 = null;
			this.integer2 = null;
			this.operation = IntOperator.Operation.Add;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0CD RID: 41165 RVA: 0x003325A7 File Offset: 0x003307A7
		public override void OnEnter()
		{
			this.DoIntOperator();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0CE RID: 41166 RVA: 0x003325BD File Offset: 0x003307BD
		public override void OnUpdate()
		{
			this.DoIntOperator();
		}

		// Token: 0x0600A0CF RID: 41167 RVA: 0x003325C8 File Offset: 0x003307C8
		private void DoIntOperator()
		{
			int value = this.integer1.Value;
			int value2 = this.integer2.Value;
			switch (this.operation)
			{
			case IntOperator.Operation.Add:
				this.storeResult.Value = value + value2;
				return;
			case IntOperator.Operation.Subtract:
				this.storeResult.Value = value - value2;
				return;
			case IntOperator.Operation.Multiply:
				this.storeResult.Value = value * value2;
				return;
			case IntOperator.Operation.Divide:
				this.storeResult.Value = value / value2;
				return;
			case IntOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(value, value2);
				return;
			case IntOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(value, value2);
				return;
			default:
				return;
			}
		}

		// Token: 0x0400865C RID: 34396
		[RequiredField]
		public FsmInt integer1;

		// Token: 0x0400865D RID: 34397
		[RequiredField]
		public FsmInt integer2;

		// Token: 0x0400865E RID: 34398
		public IntOperator.Operation operation;

		// Token: 0x0400865F RID: 34399
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;

		// Token: 0x04008660 RID: 34400
		public bool everyFrame;

		// Token: 0x0200279B RID: 10139
		public enum Operation
		{
			// Token: 0x0400F4D3 RID: 62675
			Add,
			// Token: 0x0400F4D4 RID: 62676
			Subtract,
			// Token: 0x0400F4D5 RID: 62677
			Multiply,
			// Token: 0x0400F4D6 RID: 62678
			Divide,
			// Token: 0x0400F4D7 RID: 62679
			Min,
			// Token: 0x0400F4D8 RID: 62680
			Max
		}
	}
}
