using System;
using UnityEngine;

namespace Hearthstone.UI
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class WidgetBehaviorDescriptionAttribute : PropertyAttribute
	{
		public string Path;

		public string UniqueWithinCategory = "";
	}
}
