using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FBC RID: 4028
	public static class AdventureData
	{
		// Token: 0x0600B00B RID: 45067 RVA: 0x00366BC4 File Offset: 0x00364DC4
		public static AdventureData.Adventuresubscene ParseAdventuresubsceneValue(string value)
		{
			AdventureData.Adventuresubscene result;
			EnumUtils.TryGetEnum<AdventureData.Adventuresubscene>(value, out result);
			return result;
		}

		// Token: 0x0600B00C RID: 45068 RVA: 0x00366BDC File Offset: 0x00364DDC
		public static AdventureData.Adventurebooklocation ParseAdventurebooklocationValue(string value)
		{
			AdventureData.Adventurebooklocation result;
			EnumUtils.TryGetEnum<AdventureData.Adventurebooklocation>(value, out result);
			return result;
		}

		// Token: 0x020027D6 RID: 10198
		public enum Adventuresubscene
		{
			// Token: 0x0400F632 RID: 63026
			[Description("Invalid")]
			INVALID = -1,
			// Token: 0x0400F633 RID: 63027
			[Description("Chooser")]
			CHOOSER,
			// Token: 0x0400F634 RID: 63028
			[Description("Practice")]
			PRACTICE,
			// Token: 0x0400F635 RID: 63029
			[Description("Mission_Deck_Picker")]
			MISSION_DECK_PICKER,
			// Token: 0x0400F636 RID: 63030
			[Description("Mission_Display")]
			MISSION_DISPLAY,
			// Token: 0x0400F637 RID: 63031
			[Description("Class_Challenge")]
			CLASS_CHALLENGE,
			// Token: 0x0400F638 RID: 63032
			[Description("Dungeon_Crawl")]
			DUNGEON_CRAWL,
			// Token: 0x0400F639 RID: 63033
			[Description("Adventurer_Picker")]
			ADVENTURER_PICKER,
			// Token: 0x0400F63A RID: 63034
			[Description("Bonus_Challenge")]
			BONUS_CHALLENGE,
			// Token: 0x0400F63B RID: 63035
			[Description("Location_Select")]
			LOCATION_SELECT
		}

		// Token: 0x020027D7 RID: 10199
		public enum Adventurebooklocation
		{
			// Token: 0x0400F63D RID: 63037
			[Description("Nowhere")]
			NOWHERE = -1,
			// Token: 0x0400F63E RID: 63038
			[Description("Beginning")]
			BEGINNING,
			// Token: 0x0400F63F RID: 63039
			[Description("End")]
			END
		}
	}
}
