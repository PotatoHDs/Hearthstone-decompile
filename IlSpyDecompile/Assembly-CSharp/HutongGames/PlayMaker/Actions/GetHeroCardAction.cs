namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Use this action to get the hero card for either side's hero.")]
	public class GetHeroCardAction : FsmStateAction
	{
		[Tooltip("Which player's hero are we querying for?")]
		public Player.Side m_PlayerSide;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output variable.")]
		public FsmGameObject m_HeroCardGameObject;

		public override void Reset()
		{
			m_HeroCardGameObject = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_HeroCardGameObject == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No variable hooked up to store hero card!", this);
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
				global::Log.Gameplay.PrintError("{0}.OnEnter() - No hero exists for player side {1}!", this, m_PlayerSide);
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
			Card heroCard = player.GetHeroCard();
			if (heroCard == null)
			{
				global::Log.Gameplay.PrintError("{0}.OnEnter() - Unable to find hero card for side {1}!", this, m_PlayerSide);
				Finish();
			}
			else
			{
				m_HeroCardGameObject.Value = heroCard.gameObject;
				Finish();
			}
		}
	}
}
