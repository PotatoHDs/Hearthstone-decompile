using Blizzard.Commerce;

public class BrowserListener : IBrowserListener
{
	public IBrowserListener m_browserListener;

	public BrowserListener(IBrowserListener browserListener)
	{
		m_browserListener = browserListener;
	}

	public void OnReady()
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnReady();
		}
	}

	public void OnDisconnect()
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnDisconnect();
		}
	}

	public void OnCancel()
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnCancel();
		}
	}

	public void OnWindowCloseRequested()
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnWindowCloseRequested();
		}
	}

	public void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent)
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnExternalLink(externalLinkEvent);
		}
	}

	public void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent)
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnWindowResized(windowResizeEvent);
		}
	}

	public void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t buffer)
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnBufferUpdate(buffer);
		}
	}

	public void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent)
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnWindowResizeRequested(windowResizeRequestedEvent);
		}
	}

	public void OnCursorChangeRequest()
	{
		if (m_browserListener != null)
		{
			m_browserListener.OnCursorChangeRequest();
		}
	}
}
