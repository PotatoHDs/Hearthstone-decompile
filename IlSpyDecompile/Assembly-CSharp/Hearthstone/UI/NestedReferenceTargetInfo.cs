using System.Reflection;
using UnityEngine;

namespace Hearthstone.UI
{
	public struct NestedReferenceTargetInfo
	{
		public Object Target;

		public string Path;

		public PropertyInfo Property;
	}
}
