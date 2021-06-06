using System;
using System.ComponentModel;

namespace bgs
{
	// Token: 0x0200026C RID: 620
	public enum LogLevel
	{
		// Token: 0x04000FB0 RID: 4016
		[Description("None")]
		None,
		// Token: 0x04000FB1 RID: 4017
		[Description("Debug")]
		Debug,
		// Token: 0x04000FB2 RID: 4018
		[Description("Info")]
		Info,
		// Token: 0x04000FB3 RID: 4019
		[Description("Warning")]
		Warning,
		// Token: 0x04000FB4 RID: 4020
		[Description("Error")]
		Error,
		// Token: 0x04000FB5 RID: 4021
		[Description("Exception")]
		Exception,
		// Token: 0x04000FB6 RID: 4022
		[Description("Fatal")]
		Fatal
	}
}
