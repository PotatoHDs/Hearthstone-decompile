using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

// Token: 0x020008FD RID: 2301
public class PopupDisplayManager : IHasUpdate, IService
{
	// Token: 0x17000749 RID: 1865
	// (get) Token: 0x06007FBC RID: 32700 RVA: 0x00297F18 File Offset: 0x00296118
	// (set) Token: 0x06007FBD RID: 32701 RVA: 0x00297F20 File Offset: 0x00296120
	private PopupDisplayManagerBones ChestBones { get; set; }

	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x06007FBE RID: 32702 RVA: 0x00297F29 File Offset: 0x00296129
	// (set) Token: 0x06007FBF RID: 32703 RVA: 0x00297F31 File Offset: 0x00296131
	private PopupDisplayManagerBones QuestChestBones { get; set; }

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x06007FC0 RID: 32704 RVA: 0x00297F3A File Offset: 0x0029613A
	private static HashSet<Assets.Achieve.RewardTiming> CurrentRewardTimings
	{
		get
		{
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN)
			{
				return new HashSet<Assets.Achieve.RewardTiming>
				{
					Assets.Achieve.RewardTiming.IMMEDIATE
				};
			}
			return new HashSet<Assets.Achieve.RewardTiming>
			{
				Assets.Achieve.RewardTiming.OUT_OF_BAND,
				Assets.Achieve.RewardTiming.IMMEDIATE
			};
		}
	}

	// Token: 0x14000080 RID: 128
	// (add) Token: 0x06007FC1 RID: 32705 RVA: 0x00297F6C File Offset: 0x0029616C
	// (remove) Token: 0x06007FC2 RID: 32706 RVA: 0x00297FA4 File Offset: 0x002961A4
	private event Action OnAllPopupsShown = delegate()
	{
	};

	// Token: 0x14000081 RID: 129
	// (add) Token: 0x06007FC3 RID: 32707 RVA: 0x00297FDC File Offset: 0x002961DC
	// (remove) Token: 0x06007FC4 RID: 32708 RVA: 0x00298014 File Offset: 0x00296214
	private event Action<int> OnQuestCompletedShown = delegate(int achieveId)
	{
	};

	// Token: 0x14000082 RID: 130
	// (add) Token: 0x06007FC5 RID: 32709 RVA: 0x0029804C File Offset: 0x0029624C
	// (remove) Token: 0x06007FC6 RID: 32710 RVA: 0x00298084 File Offset: 0x00296284
	private event Action OnPopupShown = delegate()
	{
	};

	// Token: 0x14000083 RID: 131
	// (add) Token: 0x06007FC7 RID: 32711 RVA: 0x002980BC File Offset: 0x002962BC
	// (remove) Token: 0x06007FC8 RID: 32712 RVA: 0x002980F4 File Offset: 0x002962F4
	private event Action<long> OnGenericRewardShown = delegate(long originData)
	{
	};

	// Token: 0x06007FC9 RID: 32713 RVA: 0x00298129 File Offset: 0x00296329
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoadComponentFromResource<PopupDisplayManagerBones> loadBones = new LoadComponentFromResource<PopupDisplayManagerBones>("ServiceData/PopupDisplayManagerBones", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadBones;
		this.ChestBones = loadBones.LoadedComponent;
		loadBones = new LoadComponentFromResource<PopupDisplayManagerBones>("ServiceData/PopupDisplayManagerBonesForQuestChests", LoadResourceFlags.AutoInstantiateOnLoad | LoadResourceFlags.FailOnError);
		yield return loadBones;
		this.QuestChestBones = loadBones.LoadedComponent;
		HearthstoneApplication.Get().WillReset += this.WillReset;
		Processor.RunCoroutine(this.StartupCoroutine(), null);
		LoginManager.Get().OnFullLoginFlowComplete += this.InitializePlayerTimeInHubAfterLogin;
		PopupDisplayManager.m_timePlayerInHubAfterLogin = 0f;
		PopupDisplayManager.m_hasPlayerReachedHub = false;
		yield break;
	}

	// Token: 0x06007FCA RID: 32714 RVA: 0x00298138 File Offset: 0x00296338
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(NetCache),
			typeof(AchieveManager),
			typeof(ReturningPlayerMgr)
		};
	}

	// Token: 0x06007FCB RID: 32715 RVA: 0x00298174 File Offset: 0x00296374
	public void Shutdown()
	{
		LoginManager loginManager;
		if (HearthstoneServices.TryGet<LoginManager>(out loginManager))
		{
			loginManager.OnFullLoginFlowComplete -= this.InitializePlayerTimeInHubAfterLogin;
		}
		HearthstoneApplication.Get().WillReset -= this.WillReset;
	}

	// Token: 0x06007FCC RID: 32716 RVA: 0x002981B2 File Offset: 0x002963B2
	private IEnumerator StartupCoroutine()
	{
		AchieveManager.Get().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated), null);
		GenericRewardChestNoticeManager.Get().RegisterRewardsUpdatedListener(new GenericRewardChestNoticeManager.GenericRewardUpdatedCallback(this.OnGenericRewardUpdated), null);
		NetCache.Get().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		Network.Get().RegisterNetHandler(GetDeckContentsResponse.PacketID.ID, new Network.NetHandler(this.OnGetDeckContentsResponse), null);
		Network.Get().RegisterNetHandler(FreeDeckChoiceResponse.PacketID.ID, new Network.NetHandler(this.OnFreeDeckChoiceResponse), null);
		yield break;
	}

	// Token: 0x06007FCD RID: 32717 RVA: 0x002981C4 File Offset: 0x002963C4
	public void Update()
	{
		if (!this.m_readyToShowPopups)
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		if (mode == SceneMgr.Mode.STARTUP)
		{
			return;
		}
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			return;
		}
		this.ShowQuestProgressToasts();
		if (GameUtils.IsAnyTransitionActive())
		{
			return;
		}
		if (this.IsShowing)
		{
			return;
		}
		if (!this.m_hasCheckedNewPlayerSetRotationPopup)
		{
			this.m_hasCheckedNewPlayerSetRotationPopup = true;
			if (SetRotationManager.ShowNewPlayerSetRotationPopupIfNeeded())
			{
				return;
			}
		}
		if (ReturningPlayerMgr.Get().ShowReturningPlayerWelcomeBannerIfNeeded(new ReturningPlayerMgr.WelcomeBannerCloseCallback(this.OnPopupClosed)))
		{
			this.OnPopupShown();
			this.m_isShowing = true;
			return;
		}
		if (FiresideGatheringManager.Get().ShowSignIfNeeded(new FiresideGatheringManager.OnCloseSign(this.OnPopupClosed)))
		{
			this.OnPopupShown();
			this.m_isShowing = true;
			return;
		}
		if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
		{
			BannerManager.Get().AutoAcknowledgeOutstandingBanner();
		}
		else if (BannerManager.Get().ShowOutstandingBannerEvent(new BannerManager.DelOnCloseBanner(this.OnPopupClosed)))
		{
			this.OnPopupShown();
			this.m_isShowing = true;
			return;
		}
		if (DraftManager.Get().ShowNextArenaPopup(new Action(this.OnPopupClosed)))
		{
			this.OnPopupShown();
			this.m_isShowing = true;
			return;
		}
		if (!this.m_hasShownCardAdditionPopups && this.ShowAddedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, null))
		{
			this.m_hasShownCardAdditionPopups = true;
			return;
		}
		if (!this.m_hasShownCardChangePopups && this.ShowChangedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, null))
		{
			this.m_hasShownCardChangePopups = true;
			return;
		}
		if (!this.m_hasShownMetaShakeupEventPopups && this.ShowLoginPopupSequence())
		{
			this.m_hasShownMetaShakeupEventPopups = true;
			return;
		}
		if (this.ShowNextTavernBrawlReward())
		{
			return;
		}
		if (this.ShowNextLeaguePromotionReward())
		{
			return;
		}
		if (this.ShowNextRankedIntro())
		{
			return;
		}
		if (this.ShowNextFreeDeckReward())
		{
			return;
		}
		if (this.ShowNextSellableDeckReward())
		{
			return;
		}
		if (this.ShowNextQuestChestReward())
		{
			return;
		}
		if (this.ShowNextDuelsReward())
		{
			return;
		}
		if (this.ShowNextProgressionAchievementReward())
		{
			return;
		}
		if (this.ShowNextProgressionQuestReward())
		{
			return;
		}
		if (this.ShowNextProgressionTrackReward())
		{
			return;
		}
		if (this.ShowRewardTrackXpGains())
		{
			return;
		}
		if (this.ShowRewardTrackSeasonRoll())
		{
			return;
		}
		if (this.m_completedAchieves.Count > 0 || this.m_rewards.Count > 0 || this.m_purchasedCardRewards.Count > 0 || this.m_genericRewards.Count > 0)
		{
			if (this.ShowNextCompletedQuest())
			{
				return;
			}
			if (this.ShowNextUnAckedReward())
			{
				return;
			}
			if (this.ShowNextUnAckedGenericReward())
			{
				return;
			}
			if (this.ShowNextUnAckedPurchasedCardReward())
			{
				return;
			}
		}
		if (this.ShowFixedRewards(PopupDisplayManager.CurrentRewardTimings))
		{
			return;
		}
		if (this.IsLoadingRewards)
		{
			return;
		}
		if (this.ShowNextQuestNotification())
		{
			return;
		}
		if (this.ShowWelcomeQuests())
		{
			return;
		}
		if (this.ShowInGameMessagePopups())
		{
			return;
		}
		NarrativeManager.Get().OnAllPopupsShown();
		if (this.IsShowing)
		{
			return;
		}
		this.OnAllPopupsShown();
		this.ClearAllPopupsShownListeners();
	}

	// Token: 0x06007FCE RID: 32718 RVA: 0x00298451 File Offset: 0x00296651
	public static PopupDisplayManager Get()
	{
		return HearthstoneServices.Get<PopupDisplayManager>();
	}

	// Token: 0x06007FCF RID: 32719 RVA: 0x00298458 File Offset: 0x00296658
	private void WillReset()
	{
		this.m_readyToShowPopups = false;
		this.m_isShowing = false;
		LoginManager loginManager;
		if (HearthstoneServices.TryGet<LoginManager>(out loginManager))
		{
			loginManager.OnFullLoginFlowComplete -= this.InitializePlayerTimeInHubAfterLogin;
		}
		UniversalInputManager universalInputManager;
		if (HearthstoneServices.TryGet<UniversalInputManager>(out universalInputManager))
		{
			universalInputManager.SetGameDialogActive(false);
		}
		DialogManager dialogManager = DialogManager.Get();
		if (dialogManager != null)
		{
			dialogManager.ReadyForSeasonEndPopup(false);
			dialogManager.ClearHandledMedalNotices();
		}
		this.m_cardReplacementNotices.Clear();
		this.m_dustRewardNotices.Clear();
		this.ClearSeenNotices();
		this.ClearAllPopupsShownListeners();
	}

	// Token: 0x06007FD0 RID: 32720 RVA: 0x002984DC File Offset: 0x002966DC
	public void ClearSeenNotices()
	{
		this.m_seenNotices.Clear();
	}

	// Token: 0x06007FD1 RID: 32721 RVA: 0x002984EC File Offset: 0x002966EC
	public bool UpdateNoticesSeen(RewardData rewardData)
	{
		if (!rewardData.HasNotices())
		{
			return true;
		}
		bool result = false;
		foreach (long key in rewardData.GetNoticeIDs())
		{
			if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE && rewardData.RewardChestBagNum != null)
			{
				if (!this.m_seenNotices.ContainsKey(key))
				{
					this.m_seenNotices.Add(key, new HashSet<int>());
				}
				if (this.m_seenNotices[key].Add(rewardData.RewardChestBagNum.Value))
				{
					result = true;
				}
			}
			else if (!this.m_seenNotices.ContainsKey(key))
			{
				this.m_seenNotices.Add(key, new HashSet<int>());
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06007FD2 RID: 32722 RVA: 0x002985CC File Offset: 0x002967CC
	public void ReadyToShowPopups()
	{
		if (!Network.IsLoggedIn())
		{
			return;
		}
		this.m_readyToShowPopups = true;
		this.Update();
	}

	// Token: 0x06007FD3 RID: 32723 RVA: 0x002985E3 File Offset: 0x002967E3
	public void ShowAnyOutstandingPopups()
	{
		this.ShowAnyOutstandingPopups(null);
	}

	// Token: 0x06007FD4 RID: 32724 RVA: 0x002985EC File Offset: 0x002967EC
	public void ShowAnyOutstandingPopups(Action callback)
	{
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.IMMEDIATE,
			Assets.Achieve.RewardTiming.OUT_OF_BAND,
			Assets.Achieve.RewardTiming.ADVENTURE_CHEST
		};
		this.ShowAnyOutstandingPopups(rewardTimings, callback);
	}

	// Token: 0x06007FD5 RID: 32725 RVA: 0x0029861F File Offset: 0x0029681F
	private void ShowAnyOutstandingPopups(HashSet<Assets.Achieve.RewardTiming> rewardTimings, Action callback)
	{
		this.PrepareNewlyCompletedAchievesToBeShown(rewardTimings);
		if (callback != null)
		{
			this.RegisterAllPopupsShownListener(callback);
		}
		this.ReadyToShowPopups();
	}

	// Token: 0x06007FD6 RID: 32726 RVA: 0x00298638 File Offset: 0x00296838
	public void ShowRewardsForAdventureUnlocks(List<AdventureHeroPowerDbfRecord> unlockedHeroPowers, List<AdventureDeckDbfRecord> unlockedDecks, List<AdventureLoadoutTreasuresDbfRecord> unlockedLoadoutTreasures, List<AdventureLoadoutTreasuresDbfRecord> upgradedLoadoutTreasures, Action callback)
	{
		List<RewardData> list = new List<RewardData>();
		if (unlockedHeroPowers != null)
		{
			foreach (AdventureHeroPowerDbfRecord heroPowerRecord in unlockedHeroPowers)
			{
				list.Add(new AdventureHeroPowerRewardData(heroPowerRecord));
			}
		}
		if (unlockedDecks != null)
		{
			foreach (AdventureDeckDbfRecord deckRecord in unlockedDecks)
			{
				list.Add(new AdventureDeckRewardData(deckRecord));
			}
		}
		if (unlockedLoadoutTreasures != null)
		{
			foreach (AdventureLoadoutTreasuresDbfRecord loadoutTreasureRecord in unlockedLoadoutTreasures)
			{
				list.Add(new AdventureLoadoutTreasureRewardData(loadoutTreasureRecord, false));
			}
		}
		if (upgradedLoadoutTreasures != null)
		{
			foreach (AdventureLoadoutTreasuresDbfRecord loadoutTreasureRecord2 in upgradedLoadoutTreasures)
			{
				list.Add(new AdventureLoadoutTreasureRewardData(loadoutTreasureRecord2, true));
			}
		}
		this.LoadRewards(list, new Reward.DelOnRewardLoaded(this.OnRewardObjectLoaded));
		if (callback != null)
		{
			this.RegisterAllPopupsShownListener(callback);
		}
		this.ReadyToShowPopups();
	}

	// Token: 0x06007FD7 RID: 32727 RVA: 0x00298794 File Offset: 0x00296994
	private void RegisterAllPopupsShownListener(Action callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnAllPopupsShown -= callback;
		this.OnAllPopupsShown += callback;
	}

	// Token: 0x06007FD8 RID: 32728 RVA: 0x002987A8 File Offset: 0x002969A8
	private void ClearAllPopupsShownListeners()
	{
		this.OnAllPopupsShown = delegate()
		{
		};
	}

	// Token: 0x06007FD9 RID: 32729 RVA: 0x002987CF File Offset: 0x002969CF
	public void RegisterCompletedQuestShownListener(Action<int> callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnQuestCompletedShown -= callback;
		this.OnQuestCompletedShown += callback;
	}

	// Token: 0x06007FDA RID: 32730 RVA: 0x002987E3 File Offset: 0x002969E3
	public void RemoveCompletedQuestShownListener(Action<int> callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnQuestCompletedShown -= callback;
	}

	// Token: 0x06007FDB RID: 32731 RVA: 0x002987F0 File Offset: 0x002969F0
	public void AddPopupShownListener(Action callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnPopupShown -= callback;
		this.OnPopupShown += callback;
	}

	// Token: 0x06007FDC RID: 32732 RVA: 0x00298804 File Offset: 0x00296A04
	public void RemovePopupShownListener(Action callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnPopupShown -= callback;
	}

	// Token: 0x06007FDD RID: 32733 RVA: 0x00298811 File Offset: 0x00296A11
	public void RegisterGenericRewardShownListener(Action<long> callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnGenericRewardShown -= callback;
		this.OnGenericRewardShown += callback;
	}

	// Token: 0x06007FDE RID: 32734 RVA: 0x00298825 File Offset: 0x00296A25
	public void RemoveGenericRewardShownListener(Action<long> callback)
	{
		if (callback == null)
		{
			return;
		}
		this.OnGenericRewardShown -= callback;
	}

	// Token: 0x06007FDF RID: 32735 RVA: 0x00298832 File Offset: 0x00296A32
	private void OnRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.LoadReward(reward, ref this.m_rewards);
	}

	// Token: 0x06007FE0 RID: 32736 RVA: 0x00298841 File Offset: 0x00296A41
	private void OnPurchasedCardRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.LoadReward(reward, ref this.m_purchasedCardRewards);
	}

	// Token: 0x06007FE1 RID: 32737 RVA: 0x00298850 File Offset: 0x00296A50
	private void OnGenericRewardObjectLoaded(Reward reward, object callbackData)
	{
		this.LoadReward(reward, ref this.m_genericRewards);
	}

	// Token: 0x06007FE2 RID: 32738 RVA: 0x00298860 File Offset: 0x00296A60
	private void LoadReward(Reward reward, ref List<Reward> allRewards)
	{
		reward.Hide(false);
		this.PositionReward(reward);
		allRewards.Add(reward);
		if (Reward.Type.CARD == reward.RewardType && reward is CardReward)
		{
			(reward as CardReward).MakeActorsUnlit();
		}
		SceneUtils.SetLayer(reward.gameObject, GameLayer.Default);
		this.m_numRewardsToLoad--;
		if (this.m_numRewardsToLoad > 0)
		{
			return;
		}
		RewardUtils.SortRewards(ref allRewards);
	}

	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x06007FE3 RID: 32739 RVA: 0x002988C9 File Offset: 0x00296AC9
	private bool IsLoadingRewards
	{
		get
		{
			return this.m_numRewardsToLoad > 0;
		}
	}

	// Token: 0x06007FE4 RID: 32740 RVA: 0x002988D4 File Offset: 0x00296AD4
	private void DisplayLoadedRewardObject(Reward reward, object callbackData)
	{
		reward.Hide(false);
		this.PositionReward(reward);
		SceneUtils.SetLayer(reward.gameObject, GameLayer.IgnoreFullScreenEffects);
		if (RewardUtils.ShowReward(UserAttentionBlocker.NONE, reward, false, this.GetRewardPunchScale(), this.GetRewardScale(), new AnimationUtil.DelOnShownWithPunch(this.OnRewardShown), reward))
		{
			this.m_isShowing = true;
		}
	}

	// Token: 0x06007FE5 RID: 32741 RVA: 0x00298926 File Offset: 0x00296B26
	private void PositionReward(Reward reward)
	{
		Transform transform = reward.transform;
		transform.parent = this.ChestBones.transform;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = this.GetRewardLocalPos();
	}

	// Token: 0x06007FE6 RID: 32742 RVA: 0x00298958 File Offset: 0x00296B58
	private bool ShowNextCompletedQuest()
	{
		PopupDisplayManager.<>c__DisplayClass80_0 CS$<>8__locals1 = new PopupDisplayManager.<>c__DisplayClass80_0();
		CS$<>8__locals1.<>4__this = this;
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (this.m_completedAchieves.Count == 0)
		{
			return false;
		}
		CS$<>8__locals1.completedAchieve = this.m_completedAchieves[0];
		this.m_isShowing = true;
		this.OnPopupShown();
		UserAttentionBlocker userAttentionBlocker = CS$<>8__locals1.completedAchieve.GetUserAttentionBlocker();
		if (ReturningPlayerMgr.Get().IsInReturningPlayerMode && CS$<>8__locals1.completedAchieve.ShowToReturningPlayer == Assets.Achieve.ShowToReturningPlayer.SUPPRESSED)
		{
			this.m_completedAchieves.Remove(CS$<>8__locals1.completedAchieve);
			CS$<>8__locals1.completedAchieve.AckCurrentProgressAndRewardNotices();
			this.m_isShowing = false;
			return true;
		}
		if (!string.IsNullOrEmpty(CS$<>8__locals1.completedAchieve.CustomVisualWidget))
		{
			AssetLoader.Get().InstantiatePrefab(CS$<>8__locals1.completedAchieve.CustomVisualWidget, new PrefabCallback<GameObject>(CS$<>8__locals1.<ShowNextCompletedQuest>g__ONAssetLoad|0), null, AssetLoadingOptions.None);
		}
		else if (!CS$<>8__locals1.completedAchieve.UseGenericRewardVisual)
		{
			this.m_completedAchieves.Remove(CS$<>8__locals1.completedAchieve);
			QuestToast.ShowQuestToast(userAttentionBlocker, delegate(object userData)
			{
				CS$<>8__locals1.<>4__this.m_isShowing = false;
			}, false, CS$<>8__locals1.completedAchieve);
			this.OnQuestCompletedShown(CS$<>8__locals1.completedAchieve.ID);
		}
		else
		{
			this.m_completedAchieves.Remove(CS$<>8__locals1.completedAchieve);
			CS$<>8__locals1.completedAchieve.AckCurrentProgressAndRewardNotices();
			CS$<>8__locals1.completedAchieve.Rewards[0].LoadRewardObject(new Reward.DelOnRewardLoaded(this.DisplayLoadedRewardObject));
		}
		return true;
	}

	// Token: 0x06007FE7 RID: 32743 RVA: 0x00298AD4 File Offset: 0x00296CD4
	private bool ShowNextUnAckedReward()
	{
		if (this.m_rewards.Count == 0)
		{
			return false;
		}
		Reward reward = this.m_rewards[0];
		this.m_rewards.RemoveAt(0);
		if (RewardUtils.ShowReward(RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData), reward, false, this.GetRewardPunchScale(), this.GetRewardScale(), new AnimationUtil.DelOnShownWithPunch(this.OnRewardShown), reward))
		{
			this.m_isShowing = true;
			this.OnPopupShown();
		}
		return true;
	}

	// Token: 0x06007FE8 RID: 32744 RVA: 0x00298B5C File Offset: 0x00296D5C
	private bool ShowNextUnAckedGenericReward()
	{
		if (this.m_genericRewards.Count == 0)
		{
			return false;
		}
		this.m_isShowing = true;
		this.OnPopupShown();
		Reward reward = this.m_genericRewards[0];
		this.m_genericRewards.RemoveAt(0);
		QuestToast.ShowGenericRewardQuestToast(RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData), delegate(object userData)
		{
			this.m_isShowing = false;
		}, reward.Data, reward.Data.NameOverride, reward.Data.DescriptionOverride);
		this.OnGenericRewardShown(reward.Data.OriginData);
		return true;
	}

	// Token: 0x06007FE9 RID: 32745 RVA: 0x00298C04 File Offset: 0x00296E04
	private bool ShowNextUnAckedPurchasedCardReward()
	{
		if (QuestToast.IsQuestActive())
		{
			QuestToast.GetCurrentToast().CloseQuestToast();
		}
		if (this.m_purchasedCardRewards.Count == 0)
		{
			return false;
		}
		Reward reward = this.m_purchasedCardRewards[0];
		UserAttentionBlocker userAttentionBlockerForReward = RewardUtils.GetUserAttentionBlockerForReward(reward.Data.Origin, reward.Data.OriginData);
		if (!UserAttentionManager.CanShowAttentionGrabber(userAttentionBlockerForReward, "ShowNextUnAckedPurchasedCardReward"))
		{
			return false;
		}
		this.m_purchasedCardRewards.RemoveAt(0);
		this.m_isShowing = true;
		this.OnPopupShown();
		string name = string.Empty;
		string description = string.Empty;
		if (reward.RewardType == Reward.Type.MINI_SET)
		{
			MiniSetRewardData miniSetRewardData = reward.Data as MiniSetRewardData;
			MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(miniSetRewardData.MiniSetID);
			int count = record.DeckRecord.Cards.Count;
			name = GameStrings.FormatLocalizedString(record.DeckRecord.Name, Array.Empty<object>());
			description = GameStrings.FormatLocalizedString(record.DeckRecord.Description, new object[]
			{
				count
			});
		}
		else
		{
			CardRewardData card = reward.Data as CardRewardData;
			EntityDef entityDef = DefLoader.Get().GetEntityDef(card.CardID);
			ProductClientDataDbfRecord record2 = GameDbf.ProductClientData.GetRecord((ProductClientDataDbfRecord r) => r.PmtProductId == card.OriginData);
			if (record2 != null)
			{
				name = GameStrings.FormatLocalizedString(record2.PopupTitle, Array.Empty<object>());
				description = GameStrings.FormatLocalizedString(record2.PopupBody, new object[]
				{
					entityDef.GetName()
				});
			}
			else
			{
				name = GameStrings.Get("GLUE_GENERIC_RANDOM_CARD_SCROLL_TITLE");
				description = GameStrings.Format("GLUE_GENERIC_RANDOM_CARD_SCROLL_DESC", new object[]
				{
					entityDef.GetName()
				});
			}
		}
		QuestToast.ShowQuestToastPopup(userAttentionBlockerForReward, delegate(object userData)
		{
			this.m_isShowing = false;
		}, null, reward.Data, name, description, false, false, null);
		return true;
	}

	// Token: 0x06007FEA RID: 32746 RVA: 0x00298DE0 File Offset: 0x00296FE0
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
			if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(profileNoticeTavernBrawlRewards.NoticeID);
			}
			else if (ReturningPlayerMgr.Get().SuppressOldPopups)
			{
				Network.Get().AckNotice(profileNoticeTavernBrawlRewards.NoticeID);
				Log.ReturningPlayer.Print("Suppressing popup for TavernBrawlRewardRewards due to being a Returning Player!", Array.Empty<object>());
			}
			else
			{
				this.m_isShowing = true;
				this.OnPopupShown();
				Transform chestRewardBoneForScene = this.GetChestRewardBoneForScene(null);
				if (chestRewardBoneForScene == null)
				{
					Log.All.PrintWarning("No bone set for reward chest in scene={0}!", new object[]
					{
						SceneMgr.Get().GetMode()
					});
					return false;
				}
				List<RewardData> rewards = Network.ConvertRewardChest(profileNoticeTavernBrawlRewards.Chest).Rewards;
				RewardUtils.ShowTavernBrawlRewards(profileNoticeTavernBrawlRewards.Wins, rewards, chestRewardBoneForScene, new Action(this.ShowChestRewardsWhenReady_DoneCallback), true, profileNoticeTavernBrawlRewards);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06007FEB RID: 32747 RVA: 0x00298F1C File Offset: 0x0029711C
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
		NetCache.NetCacheProfileNotices netCacheProfileNotices = (NetCache.Get() != null) ? NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>() : null;
		if (netCacheProfileNotices == null || netCacheProfileNotices.Notices == null)
		{
			return false;
		}
		NetCache.ProfileNoticeLeaguePromotionRewards profileNoticeLeaguePromotionRewards = (NetCache.ProfileNoticeLeaguePromotionRewards)netCacheProfileNotices.Notices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.LEAGUE_PROMOTION_REWARDS);
		if (profileNoticeLeaguePromotionRewards != null)
		{
			Network network = Network.Get();
			if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
			{
				if (network != null)
				{
					network.AckNotice(profileNoticeLeaguePromotionRewards.NoticeID);
				}
			}
			else if (ReturningPlayerMgr.Get().SuppressOldPopups)
			{
				if (network != null)
				{
					network.AckNotice(profileNoticeLeaguePromotionRewards.NoticeID);
					Log.ReturningPlayer.Print("Suppressing popup for ProfileNoticeLeaguePromotionRewards due to being a Returning Player!", Array.Empty<object>());
				}
			}
			else
			{
				this.m_isShowing = true;
				this.OnPopupShown();
				Transform chestRewardBoneForScene = this.GetChestRewardBoneForScene(null);
				if (chestRewardBoneForScene == null)
				{
					Log.All.PrintWarning("No bone set for reward chest in scene={0}!", new object[]
					{
						SceneMgr.Get().GetMode()
					});
					return false;
				}
				List<RewardData> rewards = Network.ConvertRewardChest(profileNoticeLeaguePromotionRewards.Chest).Rewards;
				RewardUtils.ShowLeaguePromotionRewards(profileNoticeLeaguePromotionRewards.LeagueId, rewards, chestRewardBoneForScene, new Action(this.ShowNextLeaguePromotionReward_DoneCallback), true, profileNoticeLeaguePromotionRewards.NoticeID);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06007FEC RID: 32748 RVA: 0x0029908C File Offset: 0x0029728C
	public void ShowRankedIntro()
	{
		this.m_shouldShowRankedIntro = true;
	}

	// Token: 0x06007FED RID: 32749 RVA: 0x00299098 File Offset: 0x00297298
	private bool ShowNextRankedIntro()
	{
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextRankedIntro"))
		{
			return false;
		}
		if (!this.m_shouldShowRankedIntro)
		{
			return false;
		}
		this.m_isShowing = true;
		this.m_shouldShowRankedIntro = false;
		DialogManager.Get().ShowRankedIntroPopUp(null);
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		DialogManager.Get().ShowBonusStarsPopup(localPlayerMedalInfo.CreateDataModel(FormatType.FT_STANDARD, RankedMedal.DisplayMode.Default, false, false, null), delegate
		{
			this.m_rankedIntroShown = true;
			this.m_isShowing = false;
		});
		return true;
	}

	// Token: 0x06007FEE RID: 32750 RVA: 0x0029910C File Offset: 0x0029730C
	private bool ShowNextFreeDeckReward()
	{
		PopupDisplayManager.<>c__DisplayClass88_0 CS$<>8__locals1 = new PopupDisplayManager.<>c__DisplayClass88_0();
		CS$<>8__locals1.<>4__this = this;
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.FATAL_ERROR_SCENE) || !UserAttentionManager.CanShowAttentionGrabber("PopupDisplayManager.ShowNextFreeDeckReward"))
		{
			return false;
		}
		bool didPromoteSelfThisSession = RankMgr.Get().DidPromoteSelfThisSession;
		if (didPromoteSelfThisSession && !this.m_rankedIntroShown)
		{
			return false;
		}
		if (didPromoteSelfThisSession && (CollectionManager.Get().AccountEverHadWildCards() || CollectionManager.Get().AccountHasRotatedItems()) && Options.Get().GetInt(Option.SET_ROTATION_INTRO_PROGRESS, 0) != 6)
		{
			return false;
		}
		if (this.m_freeDeckStatus == PopupDisplayManager.FreeDeckStatus.CHOOSING)
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
			if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
			{
				Network.Get().AckNotice(profileNoticeFreeDeckChoice.NoticeID);
				netObject.Notices.Remove(profileNoticeFreeDeckChoice);
				return false;
			}
			netObject.Notices.Remove(profileNoticeFreeDeckChoice);
			this.m_freeDeckNoticeIdBeingProcessed = profileNoticeFreeDeckChoice.NoticeID;
			this.m_freeDeckStatus = PopupDisplayManager.FreeDeckStatus.CHOOSING;
			this.m_isShowing = true;
			AssetLoader.Get().InstantiatePrefab(PopupDisplayManager.CHOOSE_A_DECK_PREFAB, new PrefabCallback<GameObject>(CS$<>8__locals1.<ShowNextFreeDeckReward>g__ONAssetLoad|2), null, AssetLoadingOptions.None);
			return true;
		}
		else
		{
			CS$<>8__locals1.deckRewardNotice = (NetCache.ProfileNoticeDeckGranted)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.DECK_GRANTED);
			if (CS$<>8__locals1.deckRewardNotice != null)
			{
				this.m_freeDeckStatus = PopupDisplayManager.FreeDeckStatus.IDLE;
				this.m_isShowing = true;
				this.UpdateOfflineDeckCache();
				DeckRewardData rewardData = RewardUtils.CreateDeckRewardData(CS$<>8__locals1.deckRewardNotice.DeckDbiID, CS$<>8__locals1.deckRewardNotice.ClassId);
				DbfLocValue name = GameDbf.Deck.GetRecord((DeckDbfRecord deckRecord) => deckRecord.ID == CS$<>8__locals1.deckRewardNotice.DeckDbiID).Name;
				this.ShowDeckRewardToast(CS$<>8__locals1.deckRewardNotice, rewardData, name, GameStrings.Get("GLUE_FREE_DECK_TITLE"), GameStrings.Get("GLUE_FREE_DECK_DESC"));
				Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, CS$<>8__locals1.deckRewardNotice.PlayerDeckID);
				return true;
			}
			return this.m_freeDeckStatus == PopupDisplayManager.FreeDeckStatus.WAITING_FOR_GRANT;
		}
	}

	// Token: 0x06007FEF RID: 32751 RVA: 0x0029933C File Offset: 0x0029753C
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
		this.m_isShowing = true;
		this.UpdateOfflineDeckCache();
		DeckTemplateDbfRecord deckTemplateDbfRecord;
		if (!RewardUtils.AttemptDeckTemplateLookupFromSellableDeckId(deckRewardNotice.SellableDeckID, out deckTemplateDbfRecord))
		{
			return false;
		}
		if (deckRewardNotice.WasDeckGranted)
		{
			int num = 1;
			int count = deckTemplateDbfRecord.DeckRecord.Cards.Count;
			string displayTitle = GameStrings.Get("GLUE_SELLABLE_DECK_TITLE");
			string displayDescription = GameStrings.Format("GLUE_SELLABLE_DECK_DESC", new object[]
			{
				count,
				num
			});
			DeckRewardData rewardData = RewardUtils.CreateDeckRewardData(deckTemplateDbfRecord.DeckId, deckTemplateDbfRecord.ClassId);
			this.ShowDeckRewardToast(deckRewardNotice, rewardData, deckTemplateDbfRecord.DeckRecord.Name, displayTitle, displayDescription);
			RewardUtils.SetNewRewardedDeck(deckRewardNotice.PlayerDeckID);
			return true;
		}
		RewardUtils.CopyDeckTemplateRecordToClipboard(deckTemplateDbfRecord, false);
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_SELLABLE_DECK_FULL_LIST_HEADER"),
			m_text = GameStrings.Get("GLUE_SELLABLE_DECK_FULL_LIST_BODY"),
			m_responseDisplay = AlertPopup.ResponseDisplay.OK,
			m_showAlertIcon = true,
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				this.m_isShowing = false;
				Network.Get().AckNotice(deckRewardNotice.NoticeID);
			}
		};
		DialogManager.Get().ShowPopup(info);
		this.OnPopupShown();
		return true;
	}

	// Token: 0x06007FF0 RID: 32752 RVA: 0x002994F4 File Offset: 0x002976F4
	private void ShowDeckRewardToast(NetCache.ProfileNotice profileNotice, DeckRewardData rewardData, DbfLocValue deckName, string displayTitle, string displayDescription)
	{
		QuestToast.ShowFixedRewardQuestToast(UserAttentionBlocker.NONE, delegate(object userData)
		{
			this.m_isShowing = false;
			Network.Get().AckNotice(profileNotice.NoticeID);
			Network.Get().RenameDeck(profileNotice.OriginData, deckName);
		}, rewardData, displayTitle, displayDescription);
		this.m_deckRewardIds.Add(profileNotice.OriginData);
	}

	// Token: 0x06007FF1 RID: 32753 RVA: 0x0029954A File Offset: 0x0029774A
	private void UpdateOfflineDeckCache()
	{
		CollectionManager.Get().AddOnNetCacheDecksProcessedListener(new Action(this.OnCollectionManagerUpdatedNetCacheDecks));
		NetCache.Get().RefreshNetObject<NetCache.NetCacheDecks>();
		NetCache.Get().RefreshNetObject<NetCache.NetCacheHeroLevels>();
	}

	// Token: 0x06007FF2 RID: 32754 RVA: 0x00299578 File Offset: 0x00297778
	private void OnCollectionManagerUpdatedNetCacheDecks()
	{
		foreach (long num in this.m_deckRewardIds)
		{
			Network.Get().RequestDeckContents(new long[]
			{
				num
			});
		}
		CollectionManager.Get().RemoveOnNetCacheDecksProcessedListener(new Action(this.OnCollectionManagerUpdatedNetCacheDecks));
	}

	// Token: 0x06007FF3 RID: 32755 RVA: 0x002995F0 File Offset: 0x002977F0
	private void OnGetDeckContentsResponse()
	{
		List<DeckContents> list = new List<DeckContents>();
		foreach (DeckContents deckContents in Network.Get().GetDeckContentsResponse().Decks)
		{
			if (this.m_deckRewardIds.Contains(deckContents.DeckId))
			{
				list.Add(deckContents);
			}
		}
		if (list.Count > 0)
		{
			List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
			OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
			foreach (DeckContents deckContents2 in list)
			{
				OfflineDataCache.CacheLocalAndOriginalDeckContents(deckContents2, deckContents2);
				this.m_deckRewardIds.Remove(deckContents2.DeckId);
			}
		}
	}

	// Token: 0x06007FF4 RID: 32756 RVA: 0x002996D0 File Offset: 0x002978D0
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
				m_responseCallback = delegate(AlertPopup.Response popupResponse, object userData)
				{
					this.m_freeDeckStatus = PopupDisplayManager.FreeDeckStatus.IDLE;
				}
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			this.m_freeDeckStatus = PopupDisplayManager.FreeDeckStatus.WAITING_FOR_GRANT;
		}
		this.m_freeDeckNoticeIdBeingProcessed = 0L;
	}

	// Token: 0x06007FF5 RID: 32757 RVA: 0x00299758 File Offset: 0x00297958
	private bool ShowNextQuestChestReward()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		NetCache.ProfileNoticeGenericRewardChest rewardChestNotice = (NetCache.ProfileNoticeGenericRewardChest)netObject.Notices.Find((NetCache.ProfileNotice n) => n.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && n.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE);
		if (rewardChestNotice == null)
		{
			return false;
		}
		if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
		{
			Network.Get().AckNotice(rewardChestNotice.NoticeID);
			netObject.Notices.Remove(rewardChestNotice);
			return false;
		}
		global::Achievement achievement = AchieveManager.Get().GetAchievement((int)rewardChestNotice.OriginData);
		if (!achievement.HasRewardChestVisuals)
		{
			Log.Achievements.PrintError("Achieve id = {0} not properly set up for chest visuals", new object[]
			{
				(int)rewardChestNotice.OriginData
			});
			return false;
		}
		this.m_isShowing = true;
		List<RewardData> rewards = Network.ConvertRewardChest(rewardChestNotice.RewardChest).Rewards;
		RewardUtils.ShowQuestChestReward(achievement.Name, achievement.Description, rewards, this.GetChestRewardBoneForScene(this.QuestChestBones), delegate
		{
			Network.Get().AckNotice(rewardChestNotice.NoticeID);
			this.OnPopupClosed();
		}, true, achievement.ID, achievement.ChestVisualPrefabPath);
		return true;
	}

	// Token: 0x06007FF6 RID: 32758 RVA: 0x00299890 File Offset: 0x00297A90
	private bool ShowNextDuelsReward()
	{
		if (DuelsConfig.Get().IsReadyToShowRewards())
		{
			NetCache.ProfileNoticeGenericRewardChest rewardNoticeToShow = DuelsConfig.Get().GetRewardNoticeToShow();
			if (rewardNoticeToShow != null)
			{
				this.m_isShowing = true;
				DuelsConfig.Get().ShowRewardsForNotice(rewardNoticeToShow, new Action(this.OnPopupClosed), this.GetChestRewardBoneForScene(null));
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007FF7 RID: 32759 RVA: 0x002998E0 File Offset: 0x00297AE0
	private bool ShowFixedRewards(HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		if (!FixedRewardsMgr.Get().HasRewardsToShow(rewardTimings))
		{
			return false;
		}
		Log.Achievements.Print("PopupDisplayManager: Showing Fixed Rewards", Array.Empty<object>());
		if (!FixedRewardsMgr.Get().ShowFixedRewards(UserAttentionBlocker.NONE, rewardTimings, delegate
		{
			this.m_isShowing = false;
		}, null))
		{
			this.m_isShowing = false;
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FF8 RID: 32760 RVA: 0x00299947 File Offset: 0x00297B47
	private bool ShowNextProgressionAchievementReward()
	{
		if (!AchievementManager.Get().ShowNextReward(new Action(this.OnPopupClosed)))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FF9 RID: 32761 RVA: 0x00299976 File Offset: 0x00297B76
	private bool ShowNextProgressionQuestReward()
	{
		if (!QuestManager.Get().ShowNextReward(new Action(this.OnPopupClosed)))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFA RID: 32762 RVA: 0x002999A5 File Offset: 0x00297BA5
	private bool ShowNextProgressionTrackReward()
	{
		if (!RewardTrackManager.Get().ShowNextReward(new Action(this.OnPopupClosed)))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFB RID: 32763 RVA: 0x002999D4 File Offset: 0x00297BD4
	private bool ShowRewardTrackXpGains()
	{
		RewardXpNotificationManager rewardXpNotificationManager = RewardXpNotificationManager.Get();
		if (!rewardXpNotificationManager.HasXpGainsToShow)
		{
			return false;
		}
		rewardXpNotificationManager.ShowXpNotificationsImmediate(delegate
		{
			this.m_isShowing = false;
		});
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFC RID: 32764 RVA: 0x00299A0B File Offset: 0x00297C0B
	private bool ShowRewardTrackSeasonRoll()
	{
		if (!RewardTrackManager.Get().ShowUnclaimedTrackRewardsPopup(new Action(this.OnPopupClosed)))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFD RID: 32765 RVA: 0x00299A3A File Offset: 0x00297C3A
	private bool ShowNextQuestNotification()
	{
		if (JournalPopup.s_isShowing)
		{
			return false;
		}
		if (!QuestManager.Get().ShowQuestNotification(new Action(this.OnPopupClosed)))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFE RID: 32766 RVA: 0x00299A74 File Offset: 0x00297C74
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
		if (!AchieveManager.Get().HasQuestsToShow(true))
		{
			return false;
		}
		if (!WelcomeQuests.Show(UserAttentionBlocker.NONE, false, new WelcomeQuests.DelOnWelcomeQuestsClosed(this.OnPopupClosed), false))
		{
			return false;
		}
		this.OnPopupShown();
		this.m_isShowing = true;
		return true;
	}

	// Token: 0x06007FFF RID: 32767 RVA: 0x00299B0C File Offset: 0x00297D0C
	private void ShowQuestProgressToasts()
	{
		if (!UserAttentionManager.CanShowAttentionGrabber("ShowQuestProgressToasts"))
		{
			return;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE && UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (!QuestManager.Get().IsSystemEnabled)
		{
			if (this.m_progressedAchieves.Count > 0)
			{
				GameToastMgr.Get().ShowQuestProgressToasts(this.m_progressedAchieves);
				this.m_progressedAchieves.Clear();
			}
			return;
		}
		QuestToastManager questToastManager = QuestToastManager.Get();
		if (questToastManager == null)
		{
			return;
		}
		questToastManager.ShowQuestProgress();
	}

	// Token: 0x06008000 RID: 32768 RVA: 0x00299B88 File Offset: 0x00297D88
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
		foreach (CollectibleCard collectibleCard in list)
		{
			if (!flag && (CollectionManager.Get().IsCardInCollection(collectibleCard.CardId, TAG_PREMIUM.NORMAL) || CollectionManager.Get().IsCardInCollection(collectibleCard.CardId, TAG_PREMIUM.GOLDEN)))
			{
				flag = true;
			}
		}
		if (Cheats.ShowFakeNerfedCards && !flag && !this.m_hasShownCheatedChangedCards)
		{
			UIStatus.Get().AddInfo("SHOWING FAKE NERFED CARDS!\nTo disable this, remove ShowFakeNerfedCards from client.config.", 5f);
			list = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL).Take(3).ToList<CollectibleCard>();
			flag = true;
			this.m_hasShownCheatedChangedCards = true;
		}
		foreach (CollectibleCard collectibleCard2 in list)
		{
			ChangedCardMgr.Get().MarkCardChangeSeen(collectibleCard2.ChangeVersion, collectibleCard2.CardDbId);
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

	// Token: 0x06008001 RID: 32769 RVA: 0x00299D5C File Offset: 0x00297F5C
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
			List<CardChange.ChangeType> changeTypes = new List<CardChange.ChangeType>
			{
				CardChange.ChangeType.ADDITION
			};
			list = CollectionManager.Get().GetChangedCards(changeTypes, TAG_PREMIUM.NORMAL, featuredCardsEventTiming);
		}
		if (Cheats.ShowFakeAddedCards && !this.m_hasShownCheatedAddedCards)
		{
			UIStatus.Get().AddInfo("SHOWING FAKE ADDED CARDS!\nTo disable this, remove ShowFakeAddedCards from client.config.", 5f);
			list = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL).Take(3).ToList<CollectibleCard>();
			this.m_hasShownCheatedAddedCards = true;
		}
		if (list.Count > 0)
		{
			foreach (CollectibleCard collectibleCard in list)
			{
				ChangedCardMgr.Get().MarkCardChangeSeen(collectibleCard.ChangeVersion, collectibleCard.CardDbId);
			}
			if (!ReturningPlayerMgr.Get().IsInReturningPlayerMode)
			{
				CardListPopup.Info info = new CardListPopup.Info();
				info.m_description = ((list.Count == 1) ? GameStrings.Get("GLUE_SINGLE_CARD_ADDED") : GameStrings.Format("GLUE_CARDS_ADDED", new object[]
				{
					list.Count
				}));
				info.m_cards = list;
				info.m_callbackOnHide = callbackOnHide;
				info.m_useMultiLineDescription = info.m_description.Contains('\n');
				DialogManager.Get().ShowCardListPopup(ignoredAttentionBlockers, info);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008002 RID: 32770 RVA: 0x00299EE0 File Offset: 0x002980E0
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
		if (!changedCards.Any<CollectibleCard>())
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

	// Token: 0x06008003 RID: 32771 RVA: 0x00299F78 File Offset: 0x00298178
	public bool ShowLoginPopupSequence()
	{
		if (!UserAttentionManager.CanShowAttentionGrabber("ShowLoginPopupSequence"))
		{
			return false;
		}
		List<LoginPopupSequenceDbfRecord> records = GameDbf.LoginPopupSequence.GetRecords((LoginPopupSequenceDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.EventTiming, false), -1);
		if (records == null || records.Count == 0)
		{
			return false;
		}
		if (PopupDisplayManager.ShouldDisableNotificationOnLogin())
		{
			List<long> list = new List<long>();
			foreach (LoginPopupSequenceDbfRecord loginPopupSequenceDbfRecord in records)
			{
				list.Add((long)loginPopupSequenceDbfRecord.ID);
			}
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, list.ToArray()), null);
			return false;
		}
		bool result = false;
		List<long> list2;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, out list2);
		using (List<LoginPopupSequenceDbfRecord>.Enumerator enumerator = records.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				LoginPopupSequenceDbfRecord popupSequenceRecord = enumerator.Current;
				if (list2 == null || !list2.Contains((long)popupSequenceRecord.ID))
				{
					List<LoginPopupSequencePopupDbfRecord> records2 = GameDbf.LoginPopupSequencePopup.GetRecords((LoginPopupSequencePopupDbfRecord r) => r.LoginPopupSequenceId == popupSequenceRecord.ID, -1);
					int i = 0;
					DialogBase.HideCallback <>9__2;
					while (i < records2.Count)
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
							if (flag)
							{
								DialogBase.HideCallback hideCallback;
								if ((hideCallback = <>9__2) == null)
								{
									hideCallback = (<>9__2 = delegate(DialogBase dialog, object userData)
									{
										this.OnPopupSequenceDismissed(popupSequenceRecord.ID);
									});
								}
								callbackOnHide = hideCallback;
								goto IL_1D9;
							}
							this.OnPopupSequenceDismissed(popupSequenceRecord.ID);
							break;
						}
						else if (flag)
						{
							goto IL_1D9;
						}
						IL_2CB:
						i++;
						continue;
						IL_1D9:
						if (popupType <= Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType.BASIC || popupType != Assets.LoginPopupSequencePopup.LoginPopupSequencePopupType.FEATURED_CARDS)
						{
							global::LoginPopupSequencePopup.Info info = new global::LoginPopupSequencePopup.Info
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
								info.m_card = CollectionManager.Get().GetCard(GameUtils.TranslateDbIdToCardId(loginPopupSequencePopupDbfRecord.CardId, false), cardPremium);
							}
							DialogManager.Get().ShowLoginPopupSequenceBasicPopup(UserAttentionBlocker.NONE, info);
							result = true;
							goto IL_2CB;
						}
						if (this.ShowFeaturedCards(loginPopupSequencePopupDbfRecord.FeaturedCardsEvent, loginPopupSequencePopupDbfRecord.HeaderText, callbackOnHide, UserAttentionBlocker.NONE))
						{
							result = true;
							goto IL_2CB;
						}
						if (flag2)
						{
							this.OnPopupSequenceDismissed(popupSequenceRecord.ID);
							goto IL_2CB;
						}
						goto IL_2CB;
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06008004 RID: 32772 RVA: 0x0029A2B8 File Offset: 0x002984B8
	private void OnPopupSequenceDismissed(int popupSequenceId)
	{
		List<long> list;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, out list);
		if (list == null)
		{
			list = new List<long>();
		}
		list.Add((long)popupSequenceId);
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, list.ToArray()), null);
	}

	// Token: 0x06008005 RID: 32773 RVA: 0x0029A310 File Offset: 0x00298510
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

	// Token: 0x06008006 RID: 32774 RVA: 0x0029A380 File Offset: 0x00298580
	public Vector3 GetRewardScale()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = Vector3.one,
				Phone = new Vector3(0.8f, 0.8f, 0.8f)
			};
		}
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(10f, 10f, 10f),
				Phone = new Vector3(7f, 7f, 7f)
			};
		}
		if (mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.LOGIN)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15f, 15f, 15f),
				Phone = new Vector3(14f, 14f, 14f)
			};
		}
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(15f, 15f, 15f),
			Phone = new Vector3(8f, 8f, 8f)
		};
	}

	// Token: 0x06008007 RID: 32775 RVA: 0x0029A49C File Offset: 0x0029869C
	public Vector3 GetRewardPunchScale()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(1.2f, 1.2f, 1.2f),
				Phone = new Vector3(1.25f, 1.25f, 1.25f)
			};
		}
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(10.2f, 10.2f, 10.2f),
				Phone = new Vector3(7.1f, 7.1f, 7.1f)
			};
		}
		if (mode == SceneMgr.Mode.STARTUP || mode == SceneMgr.Mode.LOGIN)
		{
			return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(15.1f, 15.1f, 15.1f),
				Phone = new Vector3(14.1f, 14.1f, 14.1f)
			};
		}
		return new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
		{
			PC = new Vector3(15.1f, 15.1f, 15.1f),
			Phone = new Vector3(8.1f, 8.1f, 8.1f)
		};
	}

	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x06008008 RID: 32776 RVA: 0x0029A5C4 File Offset: 0x002987C4
	public bool IsShowing
	{
		get
		{
			return this.m_isShowing || (DialogManager.Get() != null && DialogManager.Get().ShowingDialog()) || WelcomeQuests.Get() != null || (NarrativeManager.Get() != null && NarrativeManager.Get().IsShowingBlockingDialog()) || BannerManager.Get().IsShowing || RewardXpNotificationManager.Get().IsShowingXpGains;
		}
	}

	// Token: 0x06008009 RID: 32777 RVA: 0x0029A63D File Offset: 0x0029883D
	public IEnumerator WaitForAllPopups()
	{
		bool allPopupsShown = false;
		this.RegisterAllPopupsShownListener(delegate
		{
			allPopupsShown = true;
		});
		while (!allPopupsShown)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600800A RID: 32778 RVA: 0x0029A64C File Offset: 0x0029884C
	public IEnumerator<IAsyncJobResult> Job_WaitForAllPopups()
	{
		this.ReadyToShowPopups();
		bool allPopupsShown = false;
		this.RegisterAllPopupsShownListener(delegate
		{
			allPopupsShown = true;
		});
		while (!allPopupsShown)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600800B RID: 32779 RVA: 0x0029A65C File Offset: 0x0029885C
	public static bool ShouldDisableNotificationOnLogin()
	{
		return !HearthstoneApplication.IsPublic() && !StoreManager.Get().IsShown() && Options.Get().GetBool(Option.DISABLE_LOGIN_POPUPS) && (!PopupDisplayManager.m_hasPlayerReachedHub || Time.realtimeSinceStartup - PopupDisplayManager.m_timePlayerInHubAfterLogin < 20f);
	}

	// Token: 0x0600800C RID: 32780 RVA: 0x0029A6AB File Offset: 0x002988AB
	private void InitializePlayerTimeInHubAfterLogin()
	{
		PopupDisplayManager.m_hasPlayerReachedHub = true;
		PopupDisplayManager.m_timePlayerInHubAfterLogin = Time.realtimeSinceStartup;
	}

	// Token: 0x0600800D RID: 32781 RVA: 0x0029A6C0 File Offset: 0x002988C0
	private void UpdateRewards()
	{
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		List<RewardData> list = new List<RewardData>();
		List<RewardData> list2 = new List<RewardData>();
		List<RewardData> rewardsToLoad = new List<RewardData>();
		if (netObject != null)
		{
			AchieveManager achieveMgr = AchieveManager.Get();
			List<NetCache.ProfileNotice> noticesToAck = new List<NetCache.ProfileNotice>();
			List<NetCache.ProfileNotice> notices = netObject.Notices.Where(delegate(NetCache.ProfileNotice n)
			{
				if (n.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && n.Origin == NetCache.ProfileNotice.NoticeOrigin.GENERIC_REWARD_CHEST_ACHIEVE)
				{
					global::Achievement achievement2 = AchieveManager.Get().GetAchievement((int)n.OriginData);
					if (achievement2 != null && achievement2.HasRewardChestVisuals)
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
				return n.Type != NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST || this.m_genericRewardChestNoticeIdsReady.Any((long r) => n.NoticeID == r);
			}).ToList<NetCache.ProfileNotice>();
			Network network = Network.Get();
			foreach (NetCache.ProfileNotice profileNotice in noticesToAck)
			{
				network.AckNotice(profileNotice.NoticeID);
			}
			List<RewardData> rewards = RewardUtils.GetRewards(notices);
			HashSet<Assets.Achieve.RewardTiming> hashSet = new HashSet<Assets.Achieve.RewardTiming>();
			foreach (object obj in Enum.GetValues(typeof(Assets.Achieve.RewardTiming)))
			{
				Assets.Achieve.RewardTiming item = (Assets.Achieve.RewardTiming)obj;
				hashSet.Add(item);
			}
			RewardUtils.GetViewableRewards(rewards, hashSet, out list, out list2, ref rewardsToLoad, ref this.m_completedAchieves);
		}
		if (ReturningPlayerMgr.Get().SuppressOldPopups)
		{
			List<global::Achievement> list3 = new List<global::Achievement>();
			foreach (global::Achievement achievement in this.m_completedAchieves)
			{
				Assets.Achieve.ShowToReturningPlayer showToReturningPlayer = achievement.ShowToReturningPlayer;
				if (showToReturningPlayer == Assets.Achieve.ShowToReturningPlayer.SUPPRESSED)
				{
					Log.ReturningPlayer.Print("Suppressing popup for Achievement {0} due to being a Returning Player!", new object[]
					{
						achievement
					});
					achievement.AckCurrentProgressAndRewardNotices();
				}
				else
				{
					list3.Add(achievement);
				}
			}
			this.m_completedAchieves = list3;
			list2.RemoveAll(delegate(RewardData rewardData)
			{
				if (rewardData.RewardChestAssetId == null)
				{
					PopupDisplayManager.<UpdateRewards>g__AckNotices|120_2(rewardData);
					return true;
				}
				RewardChestDbfRecord record = GameDbf.RewardChest.GetRecord(rewardData.RewardChestAssetId.Value);
				if (record == null || !record.ShowToReturningPlayer)
				{
					PopupDisplayManager.<UpdateRewards>g__AckNotices|120_2(rewardData);
					return true;
				}
				return false;
			});
		}
		if (!PopupDisplayManager.ShouldDisableNotificationOnLogin())
		{
			this.LoadRewards(list, new Reward.DelOnRewardLoaded(this.OnRewardObjectLoaded));
			this.LoadRewards(rewardsToLoad, new Reward.DelOnRewardLoaded(this.OnPurchasedCardRewardObjectLoaded));
			this.LoadRewards(list2, new Reward.DelOnRewardLoaded(this.OnGenericRewardObjectLoaded));
		}
		Log.Achievements.Print("PopupDisplayManager: adding {0} rewards to load total={1}", new object[]
		{
			list.Count,
			this.m_numRewardsToLoad
		});
	}

	// Token: 0x0600800E RID: 32782 RVA: 0x0029A93C File Offset: 0x00298B3C
	private void LoadRewards(List<RewardData> rewardsToLoad, Reward.DelOnRewardLoaded callback)
	{
		foreach (RewardData rewardData in rewardsToLoad)
		{
			if (this.UpdateNoticesSeen(rewardData))
			{
				if (ReturningPlayerMgr.Get().SuppressOldPopups && (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.TOURNEY || rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.TAVERN_BRAWL_REWARD || rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.LEAGUE_PROMOTION))
				{
					Log.ReturningPlayer.Print("Suppressing popup for Reward {0} due to being a Returning Player!", new object[]
					{
						rewardData
					});
					rewardData.AcknowledgeNotices();
				}
				else
				{
					this.m_numRewardsToLoad++;
					rewardData.LoadRewardObject(callback);
				}
			}
		}
	}

	// Token: 0x0600800F RID: 32783 RVA: 0x0029A9EC File Offset: 0x00298BEC
	private void OnRewardShown(object callbackData)
	{
		Reward reward = callbackData as Reward;
		if (reward == null)
		{
			return;
		}
		reward.RegisterClickListener(new Reward.OnClickedCallback(this.OnRewardClicked));
		reward.EnableClickCatcher(true);
	}

	// Token: 0x06008010 RID: 32784 RVA: 0x0029AA24 File Offset: 0x00298C24
	private void ShowChestRewardsWhenReady_DoneCallback()
	{
		this.m_isShowing = false;
	}

	// Token: 0x06008011 RID: 32785 RVA: 0x0029AA2D File Offset: 0x00298C2D
	private void ShowNextLeaguePromotionReward_DoneCallback()
	{
		this.ShowRankedIntro();
		this.m_isShowing = false;
	}

	// Token: 0x06008012 RID: 32786 RVA: 0x0029AA3C File Offset: 0x00298C3C
	private void OnRewardClicked(Reward reward, object userData)
	{
		reward.RemoveClickListener(new Reward.OnClickedCallback(this.OnRewardClicked));
		reward.Hide(true);
		this.m_isShowing = false;
	}

	// Token: 0x06008013 RID: 32787 RVA: 0x0029AA24 File Offset: 0x00298C24
	private void OnPopupClosed()
	{
		this.m_isShowing = false;
	}

	// Token: 0x06008014 RID: 32788 RVA: 0x0029AA5F File Offset: 0x00298C5F
	private bool CanShowPopups()
	{
		return SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY || (!(EndGameScreen.Get() == null) && EndGameScreen.Get().IsDoneDisplayingRewards());
	}

	// Token: 0x06008015 RID: 32789 RVA: 0x0029AA8C File Offset: 0x00298C8C
	private void OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userData)
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
		this.PrepareNewlyProgressedAchievesToBeShown();
		this.PrepareNewlyCompletedAchievesToBeShown(hashSet);
	}

	// Token: 0x06008016 RID: 32790 RVA: 0x0029AADE File Offset: 0x00298CDE
	private void PrepareNewlyProgressedAchievesToBeShown()
	{
		this.m_progressedAchieves = AchieveManager.Get().GetNewlyProgressedQuests();
	}

	// Token: 0x06008017 RID: 32791 RVA: 0x0029AAF0 File Offset: 0x00298CF0
	private void PrepareNewlyCompletedAchievesToBeShown(HashSet<Assets.Achieve.RewardTiming> rewardTimings)
	{
		if (!this.CanShowPopups())
		{
			return;
		}
		using (List<global::Achievement>.Enumerator enumerator = AchieveManager.Get().GetNewCompletedAchievesToShow().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				global::Achievement achieve = enumerator.Current;
				if (this.m_completedAchieves.Find((global::Achievement obj) => achieve.ID == obj.ID) != null)
				{
					Log.Achievements.Print("PopupDisplayManager: skipping completed achievement already being processed: " + achieve, Array.Empty<object>());
				}
				else if (rewardTimings == null || !rewardTimings.Contains(achieve.RewardTiming))
				{
					Log.Achievements.PrintDebug("PopupDisplayManager: skipping completed achievement with {0} reward timing: {1}", new object[]
					{
						achieve.RewardTiming,
						achieve
					});
				}
				else
				{
					Log.Achievements.Print("PopupDisplayManager: adding completed achievement " + achieve, Array.Empty<object>());
					this.m_completedAchieves.Add(achieve);
				}
			}
		}
		this.UpdateRewards();
	}

	// Token: 0x06008018 RID: 32792 RVA: 0x0029AC14 File Offset: 0x00298E14
	private void OnGenericRewardUpdated(long rewardNoticeId, object userData)
	{
		this.m_genericRewardChestNoticeIdsReady.Add(rewardNoticeId);
		if (!this.CanShowPopups())
		{
			return;
		}
		this.UpdateRewards();
	}

	// Token: 0x06008019 RID: 32793 RVA: 0x0029AC32 File Offset: 0x00298E32
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		if (!this.CanShowPopups())
		{
			return;
		}
		if (newNotices.Count <= 0)
		{
			return;
		}
		this.UpdateRewards();
		newNotices.ForEach(delegate(NetCache.ProfileNotice notice)
		{
			if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.CARD_REPLACEMENT)
			{
				this.m_cardReplacementNotices.Enqueue(notice);
				return;
			}
			if (notice.Origin == NetCache.ProfileNotice.NoticeOrigin.HOF_COMPENSATION && notice.Type == NetCache.ProfileNotice.NoticeType.REWARD_DUST)
			{
				this.m_dustRewardNotices.Enqueue(notice);
			}
		});
	}

	// Token: 0x0600801A RID: 32794 RVA: 0x0029AC60 File Offset: 0x00298E60
	private Transform GetChestRewardBoneForScene(PopupDisplayManagerBones boneSet = null)
	{
		PopupDisplayManagerBones popupDisplayManagerBones = (boneSet != null) ? boneSet : this.ChestBones;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		switch (mode)
		{
		case SceneMgr.Mode.LOGIN:
		case SceneMgr.Mode.HUB:
			return popupDisplayManagerBones.m_rewardChestBone_Box;
		case SceneMgr.Mode.GAMEPLAY:
		case SceneMgr.Mode.COLLECTIONMANAGER:
			break;
		case SceneMgr.Mode.PACKOPENING:
			return popupDisplayManagerBones.m_rewardChestBone_PackOpening;
		case SceneMgr.Mode.TOURNAMENT:
			return popupDisplayManagerBones.m_rewardChestBone_PlayMode;
		default:
			if (mode == SceneMgr.Mode.PVP_DUNGEON_RUN)
			{
				return popupDisplayManagerBones.m_rewardChestBone_DungeonCrawl;
			}
			break;
		}
		return null;
	}

	// Token: 0x0600801B RID: 32795 RVA: 0x0029ACD0 File Offset: 0x00298ED0
	private bool ShowInGameMessagePopups()
	{
		MessagePopupDisplay messagePopupDisplay;
		if (HearthstoneServices.TryGet<MessagePopupDisplay>(out messagePopupDisplay))
		{
			if (messagePopupDisplay.IsDisplayingMessage)
			{
				return true;
			}
			if (messagePopupDisplay.HasMessageToDisplay && this.CanDisplayIGMPopups())
			{
				this.m_isShowing = true;
				this.OnPopupShown();
				messagePopupDisplay.DisplayNextMessage(delegate
				{
					this.m_isShowing = false;
				});
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600801C RID: 32796 RVA: 0x0029AD27 File Offset: 0x00298F27
	private bool CanDisplayIGMPopups()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.HUB && !StoreManager.Get().IsShownOrWaitingToShow() && !JournalPopup.s_isShowing;
	}

	// Token: 0x06008025 RID: 32805 RVA: 0x0029AE98 File Offset: 0x00299098
	[CompilerGenerated]
	internal static void <UpdateRewards>g__AckNotices|120_2(RewardData rewardData)
	{
		foreach (long id in rewardData.GetNoticeIDs())
		{
			Network.Get().AckNotice(id);
		}
	}

	// Token: 0x04006911 RID: 26897
	private List<Reward> m_rewards = new List<Reward>();

	// Token: 0x04006912 RID: 26898
	private List<Reward> m_purchasedCardRewards = new List<Reward>();

	// Token: 0x04006913 RID: 26899
	private List<Reward> m_genericRewards = new List<Reward>();

	// Token: 0x04006914 RID: 26900
	private List<global::Achievement> m_progressedAchieves = new List<global::Achievement>();

	// Token: 0x04006915 RID: 26901
	private List<global::Achievement> m_completedAchieves = new List<global::Achievement>();

	// Token: 0x04006916 RID: 26902
	private readonly HashSet<long> m_genericRewardChestNoticeIdsReady = new HashSet<long>();

	// Token: 0x04006917 RID: 26903
	private readonly HashSet<long> m_deckRewardIds = new HashSet<long>();

	// Token: 0x04006918 RID: 26904
	private int m_numRewardsToLoad;

	// Token: 0x04006919 RID: 26905
	private bool m_hasShownCheatedChangedCards;

	// Token: 0x0400691A RID: 26906
	private bool m_hasShownCheatedAddedCards;

	// Token: 0x0400691B RID: 26907
	private readonly Dictionary<long, HashSet<int>> m_seenNotices = new Dictionary<long, HashSet<int>>();

	// Token: 0x0400691C RID: 26908
	private bool m_isShowing;

	// Token: 0x0400691D RID: 26909
	private bool m_readyToShowPopups;

	// Token: 0x0400691E RID: 26910
	private bool m_hasShownMetaShakeupEventPopups;

	// Token: 0x0400691F RID: 26911
	private bool m_hasShownCardChangePopups;

	// Token: 0x04006920 RID: 26912
	private bool m_hasShownCardAdditionPopups;

	// Token: 0x04006921 RID: 26913
	private bool m_hasCheckedNewPlayerSetRotationPopup;

	// Token: 0x04006922 RID: 26914
	private long m_freeDeckNoticeIdBeingProcessed;

	// Token: 0x04006923 RID: 26915
	private PopupDisplayManager.FreeDeckStatus m_freeDeckStatus;

	// Token: 0x04006924 RID: 26916
	private bool m_shouldShowRankedIntro;

	// Token: 0x04006925 RID: 26917
	private bool m_rankedIntroShown;

	// Token: 0x04006926 RID: 26918
	private static bool m_hasPlayerReachedHub = false;

	// Token: 0x04006927 RID: 26919
	private static float m_timePlayerInHubAfterLogin = 0f;

	// Token: 0x04006928 RID: 26920
	private const float LOGIN_DURATION_THRESHOLD = 20f;

	// Token: 0x04006929 RID: 26921
	private static string CHOOSE_A_DECK_PREFAB = "ChooseADeck.prefab:de9efdb77e14b144ea84f333a1e78926";

	// Token: 0x0400692C RID: 26924
	private readonly Queue<NetCache.ProfileNotice> m_cardReplacementNotices = new Queue<NetCache.ProfileNotice>();

	// Token: 0x0400692D RID: 26925
	private readonly Queue<NetCache.ProfileNotice> m_dustRewardNotices = new Queue<NetCache.ProfileNotice>();

	// Token: 0x020025B7 RID: 9655
	private enum FreeDeckStatus
	{
		// Token: 0x0400EE76 RID: 61046
		IDLE,
		// Token: 0x0400EE77 RID: 61047
		CHOOSING,
		// Token: 0x0400EE78 RID: 61048
		WAITING_FOR_GRANT
	}
}
