using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001115 RID: 4373
	public static class ProgressUtils
	{
		// Token: 0x0600BF70 RID: 49008 RVA: 0x003A531A File Offset: 0x003A351A
		public static string FormatCompletionPercentageMessage(int points, int available)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION_PERCENTAGE", new object[]
			{
				Mathf.FloorToInt((float)points * 100f / (float)available)
			});
		}

		// Token: 0x0600BF71 RID: 49009 RVA: 0x003A5344 File Offset: 0x003A3544
		public static string FormatAchievementCompletionDate(long completionDate)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION", new object[]
			{
				GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION_DATE", new object[]
				{
					TimeUtils.UnixTimeStampToDateTimeLocal(completionDate)
				})
			});
		}

		// Token: 0x0600BF72 RID: 49010 RVA: 0x003A5384 File Offset: 0x003A3584
		public static string FormatAchievementSubcategoryFullName(AchievementCategoryDbfRecord categoryRecord, AchievementSubcategoryDbfRecord subcategoryRecord)
		{
			if (!string.IsNullOrEmpty(categoryRecord.Name) && !string.IsNullOrEmpty(subcategoryRecord.Name))
			{
				return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_SUBCATEGORY_NAME", new object[]
				{
					categoryRecord.Name.GetString(true),
					subcategoryRecord.Name.GetString(true)
				});
			}
			if (!string.IsNullOrEmpty(categoryRecord.Name))
			{
				return categoryRecord.Name;
			}
			if (!string.IsNullOrEmpty(subcategoryRecord.Name))
			{
				return subcategoryRecord.Name;
			}
			return "";
		}

		// Token: 0x0600BF73 RID: 49011 RVA: 0x003A5425 File Offset: 0x003A3625
		public static string FormatProgressMessage(int progress, int quota)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_PROGRESS_MESSAGE", new object[]
			{
				progress,
				quota
			});
		}

		// Token: 0x0600BF74 RID: 49012 RVA: 0x003A5449 File Offset: 0x003A3649
		public static string FormatTierMessage(int tier, int maxTier)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_TIER_MESSAGE", new object[]
			{
				tier,
				maxTier
			});
		}

		// Token: 0x0600BF75 RID: 49013 RVA: 0x003A5470 File Offset: 0x003A3670
		public static string FormatRewardsSummary(RewardListDataModel rewards, int xp, int points, bool isBoosted)
		{
			return string.Join("\n", (from text in new string[]
			{
				ProgressUtils.FormatRewards(rewards),
				isBoosted ? ProgressUtils.FormatRewardTrackXPBoosted(xp) : ProgressUtils.FormatRewardTrackXP(xp),
				ProgressUtils.FormatAchievementPoints(points)
			}
			where text != null
			select text).ToArray<string>());
		}

		// Token: 0x0600BF76 RID: 49014 RVA: 0x003A54DC File Offset: 0x003A36DC
		public static string FormatRewards(RewardListDataModel rewards)
		{
			if (rewards == null)
			{
				return null;
			}
			return rewards.Description;
		}

		// Token: 0x0600BF77 RID: 49015 RVA: 0x003A54E9 File Offset: 0x003A36E9
		public static string FormatRewardTrackXP(int xp)
		{
			if (xp <= 0)
			{
				return null;
			}
			return GameStrings.Format("GLOBAL_PROGRESSION_REWARD_TRACK_XP", new object[]
			{
				xp
			});
		}

		// Token: 0x0600BF78 RID: 49016 RVA: 0x003A550A File Offset: 0x003A370A
		public static string FormatRewardTrackXPBoosted(int xp)
		{
			if (xp <= 0)
			{
				return null;
			}
			return string.Format("<color=#24f104ff>{0}</color>", GameStrings.Format("GLOBAL_PROGRESSION_REWARD_TRACK_XP", new object[]
			{
				xp
			}));
		}

		// Token: 0x0600BF79 RID: 49017 RVA: 0x003A5535 File Offset: 0x003A3735
		public static string FormatAchievementPoints(int points)
		{
			if (points <= 0)
			{
				return null;
			}
			return GameStrings.Format("GLOBAL_PROGRESSION_POINTS", new object[]
			{
				points
			});
		}

		// Token: 0x0600BF7A RID: 49018 RVA: 0x003A5558 File Offset: 0x003A3758
		public static string FormatDescription(string description, int quota)
		{
			if (string.IsNullOrEmpty(description))
			{
				return string.Empty;
			}
			string text = Regex.Replace(description, "\\$q", "{0}", RegexOptions.IgnoreCase);
			GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
			{
				new GameStrings.PluralNumber
				{
					m_index = 0,
					m_number = quota
				}
			};
			return GameStrings.FormatLocalizedStringWithPlurals(text, pluralNumbers, new object[]
			{
				quota
			});
		}

		// Token: 0x0600BF7B RID: 49019 RVA: 0x003A55BA File Offset: 0x003A37BA
		public static bool IsAchievementComplete(AchievementManager.AchievementStatus status)
		{
			return status - AchievementManager.AchievementStatus.COMPLETED <= 2;
		}

		// Token: 0x0600BF7C RID: 49020 RVA: 0x003A55C5 File Offset: 0x003A37C5
		public static bool IsAchievementClaimed(AchievementManager.AchievementStatus status)
		{
			return status - AchievementManager.AchievementStatus.REWARD_GRANTED <= 1;
		}

		// Token: 0x0600BF7D RID: 49021 RVA: 0x003A55D0 File Offset: 0x003A37D0
		public static bool HasClaimedRewardTrackReward(RewardTrackManager.RewardStatus status)
		{
			return status - RewardTrackManager.RewardStatus.GRANTED <= 1;
		}

		// Token: 0x0600BF7E RID: 49022 RVA: 0x003A55DB File Offset: 0x003A37DB
		public static bool HasUnclaimedTrackReward(RewardTrackManager.RewardStatus status)
		{
			return !ProgressUtils.HasClaimedRewardTrackReward(status);
		}

		// Token: 0x0600BF7F RID: 49023 RVA: 0x003A55E8 File Offset: 0x003A37E8
		public static void ShowOfflinePopup()
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_HEADER"),
				m_text = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_showAlertIcon = false
			};
			DialogManager.Get().ShowPopup(info);
		}

		// Token: 0x0600BF80 RID: 49024 RVA: 0x003A5634 File Offset: 0x003A3834
		public static int CountUnclaimedRewards(RewardTrackDbfRecord record, int trackLevel, bool includePremium, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			List<RewardTrackLevelDbfRecord> nodes = (from node in record.Levels
			where node.Level <= trackLevel
			select node).ToList<RewardTrackLevelDbfRecord>();
			return ProgressUtils.CountUnclaimedFreeRewards(nodes, levelState) + (includePremium ? ProgressUtils.CountUnclaimedPremiumRewards(nodes, levelState) : 0);
		}

		// Token: 0x0600BF81 RID: 49025 RVA: 0x003A5680 File Offset: 0x003A3880
		public static int CountUnclaimedFreeRewards(IEnumerable<RewardTrackLevelDbfRecord> nodes, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			return (from state in (from node in nodes.Where(new Func<RewardTrackLevelDbfRecord, bool>(ProgressUtils.NodeHasFreeTrackRewards))
			select node.Level).Select(levelState)
			select state.FreeRewardStatus).Cast<RewardTrackManager.RewardStatus>().Count(new Func<RewardTrackManager.RewardStatus, bool>(ProgressUtils.HasUnclaimedTrackReward));
		}

		// Token: 0x0600BF82 RID: 49026 RVA: 0x003A5704 File Offset: 0x003A3904
		public static int CountUnclaimedPremiumRewards(IEnumerable<RewardTrackLevelDbfRecord> nodes, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			return (from state in (from node in nodes.Where(new Func<RewardTrackLevelDbfRecord, bool>(ProgressUtils.NodeHasPaidTrackRewards))
			select node.Level).Select(levelState)
			select state.PaidRewardStatus).Cast<RewardTrackManager.RewardStatus>().Count(new Func<RewardTrackManager.RewardStatus, bool>(ProgressUtils.HasUnclaimedTrackReward));
		}

		// Token: 0x0600BF83 RID: 49027 RVA: 0x003A5788 File Offset: 0x003A3988
		private static bool NodeHasFreeTrackRewards(RewardTrackLevelDbfRecord record)
		{
			RewardListDbfRecord freeRewardListRecord = record.FreeRewardListRecord;
			if (freeRewardListRecord == null)
			{
				return false;
			}
			List<RewardItemDbfRecord> rewardItems = freeRewardListRecord.RewardItems;
			int? num = (rewardItems != null) ? new int?(rewardItems.Count) : null;
			int num2 = 0;
			return num.GetValueOrDefault() > num2 & num != null;
		}

		// Token: 0x0600BF84 RID: 49028 RVA: 0x003A57D4 File Offset: 0x003A39D4
		private static bool NodeHasPaidTrackRewards(RewardTrackLevelDbfRecord record)
		{
			RewardListDbfRecord paidRewardListRecord = record.PaidRewardListRecord;
			if (paidRewardListRecord == null)
			{
				return false;
			}
			List<RewardItemDbfRecord> rewardItems = paidRewardListRecord.RewardItems;
			int? num = (rewardItems != null) ? new int?(rewardItems.Count) : null;
			int num2 = 0;
			return num.GetValueOrDefault() > num2 & num != null;
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600BF85 RID: 49029 RVA: 0x0001FA65 File Offset: 0x0001DC65
		// (set) Token: 0x0600BF86 RID: 49030 RVA: 0x003A5820 File Offset: 0x003A3A20
		public static bool ShowDebugIds
		{
			get
			{
				return false;
			}
			set
			{
				Options.Get().SetBool(Option.PROG_TILE_DEBUG, value);
			}
		}
	}
}
