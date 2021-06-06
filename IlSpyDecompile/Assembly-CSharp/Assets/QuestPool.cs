using System.ComponentModel;

namespace Assets
{
	public static class QuestPool
	{
		public enum QuestPoolType
		{
			[Description("none")]
			NONE,
			[Description("daily")]
			DAILY,
			[Description("weekly")]
			WEEKLY
		}

		public static QuestPoolType ParseQuestPoolTypeValue(string value)
		{
			EnumUtils.TryGetEnum<QuestPoolType>(value, out var outVal);
			return outVal;
		}
	}
}
