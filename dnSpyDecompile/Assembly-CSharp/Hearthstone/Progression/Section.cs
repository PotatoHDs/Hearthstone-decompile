using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.Extensions;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x020010FD RID: 4349
	public static class Section
	{
		// Token: 0x0600BE88 RID: 48776 RVA: 0x003A1328 File Offset: 0x0039F528
		public static DataModelList<AchievementDataModel> GetCurrentSortedAchievements(this AchievementSectionDataModel source)
		{
			return (from achievement in source.Achievements.Achievements
			where achievement.IsCurrentTierInList(source.Achievements.Achievements)
			select achievement).Sorted().ToDataModelList<AchievementDataModel>();
		}

		// Token: 0x0600BE89 RID: 48777 RVA: 0x003A136D File Offset: 0x0039F56D
		public static void LoadAchievements(this AchievementSectionDataModel source, Func<int, PlayerAchievementState> playerState)
		{
			source.Achievements.Achievements = AchievementFactory.CreateAchievementDataModelList(source.ID, playerState);
		}

		// Token: 0x0600BE8A RID: 48778 RVA: 0x003A1386 File Offset: 0x0039F586
		public static void UnloadAchievements(this AchievementSectionDataModel source)
		{
			source.Achievements.Achievements.Clear();
		}

		// Token: 0x0600BE8B RID: 48779 RVA: 0x003A1398 File Offset: 0x0039F598
		public static void UpdatePreviousChains(this DataModelList<AchievementSectionDataModel> source)
		{
			source.Accumulate(0, delegate(int acc, AchievementSectionDataModel element)
			{
				element.PreviousTileCount = acc;
				return acc + element.TileCount;
			});
		}

		// Token: 0x0600BE8C RID: 48780 RVA: 0x003A13C0 File Offset: 0x0039F5C0
		public static IEnumerable<AchievementDbfRecord> Achievements(this AchievementSectionDbfRecord source)
		{
			return GameDbf.Achievement.GetRecords(delegate(AchievementDbfRecord record)
			{
				int? num = (record != null) ? new int?(record.AchievementSection) : null;
				int id = source.ID;
				return (num.GetValueOrDefault() == id & num != null) && record.Enabled;
			}, -1);
		}

		// Token: 0x0600BE8D RID: 48781 RVA: 0x003A13F1 File Offset: 0x0039F5F1
		public static int CountAvailablePoints(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Sum((AchievementDbfRecord achievement) => achievement.Points);
		}

		// Token: 0x0600BE8E RID: 48782 RVA: 0x003A141D File Offset: 0x0039F61D
		public static int CountAchievements(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Count<AchievementDbfRecord>();
		}

		// Token: 0x0600BE8F RID: 48783 RVA: 0x003A142A File Offset: 0x0039F62A
		public static int CountChains(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Count((AchievementDbfRecord achievement) => achievement.NextTier == 0);
		}
	}
}
