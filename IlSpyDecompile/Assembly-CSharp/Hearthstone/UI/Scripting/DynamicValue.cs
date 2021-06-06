using System;

namespace Hearthstone.UI.Scripting
{
	public struct DynamicValue
	{
		public readonly object Value;

		public readonly Type ValueType;

		public bool HasValidValue => ValueType != null;

		public DynamicValue(object value, Type valueType)
		{
			Value = value;
			ValueType = ((valueType == null && value != null) ? value.GetType() : valueType);
		}
	}
}
