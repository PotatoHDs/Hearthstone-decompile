using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Internal
{
	// Token: 0x02001058 RID: 4184
	[Serializable]
	public struct FlagStateTracker
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600B528 RID: 46376 RVA: 0x0037B8A5 File Offset: 0x00379AA5
		// (set) Token: 0x0600B529 RID: 46377 RVA: 0x0037B8AD File Offset: 0x00379AAD
		public bool IsSet { get; private set; }

		// Token: 0x0600B52A RID: 46378 RVA: 0x0037B8B8 File Offset: 0x00379AB8
		public void RegisterSetListener(Action<object> listener, object context = null, bool callImmediatelyIfSet = true, bool doOnce = false)
		{
			if (this.IsSet && callImmediatelyIfSet)
			{
				listener(context);
				if (doOnce)
				{
					return;
				}
			}
			if (this.m_listeners == null)
			{
				this.m_listeners = new List<FlagStateTracker.Listener>();
			}
			int num = -1;
			for (int i = 0; i < this.m_listeners.Count; i++)
			{
				if (this.m_listeners[i].Action == listener)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				FlagStateTracker.Listener value = this.m_listeners[num];
				value.Context = context;
				this.m_listeners[num] = value;
				return;
			}
			this.m_listeners.Add(new FlagStateTracker.Listener
			{
				Action = listener,
				Context = context,
				DoOnce = doOnce
			});
		}

		// Token: 0x0600B52B RID: 46379 RVA: 0x0037B978 File Offset: 0x00379B78
		public void RemoveSetListener(Action<object> listener)
		{
			if (this.m_listeners != null)
			{
				for (int i = 0; i < this.m_listeners.Count; i++)
				{
					if (this.m_listeners[i].Action == listener)
					{
						this.m_listeners.RemoveAt(i);
						return;
					}
				}
			}
		}

		// Token: 0x0600B52C RID: 46380 RVA: 0x0037B9C9 File Offset: 0x00379BC9
		public void RemoveAllSetListeners()
		{
			if (this.m_listeners != null)
			{
				this.m_listeners.Clear();
			}
		}

		// Token: 0x0600B52D RID: 46381 RVA: 0x0037B9E0 File Offset: 0x00379BE0
		public bool SetAndDispatch()
		{
			this.IsSet = true;
			if (this.m_listeners != null)
			{
				for (int i = 0; i < this.m_listeners.Count; i++)
				{
					FlagStateTracker.Listener listener = this.m_listeners[i];
					if (listener.Action != null)
					{
						listener.Action(listener.Context);
						if (listener.DoOnce)
						{
							this.m_listeners.RemoveAt(i);
							i--;
						}
					}
				}
			}
			return this.IsSet;
		}

		// Token: 0x0600B52E RID: 46382 RVA: 0x0037BA56 File Offset: 0x00379C56
		public bool SetWithoutDispatch()
		{
			this.IsSet = true;
			return this.IsSet;
		}

		// Token: 0x0600B52F RID: 46383 RVA: 0x0037BA65 File Offset: 0x00379C65
		public void Clear()
		{
			this.IsSet = false;
		}

		// Token: 0x04009725 RID: 38693
		private List<FlagStateTracker.Listener> m_listeners;

		// Token: 0x02002868 RID: 10344
		[Serializable]
		private struct Listener
		{
			// Token: 0x0400F9A7 RID: 63911
			public Action<object> Action;

			// Token: 0x0400F9A8 RID: 63912
			public object Context;

			// Token: 0x0400F9A9 RID: 63913
			public bool DoOnce;
		}
	}
}
