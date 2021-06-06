using System;
using System.Collections;
using Assets;
using Hearthstone.Login;
using UnityEngine;

// Token: 0x020002D2 RID: 722
public class VictoryScreen : EndGameScreen
{
	// Token: 0x060025F8 RID: 9720 RVA: 0x000BEC6C File Offset: 0x000BCE6C
	protected override void Awake()
	{
		base.Awake();
		this.m_gamesWonIndicator.Hide();
		if (base.ShouldMakeUtilRequests())
		{
			if (GameMgr.Get().IsTutorial())
			{
				NetCache.Get().RegisterTutorialEndGameScreen(new NetCache.NetCacheCallback(this.OnNetCacheReady));
				return;
			}
			NetCache.Get().RegisterScreenEndOfGame(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		}
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x000BECCD File Offset: 0x000BCECD
	protected override void OnDestroy()
	{
		DefLoader.DisposableCardDef heroRewardCardDef = this.m_heroRewardCardDef;
		if (heroRewardCardDef != null)
		{
			heroRewardCardDef.Dispose();
		}
		this.m_heroRewardCardDef = null;
		base.OnDestroy();
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x000BECF0 File Offset: 0x000BCEF0
	protected override void ShowStandardFlow()
	{
		base.ShowStandardFlow();
		if (EmoteHandler.Get() != null)
		{
			EmoteHandler.Get().HideEmotes();
		}
		if (TargetReticleManager.Get() != null)
		{
			TargetReticleManager.Get().DestroyEnemyTargetArrow();
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
		}
		if (!GameMgr.Get().IsTutorial() || GameMgr.Get().IsSpectator())
		{
			this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_PrevMode));
			return;
		}
		if (GameUtils.AreAllTutorialsComplete())
		{
			LoadingScreen.Get().SetFadeColor(Color.white);
			this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_FirstTimeHub));
			return;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM && GameUtils.GetNextTutorial() == 0)
		{
			base.StartCoroutine(DemoMgr.Get().CompleteBlizzMuseumDemo());
			return;
		}
		this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(base.ContinueButtonPress_TutorialProgress));
	}

	// Token: 0x060025FB RID: 9723 RVA: 0x000BEDE0 File Offset: 0x000BCFE0
	protected override bool ShowHeroRewardEvent()
	{
		if (!this.JustEarnedHeroReward())
		{
			return false;
		}
		if (this.m_heroRewardEvent.gameObject.activeInHierarchy)
		{
			this.m_heroRewardEvent.Hide();
			this.m_showHeroRewardEvent = false;
			return false;
		}
		this.m_heroRewardAchievement.AckCurrentProgressAndRewardNotices();
		this.m_heroRewardEvent.SetRewardAchieve(this.m_heroRewardAchievement, delegate(object userData)
		{
			base.ContinueEvents();
		});
		base.SetPlayingBlockingAnim(true);
		this.m_heroRewardEvent.RegisterAnimationDoneListener(new HeroRewardEvent.AnimationDoneListener(this.NotifyOfGoldenHeroAnimComplete));
		this.m_twoScoop.StopAnimating();
		this.m_heroRewardEvent.Show();
		this.m_twoScoop.m_heroActor.transform.parent = this.m_heroRewardEvent.m_heroBone;
		this.m_twoScoop.m_heroActor.transform.localPosition = Vector3.zero;
		this.m_twoScoop.m_heroActor.transform.localScale = new Vector3(1.375f, 1.375f, 1.375f);
		return true;
	}

	// Token: 0x060025FC RID: 9724 RVA: 0x000BEEE0 File Offset: 0x000BD0E0
	protected override bool JustEarnedHeroReward()
	{
		if (this.m_hasParsedCompletedQuests)
		{
			return this.m_showHeroRewardEvent;
		}
		string heroRewardCardID = this.GetHeroRewardCardID();
		if (heroRewardCardID != "none")
		{
			CardPortraitQuality quality = new CardPortraitQuality(3, TAG_PREMIUM.GOLDEN);
			DefLoader.Get().LoadCardDef(heroRewardCardID, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnHeroRewardEventLoaded), null, quality);
			Achieve.Type achieveType = this.m_heroRewardAchievement.AchieveType;
			if (achieveType != Achieve.Type.GOLDHERO)
			{
				if (achieveType == Achieve.Type.PREMIUMHERO)
				{
					AssetLoader.Get().InstantiatePrefab("Hero2PremiumHero.prefab:1115650b4bc229d49a8d45470424f5cd", new PrefabCallback<GameObject>(this.OnHeroRewardEventLoaded), null, AssetLoadingOptions.None);
				}
			}
			else
			{
				AssetLoader.Get().InstantiatePrefab("Hero2GoldHero.prefab:a83a85837f828844caba16593ea3c1d0", new PrefabCallback<GameObject>(this.OnHeroRewardEventLoaded), null, AssetLoadingOptions.None);
			}
		}
		this.m_hasParsedCompletedQuests = true;
		this.m_showHeroRewardEvent = (heroRewardCardID != "none");
		return this.m_showHeroRewardEvent;
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x000BEFB1 File Offset: 0x000BD1B1
	protected override bool ShowHealUpDialog()
	{
		return TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_02"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_04"), TemporaryAccountManager.HealUpReason.WIN_GAME, false, new TemporaryAccountManager.OnHealUpDialogDismissed(this.OnHealUpDialogDismissed));
	}

	// Token: 0x060025FE RID: 9726 RVA: 0x000BC029 File Offset: 0x000BA229
	private void OnHealUpDialogDismissed()
	{
		base.ContinueEvents();
	}

	// Token: 0x060025FF RID: 9727 RVA: 0x000BEFDF File Offset: 0x000BD1DF
	protected override bool ShowPushNotificationPrompt()
	{
		return PushNotificationManager.Get().ShowPushNotificationContext(new Action(this.OnPushNotificationDialogDismissed));
	}

	// Token: 0x06002600 RID: 9728 RVA: 0x000BC029 File Offset: 0x000BA229
	private void OnPushNotificationDialogDismissed()
	{
		base.ContinueEvents();
	}

	// Token: 0x06002601 RID: 9729 RVA: 0x000BEFF8 File Offset: 0x000BD1F8
	protected void ContinueButtonPress_FirstTimeHub(UIEvent e)
	{
		if (!base.HasShownScoops())
		{
			return;
		}
		base.HideTwoScoop();
		if (base.ShowNextReward())
		{
			SoundManager.Get().LoadAndPlay("VO_INNKEEPER_TUT_COMPLETE_05.prefab:c8d19a552e18c7c429946f62102c9460");
			return;
		}
		if (base.ShowNextCompletedQuest())
		{
			return;
		}
		base.ContinueButtonPress_Common();
		this.m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_FirstTimeHub));
		if (Network.ShouldBeConnectedToAurora())
		{
			base.BackToMode(SceneMgr.Mode.HUB);
			return;
		}
		NotificationManager.Get().CreateTutorialDialog("GLOBAL_MEDAL_REWARD_CONGRATULATIONS", "TUTORIAL_MOBILE_COMPLETE_CONGRATS", "GLOBAL_OKAY", new UIEvent.Handler(this.UserPressedStartButton), new Vector2(0.5f, 0f), true);
		this.m_hitbox.gameObject.SetActive(false);
		this.m_continueText.gameObject.SetActive(false);
	}

	// Token: 0x06002602 RID: 9730 RVA: 0x000BF0C0 File Offset: 0x000BD2C0
	protected void UserPressedStartButton(UIEvent e)
	{
		ILoginService loginService = HearthstoneServices.Get<ILoginService>();
		if (loginService != null)
		{
			loginService.ClearAuthentication();
		}
		base.BackToMode(SceneMgr.Mode.RESET);
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x000BF0DA File Offset: 0x000BD2DA
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

	// Token: 0x06002604 RID: 9732 RVA: 0x000BF107 File Offset: 0x000BD307
	protected override void OnTwoScoopHidden()
	{
		if (this.m_showWinProgress)
		{
			this.m_gamesWonIndicator.Hide();
		}
	}

	// Token: 0x06002605 RID: 9733 RVA: 0x000BF11C File Offset: 0x000BD31C
	private void OnHeroRewardEventLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		base.StartCoroutine(this.WaitUntilTwoScoopLoaded(base.name, go));
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x000BF137 File Offset: 0x000BD337
	public void NotifyOfGoldenHeroAnimComplete()
	{
		base.SetPlayingBlockingAnim(false);
		this.m_heroRewardEvent.RemoveAnimationDoneListener(new HeroRewardEvent.AnimationDoneListener(this.NotifyOfGoldenHeroAnimComplete));
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x000BF157 File Offset: 0x000BD357
	private IEnumerator WaitUntilTwoScoopLoaded(AssetReference assetRef, GameObject go)
	{
		while (this.m_twoScoop == null || !this.m_twoScoop.IsLoaded())
		{
			yield return null;
		}
		while (!this.m_heroRewardCardDefReady)
		{
			yield return null;
		}
		go.SetActive(false);
		TransformUtil.AttachAndPreserveLocalTransform(go.transform, this.m_goldenHeroEventBone);
		this.m_heroRewardEvent = go.GetComponent<HeroRewardEvent>();
		Texture portraitTexture = this.m_heroRewardCardDef.CardDef.GetPortraitTexture();
		this.m_heroRewardEvent.SetHeroBurnAwayTexture(portraitTexture);
		this.m_heroRewardEvent.SetVictoryTwoScoop((VictoryTwoScoop)this.m_twoScoop);
		base.SetHeroRewardEventReady(true);
		yield break;
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x000BF16D File Offset: 0x000BD36D
	protected override void InitGoldRewardUI()
	{
		this.m_showWinProgress = true;
		this.InitVictoryGoldRewardUI(this.m_gamesWonIndicator);
	}

	// Token: 0x06002609 RID: 9737 RVA: 0x000BF184 File Offset: 0x000BD384
	private string GetHeroRewardCardID()
	{
		int num = 0;
		foreach (global::Achievement achievement in this.m_completedQuests)
		{
			if (achievement.AchieveType == Achieve.Type.GOLDHERO || achievement.AchieveType == Achieve.Type.PREMIUMHERO)
			{
				this.m_heroRewardAchievement = achievement;
				foreach (RewardData rewardData in achievement.Rewards)
				{
					if (rewardData.RewardType == Reward.Type.CARD)
					{
						CardRewardData cardRewardData = rewardData as CardRewardData;
						CollectionManager.Get().AddCardReward(cardRewardData, false);
						this.m_completedQuests.RemoveAt(num);
						return cardRewardData.CardID;
					}
				}
			}
			num++;
		}
		return "none";
	}

	// Token: 0x0600260A RID: 9738 RVA: 0x000BF274 File Offset: 0x000BD474
	private void OnHeroRewardEventLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		DefLoader.DisposableCardDef heroRewardCardDef = this.m_heroRewardCardDef;
		if (heroRewardCardDef != null)
		{
			heroRewardCardDef.Dispose();
		}
		this.m_heroRewardCardDef = def;
		this.m_heroRewardCardDefReady = true;
	}

	// Token: 0x0400154E RID: 5454
	public GamesWonIndicator m_gamesWonIndicator;

	// Token: 0x0400154F RID: 5455
	public Transform m_goldenHeroEventBone;

	// Token: 0x04001550 RID: 5456
	private bool m_showWinProgress;

	// Token: 0x04001551 RID: 5457
	private bool m_showHeroRewardEvent;

	// Token: 0x04001552 RID: 5458
	private bool m_hasParsedCompletedQuests;

	// Token: 0x04001553 RID: 5459
	private bool m_heroRewardCardDefReady;

	// Token: 0x04001554 RID: 5460
	private HeroRewardEvent m_heroRewardEvent;

	// Token: 0x04001555 RID: 5461
	private DefLoader.DisposableCardDef m_heroRewardCardDef;

	// Token: 0x04001556 RID: 5462
	private const string NO_HERO_REWARD = "none";
}
