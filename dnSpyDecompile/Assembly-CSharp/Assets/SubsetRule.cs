using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD5 RID: 4053
	public static class SubsetRule
	{
		// Token: 0x0600B032 RID: 45106 RVA: 0x00366F6C File Offset: 0x0036516C
		public static SubsetRule.Type ParseTypeValue(string value)
		{
			SubsetRule.Type result;
			EnumUtils.TryGetEnum<SubsetRule.Type>(value, out result);
			return result;
		}

		// Token: 0x020027FD RID: 10237
		public enum Type
		{
			// Token: 0x0400F827 RID: 63527
			[Description("invalid")]
			INVALID,
			// Token: 0x0400F828 RID: 63528
			[Description("has_tag_value")]
			HAS_TAG_VALUE,
			// Token: 0x0400F829 RID: 63529
			[Description("has_odd_numbered_tag_value")]
			HAS_ODD_NUMBERED_TAG_VALUE,
			// Token: 0x0400F82A RID: 63530
			[Description("is_card_database_id")]
			IS_CARD_DATABASE_ID,
			// Token: 0x0400F82B RID: 63531
			[Description("is_most_recent_card_set")]
			IS_MOST_RECENT_CARD_SET,
			// Token: 0x0400F82C RID: 63532
			[Description("is_most_recent_arena_card_set")]
			IS_MOST_RECENT_ARENA_CARD_SET,
			// Token: 0x0400F82D RID: 63533
			[Description("is_not_rotated")]
			IS_NOT_ROTATED,
			// Token: 0x0400F82E RID: 63534
			[Description("can_draft")]
			CAN_DRAFT,
			// Token: 0x0400F82F RID: 63535
			[Description("is_card_playable")]
			IS_CARD_PLAYABLE,
			// Token: 0x0400F830 RID: 63536
			[Description("is_active_in_battlegrounds")]
			IS_ACTIVE_IN_BATTLEGROUNDS,
			// Token: 0x0400F831 RID: 63537
			[Description("is_early_access_in_battlegrounds")]
			IS_EARLY_ACCESS_IN_BATTLEGROUNDS
		}
	}
}
