using System.Collections.Generic;

namespace Hearthstone.UI
{
	public interface IDynamicPropertyResolver
	{
		ICollection<DynamicPropertyInfo> DynamicProperties { get; }

		bool GetDynamicPropertyValue(string id, out object value);

		bool SetDynamicPropertyValue(string id, object value);
	}
}
