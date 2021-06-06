public class BaconEndGameScreen : EndGameScreen
{
	public GamesWonIndicator m_gamesWonIndicator;

	private const int ShowWinProgressPlacement = 4;

	private const int ShowAppRatingPromptPlacement = 3;

	private bool m_showWinProgress;

	private int m_place = int.MaxValue;

	private int Place
	{
		get
		{
			if (m_place == int.MaxValue && GameState.Get() != null)
			{
				Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
				if (friendlySidePlayer != null && friendlySidePlayer.GetHero() != null)
				{
					m_place = friendlySidePlayer.GetHero().GetRealTimePlayerLeaderboardPlace();
				}
			}
			return m_place;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		m_gamesWonIndicator.Hide();
		if (ShouldMakeUtilRequests())
		{
			NetCache.Get().RegisterScreenEndOfGame(OnNetCacheReady);
		}
	}

	protected override void ShowStandardFlow()
	{
		base.ShowStandardFlow();
		m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_PrevMode);
	}

	protected override void OnTwoScoopShown()
	{
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().SuppressLoginTooltip(val: true);
		}
		if (m_showWinProgress)
		{
			m_gamesWonIndicator.Show();
		}
	}

	protected override void OnTwoScoopHidden()
	{
		if (m_showWinProgress)
		{
			m_gamesWonIndicator.Hide();
		}
	}

	protected override void InitGoldRewardUI()
	{
		m_showWinProgress = Place <= 4;
		InitVictoryGoldRewardUI(m_gamesWonIndicator);
	}

	protected override bool ShowAppRatingPrompt()
	{
		if (Place <= 3)
		{
			return MobileCallbackManager.RequestAppReview();
		}
		return false;
	}
}
