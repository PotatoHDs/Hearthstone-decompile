using System.ComponentModel;

namespace Assets
{
	public static class DraftContent
	{
		public enum SlotType
		{
			[Description("none")]
			NONE,
			[Description("card")]
			CARD,
			[Description("hero")]
			HERO,
			[Description("hero_power")]
			HERO_POWER
		}

		public static SlotType ParseSlotTypeValue(string value)
		{
			EnumUtils.TryGetEnum<SlotType>(value, out var outVal);
			return outVal;
		}
	}
}
