using System.ComponentModel;

namespace Assets
{
	public static class RewardLevel
	{
		public enum SubscriptionRequirement
		{
			[Description("free")]
			FREE,
			[Description("paid")]
			PAID
		}

		public static SubscriptionRequirement ParseSubscriptionRequirementValue(string value)
		{
			EnumUtils.TryGetEnum<SubscriptionRequirement>(value, out var outVal);
			return outVal;
		}
	}
}
