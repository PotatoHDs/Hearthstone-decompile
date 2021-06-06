using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class scene_ime_selection_bounds_changed_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public blz_commerce_scene_rect_t anchor_rect
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_anchor_rect_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_anchor_rect_set(swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		public blz_commerce_scene_rect_t focus_rect
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_focus_rect_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_focus_rect_set(swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		public bool is_anchor_rect
		{
			get
			{
				return battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_is_anchor_rect_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.scene_ime_selection_bounds_changed_event_t_is_anchor_rect_set(swigCPtr, value);
			}
		}

		internal scene_ime_selection_bounds_changed_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(scene_ime_selection_bounds_changed_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~scene_ime_selection_bounds_changed_event_t()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero)
				{
					if (swigCMemOwn)
					{
						swigCMemOwn = false;
						battlenet_commercePINVOKE.delete_scene_ime_selection_bounds_changed_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public scene_ime_selection_bounds_changed_event_t()
			: this(battlenet_commercePINVOKE.new_scene_ime_selection_bounds_changed_event_t(), cMemoryOwn: true)
		{
		}
	}
}
