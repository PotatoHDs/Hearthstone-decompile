using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC3 RID: 4035
	public static class DeckRulesetRule
	{
		// Token: 0x0600B013 RID: 45075 RVA: 0x00366C84 File Offset: 0x00364E84
		public static DeckRulesetRule.RuleType ParseRuleTypeValue(string value)
		{
			DeckRulesetRule.RuleType result;
			EnumUtils.TryGetEnum<DeckRulesetRule.RuleType>(value, out result);
			return result;
		}

		// Token: 0x020027DE RID: 10206
		public enum RuleType
		{
			// Token: 0x0400F682 RID: 63106
			[Description("invalid_rule_type")]
			INVALID_RULE_TYPE,
			// Token: 0x0400F683 RID: 63107
			[Description("has_tag_value")]
			HAS_TAG_VALUE,
			// Token: 0x0400F684 RID: 63108
			[Description("has_odd_numbered_tag_value")]
			HAS_ODD_NUMBERED_TAG_VALUE,
			// Token: 0x0400F685 RID: 63109
			[Description("count_cards_in_deck")]
			COUNT_CARDS_IN_DECK,
			// Token: 0x0400F686 RID: 63110
			[Description("count_copies_of_each_card")]
			COUNT_COPIES_OF_EACH_CARD,
			// Token: 0x0400F687 RID: 63111
			[Description("count_cards_with_tag_value")]
			COUNT_CARDS_WITH_TAG_VALUE,
			// Token: 0x0400F688 RID: 63112
			[Description("count_cards_with_tag_value_odd_numbered")]
			COUNT_CARDS_WITH_TAG_VALUE_ODD_NUMBERED,
			// Token: 0x0400F689 RID: 63113
			[Description("count_cards_with_same_tag_value")]
			COUNT_CARDS_WITH_SAME_TAG_VALUE,
			// Token: 0x0400F68A RID: 63114
			[Description("count_unique_tag_values")]
			COUNT_UNIQUE_TAG_VALUES,
			// Token: 0x0400F68B RID: 63115
			[Description("is_in_any_subset")]
			IS_IN_ANY_SUBSET,
			// Token: 0x0400F68C RID: 63116
			[Description("is_in_all_subsets")]
			IS_IN_ALL_SUBSETS,
			// Token: 0x0400F68D RID: 63117
			[Description("card_text_contains_substring")]
			CARD_TEXT_CONTAINS_SUBSTRING,
			// Token: 0x0400F68E RID: 63118
			[Description("player_owns_each_copy")]
			PLAYER_OWNS_EACH_COPY,
			// Token: 0x0400F68F RID: 63119
			[Description("is_not_rotated")]
			IS_NOT_ROTATED,
			// Token: 0x0400F690 RID: 63120
			[Description("deck_size")]
			DECK_SIZE,
			// Token: 0x0400F691 RID: 63121
			[Description("is_class_or_neutral_card")]
			IS_CLASS_OR_NEUTRAL_CARD,
			// Token: 0x0400F692 RID: 63122
			[Description("is_card_playable")]
			IS_CARD_PLAYABLE,
			// Token: 0x0400F693 RID: 63123
			[Description("is_not_banned_in_league")]
			IS_NOT_BANNED_IN_LEAGUE,
			// Token: 0x0400F694 RID: 63124
			[Description("is_active_in_battlegrounds")]
			IS_ACTIVE_IN_BATTLEGROUNDS,
			// Token: 0x0400F695 RID: 63125
			[Description("is_early_access_in_battlegrounds")]
			IS_EARLY_ACCESS_IN_BATTLEGROUNDS,
			// Token: 0x0400F696 RID: 63126
			[Description("is_in_cardset")]
			IS_IN_CARDSET,
			// Token: 0x0400F697 RID: 63127
			[Description("is_in_format")]
			IS_IN_FORMAT
		}
	}
}
