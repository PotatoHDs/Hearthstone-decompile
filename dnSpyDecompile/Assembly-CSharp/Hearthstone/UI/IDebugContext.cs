using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02001011 RID: 4113
	public interface IDebugContext
	{
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600B2E1 RID: 45793
		string DebugPath { get; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x0600B2E3 RID: 45795
		// (set) Token: 0x0600B2E2 RID: 45794
		IDebugContext ParentContext { get; set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600B2E4 RID: 45796
		ICollection<IDebugContext> ChildContexts { get; }
	}
}
