using System;
using UnityEngine;

namespace Hearthstone.UI.Tests
{
	// Token: 0x02001035 RID: 4149
	[AddComponentMenu("")]
	[ExecuteAlways]
	public class UIFrameworkTests_AsyncTarget : MonoBehaviour, IAsyncInitializationBehavior
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600B466 RID: 46182 RVA: 0x00377E47 File Offset: 0x00376047
		// (set) Token: 0x0600B467 RID: 46183 RVA: 0x00377E4F File Offset: 0x0037604F
		public bool IsReady { get; private set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600B468 RID: 46184 RVA: 0x00377E58 File Offset: 0x00376058
		// (set) Token: 0x0600B469 RID: 46185 RVA: 0x00377E60 File Offset: 0x00376060
		public bool IsActive { get; private set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600B46A RID: 46186 RVA: 0x00377E69 File Offset: 0x00376069
		// (set) Token: 0x0600B46B RID: 46187 RVA: 0x00377E71 File Offset: 0x00376071
		public Behaviour Container { get; private set; }

		// Token: 0x0600B46C RID: 46188 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
		}

		// Token: 0x0600B46D RID: 46189 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
		}

		// Token: 0x0600B46E RID: 46190 RVA: 0x00377E7A File Offset: 0x0037607A
		public void RegisterReadyListener(Action<object> listener, object payload, bool callImmediatelyIfReady = true)
		{
			this.m_callback = listener;
		}

		// Token: 0x0600B46F RID: 46191 RVA: 0x00377E83 File Offset: 0x00376083
		public void RemoveReadyListener(Action<object> listener)
		{
			this.m_callback = null;
		}

		// Token: 0x0600B470 RID: 46192 RVA: 0x00377E8C File Offset: 0x0037608C
		public void BecomeReady()
		{
			this.IsReady = true;
			if (this.m_callback != null)
			{
				this.m_callback(null);
			}
		}

		// Token: 0x040096DD RID: 38621
		private Action<object> m_callback;
	}
}
