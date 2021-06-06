using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_scene_buffer_update_event_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public blz_commerce_scene_rect_t dirty_rects
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rects_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_scene_rect_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rects_set(swigCPtr, blz_commerce_scene_rect_t.getCPtr(value));
			}
		}

		public uint dirty_rect_size
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rect_size_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_dirty_rect_size_set(swigCPtr, value);
			}
		}

		public blz_commerce_buffer_t buffer
		{
			get
			{
				IntPtr intPtr = battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_buffer_get(swigCPtr);
				if (!(intPtr == IntPtr.Zero))
				{
					return new blz_commerce_buffer_t(intPtr, cMemoryOwn: false);
				}
				return null;
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_buffer_update_event_t_buffer_set(swigCPtr, blz_commerce_buffer_t.getCPtr(value));
			}
		}

		internal blz_commerce_scene_buffer_update_event_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_scene_buffer_update_event_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_scene_buffer_update_event_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_scene_buffer_update_event_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_scene_buffer_update_event_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_scene_buffer_update_event_t(), cMemoryOwn: true)
		{
		}
	}
}
