using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.InGameMessage.UI;
using Hearthstone.Progression;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class PopupDisplayManager : IHasUpdate, IService
{
	private enum FreeDeckStatus
	{
		IDLE,
		CHOOSING,
		WAITING_FOR_GRANT
	}

	private List<Reward> m_rewards = new List<Reward>();

	private List<Reward> m_purchasedCardRewards = new List<Reward>();

	private List<Reward> m_genericRewards = new List<Reward>();

	private List<Achievement> m_progressedAchieves = new List<Achievement>();

	private List<Achievement> m_completedAchieves = new List<Achievement>();

	private readonly HashSet<long> m_genericRewardChestNoticeIdsReady = new HashSet<long>();

	private readonly HashSet<long> m_deckRewardIds = new HashSet<long>();

	private int m_numRewardsToLoad;

	private bool m_hasShownCheatedChangedCards;

	private bool m_hasShownCheatedAddedCards;

	private readonly Dictionary<long, HashSet<int>> m_seenNotices = new Dictionary<long, HashSet<int>>();

	private bool m_isShowing;

	private bool m_readyToShowPopups;

	private bool m_hasShownMetaShakeupEventPopups;

	private bool m_hasShownCardChangePopups;

	private bool m_hasShownCardAdditionPopups;

	private bool m_hasCheckedNewPlayerSetRotationPopup;

	private long m_freeDeckNoticeIdBeingProcessed;

	private FreeDeckStatus m_freeDeckStatus;

	private bool m_shouldShowRankedIntro;

	private bool m_rankedIntroShown;

	private static bool m_hasPlayerReachedHub = false;

	private static float m_timePlayerInHubAfterLogin = 0f;

	private const float LOGIN_DURATION_THRESHOLD = 20f;

	private static string CHOOSE_A_DECK_PREFAB = "ChooseADeck.prefab:de9efdb77e14b144ea84f333a1e78926";

	private readonly Queue<NetCache.ProfileNotice> m_cardReplacementNotices = new Queue<NetCache.ProfileNotice>();

	private readonly Queue<NetCache.ProfileNotice> m_dustRewardNotices = new Queue<NetCache.ProfileNotice>();

	private PopupDisplayManagerBones ChestBones { get; set; }

	private PopupDisplayManagerBones QuestChestBones { get; set; }

	private static HashSet<Assets.Achieve.RewardTiming> CurrentRewardTimings
	{
		get
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN)
			{
				return new HashSet<Assets.Achieve.RewardTiming> { Assets.Achieve.RewardTiming.IMMEDIATE };
			}
			return new HashSet<Assets.Achieve.RewardTiming>
			{
				Assets.Achieve.RewardTiming.OUT_OF_BAND,
				Assets.Achieve.RewardTiming.IMMEDIATE
			};
		}
	}

	private bool IsLoadingRewards => m_numRewardsToLoad > 0;

	public bool IsShowing
	{
		get
		{
			if (m_isShowing)
			{
				return true;
			}
			if (DialogManager.Get() != null && DialogManager.Get().ShowingDialog())
			{
				return true;
			}
			if (WelcomeQuests.Get() != null)
			{
				return true;
			}
			if (NarrativeManager.Get() != null && NarrativeManager.Get().IsShowingBlockingDialog())
			{
				return true;
			}
			if (BannerManager.Get().IsShowing)
			{
				return true;
			}
			if (RewardXpNotificationManager.Get().IsShowingXpGains)
			{
				return true;
			}
			return false;
		}
	}

	private event Action OnAllPopupsShown = delegate
	{
	};

	private event Action<int> OnQuestCompletedShown = delegate
	{
	};

	private event Action OnPopupShown = delegate
	{
	};

	private event Action<long> OnGenericRewardShown = delegate
	{
	};

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadComponentFromResource<PopupDisplayManagerBones> loadBones2 = new LoadComponentFromResource<PopupDisplayManagerBones>("ServiceData/PopupDisplayManagerBones", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadBones2;
		ChestBones = loadBones2.LoadedComponent;
		loadBones2 = new LoadComponentFromResource<PopupDisplayManagerBones>("ServiceData/PopupDisplayManagerBonesForQuestChests", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadBones2;
		QuestChestBones = loadBones2.LoadedComponent;
		HearthstoneApplication.Get().WillReset += WillReset;
		Processor.RunCoroutine(StartupCoroutine());
		LoginManager.Get().OnFullLoginFlowComplete += InitializePlayerTimeInHubAfterLogin;
		m_timePlayerInHubAfterLogin = 0f;
		m_hasPlayerReachedHub = false;
	}

	public Type[] GetDependencies()
	{
		return new Type[4]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(AchieveManager),
			typeof(ReturningPlayerMgr)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<LoginManager>(out var service))
		{
			service.OnFullLoginFlowComplete -= InitializePlayerTimeInHubAfterLogin;
		}
		HearthstoneApplication.Get().WillReset -= WillReset;
	}

	private IEnumerator StartupCoroutine()
	{
		AchieveManager.Get().RegisterAchievesUpdatedListener(OnAchievesUpdated);
		GenericRewardChestNoticeManager.Get().RegisterRewardsUpdatedListener(OnGenericRewardUpdated);
		NetCache.Get().RegisterNewNoticesListener(OnNewNotices);
		Network.Get().RegisterNetHandler(GetDeckContentsResponse.PacketID.ID, OnGetDeckContentsResponse);
		Network.Get().RegisterNetHandler(FreeDeckChoiceResponse.PacketID.ID, OnFreeDeckChoiceResponse);
		yield break;
	}

	public void Update()
	{
		if (!m_readyToShowPopups)
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		ShowQuestProgressToasts();
		if (GameUtils.IsAnyTransitionActive() || IsShowing)
		{
			return;
		}
		if (!m_hasCheckedNewPlayerSetRotationPopup)
		{
			m_hasCheckedNewPlayerSetRotationPopup = true;
			if (SetRotationManager.ShowNewPlayerSetRotationPopupIfNeeded())
			{
				return;
			}
		}
		if (ReturningPlayerMgr.Get().ShowReturningPlayerWelcomeBannerIfNeeded(OnPopupClosed))
		{
			this.OnPopupShown();
			m_isShowing = true;
			return;
		}
		if (FiresideGatheringManager.Get().ShowSignIfNeeded(OnPopupClosed))
		{
			this.OnPopupShown();
			m_isShowing = true;
			return;
		}
		if (ShouldDisableNotificationOnLogin())
		{
			BannerManager.Get().AutoAcknowledgeOutstandingBanner();
		}
		else if (BannerManager.Get().ShowOutstandingBannerEvent(OnPopupClosed))
		{
			this.OnPopupShown();
			m_isShowing = true;
			return;
		}
		if (DraftManager.Get().ShowNextArenaPopup(OnPopupClosed))
		{
			this.OnPopupShown();
			m_isShowing = true;
		}
		else if (!m_hasShownCardAdditionPopups && ShowAddedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			m_hasShownCardAdditionPopups = true;
		}
		else if (!m_hasShownCardChangePopups && ShowChangedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			m_hasShownCardChangePopups = true;
		}
		else if (!m_hasShownMetaShakeupEventPopups && ShowLoginPopupSequence())
		{
			m_hasShownMetaShakeupEventPopups = true;
		}
		else if (!ShowNextTavernBrawlReward() && !ShowNextLeaguePromotionReward() && !ShowNextRankedIntro() && !ShowNextFreeDeckReward() && !ShowNextSellableDeckReward() && !ShowNextQuestChestReward() && !ShowNextDuelsReward() && !ShowNextProgressionAchievementReward() && !ShowNextProgressionQuestReward() && !ShowNextProgressionTrackReward() && !ShowRewardTrackXpGains() && !ShowRewardTrackSeasonRoll() && ((m_completedAchieves.Count <= 0 && m_rewards.Count <= 0 && m_purchasedCardRewards.Count <= 0 && m_genericRewards.Count <= 0) || (!ShowNextCompletedQuest() && !ShowNextUnAckedReward() && !ShowNextUnAckedGenericReward() && !ShowNextUnAckedPurchasedCardReward())) && !ShowFixedRewards(CurrentRewardTimings) && !IsLoadingRewards && !ShowNextQuestNotification() && !ShowWelcomeQuests() && !ShowInGameMessagePopups())
		{
			NarrativeManager.Get().OnAllPopupsShown();
			if (!IsShowing)
			{
				this.OnAllPopupsShown();
				ClearAllPopupsShownListeners();
			}
		}
	}

	public static PopupDisplayManager Get()
	{
		return HearthstoneServices.Get<PopupDisplayManager>();
	}

	private void WillReset()
	{
		m_readyToShowPopups = false;
		m_isShowing = false;
		if (HearthstoneServices.TryGet<LoginManager>(out var service))
		{
			service.OnFullLoginFlowComplete -= InitializePlayerTimeInHubAfterLogin;
		}
		if (HearthstoneServices.TryGet<UniversalInputManager>(out var service2))
		{
			service2.SetGameDialogActive(active: false);
		}
		DialogManager dialogManager = DialogManager.Get();
		if (dialogManager != null)
		{
			dialogManager.ReadyForSeasonEndPopup(ready: false);
			dialogManager.ClearHandledMedalNotices();
		}
		m_cardReplacementNotices.Clear();
		m_dustRewardNotices.Clear();
		ClearSeenNotices();
		ClearAllPopupsShownListeners();
	}

	public void ClearSeenNotices()
	{
		m_seenNotices.Clear();
	}

	public bool UpdateNoticesSeen(RewardData rewardData)
	{
		if (!rewardData.HasNotices())
		{
			return true;
		}
		bool result = false;
		foreach (long noticeID in rewardData.GetNoticeIDs())
		{
			if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE && rewardData.RewardChestBagNum.HasValue)
			{
				if (!m_seenNotices.ContainsKey(noticeID))
				{
					m_seenNotices.Add(noticeID, new HashSet<int>());
				}
				if (m_seenNotices[noticeID].Add(rewardData.RewardChestBagNum.Value))
				{
					result = true;
				}
			}
			else if (!m_seenNotices.ContainsKey(noticeID))
			{
				m_seenNotices.Add(noticeID, new HashSet<int>());
				result = true;
			}
		}
		return result;
	}

	public void ReadyToShowPopups()
	{
		if (Network.IsLoggedIn())
		{
			m_readyToShowPopups = true;
			Update();
		}
	}

	public void ShowAnyOutstandingPopups()
	{
		ShowAnyOutstandingPopups(null);
	}

	public void ShowAnyOutstandingPopups(Action callback)
	{
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.IMMEDIATE,
			Assets.Achieve.RewardTiming.OUT_OF_BAND,
			Assets.Achieve.RewardTiming.ADVENTURE_CHEST
		};
		ShowAnyOutstandingPopups(rewardTimings, callback);
	}

	private void ShowAnyOutstandingPopups(HashSet<Assets.Achieve.RewardTiming> rewardTimings, Action callback)
	{
		PrepareNewlyCompletedAchievesToBeShown(rewardTimings);
		if (callback != null)
		{
			RegisterAllPopupsShownListener(callback);
		}
		ReadyToShowPopups();
	}

	public void ShowRewardsForAdventureUnlocks(List<AdventureHeroPowerDbfRecord> unlockedHeroPowers, List<AdventureDeckDbfRecord> unlockedDecks, List<AdventureLoadoutTreasuresDbfRecord> unlockedLoadoutTreasures, List<AdventureLoadoutTreasuresDbfRecord> upgradedLoadoutTreasures, Action callback)
	{
		List<RewardData> list = new List<RewardData>();
		if (unlockedHeroPowers != null)
		{
			foreach (AdventureHeroPowerDbfRecord unlockedHeroPower in unlockedHeroPowers)
			{
				list.Add(new AdventureHeroPowerRewardData(unlockedHeroPower));
			}
		}
		if (unlockedDecks != null)
		{
			foreach (AdventureDeckDbfRecord unlockedDeck in unlockedDecks)
			{
				list.Add(new AdventureDeckRewardData(unlockedDeck));
			}
		}
		if (unlockedLoadoutTreasures != null)
		{
			foreach (AdventureLoadoutTreasuresDbfRecord unlockedLoadoutTreasure in unlockedLoadoutTreasures)
			{
				list.Add(new AdventureLoadoutTreasureRewardData(unlockedLoadoutTreasure, isUpgrade: false));
			}
		}
		if (upgradedLoadoutTreasures != null)
		{
			foreach (AdventureLoadoutTreasuresDbfRecord upgradedLoadoutTreasure in upgradedLoadoutTreasures)
			{
				list.Add(new AdventureLoadoutTreasureRewardData(upgradedLoadoutTreasure, isUpgrade: true));
			}
		}
		LoadRewards(list, OnRewardObjectLoaded);
		if (callback != null)
		{
			RegisterAllPopupsShownListener(callback);
		}
		ReadyToShowPopups();
	}

	private void RegisterAllPopupsShownListener(Action callback)
	{
		if (callback != null)
		{
			OnAllPopupsShown -= callback;
			OnAllPopupsShown += callback;
		}
	}

	private void ClearAllPopupsShownListeners()
	{
		this.OnAllPopupsShown = delegate
		{
		};
	}

	public void RegisterCompletedQuestShownListener(Action<int> callback)
	{
		if (callback != null)
		{
			OnQuestCompletedShown -= callback;
			OnQuestCompletedShown += callback;
		}
	}

	public void RemoveCompletedQuestShownListener(Action<int> callback)
	{
		if (callback != null)
		{
			OnQuestCompletedShown -= callback;
		}
	}

	public void AddPopupShownListener(Action callback)
	{
		if (callback != null)
		{
			OnPopupShown -= callback;
			OnPopupShown += callback;
		}
	}

	public void RemovePopupShownListener(Action callback)
	{
		if (callback != null)
		{
			OnPopupShown -= callback;
		}
	}

	public void RegisterGenericRewardShownListener(Action<long> callback)
	{
		if (callback != null)
		{
			OnGenericRewardShown -= callback;
			OnGenericRewardShown += callback;
		}
	}

	public void RemoveGenericRewardShownListener(Action<long> callback)
	{
		if (callback != null)
		{
			OnGenericRewardShown -= callback;
		}
	}

	private void OnRewardObjectLoaded(Reward reward, object callbackData)
	{
		LoadReward(reward, ref m_rewards);
	}

	private void OnPurchasedCardRewardObjectLoaded(Reward reward, object callbackData)
	{
		LoadReward(reward, ref m_purchasedCardRewards);
	}

	private void OnGenericRewardObjectLoaded(Reward reward, object callbackData)
	{
		LoadReward(reward, ref m_genericRewards);
	}

	private void LoadReward(Reward reward, ref List<Reward> allRewards)
	{
		reward.Hide();
		PositionReward(reward);
		allRewards.Add(reward);
		if (Reward.Type.CARD == reward.RewardType && reward is CardReward)
		{
			(reward as CardReward).MakeActorsUnlit();
		}
		SceneUtils.SetLayer(reward.gameObject, GameLayer.Default);
		m_numRewardsToLoad--;
		if (m_numRewardsToLoad <= 0)
		{
			RewardUtils.SortRewards(ref allRewards);
		}
	}

	private void DisplayLoadedRewardObject(Reward reward, object callbackData)
	{
		reward.Hide();
		PositionReward(reward);
		SceneUtils.SetLayer(reward.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (RewardUtils.ShowReward(UserAttentionBlocker.NONE, reward, updateCacheValues: false, GetRewardPunchScale(), GetRewardScale(), OnRewardShown, reward))
		{
			m_isShowing = true;
		}
	}

	private void PositionReward(Reward reward)
	{
		Transform transform = reward.transform;
		transform.parent = ChestBones.transform;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = GetRewardLocalPos();
	}

	private bool ShowNextCompletedQuest()
	{
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (m_completedAchieves.Count == 0)
		{
			return false;
		}
		Achievement completedAchieve = m_completedAchieves[0];
		m_isShowing = true;
		this.OnPopupShown();
		UserAttentionBlocker userAttentionBlocker = completedAchieve.GetUserAttentionBlocker();
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode && completedAchieve.ShowToReturningPlayer == Assets.Achieve.ShowToReturningPlayer.SUPPRESSED)
		{
			m_completedAchieves.Remove(completedAchieve);
			completedAchieve.AckCurrentProgressAndRewardNotices();
			m_isShowing = false;
			return true;
		}
		if (!string.IsNullOrEmpty(completedAchieve.CustomVisualWidget))
		{
			AssetLoader.Get().InstantiatePrefab(completedAchieve.CustomVisualWidget, ONAssetLoad);
		}
		else if (!completedAchieve.UseGenericRewardVisual)
		{
			m_completedAchieves.Remove(completedAchieve);
			QuestToast.ShowQuestToast(userAttentionBlocker, delegate
			{
				m_isShowing = false;
			}, updateCacheValues: false, completedAchieve);
			this.OnQuestCompletedShown(completedAchieve.ID);
		}
		else
		{
			m_completedAchieves.Remove(completedAchieve);
			completedAchieve.AckCurrentProgressAndRewardNotices();
			completedAchieve.Rewards[0].LoadRewardObject(DisplayLoadedRewardObject);
		}
		return true;
		void ONAssetLoad(AssetReference assetRef, GameObject go, object callbackData)
		{
			OverlayUI.Get().AddGameObject(go);
			go.GetComponent<CustomVisualReward>().SetCompleteCallback(delegate
			{
				m_isShowing = false;
				m_completedAchieves.Remove(completedAchieve);
				completedAchieve.AckCurrentProgressAndRewardNotices();
			});
		}
	}

	private bool ShowNextUnAckedReward()
	{
		if (m_rewards.Count == 0)
		{
			return false;
		}
		Reward reward = m_rewards[0];
		m_rewards.RemoveAt(0);
		if (RewardUtils.ShowReward(RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData), reward, updateCacheValues: false, GetRewardPunchScale(), GetRewardScale(), OnRewardShown, reward))
		{
			m_isShowing = true;
			this.OnPopupShown();
		}
		return true;
	}

	private bool ShowNextUnAckedGenericReward()
	{
		if (m_genericRewards.Count == 0)
		{
			return false;
		}
		m_isShowing = true;
		this.OnPopupShown();
		Reward reward = m_genericRewards[0];
		m_genericRewards.RemoveAt(0);
		QuestToast.ShowGenericRewardQuestToast(RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData), delegate
		{
			m_isShowing = false;
		}, reward.Data, reward.Data.NameOverride, reward.Data.DescriptionOverride);
		this.OnGenericRewardShown(reward.Data.OriginData);
		return true;
	}

	private bool ShowNextUnAckedPurchasedCardReward()
	{
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (m_purchasedCardRewards.Count == 0)
		{
			return false;
		}
		Reward reward = m_purchasedCardRewards[0];
		UserAttentionBlocker userAttentionBlockerForReward = RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData);
		if (!UserAttentionManager.CanShowAttentionGrabber(userAttentionBlockerForReward, "ShowNextUnAckedPurchasedCardReward"))
		{
			return false;
		}
		m_purchasedCardRewards.RemoveAt(0);
		m_isShowing = true;
		this.OnPopupShown();
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (reward.RewardType == Reward.Type.MINI_SET)
		{
			MiniSetRewardData miniSetRewardData = reward.Data as MiniSetRewardData;
			MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(miniSetRewardData.MiniSetID);
			int count = record.DeckRecord.Cards.Count;
			empty = GameStrings.FormatLocalizedString(record.DeckRecord.Name);
			empty2 = GameStrings.FormatLocalizedString(record.DeckRecord.Description, count);
		}
		else
		{
			CardRewardData card = reward.Data as CardRewardData;
			EntityDef entityDef = DefLoader.Get().GetEntityDef(card.CardID);
			ProductClientDataDbfRecord record2 = GameDbf.ProductClientData.GetRecord((ProductClientDataDbfRecord r) => r.PmtProductId == card.OriginData);
			if (record2 != null)
			{
				empty = GameStrings.FormatLocalizedString(record2.PopupTitle);
				empty2 = GameStrings.FormatLocalizedString(record2.PopupBody, entityDef.GetName());
			}
			else
			{
				empty = GameStrings.Get("GLUE_GENERIC_RANDOM_CARD_SCROLL_TITLE");
				empty2 = GameStrings.Format("GLUE_GENERIC_RANDOM_CARD_SCROLL_DESC", entityDef.GetName());
			}
		}
		QuestToast.ShowQuestToastPopup(userAttentionBlockerForReward, delegate
		{
			m_isShowing = false;
		}, null, reward.Data, empty, empty2, fullscreenEffects: false, updateCacheValues: false, null);
		return true;
	}

	private bool ShowNextTavernBrawlReward()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.UpdateTavernBrawlRewards"))
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.HUB)
		{
			return false;
		}
		NetCache.ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = (NetCache.ProfileNoticeTavernBrawlRewards)NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.TAVERN_BRAWL_REWARDS);
		if (profileNoticeTavernBrawlRewards != null)
		{
			if (ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(profileNoticeTavernBrawlRewards.NoticeID);
			}
			else if (ReturningPlayerMgr.Get().SuppressOldPopups)
			{
				Network.Get().AckNotice(profileNoticeTavernBrawlRewards.NoticeID);
				Log.ReturningPlayer.Print("Suppressing popup for TavernBrawlRewardRewards due to being a Returning Player!");
			}
			else
			{
				m_isShowing = true;
				this.OnPopupShown();
				Transform chestRewardBoneForScene = GetChestRewardBoneForScene();
				if (chestRewardBoneForScene == null)
				{
					Log.All.PrintWarning("No bone set for reward chest in scene={0}!", SceneMgr.Get().GetMode());
					return false;
				}
				List<RewardData> rewards = Network.ConvertRewardChest(profileNoticeTavernBrawlRewards.Chest).Rewards;
				RewardUtils.ShowTavernBrawlRewards(profileNoticeTavernBrawlRewards.Wins, rewards, chestRewardBoneForScene, ShowChestRewardsWhenReady_DoneCallback, fromNotice: true, profileNoticeTavernBrawlRewards);
			}
			return true;
		}
		return false;
	}

	private bool ShowNextLeaguePromotionReward()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextLeaguePromotionReward"))
		{
			return false;
		}
		if (LoadingScreen.Get() != null && LoadingScreen.Get().IsTransitioning())
		{
			return false;
		}
		NetCache.NetCacheProfileNotices netCacheProfileNotices = ((NetCache.Get() != null) ? NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() : null);
		if (netCacheProfileNotices == null || netCacheProfileNotices.Notices == null)
		{
			return false;
		}
		NetCache.ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = (NetCache.ProfileNoticeLeaguePromotionRewards)netCacheProfileNotices.Notices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.LEAGUE_PROMOTION_REWARDS);
		if (profileNoticeLeaguePromotionRewards != null)
		{
			Network network = Network.Get();
			if (ShouldDisableNotificationOnLogin())
			{
				network?.AckNotice(profileNoticeLeaguePromotionRewards.NoticeID);
			}
			else if (ReturningPlayerMgr.Get().SuppressOldPopups)
			{
				if (network != null)
				{
					network.AckNotice(profileNoticeLeaguePromotionRewards.NoticeID);
					Log.ReturningPlayer.Print("Suppressing popup for ProfileNoticeLeaguePromotionRewards due to being a Returning Player!");
				}
			}
			else
			{
				m_isShowing = true;
				this.OnPopupShown();
				Transform chestRewardBoneForScene = GetChestRewardBoneForScene();
				if (chestRewardBoneForScene == null)
				{
					Log.All.PrintWarning("No bone set for reward chest in scene={0}!", SceneMgr.Get().GetMode());
					return false;
				}
				List<RewardData> rewards = Network.ConvertRewardChest(profileNoticeLeaguePromotionRewards.Chest).Rewards;
				RewardUtils.ShowLeaguePromotionRewards(profileNoticeLeaguePromotionRewards.LeagueId, rewards, chestRewardBoneForScene, ShowNextLeaguePromotionReward_DoneCallback, fromNotice: true, profileNoticeLeaguePromotionRewards.NoticeID);
			}
			return true;
		}
		return false;
	}

	public void ShowRankedIntro()
	{
		m_shouldShowRankedIntro = true;
	}

	private bool ShowNextRankedIntro()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextRankedIntro"))
		{
			return false;
		}
		if (!m_shouldShowRankedIntro)
		{
			return false;
		}
		m_isShowing = true;
		m_shouldShowRankedIntro = false;
		DialogManager.Get().ShowRankedIntroPopUp(null);
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		DialogManager.Get().ShowBonusStarsPopup(localPlayerMedalInfo.CreateDataModel(FormatType.FT_STANDARD, RankedMedal.DisplayMode.Default), delegate
		{
			m_rankedIntroShown = true;
			m_isShowing = false;
		});
		return true;
	}

	private bool ShowNextFreeDeckReward()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextFreeDeckReward"))
		{
			return false;
		}
		bool didPromoteSelfThisSession = RankMgr.Get().DidPromoteSelfThisSession;
		if (didPromoteSelfThisSession && !m_rankedIntroShown)
		{
			return false;
		}
		if (didPromoteSelfThisSession && (CollectionManager.Get().AccountEverHadWildCards() || CollectionManager.Get().AccountHasRotatedItems()) && Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS, 0) != 6)
		{
			return false;
		}
		if (m_freeDeckStatus == FreeDeckStatus.CHOOSING)
		{
			return true;
		}
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		NetCache.ProfileNoticeFreeDeckChoice profileNoticeFreeDeckChoice = (NetCache.ProfileNoticeFreeDeckChoice)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.FREE_DECK_CHOICE);
		if (profileNoticeFreeDeckChoice != null)
		{
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			if (mode != SceneMgr.Mode.HUB && mode != SceneMgr.Mode.LOGIN)
			{
				return false;
			}
			if (ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(profileNoticeFreeDeckChoice.NoticeID);
				netObject.Notices.Remove(profileNoticeFreeDeckChoice);
				return false;
			}
			netObject.Notices.Remove(profileNoticeFreeDeckChoice);
			m_freeDeckNoticeIdBeingProcessed = profileNoticeFreeDeckChoice.NoticeID;
			m_freeDeckStatus = FreeDeckStatus.CHOOSING;
			m_isShowing = true;
			AssetLoader.Get().InstantiatePrefab(CHOOSE_A_DECK_PREFAB, ONAssetLoad);
			return true;
		}
		NetCache.ProfileNoticeDeckGranted deckRewardNotice = (NetCache.ProfileNoticeDeckGranted)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.DECK_GRANTED);
		if (deckRewardNotice != null)
		{
			m_freeDeckStatus = FreeDeckStatus.IDLE;
			m_isShowing = true;
			UpdateOfflineDeckCache();
			DeckRewardData rewardData = RewardUtils.CreateDeckRewardData(deckRewardNotice.DeckDbiID, deckRewardNotice.ClassId);
			DbfLocValue name = GameDbf.Deck.GetRecord((DeckDbfRecord deckRecord) => deckRecord.ID == deckRewardNotice.DeckDbiID).Name;
			ShowDeckRewardToast(deckRewardNotice, rewardData, name, GameStrings.Get("GLUE_FREE_DECK_TITLE"), GameStrings.Get("GLUE_FREE_DECK_DESC"));
			Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, deckRewardNotice.PlayerDeckID);
			return true;
		}
		if (m_freeDeckStatus == FreeDeckStatus.WAITING_FOR_GRANT)
		{
			return true;
		}
		return false;
		void ONAssetLoad(AssetReference assetRef, GameObject go, object callbackData)
		{
			OverlayUI.Get().AddGameObject(go);
			ChooseDeckReward component = go.GetComponent<ChooseDeckReward>();
			component.SetNoticeId(m_freeDeckNoticeIdBeingProcessed);
			component.SetCompleteCallback(delegate
			{
				m_isShowing = false;
			});
		}
	}

	private bool ShowNextSellableDeckReward()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextSellableDeckReward") || StoreManager.Get().IsPromptShowing)
		{
			return false;
		}
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		NetCache.ProfileNoticeSellableDeckGranted deckRewardNotice = (NetCache.ProfileNoticeSellableDeckGranted)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.SELLABLE_DECK_GRANTED);
		if (deckRewardNotice == null)
		{
			return false;
		}
		m_isShowing = true;
		UpdateOfflineDeckCache();
		if (!RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId(deckRewardNotice.SellableDeckID, out var deckTemplateRecord))
		{
			return false;
		}
		if (deckRewardNotice.WasDeckGranted)
		{
			int num = 1;
			int count = deckTemplateRecord.DeckRecord.Cards.Count;
			string displayTitle = GameStrings.Get("GLUE_SELLABLE_DECK_TITLE");
			string displayDescription = GameStrings.Format("GLUE_SELLABLE_DECK_DESC", count, num);
			DeckRewardData rewardData = RewardUtils.CreateDeckRewardData(deckTemplateRecord.DeckId, deckTemplateRecord.ClassId);
			ShowDeckRewardToast(deckRewardNotice, rewardData, deckTemplateRecord.DeckRecord.Name, displayTitle, displayDescription);
			RewardUtils.SetNewRewardedDeck(deckRewardNotice.PlayerDeckID);
			return true;
		}
		RewardUtils.CopyDeckTemplateRecordToClipboard(deckTemplateRecord, usePremiumCardsFromCollection: false);
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_SELLABLE_DECK_FULL_LIST_HEADER"),
			m_text = GameStrings.Get("GLUE_SELLABLE_DECK_FULL_LIST_BODY"),
			m_responseDisplay = AlertPopup.ResponseDisplay.OK,
			m_showAlertIcon = true,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
			m_responseCallback = delegate
			{
				m_isShowing = false;
				Network.Get().AckNotice(deckRewardNotice.NoticeID);
			}
		};
		DialogManager.Get().ShowPopup(info);
		this.OnPopupShown();
		return true;
	}

	private void ShowDeckRewardToast(NetCache.ProfileNotice profileNotice, DeckRewardData rewardData, DbfLocValue deckName, string displayTitle, string displayDescription)
	{
		QuestToast.ShowFixedRewardQuestToast(UserAttentionBlocker.NONE, delegate
		{
			m_isShowing = false;
			Network.Get().AckNotice(profileNotice.NoticeID);
			Network.Get().RenameDeck(profileNotice.OriginData, deckName);
		}, rewardData, displayTitle, displayDescription);
		m_deckRewardIds.Add(profileNotice.OriginData);
	}

	private void UpdateOfflineDeckCache()
	{
		CollectionManager.Get().AddOnNetCacheDecksProcessedListener(OnCollectionManagerUpdatedNetCacheDecks);
		NetCache.Get().RefreshNetObject<NetCache.NetCacheDecks>();
		NetCache.Get().RefreshNetObject<NetCache.NetCacheHeroLevels>();
	}

	private void OnCollectionManagerUpdatedNetCacheDecks()
	{
		foreach (long deckRewardId in m_deckRewardIds)
		{
			Network.Get().RequestDeckContents(deckRewardId);
		}
		CollectionManager.Get().RemoveOnNetCacheDecksProcessedListener(OnCollectionManagerUpdatedNetCacheDecks);
	}

	private void OnGetDeckContentsResponse()
	{
		List<DeckContents> list = new List<DeckContents>();
		foreach (DeckContents deck in Network.Get().GetDeckContentsResponse().Decks)
		{
			if (m_deckRewardIds.Contains(deck.DeckId))
			{
				list.Add(deck);
			}
		}
		if (list.Count <= 0)
		{
			return;
		}
		List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
		OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
		foreach (DeckContents item in list)
		{
			OfflineDataCache.CacheLocalAndOriginalDeckContents(item, item);
			m_deckRewardIds.Remove(item.DeckId);
		}
	}

	private void OnFreeDeckChoiceResponse()
	{
		if (!Network.Get().GetFreeDeckChoiceResponse().Success)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_FREE_DECK_ERROR_HEADER"),
				m_text = GameStrings.Get("GLUE_FREE_DECK_ERROR_TEXT"),
				m_showAlertIcon = false,
				m_alertTextAlignment = UberText.AlignmentOptions.Center,
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_responseCallback = delegate
				{
					m_freeDeckStatus = FreeDeckStatus.IDLE;
				}
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			m_freeDeckStatus = FreeDeckStatus.WAITING_FOR_GRANT;
		}
		m_freeDeckNoticeIdBeingProcessed = 0L;
	}

	private bool ShowNextQuestChestReward()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		NetCache.ProfileNoticeGenericRewardChest rewardChestNotice = (NetCache.ProfileNoticeGenericRewardChest)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && n.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE);
		if (rewardChestNotice != null)
		{
			if (ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(rewardChestNotice.NoticeID);
				netObject.Notices.Remove(rewardChestNotice);
				return false;
			}
			Achievement achievement = AchieveManager.Get().GetAchievement((int)rewardChestNotice.OriginData);
			if (!achievement.HasRewardChestVisuals)
			{
				Log.Achievements.PrintError("Achieve id = {0} not properly set up for chest visuals", (int)rewardChestNotice.OriginData);
				return false;
			}
			m_isShowing = true;
			List<RewardData> rewards = Network.ConvertRewardChest(rewardChestNotice.RewardChest).Rewards;
			RewardUtils.ShowQuestChestReward(achievement.Name, achievement.Description, rewards, GetChestRewardBoneForScene(QuestChestBones), delegate
			{
				Network.Get().AckNotice(rewardChestNotice.NoticeID);
				OnPopupClosed();
			}, fromNotice: true, achievement.ID, achievement.ChestVisualPrefabPath);
			return true;
		}
		return false;
	}

	private bool ShowNextDuelsReward()
	{
		if (DuelsConfig.Get().IsReadyToShowRewards())
		{
			NetCache.ProfileNoticeGenericRewardChest rewardNoticeToShow = DuelsConfig.Get().GetRewardNoticeToShow();
			if (rewardNoticeToShow != null)
			{
				m_isShowing = true;
				DuelsConfig.Get().ShowRewardsForNotice(rewardNoticeToShow, OnPopupClosed, GetChestRewardBoneForScene());
				return true;
			}
		}
		return false;
	}

	private bool ShowFixedRewards(HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		if (!FixedRewardsMgr.Get().HasRewardsToShow(rewardTimings))
		{
			return false;
		}
		Log.Achievements.Print("PopupDisplayManager: Showing Fixed Rewards");
		if (!FixedRewardsMgr.Get().ShowFixedRewards(UserAttentionBlocker.NONE, rewardTimings, delegate
		{
			m_isShowing = false;
		}, null))
		{
			m_isShowing = false;
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowNextProgressionAchievementReward()
	{
		if (!AchievementManager.Get().ShowNextReward(OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowNextProgressionQuestReward()
	{
		if (!QuestManager.Get().ShowNextReward(OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowNextProgressionTrackReward()
	{
		if (!RewardTrackManager.Get().ShowNextReward(OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowRewardTrackXpGains()
	{
		RewardXpNotificationManager rewardXpNotificationManager = RewardXpNotificationManager.Get();
		if (!rewardXpNotificationManager.HasXpGainsToShow)
		{
			return false;
		}
		rewardXpNotificationManager.ShowXpNotificationsImmediate(delegate
		{
			m_isShowing = false;
		});
		m_isShowing = true;
		return true;
	}

	private bool ShowRewardTrackSeasonRoll()
	{
		if (!RewardTrackManager.Get().ShowUnclaimedTrackRewardsPopup(OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowNextQuestNotification()
	{
		if (JournalPopup.s_isShowing)
		{
			return false;
		}
		if (!QuestManager.Get().ShowQuestNotification(OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private bool ShowWelcomeQuests()
	{
		if (QuestManager.Get().IsSystemEnabled)
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN)
		{
			return false;
		}
		if (DraftDisplay.Get() != null && DraftDisplay.Get().GetDraftMode() == DraftDisplay.DraftMode.IN_REWARDS)
		{
			return false;
		}
		if (TavernBrawlManager.Get() != null && TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_IN_REWARDS)
		{
			return false;
		}
		if (!AchieveManager.Get().HasQuestsToShow(onlyNewlyActive: true))
		{
			return false;
		}
		if (!WelcomeQuests.Show(UserAttentionBlocker.NONE, fromLogin: false, OnPopupClosed))
		{
			return false;
		}
		this.OnPopupShown();
		m_isShowing = true;
		return true;
	}

	private void ShowQuestProgressToasts()
	{
		if (UserAttentionManager.CanShowAttentionGrabber("ShowQuestProgressToasts") && (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || !UniversalInputManager.UsePhoneUI))
		{
			if (QuestManager.Get().IsSystemEnabled)
			{
				QuestToastManager.Get()?.ShowQuestProgress();
			}
			else if (m_progressedAchieves.Count > 0)
			{
				GameToastMgr.Get().ShowQuestProgressToasts(m_progressedAchieves);
				m_progressedAchieves.Clear();
			}
		}
	}

	public bool ShowChangedCards(DialogBase.HideCallback callbackOnHide = null, UserAttentionBlocker ignoredAttentionBlockers = UserAttentionBlocker.NONE, string featuredCardsEventTiming = null, List<CollectibleCard> cardsToShowOverride = null)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(ignoredAttentionBlockers, "ShowChangedCards"))
		{
			return false;
		}
		List<CollectibleCard> list;
		if (cardsToShowOverride != null)
		{
			list = cardsToShowOverride;
		}
		else
		{
			List<CardChange.ChangeType> changeTypes = new List<CardChange.ChangeType>
			{
				CardChange.ChangeType.BUFF,
				CardChange.ChangeType.NERF
			};
			list = CollectionManager.Get().GetChangedCards(changeTypes, TAG_PREMIUM.NORMAL, featuredCardsEventTiming);
		}
		bool flag = false;
		foreach (CollectibleCard item in list)
		{
			if (!flag && (CollectionManager.Get().IsCardInCollection(item.CardId, TAG_PREMIUM.NORMAL) || CollectionManager.Get().IsCardInCollection(item.CardId, TAG_PREMIUM.GOLDEN)))
			{
				flag = true;
			}
		}
		if (Cheats.ShowFakeNerfedCards && !flag && !m_hasShownCheatedChangedCards)
		{
			UIStatus.Get().AddInfo("SHOWING FAKE NERFED CARDS!\nTo disable this, remove ShowFakeNerfedCards from client.config.", 5f);
			list = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL)
				.Take(3)
				.ToList();
			flag = true;
			m_hasShownCheatedChangedCards = true;
		}
		foreach (CollectibleCard item2 in list)
		{
			ChangedCardMgr.Get().MarkCardChangeSeen(item2.ChangeVersion, item2.CardDbId);
		}
		if (flag && !ReturningPlayerMgr.Get().IsInReturningPlayerMode)
		{
			CardListPopup.Info info = new CardListPopup.Info
			{
				m_description = GameStrings.Get((list.Count == 1) ? "GLUE_SINGLE_CARD_UPDATED" : "GLUE_CARDS_UPDATED"),
				m_cards = list,
				m_callbackOnHide = callbackOnHide
			};
			info.m_useMultiLineDescription = info.m_description.Contains('\n');
			DialogManager.Get().ShowCardListPopup(ignoredAttentionBlockers, info);
			return true;
		}
		return false;
	}

	public bool ShowAddedCards(DialogBase.HideCallback callbackOnHide = null, UserAttentionBlocker ignoredAttentionBlockers = UserAttentionBlocker.NONE, string featuredCardsEventTiming = null, List<CollectibleCard> cardsToShowOverride = null)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(ignoredAttentionBlockers, "ShowAddedCards"))
		{
			return false;
		}
		List<CollectibleCard> list;
		if (cardsToShowOverride != null)
		{
			list = cardsToShowOverride;
		}
		else
		{
			List<CardChange.ChangeType> changeTypes = new List<CardChange.ChangeType> { CardChange.ChangeType.ADDITION };
			list = CollectionManager.Get().GetChangedCards(changeTypes, TAG_PREMIUM.NORMAL, featuredCardsEventTiming);
		}
		if (Cheats.ShowFakeAddedCards && !m_hasShownCheatedAddedCards)
		{
			UIStatus.Get().AddInfo("SHOWING FAKE ADDED CARDS!\nTo disable this, remove ShowFakeAddedCards from client.config.", 5f);
			list = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL)
				.Take(3)
				.ToList();
			m_hasShownCheatedAddedCards = true;
		}
		if (list.Count > 0)
		{
			foreach (CollectibleCard item in list)
			{
				ChangedCardMgr.Get().MarkCardChangeSeen(item.ChangeVersion, item.CardDbId);
			}
			if (!ReturningPlayerMgr.Get().IsInReturningPlayerMode)
			{
				CardListPopup.Info info = new CardListPopup.Info();
				info.m_description = ((list.Count == 1) ? GameStrings.Get("GLUE_SINGLE_CARD_ADDED") : GameStrings.Format("GLUE_CARDS_ADDED", list.Count));
				info.m_cards = list;
				info.m_callbackOnHide = callbackOnHide;
				info.m_useMultiLineDescription = info.m_description.Contains('\n');
				DialogManager.Get().ShowCardListPopup(ignoredAttentionBlockers, info);
				return true;
			}
		}
		return false;
	}

	private bool ShowFeaturedCards(string featuredCardsEventTiming, string headerText, DialogBase.HideCallback callbackOnHide = null, UserAttentionBlocker ignoredAttentionBlockers = UserAttentionBlocker.NONE)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(ignoredAttentionBlockers, "ShowFeaturedCards"))
		{
			return false;
		}
		MultiPagePopup.Info info = new MultiPagePopup.Info
		{
			m_callbackOnHide = callbackOnHide,
			m_blurWhenShown = true
		};
		List<CardChange.ChangeType> changeTypes = new List<CardChange.ChangeType>
		{
			CardChange.ChangeType.ADDITION,
			CardChange.ChangeType.BUFF,
			CardChange.ChangeType.NERF
		};
		List<CollectibleCard> changedCards = CollectionManager.Get().GetChangedCards(changeTypes, TAG_PREMIUM.NORMAL, featuredCardsEventTiming);
		if (!changedCards.Any())
		{
			return false;
		}
		MultiPagePopup.PageInfo item = new MultiPagePopup.PageInfo
		{
			m_pageType = MultiPagePopup.PageType.CARD_LIST,
			m_cardsToShow = changedCards,
			m_headerText = headerText
		};
		info.m_pages.Add(item);
		DialogManager.Get().ShowMultiPagePopup(UserAttentionBlocker.NONE, info);
		return true;
	}

	public bool ShowLoginPopupSequence()
	{
		if (!UserAttentionManager.CanShowAttentionGrabber("ShowLoginPopupSequence"))
		{
			return false;
		}
		List<LoginPopupSequenceDbfRecord> records = GameDbf.LoginPopupSequence.GetRecords((LoginPopupSequenceDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.EventTiming, activeIfDoesNotExist: false));
		if (records == null || records.Count == 0)
		{
			return false;
		}
		if (ShouldDisableNotificationOnLogin())
		{
			List<long> list = new List<long>();
			foreach (LoginPopupSequenceDbfRecord item in records)
			{
				list.Add(item.ID);
			}
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, list.ToArray()));
			return false;
		}
		bool result = false;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, out List<long> values);
		foreach (LoginPopupSequenceDbfRecord popupSequenceRecord in records)
		{
			if (values != null && values.Contains(popupSequenceRecord.ID))
			{
				continue;
			}
			List<LoginPopupSequencePopupDbfRecord> records2 = GameDbf.LoginPopupSequencePopup.GetRecords((LoginPopupSequencePopupDbfRecord r) => r.LoginPopupSequenceId == popupSequenceRecord.ID);
			for (int i = 0; i < records2.Count; i++)
			{
				LoginPopupSequencePopupDbfRecord loginPopupSequencePopupDbfRecord = records2[i];
				Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType popupType = loginPopupSequencePopupDbfRecord.PopupType;
				bool flag = true;
				if (loginPopupSequencePopupDbfRecord.RequiresWildUnlocked && !CollectionManager.Get().ShouldAccountSeeStandardWild())
				{
					flag = false;
				}
				else if (loginPopupSequencePopupDbfRecord.SuppressForReturningPlayer && ReturningPlayerMgr.Get().IsInReturningPlayerMode)
				{
					flag = false;
				}
				bool flag2 = i == records2.Count - 1;
				DialogBase.HideCallback callbackOnHide = null;
				if (flag2)
				{
					if (!flag)
					{
						OnPopupSequenceDismissed(popupSequenceRecord.ID);
						break;
					}
					callbackOnHide = delegate
					{
						OnPopupSequenceDismissed(popupSequenceRecord.ID);
					};
				}
				else if (!flag)
				{
					continue;
				}
				if ((uint)popupType > 1u && popupType == Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType.FEATURED_CARDS)
				{
					if (ShowFeaturedCards(loginPopupSequencePopupDbfRecord.FeaturedCardsEvent, loginPopupSequencePopupDbfRecord.HeaderText, callbackOnHide))
					{
						result = true;
					}
					else if (flag2)
					{
						OnPopupSequenceDismissed(popupSequenceRecord.ID);
					}
					continue;
				}
				LoginPopupSequencePopup.Info info = new LoginPopupSequencePopup.Info
				{
					m_headerText = loginPopupSequencePopupDbfRecord.HeaderText,
					m_bodyText = loginPopupSequencePopupDbfRecord.BodyText,
					m_buttonText = loginPopupSequencePopupDbfRecord.ButtonText,
					m_backgroundMaterialReference = new AssetReference(loginPopupSequencePopupDbfRecord.BackgroundMaterial),
					m_callbackOnHide = callbackOnHide,
					m_prefabAssetReference = loginPopupSequencePopupDbfRecord.PrefabOverride
				};
				if (loginPopupSequencePopupDbfRecord.CardId != 0)
				{
					TAG_PREMIUM cardPremium = (TAG_PREMIUM)loginPopupSequencePopupDbfRecord.CardPremium;
					info.m_card = CollectionManager.Get().GetCard(GameUtils.TranslateDbIdToCardId(loginPopupSequencePopupDbfRecord.CardId), cardPremium);
				}
				DialogManager.Get().ShowLoginPopupSequenceBasicPopup(UserAttentionBlocker.NONE, info);
				result = true;
			}
		}
		return result;
	}

	private void OnPopupSequenceDismissed(int popupSequenceId)
	{
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, out List<long> values);
		if (values == null)
		{
			values = new List<long>();
		}
		values.Add(popupSequenceId);
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, values.ToArray()));
	}

	public Vector3 GetRewardLocalPos()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(-7.72f, 8.371922f, -3.883112f),
				Phone = new Vector3(-7.72f, 7.3f, -3.94f)
			};
		}
		return new Vector3(0.1438589f, 31.27692f, 12.97332f);
	}

	public Vector3 GetRewardScale()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.GAMEPLAY:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = Vector3.one,
				Phone = new Vector3(0.8f, 0.8f, 0.8f)
			};
		case SceneMgr.Mode.ADVENTURE:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(10f, 10f, 10f),
				Phone = new Vector3(7f, 7f, 7f)
			};
		case SceneMgr.Mode.STARTUP:
		case SceneMgr.Mode.LOGIN:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15f, 15f, 15f),
				Phone = new Vector3(14f, 14f, 14f)
			};
		default:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15f, 15f, 15f),
				Phone = new Vector3(8f, 8f, 8f)
			};
		}
	}

	public Vector3 GetRewardPunchScale()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.GAMEPLAY:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(1.2f, 1.2f, 1.2f),
				Phone = new Vector3(1.25f, 1.25f, 1.25f)
			};
		case SceneMgr.Mode.ADVENTURE:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(10.2f, 10.2f, 10.2f),
				Phone = new Vector3(7.1f, 7.1f, 7.1f)
			};
		case SceneMgr.Mode.STARTUP:
		case SceneMgr.Mode.LOGIN:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15.1f, 15.1f, 15.1f),
				Phone = new Vector3(14.1f, 14.1f, 14.1f)
			};
		default:
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15.1f, 15.1f, 15.1f),
				Phone = new Vector3(8.1f, 8.1f, 8.1f)
			};
		}
	}

	public IEnumerator WaitForAllPopups()
	{
		bool allPopupsShown = false;
		RegisterAllPopupsShownListener(delegate
		{
			allPopupsShown = true;
		});
		while (!allPopupsShown)
		{
			yield return null;
		}
	}

	public IEnumerator<IAsyncJobResult> Job_WaitForAllPopups()
	{
		ReadyToShowPopups();
		bool allPopupsShown = false;
		RegisterAllPopupsShownListener(delegate
		{
			allPopupsShown = true;
		});
		while (!allPopupsShown)
		{
			yield return null;
		}
	}

	public static bool ShouldDisableNotificationOnLogin()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		if (StoreManager.Get().IsShown())
		{
			return false;
		}
		if (!Options.Get().GetBool(Option.DISABLE_LOGIN_POPUPS))
		{
			return false;
		}
		if (!m_hasPlayerReachedHub)
		{
			return true;
		}
		return Time.realtimeSinceStartup - m_timePlayerInHubAfterLogin < 20f;
	}

	private void InitializePlayerTimeInHubAfterLogin()
	{
		m_hasPlayerReachedHub = true;
		m_timePlayerInHubAfterLogin = Time.realtimeSinceStartup;
	}

	private void UpdateRewards()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		List<RewardData> rewardsToShow = new List<RewardData>();
		List<RewardData> genericRewardChestsToShow = new List<RewardData>();
		List<RewardData> purchasedCardRewardsToShow = new List<RewardData>();
		if (netObject != null)
		{
			AchieveManager achieveMgr = AchieveManager.Get();
			List<NetCache.ProfileNotice> noticesToAck = new List<NetCache.ProfileNotice>();
			List<NetCache.ProfileNotice> notices = netObject.Notices.Where(delegate(NetCache.ProfileNotice n)
			{
				if (n.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && n.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
				{
					Achievement achievement = AchieveManager.Get().GetAchievement((int)n.OriginData);
					if (achievement != null && achievement.HasRewardChestVisuals)
					{
						return false;
					}
				}
				if (n.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && n.Origin == NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS)
				{
					return false;
				}
				if (n.Origin == NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT && achieveMgr.IsAchievementVisuallyBlocklisted((int)n.OriginData))
				{
					noticesToAck.Add(n);
					return false;
				}
				return n.Type != NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST || m_genericRewardChestNoticeIdsReady.Any((long r) => n.NoticeID == r);
			}).ToList();
			Network network = Network.Get();
			foreach (NetCache.ProfileNotice item2 in noticesToAck)
			{
				network.AckNotice(item2.NoticeID);
			}
			List<RewardData> rewards = RewardUtils.GetRewards(notices);
			HashSet<Assets.Achieve.RewardTiming> hashSet = new HashSet<Assets.Achieve.RewardTiming>();
			foreach (Assets.Achieve.RewardTiming value in Enum.GetValues(typeof(Assets.Achieve.RewardTiming)))
			{
				hashSet.Add(value);
			}
			RewardUtils.GetViewableRewards(rewards, hashSet, out rewardsToShow, out genericRewardChestsToShow, ref purchasedCardRewardsToShow, ref m_completedAchieves);
		}
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			List<Achievement> list = new List<Achievement>();
			foreach (Achievement completedAchiefe in m_completedAchieves)
			{
				Assets.Achieve.ShowToReturningPlayer showToReturningPlayer = completedAchiefe.ShowToReturningPlayer;
				if (showToReturningPlayer == Assets.Achieve.ShowToReturningPlayer.SUPPRESSED)
				{
					Log.ReturningPlayer.Print("Suppressing popup for Achievement {0} due to being a Returning Player!", completedAchiefe);
					completedAchiefe.AckCurrentProgressAndRewardNotices();
				}
				else
				{
					list.Add(completedAchiefe);
				}
			}
			m_completedAchieves = list;
			genericRewardChestsToShow.RemoveAll(delegate(RewardData rewardData)
			{
				if (!rewardData.RewardChestAssetId.HasValue)
				{
					AckNotices(rewardData);
					return true;
				}
				RewardChestDbfRecord record = GameDbf.RewardChest.GetRecord(rewardData.RewardChestAssetId.Value);
				if (record == null || !record.ShowToReturningPlayer)
				{
					AckNotices(rewardData);
					return true;
				}
				return false;
			});
		}
		if (!ShouldDisableNotificationOnLogin())
		{
			LoadRewards(rewardsToShow, OnRewardObjectLoaded);
			LoadRewards(purchasedCardRewardsToShow, OnPurchasedCardRewardObjectLoaded);
			LoadRewards(genericRewardChestsToShow, OnGenericRewardObjectLoaded);
		}
		Log.Achievements.Print("PopupDisplayManager: adding {0} rewards to load total={1}", rewardsToShow.Count, m_numRewardsToLoad);
		static void AckNotices(RewardData rewardData)
		{
			foreach (long noticeID in rewardData.GetNoticeIDs())
			{
				Network.Get().AckNotice(noticeID);
			}
		}
	}

	private void LoadRewards(List<RewardData> rewardsToLoad, Reward.DelOnRewardLoaded callback)
	{
		foreach (RewardData item in rewardsToLoad)
		{
			if (UpdateNoticesSeen(item))
			{
				if (ReturningPlayerMgr.Get().SuppressOldPopups && (item.Origin == NetCache.ProfileNotice.NoticeOrigin.TOURNEY || item.Origin == NetCache.ProfileNotice.NoticeOrigin.TAVERN_BRAWL_REWARD || item.Origin == NetCache.ProfileNotice.NoticeOrigin.LEAGUE_PROMOTION))
				{
					Log.ReturningPlayer.Print("Suppressing popup for Reward {0} due to being a Returning Player!", item);
					item.AcknowledgeNotices();
				}
				else
				{
					m_numRewardsToLoad++;
					item.LoadRewardObject(callback);
				}
			}
		}
	}

	private void OnRewardShown(object callbackData)
	{
		Reward reward = callbackData as Reward;
		if (!(reward == null))
		{
			reward.RegisterClickListener(OnRewardClicked);
			reward.EnableClickCatcher(enabled: true);
		}
	}

	private void ShowChestRewardsWhenReady_DoneCallback()
	{
		m_isShowing = false;
	}

	private void ShowNextLeaguePromotionReward_DoneCallback()
	{
		ShowRankedIntro();
		m_isShowing = false;
	}

	private void OnRewardClicked(Reward reward, object userData)
	{
		reward.RemoveClickListener(OnRewardClicked);
		reward.Hide(animate: true);
		m_isShowing = false;
	}

	private void OnPopupClosed()
	{
		m_isShowing = false;
	}

	private bool CanShowPopups()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY && (EndGameScreen.Get() == null || !EndGameScreen.Get().IsDoneDisplayingRewards()))
		{
			return false;
		}
		return true;
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userData)
	{
		HashSet<Assets.Achieve.RewardTiming> hashSet = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.IMMEDIATE,
			Assets.Achieve.RewardTiming.OUT_OF_BAND
		};
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || !SceneMgr.Get().IsTransitioning())
		{
			hashSet.Add(Assets.Achieve.RewardTiming.ADVENTURE_CHEST);
		}
		PrepareNewlyProgressedAchievesToBeShown();
		PrepareNewlyCompletedAchievesToBeShown(hashSet);
	}

	private void PrepareNewlyProgressedAchievesToBeShown()
	{
		m_progressedAchieves = AchieveManager.Get().GetNewlyProgressedQuests();
	}

	private void PrepareNewlyCompletedAchievesToBeShown(HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		if (!CanShowPopups())
		{
			return;
		}
		foreach (Achievement achieve in AchieveManager.Get().GetNewCompletedAchievesToShow())
		{
			if (m_completedAchieves.Find((Achievement obj) => achieve.ID == obj.ID) != null)
			{
				Log.Achievements.Print("PopupDisplayManager: skipping completed achievement already being processed: " + achieve);
			}
			else if (rewardTimings == null || !rewardTimings.Contains(achieve.RewardTiming))
			{
				Log.Achievements.PrintDebug("PopupDisplayManager: skipping completed achievement with {0} reward timing: {1}", achieve.RewardTiming, achieve);
			}
			else
			{
				Log.Achievements.Print("PopupDisplayManager: adding completed achievement " + achieve);
				m_completedAchieves.Add(achieve);
			}
		}
		UpdateRewards();
	}

	private void OnGenericRewardUpdated(long rewardNoticeId, object userData)
	{
		m_genericRewardChestNoticeIdsReady.Add(rewardNoticeId);
		if (CanShowPopups())
		{
			UpdateRewards();
		}
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		if (!CanShowPopups() || newNotices.Count <= 0)
		{
			return;
		}
		UpdateRewards();
		newNotices.ForEach(delegate(NetCache.ProfileNotice notice)
		{
			if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.CARD_REPLACEMENT)
			{
				m_cardReplacementNotices.Enqueue(notice);
			}
			else if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.HOF_COMPENSATION && notice.Type == NetCache.ProfileNotice.NoticeType.REWARD_DUST)
			{
				m_dustRewardNotices.Enqueue(notice);
			}
		});
	}

	private Transform GetChestRewardBoneForScene(PopupDisplayManagerBones boneSet = null)
	{
		PopupDisplayManagerBones popupDisplayManagerBones = ((boneSet != null) ? boneSet : ChestBones);
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.LOGIN:
		case SceneMgr.Mode.HUB:
			return popupDisplayManagerBones.m_rewardChestBone_Box;
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
			return popupDisplayManagerBones.m_rewardChestBone_DungeonCrawl;
		case SceneMgr.Mode.TOURNAMENT:
			return popupDisplayManagerBones.m_rewardChestBone_PlayMode;
		case SceneMgr.Mode.PACKOPENING:
			return popupDisplayManagerBones.m_rewardChestBone_PackOpening;
		default:
			return null;
		}
	}

	private bool ShowInGameMessagePopups()
	{
		if (HearthstoneServices.TryGet<MessagePopupDisplay>(out var service))
		{
			if (service.IsDisplayingMessage)
			{
				return true;
			}
			if (service.HasMessageToDisplay && CanDisplayIGMPopups())
			{
				m_isShowing = true;
				this.OnPopupShown();
				service.DisplayNextMessage(delegate
				{
					m_isShowing = false;
				});
				return true;
			}
		}
		return false;
	}

	private bool CanDisplayIGMPopups()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB && !StoreManager.Get().IsShownOrWaitingToShow())
		{
			return !JournalPopup.s_isShowing;
		}
		return false;
	}
}
