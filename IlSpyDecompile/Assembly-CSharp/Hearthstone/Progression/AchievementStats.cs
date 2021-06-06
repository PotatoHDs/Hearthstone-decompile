using System;
using System.Collections.Generic;
using System.Linq;
using PegasusUtil;

namespace Hearthstone.Progression
{
	public class AchievementStats
	{
		private class Memoizer<TKey, TValue>
		{
			private readonly Dictionary<TKey, TValue> m_values = new Dictionary<TKey, TValue>();

			private readonly Func<TKey, TValue> m_compute;

			public Memoizer(Func<TKey, TValue> compute)
			{
				m_compute = compute;
			}

			public TValue GetValue(TKey key)
			{
				if (m_values.TryGetValue(key, out var value))
				{
					return value;
				}
				value = m_compute(key);
				m_values[key] = value;
				return value;
			}

			public void Invalidate(TKey key)
			{
				m_values.Remove(key);
			}

			public void InvalidateAll()
			{
				m_values.Clear();
			}
		}

		private readonly Memoizer<AchievementCategoryDbfRecord, int> m_categoryPoints;

		private readonly Memoizer<AchievementCategoryDbfRecord, int> m_categoryUnclaimed;

		private readonly Memoizer<AchievementCategoryDbfRecord, int> m_categoryCompleted;

		private readonly Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryPoints;

		private readonly Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryUnclaimed;

		private readonly Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryCompleted;

		private readonly Memoizer<AchievementSectionDbfRecord, int> m_sectionPoints;

		private readonly Memoizer<AchievementSectionDbfRecord, int> m_sectionUnclaimed;

		private readonly Memoizer<AchievementSectionDbfRecord, int> m_sectionCompleted;

		public AchievementStats(Func<int, PlayerAchievementState> playerState)
		{
			m_categoryPoints = new Memoizer<AchievementCategoryDbfRecord, int>(CountCategoryPoints);
			m_categoryUnclaimed = new Memoizer<AchievementCategoryDbfRecord, int>(CountCategoryUnclaimed);
			m_categoryCompleted = new Memoizer<AchievementCategoryDbfRecord, int>(CountCategoryPoints);
			m_subcategoryPoints = new Memoizer<AchievementSubcategoryDbfRecord, int>(CountSubcategoryPoints);
			m_subcategoryUnclaimed = new Memoizer<AchievementSubcategoryDbfRecord, int>(CountSubcategoryUnclaimed);
			m_subcategoryCompleted = new Memoizer<AchievementSubcategoryDbfRecord, int>(CountSubcategoryCompleted);
			m_sectionPoints = new Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => CountSectionPoints(section, playerState));
			m_sectionUnclaimed = new Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => CountSectionUnclaimed(section, playerState));
			m_sectionCompleted = new Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => CountSectionCompleted(section, playerState));
		}

		public int GetTotalPoints()
		{
			return ((IEnumerable<AchievementCategoryDbfRecord>)GameDbf.AchievementCategory.GetRecords()).Sum((Func<AchievementCategoryDbfRecord, int>)GetCategoryPoints);
		}

		public int GetTotalUnclaimed()
		{
			return ((IEnumerable<AchievementCategoryDbfRecord>)GameDbf.AchievementCategory.GetRecords()).Sum((Func<AchievementCategoryDbfRecord, int>)GetCategoryUnclaimed);
		}

		public int GetTotalCompleted()
		{
			return ((IEnumerable<AchievementCategoryDbfRecord>)GameDbf.AchievementCategory.GetRecords()).Sum((Func<AchievementCategoryDbfRecord, int>)GetCategoryCompleted);
		}

		public void InvalidateAll()
		{
			m_categoryUnclaimed.InvalidateAll();
			m_categoryCompleted.InvalidateAll();
			m_subcategoryUnclaimed.InvalidateAll();
			m_subcategoryCompleted.InvalidateAll();
			m_sectionUnclaimed.InvalidateAll();
			m_sectionCompleted.InvalidateAll();
		}

		public int GetCategoryPoints(AchievementCategoryDbfRecord category)
		{
			return m_categoryPoints.GetValue(category);
		}

		public int GetCategoryUnclaimed(AchievementCategoryDbfRecord category)
		{
			return m_categoryUnclaimed.GetValue(category);
		}

		public int GetCategoryCompleted(AchievementCategoryDbfRecord category)
		{
			return m_categoryCompleted.GetValue(category);
		}

		public void InvalidateCategory(AchievementCategoryDbfRecord category)
		{
			m_categoryPoints.Invalidate(category);
			m_categoryUnclaimed.Invalidate(category);
			m_categoryCompleted.Invalidate(category);
		}

		public int GetSubcategoryPoints(AchievementSubcategoryDbfRecord subcategory)
		{
			return m_subcategoryPoints.GetValue(subcategory);
		}

		public int GetSubcategoryUnclaimed(AchievementSubcategoryDbfRecord subcategory)
		{
			return m_subcategoryUnclaimed.GetValue(subcategory);
		}

		public int GetSubcategoryCompleted(AchievementSubcategoryDbfRecord subcategory)
		{
			return m_subcategoryCompleted.GetValue(subcategory);
		}

		public void InvalidSubcategory(AchievementSubcategoryDbfRecord subcategory)
		{
			m_subcategoryPoints.Invalidate(subcategory);
			m_subcategoryUnclaimed.Invalidate(subcategory);
			m_subcategoryCompleted.Invalidate(subcategory);
		}

		public int GetSectionPoints(AchievementSectionDbfRecord section)
		{
			return m_sectionPoints.GetValue(section);
		}

		public int GetSectionUnclaimed(AchievementSectionDbfRecord section)
		{
			return m_sectionUnclaimed.GetValue(section);
		}

		public int GetSectionCompleted(AchievementSectionDbfRecord section)
		{
			return m_sectionCompleted.GetValue(section);
		}

		public void InvalidateSection(AchievementSectionDbfRecord section)
		{
			m_sectionPoints.Invalidate(section);
			m_sectionUnclaimed.Invalidate(section);
			m_sectionCompleted.Invalidate(section);
		}

		private int CountCategoryPoints(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => m_subcategoryPoints.GetValue(subcategory));
		}

		private int CountCategoryUnclaimed(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => m_subcategoryUnclaimed.GetValue(subcategory));
		}

		private int CountCategoryCompleted(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => m_subcategoryCompleted.GetValue(subcategory));
		}

		private int CountSubcategoryPoints(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => m_sectionPoints.GetValue(section.AchievementSectionRecord));
		}

		private int CountSubcategoryUnclaimed(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => m_sectionUnclaimed.GetValue(section.AchievementSectionRecord));
		}

		private int CountSubcategoryCompleted(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => m_sectionCompleted.GetValue(section.AchievementSectionRecord));
		}

		private static int CountSectionPoints(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Sum((AchievementDbfRecord achievement) => achievement.CountPoints(playerState));
		}

		private static int CountSectionUnclaimed(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Count((AchievementDbfRecord achievement) => achievement.IsUnclaimed(playerState));
		}

		private static int CountSectionCompleted(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Count((AchievementDbfRecord achievement) => achievement.IsCompleted(playerState));
		}
	}
}
