using System;

// Token: 0x02000AE7 RID: 2791
public class UIEvent
{
	// Token: 0x0600948D RID: 38029 RVA: 0x00302993 File Offset: 0x00300B93
	public UIEvent(UIEventType eventType, PegUIElement element)
	{
		this.m_eventType = eventType;
		this.m_element = element;
	}

	// Token: 0x0600948E RID: 38030 RVA: 0x003029A9 File Offset: 0x00300BA9
	public UIEventType GetEventType()
	{
		return this.m_eventType;
	}

	// Token: 0x0600948F RID: 38031 RVA: 0x003029B1 File Offset: 0x00300BB1
	public PegUIElement GetElement()
	{
		return this.m_element;
	}

	// Token: 0x04007C8D RID: 31885
	private UIEventType m_eventType;

	// Token: 0x04007C8E RID: 31886
	private PegUIElement m_element;

	// Token: 0x0200271E RID: 10014
	// (Invoke) Token: 0x060138E8 RID: 80104
	public delegate void Handler(UIEvent e);
}
