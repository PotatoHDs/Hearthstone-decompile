using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public static class Achievement
	{
		private static readonly AchievementManager.AchievementStatus[] s_statusOrder = new AchievementManager.AchievementStatus[6]
		{
			AchievementManager.AchievementStatus.COMPLETED,
			AchievementManager.AchievementStatus.ACTIVE,
			AchievementManager.AchievementStatus.REWARD_GRANTED,
			AchievementManager.AchievementStatus.REWARD_ACKED,
			AchievementManager.AchievementStatus.UNKNOWN,
			AchievementManager.AchievementStatus.RESET
		};

		public static void UpdateProgress(this AchievementDataModel source)
		{
			source.ProgressMessage = ProgressUtils.FormatProgressMessage(source.Progress, source.Quota);
		}

		public static void UpdateTiers(this DataModelList<AchievementDataModel> source)
		{
			foreach (AchievementDataModel item in source)
			{
				int num = item.CountReverseChain(source);
				int num2 = item.CountForwardChain(source);
				item.Tier = num;
				item.MaxTier = num + num2 - 1;
				item.TierMessage = ProgressUtils.FormatTierMessage(item.Tier, item.MaxTier);
			}
		}

		public static int CountReverseChain(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return source.CountChain((AchievementDataModel element) => element.FindPreviousAchievement(list));
		}

		public static int CountForwardChain(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return source.CountChain((AchievementDataModel element) => element.FindNextAchievement(list));
		}

		public static int CountChain(this AchievementDataModel source, Func<AchievementDataModel, AchievementDataModel> provider)
		{
			int num = 0;
			while (source != null)
			{
				source = provider(source);
				num++;
			}
			return num;
		}

		public static bool IsCurrentTierInList(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			AchievementDataModel achievementDataModel = source.FindPreviousAchievement(list);
			if (achievementDataModel != null && !ProgressUtils.IsAchievementClaimed(achievementDataModel.Status))
			{
				return false;
			}
			if (!ProgressUtils.IsAchievementClaimed(source.Status))
			{
				return true;
			}
			return source.FindNextAchievement(list) == null;
		}

		public static AchievementDataModel FindPreviousAchievement(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return list.FirstOrDefault((AchievementDataModel element) => element.NextTierID == source.ID);
		}

		public static AchievementDataModel FindNextAchievement(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return list.FirstOrDefault((AchievementDataModel element) => element.ID == source.NextTierID);
		}

		public static IEnumerable<AchievementDataModel> Sorted(this IEnumerable<AchievementDataModel> source)
		{
			return from element in source
				orderby Array.IndexOf(s_statusOrder, element.Status), element.SortOrder, (float)element.Progress / (float)element.Quota descending, element.Name
				select element;
		}

		public static int CountPoints(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			if (!ProgressUtils.IsAchievementClaimed((AchievementManager.AchievementStatus)playerState(source.ID).Status))
			{
				return 0;
			}
			return source.Points;
		}

		public static bool IsCompleted(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			return ProgressUtils.IsAchievementComplete((AchievementManager.AchievementStatus)playerState(source.ID).Status);
		}

		public static bool IsUnclaimed(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			return playerState(source.ID).Status == 2;
		}
	}
}
