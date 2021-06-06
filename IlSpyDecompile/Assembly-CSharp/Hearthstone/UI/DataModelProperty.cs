using System;
using System.Collections.Generic;

namespace Hearthstone.UI
{
	public struct DataModelProperty
	{
		public delegate object QueryDelegate(IEnumerable<object> matchingElements);

		public int PropertyId;

		public string PropertyDisplayName;

		public Type Type;

		public QueryDelegate QueryMethod;
	}
}
