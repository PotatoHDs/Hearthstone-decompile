public class UIEvent
{
	public delegate void Handler(UIEvent e);

	private UIEventType m_eventType;

	private PegUIElement m_element;

	public UIEvent(UIEventType eventType, PegUIElement element)
	{
		m_eventType = eventType;
		m_element = element;
	}

	public UIEventType GetEventType()
	{
		return m_eventType;
	}

	public PegUIElement GetElement()
	{
		return m_element;
	}
}
