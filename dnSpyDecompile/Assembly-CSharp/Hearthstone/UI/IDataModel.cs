using System;

namespace Hearthstone.UI
{
	// Token: 0x02000FEB RID: 4075
	public interface IDataModel : IDataModelProperties
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600B151 RID: 45393
		int DataModelId { get; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600B152 RID: 45394
		string DataModelDisplayName { get; }

		// Token: 0x0600B153 RID: 45395
		void RegisterChangedListener(Action<object> listener, object payload = null);

		// Token: 0x0600B154 RID: 45396
		void RemoveChangedListener(Action<object> listener);
	}
}
