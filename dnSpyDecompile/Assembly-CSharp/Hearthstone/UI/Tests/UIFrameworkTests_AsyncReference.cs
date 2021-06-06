using System;
using UnityEngine;

namespace Hearthstone.UI.Tests
{
	// Token: 0x02001034 RID: 4148
	[AddComponentMenu("")]
	public class UIFrameworkTests_AsyncReference : MonoBehaviour
	{
		// Token: 0x040096D7 RID: 38615
		public AsyncReference ReferenceToAsyncBehavior;

		// Token: 0x040096D8 RID: 38616
		public AsyncReference ReferenceToRegularBehavior;

		// Token: 0x040096D9 RID: 38617
		public AsyncReference MissingReference;
	}
}
