using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C34 RID: 3124
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Subtracts a value from a Float Variable.")]
	public class FloatSubtract : FsmStateAction
	{
		// Token: 0x06009E6F RID: 40559 RVA: 0x0032B802 File Offset: 0x00329A02
		public override void Reset()
		{
			this.floatVariable = null;
			this.subtract = null;
			this.everyFrame = false;
			this.perSecond = false;
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x0032B820 File Offset: 0x00329A20
		public override void OnEnter()
		{
			this.DoFloatSubtract();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x0032B836 File Offset: 0x00329A36
		public override void OnUpdate()
		{
			this.DoFloatSubtract();
		}

		// Token: 0x06009E72 RID: 40562 RVA: 0x0032B840 File Offset: 0x00329A40
		private void DoFloatSubtract()
		{
			if (!this.perSecond)
			{
				this.floatVariable.Value -= this.subtract.Value;
				return;
			}
			this.floatVariable.Value -= this.subtract.Value * Time.deltaTime;
		}

		// Token: 0x040083C3 RID: 33731
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to subtract from.")]
		public FsmFloat floatVariable;

		// Token: 0x040083C4 RID: 33732
		[RequiredField]
		[Tooltip("Value to subtract from the float variable.")]
		public FsmFloat subtract;

		// Token: 0x040083C5 RID: 33733
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040083C6 RID: 33734
		[Tooltip("Used with Every Frame. Adds the value over one second to make the operation frame rate independent.")]
		public bool perSecond;
	}
}
