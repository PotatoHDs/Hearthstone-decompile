namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Set the visibility of a player's deck.")]
	public class SetDeckVisibilityAction : FsmStateAction
	{
		[Tooltip("Which player's deck are we changing?")]
		public Player.Side m_PlayerSide;

		[Tooltip("Should the Deck be set to visible or invisible?")]
		public FsmBool m_Visible;

		[Tooltip("Resets to the initial visibility once\nit leaves the state")]
		public bool m_ResetOnExit;

		protected bool m_initialVisibility;

		protected ZoneDeck m_deck;

		public override void Reset()
		{
			m_PlayerSide = Player.Side.NEUTRAL;
			m_Visible = false;
			m_ResetOnExit = false;
			m_deck = null;
			m_initialVisibility = false;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (GameState.Get() == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - GameState is null!", this);
				Finish();
				return;
			}
			if (m_PlayerSide == Player.Side.NEUTRAL)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No deck exists for player side {1}!", this, m_PlayerSide);
				Finish();
				return;
			}
			Player player = ((m_PlayerSide == Player.Side.FRIENDLY) ? GameState.Get().GetFriendlySidePlayer() : GameState.Get().GetOpposingSidePlayer());
			if (player == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find player for side {1}!", this, m_PlayerSide);
				Finish();
				return;
			}
			m_deck = player.GetDeckZone();
			if (m_deck == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", this, player);
				Finish();
			}
			else
			{
				m_initialVisibility = m_deck.GetVisibility();
				m_deck.SetVisibility(m_Visible.Value);
				Finish();
			}
		}

		public override void OnExit()
		{
			if (m_ResetOnExit && !(m_deck == null))
			{
				m_deck.SetVisibility(m_initialVisibility);
			}
		}
	}
}
