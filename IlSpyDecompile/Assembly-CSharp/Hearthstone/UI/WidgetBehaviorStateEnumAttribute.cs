using System;
using UnityEngine;

namespace Hearthstone.UI
{
	public class WidgetBehaviorStateEnumAttribute : PropertyAttribute
	{
		public Type StateEnum;

		public string PropertyName;

		public WidgetBehaviorStateEnumAttribute(Type stateEnum, string propertyName = "")
		{
			StateEnum = stateEnum;
			PropertyName = propertyName;
		}
	}
}
