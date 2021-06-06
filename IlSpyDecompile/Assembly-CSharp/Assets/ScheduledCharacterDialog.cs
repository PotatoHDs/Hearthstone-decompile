using System.ComponentModel;

namespace Assets
{
	public static class ScheduledCharacterDialog
	{
		public enum Event
		{
			[Description("login_flow_complete")]
			LOGIN_FLOW_COMPLETE,
			[Description("all_popups_shown")]
			ALL_POPUPS_SHOWN,
			[Description("welcome_quests_shown")]
			WELCOME_QUESTS_SHOWN,
			[Description("generic_reward_shown")]
			GENERIC_REWARD_SHOWN,
			[Description("entered_arena_draft")]
			ENTERED_ARENA_DRAFT,
			[Description("arena_reward_shown")]
			ARENA_REWARD_SHOWN,
			[Description("double_gold_quest_granted")]
			DOUBLE_GOLD_QUEST_GRANTED,
			[Description("entered_battlegrounds")]
			ENTERED_BATTLEGROUNDS,
			[Description("entered_tavern_brawl")]
			ENTERED_TAVERN_BRAWL,
			[Description("purchased_bundle")]
			PURCHASED_BUNDLE
		}

		public static Event ParseEventValue(string value)
		{
			EnumUtils.TryGetEnum<Event>(value, out var outVal);
			return outVal;
		}
	}
}
