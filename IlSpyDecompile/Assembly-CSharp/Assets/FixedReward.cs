using System.ComponentModel;

namespace Assets
{
	public static class FixedReward
	{
		public enum Type
		{
			[Description("unknown")]
			UNKNOWN,
			[Description("virtual_card")]
			VIRTUAL_CARD,
			[Description("real_card")]
			REAL_CARD,
			[Description("cardback")]
			CARDBACK,
			[Description("craftable_card")]
			CRAFTABLE_CARD,
			[Description("meta_action_flags")]
			META_ACTION_FLAGS
		}

		public static Type ParseTypeValue(string value)
		{
			EnumUtils.TryGetEnum<Type>(value, out var outVal);
			return outVal;
		}
	}
}
