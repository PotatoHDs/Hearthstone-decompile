namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event after an optional delay. NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEvent2 : FsmStateAction
	{
		public FsmEventTarget eventTarget;

		[RequiredField]
		public FsmString sendEvent;

		[HasFloatSlider(0f, 10f)]
		public FsmFloat delay;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			sendEvent = null;
			delay = null;
		}

		public override void OnEnter()
		{
			if (delay.Value == 0f)
			{
				base.Fsm.Event(eventTarget, sendEvent.Value);
				Finish();
			}
			else
			{
				delayedEvent = new DelayedEvent(base.Fsm, eventTarget, sendEvent.Value, delay.Value);
			}
		}

		public override void OnUpdate()
		{
			delayedEvent.Update();
			if (delayedEvent.Finished)
			{
				Finish();
			}
		}
	}
}
