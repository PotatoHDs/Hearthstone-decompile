using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EBE RID: 3774
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Performs most possible operations on 2 Vector3: Dot product, Cross product, Distance, Angle, Project, Reflect, Add, Subtract, Multiply, Divide, Min, Max")]
	public class Vector3Operator : FsmStateAction
	{
		// Token: 0x0600AA43 RID: 43587 RVA: 0x00354D23 File Offset: 0x00352F23
		public override void Reset()
		{
			this.vector1 = null;
			this.vector2 = null;
			this.operation = Vector3Operator.Vector3Operation.Add;
			this.storeVector3Result = null;
			this.storeFloatResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AA44 RID: 43588 RVA: 0x00354D4F File Offset: 0x00352F4F
		public override void OnEnter()
		{
			this.DoVector3Operator();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA45 RID: 43589 RVA: 0x00354D65 File Offset: 0x00352F65
		public override void OnUpdate()
		{
			this.DoVector3Operator();
		}

		// Token: 0x0600AA46 RID: 43590 RVA: 0x00354D70 File Offset: 0x00352F70
		private void DoVector3Operator()
		{
			Vector3 value = this.vector1.Value;
			Vector3 value2 = this.vector2.Value;
			switch (this.operation)
			{
			case Vector3Operator.Vector3Operation.DotProduct:
				this.storeFloatResult.Value = Vector3.Dot(value, value2);
				return;
			case Vector3Operator.Vector3Operation.CrossProduct:
				this.storeVector3Result.Value = Vector3.Cross(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Distance:
				this.storeFloatResult.Value = Vector3.Distance(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Angle:
				this.storeFloatResult.Value = Vector3.Angle(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Project:
				this.storeVector3Result.Value = Vector3.Project(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Reflect:
				this.storeVector3Result.Value = Vector3.Reflect(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Add:
				this.storeVector3Result.Value = value + value2;
				return;
			case Vector3Operator.Vector3Operation.Subtract:
				this.storeVector3Result.Value = value - value2;
				return;
			case Vector3Operator.Vector3Operation.Multiply:
			{
				Vector3 zero = Vector3.zero;
				zero.x = value.x * value2.x;
				zero.y = value.y * value2.y;
				zero.z = value.z * value2.z;
				this.storeVector3Result.Value = zero;
				return;
			}
			case Vector3Operator.Vector3Operation.Divide:
			{
				Vector3 zero2 = Vector3.zero;
				zero2.x = value.x / value2.x;
				zero2.y = value.y / value2.y;
				zero2.z = value.z / value2.z;
				this.storeVector3Result.Value = zero2;
				return;
			}
			case Vector3Operator.Vector3Operation.Min:
				this.storeVector3Result.Value = Vector3.Min(value, value2);
				return;
			case Vector3Operator.Vector3Operation.Max:
				this.storeVector3Result.Value = Vector3.Max(value, value2);
				return;
			default:
				return;
			}
		}

		// Token: 0x040090DC RID: 37084
		[RequiredField]
		public FsmVector3 vector1;

		// Token: 0x040090DD RID: 37085
		[RequiredField]
		public FsmVector3 vector2;

		// Token: 0x040090DE RID: 37086
		public Vector3Operator.Vector3Operation operation = Vector3Operator.Vector3Operation.Add;

		// Token: 0x040090DF RID: 37087
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector3Result;

		// Token: 0x040090E0 RID: 37088
		[UIHint(UIHint.Variable)]
		public FsmFloat storeFloatResult;

		// Token: 0x040090E1 RID: 37089
		public bool everyFrame;

		// Token: 0x020027B3 RID: 10163
		public enum Vector3Operation
		{
			// Token: 0x0400F557 RID: 62807
			DotProduct,
			// Token: 0x0400F558 RID: 62808
			CrossProduct,
			// Token: 0x0400F559 RID: 62809
			Distance,
			// Token: 0x0400F55A RID: 62810
			Angle,
			// Token: 0x0400F55B RID: 62811
			Project,
			// Token: 0x0400F55C RID: 62812
			Reflect,
			// Token: 0x0400F55D RID: 62813
			Add,
			// Token: 0x0400F55E RID: 62814
			Subtract,
			// Token: 0x0400F55F RID: 62815
			Multiply,
			// Token: 0x0400F560 RID: 62816
			Divide,
			// Token: 0x0400F561 RID: 62817
			Min,
			// Token: 0x0400F562 RID: 62818
			Max
		}
	}
}
