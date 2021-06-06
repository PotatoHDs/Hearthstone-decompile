using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2F RID: 3119
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Divides one Float by another.")]
	public class FloatDivide : FsmStateAction
	{
		// Token: 0x06009E58 RID: 40536 RVA: 0x0032B45B File Offset: 0x0032965B
		public override void Reset()
		{
			this.floatVariable = null;
			this.divideBy = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E59 RID: 40537 RVA: 0x0032B472 File Offset: 0x00329672
		public override void OnEnter()
		{
			this.floatVariable.Value /= this.divideBy.Value;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E5A RID: 40538 RVA: 0x0032B49F File Offset: 0x0032969F
		public override void OnUpdate()
		{
			this.floatVariable.Value /= this.divideBy.Value;
		}

		// Token: 0x040083AB RID: 33707
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to divide.")]
		public FsmFloat floatVariable;

		// Token: 0x040083AC RID: 33708
		[RequiredField]
		[Tooltip("Divide the float variable by this value.")]
		public FsmFloat divideBy;

		// Token: 0x040083AD RID: 33709
		[Tooltip("Repeat every frame. Useful if the variables are changing.")]
		public bool everyFrame;
	}
}
