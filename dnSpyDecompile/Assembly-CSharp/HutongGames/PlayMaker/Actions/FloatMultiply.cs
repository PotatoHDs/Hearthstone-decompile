using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C31 RID: 3121
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Multiplies one Float by another.")]
	public class FloatMultiply : FsmStateAction
	{
		// Token: 0x06009E60 RID: 40544 RVA: 0x0032B60D File Offset: 0x0032980D
		public override void Reset()
		{
			this.floatVariable = null;
			this.multiplyBy = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E61 RID: 40545 RVA: 0x0032B624 File Offset: 0x00329824
		public override void OnEnter()
		{
			this.floatVariable.Value *= this.multiplyBy.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E62 RID: 40546 RVA: 0x0032B651 File Offset: 0x00329851
		public override void OnUpdate()
		{
			this.floatVariable.Value *= this.multiplyBy.Value;
		}

		// Token: 0x040083B7 RID: 33719
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to multiply.")]
		public FsmFloat floatVariable;

		// Token: 0x040083B8 RID: 33720
		[RequiredField]
		[Tooltip("Multiply the float variable by this value.")]
		public FsmFloat multiplyBy;

		// Token: 0x040083B9 RID: 33721
		[Tooltip("Repeat every frame. Useful if the variables are changing.")]
		public bool everyFrame;
	}
}
