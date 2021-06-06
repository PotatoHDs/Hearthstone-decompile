using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C33 RID: 3123
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the sign of a Float.")]
	public class FloatSignTest : FsmStateAction
	{
		// Token: 0x06009E69 RID: 40553 RVA: 0x0032B760 File Offset: 0x00329960
		public override void Reset()
		{
			this.floatValue = 0f;
			this.isPositive = null;
			this.isNegative = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x0032B787 File Offset: 0x00329987
		public override void OnEnter()
		{
			this.DoSignTest();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E6B RID: 40555 RVA: 0x0032B79D File Offset: 0x0032999D
		public override void OnUpdate()
		{
			this.DoSignTest();
		}

		// Token: 0x06009E6C RID: 40556 RVA: 0x0032B7A5 File Offset: 0x003299A5
		private void DoSignTest()
		{
			if (this.floatValue == null)
			{
				return;
			}
			base.Fsm.Event((this.floatValue.Value < 0f) ? this.isNegative : this.isPositive);
		}

		// Token: 0x06009E6D RID: 40557 RVA: 0x0032B7DB File Offset: 0x003299DB
		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(this.isPositive) && FsmEvent.IsNullOrEmpty(this.isNegative))
			{
				return "Action sends no events!";
			}
			return "";
		}

		// Token: 0x040083BF RID: 33727
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to test.")]
		public FsmFloat floatValue;

		// Token: 0x040083C0 RID: 33728
		[Tooltip("Event to send if the float variable is positive.")]
		public FsmEvent isPositive;

		// Token: 0x040083C1 RID: 33729
		[Tooltip("Event to send if the float variable is negative.")]
		public FsmEvent isNegative;

		// Token: 0x040083C2 RID: 33730
		[Tooltip("Repeat every frame. Useful if the variable is changing and you're waiting for a particular result.")]
		public bool everyFrame;
	}
}
