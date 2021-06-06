using System.Collections;
using Assets;
using Hearthstone.Login;
using UnityEngine;

public class VictoryScreen : EndGameScreen
{
	public GamesWonIndicator m_gamesWonIndicator;

	public Transform m_goldenHeroEventBone;

	private bool m_showWinProgress;

	private bool m_showHeroRewardEvent;

	private bool m_hasParsedCompletedQuests;

	private bool m_heroRewardCardDefReady;

	private HeroRewardEvent m_heroRewardEvent;

	private DefLoader.DisposableCardDef m_heroRewardCardDef;

	private const string NO_HERO_REWARD = "none";

	protected override void Awake()
	{
		base.Awake();
		m_gamesWonIndicator.Hide();
		if (ShouldMakeUtilRequests())
		{
			if (GameMgr.Get().IsTutorial())
			{
				NetCache.Get().RegisterTutorialEndGameScreen(OnNetCacheReady);
			}
			else
			{
				NetCache.Get().RegisterScreenEndOfGame(OnNetCacheReady);
			}
		}
	}

	protected override void OnDestroy()
	{
		m_heroRewardCardDef?.Dispose();
		m_heroRewardCardDef = null;
		base.OnDestroy();
	}

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
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
		}
		if (!GameMgr.Get().IsTutorial() || GameMgr.Get().IsSpectator())
		{
			m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_PrevMode);
		}
		else if (GameUtils.AreAllTutorialsComplete())
		{
			LoadingScreen.Get().SetFadeColor(Color.white);
			m_hitbox.AddEventListener(UIEventType.RELEASE, ContinueButtonPress_FirstTimeHub);
		}
		else if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM && GameUtils.GetNextTutorial() == 0)
		{
			StartCoroutine(DemoMgr.Get().CompleteBlizzMuseumDemo());
		}
		else
		{
			m_hitbox.AddEventListener(UIEventType.RELEASE, base.ContinueButtonPress_TutorialProgress);
		}
	}

	protected override bool ShowHeroRewardEvent()
	{
		if (!JustEarnedHeroReward())
		{
			return false;
		}
		if (m_heroRewardEvent.gameObject.activeInHierarchy)
		{
			m_heroRewardEvent.Hide();
			m_showHeroRewardEvent = false;
			return false;
		}
		m_heroRewardAchievement.AckCurrentProgressAndRewardNotices();
		m_heroRewardEvent.SetRewardAchieve(m_heroRewardAchievement, delegate
		{
			ContinueEvents();
		});
		SetPlayingBlockingAnim(set: true);
		m_heroRewardEvent.RegisterAnimationDoneListener(NotifyOfGoldenHeroAnimComplete);
		m_twoScoop.StopAnimating();
		m_heroRewardEvent.Show();
		m_twoScoop.m_heroActor.transform.parent = m_heroRewardEvent.m_heroBone;
		m_twoScoop.m_heroActor.transform.localPosition = Vector3.zero;
		m_twoScoop.m_heroActor.transform.localScale = new Vector3(1.375f, 1.375f, 1.375f);
		return true;
	}

	protected override bool JustEarnedHeroReward()
	{
		if (m_hasParsedCompletedQuests)
		{
			return m_showHeroRewardEvent;
		}
		string heroRewardCardID = GetHeroRewardCardID();
		if (heroRewardCardID != "none")
		{
			CardPortraitQuality quality = new CardPortraitQuality(3, TAG_PREMIUM.GOLDEN);
			DefLoader.Get().LoadCardDef(heroRewardCardID, OnHeroRewardEventLoaded, null, quality);
			switch (m_heroRewardAchievement.AchieveType)
			{
			case Achieve.Type.GOLDHERO:
				AssetLoader.Get().InstantiatePrefab("Hero2GoldHero.prefab:a83a85837f828844caba16593ea3c1d0", OnHeroRewardEventLoaded);
				break;
			case Achieve.Type.PREMIUMHERO:
				AssetLoader.Get().InstantiatePrefab("Hero2PremiumHero.prefab:1115650b4bc229d49a8d45470424f5cd", OnHeroRewardEventLoaded);
				break;
			}
		}
		m_hasParsedCompletedQuests = true;
		m_showHeroRewardEvent = heroRewardCardID != "none";
		return m_showHeroRewardEvent;
	}

	protected override bool ShowHealUpDialog()
	{
		return TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_02"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_04"), TemporaryAccountManager.HealUpReason.WIN_GAME, userTriggered: false, OnHealUpDialogDismissed);
	}

	private void OnHealUpDialogDismissed()
	{
		ContinueEvents();
	}

	protected override bool ShowPushNotificationPrompt()
	{
		return PushNotificationManager.Get().ShowPushNotificationContext(OnPushNotificationDialogDismissed);
	}

	private void OnPushNotificationDialogDismissed()
	{
		ContinueEvents();
	}

	protected void ContinueButtonPress_FirstTimeHub(UIEvent e)
	{
		if (!HasShownScoops())
		{
			return;
		}
		HideTwoScoop();
		if (ShowNextReward())
		{
			SoundManager.Get().LoadAndPlay("VO_INNKEEPER_TUT_COMPLETE_05.prefab:c8d19a552e18c7c429946f62102c9460");
		}
		else if (!ShowNextCompletedQuest())
		{
			ContinueButtonPress_Common();
			m_hitbox.RemoveEventListener(UIEventType.RELEASE, ContinueButtonPress_FirstTimeHub);
			if (Network.ShouldBeConnectedToAurora())
			{
				BackToMode(SceneMgr.Mode.HUB);
				return;
			}
			NotificationManager.Get().CreateTutorialDialog("GLOBAL_MEDAL_REWARD_CONGRATULATIONS", "TUTORIAL_MOBILE_COMPLETE_CONGRATS", "GLOBAL_OKAY", UserPressedStartButton, new Vector2(0.5f, 0f), swapMaterial: true);
			m_hitbox.gameObject.SetActive(value: false);
			m_continueText.gameObject.SetActive(value: false);
		}
	}

	protected void UserPressedStartButton(UIEvent e)
	{
		HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
		BackToMode(SceneMgr.Mode.RESET);
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

	private void OnHeroRewardEventLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		StartCoroutine(WaitUntilTwoScoopLoaded(base.name, go));
	}

	public void NotifyOfGoldenHeroAnimComplete()
	{
		SetPlayingBlockingAnim(set: false);
		m_heroRewardEvent.RemoveAnimationDoneListener(NotifyOfGoldenHeroAnimComplete);
	}

	private IEnumerator WaitUntilTwoScoopLoaded(AssetReference assetRef, GameObject go)
	{
		while (m_twoScoop == null || !m_twoScoop.IsLoaded())
		{
			yield return null;
		}
		while (!m_heroRewardCardDefReady)
		{
			yield return null;
		}
		go.SetActive(value: false);
		TransformUtil.AttachAndPreserveLocalTransform(go.transform, m_goldenHeroEventBone);
		m_heroRewardEvent = go.GetComponent<HeroRewardEvent>();
		Texture portraitTexture = m_heroRewardCardDef.CardDef.GetPortraitTexture();
		m_heroRewardEvent.SetHeroBurnAwayTexture(portraitTexture);
		m_heroRewardEvent.SetVictoryTwoScoop((VictoryTwoScoop)m_twoScoop);
		SetHeroRewardEventReady(isReady: true);
	}

	protected override void InitGoldRewardUI()
	{
		m_showWinProgress = true;
		InitVictoryGoldRewardUI(m_gamesWonIndicator);
	}

	private string GetHeroRewardCardID()
	{
		int num = 0;
		foreach (Achievement completedQuest in m_completedQuests)
		{
			if (completedQuest.AchieveType == Achieve.Type.GOLDHERO || completedQuest.AchieveType == Achieve.Type.PREMIUMHERO)
			{
				m_heroRewardAchievement = completedQuest;
				foreach (RewardData reward in completedQuest.Rewards)
				{
					if (reward.RewardType == Reward.Type.CARD)
					{
						CardRewardData cardRewardData = reward as CardRewardData;
						CollectionManager.Get().AddCardReward(cardRewardData, markAsNew: false);
						m_completedQuests.RemoveAt(num);
						return cardRewardData.CardID;
					}
				}
			}
			num++;
		}
		return "none";
	}

	private void OnHeroRewardEventLoaded(string cardId, DefLoader.DisposableCardDef def, object userData)
	{
		m_heroRewardCardDef?.Dispose();
		m_heroRewardCardDef = def;
		m_heroRewardCardDefReady = true;
	}
}
