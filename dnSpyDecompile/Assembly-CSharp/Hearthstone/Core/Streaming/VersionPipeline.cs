using System;
using System.ComponentModel;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001094 RID: 4244
	public enum VersionPipeline
	{
		// Token: 0x04009847 RID: 38983
		[Description("Unknown")]
		UNKNOWN,
		// Token: 0x04009848 RID: 38984
		[Description("Dev")]
		DEV,
		// Token: 0x04009849 RID: 38985
		[Description("External")]
		EXTERNAL,
		// Token: 0x0400984A RID: 38986
		[Description("RC")]
		RC,
		// Token: 0x0400984B RID: 38987
		[Description("Live")]
		LIVE
	}
}
