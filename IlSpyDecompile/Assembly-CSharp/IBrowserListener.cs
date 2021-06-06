using Blizzard.Commerce;

public interface IBrowserListener
{
	void OnReady();

	void OnDisconnect();

	void OnCancel();

	void OnWindowResized(blz_commerce_scene_window_resize_event_t windowResizeEvent);

	void OnBufferUpdate(blz_commerce_scene_buffer_update_event_t buffer);

	void OnWindowResizeRequested(blz_commerce_scene_window_resize_requested_event_t windowResizeRequestedEvent);

	void OnWindowCloseRequested();

	void OnCursorChangeRequest();

	void OnExternalLink(blz_commerce_scene_external_link_event_t externalLinkEvent);
}
