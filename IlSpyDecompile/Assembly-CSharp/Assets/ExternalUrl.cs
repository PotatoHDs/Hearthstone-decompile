using System;
using System.ComponentModel;

namespace Assets
{
	public static class ExternalUrl
	{
		[Flags]
		public enum AssetFlags
		{
			[Description("none")]
			NONE = 0x0,
			[Description("dev_only")]
			DEV_ONLY = 0x1,
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 0x2,
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 0x4
		}

		public enum Endpoint
		{
			ACCOUNT,
			ALERT,
			CREATION,
			LANDING,
			FIRESIDE_GATHERINGS,
			ACCOUNT_HEALUP,
			PRIVACY_POLICY,
			EULA,
			DATA_MANAGEMENT,
			SET_ROTATION,
			SYSTEM_REQUIREMENTS,
			RECRUIT_A_FRIEND,
			TERMS_OF_SALES,
			PURCHASE,
			ADD_PAYMENT,
			CVV,
			DUPLICATE_PURCHASE_ERROR,
			DUPLICATE_THIRDPARTY_PURCHASE,
			OUTSTANDING_PURCHASE,
			HEARTHSTONE_ON_IPAD,
			PASSWORD_RESET,
			CHECKOUT,
			CHECKOUT_NAVBAR,
			PAYMENT_INFO,
			GENERIC_PURCHASE_ERROR,
			CUSTOMER_SUPPORT
		}

		public static AssetFlags ParseAssetFlagsValue(string value)
		{
			EnumUtils.TryGetEnum<AssetFlags>(value, out var outVal);
			return outVal;
		}

		public static Endpoint ParseEndpointValue(string value)
		{
			EnumUtils.TryGetEnum<Endpoint>(value, out var outVal);
			return outVal;
		}
	}
}
