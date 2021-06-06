using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FEF RID: 4079
	public interface INestedReferenceResolver : IAsyncInitializationBehavior
	{
		// Token: 0x0600B165 RID: 45413
		Component GetComponentById(long id);

		// Token: 0x0600B166 RID: 45414
		bool GetComponentId(Component component, out long id);
	}
}
