using Hearthstone.DataModels;

namespace Hearthstone.Progression
{
	public static class Stats
	{
		public static void UpdateCompletionPercentage(this AchievementStatsDataModel source)
		{
			source.CompletionPercentage = ProgressUtils.FormatCompletionPercentageMessage(source.Points, source.AvailablePoints);
		}
	}
}
