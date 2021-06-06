using System;

// Token: 0x020002C3 RID: 707
public class BaconEndGameScreen : EndGameScreen
{
	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x06002527 RID: 9511 RVA: 0x000BAF20 File Offset: 0x000B9120
	private int Place
	{
		get
		{
			if (this.m_place == 2147483647 && GameState.Get() != null)
			{
				Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
				if (friendlySidePlayer != null && friendlySidePlayer.GetHero() != null)
				{
					this.m_place = friendlySidePlayer.GetHero().GetRealTimePlayerLeaderboardPlace();
				}
			}
			return this.m_place;
		}
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x000BAF6E File Offset: 0x000B916E
	protected override void Awake()
	{
		base.Awake();
		this.m_gamesWonIndicator.Hide();
		if (base.ShouldMakeUtilRequests())
		{
			NetCache.Get().RegisterScreenEndOfGame(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		}
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x000BAFA0 File Offset: 0x000B91A0
	protected override void ShowStandardFlow()
	{
		base.ShowStandardFlow();
		this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_PrevMode));
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x000BAFC1 File Offset: 0x000B91C1
	protected override void OnTwoScoopShown()
	{
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().SuppressLoginTooltip(true);
		}
		if (this.m_showWinProgress)
		{
			this.m_gamesWonIndicator.Show();
		}
	}

	// Token: 0x0600252B RID: 9515 RVA: 0x000BAFEE File Offset: 0x000B91EE
	protected override void OnTwoScoopHidden()
	{
		if (this.m_showWinProgress)
		{
			this.m_gamesWonIndicator.Hide();
		}
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x000BB003 File Offset: 0x000B9203
	protected override void InitGoldRewardUI()
	{
		this.m_showWinProgress = (this.Place <= 4);
		this.InitVictoryGoldRewardUI(this.m_gamesWonIndicator);
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x000BB023 File Offset: 0x000B9223
	protected override bool ShowAppRatingPrompt()
	{
		return this.Place <= 3 && MobileCallbackManager.RequestAppReview(false);
	}

	// Token: 0x040014BC RID: 5308
	public GamesWonIndicator m_gamesWonIndicator;

	// Token: 0x040014BD RID: 5309
	private const int ShowWinProgressPlacement = 4;

	// Token: 0x040014BE RID: 5310
	private const int ShowAppRatingPromptPlacement = 3;

	// Token: 0x040014BF RID: 5311
	private bool m_showWinProgress;

	// Token: 0x040014C0 RID: 5312
	private int m_place = int.MaxValue;
}
