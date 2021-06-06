using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusShared;
using SpectatorProto;
using UnityEngine;

// Token: 0x020002C7 RID: 711
[CustomEditClass]
public class EndGameScreen : MonoBehaviour
{
	// Token: 0x06002546 RID: 9542 RVA: 0x000BBCF0 File Offset: 0x000B9EF0
	protected virtual void Awake()
	{
		EndGameScreen.s_instance = this;
		base.StartCoroutine(this.WaitForAchieveManager());
		this.ProcessPreviousAchievements();
		AchieveManager.Get().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated), null);
		AchieveManager.Get().CheckPlayedNearbyPlayerOnSubnet();
		this.m_shouldShowRankChange = (!GameMgr.Get().IsSpectator() && GameMgr.Get().IsPlay() && Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE));
		this.m_hitbox.gameObject.SetActive(false);
		string key = "GLOBAL_CLICK_TO_CONTINUE";
		if (UniversalInputManager.Get().IsTouchMode())
		{
			key = "GLOBAL_CLICK_TO_CONTINUE_TOUCH";
		}
		this.m_continueText.Text = GameStrings.Get(key);
		this.m_continueText.gameObject.SetActive(false);
		this.m_noGoldRewardText.gameObject.SetActive(false);
		PegUI.Get().AddInputCamera(CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects));
		SceneUtils.SetLayer(this.m_hitbox.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(this.m_continueText.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (!Network.ShouldBeConnectedToAurora())
		{
			this.UpdateRewards();
		}
		this.m_genericRewardChestNoticeIdsReady = GenericRewardChestNoticeManager.Get().GetReadyGenericRewardChestNotices();
		GenericRewardChestNoticeManager.Get().RegisterRewardsUpdatedListener(new GenericRewardChestNoticeManager.GenericRewardUpdatedCallback(this.OnGenericRewardUpdated), null);
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x000BBE30 File Offset: 0x000BA030
	protected virtual void OnDestroy()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheMedalInfo), new Action(this.OnMedalInfoUpdate));
		}
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects));
		}
		if (EndGameScreen.OnTwoScoopsShown != null)
		{
			EndGameScreen.OnTwoScoopsShown(false, this.m_twoScoop);
		}
		if (AchieveManager.Get() != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated));
		}
		if (GenericRewardChestNoticeManager.Get() != null)
		{
			GenericRewardChestNoticeManager.Get().RemoveRewardsUpdatedListener(new GenericRewardChestNoticeManager.GenericRewardUpdatedCallback(this.OnGenericRewardUpdated));
		}
		EndGameScreen.s_instance = null;
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x000BBEDC File Offset: 0x000BA0DC
	public static EndGameScreen Get()
	{
		return EndGameScreen.s_instance;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x000BBEE4 File Offset: 0x000BA0E4
	public virtual void Show()
	{
		if (GameState.Get() != null && GameState.Get().WasRestartRequested())
		{
			return;
		}
		this.m_shown = true;
		this.m_endGameScreenStartTime = Time.time;
		Network.Get().DisconnectFromGameServer();
		InputManager.Get().DisableInput();
		this.m_hitbox.gameObject.SetActive(true);
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
		if (GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null)
		{
			GameState.Get().GetFriendlySidePlayer().GetHandZone().UpdateLayout(null);
		}
		this.ShowScoreScreen();
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x000BBF97 File Offset: 0x000BA197
	public void SetPlayingBlockingAnim(bool set)
	{
		this.m_playingBlockingAnim = set;
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x000BBFA0 File Offset: 0x000BA1A0
	public bool IsPlayingBlockingAnim()
	{
		return this.m_playingBlockingAnim;
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x000BBFA8 File Offset: 0x000BA1A8
	public bool IsScoreScreenShown()
	{
		return this.m_showingScoreScreen;
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x000BBFB0 File Offset: 0x000BA1B0
	private void ShowTutorialProgress()
	{
		this.HideTwoScoop();
		base.StartCoroutine(this.LoadTutorialProgress());
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x000BBFC5 File Offset: 0x000BA1C5
	private IEnumerator LoadTutorialProgress()
	{
		yield return new WaitForSeconds(0.25f);
		AssetLoader.Get().InstantiatePrefab("TutorialProgressScreen.prefab:a78bac9caa971494ea8fac23dc1a9bd8", new PrefabCallback<GameObject>(this.OnTutorialProgressScreenCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
		yield break;
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x000BBFD4 File Offset: 0x000BA1D4
	private void OnTutorialProgressScreenCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.parent = base.transform;
		go.GetComponent<TutorialProgressScreen>().StartTutorialProgress();
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x000BBFF2 File Offset: 0x000BA1F2
	protected void ContinueButtonPress_Common()
	{
		LoadingScreen.Get().AddTransitionObject(this);
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x000BBFFF File Offset: 0x000BA1FF
	protected void ContinueButtonPress_ProceedToError(UIEvent e)
	{
		if (this.IsPlayingBlockingAnim())
		{
			return;
		}
		this.HideScoreScreen();
		this.m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_ProceedToError));
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x000BC029 File Offset: 0x000BA229
	protected void ContinueButtonPress_PrevMode(UIEvent e)
	{
		this.ContinueEvents();
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x000BC034 File Offset: 0x000BA234
	public bool ContinueEvents()
	{
		if (this.ContinueDefaultEvents())
		{
			return true;
		}
		if (this.m_twoScoop == null)
		{
			return false;
		}
		PlayMakerFSM component = this.m_twoScoop.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Death");
		}
		this.ContinueButtonPress_Common();
		this.m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_PrevMode));
		this.ReturnToPreviousMode();
		return false;
	}

	// Token: 0x06002554 RID: 9556 RVA: 0x000BC0A1 File Offset: 0x000BA2A1
	protected void ContinueButtonPress_TutorialProgress(UIEvent e)
	{
		this.ContinueTutorialEvents();
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x000BC0AC File Offset: 0x000BA2AC
	public void ContinueTutorialEvents()
	{
		if (this.ContinueDefaultEvents())
		{
			return;
		}
		this.ContinueButtonPress_Common();
		this.m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_TutorialProgress));
		this.m_continueText.gameObject.SetActive(false);
		this.ShowTutorialProgress();
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x000BC0F8 File Offset: 0x000BA2F8
	private bool ContinueDefaultEvents()
	{
		if (this.IsPlayingBlockingAnim())
		{
			return true;
		}
		if (this.m_currentlyShowingReward != null)
		{
			this.m_currentlyShowingReward.Hide(true);
			this.m_currentlyShowingReward = null;
		}
		this.HideScoreScreen();
		if (!this.m_haveShownTwoScoop)
		{
			return true;
		}
		this.HideTwoScoop();
		if (this.ShowHeroRewardEvent() && this.m_heroRewardEventReady)
		{
			return true;
		}
		if (this.ShowRewardTrackXpGains())
		{
			return true;
		}
		if (this.ShowNextRewardTrackAutoClaimedReward())
		{
			return true;
		}
		if (this.ShowFixedRewards())
		{
			return true;
		}
		if (this.ShowGoldReward())
		{
			return true;
		}
		if (this.ShowRankedCardBackProgress())
		{
			return true;
		}
		if (this.ShowRankChange())
		{
			return true;
		}
		if (this.ShowRankedRewards())
		{
			return true;
		}
		if (this.ShowNextProgressionQuestReward())
		{
			return true;
		}
		if (this.ShowNextCompletedQuest())
		{
			return true;
		}
		if (this.ShowNextReward())
		{
			return true;
		}
		if (this.ShowNextGenericReward())
		{
			return true;
		}
		if (!SpectatorManager.Get().IsSpectatingOrWatching && TemporaryAccountManager.IsTemporaryAccount() && this.ShowHealUpDialog())
		{
			return true;
		}
		if (this.ShowPushNotificationPrompt())
		{
			return true;
		}
		if (this.ShowAppRatingPrompt())
		{
			return true;
		}
		this.m_doneDisplayingRewards = true;
		return false;
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnTwoScoopShown()
	{
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnTwoScoopHidden()
	{
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void InitGoldRewardUI()
	{
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x000BC200 File Offset: 0x000BA400
	protected virtual void InitVictoryGoldRewardUI(GamesWonIndicator gamesWonIndicator)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		int num = (netObject == null) ? 100 : netObject.MaxGoldPerDay;
		int num2 = (netObject == null) ? 10 : netObject.GoldPerReward;
		int num3 = (netObject == null) ? 3 : netObject.WinsPerGold;
		string text = EndGameScreen.GetFriendlyChallengeRewardText();
		if (string.IsNullOrEmpty(text))
		{
			TAG_GOLD_REWARD_STATE tag = GameState.Get().GetFriendlySidePlayer().GetTag<TAG_GOLD_REWARD_STATE>(GAME_TAG.GOLD_REWARD_STATE);
			if (tag != TAG_GOLD_REWARD_STATE.ELIGIBLE)
			{
				Log.Gameplay.Print(string.Format("EngGameScreen.InitVictoryGoldRewardUI(): goldRewardState = {0}", tag), Array.Empty<object>());
				switch (tag)
				{
				case TAG_GOLD_REWARD_STATE.ALREADY_CAPPED:
					text = GameStrings.Format("GLOBAL_GOLD_REWARD_ALREADY_CAPPED", new object[]
					{
						num
					});
					break;
				case TAG_GOLD_REWARD_STATE.BAD_RATING:
					text = GameStrings.Get("GLOBAL_GOLD_REWARD_BAD_RATING");
					break;
				case TAG_GOLD_REWARD_STATE.SHORT_GAME_BY_TIME:
					text = GameStrings.Get("GLOBAL_GOLD_REWARD_SHORT_GAME_BY_TIME");
					break;
				case TAG_GOLD_REWARD_STATE.OVER_CAIS:
					text = GameStrings.Get("GLOBAL_GOLD_REWARD_OVER_CAIS");
					break;
				default:
					return;
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			this.m_noGoldRewardText.gameObject.SetActive(true);
			this.m_noGoldRewardText.Text = text;
			return;
		}
		NetCache.NetCacheGamesPlayed netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheGamesPlayed>();
		if (netObject2 == null)
		{
			return;
		}
		Log.Gameplay.Print(string.Format("EndGameTwoScoop.UpdateData(): {0}/{1} wins towards {2} gold", netObject2.FreeRewardProgress, num3, num2), Array.Empty<object>());
		gamesWonIndicator.Init(Reward.Type.GOLD, num2, num3, netObject2.FreeRewardProgress, GamesWonIndicator.InnKeeperTrigger.NONE);
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x000BC368 File Offset: 0x000BA568
	private static string GetFriendlyChallengeRewardMessage(global::Achievement achieve)
	{
		if (DemoMgr.Get().IsDemo())
		{
			return null;
		}
		string text = null;
		if (achieve.DbfRecord.MaxDefense > 0)
		{
			text = EndGameScreen.GetFriendlyChallengeEarlyConcedeMessage(achieve.DbfRecord.MaxDefense);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
		}
		AchieveRegionDataDbfRecord currentRegionData = achieve.GetCurrentRegionData();
		if (currentRegionData != null && currentRegionData.RewardableLimit > 0 && achieve.IntervalRewardStartDate > 0L)
		{
			DateTime d = DateTime.FromFileTimeUtc(achieve.IntervalRewardStartDate);
			if ((DateTime.UtcNow - d).TotalDays < currentRegionData.RewardableInterval && achieve.IntervalRewardCount >= currentRegionData.RewardableLimit)
			{
				text = GameStrings.Get("GLOBAL_FRIENDLYCHALLENGE_QUEST_REWARD_AT_LIMIT");
			}
		}
		if (string.IsNullOrEmpty(text) && currentRegionData != null && currentRegionData.RewardableLimit > 0 && FriendChallengeMgr.Get().DidReceiveChallenge())
		{
			achieve.IncrementIntervalRewardCount();
		}
		return text;
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x000BC434 File Offset: 0x000BA634
	protected static string GetFriendlyChallengeRewardText()
	{
		if (!FriendChallengeMgr.Get().HasChallenge())
		{
			return null;
		}
		if (DemoMgr.Get().IsDemo())
		{
			return null;
		}
		string text = null;
		AchieveManager achieveManager = AchieveManager.Get();
		PartyQuestInfo partyQuestInfo = FriendChallengeMgr.Get().GetPartyQuestInfo();
		if (partyQuestInfo != null)
		{
			bool flag = FriendChallengeMgr.Get().DidSendChallenge();
			bool flag2 = FriendChallengeMgr.Get().DidReceiveChallenge();
			PlayerType playerType = PlayerType.PT_ANY;
			if (flag)
			{
				playerType = PlayerType.PT_FRIENDLY_CHALLENGER;
			}
			if (flag2)
			{
				playerType = PlayerType.PT_FRIENDLY_CHALLENGEE;
			}
			for (int i = 0; i < partyQuestInfo.QuestIds.Count; i++)
			{
				global::Achievement achievement = achieveManager.GetAchievement(partyQuestInfo.QuestIds[i]);
				if (achievement != null && achievement.IsValidFriendlyPlayerChallengeType(playerType))
				{
					text = EndGameScreen.GetFriendlyChallengeRewardMessage(achievement);
				}
				if (string.IsNullOrEmpty(text))
				{
					global::Achievement achievement2 = achieveManager.GetAchievement(achievement.DbfRecord.SharedAchieveId);
					if (achievement2 != null && achievement2.IsValidFriendlyPlayerChallengeType(playerType))
					{
						text = EndGameScreen.GetFriendlyChallengeRewardMessage(achievement2);
					}
				}
			}
		}
		if (string.IsNullOrEmpty(text) && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, false))
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool flag3 = (from a in achieveManager.GetActiveQuests(false)
			where a.IsAffectedByFriendWeek && (a.AchieveTrigger == Achieve.Trigger.WIN || a.AchieveTrigger == Achieve.Trigger.FINISH) && a.GameModeRequiresNonFriendlyChallenge
			select a).Any<global::Achievement>();
			bool flag4 = false;
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl() && netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate)
			{
				BrawlType challengeBrawlType = FriendChallengeMgr.Get().GetChallengeBrawlType();
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(challengeBrawlType);
				TavernBrawlPlayerRecord record = TavernBrawlManager.Get().GetRecord(challengeBrawlType);
				bool flag5 = mission != null && (mission.rewardTrigger == RewardTrigger.REWARD_TRIGGER_WIN_GAME || mission.rewardTrigger == RewardTrigger.REWARD_TRIGGER_FINISH_GAME);
				if (mission != null && mission.rewardType > RewardType.REWARD_UNKNOWN && flag5 && record != null && record.RewardProgress < mission.RewardTriggerQuota)
				{
					flag4 = true;
				}
			}
			if (!flag3 && !flag4)
			{
				return null;
			}
			int concederMaxDefense = 0;
			if (netObject != null)
			{
				concederMaxDefense = netObject.FriendWeekConcederMaxDefense;
			}
			text = EndGameScreen.GetFriendlyChallengeEarlyConcedeMessage(concederMaxDefense);
		}
		return text;
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x000BC624 File Offset: 0x000BA824
	private static string GetFriendlyChallengeEarlyConcedeMessage(int concederMaxDefense)
	{
		if (DemoMgr.Get().IsDemo())
		{
			return null;
		}
		int num = 0;
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			num = netObject.FriendWeekConcededGameMinTotalTurns;
		}
		string key = null;
		int num2 = 0;
		GameState gameState = GameState.Get();
		bool flag = false;
		foreach (KeyValuePair<int, Player> keyValuePair in gameState.GetPlayerMap())
		{
			Player value = keyValuePair.Value;
			TAG_PLAYSTATE preGameOverPlayState = value.GetPreGameOverPlayState();
			if (preGameOverPlayState == TAG_PLAYSTATE.CONCEDED || preGameOverPlayState == TAG_PLAYSTATE.DISCONNECTED)
			{
				flag = true;
				Entity hero = value.GetHero();
				if (hero != null)
				{
					num2 = hero.GetCurrentDefense();
					if (value.GetSide() == Player.Side.FRIENDLY)
					{
						key = "GLOBAL_FRIENDLYCHALLENGE_REWARD_CONCEDED_YOURSELF";
						break;
					}
					key = "GLOBAL_FRIENDLYCHALLENGE_REWARD_CONCEDED_YOUR_OPPONENT";
					break;
				}
			}
		}
		bool flag2 = concederMaxDefense > 0;
		bool flag3 = !flag || (flag2 && num2 <= concederMaxDefense);
		bool flag4 = !flag || gameState.GetTurn() >= num;
		if (!flag3 && !flag4)
		{
			return GameStrings.Get(key);
		}
		return null;
	}

	// Token: 0x0600255E RID: 9566 RVA: 0x000BC738 File Offset: 0x000BA938
	protected void BackToMode(SceneMgr.Mode mode)
	{
		AchieveManager.Get().RemoveAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated));
		this.HideTwoScoop();
		if (EndGameScreen.OnBackOutOfGameplay != null)
		{
			EndGameScreen.OnBackOutOfGameplay();
		}
		if (!this.m_hasAlreadySetMode)
		{
			this.m_hasAlreadySetMode = true;
			base.StartCoroutine(this.ToMode(mode));
			Navigation.Clear();
		}
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x000BC795 File Offset: 0x000BA995
	private IEnumerator ToMode(SceneMgr.Mode mode)
	{
		yield return new WaitForSeconds(0.5f);
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		yield break;
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x000BC7A4 File Offset: 0x000BA9A4
	private void ReturnToPreviousMode()
	{
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		if (postGameSceneMode == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			DuelsConfig.Get().SetLastGameResult(GameMgr.Get().LastGameData.GameResult);
		}
		this.BackToMode(postGameSceneMode);
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x000BC7EC File Offset: 0x000BA9EC
	private void ShowScoreScreen()
	{
		if (!GameState.Get().CanShowScoreScreen())
		{
			return;
		}
		this.m_scoreScreen = GameUtils.LoadGameObjectWithComponent<ScoreScreen>(this.m_ScoreScreenPrefab);
		if (!this.m_scoreScreen)
		{
			return;
		}
		TransformUtil.AttachAndPreserveLocalTransform(this.m_scoreScreen.transform, base.transform);
		SceneUtils.SetLayer(this.m_scoreScreen, GameLayer.IgnoreFullScreenEffects);
		this.m_scoreScreen.Show();
		this.m_showingScoreScreen = true;
		this.SetPlayingBlockingAnim(true);
		base.StartCoroutine(this.WaitThenSetPlayingBlockingAnim(0.65f, false));
		if (Gameplay.Get().HasBattleNetFatalError())
		{
			this.m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ContinueButtonPress_ProceedToError));
		}
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x000BC899 File Offset: 0x000BAA99
	private void HideScoreScreen()
	{
		if (!this.m_scoreScreen)
		{
			return;
		}
		this.m_scoreScreen.Hide();
		this.m_showingScoreScreen = false;
		this.SetPlayingBlockingAnim(true);
		base.StartCoroutine(this.WaitThenSetPlayingBlockingAnim(0.25f, false));
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x000BC8D8 File Offset: 0x000BAAD8
	protected void HideTwoScoop()
	{
		if (!this.m_twoScoop.IsShown())
		{
			return;
		}
		this.m_twoScoop.Hide();
		this.m_noGoldRewardText.gameObject.SetActive(false);
		this.OnTwoScoopHidden();
		if (EndGameScreen.OnTwoScoopsShown != null)
		{
			EndGameScreen.OnTwoScoopsShown(false, this.m_twoScoop);
		}
		if (InputManager.Get() != null)
		{
			InputManager.Get().EnableInput();
		}
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x000BC944 File Offset: 0x000BAB44
	protected void ShowTwoScoop()
	{
		base.StartCoroutine(this.ShowTwoScoopWhenReady());
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x000BC953 File Offset: 0x000BAB53
	private IEnumerator ShowTwoScoopWhenReady()
	{
		while (this.m_scoreScreen)
		{
			yield return null;
		}
		if (this.ShouldMakeUtilRequests())
		{
			while (!this.m_netCacheReady)
			{
				yield return null;
			}
			while (!this.m_achievesReady)
			{
				yield return null;
			}
		}
		while (!this.m_rewardsLoaded)
		{
			yield return null;
		}
		while (!this.m_twoScoop.IsLoaded())
		{
			yield return null;
		}
		while (this.JustEarnedHeroReward() && !this.m_heroRewardEventReady)
		{
			yield return null;
		}
		this.m_twoScoop.Show(true);
		if (!SpectatorManager.Get().IsSpectatingOrWatching && this.ShouldMakeUtilRequests())
		{
			this.InitGoldRewardUI();
		}
		this.OnTwoScoopShown();
		this.m_haveShownTwoScoop = true;
		if (EndGameScreen.OnTwoScoopsShown != null)
		{
			EndGameScreen.OnTwoScoopsShown(true, this.m_twoScoop);
		}
		yield break;
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x000BC962 File Offset: 0x000BAB62
	protected IEnumerator WaitThenSetPlayingBlockingAnim(float sec, bool set)
	{
		yield return new WaitForSeconds(sec);
		this.SetPlayingBlockingAnim(set);
		yield break;
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x000BC97F File Offset: 0x000BAB7F
	protected bool ShouldMakeUtilRequests()
	{
		return Network.ShouldBeConnectedToAurora();
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x000BC98C File Offset: 0x000BAB8C
	protected bool IsReady()
	{
		return this.m_shown && this.m_netCacheReady && this.m_achievesReady && this.m_rewardsLoaded && ((this.m_rankChangeReady && this.m_medalInfoUpdated) || !this.m_shouldShowRankChange) && (this.m_rankedRewardDisplay != null || this.m_rankedRewardsToDisplay.Count == 0) && (this.m_rankedCardBackProgress != null || !this.m_shouldShowRankedCardBackProgress) && (RewardXpNotificationManager.Get().IsReady || !this.m_shouldShowRewardXpGains);
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000BCA1B File Offset: 0x000BAC1B
	public bool IsDoneDisplayingRewards()
	{
		return this.m_doneDisplayingRewards;
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x000BCA23 File Offset: 0x000BAC23
	private bool ShowStandardFlowIfReady()
	{
		if (!this.IsReady() && (this.ShouldMakeUtilRequests() || !this.m_shown))
		{
			return false;
		}
		this.SendRankedInitTelemetryIfNeeded();
		this.ShowStandardFlow();
		return true;
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x000BCA4C File Offset: 0x000BAC4C
	protected virtual void ShowStandardFlow()
	{
		this.ShowTwoScoop();
		if (RewardXpNotificationManager.Get().HasXpGainsToShow)
		{
			RewardXpNotificationManager.Get().ShowRewardTrackXpGains(delegate
			{
				this.ContinueEvents();
			}, true);
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_continueText.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x000BCAA0 File Offset: 0x000BACA0
	protected virtual void OnNetCacheReady()
	{
		this.m_netCacheReady = true;
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (RewardXpNotificationManager.Get().HasXpGainsToShow)
		{
			this.m_shouldShowRewardXpGains = true;
			RewardXpNotificationManager.Get().InitEndOfGameFlow(delegate
			{
				this.ShowStandardFlowIfReady();
			});
		}
		if (this.m_shouldShowRankChange)
		{
			this.RetryMedalInfoRequestIfNeeded();
			this.LoadRankChange();
			this.LoadRankedRewardDisplay();
			this.LoadRankedCardBackProgress();
		}
		this.MaybeUpdateRewards();
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x000BCB1C File Offset: 0x000BAD1C
	private void RetryMedalInfoRequestIfNeeded()
	{
		if (this.IsMedalInfoRetryNeeded())
		{
			base.StartCoroutine(this.RetryMedalInfoRequest());
		}
		else
		{
			NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheMedalInfo), new Action(this.OnMedalInfoUpdate));
			this.m_medalInfoUpdated = true;
		}
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x000BCB70 File Offset: 0x000BAD70
	private bool IsMedalInfoRetryNeeded()
	{
		if (!this.ShouldMakeUtilRequests())
		{
			return false;
		}
		if (!this.m_shouldShowRankChange)
		{
			return false;
		}
		if (this.m_medalInfoRetryCount >= 3)
		{
			return false;
		}
		FormatType formatType = Options.GetFormatType();
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		return localPlayerMedalInfo == null || localPlayerMedalInfo.GetChangeType(formatType) == RankChangeType.NO_GAME_PLAYED;
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x000BCBBC File Offset: 0x000BADBC
	private IEnumerator RetryMedalInfoRequest()
	{
		if (this.m_medalInfoRetryCount == 0)
		{
			this.m_medalInfoRetryDelay = 1f;
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheMedalInfo), new Action(this.OnMedalInfoUpdate));
		}
		else
		{
			this.m_medalInfoRetryDelay *= 2f;
		}
		this.m_medalInfoRetryCount++;
		yield return new WaitForSeconds(this.m_medalInfoRetryDelay);
		NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
		yield break;
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x000BCBCB File Offset: 0x000BADCB
	private void OnMedalInfoUpdate()
	{
		this.RetryMedalInfoRequestIfNeeded();
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x000BCBD4 File Offset: 0x000BADD4
	private void SendRankedInitTelemetryIfNeeded()
	{
		if (!this.m_shouldShowRankChange)
		{
			return;
		}
		if (this.m_hasSentRankedInitTelemetry)
		{
			return;
		}
		this.m_hasSentRankedInitTelemetry = true;
		float num = Time.time - this.m_endGameScreenStartTime;
		FormatType formatType = Options.GetFormatType();
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		bool flag = this.m_medalInfoRetryCount >= 3 && (localPlayerMedalInfo == null || localPlayerMedalInfo.GetChangeType(formatType) == RankChangeType.NO_GAME_PLAYED);
		if (flag && localPlayerMedalInfo != null)
		{
			Log.All.PrintError("EndGameScreen_MedalInfoTimeOut elapsedTime={0} retries={1} prev={2} curr={3}", new object[]
			{
				num,
				this.m_medalInfoRetryCount,
				localPlayerMedalInfo.GetPreviousMedal(formatType).ToString(),
				localPlayerMedalInfo.GetCurrentMedal(formatType).ToString()
			});
		}
		bool showRankedReward = this.m_rankedRewardsToDisplay.Count > 0;
		TelemetryManager.Client().SendEndGameScreenInit(num, this.m_medalInfoRetryCount, flag, showRankedReward, this.m_shouldShowRankedCardBackProgress, this.m_rewards.Count);
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x000BCCBC File Offset: 0x000BAEBC
	private void LoadRankChange()
	{
		AssetReference rank_CHANGE_TWO_SCOOP_PREFAB_NEW = RankMgr.RANK_CHANGE_TWO_SCOOP_PREFAB_NEW;
		AssetLoader.Get().InstantiatePrefab(rank_CHANGE_TWO_SCOOP_PREFAB_NEW, new PrefabCallback<GameObject>(this.OnRankChangeLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x000BCCE9 File Offset: 0x000BAEE9
	private void OnRankChangeLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_rankChangeTwoScoop = go;
		this.m_rankChangeTwoScoop.gameObject.SetActive(false);
		this.m_rankChangeReady = true;
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x06002574 RID: 9588 RVA: 0x000BCD11 File Offset: 0x000BAF11
	private void OnRankChangeClosed()
	{
		this.m_isShowingRankChange = false;
		this.m_shouldShowRankChange = false;
		this.ContinueEvents();
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x000BCD28 File Offset: 0x000BAF28
	private void LoadRankedRewardDisplay()
	{
		if (!RankMgr.Get().GetLocalPlayerMedalInfo().GetRankedRewardsEarned(Options.GetFormatType(), ref this.m_rankedRewardsToDisplay))
		{
			return;
		}
		if (this.m_rankedRewardsToDisplay.Count == 0)
		{
			return;
		}
		this.m_rankedRewardDisplayWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_DISPLAY_PREFAB, false);
		this.m_rankedRewardDisplayWidget.RegisterReadyListener(delegate(object _)
		{
			this.OnRankedRewardDisplayWidgetReady();
		}, null, true);
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x000BCD8F File Offset: 0x000BAF8F
	private void OnRankedRewardDisplayWidgetReady()
	{
		this.m_rankedRewardDisplay = this.m_rankedRewardDisplayWidget.GetComponentInChildren<RankedRewardDisplay>();
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x000BCDAC File Offset: 0x000BAFAC
	private void LoadRankedCardBackProgress()
	{
		this.m_shouldShowRankedCardBackProgress = RankMgr.Get().GetLocalPlayerMedalInfo().ShouldShowCardBackProgress();
		if (!this.m_shouldShowRankedCardBackProgress)
		{
			return;
		}
		this.m_rankedCardBackProgressWidget = WidgetInstance.Create(RankMgr.RANKED_CARDBACK_PROGRESS_DISPLAY_PREFAB, false);
		this.m_rankedCardBackProgressWidget.RegisterReadyListener(delegate(object _)
		{
			this.OnRankedCardBackProgressWidgetReady();
		}, null, true);
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x000BCE06 File Offset: 0x000BB006
	private void OnRankedCardBackProgressWidgetReady()
	{
		this.m_rankedCardBackProgress = this.m_rankedCardBackProgressWidget.GetComponentInChildren<RankedCardBackProgressDisplay>();
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x000BCE20 File Offset: 0x000BB020
	private IEnumerator WaitForAchieveManager()
	{
		while (!AchieveManager.Get().IsReady())
		{
			yield return null;
		}
		this.m_achievesReady = true;
		this.MaybeUpdateRewards();
		yield break;
	}

	// Token: 0x0600257A RID: 9594 RVA: 0x000BCE2F File Offset: 0x000BB02F
	private void ProcessPreviousAchievements()
	{
		this.OnAchievesUpdated(new List<global::Achievement>(), new List<global::Achievement>(), null);
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x000BCE44 File Offset: 0x000BB044
	private void OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userData)
	{
		if (!GameUtils.AreAllTutorialsComplete())
		{
			return;
		}
		using (List<global::Achievement>.Enumerator enumerator = AchieveManager.Get().GetNewCompletedAchievesToShow().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				global::Achievement achieve = enumerator.Current;
				if (achieve.RewardTiming == Achieve.RewardTiming.IMMEDIATE && this.m_completedQuests.Find((global::Achievement obj) => achieve.ID == obj.ID) == null && (this.m_heroRewardAchievement == null || this.m_heroRewardAchievement.ID != achieve.ID))
				{
					this.m_completedQuests.Add(achieve);
				}
			}
		}
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000BCF00 File Offset: 0x000BB100
	private void OnGenericRewardUpdated(long rewardNoticeId, object userData)
	{
		this.m_genericRewardChestNoticeIdsReady.Add(rewardNoticeId);
		this.UpdateRewards();
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000BCF15 File Offset: 0x000BB115
	protected bool HasShownScoops()
	{
		return this.m_haveShownTwoScoop;
	}

	// Token: 0x0600257E RID: 9598 RVA: 0x000BCF1D File Offset: 0x000BB11D
	protected void SetHeroRewardEventReady(bool isReady)
	{
		this.m_heroRewardEventReady = isReady;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000BCF26 File Offset: 0x000BB126
	private void MaybeUpdateRewards()
	{
		if (!this.m_achievesReady)
		{
			return;
		}
		if (!this.m_netCacheReady)
		{
			return;
		}
		this.UpdateRewards();
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x000BCF48 File Offset: 0x000BB148
	private void LoadRewards(List<RewardData> rewardsToLoad, Reward.DelOnRewardLoaded callback)
	{
		if (rewardsToLoad == null)
		{
			return;
		}
		foreach (RewardData rewardData in rewardsToLoad)
		{
			if (PopupDisplayManager.Get().UpdateNoticesSeen(rewardData))
			{
				this.m_numRewardsToLoad++;
				rewardData.LoadRewardObject(callback);
			}
		}
	}

	// Token: 0x06002581 RID: 9601 RVA: 0x000BCFB8 File Offset: 0x000BB1B8
	private void UpdateRewards()
	{
		bool flag = true;
		if (GameMgr.Get().IsTutorial())
		{
			flag = GameUtils.AreAllTutorialsComplete();
		}
		List<RewardData> list = null;
		List<RewardData> rewardsToLoad = null;
		List<RewardData> list2 = null;
		if (flag)
		{
			List<NetCache.ProfileNotice> list3 = (from n in NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices
			where n.Type != NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST || this.m_genericRewardChestNoticeIdsReady.Any((long r) => n.NoticeID == r)
			select n).ToList<NetCache.ProfileNotice>();
			list3.RemoveAll((NetCache.ProfileNotice n) => n.Origin == NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS);
			List<RewardData> rewards = RewardUtils.GetRewards(list3);
			HashSet<Achieve.RewardTiming> rewardTimings = new HashSet<Achieve.RewardTiming>
			{
				Achieve.RewardTiming.IMMEDIATE
			};
			RewardUtils.GetViewableRewards(rewards, rewardTimings, out list, out rewardsToLoad, ref list2, ref this.m_completedQuests);
		}
		else
		{
			list = new List<RewardData>();
		}
		this.JustEarnedHeroReward();
		if (!GameMgr.Get().IsSpectator())
		{
			List<RewardData> customRewards = GameState.Get().GetGameEntity().GetCustomRewards();
			if (customRewards != null)
			{
				list.AddRange(customRewards);
			}
		}
		this.LoadRewards(list, new Reward.DelOnRewardLoaded(this.OnRewardObjectLoaded));
		this.LoadRewards(rewardsToLoad, new Reward.DelOnRewardLoaded(this.OnGenericRewardObjectLoaded));
		if (this.m_numRewardsToLoad == 0)
		{
			this.m_rewardsLoaded = true;
		}
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x000BD0C1 File Offset: 0x000BB2C1
	private void OnRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.LoadReward(reward, ref this.m_rewards);
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x000BD0D0 File Offset: 0x000BB2D0
	private void OnGenericRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.LoadReward(reward, ref this.m_genericRewards);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x000BD0DF File Offset: 0x000BB2DF
	private void PositionReward(Reward reward)
	{
		reward.transform.parent = base.transform;
		reward.transform.localRotation = Quaternion.identity;
		reward.transform.localPosition = PopupDisplayManager.Get().GetRewardLocalPos();
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x000BD118 File Offset: 0x000BB318
	private void LoadReward(Reward reward, ref List<Reward> allRewards)
	{
		reward.Hide(false);
		this.PositionReward(reward);
		allRewards.Add(reward);
		this.m_numRewardsToLoad--;
		if (this.m_numRewardsToLoad > 0)
		{
			return;
		}
		RewardUtils.SortRewards(ref allRewards);
		this.m_rewardsLoaded = true;
		this.ShowStandardFlowIfReady();
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000BD168 File Offset: 0x000BB368
	private void DisplayLoadedRewardObject(Reward reward, object callbackData)
	{
		if (this.m_currentlyShowingReward != null)
		{
			this.m_currentlyShowingReward.Hide(true);
			this.m_currentlyShowingReward = null;
		}
		reward.Hide(false);
		this.PositionReward(reward);
		this.m_currentlyShowingReward = reward;
		this.SetPlayingBlockingAnim(true);
		SceneUtils.SetLayer(this.m_currentlyShowingReward.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.ShowReward(this.m_currentlyShowingReward);
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x000BD1D0 File Offset: 0x000BB3D0
	private void ShowReward(Reward reward)
	{
		bool updateCacheValues = !(reward is CardReward);
		RewardUtils.ShowReward(UserAttentionBlocker.NONE, reward, updateCacheValues, PopupDisplayManager.Get().GetRewardPunchScale(), PopupDisplayManager.Get().GetRewardScale(), "", null, null);
		base.StartCoroutine(this.WaitThenSetPlayingBlockingAnim(0.35f, false));
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool ShowHeroRewardEvent()
	{
		return false;
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x000BD224 File Offset: 0x000BB424
	protected bool ShowFixedRewards()
	{
		if (this.m_isShowingFixedRewards)
		{
			return true;
		}
		HashSet<Achieve.RewardTiming> rewardVisualTimings = new HashSet<Achieve.RewardTiming>
		{
			Achieve.RewardTiming.IMMEDIATE
		};
		FixedRewardsMgr.DelOnAllFixedRewardsShown allRewardsShownCallback = delegate()
		{
			this.m_isShowingFixedRewards = false;
			this.ContinueEvents();
		};
		this.m_isShowingFixedRewards = FixedRewardsMgr.Get().ShowFixedRewards(UserAttentionBlocker.NONE, rewardVisualTimings, allRewardsShownCallback, null);
		return this.m_isShowingFixedRewards;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x000BD270 File Offset: 0x000BB470
	private bool ShowGoldReward()
	{
		int num = this.m_rewards.FindIndex(delegate(Reward reward)
		{
			GoldRewardData goldRewardData;
			return (goldRewardData = (reward.Data as GoldRewardData)) != null && goldRewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.TOURNEY;
		});
		if (num < 0)
		{
			return false;
		}
		Reward item = this.m_rewards[num];
		this.m_rewards.RemoveAt(num);
		this.m_rewards.Insert(0, item);
		this.ShowNextReward();
		return true;
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x000BD2DC File Offset: 0x000BB4DC
	private bool ShowNextProgressionQuestReward()
	{
		return QuestManager.Get().ShowNextReward(delegate
		{
			this.ContinueEvents();
		});
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x000BD2FC File Offset: 0x000BB4FC
	protected bool ShowNextCompletedQuest()
	{
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (this.m_completedQuests.Count == 0)
		{
			return false;
		}
		global::Achievement achievement = this.m_completedQuests[0];
		this.m_completedQuests.RemoveAt(0);
		if (!achievement.UseGenericRewardVisual)
		{
			bool flag = false;
			foreach (RewardData rewardData in achievement.Rewards)
			{
				if (rewardData.RewardType == Reward.Type.CARD)
				{
					CardRewardData cardRewardData = rewardData as CardRewardData;
					if (cardRewardData != null)
					{
						TAG_CARD_SET cardSetFromCardID = GameUtils.GetCardSetFromCardID(cardRewardData.CardID);
						flag |= !GameDbf.GetIndex().GetCardSet(cardSetFromCardID).IsCoreCardSet;
					}
				}
			}
			bool updateCacheValues = !flag;
			QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, new QuestToast.DelOnCloseQuestToast(this.ShowQuestToastCallback), updateCacheValues, achievement);
			NarrativeManager.Get().OnQuestCompleteShown(achievement.ID);
		}
		else
		{
			achievement.AckCurrentProgressAndRewardNotices();
			achievement.Rewards[0].LoadRewardObject(new Reward.DelOnRewardLoaded(this.DisplayLoadedRewardObject));
		}
		return true;
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x000BD41C File Offset: 0x000BB61C
	protected void ShowQuestToastCallback(object userData)
	{
		if (this == null)
		{
			return;
		}
		this.ContinueEvents();
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x000BD430 File Offset: 0x000BB630
	protected bool ShowRewardTrackXpGains()
	{
		RewardXpNotificationManager rewardXpNotificationManager = RewardXpNotificationManager.Get();
		if (rewardXpNotificationManager.IsShowingXpGains && !rewardXpNotificationManager.JustShowGameXp)
		{
			rewardXpNotificationManager.TerminateEarly();
			return false;
		}
		if (!rewardXpNotificationManager.HasXpGainsToShow && !rewardXpNotificationManager.JustShowGameXp)
		{
			return false;
		}
		if (rewardXpNotificationManager.IsShowingXpGains && rewardXpNotificationManager.JustShowGameXp)
		{
			rewardXpNotificationManager.ContinueNotifications();
		}
		else
		{
			rewardXpNotificationManager.ShowRewardTrackXpGains(delegate
			{
				this.ContinueEvents();
			}, false);
		}
		return true;
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x000BD49C File Offset: 0x000BB69C
	protected bool ShowNextRewardTrackAutoClaimedReward()
	{
		if (this.m_isShowingTrackRewards)
		{
			return true;
		}
		Action callback = delegate()
		{
			this.m_isShowingTrackRewards = false;
			this.ContinueEvents();
		};
		if (!RewardTrackManager.Get().ShowNextReward(callback))
		{
			return false;
		}
		this.m_isShowingTrackRewards = true;
		return true;
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x000BD4D8 File Offset: 0x000BB6D8
	protected bool ShowNextReward()
	{
		if (this.m_rewards.Count == 0)
		{
			return false;
		}
		this.SetPlayingBlockingAnim(true);
		this.m_currentlyShowingReward = this.m_rewards[0];
		this.m_rewards.RemoveAt(0);
		this.ShowReward(this.m_currentlyShowingReward);
		return true;
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x000BD528 File Offset: 0x000BB728
	protected bool ShowNextGenericReward()
	{
		if (this.m_genericRewards.Count == 0)
		{
			return false;
		}
		this.SetPlayingBlockingAnim(true);
		this.m_currentlyShowingReward = this.m_genericRewards[0];
		this.m_genericRewards.RemoveAt(0);
		QuestToast.ShowGenericRewardQuestToast(UserAttentionBlocker.NONE, new QuestToast.DelOnCloseQuestToast(this.ShowQuestToastCallback), this.m_currentlyShowingReward.Data, this.m_currentlyShowingReward.Data.NameOverride, this.m_currentlyShowingReward.Data.DescriptionOverride);
		base.StartCoroutine(this.WaitThenSetPlayingBlockingAnim(0.35f, false));
		return true;
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x000BD5BC File Offset: 0x000BB7BC
	private bool ShowRankChange()
	{
		if (!this.m_shouldShowRankChange)
		{
			return false;
		}
		if (this.m_isShowingRankChange)
		{
			return true;
		}
		this.m_rankChangeTwoScoop.gameObject.SetActive(true);
		RankChangeTwoScoop_NEW component = this.m_rankChangeTwoScoop.GetComponent<RankChangeTwoScoop_NEW>();
		component.Initialize(RankMgr.Get().GetLocalPlayerMedalInfo(), Options.GetFormatType(), new Action(this.OnRankChangeClosed));
		component.Show();
		this.m_isShowingRankChange = true;
		return true;
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x000BD628 File Offset: 0x000BB828
	private bool ShowRankedRewards()
	{
		if (this.m_rankedRewardsToDisplay.Count == 0)
		{
			return false;
		}
		if (this.m_isShowingRankedReward)
		{
			return true;
		}
		this.m_isShowingRankedReward = true;
		FormatType formatType = Options.GetFormatType();
		TranslatedMedalInfo currentMedal = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(formatType);
		this.m_rankedRewardDisplay.Initialize(currentMedal, this.m_rankedRewardsToDisplay, new Action(this.OnRankedRewardsClosed));
		this.m_rankedRewardDisplay.Show();
		return true;
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000BD696 File Offset: 0x000BB896
	private void OnRankedRewardsClosed()
	{
		this.m_isShowingRankedReward = false;
		this.m_rankedRewardsToDisplay.Clear();
		UnityEngine.Object.Destroy(this.m_rankedRewardDisplayWidget.gameObject);
		this.ContinueEvents();
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000BD6C4 File Offset: 0x000BB8C4
	private bool ShowRankedCardBackProgress()
	{
		if (!this.m_shouldShowRankedCardBackProgress)
		{
			return false;
		}
		if (this.m_isShowingRankedCardBackProgress)
		{
			return true;
		}
		this.m_isShowingRankedCardBackProgress = true;
		this.m_rankedCardBackProgress.Initialize(RankMgr.Get().GetLocalPlayerMedalInfo(), new Action(this.OnRankedCardBackProgressClosed));
		this.m_rankedCardBackProgress.Show();
		return true;
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x000BD719 File Offset: 0x000BB919
	private void OnRankedCardBackProgressClosed()
	{
		this.m_shouldShowRankedCardBackProgress = false;
		this.m_isShowingRankedCardBackProgress = false;
		UnityEngine.Object.Destroy(this.m_rankedCardBackProgressWidget.gameObject);
		if (this.FindRankedCardBackRewardAndMakeNext())
		{
			this.ShowNextReward();
			return;
		}
		this.ContinueEvents();
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000BD750 File Offset: 0x000BB950
	private bool FindRankedCardBackRewardAndMakeNext()
	{
		int currentSeasonId = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentSeasonId();
		int rankedCardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(currentSeasonId);
		int num = this.m_rewards.FindIndex(delegate(Reward reward)
		{
			CardBackRewardData cardBackRewardData;
			return (cardBackRewardData = (reward.Data as CardBackRewardData)) != null && cardBackRewardData.CardBackID == rankedCardBackId;
		});
		if (num < 0)
		{
			return false;
		}
		Reward item = this.m_rewards[num];
		this.m_rewards.RemoveAt(num);
		this.m_rewards.Insert(0, item);
		return true;
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool JustEarnedHeroReward()
	{
		return false;
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool ShowHealUpDialog()
	{
		return false;
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool ShowPushNotificationPrompt()
	{
		return false;
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool ShowAppRatingPrompt()
	{
		return false;
	}

	// Token: 0x040014DD RID: 5341
	public EndGameTwoScoop m_twoScoop;

	// Token: 0x040014DE RID: 5342
	public PegUIElement m_hitbox;

	// Token: 0x040014DF RID: 5343
	public UberText m_noGoldRewardText;

	// Token: 0x040014E0 RID: 5344
	public UberText m_continueText;

	// Token: 0x040014E1 RID: 5345
	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_ScoreScreenPrefab;

	// Token: 0x040014E2 RID: 5346
	public static EndGameScreen.OnTwoScoopsShownHandler OnTwoScoopsShown;

	// Token: 0x040014E3 RID: 5347
	public static Action OnBackOutOfGameplay;

	// Token: 0x040014E4 RID: 5348
	private static EndGameScreen s_instance;

	// Token: 0x040014E5 RID: 5349
	private bool m_shown;

	// Token: 0x040014E6 RID: 5350
	private bool m_netCacheReady;

	// Token: 0x040014E7 RID: 5351
	private bool m_achievesReady;

	// Token: 0x040014E8 RID: 5352
	private bool m_heroRewardEventReady;

	// Token: 0x040014E9 RID: 5353
	protected global::Achievement m_heroRewardAchievement;

	// Token: 0x040014EA RID: 5354
	protected List<global::Achievement> m_completedQuests = new List<global::Achievement>();

	// Token: 0x040014EB RID: 5355
	private bool m_isShowingFixedRewards;

	// Token: 0x040014EC RID: 5356
	private List<Reward> m_rewards = new List<Reward>();

	// Token: 0x040014ED RID: 5357
	private int m_numRewardsToLoad;

	// Token: 0x040014EE RID: 5358
	private bool m_rewardsLoaded;

	// Token: 0x040014EF RID: 5359
	private List<Reward> m_genericRewards = new List<Reward>();

	// Token: 0x040014F0 RID: 5360
	private HashSet<long> m_genericRewardChestNoticeIdsReady = new HashSet<long>();

	// Token: 0x040014F1 RID: 5361
	private Reward m_currentlyShowingReward;

	// Token: 0x040014F2 RID: 5362
	private bool m_haveShownTwoScoop;

	// Token: 0x040014F3 RID: 5363
	private bool m_hasAlreadySetMode;

	// Token: 0x040014F4 RID: 5364
	private bool m_playingBlockingAnim;

	// Token: 0x040014F5 RID: 5365
	private bool m_doneDisplayingRewards;

	// Token: 0x040014F6 RID: 5366
	private bool m_showingScoreScreen;

	// Token: 0x040014F7 RID: 5367
	private ScoreScreen m_scoreScreen;

	// Token: 0x040014F8 RID: 5368
	private GameObject m_rankChangeTwoScoop;

	// Token: 0x040014F9 RID: 5369
	private bool m_rankChangeReady;

	// Token: 0x040014FA RID: 5370
	private bool m_medalInfoUpdated;

	// Token: 0x040014FB RID: 5371
	private const int MEDAL_INFO_RETRY_COUNT_MAX = 3;

	// Token: 0x040014FC RID: 5372
	private const float MEDAL_INFO_RETRY_INITIAL_DELAY = 1f;

	// Token: 0x040014FD RID: 5373
	private int m_medalInfoRetryCount;

	// Token: 0x040014FE RID: 5374
	private float m_medalInfoRetryDelay;

	// Token: 0x040014FF RID: 5375
	private bool m_shouldShowRankChange;

	// Token: 0x04001500 RID: 5376
	private bool m_isShowingRankChange;

	// Token: 0x04001501 RID: 5377
	private bool m_hasSentRankedInitTelemetry;

	// Token: 0x04001502 RID: 5378
	private float m_endGameScreenStartTime;

	// Token: 0x04001503 RID: 5379
	private Widget m_rankedRewardDisplayWidget;

	// Token: 0x04001504 RID: 5380
	private RankedRewardDisplay m_rankedRewardDisplay;

	// Token: 0x04001505 RID: 5381
	private bool m_isShowingRankedReward;

	// Token: 0x04001506 RID: 5382
	private List<List<RewardData>> m_rankedRewardsToDisplay = new List<List<RewardData>>();

	// Token: 0x04001507 RID: 5383
	private Widget m_rankedCardBackProgressWidget;

	// Token: 0x04001508 RID: 5384
	private RankedCardBackProgressDisplay m_rankedCardBackProgress;

	// Token: 0x04001509 RID: 5385
	private bool m_shouldShowRankedCardBackProgress;

	// Token: 0x0400150A RID: 5386
	private bool m_isShowingRankedCardBackProgress;

	// Token: 0x0400150B RID: 5387
	private bool m_isShowingTrackRewards;

	// Token: 0x0400150C RID: 5388
	private bool m_shouldShowRewardXpGains;

	// Token: 0x020015D7 RID: 5591
	// (Invoke) Token: 0x0600E1DA RID: 57818
	public delegate void OnTwoScoopsShownHandler(bool shown, EndGameTwoScoop twoScoops);
}
