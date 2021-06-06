using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	// Token: 0x02000FE8 RID: 4072
	public struct DataModelProperty
	{
		// Token: 0x04009599 RID: 38297
		public int PropertyId;

		// Token: 0x0400959A RID: 38298
		public string PropertyDisplayName;

		// Token: 0x0400959B RID: 38299
		public Type Type;

		// Token: 0x0400959C RID: 38300
		public DataModelProperty.QueryDelegate QueryMethod;

		// Token: 0x0200281E RID: 10270
		// (Invoke) Token: 0x06013B08 RID: 80648
		public delegate object QueryDelegate(IEnumerable<object> matchingElements);
	}
}
