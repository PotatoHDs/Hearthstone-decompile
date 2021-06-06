using System.ComponentModel;

namespace Assets
{
	public static class AdventureData
	{
		public enum Adventuresubscene
		{
			[Description("Invalid")]
			INVALID = -1,
			[Description("Chooser")]
			CHOOSER,
			[Description("Practice")]
			PRACTICE,
			[Description("Mission_Deck_Picker")]
			MISSION_DECK_PICKER,
			[Description("Mission_Display")]
			MISSION_DISPLAY,
			[Description("Class_Challenge")]
			CLASS_CHALLENGE,
			[Description("Dungeon_Crawl")]
			DUNGEON_CRAWL,
			[Description("Adventurer_Picker")]
			ADVENTURER_PICKER,
			[Description("Bonus_Challenge")]
			BONUS_CHALLENGE,
			[Description("Location_Select")]
			LOCATION_SELECT
		}

		public enum Adventurebooklocation
		{
			[Description("Nowhere")]
			NOWHERE = -1,
			[Description("Beginning")]
			BEGINNING,
			[Description("End")]
			END
		}

		public static Adventuresubscene ParseAdventuresubsceneValue(string value)
		{
			EnumUtils.TryGetEnum<Adventuresubscene>(value, out var outVal);
			return outVal;
		}

		public static Adventurebooklocation ParseAdventurebooklocationValue(string value)
		{
			EnumUtils.TryGetEnum<Adventurebooklocation>(value, out var outVal);
			return outVal;
		}
	}
}
