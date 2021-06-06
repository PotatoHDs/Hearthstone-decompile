using System;

namespace Hearthstone.UI.Internal
{
	[AttributeUsage(AttributeTargets.Class)]
	public class NestedReferenceScopeAttribute : Attribute
	{
		public readonly NestedReference.Scope Scope;

		public NestedReferenceScopeAttribute(NestedReference.Scope scope)
		{
			Scope = scope;
		}
	}
}
