using System;
using System.Linq;

namespace Hearthstone.Progression
{
	// Token: 0x020010FA RID: 4346
	public static class CategoryList
	{
		// Token: 0x0600BE7E RID: 48766 RVA: 0x003A1110 File Offset: 0x0039F310
		public static int CountAvailablePoints()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum((AchievementCategoryDbfRecord category) => category.CountAvailablePoints());
		}

		// Token: 0x0600BE7F RID: 48767 RVA: 0x003A1140 File Offset: 0x0039F340
		public static int CountAchievements()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum((AchievementCategoryDbfRecord category) => category.CountAchievements());
		}
	}
}
