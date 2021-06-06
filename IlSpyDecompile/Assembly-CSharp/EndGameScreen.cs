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

[CustomEditClass]
public class EndGameScreen : MonoBehaviour
{
	public delegate void OnTwoScoopsShownHandler(bool shown, EndGameTwoScoop twoScoops);

	public EndGameTwoScoop m_twoScoop;

	public PegUIElement m_hitbox;

	public UberText m_noGoldRewardText;

	public UberText m_continueText;

	[CustomEditField(T = EditType.GAME_OBJECT)]
	public string m_ScoreScreenPrefab;

	public static OnTwoScoopsShownHandler OnTwoScoopsShown;

	public static Action OnBackOutOfGameplay;

	private static EndGameScreen s_instance;

	private bool m_shown;

	private bool m_netCacheReady;

	private bool m_achievesReady;

	private bool m_heroRewardEventReady;

	protected Achievement m_heroRewardAchievement;

	protected List<Achievement> m_completedQuests = new List<Achievement>();

	private bool m_isShowingFixedRewards;

	private List<Reward> m_rewards = new List<Reward>();

	private int m_numRewardsToLoad;

	private bool m_rewardsLoaded;

	private List<Reward> m_genericRewards = new List<Reward>();

	private HashSet<long> m_genericRewardChestNoticeIdsReady = new HashSet<long>();

	private Reward m_currentlyShowingReward;

	private bool m_haveShownTwoScoop;

	private bool m_hasAlreadySetMode;

	private bool m_playingBlockingAnim;

	private bool m_doneDisplayingRewards;

	private bool m_showingScoreScreen;

	private ScoreScreen m_scoreScreen;

	private GameObject m_rankChangeTwoScoop;

	private bool m_rankChangeReady;

	private bool m_medalInfoUpdated;

	private const int MEDAL_INFO_RETRY_COUNT_MAX = 3;

	private const float MEDAL_INFO_RETRY_INITIAL_DELAY = 1f;

	private int m_medalInfoRetryCount;

	private float m_medalInfoRetryDelay;

	private bool m_shouldShowRankChange;

	private bool m_isShowingRankChange;

	private bool m_hasSentRankedInitTelemetry;

	private float m_endGameScreenStartTime;

	private Widget m_rankedRewardDisplayWidget;

	private RankedRewardDisplay m_rankedRewardDisplay;

	private bool m_isShowingRankedReward;

	private List<List<RewardData>> m_rankedRewardsToDisplay = new List<List<RewardData>>();

	private Widget m_rankedCardBackProgressWidget;

	private RankedCardBackProgressDisplay m_rankedCardBackProgress;

	private bool m_shouldShowRankedCardBackProgress;

	private bool m_isShowingRankedCardBackProgress;

	private bool m_isShowingTrackRewards;

	private bool m_shouldShowRewardXpGains;

	protected virtual void Awake()
	{
		s_instance = this;
		StartCoroutine(WaitForAchieveManager());
		ProcessPreviousAchievements();
		AchieveManager.Get().RegisterAchievesUpdatedListener(OnAchievesUpdated);
		AchieveManager.Get().CheckPlayedNearbyPlayerOnSubnet();
		m_shouldShowRankChange = !GameMgr.Get().IsSpectator() && GameMgr.Get().IsPlay() && Options.Get().GetBool(Option.IN_RANKED_PLAY_MODE);
		m_hitbox.gameObject.SetActive(value: false);
		string key = "GLOBAL_CLICK_TO_CONTINUE";
		if (UniversalInputManager.Get().IsTouchMode())
		{
			key = "GLOBAL_CLICK_TO_CONTINUE_TOUCH";
		}
		m_continueText.Text = GameStrings.Get(key);
		m_continueText.gameObject.SetActive(value: false);
		m_noGoldRewardText.gameObject.SetActive(value: false);
		PegUI.Get().AddInputCamera(CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects));
		SceneUtils.SetLayer(m_hitbox.gameObject, GameLayer.IgnoreFullScreenEffects);
		SceneUtils.SetLayer(m_continueText.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (!Network.ShouldBeConnectedToAurora())
		{
			UpdateRewards();
		}
		m_genericRewardChestNoticeIdsReady = GenericRewardChestNoticeManager.Get().GetReadyGenericRewardChestNotices();
		GenericRewardChestNoticeManager.Get().RegisterRewardsUpdatedListener(OnGenericRewardUpdated);
	}

	protected virtual void OnDestroy()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheMedalInfo), OnMedalInfoUpdate);
		}
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects));
		}
		if (OnTwoScoopsShown != null)
		{
			OnTwoScoopsShown(shown: false, m_twoScoop);
		}
		if (AchieveManager.Get() != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(OnAchievesUpdated);
		}
		if (GenericRewardChestNoticeManager.Get() != null)
		{
			GenericRewardChestNoticeManager.Get().RemoveRewardsUpdatedListener(OnGenericRewardUpdated);
		}
		s_instance = null;
	}

	public static EndGameScreen Get()
	{
		return s_instance;
	}

	public virtual void Show()
	{
		if (GameState.Get() == null || !GameState.Get().WasRestartRequested())
		{
			m_shown = true;
			m_endGameScreenStartTime = Time.time;
			Network.Get().DisconnectFromGameServer();
			InputManager.Get().DisableInput();
			m_hitbox.gameObject.SetActive(value: true);
			FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
			FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc);
			if (GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null)
			{
				GameState.Get().GetFriendlySidePlayer().GetHandZone()
					.UpdateLayout(null);
			}
			ShowScoreScreen();
			ShowStandardFlowIfReady();
		}
	}

	public void SetPlayingBlockingAnim(bool set)
	{
		m_playingBlockingAnim = set;
	}

	public bool IsPlayingBlockingAnim()
	{
		return m_playingBlockingAnim;
	}

	public bool IsScoreScreenShown()
	{
		return m_showingScoreScreen;
	}

	private void ShowTutorialProgress()
	{
		HideTwoScoop();
		StartCoroutine(LoadTutorialProgress());
	}

	private IEnumerator LoadTutorialProgress()
	{
		yield return new WaitForSeconds(0.25f);
		AssetLoader.Get().InstantiatePrefab("TutorialProgressScreen.prefab:a78bac9caa971494ea8fac23dc1a9bd8", OnTutorialProgressScreenCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnTutorialProgressScreenCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.parent = base.transform;
		go.GetComponent<TutorialProgressScreen>().StartTutorialProgress();
	}

	protected void ContinueButtonPress_Common()
	{
		LoadingScreen.Get().AddTransitionObject(this);
	}

	protected void ContinueButtonPress_ProceedToError(UIEvent e)
	{
		if (!IsPlayingBlockingAnim())
		{
			HideScoreScreen();
			m_hitbox.RemoveEventListener(UIEventType.RELEASE, ContinueButtonPress_ProceedToError);
		}
	}

	protected void ContinueButtonPress_PrevMode(UIEvent e)
	{
		ContinueEvents();
	}

	public bool ContinueEvents()
	{
		if (ContinueDefaultEvents())
		{
			return true;
		}
		if (m_twoScoop == null)
		{
			return false;
		}
		PlayMakerFSM component = m_twoScoop.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SendEvent("Death");
		}
		ContinueButtonPress_Common();
		m_hitbox.RemoveEventListener(UIEventType.RELEASE, ContinueButtonPress_PrevMode);
		ReturnToPreviousMode();
		return false;
	}

	protected void ContinueButtonPress_TutorialProgress(UIEvent e)
	{
		ContinueTutorialEvents();
	}

	public void ContinueTutorialEvents()
	{
		if (!ContinueDefaultEvents())
		{
			ContinueButtonPress_Common();
			m_hitbox.RemoveEventListener(UIEventType.RELEASE, ContinueButtonPress_TutorialProgress);
			m_continueText.gameObject.SetActive(value: false);
			ShowTutorialProgress();
		}
	}

	private bool ContinueDefaultEvents()
	{
		if (IsPlayingBlockingAnim())
		{
			return true;
		}
		if (m_currentlyShowingReward != null)
		{
			m_currentlyShowingReward.Hide(animate: true);
			m_currentlyShowingReward = null;
		}
		HideScoreScreen();
		if (!m_haveShownTwoScoop)
		{
			return true;
		}
		HideTwoScoop();
		if (ShowHeroRewardEvent() && m_heroRewardEventReady)
		{
			return true;
		}
		if (ShowRewardTrackXpGains())
		{
			return true;
		}
		if (ShowNextRewardTrackAutoClaimedReward())
		{
			return true;
		}
		if (ShowFixedRewards())
		{
			return true;
		}
		if (ShowGoldReward())
		{
			return true;
		}
		if (ShowRankedCardBackProgress())
		{
			return true;
		}
		if (ShowRankChange())
		{
			return true;
		}
		if (ShowRankedRewards())
		{
			return true;
		}
		if (ShowNextProgressionQuestReward())
		{
			return true;
		}
		if (ShowNextCompletedQuest())
		{
			return true;
		}
		if (ShowNextReward())
		{
			return true;
		}
		if (ShowNextGenericReward())
		{
			return true;
		}
		if (!SpectatorManager.Get().IsSpectatingOrWatching && TemporaryAccountManager.IsTemporaryAccount() && ShowHealUpDialog())
		{
			return true;
		}
		if (ShowPushNotificationPrompt())
		{
			return true;
		}
		if (ShowAppRatingPrompt())
		{
			return true;
		}
		m_doneDisplayingRewards = true;
		return false;
	}

	protected virtual void OnTwoScoopShown()
	{
	}

	protected virtual void OnTwoScoopHidden()
	{
	}

	protected virtual void InitGoldRewardUI()
	{
	}

	protected virtual void InitVictoryGoldRewardUI(GamesWonIndicator gamesWonIndicator)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		int num = netObject?.MaxGoldPerDay ?? 100;
		int num2 = netObject?.GoldPerReward ?? 10;
		int num3 = netObject?.WinsPerGold ?? 3;
		string text = GetFriendlyChallengeRewardText();
		if (string.IsNullOrEmpty(text))
		{
			TAG_GOLD_REWARD_STATE tAG_GOLD_REWARD_STATE = GameState.Get().GetFriendlySidePlayer().GetTag<TAG_GOLD_REWARD_STATE>(GAME_TAG.GOLD_REWARD_STATE);
			if (tAG_GOLD_REWARD_STATE != TAG_GOLD_REWARD_STATE.ELIGIBLE)
			{
				Log.Gameplay.Print($"EngGameScreen.InitVictoryGoldRewardUI(): goldRewardState = {tAG_GOLD_REWARD_STATE}");
				switch (tAG_GOLD_REWARD_STATE)
				{
				default:
					return;
				case TAG_GOLD_REWARD_STATE.ALREADY_CAPPED:
					text = GameStrings.Format("GLOBAL_GOLD_REWARD_ALREADY_CAPPED", num);
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
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			m_noGoldRewardText.gameObject.SetActive(value: true);
			m_noGoldRewardText.Text = text;
			return;
		}
		NetCache.NetCacheGamesPlayed netObject2 = NetCache.Get().GetNetObject<NetCache.NetCacheGamesPlayed>();
		if (netObject2 != null)
		{
			Log.Gameplay.Print($"EndGameTwoScoop.UpdateData(): {netObject2.FreeRewardProgress}/{num3} wins towards {num2} gold");
			gamesWonIndicator.Init(Reward.Type.GOLD, num2, num3, netObject2.FreeRewardProgress, GamesWonIndicator.InnKeeperTrigger.NONE);
		}
	}

	private static string GetFriendlyChallengeRewardMessage(Achievement achieve)
	{
		if (DemoMgr.Get().IsDemo())
		{
			return null;
		}
		string text = null;
		if (achieve.DbfRecord.MaxDefense > 0)
		{
			text = GetFriendlyChallengeEarlyConcedeMessage(achieve.DbfRecord.MaxDefense);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
		}
		AchieveRegionDataDbfRecord currentRegionData = achieve.GetCurrentRegionData();
		if (currentRegionData != null && currentRegionData.RewardableLimit > 0 && achieve.IntervalRewardStartDate > 0)
		{
			DateTime dateTime = DateTime.FromFileTimeUtc(achieve.IntervalRewardStartDate);
			if ((DateTime.UtcNow - dateTime).TotalDays < currentRegionData.RewardableInterval && achieve.IntervalRewardCount >= currentRegionData.RewardableLimit)
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
			bool num = FriendChallengeMgr.Get().DidSendChallenge();
			bool flag = FriendChallengeMgr.Get().DidReceiveChallenge();
			PlayerType playerType = PlayerType.PT_ANY;
			if (num)
			{
				playerType = PlayerType.PT_FRIENDLY_CHALLENGER;
			}
			if (flag)
			{
				playerType = PlayerType.PT_FRIENDLY_CHALLENGEE;
			}
			for (int i = 0; i < partyQuestInfo.QuestIds.Count; i++)
			{
				Achievement achievement = achieveManager.GetAchievement(partyQuestInfo.QuestIds[i]);
				if (achievement != null && achievement.IsValidFriendlyPlayerChallengeType(playerType))
				{
					text = GetFriendlyChallengeRewardMessage(achievement);
				}
				if (string.IsNullOrEmpty(text))
				{
					Achievement achievement2 = achieveManager.GetAchievement(achievement.DbfRecord.SharedAchieveId);
					if (achievement2 != null && achievement2.IsValidFriendlyPlayerChallengeType(playerType))
					{
						text = GetFriendlyChallengeRewardMessage(achievement2);
					}
				}
			}
		}
		if (string.IsNullOrEmpty(text) && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, activeIfDoesNotExist: false))
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool num2 = (from a in achieveManager.GetActiveQuests()
				where a.IsAffectedByFriendWeek && (a.AchieveTrigger == Achieve.Trigger.WIN || a.AchieveTrigger == Achieve.Trigger.FINISH) && a.GameModeRequiresNonFriendlyChallenge
				select a).Any();
			bool flag2 = false;
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl() && netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate)
			{
				BrawlType challengeBrawlType = FriendChallengeMgr.Get().GetChallengeBrawlType();
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(challengeBrawlType);
				TavernBrawlPlayerRecord record = TavernBrawlManager.Get().GetRecord(challengeBrawlType);
				bool flag3 = mission != null && (mission.rewardTrigger == RewardTrigger.REWARD_TRIGGER_WIN_GAME || mission.rewardTrigger == RewardTrigger.REWARD_TRIGGER_FINISH_GAME);
				if (mission != null && mission.rewardType != RewardType.REWARD_UNKNOWN && flag3 && record != null && record.RewardProgress < mission.RewardTriggerQuota)
				{
					flag2 = true;
				}
			}
			if (!num2 && !flag2)
			{
				return null;
			}
			int concederMaxDefense = 0;
			if (netObject != null)
			{
				concederMaxDefense = netObject.FriendWeekConcederMaxDefense;
			}
			text = GetFriendlyChallengeEarlyConcedeMessage(concederMaxDefense);
		}
		return text;
	}

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
		foreach (KeyValuePair<int, Player> item in gameState.GetPlayerMap())
		{
			Player value = item.Value;
			TAG_PLAYSTATE preGameOverPlayState = value.GetPreGameOverPlayState();
			if (preGameOverPlayState == TAG_PLAYSTATE.CONCEDED || preGameOverPlayState == TAG_PLAYSTATE.DISCONNECTED)
			{
				flag = true;
				Entity hero = value.GetHero();
				if (hero != null)
				{
					num2 = hero.GetCurrentDefense();
					key = ((value.GetSide() != Player.Side.FRIENDLY) ? "GLOBAL_FRIENDLYCHALLENGE_REWARD_CONCEDED_YOUR_OPPONENT" : "GLOBAL_FRIENDLYCHALLENGE_REWARD_CONCEDED_YOURSELF");
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

	protected void BackToMode(SceneMgr.Mode mode)
	{
		AchieveManager.Get().RemoveAchievesUpdatedListener(OnAchievesUpdated);
		HideTwoScoop();
		if (OnBackOutOfGameplay != null)
		{
			OnBackOutOfGameplay();
		}
		if (!m_hasAlreadySetMode)
		{
			m_hasAlreadySetMode = true;
			StartCoroutine(ToMode(mode));
			Navigation.Clear();
		}
	}

	private IEnumerator ToMode(SceneMgr.Mode mode)
	{
		yield return new WaitForSeconds(0.5f);
		SceneMgr.Get().SetNextMode(mode);
	}

	private void ReturnToPreviousMode()
	{
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		if (postGameSceneMode == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			DuelsConfig.Get().SetLastGameResult(GameMgr.Get().LastGameData.GameResult);
		}
		BackToMode(postGameSceneMode);
	}

	private void ShowScoreScreen()
	{
		if (!GameState.Get().CanShowScoreScreen())
		{
			return;
		}
		m_scoreScreen = GameUtils.LoadGameObjectWithComponent<ScoreScreen>(m_ScoreScreenPrefab);
		if ((bool)m_scoreScreen)
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_scoreScreen.transform, base.transform);
			SceneUtils.SetLayer(m_scoreScreen, GameLayer.IgnoreFullScreenEffects);
			m_scoreScreen.Show();
			m_showingScoreScreen = true;
			SetPlayingBlockingAnim(set: true);
			StartCoroutine(WaitThenSetPlayingBlockingAnim(0.65f, set: false));
			if (Gameplay.Get().HasBattleNetFatalError())
			{
				m_hitbox.AddEventListener(UIEventType.RELEASE, ContinueButtonPress_ProceedToError);
			}
		}
	}

	private void HideScoreScreen()
	{
		if ((bool)m_scoreScreen)
		{
			m_scoreScreen.Hide();
			m_showingScoreScreen = false;
			SetPlayingBlockingAnim(set: true);
			StartCoroutine(WaitThenSetPlayingBlockingAnim(0.25f, set: false));
		}
	}

	protected void HideTwoScoop()
	{
		if (m_twoScoop.IsShown())
		{
			m_twoScoop.Hide();
			m_noGoldRewardText.gameObject.SetActive(value: false);
			OnTwoScoopHidden();
			if (OnTwoScoopsShown != null)
			{
				OnTwoScoopsShown(shown: false, m_twoScoop);
			}
			if (InputManager.Get() != null)
			{
				InputManager.Get().EnableInput();
			}
		}
	}

	protected void ShowTwoScoop()
	{
		StartCoroutine(ShowTwoScoopWhenReady());
	}

	private IEnumerator ShowTwoScoopWhenReady()
	{
		while ((bool)m_scoreScreen)
		{
			yield return null;
		}
		if (ShouldMakeUtilRequests())
		{
			while (!m_netCacheReady)
			{
				yield return null;
			}
			while (!m_achievesReady)
			{
				yield return null;
			}
		}
		while (!m_rewardsLoaded)
		{
			yield return null;
		}
		while (!m_twoScoop.IsLoaded())
		{
			yield return null;
		}
		while (JustEarnedHeroReward() && !m_heroRewardEventReady)
		{
			yield return null;
		}
		m_twoScoop.Show();
		if (!SpectatorManager.Get().IsSpectatingOrWatching && ShouldMakeUtilRequests())
		{
			InitGoldRewardUI();
		}
		OnTwoScoopShown();
		m_haveShownTwoScoop = true;
		if (OnTwoScoopsShown != null)
		{
			OnTwoScoopsShown(shown: true, m_twoScoop);
		}
	}

	protected IEnumerator WaitThenSetPlayingBlockingAnim(float sec, bool set)
	{
		yield return new WaitForSeconds(sec);
		SetPlayingBlockingAnim(set);
	}

	protected bool ShouldMakeUtilRequests()
	{
		if (!Network.ShouldBeConnectedToAurora())
		{
			return false;
		}
		return true;
	}

	protected bool IsReady()
	{
		if (m_shown && m_netCacheReady && m_achievesReady && m_rewardsLoaded && ((m_rankChangeReady && m_medalInfoUpdated) || !m_shouldShowRankChange) && (m_rankedRewardDisplay != null || m_rankedRewardsToDisplay.Count == 0) && (m_rankedCardBackProgress != null || !m_shouldShowRankedCardBackProgress))
		{
			if (!RewardXpNotificationManager.Get().IsReady)
			{
				return !m_shouldShowRewardXpGains;
			}
			return true;
		}
		return false;
	}

	public bool IsDoneDisplayingRewards()
	{
		return m_doneDisplayingRewards;
	}

	private bool ShowStandardFlowIfReady()
	{
		if (!IsReady() && (ShouldMakeUtilRequests() || !m_shown))
		{
			return false;
		}
		SendRankedInitTelemetryIfNeeded();
		ShowStandardFlow();
		return true;
	}

	protected virtual void ShowStandardFlow()
	{
		ShowTwoScoop();
		if (RewardXpNotificationManager.Get().HasXpGainsToShow)
		{
			RewardXpNotificationManager.Get().ShowRewardTrackXpGains(delegate
			{
				ContinueEvents();
			}, justShowGameXp: true);
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_continueText.gameObject.SetActive(value: true);
		}
	}

	protected virtual void OnNetCacheReady()
	{
		m_netCacheReady = true;
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		if (RewardXpNotificationManager.Get().HasXpGainsToShow)
		{
			m_shouldShowRewardXpGains = true;
			RewardXpNotificationManager.Get().InitEndOfGameFlow(delegate
			{
				ShowStandardFlowIfReady();
			});
		}
		if (m_shouldShowRankChange)
		{
			RetryMedalInfoRequestIfNeeded();
			LoadRankChange();
			LoadRankedRewardDisplay();
			LoadRankedCardBackProgress();
		}
		MaybeUpdateRewards();
	}

	private void RetryMedalInfoRequestIfNeeded()
	{
		if (IsMedalInfoRetryNeeded())
		{
			StartCoroutine(RetryMedalInfoRequest());
		}
		else
		{
			NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheMedalInfo), OnMedalInfoUpdate);
			m_medalInfoUpdated = true;
		}
		ShowStandardFlowIfReady();
	}

	private bool IsMedalInfoRetryNeeded()
	{
		if (!ShouldMakeUtilRequests())
		{
			return false;
		}
		if (!m_shouldShowRankChange)
		{
			return false;
		}
		if (m_medalInfoRetryCount >= 3)
		{
			return false;
		}
		FormatType formatType = Options.GetFormatType();
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		if (localPlayerMedalInfo != null)
		{
			return localPlayerMedalInfo.GetChangeType(formatType) == RankChangeType.NO_GAME_PLAYED;
		}
		return true;
	}

	private IEnumerator RetryMedalInfoRequest()
	{
		if (m_medalInfoRetryCount == 0)
		{
			m_medalInfoRetryDelay = 1f;
			NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheMedalInfo), OnMedalInfoUpdate);
		}
		else
		{
			m_medalInfoRetryDelay *= 2f;
		}
		m_medalInfoRetryCount++;
		yield return new WaitForSeconds(m_medalInfoRetryDelay);
		NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
	}

	private void OnMedalInfoUpdate()
	{
		RetryMedalInfoRequestIfNeeded();
	}

	private void SendRankedInitTelemetryIfNeeded()
	{
		if (m_shouldShowRankChange && !m_hasSentRankedInitTelemetry)
		{
			m_hasSentRankedInitTelemetry = true;
			float num = Time.time - m_endGameScreenStartTime;
			FormatType formatType = Options.GetFormatType();
			MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
			bool flag = m_medalInfoRetryCount >= 3 && (localPlayerMedalInfo == null || localPlayerMedalInfo.GetChangeType(formatType) == RankChangeType.NO_GAME_PLAYED);
			if (flag && localPlayerMedalInfo != null)
			{
				Log.All.PrintError("EndGameScreen_MedalInfoTimeOut elapsedTime={0} retries={1} prev={2} curr={3}", num, m_medalInfoRetryCount, localPlayerMedalInfo.GetPreviousMedal(formatType).ToString(), localPlayerMedalInfo.GetCurrentMedal(formatType).ToString());
			}
			bool showRankedReward = m_rankedRewardsToDisplay.Count > 0;
			TelemetryManager.Client().SendEndGameScreenInit(num, m_medalInfoRetryCount, flag, showRankedReward, m_shouldShowRankedCardBackProgress, m_rewards.Count);
		}
	}

	private void LoadRankChange()
	{
		AssetReference rANK_CHANGE_TWO_SCOOP_PREFAB_NEW = RankMgr.RANK_CHANGE_TWO_SCOOP_PREFAB_NEW;
		AssetLoader.Get().InstantiatePrefab(rANK_CHANGE_TWO_SCOOP_PREFAB_NEW, OnRankChangeLoaded);
	}

	private void OnRankChangeLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_rankChangeTwoScoop = go;
		m_rankChangeTwoScoop.gameObject.SetActive(value: false);
		m_rankChangeReady = true;
		ShowStandardFlowIfReady();
	}

	private void OnRankChangeClosed()
	{
		m_isShowingRankChange = false;
		m_shouldShowRankChange = false;
		ContinueEvents();
	}

	private void LoadRankedRewardDisplay()
	{
		if (RankMgr.Get().GetLocalPlayerMedalInfo().GetRankedRewardsEarned(Options.GetFormatType(), ref m_rankedRewardsToDisplay) && m_rankedRewardsToDisplay.Count != 0)
		{
			m_rankedRewardDisplayWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_DISPLAY_PREFAB);
			m_rankedRewardDisplayWidget.RegisterReadyListener(delegate
			{
				OnRankedRewardDisplayWidgetReady();
			});
		}
	}

	private void OnRankedRewardDisplayWidgetReady()
	{
		m_rankedRewardDisplay = m_rankedRewardDisplayWidget.GetComponentInChildren<RankedRewardDisplay>();
		ShowStandardFlowIfReady();
	}

	private void LoadRankedCardBackProgress()
	{
		m_shouldShowRankedCardBackProgress = RankMgr.Get().GetLocalPlayerMedalInfo().ShouldShowCardBackProgress();
		if (m_shouldShowRankedCardBackProgress)
		{
			m_rankedCardBackProgressWidget = WidgetInstance.Create(RankMgr.RANKED_CARDBACK_PROGRESS_DISPLAY_PREFAB);
			m_rankedCardBackProgressWidget.RegisterReadyListener(delegate
			{
				OnRankedCardBackProgressWidgetReady();
			});
		}
	}

	private void OnRankedCardBackProgressWidgetReady()
	{
		m_rankedCardBackProgress = m_rankedCardBackProgressWidget.GetComponentInChildren<RankedCardBackProgressDisplay>();
		ShowStandardFlowIfReady();
	}

	private IEnumerator WaitForAchieveManager()
	{
		while (!AchieveManager.Get().IsReady())
		{
			yield return null;
		}
		m_achievesReady = true;
		MaybeUpdateRewards();
	}

	private void ProcessPreviousAchievements()
	{
		OnAchievesUpdated(new List<Achievement>(), new List<Achievement>(), null);
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userData)
	{
		if (!GameUtils.AreAllTutorialsComplete())
		{
			return;
		}
		foreach (Achievement achieve in AchieveManager.Get().GetNewCompletedAchievesToShow())
		{
			if (achieve.RewardTiming == Achieve.RewardTiming.IMMEDIATE && m_completedQuests.Find((Achievement obj) => achieve.ID == obj.ID) == null && (m_heroRewardAchievement == null || m_heroRewardAchievement.ID != achieve.ID))
			{
				m_completedQuests.Add(achieve);
			}
		}
	}

	private void OnGenericRewardUpdated(long rewardNoticeId, object userData)
	{
		m_genericRewardChestNoticeIdsReady.Add(rewardNoticeId);
		UpdateRewards();
	}

	protected bool HasShownScoops()
	{
		return m_haveShownTwoScoop;
	}

	protected void SetHeroRewardEventReady(bool isReady)
	{
		m_heroRewardEventReady = isReady;
	}

	private void MaybeUpdateRewards()
	{
		if (m_achievesReady && m_netCacheReady)
		{
			UpdateRewards();
			ShowStandardFlowIfReady();
		}
	}

	private void LoadRewards(List<RewardData> rewardsToLoad, Reward.DelOnRewardLoaded callback)
	{
		if (rewardsToLoad == null)
		{
			return;
		}
		foreach (RewardData item in rewardsToLoad)
		{
			if (PopupDisplayManager.Get().UpdateNoticesSeen(item))
			{
				m_numRewardsToLoad++;
				item.LoadRewardObject(callback);
			}
		}
	}

	private void UpdateRewards()
	{
		bool flag = true;
		if (GameMgr.Get().IsTutorial())
		{
			flag = GameUtils.AreAllTutorialsComplete();
		}
		List<RewardData> rewardsToShow = null;
		List<RewardData> genericRewardChestsToShow = null;
		List<RewardData> purchasedCardRewardsToShow = null;
		if (flag)
		{
			List<NetCache.ProfileNotice> list = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices.Where((NetCache.ProfileNotice n) => n.Type != NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST || m_genericRewardChestNoticeIdsReady.Any((long r) => n.NoticeID == r)).ToList();
			list.RemoveAll((NetCache.ProfileNotice n) => n.Origin == NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS);
			List<RewardData> rewards = RewardUtils.GetRewards(list);
			HashSet<Achieve.RewardTiming> rewardTimings = new HashSet<Achieve.RewardTiming> { Achieve.RewardTiming.IMMEDIATE };
			RewardUtils.GetViewableRewards(rewards, rewardTimings, out rewardsToShow, out genericRewardChestsToShow, ref purchasedCardRewardsToShow, ref m_completedQuests);
		}
		else
		{
			rewardsToShow = new List<RewardData>();
		}
		JustEarnedHeroReward();
		if (!GameMgr.Get().IsSpectator())
		{
			List<RewardData> customRewards = GameState.Get().GetGameEntity().GetCustomRewards();
			if (customRewards != null)
			{
				rewardsToShow.AddRange(customRewards);
			}
		}
		LoadRewards(rewardsToShow, OnRewardObjectLoaded);
		LoadRewards(genericRewardChestsToShow, OnGenericRewardObjectLoaded);
		if (m_numRewardsToLoad == 0)
		{
			m_rewardsLoaded = true;
		}
	}

	private void OnRewardObjectLoaded(Reward reward, object callbackData)
	{
		LoadReward(reward, ref m_rewards);
	}

	private void OnGenericRewardObjectLoaded(Reward reward, object callbackData)
	{
		LoadReward(reward, ref m_genericRewards);
	}

	private void PositionReward(Reward reward)
	{
		reward.transform.parent = base.transform;
		reward.transform.localRotation = Quaternion.identity;
		reward.transform.localPosition = PopupDisplayManager.Get().GetRewardLocalPos();
	}

	private void LoadReward(Reward reward, ref List<Reward> allRewards)
	{
		reward.Hide();
		PositionReward(reward);
		allRewards.Add(reward);
		m_numRewardsToLoad--;
		if (m_numRewardsToLoad <= 0)
		{
			RewardUtils.SortRewards(ref allRewards);
			m_rewardsLoaded = true;
			ShowStandardFlowIfReady();
		}
	}

	private void DisplayLoadedRewardObject(Reward reward, object callbackData)
	{
		if (m_currentlyShowingReward != null)
		{
			m_currentlyShowingReward.Hide(animate: true);
			m_currentlyShowingReward = null;
		}
		reward.Hide();
		PositionReward(reward);
		m_currentlyShowingReward = reward;
		SetPlayingBlockingAnim(set: true);
		SceneUtils.SetLayer(m_currentlyShowingReward.gameObject, GameLayer.IgnoreFullScreenEffects);
		ShowReward(m_currentlyShowingReward);
	}

	private void ShowReward(Reward reward)
	{
		bool updateCacheValues = !(reward is CardReward);
		RewardUtils.ShowReward(UserAttentionBlocker.NONE, reward, updateCacheValues, PopupDisplayManager.Get().GetRewardPunchScale(), PopupDisplayManager.Get().GetRewardScale());
		StartCoroutine(WaitThenSetPlayingBlockingAnim(0.35f, set: false));
	}

	protected virtual bool ShowHeroRewardEvent()
	{
		return false;
	}

	protected bool ShowFixedRewards()
	{
		if (m_isShowingFixedRewards)
		{
			return true;
		}
		HashSet<Achieve.RewardTiming> rewardVisualTimings = new HashSet<Achieve.RewardTiming> { Achieve.RewardTiming.IMMEDIATE };
		FixedRewardsMgr.DelOnAllFixedRewardsShown allRewardsShownCallback = delegate
		{
			m_isShowingFixedRewards = false;
			ContinueEvents();
		};
		m_isShowingFixedRewards = FixedRewardsMgr.Get().ShowFixedRewards(UserAttentionBlocker.NONE, rewardVisualTimings, allRewardsShownCallback, null);
		return m_isShowingFixedRewards;
	}

	private bool ShowGoldReward()
	{
		GoldRewardData goldRewardData;
		int num = m_rewards.FindIndex((Reward reward) => ((goldRewardData = reward.Data as GoldRewardData) != null && goldRewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.TOURNEY) ? true : false);
		if (num < 0)
		{
			return false;
		}
		Reward item = m_rewards[num];
		m_rewards.RemoveAt(num);
		m_rewards.Insert(0, item);
		ShowNextReward();
		return true;
	}

	private bool ShowNextProgressionQuestReward()
	{
		if (!QuestManager.Get().ShowNextReward(delegate
		{
			ContinueEvents();
		}))
		{
			return false;
		}
		return true;
	}

	protected bool ShowNextCompletedQuest()
	{
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (m_completedQuests.Count == 0)
		{
			return false;
		}
		Achievement achievement = m_completedQuests[0];
		m_completedQuests.RemoveAt(0);
		if (!achievement.UseGenericRewardVisual)
		{
			bool flag = false;
			foreach (RewardData reward in achievement.Rewards)
			{
				if (reward.RewardType == Reward.Type.CARD)
				{
					CardRewardData cardRewardData = reward as CardRewardData;
					if (cardRewardData != null)
					{
						TAG_CARD_SET cardSetFromCardID = GameUtils.GetCardSetFromCardID(cardRewardData.CardID);
						flag |= !GameDbf.GetIndex().GetCardSet(cardSetFromCardID).IsCoreCardSet;
					}
				}
			}
			bool updateCacheValues = !flag;
			QuestToast.ShowQuestToast(UserAttentionBlocker.NONE, ShowQuestToastCallback, updateCacheValues, achievement);
			NarrativeManager.Get().OnQuestCompleteShown(achievement.ID);
		}
		else
		{
			achievement.AckCurrentProgressAndRewardNotices();
			achievement.Rewards[0].LoadRewardObject(DisplayLoadedRewardObject);
		}
		return true;
	}

	protected void ShowQuestToastCallback(object userData)
	{
		if (!(this == null))
		{
			ContinueEvents();
		}
	}

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
				ContinueEvents();
			});
		}
		return true;
	}

	protected bool ShowNextRewardTrackAutoClaimedReward()
	{
		if (m_isShowingTrackRewards)
		{
			return true;
		}
		Action callback = delegate
		{
			m_isShowingTrackRewards = false;
			ContinueEvents();
		};
		if (!RewardTrackManager.Get().ShowNextReward(callback))
		{
			return false;
		}
		m_isShowingTrackRewards = true;
		return true;
	}

	protected bool ShowNextReward()
	{
		if (m_rewards.Count == 0)
		{
			return false;
		}
		SetPlayingBlockingAnim(set: true);
		m_currentlyShowingReward = m_rewards[0];
		m_rewards.RemoveAt(0);
		ShowReward(m_currentlyShowingReward);
		return true;
	}

	protected bool ShowNextGenericReward()
	{
		if (m_genericRewards.Count == 0)
		{
			return false;
		}
		SetPlayingBlockingAnim(set: true);
		m_currentlyShowingReward = m_genericRewards[0];
		m_genericRewards.RemoveAt(0);
		QuestToast.ShowGenericRewardQuestToast(UserAttentionBlocker.NONE, ShowQuestToastCallback, m_currentlyShowingReward.Data, m_currentlyShowingReward.Data.NameOverride, m_currentlyShowingReward.Data.DescriptionOverride);
		StartCoroutine(WaitThenSetPlayingBlockingAnim(0.35f, set: false));
		return true;
	}

	private bool ShowRankChange()
	{
		if (!m_shouldShowRankChange)
		{
			return false;
		}
		if (m_isShowingRankChange)
		{
			return true;
		}
		m_rankChangeTwoScoop.gameObject.SetActive(value: true);
		RankChangeTwoScoop_NEW component = m_rankChangeTwoScoop.GetComponent<RankChangeTwoScoop_NEW>();
		component.Initialize(RankMgr.Get().GetLocalPlayerMedalInfo(), Options.GetFormatType(), OnRankChangeClosed);
		component.Show();
		m_isShowingRankChange = true;
		return true;
	}

	private bool ShowRankedRewards()
	{
		if (m_rankedRewardsToDisplay.Count == 0)
		{
			return false;
		}
		if (m_isShowingRankedReward)
		{
			return true;
		}
		m_isShowingRankedReward = true;
		FormatType formatType = Options.GetFormatType();
		TranslatedMedalInfo currentMedal = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedal(formatType);
		m_rankedRewardDisplay.Initialize(currentMedal, m_rankedRewardsToDisplay, OnRankedRewardsClosed);
		m_rankedRewardDisplay.Show();
		return true;
	}

	private void OnRankedRewardsClosed()
	{
		m_isShowingRankedReward = false;
		m_rankedRewardsToDisplay.Clear();
		UnityEngine.Object.Destroy(m_rankedRewardDisplayWidget.gameObject);
		ContinueEvents();
	}

	private bool ShowRankedCardBackProgress()
	{
		if (!m_shouldShowRankedCardBackProgress)
		{
			return false;
		}
		if (m_isShowingRankedCardBackProgress)
		{
			return true;
		}
		m_isShowingRankedCardBackProgress = true;
		m_rankedCardBackProgress.Initialize(RankMgr.Get().GetLocalPlayerMedalInfo(), OnRankedCardBackProgressClosed);
		m_rankedCardBackProgress.Show();
		return true;
	}

	private void OnRankedCardBackProgressClosed()
	{
		m_shouldShowRankedCardBackProgress = false;
		m_isShowingRankedCardBackProgress = false;
		UnityEngine.Object.Destroy(m_rankedCardBackProgressWidget.gameObject);
		if (FindRankedCardBackRewardAndMakeNext())
		{
			ShowNextReward();
		}
		else
		{
			ContinueEvents();
		}
	}

	private bool FindRankedCardBackRewardAndMakeNext()
	{
		int currentSeasonId = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentSeasonId();
		int rankedCardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(currentSeasonId);
		CardBackRewardData cardBackRewardData;
		int num = m_rewards.FindIndex((Reward reward) => ((cardBackRewardData = reward.Data as CardBackRewardData) != null && cardBackRewardData.CardBackID == rankedCardBackId) ? true : false);
		if (num < 0)
		{
			return false;
		}
		Reward item = m_rewards[num];
		m_rewards.RemoveAt(num);
		m_rewards.Insert(0, item);
		return true;
	}

	protected virtual bool JustEarnedHeroReward()
	{
		return false;
	}

	protected virtual bool ShowHealUpDialog()
	{
		return false;
	}

	protected virtual bool ShowPushNotificationPrompt()
	{
		return false;
	}

	protected virtual bool ShowAppRatingPrompt()
	{
		return false;
	}
}
