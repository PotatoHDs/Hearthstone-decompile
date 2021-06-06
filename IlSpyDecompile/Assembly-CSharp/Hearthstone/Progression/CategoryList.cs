using System.Linq;

namespace Hearthstone.Progression
{
	public static class CategoryList
	{
		public static int CountAvailablePoints()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum((AchievementCategoryDbfRecord category) => category.CountAvailablePoints());
		}

		public static int CountAchievements()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum((AchievementCategoryDbfRecord category) => category.CountAchievements());
		}
	}
}
