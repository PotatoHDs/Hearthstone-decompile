using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E1B RID: 3611
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of a String Variable.")]
	public class StringSwitch : FsmStateAction
	{
		// Token: 0x0600A738 RID: 42808 RVA: 0x0034BB70 File Offset: 0x00349D70
		public override void Reset()
		{
			this.stringVariable = null;
			this.compareTo = new FsmString[1];
			this.sendEvent = new FsmEvent[1];
			this.everyFrame = false;
		}

		// Token: 0x0600A739 RID: 42809 RVA: 0x0034BB98 File Offset: 0x00349D98
		public override void OnEnter()
		{
			this.DoStringSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A73A RID: 42810 RVA: 0x0034BBAE File Offset: 0x00349DAE
		public override void OnUpdate()
		{
			this.DoStringSwitch();
		}

		// Token: 0x0600A73B RID: 42811 RVA: 0x0034BBB8 File Offset: 0x00349DB8
		private void DoStringSwitch()
		{
			if (this.stringVariable.IsNone)
			{
				return;
			}
			for (int i = 0; i < this.compareTo.Length; i++)
			{
				if (this.stringVariable.Value == this.compareTo[i].Value)
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x04008DB8 RID: 36280
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008DB9 RID: 36281
		[CompoundArray("String Switches", "Compare String", "Send Event")]
		public FsmString[] compareTo;

		// Token: 0x04008DBA RID: 36282
		public FsmEvent[] sendEvent;

		// Token: 0x04008DBB RID: 36283
		public bool everyFrame;
	}
}
