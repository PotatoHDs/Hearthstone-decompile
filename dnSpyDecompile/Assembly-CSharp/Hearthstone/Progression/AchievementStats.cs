using System;
using System.Collections.Generic;
using System.Linq;
using PegasusUtil;

namespace Hearthstone.Progression
{
	// Token: 0x02001106 RID: 4358
	public class AchievementStats
	{
		// Token: 0x0600BEF5 RID: 48885 RVA: 0x003A356C File Offset: 0x003A176C
		public AchievementStats(Func<int, PlayerAchievementState> playerState)
		{
			this.m_categoryPoints = new AchievementStats.Memoizer<AchievementCategoryDbfRecord, int>(new Func<AchievementCategoryDbfRecord, int>(this.CountCategoryPoints));
			this.m_categoryUnclaimed = new AchievementStats.Memoizer<AchievementCategoryDbfRecord, int>(new Func<AchievementCategoryDbfRecord, int>(this.CountCategoryUnclaimed));
			this.m_categoryCompleted = new AchievementStats.Memoizer<AchievementCategoryDbfRecord, int>(new Func<AchievementCategoryDbfRecord, int>(this.CountCategoryPoints));
			this.m_subcategoryPoints = new AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int>(new Func<AchievementSubcategoryDbfRecord, int>(this.CountSubcategoryPoints));
			this.m_subcategoryUnclaimed = new AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int>(new Func<AchievementSubcategoryDbfRecord, int>(this.CountSubcategoryUnclaimed));
			this.m_subcategoryCompleted = new AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int>(new Func<AchievementSubcategoryDbfRecord, int>(this.CountSubcategoryCompleted));
			this.m_sectionPoints = new AchievementStats.Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => AchievementStats.CountSectionPoints(section, playerState));
			this.m_sectionUnclaimed = new AchievementStats.Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => AchievementStats.CountSectionUnclaimed(section, playerState));
			this.m_sectionCompleted = new AchievementStats.Memoizer<AchievementSectionDbfRecord, int>((AchievementSectionDbfRecord section) => AchievementStats.CountSectionCompleted(section, playerState));
		}

		// Token: 0x0600BEF6 RID: 48886 RVA: 0x003A365B File Offset: 0x003A185B
		public int GetTotalPoints()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum(new Func<AchievementCategoryDbfRecord, int>(this.GetCategoryPoints));
		}

		// Token: 0x0600BEF7 RID: 48887 RVA: 0x003A3678 File Offset: 0x003A1878
		public int GetTotalUnclaimed()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum(new Func<AchievementCategoryDbfRecord, int>(this.GetCategoryUnclaimed));
		}

		// Token: 0x0600BEF8 RID: 48888 RVA: 0x003A3695 File Offset: 0x003A1895
		public int GetTotalCompleted()
		{
			return GameDbf.AchievementCategory.GetRecords().Sum(new Func<AchievementCategoryDbfRecord, int>(this.GetCategoryCompleted));
		}

		// Token: 0x0600BEF9 RID: 48889 RVA: 0x003A36B4 File Offset: 0x003A18B4
		public void InvalidateAll()
		{
			this.m_categoryUnclaimed.InvalidateAll();
			this.m_categoryCompleted.InvalidateAll();
			this.m_subcategoryUnclaimed.InvalidateAll();
			this.m_subcategoryCompleted.InvalidateAll();
			this.m_sectionUnclaimed.InvalidateAll();
			this.m_sectionCompleted.InvalidateAll();
		}

		// Token: 0x0600BEFA RID: 48890 RVA: 0x003A3703 File Offset: 0x003A1903
		public int GetCategoryPoints(AchievementCategoryDbfRecord category)
		{
			return this.m_categoryPoints.GetValue(category);
		}

		// Token: 0x0600BEFB RID: 48891 RVA: 0x003A3711 File Offset: 0x003A1911
		public int GetCategoryUnclaimed(AchievementCategoryDbfRecord category)
		{
			return this.m_categoryUnclaimed.GetValue(category);
		}

		// Token: 0x0600BEFC RID: 48892 RVA: 0x003A371F File Offset: 0x003A191F
		public int GetCategoryCompleted(AchievementCategoryDbfRecord category)
		{
			return this.m_categoryCompleted.GetValue(category);
		}

		// Token: 0x0600BEFD RID: 48893 RVA: 0x003A372D File Offset: 0x003A192D
		public void InvalidateCategory(AchievementCategoryDbfRecord category)
		{
			this.m_categoryPoints.Invalidate(category);
			this.m_categoryUnclaimed.Invalidate(category);
			this.m_categoryCompleted.Invalidate(category);
		}

		// Token: 0x0600BEFE RID: 48894 RVA: 0x003A3753 File Offset: 0x003A1953
		public int GetSubcategoryPoints(AchievementSubcategoryDbfRecord subcategory)
		{
			return this.m_subcategoryPoints.GetValue(subcategory);
		}

		// Token: 0x0600BEFF RID: 48895 RVA: 0x003A3761 File Offset: 0x003A1961
		public int GetSubcategoryUnclaimed(AchievementSubcategoryDbfRecord subcategory)
		{
			return this.m_subcategoryUnclaimed.GetValue(subcategory);
		}

		// Token: 0x0600BF00 RID: 48896 RVA: 0x003A376F File Offset: 0x003A196F
		public int GetSubcategoryCompleted(AchievementSubcategoryDbfRecord subcategory)
		{
			return this.m_subcategoryCompleted.GetValue(subcategory);
		}

		// Token: 0x0600BF01 RID: 48897 RVA: 0x003A377D File Offset: 0x003A197D
		public void InvalidSubcategory(AchievementSubcategoryDbfRecord subcategory)
		{
			this.m_subcategoryPoints.Invalidate(subcategory);
			this.m_subcategoryUnclaimed.Invalidate(subcategory);
			this.m_subcategoryCompleted.Invalidate(subcategory);
		}

		// Token: 0x0600BF02 RID: 48898 RVA: 0x003A37A3 File Offset: 0x003A19A3
		public int GetSectionPoints(AchievementSectionDbfRecord section)
		{
			return this.m_sectionPoints.GetValue(section);
		}

		// Token: 0x0600BF03 RID: 48899 RVA: 0x003A37B1 File Offset: 0x003A19B1
		public int GetSectionUnclaimed(AchievementSectionDbfRecord section)
		{
			return this.m_sectionUnclaimed.GetValue(section);
		}

		// Token: 0x0600BF04 RID: 48900 RVA: 0x003A37BF File Offset: 0x003A19BF
		public int GetSectionCompleted(AchievementSectionDbfRecord section)
		{
			return this.m_sectionCompleted.GetValue(section);
		}

		// Token: 0x0600BF05 RID: 48901 RVA: 0x003A37CD File Offset: 0x003A19CD
		public void InvalidateSection(AchievementSectionDbfRecord section)
		{
			this.m_sectionPoints.Invalidate(section);
			this.m_sectionUnclaimed.Invalidate(section);
			this.m_sectionCompleted.Invalidate(section);
		}

		// Token: 0x0600BF06 RID: 48902 RVA: 0x003A37F3 File Offset: 0x003A19F3
		private int CountCategoryPoints(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => this.m_subcategoryPoints.GetValue(subcategory));
		}

		// Token: 0x0600BF07 RID: 48903 RVA: 0x003A380C File Offset: 0x003A1A0C
		private int CountCategoryUnclaimed(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => this.m_subcategoryUnclaimed.GetValue(subcategory));
		}

		// Token: 0x0600BF08 RID: 48904 RVA: 0x003A3825 File Offset: 0x003A1A25
		private int CountCategoryCompleted(AchievementCategoryDbfRecord category)
		{
			return category.Subcategories.Sum((AchievementSubcategoryDbfRecord subcategory) => this.m_subcategoryCompleted.GetValue(subcategory));
		}

		// Token: 0x0600BF09 RID: 48905 RVA: 0x003A383E File Offset: 0x003A1A3E
		private int CountSubcategoryPoints(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => this.m_sectionPoints.GetValue(section.AchievementSectionRecord));
		}

		// Token: 0x0600BF0A RID: 48906 RVA: 0x003A3857 File Offset: 0x003A1A57
		private int CountSubcategoryUnclaimed(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => this.m_sectionUnclaimed.GetValue(section.AchievementSectionRecord));
		}

		// Token: 0x0600BF0B RID: 48907 RVA: 0x003A3870 File Offset: 0x003A1A70
		private int CountSubcategoryCompleted(AchievementSubcategoryDbfRecord subcategory)
		{
			return subcategory.Sections.Sum((AchievementSectionItemDbfRecord section) => this.m_sectionCompleted.GetValue(section.AchievementSectionRecord));
		}

		// Token: 0x0600BF0C RID: 48908 RVA: 0x003A388C File Offset: 0x003A1A8C
		private static int CountSectionPoints(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Sum((AchievementDbfRecord achievement) => achievement.CountPoints(playerState));
		}

		// Token: 0x0600BF0D RID: 48909 RVA: 0x003A38C0 File Offset: 0x003A1AC0
		private static int CountSectionUnclaimed(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Count((AchievementDbfRecord achievement) => achievement.IsUnclaimed(playerState));
		}

		// Token: 0x0600BF0E RID: 48910 RVA: 0x003A38F4 File Offset: 0x003A1AF4
		private static int CountSectionCompleted(AchievementSectionDbfRecord section, Func<int, PlayerAchievementState> playerState)
		{
			return section.Achievements().Count((AchievementDbfRecord achievement) => achievement.IsCompleted(playerState));
		}

		// Token: 0x04009B35 RID: 39733
		private readonly AchievementStats.Memoizer<AchievementCategoryDbfRecord, int> m_categoryPoints;

		// Token: 0x04009B36 RID: 39734
		private readonly AchievementStats.Memoizer<AchievementCategoryDbfRecord, int> m_categoryUnclaimed;

		// Token: 0x04009B37 RID: 39735
		private readonly AchievementStats.Memoizer<AchievementCategoryDbfRecord, int> m_categoryCompleted;

		// Token: 0x04009B38 RID: 39736
		private readonly AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryPoints;

		// Token: 0x04009B39 RID: 39737
		private readonly AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryUnclaimed;

		// Token: 0x04009B3A RID: 39738
		private readonly AchievementStats.Memoizer<AchievementSubcategoryDbfRecord, int> m_subcategoryCompleted;

		// Token: 0x04009B3B RID: 39739
		private readonly AchievementStats.Memoizer<AchievementSectionDbfRecord, int> m_sectionPoints;

		// Token: 0x04009B3C RID: 39740
		private readonly AchievementStats.Memoizer<AchievementSectionDbfRecord, int> m_sectionUnclaimed;

		// Token: 0x04009B3D RID: 39741
		private readonly AchievementStats.Memoizer<AchievementSectionDbfRecord, int> m_sectionCompleted;

		// Token: 0x020028D2 RID: 10450
		private class Memoizer<TKey, TValue>
		{
			// Token: 0x06013D06 RID: 81158 RVA: 0x0053D152 File Offset: 0x0053B352
			public Memoizer(Func<TKey, TValue> compute)
			{
				this.m_compute = compute;
			}

			// Token: 0x06013D07 RID: 81159 RVA: 0x0053D16C File Offset: 0x0053B36C
			public TValue GetValue(TKey key)
			{
				TValue tvalue;
				if (this.m_values.TryGetValue(key, out tvalue))
				{
					return tvalue;
				}
				tvalue = this.m_compute(key);
				this.m_values[key] = tvalue;
				return tvalue;
			}

			// Token: 0x06013D08 RID: 81160 RVA: 0x0053D1A6 File Offset: 0x0053B3A6
			public void Invalidate(TKey key)
			{
				this.m_values.Remove(key);
			}

			// Token: 0x06013D09 RID: 81161 RVA: 0x0053D1B5 File Offset: 0x0053B3B5
			public void InvalidateAll()
			{
				this.m_values.Clear();
			}

			// Token: 0x0400FAFC RID: 64252
			private readonly Dictionary<TKey, TValue> m_values = new Dictionary<TKey, TValue>();

			// Token: 0x0400FAFD RID: 64253
			private readonly Func<TKey, TValue> m_compute;
		}
	}
}
