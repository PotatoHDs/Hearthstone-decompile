using System;
using System.Runtime.InteropServices;

namespace Blizzard.Commerce
{
	public class blz_commerce_scene_rect_t : IDisposable
	{
		private HandleRef swigCPtr;

		protected bool swigCMemOwn;

		public int x
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_x_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_x_set(swigCPtr, value);
			}
		}

		public int y
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_y_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_y_set(swigCPtr, value);
			}
		}

		public int w
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_w_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_w_set(swigCPtr, value);
			}
		}

		public int h
		{
			get
			{
				return battlenet_commercePINVOKE.blz_commerce_scene_rect_t_h_get(swigCPtr);
			}
			set
			{
				battlenet_commercePINVOKE.blz_commerce_scene_rect_t_h_set(swigCPtr, value);
			}
		}

		internal blz_commerce_scene_rect_t(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(blz_commerce_scene_rect_t obj)
		{
			return obj?.swigCPtr ?? new HandleRef(null, IntPtr.Zero);
		}

		~blz_commerce_scene_rect_t()
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
						battlenet_commercePINVOKE.delete_blz_commerce_scene_rect_t(swigCPtr);
					}
					swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
			}
		}

		public blz_commerce_scene_rect_t()
			: this(battlenet_commercePINVOKE.new_blz_commerce_scene_rect_t(), cMemoryOwn: true)
		{
		}
	}
}
