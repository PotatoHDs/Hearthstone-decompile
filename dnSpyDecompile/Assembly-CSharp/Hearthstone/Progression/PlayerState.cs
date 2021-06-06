using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.Progression
{
	// Token: 0x0200110F RID: 4367
	public class PlayerState<T> : IEnumerable<!0>, IEnumerable where T : class
	{
		// Token: 0x140000CC RID: 204
		// (add) Token: 0x0600BF47 RID: 48967 RVA: 0x003A44C0 File Offset: 0x003A26C0
		// (remove) Token: 0x0600BF48 RID: 48968 RVA: 0x003A44F8 File Offset: 0x003A26F8
		public event Action<T, T> OnStateChanged = delegate(T oldState, T newState)
		{
		};

		// Token: 0x0600BF49 RID: 48969 RVA: 0x003A452D File Offset: 0x003A272D
		public PlayerState(Func<int, T> defaultProvider = null)
		{
			this.m_defaultProvider = defaultProvider;
		}

		// Token: 0x0600BF4A RID: 48970 RVA: 0x003A456C File Offset: 0x003A276C
		public T GetState(int id)
		{
			T result;
			if (this.m_playerState.TryGetValue(id, out result))
			{
				return result;
			}
			Func<int, T> defaultProvider = this.m_defaultProvider;
			if (defaultProvider == null)
			{
				return default(T);
			}
			return defaultProvider(id);
		}

		// Token: 0x0600BF4B RID: 48971 RVA: 0x003A45A8 File Offset: 0x003A27A8
		public void UpdateState(int id, T newState)
		{
			T state = this.GetState(id);
			this.m_playerState[id] = newState;
			this.OnStateChanged(state, newState);
		}

		// Token: 0x0600BF4C RID: 48972 RVA: 0x003A45D7 File Offset: 0x003A27D7
		public void Reset()
		{
			this.m_playerState.Clear();
		}

		// Token: 0x0600BF4D RID: 48973 RVA: 0x003A45E4 File Offset: 0x003A27E4
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.m_playerState.Values.GetEnumerator();
		}

		// Token: 0x0600BF4E RID: 48974 RVA: 0x003A45E4 File Offset: 0x003A27E4
		public IEnumerator GetEnumerator()
		{
			return this.m_playerState.Values.GetEnumerator();
		}

		// Token: 0x04009B5F RID: 39775
		private readonly Map<int, T> m_playerState = new Map<int, T>();

		// Token: 0x04009B60 RID: 39776
		private readonly Func<int, T> m_defaultProvider;
	}
}
