using System;

// Token: 0x02000AE8 RID: 2792
public class UIReleaseAllEvent : UIEvent
{
	// Token: 0x06009490 RID: 38032 RVA: 0x003029B9 File Offset: 0x00300BB9
	public UIReleaseAllEvent(bool mouseIsOver, PegUIElement element) : base(UIEventType.RELEASEALL, element)
	{
		this.m_mouseIsOver = mouseIsOver;
	}

	// Token: 0x06009491 RID: 38033 RVA: 0x003029CA File Offset: 0x00300BCA
	public bool GetMouseIsOver()
	{
		return this.m_mouseIsOver;
	}

	// Token: 0x04007C8F RID: 31887
	private bool m_mouseIsOver;
}
