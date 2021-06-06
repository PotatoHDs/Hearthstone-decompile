namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Fires an event based on a supplied Int")]
	public class SendEventBasedOnIntAction : FsmStateAction
	{
		public class ValueRangeEvent
		{
			public ValueRange m_range;

			public FsmEvent m_event;
		}

		public ValueRangeEvent[] m_rangeEventArray;

		[RequiredField]
		public FsmInt m_Int;

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_Int == null)
			{
				global::Log.All.PrintError("{0}.OnEnter() - No Int supplied!", this);
				Finish();
				return;
			}
			FsmEvent fsmEvent = DetermineEventToSend(m_Int.Value);
			if (fsmEvent != null)
			{
				base.Fsm.Event(fsmEvent);
			}
			else
			{
				global::Log.All.PrintError("{0}.OnEnter() - FAILED to find any event that could be sent, did you set up ranges?", this);
			}
			Finish();
		}

		private FsmEvent DetermineEventToSend(int tagValue)
		{
			return SpellUtils.GetAppropriateElementAccordingToRanges(m_rangeEventArray, (ValueRangeEvent x) => x.m_range, tagValue)?.m_event;
		}
	}
}
