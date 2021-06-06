using System;
using Blizzard.Commerce;

// Token: 0x0200000D RID: 13
public class BrowserListener : IBrowserListener
{
	// Token: 0x0600003E RID: 62 RVA: 0x00002C0D File Offset: 0x00000E0D
	public BrowserListener(IBrowserListener browserListener)
	{
		this.m_browserListener = browserListener;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002C1C File Offset: 0x00000E1C
	public void OnReady()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnReady();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002C31 File Offset: 0x00000E31
	public void OnDisconnect()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnDisconnect();
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002C46 File Offset: 0x00000E46
	public void OnCancel()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnCancel();
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002C5B File Offset: 0x00000E5B
	public void OnWindowCloseRequested()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnWindowCloseRequested();
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002C70 File Offset: 0x00000E70
	public void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent)
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnExternalLink(externalLinkEvent);
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002C86 File Offset: 0x00000E86
	public void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent)
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnWindowResized(windowResizeEvent);
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002C9C File Offset: 0x00000E9C
	public void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t buffer)
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnBufferUpdate(buffer);
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002CB2 File Offset: 0x00000EB2
	public void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent)
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnWindowResizeRequested(windowResizeRequestedEvent);
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00002CC8 File Offset: 0x00000EC8
	public void OnCursorChangeRequest()
	{
		if (this.m_browserListener != null)
		{
			this.m_browserListener.OnCursorChangeRequest();
		}
	}

	// Token: 0x04000011 RID: 17
	public IBrowserListener m_browserListener;
}
