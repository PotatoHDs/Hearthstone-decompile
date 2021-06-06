namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Fires an event based on a screen category")]
	public class SendEventBasedOnScreenAction : FsmStateAction
	{
		public FsmEvent m_PhoneEvent;

		public FsmEvent m_MiniTabletEvent;

		public FsmEvent m_TabletEvent;

		public FsmEvent m_PCEvent;

		public override void Reset()
		{
			m_PhoneEvent = null;
			m_MiniTabletEvent = null;
			m_TabletEvent = null;
			m_PCEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			switch (PlatformSettings.Screen)
			{
			case ScreenCategory.Phone:
				base.Fsm.Event(m_PhoneEvent);
				break;
			case ScreenCategory.MiniTablet:
				base.Fsm.Event(m_MiniTabletEvent);
				break;
			case ScreenCategory.Tablet:
				base.Fsm.Event(m_TabletEvent);
				break;
			default:
				base.Fsm.Event(m_PCEvent);
				break;
			}
			Finish();
		}
	}
}
