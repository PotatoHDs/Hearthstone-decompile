using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.Extensions;
using Hearthstone.UI;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public static class Section
	{
		public static DataModelList<AchievementDataModel> GetCurrentSortedAchievements(this AchievementSectionDataModel source)
		{
			return source.Achievements.Achievements.Where((AchievementDataModel achievement) => achievement.IsCurrentTierInList(source.Achievements.Achievements)).Sorted().ToDataModelList();
		}

		public static void LoadAchievements(this AchievementSectionDataModel source, Func<int, PlayerAchievementState> playerState)
		{
			source.Achievements.Achievements = AchievementFactory.CreateAchievementDataModelList(source.ID, playerState);
		}

		public static void UnloadAchievements(this AchievementSectionDataModel source)
		{
			source.Achievements.Achievements.Clear();
		}

		public static void UpdatePreviousChains(this DataModelList<AchievementSectionDataModel> source)
		{
			source.Accumulate(0, delegate(int acc, AchievementSectionDataModel element)
			{
				element.PreviousTileCount = acc;
				return acc + element.TileCount;
			});
		}

		public static IEnumerable<AchievementDbfRecord> Achievements(this AchievementSectionDbfRecord source)
		{
			return GameDbf.Achievement.GetRecords((AchievementDbfRecord record) => record?.AchievementSection == source.ID && record.Enabled);
		}

		public static int CountAvailablePoints(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Sum((AchievementDbfRecord achievement) => achievement.Points);
		}

		public static int CountAchievements(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Count();
		}

		public static int CountChains(this AchievementSectionDbfRecord source)
		{
			return source.Achievements().Count((AchievementDbfRecord achievement) => achievement.NextTier == 0);
		}
	}
}
