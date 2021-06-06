namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Event based on current platform")]
	public class PlatformEventAction : FsmStateAction
	{
		public FsmEvent m_DefaultEvent;

		public FsmEvent m_PhoneEvent;

		public override void Reset()
		{
			m_PhoneEvent = null;
			m_DefaultEvent = null;
		}

		public override void OnEnter()
		{
			if ((bool)UniversalInputManager.UsePhoneUI && m_PhoneEvent != null)
			{
				base.Fsm.Event(m_PhoneEvent);
			}
			else
			{
				base.Fsm.Event(m_DefaultEvent);
			}
		}

		public override string ErrorCheck()
		{
			return "";
		}
	}
}
