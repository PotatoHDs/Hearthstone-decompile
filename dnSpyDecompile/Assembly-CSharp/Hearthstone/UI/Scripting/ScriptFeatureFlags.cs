using System;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104F RID: 4175
	[Flags]
	public enum ScriptFeatureFlags
	{
		// Token: 0x040096FC RID: 38652
		Conditionals = 1,
		// Token: 0x040096FD RID: 38653
		Identifiers = 2,
		// Token: 0x040096FE RID: 38654
		Keywords = 4,
		// Token: 0x040096FF RID: 38655
		Arithmetic = 8,
		// Token: 0x04009700 RID: 38656
		Relational = 16,
		// Token: 0x04009701 RID: 38657
		Constants = 32,
		// Token: 0x04009702 RID: 38658
		Events = 64,
		// Token: 0x04009703 RID: 38659
		Tuples = 128,
		// Token: 0x04009704 RID: 38660
		Methods = 256,
		// Token: 0x04009705 RID: 38661
		All = 511
	}
}
