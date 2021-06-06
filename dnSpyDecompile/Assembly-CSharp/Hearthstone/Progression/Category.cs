using System;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x020010FB RID: 4347
	public static class Category
	{
		// Token: 0x0600BE80 RID: 48768 RVA: 0x003A1170 File Offset: 0x0039F370
		public static int CountAvailablePoints(this AchievementCategoryDbfRecord source)
		{
			return source.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => subcategory.CountAvailablePoints());
		}

		// Token: 0x0600BE81 RID: 48769 RVA: 0x003A119C File Offset: 0x0039F39C
		public static int CountAchievements(this AchievementCategoryDbfRecord source)
		{
			return source.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => subcategory.CountAchievements());
		}

		// Token: 0x0600BE82 RID: 48770 RVA: 0x003A11C8 File Offset: 0x0039F3C8
		public static void SelectSubcategory(this AchievementCategoryDataModel source, AchievementSubcategoryDataModel subcategory, Func<int, PlayerAchievementState> playerState)
		{
			if (subcategory == source.SelectedSubcategory)
			{
				return;
			}
			if (!source.Subcategories.Subcategories.Contains(subcategory))
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Category ",
					source.Name,
					" does not contain subcategory ",
					subcategory.Name,
					"."
				}));
				return;
			}
			AchievementSubcategoryDataModel selectedSubcategory = source.SelectedSubcategory;
			if (selectedSubcategory != null)
			{
				selectedSubcategory.UnloadAchievements();
			}
			source.SelectedSubcategory = subcategory;
			AchievementSubcategoryDataModel selectedSubcategory2 = source.SelectedSubcategory;
			if (selectedSubcategory2 == null)
			{
				return;
			}
			selectedSubcategory2.LoadAchievements(playerState);
		}
	}
}
