using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x020010FE RID: 4350
	public static class Achievement
	{
		// Token: 0x0600BE90 RID: 48784 RVA: 0x003A1456 File Offset: 0x0039F656
		public static void UpdateProgress(this AchievementDataModel source)
		{
			source.ProgressMessage = ProgressUtils.FormatProgressMessage(source.Progress, source.Quota);
		}

		// Token: 0x0600BE91 RID: 48785 RVA: 0x003A1470 File Offset: 0x0039F670
		public static void UpdateTiers(this DataModelList<AchievementDataModel> source)
		{
			foreach (AchievementDataModel achievementDataModel in source)
			{
				int num = achievementDataModel.CountReverseChain(source);
				int num2 = achievementDataModel.CountForwardChain(source);
				achievementDataModel.Tier = num;
				achievementDataModel.MaxTier = num + num2 - 1;
				achievementDataModel.TierMessage = ProgressUtils.FormatTierMessage(achievementDataModel.Tier, achievementDataModel.MaxTier);
			}
		}

		// Token: 0x0600BE92 RID: 48786 RVA: 0x003A14EC File Offset: 0x0039F6EC
		public static int CountReverseChain(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return source.CountChain((AchievementDataModel element) => element.FindPreviousAchievement(list));
		}

		// Token: 0x0600BE93 RID: 48787 RVA: 0x003A1518 File Offset: 0x0039F718
		public static int CountForwardChain(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return source.CountChain((AchievementDataModel element) => element.FindNextAchievement(list));
		}

		// Token: 0x0600BE94 RID: 48788 RVA: 0x003A1544 File Offset: 0x0039F744
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

		// Token: 0x0600BE95 RID: 48789 RVA: 0x003A1568 File Offset: 0x0039F768
		public static bool IsCurrentTierInList(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			AchievementDataModel achievementDataModel = source.FindPreviousAchievement(list);
			return (achievementDataModel == null || ProgressUtils.IsAchievementClaimed(achievementDataModel.Status)) && (!ProgressUtils.IsAchievementClaimed(source.Status) || source.FindNextAchievement(list) == null);
		}

		// Token: 0x0600BE96 RID: 48790 RVA: 0x003A15A8 File Offset: 0x0039F7A8
		public static AchievementDataModel FindPreviousAchievement(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return list.FirstOrDefault((AchievementDataModel element) => element.NextTierID == source.ID);
		}

		// Token: 0x0600BE97 RID: 48791 RVA: 0x003A15D4 File Offset: 0x0039F7D4
		public static AchievementDataModel FindNextAchievement(this AchievementDataModel source, DataModelList<AchievementDataModel> list)
		{
			return list.FirstOrDefault((AchievementDataModel element) => element.ID == source.NextTierID);
		}

		// Token: 0x0600BE98 RID: 48792 RVA: 0x003A1600 File Offset: 0x0039F800
		public static IEnumerable<AchievementDataModel> Sorted(this IEnumerable<AchievementDataModel> source)
		{
			return from element in source
			orderby Array.IndexOf<AchievementManager.AchievementStatus>(Achievement.s_statusOrder, element.Status), element.SortOrder, (float)element.Progress / (float)element.Quota descending, element.Name
			select element;
		}

		// Token: 0x0600BE99 RID: 48793 RVA: 0x003A169E File Offset: 0x0039F89E
		public static int CountPoints(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			if (!ProgressUtils.IsAchievementClaimed((AchievementManager.AchievementStatus)playerState(source.ID).Status))
			{
				return 0;
			}
			return source.Points;
		}

		// Token: 0x0600BE9A RID: 48794 RVA: 0x003A16C0 File Offset: 0x0039F8C0
		public static bool IsCompleted(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			return ProgressUtils.IsAchievementComplete((AchievementManager.AchievementStatus)playerState(source.ID).Status);
		}

		// Token: 0x0600BE9B RID: 48795 RVA: 0x003A16D8 File Offset: 0x0039F8D8
		public static bool IsUnclaimed(this AchievementDbfRecord source, Func<int, PlayerAchievementState> playerState)
		{
			return playerState(source.ID).Status == 2;
		}

		// Token: 0x04009B0F RID: 39695
		private static readonly AchievementManager.AchievementStatus[] s_statusOrder = new AchievementManager.AchievementStatus[]
		{
			AchievementManager.AchievementStatus.COMPLETED,
			AchievementManager.AchievementStatus.ACTIVE,
			AchievementManager.AchievementStatus.REWARD_GRANTED,
			AchievementManager.AchievementStatus.REWARD_ACKED,
			AchievementManager.AchievementStatus.UNKNOWN,
			AchievementManager.AchievementStatus.RESET
		};
	}
}
