using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C35 RID: 3125
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of a Float Variable. The float could represent distance, angle to a target, health left... The array sets up float ranges that correspond to Events.")]
	public class FloatSwitch : FsmStateAction
	{
		// Token: 0x06009E74 RID: 40564 RVA: 0x0032B896 File Offset: 0x00329A96
		public override void Reset()
		{
			this.floatVariable = null;
			this.lessThan = new FsmFloat[1];
			this.sendEvent = new FsmEvent[1];
			this.everyFrame = false;
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x0032B8BE File Offset: 0x00329ABE
		public override void OnEnter()
		{
			this.DoFloatSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E76 RID: 40566 RVA: 0x0032B8D4 File Offset: 0x00329AD4
		public override void OnUpdate()
		{
			this.DoFloatSwitch();
		}

		// Token: 0x06009E77 RID: 40567 RVA: 0x0032B8DC File Offset: 0x00329ADC
		private void DoFloatSwitch()
		{
			if (this.floatVariable.IsNone)
			{
				return;
			}
			for (int i = 0; i < this.lessThan.Length; i++)
			{
				if (this.floatVariable.Value < this.lessThan[i].Value)
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x040083C7 RID: 33735
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The float variable to test.")]
		public FsmFloat floatVariable;

		// Token: 0x040083C8 RID: 33736
		[CompoundArray("Float Switches", "Less Than", "Send Event")]
		public FsmFloat[] lessThan;

		// Token: 0x040083C9 RID: 33737
		public FsmEvent[] sendEvent;

		// Token: 0x040083CA RID: 33738
		[Tooltip("Repeat every frame. Useful if the variable is changing.")]
		public bool everyFrame;
	}
}
