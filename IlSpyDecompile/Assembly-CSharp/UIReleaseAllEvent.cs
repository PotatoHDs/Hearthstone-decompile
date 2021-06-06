public class UIReleaseAllEvent : UIEvent
{
	private bool m_mouseIsOver;

	public UIReleaseAllEvent(bool mouseIsOver, PegUIElement element)
		: base(UIEventType.RELEASEALL, element)
	{
		m_mouseIsOver = mouseIsOver;
	}

	public bool GetMouseIsOver()
	{
		return m_mouseIsOver;
	}
}
