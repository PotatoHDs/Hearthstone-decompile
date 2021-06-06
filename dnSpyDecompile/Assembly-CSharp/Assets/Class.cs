using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC1 RID: 4033
	public static class Class
	{
		// Token: 0x0600B011 RID: 45073 RVA: 0x00366C54 File Offset: 0x00364E54
		public static Class.AssetFlags ParseAssetFlagsValue(string value)
		{
			Class.AssetFlags result;
			EnumUtils.TryGetEnum<Class.AssetFlags>(value, out result);
			return result;
		}

		// Token: 0x020027DC RID: 10204
		[Flags]
		public enum AssetFlags
		{
			// Token: 0x0400F678 RID: 63096
			[Description("none")]
			NONE = 0,
			// Token: 0x0400F679 RID: 63097
			[Description("dev_only")]
			DEV_ONLY = 1,
			// Token: 0x0400F67A RID: 63098
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 2,
			// Token: 0x0400F67B RID: 63099
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 4
		}
	}
}
