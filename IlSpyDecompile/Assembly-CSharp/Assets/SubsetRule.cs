using System.ComponentModel;

namespace Assets
{
	public static class SubsetRule
	{
		public enum Type
		{
			[Description("invalid")]
			INVALID,
			[Description("has_tag_value")]
			HAS_TAG_VALUE,
			[Description("has_odd_numbered_tag_value")]
			HAS_ODD_NUMBERED_TAG_VALUE,
			[Description("is_card_database_id")]
			IS_CARD_DATABASE_ID,
			[Description("is_most_recent_card_set")]
			IS_MOST_RECENT_CARD_SET,
			[Description("is_most_recent_arena_card_set")]
			IS_MOST_RECENT_ARENA_CARD_SET,
			[Description("is_not_rotated")]
			IS_NOT_ROTATED,
			[Description("can_draft")]
			CAN_DRAFT,
			[Description("is_card_playable")]
			IS_CARD_PLAYABLE,
			[Description("is_active_in_battlegrounds")]
			IS_ACTIVE_IN_BATTLEGROUNDS,
			[Description("is_early_access_in_battlegrounds")]
			IS_EARLY_ACCESS_IN_BATTLEGROUNDS
		}

		public static Type ParseTypeValue(string value)
		{
			EnumUtils.TryGetEnum<Type>(value, out var outVal);
			return outVal;
		}
	}
}
