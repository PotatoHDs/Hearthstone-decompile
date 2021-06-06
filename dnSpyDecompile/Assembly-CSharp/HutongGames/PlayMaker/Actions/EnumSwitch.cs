using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C22 RID: 3106
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of an Enum Variable.")]
	public class EnumSwitch : FsmStateAction
	{
		// Token: 0x06009E1A RID: 40474 RVA: 0x0032AA73 File Offset: 0x00328C73
		public override void Reset()
		{
			this.enumVariable = null;
			this.compareTo = new FsmEnum[0];
			this.sendEvent = new FsmEvent[0];
			this.everyFrame = false;
		}

		// Token: 0x06009E1B RID: 40475 RVA: 0x0032AA9B File Offset: 0x00328C9B
		public override void OnEnter()
		{
			this.DoEnumSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E1C RID: 40476 RVA: 0x0032AAB1 File Offset: 0x00328CB1
		public override void OnUpdate()
		{
			this.DoEnumSwitch();
		}

		// Token: 0x06009E1D RID: 40477 RVA: 0x0032AABC File Offset: 0x00328CBC
		private void DoEnumSwitch()
		{
			if (this.enumVariable.IsNone)
			{
				return;
			}
			for (int i = 0; i < this.compareTo.Length; i++)
			{
				if (object.Equals(this.enumVariable.Value, this.compareTo[i].Value))
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x04008372 RID: 33650
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmEnum enumVariable;

		// Token: 0x04008373 RID: 33651
		[CompoundArray("Enum Switches", "Compare Enum Values", "Send")]
		[MatchFieldType("enumVariable")]
		public FsmEnum[] compareTo;

		// Token: 0x04008374 RID: 33652
		public FsmEvent[] sendEvent;

		// Token: 0x04008375 RID: 33653
		public bool everyFrame;
	}
}
