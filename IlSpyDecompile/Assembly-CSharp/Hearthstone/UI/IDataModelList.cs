using System;
using System.Collections;

namespace Hearthstone.UI
{
	public interface IDataModelList : IList, ICollection, IEnumerable, IDataModelProperties
	{
		object GetElementAtIndex(int index);

		void AddDefaultValue();

		void RegisterChangedListener(Action<object> listener, object payload = null);

		void RemoveChangedListener(Action<object> listener);

		void DontUpdateDataVersionOnChange();
	}
}
