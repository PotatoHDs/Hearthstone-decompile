using System;

namespace Hearthstone.UI.Internal
{
	// Token: 0x02001056 RID: 4182
	[AttributeUsage(AttributeTargets.Class)]
	public class NestedReferenceScopeAttribute : Attribute
	{
		// Token: 0x0600B526 RID: 46374 RVA: 0x0037B896 File Offset: 0x00379A96
		public NestedReferenceScopeAttribute(NestedReference.Scope scope)
		{
			this.Scope = scope;
		}

		// Token: 0x04009723 RID: 38691
		public readonly NestedReference.Scope Scope;
	}
}
