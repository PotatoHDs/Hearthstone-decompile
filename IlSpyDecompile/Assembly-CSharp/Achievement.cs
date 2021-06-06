using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.Progression;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class Achievement
{
	public enum ClickTriggerType
	{
		BUTTON_PLAY = 1,
		BUTTON_ARENA,
		BUTTON_ADVENTURE
	}

	public static readonly int NEW_ACHIEVE_ACK_PROGRESS = -1;

	private int m_id;

	private bool m_enabled;

	private string m_name = "";

	private string m_description = "";

	private Assets.Achieve.AltTextPredicate m_altTextPredicate;

	private string m_altName;

	private string m_altDescription;

	private Assets.Achieve.Type m_type;

	private int m_maxProgress;

	private TAG_RACE? m_raceReq;

	private TAG_CLASS? m_classReward;

	private TAG_CARD_SET? m_cardSetReq;

	private TAG_CLASS? m_myHeroClassReq;

	private ClickTriggerType? m_clickType;

	private SpecialEventType m_eventTrigger;

	private int m_linkToId;

	private Assets.Achieve.Trigger m_trigger = Assets.Achieve.Trigger.NONE;

	private Assets.Achieve.GameMode m_gameMode;

	private Assets.Achieve.Unlocks m_unlockedFeature = Assets.Achieve.Unlocks.NONE;

	private List<RewardData> m_rewards = new List<RewardData>();

	private List<int> m_scenarios = new List<int>();

	private int m_wingID;

	private int m_adventureID;

	private int m_adventureModeID;

	private Assets.Achieve.RewardTiming m_rewardTiming;

	private int m_boosterReq;

	private Assets.Achieve.ClientFlags m_clientFlags;

	private bool m_useGenericRewardVisual;

	private Assets.Achieve.ShowToReturningPlayer m_showToReturningPlayer;

	private int m_questDialogId;

	private bool m_autoDestroy;

	private string m_questTilePrefabName;

	private int m_onCompleteQuestDialogBannerId;

	private CharacterDialogSequence m_onReceivedDialogSequence;

	private CharacterDialogSequence m_onCompleteDialogSequence;

	private CharacterDialogSequence m_onProgress1DialogSequence;

	private CharacterDialogSequence m_onProgress2DialogSequence;

	private CharacterDialogSequence m_onDismissDialogSequence;

	private bool m_isGenericRewardChest;

	private string m_chestVisualPrefabPath = "";

	private string m_customVisualWidget = "";

	private int m_enemyHeroClassId;

	private int m_progress;

	private int m_ackProgress;

	private int m_completionCount;

	private bool m_active;

	private long m_dateGiven;

	private long m_dateCompleted;

	private bool m_canAck;

	private int m_intervalRewardCount;

	private long m_intervalRewardStartDate;

	private List<long> m_rewardNoticeIDs = new List<long>();

	public int ID => m_id;

	public bool Enabled
	{
		get
		{
			if (!DbfRecord.Enabled)
			{
				return false;
			}
			if (QuestManager.Get().IsSystemEnabled)
			{
				return DbfRecord.EnabledWithProgression;
			}
			return false;
		}
	}

	public Assets.Achieve.Type AchieveType => m_type;

	public int MaxProgress => m_maxProgress;

	public TAG_RACE? RaceRequirement => m_raceReq;

	public TAG_CLASS? ClassReward => m_classReward;

	public TAG_CARD_SET? CardSetRequirement => m_cardSetReq;

	public TAG_CLASS? MyHeroClassRequirement => m_myHeroClassReq;

	public ClickTriggerType? ClickType => m_clickType;

	public SpecialEventType EventTrigger => m_eventTrigger;

	public int LinkToId => m_linkToId;

	public Assets.Achieve.Trigger AchieveTrigger => m_trigger;

	public Assets.Achieve.GameMode Mode => m_gameMode;

	public Assets.Achieve.Unlocks UnlockedFeature => m_unlockedFeature;

	public List<RewardData> Rewards => m_rewards;

	public List<int> Scenarios => m_scenarios;

	public int WingID => m_wingID;

	public int AdventureID => m_adventureID;

	public int AdventureModeID => m_adventureModeID;

	public Assets.Achieve.RewardTiming RewardTiming => m_rewardTiming;

	public int BoosterRequirement => m_boosterReq;

	public bool UseGenericRewardVisual => m_useGenericRewardVisual;

	public Assets.Achieve.ShowToReturningPlayer ShowToReturningPlayer => m_showToReturningPlayer;

	public int QuestDialogId => m_questDialogId;

	public bool AutoDestroy => m_autoDestroy;

	public string QuestTilePrefabName => m_questTilePrefabName;

	public int OnCompleteQuestDialogBannerId => m_onCompleteQuestDialogBannerId;

	public CharacterDialogSequence OnReceivedDialogSequence => m_onReceivedDialogSequence;

	public CharacterDialogSequence OnCompleteDialogSequence => m_onCompleteDialogSequence;

	public CharacterDialogSequence OnProgress1DialogSequence => m_onProgress1DialogSequence;

	public CharacterDialogSequence OnProgress2DialogSequence => m_onProgress2DialogSequence;

	public CharacterDialogSequence OnDismissDialogSequence => m_onDismissDialogSequence;

	public AchieveDbfRecord DbfRecord { get; private set; }

	public int Progress => m_progress;

	public int AcknowledgedProgress => m_ackProgress;

	public bool CanBeAcknowledged => m_canAck;

	public int CompletionCount => m_completionCount;

	public bool Active => m_active;

	public long DateGiven => m_dateGiven;

	public long DateCompleted => m_dateCompleted;

	public int IntervalRewardCount => m_intervalRewardCount;

	public long IntervalRewardStartDate => m_intervalRewardStartDate;

	public bool IsGenericRewardChest => m_isGenericRewardChest;

	public string ChestVisualPrefabPath => m_chestVisualPrefabPath;

	public string CustomVisualWidget => m_customVisualWidget;

	public int EnemyHeroClassId => m_enemyHeroClassId;

	public bool IsLegendary => (m_clientFlags & Assets.Achieve.ClientFlags.IS_LEGENDARY) != 0;

	public bool IsAffectedByDoubleGold => (m_clientFlags & Assets.Achieve.ClientFlags.IS_AFFECTED_BY_DOUBLE_GOLD) != 0;

	public bool HasRewardChestVisuals => !string.IsNullOrEmpty(m_chestVisualPrefabPath);

	public bool CanShowInQuestLog
	{
		get
		{
			if ((m_clientFlags & Assets.Achieve.ClientFlags.SHOW_IN_QUEST_LOG) != 0)
			{
				return true;
			}
			switch (AchieveType)
			{
			case Assets.Achieve.Type.HERO:
			case Assets.Achieve.Type.GOLDHERO:
			case Assets.Achieve.Type.DAILY_REPEATABLE:
			case Assets.Achieve.Type.HIDDEN:
			case Assets.Achieve.Type.INTERNAL_ACTIVE:
			case Assets.Achieve.Type.INTERNAL_INACTIVE:
				return false;
			case Assets.Achieve.Type.STARTER:
			case Assets.Achieve.Type.DAILY:
			case Assets.Achieve.Type.NORMAL_QUEST:
				return true;
			default:
				return false;
			}
		}
	}

	public bool IsAffectedByFriendWeek
	{
		get
		{
			if ((m_clientFlags & Assets.Achieve.ClientFlags.IS_AFFECTED_BY_FRIEND_WEEK) != 0)
			{
				return true;
			}
			return false;
		}
	}

	public bool IsFriendlyChallengeQuest
	{
		get
		{
			Assets.Achieve.GameMode gameMode = m_gameMode;
			if (gameMode == Assets.Achieve.GameMode.FRIENDLY)
			{
				return true;
			}
			return false;
		}
	}

	public bool GameModeRequiresNonFriendlyChallenge
	{
		get
		{
			Assets.Achieve.GameMode gameMode = m_gameMode;
			if (gameMode == Assets.Achieve.GameMode.ANY || gameMode == Assets.Achieve.GameMode.FRIENDLY)
			{
				return false;
			}
			return true;
		}
	}

	public bool CanBeCancelled
	{
		get
		{
			if (!IsLegendary)
			{
				return AchieveType == Assets.Achieve.Type.DAILY;
			}
			return true;
		}
	}

	public PlayerType PlayerType => (PlayerType)DbfRecord.PlayerType;

	public string Name
	{
		get
		{
			if (!string.IsNullOrEmpty(m_altName) && AchieveManager.IsPredicateTrue(m_altTextPredicate))
			{
				return m_altName;
			}
			return m_name;
		}
	}

	public string Description
	{
		get
		{
			if (!string.IsNullOrEmpty(m_altDescription) && AchieveManager.IsPredicateTrue(m_altTextPredicate))
			{
				return m_altDescription;
			}
			return m_description;
		}
	}

	public Achievement()
	{
	}

	public Achievement(AchieveDbfRecord dbfRecord, int id, Assets.Achieve.Type achieveType, int maxProgress, int linkToId, Assets.Achieve.Trigger trigger, Assets.Achieve.GameMode gameMode, TAG_RACE? raceReq, TAG_CLASS? classReward, TAG_CARD_SET? cardSetReq, TAG_CLASS? myHeroClassReq, ClickTriggerType? clickType, Assets.Achieve.Unlocks unlockedFeature, List<RewardData> rewards, List<int> scenarios, int wingID, int adventureID, int adventureModeID, Assets.Achieve.RewardTiming rewardTiming, int boosterReq, bool useGenericRewardVisual, Assets.Achieve.ShowToReturningPlayer showToReturningPlayer, int questDialogId, bool autoDestroy, string questTilePrefabName, int onCompleteQuestDialogBannerId, CharacterDialogSequence onReceivedDialogSequence, CharacterDialogSequence onCompleteDialogSequence, CharacterDialogSequence onProgress1DialogSequence, CharacterDialogSequence onProgress2DialogSequence, CharacterDialogSequence onDismissDialogSequence, bool isGenericRewardChest, string chestVisualPrefabPath, string customVisualWidget, int enemyHeroClassId)
	{
		DbfRecord = ((dbfRecord == null) ? new AchieveDbfRecord() : dbfRecord);
		m_id = id;
		m_type = achieveType;
		m_maxProgress = maxProgress;
		m_linkToId = linkToId;
		m_trigger = trigger;
		m_gameMode = gameMode;
		m_raceReq = raceReq;
		m_classReward = classReward;
		m_cardSetReq = cardSetReq;
		m_myHeroClassReq = myHeroClassReq;
		m_clickType = clickType;
		SetRewards(rewards);
		m_unlockedFeature = unlockedFeature;
		m_scenarios = scenarios;
		m_wingID = wingID;
		m_adventureID = adventureID;
		m_adventureModeID = adventureModeID;
		m_rewardTiming = rewardTiming;
		m_boosterReq = boosterReq;
		m_useGenericRewardVisual = useGenericRewardVisual;
		m_showToReturningPlayer = showToReturningPlayer;
		m_questDialogId = questDialogId;
		m_autoDestroy = autoDestroy;
		m_questTilePrefabName = questTilePrefabName;
		m_onCompleteQuestDialogBannerId = onCompleteQuestDialogBannerId;
		m_onReceivedDialogSequence = onReceivedDialogSequence;
		m_onCompleteDialogSequence = onCompleteDialogSequence;
		m_onProgress1DialogSequence = onProgress1DialogSequence;
		m_onProgress2DialogSequence = onProgress2DialogSequence;
		m_onDismissDialogSequence = onDismissDialogSequence;
		m_isGenericRewardChest = isGenericRewardChest;
		m_chestVisualPrefabPath = chestVisualPrefabPath;
		m_customVisualWidget = customVisualWidget;
		m_enemyHeroClassId = enemyHeroClassId;
		m_progress = 0;
		m_ackProgress = NEW_ACHIEVE_ACK_PROGRESS;
		m_completionCount = 0;
		m_active = false;
		m_dateGiven = 0L;
		m_dateCompleted = 0L;
	}

	public void SetClientFlags(Assets.Achieve.ClientFlags clientFlags)
	{
		m_clientFlags = clientFlags;
	}

	public void SetAltTextPredicate(Assets.Achieve.AltTextPredicate altTextPredicate)
	{
		m_altTextPredicate = altTextPredicate;
	}

	public void SetName(string name, string altName)
	{
		m_name = name;
		m_altName = altName;
	}

	public void SetDescription(string description, string altDescription)
	{
		m_description = description;
		m_altDescription = altDescription;
	}

	public void SetEventTrigger(SpecialEventType eventType)
	{
		m_eventTrigger = eventType;
	}

	public void OnAchieveData(PegasusUtil.Achieve achieveData)
	{
		SetProgress(achieveData.Progress);
		SetAcknowledgedProgress(achieveData.AckProgress);
		m_completionCount = (achieveData.HasCompletionCount ? achieveData.CompletionCount : 0);
		m_active = achieveData.HasActive && achieveData.Active;
		m_dateGiven = (achieveData.HasDateGiven ? TimeUtils.PegDateToFileTimeUtc(achieveData.DateGiven) : 0);
		m_dateCompleted = (achieveData.HasDateCompleted ? TimeUtils.PegDateToFileTimeUtc(achieveData.DateCompleted) : 0);
		m_canAck = !achieveData.HasDoNotAck || !achieveData.DoNotAck;
		if (achieveData.HasIntervalRewardCount)
		{
			m_intervalRewardCount = achieveData.IntervalRewardCount;
		}
		m_intervalRewardStartDate = (achieveData.HasIntervalRewardStart ? TimeUtils.PegDateToFileTimeUtc(achieveData.IntervalRewardStart) : 0);
		AutoAckIfNeeded();
	}

	public void OnAchieveNotification(AchievementNotification notification)
	{
		PegasusUtil.Achieve achieve = new PegasusUtil.Achieve();
		achieve.Id = (int)notification.AchievementId;
		achieve.CompletionCount = CompletionCount;
		achieve.Progress = Progress;
		achieve.Active = Active;
		achieve.DoNotAck = !CanBeAcknowledged;
		achieve.DateCompleted = TimeUtils.FileTimeUtcToPegDate(DateCompleted);
		achieve.DateGiven = TimeUtils.FileTimeUtcToPegDate(DateGiven);
		achieve.AckProgress = AcknowledgedProgress;
		Log.Achievements.Print("OnAchieveNotification PlayerID={0} ID={1} Complete={2} New={3} Remove={4} Amount={5}", notification.PlayerId, notification.AchievementId, notification.Complete, notification.NewAchievement, notification.RemoveAchievement, notification.Amount);
		if (notification.NewAchievement)
		{
			achieve.DateGiven = TimeUtils.FileTimeUtcToPegDate(DateTime.UtcNow.ToFileTimeUtc());
			achieve.Active = true;
			achieve.AckProgress = NEW_ACHIEVE_ACK_PROGRESS;
			achieve.Progress = 0;
		}
		achieve.Progress += notification.Amount;
		if (notification.Complete)
		{
			achieve.Progress = MaxProgress;
			achieve.CompletionCount++;
			achieve.DateCompleted = TimeUtils.FileTimeUtcToPegDate(DateTime.UtcNow.ToFileTimeUtc());
			achieve.Active = false;
			achieve.DoNotAck = false;
		}
		if (notification.RemoveAchievement)
		{
			achieve.Active = false;
		}
		if (!achieve.Active)
		{
			OnAchieveData(achieve);
		}
		else
		{
			UpdateActiveAchieve(achieve);
		}
	}

	public void UpdateActiveAchieve(PegasusUtil.Achieve achieveData)
	{
		SetProgress(achieveData.Progress);
		SetAcknowledgedProgress(achieveData.AckProgress);
		m_active = true;
		m_dateGiven = (achieveData.HasDateGiven ? TimeUtils.PegDateToFileTimeUtc(achieveData.DateGiven) : 0);
		if (achieveData.HasIntervalRewardCount)
		{
			m_intervalRewardCount = achieveData.IntervalRewardCount;
		}
		if (achieveData.HasIntervalRewardStart)
		{
			m_intervalRewardStartDate = TimeUtils.PegDateToFileTimeUtc(achieveData.IntervalRewardStart);
		}
		AutoAckIfNeeded();
	}

	public void AddRewardNoticeID(long noticeID)
	{
		if (!m_rewardNoticeIDs.Contains(noticeID))
		{
			if (IsCompleted() && !NeedToAcknowledgeProgress(ackIntermediateProgress: false))
			{
				Network.Get().AckNotice(noticeID);
			}
			m_rewardNoticeIDs.Add(noticeID);
		}
	}

	public void OnCancelSuccess()
	{
		m_active = false;
	}

	public bool IsInternal()
	{
		if (Assets.Achieve.Type.INTERNAL_ACTIVE != AchieveType)
		{
			return Assets.Achieve.Type.INTERNAL_INACTIVE == AchieveType;
		}
		return true;
	}

	public bool IsNewlyActive()
	{
		return m_ackProgress == NEW_ACHIEVE_ACK_PROGRESS;
	}

	public bool IsCompleted()
	{
		return Progress >= MaxProgress;
	}

	public bool IsNewlyCompleted()
	{
		if (IsCompleted())
		{
			return AcknowledgedProgress < MaxProgress;
		}
		return false;
	}

	public bool IsActiveLicenseAddedAchieve()
	{
		if (Assets.Achieve.Trigger.LICENSEADDED != AchieveTrigger)
		{
			return false;
		}
		return Active;
	}

	public void AckCurrentProgressAndRewardNotices()
	{
		AckCurrentProgressAndRewardNotices(ackIntermediateProgress: false);
	}

	public void AckCurrentProgressAndRewardNotices(bool ackIntermediateProgress)
	{
		long[] array = m_rewardNoticeIDs.ToArray();
		m_rewardNoticeIDs.Clear();
		Network network = Network.Get();
		long[] array2 = array;
		foreach (long id in array2)
		{
			network.AckNotice(id);
		}
		if (NeedToAcknowledgeProgress(ackIntermediateProgress))
		{
			m_ackProgress = Progress;
			if (m_canAck)
			{
				network.AckAchieveProgress(ID, AcknowledgedProgress);
			}
		}
	}

	public void IncrementIntervalRewardCount()
	{
		if (m_intervalRewardCount < 0)
		{
			m_intervalRewardCount = 0;
		}
		m_intervalRewardCount++;
		if (m_intervalRewardStartDate == 0L)
		{
			m_intervalRewardStartDate = DateTime.UtcNow.ToFileTimeUtc();
		}
	}

	public bool IsValidFriendlyPlayerChallengeType(PlayerType playerType)
	{
		if (PlayerType != 0)
		{
			return playerType == PlayerType;
		}
		return true;
	}

	public override string ToString()
	{
		return $"[Achievement: ID={ID} Type={AchieveType} Name='{m_name}' MaxProgress={MaxProgress} Progress={Progress} AckProgress={AcknowledgedProgress} IsActive={Active} DateGiven={DateGiven} DateCompleted={DateCompleted} Description='{m_description}' Trigger={AchieveTrigger} CanAck={m_canAck}]";
	}

	public UserAttentionBlocker GetUserAttentionBlocker()
	{
		return (UserAttentionBlocker)DbfRecord.AttentionBlocker;
	}

	public AchieveRegionDataDbfRecord GetCurrentRegionData()
	{
		constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
		AchieveRegionDataDbfRecord record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord dbf) => dbf.AchieveId == ID && dbf.Region == (int)currentRegion);
		if (record == null)
		{
			record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord dbf) => dbf.AchieveId == ID && dbf.Region == 0);
		}
		return record;
	}

	private bool NeedToAcknowledgeProgress(bool ackIntermediateProgress)
	{
		if (AcknowledgedProgress >= MaxProgress)
		{
			return false;
		}
		if (AcknowledgedProgress == Progress)
		{
			return false;
		}
		if (!ackIntermediateProgress && Progress > 0 && Progress < MaxProgress)
		{
			return false;
		}
		return true;
	}

	private void SetProgress(int progress)
	{
		m_progress = progress;
	}

	private void SetAcknowledgedProgress(int acknowledgedProgress)
	{
		m_ackProgress = Mathf.Clamp(acknowledgedProgress, NEW_ACHIEVE_ACK_PROGRESS, Progress);
	}

	private void AutoAckIfNeeded()
	{
		if (IsInternal() || Assets.Achieve.Type.DAILY_REPEATABLE == AchieveType)
		{
			AckCurrentProgressAndRewardNotices();
		}
	}

	private void SetRewards(List<RewardData> rewardDataList)
	{
		m_rewards = new List<RewardData>(rewardDataList);
		FixUpRewardOrigins(m_rewards);
	}

	private void FixUpRewardOrigins(List<RewardData> rewardDataList)
	{
		foreach (RewardData rewardData in rewardDataList)
		{
			rewardData.SetOrigin(NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT, ID);
		}
	}
}
