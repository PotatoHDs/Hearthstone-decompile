using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FEA RID: 4074
	public interface IDataModelProperties
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x0600B14C RID: 45388
		DataModelProperty[] Properties { get; }

		// Token: 0x0600B14D RID: 45389
		bool GetPropertyValue(int id, out object value);

		// Token: 0x0600B14E RID: 45390
		bool SetPropertyValue(int id, object value);

		// Token: 0x0600B14F RID: 45391
		bool GetPropertyInfo(int id, out DataModelProperty info);

		// Token: 0x0600B150 RID: 45392
		int GetPropertiesHashCode();
	}
}
