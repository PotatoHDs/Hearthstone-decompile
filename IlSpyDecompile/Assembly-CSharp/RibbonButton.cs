using UnityEngine;

public class RibbonButton : PegUIElement
{
	public GameObject m_highlight;

	public void Start()
	{
		AddEventListener(UIEventType.ROLLOVER, OnButtonOver);
		AddEventListener(UIEventType.ROLLOUT, OnButtonOut);
	}

	public void OnButtonOver(UIEvent e)
	{
		if (m_highlight != null)
		{
			m_highlight.SetActive(value: true);
		}
	}

	public void OnButtonOut(UIEvent e)
	{
		if (m_highlight != null)
		{
			m_highlight.SetActive(value: false);
		}
	}
}
