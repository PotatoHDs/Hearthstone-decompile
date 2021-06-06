using System;
using Hearthstone.DataModels;

namespace Hearthstone.Progression
{
	// Token: 0x020010FF RID: 4351
	public static class Stats
	{
		// Token: 0x0600BE9D RID: 48797 RVA: 0x003A1706 File Offset: 0x0039F906
		public static void UpdateCompletionPercentage(this AchievementStatsDataModel source)
		{
			source.CompletionPercentage = ProgressUtils.FormatCompletionPercentageMessage(source.Points, source.AvailablePoints);
		}
	}
}
