using System;
using System.ComponentModel;

namespace Assets
{
	// Token: 0x02000FC2 RID: 4034
	public static class DeckRuleset
	{
		// Token: 0x0600B012 RID: 45074 RVA: 0x00366C6C File Offset: 0x00364E6C
		public static DeckRuleset.AssetFlags ParseAssetFlagsValue(string value)
		{
			DeckRuleset.AssetFlags result;
			EnumUtils.TryGetEnum<DeckRuleset.AssetFlags>(value, out result);
			return result;
		}

		// Token: 0x020027DD RID: 10205
		[Flags]
		public enum AssetFlags
		{
			// Token: 0x0400F67D RID: 63101
			[Description("none")]
			NONE = 0,
			// Token: 0x0400F67E RID: 63102
			[Description("dev_only")]
			DEV_ONLY = 1,
			// Token: 0x0400F67F RID: 63103
			[Description("not_packaged_in_client")]
			NOT_PACKAGED_IN_CLIENT = 2,
			// Token: 0x0400F680 RID: 63104
			[Description("force_do_not_localize")]
			FORCE_DO_NOT_LOCALIZE = 4
		}
	}
}
