namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Takes in a player and returns the gameobject for the deck mesh.")]
	public class GetDeckMesh : FsmStateAction
	{
		[Tooltip("Which player's deck are we querying the size of?")]
		public Player.Side m_PlayerSide;

		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Output GameObject.")]
		public FsmGameObject m_DeckMesh;

		public override void Reset()
		{
			m_PlayerSide = Player.Side.NEUTRAL;
			m_DeckMesh = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_DeckMesh == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store deck object!", this);
				Finish();
				return;
			}
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
			ZoneDeck deckZone = player.GetDeckZone();
			if (deckZone == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find deck for player {1}!", this, player);
				Finish();
			}
			else
			{
				m_DeckMesh.Value = deckZone.GetActiveThickness().gameObject;
				m_DeckMesh.Value.SetActive(value: true);
				Finish();
			}
		}
	}
}
