using System;
using Blizzard.Commerce;

namespace Hearthstone.Commerce
{
	// Token: 0x02001078 RID: 4216
	public class SceneEventListener
	{
		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x0600B616 RID: 46614 RVA: 0x0037DDC4 File Offset: 0x0037BFC4
		// (remove) Token: 0x0600B617 RID: 46615 RVA: 0x0037DDFC File Offset: 0x0037BFFC
		public event SceneEventListener.CommerceWindowResizeDelegate WindowResize;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x0600B618 RID: 46616 RVA: 0x0037DE34 File Offset: 0x0037C034
		// (remove) Token: 0x0600B619 RID: 46617 RVA: 0x0037DE6C File Offset: 0x0037C06C
		public event SceneEventListener.BufferUpdateDelegate BufferUpdate;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x0600B61A RID: 46618 RVA: 0x0037DEA4 File Offset: 0x0037C0A4
		// (remove) Token: 0x0600B61B RID: 46619 RVA: 0x0037DEDC File Offset: 0x0037C0DC
		public event Action Cancel;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x0600B61C RID: 46620 RVA: 0x0037DF14 File Offset: 0x0037C114
		// (remove) Token: 0x0600B61D RID: 46621 RVA: 0x0037DF4C File Offset: 0x0037C14C
		public event Action CursorChanged;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x0600B61E RID: 46622 RVA: 0x0037DF84 File Offset: 0x0037C184
		// (remove) Token: 0x0600B61F RID: 46623 RVA: 0x0037DFBC File Offset: 0x0037C1BC
		public event Action Disconnect;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x0600B620 RID: 46624 RVA: 0x0037DFF4 File Offset: 0x0037C1F4
		// (remove) Token: 0x0600B621 RID: 46625 RVA: 0x0037E02C File Offset: 0x0037C22C
		public event SceneEventListener.CommerceExternalLinkDelegate ExternalLink;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x0600B622 RID: 46626 RVA: 0x0037E064 File Offset: 0x0037C264
		// (remove) Token: 0x0600B623 RID: 46627 RVA: 0x0037E09C File Offset: 0x0037C29C
		public event Action Ready;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x0600B624 RID: 46628 RVA: 0x0037E0D4 File Offset: 0x0037C2D4
		// (remove) Token: 0x0600B625 RID: 46629 RVA: 0x0037E10C File Offset: 0x0037C30C
		public event Action WindowCloseRequest;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x0600B626 RID: 46630 RVA: 0x0037E144 File Offset: 0x0037C344
		// (remove) Token: 0x0600B627 RID: 46631 RVA: 0x0037E17C File Offset: 0x0037C37C
		public event SceneEventListener.CommerceWindowResizeRequestedDelegate WindowResizeRequested;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600B628 RID: 46632 RVA: 0x0037E1B4 File Offset: 0x0037C3B4
		// (remove) Token: 0x0600B629 RID: 46633 RVA: 0x0037E1EC File Offset: 0x0037C3EC
		public event SceneEventListener.ImeCompositionRangeChangedDelegate ImeCompsoitionRangeChanged;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600B62A RID: 46634 RVA: 0x0037E224 File Offset: 0x0037C424
		// (remove) Token: 0x0600B62B RID: 46635 RVA: 0x0037E25C File Offset: 0x0037C45C
		public event SceneEventListener.ImeStateChangedDelegate ImeStateChanged;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x0600B62C RID: 46636 RVA: 0x0037E294 File Offset: 0x0037C494
		// (remove) Token: 0x0600B62D RID: 46637 RVA: 0x0037E2CC File Offset: 0x0037C4CC
		public event Action ImeCompositionCanceled;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x0600B62E RID: 46638 RVA: 0x0037E304 File Offset: 0x0037C504
		// (remove) Token: 0x0600B62F RID: 46639 RVA: 0x0037E33C File Offset: 0x0037C53C
		public event SceneEventListener.ImeTextSelectionChangedDelegate ImeTextSelectionChanged;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x0600B630 RID: 46640 RVA: 0x0037E374 File Offset: 0x0037C574
		// (remove) Token: 0x0600B631 RID: 46641 RVA: 0x0037E3AC File Offset: 0x0037C5AC
		public event SceneEventListener.ImeSelectionBoundsChangedDelegate ImeTextBoundsChanged;

		// Token: 0x0600B632 RID: 46642 RVA: 0x0037E3E4 File Offset: 0x0037C5E4
		public void ReceivedEvent(blz_commerce_scene_event_t sceneEvent)
		{
			switch (sceneEvent.scene_type)
			{
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_BUFFER_UPDATE:
			{
				SceneEventListener.BufferUpdateDelegate bufferUpdate = this.BufferUpdate;
				if (bufferUpdate == null)
				{
					return;
				}
				bufferUpdate(new blz_commerce_scene_buffer_update_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_EXTERNAL_LINK:
			{
				SceneEventListener.CommerceExternalLinkDelegate externalLink = this.ExternalLink;
				if (externalLink == null)
				{
					return;
				}
				externalLink(new blz_commerce_scene_external_link_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_CLOSE_REQUEST:
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CLOSE_REQUESTED:
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CLOSE:
			{
				Action windowCloseRequest = this.WindowCloseRequest;
				if (windowCloseRequest == null)
				{
					return;
				}
				windowCloseRequest();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_READY:
			{
				Action ready = this.Ready;
				if (ready == null)
				{
					return;
				}
				ready();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_DISCONNECT:
			{
				Action disconnect = this.Disconnect;
				if (disconnect == null)
				{
					return;
				}
				disconnect();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CANCEL:
			{
				Action cancel = this.Cancel;
				if (cancel == null)
				{
					return;
				}
				cancel();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_RESIZE:
			{
				SceneEventListener.CommerceWindowResizeDelegate windowResize = this.WindowResize;
				if (windowResize == null)
				{
					return;
				}
				windowResize(new blz_commerce_scene_window_resize_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_CURSOR_CHANGE:
			{
				Action cursorChanged = this.CursorChanged;
				if (cursorChanged == null)
				{
					return;
				}
				cursorChanged();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_COMMERCE_SCENE_WINDOW_RESIZE_REQUESTED:
			{
				SceneEventListener.CommerceWindowResizeRequestedDelegate windowResizeRequested = this.WindowResizeRequested;
				if (windowResizeRequested == null)
				{
					return;
				}
				windowResizeRequested(new blz_commerce_scene_window_resize_requested_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_COMPOSITION_RANGE_CHANGED:
			{
				SceneEventListener.ImeCompositionRangeChangedDelegate imeCompsoitionRangeChanged = this.ImeCompsoitionRangeChanged;
				if (imeCompsoitionRangeChanged == null)
				{
					return;
				}
				imeCompsoitionRangeChanged(new scene_ime_composition_range_changed_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_STATE_CHANGED:
			{
				SceneEventListener.ImeStateChangedDelegate imeStateChanged = this.ImeStateChanged;
				if (imeStateChanged == null)
				{
					return;
				}
				imeStateChanged(new scene_ime_state_changed_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_COMPOSITION_CANCELED:
			{
				Action imeCompositionCanceled = this.ImeCompositionCanceled;
				if (imeCompositionCanceled == null)
				{
					return;
				}
				imeCompositionCanceled();
				return;
			}
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_TEXT_SELECTION_CHANGED:
			{
				SceneEventListener.ImeTextSelectionChangedDelegate imeTextSelectionChanged = this.ImeTextSelectionChanged;
				if (imeTextSelectionChanged == null)
				{
					return;
				}
				imeTextSelectionChanged(new scene_ime_text_selection_changed_event_t(sceneEvent.scene_data, false));
				return;
			}
			case blz_commerce_scene_type_t.BLZ_SCENE_IME_TEXT_BOUNDS_CHANGED:
			{
				SceneEventListener.ImeSelectionBoundsChangedDelegate imeTextBoundsChanged = this.ImeTextBoundsChanged;
				if (imeTextBoundsChanged == null)
				{
					return;
				}
				imeTextBoundsChanged(new scene_ime_selection_bounds_changed_event_t(sceneEvent.scene_data, false));
				return;
			}
			default:
				Log.Store.PrintError("Received an unrecognized Scene Event from Commerce! ({0})", new object[]
				{
					sceneEvent.scene_type.ToString()
				});
				return;
			}
		}

		// Token: 0x02002878 RID: 10360
		// (Invoke) Token: 0x06013BF9 RID: 80889
		public delegate void BufferUpdateDelegate(blz_commerce_scene_buffer_update_event_t bufferUpdate);

		// Token: 0x02002879 RID: 10361
		// (Invoke) Token: 0x06013BFD RID: 80893
		public delegate void CommerceWindowResizeDelegate(blz_commerce_scene_window_resize_event_t windowResize);

		// Token: 0x0200287A RID: 10362
		// (Invoke) Token: 0x06013C01 RID: 80897
		public delegate void CommerceExternalLinkDelegate(blz_commerce_scene_external_link_event_t externalLink);

		// Token: 0x0200287B RID: 10363
		// (Invoke) Token: 0x06013C05 RID: 80901
		public delegate void CommerceWindowResizeRequestedDelegate(blz_commerce_scene_window_resize_requested_event_t windowResizeRequest);

		// Token: 0x0200287C RID: 10364
		// (Invoke) Token: 0x06013C09 RID: 80905
		public delegate void ImeCompositionRangeChangedDelegate(scene_ime_composition_range_changed_event_t rangeChanged);

		// Token: 0x0200287D RID: 10365
		// (Invoke) Token: 0x06013C0D RID: 80909
		public delegate void ImeStateChangedDelegate(scene_ime_state_changed_event_t imeStateChange);

		// Token: 0x0200287E RID: 10366
		// (Invoke) Token: 0x06013C11 RID: 80913
		public delegate void ImeTextSelectionChangedDelegate(scene_ime_text_selection_changed_event_t imeTextSelectionChanged);

		// Token: 0x0200287F RID: 10367
		// (Invoke) Token: 0x06013C15 RID: 80917
		public delegate void ImeSelectionBoundsChangedDelegate(scene_ime_selection_bounds_changed_event_t imeSelectionBoundsChanged);
	}
}
