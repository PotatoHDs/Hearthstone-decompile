using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100F RID: 4111
	public interface IAsyncInitializationBehavior
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600B2D9 RID: 45785
		bool IsReady { get; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600B2DA RID: 45786
		bool IsActive { get; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600B2DB RID: 45787
		Behaviour Container { get; }

		// Token: 0x0600B2DC RID: 45788
		void RegisterActivatedListener(Action<object> listener, object payload = null);

		// Token: 0x0600B2DD RID: 45789
		void RegisterDeactivatedListener(Action<object> listener, object payload = null);

		// Token: 0x0600B2DE RID: 45790
		void RegisterReadyListener(Action<object> listener, object payload = null, bool callImmediatelyIfReady = true);

		// Token: 0x0600B2DF RID: 45791
		void RemoveReadyListener(Action<object> listener);
	}
}
