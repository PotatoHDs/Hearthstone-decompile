using System;
using System.ComponentModel;

namespace Assets
{
	public static class Class
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

		public static AssetFlags ParseAssetFlagsValue(string value)
		{
			EnumUtils.TryGetEnum<AssetFlags>(value, out var outVal);
			return outVal;
		}
	}
}
