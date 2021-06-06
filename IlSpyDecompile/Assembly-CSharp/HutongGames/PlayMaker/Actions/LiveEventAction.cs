namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on what events are currently active.")]
	public class LiveEventAction : FsmStateAction
	{
		public FsmOwnerDefault m_EventObject;

		[RequiredField]
		[CompoundArray("Events", "Special Event", "FSM Event")]
		public SpecialEventType[] m_SpecialEvents;

		public FsmEvent[] m_FSMEvents;

		public override void Reset()
		{
			m_EventObject = null;
			m_FSMEvents = new FsmEvent[2];
			m_SpecialEvents = new SpecialEventType[2];
			m_SpecialEvents[0] = SpecialEventType.UNKNOWN;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			for (int i = 0; i < m_FSMEvents.Length; i++)
			{
				if (m_FSMEvents[i] != null && SpecialEventManager.Get().IsEventActive(m_SpecialEvents[i], activeIfDoesNotExist: false))
				{
					base.Fsm.Event(m_FSMEvents[i]);
					break;
				}
			}
			Finish();
		}
	}
}
