using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.Progression;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000844 RID: 2116
public class Achievement
{
	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x0600711C RID: 28956 RVA: 0x00247BE2 File Offset: 0x00245DE2
	public int ID
	{
		get
		{
			return this.m_id;
		}
	}

	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x0600711D RID: 28957 RVA: 0x00247BEA File Offset: 0x00245DEA
	public bool Enabled
	{
		get
		{
			return this.DbfRecord.Enabled && QuestManager.Get().IsSystemEnabled && this.DbfRecord.EnabledWithProgression;
		}
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x0600711E RID: 28958 RVA: 0x00247C14 File Offset: 0x00245E14
	public Assets.Achieve.Type AchieveType
	{
		get
		{
			return this.m_type;
		}
	}

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x0600711F RID: 28959 RVA: 0x00247C1C File Offset: 0x00245E1C
	public int MaxProgress
	{
		get
		{
			return this.m_maxProgress;
		}
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x06007120 RID: 28960 RVA: 0x00247C24 File Offset: 0x00245E24
	public TAG_RACE? RaceRequirement
	{
		get
		{
			return this.m_raceReq;
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x06007121 RID: 28961 RVA: 0x00247C2C File Offset: 0x00245E2C
	public TAG_CLASS? ClassReward
	{
		get
		{
			return this.m_classReward;
		}
	}

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x06007122 RID: 28962 RVA: 0x00247C34 File Offset: 0x00245E34
	public TAG_CARD_SET? CardSetRequirement
	{
		get
		{
			return this.m_cardSetReq;
		}
	}

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x06007123 RID: 28963 RVA: 0x00247C3C File Offset: 0x00245E3C
	public TAG_CLASS? MyHeroClassRequirement
	{
		get
		{
			return this.m_myHeroClassReq;
		}
	}

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x06007124 RID: 28964 RVA: 0x00247C44 File Offset: 0x00245E44
	public global::Achievement.ClickTriggerType? ClickType
	{
		get
		{
			return this.m_clickType;
		}
	}

	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x06007125 RID: 28965 RVA: 0x00247C4C File Offset: 0x00245E4C
	public SpecialEventType EventTrigger
	{
		get
		{
			return this.m_eventTrigger;
		}
	}

	// Token: 0x17000687 RID: 1671
	// (get) Token: 0x06007126 RID: 28966 RVA: 0x00247C54 File Offset: 0x00245E54
	public int LinkToId
	{
		get
		{
			return this.m_linkToId;
		}
	}

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x06007127 RID: 28967 RVA: 0x00247C5C File Offset: 0x00245E5C
	public Assets.Achieve.Trigger AchieveTrigger
	{
		get
		{
			return this.m_trigger;
		}
	}

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x06007128 RID: 28968 RVA: 0x00247C64 File Offset: 0x00245E64
	public Assets.Achieve.GameMode Mode
	{
		get
		{
			return this.m_gameMode;
		}
	}

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x06007129 RID: 28969 RVA: 0x00247C6C File Offset: 0x00245E6C
	public Assets.Achieve.Unlocks UnlockedFeature
	{
		get
		{
			return this.m_unlockedFeature;
		}
	}

	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x0600712A RID: 28970 RVA: 0x00247C74 File Offset: 0x00245E74
	public List<RewardData> Rewards
	{
		get
		{
			return this.m_rewards;
		}
	}

	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x0600712B RID: 28971 RVA: 0x00247C7C File Offset: 0x00245E7C
	public List<int> Scenarios
	{
		get
		{
			return this.m_scenarios;
		}
	}

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x0600712C RID: 28972 RVA: 0x00247C84 File Offset: 0x00245E84
	public int WingID
	{
		get
		{
			return this.m_wingID;
		}
	}

	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x0600712D RID: 28973 RVA: 0x00247C8C File Offset: 0x00245E8C
	public int AdventureID
	{
		get
		{
			return this.m_adventureID;
		}
	}

	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x0600712E RID: 28974 RVA: 0x00247C94 File Offset: 0x00245E94
	public int AdventureModeID
	{
		get
		{
			return this.m_adventureModeID;
		}
	}

	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x0600712F RID: 28975 RVA: 0x00247C9C File Offset: 0x00245E9C
	public Assets.Achieve.RewardTiming RewardTiming
	{
		get
		{
			return this.m_rewardTiming;
		}
	}

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x06007130 RID: 28976 RVA: 0x00247CA4 File Offset: 0x00245EA4
	public int BoosterRequirement
	{
		get
		{
			return this.m_boosterReq;
		}
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x06007131 RID: 28977 RVA: 0x00247CAC File Offset: 0x00245EAC
	public bool UseGenericRewardVisual
	{
		get
		{
			return this.m_useGenericRewardVisual;
		}
	}

	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x06007132 RID: 28978 RVA: 0x00247CB4 File Offset: 0x00245EB4
	public Assets.Achieve.ShowToReturningPlayer ShowToReturningPlayer
	{
		get
		{
			return this.m_showToReturningPlayer;
		}
	}

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x06007133 RID: 28979 RVA: 0x00247CBC File Offset: 0x00245EBC
	public int QuestDialogId
	{
		get
		{
			return this.m_questDialogId;
		}
	}

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x06007134 RID: 28980 RVA: 0x00247CC4 File Offset: 0x00245EC4
	public bool AutoDestroy
	{
		get
		{
			return this.m_autoDestroy;
		}
	}

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x06007135 RID: 28981 RVA: 0x00247CCC File Offset: 0x00245ECC
	public string QuestTilePrefabName
	{
		get
		{
			return this.m_questTilePrefabName;
		}
	}

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x06007136 RID: 28982 RVA: 0x00247CD4 File Offset: 0x00245ED4
	public int OnCompleteQuestDialogBannerId
	{
		get
		{
			return this.m_onCompleteQuestDialogBannerId;
		}
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x06007137 RID: 28983 RVA: 0x00247CDC File Offset: 0x00245EDC
	public CharacterDialogSequence OnReceivedDialogSequence
	{
		get
		{
			return this.m_onReceivedDialogSequence;
		}
	}

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x06007138 RID: 28984 RVA: 0x00247CE4 File Offset: 0x00245EE4
	public CharacterDialogSequence OnCompleteDialogSequence
	{
		get
		{
			return this.m_onCompleteDialogSequence;
		}
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x06007139 RID: 28985 RVA: 0x00247CEC File Offset: 0x00245EEC
	public CharacterDialogSequence OnProgress1DialogSequence
	{
		get
		{
			return this.m_onProgress1DialogSequence;
		}
	}

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x0600713A RID: 28986 RVA: 0x00247CF4 File Offset: 0x00245EF4
	public CharacterDialogSequence OnProgress2DialogSequence
	{
		get
		{
			return this.m_onProgress2DialogSequence;
		}
	}

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x0600713B RID: 28987 RVA: 0x00247CFC File Offset: 0x00245EFC
	public CharacterDialogSequence OnDismissDialogSequence
	{
		get
		{
			return this.m_onDismissDialogSequence;
		}
	}

	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x0600713C RID: 28988 RVA: 0x00247D04 File Offset: 0x00245F04
	// (set) Token: 0x0600713D RID: 28989 RVA: 0x00247D0C File Offset: 0x00245F0C
	public AchieveDbfRecord DbfRecord { get; private set; }

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x0600713E RID: 28990 RVA: 0x00247D15 File Offset: 0x00245F15
	public int Progress
	{
		get
		{
			return this.m_progress;
		}
	}

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x0600713F RID: 28991 RVA: 0x00247D1D File Offset: 0x00245F1D
	public int AcknowledgedProgress
	{
		get
		{
			return this.m_ackProgress;
		}
	}

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x06007140 RID: 28992 RVA: 0x00247D25 File Offset: 0x00245F25
	public bool CanBeAcknowledged
	{
		get
		{
			return this.m_canAck;
		}
	}

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x06007141 RID: 28993 RVA: 0x00247D2D File Offset: 0x00245F2D
	public int CompletionCount
	{
		get
		{
			return this.m_completionCount;
		}
	}

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x06007142 RID: 28994 RVA: 0x00247D35 File Offset: 0x00245F35
	public bool Active
	{
		get
		{
			return this.m_active;
		}
	}

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x06007143 RID: 28995 RVA: 0x00247D3D File Offset: 0x00245F3D
	public long DateGiven
	{
		get
		{
			return this.m_dateGiven;
		}
	}

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x06007144 RID: 28996 RVA: 0x00247D45 File Offset: 0x00245F45
	public long DateCompleted
	{
		get
		{
			return this.m_dateCompleted;
		}
	}

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x06007145 RID: 28997 RVA: 0x00247D4D File Offset: 0x00245F4D
	public int IntervalRewardCount
	{
		get
		{
			return this.m_intervalRewardCount;
		}
	}

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x06007146 RID: 28998 RVA: 0x00247D55 File Offset: 0x00245F55
	public long IntervalRewardStartDate
	{
		get
		{
			return this.m_intervalRewardStartDate;
		}
	}

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x06007147 RID: 28999 RVA: 0x00247D5D File Offset: 0x00245F5D
	public bool IsGenericRewardChest
	{
		get
		{
			return this.m_isGenericRewardChest;
		}
	}

	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x06007148 RID: 29000 RVA: 0x00247D65 File Offset: 0x00245F65
	public string ChestVisualPrefabPath
	{
		get
		{
			return this.m_chestVisualPrefabPath;
		}
	}

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x06007149 RID: 29001 RVA: 0x00247D6D File Offset: 0x00245F6D
	public string CustomVisualWidget
	{
		get
		{
			return this.m_customVisualWidget;
		}
	}

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x0600714A RID: 29002 RVA: 0x00247D75 File Offset: 0x00245F75
	public int EnemyHeroClassId
	{
		get
		{
			return this.m_enemyHeroClassId;
		}
	}

	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x0600714B RID: 29003 RVA: 0x00247D7D File Offset: 0x00245F7D
	public bool IsLegendary
	{
		get
		{
			return (this.m_clientFlags & Assets.Achieve.ClientFlags.IS_LEGENDARY) > Assets.Achieve.ClientFlags.NONE;
		}
	}

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x0600714C RID: 29004 RVA: 0x00247D8A File Offset: 0x00245F8A
	public bool IsAffectedByDoubleGold
	{
		get
		{
			return (this.m_clientFlags & Assets.Achieve.ClientFlags.IS_AFFECTED_BY_DOUBLE_GOLD) > Assets.Achieve.ClientFlags.NONE;
		}
	}

	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x0600714D RID: 29005 RVA: 0x00247D97 File Offset: 0x00245F97
	public bool HasRewardChestVisuals
	{
		get
		{
			return !string.IsNullOrEmpty(this.m_chestVisualPrefabPath);
		}
	}

	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x0600714E RID: 29006 RVA: 0x00247DA8 File Offset: 0x00245FA8
	public bool CanShowInQuestLog
	{
		get
		{
			if ((this.m_clientFlags & Assets.Achieve.ClientFlags.SHOW_IN_QUEST_LOG) != Assets.Achieve.ClientFlags.NONE)
			{
				return true;
			}
			switch (this.AchieveType)
			{
			case Assets.Achieve.Type.STARTER:
			case Assets.Achieve.Type.DAILY:
			case Assets.Achieve.Type.NORMAL_QUEST:
				return true;
			case Assets.Achieve.Type.HERO:
			case Assets.Achieve.Type.GOLDHERO:
			case Assets.Achieve.Type.DAILY_REPEATABLE:
			case Assets.Achieve.Type.HIDDEN:
			case Assets.Achieve.Type.INTERNAL_ACTIVE:
			case Assets.Achieve.Type.INTERNAL_INACTIVE:
				return false;
			}
			return false;
		}
	}

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x0600714F RID: 29007 RVA: 0x00247DFF File Offset: 0x00245FFF
	public bool IsAffectedByFriendWeek
	{
		get
		{
			return (this.m_clientFlags & Assets.Achieve.ClientFlags.IS_AFFECTED_BY_FRIEND_WEEK) != Assets.Achieve.ClientFlags.NONE;
		}
	}

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x06007150 RID: 29008 RVA: 0x00247E10 File Offset: 0x00246010
	public bool IsFriendlyChallengeQuest
	{
		get
		{
			Assets.Achieve.GameMode gameMode = this.m_gameMode;
			return gameMode == Assets.Achieve.GameMode.FRIENDLY;
		}
	}

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06007151 RID: 29009 RVA: 0x00247E2C File Offset: 0x0024602C
	public bool GameModeRequiresNonFriendlyChallenge
	{
		get
		{
			Assets.Achieve.GameMode gameMode = this.m_gameMode;
			return gameMode != Assets.Achieve.GameMode.ANY && gameMode != Assets.Achieve.GameMode.FRIENDLY;
		}
	}

	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x06007152 RID: 29010 RVA: 0x00247E4B File Offset: 0x0024604B
	public bool CanBeCancelled
	{
		get
		{
			return this.IsLegendary || this.AchieveType == Assets.Achieve.Type.DAILY;
		}
	}

	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x06007153 RID: 29011 RVA: 0x00247E60 File Offset: 0x00246060
	public PlayerType PlayerType
	{
		get
		{
			return (PlayerType)this.DbfRecord.PlayerType;
		}
	}

	// Token: 0x06007154 RID: 29012 RVA: 0x00247E70 File Offset: 0x00246070
	public Achievement()
	{
	}

	// Token: 0x06007155 RID: 29013 RVA: 0x00247EE0 File Offset: 0x002460E0
	public Achievement(AchieveDbfRecord dbfRecord, int id, Assets.Achieve.Type achieveType, int maxProgress, int linkToId, Assets.Achieve.Trigger trigger, Assets.Achieve.GameMode gameMode, TAG_RACE? raceReq, TAG_CLASS? classReward, TAG_CARD_SET? cardSetReq, TAG_CLASS? myHeroClassReq, global::Achievement.ClickTriggerType? clickType, Assets.Achieve.Unlocks unlockedFeature, List<RewardData> rewards, List<int> scenarios, int wingID, int adventureID, int adventureModeID, Assets.Achieve.RewardTiming rewardTiming, int boosterReq, bool useGenericRewardVisual, Assets.Achieve.ShowToReturningPlayer showToReturningPlayer, int questDialogId, bool autoDestroy, string questTilePrefabName, int onCompleteQuestDialogBannerId, CharacterDialogSequence onReceivedDialogSequence, CharacterDialogSequence onCompleteDialogSequence, CharacterDialogSequence onProgress1DialogSequence, CharacterDialogSequence onProgress2DialogSequence, CharacterDialogSequence onDismissDialogSequence, bool isGenericRewardChest, string chestVisualPrefabPath, string customVisualWidget, int enemyHeroClassId)
	{
		this.DbfRecord = ((dbfRecord == null) ? new AchieveDbfRecord() : dbfRecord);
		this.m_id = id;
		this.m_type = achieveType;
		this.m_maxProgress = maxProgress;
		this.m_linkToId = linkToId;
		this.m_trigger = trigger;
		this.m_gameMode = gameMode;
		this.m_raceReq = raceReq;
		this.m_classReward = classReward;
		this.m_cardSetReq = cardSetReq;
		this.m_myHeroClassReq = myHeroClassReq;
		this.m_clickType = clickType;
		this.SetRewards(rewards);
		this.m_unlockedFeature = unlockedFeature;
		this.m_scenarios = scenarios;
		this.m_wingID = wingID;
		this.m_adventureID = adventureID;
		this.m_adventureModeID = adventureModeID;
		this.m_rewardTiming = rewardTiming;
		this.m_boosterReq = boosterReq;
		this.m_useGenericRewardVisual = useGenericRewardVisual;
		this.m_showToReturningPlayer = showToReturningPlayer;
		this.m_questDialogId = questDialogId;
		this.m_autoDestroy = autoDestroy;
		this.m_questTilePrefabName = questTilePrefabName;
		this.m_onCompleteQuestDialogBannerId = onCompleteQuestDialogBannerId;
		this.m_onReceivedDialogSequence = onReceivedDialogSequence;
		this.m_onCompleteDialogSequence = onCompleteDialogSequence;
		this.m_onProgress1DialogSequence = onProgress1DialogSequence;
		this.m_onProgress2DialogSequence = onProgress2DialogSequence;
		this.m_onDismissDialogSequence = onDismissDialogSequence;
		this.m_isGenericRewardChest = isGenericRewardChest;
		this.m_chestVisualPrefabPath = chestVisualPrefabPath;
		this.m_customVisualWidget = customVisualWidget;
		this.m_enemyHeroClassId = enemyHeroClassId;
		this.m_progress = 0;
		this.m_ackProgress = global::Achievement.NEW_ACHIEVE_ACK_PROGRESS;
		this.m_completionCount = 0;
		this.m_active = false;
		this.m_dateGiven = 0L;
		this.m_dateCompleted = 0L;
	}

	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x06007156 RID: 29014 RVA: 0x0024809D File Offset: 0x0024629D
	public string Name
	{
		get
		{
			if (!string.IsNullOrEmpty(this.m_altName) && AchieveManager.IsPredicateTrue(this.m_altTextPredicate))
			{
				return this.m_altName;
			}
			return this.m_name;
		}
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x06007157 RID: 29015 RVA: 0x002480C6 File Offset: 0x002462C6
	public string Description
	{
		get
		{
			if (!string.IsNullOrEmpty(this.m_altDescription) && AchieveManager.IsPredicateTrue(this.m_altTextPredicate))
			{
				return this.m_altDescription;
			}
			return this.m_description;
		}
	}

	// Token: 0x06007158 RID: 29016 RVA: 0x002480EF File Offset: 0x002462EF
	public void SetClientFlags(Assets.Achieve.ClientFlags clientFlags)
	{
		this.m_clientFlags = clientFlags;
	}

	// Token: 0x06007159 RID: 29017 RVA: 0x002480F8 File Offset: 0x002462F8
	public void SetAltTextPredicate(Assets.Achieve.AltTextPredicate altTextPredicate)
	{
		this.m_altTextPredicate = altTextPredicate;
	}

	// Token: 0x0600715A RID: 29018 RVA: 0x00248101 File Offset: 0x00246301
	public void SetName(string name, string altName)
	{
		this.m_name = name;
		this.m_altName = altName;
	}

	// Token: 0x0600715B RID: 29019 RVA: 0x00248111 File Offset: 0x00246311
	public void SetDescription(string description, string altDescription)
	{
		this.m_description = description;
		this.m_altDescription = altDescription;
	}

	// Token: 0x0600715C RID: 29020 RVA: 0x00248121 File Offset: 0x00246321
	public void SetEventTrigger(SpecialEventType eventType)
	{
		this.m_eventTrigger = eventType;
	}

	// Token: 0x0600715D RID: 29021 RVA: 0x0024812C File Offset: 0x0024632C
	public void OnAchieveData(PegasusUtil.Achieve achieveData)
	{
		this.SetProgress(achieveData.Progress);
		this.SetAcknowledgedProgress(achieveData.AckProgress);
		this.m_completionCount = (achieveData.HasCompletionCount ? achieveData.CompletionCount : 0);
		this.m_active = (achieveData.HasActive && achieveData.Active);
		this.m_dateGiven = (achieveData.HasDateGiven ? global::TimeUtils.PegDateToFileTimeUtc(achieveData.DateGiven) : 0L);
		this.m_dateCompleted = (achieveData.HasDateCompleted ? global::TimeUtils.PegDateToFileTimeUtc(achieveData.DateCompleted) : 0L);
		this.m_canAck = (!achieveData.HasDoNotAck || !achieveData.DoNotAck);
		if (achieveData.HasIntervalRewardCount)
		{
			this.m_intervalRewardCount = achieveData.IntervalRewardCount;
		}
		this.m_intervalRewardStartDate = (achieveData.HasIntervalRewardStart ? global::TimeUtils.PegDateToFileTimeUtc(achieveData.IntervalRewardStart) : 0L);
		this.AutoAckIfNeeded();
	}

	// Token: 0x0600715E RID: 29022 RVA: 0x0024820C File Offset: 0x0024640C
	public void OnAchieveNotification(AchievementNotification notification)
	{
		PegasusUtil.Achieve achieve = new PegasusUtil.Achieve();
		achieve.Id = (int)notification.AchievementId;
		achieve.CompletionCount = this.CompletionCount;
		achieve.Progress = this.Progress;
		achieve.Active = this.Active;
		achieve.DoNotAck = !this.CanBeAcknowledged;
		achieve.DateCompleted = global::TimeUtils.FileTimeUtcToPegDate(this.DateCompleted);
		achieve.DateGiven = global::TimeUtils.FileTimeUtcToPegDate(this.DateGiven);
		achieve.AckProgress = this.AcknowledgedProgress;
		global::Log.Achievements.Print("OnAchieveNotification PlayerID={0} ID={1} Complete={2} New={3} Remove={4} Amount={5}", new object[]
		{
			notification.PlayerId,
			notification.AchievementId,
			notification.Complete,
			notification.NewAchievement,
			notification.RemoveAchievement,
			notification.Amount
		});
		if (notification.NewAchievement)
		{
			achieve.DateGiven = global::TimeUtils.FileTimeUtcToPegDate(DateTime.UtcNow.ToFileTimeUtc());
			achieve.Active = true;
			achieve.AckProgress = global::Achievement.NEW_ACHIEVE_ACK_PROGRESS;
			achieve.Progress = 0;
		}
		achieve.Progress += notification.Amount;
		if (notification.Complete)
		{
			achieve.Progress = this.MaxProgress;
			PegasusUtil.Achieve achieve2 = achieve;
			int completionCount = achieve2.CompletionCount;
			achieve2.CompletionCount = completionCount + 1;
			achieve.DateCompleted = global::TimeUtils.FileTimeUtcToPegDate(DateTime.UtcNow.ToFileTimeUtc());
			achieve.Active = false;
			achieve.DoNotAck = false;
		}
		if (notification.RemoveAchievement)
		{
			achieve.Active = false;
		}
		if (!achieve.Active)
		{
			this.OnAchieveData(achieve);
			return;
		}
		this.UpdateActiveAchieve(achieve);
	}

	// Token: 0x0600715F RID: 29023 RVA: 0x002483B4 File Offset: 0x002465B4
	public void UpdateActiveAchieve(PegasusUtil.Achieve achieveData)
	{
		this.SetProgress(achieveData.Progress);
		this.SetAcknowledgedProgress(achieveData.AckProgress);
		this.m_active = true;
		this.m_dateGiven = (achieveData.HasDateGiven ? global::TimeUtils.PegDateToFileTimeUtc(achieveData.DateGiven) : 0L);
		if (achieveData.HasIntervalRewardCount)
		{
			this.m_intervalRewardCount = achieveData.IntervalRewardCount;
		}
		if (achieveData.HasIntervalRewardStart)
		{
			this.m_intervalRewardStartDate = global::TimeUtils.PegDateToFileTimeUtc(achieveData.IntervalRewardStart);
		}
		this.AutoAckIfNeeded();
	}

	// Token: 0x06007160 RID: 29024 RVA: 0x00248430 File Offset: 0x00246630
	public void AddRewardNoticeID(long noticeID)
	{
		if (this.m_rewardNoticeIDs.Contains(noticeID))
		{
			return;
		}
		if (this.IsCompleted() && !this.NeedToAcknowledgeProgress(false))
		{
			Network.Get().AckNotice(noticeID);
		}
		this.m_rewardNoticeIDs.Add(noticeID);
	}

	// Token: 0x06007161 RID: 29025 RVA: 0x00248469 File Offset: 0x00246669
	public void OnCancelSuccess()
	{
		this.m_active = false;
	}

	// Token: 0x06007162 RID: 29026 RVA: 0x00248472 File Offset: 0x00246672
	public bool IsInternal()
	{
		return Assets.Achieve.Type.INTERNAL_ACTIVE == this.AchieveType || Assets.Achieve.Type.INTERNAL_INACTIVE == this.AchieveType;
	}

	// Token: 0x06007163 RID: 29027 RVA: 0x00248488 File Offset: 0x00246688
	public bool IsNewlyActive()
	{
		return this.m_ackProgress == global::Achievement.NEW_ACHIEVE_ACK_PROGRESS;
	}

	// Token: 0x06007164 RID: 29028 RVA: 0x00248497 File Offset: 0x00246697
	public bool IsCompleted()
	{
		return this.Progress >= this.MaxProgress;
	}

	// Token: 0x06007165 RID: 29029 RVA: 0x002484AA File Offset: 0x002466AA
	public bool IsNewlyCompleted()
	{
		return this.IsCompleted() && this.AcknowledgedProgress < this.MaxProgress;
	}

	// Token: 0x06007166 RID: 29030 RVA: 0x002484C4 File Offset: 0x002466C4
	public bool IsActiveLicenseAddedAchieve()
	{
		return Assets.Achieve.Trigger.LICENSEADDED == this.AchieveTrigger && this.Active;
	}

	// Token: 0x06007167 RID: 29031 RVA: 0x002484D8 File Offset: 0x002466D8
	public void AckCurrentProgressAndRewardNotices()
	{
		this.AckCurrentProgressAndRewardNotices(false);
	}

	// Token: 0x06007168 RID: 29032 RVA: 0x002484E4 File Offset: 0x002466E4
	public void AckCurrentProgressAndRewardNotices(bool ackIntermediateProgress)
	{
		long[] array = this.m_rewardNoticeIDs.ToArray();
		this.m_rewardNoticeIDs.Clear();
		Network network = Network.Get();
		foreach (long id in array)
		{
			network.AckNotice(id);
		}
		if (!this.NeedToAcknowledgeProgress(ackIntermediateProgress))
		{
			return;
		}
		this.m_ackProgress = this.Progress;
		if (!this.m_canAck)
		{
			return;
		}
		network.AckAchieveProgress(this.ID, this.AcknowledgedProgress);
	}

	// Token: 0x06007169 RID: 29033 RVA: 0x00248558 File Offset: 0x00246758
	public void IncrementIntervalRewardCount()
	{
		if (this.m_intervalRewardCount < 0)
		{
			this.m_intervalRewardCount = 0;
		}
		this.m_intervalRewardCount++;
		if (this.m_intervalRewardStartDate == 0L)
		{
			this.m_intervalRewardStartDate = DateTime.UtcNow.ToFileTimeUtc();
		}
	}

	// Token: 0x0600716A RID: 29034 RVA: 0x0024859E File Offset: 0x0024679E
	public bool IsValidFriendlyPlayerChallengeType(PlayerType playerType)
	{
		return this.PlayerType == PlayerType.PT_ANY || playerType == this.PlayerType;
	}

	// Token: 0x0600716B RID: 29035 RVA: 0x002485B4 File Offset: 0x002467B4
	public override string ToString()
	{
		return string.Format("[Achievement: ID={0} Type={1} Name='{2}' MaxProgress={3} Progress={4} AckProgress={5} IsActive={6} DateGiven={7} DateCompleted={8} Description='{9}' Trigger={10} CanAck={11}]", new object[]
		{
			this.ID,
			this.AchieveType,
			this.m_name,
			this.MaxProgress,
			this.Progress,
			this.AcknowledgedProgress,
			this.Active,
			this.DateGiven,
			this.DateCompleted,
			this.m_description,
			this.AchieveTrigger,
			this.m_canAck
		});
	}

	// Token: 0x0600716C RID: 29036 RVA: 0x00248673 File Offset: 0x00246873
	public UserAttentionBlocker GetUserAttentionBlocker()
	{
		return (UserAttentionBlocker)this.DbfRecord.AttentionBlocker;
	}

	// Token: 0x0600716D RID: 29037 RVA: 0x00248680 File Offset: 0x00246880
	public AchieveRegionDataDbfRecord GetCurrentRegionData()
	{
		constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
		AchieveRegionDataDbfRecord record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord dbf) => dbf.AchieveId == this.ID && dbf.Region == (int)currentRegion);
		if (record == null)
		{
			record = GameDbf.AchieveRegionData.GetRecord((AchieveRegionDataDbfRecord dbf) => dbf.AchieveId == this.ID && dbf.Region == 0);
		}
		return record;
	}

	// Token: 0x0600716E RID: 29038 RVA: 0x002486D7 File Offset: 0x002468D7
	private bool NeedToAcknowledgeProgress(bool ackIntermediateProgress)
	{
		return this.AcknowledgedProgress < this.MaxProgress && this.AcknowledgedProgress != this.Progress && (ackIntermediateProgress || this.Progress <= 0 || this.Progress >= this.MaxProgress);
	}

	// Token: 0x0600716F RID: 29039 RVA: 0x00248716 File Offset: 0x00246916
	private void SetProgress(int progress)
	{
		this.m_progress = progress;
	}

	// Token: 0x06007170 RID: 29040 RVA: 0x0024871F File Offset: 0x0024691F
	private void SetAcknowledgedProgress(int acknowledgedProgress)
	{
		this.m_ackProgress = Mathf.Clamp(acknowledgedProgress, global::Achievement.NEW_ACHIEVE_ACK_PROGRESS, this.Progress);
	}

	// Token: 0x06007171 RID: 29041 RVA: 0x00248738 File Offset: 0x00246938
	private void AutoAckIfNeeded()
	{
		if (!this.IsInternal() && Assets.Achieve.Type.DAILY_REPEATABLE != this.AchieveType)
		{
			return;
		}
		this.AckCurrentProgressAndRewardNotices();
	}

	// Token: 0x06007172 RID: 29042 RVA: 0x00248757 File Offset: 0x00246957
	private void SetRewards(List<RewardData> rewardDataList)
	{
		this.m_rewards = new List<RewardData>(rewardDataList);
		this.FixUpRewardOrigins(this.m_rewards);
	}

	// Token: 0x06007173 RID: 29043 RVA: 0x00248774 File Offset: 0x00246974
	private void FixUpRewardOrigins(List<RewardData> rewardDataList)
	{
		foreach (RewardData rewardData in rewardDataList)
		{
			rewardData.SetOrigin(NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT, (long)this.ID);
		}
	}

	// Token: 0x04005A8C RID: 23180
	public static readonly int NEW_ACHIEVE_ACK_PROGRESS = -1;

	// Token: 0x04005A8D RID: 23181
	private int m_id;

	// Token: 0x04005A8E RID: 23182
	private bool m_enabled;

	// Token: 0x04005A8F RID: 23183
	private string m_name = "";

	// Token: 0x04005A90 RID: 23184
	private string m_description = "";

	// Token: 0x04005A91 RID: 23185
	private Assets.Achieve.AltTextPredicate m_altTextPredicate;

	// Token: 0x04005A92 RID: 23186
	private string m_altName;

	// Token: 0x04005A93 RID: 23187
	private string m_altDescription;

	// Token: 0x04005A94 RID: 23188
	private Assets.Achieve.Type m_type;

	// Token: 0x04005A95 RID: 23189
	private int m_maxProgress;

	// Token: 0x04005A96 RID: 23190
	private TAG_RACE? m_raceReq;

	// Token: 0x04005A97 RID: 23191
	private TAG_CLASS? m_classReward;

	// Token: 0x04005A98 RID: 23192
	private TAG_CARD_SET? m_cardSetReq;

	// Token: 0x04005A99 RID: 23193
	private TAG_CLASS? m_myHeroClassReq;

	// Token: 0x04005A9A RID: 23194
	private global::Achievement.ClickTriggerType? m_clickType;

	// Token: 0x04005A9B RID: 23195
	private SpecialEventType m_eventTrigger;

	// Token: 0x04005A9C RID: 23196
	private int m_linkToId;

	// Token: 0x04005A9D RID: 23197
	private Assets.Achieve.Trigger m_trigger = Assets.Achieve.Trigger.NONE;

	// Token: 0x04005A9E RID: 23198
	private Assets.Achieve.GameMode m_gameMode;

	// Token: 0x04005A9F RID: 23199
	private Assets.Achieve.Unlocks m_unlockedFeature = Assets.Achieve.Unlocks.NONE;

	// Token: 0x04005AA0 RID: 23200
	private List<RewardData> m_rewards = new List<RewardData>();

	// Token: 0x04005AA1 RID: 23201
	private List<int> m_scenarios = new List<int>();

	// Token: 0x04005AA2 RID: 23202
	private int m_wingID;

	// Token: 0x04005AA3 RID: 23203
	private int m_adventureID;

	// Token: 0x04005AA4 RID: 23204
	private int m_adventureModeID;

	// Token: 0x04005AA5 RID: 23205
	private Assets.Achieve.RewardTiming m_rewardTiming;

	// Token: 0x04005AA6 RID: 23206
	private int m_boosterReq;

	// Token: 0x04005AA7 RID: 23207
	private Assets.Achieve.ClientFlags m_clientFlags;

	// Token: 0x04005AA8 RID: 23208
	private bool m_useGenericRewardVisual;

	// Token: 0x04005AA9 RID: 23209
	private Assets.Achieve.ShowToReturningPlayer m_showToReturningPlayer;

	// Token: 0x04005AAA RID: 23210
	private int m_questDialogId;

	// Token: 0x04005AAB RID: 23211
	private bool m_autoDestroy;

	// Token: 0x04005AAC RID: 23212
	private string m_questTilePrefabName;

	// Token: 0x04005AAD RID: 23213
	private int m_onCompleteQuestDialogBannerId;

	// Token: 0x04005AAE RID: 23214
	private CharacterDialogSequence m_onReceivedDialogSequence;

	// Token: 0x04005AAF RID: 23215
	private CharacterDialogSequence m_onCompleteDialogSequence;

	// Token: 0x04005AB0 RID: 23216
	private CharacterDialogSequence m_onProgress1DialogSequence;

	// Token: 0x04005AB1 RID: 23217
	private CharacterDialogSequence m_onProgress2DialogSequence;

	// Token: 0x04005AB2 RID: 23218
	private CharacterDialogSequence m_onDismissDialogSequence;

	// Token: 0x04005AB3 RID: 23219
	private bool m_isGenericRewardChest;

	// Token: 0x04005AB4 RID: 23220
	private string m_chestVisualPrefabPath = "";

	// Token: 0x04005AB5 RID: 23221
	private string m_customVisualWidget = "";

	// Token: 0x04005AB6 RID: 23222
	private int m_enemyHeroClassId;

	// Token: 0x04005AB7 RID: 23223
	private int m_progress;

	// Token: 0x04005AB8 RID: 23224
	private int m_ackProgress;

	// Token: 0x04005AB9 RID: 23225
	private int m_completionCount;

	// Token: 0x04005ABA RID: 23226
	private bool m_active;

	// Token: 0x04005ABB RID: 23227
	private long m_dateGiven;

	// Token: 0x04005ABC RID: 23228
	private long m_dateCompleted;

	// Token: 0x04005ABD RID: 23229
	private bool m_canAck;

	// Token: 0x04005ABE RID: 23230
	private int m_intervalRewardCount;

	// Token: 0x04005ABF RID: 23231
	private long m_intervalRewardStartDate;

	// Token: 0x04005AC0 RID: 23232
	private List<long> m_rewardNoticeIDs = new List<long>();

	// Token: 0x02002431 RID: 9265
	public enum ClickTriggerType
	{
		// Token: 0x0400E974 RID: 59764
		BUTTON_PLAY = 1,
		// Token: 0x0400E975 RID: 59765
		BUTTON_ARENA,
		// Token: 0x0400E976 RID: 59766
		BUTTON_ADVENTURE
	}
}
