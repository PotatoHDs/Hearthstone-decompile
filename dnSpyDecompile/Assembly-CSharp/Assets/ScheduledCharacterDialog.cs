using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FD4 RID: 4052
	public static class ScheduledCharacterDialog
	{
		// Token: 0x0600B031 RID: 45105 RVA: 0x00366F54 File Offset: 0x00365154
		public static ScheduledCharacterDialog.Event ParseEventValue(string value)
		{
			ScheduledCharacterDialog.Event result;
			EnumUtils.TryGetEnum<ScheduledCharacterDialog.Event>(value, out result);
			return result;
		}

		// Token: 0x020027FC RID: 10236
		public enum Event
		{
			// Token: 0x0400F81C RID: 63516
			[Description("login_flow_complete")]
			LOGIN_FLOW_COMPLETE,
			// Token: 0x0400F81D RID: 63517
			[Description("all_popups_shown")]
			ALL_POPUPS_SHOWN,
			// Token: 0x0400F81E RID: 63518
			[Description("welcome_quests_shown")]
			WELCOME_QUESTS_SHOWN,
			// Token: 0x0400F81F RID: 63519
			[Description("generic_reward_shown")]
			GENERIC_REWARD_SHOWN,
			// Token: 0x0400F820 RID: 63520
			[Description("entered_arena_draft")]
			ENTERED_ARENA_DRAFT,
			// Token: 0x0400F821 RID: 63521
			[Description("arena_reward_shown")]
			ARENA_REWARD_SHOWN,
			// Token: 0x0400F822 RID: 63522
			[Description("double_gold_quest_granted")]
			DOUBLE_GOLD_QUEST_GRANTED,
			// Token: 0x0400F823 RID: 63523
			[Description("entered_battlegrounds")]
			ENTERED_BATTLEGROUNDS,
			// Token: 0x0400F824 RID: 63524
			[Description("entered_tavern_brawl")]
			ENTERED_TAVERN_BRAWL,
			// Token: 0x0400F825 RID: 63525
			[Description("purchased_bundle")]
			PURCHASED_BUNDLE
		}
	}
}
