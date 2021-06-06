using System;
using System.Collections.Generic;

// Token: 0x020008D8 RID: 2264
public class CheckoutEvent
{
	// Token: 0x06007D79 RID: 32121 RVA: 0x0028BB7C File Offset: 0x00289D7C
	public void Fire()
	{
		if (this.m_listeners != null)
		{
			foreach (Action action in this.m_listeners)
			{
				if (action != null)
				{
					action();
				}
			}
		}
	}

	// Token: 0x06007D7A RID: 32122 RVA: 0x0028BBDC File Offset: 0x00289DDC
	public void AddListener(Action listener)
	{
		if (this.m_listeners == null)
		{
			this.m_listeners = new List<Action>();
		}
		if (!this.m_listeners.Contains(listener))
		{
			this.m_listeners.Add(listener);
		}
	}

	// Token: 0x06007D7B RID: 32123 RVA: 0x0028BC0B File Offset: 0x00289E0B
	public void RemoveListener(Action listener)
	{
		if (this.m_listeners != null)
		{
			this.m_listeners.Remove(listener);
		}
	}

	// Token: 0x040065BC RID: 26044
	private List<Action> m_listeners;
}
