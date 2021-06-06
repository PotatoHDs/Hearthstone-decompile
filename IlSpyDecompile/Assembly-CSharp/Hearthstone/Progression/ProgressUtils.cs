using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	public static class ProgressUtils
	{
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

		public static string FormatCompletionPercentageMessage(int points, int available)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION_PERCENTAGE", Mathf.FloorToInt((float)points * 100f / (float)available));
		}

		public static string FormatAchievementCompletionDate(long completionDate)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION", GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_COMPLETION_DATE", TimeUtils.UnixTimeStampToDateTimeLocal(completionDate)));
		}

		public static string FormatAchievementSubcategoryFullName(AchievementCategoryDbfRecord categoryRecord, AchievementSubcategoryDbfRecord subcategoryRecord)
		{
			if (!string.IsNullOrEmpty(categoryRecord.Name) && !string.IsNullOrEmpty(subcategoryRecord.Name))
			{
				return GameStrings.Format("GLOBAL_PROGRESSION_ACHIEVEMENT_SUBCATEGORY_NAME", categoryRecord.Name.GetString(), subcategoryRecord.Name.GetString());
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

		public static string FormatProgressMessage(int progress, int quota)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_PROGRESS_MESSAGE", progress, quota);
		}

		public static string FormatTierMessage(int tier, int maxTier)
		{
			return GameStrings.Format("GLOBAL_PROGRESSION_TIER_MESSAGE", tier, maxTier);
		}

		public static string FormatRewardsSummary(RewardListDataModel rewards, int xp, int points, bool isBoosted)
		{
			return string.Join("\n", new string[3]
			{
				FormatRewards(rewards),
				isBoosted ? FormatRewardTrackXPBoosted(xp) : FormatRewardTrackXP(xp),
				FormatAchievementPoints(points)
			}.Where((string text) => text != null).ToArray());
		}

		public static string FormatRewards(RewardListDataModel rewards)
		{
			return rewards?.Description;
		}

		public static string FormatRewardTrackXP(int xp)
		{
			if (xp <= 0)
			{
				return null;
			}
			return GameStrings.Format("GLOBAL_PROGRESSION_REWARD_TRACK_XP", xp);
		}

		public static string FormatRewardTrackXPBoosted(int xp)
		{
			if (xp <= 0)
			{
				return null;
			}
			return string.Format("<color=#24f104ff>{0}</color>", GameStrings.Format("GLOBAL_PROGRESSION_REWARD_TRACK_XP", xp));
		}

		public static string FormatAchievementPoints(int points)
		{
			if (points <= 0)
			{
				return null;
			}
			return GameStrings.Format("GLOBAL_PROGRESSION_POINTS", points);
		}

		public static string FormatDescription(string description, int quota)
		{
			if (string.IsNullOrEmpty(description))
			{
				return string.Empty;
			}
			string text = Regex.Replace(description, "\\$q", "{0}", RegexOptions.IgnoreCase);
			GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[1]
			{
				new GameStrings.PluralNumber
				{
					m_index = 0,
					m_number = quota
				}
			};
			return GameStrings.FormatLocalizedStringWithPlurals(text, pluralNumbers, quota);
		}

		public static bool IsAchievementComplete(AchievementManager.AchievementStatus status)
		{
			if ((uint)(status - 2) <= 2u)
			{
				return true;
			}
			return false;
		}

		public static bool IsAchievementClaimed(AchievementManager.AchievementStatus status)
		{
			if ((uint)(status - 3) <= 1u)
			{
				return true;
			}
			return false;
		}

		public static bool HasClaimedRewardTrackReward(RewardTrackManager.RewardStatus status)
		{
			if ((uint)(status - 1) <= 1u)
			{
				return true;
			}
			return false;
		}

		public static bool HasUnclaimedTrackReward(RewardTrackManager.RewardStatus status)
		{
			return !HasClaimedRewardTrackReward(status);
		}

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

		public static int CountUnclaimedRewards(RewardTrackDbfRecord record, int trackLevel, bool includePremium, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			List<RewardTrackLevelDbfRecord> nodes = record.Levels.Where((RewardTrackLevelDbfRecord node) => node.Level <= trackLevel).ToList();
			return CountUnclaimedFreeRewards(nodes, levelState) + (includePremium ? CountUnclaimedPremiumRewards(nodes, levelState) : 0);
		}

		public static int CountUnclaimedFreeRewards(IEnumerable<RewardTrackLevelDbfRecord> nodes, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			return (from state in (from node in nodes.Where(NodeHasFreeTrackRewards)
					select node.Level).Select(levelState)
				select state.FreeRewardStatus).Cast<RewardTrackManager.RewardStatus>().Count(HasUnclaimedTrackReward);
		}

		public static int CountUnclaimedPremiumRewards(IEnumerable<RewardTrackLevelDbfRecord> nodes, Func<int, PlayerRewardTrackLevelState> levelState)
		{
			return (from state in (from node in nodes.Where(NodeHasPaidTrackRewards)
					select node.Level).Select(levelState)
				select state.PaidRewardStatus).Cast<RewardTrackManager.RewardStatus>().Count(HasUnclaimedTrackReward);
		}

		private static bool NodeHasFreeTrackRewards(RewardTrackLevelDbfRecord record)
		{
			RewardListDbfRecord freeRewardListRecord = record.FreeRewardListRecord;
			if (freeRewardListRecord == null)
			{
				return false;
			}
			return freeRewardListRecord.RewardItems?.Count > 0;
		}

		private static bool NodeHasPaidTrackRewards(RewardTrackLevelDbfRecord record)
		{
			RewardListDbfRecord paidRewardListRecord = record.PaidRewardListRecord;
			if (paidRewardListRecord == null)
			{
				return false;
			}
			return paidRewardListRecord.RewardItems?.Count > 0;
		}
	}
}
