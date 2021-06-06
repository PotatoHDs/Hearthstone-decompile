namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Trigger Event by name after an optional delay. NOTE: Use this over Send Event if you store events as string variables.")]
	public class EventByNameAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString sendEvent;

		[HasFloatSlider(0f, 10f)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[RequiredField]
		[Tooltip("The event to send if the send event is not found. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmString fallbackEvent;

		[Tooltip("Repeat every frame. Rarely needed.")]
		public bool everyFrame;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			sendEvent = null;
			delay = null;
			fallbackEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			string text = "FINISHED";
			if (fallbackEvent != null)
			{
				text = fallbackEvent.Value;
			}
			FsmState state = base.State;
			if (state != null)
			{
				FsmTransition[] transitions = state.Transitions;
				for (int i = 0; i < transitions.Length; i++)
				{
					if (transitions[i].EventName == sendEvent.Value)
					{
						text = sendEvent.Value;
					}
				}
			}
			if (delay.Value < 0.001f)
			{
				base.Fsm.Event(text);
				Finish();
			}
			else
			{
				delayedEvent = base.Fsm.DelayedEvent(FsmEvent.GetFsmEvent(text), delay.Value);
			}
		}

		public override void OnUpdate()
		{
			if (!everyFrame && DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}
		}
	}
}
