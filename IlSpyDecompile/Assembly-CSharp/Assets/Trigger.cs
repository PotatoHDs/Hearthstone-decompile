using System.ComponentModel;

namespace Assets
{
	public static class Trigger
	{
		public enum Triggertype
		{
			[Description("lua")]
			LUA,
			[Description("collect")]
			COLLECT,
			[Description("collect_non_unique_cards")]
			COLLECT_NON_UNIQUE_CARDS
		}

		public static Triggertype ParseTriggertypeValue(string value)
		{
			EnumUtils.TryGetEnum<Triggertype>(value, out var outVal);
			return outVal;
		}
	}
}
