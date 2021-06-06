using System;
using System.Linq;
using Blizzard.T5.Core;
using Hearthstone.DataModels;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x020010FC RID: 4348
	public static class Subcategory
	{
		// Token: 0x0600BE83 RID: 48771 RVA: 0x003A1256 File Offset: 0x0039F456
		public static AchievementCategoryDbfRecord GetAchievementCategory(this AchievementSubcategoryDbfRecord source)
		{
			return GameDbf.AchievementCategory.GetRecord(source.AchievementCategoryId);
		}

		// Token: 0x0600BE84 RID: 48772 RVA: 0x003A1268 File Offset: 0x0039F468
		public static int CountAvailablePoints(this AchievementSubcategoryDbfRecord source)
		{
			return source.Sections.Sum((AchievementSectionItemDbfRecord section) => section.AchievementSectionRecord.CountAvailablePoints());
		}

		// Token: 0x0600BE85 RID: 48773 RVA: 0x003A1294 File Offset: 0x0039F494
		public static int CountAchievements(this AchievementSubcategoryDbfRecord source)
		{
			return source.Sections.Sum((AchievementSectionItemDbfRecord section) => section.AchievementSectionRecord.CountAchievements());
		}

		// Token: 0x0600BE86 RID: 48774 RVA: 0x003A12C0 File Offset: 0x0039F4C0
		public static void LoadAchievements(this AchievementSubcategoryDataModel source, Func<int, PlayerAchievementState> playerState)
		{
			source.Sections.Sections.ForEach(delegate(AchievementSectionDataModel section)
			{
				section.LoadAchievements(playerState);
			});
		}

		// Token: 0x0600BE87 RID: 48775 RVA: 0x003A12F6 File Offset: 0x0039F4F6
		public static void UnloadAchievements(this AchievementSubcategoryDataModel source)
		{
			source.Sections.Sections.ForEach(delegate(AchievementSectionDataModel section)
			{
				section.UnloadAchievements();
			});
		}
	}
}
