using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2D RID: 3117
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Clamps the value of Float Variable to a Min/Max range.")]
	public class FloatClamp : FsmStateAction
	{
		// Token: 0x06009E4D RID: 40525 RVA: 0x0032B2A2 File Offset: 0x003294A2
		public override void Reset()
		{
			this.floatVariable = null;
			this.minValue = null;
			this.maxValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E4E RID: 40526 RVA: 0x0032B2C0 File Offset: 0x003294C0
		public override void OnEnter()
		{
			this.DoClamp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E4F RID: 40527 RVA: 0x0032B2D6 File Offset: 0x003294D6
		public override void OnUpdate()
		{
			this.DoClamp();
		}

		// Token: 0x06009E50 RID: 40528 RVA: 0x0032B2DE File Offset: 0x003294DE
		private void DoClamp()
		{
			this.floatVariable.Value = Mathf.Clamp(this.floatVariable.Value, this.minValue.Value, this.maxValue.Value);
		}

		// Token: 0x040083A0 RID: 33696
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Float variable to clamp.")]
		public FsmFloat floatVariable;

		// Token: 0x040083A1 RID: 33697
		[RequiredField]
		[Tooltip("The minimum value.")]
		public FsmFloat minValue;

		// Token: 0x040083A2 RID: 33698
		[RequiredField]
		[Tooltip("The maximum value.")]
		public FsmFloat maxValue;

		// Token: 0x040083A3 RID: 33699
		[Tooltip("Repeat every frame. Useful if the float variable is changing.")]
		public bool everyFrame;
	}
}
