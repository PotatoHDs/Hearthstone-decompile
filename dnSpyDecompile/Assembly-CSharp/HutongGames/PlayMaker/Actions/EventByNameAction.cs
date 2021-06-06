using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F31 RID: 3889
	[ActionCategory("Pegasus")]
	[Tooltip("Trigger Event by name after an optional delay. NOTE: Use this over Send Event if you store events as string variables.")]
	public class EventByNameAction : FsmStateAction
	{
		// Token: 0x0600AC52 RID: 44114 RVA: 0x0035C745 File Offset: 0x0035A945
		public override void Reset()
		{
			this.sendEvent = null;
			this.delay = null;
			this.fallbackEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600AC53 RID: 44115 RVA: 0x0035C764 File Offset: 0x0035A964
		public override void OnEnter()
		{
			string text = "FINISHED";
			if (this.fallbackEvent != null)
			{
				text = this.fallbackEvent.Value;
			}
			FsmState state = base.State;
			if (state != null)
			{
				FsmTransition[] transitions = state.Transitions;
				for (int i = 0; i < transitions.Length; i++)
				{
					if (transitions[i].EventName == this.sendEvent.Value)
					{
						text = this.sendEvent.Value;
					}
				}
			}
			if (this.delay.Value < 0.001f)
			{
				base.Fsm.Event(text);
				base.Finish();
				return;
			}
			this.delayedEvent = base.Fsm.DelayedEvent(FsmEvent.GetFsmEvent(text), this.delay.Value);
		}

		// Token: 0x0600AC54 RID: 44116 RVA: 0x0035C817 File Offset: 0x0035AA17
		public override void OnUpdate()
		{
			if (!this.everyFrame && DelayedEvent.WasSent(this.delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x0400932D RID: 37677
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString sendEvent;

		// Token: 0x0400932E RID: 37678
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		// Token: 0x0400932F RID: 37679
		[RequiredField]
		[Tooltip("The event to send if the send event is not found. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString fallbackEvent;

		// Token: 0x04009330 RID: 37680
		[Tooltip("Repeat every frame. Rarely needed.")]
		public bool everyFrame;

		// Token: 0x04009331 RID: 37681
		private DelayedEvent delayedEvent;
	}
}
