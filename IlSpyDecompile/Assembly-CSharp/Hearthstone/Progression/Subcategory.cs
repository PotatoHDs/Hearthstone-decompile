using System;
using System.Linq;
using Blizzard.T5.Core;
using Hearthstone.DataModels;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public static class Subcategory
	{
		public static AchievementCategoryDbfRecord GetAchievementCategory(this AchievementSubcategoryDbfRecord source)
		{
			return GameDbf.AchievementCategory.GetRecord(source.AchievementCategoryId);
		}

		public static int CountAvailablePoints(this AchievementSubcategoryDbfRecord source)
		{
			return source.Sections.Sum((AchievementSectionItemDbfRecord section) => section.AchievementSectionRecord.CountAvailablePoints());
		}

		public static int CountAchievements(this AchievementSubcategoryDbfRecord source)
		{
			return source.Sections.Sum((AchievementSectionItemDbfRecord section) => section.AchievementSectionRecord.CountAchievements());
		}

		public static void LoadAchievements(this AchievementSubcategoryDataModel source, Func<int, PlayerAchievementState> playerState)
		{
			source.Sections.Sections.ForEach(delegate(AchievementSectionDataModel section)
			{
				section.LoadAchievements(playerState);
			});
		}

		public static void UnloadAchievements(this AchievementSubcategoryDataModel source)
		{
			source.Sections.Sections.ForEach(delegate(AchievementSectionDataModel section)
			{
				section.UnloadAchievements();
			});
		}
	}
}
