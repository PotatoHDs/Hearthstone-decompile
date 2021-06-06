using System;
using Blizzard.Commerce;

// Token: 0x02000009 RID: 9
public interface IBrowserListener
{
	// Token: 0x0600002D RID: 45
	void OnReady();

	// Token: 0x0600002E RID: 46
	void OnDisconnect();

	// Token: 0x0600002F RID: 47
	void OnCancel();

	// Token: 0x06000030 RID: 48
	void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent);

	// Token: 0x06000031 RID: 49
	void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t buffer);

	// Token: 0x06000032 RID: 50
	void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent);

	// Token: 0x06000033 RID: 51
	void OnWindowCloseRequested();

	// Token: 0x06000034 RID: 52
	void OnCursorChangeRequest();

	// Token: 0x06000035 RID: 53
	void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent);
}
