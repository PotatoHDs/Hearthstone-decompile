using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC6 RID: 4038
	public static class ExternalUrl
	{
		// Token: 0x0600B016 RID: 45078 RVA: 0x00366CCC File Offset: 0x00364ECC
		public static ExternalUrl.AssetFlags ParseAssetFlagsValue(string value)
		{
			ExternalUrl.AssetFlags result;
			EnumUtils.TryGetEnum<ExternalUrl.AssetFlags>(value, out result);
			return result;
		}

		// Token: 0x0600B017 RID: 45079 RVA: 0x00366CE4 File Offset: 0x00364EE4
		public static ExternalUrl.Endpoint ParseEndpointValue(string value)
		{
			ExternalUrl.Endpoint result;
			EnumUtils.TryGetEnum<ExternalUrl.Endpoint>(value, out result);
			return result;
		}

		// Token: 0x020027E1 RID: 10209
		[Flags]
		public enum AssetFlags
		{
			// Token: 0x0400F6A3 RID: 63139
			[Description("none")]
			NONE = 0,
			// Token: 0x0400F6A4 RID: 63140
			[Description("dev_only")]
			DEV_ONLY = 1,
			// Token: 0x0400F6A5 RID: 63141
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 2,
			// Token: 0x0400F6A6 RID: 63142
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 4
		}

		// Token: 0x020027E2 RID: 10210
		public enum Endpoint
		{
			// Token: 0x0400F6A8 RID: 63144
			ACCOUNT,
			// Token: 0x0400F6A9 RID: 63145
			ALERT,
			// Token: 0x0400F6AA RID: 63146
			CREATION,
			// Token: 0x0400F6AB RID: 63147
			LANDING,
			// Token: 0x0400F6AC RID: 63148
			FIRESIDE_GATHERINGS,
			// Token: 0x0400F6AD RID: 63149
			ACCOUNT_HEALUP,
			// Token: 0x0400F6AE RID: 63150
			PRIVACY_POLICY,
			// Token: 0x0400F6AF RID: 63151
			EULA,
			// Token: 0x0400F6B0 RID: 63152
			DATA_MANAGEMENT,
			// Token: 0x0400F6B1 RID: 63153
			SET_ROTATION,
			// Token: 0x0400F6B2 RID: 63154
			SYSTEM_REQUIREMENTS,
			// Token: 0x0400F6B3 RID: 63155
			RECRUIT_A_FRIEND,
			// Token: 0x0400F6B4 RID: 63156
			TERMS_OF_SALES,
			// Token: 0x0400F6B5 RID: 63157
			PURCHASE,
			// Token: 0x0400F6B6 RID: 63158
			ADD_PAYMENT,
			// Token: 0x0400F6B7 RID: 63159
			CVV,
			// Token: 0x0400F6B8 RID: 63160
			DUPLICATE_PURCHASE_ERROR,
			// Token: 0x0400F6B9 RID: 63161
			DUPLICATE_THIRDPARTY_PURCHASE,
			// Token: 0x0400F6BA RID: 63162
			OUTSTANDING_PURCHASE,
			// Token: 0x0400F6BB RID: 63163
			HEARTHSTONE_ON_IPAD,
			// Token: 0x0400F6BC RID: 63164
			PASSWORD_RESET,
			// Token: 0x0400F6BD RID: 63165
			CHECKOUT,
			// Token: 0x0400F6BE RID: 63166
			CHECKOUT_NAVBAR,
			// Token: 0x0400F6BF RID: 63167
			PAYMENT_INFO,
			// Token: 0x0400F6C0 RID: 63168
			GENERIC_PURCHASE_ERROR,
			// Token: 0x0400F6C1 RID: 63169
			CUSTOMER_SUPPORT
		}
	}
}
