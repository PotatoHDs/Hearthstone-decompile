using System;
using System.Collections.Generic;

// Token: 0x020008D9 RID: 2265
public class CheckoutEvent<T>
{
	// Token: 0x06007D7D RID: 32125 RVA: 0x0028BC24 File Offset: 0x00289E24
	public void Fire(T obj)
	{
		if (this.m_listeners != null)
		{
			foreach (Action<T> action in this.m_listeners)
			{
				if (action != null)
				{
					action(obj);
				}
			}
		}
	}

	// Token: 0x06007D7E RID: 32126 RVA: 0x0028BC84 File Offset: 0x00289E84
	public void AddListener(Action<T> listener)
	{
		if (this.m_listeners == null)
		{
			this.m_listeners = new List<Action<T>>();
		}
		if (!this.m_listeners.Contains(listener))
		{
			this.m_listeners.Add(listener);
		}
	}

	// Token: 0x06007D7F RID: 32127 RVA: 0x0028BCB3 File Offset: 0x00289EB3
	public void RemoveListener(Action<T> listener)
	{
		if (this.m_listeners != null)
		{
			this.m_listeners.Remove(listener);
		}
	}

	// Token: 0x040065BD RID: 26045
	private List<Action<T>> m_listeners;
}
