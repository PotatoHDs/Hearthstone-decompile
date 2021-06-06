using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA3 RID: 3491
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends the next event on the state each time the state is entered.")]
	public class SequenceEvent : FsmStateAction
	{
		// Token: 0x0600A52C RID: 42284 RVA: 0x003460D2 File Offset: 0x003442D2
		public override void Reset()
		{
			this.delay = null;
		}

		// Token: 0x0600A52D RID: 42285 RVA: 0x003460DC File Offset: 0x003442DC
		public override void OnEnter()
		{
			if (this.reset.Value)
			{
				this.eventIndex = 0;
				this.reset.Value = false;
			}
			int num = base.State.Transitions.Length;
			if (num > 0)
			{
				FsmEvent fsmEvent = base.State.Transitions[this.eventIndex].FsmEvent;
				if (this.delay.Value < 0.001f)
				{
					base.Fsm.Event(fsmEvent);
					base.Finish();
				}
				else
				{
					this.delayedEvent = base.Fsm.DelayedEvent(fsmEvent, this.delay.Value);
				}
				this.eventIndex++;
				if (this.eventIndex == num)
				{
					this.eventIndex = 0;
				}
			}
		}

		// Token: 0x0600A52E RID: 42286 RVA: 0x00346194 File Offset: 0x00344394
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x04008BC7 RID: 35783
		[HasFloatSlider(0f, 10f)]
		public FsmFloat delay;

		// Token: 0x04008BC8 RID: 35784
		[UIHint(UIHint.Variable)]
		[Tooltip("Assign a variable to control reset. Set it to True to reset the sequence. Value is set to False after resetting.")]
		public FsmBool reset;

		// Token: 0x04008BC9 RID: 35785
		private DelayedEvent delayedEvent;

		// Token: 0x04008BCA RID: 35786
		private int eventIndex;
	}
}
