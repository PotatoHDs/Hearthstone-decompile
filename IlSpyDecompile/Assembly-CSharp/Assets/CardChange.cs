using System.ComponentModel;

namespace Assets
{
	public static class CardChange
	{
		public enum ChangeType
		{
			[Description("Invalid")]
			INVALID,
			[Description("Buff")]
			BUFF,
			[Description("Nerf")]
			NERF,
			[Description("Addition")]
			ADDITION,
			[Description("Hall_Of_Fame")]
			HALL_OF_FAME
		}

		public static ChangeType ParseChangeTypeValue(string value)
		{
			EnumUtils.TryGetEnum<ChangeType>(value, out var outVal);
			return outVal;
		}
	}
}
