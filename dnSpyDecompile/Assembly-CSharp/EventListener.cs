using System;

// Token: 0x020009B8 RID: 2488
public class EventListener<Delegate>
{
	// Token: 0x0600872C RID: 34604 RVA: 0x002BA268 File Offset: 0x002B8468
	public override bool Equals(object obj)
	{
		EventListener<Delegate> eventListener = obj as EventListener<Delegate>;
		if (eventListener == null)
		{
			return base.Equals(obj);
		}
		return this.m_callback.Equals(eventListener.m_callback) && this.m_userData == eventListener.m_userData;
	}

	// Token: 0x0600872D RID: 34605 RVA: 0x002BA2B8 File Offset: 0x002B84B8
	public override int GetHashCode()
	{
		int num = 23;
		if (this.m_callback != null)
		{
			num = num * 17 + this.m_callback.GetHashCode();
		}
		if (this.m_userData != null)
		{
			num = num * 17 + this.m_userData.GetHashCode();
		}
		return num;
	}

	// Token: 0x0600872E RID: 34606 RVA: 0x000052CE File Offset: 0x000034CE
	public EventListener()
	{
	}

	// Token: 0x0600872F RID: 34607 RVA: 0x002BA306 File Offset: 0x002B8506
	public EventListener(Delegate callback, object userData)
	{
		this.m_callback = callback;
		this.m_userData = userData;
	}

	// Token: 0x06008730 RID: 34608 RVA: 0x002BA31C File Offset: 0x002B851C
	public Delegate GetCallback()
	{
		return this.m_callback;
	}

	// Token: 0x06008731 RID: 34609 RVA: 0x002BA324 File Offset: 0x002B8524
	public void SetCallback(Delegate callback)
	{
		this.m_callback = callback;
	}

	// Token: 0x06008732 RID: 34610 RVA: 0x002BA32D File Offset: 0x002B852D
	public object GetUserData()
	{
		return this.m_userData;
	}

	// Token: 0x06008733 RID: 34611 RVA: 0x002BA335 File Offset: 0x002B8535
	public void SetUserData(object userData)
	{
		this.m_userData = userData;
	}

	// Token: 0x0400723D RID: 29245
	protected Delegate m_callback;

	// Token: 0x0400723E RID: 29246
	protected object m_userData;
}
