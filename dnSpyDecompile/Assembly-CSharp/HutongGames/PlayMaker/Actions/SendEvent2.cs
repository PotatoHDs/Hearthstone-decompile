using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9E RID: 3486
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event after an optional delay. NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEvent2 : FsmStateAction
	{
		// Token: 0x0600A518 RID: 42264 RVA: 0x003458BE File Offset: 0x00343ABE
		public override void Reset()
		{
			this.sendEvent = null;
			this.delay = null;
		}

		// Token: 0x0600A519 RID: 42265 RVA: 0x003458D0 File Offset: 0x00343AD0
		public override void OnEnter()
		{
			if (this.delay.Value == 0f)
			{
				base.Fsm.Event(this.eventTarget, this.sendEvent.Value);
				base.Finish();
				return;
			}
			this.delayedEvent = new DelayedEvent(base.Fsm, this.eventTarget, this.sendEvent.Value, this.delay.Value);
		}

		// Token: 0x0600A51A RID: 42266 RVA: 0x0034593F File Offset: 0x00343B3F
		public override void OnUpdate()
		{
			this.delayedEvent.Update();
			if (this.delayedEvent.Finished)
			{
				base.Finish();
			}
		}

		// Token: 0x04008BAF RID: 35759
		public FsmEventTarget eventTarget;

		// Token: 0x04008BB0 RID: 35760
		[RequiredField]
		public FsmString sendEvent;

		// Token: 0x04008BB1 RID: 35761
		[HasFloatSlider(0f, 10f)]
		public FsmFloat delay;

		// Token: 0x04008BB2 RID: 35762
		private DelayedEvent delayedEvent;
	}
}
