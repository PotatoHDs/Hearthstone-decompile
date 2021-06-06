namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sends Events based on whether or not an option is enabled by the player.")]
	public class IsOptionEnabledAction : FsmStateAction
	{
		[Tooltip("The option to check.")]
		public Option m_Option;

		[Tooltip("Event sent if the option is on.")]
		public FsmEvent m_TrueEvent;

		[Tooltip("Event sent if the option is off.")]
		public FsmEvent m_FalseEvent;

		[Tooltip("Check if the option is enabled every frame.")]
		public bool m_EveryFrame;

		public override void Reset()
		{
			m_Option = Option.INVALID;
			m_TrueEvent = null;
			m_FalseEvent = null;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			FireEvents();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			FireEvents();
		}

		public override string ErrorCheck()
		{
			if (!FsmEvent.IsNullOrEmpty(m_TrueEvent))
			{
				return string.Empty;
			}
			if (!FsmEvent.IsNullOrEmpty(m_FalseEvent))
			{
				return string.Empty;
			}
			return "Action sends no events!";
		}

		private void FireEvents()
		{
			if (Options.Get().GetBool(m_Option))
			{
				base.Fsm.Event(m_TrueEvent);
			}
			else
			{
				base.Fsm.Event(m_FalseEvent);
			}
		}
	}
}
