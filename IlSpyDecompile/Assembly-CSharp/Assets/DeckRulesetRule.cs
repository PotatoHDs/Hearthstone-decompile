using System.ComponentModel;

namespace Assets
{
	public static class DeckRulesetRule
	{
		public enum RuleType
		{
			[Description("invalid_rule_type")]
			INVALID_RULE_TYPE,
			[Description("has_tag_value")]
			HAS_TAG_VALUE,
			[Description("has_odd_numbered_tag_value")]
			HAS_ODD_NUMBERED_TAG_VALUE,
			[Description("count_cards_in_deck")]
			COUNT_CARDS_IN_DECK,
			[Description("count_copies_of_each_card")]
			COUNT_COPIES_OF_EACH_CARD,
			[Description("count_cards_with_tag_value")]
			COUNT_CARDS_WITH_TAG_VALUE,
			[Description("count_cards_with_tag_value_odd_numbered")]
			COUNT_CARDS_WITH_TAG_VALUE_ODD_NUMBERED,
			[Description("count_cards_with_same_tag_value")]
			COUNT_CARDS_WITH_SAME_TAG_VALUE,
			[Description("count_unique_tag_values")]
			COUNT_UNIQUE_TAG_VALUES,
			[Description("is_in_any_subset")]
			IS_IN_ANY_SUBSET,
			[Description("is_in_all_subsets")]
			IS_IN_ALL_SUBSETS,
			[Description("card_text_contains_substring")]
			CARD_TEXT_CONTAINS_SUBSTRING,
			[Description("player_owns_each_copy")]
			PLAYER_OWNS_EACH_COPY,
			[Description("is_not_rotated")]
			IS_NOT_ROTATED,
			[Description("deck_size")]
			DECK_SIZE,
			[Description("is_class_or_neutral_card")]
			IS_CLASS_OR_NEUTRAL_CARD,
			[Description("is_card_playable")]
			IS_CARD_PLAYABLE,
			[Description("is_not_banned_in_league")]
			IS_NOT_BANNED_IN_LEAGUE,
			[Description("is_active_in_battlegrounds")]
			IS_ACTIVE_IN_BATTLEGROUNDS,
			[Description("is_early_access_in_battlegrounds")]
			IS_EARLY_ACCESS_IN_BATTLEGROUNDS,
			[Description("is_in_cardset")]
			IS_IN_CARDSET,
			[Description("is_in_format")]
			IS_IN_FORMAT
		}

		public static RuleType ParseRuleTypeValue(string value)
		{
			EnumUtils.TryGetEnum<RuleType>(value, out var outVal);
			return outVal;
		}
	}
}
