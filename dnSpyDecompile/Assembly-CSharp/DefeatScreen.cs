using System;

// Token: 0x020002C5 RID: 709
public class DefeatScreen : EndGameScreen
{
	// Token: 0x06002534 RID: 9524 RVA: 0x000BB145 File Offset: 0x000B9345
	protected override void Awake()
	{
		base.Awake();
		if (base.ShouldMakeUtilRequests())
		{
			NetCache.Get().RegisterScreenEndOfGame(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		}
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x000BB16C File Offset: 0x000B936C
	protected override void ShowStandardFlow()
	{
		base.ShowStandardFlow();
		if (GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator())
		{
			this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_TutorialProgress));
		}
		else
		{
			this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_PrevMode));
		}
		if (EmoteHandler.Get() != null)
		{
			EmoteHandler.Get().HideEmotes();
		}
		if (TargetReticleManager.Get() != null)
		{
			TargetReticleManager.Get().DestroyEnemyTargetArrow();
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
		}
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x000BB204 File Offset: 0x000B9404
	protected override void InitGoldRewardUI()
	{
		string friendlyChallengeRewardText = EndGameScreen.GetFriendlyChallengeRewardText();
		if (!string.IsNullOrEmpty(friendlyChallengeRewardText))
		{
			this.m_noGoldRewardText.gameObject.SetActive(true);
			this.m_noGoldRewardText.Text = friendlyChallengeRewardText;
		}
	}
}
