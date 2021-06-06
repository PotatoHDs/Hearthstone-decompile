using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02001012 RID: 4114
	public interface IDynamicPropertyResolver
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600B2E5 RID: 45797
		ICollection<DynamicPropertyInfo> DynamicProperties { get; }

		// Token: 0x0600B2E6 RID: 45798
		bool GetDynamicPropertyValue(string id, out object value);

		// Token: 0x0600B2E7 RID: 45799
		bool SetDynamicPropertyValue(string id, object value);
	}
}
