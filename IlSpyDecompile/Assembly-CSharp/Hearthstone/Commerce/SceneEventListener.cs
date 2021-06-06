using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	public class SceneEventListener
	{
		public delegate void BufferUpdateDelegate(blz_commerce_scene_buffer_update_event_t bufferUpdate);

		public delegate void CommerceWindowResizeDelegate(blz_commerce_scene_window_resize_event_t windowResize);

		public delegate void CommerceExternalLinkDelegate(blz_commerce_scene_external_link_event_t externalLink);

		public delegate void CommerceWindowResizeRequestedDelegate(blz_commerce_scene_window_resize_requested_event_t windowResizeRequest);

		public delegate void ImeCompositionRangeChangedDelegate(scene_ime_composition_range_changed_event_t rangeChanged);

		public delegate void ImeStateChangedDelegate(scene_ime_state_changed_event_t imeStateChange);

		public delegate void ImeTextSelectionChangedDelegate(scene_ime_text_selection_changed_event_t imeTextSelectionChanged);

		public delegate void ImeSelectionBoundsChangedDelegate(scene_ime_selection_bounds_changed_event_t imeSelectionBoundsChanged);

		public event CommerceWindowResizeDelegate WindowResize;

		public event BufferUpdateDelegate BufferUpdate;

		public event Action Cancel;

		public event Action CursorChanged;

		public event Action Disconnect;

		public event CommerceExternalLinkDelegate ExternalLink;

		public event Action Ready;

		public event Action WindowCloseRequest;

		public event CommerceWindowResizeRequestedDelegate WindowResizeRequested;

		public event ImeCompositionRangeChangedDelegate ImeCompsoitionRangeChanged;

		public event ImeStateChangedDelegate ImeStateChanged;

		public event Action ImeCompositionCanceled;

		public event ImeTextSelectionChangedDelegate ImeTextSelectionChanged;

		public event ImeSelectionBoundsChangedDelegate ImeTextBoundsChanged;

		public void ReceivedEvent(blz_commerce_scene_event_t sceneEvent)
		{
			switch (sceneEvent.scene_type)
			{
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_BUFFER_UPDATE:
				this.BufferUpdate?.Invoke(new blz_commerce_scene_buffer_update_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_EXTERNAL_LINK:
				this.ExternalLink?.Invoke(new blz_commerce_scene_external_link_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_CLOSE_REQUEST:
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CLOSE_REQUESTED:
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CLOSE:
				this.WindowCloseRequest?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_READY:
				this.Ready?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_DISCONNECT:
				this.Disconnect?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CANCEL:
				this.Cancel?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_RESIZE:
				this.WindowResize?.Invoke(new blz_commerce_scene_window_resize_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CURSOR_CHANGE:
				this.CursorChanged?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_RESIZE_REQUESTED:
				this.WindowResizeRequested?.Invoke(new blz_commerce_scene_window_resize_requested_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_COMPOSITION_RANGE_CHANGED:
				this.ImeCompsoitionRangeChanged?.Invoke(new scene_ime_composition_range_changed_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_STATE_CHANGED:
				this.ImeStateChanged?.Invoke(new scene_ime_state_changed_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_COMPOSITION_CANCELED:
				this.ImeCompositionCanceled?.Invoke();
				break;
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_TEXT_SELECTION_CHANGED:
				this.ImeTextSelectionChanged?.Invoke(new scene_ime_text_selection_changed_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_TEXT_BOUNDS_CHANGED:
				this.ImeTextBoundsChanged?.Invoke(new scene_ime_selection_bounds_changed_event_t(sceneEvent.scene_data, cMemoryOwn: false));
				break;
			default:
				Log.Store.PrintError("Received an unrecognized Scene Event from Commerce! ({0})", sceneEvent.scene_type.ToString());
				break;
			}
		}
	}
}
