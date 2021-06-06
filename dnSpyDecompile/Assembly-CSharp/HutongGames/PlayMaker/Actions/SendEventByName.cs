using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9F RID: 3487
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event by name after an optional delay. NOTE: Use this over Send Event if you store events as string variables.")]
	public class SendEventByName : FsmStateAction
	{
		// Token: 0x0600A51C RID: 42268 RVA: 0x0034595F File Offset: 0x00343B5F
		public override void Reset()
		{
			this.eventTarget = null;
			this.sendEvent = null;
			this.delay = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A51D RID: 42269 RVA: 0x00345980 File Offset: 0x00343B80
		public override void OnEnter()
		{
			if (this.delay.Value < 0.001f)
			{
				base.Fsm.Event(this.eventTarget, this.sendEvent.Value);
				if (!this.everyFrame)
				{
					base.Finish();
					return;
				}
			}
			else
			{
				this.delayedEvent = base.Fsm.DelayedEvent(this.eventTarget, FsmEvent.GetFsmEvent(this.sendEvent.Value), this.delay.Value);
			}
		}

		// Token: 0x0600A51E RID: 42270 RVA: 0x003459FC File Offset: 0x00343BFC
		public override void OnUpdate()
		{
			if (!this.everyFrame)
			{
				if (DelayedEvent.WasSent(this.delayedEvent))
				{
					base.Finish();
					return;
				}
			}
			else
			{
				base.Fsm.Event(this.eventTarget, this.sendEvent.Value);
			}
		}

		// Token: 0x04008BB3 RID: 35763
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008BB4 RID: 35764
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString sendEvent;

		// Token: 0x04008BB5 RID: 35765
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		// Token: 0x04008BB6 RID: 35766
		[Tooltip("Repeat every frame. Rarely needed, but can be useful when sending events to other FSMs.")]
		public bool everyFrame;

		// Token: 0x04008BB7 RID: 35767
		private DelayedEvent delayedEvent;
	}
}
