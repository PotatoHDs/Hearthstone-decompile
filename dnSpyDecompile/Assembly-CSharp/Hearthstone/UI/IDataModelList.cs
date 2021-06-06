using System;
using System.Collections;

namespace Hearthstone.UI
{
	// Token: 0x02000FE5 RID: 4069
	public interface IDataModelList : IList, ICollection, IEnumerable, IDataModelProperties
	{
		// Token: 0x0600B119 RID: 45337
		object GetElementAtIndex(int index);

		// Token: 0x0600B11A RID: 45338
		void AddDefaultValue();

		// Token: 0x0600B11B RID: 45339
		void RegisterChangedListener(Action<object> listener, object payload = null);

		// Token: 0x0600B11C RID: 45340
		void RemoveChangedListener(Action<object> listener);

		// Token: 0x0600B11D RID: 45341
		void DontUpdateDataVersionOnChange();
	}
}
