using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB0 RID: 3760
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Performs most possible operations on 2 Vector2: Dot product, Distance, Angle, Add, Subtract, Multiply, Divide, Min, Max")]
	public class Vector2Operator : FsmStateAction
	{
		// Token: 0x0600AA06 RID: 43526 RVA: 0x00354127 File Offset: 0x00352327
		public override void Reset()
		{
			this.vector1 = null;
			this.vector2 = null;
			this.operation = Vector2Operator.Vector2Operation.Add;
			this.storeVector2Result = null;
			this.storeFloatResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA07 RID: 43527 RVA: 0x00354153 File Offset: 0x00352353
		public override void OnEnter()
		{
			this.DoVector2Operator();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA08 RID: 43528 RVA: 0x00354169 File Offset: 0x00352369
		public override void OnUpdate()
		{
			this.DoVector2Operator();
		}

		// Token: 0x0600AA09 RID: 43529 RVA: 0x00354174 File Offset: 0x00352374
		private void DoVector2Operator()
		{
			Vector2 value = this.vector1.Value;
			Vector2 value2 = this.vector2.Value;
			switch (this.operation)
			{
			case Vector2Operator.Vector2Operation.DotProduct:
				this.storeFloatResult.Value = Vector2.Dot(value, value2);
				return;
			case Vector2Operator.Vector2Operation.Distance:
				this.storeFloatResult.Value = Vector2.Distance(value, value2);
				return;
			case Vector2Operator.Vector2Operation.Angle:
				this.storeFloatResult.Value = Vector2.Angle(value, value2);
				return;
			case Vector2Operator.Vector2Operation.Add:
				this.storeVector2Result.Value = value + value2;
				return;
			case Vector2Operator.Vector2Operation.Subtract:
				this.storeVector2Result.Value = value - value2;
				return;
			case Vector2Operator.Vector2Operation.Multiply:
			{
				Vector2 zero = Vector2.zero;
				zero.x = value.x * value2.x;
				zero.y = value.y * value2.y;
				this.storeVector2Result.Value = zero;
				return;
			}
			case Vector2Operator.Vector2Operation.Divide:
			{
				Vector2 zero2 = Vector2.zero;
				zero2.x = value.x / value2.x;
				zero2.y = value.y / value2.y;
				this.storeVector2Result.Value = zero2;
				return;
			}
			case Vector2Operator.Vector2Operation.Min:
				this.storeVector2Result.Value = Vector2.Min(value, value2);
				return;
			case Vector2Operator.Vector2Operation.Max:
				this.storeVector2Result.Value = Vector2.Max(value, value2);
				return;
			default:
				return;
			}
		}

		// Token: 0x040090A4 RID: 37028
		[RequiredField]
		[Tooltip("The first vector")]
		public FsmVector2 vector1;

		// Token: 0x040090A5 RID: 37029
		[RequiredField]
		[Tooltip("The second vector")]
		public FsmVector2 vector2;

		// Token: 0x040090A6 RID: 37030
		[Tooltip("The operation")]
		public Vector2Operator.Vector2Operation operation = Vector2Operator.Vector2Operation.Add;

		// Token: 0x040090A7 RID: 37031
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 result when it applies.")]
		public FsmVector2 storeVector2Result;

		// Token: 0x040090A8 RID: 37032
		[UIHint(UIHint.Variable)]
		[Tooltip("The float result when it applies")]
		public FsmFloat storeFloatResult;

		// Token: 0x040090A9 RID: 37033
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x020027B2 RID: 10162
		public enum Vector2Operation
		{
			// Token: 0x0400F54D RID: 62797
			DotProduct,
			// Token: 0x0400F54E RID: 62798
			Distance,
			// Token: 0x0400F54F RID: 62799
			Angle,
			// Token: 0x0400F550 RID: 62800
			Add,
			// Token: 0x0400F551 RID: 62801
			Subtract,
			// Token: 0x0400F552 RID: 62802
			Multiply,
			// Token: 0x0400F553 RID: 62803
			Divide,
			// Token: 0x0400F554 RID: 62804
			Min,
			// Token: 0x0400F555 RID: 62805
			Max
		}
	}
}
