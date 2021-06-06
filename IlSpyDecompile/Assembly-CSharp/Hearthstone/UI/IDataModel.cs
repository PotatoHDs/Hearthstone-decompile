using System;

namespace Hearthstone.UI
{
	public interface IDataModel : IDataModelProperties
	{
		int DataModelId { get; }

		string DataModelDisplayName { get; }

		void RegisterChangedListener(Action<object> listener, object payload = null);

		void RemoveChangedListener(Action<object> listener);
	}
}
