using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FCF RID: 4047
	public static class QuestPool
	{
		// Token: 0x0600B02A RID: 45098 RVA: 0x00366EAC File Offset: 0x003650AC
		public static QuestPool.QuestPoolType ParseQuestPoolTypeValue(string value)
		{
			QuestPool.QuestPoolType result;
			EnumUtils.TryGetEnum<QuestPool.QuestPoolType>(value, out result);
			return result;
		}

		// Token: 0x020027F5 RID: 10229
		public enum QuestPoolType
		{
			// Token: 0x0400F7DE RID: 63454
			[Description("none")]
			NONE,
			// Token: 0x0400F7DF RID: 63455
			[Description("daily")]
			DAILY,
			// Token: 0x0400F7E0 RID: 63456
			[Description("weekly")]
			WEEKLY
		}
	}
}
