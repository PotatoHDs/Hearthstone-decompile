using System;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

namespace Hearthstone.Progression
{
	public static class Category
	{
		public static int CountAvailablePoints(this AchievementCategoryDbfRecord source)
		{
			return source.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => subcategory.CountAvailablePoints());
		}

		public static int CountAchievements(this AchievementCategoryDbfRecord source)
		{
			return source.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => subcategory.CountAchievements());
		}

		public static void SelectSubcategory(this AchievementCategoryDataModel source, AchievementSubcategoryDataModel subcategory, Func<int, PlayerAchievementState> playerState)
		{
			if (subcategory != source.SelectedSubcategory)
			{
				if (!source.Subcategories.Subcategories.Contains(subcategory))
				{
					Debug.LogWarning("Category " + source.Name + " does not contain subcategory " + subcategory.Name + ".");
				}
				else
				{
					source.SelectedSubcategory?.UnloadAchievements();
					source.SelectedSubcategory = subcategory;
					source.SelectedSubcategory?.LoadAchievements(playerState);
				}
			}
		}
	}
}
