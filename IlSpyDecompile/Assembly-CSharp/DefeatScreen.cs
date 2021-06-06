public class DefeatScreen : EndGameScreen
{
	protected override void Awake()
	{
		base.Awake();
		if (ShouldMakeUtilRequests())
		{
			NetCache.Get().RegisterScreenEndOfGame(OnNetCacheReady);
		}
	}

	protected override void ShowStandardFlow()
	{
		base.ShowStandardFlow();
		if (GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator())
		{
			m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_TutorialProgress);
		}
		else
		{
			m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_PrevMode);
		}
		if (EmoteHandler.Get() != null)
		{
			EmoteHandler.Get().HideEmotes();
		}
		if (TargetReticleManager.Get() != null)
		{
			TargetReticleManager.Get().DestroyEnemyTargetArrow();
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
		}
	}

	protected override void InitGoldRewardUI()
	{
		string friendlyChallengeRewardText = EndGameScreen.GetFriendlyChallengeRewardText();
		if (!string.IsNullOrEmpty(friendlyChallengeRewardText))
		{
			m_noGoldRewardText.gameObject.SetActive(value: true);
			m_noGoldRewardText.Text = friendlyChallengeRewardText;
		}
	}
}
